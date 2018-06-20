using System;
using HubEI.Controllers;
using HubEI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace XUnit_HubEI
{
    public class StudentUnitTests : Controller
    {
        private HUBEI_DBContext _context;
        private BackOfficeController _backOffice;

        public StudentUnitTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HUBEI_DBContext>();
            optionsBuilder.UseInMemoryDatabase();
            _context = new HUBEI_DBContext(optionsBuilder.Options);
        }
    }
}
