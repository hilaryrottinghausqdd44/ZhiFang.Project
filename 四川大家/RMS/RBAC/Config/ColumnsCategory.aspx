<%@ Page Language="c#" AutoEventWireup="false"
    Inherits="theNews.Config.ColumnsCategory" Codebehind="ColumnsCategory.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>DeskTopDefine</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="/ioffice.css" type="text/css" rel="stylesheet">
    <style>
        .text
        {
            font-size: 9pt;
        }
        A:link
        {
            color: black;
            text-decoration: none;
        }
        A:visited
        {
            color: black;
            text-decoration: none;
        }
        A:hover
        {
            color: red;
            text-decoration: underline;
        }
        .alpha
        {
            filter: Alpha(Opacity=50);
        }
    </style>

    <script language="javascript">
			var new_item_side;
			var need_param;
			var board_id;
			var folder_id;
			
			var drag_status = 0;	//0:无　1:准备拖动	2:拖动中
			
			need_param = 0;
			board_id = 0;
			folder_id = 0;
			
			function AddItem(p)
			{
				new_item_side = p;
				ItemList.style.display = "";
				DeskTopDefine.ThemeList.style.display = 'none';
				DeskTopDefine.btnSave.style.display = 'none';
				DeskTopDefine.btnModify.style.display = 'none';
				DeskTopDefine.title.style.display = 'none';
			}
			function RemoveItem(x,y)
			{
				
					window.location = "remove_item.aspx?src=1&dipx=" + x + "&dipy=" + y;
				
			}
			function DoAddItem()
			{
				if (window.document.DeskTopDefine.selItems.value == '')
				{
					alert ('请选择您要添加的桌面项。');
					return false;
				}
				if (need_param == 0)
				{
					
						window.location = "add_item.aspx?p=" + new_item_side + "&di_id=" + window.document.DeskTopDefine.selItems.value;
					
				}
				if (need_param == 1)
				{
					if (board_id == 0)
					{
						alert ('请选择讨论组。');
						return false;
					}
					else
					{
						
							window.location = "add_item.aspx?p=" + new_item_side + "&di_id=" + window.document.DeskTopDefine.selItems.value + "&param=" + board_id;	
						
					}
				}
				if (need_param == 2)
				{
					if (folder_id == 0)
					{
						alert ('请选择文档夹。');
						return false;
					}
					else
					{
						
							window.location = "add_item.aspx?p=" + new_item_side + "&di_id=" + window.document.DeskTopDefine.selItems.value + "&param=" + folder_id;
						
					}
				}
				if (need_param == 3)
				{
					if (group_id == 0)
					{
						alert ('请选择新闻库。');
						return false;
					}
					else
					{
						
							window.location = "add_item.aspx?p=" + new_item_side + "&di_id=" + window.document.DeskTopDefine.selItems.value + "&param=" + group_id;
						
					}
				}
				if (need_param == 4)
				{
					if (group_id == 0)
					{
						alert ('请选择新闻库。');
						return false;
					}
					else
					{
						
							window.location = "add_item.aspx?p=" + new_item_side + "&di_id=" + window.document.DeskTopDefine.selItems.value + "&param=" + group_id;
						
					}
				}
			}
			function on_SelItem()
			{
				switch(DeskTopDefine.selItems.value - 0)
				{
					case 6:	//论坛
						window.document.DeskTopDefine.txtBoard.value = "";
						board_id = 0;
						Param_Forum.style.display = "";
						Param_VDisk.style.display = "none";
						Param_News.style.display = "none";
						need_param = 1;
						break;
					case 7:	//文档夹
						window.document.DeskTopDefine.txtFolder.value = "";
						folder_id = 0;
						Param_VDisk.style.display = "";
						Param_Forum.style.display = "none";
						Param_News.style.display = "none";
						need_param = 2;
						break;
					case 1:	//公司新闻
						window.document.DeskTopDefine.txtNews.value = "";
						group_id = 0;				
						Param_News.style.display = "";
						Param_VDisk.style.display = "none";
						Param_Forum.style.display = "none";
						need_param = 3;
						break;
					case 11:	//头条新闻
						window.document.DeskTopDefine.txtNews.value = "";
						group_id = 0;				
						Param_News.style.display = "";
						Param_VDisk.style.display = "none";
						Param_Forum.style.display = "none";
						need_param = 3;
						break;
					default:
						Param_VDisk.style.display = "none";
						Param_Forum.style.display = "none";
						Param_News.style.display = "none";
						need_param = 0;
				}
			}
			function BrowseVDisk()
			{
				var n;
				
				Folder = window.showModalDialog('../vdisk/vdiskList.aspx','','dialogWidth:20;dialogHeight:20;scroll=no;status=no');
				if (Folder != 'undefined' && typeof(Folder)!='undefined')
				{
					n = Folder.indexOf(',');
				
					folder_id = Folder.substr(0,n);
					window.document.DeskTopDefine.txtFolder.value = Folder.substr(n+1);
				}
			}
			function BrowseBoards()
			{
				var n;
				
				Board = window.showModalDialog('/forum/ForumList.aspx','','dialogWidth:15;dialogHeight:20;scroll=no;status=no');
				if (Board != 'undefined' && typeof(Board)!='undefined')
				{
					n = Board.indexOf(',');
					
					board_id = Board.substr(0,n);
					window.document.DeskTopDefine.txtBoard.value = Board.substr(n+1);
				}
			}
			function BrowseNews()
			{
				var n;
				
				Group = window.showModalDialog('../news/GroupList.aspx','','dialogWidth:15;dialogHeight:20;scroll=no;status=no');
				if (Group != 'undefined' && typeof(Group)!='undefined')
				{
					n = Group.indexOf(',');
					
					group_id = Group.substr(0,n);
					window.document.DeskTopDefine.txtNews.value = Group.substr(n+1);
				}
			}
			
			var dragpos_x, dragpos_y;
			var dragitem_x, dragitem_y;
			var new_x, new_y;

			function BeginDrag(x, y)
			{
				divMover.style.display = '';
				divMover.style.left = table1.offsetLeft + 151 * (x-1);
				divMover.style.top = table1.offsetTop + 51 * (y-1);
				
				divLine.style.display = '';
				divLine.style.left = table1.offsetLeft + 151 * (x-1);
				divLine.style.top = table1.offsetTop + 51 * (y-1);
				
				tdMoverContent.innerHTML = document.all['tdContent_' + x + '_' + y].innerHTML;
				//document.all['tbItem_' + x + '_' + y].style.display = 'none';
				
				drag_status = 1;
				
				dragpos_x = event.screenX;
				dragpos_y = event.screenY;
				dragitem_x = x;
				dragitem_y = y;
				new_x = x;
				new_y = y;
			}
			function Drag()
			{
				if (drag_status == 1)
				{
					divMover.style.left = table1.offsetLeft + 151 * (dragitem_x - 1) + event.screenX - dragpos_x;
					divMover.style.top = table1.offsetTop + 51 * (dragitem_y - 1) + event.screenY - dragpos_y;
					
					//判断当前应放置的位置
					if (divMover.offsetLeft >= table1.offsetLeft + 150/2)
						new_x = 2;
					else
						new_x = 1;						
					new_y = Math.round((divMover.offsetTop - table1.offsetTop)/51) + 1;
					
					if (new_x == 1)
						if (new_y > 14)
							new_y = 14;
						else if (new_y < 1)
							new_y = 1;
					if (new_x == 2)
						if (new_y > 12)
							new_y = 12;
						else if (new_y < 1)
							new_y = 1;
					
					divLine.style.left = table1.offsetLeft + 151 * (new_x - 1);
					divLine.style.top = table1.offsetTop + 51 * (new_y-1);
					
				}
			}
			function EndDrag()
			{
				if (drag_status == 1)
				{
					divMover.style.display = 'none';
					divLine.style.display = 'none';
					
					//document.all['tbItem_' + dragitem_x + '_' + dragitem_y].style.display = '';
					drag_status = 0;
					
					//alert (new_x + ',' + new_y);
					
					if (new_x == 1)
					{
						if (new_y > 13)
							new_y = 13;
					}
					else
					{
						if (new_y > 11)
							new_y = 11;
					}
					
					location = 'DeskTopDefine.aspx?x=' + dragitem_x + '&y=' + dragitem_y + '&new_x=' + new_x + '&new_y=' + new_y;
				}
			}
    </script>

