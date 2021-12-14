<html>
	<head>
		<title>eWebEditor - eWebSoft在线文本编辑器</title>
		<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
		<link href="css/light/Editor.css" type="text/css" rel="stylesheet">
			<Script Language="Javascript">
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
</Script>
<Script Language=Javascript src="include/editor.js"></Script>
<Script Language=Javascript src="include/table.js"></Script>
<Script Language=Javascript src="include/menu.js"></Script>

<script language="javascript" event="onerror(msg, url, line)" for="window">
//return true ;	 // 隐藏错误
</script>
	</head>
	<body SCROLLING="no" onfocus="VerifyFocus()"> <!--onConTextMenu="event.returnValue=false;"-->
		<table border="0" cellpadding="0" cellspacing="0" width='100%' height='100%' ID="Table1">
			<tr>
				<td>
					<table border="0" cellpadding="0" cellspacing="0" width='100%' class='Toolbar' id='eWebEditor_Toolbar'>
						
					
						<tr>
							<td><div class="yToolbar"><DIV CLASS="TBHandle"></DIV>
									<DIV CLASS="Btn" TITLE="插入表格" onclick="TableInsert()"><IMG CLASS="Ico" SRC="buttonimage/standard/tableinsert.gif"></DIV>
									<DIV CLASS="Btn" TITLE="修改表格属性" onclick="TableProp()"><IMG CLASS="Ico" SRC="buttonimage/standard/tableProp.gif"></DIV>
									<DIV CLASS="TBSep"></DIV>
									<DIV CLASS="Btn" TITLE="单元格属性" onclick="TableCellProp()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableCellProp.gif"></DIV>
									<DIV CLASS="Btn" TITLE="拆分单元格" onclick="TableCellSplit()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableCellSplit.gif"></DIV>
									<DIV CLASS="TBSep"></DIV>
									<DIV CLASS="Btn" TITLE="表格行属性" onclick="TableRowProp()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableRowProp.gif"></DIV>
									<DIV CLASS="Btn" TITLE="插入行（在上方）" onclick="TableRowInsertAbove()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableRowInsertAbove.gif"></DIV>
									<DIV CLASS="Btn" TITLE="插入行（在下方）" onclick="TableRowInsertBelow()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableRowInsertBelow.gif"></DIV>
									<DIV CLASS="Btn" TITLE="合并行（向下方）" onclick="TableRowMerge()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableRowMerge.gif"></DIV>
									<DIV CLASS="Btn" TITLE="拆分行" onclick="TableRowSplit(2)"><IMG CLASS="Ico" SRC="buttonimage/standard/TableRowSplit.gif"></DIV>
									<DIV CLASS="Btn" TITLE="删除行" onclick="TableRowDelete()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableRowDelete.gif"></DIV>
									<DIV CLASS="TBSep"></DIV>
									<DIV CLASS="Btn" TITLE="插入列（在左侧）" onclick="TableColInsertLeft()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableColInsertLeft.gif"></DIV>
									<DIV CLASS="Btn" TITLE="插入列（在右侧）" onclick="TableColInsertRight()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableColInsertRight.gif"></DIV>
									<DIV CLASS="Btn" TITLE="合并列（向右侧）" onclick="TableColMerge()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableColMerge.gif"></DIV>
									<DIV CLASS="Btn" TITLE="拆分列" onclick="TableColSplit(2)"><IMG CLASS="Ico" SRC="buttonimage/standard/TableColSplit.gif"></DIV>
									<DIV CLASS="Btn" TITLE="删除列" onclick="TableColDelete()"><IMG CLASS="Ico" SRC="buttonimage/standard/TableColDelete.gif"></DIV>
									<DIV CLASS="TBSep"></DIV>
									<DIV CLASS="Btn" TITLE="表格菜单" onclick="showToolMenu('table')"><IMG CLASS="Ico" SRC="buttonimage/standard/tablemenu.gif"></DIV>
								</div>
							</td>
						</tr>
						
					</table>
				</td>
			</tr>
			<tr>
				<td height='100%'>
					<table border="0" cellpadding="0" cellspacing="0" width='100%' height='100%' ID="Table2">
						<tr>
							<td height='100%'>
								<input type="hidden" ID="ContentEdit" value="" NAME="ContentEdit"> <input type="hidden" ID="ContentLoad" value="" NAME="ContentLoad">
								<input type="hidden" ID="ContentFlag" value="0" NAME="ContentFlag">
								<iframe class="Composition" ID="eWebEditor" MARGINHEIGHT="1" MARGINWIDTH="1" width="100%"
									height="100%" scrolling="yes"></iframe>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="25">
					<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%" class="StatusBar" height="25"
						ID="Table3">
						<TR valign="middle">
							<td>
								<table border="0" cellpadding="0" cellspacing="0" height="20" ID="Table4">
									<tr>
										<td width="10"></td>
										<td class="StatusBarBtnOff" id="eWebEditor_CODE" onclick="setMode('CODE')"><img border="0" src="buttonimage/standard/modecode.gif" width="50" height="15" align="absmiddle"></td>
										<td width="5"></td>
										<td class="StatusBarBtnOff" id="eWebEditor_EDIT" onclick="setMode('EDIT')"><img border="0" src="buttonimage/standard/modeedit.gif" width="50" height="15" align="absmiddle"></td>
										<td width="5"></td>
										<td class="StatusBarBtnOff" id="eWebEditor_VIEW" onclick="setMode('VIEW')"><img border="0" src="buttonimage/standard/modepreview.gif" width="50" height="15" align="absmiddle"></td>
									</tr>
								</table>
							</td>
							<td align="right">
								<table border="0" cellpadding="0" cellspacing="0" height="20" ID="Table5">
									<tr>
										<td style="cursor:pointer;" onclick="sizeChange(300)"><img border="0" SRC="buttonimage/standard/sizeplus.gif" width="20" height="20" alt="增高编辑区"></td>
										<td width="5"></td>
										<td style="cursor:pointer;" onclick="sizeChange(-300)"><img border="0" SRC="buttonimage/standard/sizeminus.gif" width="20" height="20" alt="减小编辑区"></td>
										<td width="40"></td>
									</tr>
								</table>
							</td>
						</TR>
					</TABLE>
				</td>
			</tr>
		</table>
		<div id="divTemp" style="VISIBILITY: hidden; OVERFLOW: hidden; POSITION: absolute; WIDTH: 1px; HEIGHT: 1px"></div>
	</body>
</html>
