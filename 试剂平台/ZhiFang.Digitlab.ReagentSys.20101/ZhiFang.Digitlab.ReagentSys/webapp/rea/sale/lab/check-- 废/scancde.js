$(function() {
	//正确扫码的标题
	var SUCCESS_TITLE = "正确扫码";
	//错误扫码的标题
	var ERROR_TITLE = "错误扫码";
	//正确扫码的提示内容
	var SUCCESS_MSG = "凡正确扫码的产品都是通过验收的";
	//错误扫码的提示内容
	var ERROR_MSG = "凡错误扫码的产品都是不验收的";

	//供货单明细本地LocalStorage的Key
	var LocalStorage_BmsCenSaleDtl = "BmsCenSaleDtl";
	var PK = Shell.util.getRequestParams(false).id;
	var MARK = Shell.util.getRequestParams(false).mark;

	//扫码字段
	var SCANCODEFILE = "BmsCenSaleDtl_MixSerial";
	//扫码总数
	var SCANCODECOUNT = 0;
	//已经扫码的HTML行
	var HTMLROWS = [];
	//当前已经扫码的条码集合
	var CurBarCodeList = [];

	//初始化监听
	function initListeners() {
		//确定扫码
		$("#div-check-ok").on(Shell.util.Event.click, function(e) {
			confirmMarkLocalData();
			history.go(-1);
		});
		//当按下回车键时扫码	
		$('#txt-scancode').keydown(function(e) {
			if(e.keyCode == 13) {
				var barcode = $('#txt-scancode').val();
				if(!barcode) return;
				setMarkLocalData(barcode);
			}
		});
		//弹出确定按钮处理
		$("#createDoctorOrder-button").on("click", function() {
			$("#createDoctorOrderModal").modal("hide");
			$('#txt-scancode').focus();
		});
	}
	/****
	 * 依扫码找出明细行并更新扫码标志
	 * @param {Object} list 供货单原始明细数据
	 * @param {Object} mark 扫码标志:0为没有扫码确认;1为正确扫码确认;2为错误扫码确认
	 */
	function setMarkLocalData(barcode) {
		var allList = Shell.util.LocalStorage.get(LocalStorage_BmsCenSaleDtl);
		allList = allList ? Shell.util.JSON.decode(allList) : [];
		if(allList.length == 0) return;
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
			for(var i = 0; i < allList.length; i++) {
				var barcode2 = allList[i][SCANCODEFILE];
				if(barcode == barcode2) {
					isValidAll = true;
					CurBarCodeList.push(allList[i]);
					SCANCODECOUNT = SCANCODECOUNT + 1;
					createDtl(allList[i]);
					break;
				}
			}
		}
		$('#txt-scancode').val("");
		var info = "";
		if(!isValidCur) {
			//info = "当前条码为:" + barcode + "的已被扫码!";
			info = "该条码已被扫描！"
		}
		if(isValidCur == true && !isValidAll) {
			info = "该供货单中不存在条码为:" + barcode + "的产品!";
		}
		if(info) {
			$('#txt-scancode').blur();
			$("#createDoctorOrder-msg").html(info);
			$("#createDoctorOrder-footer").show();
			$("#createDoctorOrderModal").modal({
				backdrop: 'static',
				keyboard: false
			});
		}
	}
	/***
	 * 确认按钮操作
	 * 处理当前已经扫过码之外的明细行的扫码标志值
	 * 如当前为错误扫码,某一产品有6箱,只确认扫了一箱错误条码,需要处理其他5箱为正确扫码
	 */
	function confirmMarkLocalData() {
		var allList = Shell.util.LocalStorage.get(LocalStorage_BmsCenSaleDtl);
		allList = Shell.util.JSON.decode(allList);
		if(!allList) allList = [];
		if(CurBarCodeList.length == 0) return;
		if(allList.length == 0) return;
		//扫描条码隶属的批号产品map
		var DtlMap = {};
		//已经扫过条码集合
		for(var i in CurBarCodeList) {
			var dtl = CurBarCodeList[i][SCANCODEFILE].split("|")[7];
			if(!DtlMap[dtl]) DtlMap[dtl] = [];
		}
		//获取所有与扫描条码一致的产品
		for(var i in DtlMap) {
			for(var j in allList) {
				var dtl = allList[j][SCANCODEFILE].split("|")[7];
				//混合条码的第8位相同
				if(i == dtl) {
					DtlMap[i].push(allList[j]);
				}
			}
		}

		//扫描产品相关的所有明细数据
		var List = [];
		for(var i in DtlMap) {
			List = List.concat(DtlMap[i]);
		}
		//先设置某一相同产品的的标志值(ScanCodeMark)为当前标志(正确为2;错误为1)的相反值
		for(var j in List) {
			var indexOf = allList.indexOf(List[j]);
			if(indexOf != -1) allList[indexOf].ScanCodeMark = (MARK == 1 ? 2 : 1);
		}
		//设置当前扫码明细的标志值为当前扫码标志值
		for(var i in CurBarCodeList) {
			var dtl = CurBarCodeList[i][SCANCODEFILE];
			for(var j in allList) {
				if(dtl == allList[j][SCANCODEFILE]) {
					allList[j].ScanCodeMark = MARK;
					break;
				}
			}
		}

		if(allList && allList.length > 0) {
			Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, Shell.util.JSON.encode(allList));
		} else {
			Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, "");
		}
	}
	//创建供货单详细列表内容
	function createDtl(model) {
		var html = [];
		createRow(model);
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
		$("#ContentDiv").html(dtlHtml);

		var showinfo = "总数" + SCANCODECOUNT;
		$("#div-show-count").html(showinfo);
	}
	//创建数据行内容
	function createRow(value) {
		var html = [];
		var id = value.BmsCenSaleDtl_Id;
		var MixSerial = value[SCANCODEFILE];
		var tempArr2 = [];
		if(MixSerial) tempArr2 = MixSerial.split("|");
		//条码显示只取混合条码分割"|"后的数组的后三位
		if(tempArr2 && tempArr2.length > 0) {
			if(tempArr2[7])MixSerial = tempArr2[7]+ "|";
			if(tempArr2[8])MixSerial = MixSerial+tempArr2[8]+ "|";
			if(tempArr2[9])MixSerial = MixSerial+tempArr2[9]+ "|";
			if(MixSerial&&MixSerial.length>0)MixSerial=MixSerial.substring(0,MixSerial.length-1);
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
	//重置还原变量的初始值
	function resetParams() {
		if(MARK) MARK = parseInt(MARK);
		SCANCODECOUNT = 0;
		HTMLROWS = [];
		CurBarCodeList = [];

		var info = (MARK == 1 ? ERROR_MSG : SUCCESS_MSG);
		var title = (MARK == 1 ? ERROR_TITLE : SUCCESS_TITLE);
		$("#div-show-title").html(title);
		$("#div-show-info").html(info);

		$('#txt-scancode').val("");
		$('ContentDiv').val("");
		$("#div-show-count").val("总数");
		$('#txt-scancode').focus();
	}
	//判断客户端是移动设备还是PC,设置单击事件
	function browserEventClick() {
		var isPC = Shell.util.Event.isPC();
		if(isPC) {
			Shell.util.Event.click = "click";
			//返回按钮监听
			$(".navbar-top-back").on(Shell.util.Event.click, function() {
				location.href = "info.html?id=" + PK;
			});
		} else {
			Shell.util.Event.click = "touchend";
		}
		initListeners();
	}
	browserEventClick();
	resetParams();
});