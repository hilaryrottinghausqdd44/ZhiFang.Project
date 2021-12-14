$(function() {
	//加载订货单信息服务
	var LOAD_ORDER_DOC_URL = Shell.util.Path.ROOT + "/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById";
	//加载订货单明细服务
	var LOAD_ORDER_DTL_URL = Shell.util.Path.ROOT + "/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL";
	//更新订货单信息服务
	var UPDATE_ORDER_DOC_URL = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_BmsCenSaleDocByOrderDoc'
	//实验室消息推送服务
    var ON_LAB_PUSH_MESSAGE_URL = Shell.util.Path.ROOT + "/WeiXinAppService.svc/PushConfirmBmsCenOrderDoc";
	
	//默认类型:已提交待确认1,已确认待发货2
	var TYPE = null;
	//订单主键
	var DOC_ID = null;
	var LOCAL_DATA = {
		DocInfo:{},//主单信息
		DtlList:{}//明细内容{count:0,list:[]}
	};
	//总价
	var TOTAL_PRICE = 0;
	
	//初始化参数
	function initParamsData(){
		var params = Shell.util.getRequestParams(true);
		TYPE = params.TYPE || "";
		DOC_ID = params.ID;
	}
	//加载供货单信息数据
	function loadInfoData(callback){
		var url = LOAD_ORDER_DOC_URL + '?isPlanish=true&id=' + DOC_ID;
		var fields = [
			'BmsCenOrderDoc_Id','BmsCenOrderDoc_SaleDocNo','BmsCenOrderDoc_Lab_CName','BmsCenOrderDoc_Comp_CName',
			'BmsCenOrderDoc_UrgentFlag','BmsCenOrderDoc_Status','BmsCenOrderDoc_Lab_Id','BmsCenOrderDoc_Comp_Id',
			'BmsCenOrderDoc_ReqDeliveryTime','BmsCenOrderDoc_CompMemo','BmsCenOrderDoc_LabMemo','BmsCenOrderDoc_TotalPrice'
		];
		url += '&fields=' + fields.join(',');
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	//加载供货单明细列表数据
	function loadDtlData(callback){
		var url = LOAD_ORDER_DTL_URL;
		var fields = [
			'BmsCenOrderDtl_Id','BmsCenOrderDtl_GoodsName','BmsCenOrderDtl_ProdGoodsNo',
			'BmsCenOrderDtl_GoodsQty','BmsCenOrderDtl_Price','BmsCenOrderDtl_ProdOrgName',
			'BmsCenOrderDtl_GoodsUnit','BmsCenOrderDtl_UnitMemo',
			'BmsCenOrderDtl_Goods_GoodsClass','BmsCenOrderDtl_Goods_GoodsClassType'
		];
		url += '?isPlanish=true&fields=' + fields.join(',');
		url += '&where=bmscenorderdtl.BmsCenOrderDoc.Id=' + DOC_ID
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	
	//更新订单状态
	function updateStatus(id,status,callback){
		var data = {
			entity:{
				Id:id,
				Status:status
			},
			fields:"Id,Status"
		};
		
		//供应商备注
		var CompMemo = $("#CompMemo");
		if(CompMemo){
			var CompMemoValue = CompMemo.val();
			data.entity.CompMemo = CompMemoValue;
			data.fields += ",CompMemo";
		}
		data = JSON.stringify(data);
		
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			type:'post',
			url: UPDATE_ORDER_DOC_URL,
			data:data
		}, function(data){
			ShellComponent.mask.hide();
			if(data.success){
				callback();
			}else{
				ShellComponent.messagebox.msg(data.msg);
			}
		});
	}
	//实验室消息推送服务
	function onLabPushMessage(id,callback){
		var me = this,
			url = ON_LAB_PUSH_MESSAGE_URL + '?Id=' + id;
		
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			if(data.success){
				callback();
			}else{
				ShellComponent.messagebox.msg(data.msg);
			}
		});
	}
	
	//创建供货单信息内容
	function createInfo(data){
		var html = [];
			
		html.push('<div style="padding:10px;border:1px solid #eee;margin-bottom:10px;border-radius: 2px;">');
		
		html.push('<div class="list-group-item-text">供货方: ' + data.BmsCenOrderDoc_Comp_CName + '</div>');
		html.push('<div class="list-group-item-text">订货方: ' + data.BmsCenOrderDoc_Lab_CName + '</div>');
		html.push('<div class="list-group-item-text">要求送货时间: ' + 
			(Shell.util.Date.toString(data.BmsCenOrderDoc_ReqDeliveryTime,true) || '')+ '</div>');
		html.push('<div class="list-group-item-text">供货方备注: ' + (data.BmsCenOrderDoc_CompMemo || '') + '</div>');
		html.push('<div class="list-group-item-text">订货方备注: ' + (data.BmsCenOrderDoc_LabMemo || '') + '</div>');
		html.push('<div class="list-group-item-text">提交时间：' + 
				(Shell.util.Date.toString(data.BmsCenOrderDoc_OperDate) || '') + '</div>');
		
		html.push('<div style="height:25px;">');
		//紧急程度
		var UrgentFlag = Shell.Rea.Enum.BmsCenSaleDoc_UrgentFlag['E' + data.BmsCenOrderDoc_UrgentFlag];
		html.push('<div class="enum-div" style="float:left;background-color:' + UrgentFlag.bcolor + 
			';color:' + UrgentFlag.color + ';">' + UrgentFlag.value +'</div>');
		//订单状态
		var Status = Shell.Rea.Enum.BmsCenSaleDoc_Status['E' + data.BmsCenOrderDoc_Status];
		html.push('<div class="enum-div" style="float:left;background-color:' + Status.bcolor + 
			';color:' + Status.color + ';">' + Status.value +'</div>');
		//总价
//		html.push('<div style="float:left;margin:2px 5px 2px 10px;">总价:<span style="color:#d9534f;font-weight:bold;">' + 
//			data.BmsCenOrderDoc_TotalPrice + '</span>元</div>');
//		html.push('</div>');
		html.push('<div style="float:left;margin:2px 5px 2px 10px;">总价:<span style="color:#d9534f;font-weight:bold;">' + 
			TOTAL_PRICE.toFixed(2) + '</span>元</div>');
		html.push('</div>');
			
		html.push('</div>');
		
		return html.join('');
	}
	//创建供货单详细列表内容
	function createDtl(data){
		var list = data.list || [],
			len = list.length,
			html = [];
			
		html.push('<div>');
		for(var i=0;i<len;i++){
			var row = createRow(list[i]);
			html.push(row);
		}
		if(len == 0){
			html.push('<div class="no-data-div">没有找到数据!</div>');//没有数据
		}
		html.push('</div>');
		return html.join('');
	}
	//创建数据行内容
	function createRow(value){
		var html = [];
		
		//供货单号+订货方+紧急标志+单据状态+提取标志
		//SaleDocNo+Lab_CName+UrgentFlag+Status+IOFlag+UserName+OperDate
		
		var id = value.Goods_Id;
		var title = value.BmsCenOrderDtl_GoodsName;
		
		var row0 = '<span style="color:#d9534f;font-weight:bold;">规格：( ' + value.BmsCenOrderDtl_UnitMemo + ' )';
		var row1 = '<span style="color:#666666;">品牌：' + value.BmsCenOrderDtl_ProdOrgName + '</span>';
		var row2 = value.BmsCenOrderDtl_Goods_GoodsClass + ' - ' + value.BmsCenOrderDtl_Goods_GoodsClassType;
		var TotalPrice = (parseFloat(value.BmsCenOrderDtl_GoodsQty) * parseFloat(value.BmsCenOrderDtl_Price)).toFixed(2);
		
		html.push('<div class="list-group-item" style="padding:0;" data="' + id + '">');
		html.push('<div class="list-group-item-content">');
		html.push('<h4 class="list-group-item-heading">' + title + '</h4>');
		html.push('<div class="list-group-item-text">' + row0 + '</div>');
		html.push('<div class="list-group-item-text touch">' + row1 + '</div>');
		
		if(value.BmsCenOrderDtl_Goods_GoodsClass && value.BmsCenOrderDtl_Goods_GoodsClassType){
			html.push('<div class="list-group-item-text">' + row2 + '</div>');
		}
		
		//数量+单价+总价
		html.push('<div class="list-group-item-text">');
		html.push('数量:<span style="color:#d9534f;font-weight:bold;">' +
			value.BmsCenOrderDtl_GoodsQty + '</span>' + value.BmsCenOrderDtl_GoodsUnit);
		html.push('&nbsp;&nbsp;单价:<span style="color:#d9534f;font-weight:bold;">' +
			value.BmsCenOrderDtl_Price + '</span>元');
		html.push('&nbsp;&nbsp;总价:<span style="color:#d9534f;font-weight:bold;">' +
			TotalPrice + '</span>元');
		html.push('</div>');
//		//数量
//		html.push('<div style="float:left;margin:2px 5px 2px 10px;">数量:<span style="color:#d9534f;font-weight:bold;">' + 
//			value.BmsCenOrderDtl_GoodsQty + '</span>' + value.BmsCenOrderDtl_GoodsUnit + '</div>');
//		//单价
//		html.push('<div style="float:left;margin:2px 5px 2px 10px;">单价:<span style="color:#d9534f;font-weight:bold;">' + 
//			value.BmsCenOrderDtl_Price + '</span>元</div>');
//		//总价
//		html.push('<div style="float:left;margin:2px 5px 2px 10px;">总价:<span style="color:#d9534f;font-weight:bold;">' + 
//			TotalPrice + '</span>元</div>');
		
		html.push('</div>');//end list-group-item-content
		
		html.push('</div>');
		
		return html.join('');
	}
	
	//初始化页面内容
	function initContentDiv(){
		var html = [];
		
		html.push('<div style="margin:5px;">');
		//主单内容
		html.push(createInfo(LOCAL_DATA.DocInfo));
		//明细内容
		html.push(createDtl(LOCAL_DATA.DtlList));
		
		if(TYPE == "1"){//已提交待确认1
			if((LOCAL_DATA.DocInfo.BmsCenOrderDoc_Status + "") == "1"){
				html.push('<textarea id="CompMemo" class="form-control" rows="3" placeholder="备注" style="margin-top:20px;">' + 
					(LOCAL_DATA.DocInfo.BmsCenOrderDoc_CompMemo || '') + '</textarea>');
				html.push('<div class="button-div" style="margin:40px 20px;text-align:center;">');
				html.push('<button id="save" style="width:160px;">确认</button>');
				html.push('</div>');
			}
		}else if(TYPE == "2"){//已确认待发货2
			if((LOCAL_DATA.DocInfo.BmsCenOrderDoc_Status + "") == "2"){
				html.push('<div class="button-div" style="margin:40px 20px;text-align:center;">');
				html.push('<button id="save" style="width:160px;">发货</button>');
				html.push('</div>');
			}
		}
		
		html.push('</div>');
		
		$("#ContentDiv").html(html.join(""));
		//初始化监听
		initListeners();
	}
	//初始化监听
	function initListeners(){
		$("#save").on(Shell.util.Event.click,function(){
			$("#InfoModal").modal({ backdrop: 'static', keyboard: false });
//			if(TYPE == "1"){//确认
//				updateStatus(DOC_ID,"2",function(){
//					location.href = "list_comp.html?type=" + 2;
//				});
//			}else if(TYPE == "2"){//发货
//				updateStatus(DOC_ID,"3",function(){
//					location.href = "list_comp.html?type=" + 3;
//				});
//			}
		});
	}
	
	//刷新列表数据
	function refreshContent(){
		//加载供货单信息数据
		loadInfoData(function(data){
			if(data.success){
				LOCAL_DATA.DocInfo = data.value || {};
				//加载供货单明细列表数据
				loadDtlData(function(value){
					if(value.success){
						LOCAL_DATA.DtlList = value.value || {};
						//数据处理
						changeData();
						//初始化页面内容
						initContentDiv();
					}else{
						$("#ContentDiv").html('<div class="error-div">' + data.msg + '</div>');
					}
				});
			}else{
				$("#ContentDiv").html('<div class="error-div"">' + data.msg + '</div>');
			}
		});
		
	}
	//数据处理
	function changeData(){
		var list = LOCAL_DATA.DtlList.list || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
			var value = list[i];
			var TotalPrice = parseFloat(value.BmsCenOrderDtl_GoodsQty) * parseFloat(value.BmsCenOrderDtl_Price);
			TOTAL_PRICE += TotalPrice;
		}
	}
	
	//确定操作
	$("#ok-button").on("click",function(){
		if(TYPE == "1"){//确认
			updateStatus(DOC_ID,"2",function(){
				onLabPushMessage(DOC_ID,function(){
					location.href = "list_comp.html?type=" + 2;
				});
			});
		}else if(TYPE == "2"){//发货
			updateStatus(DOC_ID,"3",function(){
				location.href = "list_comp.html?type=" + 3;
			});
		}
	});
	
	//初始化参数
	initParamsData();
	setTimeout(function(){
		refreshContent();
	},500);
});