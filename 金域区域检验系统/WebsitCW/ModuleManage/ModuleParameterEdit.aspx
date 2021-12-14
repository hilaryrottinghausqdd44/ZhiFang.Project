<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleParameterEdit.aspx.cs" Inherits="OA.ModuleManage.ModuleParameterEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>输入参数可视化编辑界面</title>
       <link href="style.css" rel="stylesheet" />
   
    <script language="javascript" type="text/javascript">
// <!CDATA[

        function btnSave_Click() {
            var MultiText = "";
            var allDataRows = document.all['myDataGrid'].rows;
            for (var eachRow = 1; eachRow < allDataRows.length; eachRow++) {

                var objRow = allDataRows[eachRow];

//                try {
                if (objRow.cells[1].firstChild.value == "" || objRow.cells[2].firstChild.value == "")
                    continue;
                  
               MultiText +="\n" + objRow.cells[1].firstChild.value.replace(' ','') + " ";
               MultiText += objRow.cells[2].firstChild.value.replace(' ','') + " ";
               MultiText += objRow.cells[3].firstChild.value.replace(' ','') + " ";
               MultiText += objRow.cells[0].firstChild.checked + " ";
               var ParaDesc=objRow.cells[4].firstChild.value;
               //alert(ParaDesc);
               ParaDesc = ParaDesc.replace(/ /g, '').replace(/\r\n/g, '\\');

               if (ParaDesc.length > 0) {
                   while (ParaDesc.lastIndexOf('\\') == ParaDesc.length - 1) {
                       //debugger;
                       
                       ParaDesc = ParaDesc.substr(0, ParaDesc.length - 1);
                   }
               }
               MultiText += ParaDesc;

                   //alert(ParaDesc);
//                }
//                catch (e) {
//                    continue;
//                }
            }
            if (MultiText.length > 0)
                MultiText = MultiText.substr(1);

            //alert(MultiText + '\n=\n' +  dialogArguments.document.all['txtvalue'].innerHTML);
            this.document.all['TextPara'].value = MultiText;
            //dialogArguments.document.all['txtvalue'].value = MultiText;
            window.returnValue = MultiText;
            window.close();
            
        }
        function window_onload() {
            var paraValue = dialogArguments.document.all['txtvalue'].innerHTML;
            //alert(paraValue);
            this.document.all['TextPara'].value = paraValue;
            if (paraValue.length > 0) {
                var paras = paraValue.split("\n");
                for (var eachRow = 0; eachRow < paras.length; eachRow++) {

                    var objDataTable = document.all['myDataGrid'];
                    //var newTR = objDataTable.insertRow();
                    var NewTr = objDataTable.lastChild.cloneNode(true);
                    
                    
                    
                    if(eachRow>0)
                        objDataTable.appendChild(NewTr);

                    var objRow = document.all['myDataGrid'].rows[eachRow + 1];
                    
                    //var objCells=objRow.childNodes;

                    var parasCells = paras[eachRow].replace("\r","").split(" ");
                    if (parasCells[0] == "")
                        continue;
                    try {
                        //名称
                        objRow.cells[1].firstChild.value = parasCells[0];
                        //名称
                        objRow.cells[2].firstChild.value = parasCells[1];
                        //名称
                        objRow.cells[3].firstChild.value = parasCells[2];

                        //复选
                        parasCells[3] = parasCells[3].replace("\r", "");
                        var boolChecked = false;
                        if (parasCells[3] == 'true' || parasCells[3] == 'Yes' || parasCells[3] == 'yes'
                        || parasCells[3] == 'on' || parasCells[3] == 'On')
                            boolChecked = true;

                        //alert(unescape(parasCells[3]) == 'On');
                        //debugger;
                        objRow.cells[0].firstChild.checked = boolChecked;
                        
                        
                        //名称
                        objRow.cells[4].firstChild.value = parasCells[4].replace(/\\/g,'\n');
                        
//                        for (var eachCell = 0; eachCell < parasCells.length; eachCell++) {
//                            objRow.cells[eachCell].firstChild.value = parasCells[eachCell]
//                        }
                    }
                    catch(e)
                    {
                        continue;
                    }

                    
                   
                }
            }
        }

// ]]>
    </script>
</head>
<body onload="return window_onload()">
    <form id="form1" runat="server">
    <div>
        <table bordercolor="#003366" cellspacing="0" bordercolordark="#ffffff" cellpadding="0"
            width="100%" align="center" bgcolor="#fcfcfc" bordercolorlight="#aecdd5" border="1">
            <tbody>
                 <tr>
                    <td align="center">
                        <input ID="btnSave" CssClass="buttonstyle" type="button" value="确  定" 
                            onclick="btnSave_Click();"></input>
                    </td>
                     <td align="right">
                        <input ID="btnadd" CssClass="buttonstyle" type="button" value="添 加 新 参 数" 
                             onclick="btnadd_Click()" />
                        
                     </td>
                 </tr>
                 
                <tr>
                    <td colspan="2">
                        <table cellspacing="1" cellpadding="0"  id="myDataGrid" style="background-color:Purple;">
                            <tr style="color:Black;background-color:#990000;font-size:Smaller;font-weight:bold;">
                                <td align="center" style="background-color:Silver;">
							        <!--input id="CheckAll" type="checkbox" /-->参数重要性
							    </td>
							    <td align="center" style="background-color:Silver;width:20%;">名称</td>
							    <td align="center" style="background-color:Silver;width:10%;">缩写</td>
							    <td align="center" style="background-color:Silver;width:20%;">默认</td>
							    <td align="center" style="background-color:Silver;width:30%;">描述</td>
							    <td align="center" style="background-color:Silver;width:10%;">操作</td>
                            </tr>
                            <tbody><tr onmouseover="javascript:this.style.backgroundColor='#ddf3dd';this.style.cursor='hand';" 
                                onmouseout="javascript:this.style.backgroundColor='white';" style="color:#330099;background-color:White;">
                                <td align="center">
								     <input id="CheckBox1" type="checkbox" name="CheckBox1" checked="checked" />															
							    </td>
							    <td align="center">
                                    <input name="Text1" type="text" value="" id="Text1"  size="8"  />
                                </td>
                                <td align="center">
                                    <input name="Text2" type="text" value="" id="Text2" size="8" />
                                </td>
                                <td align="left">                                                       
                                    <input name="Text3" type="text" value="" id="Text3" />
                                </td>
                                <td align="left">
                                    <textarea name="Text4" cols="25" rows="3" id="Text4"></textarea>
                                </td>
                                <td align="center" style="width:100px;">
                                    <a onclick="return confirm('您真的要删除名为：[ 字数 ] 的数据行吗？');" 
                                    id="btnDelete" href="javascript:doPostBackDelete(this)">删除</a>
                                    
                                </td>
                            </tr></tbody>
                        </table>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2" style="height:30px"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <textarea id="TextPara" name="TextPara" cols="70" rows="6" 
                            style="background-color: #EBECED"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    
    </div>
    </form>
</body>
</html>
