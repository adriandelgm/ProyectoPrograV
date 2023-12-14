using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProyectoProgra5.DataBaseHelper;
using ProyectoProgra5.Models;
using System.Data;
using static ProyectoProgra5.DataBaseHelper.DataBaseWebHelper;
using ProyectoProgra5.Controllers;
using Microsoft.AspNetCore.Diagnostics;

namespace ProyectoProgra5.Controllers
{
    public class RootController : Controller
    {
        private readonly DataBaseWebHelper _dbHelper;

        public RootController()
        {
            _dbHelper = new DataBaseWebHelper();
        }

        public class RootViewModel
        {
            public Tuple<List<Rol>, List<Condo>, List<Location>, List<Condo>, List<House>, List<Person>, List<Person>> OriginalData { get; set; }
            public DataTable SearchResults { get; set; }
        }

        [HttpGet]
        public IActionResult Root(string searchValue)
        {
            try
            {
                var RolList = GetRol();
                var CondoList = GetCondo();
                var LocationList = GetLocation();
                var CondosList = GetCondos();
                var HouseList = GetHouse();
                var TenantList = GetPerson();
                var GuardList = GetGuard();

                var originalData = new Tuple<List<Rol>, List<Condo>, List<Location>, List<Condo>, List<House>, List<Person>, List<Person>>(RolList, CondoList, LocationList, CondosList, HouseList, TenantList, GuardList);

                var viewModel = new RootViewModel
                {
                    OriginalData = originalData,
                    SearchResults = string.IsNullOrEmpty(searchValue) ? null : _dbHelper.SearchRecords(searchValue)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, show error message, etc.)
                return View("Error", ex); // Assuming you have an error view
            }
        }

        [HttpGet]
        public List<Location> GetLocation()
        {
            string storedProcedure = "GetLocation"; // Nombre de tu procedimiento almacenado.
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Location> place = new List<Location>();

            foreach (DataRow row in result.Rows)
            {
                Location location = new Location
                {
                    LocationID = Convert.ToInt32(row["LocationID"]),
                    LocationName = row["LocationName"].ToString()
                };

                place.Add(location);
            }

            return place;
        }


