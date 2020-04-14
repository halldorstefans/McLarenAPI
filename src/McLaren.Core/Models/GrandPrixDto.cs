using System.Collections.Generic;

namespace McLaren.Core.Models
{
    public class GrandPrixDto
    {
        public int raceid { get; set; }
        public int year { get; set; }
        public string country { get; set; }
        public IEnumerable<GrandPrixDriverDto> drivers { get; set; }
    }
}