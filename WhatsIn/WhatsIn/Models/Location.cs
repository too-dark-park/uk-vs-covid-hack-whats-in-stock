namespace WhatsIn.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // e.g supermarket, café, shop
        public string Type { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
