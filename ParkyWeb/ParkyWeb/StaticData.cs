namespace ParkyWeb
{
    public  static class StaticData
    {
        public static string ApiBaseUrl { get; set; } = "https://localhost:7127/";
        public static string ApiNationalParkUrl { get; set; } = ApiBaseUrl + "api/v1/NationalPark";

        public static string ApiTrailUrl { get; set; } = ApiBaseUrl + "api/Trail";



    }
}
