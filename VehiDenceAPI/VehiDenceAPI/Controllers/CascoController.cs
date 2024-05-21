using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using User.Management.Service.Services;
using VehiDenceAPI.Models;

namespace VehiDenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CascoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailService;

        public CascoController(IConfiguration configuration, IEmailServices emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("AddCasco")]

        public Response AddCasco(Casco casco)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.AddCasco(casco, connection);

            return response;
        }
        [HttpDelete]
        [Route("DeleteCasco")]

        public Response DeleteCaco(Casco casco)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.DeleteCasco(casco, connection);

            return response;
        }
        [HttpGet]
        [Route("CascoList/{nrinmatriculare}")]
        public Response CascoList(string nrinmatriculare)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            Casco casco = new Casco();
            casco.NrInmatriculare = nrinmatriculare;
            response = dal.CascoList(casco, connection);

            return response;

        }
    }
}
