<%@ Page Title="問卷回答統計頁" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="TotalAnswer.aspx.cs" Inherits="Questionnaire.FrontDesk.TotalAnswer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>
            <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h2>
        <h4>
            <asp:Literal ID="ltlBody" runat="server"></asp:Literal></h4>
        <asp:Panel ID="panTotal" runat="server" Visible="false" BorderStyle="Solid">
        </asp:Panel>
    </div>
</asp:Content>
