/***
 * 供货单分批验收
 */
$(function() {
	//外部参数
	var params = Shell.util.getRequestParams(true);
	var PK = params.ID || "";
	//账号
	var ACCOUNT = params.ACCOUNT || "";
	//密码
	var PASSWORD = params.PASSWORD || "";
	//验收时是否需要双确认:不需要:0(不需要双确认时,验收时secAccepterType值不用传回后台),默认本实验室:1,供应商:2,供应商或实验室:3
	var SECACCEPTERTYPE = params.SECACCEPTERTYPE || 0;
	//选中的按钮标志,1=重置,2=整单验收
	var CHECKED_BUTTON = null;
	//所有明细数据
	var AllList = [];
	//当前已经扫码的条码集合
	var CurBarCodeList = [];
	var DEFAULT_TITLE = "待验收信息";
	//接收扫码的标题
	var SUCCESS_TITLE = "接收扫码";
	//拒收扫码的标题
	var ERROR_TITLE = "拒收扫码";
	//接收扫码的提示内容
	var SUCCESS_MSG = "凡接收扫码的产品都是通过验收的";
	//拒收扫码的提示内容
	var ERROR_MSG = "凡拒收扫码的产品都是不验收的";
	//当次已扫条码总数量
	var SCANCODECOUNT = 0;
	//扫码用的扫码标志值:1:拒收扫码,2:接收扫码;
	var MARK = 2;
	//显示类型:1是明细类型操作;2是扫码操作
	var SHOWTYPE = 1;

	//初始化监听
	function initListeners() {
		//返回按钮监听
		$("#dt-div-top-back").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			topback();
		});
		//明细重置
		$("#btn-reset-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			SHOWTYPE = 1;
			CHECKED_BUTTON = 1;
			showOperateConfirm("确定重置数据吗？");
		});
		//明细拒收扫码
		$("#btn-error-scancode").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			MARK = 1;
			SHOWTYPE = 2;
			showScanCode();
		});
		//明细接收扫码
		$("#btn-right-scancode").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			MARK = 2;
			SHOWTYPE = 2;
			CHECKED_BUTTON = 5;
			showScanCode();
		});
		//明细整单验收
		$("#btn-alldata-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			MARK = 2;
			SHOWTYPE = 1;
			CHECKED_BUTTON = 2;
			showOperateConfirm("确定整单验收吗？");
		});
		//明细确认提交操作
		$("#dt-btn-save-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			onverificationAccept();
		});
		//保存确认提交处理
		$("#btn-check-confirm").on("click", function() {
			onConfirmSaveCheckClick();
		});
		//明细弹出确定按钮处理
		$("#btn-operate-confirm").on("click", function() {
			onOperateConfirm();
		});
		//扫码--当按下回车键时扫码	
		$('#scancode-text').keydown(function(e) {
			if(e.keyCode == 13) {
				var barcode = $('#scancode-text').val();
				if(!barcode) return;
				onScanCodeAccept(barcode);
			}
		});
		//扫码--确定扫码
		$("#btn-confirm-scancode").on(Shell.util.Event.click, function(e) {
			if(!Shell.util.Event.isClick()) return;
			confirmMarkLocalData();
		});
		//明细列表每一行的备注录入确认提交处理
		$("#btn-confirm-AcceptMemo").on("click", function() {
			var acceptMemo = $('#txtAcceptMemo').val();
			var data = $('#txtAcceptMemoData').val();
			var data1 = Shell.util.JSON.decode(data);
			if(!data) {
				$("#modal-operate-AcceptMemo").modal("hide");
				return;
			}
			AllList = Shell.infoCommon.setAllDtAcceptMemoOfPsaleDtlIDStr(data1, acceptMemo, AllList);
			$("#modal-operate-AcceptMemo").modal("hide");
		});
	}
	//弹出的操作提示的确定按钮处理
	function onOperateConfirm() {
		$("#modal-operate-confirm").modal("hide");
		if(SHOWTYPE == 2) {
			$('#scancode-text').focus();
		} else {
			switch(CHECKED_BUTTON) {
				case 1: //明细重置
					refreshDataAndContent(4);
					Shell.infoCommon.seSubmitBtnsHidden(AllList, true);
					break;
				case 2: //明细整单验收
					allDataCheck(3);
					Shell.infoCommon.seSubmitBtnsHidden(AllList, false);
					break;
				case 3: //确认提交按钮验收数量有某种试剂为0的处理
					onShowConfirm();
					break;
				case 4: //确认提交按钮验收数量全部为0的处理,停止保存
					break;
				default:
					ShellComponent.messagebox.msg("操作错误！");
					break;
			}
		}
	}
	//明细列表及扫码明细列表的事件监听
	function initDtListener() {
		var isPC = Shell.util.Event.isPC();
		Shell.util.Event.click = "touchend";
		if(isPC) Shell.util.Event.click = "click";
		//初始化列表明细行的备注按钮监听  name=AcceptMemo"
		$(".input-group-addon").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			var name = $(this).attr("name");
			if(name != "AcceptMemo") return;
			var data = $(this).attr("data");
			if(!data) return;
			var data1 = Shell.util.JSON.decode(data);
			if(!data1) return;

			var acceptMemo = Shell.infoCommon.getAllDtAcceptMemoOfPsaleDtlIDStr(data1, AllList);
			$("#txtAcceptMemo").val(acceptMemo);
			$("#txtAcceptMemoData").val(data);
			$("#modal-operate-AcceptMemo-footer").show();
			$('#txtAcceptMemo').focus();
			$("#modal-operate-AcceptMemo").modal({
				backdrop: 'static',
				keyboard: false
			});
		});
		//数量加减监听
		$(".number-spinner button").on(Shell.util.Event.click, function(e) {
			//e.preventDefault();
			if(!Shell.util.Event.isClick()) return;
			var numberDiv = $(this).parent(),
				input = $(numberDiv).children("input");
			var data = $(this).attr("data");
			if(!data) return;
			if(data) data = Shell.util.JSON.decode(data);
			if(!data) return;

			var otype = data.otype;
			switch(otype) {
				case 1: //明细列表
					onDtSpinnerButtonClick(input, data);
					break;
				case 2: //扫码明细列表
					onScancodeSpinnerButtonClick(input, data);
					break;
				default:
					break;
			}
		});
		//监听数量录入框直接录入值后处理
		$(".number-spinner input").on("change", function(e) {
			e.preventDefault();
			if(!Shell.util.Event.isClick()) return;
			var input = $(this);
			var data = $(this).attr("data");
			if(!data) return;
			if(data) data = Shell.util.JSON.decode(data);
			if(!data) return;
			var otype = data.otype;
			switch(otype) {
				case 1: //明细列表
					onDtInputChange(input, data);
					break;
				case 2: //扫码明细列表
					onScancodeInputChange(input, data);
					break;
				default:
					break;
			}
		});
	}
	//明细列表录入框--监听数量输入框累加或减少后联动更新
	function onDtSpinnerButtonClick(input, data) {
		//原来的数量值
		var old = parseFloat(input.val() || "0");
		var now = old;
		//验收数量
		var nowValue = 0;
		//数量操作类型
		switch(parseInt(data.type)) {
			case 1: //数量减少1
				now = now - 1;
				//数量只能是>=0
				if(now < 0) now = 0;
				nowValue = now;
				break;
			case 3: //数量累加1
				now = now + 1;
				//数量只能是<=原始总数量-同一试剂已入库总数
				var goodsQty = data.goodsQty - data.stockSumTotal;
				if(now > goodsQty) now = goodsQty;
				nowValue = now;
				break;
			default:
				break;
		}
		input.val(now);
		input.text(now);
		//明细的验收数量更新
		AllList = Shell.infoCommon.calcAcceptCount(data, nowValue, AllList);
		//验收总金额更新
		setShowSumTotal();
		Shell.infoCommon.seSubmitBtnsHidden(AllList);
	}
	//明细列表--数量输入框直接录入后联动更新
	function onDtInputChange(input, data) {
		//当前数量值
		var nowValue = 0;
		var now = parseFloat(input.val() || "0");

		//操作类型
		switch(parseInt(data.type)) {
			case 2: //明细列表里的验收数量直接录入
				//数量只能是>=0
				if(now < 0) now = 0;
				//数量只能是<=原始总数量-同一试剂已入库总数
				var goodsQty = data.goodsQty - data.stockSumTotal;
				if(now > goodsQty) now = goodsQty;
				nowValue = now;
				break;
			default:
				break;
		}
		input.val(now);
		input.text(now);
		//供货单明细列表的录入数量更新
		AllList = Shell.infoCommon.calcAcceptCount(data, nowValue, AllList);
		//验收总金额更新
		setShowSumTotal();
		Shell.infoCommon.seSubmitBtnsHidden(AllList);
	}
	//明细列表--供货单整单验收
	function allDataCheck(mark) {
		SHOWTYPE = 1;
		refreshDataAndContent(mark);
		Shell.infoCommon.seSubmitBtnsHidden(AllList, false);
	}
	//明细--打开供货单明细扫码
	function showScanCode() {
		setTitle();
		resetScanCodeParams();
		Shell.infoCommon.setDtHidden(true);
		Shell.infoCommon.setScanCodeHidden(false);
	}
	//明细--创建供货单详细列表内容
	function createHtmlOfDt(list) {
		list = list || [];
		var html = [];
		html.push('<div>');
		for(var i = 0; i < list.length; i++) {
			var row = createRowOfDt(list[i], 1);
			html.push(row);
		}
		if(list.length == 0) html.push('<div class="no-data-div">没有找到数据!</div>');
		html.push('</div>');
		return html.join('');
	}
	/***
	 * 创建明细行内容
	 * @param {Object} model 为合并行后的信息
	 * @param {Object} otype 明细列表显示:1;扫码明细列表显示:2
	 */
	function createRowOfDt(model, otype) {
		var html = [];
		var dtId = model.Id;
		var barCodeMgr = "" + model.BarCodeMgr;
		var dtlCount = Shell.infoCommon.getdtlCount(model);
		//已存库的验收数量
		var acceptCounted = parseFloat(model.AcceptCounted);
		//已存库的拒收数量
		var refuseCounted = parseFloat(model.RefuseCounted);
		//明细的同一试剂入库总数(接收+拒收)
		var stockSumTotal = parseFloat(model.StockSumTotal);
		var price = parseFloat(model.Price);
		var unit = model.GoodsUnit;
		//总额
		var sumTotal = model.SumTotal;
		if(sumTotal && sumTotal.toString().indexOf('.') > 0) sumTotal = parseFloat(sumTotal).toFixed(2);
		var invalidTime = Shell.util.Date.getDate(model.InvalidDate);
		//该试剂是否已全部验收完
		var isComplete = Shell.infoCommon.isCompleteOfScanCodeMark(model);
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
		var title = '' + model.GoodsName + '';
		var LotNo = '批号:' + model.LotNo;
		var InvalidDate = '有效期至:' + Shell.util.Date.toString(model.InvalidDate, true);
		var UnitMemo = '规格:' + model.UnitMemo;
		var Price = '单价:' + price + "/" + unit + ' 总额:<span style="color:#d9534f;font-weight:bold;">' + sumTotal + '</span>元';

		var margintop = "-73px";
		var titlePre = Shell.confirm.common.getTitlePre(model);
		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<div class="list-group-item" data="' + dtId + '" style="margin:2px;">');

		html.push('<h4 class="list-group-item-heading">' + titlePre + title + '</h4>');
		//扫码明细显示
		if(otype == 2) {
			var mixSerial = '条码:' + Shell.confirm.common.getBarCodeOfMixSerial(model, false);
			html.push('<div class="list-group-item-text" style="' + pStyle + '">' + mixSerial + '</div>');
		}
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + LotNo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UnitMemo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + Price + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + InvalidDate + '</div>');
		//-----本次接收或拒收的浮动层信息开始-------
		//if(isComplete == true) margintop = "-45px";
		html.push('<div style="margin-top:' + margintop + ';width:118px;float:right;">');
		switch(barCodeMgr) {
			case "1":
				//otype 明细列表显示:1;扫码明细列表显示:2
				switch(otype) {
					case 2:
						html = Shell.infoCommon.getAcceptCountRowHtmlOf2(model, html, [model]);
						html = Shell.infoCommon.getRefuseCounthtmlOf1(model, html, [model]);
						break;
					default:
						if(isComplete == true) {
							html = Shell.infoCommon.getCompleteRowhtml(model, html);
						} else {
							html = Shell.infoCommon.getAcceptCountRowHtmlOf2(model, html, AllList);
							html = Shell.infoCommon.getRefuseCounthtmlOf1(model, html, AllList);
						}
						break;
				}
				break;
			default:
				//批条码(非盒条码):0、无条码:2
				if(isComplete == true) {
					html = Shell.infoCommon.getCompleteRowhtml(model, html);
				} else {
					html = Shell.infoCommon.getAcceptCountRowHtml(html, model, otype);
					html = Shell.infoCommon.getRefuseCountRowHtml(html, model, otype);
				}
				break;
		}

		html.push('</div>');

		if(barCodeMgr != "1" && isComplete == false) {
			html.push('<div style="float:right;margin-top:-65px;spadding:2px;margin-right:118px;width:30px;text-align:right;background-color: #5cb85c;color:#fff;">接收</div>');
			//清除浮动
			html.push('<div style="clear: both;"></div>');
			html.push('<div style="float:right;margin-top:-30px;spadding:2px;margin-right:118px;width:30px;text-align:right;    background-color: #d9534f;color:#fff;">拒收</div>');
		}

		//-----本次接收或拒收的浮动层信息结束-------	
		html.push('<div class="input-group" style="width:100%;margin:2px;padding:2px;">');
		html.push('<span style="width:80px;color:#fff;font-weight:bold;background-color: #f0ad4e;">&nbsp;供货:' + dtlCount + '&nbsp;</span>');
		html.push('<span style="width:80px;color:#fff;font-weight:bold;background-color: #5cb85c;">&nbsp;已验收:' + acceptCounted + '&nbsp;</span>');
		html.push('<span style="width:80px;color:#fff;font-weight:bold;background-color:#d9534f;">&nbsp;已拒收:' + refuseCounted + '&nbsp;</span>');

		if(isComplete == false) {
			//区分是明细列表的备注还是扫码明细的备注按钮
			var data1 = {
				otype: otype, //明细列表显示:1;扫码明细列表显示:2
				PSaleDtlIDStr: model.PSaleDtlIDStr,
				Id: model.Id
			};
			data1 = Shell.util.JSON.encode(data1);
			data1 = data1.replace(/"/g, "'");
			html.push('<div class="input-group-addon" name="AcceptMemo" style="width:55px; padding:2px;margin:2px;color:#d9534f;font-weight:bold;"data="' + data1 + '">备注</div>');
		}

		html.push('</div>');

		html.push('</div>');
		html.push('</div>');
		return html.join('');
	}
	/***
	 * 明细--重置或整单接收处理
	 * @param {Object} mark 标志:0为没有扫码确认;3为整单接收;4为整单重置;
	 */
	function refreshDataAndContent(mark) {
		if(!AllList) AllList = [];
		Shell.infoCommon.seSubmitBtnsHidden(AllList, true);
		AllList = Shell.infoCommon.setAllListofMark(AllList, mark);
		var data = Shell.infoCommon.mergerData(AllList);
		var dtlHtml = createHtmlOfDt(data);
		$("#dt-div-content").html("");
		$("#dt-div-content").html(dtlHtml);
		initDtListener();
		setShowSumTotal();
		Shell.infoCommon.seSubmitBtnsHidden(AllList);
	}
	//设置显示金额 #d9534f  #d9534f
	function setShowSumTotal() {
		var html = "";
		var result = Shell.infoCommon.calcSumTotal(AllList);

		var allSumTotal = result.allSumTotal;
		var checkSumTotal = result.checkSumTotal;
		var refuseumTotal = result.refuseumTotal;
		if(checkSumTotal > allSumTotal) checkSumTotal = allSumTotal;

		html += '<span>供货:<b style="color:#d9534f;">' + allSumTotal + '</b>元</span>&nbsp;&nbsp;';
		html += '<span>本次验收:<b style="color:#d9534f;">' + checkSumTotal + '</b>元</span>&nbsp;&nbsp;';
		if(checkSumTotal != allSumTotal) {
			html += '<span style="padding:2px;background-color:#d9534f;color:#ffffff;font-size:10px;">缺</span>';
		}
		html += '<span>&nbsp;&nbsp;本次拒收:<b style="color:#d9534f;">' + refuseumTotal + '</b>元</span>&nbsp;&nbsp;';
		$("#dt-show-total").html(html);
	}
	//明细--加载明细列表数据
	function loadDtContent() {
		$("#dt-btn-save-check").hide();
		$("#dt-div-submit").hide();
		if(AllList && AllList.length > 0) {
			var data = Shell.infoCommon.mergerData(AllList);
			var dtlHtml = createHtmlOfDt(data);
			$("#dt-div-content").html(dtlHtml);
			initDtListener();
			setShowSumTotal();
			Shell.infoCommon.seSubmitBtnsHidden(AllList);
		} else {
			AllList = [];
			//加载供货单明细列表数据
			ShellComponent.mask.loading();
			Shell.infoCommon.loadDtlDataById(PK, function(value) {
				ShellComponent.mask.hide();
				if(value.success) {
					AllList = value.value.list;
					if(!AllList)AllList = [];
					var data = Shell.infoCommon.mergerData(AllList);
					var dtlHtml = createHtmlOfDt(data);
					$("#dt-div-content").html(dtlHtml);
					initDtListener();
					setShowSumTotal();
					Shell.infoCommon.seSubmitBtnsHidden(AllList);
				} else {
					AllList = [];
					var msg = value.msg;
					if(!msg) msg = "获取供货单明细出错了!";
					$("#dt-div-content").html('<div class="error-div">' + msg + '</div>');
					$("#dt-btn-save-check").hide();
					$("#dt-div-submit").hide();
				}
			});
		}
	}
	/***
	 * 扫码--监听数量值累加或减少1按钮的处理
	 * @param {Object} input
	 * @param {Object} data
	 */
	function onScancodeSpinnerButtonClick2(input, data) {
		var inputValue = parseFloat(input.val() || "0");
		if(data.type == 1) { //拒收
			inputValue -= 1;
			if(inputValue < 0) inputValue = 0;
		} else if(data.type == 3) { //接收
			inputValue += 1;
		}
		setInputValue(input, data, inputValue);
	}
	//扫码--监听数量值累加或减少1按钮的处理
	function onScancodeSpinnerButtonClick(input, data) {
		//原来的数量值
		var old = parseFloat(input.val() || "0");
		var now = old;
		//验收数量
		var nowValue = 0;
		//数量操作类型
		switch(parseInt(data.type)) {
			case 1: //数量减少1
				now = now - 1;
				//数量只能是>=0
				if(now < 0) now = 0;
				nowValue = now;
				break;
			case 3: //数量累加1
				now = now + 1;
				//数量只能是<=原始总数量-同一试剂已验收总数
				var goodsQty = data.goodsQty - data.stockSumTotal;
				if(now > goodsQty) now = goodsQty;
				nowValue = now;
				break;
			default:
				break;
		}
		//input.val(now);
		//input.text(now);
		setInputValue(input, data, nowValue);
	}
	/***
	 * 扫码--监听数量值直接录入改变后处理
	 * @param {Object} input
	 * @param {Object} data
	 */
	function onScancodeInputChange(input, data) {
		var inputValue = parseFloat(input.val() || "0");
		setInputValue(input, data, inputValue);
	}
	//数量累加1或自减1后接收及拒收数量的值处理
	function setInputValue(input, data, inputValue) {
		var dtId = data.dtId;
		for(var i = 0; i < CurBarCodeList.length; i++) {
			var dtId2 = CurBarCodeList[i]["Id"];
			var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(CurBarCodeList[i]);
			if(isConfirm == true) break;

			if(dtId == dtId2) {
				var curRow = CurBarCodeList[i];
				//当前输入框的对应另一个输入框(如当前是接收输入框,对应的就为拒收输入框)
				var isAccept = !data.isAccept;
				var inputId2 = curRow.Id + "-" + data.otype + "-" + isAccept;
				var input2 = $("#" + inputId2);
				var data2 = $(input2).attr("data");
				if(!data2) return;
				if(data2) data2 = Shell.util.JSON.decode(data2);
				var inputValue2 = parseFloat(input2.val() || "0");

				var dtlCount = Shell.infoCommon.getdtlCount(curRow);
				//试剂当前可扫码总数量
				var tempDtlCount = Shell.infoCommon.getTempDtlCount(curRow);

				var acceptCount = parseFloat(curRow.AcceptCount);
				if(!acceptCount) acceptCount = 0;
				var refuseCount = parseFloat(curRow.RefuseCount);
				if(!refuseCount) refuseCount = 0;

				//如果当前输入框为接收输入框
				if(data.isAccept == true) {
					acceptCount = inputValue;
					refuseCount = inputValue2;
				} else {
					acceptCount = inputValue2;
					refuseCount = inputValue;
				}

				//当前输入的接收或拒收数值
				if(inputValue > tempDtlCount) inputValue = tempDtlCount;
				if(inputValue < 0) inputValue = 0;
				//当前扫码总数
				var tempCurCount = inputValue + inputValue2;
				if(tempCurCount >= tempDtlCount) {
					//如果当前输入框为接收输入框
					if(data.isAccept == true) {
						refuseCount = refuseCount - 1;
						if(acceptCount < 0) acceptCount = 0;
						acceptCount = tempDtlCount - refuseCount;
						inputValue = acceptCount;
						inputValue2 = refuseCount;
					} else {
						acceptCount = acceptCount - 1;
						if(refuseCount < 0) refuseCount = 0;
						refuseCount = tempDtlCount - acceptCount;
						inputValue2 = acceptCount;
						inputValue = refuseCount;
					}
				}
				if(inputValue < 0) inputValue = 0;
				if(inputValue2 < 0) inputValue2 = 0;
				if(inputValue>tempDtlCount) inputValue =tempDtlCount;
				if(inputValue2>tempDtlCount) inputValue2 =tempDtlCount;				
				$(input).val(inputValue);
				if(input2) $(input2).val(inputValue2);

				if(acceptCount < 0) acceptCount = 0;
				if(refuseCount < 0) refuseCount = 0;				
				if(acceptCount>tempDtlCount) acceptCount =tempDtlCount;
				if(refuseCount>tempDtlCount) refuseCount =tempDtlCount;				
				CurBarCodeList[i]["AcceptCount"] = acceptCount;
				CurBarCodeList[i]["RefuseCount"] = refuseCount;
				break;
			}
		}
		SCANCODECOUNT = Shell.infoCommon.calcScancodeCount(CurBarCodeList, MARK);
		setShowCountOfScancode();
	}
	/***
	 * 扫码--依扫码找出明细行并更新扫码标志
	 * 需要判断是否已经被接收入库或拒收入库
	 * @param {Object} barcode
	 */
	function onScanCodeAccept(barcode) {
		if(AllList.length == 0) return;
		//当前条码是否存在所有明细的集合里的
		var isExistAll = false;
		//当前条码是否存在当前已经扫过的明细的集合里
		var isExistCur = false;
		var isExec = true;
		//当前条码在明细里的行信息
		var curRow = null;
		//当前条码的条码类型
		var curBarCodeMgr = "";
		//当前条码在已扫条码明细里的索引
		var curRowIndex = 0;
		var msgInfo = "";
		//当前扫码是否存在里供货单明细中
		for(var i = 0; i < AllList.length; i++) {
			var barcode2 = AllList[i][Shell.confirm.common.SCANCODEFILE];
			if(barcode == barcode2) {
				curRow = $.extend(true, {}, AllList[i]);
				isExistAll = true;
				break;
			}
		}
		//判断是否已经被验收(被接收入库或拒收入库)
		if(curRow) {
			curBarCodeMgr = "" + AllList[i].BarCodeMgr;
			//标志(0:待验收,扫码拒收:1,扫码接收:2,已全部验收:3,已全部拒收:4;已全部入库(已验收数+已拒收数):5)
			var scanCodeMark = curRow.ScanCodeMark;
			var tempMsg = "";
			switch(scanCodeMark) {
				case 3:
					tempMsg = "已全部扫码验收";
					break;
				case 4:
					tempMsg = "已全部扫码拒收";
					break;
				case 5:
					tempMsg = "已全部扫码完成";
					break;
				case 6:
					tempMsg = "已被验收确认";
					break;
				case 7:
					tempMsg = "已被拒收确认";
					break;
				default:
					break;
			}
			if(tempMsg && tempMsg.length > 0) {
				isExistAll == false;
				isExec = false;
				tempMsg = tempMsg + ",请不要重复扫码!";
				msgInfo += "条码为:" + barcode + "的产品," + tempMsg + "!";
			}
		}

		if(isExistAll == true && isExec == true) {
			//判断是否存在当前扫码明细里
			for(var i = 0; i < CurBarCodeList.length; i++) {
				var barcode2 = CurBarCodeList[i][Shell.confirm.common.SCANCODEFILE];
				if(barcode == barcode2) {
					//不是盒条码的,以当次已扫的条码明细行为当前条码行信息,方便累加扫码(接收或拒收)数量
					if(curBarCodeMgr != "1") curRow = CurBarCodeList[i];
					isExistCur = true;
					curRowIndex = i;
					break;
				}
			}
			//试剂当前可扫码总数量
			var tempDtlCount = Shell.infoCommon.getTempDtlCount(curRow);
			//当次扫码接收数量
			var acceptCount = parseFloat(curRow.AcceptCount);
			if(!acceptCount) acceptCount = 0;
			//当次扫码拒收数量
			var refuseCount = parseFloat(curRow.RefuseCount);
			if(!refuseCount) refuseCount = 0;
			var goodsQty = curRow.GoodsQty;
			if(goodsQty) goodsQty = parseFloat(goodsQty);
			switch(curBarCodeMgr) {
				case "1": //盒条码	
					if(isExistCur == false) {
						curRow.ScanCodeMark = MARK;
						//如果当前扫码为接收,当前扫码明细验收数量为1 :2017-12-22
						//curRow.AcceptCount = (MARK == 2 ? 1 : 0);
						//curRow.RefuseCount = (MARK == 1 ? 1 : 0);

						//如果当前扫码为接收,当前扫码明细验收数量为goodsQty:2017-12-22(以支持goodsQty为小数点)
						curRow.AcceptCount = (MARK == 2 ? goodsQty : 0);
						curRow.RefuseCount = (MARK == 1 ? goodsQty : 0);

						CurBarCodeList.splice(0, 0, curRow);
						createHtmlOfScanCode();
					}
					break;
				default: //批条码
					switch(MARK) {
						case 2: //接收扫码
							acceptCount += 1;
							//验收数量只能是小于等于原始数量并大于等于0
							if(acceptCount > tempDtlCount) {
								acceptCount = tempDtlCount;
							} else {
								SCANCODECOUNT += 1;
							}
							break;
						default:
							refuseCount += 1;
							if(refuseCount > tempDtlCount) {
								refuseCount = tempDtlCount;
							} else {
								SCANCODECOUNT += 1;
							}
							if(refuseCount < 0) refuseCount = 0;
							break;
					}
					//当次的接收数量加当次拒收数量的值大于试剂当前可扫码总数量处理
					if((acceptCount + refuseCount) > tempDtlCount) {
						switch(MARK) {
							case 2: //接收扫码
								refuseCount = refuseCount - 1;
								break;
							default:
								acceptCount = acceptCount - 1;
								break;
						}
					}
					if(acceptCount < 0) acceptCount = 0;
					if(refuseCount < 0) refuseCount = 0;
					curRow.AcceptCount = acceptCount;
					curRow.RefuseCount = refuseCount;
					curRow.ScanCodeMark = MARK;

					//如果条码不存在当前扫码明细里						
					if(isExistCur == false) {
						CurBarCodeList.splice(0, 0, curRow);
						createHtmlOfScanCode();
					} else {
						CurBarCodeList[curRowIndex] = curRow;
						//已扫条码的数量输入框值处理
						//给input添加id(明细Id+"-"+操作类型+"-"+isAccept)),方便直接查找到
						var isAccept = (MARK == 2 ? true : false);
						var inputId = curRow.Id + "-" + "2" + "-" + isAccept;
						var input = $("#" + inputId);

						var nowValue = (MARK == 2 ? acceptCount : refuseCount);
						$(input).val(nowValue);
						if((acceptCount + refuseCount) >= tempDtlCount) {
							//给当前操作类型的相反(接收的拒收)数值录入框更新赋值
							var isAccept = (MARK == 2 ? false : true);
							var inputId = curRow.Id + "-" + "2" + "-" + isAccept;
							var input = $("#" + inputId);
							var nowValue = (MARK == 2 ? refuseCount : acceptCount);
							$(input).val(nowValue);
						}
					}
					setShowCountOfScancode();
					break;
			}
		}
		$('#scancode-text').val("");
		if(isExistCur == true && curBarCodeMgr == "1") msgInfo += "该条码已被扫描过！<br />"
		if(!isExistAll == true) msgInfo += "供货单不存在条码为:" + barcode + "的产品!";
		if(msgInfo) {
			$('#scancode-text').blur();
			showOperateConfirm(msgInfo);
		} else {
			$('#scancode-text').focus();
		}
	}
	/***
	 * 扫码--确认扫码按钮操作
	 * 处理当前已经扫过码之外的明细行的扫码标志值
	 */
	function confirmMarkLocalData() {
		if(CurBarCodeList.length >= 0 && AllList.length > 0) {
			//扫描条码隶属的批号产品map
			var DtlMap = {};
			//已经扫过条码集合
			for(var i in CurBarCodeList) {
				var mixSerial = CurBarCodeList[i].PSaleDtlIDStr;
				if(!mixSerial) continue;
				if(!DtlMap[mixSerial]) DtlMap[mixSerial] = [];
			}
			//获取所有与扫描条码一致的产品
			for(var mapMixSerial in DtlMap) {
				for(var j in AllList) {
					var mixSerial2 = AllList[j].PSaleDtlIDStr;
					if(!mixSerial2) continue;
					//相同的试剂明细
					if(mapMixSerial == mixSerial2) {
						var scanCodeMark = AllList[j].ScanCodeMark;
						var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(AllList[j]);
						if(isConfirm == false) {
							var curBarCodeMgr = "" + AllList[j].BarCodeMgr;
							if(curBarCodeMgr == "1") {
								if(scanCodeMark != 0) DtlMap[mapMixSerial].push(AllList[j]);
							} else {
								DtlMap[mapMixSerial].push(AllList[j]);
							}
						}
					}
				}
			}
			//当次扫描产品相关的所有明细数据
			var CurList = [];
			for(var mapMixSerial in DtlMap) {
				CurList = CurList.concat(DtlMap[mapMixSerial]);
			}
			//先设置某一相同产品的标志值(ScanCodeMark)设置为当前扫码操作(接收:2,拒收:1)的相反值
			for(var j in CurList) {
				var dtId = CurList[j]["Id"];
				var indexOf = -1;
				for(var k in AllList) {
					var dtId2 = AllList[k]["Id"];
					if(dtId == dtId2) {
						var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(AllList[k]);
						if(isConfirm == false) indexOf = k;
						break;
					}
				}
				if(indexOf != -1) {
					var curBarCodeMgr = "" + AllList[indexOf].BarCodeMgr;
					var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(AllList[indexOf]);
					if(isConfirm == false) {
						//当前扫码为接收,就设置为拒收;当前扫码为拒收,就设置为接收;
						AllList[indexOf].ScanCodeMark = (MARK == 1 ? 2 : 1);
						switch(curBarCodeMgr) {
							case "1": //盒条码
								//当前扫码为接收,AcceptCount为0,拒收AcceptCount为1
								AllList[indexOf].AcceptCount = MARK == 2 ? 0 : 1;
								AllList[indexOf].RefuseCount = MARK == 2 ? 1 : 0;
								break;
							default:

								break;
						}
					}
				}
			}
			//设置当前扫码明细的标志值为当前扫码操作(接收:2或拒收:1),及验收数量的处理
			for(var i in CurBarCodeList) {
				var mixSerial = Shell.confirm.common.getMixSerialByBarCodeMgr(CurBarCodeList[i]);
				for(var j in AllList) {
					var mixSerial2 = Shell.confirm.common.getMixSerialByBarCodeMgr(AllList[j]);
					if(mixSerial == mixSerial2) {
						var curBarCodeMgr = "" + AllList[j].BarCodeMgr;
						var isConfirm = Shell.infoCommon.isConfirmOfScanCodeMark(AllList[j]);
						if(isConfirm == false) {
							AllList[j].ScanCodeMark = MARK;
							switch(curBarCodeMgr) {
								case "1": //盒条码
									AllList[j].AcceptCount = MARK == 2 ? 1 : 0;
									AllList[j].RefuseCount = MARK == 1 ? 1 : 0;
									break;
								default:
									AllList[j].AcceptCount = CurBarCodeList[i].AcceptCount;
									AllList[j].RefuseCount = CurBarCodeList[i].RefuseCount;
									break;
							}
							break;
						}
					}
				}
			}
		}
		//显示类型为明细
		SHOWTYPE = 1;
		setTitle();
		//重置还原扫码的变量
		resetScanCodeParams();
		//隐藏扫码相关div
		Shell.infoCommon.setScanCodeHidden(true);
		//显示明细的相关DIV
		Shell.infoCommon.setDtHidden(false);
		//刷新明细内容
		loadDtContent();
	}
	//扫码--创建当前已扫码完成的所有HTML内容
	function createHtmlOfScanCode() {
		var html = [];
		html.push('<div>');
		if(CurBarCodeList.length == 0) {
			html.push('<div class="no-data-div">没有找到数据!</div>'); //没有数据
		} else {
			for(var i = 0; i < CurBarCodeList.length; i++) {
				var row = createRowOfDt(CurBarCodeList[i], 2);
				html.push(row);
			}
		}
		html.push('</div>');
		var dtlHtml = html.join('');
		$("#scancode-div-content").html(dtlHtml);
		initDtListener();
		SCANCODECOUNT = Shell.infoCommon.calcScancodeCount(CurBarCodeList, MARK);
		setShowCountOfScancode();
	}
	//设置当前扫码的总数量
	function setShowCountOfScancode() {
		if(SCANCODECOUNT < 0) SCANCODECOUNT = 0;
		var showinfo = "总数" + SCANCODECOUNT;
		$("#scancode-div-show-count").html(showinfo);
	}
	//设置显示的标题
	function setTitle() {
		var title = DEFAULT_TITLE;
		if(SHOWTYPE == 2) title = (MARK == 1 ? ERROR_TITLE : SUCCESS_TITLE);
		$("#div-show-title").html(title);
	}
	//扫码-重置还原变量的初始值
	function resetScanCodeParams() {
		if(MARK) MARK = parseInt(MARK);
		//已扫码总数量
		SCANCODECOUNT = 0;
		//已扫码的明细
		CurBarCodeList = [];
		var info = (MARK == 1 ? ERROR_MSG : SUCCESS_MSG);
		$("#scancode-div-show-info").html(info);
		$('#scancode-text').val("");
		$('#scancode-div-content').html("");
		$("#scancode-div-show-count").html("总数");
		$('#scancode-text').focus();
	}
	//返回的操作
	function topback() {
		switch(SHOWTYPE) {
			case 2: //由扫码返回明细显示操作
				Shell.infoCommon.setDtHidden(false);
				Shell.infoCommon.setScanCodeHidden(true);
				resetScanCodeParams();
				SHOWTYPE = 1;
				setTitle();
				break;
			default: //由明细返回供货单列表
				AllList = [];
				location.href = "list.html?account=" + ACCOUNT + "&password=" + PASSWORD + "&islogin=0" + "&secAccepterType=" + SECACCEPTERTYPE + "&type=2&t=" + new Date().getTime();
				break;
		}
	}
	//默认值设置
	function setCheckDefaultValue() {
		var username = Shell.util.Cookie.get(Shell.util.Cookie.map.USERNAME);
		if(!username) username = "";
		$("#mainCheckUseName").html(username);
		Shell.infoCommon.showCheckUserInfo(SECACCEPTERTYPE);
	}
	//保存前的验收数量(接收及拒收)处理
	function onverificationAccept() {
		if(!AllList || AllList.length == 0) {
			ShellComponent.messagebox.msg("获取供货明细信息为空!");
		} else {
			//先验收所有明细的验收数量
			var result = Shell.infoCommon.verificationSave(AllList);
			switch(result.resultType) {
				case 1:
					CHECKED_BUTTON = 3;
					$('#btn-operate-confirm').show();
					$('#btn-operate-cancel').show();
					showOperateConfirm(result.msg);
					break;
				case 2:
				case 3:
					CHECKED_BUTTON = 4;
					$('#btn-operate-confirm').hide();
					$('#btn-operate-cancel').hide();
					showOperateConfirm(result.msg);
					break;
				default:
					onShowConfirm();
					break;
			}
		}
	}
	//打开保存的备注录入提示页
	function onShowConfirm() {
		setCheckDefaultValue();
		$("#check-data-footer").show();
		$("#modal-check-confirm").modal({
			backdrop: 'static',
			keyboard: false
		});
	}
	//弹出操作确认提示框
	function showOperateConfirm(msg) {
		$("#operate-confirm-msg").html(msg);
		$("#operate-confirm-footer").show();
		$("#modal-operate-confirm").modal({
			backdrop: 'static',
			keyboard: false
		});
	}
	//确认提交验收
	function onConfirmSaveCheckClick() {
		var result = true;
		if(SECACCEPTERTYPE) SECACCEPTERTYPE = parseFloat(SECACCEPTERTYPE);
		if(SECACCEPTERTYPE != 0) result = Shell.infoCommon.checkVerification(ACCOUNT);
		if(result == true) {
			$("#modal-check-confirm").modal("hide");
			onSave();
		}
	}
	//提交保存
	function onSave() {
		var params = Shell.infoCommon.getSaveParamsData(SECACCEPTERTYPE, AllList);
		var data = JSON.stringify(params); // Shell.util.JSON.encode(params);
		//console.log("data:" + data);
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_AddBmsCenSaleDocConfirm';
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			type: 'post',
			url: url,
			data: data
		}, function(data) {
			ShellComponent.mask.hide();
			if(data.success) {
				AllList = []; //清空所有明细信息
				resetScanCodeParams();
				location.href = "list.html?account=" + ACCOUNT + "&password=" + PASSWORD + "&islogin=0" + "&secAccepterType=" + SECACCEPTERTYPE + "&type=2&t=" + new Date().getTime();
			} else {
				//ShellComponent.messagebox.msg(data.ErrorInfo);
				showOperateAlert(data.ErrorInfo);
			}
		});
	}
	//弹出操作提示框
	function showOperateAlert(msg) {
		$("#operate-alert-msg").html(msg);
		$("#operate-alert-footer").show();
		$("#modal-operate-alert").modal({
			backdrop: 'static',
			keyboard: false
		});
	}
	//判断客户端是移动设备还是PC,设置单击事件
	function browserEventClick() {
		var isPC = Shell.util.Event.isPC();
		Shell.util.Event.click = "touchend";
		if(isPC) Shell.util.Event.click = "click";
		initListeners();
	}
	browserEventClick();
	loadDtContent();
});