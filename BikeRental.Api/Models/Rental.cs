namespace BikeRental.Api.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int MotorcycleId { get; set; }
        public int CourierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string Plan { get; set; }

        public Motorcycle Motorcycle { get; set; }
        public Courier Courier { get; set; }
    }
}
