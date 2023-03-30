using migradata.Repositories;

namespace migradata.Migrate;

public static class Normalize
{
    public static async Task Start()
    {
        var data = new Generic();
        Console.WriteLine("Verificando Conexões...");
        Thread.Sleep(3000);
        data.CheckDB();
        Thread.Sleep(3000);
        Console.WriteLine("Normalizando Banco de Dados...");
        await data.WriteAsync(@"DELETE FROM Cnaes");
        Console.WriteLine("Tebela Cnaes Normalizado!");
        await data.WriteAsync(@"DELETE FROM MotivoSituacaoCadastral");
        Console.WriteLine("Tebela MotivoSituacaoCadastral Normalizado!");
        await data.WriteAsync(@"DELETE FROM Municipios");
        Console.WriteLine("Tebela Municipios Normalizado!");
        await data.WriteAsync(@"DELETE FROM NaturezaJuridica");
        Console.WriteLine("Tebela NaturezaJuridica Normalizado!");
        await data.WriteAsync(@"DELETE FROM Paises");
        Console.WriteLine("Tebela Paises Normalizado!");
        await data.WriteAsync(@"DELETE FROM QualificacaoSocios");
        Console.WriteLine("Tebela QualificacaoSocios Normalizado!");
        await data.WriteAsync(@"DELETE FROM Estabelecimentos");
        Console.WriteLine("Tebela Estabelecimentos Normalizado!");
        await data.WriteAsync(@"DELETE FROM Empresas");
        Console.WriteLine("Tebela Empresas Normalizado!");
        await data.WriteAsync(@"DELETE FROM Socios");
        Console.WriteLine("Tebela Socios Normalizado!");
        await data.WriteAsync(@"DELETE FROM Simples");
        Console.WriteLine("Tebela Simples Normalizado!");
        Thread.Sleep(3000);
    }
}