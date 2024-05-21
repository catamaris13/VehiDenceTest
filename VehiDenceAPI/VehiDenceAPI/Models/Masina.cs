namespace VehiDenceAPI.Models
{
    public class Masina
    {
        public int Id { get; set; }

        public string SerieSasiu { get; set; }
        public string NrInmatriculare {  get; set; }
        public string Marca { get; set; }   
        public string Model {  get; set; }
        public string Username {  get; set; }
        public byte[] ImageData { get; set; } = null;


    }
}
