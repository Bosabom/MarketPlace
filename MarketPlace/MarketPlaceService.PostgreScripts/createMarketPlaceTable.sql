CREATE TABLE public."marketplaces"
(
	marketplace_id serial PRIMARY KEY,
	name text UNIQUE NOT NULL,
	image_id bigint NOT NULL,
	products_id bigint[] NOT NULL
)