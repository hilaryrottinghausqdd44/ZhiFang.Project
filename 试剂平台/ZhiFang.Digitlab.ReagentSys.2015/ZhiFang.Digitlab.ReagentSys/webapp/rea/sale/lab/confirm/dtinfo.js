$(function() {
	//外部参数
	var params = Shell.util.getRequestParams(true);
	var PK = params.ID || "";
	//账号
	var ACCOUNT = params.ACCOUNT || "";
	//密码
	var PASSWORD = params.PASSWORD || "";
	//所有明细数据
	var AllList = [];
	//所有明细的金额
	var ALLSUMTOTAL = 0;
	//初始化监听
	function initListeners() {
		//返回按钮监听
		$("#dt-div-top-back").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			topback();
		});
	}
	//明细--创建供货单详细列表内容
	function createDtHtml(list) {
		list = list || [];
		var html = [];
		html.push('<div>');
		for(var i = 0; i < list.length; i++) {
			var row = createDtRow(list[i]);
			html.push(row);
		}
		if(list.length == 0) {
			html.push('<div class="no-data-div">没有找到数据!</div>'); //没有数据
		}
		html.push('</div>');
		return html.join('');
	}

	function getTitlePre(model) {
		var titlePre = "";
		//条码类型
		var barCodeMgr =""+model.BmsCenSaleDtl_BarCodeMgr;
		switch(barCodeMgr) {
			case "1": //盒条码	
				titlePre = '<span style="padding:2px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				break;
			case "0": //批条码	
				titlePre = '<span style="padding:2px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				break;
			default:
				break;
		}
		return titlePre;
	}
	//明细--创建数据行内容,value为合并行后的信息
	function createDtRow(model) {
		var html = [];
		var id = model.BmsCenSaleDtl_Id;
		//条码类型
		var barCodeMgr = ""+model.BmsCenSaleDtl_BarCodeMgr;
		var dtlCount = model.BmsCenSaleDtl_DtlCount;
		if(dtlCount) dtlCount = parseFloat(dtlCount);
		var goodsQty = model.BmsCenSaleDtl_GoodsQty;
		if(goodsQty) goodsQty = parseFloat(goodsQty);
		var unit = model.BmsCenSaleDtl_GoodsUnit;
		var price = parseFloat(model.BmsCenSaleDtl_Price);
		//兼容因为在早期时的数据对DtlCount是没有赋值的,还是取GoodsQty
		if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;
		
		//总额
		var sumTotal = 0;
		sumTotal = dtlCount * price;
		if(sumTotal && sumTotal.toString().indexOf('.') > 0) sumTotal = sumTotal.toFixed(2);
		
		var titlePre = getTitlePre(model);
		var invalidTime = Shell.util.Date.getDate(model.BmsCenSaleDtl_InvalidDate);
		if(invalidTime) {
			invalidTime = invalidTime.getTime();
			var newTime = new Date().getTime();
			var num = invalidTime - newTime;
			var times = 10 * 24 * 3600 * 1000;
			if(num < 0) {
				InvalidDate = '<span style="color:#d9534f">' + InvalidDate + '<span>';
			} else if(num < times) {
				InvalidDate = '<span style="color:#f0ad4e">' + InvalidDate + '<span>';
			} else {
				InvalidDate = '<span style="color:#5cb85c">' + InvalidDate + '<span>';
			}
		}

		var title = '' + model.BmsCenSaleDtl_GoodsName + '';
		var LotNo = '批号:' + model.BmsCenSaleDtl_LotNo;
		var InvalidDate = '有效期至:' + Shell.util.Date.toString(model.BmsCenSaleDtl_InvalidDate, true);
		var UnitMemo = '规格:' + model.BmsCenSaleDtl_UnitMemo;
		var Price = '单价:' + price+"/"+unit;
		var sumTotal = '总额:<span style="color:#d9534f;font-weight:bold;">' + sumTotal + '</span>元';
		
		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<a class="list-group-item" data="' + id + '" style="margin:2px;">');
		html.push('<h4 class="list-group-item-heading">' + titlePre + title + '</h4>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + LotNo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UnitMemo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + Price + '</div>');		
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + sumTotal + '</div>');	
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + InvalidDate + '</div>');
		
		html.push('<div style="padding:2px;margin:2px;margin-top:-88px;width:106px;float:right;">');

		//明细数量
		html.push('<div class="btn btn-sm btn-success" style="width:105px;padding:2px;margin:2px;">总明细:' + dtlCount + '</div>');
		//验收数量
		html = getAcceptCounthtml(model, html);
		//已拒收
		html = getRefuseCounthtml(model, html);
		html.push('</div>');
		//html.push('<div style="padding:2px;margin:2px;margin-top:-22px;width:150px;float:right;text-align:right;">');
		//html.push('<div>总额:<span style="color:#d9534f;font-weight:bold;">' + sumTotal + '</span>元</div>');		
		html.push('</div>');
		html.push('</a>');
		return html.join('');
	}
	function getAcceptCounthtml(model, html) {
		var dtlCount = model.BmsCenSaleDtl_DtlCount;
		if(dtlCount) dtlCount = parseFloat(dtlCount);
		var goodsQty = model.BmsCenSaleDtl_GoodsQty,
			unit = model.BmsCenSaleDtl_GoodsUnit;
		var acceptCount = model.BmsCenSaleDtl_AcceptCount;
		if(goodsQty) goodsQty = parseFloat(goodsQty);
		if(acceptCount) acceptCount = parseFloat(acceptCount);
		//兼容因为在早期时的数据对DtlCount是没有赋值的,还是取GoodsQty
		if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;
		varcheckInfo = "";
		var divclass = "";
		if(acceptCount == 0) {
			divclass = "btn btn-sm btn-danger";
			checkInfo = "已验收:" + acceptCount;
		} else if(acceptCount == dtlCount) {
			divclass = "btn btn-sm btn-success";
			checkInfo = "已验收:" + acceptCount;
		} else if(acceptCount != dtlCount) {
			divclass = "btn btn-sm btn-danger";
			checkInfo = "已验收:" + acceptCount;
		}
		html.push('<div class="' + divclass + '" style="width:105px;padding:2px;margin:2px;">' + checkInfo + '</div>');
		return html;
	};
	function getRefuseCounthtml(model, html) {
		var dtlCount = model.BmsCenSaleDtl_DtlCount;
		if(dtlCount) dtlCount = parseFloat(dtlCount);
		var refuseCount = model.BmsCenSaleDtl_RefuseCount;
		if(refuseCount) refuseCount = parseFloat(refuseCount);
		varcheckInfo = "";
		var divclass = "";
		divclass = "btn btn-sm btn-danger";	
		checkInfo = "已拒收:" + refuseCount;
		html.push('<div class="' + divclass + '" style="width:105px;padding:2px;margin:2px;">' + checkInfo + '</div>');
		return html;
	};
	//设置显示金额(先隐藏)
	function setShowSumTotal() {
		var html = "";
		ALLSUMTOTAL = 0;
		html += '<span hidden="hidden">总额:<b style="color:#d9534f;">' + ALLSUMTOTAL + '</b>元</span>&nbsp;&nbsp;';
		$("#dt-show-total").html(html);
	}
	/***
	 * 明细--加载供货单明细列表数据
	 * @param {Object} PK
	 * @param {Object} callback
	 */
	function loadergerDtById(PK, callback) {
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchMergerDtListForCheckByBmsCenSaleDocId?isPlanish=true&fields=BmsCenSaleDtl_GoodsName,BmsCenSaleDtl_LotNo,BmsCenSaleDtl_InvalidDate,BmsCenSaleDtl_GoodsUnit,BmsCenSaleDtl_UnitMemo,BmsCenSaleDtl_GoodsQty,BmsCenSaleDtl_Price,BmsCenSaleDtl_SumTotal,BmsCenSaleDtl_Id,BmsCenSaleDtl_MixSerial,BmsCenSaleDtl_BarCodeMgr,BmsCenSaleDtl_DtlCount,BmsCenSaleDtl_AcceptCount,BmsCenSaleDtl_ProdGoodsNo,BmsCenSaleDtl_PSaleDtlID,BmsCenSaleDtl_RefuseCount,BmsCenSaleDtl_RefuseCount,BmsCenSaleDtl_AcceptFlag';
		var sort = "[{\"property\":\"BmsCenSaleDtl_DispOrder\",\"direction\":\"ASC\"}]";
		url += ('&page=1&start=0&limit=10000&bmsCenSaleDocId=' + PK + "&sort=" + sort + "&t=" + new Date().getTime());
		Shell.util.Server.ajax({
			async: false,
			url: url
		}, function(data) {
			callback(data);
		});
	}
	//明细--加载明细列表数据
	function loadDtContent() {
		if(AllList && AllList.length > 0) {
			var data = AllList;
			var dtlHtml = createDtHtml(data);
			$("#dt-div-content").html(dtlHtml);
			setShowSumTotal();
		} else {
			AllList = [];
			//加载供货单明细列表数据
			ShellComponent.mask.loading();
			loadergerDtById(PK, function(value) {
				ShellComponent.mask.hide();
				if(value.success) {
					$("#dt-div-submit").show();
					AllList = value.value.list;
					var data = AllList;
					var dtlHtml = createDtHtml(data);
					$("#dt-div-content").html(dtlHtml);
					setShowSumTotal();
				} else {
					$("#dt-div-content").html('<div class="error-div">' + value.msg + '</div>');
				}
			});
		}
	}
	//返回的操作
	function topback() {
		AllList = [];
		location.href = "list.html?account=" + ACCOUNT + "&password=" + PASSWORD + "&islogin=0" + "&type=1&&t=" + new Date().getTime();
	}
	//判断客户端是移动设备还是PC,设置单击事件
	function browserEventClick() {
		var isPC = Shell.util.Event.isPC();
		if(isPC) {
			Shell.util.Event.click = "click";
		} else {
			Shell.util.Event.click = "touchend";
		}
		initListeners();
	}
	browserEventClick();
	loadDtContent();
});