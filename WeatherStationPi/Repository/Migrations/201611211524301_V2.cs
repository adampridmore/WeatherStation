namespace Repository.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DataPoints");
            AlterColumn("dbo.DataPoints", "StationId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.DataPoints", "SensorType", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.DataPoints", new[] { "StationId", "SensorType", "SensorTimestampUtc" });
            DropColumn("dbo.DataPoints", "Id");
            DropColumn("dbo.DataPoints", "SensorValueText");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DataPoints", "SensorValueText", c => c.String());
            AddColumn("dbo.DataPoints", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.DataPoints");
            AlterColumn("dbo.DataPoints", "SensorType", c => c.String());
            AlterColumn("dbo.DataPoints", "StationId", c => c.String());
            AddPrimaryKey("dbo.DataPoints", "Id");
        }
    }
}
