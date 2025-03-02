create or replace function delete_marketplace(jsonstring json)
returns json
language plpgsql
as
$$
DECLARE
new_json json;
begin
WITH d as (DELETE FROM marketplaces WHERE marketplace_id=(jsonstring->>'marketplace_id')::integer RETURNING *) SELECT * FROM d INTO new_json;
return new_json;
end;
$$;
