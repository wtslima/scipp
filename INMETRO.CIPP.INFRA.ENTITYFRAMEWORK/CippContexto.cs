using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using INMETRO.CIPP.DOMINIO.Modelos;
using INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Mapeamentos;

namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK
{
    public class CippContexto : DbContext
    {
        public CippContexto() : base("CIPP_CONTEXTO")
        {
            Database.SetInitializer<CippContexto>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            modelBuilder.Configurations.Add(new OrganismoMap());

            modelBuilder.Entity<Organismo>().HasIndex(o => o.CodigoOIA)
                .IsUnique();

            modelBuilder.Entity<Inspecao>().HasIndex(o => o.CodigoCIPP)
                .IsUnique();

            modelBuilder.Entity<HistoricoExclusao>().HasIndex(o => o.Cipp)
                .IsUnique();


            // Configura OrganismoId como PK para FTPInfo
            //modelBuilder.Entity<IntegracaoInfos>()
            //    .HasKey(e => e.OrganismoId);



            //Configura OrganismoId como FK for Ftpinfo

           modelBuilder.Entity<Organismo>()
               .HasRequired(s => s.IntegracaoInfo)
               .WithRequiredPrincipal(ad => ad.Organismo);

           modelBuilder.Entity<Historico>()
                .HasKey(e => e.InspecaoId);

            modelBuilder.Entity<Inspecao>()
                .HasRequired(s => s.Historico)
                .WithRequiredPrincipal(s => s.Inspecao);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Organismo> Organismos { get; set; }

        public DbSet<Inspecao> Inspecoes { get; set; }

        public DbSet<IntegracaoInfos> IntegracaoInfo { get; set; }

        public DbSet<Historico> Historico { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<HistoricoExclusao> HistoricoExclusoes { get; set; }
    }
}