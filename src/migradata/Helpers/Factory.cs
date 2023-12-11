using migradata.Interfaces;

namespace migradata.Helpers;

public static class Factory
{
    public static IData Data(TServer server)
    {
        return new SqlServer.Data();
        /*
        if (server == TServer.SqlServer)
            return new SqlServer.Data();

        
        else if (server == TServer.PostgreSql)
            return new SqlServer.Data();    
        
        else
            return new SqlServer.Data();
        */
    }
}