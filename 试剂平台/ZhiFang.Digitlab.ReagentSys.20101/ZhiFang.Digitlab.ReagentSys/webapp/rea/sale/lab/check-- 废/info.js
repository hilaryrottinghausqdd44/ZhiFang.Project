$(function() {
	var PK = Shell.util.getRequestParams(false).id;
	//合并行的条件字段
	var SCANCODEFILE = "BmsCenSaleDtl_MixSerial";
	//供货单明细本地LocalStorage的Key
	var LocalStorage_BmsCenSaleDtl = "BmsCenSaleDtl";

	//选中的按钮标志,1=重置,2=整单验收,3=验收确认
	var CHECKED_BUTTON = null;
	//初始化监听
	function initListeners() {
		//重置
		$("#btn-reset-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			CHECKED_BUTTON = 1;
			$("#createDoctorOrder-msg").html("确定重置数据吗？");
			$("#createDoctorOrder-footer").show();
			$("#createDoctorOrderModal").modal({
				backdrop: 'static',
				keyboard: false
			});
		});
		//错误扫码
		$("#btn-error-scancode").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			showScanCode(1);
		});
		//正确扫码
		$("#btn-right-scancode").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			showScanCode(2);
		});
		$("#btn-alldata-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			CHECKED_BUTTON = 2;
			$("#createDoctorOrder-msg").html("确定整单验收吗？");
			$("#createDoctorOrder-footer").show();
			$("#createDoctorOrderModal").modal({
				backdrop: 'static',
				keyboard: false
			});
		});
		//确认提交操作
		$("#div-save-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			CHECKED_BUTTON = 3;
			$("#createDoctorOrder-msg").html("确定提交验收吗？");
			$("#createDoctorOrder-footer").show();
			$("#createDoctorOrderModal").modal({
				backdrop: 'static',
				keyboard: false
			});
		});
		//弹出确定按钮处理
		$("#createDoctorOrder-button").on("click", function() {
			$("#createDoctorOrderModal").modal("hide");
			if(CHECKED_BUTTON == 1) {
				refreshDataAndContent(0);
			} else if(CHECKED_BUTTON == 2) {
				allDataCheck(2);
			} else if(CHECKED_BUTTON == 3) {
				checkSaveData(function() {
					Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, "");
					var account = Shell.util.LocalStorage.get("S_ACCOUNT");
					var password = Shell.util.LocalStorage.get("S_PASSWORD");
					location.href = "list.html?account=" + account + "&password=" + password;
				});
			} else {
				ShellComponent.messagebox.msg("操作错误！");
			}
		});
	}
	//供货单整单验收
	function allDataCheck(mark) {
		refreshDataAndContent(mark);
	}
	//打开供货单明细扫码
	function showScanCode(mark) {
		location.href = 'scancode.html?mark=' + mark + "&id=" + PK;
	}
	//加载供货单明细列表数据
	function loadDtlData(callback) {
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true&fields=BmsCenSaleDtl_GoodsName,BmsCenSaleDtl_ShortCode,BmsCenSaleDtl_ProdGoodsNo,BmsCenSaleDtl_LotNo,BmsCenSaleDtl_InvalidDate,BmsCenSaleDtl_GoodsUnit,BmsCenSaleDtl_UnitMemo,BmsCenSaleDtl_GoodsQty,BmsCenSaleDtl_IOGoodsQty,BmsCenSaleDtl_AcceptCount,BmsCenSaleDtl_AccepterErrorMsg,BmsCenSaleDtl_Price,BmsCenSaleDtl_SumTotal,BmsCenSaleDtl_TaxRate,BmsCenSaleDtl_ProdDate,BmsCenSaleDtl_BiddingNo,BmsCenSaleDtl_Id,BmsCenSaleDtl_GoodsSerial,BmsCenSaleDtl_LotSerial,BmsCenSaleDtl_PackSerial,BmsCenSaleDtl_MixSerial'
		url += '&page=1&start=0&limit=10000';
		url += '&where=bmscensaledtl.BmsCenSaleDoc.Id=' + PK;
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async: false,
			url: url
		}, function(data) {
			setTimeout(function(){
				ShellComponent.mask.hide();
				callback(data);
			},100);
		});
	}
	//创建供货单详细列表内容
	function createDtl(list) {
		list = list || [];
		var html = [];
		html.push('<div>');
		for(var i = 0; i < list.length; i++) {
			var row = createRow(list[i]);
			html.push(row);
		}
		if(list.length == 0) {
			html.push('<div class="no-data-div">没有找到数据!</div>'); //没有数据
		}
		html.push('</div>');
		return html.join('');
	}
	//创建数据行内容,value为合并行后的信息
	function createRow(value) {
		var html = [];

		var id = value.BmsCenSaleDtl_Id;
		var title = '' + value.BmsCenSaleDtl_GoodsName + '';
		//var GoodsNo = '编号:' + value.BmsCenSaleDtl_ProdGoodsNo;
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

		html.push('<div style="padding:2px;margin:2px;margin-top:-80px;width:70px;float:right;">');
		//验收数量
		html = getcheckhtml(value, html);
		//原始数量
		var goodsQty = value.BmsCenSaleDtl_GoodsQty,
			unit = value.BmsCenSaleDtl_GoodsUnit;
		html.push('<div class="btn btn-sm btn-success" style="width:70px;padding:2px;margin:2px;">原始:' + goodsQty + unit + '</div>');
		html.push('</div>');

		//总额
		html.push('<div style="padding:2px;margin:2px;margin-top:-25px;width:180px;float:right;text-align:right;">');
		html.push('<div>总额:<span style="color:#d9534f;font-weight:bold;">' + value.BmsCenSaleDtl_SumTotal + '</span>元</div>');
		html.push('</div>');
		html.push('</a>');
		return html.join('');
	}
	//验收数量Html处理
	function getcheckhtml(value, html) {
		var goodsQty = value.BmsCenSaleDtl_GoodsQty,
			unit = value.BmsCenSaleDtl_GoodsUnit;
		if(goodsQty) {
			goodsQty = parseInt(goodsQty);
		}
		var qty = getDheckQty(value);
		var checkQty = qty.checkQty,
			checkInfo = "";
		var divclass = "";
		if(checkQty == 0) {
			divclass = "btn btn-sm btn-danger";
			checkInfo = "未验收:" + checkQty + unit;
		} else if(checkQty == goodsQty) {
			divclass = "btn btn-sm btn-success";
			checkInfo = "验收:" + checkQty + unit;
		} else if(checkQty != goodsQty) {
			divclass = "btn btn-sm btn-danger";
			checkInfo = "验收:" + checkQty + unit;
		}

		html.push('<div class="' + divclass + '" style="width:70px;padding:2px;margin:2px;">' + checkInfo + '</div>');
		return html;
	}
	/**找出某一合并行的验收数量*/
	function getDheckQty(value) {
		var checkQty = 0;
		var qty = {
			rightQty: 0, //正解扫码数量
			errorQty: 0, //错误扫码数量
			uncheckQty: 0, //未验收数量
			checkQty: 0 //已验收数量
		};
		var list = Shell.util.LocalStorage.get(LocalStorage_BmsCenSaleDtl);
		list = list ? Shell.util.JSON.decode(list) : [];
		for(var i = 0; i < list.length; i++) {
			//产品条码
			if(value.BmsCenSaleDtl_GoodsSerial === list[i].BmsCenSaleDtl_GoodsSerial) {
				//扫码标志
				switch(list[i].ScanCodeMark) {
					case 0:
						qty.uncheckQty = qty.uncheckQty + 1;
						break;
					case 1:
						qty.errorQty = qty.errorQty + 1;
						break;
					case 2:
						qty.rightQty = qty.rightQty + 1;
						break;
					default:
						break;
				}
			}
		}
		qty.checkQty = qty.rightQty; // + qty.errorQty
		return qty;
	}
	/**依产品条码合并明细数据*/
	function mergerData(data, bo) {
		var list = data || [];
		var map = {},
			data = [];
		if(bo) {
			var tempArr = [];
			for(var i = 0; i < list.length; i++) {
				//混合条码BmsCenSaleDtl_MixSerial
				var MixSerial = list[i][SCANCODEFILE];
				if(MixSerial) tempArr = MixSerial.split("|");
				if(MixSerial.length > 0) MixSerial = tempArr[7];
				if(MixSerial.length > 0 && !map[MixSerial]) {
					map[MixSerial] = list[i];
				} else {
					var GoodsQty = list[i].BmsCenSaleDtl_GoodsQty;
					map[MixSerial].BmsCenSaleDtl_GoodsQty = parseInt(GoodsQty) +
						parseInt(map[MixSerial].BmsCenSaleDtl_GoodsQty);
					map[MixSerial].BmsCenSaleDtl_SumTotal =
						parseInt(map[MixSerial].BmsCenSaleDtl_GoodsQty) *
						parseFloat(map[MixSerial].BmsCenSaleDtl_Price);
				}
			}
			var i = 0;
			for(var m in map) {
				data[i++] = map[m];
			}
		} else {
			data = list;
		}
		return data;
	}
	/****
	 * 初始化供货单明细数据(明细手工添加扫码标志)
	 * @param {Object} list 供货单原始明细数据
	 * @param {Object} mark 扫码标志:0为没有扫码确认;1为正确扫码确认;2为错误扫码确认
	 */
	function setMarkLocalData(list, mark) {
		for(var i = 0; i < list.length; i++) {
			list[i].ScanCodeMark = mark;
		}
		if(list && list.length > 0) {
			Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, Shell.util.JSON.encode(list));
		} else {
			Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, "");
		}
		return list;
	}
	/***
	 * 还原供货单明细数据(明细手工添加扫码标志)
	 * @param {Object} mark 扫码标志:0为没有扫码确认;1为正确扫码确认;2为错误扫码确认
	 */
	function refreshDataAndContent(mark) {
		var list = Shell.util.LocalStorage.get(LocalStorage_BmsCenSaleDtl);
		list = list ? Shell.util.JSON.decode(list) : [];
		list = setMarkLocalData(list, mark);
		var data = mergerData(list, true);
		var dtlHtml = createDtl(data);
		$("#ContentDiv").html("");
		$("#ContentDiv").html(dtlHtml);
	}
	//供货单验收处理
	function checkSaveData(callback) {
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_ConfirmSaleDocByIDAndDtlIDList';
		var idStr = "";
		var list = Shell.util.LocalStorage.get(LocalStorage_BmsCenSaleDtl);
		list = list ? Shell.util.JSON.decode(list) : [];
		for(var i = 0; i < list.length; i++) {
			var mark = list[i].ScanCodeMark;
			if(mark) mark = parseInt(mark);
			switch(mark) {
				case 1:
					idStr = idStr + list[i].BmsCenSaleDtl_Id + ",";
					break;
				default:
					break;
			}
		}
		if(idStr && idStr.length > 0) idStr = idStr.substring(0, idStr.length - 1);

		var entity = {
			saleDocID: PK,
			saleDtlIDList: idStr //错误扫码的明细Id
		};
		var data = Shell.util.JSON.encode(entity);
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			type: 'post',
			url: url,
			data: data
		}, function(data) {
			setTimeout(function(){
				ShellComponent.mask.hide();
				if(data.success) {
					callback();
				} else {
					ShellComponent.messagebox.msg(data.msg);
				}
			},100);
		});
	}
	//加载明细列表数据
	function loadDtContent() {
		var list = Shell.util.LocalStorage.get(LocalStorage_BmsCenSaleDtl);
		list = list ? Shell.util.JSON.decode(list) : [];
		if(list && list.length > 0) {
			var data = mergerData(list, true);
			var dtlHtml = createDtl(data);
			$("#ContentDiv").html(dtlHtml);
		} else {
			Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, "");
			//加载供货单明细列表数据
			loadDtlData(function(value) {
				if(value.success) {
					var mark = 0;
					var list = setMarkLocalData(value.value.list, mark);
					var data = mergerData(list, true);
					var dtlHtml = createDtl(data);
					$("#ContentDiv").html(dtlHtml);

				} else {
					$("#ContentDiv").html('<div class="error-div">' + data.msg + '</div>');
				}
			});
		}
	}
	//判断客户端是移动设备还是PC,设置单击事件
	function browserEventClick() {
		var isPC = Shell.util.Event.isPC();
		if(isPC) {
			Shell.util.Event.click = "click";
			//返回按钮监听
			$(".navbar-top-back").on(Shell.util.Event.click, function() {
				Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, "");
				var account = Shell.util.LocalStorage.get("S_ACCOUNT");
				var password = Shell.util.LocalStorage.get("S_PASSWORD");
				location.href = "list.html?account=" + account + "&password=" + password;
			});
		} else {
			Shell.util.Event.click = "touchend";
		}
		initListeners();
	}
	browserEventClick();
	loadDtContent();
});