using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using User.Management.Service.Services;
using VehiDenceAPI.Models;

namespace VehiDenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VignetaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailService;

        public VignetaController(IConfiguration configuration, IEmailServices emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }
        [HttpPost]
        [Route("AddVigneta")]

        public Response AddVigneta(Vigneta vigneta)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.AddVigneta(vigneta, connection);



            return response;
        }
        [HttpDelete]
        [Route("DeleteVigneta")]

        public Response DeleteVigneta(Vigneta vigneta)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            response = dal.DeleteVigneta(vigneta, connection);

            return response;
        }
        [HttpGet]
        [Route("VignetaList/{nrInmatriculare}")]
        public Response RevizieServiceList(string nrInmatriculare)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("VehiDenceConnectionString").ToString());
            Dal dal = new Dal();
            Vigneta vg = new Vigneta();
            vg.NrInmatriculare = nrInmatriculare;
            response = dal.VignetaList(vg, connection);

            return response;

        }
    }
}
