
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>正在修改数据</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script>
		var btnid = 'BModify';
		function EnableElement(bClear,BKeyIndexEnable)
			{
				CollectWhereClause(this.frames["Right"].Form1.childNodes,bClear,BKeyIndexEnable);	
				this.frames["Right"].Form1.hAction.value='BModify';
			}
		function DelData()
			{
				this.frames["Right"].Form1.hAction.value='BModify';
				this.frames["Right"].Form1.submit();
				window.close();
				//alert('123');
			}
		//所有元素可用
		function CollectWhereClause(kids,bClear,BKeyIndexEnable)
			{
				for(var i=0;i<kids.length;i++)
				{
					
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						//alert(kids[i].NoChange);
						if(kids[i].NoChange == "No") //只读字段
						{
							//alert(kids[i].title);
							if(kids[i].disabled==true)
								kids[i].disabled=false;
						}
						//if(bClear&&(kids[i].type.toUpperCase()=='TEXT'))//重大问题，不能设置||kids[i].type.toUpperCase()=='HIDDEN'))
						if(bClear)
						{
							if(kids[i].type.toUpperCase()=='TEXT')
							{
								kids[i].value="";
								if(BKeyIndexEnable==null||BKeyIndexEnable==false)
								{
									if(kids[i].keyIndex=='Yes')
									{
										kids[i].disabled=true;
									}
								}
							}
							
						}
						
					}
					
					//==================TextArea=====================
					if(kids[i].nodeName.toUpperCase() =='TEXTAREA')
					{
						if(kids[i].NoChange == "No") //只读字段
						{
							if(kids[i].disabled==true)
								kids[i].disabled=false;
						}
						if(bClear)
						{
							kids[i].value = "";
						}
					}
					if(kids[i].nodeName.toUpperCase()=='SELECT')
					{
						if(kids[i].NoChange == "No") //只读字段
						{
							if(kids[i].disabled)
								kids[i].disabled=false;
						}
						if(bClear)
						{
							kids[i].options[0].selected = true;
						}
						
						if(BKeyIndexEnable==null||BKeyIndexEnable==false)
						{
							if(kids[i].keyIndex=='Yes')
							{
								kids[i].options[0].selected=true;
								kids[i].disabled=true;
							}
						}
						
							
					}
					if(kids[i].nodeName.toUpperCase()=='A')
					{
						if(kids[i].NoChange == "No") //只读字段
						{
							if(kids[i].disabled)
								kids[i].disabled=false;
						}
					}
					if(kids[i].hasChildNodes)
						CollectWhereClause(kids[i].childNodes,bClear,BKeyIndexEnable);
				}
			}
			
			//设置默认值 增加 boolReplace是否将当前的默认值覆盖以前的值，空值除外
			
			function SetDefaultValue(boolReplace)
			{
				SetDefaultValues(this.frames["Right"].Form1.childNodes,boolReplace)
				
			} 
			function SetDefaultValues(kids,boolReplace)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
						{
							if(boolReplace && (kids[i].value==""||kids[i].keyIndex=="Yes"))
							{
								kids[i].value="";
								kids[i].value=kids[i].ColumnDefault;
							}
							//else if(kids[i].value=="")
							//{
							//	kids[i].value=kids[i].ColumnDefault;
							//}
							//alert(kids[i].ColumnDefault);
						}
					}
					if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
					{
						if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
						{
							if(boolReplace || (kids[i].value==""||kids[i].keyIndex=="Yes") ) //修改情况下默认值调节boolReplace && (kids[i].value==""||kids[i].keyIndex=="Yes")
							{
								
								kids[i].value="";
								kids[i].value=kids[i].ColumnDefault;
							}
						}
					}
					
					if(kids[i].nodeName.toUpperCase()=='SELECT')
					{
						if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
						{
							if(boolReplace||(kids[i].options.length>0&&kids[i].options[kids[i].selectedIndex].text==""))
							{
								for(var item=0;item<kids[i].options.length;item++)
								{
									if(kids[i].options[item].text==kids[i].ColumnDefault)
									{	
										kids[i].options[item].selected=true;
										break;
									}	
								}
							}
							//else if(kids[i].options[kids[i].selectedIndex].text=="")
							//{
							//	kids[i].value=kids[i].ColumnDefault;
							//}
							
						}
						
					}
					//=================清空iframe中的东西===============
					if(kids[i].nodeName.toUpperCase()=='IFRAME')
					{
						//kids[i].src="";
					}
					
					if(kids[i].hasChildNodes)
						SetDefaultValues(kids[i].childNodes,boolReplace);
				}
			}
			
			function FireMove(obj)
			{
				if(obj.style.border!='#ccccff thin inset')
					obj.style.border='#ccccff thin outset';
			}
			function FireOut(obj)
			{
				if(obj.style.border!='#ccccff thin inset')
					obj.style.border='#ccccff 0px outset';
			}
			function RandomNum()
			{
					var today = new Date();
					var yy = eval(today.getYear())+'';
					var mon = eval(today.getMonth()) + '';
					//mm=(mm.length>1)?mm:0+mm;
					var dd = eval(today.getDate()) + '';
					var hh = eval(today.getHours()) + '';
					var mm = eval(today.getMinutes()) + '';
					var ss = eval(today.getSeconds()) +'';
						
					var vrand = Math.round(Math.random()*1000000);
					//var vrand1 = Math.round(Math.random()*1000000);
					//alert(ss.length);

					if(mon.length==1)
					{
						mon = '0' + mon;
					//	alert(mon);
					}
					//alert(yy + mon + dd + hh + mm + ss + vrand);
					return yy + mon +dd + "-" + hh + mm + ss + "-" + vrand;
			}
			
			var buttLastClicked=null
			
			function FireQuery(obj)
			{
				switch(obj.id)
				{
					case 'BSave':
						obj.style.border='#ccccff 0px outset';
						//if(buttLastClicked!=null&&buttLastClicked.id!='BSave')
							
							
						//{
						
							var strActionButton="";
							var strActionName="";
							if(btnid=='BAdd'){
								strActionButton="您正在增加一条新记录数据\r";
								strActionName="增加";}
							/*
							if(btnid=='BCopy'){
							
								//是否有子表复制-----------------------------------------------------------------------------------------
								var subTables=window.showModalDialog('SelectModalDialog.aspx?SelectSubTablesForCopy.aspx?TableName=' 
									+ hTableEName.value + '&DataXPath=' + document.all["hParentXpath"].value 
									+ '&btnid=BModify&TableEName=Project&db=CRM_Project&name=ZF%ba%cf%cd%ac%b9%dc%c0%ed&WindowSize=MAX&RBACModuleID=412&DataXPath=Table[@EName='Project']/tr[td[@Column='ID'%20and%20text()='915']]&AssessorXpath=Table[@EName='Project']/tr[td[@Column='ID'%20and%20text()='915']]','',
									'dialogWidth:588px;dialogHeight:468px;help:no;scroll:auto;status:no');
																														
								if ( typeof(subTables) != 'undefined'&&subTables!="")
								{
									this.frames["Right"].Form1.hSubTablesCopy.value=subTables;
								}
								//是否有子表复制-----------------------------------------------------------------------------------------
								
								strActionButton="您正在复制一条已经存在的记录数据，使之方便的成为一条新数据\r";
								strActionName="复制";}
						*/
							if(btnid=='BModify'){
								strActionButton="您正在修改一条记录数据\r";
								strActionName="修改";}
									
							if(btnid.id=='BBatch'){
								strActionButton="您正在批量增加数条新记录数据,保存会根据设置来产生批量结果\r";
								strActionName="批量增加";}
							
							ConfirmNotAllowNull(this.frames["Right"].Form1.document.all['TableData'].childNodes);
								
								var notAllowNullObj=this.frames["Right"].Form1.document.all['hNotAllowNull'].value;
								if(notAllowNullObj != '')
								{
									alert(notAllowNullObj +"不能为空");
									this.frames["Right"].Form1.document.all['hNotAllowNull'].value='';
									
									return;
								}
								
								
							var Bconfirm=confirm(strActionButton + "确认要 ["+strActionName+"] 以下数据吗？\r\r");
							
							if(Bconfirm)
							{
								
								var frm=this.frames["Right"].frames;
								//提交每个新闻浏览时的保存内容,如规范式的录入内容
								
								for(var iF=0;iF<frm.length;iF++)
								{
									if(frm(iF).location.href.indexOf("inputBrowseNews.aspx")<=0)
										continue;
									//if(frm(iF).parent.document.all[ColumnName].value.length>0)
									//	continue;
									
									var newIframObjTextBox1=frm(iF).document.all["TextBox1"];
								
									newIframObjTextBox1.value=frm(iF).document.body.innerHTML;
									//alert(frm(iF).name);
									if(newIframObjTextBox1.value.indexOf("<FORM ")>0)
									{
										
										newIframObjTextBox1.value=newIframObjTextBox1.value.substr(0,newIframObjTextBox1.value.indexOf("<FORM "));
									
										var fileName=RandomNum() + ".xml";
										if(btnid=='BAdd')
										{
											if(frm(iF).name.length>0)
											{
												//alert(frm(iF).Form1.action);
												//alert(frm(iF).Form1.action.length>frm(iF).Form1.action.indexOf("&Template=")+11);
												if((frm(iF).Form1.action.indexOf("&Template=")>0)&&(frm(iF).Form1.action.length>frm(iF).Form1.action.indexOf("&Template=")+11))
												{
													
													var ColumnName=frm(iF).name.substr(3);
													if(frm(iF).parent.document.all[ColumnName].value.length==0)
														frm(iF).parent.document.all[ColumnName].value=fileName;
													//alert(frm(iF).Form1.action);
													var shref=frm(iF).Form1.action.substr(0,frm(iF).Form1.action.indexOf("&Template="));
													//alert(frm(iF).parent.document.all[ColumnName].value);
													frm(iF).parent.document.all[ColumnName].disabled=false;
													
													shref=shref + "&FileName=" + fileName;
													frm(iF).Form1.action=shref;
													//alert(shref);
													frm(iF).Form1.submit();
													
												}
												
											}
										}
										else	
											frm(iF).Form1.submit();
									}
								
								}
								//return;
								//alert('a');
								//meizzDateLayer
								//alert(this.frames["Right"].frames.length);
								
								//newIframObjTextBox1.value=newIframObjTextBox1.value.substr(0,newIframObjTextBox1.value.indexOf("<FORM "));
								
								//alert(newIframObjTextBox1.value);
								//alert(this.frames["Right"].frames["frmEName8"].document.all["TextBox1"].value);
								//this.frames["Right"].frames["frmEName8"].Form1.submit();
								//return false;
								var myDataRun=CollectDataRun(this.frames["Right"].Form1);
							
								this.frames["Right"].Form1.submit();
								//alert(this.frames["Right"].Form1.document.all['hQueryCollection'].value);
								
								//return;
								//this.frames["Right"].Form1.document.all['hNotAllowNull']
								window.status="完成！！！";
								if(opener!=null&&opener.document.getElementById("BAdd")!=null)
								{
									opener.document.getElementById(btnid).style.border='#ccccff 0px outset';
								}
								window.close();
							}
							else
							{
								window.status="放弃保存";
								buttLastClicked=obj;
								var strUrl=this.frames["Right"].location.href;
								if(strUrl.substr(strUrl.length-1)=="#")
									strUrl=strUrl.substr(0,strUrl.length-1);
								this.frames["Right"].location=strUrl;//Form1.reset();
								return;
							}
							/*
						}
						else
						{
							window.status="不能保存，请选择增加或修改复制操作";
							buttLastClicked=obj;
							return;
						}
						*/
						break;
			case 'BCancel':
						var strUrl=this.frames["Right"].location.href;
						if(strUrl.substr(strUrl.length-1)=="#")
							strUrl=strUrl.substr(0,strUrl.length-1);
						//this.frames["Right"].location=strUrl;//Form1.reset();
						obj.style.border='#ccccff 0px outset';
						window.status="取消操作！！！,已经返回查询页面！";
						
						if(opener!=null&&opener.document.getElementById(btnid)!=null)
						{
							opener.document.getElementById(btnid).style.border='#ccccff 0px outset';
						}
					
						
						window.close();
						
			default:
						break;
						
				}
				buttLastClicked=obj;
			}
			
			function ConfirmNotAllowNull (kids)
			{
				
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						switch(kids[i].type.toUpperCase())
						{
							case "TEXT":
								if(kids[i].value==""&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
								{
									kids[i].select();
									kids[i].focus();
									kids[i].style.backgroundColor='Coral';
									this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
									return true;
								}
								break;
							case "RADIO":
								
								break;
							case "CHECKBOX":
								if((kids[i].options.length==0||kids[i].options[kids[i].selectedIndex].text=="")&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
								{
									kids[i].focus();
									kids[i].style.backgroundColor='Coral';
									this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
									return;
								}
								break;
						
							default:
								break;
						}
						
					}
					if(kids[i].nodeName.toUpperCase()=='SELECT')
					{
						if((kids[i].options.length==0||kids[i].selectedIndex==-1||kids[i].options[kids[i].selectedIndex].text=="")&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
						{
							kids[i].focus();
							kids[i].style.backgroundColor='Coral';
							this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
							return;
						}
					}
					
					//=================TextArea节点=================
					if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
					{
						if(kids[i].value==""&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
						{
							kids[i].select();
							kids[i].focus();
							kids[i].style.backgroundColor='Coral';
							this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
							return;
						}
					}
					//-------------------End------------------------
					if(kids[i].hasChildNodes)
						ConfirmNotAllowNull(kids[i].childNodes);
				}
			}
			//收集数据
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
									var checkBoxList=window.frames["ContentMain"].frames["Right"].Form1.document.all[kids[i].name];
									
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
				CollectDataRunList(myForm.document.all['TableData'].childNodes);
				myForm.hQueryCollection.value=hDataRun;
				//alert(hDataRun);
				return hDataRun;
				//return false;
			}
			
			function ClearElement(ColumnType)
			{
				ClearElementLoop(this.frames["Right"].Form1.childNodes,ColumnType);
			}
			
			function ClearElementLoop(kids,ColumnType)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')
					{
						if(kids[i].type.toUpperCase()=='TEXT')
						{
							if(kids[i].getAttribute("columnType")!=null&&kids[i].columnType==ColumnType)
								kids[i].value="";
						}
					}
					if(kids[i].hasChildNodes)
						ClearElementLoop(kids[i].childNodes,ColumnType);
				}
			}
			
			function SetDefaultValueForCopy()
			{
				SetDefaultValues(this.frames["Right"].Form1.childNodes,false)
			} 
			
				
			
			
    </script>
    <script language=javascript for="hLoaded" event="onpropertychange">
		
		
		switch(btnid)
		{
			case 'BAdd':
				EnableElement(true,true);
				SetDefaultValue(true);
				ClearElement("新闻");
				break;
			case 'BModify':
				EnableElement(false,true);
				SetDefaultValue(false);
				break;
			case 'BCopy':
				EnableElement(false,true);
				ClearElement("文件");
				ClearElement("新闻");
				SetDefaultValueForCopy();
				break;
			case 'BDelete':
				DelData();
			
			break;

		}
	</script>
  </head>
  <body MS_POSITIONING="GridLayout" topmargin=0 leftmargin=0 rightmargin=0 bottommargin=0>
	<table width=100% height=100%>
	<tr>
	<TD vAlign="top">
					
					<iframe id="Right" name="Right" src="InputOneTableMain.aspx?btnid=BModify&TableEName=Project&db=CRM_Project&name=ZF%ba%cf%cd%ac%b9%dc%c0%ed&WindowSize=MAX&RBACModuleID=412&DataXPath=Table[@EName='Project']/tr[td[@Column='ID'%20and%20text()='915']]&AssessorXpath=Table[@EName='Project']/tr[td[@Column='ID'%20and%20text()='915']]" frameBorder="0" width="100%"
							scrolling="auto" height="100%"></iframe>
		</TD>
	</tr>
	<tr height=24>
		<td align=center>
		<img id="BSave" src="../image/middle/save.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="保存操作">	&nbsp;&nbsp;
					<img id="BCancel" src="../image/middle/cancel.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="取消操作"></td>
	</tr>
	</table>

	<input type=hidden name="hLoaded" id="hLoaded" value="0">

  </body>
  <script>
	
  </script>
</html>
