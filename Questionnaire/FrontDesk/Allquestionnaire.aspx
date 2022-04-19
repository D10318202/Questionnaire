<%@ Page Title="" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="Allquestionnaire.aspx.cs" Inherits="Questionnaire.Allquestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>問卷總覽</h1>
    問卷標題:
        <asp:TextBox ID="txtquestitle" runat="server" placeholder="輸入關鍵字"></asp:TextBox><br />
    開始/結束:
        <asp:TextBox ID="txtstart" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
    <asp:TextBox ID="txtend" runat="server" TextMode="DateTimeLocal"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="搜尋" /><br />
    <table border="1">
        <tr>
            <th></th>
            <th>#</th>
            <th>問卷</th>
            <th>狀態</th>
            <th>開始時間</th>
            <th>結束時間</th>
            <th>觀看統計</th>
        </tr>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:CheckBox ID="ckbDel" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                    </td>
                    <td>
                        <a href="Addquestioonaire.aspx?ID=<%#Eval("quesID") %>">
                        <asp:Literal ID="ltlName" runat="server" Text='<%#Eval("quesTitle") %>'></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="ltlstate" runat="server" Text='<%#Eval("quesstates") %>'></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="ltlStart" runat="server" Text='<%#Eval("quesstart") %>'></asp:Literal>
                    </td>
                    <td>
                        <asp:Literal ID="ltlEnd" runat="server" Text='<%#Eval("quesend") %>'></asp:Literal>
                    </td>
                    <td>
                        <a href="TotalAnswer.aspx?AccountID=<%#Eval("AccountID") %>">前往</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <script src="js/SearchkeyWord.js"></script>
</asp:Content>
