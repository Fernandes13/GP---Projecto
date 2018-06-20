using System;
using HubEI.Controllers;
using HubEI.Models;
using HubEI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnit_HubEI
{
    public class CompanyUnitTests : Controller
    {
        private HUBEI_DBContext _context;
        private CompanyController _controller;

        public CompanyUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HUBEI_DBContext>();
            optionsBuilder.UseInMemoryDatabase();
            _context = new HUBEI_DBContext(optionsBuilder.Options);

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

            _controller = new CompanyController(_context, null);
        }

        [Fact]
        public void CompanyGetCompanyTest()
        {
            var actionResultTask = _controller.Index("1");
            //var viewResult = actionResultTask as System.Web.Mvc.ViewResult;
            var viewResult = actionResultTask as ViewResult;

            LoginViewModel lvm = (LoginViewModel)viewResult.Model;

            Assert.NotNull(lvm.CompanyViewModel.Company);
        }

        [Fact]
        public void CompanyGetNoCompanyTest()
        {
            var actionResultTask = _controller.Index("2");
            //var viewResult = actionResultTask as System.Web.Mvc.ViewResult;
            var viewResult = actionResultTask as ViewResult;

            LoginViewModel lvm = (LoginViewModel)viewResult.Model;

            Assert.Null(lvm.CompanyViewModel.Company);
        }
    }
}
