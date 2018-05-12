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
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public IActionResult Students()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            BOStudentViewModel viewModel = new BOStudentViewModel();
            var students = _context.Student.Include(s => s.IdStudentBranchNavigation).Include(s => s.IdAddressNavigation.IdDistrictNavigation);

            viewModel.Students = students;
            viewModel.Branches = PopulateBranches();
            viewModel.Districts = PopulateDistricts();

            //return await PaginatedList<Technician>.CreateAsync(technicians.AsNoTracking(), intTechniciansPageNumber, intPendingPageSize);

            return View(viewModel);
        }

        public IActionResult Mentors()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            BOMentorViewModel viewModel = new BOMentorViewModel();
            var mentors = _context.SchoolMentor;

            viewModel.Mentors = mentors;


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
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

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
            TempData["AlertMessage"] = "Estudante adicionado com sucesso.";

            return RedirectToAction("Students", "BackOffice");
        }

        [HttpPost]
        public IActionResult EditStudent(BOStudentViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

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

        public IActionResult Projects()
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            return PartialView("_CreateProject",
                new ProjectViewModel { Companies = PopulateCompanies(), Students = PopulateStudents(), ProjectTypes = PopulateProjectTypes(), ProjectDate = DateTime.Now.Date });
        }

        public IActionResult CreateProjectTest()
        {
            return View(
                new ProjectViewModel { Companies = PopulateCompanies(), Students = PopulateStudents(), ProjectTypes = PopulateProjectTypes() });
        }

        public async Task<IActionResult> EditProjectTest(long idProject)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                Project project = await context.Project
                                        .Include(p => p.IdCompanyNavigation)
                                        .Include(p => p.IdProjectTypeNavigation)
                                        .Include(p => p.IdStudentNavigation).SingleOrDefaultAsync();

                if(project == null)
                    return RedirectToAction("Projects", "BackOffice");

                MemoryStream file = new MemoryStream();
                file.Write(project.Report, 0, project.Report.Length);
                file.Close();

                return View(new ProjectViewModel
                {
                    IdProject = project.IdProject,
                    Title = project.Title,
                    Description = project.Description,
                    Report = (IFormFile)file,
                    IsVisible = Convert.ToBoolean(project.IsVisible),
                    IdProjectType = project.IdProjectType,
                    IdCompany = project.IdCompany,
                    IdStudent = project.IdStudent,
                    Companies = PopulateCompanies(),
                    Students = PopulateStudents(),
                    ProjectTypes = PopulateProjectTypes()
                });
            }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProject(ProjectViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

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
                        Title = model.Title,
                        Description = model.Description,
                        Report = file,
                        ProjectDate = model.ProjectDate,
                        IsVisible = Convert.ToByte(model.IsVisible),
                        IdCompany = model.IdCompany,
                        IdProjectType = model.IdProjectType,
                        IdStudent = model.IdStudent
                    };

                    context.Add(project);
                    await context.SaveChangesAsync();

                    return RedirectToAction("Projects", "BackOffice");
                }

            }

            return PartialView("_CreateProject",
                new ProjectViewModel { Companies = PopulateCompanies(), Students = PopulateStudents(), ProjectTypes = PopulateProjectTypes() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(ProjectViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewData["Got-Error"] = "true";
                ViewData["Login-Message"] = "É necessário iniciar sessão";

                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
                {
                    Project oldProject = await context.Project.SingleOrDefaultAsync(p => p.IdProject == model.IdProject);

                    byte[] file = null;

                    using (var memoryStream = new MemoryStream())
                    {
                        await model.Report.CopyToAsync(memoryStream);
                        file = memoryStream.ToArray();
                    }

                    oldProject.Title = model.Title;
                    oldProject.Description = model.Description;
                    oldProject.Report = file;
                    oldProject.ProjectDate = model.ProjectDate;
                    oldProject.IsVisible = Convert.ToByte(model.IsVisible);
                    oldProject.IdCompany = model.IdCompany;
                    oldProject.IdProjectType = model.IdProjectType;
                    oldProject.IdStudent = model.IdStudent;

                    context.Update(model);
                    await context.SaveChangesAsync();

                    return RedirectToAction("Projects", "BackOffice");
                }
            }

            return View(model);
        }

        public void DeleteProject(long idProject)
        {

            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                Project project = context.Project.SingleOrDefault(p => p.IdProject == idProject);

                context.Project.Remove(project);
                context.SaveChanges();
            }
        }
    }
}