<%@ Page language="c#" AutoEventWireup="True" Inherits="OA.DBQuery.Input.addOpenTable" Codebehind="addOpenTable.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		    <title><%if(Request.QueryString["btnid"].ToString()=="BAdd")
                   Response.Write("������������addOpenTable");
			else if(Request.QueryString["btnid"].ToString()=="BModify")
                   Response.Write("�����޸�����addOpenTable");
			else if(Request.QueryString["btnid"].ToString()=="viewinfo")
                   Response.Write("�����������addOpenTable");
			else
			        Response.Write("");
			%></title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<script>

var btnid = '<%=Request.QueryString["btnid"]%>';
var modName = '<%=Request.QueryString["name"]%>';
var boolDisabled = false;
function EnableElement(bClear, BKeyIndexEnable, bDisabled) {
    if (bDisabled == true || bDisabled == false) {
        boolDisabled = bDisabled;
    }
	CollectWhereClause(this.frames["Right"].Form1.childNodes,bClear,BKeyIndexEnable);	
	this.frames["Right"].Form1.hAction.value='<%=Request.QueryString["btnid"]%>';
}
function DelData()
{
	this.frames["Right"].Form1.hAction.value='<%=Request.QueryString["btnid"]%>';
	this.frames["Right"].Form1.submit();
	window.close();
	//alert('123');
}
//����INPUT,TEXT,TEXTAREA,SELECT,AԪ�ؿ���
function CollectWhereClause(kids,bClear,BKeyIndexEnable)
{
	for(var i=0;i<kids.length;i++)
	{
		
		if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
		{
			//alert(kids[i].NoChange);
		    if (kids[i].NoChange == "No") //ֻ���ֶ�
		    {
		        //alert(kids[i].title);
		        if (kids[i].disabled == true)
		            kids[i].disabled = false;
		    }
		    //if(bClear&&(kids[i].type.toUpperCase()=='TEXT'))//�ش����⣬��������||kids[i].type.toUpperCase()=='HIDDEN'))
			if(bClear)
			{
				if(kids[i].type.toUpperCase()=='TEXT')
				{
					kids[i].value="";
					if(BKeyIndexEnable==null||BKeyIndexEnable==false)
					{
						if(kids[i].keyIndex=='Yes')
						{
							kids[i].disabled=true;
						}
					}
				}
				
			}
			//kids[i].style.border = 'solid 1pt #EFEFEF';
			//if (!boolDisabled) {
			//    kids[i].disabled = false;
			//}
		}
		
		//==================TextArea=====================
		if(kids[i].nodeName.toUpperCase() =='TEXTAREA') {
		    kids[i].style.border = 'solid 1pt #EFEFEF';
			if(kids[i].NoChange == "No") //ֻ���ֶ�
			{
				//if(kids[i].disabled==true)
				//	kids[i].disabled=false;
			}
			if(bClear)
			{
				kids[i].value = "";
            }
            if (!boolDisabled) {
                kids[i].disabled = false;
            }
		}
		if(kids[i].nodeName.toUpperCase()=='SELECT')
		{
			if(kids[i].NoChange == "No") //ֻ���ֶ�
			{
				if(kids[i].disabled)
					kids[i].disabled=false;
			}
			if(bClear)
			{
				kids[i].options[0].selected = true;
			}
			
			if(BKeyIndexEnable==null||BKeyIndexEnable==false)
			{
				if(kids[i].keyIndex=='Yes')
				{
					kids[i].options[0].selected=true;
					kids[i].disabled=true;
				}
			}

			if (boolDisabled) {
			    kids[i].disabled = false;
			    //kids[i].style.border = 'solid 1pt #EFEFEF';
			}
			if (kids[i].NoChange == "Yes") //ֻ���ֶ�
			{
			    kids[i].disabled = false;
			    kids[i].options[kids[i].selectedIndex].style.backgroundColor = 'white';
			    kids[i].disabled = true;
			}	
		}
		if(kids[i].nodeName.toUpperCase()=='A')
		{
			if(kids[i].NoChange == "No") //ֻ���ֶ�
			{
				if(kids[i].disabled)
					kids[i].disabled=false;
			}
        }
        
        if(kids[i].nodeName.toUpperCase()=='IFRAME')
		{
			if (!boolDisabled) {
			    //kids[i].style.border = 'solid 1pt #EFEFEF';
            }
        }
        
//        if (!boolDisabled) {
//            try {
////                kids[i].disabled = false;
////                kids[i].readOnly = true;
////                kids[i].style.readOnly = true;
//                //                kids[i].style.border = '1px solid';
//                    kids[i].disabled = false;
//                    kids[i].style.border = '1px solid';
//            }
//            catch (e) { }
        //        }

		if(kids[i].hasChildNodes)
			CollectWhereClause(kids[i].childNodes,bClear,BKeyIndexEnable);
	}
}

