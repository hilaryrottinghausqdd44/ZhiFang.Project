<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.personaddhr"
    CodeBehind="personaddhr.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>���/�޸���Ա��Ϣ</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../../Includes/CSS/ioffice.css" type="text/css" rel="stylesheet">

    <script language="javascript" type="text/javascript">
		function VerifyFormEmployee()
		{
			if(personadd1.NameL.value.length==0)
			{
				alert('������������')
				return false
			}
			if(personadd1.NameF.value.length==0)
			{
				alert('������������')
				return false
			}
			
//			var strTest="";
//			for(var i=0;i<personadd1.Deptpos.options.length;i++)
//			{
//				strTest=strTest + personadd1.Deptpos.options[i].value + "|" + personadd1.Deptpos.options[i].text + ",";
//			}
//			
//			if(strTest.indexOf(',')>0)
//			{
//				strTest=strTest.substring(0,strTest.length-1);
//			}
//			personadd1.TEST.value=strTest;
		
			//alert(personadd.TEST.value);
			//return false;
			return true;
			
		}
		
		
		 function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
         //���Ӵ���       
        function showUserDialog(tag)
        {           
          strurl= '../../SystemModules/mdOneNews.aspx';
           listurl='../../News/Browse/homepage.aspx';//Ҫ���ӵ�ҳ��
           listname='�Զ�����ҳ';
           moreurl='../../SystemModules/SelectNewsPage.aspx';
           strurl = strurl + "?moreurl="+escape(moreurl);         
           window.showModalDialog(strurl,this,'dialogWidth:500px;dialogHeight:500px;');
        }
        //�Ӵ���Ը�����ĸ�ֵ����
        function setValue(tag,id,name)
        {
            //tag 
            if(id != null && name != null)
            {
                if(tag == 0)
                {
                    $('txtname').value = name;
                    $('txtvalue').value = id;                  
                }
            }
        } 
    </script>

    <script language="javascript" id="clientEventHandlersJS">
<!--

function window_onload() {
	if('<%=Page.IsPostBack%>'=='True')
	{
		//window.opener.location.reload();
	}
}

//-->
    </script>

    <style type="text/css">
        .hideClass
        {
            display: none;
        }
    </style>

   

