<?xml version="1.0" encoding="gb2312"?><!-- DWXMLSource="qhhsz.xml" --><!DOCTYPE xsl:stylesheet  [
	<!ENTITY nbsp   "&#160;">
	<!ENTITY copy   "&#169;">
	<!ENTITY reg    "&#174;">
	<!ENTITY trade  "&#8482;">
	<!ENTITY mdash  "&#8212;">
	<!ENTITY ldquo  "&#8220;">
	<!ENTITY rdquo  "&#8221;"> 
	<!ENTITY pound  "&#163;">
	<!ENTITY yen    "&#165;">
	<!ENTITY euro   "&#8364;">
]>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" encoding="gb2312" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
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
<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
<title>Untitled Document</title>
<style type="text/css">
<xsl:comment>
.jg10 {margin-bottom:10px;}
</xsl:comment>
</style>
</head>
<style type="text/css" >
        <![CDATA[ 
*{font-family: "宋体", Arial, Helvetica, sans-serif;  line-height:120%;}
#bodytable{font-size:14px; margin-left:20px; margin-right:20px;}

h2{line-height:150%;text-align:center;font-size:24px;}/*标题样式*/

#formstyle tr{ height:22px; line-height:22px;}/*姓名等信息样式*/
#itemstyle{border-top:#000000 solid 1px; border-bottom:#000000 solid 1px;}/*item样式*/
#footstyle{}/*核对者、声明部分样式*/
#footstyle tr td{ height:22px; line-height:22px;}

.bottomsolid{border-bottom:#000000 solid 1px;}/*添加黑色下划线*/

.jg10{margin-bottom:10px;}/*表格间的10px间隙*/
        ]]>
</style>
<body>
<table width="720" border="0" cellspacing="0" cellpadding="0" id="bodytable">
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0" id="formstyle" class="jg10">
      <tr>
        <td colspan="4"><h2>青海省红十字医院特种检验报告单</h2></td>
      </tr>
      <xsl:for-each select="NewDataSet/dtf">
        <tr>
          <td>姓　　名：<xsl:value-of select="CName"/></td>
          <td>病　　区：<xsl:value-of select="District"/></td>
          <td>送检医生：<xsl:value-of select="Doctor"/></td>
          <td width="34%">样 本 号：<xsl:value-of select="TestType"/><xsl:value-of select="SampleNo"/></td>
        </tr>
        <tr>
          <td>性　　别：<xsl:value-of select="Gender"/></td>
          <td>病 案 号：<xsl:value-of select="PatNo"/></td>
          <td>标本种类：<xsl:value-of select="SampleType"/></td>
          <td>检测日期：<xsl:value-of select="CollectDate"/>　<xsl:value-of select="CollectTime"/></td>
        </tr>
        <tr>
          <td>年　　龄：<xsl:value-of select="Age"/> <xsl:value-of select="AgeUnit"/></td>
          <td>病 床 号：<xsl:value-of select="Bed"/></td>
          <td>临床诊断：<xsl:value-of select="Diag"/></td>
          <td>备　　注：<xsl:value-of select="FromMemo"/></td>
        </tr>
      </xsl:for-each>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" id="itemstyle" class="jg10">
        <tr>
          <td class="bottomsolid">No</td>
          <td class="bottomsolid" width="25%">项目名称</td>
          <td colspan="2" class="bottomsolid">测试结果</td>
          <td class="bottomsolid">单位</td>
          <td class="bottomsolid" width="25%">参考值</td>
        </tr>
        <xsl:for-each select="NewDataSet/dti">
          <tr>
            <td><xsl:value-of select="DisplayId"/></td>
            <td><xsl:value-of select="ItemCName"/></td>
            <td><xsl:value-of select="ItemValue"/></td>
            <td><xsl:value-of select="resultstatus"/></td>
            <td><xsl:value-of select="ItemUnit"/></td>
            <td>
				<xsl:call-template name="StringReplace">
					<xsl:with-param name="SrcString" select="RefRange"/>
				</xsl:call-template>
			</td>
          </tr>
        
        <tr>
          <td colspan="6" height="6"></td>
        </tr>
		</xsl:for-each>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0" id="footstyle">
        <xsl:for-each select="NewDataSet/dtf">
          <tr>
            <td>检验日期：<xsl:value-of select="TestDate"/></td>
            <td>报告日期：<xsl:value-of select="DATE"/></td>
            <td>检 验 者:<xsl:value-of select="Technician"/></td>
            <td>审 核 者：<xsl:value-of select="Checker"/></td>
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