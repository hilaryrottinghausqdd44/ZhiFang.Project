var id;

//自动调整框架的大小为页面的大小
function iframeAutoFit()
{
	try
	{
		if(window!=parent)
		{
			var a = parent.document.getElementsByTagName("IFRAME");
			for(var i=0; i<a.length; i++) 
			{
				if(a[i].contentWindow==window)
				{
					//自适应高度
					var h1=0, h2=0, d=document, dd=d.documentElement;
					a[i].parentNode.style.height = a[i].offsetHeight +"px";
					a[i].style.height = "10px";

					if(dd && dd.scrollHeight) h1=dd.scrollHeight;
					if(d.body) h2=d.body.scrollHeight;
					var h=Math.max(h1, h2);
					if(h < 40)
						h = 40;

					if(document.all) {h += 16;}//4
					if(window.opera) {h += 12;}//1
					a[i].style.height = a[i].parentNode.style.height = h +"px";
					
					//自适应宽度
					var W1=0, W2=0;//, d=document, dd=d.documentElement;
					//a[i].parentNode.style.width = a[i].offsetWidth +"px";
					//a[i].style.width = "10px";

					if(dd && dd.scrollWidth) W1=dd.scrollWidth;
					if(d.body) W2=d.body.scrollWidth;
					var W=Math.max(W1, W2);

					if(document.all) {W += 24;}
					if(window.opera) {W += 21;}
					//a[i].style.width = a[i].parentNode.style.width = W +"px";
				}
			}              
		}
	}
	catch (ex){}
}

//重新调整窗口的大小使窗口的大小正好能容下整个页面,并将窗口居中显示(主要是重新设置高度和窗口的Top)
function resetWindowSize()
{
	//alert("resetWindowSize");
	var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
	if(allFrame.length != 1)
	{
	    //window.alert("noooo");
		return;
	}
	var mainFrame = allFrame[0];
	var h1=0, h2=0, d=document, dd=d.documentElement;
	//自适应高度
	mainFrame.parentNode.style.height = mainFrame.offsetHeight +"px";
	if(dd && dd.scrollHeight) h1=dd.scrollHeight;
	if(d.body) h2=d.body.scrollHeight;
	var h=Math.max(h1, h2);
	h += 48;
	window.dialogHeight = h + "px";
	//自适应宽度
	var w1=0,w2=0;
	mainFrame.parentNode.style.width = mainFrame.offsetWidth +"px";
	if(dd && dd.scrollWidth) w1=dd.scrollWidth;
	if(d.body) w2=d.body.scrollWidth;
	var w=Math.max(w1, w2);
	w += 24;
	window.dialogWidth = w + "px";
	//左上角位置
	var top = (window.screen.height - h) / 2;
	window.dialogTop = top + "px";
	//左边的位置
	var left = (window.screen.width - w) / 2;
	window.dialogLeft = left + "px";
}


        //重新设置用window.open打开的窗口的大小，并将窗口居中显示：
        //（主要是重新设置窗口的高度）
        //页面的宽度是初始打开的页面的：取窗口的body的宽度 + 48
        //页面的高度要根据页面的大小适当进行调整，一般是使用最好一个
        //页面控件的id所在的页面的绝对位置offsetTop值，加上这个控件所在的父控件里的位置，
        //再加96，这样窗口的高度在不大于屏幕的高度时，才不会有上下的滚动条
		function resetWindowSizeForUseWindowOpen(pageBottomControlID)
		{
		    //取最后一个控件ID所在的页面的绝对位置
		    var e=window.document.getElementById(pageBottomControlID);
            var t = e.offsetTop; //作为页面的高度
            var l = e.offsetLeft;  
            while(e = e.offsetParent) 
            {  
                t += e.offsetTop;
                l += e.offsetLeft;
            }
            t += 96;
		    //屏幕大小
		    var screenW = window.screen.width;
		    var screenH = window.screen.height;
		    //页面大小
		    var pageW = 48 + window.document.body.offsetWidth;//宽度
		    //alert(pageW);
		    var pageH = t;//高度
	        //上边位置
	        var top = (screenH - pageH) / 2;
	        //左边位置
	        var left = (screenW - pageW) / 2;
	        //左上角坐标（左边，上边）
		    window.moveTo(left, top);
	        //窗口大小（宽度、高度）
		    window.resizeTo(pageW, pageH);
		}


