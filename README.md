# 📘 Lab Unity Project (360 Video + Questionnaire)

## 🧩 專案簡介
此專案為實驗室過去用於**資料收集**的 Unity 專案，主要包含：
- 360 度影片播放
- 問卷填寫（SAM）

Unity 版本：

Unity 2022.3.27f1


---

## 📁 專案結構說明

### 🔹 `Assets/`
主要開發內容皆放於此資料夾。

---

### 🎬 `Assets/Scenes`
放置所有場景（Scenes）

| 資料夾 / 場景 | 說明 |
|------|------|
| `360 Video` | 播放 360 影片的正式場景 |
| `360 Video - Test` | 測試用場景（目前播放影片可以用這個） |
| `SAM` | 問卷填寫場景 |
| `Example/_Start` | 實驗最初場景（我猜現在是用不到） |

---

### 💻 `Assets/Scripts`
放置所有 C# 腳本

---

### 🎥 `Assets/Videos/Sources`
放置 360 影片檔案

---

## ▶️ 如何播放 360 影片

1. 開啟場景：

Assets/Scenes/360 Video - Test


2. 在左側 Hierarchy 找到：

Skybox Video Player


3. 在 Inspector 中找到：

SkyboxVideoPlayerController (Script)
<img width="594" height="441" alt="image" src="https://github.com/user-attachments/assets/3b5c5ca5-ad66-4f23-80bf-98e3f0e6da19" />


4. 將你的影片拖入：

Clip 欄位


5. 按下 Play 即可播放 🎬

---

## 🥽 VR 設備使用方式

1. 使用實驗室的 VR 頭盔（Meta Quest）
2. 用連接線將頭盔接到電腦
3. 電腦需先安裝官方軟體：  
   https://www.meta.com/zh-tw/help/quest/1517439565442928/

4. 成功連線後：
   - 頭盔內會看到黑白虛擬空間

5. 回到 Unity：
   - 按下 ▶ Play
   - 即可在 VR 中運行專案

---

## 👀 備註
此專案為舊實驗室專案，部分功能可能未完整維護，可能會有一點問題QAQ
