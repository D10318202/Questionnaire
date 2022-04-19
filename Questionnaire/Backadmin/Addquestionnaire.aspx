<%@ Page Title="後台-問卷建立" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="Addquestionnaire.aspx.cs" Inherits="Questionnaire.Backadmin.Addquestionnaire" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:LinkButton ID="LinkQuestionnaire" runat="server" OnClick="LinkQuestionnaire_Click">問卷</asp:LinkButton>
    <asp:LinkButton ID="LinkQuestions" runat="server" OnClick="LinkQuestions_Click">問題</asp:LinkButton>
    <asp:LinkButton ID="LinkFillQuestions" runat="server" OnClick="LinkFillQuestions_Click">填寫資料</asp:LinkButton>
    <asp:LinkButton ID="LinkTotal" runat="server" OnClick="LinkTotal_Click">統計</asp:LinkButton>
    <br />

    <asp:Panel ID="panQuestionnaire" runat="server" BorderStyle="Double">
        問卷名稱:
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox><br />
        描述內容:
        <asp:TextBox ID="txtBody" runat="server"></asp:TextBox><br />
        開始時間:
        <asp:TextBox ID="txtStart" runat="server" TextMode="DateTime"></asp:TextBox><br />
        結束時間:
        <asp:TextBox ID="txtEnd" runat="server" TextMode="DateTime"></asp:TextBox><br />
        已啟用<asp:CheckBox ID="checUse" runat="server" checked="true"/><br />
        <asp:Button ID="Cancle" runat="server" Text="取消" OnClick="Cancle_Click"/>
        <asp:Button ID="Save" runat="server" Text="送出" onclick="Save_Click"/>
    </asp:Panel>

    <asp:Panel ID="panQuestions" runat="server" Visible="false" BorderStyle="Double">
        種類:
        <asp:DropDownList ID="dropclass" runat="server">
            <asp:ListItem Selected="True" Value="createquestion">自訂問題</asp:ListItem>
            <asp:ListItem Value="commonquestion">常用問題</asp:ListItem>
        </asp:DropDownList>
        <br />
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
        <asp:Button ID="BtnAdd" runat="server" Text="加入" OnClick="BtnAdd_Click" />
        <br />
        <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" />
        <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkdel" runat="server" />
                        <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("quesDetailID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="quesDetailTitle" HeaderText="問題" />
                <asp:BoundField DataField="quesDetailType" HeaderText="種類" />
                <asp:BoundField DataField="quesDetailMustKeyIn" HeaderText="必填" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <a href="Addquestionnaire.aspx?ID=<%# Eval("quesDetailID") %>">編輯</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnquescancle" runat="server" Text="取消" OnClick="btnquescancle_Click" />
        <asp:Button ID="btnquessave" runat="server" Text="送出" onclick="btnquessave_Click"/>
    </asp:Panel>

    <asp:Panel ID="panFillQuestions" runat="server" Visible="false" BorderStyle="Double">
        <asp:Button ID="btnsavefile" runat="server" Text="匯出" OnClick="btnsavefile_Click" />
        <table border="1">
            <tr>
                <th>#</th>
                <th>姓名</th>
                <th>填寫時間</th>
                <th>觀看細節</th>
            </tr>
            <asp:Repeater ID="rptList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label ID="lblNumber" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Literal ID="ltlName" runat="server" Text='<%#Eval("Name") %>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="ltlTime" runat="server" Text='<%#Eval("CreateTime") %>'></asp:Literal>
                        </td>
                        <td>
                            <a href="questionList.aspx?AccountID=<%#Eval("AccountID") %>">前往</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </asp:Panel>

    <asp:Panel ID="panTotal" runat="server" Visible="false" BorderStyle="Double">
    </asp:Panel>
</asp:Content>
