namespace INMETRO.CIPP.INFRA.ENTITYFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumn_ChavePrivada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TB_FTP_INFO", "CDA_PRIVATE_KEY", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TB_FTP_INFO", "CDA_PRIVATE_KEY");
        }
    }
}
