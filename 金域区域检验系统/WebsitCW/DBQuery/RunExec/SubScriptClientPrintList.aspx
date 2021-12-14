<%@ Import Namespace="NewsWebService" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubScriptClientPrintList.aspx.cs"
    Inherits="OA.DBQuery.RunExec.SubScriptClientPrintList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>打印清单</title>

    <script type="text/javascript" src="../../Includes/JS/calendarDateTime.js"></script>

    <style type="text/css">
        .PrintTitle
        {
            border-bottom-width: thin;
            border-bottom-style: solid;
            border-left-style: none;
            border-right-style: none;
            border-top-style: none;
        }
        .titleFont
        {
            font-family: "宋体";
            font-size: 18px;
            font-weight: bold;
        }
        INPUT
        {
            border-right-style: none;
            border-top-style: none;
            border-left-style: none;
        }
        .lableFont
        {
            font-family: "宋体";
            font-size: 14px;
        }
        @media Print
        {
            .prt
            {
                visibility: hidden;
            }
        }
    </style>

    <script type="text/javascript">  
　　	function printsetup(){  
　　	// 打印页面设置  
　　	window.document.wb.ExecWB(8,1);  
　　	}  
　　	function printpreview()
　　	{  
　　	    // 打印页面预览  
　　	    //wb.ExecWB(7,1);
　　	    doPrint();
　　	}  

