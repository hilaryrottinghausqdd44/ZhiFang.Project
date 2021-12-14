<%@ Import Namespace="System.Xml" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Main" Codebehind="Main.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Main</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<%if(cssFile.Trim()==""){%>
		<LINK href="css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<%}else{%>
		<LINK href="<%=cssFile%>" type="text/css" rel="stylesheet">
		<%}%>
		<script language="javascript" event="onpropertychange" for="RelocateListRow">
		    //alert('aaabb');
		</script>
		<script language="javascript">
		    
			function openWin(url,w,h)
			{
				var px=Math.round((window.screen.availWidth-w)/2);
				var py=Math.round((window.screen.availHeight-h)/2);
				//var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
				var mywin=window.open(url,"MyWin","toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top=0,left=0,width="+w+",height="+h+",scrollbars=yes");
			}
			
			function openWinPositionSize(url,sTarget, ileft,itop,iwidth,iheight)
			{
				//alert(" " + window.screen.availWidth + "," + ileft + "," + itop + "," + iwidth + "," + iheight);
				//var mywin=window.open(url,"MyWin","toolbar=no,location=no,menubar=no,resizable=no,top="+py+",left="+px+",width="+w+",height="+h+",scrollbars=yes,status=yes");
				//window.prompt("url", url);
				//alert("toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top="+itop +",left="+ileft +",width="+iwidth+"px,height="+iheight+"px,scrollbars=yes");
				var mywin=window.open(url,"" + sTarget,"toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top="+itop +",left="+ileft +",width="+iwidth+"px,height="+iheight+"px,scrollbars=yes");
			}

            function openWinPositionSizeUrl(url,sTarget, strWinPositionSize)
            {
                var strPositionSize=strWinPositionSize;
				var arrPositionSize=strPositionSize.split(",");
				var PositionLeft=window.screen.availWidth * .1;
				var PositionTop=window.screen.availHeight * .1;
				var PositionWidth=window.screen.availWidth * .8;
				var PositionHeight=window.screen.availHeight * .8;
				
				if(arrPositionSize.length==4)
				{
					try
					{
						var iPositionLeft=arrPositionSize[0];
						var iPositionTop=arrPositionSize[1];
						var iPositionWidth=parseFloat(arrPositionSize[2]);
						//alert(Math.round(iPositionWidth));
						var iPositionHeight=parseFloat(arrPositionSize[3]);
						switch(arrPositionSize[0])
						{
							case "��":
								PositionLeft=0;
								break;
							case "��":
								PositionLeft=window.screen.availWidth * (100-iPositionWidth)/200;
								PositionLeft=Math.round(PositionLeft);
								//alert(PositionHeight);
								break;
							case "��":
								PositionLeft=window.screen.availWidth * (100-iPositionWidth)/100;
								PositionLeft=Math.round(PositionLeft);
								break;
							default:
								try
								{
									PositionLeft=parseInt(iPositionLeft);
								}
								catch(e){}
								break;
						}
						switch(arrPositionSize[1])
						{
							case "��":
								PositionTop=0;
								break;
							case "��":
								PositionTop=window.screen.availHeight * (100-iPositionHeight)/200;
								PositionTop=Math.round(PositionTop);
								break;
							case "��":
								PositionTop=window.screen.availHeight * (100-iPositionHeight)/100;
								PositionTop=Math.round(PositionTop);
								break;
							default:
								try
								{
									PositionTop=parseInt(iPositionTop);
								}
								catch(e){}
								break;
						}
						
						switch(arrPositionSize[2])
						{
							default:
								try
								{
									PositionWidth=window.screen.availWidth * iPositionWidth/100;
									PositionWidth=Math.round(PositionWidth);
								}
								catch(e){}
								break;
						}
						
						switch(arrPositionSize[2])
						{
							default:
								try
								{
									PositionHeight=window.screen.availHeight * iPositionHeight/100;
									PositionHeight=Math.round(PositionHeight);
								}
								catch(e){}
								break;
						}
						//alert(PositionLeft +"-" + PositionTop +"-" + PositionWidth +"-" + PositionHeight)
						openWinPositionSize(url,sTarget,PositionLeft,PositionTop,PositionWidth,PositionHeight);
					}
					catch(e)
					{
						alert(e);
					}
				}
            }			
			var objLast=null;
			
			//�л��ӱ��ʱ�򴥷�����ֵhisOpen
			function LocateTable(TableName,obj,batched,batchButtons,stringAllButtons,stropen)
			{
				document.all["BAdd"].style.display="none";
				document.all["BDelete"].style.display="none";
				document.all["BCopy"].style.display="none";
				
				if(batched)
				{
					document.all["BBatch"].style.display="";
					if(batchButtons.indexOf("BAdd")>-1)
						document.all["BAdd"].style.display="";
					if(batchButtons.indexOf("BDelete")>-1)
						document.all["BDelete"].style.display="";
					if(batchButtons.indexOf("BCopy")>-1)
						document.all["BCopy"].style.display="";
				}
				else
				{
					document.all["BBatch"].style.display="none";
					
					document.all["BAdd"].style.display="";
					document.all["BDelete"].style.display="";
					document.all["BCopy"].style.display="";
				}
				obj.style.color='red';
				if(objLast!=null&&objLast!=obj)
					objLast.style.color='';
				
				hTableEName.value=TableName;
				//alert(stropen);
				hisOpen.value=stropen;
				//alert(hisOpen.value + 'title');
				//�ӱ�ķ�Χ���������������
				var AssessorXpath;
				if(TableName.indexOf("/")>-1)
					AssessorXpath=hParentXpath.value;
				else
					AssessorXpath="";
				
				if(objLast!=null)
				{
					var strTableNameTemp=TableName;
					if(TableName.indexOf(objLast.title)!=0)
					{
						if(TableName.indexOf("/")>-1)
							strTableNameTemp=strTableNameTemp.substr(0,strTableNameTemp.indexOf("/"));
							
						if(objLast.title.indexOf(strTableNameTemp)!=0)
							AssessorXpath="";
					}
				}
				
				ExpandCollapeTable(TableName);
				
				window.frames["Top"].location="Input/QueryTop.aspx?TableEName=" 
					+ TableName + "&AssessorXpath=" + AssessorXpath +"&<%=Request.ServerVariables["Query_String"]%>";
				window.frames["ContentMain"].location="Input/InputOneTable.aspx?TableEName=" 
					+ TableName + "&AssessorXpath=" + AssessorXpath +"&<%=Request.ServerVariables["Query_String"]%>";
				
				objLast=obj;
				
				loadButtons(stringAllButtons);
				//alert(hParentXpath.value);
			}
			function ExpandCollapeTable(TableName)
			{
				//TableAllTable
				//ֻ��������
				var allTables= document.all['TableAllTable'].childNodes[0].childNodes[0].childNodes;
				//alert(allTables[0].nodeName);
				
				for(var eachtable=0;eachtable<allTables.length;eachtable++)
				{
					if(TableName.indexOf("/")==-1)
					{
						if(allTables[eachtable].id.indexOf("/")!=allTables[eachtable].id.lastIndexOf("/"))
						{
							allTables[eachtable].style.width="0px";
							allTables[eachtable].childNodes[0].style.display="none";
						}
						else
						{
							allTables[eachtable].style.width="97px";
							allTables[eachtable].childNodes[0].style.display="";
						}
					}
					else if(TableName.indexOf("/")==TableName.lastIndexOf("/"))
					{ 
						if(allTables[eachtable].id.indexOf("/")==-1)
							continue;
						else if(allTables[eachtable].id.indexOf("/")==allTables[eachtable].id.lastIndexOf("/"))
						{
							allTables[eachtable].style.width="97px";
							allTables[eachtable].childNodes[0].style.display="";
							continue;
						}
						
						if(allTables[eachtable].id.indexOf("TD" + TableName)==0)
						{
							allTables[eachtable].style.width="97px";
							allTables[eachtable].childNodes[0].style.display="";
						}
						else
						{
							allTables[eachtable].style.width="0px";
							allTables[eachtable].childNodes[0].style.display="none";
						}
						
					}
				}
			}
			var buttLastClicked=null
			function FireQuery(obj)
			{
			    //alert(typeof(obj));
				if(window.frames["ContentMain"].document!=null
					&&window.frames["ContentMain"].frames["Right"]!=null
					&&window.frames["ContentMain"].frames["Right"].document!=null
					&&window.frames["ContentMain"].frames["Right"].document.readyState=="complete")
				;
				else
				{
					alert("�����������,���Ժ�");
					return false;
				}

				ss = hisOpen.value;
				obj.style.border='#ccccff thin inset';                 //ͻ����ʾ��ǰ��ť����ʽ
				if(buttLastClicked!=null&&buttLastClicked!=obj&&buttLastClicked.id!='BCollapse')
					buttLastClicked.style.border='#ccccff 0px outset';
				
				window.status="���ڲ���������";
				if(ss=='No')
				{
				
				switch(obj.id)
				{
					case 'BBatch':
						if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
						{
							obj.style.border='#ccccff 0px outset';
							buttLastClicked.style.border='#ccccff thin inset';
							
							window.status="����û����ɣ��������ȱ����ȡ��������";
							return;
						}
						if(buttLastClicked!=obj)
						{
							var r;
							r= window.showModalDialog("Input/BatchDialog.aspx?TableName=" + hTableEName.value + "&<%=Request.ServerVariables["Query_String"]%>", "", "");
							if ( typeof(r) != 'undefined'&&r!="")
							{
								window.frames["ContentMain"].frames["Right"].Form1.all["txtBatches"].value = r;
								window.frames["ContentMain"].frames["Right"].Form1.all["txtKeyIndexBatches"].innerHTML = "Ҫ�������ӵļ�¼�� " + r;
								window.frames["ContentMain"].frames["Right"].Form1.all["txtKeyIndexBatches"].style.display="";
							}
							else
							{
								obj.style.border='#ccccff 0px outset';
								return;
							}
							EnableElement(false,false);
							window.frames["ContentMain"].frames["Right"].Form1.hAction.value=obj.id;
						}
						
						break;
					case 'BAdd':
						
						//������Ӱ�ť���ٵ�����ӡ����ơ��޸ĵȴ���
						if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
						{
							obj.style.border='#ccccff 0px outset';
							buttLastClicked.style.border='#ccccff thin inset';
							
							window.status="����û����ɣ��������ȱ����ȡ��������";
							return;
						}
						
						//�����¼�
						if(buttLastClicked!=obj)
						{
							
							EnableElement(true,true);
							SetDefaultValue(true); 
							window.frames["ContentMain"].frames["Right"].Form1.hAction.value=obj.id;
							
						}
						break;
					case 'BDelete':
						if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
						{
							obj.style.border='#ccccff 0px outset';
							buttLastClicked.style.border='#ccccff thin inset';
							
							window.status="����û����ɣ��������ڲ���ɾ��������";
							return;
						}
						if(window.frames["ContentMain"].Form1.hiCalculator.value!=0)
						{
							var confirmDel=confirm("ȷ��Ҫɾ���˼�¼������Ҫ�Ǹñ��¼���ӱ�\n�ü�¼���ӱ�����Ҳ��ȫ��ɾ����");
							if(confirmDel)
							{
								window.frames["ContentMain"].frames["Right"].Form1.hAction.value=obj.id;
								window.frames["ContentMain"].frames["Right"].Form1.submit();
							}
						}
						else
							window.status='û��Ҫɾ���ļ�¼';
						obj.style.border='#ccccff 0px outset';
						
						break;
					case 'BCopy':
						if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
						{
							obj.style.border='#ccccff 0px outset';
							buttLastClicked.style.border='#ccccff thin inset';
							
							window.status="����û����ɣ��������ȱ����ȡ��������";
							return;
						}
						if(buttLastClicked!=obj)
						{
							EnableElement(false,true);
							ClearElement("�ļ�");
							ClearElement("����");
							SetDefaultValueForCopy();
							window.frames["ContentMain"].frames["Right"].Form1.hAction.value=obj.id;
						}
						
						break;
					case 'BModify':

						if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
						{
							obj.style.border='#ccccff 0px outset';
							buttLastClicked.style.border='#ccccff thin inset';
							
							window.status="����û����ɣ��������ȱ����ȡ��������";
							return;
						}
						window.frames["ContentMain"].frames["Right"].Form1.hAction.value=obj.id;
						EnableElement(false,true);
						
						SetDefaultValue(true);
						
						window.status="����ִ���޸����ݲ���������,����ͬʱ��������ɾ�����ƵȲ��������ȵ㱣���ȡ��";
						

						break;
					case 'BCollapse':
						if(obj.src.indexOf('image/middle/zhankai.jpg')>-1)
						{
							obj.src='image/middle/zhedie.jpg';
							obj.style.border='#ccccff 0px outset';
						}
						else
						{
							obj.src='image/middle/zhankai.jpg';
							obj.style.border='#ccccff thin inset';
						}
						
						window.frames["ContentMain"].frames["Right"].location=window.frames["ContentMain"].frames["Right"].location;//Form1.reset();
						
						
						window.status="��ɣ�����";
						break;
					case 'BSave':
						obj.style.border='#ccccff 0px outset';
						if(buttLastClicked!=null
							&&buttLastClicked.id!='BSave'
							&&(buttLastClicked.id=='BAdd'
							||buttLastClicked.id=='BCopy'
							||buttLastClicked.id=='BModify'
							||buttLastClicked.id=='BBatch'))
						{///B..B.B..B.B..Bugs??..????
							var myDataRun=CollectDataRun(window.frames["ContentMain"].frames["Right"].Form1);
							var strActionButton="";
							var strActionName="";
							if(buttLastClicked.id=='BAdd'){
								strActionButton="����������һ���¼�¼����\r";
								strActionName="����";}
							if(buttLastClicked.id=='BCopy'){
							
								//�Ƿ����ӱ���-----------------------------------------------------------------------------------------
								var subTables=window.showModalDialog('input/SelectModalDialog.aspx?SelectSubTablesForCopy.aspx?TableName=' 
									+ hTableEName.value + '&DataXPath=' + document.all["hParentXpath"].value 
									+ '&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>','',
									'dialogWidth:588px;dialogHeight:468px;help:no;scroll:auto;status:no');
																														
								if ( typeof(subTables) != 'undefined'&&subTables!="")
								{
									window.frames["ContentMain"].frames["Right"].Form1.hSubTablesCopy.value=subTables;
								}
								//�Ƿ����ӱ���-----------------------------------------------------------------------------------------
								
								strActionButton="�����ڸ���һ���Ѿ����ڵļ�¼���ݣ�ʹ֮����ĳ�Ϊһ��������\r";
								strActionName="����";
							}
							if(buttLastClicked.id=='BModify'){
								strActionButton="�������޸�һ����¼����\r";
								strActionName="�޸�";}
							if(buttLastClicked.id=='BBatch'){
								strActionButton="�������������������¼�¼����,�������������������������\r";
								strActionName="��������";}
							
							ConfirmNotAllowNull(window.frames["ContentMain"].frames["Right"].Form1.document.all['TableData'].childNodes);
								//alert(window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value);
								var notAllowNullObj=window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value;
								//debugger;
								if(notAllowNullObj != '')
								{
									alert(notAllowNullObj +"����Ϊ��");
									window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value='';
									buttLastClicked.style.border='#ccccff thin inset';
									return;
								}
								
							var Bconfirm=confirm(strActionButton + "ȷ��Ҫ ["+strActionName+"] ����������\r\r");
							if(Bconfirm)
							{
								showoToolTip();
								//window.frames["ContentMain"].frames["Right"].Form1.submit();
								//AsycMainDataOk Ϊ��α��治�ɹ�ʱ����?
								
								if(!AsycMainDataOk)
									ClockDataRun=window.setTimeout(getDataRunStat,100);
								
								ClockSubMeDataRun=window.setTimeout(getSubMeDataRunStat,100);
								//window.status="��ɣ�����";
								
								getSubMeDataRunStatList();
								window.frames["ContentMain"].frames["Right"].Form1.submit();
								//window.setTimeout(window.frames["ContentMain"].frames["Right"].Form1.submit(),3000);
								window.setTimeout(ReturnState, 200); 
							}
							else
							{
								window.status="��������";
								//buttLastClicked=obj;
								buttLastClicked.style.border='#ccccff thin inset';
								var strUrl=window.frames["ContentMain"].frames["Right"].location.href;
								if(strUrl.substr(strUrl.length-1)=="#")
									strUrl=strUrl.substr(0,strUrl.length-1);
								//window.frames["ContentMain"].frames["Right"].location=strUrl;//Form1.reset();
								return;
							}
						}
						else
						{
							window.status="���ܱ��棬��ѡ�����ӻ��޸ĸ��Ʋ���";
							buttLastClicked=obj;
							return;
						}
						break;
					case 'BCancel':
						var strUrl=window.frames["ContentMain"].frames["Right"].location.href;
						if(strUrl.substr(strUrl.length-1)=="#")
							strUrl=strUrl.substr(0,strUrl.length-1);
						window.frames["ContentMain"].frames["Right"].location=strUrl;//Form1.reset();
						
						
						obj.style.border='#ccccff 0px outset';
						window.status="ȡ������������,�Ѿ����ز�ѯҳ�棡";
						break;
					
					default:
						break;
				}
				}
				else
				{
				    var strPositionSize=openPositionSize.value;
					switch(obj.id)
					{
						case 'BAdd':
						    //alert('���Ե�������:' + strPositionSize);
							openWinPositionSizeUrl('./Input/addOpenTable.aspx?btnid='+obj.id+'&TableEName='+hTableEName.value+'&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&DataXPath='+ document.all["hParentXpath"].value+'&AssessorXpath='+document.all["hParentXpath"].value,'_blank',strPositionSize);
							break;
						case 'BModify':
							openWinPositionSizeUrl('<%="http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.ApplicationPath %>/DBQuery/Input/addOpenTable.aspx?btnid='+obj.id+'&TableEName='+hTableEName.value+'&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&DataXPath='+ document.all["hParentXpath"].value+'&AssessorXpath='+document.all["hParentXpath"].value,'_blank',strPositionSize);
							//window.open('Input/addOpenTable.aspx?btnid='+obj.id+'&TableEName='+hTableEName.value+'&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&DataXPath='+ document.all["hParentXpath"].value+'&AssessorXpath='+document.all["hParentXpath"].value,'','width=' + window.screen.availWidth +',height='+window.screen.availHeight+',status=yes,scrollbars=yes,top=0,left=0');
							//�޸Ĺ�+'&AssessorXpath='+document.all["hParentXpath"].value��ԭ����û�е�
							break;
						case 'BCancel':
							obj.style.border='#ccccff 0px outset';
							window.status="ȡ������������,�Ѿ����ز�ѯҳ�棡";
							break;
							/*�Ȳ����Ǹ���
						case 'BCopy':
							window.open('Input/add.aspx?btnid='+obj.id+'&TableEName='+hTableEName.value+'&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&DataXPath='+ document.all["hParentXpath"].value,'','width=800,height=500,status=yes,scrollbars=yes');
							break;
							*/
						case 'BDelete':
							if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
							{
								obj.style.border='#ccccff 0px outset';
								buttLastClicked.style.border='#ccccff thin inset';
								
								window.status="����û����ɣ��������ڲ���ɾ��������";
								return;
							}
							//break;
							if(window.frames["ContentMain"].Form1.hiCalculator.value!=0)
							{
								var confirmDel=confirm("ȷ��Ҫɾ���˼�¼������Ҫ�Ǹñ��¼���ӱ�\n�ü�¼���ӱ�����Ҳ��ȫ��ɾ����");
								if(confirmDel)
								{
									window.frames["ContentMain"].frames["Right"].Form1.hAction.value=obj.id;
									window.frames["ContentMain"].frames["Right"].Form1.submit();
								}
							}
							else
								window.status='û��Ҫɾ���ļ�¼';
							obj.style.border='#ccccff 0px outset';
							
							break;
					
					}
					
					//alert('123');
				}
				

				buttLastClicked=obj;
				
				//Form1.acceptCharset="gb2312";
			}
			
			//ˢ�½���
			function ReturnState()
            {
//                window.frames["ContentMain"].frames["Right"].Form1.submit();
//                frmDataRun
                if ( window.frames["ContentMain"].frames["Right"].document.readyState == "complete"
                && window.frames["frmDataRun"].document.readyState == "complete") {
                    
                    var frmFromListWindow = window.frames["ContentMain"];
                    if (frmFromListWindow != null) {
                        frmFromListWindow.Form1.submit();
                        return;
                    }
                    else {
                        var frmFromMainButtons = window.frames["Right"];
                        if (frmFromMainButtons != null) {
                            //frmFromMainButtons.document.location.href = frmFromMainButtons.document.location.href;
                            var parentQuery = frmFromMainButtons.parent; //.parent.frames["Top"];
                            if (parentQuery != null) {
                                parentQuery.Form1.submit();
                                return;
                            }
                        }
                    }
                }
                window.setTimeout(ReturnState, 200); 
            }
			
			//����Ĭ��ֵ ���� boolReplace�Ƿ񽫵�ǰ��Ĭ��ֵ������ǰ��ֵ����ֵ����
			function SetDefaultValue(boolReplace)
			{
				SetDefaultValues(window.frames["ContentMain"].frames["Right"].Form1.childNodes,boolReplace)
			} 
			
			function SetDefaultValues(kids,boolReplace)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
						{
							if(boolReplace && (kids[i].value==""))//||kids[i].keyIndex=="Yes"
							{
								kids[i].value="";
								kids[i].value=kids[i].ColumnDefault;
								if (kids[i].keyIndex == "Yes") {
						            kids[i].value += "??";
						        }
							}
						}
					}
					if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
					{
						if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
						{
							if(boolReplace && (kids[i].value==""||kids[i].keyIndex=="Yes") ) //�޸������Ĭ��ֵ����boolReplace && (kids[i].value==""||kids[i].keyIndex=="Yes")
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
							if(boolReplace && (kids[i].options.length>0&&kids[i].options[kids[i].selectedIndex].text==""))
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
					//=================���iframe�еĶ���===============
					if(kids[i].nodeName.toUpperCase()=='IFRAME')
					{
						//kids[i].src="";
					}
					
					if(kids[i].hasChildNodes)
						SetDefaultValues(kids[i].childNodes,boolReplace);
				}
			}
			
			function SetDefaultValueForCopy()
			{
				SetDefaultValues(window.frames["ContentMain"].frames["Right"].Form1.childNodes,false)
			} 
			
			function SetDefaultValueForCopys(kids)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined"&&kids[i].keyIndex=="Yes")
						{
							kids[i].value="";
							kids[i].value=kids[i].ColumnDefault;
							//alert(kids[i].ColumnDefault);
						}
					}
					if(kids[i].hasChildNodes)
						SetDefaultValueForCopys(kids[i].childNodes);
				}
			}
			
			//��¼���������б�����������
			//����1���Ƿ�����ı����ֵ
			//����2���Ƿ��������ؼ���¼��
			function EnableElement(bClear,BKeyIndexEnable)
			{
				CollectWhereClause(window.frames["ContentMain"].frames["Right"].Form1.childNodes,bClear,BKeyIndexEnable);
			}
			
			//���еĿռ���Ϊ����
			function CollectWhereClause(kids,bClear,BKeyIndexEnable)
			{
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						//alert(kids[i].NoChange);
						if(kids[i].NoChange == "No") //ֻ���ֶ�
						{
							//alert(kids[i].title);
							if(kids[i].disabled==true)
								kids[i].disabled=false;
						}
						//if(bClear&&(kids[i].type.toUpperCase()=='TEXT'))//�ش����⣬��������||kids[i].type.toUpperCase()=='HIDDEN'))
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
						if(kids[i].NoChange == "No") //ֻ���ֶ�
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
						if(kids[i].NoChange == "No") //ֻ���ֶ�
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
						if(kids[i].NoChange == "No") //ֻ���ֶ�
						{
							if(kids[i].disabled)
								kids[i].disabled=false;
						}
					}
					if(kids[i].hasChildNodes)
						CollectWhereClause(kids[i].childNodes,bClear,BKeyIndexEnable);
				}
			}
			
			function ClearElement(ColumnType)
			{
				ClearElementLoop(window.frames["ContentMain"].frames["Right"].Form1.childNodes,ColumnType);
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
			function ConfirmNotAllowNull (kids)
			{
				
				for(var i=0;i<kids.length;i++)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
						switch(kids[i].type.toUpperCase())
						{
							case "TEXT":
							    //debugger;
								if(kids[i].value==""&&kids[i].AllowNull=="No")//kids[i].NoChange=="No"&&
								{
									kids[i].select();
									kids[i].focus();
									kids[i].style.backgroundColor='Coral';
									window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
									return true;
								}
								break;
							case "RADIO":
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
				                        window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].value + "...";
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
				                        window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].value + "...";
				                        return;
				                    }
				                }