//从CheckBoxList控件中获取选择的值，返回value，用指定的分隔符分隔
//参数说明
//checkBoxListID：控件的ID
//spitChar：分隔符
function getCheckBoxListSelectText(checkBoxListID, spitChar)
{
    var allNum = window.document.getElementById(checkBoxListID).getElementsByTagName("input").length
    var selectValue = "";
    for(var i=0;i<allNum;i++)
    {
        var controlID= checkBoxListID + "_" + i;
        var obj = window.document.getElementById(controlID);
        if(obj == null)
        {
            continue;
        }
        if(obj.checked)
        {
	        selectValue += obj.parentElement.innerText + spitChar;
        }      
    }
    return selectValue;
}
//从CheckBoxList控件中获取选择的值，返回value，用指定的分隔符分隔
//参数说明
//checkBoxListID：控件的ID
//spitChar：分隔符
function getCheckBoxListSelectValue(checkBoxListID, spitChar)
{
    var cblObjID = window.document.all(checkBoxListID);
    var cblObj = cblObjID.getElementsByTagName("input");
    var allNum = cblObj.length
    var selectValue = "";
    for(var i=0;i<allNum;i++)
    {
        var controlID= checkBoxListID + "_" + i;
        var obj = document.getElementById(controlID);
        if(obj == null)
        {
            continue;
        }
        if(obj.checked)
        {
	        selectValue += obj.value + spitChar;
	        //selectValue += cblObj[i].value + spitChar;
        }      
    }
    return selectValue;
}
//从RadioButtonList控件中获取选择的值，返回Text
//参数说明
//radioButtonListID：RadioButtonList控件ID
function getRadioButtonListSelectText(radioButtonListID)
{
    var allNum = document.all(radioButtonListID).length
    var selectValue = "";
    for(var i=0;i<allNum;i++)
    {
        var controlID="rblSelectOne_"+i;
        var obj = document.getElementById(controlID);
        if(obj == null)
        {
            continue;
        }
        if(obj.checked)
        {
	        selectValue = obj.parentElement.innerText;
	        break;
        }      
    }
    return selectValue;
}
//从RadioButtonList控件中获取选择的值，返回value
//参数说明
//radioButtonListID：RadioButtonList控件ID
function getRadioButtonListSelectValue(radioButtonListID)
{
    var allNum = document.all(radioButtonListID).length
    var selectValue = "";
    for(var i=0;i<allNum;i++)
    {
        var controlID="rblSelectOne_"+i;
        var obj = document.getElementById(controlID);
        if(obj == null)
        {
            continue;
        }
        if(obj.checked)
        {
	        selectValue = obj.value;
	        break;
        }      
    }
    return selectValue;
}


//获取当前机器的时钟,返回:日期 + " " + 时间(精确到秒)
function getServerTime()
{
    //获取当前机器的时钟,返回是格林威治时间
    var timeServer = new Date();
    //转换为本地日期和时间
    var date = timeServer.toLocaleDateString();
    var time = timeServer.toLocaleTimeString();
    return date + ' ' + time;
}


//获取机器IP地址
function getClientIP()
{
    //var wsh = new ActiveXObject("WScript.Shell"); 
    //wsh.Run("ipconfig /all > C:\\ip.txt"); 
    var fso = new ActiveXObject("Scripting.FileSystemObject");
    var f = fso.OpenTextFile("C:\ip.txt"); 
    var s = f.ReadAll(); 
    f.Close(); 
    var ip = s.match(/IP Address(\. )+\: ((\d{1,3}\.){3}(\d{1,3}))/i)[2];
    alert(ip);
    return ip;
}



/*
*######################################
* 
*
* 以下几个方法是根框架相互之间的调用有关
* 
*
*######################################
*/


