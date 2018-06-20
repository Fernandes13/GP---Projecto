using System;
using System.Collections.Generic;
using System.Linq;
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

            _context.Address.Add(new Address()
            {
                IdAddress = 1,
                IdDistrict = 1,
                Address1 = "Address",
                Door = "23",

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
            //    IdStudent = 1,
            //    BirthDate = new DateTime(),
            //    Email = "email@exemplo.com",
            //    IdAddress
            //});

            _context.SaveChanges();
        }

        public List<int> MarksStats()
        {
            List<int> marks = new List<int>();

            for (int i = 0; i <= 20; i++)
            {
                marks.Add(_context.Project.Where(p => p.Grade == i).Count());
            }

            return marks;
        }
    }
}
