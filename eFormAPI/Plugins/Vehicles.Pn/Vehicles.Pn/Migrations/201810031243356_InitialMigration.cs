namespace Vehicles.Pn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleInspections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkflowState = c.String(maxLength: 255),
                        Version = c.Int(),
                        Status = c.Int(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        DoneAt = c.DateTime(),
                        EformId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNumber = c.String(maxLength: 100),
                        CustomerName = c.String(maxLength: 250),
                        Brand = c.String(maxLength: 100),
                        ModelName = c.String(maxLength: 100),
                        RegistrationDate = c.DateTime(nullable: false),
                        VinNumber = c.String(maxLength: 17),
                        ContractStartDate = c.DateTime(nullable: false),
                        ContractEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.VinNumber, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleInspections", "VehicleId", "dbo.Vehicles");
            DropIndex("dbo.Vehicles", new[] { "VinNumber" });
            DropIndex("dbo.VehicleInspections", new[] { "VehicleId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.VehicleInspections");
        }
    }
}
