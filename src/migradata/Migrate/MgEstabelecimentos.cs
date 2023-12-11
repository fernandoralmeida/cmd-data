using System.Diagnostics;
using System.Text;
using migradata.Helpers;
using migradata.Models;
using System.Data;
using MongoDB.Driver.Linq;
using System.Reflection;

namespace migradata.Migrate;

public static class MgEstabelecimentos
{
    public static async Task FileToDataBase(TServer server, string database, string datasource)
    => await Task.Run(async () =>
    {
        int c1 = 0;
        int c2 = 0;
        int c3 = 0;
        var _insert = SqlCommands.InsertCommand("Estabelecimentos",
                        SqlCommands.Fields_Estabelecimentos,
                        SqlCommands.Values_Estabelecumentos);

        var _timer = new Stopwatch();
        _timer.Start();
        try
        {
            foreach (var file in await FilesCsv.FilesListAync(@"C:\data", ".ESTABELE"))
            {
                var _data = Factory.Data(server);
                var _list = new List<MEstabelecimento>();
                Log.Storage($"Reading File {Path.GetFileName(file)}");
                Console.Write("\n|");
                using (var reader = new StreamReader(file, Encoding.GetEncoding("ISO-8859-1")))
                {
                    var _rows = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var fields = line!.Split(';');

                        if (fields[19].ToString().Replace("\"", "").Trim() == "SP")
                        {
                            _list.Add(DoFields(fields));
                            _rows++;
                            if (c1 % 1000 == 0)
                            {
                                Console.Write($"  {_rows}");
                                Console.Write("\r");
                            }
                        }
                        c1++;
                    }
                }

                var _tasks = new List<Task>();
                var _list_datatables = new List<DataTable>();

                int parts = Cpu.Count;
                int size = (_list.Count / parts) + 1;

                for (int p = 0; p < parts; p++)
                {
                    _list_datatables.Add(
                        ModelToDataTable(
                            new MEstabelecimento(),
                            _list.Skip(p * size).Take(size)
                        ));
                }

                Log.Storage($"Total: {_list.Count} -> Parts: {parts} -> Rows: {size}");

                int _ntask = -1;
                foreach (var dtables in _list_datatables)
                {
                    _tasks.Add(Task.Run(async () =>
                    {
                        _ntask++;
                        var _timer_task = new Stopwatch();
                        _timer_task.Start();
                        var _db = Factory.Data(server);
                        c2 = dtables.Rows.Count;
                        c3 += c2;                     
                        await _data.WriteAsync(dtables, "Estabelecimentos", database, datasource);                      
                        _timer_task.Start();
                        Log.Storage($"Task: {_ntask} | Migrated: {c2} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
                    }));
                }

                await Parallel.ForEachAsync(_tasks,
                    async (t, _) =>
                       await t
                    );

            }
            _timer.Stop();

            Log.Storage($"Read: {c1} | Migrated: {c3} | Time: {_timer.Elapsed:hh\\:mm\\:ss}");
        }
        catch (Exception ex)
        {
            Log.Storage($"Erro: {ex.Message}");
        }
    });

    private static MEstabelecimento DoFields(string[] fields)
    => new()
    {
        CNPJBase = fields[0].ToString().Replace("\"", "").Trim(),
        CNPJOrdem = fields[1].ToString().Replace("\"", "").Trim(),
        CNPJDV = fields[2].ToString().Replace("\"", "").Trim(),
        IdentificadorMatrizFilial = fields[3].ToString().Replace("\"", "").Trim(),
        NomeFantasia = fields[4].ToString().Replace("\"", "").Trim(),
        SituacaoCadastral = fields[5].ToString().Replace("\"", "").Trim(),
        DataSituacaoCadastral = fields[6].ToString().Replace("\"", "").Trim(),
        MotivoSituacaoCadastral = fields[7].ToString().Replace("\"", "").Trim(),
        NomeCidadeExterior = fields[8].ToString().Replace("\"", "").Trim(),
        Pais = fields[9].ToString().Replace("\"", "").Trim(),
        DataInicioAtividade = fields[10].ToString().Replace("\"", "").Trim(),
        CnaeFiscalPrincipal = fields[11].ToString().Replace("\"", "").Trim(),
        CnaeFiscalSecundaria = fields[12].ToString().Replace("\"", "").Trim(),
        TipoLogradouro = fields[13].ToString().Replace("\"", "").Trim(),
        Logradouro = fields[14].ToString().Replace("\"", "").Trim(),
        Numero = fields[15].ToString().Replace("\"", "").Trim(),
        Complemento = fields[16].ToString().Replace("\"", "").Trim(),
        Bairro = fields[17].ToString().Replace("\"", "").Trim(),
        CEP = fields[18].ToString().Replace("\"", "").Trim(),
        UF = fields[19].ToString().Replace("\"", "").Trim(),
        Municipio = fields[20].ToString().Replace("\"", "").Trim(),
        DDD1 = fields[21].ToString().Replace("\"", "").Trim(),
        Telefone1 = fields[22].ToString().Replace("\"", "").Trim(),
        DDD2 = fields[23].ToString().Replace("\"", "").Trim(),
        Telefone2 = fields[24].ToString().Replace("\"", "").Trim(),
        DDDFax = fields[25].ToString().Replace("\"", "").Trim(),
        Fax = fields[26].ToString().Replace("\"", "").Trim(),
        CorreioEletronico = fields[27].ToString().Replace("\"", "").Trim(),
        SituacaoEspecial = fields[28].ToString().Replace("\"", "").Trim(),
        DataSitucaoEspecial = fields[29].ToString().Replace("\"", "").Trim()
    };

    private static DataTable ModelToDataTable(MEstabelecimento model, IEnumerable<MEstabelecimento> modelList)
    {
        DataTable dataTable = new();

        foreach (PropertyInfo property in model.GetType().GetProperties())
            dataTable.Columns.Add(property.Name, property.PropertyType);

        foreach (var item in modelList)
        {
            DataRow row = dataTable.NewRow();

            foreach (PropertyInfo property in model.GetType().GetProperties())
                row[property.Name] = property.GetValue(item);

            dataTable.Rows.Add(row);
        }

        return dataTable;
    }

}