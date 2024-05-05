using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NETCore.MailKit.Core;
using System.Data.SqlTypes;
using User.Management.Service.Models;
using User.Management.Service.Services;
using VehiDenceAPI.Models;
using IEmailServices = User.Management.Service.Services.IEmailServices;

namespace VehiDenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailService;

        public UserController(IConfiguration configuration, IEmailServices emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Registration(Users user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            user.Token= Guid.NewGuid().ToString();
            response = dal.Registration(user, connection);
            Console.WriteLine(user.Token);

            // Generate unique token for email validation


            // Save the token in the database along with the user's email

            // Send validation email
            if (response.StatusCode == 200)
            {
                //string validationLink = $"http://localhost:5277/api/User/ValidateEmail?username={user.username}&token={user.Token}";
                string validationLink = $"http://localhost:3000/email_validation?username={user.username}&token={user.Token}";
                string subject = "Welcome to our platform!";
                string message = $"Thank you for registering with us, {user.Name}. Please click the following link to validate your email: {validationLink}";

                try
                {

                    await _emailService.SendEmailAsync(user.Email, subject, message);
                    
                    return StatusCode(200,"Registration successful. Please check your email for validation instructions.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to send email: {ex.Message}");
                }
            }
             return StatusCode(500, "Failed to send email");

        }
        [HttpPost]
        [Route("Login")]
        public Response Login(Users user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.Login(user, connection);

            return response;
        }

        [HttpPut]
        [Route("Update")]
        public Response UpdateUser(Users user)
        {
            Response response = new Response();
            SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();

            response=dal.UserUpdate(user, connection);

            return response;

        }
        [HttpDelete]
        [Route("Delete")]
        public Response DeleteUser(Users user)
        {
            Response response = new Response();
            SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.DeleteUser(user, connection);
            return response;
        }

        [HttpGet]
        [Route("All/Users")]
        public List<Users> GetUsers()
        {
            List<Users> users = new List<Users>();
            SqlConnection connection =
                new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();

            users=dal.GetUsers(connection);
            return users;
        }
        
        [HttpGet]
        [Route("SendEmail")]
        public async Task<IActionResult> TestEmail()
        {
            var reciver = "catalinmaris@yahoo.com";
            var subiect = "ceva";
            var mesaj = "Du te ba de aici";
            
            try
            {
                await _emailService.SendEmailAsync(reciver, subiect, mesaj);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Failed to send email: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("ValidateEmail")]
        public Response ValidateEmail([FromQuery]string username,[FromQuery] string token)
        {
            Response response = new Response();
            Users user=new Users();
            user.username = username;
            user.Token = token;
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.UserValidation(user, connection);

            return response;

            // Redirect the user to a page indicating successful validation
           
        }
        [HttpPost]
        [Route("ResetPassword")]
        public Response RessetPassword(Users user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.ResetPassword(user, connection);

            return response;

            // Redirect the user to a page indicating successful validation

        }
        [HttpPost]
        [Route("SendEmailPassword")]
        public async Task<IActionResult> SendEmailPassword(Users user)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            user.Token = Guid.NewGuid().ToString();
            response = dal.ResetToken(user, connection);
            Console.WriteLine(user.Token);

            // Generate unique token for email validation


            // Save the token in the database along with the user's email

            // Send validation email
            if (response.StatusCode == 200)
            {
                //string validationLink = $"http://localhost:5277/api/User/ValidateEmail?username={user.username}&token={user.Token}";
                string resetLink = $"http://localhost:3000/resset_password?email={user.Email}&token={user.Token}";
                string subject = "Reset Password";
                string message = $"Hi {user.Name}! Please click the following link to reset your password: {resetLink}";

                try
                {

                    await _emailService.SendEmailAsync(user.Email, subject, message);

                    return StatusCode(200, "Email sent successful. Please check your email for resset instructions.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to send email: {ex.Message}");
                }
            }
            return StatusCode(500, "Failed to send email");

        }



    }


}

