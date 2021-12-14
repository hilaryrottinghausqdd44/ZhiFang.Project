$(function() {
	//获取套餐类型树服务地址
	var GET_TYPE_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL";
	
	//数据
	var INFO_DATA = null;
	//选中的父类型节点
	var CHECKED_PARENT_TYPE_DIV = null;
	//选中的子类型节点
	var CHECKED_CHILDREN_TYPE_DIV = null;
	
	//获取套餐类型数据
	function getPackageData(callback) {
		var url = GET_TYPE_URL;
		url += '?maxlevel=2&id=0&fields=Id';
		//url = JcallShell.String.encode(url,true);
		
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		//获取数据
		JcallShell.Server.ajax({
			showError:true,
			url: url
		}, function(data) {
			setTimeout(function(){$("#loading-div").modal("hide");},500);
			callback(data);
		});
	}
	
	//更改父类型列表内容
	function changeParentTypeHtml() {
		var list = INFO_DATA.Tree || [],
			html = [];
			
		for(var i in list) {
			html.push(
				'<div class="parentDiv" onclick="onParentTypeClick(this,\'' + list[i].tid + '\')">' + list[i].text + '</div>'
			);
		}
		if(html.length == 0){
			html.push('<div style="text-align:center;color:#999999;padding:40px 0;">没有类型</div>');
		}
		$("#info").html("");
		$("#list2").html("");
		$("#list1").html(html.join(""));
		
		if(list.length > 0){
			$("#list1").children(":first").click();
		}
	}
	//更改子类型列表内容
	function changeChildrenTypeHtml(id) {
		var pList = INFO_DATA.Tree || [],
			list = [],
			html = [];
			
		for(var i in pList){
			if(pList[i].tid == id){
				list = pList[i].Tree;
				break;
			}
		}
		
		for(var i in list) {
			html.push(
				'<div class="childrenDiv" onclick="onChildrenClick(\'' + list[i].tid + '\')">' + list[i].text + '</div>'
			);
		}
		
		if(html.length == 0){
			html.push('<div style="text-align:center;color:#999999;padding:40px;20px;">没有子类</div>');
		}
		$("#list2").html(html.join(""));
	}
	
	//显示错误信息
	function showError(msg) {
		$("#list1").html("");
		$("#list2").html("");
		$("#info").html('<div style="padding:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}
	
	//父节点点击处理
	function onParentTypeClick(div,id) {
		if(CHECKED_PARENT_TYPE_DIV){
			CHECKED_PARENT_TYPE_DIV.removeClass("checked");
		}
		CHECKED_PARENT_TYPE_DIV = $(div);
		CHECKED_PARENT_TYPE_DIV.addClass("checked");
		
		changeChildrenTypeHtml(id);
	}
	window.onParentTypeClick = onParentTypeClick;
	//子节点点击处理
	function onChildrenClick(id) {
		//跳转到信息页面
		location.href = "../type_list.html?id=" + id;
	}
	window.onChildrenClick = onChildrenClick;
	
	
	//查询数据
	function onSearch(){
		//获取套餐列表数据
		getPackageData(function(data) {
			if(data.success == true) {
				INFO_DATA = data.value || {};
				//更改列表内容
				changeParentTypeHtml();
			} else {
				showError(data.msg);
			}
		});
	}
	
	//初始化页面
	function initHtml() {
		//查询数据
		onSearch();
	}
	
	//初始化页面
	initHtml();
});