use codecoolers;
go

select 
	c.id, last_name, first_name, birth_year, birth_city_id
from
	dbo.codecoolers as c
	inner join
	dbo.codecoolers_schools as cs
	on (c.id = cs.codecooler_id)
	inner join
	dbo.schools as s
	on (s.id = cs.school_id)
where
	c.birth_city_id <> s.city_id;
go

-- pr�ba pobrania wszystkich rekord�w b�dzie karko�omna, jest ich za du�o, �eby sprawdzi� efektywno��
-- zapytania z negacj� po prostu policzmy takich student�w

select 
	count(*)
from
	dbo.codecoolers as c
	inner join
	dbo.codecoolers_schools as cs
	on (c.id = cs.codecooler_id)
	inner join
	dbo.schools as s
	on (s.id = cs.school_id)
where
	c.birth_city_id <> s.city_id;
go

-- 4999800