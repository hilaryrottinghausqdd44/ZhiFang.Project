<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.RBAC.Tools.BeginEndTime" Codebehind="BeginEndTime.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>阶段性时效设置</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <style type="text/css">
        <!
        -- .GrayBorder
        {
            border-top: 0px solid #999999;
            border-right: 1px solid #999999;
            border-bottom: 1px solid #999999;
            border-left: 1px solid #999999;
        }
        .Skyblue
        {
            border: 1px solid #00CC99;
        }
        -- ></style>

    <script language="javascript" id="clientEventHandlersJS">
	<!--
	var minutesBit=0;

	function AddTime() {
		//Table1.bgColor='red';
	}

	
function selAllTimes_onchange() {
	minutesBit=0;
	//alert(selAllTimes.options(selAllTimes.selectedIndex).value);
	var BeginEndTime=selAllTimes.options(selAllTimes.selectedIndex).value;
	if(BeginEndTime.indexOf('be')>=0)
	{
		BeginEndTime=BeginEndTime.substring(4);
		var BED=BeginEndTime.split(';');
		if(BED[0].indexOf(":")>=0)
		{
			BeginDate.value=BED[0].split(':')[0];
			EndDate.value=BED[0].split(':')[1];
		}
		else
		{
			alert('起止时间不正确');
			return;
		}
		if(BED.length>1)
		{	
			var BET=BED[1].split(':');
			selectBegins.options[parseInt(BET[0].split('-')[0])+1].selected=true;
			selectEnds.options[parseInt(BET[0].split('-')[1])+1].selected=true;
			//txtMinutes.options[parseInt(BET[0].split('-')[2]).selected=true;
			for(var i=0;i<txtMinutes.options.length;i++)
			{
				var option=txtMinutes.options[i];
				if(option.value==BET[0].split('-')[2])
				{
					option.selected=true;
					option.style.backgroundColor='skyblue';
					break;
				}
			}
			
			intTable();
		}
	}
	else
	{
		alert('这是重复性时间格式，或未知的不能解析的时间格式');
		selAllTimes.options[selAllTimes.selectedIndex].style.backgroundColor='gray';
	}
	
}

function selectBegins_onchange() {
	minutesBit=0;
	intTable();
	
}

function selectEnds_onchange() {
	minutesBit=0;
	intTable();
	
}
function txtMinutes_onchange() {
	minutesBit=0;
	intTable();
}
function intTable()
{
	if(selectBegins.selectedIndex>0&&selectEnds.selectedIndex>0&&selectBegins.selectedIndex<=selectEnds.selectedIndex)
	{
		tableTime.style.display="";
	}
	else
	{
		tableTime.style.display="none";
		return false;
	}
	
	
	
		var beginTime=selectBegins.selectedIndex-1;
		var endTime=selectEnds.selectedIndex-1
		if(beginTime>endTime)
		{
			alert('开始时间要小于结束时间');
			return false;
		}
		var minutesValue=txtMinutes.options[txtMinutes.selectedIndex].value;
		var minutes=parseInt(txtMinutes.options[txtMinutes.selectedIndex].value);
		
		
		var strAm="";
		var strAmMinutes="";
		if(beginTime<12)
		{
			var AMEnd=11;
			if(endTime<12)
				AMEnd=endTime;
			for(var i=beginTime;i<=AMEnd;i++)
			{
				strAm +="<TD align=\"center\" width=\"60px\" colspan=\""+60/minutes+"\" class=\"Skyblue\">"+i+"</TD>";
				for(var j=0;j<60/minutes;j++)
				{
					strAmMinutes +="<TD id=\"trAM"+(i) + "" + j 
					+"a\" width=\""+minutes+"px\" class=\"GrayBorder\" style=\"display:;\" onmousemove=\"checkBgColor(trAM"+(i) + "" + j 
					+"a)\">"+j*minutes+"</TD>";
				}
			
			}
			
			
			strAm ="<TR>" + strAm + "</TR>";
			strAm +="<TR>" + strAmMinutes + "</TR>";
			
			strAm="<table id=\"tableTimeAM\" align=\"left\" cellSpacing=\"0\" cellPadding=\"0\" border=\"0\">" + strAm;
			strAm=strAm+ "</table>";
			divtableTimeAM.innerHTML=strAm;
			
			}
		
		var strPm="";
		var strPmMinutes="";
		if(endTime>=12)
		{
			var PMBegin=12;
			if(beginTime>=12)
			{
				PMBegin=beginTime;
			}
			for(var i=PMBegin;i<=parseInt(endTime);i++)
			{
				strPm +="<TD align=\"center\" width=\"60px\" colspan=\""+60/minutes+"\" class=\"Skyblue\">"+i+"</TD>";
				for(var j=0;j<60/minutes;j++)
				{
					strPmMinutes +="<TD id=\"trPM"+(i) + "_" + j 
					+"a\" width=\""+minutes+"px\" class=\"GrayBorder\" style=\"display:;\"  onmousemove=\"checkBgColor(trPM"+(i) + "_" + j 
					+"a)\">"+j*minutes+"</TD>";
				}
			}
		}
		
		strPm ="<TR>" + strPm + "</TR>";
		strPm +="<TR>" + strPmMinutes + "</TR>";
		strPm="<table id=\"tableTimeAM\" align=\"left\" cellSpacing=\"0\" cellPadding=\"0\" border=\"0\">" + strPm
		strPm=strPm+ "</table>";
		divtableTimePM.innerHTML=strPm;
		return true;
}