//����Ĭ��ֵ ���� boolReplace�Ƿ񽫵�ǰ��Ĭ��ֵ������ǰ��ֵ����ֵ����

function SetDefaultValue(boolReplace)
{
	SetDefaultValues(this.frames["Right"].Form1.childNodes,boolReplace)
	
} 
function SetDefaultValues(kids,boolReplace)
{
	for(var i=0;i<kids.length;i++)
	{
		if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
		{
			if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
			{
			    if (boolReplace && (kids[i].value == ""))//||kids[i].keyIndex=="Yes"
				{
					kids[i].value="";
					kids[i].value = kids[i].ColumnDefault;
					if (kids[i].keyIndex == "Yes") {
					    kids[i].value += "??";
					}
				}
			}
		}
		if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
		{
			if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
			{
				if(boolReplace || (kids[i].value==""||kids[i].keyIndex=="Yes") ) //�޸������Ĭ��ֵ����boolReplace && (kids[i].value==""||kids[i].keyIndex=="Yes")
				{
					
					kids[i].value="";
					kids[i].value=kids[i].ColumnDefault;
				}
			}
		}
		
		if(kids[i].nodeName.toUpperCase()=='SELECT')
		{
			if(kids[i].ColumnDefault!=""&&typeof(kids[i].ColumnDefault)!="undefined")
			{
				if(boolReplace||(kids[i].options.length>0&&kids[i].options[kids[i].selectedIndex].text==""))
				{
					for(var item=0;item<kids[i].options.length;item++)
					{
						if(kids[i].options[item].text==kids[i].ColumnDefault)
						{	
							kids[i].options[item].selected=true;
							break;
						}	
					}
				}
			}
		}
		//=================���iframe�еĶ���===============
		if(kids[i].nodeName.toUpperCase()=='IFRAME')
		{
			//kids[i].src="";
		}
		
		if(kids[i].hasChildNodes)
			SetDefaultValues(kids[i].childNodes,boolReplace);
	}
}

function FireMove(obj)
{
	if(obj.style.border!='#ccccff thin inset')
		obj.style.border='#ccccff thin outset';
}
function FireOut(obj)
{
	if(obj.style.border!='#ccccff thin inset')
		obj.style.border='#ccccff 0px outset';
}
function RandomNum()
{
		var today = new Date();
		var yy = eval(today.getYear())+'';
		var mon = eval(today.getMonth()) + '';
		//mm=(mm.length>1)?mm:0+mm;
		var dd = eval(today.getDate()) + '';
		var hh = eval(today.getHours()) + '';
		var mm = eval(today.getMinutes()) + '';
		var ss = eval(today.getSeconds()) +'';
			
		var vrand = Math.round(Math.random()*1000000);
		//var vrand1 = Math.round(Math.random()*1000000);
		//alert(ss.length);

		if(mon.length==1)
		{
			mon = '0' + mon;
		//	alert(mon);
		}
		//alert(yy + mon + dd + hh + mm + ss + vrand);
		return yy + mon +dd + "-" + hh + mm + ss + "-" + vrand;
}

