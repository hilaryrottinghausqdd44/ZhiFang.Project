<%@ Import Namespace="NewsWebService" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubScriptPrintList.aspx.cs"
    Inherits="OA.DBQuery.RunExec.SubScriptPrintList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��ӡ�嵥</title>
    <style type="text/css">
        .PrintTitle
        {
            border-bottom-width: thin;
            border-bottom-style: solid;
            border-left-style: none;
            border-right-style: none;
            border-top-style: none;
        }
    </style>

    <script type="text/javascript" src="../../Includes/JS/calendarDateTime.js"></script>

    <script type="text/javascript"> 
    var FirstTimesFlag = true;//ͳ�ƴ���
    //���û�¼��ʱ���в�ѯ
    function SelectClient(obj,num)
    {
        if(!FirstTimesFlag)
        {
            FirstTimesFlag = true;
            return;
        }
        SelectRow = -1;//ÿ�β�ѯ֮ǰ��ѡ������Ϊ-1
        var topObj = obj;
        var ttop = obj.offsetTop;	//TT�ؼ��Ķ�λ���
        var tleft = obj.offsetLeft;	//TT�ؼ��Ķ�λ���
	    var thei = obj.clientHeight;	//TT�ؼ�����ĸ�
	    
	    var ttyp = obj.type;	//TT�ؼ�������
	    while (topObj = topObj.offsetParent){ttop+=topObj.offsetTop; tleft+=topObj.offsetLeft;}
	    ttop = (ttyp=="image") ? ttop+thei : ttop+thei+6;
	    tleft = tleft;

	    
	    var Sys = {};
        var ua = navigator.userAgent.toLowerCase();
        if (window.ActiveXObject) {
            Sys.ie = ua.match(/msie ([\d.]+)/)[1];//��ȡ������汾
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
        
       
        if(num === 1)
        {
            if(obj.value == null || obj.value == "")
            {
                var hiddenSelectedClient = document.getElementById("hiddenSelectedClient");
                hiddenSelectedClient.value = "";

                var tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
                if (tableUserInputSJDWList == null || tableUserInputSJDWList.childNodes.length <= 0) {
                    return;
                }
                var nodes = tableUserInputSJDWList.childNodes[0].childNodes; 
                for(var i=nodes.length-1;nodes.length>0;i--) 
                { 
                    tableUserInputSJDWList.childNodes[0].removeChild(nodes[i]); 
                }
                return;
            }
            if(FirstTimesFlag)
            {
                var btnSearchSJDW = document.getElementById("btnSearchSJDW");
		        btnSearchSJDW.click();
		    }
		}
		if(num === 2)
		{
		    if(obj.value == null || obj.value == "")
            {
                var tableBSC = document.getElementById("tableBSC");
                if (tableBSC == null || tableBSC.childNodes.length <= 0) {
                    return;
                }
                var nodes = tableBSC.childNodes[0].childNodes; 
                for(var i=nodes.length-1;nodes.length>0;i--) 
                { 
                    tableBSC.childNodes[0].removeChild(nodes[i]); 
                }
                
                return;
            }
            if(FirstTimesFlag)
            {
		        var btnSearchBSC = document.getElementById("btnSearchBSC");
		        btnSearchBSC.click();
		    }
		}
//		if(!FirstTimesFlag)
//		{
//		    FirstTimesFlag = !FirstTimesFlag
//		}
    }
    var SelectRow = -1;//�����¼����� ����  ��ʱѡ�е��б��
    function SelectNextClient(obj,num)
    {
//        var Div_Input = document.getElementById("Div_Input");
//        if(Div_Input.style.display != "none")
//        {
            var tableUserInputSJDWList = null;
            if(num === 1)
            {
                tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
            }
            if(num === 2)
            {
                tableUserInputSJDWList = document.getElementById("tableBSC");
            }
            var totalRow = tableUserInputSJDWList.rows.length;
            if(totalRow>0)
            {
                //event.keyCode=38   ����
                if(event.keyCode == 38)
                {
                    if(SelectRow != -1)
                    {
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#a3f1f5';
                    }
                    if(SelectRow == 0 || SelectRow == -1)
                    {
                        SelectRow = totalRow-1;
                    }
                    else
                    {
                        SelectRow = SelectRow - 1;
                    }
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#AAA';
                }
                //event.keyCode=40   ����
                if(event.keyCode == 40)
                {
                    if(SelectRow != -1)
                    {
                        tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#a3f1f5';
                    }
                    if(SelectRow == (totalRow-1))
                    {
                        SelectRow = 0;
                    }
                    else
                    {
                        SelectRow = SelectRow + 1;
                    }
                    tableUserInputSJDWList.rows[SelectRow].style.backgroundColor='#AAA';
                }
                //event.keyCode=13   �س���
                if(event.keyCode == 13)
                {
                    if(SelectRow != -1)
                    {
                        tableUserInputSJDWList.rows[SelectRow].click();
                    }
                }
            }
//        }
    }
    
    //������ѯ���ͼ쵥λ������ѡ��
    function GetClient(id,name)
    {
        FirstTimesFlag = false;
        var obj = document.getElementById("inputName");
        obj.value = name;
        var hiddenSelectedClient = document.getElementById("hiddenSelectedClient");
        hiddenSelectedClient.value = id;
        try
        {
            var tableUserInputSJDWList = document.getElementById("tableUserInputSJDWList");
            var nodes = tableUserInputSJDWList.childNodes[0].childNodes; 
            for(var i=nodes.length-1;nodes.length>0;i--) 
            { 
                tableUserInputSJDWList.childNodes[0].removeChild(nodes[i]); 
            }
        }
        catch(e)
        {
            alert(e.description());
        }
        SelectRow = -1;//�ָ�ѡ���к�Ϊ-1
    }
    function GetBSC(CName)
    {
        FirstTimesFlag = false;
        var obj = document.getElementById("inputBSC");
        obj.value = CName;
        try
        {
            var tableBSC = document.getElementById("tableBSC");
            var nodes = tableBSC.childNodes[0].childNodes; 
            for(var i=nodes.length-1;nodes.length>0;i--) 
            { 
                tableBSC.childNodes[0].removeChild(nodes[i]); 
            }
        }
        catch(e)
        {
            alert(e.description());
        }
        SelectRow = -1;//�ָ�ѡ���к�Ϊ-1
    }
    </script>

    <style type="text/css">
        .titleFont
        {
            font-family: "����";
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
            font-family: "����";
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

    <script language="javascript">  
����	function printsetup(){  
����	// ��ӡҳ������  
����	window.document.wb.ExecWB(8,1);  
����	}  
����	function printpreview()
����	{  
����	    // ��ӡҳ��Ԥ��  
����	    //wb.ExecWB(7,1);
����	    doPrint();
����	}  

����	function printit()  
����	{  
����		if (confirm('ȷ����ӡ��')) 
����		{  
����			wb.ExecWB(6,1)  
����		}  
����	}
	����
����	function Print(cmdid,cmdexecopt)
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
					alert('��ӡ���');
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
	            alert('���ڸ�ʽ:yyyy-MM-DD\r��:1999-01-01\r��:2005-06-21');
	            obj.focus();
	            obj.select();
            }
        }
    </script>

