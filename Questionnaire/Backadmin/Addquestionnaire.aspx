<%@ Page Title="後台-問卷建立" Language="C#" MasterPageFile="~/Backadmin/Backadmin.Master" AutoEventWireup="true" CodeBehind="Addquestionnaire.aspx.cs" Inherits="Questionnaire.Backadmin.Addquestionnaire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <h2>生成問卷</h2>
        </div>
        <div>
            <div class="row">
                <div class="col-lg-12">
                    <asp:LinkButton ID="LinkQuestionnaire" runat="server" OnClick="LinkQuestionnaire_Click">問卷</asp:LinkButton>
                    <asp:LinkButton ID="LinkQuestions" runat="server" OnClick="LinkQuestions_Click">問題</asp:LinkButton>
                    <asp:LinkButton ID="LinkFillQuestions" runat="server" OnClick="LinkFillQuestions_Click">填寫資料</asp:LinkButton>
                    <asp:LinkButton ID="LinkTotal" runat="server" OnClick="LinkTotal_Click">統計</asp:LinkButton>
                    <br />

                    <asp:Panel ID="panQuestionnaire" runat="server" BorderStyle="Solid">
                        <asp:Label ID="ltlmistamsg" runat="server" ForeColor="Red"></asp:Label><br />
                        問卷名稱:
                            <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>*問卷名稱至少要有五個字*<br />
                        描述內容:
                            <asp:TextBox ID="txtBody" runat="server"></asp:TextBox><br />
                        開始時間:
                            <asp:TextBox ID="txtStart" runat="server" TextMode="Date"></asp:TextBox>*開始時間不能早於今天*<br />
                        結束時間:
                            <asp:TextBox ID="txtEnd" runat="server" TextMode="Date"></asp:TextBox>*結束時間不能早於開始時間*<br />
                        已啟用<asp:CheckBox ID="checUse" runat="server" Checked="true" /><br />

                        <asp:Button ID="Cancle" runat="server" Text="取消" OnClick="Cancle_Click" BackColor="Red" />
                        <asp:Button ID="Save" runat="server" Text="送出" OnClick="Save_Click" BackColor="LimeGreen"/>
                    </asp:Panel>

                    <asp:Panel ID="panQuestions" runat="server" Visible="false" BorderStyle="Solid">
                        <asp:Label ID="ltlquesmistMsg" runat="server" ForeColor="Red"></asp:Label><br />
                        種類:
                           <asp:DropDownList ID="dropclass" runat="server" OnSelectedIndexChanged="dropclass_SelectedIndexChanged" AutoPostBack="true">
                               <asp:ListItem Selected="True" Value="createquestion">自訂問題</asp:ListItem>

                           </asp:DropDownList>
                        <br />
                        問題:
                            <asp:TextBox ID="txtTitle1" runat="server"></asp:TextBox>&nbsp;                     
                            <asp:DropDownList ID="droptype" runat="server">
                                <asp:ListItem Value="0">單選方塊</asp:ListItem>
                                <asp:ListItem Value="1">複選方塊</asp:ListItem>
                                <asp:ListItem Value="2">文字方塊</asp:ListItem>
                            </asp:DropDownList>
                        *問題至少要有三個字*<br />                       
                        回答:
                          <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>*多個答案以;分隔*
                          <asp:Button ID="BtnAdd" runat="server" Text="加入" OnClick="BtnAdd_Click" BackColor="LimeGreen"/><br />
                        必填項目: <asp:CheckBox ID="checMust" runat="server" Checked="true" />
                        <br />
                        <br />
                        <asp:Button ID="btnDelete" runat="server" Text="刪除" OnClick="btnDelete_Click" BackColor="Red"/>
                        <table class="table table-striped">
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
                        <asp:Button ID="btnquescancle" runat="server" Text="取消" OnClick="btnquescancle_Click" BackColor="Red"/>
                        <asp:Button ID="btnquessave" runat="server" Text="送出" OnClick="btnquessave_Click" BackColor="LimeGreen"/>
                    </asp:Panel>

                    <asp:Panel ID="panFillQuestions" runat="server" Visible="false" BorderStyle="Solid">
                        <asp:Button ID="btnsavefile" runat="server" Text="匯出(.csv)" OnClick="btnsavefile_Click" CssClass="alert-light" /><br />
                        <table class="table table-success">
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
                                            <a href="AnswerDetail.aspx?AccountID=<%#Eval("AccountID") %>">前往</a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </asp:Panel>

                    <asp:PlaceHolder ID="plcTotal" runat="server" Visible="false" OnLoad="plcTotal_Load"></asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
