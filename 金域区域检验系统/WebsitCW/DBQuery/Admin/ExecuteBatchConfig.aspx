<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Admin.ExecuteBatchConfig" Codebehind="ExecuteBatchConfig.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>��������</title>
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
						//alert("û��ѡ��Ҫ�Ƴ�����");
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
			
			//=======================���ȷ��=============
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
			
			//=======================������ˮ��==========
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
				
				//============��ˮ����===================
				if(pipeRule.value == "")
				{
					alert("��û��������ˮ����!");
					return;
					//�鿴�Ƿ��в������
					if(pipeRule.value.indexOf("������ˮ�š�") == -1)
					{
						alert("û�в����滻����");
						return;
					}
				}
				//=================End==================
				
				//===============��ˮ��====================
				if(pipeNum.value == "")
				{
					alert("û��������ˮ��");
					return;
				}
				else
				{
					if(isNaN(pipeNum.value))
					{
						alert("��ˮ��Ҫ��������");
						return;
					}
				}
				//================End====================
				
				//===============��ʼ��=================
				if(pipeStartNum.value == "")
				{
					alert("û��������ʼ��");
					return;
				}
				else
				{
					if(isNaN(pipeStartNum.value))
					{
						alert("��ʼ��Ҫ��������");
						return;
					}
				}
				//================End================
				
				//================λ��=================
				if(pipeBNum.value == "")
				{
					alert("û������λ��");
					return;
				}
				else
				{
					if(isNaN(pipeBNum.value))
					{
						alert("λ��Ҫ��������");
						return;
					}
				}
				//=================End================
				
				for(var index=listBatch.options.length-1; index>=0; index--)//����б�
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
			
			function InsertChar()//�������
			{
				if(document.Form1.all["tablePipe"].disabled == true)
				 return;
				 
				var str = Form1.pipeFunc.value;
				if(str.indexOf("������ˮ�š�") != -1)
				{
					alert("�Ѿ����ڲ����");
					return;
				}
				Form1.pipeFunc.focus();
				document.execCommand('Paste',true,"������ˮ�š�");
			}
			
			//================��ˮ�����ɹ���======================
			function RuleBuilder(rule, startNum, currNum, bitNum)
			{
				var displayChar="";
				var returnRule="";
				var displayNum = parseInt(currNum) + parseInt(startNum) -1;
				if((displayNum).toString().length < bitNum)//С��λ��������
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
				
				returnRule = rule.replace(/������ˮ�š�/i, displayChar);
				
				return returnRule;
			} 
			//=====================End============================
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="tablePipe" width="90%" align="center" runat="server">
				<tr>
					<td noWrap>��ˮ����</td>
					<td noWrap colSpan="3"><input id="pipeFunc" type="text" size="25" name="pipeFunc">
					</td>
					<td align="left" colSpan="3"><input onclick="InsertChar()" type="button" value="������ˮ��" name="btnInsert"></td>
				</tr>
				<tr>
					<td colSpan="7">���磺</td>
				</tr>
				<tr>
					<td noWrap>��ʼ��</td>
					<td noWrap><input type="text" size="8" name="pipeStartNum"></td>
					<td noWrap>��ˮ��</td>
					<td><input id="pipeNum" type="text" size="8" name="pipeNum"></td>
					<td noWrap>λ��</td>
					<td><input type="text" size="8" name="pipeBitNum"></td>
					<td><input onclick="GeneratePipeNum()" type="button" value="������ˮ��"></td>
				</tr>
			</table>
			<br>
			<table width="90%" align="center">
				<tr>
					<td width="40%"><asp:listbox id="listBatchAllField" Width="100%" Runat="server" Height="250" SelectionMode="Multiple"></asp:listbox></td>
					<td vAlign="middle" align="center" width="20%"><input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchAddAllField()" type="button" value="���ȫ��>"
							name="batchAddAll">
						<br>
						<input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchAddField()" type="button" value="-->>"
							name="batchAdd">
						<br>
						<input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchRemoveField()" type="button" value="<<--"
							name="batchRemove">
						<br>
						<input style="WIDTH: 95px; HEIGHT: 24px" onclick="BatchRemoveAllField()" type="button"
							value="<ȫ���Ƴ�" name="batchRemoveAll">
					</td>
					<td width="40%"><asp:listbox id="listBatchSelectedField" Width="100%" Runat="server" Height="250" SelectionMode="Multiple"></asp:listbox></td>
				</tr>
			</table>
			<br>
			<br>
			<table width="80%" align="center" border="0">
				<tr>
					<td align="center"><input onclick="return ConfirmClick()" type="button" value="ȷ��" name="btnConfirm"></td>
					<td align="center"><input onclick="return window.parent.close()" type="button" value="ȡ��" name="btnCancel"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
