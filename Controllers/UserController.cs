using Backend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

namespace Backend.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            UserRepository userEntity = new UserRepository();
            UserModel userSession = userEntity.validateLogin(user);

            if (userSession == null)
            {
                ViewBag.Mensagem = "Falha no login";
            }
            else
            {
                ViewBag.Mensagem = "Login realizado com êxito";
                HttpContext.Session.SetInt32("id", userSession.id);
                HttpContext.Session.SetString("NomeUser", userSession.name);
            }
            return Json(new { mensagem = ViewBag.Mensagem });
        }


        public IActionResult Logout(UserModel user)
        {
            HttpContext.Session.Clear();
            return View("Login");
        }


        public IActionResult Listing()
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            UserRepository userEntity = new UserRepository();
            List<UserModel> list = userEntity.listAllUsers();

            return Json(list);
        }


        public IActionResult ShopWindow()
        {
            UserRepository userEntity = new UserRepository();
            List<UserModel> list = userEntity.listAllUsers();

            return View(list);
        }

        [HttpPost]

        public IActionResult Register(UserModel user)
        {
            UserRepository userEntity = new UserRepository();
            var password = CalculateMD5Hash(user.password);
            user.password = password;
            userEntity.insertNewUser(user);
            return Json(new { mensagem = "Cadastro realizado com sucesso" });
        }

        public string CalculateMD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
    }
        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Remove(int id)
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            UserRepository userEntity = new UserRepository();
            UserModel user = userEntity.searchUserFromId(id);
            userEntity.deleteUser(user);
            return RedirectToAction("Listing");
        }


        public IActionResult Edit(int id)
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            UserRepository userEntity = new UserRepository();
            UserModel user = userEntity.searchUserFromId(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            // if (HttpContext.Session.GetInt32("id") == null)
            // {
            //     return RedirectToAction("Login");
            // }

            UserRepository userEntity = new UserRepository();
            userEntity.updateDataUser(user);
            return RedirectToAction("Listing");
        }
    }
}