var buttLastClicked=null

function FireQuery(obj)
{
	switch(obj.id)
	{
	    case 'BSave':
	        obj.style.border = '#ccccff 0px outset';
	        //if(buttLastClicked!=null&&buttLastClicked.id!='BSave')


	        //{

	        var strActionButton = "";
	        var strActionName = "";
	        if (btnid == 'BAdd') {
	            strActionButton = "����������һ���¼�¼����\r";
	            strActionName = "����";
	        }

	        ////���,���,��
	        ////���,���,��
	        ////���,���,��
	        ////���,���,�� �����и�ע��,����Ͳ�������,�������
	        ////���,���,��
	        ////���,���,��

	        if (btnid == 'BModify') {
	            strActionButton = "�������޸�һ����¼����\r";
	            strActionName = "�޸�";
	        }

	        if (btnid.id == 'BBatch') {
	            strActionButton = "�������������������¼�¼����,�������������������������\r";
	            strActionName = "��������";
	        }


	        //ConfirmNotAllowNull(this.frames["Right"].Form1.document.all['TableData'].childNodes);   //��֤���б���Ϊ��
	        ConfirmNotAllowNull(this.frames["Right"].Form1.childNodes);   //��֤���б���Ϊ��

	        var notAllowNullObj = this.frames["Right"].Form1.document.all['hNotAllowNull'].value;
	        //alert(this.frames["Right"].Form1.document.all['hNotAllowNull'].value);
	        if (notAllowNullObj != '') {
	            alert(notAllowNullObj + "����Ϊ��");
	            this.frames["Right"].Form1.document.all['hNotAllowNull'].value = '';

	            return;
	        }

	        //alert(notAllowNullObj);

	        var Bconfirm = confirm(strActionButton + "ȷ��Ҫ [" + strActionName + "] ����������\r\r");

	        if (Bconfirm) {

	            var frm = this.frames["Right"].frames;
	            //�ύÿ���������ʱ�ı�������,��淶ʽ��¼������
	            //alert(frm.length);

	            for (var iF = 0; iF < frm.length; iF++) {
	                if (frm(iF).location.href.indexOf("inputBrowseNews.aspx") <= 0)
	                    continue;

	                var newIframObjTextBox1 = frm(iF).document.all["TextBox1"];

	                newIframObjTextBox1.value = frm(iF).document.body.innerHTML;
	                //alert(frm(iF).name);
	                if (newIframObjTextBox1.value.indexOf("<FORM ") > 0) {

	                    newIframObjTextBox1.value = newIframObjTextBox1.value.substr(0, newIframObjTextBox1.value.indexOf("<FORM "));

	                    var fileName = RandomNum() + ".xml";
	                    if (btnid == 'BAdd') {
	                        if (frm(iF).name.length > 0) {
	                            //alert(frm(iF).Form1.action);
	                            //alert(frm(iF).Form1.action.length>frm(iF).Form1.action.indexOf("&Template=")+11);
	                            if ((frm(iF).Form1.action.indexOf("&Template=") > 0) && (frm(iF).Form1.action.length > frm(iF).Form1.action.indexOf("&Template=") + 11)) {

	                                var ColumnName = frm(iF).name.substr(3)
	                                //alert(ColumnName);
	                                frm(iF).parent.document.all[ColumnName].value = fileName;
	                                //alert(frm(iF).Form1.action);
	                                var shref = frm(iF).Form1.action.substr(0, frm(iF).Form1.action.indexOf("&Template="));
	                                //alert(frm(iF).parent.document.all[ColumnName].value);
	                                frm(iF).parent.document.all[ColumnName].disabled = false;

	                                shref = shref + "&FileName=" + fileName;
	                                frm(iF).Form1.action = shref;
	                                //alert(shref);
	                                frm(iF).Form1.submit();

	                            }

	                        }
	                    }
	                    else
	                        frm(iF).Form1.submit();
	                }


	            }
	            //return;
	            //alert('a');

	            //meizzDateLayer
	            //alert(this.frames["Right"].frames.length);

	            //newIframObjTextBox1.value=newIframObjTextBox1.value.substr(0,newIframObjTextBox1.value.indexOf("<FORM "));

	            //alert(newIframObjTextBox1.value);
	            //alert(this.frames["Right"].frames["frmEName8"].document.all["TextBox1"].value);
	            //this.frames["Right"].frames["frmEName8"].Form1.submit();
	            //return false;


	            var filename = OA.DBQuery.Input.addOpenTable.CreateFileName();           //��ȡ�������˵�GUID�ļ���
	            //alert(filename.value)
	            if (window.frames["Right"].document.getElementById("$$Desig_" + modName))           //�鿴ģ�����Ƿ���$$Desig_ģ�����ؼ� xiaod edit  07/07/04
	            {
	                window.frames["Right"].document.getElementById("$$Desig_" + modName).value = filename.value;
	            }
	            else {
	                alert('ϵͳ�Ҳ���$$Desig�ؼ�������ϵ����Ա��');
	                return;
	            }



	            var strContent = window.frames["Right"].document.getElementById("userdesin").innerHTML;
	            window.frames["WriteFile"].document.getElementById("TextBox1").value = strContent;
	            window.frames["WriteFile"].document.getElementById("TextBox2").value = filename.value;
	            window.frames["WriteFile"].Form1.submit();


	            var myDataRun = CollectDataRun(this.frames["Right"].Form1);
	            //alert(myDataRun);
	            //���ݴ���frames["Right"].Form1.document.all['hQueryCollection']��
	            this.frames["Right"].Form1.submit();



	            //alert(this.frames["Right"].Form1.document.body.innerHTML);
	            //alert(this.frames["Right"].Form1.document.all['hQueryCollection'].value);
	            //var ss = OA.DBQuery.Input.addOpenTable.ServerSideAdd(1,2);
	            //alert(ss.value);
	            //return;
	            //this.frames["Right"].Form1.document.all['hNotAllowNull']
	            window.status = "��ɣ�����";
	            if (opener != null && opener.document.getElementById("BAdd") != null) {
	                opener.document.getElementById(btnid).style.border = '#ccccff 0px outset';
	            }
	            //alert('a');
	            window.setInterval(ReturnState, 1000);    //ѯ��iframe��״̬
	            //window.close();

	        }
	        else {
	            window.status = "��������";
	            buttLastClicked = obj;
	            var strUrl = this.frames["Right"].location.href;
	            if (strUrl.substr(strUrl.length - 1) == "#")
	                strUrl = strUrl.substr(0, strUrl.length - 1);
	            this.frames["Right"].location = strUrl; //Form1.reset();

	            return;
	        }
	        /*
	        }
	        else
	        {
	        window.status="���ܱ��棬��ѡ�����ӻ��޸ĸ��Ʋ���";
	        buttLastClicked=obj;
	        return;
	        }
	        */
	        break;
case 'BCancel':
			var strUrl=this.frames["Right"].location.href;
			if(strUrl.substr(strUrl.length-1)=="#")
				strUrl=strUrl.substr(0,strUrl.length-1);
			//this.frames["Right"].location=strUrl;//Form1.reset();
			obj.style.border='#ccccff 0px outset';
			window.status="ȡ������������,�Ѿ����ز�ѯҳ�棡";
			
			if(opener!=null&&opener.document.getElementById(btnid)!=null)
			{
				opener.document.getElementById(btnid).style.border='#ccccff 0px outset';
			}
		
			
			window.close();
			
default:
			break;
			
	}
	buttLastClicked=obj;
}
function ReturnState()
{
    if (window.frames["Right"].document.readyState == "complete") {
        
        var frmFromListWindow = window.opener.frames["ContentMain"];
        if (frmFromListWindow != null) {
            frmFromListWindow.Form1.submit();
//            var parentQuery = frmFromListWindow.parent.frames["Top"];
//            if (parentQuery != null) {
//                parentQuery.Form1.submit();
//            }
        }
        else {
            var frmFromMainButtons = window.opener.frames["Right"];
            if (frmFromMainButtons != null) {
                //frmFromMainButtons.document.location.href = frmFromMainButtons.document.location.href;
                var parentQuery = frmFromMainButtons.parent; //.parent.frames["Top"];
                if (parentQuery != null) {
                    parentQuery.Form1.submit();
                }
            }
        }
        window.close();
	}
}