function checkBgColor(obj)
{
	if(minutesBit==1)
	{
		obj.style.backgroundColor='skyblue';
	}
	else
	{
		obj.style.backgroundColor='white';
		//obj.innerHTML="&nbsp;&nbsp;";
	}
		
}

function divtableTimeAM_onmousedown() {
	minutesBit=1;
}

function divtableTimePM_onmousedown() {
	minutesBit=1;
}

function document_onmouseup() {
	minutesBit=0;
}



//-->
    </script>

    <script language="javascript" for="document" event="onmouseup">
<!--

return document_onmouseup()
//-->
    </script>

</head>
<body language='javascript"' ms_positioning="GridLayout" leftmargin="15" rightmargin="15"
    bottommargin="16" topmargin="16">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td align="left" width="100%" style="border-right: silver 1px solid; border-top: silver 1px solid;
                border-left: silver 1px solid; border-bottom: silver 1px solid">
                <table id="Table3" align="left" border="0">
                    <tr>
                        <td nowrap align="right">
                            开始日期：
                        </td>
                        <td style="width: 133px" nowrap>
                            <input id="BeginDate" style="width: 104px; height: 22px" onpropertychange="DTChanged = 1"
                                size="12" value="2004-11-17" name="BeginDate">
                        </td>
                        <td nowrap align="right">
                            结束日期：
                        </td>
                        <td nowrap>
                            <input id="EndDate" style="width: 120px" onpropertychange="DTChanged = 1" value="2004-11-24"
                                name="EndDate">
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            开始时间：
                        </td>
                        <td style="width: 133px" nowrap>
                            <select language="javascript" id="selectBegins" style="width: 72px" onchange="return selectBegins_onchange()"
                                name="selectBegins">
                                <option value="-1" selected>全天</option>
                                <option value="0">0</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                            </select>点钟
                        </td>
                        <td nowrap align="right">
                            结束时间：
                        </td>
                        <td nowrap>
                            <select id="selectEnds" style="width: 72px" name="selectEnds" language="javascript"
                                onchange="return selectEnds_onchange()">
                                <option value="-1" selected>全天</option>
                                <option value="0">0</option>
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="7">7</option>
                                <option value="8">8</option>
                                <option value="9">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                                <option value="13">13</option>
                                <option value="14">14</option>
                                <option value="15">15</option>
                                <option value="16">16</option>
                                <option value="17">17</option>
                                <option value="18">18</option>
                                <option value="19">19</option>
                                <option value="20">20</option>
                                <option value="21">21</option>
                                <option value="22">22</option>
                                <option value="23">23</option>
                            </select>点钟
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table id="tableTime" cellspacing="0" cellpadding="0" width="100%" border="0" style="border-right: silver 1px solid;
                    border-top: silver 1px solid; display: none; border-left: silver 1px solid; border-bottom: silver 1px solid">
                    <tr>
                        <td width="5">
                        </td>
                        <td align="left" width="95%">
                            <select id="txtMinutes" name="txtMinutes" language="javascript" onchange="return txtMinutes_onchange()">
                                <option value="1">1</option>
                                <option value="2">2</option>
                                <option value="3">3</option>
                                <option value="4">4</option>
                                <option value="5">5</option>
                                <option value="6">6</option>
                                <option value="10">10</option>
                                <option value="12">12</option>
                                <option value="15" selected>15</option>
                                <option value="20">20</option>
                                <option value="30">30</option>
                                <option value="60">60</option>
                            </select>
                            分钟分隔
                        </td>
                    </tr>
                    <tr>
                        <td width="5">
                            AM.
                        </td>
                        <td align="left" width="95%">
                            <div id="divtableTimeAM" align="left" language="javascript" onmousedown="return divtableTimeAM_onmousedown()">
                                <table id="tableTimeAM" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="Skyblue">
                                            1
                                        </td>
                                        <td class="Skyblue">
                                            2
                                        </td>
                                        <td class="Skyblue">
                                            3
                                        </td>
                                        <td class="Skyblue">
                                            4
                                        </td>
                                        <td class="Skyblue">
                                            5
                                        </td>
                                        <td class="Skyblue">
                                            6
                                        </td>
                                        <td class="Skyblue">
                                            7
                                        </td>
                                        <td class="Skyblue">
                                            8
                                        </td>
                                        <td class="Skyblue">
                                            9
                                        </td>
                                        <td class="Skyblue">
                                            10
                                        </td>
                                        <td class="Skyblue">
                                            11
                                        </td>
                                        <td class="Skyblue">
                                            12
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="8">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            PM.
                        </td>
                        <td align="left">
                            <div id="divtableTimePM" align="left" language="javascript" onmousedown="return divtableTimePM_onmousedown()">
                                <table id="tableTimePM" cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td class="Skyblue">
                                            1
                                        </td>
                                        <td class="Skyblue">
                                            2
                                        </td>
                                        <td class="Skyblue">
                                            3
                                        </td>
                                        <td class="Skyblue">
                                            4
                                        </td>
                                        <td class="Skyblue">
                                            5
                                        </td>
                                        <td class="Skyblue">
                                            6
                                        </td>
                                        <td class="Skyblue">
                                            7
                                        </td>
                                        <td class="Skyblue">
                                            8
                                        </td>
                                        <td class="Skyblue">
                                            9
                                        </td>
                                        <td class="Skyblue">
                                            10
                                        </td>
                                        <td class="Skyblue">
                                            11
                                        </td>
                                        <td class="Skyblue">
                                            12
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                        <td class="GrayBorder">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="101" style="height: 101px">
                <table id="Table4" cellspacing="0" cellpadding="0" width="100%" border="0" style="border-right: silver 1px solid;
                    border-top: silver 1px solid; border-left: silver 1px solid; border-bottom: silver 1px solid">
                    <tr>
                        <td align="center" width="12%">
                            <input onclick="javascript:AddTime()" type="button" value="添加">
                        </td>
                        <td width="81%" rowspan="2">
                            <select language="javascript" style="width: 100%" onchange="return selAllTimes_onchange()"
                                size="6" name="selAllTimes" width="300px">
                                <option value="be::2001-01-01:2003-01-01;9-16-20:4345">(阶段)be::2001-01-01:2003-01-01;9-16-20:4345</option>
                                <option value="re::d:2001-01-01,w:mon,w:mon-web;(9-16:60)4345">(重复)re::d:2001-01-01,w:mon,w:mon-web;(9-16:60)4345</option>
                            </select>
                        </td>
                        <td align="center" width="12%">
                            <img src="../../Images/icons/0048_a.gif" style="cursor: hand">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <input type="button" value="删除">
                        </td>
                        <td align="center" width="7%">
                            <img src="../../Images/icons/0049_a.gif" style="cursor: hand">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" height="3" style="height: 3px">
                <input onclick="javascript:window.close()" type="button" value="确定">&nbsp;<input
                    id="buttRefresh" onclick="javascript:location.reload()" type="button" value="刷新">
            </td>
        </tr>
    </table>
</body>
</html>
