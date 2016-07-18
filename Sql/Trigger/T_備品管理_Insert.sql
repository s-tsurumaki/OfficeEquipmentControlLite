USE [test備品管理]
GO

/****** Object:  Trigger [T_備品管理_Insert]    Script Date: 2016/07/13 9:34:09 ******/
DROP TRIGGER [dbo].[T_備品管理_Insert]
GO

/****** Object:  Trigger [dbo].[T_備品管理_Insert]    Script Date: 2016/07/13 9:34:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE TRIGGER [dbo].[T_備品管理_Insert] ON [dbo].[T_備品管理] 
FOR  INSERT
AS

	-- ■■■■■■■
	-- 変数定義
	-- ■■■■■■■
    DECLARE @備品ID INT;
    DECLARE @状態ID INT;
	DECLARE @更新日 datetime;
    DECLARE @更新者 nvarchar(256);

	SELECT
	    @備品ID = 備品ID
	   ,@状態ID = 状態ID
	   ,@更新日 = 更新日
	   ,@更新者 = 更新者
	FROM inserted

	IF (@状態ID = 3)
	BEGIN
		INSERT INTO [dbo].[T_備品管理]
				   ([備品ID]
				   ,[状態ID]
				   ,[利用者名]
				   ,[貸出返却日]
				   ,[更新日]
				   ,[更新者])
		VALUES
			(@備品ID
			,1
			,NULL
			,NULL
			,@更新日
			,@更新者
			)
	END


GO

