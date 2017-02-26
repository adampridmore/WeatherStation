using System.Data.Entity.Migrations;

namespace Repository.Migrations
{
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.DataPoints",
                    c => new
                    {
                        Id = c.Int(false, true),
                        StationId = c.String(),
                        SensorType = c.String(),
                        SensorValueText = c.String(),
                        SensorValueNumber = c.Double(false),
                        SensorTimestampUtc = c.DateTime(false),
                        ReceivedTimestampUtc = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.DataPoints");
        }
    }
}