public interface IInteractable
{
    /// <summary>
    /// Meant to be called when a player interacts with this thing
    /// by walking up to it, (into a trigger collider), and pressing
    /// the interaction button.
    /// </summary>
    /// <param name="pc"></param>
    public void OnInteract( PlayerOverworldControl pc );
}
