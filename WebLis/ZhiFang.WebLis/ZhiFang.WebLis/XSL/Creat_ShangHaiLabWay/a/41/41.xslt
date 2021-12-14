<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html" encoding="utf-8" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
<xsl:template match="/">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>Untitled Document</title>
</head>

<body style="font-size:13px;line-height:150%; font-family:宋体,Arial, Helvetica, sans-serif;">
<table width="720" border="0" cellspacing="0" cellpadding="0" style="font-size:13px; margin-left:20px; margin-right:20px;">
  <tr>
    <td><table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:10px;">
      <tr style=" height:22px; line-height:22px;">
        <td colspan="4"><h2 style="line-height:150%;text-align:center;font-size:24px;">青海省心脑血管病专科医院血液检验报告单</h2></td>
      </tr>
	  <xsl:for-each select="WebReportFile/ReportForm">
      <tr>
        <td>姓　　名：<xsl:value-of select="CNAME"/></td>
        <td>民　　族：<xsl:value-of select="FOLKNAME"/></td>
        <td>床　　号：<xsl:value-of select="BED"/></td>
        <td>样 本 号：<xsl:value-of select="TESTTYPENAME"/><xsl:value-of select="SAMPLENO"/></td>
      </tr>
      <tr>
        <td>性　　别：<xsl:value-of select="GENDERNAME"/></td>
        <td>病 人 ID：<xsl:value-of select="PATNO"/></td>
        <td>送检医生：<xsl:value-of select="DOCTOR"/></td>
        <td>标本种类：<xsl:value-of select="SAMPLETYPENAME"/></td>
      </tr>
      <tr>
        <td>年　　龄：<xsl:value-of select="AGE"/> <xsl:value-of select="AGEUNITNAME"/></td>
        <td>科　　室：<xsl:value-of select="DEPTNAME"/></td>
        <td>送检日期：<xsl:value-of select="COLLECTDATE"/></td>
        <td>送检时间：<xsl:value-of select="COLLECTTIME"/></td>
      </tr>
      <tr>
        <td colspan="2">临床诊断：<xsl:value-of select="DIAGDESCRIBE"/></td>
        <td colspan="2">备　　注：<xsl:value-of select="FORMMEMO"/></td>
      </tr>
	  </xsl:for-each>
    </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0"  style="margin-bottom:10px;border-top:#000000 solid 1px; border-bottom:#000000 solid 1px;">
        <tr>
          <td colspan="2" style="border-bottom:#000000 solid 1px;">项目代号</td>
          <td style="border-bottom:#000000 solid 1px;" width="30%">项目名称</td>
          <td colspan="2" style="border-bottom:#000000 solid 1px;">结果</td>
          <td style="border-bottom:#000000 solid 1px;">单位</td>
          <td style="border-bottom:#000000 solid 1px;" width="15%">参考值</td>
        </tr>
		<xsl:for-each select="WebReportFile/ReportItem">
        <tr>
          <td><xsl:value-of select="DISPLAYID"/></td>
          <td><xsl:value-of select="ItemEName"/></td>
          <td><xsl:value-of select="TESTITEMNAME"/></td>
          <td>
		  		<xsl:choose>
					<xsl:when test="REPORTVALUE!=''">
						<xsl:value-of select="REPORTVALUE"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="ReportDesc"/>
					</xsl:otherwise>
				</xsl:choose> 
		  </td>
          <td><xsl:value-of select="RESULTSTATUS"/></td>
          <td><xsl:value-of select="UNIT"/></td>
          <td><xsl:value-of select="REFRANGE"/></td>
        </tr>
		</xsl:for-each>
		<tr>
          <td colspan="7"></td>
          </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0"  style="margin-bottom:10px;border-bottom:#000000 solid 1px;">
        <tr>
          <xsl:for-each   select= "WebReportFile/ReportGraph">
            <xsl:if test="position() &lt;= 5">
              <td height="150" align="CENTER">
			  <xsl:element name="img">
                  <xsl:attribute name="src">
				   <xsl:choose>
						<xsl:when test="Url!=''">
							<xsl:value-of select="Url"/>/<xsl:value-of select="GraphName"/>.<xsl:value-of select="Type"/> 
						</xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="GraphName"/>.<xsl:value-of select="Type"/> 
						</xsl:otherwise>
					</xsl:choose>       
				  </xsl:attribute>
                  <xsl:attribute name="width"> <xsl:value-of select="130"/> </xsl:attribute>
                  <xsl:attribute name="height"> <xsl:value-of select="130"/> </xsl:attribute>
              </xsl:element>
              </td>
            </xsl:if>
          </xsl:for-each>
        </tr>
        <tr>
          <td height="30" align="CENTER">RBC DISCRI</td>
          <td align="CENTER">PLT DISCRI</td>
          <td align="CENTER">DIFF SCAT</td>
          <td align="CENTER">BASO SCAT</td>
          <td align="CENTER">RET SCAT</td>
        </tr>
      </table>
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
	  <xsl:for-each select="WebReportFile/ReportForm">
        <tr style="height:22px; line-height:22px;">
          <td>检验日期：<xsl:value-of select="TESTDATE"/></td>
          <td>报告日期：!creatdate!</td>
          <td>检验者:<span style="vertical-align:middle; margin:1px;">
		 <xsl:choose>
				<xsl:when test="TechnicianUrl!=''">
				
				  <xsl:element name="img">
					<xsl:attribute name="src">
					  <xsl:value-of select="TechnicianUrl"/>
					</xsl:attribute>
					<xsl:attribute name="width">
					<!--修改电子签名图片的宽度-->
					  <xsl:value-of select="60"/>
					</xsl:attribute>
					<xsl:attribute name="height">
					<!--修改电子签名图片的高度-->
					  <xsl:value-of select="25"/>
					</xsl:attribute>
				  </xsl:element>
				</xsl:when>
				<xsl:otherwise>
				  <xsl:value-of select="TECHNICIAN"/>
				</xsl:otherwise>
				
		   </xsl:choose></span>
		  </td>
          <td>核对者：<span style="vertical-align:middle; margin:1px;">
		   <xsl:choose>
				<xsl:when test="CheckerUrl!=''">
				
				  <xsl:element name="img">
					<xsl:attribute name="src">
					  <xsl:value-of select="CheckerUrl"/>
					</xsl:attribute>
					<xsl:attribute name="width">
					<!--修改电子签名图片的宽度-->
					  <xsl:value-of select="60"/>
					</xsl:attribute>
					<xsl:attribute name="height">
					<!--修改电子签名图片的高度-->
					  <xsl:value-of select="25"/>
					</xsl:attribute>
				  </xsl:element>
				</xsl:when>
				<xsl:otherwise>
				  <xsl:value-of select="CHECKER"/>
				</xsl:otherwise>
				
		   </xsl:choose></span>
        </td>
        </tr>
		</xsl:for-each>
        <tr>
          <td colspan="4">声明：此报告只对该样本负责！</td>
          </tr>
      </table></td>
  </tr>
</table>
</body></html>

</xsl:template>
</xsl:stylesheet>