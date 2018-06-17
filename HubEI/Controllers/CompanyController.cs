﻿using System;
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
                var maxChars = 460;
                c.Description = c.Description.Length <= maxChars ? c.Description : c.Description.Substring(0, maxChars) + "...";

                var businessareas = new List<BusinessArea>();

                var projects_count = _context.Project.Where(p => p.IdCompany == c.IdCompany).Count();

                var projects = new List<Project>();

                if (projects_count >= 3)
                {
                    projects = _context.Project
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

                }
                else
                {
                     projects = _context.Project
                    .Where(p => p.IdCompany == c.IdCompany)
                    .Select(p => new Project
                    {
                        IdProject = p.IdProject,
                        Title = p.Title
                    })
                    .OrderByDescending(p => p.Views)
                    .OrderByDescending(p => p.Downloads)
                    .ToList()
                    .GetRange(0, projects_count);
                }

                var businessAreas = new HashSet<BusinessArea>();

                foreach(Project p in _context.Project.Where(p => p.IdCompany == c.IdCompany).Include(p=>p.IdBusinessAreaNavigation))
                {
                    businessAreas.Add(p.IdBusinessAreaNavigation);

                }

                list.Add(new CompaniesListViewModel
                {
                    Company = c,
                    Projects = projects,
                    BusinessAreas = businessAreas.ToList()                    
                });
            }

            viewModel.Companies = list;

            return View(viewModel);
        }
    }
}