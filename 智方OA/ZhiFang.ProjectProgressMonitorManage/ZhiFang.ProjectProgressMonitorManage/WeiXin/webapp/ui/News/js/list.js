$(function () {
	//dictreeid=5592236043558159468&dictreename=行业新闻
	
	//QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeIDAndDictreeids?dictreeids={dictreeids}&page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}
	
	
	//外部传入参数
	var PARAMS = Shell.util.getRequestParams(true);
	//var WEIXIN_URL = Shell.util.Path.rootPath + "/QMSService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID?isSearchChildNode=true";
	var WEIXIN_URL = Shell.util.Path.rootPath + "/QMSService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeIDAndDictreeids?isSearchChildNode=true";
	var WEIXIN_FIELDS = ['FFile_Id','FFile_WeiXinTitle','FFile_IsSyncWeiXin','FFile_PublisherDateTime','FFile_ThumbnailsPath','FFile_WeiXinUrl'];
	WEIXIN_URL += '&fields=' + WEIXIN_FIELDS.join(",") + '&dictreeids='  + PARAMS.DICTREEID;;
	
	//当前页数
	var page = 1;
	//每页数量
	var pageSize = 10;
	//查询值
	var searchText = '';
	//当前页数据
	var NowPageData = null;
	
	//显示蒙版
	function onShowMask() {
		$("#MaskWin").show();
		$("#MaskText").show(); 
    }
	//隐藏蒙版
    function onHideMask() {
    	$("#MaskWin").hide();
		$("#MaskText").hide();
    }
	//查询数据
	function onSearch(){
		var url = WEIXIN_URL;
		
		var value = $("#SearchInput").val();
		if(value != searchText){
			page = 1;//定位到第一页
			searchText = value;
		}
		
		url += '&page=' + page + '&limit=' + pageSize;
		
		if(searchText){
			url += "&where=ffile.WeiXinTitle like '%" + searchText + "%'";
		}
		url = encodeURI(url);
		onLoadData(url,function(){
			//追加列表HTML
			InsertListHtml();
		});
	}
	//加载数据
	function onLoadData(url,callback){
		NowPageData = null;
		//显示蒙版
    	onShowMask();
    	
		Shell.util.Server.ajax({
    		showError:true,
    		url:url
    	},function(data){
    		//隐藏蒙版
    		onHideMask();
    		
    		if(data.success){
    			var value = data.value || {};
    			NowPageData = value.list;
    		}
    		callback();
    	});
	}
	//追加列表HTML
	function InsertListHtml(){
		var list = NowPageData || [],
			len = list.length,
			html = [];
			
		for(var i=0;i<len;i++){
			html.push(createRowHtml(list[i]));
		}
		
		if(page == 1){
			$("#Content").html("");
			//没有获取到数据
			if(html.length == 0){
				html.push(getNoDataHtml());
			}
		}
		$("#Content").append(html.join(""));
		$("#LoadButtonDiv").show();
	}
	//创建行HTML
	function createRowHtml(data){
		var templet = getRowTemplet();
		var src = Shell.util.Path.rootPath + '/' + data.ThumbnailsPath;
		templet = templet.replace(/{ThumbnailsPath}/g,src);
		templet = templet.replace(/{WeiXinTitle}/g,data.WeiXinTitle);
		templet = templet.replace(/{IsSyncWeiXin}/g,data.IsSyncWeiXin);
		templet = templet.replace(/{Id}/g,data.Id);
		templet = templet.replace(/{WeiXinUrl}/g,data.WeiXinUrl);
		
		return templet;
	}
	//模板
	function getRowTemplet(){
		var templet = 
			'<div class="Row" onclick="onRowClick({IsSyncWeiXin},\'{Id}\',\'{WeiXinUrl}\');">' +
				'<img src="{ThumbnailsPath}">' +
				'<span>{WeiXinTitle}</span>' +
			'</div>';
		return templet;
	}
	//没有数据的提示
	function getNoDataHtml(){
		return '<div class="NoDataDiv">没有获取到数据</div>';
	}
	//行点击事件
	function onRowClick(IsSyncWeiXin,Id,WeiXinUrl){
		if(IsSyncWeiXin){
			window.location.href = WeiXinUrl;
		}else{
			window.location.href = 'info.html?id=' + Id;
		}
	}
	
    //初始化页面
    function initHtml(){
    	//查询按钮监听
    	$("#SearchButton").on("click",function(){
    		page = 1;
    		$("#LoadButtonDiv").hide();
    		onSearch();
    	});
    	//加载更多按钮监听
    	$("#LoadButtonDiv").on("click",function(){
    		page++;
    		onSearch();
    	});
    	//查询内容初始化
    	$("#SearchInput").val(PARAMS.DICTREENAME);
    	onSearch();
    }
    
    window.onSearch = onSearch;
    window.onRowClick = onRowClick;
    
    //初始化页面
    initHtml();
});