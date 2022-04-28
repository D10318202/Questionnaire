<%@ Page Title="" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="QuestionListConfirm.aspx.cs" Inherits="Questionnaire.FrontDesk.QuestionListConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <asp:TextBox ID="txtname" runat="server" placeholder="輸入姓名"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>*年齡:</td>
                        <td>
                            <asp:TextBox ID="txtage" runat="server" placeholder="輸入年齡"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>*手機號碼:</td>
                        <td>
                            <asp:TextBox ID="txtphone" runat="server" TextMode="Phone" placeholder="輸入手機號碼"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>*E-mail:</td>
                        <td>
                            <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="輸入信箱"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:PlaceHolder ID="plcquestion" runat="server"></asp:PlaceHolder>
                <br />
                <br />
            </div>
        </div>
        <asp:Button ID="Save" runat="server" Text="送出" OnClick="Save_Click" />
        <asp:Button ID="Cancle" runat="server" Text="取消" OnClick="Cancle_Click" /><br />
    </div>
</asp:Content>
