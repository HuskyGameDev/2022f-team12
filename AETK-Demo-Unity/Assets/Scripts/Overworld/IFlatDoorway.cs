public interface IFlatDoorway
{
    /// <summary>
    /// Meant to be called when a player interacts with this flat doorway.
    /// (Most likely by pressing up in front of it).
    /// </summary>
    /// <param name="pc"></param>
    public void OnInteract(PlayerOverworldControl pc);
}
