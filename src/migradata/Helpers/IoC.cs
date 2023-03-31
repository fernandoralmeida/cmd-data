using migradata.Interfaces;

namespace migradata.Helpers;

public static class IoC
{
    public static IData Data(TServer server)
    {
        if (server == TServer.SqlServer)
            return new SqlServer.Data();
        else
            return new MySql.Data();
    }
}