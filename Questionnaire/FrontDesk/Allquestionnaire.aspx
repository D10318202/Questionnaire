<%@ Page Title="" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="Allquestionnaire.aspx.cs" Inherits="Questionnaire.Allquestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>問卷總覽</h1>
    問卷標題:
        <asp:TextBox ID="txtquestitle" runat="server" placeholder="輸入關鍵字"></asp:TextBox><br />
    開始/結束:
        <asp:TextBox ID="txtstart" runat="server" TextMode="DateTime" placeholder="輸入開始時間"></asp:TextBox>
    <asp:TextBox ID="txtend" runat="server" TextMode="DateTime" placeholder="輸入結束時間"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜尋" /><br />
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="quesID" HeaderText="問卷代號" />
            <asp:BoundField DataField="quesTitle" HeaderText="問卷標題" />
            <asp:BoundField DataField="quesstates" HeaderText="狀態" />
            <asp:BoundField DataField="quesstart" HeaderText="開始時間" />
            <asp:BoundField DataField="quesend" HeaderText="結束時間" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="Addquestionnaire.aspx?ID=<%# Eval("quesID") %>">觀看統計</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <script src="js/SearchkeyWord.js"></script>
</asp:Content>
