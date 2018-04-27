namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_new_table : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TB_FTP_INFO", newName: "TB_INTEGRACAO_INFO");
            DropIndex("dbo.TB_ORGANISMO", new[] { "CDA_CODIGO_OIA" });
            AddColumn("dbo.TB_INTEGRACAO_INFO", "IDT_INTEGRACAO_INFO", c => c.Int(nullable: false));
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.String(nullable: false));
            AlterColumn("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", c => c.String(nullable: false));
            CreateIndex("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TB_ORGANISMO", new[] { "CDA_CODIGO_OIA" });
            AlterColumn("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", c => c.Int(nullable: false));
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.Int(nullable: false));
            DropColumn("dbo.TB_INTEGRACAO_INFO", "IDT_INTEGRACAO_INFO");
            CreateIndex("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", unique: true);
            RenameTable(name: "dbo.TB_INTEGRACAO_INFO", newName: "TB_FTP_INFO");
        }
    }
}
