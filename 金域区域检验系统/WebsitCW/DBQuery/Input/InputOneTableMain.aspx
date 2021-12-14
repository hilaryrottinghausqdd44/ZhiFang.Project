<%@ Import Namespace="System.Xml" %>
<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Input.InputOneTableMain" Codebehind="InputOneTableMain.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%//Response.Write("<br>ʱ�䣺" + DateTime.Now + ":" + DateTime.Now.Millisecond);%><HTML>
	<HEAD>
		<title>����¼��</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		
		<%if(cssFile.Trim()==""){%>
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<%}else{%>
		<LINK href="../<%=cssFile%>" type="text/css" rel="stylesheet">
		<%}%>
		<!--LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet"-->
		
		<script language="javascript" src="../js/dialog.js"></script>
		<!--script language="javascript" src="../../Includes/js/calendar.js"></script-->
		<!--#include file="../../Util/Calendar.js"-->
		<script language="javascript">
		function CheckOption(Name,getString)
		{
			if(document.all[Name].options.length==0)
				return;
			var StringArray=getString.split(",");
			for(var i=0; i<StringArray.length;i++)
			{
				for(var n=0;n<document.all[Name].options.length;n++)
				{
					if(StringArray[i]==document.all[Name].options[n].text)
						document.all[Name].options[n].selected=true;
				}
			}
		}
		function CollectDataRunList(kids)
		{
			for(var i=0;i<kids.length;i++)
			{
				if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
				{
					if(kids[i].value!="")
					hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].value;
				}
				if(kids[i].nodeName.toUpperCase()=='SELECT')
				{
					if(kids[i].options[kids[i].selectedIndex].text!="")
						hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].options[kids[i].selectedIndex].text;
				}
				if(kids[i].hasChildNodes)
					CollectDataRunList(kids[i].childNodes);
			}
		}
		var hDataRun="";
		function CollectDataRun()
		{
			//alert('a');
			
			Form1.action = "";
			hDataRun="";
			CollectDataRunList(Form1.TableData.childNodes);
			Form1.hQueryCollection.value=hDataRun;
			
			//alert(hDataRun);
			
			//Form1.submit(); onsubmit="alert('a');return false;"
			return true;
		}
		
		function window_onload()
		{
		    Form1.action = 'DataRun.aspx?<%=Request.ServerVariables["Query_String"]%>';		   
			var buttLastClicked=parent.parent.buttLastClicked;
			
			if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BDelete'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
			{
				buttLastClicked.style.border='#ccccff 0px outset';
				parent.parent.buttLastClicked=null;
			}

			if (parent != null && parent.document.getElementById("hLoaded") != null) {
			    parent.document.all["hLoaded"].value = "1";
			}
			if(true)
			{
			    modifyObj = parent.parent.document.getElementById("BModify");
				addObj = parent.parent.document.getElementById("BAdd");
				deleteObj = parent.parent.document.getElementById("BDelete");
				saveObj=parent.parent.document.getElementById("BSave");
				 if((modifyObj != void 0) && modifyObj.style.display == "none"
				    && (addObj != void 0) && addObj.style.display == "none"
				    && (deleteObj != void 0) && deleteObj.style.display == "none"
				    && (saveObj != void 0) && saveObj.style.display != "none"
															            )											    
															       
			    fireEdit();
			    
			}
			return true;
	
			//alert(parent.document.all["hLoaded"].value);
			//alert(window.parent.parent.document.getElementById("BAdd"));
		}
		
		function DownloadFile(strObj,thisObj)
		{
			if(document.all[strObj].value!="")
			{
				thisObj.href='DownLoadFile.aspx?File=' + document.all[strObj].value;
				return true;
			}
			alert('�ļ�δ�ϴ����Ѿ�ɾ��,�޷�����\r���ȱ����¼');
			return false;
		}
		function uploadFile(strObj,strUrl,strTarget,thisObj)
		{
			///��������״̬�����˳�
			if(Form1.hAction.value=="BBatch"||Form1.hAction.value=="BAdd"||Form1.hAction.value=="BModify"||Form1.hAction.value=="BCopy")
			{	
				if(!thisObj.disabled)
				{
					var r;
					r=window.showModalDialog('InputUploadFileDialog.aspx','','dialogWidth:398px;dialogHeight:158px;help:no;scroll:auto;status:no');
					if (r != '' && typeof(r) != 'undefined')
					{
						if(document.all[strObj].value!="")
							window.open('DeleteFile.aspx?File=' +document.all[strObj].value ,strTarget);
						document.all[strObj].value=r;
						thisObj.disabled=true;
					}
				}
				else
					alert('ֻ�����Բ����ϴ�������ǰӦ����');
			}
			
		}
		
		function DeleteFile(strObj,strUrl,strTarget)
		{
			if(Form1.hAction.value=="BModify")
			{	
				if(document.all[strObj].value!="")
				{
					window.open('DeleteFile.aspx?File=' +document.all[strObj].value ,strTarget);
					document.all[strObj].value="";
					//window.open(strUrl,strTarget);
				}
				else
					alert('�ļ��Ѿ�ɾ��,�뱣�浱ǰ����');
			}
			if(Form1.hAction.value=="BAdd"||Form1.hAction.value=="BCopy")
			{
				if(document.all[strObj].value!="")
				{
					window.open('DeleteFile.aspx?File=' +document.all[strObj].value ,strTarget);
					document.all[strObj].value="";
				}
				else
					alert('�ļ��Ѿ�ɾ��,�뱣�浱ǰ����');
			}
		}
		
		function EditNews(strObj,strDataBase,DefaultValue,editMode)
		{
			///��������״̬�����˳�
			if(Form1.hAction.value=="BBatch")
			{
				alert("�������Ӽ�¼����ʹ����ͬ����Ϣ�༭,\r������ɺ���Ե����༭ÿ����Ϣ");
				return;
			}
			if((Form1.hAction.value == "BAdd" || Form1.hAction.value == "BCopy") && document.all[strObj].style.backgroundColor!="skyblue")
			{
				document.all[strObj].value="";
				document.all[strObj].style.backgroundColor = 'skyblue';
			}
			if(Form1.hAction.value=="BModify"||Form1.hAction.value=="BAdd"||Form1.hAction.value=="BCopy")
			{	
				var r;
				if(strDataBase==null || strDataBase=="")
				    strDataBase=document.all[strObj].value;
				    
				var strEditMode='';
				if(editMode=='3')
				    strEditMode='&readonly=1';
				r=window.showModalDialog('InputEditNewsDialog.aspx?DefaultValue='+DefaultValue+'&FilePath=' + strDataBase + '&FileName=' + document.all[strObj].value + strEditMode,'','dialogWidth:588px;dialogHeight:468px;resizable:yes;scroll:auto;status:no');
				if (r != '' && typeof(r) != 'undefined')
				{
					document.all[strObj].value=r;
					if(document.frames["frm" + strObj]!=null)
						document.frames["frm" + strObj].location="inputBrowseNews.aspx?FilePath=<%=DataBase%>&FileName=" + r;
				}
				else if(r=="")
				{
					document.all[strObj].value='';
					if(document.frames["frm" + strObj]!=null)
						document.frames["frm" + strObj].location="about:blank";
				}
			}
		}
		function BrowseNews(strObj,strDataBase)
		{
			///��������״̬�����˳�
			if(Form1.hAction.value=="BAdd"||Form1.hAction.value=="BCopy"||Form1.hAction.value=="BBatch")
			{
				alert("��Ϣ����֮ǰ������Ԥ��\���ȱ༭��Ϣ��������Щ��¼");
				return
			}
			if(Form1.hAction.value=="BModify"||Form1.hAction.value=="Browse")
			{	
			    if(strDataBase==null || strDataBase=="")
				    strDataBase=document.all[strObj].value;
				window.open('inputBrowseNews.aspx?FilePath=' + strDataBase + "&FileName=" + document.all[strObj].value,'','width=500,height=600,scrollbars=1,resizable=yes,top=0,left=200');
			}
		}
		
		var bEdit;
		function fireEdit()
		{
		    var dataXPATHedit = "<%=Request.QueryString["DataXPath"] %>";
			if(dataXPATHedit!='')
			{
			    var d = new Date();
				//parent.parent.document.all["hDoubleClick"].value=d.getUTCMilliseconds();
				parent.parent.FireQuery(parent.parent.document.all["BModify"]);
				//alert(parent.parent.document.all["hDoubleClick"].value);
			}
		}
		
		//==================��֤��������================
		function IsValidateDigit(obj)
		{
			//var digit = obj.value;
			//if(isNaN(Number(digit)))
			//{
				//��������
				//obj.focus();
				//obj.select();
				//alert(digit + " ��������! ��������");
			//}
			
			if((event.keyCode >= 48) && (event.keyCode <= 57) || (event.keyCode ==46) || (event.keyCode==45) )
			{
				if(obj.value.indexOf(".")!=-1 && (event.keyCode ==46))
				{
					alert("��Ч������");
					return false;
				}
				if(event.keyCode==45 && obj.value!="")
				{
					alert("��Ч������");
					return false;
				}
				return true;
			}
			else
				alert('ֻ����������');
			return false;

		}
		//======================End====================
		
		//======================¼��ʱ�����ܵĴ������====================
		var gAllElementsValues="";
		function CollectInParaValues(strElementTitle)
		{
			if(strElementTitle=="")
				return "";
			gAllElementsValues="";
			varElementsArray=strElementTitle.split(",");
			for(var iE=0;iE<varElementsArray.length;iE++)
			{
				CollectInParaValuesLoop(varElementsArray[iE],document.all['TableData'].childNodes);
			}
			if(gAllElementsValues.length>0)
				gAllElementsValues=gAllElementsValues.substr(1);
			return gAllElementsValues;
		}
		function CollectInParaValuesLoop(elementTitle,kids)
		{
			for(var i=0;i<kids.length;i++)
			{
				if(kids[i].nodeName.toUpperCase()=="#TEXT")
					continue;
				if(kids[i].getAttribute('title')!=null&&kids[i].title==elementTitle)
				{
					if(kids[i].nodeName.toUpperCase()=='INPUT'&&kids[i].type.toUpperCase()=='TEXT')
					{
						gAllElementsValues +="," + elementTitle + "=" + kids[i].value;
					}
					if(kids[i].nodeName.toUpperCase()=='SELECT') 
					{
						if(kids[i].selectedIndex!=-1)
							gAllElementsValues +="," + elementTitle + "=" + kids[i].options[kids[i].selectedIndex].text;
					}
					if(kids[i].nodeName.toUpperCase()=='TEXTAREA')
					{
						gAllElementsValues +="," + elementTitle + "=" + kids[i].value;
					}
					//��������,��ѡ��Radio, Checkbox�ȵ�;
				}
				if(kids[i].hasChildNodes)
					CollectInParaValuesLoop(elementTitle,kids[i].childNodes);
			}
		}
		//======================¼��ʱ�����ܵĴ������====================
		
		//======================¼��ʱ�����ܵķ��ز���====================
		var objElement;
		function setOutParaValues(strReturnValues,strElementTitle,strDefaultElementTitle)
		{
			if(strElementTitle=="null")
				strElementTitle=strDefaultElementTitle;
			var setElements=strElementTitle.split(",");
			var setElementsValues=strReturnValues.split(",");
			if(setElements.length!=setElementsValues.length)
			{
				alert('���ؽ��ʱ�������صĲ���������ƥ�䣡����');
				return;
			}
			for(var iS=0;iS<setElements.length;iS++)
			{
				objElement=null;
				selectElementByTitle(setElements[iS],document.all['TableData'].childNodes);
				if(objElement==null)
					continue;
				if((objElement.nodeName.toUpperCase()=='INPUT'&&objElement.type.toUpperCase()=='TEXT')
					||objElement.nodeName.toUpperCase()=='TEXTAREA')
				{
					objElement.value=setElementsValues[iS];
				}
				if(objElement.nodeName.toUpperCase()=='SELECT')
				{
					for(var iOptions=0;iOptions<objElement.options.length;iOptions++)
					{
						if(objElement.options[iOptions].text==setElementsValues[iS])
						{
							objElement.options[iOptions].selected=true;
						}
					}
				}
				///��������,��ѡ��Radio, Checkbox�ȵ�;
			}
		}
		
		function selectElementByTitle(elementTitle,kids)
		{
			for(var i=0;i<kids.length;i++)
			{
				if(kids[i].nodeName.toUpperCase()=="#TEXT")
					continue;
				if(kids[i].getAttribute('title')!=null&&kids[i].title==elementTitle)
				{
					objElement =kids[i];
					return;
				}
				if(kids[i].hasChildNodes)
					selectElementByTitle(elementTitle,kids[i].childNodes);
			}
		}
		
		function ReCollectInParaValues(strModulPath,strParas)
		{
			var returnPath=strModulPath;
			var arrParas=strParas.split(",");
			for(var i=0;i<arrParas.length;i++)
			{
				var arrEachPara=arrParas[i].split("=");
				//alert(arrEachPara);
				if(arrEachPara.lengh<2)
					continue;
				if(returnPath.indexOf("=" + arrEachPara[0])>0)
					returnPath=returnPath.replace("=" + arrEachPara[0],"=" + arrEachPara[1]);
				
			}
			return returnPath;
		}
		
		//�ӱ����======================================================================================================
		//ɾ����¼
		function DeleteSubMe(objSubMe,boolDelete)
		{
			if(objSubMe.disabled==true)
				return;
			if(objSubMe.innerHTML=="ɾ��")
			{
				if(!boolDelete)
					return;
				objSubMe.innerHTML="ȡ��ɾ��";
				objSubMe.parentNode.parentNode.style.textDecoration='line-through';
			}
			else if(objSubMe.innerHTML=="ȡ��ɾ��")
			{
				objSubMe.innerHTML="ɾ��";
				objSubMe.parentNode.parentNode.style.textDecoration='none';
			}
			else if(objSubMe.innerHTML=="ȡ���޸�")
			{
				objSubMe.innerHTML="ɾ��";
			}
			
			else if(objSubMe.innerHTML=="ȡ������")
			{
				if(objSubMe.parentNode.parentNode.parentNode.parentNode.rows.length>2)
					objSubMe.parentNode.parentNode.parentNode.parentNode.deleteRow(objSubMe.parentNode.parentNode.rowIndex);//.innerHTML="ɾ��";
				else
					window.status="����ɾ��";
			}
		}
		
		function LocateSubMeEditorMode(objSubMeTD,boolEdit,boolButtons)
		{
		    if(boolButtons==null || boolButtons ==false)
		    {
		        return;
		    }
			if(objSubMeTD.parentNode.lastChild.firstChild.disabled==true)
				return;
				
			if(Form1.hAction.value=="BAdd")
				boolEdit=false;
			if(boolEdit)
			{   if(objSubMeTD.parentNode.lastChild.firstChild.innerHTML!="ȡ������")
				{
					objSubMeTD.parentNode.lastChild.firstChild.innerHTML="ȡ���޸�";
					objSubMeTD.parentNode.style.textDecoration='none';
				}
			}	
			else
				objSubMeTD.parentNode.lastChild.firstChild.innerHTML="ȡ������";
		}
		
		
		//��������----
		function AddNewSubMe(objSubMeAddNew,boolCopyFlag)
		{
			if(objSubMeAddNew.disabled==true)
				return;
			
			//alert(typeof(boolCopyFlag)=="undefined");
			if(typeof(boolCopyFlag)=="undefined")
			{
				//alert(objSubMeAddNew.parentNode.parentNode.nextSibling.firstChild.firstChild.firstChild.innerHTML);
				var objDataTable=objSubMeAddNew.parentNode.parentNode.nextSibling.firstChild.firstChild.firstChild;
				//var newTR = objDataTable.insertRow();
				var NewTr=objDataTable.lastChild.cloneNode(true);
				ClearCopyValue(NewTr.childNodes);
				NewTr.lastChild.firstChild.innerHTML="ȡ������";
				NewTr.style.textDecoration='none';
				objDataTable.appendChild(NewTr);
			}
			else //if(boolCopyFlag)
			{
				var objDataTable=objSubMeAddNew.parentNode.parentNode.parentNode;
				//var newTR = objDataTable.insertRow();
				var NewTr=objSubMeAddNew.parentNode.parentNode.cloneNode(true);
				window.parent.parent.SetDefaultValues(NewTr.childNodes,true);
				NewTr.lastChild.firstChild.innerHTML="ȡ������";
				NewTr.style.textDecoration='none';
				objDataTable.appendChild(NewTr);
			}
			//alert(objDataTable.rows.length);
		}
		
		function ClearCopyValue(NewTrChildren)
		{
			//var objTrSubMe=NewTr;
			for(var i=0;i<NewTrChildren.length;i++)
			{
				var objSubMe=NewTrChildren[i].firstChild
				if(objSubMe!=null)
				{
					window.parent.parent.CollectWhereClause(NewTrChildren[i].childNodes,true,true);
					window.parent.parent.SetDefaultValues(NewTrChildren[i].childNodes,true);
					
				}
			}
			//return objTrSubMe
		}
		
		function ReCorrectOperation(NewTrChildren)
		{
			
		}
		//�ӱ����======================================================================================================
		
		
		//���̲���
		function findNextFocus()
		{
		    
		    if (window.event.srcElement.tagName == "input" || window.event.srcElement.tagName == "INPUT")
		    {
		        
		        if(window.event.srcElement.type=='text'){
		            //alert(window.event.srcElement.objRunEvent==null);
		            if(window.event.srcElement.objRunEvent!=null)
		            {
		                //alert(event.keyCode);
		                //�س�Ҫ��Ҫ������ǰ���̣�����һ��-->??
		                return true;
		            }
		        }
		    }
				if ((window.event.altKey)&&
				((window.event.keyCode==37)|| //���� Alt+ ����� ��
				(window.event.keyCode==39))){ //���� Alt+ ����� ��

						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}

				if ((event.keyCode == 8) && 
				(event.srcElement.type != "text" && 
				event.srcElement.type != "textarea" && 
				event.srcElement.type != "password") || //�����˸�ɾ���� 
				(event.keyCode==116)|| //���� F5 ˢ�¼�
				(event.ctrlKey && event.keyCode==82)){ //Ctrl + R
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if ((event.ctrlKey)&&(event.keyCode==78)) //���� Ctrl+n
				{
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if ((event.shiftKey)&&(event.keyCode==121)) //���� shift+F10
				{
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if (window.event.srcElement.tagName == "A" && window.event.shiftKey) 
				{
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false; 
				}
				if ((window.event.altKey)&&(window.event.keyCode==115))
				{ //����Alt+F4
						event.keyCode = 0; 
						event.cancelBubble = true; 
						return false;
				}
			

			//if(event.srcElement.tagName == "TEXTAREA"
			//||event.srcElement.tagName == "SELECT")
			//alert(event.keyCode);
			var up,down,left,right; 
			up = 38; 
			down = 40; 
			left = 37; 
			right = 39; 
			enter =13;
			
			switch(event.keyCode) 
			{ 
				case left:{ 
					if(event.srcElement.tagName == "SELECT")
					{
						var objFocus=event.srcElement;
						
						if(objFocus.id!=null&&objFocus.id!="")
						{
							var thisID=objFocus.id;
							var allIds=document.all["hAllIds"].value;
							if(allIds.indexOf(thisID)>0)
							{
								//var arrayIDs=allIds.split(",");
								allIds=allIds.substr(0,allIds.indexOf(thisID)-1);
								
								var previousID="";
								while(document.getElementById(previousID)==null)
								{
									if(allIds.length<=1)
										break;
									previousID=allIds.substr(allIds.lastIndexOf(",")+1);
									//alert(allIds);
									
									var preObject=document.getElementById(previousID);
									if(preObject!=null)
									{
										preObject.focus();
										break;
									}
									allIds=allIds.substr(0,allIds.lastIndexOf(","));
									
								}
							}
						}
					}
					break; 
					} 
				case up:{ 
					if(event.srcElement.tagName == "SELECT"
					||event.srcElement.tagName == "TEXTAREA")
						return;
					var objFocus=event.srcElement;
					
					if(objFocus.id!=null&&objFocus.id!="")
					{
						var thisID=objFocus.id;
						var allIds=document.all["hAllIds"].value;
						if(allIds.indexOf(thisID)>0)
						{
							//var arrayIDs=allIds.split(",");
							allIds=allIds.substr(0,allIds.indexOf(thisID)-1);
							
							var previousID="";
							while(document.getElementById(previousID)==null)
							{
								if(allIds.length<=1)
									break;
								previousID=allIds.substr(allIds.lastIndexOf(",")+1);
								//alert(allIds);
								
								var preObject=document.getElementById(previousID);
								if(preObject!=null)
								{
									preObject.focus();
									break;
								}
								allIds=allIds.substr(0,allIds.lastIndexOf(","));
								
							}
						}
					}
					
					//while(objFocus.previousSibling==null)
					//{
					//	objFocus=objFocus.parentNode;
					//	objFocus.focus();
					//}
					
					break; 
					} 
				case right:{
					if(event.srcElement.tagName == "SELECT")
						event.keyCode=9;
					break; 
					} 
				case down:{ 
					if(event.srcElement.tagName != "SELECT"&&event.srcElement.tagName != "TEXTAREA")
						event.keyCode=9;
					break; 
					} 
				case enter:{ 
					if(event.srcElement.tagName != "TEXTAREA")
						event.keyCode=9;
					break; 
					} 
			} 
			
		}
		//======================¼��ʱ�����ܵķ��ز���====================
		
		var varOrgValue="";
		function SetV(objID,v) {
            //alert(objID);
            $(objID).value = v;
        }
        function $(s) {
            return document.getElementById ? document.getElementById(s) : document.all[s];
        }
        function keyPressDivLocal(objRun,OrgValue){
            if(parent==null)
                return;
            //showParentDiv(objRun);
             var key = window.event.keyCode;
            // alert('key=' + key);
            //window.parent.keyPressdiv(key, window.name,objRun.id,OrgValue);window.parent.
            keyPressdiv(key, window.name,objRun.id,OrgValue);
        }
        function showParentDiv(objRun)
        {
            if(parent==null)
                return;
            var key = window.event.keyCode;
															    
		    var tt=objRun;
		    var ttop =tt.offsetTop;//tt.offsetTop;
		    ttop +=tt.offsetHeight;
		    var tleft =tt.offsetLeft;
		    while (tt = tt.offsetParent){ttop+=tt.offsetTop; tleft+=tt.offsetLeft;}
		    
		    //tWindow=window
		    //while (tWindow.parent!=null){ttop+=tWindow.offsetTop; tleft+=tWindow.offsetLeft;tWindow=tWindow.parent;}
		    
		    
//		    alert("window.offsetTop=" + parent.document.all['Right'].offsetTop 
//		        + ", parent.document.all['Right'].parentNode.offsetTop=" + parent.document.all['Right'].parentNode.offsetTop
//		        + ", parent.document.all['Right'].offsetTop=" + parent.document.all['Right'].offsetTop);
//            tt=parent.document.all['Right'].parentNode;
//            ttop +=tt.offsetTop;
//            while (tt = tt.offsetParent){ttop+=tt.offsetTop;}
            
		    var objTop=ttop;// + parent.document.all['Right'].parentNode.offsetTop;//document.body.scrollTop+objRun.offsetTop + objRun.offsetHeight;
		    var objLeft=tleft-2;//document.body.scrollLeft+objRun.offsetLeft;
		    
		    //window.parent.DivSetVisible(true,0,0,window.name,objRun.id);window.parent.
		    DivSetVisible(true,objTop,objLeft,window.name,objRun.id);
        }
        function closeParentDiv(objRun)
        {
            if(parent==null)//window.parent.
                return;
            DivSetVisible(false,0,0,window.name,objRun.id);
        }
        
        //�����б����----------------------------------------------------------------
        var num = 0;
		var k = -1;
		var selecto = null;
		var g_iFrame = "";
		var g_objID = "";
			

			function keyPressdiv(key, iFrame, ObjID, OrgValue) {
			    //alert(key);
			    if (key == 13) {
			        DivSetVisible(false, 0, 0,'','');
			        return;
			    }
			    if (key != 40 && key != 38)
			        return;
			    g_iFrame = iFrame;
			    g_objID = ObjID;
			    var DivRef = document.getElementById('PopupDiv');
			    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
			        num = DivRef.firstChild.rows.length;
			    }
			    else
			    {
			        return;
			    }
			    if (num <= 0)
			        return;
			    //alert(key);
			    if (PopupDiv.style.display == "block") {
			        //��
			        if (key == 40) {
			            k++;
			            if (k >= num) {
			                k = -1;
			            }
			        }
			        //��
			        if (key == 38) {
			            k--;
			            if (k < 0) {
			                k = num;
			            }
			        }
			        //alert(k);
			        
			        for (var i = 0; i < num; i++) {
			            mouseOut(DivRef.firstChild.rows[i]);
			        }
			        if (k >= 0 && k < num) {//ѡ��ֵ
			            mouseOver(DivRef.firstChild.rows[k]);
			            selecto = DivRef.firstChild.rows[k].cells[0].innerHTML;
			            SelectV(selecto);
			        }
			        else if(OrgValue.indexOf('=')>0)//ԭʼֵ
			        {
			            OrgValue=OrgValue.substr(OrgValue.indexOf('=')+1);
			            SelectV(OrgValue);
			        }
			    }
			}

			function innerHTMLdiv(strList) {
			    var DivRef = document.getElementById('PopupDiv');
			    DivRef.innerHTML = strList;
			    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
			        num = DivRef.firstChild.rows.length;
			    }
			}

			
			function SelectV(str) {
			    SetV(g_objID, str);//window.frames[g_iFrame].
			}

			function ClickSelectV(str) {
			    //alert(g_objID + "_" + str);
			    SetV(g_objID, str);//window.frames[g_iFrame].
			    DivSetVisible(false, 0, 0, '', '');
			}
			function mouseOver(e) {
			    e.style.backgroundColor = "blue";
			    e.style.color = "white";
			    //e.style.fontWeight = "bold";
			}
			function mouseOut(e) {
			    e.style.backgroundColor = "#dedede";
			    e.style.color = "black";
			    //e.style.fontWeight = "normal";
			}
			function DivSetVisible(state, objTop, objLeft, iFrame, ObjID) {
			    var DivRef = document.getElementById('PopupDiv');
			   
			    DivRef.style.top = objTop; //document.body.scrollTop + event.clientY + 10;
			    DivRef.style.left = objLeft; //document.body.scrollLeft + event.clientX;
			    
			    var IfrRef = document.getElementById('framePopupDiv');
			    if (state) {
			        DivRef.style.display = "block";
			        IfrRef.style.width = DivRef.offsetWidth;
			        IfrRef.style.height = DivRef.offsetHeight;
			        IfrRef.style.top = DivRef.style.top;
			        IfrRef.style.left = DivRef.style.left;
			        IfrRef.style.zIndex = DivRef.style.zIndex - 1;
			        IfrRef.style.display = "block";
			        g_iFrame = iFrame;
			        g_objID = ObjID;
			    }
			    else {
			        DivRef.style.display = "none";
			        IfrRef.style.display = "none";
			    }
			} 
        //�����б����----------------------------------------------------------------
        
		</script>
		
	</HEAD>
	<body class="InputBody" onload="return window_onload();" ondblclick="fireEdit();" onkeydown="findNextFocus();" onclick="DivSetVisible(false,0,0,window.name,'');">
	    <div id="PopupDiv"  style="border: 1px solid #0000FF; background-position: #F2F5E0; position: absolute; cursor: hand; filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
        color:Black; padding: 0px; background:#F2F5E0; display:none; z-index: 100; width:300px; left:0px; top:0px">
    </div>
    <iframe id="framePopupDiv" style="position: absolute; Display:none; z-index: 99; width: 0; height: 0;
        top: 0; left: 0; scrolling: no;" frameborder="0" src=""></iframe>
		<form id="Form1" name="Form1" method="post" target="frmDataRun" onsubmit = "CollectDataRun()">
		
			<%//=Request.QueryString["DataXPath"]%>
			<%//Response.Write(ShowDoc.InnerXml);||NodeTrBodyList.Count==0<%if(NodeTrBodyList==null) Response.Write("none");%>
			<table width="100%"  cellspacing=0 cellpadding=0 border=0 align=center style="DISPLAY:<%=strHideInput%>">
				<tr>
					<td valign="top" align="center">
						<%if(/*nodeAssessorTds!=null&&*/nodeAssessorTdsTitle!=null&&nodeAssessorTdsTitle.Count>0){
							int iAssessorEnum=0;
							
						%>
						<TABLE id="TablenodeAssessorTds" class="AssessorTable" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<%
								int AssessorsTDS=iColsAssessor;
								foreach(XmlNode myNode in nodeAssessorTdsTitle){
									//foreach(XmlNode myNode in nodeAssessorTds){
									AssessorsTDS--;
									iAssessorEnum++;
									string tdTitle=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
									string tdTitleEName=myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
									
									string tdDataValue="";
									
									XmlNode tdDataNode = null;
									if(nodeAssessorTds != null)
									{
										tdDataNode = nodeAssessorTds[0].ParentNode.SelectSingleNode("td[@Column='"+tdTitleEName+"']");
									}
									if(tdDataNode!=null)
										tdDataValue=tdDataNode.InnerXml;
									else
										tdDataValue="";
										
									tdDataValue=Server.HtmlEncode(tdDataValue);
									%>
									<td nowrap align=right style="font-weight:bold"><%=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml%></td>
									<td nowrap style="width:128px;"><div title="<%=tdDataValue%>" disabled><%=(tdDataValue.Length>15)?tdDataValue.Substring(0,13)+ "..":tdDataValue%>&nbsp;</div></td>
									<%
									if((int)iAssessorEnum/iColsAssessor==(double)iAssessorEnum/iColsAssessor&&iAssessorEnum!=nodeAssessorTdsTitle.Count)
									{
										Response.Write("</tr><tr>");
										AssessorsTDS=iColsAssessor;
									}
									//}
								}%>
								<%if(AssessorsTDS>0){
									for(int blankTdLeft=0;blankTdLeft<AssessorsTDS;blankTdLeft++)
									{
									%>
										<td></td><td></td>
									<%
									}
								
								}%>
							</tr>
						</table>
						<br>
						<%}%>
						<TABLE id="TableData" name="TableData" cellSpacing="1" cellPadding="1" border="0" width="98%">
							<tr class="top">
								<td valign="top" align="center" width="<%=(iCols==0)?100:(int)(100/iCols)%>%">
								<%
								int i=0;
								if(iCols==0)iCols=2;
								string hTableFields="";
								
								int iEachColsCount=(int)(NodeTdTitleList.Count/iCols);
								if(iEachColsCount<(double)NodeTdTitleList.Count/iCols)
									iEachColsCount++;
									
								int iEachColsCountEnum=0;
								
								//Ϊ������ͨ�������ƶ�,ȡID������
								string strAllIds="";
								
								if(NodeTdTitleList!=null)
								{
									foreach(XmlNode myNode in NodeTdTitleList)
									{
										//�ֶ�����;ColumnType="0" ColumnPrecision="20" 
										//���� table1.field1 table1/table1/tablex.fieldx
										
										string strColumnType=myNode.Attributes.GetNamedItem("ColumnType").InnerXml;
										string strColumnPrecision=myNode.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
										string strTableName=RetrieveTableName(myNode.ParentNode.ParentNode);
										string strColumnCName=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
										string strColumnEName=myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
										
										string strKeyIndex=myNode.Attributes.GetNamedItem("KeyIndex").InnerXml;
										
										//================ֻ������=================
										string strNoChange = myNode.Attributes.GetNamedItem("ReadOnly").Value;
										//====================End==================
										
										//================���������=================
										string strAllowNull ="No";
										try
										{
											strAllowNull = myNode.Attributes.GetNamedItem("AllowNull").Value;
										}
										catch{}
										//====================End==================
										
										string ValidateValue=" ";
										
										//<OPTION selected value="0">�ַ�</OPTION>
										//<OPTION value="1">����</OPTION>
										//<OPTION value="2">����</OPTION>
										//<OPTION value="3">�ļ�</OPTION>
										//<OPTION value="4">������Ϣ</OPTION>
										//<OPTION value="5">�����б�</OPTION> 
										//<OPTION value="6">��¼�������Ϣ</OPTION> 
										switch(strColumnType)
										{
											case "0":
												ValidateValue=" onfocus=\"window.status='���������ַ�'\" ONKEYPRESS=\"window.status='���������ַ�';\" ";
												break;
												
											case "1":
												//ValidateValue=" onfocus=\"window.status='ֻ����������'\" ONKEYPRESS=\"event.returnValue=IsDigit();\" ";
												ValidateValue=" onfocus=\"window.status='ֻ����������'\" ONKEYPRESS=\"event.returnValue=IsValidateDigit(this);\" ";
												break;
											case "2":
												ValidateValue=" onchange=\"IsDate(this);\" onfocus=\"setday(this);window.status='ֻ����������,��ʽyyyy-MM-DD';\" ";
												break;
											case "3":
												break;
											case "4":
												break;
											case "5":
												break;
											case "6":
												break;
												
										}
										
										//TableName.FieldName,TableName.FieldName;
										hTableFields=hTableFields + "," + myNode.Attributes.GetNamedItem("ColumnCName").InnerXml;
										strAllIds=strAllIds + "," + myNode.Attributes.GetNamedItem("ColumnEName").InnerXml;
										
									%>
									<%if(iEachColsCountEnum==0){%>
									<TABLE id="TableData<%=i%>" name="TableData<%=i%>" cellSpacing="1" cellPadding="1" width="100%" border="0" class="InputTable">
									<%}%>
										<tr>
										<td width="12" >&nbsp;</td>
										<td nowrap align=right width="20%" title="<%=strColumnEName%>"><%=myNode.Attributes.GetNamedItem("ColumnCName").InnerXml%>&nbsp;</td>
										<td nowrap class="small" style="FONT-SIZE: 9pt" width="79%">
										<%
										//����Ĵ�����¼��ʱ�Ĵ�����**********************************************k
										//FunctionRules[0]�����ܰ�ť��
										//FunctionRules[1]�����ܰ�ť���
										//FunctionRules[2]���������
										//FunctionRules[3]���Ƿ��ƹ���
										//FunctionRules[4]�����ƹ��ܻ���㹦�ܹ���
										//FunctionRules[5]���¼�
										//FunctionRules[6]����������
										
										XmlNode myFunction=myNode.SelectSingleNode("Input/@FunctionOnInput");
										string[] FunctionRules;
										string FunctionName="";
										string FunctinEvent="";
										string FunctionButtonEvent="";
										int intFunctionRulesLength=1;
										string strOutPara="";
										if(myFunction!=null&&myFunction.InnerXml.Trim()!="")
										{
											FunctionRules=myFunction.InnerXml.Split("|".ToCharArray());
											FunctionName=FunctionRules[0].Trim();
											intFunctionRulesLength=FunctionRules.Length;
											FunctionButtonEvent="";
											if(FunctionRules[5].Trim()!="")
											{
												FunctionButtonEvent=FunctionRules[5].Trim() + "=\"";
												strOutPara=(FunctionRules[6].Trim()=="")?strColumnCName:FunctionRules[6].Trim();
												FunctionButtonEvent +="Javascript:Run" + strColumnEName 
													+ "F("+FunctionRules[3].Trim().ToLower()+",'"
													+ FunctionRules[4].Trim()+"','"
													+ FunctionRules[2].Trim()+"','"
													+ strOutPara +"','" + FunctionRules[0].Trim().ToLower()+"',this)" + "\"";
												%>
													<script language="javascript">
													
													function Run<%=strColumnEName%>F(bUserDefinedModul,ModulPath,InPara,OutPara,OperDesc,objRun)
													{
													    var strInPara="";
														strInPara=CollectInParaValues(InPara);
													    //alert(modifyObj);
													    <%if(FunctionRules[3].Trim().ToLower()=="true"){ %>
														if(bUserDefinedModul)
														{
															var strDelimiter="?";
															if(ModulPath.indexOf("?")>0)
															{
																ModulPath=ReCollectInParaValues(ModulPath,strInPara);
																//alert(ModulPath);
																strDelimiter="&";
															}
															
															//�ж��Ƿ����޸�,������
															if(ModulPath.indexOf("=BModify")>0 || ModulPath.indexOf("=undefinedBModify")>0){
															    var DataXPathModify="<%=Request.QueryString["DataXPath"]%>";
//															    if(DataXPathModify=='')
//															    {
//															        alert('���ܽ���['+OperDesc+']����,����������¼���������ܴ˲���');
//															        return;
//															    }
															    
															    var modifyObj = parent.parent.document.getElementById("BAdd");
															    
															        if((modifyObj != void 0)
															            && modifyObj.style.display != "none"
															            && modifyObj.style.border=='#ccccff thin inset'){
															                var strFunctionString=strInPara.replace("��Ŀ����=","");
															                if(ModulPath.indexOf("FunctionString=")>0){
															                    ModulPath=ModulPath.replace("FunctionString=","TemplateRules=" + strFunctionString);
															                    //ModulPath=ModulPath.replace(strInPara,);
															                    ModulPath=ModulPath.replace("=BModify","=BAdd");
															                    ModulPath=ModulPath.replace("=undefinedBModify","=BAdd");//&AssessorXpath=
    															                if(strFunctionString=='')
    															                {
															                        alert('��Ҫ��ȷ���걨������Ŀ����ѡ����Ŀ����!');
															                        return;
															                    }
															                }
															                else{
															                    alert('���ܽ���['+OperDesc+']����,������������¼���������ȱ���');
															                    return;
															                }
															        }
															        else
															        {
															            if(DataXPathModify=='')
															            {
															                alert('���ܽ���['+OperDesc+']����,���Ķ����Ͻǵ� [��������]\n\n����ҳ���·� [���걨]');
															                return;
															            }
															        }
															    /*
															    var modifyObj = parent.parent.document.getElementById("BModify");
															    if((modifyObj != void 0)
															        && modifyObj.style.display != "none"
															        && modifyObj.style.border!='#ccccff thin inset'){
															            alert('û���޸Ĳ������˹��ܲ�����,����ѡ������޸Ĳ���');
															            return;
															    }
															    */
															}
															//alert('1');
															//�ж�ֱ�ӵ����޸�
															var strFunctionStringE=strInPara.substr(strInPara.indexOf("=")+1);
															if(ModulPath.indexOf("FunctionStringEmployee=")>0){
											                    ModulPath=ModulPath.replace("FunctionStringEmployee=","PersonsList=" + strFunctionStringE);
											                    //ModulPath=ModulPath.replace(strInPara,);
											                    ModulPath=ModulPath.replace("=BModify","=BAdd");
											                    ModulPath=ModulPath.replace("=undefinedBModify","=BAdd");//&AssessorXpath=
												                if(strFunctionString=='')
												                {
											                        alert('��Ҫ��ȷ���걨������Ŀ����ѡ����Ŀ����!');
											                        return;
											                    }
											                }
															if(ModulPath.indexOf("Fire=Modify")>0 || ModulPath.indexOf("=Modify")>0){
															    var DataXPathModify="<%=Request.QueryString["DataXPath"] %>"
															    if(DataXPathModify!='')
															    {
															       var modifyObj = parent.parent.document.getElementById("BModify");
															        if((modifyObj != void 0)
															            && modifyObj.style.display != "none"
															            && modifyObj.style.border!='#ccccff thin inset'){
															                fireEdit();
															        }
															    }
														        else
														        {
														            alert('���ܽ���['+OperDesc+']����,û��ѡ������');
														            return;
														        }
															}
															//alert(ModulPath);
															//alert(ModulPath.indexOf("ServerVariables={")>0);
															//ȡ�ϼ�ģ��Ĳ���,�Ժ�Ҫ�ģ�����������
															//ServerVariables={TableCName,TableEName,ModuleID,DataXPath,AssessorXpath,Style}
															var ParentParas="";
															if(ModulPath.indexOf("ServerVariables={")>0){
															    var ModulePathParas=ModulPath.substr(ModulPath.indexOf("ServerVariables={") );
															    ModulePathParas=ModulePathParas.substr(ModulePathParas.indexOf("{")+1);
															    if(ModulePathParas.indexOf("}")>0)
															    {
															        ModulePathParas = ModulePathParas.substr(0,ModulePathParas.indexOf("}"));
															    }
															    var ParentParasAll="<%=Request.ServerVariables["Query_String"] %>&";
															    if(ModulePathParas=="true")
															    {
															        ParentParas=ParentParasAll;
															    }
															    else{
															        ParentParasAll = "&" + ParentParasAll;
															        //alert("ModulePathParas=" + ModulePathParas);
															        var ModulePathParasArray=ModulePathParas.split(",");
															        for(var iPara=0;iPara<ModulePathParas.length;iPara++)
															        {
															            var myString="&" + ModulePathParasArray[iPara] + "=";
															            var intIndex=ParentParasAll.indexOf(myString);
															            
															            if(intIndex>=0)
															            {
															                var iParaValue=ParentParasAll.substr(intIndex + myString.length);
															                if(iParaValue.indexOf("&")>0)
															                    iParaValue=iParaValue.substr(0,iParaValue.indexOf("&"));
															                
															                ParentParas +=myString + iParaValue;
															            }
															        }
															        if(ParentParas.length>0)
															            ParentParas=ParentParas.substr(1) + "&";
															        //alert("ParentParas=" + ParentParas);
															    }
															}
															var strPath="SelectModalDialog.aspx?" 
																+ ModulPath + strDelimiter + ParentParas
																+ "FunctionString=" 
																+ strInPara;
															
															//alert(strPath);
															var WindowsOpenStyle=ModulPath;//WindowsOpenStyle=width=500,height=600,scrollbars=1,resizable=yes,top=0,left=200
															if(WindowsOpenStyle.indexOf('WindowsOpenStyle=')>0)
															{
															    WindowsOpenStyle=WindowsOpenStyle.substr(WindowsOpenStyle.indexOf('WindowsOpenStyle='));
															    WindowsOpenStyle=WindowsOpenStyle.substr(WindowsOpenStyle.indexOf('=') +1);
															    if(WindowsOpenStyle.indexOf('&')>0)
															    {
															        WindowsOpenStyle=WindowsOpenStyle.substr(0,WindowsOpenStyle.indexOf('&'));
															    }
															}
															if(ModulPath.indexOf("WindowShowModalDialog=true")>0)
															{
															    if(WindowsOpenStyle.indexOf('width')>=0)
															    {
															        WindowsOpenStyle =WindowsOpenStyle.replace(/=/g,':');
															        WindowsOpenStyle =WindowsOpenStyle.replace(/,/g,';');
															        WindowsOpenStyle =WindowsOpenStyle.replace(/width:/g,'dialogWidth:');
															        WindowsOpenStyle =WindowsOpenStyle.replace(/;height:/g,';dialogHeight:');
															    }
															    else
															    {
															        WindowsOpenStyle="status:yes;resizable:yes;dialogHeight:560px;dialogWidth:780px;center:yes";
															    }
															    //alert(WindowsOpenStyle);
															    var DlgRtnValue = window.showModalDialog(strPath, 
																    "", WindowsOpenStyle);
    																
															    if(DlgRtnValue != void 0)
															    {
																    //alert(DlgRtnValue);
																    setOutParaValues(DlgRtnValue,OutPara,'<%=strColumnCName%>');
															    }
															}
															else
															{
															    if(WindowsOpenStyle.indexOf('width=')>=0)
															    {
															        WindowsOpenStyle =WindowsOpenStyle.replace(/=/g,'=');
															    }
															    else
															    {
															        WindowsOpenStyle="width=500,height=600,scrollbars=1,resizable=yes,top=0,left=200";
															    }
															    strPath=strPath.substr(strPath.indexOf("?")+1);
															    window.open(strPath,"_blank",WindowsOpenStyle);
															}
														}
														<%} %>
														<%if(FunctionRules[3].Trim().ToLower()!="true"){ %>
														if(!bUserDefinedModul)
														{
														    if(strInPara.indexOf("=")>0){
															        strInPara=strInPara.substr(strInPara.lastIndexOf("=")+1);
															    }
														    //bUserDefinedModul,ModulPath,InPara,OutPara,OperDesc
														    //alert(strInPara);
															//setOutParaValues(DlgRtnValue,OutPara,'<%=strColumnCName%>');
															if(ModulPath=='����ƴ����ͷ()')
															{
															   
															    var RtnValue = Zhifang.Utilities.Query.Input.InputOneTableMain.GetPYString(strInPara).value;  
															    //alert(RtnValue);
															    setOutParaValues(RtnValue,OutPara,'<%=strColumnCName%>');
															}
															//��ȡ�б�(��:hr_employees;�ֶ�:Namel+NameF,SN,EMail;���:200;����:15;��ʾ�ֶ�:*)
															//(key,iFrame,strList,ObjID)
															if(ModulPath.indexOf('��ȡ�б�(')==0)
															{
															    //if(event.propertyName!='value')return; 
															    if(strInPara.indexOf("=")>0){
															        strInPara=strInPara.substr(strInPara.lastIndexOf("=")+1);
															    }
//															    if(strInPara=="")
//															    {
//															        //window.parent.DivSetVisible(false,0,0,window.name,objRun.id);
//															        DivSetVisible(false,0,0,window.name,objRun.id);
//															        return true;
//															    }
															    if(parent==null)
															        return true;
                
															    if(varOrgValue=="" || varOrgValue.indexOf(objRun.id)<0)
															    {
															        varOrgValue = objRun.id + "=" + objRun.value;
															    }
															    
															    if(event.keyCode>=37 && event.keyCode<=40 || event.keyCode==13)
															    {
															        if(document.getElementById('PopupDiv').style.display == "block")//window.parent.
															        {
															            keyPressDivLocal(objRun,varOrgValue);
															            return true;
															        }
															    }
															    
															    var RtnValue = Zhifang.Utilities.Query.Input.InputOneTableMain.SearchList(strInPara,ModulPath).value;  
															    //alert(objRun.id);
//															    if(event.propertyName=='value'){
//															        showParentDiv(objRun);
//															    }
															    window.status='��ȡ������....';
															    innerHTMLdiv(RtnValue);//window.parent.
															    //alert(RtnValue=='');
															    showParentDiv(objRun);
															    varOrgValue = objRun.id + "=" + objRun.value;
															    k=-1;
															        
															    //window.parent.changediv(key,'Right',RtnValue,objRun.id);
															    //window.status=RtnValue;
															    //alert(event.propertyName);
															    //setOutParaValues(RtnValue,OutPara,'<%=strColumnCName%>');
															}
															
															//������---------------------------------
															if(ModulPath.indexOf('������(')==0)
															{
															    //debugger;
															    var strModulPath=ModulPath.substr(ModulPath.indexOf('(') +1);
															    strModulPath=strModulPath.substr(0,strModulPath.length-1);
															    var strModulPathLoop=strModulPath;
															    
															    while(strModulPathLoop.length>0)
															    {
															        var iModulPathStarts=strModulPathLoop.indexOf("[");
															        var iModulPathEnds=strModulPathLoop.indexOf("]");
															        var strThisM="";
															        //alert("����1:  " + strModulPath);
															        if(iModulPathStarts>=0 && iModulPathEnds > iModulPathStarts)
															        {
															            strThisM=strModulPathLoop.substr(iModulPathStarts+1,iModulPathEnds-iModulPathStarts-1);
															            var objThisM=document.getElementById(strThisM);
															            //alert("����2: �ֶ�: " + strThisM );
															            if(objThisM!=null && objThisM.value!='undefined')
															            {
															                var strMValue=objThisM.value;
															                if(strMValue=="" || strMValue.indexOf(" ")>=0)
															                {
															                    strMValue="0";
															                }
															                //strModulPath=strModulPath.replace(/'['objThisM.value']'/g,strThisM);
															                strModulPath=strModulPath.replace("[" + strThisM + "]",strMValue);
															                //alert("����2: �ֶ�: " + strThisM );//+ "  ��ʼֵ:" + objThisM.value );
															            }
															            else
															            {
															                window.status +=strThisM +'δ�ҵ����ı���.';
															            }
															            strModulPathLoop=strModulPathLoop.substr(iModulPathEnds+1);
															        }
															        else
															        {
															            strModulPathLoop="";
															        }
															    }
															    var RtnValue='';
															    var strEval=strModulPath.replace(/ /g,'');
															    
															    //alert(strModulPath == strModulPath.replace(/ /g,''));
															    try{
															        //alert(strEval);
															        
															        RtnValue='' +eval(strEval);
															        
															        //alert(RtnValue);
															        setOutParaValues(RtnValue,OutPara,'<%=strColumnCName%>');
															    }
															    catch(e)
															    {
															        window.status='<%=strColumnCName%>�޷���ֵ:' + e.message;
															    }
															}
															//������---------------------------------
														}
														<%} %>
													}
													</script>
												<%
											}
												
											if(FunctionName!=""&&intFunctionRulesLength>5)
											{
												%>
												<table cellpadding="0" cellspacing="0" border="0" width="100%">
												<tr>
													<td  width="90%">
												<%
											}
											else
											{
												//��ʱ�ɵ�ǰ�ؼ���ɴ˹���
												FunctinEvent=FunctionButtonEvent;
                                                //ValidateValue += " " + FunctionButtonEvent + " ";
                                                ValidateValue = " " + FunctionButtonEvent + " ";
                                                ValidateValue += " objRunEvent=\"true\"";
                                                //ValidateValue += " onkeydown=\"keyPressDivLocal(this);\"";
                                                //ValidateValue += " onfocus=\"showParentDiv(this);\" ";
                                                //ValidateValue += " onblur =\"closeParentDiv(this);\" ";
                                                
                                            }
										}
										//����Ĵ�����¼��ʱ�Ĵ�����**********************************************
										%>
										<%
										XmlNode NodeData=null;
										string strTdData="";
										if(NodeTrBodyList!=null&&NodeTrBodyList.Count>0)
										{
											NodeData=NodeTrBodyList[0].SelectSingleNode("td[@Column='"+strColumnEName+"']");
											if(NodeData!=null)
												strTdData=NodeData.InnerXml;
										}
										int ColumnHeight=1;
										if(myNode.SelectSingleNode("Input/@ColumnHeight").InnerXml!="")
										{
											try
											{
												ColumnHeight=Int32.Parse(myNode.SelectSingleNode("Input/@ColumnHeight").InnerXml);
											}
											catch
											{
												ColumnHeight=1;
											}
										}
                                        string strNoChangeYesToReadonly = "";
                                        string objStyle = "";
                                        if (strNoChange.ToUpper() == "YES")
                                        {
                                            strNoChangeYesToReadonly = " readonly=\"readonly\" ";

                                            ValidateValue += strNoChangeYesToReadonly;
                                            objStyle += " style=\"background-color:#EFEFEF;\" ";
                                            ValidateValue += objStyle;
                                        }
										switch(myNode.Attributes.GetNamedItem("ColumnType").InnerXml)
										{
											case "0":
											case "1":
											case "2":
												%>
													<% if(ColumnHeight == 1)
														{%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													type="text" style="width:100%" method="=" disabled AllowNull="<%=strAllowNull%>"
													id="<%=strColumnEName%>" <%=ValidateValue%> 
													value="<%=strTdData%>" ColumnDefault="<%=RetrieveDefaultValue(myNode,NodeTrBodyList)%>">
												<%
													}
												    else
												    {
						
													%>
														<textarea style="width:100%" title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>" 
														method="=" disabled AllowNull="<%=strAllowNull%>" id="<%=strColumnEName%>" <%=ValidateValue%> ColumnDefault="<%=RetrieveDefaultValue(myNode,NodeTrBodyList)%>" rows="<%=ColumnHeight%>"><%=strTdData%></textarea>
													<%
												    }
												break;
											case "3"://�ļ�
												%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													type="text" method="=" disabled style="WIDTH:0px;HEIGHT:20px"
													id="<%=strColumnEName%>" columnType="�ļ�" AllowNull="<%=strAllowNull%>"
													value="<%=strTdData%>">
													<%if(strTdData!=""){%><a id="<%=strColumnEName%>0a0" href="DownLoadFile.aspx?File=<%=strTdData%>" onclick="return DownloadFile('<%=strColumnEName%>',this)" NoChange="<%=strNoChange%>">����</a
													>&nbsp;<a id="<%=strColumnEName%>1b1" href="#" onclick="DeleteFile('<%=strColumnEName%>','DeleteFile.aspx?File=<%=strTdData%>','frmDataRun')" disabled NoChange="<%=strNoChange%>">ɾ��</a
													>&nbsp;<%}%><a href="#" onclick="javascript:uploadFile('<%=strColumnEName%>','DeleteFile.aspx?File=<%=strTdData%>','frmDataRun',this)" disabled NoChange="<%=strNoChange%>">�ϴ�</a>
												<%
											break;
											case "4"://����
                                                string EditMode = "1";
                                                if (myNode.Attributes.GetNamedItem("ColumnPrecision")!=null)//ColumnPrecision
                                                    EditMode = myNode.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
												%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>"  NoChange="<%=strNoChange%>"
													type="text" size="1" method="=" disabled style="WIDTH:0px;HEIGHT:20px" 
													id="<%=strColumnEName%>" columnType="����" AllowNull="<%=strAllowNull%>"
													value="<%=strTdData%>">
													<%if(strTdData!=""){%><a href="#" onclick="javascript:BrowseNews('<%=strColumnEName%>','<%=DataBase%>')" NoChange="<%=strNoChange%>">�����Ϣ</a
													>&nbsp;<%}%><a href="#" onclick="javascript:EditNews('<%=strColumnEName%>','<%=DataBase%>','<%=myNode.Attributes.GetNamedItem("ColumnDefault").InnerXml%>','<%=EditMode %>')" disabled NoChange="<%=strNoChange%>" style="Display:<%if (strNoChange == "Yes"){%>none<%}%>">�༭��Ϣ</a>
												<%	
													if(ColumnHeight>1)
													{
														%>	<br>
															<iframe name="frm<%=strColumnEName%>" id="frm<%=strColumnEName%>" width="100%" style="BORDER:skyblue 1px solid" scrolling="no"  
															height="<%=ColumnHeight*22%>" 
															<%if(Request.QueryString["btnid"]=="BAdd"){%>
															src="inputBrowseNews.aspx?FilePath=<%=DataBase%>&Template=<%=myNode.Attributes.GetNamedItem("ColumnDefault").InnerXml%>"
															<%}else{%>
															src="inputBrowseNews.aspx?FilePath=<%=DataBase%>&FileName=<%=strTdData%>"
															<%}%>
															 frameborder=0></iframe>
														<%
														//iEachColsCountEnum =iEachColsCountEnum+ColumnHeight-1;
													}
												break;
											case "5"://�б�
												%>
													<%
														switch(strColumnPrecision)//����
														{
															case "1": //������ѡ
													%>
																<select title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
																size="1" style="width:100%" method="=" readonly disabled AllowNull="<%=strAllowNull%>"
																id="<%=strColumnEName%>" ColumnDefault="<%=RetrieveDefaultValue(myNode,NodeTrBodyList)%>">
																<option></option>
																<%=RetrieveDropDownList(myNode.SelectSingleNode("Dictionary/@DataSource").InnerXml,myNode.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData)%>
																</select>
																<%
															break;
															
															case "2"://������ѡ
																%>
																<SELECT title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
																style="width:100%" method="=" readonly disabled AllowNull="<%=strAllowNull%>"
																id="<%=strColumnEName%>"  multiple size="3">
																	<option></option>
																	<%=RetrieveDropDownList(myNode.SelectSingleNode("Dictionary/@DataSource").InnerXml,myNode.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData)%>
																</SELECT>
															<%	
																//iEachColsCountEnum =iEachColsCountEnum+1;
																break;
																
															case "3": //Radio ��ѡ
															%><%=RetrieveRadioCheckList(strColumnEName,"Radio",myNode.SelectSingleNode("Dictionary/@DataSource").InnerXml,myNode.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData, strNoChange)%>
																<%
																break;
															case "4"://Check ��ѡ
															%><%=RetrieveRadioCheckList(strColumnEName,"CheckBox",myNode.SelectSingleNode("Dictionary/@DataSource").InnerXml,myNode.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData, strNoChange)%>
															
														<%
															break;
														}
														 %>
													
													
												<%
												break;
											case "6":
												%>
													<select title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													size="1" style="width:100%" method="=" readonly disabled AllowNull="<%=strAllowNull%>"
													id="<%=strColumnEName%>" ColumnDefault="<%=RetrieveDefaultValue(myNode,NodeTrBodyList)%>">
													<%=RetrieveUserInfo(myNode.SelectSingleNode("Dictionary/@DataSource").InnerXml,myNode.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData)%>
													</select>
												<%
												break;
											default:
												break;
										}
										%>
										<%
										if(myFunction!=null&&myFunction.InnerXml.Trim()!="")
										{
											if(FunctionName!=""&&intFunctionRulesLength>5){
											%>
											</td><td width="10%" align="center">
												<input type="button" value="&nbsp;<%=FunctionName%>" style="width:<%=FunctionName.Length*15+5%>px" <%=FunctionButtonEvent%>></td>
											</tr>
											</table>
											<%}
										}
										%>
										</td>
										</tr>
										<%
										i++;
										iEachColsCountEnum++;
										if(iEachColsCountEnum>=iEachColsCount){%>
										</table>
										<%
											iEachColsCountEnum=0;
											if(i!=NodeTdTitleList.Count){
											%>
											</td><td valign="top" align="center" width="<%=(iCols==0)?100:(int)(100/iCols)%>%">
											<%}
										}%>
									<%
									
									//if((int)i/iCols==(double)i/iCols&&i!=NodeTdTitleList.Count)
									//	Response.Write("</tr><tr class=\"top\">");
									}
								}%>
								</td>
							</tr>
						</table>	
						</table>
					</td>
				</tr>
			</table>
			<input type="hidden" id="hQueryCollection" name="hQueryCollection" value="">
			<input type="hidden" id="hAction" name="hAction" value="Browse">
			<input type="hidden" id="txtBatches" name="txtBatches" value="">
			<input type="hidden" id="hSubTablesCopy" name="hSubTablesCopy" value="">
			<input type="hidden" id="hNotAllowNull" name="hNotAllowNull" value="">
			<input type="hidden" id="hAllIds" name="hAllIds" value="<%=strAllIds%>">
			<input type="hidden" id="hDataCollectionSubMes" name="hDataCollectionSubMes" value="">
			
		
			<br>
			<div class="biaoti" id="txtKeyIndexBatches" style="display:none"></div>
			
		<!--/form-->
		<div id="divSubsMe">
		<%
			if(NodeTdTitleList!=null&&NodeTdTitleList.Count>0)
			{
				XmlNode NodeSubMeParent=NodeTdTitleList[0].ParentNode.ParentNode;
				if(NodeSubMeParent.Attributes.GetNamedItem("SubsIuputShow")!=null&&NodeSubMeParent.Attributes.GetNamedItem("SubsIuputShow").InnerXml=="True")
				{
				XmlNodeList tablesSubsMe=NodeSubMeParent.SelectNodes("tr/Table");
				foreach(XmlNode EachSub in tablesSubsMe)
				{
					string tableCName=EachSub.Attributes.GetNamedItem("TableCName").InnerXml;
					string tableEName=EachSub.Attributes.GetNamedItem("EName").InnerXml;
					XmlNodeList tdSubsMe=EachSub.SelectNodes("tr/td[Input/@DisplayOnInput='Yes']");
					if(EachSub.Attributes.GetNamedItem("SubsMeIuputShow")!=null&&EachSub.Attributes.GetNamedItem("SubsMeIuputShow").InnerXml=="False")
						continue;
					int intSubMeRows=10;

                    string SubButtons = "|||||||";
                    if (EachSub.Attributes.GetNamedItem("Buttons") != null)
                        SubButtons = EachSub.Attributes.GetNamedItem("Buttons").Value;
					
					string strSubMeRows=(EachSub.Attributes.GetNamedItem("SubsMeIuputLines")!=null)?EachSub.Attributes.GetNamedItem("SubsMeIuputLines").InnerXml:"10";
					intSubMeRows=Int32.Parse(strSubMeRows);
					
                    //XmlNodeList NodeTrBodySubsMeList=null;
					string SubMeDataLines="0";
									
					if(NodeTrBodyList!=null&&NodeTrBodyList.Count>0)
					{
                        NodeTrBodySubsMeList=getNodeTrBodySubsMeList(tableEName,"",NodeTrBodyList[0]);
						//NodeTrBodySubsMeList=NodeTrBodyList[0].SelectNodes("Table[@EName='"+tableEName+"']/tr");
					}
            	    if(NodeTrBodySubsMeList!=null)
					    SubMeDataLines=NodeTrBodySubsMeList.Count.ToString();
					
					if(Int32.Parse(SubMeDataLines)>intSubMeRows)
						intSubMeRows=Int32.Parse(SubMeDataLines);
						
								
					%>
					<TABLE id="TableSubMeData" name="TableSubMeData" cellSpacing="0" cellPadding="1" width="95%" border="0" align="right">
						<tr>
						<td nowrap align=left width="20%" title="<%=tableEName%>" style="FONT-WEIGHT: bold"><%=tableCName%></td>
						<td nowrap align=left width="20%" title="<%=tableEName%>" style="FONT-WEIGHT: bold;FONT-SIZE: 9pt">����<%=SubMeDataLines%>��¼</td>
						
						<td nowrap align=right class="small" style="FONT-SIZE: 9pt" width="60%"><a href="#" disabled NoChange="No"  onclick="AddNewSubMe(this)"><%=(SubButtons.IndexOf("Add")>0)?"����":"" %></a>&nbsp;</td>
						</tr>
						<tr>
							<td colspan=3>
								<TABLE id="TableSubMeData_<%=tableEName%>" name="TableSubMeData_<%=tableEName%>" cellSpacing="0" cellPadding="1" width="100%" border="0" class="InputTable">
								<tr>
									<%foreach(XmlNode EachSubTD in tdSubsMe)
									{%>
									<td title="<%=EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml%>"><%=EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml%></td>
									<%}%>
									<td width="1%" nowrap colspan=2>����</td>
								</tr>
								<%for(int iRows=0;iRows<intSubMeRows;iRows++){
									bool ExistRow=true;
								%>
								<tr>
									<%foreach(XmlNode EachSubTD in tdSubsMe)
									{%>
									<!--td title="<%=EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml%>"-->
									<%//------------------------------------------------------------------------------------------------------
									//foreach(XmlNode EachSubTD in NodeTdTitleList)
									//{
										//�ֶ�����;ColumnType="0" ColumnPrecision="20" 
										//���� table1.field1 table1/table1/tablex.fieldx
										
										string strColumnType=EachSubTD.Attributes.GetNamedItem("ColumnType").InnerXml;
										string strColumnPrecision=EachSubTD.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
										string strTableName=RetrieveTableName(EachSubTD.ParentNode.ParentNode);
										string strColumnCName=EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;
										string strColumnEName=EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml;
										
										string strKeyIndex=EachSubTD.Attributes.GetNamedItem("KeyIndex").InnerXml;
										
										//================ֻ������=================
										string strNoChange = EachSubTD.Attributes.GetNamedItem("ReadOnly").Value;
										//====================End==================
										
										//================���������=================
										string strAllowNull ="No";
										try
										{
											strAllowNull = EachSubTD.Attributes.GetNamedItem("AllowNull").Value;
										}
										catch{}
										//====================End==================
										
										string ValidateValue=" ";
										
										//<OPTION selected value="0">�ַ�</OPTION>
										//<OPTION value="1">����</OPTION>
										//<OPTION value="2">����</OPTION>
										//<OPTION value="3">�ļ�</OPTION>
										//<OPTION value="4">������Ϣ</OPTION>
										//<OPTION value="5">�����б�</OPTION> 
										//<OPTION value="6">��¼�������Ϣ</OPTION> 
										switch(strColumnType)
										{
											case "0":
												ValidateValue=" onfocus=\"window.status='���������ַ�'\" ONKEYPRESS=\"window.status='���������ַ�';\" ";
												break;
												
											case "1":
												//ValidateValue=" onfocus=\"window.status='ֻ����������'\" ONKEYPRESS=\"event.returnValue=IsDigit();\" ";
												ValidateValue=" onfocus=\"window.status='ֻ����������'\" ONKEYPRESS=\"event.returnValue=IsValidateDigit(this);\" ";
												break;
											case "2":
												ValidateValue=" onchange=\"IsDate(this);\" onfocus=\"setday(this);window.status='ֻ����������,��ʽyyyy-MM-DD';\" ";
												break;
											case "3":
												break;
											case "4":
												break;
											case "5":
												break;
											case "6":
												break;
												
										}
										
										//TableName.FieldName,TableName.FieldName;
										hTableFields=hTableFields + "," + EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;
										strAllIds=strAllIds + "," + EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml;
										
										XmlNode NodeData=null;
										string strTdData="";
										//Response.Write(NodeTrBodySubsMeList.Count);
										if(NodeTrBodySubsMeList!=null&&NodeTrBodySubsMeList.Count>0&&iRows<=(NodeTrBodySubsMeList.Count-1))
										{
											NodeData=NodeTrBodySubsMeList[iRows].SelectSingleNode("td[@Column='"+strColumnEName+"']");
											if(NodeData!=null)
												strTdData=NodeData.InnerXml;
										}
										
										if(strKeyIndex=="Yes")
										{
											if(strTdData=="")
												ExistRow=false;
											else
												ExistRow=true;
										}
										
									%>
									
										<td onclick="LocateSubMeEditorMode(this,<%=ExistRow.ToString().ToLower()%>,<%=SubButtons.IndexOf("Add")>0?"true":"false" %>)" 
											ONKEYPRESS="LocateSubMeEditorMode(this,<%=ExistRow.ToString().ToLower()%>,<%=SubButtons.IndexOf("Add")>0?"true":"false" %>)" 
											nowrap class="small" style="FONT-SIZE: 9pt;CURSOR:hand" 
											title="<%=strColumnCName%>" width="<%=100/tdSubsMe.Count%>%">
										<%
										//����Ĵ�����¼��ʱ�Ĵ�����**********************************************
										//FunctionRules[0]�����ܰ�ť��
										//FunctionRules[1]�����ܰ�ť���
										//FunctionRules[2]���������
										//FunctionRules[3]���Ƿ��ƹ���
										//FunctionRules[4]�����ƹ��ܻ���㹦�ܹ���
										//FunctionRules[5]���¼�
										//FunctionRules[6]����������
										
										XmlNode myFunction=EachSubTD.SelectSingleNode("Input/@FunctionOnInput");
										string[] FunctionRules;
										string FunctionName="";
										string FunctinEvent="";
										string FunctionButtonEvent="";
										int intFunctionRulesLength=1;
										string strOutPara="";
										if(myFunction!=null&&myFunction.InnerXml.Trim()!="")
										{
											FunctionRules=myFunction.InnerXml.Split("|".ToCharArray()); 
											FunctionName=FunctionRules[0].Trim();
											intFunctionRulesLength=FunctionRules.Length;
											FunctionButtonEvent="";
											if(FunctionRules[5].Trim()!="")
											{
												FunctionButtonEvent=FunctionRules[5].Trim() + "=\"";
												strOutPara=(FunctionRules[6].Trim()=="")?strColumnCName:FunctionRules[6].Trim();
												FunctionButtonEvent +="Javascript:Run" + strColumnEName 
													+ "F("+FunctionRules[3].Trim().ToLower()+",'"
													+ FunctionRules[4].Trim()+"','"
													+ FunctionRules[2].Trim()+"','"
													+ strOutPara+"')" + "\"";
												%>
													<script language="javascript">
													function Run<%=strColumnEName%>F(bUserDefinedModul,ModulPath,InPara,OutPara)
													{
														if(bUserDefinedModul)
														{
															var strInPara="";
															strInPara=CollectInParaValues(InPara);
															var strDelimiter="?";
															if(ModulPath.indexOf("?")>0)
															{
																ModulPath=ReCollectInParaValues(ModulPath,strInPara);
																//alert(ModulPath);
																strDelimiter="&";
															}
															var strPath="SelectModalDialog.aspx?" 
																+ ModulPath + strDelimiter
																+ "FunctionString=" 
																+ strInPara;
															//alert(strPath);
															var DlgRtnValue = window.showModalDialog(strPath, 
																"", "status:yes;resizable:yes;dialogHeight:560px;dialogWidth:780px;center:yes");
																
															if(DlgRtnValue != void 0)
															{
																alert(DlgRtnValue);
																setOutParaValues(DlgRtnValue,OutPara,'<%=strColumnCName%>');
															}
														}
														else
														{
															
														}
													}
													</script>
												<%
											}
												
											if(FunctionName!=""&&intFunctionRulesLength>5)
											{
												%>
												<table cellpadding="0" cellspacing="0" border="0" width="100%">
												<tr>
													<td  width="90%">
												<%
											}
											else
											{
												//��ʱ�ɵ�ǰ�ؼ���ɴ˹���
												FunctinEvent=FunctionButtonEvent;
                                                ValidateValue += " " + FunctionButtonEvent + " ";
											}
										}
										//����Ĵ�����¼��ʱ�Ĵ�����**********************************************
										%>
										<%
										
										int ColumnHeight=1;
										if(EachSubTD.SelectSingleNode("Input/@ColumnHeight").InnerXml!="")
										{
											try
											{
												ColumnHeight=Int32.Parse(EachSubTD.SelectSingleNode("Input/@ColumnHeight").InnerXml);
											}
											catch
											{
												ColumnHeight=1;
											}
										}
										//bool ExistRow=true;
										if(strKeyIndex=="Yes")
										{
											%>
												<input type=hidden title="<%=strColumnEName%>" id="<%=strTableName + "_" + strColumnEName%>" value="<%=strTdData%>">
											<%
										}
										
										
										switch(EachSubTD.Attributes.GetNamedItem("ColumnType").InnerXml)
										{
											case "0":
											case "1":
											case "2":
												%>
													<% if(ColumnHeight == 1)
														{%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													type="text" style="width:100%" method="=" disabled AllowNull="<%=strAllowNull%>"
													id="<%=strColumnEName%>" <%=ValidateValue%> 
													value="<%=strTdData%>" ColumnDefault="<%=RetrieveDefaultValue(EachSubTD,NodeTrBodyList)%>">
												<%
													}
												    else
												    {
						
													%>
														<textarea style="width:100%" title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>" 
														method="=" disabled AllowNull="<%=strAllowNull%>" id="<%=strColumnEName%>" <%=ValidateValue%> ColumnDefault="<%=RetrieveDefaultValue(EachSubTD,NodeTrBodyList)%>" rows="<%=ColumnHeight%>"><%=strTdData%></textarea>
													<%
												    }
												break;
											case "3"://�ļ�
												%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													type="text" method="=" disabled style="WIDTH:0px;HEIGHT:20px"
													id="<%=strColumnEName%>" columnType="�ļ�" AllowNull="<%=strAllowNull%>"
													value="<%=strTdData%>">
													<%if(strTdData!=""){%><a id="<%=strColumnEName%>0a0" href="DownLoadFile.aspx?File=<%=strTdData%>" onclick="return DownloadFile('<%=strColumnEName%>',this)" NoChange="<%=strNoChange%>">����</a
													>&#20;<a id="<%=strColumnEName%>1b1" href="#" onclick="DeleteFile('<%=strColumnEName%>','DeleteFile.aspx?File=<%=strTdData%>','frmDataRun')" disabled NoChange="<%=strNoChange%>">ɾ��</a
													>&#20;<%}%><a href="#" onclick="javascript:uploadFile('<%=strColumnEName%>','DeleteFile.aspx?File=<%=strTdData%>','frmDataRun',this)" disabled NoChange="<%=strNoChange%>">�ϴ�</a>
												<%
											break;
											case "4"://����
												%>
													<input title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>"  NoChange="<%=strNoChange%>"
													type="text" size="1" method="=" disabled style="WIDTH:0px;HEIGHT:20px" 
													id="<%=strColumnEName%>" columnType="����" AllowNull="<%=strAllowNull%>"
													value="<%=strTdData%>">
													<%if(strTdData!=""){%><a href="#" onclick="javascript:BrowseNews('<%=strColumnEName%>','<%=DataBase%>')" NoChange="<%=strNoChange%>">�����Ϣ</a
													>&#20;<%}%><a href="#" onclick="javascript:EditNews('<%=strColumnEName%>','<%=DataBase%>','<%=EachSubTD.Attributes.GetNamedItem("ColumnDefault").InnerXml%>')" disabled NoChange="<%=strNoChange%>">�༭��Ϣ</a>
												<%	
													if(ColumnHeight>1)
													{
														%>	<br>
															<iframe name="frm<%=strColumnEName%>" id="frm<%=strColumnEName%>" width="100%" style="BORDER:skyblue 2px solid" scrolling="no"  
															height="<%=ColumnHeight*22%>" 
															<%if(Request.QueryString["btnid"]=="BAdd"){%>
															src="inputBrowseNews.aspx?FilePath=<%=DataBase%>&Template=<%=EachSubTD.Attributes.GetNamedItem("ColumnDefault").InnerXml%>"
															<%}else{%>
															src="inputBrowseNews.aspx?FilePath=<%=DataBase%>&FileName=<%=strTdData%>"
															<%}%>
															 frameborder=0></iframe>
														<%
														//iEachColsCountEnum =iEachColsCountEnum+ColumnHeight-1;
													}
												break;
											case "5"://�б�
												%>
													<%
														switch(strColumnPrecision)//����
														{
															case "1": //������ѡ
													%>
																<select title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
																size="1" style="width:100%" method="=" readonly disabled AllowNull="<%=strAllowNull%>"
																id="<%=strColumnEName%>" ColumnDefault="<%=RetrieveDefaultValue(EachSubTD,NodeTrBodyList)%>">
																<option></option>
																<%=RetrieveDropDownList(EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml,EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData)%>
																</select>
																<%
															break;
															
															case "2"://������ѡ
																%>
																<SELECT title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
																style="width:100%" method="=" readonly disabled AllowNull="<%=strAllowNull%>"
																id="<%=strColumnEName%>"  multiple size="3">
																	<option></option>
																	<%=RetrieveDropDownList(EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml,EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData)%>
																</SELECT>
															<%	
																//iEachColsCountEnum =iEachColsCountEnum+1;
																break;
																
															case "3": //Radio ��ѡ
															%><%=RetrieveRadioCheckList(strColumnEName,"Radio",EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml,EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData, strNoChange)%>
																<%
																break;
															case "4"://Check ��ѡ
															%><%=RetrieveRadioCheckList(strColumnEName,"CheckBox",EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml,EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData, strNoChange)%>
															
														<%
															break;
														}
														 %>
													
													
												<%
												break;
											case "6":
												%>
													<select title="<%=strColumnCName%>" keyIndex="<%=strKeyIndex%>" NoChange="<%=strNoChange%>"
													size="1" style="width:100%" method="=" readonly disabled AllowNull="<%=strAllowNull%>" 
													id="<%=strColumnEName%>" ColumnDefault="<%=RetrieveDefaultValue(EachSubTD,NodeTrBodyList)%>">
													<%=RetrieveUserInfo(EachSubTD.SelectSingleNode("Dictionary/@DataSource").InnerXml,EachSubTD.SelectSingleNode("Dictionary/@DataSourceName").InnerXml,strColumnPrecision,strTdData)%>
													</select>
												<%
												break;
											default:
												break;
										}
										%>
										<%
										if(myFunction!=null&&myFunction.InnerXml.Trim()!="")
										{
											if(FunctionName!=""&&intFunctionRulesLength>5){
											%>
											</td><td width="10%" align="center">
												<input type="button" value="<%=FunctionName%>" style="width:<%=FunctionName.Length*15+5%>px" <%=FunctionButtonEvent%>></td>
											</tr>
											</table>
											<%}
										}//------------------------------------------------------------------------------------------------------
										%>
										
										
										
										
										
										
										<!--input type=text style="WIDTH: 100%"-->
									</td>
									<%}%>
									<td nowrap style="<%//=ExistRow?"FONT-WEIGHT: bold;":"FONT-WEIGHT: normal;"%>FONT-SIZE: 9pt"><a href="#" disabled NoChange="No"  onclick="AddNewSubMe(this,<%=ExistRow.ToString().ToLower()%>)"><%=(ExistRow&&SubButtons.IndexOf("Add")>0)?"����":"&nbsp;"%></a></td>
									<td nowrap style="<%//=ExistRow?"FONT-WEIGHT: bold;":"FONT-WEIGHT: normal;"%>FONT-SIZE: 9pt"><a href="#" disabled NoChange="No" onclick="DeleteSubMe(this,<%=ExistRow.ToString().ToLower()%>)"><%=(ExistRow&&SubButtons.IndexOf("Delete")>0)?"ɾ��":"&nbsp;"%></a></td>
								</tr>
								<%}%>
								</TABLE>
							</td>
						</tr>
					</TABLE>
					<%
				}
			}
			}
		%>
		
		
		</div>
		<%//Response.Write("<br>ʱ�䣺" + DateTime.Now + ":" + DateTime.Now.Millisecond);%>
		
				</form>
	</body>
</HTML>

<!--

if(NodeTrBodyList!=null&&NodeTrBodyList.Count>0)
{
	NodeData=NodeTrBodyList[0].SelectSingleNode("td[@Column='"+strColumnEName+"']");
	if(NodeData!=null)
		strTdData=NodeData.InnerXml;
}
-->

