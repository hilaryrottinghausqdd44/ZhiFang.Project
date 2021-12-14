$(function() {
	//获取特推套餐服务地址
	var PACKAGE_RECOMMENDED_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchOSRecommendationItemByAreaID";
	
	//当前页码
	var PAGE = 1;
	//每页数量
	var LIMIT = 20;
	
	//获取特推套餐数据
	function getPackageData(callback) {
		var url = PACKAGE_RECOMMENDED_URL;
		url += '?page=' + PAGE + '&limit=' + LIMIT + '&where=' + getWhere();
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
		//VO:ZhiFang.WeiXin.Entity.ViewObject.Response.BLabTestItemVO
		for(var i in list) {
			var row = templet;
			row = row.replace(/{Pic}/g, list[i].Pic || defaultPic);
			row = row.replace(/{Name}/g, list[i].CName);
			row = row.replace(/{MarketPrice}/g, list[i].MarketPrice);
			row = row.replace(/{GreatMasterPrice}/g, list[i].GreatMasterPrice);
			row = row.replace(/{DiscountPrice}/g, list[i].DiscountPrice);
			row = row.replace(/{Id}/g, list[i].Id);
			row = row.replace(/{ItemID}/g, list[i].ItemID);
			row = row.replace(/{ItemNo}/g, list[i].ItemNo);
			row = row.replace(/{BonusPercent}/g, list[i].BonusPercent);
			html.push(row);
		}

		$("#list").append(html.join(""));
	}
	//获取列表行模板
	function getRowTemplet() {
		var templet =
			'<div class="list-div">' +
				'<img style="float:left;margin-left:5px;" src="{Pic}" onclick="showInfo(\'{Id}\')"></img>' +
				'<div style="float:left;padding-left:5px;">' +
					'<div style="color:#169ada;"><b>{Name}</b></div>' +
					'<div>' +
						'<s style="color:red;">市场价:￥{MarketPrice}</s>' +
						'<s style="color:green;margin-left:5px;">大家价:￥{GreatMasterPrice}</s>' +
					'</div>' +
					'<div><a>实际价:￥{DiscountPrice}</a></div>' +
				'</div>' +
				'<div style="float:left;width:100%;">' +
					'<div class="list-div-button" onclick="addDoctorOrder(\'{Id}\',\'{ItemID}\',\'{Name}\',\'{MarketPrice}\',' +
						'\'{GreatMasterPrice}\',\'{DiscountPrice}\',\'{Pic}\',\'{ItemNo}\',\'{BonusPercent}\')">加入</div>' +
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
	//加入医嘱单
    function addDoctorOrder(Id,ItemID,CName,MarketPrice,GreatMasterPrice,DiscountPrice,Pic,ItemNo,BonusPercent){
    	var data = {
    		Id:Id,
    		ItemID:ItemID,
    		CName:CName,
    		MarketPrice:MarketPrice,
    		GreatMasterPrice:GreatMasterPrice,
    		Price:DiscountPrice,
    		Pic:Pic,
    		ItemNo:ItemNo,
    		BonusPercent:BonusPercent,
    		Type:'recommend'
    	};
    	var hasValue = JcallShell.LocalStorage.DoctorOrder.addPackage(data);
    	if(hasValue){
	    	$.toptip('该套餐已经在医嘱单中', 'error');
    	}else{
    		changeOrderInfo();//更改医嘱单信息
	    	$.toptip('加入成功', 'success');
    	}
    };
	window.addDoctorOrder = addDoctorOrder;
	//更改医嘱单信息
	function changeOrderInfo(){
		var list = JcallShell.LocalStorage.DoctorOrder.getPackageList();
		var count = list.length;
		var html = '<a>共</a>&nbsp;&nbsp;<b style="color:red;">' + count + '</b>&nbsp;&nbsp;<a>个套餐</a>';
		$("#orderInfo").html(html);
	}
	//查询数据
	function onSearch(){
		if(PAGE == 1){
			$("#list").html("");
		}
		//获取报告列表数据
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
	//返回医嘱单
	$("#backtoorder-button").on("click",function(){
		location.href = '../../order/create.html';
	});
	//初始化页面
	function initHtml() {
		//查询数据
		onSearch();
		changeOrderInfo();//更改医嘱单信息
	}
	
	//初始化页面
	initHtml();
});