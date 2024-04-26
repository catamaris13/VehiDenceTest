using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using VehiDenceAPI.Models;

namespace VehiDenceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsigurareController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AsigurareController(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }
}
