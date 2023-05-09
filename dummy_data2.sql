set search_path = clothing_store;
-- FILLING SECTION TABLE
	INSERT INTO sections (name)
	VALUES ('Men''s'),
		('Women''s');
	-- FILLING CATEGORY TABLE
	INSERT INTO categories (name)
	VALUES ('Dresses & Jumpsuits'),
		('Shirts');
	-- ADDING SUBCATEGORIES
	INSERT INTO categories (name, parent_category_id)
	SELECT 'Mini Dresses',
		id
	FROM categories
	WHERE name = 'Dresses & Jumpsuits';
	-- FILLING SECTION_CATEGORY TABLE
	INSERT INTO section_categories (section_id, category_id)
	SELECT (
			SELECT id
			FROM sections
			WHERE name = 'Men''s'
		),
		id
	FROM categories
	WHERE name = 'Shirts';
	INSERT INTO section_categories (section_id, category_id)
	SELECT (
			SELECT id
			FROM sections
			WHERE name = 'Women''s'
		),
		id
	FROM categories
	WHERE name = 'Dresses & Jumpsuits';
	INSERT INTO section_categories (section_id, category_id)
	SELECT (
			SELECT id
			FROM sections
			WHERE name = 'Women''s'
		),
		id
	FROM categories
	WHERE name = 'Mini Dresses';
	-- FILLING BRAND TABLE
	INSERT INTO brands (name)
	VALUES ('GUCCI'),
		('Adidas'),
		('Tommy Hilfiger'),
		('Zara');
	-- FILLING PRODUCT TABLE
	INSERT INTO products (
			name,
			description,
			price,
			brand_id,
			add_date,
			section_category_id,
			quantity,
			image_url
		)
	SELECT 'GUCCI Daisy Dotty Mini Dress',
		'Flocked mesh mini dress by GUCCI, complete with a cute dotty design in the frilly details. Features a sweetheart neck, frilly cap sleeves, a fitted bodice, asymmetrical frill seams through the body and a mini hem.',
		100.00,
		id,
		now(),
		(
			SELECT sc.id
			FROM section_categories AS sc
				INNER JOIN categories AS c ON sc.category_id = c.id
				INNER JOIN sections AS s ON sc.section_id = s.id
			WHERE c.name = 'Mini Dresses'
				AND s.name = 'Women''s'
		),
		100,
		'https://imageseu.urbndata.com/is/image/UrbanOutfittersEU/0180950660010_066_b?$xlarge$&fit=constrain&fmt=webp&qlt=80&wid=720'
	FROM brands
	WHERE name = 'GUCCI';
	INSERT INTO products (
			name,
			description,
			price,
			brand_id,
			add_date,
			section_category_id,
			quantity,
			image_url
		)
	SELECT 'Tommy Hulfiger Stone Green Utility Shirt',
		'Utility-ready hike shirt by Columbia, a durable ripstop style. Features a spread collar, short sleeves, two patch pockets at the front and a press-stud placket. Complete with a logo badge to chest.',
		65.00,
		id,
		now(),
		(
			SELECT sc.id
			FROM section_categories AS sc
				INNER JOIN categories AS c ON sc.category_id = c.id
				INNER JOIN sections AS s ON sc.section_id = s.id
			WHERE c.name = 'Shirts'
				AND s.name = 'Men''s'
		),
		322,
		'https://imageseu.urbndata.com/is/image/UrbanOutfittersEU/0215461240015_030_b?$xlarge$&fit=constrain&fmt=webp&qlt=80&wid=720'
	FROM brands
	WHERE name = 'Tommy Hilfiger';
	INSERT INTO products (
			name,
			description,
			price,
			brand_id,
			add_date,
			section_category_id,
			quantity,
			image_url
		)
	SELECT 'Adidas Green Cactus Short Sleeve Shirt',
		'Add fun motifs to your wardrobe with this short sleeve shirt. Complete with a spread collar, button-through placket, drop shoulders and short sleeves. Topped with a vibrant cactus motif all over.',
		39.00,
		id,
		now(),
		(
			SELECT sc.id
			FROM section_categories AS sc
				INNER JOIN categories AS c ON sc.category_id = c.id
				INNER JOIN sections AS s ON sc.section_id = s.id
			WHERE c.name = 'Shirts'
				AND s.name = 'Men''s'
		),
		60,
		'https://imageseu.urbndata.com/is/image/UrbanOutfittersEU/0215911400128_030_b?$xlarge$&fit=constrain&fmt=webp&qlt=80&wid=720'
	FROM brands
	WHERE name = 'Adidas';
	-- FILLING USER_ACCOUNT TABLE
	INSERT INTO users (email, password, role, first_name, last_name)
	VALUES (
			'bobbyjohn@gmail.com',
			'65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5',
			0,
			'Bobby',
			'John'
		);
	INSERT INTO users (
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
			1,
			'Mary',
			'Jain'
		);
	-- FILLING ADDRESS TABLE
	INSERT INTO addresses (
			user_id,
			country,
			district,
			city,
			postcode,
			address_line1,
			address_line2
		)
	SELECT id,
		1,
		'Gomelskaya oblast',
		'Mozyr',
		'247760',
		'bulvar Junosti',
		'dom 120'
	FROM users
	WHERE email = 'maryjane@gmail.com';
	-- FILLING REVIEW TABLE
	INSERT INTO reviews (
			product_id,
			user_id,
			review_title,
			comment,
			rating,
			add_date
		)
	SELECT id,
		(
			SELECT id
			FROM users
			WHERE email = 'maryjane@gmail.com'
		),
		'Perfect dress',
		'Very Flattering and Pop up color',
		5,
		now()
	FROM products
	WHERE name = 'GUCCI Daisy Dotty Mini Dress';
	-- FILLING CUSTOMER_ORDER TABLE
	INSERT INTO customer_orders (user_id, current_status, order_date)
	SELECT id,
		0,
		now()
	FROM users
	WHERE email = 'maryjane@gmail.com';
	UPDATE customer_orders
	SET current_status = 1
	FROM customer_orders AS c
		LEFT JOIN users AS u ON c.user_id = u.id
	WHERE u.email = 'maryjane@gmail.com';
	UPDATE customer_orders
	SET current_status = 2
	FROM customer_orders AS c
		LEFT JOIN users AS u ON c.user_id = u.id
	WHERE u.email = 'maryjane@gmail.com';
	-- FILLING ORDER_PRODUCT TABLE
	INSERT INTO order_products (product_id, order_id, quantity, price)
	SELECT id,
		(
			SELECT customer_orders.id
			FROM customer_orders
				LEFT JOIN users AS user_acc ON user_id = user_acc.id
				AND user_acc.email = 'maryjane@gmail.com'
		),
		1,
		price
	FROM products
	WHERE name = 'GUCCI Daisy Dotty Mini Dress';