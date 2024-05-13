using System.Runtime.CompilerServices;

namespace VehiDenceAPI.Models
{
    public class Asigurare
    {
        public int Id { get; set; }
        public string SerieSasiu { get; set; }
        public string NrInmatriculare {  get; set; }
        public DateTime DataCreare { get; set; }
        public DateTime DataExpirare { get; set; }
        public string Asigurator {  get; set; }

    }
}
