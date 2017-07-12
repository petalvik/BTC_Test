namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderId : DbMigration
    {
        public override void Up()
        {
            AddColumn("rpg.Ability", "OrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("rpg.Ability", "OrderId");
        }
    }
}
