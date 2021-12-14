<<?xml version="1.0" encoding="utf-8"?><!-- DWXMLSource="Untitled-1.xml" -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" encoding="utf-8" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
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
<body style="font-size:14px;line-height:120%; font-family:宋体,Arial, Helvetica, sans-serif;">
<table width="720" border="0" cellspacing="0" cellpadding="0" style="margin-left:20px; margin-right:20px;">
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0"  style="margin-bottom:10px;">
      <tr style="height:22px; line-height:22px;">
        <td colspan="4"><h2 style="line-height:150%;text-align:center;font-size:24px;">青海省红十字医院特种检验报告单</h2></td>
      </tr>
      <xsl:for-each select="WebReportFile/ReportForm">
        <tr>
          <td style=" height:22px; line-height:22px">姓名：<xsl:value-of select="CNAME"/></td>
          <td>病 　区：<xsl:value-of select="DISTRICTNAME"/></td>
          <td>送检医生：<xsl:value-of select="DOCTOR"/></td>
          <td width="34%">样 本 号：<xsl:value-of select="TESTTYPENAME"/><xsl:value-of select="SAMPLENO"/></td>
        </tr>
        <tr>
          <td style=" height:22px; line-height:22px">性别：<xsl:value-of select="GENDERNAME"/></td>
          <td>病 案 号：<xsl:value-of select="PATNO"/></td>
          <td>标本种类：<xsl:value-of select="SAMPLETYPENAME"/></td>
          <td>检测日期：<xsl:value-of select="COLLECTDATE"/>　<xsl:value-of select="COLLECTTIME"/></td>
        </tr>
        <tr>
          <td style=" height:22px; line-height:22px">年龄：<xsl:value-of select="AGE"/> <xsl:value-of select="AGEUNITNAME"/></td>
          <td>病 床 号：<xsl:value-of select="BED"/></td>
          <td>临床诊断：<xsl:value-of select="DIAGDESCRIBE"/></td>
          <td>备　　注：<xsl:value-of select="FORMMEMO"/></td>
        </tr>
      </xsl:for-each>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0"  style="margin-bottom:10px;border-top:#000000 solid 1px; border-bottom:#000000 solid 1px;">
        <tr>
          <td style="border-bottom:#000000 solid 1px;height:22px; line-height:22px">No</td>
          <td style="border-bottom:#000000 solid 1px;" width="25%">项目名称</td>
          <td colspan="2" style="border-bottom:#000000 solid 1px;">测试结果</td>
          <td style="border-bottom:#000000 solid 1px;">单位</td>
          <td style="border-bottom:#000000 solid 1px;" width="25%">参考值</td>
        </tr>
        <xsl:for-each select="WebReportFile/ReportItem">
          <tr>
            <td><xsl:value-of select="DISPLAYID"/></td>
            <td><xsl:value-of select="TESTITEMNAME"/></td>
            <td><xsl:value-of select="REPORTVALUE"/></td>
            <td><xsl:value-of select="RESULTSTATUS"/></td>
            <td><xsl:value-of select="UNIT"/></td>
            <td>
				<xsl:call-template name="StringReplace">
					<xsl:with-param name="SrcString" select="REFRANGE"/>
				</xsl:call-template>
			</td>
          </tr>
        
        <tr>
          <td colspan="6" height="6"></td>
        </tr>
		</xsl:for-each>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" >
        <xsl:for-each select="WebReportFile/ReportForm">
          <tr>
            <td style="height:22px; line-height:22px;">检验日期：<xsl:value-of select="TESTDATE"/></td>
            <td>报告日期：<creatdate></td>
            <td>检 验 者:<xsl:value-of select="TECHNICIAN"/></td>
            <td>审 核 者：<xsl:value-of select="CHECKER"/></td>
          </tr>
        </xsl:for-each>
        <tr>
          <td colspan="4">声明：此结果仅对该检验标本负责！</td>
        </tr>
      </table></td>
  </tr>
</table>

</body>
</html>

</xsl:template>
</xsl:stylesheet>