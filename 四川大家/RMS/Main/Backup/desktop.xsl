<?xml version="1.0" encoding="GB2312"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format">
	<xsl:output method="html" encoding="GB2312" omit-xml-declaration="yes" standalone="yes" doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN" indent="yes"/>
	<xsl:template match="TREENODES">
		<html xmlns="http://www.w3.org/1999/xhtml">
			<head>
				<title>桌面配置</title>
				<meta http-equiv="Content-Type" content="text/html; charset=gb2312"/>
				
				<link href="../style.css" rel="stylesheet" type="text/css"/>
				<script language="javascript" src="calendar.js"></script>
				<script language="javascript">
				<![CDATA[
					function onResize()
					{
						setday(document.all.text1);
						document.all.text1.focus();
						window.status=document.all.text1.clientX;
						window.status +=":" + document.all.text1.clientY;
						//document.all.meizzDateLayer.style.top=document.all.meizzDateLayer.style.top-50;
						//document.all.meizzDateLayer.style.left=20 + document.all.meizzDateLayer.left.top;
						//alert(document.all.TDCalendar.style.left);
						//document.all.meizzDateLayer.style.left=715;
						//document.all.meizzDateLayer.style.top=25;
					}
					function window_onload()
					{
						setday(document.all.text1);
					}
				]]>
				</script>
			</head>
			<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" bgcolor="#E3E3E3" language="javascript" onresize="onResize()" onload="return window_onload();">
				<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#ffffff"> 
					<tr>
						<td valign="top">
							<table width="100%" border="0" cellpadding="0" cellspacing="0" id="Desktop" >
								<tr>
									<td height="14"/>
								</tr>
								<tr>
									<td valign="top">
										
										<table width="100%" border="0" cellpadding="0" cellspacing="0">
											<tr>
												<td><img name="" src="" width="1" height="1" alt=""/>
												</td>
												<td valign="top">
													<xsl:for-each select="treenode/treenode[@Text='左侧']/treenode">
													<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td height="21"><table width="100%" height="21" border="0" cellpadding="0" cellspacing="0">
															<tr>
															<td width="13" background="images/share/1.jpg"/>                                                
	                                                          　
															<td background="images/share/3.jpg"><table width="100%" height="21" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td height="3" colspan="2"/>                                                
																</tr>
																<tr>
																	<td><img src="{@ImageUrl}"/></td>
																	<td class="news" nowrap="true" title="{@Text}">
																		<xsl:if test="string-length(@Text) &gt; 10">...</xsl:if>
																		<xsl:value-of select="substring(@Text,1,30)"/>
																	</td>
																</tr>
															</table></td>
															<td width="138" background="images/share/2.jpg"/>                                                
	                                                          　 
														</tr>
														</table></td>
													</tr>
													<tr>
														<td ><table width="100%" border="0" cellpadding="0" cellspacing="0">
															<tr>
															<td><table width="100%" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td width="1" bgcolor="#CCCCCC"><img name="" src="" width="1" height="1" alt=""/> </td>                                               
																	<td valign="bottom" bgcolor="F7FBFF"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
																		<tr>
																		<td height="5"/>                                                
																	</tr>
																		<xsl:for-each select="treenode">
																		<tr>
																		<td valign="bottom"><table width="100%" border="0" cellspacing="0" cellpadding="0">
																			<tr>
																				<td width="3"><img name="" src="" width="8" height="1" alt=""/></td>                                                
																				<td width="2%">
																				<xsl:if test="@Itemtype='dir'">
																					<img src="../../Documents/images/folder16.gif" width="16" height="16"/>
																				</xsl:if>
																				<xsl:if test="@Itemtype='file'">
																					<img src="../../Documents/images/file16.gif" width="16" height="16"/>
																				</xsl:if>
																				<xsl:if test="not(@Itemtype='file') and not(@Itemtype='dir')">
																				
																					<img src="{../@ImageUrl}" width="16" height="16"/>
																				</xsl:if>
																				 </td>
																				<td width="1"><img name="" src="" width="3" height="1" alt=""/></td>                                                
																				<td width="94%" nowrap="true" height="18" class="text" style="overflow:hidden">
																				
																				<a href="#" class="text">
																					<xsl:if test="@Tooltip='新闻'">
																						<xsl:attribute name="onclick">javascript:window.open('<xsl:value-of select="@NavigateUrl"/>?<xsl:value-of select="@Para"/>','_blank','width=800,height=600,scrollbars=1,resizable=yes,top=0,left=200')</xsl:attribute>
																						<xsl:value-of select="substring(@Text,1,20)"/>
																					</xsl:if>
																					<xsl:if test="not(@Tooltip='新闻')">
																						<xsl:choose >
																							<xsl:when test="@Itemtype='file'">
																								<xsl:attribute name="onclick">javascript:window.open('../../documents/download.aspx?file=\\' + escape('<xsl:value-of select="@Para"/>'),'frmDownload')</xsl:attribute>
																								<xsl:value-of select="substring(@Text,1,20)"/>
																							</xsl:when>
																							<xsl:otherwise>
																								<xsl:attribute name="onclick">javascript:window.open('<xsl:value-of select="@NavigateUrl"/>?Folder=\\' + escape('<xsl:value-of select="@Para"/>'),'_self')</xsl:attribute>
																								<xsl:value-of select="substring(@Text,1,20)"/>
																								
																							</xsl:otherwise>
																						</xsl:choose>
																						
																					</xsl:if>
																				</a> </td>
																			</tr>
																		</table></td><iframe id="frmDownload" name="frmDownload" src="" width="0" height="0"></iframe>
																		</tr>
																		</xsl:for-each>
																		<tr>
																		<td>
																		<table width="100%" border="0" cellspacing="0" cellpadding="0">
																			<tr><td height="7"></td></tr>
																			<tr>
																				<td >
																				<xsl:if test="count(treenode) > 0">
																					<div align="right">
																						<a>
																							<xsl:attribute name="href"><xsl:value-of select="@NavigateUrl"/>?<xsl:value-of select="@Para"/></xsl:attribute>
																							<img src="images/news/more.jpg" width="30" height="11" border="0"/>
																						</a>
																					</div>
																				</xsl:if>
																				</td>
																				<td width="11"/>                                                
																			</tr>
																			<tr><td height="7"></td></tr>
																		</table></td>
																		</tr>
																	</table></td>
																	<td width="1" bgcolor="#CCCCCC"><img name="" src="" width="1" height="1" alt=""/></td>                                                
																</tr>
															</table></td>
															</tr>
															<tr>
																<td height="1" bgcolor="21559C"/>                                                
															</tr>
														</table>
														
														</td>
													</tr>
													</table>
													<br/>

													</xsl:for-each>
													
													
											  </td>
												<td width="12">
													<img name="" src="" width="12" height="1" alt=""/>
												</td>
												<td valign="top">
													<xsl:for-each select="treenode/treenode[@Text='右侧']/treenode">
													<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
													<tr>
														<td height="21"><table width="100%" height="21" border="0" cellpadding="0" cellspacing="0">
															<tr>
															<td width="13" background="images/share/1.jpg"/>                                                
	                                                          　
															<td background="images/share/3.jpg"><table width="100%" height="21" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td height="3" colspan="2"/>                                                
																</tr>
																<tr>
																	<td><img src="{@ImageUrl}"/></td>
																	<td class="news" nowrap="true"><xsl:value-of select="@Text"/></td>
																</tr>
															</table></td>
															<td width="138" background="images/share/2.jpg"/>                                                
	                                                          　 
														</tr>
														</table></td>
													</tr>
													<tr>
														<td ><table width="100%" border="0" cellpadding="0" cellspacing="0">
															<tr>
															<td><table width="100%" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td width="1" bgcolor="#CCCCCC"><img name="" src="" width="1" height="1" alt=""/> </td>                                               
																	<td valign="bottom" bgcolor="F7FBFF"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
																		<tr>
																		<td height="5"/>                                                
																	</tr>
																		<xsl:for-each select="treenode">
																		<tr>
																		<td valign="bottom"><table width="100%" border="0" cellspacing="0" cellpadding="0">
																			<tr>
																				<td width="3"><img name="" src="" width="8" height="1" alt=""/></td>                                                
																				<td width="2%">
																					<xsl:if test="@Itemtype='dir'">
																						<img src="../../Documents/images/folder16.gif" width="16" height="16"/>
																					</xsl:if>
																					<xsl:if test="@Itemtype='file'">
																						<img src="../../Documents/images/file16.gif" width="16" height="16"/>
																					</xsl:if>
																					<xsl:if test="not(@Itemtype='file') and not(@Itemtype='dir')">
																					
																						<img src="{../@ImageUrl}" width="16" height="16"/>
																					</xsl:if>
																				 </td>
																				<td width="1"><img name="" src="" width="3" height="1" alt=""/></td>                                                
																				<td width="94%" nowrap="true" height="18" class="text" style="overflow:hidden">
																				<a href="#" class="text">
																					<xsl:if test="@Tooltip='新闻'">
																						<xsl:attribute name="onclick">javascript:window.open('<xsl:value-of select="@NavigateUrl"/>?<xsl:value-of select="@Para"/>','_blank','width=800,height=600,scrollbars=1,resizable=yes,top=0,left=200')</xsl:attribute>
																						<xsl:value-of select="substring(@Text,1,20)"/>
																					</xsl:if>
																					<xsl:if test="not(@Tooltip='新闻')">
																						<xsl:choose >
																							<xsl:when test="@Itemtype='file'">
																								<xsl:attribute name="onclick">javascript:window.open('../../documents/download.aspx?file=\\' + escape('<xsl:value-of select="@Para"/>'),'frmDownload')</xsl:attribute>
																								<xsl:value-of select="substring(@Text,1,20)"/>
																							</xsl:when>
																							<xsl:otherwise>
																								<xsl:attribute name="onclick">javascript:window.open('<xsl:value-of select="@NavigateUrl"/>?Folder=\\' + escape('<xsl:value-of select="@Para"/>'),'_self')</xsl:attribute>
																								<xsl:value-of select="substring(@Text,1,20)"/>
																								
																							</xsl:otherwise>
																						</xsl:choose>
																						
																					</xsl:if>
																				</a> </td>
																			</tr>
																		</table></td>
																		</tr>
																		</xsl:for-each>
																		<tr>
																		<td><table width="100%" border="0" cellspacing="0" cellpadding="0">
																			<tr><td height="7"></td></tr>
																			<tr>
																				<td >
																					<xsl:if test="count(treenode) > 0">
																						<div align="right">
																							<a>
																								<xsl:attribute name="href"><xsl:value-of select="@NavigateUrl"/>?<xsl:value-of select="@Para"/></xsl:attribute>
																								<img src="images/news/more.jpg" width="30" height="11" border="0"/>
																							</a>
																						</div>
																					</xsl:if>
																				</td>
																				<td width="11"/>                                                
																			</tr>
																			<tr><td height="7"></td></tr>
																		</table></td>
																		</tr>
																	</table></td>
																	<td width="1" bgcolor="#CCCCCC"><img name="" src="" width="1" height="1" alt=""/></td>                                                
																</tr>
															</table></td>
															</tr>
															<tr>
															<td height="1" bgcolor="21559C"/>                                                
														</tr>
														</table>
														
														</td>
													</tr>
													</table>
													<br/>

													</xsl:for-each>
											  </td>
												<td width="12">
													<img name="" src="" width="12" height="1" alt=""/>
												</td>
												<td valign="top" width="172">
													<table width="172" border="0" align="right" cellpadding="0" cellspacing="0">
														<tr>
															<td width="5" background="images/middle/left.jpg"></td>
															<td width="162"><table width="162" border="0" cellpadding="0" cellspacing="0">
															<tr>
																<td><table width="162" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td height="10" background="images/middle/t1.jpg"></td>
																</tr>
																<tr>
																	<td height="152" id="TDCalendar" align="left">
																	<input type="text" id="text1" onfocus="setday(this)" name="text1" style="WIDTH: 0px; HEIGHT:0px;	LEFT: 10px; POSITION: relative; TOP: -194px" size="78"></input>
																	<script language="javascript">
																		//setday(document.all.text1);
																		//document.all.meizzDateLayer.style.left=615;
																		//document.all.meizzDateLayer.style.top=25;
																		document.all.TDCalendar.height=document.all.meizzDateLayer.style.height;
																		//document.all.meizzDateLayer.style.width=174
																		//alert(document.all.meizzDateLayer.style.width);
																	</script>
																	</td>
																</tr>
																<tr>
																	<td height="10" background="images/middle/t3.jpg"></td>
																</tr>
																</table></td>
															</tr>
															<tr>
																<td ><table width="162" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td ><table width="162" border="1" cellpadding="0" cellspacing="1" bordercolor="#CCCCCC">
																	<tr>
																		<td valign="middle"><table width="154" border="0" align="center" cellpadding="0" cellspacing="0">
																		<tr>
																			<td height="27" valign="top"><table width="154" height="27" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="98CAF2">
																			
																			<tr>
																				<td class="tool"><div align="center">::工具箱::</div></td>
																			</tr>
																			
																			</table></td>
																		</tr>
																		<tr>
																			<td valign="top"><table width="154" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="EBF7FB">
																			<xsl:for-each select="treenode[@NodeData='RemoteTools']/treenode">
																				<tr>
																					<td align="center" width="40"><img border="0" height="16" width="16"><xsl:attribute name="src"><xsl:value-of select="@ImageUrl"/></xsl:attribute></img></td>
																					<td class="text" height="23" align="left"><div align="left"><a href="#" class="right">
																					<xsl:attribute name="onclick">javascript:window.open(escape('<xsl:value-of select="@NavigateUrl"/>'),'_self','scrollbars=1,top=100,left=200')</xsl:attribute>
																					<xsl:value-of select="@Text"/></a></div></td>
																				</tr>
																			</xsl:for-each>
																			</table></td>
																		</tr>
																		
																		</table></td>
																	</tr>
																	</table></td>
																</tr>
																<tr>
																	<td height="12" background="images/middle/t4.jpg"></td>
																</tr>
																</table></td>
															</tr>
															<tr>
																<td><table width="162" border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td><table width="162" border="1" cellpadding="0" cellspacing="1" bordercolor="#CCCCCC">
																	<tr>
																		<td width="164"><table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
																		<tr>
																			<td height="20" class="tool"><div align="center">::公用工具::</div></td>
																		</tr>
																		<tr>
																			<td valign="top"><table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
																			<tr>
																				<td width="6">
																					<object classid="clsid:0D17F212-7827-4588-B0E9-827E356CDF5A" codebase="../../Includes/Activx/ThCrypt.CAB"
																						id="ThCrypt1" width="" height=""  visible="false" name="ThCrypt">
																						<param name="_Version" value="65536"/>
																						<param name="_ExtentX" value="2646"/>
																						<param name="_ExtentY" value="1323"/>
																						<param name="_StockProps" value="0"/>
																					</object>
																				</td>
																				<td valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
																				<xsl:for-each select="treenode[@NodeData='LocalTools']/treenode">
																					<tr>
																						<td align="right" width="20"><img border="0" height="16" width="16"><xsl:attribute name="src"><xsl:value-of select="@ImageUrl"/></xsl:attribute></img></td>
																						<td class="text" height="23" align="left"><div align="center" style="cursor:hand"><a class="right" id="a{position()}" title="{@NavigateUrl}">
																						<xsl:attribute name="onclick">ThCrypt.ExecCmd(a<xsl:value-of select="position()"/>.title)</xsl:attribute>
																						<xsl:value-of select="@Text"/></a></div></td>
																					</tr>
																				</xsl:for-each>
																				
																				</table></td>
																				<td width="6"></td>
																			</tr>
																			</table></td>
																		</tr>
																		</table></td>
																	</tr>
																	</table></td>
																</tr>
																<tr>
																	<td height="11" background="images/middle/t5.jpg"></td>
																</tr>
																</table></td>
															</tr>
															</table></td>
															<td width="5" background="images/middle/right.jpg"></td>
														</tr>
</table>
												</td>
											</tr>
									  </table>
									</td>
								</tr>
						  </table>
						</td>
						<td width="9" background="images/middle/margin.jpg"/>
                    　
					</tr>
			</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
