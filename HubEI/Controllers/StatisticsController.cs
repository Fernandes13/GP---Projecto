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
    }
}