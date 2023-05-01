CREATE SCHEMA IF NOT EXISTS clothing_store;
SET search_path = clothing_store;

CREATE TYPE country AS ENUM ('Belarus', 'USA', 'France', 'Poland');
CREATE TYPE role AS ENUM ('Admin', 'Customer');
CREATE TYPE status AS ENUM ('In review', 'In delivery', 'Completed');
CREATE TYPE sex AS ENUM ('Male', 'Female', 'Unisex');

CREATE TABLE IF NOT EXISTS section (
  id SERIAL,
  name varchar(50) NOT NULL UNIQUE,
  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS category (
  id SERIAL,
  name varchar(50) NOT NULL,
  parent_category_id integer,
  PRIMARY KEY (id),
  CONSTRAINT fk_category_parent_category_id
    FOREIGN KEY (parent_category_id)
      REFERENCES category(id) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS section_category (
  id SERIAL,
  section_id integer NOT NULL,
  category_id integer NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_section_category_section_id
    FOREIGN KEY (section_id)
      REFERENCES section(id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT fk_section_category_category_id
    FOREIGN KEY (category_id)
      REFERENCES category(id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS brand (
  id SERIAL,
  name varchar(50) NOT NULL UNIQUE,
  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS product (
  id SERIAL,
  name varchar(100) NOT NULL,
  description varchar(500) NOT NULL,
  price numeric NOT NULL CHECK (price > 0),
  sex sex NOT NULL,
  brand_id integer NOT NULL,
  add_date timestamp NOT NULL,
  category_id integer NOT NULL,
  section_id integer NOT NULL,
  quantity integer NOT NULL CHECK (quantity >= 0),
  image varchar(500) NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_product_category_id
    FOREIGN KEY (category_id)
      REFERENCES category(id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_product_section_id
    FOREIGN KEY (section_id)
      REFERENCES section(id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_product_brand_id
    FOREIGN KEY (brand_id)
      REFERENCES brand(id) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS user_account (
  id SERIAL,
  phone varchar(20) UNIQUE,
  email varchar(100) NOT NULL UNIQUE,
  password varchar(64) NOT NULL,
  role role NOT NULL,
  first_name varchar(50),
  last_name varchar(50),
  PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS address (
  user_id integer,
  country country NOT NULL,
  district varchar(50) NOT NULL,
  city varchar(50) NOT NULL,
  postcode varchar(10) NOT NULL,
  address_line_1 varchar(50) NOT NULL,
  address_line_2 varchar(20),
  PRIMARY KEY (user_id),
  CONSTRAINT fk_address_user_id
    FOREIGN KEY (user_id)
      REFERENCES user_account(id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS review (
  id SERIAL,
  product_id integer NOT NULL,
  user_id integer NOT NULL,
  review_title varchar(50) NOT NULL,
  review varchar(500) NOT NULL,
  rating integer NOT NULL CHECK (rating >= 0 AND rating <= 5),
  add_date timestamp NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_review_product_id
    FOREIGN KEY (product_id)
      REFERENCES product(id) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT fk_review_user_id
    FOREIGN KEY (user_id)
      REFERENCES user_account(id) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS customer_order (
  id SERIAL,
  user_id integer NOT NULL,
  order_date timestamp NOT NULL,
  current_status status NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_order_user_id
    FOREIGN KEY (user_id)
      REFERENCES user_account(id) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS order_product (
  id SERIAL,
  product_id integer NOT NULL,
  order_id integer NOT NULL,
  quantity integer NOT NULL CHECK (quantity > 0),
  price numeric NOT NULL CHECK (price > 0),
  PRIMARY KEY (id),
  CONSTRAINT fk_order_product_order_id
    FOREIGN KEY (order_id)
      REFERENCES customer_order(id) ON DELETE RESTRICT ON UPDATE CASCADE,
  CONSTRAINT fk_order_product_product_id
    FOREIGN KEY (product_id)
      REFERENCES product(id) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS order_history (
  id SERIAL,
  order_id integer NOT NULL,
  status status NOT NULL,
  date timestamp NOT NULL,
  PRIMARY KEY (id),
  CONSTRAINT fk_order_history_order_id
    FOREIGN KEY (order_id)
      REFERENCES customer_order(id) ON DELETE CASCADE ON UPDATE CASCADE
);