<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.personadd"
    CodeBehind="personadd.aspx.cs" %>

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
			if(personadd1.username.value.length==0)
			{
				alert('�������ʺ�����')
				return false
			}
			
			if(personadd1.userpwd.value!=personadd1.userpwd1.value)
			{
				alert('�����������벻һ�£�')
				return false
			}
			
//			if(personadd1.NameF.value.length==0)
//			{
//				alert('������������')
//				return false
//			}
			
			var strTest="";
			for(var i=0;i<personadd1.Deptpos.options.length;i++)
			{
				strTest=strTest + personadd1.Deptpos.options[i].value + "|" + personadd1.Deptpos.options[i].text + ",";
			}
			
			if(strTest.indexOf(',')>0)
			{
				strTest=strTest.substring(0,strTest.length-1);
			}
			personadd1.TEST.value=strTest;
			
			return IsValidate();
			//alert(personadd.TEST.value);
			//return false;
			
		}
		function ChooseDept()
		{
			var Eid=<%=EmpID%>
			if(Eid==0)
			{
		
				r = window.showModalDialog ('chooseDept.aspx?&DeptId='+<%=DeptId%>,'','dialogWidth=20;dialogHeight=25;resizable=no;scroll=no;status=no');
			
			}
			else
				r = window.showModalDialog ('chooseDept.aspx?Id='+<%=EmpID%>,'','dialogWidth=20;dialogHeight=25;resizable=no;scroll=no;status=no');
		
			 //window.open('chooseDept.aspx','','width=350px,height=320px,resizable=yes,left=' + (screen.availWidth-640)/2 + ',top=' + (screen.availHeight-470)/2 );
			 //������һ�������ڣ�����ȫ��ɾ��
					 
			if (typeof(r) != 'undefined' && r != 'undefined'  &&r!=-1)
			{	
			    
				personadd1.TEST.value=r;
				for(var i=personadd1.Deptpos.options.length-1;i>=0;i--)
				{
					personadd1.Deptpos.options.remove(i);
				}
				if(r!='')
				{
				    var mrv = r.split(",");
    				
				    for(var i=0;i<mrv.length;i++)
				    {	
					    var rv=mrv[i].split("|");
					    var item;
					    item=window.document.createElement("OPTION");
					    item.text = "";
					    item.value = "";
					    for(var k=0;k<rv.length;k++)
					    {
						    if(typeof(rv[1])!='undefined')
						    {
						    item.text=rv[1];
						    item.value=rv[0];						
						    }					
					    }
					    if(item.text!='')
					    personadd1.Deptpos.add(item);
				    }
				}	
			}
		}
		function ChoosePost()
		{
			window.open ('EmployeeRole.aspx?Id='+<%=EmpID%>,'','width=500px,height=430px,scrollbars=0,top=200,left=300');
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
           //alert('tag=' + tag + ',strurl=' + strurl +',listurl=' + listurl+',listname=' + listname+',moreurl=' + moreurl);
           var r = window.showModalDialog(strurl,this,'dialogWidth:500px;dialogHeight:500px;');
           if (r == '' || typeof(r) == 'undefined' || typeof(r)=='object')
	        {
		        return;
	        }
	        else
	        {
	            var id = r.substring(0,r.indexOf(','));
	            var name = r.substring(r.indexOf(',')+1);
	            $('txtname').value = name;
                $('txtvalue').value = "id="+id;       
	        }
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
                    $('txtvalue').value = "id="+id;                  
                }
            }
        } 
    </script>

    <script language="javascript" id="clientEventHandlersJS">


        function window_onload() {
	        if('<%=Page.IsPostBack%>'=='True')
	        {
		        //window.opener.location.reload();
	        }
        }


    </script>

    <style type="text/css">
        .hideClass
        {
            display: none;
        }
    </style>

    <script language="javascript">
		function IsValidate()
		{
			var MSG_NEED_username = "����д�����û�����";
			var MSG_NEED_userpwd = "�����������û����";
			var MSG_NEED_userpwd1 = "��������������벻һ�£����������롣";
			var MSG_NEED_person = "��ѡ���Ӧ��Ա����š�";
			
			if (personadd1.username.value.length == 0)
				{
					alert (MSG_NEED_username);
					personadd1.username.focus();
					return false;
				}
			
//			if (personadd1.username.value == "")
//			{
//				alert (MSG_NEED_person);
//				personadd1.person.focus();
//				return false;
//			}
			if (personadd1.ChkAccExprd.checked == false) {
			    if (personadd1.TxtAccExpTm == "") {
			        alert('����д����ʱ�䣡');
			        return false;
			    }
			}
			return true;
			
		}
		function ShowAccTime()
		{
			if(document.all("TxtAccExpTm").style.display=="none")
				{
					
					document.all("TxtAccExpTm").style.display="";
					document.all("ChkPwdExprd1").style.display="inline";
				}
			else
				{
					document.all("TxtAccExpTm").style.display="none";
					document.all("ChkPwdExprd1").style.display="none";
				}
			
		}
		function ShowAccLock()
		{
			if(personadd1.ChkAuUnlocka.style.display=="none")
			{
				personadd1.ChkAuUnlocka.style.display="";
				personadd1.ChkAuUnlockb.style.display="";				
				document.all("ChkAuUnlock1").style.display="inline"
				document.all("ChkAuUnlock2").style.display="inline"			
				document.all("SelLockedPeriod").style.display="inline"
				document.all("ChkAuUnlock3").style.display="inline"
				
			}
			else
			{
				personadd1.ChkAuUnlocka.style.display="none";
				personadd1.ChkAuUnlockb.style.display="none";
				document.all("ChkAuUnlock1").style.display="none"
				document.all("ChkAuUnlock2").style.display="none"			
				document.all("SelLockedPeriod").style.display="none"
				document.all("ChkAuUnlock3").style.display="none"
            }
		}

		function checkUserNameA(obj) {
		    personadd1.username.value += "1";
		    //obj.valueOf += "1";
		}
