<%@ Page Language="C#" enableEventValidation="false" AutoEventWireup="true" CodeBehind="PrintPDFList.aspx.cs" Inherits="OA.DBQuery.Print.PrintPDFList" %>

<%@ Import Namespace="System.Xml" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批量浏览打印文件</title>

    <script type="text/javascript">
        var inum = 0;
        var imax = 3;
        var iIntervalId = null;
        function reonload() {
            //setTimeout(sayHelloWorld,3000);
            iIntervalId = setInterval(sayHelloWorld, 1000);

        }
        function sayHelloWorld() {
            inum++;
            div1.innerHTML = inum;
            if (inum == imax) {
                //alert(inum);
                clearInterval(iIntervalId);
                window.frames["frame2"].PrintPdf();

                alert('end');
            }
        }
        function test() {
            alert('begin');
            window.frames["frame1"].PrintPdf();
            reonload();
            //window.frames["frame2"].PrintPdf();

        }
        //测试在iframe打开另一页面
        function showpdf(fpath) {
            window.frames['frame1'].location.href = "PrintPDF.aspx?reportfile=" + fpath;
        }
        //全选复选框
        function SelectAll(spanChk) {
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            //alert(xState);
            for (var i = 1; i < Table111.rows.length; i++) {
                var row = Table111.rows[i];
                //alert(i);
                //取得CHECKBOX             
                var objCheck = row.cells[0].getElementsByTagName("INPUT")[0];
                var div = row.cells[1].getElementsByTagName("DIV")[0];
                //row.getElementById("ReportFormID");
                //alert("div:" + div+","+div.innerHTML);
                //如果CHECKBOX被选中
                if (objCheck != null && objCheck.type == "checkbox") 
                {
                    objCheck.checked = xState;
                }
            }
        }
        function ChangeButtonState(flag) 
        {
        
            //表示正在打印中按钮不可操作
            if (flag == "1") 
            {
                document.getElementById("btnsave").disabled = true;
                document.getElementById("btndao").disabled = true;                
            }
            else 
            {
                document.getElementById("btnsave").disabled = false;
                document.getElementById("btndao").disabled = false;
            }
        }
        //判断是否有选择的
        //记录选择行数数组
        var selectrows = null;
        var printtype = 0;//套打 1,非套打 0
        function JudgeSelectRow(printtypeflag) 
        {
            selectrows = new Array();
            var selectnum = 0;
            var flag = false;
            for (var i = 1; i < Table111.rows.length; i++) 
            {
                var row = Table111.rows[i];
                row.style.backgroundColor = 'white';
                //取得CHECKBOX             
                var objCheck = row.cells[0].getElementsByTagName("INPUT")[0];
                if (objCheck != null && objCheck.type == "checkbox" && objCheck.checked) 
                {
                    flag = true;
                    //记录选中行
                    selectrows[selectnum] = i;
                    //将数组索引加1
                    selectnum++;
                }
            }
            if (flag) {
                //alert('选择的类型:'+document.getElementById('changeprinttype').value);
                if (document.getElementById('changeprinttype').value == "1") 
                {
                    printtype = 1;
                }
                ChangeButtonState(1);
                //用循环selectrows(记录的是已选择的行)数组的方式进行打印                
                printsave1(0, 1);               
            }
            else 
            {
                alert('请选择要打印的数据!');
            }
        }


        var stime = null;
        var rownum = 1;
        //循环间隔时间
        var ftime = 5000;
        //送检单位
        var orgName = "";
        //姓名
        var cname = "";
        //打印操作
        //completeflag 0加载未完成 1 加载完成
        function printsave1(rownum, completeflag) 
        {
            var row = Table111.rows[selectrows[rownum]];
            window.status = rownum;
            //取得CHECKBOX
            //var objCheck = row.cells[0].getElementsByTagName("INPUT")[0];
            //var div = row.cells[1].getElementsByTagName("DIV")[0];
            //alert("div:" + div + "," + div.innerHTML + "," + objCheck.checked + "," + objCheck.type);
            //如果CHECKBOX被选中则执行操作
            //些处就不用再判断是已经选中的了
//            if (objCheck != null && objCheck.type == "checkbox" && objCheck.checked) 
//            {
                //取关键字
                var rid = "";
                //取打印次数
                var printtimes = "";
                //取文件路径
                var fpath = "";
               
                //循环列
                for (var i = 1; i < row.cells.length; i++) 
                {
                    var divfield = row.cells[i].getElementsByTagName("DIV")[0];
                    //alert(divfield.id);

                    if (divfield.id.toLowerCase() == "reportformid") {
                        rid = divfield.innerHTML;
                    }
                    if (divfield.id.toLowerCase() == "printtimes") {
                        printtimes = divfield.innerHTML;
                    }

                    if (printtype == "1") {                        
                        //套打
                        if (divfield.id.toLowerCase() == "tpdffile") {
                            fpath = divfield.innerHTML;
                        }
                    }
                    else {//非套打
                        if (divfield.id.toLowerCase() == "pdffile") {
                            fpath = divfield.innerHTML;
                        }
                    }

                    if (divfield.id.toLowerCase() == "orgname") {
                        orgName = divfield.innerHTML;
                    }
                    if (divfield.id.toLowerCase() == "cname") {
                        cname = divfield.innerHTML;
                    }
                    if (rid.length > 0 && printtimes.length > 0 && fpath.length > 0 && orgName.length > 0 && cname.length > 0) {
                        break;
                    }
                }
                //alert(rid + "," + printtimes + "," + fpath);
                divprint.innerHTML = "正在打印:  <img src='../../images/loading.gif'/>" + orgName + "   " + cname;
                window.status = rid + "," + printtimes + "," + fpath;
                //当是下一条记录时才刷新
                if (completeflag == 1) 
                {                
                   window.frames['frame1'].location.href = "PrintPDF.aspx?reportfile=" + fpath;                  
                }
                //判断是否加载完成,未完成则延时几秒
                if (window.frames["frame1"] != null && window.frames["frame1"].document != null
		                && window.frames["frame1"].document.readyState == "complete") 
		          {
		              if (window.frames["frame1"].GetIsNoPdf()) 
		              {
		                  window.frames["frame1"].PrintPdf();
		                  //更新打印次数
		                  updateprinttimes(rid, printtimes);
		                  row.style.backgroundColor = 'skyblue';
		              }
                    rownum++;
                    //当行数小于等于表行数减1时才往下执行
                    if (rownum <= selectrows.length-1) 
                    {
                        //延时5秒执行下一步
                        stime = setTimeout("printsave1(" + rownum + ",1)", ftime)
                    }
                    else 
                    {
                        //终止时钟循环
                        clearTimeout(stime);
                        window.status = "已打印完成";
                        divprint.innerHTML = "已打印完成";
                        ChangeButtonState(0);
                    }

                }
                else 
                {
                    //延时
                    stime = setTimeout("printsave1(" + rownum + ",0)", "1000")
                }

//            }
//            else 
//            {
//                rownum++;
//                if (rownum <= selectrows.length-1) 
//                {
//                    printsave1(rownum, 1);
//                }
//                else 
//                {
//                    divprint.innerHTML = "已打印完成";
//                }
//            }

        }
        //终止时钟停止打印
        function stopprint() {
            if (stime != null) {
                clearTimeout(stime);
                window.status = "已停止";
                divprint.innerHTML = "已停止,该打印:" + orgName + "   " + cname;
                ChangeButtonState(0);
            }
        }
        //更新打印次数
        function updateprinttimes(id, times) {
            OA.DBQuery.Print.PrintPDFList.UpdatePrintTimes(id, times, GetCallresult);
        }
        //回调结果
        function GetCallresult(result) {
        }
        //改变选中行背景色
        var changecolorobj = null;
        function ChangeBgColor(obj) {
            obj.style.backgroundColor = "#ddf3dd";
            if (changecolorobj != null) {
                changecolorobj.style.backgroundColor = "white";
                changecolorobj = obj;
            }
            else {
                changecolorobj = obj;
            }
        }
        function window_onload() {

            if (Table111.rows[1] != null) {
                ChangeBgColor(Table111.rows[1]);
            }
        }


        //用于记录翻页操作
        function RunPage(StartPage, PageCount) {
           //alert(StartPage+',总页数:'+PageCount)
            Form1.hPageBegins.value = StartPage;
            Form1.hPageSize.value = PageCount;
            Form1.submit();
        }
        function selectprinttype(type) {
            if (type == "0") {
                document.getElementById('selectprinttypetxt').value = '当前选择:正常打印形式';
                document.getElementById('changeprinttype').value = '0';
            }
            else {
                document.getElementById('selectprinttypetxt').value = '当前选择: 套打形式';
                document.getElementById('changeprinttype').value = '1';             
             }
            Form1.submit();
        
        }
    </script>

    <%if (cssFile.Trim() == "")
      {%>
    <link href="../css/DefaultStyle/admin.css" type="text/css" rel="stylesheet" />
    <%}
      else
      {%>
    <link href="<%="../" + cssFile%>" type="text/css" rel="stylesheet" />
    <%;
      }%>
