namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class NovaTabela : DbMigration
    {
        public override void Up()
        {
           // DropForeignKey("dbo.TB_FTP_INFO", "IDT_ORGANISMO", "dbo.TB_ORGANISMO");
          //  DropIndex("dbo.TB_FTP_INFO", new[] { "IDT_ORGANISMO" });
            DropPrimaryKey("dbo.TB_ORGANISMO");
            CreateTable(
                "dbo.TB_INTEGRACAO_INFO",
                c => new
                {
                    IDT_INTEGRACAO_INFO = c.Int(nullable: false, identity: true),
                    IDT_ORGANISMO = c.Int(nullable: false),
                    DES_DIRETORIO_CIPP = c.String(nullable: false),
                    CDN_TIPO_INTEGRACAO = c.Int(nullable: false),
                    DES_DIRETORIO_LOCAL_CIPP = c.String(nullable: false),
                    DES_HOST_URI = c.String(nullable: false),
                    DES_PORTA = c.String(),
                    CDA_LOGIN_FTP = c.String(nullable: false),
                    DES_SENHA_FTP = c.String(nullable: false),
                    CDA_PRIVATE_KEY = c.String(),
                })
                .PrimaryKey(t => t.IDT_INTEGRACAO_INFO);

            AddColumn("dbo.TB_HISTORICO_DOWNLOAD_INSPECAO", "IDT_HISTORICO_DOWNLOAD_INSPECAO", c => c.Int(nullable: false));
            AddColumn("dbo.TB_ORGANISMO", "CDA_LI", c => c.String(nullable: false));
            AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.String(nullable: false));
           // AlterColumn("dbo.TB_ORGANISMO", "IDT_ORGANISMO", c => c.Int(nullable: false));
          //  AddPrimaryKey("dbo.TB_ORGANISMO", "IDT_ORGANISMO");
          //  CreateIndex("dbo.TB_ORGANISMO", "IDT_ORGANISMO");
           // AddForeignKey("dbo.TB_INTEGRACAO_INFO", "IDT_INTEGRACAO_INFO", "dbo.TB_ORGANISMO", "IDT_ORGANISMO");
         //   DropTable("dbo.TB_FTP_INFO");
        }

        //public override void Down()
        //{
        //    CreateTable(
        //        "dbo.TB_FTP_INFO",
        //        c => new
        //        {
        //            IDT_ORGANISMO = c.Int(nullable: false),
        //            DES_DIRETORIO_CIPP = c.String(nullable: false),
        //            CDN_TIPO_INTEGRACAO = c.Int(nullable: false),
        //            DES_DIRETORIO_LOCAL_CIPP = c.String(nullable: false),
        //            DES_HOST_URI = c.String(nullable: false),
        //            CDA_LOGIN_FTP = c.String(nullable: false),
        //            DES_SENHA_FTP = c.String(nullable: false),
        //            CDA_PRIVATE_KEY = c.String(),
        //        })
        //        .PrimaryKey(t => t.IDT_ORGANISMO);

        //    DropForeignKey("dbo.TB_ORGANISMO", "IDT_ORGANISMO", "dbo.TB_INTEGRACAO_INFO");
        //    DropIndex("dbo.TB_ORGANISMO", new[] { "IDT_ORGANISMO" });
        //    DropPrimaryKey("dbo.TB_ORGANISMO");
        //    AlterColumn("dbo.TB_ORGANISMO", "IDT_ORGANISMO", c => c.Int(nullable: false, identity: true));
        //    AlterColumn("dbo.TB_INSPECAO_CIPP", "CDA_CODIGO_OIA", c => c.Int(nullable: false));
        //    DropColumn("dbo.TB_ORGANISMO", "Codigo");
        //    DropColumn("dbo.TB_ORGANISMO", "CDA_LI");
        //    DropColumn("dbo.TB_HISTORICO_DOWNLOAD_INSPECAO", "IDT_HISTORICO_DOWNLOAD_INSPECAO");
        //    DropTable("dbo.TB_INTEGRACAO_INFO");
        //    AddPrimaryKey("dbo.TB_ORGANISMO", "IDT_ORGANISMO");
        //    CreateIndex("dbo.TB_FTP_INFO", "IDT_ORGANISMO");
        //    AddForeignKey("dbo.TB_FTP_INFO", "IDT_ORGANISMO", "dbo.TB_ORGANISMO", "IDT_ORGANISMO");
        //}
    }
}
