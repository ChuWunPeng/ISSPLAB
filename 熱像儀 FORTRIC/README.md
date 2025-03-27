---
title: 實驗室熱像儀 FOTRIC 設備連接、範例程式及函式庫介紹
---

# 實驗室熱像儀 FOTRIC 設備連接、範例程式及函式庫介紹
###### 🏷 Tags: `Lab Equipment`
---
## 📚 目錄

- [📌 廠商基本設定須知](#-廠商基本設定須知)
- [I. 🔌 硬體連接熱像儀](#i--硬體連接熱像儀)
  - [🧰 設備材料](#-設備材料)
  - [🧯 設備接法](#-設備接法)
- [II. 🖥 熱像儀連接電腦設定](#ii--熱像儀連接電腦設定)

---

## 📌 廠商基本設定須知

- 熱像儀固定 IP：`192.168.1.100`  
- 熱像儀網站帳密：`admin / admin`  
- 廠商C# APP 帳密：`admin / 無密碼`

---

## I. 🔌 硬體連接熱像儀

### 🧰 設備材料

- DC 12V/24V 電源線 x1  
- 網路線（RJ-45）x1  
- 熱像儀 x1

| <img src="電源線24v.jpg" height="150"> | <img src="網路線.jpg" height="200"> | <img src="熱像儀.jpg" height="200"> | 
|:---------------------------:|:----------------------------:|:------------------:|
| **Fig.1** 電源線 | **Fig.2** 網路線 | **Fig.3** 熱像儀 |

---

### 🧯 設備接法

1. 將網路線一端連接至熱像儀接口，如 [Fig.4](#fig4-熱像儀接口) 的標示 1，另一端接電腦或路由器。  
2. 將電源線插入熱像儀的另一個接口，如 [Fig.4](#fig4-熱像儀接口) 的標示 2。

<p align="center">
  <img src="熱像儀接口.png" height="150"><br>
  <b><a name="fig4-熱像儀接口">Fig.4 熱像儀接口</a></b>
</p>

---

## II. 🖥 熱像儀連接電腦設定

<details>
<summary><strong>Step 1:</strong> 開啟乙太網路內容</summary>

右鍵點選乙太網路 → 點選「內容」  
<img src="連接1.png">
</details>

<details>
<summary><strong>Step 2:</strong> 進入 IPv4 設定</summary>

選擇「網際通訊協定第 4 版（TCP/IPv4）」 → 點選「內容」  
<img src="連接2.png">
</details>

<details>
<summary><strong>Step 3 & 4:</strong> 手動設定 IP</summary>

- 選擇「使用下列的 IP 位址」  
- IP 輸入 `192.168.1.xx`（`xx` 為任意 2~254 之間的數字）  
- 子網路遮罩：`255.255.255.0`  

> ⚠️ **注意**：IP 不可與 [熱像儀固定 IP](#📌-廠商基本設定須知) `192.168.1.100` 相同。

<img src="連接3.png">
</details>

<details>
<summary><strong>Step 5:</strong> 確認是否連線成功</summary>

在瀏覽器輸入 `192.168.1.100`，若連接成功會顯示以下畫面  
（登入帳密見 [廠商基本設定須知](#📌-廠商基本設定須知)）  
<img src="熱像儀網站畫面.png">
</details>

---
