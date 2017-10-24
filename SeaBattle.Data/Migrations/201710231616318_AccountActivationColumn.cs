namespace SeaBattle.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountActivationColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccount", "IsActivated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAccount", "IsActivated");
        }
    }
}
