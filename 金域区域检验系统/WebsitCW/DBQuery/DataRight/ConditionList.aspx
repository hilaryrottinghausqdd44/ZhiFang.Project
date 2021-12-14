<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.DataRight.ConditionList" Codebehind="ConditionList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ConditionList</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<style type="text/css">TH { FONT-WEIGHT: 600; FONT-SIZE: 15px }
	TD { FONT-SIZE: 12px }
	.InputButton { BORDER-RIGHT: #666666 2px outset; BORDER-TOP: #666666 2px outset; BORDER-LEFT: #666666 2px outset; COLOR: white; BORDER-BOTTOM: #666666 2px outset; BACKGROUND-COLOR: #000080 }
	        .style1
            {
                width: 350px;
                border-collapse: collapse;
                border: 1px solid #008000;
            }
            .style6
            {
                font-size: medium;
                font-weight: bold;
                color: #00CC00;
                font-family: ����;
            }
	        .style7
            {
                width: 10%;
                height: 89px;
            }
            .style8
            {
                width: 90%;
                height: 89px;
            }
            .style9
            {
                width: 10%;
                height: 86px;
            }
            .style10
            {
                width: 90%;
                height: 86px;
            }
            .style11
            {
                font-size: medium;
                font-weight: bold;
                font-family: ����;
                color: #CC0000;
            }
            .style12
            {
                color: #3333CC;
            }
            </style>
		<script language="javascript">
		function Add()
		{
			window.open("AddCondition.aspx?permissionFile="+document.getElementById('tbpermissionFile').value+"&TableEName="+document.getElementById('tableEName').value);
		}
		</script>
</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<div id="divCondition">
				<table style="BORDER-TOP-STYLE: solid; BORDER-RIGHT-STYLE: solid; BORDER-LEFT-STYLE: solid; BORDER-COLLAPSE: collapse; BORDER-BOTTOM-STYLE: solid"
					borderColor="#000080" width="100%" bgColor="#ffffe0" border="2">
					<asp:repeater id="rptCondition" runat="server">
						<HeaderTemplate>
							<thead bgcolor="#000080" style="COLOR: #ffffff">
								<tr>
									<th align="center">
										���</th>
									<th align="center">
										����</th>
									<th align="center">
									</th>
								</tr>
							</thead>
						</HeaderTemplate>
						<ItemTemplate>
							<tr bordercolor="#000080">
								<td align="center">
									<asp:Label ID="lblID" Runat="server"></asp:Label>
								</td>
								<td>
									<asp:Label ID="lblMessage" Runat="server"></asp:Label>
								</td>
								<td>
									<asp:LinkButton ID="btnDelete" text="ɾ��" Runat="server" CommandName="Delete" ></asp:LinkButton>
									<asp:LinkButton ID="lbtnEdit" text="�༭" Runat="server" CommandName="Edit" ></asp:LinkButton>
								</td>
							</tr>
						</ItemTemplate>
						<FooterTemplate>
							<tr bordercolor="#000080" bgcolor="#ffffff">
								<td align="center" colspan="5"><input type="button" value="��������" onclick="Add()" class="InputButton"></td>
							</tr>
						</FooterTemplate>
					</asp:repeater></table>
			</div>
			<asp:TextBox id="tbpermissionFile" style="Z-INDEX: 101; LEFT: 112px; POSITION: absolute; TOP: 49px"
				runat="server" Height="0px" Width="0px"></asp:TextBox>
			<asp:TextBox id="tableEName" style="Z-INDEX: 102; LEFT: 112px; POSITION: absolute; TOP: 96px"
				runat="server" Height="0px" Width="0px"></asp:TextBox>
            <p>
                <asp:Label ID="LabMSG" runat="server"></asp:Label>
            </p>
            <table class="style1">
                <tr>
                    <td class="style7">
                        <img src="../../Images/icons/0013_a.gif" style="width: 32px; height: 32px" /></td>
                    <td class="style8">
                        <span class="style6">����ʹ��:</span><br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ����¼�߽�ɫ���������ݹ��˹���û���κ�ƥ��ʱ,Ҳ����˵,�������û��ָ�����ݷ���Ȩ��ʱ,��<span class="style12">����</span>������������,���������ݲ���.</td>
                </tr>
                <tr>
                    <td class="style9">
                        <img src="../../Images/icons/0014_a.gif" style="width: 32px; height: 32px" /></td>
                    <td class="style10">
                        <span class="style11">��ֹʹ��:</span><br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ���Ҫ��ֹ��¼��ʹ���κ����ݣ������������ɫ�޹صĹ��˹�����[�ֶα��]&lt;0.</td>
                </tr>
            </table>
        </form>
	</body>
</HTML>
