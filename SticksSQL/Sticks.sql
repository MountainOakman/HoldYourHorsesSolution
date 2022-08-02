CREATE TABLE [dbo].[Sticks]
(
	[Artikelnr] INT NOT NULL PRIMARY KEY identity, 
    [Pris] MONEY NOT NULL, 
    [Hästkrafter] INT NOT NULL, 
    [Trädensitet] INT NOT NULL, 
    [Artikelnamn] NVARCHAR(50) NOT NULL unique, 
    [Material] NVARCHAR(50) NOT NULL, 
    [Typ] NVARCHAR(50) NOT NULL, 
    [Beskrivning] NVARCHAR(1000) NOT NULL, 
    [Tillverkningsland] NVARCHAR(50) NOT NULL, 
    [ABS broms] BIT NOT NULL
)
