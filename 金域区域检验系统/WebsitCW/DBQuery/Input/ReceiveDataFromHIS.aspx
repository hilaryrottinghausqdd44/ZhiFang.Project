<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.ReceiveDataFromHIS" Codebehind="ReceiveDataFromHIS.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReceiveDataFromHIS</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!--script language="javascript" src="../../Includes/js/calendar.js"></script-->
		<!--#include file="../../Includes/JS/Calendar.js"-->
		<script>
			function FireQuery()
			{
				var strActionButton="";
				var strActionName="";
				strActionButton="����������һ���¼�¼����\r";
				strActionName="����";
				var Bconfirm=confirm(strActionButton + "ȷ��Ҫ ["+strActionName+"] ����������\r\r");
			
				if(Bconfirm)
				{
					var myDataRun=CollectDataRun(form1);
					form1.submit();
					//window.opener.Form1.submit();
					window.status="��ɣ�����";
					window.close();
				}
				else
				{
					window.status="��������";
					return;
				}
			}
			
			function CollectDataRunList(kids)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						switch(kids[i].type.toUpperCase())
						{
							case "TEXT":
							//case "HIDDEN"://�ش����⣬��������||kids[i].type.toUpperCase()=='HIDDEN'))
								if(kids[i].value!="")
								//hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].value;
								hDataRun +="\t" +  kids[i].id + kids[i].method + kids[i].value;
								break;
							case "RADIO":
								if(kids[i].checked!="")
									hDataRun +="\t" +  kids[i].name + kids[i].method + kids[i].value;
								break;
							case "CHECKBOX":
								if(hDataRun.indexOf(kids[i].name + kids[i].method)==-1)
								{
									var checkBoxList=form1.document.all[kids[i].name];
									
									var strCHKValues="";
									for(var iCHK=0;iCHK<checkBoxList.length;iCHK++)
									{
										if(checkBoxList[iCHK].checked)
											strCHKValues +="," + checkBoxList[iCHK].value;
											//hDataRun +="\n" +  kids[i].name + kids[i].method + kids[i].value;
									}
									if(strCHKValues.length>0)
										strCHKValues=strCHKValues.substr(1);
									if(strCHKValues.length>0) //�ǲ���Ҫ�޸�?
										hDataRun +="\t" +  kids[i].name + kids[i].method + strCHKValues;///����ż�������
								}
								break;
							default:
								break;
						}
						
					}
					
					//============�ռ�TextArea============
					if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
					{
						if(kids[i].value!="")
						{
							//var txtValue = kids[i].value.replace(/[\r][\n]/g, "��");
							//alert(txtValue);
							hDataRun +="\t" +  kids[i].id + kids[i].method + kids[i].value;
						}
					}
					//-----------------End----------------
					
					if(kids[i].nodeName.toUpperCase()=='SELECT')
					{
						var selOptions=kids[i].options;
						var strCHKValues="";
						for(var iCHK=0;iCHK<selOptions.length;iCHK++)
						{
							if(selOptions[iCHK].selected)
								strCHKValues +="," + selOptions[iCHK].text;
								//hDataRun +="\n" +  kids[i].name + kids[i].method + kids[i].value;
						}
						if(strCHKValues.length>0)
							strCHKValues=strCHKValues.substr(1);
						if(strCHKValues.length>0)
							hDataRun +="\t" +  kids[i].id + kids[i].method + strCHKValues;///����ż�������
						
						//if(kids[i].options[kids[i].selectedIndex].text!="")
						//	hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].options[kids[i].selectedIndex].text;
					}
					if(kids[i].hasChildNodes)
						CollectDataRunList(kids[i].childNodes);
				}
			}
			var hDataRun="";
			function CollectDataRun(myForm)
			{
				hDataRun="";
				CollectDataRunList(form1.document.all['TableData'].childNodes);
				form1.hQueryCollection.value=hDataRun;
				
				alert(hDataRun);
				return hDataRun;
				//return false;
			}
			

		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
			<form runat=server>
			<table width="98%" border="0">
				<tr>
					<td width="18%" align="right">��ˮ��</td>
					<td width="82%">
						<label><input type="text" name="textfield" id="txt_Serialno" runat="server"> </label>
						<label><input type="button" name="Submit1" value="��ȡ����" id="Submit1" runat="server" onserverclick="Submit1_ServerClick"> 
						<!--<input type="checkbox" name="checkbox" value="checkbox">
							�Ƿ������Զ�����--></label></td>
				</tr>

			</table>
			</form>
			<form id="form1" name="form1" method="post" action="DataRun.aspx?<%=Request.ServerVariables["Query_String"]%>">
			<table width="98%" border="0" id="TableData">
				<tr>
					<td colspan="2"><table width="98%" border="0">
							<tr>
								<td>&nbsp;</td>
								<td rowspan="2">���������</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td width="18%" align="right">������</td>
								<td width="82%"><label>
								<input title="������" type="text"  method="=" id="ybid" value="" ></label>
								</td>
							</tr>
							<tr>
								<td align="right">��������</td>
								<td>												
								<input title="��������" type="text"  method="=" id="uname" value="" name="uname"></td>
							</tr>
							<tr>
								<td align="right">�Ա�</td>
								<td>
								<select title="�����Ա�" keyIndex="No" NoChange="No"
																size="1"  method="=" readonly  AllowNull="Yes"
																id="sex" ColumnDefault="">
																<option></option>
																
																<option value="1">��</option>
																<option value="2">Ů</option>
																
																</select>
																</td>
							</tr>
							<tr>
								<td align="right">����</td>
								<td><input type="text" method="=" id="uage" ></td>
							</tr>
							<tr>
								<td align="right">�ͼ쵥λ</td>
								<td><input type="text" method="=" id="sjyy" value="��Ժ"></td>
							</tr>
							<tr>
								<td align="right">�ͼ����</td>
								<td><input type="text" id="sjks" method="="></td>
							</tr>
							<tr>
								<td align="right">����</td>
								<td><input type="text" id="ybdjch"  name="ybdjch" method="="></td>
							</tr>
							<tr>
								<td align="right">סԺ��/�����</td>
								<td><input type="text" id="EName1" name="EName1" method="="></td>
							</tr>
							<tr>
								<td align="right">������ϵ��ʽ����ַ</td>
								<td><input type="text" id="EName46" method="="></td>
							</tr>
							<tr>
								<td align="right">�ٴ���ϼ�����</td>
								<td><input type="text" id="lczd" method="="></td>
							</tr>
							<tr>
								<td align="right">�걾����</td>
								<td>
