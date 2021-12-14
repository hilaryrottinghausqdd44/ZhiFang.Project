<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" exclude-result-prefixes="xsl msxsl ddwrt" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:ddwrt="http://schemas.microsoft.com/WebParts/v2/DataView/runtime" xmlns:asp="http://schemas.microsoft.com/ASPNET/20" 
xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:ms="urn:schemas-microsoft-com:xslt" xmlns:SharePoint="Microsoft.Sharepoint.WebControls">
<xsl:output method="html" encoding="utf-8" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" 
	doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>

<xsl:template match="/">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<title>报告结果</title>
	</head>
	<body>
		<div style="padding:10px 5px;font-weight:bold;border-bottom:1px dashed #e0e0e0;font-size:12px;">医嘱信息</div>
		<xsl:for-each select="WebReportFile/ReportForm">
		<!--主单信息-->
  		<div class="row" style="margin:0;padding:10px 5px;border-bottom:1px dashed #e0e0e0;font-size:12px;">
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">姓名：<xsl:value-of select="CNAME"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">性别：<xsl:value-of select="GENDERNAME"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">年龄：<xsl:value-of select="AGE"/> <xsl:value-of select="AGEUNITNAME"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">病历号：<xsl:value-of select="PATNO"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">科室：<xsl:value-of select="DEPTNAME"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">病房：<xsl:value-of select="WARDNO"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">医生：<xsl:value-of select="DOCTOR"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">床号：<xsl:value-of select="BED"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">检验小组：<xsl:value-of select="SECTIONNAME"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">检验者：<xsl:value-of select="OPERATOR"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">录入者：<xsl:value-of select="TECHNICIAN"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">审核者：<xsl:value-of select="CHECKER"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">操作日期：<xsl:value-of select="substring-before(TESTDATE,' 0')"/></div>
			<div class="col-xs-6 col-sm-3 col-md-3 col-lg-2">操作时间：<xsl:value-of select="substring-before(TESTTIME,':000')"/></div>
			<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">临床诊断：<xsl:value-of select="FORMMEMO"/></div>
			<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">备注：<xsl:value-of select="FORMMEMO"/></div>
			<div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">细菌描述：<xsl:value-of select="DISTRICTNO"/></div>
		</div>
		</xsl:for-each>
		<div style="padding:10px 5px;font-weight:bold;border-bottom:1px dashed #e0e0e0;font-size:12px;">检验项目</div>
		<!--项目明细-->
		<table class="table" style="font-size:12px;margin:0;">
			<thead>
				<tr>
					<th>序号</th>
					<th>检验项目</th>
					<th>结果</th>
					<th>状态</th>
					<th>单位</th>
					<th>参考值</th>
				</tr>
			</thead>
			<tbody>
				<xsl:for-each select="WebReportFile/ReportItem">
				<tr>
					<td><xsl:value-of select="position()"/></td>
					<td><xsl:value-of select="TESTITEMNAME"/></td>
					<td>
						<xsl:choose>
							<xsl:when test="RESULTSTATUS='H'">
								<span style="color:red;"><xsl:value-of select="REPORTVALUE"/></span>
							</xsl:when>
							<xsl:when test="RESULTSTATUS='L'">
								<span style="color:blue;"><xsl:value-of select="REPORTVALUE"/></span>
							</xsl:when>
							<xsl:when test="RESULTSTATUS='LL'">
								<span style="color:yellow;width:100%;background-color:#008080;">
									<xsl:value-of select="REPORTVALUE"/>
								</span>
							</xsl:when>
							<xsl:when test="RESULTSTATUS='HH'">
								<span style="color:yellow;width:100%;background-color:#008080;">
									<xsl:value-of select="REPORTVALUE"/>
								</span>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="REPORTVALUE"/>
							</xsl:otherwise>
						</xsl:choose>
						<xsl:value-of select="REPORTDESC"/>
					</td>
					<td>
						<xsl:choose>
							<xsl:when test="RESULTSTATUS='H'">
								<span style="color:red;"><xsl:value-of select="RESULTSTATUS"/></span>
							</xsl:when>
							<xsl:when test="RESULTSTATUS='L'">
								<span style="color:blue;"><xsl:value-of select="RESULTSTATUS"/></span>
							</xsl:when>
							<xsl:when test="RESULTSTATUS='LL'">
								<span style="color:yellow;width:100%;background-color:#008080;">
									<xsl:value-of select="RESULTSTATUS"/>
								</span>
							</xsl:when>
							<xsl:when test="RESULTSTATUS='HH'">
								<span style="color:yellow;width:100%;background-color:#008080;">
									<xsl:value-of select="RESULTSTATUS"/>
								</span>
							</xsl:when>
							<xsl:otherwise>
								<xsl:value-of select="RESULTSTATUS"/>
							</xsl:otherwise>
						</xsl:choose>	
					</td>
					<td><xsl:value-of select="UNIT"/></td>
					<td><xsl:value-of select="REFRANGE"/></td>
				</tr>
				</xsl:for-each>
			</tbody>
		</table>
	</body>
</html>
</xsl:template>
</xsl:stylesheet>