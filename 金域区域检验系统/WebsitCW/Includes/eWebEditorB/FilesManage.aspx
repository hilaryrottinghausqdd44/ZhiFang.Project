<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FilesManage.aspx.cs" Inherits="OA.Includes.eWebEditorB.FilesManage" %>



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <title>�ļ�������</title>
    <link href="../../modulemanage/style.css" rel="stylesheet" />
    <script type="text/javascript">	
       
       
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
        
    //����������Ƶ
    function NetUrl() {
        var aG = document.getElementById('pneturl').value;
        var aW = document.getElementById('pwidth').value;
        var aH = document.getElementById('pheight').value;
        var re_text = /\mpg|\mpeg|\wmv|\asf|\wma|\avi|\RM|\rmvb/i;             
        var filename = aG;          
        var newFileName = filename.split('.');     
        newFileName = newFileName[newFileName.length - 1];          

        /* Checking file type */
        if (newFileName.search(re_text) == -1) {             
            alert("ֻ������mpg, mpeg,asf,wma,wmv,avi,rm,rmvb���͵���Ƶ�ļ�");
            return false;
        }
        else {
            VideoPlay(aG, aW, aH);
        }
    }
    var prev = null;
        //�����ϴ���Ƶ
        function selectx(row,index)   /**//*�ı�ѡ���е���ɫ��ԭΪѡ���е���ɫ*/
        {
            if(prev!=null)
            {
                prev.style.backgroundColor='#fff';
            }
            row.style.backgroundColor='#e4ecf1';
            var aG = '../../includes/ewebeditorb/videouploadfile/' + $('NAME_' + index).value;
            var aW = document.getElementById('pwidth').value;
            var aH = document.getElementById('pheight').value;
            VideoPlay(aG, aW, aH) 
        }
        function VideoPlay(aG, aW, aH) 
        {
            var ad = aG.lastIndexOf(".");
            var aExtens = aG.substring(ad + 1).toLowerCase();
            var as = "";
            if (aExtens == "rm" || aExtens == "rmvb") {
                as = '<object id="vid" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width="' + aW + '" height="' + aH + '">';
                as = as + '<param name="_ExtentX" value="11298" />';
                as = as + '<param name="_ExtentY" value="7938" />';
                as = as + '<param name="AUTOSTART" value="-1" />';
                as = as + '<!--AUTOSTART 0Ϊ��ҳ�洦�ڴ���״̬��Ϊ-1ʱ��ҳ��ֱ�Ӳ���-->';
                as = as + '<param name="SHUFFLE" value="0" />';
                as = as + '<param name="PREFETCH" value="0" />';
                as = as + '<param name="NOLABELS" value="-1" />';
                as = as + '<param name="SRC" value="' + aG + '"; />';
                as = as + '<!--�����ļ���ַ-->';
                as = as + '<param name="CONTROLS" value="Imagewindow" />';
                as = as + '<param name="CONSOLE" value="clip1" />';
                as = as + '<param name="LOOP" value="0" />';
                as = as + '<param name="NUMLOOP" value="0" />';
                as = as + '<param name="CENTER" value="0" />';
                as = as + '<param name="MAINTAINASPECT" value="0" />';
                as = as + '<param name="BACKGROUNDCOLOR" value="#000000" />';
                as = as + '</object><br/>';
                as = as + '<object id="vid2" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width="' + aW + '" height="30">';
                as = as + '<param name="_ExtentX" value="11298" />';
                as = as + '<param name="_ExtentY" value="794" />';
                as = as + '<param name="AUTOSTART" value="-1" />';
                as = as + '<param name="SHUFFLE" value="0" />';
                as = as + '<param name="PREFETCH" value="0" />';
                as = as + '<param name="NOLABELS" value="-1" />';
                as = as + '<param name="SRC" value="' + aG + '"; />';
                as = as + '<!--�����ļ���ַ-->';
                as = as + '<param name="CONTROLS" value="ControlPanel" />';
                as = as + '<param name="CONSOLE" value="clip1" />';
                as = as + '<param name="LOOP" value="0" />';
                as = as + '<param name="NUMLOOP" value="0" />';
                as = as + '<param name="CENTER" value="0" />';
                as = as + '<param name="MAINTAINASPECT" value="0" />';
                as = as + '<param name="BACKGROUNDCOLOR" value="#000000" />';
                as = as + '</object>';
            }
            else {
                //as = '<EMBED src="' + aG + '" width="' + aW + '" height="' + aH + '" type="audio/x-pn-realaudio-plugin" autostart="true" controls="IMAGEWINDOW,ControlPanel,StatusBar" console="Clip1"></EMBED>';
                as = '<object id="mPlayer1" width="' + aW + '" height="' + aH + '" classid="CLSID:6BF52A52-394A-11D3-B153-00C04F79FAA6">';
                as = as + '<param name="URL" value="' + aG + '">';
                as = as + '<param name="rate" value="1">';
                as = as + '<param name="balance" value="0">';
                as = as + '<param name="currentPosition" value="0">';
                as = as + '<param name="defaultFrame" value>';
                as = as + '<param name="playCount" value="1">';
                as = as + '<param name="autoStart" value="1">';
                as = as + '<param name="currentMarker" value="0">';
                as = as + '<param name="invokeURLs" value="1">';
                as = as + '<param name="baseURL" value>';
                as = as + '<param name="volume" value="100">';
                as = as + '<param name="mute" value="0">';
                as = as + '<param name="uiMode" value="full">';
                as = as + '<param name="stretchToFit" value="0">';
                as = as + '<param name="windowlessVideo" value="0">';
                as = as + '<param name="enabled" value="1">';
                as = as + '<param name="enableContextMenu" value="1">';
                as = as + '<param name="fullScreen" value="0">';
                as = as + '<param name="SAMIStyle" value>';
                as = as + '<param name="SAMILang" value>';
                as = as + '<param name="SAMIFilename" value>';
                as = as + '<param name="captioningID" value>';
                as = as + '<param name="enableErrorDialogs" value="0">';
                as = as + '<param name="_cx" value="7779">';
                as = as + '<param name="_cy" value="1693">';
                as = as + '</object>';
            }
            window.returnValue = as;
            window.close();
        }
        function check() 
        {
            if (document.getElementById("File").value == "" || document.getElementById("File").value == null) {

                alert("��ѡ��Ҫ�ϴ����ļ�");
                document.getElementById("File").focus();
                return false;
            }
            return true;
        }
        //����ϴ��ļ�������
        function clickFileName(upload_field) 
        {
            var re_text = /\mpg|\mpeg|\asf|\wma|\wmv|\avi|\RM|\rmvb/i;             //�����ȽϹؼ�,�����ϴ����ļ�����,�����ϴ��ľ�д��.
            var filename = upload_field.value;          //����ǵõ��ļ����ֵ
            var newFileName = filename.split('.');        //���ǽ��ļ����Ե�ֿ�,��Ϊ��׺�϶����Ե�ʲô��β��.
            newFileName = newFileName[newFileName.length-1];           //����ǵõ��ļ���׺,��Ϊsplit����һ���������������Ǹ���������ļ��ĺ�׺��.
            /* Checking file type */
            if (newFileName.search(re_text) == -1) 
            {                  //search���û�иշ���-1.������newFileName��re_text��û��,��Ϊ�������ϴ�������. .
               alert("ֻ�����ϴ�mpg,mpeg,asf,wma,wmv,avi,rm,rmvb���͵���Ƶ�ļ�");
               upload_field.form.reset();
               return false;
           }
//           if (ShowSize() > 3) {
//               alert('�ϴ��ļ�Ӧ��С��3M');
//               upload_field.form.reset();
//               return false;
//           }
     }
     
     function ShowSize() {
         var fso, f, files;
         files = form1.File.value;
         fso = new ActiveXObject("Scripting.FileSystemObject");
         f = fso.GetFile(files);
         var fileSize = f.size;
  
         return fileSize / (1024 * 1024);
       
     }  



    </script>
