# Questionnaire

※※※5/11更正部分※※※
1.AnswerDetail個人資訊部分改成Enabled
2.Addquestionnaire匯出部分修正
3.Allquestionnaire問卷標題改成有url模式和沒有的
4.Oftenusequestion搜尋功能修復

網站基本功能
1.前台可搜尋問卷、作答、觀看問題的統計數據
2.後台可設計、刪除問卷、觀看問題的統計數據
3.後台可以新增、刪除常用問題
4.後台可以觀看所有作答的內容、並匯出資料

資料庫(MSSQL)
結構與描述 questionnaire.sql
資料 questionnaireData.sql
備份 questionnaire.bak

環境依賴
asp.net framework4.8

<<<<<<< HEAD
目錄結構描述
=======
>>>>>>> cb0b9d372edce37bfa510c13b1cf81518a5e3b21
[QuestionHelpers]
1.CongifHelper.cs 接收web.config資料
2.Logger.cs 寫入錯誤訊息

<<<<<<< HEAD
[QuestionManagers]
1.QuestionnaireManager.cs 與資料庫溝通

[QuestionModels]
1.AccountInfoModel.cs 個人資訊model
2.QuestionAnswerModel.cs 回答model
3.QuestionDetailModel.cs 問題model
4.QuestionModel.cs 問卷model
5.QuestionTotalModel.cs 統計model

[Questionnaire]
【前台起始頁面】
FrontDesk/Index.aspx

【後台起始頁面】
Backadmin/Allquestionnaires.aspx

◆API
1.QuestionAnswerHandler.ashx 前台使用取得作答內容

◆Backadmin 後台
1.Addquestionnaire.aspx 設計問卷頁面、統計頁面、填寫資料頁面
2.Allquestionnaires.aspx 列出全部問卷頁面
3.AnswerDetail.aspx 回答內容頁面
4.OftenQuestionDesign.aspx 設計常用問題頁面
5.BackAdmin.Master 用於後台主板
6.Oftenusequestion.aspx 常用問題頁面

◆CSS
1.boopstrap.min.css

◆FrontDesk 前台
1.Allquestionnaire.aspx 列出全部問卷頁面
2.FrontDesk.Master 用於前台主板
3.Index.aspx 首頁
4.QuestionList.aspx 回答頁面
5.QuestionListConfirm.aspx 回答確認頁面
6.TotalAnswer.aspx 統計頁面

◆JS
1.boopstrap.min.js
2.jquery,min.js

◆Pictures
1.75470.png logo

◆ShareControls
1.ucPage.ascx 分頁控制項(列表頁/常用問題頁)
=======
>>>>>>> cb0b9d372edce37bfa510c13b1cf81518a5e3b21
