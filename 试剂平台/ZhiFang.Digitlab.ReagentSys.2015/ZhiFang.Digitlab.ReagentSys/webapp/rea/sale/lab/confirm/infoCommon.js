var Shell = Shell || {};
Shell.infoCommon = Shell.infoCommon || {};
Shell.infoCommon.loadDtlDataById = function(PK, callback) {
	var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlForCheckByBmsCenSaleDocId?isPlanish=false&fields=';
	var sort = "[{\"property\":\"BmsCenSaleDtl_DispOrder\",\"direction\":\"ASC\"}]";
	url += ('&page=1&start=0&limit=10000&bmsCenSaleDocId=' + PK + "&sort=" + sort + "&t=" + new Date().getTime());
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
Shell.infoCommon.mergerData = function(list) {
	list = list || [];
	var map = {},
		data = [];
	var mixSerial = "";
	for(var i = 0; i < list.length; i++) {
		var model = $.extend(true, {}, list[i]);
		//同一种试剂的区分值,PSaleDtlIDStr值由后台处理好
		mixSerial = model.PSaleDtlIDStr;
		if(!mixSerial) continue;

		if(mixSerial && !map[mixSerial]) {
			map[mixSerial] = model;
		} else {
			var goodsQty = model.GoodsQty;
			var dtlCount = list[i].DtlCount;
			if(dtlCount) dtlCount = parseFloat(dtlCount);
			var barCodeMgr = "" + list[i].BarCodeMgr;
			switch(barCodeMgr) {
				case "1": //盒条码
					//第三方接口,dtlCount为0或者没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
					if(!dtlCount || dtlCount == 0) dtlCount = Shell.infoCommon.calcDtlCount(list[i], list);
					map[mixSerial].DtlCount = dtlCount;
					break;
				default:
					break;
			}
			//兼容因为在早期时的数据对DtlCount是没有赋值的,还是取GoodsQty
			if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;
			map[mixSerial].GoodsQty = parseFloat(goodsQty) +
				parseFloat(map[mixSerial].GoodsQty);
			map[mixSerial].SumTotal = dtlCount *
				parseFloat(map[mixSerial].Price);
		}
	}
	var i = 0;
	for(var m in map) {
		data[i++] = map[m];
	}
	return data;
};
/***
 * 明细--盒条码的接收或拒收Html处理
 * @param {Object} model
 * @param {Object} html
 * @param {Object} list 供货单明细数据
 */
Shell.infoCommon.getAcceptCountRowHtmlOf2 = function(model, html, list) {
	var unit = model.GoodsUnit;
	var dtlCount = Shell.infoCommon.getdtlCount(model);
	var acceptCount = Shell.infoCommon.getAcceptCount(model, list);
	var checkInfo = "";
	var divclass = "";
	if(acceptCount > dtlCount) acceptCount = dtlCount;
	divclass = "btn btn-sm btn-success";
	checkInfo = "本次验收:" + acceptCount + unit;
	html.push('<div class="' + divclass + '" style="width:112px;padding:2px;margin:2px;">' + checkInfo + '</div>');
	return html;
};
/***
 * 明细--盒条码:找出某一试剂的验收数量
 * @param {Object} model
 * @param {Object} list 供货单明细数据
 */
Shell.infoCommon.getAcceptCount = function(model, list) {
	var barCodeMgr = "" + model.BarCodeMgr;
	var acceptCount = 0;
	if(model.AcceptCount) acceptCount = parseFloat(model.AcceptCount);
	if(barCodeMgr != "1") return acceptCount;

	if(barCodeMgr == "1") {
		acceptCount = 0;
		var mixSerial1 = model.PSaleDtlIDStr;
		if(!list) list = [];
		for(var i = 0; i < list.length; i++) {
			var mixSerial2 = list[i].PSaleDtlIDStr;
			var scanCodeMark = list[i].ScanCodeMark;
			//验收标志(0:待验收,扫码拒收:1,扫码接收:2,已存库验收:3,已存库拒收:4)
			if(mixSerial1 == mixSerial2 && (scanCodeMark == 2) && list[i].AcceptCount) {
				acceptCount += parseFloat(list[i].AcceptCount);
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
Shell.infoCommon.calcDtlCount = function(model, list) {
	var dtlCount = 0;
	var mixSerial1 = model.PSaleDtlIDStr;
	if(!list) list = [];
	for(var i = 0; i < list.length; i++) {
		var mixSerial2 = list[i].PSaleDtlIDStr;
		if(mixSerial1 == mixSerial2 && list[i].GoodsQty) dtlCount += parseFloat(list[i].GoodsQty);
	}
	return dtlCount;
};
//明细--计算总价格
Shell.infoCommon.calcSumTotal = function(list) {
	var result = {
		allSumTotal: 0,
		refuseumTotal: 0,
		checkSumTotal: 0
	};
	if(!list || list.length == 0) return result;
	var price = 0;
	for(var i = 0; i < list.length; i++) {
		var barCodeMgr = "" + list[i].BarCodeMgr;
		//验收总价格计算
		switch(barCodeMgr) {
			case "1":
				if(list[i].Price) price = parseFloat(list[i].Price);

				var scanCodeMark = list[i].ScanCodeMark;
				if(scanCodeMark == 2) result.checkSumTotal += parseFloat(list[i].Price);
				if(scanCodeMark == 1) result.refuseumTotal += parseFloat(list[i].Price);
				break;
			default:
				//批条码及无条码:原始数量*单价格
				var goodsQty = list[i].GoodsQty;
				if(goodsQty) goodsQty = parseFloat(goodsQty);
				price = goodsQty * parseFloat(list[i].Price);

				result.checkSumTotal += parseFloat(list[i].AcceptCount) * parseFloat(list[i].Price);
				result.refuseumTotal += parseFloat(list[i].RefuseCount) * parseFloat(list[i].Price);
				break;
		}
		result.allSumTotal += price;
	}
	if(result.allSumTotal && result.allSumTotal.toString().indexOf('.') > 0) result.allSumTotal = result.allSumTotal.toFixed(2);
	if(result.checkSumTotal && result.checkSumTotal.toString().indexOf('.') > 0) result.checkSumTotal = result.checkSumTotal.toFixed(2);
	if(result.refuseumTotal && result.refuseumTotal.toString().indexOf('.') > 0) result.refuseumTotal = result.refuseumTotal.toFixed(2);
	return result;
};
//重新计算当次已扫条码总数量
Shell.infoCommon.calcScancodeCount = function(CurBarCodeList, MARK) {
	var SCANCODECOUNT = 0;
	for(var i = 0; i < CurBarCodeList.length; i++) {
		var model = CurBarCodeList[i];
		var curBarCodeMgr = "" + model.BarCodeMgr;
		if(curBarCodeMgr == "1") {
			SCANCODECOUNT += 1;
		} else {
			var acceptCount = parseFloat(model.AcceptCount);
			var refuseCount = parseFloat(model.RefuseCount);
			//区分当前是接收还是拒收扫码
			SCANCODECOUNT += (MARK == 2 ? acceptCount : refuseCount);
		}
	}
	return SCANCODECOUNT;
};
/****
 * 明细--设置供货单明细数据本次接收及本次拒收,验收标志值
 * 已全部验收完或拒收完后,不需要再设置
 * @param {Object} list 供货单原始明细数据
 * @param {Object} mark 标志:0为没有扫码确认;3为整单接收;4为整单重置;
 */
Shell.infoCommon.setAllListofMark = function(list, mark) {
	for(var i = 0; i < list.length; i++) {
		var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(list[i]);
		if(isConfirm == false) {
			var acceptCount = parseFloat(list[i].AcceptCount);
			var refuseCount = parseFloat(list[i].RefuseCount);
			if(mark == 3) {
				var goodsQty = list[i].GoodsQty;
				if(goodsQty) goodsQty = parseFloat(goodsQty);
				list[i].GoodsQty = goodsQty;

				var dtlCount = list[i].DtlCount;
				if(dtlCount) dtlCount = parseFloat(dtlCount);

				//已存库的总数量
				var stockSumTotal = list[i].StockSumTotal;
				if(stockSumTotal) stockSumTotal = parseFloat(stockSumTotal);
				if(!stockSumTotal) stockSumTotal = 0;

				//条码类型
				var barCodeMgr = "" + list[i].BarCodeMgr;
				switch(barCodeMgr) {
					case "1":
						//第三方接口dtlCount为0或没有进行同一种试剂的明细数量计算时,dtlCount=同一种试剂的goodsQty累加
						if(!dtlCount || dtlCount == 0) dtlCount = Shell.infoCommon.calcDtlCount(list[i], list);
						//整单操作的验收数量处理
						acceptCount = 1;
						break;
					default:
						//兼容因为在早期的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
						if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;
						list[i].DtlCount = dtlCount;
						acceptCount = Shell.infoCommon.getTempDtlCount(list[i]);
						acceptCount = acceptCount >= 0 ? acceptCount : 0;
						break;
				}
				refuseCount = 0;
				//整单重置的验收数量处理
			} else if(mark == 4) {
				acceptCount = 0;
				refuseCount = 0;
			}
			list[i].DtlCount = dtlCount;
			list[i].AcceptCount = acceptCount;
			list[i].RefuseCount = refuseCount;
			//mark为整单验收
			if(mark == 3) list[i].ScanCodeMark = 2;
			//mark为整单重置
			if(mark == 4) list[i].ScanCodeMark = 0;
		}
	}
	return list;
};
//获取批条码的某一试剂当前还可扫码的总数量
Shell.infoCommon.getTempDtlCount = function(model) {
	var dtlCount = Shell.infoCommon.getdtlCount(model);
	var stockSumTotal = parseFloat(model.StockSumTotal);
	var tempDtlCount = dtlCount - stockSumTotal;
	return tempDtlCount;
};
//明细--供货单明细列表的录入数量更新
Shell.infoCommon.calcAcceptCount = function(data, nowValue, AllList) {
	var dtId = data.dtId;
	var isAccept = data.isAccept;
	for(var i = 0; i < AllList.length; i++) {
		if(AllList[i]["Id"] == dtId) {
			var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(AllList[i]);
			if(isConfirm == false) {
				//当前可以扫码的总数量
				var tempDtlCount = Shell.infoCommon.getTempDtlCount(AllList[i]);
				if(nowValue > tempDtlCount) nowValue = tempDtlCount;
				if(isAccept == true) {
					//验收录入操作
					AllList[i]["AcceptCount"] = nowValue;
					AllList[i]["ScanCodeMark"] = 2;
				} else {
					AllList[i]["RefuseCount"] = nowValue;
					AllList[i]["ScanCodeMark"] = 1;
				}
				var refuseCount = parseFloat(AllList[i].RefuseCount);
				var acceptCount = parseFloat(AllList[i].AcceptCount);
				var inputValue2 = (isAccept == true ? refuseCount : acceptCount);
				if((nowValue + inputValue2) >= tempDtlCount) {
					//需要联动更新的录入框Id(如果当前是接收,就需要更新拒收录入框的值)
					var calcCount = tempDtlCount - nowValue;
					if(calcCount < 0) calcCount = 0;
					var inputId = "#" + dtId + "-" + data.otype + "-" + (!data.isAccept);
					var input = $(inputId);
					if(input) input.val(calcCount);
					switch(isAccept) {
						case true:
							//验收录入操作
							AllList[i]["RefuseCount"] = calcCount;
							break;
						default:
							AllList[i]["AcceptCount"] = calcCount;
							break;
					}
				}
				break;
			}
		}
	}
	return AllList;
};
//设置备注录入信息值
Shell.infoCommon.setAllDtAcceptMemoOfPsaleDtlIDStr = function(data, acceptMemo, AllList) {
	var otype = data.otype;
	var isbreak = false;
	for(var i = 0; i < AllList.length; i++) {
		switch(otype) {
			case 2:
				var Id = data.Id;
				if(AllList[i]["Id"] == Id) {
					AllList[i]["AcceptMemo"] = acceptMemo;
					isbreak = true;
				}
				break;
			default:
				//明细列表
				var pSaleDtlIDStr = data.PSaleDtlIDStr;
				if(AllList[i]["PSaleDtlIDStr"] == pSaleDtlIDStr) {
					AllList[i]["AcceptMemo"] = acceptMemo;
					//isbreak = true;
				}
				break;
		}
		if(isbreak == true) break;
	}
	return AllList;
};
Shell.infoCommon.getTitlePre = function(model) {
	var titlePre = "";
	var barCodeMgr = "" + model.BmsCenSaleDtl_BarCodeMgr;
	switch(barCodeMgr) {
		case "1": //盒条码	
			titlePre = '<span style="padding:2px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
			break;
		case "0": //批条码	
			titlePre = '<span style="padding:2px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
			break;
		default:
			titlePre = '<span style="padding:2px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
			break;
	}
	return titlePre;
};
//获取对应的备注信息值
Shell.infoCommon.getAllDtAcceptMemoOfPsaleDtlIDStr = function(data, AllList) {
	var acceptMemo = "";
	var otype = data.otype;
	var isbreak = false;
	for(var i = 0; i < AllList.length; i++) {
		switch(otype) {
			case 2:
				var Id = data.Id;
				if(AllList[i]["Id"] == Id) {
					acceptMemo = AllList[i]["AcceptMemo"];
					isbreak = true;
				}
				break;
			default:
				//明细列表
				var pSaleDtlIDStr = data.PSaleDtlIDStr;
				if(AllList[i]["PSaleDtlIDStr"] == pSaleDtlIDStr) {
					acceptMemo = AllList[i]["AcceptMemo"];
					isbreak = true;
				}
				break;
		}
		if(isbreak == true) break;
	}
	return acceptMemo;
};
//盒条码的拒收按钮html生成
Shell.infoCommon.getRefuseCounthtmlOf1 = function(model, html, list) {
	var unit = model.GoodsUnit;
	var dtlCount = Shell.infoCommon.getdtlCount(model);
	var refuseCount = Shell.infoCommon.getRefuseCountOf1(model, list);
	var checkInfo = "";
	var divclass = "";
	if(refuseCount > dtlCount) refuseCount = dtlCount;
	divclass = "btn btn-sm btn-danger";
	checkInfo = "本次拒收:" + refuseCount + unit;
	html.push('<div class="' + divclass + '" style="width:112px;padding:2px;margin:2px;">' + checkInfo + '</div>');
	return html;
};
//盒条码的拒收按钮html生成
Shell.infoCommon.getCompleteRowhtml = function(model, html) {
	var scanCodeMark = parseInt(model.ScanCodeMark);
	var checkInfo = "";
	var divclass = "";
	divclass = "btn btn-sm btn-success";
	switch(scanCodeMark) {
		case 3:
			checkInfo = "已全部验收";
			break;
		case 4:
			checkInfo = "已全部拒收";
			divclass = "btn btn-sm btn-danger";
			break;
		case 5:
			checkInfo = "已全部验收";
			break;
		case 6:
			checkInfo = "盒条码已验收";
			divclass = "btn btn-sm btn-danger";
			break;
		case 7:
			checkInfo = "盒条码已拒收";
			break;
		default:
			break;
	}
	html.push('<div class="' + divclass + '" style="width:112px;padding:2px;margin:2px;">' + checkInfo + '</div>');
	return html;
};
//盒条码的拒收计算
Shell.infoCommon.getRefuseCountOf1 = function(model, list) {
	var barCodeMgr = "" + model.BarCodeMgr;
	var refuseCount = 0;
	if(model.RefuseCount) refuseCount = parseFloat(model.RefuseCount);
	if(barCodeMgr != "1") return refuseCount;
	if(barCodeMgr == "1") {
		refuseCount = 0;
		var mixSerial1 = model.PSaleDtlIDStr;
		if(!list) list = [];
		for(var i = 0; i < list.length; i++) {
			var mixSerial2 = list[i].PSaleDtlIDStr;
			var scanCodeMark = list[i].ScanCodeMark;
			//验收标志(0:待验收,扫码拒收:1,扫码接收:2,已存库验收:3,已存库拒收:4)
			if(mixSerial1 == mixSerial2 && (scanCodeMark == 1) && list[i].RefuseCount) {
				refuseCount += parseFloat(list[i].RefuseCount);
			}
		}
	}
	return refuseCount;
};
/***
 * 生成验收数量Html行
 * @param {Object} html
 * @param {Object} model
 * @param {Object} otype 明细列表显示:1;扫码明细列表显示:2
 */
Shell.infoCommon.getAcceptCountRowHtml = function(html, model, otype) {
	var dtId = model.Id;
	var acceptCount = parseFloat(model.AcceptCount);
	//同一试剂已入库总数
	var stockSumTotal = parseFloat(model.StockSumTotal);
	var dtlCount = Shell.infoCommon.getdtlCount(model);
	//数量的data封装属性值
	var data = {
		otype: otype, //明细列表显示:1;扫码明细列表显示:2
		isAccept: true, //是否接收:true为接收;false为拒收
		type: 1, //数量操作类型:验收数量减少1:1;数量直接录入:2;验收数量累加1:3;			
		dtId: dtId, //明细Id
		goodsQty: dtlCount, //明细的原始总数量
		stockSumTotal: stockSumTotal
	};
	if(!acceptCount) acceptCount = 0;

	//数量减少1的button				
	var data1 = Shell.util.JSON.encode(data);
	data1 = data1.replace(/"/g, "'");

	html.push('<div class="number-spinner" style="margin-bottom:2px;">');
	html.push('<button class="number-spinner-decrease" data="' + data1 + '">-</button>');
	//onkeyup="this.value=this.value.replace(/\\D/gi,\'\')" maxlength="5"
	html.push('<input type="number" min="0" value="' + acceptCount + '"');
	//数量直接录入
	data.type = 2;
	var data2 = Shell.util.JSON.encode(data);
	data2 = data2.replace(/"/g, "'");
	html.push(' data="' + data2 + '"');

	//给input添加id(明细Id+"-"+操作类型+"-"+isAccept),方便直接查找到
	var inputId1 = dtId + "-" + data.otype + "-" + data.isAccept;
	html.push(' id="' + inputId1 + '">');

	//数量累加1的button
	data.type = 3;
	var data3 = Shell.util.JSON.encode(data);
	data3 = data3.replace(/"/g, "'");
	html.push('<button class="number-spinner-increase" data="' + data3 + '">+</button>');

	html.push('</div>'); //end number-spinner
	return html;
};
/***
 * 生成拒收数量Html行
 * @param {Object} html
 * @param {Object} model
 * @param {Object} otype 明细列表显示:1;扫码明细列表显示:2
 */
Shell.infoCommon.getRefuseCountRowHtml = function(html, model, otype) {
	var dtId = model.Id;
	var refuseCount = parseFloat(model.RefuseCount);
	//同一试剂已入库总数
	var stockSumTotal = parseFloat(model.StockSumTotal);
	if(stockSumTotal) stockSumTotal = parseFloat(stockSumTotal);
	var dtlCount = Shell.infoCommon.getdtlCount(model);
	html.push('<div class="number-spinner" style="margin-bottom:2px;">');
	//数量的data封装属性值
	var data = {
		otype: otype, //明细列表显示:1;扫码明细列表显示:2
		isAccept: false, //是否接收:true为接收;false为拒收
		type: 1, //数量操作类型:验收数量减少1:1;验收数量累加1:2;数量直接录入:3;
		dtId: dtId, //明细Id
		goodsQty: dtlCount, //明细的原始总数量
		stockSumTotal: stockSumTotal
	};
	if(!refuseCount) refuseCount = 0;

	//数量减少1的button				
	var data1 = Shell.util.JSON.encode(data);
	data1 = data1.replace(/"/g, "'");
	html.push('<button class="number-spinner-decrease" data="' + data1 + '">-</button>');
	//onkeyup="this.value=this.value.replace(/\\D/gi,\'\')" maxlength="5" 
	html.push('<input type="number" min="0" value="' + refuseCount + '"');
	//数量直接录入
	data.type = 2;
	var data2 = Shell.util.JSON.encode(data);
	data2 = data2.replace(/"/g, "'");
	html.push(' data="' + data2 + '"');

	//给input添加id(明细Id+"-"+操作类型+"-"+isAccept)),方便直接查找到
	var inputId1 = dtId + "-" + data.otype + "-" + data.isAccept;
	html.push(' id="' + inputId1 + '">');

	//数量累加1的button
	data.type = 3;
	var data3 = Shell.util.JSON.encode(data);
	data3 = data3.replace(/"/g, "'");
	html.push('<button class="number-spinner-increase" data="' + data3 + '">+</button>');
	html.push('</div>'); //end number-spinner
	return html;
};
//联动时明细操作相关DIV显示或隐藏
Shell.infoCommon.setDtHidden = function(hidden) {
	switch(hidden) {
		case true:
			$("#dt-show-total").hide();
			$("#dt-div-buttons").hide();
			$("#dt-div-content").hide();
			break;
		default:
			$("#dt-show-total").show();
			$("#dt-div-buttons").show();
			$("#dt-div-content").show();
			break;
	}
};
//联动时扫码操作相关DIV显示或隐藏
Shell.infoCommon.setScanCodeHidden = function(hidden) {
	switch(hidden) {
		case true:
			$("#scancode-div-top").hide();
			$("#scancode-div-content").hide();
			$("#scancode-div-submit").hide();
			break;
		default:
			$("#scancode-div-top").show();
			$("#scancode-div-content").show();
			$("#scancode-div-submit").show();
			break;
	}
};
//提交按钮的显示或者隐藏 当全部明细的验收数量都为0时,隐藏,当有一种试剂的验收数量大于0时,显示
Shell.infoCommon.seSubmitBtnsHidden = function(AllList, hidden) {
	if(!hidden) hidden = true;
	if(AllList) {
		for(var i = 0; i < AllList.length; i++) {
			var acceptCount = parseFloat(AllList[i].AcceptCount);
			var refuseCount = parseFloat(AllList[i].RefuseCount);
			if(acceptCount > 0 || refuseCount > 0) {
				hidden = false;
				break;
			}
		}
	}
	switch(hidden) {
		case true:
			$("#dt-btn-save-check").hide();
			$("#dt-div-submit").hide();
			break;
		default:
			$("#dt-btn-save-check").show();
			$("#dt-div-submit").show();
			break;
	}
};
/**
 * 验收标志(0:待验收,扫码拒收:1,扫码接收:2,已全部验收:3,已全部拒收:4;已全部入库(已验收数+已拒收数):5,
 * 某一盒条码已验收:6,某一盒条码已拒收:7)
 * @param {Object} model
 */
Shell.infoCommon.isCompleteOfScanCodeMark = function(model) {
	var isComplete = false;
	var scanCodeMark = model.ScanCodeMark;
	//|| scanCodeMark == 6 || scanCodeMark == 7
	if(scanCodeMark == 3 || scanCodeMark == 4 || scanCodeMark == 5) {
		isComplete = true;
	}
	return isComplete;
};
//某一试剂明细是否确认(接收或拒收)保存
Shell.infoCommon.isConfirmOfScanCodeMark = function(model) {
	var isConfirm = false;
	var scanCodeMark = model.ScanCodeMark;
	if(scanCodeMark == 3 || scanCodeMark == 4 || scanCodeMark == 5 || scanCodeMark == 6 || scanCodeMark == 7) {
		isConfirm = true;
	}
	return isConfirm;
};
Shell.infoCommon.getdtlCount = function(model) {
	var goodsQty = model.GoodsQty;
	if(goodsQty) goodsQty = parseFloat(goodsQty);
	var dtlCount = model.DtlCount;
	if(dtlCount) dtlCount = parseFloat(dtlCount);
	//兼容因为在早期时的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
	if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;
	return dtlCount;
};
//验收确认验证
Shell.infoCommon.checkVerification = function(ACCOUNT) {
	var accountValue = $("#Account").val();
	var pwdValue = $("#Pwd").val();
	if(!accountValue || !pwdValue) {
		ShellComponent.messagebox.msg('必须填写供货确认账号及密码才能验收，请填写后操作！');
		$("#Account").focus();
		return false;
	}
	if(accountValue == ACCOUNT) {
		ShellComponent.messagebox.msg('验收时，供货确认账号不能是登录者本人，请重新填写！');
		$("#Account").focus();
		return false;
	}
	return true;
};
//显示验收需要双确认的录入信息
Shell.infoCommon.showCheckUserInfo = function(SECACCEPTERTYPE) {
	if(SECACCEPTERTYPE) SECACCEPTERTYPE = parseFloat(SECACCEPTERTYPE);
	switch(SECACCEPTERTYPE) {
		case 0:
			$("#checkUserInfo").hide();
			$("#checkInfoMsg").hide();
			$("#Account").hide();
			$("#Pwd").hide();
			break;
		default:
			$("#checkUserInfo").show();
			$("#checkInfoMsg").show();
			$("#Account").show();
			$("#Pwd").show();
			break;
	}
};
//封装提交的数据信息
Shell.infoCommon.getSaveParamsData = function(SECACCEPTERTYPE, AllList) {
	var accountValue = $("#Account").val();
	var pwdValue = $("#Pwd").val();
	var invoiceNoValue = $("#InvoiceNo").val();
	var accepterMemoValue = $("#AccepterMemo").val();

	if(accountValue) accountValue = accountValue.replace(/"/g, '');
	if(pwdValue) pwdValue = pwdValue.replace(/"/g, '');
	if(invoiceNoValue) {
		invoiceNoValue = invoiceNoValue.replace(/"/g, "");
		invoiceNoValue = invoiceNoValue.replace(/\\/g, '');
		invoiceNoValue = invoiceNoValue.replace(/[\r\n]/g, '');
	}
	if(accepterMemoValue) {
		accepterMemoValue = accepterMemoValue.replace(/"/g, "");
		accepterMemoValue = accepterMemoValue.replace(/\\/g, '');
		accepterMemoValue = accepterMemoValue.replace(/[\r\n]/g, '');
	}
	if(SECACCEPTERTYPE) SECACCEPTERTYPE = parseFloat(SECACCEPTERTYPE);
	var params = {
		invoiceNo: invoiceNoValue,
		accepterMemo: accepterMemoValue,
		secAccepterAccount: accountValue,
		secAccepterPwd: pwdValue
	};
	//验收时不需要双确认时,secAccepterType不用传入
	if(SECACCEPTERTYPE && SECACCEPTERTYPE != 0) params.secAccepterType = SECACCEPTERTYPE;

	//验收明细单实体列表
	var listEntity = [];
	var refuseCount = 0;
	var acceptCount = 0;
	var barCodeMgr = "";
	var mark = 0;
	for(var i = 0; i < AllList.length; i++) {
		mark = parseInt(AllList[i].ScanCodeMark);
		//待验收(本次没扫码),不处理
		if(mark == 0) continue;
		//是否已(接收/拒收)确认保存过
		var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(AllList[i]);
		if(isConfirm == true) continue;

		barCodeMgr = "" + AllList[i].BarCodeMgr;
		refuseCount = 0;
		acceptCount = 0;
		switch(barCodeMgr) {
			case "1": //盒条码
				mark == 2 ? (acceptCount = 1) : (refuseCount = 1);
				break;
			default:
				//当次的接收或拒收入库
				acceptCount = AllList[i].AcceptCount;
				if(acceptCount) acceptCount = parseFloat(acceptCount);
				if(!acceptCount) acceptCount = 0;

				refuseCount = AllList[i].RefuseCount;
				if(refuseCount) refuseCount = parseFloat(refuseCount);
				if(!refuseCount) refuseCount = 0;

				var dtlCount = Shell.infoCommon.getdtlCount(AllList[i]);
				var stockSumTotal = AllList[i].StockSumTotal;
				if(stockSumTotal) stockSumTotal = parseFloat(stockSumTotal);
				//试剂当前可扫码总数量
				var tempDtlCount = dtlCount - stockSumTotal;

				//当次的验收或拒收数量不能大于试剂当前可扫码总数量
				if(acceptCount > tempDtlCount) acceptCount = tempDtlCount;
				if(refuseCount > tempDtlCount) refuseCount = tempDtlCount;

				if(acceptCount < 0) acceptCount = 0;
				if(refuseCount < 0) refuseCount = 0;
				acceptCount = acceptCount;
				refuseCount = refuseCount;
				break;
		}
		//当前明细的验收标志值不为已存库验收=3并且不为已存库拒收=4,且当次验收数量或当次拒收数量大于0
		if(mark != 0 && (acceptCount > 0 || refuseCount > 0)) {
			listEntity.push({
				Id: -1,
				AcceptCount: acceptCount,
				RefuseCount: refuseCount,
				AcceptMemo: AllList[i].AcceptMemo,
				"BmsCenSaleDtl": {
					Id: AllList[i].Id
				}
			});
		}
	}
	params.listEntity = listEntity;
	return params;
};
/****
 * 先验收所有明细的验收数量
 * 1.如果不是全部明细的验收数量为0,有一种试剂明细为0，弹出提示,可以关闭提示或者点确认后继续验收
 * 2.全部是0的，弹出提示,可以关掉提示,不能操作验收确认。
 * resultType:验收数量的验证结果类型(0:继续操作;1:弹出提示,可以关闭提示或者点确认后继续验收;2:弹出提示,可以关掉提示,不能操作验收确认)
 */
Shell.infoCommon.verificationSave = function(AllList) {
	var allAcceptCount = 0;
	var map = {};
	var result = {
		"resultType": 0,
		"msg": ""
	};
	//先合并明细
	for(var i = 0; i < AllList.length; i++) {
		var model = $.extend(true, {}, AllList[i]);
		var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(model);
		if(isConfirm == true) continue;

		//条码类型
		var barCodeMgr = "" + model.BarCodeMgr;

		var acceptCount = model.AcceptCount;
		if(acceptCount) acceptCount = parseFloat(acceptCount);
		if(!acceptCount) acceptCount = 0;

		var refuseCount = AllList[i].RefuseCount;
		if(refuseCount) refuseCount = parseFloat(refuseCount);
		if(!refuseCount) refuseCount = 0;
		//所有的明细验收及拒收数量累加
		allAcceptCount += (acceptCount + refuseCount);

		//混合条码MixSerial
		var mixSerial = model.PSaleDtlIDStr;
		if(!mixSerial) continue;

		if(mixSerial && !map[mixSerial]) {
			map[mixSerial] = model;
		} else {
			if(barCodeMgr == "1") {
				map[mixSerial].AcceptCount = acceptCount +
					parseFloat(map[mixSerial].AcceptCount);
				map[mixSerial].RefuseCount = refuseCount +
					parseFloat(map[mixSerial].RefuseCount);
			}
		}
	}
	if(allAcceptCount > 0) {
		for(var m in map) {
			var tempCount = map[m].AcceptCount + map[m].RefuseCount;
			var dtlCount = Shell.infoCommon.getdtlCount(map[m]);
			var stockSumTotal = map[m].StockSumTotal;
			if(stockSumTotal) stockSumTotal = parseFloat(stockSumTotal);
			//试剂当前可扫码总数量
			var tempDtlCount = dtlCount - stockSumTotal;
			if(tempCount == 0) {
				result.resultType = 1;
				result.msg = "当前操作存在扫码数量为0的试剂，请确认是否继续执行操作，点【确定】按钮可以继续验收！";
			}
			if(tempCount > tempDtlCount) {
				result.resultType = 3;
				result.msg = "试剂为" + map[m].GoodsName + ",当次操作的接收扫码数量为" + map[m].AcceptCount + "拒收扫码数量为" + map[m].RefuseCount + "，大于试剂当前可扫码总数量" + tempDtlCount + ",不能确认提交，请修改后再提交！";
				break;
			}
		}
	} else {
		result.resultType = 2;
		result.msg = "当次操作的所有试剂明细的扫码数量为0，不能确认提交，请扫码后再提交！";
	}
	return result;
};