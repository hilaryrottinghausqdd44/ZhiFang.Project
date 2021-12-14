<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Organizations.personadd"
    CodeBehind="personadd.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>添加/修改人员信息</title>
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
				alert('请输入姓名！')
				return false
			}
			if(personadd1.username.value.length==0)
			{
				alert('请输入帐号名！')
				return false
			}
			
			if(personadd1.userpwd.value!=personadd1.userpwd1.value)
			{
				alert('两次密码输入不一致！')
				return false
			}
			
//			if(personadd1.NameF.value.length==0)
//			{
//				alert('请输入姓名！')
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
			 //必须有一个部门在，不能全部删除
					 
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
         //打开子窗体       
        function showUserDialog(tag)
        {           
           strurl= '../../SystemModules/mdOneNews.aspx';
           listurl='../../News/Browse/homepage.aspx';//要链接的页面
           listname='自定义首页';
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
        //子窗体对父窗体的赋值操作
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
			var MSG_NEED_username = "请填写您的用户名。";
			var MSG_NEED_userpwd = "请设置您的用户口令。";
			var MSG_NEED_userpwd1 = "您两次输入的密码不一致，请重新输入。";
			var MSG_NEED_person = "请选择对应的员工编号。";
			
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
			        alert('请填写到期时间！');
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
//		    //   用正则表达式将前后空格
//		    //   用空字符串替代。
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

	        if (s.indexOf(' ') >= 0 || s.indexOf("　") >= 0) {
	            alert("用户名不能包含空格");
	        }
//	        personadd1.username.value = s.replace(" ", "");
//	        personadd1.username.value = s.replace("　", "");
	        
	        
	        
		    personadd1.Button1.disabled=true;
//		    if (personadd1.username.title != personadd1.username.value) {
//		        personadd1.buttCheckUser.style.display = "";
//		    }
//		    else {
//		        personadd1.buttCheckUser.style.display = "none";
//		    }
		    //var boolUserExist = false;
		    var boolUserExist = OA.RBAC.Organizations.personadd.ifUserExist(personadd1.username.value);

		    //document.all("Label1").innerHTML = "帐号可以使用";
		    if (boolUserExist.value) {
		        if (personadd1.Button1.value == "修改员工信息") {
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
		            //document.all("Label1").innerHTML = "帐号名存在";
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
                &nbsp;&nbsp;<b>添加/修改人员信息</b>
            </td>
        </tr>
    </table>
    <br>
    <table style="width: 576px; height: 300px" cellpadding="0" width="576" align="center"
        border="0">
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="宋体"><font color="red">*</font>姓：</font>
            </td>
            <td width="200">
                <asp:TextBox ID="NameL" runat="server" TabIndex="-5"></asp:TextBox>
            </td>
            <td nowrap align="right" width="100">
                <font color="red">*</font> 名：&nbsp;
            </td>
            <td width="157">
                <font face="宋体"></font>
                <asp:TextBox ID="NameF" runat="server" TabIndex="-4"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 105px; height: 23px" nowrap align="right">
                <font color="red">*</font>性别：&nbsp;
            </td>
            <td style="height: 23px">
                <font face="宋体"></font>
                <asp:DropDownList ID="Sex" runat="server" Width="88px" TabIndex="4">
                    <asp:ListItem Value="男">男</asp:ListItem>
                    <asp:ListItem Value="女">女</asp:ListItem>
                </asp:DropDownList>
            </td>
           
            <td nowrap align="right" width="100">
               
            </td>
            <td width="157">
                <font face="宋体"></font>
                <asp:TextBox ID="Email" Visible="false" runat="server" TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <asp:Panel runat="server" Visible="false" ID="pa2">
         <tr>
            <td nowrap align="right" bgcolor="#f0f0f0">
                联系电话1：
            </td>
            <td>
                <font face="宋体"></font>
                <asp:TextBox ID="Tel1" runat="server"></asp:TextBox>
            </td>
            <td style="height: 23px" nowrap align="right">
                联系电话2：
            </td>
            <td style="height: 23px">
                <font face="宋体"></font>
                <asp:TextBox ID="Tel2" runat="server" TabIndex="5"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <p>
                    出生日期：&nbsp;
                </p>
            </td>
            <td>
                <font face="宋体"></font>
                <asp:TextBox ID="Birth" runat="server" TabIndex="6"></asp:TextBox>
            </td>
            <td nowrap align="right">
                移动电话：
            </td>
            <td>
                <font face="宋体"></font>
                <asp:TextBox ID="Mobile" runat="server" TabIndex="7"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="宋体">&nbsp;&nbsp;所在省： </font>
            </td>
            <td>
                <font face="宋体">
                    <asp:TextBox ID="Province" runat="server" TabIndex="8"></asp:TextBox></font>
            </td>
            <td nowrap align="right">
                <font face="宋体">住址：</font>
            </td>
            <td>
                <font face="宋体">
                    <asp:TextBox ID="Address" runat="server" TabIndex="9"></asp:TextBox></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="宋体">所在城市： </font>
            </td>
            <td>
                <font face="宋体">
                    <asp:TextBox ID="City" runat="server" TabIndex="10"></asp:TextBox></font>
            </td>
            <td nowrap align="right">
                <font face="宋体">所在国家：</font>
            </td>
            <td>
                <font face="宋体"><font face="宋体">
                    <asp:TextBox ID="Country" runat="server" TabIndex="11"></asp:TextBox></font></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px; height: 26px" nowrap align="right">
                <font face="宋体"></font><font face="宋体">开始工作时间： </font>
            </td>
            <td style="height: 26px">
                <font face="宋体">
                    <asp:TextBox ID="JoinDate" runat="server" TabIndex="12"></asp:TextBox></font>
            </td>
            <td style="height: 26px" nowrap align="right">
                <font face="宋体"><font face="宋体">婚姻状况：</font></font>
            </td>
            <td style="height: 26px">
                <font face="宋体"><font face="宋体">
                    <asp:DropDownList ID="MaritalStatus" runat="server" TabIndex="13">
                        <asp:ListItem Value="已婚">已婚</asp:ListItem>
                        <asp:ListItem Value="未婚">未婚</asp:ListItem>
                    </asp:DropDownList>
                </font></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="宋体">学历：</font>
            </td>
            <td>
                <asp:TextBox ID="EducationLevel" runat="server" TabIndex="14"></asp:TextBox>
            </td>
            <td nowrap align="right">
                邮政编码：&nbsp;
            </td>
            <td>
                <font face="宋体">
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
                    员工号：</p>
                <p style="display:none">
                    桌面类型：&nbsp;</p>
            </td>
            <td style="height: 31px" nowrap>
                <p>
                    <asp:TextBox ID="bianhao" runat="server" TabIndex="16"></asp:TextBox>&nbsp;&nbsp;</p>
                <p  style="display:none">
                    <asp:DropDownList ID="DesktopTheme" runat="server" DataTextField="CName" DataValueField="Id"
                        TabIndex="18">
                    </asp:DropDownList>
                    <input type="button" value="应 用" id="setDesk" onclick="SetDesk()"></p>
                <!--(员工Id请使用 0 至 999999 之间的数字)//-->
            </td>
            <td style="height: 31px" nowrap align="right">
                部门职位：
            </td>
            <td style="height: 31px">
                <font face="宋体">&nbsp;<select id="Deptpos" style="width: 152px; height: 70px" size="2"
                    name="Deptpos" tabindex="17">
                    <%=DeptAndPosi%>
                </select><input style="width: 39px; height: 24px" onclick="ChooseDept();" type="button"
                    value="选择" tabindex="19"></font>
            </td>
        </tr>
        <tr>
            <td style="width: 105px" nowrap align="right">
                <font face="宋体">启动模块：</font>
            </td>
            <td style="height: 62px" nowrap>
                <font face="宋体">
                    <asp:DropDownList ID="Dp_dm" runat="server">
                    </asp:DropDownList>
                    <br />
                    <input type="button" id="setdm" onclick="SetDM()" value="应用"></font>
            </td>
            <td nowrap align="right">
                <font face="宋体">岗位信息：</font>
            </td>
            <td style="height: 62px">
                <font face="宋体">&nbsp;<select id="Post" style="width: 152px; height: 70px" tabindex="17"
                    size="2" name="Post">
                    <%=PostName%>
                </select><input style="width: 39px" onclick="ChoosePost();" tabindex="19" type="button"
                    id="CPost" value="设置"></font>
            </td>
        </tr>
        <asp:Panel runat="server" ID="pa1">
            <tr>
                <td style="width: 105px" nowrap align="right">
                    默认首页模版:
                </td>
                <td style="" nowrap>
                    <asp:TextBox ID="txtname" Width="100px" onclick="showUserDialog(0);"
                        runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtvalue" CssClass="hideClass" Width="50px" runat="server"></asp:TextBox>
                    <asp:Button runat="server" ID="btnApp" runat="server" Text="应用" OnClick="btnApp_Click" />
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
                            &nbsp;操作描述：
                        </td>
                        <td style="height: 25px" width="25%">
                            <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                            <font face="宋体">账号不能重复，一个员工对应一个账号</font>
                            <!--	<select name="usertype" id="usertype" style="width:60%">
							
						</select>-->
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right" width="10%">
                            <font color="red">*</font>账号名：
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="username" runat="server" Width="272px"></asp:TextBox>
                            <input id="buttCheckUser" name="buttCheckUser" runat="server" type="button" value="帐号存在,请换一个" onclick="checkUserNameA()" style="display:none"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px" nowrap align="right" width="10%">
                            <font color="red">*</font>账号口令：
                        </td>
                        <td style="height: 25px" width="25%">
                            <asp:TextBox ID="userpwd" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px" nowrap align="right" width="10%">
                            <font color="red">*</font>口令确认：
                        </td>
                        <td style="height: 22px" width="25%">
                            <asp:TextBox ID="userpwd1" runat="server" Width="272px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right" width="10%">
                            <font id="FONT1" face="宋体" runat="server">员工描述：</font>
                        </td>
                        <td width="25%">
                            <asp:TextBox ID="UserDesc" runat="server" Width="272px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="宋体"></font>
                        </td>
                        <td>
                            <font face="宋体">
                                <input id="ChkEnMPwd" type="checkbox" checked name="ChkEnMPwd">&nbsp;允许修改密码</font>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px" align="right">
                        </td>
                        <td style="height: 21px">
                            <font face="宋体">
                                <input id="Checkbox2" type="checkbox" checked name="ChkPwdExprd"><font face="宋体">&nbsp;密码永不过期</font></font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="宋体"></font>
                        </td>
                        <td>
                            <input id="ChkAccExprd" onclick="javascript:ShowAccTime()" type="checkbox" checked
                                name="ChkAccExprd"><font face="宋体">&nbsp;账号永不过期
                                    <div id="ChkPwdExprd1" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                                        到期时间</div>
                                    <input id="TxtAccExpTm" style="display: none; width: 88px; height: 22px" type="text"
                                        size="9" name="TxtAccExpTm"></font>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <font face="宋体"></font>
                        </td>
                        <td>
                            <input id="CheckAccLock" onclick="ShowAccLock();" type="checkbox" name="CheckAccLock">
                            <div style="display: inline; width: 88px; color: red; height: 18px">
                                帐号被锁定</div>
                            <br>
                            <font face="宋体">&nbsp;&nbsp; </font>
                            <input id="ChkAuUnlocka" style="display: none" type="radio" value="0" name="ChkAuUnlock">
                            <div id="ChkAuUnlock1" style="display: none; width: 232px; height: 14px">
                                永久锁定，直至系统管理员解锁</div>
                            <br>
                            <font face="宋体">&nbsp;&nbsp; </font>
                            <input id="ChkAuUnlockb" style="display: none" type="radio" checked value="1" name="ChkAuUnlock">
                            <div id="ChkAuUnlock2" style="display: none; width: 70px; height: 15px">
                                自动解锁</div>
                            <select id="SelLockedPeriod" style="display: none; width: 72px" name="SelLockedPeriod">
                                <option value="1">一&nbsp;&nbsp;分钟</option>
                                <option value="5" selected>五&nbsp;&nbsp;分钟</option>
                                <option value="10">十&nbsp;&nbsp;分钟</option>
                                <option value="15">十五分钟</option>
                                <option value="30">三十分钟</option>
                                <option value="60">一&nbsp;&nbsp;小时</option>
                                <option value="360">六&nbsp;&nbsp;小时</option>
                                <option value="720">十二小时</option>
                                <option value="1440">一&nbsp;&nbsp;天</option>
                                <option value="9080">一&nbsp;&nbsp;周</option>
                                <option value="272400">一&nbsp;&nbsp;月</option>
                            </select>
                            <div id="ChkAuUnlock3" style="display: none; width: 70px; height: 15px" ms_positioning="FlowLayout">
                                后解开</div>
                         
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
                <asp:Button ID="Button1" runat="server" Text="确  定" TabIndex="20" OnClick="Button1_Click">
                </asp:Button>&nbsp;<input onclick="window.close()" type="button" value="关闭窗口" tabindex="21">
                <asp:CheckBox ID="UnClose" runat="server" Text="操作完成后，自动关闭窗口" Checked="True"></asp:CheckBox>
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
