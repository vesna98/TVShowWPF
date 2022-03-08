namespace Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serijica2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Episodes",
                c => new
                    {
                        IDepisode = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        PlotOutline = c.String(),
                        Title = c.String(),
                        Sezonaid = c.Int(nullable: false),
                        Season_IDseason = c.Int(),
                    })
                .PrimaryKey(t => t.IDepisode)
                .ForeignKey("dbo.Seasons", t => t.Season_IDseason)
                .Index(t => t.Season_IDseason);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        IDGenre = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                        IDshow = c.Int(nullable: false),
                        TvShow_Showid = c.Int(),
                    })
                .PrimaryKey(t => t.IDGenre)
                .ForeignKey("dbo.TvShows", t => t.TvShow_Showid)
                .Index(t => t.TvShow_Showid);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        IDseason = c.Int(nullable: false, identity: true),
                        TvShow = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        TvShow_Showid = c.Int(),
                    })
                .PrimaryKey(t => t.IDseason)
                .ForeignKey("dbo.TvShows", t => t.TvShow_Showid)
                .Index(t => t.TvShow_Showid);
            
            CreateTable(
                "dbo.TvShows",
                c => new
                    {
                        Showid = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        EpisodeCnt = c.Int(nullable: false),
                        Name = c.String(),
                        SeasonCnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Showid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Userid = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        Name = c.String(),
                        Lastname = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "TvShow_Showid", "dbo.TvShows");
            DropForeignKey("dbo.Genres", "TvShow_Showid", "dbo.TvShows");
            DropForeignKey("dbo.Episodes", "Season_IDseason", "dbo.Seasons");
            DropIndex("dbo.Seasons", new[] { "TvShow_Showid" });
            DropIndex("dbo.Genres", new[] { "TvShow_Showid" });
            DropIndex("dbo.Episodes", new[] { "Season_IDseason" });
            DropTable("dbo.Users");
            DropTable("dbo.TvShows");
            DropTable("dbo.Seasons");
            DropTable("dbo.Genres");
            DropTable("dbo.Episodes");
        }
    }
}
