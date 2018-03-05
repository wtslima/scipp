namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoColumnResponsavelTec : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TB_INSPECAO_CIPP", "NOM_RESPONSAVEL_TECNICO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TB_INSPECAO_CIPP", "NOM_RESPONSAVEL_TECNICO", c => c.String());
        }
    }
}
