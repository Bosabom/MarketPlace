create or replace function get_marketplace(jsonstring json)
returns json
language plpgsql
as
$$
DECLARE
new_json json;
begin
SELECT row_to_json(marketplaces) FROM marketplaces INTO new_json WHERE marketplace_id=(jsonstring->>'marketplace_id')::integer;
return new_json;
end;
$$;
