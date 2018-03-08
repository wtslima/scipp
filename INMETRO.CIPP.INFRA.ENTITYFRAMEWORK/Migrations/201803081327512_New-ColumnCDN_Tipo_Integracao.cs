namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnCDN_Tipo_Integracao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_FTP_INFO", "CDN_TIPO_INTEGRACAO", c => c.Int(nullable: false));
            //DropColumn("dbo.TB_FTP_INFO", "DES_DIRETORIO_REMOTO_CIPP");
        }

        public override void Down()
        {
            AddColumn("dbo.TB_FTP_INFO", "DES_DIRETORIO_REMOTO_CIPP", c => c.String(nullable: false));
            //DropColumn("dbo.TB_FTP_INFO", "CDN_TIPO_INTEGRACAO");
        }
    }
}
