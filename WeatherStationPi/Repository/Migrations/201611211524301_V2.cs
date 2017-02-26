using System.Data.Entity.Migrations;

namespace Repository.Migrations
{
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DataPoints");
            AlterColumn("dbo.DataPoints", "StationId", c => c.String(false, 128));
            AlterColumn("dbo.DataPoints", "SensorType", c => c.String(false, 128));
            AddPrimaryKey("dbo.DataPoints", new[] {"StationId", "SensorType", "SensorTimestampUtc"});
            DropColumn("dbo.DataPoints", "Id");
            DropColumn("dbo.DataPoints", "SensorValueText");
        }

        public override void Down()
        {
            AddColumn("dbo.DataPoints", "SensorValueText", c => c.String());
            AddColumn("dbo.DataPoints", "Id", c => c.Int(false, true));
            DropPrimaryKey("dbo.DataPoints");
            AlterColumn("dbo.DataPoints", "SensorType", c => c.String());
            AlterColumn("dbo.DataPoints", "StationId", c => c.String());
            AddPrimaryKey("dbo.DataPoints", "Id");
        }
    }
}