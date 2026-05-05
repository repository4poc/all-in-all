CREATE DATABASE world;

CREATE TABLE capitals (
id SERIAL PRIMARY KEY,
country VARCHAR(45),
capital VARCHAR(45)
);

SELECT * FROM capitals;

/*
Import the base data into tables.
COPY capitals FROM '/server/path/file.csv' DELIMITER ',' CSV HEADER;
*/

CREATE TABLE flags(
id SERIAL PRIMARY KEY,
name VARCHAR(45),
flag TEXT
);

