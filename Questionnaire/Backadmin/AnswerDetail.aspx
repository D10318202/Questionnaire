<%@ Page Title="" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="AnswerDetail.aspx.cs" Inherits="Questionnaire.Backadmin.AnswerDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <h2>
            <asp:Literal ID="ltltitle" runat="server"></asp:Literal></h2>
        <br />
        <h3>
            <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h3>
        <br />
        <p>有標示(*)為必填欄位</p>
        *姓名:
            <asp:TextBox ID="txtname" runat="server" placeholder="輸入姓名" Enabled="false"></asp:TextBox><br />
        <br />
        *年齡:
            <asp:TextBox ID="txtage" runat="server" placeholder="輸入年齡" Enabled="false"></asp:TextBox><br />
        <br />
        *手機號碼:
            <asp:TextBox ID="txtphone" runat="server" TextMode="Phone" placeholder="輸入手機號碼" Enabled="false"></asp:TextBox><br />
        <br />
        *E-mail:
            <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="輸入信箱" Enabled="false"></asp:TextBox><br />
        <br />
        <asp:PlaceHolder ID="plcquestion" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>