        [HttpGet]
        public List<Rol> GetRol()
        {
            string storedProcedure = "getrol";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Rol> rol = new List<Rol>();

            foreach (DataRow row in result.Rows)
            {
                Rol roles = new Rol
                {
                    RolID = Convert.ToInt32(row["RolID"]),
                    RolName = row["RolName"].ToString()
                };

                rol.Add(roles);
            }

            return rol;
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

        [HttpGet]
        public List<Condo> GetCondos()
        {
            string storedProcedure = "GetCondoInfo";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Condo> condos = new List<Condo>();

            foreach (DataRow row in result.Rows)
            {
                Condo condo = new Condo
                {
                    CondoID = Convert.ToInt32(row["CondoID"]),
                    CondoName = row["CondoName"].ToString(),
                    /*LocationName = row["LocationName"].ToString(),*/
                    Logo = row["Logo"].ToString()
                };

                condos.Add(condo);
            }

            return condos;
        }

        [HttpGet]
        public List<House> GetHouse()
        {
            string storedProcedure = "GetHouseInfo";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<House> houses = new List<House>();

            foreach (DataRow row in result.Rows)
            {
                House house = new House
                {
                    Id = Convert.ToInt32(row["HouseID"]),
                    CondoName = row["CondoName"].ToString(),
                    Person = Convert.ToInt32(row["Person"]),
                };

                houses.Add(house);
            }

            return houses;
        }

        [HttpGet]
        public List<Person> GetPerson()
        {
            string storedProcedure = "GetTenantInfo";
            List<MySqlParameter> parameters = new List<MySqlParameter>();

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


        [HttpGet]
        public List<Person> GetGuard()
        {
            string storedProcedure = "GetGuardInfo"; // Modified stored procedure name
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            DataTable result = _dbHelper.Fill(storedProcedure, parameters);

            List<Person> guards = new List<Person>();

            foreach (DataRow row in result.Rows)
            {
                Person guard = new Person
                {
                    Id = Convert.ToInt32(row["PersonID"]),
                    Name = row["PersonName"].ToString(),
                    LastName = row["PersonLastName"].ToString(),
                    Rol = row["RolName"].ToString(),
                    PersonUser = row["PersonUser"].ToString(),
                    Email = row["ContactEmail"].ToString(),
                    Phone = row["ContactPhone"].ToString()
                };

                guards.Add(guard);
            }

            return guards;
        }

        [HttpPost]
        public ActionResult SaveCondo(string CondoName, int Location, string Logo)
        {
            string storedProcedure = "SaveCondo";

            List<MySqlParameter> param = new List<MySqlParameter>()
        {
            new MySqlParameter("_CondoName", CondoName),
            new MySqlParameter("_Location", Location),
            new MySqlParameter("_Logo", Logo)

        };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, param);

            return RedirectToAction("Root", "Root");
        }

        private int GetLastSavedOfficeId()
        {
            string storedProcedure = "GetLastSavedOfficeID";
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            DataTable result = helper.Fill(storedProcedure, parameters);

            if (result.Rows.Count > 0)
            {
                // Return the last inserted office ID
                return Convert.ToInt32(result.Rows[0]["LastOfficeID"]);
            }
            else
            {
                // Handle the case where the office ID was not found (this may not be applicable to your specific use case)
                throw new InvalidOperationException("Last office ID not found.");
            }
        }

        [HttpPost]
        public ActionResult SaveOfficeAndContact(int Condo, string OfficeName, string Email, string Phone)
        {
            // Assuming the stored procedures are the same as in the original methods
            string saveOfficeStoredProcedure = "SaveOffice";
            string saveContactOfficeStoredProcedure = "SaveContactOffice";

            // Save Office
            List<MySqlParameter> officeParamList = new List<MySqlParameter>()
        {
            new MySqlParameter("_OfficeName", OfficeName),
            new MySqlParameter("_Condo", Condo)
        };
            DataBaseHelper.DataBaseWebHelper officeHelper = new DataBaseWebHelper();
            officeHelper.ExecuteQuery(saveOfficeStoredProcedure, officeParamList);

            // Get the ID of the last saved office
            int officeId = GetLastSavedOfficeId();

            // Save Contact Office
            List<MySqlParameter> contactParamList = new List<MySqlParameter>()
        {
            new MySqlParameter("_Office", officeId),
            new MySqlParameter("_Email", Email),
            new MySqlParameter("_Phone", Phone)
        };
            DataBaseHelper.DataBaseWebHelper contactHelper = new DataBaseWebHelper();
            contactHelper.ExecuteQuery(saveContactOfficeStoredProcedure, contactParamList);

            return RedirectToAction("Root", "Root");
        }


        [HttpPost]
        public ActionResult SaveHouse(int Condo, int Person)
        {
            string storedProcedure = "SaveHouse";

            List<MySqlParameter> paramList = new List<MySqlParameter>()
    {
        new MySqlParameter("_Condo", Condo),
        new MySqlParameter("_Person", Person)

    };
            DataBaseHelper.DataBaseWebHelper helper = new DataBaseWebHelper();
            helper.ExecuteQuery(storedProcedure, paramList);


            return RedirectToAction("Root", "Root");

        }

        [HttpPost]
        public ActionResult SavePersonAndContact
    (
        int PersonID,
        string PersonName,
        string PersonLastName,
        int PersonRol,
        string PersonUser,
        string PersonPassword,
        string Email,
        string Phone,
        string house,
        string condo,
        string condoPhone,
        string PersonPic
    )
        {
            try
            {
                // Assuming the stored procedures are the same as in the original methods
                string savePersonStoredProcedure = "SavePerson";
                string saveContactPersonStoredProcedure = "SaveContactPerson";

                // Save Person
                List<MySqlParameter> personParamList = new List<MySqlParameter>()
        {
            new MySqlParameter("_PersonID", PersonID),
            new MySqlParameter("_PersonName", PersonName),
            new MySqlParameter("_PersonLastName", PersonLastName),
            new MySqlParameter("_PersonRol", PersonRol),
            new MySqlParameter("_PersonUser", PersonUser),
            new MySqlParameter("_PersonPassword", PersonPassword),
            new MySqlParameter("_PersonPic", PersonPic)
        };
                DataBaseHelper.DataBaseWebHelper personHelper = new DataBaseWebHelper();
                personHelper.ExecuteQuery(savePersonStoredProcedure, personParamList);

                // Save Contact Person
                List<MySqlParameter> contactParamList = new List<MySqlParameter>()
        {
            new MySqlParameter("p_Person", PersonID), // Use the provided PersonID directly
            new MySqlParameter("p_Email", Email),
            new MySqlParameter("p_Phone", Phone)
        };
                DataBaseHelper.DataBaseWebHelper contactHelper = new DataBaseWebHelper();
                contactHelper.ExecuteQuery(saveContactPersonStoredProcedure, contactParamList);

                // Send confirmation email
                EmailController emailController = new EmailController();
                int emailResult = emailController.SendEmailPerson(PersonName, PersonLastName, house, condo, condoPhone, Email, Convert.ToInt32(Phone));

                if (emailResult == 1)
                {
                    return RedirectToAction("Root", "Root");
                }
                else
                {
                    ViewBag.Message = "Person and contact information saved successfully, but there was an issue sending the confirmation email.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error saving person and contact information: " + ex.Message;
                return View("Error");
            }
        }




        [HttpPost]
        public IActionResult UpdateCondo(int condoId, string condoName, string logo)
        {
            string storedProcedure = "UpdateCondo";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("p_condo_id", condoId),
            new MySqlParameter("p_condo_name", condoName),
            new MySqlParameter("p_logo", logo)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);


                return Ok("Condo actualizado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el condo: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult UpdatePerson(int personId, string personName, string personLastName, int personRolId, string personUser)
        {
            string storedProcedure = "UpdatePerson";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
    {
        new MySqlParameter("p_person_id", personId),
        new MySqlParameter("p_person_name", personName),
        new MySqlParameter("p_person_last_name", personLastName),
        new MySqlParameter("p_person_rol_id", personRolId),
        new MySqlParameter("p_person_user", personUser)
    };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return Ok("Persona actualizada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la persona: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult UpdateHouse(int houseId, int condoIdHouse)
        {
            string storedProcedure = "UpdateHouse";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("p_house_id", houseId),
            new MySqlParameter("p_condo_id_house", condoIdHouse)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return Ok("Casa actualizada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la casa: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult UpdateOffice(int officeId, string officeName, int condoIdOffice)
        {
            string storedProcedure = "UpdateOffice";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("p_office_id", officeId),
            new MySqlParameter("p_office_name", officeName),
            new MySqlParameter("p_condo_id_office", condoIdOffice)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return Ok("Oficina actualizada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar la oficina: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult DeleteCondo(int condoId)
        {
            string storedProcedure = "DeleteCondo";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("condoIDParam", condoId)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return RedirectToAction("Root", "Root");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting condo: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult DeleteHouse(int houseId)
        {
            string storedProcedure = "DeleteHouse";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("houseIDParam", houseId)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return RedirectToAction("Root", "Root");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting house: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult DeleteGuard(int personId)
        {
            string storedProcedure = "DeletePersonbyID";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("personIDParam", personId)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return RedirectToAction("Root", "Root");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting guard: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult DeletePersonAndRelatedRecords(int personId)
        {
            string storedProcedure = "DeletePersonAndRelatedRecords";

            List<MySqlParameter> parameters = new List<MySqlParameter>()
        {
            new MySqlParameter("personIDParam", personId)
        };

            try
            {
                DataBaseWebHelper helper = new DataBaseWebHelper();
                helper.ExecuteQuery(storedProcedure, parameters);

                return RedirectToAction("Root", "Root");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting person: {ex.Message}");
            }
        }

        public IActionResult Search(string searchValue)
        {
            try
            {
                // Search for records based on the provided searchValue
                DataTable searchResults = _dbHelper.SearchRecords(searchValue);

                // Retrieve the original data using appropriate methods
                var rolList = GetRol();
                var condoList = GetCondo();
                var locationList = GetLocation();
                var condosList = GetCondos();
                var houseList = GetHouse();
                var tenantList = GetPerson();
                var guardList = GetGuard();

                // Create a RootViewModel instance to hold both the original data and search results
                var viewModel = new RootViewModel
                {
                    OriginalData = new Tuple<List<Rol>, List<Condo>, List<Location>, List<Condo>, List<House>, List<Person>, List<Person>>(
                        rolList, condoList, locationList, condosList, houseList, tenantList, guardList),
                    SearchResults = searchResults
                };

                // Pass the viewModel to the "Root" view
                return View("Root", viewModel);
            }
            catch (Exception ex)
            {
                // Handle exceptions (log, show error message, etc.)
                return View("Error", ex); // Assuming you have an error view
            }
        }

    }
}
