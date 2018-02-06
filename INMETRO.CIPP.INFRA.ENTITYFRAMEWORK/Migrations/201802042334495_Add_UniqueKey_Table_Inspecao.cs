namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UniqueKey_Table_Inspecao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDN_CIPP", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.TB_INSPECAO_CIPP", "CDN_CIPP", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TB_INSPECAO_CIPP", new[] { "CDN_CIPP" });
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDN_CIPP", c => c.String(nullable: false));
        }
    }
}
