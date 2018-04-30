namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Novo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TB_INTEGRACAO_INFO", new[] { "IDT_ORGANISMO" });
            DropColumn("dbo.TB_INTEGRACAO_INFO", "IDT_ORGANISMO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TB_INTEGRACAO_INFO", "IDT_ORGANISMO", c => c.Int(nullable: false));
            CreateIndex("dbo.TB_INTEGRACAO_INFO", "IDT_ORGANISMO", unique: true);
        }
    }
}
