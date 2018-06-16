using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubEI.Models;
using HubEI.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HubEI.Controllers
{
    public class CompanyController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CompanyController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Companies")]
        public IActionResult List()
        {

            var companies = _context.Company.Include(c => c.IdDistrictNavigation).ToList();

            var viewModel = new LoginViewModel();

            var list = new List<CompaniesListViewModel>();

            foreach (var c in companies)
            {
                var businessareas = new List<BusinessArea>();

                var projects = _context.Project
                    .Where(p => p.IdCompany == c.IdCompany)
                    .Select(p => new Project
                    {
                        IdProject = p.IdProject,
                        Title = p.Title
                    })
                    .OrderByDescending(p => p.Views)
                    .OrderByDescending(p => p.Downloads)
                    .ToList()
                    .GetRange(0, 3);



                list.Add(new CompaniesListViewModel
                {
                    Company = c,
                    Projects = projects
                });
            }






            viewModel.Companies = list;

            return View(viewModel);
        }
    }
}