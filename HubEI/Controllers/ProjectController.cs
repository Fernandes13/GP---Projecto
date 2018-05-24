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
    public class ProjectController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProjectController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment;
        }

        [Route("Project")]
        [HttpGet]
        public IActionResult Project([FromQuery] string project_id)
        {
            Project project = _context.Project.Include(s => s.IdCompanyNavigation)
                                              .Include(s => s.IdProjectTypeNavigation)
                                              .Include(s => s.IdStudentNavigation)
                                              .Where(p => p.IdProject.ToString() == project_id).FirstOrDefault();

            var projectAdvisors = _context.ProjectAdvisor.Where(pa => pa.IdProject.ToString() == project_id)
                                                        .Include(pa => pa.IdSchoolMentorNavigation).ToList();

            var projectTechnologies = _context.ProjectTechnology.Where(pt => pt.IdProject.ToString() == project_id)
                                                                .Include(pt => pt.IdTechnologyNavigation).ToList();

            var projectDocument = _context.ProjectDocument.Where(pt => pt.IdProject.ToString() == project_id).ToList();

            project.ProjectAdvisor = projectAdvisors;
            project.ProjectTechnology = projectTechnologies;

            project.Views += 1;

            _context.Project.Update(project);
            _context.SaveChanges();

            return View(new LoginViewModel { Project = project });
        }

        public IActionResult _ProjectFile(byte[] report)
        {
            var file = File(report, "application/pdf");

            return PartialView(file);
        }

        public IActionResult GetProjectReport(string project_id)
        {
            byte[] file = _context.Project.Where(p => p.IdProject.ToString() == project_id).Select(p => p.Report).FirstOrDefault();

            return new FileContentResult(file, "application/pdf");
        }

        public FileResult DownloadReport(int projectId)
        {
            Project project = _context.Project.Where(p => p.IdProject == projectId).FirstOrDefault();

            project.Downloads += 1;
            _context.Project.Update(project);
            _context.SaveChanges();

            return File(project.Report, "application/pdf", project.Title + ".pdf");
        }

        public FileResult DownloadAttachment(int projectId, int documentId)
        {
            Project project = _context.Project.Where(p => p.IdProject == projectId).FirstOrDefault();

            project.Downloads += 1;
            _context.Project.Update(project);
            _context.SaveChanges();

            var document = _context.ProjectDocument.Where(p => p.IdProject == projectId && p.IdProjectDocument == documentId).FirstOrDefault();

            if(document.FileName.Substring(document.FileName.Length - 5) == ".xls")
            {
                return File(document.Document, "application/vnd.ms-excel", "Anexo de " + project.Title + ".xls");
            }
            else if(document.FileName.Substring(document.FileName.Length - 5) == ".xlsx")
            {
                return File(document.Document, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Anexo de " + project.Title + ".xlsx");
            }
            else
            {
                return File(document.Document, "application/pdf", "Anexo de " + project.Title + ".pdf");
            }
        }

        [Route("Projects")]
        public IActionResult List([FromQuery] string search_by, string technologies)
        {
            LoginViewModel viewModel = new LoginViewModel();

            var projects = _context.Project.AsQueryable();
            //var projects = _context.Project.AsQueryable();

            if (search_by == null || search_by == "null" || search_by.Trim() == "")
            {
                projects = projects.Include(s => s.IdCompanyNavigation)
                                                .Include(s => s.IdProjectTypeNavigation)
                                                .Include(s => s.IdStudentNavigation);
            }
            else
            {
                projects = projects.Where(p => p.Title.Contains(search_by))
                                                .Include(s => s.IdCompanyNavigation)
                                                .Include(s => s.IdProjectTypeNavigation)
                                                .Include(s => s.IdStudentNavigation);
            }

            if(technologies != null && technologies != "null" && technologies.Trim() != "")
            {
                foreach(var project in projects)
                {
                    var projectTechnologies = _context.ProjectTechnology.Where(pt => pt.IdProject == project.IdProject)
                                                                    .Include(pt => pt.IdTechnologyNavigation).ToList();

                    project.ProjectTechnology = projectTechnologies;
                }

                //var technologiesList = "";
                var technologiesList = new List<Technology>();

                var techQuery = technologies.Split(",");

                foreach(var tech in techQuery)
                {
                    //technologiesList += _context.Technology.Where(t => t.Description == tech).Select(p => p.IdTechnology).SingleOrDefault() + ",";
                    technologiesList.Add(_context.Technology.Where(t => t.Description == tech).SingleOrDefault());
                }

                projects = projects.Where(p => technologiesList.Any(tl => p.ProjectTechnology.Any(pcat => pcat.IdTechnologyNavigation == tl)));
            }

            var maxChars = 100;

            foreach (var project in projects)
            {
                project.Description = project.Description.Length <= maxChars ? project.Description : project.Description.Substring(0, maxChars) + "...";

                var projectAdvisors = _context.ProjectAdvisor.Where(pa => pa.IdProject == project.IdProject)
                                            .Include(pa => pa.IdSchoolMentorNavigation).ToList();

                project.ProjectAdvisor = projectAdvisors; 
            }

            viewModel.Projects = projects.Select(p => new Project {
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
                IdStudentNavigation = p.IdStudentNavigation}).ToList();

            return View(viewModel);
        }
    }
}