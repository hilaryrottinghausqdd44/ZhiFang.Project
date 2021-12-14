<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

 <!--字符串替换模板-->
 <xsl:template name="StringReplace">
  <xsl:param name="SrcString"/>
  <xsl:choose>
   <xsl:when test="contains($SrcString,';')">
    <xsl:value-of select="substring-before($SrcString,';')"/><br/>
    <xsl:call-template name="StringReplace">
     <xsl:with-param name="SrcString" select="substring-after($SrcString,';')"/>
    </xsl:call-template>
   </xsl:when>
   <xsl:otherwise><xsl:value-of select="$SrcString"/></xsl:otherwise>
  </xsl:choose>
 </xsl:template>
 <!--模板结束-->
<xsl:template match="/">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>Untitled Document</title>
</head>
<body style="font-size:13px;line-height:150%; font-family:宋体,Arial, Helvetica, sans-serif;">
<table width="720" border="0" cellspacing="0" cellpadding="0" >
  <tr>
    <td colspan="4"><h2 style="line-height:150%;text-align:center;font-size:24px;">青海省心脑血管病专科医院检验报告单</h2></td>
    </tr>
  <tr>
    <td> </td>
    <td>  </td>
	<xsl:for-each select="WebReportFile/ReportForm">
    <td align="RIGHT" style="font-size:13px">样 本 号：<xsl:value-of select="TESTTYPENAME"/><xsl:value-of select="SAMPLENO"/></td>
	<td ></td>
	</xsl:for-each>
  </tr>
  
  <tr>
    <td valign="TOP" align="LEFT" width="22%">
	<table border="0" cellspacing="0" cellpadding="0" height="300" style="font-size:13px">
	<xsl:for-each select="WebReportFile/ReportForm">
      <tr>
        <td width="200">姓　　名：<xsl:value-of select="CNAME"/></td>
      </tr>
      <tr>
        <td>性　　别：<xsl:value-of select="GENDERNAME"/></td>
      </tr>
      <tr>
        <td>年　　龄：<xsl:value-of select="AGE"/><xsl:value-of select="AGEUNITNAME"/></td>
      </tr>
      <tr>
        <td>病 历 号：<xsl:value-of select="PATNO"/></td>
      </tr>
      <tr>
        <td>床　　号：<xsl:value-of select="BED"/></td>
      </tr>
      <tr>
        <td>标本种类：<xsl:value-of select="SAMPLETYPENAME"/></td>
      </tr>
      <tr>
        <td>科　　室：<xsl:value-of select="DEPTNAME"/></td>
      </tr>
      <tr>
        <td>医　　生：<xsl:value-of select="DOCTOR"/></td>
      </tr>
      <tr>
        <td>采样日期：<xsl:value-of select="COLLECTDATE"/></td>
      </tr>
      <tr>
        <td>采样时间：<xsl:value-of select="COLLECTTIME"/></td>
      </tr>
	  </xsl:for-each>
    </table></td>
    <td colspan="3" valign="TOP" align="LEFT" style="height:100px;">
	<table border="0" cellspacing="0" cellpadding="0" style="border:#000000 solid 1px; height:300px;" width="520">
        <tr>
          <td valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0"  style="height:100%;border-right:#000000 solid 1px; font-size:13px">
            <tr>
              <td style="border-right:#000000 dotted 1px;border-bottom:#000000 solid 1px;padding-left:2px; height:22px" width="104"><nobr>项目</nobr></td>
              <td colspan="2" style="border-right:#000000 dotted 1px;border-bottom:#000000 solid 1px;padding-left:2px;" width="52"><nobr>结果</nobr></td>
              <td style="border-right:#000000 dotted 1px;border-bottom:#000000 solid 1px;padding-left:2px;" width="52"><nobr>单位</nobr></td>
              <td style="border-bottom:#000000 solid 1px;padding-left:2px;" width="52"><nobr>参考值</nobr></td>
            </tr>
            <xsl:for-each select="WebReportFile/ReportItem[DISPLAYID mod 2 =1]">
              <tr style=" line-height:18px">
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 dotted 1px;padding-left:2px;"><xsl:value-of select="TESTITEMNAME"/>　</td>
                <td style="border-bottom:#000000 dotted 1px;"><xsl:value-of select="REPORTVALUE"/>　</td>
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 dotted 1px;padding-left:2px;"><xsl:value-of select="RESULTSTATUS"/>　</td>
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 dotted 1px;padding-left:2px;"><xsl:value-of select="UNIT"/>　</td>
                <td style="padding-left:2px;border-bottom:#000000 dotted 1px;"><xsl:call-template name="StringReplace"><xsl:with-param name="SrcString" select="REFRANGE"/></xsl:call-template>　</td>
              </tr>
            </xsl:for-each>
            <tr style="height:100%">
              <td  style="border-right:#000000 dotted 1px;">　</td>
              <td>　</td>
              <td style="border-right:#000000 dotted 1px;">　</td>
              <td style="border-right:#000000 dotted 1px;">　</td>
              <td>　</td>
            </tr>
          </table></td>
          <td valign="top">
		 <table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%" style="font-size:13px"> 
              <tr>
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 solid 1px;padding-left:2px; height:22px" width="104"><nobr>项目</nobr></td>
                <td colspan="2" style="border-right:#000000 dotted 1px;border-bottom:#000000 solid 1px;padding-left:2px;" width="52"><nobr>结果</nobr></td>
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 solid 1px;padding-left:2px;" width="52"><nobr>单位</nobr></td>
                <td style="border-bottom:#000000 solid 1px;padding-left:2px;" width="52"><nobr>参考值</nobr></td>
              </tr>
			  <xsl:for-each select="WebReportFile/ReportItem[DISPLAYID mod 2 =0]">
              <tr style=" line-height:18px">
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 dotted 1px;padding-left:2px;"><xsl:value-of select="TESTITEMNAME"/>　</td>
                <td style="padding-left:2px;border-bottom:#000000 dotted 1px;"><xsl:value-of select="REPORTVALUE"/>　</td>
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 dotted 1px;padding-left:2px;"><xsl:value-of select="RESULTSTATUS"/>　</td>
                <td style="border-right:#000000 dotted 1px;border-bottom:#000000 dotted 1px;padding-left:2px;"><xsl:value-of select="UNIT"/>　</td>
                <td style="padding-left:2px;border-bottom:#000000 dotted 1px;"><xsl:call-template name="StringReplace"><xsl:with-param name="SrcString" select="REFRANGE"/></xsl:call-template>　</td>
              </tr>
			  </xsl:for-each>
			  <tr style="height:100%">
                <td style="border-right:#000000 dotted 1px;">　</td>
                <td >　</td>
                <td style="border-right:#000000 dotted 1px;">　</td>
                <td style="border-right:#000000 dotted 1px;">　</td>
                <td >　</td>
              </tr>
          </table></td>
          </tr>
      </table></td>
    </tr>
  <xsl:for-each select="WebReportFile/ReportForm">
  </xsl:for-each>
