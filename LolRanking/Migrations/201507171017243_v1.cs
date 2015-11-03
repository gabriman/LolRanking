namespace FirstSPA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Summoners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SummonerId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        ProfileIconId = c.Int(nullable: false),
                        SummonerLevel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Summoners");
        }
    }
}
