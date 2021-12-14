<%@ Page Language="c#" CodeBehind="ModuleDefault_List.aspx.cs" AutoEventWireup="True"
    Inherits="OA.ModuleManage.ModuleDefault_List" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>ѡ��Ԥ����ģ��</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="style.css" rel="stylesheet">

    <script type="text/javascript">
		//�Ӵ���Ը�����ĸ�ֵ����
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
                    if(name == "�����ļ���" || name == "�����ļ���" || name == "�����ļ���")
                   {
                      document.getElementById("newid").innerText = "��ѡ�����:" + $('txttmpname').value;
                   }
                   else
                   {
                      document.getElementById("newid").innerText = "��ѡ�����:" + $('txttmpname').value + "  " + name;
                   }                          
                }
            }
        }        
       
        
        //���Ӵ���       
        function showUserDialog(tag,strurl,listurl,listname,moreurl,mid,rpam,vvalue,moduleargprv)
        {  
           //ģ����Ϣ
           $('txtmoduleargprv').value = moduleargprv;
           //�������
           if(vvalue != '' || vvalue.length > 0)
           {
             var reg=new RegExp("<br>","g");
             $('txttmpinputpam').value = vvalue.replace(reg,"\r\n");  
           }
           $('txtmid').value = mid;//ģ���� 
           $('txtreturnpam').value = rpam;//���ز���   
           $('txtmoreurl').value = moreurl;//��������        
           $('txtlisturl').value = listurl;//�Ƚ���ѡ������ӵ�ַ����һ��textbox
           $('txttmpname').value = listname;//ģ������
           $('txtname').value = "";//����ٴ�ѡ��ǰ��textbox
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
	            //��֧��һ����¼������
	            var tmp = r.indexOf(';');
	            if(tmp>0)
	            {
	                var pam1 = new Array;
                    pam1 = r.split(";");
                    for(var i=0;i<pam1.length;i++)
                    {
                        //�ж��Ƿ��ж��Ŵ���
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
	               //ֻ��һ����¼ʱ�Ĵ���
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
                   name = "δ֪";
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
        //ƴ���������
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
        //ѡ����ѡ��Ŀ����
        function SelectTxt(tmppam)
        {
           //ģ����,ģ������
           var modulepam = $('txtmoduleargprv').value;           
           var url = $('txtlisturl').value;
           //���Ӹ���
           var tmpmoreurl = $('txtmoreurl').value;
           //�������ܲ���
           var tmppfpam = $('pfpam').value;
           //���ز������
           var pam1 = $('txtreturnpam').value;
           //���ز����ұ�
           var pam2 = $('txtrpamemater').value;
           
           
           var par = pam1 + '=' + pam2;
           if(pam2 == "" || pam2.length == 0)
           {
              alert('��ѡ��ģ���������!');
           }
           else
           {              
              result = url;
              if(par == "�����ļ���" || par == "�����ļ���")
              {
                 result = url;              
              }
              else if(par == "�����ļ���")
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
              //ģ�����
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
                 alert('��ѡ��ģ��');
                 return;
              }
              //���Ӹ���
              if(tmpmoreurl.length > 0)
              {
                 result = result + "&moreurl=" + tmpmoreurl;
              }              
              //�б�����ѡ�����
              if(tmppam.length > 0)
              {
                  result = result + tmppam;
              }
                          
              //���ϲ������ܲ���              
              if(document.getElementById("chkisp").checked)
              {
                  if(tmppfpam.length > 0)
                  {
                     var reg = new RegExp("&","g");
                     result = result + "&OutPutParaUrl=" + tmppfpam.replace(reg,"��");
                  } 
              } 
              //alert(result);     
              $('TextAllUrl').value = result;          
              //��ȷ����Ϊ����
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
			  alert('��ѡ����Ӧ����');
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

        
        
         //�Ƿ�ѡ��������ܲ���
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
        //�������ѡ��
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
		       alert('�����������Ϊ��');
		   }
	}

	function inputEditRedirect() {
	    var vValue = window.showModalDialog("ModuleRedirectSelect.aspx", this, 'dialogWidth:800px;dialogHeight:550px;');
	    //alert(vValue);
	    if (vValue) {
	        $('pfpam').value = vValue;
	    }
	}
	//ѡ��ģ�����
	function moduletypeselect()
	{
	    var moduleargprv = $('txtmoduleargprv').value;
	    var vValue = window.showModalDialog("ModulePamFunSelect.aspx?moduleargprv=" + moduleargprv, this, 'dialogWidth:800px;dialogHeight:550px;');
	    //alert(vValue);
	    if (vValue) 
	    {
	        var moduleid = '';//ģ����
	        if(vValue.indexOf(',') > 0)
	        {
	          moduleid = vValue.substring(0,vValue.indexOf(','));//ģ����
	          $('txtmoduletype').value = vValue.substring(vValue.indexOf(',')+1);
	        }	
	        else
	        {        
	          moduleid = vValue;//ģ����
	          $('txtmoduletype').value = vValue;
	        }
	        $('txtmoduleargprv').value = vValue;
	        //alert(moduleid);
	        //ȡģ���ŵõ���ѡģ�������Ϣ
	        OA.ModuleManage.ModuleDefault_List.GetModuleObjectById(moduleid,GetCallresult);
	    }
	     
	}
	  //�ص����  ��ģ��������Ը���ҳ��ؼ�
      function GetCallresult(result)
      {
          var r = result.value;
          if(r)
          {
               //�������
               var vvalue = r.Vvalue;
               if(vvalue != '' || vvalue.length > 0)
               {
                 var reg=new RegExp("<br>","g");
                 //�������
                 //$('txttmpinputpam').value = vvalue.replace(reg,"\r\n");  
                 $('inputpammid').value = vvalue.replace(reg,"\r\n"); //�����������
                 SplitInputPam();//����������
               }
               $('txtstrurl').value = r.PopUrl;
               $('txtmid').value = r.Id;//ģ���� 
               $('txtreturnpam').value = r.Returnpam;//���ز���   
               $('txtmoreurl').value = r.MoreUrl;//��������        
               $('txtlisturl').value = r.Url;//�Ƚ���ѡ������ӵ�ַ����һ��textbox
               $('txttmpname').value = r.Name;
               $('txtunionpam').value = r.Unionpam;//�������
               document.getElementById('divremark').innerHTML = r.Description;//ģ��˵��
               //alert(r.Id + ',' +r.Name + ',' +r.Returnpam + ',' + r.Url + ','+ r.MoreUrl+ ','+ r.Vvalue);
          }
          else
          {
             alert('��');
          }
      }
      //�����������ֳ� style=1.css ��ʽ, �������Ӧ˵��
     function SplitInputPam()
     {
         //$('txttmpinputpam').value ��ֺ��ֵ
         //inputpamdiv1  ��ֺ��˵��
         
           var MultiTextKey = "";//ֵ
           var MultiTextValue = "";//˵��
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
                        //�Ƿ�ѡ��
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
	//ѡ�����ģ����Ϣ
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
                    //��֧��һ����¼������
                    var tmp = r.indexOf(';');
                    if(tmp>0)
                    {
                        var pam1 = new Array;
                        pam1 = r.split(";");
                        for(var i=0;i<pam1.length;i++)
                        {
                            //�ж��Ƿ��ж��Ŵ���
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
                       //ֻ��һ����¼ʱ�Ĵ���
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
                       name = "δ֪";
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
              alert('��ǰѡ��ģ�����û�е���ҳ');
           }
        }
        else
        {
           alert('����ѡ��ģ�����');
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
    <asp:TextBox ID="txtmoduleargprv" CssClass="hideClass" runat="server" ToolTip="ģ����,ģ������"></asp:TextBox>
    <asp:TextBox ID="txtmid" CssClass="hideClass" runat="server" ToolTip="ģ����"></asp:TextBox>
    <asp:TextBox ID="txtvalue" CssClass="hideClass" runat="server" ToolTip="�������ı�Ÿĺ�����"></asp:TextBox>
    <asp:TextBox ID="txtname" CssClass="hideClass" runat="server" ToolTip="�����������Ƹĺ�����"></asp:TextBox>
    <asp:TextBox ID="txtstrurl" CssClass="hideClass" runat="server" ToolTip="Ҫ�򿪵����ĵ�ַ"></asp:TextBox>
    <asp:TextBox ID="txtmoreurl" CssClass="hideClass" runat="server" ToolTip="���Ӹ���ĵ�ַ"></asp:TextBox>
    <asp:TextBox ID="txtlisturl" CssClass="hideClass" runat="server" ToolTip="��ѡ������ӵ�ַ"></asp:TextBox>
    <asp:TextBox ID="txttmpname" CssClass="hideClass" runat="server" ToolTip="ģ������"></asp:TextBox>
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
                                ѡ��ģ�����<asp:TextBox runat="server" contentEditable="false" ID="txtmoduletype"></asp:TextBox>
                                <input type="button" id="btnmoduletypeselect" value="ѡ��" onclick="moduletypeselect();" />
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
                                ���ղ���:<asp:TextBox runat="server" contentEditable="false" Width="50px" ID="txtreturnpam"></asp:TextBox>
                                =
                                <asp:TextBox runat="server" ID="txtrpamemater" Width="30%"></asp:TextBox>
                                <input type="button" id="Button1" value="ѡ��" onclick="modulereturnpamselect();" />&nbsp;
                                <div id="divrpamemater" style="width: auto">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="border-bottom: #aecdd5 solid 1px;">
                                <div id="inputpamdiv" style="display: block; float: left">
                                    �������<br />
                                    <asp:TextBox runat="server" CssClass="hideClass" ID="inputpammid" TextMode="MultiLine"></asp:TextBox>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txttmpinputpam" Width="250px"
                                        Height="90px"></asp:TextBox></div>
                                <div id="inputpamdiv1">
                                    �������˵��</div>
                                <input type="hidden" id="btnselectpam1" value="�༭" onclick="inputpamselect();" />
                            </td>
                        </tr>
                        <tr>
                            <td id="Td2" style="border-bottom: #aecdd5 solid 1px;">
                                <input type="checkbox" onclick="ChkProcess();" runat="server" id="chkisp">�Ƿ�ˢ����������&nbsp;<a
                                    href="ModuleUrlHelp.aspx" target="_blank">����˵��</a>
                                <div id="divp" style="display: none">
                                    <asp:TextBox runat="server" Width="500px" ID="pfpam" Height="79px" TextMode="MultiLine"></asp:TextBox>
                                    <br />
                                    �������<br />
                                    <asp:TextBox runat="server" ID="txtunionpam" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                    <input type="hidden" id="ButtonEditRedirect" value="�༭" onclick="inputEditRedirect();" />
                                </div>
                                <input id="d_PositionSize" type="hidden" size="29" value="��,��,80%,80%" name="d_PositionSize" />
                            </td>
                        </tr>
                        <tr>
                            <td style="border-bottom: #aecdd5 solid 1px;">
                                <input type="button" id="btntest" class="buttonstyle" value="���ɴ���" onclick="GetInputPam();" />&nbsp;<input
                                    type="button" disabled id="btnSub" class="buttonstyle" onclick="returnTxt();"
                                    value="ȷ ��" />&nbsp;&nbsp;<input type="button" id="btncancel" class="buttonstyle"
                                        value="�� ��" onclick="window.close();">
                            </td>
                        </tr>
                        <tr>
                            <td id="Td4" style="border-bottom: #aecdd5 solid 1px;">
                                ��󷵻ص����Ӳ�����<br />
                                <asp:TextBox runat="server" BackColor="LightGray" Width="550px" ID="TextAllUrl" TextMode="MultiLine"
                                    Height="87px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td3" style="border-bottom: #aecdd5 solid 1px;">
                                ģ��˵��:
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
