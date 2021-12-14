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
        <LINK rel="stylesheet" type="text/css" href="../images2008-12/css.css"/>
      </HEAD>
      <body bottomMargin="0" leftMargin="2" topMargin="5" rightMargin="1" bgcolor="#E3E3E3">
        <table width="100%" height="100" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td width="11%" height="22" align="left" bgcolor="#49b4f8">
              <img id="TopExpand" src="images/icon/icon_last.gif" style="Z-INDEX: 0; LEFT: 2px; POSITION: absolute; TOP: 50px;Cursor:hand;DISPLAY:none" onclick="this.style.display='none';var frmTop=top.fset;frmTop.cols='209,0,*';"></img>
              <OBJECT WIDTH="1" HEIGHT="1" ID="RemoveIEToolbar" CLASSID="CLSID:2646205B-878C-11d1-B07C-0000C040BCDB" codebase="../Includes/Activex/nskey.dll">
                <PARAM NAME="ToolBar" VALUE="0"/>
              </OBJECT>
              
            </td>
            <td width="45%" align="left" bgcolor="#49b4f8"></td>
            <td width="44%" align="right" bgcolor="#49b4f8">
              
            </td>
          </tr>
          <tr>
            <td height="78" colspan="3" background="../images2008-12/nav-bg.gif">
              <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="19%" align="left">
                    <table width="221" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="11" align="center"></td>
                        <td width="68" align="center">
                          <img src="../images2008-12/man.jpg" width="46" height="44" />                        
                          <a href="#"></a></td>
                        <td width="122">
                          <table width="117" height="45" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td>
                                <span class="font">欢迎您：</span>                              </td>
                            </tr>
                            <tr>
                              <td>
                                <span class="font">
                                  <xsl:value-of select="$UserName"/></span>                              </td>
                            </tr>
                            <tr>
                              <td>
                              </td>
                            </tr>
                          </table>                        </td>
                        <td width="20" align="right">
                          <img src="../images2008-12/line.gif" width="9" height="70" />                        </td>
                      </tr>
                    </table>
                  </td>
                  <td width="81%" align="right"><table width="90%" height="65" border="0" align="right" cellpadding="0" cellspacing="0">
                      <tr>
                        <xsl:for-each select="treenode">
                          <xsl:value-of select="../treenode"/>
                          <td width="{floor(100 div count(../treenode))}%">
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
          </tr>
        </table>
      </body>   </html>
  </xsl:template>
</xsl:stylesheet>
