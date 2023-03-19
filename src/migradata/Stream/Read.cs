using migradata.Models;
using migradata.Repositories;

namespace migradata.Stream;

public class Read
{
    public async Task Empresas()
    {
        int i = 0;
        using (var context = new Context())
            for (int x = 0; x < 10; x++)
            {
                try
                {
                    Console.WriteLine($"File K3241.K03200Y{x}.D30211.EMPRECSV");
                    using (var reader = new StreamReader($"c:/data/K3241.K03200Y{x}.D30211.EMPRECSV"))
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var fields = line!.Split(';');
                            var emp = new Empresa()
                            {
                                Id = new Guid(),
                                CNPJBase = fields[0].ToString().Replace("\"", ""),
                                RazaoSocial = fields[1].ToString().Replace("\"", ""),
                                NaturezaJuridica = fields[2].ToString().Replace("\"", ""),
                                QualificacaoResponsavel = fields[3].ToString().Replace("\"", ""),
                                CapitalSocial = fields[4].ToString().Replace("\"", ""),
                                PorteEmpresa = fields[5].ToString().Replace("\"", ""),
                                EnteFederativoResponsavel = fields[6].ToString().Replace("\"", "")
                            };
                            i++;
                            await context.AddAsync(emp);
                            await context.SaveChangesAsync();
                        }
                    Console.WriteLine($"Parte {x} ok!");
                }
                catch
                { }
            }
        Console.WriteLine($"Registros Migrados {i}");
    }

    public void Estabelecimento()
    {
        int i = 0;
        int ci = 0;

        using (var reader = new StreamReader($"c:/data/K3241.K03200Y{"0"}.D30211.ESTABELE"))
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                // divide a linha em campos usando o separador (vírgula)
                var fields = line!.Split(';');

                if (fields[20].ToString().Replace("\"", "") == "6607")
                {
                    var c1 = fields[10].Replace("\"", "").ToString();
                    var c2 = fields[5].Replace("\"", "").ToString();
                    var c3 = fields[19].Replace("\"", "").ToString();
                    ci++;
                    Console.WriteLine($"D:{c1} S:{c2} U:{c3}");
                }
                i++;
            }

        Console.WriteLine($"M:{ci} T:{i}");
    }
}