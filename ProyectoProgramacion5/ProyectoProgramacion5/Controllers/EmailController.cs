using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using System.Net;
using Xceed.Document.NET;
using ProyectoProgra5.Models;
using System.Drawing;
using System.IO;
using QRCoder;
using System.Text;
using Google.Apis.Upload;
using Google.Apis.Upload;
using Firebase.Storage;
using ProyectoProgra5.FirebaseAuth;


namespace ProyectoProgra5.Controllers
{
    public class EmailController : Controller
    {
        // GET: EmailController


        public int SendEmailPerson(string Name, string Lastname, string house, string condo, string location, string email1, int phone)
        {
            try
            {
                Console.WriteLine("SendEmailPerson method called.");
                MailMessage message = new MailMessage();
                message.From = new MailAddress("prograsoporte32@gmail.com");
                message.To.Add(email1);
                message.Subject = "Register Confirmation";

                string body = string.Format("<div style='font-family: Arial, sans-serif; color: #333;'>" +
                                    "<h2 style='color: #007BFF;'>Registration Confirmation!</h2>" +
                                    "<p>Dear {0},</p>" +
                                    "<p>Welcome to our system. Your registration for the room has been successfully confirmed.</p>" +
                                    "<p>Below are the details of your registration:</p>" +
                                    "<ul>" +
                                    "<li><strong>Name:</strong> {0}</li>" +
                                    "<li><strong>Email:</strong> {1}</li>" +
                                    "<li><strong>Room:</strong> {2}</li>" +
                                    "<li><strong>Habitational Project:</strong> {3}</li>" +
                                    "<li><strong>Direction:</strong> {4}</li>" +
                                    "</ul>" +
                                    "<p style='background-color: #f8f9fa; padding: 10px; border-radius: 5px;'>" +
                                    "Thank you for choosing our platform. If you have any questions, feel free to contact us +506 {5}" +
                                    "</p>" +
                                    "<p>Best regards,<br>The [Condominium APP] Team</p>" +
                                    "</div>", Name, email1, house, condo, location, phone);
                message.Body = body;
                message.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential("soportprimeprogram@gmail.com", "kihjvhoodlmvgmvw");
                smtpClient.EnableSsl = true;

                smtpClient.Send(message);

                Console.WriteLine("Email sent successfully.");

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo enviar el correo. Error: " + ex.Message);

                return 2;
            }
        }

        public int SendEmailVisit(int ID, string Name, string Lastname, string house, string condo, string location, string email1, int phone)
        {
            try
            {


                string qrContent = new Random().Next(1000, 9999).ToString();

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                qrCodeImage.Save("QRImg/" + qrContent + ".png", System.Drawing.Imaging.ImageFormat.Png);


                var QRURL = UploadQRToFirebase("QRImg/" + qrContent + ".png", qrContent + ".png").Result;
           


                MailMessage message = new MailMessage();
                message.From = new MailAddress("prograsoporte32@gmail.com");
                message.To.Add(email1);
                message.Subject = "Confirmación de visita";

                string body = string.Format("<div style='font-family: Arial, sans-serif; color: #333;'>" +
                                    "<h2 style='color: #007BFF;'>Registration Confirmation!</h2>" +
                                    "<p>Dear {0},</p>" +
                                    "<p> Bienvenido a nuestro sistema.</p>" +
                                    "<p>Estos son tus datos personales:</p>" +
                                    "<ul>" +
                                    "<li><strong>Name:</strong> {0}</li>" +
                                    "<li><strong>Last Name:</strong> {1}</li>" +
                                    "<li><strong>Email:</strong> {2}</li>" +
                                    "<li><strong>Room:</strong> {3}</li>" +
                                    "<li><strong>Habitational Project:</strong> {4}</li>" +
                                    "<li><strong>Direction:</strong> {5}</li>" +
                                    "<img src='{6}' alt='QR Code'/>" +
                                    "</ul>" +
                                    "<p style='background-color: #f8f9fa; padding: 10px; border-radius: 5px;'>" +
                                    "Si tienes alguna consulta, contacta a la persona de soporte +506 {7}" +
                                    "</p>" +
                                    "<p>Saludos,<br>LandLock</p>" +
                                    "</div>", Name, Lastname, email1,house, condo, location, QRURL, phone); 
                message.Body = body;
                message.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential("prograsoporte32@gmail.com", "eenrsmybsmzmqvid");
                smtpClient.EnableSsl = true;

                smtpClient.Send(message);

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo enviar el correo. Error: " + ex.Message);

                return 1;
            }
        }
        [HttpGet]
        public IActionResult GetQRCodeImage(string fileName)
        {
            try
            {
                // Construct the full path to the QR code image file
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\alemu\\OneDrive\\Documentos\\GitHub\\ProyectoPrograIV\\ProyectoPrograIV\\ProyectoPrograIV\\", "QRImg");
                string filePath = Path.Combine(folderPath, fileName);

                // Return the image file
                var stream = new FileStream(filePath, FileMode.Open);
                return File(stream, "image/png");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving QR code image: " + ex.Message);
                return NotFound(); // or handle the error in an appropriate way
            }
        }

        public static async Task<string> UploadQRToFirebase(string qrPath, string qrName)
        {
            var downloadUrl = string.Empty;
            using (var streamToFb = System.IO.File.OpenRead(qrPath))
            {
                //Mandamos la foto a Firebase storage y este nos reponde la URL
                downloadUrl = await new FirebaseStorage($"{FirebaseAuthHelper.firebaseAppId}.appspot.com")
                                 .Child("QR")
                                 .Child(qrName)
                                 .PutAsync(streamToFb);
            }

            return downloadUrl;
        }            












        public ActionResult Index()
        {
            return View();
        }

        // GET: EmailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailController/Create
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

        // GET: EmailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmailController/Edit/5
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

        // GET: EmailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmailController/Delete/5
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
