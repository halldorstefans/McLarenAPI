using McLaren.Core.Interfaces.Repositories;
using McLaren.Core.Models;

namespace McLaren.Core.Entities
{
    public class GrandPrix : BaseEntity
    {
        public int id { get; set; }
        public int raceid { get; set; }
        public int year { get; set; }
        public string country { get; set; }
        public string team { get; set; }
        public int carNumber { get; set; }
        public int driverId { get; set; }
        public int carId { get; set; }
        public string engine { get; set; }
        public string tyre { get; set; }
        public string grid { get; set; }
        public string position { get; set; }
        public string comment { get; set; }

        public GrandPrixDto Map() =>
            new GrandPrixDto
            {
                raceid = raceid,
                year = year,
                country = country
            };

        public GrandPrixDriverDto MapDriver(ICarsRepository carsRepository, IDriversRepository driversRepository)
        {

            DriverDto driver = driversRepository.Get(driverId).Result.Map();
            return new GrandPrixDriverDto
            {
                team = team,
                carNumber = carNumber,                
                driver = driver.firstName + " " + driver.lastName,
                car = carsRepository.Get(carId).Result.name,
                engine = engine,
                tyre = tyre,
                grid = grid,
                position = position,
                comment = comment
            };
        }
    }
}