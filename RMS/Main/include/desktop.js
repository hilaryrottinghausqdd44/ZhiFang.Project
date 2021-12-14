
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
function draging()//重要:判断MOUSE的位置 
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
function getInfo(o)//取得坐标
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
function ff(aa,ab)//从GOOGLE网站来,用于恢复位置 
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
//弹出列表
function ShowWinList()
{
	showmask();     //给背景铺上一层滤镜
	if(winlistDiv==null||!winlistDiv)
	{
		winlistDiv=document.createElement("DIV");         //创建弹出的界面
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
//创建maskDiv  ,给背景铺上一层滤镜，并设置透明度
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
//关闭定制窗口
function closeMaskDiv()
{
	if(maskDiv==null)
		return;
	document.body.removeChild(maskDiv);
	maskDiv=null;
}

//添加栏目
function showWinDiv(id)
{
	var windiv=document.getElementById(id);
	windiv.style.display="";          //模块显示
	//setjson3721value(id,"h","0");
	winlistDiv.innerHTML=cre_winlist();
	return false;
}
//关闭项目
function hideWinDiv(id)
{
	var windiv=document.getElementById(id);
	windiv.style.display="none";     //模块不显示
	//setjson3721value(id,"h","1");
	winlistDiv.innerHTML=cre_winlist();
	return false;
}

//定制栏目的页面内容
function cre_winlist()
{
	
	var str='<div id="mydiy"><div class="box1"><div class="box2"><ul><li class="t"><div class="l"><strong>定制页面栏目</strong></div><div class="r"><button type="button" onclick="closeWinList()">完成</button></div></li>';
	var index=0;
	var arrModule = strModules.split('+');      //智方新闻,zfnews+行业新闻,hy+ ……
	//alert(subModule[0]);
	for(var i=0;i<arrModule.length;i++)
	{
		
		var subModule = arrModule[i].split(',');  //智方新闻,zfnews
		var ModuleName = subModule[0];            //智方新闻
		var ModuleId = subModule[1];               //zfnews	
		if(!document.getElementById(ModuleId))    //如果找不到该控件则继续下一循环
		{
			continue;
		}	
		var windiv=document.getElementById(ModuleId);
		var listr="a";
		str+='<li class="'+listr+'"><div class="l">'+ ModuleName +'</div><div class="r">';
		if(windiv.style.display=="none")
		{
			str+='<a href="" onclick="return showWinDiv(\''+ModuleId+'\')">+&nbsp;添加</a></div></li>';
		}
		else
		{
			str+='<a href="" onclick="return hideWinDiv(\''+ModuleId+'\')">+&nbsp;关闭</a></div></li>';
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

//导入导出功能
function ShowExpdata()
{
	showmask();   //设置背景的滤镜
	ExpdataDiv=document.createElement("DIV");
	ExpdataDiv.style.cssText="position:absolute;top:75px;left:185px;background-color:#fff;";
	ExpdataDiv.innerHTML='<div id="iopanel"><div class="box1"><div class="box2"><div class="iocon"><form id="expform" enctype="multipart/form-data" action="http://www.3721.com/updatauser.php" method="post"><select id="expdata" onchange="SetExp(this.value)"><option value="in">导入</option><option value="out">导出</option></select>&nbsp;<span id="expcont"><input type="hidden" name="MAX_FILE_SIZE" value="30000"><input type="file" name="userfile" id="textfield" style="height:22px;" />&nbsp;<input type="submit" value="提交"></span><div class="iotxt">将当前页面的配置文件保存到本地，以后将该文件导入，即可恢复成您之前保存时的状态。 </div></form><div class="close2"><a href="" onclick="return closeExpdata()" title="关闭"><img src="http://cn.yimg.com/i/3721/close_move.gif " border=0 /></a></div></div></div></div></div>';
	ExpdataDiv.style.display="";
	document.body.appendChild(ExpdataDiv);
	return false;
}
//关闭导入导出窗口
function closeExpdata()
{
	if(ExpdataDiv==null)
		return;
	document.body.removeChild(ExpdataDiv);
	ExpdataDiv=null;
	closeMaskDiv();
	return false;
}
//设定导入导出的下拉选项，并更新界面
function SetExp(str)
{
	var res='<input type="hidden" name="MAX_FILE_SIZE" value="30000"><input type="file" name="userfile" id="textfield" style="height:22px;" />&nbsp;<input type="submit" value="提交">';
	if(str=="out")
	{
		document.getElementById("expform").action="http://www.3721.com/expuser.php";
		res='<button onclick="javascript:closeExpdata();location.href=\'http://www.3721.com/expuser.php\'">导出</button>';
	}
	else
	{
		document.getElementById("expform").action = "http://www.3721.com/updatauser.php";
	}
	document.getElementById("expcont").innerHTML=res;
}

//定制模块
function ModuleConfig(module_id)
{
	
	showmask();   //设置背景的滤镜
	ExpdataDiv=document.createElement("DIV");
	ExpdataDiv.style.cssText="position:absolute;top:75px;left:185px;background-color:#fff;";
	var str1 = '';
	str1 += '<div id="iopanel"><div class="box1"><div class="box2"><div class="iocon">';
	str1 +='<div class="iotxt">模块定制功能</div></div>';
	str1 +='<div class="iotxt">模块RSS地址:&nbsp;<input value="" style="height:20px" id="module_rss"/></div>';
	str1 +='<div class="iotxt">模块标题:&nbsp;&nbsp;&nbsp;&nbsp;<input value="" style="height:20px" id="module_title"/></div>';
	str1 +='<div class="iotxt">模块显示行数:<input value="" style="height:20px"/></div>';
	str1 +='<div class="iotxt">模块属性:&nbsp;&nbsp;&nbsp;&nbsp;<input value="" style="height:20px"/></div>';
	str1 +='<div class="iotxt"><input type="button" value="提交" style="height:20px" onclick="SaveModuleConfig()"/><input type="hidden" id="module_id" value=' + module_id + ' width="0" /></div></div>';
	
	str1 +='<div class="close2"><a href="" onclick="return closeExpdata()" title="关闭"><img src="../include/close_move.gif " border=0 /></a></div></div></div></div></div>';
	ExpdataDiv.innerHTML = str1;
	ExpdataDiv.style.display="";
	document.body.appendChild(ExpdataDiv);	
	
	ReadModuleConfig();
	return false;

}
//读取模块属性
function ReadModuleConfig()
{
	var module_id = document.getElementById("module_id").value;
	var result = DesktopSys.Desktop_config.ReadModuleConfig(module_id).value;    //调用Ajax方法读取模块的基本属性
	var attr = result.split(',');

		//alert(attr.length);
	if(attr.length >1)
	{
		document.getElementById("module_rss").value = attr[0];
		document.getElementById("module_title").value = attr[1];
	}
	
		
}
//保存模块
function SaveModuleConfig()
{
	var module_rss = document.getElementById("module_rss").value;
	var module_title = document.getElementById("module_title").value;
	var module_id = document.getElementById("module_id").value;

	//alert(module_id);
	var result = DesktopSys.Desktop_config.SaveModuleConfig(module_id,module_rss,module_title).value;    //调用Ajax方法保存模块的基本属性
	if(result=="0")
	{
		alert("保存成功");
		closeExpdata();
	}
	else
	{
		alert("保存失败");
	}
	//
}

