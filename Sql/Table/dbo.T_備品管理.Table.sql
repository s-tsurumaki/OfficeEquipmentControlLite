USE [OfficeEquipmentControl]
GO
ALTER TABLE [dbo].[T_備品管理] DROP CONSTRAINT [FK_T_備品管理_T_備品]
GO
ALTER TABLE [dbo].[T_備品管理] DROP CONSTRAINT [FK_T_備品管理_T_状態]
GO
DROP TABLE [dbo].[T_備品管理]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_備品管理](
	[備品管理ID] [int] IDENTITY(1,1) NOT NULL,
	[備品ID] [int] NOT NULL,
	[状態ID] [int] NOT NULL,
	[利用者名] [nvarchar](256) NULL,
	[貸出返却日] [datetime] NULL,
	[TS] [timestamp] NOT NULL,
	[更新日] [datetime] NOT NULL,
	[更新者] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_T_備品管理] PRIMARY KEY CLUSTERED 
(
	[備品管理ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 100) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[T_備品管理]  WITH CHECK ADD  CONSTRAINT [FK_T_備品管理_T_状態] FOREIGN KEY([状態ID])
REFERENCES [dbo].[T_状態] ([状態ID])
GO
ALTER TABLE [dbo].[T_備品管理] CHECK CONSTRAINT [FK_T_備品管理_T_状態]
GO
ALTER TABLE [dbo].[T_備品管理]  WITH CHECK ADD  CONSTRAINT [FK_T_備品管理_T_備品] FOREIGN KEY([備品ID])
REFERENCES [dbo].[T_備品] ([備品ID])
GO
ALTER TABLE [dbo].[T_備品管理] CHECK CONSTRAINT [FK_T_備品管理_T_備品]
GO
