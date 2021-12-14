$(function() {
	//外部参数
	var params = Shell.util.getRequestParams(true);
	//账号
	var ACCOUNT = params.ACCOUNT || "";
	//密码
	var PASSWORD = params.PASSWORD || "";
	//是否需要重新登录:不需要:0;需要:1
	var ISLOGIN = params.ISLOGIN || 1;
	//验收时是否需要双确认:不需要:0(不需要双确认时,验收时secAccepterType值不用传回后台),默认本实验室:1,供应商:2,供应商或实验室:3
	var SECACCEPTERTYPE = params.SECACCEPTERTYPE || 0;
	//机构ID
	var LABID = "";
	//当前已经扫码的条码集合
	var CurBarCodeList = [];
	//提交类型:暂存:0,确认提交:1
	var SUBMITTYPE = 0;
	//当前机构的所有供货商的集合信息
	var COMPORGDATA = null;
	//初始化监听
	function initListeners() {
		var isPC = Shell.util.Event.isPC();
		Shell.util.Event.click = "touchend";
		if(isPC) Shell.util.Event.click = "click";
		//扫码确认提取供货单--当按下回车键时扫码	
		$('#text-saleDocNo').keydown(function(e) {
			if(e.keyCode == 13) {
				var barcode = $('#text-saleDocNo').val();
				if(!barcode) return;
				confirmextractdata();
			}
		});
		//监听供应商选择
		$("#CompOrgOption ul li").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			$("#CompOrgOption").attr("data", $(this).attr("data"));
			$("#CompOrgOption a").first().html($(this).text() + ' <span class="caret"></span>');
		});
		//刷新
		$("#div-refresh-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			$("#scancode-div-content").html("");
			refreshContent();
		});
		//明细暂存提交操作
		$("#dt-btn-temp-save").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			SUBMITTYPE = 1;
			onShowConfirm();
		});
		//明细确认提交操作
		$("#dt-btn-save-check").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			SUBMITTYPE = 0;
		});
	}
	function onShowConfirm() {
		if(!CurBarCodeList || CurBarCodeList.length == 0) {
			ShellComponent.messagebox.msg("获取供货明细信息为空!");
		} else {
			setCheckDefaultValue();
			$("#check-data-footer").show();
			$("#modal-check-confirm").modal({
				backdrop: 'static',
				keyboard: false
			});
		}
	}
	function refreshContent() {
		if(!CurBarCodeList) CurBarCodeList = [];
		$('#scancode-text').val("");
		$('#scancode-div-content').html("");
		$('#scancode-text').focus();
	}
	//设置获取供应商下拉选择
	function setCompOrgOptionData(listData) {
		var listHtml = [];
		for(var i = 0; i < listData.length; i++) {
			var text = listData[i].CenOrg_CName + "(" + listData[i].CenOrg_OrgNo + ")";
			listHtml.push('<li role="presentation" data="' + listData[i].CenOrg_Id + '"><a href="#">' + text + '</a></li>');
		}
		$("#CompOrgOption ul").html(listHtml.join(""));
		//设置默认人供应商
		if(listData.length > 0) {
			var text = listData[0].CenOrg_CName + "(" + listData[0].CenOrg_OrgNo + ")";
			$("#CompOrgOption").attr("data", listData[0].CenOrg_Id);
			$("#CompOrgOption a").first().html(text + ' <span class="caret"></span>');
			//监听供应商选择
			$("#CompOrgOption ul li").on(Shell.util.Event.click, function() {
				if(!Shell.util.Event.isClick()) return;
				$("#CompOrgOption").attr("data", $(this).attr("data"));
				$("#CompOrgOption a").first().html($(this).text() + ' <span class="caret"></span>');
			});
		}
	}
	//确认提取供货单
	function confirmextractdata() {
		if(!LABID) {
			ShellComponent.messagebox.msg("非法登录:缺失账户信息!");
			//showError("非法登录:缺失账户信息!");
			return;
		}
		var compOrgId = $('#CompOrgOption').attr("data");
		var saleDocNo = $('#text-saleDocNo').val();
		var msgInfo = "";
		if(!compOrgId) {
			msgInfo = "供应商选择为空！";
		}
		if(!saleDocNo) {
			if(msgInfo) msgInfo += "<br />";
			msgInfo += "供货单号为空！";
		}
		if(msgInfo) {
			ShellComponent.messagebox.msg(msgInfo);
			//showError(msgInfo);
			return;
		}
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_InputSaleDocInterface';
		var where = '?compOrgId=' + compOrgId + "&saleDocNo=" + saleDocNo; // + '&labOrgId='+LABID;
		url += where;
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async: false,
			url: url
		}, function(data) {
			ShellComponent.mask.hide();
			if(data.success) {
				$("#modal-operate-confirm").modal("hide");

			} else {
				ShellComponent.messagebox.msg(data.ErrorInfo); //data.msg
			}
		});
	}
	/***
	 * 创建数据行内容
	 * @param {Object} model 为合并行后的信息
	 * @param {Object} otype 明细列表显示:1;扫码明细列表显示:2
	 */
	function createRowOfDt(model, otype) {

	}
	//登录
	Shell.check.loginIn({
		ISLOGIN: ISLOGIN,
		ACCOUNT: ACCOUNT,
		PASSWORD: PASSWORD,
		LABID: LABID
	}, function(userInfo, data) {
		LABID = userInfo.LABID;
		ISLOGIN = userInfo.ISLOGIN;
		if(data.success) {
			initListeners();
			if(!COMPORGDATA || COMPORGDATA.length == 0) {
				Shell.check.login.loadCompOrgOptionData(LABID, function(data) {
					COMPORGDATA = data;
					setCompOrgOptionData(data);
				});
			}
		} else {
			showError(data.msg);
		}
	});
	//错误信息显示
	function showError(msg) {
		var html = [];
		html.push('<div class="alert alert-warning" style="float:left;width:90%;margin:20px 5%;text-align:center;">' + msg + '</div>');
		$("#scancode-div-content").html(html);
	}

});