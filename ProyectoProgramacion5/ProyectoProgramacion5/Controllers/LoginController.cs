using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using ProyectoProgra5.Models;
using ProyectoProgra5.DataBaseHelper;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;

namespace ProyectoProgra5.Controllers
{
    public class LoginController : Controller
    {
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
      

        public ActionResult LoginF(string txtUser, string txtPassword)
        {
            List<Person> Person = new List<Person>();
            string storedProcedure = "Login";
            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("p_User",txtUser),
                new MySqlParameter("p_pass",txtPassword),
            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
           DataTable ds =  helper.Fill(storedProcedure, param);


            if (ds.Rows.Count > 0)
            {
                Person user = new Person
                {
                    Id = Convert.ToInt32(ds.Rows[0]["PersonID"].ToString()),
                    Name = ds.Rows[0]["PersonName"].ToString(),
                    LastName = ds.Rows[0]["PersonLastName"].ToString(),
                    PersonRol = Convert.ToInt32(ds.Rows[0]["PersonRol"].ToString()),
                    PersonUser = txtUser,
                    PersonPassword = txtPassword

                };


                if (user.PersonRol == 1)
                {
                    ViewBag.PersonRol = user.Id;
                    return RedirectToAction("Root", "Root");
                }
                else if(user.PersonRol == 2)
                {
                    ViewBag.PersonRol = user.Id;
            
                    return RedirectToAction("Index", "Home");
                }
                else if(user.PersonRol == 3)
                {
                    ViewBag.PersonRol = user.Id;
                    return RedirectToAction("Condos","Security");
                }
                else
                {
                    return View(ErrorEventArgs.Empty);
                }
            }
            else
            {
                return null;
            }

        }

        public ActionResult SignUP()
        {
            return View();
        }
        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