<input id="bbStatus_102" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="����Ѫ">����Ѫ
<input id="bbStatus_103" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="����">����
<input id="bbStatus_2" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="�ܰͽ�">�ܰͽ�
<input id="bbStatus_3" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="��Ѫ">��Ѫ
<input id="bbStatus_4" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="��ëĤ">��ëĤ
<br />
<input id="bbStatus_5" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="ӡ��Ƭ">ӡ��Ƭ
<input id="bbStatus_6" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="��֯��Ƭ">��֯��Ƭ
<input id="bbStatus_7" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="����ͿƬ">����ͿƬ
<input id="bbStatus_8" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="ѪͿƬ">ѪͿƬ
<br />
<input id="bbStatus_9" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="��Ⱦɫ��Ƭ">��Ⱦɫ��Ƭ
<input id="bbStatus_10" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="��ǻ��ƤճĤ">��ǻ��ƤճĤ
<input id="bbStatus_11" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="����">����</td>
							</tr>
							<tr>
								<td align="right">�ɼ�����</td>
								<td><input type="text" id="bbcjsj" name="bbcjsj"  method="=" onfocus="setday(this);window.status='ֻ����������,��ʽyyyy-MM-DD';"></td>
							</tr>
							<tr>
								<td align="right">�ͼ�����</td>
								<td><input type="text" name="sjdate" id="sjdate"  method="=" onfocus="setday(this);window.status='ֻ����������,��ʽyyyy-MM-DD';"></td>
							</tr>
							<tr>
								<td align="right">�ͼ���</td>
								<td><input type="text" id="sjys" name="sjys" method="="></td>
							</tr>
							<tr>
								<td align="right">������Ŀ</td>
								<td><input id="EName41_0" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="��Ѫ����¡��Ⱦɫ���쳣���" >��Ѫ����¡��Ⱦɫ���쳣���
