var Shell = Shell || {};
Shell.acceptance = Shell.acceptance || {};
//合并行的条件字段
Shell.acceptance.SCANCODEFILE = "BmsCenSaleDtl_MixSerial";

/***
 * 明细--加载供货单明细列表数据
 * @param {Object} PK
 * @param {Object} callback
 */
Shell.acceptance.loadDtlData = function(PK, callback) {
	var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true&fields=BmsCenSaleDtl_GoodsName,BmsCenSaleDtl_LotNo,BmsCenSaleDtl_InvalidDate,BmsCenSaleDtl_GoodsUnit,BmsCenSaleDtl_UnitMemo,BmsCenSaleDtl_GoodsQty,BmsCenSaleDtl_Price,BmsCenSaleDtl_SumTotal,BmsCenSaleDtl_Id,BmsCenSaleDtl_MixSerial,BmsCenSaleDtl_BarCodeMgr,BmsCenSaleDtl_DtlCount,BmsCenSaleDtl_AcceptCount';
	var sort = "[{\"property\":\"BmsCenSaleDtl_DispOrder\",\"direction\":\"ASC\"}]";
	url += ('&page=1&start=0&limit=10000&where=bmscensaledtl.BmsCenSaleDoc.Id=' + PK + "&sort=" + sort + "&t=" + new Date().getTime());
	Shell.util.Server.ajax({
		async: false,
		url: url
	}, function(data) {
		callback(data);
	});
};
/* 获取混合条码分割后的值,混合条码格式有
 * (1)平台的盒装混全条码格式:ZFRP|1|1116|078-K138-01|T11131|2017-05-30|110001118|5459663006151236684|1|111
 * (2)平台的非盒装混合条码格式:5459663006151236684,(验收待定)
 * (3)第三方平台的混合条码格式:11131|1747070530|20170530|111,分割后取前三段
 * */
Shell.acceptance.getSplitMixSerial = function(model) {
	if(!model) return "";
	//条码类型
	var barCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
	var mixSerial = Shell.acceptance.getMixSerialByBarCodeMgr(model);
	if(!mixSerial) return "";
	switch(barCodeMgr) {
		case "1": //盒条码试剂的混合条码处理
			var tempArr = mixSerial.split("|");
			if(!tempArr) return mixSerial;
			if(tempArr.length == 4) {
				mixSerial = tempArr[3];
			} else if(tempArr.length > 4) {
				mixSerial = tempArr[7];
			} else {
				mixSerial = tempArr[0];
			}
			break;
		default:
			break;
	}
	return mixSerial;

};
Shell.acceptance.getBarCodeOfMixSerial = function(model) {
	if(!model) return "";
	//条码类型
	var barCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
	var mixSerial = model[Shell.acceptance.SCANCODEFILE];
	if(!mixSerial) return "";
	switch(barCodeMgr) {
		case "1": //盒条码试剂的混合条码处理
			var tempArr = mixSerial.split("|");
			if(!tempArr) return mixSerial;
			if(tempArr.length == 4) {
				mixSerial = "" + tempArr[3];
			} else if(tempArr.length >= 9) {
				mixSerial = +tempArr[7] + "|" + tempArr[8] + "|" + tempArr[9];
			}
			break;
		default:
			break;
	}
	//var isPC = Shell.util.Event.isPC();
	//if(isPC==false&&mixSerial.length >= 24) mixSerial = "" + mixSerial.substring(4, mixSerial.length);
	return mixSerial;
};
/***
 * 获取比较同一试剂的比较判断依据值
 * 如果条码类型为盒类型(以混合条码为比较判断)
 * 如果条码类型为批条码或无条码(先取混合条码,如果混合条码没值,取该试剂明细的主键值为比较判断依据)
 * @param {Object} model
 */
Shell.acceptance.getMixSerialByBarCodeMgr = function(model) {
	if(!model) return "";
	//条码类型
	var barCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
	var mixSerial = model[Shell.acceptance.SCANCODEFILE];
	switch(barCodeMgr) {
		case "1": //盒条码试剂的混合条码处理
			mixSerial = model[Shell.acceptance.SCANCODEFILE];
			break;
		default:
			if(!mixSerial) mixSerial = model["BmsCenSaleDtl_Id"];
			break;
	}
	return mixSerial;
};
/***
 * 明细--依产品条码合并明细数据
 * @param {Object} list 供货单明细数据
 * @param {Object} bo
 */
