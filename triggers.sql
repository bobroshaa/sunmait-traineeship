CREATE OR REPLACE FUNCTION log_order_status_changes()
    RETURNS TRIGGER
    LANGUAGE PLPGSQL AS
$$
BEGIN
    IF (TG_OP = 'UPDATE' AND NEW.current_status <> OLD.current_status) THEN
        INSERT INTO order_history(order_id, status, date) 
        VALUES(OLD.id, NEW.current_status,  now());
    END IF;
    IF (TG_OP = 'INSERT') THEN
        INSERT INTO order_history(order_id, status, date) 
        VALUES(NEW.id, NEW.current_status,  now());
    END IF;
    RETURN NEW;
END;
$$;

CREATE TRIGGER order_status_changes
    AFTER INSERT OR UPDATE
    ON customer_order
    FOR EACH ROW
    EXECUTE PROCEDURE log_order_status_on_update();