-- Get all products of selected brand
CREATE OR REPLACE FUNCTION get_products_of_brand(brand_id int) RETURNS TABLE (
        id integer,
        name varchar,
        description varchar,
        price numeric,
        add_date timestamp,
        secion_category_id integer,
        quantity integer,
        image varchar
    ) LANGUAGE plpgsql AS $$ BEGIN RETURN query
SELECT *
FROM product
WHERE product.brand_id = brand_id;
END;
$$;
-- Select all brands with the number of their products
CREATE OR REPLACE FUNCTION get_brands() RETURNS TABLE (
        id integer,
        name varchar,
        product_count bigint
    ) LANGUAGE plpgsql AS $$ BEGIN RETURN query
SELECT brand.id,
    brand.name,
    count(product.id)
FROM brand
    LEFT JOIN product ON brand.id = product.brand_id
GROUP BY brand.id
ORDER BY count(product.id) DESC;
END;
$$;
-- Get all products for a given category and section.
CREATE OR REPLACE FUNCTION get_products_of_section(section_id integer, category_id integer) RETURNS TABLE (
        id integer,
        name varchar,
        description varchar,
        price numeric,
        brand_id integer,
        add_date timestamp,
        quantity integer,
        image varchar
    ) LANGUAGE plpgsql AS $$ BEGIN RETURN query
SELECT *
FROM section_category AS sc
    INNER JOIN product AS p ON sc.id = p.secion_category_id
WHERE sc.section_id = section_id
    AND sc.category_id = category_id;
END;
$$;
-- Get all completed orders with a given product. Order from newest to latest.
CREATE OR REPLACE FUNCTION get_completed_orders_of_product(product_id integer) RETURNS TABLE (
        id integer,
        user_id integer,
        current_status status,
        order_date timestamp
    ) LANGUAGE plpgsql AS $$ BEGIN RETURN query
SELECT co.id,
    co.user_id,
    co.current_status,
    co.order_date
FROM order_product AS op
    LEFT JOIN customer_order AS co ON op.product_id = product_id
WHERE co.current_status = 'Completed'
ORDER BY co.order_date ASC;
END;
$$;
-- Get all reviews for a given product. Implement this as a view table which contains rating, comment and info of a person who left a comment.
CREATE OR REPLACE FUNCTION get_reviews_of_product(product_id integer) RETURNS TABLE (
        rating integer,
        comment varchar,
        phone varchar,
        email varchar,
        role role,
        first_name varchar,
        last_name varchar
    ) LANGUAGE plpgsql AS $$ BEGIN RETURN query
SELECT r.rating,
    r.review,
    u.phone,
    u.email,
    u.role,
    u.first_name,
    u.last_name
FROM review AS r
    LEFT JOIN user_account AS u ON user_id = u.id
WHERE r.product_id = product_id;
END;
$$;