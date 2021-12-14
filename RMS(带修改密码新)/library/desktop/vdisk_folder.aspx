<%@ Page Language="c#" AutoEventWireup="True" Inherits="OA.library.desktop.vdisk_folder" Codebehind="vdisk_folder.aspx.cs" %>

<%
    com.unicafe.vdisk.Folder f = new com.unicafe.vdisk.Folder();
    com.unicafe.vdisk.FolderMgr fm = new com.unicafe.vdisk.FolderMgr();
    f = fm.GetFolder(int.Parse(Request.QueryString["Param"].ToString()));

    string ID = System.Guid.NewGuid().ToString().Replace("-", "");
%>

<script language="javascript">
	function downloadfile(fileid)
	{
		//window.frames['fraDownload'].location = '/vdisk/Download.aspx?objid=<%=Request.QueryString["Param"].ToString().Trim()%>&fileid=' + fileid;
		ActiveXDownload ('/vdisk/Download.aspx?fileid=' + fileid);
	}
</script>

<table border="0" width="95%" cellspacing="0" align="center">
    <tr>
        <td class="DESKTOPITEM">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <font color="Teal"><b>
                            <%=f.FolderName%></b> - 在线文档</font>
                    </td>
                    <td align="right" nowrap>
                        <a style="cursor: hand" onclick="imgOn_<%=ID%>.style.display = 'none';imgOff_<%=ID%>.style.display = '';trContent_<%=ID%>.style.display = 'none';">
                            <img id="imgOn_<%=ID%>" src="/images/hidden-on.gif" border="0" align="absbottom"></a>
                        <a style="cursor: hand" onclick="imgOn_<%=ID%>.style.display = '';imgOff_<%=ID%>.style.display = 'none';trContent_<%=ID%>.style.display = '';">
                            <img id="imgOff_<%=ID%>" src="/images/hidden-off.gif" border="0" align="absbottom"
                                style="display: none"></a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr style="height: 1px" bgcolor="Teal">
        <td>
        </td>
    </tr>
    <tr style="height: 5px">
        <td>
        </td>
    </tr>
    <tr id="trContent_<%=ID%>">
        <td valign="top">
            <%
                ArrayList filelist;
                filelist = fm.ListAttachment(int.Parse(Request.QueryString["Param"].ToString()), 8);
                if (filelist.Count == 0)
                {
                    Response.Write("<font color=gray>[无]</font>");
                }
                for (int j = 0; j < filelist.Count && j < 5; j++)
                {
                    com.unicafe.vdisk.Attachment att = (com.unicafe.vdisk.Attachment)filelist[j];

                    if (f.FolderID == 2)
                    {
                        if (att.FileOwnerID == CurrentEmployee.EmplID)
                        {
                            Response.Write(GetImageFile(att.AttFileName) + " ");
                            Response.Write("<a href=\"javascript:downloadfile('" + att.AttID + "')\">" + att.AttFileName + "</a><br>");
                        }
                    }
                    else
                    {
                        Response.Write(GetImageFile(att.AttFileName) + " ");
                        Response.Write("<a href=\"javascript:downloadfile('" + att.AttID + "')\">" + att.AttFileName + "</a><br>");
                    }
                }
            %>
            <div align="right">
                <a href="/collabration/default.aspx?Node=2&Url=/vdisk/FileList.aspx?fid=<%=f.FolderID%>">
                    <img src="/images/more.gif" border="0" alt="更多"></a></div>
        </td>
    </tr>
</table>
<iframe id="fraDownload" name="fraDownload" style="display: none"></iframe>
