var Shell = Shell || {};
Shell.acceptance = Shell.acceptance || {};

/***
 * 明细--加载供货单明细列表数据
 * @param {Object} PK
 * @param {Object} callback
 */
Shell.acceptance.loadDtlData = function(PK, callback) {
	var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true&fields=BmsCenSaleDtl_GoodsName,BmsCenSaleDtl_LotNo,BmsCenSaleDtl_InvalidDate,BmsCenSaleDtl_GoodsUnit,BmsCenSaleDtl_UnitMemo,BmsCenSaleDtl_GoodsQty,BmsCenSaleDtl_Price,BmsCenSaleDtl_SumTotal,BmsCenSaleDtl_Id,BmsCenSaleDtl_MixSerial,BmsCenSaleDtl_BarCodeMgr,BmsCenSaleDtl_DtlCount,BmsCenSaleDtl_AcceptCount,BmsCenSaleDtl_ProdGoodsNo,BmsCenSaleDtl_PSaleDtlID';
	var sort = "[{\"property\":\"BmsCenSaleDtl_DispOrder\",\"direction\":\"ASC\"}]";
	url += ('&page=1&start=0&limit=10000&where=bmscensaledtl.BmsCenSaleDoc.Id=' + PK + "&sort=" + sort + "&t=" + new Date().getTime());
	Shell.util.Server.ajax({
		async: false,
		url: url
	}, function(data) {
		callback(data);
	});
};
/***
 * 明细--依产品条码合并明细数据
 * @param {Object} list 供货单明细数据
 */
Shell.acceptance.mergerData = function(list) {
	list = list || [];
	var map = {},
		data = [];
	for(var i = 0; i < list.length; i++) {
		var model = $.extend(true, {}, list[i]);
		//混合条码BmsCenSaleDtl_MixSerial
		var mixSerial = Shell.check.common.getSplitMixSerial(model, true);
		if(!mixSerial) continue;

		if(mixSerial && !map[mixSerial]) {
			map[mixSerial] = model;
		} else {
			var goodsQty = model.BmsCenSaleDtl_GoodsQty;
			var dtlCount = list[i].BmsCenSaleDtl_DtlCount;
			if(dtlCount) dtlCount = parseFloat(dtlCount);
			//条码类型
			var barCodeMgr = "" + list[i].BmsCenSaleDtl_BarCodeMgr;
			switch(barCodeMgr) {
				case "1": //盒条码
					//第三方接口时,dtlCount为0或者没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
					if(!dtlCount || dtlCount == 0) dtlCount = Shell.acceptance.calcDtlCount(list[i], list);
					map[mixSerial].BmsCenSaleDtl_DtlCount = dtlCount;
					break;
				default:
					break;
			}
			//兼容因为在早期时的数据对DtlCount是没有赋值的,还是取GoodsQty
			if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;
			map[mixSerial].BmsCenSaleDtl_GoodsQty = parseFloat(goodsQty) +
				parseFloat(map[mixSerial].BmsCenSaleDtl_GoodsQty);
			map[mixSerial].BmsCenSaleDtl_SumTotal = dtlCount *
				parseFloat(map[mixSerial].BmsCenSaleDtl_Price);
		}
	}
	var i = 0;
	for(var m in map) {
		data[i++] = map[m];
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
	if(goodsQty) goodsQty = parseFloat(goodsQty);

	var dtlCount = model.BmsCenSaleDtl_DtlCount;
	if(dtlCount) dtlCount = parseFloat(dtlCount);

	//兼容因为在早期时的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
	if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;

	var acceptCount = Shell.acceptance.getAcceptCount(model, list);
	var checkInfo = "";
	var divclass = "";
	if(acceptCount == 0) {
		divclass = "btn btn-sm btn-danger";
		checkInfo = "待验收:" + acceptCount + unit;
	} else if(acceptCount == dtlCount) {
		divclass = "btn btn-sm btn-success";
		checkInfo = "待验收:" + acceptCount + unit;
	} else if(acceptCount != dtlCount) {
		divclass = "btn btn-sm btn-danger";
		checkInfo = "待验收:" + acceptCount + unit;
	}
	html.push('<div class="' + divclass + '" style="width:112px;padding:2px;margin:2px;">' + checkInfo + '</div>');
	return html;
};
/***
 * 明细--盒条码:找出某一合并行的验收数量
 * @param {Object} model
 * @param {Object} list 供货单明细数据
 */
Shell.acceptance.getAcceptCount = function(model, list) {
	//条码类型
	var barCodeMgr = "" + model.BmsCenSaleDtl_BarCodeMgr;
	var acceptCount = 0; //已验收数量
	if(model.BmsCenSaleDtl_AcceptCount) acceptCount = parseFloat(model.BmsCenSaleDtl_AcceptCount);
	if(barCodeMgr != "1") return acceptCount;
	if(barCodeMgr == "1") {
		acceptCount = 0;
		var mixSerial1 = Shell.check.common.getSplitMixSerial(model, true);
		if(!list) list = [];
		for(var i = 0; i < list.length; i++) {
			var mixSerial2 = Shell.check.common.getSplitMixSerial(list[i], true);
			if(mixSerial1 == mixSerial2 && list[i].ScanCodeMark == 2&&list[i].BmsCenSaleDtl_AcceptCount) {
				acceptCount += parseFloat(list[i].BmsCenSaleDtl_AcceptCount);
			}
		}
	}
	return acceptCount;
};
/***
 * 第三方接口时,dtlCount为0或者没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
 * @param {Object} model
 * @param {Object} list 供货单明细数据
 */
Shell.acceptance.calcDtlCount = function(model, list) {
	var dtlCount = 0;
	var mixSerial1 = Shell.check.common.getSplitMixSerial(model, true);
	if(!list) list = [];
	for(var i = 0; i < list.length; i++) {
		var mixSerial2 = Shell.check.common.getSplitMixSerial(list[i], true);
		if(mixSerial1 == mixSerial2&&list[i].BmsCenSaleDtl_GoodsQty) dtlCount += parseFloat(list[i].BmsCenSaleDtl_GoodsQty);
	}
	return dtlCount;
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
				if(list[i].ScanCodeMark == 2) CHECKSUMTOTAL += parseFloat(list[i].BmsCenSaleDtl_Price);
				break;
			default: //批条码及无条码:验收数据*单价格
				CHECKSUMTOTAL += parseFloat(list[i].BmsCenSaleDtl_AcceptCount) *
					parseFloat(list[i].BmsCenSaleDtl_Price);
				break;
		}
	}
	if(CHECKSUMTOTAL && CHECKSUMTOTAL.toString().indexOf('.') > 0) CHECKSUMTOTAL = CHECKSUMTOTAL.toFixed(2);
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
				if(list[i].BmsCenSaleDtl_Price) price = parseFloat(list[i].BmsCenSaleDtl_Price);
				break;
			default: //批条码及无条码:原始数量*单价格
				var goodsQty = list[i].BmsCenSaleDtl_GoodsQty;
				if(goodsQty) goodsQty = parseFloat(goodsQty);
				price = goodsQty * parseFloat(list[i].BmsCenSaleDtl_Price);
				break;
		}
		ALLSUMTOTAL += price;
	}
	if(ALLSUMTOTAL && ALLSUMTOTAL.toString().indexOf('.') > 0) ALLSUMTOTAL = ALLSUMTOTAL.toFixed(2);
	return ALLSUMTOTAL;
};
/****
 * 明细--初始化供货单明细数据(明细手工添加扫码标志)
 * @param {Object} list 供货单原始明细数据
 * @param {Object} mark 标志:0为没有扫码确认;3为整单接收;4为整单重置;
 */
