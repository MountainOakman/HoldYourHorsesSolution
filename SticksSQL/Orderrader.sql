CREATE TABLE [dbo].[Orderrader]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [OrderId] INT NOT NULL REFERENCES Ordrar(id), 
    [ArtikelNr] INT NOT NULL REFERENCES Sticks(artikelNr), 
    [Antal] INT NOT NULL, 
    [Pris] MONEY NOT NULL
)
