<%@ Page Language="c#" AutoEventWireup="True" Inherits="Zhifang.Utilities.Query.InputBackup.InputOneTableTemplate"
    CodeBehind="InputOneTableTemplate.aspx.cs" %>

<%@ Import Namespace="System.Xml" %>
<html>
<head>
    <title>选择表字段</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <%if (cssFile.Trim() == "")
      {%>
    <link href="../../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet">
    <%}
      else
      {%>
    <link href="../<%=cssFile%>" type="text/css" rel="stylesheet">
    <%}%>

    <script language="javascript" src="../../js/dialog.js"></script>

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
			
			///如果在浏览状态，请退出
			if(Form1.hAction.value=="BAdd"||Form1.hAction.value=="BCopy"||Form1.hAction.value=="BBatch")
			{
				alert("信息保存之前，不能预览\请先编辑信息，并保存些记录");
				return
			}
			if(Form1.hAction.value=="BModify"||Form1.hAction.value=="Browse")
			{	
				window.open('inputBrowseNews.aspx?FilePath=' + strDataBase + "&FileName=" + document.all[strObj].value,'','width=500,height=600,scrollbars=1,resizable=yes,top=0,left=200');
			}
		}
		function EditNews(strObj,strDataBase,DefaultValue)
		{
			
			///如果在浏览状态，请退出
			if(Form1.hAction.value=="BBatch")
			{
				alert("批量增加记录不能使用相同的信息编辑,\r批量完成后可以单独编辑每条信息");
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
				if (r != '' && typeof(r) != 'undefined')
				{
					document.all[strObj].value=r;
				}
			}
		}
		function uploadFile(strObj,strUrl,strTarget,thisObj)
		{
			///如果在浏览状态，请退出
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
					var uploadTxt = "<a href='#' onclick='javascript:uploadFile("+strObj+","+deleteUrl+",'frmDataRun',"+thisObj+")'  NoChange='No'>上传</a>&nbsp;";
					
					var downTxt   = "<a href="+downUrl+" id="+strObj+"0a0  target='_blank' >下载</a>&nbsp;";
				
					var deleTxt = "<a id="+strObj+"1b1 href="+deleteUrl+" target='_blank'>删除</a>&nbsp;";
	
					var strLink=uploadTxt+downTxt+deleTxt;
                  //
				  thisObj.outerHTML="<input id="+thisObj.id +" title="+ thisObj.title +" keyIndex="+thisObj.keyIndex+" NoChange="+thisObj.NoChange+" method='=' disabled size='1' style='WIDTH:0px;HEIGHT:20px'  AllowNull="+thisObj.AllowNull+"  columnType='文件' databand value="+data+" />"+strLink ;
				   }
																
				}
				else
					alert('只读属性不能上传或新增前应保存');
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
		//所有INPUT,TEXT,TEXTAREA,SELECT,A元素可用
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
				//不是数字
				//obj.focus();
				//obj.select();
				//alert(digit + " 不是数字! 重新输入");
			//}
			
			if((event.keyCode >= 48) && (event.keyCode <= 57) || (event.keyCode ==46) || (event.keyCode==45) )
			{
				if(obj.value.indexOf(".")!=-1 && (event.keyCode ==46))
				{
					alert("无效的输入");
					return false;
				}
				if(event.keyCode==45 && obj.value!="")
				{
					alert("无效的输入");
					return false;
				}
				return true;
			}
			else
				alert('只能输入数字');
			return false;

		}
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
					alert('返回结果时出错，返回的参数个数不匹配！！！');
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
				///还有其他,多选，Radio, Checkbox等等;
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
    </script>

</head>
<body onload="return window_onload()" topmargin="0" leftmargin="0" rightmargin="0"
    bottommargin="0">
    <form id="Form1" name="Form1" method="post" target="frmDataRun" action="DataRun.aspx?<%=Request.ServerVariables["Query_String"]%>"
    onsubmit="CollectDataRun()">
    <div id="userdesin">
        <%=strContent%>
    </div>
    <input id="$$Desig_<%=modName%>" title="" style="width: 0px" disabled columndefault=""
        method="=" databand nochange="No" allownull="Yes" keyindex="Yes">
    <%if (Request.QueryString["btnid"] == "viewinfo")
      {     //在增加的时候启用%>

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
    <div class="biaoti" id="txtKeyIndexBatches" style="display: none">
    </div>
    </form>
</body>
</html>
