USE [OfficeEquipmentControl]
GO
DROP TABLE [dbo].[T_備品]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_備品](
	[備品ID] [int] IDENTITY(1,1) NOT NULL,
	[品名] [nvarchar](200) NOT NULL,
	[型番] [nvarchar](200) NULL,
	[備考] [nvarchar](200) NULL,
	[TS] [timestamp] NOT NULL,
	[更新日] [datetime] NOT NULL,
	[更新者] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_T_備品] PRIMARY KEY CLUSTERED 
(
	[備品ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100) ON [PRIMARY]
) ON [PRIMARY]

GO
