<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.InputBatch.BatchOpen" Codebehind="BatchOpen.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		    <title><%if(Request.QueryString["btnid"].ToString()=="BAdd")
                   Response.Write("批量增加数据BatchOpen");
			else if(Request.QueryString["btnid"].ToString()=="BModify")
                   Response.Write("批量修改/编辑数据BatchOpen");
			else if(Request.QueryString["btnid"].ToString()=="viewinfo")
                   Response.Write("批量浏览数据BatchOpen");
			else
			        Response.Write("");
			%></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<script>

var btnid = '<%=Request.QueryString["btnid"]%>';
var  modName = '<%=Request.QueryString["name"]%>';
function EnableElement(bClear,BKeyIndexEnable)
{
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
//所有INPUT,TEXT,TEXTAREA,SELECT,A元素可用
function CollectWhereClause(kids,bClear,BKeyIndexEnable)
{
	for(var i=0;i<kids.length;i++)
	{
		
		if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
		{
			//alert(kids[i].NoChange);
		    if (kids[i].NoChange == "No") //只读字段
		    {
		        if (kids[i].disabled == true)
		            kids[i].disabled = false;
		    }
		    else {
		        kids[i].style.backgroundColor='#eef7fd';
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
			    if (boolReplace && (kids[i].value == ""))//||kids[i].keyIndex=="Yes"
				{
					kids[i].value=kids[i].ColumnDefault;
	            }
	            if (kids[i].keyIndex == "Yes") {
	                kids[i].style.backgroundColor = '#e8e7e7';
	            }
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
    buttLastClicked=obj;
	switch(obj.id)
	{
	    case 'BSave':
	    
	        if (window.frames["Right"] != null
		    && window.frames["Right"].document != null
		    && window.frames["Right"].document.readyState == "complete")
	            ;
	        else {
	            alert('正在读取数据，请稍候再点击保存')
	            return;
	        }
	        obj.style.border = '#ccccff 0px outset';

	        var strActionButton = "";
	        var strActionName = "";
	        if (btnid == 'BAdd') {
	            strActionButton = "您正在批量增加增加新记录数据\r";
	            strActionName = "增加";
	        }

	        if (btnid == 'BModify') {
	            strActionButton = "您正在批量修改/编辑记录数据\r";
	            strActionName = "修改";
	        }

	        if (btnid.id == 'BBatch') {
	            strActionButton = "您正在批量增加数条新记录数据,保存会根据设置来产生批量结果\r";
	            strActionName = "批量增加";
	        }


	        ConfirmNotAllowNull(this.frames["Right"].Form1.childNodes);   //验证所有表单不为空

	        var notAllowNullObj = this.frames["Right"].Form1.document.all['hNotAllowNull'].value;
	        if (notAllowNullObj != '') {
	            alert(notAllowNullObj + "不能为空");
	            this.frames["Right"].Form1.document.all['hNotAllowNull'].value = '';

	            return;
	        }


	        var Bconfirm = confirm(strActionButton + "确认要 [" + strActionName + "] 以下数据吗？\r\r");

	        if (Bconfirm) {

	            DivSetVisible(true);
	            window.status = "正在批量操作！！！";
	            getSubMeDataRunStatList();
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

//批量修改各种操作
var iSubMeCount;
iSubMeCount = 0;
function getSubMeDataRunStat() {
    iSubMeCount++
    window.status += iSubMeCount;
    if (!AsycMainDataOk)
        ClockSubMeDataRun = window.setTimeout(getSubMeDataRunStat, 100);
    else {
        ClockSubMeDataRun = window.setTimeout(getSubMeDataRunStatList, 100);
        //window.clearTimeout(ClockSubMeDataRun);
    }
}

var ClockSubMeDataRow;
var iTimeOutInterval = 2500;

var strXmlSubMe = ""; //<Tables/>
function getSubMeDataRunStatList() {
//    if (window.frames["Right"] != null
//		&& window.frames["Right"].document != null
//		&& window.frames["Right"].document.readyState == "complete")
//        ;
//    else {
//        closeToolTip();
//        return;
    //    }
    //oToolTip.setCapture(true);
    oToolTip.setCapture(true);
    var divSubMe = window.frames["Right"].document.all["divSubsMe"];
    //alert(divSubMe.childNodes.length);
    if (divSubMe != null
					&& divSubMe.childNodes != null
					&& divSubMe.childNodes.length > 0) {
        //所有子表
        //debugger;
        for (var iSubMeTable = 0; iSubMeTable < divSubMe.childNodes.length; iSubMeTable++) {

            var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
            var root;
            var newElem;
            //xmlDoc.async = false;
            //xmlDoc.resolveExternals = false;
            xmlDoc.loadXML("<?xml version='1.0' encoding='GB2312'?><SubMes/>");
            root = xmlDoc.documentElement;
            
            var thisSubMeEName = divSubMe.childNodes[iSubMeTable].firstChild.firstChild.firstChild.title;

            oToolTip.firstChild.lastChild.previousSibling.innerHTML = divSubMe.childNodes[iSubMeTable].firstChild.firstChild.firstChild.outerHTML;
            var thisTableSubMeTableData = divSubMe.childNodes[iSubMeTable].firstChild.lastChild.firstChild.firstChild;
            //thisSubMeDataRow.style.backgroundColor = 'transparent';
                //alert(thisTableSubMeTableData.outerHTML);
            //return;
            //所有子表的数据操作行数
            var thisTableSubMeData = "";

            newElem = xmlDoc.createElement("SubMe");

            var AttrEName;
            AttrEName = xmlDoc.createAttribute("EName");
            AttrEName.value = thisSubMeEName;

            newElem.setAttributeNode(AttrEName);

            root.appendChild(newElem);

            iTimeOutMillsencond = 100;
            window.frames["Right"].document.all['hTxt'].innerText = "";

            for (var iSubMeRows = 1; iSubMeRows < thisTableSubMeTableData.rows.length; iSubMeRows++) {
                var thisSubMeDataRow=  thisTableSubMeTableData.rows[iSubMeRows];
                thisSubMeDataRow.style.backgroundColor = 'transparent';
                var boolClose = false;
                ClockSubMeDataRow = window.setTimeout("RunSubMeDataRow('" + thisTableSubMeTableData.id + "'," + iSubMeRows + "," + boolClose + ")", iTimeOutMillsencond);
                
            }
            if (iSubMeTable == divSubMe.childNodes.length - 1) {
                boolClose = true;
                iTimeOutMillsencond = iTimeOutMillsencond + iTimeOutInterval

                ClockSubMeDataRow = window.setTimeout("RunSubMeDataRow('" + thisTableSubMeTableData.id + "'," + iSubMeRows + "," + boolClose + ")", iTimeOutMillsencond);

            }
        }
    }
    else
        closeToolTip();
    AsycMainDataOk = false;
    oToolTip.releaseCapture();
}

function getSubMeDataRunStatList_bak() {
    //    if (window.frames["Right"] != null
    //		&& window.frames["Right"].document != null
    //		&& window.frames["Right"].document.readyState == "complete")
    //        ;
    //    else {
    //        closeToolTip();
    //        return;
    //    }
    //oToolTip.setCapture(true);
    oToolTip.setCapture(true);
    var divSubMe = window.frames["Right"].document.all["divSubsMe"];
    //alert(divSubMe.childNodes.length);
    if (divSubMe != null
					&& divSubMe.childNodes != null
					&& divSubMe.childNodes.length > 0) {
        //所有子表
        //debugger;
        var iTimeOutMillsencond;
        iTimeOutMillsencond = 100;

        var iTimeInterval = 500;



        for (var iSubMeTable = 0; iSubMeTable < divSubMe.childNodes.length; iSubMeTable++) {

            var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
            var root;
            var newElem;
            //xmlDoc.async = false;
            //xmlDoc.resolveExternals = false;
            xmlDoc.loadXML("<?xml version='1.0' encoding='GB2312'?><SubMes/>");
            root = xmlDoc.documentElement;

            var thisSubMeEName = divSubMe.childNodes[iSubMeTable].firstChild.firstChild.firstChild.title;

            oToolTip.firstChild.lastChild.previousSibling.innerHTML = divSubMe.childNodes[iSubMeTable].firstChild.firstChild.firstChild.outerHTML;
            var thisTableSubMeTableData = divSubMe.childNodes[iSubMeTable].firstChild.lastChild.firstChild.firstChild;
            //alert(thisTableSubMeTableData.outerHTML);
            //return;
            //所有子表的数据操作行数
            var thisTableSubMeData = "";

            newElem = xmlDoc.createElement("SubMe");

            var AttrEName;
            AttrEName = xmlDoc.createAttribute("EName");
            AttrEName.value = thisSubMeEName;

            newElem.setAttributeNode(AttrEName);

            root.appendChild(newElem);


            for (var iSubMeRows = 1; iSubMeRows < thisTableSubMeTableData.rows.length; iSubMeRows++) {

                var boolClose = false;

                var thisSubMeDataRow = thisTableSubMeTableData.rows[iSubMeRows];
                //thisSubMeDataRow.lastChild.firstChild.style
                var strAction = "";
                if (thisSubMeDataRow.lastChild.firstChild != null)
                    strAction = thisSubMeDataRow.lastChild.firstChild.innerHTML;
                
                //alert(strAction);
                if (strAction != "" && strAction != "&nbsp;" && strAction != "删除") {
                    iTimeOutMillsencond = iTimeOutMillsencond + iTimeOutInterval;
                    ClockSubMeDataRow = window.setTimeout("RunSubMeDataRow('" + thisTableSubMeTableData.id + "'," + iSubMeRows + "," + boolClose + ")", iTimeOutMillsencond);
                    //Table,tr,td ..s.s.s.s.
                    hDataRun = "";
                    CollectDataRunListSubMe(thisSubMeDataRow.cells);
                    //alert(hDataRun);

                    newEleTR = xmlDoc.createElement("tr");

                    var AttrEName;
                    AttrEName = xmlDoc.createAttribute("RunAction");
                    AttrEName.value = strAction;

                    newEleTR.setAttributeNode(AttrEName);
                    if (strAction == "取消删除" || strAction == "取消修改") {
                        AttrEName = xmlDoc.createAttribute("RunIndex");
                        AttrEName.value = thisSubMeDataRow.cells[0].firstChild.title + "=" + thisSubMeDataRow.cells[0].firstChild.value;

                        newEleTR.setAttributeNode(AttrEName);
                    }

                    newEleTR.text = hDataRun;

                    newElem.appendChild(newEleTR);
                }
                else {
                    continue;
                }

                window.frames["Right"].document.all['hTxt'].innerText = root.xml;
                //分步骤保存

            }
            if (iSubMeTable == divSubMe.childNodes.length - 1) {
                boolClose = true;
                iTimeOutMillsencond = iTimeOutMillsencond + iTimeOutInterval

                ClockSubMeDataRow = window.setTimeout("RunSubMeDataRow('" + thisTableSubMeTableData.id + "'," + iSubMeRows + "," + boolClose + ")", iTimeOutMillsencond);

            }
        }
        //window.frames["Right"].document.all["hDataCollectionSubMes"].value = root.xml;
        window.frames["Right"].document.all['hTxt'].innerText = root.xml;
        //alert(root.xml);
        //alert(window.frames["ContentMain"].frames["Right"].document.all["hDataCollectionSubMes"].value);
    }
    else
        closeToolTip();
    AsycMainDataOk = false;
    oToolTip.releaseCapture();
}

//收集子表数据
function CollectDataRunListSubMe(kids) {
    for (var i = 0; i < kids.length; i++) {
        if (kids[i].nodeName.toUpperCase() == 'INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
        {
            switch (kids[i].type.toUpperCase()) {
                case "TEXT":
                    //case "HIDDEN"://重大问题，不能设置||kids[i].type.toUpperCase()=='HIDDEN'))
                    if (kids[i].value != "")
                    //hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].value;
                        hDataRun += "\t" + kids[i].id + kids[i].method + kids[i].value;
                    break;
                case "RADIO":
                    if (kids[i].checked != "")
                        hDataRun += "\t" + kids[i].name + kids[i].method + kids[i].value;
                    break;
                case "CHECKBOX":
                    if (hDataRun.indexOf(kids[i].name + kids[i].method) == -1) {
                        var checkBoxList = kids[i].parentNode.getElementsByTagName("INPUT"); //[kids[i].name];

                        var strCHKValues = "";
                        for (var iCHK = 0; iCHK < checkBoxList.length; iCHK++) {
                            if (checkBoxList[iCHK].checked)
                                strCHKValues += "," + checkBoxList[iCHK].value;
                            //hDataRun +="\n" +  kids[i].name + kids[i].method + kids[i].value;
                        }
                        if (strCHKValues.length > 0)
                            strCHKValues = strCHKValues.substr(1);
                        if (strCHKValues.length > 0) //是不是要修改?
                            hDataRun += "\t" + kids[i].name + kids[i].method + strCHKValues; ///这里才加入数据
                    }
                    break;
                default:
                    break;
            }

        }

        //============收集TextArea============
        if (kids[i].nodeName.toUpperCase() == 'TEXTAREA') {
            if (kids[i].value != "") {
                //var txtValue = kids[i].value.replace(/[\r][\n]/g, "▲");
                //alert(txtValue);
                hDataRun += "\t" + kids[i].id + kids[i].method + kids[i].value;
            }
        }
        //-----------------End----------------

        if (kids[i].nodeName.toUpperCase() == 'SELECT') {
            var selOptions = kids[i].options;
            var strCHKValues = "";
            for (var iCHK = 0; iCHK < selOptions.length; iCHK++) {
                if (selOptions[iCHK].selected)
                    strCHKValues += "," + selOptions[iCHK].text;
                //hDataRun +="\n" +  kids[i].name + kids[i].method + kids[i].value;
            }
            if (strCHKValues.length > 0)
                strCHKValues = strCHKValues.substr(1);
            if (strCHKValues.length > 0)
                hDataRun += "\t" + kids[i].id + kids[i].method + strCHKValues; ///这里才加入数据

            //if(kids[i].options[kids[i].selectedIndex].text!="")
            //	hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].options[kids[i].selectedIndex].text;
        }
        if (kids[i].hasChildNodes)
            CollectDataRunListSubMe(kids[i].childNodes);
    }
}

function closeToolTip() {
    DivSetVisible(false);
    oToolTip.style.display = "none";
    oToolTip.firstChild.lastChild.innerHTML = "";
    oToolTip.firstChild.lastChild.previousSibling.innerHTML = "完成";
}



var iTimeOutMillsencond;
iTimeOutMillsencond = 100;

var iTimeInterval = 1500;

function RunSubMeDataRow(objSubMeDataTable_id, iDataTableRow, boolClose) {
    var divSubMe = window.frames["Right"].document.all["divSubsMe"];
    //document.all["oToolTip"].style.display = '';
    DivSetVisible(true);
    
    if (window.frames["frmDataRun"].document != null && window.frames["frmDataRun"].document.readyState == "complete") {
        if (boolClose) {
            DivSetVisible(false);
        }
        else {
            var thisSubMeEName = divSubMe.childNodes[0].firstChild.firstChild.firstChild.title;

            oToolTip.firstChild.lastChild.previousSibling.innerHTML = divSubMe.childNodes[0].firstChild.firstChild.firstChild.outerHTML;
            var thisTableSubMeTableData = divSubMe.childNodes[0].firstChild.lastChild.firstChild.firstChild;

            var objDataTableSubMe = thisTableSubMeTableData; //window.frames["Right"].document.getElementById(objSubMeDataTable_id);
            var objTableRowsAll = objDataTableSubMe.rows;
            var objDataTableRow = objTableRowsAll[iDataTableRow];
            var strAction = "";
            strAction = objDataTableRow.lastChild.innerHTML;
            oToolTip.firstChild.lastChild.innerHTML = "" + objDataTableRow.rowIndex + ":" + objDataTableRow.lastChild.innerHTML;
            //window.clearTimeout(ClockSubMeDataRow);
            //window.frames["frmDataRun"].Form1.submit();


            var xmlDoc = new ActiveXObject("Msxml2.DOMDocument");
            var root;
            var newElem;
            //xmlDoc.async = false;
            //xmlDoc.resolveExternals = false;
            xmlDoc.loadXML("<?xml version='1.0' encoding='GB2312'?><SubMes/>");
            root = xmlDoc.documentElement;

            
            //alert(thisTableSubMeTableData.outerHTML);
            //return;
            //所有子表的数据操作行数
            var thisTableSubMeData = "";

            newElem = xmlDoc.createElement("SubMe");

            var AttrEName;
            AttrEName = xmlDoc.createAttribute("EName");
            AttrEName.value = thisSubMeEName;

            newElem.setAttributeNode(AttrEName);

            root.appendChild(newElem);

            var thisSubMeDataRow = objDataTableRow;
            //thisSubMeDataRow.lastChild.firstChild.style
            var strAction = "";
            if (thisSubMeDataRow.lastChild.firstChild != null)
                strAction = thisSubMeDataRow.lastChild.firstChild.innerHTML;
            //alert(strAction);
            if (strAction != "") {// && strAction != "&nbsp;" && strAction != "删除"
                iTimeOutMillsencond = iTimeOutMillsencond + iTimeOutInterval;
                //ClockSubMeDataRow = window.setTimeout("RunSubMeDataRow('" + thisTableSubMeTableData.id + "'," + iDataTableRow + "," + boolClose + ")", iTimeOutMillsencond);
                //Table,tr,td ..s.s.s.s.
                hDataRun = "";
                CollectDataRunListSubMe(thisSubMeDataRow.cells);
                if(hDataRun.indexOf('\t')==0)
                {
                    hDataRun=hDataRun.substr(1);
                }
                //alert(hDataRun);

                newEleTR = xmlDoc.createElement("tr");

                var AttrEName;
                AttrEName = xmlDoc.createAttribute("RunAction");
                AttrEName.value = strAction;

                newEleTR.setAttributeNode(AttrEName);
                if (strAction == "取消修改") {//strAction == "取消删除" || 
                    AttrEName = xmlDoc.createAttribute("RunIndex");
                    AttrEName.value = thisSubMeDataRow.cells[0].firstChild.title + "=" + thisSubMeDataRow.cells[0].firstChild.value;

                    newEleTR.setAttributeNode(AttrEName);
                

                    newEleTR.text = hDataRun;

                    newElem.appendChild(newEleTR);

                    //AssessorXpath,DataXPath
				    //http://localhost/oa2008/dbquery/Input/InputOneTableMain.aspx
				    //?btnid=viewinfo&TableCName=%bf%aa%b7%a2%c8%ce%ce%f1%b9%dc%c0%ed
				    //&TableEName=DevelopmentManage&db=DevelopmentManage&name=ZF%bf%aa%b7%a2%b1%e4%b8%fc
				    //&ControlRights=no&RBACModuleID=460&ModuleID=460
				    //&DataXPath=/*|*/DevelopmentManage.%b1%e0%ba%c5=3
				    //&AssessorXpath=/*|*/DevelopmentManage.%b1%e0%ba%c5=3
                    
                    var strAct = '../input/DataRun.aspx?';
                    strAct += '&<%=Request.ServerVariables["Query_String"].ToString().Replace("'","\\'")%>'
				        + '&DataXPath=/*|*/' + hTableEName.value + '.' + AttrEName.value
				        + '&AssessorXpath=' + '';
				    thisSubMeDataRow.style.backgroundColor = '#cdcdcd';
                    //document.all["oToolTip"].style.display = 'none';
                    DivSetVisible(false);
                    window.frames["Right"].Form1.action=strAct;
                    window.frames["Right"].Form1.hQueryCollection.value=hDataRun;
                    window.frames["Right"].Form1.submit();
                    window.status = "批量修改";
                    document.all['lblMSG'].innerHTML="["+AttrEName.value+"] 修改完成,可以继续修改或选择关闭窗口退出";
                    objDataTableRow.lastChild.firstChild.innerHTML='';
                }

            }
            //window.frames["Right"].document.all['hTxt'].innerText += "" + iDataTableRow + "" + root.xml + "\n\r     ";
        }
    }
    else
        window.setTimeout("RunSubMeDataRow('" + objSubMeDataTable_id + "'," + iDataTableRow + "," + boolClose + ")", 200)
}
function ReturnState()
{
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
					if(kids[i].value==""&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
					{
						kids[i].select();
						kids[i].focus();
						kids[i].style.backgroundColor='Coral';
						this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
						//alert(kids[i].title);
						return true;
					}
					else if(kids[i].id!=null&&kids[i].id.length>0)
					{
						kids[i].style.backgroundColor='transparent';
					}
					break;
				case "RADIO":
					
					break;
				case "CHECKBOX":
					//if((kids[i].options.length==0||kids[i].options[kids[i].selectedIndex].text=="")&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
					if(kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
					
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
				//alert(kids[i].title);
				return;
			}
			else if(kids[i].id!=null&&kids[i].id.length>0)
			{
				kids[i].style.backgroundColor='transparent';
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
			else if(kids[i].id!=null&&kids[i].id.length>0)
			{
				kids[i].style.backgroundColor='transparent';
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
			if(kids[i].databand!=null)                   //判断元素是否绑订到数据库
			{
			    //alert(kids[i].type.toUpperCase() + ":" + kids[i].id + kids[i].method + kids[i].value);
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
						 
							var checkBoxList=window.frames["Right"].Form1.document.all[kids[i].name];//window.frames["ContentMain"].frames["Right"].Form1.document.all[kids[i].name];
							// edit by 邓晓  07/07/04
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
	//CollectDataRunList(myForm.document.all['TableData'].childNodes);
	CollectDataRunList(myForm.childNodes);
	if(hDataRun.indexOf('\t')==0)
    {
        hDataRun=hDataRun.substr(1);
    }
    if(hDataRun.indexOf('\t')==0)
    {
        hDataRun=hDataRun.substr(1);
    }
    alert(hDataRun);
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

function testAlert(obj) {
    if (window.frames["Right"].document.readyState == "complete") {
        window.opener.location.reload();
        var frmFromListWindow = window.opener.frames["Right"];
        if (frmFromListWindow) {
            frmFromListWindow.parent.Form1.submit();
            //alert('a');
            //debugger;
            //alert("Right=" + frmFromListWindow.document.location.href);
            window.close();
        }
        else {
            var frmFromMainButtons = window.opener.frames["ContentMain"];
            if (frmFromMainButtons) {
                frmFromMainButtons.Form1.submit();
            //    alert('b');
            //    alert("ContentMain=" + frmFromMainButtons.document.location.href);
              window.close();
            }
        }
    }
    return;
    var strUrl = this.frames["Right"].location.href;
    if (strUrl.substr(strUrl.length - 1) == "#")
        strUrl = strUrl.substr(0, strUrl.length - 1);
    //this.frames["Right"].location=strUrl;//Form1.reset();
    obj.style.border = '#ccccff 0px outset';
    window.status = "取消操作！！！,已经返回查询页面！";

    if (opener != null && opener.document.getElementById(btnid) != null) {
        opener.document.getElementById(btnid).style.border = '#ccccff 0px outset';
    }

    
    window.close();
}			
		
function resizeFrm()
{
    //alert(document.body.clientHeight);
    //alert(document.all["Right"].style.height);
    var rightFrameHeight=window.frames["Right"].document.body.scrollHeight+30;
    if(rightFrameHeight>document.body.clientHeight-50){
        rightFrameHeight=document.body.clientHeight-50;
        }
    document.all["Right"].height=rightFrameHeight;
    
}	

function DivSetVisible(state) {            
    var DivRef = document.getElementById('oToolTip');
    var IfrRef = document.getElementById('DivShim');
    if (state) {
        DivRef.style.display = "block";
        IfrRef.style.width = DivRef.offsetWidth;
        IfrRef.style.height = DivRef.offsetHeight;
        IfrRef.style.top = DivRef.style.top;
        IfrRef.style.left = DivRef.style.left;
        IfrRef.style.zIndex = DivRef.style.zIndex - 1;
        IfrRef.style.display = "block";
    }
    else {
        DivRef.style.display = "none";
        IfrRef.style.display = "none";
    }
} 
    </script>
    <script language=javascript for="hLoaded" event="onpropertychange">
		
		switch(btnid)
		{
			case 'BAdd':
				//alert(document.getElementsByName("hLoaded").value);
				//alert('ok');
				EnableElement(true,true);
				SetDefaultValue(true);
				break;
			case 'BModify':
				EnableElement(false,true);
				SetDefaultValue(true);
				resizeFrm();
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
		//document.all["downing"].style.visibility='hidden';
		//document.all["oToolTip"].style.display='none';
		//$('#downing').css('visibility','hidden');
		DivSetVisible(false);
	</script>
	</HEAD>
	
	
	 <body MS_POSITIONING="GridLayout" topmargin=0 leftmargin=0 rightmargin=0 bottommargin=0>
		<form id="Form1" method="post" runat="server">
			<table width=100%>
	<tr>
	<TD vAlign="top">
					
					<iframe id="Right" name="Right" src="Default.aspx?<%=Request.ServerVariables["Query_String"]%>" frameBorder="0" width="100%"
							scrolling="auto" height="100%"></iframe>
		</TD>
	</tr>
	<tr height="26">
		<td align=center>
		<iframe src="WriteFile.aspx" id="WriteFile" height="30px" width=0></iframe>
		<img id="BSave" src="../image/middle/save.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							 onclick="FireQuery(this)" title="保存操作">	&nbsp;&nbsp;
					<img id="BCancel" src="../image/middle/cancel.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="取消操作"> </td>
	</tr>
	<tr height=15>
		<td align="left">
		    <label id="lblMSG" style="border: 1px solid #FF0000; font-weight:bold; color:#660033; width:100%;">提示：</label>
	    </td>
	</tr>
	</table>
		
	<input type=hidden name="hLoaded" id="hLoaded" value="0">
	<input type="hidden" id="RelocateListRow" name="RelocateListRow" value="0" />
    </form>
        <input type="hidden" id="hTableEName" name="hTableEName" value="<%=Request.QueryString["TableEName"]%>">
	    <DIV id="oToolTip" style="DISPLAY: ;POSITION:  absolute;background-image:url(../images/bg_adminTool.gif); z-index:100;left:300;top:200;">
			<div style="z-index:100;BORDER-RIGHT: black 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 2px solid; PADDING-LEFT: 10px; FILTER: progid:DXImageTransform.Microsoft.Gradient(GradientType=0, StartColorStr=skyblue, EndColorStr=#FFFFFF); LEFT: 0px; PADDING-BOTTOM: 10px; FONT: 10pt tahoma; BORDER-LEFT: black 2px solid; WIDTH: 170px; PADDING-TOP: 10px; BORDER-BOTTOM: black 2px solid; TOP: 0px; HEIGHT: 120px">
				<b>正在操作:批量读取数据</b>
				<hr style=" z-index:100; BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
					SIZE="1">
				<marquee  behavior=alternate>:::请稍候:::</marquee>
				<b></b>
				<div></div>
			</div>
		</DIV>
		<iframe id="frmDataRun" name="frmDataRun" 
					src="" 
					frameBorder="0" width="100%"
						scrolling="auto" height="0"></iframe>
		<iframe id="DivShim" src="" scrolling="no" frameborder="0" style="position: absolute;  left:300;top:200; display:; z-index:99; filter=progid: DXImageTransform.Microsoft.Alpha(style=0,opacity=0);">
    </iframe>
	</body>
</HTML>

