use codecoolers;
go

-- w trakcie "�ycia" bazy danych indeksy ulegaj� fragmentacji, aby przywr�ci� im "optymalny" stan 
-- warto je cyklicznie przebudowywa� (operacja generalnie blokuj�ca), a co najmniej reorganizowa�
-- (operacja online), kolejna (kluczowa!) sprawa to posiadanie aktualnych statystyk, te aktualizuj�
-- si� same (przy odpowiednich ustawieniach dla bazy), ale nast�pi to te� przy okazji przebudowy
-- indeks�w

-- wykrywanie fragmentacji indeks�w: https://docs.microsoft.com/en-us/sql/relational-databases/indexes/reorganize-and-rebuild-indexes?view=sql-server-ver15
-- przebudowa indeks�w: 

alter index all on dbo.cities rebuild;
alter index all on dbo.codecoolers rebuild;
alter index all on dbo.codecoolers_schools rebuild;
alter index all on dbo.schools rebuild;

-- normalnie nie b�dziemy po prostu przebudowywa� wszystkiego, zrobimy to inteligentniej, korzystaj�c z dedykowanych skrypt�w, np. takich:
-- https://ola.hallengren.com/sql-server-index-and-statistics-maintenance.html