using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using User.Management.Service.Services;
using VehiDenceAPI.Models;

namespace VehiDenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ITPController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailService;

        public ITPController(IConfiguration configuration, IEmailServices emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("AddItp")]

        public Response AddItp(ITP itp)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.AddItp(itp, connection);
            


            return response;
        }
        [HttpDelete]
        [Route("DeleteITP")]

        public Response DeleteITP(ITP itp)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.DeleteITP(itp, connection);

            return response;
        }
        [HttpGet]
        [Route("ITPList/{nrinmatriculare}")]
        public Response ITPList(string nrinmatriculare)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            ITP itp = new ITP();
            itp.NrInmatriculare = nrinmatriculare;
            response = dal.ITPList(itp, connection);

            return response;

        }
    }
}
