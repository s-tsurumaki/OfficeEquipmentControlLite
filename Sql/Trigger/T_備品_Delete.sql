USE [test備品管理]
GO

/****** Object:  Trigger [T_備品_Delete]    Script Date: 2016/07/13 9:34:45 ******/
DROP TRIGGER [dbo].[T_備品_Delete]
GO

/****** Object:  Trigger [dbo].[T_備品_Delete]    Script Date: 2016/07/13 9:34:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[T_備品_Delete] ON [dbo].[T_備品] 
FOR  DELETE
AS

	-- ■■■■■■■
	-- 変数定義
	-- ■■■■■■■
    DECLARE @備品ID INT;

	SELECT
	    @備品ID = 備品ID
	FROM deleted

	DELETE FROM T_備品管理 WHERE 備品ID = @備品ID;


GO

