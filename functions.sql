-- Get all products of selected brand
CREATE OR REPLACE FUNCTION get_products_of_brand(brand int)
RETURNS TABLE (
    id integer, 
    name varchar, 
    description varchar, 
    price numeric, 
    sex sex, 
    brand_id integer, 
    add_date timestamp, 
    category_id integer,
    section_id integer,
    quantity integer,
    image varchar)
LANGUAGE plpgsql
AS $$
BEGIN
RETURN query
	SELECT
		*
		FROM
			product
		WHERE
			product.brand_id = brand;
END;$$;

-- Select all brands with the number of their products
CREATE OR REPLACE FUNCTION get_brands()
RETURNS TABLE (
    id integer, 
    name varchar, 
    product_count bigint)
LANGUAGE plpgsql
AS $$
BEGIN
RETURN query
	SELECT 
        brand.id, brand.name, count(product.id)
    FROM 
        brand LEFT JOIN product ON brand.id = product.brand_id 
    GROUP BY brand.id
    ORDER BY count(product.id) DESC;
END;$$;

-- Get all products for a given category and section.
CREATE OR REPLACE FUNCTION get_products_of_section(sect_id integer, categ_id integer)
RETURNS TABLE (
    id integer, 
    name varchar, 
    description varchar, 
    price numeric, 
    sex sex, 
    brand_id integer, 
    add_date timestamp, 
    category_id integer,
    section_id integer,
    quantity integer,
    image varchar)
LANGUAGE plpgsql
AS $$
BEGIN
RETURN query
	SELECT 
        *
    FROM 
        product 
    WHERE
        product.section_id=sect_id AND product.category_id=categ_id;
END;$$;


-- Get all completed orders with a given product. Order from newest to latest.
CREATE OR REPLACE FUNCTION get_completed_orders_of_product(prod_id integer)
RETURNS TABLE (
    id integer,
    user_id integer, 
    current_status status, 
    order_date timestamp)
LANGUAGE plpgsql
AS $$
BEGIN
RETURN query
	SELECT 
        customer_order.id,
		customer_order.user_id,
		customer_order.current_status,
		customer_order.order_date
    FROM 
       order_product LEFT JOIN customer_order ON product_id = prod_id
	WHERE customer_order.current_status = 'Completed'
    ORDER BY customer_order.order_date ASC;
END;$$;

-- Get all reviews for a given product. Implement this as a view table which contains rating, comment and info of a person who left a comment.
CREATE OR REPLACE FUNCTION get_reviews_of_product(prod_id integer)
RETURNS TABLE (
    rating integer,
    comment varchar, 
    phone varchar,
    email varchar,
    role role, 
    first_name varchar, 
    last_name varchar)
LANGUAGE plpgsql
AS $$
BEGIN
RETURN query
	SELECT 
        r.rating,
        r.review,
        u.phone,
        u.email,
        u.role,
        u.first_name,
        u.last_name
    FROM 
       review AS r LEFT JOIN user_account AS u ON user_id = u.id
	WHERE product_id=prod_id;
END;$$;