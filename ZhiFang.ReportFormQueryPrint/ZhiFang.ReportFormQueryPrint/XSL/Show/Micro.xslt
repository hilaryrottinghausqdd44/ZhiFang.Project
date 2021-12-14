<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="utf-8" doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN" doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>

  <xsl:template match="/">

    <html xmlns="http://www.w3.org/1999/xhtml">
      <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <title>Untitled Document</title>
      </head>

      <body>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size:13px;text-align:left; ">
          <tr>
            <td>
              <table id="yishu" style="width:100%;">
                <tr>
                  <td colspan='5' style="padding:10px 5px;font-weight:bold;font-size:13px;border-bottom:2px solid #e0e0e0;">医嘱信息</td>
                </tr>
                <xsl:for-each select="WebReportFile/ReportForm">
                  <tr>
                    <td style="padding:5px;padding-top:10px;">
                      姓名：<xsl:value-of select="CNAME"/>
                    </td>
                    <td style="padding:5px;padding-top:10px;">
                      性别：<xsl:value-of select="GENDERNAME"/>
                    </td>
                    <td style="padding:5px;padding-top:10px;">
                      年龄：<xsl:value-of select="AGE"/> <xsl:value-of select="AGEUNITNAME"/>
                    </td>
                    <td style="padding:5px;padding-top:10px;">
                      病历号：<xsl:value-of select="PATNO"/>
                    </td>
                    <td style="padding:5px;padding-top:10px;">
                      科室：<xsl:value-of select="DEPTNAME"/>
                    </td>
                  </tr>
                  <tr>
                    <td style="padding:5px;">
                      病房：<xsl:value-of select="WARDNO"/>
                    </td>
                    <td style="padding:5px;">
                      床号：<xsl:value-of select="BED"/>
                    </td>
                    <td style="padding:5px;">
                      医生：<xsl:value-of select="DOCTOR"/>
                    </td>
                    <td style="padding:5px;">
                      检验者：<xsl:value-of select="TECHNICIAN"/>
                    </td>
                    <td style="padding:5px;">
                      录入者：<xsl:value-of select="OPERATOR"/>
                    </td> 
                  </tr>
                  <tr>
                    <td style="padding:5px;">
                      审核者：<xsl:value-of select="CHECKER"/>
                    </td>
                    <td style="padding:5px;">
                      临床诊断：<xsl:value-of select="FORMMEMO"/>
                    </td>
                    <td style="padding:5px;">
                      检验小组：<xsl:value-of select="SECTIONNAME"/>
                    </td>
                    <td style="padding:5px;">
                      操作日期：<xsl:value-of select="TESTDATE"/>
                    </td>
                    <td style="padding:5px;">
                      操作时间：<xsl:value-of select="TESTTIME"/>
                    </td>
                  </tr>
                  <tr>
                    <td colspan='2' style="padding:5px;padding-bottom:10px;">
                      备注：<xsl:value-of select="FORMMEMO"/>
                    </td>
                    <td colspan='3' style="padding:5px;padding-bottom:10px;">
                      细菌描述：<xsl:value-of select="DISTRICTNO"/><input id="hidden_patno" type="hidden" value="{PATNO}"></input>
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
                  <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">序号</td>
                  <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">检验项目/细菌名称/抗生素</td>
                  <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">结果MIC(ug/ml)</td>
                  <td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">药敏度</td>
                  <!--<td style="padding-left:0.5em; padding-right:0.5em;white-space:nowrap">操作</td>-->
                </tr>
                <xsl:for-each select="WebReportFile/ReportItem">
                  <tr style="height:24px; line-height:24px;background:#ffffff;"  onmouseover="this.style.background='#2394af'" onmouseout="this.style.background='#ffffff';">
                    <td style="text-align:center;">
                      <xsl:value-of select="position()"/>
                      <xsl:if test="position() =1 ">
                        <table>
                          <tr style="display:none;" >
                            <td  id="aaa" onclick="document.getElementById('tr_{position()}').style.background='#2394af';document.getElementById('tmptrid').value='tr_{position()}'" >111</td>
                          </tr>
                        </table>
                      </xsl:if>
                    </td>
                    <td style="padding-left:0.5em; padding-right:0.5em;">
                      <xsl:if test="ITEMNAME != ''">
                        <xsl:value-of select="ITEMNAME"/>
                      </xsl:if>
                      <xsl:if test="MICRONAME != ''">
                        &#160;&#160;&#160;&#160;&#160;&#160;&#160;<xsl:value-of select="MICRONAME"/>
                      </xsl:if>
                      <xsl:if test="DESCNAME != ''">
                        &#160;&#160;&#160;&#160;&#160;&#160;&#160;<xsl:value-of select="DESCNAME"/>
                      </xsl:if>
                      <xsl:if test="ANTINAME != ''">
                        &#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;<xsl:value-of select="ANTINAME"/>
                      </xsl:if>
                    </td>
                    <td style="padding-left:0.5em; padding-right:0.5em;">
                      <xsl:value-of select="SUSDESC"/>
                      <xsl:value-of select="SUSQUAN"/>
                    </td>
                    <td style="padding-left:0.5em; padding-right:0.5em;">
                      <xsl:value-of select="SUSCEPT"/>
                    </td>
                    <!--<td style="padding-left:0.5em; padding-right:0.5em;">
                                            -->
                    <!--<button onClick="ss()">Query</button><xsl:value-of select="FORMNO"/>-->
                    <!--
                                            <xsl:if test="ITEMNAME != ''">
                                            <a onclick="parent.printResult('{PATNO}','{ITEMNO}','item','{RECEIVEDATE}');"   style=" cursor:pointer;">历史对比</a>
                                            </xsl:if>
                                        </td>-->
                  </tr>
                </xsl:for-each>
              </table>
              <input id="tmptrid" type="hidden" value="tr_1"></input>
            </td>
          </tr>
        </table>
      </body>

    </html>

  </xsl:template>
</xsl:stylesheet>