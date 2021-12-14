<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallBarPrintTest.aspx.cs" Inherits="OA.DBQuery.RunExec.CallBarPrintTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>调用DLL打印条码</title>
    <script type="text/javascript">
        function print() {
            var bar = document.getElementById('txtbar1').value;
            var controlobject = document.getElementById('controlbyid');
            alert('open:0');
            var r = controlobject.PortOpen(5, 19200);
            alert('open:'+r);
            if (r == '0') {
                var rset = controlobject.SetPrintXY(0, 0);
                alert('setprintxy:' + rset);
                var rp = controlobject.SetPaperSize(0, 100, 30, 2);
                alert('SetPaperSize:' + rp);
                var rc = controlobject.PrintChineseStr(65, 3, 0, 2, 2, 0, '王德才');
                alert('PrintChineseStr:' + rc);
                var rc2 = controlobject.PrintChineseStr(65, 7, 0, 0, 2, 0, '男/21岁');
                var rc3 = controlobject.PrintChineseStr(65, 11, 0, 0, 2, 0, '内科');
                var rc4 = controlobject.PrintChineseStr(52, 16, 0, 0, 2, 0, '12/18/09 15:45');
                var rc5 = controlobject.PrintChineseStr(25, 20, 0, 2, 2, 0, '尿常规');

                var rb = controlobject.PrintBarcode(25, 2, 0, 2, 3, 6, 100, 1, bar);
                alert('PrintBarcode:' + rb);
                var rp = controlobject.Print(1);
            }
            var rclear = controlobject.SetClear();
                //关闭端口
            var rclose = controlobject.PortClose();
            alert(rclear + "," + rclose);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <object id="controlbyid" classid="clsid:{8f6c360e-6600-4519-98bf-dec47f5f4c4d}" codebase="setup.exe" ></object>
     <br />
     <input type="text" id="txtbar1" value="X0909150008"/>
     <input type="button" onclick="print();" value="打 印 测试"/>
     <br />
     <div style="display:">
     <asp:Label runat="server" ID="lab1" Text="条码号"></asp:Label>
     <asp:TextBox runat="server" ID="txtbar">X0909150012</asp:TextBox>
     <asp:Button runat="server" ID="btnprint" Text="打印条码" onclick="btnprint_Click" />
    </form>
</body>
</html>
