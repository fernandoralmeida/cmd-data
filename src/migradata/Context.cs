using Microsoft.EntityFrameworkCore;
using migradata.Models;
using migradata.ContextConfig;

namespace migradata;

public class Context : DbContext
{
    public Context()
    { }

    public Context(DbContextOptions<Context> options) : base(options)
    { }

    public DbSet<Cnae>? Cnaes { get; set; }
    public DbSet<Empresa>? Empresas { get; set; }
    public DbSet<Estabelecimento>? Estabelecimentos { get; set; }
    public DbSet<MotivoSituacaoCadastral>? MotivoSituacaoCadastral { get; set; }
    public DbSet<Municipio>? Municipios { get; set; }
    public DbSet<NaturezaJuridica>? NaturezaJuridica { get; set; }
    public DbSet<Pais>? Paises { get; set; }
    public DbSet<QualificacaoSocio>? QualificacaoSocios { get; set; }
    public DbSet<Simples>? Simples { get; set; }
    public DbSet<Socio>? Socios { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("connection_string_migradata"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CnaeMap());
        modelBuilder.ApplyConfiguration(new EmpresaMap());
        modelBuilder.ApplyConfiguration(new EstabelecimentoMap());
        modelBuilder.ApplyConfiguration(new MotivoSituacaoCadastralMap());
        modelBuilder.ApplyConfiguration(new MunicipioMap());
        modelBuilder.ApplyConfiguration(new NaturezaJuridicaMap());
        modelBuilder.ApplyConfiguration(new PaisMap());
        modelBuilder.ApplyConfiguration(new QualificacaoSocioMap());
        modelBuilder.ApplyConfiguration(new SimplesMap());
        modelBuilder.ApplyConfiguration(new SociosMap());

        base.OnModelCreating(modelBuilder);
    }
}