<?xml version="1.0" encoding="GB2312"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
<xsl:output method="xml" encoding="GB2312" indent="yes"/>
<xsl:param name="ID1" />
<xsl:template match="TREENODES">

<table height="100%" width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#1571d6" background="images/XSmall/mainHead.jpg" ID="Table1">
		<tr>
			<td width="62%" class="font1" align="center">
<style type="text/css">
<![CDATA[
<!--
body {
	margin-top: 0px;
	background-image: url(images/beijing.jpg);
         font-size: 14px;
}
.menu {background-color:transparent;width:90; height:25;color: white; text-align: center;font-size:12pt;font-weight:bolder}
.submenu {position:absolute;top:40;background-color:lightskyblue;width:120; font-size:10pt}
-->
]]>
</style>

<script language="JavaScript" src="objectSwap.js"></script>
<script language="JavaScript" src="js/menushow.js"></script>						
<div align="center" nowrap="nowrap">

						<xsl:for-each select="treenode/treenode[@Location='top' and  @Checked='True']">
							<A id="d{position()}" style="cursor:hand" onmouseover="show(this,ds{position()});" HREF="?ID1={@NodeData}" class="menu"><xsl:value-of select="@Text"/></A>
							<DIV ID="ds{position()}" CLASS="submenu" style="display:none">
								<xsl:for-each select="./treenode">
                                   <BR/>
                                   <A HREF="?ID1={../@NodeData}&amp;ID2={@NodeData}"  style="cursor:hand"><xsl:value-of select="@Text"/></A>
									
								</xsl:for-each>
							</DIV>
								
						   <!--xsl:if test="position()!=last()"> | </xsl:if-->
						</xsl:for-each>
							

       			</div>
       	</td>
</tr>
   </table>
  </xsl:template>
</xsl:stylesheet>
