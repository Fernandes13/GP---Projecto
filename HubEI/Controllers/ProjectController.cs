using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubEI.Models;
using HubEI.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HubEI.Controllers
{
    /// <summary>
    /// Controller used for the actions affecting a Project.
    /// Has methods to list all projects, to view a project details and to manage the project attachments.
    /// </summary>
    /// <remarks></remarks>
    public class ProjectController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubEI.Controllers.ProjectController" /> class. 
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="HostingEnvironment">Hosting Environment</param>
        /// <remarks></remarks>
        public ProjectController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment;
        }


        /// <summary>
        /// Action to show a project in details.
        /// It shows all project properties and the project mentors, technologies, and documents.
        /// </summary>
        /// <param name="project_id">Project Identification</param>
        /// <returns>View with Project</returns>
        /// <remarks></remarks>
        [Route("Project")]
        [HttpGet]
        public IActionResult Project([FromQuery] string project_id)
        {
            Project project = _context.Project.Include(s => s.IdCompanyNavigation)
                                              .Include(s => s.IdProjectTypeNavigation)
                                              .Include(s => s.IdStudentNavigation)
                                              .Include(s => s.IdBusinessAreaNavigation)
                                              .Where(p => p.IdProject.ToString() == project_id).FirstOrDefault();

            var projectAdvisors = _context.ProjectAdvisor.Where(pa => pa.IdProject.ToString() == project_id)
                                                        .Include(pa => pa.IdSchoolMentorNavigation).ToList();

            var projectTechnologies = _context.ProjectTechnology.Where(pt => pt.IdProject.ToString() == project_id)
                                                                .Include(pt => pt.IdTechnologyNavigation).ToList();

            var projectDocument = _context.ProjectDocument.Where(pt => pt.IdProject.ToString() == project_id).ToList();

            if(project != null)
            {
                project.ProjectAdvisor = projectAdvisors;
                project.ProjectTechnology = projectTechnologies;

                project.Views += 1;

                _context.Project.Update(project);
                _context.SaveChanges();
            }

            return View(new LoginViewModel { Project = project });
        }

        /// <summary>
        /// Returns the project file and the browser may download the file.
        /// </summary>
        /// <param name="report">File data</param>
        /// <returns>Partial View with file</returns>
        /// <remarks></remarks>
        public IActionResult _ProjectFile(byte[] report)
        {
            var file = File(report, "application/pdf");

            return PartialView(file);
        }


        /// <summary>
        /// Action that gets the project report.
        /// </summary>
        /// <param name="project_id">Project unique identification</param>
        /// <returns>Project report in file</returns>
        /// <remarks></remarks>
        public IActionResult GetProjectReport(string project_id)
        {
            byte[] file = _context.Project.Where(p => p.IdProject.ToString() == project_id).Select(p => p.Report).FirstOrDefault();

            return new FileContentResult(file, "application/pdf");
        }

        /// <summary>
        /// Action that gets the project video.
        /// </summary>
        /// <param name="project_id">Project unique identification</param>
        /// <returns>Project video in file</returns>
        /// <remarks></remarks>
        public IActionResult GetProjectVideo(string project_id)
        {
            byte[] file = _context.Project.Where(p => p.IdProject.ToString() == project_id).Select(p => p.Video).FirstOrDefault();

            return new FileContentResult(file, "video/mp4");
        }

        /// <summary>
        /// Method that downloads the project report
        /// </summary>
        /// <param name="projectId">Project unique identification</param>
        /// <returns>Project report file</returns>
        /// <remarks></remarks>
        public FileResult DownloadReport(int projectId)
        {
            Project project = _context.Project.Where(p => p.IdProject == projectId).FirstOrDefault();

            project.Downloads += 1;
            _context.Project.Update(project);
            _context.SaveChanges();

            return File(project.Report, "application/pdf", project.Title + ".pdf");
        }

        /// <summary>
        /// Method that downloads a project attachment
        /// </summary>
        /// <param name="projectId">Project unique identification</param>
        ///  <param name="documentId">Document unique identification</param>
        /// <returns>Project attachment file</returns>
        /// <remarks></remarks>
        public FileResult DownloadAttachment(int projectId, int documentId)
        {
            Project project = _context.Project.Where(p => p.IdProject == projectId).FirstOrDefault();

            project.Downloads += 1;
            _context.Project.Update(project);
            _context.SaveChanges();

            var document = _context.ProjectDocument.Where(p => p.IdProject == projectId && p.IdProjectDocument == documentId).FirstOrDefault();

            if (document.FileName.Substring(document.FileName.Length - 5) == ".xls")
            {
                return File(document.Document, "application/vnd.ms-excel", "Anexo de " + project.Title + ".xls");
            }
            else if (document.FileName.Substring(document.FileName.Length - 5) == ".xlsx")
            {
                return File(document.Document, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Anexo de " + project.Title + ".xlsx");
            }
            else
            {
                return File(document.Document, "application/pdf", "Anexo de " + project.Title + ".pdf");
            }
        }

        /// <summary>
        /// Lists submitted projects based on three filters: Title/Description, Technologias and Marks
        /// </summary>
        /// <param name="q">Title/Description filter criteria</param>
        /// <param name="technologies">Technologies filter criteria</param>
        /// <param name="marks">Evaluation filter criteria</param>
        /// <returns>View with Projects List</returns>
        /// <remarks></remarks>
        [Route("Projects")]
        public IActionResult List([FromQuery] string q, string technologies, string marks)
        {
            LoginViewModel viewModel = new LoginViewModel();

            var projects = _context.Project.AsQueryable();
            //var projects = _context.Project.AsQueryable();

            if (q == null || q == "" || q.Trim() == "")
            {
                projects = projects.Include(s => s.IdCompanyNavigation)
                                                .Include(s => s.IdProjectTypeNavigation)
                                                .Include(s => s.IdStudentNavigation);
            }
            else
            {
                projects = projects.Where(p => p.Title.Contains(q))
                                                .Include(s => s.IdCompanyNavigation)
                                                .Include(s => s.IdProjectTypeNavigation)
                                                .Include(s => s.IdStudentNavigation);
            }

            if (technologies != null && technologies != "null" && technologies.Trim() != "")
            {
                foreach (var project in projects)
                {
                    var projectTechnologies = _context.ProjectTechnology.Where(pt => pt.IdProject == project.IdProject)
                                                                    .Include(pt => pt.IdTechnologyNavigation).ToList();

                    project.ProjectTechnology = projectTechnologies;
                }

                var technologiesList = new List<Technology>();

                var techQuery = technologies.Split(",");

                foreach (var tech in techQuery)
                {
                    technologiesList.Add(_context.Technology.Where(t => t.Description == tech).SingleOrDefault());
                }

                projects = projects.Where(p => technologiesList.Any(tl => p.ProjectTechnology.Any(pcat => pcat.IdTechnologyNavigation == tl)));
            }

            if (marks != null && marks != "null")
            {

                var marks_str = marks.Split('_');
                projects = projects.Where(p => p.Grade >= Int32.Parse(marks_str[0]) && p.Grade <= Int32.Parse(marks_str[1]));
            }


            var maxChars = 100;

            foreach (var project in projects)
            {
                project.Description = project.Description.Length <= maxChars ? project.Description : project.Description.Substring(0, maxChars) + "...";

                var projectAdvisors = _context.ProjectAdvisor.Where(pa => pa.IdProject == project.IdProject)
                                            .Include(pa => pa.IdSchoolMentorNavigation).ToList();

                project.ProjectAdvisor = projectAdvisors;
            }

            viewModel.Projects = projects.Select(p => new Project
            {
                IdProject = p.IdProject,
                Title = p.Title,
                Description = p.Description.Length <= maxChars ? p.Description : p.Description.Substring(0, maxChars) + "...",
                ProjectDate = p.ProjectDate,
                IsVisible = p.IsVisible,
                IdCompany = p.IdCompany,
                IdProjectType = p.IdProjectType,
                IdStudent = p.IdStudent,
                IdCompanyNavigation = p.IdCompanyNavigation,
                IdProjectTypeNavigation = p.IdProjectTypeNavigation,
                IdStudentNavigation = p.IdStudentNavigation,
                ProjectAdvisor = p.ProjectAdvisor
            }).ToList();

            return View(viewModel);
        }

        /// <summary>
        /// Action that shows a project mentor in detail.
        /// </summary>
        /// <param name="mentor_id">Mentor unique identification</param>
        /// <returns>View with mentor object</returns>
        /// <remarks></remarks>
        [Route("Mentor")]
        [HttpGet]
        public IActionResult Mentor([FromQuery] string mentor_id)
        {
            SchoolMentor mentor = _context.SchoolMentor.Where(x => x.IdSchoolMentor.ToString() == mentor_id).FirstOrDefault();

            ICollection<ProjectAdvisor> projectAdvisors = _context.ProjectAdvisor.Include(x => x.IdProjectNavigation)
                                                                                 .Include(y => y.IdSchoolMentorNavigation)
                                                                                 .Where(x => x.IdSchoolMentor.ToString() == mentor_id).ToList();

            ICollection<Project> projects = _context.Project.Include(p => p.IdStudentNavigation)
                                                            .Include(x => x.ProjectAdvisor)
                                                    .Where(p => _context.ProjectAdvisor.Where(x => x.IdSchoolMentor.ToString() == mentor_id).Any(pa => pa.IdProject == p.IdProject)).ToList();

            var average = projects.Average(x => x.Grade);
            Console.WriteLine(average);
            return View(new LoginViewModel { mentorViewModel = new ProjectMentorViewModel { Mentor = mentor, Projects = projects, AverageGradeGiven = average } });
        }
    }
}
