<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
<xsl:output method="html" encoding="GB2312" indent="yes" cdata-section-elements="div"/>
	<xsl:template match="/">
	<xsl:variable name="Counts"><xsl:value-of select="count(Tables/Table/tr/td[InputQuery/@Display='Yes'])"/></xsl:variable>
	<xsl:variable name="Cols"><xsl:value-of select="Tables/Table[@InputCols]"/></xsl:variable>
	<div>
	  <table width="776" height="49" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="F3F2F3">
	     <tr class="top">
			<xsl:for-each select="Tables/Table/tr/td[InputQuery/@Display='Yes']">
				<xsl:if test="position() &lt;4">
					<td width="76" height="20" nowrap="true">
						<xsl:value-of select="@ColumnCName"/>
					</td>
					<td width="76" height="20" nowrap="true">
						<input size="15" type="text" name="a{position()}" id="a{position()}" />
					</td>
				</xsl:if>
			</xsl:for-each>
			<td></td>
		</tr>
		<tr class="top">
			<xsl:for-each select="Tables/Table/tr/td[InputQuery/@Display='Yes']">
				<xsl:if test="position() &gt;=4">
					<td width="76" height="20" nowrap="true">
						<xsl:value-of select="@ColumnCName"/>
					</td>
					<td width="76" height="20" nowrap="true">
						<input size="15" type="text" name="a{position()}" id="a{position()}" />
					</td>
				</xsl:if>
			</xsl:for-each>
			<td height="22"><div align="center"><img src="../image/middle/search.jpg" width="63" height="22" border="0" usemap="#Map6" /></div></td>
		</tr>
	  </table>
	 </div>
	</xsl:template>
</xsl:stylesheet>