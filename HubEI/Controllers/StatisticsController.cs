using System;
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

        [HttpGet]
        public JsonResult Top10Technologies()
        {
            var query = _context.ProjectTechnology
                        .GroupBy(pt => new {
                            pt.IdTechnology,
                            pt.IdTechnologyNavigation.Description
                        })
                        .Select(pt => new {
                            pt.Key.Description,
                            Count = pt.Count()
                        }).ToList();

            query = query.OrderByDescending(q => q.Count).Take(10).ToList();

            return Json(query);
        }

        [HttpGet]
        public JsonResult Top10MentorsAverage()
        {
            //var query = _context.ProjectAdvisor
            //   .GroupBy(pa =>  pa.IdSchoolMentorNavigation.Name, pa => pa.IdProjectNavigation.Grade)
            //   .Select(pa => new
            //   {
            //       Name = pa.Key,
            //       Average = pa.Average()
            //   });

            var query = from a in _context.ProjectAdvisor
                        group a by a.IdSchoolMentorNavigation.Name into mentorGroup
                        select new { name = mentorGroup.Key, Average = mentorGroup.Average(x => x.IdProjectNavigation.Grade)};

            query = query.OrderByDescending(q => q.Average).Take(10);

            return Json(query);
        }
    }
}