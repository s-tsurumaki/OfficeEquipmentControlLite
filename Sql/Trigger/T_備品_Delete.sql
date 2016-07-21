DROP TRIGGER dbo.T_備品_Delete
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
======================================================================
概要    ：備品を削除した場合、T_備品管理の全てのデータを削除します。
備考    ：
======================================================================
*/
CREATE TRIGGER dbo.T_備品_Delete ON dbo.T_備品
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

