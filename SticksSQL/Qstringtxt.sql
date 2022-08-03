
--Tömmer den förra tabellen. Behövdes göra innan publicering av uppdaterade tabeller.
truncate table sticks

--Sätter in värden i tabellerna
insert into Material(Namn)
values
('Furu'), ('Ek'), ('Mahogony')
go

insert into Kategorier(Namn)
values
('Sport'), ('Fritid'), ('Barn')
go

insert into Tillverkningsländer(Namn)
values
('Sverige'), ('Norge'), ('Danmark'), ('Norge')
go

insert into Sticks (Pris, Hästkrafter, Trädensitet, Artikelnamn, MaterialId, KategoriId, Beskrivning, TillverkningslandId, AbsBroms)
values
(1337, 250, 230,'Cloudberry Castle Inzanity', 2, 1, 'Riktigt vacker häst', 1, 1),
(1200, 180, 160,'Cloudberry Castle Budget', 1, 2, 'Fritidshäst',2, 0)
go

--visa tabellerna
select * from Material
select * from Kategorier
select * from Tillverkningsländer

select 
	Artikelnr,
	Pris,
	Hästkrafter,
	Trädensitet,
	Artikelnamn,
	material.id as MaterialId,
	material.Namn as MaterialNamn,
	Kategorier.id as KategoriId,
	kategorier.namn as KategoriNamN,
	Tillverkningsländer.namn as TillverkningslandNamn,
	Tillverkningsländer.Id as TillverkningslandId
from Sticks
left join Material on sticks.MaterialId=material.Id
left join Kategorier on sticks.KategoriId=Kategorier.Id
left join Tillverkningsländer on sticks.TillverkningslandId = Tillverkningsländer.Id