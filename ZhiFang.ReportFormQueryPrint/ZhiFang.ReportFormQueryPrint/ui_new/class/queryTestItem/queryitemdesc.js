function getRootPath(){
   //获取当前网址，如： http://localhost:8083/uimcardprj/share/meun.jsp
	var curWwwPath=window.document.location.href;
	//获取主机地址之后的目录，如： uimcardprj/share/meun.jsp
	var pathName=window.document.location.pathname;
	var pos=curWwwPath.indexOf(pathName);
	//获取主机地址，如： http://localhost:8083    
	var localhostPaht=curWwwPath.substring(0,pos);
	//获取带"/"的项目名，如：/uimcardprj
	var projectName=pathName.substring(0,pathName.substr(1).indexOf('/')+1);
	var rootPath = localhostPaht+projectName;
	//console.log(rootPath);
	return rootPath;
};

$(function(){
	var itemno = Shell.util.Path.getRequestParams(true);
	itemdescalert(itemno.ITEMNO);
});


function itemdescalert(itemno,url){
	var selecturl = "";
	if(url){
		selecturl = url;
	}else{
		selecturl = getRootPath()+"/ServiceWCF/DictionaryService.svc/GetTestItemItemDescByItemNo";
	}
	$.ajax({
		type:'get',
		url:selecturl,
		async:true,
		data:{ItemNo:itemno},
		contentType:"json/application",
		dataType:'json',
		success:function(data){
			if(JSON.parse(data.ResultDataValue)){
					$("#itemname").html("项目名称："+JSON.parse(data.ResultDataValue)[0].CName);
					
					var itemdescs =  JSON.parse(data.ResultDataValue)[0].ItemDesc;
					var itemdescsarr1 = itemdescs.split("正常值：");
					if(itemdescsarr1.length >= 2){
						var itemdescsarr2 = itemdescsarr1[1].split("临床意义：");
						$("#itemdesc").html("<span style='color: #0fabef; '>正常值：</span><br /><span style='padding-left:20px;'>"+itemdescsarr2[0]+"</span>"+"<br/><span style='color: #0fabef; '>临床意义：</span><br /><span style='padding-left:20px;'>"+itemdescsarr2[1]+"</span>");
					}else{
						$("#itemdesc").html("<span style='color: #0fabef; '>临床意义：</span><br /><span style='padding-left:20px;'>"+itemdescs+"</span>");
					}
			}else{
				$("#error").html("此项目无临床意义");
			}
			
		},
		error:function(data){
			$("#error").html("查询错误");
		}
	})
};


