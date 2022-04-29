<%@ Page Title="問卷回答統計頁" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="TotalAnswer.aspx.cs" Inherits="Questionnaire.FrontDesk.TotalAnswer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/jquery.min.js"></script>
    <script src="../JS/bootstrap.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-10">
                <h2>
                    <asp:Literal ID="ltlTitle" runat="server"></asp:Literal></h2>
                <h4>
                    <asp:Literal ID="ltlBody" runat="server"></asp:Literal></h4>
                <asp:PlaceHolder ID="plcTotal" runat="server" Visible="false"></asp:PlaceHolder>
            </div>
        </div>
    </div>
</asp:Content>
