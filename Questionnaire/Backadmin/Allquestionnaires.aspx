<%@ Page Title="後台-問卷管理" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="Allquestionnaires.aspx.cs" Inherits="Questionnaire.Backadmin.Allquestionnaires" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="plcsearch" runat="server">
        問卷標題:
        <asp:TextBox ID="txtquestitle" runat="server"></asp:TextBox><br />
        開始/結束:
        <asp:TextBox ID="txtstart" runat="server" TextMode="DateTime"></asp:TextBox>
        <asp:TextBox ID="txtend" runat="server" TextMode="DateTime"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="搜尋" />
    </asp:PlaceHolder>
    <br /><br />

    <asp:Button ID="delete" runat="server" Text="刪除" />
    <asp:Button ID="add" runat="server" Text="新增" onclick="add_Click"/>
    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkdel" runat="server" />
                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("quesID")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="quesID" HeaderText="問卷代號" />
            <asp:BoundField DataField="quesTitle" HeaderText="問卷標題" />
            <asp:BoundField DataField="quesstates" HeaderText="狀態" />
            <asp:BoundField DataField="quesstart" HeaderText="開始時間" />
            <asp:BoundField DataField="quesend" HeaderText="結束時間" />
            <asp:TemplateField>
                <ItemTemplate>
                    <a href="?.aspx?ID=<%# Eval("quesID") %>">觀看統計</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
