# Questionnaire
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

[QuestionHelpers]
1.CongifHelper.cs 接收web.config資料
2.Logger.cs 寫入錯誤訊息

前台起始頁面
Index.aspx

後台起始頁面
Backadmin/Allquestionnaires.aspx