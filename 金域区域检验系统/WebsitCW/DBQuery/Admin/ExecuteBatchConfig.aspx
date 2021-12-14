<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.ExecuteBatchConfig" Codebehind="ExecuteBatchConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>批量配置</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../css/ioffice.css" type="text/css" rel="stylesheet">
		<script language="javascript">
			function BatchAddField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var listLength = field.options.length;
				
				for(var i=0; i<listLength; i++)
				{
					
					if(field.selectedIndex != -1)
					{
						var selectedText = field.options[field.selectedIndex].text;
						var obj = new Option(selectedText, "", "", "");
						fieldSelected.options[fieldSelected.options.length] = obj;
						field.options[field.selectedIndex] = null;
					}
					
				}
			}
			
			function BatchRemoveField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var listLength = fieldSelected.options.length;
				
				for(var i=0; i<listLength; i++)
				{
					if(fieldSelected.selectedIndex != -1)
					{
						var selectedText = fieldSelected.options[fieldSelected.selectedIndex].text;
						var obj = new Option(selectedText, "", "", "");
						field.options[field.options.length] = obj;
						fieldSelected.options[fieldSelected.selectedIndex] = null;
					}
					else
					{
						//alert("没有选择要移除的项");
					}
				}
			}
			
			function BatchAddAllField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var obj;
				var selectedText;
				
				for(var i=0; i<field.options.length; i++)
				{
					selectedText = field.options[i].text;
					obj = new Option(selectedText, "", "", "");
					fieldSelected.options[fieldSelected.options.length] = obj;
					//field.options[i] = null;
				}
			
				for(var index=field.options.length-1; index>=0; index--)
				{
					field.options[index] = null;
				}
			}
			
			function BatchRemoveAllField()
			{
				var field = document.Form1.all["listBatchAllField"];
				var fieldSelected = document.Form1.all["listBatchSelectedField"];
				var obj;
				var selectedText;
				
				for(var i=0; i<fieldSelected.options.length; i++)
				{
					selectedText = fieldSelected.options[i].text;
					obj = new Option(selectedText, "", "", "");
					field.options[field.options.length] = obj;
					//field.options[i] = null;
				}
			
				for(var index=fieldSelected.options.length-1; index>=0; index--)
				{
					fieldSelected.options[index] = null;
				}
			}
			
			//=======================点击确定=============
			function ConfirmClick()
			{
				var r = "";
				var obj = document.Form1.all["listBatchSelectedField"];
				for(var i=0; i<obj.length; i++)
				{
					r += obj.options[i].text + ","; 
				}
				
				if(r.charAt(r.length-1) == ",")
				{
					r = r.substring(0, r.length-1);
				}
				
				window.parent.returnValue = r;
				window.parent.close();
			}
			
			//=======================生成流水号==========
			function GeneratePipeNum()
			{
				if(document.Form1.all["tablePipe"].disabled == true)
				 return;
				 
				var pipeRule = document.Form1.all["pipeFunc"];
				var pipeNum = document.Form1.all["pipeNum"];
				var pipeStartNum = document.Form1.all["pipeStartNum"];
				var pipeBNum = document.Form1.all["pipeBitNum"];
				var listBatch = document.Form1.all["listBatchAllField"];
			
				var obj;
				
				//============流水规则===================
				if(pipeRule.value == "")
				{
					alert("还没有输入流水规则!");
					return;
					//查看是否有插入符号
					if(pipeRule.value.indexOf("【№流水号】") == -1)
					{
						alert("没有插入替换符号");
						return;
					}
				}
				//=================End==================
				
				//===============流水数====================
				if(pipeNum.value == "")
				{
					alert("没有输入流水数");
					return;
				}
				else
				{
					if(isNaN(pipeNum.value))
					{
						alert("流水数要输入数字");
						return;
					}
				}
				//================End====================
				
				//===============起始数=================
				if(pipeStartNum.value == "")
				{
					alert("没有输入起始数");
					return;
				}
				else
				{
					if(isNaN(pipeStartNum.value))
					{
						alert("起始数要输入数字");
						return;
					}
				}
				//================End================
				
				//================位数=================
				if(pipeBNum.value == "")
				{
					alert("没有输入位数");
					return;
				}
				else
				{
					if(isNaN(pipeBNum.value))
					{
						alert("位数要输入数字");
						return;
					}
				}
				//=================End================
				
				for(var index=listBatch.options.length-1; index>=0; index--)//清空列表
				{
					listBatch.options[index] = null;
				}
					
				for(var i=1; i<=parseInt(pipeNum.value); i++)
				{
					//obj = new Option(pipeRule.value + i, "", "", "");
					obj = new Option(RuleBuilder(pipeRule.value, pipeStartNum.value, i, pipeBNum.value), "", "","");
					listBatch.options[listBatch.length] = obj;
				}
				
			}
			
			function InsertChar()//插入符号
			{
				if(document.Form1.all["tablePipe"].disabled == true)
				 return;
				 
				var str = Form1.pipeFunc.value;
				if(str.indexOf("【№流水号】") != -1)
				{
					alert("已经存在插入符");
					return;
				}
				Form1.pipeFunc.focus();
				document.execCommand('Paste',true,"【№流水号】");
			}
			
			//================流水号生成规则======================
			function RuleBuilder(rule, startNum, currNum, bitNum)
			{
				var displayChar="";
				var returnRule="";
				var displayNum = parseInt(currNum) + parseInt(startNum) -1;
				if((displayNum).toString().length < bitNum)//小于位数，补零
				{
					for(var i=0; i< bitNum-(displayNum).toString().length; i++)
					{
						displayChar += "0";
					}
					displayChar += displayNum;
				}
				else
				{
					displayChar = displayNum;
				}
				
				returnRule = rule.replace(/【№流水号】/i, displayChar);
				
				return returnRule;
			} 
			//=====================End============================
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="tablePipe" width="90%" align="center" runat="server">
				<tr>
					<td noWrap>流水规则</td>
					<td noWrap colSpan="3"><input id="pipeFunc" type="text" size="25" name="pipeFunc">
					</td>
					<td align="left" colSpan="3"><input onclick="InsertChar()" type="button" value="插入流水号" name="btnInsert"></td>
				</tr>
				<tr>
					<td colSpan="7">例如：</td>
				</tr>
				<tr>
					<td noWrap>起始数</td>
					<td noWrap><input type="text" size="8" name="pipeStartNum"></td>
					<td noWrap>流水数</td>
					<td><input id="pipeNum" type="text" size="8" name="pipeNum"></td>
					<td noWrap>位数</td>
					<td><input type="text" size="8" name="pipeBitNum"></td>
					<td><input onclick="GeneratePipeNum()" type="button" value="生成流水号"></td>
				</tr>
			</table>
			<br>
			<table width="90%" align="center">
				<tr>
					<td width="40%"><asp:listbox id="listBatchAllField" Width="100%" Runat="server" Height="250" SelectionMode="Multiple"></asp:listbox></td>
					<td vAlign="middle" align="center" width="20%"><input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchAddAllField()" type="button" value="添加全部>"
							name="batchAddAll">
						<br>
						<input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchAddField()" type="button" value="-->>"
							name="batchAdd">
						<br>
						<input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchRemoveField()" type="button" value="<<--"
							name="batchRemove">
						<br>
						<input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchRemoveAllField()" type="button"
							value="<全部移除" name="batchRemoveAll">
					</td>
					<td width="40%"><asp:listbox id="listBatchSelectedField" Width="100%" Runat="server" Height="250" SelectionMode="Multiple"></asp:listbox></td>
				</tr>
			</table>
			<br>
			<br>
			<table width="80%" align="center" border="0">
				<tr>
					<td align="center"><input onclick="return ConfirmClick()" type="button" value="确定" name="btnConfirm"></td>
					<td align="center"><input onclick="return window.parent.close()" type="button" value="取消" name="btnCancel"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
