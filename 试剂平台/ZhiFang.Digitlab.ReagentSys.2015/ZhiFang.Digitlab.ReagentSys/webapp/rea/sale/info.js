$(function() {
	var id = Shell.util.getRequestParams().id;
	
	//加载供货单信息数据
	function loadInfoData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocById?isPlanish=true';
		url += '&id=' + id;
		var fields = [
			'BmsCenSaleDoc_Id','BmsCenSaleDoc_SaleDocNo','BmsCenSaleDoc_Lab_CName',
			'BmsCenSaleDoc_UrgentFlag','BmsCenSaleDoc_Status','BmsCenSaleDoc_IOFlag',
			'BmsCenSaleDoc_UserName','BmsCenSaleDoc_OperDate'
		];
		url += '&fields=' + fields.join(',');
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async:false,
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	//加载供货单明细列表数据
	function loadDtlData(callback){
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true';
		var fields = [
			'BmsCenSaleDtl_Id','BmsCenSaleDtl_ProdGoodsNo','BmsCenSaleDtl_GoodsName',
			'BmsCenSaleDtl_LotNo','BmsCenSaleDtl_InvalidDate','BmsCenSaleDtl_GoodsQty',
			'BmsCenSaleDtl_Price','BmsCenSaleDtl_SumTotal','BmsCenSaleDtl_GoodsUnit',
			'BmsCenSaleDtl_UnitMemo'
		];
		url += '&fields=' + fields.join(',');
		url += '&where=bmscensaledtl.BmsCenSaleDoc.Id=' + id
			
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async:false,
			url: url
		}, function(data){
			ShellComponent.mask.hide();
			callback(data);
		});
	}
	
	//创建供货单信息内容
	function createInfo(data){
		var html = [];
			
		//错误信息
		if(data.msg){
			html.push('<div style="color:red;text-align:center;">' + data.msg + '</div>');
		}else{
			html.push('<div style="border-bottom: 1px solid #eee;margin-bottom:10px;padding-bottom:5px;">');
			var pStyle = 'margin:2px;font-size:12px;';
			html.push('<div style="' + pStyle + '">订货方: ' + data.BmsCenSaleDoc_Lab_CName + '</div>');
			html.push('<div style="' + pStyle + '">供货单号: ' + data.BmsCenSaleDoc_SaleDocNo + '</div>');
			html.push('<div style="' + pStyle + '"">操作人员: ' + data.BmsCenSaleDoc_UserName + '</div>');
			html.push('<div style="' + pStyle + '"">操作时间: ' + data.BmsCenSaleDoc_OperDate + '</div>');
			
			//状态标识
			var UrgentFlag = Shell.Rea.Enum.BmsCenSaleDoc_UrgentFlag['E' + data.BmsCenSaleDoc_UrgentFlag];
			var Status = Shell.Rea.Enum.BmsCenSaleDoc_Status['E' + data.BmsCenSaleDoc_Status];
			var IOFlag = Shell.Rea.Enum.BmsCenSaleDoc_IOFlag['E' + data.BmsCenSaleDoc_IOFlag];
			html.push('<div style="float:right;margin-top:-75px;">');
			var divStyle = 'width:60px;text-align:center;padding:2px;margin:2px;font-size:11px;';
			html.push('<div style="' + divStyle + 'background-color:' + UrgentFlag.bcolor + 
				';color:' + UrgentFlag.color + ';">' + UrgentFlag.value +'</div>');
			html.push('<div style="' + divStyle + 'background-color:' + Status.bcolor + 
				';color:' + Status.color + ';">' + Status.value +'</div>');
			html.push('<div style="' + divStyle + 'background-color:' + IOFlag.bcolor + 
				';color:' + IOFlag.color + ';">' + IOFlag.value +'</div>');
			html.push('</div>');
			html.push('</div>');
		}
		
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
		
		var id = value.BmsCenSaleDoc_Id;
		var title = '产品名称:' + value.BmsCenSaleDtl_GoodsName + '';
		var GoodsNo = '编号:' + value.BmsCenSaleDtl_ProdGoodsNo;
		var LotNo = '批号:' + value.BmsCenSaleDtl_LotNo;
		var InvalidDate = '有效期至:' + Shell.util.Date.toString(value.BmsCenSaleDtl_InvalidDate,true);
		var UnitMemo = '规格:' + value.BmsCenSaleDtl_UnitMemo;
		var Price = '单价:' + value.BmsCenSaleDtl_Price;
		
		var invalidTime = Shell.util.Date.getDate(value.BmsCenSaleDtl_InvalidDate);
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
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + LotNo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UnitMemo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + Price + '</div>');
		//html.push('<div class="list-group-item-text" style="' + pStyle + '">' + SumTotal + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + InvalidDate + '</div>');
		
		html.push('<div style="padding:2px;margin:2px;margin-top:-75px;width:80px;background-color:#f5f5f5;float:right;font-weight:bold;text-align:center;">');
		html.push('<div style="color:#5cb85c">' + value.BmsCenSaleDtl_GoodsQty + '</div>');//数量
		html.push('<div style="">' + value.BmsCenSaleDtl_GoodsUnit + '</div>');//单位
		html.push('</div>');
		html.push('<div style="padding:2px;margin:2px;margin-top:-25px;width:180px;float:right;text-align:right;">');
		html.push('<div>总额:<span style="color:#d9534f;font-weight:bold;">' + value.BmsCenSaleDtl_SumTotal + '</span>元</div>');//数量
		html.push('</div>');
		html.push('</a>');
		return html.join('');
	}
	
	//刷新列表数据
	function refreshContent(){
		//加载供货单信息数据
		loadInfoData(function(data){
			if(data.success){
				var listHtml = createInfo(data.value);
				//加载供货单明细列表数据
				loadDtlData(function(value){
					if(value.success){
						var dtlHtml = createDtl(value.value);
						$("#ContentDiv").html(listHtml + dtlHtml);
					}else{
						$("#ContentDiv").html('<div class="error-div">' + data.msg + '</div>');
					}
				});
			}else{
				$("#ContentDiv").html('<div class="error-div">' + data.msg + '</div>');
			}
		});
		
	}
	refreshContent();
});