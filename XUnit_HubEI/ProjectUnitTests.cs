using System;
using System.Collections.Generic;
using HubEI.Controllers;
using HubEI.Models;
using HubEI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnit_HubEI
{
    public class ProjectUnitTests : Controller
    {
        private HUBEI_DBContext _context;
        private ProjectController _controller;

        public ProjectUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HUBEI_DBContext>();
            optionsBuilder.UseInMemoryDatabase();
            _context = new HUBEI_DBContext(optionsBuilder.Options);

            _controller = new ProjectController(_context, null);

            _context.Project.Add(new Project()
            {
                IdProject = 1,
                Title = "project1",
                Description = "Margem Sul",
                Report = new byte[100],
                ProjectDate = new DateTime(2018, 01, 01),
                IsVisible = true,
                Views = 10,
                Downloads = 4,
                Grade = 17,
                Video = null,
                IdProjectType = 1,
                IdCompany = 1,
                IdStudent = 1,
                IdBusinessArea = 1
            });

            _context.BusinessArea.Add(new BusinessArea()
            {
                IdBusinessArea = 1,
                Description = "Descrição do BusinessArea"
            });

            _context.ProjectType.Add(new ProjectType()
            {
                IdProjectType = 1,
                Description = "Tipo1"
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

            _context.Address.Add(new Address()
            {
                IdAddress = 1,
                PostalCode = "2475-123",
                Address1 = "rua",
                Door = "2",
                Locality = "Sul",
                IdDistrict = 1,

            });


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

            _context.SaveChanges();

            
        }

        [Fact]
        public void ProjectGetTest()
        {
            var actionResultTask = _controller.Project("1");
            var viewResult = actionResultTask as ViewResult;

            LoginViewModel lvm = (LoginViewModel)viewResult.Model;

            Assert.NotNull(lvm.Project);
        }

        [Fact]
        public void ProjectGetNoTest()
        {
            var actionResultTask = _controller.Project("2");
            var viewResult = actionResultTask as ViewResult;

            LoginViewModel lvm = (LoginViewModel)viewResult.Model;

            Assert.Null(lvm.Project);
        }

    }
}
