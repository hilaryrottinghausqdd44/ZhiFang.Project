<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.add" Codebehind="add.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title><%if(Request.QueryString["btnid"].ToString()=="BAdd")
			Response.Write("正在增加数据");
			else if(Request.QueryString["btnid"].ToString()=="BModify")
			Response.Write("正在修改数据");
			else if(Request.QueryString["btnid"].ToString()=="viewinfo")
			Response.Write("正在浏览数据");
			else
			Response.Write("");
			%></title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <script>
        var btnid = '<%=Request.QueryString["btnid"]%>';
        var boolDisabled = false;
		function EnableElement(bClear,BKeyIndexEnable,bDisabled) {
		    if (bDisabled == true || bDisabled == false) {
		        boolDisabled = bDisabled;
		    }
				CollectWhereClause(this.frames["Right"].Form1.childNodes,bClear,BKeyIndexEnable);	
				this.frames["Right"].Form1.hAction.value='<%=Request.QueryString["btnid"]%>';
			}
		function DelData()
			{
				this.frames["Right"].Form1.hAction.value='<%=Request.QueryString["btnid"]%>';
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
					    if (kids[i].NoChange == "No") //只读字段
					    {
					        //alert(kids[i].title);
					        if (kids[i].disabled == true)
					            kids[i].disabled = false;
					    }
					    else {
					        kids[i].style.backgroundColor = '#EFEFEF';
					        //kids[i].disabled = true;
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
//						if (!boolDisabled) {
////						    var thisobj = document.all[kids[i].id];
////						    window.status=kids[i].id;
////						    if (thisobj != null) {
////						        alert(thisobj.id);
////						    }
////						    if (thisobj != null) {
////						        kids[i].disabled = false;
////						        kids[i].readOnly = true;
////						        //alert(kids[i].style.readOnly);
////						    }
//						    kids[i].disabled = false;
//						    kids[i].readOnly = true;
//						    kids[i].style.readOnly = true;
//						}
					}
					
					//==================TextArea=====================
					if(kids[i].nodeName.toUpperCase() =='TEXTAREA')
					{
					    if (kids[i].NoChange == "No") //只读字段
					    {
					        if (kids[i].disabled == true)
					            kids[i].disabled = false;
					    }
					    else {
					        kids[i].style.backgroundColor = '#EFEFEF';
					    }
						if(bClear)
						{
							kids[i].value = "";
			            }
//			            if (!boolDisabled) {
//			                kids[i].style.border = '1px solid';
			            //			            }
			            if (kids[i].NoChange == "Yes") //只读字段
			            {
			                kids[i].disabled = false;
			                kids[i].disabled = true;
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

						if (kids[i].NoChange == "Yes") //只读字段
						{
						    kids[i].disabled = false;
						    kids[i].options[kids[i].selectedIndex].style.backgroundColor = 'white';
						    kids[i].disabled = true;
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
//		            if (!boolDisabled) {
//		                try {
//		                    kids[i].disabled = false;
//		                    kids[i].style.border = '1px solid';
//		                }
//		                catch (e) { }
//		            }
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
						    if (boolReplace && (kids[i].value == ""))//||kids[i].keyIndex=="Yes"))
						    {
						        kids[i].value = "";
						        kids[i].value = kids[i].ColumnDefault;
						        if (kids[i].keyIndex == "Yes") {
						            kids[i].value += "??";
						        }
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

			function FireQuery(obj) {
			    //alert(obj == void 0);
			    if ((obj == void 0) ||obj == 'undefined' || obj.id == null || obj.id == 'undefined')
			        return;
				switch(obj.id)
				{
				    case 'BSave':
				        obj.style.border = '#ccccff 0px outset';
				        //if(buttLastClicked!=null&&buttLastClicked.id!='BSave')


				        //{

				        var strActionButton = "";
				        var strActionName = "";
				        if (btnid == 'BAdd') {
				            strActionButton = "您正在增加一条新记录数据\r";
				            strActionName = "增加";
				        }

				        if (btnid == 'BModify') {
				            strActionButton = "您正在修改一条记录数据\r";
				            strActionName = "修改";
				        }

				        if (btnid.id == 'BBatch') {
				            strActionButton = "您正在批量增加数条新记录数据,保存会根据设置来产生批量结果\r";
				            strActionName = "批量增加";
				        }

				        ConfirmNotAllowNull(this.frames["Right"].Form1.document.all['TableData'].childNodes);

				        var notAllowNullObj = this.frames["Right"].Form1.document.all['hNotAllowNull'].value;
				        if (notAllowNullObj != '') {
				            alert(notAllowNullObj + "不能为空");
				            this.frames["Right"].Form1.document.all['hNotAllowNull'].value = '';

				            return;
				        }


				        var Bconfirm = confirm(strActionButton + "确认要 [" + strActionName + "] 以下数据吗？\r\r");

				        if (Bconfirm) {

				            var frm = this.frames["Right"].frames;
				            //提交每个新闻浏览时的保存内容,如规范式的录入内容

				            for (var iF = 0; iF < frm.length; iF++) {
				                if (frm(iF).location.href.indexOf("inputBrowseNews.aspx") <= 0)
				                    continue;
				                //if(frm(iF).parent.document.all[ColumnName].value.length>0)
				                //	continue;

				                var newIframObjTextBox1 = frm(iF).document.all["TextBox1"];

				                newIframObjTextBox1.value = frm(iF).document.body.innerHTML;
				                //alert(frm(iF).name);
				                if (newIframObjTextBox1.value.indexOf("<FORM ") > 0) {

				                    newIframObjTextBox1.value = newIframObjTextBox1.value.substr(0, newIframObjTextBox1.value.indexOf("<FORM "));

				                    var fileName = RandomNum() + ".xml";
				                    if (btnid == 'BAdd') {
				                        if (frm(iF).name.length > 0) {
				                            //alert(frm(iF).Form1.action);
				                            //alert(frm(iF).Form1.action.length>frm(iF).Form1.action.indexOf("&Template=")+11);
				                            if ((frm(iF).Form1.action.indexOf("&Template=") > 0) && (frm(iF).Form1.action.length > frm(iF).Form1.action.indexOf("&Template=") + 11)) {

				                                var ColumnName = frm(iF).name.substr(3);
				                                if (frm(iF).parent.document.all[ColumnName].value.length == 0)
				                                    frm(iF).parent.document.all[ColumnName].value = fileName;
				                                //alert(frm(iF).Form1.action);
				                                var shref = frm(iF).Form1.action.substr(0, frm(iF).Form1.action.indexOf("&Template="));
				                                //alert(frm(iF).parent.document.all[ColumnName].value);
				                                frm(iF).parent.document.all[ColumnName].disabled = false;

				                                shref = shref + "&FileName=" + fileName;
				                                frm(iF).Form1.action = shref;
				                                //alert(shref);
				                                frm(iF).Form1.submit();

				                            }

				                        }
				                    }
				                    else {
				                        try {
				                            frm(iF).Form1.submit();
				                        }
				                        catch (e) { }
				                    }
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
				            var myDataRun = CollectDataRun(this.frames["Right"].Form1);

				            this.frames["Right"].Form1.submit();
				            //alert(this.frames["Right"].Form1.document.all['hQueryCollection'].value);

				            //return;
				            //this.frames["Right"].Form1.document.all['hNotAllowNull']
				            window.status = "完成！！！";
				            if (opener != null && opener.document.getElementById("BAdd") != null) {
				                opener.document.getElementById(btnid).style.border = '#ccccff 0px outset';
				            }

				            //window.close();
				            window.setInterval(ReturnState, 1000);    //询问iframe的状态
				        }
				        else {
				            window.status = "放弃保存";
				            buttLastClicked = obj;
				            var strUrl = this.frames["Right"].location.href;
				            if (strUrl.substr(strUrl.length - 1) == "#")
				                strUrl = strUrl.substr(0, strUrl.length - 1);
				            this.frames["Right"].location = strUrl; //Form1.reset();
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
			function ReturnState() {
			    if (window.frames["Right"].document.readyState == "complete") {

			        var frmFromListWindow = window.opener.frames["ContentMain"];
			        if (frmFromListWindow != null) {
			            frmFromListWindow.Form1.submit();
			            //            var parentQuery = frmFromListWindow.parent.frames["Top"];
			            //            if (parentQuery != null) {
			            //                parentQuery.Form1.submit();
			            //            }
			        }
			        else {
			            var frmFromMainButtons = window.opener.frames["Right"];
			            if (frmFromMainButtons != null) {
			                //frmFromMainButtons.document.location.href = frmFromMainButtons.document.location.href;
			                var parentQuery = frmFromMainButtons.parent; //.parent.frames["Top"];
			                if (parentQuery != null) {
			                    parentQuery.Form1.submit();
			                }
			            }
			        }
			        window.close();
			    }
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
						        if (kids[i].NoChange == "Yes") {
						            if (kids[i].value == "" && kids[i].AllowNull == "No") {
						                kids[i].style.backgroundColor = 'Coral';
						                this.frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].title;
						                return true;
						            }
						        }
						        else if (kids[i].value == "" && kids[i].AllowNull == "No") {
						            kids[i].select();
						            kids[i].focus();
						            kids[i].style.backgroundColor = 'Coral';
						            this.frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].title;
						            return true;
						        }
						        break;
				            case "RADIO":
				                //	                if (kids[i].AllowNull == "No") {
				                //	                    
				                //	                }
				                var strRadioID = kids[i].name;

				                var rdAll = kids[i].ownerDocument.all[strRadioID];
				                if (rdAll != null) {
				                    var bRadioChecked = false;
				                    for (var iRadio = 0; iRadio < rdAll.length; iRadio++) {
				                        if (rdAll[iRadio].checked) {
				                            bRadioChecked = true;
				                        }
				                    }
				                    if (!bRadioChecked) {
				                        kids[i].style.backgroundColor = 'Coral';
				                        this.frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].value + "...";
				                        return;
				                    }
				                }
				                break;

				            case "CHECKBOX":
				                var strRadioID = kids[i].name;

				                var rdAll = kids[i].ownerDocument.all[strRadioID];
				                if (rdAll != null) {
				                    var bRadioChecked = false;
				                    for (var iRadio = 0; iRadio < rdAll.length; iRadio++) {
				                        if (rdAll[iRadio].checked) {
				                            bRadioChecked = true;
				                        }
				                    }
				                    if (!bRadioChecked) {
				                        kids[i].style.backgroundColor = 'Coral';
				                        this.frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].value + "...";
				                        return;
				                    }
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
								    var checkBoxList = window.frames["Right"].Form1.document.all[kids[i].name]; //frames["ContentMain"].
									
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
						    kids[i].value = kids[i].value.replace(/[\t]/g, '');
						    hDataRun += "\t" + kids[i].id + kids[i].method + kids[i].value;
							
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

//			var num = 0;
//			var k = -1;
//			var selecto = null;
//			var g_iFrame = "";
//			var g_objID = "";
//			

//			function keyPressdiv(key, iFrame, ObjID, OrgValue) {
//			    //alert(key);
//			    if (key == 13) {
//			        DivSetVisible(false, 0, 0,'','');
//			        return;
//			    }
//			    if (key != 40 && key != 38)
//			        return;
//			    g_iFrame = iFrame;
//			    g_objID = ObjID;
//			    var DivRef = document.getElementById('PopupDiv');
//			    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
//			        num = DivRef.firstChild.rows.length;
//			    }
//			    if (num <= 0)
//			        return;
//			    //alert(key);
//			    if (PopupDiv.style.display == "block") {
//			        //下
//			        if (key == 40) {
//			            k++;
//			            if (k >= num) {
//			                k = -1;
//			            }
//			        }
//			        //上
//			        if (key == 38) {
//			            k--;
//			            if (k < 0) {
//			                k = num;
//			            }
//			        }
//			        //alert(k);
//			        
//			        for (var i = 0; i < num; i++) {
//			            mouseOut(DivRef.firstChild.rows[i]);
//			        }
//			        if (k >= 0 && k < num) {//选定值
//			            mouseOver(DivRef.firstChild.rows[k]);
//			            selecto = DivRef.firstChild.rows[k].cells[0].innerHTML;
//			            SelectV(selecto);
//			        }
//			        else if(OrgValue.indexOf('=')>0)//原始值
//			        {
//			            OrgValue=OrgValue.substr(OrgValue.indexOf('=')+1);
//			            SelectV(OrgValue);
//			        }
//			    }
//			}

//			function innerHTMLdiv(strList) {
//			    var DivRef = document.getElementById('PopupDiv');
//			    DivRef.innerHTML = strList;
//			    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
//			        num = DivRef.firstChild.rows.length;
//			    }
//			}

//			
//			function SelectV(str) {
//			    window.frames[g_iFrame].SetV(g_objID, str);
//			}

//			function ClickSelectV(str) {
//			    window.frames[g_iFrame].SetV(g_objID, str);
//			    DivSetVisible(false, 0, 0, '', '');
//			}
//			function mouseOver(e) {
//			    e.style.backgroundColor = "blue";
//			    e.style.color = "white";
//			    //e.style.fontWeight = "bold";
//			}
//			function mouseOut(e) {
//			    e.style.backgroundColor = "#dedede";
//			    e.style.color = "black";
//			    //e.style.fontWeight = "normal";
//			}
//			function DivSetVisible(state, objTop, objLeft, iFrame, ObjID) {
//			    var DivRef = document.getElementById('PopupDiv');
//			   
//			    DivRef.style.top = objTop; //document.body.scrollTop + event.clientY + 10;
//			    DivRef.style.left = objLeft; //document.body.scrollLeft + event.clientX;
//			    
//			    var IfrRef = document.getElementById('framePopupDiv');
//			    if (state) {
//			        DivRef.style.display = "block";
//			        IfrRef.style.width = DivRef.offsetWidth;
//			        IfrRef.style.height = DivRef.offsetHeight;
//			        IfrRef.style.top = DivRef.style.top;
//			        IfrRef.style.left = DivRef.style.left;
//			        IfrRef.style.zIndex = DivRef.style.zIndex - 1;
//			        IfrRef.style.display = "block";
//			        g_iFrame = iFrame;
//			        g_objID = ObjID;
//			    }
//			    else {
//			        DivRef.style.display = "none";
//			        IfrRef.style.display = "none";
//			    }
//			} 
    </script>
    <script language=javascript for="hLoaded" event="onpropertychange">
		
		//alert(btnid);
		switch(btnid)
		{
			case 'BAdd':
				EnableElement(true,true,true);
				SetDefaultValue(true);
				ClearElement("新闻");
				break;
			case 'BModify':
				EnableElement(false,true,true);
				SetDefaultValue(true);
				break;
			case 'BCopy':
				EnableElement(false,true,true);
				ClearElement("文件");
				ClearElement("新闻");
				SetDefaultValueForCopy();
				break;
			case 'BDelete':
				DelData();
			    break;
            case 'viewinfo':
				EnableElement(false,true,false);
				//SetDefaultValue(true);
				break;
		}
	</script>
  </head>
  <body MS_POSITIONING="GridLayout" topmargin=0 leftmargin=0 rightmargin=0 bottommargin=0>
	
    <table width=100% height=100%>
	<tr>
	<TD vAlign="top">
					
					<iframe id="Right" name="Right" src="InputOneTableMain.aspx?<%=Request.ServerVariables["Query_String"]%>" frameBorder="0" width="100%"
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
<iframe id="frmDataRun" name="frmDataRun" 
					src="" 
					frameBorder="0" width="100%"
						scrolling="auto" height="0"></iframe>
	<input type=hidden name="hLoaded" id="hLoaded" value="0">
    
  </body>
  <script>
	<%if(Request.QueryString["btnid"].ToString()=="viewinfo"){%>
	document.all.BSave.style.display="none";
	document.all.BCancel.style.display="none";
	<%}%>
  </script>
</html>
