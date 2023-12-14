using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoProgra5.DataBaseHelper;
using ProyectoProgra5.Models;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace ProyectoProgra5.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        private readonly DataBaseWebHelper _dbHelper;

        public HomeController()
        {
            _dbHelper = new DataBaseWebHelper();
        }



        public ActionResult Profile(string param)
        {
            var PersonList = GetPerson(param);
            ViewBag.PersonList = PersonList;
            return View();
        }

        public ActionResult Index(List<Person> id)
        {
            ViewBag.Person = id;

            return View();
        }

        public ActionResult AdminView()
        {
            return View();
        }
        public ActionResult GuardView()
        {
            return View();
        }

        public IActionResult GetPersons()
        {
            string storedProcedure = "GetPerson";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Person> people = new List<Person>();

            foreach (DataRow row in result.Rows)
            {
                Person person = new Person
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    LastName = row["LastName"].ToString(),
                    PersonRol = Convert.ToInt32(row["PersonRol"]),
                    PersonUser = row["PersonUser"].ToString(),
                    PersonPassword = row["PersonPassword"].ToString(),
                    Vehicle = Convert.ToInt32(row["Vehicle"])
                };

                people.Add(person);
            }

            return View(people);
        }

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

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
           return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}