Shell.acceptance.mergerData = function(list, bo) {
	list = list || [];
	var map = {},
		data = [];
	if(bo) {
		for(var i = 0; i < list.length; i++) {
			var model = $.extend(true, {}, list[i]);
			//混合条码BmsCenSaleDtl_MixSerial
			var mixSerial = Shell.acceptance.getSplitMixSerial(model);
			if(!mixSerial) continue;

			if(mixSerial && !map[mixSerial]) {
				map[mixSerial] = model;
			} else {
				var GoodsQty = model.BmsCenSaleDtl_GoodsQty;
				map[mixSerial].BmsCenSaleDtl_GoodsQty = parseInt(GoodsQty) +
					parseInt(map[mixSerial].BmsCenSaleDtl_GoodsQty);
				map[mixSerial].BmsCenSaleDtl_SumTotal =
					parseInt(map[mixSerial].BmsCenSaleDtl_GoodsQty) *
					parseFloat(map[mixSerial].BmsCenSaleDtl_Price);
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
};
/***
 * 明细--盒条码的验收数量Html处理
 * @param {Object} model
 * @param {Object} html
 * @param {Object} list 供货单明细数据
 */
Shell.acceptance.getcheckhtml = function(model, html, list) {
	var goodsQty = model.BmsCenSaleDtl_GoodsQty,
		unit = model.BmsCenSaleDtl_GoodsUnit;
	if(goodsQty) {
		goodsQty = parseInt(goodsQty);
	}
	var qty = Shell.acceptance.getDheckQty(model, list);

	var AcceptCount = qty.AcceptCount,
		checkInfo = "";
	var divclass = "";
	if(AcceptCount == 0) {
		divclass = "btn btn-sm btn-danger";
		checkInfo = "验收数量:" + AcceptCount + unit;
	} else if(AcceptCount == goodsQty) {
		divclass = "btn btn-sm btn-success";
		checkInfo = "验收数量:" + AcceptCount + unit;
	} else if(AcceptCount != goodsQty) {
		divclass = "btn btn-sm btn-danger";
		checkInfo = "验收数量:" + AcceptCount + unit;
	}

	html.push('<div class="' + divclass + '" style="width:105px;padding:2px;margin:2px;">' + checkInfo + '</div>');
	return html;
};
/***
 * 明细--盒条码:找出某一合并行的验收数量
 * @param {Object} model
 * @param {Object} list 供货单明细数据
 */
Shell.acceptance.getDheckQty = function(model, list) {
	var AcceptCount = 0;
	var qty = {
		rightQty: 0, //正解扫码数量
		errorQty: 0, //拒收扫码数量
		unAcceptCount: 0, //未验收数量
		AcceptCount: 0 //已验收数量
	};
	if(!list) list = [];
	var mixSerial1 = Shell.acceptance.getSplitMixSerial(model);
	for(var i = 0; i < list.length; i++) {
		var mixSerial2 = Shell.acceptance.getSplitMixSerial(list[i]);
		if(mixSerial1 == mixSerial2) {
			//扫码标志list[i].ScanCodeMark
			switch(list[i].ScanCodeMark) {
				case 0:
					qty.unAcceptCount = qty.unAcceptCount + 1;
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
	qty.AcceptCount = qty.rightQty;
	return qty;
};
/****
 * 明细--计算验收总价格
 * @param {Object} list 供货单原始明细数据
 */
Shell.acceptance.calcCheckSumTotal = function(list) {
	var CHECKSUMTOTAL = 0;
	for(var i = 0; i < list.length; i++) {
		var barCodeMgr = list[i].BmsCenSaleDtl_BarCodeMgr;
		//验收总价格计算
		switch(barCodeMgr) {
			case "1": //盒条码试剂的混合条码处理
				var mark = list[i].ScanCodeMark;
				if(mark == 2) CHECKSUMTOTAL += parseInt(list[i].BmsCenSaleDtl_Price);
				break;
			default: //批条码及无条码:验收数据*单价格
				CHECKSUMTOTAL += parseInt(list[i].BmsCenSaleDtl_AcceptCount) *
					parseFloat(list[i].BmsCenSaleDtl_Price);
				break;
		}
	}
	return CHECKSUMTOTAL;
};
/****
 * 明细--计算总价格
 * @param {Object} list 供货单原始明细数据
 */
Shell.acceptance.calcAllSumTotal = function(list) {
	var ALLSUMTOTAL = 0;
	var price = 0;
	for(var i = 0; i < list.length; i++) {
		var barCodeMgr = list[i].BmsCenSaleDtl_BarCodeMgr;
		//验收总价格计算
		switch(barCodeMgr) {
			case "1": //盒条码试剂的混合条码处理
				if(list[i].BmsCenSaleDtl_Price) price = parseInt(list[i].BmsCenSaleDtl_Price);
				break;
			default: //批条码及无条码:原始数量*单价格
				var goodsQty = list[i].BmsCenSaleDtl_GoodsQty;
				if(goodsQty) goodsQty = parseInt(goodsQty);
				price = goodsQty * parseFloat(list[i].BmsCenSaleDtl_Price);
				break;
		}
		ALLSUMTOTAL += price;
	}
	return ALLSUMTOTAL;
};
/****
 * 明细--初始化供货单明细数据(明细手工添加扫码标志)
 * @param {Object} list 供货单原始明细数据
 * @param {Object} mark 扫码标志:0为没有扫码确认;2为接收扫码确认;1为拒收扫码确认
 */
Shell.acceptance.setMarkDtLocalData = function(list, mark) {
	for(var i = 0; i < list.length; i++) {
		list[i].ScanCodeMark = mark;
		var barCodeMgr = "" + list[i].BmsCenSaleDtl_BarCodeMgr;
		//条码类型为批条码及无条码验收数量处理
		if(barCodeMgr != "1") {
			var goodsQty = list[i].BmsCenSaleDtl_GoodsQty;
			if(goodsQty) goodsQty = parseInt(goodsQty);
			list[i].BmsCenSaleDtl_GoodsQty = goodsQty;
			//验收数量的验收处理或重置
			var acceptCount = (mark == 2 ? goodsQty : 0);
			list[i].BmsCenSaleDtl_AcceptCount = acceptCount;
		}
	}
	return list;
};