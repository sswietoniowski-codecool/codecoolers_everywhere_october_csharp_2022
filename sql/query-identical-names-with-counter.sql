use codecoolers;
go

select last_name, first_name, count(*) as counter from dbo.codecoolers group by last_name, first_name order by count(*) desc;
go