using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using User.Management.Service.Services;
using VehiDenceAPI.Models;

namespace VehiDenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisConducereController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailService;

        public PermisConducereController(IConfiguration configuration, IEmailServices emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("AddPermis")]

        public Response AddPermis(PermisConducere pc)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.AddPermisConducere(pc, connection);



            return response;
        }
        [HttpDelete]
        [Route("DeletePermisConducere")]

        public Response DeletePermisConducere(PermisConducere pc)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.DeletePermisConducere(pc, connection);

            return response;
        }
        [HttpGet]
        [Route("PermisList/{username}")]
        public Response PermisConducereList(string username)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            PermisConducere pc = new PermisConducere();
            pc.username = username;
            response = dal.PermisConducereList(pc, connection);

            return response;

        }
    }
}
