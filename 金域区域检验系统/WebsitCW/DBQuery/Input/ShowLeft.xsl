<?xml version="1.0" encoding="UTF-8"?>
<!--
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:template match="/">
		<TABLE id="Table1" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="1" width="100%" border="1">
			<TR>
				<TD norap="true" style="WIDTH: 28px">序号</TD>
				<xsl:for-each select="Table/tr[@Type='ShowItem']/td">
					<TD norap="true" ><xsl:value-of select="@ColumnCName"/></TD>				
				</xsl:for-each>
			</TR>
			<xsl:for-each select="Table/tr[@Type='DataItem']">
			<TR  height="26">
				<TD norap="true" class="xl25"  height="26"><xsl:value-of select="position()"/></TD>
				<xsl:for-each select="td">
				<TD norap="true" class="xl32" >
				<xsl:value-of select="text()"/>
				</TD>
				</xsl:for-each>
			</TR>
			</xsl:for-each>
		</TABLE>
	</xsl:template>
</xsl:stylesheet>
-->
<!--要使用的
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:template match="/">
		<TABLE id="Table1" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="1" width="100%" border="1">
			<TR>
				<TD norap="true" style="WIDTH: 28px">序号</TD>
				<xsl:for-each select="Table/tr[@Type='ShowItem']/td">
					<TD norap="true" ><xsl:value-of select="@ColumnCName"/></TD>				
				</xsl:for-each>
			</TR>
			<xsl:for-each select="Table/tr[@Type='DataItem']">
			<TR  height="26">
				<TD norap="true" class="xl25"  height="26"><xsl:value-of select="position()"/></TD>
				<xsl:for-each select="td">
				<xsl:variable name="getValue"><xsl:value-of select="@Column"></xsl:value-of></xsl:variable>
					<TD norap="true" class="xl32" >
						<xsl:if test="../../../Table/tr[@Type='ShowItem']/td[@ColumnEName=$getValue]/@KeyIndex='Yes'">
							<a href="#" onclick="Expound('{text()}')"><xsl:value-of select="text()"/></a>
						</xsl:if>
						<xsl:if test="not(../../../Table/tr[@Type='ShowItem']/td[@ColumnEName=$getValue]/@KeyIndex='Yes')">
							<xsl:value-of select="text()"/>
							
						</xsl:if>
					</TD>
				</xsl:for-each>
			</TR>
			</xsl:for-each>
		</TABLE>		
	</xsl:template>
</xsl:stylesheet>
-->
<!DOCTYPE xsl:stylesheet[<!ENTITY nbsp "&#160;">]>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:template match="/Table">
		<table height="95%" style="BORDER:1px solid skyblue">
			<tr>
				<td valign="top">
					<TABLE id="Table1" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="0" width="100%" border="0" bgcolor="gray">
						<xsl:apply-templates select="tr"/>	
					</TABLE>
				</td>
			</tr>
				
			<tr height="25">
				<td>
					<table width="100%" height="25" border="0" cellpadding="0" cellspacing="0" class="biaoti">
                          <tr>
                            <td width="15">&nbsp;</td>
                            <td width="122" valign="bottom">2/3页(共40记录)</td>
                            <td width="15">&nbsp;</td>
                            <td width="49" valign="bottom"><img src="../image/middle/new1.3_r5_c5.jpg" width="49" height="10" border="0" usemap="#Map" /></td>
                            <td width="15">&nbsp;</td>
                          </tr>
                     </table>
				</td>
			</tr>
			
		</table>
	</xsl:template>
	
	<xsl:template match="tr">
		<tr bgcolor="F2F3F2" height="20">
			<xsl:attribute name="class"><xsl:if test="position()=1">biaoti</xsl:if></xsl:attribute>
			<xsl:if test="position()=1"><td nowrap="true">序号</td></xsl:if>
			<xsl:if test="position()!=1"><td><xsl:value-of select="position()-1"/></td></xsl:if>
			<xsl:apply-templates select="td"/>
		</tr>
	</xsl:template>
	
	<xsl:template match="td">
		<xsl:if test="@EKey"><td nowrap="true"><a href="#" OnClick="QueryString('{@EKey}')"><xsl:value-of select="."/></a></td></xsl:if>
		<xsl:if test="not(@EKey)"><td nowrap="true"><xsl:value-of select="."/></td></xsl:if>
	</xsl:template>
	
</xsl:stylesheet>