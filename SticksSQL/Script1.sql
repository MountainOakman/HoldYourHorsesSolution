
--Tömmer den förra tabellen. Behövdes göra innan publicering av uppdaterade tabeller.
--truncate table Tillverkningsländer

--Sätter in värden i tabellerna


insert into Material(Namn)
values
('Furu'), ('Ek'), ('Mahogony'), ('Gran')
go

insert into Kategorier(Namn)
values
('Sport'), ('Fritid'), ('Barn')
go

insert into Tillverkningsländer(Namn)
values
('Sverige'), ('Norge'), ('Danmark'), ('Finland')
go

insert into Sticks (Pris, Hästkrafter, Trädensitet, Artikelnamn, MaterialId, KategoriId, Beskrivning, TillverkningslandId, AbsBroms)
values
(1337, 250, 230,'Cloudberry Castle Inzanity', 2, 1, 'Riktigt vacker häst', 1, 1),
(1200, 180, 260,'Cloudberry Castle Budget', 1, 2, 'Fritidshäst',2, 0),
(1100, 350, 260,'Pegasus Sea Lake', 3, 3, 'Lyxhäst',4, 1),
(1800, 370, 200, 'Italian Stallion', 4, 1, 'Snabbar än Rocky', 2, 1),
(1500, 300, 350, 'Kingdra the Scaler', 1, 3, 'Simmar snabbt i vatten', 4, 0),
(1423, 300, 350, 'The Cimarron Spirit', 4, 3, 'Druknar snabbt i vatten och lera', 4, 0), --samma stats som Kingdra med flit
(1630, 400, 200, 'Secretariat', 3, 2, 'Världens mest kända häst', 3, 1),
(900, 150, 200, 'SkyScraper Horsea', 1,1, 'Längre än Empire State Byggnaden i New York', 1,1)
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
	kategorier.namn as KategoriNamn,
	Tillverkningsländer.namn as TillverkningslandNamn,
	Tillverkningsländer.Id as TillverkningslandId
from Sticks
left join Material on sticks.MaterialId=material.Id
left join Kategorier on sticks.KategoriId=Kategorier.Id
left join Tillverkningsländer on sticks.TillverkningslandId = Tillverkningsländer.Id
go

--Ersätta en Norge (blev dubbelt) med Finland
Update
	Tillverkningsländer
Set 
	Namn = 'Finland'
where
	id = 4

	-- om man vill radera alla poster i Sticks
	delete from Sticks 
	--om man vill radera alla poster i Material
	delete from Material 