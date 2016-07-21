EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Common', @level2type=N'COLUMN',@level2name=N'MainURL'
GO
DROP TABLE dbo.T_Common
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE dbo.T_Common(
    MainURL nvarchar(256) NOT NULL,
    OneEditURL nvarchar(256) NOT NULL
) ON PRIMARY
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'このテーブルは必ず1行だけにしてください。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Common', @level2type=N'COLUMN',@level2name=N'MainURL'
GO
