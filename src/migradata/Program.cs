using migradata.Models;
using migradata.Repositories;

namespace migradata;
class Program
{
    static async Task Main(string[] args)
    {

        Console.WriteLine("Iniciando Sistema!");
        Console.WriteLine(Environment.GetEnvironmentVariable("connection_string_migradata"));
        Console.WriteLine("Pressione qualquer tecla para iniciar!");
        Console.ReadKey();
        Console.WriteLine("Conectando no Banco de Dados!");
        Console.WriteLine("Deletando Empresas!");

        var iniciar = new Stream.Read();

        var empresas = new REmpresas();

        await empresas.RemoveAllAsync();
        Console.WriteLine("Empresas apagadas!");
        Console.ReadKey();
        Console.WriteLine("Iniciando migração de Empresas!");
        iniciar.Empresas();
        Console.WriteLine("Migração finalizada!");
        /*
        var empresa = new REstabelecimentos();
        var c = empresa.DoListAsync();
        int count = 0;

        await foreach (var cont in empresa.DoListAsync(s => s.Municipio == "6607" && s.SituacaoCadastral == "02"))
        {
            Console.WriteLine($"{cont.CNPJBase}/{cont.CNPJOrdem}-{cont.CNPJDV} {cont.SituacaoCadastral}");
            count++;
        }        
        Console.WriteLine(count);
        */
    }
}