</head>
<body leftmargin="40" onselectstart="return false;" onmousemove="Drag()" onmouseup="EndDrag()">
    <form name="DeskTopDefine" method="post" action="DeskTopDefine.aspx" id="DeskTopDefine">
    <table id="table1" width="576" border="3" cellspacing="0" cellpadding="0" style="width: 576px;
        border-collapse: collapse; height: 88px" bordercolor="silver">
        <tr>
            <td width="150" align="center" height="50" valign="top">
                <table id="tbItem_1_11" width="240" border="2" cellspacing="0" cellpadding="0" height="104"
                    bgcolor="#f09cf0" style="width: 240px; height: 104px" align="center">
                    <tr height="10">
                        <td width="99%" bgcolor="silver" onmouseover="this.bgColor='gray'" onmouseout="this.bgColor='silver';"
                            style="cursor: hand" onmousedown="BeginDrag(1,11);">
                            <font color="#f0f0f0" size="1" face="Arial"><b>11</b></font>
                        </td>
                        <td bgcolor="silver" width="1" valign="middle">
                            <font face="宋体"></font>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center" colspan="3" id="tdContent_1_11">
                            <b>工程文件夹</b>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="150" align="center" height="50" valign="top">
                <font face="宋体"></font>
            </td>
        </tr>
    </table>
    <br>
    <p>
        &nbsp;&nbsp;&nbsp;
        <br>
        <br>
        &nbsp;&nbsp;&nbsp;
    </p>
    </form>
    <div id="divLine" style="display: none; z-index: 102; position: absolute">
        <table width="150" border="0" cellspacing="0" cellpadding="0" height="5" bgcolor="orange">
            <tr>
                <td height="2">
                    <font face="宋体"></font>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMover" style="display: none">
        <table class="alpha" height="50" cellspacing="0" cellpadding="0" width="150" border="0">
            <tr bgcolor="gray" height="10">
                <td onmouseup="EndDrag();">
                </td>
            </tr>
            <tr>
                <td id="tdMoverContent" valign="middle" align="center" bgcolor="#c0c0c0">
                    <font face="宋体"></font>
                </td>
            </tr>
        </table>
    </div>

    <script language="javascript">
		function SaveNewTheme()
		{
			window.open('NewTheme.aspx','','width=300px,height=200px,scrollbars=0,left=300,top=200');
		}
		
		function ModifyTheme()
		{
			window.open('NewTheme.aspx?tid=','','width=300px,height=200px,scrollbars=0,left=300,top=200');
		}
		
		function selecttheme()
		{
			if (document.all("ThemeList").value > 0)
			{
				document.all("loadtheme").style.display = '';
				document.all("deltheme").style.display = '';
			}
			if (document.all("ThemeList").value == 0)
			{
				document.all("loadtheme").style.display = 'none';
				document.all("deltheme").style.display = 'none';
				window.location = 'DeskTopDefine.aspx';
			}
		}
		
		function load()
		{
			window.location = 'DeskTopDefine.aspx?tid=' + document.all("ThemeList").value;
		}
		
		function del()
		{
			window.location = 'DeskTopDefine.aspx?del=' + document.all("ThemeList").value;
		}
		
		
				DeskTopDefine.ThemeList.style.display = 'none';
				DeskTopDefine.btnSave.style.display = 'none';
				DeskTopDefine.btnModify.style.display = 'none';
				DeskTopDefine.title.style.display = 'none';
		
			DeskTopDefine.btnModify.style.display = 'none';
		
		
		function Cancel()
		{
			ItemList.style.display = "none";
			
				DeskTopDefine.btnModify.style.display = 'none';
				DeskTopDefine.ThemeList.style.display = 'none';
				DeskTopDefine.btnSave.style.display = 'none';
				DeskTopDefine.title.style.display = 'none';
			
		}
    </script>

</body>
</html>
