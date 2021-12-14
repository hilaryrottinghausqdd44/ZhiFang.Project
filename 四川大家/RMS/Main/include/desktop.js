
var draged=false; 
tdiv=null; 
function dragStart()
{ 
	ao=event.srcElement; 
	if((ao.tagName=="TD")||(ao.tagName=="TR")||(ao.tagName=="DIV"))ao=ao.offsetParent; 
	else return; 
	draged=true; 
	tdiv=document.createElement("div"); 
	tdiv.innerHTML=ao.outerHTML; 
	tdiv.style.display="block"; 
	tdiv.style.position="absolute"; 
	tdiv.style.filter="alpha(opacity=70)"; 
	tdiv.style.cursor="move"; 
	tdiv.style.width=ao.offsetWidth; 
	tdiv.style.height=ao.offsetHeight; 
	tdiv.style.top=getInfo(ao).top; 
	tdiv.style.left=getInfo(ao).left; 
	document.body.appendChild(tdiv); 
	lastX=event.clientX; 
	lastY=event.clientY; 
	lastLeft=tdiv.style.left; 
	lastTop=tdiv.style.top; 
	try{ 
	ao.dragDrop(); 
	}catch(e){} 
} 
function draging()//��Ҫ:�ж�MOUSE��λ�� 
{
	if(!draged)return; 
	var tX=event.clientX; 
	var tY=event.clientY; 
	tdiv.style.left=parseInt(lastLeft)+tX-lastX; 
	tdiv.style.top=parseInt(lastTop)+tY-lastY; 
	for(var i=0;i<parentTable.cells.length;i++)
	{ 
		var parentCell=getInfo(parentTable.cells[i]); 
		if(tX>=parentCell.left&&tX<=parentCell.right&&tY>=parentCell.top&&tY<=parentCell.bottom)
		{ 
			var subTables=parentTable.cells[i].getElementsByTagName("table"); 
			if(subTables.length==0)
			{ 
				if(tX>=parentCell.left&&tX<=parentCell.right&&tY>=parentCell.top&&tY<=parentCell.bottom)
				{ 
					parentTable.cells[i].appendChild(ao); 
				} 
				break; 
			} 
			for(var j=0;j<subTables.length;j++)
			{ 
				var subTable=getInfo(subTables[j]); 
				if(tX>=subTable.left&&tX<=subTable.right&&tY>=subTable.top&&tY<=subTable.bottom)
				{ 
					parentTable.cells[i].insertBefore(ao,subTables[j]); 
					break; 
				}
				else
				{ 
					parentTable.cells[i].appendChild(ao); 
				} 
			} 
		} 
	} 
} 
function dragEnd()
{ 
	if(!draged)return; 
	draged=false; 
	mm=ff(150,15); 
} 
function getInfo(o)//ȡ������
{ 
	var to=new Object(); 
	to.left=to.right=to.top=to.bottom=0; 
	var twidth=o.offsetWidth; 
	var theight=o.offsetHeight; 
	while(o!=document.body)
	{ 
		to.left+=o.offsetLeft; 
		to.top+=o.offsetTop; 
		o=o.offsetParent; 
	} 
	to.right=to.left+twidth; 
	to.bottom=to.top+theight; 
	return to; 
} 
function ff(aa,ab)//��GOOGLE��վ��,���ڻָ�λ�� 
{
	var ac=parseInt(getInfo(tdiv).left); 
	var ad=parseInt(getInfo(tdiv).top); 
	var ae=(ac-getInfo(ao).left)/ab; 
	var af=(ad-getInfo(ao).top)/ab; 
	return setInterval(function(){if(ab<1){ 
	clearInterval(mm); 
	tdiv.removeNode(true); 
	ao=null; 
	return 
	} 
	ab--; 
	ac-=ae; 
	ad-=af; 
	tdiv.style.left=parseInt(ac)+"px"; 
	tdiv.style.top=parseInt(ad)+"px" 
	} 
	,aa/ab) 
} 


