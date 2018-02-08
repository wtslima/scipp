namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TB_HISTORICO_DOWNLOAD_INSPECAO",
                c => new
                    {
                        IDT_INSPECAO = c.Int(nullable: false),
                        NOM_RESPONSAVEL = c.String(nullable: false),
                        DAT_DOWNLOAD = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IDT_INSPECAO)
                .ForeignKey("dbo.TB_INSPECAO_CIPP", t => t.IDT_INSPECAO)
                .Index(t => t.IDT_INSPECAO);
            
            CreateTable(
                "dbo.TB_INSPECAO_CIPP",
                c => new
                    {
                        IDT_INSPECAO = c.Int(nullable: false, identity: true),
                        CDN_CIPP = c.String(nullable: false, maxLength: 20),
                        DES_PLACA_LICENCA = c.String(nullable: false),
                        NUM_EQUIPAMENTO = c.Int(nullable: false),
                        DAT_INSPECAO = c.DateTime(nullable: false),
                        NOM_RESPONSAVEL_TECNICO = c.String(),
                        CDA_CODIGO_OIA = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IDT_INSPECAO)
                .Index(t => t.CDN_CIPP, unique: true);
            
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
                .PrimaryKey(t => t.IDT_ORGANISMO)
                .ForeignKey("dbo.TB_ORGANISMO", t => t.IDT_ORGANISMO)
                .Index(t => t.IDT_ORGANISMO);
            
            CreateTable(
                "dbo.TB_ORGANISMO",
                c => new
                    {
                        IDT_ORGANISMO = c.Int(nullable: false, identity: true),
                        NOM_ORGANISMO = c.String(nullable: false),
                        CDA_CODIGO_OIA = c.String(nullable: false, maxLength: 4),
                        CDA_ATIVO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IDT_ORGANISMO)
                .Index(t => t.CDA_CODIGO_OIA, unique: true);
            
            CreateTable(
                "dbo.TB_USUARIO",
                c => new
                    {
                        IDT_USUARIO = c.Int(nullable: false, identity: true),
                        CDA_USUARIO = c.String(nullable: false),
                        CDA_ATIVO = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IDT_USUARIO);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TB_FTP_INFO", "IDT_ORGANISMO", "dbo.TB_ORGANISMO");
            DropForeignKey("dbo.TB_HISTORICO_DOWNLOAD_INSPECAO", "IDT_INSPECAO", "dbo.TB_INSPECAO_CIPP");
            DropIndex("dbo.TB_ORGANISMO", new[] { "CDA_CODIGO_OIA" });
            DropIndex("dbo.TB_FTP_INFO", new[] { "IDT_ORGANISMO" });
            DropIndex("dbo.TB_HISTORICO_EXCLUSAO", new[] { "CDA_CIPP" });
            DropIndex("dbo.TB_INSPECAO_CIPP", new[] { "CDN_CIPP" });
            DropIndex("dbo.TB_HISTORICO_DOWNLOAD_INSPECAO", new[] { "IDT_INSPECAO" });
            DropTable("dbo.TB_USUARIO");
            DropTable("dbo.TB_ORGANISMO");
            DropTable("dbo.TB_FTP_INFO");
            DropTable("dbo.TB_HISTORICO_EXCLUSAO");
            DropTable("dbo.TB_INSPECAO_CIPP");
            DropTable("dbo.TB_HISTORICO_DOWNLOAD_INSPECAO");
        }
    }
}
