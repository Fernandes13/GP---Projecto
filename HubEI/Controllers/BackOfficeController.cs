using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HubEI.Models;
using HubEI.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;

namespace HubEI.Controllers
{
    /// <summary>
    /// Controller used for the actions affecting the BackOffice.
    /// </summary>
    /// <remarks></remarks>
    public class BackOfficeController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubEI.Controllers.BackOfficeController" /> class. 
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="HostingEnvironment">Hosting Environment</param>
        /// <remarks></remarks>
        public BackOfficeController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment; 
        }


        /// <summary>
        /// Gets the BackOffice mainpage
        /// </summary>
        /// <returns>BackOffice mainpage</returns>
        /// <remarks></remarks>
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            ViewData["projects"] = _context.Project.Count();
            ViewData["students"] = _context.Student.Count();
            ViewData["mentors"] = _context.SchoolMentor.Count();
            ViewData["companies"] = _context.Company.Count();

            return View();
        }

        /// <summary>
        /// Gets the Students Main List Backoffice page
        /// </summary>
        /// <returns>Students Main List Backoffice page</returns>
        /// <remarks></remarks>
        public IActionResult Students()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            TempData["minDate"] = DateTime.Now.AddYears(-70).ToString("MM-dd-yyyy");
            TempData["maxDate"] = DateTime.Now.AddYears(-18).ToString("MM-dd-yyyy");

            BOStudentViewModel viewModel = new BOStudentViewModel();
            var students = _context.Student.Include(s => s.IdStudentBranchNavigation).Include(s => s.IdAddressNavigation.IdDistrictNavigation);

            viewModel.Students = students;
            viewModel.Branches = PopulateBranches();
            viewModel.Districts = PopulateDistricts();

            return View(viewModel);
        }



        /// <summary>
        /// Populates a List with the available Branches in the database
        /// </summary>
        /// <returns>Populated List</returns>
        /// <remarks></remarks>
        private IEnumerable<SelectListItem> PopulateBranches()
        {
            List<SelectListItem> branchesSelectList = new List<SelectListItem>();

            var branchesList = _context.StudentBranch;

            foreach (StudentBranch sb in branchesList)
            {
                branchesSelectList.Add(new SelectListItem { Value = sb.IdStudentBranch.ToString(), Text = sb.Description });
            }

            return branchesSelectList;

        }

        /// <summary>
        /// Populates a List with the available Bustiness Areas in the database
        /// </summary>
        /// <returns>Populated List</returns>
        /// <remarks></remarks>
        private IEnumerable<SelectListItem> PopulateBusinessAreas()
        {
            List<SelectListItem> bareasSelectList = new List<SelectListItem>();

            var baList = _context.BusinessArea;

            foreach (BusinessArea sb in baList)
            {
                bareasSelectList.Add(new SelectListItem { Value = sb.IdBusinessArea.ToString(), Text = sb.Description });
            }

            return bareasSelectList;
        }

        /// <summary>
        /// Populates a List with the available Districts in the database
        /// </summary>
        /// <returns>Populated List</returns>
        /// <remarks></remarks>
        private IEnumerable<SelectListItem> PopulateDistricts()
        {
            List<SelectListItem> districtsSelectList = new List<SelectListItem>();

            var districtList = _context.District;

            foreach (District d in districtList)
            {
                districtsSelectList.Add(new SelectListItem { Value = d.IdDistrict.ToString(), Text = d.Description });
            }

            return districtsSelectList;

        }


        /// <summary>
        /// Gets a Student model based on student_id
        /// </summary>
        /// <param name="student_id">Student unique identifier</param>
        /// <returns>Student model</returns>
        /// <remarks></remarks>
        [HttpGet]
        public JsonResult GetStudent([FromQuery] string student_id)
        {
            Student std = _context.Student.Include(s => s.IdStudentBranchNavigation).Include(s => s.IdAddressNavigation).Where(st => st.IdStudent.ToString() == student_id).FirstOrDefault();

            return Json(std);
        }

        /// <summary>
        /// POST a student in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Students Main List Backoffice view</returns>
        /// <remarks></remarks>
        [HttpPost]
        public IActionResult Student(BOStudentViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            _context.Address.Add(model.Address);

            Student std = new Student
            {
                Email = model.Student.Email,
                Name = model.Student.Name,
                BirthDate = model.Student.BirthDate,
                Telephone = model.Student.Telephone,
                StudentNumber = model.Student.StudentNumber,
                IdStudentBranchNavigation = _context.StudentBranch.Where(sb => sb.IdStudentBranch == model.Student.IdStudentBranch).FirstOrDefault(),
                IdAddressNavigation = model.Address

            };

            _context.Student.Add(std);

            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Student added successfully.";

            return RedirectToAction("Students", "BackOffice");
        }

        /// <summary>
        /// PUTS(UPDATES) a student in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Students Main List Backoffice view</returns>
        /// <remarks></remarks>
        [HttpPost]
        public IActionResult EditStudent(BOStudentViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            model.Student.IdAddressNavigation = model.Address;

            _context.Student.Update(model.Student);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Student edited succesfully.";

            return RedirectToAction("Students", "BackOffice");
        }

        /// <summary>
        /// DELETS a student from the database
        /// </summary>
        /// <param name="StudentId">Student unique identifier</param>
        /// <returns>JSON operation success</returns>
        /// <remarks></remarks>
        [HttpDelete]
        public IActionResult Student([FromQuery]string StudentId)
        {
            Student aux_std = _context.Student.Where(std => std.IdStudent.ToString() == StudentId).FirstOrDefault();
            _context.Student.Remove(aux_std);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Student eliminated successfully.";

            return Json("Success");
        }


        /// <summary>
        /// Gets the Mentors Main List Backoffice page
        /// </summary>
        /// <returns>Mentors Main List Backoffice page</returns>
        /// <remarks></remarks>
        public IActionResult Mentors()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            BOMentorViewModel viewModel = new BOMentorViewModel();
            var mentors = _context.SchoolMentor;

            viewModel.Mentors = mentors;

            return View(viewModel);
        }



        /// <summary>
        /// POST a mentor in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Mentors Main List Backoffice page</returns>
        /// <remarks></remarks>
        [HttpPost]
        public IActionResult Mentor(BOMentorViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            SchoolMentor mentor = new SchoolMentor
            {
                IdSchoolMentor = model.Mentor.IdSchoolMentor,
                Name = model.Mentor.Name,
                Email = model.Mentor.Email,
                ProjectAdvisor = model.Mentor.ProjectAdvisor
            };

            _context.SchoolMentor.Add(mentor);

            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Mentor added successfully.";

            return RedirectToAction("Mentors", "BackOffice");
        }

        /// <summary>
        /// Gets a mentor model based on mentor_id
        /// </summary>
        /// <param name="mentor_id">Mentor unique identifier</param>
        /// <returns>JSON with the mentor</returns>
        /// <remarks></remarks>
        [HttpGet]
        public JsonResult GetMentor([FromQuery] string mentor_id)
        {
            SchoolMentor std = _context.SchoolMentor.Where(st => st.IdSchoolMentor.ToString() == mentor_id).FirstOrDefault();

            return Json(std);
        }


        /// <summary>
        /// PUTS(UPDATES) a mentor in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Mentors Main List Backoffice view</returns>
        /// <remarks></remarks>
        [HttpPost]
        public IActionResult EditMentor(BOMentorViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            _context.SchoolMentor.Update(model.Mentor);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Mentor edited successfully.";

            return RedirectToAction("Mentors", "BackOffice");
        }

        /// <summary>
        /// Deletes a mentor in the database
        /// </summary>
        /// <param name="MentorId">Mentor unique identifier</param>
        /// <returns>JSON operation success</returns>
        /// <remarks></remarks>
        [HttpDelete]
        public IActionResult Mentor([FromQuery]string MentorId)
        {
            SchoolMentor aux_mentor = _context.SchoolMentor.Where(std => std.IdSchoolMentor.ToString() == MentorId).FirstOrDefault();
            Console.WriteLine(aux_mentor);
            _context.SchoolMentor.Remove(aux_mentor);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Mentor deleted successfully.";

            return Json("Success");
        }

        /// <summary>
        /// GETS the main projects list view
        /// </summary>
        /// <returns>Main projects list view</returns>
        /// <remarks></remarks>
        public IActionResult Projects()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            BOProjectViewModel viewModel = new BOProjectViewModel();
            var projects = _context.Project.Include(s => s.IdCompanyNavigation)
                                            .Include(s => s.IdProjectTypeNavigation)
                                            .Include(s => s.IdStudentNavigation)
                                            .Include(s => s.IdBusinessAreaNavigation).ToList();


            viewModel.Projects = projects;
            viewModel.Companies = PopulateCompanies();
            viewModel.ProjectTypes = PopulateProjectTypes();
            viewModel.Students = PopulateStudents();
            viewModel.BusinessAreas = PopulateBusinessAreas();

            var mentors = _context.SchoolMentor.ToList();

            viewModel.Mentors = new List<MentorsCheckBox>();

            foreach (SchoolMentor mentor in mentors)
            {
                viewModel.Mentors.Add(new MentorsCheckBox { SchoolMentor = mentor, Selected = false });
            }

            return View(viewModel);
        }

        /// <summary>
        /// PUTS(UPDATES) a project in the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Projects Main List Backoffice view</returns>
        /// <remarks></remarks>
        public async Task<IActionResult> EditProject(BOProjectViewModel viewModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            if (viewModel.Report != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.Report.CopyToAsync(memoryStream);
                    viewModel.Project.Report = memoryStream.ToArray();
                }
            }

            viewModel.Project.ProjectAdvisor = _context.ProjectAdvisor.Where(pa => pa.IdProject == viewModel.Project.IdProject).ToList();

            foreach (var advisor in viewModel.Project.ProjectAdvisor)
            {
                _context.ProjectAdvisor.Remove(advisor);
                await _context.SaveChangesAsync();
            }

            foreach (var mentor in viewModel.Mentors)
            {
                if (mentor.Selected)
                {
                    var advisor = new ProjectAdvisor { IdProject = viewModel.Project.IdProject, IdSchoolMentor = mentor.SchoolMentor.IdSchoolMentor };
                    await _context.ProjectAdvisor.AddAsync(advisor);
                    await _context.SaveChangesAsync();
                }
            }

            viewModel.Project.ProjectAdvisor = new List<ProjectAdvisor>();

            if(viewModel.Attachments != null)
            {
                if (viewModel.Attachments.Count() > 0)
                {
                    viewModel.Project.ProjectDocument = _context.ProjectDocument.Where(pd => pd.IdProject == viewModel.Project.IdProject).ToList();

                    foreach (var document in viewModel.Project.ProjectDocument)
                    {
                        _context.ProjectDocument.Remove(document);
                        await _context.SaveChangesAsync();
                    }

                        foreach (var formFile in viewModel.Attachments)
                        {
                            if (formFile.Length > 0)
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    await formFile.CopyToAsync(memoryStream);
                                    var document = new ProjectDocument
                                    {
                                        IdProject = viewModel.Project.IdProject,
                                        Document = memoryStream.ToArray(),
                                        FileName = formFile.FileName,
                                        FileSize = Convert.ToDouble(Convert.ToDecimal(formFile.Length) / 1024m / 1024m)
                                    };

                                    await _context.ProjectDocument.AddAsync(document);
                                    await _context.SaveChangesAsync();
                                }
                            }
                        }
                    

                    viewModel.Project.ProjectDocument = new List<ProjectDocument>();
                }
            }

            

            _context.Project.Update(viewModel.Project);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Project edited successfully.";

            return RedirectToAction("Projects", "BackOffice");
        }


        
        /// <summary>
        /// Populates a List with the available Companies in the database
        /// </summary>
        /// <returns>Populated List</returns>
        /// <remarks></remarks>
        private IEnumerable<SelectListItem> PopulateCompanies()
        {
            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                List<SelectListItem> lisCompanies = new List<SelectListItem>();

                var listCompanies = context.Company.OrderBy(x => x.Name).ToList();

                foreach (Company n in listCompanies)
                {
                    lisCompanies.Add(new SelectListItem { Value = n.IdCompany.ToString(), Text = n.Name });
                }

                return lisCompanies;
            }
        }

        
        /// <summary>
        /// Populates a List with the available Students in the database
        /// </summary>
        /// <returns>Populated List</returns>
        /// <remarks></remarks>
        private IEnumerable<SelectListItem> PopulateStudents()
        {
            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                List<SelectListItem> lisStudents = new List<SelectListItem>();

                var listStudents = context.Student.OrderBy(x => x.Name).ToList();

                foreach (Student n in listStudents)
                {
                    lisStudents.Add(new SelectListItem { Value = n.IdStudent.ToString(), Text = n.Name });
                }

                return lisStudents;
            }
        }

        
        /// <summary>
        /// Populates a List with the available Project Types in the database
        /// </summary>
        /// <returns>Populated List</returns>
        /// <remarks></remarks>
        private IEnumerable<SelectListItem> PopulateProjectTypes()
        {
            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                List<SelectListItem> lisProjectTypes = new List<SelectListItem>();

                var listProjectTypes = context.ProjectType.ToList();

                foreach (ProjectType n in listProjectTypes)
                {
                    lisProjectTypes.Add(new SelectListItem { Value = n.IdProjectType.ToString(), Text = n.Description });
                }

                return lisProjectTypes;
            }
        }

        /// <summary>
        /// Gets a project information in JSON
        /// </summary>
        /// <param name="project_id">Project unique identifier</param>
        /// <returns>Project in JSON</returns>
        /// <remarks></remarks>
        [HttpGet]
        public JsonResult GetProject([FromQuery] string project_id)
        {
            Project project = _context.Project.Include(s => s.IdCompanyNavigation)
                                              .Include(s => s.IdProjectTypeNavigation)
                                              .Include(s => s.IdStudentNavigation)
                                              .Where(p => p.IdProject.ToString() == project_id).FirstOrDefault();

            project.ProjectAdvisor = _context.ProjectAdvisor.Where(pa => pa.IdProject == project.IdProject).ToList();


            project.ProjectTechnology = _context.ProjectTechnology.Where(pt => pt.IdProject == project.IdProject)
                                                                .Include(pt => pt.IdTechnologyNavigation).ToList();

            return Json(project);
        }

        /// <summary>
        /// POST a project in the database.
        /// </summary>
        /// <param name="model">Project viewmodel.</param>
        /// <returns>Projects list backoffice view</returns>
        /// <remarks></remarks>
        [HttpPost]
        public async Task<IActionResult> Project(BOProjectViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                byte[] file = null;
                byte[] video = null;

                List<ProjectDocument> attachments = new List<ProjectDocument>();

                if (model.Attachments != null)
                {
                    foreach (var formFile in model.Attachments)
                    {
                        if (formFile.Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await formFile.CopyToAsync(memoryStream);
                                attachments.Add(new ProjectDocument
                                {
                                    Document = memoryStream.ToArray(),
                                    FileName = formFile.FileName,
                                    FileSize = formFile.Length / 1024 / 1024
                                });
                            }
                        }
                    }
                }

                using (var memoryStream = new MemoryStream())
                {
                    await model.Report.CopyToAsync(memoryStream);
                    file = memoryStream.ToArray();
                }

                if(model.Video != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Video.CopyToAsync(memoryStream);
                        video = memoryStream.ToArray();
                    }
                }

                var project = new Project
                {
                    Title = model.Project.Title,
                    Description = model.Project.Description,
                    Report = file,
                    ProjectDate = model.Project.ProjectDate,
                    IsVisible = model.Project.IsVisible,
                    IdCompany = model.Project.IdCompany,
                    IdProjectType = model.Project.IdProjectType,
                    IdStudent = model.Project.IdStudent,
                    Grade = model.Project.Grade,
                    IdBusinessArea = model.Project.IdBusinessArea,
                    Video = video
                };

                _context.Add(project);
                await _context.SaveChangesAsync();

                foreach (var attachment in attachments)
                {
                    attachment.IdProject = project.IdProject;
                    await _context.AddAsync(attachment);
                }

                _context.SaveChanges();

                foreach (MentorsCheckBox mentor in model.Mentors)
                {
                    if (mentor.Selected)
                    {
                        var newMentor = new ProjectAdvisor
                        {
                            IdProject = project.IdProject,
                            IdSchoolMentor = mentor.SchoolMentor.IdSchoolMentor
                        };

                        _context.Add(newMentor);
                    }
                }

                _context.SaveChanges();

                if (model.Project.IdCompany != 0)
                {
                    if (model.Project.IdCompany == null) model.Project.IdCompany = 0;
                    SendCompanyEmail((long)model.Project.IdCompany, model.Project.Title, model.Project.IdStudent);
                }

                return RedirectToAction("Projects", "BackOffice");
            }

            return RedirectToAction("Projects", "BackOffice");
        }

        /// <summary>
        /// Sends an email to a company, indicating the privacy policies associated with the submission of a project.
        /// </summary>
        /// <param name="idCompany">Company's unique identifier</param>
        /// <param name="projectTitle">Project Title</param>
        /// <param name="studentId">Students's unique identifier</param>
        /// <remarks></remarks>
        public void SendCompanyEmail(long idCompany, string projectTitle, long studentId)
        {
            if(idCompany != 0)
            {
                var email = _context.Company.Where(c => c.IdCompany == idCompany).Select(c => c.Email).SingleOrDefault();
                var studentName = _context.Student.Where(s => s.IdStudent == studentId).Select(s => s.Name).FirstOrDefault();

                var strbBody = new StringBuilder();
                strbBody.AppendLine("To whom this may concern,<br><br>");
                strbBody.AppendFormat(@"We are pleased to announce that a project that was developed under your company by " + studentName + " with the title '" + projectTitle + "'");
                strbBody.AppendFormat(@" was submitted to our platform, HubEI. Due to the Terms and Conditions of the Polytechnic Institute of Setúbal");
                strbBody.AppendFormat(@" all the information about this project, including the contents of the documents developed by the author");
                strbBody.AppendFormat(@" are to be made public in our platform. Please reply to this email, informing if you do or do not consent with this.");
                strbBody.AppendFormat(@" Thank you kindly, in advance.<br><br>");
                strbBody.AppendLine(@"Best Regards,<br>HubEI Team");

                Email.SendEmail(email, "Publish permission regarding data protection", strbBody.ToString());
            }
        }

        /// <summary>
        /// Adds a technology to the database. If it already exists, ignores and doesn't insert.
        /// </summary>
        /// <param name="tech">Technology name</param>
        /// <returns>JSON operation success</returns>
        /// <remarks></remarks>
        [HttpPost]
        public async Task<IActionResult> AddProjectTechnology([FromQuery] string tech)
        {
            var last_project = _context.Project.OrderByDescending(p => p.IdProject).FirstOrDefault();

            var technology = _context.Technology.Where(t => t.Description.ToLower() == tech.ToLower()).FirstOrDefault();

            if (technology == null)
            {
                //CREATE TECHNOLOGY
                await _context.Technology.AddAsync(new Technology
                {
                    Description = tech
                });

                await _context.SaveChangesAsync();

                var last_technoloy = await _context.Technology.OrderByDescending(t => t.IdTechnology).FirstOrDefaultAsync();
                await _context.ProjectTechnology.AddAsync(new ProjectTechnology
                {
                    IdProject = last_project.IdProject,
                    IdTechnology = last_technoloy.IdTechnology
                });

                await _context.SaveChangesAsync();
            }

            else
            {
                await _context.ProjectTechnology.AddAsync(new ProjectTechnology
                {
                    IdProject = last_project.IdProject,
                    IdTechnology = technology.IdTechnology
                });

                await _context.SaveChangesAsync();
            }

            return Json("");
        }

        /// <summary>
        /// DELETE All project technologiy association
        /// </summary>
        /// <param name="project_id">Project Unique identifier</param>
        /// <returns>JSON Sucess operation</returns>
        /// <remarks></remarks>
        [HttpDelete]
        public IActionResult ClearProjectTechnologies([FromQuery] string project_id)
        {
            _context.Database.ExecuteSqlCommand("DELETE Project_technology WHERE id_project = " + project_id);

            _context.SaveChanges();
            return Json("");
        }

        /// <summary>
        /// Adds a technologies to a project.
        /// </summary>
        /// <param name="project_id">Project unique identifier</param>
        /// <param name="tech">Technologie</param>
        /// <returns>JSON - success operation</returns>
        /// <remarks></remarks>
        [HttpPost]
        public async Task<IActionResult> EditProjectTechnology([FromQuery] string project_id, [FromQuery] string tech)
        {
            var project = _context.Project.Where(p => p.IdProject.ToString() == project_id).FirstOrDefault();

            var technology = await _context.Technology.Where(t => t.Description.ToLower() == tech.ToLower()).FirstOrDefaultAsync();

            if (technology == null)
            {
                //CREATE TECHNOLOGY
                _context.Technology.Add(new Technology
                {
                    Description = tech
                });
                _context.SaveChanges();

                var last_technology = _context.Technology.OrderByDescending(t => t.IdTechnology).FirstOrDefault();
                _context.ProjectTechnology.Add(new ProjectTechnology
                {
                    IdProject = project.IdProject,
                    IdTechnology = last_technology.IdTechnology
                });

                _context.SaveChanges();
            }
            else
            {
                _context.ProjectTechnology.Add(new ProjectTechnology
                {
                    IdProject = project.IdProject,
                    IdTechnology = technology.IdTechnology
                });
                _context.SaveChanges();
            }
            return Json("");
        }


        /// <summary>
        /// DELETE a Project
        /// </summary>
        /// <param name="ProjectId">Project unique identifier</param>
        /// <returns>JSON - Operation Success</returns>
        /// <remarks></remarks>
        [HttpDelete]
        public IActionResult Project([FromQuery]string ProjectId)
        {
            Project project = _context.Project.Where(p => p.IdProject.ToString() == ProjectId).FirstOrDefault();

            project.ProjectAdvisor = _context.ProjectAdvisor.Where(pa => pa.IdProject == project.IdProject).ToList();

            foreach (var advisor in project.ProjectAdvisor)
            {
                _context.ProjectAdvisor.Remove(advisor);
            }

            project.ProjectAdvisor = new List<ProjectAdvisor>();

            project.ProjectDocument = _context.ProjectDocument.Where(pd => pd.IdProject == project.IdProject).ToList();

            foreach (var document in project.ProjectDocument)
            {
                _context.ProjectDocument.Remove(document);
            }

            project.ProjectDocument = new List<ProjectDocument>();

            project.ProjectTechnology = _context.ProjectTechnology.Where(pt => pt.IdProject == project.IdProject).ToList();

            foreach (var technology in project.ProjectTechnology)
            {
                _context.ProjectTechnology.Remove(technology);
            }

            project.ProjectTechnology = new List<ProjectTechnology>();

            _context.Project.Remove(project);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Project eliminated successfully.";

            return Json("Success");
        }

        /// <summary>
        /// Gets the Technologies list view.
        /// </summary>
        /// <returns>Technologies list view</returns>
        /// <remarks></remarks>
        public IActionResult Technologies()
        {
            var technologies = _context.Technology.ToList();
            return Json(technologies);
        }

        /// <summary>
        /// Gets the Companies list view.
        /// </summary>
        /// <returns>Companies list view</returns>
        /// <remarks></remarks>
        public IActionResult Companies()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            BOCompanyViewModel viewModel = new BOCompanyViewModel();
            var companies = _context.Company;

            viewModel.Companies = companies;
            viewModel.Districts = PopulateDistricts();
            return View(viewModel);
        }

        /// <summary>
        /// POSTS a company to the database.
        /// </summary>
        /// <param name="model">BOCompanyViewModel company model</param>
        /// <returns>Companies list backoffice view</returns>
        /// <remarks></remarks>
        [HttpPost]
        public IActionResult Company(BOCompanyViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            Company company = new Company
            {
                IdCompany = model.Company.IdCompany,
                Name = model.Company.Name,
                Description = model.Company.Description,
                IdDistrict = model.Company.IdDistrict,
                Email = model.Company.Email
            };

            _context.Company.Add(company);

            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Company added successfully.";

            return RedirectToAction("Companies", "BackOffice");
        }

        /// <summary>
        /// GET Company
        /// </summary>
        /// <param name="company_id">Company unique identifier</param>
        /// <returns>Company</returns>
        /// <remarks></remarks>
        [HttpGet]
        public JsonResult GetCompany([FromQuery] string company_id)
        {
            Company company = _context.Company.Where(st => st.IdCompany.ToString() == company_id).FirstOrDefault();

            return Json(company);
        }

        /// <summary>
        /// EDIT Company
        /// </summary>
        /// <param name="model">BOCompanyViewModel company model to edit</param>
        /// <returns>BackOffice view companies</returns>
        /// <remarks></remarks>
        [HttpPost]
        public IActionResult EditCompany(BOCompanyViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Got-Error"] = "true";
                TempData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            _context.Company.Update(model.Company);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Company edited successfully.";

            return RedirectToAction("Companies", "BackOffice");
        }

        /// <summary>
        /// DELETE a Company
        /// </summary>
        /// <param name="CompanyId">Company unique identifier</param>
        /// <returns>JSON - Operation Success</returns>
        /// <remarks></remarks>
        [HttpDelete]
        public IActionResult Company([FromQuery]string CompanyId)
        {
            Company aux_company = _context.Company.Where(std => std.IdCompany.ToString() == CompanyId).FirstOrDefault();
            Console.WriteLine(aux_company);
            _context.Company.Remove(aux_company);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Company deleted successfully.";

            return Json("Success");
        }
    }
}