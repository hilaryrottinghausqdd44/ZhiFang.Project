$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//获取医嘱单信息服务地址
	var DOCTOR_ORDER_INFO_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderFormById";
	//获取医嘱单明细服务地址
	var DOCTOR_ORDER_ITEMS_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderItemByHQL";
	var FIELDS = [
		'Id','HospitalName','DeptName','DoctorName','UserName',
		'Age','AgeUnitName','SexName','PatNo','Status','DataAddTime'
	];
	DOCTOR_ORDER_INFO_URL += '?fields=OSDoctorOrderForm_' + FIELDS.join(',OSDoctorOrderForm_');
	
	//医嘱单数据
	var INFO_DATA = null;
	//医嘱单状态信息
	var STATUS_INFO = null;
	//市场价格-总和
	var MARKET_PRICE_COUNT = 0;
	//大家价格-总和
	var GREAT_MASTER_PRICE_COUNT = 0;
	//折扣价格-总和
	var DISCOUNT_PRICE_COUNT = 0;
	
	//获取医嘱单内容
	function getInfoData(callback){
		var url = DOCTOR_ORDER_INFO_URL + '&id=' + params.ID;
        JcallShell.Server.ajax({
        	showError:true,
            url: url
        }, function (data) {
            callback(data);
        });
	}
	//获取医嘱单明细内容
	function getItemsData(callback){
		var url = DOCTOR_ORDER_ITEMS_URL + '?where=osdoctororderitem.DOFID=' + params.ID;
		var fields = ['MarketPrice','GreatMasterPrice','DiscountPrice','ItemCName'];
		url += '&fields=OSDoctorOrderItem_' + fields.join(',OSDoctorOrderItem_');
		
        JcallShell.Server.ajax({
        	showError:true,
            url: url
        }, function (data) {
            callback(data);
        });
	}
	
	//显示错误信息
    function showError(msg) {
        $("#info").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
    }
    
	//初始化内容
	function initContent(){
		var info = getInfoHtml(INFO_DATA);
		var items = getItemsHtml(INFO_DATA.items);
		
		$("#info").html(info + items);
	}
	//更改医嘱单内容
	function getInfoHtml(data){
		var html = getInfoTemplet();
		html = html.replace(/{Hospital}/g, data.HospitalName || "");
		html = html.replace(/{Dept}/g, data.DeptName || "");
		html = html.replace(/{Doctor}/g, data.DoctorName || "");
		html = html.replace(/{Patient}/g, data.UserName || "");
		html = html.replace(/{Sex}/g, data.SexName || "");
		html = html.replace(/{Age}/g, data.Age + ""  || "");
		html = html.replace(/{AgeUnit}/g, data.AgeUnitName || "");
		html = html.replace(/{PatNo}/g, data.PatNo || "");
		//html = html.replace(/{Status}/g, data.Status);
		html = html.replace(/{DataAddTime}/g, data.DataAddTime || "");
		
		html = html.replace(/{StatusBgColor}/g, STATUS_INFO.BGColor || "");
		html = html.replace(/{StatusColor}/g, STATUS_INFO.BGColor || "");
		html = html.replace(/{StatusName}/g, STATUS_INFO.Name || "");
		
		html = html.replace(/{MarketPriceCount}/g, MARKET_PRICE_COUNT);
		html = html.replace(/{GreatMasterPriceCount}/g, GREAT_MASTER_PRICE_COUNT);
		html = html.replace(/{DiscountPriceCount}/g, DISCOUNT_PRICE_COUNT);
		
		return html;
	}
	//获取医嘱单内容模板
	function getInfoTemplet() {
		var templet =
			'<div style="padding:5px 0;">医院：{Hospital}</div>' +
			'<div style="padding:5px 0;">科室：{Dept}</div>' +
			'<div style="padding:5px 0;">医生：{Doctor}</div>' +
			'<div style="padding:5px 0;">患者：{Patient} {Sex} {Age}{AgeUnit}</div>' +
			'<div style="padding:5px 0;">病历号：{PatNo}</div>' +
			//'<div style="padding:5px 0;">状态：{Status}</div>' +
			'<div style="padding:5px 0;">开单时间：{DataAddTime}</div>' +
			//'<div style="padding:5px 0;text-align:center;font-size:18px;border-top:1px solid #e0e0e0;"><b>总价：￥200.00元</b></div>';
			'<div style="padding:5px 0;">' +
    			'<span style="text-align:center;padding:5px 10px;text-align:center;border:1px solid {StatusBgColor};color:{StatusColor}">状态：{StatusName}</span>' +
    		'</div>' +
    		'<div style="padding:5px 0;">' +
    			'<span style="text-align:center;">总价:￥<b style="color:red;">{GreatMasterPriceCount}</b>元</span>' +
    		'</div>' +
    		'<div style="padding:5px 0;">' +
    			'<span style="text-align:center;color:#e0e0e0;"><s>市场价:￥<b>{MarketPriceCount}</b>元</s></span>' +
    		'</div>';
			
		return templet;
	}
	
	//更改医嘱单明细
	function getItemsHtml(list){
		var len = (list || []).length,
			html = [];
			
		html.push('<table class="basic_table" style="margin:10px 0;">');
		html.push('<thead><th>套餐名称</th><th>市场价格</th><th>大家价格</th><th>折扣价格</th></thead>');
		html.push('<tbody>');
		for(var i=0;i<len;i++){
			var item = getItemTemplet();
			var data = list[i];
			item = item.replace(/{ItemName}/g, data.ItemCName || "");
			item = item.replace(/{MarketPrice}/g, data.MarketPrice);
			item = item.replace(/{GreatMasterPrice}/g, data.GreatMasterPrice);
			item = item.replace(/{DiscountPrice}/g, data.DiscountPrice);
			html.push(item);
		}
		html.push('</tbody>');
		html.push('</table>');
		
		return html.join("");
	}
	//获取医嘱单明细模板
	function getItemTemplet() {
		//市场价格,大家价格,折扣价格
		var templet =
			'<tr>' + 
				'<td>{ItemName}</td>' +
				'<td style="width:50px;">{MarketPrice}</td>' +
				'<td style="width:50px;">{GreatMasterPrice}</td>' +
				'<td style="width:50px;">{DiscountPrice}</td>' +
			'</tr>';
			
		return templet;
	}
	
	
	//初始化数据
	function initData(callback){
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		//获取医嘱单数据
		getInfoData(function(data){
			if (data.success == true) {
				INFO_DATA = data.value || {};//医嘱单数据
				//获取医嘱单状态信息
				JcallShell.LocalStorage.Enum.getInfoById('DoctorOrderFormStatus',INFO_DATA.Status,function(info){
					STATUS_INFO = info;//医嘱单状态信息
					//获取明细数据
	                getItemsData(function(data){
						if (data.success == true) {
							INFO_DATA.items = (data.value || {}).list || [];
							$("#loading-div").modal("hide");
							callback();
			            } else {
			            	$("#loading-div").modal("hide");
			                showError(data.msg);
			            }
					});
				});
            } else {
            	$("#loading-div").modal("hide");
                showError(data.msg);
            }
		});
	}
	function changeData(){
		//总价格计算
		var list = INFO_DATA.items || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
    		MARKET_PRICE_COUNT += parseFloat(list[i].MarketPrice);
    		GREAT_MASTER_PRICE_COUNT += parseFloat(list[i].GreatMasterPrice);
    		DISCOUNT_PRICE_COUNT += parseFloat(list[i].DiscountPrice);
    	}
	}
	
	//初始化页面
	function initHtml(){
		initData(function(){
			//是否显示"医嘱单确认"按钮
			var status = INFO_DATA.Status + "";
			if(status == "1" || status == "2" || status == "3" || status == "4"){
				$("#submit-div").show();
			}
			//数据处理
			changeData();
			//初始化内容
			initContent();
		});
	}
	initHtml();
});