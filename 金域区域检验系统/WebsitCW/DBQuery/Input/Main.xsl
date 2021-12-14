<?xml version="1.0" encoding="GB2312"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:template match="/">
	<xsl:param name="SiteMapDoc" select="document('../Dictionaries.xml')" />
	<table width="100%" >
	<tr>
		<td valign="top">
			<TABLE id="Table1" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="1" width="100%" border="0" bgcolor="F5F5F5" >
			<xsl:for-each select="Table/tr/td">
			<xsl:if test="position() &lt;10">
				<TR class="biaoti">
					<xsl:variable name="Column"><xsl:value-of select="@ColumnEName[position()=1]"/></xsl:variable>
					<xsl:variable name="getValue"><xsl:value-of select="../../tr/td[@Column=$Column]"/></xsl:variable>
					<TD norap="true" ><xsl:value-of select="@ColumnCName[position()=1]"/></TD>						
					<xsl:choose>
						<!--����������������-->
						<xsl:when test="@ColumnType=0">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>	
						</xsl:when>
						<xsl:when test="@ColumnType=1">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>	
						</xsl:when>
						<xsl:when test="@ColumnType=2">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=3">
							<TD norap="true" ><a href="#" onclick="javascript:window.open()">����</a>&#20;<a href="#" onclick="javascript:window.open()">ɾ��</a>&#20;<a href="#" onclick="javascript:window.open()">�ϴ�</a></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=4">
							<TD norap="true" ><a href="#" onclick="javascript:window.open()">�����Ϣ</a><a href="#" onclick="javascript:window.open()">�༭��Ϣ</a></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=5">
							<xsl:variable name="TableName"><xsl:value-of select="Dictionary/@DataSourceName"/></xsl:variable>
							<xsl:choose>
								<xsl:when test="@ColumnPrecision=1">
									<TD norap="true" >
										<SELECT name="DropDownList1" style="WIDTH: 152px;">
										<xsl:for-each select="$SiteMapDoc/Tables/Table[@EName=$TableName]/tr">
												<xsl:variable name="TdValue"><xsl:value-of select="td"/></xsl:variable>
												<xsl:if test="$TdValue=$getValue">
													<option selected="true"><xsl:value-of select="td"/></option>													
												</xsl:if>
												<xsl:if test="not($TdValue=$getValue)">
													<option><xsl:value-of select="td"/></option>
												</xsl:if>
											</xsl:for-each>										
										</SELECT>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=2">
									<TD norap="true" >
										<SELECT id="DropDownList{position()}" name="DropDownList{position()}" multiple="true"  style="WIDTH: 152px;">
											<xsl:for-each select="$SiteMapDoc/Tables/Table[@EName=$TableName]/tr">
												<xsl:variable name="TdValue"><xsl:value-of select="td"/></xsl:variable>
												<xsl:if test="$TdValue=$getValue">
													<option selected="true"><xsl:value-of select="td"/></option>
													<option selected="true"><xsl:value-of select="../@EName"/></option>
												</xsl:if>
												<xsl:if test="not($TdValue=$getValue)">
													<option><xsl:value-of select="td"/></option>
												</xsl:if>
											</xsl:for-each>
										</SELECT>
										<script language="javascript">
											CheckOption('DropDownList<xsl:value-of select="position()"/>','<xsl:value-of select="$getValue"/>');
										</script>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=3">
									<TD norap="true" ><xsl:value-of select="../../tr/td[@Column=$Column]"/></TD>
								</xsl:when >
								<xsl:when test="@ColumnPrecision=4">
									<TD norap="true" ><xsl:value-of select="../../tr/td[@Column=$Column]"/></TD>
								</xsl:when>
							</xsl:choose>							
						</xsl:when>
						<xsl:when test="@ColumnType=6">
							<xsl:choose>
								<xsl:when test="@ColumnPrecision=1">
									<TD norap="true" >
										<xsl:if test="$getValue='Yes'">
											<INPUT type="radio" name="{$Column}" id="{$Column}1" checked="true">��</INPUT>
											<INPUT type="radio" name="{$Column}" id="{$Column}2" >��</INPUT>
										</xsl:if>
										<xsl:if test="$getValue='No'">
											<INPUT type="radio" name="{$Column}" id="{$Column}1" >��</INPUT>
											<INPUT type="radio" name="{$Column}" id="{$Column}2" checked="true">��</INPUT>
										</xsl:if>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=2">
									<TD norap="true" >
										<xsl:if test="$getValue='Yes'">
											<INPUT type="checkbox" name="{$Column}" id="{$Column}" checked="true"><xsl:value-of select="@ColumnCName[position()=1]"/></INPUT>											
										</xsl:if>
										<xsl:if test="$getValue='No'">
											<INPUT type="checkbox" name="{$Column}" id="{$Column}" ><xsl:value-of select="@ColumnCName[position()=1]"/></INPUT>
										</xsl:if>
									</TD>
								</xsl:when>
							</xsl:choose>
						</xsl:when>
					</xsl:choose>
				</TR>
			</xsl:if>
			</xsl:for-each>
		</TABLE>
		</td>
		<td  valign="top">
			
			<TABLE id="Table1" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="1" width="100%" border="0" bgcolor="F5F5F5">
			<xsl:for-each select="Table/tr/td">
			<xsl:if test="position() &gt;=8 and position() &lt; 16">
				<TR>
					<xsl:variable name="Column"><xsl:value-of select="@ColumnEName[position()=1]"/></xsl:variable>
					<xsl:variable name="getValue"><xsl:value-of select="../../tr/td[@Column=$Column]"/></xsl:variable>
					<TD norap="true" ><xsl:value-of select="@ColumnCName[position()=1]"/></TD>						
					<xsl:choose>
						<!--����������������-->
						<xsl:when test="@ColumnType=0">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>	
						</xsl:when>
						<xsl:when test="@ColumnType=1">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>	
						</xsl:when>
						<xsl:when test="@ColumnType=2">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=3">
							<TD norap="true" ><a href="#" onclick="javascript:window.open()">����</a>&#20;<a href="#" onclick="javascript:window.open()">ɾ��</a>&#20;<a href="#" onclick="javascript:window.open()">�ϴ�</a></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=4">
							<TD norap="true" ><a href="#" onclick="javascript:window.open()">�����Ϣ</a><a href="#" onclick="javascript:window.open()">�༭��Ϣ</a></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=5">
							<xsl:variable name="TableName"><xsl:value-of select="Dictionary/@DataSourceName"/></xsl:variable>
							<xsl:choose>
								<xsl:when test="@ColumnPrecision=1">
									<TD norap="true" >
										<SELECT name="DropDownList1" style="WIDTH: 152px;">
										<xsl:for-each select="$SiteMapDoc/Tables/Table[@EName=$TableName]/tr">
												<xsl:variable name="TdValue"><xsl:value-of select="td"/></xsl:variable>
												<xsl:if test="$TdValue=$getValue">
													<option selected="true"><xsl:value-of select="td"/></option>													
												</xsl:if>
												<xsl:if test="not($TdValue=$getValue)">
													<option><xsl:value-of select="td"/></option>
												</xsl:if>
											</xsl:for-each>										
										</SELECT>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=2">
									<TD norap="true" >
										<SELECT id="DropDownList{position()}" name="DropDownList{position()}" multiple="true"  style="WIDTH: 152px;">
											<xsl:for-each select="$SiteMapDoc/Tables/Table[@EName=$TableName]/tr">
												<xsl:variable name="TdValue"><xsl:value-of select="td"/></xsl:variable>
												<xsl:if test="$TdValue=$getValue">
													<option selected="true"><xsl:value-of select="td"/></option>
													<option selected="true"><xsl:value-of select="../@EName"/></option>
												</xsl:if>
												<xsl:if test="not($TdValue=$getValue)">
													<option><xsl:value-of select="td"/></option>
												</xsl:if>
											</xsl:for-each>
										</SELECT>
										<script language="javascript">
											CheckOption('DropDownList<xsl:value-of select="position()"/>','<xsl:value-of select="$getValue"/>');
										</script>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=3">
									<TD norap="true" ><xsl:value-of select="../../tr/td[@Column=$Column]"/></TD>
								</xsl:when >
								<xsl:when test="@ColumnPrecision=4">
									<TD norap="true" ><xsl:value-of select="../../tr/td[@Column=$Column]"/></TD>
								</xsl:when>
							</xsl:choose>							
						</xsl:when>
						<xsl:when test="@ColumnType=6">
							<xsl:choose>
								<xsl:when test="@ColumnPrecision=1">
									<TD norap="true" >
										<xsl:if test="$getValue='Yes'">
											<INPUT type="radio" name="{$Column}" id="{$Column}1" checked="true">��</INPUT>
											<INPUT type="radio" name="{$Column}" id="{$Column}2" >��</INPUT>
										</xsl:if>
										<xsl:if test="$getValue='No'">
											<INPUT type="radio" name="{$Column}" id="{$Column}1" >��</INPUT>
											<INPUT type="radio" name="{$Column}" id="{$Column}2" checked="true">��</INPUT>
										</xsl:if>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=2">
									<TD norap="true" >
										<xsl:if test="$getValue='Yes'">
											<INPUT type="checkbox" name="{$Column}" id="{$Column}" checked="true"><xsl:value-of select="@ColumnCName[position()=1]"/></INPUT>											
										</xsl:if>
										<xsl:if test="$getValue='No'">
											<INPUT type="checkbox" name="{$Column}" id="{$Column}" ><xsl:value-of select="@ColumnCName[position()=1]"/></INPUT>
										</xsl:if>
									</TD>
								</xsl:when>
							</xsl:choose>
						</xsl:when>
					</xsl:choose>
				</TR>
			</xsl:if>
			</xsl:for-each>
		</TABLE>
		</td>
	</tr>
	<tr>
		<td width="100%" colspan="2" height="40"></td>
	</tr>
	
	</table>
		
	</xsl:template>
