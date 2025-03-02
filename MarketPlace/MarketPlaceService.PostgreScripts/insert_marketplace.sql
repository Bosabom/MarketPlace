create or replace function insert_marketplace(jsonstring json)
returns json
language plpgsql
as
$$
DECLARE
new_id integer;
new_json json;
begin
INSERT INTO marketplaces (name,image_id,products_id)
VALUES((jsonstring->>'name')::text,(jsonstring->>'image_id')::bigint,json_array_castbigint(jsonstring->'products_id'));
SELECT currval(pg_get_serial_sequence('marketplaces','marketplace_id')) INTO new_id;
SELECT row_to_json(marketplaces) FROM marketplaces INTO new_json WHERE marketplace_id=new_id;
return new_json;
end;
$$;