<script type="text/javascript">
    var falpha;
    falpha = 0;
    function fchange() {
        if (falpha != 90) {
            table1.style.filter = "alpha(opacity=" + falpha + ")";
            falpha = falpha + 10;
            setTimeout("fchange()", 200);
        }
        else {
            falpha = 0;
        }
    }
    //�������ڲ�����Ƶ 
    function sss(row,videourl) {

        var tabledg = document.getElementById('myDataGrid');
        //���ǰ��ѡ���б���ɫ
        var hidbg = document.getElementById('hidbackground').value;
        if (hidbg != null || hidbg != "") {
            tabledg.rows[hidbg].style.backgroundColor = "";
        }
        tabledg.rows[row].style.backgroundColor = "yellow";
        document.getElementById('hidbackground').value = row;
        table1.style.height = (window.document.body.clientHeight > window.document.body.scrollHeight) ? window.document.body.clientHeight : window.document.body.scrollHeight;
        table1.style.width = "100%";
        table1.style.display = 'block'
        table2.style.left = document.documentElement.scrollLeft + 50;
        table2.style.top = document.body.scrollTop + 50;
        table2.style.display = 'block';
        fchange()

        var aG = 'videouploadfile/' + videourl;
        var aW = 400; 
        var aH = 300;

        var ad = aG.lastIndexOf(".");
        var aExtens = aG.substring(ad + 1).toLowerCase();
        var as = "";
        if (aExtens == "rm" || aExtens == "rmvb") {
            as = '<object id="vid" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width="' + aW + '" height="' + aH + '">';
            as = as + '<param name="_ExtentX" value="11298" />';
            as = as + '<param name="_ExtentY" value="7938" />';
            as = as + '<param name="AUTOSTART" value="-1" />';
            as = as + '<!--AUTOSTART 0Ϊ��ҳ�洦�ڴ���״̬��Ϊ-1ʱ��ҳ��ֱ�Ӳ���-->';
            as = as + '<param name="SHUFFLE" value="0" />';
            as = as + '<param name="PREFETCH" value="0" />';
            as = as + '<param name="NOLABELS" value="-1" />';
            as = as + '<param name="SRC" value="' + aG + '"; />';
            as = as + '<!--�����ļ���ַ-->';
            as = as + '<param name="CONTROLS" value="Imagewindow" />';
            as = as + '<param name="CONSOLE" value="clip1" />';
            as = as + '<param name="LOOP" value="0" />';
            as = as + '<param name="NUMLOOP" value="0" />';
            as = as + '<param name="CENTER" value="0" />';
            as = as + '<param name="MAINTAINASPECT" value="0" />';
            as = as + '<param name="BACKGROUNDCOLOR" value="#000000" />';
            as = as + '</object><br/>';
            as = as + '<object id="vid2" classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width="' + aW + '" height="30">';
            as = as + '<param name="_ExtentX" value="11298" />';
            as = as + '<param name="_ExtentY" value="794" />';
            as = as + '<param name="AUTOSTART" value="-1" />';
            as = as + '<param name="SHUFFLE" value="0" />';
            as = as + '<param name="PREFETCH" value="0" />';
            as = as + '<param name="NOLABELS" value="-1" />';
            as = as + '<param name="SRC" value="' + aG + '"; />';
            as = as + '<!--�����ļ���ַ-->';
            as = as + '<param name="CONTROLS" value="ControlPanel" />';
            as = as + '<param name="CONSOLE" value="clip1" />';
            as = as + '<param name="LOOP" value="0" />';
            as = as + '<param name="NUMLOOP" value="0" />';
            as = as + '<param name="CENTER" value="0" />';
            as = as + '<param name="MAINTAINASPECT" value="0" />';
            as = as + '<param name="BACKGROUNDCOLOR" value="#000000" />';
            as = as + '</object>';
        }
        else {
            //as = '<EMBED src="' + aG + '" width="' + aW + '" height="' + aH + '" type="audio/x-pn-realaudio-plugin" autostart="true" controls="IMAGEWINDOW,ControlPanel,StatusBar" console="Clip1"></EMBED>';
            as = '<object id="mPlayer1" width="' + aW + '" height="' + aH + '" classid="CLSID:6BF52A52-394A-11D3-B153-00C04F79FAA6">';
            as = as + '<param name="URL" value="' + aG + '">';
            as = as + '<param name="rate" value="1">';
            as = as + '<param name="balance" value="0">';
            as = as + '<param name="currentPosition" value="0">';
            as = as + '<param name="defaultFrame" value>';
            as = as + '<param name="playCount" value="1">';
            as = as + '<param name="autoStart" value="1">';
            as = as + '<param name="currentMarker" value="0">';
            as = as + '<param name="invokeURLs" value="1">';
            as = as + '<param name="baseURL" value>';
            as = as + '<param name="volume" value="100">';
            as = as + '<param name="mute" value="0">';
            as = as + '<param name="uiMode" value="full">';
            as = as + '<param name="stretchToFit" value="0">';
            as = as + '<param name="windowlessVideo" value="0">';
            as = as + '<param name="enabled" value="1">';
            as = as + '<param name="enableContextMenu" value="1">';
            as = as + '<param name="fullScreen" value="0">';
            as = as + '<param name="SAMIStyle" value>';
            as = as + '<param name="SAMILang" value>';
            as = as + '<param name="SAMIFilename" value>';
            as = as + '<param name="captioningID" value>';
            as = as + '<param name="enableErrorDialogs" value="0">';
            as = as + '<param name="_cx" value="7779">';
            as = as + '<param name="_cy" value="1693">';
            as = as + '</object>';
        }
        table2.innerHTML = table2.innerHTML + "<br/>"+as;
    }

    function freset() {
        table1.style.display = 'none';
        table2.style.display = 'none';
        table2.innerHTML = '<div style="text-align:right;height:5;color:Red"><a href="#" onclick="freset()"><font color=red>�ر�</font></a></div>';
    }

