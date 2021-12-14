<html>
	<head>
		<title>eWebEditor - eWebSoft在线文本编辑器</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<link href="css/light/Editor.css" type="text/css" rel="stylesheet">
		<script language="Javascript">
			var sPath = document.location.pathname;
			sPath = sPath.substr(0, sPath.length-14);

			var sLinkFieldName = "content1" ;

			// 全局设置对象
			var config = new Object() ;
			config.Version = "2.0.0 beta" ;
			config.ReleaseDate = "2004-02-04" ;
			config.StyleName = "standard_light";
			config.StyleEditorHeader = "<head><link href=\""+sPath+"css/light/EditorArea.css\" type=\"text/css\" rel=\"stylesheet\"></head><body MONOSPACE>" ;
			config.StyleMenuHeader = "<head><link href=\""+sPath+"css/light/MenuArea.css\" type=\"text/css\" rel=\"stylesheet\"></head><body scroll=\"no\" onConTextMenu=\"event.returnValue=false;\">";
			config.StyleDir = "standard";
			config.StyleUploadDir = "UploadFile";
			config.InitMode = "EDIT";
			config.AutoDetectPasteFromWord = true;
			config.BaseUrl = true;
		</script>
		<script language="Javascript" src="include/editor.js"></script>
		<script language="Javascript" src="include/table.js"></script>
		<script language="Javascript" src="include/menu.js"></script>
		<script language="javascript" event="onerror(msg, url, line)" for="window">
			//return true ;	 // 隐藏错误
		</script>
	</head>
	<body scrolling="no" onfocus="VerifyFocus()">
		<!--onConTextMenu="event.returnValue=false;"-->
		<table border="0" cellpadding="0" cellspacing="0" width='100%' height='100%' id="Table1">
			<tr>
				<td>
					<table border="0" cellpadding="0" cellspacing="0" width='100%' class='Toolbar' id='eWebEditor_Toolbar'>
						<tr>
							<td>
								<div class="yToolbar">
									<div class="TBHandle"></div>
									<select class="TBGen" onchange="format('FormatBlock',this[this.selectedIndex].value);this.selectedIndex=0" id="Select1" name="Select1">
										<option selected>段落样式</option>
										<option value="&lt;P&gt;">普通</option>
										<option value="&lt;H1&gt;">标题一</option>
										<option value="&lt;H2&gt;">标题二</option>
										<option value="&lt;H3&gt;">标题三</option>
										<option value="&lt;H4&gt;">标题四</option>
										<option value="&lt;H5&gt;">标题五</option>
										<option value="&lt;H6&gt;">标题六</option>
										<option value="&lt;p&gt;">段落</option>
										<option value="&lt;dd&gt;">定义</option>
										<option value="&lt;dt&gt;">术语定义</option>
										<option value="&lt;dir&gt;">目录列表</option>
										<option value="&lt;menu&gt;">菜单列表</option>
										<option value="&lt;PRE&gt;">已编排格式</option>									</select>
									<select class="TBGen" onchange="format('fontname',this[this.selectedIndex].value);this.selectedIndex=0" id="Select2" name="Select2">
										<option selected>字体</option>
										<option value="宋体">宋体</option>
										<option value="黑体">黑体</option>
										<option value="楷体_GB2312">楷体</option>
										<option value="仿宋_GB2312">仿宋</option>
										<option value="隶书">隶书</option>
										<option value="幼圆">幼圆</option>
										<option value="Arial">Arial</option>
										<option value="Arial Black">Arial Black</option>
										<option value="Arial Narrow">Arial Narrow</option>
										<option value="Brush Script&#9;MT">Brush Script MT</option>
										<option value="Century Gothic">Century Gothic</option>
										<option value="Comic Sans MS">Comic Sans MS</option>
										<option value="Courier">Courier</option>
										<option value="Courier New">Courier New</option>
										<option value="MS Sans Serif">MS Sans Serif</option>
										<option value="Script">Script</option>
										<option value="System">System</option>
										<option value="Times New Roman">Times New Roman</option>
										<option value="Verdana">Verdana</option>
										<option value="Wide Latin">Wide Latin</option>
										<option value="Wingdings">Wingdings</option>
									</select>
									<select class="TBGen" onchange="format('fontsize',this[this.selectedIndex].value);this.selectedIndex=0" id="Select3" name="Select3">
										<option selected>字号</option>
										<option value="7">一号</option>
										<option value="6">二号</option>
										<option value="5">三号</option>
										<option value="4">四号</option>
										<option value="3">五号</option>
										<option value="2">六号</option>
										<option value="1">七号</option>
									</select>
									<select class="TBGen" onchange="doZoom(this[this.selectedIndex].value)" id="Select4" name="Select4">
										<option value="10">10%</option>
										<option value="25">25%</option>
										<option value="50">50%</option>
										<option value="75">75%</option>
										<option value="100" selected>100%</option>
										<option value="150">150%</option>
										<option value="200">200%</option>
										<option value="500">500%</option>
									</select>
									<div class="Btn" title="粗体" onclick="format('bold')"><img class="Ico" src="buttonimage/standard/bold.gif"></div>
									<div class="Btn" title="斜体" onclick="format('italic')"><img class="Ico" src="buttonimage/standard/italic.gif"></div>
									<div class="Btn" title="下划线" onclick="format('underline')"><img class="Ico" src="buttonimage/standard/underline.gif"></div>
									<div class="Btn" title="中划线" onclick="format('StrikeThrough')"><img class="Ico" src="buttonimage/standard/strikethrough.gif"></div>
									<div class="Btn" title="上标" onclick="format('superscript')"><img class="Ico" src="buttonimage/standard/superscript.gif"></div>
									<div class="Btn" title="下标" onclick="format('subscript')"><img class="Ico" src="buttonimage/standard/subscript.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="左对齐" onclick="format('justifyleft')"><img class="Ico" src="buttonimage/standard/JustifyLeft.gif"></div>
									<div class="Btn" title="居中对齐" onclick="format('justifycenter')"><img class="Ico" src="buttonimage/standard/JustifyCenter.gif"></div>
									<div class="Btn" title="右对齐" onclick="format('justifyright')"><img class="Ico" src="buttonimage/standard/JustifyRight.gif"></div>
									<div class="Btn" title="两端对齐" onclick="format('JustifyFull')"><img class="Ico" src="buttonimage/standard/JustifyFull.gif"></div>
									<div class="Btn" title="折叠展开" onclick="ExpandClose()"><img class="Ico" src="buttonimage/standard/Plus.gif"></div>
								</div>
							</td>
						</tr>
						<tr>
							<td>
								<div class="yToolbar">
									<div class="TBHandle"></div>
									<div class="Btn" title="剪切" onclick="format('cut')"><img class="Ico" src="buttonimage/standard/cut.gif"></div>
									<div class="Btn" title="复制" onclick="format('copy')"><img class="Ico" src="buttonimage/standard/copy.gif"></div>
									<div class="Btn" title="常规粘贴" onclick="format('paste')"><img class="Ico" src="buttonimage/standard/paste.gif"></div>
									<div class="Btn" title="纯文本粘贴" onclick="PasteText()"><img class="Ico" src="buttonimage/standard/pastetext.gif"></div>
									<div class="Btn" title="从Word中粘贴" onclick="PasteWord()"><img class="Ico" src="buttonimage/standard/pasteword.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="查找替换" onclick="findReplace()"><img class="Ico" src="buttonimage/standard/findreplace.gif"></div>
									<div class="Btn" title="删除" onclick="format('delete')"><img class="Ico" src="buttonimage/standard/delete.gif"></div>
									<div class="Btn" title="删除文字格式" onclick="format('RemoveFormat')"><img class="Ico" src="buttonimage/standard/RemoveFormat.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="撤消" onclick="format('undo')"><img class="Ico" src="buttonimage/standard/undo.gif"></div>
									<div class="Btn" title="恢复" onclick="format('redo')"><img class="Ico" src="buttonimage/standard/redo.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="全部选中" onclick="format('SelectAll')"><img class="Ico" src="buttonimage/standard/selectAll.gif"></div>
									<div class="Btn" title="取消选择" onclick="format('Unselect')"><img class="Ico" src="buttonimage/standard/unselect.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="编号" onclick="format('insertorderedlist')"><img class="Ico" src="buttonimage/standard/insertorderedlist.gif"></div>
									<div class="Btn" title="项目符号" onclick="format('insertunorderedlist')"><img class="Ico" src="buttonimage/standard/insertunorderedlist.gif"></div>
									<div class="Btn" title="增加缩进量" onclick="format('indent')"><img class="Ico" src="buttonimage/standard/indent.gif"></div>
									<div class="Btn" title="减少缩进量" onclick="format('outdent')"><img class="Ico" src="buttonimage/standard/outdent.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="字体颜色" onclick="ShowDialog('dialog/selcolor.htm?action=forecolor', 280, 250, true)"><img class="Ico" src="buttonimage/standard/forecolor.gif"></div>
									<div class="Btn" title="对象背景颜色" onclick="ShowDialog('dialog/selcolor.htm?action=bgcolor', 280, 250, true)"><img class="Ico" src="buttonimage/standard/bgcolor.gif"></div>
									<div class="Btn" title="字体背景颜色" onclick="ShowDialog('dialog/selcolor.htm?action=backcolor', 280, 250, true)"><img class="Ico" src="buttonimage/standard/backcolor.gif"></div>
									<div class="Btn" title="背景图片" onclick="ShowDialog('dialog/backimage.htm', 350, 210, true)"><img class="Ico" src="buttonimage/standard/bgpic.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="绝对或相对位置" onclick="absolutePosition()"><img class="Ico" src="buttonimage/standard/abspos.gif"></div>
									<div class="Btn" title="上移一层" onclick="zIndex('forward')"><img class="Ico" src="buttonimage/standard/forward.gif"></div>
									<div class="Btn" title="下移一层" onclick="zIndex('backward')"><img class="Ico" src="buttonimage/standard/backward.gif"></div>
								    <div class="Btn" title="插入或修改图片" onclick="ShowDialog('dialog/img.htm', 350, 345, true)"><img class="Ico" src="buttonimage/standard/img.gif"></div>
								</div>
							</td>
						</tr>
						<tr>
							<td>
								<div class="yToolbar">
									<div class="TBHandle"></div>
									
									<div class="Btn" title="插入Flash动画" onclick="ShowDialog('dialog/flash.htm', 350, 250, true)"><img class="Ico" src="buttonimage/standard/flash.gif"></div>
									<div class="Btn" title="插入自动播放的媒体文件" onclick="ShowDialog('dialog/media.htm', 350, 250, true)"><img class="Ico" src="buttonimage/standard/Media.gif"></div>
									<div class="Btn" title="插入其他文件" onclick="ShowDialog('dialog/file.htm', 350, 250, true)"><img class="Ico" src="buttonimage/standard/file.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="表格菜单" onclick="showToolMenu('table')"><img class="Ico" src="buttonimage/standard/tablemenu.gif"></div>
									<div class="Btn" title="表单菜单" onclick="showToolMenu('form')"><img class="Ico" src="buttonimage/standard/FormMenu.gif"></div>
									<div class="Btn" title="显示或隐藏指导方针" onclick="showBorders()"><img class="Ico" src="buttonimage/standard/ShowBorders.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="插入或修改栏目框" onclick="ShowDialog('dialog/fieldset.htm', 350, 170, true)"><img class="Ico" src="buttonimage/standard/fieldset.gif"></div>
									<div class="Btn" title="插入或修改网页帧" onclick="ShowDialog('dialog/iframe.htm', 600, 405, true)"><img class="Ico" src="buttonimage/standard/iframe.gif"></div>
									<div class="Btn" title="插入水平尺" onclick="format('InsertHorizontalRule')"><img class="Ico" src="buttonimage/standard/InsertHorizontalRule.gif"></div>
									<div class="Btn" title="插入或修改字幕" onclick="ShowDialog('dialog/marquee.htm', 395, 250, true)"><img class="Ico" src="buttonimage/standard/Marquee.gif"></div>
									<div class="Btn" title="书签管理" onclick="ShowDialog('dialog/anchor.htm', 270, 220, true)"><img class="Ico" src="buttonimage/standard/anchor.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="插入或修改超级链接" onclick="CreateLink()"><img class="Ico" src="buttonimage/standard/CreateLink.gif"></div>
									<div class="Btn" title="取消超级链接或标签" onclick="format('UnLink')"><img class="Ico" src="buttonimage/standard/Unlink.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="插入特殊字符" onclick="ShowDialog('dialog/symbol.htm', 350, 250, true)"><img class="Ico" src="buttonimage/standard/symbol.gif"></div>
									<div class="Btn" title="插入表情图标" onclick="ShowDialog('dialog/emot.htm', 300, 280, true)"><img class="Ico" src="buttonimage/standard/emot.gif"></div>
									<div class="Btn" title="插入Excel表格" onclick="insert('excel')"><img class="Ico" src="buttonimage/standard/excel.gif"></div>
									<div class="Btn" title="插入当前日期" onclick="insert('nowdate')"><img class="Ico" src="buttonimage/standard/date.gif"></div>
									<div class="Btn" title="插入当前时间" onclick="insert('nowtime')"><img class="Ico" src="buttonimage/standard/time.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="引用样式" onclick="insert('quote')"><img class="Ico" src="buttonimage/standard/quote.gif"></div>
									<div class="Btn" title="代码样式" onclick="insert('code')"><img class="Ico" src="buttonimage/standard/code.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="插入新闻栏目" onclick="InsertNewsCatagory()"><img class="Ico" src="buttonimage/standard/III.gif"></div>
									<div class="Btn" title="插入系统配置" onclick="InsertAppSystemConfig()"><img class="Ico" src="buttonimage/standard/DDD.gif"></div>
									<div class="TBSep"></div>
									<div class="Btn" title="全屏编辑" onclick="Maximize()"><img class="Ico" src="buttonimage/standard/maximize.gif"></div>
									<div class="Btn" title="查看使用帮助" onclick="ShowDialog('dialog/help.htm','400','300')"><img class="Ico" src="buttonimage/standard/help.gif" height="20" width="8"></div>
								</div>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height='100%'>
					<table border="0" cellpadding="0" cellspacing="0" width='100%' height='100%' id="Table2">
						<tr>
							<td height='100%'>
								<input type="hidden" id="ContentEdit" name="ContentEdit"> <input type="hidden" id="ContentLoad" name="ContentLoad">
								<input type="hidden" id="ContentFlag" value="0" name="ContentFlag">
								<iframe class="Composition" id="eWebEditor" marginheight="1" marginwidth="1" width="100%"
									height="100%" scrolling="yes"></iframe>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="25">
					<table border="0" cellpadding="0" cellspacing="0" width="100%" class="StatusBar" height="25"
						id="Table3">
						<tr valign="middle">
							<td>
								<table border="0" cellpadding="0" cellspacing="0" height="20" id="Table4">
									<tr>
										<td width="10">
										</td>
										<td class="StatusBarBtnOff" id="eWebEditor_CODE" onclick="setMode('CODE')">
											<img border="0" src="buttonimage/standard/modecode.gif" width="50" height="15" align="absMiddle"></td>
										<td width="5">
										</td>
										<td class="StatusBarBtnOff" id="eWebEditor_EDIT" onclick="setMode('EDIT')">
											<img border="0" src="buttonimage/standard/modeedit.gif" width="50" height="15" align="absMiddle"></td>
										<td width="5">
										</td>
										<td class="StatusBarBtnOff" id="eWebEditor_VIEW" onclick="setMode('VIEW')">
											<img border="0" src="buttonimage/standard/modepreview.gif" width="50" height="15" align="absMiddle"></td>
									</tr>
								</table>
							</td>
							<td align="center" id="eWebEditor_License">
								<table border="0" cellpadding="0" cellspacing="0" height="20" id="Table6" style="FONT-SIZE: 9pt">
									<tr>
										<td valign="bottom">
											共享图片</td>
										<td onclick="insertSiteMapElement('XXSmall')">
											<img border="0" src="../images/icons/tu.jpg" width="16" height="16" alt="图片集XXSmall"></td>
										<td width="5">
										</td>
										<td onclick="insertSiteMapElement('XSmall')">
											<img border="0" src="../images/icons/tu.jpg" width="18" height="18" alt="图片集XSmall"></td>
										<td width="5">
										</td>
										<td onclick="insertSiteMapElement('Small')">
											<img border="0" src="../images/icons/tu.jpg" width="19" height="19" alt="图片集Small"></td>
										<td width="5">
										</td>
										<td onclick="insertSiteMapElement('Large')">
											<img border="0" src="../images/icons/tu.jpg" width="20" height="20" alt="图片集Large"></td>
										<td width="5">
										</td>
										<td onclick="insertSiteMapElement('XLarge')">
											<img border="0" src="../images/icons/tu.jpg" width="21" height="21" alt="图片集XLarge"></td>
										<td width="5">
										</td>
										<td onclick="insertSiteMapElement('XXLarge')">
											<img border="0" src="../images/icons/tu.jpg" width="22" height="22" alt="图片集XXLarge"></td>
										<td width="40">
										</td>
									</tr>
								</table>
							</td>
							<td align="right">
								<table border="0" cellpadding="0" cellspacing="0" height="20" id="Table5">
									<tr>
										<td onclick="sizeChange(300)">
											<img border="0" src="buttonimage/standard/sizeplus.gif" width="20" height="20" alt="增高编辑区"></td>
										<td width="5">
										</td>
										<td onclick="sizeChange(-300)">
											<img border="0" src="buttonimage/standard/sizeminus.gif" width="20" height="20" alt="减小编辑区"></td>
										<td width="40">
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<div id="divTemp" style="VISIBILITY: hidden; OVERFLOW: hidden; WIDTH: 1px; POSITION: absolute; HEIGHT: 1px">
		</div>
		<script language="javascript">
