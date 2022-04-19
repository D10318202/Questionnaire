<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Questionnaire.FrontDesk.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 361px;
            height: 322px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <a href="Index.aspx">首頁</a>
        <a href="Allquestionnaire.aspx">問卷總覽</a>
        <div align="center">
            <img src="../Pictures/75470.png" class="auto-style1" /><br />
            <asp:TextBox ID="txtkeyword" runat="server" placeHolder="請輸入搜尋文字" Width="450px" Height="44px"></asp:TextBox>&nbsp;
            <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" Height="48px" Width="83px" />
        </div>
    </form>
</body>
<script src="../JS/SearchkeyWord.js"></script>
</html>
