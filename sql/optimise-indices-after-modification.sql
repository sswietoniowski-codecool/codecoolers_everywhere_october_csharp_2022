use codecoolers;
go

-- w trakcie "¿ycia" bazy danych indeksy ulegaj¹ fragmentacji, aby przywróciæ im "optymalny" stan 
-- warto je cyklicznie przebudowywaæ (operacja generalnie blokuj¹ca), a co najmniej reorganizowaæ
-- (operacja online), kolejna (kluczowa!) sprawa to posiadanie aktualnych statystyk, te aktualizuj¹
-- siê same (przy odpowiednich ustawieniach dla bazy), ale nast¹pi to te¿ przy okazji przebudowy
-- indeksów

-- wykrywanie fragmentacji indeksów: https://docs.microsoft.com/en-us/sql/relational-databases/indexes/reorganize-and-rebuild-indexes?view=sql-server-ver15
-- przebudowa indeksów: 

alter index all on dbo.cities rebuild;
alter index all on dbo.codecoolers rebuild;
alter index all on dbo.codecoolers_schools rebuild;
alter index all on dbo.schools rebuild;

-- normalnie nie bêdziemy po prostu przebudowywaæ wszystkiego, zrobimy to inteligentniej, korzystaj¹c z dedykowanych skryptów, np. takich:
-- https://ola.hallengren.com/sql-server-index-and-statistics-maintenance.html