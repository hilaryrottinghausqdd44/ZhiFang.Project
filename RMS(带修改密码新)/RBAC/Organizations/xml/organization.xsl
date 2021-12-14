<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:template match="/">
		<table border="1" width="95%" cellspacing="0" cellpadding="2" align="center" style="BORDER-COLLAPSE:collapse">
			<tr bgcolor="#e0e0e0">
				<td align="center" nowrap="nowrap">员工号</td>
				<td align="left" nowrap="nowrap">姓名</td>
				<td align="left" nowrap="nowrap">账号名</td>
				<td align="left" nowrap="nowrap">部门(职位)</td>
				<td align="center" nowrap="nowrap">电子邮件</td>
				<td align="center" nowrap="nowrap">手机</td>
			</tr>
			<xsl:for-each select="NewDataSet/Table">
				<tr id="NM{position()}" bgcolor="white" onclick="SelectEmpl('{position()}','{EmpID}' )" ondblclick="EditPerson('{EmpID}')" onmouseover="this.bgColor='LemonChiffon'" onmouseout="this.bgColor=''">
					<td>
						<xsl:value-of select="EmpSN"/>
					</td>
					<td>
						<xsl:value-of select="EmpNameL"/>
						<xsl:value-of select="EmpNameF"/>
					</td>
					<td>
						<xsl:value-of select="Account"/>
					</td>
					<td id="tdDEPT{position()}">
						<input type="hidden" id="inputDept{position()}" value="{Dept}"/>
						<script language="javascript">
							tdDEPT<xsl:value-of select="position()"/>.innerHTML=inputDept<xsl:value-of select="position()"/>.value;
							//alert(inputDept<xsl:value-of select="position()"/>.value);
						</script>
					</td>
					<td>
						<xsl:value-of select="Email"/>
					</td>
					<td>
						<xsl:value-of select="Mobile"/>
					</td>
				</tr>
			</xsl:for-each>
		</table>
	</xsl:template>
</xsl:stylesheet>
