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

namespace HubEI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

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
                    ViewData["initial-email"] = strEmail;

                    return View("Login");
                }
                else
                {
                    var principal = new ClaimsPrincipal();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = model.RememberMe });

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
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

                return LoginState.EMAIL_NOTFOUND;

            }
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
            //Console.WriteLine(str);
            md5.ComputeHash(Encoding.ASCII.GetBytes(str));

            return md5.Hash;
        }
    }
}
