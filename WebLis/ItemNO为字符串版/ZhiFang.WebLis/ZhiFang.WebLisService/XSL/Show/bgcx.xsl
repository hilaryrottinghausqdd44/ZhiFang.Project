<?xml version="1.0" encoding="gb2312"?><!-- DWXMLSource="bgcxsh.xml" --><!DOCTYPE xsl:stylesheet  [
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
<xsl:template match="/">
<style type="text/css" >
        <![CDATA[ 
table tr td{ font-size:13px;height:22px; line-height:22px;}
.formstyle{ border:#eae7de solid 2px; text-align:left;}/*医嘱信息样式*/
.itemstyle tr td{ border-left:#eae7de solid 1px; border-bottom:#eae7de solid 1px;  text-align:left;}/*项目表样式*/
.headbg td{ height:26px; line-height:26px; background-color:#99ccff;border-top:#eae7de solid 1px;}/*项目表表头样式*/
.rightborder{ border-right:#eae7de solid 1px;}/*项目表样式 右边框*/
.fsj{padding-left:0.5em;}/*文字缩进0.5em*/
.fstrong{ font-weight:bold}
        ]]>
</style>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td valign="TOP" align="LEFT"><table width="100%" border="0" cellspacing="0" cellpadding="0" class="itemstyle fsj">
      <tr class="headbg fstrong">
        <td>状态</td>
        <td>检验项目</td>
        <td>英文名称</td>
        <td>结果</td>
        <td>单位</td>
        <td>参考值</td>
        <td>历史结果</td>
        <td>历史比值</td>
        <td class="rightborder">组合项目</td>
      </tr>
      <xsl:for-each select="WebReportFile/ReportItem">
        <tr class="bottomborder" onmouseover="this.style.background='#deeefe'" onmouseout="this.style.background='#ffffff'" onclick="this.style.background='#cae3f9'">
          <td><xsl:value-of select="RESULTSTATUS"/></td>
          <td><xsl:value-of select="TESTITEMNAME"/></td>
          <td><xsl:value-of select="TESTITEMSNAME"/></td>
          <td><xsl:value-of select="REPORTVALUE"/></td>
          <td><xsl:value-of select="UNIT"/></td>
          <td><xsl:value-of select="REFRANGE"/></td>
          <td><xsl:value-of select="ZDY2"/></td>
          <td><xsl:value-of select="ZDY3"/></td>
          <td class="rightborder"><xsl:value-of select="ZDY4"/></td>
        </tr>
      </xsl:for-each>
    </table></td>
    <td align="RIGHT" valign="TOP" width="160">
	
	  <table width="150" border="0" cellspacing="0" cellpadding="0" class="formstyle fsj">
	    <tr>
          <td bgcolor="#eae7de" class="fstrong">医嘱信息</td>
        </tr>
	<xsl:for-each select="WebReportFile/ReportForm">      
        <tr>
          <td>病人姓名：<xsl:value-of select="CNAME"/></td>
        </tr>
        <tr>
          <td>病历编号：<xsl:value-of select="PATNO"/></td>
        </tr>
        <tr>
          <td>病人性别：<xsl:value-of select="GENDER"/></td>
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
          <td>操作日期：<xsl:value-of select="OPERDATE"/></td>
        </tr>
        <tr>
          <td>操作时间：<xsl:value-of select="OPERTIME"/></td>
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
          <td>采样日期：<xsl:value-of select="COLLECTDATE"/></td>
        </tr>
        <tr>
          <td>采样时间：<xsl:value-of select="COLLECTTIME"/></td>
        </tr>
        <tr>
          <td>备注信息：<xsl:value-of select="FORMMEMO"/></td>
        </tr>
        <tr>
          <td>签收日期：<xsl:value-of select="INCEPTDATE"/></td>
        </tr>
        <tr>
          <td>签收时间：<xsl:value-of select="INCEPTTIME"/></td>
        </tr>
        <tr>
          <td>小组名称：<xsl:value-of select="SECTIONNAME"/></td>
        </tr>
		</xsl:for-each>
    </table>	</td>
  </tr>

</xsl:template>
</xsl:stylesheet>