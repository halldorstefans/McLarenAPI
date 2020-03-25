namespace McLaren.Core.Models
{
    public class DriverDto
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
    }
}