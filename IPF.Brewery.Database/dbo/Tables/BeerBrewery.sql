CREATE TABLE [dbo].[BeerBrewery]
(
	[BreweryId] INT NOT NULL,
	[BeerId] INT NOT NULL
	CONSTRAINT [PK_BeerBrewery] PRIMARY KEY CLUSTERED 
	(
		[BreweryId] ASC,
		[BeerId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BeerBrewery]  WITH CHECK ADD  CONSTRAINT [FK_BeerBrewery_BeerId] FOREIGN KEY([BeerId])
REFERENCES [dbo].[Beer] ([Id])
GO

ALTER TABLE [dbo].[BeerBrewery] CHECK CONSTRAINT [FK_BeerBrewery_BeerId]
GO

ALTER TABLE [dbo].[BeerBrewery]  WITH CHECK ADD  CONSTRAINT [FK_BeerBrewery_BreweryId] FOREIGN KEY([BreweryId])
REFERENCES [dbo].[Brewery] ([Id])
GO

ALTER TABLE [dbo].[BeerBrewery] CHECK CONSTRAINT [FK_BeerBrewery_BreweryId]
GO
