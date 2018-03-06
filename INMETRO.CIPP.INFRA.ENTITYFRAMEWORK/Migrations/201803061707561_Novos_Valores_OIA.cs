namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Novos_Valores_OIA : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TB_ORGANISMO", new[] { "CDA_CODIGO_OIA" });
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.Int(nullable: false));
            AlterColumn("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", c => c.Int(nullable: false));
            CreateIndex("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TB_ORGANISMO", new[] { "CDA_CODIGO_OIA" });
            AlterColumn("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", c => c.String(nullable: false, maxLength: 4));
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.String(nullable: false));
            CreateIndex("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", unique: true);
        }
    }
}
