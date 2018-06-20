using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HubEI.Models;
using HubEI.Models.ViewModels;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HubEI.Controllers
{
    /// <summary>
    /// Controller used for the actions affecting the website in general. 
    /// Has methods to access the homepage, login and logout, and actions to manage the website's cookies.
    /// </summary>
    /// <remarks></remarks>
    public class HomeController : Controller
    {
        private readonly HUBEI_DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubEI.Controllers.HomeController" /> class. 
        /// </summary>
        /// <param name="context">Database Context</param>
        /// <param name="HostingEnvironment">Hosting Environment</param>
        /// <remarks></remarks>
        public HomeController(HUBEI_DBContext context, IHostingEnvironment HostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = HostingEnvironment;
        }


        /// <summary>
        /// This method shows the website's homepage.
        /// It manage the privacy politics cookie, so that it only shows the message the first time a user enters the website.
        /// </summary>
        /// <returns>View with HomePage</returns>
        /// <remarks></remarks>
        public IActionResult Index()
        {
            var rgpdInfo = _context.RgpdInfo.FirstOrDefault();

            ViewBag.rgpdCookie = Request.Cookies["rgpd"];

            ViewData["RgpdInfo"] = rgpdInfo.Description;

            var topProjects = _context.Project.Where(p => p.IsVisible)
                .OrderByDescending(p => p.Downloads)
                .OrderByDescending(p => p.Views)
                .Select(p => new Project
                {
                    Description = p.Description,
                    Title = p.Title,
                    IdProject = p.IdProject
                })
                .ToList().GetRange(0, 3);


            return View(new LoginViewModel
            {
                Projects = topProjects
            });
        }

        /// <summary>
        /// This method registers the privacy politics cookie, so that it only shows the message the first time a user enters the website.
        /// The cookie expires after a year.
        /// </summary>
        /// <returns>View with HomePage</returns>
        /// <remarks></remarks>
        public IActionResult SaveRPGCookie()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(365);
            Response.Cookies.Append("rgpd", "true", options);

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// This method fills the privacy politics message.
        /// </summary>
        /// <returns>Partial View with Privacy Politics message</returns>
        /// <remarks></remarks>
        public IActionResult Privacy()
        {
            var rgpdInfo = _context.RgpdInfo.FirstOrDefault();

            ViewData["RgpdInfo"] = rgpdInfo.Description;

            return View("Privacy");
        }


        /// <summary>
        /// This method executes a login in the website
        /// </summary>
        /// <param name="model">LoginViewModel containing username and password.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string strEmail = model.Email;
            string strPassword = model.Password;

            if (ModelState.IsValid)
            {
                LoginState state = IsRegistered(strEmail, strPassword);

                if (state == LoginState.EMAIL_NOTFOUND || state == LoginState.CONNECTION_FAILED || state == LoginState.WRONG_PASSWORD) //Email não encontrado, ou password inválida
                {
                    ViewData["Login-Message"] = state.GetMessage();
                    ViewData["Got-Error"] = "true";
                }
                else
                {
                    var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name, strEmail),
                        new Claim(ClaimTypes.Role, "Mentor"),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(claimsIdentity);


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "BackOffice");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Executs a logout, redirecting the user to the homepage.
        /// </summary>
        /// <returns>View Home/Index</returns>
        /// <remarks></remarks>
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Regista um novo admin na aplicação.
        /// </summary>
        /// <param name="model">Modelo</param>
        /// <returns>Retorna a View com uma mensagem de erro ou sucesso na operação.</returns>
        /// <remarks></remarks>
        [HttpPost]
        public IActionResult RegisterAdmin(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            string strEmail = model.Email;
            string strPassword = model.Password;

            Admin admin = new Admin { Email = strEmail, Password = StrToArrByte(strPassword) };

            InsertAdmin(admin);

            return RedirectToAction("Index", "Home");
        }

        public string AccountID(string strEmail)
        {
            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                var accoundId = from a in context.Admin where a.Email == strEmail select a;

                return accoundId.FirstOrDefault().IdAdmin.ToString();
            }
        }

        public string AccountName(string _strAccountID)
        {
            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                var name = "";

                name = (from s in context.Admin where s.IdAdmin == Int32.Parse(_strAccountID) select s.Email).FirstOrDefault().ToString();

                return name;
            }
        }

        public LoginState IsRegistered(string _strEmail, string _strPassword)
        {
            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                var accountWEmail = from a in context.Admin where a.Email == _strEmail select a;
                var account = accountWEmail.FirstOrDefault();

                if (account != null)
                {
                    var strAccountID = account.IdAdmin.ToString();

                    string strBDPW = ToHex(account.Password, false);

                    if (!strBDPW.Equals(EncryptToMD5(_strPassword)))
                        return LoginState.WRONG_PASSWORD;

                }
                else
                {
                    return LoginState.EMAIL_NOTFOUND;
                }
            }

            return LoginState.CONNECTED;

        }

        /// <summary>
        /// Converte um vetor de bytes numa string em formato Hexadecimal
        /// </summary>
        /// <param name="bytes">N</param>
        /// <param name="upperCase">Se for <see langword="true" />, então distingue os caracteres maiúsculos ; caso contrário não distingue os caracteres maiúsculoss.</param>
        /// <returns>String resultante</returns>
        /// <remarks></remarks>
        public string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                stringBuilder.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Encripta uma string para MD5
        /// </summary>
        /// <param name="strPassword">String a encriptar</param>
        /// <returns>String encriptada</returns>
        /// <remarks></remarks>
        public string EncryptToMD5(string strPassword)
        {
            byte[] result = StrToArrByte(strPassword);

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        /// <summary>
        /// Converte uma string num vetor de bytes
        /// </summary>
        /// <param name="str">String a converter</param>
        /// <returns>Vetor de bytes resultante</returns>
        /// <remarks></remarks>
        public byte[] StrToArrByte(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(Encoding.ASCII.GetBytes(str));

            return md5.Hash;
        }


        private async void InsertAdmin(Admin admin)
        {
            using (var context = new HUBEI_DBContext(new DbContextOptions<HUBEI_DBContext>()))
            {
                context.Add(admin);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Returns a list of projects filtered by the argument q, which compares the project title or description.
        /// </summary>
        /// <param name="q">Search criteria</param>
        /// <returns>JSON object with list of projects</returns>
        /// <remarks></remarks>
        public IActionResult SearchSuggestions([FromQuery] string q)
        {
            var projects = _context.Project.Where(p => p.Title.Contains(q))
                .Include(p => p.IdStudentNavigation)
                .Include(p => p.IdProjectTypeNavigation)
                .Select(p => new
                {
                    p.IdProject,
                    p.Title,
                    StudentName = p.IdStudentNavigation.Name,
                    Type = p.IdProjectTypeNavigation.Description,
                    Date = p.ProjectDate
                }).ToList();


            return Json(projects.Take(4));
        }
    }
}
