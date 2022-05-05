<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionList.aspx.cs" Inherits="Questionnaire.QuestionList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../JS/jquery.min.js"></script>
    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <script src="../JS/bootstrap.min.js"></script>
    <style>
        .auto-style1 {
            width: 79px;
            height: 73px;
        }

        .auto-style2 {
            width: 79px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%">
            <tr>
                <td class="auto-style2">
                    <img src="../Pictures/75470.png" class="auto-style1" /></td>
                <td>
                    <a href="Index.aspx">首頁</a>|
                    <a href="Allquestionnaire.aspx">問卷管理</a>|
                </td>
            </tr>
        </table>
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
                            <td>
                                <asp:TextBox ID="txtname" runat="server" placeholder="輸入姓名" CssClass="Must"></asp:TextBox>
                                *請輸入中文姓名*
                            </td>
                        </tr>
                        <tr>
                            <td>*年齡:</td>
                            <td>
                                <asp:TextBox ID="txtage" runat="server" placeholder="輸入年齡" CssClass="Must"></asp:TextBox>
                                *請輸入介於1~150之間*
                            </td>
                        </tr>
                        <tr>
                            <td>*手機號碼:</td>
                            <td>
                                <asp:TextBox ID="txtphone" runat="server" TextMode="Phone" placeholder="輸入手機號碼" CssClass="Must"></asp:TextBox>
                                *請輸入10位數字的號碼*
                            </td>
                        </tr>
                        <tr>
                            <td>*E-mail:</td>
                            <td>
                                <asp:TextBox ID="txtemail" runat="server" TextMode="Email" placeholder="輸入信箱" CssClass="Must"></asp:TextBox>
                                *信箱應該包含@*
                            </td>
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
            <input type="button" id="btnSubmit" value="送出" />
            <asp:Button ID="btncancle" runat="server" Text="取消" OnClick="btncancle_Click" /><br />

        </div>
    </form>
    <script>
        $(document).ready(function () {
            $("input[id=btnSubmit]").click(function () {
                var inputWrong = true;
                var Mustlist = $(".Must").get();
                for (var mustitem of Mustlist) {
                    if (mustitem.tagName == 'INPUT ') {
                        if (mustitem.value == "") {
                            inputWrong = true;
                            alert("尚未作答完畢");
                            return;
                        }
                    }
                    if (mustitem.tagName == 'TABLE') {
                        var profileTable = $(`#${mustitem.id}`);
                        var mustlist = profileTable.find('input').get();
                        var mustcheck = [];
                        for (var selection of mustlist) {
                            if (selection.checked) {
                                mustcheck.push(selection);
                            }
                        }
                        if (mustcheck.length == 0) {
                            inputWrong = true;
                            alert("尚未作答完畢");
                            return;
                        }
                    }
                }
                inputWrong = false;

                if (inputWrong == false) {
                    var answer = "";
                    var profile = `${$("#txtname").val()};${$("#txtage").val()};${$("#txtphone").val()};${$("#txtemail").val()}`;
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
                        "Profile": profile
                    };
                    $.ajax({
                        url: "../API/QuestionAnswerHandler.ashx?quesID=" + $("#hfID").val(),
                        method: "POST",
                        data: postData,
                        success: function (txtMsg) {
                            console.log(txtMsg);
                            if (txtMsg == "success") {
                                window.location = "QuestionListConfirm.aspx?quesID=" + $("#hfID").val();
                            }
                            if (txtMsg == "noAnswer") {
                                alert("未作答");
                            }
                            if (txtMsg == "errorinput") {
                                alert("個人資訊有誤");
                            }
                        },
                        error: function (msg) {
                            console.log(msg);
                            alert("通訊失敗，請聯絡管理員。");
                        }
                    });
                }

            });
        })
    </script>
</body>
</html>
