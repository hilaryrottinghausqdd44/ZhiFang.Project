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
				strActionButton="您正在增加一条新记录数据\r";
				strActionName="增加";
				var Bconfirm=confirm(strActionButton + "确认要 ["+strActionName+"] 以下数据吗？\r\r");
			
				if(Bconfirm)
				{
					var myDataRun=CollectDataRun(form1);
					form1.submit();
					//window.opener.Form1.submit();
					window.status="完成！！！";
					window.close();
				}
				else
				{
					window.status="放弃保存";
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
							//case "HIDDEN"://重大问题，不能设置||kids[i].type.toUpperCase()=='HIDDEN'))
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
									if(strCHKValues.length>0) //是不是要修改?
										hDataRun +="\t" +  kids[i].name + kids[i].method + strCHKValues;///这里才加入数据
								}
								break;
							default:
								break;
						}
						
					}
					
					//============收集TextArea============
					if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
					{
						if(kids[i].value!="")
						{
							//var txtValue = kids[i].value.replace(/[\r][\n]/g, "▲");
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
							hDataRun +="\t" +  kids[i].id + kids[i].method + strCHKValues;///这里才加入数据
						
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
					<td width="18%" align="right">流水号</td>
					<td width="82%">
						<label><input type="text" name="textfield" id="txt_Serialno" runat="server"> </label>
						<label><input type="button" name="Submit1" value="提取数据" id="Submit1" runat="server" onserverclick="Submit1_ServerClick"> 
						<!--<input type="checkbox" name="checkbox" value="checkbox">
							是否启用自动保存--></label></td>
				</tr>

			</table>
			</form>
			<form id="form1" name="form1" method="post" action="DataRun.aspx?<%=Request.ServerVariables["Query_String"]%>">
			<table width="98%" border="0" id="TableData">
				<tr>
					<td colspan="2"><table width="98%" border="0">
							<tr>
								<td>&nbsp;</td>
								<td rowspan="2">所获得数据</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td width="18%" align="right">样本号</td>
								<td width="82%"><label>
								<input title="样本号" type="text"  method="=" id="ybid" value="" ></label>
								</td>
							</tr>
							<tr>
								<td align="right">病人姓名</td>
								<td>												
								<input title="病人姓名" type="text"  method="=" id="uname" value="" name="uname"></td>
							</tr>
							<tr>
								<td align="right">性别</td>
								<td>
								<select title="病人性别" keyIndex="No" NoChange="No"
																size="1"  method="=" readonly  AllowNull="Yes"
																id="sex" ColumnDefault="">
																<option></option>
																
																<option value="1">男</option>
																<option value="2">女</option>
																
																</select>
																</td>
							</tr>
							<tr>
								<td align="right">年龄</td>
								<td><input type="text" method="=" id="uage" ></td>
							</tr>
							<tr>
								<td align="right">送检单位</td>
								<td><input type="text" method="=" id="sjyy" value="本院"></td>
							</tr>
							<tr>
								<td align="right">送检科室</td>
								<td><input type="text" id="sjks" method="="></td>
							</tr>
							<tr>
								<td align="right">床号</td>
								<td><input type="text" id="ybdjch"  name="ybdjch" method="="></td>
							</tr>
							<tr>
								<td align="right">住院号/门诊号</td>
								<td><input type="text" id="EName1" name="EName1" method="="></td>
							</tr>
							<tr>
								<td align="right">病人联系方式及地址</td>
								<td><input type="text" id="EName46" method="="></td>
							</tr>
							<tr>
								<td align="right">临床诊断及描述</td>
								<td><input type="text" id="lczd" method="="></td>
							</tr>
							<tr>
								<td align="right">标本类型</td>
								<td>
<input id="bbStatus_102" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="外周血">外周血
<input id="bbStatus_103" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="骨髓">骨髓
<input id="bbStatus_2" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="淋巴结">淋巴结
<input id="bbStatus_3" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="脐血">脐血
<input id="bbStatus_4" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="绒毛膜">绒毛膜
<br />
<input id="bbStatus_5" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="印迹片">印迹片
<input id="bbStatus_6" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="组织切片">组织切片
<input id="bbStatus_7" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="骨髓涂片">骨髓涂片
<input id="bbStatus_8" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="血涂片">血涂片
<br />
<input id="bbStatus_9" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="已染色玻片">已染色玻片
<input id="bbStatus_10" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="口腔上皮粘膜">口腔上皮粘膜
<input id="bbStatus_11" NoChange="No" method="=" type="CheckBox"  name="bbStatus" style="width:20px;border-bottom-width: 0px;" value="其他">其他</td>
							</tr>
							<tr>
								<td align="right">采集日期</td>
								<td><input type="text" id="bbcjsj" name="bbcjsj"  method="=" onfocus="setday(this);window.status='只能输入日期,格式yyyy-MM-DD';"></td>
							</tr>
							<tr>
								<td align="right">送检日期</td>
								<td><input type="text" name="sjdate" id="sjdate"  method="=" onfocus="setday(this);window.status='只能输入日期,格式yyyy-MM-DD';"></td>
							</tr>
							<tr>
								<td align="right">送检者</td>
								<td><input type="text" id="sjys" name="sjys" method="="></td>
							</tr>
							<tr>
								<td align="right">检验项目</td>
								<td><input id="EName41_0" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="白血病克隆性染色体异常检查" >白血病克隆性染色体异常检查
<br />
<input id="EName41_10010" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="淋巴瘤克隆性染色体异常检查">淋巴瘤克隆性染色体异常检查
<br />
<input id="EName41_10011" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="常规外周血染色体检查">常规外周血染色体检查
<br />
<input id="EName41_10012" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="常规外周血染色体检查+嵌合体染色体检查">常规外周血染色体检查+嵌合体染色体检查
<br />
<input id="EName41_4" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="高分辨率染色体检查">高分辨率染色体检查
<br />
<input id="EName41_5" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="白血病及肿瘤荧光原位杂交(FISH)检查">白血病及肿瘤荧光原位杂交(FISH)检查
<br />
<input id="EName41_6" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="白血病及肿瘤石蜡切片/印片荧光原位杂交(FISH)检查">白血病及肿瘤石蜡切片/印片荧光原位杂交(FISH)检查
<br />
<input id="EName41_7" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="遗传性疾病荧光原位杂交(FISH)检查">遗传性疾病荧光原位杂交(FISH)检查
<br />
<input id="EName41_8" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="肿瘤融合基因的PCR分子检查">肿瘤融合基因的PCR分子检查
<br />
<input id="EName41_9" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="脆性X染色体综合征基因检测">脆性X染色体综合征基因检测
<br />
<input id="EName41_10" NoChange="No" method="=" type="CheckBox"  name="EName41" style="width:20px;border-bottom-width: 0px;" value="遗传性耳聋基因突变检查">遗传性耳聋基因突变检查
<input id="EName41_11" NoChange="No" method="=" style="border-Bottom:red 2px solid;width:20px" checked type="CheckBox"  name="EName41"  value="普通外周血染色体检查">普通外周血染色体检查
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
				<input type=button name="Submit2" onclick="FireQuery();" value="保 存" id="Submit2" runat="server" onserverclick="Submit2_ServerClick"> </label>
											
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
							document.getElementById("uname").value='<%=(ds.Tables[0].Rows[0]["CName"].ToString())%>';    //姓名
							document.getElementById("lczd").value='<%=(ds.Tables[0].Rows[0]["zdy1"].ToString())%>';      //临床诊断
							document.getElementById("bbcjsj").value='<%=(ds.Tables[0].Rows[0]["zdy2"].ToString())%>';    //采集日期
							document.getElementById("ybdjch").value='<%=(ds.Tables[0].Rows[0]["Bed"].ToString())%>';     //床号
							document.getElementById("EName1").value='<%=(ds.Tables[0].Rows[0]["Serialno"].ToString())%>';     //流水号
							document.getElementById("sjdate").value='<%=(ds.Tables[0].Rows[0]["FlagDateDelete"].ToString())%>';    // 送检日期
							document.getElementById("sjks").value='<%=(ds.Tables[0].Rows[0]["DeptName"].ToString())%>';    //送检科室
							
							document.getElementById("sex").value='<%=(ds.Tables[0].Rows[0]["GenderNo"].ToString())%>';  //性别
							
							var SampleType = "bbStatus_"+<%=(ds.Tables[0].Rows[0]["SampleTypeNo"].ToString())%>;
							document.getElementById(SampleType).checked = true;                                   //样本类型
							 
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
