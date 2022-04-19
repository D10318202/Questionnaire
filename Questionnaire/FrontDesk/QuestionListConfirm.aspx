<%@ Page Title="問卷填寫內容-確認頁" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="QuestionListConfirm.aspx.cs" Inherits="Questionnaire.FrontDesk.QuestionListConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div align="center">
        <h3>
            <asp:Literal ID="ltltitle" runat="server"></asp:Literal></h3>
        <br />
        <h4>
            <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h4>
        <br />
        <p>有標示(*)為必填欄位</p>
        *姓名:
            <asp:TextBox ID="txtname" runat="server" placeholder="輸入姓名"></asp:TextBox><br />
        <br />
        *年齡:
            <asp:TextBox ID="txtage" runat="server" placeholder="輸入年齡"></asp:TextBox><br />
        <br />
        *手機號碼:
            <asp:TextBox ID="txtphone" runat="server" TextMode="Phone" placeholder="輸入手機號碼"></asp:TextBox><br />
        <br />
        *E-mail:
            <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="輸入信箱"></asp:TextBox><br />
        <br />
        <asp:PlaceHolder ID="plcquestion" runat="server"></asp:PlaceHolder>
        <br />
        <asp:Button ID="Save" runat="server" Text="送出" OnClick="Save_Click" />
        <asp:Button ID="Cancle" runat="server" Text="取消" OnClick="Cancle_Click" /><br />
    </div>
</asp:Content>
