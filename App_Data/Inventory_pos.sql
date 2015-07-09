USE [GentleParkHO]
GO

/****** Object:  Table [dbo].[Pos_Inventory]    Script Date: 09/14/2014 12:47:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pos_Inventory](
	[InventoryID] [int] IDENTITY(1,1) NOT NULL,
	[WorkStationID] [int] NULL,
	[InventoryDate] [datetime] NULL,
	[AddedBy] [int] NULL,
	[AddedDate] [datetime] NULL,
	[ExtraField1] [nvarchar](256) NULL,
	[ExtraField2] [nvarchar](256) NULL,
	[ExtraField3] [nvarchar](256) NULL,
	[ExtraField4] [nvarchar](256) NULL,
	[ExtraField5] [nvarchar](256) NULL
) ON [PRIMARY]

GO


