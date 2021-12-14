$(function() {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//获取套餐服务地址
	var PACKAGE_RECOMMENDED_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchBLabTestItemVOByTreeId";
	
	//当前页码
	var PAGE = 1;
	//每页数量
	var LIMIT = 20;
	
	//获取套餐数据
	function getPackageData(callback) {
		//page={page}&limit={limit}&isPlanish={isPlanish}&isSearchChild={isSearchChild}&
		//treeId={treeId}&fields={fields}&where={where}&sort={sort}'
		var url = PACKAGE_RECOMMENDED_URL;
		url += '?page=' + PAGE + '&limit=' + LIMIT + '&isSearchChild=1&treeId=' + params.ID + '&where=' + getWhere();
		url = JcallShell.String.encode(url,true);
		
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
	
	//更改套餐列表内容
	function changeListHtml(list) {
		var templet = getRowTemplet(),
			defaultPic = JcallShell.System.Path.ROOT + '/Images/dajia/logo.jpg',
			html = [];
			
		for(var i in list) {
			var row = templet;
			row = row.replace(/{Pic}/g, list[i].Pic || defaultPic);
			row = row.replace(/{Name}/g, list[i].CName);
			row = row.replace(/{MarketPrice}/g, list[i].MarketPrice);
			row = row.replace(/{GreatMasterPrice}/g, list[i].GreatMasterPrice);
			row = row.replace(/{Price}/g, list[i].Price);
			row = row.replace(/{Id}/g, list[i].Id);
			html.push(row);
		}

		$("#list").append(html.join(""));
	}
	//获取列表行模板
	function getRowTemplet() {
		var templet =
			'<div class="list-div">' +
				'<img style="float:left;margin-left:5px;" src="{Pic}"></img>' +
				'<div style="float:left;padding-left:5px;">' +
					'<div style="color:#169ada;"><b>{Name}</b></div>' +
					'<div>' +
						'<s style="color:red;">市场价:￥{MarketPrice}</s>' +
						'<s style="color:green;margin-left:5px;">大家价:￥{GreatMasterPrice}</s>' +
					'</div>' +
					'<div><a>实际价:￥{Price}</a></div>' +
				'</div>' +
				'<div style="float:left;width:100%;">' +
					'<div class="list-div-button" onclick="showInfo(\'{Id}\')">详情</div>' +
				'</div>' +
			'</div>';
		return templet;
	}
	//显示错误信息
	function showError(msg) {
		$("#list").html('<div style="padding:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
	}

	function showInfo(id) {
		//跳转到信息页面
		location.href = "info.html?hasbutton=1&id=" + id;
	}
	window.showInfo = showInfo;
	
	//查询数据
	function onSearch(){
		if(PAGE == 1){
			$("#list").html("");
		}
		//获取套餐列表数据
		getPackageData(function(data) {
			if(data.success == true) {
				//更改列表内容
				var list = data.value ? (data.value.list || []) : [];
				if(list.length == 0){
					if(PAGE == 1) {
						showError("没有找到套餐！");
					}
				}else{
					if(list.length < LIMIT) {
						$("#button-loadmore").hide();
					} else {
						$("#button-loadmore").show();
					}
					changeListHtml(list);
				}
			} else {
				if(PAGE == 1){
					$("#button-loadmore").hide();
					showError(data.msg);
				}
			}
		});
	}
	//获取条件
	function getWhere(){
		var searchValue = $("#search-value").val();
		var where = "";
		if(searchValue){
			where = "CName like '%" + searchValue + "%'";
		}
		return where;
	}
	
	$("#search-button").on("click",function(){
		PAGE = 1;//重新定位第一页
		onSearch();
	});
	
	//加载更多按钮处理
	$("#button-loadmore").on("click",function(){
		PAGE++;
		initHtml();
	});
	
	//初始化页面
	function initHtml() {
		//查询数据
		onSearch();
	}
	
	//初始化页面
	initHtml();
});