//								if(kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
//								{
//									kids[i].focus();
//									kids[i].style.backgroundColor='Coral';
//									window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
//									return;
//								}
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
							window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
							return;
						}
					}
					
					//=================TextArea�ڵ�=================
					if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
					{
						if(kids[i].value==""&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
						{
							kids[i].select();
							kids[i].focus();
							kids[i].style.backgroundColor='Coral';
							window.frames["ContentMain"].frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
							return;
						}
					}
					//-------------------End------------------------
					if(kids[i].hasChildNodes)
						ConfirmNotAllowNull(kids[i].childNodes);
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
							kids[i].value = kids[i].value.replace(/[\t]/g, '');
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
				CollectDataRunList(myForm.document.all['TableData'].childNodes);
				myForm.hQueryCollection.value=hDataRun;
				//alert(hDataRun);
				return hDataRun;
				//return false;
			}
		
		
			//�ռ��ӱ�����
			function CollectDataRunListSubMe(kids)
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
									var checkBoxList=kids[i].parentNode.getElementsByTagName("INPUT");//[kids[i].name];
									
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
						CollectDataRunListSubMe(kids[i].childNodes);
				}
			}
			function ExportClick()
			{
				//alert(document.all["hParentXpath"].value);
				//alert('<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>');
				//window.open('Analysis/InputExport.aspx?<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&StrXPath='+ document.all["hParentXpath"].value, '_blank', 'width=320,height=280');
				
				var strPaths = window.frames["ContentMain"].document.all["hQueryCollection"].value;
				strPaths = strPaths.split("\n");
				var strCondition = "";
				for(var i=0;i<strPaths.length;i++)
				{
					strCondition += "/*|*/" + strPaths[i];
				}
					
				window.open('Analysis/PortExport.aspx?<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&StrXPath='
				+ document.all["hParentXpath"].value+'&StrXPaths=' + strCondition + "&TableEName=" + hTableEName.value, '_blank', 'width=320,height=280');
				
				
				
				//window.open('Analysis/PortInput.aspx?<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&StrXPath='+ document.all["hParentXpath"].value, '_blank', 'width=320,height=280');
			}
			function ImportClick()
			{
				var topPosition = (window.screen.availHeight - 500)/2;
				var leftPosition = (window.screen.availWidth - 500)/2;
				var myPosition = "top=" + topPosition + ",left=" + leftPosition;
				window.open('Analysis/PortInput.aspx?<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&StrXPath='+ document.all["hParentXpath"].value, '_blank', 'width=320,height=280');
				//window.open('ExcelPage/ImportPort.aspx?<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>', '', 'width=500px,height=500px,' + myPosition);
			}
			
		function PublishNews()
			{
				var topPosition = (window.screen.availHeight - 500)/2;
				var leftPosition = (window.screen.availWidth - 500)/2;
				var myPosition = "top=" + topPosition + ",left=" + leftPosition;
				window.open('Analysis/PublishNews.aspx?<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&StrXPath='+ document.all["hParentXpath"].value, '_blank', 'width=320,height=280');
				//window.open('ExcelPage/ImportPort.aspx?<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>', '', 'width=500px,height=500px,' + myPosition);
			}
			
			var parentSet="";
			var topSet="";
			var mainSet="";
			function ZoomAll(obj)
			{
				//try
				//{//�Ŵ�
				var frmTop=top.fset;
				if(frmTop==null)
				    return;
				if(window.parent==null||window.parent.name=="")
					return;
					if(obj.src.indexOf("image/cursors/ico_zoomin.gif")>0)
					{
						//alert("�Ŵ�");
						obj.src="image/cursors/ico_zoomall.gif";
						
						var frm=parent.fset;
						parentSet=frm.rows;
						frm.rows="0,*";
						
						
						topSet=frmTop.cols;
						frmTop.cols="0,0,*";
						
						var frmMain=top.fsetMain;
						mainSet=frmMain.rows;
						frmMain.rows="0,*";
					}
					else//��ԭ
					{   
						//alert("��ԭ");
						obj.src="image/cursors/ico_zoomin.gif";
						
						var frm=parent.fset;
							
						if(parentSet!="")
						{
							frm.rows=parentSet;
						}
						
							
						if(topSet!=""&&topSet!="0,0,*")
						{
							frmTop.cols=topSet;
						}
						else
							frmTop.cols="209,0,*";
						
						var frmMain=top.fsetMain;
						if(mainSet!=""&&mainSet!="0,*")
						{
							frmMain.rows=mainSet;
						}
						else
							frmMain.rows="100,*";
					}
				//}
				//catch(e)
				//{
				
				//}
			}
			function loadButtons(strButtons)
			{
			    
				var Regex=/\.\.\//g;
				strButtons=strButtons.replace(Regex,"");
				
				//alert(strButtons);
				var AllButtons=strButtons.split("|");
				//alert(AllButtons.length);
				
				if(AllButtons.length==1)
				{
					
					//var strButtons="../image/middle/batch.jpg|../image/middle/add.jpg|../image/middle/delete.jpg|../image/middle/copy.jpg|../image/middle/gai.jpg|../image/middle/save.jpg|../image/middle/cancel.jpg|../image/middle/export.jpg";
					var strButtons="|../image/middle/add.jpg|../image/middle/delete.jpg|../image/middle/copy.jpg|../image/middle/gai.jpg|../image/middle/save.jpg|../image/middle/cancel.jpg|../image/middle/export.jpg";
					strButtons=strButtons.replace(Regex,"");
					//alert(strButtons);
					AllButtons=strButtons.split("|");
				}
				
//				for(var iButton=0;iButton<AllButtons.length;iButton++)
//				{
//				    ButVPath=AllButtons[iButton];
//                    if (ButVPath.indexOf("://") > 0 && ButVPath.length>8 && ButVPath.indexOf("/",8)>0)
//                    {
//                        AllButtons[iButton] = ButVPath.substr(ButVPath.indexOf("/", 8));
//                    }
//                    //alert( ButVPath.indexOf("/",8)>0);
//				}
					var buttonLocation=document.all["hButtonsTableLocation"].value;
					var buttonLocationVisible="none";
					if(buttonLocation!="in"){
					    buttonLocationVisible="";
					}
					if(AllButtons[0]!="")
					{
						document.all["BBatch"].src=AllButtons[0];
						document.all["BBatch"].style.display=buttonLocationVisible;
					}
					else
						document.all["BBatch"].style.display="none";
					
					if(AllButtons[1]!="")
					{
						document.all["BAdd"].src=AllButtons[1];
						document.all["BAdd"].style.display=buttonLocationVisible;
					}
					else
						document.all["BAdd"].style.display="none";
					
					if(AllButtons[2]!="")
					{
						document.all["BDelete"].src=AllButtons[2];
						document.all["BDelete"].style.display=buttonLocationVisible;
					}
					else
						document.all["BDelete"].style.display="none";
					
					if(AllButtons[3]!="")
					{
						document.all["BCopy"].src=AllButtons[3];
						document.all["BCopy"].style.display=buttonLocationVisible;
					}
					else
						document.all["BCopy"].style.display="none";
					
					if(AllButtons[4]!="")
					{
						document.all["BModify"].src=AllButtons[4];
						document.all["BModify"].style.display=buttonLocationVisible;
					}
					else
						document.all["BModify"].style.display="none";
					
					if(AllButtons[5]!="")
					{
						document.all["BSave"].src=AllButtons[5];
						document.all["BSave"].style.display=buttonLocationVisible;
					}
					else
						document.all["BSave"].style.display="none";
					
					if(AllButtons[6]!="")
					{
						//alert(AllButtons[6]);
						document.all["BCancel"].src=AllButtons[6];
						document.all["BCancel"].style.display=buttonLocationVisible;
					}
					else
						document.all["BCancel"].style.display="none";
					
					if(AllButtons[7]!="")
					{
						
						document.all["BExport"].src=AllButtons[7];
						document.all["BExport"].style.display="";
						//document.all["ButtonListAll"].style.display="";
					}
					else
					{
						document.all["BExport"].style.display="none";
						var ButtonsBatchVisible=document.all["hButtonsBatchVisible"].value;
						if(buttonLocation=="in" && ButtonsBatchVisible=="none")
						{
						    document.all["ButtonListAll"].style.display="none";
						}
						//����ť���б�Ҳû��������ťʱ
					}
						
					//==========Admin����ʱ,AllButtons������8�� �����˵�����9
					if(AllButtons[8] != void(0))
					{
						
						if(AllButtons[8] != "")
						{
							document.all["BImport"].src=AllButtons[8];
							document.all["BImport"].style.display="";
						}
						else
							document.all["BImport"].style.display = "none";
					}
					
					//���ذ�ť
					//if(strButtons=="|||||||")
					//	document.all["ButtonListAll"].style.display="none";
			}
			
			//ʱ���첽�������
			var iStat=0;
			var ClockDataRun;//����ʱ��
			var ClockSubMeDataRun;
			var AsycMainDataOk=false;//
			function getDataRunStat()
			{
				iStat ++;
				window.status +=window.frames["frmDataRun"].document.readyState + iStat;
				//window.frames["frmDataRun"].document.readyState;
				oToolTip.firstChild.lastChild.previousSibling.innerHTML = "�ռ�����...";
				if(window.frames["frmDataRun"].document!=null&&window.frames["frmDataRun"].document.readyState=="complete"&&iStat>2)
				{
					for(var iFrame=0;iFrame<window.frames.length;iFrame++)
					{
						//window.frames(iFrame).document.body.style.cursor="default";
					}
					window.document.body.style.cursor="default";
					window.clearTimeout(ClockDataRun);
					//alert(window.status);
					iStat=0;
					oToolTip.style.display="";
					//window.detachEvent("onclick",forbidClick);
					oToolTip.releaseCapture();
					window.document.body.style.zoom="100%";
					AsycMainDataOk=true;
					
				}
				else
					ClockDataRun=window.setTimeout(getDataRunStat,500);
			}
			
			function showoToolTip()
			{
				//alert(screen.height/2);
				window.document.body.style.cursor="wait";
				oToolTip.setCapture(true);
				window.document.body.style.cursor="wait";
				window.document.body.style.zoom="100%";
				//for(var iFrame=0;iFrame<window.frames.length;iFrame++)
				//window.attachEvent("onclick",forbidClick);
				oToolTip.style.top=window.document.body.clientHeight/3;
				oToolTip.style.left=window.document.body.clientWidth/3;
				oToolTip.style.display="";
				oToolTip.firstChild.lastChild.previousSibling.innerHTML="��ʼ";
			}
			
			//�ӱ���ֲ���
			var iSubMeCount;
			iSubMeCount=0;
			function getSubMeDataRunStat()
			{
				iSubMeCount++
				window.status +=iSubMeCount;
				if(!AsycMainDataOk)
					ClockSubMeDataRun=window.setTimeout(getSubMeDataRunStat,100);
				else
				{
					ClockSubMeDataRun=window.setTimeout(getSubMeDataRunStatList,100);
					//window.clearTimeout(ClockSubMeDataRun);
				}
			}
			
			var ClockSubMeDataRow;
			var iTimeOutInterval=500;
			
			var strXmlSubMe="";//<Tables/>
			function getSubMeDataRunStatList()
			{
//			    if(window.frames["ContentMain"].document!=null
//					&&window.frames["ContentMain"].frames["Right"]!=null
//					&&window.frames["ContentMain"].frames["Right"].document!=null
//					&&window.frames["ContentMain"].frames["Right"].document.readyState=="complete")
//				;
//				else
//				{
//				    closeToolTip();
//				    return;
//				}
				//oToolTip.setCapture(true);
				oToolTip.setCapture(true);
				var divSubMe=window.frames["ContentMain"].frames["Right"].document.all["divSubsMe"];
				//alert(divSubMe.childNodes.length);
				if(divSubMe!=null
					&&divSubMe.childNodes!=null
					&&divSubMe.childNodes.length>0)
				{
					//�����ӱ�
					//debugger;
					var iTimeOutMillsencond;
					iTimeOutMillsencond=100;
					
					var iTimeInterval=500;
					var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
					var root;
					var newElem;
					//xmlDoc.async = false;
					//xmlDoc.resolveExternals = false;
					xmlDoc.loadXML("<?xml version='1.0' encoding='GB2312'?><SubMes/>");
					root = xmlDoc.documentElement;
					
						
					for(var iSubMeTable=0;iSubMeTable<divSubMe.childNodes.length;iSubMeTable++)
					{
						var thisSubMeEName=divSubMe.childNodes[iSubMeTable].firstChild.firstChild.firstChild.title;
						
						oToolTip.firstChild.lastChild.previousSibling.innerHTML = divSubMe.childNodes[iSubMeTable].firstChild.firstChild.firstChild.outerHTML;
						var thisTableSubMeTableData=divSubMe.childNodes[iSubMeTable].firstChild.lastChild.firstChild.firstChild;
						//alert(thisTableSubMeTableData.outerHTML);
						//return;
						//�����ӱ�����ݲ�������
						var thisTableSubMeData="";
						
						newElem = xmlDoc.createElement("SubMe");
					
						var AttrEName;
						AttrEName = xmlDoc.createAttribute("EName");
						AttrEName.value = thisSubMeEName;
						
						newElem.setAttributeNode(AttrEName);

						root.appendChild(newElem);
						

						for(var iSubMeRows=1;iSubMeRows<thisTableSubMeTableData.rows.length;iSubMeRows++)
						{
							
							var boolClose=false;
							
							var thisSubMeDataRow=thisTableSubMeTableData.rows[iSubMeRows];
							//thisSubMeDataRow.lastChild.firstChild.style
							var strAction="";
							if(thisSubMeDataRow.lastChild.firstChild!=null)
								strAction=thisSubMeDataRow.lastChild.firstChild.innerHTML;
							//alert(strAction);
							if(strAction!=""&&strAction!="&nbsp;"&&strAction!="ɾ��")
							{
								iTimeOutMillsencond =iTimeOutMillsencond + iTimeOutInterval
								ClockSubMeDataRow=window.setTimeout("RunSubMeDataRow('"+thisTableSubMeTableData.id+"',"+iSubMeRows+","+boolClose+")",iTimeOutMillsencond);
								//Table,tr,td ..s.s.s.s.
								hDataRun="";
								CollectDataRunListSubMe(thisSubMeDataRow.cells);
								//alert(hDataRun);
								
								newEleTR = xmlDoc.createElement("tr");
					
								var AttrEName;
								AttrEName = xmlDoc.createAttribute("RunAction");
								AttrEName.value = strAction;
								
								newEleTR.setAttributeNode(AttrEName);
								if(strAction=="ȡ��ɾ��"||strAction=="ȡ���޸�")
								{
									AttrEName = xmlDoc.createAttribute("RunIndex");
									AttrEName.value = thisSubMeDataRow.cells[0].firstChild.title + "=" + thisSubMeDataRow.cells[0].firstChild.value;
									
									newEleTR.setAttributeNode(AttrEName);
								}
								
								newEleTR.text = hDataRun;
								
								newElem.appendChild(newEleTR);
							}
							else
							{
								continue;
							}
						}
						if(iSubMeTable==divSubMe.childNodes.length-1)
						{
							boolClose=true;
							iTimeOutMillsencond =iTimeOutMillsencond + iTimeOutInterval
							
							ClockSubMeDataRow=window.setTimeout("RunSubMeDataRow('"+thisTableSubMeTableData.id+"',"+iSubMeRows+","+boolClose+")",iTimeOutMillsencond);
						
						}
					}
					window.frames["ContentMain"].frames["Right"].document.all["hDataCollectionSubMes"].value=root.xml;
					//alert(window.frames["ContentMain"].frames["Right"].document.all["hDataCollectionSubMes"].value);
				}
				else
					closeToolTip();
				AsycMainDataOk=false;
				oToolTip.releaseCapture();
				
			}
			
			function RunSubMeDataRow(objSubMeDataTable,iDataTableRow,boolClose)
			{
				if(window.frames["frmDataRun"].document!=null&&window.frames["frmDataRun"].document.readyState=="complete")
				{
					if(boolClose)
					{
						closeToolTip();
					}
					else
					{
						var objDataTableSubMe=window.frames["ContentMain"].frames["Right"].document.getElementById(objSubMeDataTable);
						var objDataTableRow=objDataTableSubMe.rows[iDataTableRow];
						var strAction="";
						strAction=objDataTableRow.lastChild.innerHTML;
						oToolTip.firstChild.lastChild.innerHTML ="" + objDataTableRow.rowIndex + ":" +  objDataTableRow.lastChild.innerHTML;
						//window.clearTimeout(ClockSubMeDataRow);
						//window.frames["frmDataRun"].Form1.submit();
					}
				}
				else
					window.setTimeout("RunSubMeDataRow('"+objSubMeDataTable.id+"',"+iDataTableRow+","+boolClose+")",99)
			}
			
			function closeToolTip()
			{
				oToolTip.style.display="none";
				oToolTip.firstChild.lastChild.innerHTML ="";
				oToolTip.firstChild.lastChild.previousSibling.innerHTML = "���";
				//window.frames["ContentMain"].location.href=window.frames["ContentMain"].location.href;
			}
			function showTitle()
			{
			  if(parent!=null&&parent.name!="MainList"&&"<%=Request.QueryString["showTitle"] %>"=="no")
				 Table1.rows[0].style.display="none";
			}
			
			//��������(�������޸ģ���)
			function BatchOpen(sUrl,sAct)
			{
			    var hPageBegins=1;
			    var hPageSize=15;
			    var hQueryCollection='';
			    if(window.frames["ContentMain"].document==null
					||window.frames["ContentMain"].document.readyState!="complete")
				{
					alert("�����������,���Ժ�");
					return false;
				}
				if(window.frames["ContentMain"].document.all["hPageBegins"]!=null)
				    hPageBegins=window.frames["ContentMain"].document.all["hPageBegins"].value;
				if(window.frames["ContentMain"].document.all["hPageSize"]!=null)
				    hPageSize=window.frames["ContentMain"].document.all["hPageSize"].value;
				if(window.frames["ContentMain"].document.all["hQueryCollection"]!=null)
				    hQueryCollection=window.frames["ContentMain"].document.all["hQueryCollection"].value;
				
				strPaths = hQueryCollection.split("\n");
				var strCondition = "";
				for(var i=0;i<strPaths.length;i++)
				{
					strCondition += "/*|*/" + strPaths[i];
				}
				//alert(strCondition== "/*|*/");
				if((strCondition=="" || strCondition== "/*|*/") && "<%=Request.QueryString["QueryRules"] %>"!="")
				{
				    if(strCondition== "/*|*/")
				        strCondition="";
				    strCondition="/*|*/" + "<%=Request.QueryString["QueryRules"] %>";
				}
				    
				var winS = '��,��,90%,80%';
				sUrl +='xCode=�й���&TableEName='+hTableEName.value
				    +'&hPageBegins='+hPageBegins+'&hPageSize='+hPageSize
				    +'&btnid=BModify&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>&StrXPath='
				    + document.all["hParentXpath"].value
				    + '&DataXPath=' + strCondition;
				openWinPositionSizeUrl(sUrl,'_blank',winS);
			}
		</script>
		
		
	</HEAD>
	<body class="daohang1Body" onload="Javascript:showTitle()">
		<%if(tableNodeList==null||tableNodeList.Count==0)
		{
			%>
				<script language="javascript">
					var frm=parent.fset;
					if(frm.rows!="60,*")
					{
						frm.rows=frm.rows.replace("0,*","60,*");
					}
					else
					{
						frm.rows=frm.rows.replace("60,*","0,*");
					}
					
				</script>
			<%
			Response.Write("û���������");
			Response.End();
			
		}
		%>
		<%//Response.Write("<br>ʱ�䣺" + DateTime.Now + ":" + DateTime.Now.Millisecond);%>
		<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%"
			border="0">
			<tr height="20" style="DISPLAY:<%if(tableNodeList[0].Attributes.GetNamedItem("UserNavigator")!=null&&tableNodeList[0].Attributes.GetNamedItem("UserNavigator").InnerXml=="Yes") Response.Write("None");%>">
				<td width="10%" nowrap style="DISPLAY:none"><a href="../main/index.aspx" target="_top"><IMG id="zoomWholeDesctop" height=23 width=23 SRC="../images/icons/0078_b.gif" border="0"> ����</a></td>
				<td style="HEIGHT: 20px;width:75%" align="left">
					<DIV align="left">
						<TABLE class="daohang1" id="TableAllTable" height="25" cellSpacing="2" cellPadding="0" align="center"
							 border="0" width="20%">
							<TR>
								<%
								string isOpen;
								string PositionSize="��,��,80%,80%";
								XmlNode xn;
								foreach(XmlNode myNode in tableNodeList)
								{
								bool Batched=false;
								string BatchButtons="";
								if(myNode.Attributes.GetNamedItem("Batch")!=null)
								{
									Batched=true;
									BatchButtons=myNode.Attributes.GetNamedItem("ButtonSetting").InnerXml;
								}
								string thisTableCName=RetrieveTableName(myNode);
								string thisTableEName=RetrieveTableEName(myNode);
								
								isOpen="No";
								xn = myNode.Attributes.GetNamedItem("Open");    //ȡ���ڵ�ĵ������ԣ�����ʱisOpen="Yes",����isOpen="No"
								if(xn!=null)
								{
									isOpen=xn.Value;                                       
								}
								
								string DisplayNav="";
								xn = myNode.Attributes.GetNamedItem("Open");    //ȡ���ڵ�ĵ������ԣ�����ʱisOpen="Yes",����isOpen="No"
								if(xn!=null)
								{
									isOpen=xn.Value;                                       
								}
								
								xn = myNode.Attributes.GetNamedItem("PositionSize");    //ȡ���ڵ�Ľ���λ�����С������,��,80%,80%
								if(xn!=null)
								{
									PositionSize=xn.Value;                                       
								}
								
								
								
								string strTableButtons="";
								XmlNode TableButton=myNode.Attributes.GetNamedItem("Buttons");
								if(TableButton!=null)
									strTableButtons=TableButton.InnerXml;
								bool grandChildren=(thisTableCName.IndexOf("/")==thisTableCName.LastIndexOf("/")?false:true);
								%>
								

								<TD nowrap id="TD<%=thisTableEName%>" vAlign="middle"  align="center" width="97px" style="DISPLAY:">
									<DIV id="DIV<%=thisTableEName%>" align="center" style="display:<%if(grandChildren){%>none<%}%>"><A class="daohang1Font" href="#" 
									onclick="LocateTable('<%=thisTableEName%>',this,<%=Batched.ToString().ToLower()%>,'<%=BatchButtons%>','<%=strTableButtons.Replace("../","")%>','<%=isOpen%>')" 
									title="<%=thisTableEName%>">&nbsp;&nbsp;
									<%if(grandChildren){%>_<%}%><%=myNode.Attributes.GetNamedItem("TableCName").InnerXml%></A></DIV>
									
								</TD>
						
							
							   <% 
							   if(ModulesNavigateNodeList!=null  && linkConfig == "Yes")
							   {
							    foreach(XmlNode modulXNode in ModulesNavigateNodeList)
							    {
							    string linkName = modulXNode.Attributes["Text"].Value;
							    string linkUrl = modulXNode.Attributes["NavigateUrl"].Value;
							    string[] linkUrlList = linkUrl.Split('=');
							    string modulID = linkUrlList[1];
							    
							   %>
							   <script language="javascript">
							   
							       function openModulesNavigate(ModulName,ModulID)
							       {
							          var openWin = window.frames["ContentMain"].Form1.openWinUrl.value;
							          var openWin1 = "TableCName="+ModulName+"&TableEName=101&db=rstDB&name="+ModulName+"&RBACModuleID="+ModulID+"&"+openWin;
							   
							          window.open('Input/addOpenTable.aspx?btnid=BModify&'+openWin1,'_blank','width = 800px;height=600px');
							          return false;
							          
							       }
							   </script>
								<TD nowrap id="LINK<%=linkName%>"   align="center">
							   <div id="LINKDIV<%=linkName%>" align=center  >
									<a class="daohang1Font" href="#" onclick="javascript:openModulesNavigate('<%=linkName%>',<%=modulID%>)" > <%=linkName%> </a>
							   </div>
							</td>
							<%
							 }
							} %>
							
							
								<%}%>
							</TR>
						
						</TABLE>
					</DIV>
				</td>
				<td width="2%" valign="bottom" nowrap>
					<%if(xhnode!=null)
					{
						XmlNode xn_able = xhnode.Attributes.GetNamedItem("enable");
						if(xn_able!=null&&xn_able.Value=="Yes")
						{
							string strurl="",strtarget="",strPositionAndSize="",strpara="";
							
							xn_able = xhnode.Attributes.GetNamedItem("para");
							if(xn_able==null||xn_able.Value=="")
							{
								strurl="#";
							}
							else
							{
								strurl="../News/Browse/homepage.aspx?id="+xn_able.Value;
							}

                            xn_able = xhnode.Attributes.GetNamedItem("PositionSize");
							if(xn_able!=null)
							{
                                strPositionAndSize = xn_able.Value;
							}
                            if (strPositionAndSize == "")
                                strPositionAndSize = "��,��,80%,80%";
							
							xn_able = xhnode.Attributes.GetNamedItem("target");
							if(xn_able!=null&&xn_able.Value=="_blank"&&strurl.Length>1)
							{
							%>

							<a onclick="openWinPositionSizeUrl('<%=strurl%>','_blank','<%=strPositionAndSize%>')" >
							<%}else{%>
							<a href="<%=strurl%>">
							<%}%>
					
					
					<IMG src="images/icons/0067_b.gif" border="0" style="cursor:hand;"></a>
					<%	}
					}%>
				
				</td>
				<td width="1%" nowrap><IMG id="zoomWhole" height=23 width=23 src="image/cursors/ico_zoomin.gif" style="cursor:hand;"
				 onmousemove="this.style.border='#ccccff 0px outset'" 
				 onmouseout="this.style.border='#ccccff 0px outset'" 
				 onclick="javascript:ZoomAll(this)"></td>
				<script language=javascript>
				 	<%if(Request.QueryString["WindowSize"]!=null&&Request.QueryString["WindowSize"].ToString().ToUpper()=="MAX"){%>
						ZoomAll(document.all["zoomWhole"]);
						//document.all["zoomWhole"].click();
						//alert(document.all["zoomWhole"].src);
					<%}%>
				</script>
			</tr>
			<tr>
				<td class="spLine" height="12" colspan="4"></td>
			</tr>
			<TR>
				<TD colspan=4 style="WIDTH: 100%; HEIGHT: 0px"><iframe id="Top" name="Top" 
					src="Input/QueryTop.aspx?TableCName=<%=tableNodeList[0].Attributes.GetNamedItem("TableCName").InnerXml%>&TableEName=<%=tableNodeList[0].Attributes.GetNamedItem("EName").InnerXml%>&<%=Request.ServerVariables["Query_String"]%>" 
					frameBorder="0" width="100%" scrolling="<%if(Request.QueryString["DataOutQuery"]==null)Response.Write("auto");else Response.Write("no");%>"
						height="50px"></iframe>
				</TD>
			</TR>
			<tr>
				<td class="spLine" height="12" colspan=4></td>
			</tr>
			<TR>
				<TD colspan=4 style="WIDTH: 100%" vAlign="top"><iframe id="ContentMain" name="ContentMain" 
					src="Input/InputOneTable.aspx?TableCName=<%=tableNodeList[0].Attributes.GetNamedItem("TableCName").InnerXml%>&TableEName=<%=tableNodeList[0].Attributes.GetNamedItem("EName").InnerXml%>&<%=Request.ServerVariables["Query_String"]%>" 
					frameBorder="0" width="100%"
						scrolling="<%if(Request.QueryString["DataOutQuery"]==null)Response.Write("auto");else Response.Write("no");%>" height="100%"></iframe>
				</TD>
			</TR>
			<%if (tableNodeList[0].Attributes.GetNamedItem("BatchModify") != null)
             {
                 BatchModify = tableNodeList[0].Attributes.GetNamedItem("BatchModify").Value;
             }
            if (tableNodeList[0].Attributes.GetNamedItem("BatchPrint") != null)
             {
                 BatchPrint = tableNodeList[0].Attributes.GetNamedItem("BatchPrint").Value;
             }	    
                string buttonsTableVisible = "";
                string buttonsBatchesVisible = "";
                string buttonsTableLocation = "bot";	    	    
                if ((tableNodeList[0].Attributes.GetNamedItem("ButtonsLocation") != null && tableNodeList[0].Attributes.GetNamedItem("ButtonsLocation").Value == "in")
                    )//&& (tableNodeList[0].Attributes.GetNamedItem("Buttons") != null && tableNodeList[0].Attributes.GetNamedItem("Buttons").Value == "||||||||")
                {//Response.Write("none");
                    buttonsTableVisible = "none";
                    buttonsTableLocation = "in";  
                }
                //buttonsTableVisible = "";  	    
                if (BatchModify.ToLower() != "true"
                    && BatchPrint.ToLower() != "true")
                {
                    buttonsBatchesVisible = "none";
                }
			    %>
			<tr height="0px"  id="ButtonListAll">
				<td colspan=3 height="35" align="center" nowrap valign="bottom" ><%//=tableNodeList[0].Attributes.GetNamedItem("Buttons").Value %>
					<img id="BBatch" src="image/middle/BBatch.jpg" style="DISPLAY:<%if(tableNodeList[0].Attributes.GetNamedItem("Batch")==null) Response.Write("none");%>" width="79" height="24" border="0" 
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="��������">
					<img id="BAdd" src="image/middle/add.jpg"  style="DISPLAY:<%if(tableNodeList[0].Attributes.GetNamedItem("Batch")!=null&&tableNodeList[0].Attributes.GetNamedItem("ButtonSetting ").InnerXml.IndexOf("BAdd")==-1) Response.Write("none");%>" width="79" height="24" border="0" 
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="���ӣ�¼�룬�¼�¼">
					<img id="BDelete" src="image/middle/delete.jpg"  style="DISPLAY:<%if(tableNodeList[0].Attributes.GetNamedItem("Batch")!=null&&tableNodeList[0].Attributes.GetNamedItem("ButtonSetting ").InnerXml.IndexOf("BDelete")==-1) Response.Write("none");%>" width="79" height="24" border="0" 
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="ɾ��">
					<img id="BCopy" src="image/middle/copy.jpg"  style="DISPLAY:<%if(tableNodeList[0].Attributes.GetNamedItem("Batch")!=null&&tableNodeList[0].Attributes.GetNamedItem("ButtonSetting ").InnerXml.IndexOf("BCopy")==-1) Response.Write("none");%>" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="����">
					<img id="BModify" src="image/middle/gai.jpg" width="81" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="�޸�">
					<!--img id="BCollapse" src="image/middle/zhedie.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)"-->
					<img id="BSave" src="image/middle/save.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="�������">	
					<img id="BCancel" src="image/middle/cancel.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="ȡ������">
					<img id="BExport" src="image/middle/export.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="ExportClick()" title="������">
					<img id="BImport" src="image/import.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="ImportClick()" title="������" style="Display:none">
						
					<img id="BOut" src="image/publish.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="PublishNews()" title="����Ϊ����" style="display:none">	
				</td>
				<td  style="display:<%=buttonsBatchesVisible%>">
				    <table cellSpacing="0" cellPadding="0" border="0" id="table10" style="display:">
						<tr>
							<td style="display:none">
							<div align="center">
								<table style="BORDER-RIGHT: #808080 1px solid; BORDER-TOP: #808080 1px solid; BORDER-LEFT: #808080 1px solid; BORDER-BOTTOM: #808080 1px solid" cellSpacing="0" cellPadding="0" bgColor="#000000" border="0" id="table11">
									<tr>
										<td noWrap>
										<font face="Webdings" color="#ffff00">
										1</font>&nbsp;<a class="act" onclick="vopen('/sys/plist/?dtname=contact&amp;pn=&amp;edittype=1&amp;mcr=6601d487db66329a1bd7bf8c0cd6a709','plist',700,550);" href="https://x2.xtcrm.com/xcrm/customer/contact/#">�����½�����</a>&nbsp;</td>
									</tr>
								</table>
							</div>
							</td>
							<td>&nbsp;&nbsp;</td>
							<td style="Display:<%
			                    BatchModify = (BatchModify.ToLower()=="true")?"":"none";
                                Response.Write(BatchModify);
                               %>">
							<div align="center">
								<table style="BORDER-RIGHT: #808080 1px solid; BORDER-TOP: #808080 1px solid; BORDER-LEFT: #808080 1px solid; BORDER-BOTTOM: #808080 1px solid" cellSpacing="0" cellPadding="0" bgColor="#336699" border="0" id="table12">
									<tr>
										<td noWrap>
										<font face="Webdings" color="#ffff00">
										1</font>&nbsp;<a class="none" onclick="BatchOpen('InputBatch/BatchOpen.aspx?','BatchModify');" href="#">�����༭����</a>&nbsp;</td>
									</tr>
								</table>
							</div>
							</td>
							<td>&nbsp;&nbsp;</td>
							<td style="Display:<%
			                    BatchPrint = (BatchPrint.ToLower()=="true")?"":"none";
                                Response.Write(BatchPrint);
                               %>">
							<div align="center">
								<table style="BORDER-RIGHT: #808080 1px solid; BORDER-TOP: #808080 1px solid; BORDER-LEFT: #808080 1px solid; BORDER-BOTTOM: #808080 1px solid" cellSpacing="0" cellPadding="0" bgColor="#336699" border="0" id="table2">
									<tr>
										<td noWrap>
										<font face="Webdings" color="#ffff00">
										1</font>&nbsp;<a class="none" onclick="BatchOpen('Print/PrintPDFList.aspx?','BatchPrint');" href="#">������ӡ</a>&nbsp;</td>
									</tr>
								</table>
							</div>
							</td>
							<td style="display:none">��<font color="#cdcdcd">��*��</font></td>
						</tr>
					</table>
				</td>
			</tr>
		</TABLE>
		<input type="hidden" id="RelocateListRow" name="RelocateListRow" value="0" />
		<input type="hidden" id="hTableEName" name="hTableEName" value="<%=tableNodeList[0].Attributes.GetNamedItem("EName").InnerXml%>">
		<input type="hidden" id="hKeyValue" value="">
		<input type="hidden" id="hParentXpath" value="">
		<input type="hidden" id="hDoubleClick" value="">
		<input type="hidden" id="hButtonsTableLocation" value="<%=buttonsTableLocation %>">
		<input type="hidden" id="hButtonsBatchVisible" value="<%=buttonsBatchesVisible %>">
		<input type="hidden" id="hisOpen" name="hisOpen" value="<%
			xn=tableNodeList[0].Attributes.GetNamedItem("Open");
			if(xn!=null)
			{
				Response.Write("Yes");
			}
			else
			{
				Response.Write("No");
			}%>">
		<input type="hidden" id="openPositionSize" name="openPositionSize" value="<%=PositionSize%>">
		<iframe id="frmDataRun" name="frmDataRun" 
					src="" 
					frameBorder="0" width="100%"
						scrolling="auto" height="0"></iframe>
		<%//Response.Write("<br>ʱ�䣺" + DateTime.Now + ":" + DateTime.Now.Millisecond);%>
		<DIV id="oToolTip" style="DISPLAY: none;POSITION: absolute">
			<div style="BORDER-RIGHT: black 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 2px solid; PADDING-LEFT: 10px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=skyblue, EndColorStr=#FFFFFF); LEFT: 0px; PADDING-BOTTOM: 10px; FONT: 10pt tahoma; BORDER-LEFT: black 2px solid; WIDTH: 170px; PADDING-TOP: 10px; BORDER-BOTTOM: black 2px solid; TOP: 0px; HEIGHT: 120px">
				<b>���ڲ���:</b>
				<hr style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
					SIZE="1">
				<marquee  behavior=alternate>:::���Ժ�:::</marquee>
				<b></b>
				<div></div>
			</div>
		</DIV>
		<%if(tableNodeList[0].Attributes.GetNamedItem("Buttons")!=null){%>
			<script language="javascript">
			    loadButtons('<%=tableNodeList[0].Attributes.GetNamedItem("Buttons").InnerXml.Replace("../","")%>');
			</script>
		<%}%>
	</body>
</HTML>
<script language="javascript" event="onpropertychange" for="hDoubleClick">
	//alert(this.value);
	if(this.value!="")
	{
		FireQuery(document.all["BModify"]);
	}
	
	//else
	//	FireQuery(document.all["BCancel"]);
</script>