</table>
<table width="720" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="30" style="border-bottom:#000000 solid 1px;font-size:13px">临床诊断：<xsl:value-of select="DIAGDESCRIBE"/></td>
    <td style="border-bottom:#000000 solid 1px;font-size:13px">备　　注：<xsl:value-of select="FORMMEMO"/></td>
  </tr>
</table>
<table width="720" border="0" cellspacing="0" cellpadding="0" style="font-size:13px">
<xsl:for-each select="WebReportFile/ReportForm">
  <tr style="height:22px; line-height:22px;">
    <td height="30">检验日期：<xsl:value-of select="TESTDATE"/></td>
    <td>报告日期：!creatdate!</td>
    <td>检 验 者： <xsl:choose>
            <xsl:when test="TechnicianUrl!=''">
	    <span style='vertical-align:middle; margin:1px;'>
              <xsl:element name="img">
                <xsl:attribute name="src">
                  <xsl:value-of select="TechnicianUrl"/>
                </xsl:attribute>
                <xsl:attribute name="width">
                  <xsl:value-of select="60"/>
                </xsl:attribute>
                <xsl:attribute name="height">
                  <xsl:value-of select="25"/>
                </xsl:attribute>
              </xsl:element></span>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="TECHNICIAN"/>
            </xsl:otherwise>
          </xsl:choose></td>
    <td >审 核 者：<xsl:choose>
            <xsl:when test="TechnicianUrl!=''">
              <span style='vertical-align:middle; margin:1px;'>
	      <xsl:element name="img">
                <xsl:attribute name="src">
                  <xsl:value-of select="CheckerUrl"/>
                </xsl:attribute>
                <xsl:attribute name="width">
                  <xsl:value-of select="60"/>
                </xsl:attribute>
                <xsl:attribute name="height">
                  <xsl:value-of select="25"/>
                </xsl:attribute>
              </xsl:element></span>
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="CHECKER"/>
            </xsl:otherwise>
          </xsl:choose></td>
  </tr>
  <tr>
    <td colspan="6" height="30">声明：此结果仅对该检验标本负责！</td>
  </tr>
</xsl:for-each>
</table>
</body></html>

</xsl:template>
</xsl:stylesheet>