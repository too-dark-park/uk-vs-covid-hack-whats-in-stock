namespace WhatsIn.Helpers
{
    public static class LocationHelper
    {
        public static bool IsValidLocation(double? latitude, double? longitude)
        {
            if (latitude == null || longitude == null)
            {
                return false;
            }

            // Latitude. May range from -90.0 to 90.0
            if (latitude < -90 || latitude > 90)
            {
                return false;
            }

            // Longitude. May range from -180.0 to 180.0
            if (longitude < -180 || latitude > 180)
            {
                return false;
            }

            return true;
        }
    }
}
