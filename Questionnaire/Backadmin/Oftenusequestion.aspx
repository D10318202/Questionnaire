<%@ Page Title="後台-常用問題管理" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="Oftenusequestion.aspx.cs" Inherits="Questionnaire.Backadmin.Oftenusequestion"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h2>後台-常用問題管理</h2>
        </div>
        <div class="row">
            <div class="col-lg-2">
                <a href="List.aspx">問卷管理</a><br />
                <a href="ExampleList.aspx">常用問題管理</a><br />
            </div>
            <div class="col-lg-8">
                <table>
                    <tr>
                        <td>問卷標題</td>
                        <td>
                            <asp:TextBox ID="txtkeyword" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>新增問題範例</td>
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
                    </tr>
                    <asp:Repeater ID="rptTable" runat="server">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("quesDetailID") %>' />
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ckbDel" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <a href="Allquestionnaire.aspx?ID=<%#Eval("quesDetailID") %>">
                                        <asp:Label ID="lblQueryName" runat="server" Text='<%#Eval("quesDetailTitle") %>'></asp:Label></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Button ID="btnDelete" runat="server" Text="刪除" />
            </div>
        </div>
    </div>
    <script src="../JS/SearchkeyWord.js"></script>
</asp:Content>
