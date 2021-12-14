<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" exclude-result-prefixes="xsl msxsl ddwrt" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:ddwrt="http://schemas.microsoft.com/WebParts/v2/DataView/runtime" xmlns:asp="http://schemas.microsoft.com/ASPNET/20" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:ms="urn:schemas-microsoft-com:xslt"
 xmlns:SharePoint="Microsoft.Sharepoint.WebControls">
<xsl:output method="html" encoding="utf-8" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>

<xsl:template match="/">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>Untitled Document</title>
	<script language="javascript">
			 <![CDATA[	  
			 function ss()
				  {
				  alert('a');
				  $('#aaa').trigger("click");
				  }
				  ]]>
			  </script>
</head>

<body onload="ss()">
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size:13px;text-align:left; ">
  <tr>
    <td valign="TOP" align="LEFT" width="100%">
	<table id="normalxsl" class="normitem" width="100%" border="0" cellspacing="1" cellpadding="0" style="">
        <tr style=" font-size:13px; font-weight:bold;background-color:#8fd9fe;padding-left:0.5em; padding-right:0.5em;">
		<td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">序号</td>
        <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">检验项目</td>
        <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">项目简码</td>
        <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">结果</td>
		<td style="height:26px; line-height:26px; white-space:nowrap;padding-left:0.5em; padding-right:0.5em;">状态</td>
        <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">单位</td>
        <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">参考值</td>
        <!--<td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">历史结果</td>-->
        <!--td style="border-left:#eae7de solid 1px; border-bottom:#eae7de solid 1px;">历史比值</td-->
        <!--<td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">组合项目</td>-->
		<!--<td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">操作</td>-->
      </tr>
      <xsl:for-each select="WebReportFile/ReportItem">
        <tr style="height:22px; line-height:22px;background:#CCFFFF;" onmouseover="this.style.background='#ffffcc'" onmouseout="this.style.background='#CCFFFF'" onclick="this.style.background='#ffff99'">
			<td style="text-align:center;">
				<xsl:value-of select="position()"/>
				<xsl:if test="position() =1 ">
					<table>
						<tr style="display:none;" >
							<td  id="aaa" onclick="printResult('{ReportFormID};{ITEMNO};item;{RECEIVEDATE}');" >111</td>
						</tr>
					</table>
				</xsl:if>
			</td>
          <td onclick="printResult('{ReportFormID};{ITEMNO};item;{RECEIVEDATE}')" style="padding-left:0.5em; padding-right:0.5em;"><xsl:value-of select="TESTITEMNAME"/></td>
          <td onclick="printResult('{ReportFormID};{ITEMNO};item;{RECEIVEDATE}')" style="padding-left:0.5em; padding-right:0.5em;"><xsl:value-of select="TESTITEMSNAME"/>
		
		  </td>
          <td onclick="printResult('{ReportFormID};{ITEMNO};item;{RECEIVEDATE}')" style="padding-left:0.5em; padding-right:0.5em;">
	  <xsl:choose>
      <xsl:when test="RESULTSTATUS='H'">
      <span style="color:red;">
	  <xsl:value-of select="REPORTVALUE"/></span>
	  </xsl:when>
      <xsl:when test="RESULTSTATUS='L'">
      <span style="color:blue;">
	  <xsl:value-of select="REPORTVALUE"/></span>
	  </xsl:when>
      <xsl:otherwise><xsl:value-of select="REPORTVALUE"/></xsl:otherwise>      
    </xsl:choose>	  
	  <xsl:value-of select="REPORTDESC"/></td>
			<td style="padding-left:0.5em; padding-right:0.5em;">
				<xsl:choose>
					<xsl:when test="RESULTSTATUS='H'">
						<span style="color:red;">
							<xsl:value-of select="RESULTSTATUS"/>
						</span>
					</xsl:when>
					<xsl:when test="RESULTSTATUS='L'">
						<span style="color:blue;">
							<xsl:value-of select="RESULTSTATUS"/>
						</span>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="RESULTSTATUS"/>
					</xsl:otherwise>
				</xsl:choose>
			</td>
          <td style="padding-left:0.5em; padding-right:0.5em;"><xsl:value-of select="UNIT"/></td>
          <td style="padding-left:0.5em; padding-right:0.5em;"><xsl:value-of select="REFRANGE"/></td>
          <!--<td style="padding-left:0.5em; padding-right:0.5em;"><xsl:value-of select="HISVALUE"/></td>-->
          <!--td style="border-left:#eae7de solid 1px; border-bottom:#eae7de solid 1px;"><xsl:value-of select="HISCOMP"/></td-->
          <!--<td style="padding-left:0.5em; padding-right:0.5em;"><xsl:value-of select="paritemname"/></td>-->
		  <!--<td style="padding-left:0.5em; padding-right:0.5em;">-->
			  <!--<button onClick="ss()">Query</button><xsl:value-of select="FORMNO"/>-->
			  <!--<a onclick="fNo('{PATNO};{ITEMNO};item;{SJ}')" title="{PATNO};{ITEMNO};item;{SJ}"  style=" cursor:pointer; color:blue;">历史对比</a>
		  </td>-->
			
        </tr>
      </xsl:for-each>
    </table>
		<!--<div id="iframeContrast"></div>-->
		<!--<iframe id="iframeContrast" name="ifct" src="" height="400px" scrolling="no" width="100%"></iframe>-->
	</td>
	  
    <td align="LEFT" valign="TOP" width="160">
	
	  <table id="yishu" width="158" border="0" cellspacing="0" cellpadding="0" style="font-size:13px;height:22px; line-height:22px;border:#eae7de solid 2px; text-align:left; text-indent:0.5em;">
	    <tr>
          <td width="154" bgcolor="#eae7de" style=" font-weight:bold">医嘱信息</td>
        </tr>
	<xsl:for-each select="WebReportFile/ReportForm">      
        <tr>
          <td>病人姓名：<xsl:value-of select="CNAME"/></td>
        </tr>
        <tr>
          <td>病历编号：<xsl:value-of select="PATNO"/></td>
        </tr>
		  <tr>
           <td>样本号：<xsl:value-of select="SAMPLENO"/></td>
        </tr>
        <tr>
          <td>病人性别：<xsl:value-of select="GENDERNAME"/></td>
        </tr>
        <tr>
          <td>病人年龄：<xsl:value-of select="AGE"/> <xsl:value-of select="AGEUNITNAME"/></td>
        </tr>
        <tr>
          <td>病人民族：<xsl:value-of select="FOLKNO"/></td>
        </tr>
        <tr>
          <td>开单科室：<xsl:value-of select="DEPTNO"/></td>
        </tr>
        <tr>
          <td>开单医生：<xsl:value-of select="DOCTOR"/></td>
        </tr>
        <tr>
          <td>操作日期：<xsl:value-of select="substring-before(OPERDATE,' 0')"/></td>
        </tr>
        <tr>
          <td>操作时间：<xsl:value-of select="substring(OPERTIME,10,9)"/></td>
        </tr>
        <tr>
          <td>样本类型：<xsl:value-of select="SAMPLETYPENAME"/></td>
        </tr>
        <tr>
          <td>所属病区：<xsl:value-of select="DISTRICTNO"/></td>
        </tr>
        <tr>
          <td>病人病房：<xsl:value-of select="WARDNO"/></td>
        </tr>
        <tr>
          <td>病床号码：<xsl:value-of select="BED"/></td>
        </tr>
        <tr>
          <td>就诊类型：<xsl:value-of select="SICKTYPENO"/></td>
        </tr>
        <tr>
          <td>收费类型：<xsl:value-of select="CHARGENO"/></td>
        </tr>
        <tr>
          <td>收费金额：<xsl:value-of select="CHARGE"/></td>
        </tr>
        <tr>
          <td>采样人员：<xsl:value-of select="COLLECTER"/></td>
        </tr>
        <tr>
          <td>采样日期：<xsl:value-of select="substring-before(COLLECTDATE,' 0')"/></td>
        </tr>
        <tr>
          <td>采样时间：<xsl:value-of select="substring(COLLECTTIME,10,9)"/></td>
        </tr>
        <tr>
          <td>备注信息：<xsl:value-of select="FORMMEMO"/></td>
        </tr>
        <tr>
          <td>签收日期：<xsl:value-of select="substring-before(INCEPTDATE,' 0')"/></td>
        </tr>
        <tr>
          <td>签收时间：<xsl:value-of select="substring(INCEPTTIME,10,9)"/></td>
        </tr>
      
		</xsl:for-each>
    </table>	</td>
  </tr>
</table>
</body>

</html>

</xsl:template>
</xsl:stylesheet>