function ConfirmNotAllowNull (kids)
{
	
	for(var i=0;i<kids.length;i++)
	{
		if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
		{
			switch(kids[i].type.toUpperCase())
			{
			    case "TEXT":
			        if (kids[i].NoChange == "Yes") {
			            if (kids[i].value == "" && kids[i].AllowNull == "No") {
			                kids[i].style.backgroundColor = 'Coral';
			                this.frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].title;
			                return true;
			            }
			        }
			        else
					if(kids[i].value==""&&kids[i].AllowNull=="No")
					{
						kids[i].select();
						kids[i].focus();
						kids[i].style.backgroundColor='Coral';
						this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
						//alert(kids[i].title);
						return true;
					}
					else if(kids[i].id!=null&&kids[i].id.length>0)
					{
						kids[i].style.backgroundColor='transparent';
					}
					break;
	            case "RADIO":
	                //	                if (kids[i].AllowNull == "No") {
	                //	                    
	                //	                }
	                var strRadioID = kids[i].name;

	                var rdAll = kids[i].ownerDocument.all[strRadioID];
	                if (rdAll != null) {
	                    var bRadioChecked = false;
	                    for (var iRadio = 0; iRadio < rdAll.length; iRadio++) {
	                        if (rdAll[iRadio].checked) {
	                            bRadioChecked = true;
	                        }
	                    }
	                    if (!bRadioChecked) {
	                        kids[i].style.backgroundColor = 'Coral';
	                        this.frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].value + "...";
	                        return;
	                    }
	                }
	                break;

				case "CHECKBOX":
				    var strRadioID = kids[i].name;

				    var rdAll = kids[i].ownerDocument.all[strRadioID];
				    if (rdAll != null) {
				        var bRadioChecked = false;
				        for (var iRadio = 0; iRadio < rdAll.length; iRadio++) {
				            if (rdAll[iRadio].checked) {
				                bRadioChecked = true;
				            }
				        }
				        if (!bRadioChecked) {
				            kids[i].style.backgroundColor = 'Coral';
				            this.frames["Right"].Form1.document.all['hNotAllowNull'].value = kids[i].value + "...";
				            return;
				        }
				    }
				    break;
			
				default:
					break;
			}
			
		}
		if(kids[i].nodeName.toUpperCase()=='SELECT')
		{
			if((kids[i].options.length==0||kids[i].selectedIndex==-1||kids[i].options[kids[i].selectedIndex].text=="")&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
			{
				kids[i].focus();
				kids[i].style.backgroundColor='Coral';
				this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
				//alert(kids[i].title);
				return;
			}
			else if(kids[i].id!=null&&kids[i].id.length>0)
			{
				kids[i].style.backgroundColor='transparent';
			}
		}
		
		//=================TextArea�ڵ�=================
		if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
		{
			if(kids[i].value==""&&kids[i].NoChange=="No"&&kids[i].AllowNull=="No")
			{
				kids[i].select();
				kids[i].focus();
				kids[i].style.backgroundColor='Coral';
				this.frames["Right"].Form1.document.all['hNotAllowNull'].value=kids[i].title;
				return;
			}
			else if(kids[i].id!=null&&kids[i].id.length>0)
			{
				kids[i].style.backgroundColor='transparent';
			}
		}
		//-------------------End------------------------
		if(kids[i].hasChildNodes)
			ConfirmNotAllowNull(kids[i].childNodes);
	}
}
//�ռ�����
function CollectDataRunList(kids)
{
	for(var i=0;i<kids.length;i++)
	{	

		if(kids[i].nodeName.toUpperCase()=='INPUT')//&&kids[i].type.toUpperCase()=='TEXT')
		{
			if(kids[i].databand!=null)                   //�ж�Ԫ���Ƿ�󶩵����ݿ�
			{
			    //alert(kids[i].type.toUpperCase() + ":" + kids[i].id + kids[i].method + kids[i].value);
				switch(kids[i].type.toUpperCase())
				{
					case "TEXT":
					//case "HIDDEN"://�ش����⣬��������||kids[i].type.toUpperCase()=='HIDDEN'))
						if(kids[i].value!="")
						//hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].value;
						hDataRun +="\t" +  kids[i].id + kids[i].method + kids[i].value;
						break;
					case "RADIO":
						if(kids[i].checked!="")
							hDataRun +="\t" +  kids[i].name + kids[i].method + kids[i].value;
						break;
					case "CHECKBOX":
						if(hDataRun.indexOf(kids[i].name + kids[i].method)==-1)
						{
						 
							var checkBoxList=window.frames["Right"].Form1.document.all[kids[i].name];//window.frames["ContentMain"].frames["Right"].Form1.document.all[kids[i].name];
							// edit by ����  07/07/04
							var strCHKValues="";
							for(var iCHK=0;iCHK<checkBoxList.length;iCHK++)
							{
								if(checkBoxList[iCHK].checked)
									strCHKValues +="," + checkBoxList[iCHK].value;
									//hDataRun +="\n" +  kids[i].name + kids[i].method + kids[i].value;
							}
							if(strCHKValues.length>0)
								strCHKValues=strCHKValues.substr(1);
							if(strCHKValues.length>0) //�ǲ���Ҫ�޸�?
								hDataRun +="\t" +  kids[i].name + kids[i].method + strCHKValues;///����ż�������
						}
						break;
					default:
						break;
				}
			}
			
		}
		
		//============�ռ�TextArea============
		if(kids[i].nodeName.toUpperCase() == 'TEXTAREA')
		{
			if(kids[i].value!="")
			{
				//var txtValue = kids[i].value.replace(/[\r][\n]/g, "��");
			    //alert(txtValue);
			    kids[i].value = kids[i].value.replace(/[\t]/g, '');
				hDataRun +="\t" +  kids[i].id + kids[i].method + kids[i].value;
			}
		}
		//-----------------End----------------
		
		if(kids[i].nodeName.toUpperCase()=='SELECT')
		{
			var selOptions=kids[i].options;
			var strCHKValues="";
			for(var iCHK=0;iCHK<selOptions.length;iCHK++)
			{
				if(selOptions[iCHK].selected)
					strCHKValues +="," + selOptions[iCHK].text;
					//hDataRun +="\n" +  kids[i].name + kids[i].method + kids[i].value;
			}
			if(strCHKValues.length>0)
				strCHKValues=strCHKValues.substr(1);
			if(strCHKValues.length>0)
				hDataRun +="\t" +  kids[i].id + kids[i].method + strCHKValues;///����ż�������
			
			//if(kids[i].options[kids[i].selectedIndex].text!="")
			//	hDataRun +="\n" +  kids[i].id + kids[i].method + kids[i].options[kids[i].selectedIndex].text;
		}
		if(kids[i].hasChildNodes)
			CollectDataRunList(kids[i].childNodes);
		
	}
}
var hDataRun="";
function CollectDataRun(myForm)
{
	hDataRun="";
	//CollectDataRunList(myForm.document.all['TableData'].childNodes);
	CollectDataRunList(myForm.childNodes);
	myForm.hQueryCollection.value=hDataRun;
	//alert(hDataRun);
	return hDataRun;
	//return false;
}

