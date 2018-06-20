using System;
using HubEI.Controllers;
using HubEI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XUnit_HubEI
{
    public class StatisticsUnitTests : Controller
    {
        private HUBEI_DBContext _context;
        private StatisticsController _controller;

        public StatisticsUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HUBEI_DBContext>();
            optionsBuilder.UseInMemoryDatabase();
            _context = new HUBEI_DBContext(optionsBuilder.Options);

            _controller = new StatisticsController(_context, null);


        }

        //public IActionResult MarksStats()
        //{
        //    List<int> marks = new List<int>();

        //    for (int i = 0; i <= 20; i++)
        //    {
        //        marks.Add(_context.Project.Where(p => p.Grade == i).Count());
        //    }

        //    return Json(marks);
        //}
    }
}