</script>


 


    <style type="text/css">
        .hideClass
        {
            display: none;
        }
    </style>
    <base target="_self" />
</head>
<body>
    <form id="form1" method="post" encType="multipart/form-data" runat="server">
    <input type="hidden" id="hidbackground" value="1" />
    <div id="table1" style=" background:#FFFFFF; display: none; position: absolute;z-index:1;filter:alpha(opacity=40)" oncontextmenu="return false">
 
</div>

<div id="table2" style="position:absolute; onmousedown=MDown(table2);background:#000000; display:none; z-index:2; width:400; height:300; border-left:solid #000000 1px; border-top:solid #000000 1px; border-bottom:solid #000000 1px; border-right:solid #000000 1px; cursor:move;" cellspacing="0" cellpadding="0" oncontextmenu="return false">
 <div style="text-align:right;height:5;color:Red"><a href="#" onclick="freset()"><font color=red>�ر�</font></a></div>
 
</div>


    <div>
     <table bordercolor="#003366" height="100%" cellspacing="0" bordercolordark="#ffffff"
            cellpadding="1" width="98%" align="center" bgcolor="#f6f6f6" bordercolorlight="#aecdd5"
            border="1">
            <tbody>
                <tr>
                    <td valign="top">
                        <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="10"
                            width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
                            <tbody>
                              <tr>
                                    <td align="left" colspan="2">
                                       ��������:<input type="text" id="pneturl" value="http://" /> &nbsp;<input type="button" id="btnneturl" onclick="NetUrl();" value="��������ȷ��" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                             �ļ��ϴ�:<INPUT type="file" NAME="File" onChange="clickFileName(this)"><asp:Button runat="server" 
                                                 ID="btnfileupload" Text="�ϴ�" onclick="btnfileupload_Click" Width="48px" />
                                    </td>
                                </tr>
                                <tr><td colspan="2">ָ��������Ƶ�Ŀ��<input type="text" id="pwidth" style="width:50px;" value="500" />�߶�<input type="text" style="width:50px;" id="pheight" value="400" /></td></tr>
                                <tr><td colspan="2"></td></tr>
                                <tr>
                                    <td colspan="2">
                                    �ؼ���:<asp:TextBox runat="server" ID="txtkey"></asp:TextBox>
                                    <asp:Button runat="server" ID="btnsearch" Text="����" onclick="btnsearch_Click" />
                                        <asp:DataGrid ID="myDataGrid" runat="server" PageSize="15" Font-Size="Smaller" BorderWidth="1px"
                                            CellPadding="3" BorderStyle="None" AllowPaging="false" BackColor="White" AutoGenerateColumns="False"
                                            BorderColor="#A7C4F7" Width="100%" 
                                            onitemdatabound="myDataGrid_ItemDataBound" 
                                            onitemcommand="myDataGrid_ItemCommand">
                                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                            <ItemStyle ForeColor="#330099" BackColor="White"></ItemStyle>
                                            <HeaderStyle Font-Size="Smaller" Font-Bold="True" ForeColor="Black" BackColor="#990000">
                                            </HeaderStyle>
                                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                                            <Columns>
                                                 <asp:TemplateColumn SortExpression="���" HeaderText="���">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    <ItemTemplate>                                                    
                                                       <%# Container.ItemIndex + 1%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="����" HeaderText="����">
                                                    <HeaderStyle HorizontalAlign="Center" Width="40%" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
                                                    <ItemTemplate>                                                    
                                                        <a href="#" onclick="selectx(this,'<%# DataBinder.Eval(Container, "DataItem.name") %>');"><%# DataBinder.Eval(Container, "DataItem.name") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn SortExpression="Ԥ��" HeaderText="Ԥ��">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>                                                    
                                                        <div onclick="javascript:sss('<%# Container.ItemIndex+1%>','<%# DataBinder.Eval(Container, "DataItem.name") %>');">Ԥ��</div>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn SortExpression="��С" HeaderText="��С">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>                                                    
                                                        <%# DataBinder.Eval(Container, "DataItem.size") %>kb
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                 <asp:TemplateColumn SortExpression="����ʱ��" HeaderText="����ʱ��">
                                                    <HeaderStyle HorizontalAlign="Center" BackColor="Silver"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    <ItemTemplate>                                                    
                                                        <%# DataBinder.Eval(Container, "DataItem.createdate")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
													<HeaderStyle CssClass="hideClass"></HeaderStyle>
													<ItemStyle CssClass="hideClass"></ItemStyle>
													<ItemTemplate>
														<input id='<%#"NAME_" + DataBinder.Eval(Container, "DataItem.name")%>' value='<%# DataBinder.Eval(Container, "DataItem.name") %>'
															type="text" />
													</ItemTemplate>
												</asp:TemplateColumn>
												 <asp:TemplateColumn HeaderText="����">
                                                    <HeaderStyle HorizontalAlign="Center" Width="10%" BackColor="Silver" Wrap="False">
                                                    </HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" BackColor="#D1CEFF" Font-Bold="True">
                                                    </ItemStyle>
                                                    <ItemTemplate>                                                        
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete">ɾ��</asp:LinkButton>
                                                        <asp:Label ID="labfilename" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>'
                                                            Visible="False">
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Right" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                              
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    
    </form>
</body>
</html>
