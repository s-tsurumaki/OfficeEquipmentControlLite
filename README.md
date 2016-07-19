# 備品管理Lite
備品のQRコードをスマホで読み取り「借りる」ボタンを押すだけで借りる事ができます。

返却する場合は再度QRコードを読み取りボタンを押すだけです。
![サンプル](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/QRScanSample.png)
 
※デフォルトでは備品を借りるにはユーザー認証が必要です。
 
応用すれば様々な場面で活用できるかと思います。

## 利用している技術
ASP.net MVC5
 
C#
 
PagedList.Mvc
 
## 設定方法について
#### ロールについて
このシステムでは２つのロールを用意しています。
admin 管理者権限
user ユーザー権限

#### 設定値について
設定情報はinser文にて用意しています。設定値によって動作が異なるのでデフォルトの設定値を利用して下さい。


### 機能について

#### 自分が借りている備品について
ユーザーはログインすると自分が借りている備品を確認する事ができます。
![自分が借りている備品](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/MyItem.png)

#### 備品検索について
管理者は登録している全ての

#### 備品の追加について
SQLServerでテーブルに直接値を書いた方が早いと思いますが、web経由でも登録ができます。
![備品登録1](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/additem.png)
 
![備品登録2](https://github.com/s-tsurumaki/OfficeEquipmentControlLite/blob/master/ReadmeImages/AddItemMsg.png)



## VS. 

## Requirement

## Usage

## Install

## Contribution

## Licence

[MIT](https://github.com/tcnksm/tool/blob/master/LICENCE)

## Author

[s-tsurumaki](https://github.com/s-tsurumaki)
