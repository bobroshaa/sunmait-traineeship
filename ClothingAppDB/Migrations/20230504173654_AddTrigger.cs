using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingAppDB.Migrations
{
    /// <inheritdoc />
    public partial class AddTrigger : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE OR REPLACE FUNCTION clothing_store.""LogOrderStatusChanges""()
            RETURNS TRIGGER
            LANGUAGE PLPGSQL AS
                $$
            BEGIN
            IF (TG_OP = 'UPDATE' AND NEW.""CurrentStatus"" <> OLD.""CurrentStatus"") THEN
                INSERT INTO clothing_store.""OrderHistories""(""OrderID"", ""Status"", ""Date"") 
            VALUES(OLD.""ID"", NEW.""CurrentStatus"",  now());
            END IF;
            IF (TG_OP = 'INSERT') THEN
                INSERT INTO clothing_store.""OrderHistories""(""OrderID"", ""Status"", ""Date"") 
            VALUES(NEW.""ID"", NEW.""CurrentStatus"",  now());
            END IF;
            RETURN NEW;
            END;
                $$;

            CREATE OR REPLACE TRIGGER ""OrderStatusChanges""
                AFTER INSERT OR UPDATE
                ON clothing_store.""CustomerOrders""
                FOR EACH ROW
            EXECUTE PROCEDURE clothing_store.""LogOrderStatusChanges""();");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS clothing_store.""OrderStatusChanges"" ON clothing_store.""CustomerOrder"";
                                   DROP FUNCTION IF EXISTS clothing_store.""LogOrderStatusChanges""();");
        }
    }
}