</head>
<body style="background-color: #e1f3ff">
    <form id="form1" runat="server">
    <div class="prt" id="printmenu" align="left">
        <asp:ScriptManager ID="ScriptManage1" runat="server">
        </asp:ScriptManager>
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="ListPrint" runat="server" OnClick="ListPrint_Click" AccessKey="P"
                                Text="��ӡ[P]" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Button TabIndex="4" ID="btnSearch" runat="server" Text="��ѯ[S]" AccessKey="S"
                        OnClick="btnSearch_Click" /><span style="color: Red;">��ӡ�������Ҫ���´�ӡ��Ҫˢ�±�ҳ��</span>
                </td>
            </tr>
        </table>
        <table width="98%" height="50" border="0" cellpadding="0" cellspacing="0">
            <tr height="25">
                <td nowrap="nowrap">
                    �ͼ쵥λ��<input type="text" id="inputName" onkeydown="SelectNextClient(this,1)" onpropertychange="SelectClient(this,1)"
                        runat="server" />
                        <div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                    �� �� ����<input type="text" id="inputBSC" onkeydown="SelectNextClient(this,2)" onpropertychange="SelectClient(this,2)"
                        runat="server" />
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div id="Div_BSC" style="position: absolute; z-index: 9998; width: 220;" runat="server">
                                    <table id="tableBSC" border="1" width="220" cellpadding="0" cellspacing="0" runat="server">
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
                <td>
                    ��    ����<asp:TextBox TabIndex="0" ID="txtPatientName" runat="server"></asp:TextBox>
                </td>
                
            </tr>
            <tr height="25">
                <td>
                    �� �� �ţ�<asp:TextBox TabIndex="0" ID="txtPatientID" runat="server"></asp:TextBox>
                </td>
                <td>
                    �ͼ�ҽ����<asp:TextBox TabIndex="0" ID="txtDoctorName" runat="server"></asp:TextBox>
                </td><td>
                    ��    �ϣ�<asp:TextBox TabIndex="0" ID="txtMemo" runat="server"></asp:TextBox>
                </td>
            </tr>
             <tr height="25">
                <td colspan=3 nowrap=nowrap>
                    ����ʱ�䣺<input onchange="IsDate(this);" onfocus="setday(this)" type="text" class="inputText"
                        id="txtCollectStartDate" runat="server" />-<input onchange="IsDate(this);" onfocus="setday(this)"
                            type="text" class="inputText" id="txtCollectEndDate" runat="server" />
                            
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                 ����ʱ�䣺<input onfocus="setday(this)" type="text" class="inputText" id="txtStartDate"
                        runat="server" />-<input onchange="IsDate(this);" onfocus="setday(this)" type="text"
                            class="inputText" id="txtEndDate" runat="server" />
                
                </td>
              
            </tr>
        </table>
    </div>
    <div style="display: none;">
        <asp:UpdatePanel ID="updatepanel2" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnSearchSJDW" runat="server" Width="0" Style="display: none;" OnClick="btnSearchSJDW_Click" />
                <asp:Button ID="btnSearchBSC" runat="server" Width="0" Style="display: none;" Text="���´�"
                    OnClick="btnSearchBSC_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <!--startprint-->
    <link href="SubScriptCss.css" rel="stylesheet" type="text/css" media="all" />
    <div>
        <table border="0" width="98%">
            <tr>
                <td width="30%">
                    �嵥�ţ�<label id="labelListNo" class="PrintTitle" runat="server"></label>
                </td>
                <td width="20%">
                    ������<label id="labelDepartment" class="PrintTitle" runat="server"></label>
                </td>
                <td width="50%">
                    ��ӡʱ�䣺<label id="labelPrintTime" class="PrintTitle" runat="server"></label>&nbsp;&nbsp;&nbsp;&nbsp;
                     ��ӡ�ˣ�<%=loginName %>
                </td>
            </tr>
        </table>
        <table border="1" cellpadding="0" cellspacing="0" width="98%" class="pad" id="List_Table">
            <tr bgcolor="#c3c1c5">
                <td nowrap="nowrap">
                    �����
                </td>
                <td nowrap="nowrap">
                    ����
                </td>
                <td nowrap="nowrap">
                    �Ա�
                </td>
                <td nowrap="nowrap">
                    ����
                </td>
                <td nowrap="nowrap">
                    ���´�
                </td>
                <td nowrap="nowrap">
                    �ͼ쵥λ
                </td>
                <td nowrap="nowrap">
                    ҽ��
                </td>
                <td nowrap="nowrap">
                    ����ʱ��
                </td>
                <td nowrap="nowrap">
                    �����Ŀ
                </td>
                <td nowrap="nowrap">
                    ���
                </td>
                <td nowrap="nowrap">
                    ���
                </td>
                <!--td nowrap="nowrap">
                    ��������
                </td>
                <td nowrap="nowrap">
                    վ������
                </td>
                <td nowrap="nowrap">
                    ����ʱ��
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
            <tr>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["BarCode"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["CName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["GenderName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%
                        string Age = dt.Rows[i]["Age"].ToString();
                        if (Age != "")
                        {
                            Age += dt.Rows[i]["AgeUnitName"].ToString(); 
                            Response.Write(Age);
                        }
                    %>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["CLIENTELEGroupName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["WebLisSourceOrgName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["DoctorName"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
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
                <td>
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
                        //��ȡ�ַ���
                        NewsWebService.RBACFunctions nws = new RBACFunctions();
                        //TestItems = nws.StringLength(TestItems, 8);
                        Response.Write(TestItems);
                    %>&nbsp;
                </td>
                <td nowrap="nowrap">
                    <%=dt.Rows[i]["TestItemPrice"].ToString()%>&nbsp;
                </td>
                <td nowrap="nowrap">
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
                    ECDS.Util.JScript.Alert(this.Page, "��ѯ�����޷���ʾ��");
                }
            %>
            <tr style=" font-weight:bold"><td colspan="9">�ϼƣ�</td><td colspan="2"><% Response.Write(count); %></td></tr>
        </table>
        <table width="98%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="width: 80;" align="right">
                    �ͼ��ˣ�
                </td>
                <td style="width: 80;">
                    &nbsp;
                </td>
                <td style="width: 80;" align="right">
                    ǩ���ˣ�
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
                    �ͼ�ʱ�䣺
                </td>
                <td style="width: 80px;">
                    &nbsp;
                </td>
                <td style="width: 80px;" align="right">
                    ǩ��ʱ�䣺
                </td>
                <td style="width: 80px;">
                    &nbsp;
                </td>
            </tr>
        </table>
        <!--endprint-->
    </div>
    <!-- �û����ı�������������� -->
    <input type="hidden" id="hiddenUserInput" runat="server" />
    <!-- �û�ѡ�е��ͼ쵥λ��� -->
    <input type="hidden" id="hiddenSelectedClient" runat="server" />
    <!-- ���տؼ��ĺ����� -->
    <input type="hidden" id="hiddenLeft" runat="server" />
    <!-- ���տؼ��������� -->
    <input type="hidden" id="hiddenTop" runat="server" />
    <!-- �û����ҵ���ȫ������ -->
    <input type="hidden" id="hiddenAllBarCode" runat="server" />
    </form>
</body>
</html>