</head>
<body language="javascript" bottommargin="0" bgcolor="#f0f0f0" leftmargin="0" topmargin="0"
    onload="return window_onload()" rightmargin="0">
    <form id="personadd1" name="personadd1" onsubmit="return VerifyFormEmployee()" method="post"
    runat="server">
    <table cellspacing="0" width="732" align="center" bgcolor="black" border="0" style="width: 732px;
        height: 32px">
        <tr bgcolor="lightgrey" height="30">
            <td colspan="6">
                &nbsp;&nbsp;<b>���/�޸���Ա��Ϣ</b>
            </td>
        </tr>
    </table>
    <br>
    <table style="width: 576px; height: 300px" cellpadding="0" width="576" align="center"
        border="0">
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="����"><font color="red">*</font>�գ�</font>
            </td>
            <td width="200">
                <asp:TextBox ID="NameL" runat="server" TabIndex="-5"></asp:TextBox>
            </td>
            <td nowrap align="right" width="100">
                <font color="red">*</font> ����&nbsp;
            </td>
            <td width="157">
                <font face="����"></font>
                <asp:TextBox ID="NameF" runat="server" TabIndex="-4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 105px; height: 23px" nowrap align="right">
                <font color="red">*</font>�Ա�&nbsp;
            </td>
            <td style="height: 23px">
                <font face="����"></font>
                <asp:DropDownList ID="Sex" runat="server" Width="88px" TabIndex="4">
                    <asp:ListItem Value="��">��</asp:ListItem>
                    <asp:ListItem Value="Ů">Ů</asp:ListItem>
                </asp:DropDownList>
            </td>
           
            <td nowrap align="right" width="100">
               EMAIL:
            </td>
            <td width="157">
                <font face="����"></font>
                <asp:TextBox ID="Email" runat="server" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td nowrap align="right" bgcolor="#f0f0f0">
                ��ϵ�绰1��
            </td>
            <td>
                <font face="����"></font>
                <asp:TextBox ID="Tel1" runat="server"></asp:TextBox>
            </td>
            <td style="height: 23px" nowrap align="right">
                ��ϵ�绰2��
            </td>
            <td style="height: 23px">
                <font face="����"></font>
                <asp:TextBox ID="Tel2" runat="server" TabIndex="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <p>
                    �������ڣ�&nbsp;
                </p>
            </td>
            <td>
                <font face="����"></font>
                <asp:TextBox ID="Birth" runat="server" TabIndex="6"></asp:TextBox>
            </td>
            <td nowrap align="right">
                �ƶ��绰��
            </td>
            <td>
                <font face="����"></font>
                <asp:TextBox ID="Mobile" runat="server" TabIndex="7"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="����">&nbsp;&nbsp;����ʡ�� </font>
            </td>
            <td>
                <font face="����">
                    <asp:TextBox ID="Province" runat="server" TabIndex="8"></asp:TextBox></font>
            </td>
            <td nowrap align="right">
                <font face="����">סַ��</font>
            </td>
            <td>
                <font face="����">
                    <asp:TextBox ID="Address" runat="server" TabIndex="9"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="����">���ڳ��У� </font>
            </td>
            <td>
                <font face="����">
                    <asp:TextBox ID="City" runat="server" TabIndex="10"></asp:TextBox></font>
            </td>
            <td nowrap align="right">
                <font face="����">���ڹ��ң�</font>
            </td>
            <td>
                <font face="����"><font face="����">
                    <asp:TextBox ID="Country" runat="server" TabIndex="11"></asp:TextBox></font></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px; height: 26px" nowrap align="right">
                <font face="����"></font><font face="����">��ʼ����ʱ�䣺 </font>
            </td>
            <td style="height: 26px">
                <font face="����">
                    <asp:TextBox ID="JoinDate" runat="server" TabIndex="12"></asp:TextBox></font>
            </td>
            <td style="height: 26px" nowrap align="right">
                <font face="����"><font face="����">����״����</font></font>
            </td>
            <td style="height: 26px">
                <font face="����"><font face="����">
                    <asp:DropDownList ID="MaritalStatus" runat="server" TabIndex="13">
                        <asp:ListItem Value="�ѻ�">�ѻ�</asp:ListItem>
                        <asp:ListItem Value="δ��">δ��</asp:ListItem>
                    </asp:DropDownList>
                </font></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="����">ѧ����</font>
            </td>
            <td>
                <asp:TextBox ID="EducationLevel" runat="server" TabIndex="14"></asp:TextBox>
            </td>
            <td nowrap align="right">
                �������룺&nbsp;
            </td>
            <td>
                <font face="����">
                    <asp:TextBox ID="Zip" runat="server" TabIndex="15"></asp:TextBox>
                    </font>
            </td>
        </tr>

        <tr>
            <td colspan="5">
                <hr style="border-top: silver 1px solid; width: 517px; border-bottom: white 1px solid;
                    height: 2px" noshade size="2">
            </td>
        </tr>
        <asp:Panel runat="server" Visible=false ID="pa1">
         <tr>
            <td style="width: 105px; height: 31px" nowrap align="right">
                <p>
                    Ա���ţ�</p>
                <p>
                    �������ͣ�&nbsp;</p>
            </td>
            <td style="height: 31px" nowrap>
                <p>
                    <asp:TextBox ID="bianhao" runat="server" TabIndex="16"></asp:TextBox>&nbsp;&nbsp;</p>
                <p>
                    <asp:DropDownList ID="DesktopTheme" runat="server" DataTextField="CName" DataValueField="Id"
                        TabIndex="18">
                    </asp:DropDownList>
                    <input type="button" value="Ӧ ��" id="setDesk" onclick="SetDesk()"></p>
                <!--(Ա��Id��ʹ�� 0 �� 999999 ֮�������)//-->
            </td>
            <td style="height: 31px" nowrap align="right">
                ����ְλ��
            </td>
            <td style="height: 31px">
                <font face="����">&nbsp;<select id="Deptpos" style="width: 152px; height: 70px" size="2"
                    name="Deptpos" tabindex="17">
                    <%=DeptAndPosi%>
                </select><input style="width: 39px; height: 24px" onclick="ChooseDept();" type="button"
                    value="ѡ��" tabindex="19"></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="����">Ĭ������ģ�飺</font>
            </td>
            <td style="height: 62px" nowrap>
                <font face="����">
                    <asp:DropDownList ID="Dp_dm" runat="server">
                    </asp:DropDownList>
                    <input type="button" id="setdm" onclick="SetDM()" value="Ӧ��"></font>
            </td>
            <td nowrap align="right">
                <font face="����">��λ��Ϣ��</font>
            </td>
            <td style="height: 62px">
                <font face="����">&nbsp;<select id="Post" style="width: 152px; height: 70px" tabindex="17"
                    size="2" name="Post">
                    <%=PostName%>
                </select><input style="width: 39px" onclick="ChoosePost();" tabindex="19" type="button"
                    id="CPost" value="����"></font>
            </td>
        </tr>
        
        <tr>
            <td style="height: 30px" colspan="5">
                <div align="center">
                    <hr style="border-top: silver 1px solid; width: 509px; border-bottom: white 1px solid;
                        height: 2px" noshade size="2">
                </div>
            </td>
        </tr></asp:Panel>
    </table>
    <table height="47" cellspacing="0" width="680" border="0" style="width: 680px; height: 47px">
        <tr bgcolor="lightgrey" height="30">
            <td valign="middle" align="center" colspan="6">
                <asp:Button ID="Button1" runat="server" Text="ȷ  ��" TabIndex="20" OnClick="Button1_Click">
                </asp:Button>&nbsp;<input onclick="window.close()" type="button" value="�رմ���" tabindex="21">
                <asp:CheckBox ID="UnClose" runat="server" Text="������ɺ��Զ��رմ���" Checked="True"></asp:CheckBox>
                <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label><input id="TEST"
                    style="width: 40px; height: 11px" type="hidden" size="1" name="TEST">
            </td>
        </tr>
    </table>
    <iframe id="SetDesktop" name="SetDesktop" style="width: 0px"></iframe>
    <iframe id="SetDefault" name="SetDefault" style="width: 0px"></iframe>

    <script language="javascript">
			if('<%=EmpID%>'==''||'<%=EmpID%>'=='0')
			{
				personadd1.CPost.disabled=true;
				personadd1.Post.disabled=true;				
				document.all['setDesk'].style.display='none';
			}
				function SetDesk()
				{
					window.document.frames['SetDesktop'].location="setDesktop.aspx?DesktopId="+document.all["DesktopTheme"].value+"&PersonId="+<%=EmpID%>;
				}
				function SetDM()
				{
					window.document.frames['SetDefault'].location="setDesktop.aspx?DefaultModule="+document.all["Dp_dm"].value+"&PersonId="+<%=EmpID%>;
				}
    </script>

    </form>
</body>
</html>
