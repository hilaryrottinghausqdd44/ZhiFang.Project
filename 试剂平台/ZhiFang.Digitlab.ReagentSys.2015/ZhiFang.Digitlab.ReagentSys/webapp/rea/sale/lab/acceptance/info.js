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
	//所有明细的金额
	var ALLSUMTOTAL = 0;
	//明细验收总金额
	var CHECKSUMTOTAL = 0;
	//是否加载了总单的验收备注及发票信息:true:为加载,false为没加载,暂时设为true
	var ISLOADMEMO = true;
	//提交类型:暂存:1,确认提交:0
	var SUBMITTYPE = 0;
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
		//明细暂存提交操作
		$("#dt-btn-temp-save").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			SUBMITTYPE = 1;
			onverificationAccept();
		});
		//明细确认提交操作
		$("#dt-btn-save-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			SUBMITTYPE = 0;
			onverificationAccept();
		});
		//实验室暂存/确认提交处理
		$("#button-check-confirm").on("click", function() {
			switch(SUBMITTYPE) {
				case 1: //暂存
					onTempSaveClick();
					break;
				default:
					onConfirmSaveCheckClick();
					break;
			}
		});
		//明细弹出确定按钮处理
		$("#operate-confirm-button").on("click", function() {
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
					seSubmitBtnsHidden(true);
					break;
				case 2: //明细整单验收
					allDataCheck(3);
					seSubmitBtnsHidden(false);
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
		//数量加减监听
		$(".number-spinner button").on(Shell.util.Event.click, function(e) {
			e.preventDefault();
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
		//监听验收数量直接录入值变化后处理
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
	//监听验收数量输入框累加或减少后联动更新
	function onDtSpinnerButtonClick(input, data) {
		//原来的数量值
		var old = parseFloat(input.val() || "0");
		var now = old;
		//验收数量
		var acceptCount = 0;
		//数量操作类型
		switch(parseInt(data.type)) {
			case 1: //明细列表里的验收数量减少1
				now = now - 1;
				//数量只能是>=0
				if(now < 0) now = 0;
				acceptCount = now;
				break;
			case 3: //明细列表里的验收数量累加1
				now = now + 1;
				//数量只能是<=原始数量
				if(now > data.goodsQty) now = data.goodsQty;
				acceptCount = now;
				break;
			default:
				break;
		}
		input.val(now);
		input.text(now);
		//明细的验收数量更新
		setAcceptCount(data.dtId, acceptCount);
		//验收总金额更新
		setShowSumTotal();
		seSubmitBtnsHidden();
	}
	//明细--数量输入框直接录入后联动更新
	function onDtInputChange(input, data) {
		//当前数量值
		var now = parseFloat(input.val() || "0");
		//验收数量
		var acceptCount = 0;
		//操作类型
		switch(parseFloat(data.type)) {
			case 2: //明细列表里的验收数量直接录入
				//数量只能是>=0
				if(now < 0) now = 0;
				//数量只能是<=原始数量
				if(now > data.goodsQty) now = data.goodsQty;
				acceptCount = now;
				break;
			default:
				break;
		}
		input.val(now);
		input.text(now);
		//明细的验收数量更新
		setAcceptCount(data.dtId, acceptCount);
		//验收总金额更新
		setShowSumTotal();
		seSubmitBtnsHidden();
	}
	//明细--供货单明细列表的验收数量更新
	function setAcceptCount(dtId, acceptCount) {
		for(var i = 0; i < AllList.length; i++) {
			if(AllList[i]["BmsCenSaleDtl_Id"] == dtId) {
				AllList[i]["BmsCenSaleDtl_AcceptCount"] = acceptCount;
				break;
			}
		}
	}
	//明细--供货单整单验收
	function allDataCheck(mark) {
		SHOWTYPE = 1;
		refreshDataAndContent(mark);
		seSubmitBtnsHidden(false);
	}
	//明细--打开供货单明细扫码
	function showScanCode() {
		setTitle();
		resetScanCodeParams();
		setDtHidden(true);
		setScanCodeHidden(false);
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
		if(list.length == 0) {
			html.push('<div class="no-data-div">没有找到数据!</div>'); //没有数据
		}
		html.push('</div>');
		return html.join('');
	}
	/***
	 * 创建数据行内容
	 * @param {Object} model 为合并行后的信息
	 * @param {Object} otype 明细列表显示:1;扫码明细列表显示:2
	 */
	function createRowOfDt(model, otype) {
		var html = [];
		var dtId = model.BmsCenSaleDtl_Id;
		//条码类型
		var barCodeMgr = "" + model.BmsCenSaleDtl_BarCodeMgr;
		var dtlCount = model.BmsCenSaleDtl_DtlCount;
		if(dtlCount) dtlCount = parseFloat(dtlCount);
		var goodsQty = model.BmsCenSaleDtl_GoodsQty;
		if(goodsQty) goodsQty = parseFloat(goodsQty);
		var unit = model.BmsCenSaleDtl_GoodsUnit;
		var price = parseFloat(model.BmsCenSaleDtl_Price);
		model.BmsCenSaleDtl_GoodsQty = goodsQty;

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
		var Price = '单价:' + price;

		var margintop = "-73px";
		var titlePre = Shell.check.common.getTitlePre(model);
		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<a class="list-group-item" data="' + dtId + '" style="margin:2px;">');
		html.push('<h4 class="list-group-item-heading">' + titlePre + title + '</h4>');
		//扫码明细显示
		if(otype == 2) {
			var mixSerial = '条码:' + Shell.check.common.getBarCodeOfMixSerial(model, false);
			html.push('<div class="list-group-item-text" style="' + pStyle + '">' + mixSerial + '</div>');
		}
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + LotNo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UnitMemo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + Price + '</div>');

		//原始数量
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + InvalidDate + '</div>');
		html.push('<div style="margin-top:' + margintop + ';width:111px;float:right;">');

		//兼容因为在早期时的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
		if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;

		switch(barCodeMgr) {
			case "1": //盒条码
				html.push('<div class="btn btn-sm btn-success" style="width:112px;padding:2px;margin:2px;">供货数:' + dtlCount + unit + '</div>');
				//验收数量
				if(otype == 1) html = Shell.acceptance.getcheckhtml(model, html, AllList, otype);
				break;
			default:
				//批条码(非盒条码):0、无条码:2
				html.push('<div class="btn btn-sm btn-success" style="width:112px;padding:2px;margin-left:4px;">供货数:' + dtlCount + unit + '</div>');
				html = getAcceptCountRowHtml(html, model, otype);
				break;
		}
		html.push('</div>');
		//总额
		var sumTotal = model.BmsCenSaleDtl_SumTotal;
		if(sumTotal && sumTotal.toString().indexOf('.') > 0) sumTotal = parseFloat(sumTotal).toFixed(2);
		html.push('<div style="padding:2px;margin:2px;margin-top:-15px' + ';width:180px;float:right;text-align:right;">');
		html.push('<div>总额:<span style="color:#d9534f;font-weight:bold;">' + sumTotal + '</span>元</div>');

		html.push('</div>');
		html.push('</a>');
		return html.join('');
	}
	/***
	 * 生成验收数量Html行
	 * @param {Object} html
	 * @param {Object} model
	 * @param {Object} otype 明细列表显示:1;扫码明细列表显示:2
	 */
	function getAcceptCountRowHtml(html, model, otype) {
		var dtId = model.BmsCenSaleDtl_Id;
		var dtlCount = model.BmsCenSaleDtl_DtlCount;
		if(dtlCount) dtlCount = parseFloat(dtlCount);

		var acceptCount = model.BmsCenSaleDtl_AcceptCount;
		if(acceptCount) acceptCount = parseFloat(acceptCount);
		//原始数量
		var goodsQty = model.BmsCenSaleDtl_GoodsQty;
		if(goodsQty) goodsQty = parseFloat(goodsQty);
		//兼容因为在早期时的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
		if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;

		html.push('<div class="number-spinner">');
		//数量的data封装属性值
		var data = {
			otype: otype, //明细列表显示:1;扫码明细列表显示:2
			type: 1, //数量操作类型:验收数量减少1:1;验收数量累加1:2;拒收数量减少1:3;拒收数量累加1:4;
			dtId: dtId, //明细Id
			goodsQty: dtlCount //明细的原始总数量
		};
		//如果是拒收扫码,输入框显示拒收数量
		if(otype == "2" && MARK == 1) acceptCount = dtlCount - acceptCount;
		if(!acceptCount) acceptCount = 0;
		//数量减少1的button				
		var data1 = Shell.util.JSON.encode(data);
		data1 = data1.replace(/"/g, "'");
		html.push('<button class="number-spinner-decrease" data="' + data1 + '">-</button>');
		//onkeyup="this.value=this.value.replace(/\\D/gi,\'\')"
		html.push('<input type="number" min="0" value="' + acceptCount + '"');
		//数量直接录入
		data.type = 2;
		var data2 = Shell.util.JSON.encode(data);
		data2 = data2.replace(/"/g, "'");
		html.push(' data="' + data2 + '"');

		//给input添加id(明细Id+"-"+操作类型),方便直接查找到
		var inputId1 = dtId + "-" + data.otype;
		html.push(' id="' + inputId1 + '">');

		//数量累加1的button
		data.type = 3;
		var data3 = Shell.util.JSON.encode(data);
		data3 = data3.replace(/"/g, "'");
		html.push('<button class="number-spinner-increase" data="' + data3 + '">+</button>');
		html.push('</div>'); //end number-spinner
		return html;
	}
	/***
	 * 明细--还原供货单明细数据(明细手工添加扫码标志)
	 * @param {Object} mark 标志:0为没有扫码确认;3为整单接收;4为整单重置;
	 */
	function refreshDataAndContent(mark) {
		if(!AllList) AllList = [];
		seSubmitBtnsHidden(true);
		AllList = Shell.acceptance.setMarkAndAcceptCountDtData(AllList, mark);
		var data = Shell.acceptance.mergerData(AllList);
		var dtlHtml = createHtmlOfDt(data);
		$("#dt-div-content").html("");
		$("#dt-div-content").html(dtlHtml);
		initDtListener();
		setShowSumTotal();
		seSubmitBtnsHidden();
	}
	//设置显示金额 #d9534f  #d9534f
	function setShowSumTotal() {
		var html = "";
		CHECKSUMTOTAL = Shell.acceptance.calcCheckSumTotal(AllList);
		ALLSUMTOTAL = Shell.acceptance.calcAllSumTotal(AllList);
		html += '<span>供货:<b style="color:#d9534f;">' + ALLSUMTOTAL + '</b>元</span>&nbsp;&nbsp;';
		html += '<span>验收:<b style="color:#d9534f;">' + CHECKSUMTOTAL + '</b>元</span>&nbsp;&nbsp;';
		if(CHECKSUMTOTAL != ALLSUMTOTAL) {
			html += '<span style="padding:2px;background-color:#d9534f;color:#ffffff;font-size:10px;">缺</span>';
		}
		$("#dt-show-total").html(html);
	}
	//明细--加载明细列表数据
	function loadDtContent() {
		seSubmitBtnsHidden(true);
		if(AllList && AllList.length > 0) {
			var data = Shell.acceptance.mergerData(AllList);
			var dtlHtml = createHtmlOfDt(data);
			$("#dt-div-content").html(dtlHtml);
			initDtListener();
			setShowSumTotal();
			seSubmitBtnsHidden();
		} else {
			AllList = [];
			//加载供货单明细列表数据
			ShellComponent.mask.loading();
			Shell.acceptance.loadDtlData(PK, function(value) {
				ShellComponent.mask.hide();
				if(value.success) {
					AllList = value.value.list;
					AllList = Shell.acceptance.setMarkAndAcceptCountDtData(AllList, 0);
					var data = Shell.acceptance.mergerData(AllList);
					var dtlHtml = createHtmlOfDt(data);
					$("#dt-div-content").html(dtlHtml);
					initDtListener();
					setShowSumTotal();
					seSubmitBtnsHidden();
				} else {
					var msg = value.msg;
					if(!msg) msg = "获取供货单明细出错了!";
					$("#dt-div-content").html('<div class="error-div">' + msg + '</div>');
					seSubmitBtnsHidden(true);
				}
			});
		}
	}
	/***
	 * 扫码--监听数量值累加或减少1按钮的处理
	 * @param {Object} input
	 * @param {Object} data
	 */
	function onScancodeSpinnerButtonClick(input, data) {
		var inputValue = parseFloat(input.val() || "0");
		switch(parseInt(data.type)) {
			case 1:
				//明细列表里的数量减少1
				inputValue -= 1;
				if(inputValue < 0) inputValue = 0;
				break;
			case 3:
				//明细列表里的数量累加1
				inputValue += 1;
				break;
			default:
				break;
		}
		setAcceptCountValue(input, data, inputValue);
	}
	/***
	 * 扫码--监听数量值直接录入改变后处理
	 * @param {Object} input
	 * @param {Object} data
	 */
	function onScancodeInputChange(input, data) {
		var inputValue = parseFloat(input.val() || "0");
		setAcceptCountValue(input, data, inputValue);
	}

	function setAcceptCountValue(input, data, inputValue) {
		var dtId = data.dtId;
		var acceptCount = 0;
		for(var i = 0; i < CurBarCodeList.length; i++) {
			var dtId2 = CurBarCodeList[i]["BmsCenSaleDtl_Id"];
			if(dtId == dtId2) {
				var curRow = CurBarCodeList[i];
				var goodsQty = curRow.BmsCenSaleDtl_GoodsQty;
				if(goodsQty) goodsQty = parseFloat(goodsQty);
				//验收数量只能是小于等于原始数量并大于等于0
				if(inputValue > goodsQty) inputValue = goodsQty;
				if(inputValue < 0) inputValue = 0;

				if(MARK == 2) { //接收扫码
					acceptCount = inputValue;
					$(input).val(acceptCount);
				} else { //拒收扫码
					acceptCount = goodsQty - inputValue;
					$(input).val(inputValue);
				}
				CurBarCodeList[i]["BmsCenSaleDtl_AcceptCount"] = acceptCount;
				break;
			}
		}
		calcScancodeCount();
		setShowCountOfScancode();
	}
	//重新计算当次已扫条码总数量
	function calcScancodeCount() {
		SCANCODECOUNT = 0;
		for(var i = 0; i < CurBarCodeList.length; i++) {
			var model = CurBarCodeList[i];
			
			var curBarCodeMgr = "" + model.BmsCenSaleDtl_BarCodeMgr;
			if(curBarCodeMgr == "1") {
				SCANCODECOUNT += 1;
			} else {
				var acceptCount = model.BmsCenSaleDtl_AcceptCount;
				if(acceptCount) acceptCount = parseFloat(acceptCount);
				var refuseCount = parseFloat(model.BmsCenSaleDtl_GoodsQty)-acceptCount;
				if(!refuseCount) refuseCount = 0;
				//区分当前是接收还是拒收扫码
				SCANCODECOUNT += (MARK == 2 ? acceptCount : refuseCount);
			}
		}
	}
	//重新计算当次已扫条码总数量(旧的,不用)
	function calcScancodeCount2() {
		SCANCODECOUNT = 0;
		for(var i = 0; i < CurBarCodeList.length; i++) {
			var model = CurBarCodeList[i];
			var acceptCount = model.BmsCenSaleDtl_AcceptCount;
			if(acceptCount) acceptCount = parseFloat(acceptCount);
			var curBarCodeMgr = model.BmsCenSaleDtl_BarCodeMgr;
			if(curBarCodeMgr == "1") {
				SCANCODECOUNT += 1;
			} else {
				var acceptCount = model.BmsCenSaleDtl_AcceptCount;
				if(acceptCount) acceptCount = parseFloat(acceptCount);
				SCANCODECOUNT += acceptCount;
			}
		}
	}
	/***
	 * 扫码--依扫码找出明细行并更新扫码标志
	 * @param {Object} barcode
	 */
	function onScanCodeAccept(barcode) {
		if(AllList.length == 0) return;
		//当前条码是否存在所有明细的集合里的
		var isExistAll = false;
		//当前条码是否存在当前已经扫过的明细的集合里
		var isExistCur = false;
		//当前条码在明细里的行信息
		var curRow = null;
		//当前条码的条码类型
		var curBarCodeMgr = "";
		//当前条码在已扫条码明细里的索引
		var curRowIndex = 0;
		var msgInfo = "";
		//当前扫码是否存在里供货单明细中
		for(var i = 0; i < AllList.length; i++) {
			//var barcode2 = Shell.check.common.getMixSerialByBarCodeMgr(AllList[i]);
			var barcode2 = AllList[i][Shell.check.common.SCANCODEFILE];
			if(barcode == barcode2) {
				curRow = $.extend(true, {}, AllList[i]);
				curBarCodeMgr = AllList[i].BmsCenSaleDtl_BarCodeMgr;
				isExistAll = true;
				break;
			}
		}
		//当前已扫码明细,判断是否存在当前扫码明细里
		if(isExistAll == true) {
			for(var i = 0; i < CurBarCodeList.length; i++) {
				//var barcode2 = Shell.check.common.getMixSerialByBarCodeMgr(CurBarCodeList[i]);
				var barcode2 = CurBarCodeList[i][Shell.check.common.SCANCODEFILE];
				if(barcode == barcode2) {
					//不是盒条码的,以当次已扫的条码明细行为当前条码行信息,方便累加验收数量
					if(curBarCodeMgr != "1") curRow = CurBarCodeList[i];
					isExistCur = true;
					curRowIndex = i;
					break;
				}
			}
			var goodsQty = curRow.BmsCenSaleDtl_GoodsQty;
			if(goodsQty) goodsQty = parseFloat(goodsQty);
			var dtlCount = curRow.BmsCenSaleDtl_DtlCount;
			if(dtlCount) dtlCount = parseFloat(dtlCount);
			//兼容因为在早期时的数据对DtlCount是没有赋值的(取接口的数据时也是为0),还是取GoodsQty
			if((!dtlCount || dtlCount == 0) && goodsQty) dtlCount = goodsQty;

			var acceptCount = curRow.BmsCenSaleDtl_AcceptCount;
			if(acceptCount) acceptCount = parseFloat(acceptCount);
			if(!acceptCount) acceptCount = 0;

			switch(curBarCodeMgr) {
				case "1": //盒条码	
					if(isExistCur == false) {
						curRow.ScanCodeMark = MARK;
						//如果当前扫码为接收,当前扫码明细验收数量为1:2017-12-22
						//curRow.BmsCenSaleDtl_AcceptCount = (MARK == 2 ? 1 : 0);
						//如果当前扫码为接收,当前扫码明细验收数量为goodsQty:2017-12-22(以支持goodsQty为小数点)
						curRow.BmsCenSaleDtl_AcceptCount = (MARK == 2 ? goodsQty: 0);
						
						CurBarCodeList.splice(0, 0, curRow);
						createHtmlOfScanCode();
					}
					break;
				default: //批条码
					//给input添加id(明细Id+"-"+操作类型),方便直接查找到
					var inputId = curRow.BmsCenSaleDtl_Id + "-2";
					var input = $("#" + inputId);
					if(MARK == 2) { //接收扫码						
						//条码当前行的验收数量值处理
						acceptCount += 1;
						//验收数量只能是小于等于原始数量并大于等于0
						if(acceptCount > dtlCount) {
							acceptCount = dtlCount;
						} else {
							SCANCODECOUNT += 1;
						}
						//已扫条码的验收数量输入框值累加1
						if(input && isExistCur == true) $(input).val(acceptCount);
					} else { //拒收扫码
						if(isExistCur == false) acceptCount = goodsQty;
						acceptCount = acceptCount - 1;
						if(acceptCount > 0) SCANCODECOUNT += 1;
						if(acceptCount < 0) acceptCount = 0;
						var unAcceptCount = dtlCount - acceptCount;
						//已扫条码的拒收数量输入框值
						if(input && isExistCur == true) $(input).val(unAcceptCount);
					}
					curRow.BmsCenSaleDtl_GoodsQty = goodsQty;
					curRow.BmsCenSaleDtl_AcceptCount = acceptCount;
					//如果条码不存在当前扫码明细里						
					if(isExistCur == false) {
						CurBarCodeList.splice(0, 0, curRow);
						createHtmlOfScanCode();
					} else {
						CurBarCodeList[curRowIndex] = curRow;
					}
					setShowCountOfScancode();
					break;
			}
		}
		$('#scancode-text').val("");
		if(isExistCur == true && curBarCodeMgr == "1") {
			msgInfo += "该条码已被扫描过！<br />"
		}
		if(!isExistAll == true) {
			msgInfo += "该供货单中不存在条码为:" + barcode + "的产品!";
		}
		if(msgInfo) {
			$('#scancode-text').blur();
			showOperateConfirm(msgInfo);
		} else {
			$('#scancode-text').focus();
		}
	}
	/***
	 * 扫码--确认按钮操作
	 * 处理当前已经扫过码之外的明细行的扫码标志值
	 * 如当前为拒收扫码,某一产品有6箱,只确认扫了一箱错误条码,需要处理其他5箱为接收扫码
	 */
	function confirmMarkLocalData() {
		if(CurBarCodeList.length >= 0 && AllList.length > 0) {
			//扫描条码隶属的批号产品map
			var DtlMap = {};
			//已经扫过条码集合
			for(var i in CurBarCodeList) {
				var mixSerial = Shell.check.common.getSplitMixSerial(CurBarCodeList[i], true);
				if(!mixSerial) continue;
				if(!DtlMap[mixSerial]) DtlMap[mixSerial] = [];
			}
			//获取所有与扫描条码一致的产品
			for(var mapMixSerial in DtlMap) {
				for(var j in AllList) {
					var mixSerial2 = Shell.check.common.getSplitMixSerial(AllList[j], true);
					if(!mixSerial2) continue;
					//混合条码的第8位或前三段相同
					if(mapMixSerial == mixSerial2) DtlMap[mapMixSerial].push(AllList[j]);
				}
			}
			//当次扫描产品相关的所有明细数据
			var CurList = [];
			for(var mapMixSerial in DtlMap) {
				CurList = CurList.concat(DtlMap[mapMixSerial]);
			}
			//先设置某一相同产品的标志值(ScanCodeMark)设置为当前扫码操作(接收:2,拒收:1)的相反值
			for(var j in CurList) {
				var dtId = CurList[j]["BmsCenSaleDtl_Id"];
				var indexOf = -1;
				for(var k in AllList) {
					var dtId2 = AllList[k]["BmsCenSaleDtl_Id"];
					if(dtId == dtId2) {
						indexOf = k;
						break;
					}
				}
				if(indexOf != -1) {
					var curBarCodeMgr = "" + AllList[indexOf].BmsCenSaleDtl_BarCodeMgr;
					switch(curBarCodeMgr) {
						case "1": //盒条码
							//当前扫码为接收,就设置为拒收;当前扫码为拒收,就设置为接收;
							AllList[indexOf].ScanCodeMark = (MARK == 1 ? 2 : 1);
							//当前扫码为接收,AcceptCount为0,拒收AcceptCount为1
							AllList[indexOf].BmsCenSaleDtl_AcceptCount = MARK == 2 ? 0 : 1;
							break;
						default:
							break;
					}
				}
			}
			//设置当前扫码明细的标志值为当前扫码操作(接收:2或拒收:1),及验收数量的处理
			for(var i in CurBarCodeList) {
				var mixSerial = Shell.check.common.getMixSerialByBarCodeMgr(CurBarCodeList[i]);
				for(var j in AllList) {
					var mixSerial2 = Shell.check.common.getMixSerialByBarCodeMgr(AllList[j]);
					if(mixSerial == mixSerial2) {
						var curBarCodeMgr = "" + AllList[j].BmsCenSaleDtl_BarCodeMgr;
						switch(curBarCodeMgr) {
							case "1": //盒条码
								AllList[j].ScanCodeMark = MARK;
								//接收时AcceptCount为1,拒收AcceptCount为0
								AllList[j].BmsCenSaleDtl_AcceptCount = MARK == 2 ? 1 : 0;
								break;
							default:
								//如果是拒收扫码,也只以换算成接收数量处理
								var acceptCount = CurBarCodeList[i].BmsCenSaleDtl_AcceptCount;
								if(acceptCount) acceptCount = parseFloat(acceptCount);
								AllList[j].BmsCenSaleDtl_AcceptCount = acceptCount;
								break;
						}
						break;
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
		setScanCodeHidden(true);
		//显示明细的相关DIV
		setDtHidden(false);
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
		//SCANCODECOUNT显示值处理
		calcScancodeCount();
		setShowCountOfScancode();
	}

	function setShowCountOfScancode() {
		if(SCANCODECOUNT < 0) SCANCODECOUNT = 0;
		var showinfo = "总数" + SCANCODECOUNT;
		$("#scancode-div-show-count").html(showinfo);
	}
	//联动时明细操作相关DIV显示或隐藏
	function setDtHidden(hidden) {
		switch(hidden) {
			case true:
				$("#dt-show-total").hide();
				$("#dt-div-buttons").hide();
				$("#dt-div-content").hide();
				//$("#dt-div-submit").hide();
				break;
			default:
				$("#dt-show-total").show();
				$("#dt-div-buttons").show();
				$("#dt-div-content").show();
				//$("#dt-div-submit").show();
				break;
		}
	}
	//联动时扫码操作相关DIV显示或隐藏
	function setScanCodeHidden(hidden) {
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
	}
	/***
	 * 提交按钮的显示或者隐藏
	 * 当全部明细的验收数量都为0时,隐藏,当有一种试剂的验收数量大于0时,显示
	 * @param {Object} hidden
	 */
	function seSubmitBtnsHidden(hidden) {
		if(!hidden) {
			hidden = true;
			for(var i = 0; i < AllList.length; i++) {
				if(AllList[i].BmsCenSaleDtl_AcceptCount) {
					var acceptCount = parseFloat(AllList[i].BmsCenSaleDtl_AcceptCount);
					if(acceptCount > 0) {
						hidden = false;
						break;
					}
				}
			}
		}
		switch(hidden) {
			case true:
				$("#dt-btn-temp-save").hide();
				$("#dt-btn-save-check").hide();
				$("#dt-div-submit").hide();
				break;
			default:
				$("#dt-btn-temp-save").show();
				$("#dt-btn-save-check").show();
				$("#dt-div-submit").show();
				break;
		}
	}
	//设置显示的标题
	function setTitle() {
		var title = DEFAULT_TITLE;
		switch(SHOWTYPE) {
			case 2:
				title = (MARK == 1 ? ERROR_TITLE : SUCCESS_TITLE);
				break;
			default:
				break;
		}
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
				setDtHidden(false);
				setScanCodeHidden(true);
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
	//加载供货单备注信息
	function loadMemoInfo(callback) {
		if(ISLOADMEMO == false) {
			var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocById?isPlanish=true';
			var fields = 'BmsCenSaleDoc_Id,BmsCenSaleDoc_AccepterMemo,BmsCenSaleDoc_InvoiceNo';
			var where = "&id=" + PK
			url += '&fields=' + fields + "&where=" + where;
			url += ("&t=" + new Date().getTime());
			//ShellComponent.mask.loading();
			Shell.util.Server.ajax({
				async: true,
				url: url
			}, function(data) {
				if(data.success) {
					ISLOADMEMO = true;
					if(data.value) callback(data.value);
				}
			});
		}
	}
	//默认值设置
	function setCheckDefaultValue() {
		var username = Shell.util.Cookie.get(Shell.util.Cookie.map.USERNAME);
		if(!username) username = "";
		$("#mainCheckUseName").html(username);
		if(ISLOADMEMO == false) {
			loadMemoInfo(function(data) {
				var invoiceNo = data["BmsCenSaleDoc_InvoiceNo"];
				var accepterMemo = data["BmsCenSaleDoc_AccepterMemo"];
				if(!invoiceNo) invoiceNo = "";
				if(!accepterMemo) accepterMemo = "";

				$("#InvoiceNo").val(invoiceNo);
				$("#AccepterMemo").val(accepterMemo);
				showCheckUserInfo();
			});
		} else {
			showCheckUserInfo();
		}
	}
	//显示验收需要双确认的录入信息
	function showCheckUserInfo() {
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
	}
	//验收确认验证
	function checkVerification() {
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
	}
	//保存前的验收数量处理
	function onverificationAccept() {
		if(!AllList || AllList.length == 0) {
			ShellComponent.messagebox.msg("获取供货明细信息为空!");
		} else {
			//先验收所有明细的验收数量
			var resultType = verificationAcceptCount();
			switch(resultType) {
				case 1:
					CHECKED_BUTTON = 3;
					$('#operate-confirm-button').show();
					$('#operate-cancel-button').show();
					showOperateConfirm("当前操作存在验收数量为0的试剂，请确认是否继续执行操作，点【确定】按钮可以继续验收！");
					break;
				case 2:
					CHECKED_BUTTON = 4;
					$('#operate-confirm-button').hide();
					$('#operate-cancel-button').hide();
					showOperateConfirm("当前操作的所有试剂明细的验收数量为0，不能验收确认，请操作后再进行验收！");
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
	/****
	 * 先验收所有明细的验收数量
	 * 1.如果不是全部明细的验收数量为0,有一种试剂明细为0，弹出提示,可以关闭提示或者点确认后继续验收
	 * 2.全部是0的，弹出提示,可以关掉提示,不能操作验收确认。
	 * resultType:验收数量的验证结果类型(0:继续操作;1:弹出提示,可以关闭提示或者点确认后继续验收;2:弹出提示,可以关掉提示,不能操作验收确认)
	 */
	function verificationAcceptCount() {
		var resultType = 0,
			allAcceptCount = 0;
		var map = {};
		//先合并明细
		for(var i = 0; i < AllList.length; i++) {
			var model = $.extend(true, {}, AllList[i]);
			//条码类型
			var barCodeMgr = "" + model.BmsCenSaleDtl_BarCodeMgr;
			var acceptCount = model.BmsCenSaleDtl_AcceptCount;
			if(acceptCount) acceptCount = parseFloat(acceptCount);
			if(!acceptCount) acceptCount = 0;
			//所有的明细验收数量累加
			allAcceptCount += acceptCount;

			//混合条码BmsCenSaleDtl_MixSerial
			var mixSerial = Shell.check.common.getSplitMixSerial(model, true);
			if(!mixSerial) continue;
			if(mixSerial && !map[mixSerial]) {
				map[mixSerial] = model;
			} else {
				if(barCodeMgr == "1") {
					map[mixSerial].BmsCenSaleDtl_AcceptCount = acceptCount +
						parseFloat(map[mixSerial].BmsCenSaleDtl_AcceptCount)
				}
			}
		}
		if(allAcceptCount > 0) {
			for(var m in map) {
				var acceptCount = map[m].BmsCenSaleDtl_AcceptCount;
				if(acceptCount == 0) resultType = 1;
			}
		} else {
			resultType = 2;
		}
		return resultType;
	}
	//暂存提交
	function onTempSaveClick() {
		if(SECACCEPTERTYPE) SECACCEPTERTYPE = parseFloat(SECACCEPTERTYPE);
		if(SECACCEPTERTYPE != 0) result = checkVerification();
		$("#modal-check-confirm").modal("hide");
		onTempSave();
	}
	//确认提交验收
	function onConfirmSaveCheckClick() {
		var result = true;
		if(SECACCEPTERTYPE) SECACCEPTERTYPE = parseFloat(SECACCEPTERTYPE);
		if(SECACCEPTERTYPE != 0) result = checkVerification();
		if(result == true) {
			$("#modal-check-confirm").modal("hide");
			onConfirmSaveCheck();
		}
	}
	//供货单暂存处理
	function onTempSave() {
		onSave();
	}
	//供货单确认验收处理
	function onConfirmSaveCheck() {
		onSave();
	}
	//提交保存
	function onSave() {
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_ConfirmSaleDocByID';
		var params = getSaveParamsData();
		var data = Shell.util.JSON.encode(params);
		//console.log("data:"+data);
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
				ShellComponent.messagebox.msg(data.msg);
			}
		});
	}
	//封装提交的数据信息
	function getSaveParamsData() {
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
			isSplit: 1, //只验收不拆分
			docID: PK,
			invoiceNo: invoiceNoValue,
			accepterMemo: accepterMemoValue,
			secAccepterAccount: accountValue,
			secAccepterPwd: pwdValue,
			isTemp: SUBMITTYPE //保存类型:暂存或确认验收
		};
		//验收时不需要双确认时,secAccepterType不用传入
		if(SECACCEPTERTYPE && SECACCEPTERTYPE != 0) params.secAccepterType = SECACCEPTERTYPE;
		//盒条码类型的拒收扫码明细
		var listSaleDtlError = [];
		//批条码及无条码类型的明细
		var listSaleDtlNoBarCode = [];
		for(var i = 0; i < AllList.length; i++) {
			//条码类型
			var barCodeMgr = "" + AllList[i].BmsCenSaleDtl_BarCodeMgr;
			var acceptCount = AllList[i].BmsCenSaleDtl_AcceptCount;
			if(acceptCount) acceptCount = parseFloat(acceptCount);
			if(!acceptCount) acceptCount = 0;

			switch(barCodeMgr) {
				case "1": //盒条码
					var mark = AllList[i].ScanCodeMark;
					if(mark) mark = parseInt(mark);
					//不等于验收的值(可能包含mark=0,待验收)
					if(mark != 2) {
						//拒收扫码,有异常的单子 会把条码信息清空
						listSaleDtlError.push({
							Id: AllList[i].BmsCenSaleDtl_Id
						});
					}
					break;
				default:
					listSaleDtlNoBarCode.push({
						Id: AllList[i].BmsCenSaleDtl_Id,
						AcceptCount: acceptCount
					});
					break;
			}
		}
		params.listSaleDtlError = [];
		if(listSaleDtlError.length > 0) params.listSaleDtlError = listSaleDtlError;

		params.listSaleDtlNoBarCode = [];
		if(listSaleDtlNoBarCode.length > 0) params.listSaleDtlNoBarCode = listSaleDtlNoBarCode;
		return params;
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