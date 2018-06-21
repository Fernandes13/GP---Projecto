using System;
using HubEI.Controllers;
using HubEI.Models;
using HubEI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace XUnit_HubEI
{
    public class StudentUnitTests : Controller
    {
        private HUBEI_DBContext _context;
        private BackOfficeController _controller;

        public StudentUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HUBEI_DBContext>();
            optionsBuilder.UseInMemoryDatabase();
            _context = new HUBEI_DBContext(optionsBuilder.Options);

            _controller = new BackOfficeController(_context, null);

            _context.StudentBranch.Add(new StudentBranch()
            {
                IdStudentBranch = 1,
                Description = "Ramo"
            });

            _context.District.Add(new District()
            {
                IdDistrict = 1,
                Description = "Margem Sul"
            });

            _context.Address.Add(new Address()
            {
                IdAddress = 1,
                PostalCode = "2475-123",
                Address1 = "rua",
                Door = "2",
                Locality = "Sul",
                IdDistrict = 1,

            });

            _context.Student.Add(new Student()
            {
                IdStudent = 1,
                BirthDate = new DateTime(1999, 01, 01),
                Email = "email@exemplo.com",
                IdAddress = 1,
                IdStudentBranch = 1,
                Name = "Estudante",
                StudentNumber = 150221066,
                Telephone = 911111111,
            });

            _context.SaveChanges();
        }

        [Fact]
        public void GetStudentTest()
        {
            var actual = _controller.GetStudent("1");
            Student result = (Student)actual.Value;

            Assert.Equal(150221066, result.StudentNumber);
        }

        [Fact]
        public void GetStudentNoTest()
        {
            Student studentNotFound = null;
            var actual = _controller.GetStudent("2");
            Student result = (Student)actual.Value;

            Assert.Equal(studentNotFound, result);
        }
    }
}
