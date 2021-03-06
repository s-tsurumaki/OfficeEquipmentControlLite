DROP VIEW dbo.V_備品管理_MAX
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
======================================================================
概要    ：最新の備品管理IDを取得します。
備考    ：
======================================================================
*/
CREATE VIEW dbo.V_備品管理_MAX
AS
SELECT
   MAIN_T.備品管理ID
  ,MAIN_T.品名
  ,MAIN_T.備品ID
  ,B.型番
  ,B.備考
  ,SUB_T.利用者名
  ,SUB_T.貸出返却日
  ,S.状態ID
  ,S.状態
  ,(C.OneEditURL + CONVERT(NVARCHAR, MAIN_T.備品ID)) AS QRCodeURL -- QR Code 作成用
   -- 次の備品管理状態（数値）
  ,CASE 
   WHEN S.状態ID = 1 THEN 2
   WHEN S.状態ID = 2 THEN 3
   WHEN S.状態ID = 3 THEN 1
   WHEN S.状態ID = 4 THEN 4
   ELSE NULL
   END AS Next状態
   -- 次の備品管理状態（日本語）
  ,CASE 
   WHEN S.状態ID = 1 THEN '借りる'
   WHEN S.状態ID = 2 THEN '返す'
   WHEN S.状態ID = 3 THEN '返却済'
   WHEN S.状態ID = 4 THEN '行方不明'
   ELSE ''
   END AS Next状態String
FROM
(
  -- ■■■■■■■■■■■■■■■
  -- 最新の備品管理IDを取得するSQL
  -- ■■■■■■■■■■■■■■■
  SELECT
       MAX(B.備品管理ID) AS 備品管理ID
      ,B.備品ID
      ,(SELECT I.品名 FROM T_備品 I WHERE B.備品ID = I.備品ID) AS 品名
  FROM T_備品管理 B
  GROUP BY B.備品ID
) AS MAIN_T
INNER JOIN  T_備品管理 SUB_T ON MAIN_T.備品管理ID = SUB_T.備品管理ID
INNER JOIN  T_備品         B ON MAIN_T.備品ID     = B.備品ID
INNER JOIN  T_状態         S ON S.状態ID          = SUB_T.状態ID
CROSS JOIN  T_Common       C -- QR Code 作成用

GO
