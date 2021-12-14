<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" exclude-result-prefixes="xsl msxsl ddwrt" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
xmlns:ddwrt="http://schemas.microsoft.com/WebParts/v2/DataView/runtime" xmlns:asp="http://schemas.microsoft.com/ASPNET/20" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
xmlns:ms="urn:schemas-microsoft-com:xslt"
xmlns:SharePoint="Microsoft.Sharepoint.WebControls">
	<xsl:output method="html" encoding="utf-8" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
	<xsl:template match="/">
		<html xmlns="http://www.w3.org/1999/xhtml">
			<head>
				<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
				<title>Untitled Document</title>
				<script language="javascript">function ss(){}</script>
			</head>
			<body onload="alert('aaa');">
				<!--theResultYouWant复制结果时，需要复制的列配置项，value="1,2,3"则代表复制序号、中文名、英文名，以此类推 -->
				<input id="theResultYouWant" type="hidden" value="1,2,3,4,5,6,7"></input>
				<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size:12px;text-align:left; ">
					<tr>
						<td>
							<table id="yishu" style="width:100%;">
								<tr>
									<td colspan='3' style="padding:10px 5px;font-weight:bold;font-size:13px;border-bottom:2px solid #e0e0e0;">医嘱信息</td>
									<!--复制按钮开始 -->
									<td colspan='1' style="padding:10px 5px;font-weight:bold;font-size:13px;border-bottom:2px solid #e0e0e0;">
										<input  style="padding:2px" type="button" onclick="copyNormalUnusual()"  value="复制异常结果"></input>
									</td>
									<td colspan='1' style="padding:10px 5px;font-weight:bold;font-size:13px;border-bottom:2px solid #e0e0e0;">
										<input  style="padding:2px" type="button" onclick="copyNormal()"  value="复制结果"></input>
									</td>
									<!--复制按钮结束 -->
								</tr>
								<xsl:for-each select="WebReportFile/ReportForm">
									<tr>
										<td style="padding:5px;padding-top:10px;">
											姓名：
											<xsl:value-of select="CNAME"/>
										</td>
										<td style="padding:5px;padding-top:10px;">
											性别：
											<xsl:value-of select="GENDERNAME"/>
										</td>
										<td style="padding:5px;padding-top:10px;">
											年龄：
											<xsl:value-of select="AGE"/>
											<xsl:value-of select="AGEUNITNAME"/>
										</td>
										<td style="padding:5px;padding-top:10px;">
											病历号：
											<xsl:value-of select="PATNO"/>
										</td>
										<td style="padding:5px;padding-top:10px;">
											科室：
											<xsl:value-of select="DEPTNAME"/>
										</td>
									</tr>
									<tr>
										<td style="padding:5px;">
											病房：
											<xsl:value-of select="WARDNO"/>
										</td>
										<td style="padding:5px;">
											床号：
											<xsl:value-of select="BED"/>
										</td>
										<td style="padding:5px;">
											医生：
											<xsl:value-of select="DOCTOR"/>
										</td>
										<td style="padding:5px;">
											检验者：
											<xsl:value-of select="TECHNICIAN"/>
										</td>
										<td style="padding:5px;">
											录入者：
											<xsl:value-of select="OPERATOR"/>
										</td>
									</tr>
									<tr>
										<td style="padding:5px;">
											审核者：
											<xsl:value-of select="CHECKER"/>
										</td>
										<td style="padding:5px;">
											临床诊断：
											<xsl:value-of select="FORMMEMO"/>
										</td>
										<td style="padding:5px;">
											检验小组：
											<xsl:value-of select="SECTIONNAME"/>
										</td>
										<td style="padding:5px;">
											操作日期：
											<xsl:value-of select="substring-before(TESTDATE,' 0')"/>
										</td>
										<td style="padding:5px;">
											操作时间：
											<xsl:value-of select="substring-before(TESTTIME,':000')"/>
										</td>
									</tr>
									<tr>
										<td colspan='2' style="padding:5px;padding-bottom:10px;">
											备注：
											<xsl:value-of select="FORMMEMO"/>
										</td>
										<td colspan='3' style="padding:5px;padding-bottom:10px;">
											细菌描述：
											<xsl:value-of select="DISTRICTNO"/>
											<input id="hidden_patno" type="hidden" value="{PATNO}"></input>
										</td>
									</tr>
								</xsl:for-each>
							</table>
						</td>
					</tr>
					<tr>
						<td style="border-top:2px solid #e0e0e0;">
							<table id="normalxsl" class="normitem" style="width:100%;">
								<tr style=" font-size:13px; font-weight:bold;border-bottom:2px solid #e0e0e0;">
									<!--复选框开始-->
									<td style="padding:10px 5px;width:40px">
										<input type="checkbox" id="allboxs"  onclick="allcheck()" ></input>
									</td>
									<!--复选框结束-->
									<td style="padding:10px 5px;width:40px">序号</td>
									<td style="padding:10px 5px;">检验项目</td>
									<td style="padding:10px 5px;">英文名称</td>
									<td style="padding:10px 5px;">结果</td>
									<td style="padding:10px 5px;">状态</td>
									<td style="padding:10px 5px;">单位</td>
									<td style="padding:10px 5px;">参考值</td>
                  <td style="padding:10px 5px;">临床意义</td>
								</tr>
								<xsl:for-each select="WebReportFile/ReportItem">
									<tr id="tr_{position()}"
									onclick="parent.printResult(document.getElementById('hidden_patno').value,'{ITEMNO}','item','{RECEIVEDATE}');(document.getElementById('tmptrid').value=='tr_{position()}')?document.getElementById(document.getElementById('tmptrid').value).style.background='#e0e0e0':document.getElementById(document.getElementById('tmptrid').value).style.background='#ffffff';document.getElementById('tmptrid').value='tr_{position()}';">
										<xsl:if test="(position() mod 2) = 0">
											<xsl:attribute name="style">height:24px; line-height:24px;background:#e0e0e0;</xsl:attribute>
											<xsl:attribute name="onmouseover">this.style.background='#cbdaf0';</xsl:attribute>
											<xsl:attribute name="onmouseout">this.style.background='#e0e0e0';</xsl:attribute>
										</xsl:if>
										<xsl:if test="(position() mod 2) = 1">
											<xsl:attribute name="style">height:24px; line-height:24px;background:#ffffff;</xsl:attribute>
											<xsl:attribute name="onmouseover">this.style.background='#cbdaf0';</xsl:attribute>
											<xsl:attribute name="onmouseout">this.style.background='#ffffff';</xsl:attribute>
										</xsl:if>
										<!--复选框开始-->
										<td style="padding:10px 5px;width:40px"> 
											<input type="checkbox" name="boxs" onclick="" rownum="{position()}"></input>
										</td>
										<!--复选框结束-->
										<td style="text-align:center;width:25px;padding;5px;">
											<xsl:value-of select="position()"/>
											<xsl:if test="position() =1 ">
												<table>
													<tr style="display:none;" >
														<td  id="aaa" onclick="parent.printResult('{PATNO}','{ITEMNO}','item','{RECEIVEDATE}');document.getElementById('tr_{position()}').style.background='#2394af';document.getElementById('tmptrid').value='tr_{position()}'" >111</td>
													</tr>
												</table>
											</xsl:if>
										</td>
										<td style="padding;5px;">
											<xsl:value-of select="ITEMCNAME"/>
										</td>
										<td style="padding;5px;">
											<xsl:value-of select="ITEMENAME"/>
										</td>
										<td style="padding;5px;">
											<xsl:choose>
												<xsl:when test="RESULTSTATUS='H'">
													<span style="color:red;">
														<xsl:value-of select="REPORTVALUE"/>
													</span>
												</xsl:when>
												<xsl:when test="RESULTSTATUS='L'">
													<span style="color:blue;">
														<xsl:value-of select="REPORTVALUE"/>
													</span>
												</xsl:when>
												<xsl:when test="RESULTSTATUS='LL'">
													<table style="color:yellow;width:100%;background-color: #008080">
														<tr>
															<td>
																<xsl:value-of select="REPORTVALUE"/>
															</td>
														</tr>
													</table>
												</xsl:when>
												<xsl:when test="RESULTSTATUS='HH'">
													<table style="color:yellow;width:100%;background-color: #008080">
														<tr>
															<td>
																<xsl:value-of select="REPORTVALUE"/>
															</td>
														</tr>
													</table>
												</xsl:when>
												<xsl:otherwise>
													<xsl:value-of select="REPORTVALUE"/>
												</xsl:otherwise>
											</xsl:choose>
											<xsl:value-of select="REPORTDESC"/>
										</td>
										<td style="padding;5px;">
											<xsl:choose>
												<xsl:when test="RESULTSTATUS='H'">
													<span style="color:red;">
														<xsl:value-of select="RESULTSTATUS"/>
													</span>
												</xsl:when>
												<xsl:when test="RESULTSTATUS='L'">
													<span style="color:blue;">
														<xsl:value-of select="RESULTSTATUS"/>
													</span>
												</xsl:when>
												<xsl:when test="RESULTSTATUS='LL'">
													<table style="color:yellow;width:100%;background-color: #008080">
														<tr>
															<td>
																<xsl:value-of select="RESULTSTATUS"/>
															</td>
														</tr>
													</table>
												</xsl:when>
												<xsl:when test="RESULTSTATUS='HH'">
													<table style="color:yellow;width:100%;background-color: #008080">
														<tr>
															<td>
																<xsl:value-of select="RESULTSTATUS"/>
															</td>
														</tr>
													</table>
												</xsl:when>
												<xsl:otherwise>
													<xsl:value-of select="RESULTSTATUS"/>
												</xsl:otherwise>
											</xsl:choose>
										</td>
										<td style="padding;5px;">
											<xsl:value-of select="UNIT"/>
										</td>
										<td style="">
											<xsl:value-of select="REFRANGE"/>
										</td>
                    <td style="">
                      
                      <button  onclick="window.open('../../ZhiFang.ReportFormQueryPrint/ui_new/class/queryTestItem/index.html?itemno='+'{ITEMNO}','临床意义','width=350, height=250 ,top=300,left=700,toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no,alwaysRaised=yes,depended=yes')">
                        查看
                      </button>
                    </td>
									</tr>
								</xsl:for-each>
							</table>
							<input id="tmptrid" type="hidden" value="tr_1"></input>
						</td>
					</tr>
				</table>
				<table width="100%" cellspacing="0" cellpadding="0" style="font-size:12px;text-align:left;margin-top:5px; ">
					<tr>
						<td colspan='3' style="padding:10px 5px;font-weight:bold;font-size:13px;
						border-bottom:2px solid #e0e0e0;border-top:2px solid #e0e0e0;">图片结果</td>
					</tr>
					<xsl:for-each select="WebReportFile/ReportGraph">
						<tr style="height:24px; line-height:24px;background:#ffffff;" id="tr_{position()}"  >
							<td style="text-align:center;width:25px;padding;5px;">
								<xsl:value-of select="position()"/>
							</td>
							<td style="padding;5px;">
								<xsl:value-of select="GraphName"/>
							</td>
							<td style="padding;5px;">
								<xsl:choose>
									<xsl:when test="pointtype='8'">
										<iframe width="700" height="800">
											<xsl:attribute name="src">
												../
												<xsl:value-of select="FilePath"/>
											</xsl:attribute>
										</iframe>
									</xsl:when>
									<xsl:otherwise>
										<img onload="(this.width>700)?this.style.width='100%':null" >
											<xsl:attribute name="src">
												data:image/jpeg;base64,
												<xsl:value-of select="Base64StrContent"/>
											</xsl:attribute>
										</img>
									</xsl:otherwise>
								</xsl:choose>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>