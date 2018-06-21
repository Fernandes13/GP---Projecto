using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HubEI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HubEI.Controllers
{
    /// <summary>
    /// Controller used for the actions affecting Statistics envolving the submitted projects.
    /// </summary>
    /// <remarks></remarks>
    public class StatisticsController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubEI.Controllers.StatisticsController" /> class. 
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="HostingEnvironment">Hosting Environment</param>
        /// <remarks></remarks>
        public StatisticsController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment;
        }


        /// <summary>
        /// Returns the statistics page
        /// </summary>
        /// <returns>View with statistics page</returns>
        /// <remarks></remarks>
        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Method that constructs the grades bounds in a list (0,1,2,3,4,5,6,7,...,20)
        /// </summary>
        /// <returns>List with values</returns>
        /// <remarks></remarks>
        public IActionResult MarksStats()
        {
            List<int> marks = new List<int>();

            for (int i = 0; i <= 20; i++)
            {
                marks.Add(_context.Project.Where(p => p.Grade == i).Count());
            }

            return Json(marks);
        }


        /// <summary>
        /// Class to help in the top 5 districts in which students live the most.
        /// It as a Model District and a count of students.
        /// </summary>
        /// <remarks></remarks>
        public class DistrictsStat
        {
            public District District { get; set; }
            public int count { get; set; }
        }

        /// <summary>
        ///   Statistic that returns the top 5 districts in which students live the most.
        /// </summary>
        /// <returns>Top 5 districts in which students live the most</returns>
        /// <remarks></remarks>
        public IActionResult DistrictsStats()
        {
            List<DistrictsStat> stats = new List<DistrictsStat>();

            var districts = _context.District.Distinct().ToList();
            foreach (var district in districts)
            {
                int stat_count = _context.Student.Where(st => st.IdAddressNavigation.IdDistrictNavigation.Equals(district)).Count();
                stats.Add(new DistrictsStat
                {
                    District = district,
                    count = stat_count
                });
            }

            stats = stats.OrderByDescending(st => st.count).ToList();

            if(stats.Count() >= 5)
            {
                stats = stats.GetRange(0, 5);
            }

            return Json(stats);
        }

        /// <summary>
        /// Statistic that returns the top 10 most used technologies.
        /// </summary>
        /// <returns>Top 10 most used technologies.</returns>
        /// <remarks></remarks>
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

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            query.ForEach(q =>
            {
                dictionary.Add(q.Description, q.Count);
            });

            return Json(dictionary);
        }

        /// <summary>
        /// Statistic that returns the top 10 mentors with best average evaluations
        /// </summary>
        /// <returns>Top 10 mentors with best average evaluations</returns>
        /// <remarks></remarks>
        [HttpGet]
        public JsonResult Top10MentorsAverage()
        {
            var query = from a in _context.ProjectAdvisor
                        group a by a.IdSchoolMentorNavigation.Name into mentorGroup
                        select new { name = mentorGroup.Key, Average = mentorGroup.Average(x => x.IdProjectNavigation.Grade)};

            query = query.OrderByDescending(q => q.Average).Take(10);

            var list = query.ToList();

            Dictionary<string, double> dictionary = new Dictionary<string, double>();

            list.ForEach(q =>
            {
                dictionary.Add(q.name, q.Average);
            });

            return Json(dictionary);
        }

        /// <summary>
        /// Statistic that returns the top 5 business areas
        /// </summary>
        /// <returns>Top 5 business areas</returns>
        /// <remarks></remarks>
        [HttpGet]
        public JsonResult Top5BusinessAreas()
        {
            var query = from a in _context.Project
                        group a by a.IdBusinessAreaNavigation.Description into baGroup
                        select new { name = baGroup.Key, Count = baGroup.Count() };

            query = query.Take(5);

            var list = query.ToList();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            list.ForEach(q =>
            {
                dictionary.Add(q.name, q.Count);
            });

            return Json(dictionary);
        }
    }
}