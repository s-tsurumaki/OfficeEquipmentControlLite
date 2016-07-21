DROP TRIGGER dbo.T_備品_Insert
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
======================================================================
概要    ：備品を追加した場合、追加した備品をT_備品管理に
        ：貸出可能として登録します。
備考    ：
======================================================================
*/
CREATE TRIGGER dbo.T_備品_Insert ON dbo.T_備品
FOR  INSERT
AS
    -- ■■■■■■■
    -- 変数定義
    -- ■■■■■■■
    DECLARE @備品ID INT;
    DECLARE @更新日 DATETIME;
    DECLARE @更新者 NVARCHAR(256);

    SELECT
        @備品ID = 備品ID
       ,@更新日 = 更新日
       ,@更新者 = 更新者
    FROM inserted

    INSERT INTO T_備品管理
               (備品ID
               ,状態ID
               ,利用者名
               ,貸出返却日
               ,更新日
               ,更新者)
    VALUES
              (@備品ID
              ,1 -- 貸出可能
              ,NULL
              ,NULL
              ,@更新日
              ,@更新者
              )
GO