function insertSiteMapElement(strSiteMapAction)
{
	frames['eWebEditor'].focus();
	if(strSiteMapAction=='XXSmall')
		ButtSelectFile_onclick('txtXML','list','images/'+strSiteMapAction+'/',10);
	if(strSiteMapAction=='XSmall')
		ButtSelectFile_onclick('txtXML','list','images/'+strSiteMapAction+'/',8);
	if(strSiteMapAction=='Small')
		ButtSelectFile_onclick('txtXML','list','images/'+strSiteMapAction+'/',6);
	if(strSiteMapAction=='Large')
		ButtSelectFile_onclick('txtXML','list','images/'+strSiteMapAction+'/',4);
	if(strSiteMapAction=='XLarge')
		ButtSelectFile_onclick('txtXML','list','images/'+strSiteMapAction+'/',2);
	if(strSiteMapAction=='XXLarge')
		ButtSelectFile_onclick('txtXML','list','images/'+strSiteMapAction+'/',2);
		//frames['eWebEditor'].document.selection.createRange().pasteHTML("<img>")
	
	frames['eWebEditor'].focus();
}
function ButtSelectFile_onclick(returnid,style,path,pageSize) 
{
	//style='lists','images'
	r=window.showModalDialog('../PopupSelectImageFile.aspx?path=' 
		+ path +'&pageSize='
		+ pageSize + '&style='
		+ style,'','dialogWidth:588px;dialogHeight:618px;help:no;scroll:no;status:no');
	if (r == '' || typeof(r) == 'undefined'||typeof(r)=='object')
	{
		return;
	}
	else
	{
		//document.all[returnid].value=r;
		frames['eWebEditor'].document.selection.createRange().pasteHTML("<img src=" + r + ">")
	}
	//alert(r);
}
		</script>
	</body>
</html>