var maskDiv=null;
var winlistDiv=null;
//�����б�
function ShowWinList()
{
	showmask();     //����������һ���˾�
	if(winlistDiv==null||!winlistDiv)
	{
		winlistDiv=document.createElement("DIV");         //���������Ľ���
		winlistDiv.style.cssText="position:absolute;top:75px;left:125px;background-color:#fff;";
		winlistDiv.innerHTML=cre_winlist();
		winlistDiv.style.display="";
		document.body.appendChild(winlistDiv);
	}
	else
	{
		winlistDiv.innerHTML=cre_winlist();
		winlistDiv.style.display="";
		winlistDiv.style.zIndex=100;
	}
	return false;
}
//����maskDiv  ,����������һ���˾���������͸����
function showmask()
{
	if(maskDiv==null||!maskDiv)
	{
		maskDiv=document.createElement("DIV");		
		maskDiv.style.cssText="position:absolute;top:0px;left:0px;background-color:#000;filter:alpha(opacity=30);";
		maskDiv.style.height=getscrollHeight();
		maskDiv.style.width=document.body.clientWidth;
		document.body.appendChild(maskDiv);
	}
}
function getscrollHeight()
{
	if(document.body.scrollHeight>document.documentElement.clientHeight)
	{
		return document.body.scrollHeight
	}
	else
	{
		return document.documentElement.clientHeight
	}
}
//�رն��ƴ���
function closeMaskDiv()
{
	if(maskDiv==null)
		return;
	document.body.removeChild(maskDiv);
	maskDiv=null;
}

//�����Ŀ
function showWinDiv(id)
{
	var windiv=document.getElementById(id);
	windiv.style.display="";          //ģ����ʾ
	//setjson3721value(id,"h","0");
	winlistDiv.innerHTML=cre_winlist();
	return false;
}
//�ر���Ŀ
function hideWinDiv(id)
{
	var windiv=document.getElementById(id);
	windiv.style.display="none";     //ģ�鲻��ʾ
	//setjson3721value(id,"h","1");
	winlistDiv.innerHTML=cre_winlist();
	return false;
}

//������Ŀ��ҳ������
function cre_winlist()
{
	
	var str='<div id="mydiy"><div class="box1"><div class="box2"><ul><li class="t"><div class="l"><strong>����ҳ����Ŀ</strong></div><div class="r"><button type="button" onclick="closeWinList()">���</button></div></li>';
	var index=0;
	var arrModule = strModules.split('+');      //�Ƿ�����,zfnews+��ҵ����,hy+ ����
	//alert(subModule[0]);
	for(var i=0;i<arrModule.length;i++)
	{
		
		var subModule = arrModule[i].split(',');  //�Ƿ�����,zfnews
		var ModuleName = subModule[0];            //�Ƿ�����
		var ModuleId = subModule[1];               //zfnews	
		if(!document.getElementById(ModuleId))    //����Ҳ����ÿؼ��������һѭ��
		{
			continue;
		}	
		var windiv=document.getElementById(ModuleId);
		var listr="a";
		str+='<li class="'+listr+'"><div class="l">'+ ModuleName +'</div><div class="r">';
		if(windiv.style.display=="none")
		{
			str+='<a href="" onclick="return showWinDiv(\''+ModuleId+'\')">+&nbsp;���</a></div></li>';
		}
		else
		{
			str+='<a href="" onclick="return hideWinDiv(\''+ModuleId+'\')">+&nbsp;�ر�</a></div></li>';
		}
	}
	str+='</ul></div></div></div>';
	return str;
}

function closeWinList()
{
	if(winlistDiv==null)
		return;
	winlistDiv.style.display="none";
	closeMaskDiv();
}

