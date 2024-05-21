using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NETCore.MailKit.Core;
using User.Management.Service.Services;
using VehiDenceAPI.Models;

namespace VehiDenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsigurareController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailService;

        public AsigurareController(IConfiguration configuration, IEmailServices emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("AddAsigurare")]

        public Response AddAsigurare(Asigurare asigurare)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.AddAsigurare(asigurare, connection);

            return response;
        }
        [HttpDelete]
        [Route("DeleteAsigurare")]

        public Response DeleteAsigurare(Asigurare asigurare)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.DeleteAsigurare(asigurare, connection);

            return response;
        }
        [HttpGet]
        [Route("AsigutareList/{nrinmatriculare}")]
        public Response AsigutareList(string nrinmatriculare)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            Asigurare asigurare = new Asigurare();
            asigurare.NrInmatriculare = nrinmatriculare;
            response = dal.AsigutareList(asigurare, connection);
            
            return response;

        }
        
        [HttpPost]
        [Route("SendExpirationReminder")]
        public async Task<IActionResult> SendExpirationReminder()
        {

            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.VerificareExpirareAsigurare(connection);
            RecurringJob.AddOrUpdate("Verificare asigurare", () => SendExpirationReminder(), "0 0 * * *");
            //Console.WriteLine(response.ToString());
  
                if (response.StatusCode == 200)
            {

                
               
                string subject = "Expirare Asigurare";

                foreach (Users user in response.listUsers)
                {
                    string message = $"Hi {user.Name}! " +
                        $"Your insurance will expire in 7 days from now !" +
                        $"Don't forget to get in touch with your inssurance company!";


                    try
                    {

                        await _emailService.SendEmailAsync(user.Email, subject, message);

                       
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to send email: {ex.Message}");
                    }
                   
                } 
                return StatusCode(200, "Email sent successful. Please check your email for resset instructions.");
            }
            return StatusCode(500, "Failed to send email");
        }
    }
    }

