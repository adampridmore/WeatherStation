namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DataPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StationId = c.String(),
                        SensorType = c.String(),
                        SensorValueText = c.String(),
                        SensorValueNumber = c.Double(nullable: false),
                        SensorTimestampUtc = c.DateTime(nullable: false),
                        ReceivedTimestampUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DataPoints");
        }
    }
}
