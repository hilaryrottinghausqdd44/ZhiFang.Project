


<html>
<head>
<title>eWebEditor - eWebSoft�����ı��༭��</title>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312">
<link href="css/light/Editor.css" type="text/css" rel="stylesheet">

<Script Language=Javascript>
var sPath = document.location.pathname;
sPath = sPath.substr(0, sPath.length-14);

var sLinkFieldName = "content1" ;

// ȫ�����ö���
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
//return true ;	 // ���ش���
</script>

</head>

<body SCROLLING=no  onfocus="VerifyFocus()"><!--onConTextMenu="event.returnValue=false;"-->

<table border=0 cellpadding=0 cellspacing=0 width='100%' height='100%' ID="Table1">
<tr><td>

	<table border=0 cellpadding=0 cellspacing=0 width='100%' class='Toolbar' id='eWebEditor_Toolbar'><tr><td><div class=yToolbar><DIV CLASS="TBHandle"></DIV><SELECT CLASS="TBGen" onchange="format('FormatBlock',this[this.selectedIndex].value);this.selectedIndex=0" ID="Select1" NAME="Select1"><option selected>������ʽ</option>
<option value="&lt;P&gt;">��ͨ</option>
<option value="&lt;H1&gt;">����һ</option>
<option value="&lt;H2&gt;">�����</option>
<option value="&lt;H3&gt;">������</option>
<option value="&lt;H4&gt;">������</option>
<option value="&lt;H5&gt;">������</option>
<option value="&lt;H6&gt;">������</option>
<option value="&lt;p&gt;">����</option>
<option value="&lt;dd&gt;">����</option>
<option value="&lt;dt&gt;">���ﶨ��</option>
<option value="&lt;dir&gt;">Ŀ¼�б�</option>
<option value="&lt;menu&gt;">�˵��б�</option>
<option value="&lt;PRE&gt;">�ѱ��Ÿ�ʽ</option></SELECT><SELECT CLASS="TBGen" onchange="format('fontname',this[this.selectedIndex].value);this.selectedIndex=0" ID="Select2" NAME="Select2"><option selected>����</option>
<option value="����">����</option>
<option value="����">����</option>
<option value="����_GB2312">����</option>
<option value="����_GB2312">����</option>
<option value="����">����</option>
<option value="��Բ">��Բ</option>
<option value="Arial">Arial</option>
<option value="Arial Black">Arial Black</option>
<option value="Arial Narrow">Arial Narrow</option>
<option value="Brush Script	MT">Brush Script MT</option>
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
<option value="Wingdings">Wingdings</option></SELECT><SELECT CLASS="TBGen" onchange="format('fontsize',this[this.selectedIndex].value);this.selectedIndex=0" ID="Select3" NAME="Select3"><option selected>�ֺ�</option>
<option value="7">һ��</option>
<option value="6">����</option>
<option value="5">����</option>
<option value="4">�ĺ�</option>
<option value="3">���</option>
<option value="2">����</option>
<option value="1">�ߺ�</option></SELECT><SELECT CLASS="TBGen" onchange="doZoom(this[this.selectedIndex].value)" ID="Select4" NAME="Select4"><option value="10">10%</option>
<option value="25">25%</option>
<option value="50">50%</option>
<option value="75">75%</option>
<option value="100" selected>100%</option>
<option value="150">150%</option>
<option value="200">200%</option>
<option value="500">500%</option></SELECT><DIV CLASS="Btn" TITLE="����" onclick="format('bold')"><IMG CLASS="Ico" SRC="buttonimage/standard/bold.gif"></DIV><DIV CLASS="Btn" TITLE="б��" onclick="format('italic')"><IMG CLASS="Ico" SRC="buttonimage/standard/italic.gif"></DIV><DIV CLASS="Btn" TITLE="�»���" onclick="format('underline')"><IMG CLASS="Ico" SRC="buttonimage/standard/underline.gif"></DIV><DIV CLASS="Btn" TITLE="�л���" onclick="format('StrikeThrough')"><IMG CLASS="Ico" SRC="buttonimage/standard/strikethrough.gif"></DIV><DIV CLASS="Btn" TITLE="�ϱ�" onclick="format('superscript')"><IMG CLASS="Ico" SRC="buttonimage/standard/superscript.gif"></DIV><DIV CLASS="Btn" TITLE="�±�" onclick="format('subscript')"><IMG CLASS="Ico" SRC="buttonimage/standard/subscript.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="�����" onclick="format('justifyleft')"><IMG CLASS="Ico" SRC="buttonimage/standard/JustifyLeft.gif"></DIV><DIV CLASS="Btn" TITLE="���ж���" onclick="format('justifycenter')"><IMG CLASS="Ico" SRC="buttonimage/standard/JustifyCenter.gif"></DIV><DIV CLASS="Btn" TITLE="�Ҷ���" onclick="format('justifyright')"><IMG CLASS="Ico" SRC="buttonimage/standard/JustifyRight.gif"></DIV><DIV CLASS="Btn" TITLE="���˶���" onclick="format('JustifyFull')"><IMG CLASS="Ico" SRC="buttonimage/standard/JustifyFull.gif"></DIV></div></td></tr><tr><td><div class=yToolbar><DIV CLASS="TBHandle"></DIV><DIV CLASS="Btn" TITLE="����" onclick="format('cut')"><IMG CLASS="Ico" SRC="buttonimage/standard/cut.gif"></DIV><DIV CLASS="Btn" TITLE="����" onclick="format('copy')"><IMG CLASS="Ico" SRC="buttonimage/standard/copy.gif"></DIV><DIV CLASS="Btn" TITLE="����ճ��" onclick="format('paste')"><IMG CLASS="Ico" SRC="buttonimage/standard/paste.gif"></DIV><DIV CLASS="Btn" TITLE="���ı�ճ��" onclick="PasteText()"><IMG CLASS="Ico" SRC="buttonimage/standard/pastetext.gif"></DIV><DIV CLASS="Btn" TITLE="��Word��ճ��" onclick="PasteWord()"><IMG CLASS="Ico" SRC="buttonimage/standard/pasteword.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="�����滻" onclick="findReplace()"><IMG CLASS="Ico" SRC="buttonimage/standard/findreplace.gif"></DIV><DIV CLASS="Btn" TITLE="ɾ��" onclick="format('delete')"><IMG CLASS="Ico" SRC="buttonimage/standard/delete.gif"></DIV><DIV CLASS="Btn" TITLE="ɾ�����ָ�ʽ" onclick="format('RemoveFormat')"><IMG CLASS="Ico" SRC="buttonimage/standard/RemoveFormat.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="����" onclick="format('undo')"><IMG CLASS="Ico" SRC="buttonimage/standard/undo.gif"></DIV><DIV CLASS="Btn" TITLE="�ָ�" onclick="format('redo')"><IMG CLASS="Ico" SRC="buttonimage/standard/redo.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="ȫ��ѡ��" onclick="format('SelectAll')"><IMG CLASS="Ico" SRC="buttonimage/standard/selectAll.gif"></DIV><DIV CLASS="Btn" TITLE="ȡ��ѡ��" onclick="format('Unselect')"><IMG CLASS="Ico" SRC="buttonimage/standard/unselect.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="���" onclick="format('insertorderedlist')"><IMG CLASS="Ico" SRC="buttonimage/standard/insertorderedlist.gif"></DIV><DIV CLASS="Btn" TITLE="��Ŀ����" onclick="format('insertunorderedlist')"><IMG CLASS="Ico" SRC="buttonimage/standard/insertunorderedlist.gif"></DIV><DIV CLASS="Btn" TITLE="����������" onclick="format('indent')"><IMG CLASS="Ico" SRC="buttonimage/standard/indent.gif"></DIV><DIV CLASS="Btn" TITLE="����������" onclick="format('outdent')"><IMG CLASS="Ico" SRC="buttonimage/standard/outdent.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="������ɫ" onclick="ShowDialog('dialog/selcolor.htm?action=forecolor', 280, 250, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/forecolor.gif"></DIV><DIV CLASS="Btn" TITLE="���󱳾���ɫ" onclick="ShowDialog('dialog/selcolor.htm?action=bgcolor', 280, 250, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/bgcolor.gif"></DIV><DIV CLASS="Btn" TITLE="���屳����ɫ" onclick="ShowDialog('dialog/selcolor.htm?action=backcolor', 280, 250, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/backcolor.gif"></DIV><DIV CLASS="Btn" TITLE="����ͼƬ" onclick="ShowDialog('dialog/backimage.htm', 350, 210, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/bgpic.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="���Ի����λ��" onclick="absolutePosition()"><IMG CLASS="Ico" SRC="buttonimage/standard/abspos.gif"></DIV><DIV CLASS="Btn" TITLE="����һ��" onclick="zIndex('forward')"><IMG CLASS="Ico" SRC="buttonimage/standard/forward.gif"></DIV><DIV CLASS="Btn" TITLE="����һ��" onclick="zIndex('backward')"><IMG CLASS="Ico" SRC="buttonimage/standard/backward.gif"></DIV></div></td></tr><tr><td><div class=yToolbar><DIV CLASS="TBHandle"></DIV><DIV CLASS="Btn" TITLE="������޸�ͼƬ" onclick="ShowDialog('dialog/img.htm', 350, 315, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/img.gif"></DIV><DIV CLASS="Btn" TITLE="����Flash����" onclick="ShowDialog('dialog/flash.htm', 350, 200, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/flash.gif"></DIV><DIV CLASS="Btn" TITLE="�����Զ����ŵ�ý���ļ�" onclick="ShowDialog('dialog/media.htm', 350, 200, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/Media.gif"></DIV><DIV CLASS="Btn" TITLE="���������ļ�" onclick="ShowDialog('dialog/file.htm', 350, 150, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/file.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="���˵�" onclick="showToolMenu('table')"><IMG CLASS="Ico" SRC="buttonimage/standard/tablemenu.gif"></DIV><DIV CLASS="Btn" TITLE="���˵�" onclick="showToolMenu('form')"><IMG CLASS="Ico" SRC="buttonimage/standard/FormMenu.gif"></DIV><DIV CLASS="Btn" TITLE="��ʾ������ָ������" onclick="showBorders()"><IMG CLASS="Ico" SRC="buttonimage/standard/ShowBorders.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="������޸���Ŀ��" onclick="ShowDialog('dialog/fieldset.htm', 350, 170, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/fieldset.gif"></DIV><DIV CLASS="Btn" TITLE="������޸���ҳ֡" onclick="ShowDialog('dialog/iframe.htm', 600, 400, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/iframe.gif"></DIV><DIV CLASS="Btn" TITLE="����ˮƽ��" onclick="format('InsertHorizontalRule')"><IMG CLASS="Ico" SRC="buttonimage/standard/InsertHorizontalRule.gif"></DIV><DIV CLASS="Btn" TITLE="������޸���Ļ" onclick="ShowDialog('dialog/marquee.htm', 395, 150, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/Marquee.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="������޸ĳ�������" onclick="format('CreateLink')"><IMG CLASS="Ico" SRC="buttonimage/standard/CreateLink.gif"></DIV><DIV CLASS="Btn" TITLE="ȡ���������ӻ��ǩ" onclick="format('UnLink')"><IMG CLASS="Ico" SRC="buttonimage/standard/Unlink.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="���������ַ�" onclick="ShowDialog('dialog/symbol.htm', 350, 220, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/symbol.gif"></DIV><DIV CLASS="Btn" TITLE="�������ͼ��" onclick="ShowDialog('dialog/emot.htm', 300, 180, true)"><IMG CLASS="Ico" SRC="buttonimage/standard/emot.gif"></DIV><DIV CLASS="Btn" TITLE="����Excel���" onclick="insert('excel')"><IMG CLASS="Ico" SRC="buttonimage/standard/excel.gif"></DIV><DIV CLASS="Btn" TITLE="���뵱ǰ����" onclick="insert('nowdate')"><IMG CLASS="Ico" SRC="buttonimage/standard/date.gif"></DIV><DIV CLASS="Btn" TITLE="���뵱ǰʱ��" onclick="insert('nowtime')"><IMG CLASS="Ico" SRC="buttonimage/standard/time.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="������ʽ" onclick="insert('quote')"><IMG CLASS="Ico" SRC="buttonimage/standard/quote.gif"></DIV><DIV CLASS="Btn" TITLE="������ʽ" onclick="insert('code')"><IMG CLASS="Ico" SRC="buttonimage/standard/code.gif"></DIV><DIV CLASS="TBSep"></DIV><DIV CLASS="Btn" TITLE="ȫ���༭" onclick="Maximize()"><IMG CLASS="Ico" SRC="buttonimage/standard/maximize.gif"></DIV><DIV CLASS="Btn" TITLE="�鿴ʹ�ð���" onclick="ShowDialog('dialog/help.htm','400','300')"><IMG CLASS="Ico" SRC="buttonimage/standard/help.gif"></DIV></div></td></tr></table>

</td></tr>
<tr><td height='100%'>

	<table border=0 cellpadding=0 cellspacing=0 width='100%' height='100%' ID="Table2">
	<tr><td height='100%'>
	<input type="hidden" ID="ContentEdit" value="" NAME="ContentEdit">
	<input type="hidden" ID="ContentLoad" value="" NAME="ContentLoad">
	<input type="hidden" ID="ContentFlag" value="0" NAME="ContentFlag">
	<iframe class="Composition" ID="eWebEditor" MARGINHEIGHT="1" MARGINWIDTH="1" width="100%" height="100%" scrolling="yes"> 
	</iframe>
	</td></tr>
	</table>

</td></tr>


<tr><td height=25>

	<TABLE border="0" cellPadding="0" cellSpacing="0" width="100%" class=StatusBar height=25 ID="Table3">
	<TR valign=middle>
	<td>
		<table border=0 cellpadding=0 cellspacing=0 height=20 ID="Table4">
		<tr>
		<td width=10></td>
		<td class=StatusBarBtnOff id=eWebEditor_CODE onclick="setMode('CODE')"><img border=0 src="buttonimage/standard/modecode.gif" width=50 height=15 align=absmiddle></td>
		<td width=5></td>
		<td class=StatusBarBtnOff id=eWebEditor_EDIT onclick="setMode('EDIT')"><img border=0 src="buttonimage/standard/modeedit.gif" width=50 height=15 align=absmiddle></td>
		<td width=5></td>
		<td class=StatusBarBtnOff id=eWebEditor_VIEW onclick="setMode('VIEW')"><img border=0 src="buttonimage/standard/modepreview.gif" width=50 height=15 align=absmiddle></td>
		</tr>
		</table>
	</td>
	<td align=center id=eWebEditor_License style="font-size:9pt; cursor:pointer;">
		<table border=0 cellpadding=0 cellspacing=0 height=20 ID="Table6" style="font-size:9pt;">
		<tr>
		<td valign=bottom>����ͼƬ</td>
		<td style="cursor:pointer;"  onclick="insertSiteMapElement('XXSmall')"><img border=0 SRC="../images/icons/tu.jpg" width=16 height=16 alt="ͼƬ��XXSmall"></td>
		<td width=5></td>
		<td style="cursor:pointer;"  onclick="insertSiteMapElement('XSmall')"><img border=0 SRC="../images/icons/tu.jpg" width=18 height=18 alt="ͼƬ��XSmall"></td>
		<td width=5></td>
		<td style="cursor:pointer;"  onclick="insertSiteMapElement('Small')"><img border=0 SRC="../images/icons/tu.jpg" width=19 height=19 alt="ͼƬ��Small"></td>
		<td width=5></td>
		<td style="cursor:pointer;"  onclick="insertSiteMapElement('Large')"><img border=0 SRC="../images/icons/tu.jpg" width=20 height=20 alt="ͼƬ��Large"></td>
		<td width=5></td>
		<td style="cursor:pointer;"  onclick="insertSiteMapElement('XLarge')"><img border=0 SRC="../images/icons/tu.jpg" width=21 height=21 alt="ͼƬ��XLarge"></td>
		<td width=5></td>
		<td style="cursor:pointer;"  onclick="insertSiteMapElement('XXLarge')"><img border=0 SRC="../images/icons/tu.jpg" width=22 height=22 alt="ͼƬ��XXLarge"></td>
		<td width=40></td>
		</tr>
		</table>
	</td>
	<td align=right>
		<table border=0 cellpadding=0 cellspacing=0 height=20 ID="Table5">
		<tr>
		<td style="cursor:pointer;" onclick="sizeChange(300)"><img border=0 SRC="buttonimage/standard/sizeplus.gif" width=20 height=20 alt="���߱༭��"></td>
		<td width=5></td>
		<td style="cursor:pointer;" onclick="sizeChange(-300)"><img border=0 SRC="buttonimage/standard/sizeminus.gif" width=20 height=20 alt="��С�༭��"></td>
		<td width=40></td>
		</tr>
		</table>
	</td>
	</TR>
	</Table>

</td></tr>


</table>

<div id="divTemp" style="VISIBILITY: hidden; OVERFLOW: hidden; POSITION: absolute; WIDTH: 1px; HEIGHT: 1px"></div>

</body>
</html>
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

