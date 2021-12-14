<?xml version="1.0" encoding="gb2312"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" version="1.0" encoding="GB2312" indent="yes"/>
<xsl:template match="NewDataSet/Table">
		<treenode>
			<xsl:attribute name="Text"><xsl:value-of select="title"/></xsl:attribute>
			<xsl:attribute name="Time"><xsl:value-of select="buildtime"/></xsl:attribute>
			<xsl:attribute name="pic"><xsl:value-of select="pic"/></xsl:attribute>
			<xsl:attribute name="passed"><xsl:value-of select="passed"/></xsl:attribute>
					<keyword><xsl:value-of select="keyword"/></keyword>
					<digester><xsl:value-of select="digester"/></digester>
<text><xsl:value-of select="text"/></text>
<writer><xsl:value-of select="writer"/></writer>
<source><xsl:value-of select="source"/></source>
<writer><xsl:value-of select="writer"/></writer>
<verifier><xsl:value-of select="verifier"/></verifier>
		</treenode>
</xsl:template>
</xsl:stylesheet>
