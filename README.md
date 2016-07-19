# 備品管理Lite
備品に貼ったQRコードをスマホで読み取り「借りる」ボタンを押すだけで借りる事ができます。

返却する場合は再度QRコードを読み取りボタンを押すだけです。
![サンプル](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/QRScanSample.png)
 
※デフォルトでは備品を借りるにはユーザー認証が必要です。
 
## 利用している技術
* [ASP.net MVC5](https://msdn.microsoft.com/ja-jp/library/dn448362(v=vs.118).aspx)
* [SQLServer2014Express](https://www.microsoft.com/ja-jp/download/details.aspx?id=42299)
* [C#](https://msdn.microsoft.com/ja-jp/library/618ayhy6.aspx?f=255&MSPPError=-2147217396)
* [PagedList.Mvc](http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application)
* [bootstrap](http://getbootstrap.com/)
 
## システムの持つ状態について
備品の状態遷移はシンプルな構造です。
![状態遷移](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/State.png)

## 設定方法について
#### ロールについて
このシステムでは２つのロールを用意しています。
<table>
    <tr>
        <td>admin</td>
        <td>管理者権限です。</td>
    </tr>
    <tr>
        <td>user</td>
        <td>ユーザー権限です。</td>
    </tr>
</table>

#### 設定値について
設定情報はinser文にて用意しています。設定値によって動作が異なるのでデフォルトの設定値を利用して下さい。

### 機能について

#### 自分が借りている備品について
ユーザーはログインすると自分が借りている備品を確認する事ができます。
 
![MyItem](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/MyItem.png)

#### 備品検索について
管理者は登録している全ての備品を検索する事ができます。
 
![Search](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/ItemSearch.png)

#### 備品の追加について
管理者は備品の登録が行えます。
 
![備品登録1](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/additem.png)
 
![備品登録2](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/AddItemMsg.png)

#### 備品の状態変更について
管理者は備品の状態変更が行えます。借りた状態のものを貸出可能にしたり、紛失してしまったものを行方不明にする事ができます。
 
![ItemDelete](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/ItemEdit.png)

#### 備品の削除について
管理者は備品の削除が行えます。また、備品を削除すると貸し借りの履歴データも削除されます。
 
![ItemDelete](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/ItemDelete.png)

## Licence

[MIT](https://github.com/tcnksm/tool/blob/master/LICENCE)

## Author

[s-tsurumaki](https://github.com/s-tsurumaki)
