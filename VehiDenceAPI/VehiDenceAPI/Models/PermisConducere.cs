using System.Runtime.CompilerServices;

namespace VehiDenceAPI.Models
{
    public class PermisConducere
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string username { get; set; }    
        public DateTime DataCreare { get; set; }
        public DateTime DataExpirare { get; set; }
        public string Categorie { get; set; }
        public byte[] ImageData { get; set; } = null;
    }
}
