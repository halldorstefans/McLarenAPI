using System.Collections.Generic;
using McLaren.Core.Models;

namespace McLaren.Core.Entities
{
    public class Car : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public int fromyear { get; set; }
        public int toyear { get; set; }

        public CarDto Map() =>
            new CarDto
            {
                id = id,
                name = name,
                fromYear = fromyear,
                toYear = toyear
            };
    }
}