　　	function printit()  
　　	{  
　　		if (confirm('确定打印吗？')) 
　　		{  
　　			wb.ExecWB(6,1)  
　　		}  
　　	}
	　　
　　	function Print(cmdid,cmdexecopt)
		{
			document.all("printmenu").style.display="none";
			
			document.body.focus();
			
			try
			{
				var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>'; 

				document.body.insertAdjacentHTML('BeforeEnd', WebBrowser); 
				
				WebBrowser1.ExecWB(cmdid,cmdexecopt,null,null); 
				WebBrowser1.outerHTML = ""; 
				
				if(cmdid==6 && cmdexecopt==6)
				{
					alert('打印完成');
				}
			}
			catch(e){}
			finally
			{
				document.all("printmenu").style.display="";
			}
		}
		function doPrint() 
		{
		    var labelPrintTime = document.getElementById("labelPrintTime");
		    var  now  =  new  Date();
		    var  year = now.getFullYear();
		    var  month = now.getMonth();
		    var  day = now.getDate();
            var  hours  =  now.getHours();
            var  minutes  =  now.getMinutes();
            var DateTime = year+"-"+month+"-"+day+" "+hours+":"+minutes;

		    //labelPrintTime.value = DateTime;

            bdhtml=window.document.body.innerHTML;    
            sprnstr="<!--startprint-->";    
            eprnstr="<!--endprint-->";    
            prnhtml=bdhtml.substr(bdhtml.indexOf(sprnstr)+65);    
            prnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr));    
            window.document.body.innerHTML=prnhtml;    
            window.print();
            
        } 
        function IsDate(obj)
        {          
            reg=new RegExp("^(19|20)[0-9]{2}\-[0-9]{2}\-[0-9]{2}$");
            if(reg.test(obj.value)==false)
            {
	            alert('日期格式:yyyy-MM-DD\r如:1999-01-01\r或:2005-06-21');
	            obj.focus();
	            obj.select();
            }
        }
    </script>

    <script type="text/javascript">
        var FirstTimesFlag = true; //统计次数
        //当用户录入时进行查询
        function SelectClient(obj, num) {
            if (!FirstTimesFlag) {
                FirstTimesFlag = true;
                return;
            }
            SelectRow = -1; //每次查询之前将选中行置为-1
            var topObj = obj;
            var ttop = obj.offsetTop; //TT控件的定位点高
            var tleft = obj.offsetLeft; //TT控件的定位点宽
            var thei = obj.clientHeight; //TT控件本身的高

            var ttyp = obj.type; //TT控件的类型
            while (topObj = topObj.offsetParent) { ttop += topObj.offsetTop; tleft += topObj.offsetLeft; }
            ttop = (ttyp == "image") ? ttop + thei : ttop + thei + 6;
            tleft = tleft;


            var Sys = {};
            var ua = navigator.userAgent.toLowerCase();
            if (window.ActiveXObject) {
                Sys.ie = ua.match(/msie ([\d.]+)/)[1]; //获取浏览器版本
            }
            var UpdatePanel1 = document.getElementById("UpdatePanel1");
            UpdatePanel1.style.top = ttop;
            UpdatePanel1.style.left = tleft;


            var hiddenLeft = document.getElementById("hiddenLeft");
            hiddenLeft.value = tleft;
            var hiddenTop = document.getElementById("hiddenTop");
            hiddenTop = ttop;

            var hiddenUserInput = document.getElementById("hiddenUserInput");
            hiddenUserInput.value = obj.value;


            if (num === 1) {
                if (obj.value == null || obj.value == "") {
                    var hiddenSelectedClient = document.getElementById("hiddenSelectedClient");
                    hiddenSelectedClient.value = "";

                    var tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
                    if (tableUserInputSJDWList == null || tableUserInputSJDWList.childNodes.length <= 0) {
                        return;
                 
                    }
                    var nodes = tableUserInputSJDWList.childNodes[0].childNodes;
                    for (var i = nodes.length - 1; nodes.length > 0; i--) {
                        tableUserInputSJDWList.childNodes[0].removeChild(nodes[i]);
                    }
                    return;
                }
                if (FirstTimesFlag) {
                    var btnSearchSJDW = document.getElementById("btnSearchSJDW");
                    btnSearchSJDW.click();
                }
            }
            if (num === 2) {
                if (obj.value == null || obj.value == "") {
                    var tableBSC = document.getElementById("tableBSC");
                    var nodes = tableBSC.childNodes[0].childNodes;
                    for (var i = nodes.length - 1; nodes.length > 0; i--) {
                        tableBSC.childNodes[0].removeChild(nodes[i]);
                    }

                    return;
                }
                if (FirstTimesFlag) {
                    var btnSearchBSC = document.getElementById("btnSearchBSC");
                    btnSearchBSC.click();
                }
            }
            //		if(!FirstTimesFlag)
            //		{
            //		    FirstTimesFlag = !FirstTimesFlag
            //		}
        }
        var SelectRow = -1; //当按下键盘上 上下  键时选中的行编号
        function SelectNextClient(obj, num) {
            //        var Div_Input = document.getElementById("Div_Input");
            //        if(Div_Input.style.display != "none")
            //        {
            var tableUserInputSJDWList = null;
            if (num === 1) {
                tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
            }
            if (num === 2) {
                tableUserInputSJDWList = document.getElementById("tableBSC");
            }
            var totalRow = tableUserInputSJDWList.rows.length;
            if (totalRow > 0) {
                //event.keyCode=38   向上
                if (event.keyCode == 38) {
                    if (SelectRow != -1) {
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#a3f1f5';
                    }
                    if (SelectRow == 0 || SelectRow == -1) {
                        SelectRow = totalRow - 1;
                    }
                    else {
                        SelectRow = SelectRow - 1;
                    }
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#AAA';
                }
                //event.keyCode=40   向下
                if (event.keyCode == 40) {
                    if (SelectRow != -1) {
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#a3f1f5';
                    }
                    if (SelectRow == (totalRow - 1)) {
                        SelectRow = 0;
                    }
                    else {
                        SelectRow = SelectRow + 1;
                    }
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor = '#AAA';
                }
                //event.keyCode=13   回车键
                if (event.keyCode == 13) {
                    if (SelectRow != -1) {
                        tableUserInputSJDWList.rows[SelectRow].click();
                    }
                }
            }
            //        }
        }

        //单击查询到送检单位，进行选择
        function GetClient(id, name) {
            FirstTimesFlag = false;
            var obj = document.getElementById("inputName");
            obj.value = name;
            var hiddenSelectedClient = document.getElementById("hiddenSelectedClient");
            hiddenSelectedClient.value = id;
            try {
                var tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
                var nodes = tableUserInputSJDWList.childNodes[0].childNodes;
                for (var i = nodes.length - 1; nodes.length > 0; i--) {
                    tableUserInputSJDWList.childNodes[0].removeChild(nodes[i]);
                }
            }
            catch (e) {
                alert(e.description());
            }
            SelectRow = -1; //恢复选中行号为-1
        }
        function GetBSC(CName) {
            FirstTimesFlag = false;
            var obj = document.getElementById("inputBSC");
            obj.value = CName;
            try {
                var tableBSC = document.getElementById("tableBSC");
                var nodes = tableBSC.childNodes[0].childNodes;
                for (var i = nodes.length - 1; nodes.length > 0; i--) {
                    tableBSC.childNodes[0].removeChild(nodes[i]);
                }
            }
            catch (e) {
                alert(e.description());
            }
            SelectRow = -1; //恢复选中行号为-1
        }
    </script>
