using migradata.Interfaces;

namespace migradata.Helpers;

public static class Factory
{
    public static IData Data(TServer server)
    {
        if (server == TServer.SqlServer)
            return new SqlServer.Data();

        else if (server == TServer.PostgreSql)
            return new Postgres.Data();    
            
        else
            return new MySql.Data();
    }
}