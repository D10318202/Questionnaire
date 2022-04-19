<%@ Page Title="後台-常用問題管理" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="Oftenusequest.aspx.cs" Inherits="Questionnaire.Backadmin.Oftenusequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>問卷標題</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />

            </td>
        </tr>
        <tr>
            <td>建立新問題</td>
            <td>
                <asp:TextBox ID="txtCreate" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnCreate" runat="server" Text="新增" OnClick="btnCreate_Click" />
            </td>
        </tr>
    </table>

    <table class="table table-striped">
        <tr>
            <th></th>
            <th>#</th>
            <th>標題</th>
            <th>建立時間</th>
        </tr>
        <asp:Repeater ID="rptTable" runat="server">
            <ItemTemplate>
                <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("QuestionnaireID") %>' />
                <tr>
                    <td>
                        <asp:CheckBox ID="ckbDel" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                    </td>
                    <td>
                        <a href="Addquestionnaires.aspx?ID=<%#Eval("quesDetailID") %>">
                            <asp:Label ID="lblQueryName" runat="server" Text='<%#Eval("QueryName") %>'></asp:Label></a>
                    </td>
                    <td>
                        <asp:Label ID="lblCreateTime" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
    </div>
            </div>
        </div>
</asp:Content>
