CREATE TABLE [dbo].[Beer]
(
	[Id] INT PRIMARY KEY IDENTITY(1,1),
	[BeerName] VARCHAR(50) NOT NULL,
	[PercentageAlcoholByVolume] DECIMAL(16,2) NOT NULL,
	[BeerTypeId] INT NOT NULL

	CONSTRAINT [FK_Beer_BeerTypeId] FOREIGN KEY ([BeerTypeId]) 
         REFERENCES [dbo].[BeerType]([Id])
)
