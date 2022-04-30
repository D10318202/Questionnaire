<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionListConfirm.aspx.cs" Inherits="Questionnaire.QuestionListConfirm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../JS/jquery.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/bootstrap.min.js"></script>
    <style>
        body {
            background-color: aliceblue;
        }

        .auto-style1 {
            width: 79px;
            height: 73px;
        }
        .auto-style2 {
            width: 87px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td class="auto-style2">
                    <img src="../Pictures/75470.png" class="auto-style1" /></td>
                <td>
                    <a href="Index.aspx">首頁</a>|
                    <a href="Allquestionnaire.aspx">問卷管理</a>|
                </td>
            </tr>
        </table>
        <div class="container">
            <div class="row">
                <div class="col-lg-10">
                    <asp:HiddenField ID="hfID" runat="server" />
                    <h1>
                        <asp:Literal ID="ltltitle" runat="server"></asp:Literal></h1>
                    <br />
                    <h3>
                        <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h3>
                    <br />
                    <p>有標示(*)為必填欄位</p>
                    <table>
                        <tr>
                            <td>*姓名:</td>
                            <td>
                                <asp:TextBox ID="txtname" runat="server" placeholder="輸入姓名" CssClass="txtname"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>*年齡:</td>
                            <td>
                                <asp:TextBox ID="txtage" runat="server" placeholder="輸入年齡" CssClass="txtage"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>*手機號碼:</td>
                            <td>
                                <asp:TextBox ID="txtphone" runat="server" TextMode="Phone" placeholder="輸入手機號碼" CssClass="txtphone"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>*E-mail:</td>
                            <td>
                                <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="輸入信箱" CssClass="txtemail"></asp:TextBox></td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:PlaceHolder ID="plcquestion" runat="server"></asp:PlaceHolder>
                    <br />
                    <br />
                </div>
            </div>
            <asp:Button ID="Save" runat="server" Text="送出" OnClick="Save_Click" CssClass="Save" />
            <asp:Button ID="Cancle" runat="server" Text="取消" OnClick="Cancle_Click" CssClass="Cancle" /><br />
        </div>
    </form>
</body>
</html>