function ClearElement(ColumnType)
{
	ClearElementLoop(this.frames["Right"].Form1.childNodes,ColumnType);
}

function ClearElementLoop(kids,ColumnType)
{
	for(var i=0;i<kids.length;i++)
	{
		if(kids[i].nodeName.toUpperCase()=='INPUT')
		{
			if(kids[i].type.toUpperCase()=='TEXT')
			{
				if(kids[i].getAttribute("columnType")!=null&&kids[i].columnType==ColumnType)
					kids[i].value="";
			}
		}
		if(kids[i].hasChildNodes)
			ClearElementLoop(kids[i].childNodes,ColumnType);
	}
}

function SetDefaultValueForCopy()
{
	SetDefaultValues(this.frames["Right"].Form1.childNodes,false)
}

function testAlert(obj) {
    if (window.frames["Right"].document.readyState == "complete") {
        window.opener.location.reload();
        var frmFromListWindow = window.opener.frames["Right"];
        if (frmFromListWindow) {
            frmFromListWindow.parent.Form1.submit();
            //alert('a');
            //debugger;
            //alert("Right=" + frmFromListWindow.document.location.href);
            window.close();
        }
        else {
            var frmFromMainButtons = window.opener.frames["ContentMain"];
            if (frmFromMainButtons) {
                frmFromMainButtons.Form1.submit();
            //    alert('b');
            //    alert("ContentMain=" + frmFromMainButtons.document.location.href);
              window.close();
            }
        }
    }
    return;
    var strUrl = this.frames["Right"].location.href;
    if (strUrl.substr(strUrl.length - 1) == "#")
        strUrl = strUrl.substr(0, strUrl.length - 1);
    //this.frames["Right"].location=strUrl;//Form1.reset();
    obj.style.border = '#ccccff 0px outset';
    window.status = "ȡ������������,�Ѿ����ز�ѯҳ�棡";

    if (opener != null && opener.document.getElementById(btnid) != null) {
        opener.document.getElementById(btnid).style.border = '#ccccff 0px outset';
    }

    
    window.close();
}

