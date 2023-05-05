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
            CREATE OR REPLACE FUNCTION clothing_store.log_order_status_changes()
            RETURNS TRIGGER
            LANGUAGE PLPGSQL AS
                $$
            BEGIN
            IF (TG_OP = 'UPDATE' AND NEW.current_status <> OLD.current_status) THEN
                INSERT INTO clothing_store.order_histories(order_id, status, date) 
            VALUES(OLD.id, NEW.current_status,  now());
            END IF;
            IF (TG_OP = 'INSERT') THEN
                INSERT INTO clothing_store.order_histories(order_id, status, date) 
            VALUES(NEW.id, NEW.current_status,  now());
            END IF;
            RETURN NEW;
            END;
                $$;

            CREATE OR REPLACE TRIGGER order_status_changes
                AFTER INSERT OR UPDATE
                ON clothing_store.customer_orders
                FOR EACH ROW
            EXECUTE PROCEDURE clothing_store.log_order_status_changes();");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS clothing_store.""OrderStatusChanges"" ON clothing_store.""CustomerOrder"";
                                   DROP FUNCTION IF EXISTS clothing_store.""LogOrderStatusChanges""();");
        }
    }
}
