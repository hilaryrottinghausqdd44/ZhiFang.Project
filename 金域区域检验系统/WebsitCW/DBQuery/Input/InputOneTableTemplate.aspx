<%@ Page language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.Input.InputOneTableTemplate" Codebehind="InputOneTableTemplate.aspx.cs" %>
<%@ Import Namespace="System.Xml" %>
<html>
	<HEAD>
		<title>ѡ����ֶ�</title>
		<META http-equiv="Content-Type" content="text/html; charset=gb2312">
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<%if(cssFile.Trim()==""){%>
		<LINK href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
		<%}else{%>
		<LINK href="../<%=cssFile%>" type="text/css" rel="stylesheet">
		<%}%>
		<script language="javascript" src="../js/dialog.js"></script>
		<!--#include file="../../Includes/JS/Calendar.js"-->
	
<script>
		function window_onload()
		{
			var buttLastClicked=parent.parent.buttLastClicked;
			
			if(buttLastClicked!=null&&(buttLastClicked.id=='BAdd'||buttLastClicked.id=='BCopy'||buttLastClicked.id=='BDelete'||buttLastClicked.id=='BModify'||buttLastClicked.id=='BBatch'))
			{
				buttLastClicked.style.border='#ccccff 0px outset';
				parent.parent.buttLastClicked=null;
			}
			
			if(parent!=null&&parent.document.getElementById("hLoaded")!=null)
				parent.document.all["hLoaded"].value="1";		
			//alert(parent.document.all["hLoaded"].value);
			//alert(window.parent.parent.document.getElementById("BAdd"));
			//document.getElementById("$$Desig").value="123455.html";
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
				window.open('inputBrowseNews.aspx?FilePath=' + strDataBase + "&FileName=" + document.all[strObj].value,'','width=500,height=600,scrollbars=1,resizable=yes,top=0,left=200');
			}
		}
		function EditNews(strObj,strDataBase,DefaultValue)
		{
			
			///��������״̬�����˳�
			if(Form1.hAction.value=="BBatch")
			{
				alert("�������Ӽ�¼����ʹ����ͬ����Ϣ�༭,\r������ɺ���Ե����༭ÿ����Ϣ");
				return;
			}
			if(Form1.hAction.value=="BAdd"||Form1.hAction.value=="BCopy")
			{
				document.all[strObj].value="";
			}
			if(Form1.hAction.value=="BModify"||Form1.hAction.value=="BAdd"||Form1.hAction.value=="BCopy")
			{	
				var r;
				
				r=window.showModalDialog('InputEditNewsDialog.aspx?DefaultValue='+DefaultValue+'&FilePath=' + strDataBase + '&FileName=' + document.all[strObj].value,'','dialogWidth:588px;dialogHeight:468px;resizable:yes;scroll:auto;status:no');
				if (r != '' && typeof(r) != 'undefined') {
				    //alert(r);
				    document.all[strObj].value = r;
				    //alert(doc/nt.all[strObj].value);
				    if (document.frames["frm" + strObj] != null)
				        document.frames["frm" + strObj].location = "inputBrowseNews.aspx?FilePath=" + strDataBase + "&FileName=" + r;
				}
			}
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
					var data = document.all[strObj].value;
					if(data!='')
					{
					
					var deleteUrl = "DeleteFile.aspx?file="+data;
					var downUrl = "DownLoadFile.aspx?File="+data;
					var uploadTxt = "<a href='#' onclick='javascript:uploadFile("+strObj+","+deleteUrl+",'frmDataRun',"+thisObj+")'  NoChange='No'>�ϴ�</a>&nbsp;";
					
					var downTxt   = "<a href="+downUrl+" id="+strObj+"0a0  target='_blank' >����</a>&nbsp;";
				
					var deleTxt = "<a id="+strObj+"1b1 href="+deleteUrl+" target='_blank'>ɾ��</a>&nbsp;";
	
					var strLink=uploadTxt+downTxt+deleTxt;
                  //
				  thisObj.outerHTML="<input id="+thisObj.id +" title="+ thisObj.title +" keyIndex="+thisObj.keyIndex+" NoChange="+thisObj.NoChange+" method='=' disabled size='1' style='WIDTH:0px;HEIGHT:20px'  AllowNull="+thisObj.AllowNull+"  columnType='�ļ�' databand value="+data+" />"+strLink ;
				   }
																
				}
				else
					alert('ֻ�����Բ����ϴ�������ǰӦ����');
			}
			
		}
		function func()
		{
			alert('ok');
		}
		var hDataRun="";
		function CollectDataRun()
		{
			//alert('a');
			
			hDataRun="";
			CollectDataRunList(document.getElementById("Form1").childNodes);
			document.getElementById("Form1").hQueryCollection.value=hDataRun;
			
			//alert(hDataRun);
			
			//Form1.submit(); onsubmit="alert('a');return false;"
			return true;
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
		//����INPUT,TEXT,TEXTAREA,SELECT,AԪ�ؿ���
		function CollectWhereClause(kids)
			{
				for(var i=0;i<kids.length;i++)
				{
					//alert('ok');
					if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
					{
								kids[i].disabled=true;
					}
					
					//==================TextArea=====================
					if(kids[i].nodeName.toUpperCase() =='TEXTAREA')
					{
								kids[i].disabled=true;
					}
					if(kids[i].nodeName.toUpperCase()=='SELECT')
					{
						kids[i].disabled=true;
					}
					if(kids[i].nodeName.toUpperCase()=='A')
					{
						kids[i].disabled=true;
					}
					if(kids[i].hasChildNodes)
						CollectWhereClause(kids[i].childNodes);
				}
			}
		function ReadAttribute(obj,strTitle,strKeyIndex,strNoChange,strAllowNull,strColumnDefault)
		{
			
			if(obj.title!=null||typeof(obj.title)!="undefined")
			{
				
				obj.title=strTitle;
			}
			if(obj.strKeyIndex!=null||typeof(obj.strKeyIndex)!="undefined")
			{
				obj.KeyIndex=strKeyIndex;
			}
			if(obj.NoChange!=null||typeof(obj.NoChange)!="undefined")
			{
				obj.NoChange=strNoChange;
			}
			if(obj.AllowNull!=null||typeof(obj.AllowNull)!="undefined")
			{
				obj.AllowNull=strAllowNull;
			}		
			if(obj.ColumnDefault!=null||typeof(obj.ColumnDefault)!="undefined")
			{
				
				obj.ColumnDefault=strColumnDefault;
			}	
		}
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

		//======================¼��ʱ�����ܵĴ������====================
		var gAllElementsValues = "";
		function CollectInParaValues(strElementTitle) {
		    if (strElementTitle == "")
		        return "";
		    gAllElementsValues = "";
		    varElementsArray = strElementTitle.split(",");
		    for (var iE = 0; iE < varElementsArray.length; iE++) {
		        CollectInParaValuesLoop(varElementsArray[iE], document.all['userdesin'].childNodes);
		    }
		    if (gAllElementsValues.length > 0)
		        gAllElementsValues = gAllElementsValues.substr(1);
		    return gAllElementsValues;
		}
		function CollectInParaValuesLoop(elementTitle, kids) {
		    for (var i = 0; i < kids.length; i++) {
		        if (kids[i].nodeName.toUpperCase() == "#TEXT")
		            continue;
		        if (kids[i].getAttribute('title') != null && kids[i].title == elementTitle) {
		            if (kids[i].nodeName.toUpperCase() == 'INPUT' && kids[i].type.toUpperCase() == 'TEXT') {
		                gAllElementsValues += "," + elementTitle + "=" + kids[i].value;
		            }
		            if (kids[i].nodeName.toUpperCase() == 'SELECT') {
		                if (kids[i].selectedIndex != -1)
		                    gAllElementsValues += "," + elementTitle + "=" + kids[i].options[kids[i].selectedIndex].text;
		            }
		            if (kids[i].nodeName.toUpperCase() == 'TEXTAREA') {
		                gAllElementsValues += "," + elementTitle + "=" + kids[i].value;
		            }
		            //��������,��ѡ��Radio, Checkbox�ȵ�;
		        }
		        if (kids[i].hasChildNodes)
		            CollectInParaValuesLoop(elementTitle, kids[i].childNodes);
		    }
		}
		//======================¼��ʱ�����ܵĴ������====================
		
		
	    var objElement;
		function setOutParaValues(strReturnValues,strElementTitle,strDefaultElementTitle)
		{
			//alert(strReturnValues + strElementTitle + strDefaultElementTitle);
			if(strElementTitle=="null")
				strElementTitle=strDefaultElementTitle;
			var setElements=strElementTitle.split(",");
			var setElementsValues=strReturnValues.split(",");
			if(setElements.length!=setElementsValues.length)
			{
				if(setElements.length>1)
				{
					alert('���ؽ��ʱ�������صĲ���������ƥ�䣡����');
					return;
				}
				else
				{
					setElementsValues=strReturnValues.split("\t");
				}
			}
			for(var iS=0;iS<setElements.length;iS++)
			{
				objElement=null;
				selectElementByTitle(setElements[iS],document.all['userdesin'].childNodes);
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

		function ReCollectInParaValues(strModulPath, strParas) {
		    var returnPath = strModulPath;
		    var arrParas = strParas.split(",");
		    for (var i = 0; i < arrParas.length; i++) {
		        var arrEachPara = arrParas[i].split("=");
		        //alert(arrEachPara);
		        if (arrEachPara.lengh < 2)
		            continue;
		        if (returnPath.indexOf("=" + arrEachPara[0]) > 0)
		            returnPath = returnPath.replace("=" + arrEachPara[0], "=" + arrEachPara[1]);

		    }
		    return returnPath;
		}
		
		
		var varOrgValue = "";
		function SetV(objID, v) {
		    //alert(objID);
		    $(objID).value = v;
		}
		function $(s) {
		    return document.getElementById ? document.getElementById(s) : document.all[s];
		}
		function keyPressDivLocal(objRun, OrgValue) {
		    if (parent == null)
		        return;
		    //showParentDiv(objRun);
		    var key = window.event.keyCode;
		    // alert('key=' + key);
		    //window.parent.keyPressdiv(key, window.name,objRun.id,OrgValue);window.parent.
		    keyPressdiv(key, window.name, objRun.id, OrgValue);
		}
		function showParentDiv(objRun) {
		    if (parent == null)
		        return;
		    var key = window.event.keyCode;

		    var tt = objRun;
		    var ttop = tt.offsetTop; //tt.offsetTop;
		    ttop += tt.offsetHeight;
		    var tleft = tt.offsetLeft;
		    while (tt = tt.offsetParent) { ttop += tt.offsetTop; tleft += tt.offsetLeft; }

		    //tWindow=window
		    //while (tWindow.parent!=null){ttop+=tWindow.offsetTop; tleft+=tWindow.offsetLeft;tWindow=tWindow.parent;}


		    //		    alert("window.offsetTop=" + parent.document.all['Right'].offsetTop 
		    //		        + ", parent.document.all['Right'].parentNode.offsetTop=" + parent.document.all['Right'].parentNode.offsetTop
		    //		        + ", parent.document.all['Right'].offsetTop=" + parent.document.all['Right'].offsetTop);
		    //            tt=parent.document.all['Right'].parentNode;
		    //            ttop +=tt.offsetTop;
		    //            while (tt = tt.offsetParent){ttop+=tt.offsetTop;}

		    var objTop = ttop; // + parent.document.all['Right'].parentNode.offsetTop;//document.body.scrollTop+objRun.offsetTop + objRun.offsetHeight;
		    var objLeft = tleft - 2; //document.body.scrollLeft+objRun.offsetLeft;

		    //window.parent.DivSetVisible(true,0,0,window.name,objRun.id);window.parent.
		    DivSetVisible(true, objTop, objLeft, window.name, objRun.id);
		}
		function closeParentDiv(objRun) {
		    if (parent == null)//window.parent.
		        return;
		    DivSetVisible(false, 0, 0, window.name, objRun.id);
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
		        DivSetVisible(false, 0, 0, '', '');
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
		    else {
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
		        else if (OrgValue.indexOf('=') > 0)//ԭʼֵ
		        {
		            OrgValue = OrgValue.substr(OrgValue.indexOf('=') + 1);
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
		    SetV(g_objID, str); //window.frames[g_iFrame].
		}

		function ClickSelectV(str) {
		    //alert(g_objID + "_" + str);
		    SetV(g_objID, str); //window.frames[g_iFrame].
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
</head>
<body onload="return window_onload()" topmargin=0 leftmargin=0 rightmargin=0 bottommargin=0  onclick="DivSetVisible(false,0,0,window.name,'');">
<form id="Form1" name="Form1" method="post" target="frmDataRun" action="DataRun.aspx?<%=Request.ServerVariables["Query_String"]%>" onsubmit="CollectDataRun()">
	<div id="userdesin">
		<%=strContent%>
	</div>
		<INPUT id="$$Desig_<%=modName%>" title="" style="WIDTH: 0px" disabled ColumnDefault="" method="=" databand NoChange="No" AllowNull="Yes" keyIndex="Yes">
	<%if(Request.QueryString["btnid"]=="viewinfo"){     //�����ӵ�ʱ������%>
		<script>
			
			CollectWhereClause(document.getElementById("userdesin").childNodes);
		</script>
	<%}%>
	


<%	
%>
						<!---->

			<input type="hidden" id="hQueryCollection" name="hQueryCollection" value="">
			<input type="hidden" id="hAction" name="hAction" value="Browse">
			<input type="hidden" id="txtBatches" name="txtBatches" value="">
			<input type="hidden" id="hSubTablesCopy" name="hSubTablesCopy" value="">
			<input type="hidden" id="hNotAllowNull" name="hNotAllowNull" value="">
			
		
			<br>
			<div class="biaoti" id="txtKeyIndexBatches" style="display:none"></div>
			
			
</form>
<div id="PopupDiv"  style="border: 1px solid #0000FF; background-position: #F2F5E0; position: absolute; cursor: hand; filter: Alpha(Opacity=90, FinishOpacity=50, Style=0, StartX=0, StartY=0, FinishX=100, FinishY=100);
        color:Black; padding: 0px; background:#F2F5E0; display:none; z-index: 100; width:300px; left:0px; top:0px">
    </div>
    <iframe id="framePopupDiv" style="position: absolute; Display:none; z-index: 99; width: 0; height: 0;
        top: 0; left: 0; scrolling: no;" frameborder="0" src=""></iframe>
</body>
</HTML>

