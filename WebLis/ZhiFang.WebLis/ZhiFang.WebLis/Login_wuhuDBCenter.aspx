<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginMain.aspx.cs" Inherits="ZhiFang.WebLis.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>WebLis登陆</title>
    <%-- <link rel="stylesheet" type="text/css" href="css/Login.css" />--%>
    <script src="JS/Tools.js" type="text/javascript"></script>
    <script src="ui/util/base64.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Check() {
            if (document.getElementById('textPassword') && document.getElementById('textPassword').value && document.getElementById('textPassword').value != "") {
                var b = new Base64();
                var str = b.encode(document.getElementById('textPassword').value);
                document.getElementById('textPassword').value = str;
                return true;
            }
            return false;
        }
    </script>
    <style type="text/css">
        * {
            font-family: "微软雅黑";
            font-size: 16px;
            border: 0;
            padding: 0;
            margin: 0;
            color: #666;
            box-sizing: border-box;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
        }

        .bgone {
            width: 100%;
            height: 100%;
            position: absolute;
            top: 0;
            right: 0;
            left: 0;
            bottom: 0;
            margin: auto;
        }

        .pic {
            width: 492px;
            height: auto;
            position: absolute;
            top: 173px;
            right: 453px;
            left: 0;
            bottom: 0;
            margin: auto;
            z-index: 99999;
        }

        .table {
            background-color: #FFFFFF;
            width: 100%;
            height: 200px;
            position: absolute;
            top: 0;
            right: 0;
            left: 0;
            margin: auto;
        }

        .password {
            position: absolute;
            top: 12.8rem;
            right: 4rem;
            display: flex;
        }

        .btn {
            position: absolute;
            top: 18.5rem;
            right: 3.9rem;
            border: none;
            color: #fff;
            width: 373px;
            text-align: center;
            background-color: #1592ef;
            text-indent: 0rem;
            border-radius: 10px;
            text-shadow: 2px 2px 1px rgba(0, 0, 0, 0.2);
            box-shadow: 2px 2px 1px rgba(0, 0, 0, 0.2);
            font-size: 20px;
            height: 50px;
        }

        .wel {
            color: #fff;
            font-size: 30px;
            position: absolute;
            text-align: center;
        }

        .wel1 {
            color: #fff;
            font-size: 12.38px;
            position: absolute;
            text-align: center;
        }

        input {
            height: 30px;
            width: 200px;
            text-indent: 55px;
            outline: none;
            background: #eef6fd;
        }

        .password input {
            border: 0;
        }

        .user {
            position: absolute;
            top: 7.8rem;
            right: 4rem;
            display: flex;
        }

        #yonghu img {
           
        }

        }
    </style>
</head>
<body>
    <img class="bgone" src="Images/wuhucenter.png" />
    <form id="form1" runat="server" onsubmit="return Check();">
        <table class="table" border="1" style="border-color:#000" cellspacing="50">
            <tr align="center">
                <td colspan="2">
                    <img src="Images/wuhulogintitle.png" /></td>
            </tr>
            <tr >
                <td  style="width:50%">
                    <div style="float :right; border-bottom: 2px #d6e7fa solid;" >
                        <img style="height:30px;" src="Images/yhm.png" />  <asp:TextBox ID="textUserid" runat="server" placeholder="用户名"></asp:TextBox>
                    </div>
                  
                </td>
                <td >
                    <div style="float :left; border-bottom: 2px #d6e7fa solid;">
                        <img style="height:30px;" src="Images/mm.png" /><asp:TextBox ID="textPassword" runat="server" TextMode="Password" placeholder="密码" name="密码"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr align="center">
                <td colspan="2" style="height:50px">
                    <asp:ImageButton style="height:50px;width:260px" ID="ImageButton1" runat="server" ImageUrl="Images/LoginButton_wuhu1.png" OnClick="ImageButton1_Click" />
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
