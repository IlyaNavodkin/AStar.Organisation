CREATE TABLE customer (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    phone DECIMAL
);

CREATE TABLE product (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    price DECIMAL
);

CREATE TABLE cart (
    id SERIAL PRIMARY KEY,
    customerId INT NOT NULL REFERENCES customer(id)
);

CREATE TABLE cartproduct (
    id SERIAL PRIMARY KEY,
    productid INT NOT NULL REFERENCES product(id),
    cartid INT NOT NULL REFERENCES cart(id)
);

CREATE TABLE productphoto (
    id SERIAL PRIMARY KEY,
    url VARCHAR(255) NOT NULL,
    productid INT NOT NULL REFERENCES product(id)
);

INSERT INTO customer (name, email, phone)
VALUES
    ('Иванов Иван', 'ivanovivan@example.com', '88999993323'),
    ('Олег Монгол', 'mongol22@example.com', '880055535'),
    ('Василиса Иордановка', 'vsilokl@example.com', '89990734543');

INSERT INTO product (name, description, price)
VALUES
    ('Шапка ушанка', 'Теплая и мягкая', 25.98),
    ('Куртка черная', 'Для ценителей моды', 45.28);

INSERT INTO cart (customerId)
VALUES
    (1),
    (2);

INSERT INTO cartproduct (productid, cartid)
VALUES
    (1,1),
    (1,2),
    (2,1);

INSERT INTO productphoto (url, productid)
VALUES
    ('sample.com/shapkaushanka',1),
    ('sample.com/blackkurtka',1);