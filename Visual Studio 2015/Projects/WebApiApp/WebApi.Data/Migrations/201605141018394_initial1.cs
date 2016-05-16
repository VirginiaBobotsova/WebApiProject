namespace WebApi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Town = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exibitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Img = c.String(),
                        GalleryId = c.String(),
                        Gallery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id)
                .Index(t => t.Gallery_Id);
            
            CreateTable(
                "dbo.Paintings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                        Img = c.String(),
                        Exibition_Id = c.Int(),
                        Gallery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exibitions", t => t.Exibition_Id)
                .ForeignKey("dbo.Galleries", t => t.Gallery_Id)
                .Index(t => t.Exibition_Id)
                .Index(t => t.Gallery_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paintings", "Gallery_Id", "dbo.Galleries");
            DropForeignKey("dbo.Paintings", "Exibition_Id", "dbo.Exibitions");
            DropForeignKey("dbo.Exibitions", "Gallery_Id", "dbo.Galleries");
            DropIndex("dbo.Paintings", new[] { "Gallery_Id" });
            DropIndex("dbo.Paintings", new[] { "Exibition_Id" });
            DropIndex("dbo.Exibitions", new[] { "Gallery_Id" });
            DropTable("dbo.Paintings");
            DropTable("dbo.Exibitions");
            DropTable("dbo.Galleries");
        }
    }
}
