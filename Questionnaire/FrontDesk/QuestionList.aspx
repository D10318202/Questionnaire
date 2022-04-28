<%@ Page Title="" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="QuestionList.aspx.cs" Inherits="Questionnaire.FrontDesk.QuestionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/jquery.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-10">
                <asp:HiddenField ID="hfID" runat="server" />
                <h1>
                    <asp:Literal ID="ltltitle" runat="server"></asp:Literal></h1>
                <br />
                <h3>
                    <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h3>
                <br />
                <p>有標示(*)為必填欄位</p>
                <table>
                    <tr>
                        <td>*姓名:</td>
                        <td><asp:TextBox ID="txtname" runat="server" placeholder="輸入姓名"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>*年齡:</td>
                        <td><asp:TextBox ID="txtage" runat="server" placeholder="輸入年齡"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>*手機號碼:</td>
                        <td><asp:TextBox ID="txtphone" runat="server" TextMode="Phone" placeholder="輸入手機號碼"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>*E-mail:</td>
                        <td><asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="輸入信箱"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:PlaceHolder ID="plcquestion" runat="server"></asp:PlaceHolder>
                <br />
            </div>
        </div>
        <br />
        <br />
        <%--<button id="btnSubmit">送出</button>--%>
        <input type="button" id="btnSubmit" value="送出" />
        <%--        <asp:Button ID="Save" runat="server" Text="送出"  />--%>
        <asp:Button ID="btncancle" runat="server" Text="取消" OnClick="btncancle_Click" /><br />

    </div>
    <script>
        $(document).ready(function () {
            $("input[id=btnSubmit]").click(function () {
                var answer = "";
                var profile = `${$("#txtname").val()};${$("#txtphone").val()};${$("#txtemail").val()};${$("#txtage").val()}`;
                var QuesDea = $("input[id*=Q]").get();
                console.log(QuesDea);
                for (var ans of QuesDea) {
                    if (ans.type == "radio" && ans.checked) {
                        answer += ans.id + ";";
                    }
                    if (ans.type == "checkbox" && ans.checked) {
                        answer += ans.id + ";";
                    }
                    if (ans.type == "text") {
                        answer += `${ans.id}_${ans.value}` + ";";
                    }
                }
                var postData = {
                    "Answer": answer,
                    "Profile": profile
                };
                $.ajax({
                    url: "../API/QuestionAnswerHandler.ashx?quesID=" + $("#hfID").val(),
                    method: "POST",
                    data: postData,
                    success: function (txtMsg) {
                        console.log(txtMsg);
                        if (txtMsg == "success") {
                            window.location.href = "QuestionListConfirm.aspx?quesID=" + $("#hfID").val();
                        }
                        if (txtMsg == "noAnswer") {
                            alert("問題還沒完成");
                        }
                        if (txtMsg == "errorinput") {
                            alert("個人資訊有錯誤，請檢查")
                        }
                    },
                    error: function (msg) {
                        console.log(msg);
                        alert("通訊失敗，請聯絡管理員。");
                    }
                });
            });
        })
    </script>
</asp:Content>
