﻿CREATE TABLE [dbo].[BarBeer]
(
	[BarId] INT NOT NULL,
	[BeerId] INT NOT NULL
	CONSTRAINT [PK_BarBeer] PRIMARY KEY CLUSTERED 
	(
		[BarId] ASC,
		[BeerId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BarBeer]  WITH CHECK ADD  CONSTRAINT [FK_BarBeer_BeerId] FOREIGN KEY([BeerId])
REFERENCES [dbo].[Beer] ([Id])
GO

ALTER TABLE [dbo].[BarBeer] CHECK CONSTRAINT [FK_BarBeer_BeerId]
GO

ALTER TABLE [dbo].[BarBeer]  WITH CHECK ADD  CONSTRAINT [FK_BarBeer_BarId] FOREIGN KEY([BarId])
REFERENCES [dbo].[Bar] ([Id])
GO

ALTER TABLE [dbo].[BarBeer] CHECK CONSTRAINT [FK_BarBeer_BarId]
GO
