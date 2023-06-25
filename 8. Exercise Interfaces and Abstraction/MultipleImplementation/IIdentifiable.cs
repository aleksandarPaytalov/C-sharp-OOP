namespace PersonInfo
{
    public interface IIdentifiable : IBirthable
    {
        string Id { get; }
    }
}