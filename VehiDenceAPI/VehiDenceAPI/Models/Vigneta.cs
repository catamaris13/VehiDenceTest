﻿namespace VehiDenceAPI.Models
{
    public class Vigneta
    {
        public int Id { get; set; }
        public string NrInmatriculare { get; set; }
        public DateTime DataCreare { get; set; }
        public DateTime DataExpirare { get; set; }
        public string Tara { get; set; }
    }
}
