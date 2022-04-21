<%@ Page Title="後台-問卷管理" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="Allquestionnaires.aspx.cs" Inherits="Questionnaire.Backadmin.Allquestionnaires" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h2>問卷管理</h2>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <asp:PlaceHolder ID="plcsearch" runat="server">問卷標題:
        <asp:TextBox ID="txtquestitle" runat="server"></asp:TextBox><br />
                    開始/結束:
        <asp:TextBox ID="txtstart" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:TextBox ID="txtend" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
                </asp:PlaceHolder>
                <br />
                <br />

                <asp:Button ID="delete" runat="server" Text="刪除"onclick="delete_Click" />
                <asp:Button ID="add" runat="server" Text="新增" OnClick="add_Click" />
                <table class="table table-striped">
                    <tr>
                        <th></th>
                        <th>#</th>
                        <th>問卷標題</th>
                        <th>狀態</th>
                        <th>開始時間</th>
                        <th>結束時間</th>
                        <th>觀看統計</th>
                    </tr>
                    <asp:Repeater ID="repQuestionnaire" runat="server" OnItemCommand="repQuestionnaire_ItemCommand">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfquesID" runat="server" Value='<%#Eval("quesID") %>' />
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ckbDel" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <a href="Addquestionnaire.aspx?quesID=<%# Eval("quesID") %>">
                                        <asp:Label ID="lblquesTitle" runat="server" Text='<%#Eval("quesTitle") %>'></asp:Label></a>
                                </td>
                                <td>
                                    <asp:Label ID="lblquesstates" runat="server" Text='<%#Eval("stateType")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblquesstart" runat="server" Text='<%#Eval("quesstart") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblquesend" runat="server" Text='<%#Eval("quesend") %>'></asp:Label>
                                </td>
                                <td>
                                    <a href="Addquestionnaire.aspx?quesID=<%#Eval("quesID") %>">前往
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <script src="../JS/SearchkeyWord.js"></script>
</asp:Content>
