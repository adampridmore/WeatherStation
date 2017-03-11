namespace Repository.Interfaces
{
    // TODO - Rename to IMsSqlConnectionStringFactory
    public interface IConnectionStringFactory
    {
        string GetNameOrConnectionString();
    }
}