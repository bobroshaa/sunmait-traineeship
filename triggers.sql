CREATE OR REPLACE FUNCTION log_order_status_on_update()
    RETURNS TRIGGER
    LANGUAGE PLPGSQL
    AS
$$
BEGIN
    IF NEW.current_status <> OLD.current_status THEN
        INSERT INTO order_history(order_id, status, date) 
        VALUES(OLD.id, NEW.current_status,  now());
    END IF;
    RETURN NEW;
END;
$$;

CREATE TRIGGER order_status_on_update
  BEFORE UPDATE
  ON customer_order
  FOR EACH ROW
  EXECUTE PROCEDURE log_order_status_on_update();

CREATE OR REPLACE FUNCTION log_order_status_on_add()
    RETURNS TRIGGER
    LANGUAGE PLPGSQL
    AS
$$
BEGIN
    INSERT INTO order_history(order_id, status, date) 
    VALUES(NEW.id, NEW.current_status,  now());
    RETURN NEW;
END;
$$;

CREATE TRIGGER order_status_on_add
  AFTER INSERT
  ON customer_order
  FOR EACH ROW
  EXECUTE PROCEDURE log_order_status_on_add();
