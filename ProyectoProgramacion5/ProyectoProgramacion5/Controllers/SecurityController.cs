using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoProgra5.DataBaseHelper;
using ProyectoProgra5.Models;
using System.Data;

namespace ProyectoProgra5.Controllers
{
    public class SecurityController : Controller
    {
        private readonly DataBaseWebHelper _dbHelper;

        public SecurityController()
        {
            _dbHelper = new DataBaseWebHelper();
        }

        // GET: SecurityController
        public ActionResult Security(int projectId)
        {
            var allVisits = GetVisitors(projectId);
            var condo = GetCondo().FirstOrDefault(c => c.CondoID == projectId);

            ViewBag.AllVisits = allVisits;
            ViewBag.ProjectId = projectId;
            ViewBag.CondoName = condo?.CondoName;

            return View();
        }


        public List<Visits> GetVisitors(int projectId)
        {
            string storedProcedure = "GetVisitorsInfo";
            List<MySqlParameter> parameters = new List<MySqlParameter>
    {
        new MySqlParameter("@projectId", projectId)
    };

            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Visits> visitors = new List<Visits>();

            DateTime systemDate = DateTime.Now.Date; // Get the current date without time

            foreach (DataRow row in result.Rows)
            {
                DateTime arrivalTime = Convert.ToDateTime(row["ArrivalTime"]).Date;

                // Check if the arrival time date part is the same as the system's date
                if (arrivalTime == systemDate)
                {
                    Visits visitor = new Visits
                    {
                        VisitorID = Convert.ToInt32(row["VisitorID"]),
                        VisitorName = row["VisitorName"].ToString(),
                        VisitorLastName = row["VisitorLastName"].ToString(),
                        VehicleBrand = row["VehicleBrand"].ToString(),
                        VehiclePlate = row["VehiclePlate"].ToString(),
                        VehicleColor = row["VehicleColor"].ToString(),
                        ArrivalTime = Convert.ToDateTime(row["ArrivalTime"]),
                        PersonName = row["PersonName"].ToString(),
                        PersonLastName = row["PersonLastName"].ToString()
                    };

                    visitors.Add(visitor);
                }
            }

            return visitors;
        }






        // GET: SecurityController/Details/5
        public ActionResult Condos()
        {
            var condos = GetCondo();

            ViewBag.CondoList = condos;
            return View();
        }

        [HttpGet]
        public List<Condo> GetCondo()
        {
            string storedProcedure = "getcondo"; // Nombre de tu procedimiento almacenado.
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Condo> condo = new List<Condo>();

            foreach (DataRow row in result.Rows)
            {
                Condo condos = new Condo
                {
                    CondoID = Convert.ToInt32(row["CondoID"]),
                    CondoName = row["CondoName"].ToString(),
                    Location = Convert.ToInt32(row["Location"]),
                    Logo = row["Logo"].ToString()
                };

                condo.Add(condos);
            }
            return condo;
        }

        public List<Visits> SearchVisitors(string searchValue)
        {
            string storedProcedure = "SearchVisits";
            List<MySqlParameter> parameters = new List<MySqlParameter>
        {
            new MySqlParameter("@searchValue", searchValue)
        };

            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Visits> visitors = new List<Visits>();

            foreach (DataRow row in result.Rows)
            {
                Visits visitor = new Visits
                {
                    VisitorID = Convert.ToInt32(row["VisitorID"]),
                    VisitorName = row["VisitorName"].ToString(),
                    VisitorLastName = row["VisitorLastName"].ToString(),
                    VehicleBrand = row["VehicleBrand"].ToString(),
                    VehiclePlate = row["VehiclePlate"].ToString(),
                    VehicleColor = row["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(row["ArrivalTime"]),
                    PersonName = row["PersonName"].ToString(),
                    PersonLastName = row["PersonLastName"].ToString()
                };

                visitors.Add(visitor);
            }

            return visitors;
        }

        public ActionResult SearchVisits(int projectId, string searchValue)
        {
            var searchResults = SearchVisitors(searchValue);
            var allVisits = GetVisitors(projectId);

            var condo = GetCondo().FirstOrDefault(c => c.CondoID == projectId);

            ViewBag.SearchResults = searchResults;
            ViewBag.AllVisits = allVisits;
            ViewBag.ProjectId = projectId;
            ViewBag.CondoName = condo?.CondoName;

            return View("Security");
        }


    }
}