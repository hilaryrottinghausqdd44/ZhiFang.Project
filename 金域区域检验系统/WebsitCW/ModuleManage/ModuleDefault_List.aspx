<%@ Page Language="c#" CodeBehind="ModuleDefault_List.aspx.cs" AutoEventWireup="True"
    Inherits="OA.ModuleManage.ModuleDefault_List" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>选择预定义模块</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="style.css" rel="stylesheet">

    <script type="text/javascript">
		//子窗体对父窗体的赋值操作
        function setValue(tag,id,name)
        {
            //tag 
            if(id != null && name != null)
            {
                if(tag == 0)
                {                    
                    $('txtname').value = name;
                    $('txtvalue').value = id;
                    var pam1 = new Array;
                    pam1 = id.split("=");
                    $('txtrpamemater').value = pam1[1];
                    var rpam = $('txtreturnpam').value;
                    if(rpam == "" || rpam.length== 0)
                    {
                       $('txtreturnpam').value = pam1[0];
                    }
                    if(name == "个人文件夹" || name == "共享文件夹" || name == "公共文件夹")
                   {
                      document.getElementById("newid").innerText = "你选择的是:" + $('txttmpname').value;
                   }
                   else
                   {
                      document.getElementById("newid").innerText = "你选择的是:" + $('txttmpname').value + "  " + name;
                   }                          
                }
            }
        }        
       
        
        //打开子窗体       
        function showUserDialog(tag,strurl,listurl,listname,moreurl,mid,rpam,vvalue,moduleargprv)
        {  
           //模块信息
           $('txtmoduleargprv').value = moduleargprv;
           //输入参数
           if(vvalue != '' || vvalue.length > 0)
           {
             var reg=new RegExp("<br>","g");
             $('txttmpinputpam').value = vvalue.replace(reg,"\r\n");  
           }
           $('txtmid').value = mid;//模块编号 
           $('txtreturnpam').value = rpam;//返回参数   
           $('txtmoreurl').value = moreurl;//更新链接        
           $('txtlisturl').value = listurl;//先将所选择的连接地址赋给一个textbox
           $('txttmpname').value = listname;//模块名称
           $('txtname').value = "";//清空再次选择前的textbox
           $('txtvalue').value = "";  
           strurl = strurl + "?moreurl="+escape(moreurl); 
           //alert('tag=' + tag + ',strurl=' + strurl +',listurl=' + listurl+',listname=' + listname+',moreurl=' + moreurl);
            var r = window.showModalDialog(strurl,this,'dialogWidth:500px;dialogHeight:500px;');
            if (r == '' || typeof(r) == 'undefined' || typeof(r)=='object')
	        {
		        return;
	        }
	        else
	        {
	            //alert(r);
	            var id = '';
	            var name = '';
	            if(r.lastIndexOf(';')>0)
	            {
	               r = r.substring(0,r.length-1);
	            }
	            //先支持一条记录的输入
	            var tmp = r.indexOf(';');
	            if(tmp>0)
	            {
	                var pam1 = new Array;
                    pam1 = r.split(";");
                    for(var i=0;i<pam1.length;i++)
                    {
                        //判断是否有逗号存在
                        if(pam1[i].indexOf(',')>0)
                        {
                           var pam2 = new Array;
                           pam2 = pam1[i].split(",");
                           id = id + pam2[0] + ",";
                           name = name + pam2[1] + ",";
                        }
                        else
                        {
                           id = id + pam1[i] + ",";
                        }
                    }
	            }
	            else
	            {
	               //只有一条记录时的处理
	               var tmpr = r.lastIndexOf(',');
	               if(tmpr > 0)
	               {
	                  id = r.substring(0,tmpr);
	                  name = r.substring(tmpr+1,r.length);	                 
	               }
	               else
	               {
	                  id = r;
	               }
	            }
	            if(name.length <=0)
                {
                   name = "未知";
                }
                if(id.substring(id.length-1) == ',')
                {
                   id = id.substring(0,id.length-1);
                }
                if(name.substring(id.length-1) == ',')
                {
                   name = name.substring(0,name.length-1);
                }
	            //alert(r);            
	            $('txtrpamemater').value = id;
	            document.getElementById('divrpamemater').innerHTML = '<font color=red>'+name+'</font>';
	        }
        }    
       
        var result = "";
        //拼接输入参数
        function GetInputPam()
        {
           var MultiText = "";
           var paraValue = $('txttmpinputpam').value;
           if (paraValue.length > 0) 
            {
                var paras = paraValue.split("\r\n");
                for (var eachRow = 0; eachRow < paras.length; eachRow++) 
                {                  
                    try 
                    {   if(paras[eachRow].length > 0)
                        {
                          MultiText += "&" + paras[eachRow];  
                        }                   
                    }
                    catch(e)
                    {
                        continue;
                    }
                }
            }
            //alert(MultiText);
            SelectTxt(MultiText);
        }
        //选择已选项目返回
        function SelectTxt(tmppam)
        {
           //模块编号,模块名称
           var modulepam = $('txtmoduleargprv').value;           
           var url = $('txtlisturl').value;
           //链接更多
           var tmpmoreurl = $('txtmoreurl').value;
           //操作功能参数
           var tmppfpam = $('pfpam').value;
           //返回参数左边
           var pam1 = $('txtreturnpam').value;
           //返回参数右边
           var pam2 = $('txtrpamemater').value;
           
           
           var par = pam1 + '=' + pam2;
           if(pam2 == "" || pam2.length == 0)
           {
              alert('请选择模块参数条件!');
           }
           else
           {              
              result = url;
              if(par == "个人文件夹" || par == "共享文件夹")
              {
                 result = url;              
              }
              else if(par == "公共文件夹")
              {
                 result = url  + "?folder="+escape('\0');
              }
              else
              {
                 if(url.indexOf("?") > 0)
                 {
                   if(pam1.length > 0)
                   {
                     result = url + "&" + par;
                   }
                 }
                 else
                 {
                   if(pam1.length > 0)
                   {
                     result = url  + "?" + par;
                   }
                 }
              }
              //模块参数
              if(modulepam.length > 0)
              {
                if(result.indexOf("?") > 0)
                {
                   result = result + "&moduleargprv="+modulepam;
                }
                else
                {
                   result = result + "?moduleargprv="+modulepam;
                }
              }
              else
              {
                 alert('请选择模块');
                 return;
              }
              //链接更多
              if(tmpmoreurl.length > 0)
              {
                 result = result + "&moreurl=" + tmpmoreurl;
              }              
              //列表输入选择参数
              if(tmppam.length > 0)
              {
                  result = result + tmppam;
              }
                          
              //加上操作功能参数              
              if(document.getElementById("chkisp").checked)
              {
                  if(tmppfpam.length > 0)
                  {
                     var reg = new RegExp("&","g");
                     result = result + "&OutPutParaUrl=" + tmppfpam.replace(reg,"＃");
                  } 
              } 
              //alert(result);     
              $('TextAllUrl').value = result;          
              //将确定置为可用
              document.getElementById('btnSub').disabled = false;			 		   
           }          
        }
        function returnTxt()
        {
           if(result.length > 0)
           {
              window.returnValue = $('TextAllUrl').value;
			  window.close();	
		   }
		   else
		   {
			  alert('请选择相应参数');
		   }
        }
        function $(s)
        {
            return document.getElementById?document.getElementById(s):document.all[s];
        }
        function window_onload() 
        {
         /*
            var sPath = document.location.href;
            if(sPath.indexOf("?PreviouseUrl=")>0)
                sPath = sPath.substr(sPath.indexOf("?PreviouseUrl=")+14);
            
            sPath=unescape(sPath);
            //alert(sPath);
            $('TextAllUrl').value = sPath;
           
            var s="TextAllUrl";
            if(document.getElementById)
                document.getElementById(s).innerHTML=sPath;
            else
                document.all[s].innerHTML=sPath;               
            
            var sPathTemp="";
            if(sPath.indexOf("PopWinLS=")>0)
            {
                sPathTemp=sPath.substr(sPath.indexOf("PopWinLS=")+9);
                if(sPathTemp.indexOf("&")>0)
                    sPathTemp=sPathTemp.substr(0,sPathTemp.indexOf("&"));
                    
                document.getElementById("d_PositionSize").value=sPathTemp;
            }*/
        }

        
        
         //是否选择操作功能参数
        function ChkProcess()
        {
            var chk = document.getElementById("chkisp");
            if(chk.checked)
            {
                divp.style.display = "";                
            }
            else
            {
                divp.style.display = "none";
            }
        }
        //输入参数选择
        function inputpamselect()
		{
		   if($('inputpammid').value.length > 0)
		   {
		       var vValue = window.showModalDialog("ModuleUnionSelect.aspx", this, 'dialogWidth:800px;dialogHeight:550px;');
		       //alert(vValue);
		       if (vValue) 
		       {
		           $('inputpammid').value = vValue;
		       }
		   }
		   else
		   {
		       alert('输入参数不能为空');
		   }
	}

	function inputEditRedirect() {
	    var vValue = window.showModalDialog("ModuleRedirectSelect.aspx", this, 'dialogWidth:800px;dialogHeight:550px;');
	    //alert(vValue);
	    if (vValue) {
	        $('pfpam').value = vValue;
	    }
	}
	//选择模块分类
	function moduletypeselect()
	{
	    var moduleargprv = $('txtmoduleargprv').value;
	    var vValue = window.showModalDialog("ModulePamFunSelect.aspx?moduleargprv=" + moduleargprv, this, 'dialogWidth:800px;dialogHeight:550px;');
	    //alert(vValue);
	    if (vValue) 
	    {
	        var moduleid = '';//模块编号
	        if(vValue.indexOf(',') > 0)
	        {
	          moduleid = vValue.substring(0,vValue.indexOf(','));//模块编号
	          $('txtmoduletype').value = vValue.substring(vValue.indexOf(',')+1);
	        }	
	        else
	        {        
	          moduleid = vValue;//模块编号
	          $('txtmoduletype').value = vValue;
	        }
	        $('txtmoduleargprv').value = vValue;
	        //alert(moduleid);
	        //取模块编号得到所选模块对象信息
	        OA.ModuleManage.ModuleDefault_List.GetModuleObjectById(moduleid,GetCallresult);
	    }
	     
	}
	  //回调结果  将模块对象属性赋给页面控件
      function GetCallresult(result)
      {
          var r = result.value;
          if(r)
          {
               //输入参数
               var vvalue = r.Vvalue;
               if(vvalue != '' || vvalue.length > 0)
               {
                 var reg=new RegExp("<br>","g");
                 //输入参数
                 //$('txttmpinputpam').value = vvalue.replace(reg,"\r\n");  
                 $('inputpammid').value = vvalue.replace(reg,"\r\n"); //输入参数隐藏
                 SplitInputPam();//拆分输入参数
               }
               $('txtstrurl').value = r.PopUrl;
               $('txtmid').value = r.Id;//模块编号 
               $('txtreturnpam').value = r.Returnpam;//返回参数   
               $('txtmoreurl').value = r.MoreUrl;//更多链接        
               $('txtlisturl').value = r.Url;//先将所选择的连接地址赋给一个textbox
               $('txttmpname').value = r.Name;
               $('txtunionpam').value = r.Unionpam;//输出参数
               document.getElementById('divremark').innerHTML = r.Description;//模块说明
               //alert(r.Id + ',' +r.Name + ',' +r.Returnpam + ',' + r.Url + ','+ r.MoreUrl+ ','+ r.Vvalue);
          }
          else
          {
             alert('无');
          }
      }
      //将输入参数拆分成 style=1.css 形式, 并拆出对应说明
     function SplitInputPam()
     {
         //$('txttmpinputpam').value 拆分后的值
         //inputpamdiv1  拆分后的说明
         
           var MultiTextKey = "";//值
           var MultiTextValue = "";//说明
           var paraValue = $('inputpammid').value;
           var aTable = document.createElement("table"); 
           if (paraValue.length > 0) 
            {
                var paras = paraValue.split("\n");
                for (var eachRow = 0; eachRow < paras.length; eachRow++) 
                {
                    var parasCells = paras[eachRow].replace("\r","").split(" ");
                    if (parasCells[0] == "")
                        continue;
                    try 
                    {                        
                        //是否选中
                        parasCells[3] = parasCells[3].replace("\r", "");
                        var boolChecked = false;
                        if (parasCells[3] == 'true' || parasCells[3] == 'Yes' || parasCells[3] == 'yes'
                        || parasCells[3] == 'on' || parasCells[3] == 'On')
                        {
                            boolChecked = true;
                         }
                         MultiTextKey += parasCells[1].replace(' ','') + "=" + parasCells[2].replace(' ','')+"\r\n";
                         var aRow = aTable.insertRow();
                         
                         var aCell0 = aRow.insertCell();
                         aCell0.style.valign = "top"; 
                         aCell0.style.width = "80px";  
                         aCell0.style.paddingLeft = 5;   
                         aCell0.style.paddingTop = 0;   
                         aCell0.style.paddingRight = 5;   
                         aCell0.style.paddingBottom = 0;   
                         aCell0.innerHTML = parasCells[0].replace(' ','');
                         
                         var aCell1 = aRow.insertCell();
                         aCell1.style.valign = "top"; 
                         aCell1.style.width = "60px";  
                         aCell1.style.paddingLeft = 5;   
                         aCell1.style.paddingTop = 0;   
                         aCell1.style.paddingRight = 5;   
                         aCell1.style.paddingBottom = 0;   
                         aCell1.innerHTML = parasCells[1].replace(' ','');
                         var aCell2 = aRow.insertCell();  
                         aCell2.style.paddingLeft = 5;   
                         aCell2.style.paddingTop = 0;   
                         aCell2.style.paddingRight = 5;   
                         aCell2.style.paddingBottom = 0;                                                 
                         aCell2.innerHTML = parasCells[4].replace(/\\/g,'<br/>');
                    }
                    catch(e)
                    {
                        continue;
                    }
                }
            }
            aTable.border = 1;
            aTable.cellSpacing = 0;   
            aTable.cellPadding = 0; 
            aTable.width = "300px"; 
            aTable.height = "100%"; 
            aTable.align="left"
            $('txttmpinputpam').value = MultiTextKey;  
            document.getElementById('inputpamdiv1').appendChild(aTable);

     }
	//选择具体模块信息
	function modulereturnpamselect()
	{
	  if($('txtmoduletype').value.length > 0)
	  {
           var strurl = $('txtstrurl').value;
           if(strurl.length > 0)
           {          
               var moreurl = $('txtmoreurl').value;
               strurl = strurl + "?moreurl="+escape(moreurl); 
               if(strurl.indexOf('treeselect.aspx') > 0)
               {
                  strurl = strurl + "&treename="+$('txtrpamemater').value;
               }
               //alert('tag=' + tag + ',strurl=' + strurl +',listurl=' + listurl+',listname=' + listname+',moreurl=' + moreurl);
                var r = window.showModalDialog(strurl,this,'dialogWidth:500px;dialogHeight:500px;');
                if (r == '' || typeof(r) == 'undefined' || typeof(r)=='object')
                {
	                return;
                }
                else
                {
                    //alert(r);
                    var id = '';
                    var name = '';
                    if(r.lastIndexOf(';')>0)
                    {
                       r = r.substring(0,r.length-1);
                    }
                    //先支持一条记录的输入
                    var tmp = r.indexOf(';');
                    if(tmp>0)
                    {
                        var pam1 = new Array;
                        pam1 = r.split(";");
                        for(var i=0;i<pam1.length;i++)
                        {
                            //判断是否有逗号存在
                            if(pam1[i].indexOf(',')>0)
                            {
                               var pam2 = new Array;
                               pam2 = pam1[i].split(",");
                               id = id + pam2[0] + ",";
                               name = name + pam2[1] + ",";
                            }
                            else
                            {
                               id = id + pam1[i] + ",";
                            }
                        }
                    }
                    else
                    {
                       //只有一条记录时的处理
                       var tmpr = r.lastIndexOf(',');
                       if(tmpr > 0)
                       {
                          id = r.substring(0,tmpr);
                          name = r.substring(tmpr+1,r.length);	                 
                       }
                       else
                       {
                          id = r;
                       }
                    }
                    if(name.length <=0)
                    {
                       name = "未知";
                    }
                    if(id.substring(id.length-1) == ',')
                    {
                       id = id.substring(0,id.length-1);
                    }
                    if(name.substring(id.length-1) == ',')
                    {
                       name = name.substring(0,name.length-1);
                    }
                    //alert(r);            
                    $('txtrpamemater').value = id;
                    document.getElementById('divrpamemater').innerHTML = '<font color=red>'+name+'</font>';
                }
             }
           else
           {
              alert('当前选择模块分类没有弹出页');
           }
        }
        else
        {
           alert('请先选择模块分类');
        }
	}
        
    </script>

    <style type="text/css">
        .hideClass
        {
            display: none;
        }
        #TextAllUrl
        {
            height: 64px;
            width: 52%;
        }
    </style>
    <base target="_self" />
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <asp:TextBox ID="txtmoduleargprv" CssClass="hideClass" runat="server" ToolTip="模块编号,模块名称"></asp:TextBox>
    <asp:TextBox ID="txtmid" CssClass="hideClass" runat="server" ToolTip="模块编号"></asp:TextBox>
    <asp:TextBox ID="txtvalue" CssClass="hideClass" runat="server" ToolTip="返回来的编号改后无用"></asp:TextBox>
    <asp:TextBox ID="txtname" CssClass="hideClass" runat="server" ToolTip="返回来的名称改后无用"></asp:TextBox>
    <asp:TextBox ID="txtstrurl" CssClass="hideClass" runat="server" ToolTip="要打开弹出的地址"></asp:TextBox>
    <asp:TextBox ID="txtmoreurl" CssClass="hideClass" runat="server" ToolTip="链接更多的地址"></asp:TextBox>
    <asp:TextBox ID="txtlisturl" CssClass="hideClass" runat="server" ToolTip="所选择的连接地址"></asp:TextBox>
    <asp:TextBox ID="txttmpname" CssClass="hideClass" runat="server" ToolTip="模块名称"></asp:TextBox>
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
            </td>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#fcfcfc" style="border: #aecdd5 solid 1px;
                    border-top: #aecdd5 solid 8px; border-bottom: #aecdd5 solid 8px;" border="0">
                    <tbody>
                        <tr>
                            <td align="left" width="100%" style="border-bottom: #aecdd5 solid 1px;">
                                选择模块分类<asp:TextBox runat="server" contentEditable="false" ID="txtmoduletype"></asp:TextBox>
                                <input type="button" id="btnmoduletypeselect" value="选择" onclick="moduletypeselect();" />
                            </td>
                        </tr>
                        <tr>
                            <td id="newid" style="color: #ff3300">
                            </td>
                        </tr>
                        <tr>
                            <td id="Td1" style="color: #ff3300">
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #aecdd5 solid 1px;">
                                接收参数:<asp:TextBox runat="server" contentEditable="false" Width="50px" ID="txtreturnpam"></asp:TextBox>
                                =
                                <asp:TextBox runat="server" ID="txtrpamemater" Width="30%"></asp:TextBox>
                                <input type="button" id="Button1" value="选择" onclick="modulereturnpamselect();" />&nbsp;
                                <div id="divrpamemater" style="width: auto">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                <div id="inputpamdiv" style="display: block; float: left">
                                    输入参数<br />
                                    <asp:TextBox runat="server" CssClass="hideClass" ID="inputpammid" TextMode="MultiLine"></asp:TextBox>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txttmpinputpam" Width="250px"
                                        Height="90px"></asp:TextBox></div>
                                <div id="inputpamdiv1">
                                    输入参数说明</div>
                                <input type="hidden" id="btnselectpam1" value="编辑" onclick="inputpamselect();" />
                            </td>
                        </tr>
                        <tr>
                            <td id="Td2" style="border-bottom: #aecdd5 solid 1px;">
                                <input type="checkbox" onclick="ChkProcess();" runat="server" id="chkisp">是否刷新其它界面&nbsp;<a
                                    href="ModuleUrlHelp.aspx" target="_blank">配置说明</a>
                                <div id="divp" style="display: none">
                                    <asp:TextBox runat="server" Width="500px" ID="pfpam" Height="79px" TextMode="MultiLine"></asp:TextBox>
                                    <br />
                                    输出参数<br />
                                    <asp:TextBox runat="server" ID="txtunionpam" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                    <input type="hidden" id="ButtonEditRedirect" value="编辑" onclick="inputEditRedirect();" />
                                </div>
                                <input id="d_PositionSize" type="hidden" size="29" value="中,中,80%,80%" name="d_PositionSize" />
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #aecdd5 solid 1px;">
                                <input type="button" id="btntest" class="buttonstyle" value="生成代码" onclick="GetInputPam();" />&nbsp;<input
                                    type="button" disabled id="btnSub" class="buttonstyle" onclick="returnTxt();"
                                    value="确 定" />&nbsp;&nbsp;<input type="button" id="btncancel" class="buttonstyle"
                                        value="关 闭" onclick="window.close();">
                            </td>
                        </tr>
                        <tr>
                            <td id="Td4" style="border-bottom: #aecdd5 solid 1px;">
                                最后返回的链接参数：<br />
                                <asp:TextBox runat="server" BackColor="LightGray" Width="550px" ID="TextAllUrl" TextMode="MultiLine"
                                    Height="87px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td3" style="border-bottom: #aecdd5 solid 1px;">
                                模块说明:
                                <div style="color: #ff3300" id="divremark">
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
