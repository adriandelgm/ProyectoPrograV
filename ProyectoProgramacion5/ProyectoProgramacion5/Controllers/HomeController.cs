using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoProgra5.DataBaseHelper;
using ProyectoProgra5.Models;
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

        public ActionResult Validacion(int rol)
        {   
            if(rol == 1)
            {
                AdminView();
            }
            else if(rol == 2)
            {
                Index();
            }   
            else if(rol == 3)
            {
                GuardView();
            }
            return View("Error");
            
        }
        public ActionResult Index()
        {
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