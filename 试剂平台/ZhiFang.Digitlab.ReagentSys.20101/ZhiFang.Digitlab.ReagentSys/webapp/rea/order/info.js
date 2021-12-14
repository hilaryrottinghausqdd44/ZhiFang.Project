$(function() {
	//订单主键
	var DOC_ID = Shell.util.getRequestParams(true).ID;
	var LOCAL_DATA = {
		DocInfo:{},//主单信息
		DtlList:{}//明细内容{count:0,list:[]}
	};
	//总价
	var TOTAL_PRICE = 0;
	
	//加载订货单信息数据
	function loadInfoData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById?isPlanish=true';
		url += '&id=' + DOC_ID;
		var fields = [
			'BmsCenOrderDoc_Id','BmsCenOrderDoc_SaleDocNo','BmsCenOrderDoc_Lab_CName','BmsCenOrderDoc_Comp_CName',
			'BmsCenOrderDoc_UrgentFlag','BmsCenOrderDoc_Status','BmsCenOrderDoc_Lab_Id','BmsCenOrderDoc_Comp_Id',
			'BmsCenOrderDoc_ReqDeliveryTime','BmsCenOrderDoc_LabMemo','BmsCenOrderDoc_TotalPrice','BmsCenOrderDoc_OperDate'
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
	//加载订货单明细列表数据
	function loadDtlData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL?isPlanish=true';
		var fields = [
			'BmsCenOrderDtl_Id','BmsCenOrderDtl_GoodsName','BmsCenOrderDtl_ProdGoodsNo',
			'BmsCenOrderDtl_GoodsQty','BmsCenOrderDtl_Price','BmsCenOrderDtl_ProdOrgName',
			'BmsCenOrderDtl_GoodsUnit','BmsCenOrderDtl_UnitMemo',
			'BmsCenOrderDtl_Goods_GoodsClass','BmsCenOrderDtl_Goods_GoodsClassType'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=bmscenorderdtl.BmsCenOrderDoc.Id=' + DOC_ID
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	
	//创建订货单信息内容
	function createInfo(data){
		var html = [];
			
		html.push('<div style="padding:10px;border:1px solid #eee;margin-bottom:10px;border-radius: 2px;">');
		
		html.push('<div class="list-group-item-text" style="font-size:13px;">供货方: ' + data.BmsCenOrderDoc_Comp_CName + '</div>');
		html.push('<div class="list-group-item-text" style="font-size:13px;">订货方: ' + data.BmsCenOrderDoc_Lab_CName + '</div>');
		html.push('<div class="list-group-item-text" style="font-size:13px;">要求送货时间: ' + 
			(Shell.util.Date.toString(data.BmsCenOrderDoc_ReqDeliveryTime,true) || '') + '</div>');
		html.push('<div class="list-group-item-text" style="font-size:13px;">供货方备注: ' + (data.BmsCenOrderDoc_CompMemo || '') + '</div>');
		html.push('<div class="list-group-item-text" style="font-size:13px;">订货方备注: ' + (data.BmsCenOrderDoc_LabMemo || '') + '</div>');
		html.push('<div class="list-group-item-text" style="font-size:13px;">提交时间：' + 
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
		html.push('<div style="float:left;margin:2px 5px 2px 10px;">总价:<span style="color:#d9534f;font-weight:bold;">' + 
			TOTAL_PRICE.toFixed(2) + '</span>元</div>');
		html.push('</div>');
			
		html.push('</div>');
		
		return html.join('');
	}
	//创建订货单详细列表内容
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
	function createRow2(value){
		var html = [];
		
		var id = value.BmsCenOrderDoc_Id;
		var title = '产品名称:' + value.BmsCenOrderDtl_GoodsName + '';
		var GoodsNo = '编号:' + value.BmsCenOrderDtl_ProdGoodsNo;
		var InvalidDate = '有效期至:' + Shell.util.Date.toString(value.BmsCenOrderDtl_InvalidDate,true);
		var UnitMemo = '规格:' + value.BmsCenOrderDtl_UnitMemo;
		
		var invalidTime = Shell.util.Date.getDate(value.BmsCenOrderDtl_InvalidDate);
		if(invalidTime){
			invalidTime = invalidTime.getTime();
			var newTime = new Date().getTime();
			var num = invalidTime - newTime;
			
			var times = 10 * 24 * 3600 * 1000;
			if(num < 0){
				InvalidDate = '<span style="color:#d9534f">' + InvalidDate + '<span>';
			}else if(num < times){
				InvalidDate = '<span style="color:#f0ad4e">' + InvalidDate + '<span>';
			}else{
				InvalidDate = '<span style="color:#5cb85c">' + InvalidDate + '<span>';
			}
		}
		
		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<a class="list-group-item" data="' + id + '">');
		html.push('<h4 class="list-group-item-heading">' + title + '</h4>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + GoodsNo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UnitMemo + '</div>');
		
		html.push('<div style="padding:2px;margin:2px;margin-top:-75px;width:80px;background-color:#f5f5f5;float:right;font-weight:bold;text-align:center;">');
		html.push('<div style="color:#5cb85c">' + value.BmsCenOrderDtl_GoodsQty + '</div>');//数量
		html.push('<div style="">' + value.BmsCenOrderDtl_GoodsUnit + '</div>');//单位
		html.push('</div>');
		html.push('</a>');
		return html.join('');
	}
	//创建数据行内容
	function createRow(value){
		var html = [];
		
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
		html.push('</div>');
		
		$("#ContentDiv").html(html.join(""));
	}
	
	//刷新列表数据
	function refreshContent(){
		//加载订货单信息数据
		loadInfoData(function(data){
			if(data.success){
				LOCAL_DATA.DocInfo = data.value || {};
				if(!data.value){
					$("#ContentDiv").html('<div class="error-div">没有找到数据！</div>');
					return;
				}
				//加载订货单明细列表数据
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
	
	setTimeout(function(){
		refreshContent();
	},500);
});