using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoProgra5.DataBaseHelper;
using ProyectoProgra5.Models;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace ProyectoProgra5.Controllers
{
    public class FunctionsController : Controller
    {

        // GET: FunctionsController
        public ActionResult AddCar()
        {
            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Cars> Car = new List<Cars>();


            DataTable ds = db.Fill("GetCarsBrand", null);

            foreach (DataRow dr in ds.Rows)
            {
                Car.Add(new Cars
                {
                    Id = Convert.ToInt16(dr["BrandID"].ToString()),
                    Brand = dr["BrandName"].ToString()

                });
            }

            ViewBag.Cars = Car;
            return View();
        }

        public ActionResult UpdateVisit(int id)
        {
            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Visits> Visit = new List<Visits>();

            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("pID", id),
            };
            DataTable ds = db.Fill("Get_Visit", param);

            foreach (DataRow dr in ds.Rows)
            {
                Visit.Add(new Visits
                {
                    VisitorID = Convert.ToInt32(dr["VisitorID"].ToString()),
                    VisitorName = dr["VisitorName"].ToString(),
                    VisitorLastName = dr["VisitorLastName"].ToString(),
                    VehicleBrand = dr["VehicleBrand"].ToString(),
                    VehiclePlate = dr["VehiclePlate"].ToString(),
                    VehicleColor = dr["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString()),
                    Person = Convert.ToInt16(dr["Person"].ToString())
                });
            }

            ViewBag.Visit = Visit;

            return View();

        }
        public ActionResult AddDelivery()
        {
            return View();
        }
        public ActionResult UpdateDelivery(int id)
        {
            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Delivery> Delivery = new List<Delivery>();

            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("pID", id),
            };
            DataTable ds = db.Fill("GetDelivery", param);

            foreach (DataRow dr in ds.Rows)
            {
                Delivery.Add(new Delivery
                {
                    DeliveryID = Convert.ToInt16(dr["DeliveryID"].ToString()),
                    DeliveryName = dr["DeliveryName"].ToString(),
                    Person = Convert.ToInt16(dr["Person"].ToString())
       
         
                });
            }

            ViewBag.Delivery = Delivery;
            return View();
        }
        public ActionResult GetDelivery()
        {
            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Delivery> Delivery = new List<Delivery>();

            
            DataTable ds = db.Fill("GetDeliverys", null);

            foreach (DataRow dr in ds.Rows)
            {
                Delivery.Add(new Delivery
                {
                    DeliveryID = Convert.ToInt16(dr["DeliveryID"].ToString()),
                    DeliveryName = dr["DeliveryName"].ToString(),
                    Person = Convert.ToInt16(dr["Person"].ToString())


                });
            }

            ViewBag.Delivery = Delivery;
            return View();
        }
        public ActionResult AddVisit(int PersonID)
        {
            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Cars> Car = new List<Cars>();


            DataTable ds = db.Fill("GetCarsBrand", null);

            foreach (DataRow dr in ds.Rows)
            {
                Car.Add(new Cars
                {
                    Id = Convert.ToInt16(dr["BrandID"].ToString()),
                    Brand = dr["BrandName"].ToString()

                });
            }

            ViewBag.Cars = Car;
            ViewBag.Person = PersonID;
            return View();

        }

        // GET: FunctionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

        public ActionResult GetVisit(int id)
        {
            
            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Visits> Visit = new List<Visits>();
           
            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("pID", 1),
            }; 
            DataTable ds = db.Fill("Get_Visit", param);

            foreach (DataRow dr in ds.Rows)
            {
                Visit.Add(new Visits
                {
                    VisitorID = Convert.ToInt32(dr["VisitorID"].ToString()),
                    VisitorName = dr["VisitorName"].ToString(),
                    VisitorLastName = dr["VisitorLastName"].ToString(),
                    VehicleBrand  =  dr["VehicleBrand"].ToString(),
                    VehiclePlate = dr["VehiclePlate"].ToString(),
                    VehicleColor = dr["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString()),
                    Person = Convert.ToInt16(dr["Person"].ToString())
                }); 
            }

            ViewBag.Visit = Visit;

            return View();
        }
        public ActionResult GetMyVisits()
        {

            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Visits> Visit = new List<Visits>();
            List<Visits> Visit1 = new List<Visits>();
            List<Visits> Visit2 = new List<Visits>();
            DataTable ds = db.Fill("GetVisits", null);
            DataTable ds1 = db.Fill("GetVisitsFav", null);
            DataTable ds2 = db.Fill("GetVisitsNormal", null);


            foreach (DataRow dr in ds.Rows)
            {
                Visit.Add(new Visits
                {
                    VisitorID = Convert.ToInt32(dr["VisitorID"].ToString()),
                    VisitorName = dr["VisitorName"].ToString(),
                    VisitorLastName = dr["VisitorLastName"].ToString(),
                    VehicleBrand = dr["BrandName"].ToString(),
                    VehiclePlate = dr["VehiclePlate"].ToString(),
                    VehicleColor = dr["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString()),
                    Person = Convert.ToInt16(dr["PersonID"].ToString())
                });
            }
            foreach (DataRow dr in ds1.Rows)
            {
                Visit1.Add(new Visits
                {
                    VisitorID = Convert.ToInt32(dr["VisitorID"].ToString()),
                    VisitorName = dr["VisitorName"].ToString(),
                    VisitorLastName = dr["VisitorLastName"].ToString(),
                    VehicleBrand = dr["VehicleBrand"].ToString(),
                    VehiclePlate = dr["VehiclePlate"].ToString(),
                    VehicleColor = dr["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString()),
                    Person = Convert.ToInt16(dr["PersonID"].ToString())
                });
            }
            foreach (DataRow dr in ds2.Rows)
            {
                Visit2.Add(new Visits
                {
                    VisitorID = Convert.ToInt32(dr["VisitorID"].ToString()),
                    VisitorName = dr["VisitorName"].ToString(),
                    VisitorLastName = dr["VisitorLastName"].ToString(),
                    VehicleBrand = dr["VehicleBrand"].ToString(),
                    VehiclePlate = dr["VehiclePlate"].ToString(),
                    VehicleColor = dr["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString()),
                    Person = Convert.ToInt16(dr["PersonID"].ToString())
                });
            }

            ViewBag.Visit = Visit;
            ViewBag.VisitFav = Visit1;
            ViewBag.VisitNormal = Visit2;
            return View();
        }
        public ActionResult GetVisitsFav()
        {

            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Visits> Visit = new List<Visits>();
            DataTable ds = db.Fill("GetVisitsFav", null);


            foreach (DataRow dr in ds.Rows)
            {
                Visit.Add(new Visits
                {
                    VisitorID = Convert.ToInt32(dr["VisitorID"].ToString()),
                    VisitorName = dr["VisitorName"].ToString(),
                    VisitorLastName = dr["VisitorLastName"].ToString(),
                    VehicleBrand = dr["VehicleBrand"].ToString(),
                    VehiclePlate = dr["VehiclePlate"].ToString(),
                    VehicleColor = dr["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString())
                    //Person = Convert.ToInt16(dr["Person"].ToString())
                });
            }
            return View(Visit);
        }

        public ActionResult GetVisitsNormal()
        {

            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Visits> Visit = new List<Visits>();
            DataTable ds = db.Fill("GetVisitsNormal", null);


            foreach (DataRow dr in ds.Rows)
            {
                Visit.Add(new Visits
                {
                    VisitorID = Convert.ToInt32(dr["VisitorID"].ToString()),
                    VisitorName = dr["VisitorName"].ToString(),
                    VisitorLastName = dr["VisitorLastName"].ToString(),
                    VehicleBrand = dr["VehicleBrand"].ToString(),
                    VehiclePlate = dr["VehiclePlate"].ToString(),
                    VehicleColor = dr["VehicleColor"].ToString(),
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString())
                    //Person = Convert.ToInt16(dr["Person"].ToString())
                });
            }
            return View(Visit);
        }

        public ActionResult DeleteVisit(int Id)
        {
            string storedProcedure = "DeleteVisit";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("p_VisitID", Id)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return RedirectToAction("GetMyVisits", "Functions");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting condo: {ex.Message}");
            }
        }

        public ActionResult DeleteFavVisit(int ID)
        {
            string storedProcedure = "DeleteFav";
            List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("p_VisitID", ID),

            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, paramList);


            return RedirectToAction("GetMyVisits","Functions");
        }
        public ActionResult DeleteNormal(int ID)
        {
            string storedProcedure = "normalvisis";
            List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("p_VisitID", ID),

            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, paramList);


            return View("GetMyVisits");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delivery(string DeliveryName, int Person) 
        {
            string storedProcedure = "InsertDelivery";
            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("p_DeliveryName",DeliveryName),
                new MySqlParameter("p_Person",1),
            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, param); 
            return Ok();
        }

        // POST: FunctionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int VisitorID, string VisitName, string email, string VisitLastName, string VehicleBrand, string VehicleColor, int VehiclePlate, DateTime ArrivalTime, int Person)
        {
           


                string storedProcedure = "Create_Visit";

                List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("_VisitorID", VisitorID),
                new MySqlParameter("_VisitorName", VisitName),
                new MySqlParameter("_VisitorLastName",VisitLastName),
                new MySqlParameter("_VehicleBrand",VehicleBrand),
                new MySqlParameter("_VehiclePlate",VehiclePlate),
                new MySqlParameter("_VehicleColor",VehicleColor),
                new MySqlParameter("_ArrivalTime",ArrivalTime),
                new MySqlParameter("_person",1)

            };
                DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, paramList);

                EmailController emailController = new EmailController();
                int emailresult = emailController.SendEmailVisit(VisitorID, VisitName, VisitLastName, "120", "View Valley", "Escazú", email, 4214152);

                if (emailresult == 1)
                {
                    ViewBag.Person = Person;
                    return RedirectToAction("GetMyVisits", "Functions");
                }
                else
                {
                    ViewBag.Message = "Error al guardar a tu visita";
                    return View("Error");
                }



            }
        
        public ActionResult insertFav(int VisitorID)
        {
            string storedProcedure = "insert_fav";

            List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("p_ID", VisitorID),

            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, paramList);

            
            return RedirectToAction("GetMyVisits", "Functions");

        }

        public ActionResult insertNormal(int VisitorID, DateTime arrivalTime)
        {
            string storedProcedure = "insert_fav";

            List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("p_ID", VisitorID),
                 new MySqlParameter("p_ArrivalDate", arrivalTime)

            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, paramList);

            return RedirectToAction("GetMyVisits", "Functions");

        }

        public ActionResult UpdateV(int VisitorID, string VisitName, string VisitLastName, string VehicleBrand, string VehicleColor, int VehiclePlate, DateTime ArrivalTime, int person)
        {
            string storedProcedure = "UpdateVisitors";

            List<MySqlParameter> paramList = new List<MySqlParameter>()
            {
                new MySqlParameter("p_VisitorID", VisitorID),
                new MySqlParameter("p_VisitorName", VisitName),
                new MySqlParameter("p_VisitorLastName",VisitLastName),
                new MySqlParameter("p_VehicleBrand",VehicleBrand),
                new MySqlParameter("p_VehiclePlate",VehiclePlate),
                new MySqlParameter("p_VehicleColor",VehicleColor),
                new MySqlParameter("p_ArrivalTime",ArrivalTime),

            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, paramList);

            ViewBag.Person = person;
            return RedirectToAction("Index", "Home");

        }

        // GET: FunctionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FunctionsController/Edit/5
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

        // GET: FunctionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FunctionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection f)
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
