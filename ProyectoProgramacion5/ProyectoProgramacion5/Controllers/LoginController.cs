using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using ProyectoProgra5.Models;
using ProyectoProgra5.DataBaseHelper;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using Firebase.Auth;

namespace ProyectoProgra5.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataBaseWebHelper _dbHelper;

        public LoginController()
        {
            _dbHelper = new DataBaseWebHelper();
        }

        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        public class LoginViewModel
        {
            public string txtUser { get; set; }
            public string txtPassword { get; set; }
            // Other properties as needed
        }

        [HttpPost]
        public IActionResult LoginF(LoginViewModel model)
        {
            string personUser = model.txtUser;
            List<Person> persons = GetPerson(personUser);

            if (persons.Count > 0)
            {
                int personId = persons[0].Id;

                // Set the personId in ViewBag
                ViewBag.PersonId = personId;

                return RedirectToAction("Index", "Home", new { personId = personId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View();
            }
        }


        /*
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
                Person.Add(user);

                if (user.PersonRol == 1)
                {
                    ViewBag.Person = user.Id;
                    return RedirectToAction("Root", "Root");
                }
                else if(user.PersonRol == 2)
                {
                    ViewBag.Person = user.Id;
                    HomeController home = new HomeController();
                    home.Index(Person);
                    return RedirectToAction("Index", "Home");
                }
                else if(user.PersonRol == 3)
                {
                    ViewBag.Person = user.Id;
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

        }*/

        [HttpGet]
        public List<Person> GetPerson(string personUser)
        {
            string storedProcedure = "GetPersonInfo";

            // Create a list to store parameters
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            // Add the PersonUser parameter
            parameters.Add(new MySqlParameter("@PersonUser", personUser));

            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Person> persons = new List<Person>();

            foreach (DataRow row in result.Rows)
            {
                Person person = new Person
                {
                    Id = row["PersonID"] != DBNull.Value ? Convert.ToInt32(row["PersonID"]) : 0,
                    Name = row["PersonName"].ToString(),
                    LastName = row["PersonLastName"].ToString(),
                    Rol = row["RolName"].ToString(),
                    PersonUser = row["PersonUser"].ToString(),
                    Email = row["ContactEmail"].ToString(),
                    Phone = row["ContactPhone"].ToString(),
                    Condo = row["CondoName"].ToString(),
                    House = row["HouseID"] != DBNull.Value ? Convert.ToInt32(row["HouseID"]) : 0
                };

                persons.Add(person);
            }

            return persons;
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
