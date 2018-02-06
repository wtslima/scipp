namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_Historico_Exlusao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_HISTORICO_EXCLUSAO",
                c => new
                    {
                        IDT_HISTORICO_EXCLUSAO = c.Int(nullable: false, identity: true),
                        CDA_CIPP = c.String(nullable: false, maxLength: 20),
                        CDA_CODIGO_OIA = c.String(nullable: false),
                        DAT_DOWNLOAD = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDT_HISTORICO_EXCLUSAO)
                .Index(t => t.CDA_CIPP, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TB_HISTORICO_EXCLUSAO", new[] { "CDA_CIPP" });
            DropTable("dbo.TB_HISTORICO_EXCLUSAO");
        }
    }
}
