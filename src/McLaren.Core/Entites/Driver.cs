using McLaren.Core.Models;

namespace McLaren.Core.Entities
{
    public class Driver : BaseEntity
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int gpEntries { get; set; }
        public int gpWins { get; set; }
        public int gpPoles { get; set; }
        public int gpFastestLap { get; set; }
        public int gpPodiums { get; set; }
        public double gpPoints { get; set; }

        public DriverDto Map() =>
            new DriverDto
            {
                id = id,
                firstName = firstName,
                lastName = lastName,                
                gpEntries = gpEntries,
                gpWins = gpWins,
                gpPoles = gpPoles,
                gpFastestLap = gpFastestLap,
                gpPodiums = gpPodiums,
                gpPoints = gpPoints
            };
    }
}