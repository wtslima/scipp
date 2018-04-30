namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correcoes_IntegracaoInfoes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TB_FTP_INFO", "IDT_ORGANISMO", "dbo.TB_ORGANISMO");
            DropIndex("dbo.TB_FTP_INFO", new[] { "IDT_ORGANISMO" });
            DropIndex("dbo.TB_ORGANISMO", new[] { "CDA_CODIGO_OIA" });
            CreateTable(
                "dbo.TB_INTEGRACAO_INFO",
                c => new
                    {
                        IDT_INTEGRACAO_INFO = c.Int(nullable: false),
                        DES_DIRETORIO_CIPP = c.String(nullable: false),
                        CDN_TIPO_INTEGRACAO = c.Int(nullable: false),
                        DES_DIRETORIO_LOCAL_CIPP = c.String(nullable: false),
                        DES_HOST_URI = c.String(nullable: false),
                        DES_PORTA = c.String(),
                        CDA_LOGIN_FTP = c.String(nullable: false),
                        DES_SENHA_FTP = c.String(nullable: false),
                        IDT_ORGANISMO = c.Int(nullable: false),
                        CDA_PRIVATE_KEY = c.String(),
                    })
                .PrimaryKey(t => t.IDT_INTEGRACAO_INFO)
                .ForeignKey("dbo.TB_ORGANISMO", t => t.IDT_ORGANISMO)
                .Index(t => t.IDT_INTEGRACAO_INFO)
                .Index(t => t.IDT_ORGANISMO, unique: true);
            
            AddColumn("dbo.TB_HISTORICO_DOWNLOAD_INSPECAO", "IDT_HISTORICO_DOWNLOAD_INSPECAO", c => c.Int(nullable: false));
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.String(nullable: false));
            AlterColumn("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", c => c.String(nullable: false));
            //CreateIndex("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", unique: true);
            DropTable("dbo.TB_FTP_INFO");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TB_FTP_INFO",
                c => new
                    {
                        IDT_ORGANISMO = c.Int(nullable: false),
                        DES_DIRETORIO_CIPP = c.String(nullable: false),
                        DES_DIRETORIO_REMOTO_CIPP = c.String(nullable: false),
                        DES_DIRETORIO_LOCAL_CIPP = c.String(nullable: false),
                        DES_HOST_URI = c.String(nullable: false),
                        CDA_LOGIN_FTP = c.String(nullable: false),
                        DES_SENHA_FTP = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDT_ORGANISMO);
            
            DropForeignKey("dbo.TB_INTEGRACAO_INFO", "IDT_INTEGRACAO_INFO", "dbo.TB_ORGANISMO");
            DropIndex("dbo.TB_ORGANISMO", new[] { "CDA_CODIGO_OIA" });
            DropIndex("dbo.TB_INTEGRACAO_INFO", new[] { "DES_DIRETORIO_LOCAL_CIPP" });
            DropIndex("dbo.TB_INTEGRACAO_INFO", new[] { "IDT_INTEGRACAO_INFO" });
            AlterColumn("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", c => c.Int(nullable: false));
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.Int(nullable: false));
            DropColumn("dbo.TB_HISTORICO_DOWNLOAD_INSPECAO", "IDT_HISTORICO_DOWNLOAD_INSPECAO");
            DropTable("dbo.TB_INTEGRACAO_INFO");
            CreateIndex("dbo.TB_ORGANISMO", "CDA_CODIGO_OIA", unique: true);
            CreateIndex("dbo.TB_FTP_INFO", "IDT_ORGANISMO");
            AddForeignKey("dbo.TB_FTP_INFO", "IDT_ORGANISMO", "dbo.TB_ORGANISMO", "IDT_ORGANISMO");
        }
    }
}
