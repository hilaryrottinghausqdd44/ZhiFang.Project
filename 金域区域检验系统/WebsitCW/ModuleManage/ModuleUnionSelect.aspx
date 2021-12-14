<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleUnionSelect.aspx.cs" Inherits="OA.ModuleManage.ModuleUnionSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>输入参数选择</title>
    <script type="text/javascript">
    function window_onload() 
    {
            var paraValue = dialogArguments.document.all['inputpammid'].innerHTML;
            //alert(paraValue);
            if (paraValue.length > 0) 
            {
                var paras = paraValue.split("\n");
                for (var eachRow = 0; eachRow < paras.length; eachRow++) 
                {
                    var objDataTable = document.all['Table1'];
                    //var newTR = objDataTable.insertRow();
                    var NewTr = objDataTable.lastChild.cloneNode(true);
                    if(eachRow>0)
                        objDataTable.appendChild(NewTr);

                    var objRow = document.all['Table1'].rows[eachRow + 1];
                    
                    //var objCells=objRow.childNodes;

                    var parasCells = paras[eachRow].replace("\r","").split(" ");
                    if (parasCells[0] == "")
                        continue;
                    try {
                        //名称
                        objRow.cells[1].firstChild.innerHTML = parasCells[0];
                        //名称
                        objRow.cells[2].firstChild.innerHTML = parasCells[1];
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
                        objRow.cells[4].firstChild.innerHTML = parasCells[4].replace(/\\/g,'\n');  
                    }
                    catch(e)
                    {
                        continue;
                    }
                }
            }
            else
            {
               alert('输入参数为空,请选择相应模块');
               window.close();
            }
     }
     //得到输入参数的信息
    function Getinputpam() 
    {
            var MultiText = "";
            var allDataRows = document.all['Table1'].rows;
            for (var eachRow = 1; eachRow < allDataRows.length; eachRow++) 
            {
                var objRow = allDataRows[eachRow];

                if (objRow.cells[1].firstChild.innerHTML == "" || objRow.cells[2].firstChild.innerHTML == "")
                    continue;
                  
               MultiText +="\n" + objRow.cells[1].firstChild.innerHTML.replace(' ','') + " ";
               MultiText += objRow.cells[2].firstChild.innerHTML.replace(' ','') + " ";
               MultiText += objRow.cells[3].firstChild.value.replace(' ','') + " ";
               MultiText += objRow.cells[0].firstChild.checked + " ";
               var ParaDesc=objRow.cells[4].firstChild.innerHTML;
               //alert(ParaDesc);
               ParaDesc = ParaDesc.replace(/ /g, '').replace(/\r\n/g, '\\');

               if (ParaDesc.length > 0) {
                   while (ParaDesc.lastIndexOf('\\') == ParaDesc.length - 1) 
                   {
                       //debugger;                       
                       ParaDesc = ParaDesc.substr(0, ParaDesc.length - 1);
                   }
               }
               MultiText += ParaDesc;
            }
            if (MultiText.length > 0)
                MultiText = MultiText.substr(1);

            window.returnValue = MultiText;
            window.close();         
    }
    </script>
    <base target="_self" />
</head>
<body onload="return window_onload()">
    <form id="form1" runat="server">
    <div>
    <table cellspacing="1" cellpadding="0" id="Table1" style="background-color: Purple;">
                             <tr style="color: Black; background-color: #990000; font-size: Smaller; font-weight: bold;">
                                    <td align="center" style="background-color: Silver;">
                                        <!--input id="CheckAll" type="checkbox" /-->
                                        参数重要性
                                    </td>
                                    <td align="center" style="background-color: Silver; width: 20%;">
                                        名称
                                    </td>
                                    <td align="center" style="background-color: Silver; width: 10%;">
                                        缩写
                                    </td>
                                    <td align="center" style="background-color: Silver; width: 20%;">
                                        默认
                                    </td>
                                    <td align="center" style="background-color: Silver; width: 30%;">
                                        描述
                                    </td>
                                </tr>
                                <tbody>                                       
                                    <tr onmouseover="javascript:this.style.backgroundColor='#ddf3dd';this.style.cursor='hand';"
                                        onmouseout="javascript:this.style.backgroundColor='white';" style="color: #330099;
                                        background-color: White;">
                                        <td align="center">
                                            <input id="CheckBox1" type="checkbox" name="CheckBox1" checked="checked" />
                                        </td>
                                        <td align="center">
                                             <div id="inputdiv1"></div>
                                        </td>
                                        <td align="center">
                                            <div id="inputdiv2"></div>
                                        </td>
                                        <td align="left">
                                            <input name="Text3" type="text" value="" id="Text3" />
                                        </td>
                                        <td align="left">
                                            <div id="inputdiv3"></div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <input type="button" id="btnsub" value="确定" onclick="Getinputpam();" />
                            &nbsp;<input type="button" id="btnclose" value="关闭" onclick="window.close();" />
    </div>
    </form>
</body>
</html>