//刷新与当前框架相关联的框架(参数为查询条件)
function RefreshRelationModalData(pkSQL, opid)
{
    //alert(pkSQL + "  opid=" + opid);
	//取当前的框架的id(因为只刷新跟当前选择内容有关联的框架)
	var currentId = window.frameElement.name;
	var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
	for (var i=0; i < allFrame.length; i++)
	{
		//框架的id
		var id = allFrame[i].id;
		if(currentId != id)//不是当前选中的框架,继续下一个框架
		{
			continue;
		}
		//alert("当前模块为:" + currentId);
		//取url
		var url = allFrame[i].src;
		//alert(url);
		//取关联的子模块名称
		var relationModalName = getParamValueFromUrl(url, "relationModalName");
		if(relationModalName == "")
			continue;
		//alert(relationModalName);
		//拆分
		var relModalList = relationModalName.split(",");
		//alert(relModalList.length);
		for(var j=0;j<relModalList.length; j++)
		{
			var relationIframeID = relModalList[j];
			//alert("刷新的关联的子模块为:" + relationIframeID);
			makeAndResetIframeUrl(pkSQL, relationIframeID, opid);
		}
	}
}

//从URL里获取指定的参数对应的值
function getParamValueFromUrl(url, getParamName)
{
	//从 url 中取 参数配置情况
	var splitUrl = url.split("?");
	var splitParams = splitUrl[1].split("&");//取到参数列表并拆分
	for(var i=0;i<splitParams.length;i++)
	{
		var splitParam = splitParams[i].split("=");//拆分当前参数
		var paramName = splitParam[0];
		var paramValue = splitParams[i].replace(paramName + "=", "");
		if(paramName == getParamName)
			return paramValue;
	}
	return "";
}

//根据指定条件,查询指定的框架
function makeAndResetIframeUrl(pkSQL, iframeID, opid)
{
	iframeUrl = "";
	var allFrame = window.parent.document.body.getElementsByTagName("IFRAME");
	var selectIFRAME = 0;//保存选中的框架ID
	for (var i=0; i < allFrame.length; i++)
	{
		//找到框架
		if(allFrame[i].id == iframeID)
		{
			iframeUrl = window.parent.document.frames(i).location.href;
			selectIFRAME = i;
			break;
		}
	}
	if (iframeUrl == "")
	{
		return "";
	}
	
	//从 url 中取 参数配置情况
	var splitUrl = iframeUrl.split("?");
	//页面名称
	var pageName = splitUrl[0];
	//页面参数
	var splitParams = splitUrl[1].split("&");//取到参数列表并拆分
	//定义参数
	var para = "";//应用系统的固定参数列表,用逗号分隔
	var modalName = "";//模块名称
	var modalID = "";//模块标识
	var relationModalName = "";//关联的模块名称
	for(var i=0;i<splitParams.length;i++)
	{
		var splitParam = splitParams[i].split("=");//拆分当前参数参数
		var paramName = splitParam[0];
		//var paramValue = splitParam[1];
		var paramValue = splitParams[i].replace(paramName + "=", "");
		if(paramName == "para") para = paramValue;//对应的应用系统名称
		else if(paramName == "modalName") modalName = paramValue;
		else if(paramName == "modalID") modalID = paramValue;
		else if(paramName == "relationModalName") relationModalName = paramValue;
	}
	//转义查询表达式
	pkSQL = pkSQL.replace(/&apos;/g,"'");
	//参数列表
    var paramUrl = "para=" + para + "&pkSQL=" + pkSQL  + "&opid=" + opid + "&relationModalName=" + relationModalName;
	//框架的URL
	var newUrl = pageName + "?" + paramUrl;
	//把URL设回去
	window.parent.document.frames(selectIFRAME).location.href = newUrl;
	return relationModalName;
}



//打开窗口:按指定的位置,大小参数继续打开
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
				case "左":
					PositionLeft=0;
					break;
				case "中":
					PositionLeft=window.screen.availWidth * (100-iPositionWidth)/200;
					PositionLeft=Math.round(PositionLeft);
					//alert(PositionHeight);
					break;
				case "右":
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
				case "上":
					PositionTop=0;
					break;
				case "中":
					PositionTop=window.screen.availHeight * (100-iPositionHeight)/200;
					PositionTop=Math.round(PositionTop);
					break;
				case "下":
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
			openWinPositionSize(url,sTarget,PositionLeft,PositionTop,PositionWidth,PositionHeight);
		}
		catch(e)
		{
			alert(e);
		}
	}
}			

function openWinPositionSize(url,sTarget, ileft,itop,iwidth,iheight)
{
	var mywin=window.open(url,"" + sTarget,"toolbar=no,location=no,status=yes,menubar=no,resizable=yes,top="+itop +",left="+ileft +",width="+iwidth+"px,height="+iheight+"px,scrollbars=yes");
}






