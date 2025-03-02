CREATE OR REPLACE FUNCTION json_array_castbigint(json) RETURNS bigint[] AS $f$
    SELECT array_agg(x)::bigint[] || ARRAY[]::bigint[] FROM json_array_elements_text($1) t(x);
$f$ LANGUAGE sql IMMUTABLE;