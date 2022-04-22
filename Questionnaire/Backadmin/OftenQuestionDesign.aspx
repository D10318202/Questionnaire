<%@ Page Title="" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="OftenQuestionDesign.aspx.cs" Inherits="Questionnaire.Backadmin.OftenQuestionDesign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/bootstrap.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container">
        <div class="row">
            <h2>常用問題新增</h2>
        </div>
        <div class="row">
            <div class="col-lg-8">
                <asp:Label ID="ltlquesmistMsg" runat="server" ForeColor="Red"></asp:Label><br />
                問卷名稱:
                     <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
                問題:
                     <asp:TextBox ID="txtTitle1" runat="server"></asp:TextBox>&nbsp;
                     <asp:DropDownList ID="droptype" runat="server">
                         <asp:ListItem Value="0">單選方塊</asp:ListItem>
                         <asp:ListItem Value="1">複選方塊</asp:ListItem>
                         <asp:ListItem Value="2">文字方塊</asp:ListItem>
                     </asp:DropDownList>
                必填<asp:CheckBox ID="checMust" runat="server" Checked="true" />
                <br />
                回答:
                    <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>(多個答案以,分隔)
                    <asp:Button ID="BtnAdd" runat="server" Text="加入" OnClick="BtnAdd_Click"/>
                <br />
                <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click"/>
                <table class="table table-info">
                    <tr>
                        <th></th>
                        <th>#</th>
                        <th>問題</th>
                        <th>種類</th>
                        <th>必填</th>
                        <th></th>
                    </tr>
                    <asp:Repeater ID="repQuestions" runat="server" OnItemCommand="repQuestions_ItemCommand">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfquesDetailID" runat="server" Value='<%#Eval("quesDetailID") %>' />
                            <tr>
                                <td>
                                    <asp:CheckBox ID="ckbDel" runat="server" />
                                </td>
                                <td>
                                    <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblquesDetailTitle" runat="server" Text='<%#Eval("quesDetailTitle") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblquesDetailType" runat="server" Text='<%#Eval("quesDetailType") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckbListUse" runat="server" Checked='<%#Eval("quesDetailMustKeyIn") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="LinkEdit" runat="server" CommandArgument='<%#Eval("quesDetailID") %>' CommandName="lkbEdit">編輯</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Button ID="btnquescancle" runat="server" Text="取消" Onclick="btnquescancle_Click" />
                <asp:Button ID="btnquessave" runat="server" Text="送出" OnClick="btnquessave_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
