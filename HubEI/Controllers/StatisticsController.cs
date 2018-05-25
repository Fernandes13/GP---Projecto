﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubEI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HubEI.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StatisticsController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MarksStats()
        {
            List<int> marks = new List<int>();

            for (int i = 0; i <= 20; i++)
            {
                marks.Add(_context.Project.Where(p => p.Grade == i).Count());
            }

            return Json(marks);
        }


        private class DistrictsStat
        {
            public District District { get; set; }
            public int count { get; set; }
        }

        public IActionResult DistrictsStats()
        {
            List<DistrictsStat> stats = new List<DistrictsStat>();

            var districts = _context.District.Distinct().ToList();
            foreach(var district in districts)
            {
                int stat_count = _context.Student.Where(st => st.IdAddressNavigation.IdDistrictNavigation.Equals(district)).Count();
                stats.Add(new DistrictsStat
                {
                    District = district,
                    count = stat_count
                });
            }

            stats = stats.OrderByDescending(st => st.count).ToList().GetRange(0,5);


            return Json(stats);
        }
    }
}