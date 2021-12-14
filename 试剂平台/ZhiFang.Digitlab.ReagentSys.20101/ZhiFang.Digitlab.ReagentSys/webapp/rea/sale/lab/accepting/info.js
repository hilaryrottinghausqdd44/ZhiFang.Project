$(function() {
	//外部参数
	var params = Shell.util.getRequestParams(true);
	var PK = params.ID || "";
	//账号
	var ACCOUNT = params.ACCOUNT || "";
	//密码
	var PASSWORD = params.PASSWORD || "";
	//合并行的条件字段
	var SCANCODEFILE = "BmsCenSaleDtl_MixSerial";
	//选中的按钮标志,1=重置,2=整单验收,3=验收确认
	var CHECKED_BUTTON = null;
	//所有明细数据
	var AllList = [];

	var DEFAULT_TITLE = "待验收信息";
	//接收扫码的标题
	var SUCCESS_TITLE = "接收扫码";
	//拒收扫码的标题
	var ERROR_TITLE = "拒收扫码";
	//接收扫码的提示内容
	var SUCCESS_MSG = "凡接收扫码的产品都是通过验收的";
	//拒收扫码的提示内容
	var ERROR_MSG = "凡拒收扫码的产品都是不验收的";
	//扫码总数
	var SCANCODECOUNT = 0;
	//已经扫码的HTML行
	var HTMLROWS = [];
	//当前已经扫码的条码集合
	var CurBarCodeList = [];
	//扫码用的扫码标志值:1:拒收扫码,2:接收扫码;
	var MARK = 2;
	//显示类型:1是明细类型操作;2是扫码操作
	var SHOWTYPE = 1;
	//所有明细的金额
	var ALLSUMTOTAL = 0;
	//明细验收总金额
	var CHECKSUMTOTAL = 0;

	/* 获取混合条码分割后的值,混合条码格式有
	 * (1)平台的盒装混全条码格式:ZFRP|1|1116|078-K138-01|T11131|2017-05-30|110001118|5459663006151236684|1|111
	 * (2)平台的非盒装混合条码格式:5459663006151236684,(验收待定)
	 * (3)第三方平台的混合条码格式:11131|1747070530|20170530|111,分割后取前三段
	 * */
	function getSplitMixSerial(mixSerial) {
		if(!mixSerial) return "";
		var tempArr = mixSerial.split("|");
		if(!tempArr) return "";
		if(tempArr.length == 4) {
			mixSerial = tempArr[0] + tempArr[1] + tempArr[2];
		} else {
			mixSerial = tempArr[7];
		}
		return mixSerial;
	}
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
			CHECKED_BUTTON = 1;
			SHOWTYPE = 1;
			$("#createDoctorOrder-msg").html("确定重置数据吗？");
			$("#createDoctorOrder-footer").show();
			$("#modal-operate-confirm").modal({
				backdrop: 'static',
				keyboard: false
			});
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
			showScanCode();
		});
		//明细整单验收
		$("#btn-alldata-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			CHECKED_BUTTON = 2;
			SHOWTYPE = 1;
			$("#createDoctorOrder-msg").html("确定整单验收吗？");
			$("#createDoctorOrder-footer").show();
			$("#modal-operate-confirm").modal({
				backdrop: 'static',
				keyboard: false
			});
		});
		//明细确认提交操作
		$("#dt-div-save-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			CHECKED_BUTTON = 3;
			SHOWTYPE = 1;
			$("#createDoctorOrder-msg").html("确定提交验收吗？");
			$("#createDoctorOrder-footer").show();
			$("#modal-operate-confirm").modal({
				backdrop: 'static',
				keyboard: false
			});
		});
		//明细弹出确定按钮处理
		$("#createDoctorOrder-button").on("click", function() {
			$("#modal-operate-confirm").modal("hide");
			switch(SHOWTYPE) { //操作类型
				case 2:
					$('#scancode-text').focus();
					break;
				default:
					switch(CHECKED_BUTTON) {
						case 1: //明细重置
							refreshDataAndContent(0);
							break;
						case 2: //明细整单验收
							allDataCheck(2);
							break;
						case 3: //明细确认提交
							checkSaveData(function() {
								AllList = []; //清空所有明细信息
								resetScanCodeParams();
								location.href = "list.html?account=" + ACCOUNT + "&password=" + PASSWORD + "&t=" + new Date().getTime();
							});
							break;
						default:
							ShellComponent.messagebox.msg("操作错误！");
							break;
					}
					break;
			}
		});

		//扫码--当按下回车键时扫码	
		$('#scancode-text').keydown(function(e) {
			if(e.keyCode == 13) {
				var barcode = $('#scancode-text').val();
				if(!barcode) return;
				setMarkScanCodeLocalData(barcode);
			}
		});
		//扫码--确定扫码
		$("#scancode-div-check-ok").on(Shell.util.Event.click, function(e) {
			if(!Shell.util.Event.isClick()) return;
			confirmMarkLocalData();
		});
	}
	//明细--供货单整单验收
	function allDataCheck(mark) {
		SHOWTYPE = 1;
		refreshDataAndContent(mark);
	}
	//明细--打开供货单明细扫码
	function showScanCode() {
		setTitle();
		resetScanCodeParams();
		setDtHidden(true);
		setScanCodeHidden(false);
	}
	//明细--加载供货单明细列表数据
	function loadDtlData(callback) {
		var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true&fields=BmsCenSaleDtl_GoodsName,BmsCenSaleDtl_ProdGoodsNo,BmsCenSaleDtl_LotNo,BmsCenSaleDtl_InvalidDate,BmsCenSaleDtl_GoodsUnit,BmsCenSaleDtl_UnitMemo,BmsCenSaleDtl_GoodsQty,BmsCenSaleDtl_Price,BmsCenSaleDtl_SumTotal,BmsCenSaleDtl_Id,BmsCenSaleDtl_GoodsSerial,BmsCenSaleDtl_MixSerial'
		url += '&page=1&start=0&limit=10000';
		url += '&where=bmscensaledtl.BmsCenSaleDoc.Id=' + PK;
		url += ("&t=" + new Date().getTime());
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async: false,
			url: url
		}, function(data) {
			setTimeout(function() {
				ShellComponent.mask.hide();
				callback(data);
			}, 100);
		});
	}
	//明细--创建供货单详细列表内容
	function createDtHtml(list) {
		list = list || [];
		var html = [];
		CHECKSUMTOTAL = 0;
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

		html.push('<div style="padding:2px;margin:2px;margin-top:-77px;width:70px;float:right;">');
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
	//明细--验收数量Html处理
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
			checkInfo = "验收:" + checkQty + unit;
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
	//明细--找出某一合并行的验收数量
	function getDheckQty(value) {
		var checkQty = 0;
		var qty = {
			rightQty: 0, //正解扫码数量
			errorQty: 0, //拒收扫码数量
			uncheckQty: 0, //未验收数量
			checkQty: 0 //已验收数量
		};
		if(!AllList) AllList = [];
		var MixSerial1 = getSplitMixSerial(value[SCANCODEFILE]);
		for(var i = 0; i < AllList.length; i++) {
			var MixSerial2 = getSplitMixSerial(AllList[i][SCANCODEFILE]);
			//产品条码
			//if(value.BmsCenSaleDtl_GoodsSerial === AllList[i].BmsCenSaleDtl_GoodsSerial) {
			if(MixSerial1 == MixSerial2) {
				//扫码标志
				switch(AllList[i].ScanCodeMark) {
					case 0:
						qty.uncheckQty = qty.uncheckQty + 1;
						break;
					case 1:
						qty.errorQty = qty.errorQty + 1;
						break;
					case 2:
						qty.rightQty = qty.rightQty + 1;
						CHECKSUMTOTAL += parseInt(AllList[i].BmsCenSaleDtl_Price);
						break;
					default:
						break;
				}
			}
		}
		qty.checkQty = qty.rightQty;
		return qty;
	}
	//明细--依产品条码合并明细数据
	function mergerData(list, bo) {
		list = list || [];
		var map = {},
			data = [];
		if(bo) {
			for(var i = 0; i < list.length; i++) {
				var model = $.extend(true, {}, list[i]);
				//混合条码BmsCenSaleDtl_MixSerial
				var MixSerial = getSplitMixSerial(model[SCANCODEFILE]);
				if(!MixSerial) continue;

				if(MixSerial && !map[MixSerial]) {
					map[MixSerial] = model;
				} else {
					var GoodsQty = model.BmsCenSaleDtl_GoodsQty;
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
	 * 明细--初始化供货单明细数据(明细手工添加扫码标志)
	 * @param {Object} list 供货单原始明细数据
	 * @param {Object} mark 扫码标志:0为没有扫码确认;2为接收扫码确认;1为拒收扫码确认
	 */
	function setMarkDtLocalData(mark) {
		ALLSUMTOTAL = 0;
		for(var i = 0; i < AllList.length; i++) {
			AllList[i].ScanCodeMark = mark;
			ALLSUMTOTAL += parseInt(AllList[i].BmsCenSaleDtl_Price);
		}
	}
	/***
	 * 明细--还原供货单明细数据(明细手工添加扫码标志)
	 * @param {Object} mark 扫码标志:0为没有扫码确认;2为接收扫码确认;1为拒收扫码确认
	 */
	function refreshDataAndContent(mark) {
		if(!AllList) AllList = [];
		setMarkDtLocalData(mark);
		var data = mergerData(AllList, true);
		var dtlHtml = createDtHtml(data);
		$("#dt-div-content").html("");
		$("#dt-div-content").html(dtlHtml);
		setShowSumTotal();
	}
	//设置显示金额 #d9534f  #d9534f
	function setShowSumTotal() {
		var html = "";
		html += '<span>供货:<b style="color:#d9534f;">' + ALLSUMTOTAL + '</b>元</span>&nbsp;&nbsp;';
		html += '<span>验收:<b style="color:#d9534f;">' + CHECKSUMTOTAL + '</b>元</span>&nbsp;&nbsp;';
		if(CHECKSUMTOTAL != ALLSUMTOTAL) {
			html += '<span style="padding:2px;background-color:#d9534f;color:#ffffff;font-size:10px;">缺</span>';
		}
		$("#dt-show-total").html(html);
	}
	//供货单验收处理
	function checkSaveData(callback) {
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_ConfirmSaleDocByIDAndDtlIDList';
		var idStr = "";
		for(var i = 0; i < AllList.length; i++) {
			var mark = AllList[i].ScanCodeMark;
			if(mark) mark = parseInt(mark);
			switch(mark) {
				case 1: //拒收扫码,有异常的单子 会把条码信息清空
					idStr = idStr + AllList[i].BmsCenSaleDtl_Id + ",";
					break;
				default:
					//mark=0(待扫码)或mark=2(接收扫码)都为验收
					break;
			}
		}
		if(idStr && idStr.length > 0) idStr = idStr.substring(0, idStr.length - 1);
		var entity = {
			saleDocID: PK,
			saleDtlIDList: idStr //拒收扫码的明细Id
		};
		var data = Shell.util.JSON.encode(entity);
		ShellComponent.mask.save();
		Shell.util.Server.ajax({
			type: 'post',
			url: url,
			data: data
		}, function(data) {
			setTimeout(function() {
				ShellComponent.mask.hide();
				if(data.success) {
					callback();
				} else {
					ShellComponent.messagebox.msg(data.msg);
				}
			}, 100);
		});
	}
	//明细--加载明细列表数据
	function loadDtContent() {
		if(AllList && AllList.length > 0) {
			var data = mergerData(AllList, true);
			var dtlHtml = createDtHtml(data);
			$("#dt-div-content").html(dtlHtml);
			setShowSumTotal();
		} else {
			AllList = [];
			//加载供货单明细列表数据
			loadDtlData(function(value) {
				if(value.success) {
					var mark = 0;
					$("#dt-div-submit").show();
					AllList = value.value.list;
					setMarkDtLocalData(mark);
					var data = mergerData(AllList, true);
					var dtlHtml = createDtHtml(data);
					$("#dt-div-content").html(dtlHtml);
					setShowSumTotal();
				} else {
					$("#dt-div-content").html('<div class="error-div">' + value.msg + '</div>');
				}
			});
		}
	}

	//扫码--依扫码找出明细行并更新扫码标志
	function setMarkScanCodeLocalData(barcode) {
		if(AllList.length == 0) return;
		//当前条码是否存在所有明细的集合里的验证
		var isValidAll = false;
		//当前条码是否存在当前已经扫过的明细的集合里的验证
		var isValidCur = true;

		//当前已扫码明细,判断是否存在当前扫码明细里
		for(var i = 0; i < CurBarCodeList.length; i++) {
			var barcode2 = CurBarCodeList[i][SCANCODEFILE];
			if(barcode == barcode2) {
				isValidCur = false;
				break;
			}
		}
		//所有明细,当前条码存在所有明细中时,才生成添加行并总数加1
		if(isValidCur == true) {
			for(var i = 0; i < AllList.length; i++) {
				var barcode2 = AllList[i][SCANCODEFILE];
				if(barcode == barcode2) {
					isValidAll = true;
					CurBarCodeList.push(AllList[i]);
					SCANCODECOUNT = SCANCODECOUNT + 1;
					createScanCodeHtml(AllList[i]);
					break;
				}
			}
		}
		$('#scancode-text').val("");
		var info = "";
		if(!isValidCur) {
			info = "该条码已被扫描！"
		}
		if(isValidCur == true && !isValidAll) {
			info = "该供货单中不存在条码为:" + barcode + "的产品!";
		}
		if(info) {
			$('#scancode-text').blur();
			$("#createDoctorOrder-msg").html(info);
			$("#createDoctorOrder-footer").show();
			$("#modal-operate-confirm").modal({
				backdrop: 'static',
				keyboard: false
			});
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
				var MixSerial = getSplitMixSerial(CurBarCodeList[i][SCANCODEFILE]);
				if(!MixSerial) continue;
				if(!DtlMap[MixSerial]) DtlMap[MixSerial] = [];
			}
			//获取所有与扫描条码一致的产品
			for(var mapMixSerial in DtlMap) {
				for(var j in AllList) {
					var MixSerial = getSplitMixSerial(AllList[j][SCANCODEFILE]);
					if(!MixSerial) continue;
					//混合条码的第8位或前三段相同
					if(mapMixSerial == MixSerial) {
						DtlMap[mapMixSerial].push(AllList[j]);
					}
				}
			}
			//扫描产品相关的所有明细数据
			var List = [];
			for(var mapMixSerial in DtlMap) {
				List = List.concat(DtlMap[mapMixSerial]);
			}
			//先设置某一相同产品但不在当前已扫码数据的的标志值(ScanCodeMark)为当前标志(正确为2;错误为1)的相反值
			for(var j in List) {
				var indexOf = AllList.indexOf(List[j]);
				if(indexOf != -1) AllList[indexOf].ScanCodeMark = (MARK == 1 ? 2 : 1);
			}
			//设置当前扫码明细的标志值为当前扫码标志值
			for(var i in CurBarCodeList) {
				var dtl = CurBarCodeList[i][SCANCODEFILE];
				for(var j in AllList) {
					if(dtl == AllList[j][SCANCODEFILE]) {
						AllList[j].ScanCodeMark = MARK;
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
	function createScanCodeHtml(model) {
		var html = [];
		createScanCodeRow(model);
		html.push('<div>');
		if(HTMLROWS.length == 0) {
			html.push('<div class="no-data-div">没有找到数据!</div>'); //没有数据
		} else {
			for(var i = 0; i < HTMLROWS.length; i++) {
				html.push(HTMLROWS[i]);
			}
		}
		html.push('</div>');
		var dtlHtml = html.join('');
		$("#scancode-div-content").html(dtlHtml);

		var showinfo = "总数" + SCANCODECOUNT;
		$("#scancode-div-show-count").html(showinfo);

	}
	//扫码--创建扫码存在所有明细中的html数据行内容
	function createScanCodeRow(value) {
		var html = [];
		var id = value.BmsCenSaleDtl_Id;
		var MixSerial = value[SCANCODEFILE];
		var tempArr = [];
		if(MixSerial) tempArr = MixSerial.split("|");
		//条码显示只取混合条码分割"|"后的数组的后三位
		if(tempArr && tempArr.length == 4) {
			MixSerial = tempArr[3];
		} else if(tempArr && tempArr.length >= 9) {
			if(tempArr[7]) MixSerial = tempArr[7] + "|";
			if(tempArr[8]) MixSerial = MixSerial + tempArr[8] + "|";
			if(tempArr[9]) MixSerial = MixSerial + tempArr[9] + "|";
			if(MixSerial && MixSerial.length > 0) MixSerial = MixSerial.substring(0, MixSerial.length - 1);
		}
		if(!MixSerial) MixSerial = "";

		var title = '' + value.BmsCenSaleDtl_GoodsName + '';
		var LotNo = '批号:' + value.BmsCenSaleDtl_LotNo;
		var UnitMemo = '规格:' + value.BmsCenSaleDtl_UnitMemo;
		MixSerial = '条码:' + MixSerial;
		var InvalidDate = '有效期至:' + Shell.util.Date.toString(value.BmsCenSaleDtl_InvalidDate, true);
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
		html.push('<a class="list-group-item" style="margin:2px;">');
		html.push('<h4 class="list-group-item-heading">' + title + '</h4>');

		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + LotNo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UnitMemo + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + MixSerial + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + InvalidDate + '</div>');
		html.push('</a>');
		html = html.join('');

		HTMLROWS.push(html);
	}
	//联动时明细操作相关DIV显示或隐藏
	function setDtHidden(hidden) {
		switch(hidden) {
			case true:
				$("#dt-show-total").hide();
				$("#dt-div-buttons").hide();
				$("#dt-div-content").hide();
				$("#dt-div-submit").hide();
				break;
			default:
				$("#dt-show-total").show();
				$("#dt-div-buttons").show();
				$("#dt-div-content").show();
				$("#dt-div-submit").show();
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
		//已扫码明细的HTML行
		HTMLROWS = [];
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
				location.href = "list.html?account=" + ACCOUNT + "&password=" + PASSWORD + "&t=" + new Date().getTime();
				break;
		}
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