create or replace function update_marketplace(jsonstring json)
returns json
language plpgsql
as
$$
DECLARE
new_json json;
begin
UPDATE marketplaces
SET name = (jsonstring->>'name')::text, image_id = (jsonstring->>'image_id')::bigint, products_id = json_array_castbigint(jsonstring->'products_id')
WHERE marketplace_id=(jsonstring->>'marketplace_id')::integer;
SELECT row_to_json(marketplaces) FROM marketplaces INTO new_json WHERE marketplace_id=(jsonstring->>'marketplace_id')::integer;
return new_json;
end;
$$;