var num = 0;
var k = -1;
var selecto = null;
var g_iFrame = "";
var g_objID = "";


function keyPressdiv(key, iFrame, ObjID, OrgValue) {
    //alert(key);
    if (key == 13) {
        DivSetVisible(false, 0, 0, '', '');
        return;
    }
    if (key != 40 && key != 38)
        return;
    g_iFrame = iFrame;
    g_objID = ObjID;
    var DivRef = document.getElementById('PopupDiv');
    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
        num = DivRef.firstChild.rows.length;
    }
    if (num <= 0)
        return;
    //alert(key);
    if (PopupDiv.style.display == "block") {
        //��
        if (key == 40) {
            k++;
            if (k >= num) {
                k = -1;
            }
        }
        //��
        if (key == 38) {
            k--;
            if (k < 0) {
                k = num;
            }
        }
        //alert(k);

        for (var i = 0; i < num; i++) {
            mouseOut(DivRef.firstChild.rows[i]);
        }
        if (k >= 0 && k < num) {//ѡ��ֵ
            mouseOver(DivRef.firstChild.rows[k]);
            selecto = DivRef.firstChild.rows[k].cells[0].innerHTML;
            SelectV(selecto);
        }
        else if (OrgValue.indexOf('=') > 0)//ԭʼֵ
        {
            OrgValue = OrgValue.substr(OrgValue.indexOf('=') + 1);
            SelectV(OrgValue);
        }
    }
}