//		function trim(s) {
//		    //   ��������ʽ��ǰ��ո�
//		    //   �ÿ��ַ��������
//		    return s.replace(/(^\s*)|(\s*$)/g, "");
//		}

		function trim(str) {
		    for (var i = 0; i < str.length && str.charAt(i) == " "; i++);
		    for (var j = str.length; j > 0 && str.charAt(j - 1)==" "; j--);
		    if (i > j) return "";
		    return str.substring(i, j);
		}


		function checkUserName() {
	        var s = "";
	        s = personadd1.username.value;

	        if (s.indexOf(' ') >= 0 || s.indexOf("��") >= 0) {
	            alert("�û������ܰ����ո�");
	        }
//	        personadd1.username.value = s.replace(" ", "");
//	        personadd1.username.value = s.replace("��", "");
	        
	        
	        
		    personadd1.Button1.disabled=true;
//		    if (personadd1.username.title != personadd1.username.value) {
//		        personadd1.buttCheckUser.style.display = "";
//		    }
//		    else {
//		        personadd1.buttCheckUser.style.display = "none";
//		    }
		    //var boolUserExist = false;
		    var boolUserExist = OA.RBAC.Organizations.personadd.ifUserExist(personadd1.username.value);

		    //document.all("Label1").innerHTML = "�ʺſ���ʹ��";
		    if (boolUserExist.value) {
		        if (personadd1.Button1.value == "�޸�Ա����Ϣ") {
		            personadd1.Button1.disabled = false;
		            if (personadd1.username.title != personadd1.username.value) {
		                personadd1.Button1.disabled = true;
		                personadd1.buttCheckUser.style.display = "";
		            }
		            else
		                personadd1.buttCheckUser.style.display = "none";
		        }
		        else {
		            personadd1.Button1.disabled = true;
		            personadd1.buttCheckUser.style.display = "";
		            //document.all("Label1").innerHTML = "�ʺ�������";
		        }
		        //alert(personadd1.Button1.disabled);
		    }
		    else {
		        personadd1.Button1.disabled = false;
		        personadd1.buttCheckUser.style.display = "none";
		    }
		}
		

    </script>

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
               
            </td>
            <td width="157">
                <font face="����"></font>
                <asp:TextBox ID="Email" Visible="false" runat="server" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <asp:Panel runat="server" Visible="false" ID="pa2">
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
                    <asp:TextBox ID="Zip" runat="server" TabIndex="15"></asp:TextBox></font>
            </td>
        </tr>
        </asp:Panel>
        <tr>
            <td colspan="5">
                <hr style="border-top: silver 1px solid; width: 517px; border-bottom: white 1px solid;
                    height: 2px" noshade size="2">
            </td>
        </tr>
        <tr>
            <td style="width: 105px; height: 31px" nowrap align="right">
                <p>
                    Ա���ţ�</p>
                <p style="display:none">
                    �������ͣ�&nbsp;</p>
            </td>
            <td style="height: 31px" nowrap>
                <p>
                    <asp:TextBox ID="bianhao" runat="server" TabIndex="16"></asp:TextBox>&nbsp;&nbsp;</p>
                <p  style="display:none">
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
                <font face="����">����ģ�飺</font>
            </td>
            <td style="height: 62px" nowrap>
                <font face="����">
                    <asp:DropDownList ID="Dp_dm" runat="server">
                    </asp:DropDownList>
                    <br />
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
        <asp:Panel runat="server" ID="pa1">
            <tr>
                <td style="width: 105px" nowrap align="right">
                    Ĭ����ҳģ��:
                </td>
                <td style="" nowrap>
                    <asp:TextBox ID="txtname" Width="100px" onclick="showUserDialog(0);"
                        runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtvalue" CssClass="hideClass" Width="50px" runat="server"></asp:TextBox>
                    <asp:Button runat="server" ID="btnApp" runat="server" Text="Ӧ��" OnClick="btnApp_Click" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </asp:Panel>
        <tr>
            <td style="height: 30px" colspan="5">
                <div align="center">
                    <hr style="border-top: silver 1px solid; width: 509px; border-bottom: white 1px solid;
                        height: 2px" noshade size="2">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <table cellspacing="0" width="100%" align="center" border="0">
                    <tr>
                        <td style="height: 25px" nowrap align="right" width="8%">
                            &nbsp;����������
                        </td>
                        <td style="height: 25px" width="25%">
                            <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                            <font face="����">�˺Ų����ظ���һ��Ա����Ӧһ���˺�</font>
                            <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right" width="10%">
                            <font color="red">*</font>�˺�����
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="username" runat="server" Width="272px"></asp:TextBox>
                            <input id="buttCheckUser" name="buttCheckUser" runat="server" type="button" value="�ʺŴ���,�뻻һ��" onclick="checkUserNameA()" style="display:none"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px" nowrap align="right" width="10%">
                            <font color="red">*</font>�˺ſ��
                        </td>
                        <td style="height: 25px" width="25%">
                            <asp:TextBox ID="userpwd" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px" nowrap align="right" width="10%">
                            <font color="red">*</font>����ȷ�ϣ�
                        </td>
                        <td style="height: 22px" width="25%">
                            <asp:TextBox ID="userpwd1" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right" width="10%">
                            <font id="FONT1" face="����" runat="server">Ա��������</font>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="UserDesc" runat="server" Width="272px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="����"></font>
                        </td>
                        <td>
                            <font face="����">
                                <input id="ChkEnMPwd" type="checkbox" checked name="ChkEnMPwd">&nbsp;�����޸�����</font>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px" align="right">
                        </td>
                        <td style="height: 21px">
                            <font face="����">
                                <input id="Checkbox2" type="checkbox" checked name="ChkPwdExprd"><font face="����">&nbsp;������������</font></font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="����"></font>
                        </td>
                        <td>
                            <input id="ChkAccExprd" onclick="javascript:ShowAccTime()" type="checkbox" checked
                                name="ChkAccExprd"><font face="����">&nbsp;�˺���������
                                    <div id="ChkPwdExprd1" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                                        ����ʱ��</div>
                                    <input id="TxtAccExpTm" style="display: none; width: 88px; height: 22px" type="text"
                                        size="9" name="TxtAccExpTm"></font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="����"></font>
                        </td>
                        <td>
                            <input id="CheckAccLock" onclick="ShowAccLock();" type="checkbox" name="CheckAccLock">
                            <div style="display: inline; width: 88px; color: red; height: 18px">
                                �ʺű�����</div>
                            <br>
                            <font face="����">&nbsp;&nbsp; </font>
                            <input id="ChkAuUnlocka" style="display: none" type="radio" value="0" name="ChkAuUnlock">
                            <div id="ChkAuUnlock1" style="display: none; width: 232px; height: 14px">
                                ����������ֱ��ϵͳ����Ա����</div>
                            <br>
                            <font face="����">&nbsp;&nbsp; </font>
                            <input id="ChkAuUnlockb" style="display: none" type="radio" checked value="1" name="ChkAuUnlock">
                            <div id="ChkAuUnlock2" style="display: none; width: 70px; height: 15px">
                                �Զ�����</div>
                            <select id="SelLockedPeriod" style="display: none; width: 72px" name="SelLockedPeriod">
                                <option value="1">һ&nbsp;&nbsp;����</option>
                                <option value="5" selected>��&nbsp;&nbsp;����</option>
                                <option value="10">ʮ&nbsp;&nbsp;����</option>
                                <option value="15">ʮ�����</option>
                                <option value="30">��ʮ����</option>
                                <option value="60">һ&nbsp;&nbsp;Сʱ</option>
                                <option value="360">��&nbsp;&nbsp;Сʱ</option>
                                <option value="720">ʮ��Сʱ</option>
                                <option value="1440">һ&nbsp;&nbsp;��</option>
                                <option value="9080">һ&nbsp;&nbsp;��</option>
                                <option value="272400">һ&nbsp;&nbsp;��</option>
                            </select>
                            <div id="ChkAuUnlock3" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                                ��⿪</div>
                         
                        </td>
                    </tr>
                </table>

                <script language="javascript">
				function ChooseEmpl()
				{
					var result;
					result = window.showModalDialog('searchperson.aspx','','dialogWidth=30;dialogHeight=30;status=no;scroll=no');
					if (result != 'undefined' && typeof(result)!='undefined')
					{
						var rv = result.split("|");
						if (rv.length == 2);
						{	
							personadd1.test1.value = rv[1];
							personadd1.userid.value = rv[0];
						}
					}
				}
				
                </script>

            </td>
        </tr>
        <tr>
            <td style="height: 30px" colspan="5">
                <div align="center">
                    <hr style="border-top: silver 1px solid; width: 509px; border-bottom: white 1px solid;
                        height: 2px" noshade size="2">
                </div>
            </td>
        </tr>
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
