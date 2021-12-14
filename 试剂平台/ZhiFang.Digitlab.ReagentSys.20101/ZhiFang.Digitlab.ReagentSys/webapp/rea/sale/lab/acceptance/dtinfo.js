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
	//明细--创建数据行内容,value为合并行后的信息
	function createDtRow(value) {
		var html = [];

		var id = value.BmsCenSaleDtl_Id;
		var title = '' + value.BmsCenSaleDtl_GoodsName + '';
		var LotNo = '批号:' + value.BmsCenSaleDtl_LotNo;
		var InvalidDate = '有效期至:' + Shell.util.Date.toString(value.BmsCenSaleDtl_InvalidDate, true);
		var UnitMemo = '规格:' + value.BmsCenSaleDtl_UnitMemo;
		var Price = '单价:' + value.BmsCenSaleDtl_Price;

		var invalidTime = Shell.util.Date.getDate(value.BmsCenSaleDtl_InvalidDate);
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
		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<a class="list-group-item" data="' + id + '" style="margin:2px;">');
		html.push('<h4 class="list-group-item-heading">' + title + '</h4>');

		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + LotNo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UnitMemo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + Price + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + InvalidDate + '</div>');

		html.push('<div style="padding:2px;margin:2px;margin-top:-77px;width:106px;float:right;">');

		//供货数量
		var goodsQty = value.BmsCenSaleDtl_GoodsQty,
			unit = value.BmsCenSaleDtl_GoodsUnit;
		html.push('<div class="btn btn-sm btn-success" style="width:105px;padding:2px;margin:2px;">供货数量:' + goodsQty + unit + '</div>');

		//验收数量
		html = getcheckhtml(value, html);
		html.push('</div>');

		//总额
		html.push('<div style="padding:2px;margin:2px;margin-top:-25px;width:180px;float:right;text-align:right;">');
		html.push('<div>总额:<span style="color:#d9534f;font-weight:bold;">' + value.BmsCenSaleDtl_SumTotal + '</span>元</div>');
		html.push('</div>');
		html.push('</a>');
		return html.join('');
	}

	function getcheckhtml(model, html) {
		var goodsQty = model.BmsCenSaleDtl_GoodsQty,
			unit = model.BmsCenSaleDtl_GoodsUnit;
		var acceptCount = model.BmsCenSaleDtl_AcceptCount;
		if(goodsQty) goodsQty = parseInt(goodsQty);
		if(acceptCount) acceptCount = parseInt(acceptCount);
		varcheckInfo = "";
		var divclass = "";
		if(acceptCount == 0) {
			divclass = "btn btn-sm btn-danger";
			checkInfo = "验收数量:" + acceptCount + unit;
		} else if(acceptCount == goodsQty) {
			divclass = "btn btn-sm btn-success";
			checkInfo = "验收数量:" + acceptCount + unit;
		} else if(acceptCount != goodsQty) {
			divclass = "btn btn-sm btn-danger";
			checkInfo = "验收数量:" + acceptCount + unit;
		}

		html.push('<div class="' + divclass + '" style="width:105px;padding:2px;margin:2px;">' + checkInfo + '</div>');
		return html;
	};
	//设置显示金额 #d9534f  #d9534f
	function setShowSumTotal() {
		var html = "";
		ALLSUMTOTAL = Shell.acceptance.calcAllSumTotal(AllList);
		html += '<span>总额:<b style="color:#d9534f;">' + ALLSUMTOTAL + '</b>元</span>&nbsp;&nbsp;';
		$("#dt-show-total").html(html);
	}
	//明细--加载明细列表数据
	function loadDtContent() {
		if(AllList && AllList.length > 0) {
			var data = Shell.acceptance.mergerData(AllList, true);
			var dtlHtml = createDtHtml(data);
			$("#dt-div-content").html(dtlHtml);
			setShowSumTotal();
		} else {
			AllList = [];
			//加载供货单明细列表数据
			ShellComponent.mask.loading();
			Shell.acceptance.loadDtlData(PK, function(value) {
				ShellComponent.mask.hide();
				if(value.success) {
					var mark = 2;
					$("#dt-div-submit").show();
					AllList = value.value.list;
					AllList = setMarkDtLocalData(AllList, mark);
					var data = Shell.acceptance.mergerData(AllList, true);
					var dtlHtml = createDtHtml(data);
					$("#dt-div-content").html(dtlHtml);
					setShowSumTotal();
				} else {
					$("#dt-div-content").html('<div class="error-div">' + value.msg + '</div>');
				}
			});
		}
	}

	function setMarkDtLocalData(list, mark) {
		for(var i = 0; i < list.length; i++) {
			list[i].ScanCodeMark = mark;
			var goodsQty = list[i].BmsCenSaleDtl_GoodsQty;
			var acceptCount = list[i].BmsCenSaleDtl_AcceptCount;
			if(goodsQty) goodsQty = parseInt(goodsQty);
			if(acceptCount) acceptCount = parseInt(acceptCount);
			list[i].BmsCenSaleDtl_GoodsQty = goodsQty;
			list[i].BmsCenSaleDtl_AcceptCount = acceptCount;
			var barCodeMgr = list[i].BmsCenSaleDtl_BarCodeMgr;
		}
		return list;
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