function innerHTMLdiv(strList) {
    var DivRef = document.getElementById('PopupDiv');
    DivRef.innerHTML = strList;
    if (DivRef.firstChild != null && DivRef.firstChild.rows != null) {
        num = DivRef.firstChild.rows.length;
    }
}


function SelectV(str) {
    window.frames[g_iFrame].SetV(g_objID, str);
}

function ClickSelectV(str) {
    window.frames[g_iFrame].SetV(g_objID, str);
    DivSetVisible(false, 0, 0, '', '');
}
function mouseOver(e) {
    e.style.backgroundColor = "blue";
    e.style.color = "white";
    //e.style.fontWeight = "bold";
}
function mouseOut(e) {
    e.style.backgroundColor = "#dedede";
    e.style.color = "black";
    //e.style.fontWeight = "normal";
}
function DivSetVisible(state, objTop, objLeft, iFrame, ObjID) {
    var DivRef = document.getElementById('PopupDiv');

    DivRef.style.top = objTop; //document.body.scrollTop + event.clientY + 10;
    DivRef.style.left = objLeft; //document.body.scrollLeft + event.clientX;

    var IfrRef = document.getElementById('framePopupDiv');
    if (state) {
        DivRef.style.display = "block";
        IfrRef.style.width = DivRef.offsetWidth;
        IfrRef.style.height = DivRef.offsetHeight;
        IfrRef.style.top = DivRef.style.top;
        IfrRef.style.left = DivRef.style.left;
        IfrRef.style.zIndex = DivRef.style.zIndex - 1;
        IfrRef.style.display = "block";
        g_iFrame = iFrame;
        g_objID = ObjID;
    }
    else {
        DivRef.style.display = "none";
        IfrRef.style.display = "none";
    }
} 	
    </script>
    <script language=javascript for="hLoaded" event="onpropertychange">
		
		switch(btnid)
		{
			case 'BAdd':
				//alert(document.getElementsByName("hLoaded").value);
				//alert('ok');
				EnableElement(true,true,true);
				SetDefaultValue(true);
				break;
			case 'BModify':
				EnableElement(false,true,true);
				SetDefaultValue(true);
				break;
			case 'BCopy':
				EnableElement(false,true,true);
				ClearElement("�ļ�");
				ClearElement("����");
				SetDefaultValueForCopy();
				break;
			case 'BDelete':
				DelData();
			
			    break;
            case 'viewinfo':
				EnableElement(false,true,false);
				//SetDefaultValue(true);
				break;
		}
	</script>
	</HEAD>
	
	
	 <body MS_POSITIONING="GridLayout" topmargin=0 leftmargin=0 rightmargin=0 bottommargin=0>
		<form id="Form1" method="post" runat="server">
			<table width=100% height=100%>
	<tr>
	<TD vAlign="top">
					
					<iframe id="Right" name="Right" src="InputOneTableTemplate.aspx?<%=Request.ServerVariables["Query_String"]%>" frameBorder="0" width="100%"
							scrolling="auto" height="100%"></iframe>
		</TD>
	</tr>
	<tr height=24>
		<td align=center>
		<iframe src="WriteFile.aspx" id="WriteFile" height="0px" width=0></iframe>
		<img id="BSave" src="../image/middle/save.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							 onclick="FireQuery(this)" title="�������">	&nbsp;&nbsp;
					<img id="BCancel" src="../image/middle/cancel.jpg" width="79" height="24" border="0"
							onmouseover="FireMove(this)" 
							onmouseout="FireOut(this)" 
							onclick="FireQuery(this)" title="ȡ������"></td>
	</tr>
	</table>
		
		<input type=hidden name="hLoaded" id="hLoaded" value="0">

		</form>
	
	</body>
	  <script>
	<%if(Request.QueryString["btnid"].ToString()=="viewinfo"){%>
	document.all.BSave.style.display="none";
	document.all.BCancel.style.display="none";
	<%}%>
	

	
	
  </script>

</HTML>

