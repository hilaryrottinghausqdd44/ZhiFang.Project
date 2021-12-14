<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.TableIsVisable" Codebehind="TableIsVisable.aspx.cs" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TableIsVisable</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">
			#divBind { BORDER-RIGHT: #000080 1px solid; BORDER-TOP: #000080 1px solid; FONT-SIZE: 12px; BORDER-LEFT: #000080 1px solid; BORDER-BOTTOM: #000080 1px solid }
		</style>
		<script type="text/javascript">
			function SaveClick()
			{
				var chkObj = document.getElementsByName("MyCheck");
				if(chkObj.length==0 || chkObj==undefined)
				{
					return false;
				}
				
				var PostBackValue = new Array(chkObj.length);
				for(var i=0; i<chkObj.length; i++)
				{
					if(chkObj[i].checked)
					{
						//alert("ѡ��");
						PostBackValue[i] = chkObj[i].id + "/1";
					}
					else
					{
						if(chkObj[i].style.backgroundColor == "red")
						{
							//alert("��ɫδѡ��");
							PostBackValue[i] = chkObj[i].id + "/2";
						}
						else
						{
							//alert("δѡ��");
							PostBackValue[i] = chkObj[i].id + "/0";
						}
					}
				}
				//alert(PostBackValue.join(";"));
				document.getElementById("tableNameHidden").value = PostBackValue.join(";"); //�ŵ����ؿؼ���
				return true;
			}
			//==========CheckBox��ѡ��㼫=====
			function CheckBoxClick(obj)
			{
				var chkObj = document.getElementsByName("MyCheck");
				var tableLength = obj.id.split("/").length;
				
				if(obj.checked)
				{
					if(obj.style.backgroundColor == "red")
					//ѡ��
					{
						
						obj.style.backgroundColor = "";
						//===============�ӱ�ѡ�е������==============
						for(var j=0; j<chkObj.length; j++)
						{
							if(tableLength > chkObj[j].id.split("/").length)
							{
								chkObj[j].style.backgroundColor = "";
								chkObj[j].checked = true;
							}
						}
					}
					else
					{
						//===========��ɫδѡ��==========
						obj.style.backgroundColor = "red";
						obj.checked = false;
						
						//=======================
						
						for(var i=0; i<chkObj.length; i++)
						{
							if(tableLength < chkObj[i].id.split("/").length)
							{
								chkObj[i].style.backgroundColor = "";
								chkObj[i].checked = false;
							}
						}
					}
				}
				else
				{
					obj.style.backgroundColor = "";
				}
			}
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="divBind" align="left">
				<span style="FONT-WEIGHT: bold">��Ȩ������</span><br>
				<asp:Repeater ID="rptTable" Runat="server">
					<ItemTemplate>
						<asp:PlaceHolder ID="plhImage" Runat="server"></asp:PlaceHolder>
						<span id="spanNav" runat="server" style="Cursor:pointer; Cursor:hand; COLOR:DarkBlue;">
						</span>
					</ItemTemplate>
					<SeparatorTemplate>
						<br>
					</SeparatorTemplate>
				</asp:Repeater>
			</div>
			<div align="center">
				<asp:Button ID="btnSave" Runat="server" Text="����" onclick="btnSave_Click"></asp:Button>
			</div>
			<input type="hidden" runat="server" id="tableNameHidden">
		</form>
	</body>
</HTML>