</head>
<body style="background-color: #e1f3ff">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div class="prt" id="printmenu" align="left">
        <table>
            
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="ListPrint" class="prt" runat="server" AccessKey="P" OnClick="ListPrint_Click"
                                Text="打印[P]" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Button TabIndex="4" ID="btnSearch" runat="server" Text="查询[S]" AccessKey="S"
                        OnClick="btnSearch_Click" /><span style="color: Red;">打印后，如果需要重新打印需要刷新本页面</span>
                </td>
            </tr>
        </table>
        <table width="98%" height="50" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    送检单位：<input type="text" id="inputName" onkeydown="SelectNextClient(this,1)" onpropertychange="SelectClient(this,1)"
                        runat="server" />
                        <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div id="Div_Input" style="position: absolute; z-index: 9998; width: 220;" runat="server">
                                    <table id="tableUserInputSJDWList" border="1" width="220" cellpadding="0" cellspacing="0"
                                        runat="server">
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    
                </td>
                
                <td>
                    姓    名：<asp:TextBox TabIndex="0" ID="txtPatientName" Width="100" runat="server"></asp:TextBox>
                </td>
                <td>
                    病 历 号：<asp:TextBox TabIndex="0" ID="txtPatientID" Width="100" runat="server"></asp:TextBox>
                 
                </td>
            </tr><tr>
                <td>
                     送检医生：<asp:TextBox TabIndex="0" ID="txtDoctorName" Width="100" runat="server"></asp:TextBox>
                </td>
                <td colspan=2>
                    诊    断：<asp:TextBox TabIndex="0" ID="txtMemo" Width="100" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="380" colspan=3>
                    采样时间：<input onchange="IsDate(this);" onfocus="setday(this)" type="text" class="inputText"
                        id="txtCollectStartDate" runat="server" />-<input onchange="IsDate(this);" onfocus="setday(this)"
                            type="text" class="inputText" id="txtCollectEndDate" runat="server" />
                    
                </td>
            </tr>
            <tr>
                <td width="380" colspan=3>
                    开单时间：<input onfocus="setday(this)" type="text" class="inputText" id="txtStartDate"
                        runat="server" size=10 />-<input onchange="IsDate(this);" onfocus="setday(this)" type="text"
                            class="inputText" id="txtEndDate" runat="server" size=10 />
                </td>
            </tr>
        </table>
    </div>
    <div style="display: ;">
        <asp:UpdatePanel ID="updatepanel3" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnSearchSJDW" runat="server" Width="0" Style="display: none;" OnClick="btnSearchSJDW_Click" />
                <asp:Button ID="btnSearchBSC" runat="server" Width="0" Style="display: none;" Text="办事处"
                    OnClick="btnSearchBSC_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--startprint-->
    <link href="SubScriptCss.css" rel="stylesheet" type="text/css" media="all" />
    <div>
        <table border="0" width="98%">
            <tr>
                <td width="30%">
                    清单号：<label id="labelListNo" class="PrintTitle" runat="server"></label>
                </td>
                <td width="20%">
                    机构：<label id="labelDepartment" class="PrintTitle" runat="server"></label>
                </td>
                <td width="50%">
                    打印时间：<label id="labelPrintTime" class="PrintTitle" runat="server"></label>&nbsp;&nbsp;&nbsp;&nbsp;
                    打印人：<%=loginName %>
                </td>
            </tr>
        </table>
        <table border="1" cellpadding="0" cellspacing="0" width="98%" class="pad" id="List_Table" >
            <tr bgcolor="#c3c1c5">
                <td nowrap="nowrap" class="pad1" >
                    条码号
                </td>
                <td nowrap="nowrap" class="pad1" >
                    姓名
                </td>
                <td nowrap="nowrap" class="pad1" >
                    性别
                </td>
                <td nowrap="nowrap" class="pad1" >
                    年龄
                </td>
                <td nowrap="nowrap" class="pad1" >
                    办事处
                </td>
                <td nowrap="nowrap" class="pad1" >
                    送检单位
                </td>
                <td nowrap="nowrap" class="pad1" >
                    医生
                </td>
                <td nowrap="nowrap" class="pad1" >
                    采样时间
                </td>
                <td nowrap="nowrap" class="pad1" >
                    检测项目
                </td>
                <td nowrap="nowrap" class="pad1" >
                    金额
                </td>
                <td nowrap="nowrap" class="pad1" >
                    诊断
                </td>
                <!--td nowrap="nowrap">
                    样本类型
                </td>
                <td nowrap="nowrap">
                    站点名称
                </td>
                <td nowrap="nowrap">
                    开单时间
                </td-->
            </tr>
            <%
                try
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            count += Convert.ToDouble(dt.Rows[i]["TestItemPrice"].ToString());
                        }
                        catch
                        {
                            count += 0;
                        }
                        %>
            <tr bgcolor="#e1f3ff">
                <td nowrap="nowrap" class="pad1" >
                    <%=dt.Rows[i]["BarCode"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap" class="pad1" >
                    <%=dt.Rows[i]["CName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap" class="pad1" >
                    <%=dt.Rows[i]["GenderName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap" class="pad1" >
                    <%
                        string Age = dt.Rows[i]["Age"].ToString();
                        if (Age != "")
                        {
                            Age += dt.Rows[i]["AgeUnitName"].ToString(); 
                            Response.Write(Age);
                        }
                    %>&nbsp;
                </td>
                <td nowrap="nowrap" class="pad1" >
                    <%=dt.Rows[i]["CLIENTELEGroupName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap" class="pad1" >
                    <%=dt.Rows[i]["WebLisSourceOrgName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap" class="pad1" >
                    <%=dt.Rows[i]["DoctorName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap" class="pad1" >
                    <%
                        try
                        {
                            DateTime dtCollect = DateTime.Parse(dt.Rows[i]["CollectTime"].ToString());
                            Response.Write(dtCollect.ToString("yy-MM-dd HH:mm"));
                        }
                        catch
                        { }
                    %>&nbsp;
                </td>
                <td class="pad1" >
                    <%
                        string BarCodeFormNo = dt.Rows[i]["BarCodeFormNo"].ToString().Trim();
                        string TestItems = "";
                        for (int t = 0; t < dtTestItem.Rows.Count; t++)
                        {
                            string testBarCodeFormNo = dtTestItem.Rows[t][1].ToString().Trim();
                            if (testBarCodeFormNo == BarCodeFormNo)
                            {
                                TestItems += dtTestItem.Rows[t][0].ToString().Trim() + ";";
                            }
                        }
                        //截取字符串
                        NewsWebService.RBACFunctions nws = new RBACFunctions();
                        //TestItems = nws.StringLength(TestItems, 8);
                        Response.Write(TestItems);
                    %>&nbsp;
                </td>
                <td align="right" class="pad1" >
                    <%=dt.Rows[i]["TestItemPrice"].ToString()%>
                </td>
                <td class="pad1" >
                    <%
                        try
                        {
                            string Diag = dt.Rows[i]["Diag"].ToString();
                            Diag = nws.StringLength(Diag, 6);
                            Response.Write(Diag);
                        }
                        catch
                        { }
                    %>&nbsp;
                </td>
                <!--td nowrap="nowrap">TestItemPrice
                    <%=dt.Rows[i]["SampleTypeCName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["ClientName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["OperTime"].ToString()%>&nbsp;
                </td-->
            </tr>
            <%}
                }
                catch
                {
                    ECDS.Util.JScript.Alert(this.Page, "查询出错，无法显示！");
                }
            %>
            <tr style=" font-weight:bold" bgcolor="#e1f3ff" class="pad1" ><td colspan="8" class="pad1" >合计：</td><td colspan="2" align="right" class="pad1" ><% Response.Write(count); %></td><td class="pad1" >&nbsp;</td></tr>
        </table>
        <table width="98%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="width: 80;" align="right">
                    送检人：
                </td>
                <td style="width: 80;">
                    &nbsp;
                </td>
                <td style="width: 80;" align="right">
                    签收人：
                </td>
                <td style="width: 80;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="width: 80px;" align="right">
                    送检时间：
                </td>
                <td style="width: 80px;">
                    &nbsp;
                </td>
                <td style="width: 80px;" align="right">
                    签收时间：
                </td>
                <td style="width: 80px;">
                    &nbsp;
                </td>
            </tr>
        </table>
        <!--endprint-->
    </div>
    <!-- 用户选中的送检单位编号 -->
    <input type="hidden" id="hiddenSelectedClient" runat="server" />
     <!-- 用户在文本框中输入的内容 -->
    <input type="hidden" id="hiddenUserInput" runat="server" />
    <!-- 对照控件的横坐标 -->
    <input type="hidden" id="hiddenLeft" runat="server" />
    <!-- 对照控件的纵坐标 -->
    <input type="hidden" id="hiddenTop" runat="server" />
    <!-- 用户差找到的全部条码 -->
    <input type="hidden" id="hiddenAllBarCode" runat="server" />
    </form>
</body>
</html>
