using System.Data.Entity.ModelConfiguration;
using INMETRO.CIPP.DOMINIO.Modelos;

namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Mapeamentos
{
    public class OrganismoMap : EntityTypeConfiguration<Organismo>
    {
        public OrganismoMap()
        {
            ToTable("TB_ORGANISMO");

            HasKey(o => o.Id);

            Property(o => o.CodigoOIA).HasColumnName("CDA_CODIGO_OIA").IsRequired();

            Property(o => o.Nome).HasColumnName("NOM_ORGANISMO").IsRequired();

            Property(o => o.Nome).HasColumnName("NOM_ORGANISMO").IsRequired();

            Property(o => o.EhAtivo).HasColumnName("CDA_ATIVO");
            HasRequired(o => o.FtpInfo);

        }
    }
}