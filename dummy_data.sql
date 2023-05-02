-- FILLING SECTION TABLE
INSERT INTO section (name)
VALUES ('Men''s'),
	('Women''s');
-- FILLING CATEGORY TABLE
INSERT INTO category (name)
VALUES ('Dresses & Jumpsuits'),
	('Shirts');
-- ADDING SUBCATEGORIES
INSERT INTO category (name, parent_category_id)
SELECT 'Mini Dresses',
	id
FROM category
WHERE name = 'Dresses & Jumpsuits';
-- FILLING SECTION_CATEGORY TABLE
INSERT INTO section_category (section_id, category_id)
SELECT (
		SELECT id
		FROM section
		WHERE name = 'Men''s'
	),
	id
FROM category
WHERE name = 'Shirts';
INSERT INTO section_category (section_id, category_id)
SELECT (
		SELECT id
		FROM section
		WHERE name = 'Women''s'
	),
	id
FROM category
WHERE name = 'Dresses & Jumpsuits';
INSERT INTO section_category (section_id, category_id)
SELECT (
		SELECT id
		FROM section
		WHERE name = 'Women''s'
	),
	id
FROM category
WHERE name = 'Mini Dresses';
-- FILLING BRAND TABLE
INSERT INTO brand (name)
VALUES ('GUCCI'),
	('Adidas'),
	('Tommy Hilfiger'),
	('Zara');
-- FILLING PRODUCT TABLE
INSERT INTO product (
		name,
		description,
		price,
		brand_id,
		add_date,
		section_category_id,
		quantity,
		image
	)
SELECT 'GUCCI Daisy Dotty Mini Dress',
	'Flocked mesh mini dress by GUCCI, complete with a cute dotty design in the frilly details. Features a sweetheart neck, frilly cap sleeves, a fitted bodice, asymmetrical frill seams through the body and a mini hem.',
	100.00,
	id,
	now(),
	(
		SELECT sc.id
		FROM section_category AS sc
			INNER JOIN category AS c ON sc.category_id = c.id
			INNER JOIN section AS s ON sc.section_id = s.id
		WHERE c.name = 'Mini Dresses'
			AND s.name = 'Women''s'
	),
	100,
	'https://imageseu.urbndata.com/is/image/UrbanOutfittersEU/0180950660010_066_b?$xlarge$&fit=constrain&fmt=webp&qlt=80&wid=720'
FROM brand
WHERE name = 'GUCCI';
INSERT INTO product (
		name,
		description,
		price,
		brand_id,
		add_date,
		section_category_id,
		quantity,
		image
	)
SELECT 'Tommy Hulfiger Stone Green Utility Shirt',
	'Utility-ready hike shirt by Columbia, a durable ripstop style. Features a spread collar, short sleeves, two patch pockets at the front and a press-stud placket. Complete with a logo badge to chest.',
	65.00,
	id,
	now(),
	(
		SELECT sc.id
		FROM section_category AS sc
			INNER JOIN category AS c ON sc.category_id = c.id
			INNER JOIN section AS s ON sc.section_id = s.id
		WHERE c.name = 'Shirts'
			AND s.name = 'Men''s'
	),
	322,
	'https://imageseu.urbndata.com/is/image/UrbanOutfittersEU/0215461240015_030_b?$xlarge$&fit=constrain&fmt=webp&qlt=80&wid=720'
FROM brand
WHERE name = "Tommy Hilfiger";
INSERT INTO product (
		name,
		description,
		price,
		brand_id,
		add_date,
		section_category_id,
		quantity,
		image
	)
SELECT 'Adidas Green Cactus Short Sleeve Shirt',
	'Add fun motifs to your wardrobe with this short sleeve shirt. Complete with a spread collar, button-through placket, drop shoulders and short sleeves. Topped with a vibrant cactus motif all over.',
	39.00,
	id,
	now(),
	(
		SELECT sc.id
		FROM section_category AS sc
			INNER JOIN category AS c ON sc.category_id = c.id
			INNER JOIN section AS s ON sc.section_id = s.id
		WHERE c.name = 'Shirts'
			AND s.name = 'Men''s'
	),
	60,
	'https://imageseu.urbndata.com/is/image/UrbanOutfittersEU/0215911400128_030_b?$xlarge$&fit=constrain&fmt=webp&qlt=80&wid=720'
FROM brand
WHERE name = 'Adidas';
-- FILLING USER_ACCOUNT TABLE
INSERT INTO user_account (email, password, role, first_name, last_name)
VALUES (
		'bobbyjhon@gmail.com',
		'65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5',
		'Admin',
		'Bobby',
		'Jhon'
	);
INSERT INTO user_account (
		phone,
		email,
		password,
		role,
		first_name,
		last_name
	)
VALUES (
		'+375336441656',
		'maryjane@gmail.com',
		'65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5',
		'Customer',
		'Mary',
		'Jain'
	);
-- FILLING ADDRESS TABLE
INSERT INTO address (
		user_id,
		country,
		district,
		city,
		postcode,
		address_line_1,
		address_line_2
	)
SELECT id,
	'Belarus',
	'Gomelskaya oblast',
	'Mozyr',
	'247760',
	'bulvar Junosti',
	'dom 120'
FROM user_account
WHERE email = 'maryjane@gmail.com';
-- FILLING REVIEW TABLE
INSERT INTO review (
		product_id,
		user_id,
		review_title,
		review,
		rating,
		add_date
	)
SELECT id,
	(
		SELECT id
		FROM user_account
		WHERE email = 'maryjane@gmail.com'
	),
	'Perfect dress',
	'Very Flattering and Pop up color',
	5,
	now()
FROM product
WHERE name = 'GUCCI Daisy Dotty Mini Dress';
-- FILLING CUSTOMER_ORDER TABLE
INSERT INTO customer_order (user_id, current_status, order_date)
SELECT id,
	'In review',
	now()
FROM user_account
WHERE email = 'maryjane@gmail.com';
UPDATE customer_order
SET current_status = 'In delivery'
FROM customer_order AS c
	LEFT JOIN user_account AS u ON c.user_id = u.id
WHERE u.email = 'maryjane@gmail.com';
UPDATE customer_order
SET current_status = 'Completed'
FROM customer_order AS c
	LEFT JOIN user_account AS u ON c.user_id = u.id
WHERE u.email = 'maryjane@gmail.com';
-- FILLING ORDER_PRODUCT TABLE
INSERT INTO order_product (product_id, order_id, quantity, price)
SELECT id,
	(
		SELECT customer_order.id
		FROM customer_order
			LEFT JOIN user_account AS user_acc ON user_id = user_acc.id
			AND user_acc.email = 'maryjane@gmail.com'
	),
	1,
	price
FROM product
WHERE name = 'GUCCI Daisy Dotty Mini Dress';