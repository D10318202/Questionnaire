<%@ Page Title="" Language="C#" MasterPageFile="~/FrontDesk/FrontDesk.Master" AutoEventWireup="true" CodeBehind="QuestionList.aspx.cs" Inherits="Questionnaire.FrontDesk.QuestionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/jquery.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <asp:HiddenField ID="hfID" runat="server" />
        <h1>
            <asp:Literal ID="ltltitle" runat="server"></asp:Literal></h1>
        <br />
        <h3>
            <asp:Literal ID="ltlContent" runat="server"></asp:Literal></h3>
        <br />
        <p>有標示(*)為必填欄位</p>
        *姓名:
            <asp:TextBox ID="txtname" runat="server" placeholder="輸入姓名"></asp:TextBox><br />
        <br />
        *年齡:
            <asp:TextBox ID="txtage" runat="server" placeholder="輸入年齡"></asp:TextBox><br />
        <br />
        *手機號碼:
            <asp:TextBox ID="txtphone" runat="server" TextMode="Phone" placeholder="輸入手機號碼"></asp:TextBox><br />
        <br />
        *E-mail:
            <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="輸入信箱"></asp:TextBox><br />
        <br />
        <asp:PlaceHolder ID="plcquestion" runat="server"></asp:PlaceHolder>
        <br />
        <input type="button" id="btnSubmit" value="送出"  />
        <%--<asp:Button ID="Save" runat="server" Text="送出" OnClick="Save_Click" />--%>
        <asp:Button ID="Cancle" runat="server" Text="取消" OnClick="Cancle_Click" /><br />
    </div>
    <script>
        $(document).ready(function () {
            $("input[id=btnSubmit]").click(function () {
                var answer = "";
                var QuesDea = $("input[id*=Q]").get();
                console.log(QuesDea);
                for (var item of QuesDea) {
                    if (item.type == "radio" && item.checked) {
                        answer += item.id + " ";
                    }
                    if (item.type == "checkbox" && item.checked) {
                        answer += item.id + " ";
                    }
                    if (item.type == "text") {
                        answer += `${item.id}_${item.value}` + " ";
                    }
                }
                var postData = {
                    "Answer": answer,
                    "Name": $("#txtname").val(),
                    "Phone": $("#txtphone").val(),
                    "Email": $("#txtemail").val(),
                    "Age": $("#txtage").val()
                };

                $.ajax({
                    url: "../API/QuestionAnswerHandler.ashx?quesID=" + $("#hfID").val(),
                    method: "POST",
                    data: postData,
                    success: function (txtMsg) {
                        console.log(txtMsg);
                        if (txtMsg == "success"){
                            window.location = "QuestionListConfirm.aspx?quesID=" + $("#hfID").val(); 
                        }
                        else (txtMsg == "noAnswer")
                        {
                            alert("問題還沒完成");
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
