namespace ServerManagement.Models;

public static class CitiesRepository
{
    private static List<string> cities = ["Chicago", "Rockford", "Springfield", "Joliet", "Peoria"];
    public static List<string> GetCities() => cities;
}