//���뵼������
function ShowExpdata()
{
	showmask();   //���ñ������˾�
	ExpdataDiv=document.createElement("DIV");
	ExpdataDiv.style.cssText="position:absolute;top:75px;left:185px;background-color:#fff;";
	ExpdataDiv.innerHTML='<div id="iopanel"><div class="box1"><div class="box2"><div class="iocon"><form id="expform" enctype="multipart/form-data" action="http://www.3721.com/updatauser.php" method="post"><select id="expdata" onchange="SetExp(this.value)"><option value="in">����</option><option value="out">����</option></select>&nbsp;<span id="expcont"><input type="hidden" name="MAX_FILE_SIZE" value="30000"><input type="file" name="userfile" id="textfield" style="height:22px;" />&nbsp;<input type="submit" value="�ύ"></span><div class="iotxt">����ǰҳ��������ļ����浽���أ��Ժ󽫸��ļ����룬���ɻָ�����֮ǰ����ʱ��״̬�� </div></form><div class="close2"><a href="" onclick="return closeExpdata()" title="�ر�"><img src="http://cn.yimg.com/i/3721/close_move.gif " border=0 /></a></div></div></div></div></div>';
	ExpdataDiv.style.display="";
	document.body.appendChild(ExpdataDiv);
	return false;
}
//�رյ��뵼������
function closeExpdata()
{
	if(ExpdataDiv==null)
		return;
	document.body.removeChild(ExpdataDiv);
	ExpdataDiv=null;
	closeMaskDiv();
	return false;
}
//�趨���뵼��������ѡ������½���
function SetExp(str)
{
	var res='<input type="hidden" name="MAX_FILE_SIZE" value="30000"><input type="file" name="userfile" id="textfield" style="height:22px;" />&nbsp;<input type="submit" value="�ύ">';
	if(str=="out")
	{
		document.getElementById("expform").action="http://www.3721.com/expuser.php";
		res='<button onclick="javascript:closeExpdata();location.href=\'http://www.3721.com/expuser.php\'">����</button>';
	}
	else
	{
		document.getElementById("expform").action = "http://www.3721.com/updatauser.php";
	}
	document.getElementById("expcont").innerHTML=res;
}

//����ģ��
function ModuleConfig(module_id)
{
	
	showmask();   //���ñ������˾�
	ExpdataDiv=document.createElement("DIV");
	ExpdataDiv.style.cssText="position:absolute;top:75px;left:185px;background-color:#fff;";
	var str1 = '';
	str1 += '<div id="iopanel"><div class="box1"><div class="box2"><div class="iocon">';
	str1 +='<div class="iotxt">ģ�鶨�ƹ���</div></div>';
	str1 +='<div class="iotxt">ģ��RSS��ַ:&nbsp;<input value="" style="height:20px" id="module_rss"/></div>';
	str1 +='<div class="iotxt">ģ�����:&nbsp;&nbsp;&nbsp;&nbsp;<input value="" style="height:20px" id="module_title"/></div>';
	str1 +='<div class="iotxt">ģ����ʾ����:<input value="" style="height:20px"/></div>';
	str1 +='<div class="iotxt">ģ������:&nbsp;&nbsp;&nbsp;&nbsp;<input value="" style="height:20px"/></div>';
	str1 +='<div class="iotxt"><input type="button" value="�ύ" style="height:20px" onclick="SaveModuleConfig()"/><input type="hidden" id="module_id" value=' + module_id + ' width="0" /></div></div>';
	
	str1 +='<div class="close2"><a href="" onclick="return closeExpdata()" title="�ر�"><img src="../include/close_move.gif " border=0 /></a></div></div></div></div></div>';
	ExpdataDiv.innerHTML = str1;
	ExpdataDiv.style.display="";
	document.body.appendChild(ExpdataDiv);	
	
	ReadModuleConfig();
	return false;

}
//��ȡģ������
function ReadModuleConfig()
{
	var module_id = document.getElementById("module_id").value;
	var result = DesktopSys.Desktop_config.ReadModuleConfig(module_id).value;    //����Ajax������ȡģ��Ļ�������
	var attr = result.split(',');

		//alert(attr.length);
	if(attr.length >1)
	{
		document.getElementById("module_rss").value = attr[0];
		document.getElementById("module_title").value = attr[1];
	}
	
		
}
//����ģ��
function SaveModuleConfig()
{
	var module_rss = document.getElementById("module_rss").value;
	var module_title = document.getElementById("module_title").value;
	var module_id = document.getElementById("module_id").value;

	//alert(module_id);
	var result = DesktopSys.Desktop_config.SaveModuleConfig(module_id,module_rss,module_title).value;    //����Ajax��������ģ��Ļ�������
	if(result=="0")
	{
		alert("����ɹ�");
		closeExpdata();
	}
	else
	{
		alert("����ʧ��");
	}
	//
}

