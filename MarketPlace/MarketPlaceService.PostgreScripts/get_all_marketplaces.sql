create or replace function get_all_marketplaces()
returns json
language plpgsql
as
$$
DECLARE
new_json json;
begin
SELECT to_jsonb(array_agg(marketplaces)) FROM marketplaces INTO new_json;
return new_json;
end;
$$;