<br />
<input id="EName41_10010" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="�ܰ�����¡��Ⱦɫ���쳣���">�ܰ�����¡��Ⱦɫ���쳣���
<br />
<input id="EName41_10011" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="��������ѪȾɫ����">��������ѪȾɫ����
<br />
<input id="EName41_10012" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="��������ѪȾɫ����+Ƕ����Ⱦɫ����">��������ѪȾɫ����+Ƕ����Ⱦɫ����
<br />
<input id="EName41_4" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="�߷ֱ���Ⱦɫ����">�߷ֱ���Ⱦɫ����
<br />
<input id="EName41_5" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="��Ѫ��������ӫ��ԭλ�ӽ�(FISH)���">��Ѫ��������ӫ��ԭλ�ӽ�(FISH)���
<br />
<input id="EName41_6" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="��Ѫ��������ʯ����Ƭ/ӡƬӫ��ԭλ�ӽ�(FISH)���">��Ѫ��������ʯ����Ƭ/ӡƬӫ��ԭλ�ӽ�(FISH)���
<br />
<input id="EName41_7" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="�Ŵ��Լ���ӫ��ԭλ�ӽ�(FISH)���">�Ŵ��Լ���ӫ��ԭλ�ӽ�(FISH)���
<br />
<input id="EName41_8" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="�����ںϻ����PCR���Ӽ��">�����ںϻ����PCR���Ӽ��
<br />
<input id="EName41_9" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="����XȾɫ���ۺ���������">����XȾɫ���ۺ���������
<br />
<input id="EName41_10" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="�Ŵ��Զ�������ͻ����">�Ŵ��Զ�������ͻ����
<input id="EName41_11" NoChange="No" method="=" style="border-Bottom:red 2px solid;width:20px" checked type="CheckBox"  name="EName41"  value="��ͨ����ѪȾɫ����">��ͨ����ѪȾɫ����
</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table width="98%" border="0">
			<tr>
				<td>&nbsp;</td>
				<td><label> 
				<input type=button name="Submit2" onclick="FireQuery();" value="�� ��" id="Submit2" runat="server" onserverclick="Submit2_ServerClick"> </label>
											
				</td>
			</tr>
			</table>
			<input type="hidden" id="hQueryCollection" name="hQueryCollection" value="">
			<input type="hidden" id="hAction" name="hAction" value="BAdd">
			<%
					if(ds!=null)
					{
						if(ds.Tables[0].Rows.Count>0)
						{
							%>
						<script>
							document.getElementById("uname").value='<%=(ds.Tables[0].Rows[0]["CName"].ToString())%>';    //����
							document.getElementById("lczd").value='<%=(ds.Tables[0].Rows[0]["zdy1"].ToString())%>';      //�ٴ����
							document.getElementById("bbcjsj").value='<%=(ds.Tables[0].Rows[0]["zdy2"].ToString())%>';    //�ɼ�����
							document.getElementById("ybdjch").value='<%=(ds.Tables[0].Rows[0]["Bed"].ToString())%>';     //����
							document.getElementById("EName1").value='<%=(ds.Tables[0].Rows[0]["Serialno"].ToString())%>';     //��ˮ��
							document.getElementById("sjdate").value='<%=(ds.Tables[0].Rows[0]["FlagDateDelete"].ToString())%>';    // �ͼ�����
							document.getElementById("sjks").value='<%=(ds.Tables[0].Rows[0]["DeptName"].ToString())%>';    //�ͼ����
							
							document.getElementById("sex").value='<%=(ds.Tables[0].Rows[0]["GenderNo"].ToString())%>';  //�Ա�
							
							var SampleType = "bbStatus_"+<%=(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString())%>;
							document.getElementById(SampleType).checked = true;                                   //��������
							 
						</script>
							<%
						}
						if(ds.Tables[1].Rows.Count>0)
						{
							for(int i=0;i<ds.Tables[1].Rows.Count;i++)
							{
							%>
						<script>
							var ItemNo = "EName41_"  + <%=(ds.Tables[1].Rows[i]["ParItemNo"].ToString())%>;
							document.getElementById(ItemNo).checked = true;  
							//alert(ItemNo);
						</script>
							<%
							}
						}
						
					}
					
				%>
		</form>
	</body>
</HTML>
