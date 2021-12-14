<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipRecordDaily.aspx.cs" Inherits="OA.QMSEquips.EquipRecordDaily" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>������ά��������¼</title>
    <style type="text/css">
        table
        {
        	border:1px;
        }
        .STYLE1 {color: #FFFFFF}
        .STYLE5 {
	        color: #e14d4d;
	        font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">
    function getOperatorChange(obj)
    {
        var objHiddenOperator = document.getElementById("hiddenOperatorChange");
        var addvalue = (obj.value+"|"+obj.id)+"@";
        objHiddenOperator.value += addvalue;
    }
    
    //��ȫ������������д�����ؿؼ�����ʽ������1|����2...
    function getOperatorName()
    {
        var maxDay = 31;
        var operatorNameID = "txt_";
        var objHiddenOperator = document.getElementById("hiddenOperatorName");
        objHiddenOperator.value = "";//���ԭ��ֵ
        for(var i=1;i<=maxDay;i++)
        {
            var txtboxOperatorName = document.getElementById(operatorNameID+i);
            if(txtboxOperatorName != null)
            {
                objHiddenOperator.value += (txtboxOperatorName.value+"|");
            }
        }
    }
    
    
    //�������޸Ĺ���ά����¼д�����ؿؼ� hiddenAllRecordInfo
    function selectRecord(obj)
    {
        var recordInfo = document.getElementById("hiddenAllRecordInfo");//ȡ�����ؿؼ����洢ҳ�����޸Ĺ��ļ�¼
        //���ؿؼ��洢���ݵĸ�ʽ����ѡ��ID|ѡ��״̬|value@��ѡ��ID|ѡ��״̬|value@������
        var cellID = obj.id;//��ǰ�����ļ�¼����¼��Ÿ�ʽ��1_4��ʾ��1����ά����Ŀ�����ݿ��д洢��ID,4����ά��������
        var cellChecked = obj.checked;//��¼��ѡ��״̬
        var cellValue = obj.value;//ά������
        
        //��ϸ��Ϣ��ѡ��
        var curDayCheck = document.getElementById("check_"+cellID.split('_')[0]);
        if(curDayCheck && !selectTwiceTime)
        {
            curDayCheck.checked = cellChecked;
        }

        var cellHaveChecked = false; //��־����true��ʾ��ǰ��¼�޸Ĺ�
        
        var allRecords = recordInfo.value.toString();
        if(allRecords.length>0)
        {
            allRecords = allRecords.substring(0,allRecords.length-1);
        }
        var recordInfoDetails = allRecords.split('@');
        
        //�鿴��ǰ��¼�Ƿ��޸Ĺ�
        for(var i=0;i<recordInfoDetails.length;i++)
        {
            var dateRecord = recordInfoDetails[i].split('|');//����һ�ܵ�ά����¼(ID,checked,value)
            
            var curRecordID = dateRecord[0];//ά����ĿID
            if(curRecordID == cellID)
            {
                if(!selectTwiceTime)//�鿴��ǰ��¼�Ƿ��޸Ĺ����ڶ����޸ģ�
                {
                    recordInfoDetails[i] = "";   //���ά������ɾ����¼
                    cellHaveChecked = true;
                }
                else
                {
                    recordInfoDetails[i] = obj.id+"|"+cellChecked+"|"+cellValue;
                    selectTwiceTime = false;
                    cellHaveChecked = true;
                }
            }
        }
        
        //��¼��һ�α���������ֱ��д�����ؿؼ�����ʽ  ��ѡ��ID|��ѡ��״̬@��ѡ��ID|��ѡ��״̬@
        if(!cellHaveChecked)
        {
             recordInfo.value += obj.id+"|"+cellChecked+"|"+cellValue+"@";
        }
        //�����¼���޸Ĺ�������ƴ������recordInfoDetails�����������������Ѿ����޸Ĺ�
        else
        {
            var tempRecord = "";
            for(var k=0;k<recordInfoDetails.length;k++)
            {
                if(recordInfoDetails[k] == "")//�ֶ�Ϊ��������
                {
                    continue;
                }
                tempRecord += recordInfoDetails[k]+"@";
            }
            recordInfo.value = tempRecord;
        }
    }

    var curLocation = window.location.toString();
    var htmlUrl = curLocation.substring(0,curLocation.indexOf('?'));
    var parameter = curLocation.substring(curLocation.indexOf('?')+1,curLocation.length);
    var parameters = parameter.split('&');
    
    function fillMonthLink()
    {
	var PanelDateLink = document.getElementById("PanelDateLink");
	if(!PanelDateLink)
	{
		return false;
	}


	var dateString="";
        for(var p=0;p<parameters.length;p++)
        {
            var evenParameterValue = parameters[p].split('=');
		if(evenParameterValue[0].toLowerCase() == "recordmonth")
            if(evenParameterValue[0].toLowerCase() == "recordmonth")
            {
                //���ݵĲ�����������
		if(evenParameterValue[1]==null || evenParameterValue[1] == '')
{
var date = new Date();
dateString = date.getYear()+"-"+(date.getMonth()+1)+"-01";
evenParameterValue[1] = dateString;
}
                
                    var dateInfo = evenParameterValue[1].split('-');

                    for(var m=1;m<=12;m++)
                    {
                        var newHref = "";
                        var newParameter = "";
                        var linkID = "a_"+m;
                        var monthLink = document.getElementById(linkID);
                        if(m<10)
                        {
                            evenParameterValue[1] = dateInfo[0]+"-0"+m+"-"+dateInfo[2];
                        }
                        else
                        {
                            evenParameterValue[1] = dateInfo[0]+"-"+m+"-"+dateInfo[2];
                        }
                        parameters[p] = "recordMonth"+"="+evenParameterValue[1];
 
                        for(var newP=0;newP<parameters.length;newP++)
                        {
                            newParameter += (parameters[newP]+'&');
                        }
                        newHref = htmlUrl+'?'+newParameter.substring(0,newParameter.length-1);
                        monthLink.href=newHref;
                    }
                break;
            }
            else
            {
                continue;
            }
        }
    }
    
    //�����������ϵ����س���ťʱ���¼���ҳ��
    function reloadPage()
    {
        if(event.keyCode == 13)
        {   
            var newParameter = "";
            var newYear = document.getElementById("year");
            var newInputYear = newYear.value;
            
            if(isNaN(newInputYear) || newInputYear.length != 4)
            {
                alert("��ݱ���Ϊ4λ�������͡�");
                return false;
            }
var dateString="";
            for(var p=0;p<parameters.length;p++)
            {
                var evenParameterValue = parameters[p].split('=');
                if(evenParameterValue[0].toLowerCase() == "recordmonth")
                {
			if(evenParameterValue[1] ==null || evenParameterValue[1] == '')
{
var date = new Date();
dateString = date.getYear()+"-"+(date.getMonth()+1)+"-01";alert(dateString );
evenParameterValue[1] = dateString;
}
                    //���ݵĲ�����������
                    //if(evenParameterValue[1]!=null && evenParameterValue[1] != '')
                    //{
                        var dateInfo = evenParameterValue[1].split('-');

                        dateInfo[0] = newYear.value;
                        var newDate = dateInfo[0]+"-"+dateInfo[1]+"-"+dateInfo[2];
                        parameters[p] = "recordMonth"+"="+newDate;

                    break;
                }
            }
            for(var newP=0;newP<parameters.length;newP++)
            {
                newParameter += (parameters[newP]+'&');
            }
            var newHref = htmlUrl+'?'+newParameter.substring(0,newParameter.length-1);

            //ȡ�������ύ�¼�����ֹҳ����ԭ�л�����ˢ�£��޷����¼�¼
            document.getElementById("form1").onsubmit=function notSubmit(){return false;};
            window.location.href = newHref;
            
        }
    }
    //�����¼����������Ϣ���������ŵ���ѡ������ʾ��ʾ
    //����obj��ʾ����ƶ����ĸ�ѡ��
    function  showRecordDetailInfo(obj)
    {
        var strValue = "";
        strValue = obj.value;
        if(strValue != null &&��strValue!= "")
        {
            var left = document.documentElement.scrollLeft+event.clientX+10;
            var top = document.documentElement.scrollTop+event.clientY;
            
            var divInfo = document.getElementById("divRecordDetailInfo");
            divInfo.innerHTML = strValue;
            divInfo.style.marginLeft = left;
            divInfo.style.marginTop = top;
            divInfo.style.display = "";
        }
    }
    //����Ƴ���ѡ��ȡ��DIV��ʾ
    function hideDIV()
    {
        var divInfo = document.getElementById("divRecordDetailInfo");
        divInfo.style.display = "none";
    }
    //��ʾά����Ϣ¼��ҳ��
    function showInputRecord()
    {
        inputRecord.style.display='';
    }
    
    //ȡ�õ���ά����Ϣ
    function getTodayRecord1()
    {
        var inputCheckBoxs = document.getElementsByName("inputCheckBox");//����ά����Ϣ��ѡ��
        var inputTextBoxes = document.getElementsByName("inputTextBox");//����/��ά����Ϣ�ı���
        
        
        var hiddenTodayRecord = document.getElementById("hiddenTodayRecord");//�洢����ά����Ϣ

        if(curDay == "")//��ǰ��
        {
            var hiddenDay = document.getElementById("hiddenDay");
            curDay = hiddenDay.value;
        }
        
        var todayRecordInfo = "";//new Array(inputCheckBoxs.length);
        
        for(var r = 0; r<inputCheckBoxs.length; r++)
        {
            var ProjectID = inputCheckBoxs[r].id.split('_')[1];
            var inputTextBox = document.getElementById("input_"+ProjectID);//������ͬά����Ŀ��¼��Ϣ
            todayRecordInfo += ProjectID;
            todayRecordInfo += "#";//��ѡ��ID�븴ѡ��֮��ķָ�
            todayRecordInfo += inputCheckBoxs[r].checked;
            todayRecordInfo += "|";//��ѡ�����ı���֮��ķָ�
            todayRecordInfo += inputTextBox.value;
            todayRecordInfo += "@";//ά����Ŀ֮��ķָ�
        }
        var index = todayRecordInfo.lastIndexOf('@');
        todayRecordInfo = todayRecordInfo.substring(0,index);
        hiddenTodayRecord.value = todayRecordInfo;
        
        return true;
    }
    
    
    //ȡ�õ���ά����Ϣ
    var selectTwiceTime = false;//��ϸ��Ϣ�����������޸Ľ�������еĲ����� Ϊtrue
    function getTodayRecord()
    {
        var inputCheckBoxes = document.getElementsByName("inputCheckBox");//����/��ά����Ϣ��ѡ��
        var inputTextBoxes = document.getElementsByName("inputTextBox");//����/��ά����Ϣ�ı���

        if(curDay == "")//����
        {
            var hiddenDay = document.getElementById("hiddenDay");
            curDay = hiddenDay.value;
        }
        for(var r = 0; r<inputCheckBoxes.length; r++)
        {
            var ProjectID = inputCheckBoxes[r].id.split('_')[1];//��Ŀ���
            var inputTextBox = document.getElementById("input_"+ProjectID);//������ͬά����Ŀ��¼��Ϣ���ı������ݣ�

            var otherInputID = "oth_"+ProjectID+"_"+curDay;//������Ǹ�ѡ�����������Ӧ�ؼ�
            var otherInput = document.getElementById(otherInputID);
            if(otherInput)
            {
                otherInput.value = inputTextBox.value;
                
                //changeChkValue(otherInput);
            }
            
            
            var detailCheckID = ProjectID+"_"+curDay; //��ǰ��¼��Ӧ�� ά����Ϣ��ѡ��
            
            var detailCheck = document.getElementById(detailCheckID);
            if(detailCheck)
            {
                detailCheck.value = inputTextBox.value;
                if(inputTextBox.value != "")
                {
                    detailCheck.parentNode.style.backgroundColor = "#8080ff";
                }
                else
                {
                    detailCheck.parentNode.style.backgroundColor = "#d3dfee";
                }
                
                selectTwiceTime = true;
                //����ά����¼��ѡ��ĵ����¼�
                selectRecord(detailCheck);
            }
            
            
        }
        //����¼���
        var inputRecord = document.getElementById("inputRecord");
        inputRecord.style.display='none';
    }
    
    
    function changeChkValue(obj)
    {
        var id = obj.id;
        id = id.substring(4,id.length);//ȡ�����ظ�ѡ��
        
        var inputID = "input_"+id.split('_')[0];//����¼�����ݵ��ı���
        
        var chk = document.getElementById(id);

        
        if(chk)
        {
            chk.value = obj.value;
            selectRecord(chk);
        }
        var input = document.getElementById(inputID);
        if(input)
        {
            input.value = obj.value;
        }
    }
    
    var QX = "";
    
    var url = window.location;
    var windowUrl = url.toString();
    var urlPares = windowUrl.split("?");
    var pares =  urlPares[1].split("&");
    for(var i=0;i<pares.length;i++)
    {
        var userQX = pares[i].split("=");
        if(userQX[0] == "userQX")
        {
            QX = userQX[1];
            QX = QX.toLowerCase();
        }
    }
    
    //���޸ĵ����ڣ����ڣ�д�����ؿؼ����磺2009-04#3��ʾ2009��4�µ����ܣ�
    var curDay = "";
    function getModefyDay(str,obj)
    {
        var hiddenToday = document.getElementById("hiddenToday");
        var curToday = hiddenToday.value;//��������� ��2009-5-14��

        curDay = str.split('#')[1];//��ǰѡ������
        
        var curDate = str.split('#')[0] +"-"+curDay;
        
        var disabled = false;//��ϸ��Ϣ�����޸�
        if(curToday != curDate && QX!="completecontrol")
        {
            disabled = true;
        }

        var cell = equipRecordTable.rows[0].childNodes.length;
        for(var i = 1 ;i<cell;i++)
        {
            equipRecordTable.rows[0].childNodes[i].style.backgroundColor="#4f81bd";
        }
        equipRecordTable.rows[0].childNodes[curDay].style.backgroundColor="#b07e32";

        //ѭ����ȡ���ǰ�У�����ǰ�е�����д����ϸ���
        var records = equipRecordTable.rows.length;
        for(var r = 1;r<records-1;r++)
        {
            //ȡ�õ�Ԫ���ڵĸ�ѡ��
            for(var c = 0 ;c<equipRecordTable.rows[r].childNodes[curDay].childNodes.length;c++)
            {
                var checkBox = equipRecordTable.rows[r].childNodes[curDay].childNodes[c];
                
                if(checkBox.tagname = "input" && checkBox.type == "checkbox")
                {
                    var checkBoxID = checkBox.id;
                    var equipID = checkBox.id.split('_')[0];
                    var detailCheck = document.getElementById("check_"+equipID);
                    var detailText = document.getElementById("input_"+equipID);
                   
                    if(detailCheck)
                    {
                        detailCheck.checked = checkBox.checked;
                        detailCheck.disabled = disabled;
                    }
                    if(detailText)
                    {
                        detailText.value = checkBox.value;
                        detailText.disabled = disabled;
                    }
                }
            }
        }
    }
    
    //�޸�ÿ��ά���ĸ�ѡ��ͬʱ�޸���ϸά�����ݵĸ�ѡ��
    //����ÿ����ϸά����¼��ѡ��ʱ����
    function checkDetailCheck(obj)
    {
        var projectId = obj.id.split('_')[1];
        if(curDay == "")
        {
            var hiddenDay = document.getElementById("hiddenDay");
            curDay = hiddenDay.value;
        }
        var detailCheckID = projectId+"_"+curDay
        var detailCheck = document.getElementById(detailCheckID);
        if(detailCheck)
        {
            detailCheck.checked = obj.checked;
        }
    }
   </script>
</head>
<body onload="fillMonthLink();">
    <form id="form1" runat="server">
    <div id="divRecordDetailInfo" style="border:1px; border-color:Yellow; background-color:#FFFF33; position:absolute; width:100px; height:20px; display:none;"></div>
    <div>
    <font size="2">
    <table border="0">
        <tr>
            <td width="1000" align="center">
                <b><asp:Label ID="equipRecordHead" runat="server"  Text="ÿ��ά��������¼��"></asp:Label></b>
            </td>
        </tr>
        
        <tr>
            <td align="left">&nbsp;&nbsp;��¼ʱ�䣺<asp:TextBox ID="year" Width="40px" 
                    BorderStyle="Groove" runat="server"></asp:TextBox>��<asp:Label ID="month" runat="server" Text=""></asp:Label>��
            &nbsp;
            <span ID="PanelDateLink" Visible="false" runat="server">
                <a id="a_1">1��</a>
                <a id="a_2">2��</a>
                <a id="a_3">3��</a>
                <a id="a_4">4��</a>
                <a id="a_5">5��</a>
                <a id="a_6">6��</a>
                <a id="a_7">7��</a>
                <a id="a_8">8��</a>
                <a id="a_9">9��</a>
                
                <a id="a_10">10��</a>
                <a id="a_11">11��</a>
                <a id="a_12">12��</a>
            </span>
            </td>
        </tr>
    </table>
    <table id="equipRecordTable" cellpadding=0 cellspacing=0 border=1 runat="server">
    </table>
    <table border=0 width="1000">
    <tr>
        <td align="left"><b>ע����ɺ��</b><asp:Button ID="btnSaveRecord" runat="server" Text="����" 
            onclick="btnSaveRecord_Click" />
    <font size=2>
            <input type="button" id="btnShowInput" visible="false" value="��д����ά����¼" onclick="showInputRecord();" runat="server" /></font>
        </td>
    </tr>
    </table>
    </font>
    </div>
    <div id="inputRecord" style="display:none;">
    <table border="1" width="600" style=" border-bottom:none; margin-bottom:0px;" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="2">
                <b>
                    ��дά����¼
                </b>
            </td> 
        </tr>
    </table>
    <table id="inputTable" runat="server" width="600" border="1" cellpadding="0" cellspacing="0">
    </table>

    <table border="1" width="600" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" style="border-top:none; margin-top:0px;">
                <input type="button" value="ȷ��" onclick="getTodayRecord();" />
            </td>
        </tr>
    </table>
    </div>
    <input type="hidden" id="hiddenEquipID" runat="server" />
    <input type="hidden" id="hiddenAllRecordInfo" runat="server" /><!-- �޸Ĺ���ά����¼ -->
    <input type="hidden" id="hiddenOperatorName" runat="server" /><!-- ҳ����ȫ������������ -->
    <input type="hidden" id="hiddenOperatorChange" runat="server" /><!-- �޸Ĺ��Ĳ��������� -->
    
    <input type="hidden" id="hiddenToday" runat="server" /><!-- ��ǰ���ڣ�2009-5-14 -->
    
    <input type="hidden" id="hiddenDay" runat="server" /><!-- ��ǰ���ڣ�1-31�� -->
    </form>
</body>
</html>
