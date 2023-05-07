use codecoolers;
go

declare @LastName varchar(50);
declare @FirstName varchar(50);

set @LastName = 'Ware'
set @FirstName = 'Selena';

select
	*
from
	dbo.codecoolers
where
	last_name = @LastName
	and
	first_name = @FirstName;
go	

-- dla ambitnych chêtnych do poczytania i za darmo:
-- https://www.sqlservercentral.com/books/sql-server-execution-plans-third-edition-by-grant-fritchey

-- plan zapytania (w wersji tekstowej):
-- https://docs.microsoft.com/en-us/sql/t-sql/statements/set-showplan-text-transact-sql?view=sql-server-ver15

-- statystyki zapytania:
-- https://docs.microsoft.com/en-us/sql/t-sql/statements/set-statistics-io-transact-sql?view=sql-server-ver15
-- https://docs.microsoft.com/en-us/sql/t-sql/statements/set-statistics-time-transact-sql?view=sql-server-ver15

-- dla w/w zapytania na starcie mamy:

set showplan_text on
go

declare @LastName varchar(50);
declare @FirstName varchar(50);

set @LastName = 'Ware'
set @FirstName = 'Selena';

select
	*
from
	dbo.codecoolers
where
	last_name = @LastName
	and
	first_name = @FirstName;
go

set showplan_text off;
go	

set statistics io on;
set statistics time on;
go

declare @LastName varchar(50);
declare @FirstName varchar(50);

set @LastName = 'Ware'
set @FirstName = 'Selena';

select
	*
from
	dbo.codecoolers
where
	last_name = @LastName
	and
	first_name = @FirstName;
go

set statistics io off;
set statistics time off;
go

/*

Przed zmianami:

plan:

  |--Clustered Index Scan(OBJECT:([codecoolers].[dbo].[codecoolers].[PK__codecool__3213E83FF8E905B8]), WHERE:([codecoolers].[dbo].[codecoolers].[last_name]=[@LastName] AND [codecoolers].[dbo].[codecoolers].[first_name]=[@FirstName]))

statystyki:

SQL Server parse and compile time: 
   CPU time = 0 ms, elapsed time = 2 ms.

 SQL Server Execution Times:
   CPU time = 0 ms,  elapsed time = 0 ms.

 SQL Server Execution Times:
   CPU time = 0 ms,  elapsed time = 0 ms.

(1 row affected)
Table 'codecoolers'. Scan count 1, logical reads 4853, physical reads 1, read-ahead reads 4882, lob logical reads 0, lob physical reads 0, lob read-ahead reads 0.

(1 row affected)

 SQL Server Execution Times:
   CPU time = 203 ms,  elapsed time = 398 ms.
SQL Server parse and compile time: 
   CPU time = 0 ms, elapsed time = 0 ms.

 SQL Server Execution Times:
   CPU time = 0 ms,  elapsed time = 0 ms.

*/

create index IX_codecoolers_last_name_first_name on dbo.codecoolers(last_name, first_name);
go

/*

Po zmianach:

plan:

  |--Nested Loops(Inner Join, OUTER REFERENCES:([codecoolers].[dbo].[codecoolers].[id]))
       |--Index Seek(OBJECT:([codecoolers].[dbo].[codecoolers].[IX_codecoolers_last_name_first_name]), SEEK:([codecoolers].[dbo].[codecoolers].[last_name]=[@LastName] AND [codecoolers].[dbo].[codecoolers].[first_name]=[@FirstName]) ORDERED FORWARD)
       |--Clustered Index Seek(OBJECT:([codecoolers].[dbo].[codecoolers].[PK__codecool__3213E83FF8E905B8]), SEEK:([codecoolers].[dbo].[codecoolers].[id]=[codecoolers].[dbo].[codecoolers].[id]) LOOKUP ORDERED FORWARD)

SQL Server parse and compile time: 
   CPU time = 0 ms, elapsed time = 2 ms.

 SQL Server Execution Times:
   CPU time = 0 ms,  elapsed time = 0 ms.

 SQL Server Execution Times:
   CPU time = 0 ms,  elapsed time = 0 ms.

(1 row affected)
Table 'codecoolers'. Scan count 1, logical reads 6, physical reads 6, read-ahead reads 0, lob logical reads 0, lob physical reads 0, lob read-ahead reads 0.

(1 row affected)

 SQL Server Execution Times:
   CPU time = 0 ms,  elapsed time = 189 ms.
SQL Server parse and compile time: 
   CPU time = 0 ms, elapsed time = 0 ms.

 SQL Server Execution Times:
   CPU time = 0 ms,  elapsed time = 0 ms.

*/


-- lepsze narzêdzie do analizy planów zapytañ ni¿ to wbudowane Sentry Plan Explorer:
-- https://www.sentryone.com/plan-explorer