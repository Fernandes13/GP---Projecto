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

            _context.District.Add(new District()
            {
                IdDistrict = 1,
                Description = "Margem Sul"
            });

            _context.Company.Add(new Company()
            {
                IdCompany = 1,
                Description = "Esta Empresa",
                Name = "Empresa",
                Email = "empresa@hotmail.com",
                IdDistrict = 1
            });

            //_context.Student.Add(new Student()
            //{
            //    IdAddress
            //});

            _context.SaveChanges();
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