Shell.acceptance.setMarkAndAcceptCountDtData = function(list, mark) {
	for(var i = 0; i < list.length; i++) {
		var acceptCount = list[i].BmsCenSaleDtl_AcceptCount;
		if(acceptCount) acceptCount = parseFloat(acceptCount);

		var goodsQty = list[i].BmsCenSaleDtl_GoodsQty;
		if(goodsQty) goodsQty = parseFloat(goodsQty);
		list[i].BmsCenSaleDtl_GoodsQty = goodsQty;

		var dtlCount = list[i].BmsCenSaleDtl_DtlCount;
		if(dtlCount) dtlCount = parseFloat(dtlCount);

		//条码类型
		var barCodeMgr = "" + list[i].BmsCenSaleDtl_BarCodeMgr;
		switch(barCodeMgr) {
			case "1":
				//第三方接口时,dtlCount为0或者没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
				if(!dtlCount || dtlCount == 0) dtlCount = Shell.acceptance.calcDtlCount(list[i], list);
				//整单验收验收数量处理
				acceptCount = (mark == 3 ? 1 : acceptCount);
				break;
			default:
				//兼容因为在早期时的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
				if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;
				//整单验收的验收数量处理
				acceptCount = (mark == 3 ? dtlCount : acceptCount);
				break;
		}
		list[i].BmsCenSaleDtl_DtlCount = dtlCount;
		//整单重置的验收数量处理
		if(mark == 4) acceptCount = 0;
		list[i].BmsCenSaleDtl_AcceptCount = acceptCount;
		//盒条码类型的试剂明细扫码标志处理
		//如果验收数量并且为待验收标志
		if(acceptCount > 0 && mark == 0) list[i].ScanCodeMark = 2;
		//mark为整单验收
		if(mark == 3) list[i].ScanCodeMark = 2;
		//mark为整单重置
		if(mark == 4) list[i].ScanCodeMark = 0;
	}
	return list;
};