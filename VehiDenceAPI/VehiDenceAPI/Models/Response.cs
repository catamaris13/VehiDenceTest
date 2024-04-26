using Microsoft.EntityFrameworkCore;

namespace VehiDenceAPI.Models
{
    public class Response
    {
        public int StatusCode {  get; set; }    
        public string StatusMessage { get; set; }
        public Users User { get; set; }
        public List<Masina> listMasina { get; set; }
        public List<Asigurare> listAsigurare { get; set;}

    }
}
