<?xml version="1.0" encoding="gb2312"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:param name="UserName"></xsl:param>
  <xsl:param name="Renwu"></xsl:param>
  <xsl:param name="Informations"></xsl:param>
  <xsl:param name="OnlineUsers"></xsl:param>
  <xsl:param name="EmailNums"></xsl:param>
  <xsl:param name="Company"></xsl:param>
  <xsl:output method="html" encoding="GB2312" omit-xml-declaration="yes" standalone="yes" doctype-public="-//W3C//DTD HTML 4.01 Transitional//EN" indent="yes"/>
  <xsl:template match="/TREENODES/treenode[@NodeData='Topmenu']">
    <html xmlns="http://www.w3.org/1999/xhtml">
      <HEAD>
        <title>IndexTop</title>
        <meta name="GENERATOR" Content="Microsoft FrontPage 4.0"/>
        <meta name="CODE_LANGUAGE" Content="C#"/>
        <meta name="vs_defaultClientScript" content="JavaScript"/>
        <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
        <LINK rel="stylesheet" type="text/css" href="css.css"/>
        <LINK rel="stylesheet" type="text/css" href="style.css"/>
      </HEAD>
      <body bottomMargin="0" leftMargin="2" topMargin="5" rightMargin="1" bgcolor="#E3E3E3">
        <OBJECT WIDTH="1" HEIGHT="1" ID="RemoveIEToolbar" CLASSID="CLSID:2646205B-878C-11d1-B07C-0000C040BCDB" codebase="../Includes/Activex/nskey.dll">
          <PARAM NAME="ToolBar" VALUE="0"/>
        </OBJECT>
        <table width="100%" height="100" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
          <tr>
            <td valign="top">
              <img id="TopExpand" src="images/icon/icon_last.gif" style="Z-INDEX: 0; LEFT: 2px; POSITION: absolute; TOP: 50px;Cursor:hand;DISPLAY:none" onclick="this.style.display='none';var frmTop=top.fset;frmTop.cols='209,0,*';"></img>
              <table width="100%" height="100" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td height="87" valign="top">
                    <table width="100%" height="87" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td height="30" valign="top">
                          <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td width="1%">
                                <img src="images/top/cor1.jpg" width="12" height="30"/>
                              </td>
                              <td width="98%">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td height="29" align="left" valign="bottom" class="zhifang" bgcolor="red" style="background-image: url('images/top/tiao.jpg'); backgroundrepeat: no-repeat">
                                      <xsl:value-of select="$Company"/>内部办公平台
                                    </td>
                                  </tr>
                                  <tr>
                                    <td height="1" bgcolor="#064289"/>
                                  </tr>
                                </table>
                              </td>
                              <td width="1%">
                                <img src="images/top/cor2.jpg" width="12" height="30"/>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <!--*********************middle_left*********************-->
                              <td>
                                <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td width="1" background="images/top/tiao1.jpg">　</td>
                                    <td background="images/top/tiao2.jpg">
                                      <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <xsl:for-each select="treenode[count(../treenode) div position()   &gt;= 2 ]">
                                            <xsl:value-of select="../treenode[count(../treenode[count(../treenode) div position()   &gt;= 2])]"/>
                                            <td width="{floor(100 div count(../treenode[count(../treenode) div position()   &gt;= 2]))}%">
                                              <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td height="37" align="center">
                                                    <a class="top">
                                                      <xsl:attribute name="Target">
                                                        <xsl:value-of select="@Target"/>
                                                      </xsl:attribute>
                                                      <xsl:attribute name="href">
                                                        <xsl:value-of select="@NavigateUrl"/>
                                                      </xsl:attribute>
                                                      <img width="33" height="34" border="0">
                                                        <xsl:attribute name="src">
                                                          <xsl:value-of select="@ImageUrl"/>
                                                        </xsl:attribute>
                                                      </img>
                                                    </a>
                                                  </td>
                                                </tr>
                                                <tr>
                                                  <td align="center" class="top" nowrap="nowrap">
                                                    <a class="top" >
                                                      <xsl:attribute name="href">
                                                        <xsl:value-of select="@NavigateUrl"/>
                                                      </xsl:attribute>
                                                      <xsl:attribute name="Target">
                                                        <xsl:value-of select="@Target"/>
                                                      </xsl:attribute>
                                                      <xsl:value-of select="@Text"/>
                                                    </a>
                                                  </td>
                                                </tr>
                                              </table>
                                            </td>
                                          </xsl:for-each>
                                        </tr>
                                      </table>
                                    </td>
                                  </tr>
                                </table>
                              </td>

                              <!--*****************************middel_register**************************************-->
                              <td width="383">
                                <table width="383" height="55" border="0" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td width="67" background="images/top/corner1.jpg"> </td>
                                    <td width="64%" background="images/top/corner3.jpg">
                                      <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="center" valign="bottom">
                                            <span class="register">
                                              登陆用户：<xsl:value-of select="$UserName"/>
                                            </span>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td class="register1" nowrap="true">
                                            <a class="number">
                                              <xsl:value-of select="$Informations"/>
                                            </a>邀请信息 <a class="number">
                                              <xsl:value-of select="$Renwu"/>
                                            </a>件任务
                                            <a class="number">
                                              <xsl:value-of select="$EmailNums"/>
                                            </a>条未处理收件 <a class="number">
                                              <xsl:value-of select="$OnlineUsers"/>
                                            </a>人在线
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                    <td width="69" background="images/top/corner2.jpg"> </td>
                                  </tr>
                                </table>
                              </td>
                              <!--******************************middle_right**************************************-->
                              <td>
                                <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td background="images/top/tiao2.jpg">
                                      <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" valign="top">
                                            <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <xsl:for-each select="treenode">
                                                  <xsl:if test="count(../treenode) div position()   &lt; 2 ">
                                                    <xsl:value-of select="../treenode[count(../treenode[count(../treenode) div position()   &lt; 2])]"/>
                                                    <td width="{floor(100 div count(../treenode[count(../treenode) div position()   &lt; 2]))}%">
                                                      <table width="100%" height="55" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                          <td height="37" align="center">
                                                            <a class="top">
                                                              <xsl:attribute name="Target">
                                                                <xsl:value-of select="@Target"/>
                                                              </xsl:attribute>
                                                              <xsl:attribute name="href">
                                                                <xsl:value-of select="@NavigateUrl"/>
                                                              </xsl:attribute>
                                                              <img width="32" height="34" border="0">
                                                                <xsl:attribute name="src">
                                                                  <xsl:value-of select="@ImageUrl"/>
                                                                </xsl:attribute>
                                                              </img>
                                                            </a>
                                                          </td>
                                                        </tr>
                                                        <tr>
                                                          <td align="center" class="top" nowrap="nowrap">
                                                            <a class="top">
                                                              <xsl:attribute name="Target">
                                                                <xsl:value-of select="@Target"/>
                                                              </xsl:attribute>
                                                              <xsl:attribute name="href">
                                                                <xsl:value-of select="@NavigateUrl"/>
                                                              </xsl:attribute>
                                                              <xsl:value-of select="@Text"/>
                                                            </a>
                                                          </td>
                                                        </tr>
                                                      </table>
                                                    </td>
                                                  </xsl:if>
                                                </xsl:for-each>
                                              </tr>
                                            </table>
                                          </td>
                                        </tr>
                                      </table>
                                    </td>
                                    <td width="1" background="images/top/tiao1.jpg">　</td>
                                  </tr>
                                </table>
                              </td>
                              <!--********************************************************************-->
                            </tr>
                          </table>
                        </td>
                      </tr>
                      <tr>
                        <td height="4"></td>
                      </tr>
                      <tr>
                        <td height="2" bgcolor="4090DB"/>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
            <td width="2">
              <img src="images/top/main_r2_c5.jpg" width="2" height="100"/>
            </td>
          </tr>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