</head>
<body onload="return window_onload()">
<form id="Form1"  method="post" runat="server">
    <div id="divSubsMe">
        <%
            //需要显示的字段信息 序号,        送检单位名称  送检单位 姓名  采样日期    审定日期  打印次数   病历号 年龄 性别     文件
            string showfild = ",ReportFormID,ClientName,CNAME,CHECKDATE,SERIALNO,PDFFILE,PRINTTIMES,TPDFFILE";
            //需要显示出来隐藏的字段信息,首先得满足上面条件才有下面条件
            //string hiddenfild = ",ReportFormID,PRINTTIMES,PDFFILE";
            string hiddenfild = ",ReportFormID,PDFFILE,PRINTTIMES,TPDFFILE";


            string hTableFields = "";
            //为了上下通过键盘移动,取ID数据组
            string strAllIds = "";

            int iPages = 0;
            
            
            if (NodeTdTitleList != null && NodeTdTitleList.Count > 0)
            {

                XmlNode NodeSubMeParent = NodeTdTitleList[0].ParentNode.ParentNode;
                //XmlNodeList tablesSubsMe = NodeSubMeParent.ParentNode.SelectNodes("Table");
                XmlNode EachSub = NodeSubMeParent;
                //foreach(XmlNode EachSub in tablesSubsMe)
                {
                    string tableCName = EachSub.Attributes.GetNamedItem("TableCName").InnerXml;
                    string tableEName = EachSub.Attributes.GetNamedItem("EName").InnerXml;
                    //XmlNodeList tdSubsMe = EachSub.SelectNodes("tr/td[Batch/@DisplayOnQuery='Yes']");
                    XmlNodeList tdSubsMe = EachSub.SelectNodes("tr/td");
                    if (tdSubsMe.Count <= 0)
                        Response.Write("没有设置批量操作的字段，请在功能设置-按钮处设置");
                    int intSubMeRows = 10;

                    //string strSubMeRows=(EachSub.Attributes.GetNamedItem("SubsMeIuputLines")!=null)?EachSub.Attributes.GetNamedItem("SubsMeIuputLines").InnerXml:"10";
                    //intSubMeRows = hPageSize;// Int32.Parse(10);
                    if (intSubMeRows > iCount)
                        intSubMeRows = iCount;

                    iPages = iCount / hPageSize;
                    iPages++;
                    if ((int)iCount / hPageSize == (double)iCount / hPageSize)
                        iPages--;
                    //XmlNodeList NodeTrBodySubsMeList=null;
                    string SubMeDataLines = iCount.ToString();

                    //if(NodeTrBodyList!=null&&NodeTrBodyList.Count>0)
                    //{
                    //    NodeTrBodySubsMeList=NodeTrBodyList[0].SelectNodes("Table[@EName='"+tableEName+"']/tr");
                    //    if(NodeTrBodySubsMeList!=null)
                    //        SubMeDataLines=NodeTrBodySubsMeList.Count.ToString();
                    //}
                    
                    if (Int32.Parse(SubMeDataLines) > intSubMeRows)
                        intSubMeRows = Int32.Parse(SubMeDataLines);
						
								
        %>
        
        <table id="TableSubMeData" name="TableSubMeData" width="100%" cellspacing="0" cellpadding="1"
            border="0">
            <tr>
                <td nowrap align="center" width="20%" title="<%=tableEName%>" style="font-weight: bold">
                    <%=tableCName%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td nowrap align="left" width="20%" title="" style="font-weight: bold;font-size: 9pt">
                    <div style="float:left"><a href="#" onclick="selectprinttype(0);">正常打印形式</a>    <a href="#" onclick="selectprinttype(1);">套打形式</a>
                   </div>
                   <input type="text" runat="server" id="selectprinttypetxt" style="color:Red;width:120" value="当前选择:正常打印形式" />                   
                </td>
                <td nowrap align="left" width="20%" title="<%=tableEName%>" style="font-weight: bold;
                    font-size: 9pt">
                      <br />
                      <%hPageBegins=hPageBegins>iPages?iPages:hPageBegins;%>
                      <input type=text style="border:#666666 1px solid;" size=2 ONKEYPRESS="if(event.keyCode==13) {RunPage(this.value,<%=hPageSize%>);return;}event.returnValue=IsDigit();" id="txtiPageBegins" value="<%=hPageBegins%>" />/<%=iPages%>页(共<%=iCount%>条记录)
                      <img src="../image/bottom/Previous.jpg" border="0" 
							<%if(hPageBegins>1){%>
								style="Cursor:url('../image/cursors/H_POINT.CUR')"
								title="第<%=hPageBegins-1%>/<%=iPages%>页"
								onclick="RunPage('<%=hPageBegins-1%>',<%=hPageSize%>)"
							<%}
							else
							{%>
								style="Cursor:url('../image/cursors/H_NODROP.CUR')"
							<%}%>
						>
						<img src="../image/bottom/Next.jpg" border="0" 
							<%if(iPages>hPageBegins){%>
								style="Cursor:url('../image/cursors/H_POINT.CUR')"
								title="第<%=hPageBegins+1%>/<%=iPages%>页"
								onclick="RunPage('<%=hPageBegins+1%>',<%=hPageSize%>)"
							<%}
							else
							{%>
								style="Cursor:url('../image/cursors/H_NODROP.CUR')"
							<%}%>
						>
                </td>
                <td nowrap align="right" class="small" style="font-size: 9pt" width="60%">
                    <a href="#" disabled nochange="No" onclick="AddNewSubMe(this)"></a>&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" width="40%" colspan="2">
                    <table id="Table111" name="TableSubMeData_<%=tableEName%>" cellspacing="1" cellpadding="0"
                        border="0" width="100%">
                        <tr class="ListHeader" height="20" onmousemove="javascript:this.style.backgroundColor='#ddf3dd';this.style.cursor='hand';"
                            onmouseout="javascript:this.style.backgroundColor='white';" style="font-weight: bold">
                            <td>
                                <input type="checkbox" onclick="SelectAll(this);" />
                            </td>
                            <%foreach (XmlNode EachSubTD in tdSubsMe)
                              {
                                  string strColumnWidth = "15";// EachSubTD.SelectSingleNode("Batch/@DisplayLength").InnerXml;
                                  string strColumnCName = EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;//中文名
                                  string tmpcName = EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml;//字段名


                                  int iColumnWidth = 1;
                                  try
                                  {
                                      iColumnWidth = Int32.Parse(strColumnWidth);
                                  }
                                  catch
                                  {
                                      iColumnWidth = strColumnCName.Length;
                                  }

                                  if (!JudgeShowOrHidden(showfild, tmpcName))
                                  {
                                      continue;
                                  }
                                  string showflag = "block";
                                  if (JudgeShowOrHidden(hiddenfild, tmpcName))
                                  {
                                      showflag = "none";
                                  }
                            %>
                            <td align="left" nowrap style="display: <%=showflag%>" title="<%=tmpcName%>">
                                <%=strColumnCName%>
                            </td>
                            <%}%>
                        </tr>
                        <%for (int iRows = 0; iRows < NodeTrBodyList.Count; iRows++)
                          {                                 
                        %>
                        <tr onclick="ChangeBgColor(this);">
                            <td>
                                <input type="checkbox" id="chk" />
                            </td>
                            <%foreach (XmlNode EachSubTD in tdSubsMe)
                              {
                                  string strColumnWidth = "15";//EachSubTD.SelectSingleNode("Batch/@DisplayLength").InnerXml;
                                  string strColumnCName = EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;

                                  string strColumnEName = EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml;
                                  if (!JudgeShowOrHidden(showfild, strColumnEName))
                                  {
                                      continue;
                                  }
                                  int iColumnWidth = 1;
                                  try
                                  {
                                      iColumnWidth = Int32.Parse(strColumnWidth);
                                  }
                                  catch
                                  {
                                      iColumnWidth = strColumnCName.Length;
                                  }
                                  //iColumnWidth = iColumnWidth * 15+4;
                                  //iColumnWidth = 98;

                                  string strColumnType = EachSubTD.Attributes.GetNamedItem("ColumnType").InnerXml;
                                  string strColumnPrecision = EachSubTD.Attributes.GetNamedItem("ColumnPrecision").InnerXml;
                                  string strTableName = RetrieveTableName(EachSubTD.ParentNode.ParentNode);

                                  string ValidateValue = " ";



                                  //TableName.FieldName,TableName.FieldName;
                                  hTableFields = hTableFields + "," + EachSubTD.Attributes.GetNamedItem("ColumnCName").InnerXml;
                                  strAllIds = strAllIds + "," + EachSubTD.Attributes.GetNamedItem("ColumnEName").InnerXml;

                                  XmlNode NodeData = null;
                                  string strTdData = "";
                                  //Response.Write(NodeTrBodySubsMeList.Count);  iRows <= (NodeTrBodySubsMeList.Count - 1)
                                  if (NodeTrBodyList != null && NodeTrBodyList.Count > 0 && iRows <= (NodeTrBodyList.Count - 1))
                                  {
                                      NodeData = NodeTrBodyList[iRows].SelectSingleNode("td[@Column='" + strColumnEName + "']");
                                      if (NodeData != null)
                                          strTdData = NodeData.InnerXml;
                                  }



                                  string showflag = "block";
                                  if (JudgeShowOrHidden(hiddenfild, strColumnEName))
                                  {
                                      showflag = "none";
                                  }

                                  string changeprintname = "PDFFILE";
                                  if (changeprinttype.Value == "1")
                                  {
                                      changeprintname = "TPDFFILE";
                                  }
                            %>
                            <td nowrap style="display: <%=showflag%>; font-size: 9pt; cursor: hand" title="<%=strColumnCName%>"
                                align="left" onclick="showpdf('<%=NodeTrBodyList[iRows].SelectSingleNode("td[@Column='"+changeprintname+"']").InnerXml.Replace("\\","/")%>');">
                                <div title="<%=strColumnCName%>" id="<%=strColumnEName%>">
                                    <%=strTdData%></div>
                            </td>
                            <%}%>
                        </tr>
                        <%}%>
                    </table>
                    <div style="float: left;">
                        <input type="button" id="btnsave" value="打印" onclick="JudgeSelectRow(0);" />
                        <input type="hidden" id="btndao" value="套打" onclick="JudgeSelectRow(1);" />
                        &nbsp;&nbsp;
                        <input type="button" id="btnstop" value="停止" onclick="stopprint();" />
                    </div><div id="divprint" style="float: left; background-color: #ddf3dd; font-size: larger;">
                    </div>
                </td>
                <td width="57%" valign="top">
                    <%  
                        string tmpfile = "";
                        if (NodeTrBodySubsMeList != null && NodeTrBodySubsMeList.Count > 0)
                        {
                            string changeprintname = "PDFFILE";
                            if (changeprinttype.Value == "1")
                            {
                                changeprintname = "TPDFFILE";
                            }
                            tmpfile = NodeTrBodySubsMeList[0].SelectSingleNode("td[@Column='"+changeprintname+"']").InnerXml.Replace("\\", "/");
                        }
                    %>
                    <iframe id="frame1" style="width: 100%; height: 600px;" scrolling="auto" frameborder="1"
                        src="PrintPDF.aspx?reportfile=<%=tmpfile%>"></iframe>
                </td>
            </tr>
        </table>
        <%
            }
            }
            else
            {
                Response.Write("没有设置批量操作的字段，请在功能设置-按钮处设置");
            }		    
			
        %>
    </div>
    <input type="hidden" id="hAction" name="hAction" value="BModify" />
    <input type="hidden" id="hDataCollectionSubMes" name="hDataCollectionSubMes" value="" />
    <input type="hidden" id="hQueryCollection" name="hQueryCollection" value="" />
    <input type="hidden" id="txtBatches" name="txtBatches" value="" />
    <input type="hidden" id="hSubTablesCopy" name="hSubTablesCopy" value="" />
    <input type="hidden" id="hNotAllowNull" name="hNotAllowNull" value="" />
    <input type="hidden" id="hAllIds" name="hAllIds" value="<%=strAllIds%>" />
    <textarea id="hTxt" rows="5" cols="0" style="display: none"></textarea>
    <input type="hidden" id="hPageBegins" name="hPageBegins" value="<%=hPageBegins %>" />
	<input type="hidden" id="hPageSize" name="hPageSize" value="<%=hPageSize%>" />
	<!--打印形式 0 正常打印形式,1 套打形式-->
    <input type="hidden" id="changeprinttype" name="changeprinttype" runat="server" value="0" />
    <input type="hidden" id="h1" name="h1" runat="server" value="当前选择类型:正常打印形式" />
    </form>
</body>
</html>
