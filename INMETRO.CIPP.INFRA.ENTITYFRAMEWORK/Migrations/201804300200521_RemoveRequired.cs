namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TB_INSPECAO_CIPP", "DES_PLACA_LICENCA", c => c.String());
            AlterColumn("dbo.TB_INSPECAO_CIPP", "NUM_EQUIPAMENTO", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TB_INSPECAO_CIPP", "NUM_EQUIPAMENTO", c => c.String(nullable: false));
            AlterColumn("dbo.TB_INSPECAO_CIPP", "DES_PLACA_LICENCA", c => c.String(nullable: false));
        }
    }
}
