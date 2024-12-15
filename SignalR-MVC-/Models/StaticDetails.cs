namespace SignalRMVC.Models;

public static class StaticDetails
{
    public static Dictionary<DeathlyHallowType, int> DealthyHallowRace = new(3);

    static StaticDetails()
    {
        DealthyHallowRace.Add(DeathlyHallowType.Wand, 0);
        DealthyHallowRace.Add(DeathlyHallowType.Stone, 0);
        DealthyHallowRace.Add(DeathlyHallowType.Cloak, 0);
    }

}

public enum DeathlyHallowType
{
    Wand,
    Stone,
    Cloak
}