</xsl:stylesheet>
<!--
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:template match="/">
		<TABLE id="Table1" style="FONT-SIZE: 9pt" cellSpacing="1" cellPadding="0" width="100%" border="0" bgColor="gray">
			<xsl:for-each select="Table/tr/td">							
				<TR bgColor="white">
					<xsl:variable name="Column"><xsl:value-of select="@ColumnEName[position()=1]"/></xsl:variable>
					<xsl:variable name="getValue"><xsl:value-of select="../../tr/td[@Column=$Column]"/></xsl:variable>
					<TD norap="true" ><xsl:value-of select="@ColumnCName[position()=1]"/></TD>						
					<xsl:choose>
						
						<xsl:when test="@ColumnType=0">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>	
						</xsl:when>
						<xsl:when test="@ColumnType=1">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>	
						</xsl:when>
						<xsl:when test="@ColumnType=2">
							<TD norap="true" ><input type="text" name="{$Column}" id="{$Column}" value="{$getValue}"/></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=3">
							<TD norap="true" ><a href="#" onclick="javascript:window.open()">�ĵ�����</a>&#20;<a href="#" onclick="javascript:window.open()">ɾ���ĵ�</a>&#20;<a href="#" onclick="javascript:window.open()">�ϴ��ĵ�</a></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=4">
							<TD norap="true" ><a href="#" onclick="javascript:window.open()">�����Ϣ</a><a href="#" onclick="javascript:window.open()">�༭��Ϣ</a></TD>
						</xsl:when>
						<xsl:when test="@ColumnType=5">
							<xsl:variable name="TableName"><xsl:value-of select="Dictionary/@DataSourceName"/></xsl:variable>
							<xsl:choose>
								<xsl:when test="@ColumnPrecision=1">
									<TD norap="true" >
										<SELECT name="DropDownList1">
											<xsl:for-each select="../../tr/Table[@EName=$TableName]/tr">
											<xsl:variable name="TdValue"><xsl:value-of select="td"/></xsl:variable>
												<xsl:if test="$TdValue=$getValue">
													<option selected="true"><xsl:value-of select="td"/></option>
												</xsl:if>
												<xsl:if test="not($TdValue=$getValue)">
													<option><xsl:value-of select="td"/></option>
												</xsl:if>												
											</xsl:for-each>		
										</SELECT>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=2">
									<TD norap="true" >
										<SELECT id="DropDownList{position()}" name="DropDownList{position()}" multiple="true">
											<xsl:for-each select="../../tr/Table[@EName=$TableName]/tr">
											<xsl:variable name="TdValue"><xsl:value-of select="td"/></xsl:variable>
												<xsl:if test="$TdValue=$getValue">
													<option selected="true"><xsl:value-of select="td"/></option>
												</xsl:if>
												<xsl:if test="not($TdValue=$getValue)">
													<option><xsl:value-of select="td"/></option>
												</xsl:if>
											</xsl:for-each>
										</SELECT>
										<script language="javascript">
											CheckOption('DropDownList<xsl:value-of select="position()"/>','<xsl:value-of select="$getValue"/>');
										</script>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=3">
									<TD norap="true" ><xsl:value-of select="../../tr/td[@Column=$Column]"/></TD>
								</xsl:when >
								<xsl:when test="@ColumnPrecision=4">
									<TD norap="true" ><xsl:value-of select="../../tr/td[@Column=$Column]"/></TD>
								</xsl:when>
							</xsl:choose>							
						</xsl:when>
						<xsl:when test="@ColumnType=6">
							<xsl:choose>
								<xsl:when test="@ColumnPrecision=1">
									<TD norap="true" >
										<xsl:if test="$getValue='Yes'">
											<INPUT type="radio" name="{$Column}" id="{$Column}1" checked="true">��</INPUT>
											<INPUT type="radio" name="{$Column}" id="{$Column}2" >��</INPUT>
										</xsl:if>
										<xsl:if test="$getValue='No'">
											<INPUT type="radio" name="{$Column}" id="{$Column}1" >��</INPUT>
											<INPUT type="radio" name="{$Column}" id="{$Column}2" checked="true">��</INPUT>
										</xsl:if>
									</TD>
								</xsl:when>
								<xsl:when test="@ColumnPrecision=2">
									<TD norap="true" >
										<xsl:if test="$getValue='Yes'">
											<INPUT type="checkbox" name="{$Column}" id="{$Column}" checked="true"><xsl:value-of select="@ColumnCName[position()=1]"/></INPUT>											
										</xsl:if>
										<xsl:if test="$getValue='No'">
											<INPUT type="checkbox" name="{$Column}" id="{$Column}" ><xsl:value-of select="@ColumnCName[position()=1]"/></INPUT>
										</xsl:if>
									</TD>
								</xsl:when>
							</xsl:choose>
						</xsl:when>
					</xsl:choose>
				</TR>
			</xsl:for-each>					
		</TABLE>
	</xsl:template>
</xsl:stylesheet>
-->