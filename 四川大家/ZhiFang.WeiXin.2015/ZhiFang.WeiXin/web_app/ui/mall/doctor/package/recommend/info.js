$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//获取套餐信息服务地址
	var PACKAGE_RECOMMENDED_URL = JcallShell.System.Path.ROOT + 
		'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProductById';
	var FIELDS = [
		'OSRecommendationItemProductVO_Id','OSRecommendationItemProductVO_ItemID',
		'OSRecommendationItemProductVO_CName','OSRecommendationItemProductVO_MarketPrice',
		'OSRecommendationItemProductVO_GreatMasterPrice','OSRecommendationItemProductVO_DiscountPrice',
		'OSRecommendationItemProductVO_Pic','OSRecommendationItemProductVO_Memo','OSRecommendationItemProductVO_BonusPercent'
	];
	PACKAGE_RECOMMENDED_URL += '?fields=' + FIELDS.join(',');
	
	var INFO_DATA = null;
    //获取套餐信息
	function getData(callback){
		var url = PACKAGE_RECOMMENDED_URL + '&id=' + params.ID;
		
        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
		JcallShell.Server.ajax({
            url: url
        }, function (data) {
            $("#loading-div").modal("hide");
            callback(data);
        });
	}
	//更改套餐列表内容
	function changeHtml(data){
		var defaultPic = JcallShell.System.Path.UI + '/images/home/top/img1.jpg';
		var html = 
		'<div><img style="width:100%;height:120px;margin-top:10px;" src="' + (data.Pic || defaultPic) +'"></img></div>' +
		'<div style="padding:10px;text-align:center;">' + data.CName +'</div>' +
		'<div style="color:red;"><s>市场价格:￥' + data.MarketPrice +'</s></div>' +
		'<div style="color:green;"><s>大家价格:￥' + data.GreatMasterPrice +'</s></div>' +
		'<div>折扣价格:￥' + data.DiscountPrice +'</div>' +
		'<div style="margin-top:10px;">' + (data.Memo || '') + '</div>';
		
		$("#info").html(html);
	}
	//显示错误信息
    function showError(msg) {
        $("#info").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
    }
    //加入医嘱单
    function addDoctorOrder(){
    	var data = {
    		Id:INFO_DATA.Id,
    		ItemID:INFO_DATA.ItemID,
    		CName:INFO_DATA.CName,
    		MarketPrice:INFO_DATA.MarketPrice,
    		GreatMasterPrice:INFO_DATA.GreatMasterPrice,
    		Price:INFO_DATA.DiscountPrice,
    		Pic:INFO_DATA.Pic,
    		ItemNo:INFO_DATA.ItemNo,
    		BonusPercent:INFO_DATA.BonusPercent,
    		Type:'recommend'
    	};
    	var hasValue = JcallShell.LocalStorage.DoctorOrder.addPackage(data);
    	if(hasValue){
    		$("#showMsgModal-Msg").html("该套餐已经在医嘱单中");
	    	$("#showMsgModal").modal({ backdrop: 'static', keyboard: false });
	    	setTimeout(function(){
	    		$("#showMsgModal").modal("hide");
	    	},2000);
    	}else{
    		location.href = '../../order/create.html';
    	}
    };
    //加入医嘱单
    $("#addDoctorOrder-button").on("click",function(){
    	addDoctorOrder();
    });
    //进入医嘱单
    $("#toDoctorOrder-button").on("click",function(){
    	location.href = '../../order/create.html';
    });
    
    //初始化页面
    function initHtml(){
    	getData(function(data){
			if (data.success == true) {
				INFO_DATA = data.value;
				//更改套餐列表内容
                changeHtml(data.value);
                if(params.HASBUTTON){
					$("#submit-div").show();
				}
            } else {
                showError(data.msg);
            }
		});
    }
    initHtml();
});