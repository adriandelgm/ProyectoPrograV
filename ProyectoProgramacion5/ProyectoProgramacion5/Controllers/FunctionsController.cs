using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoProgra5.DataBaseHelper;
using ProyectoProgra5.Models;
using System.Data;

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
        public ActionResult AddDelivery()
        {
            return View();
        }
        public ActionResult UpdateDelivery()
        {
            DataBaseHelper.DataBaseWebHelper db = new DataBaseHelper.DataBaseWebHelper();
            List<Delivery> Delivery = new List<Delivery>();

            List<MySqlParameter> param = new List<MySqlParameter>()
            {
                new MySqlParameter("pID", 1),
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
        public ActionResult AddVisit()
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
        public ActionResult GetVisits()
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
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString())
                    //Person = Convert.ToInt16(dr["Person"].ToString())
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
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString())
                    //Person = Convert.ToInt16(dr["Person"].ToString())
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
                    ArrivalTime = Convert.ToDateTime(dr["ArrivalTime"].ToString())
                    //Person = Convert.ToInt16(dr["Person"].ToString())
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
        public ActionResult Create(int VisitorID, string VisitName, string VisitLastName, string VehicleBrand,string VehicleColor ,int VehiclePlate, DateTime ArrivalTime, int person)
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
                new MySqlParameter("_person",1 )

            };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, paramList);
            
           
            return Ok();
            
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
