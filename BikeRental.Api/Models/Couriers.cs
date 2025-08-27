namespace BikeRental.Api.Models
{
    public class Courier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string DriverLicenseNumber { get; set; }
        public string DriverLicenseType { get; set; } // A, B, A+B
        public string DriverLicenseImageUrl { get; set; }
    }
}
