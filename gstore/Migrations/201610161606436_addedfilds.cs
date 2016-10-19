namespace gstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfilds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.logins", "Password", c => c.String());
            AddColumn("dbo.logins", "role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.logins", "role");
            DropColumn("dbo.logins", "Password");
        }
    }
}
