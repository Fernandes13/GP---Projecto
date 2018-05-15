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


namespace HubEI.Controllers
{
    public class BackOfficeController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public BackOfficeController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment;
        }

        public IActionResult Index()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}


            return View();
        }

        public IActionResult Students()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

            BOStudentViewModel viewModel = new BOStudentViewModel();
            var students = _context.Student.Include(s => s.IdStudentBranchNavigation).Include(s => s.IdAddressNavigation.IdDistrictNavigation);

            viewModel.Students = students;
            viewModel.Branches = PopulateBranches();
            viewModel.Districts = PopulateDistricts();

            //return await PaginatedList<Technician>.CreateAsync(technicians.AsNoTracking(), intTechniciansPageNumber, intPendingPageSize);

            return View(viewModel);
        }

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

        [HttpGet]
        public JsonResult GetStudent([FromQuery] string student_id)
        {
            Student std = _context.Student
                .Include(s => s.IdStudentBranchNavigation)
                .Include(s => s.IdAddressNavigation)
                .Where(st => st.IdStudent.ToString() == student_id).FirstOrDefault();

            return Json(std);
        }

        [HttpPost]
        public IActionResult Student(BOStudentViewModel model)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

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
            TempData["AlertMessage"] = "Estudante adicionado com sucesso.";

            return RedirectToAction("Students", "BackOffice");
        }

        [HttpPost]
        public IActionResult EditStudent(BOStudentViewModel model)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

            model.Student.IdAddressNavigation = model.Address;

            _context.Student.Update(model.Student);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Estudante editado com sucesso.";

            return RedirectToAction("Students", "BackOffice");
        }

        [HttpDelete]
        public IActionResult Student([FromQuery]string StudentId)
        {
            Student aux_std = _context.Student.Where(std => std.IdStudent.ToString() == StudentId).FirstOrDefault();
            _context.Student.Remove(aux_std);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Estudante eliminado com sucesso.";

            return Json("Success");
        }

        public IActionResult Mentors()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

            BOMentorViewModel viewModel = new BOMentorViewModel();
            var mentors = _context.SchoolMentor;

            viewModel.Mentors = mentors;


            //return await PaginatedList<Technician>.CreateAsync(technicians.AsNoTracking(), intTechniciansPageNumber, intPendingPageSize);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Mentor(BOMentorViewModel model)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

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
            TempData["AlertMessage"] = "Orientador adicionado com sucesso.";

            return RedirectToAction("Mentors", "BackOffice");
        }

        [HttpGet]
        public JsonResult GetMentor([FromQuery] string mentor_id)
        {
            SchoolMentor std = _context.SchoolMentor.Where(st => st.IdSchoolMentor.ToString() == mentor_id).FirstOrDefault();

            return Json(std);
        }

        [HttpPost]
        public IActionResult EditMentor(BOMentorViewModel model)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

            _context.SchoolMentor.Update(model.Mentor);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Orientador editado com sucesso.";

            return RedirectToAction("Mentors", "BackOffice");
        }

        [HttpDelete]
        public IActionResult Mentor([FromQuery]string MentorId)
        {
            SchoolMentor aux_mentor = _context.SchoolMentor.Where(std => std.IdSchoolMentor.ToString() == MentorId).FirstOrDefault();
            Console.WriteLine(aux_mentor);
            _context.SchoolMentor.Remove(aux_mentor);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Orientador eliminado com sucesso.";

            return Json("Success");
        }

        public IActionResult Projects()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

            BOProjectViewModel viewModel = new BOProjectViewModel();
            var projects = _context.Project.Include(s => s.IdCompanyNavigation)
                                            .Include(s => s.IdProjectTypeNavigation)
                                            .Include(s => s.IdStudentNavigation);


            viewModel.Projects = projects;
            viewModel.Companies = PopulateCompanies();
            viewModel.ProjectTypes = PopulateProjectTypes();
            viewModel.Students = PopulateStudents();

            var mentors = _context.SchoolMentor.ToList();

            viewModel.Mentors = new List<MentorsCheckBox>();

            foreach (SchoolMentor mentor in mentors)
            {
                viewModel.Mentors.Add(new MentorsCheckBox { SchoolMentor = mentor, Selected = false });
            }

            return View(viewModel);
        }

        public async Task<IActionResult> EditProject(BOProjectViewModel viewModel)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

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

            _context.Project.Update(viewModel.Project);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Projecto editado com sucesso.";

            return RedirectToAction("Projects", "BackOffice");
        }

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

        [HttpPost]
        public async Task<IActionResult> Project(BOProjectViewModel model)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    ViewData["Got-Error"] = "true";
            //    ViewData["Login-Message"] = "É necessário iniciar sessão";

            //    return RedirectToAction("Index", "Home");
            //}

            if (ModelState.IsValid)
            {
                using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
                {
                    byte[] file = null;

                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Report.CopyToAsync(memoryStream);
                        file = memoryStream.ToArray();
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
                        IdStudent = model.Project.IdStudent
                    };

                    context.Add(project);
                    await context.SaveChangesAsync();

                    foreach (MentorsCheckBox mentor in model.Mentors)
                    {
                        if (mentor.Selected)
                        {
                            var newMentor = new ProjectAdvisor
                            {
                                IdProject = project.IdProject,
                                IdSchoolMentor = mentor.SchoolMentor.IdSchoolMentor
                            };

                            context.Add(newMentor);
                        }
                    }

                    await context.SaveChangesAsync();


                    return RedirectToAction("Projects", "BackOffice");
                }

            }

            return RedirectToAction("Projects", "BackOffice");
        }

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

            _context.Project.Remove(project);
            _context.SaveChanges();

            TempData["HasAlert"] = "true";
            TempData["AlertMessage"] = "Projecto eliminado com sucesso.";

            return Json("Success");
        }

        public IActionResult Technologies()
        {
            var technologies = _context.Technology.ToList();



            return Json(technologies);
        }

    }

}