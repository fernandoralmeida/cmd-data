namespace migradata.Helpers;

public static class DataSource
{
    public static readonly string SqlServer
        = Environment.GetEnvironmentVariable("datasource_sqlserver")!;

    public static readonly string Docker_SqlServer_VPS
        = Environment.GetEnvironmentVariable("datasource_sqlserver_vps")!;

    public static readonly string MySQL
        = Environment.GetEnvironmentVariable("datasource_mysql")!;

    public static readonly string PostgreSql
        = Environment.GetEnvironmentVariable("datasource_postgresql")!;

}