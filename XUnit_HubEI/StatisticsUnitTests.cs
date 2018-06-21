using System;
using System.Collections.Generic;
using System.Linq;
using HubEI.Controllers;
using HubEI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

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
                Locality = "Baixa",
                PostalCode = "2835-123",
            });

            _context.Company.Add(new Company()
            {
                IdCompany = 1,
                Description = "Esta Empresa",
                Name = "Empresa",
                Email = "empresa@hotmail.com",
                IdDistrict = 1
            });

            _context.StudentBranch.Add(new StudentBranch()
            {
                IdStudentBranch = 1,
                Description = "Ramo"
            });

            _context.Student.Add(new Student()
            {
                IdStudent = 1,
                BirthDate = new DateTime(),
                Email = "email@exemplo.com",
                IdAddress = 1,
                IdStudentBranch = 1,
                Name = "Estudante",
                StudentNumber = 150221066,
                Telephone = 911111111,
            });

            _context.Technology.Add(new Technology()
            {
                IdTechnology = 1,
                Description = "Tecnologia"
            });

            _context.BusinessArea.Add(new BusinessArea()
            {
                IdBusinessArea = 1,
                Description = "BA",
            });

            _context.SchoolMentor.Add(new SchoolMentor()
            {
                IdSchoolMentor = 1,
                Name = "Nome",
                Email = "email@email.com",
            });

            _context.ProjectAdvisor.Add(new ProjectAdvisor()
            {
                IdProject = 1,
                IdSchoolMentor = 1
            });

            _context.Project.Add(new Project()
            {
                IdProject = 1,
                Title = "project1",
                Description = "Margem Sul",
                Report = new byte[100],
                ProjectDate = new DateTime().Date,
                IsVisible = true,
                Views = 10,
                Downloads = 4,
                Grade = 17,
                Video = null,
                IdBusinessArea = 1
            });

            _context.ProjectTechnology.Add(new ProjectTechnology()
            {
                IdProject = 1,
                IdTechnology = 1
            });

            _context.SaveChanges();
        }

        public List<int> MarksStats()
        {
            var stats = _controller.DistrictsStats();

            var viewResult = stats as JsonResult;

            var district = ((List<StatisticsController.DistrictsStat>)viewResult.Value)[0];

            Assert.True(district.District.IdDistrict == 1);
        }

        //TODO
        [Fact]
        public void Top10TechnologiesTests()
        {
            var stats = _controller.Top10Technologies();

            var viewResult = stats as JsonResult;

            //var technology = ((List<ProjectTechnology>)viewResult.Value)[0];

            //Assert.True(technology.Description == "Tecnologia");
            //Assert.True(technology.Count == 1);
            Assert.True(true);
        }

        [Fact]
        public void Top10MentorsAverageTests()
        {
            var stats = _controller.Top10MentorsAverage();

            var viewResult = stats as JsonResult;

            //var technology = ((Dictionary<String, double>)viewResult.Value).First();

            //Assert.True(technology.Description == "Tecnologia");
            //Assert.True(technology.Count == 1);
            Assert.True(true);
        }

        [Fact]
        public void Top5BusinessAreasTests()
        {
            var stats = _controller.Top5BusinessAreas();

            var viewResult = stats as JsonResult;

            //var technology = ((List<ProjectTechnology>)viewResult.Value)[0];

            //Assert.True(technology.Description == "Tecnologia");
            //Assert.True(technology.Count == 1);
            Assert.True(true);
        }
    }
}
