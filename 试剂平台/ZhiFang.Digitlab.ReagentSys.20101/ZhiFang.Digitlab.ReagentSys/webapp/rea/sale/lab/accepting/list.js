$(function() {
	//外部参数
	var params = Shell.util.getRequestParams(true);
	//账号
	var ACCOUNT = params.ACCOUNT || "";
	//密码
	var PASSWORD = params.PASSWORD || "";
	//机构ID
	var LABID = "";

	//每页加载多少条数据
	var LIMIT = 10;
	//加载第几页
	var PAGE = 1;

	//登录
	function login() {
		if(ACCOUNT && PASSWORD) {
			var url = Shell.util.Path.ROOT + '/RBACService.svc/RBAC_BA_Login' +
				'?strUserAccount=' + ACCOUNT + '&strPassWord=' + PASSWORD + "&t=" + new Date().getTime();
			ShellComponent.mask.loading();
			//获取账户信息
			Shell.util.Server.ajax({
				async: true,
				url: url
			}, function(data) {
				setTimeout(function() {
					ShellComponent.mask.hide();
					if(data == true) {
						loginSuccess(ACCOUNT, PASSWORD); //登录成功
					} else {
						showError("账号或密码错误！");
					}
				}, 100);
			});
		} else {
			showError("参数:ACCOUNT,PASSWORD的账号或密码错误！");
		}
	}
	//登录成功
	function loginSuccess(account, password) {
		Shell.Rea.onAfterLogin(function(data) {
			if(data.success) {
				//机构id
				LABID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
				refreshContent();
			} else {
				showError("参数:ACCOUNT,PASSWORD的账号或密码错误！");
			}
		});
	}
	//错误信息显示
	function showError(msg) {
		var html = [];
		html.push('<div class="alert alert-warning" style="float:left;width:90%;margin:20px 5%;text-align:center;">' + msg + '</div>');
		$("#div-loadmore-data").hide();
		$("#ContentDiv").html(html);
	}

	//加载供货单列表数据
	function loadData(callback) {
		if(LABID) {
			var url = Shell.util.Path.ROOT + '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true';
			var fields = [
				'BmsCenSaleDoc_Id', 'BmsCenSaleDoc_SaleDocNo', 'BmsCenSaleDoc_Lab_CName',
				'BmsCenSaleDoc_UrgentFlag', 'BmsCenSaleDoc_Status', 'BmsCenSaleDoc_IOFlag',
				'BmsCenSaleDoc_UserName', 'BmsCenSaleDoc_OperDate'
			];
			var where = '(bmscensaledoc.DeleteFlag=0 or bmscensaledoc.DeleteFlag is null) and bmscensaledoc.Status=2 and bmscensaledoc.IsSplit=1 and bmscensaledoc.Lab.Id=' + LABID;
			where += "&page=" + PAGE + "&limit=" + LIMIT;

			var sort = "[{\"property\":\"BmsCenSaleDoc_OperDate\",\"direction\":\"DESC\"}]";
			url += '&fields=' + fields.join(',') + "&where=" + where + "&sort=" + sort;
			url += ("&t=" + new Date().getTime());
			ShellComponent.mask.loading();
			Shell.util.Server.ajax({
				async: false,
				url: url
			}, function(data) {
				setTimeout(function() {
					ShellComponent.mask.hide();
					var html = createList(data);
					$("#ContentDiv").append(html);
					callback(data);
				}, 100);
			});
		} else {
			showError("非法登录:缺失账户信息!");
		}
	}
	//创建列表内容
	function createList(data) {
		var html = [];
		if(!data.success) {
			$("#div-loadmore-data").hide();
			if(PAGE == 1) {
				html.push('<div class="alert alert-warning" style="float:left;width:90%;margin:20px 5%;text-align:center;">' + data.msg + '</div>');
			}
		} else {
			var list = data.value.list || [],
				len = list.length;
			for(var i = 0; i < len; i++) {
				var row = createRow(list[i]);
				html.push(row);
			}
			if(len == 0) {
				$("#div-loadmore-data").hide();
				if(PAGE == 1) {
					html.push('<div class="alert alert-success" style="float:left;width:90%;margin:20px 5%;text-align:center;">没有找到数据!</div>');
				}
			} else if(len < LIMIT) {		
				$("#div-loadmore-data").hide();
			} else {
				$("#div-loadmore-data").show();
			}
		}
		return html.join('');
	}
	//创建数据行内容
	function createRow(value) {
		var html = [];
		var id = value.BmsCenSaleDoc_Id;
		var title = '订货方:' + value.BmsCenSaleDoc_Lab_CName + '';
		var info = '供货单号:' + value.BmsCenSaleDoc_SaleDocNo;
		var UserName = '操作人员:' + value.BmsCenSaleDoc_UserName;
		var OperDate = '操作时间:' + value.BmsCenSaleDoc_OperDate;

		var pStyle = 'margin:2px;font-size:11px;';
		html.push('<a class="list-group-item" data="' + id + '">');
		html.push('<h4 class="list-group-item-heading">' + title + '</h4>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + info + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + UserName + '</div>');
		html.push('<div class="list-group-item-text" style="' + pStyle + '">' + OperDate + '</div>');

		//状态标识
		var UrgentFlag = Shell.Rea.Enum.BmsCenSaleDoc_UrgentFlag['E' + value.BmsCenSaleDoc_UrgentFlag];
		var Status = Shell.Rea.Enum.BmsCenSaleDoc_Status['E' + value.BmsCenSaleDoc_Status];
		var IOFlag = Shell.Rea.Enum.BmsCenSaleDoc_IOFlag['E' + value.BmsCenSaleDoc_IOFlag];
		html.push('<div style="float:right;margin-top:-70px;">');
		var divStyle = 'width:60px;text-align:center;padding:2px;margin:2px;font-size:11px;';
		html.push('<div style="' + divStyle + 'background-color:' + UrgentFlag.bcolor +
			';color:' + UrgentFlag.color + ';">' + UrgentFlag.value + '</div>');

		html.push('<div style="' + divStyle + 'background-color:' + Status.bcolor +
			';color:' + Status.color + ';">' + Status.value + '</div>');
		html.push('<div style="' + divStyle + 'background-color:' + IOFlag.bcolor +
			';color:' + IOFlag.color + ';">' + IOFlag.value + '</div>');
		html.push('</div>');
		html.push('</a>');

		return html.join('');
	}
	//初始化列表监听
	function initListeners() {
		$(".list-group-item").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			var id = $(this).attr("data");
			//修改供货单视图
			showInfo(id);
		});
	}
	//供货单待验收处理
	function showInfo(id) {
		location.href = 'info.html?id=' + id + "&account=" + ACCOUNT + "&password=" + PASSWORD + "&t=" + new Date().getTime();
	}
	//刷新列表数据
	function refreshContent() {
		$("#ContentDiv").html("");
		PAGE = 1;
		loadData(function(data) {
			initListeners();
		});
	}
	//加载更多数据
	function loadMoreData() {
		PAGE++;
		loadData(function(data) {
			initListeners();
			if(!data.value||!data.value.list||data.value.list.length==0){
				PAGE--;
			}
		});
	}
	//提取供货单成功后列表数据刷新处理
	function extractRefreshData() {	
		refreshContent();
		
//		PAGE++;
//		loadData(function(data) {
//			initListeners();
//			if(!data.value||!data.value.list||data.value.list.length==0){
//				PAGE--;				
//				//refreshContent();
//			}
//		});
	}
	//弹出提取供货单
	function showExtractData() {
		$("#extract-data-footer").show();
		$("#modal-operate-confirm").modal({
			backdrop: 'static',
			keyboard: false
		});
		$('#text-saleDocNo').focus();
	}
	//获取供应商选择数据
	function getCompOrgOptionData() {
		if(!LABID) {
			ShellComponent.messagebox.msg("非法登录:缺失账户信息!");
			return;
		}
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_GetLabInterfaceOrgList';
		//url += ("&t=" + new Date().getTime());
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async: false,
			url: url
		}, function(data) {
			ShellComponent.mask.hide();
			if(data.success) {
				if(data.value && data.value.list) setCompOrgOptionData(data.value.list);
			} else {
				ShellComponent.messagebox.msg(data.ErrorInfo); //data.msg
			}
		});
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
			return;
		}
		var url = Shell.util.Path.ROOT + '/ReagentService.svc/RS_UDTO_InputSaleDocInterface';
		var where = '?compOrgId=' + compOrgId + "&saleDocNo=" + saleDocNo; // + '&labOrgId='+LABID;
		url += where;
		//url += ("&t=" + new Date().getTime());
		ShellComponent.mask.loading();
		Shell.util.Server.ajax({
			async: false,
			url: url
		}, function(data) {
			ShellComponent.mask.hide();
			if(data.success) {
				$("#modal-operate-confirm").modal("hide");
				extractRefreshData();
			} else {
				ShellComponent.messagebox.msg(data.ErrorInfo); //data.msg
			}
		});
	}
	//重置提取供货单选项信息
	function resetCompOrgOption() {
		$("#CompOrgOption").attr("data", "");
		$("#CompOrgOption a").first().html('供应商选择<span class="caret"></span>');
		$('#text-saleDocNo').val("");
	}
	//判断客户端是移动设备还是PC,设置单击事件
	function browserEventClick() {
		var isPC = Shell.util.Event.isPC();
		if(isPC) {
			Shell.util.Event.click = "click";
		} else {
			Shell.util.Event.click = "touchend";
		}
		//实验室获取供货单
		$("#div-extract-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			resetCompOrgOption();
			getCompOrgOptionData();
			showExtractData();
		});
		//确认提取供货单
		$("#button-confirm-extract-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			confirmextractdata();
		});
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
			$("#ContentDiv").html("");
			refreshContent();
		});
		//加载更多
		$("#div-loadmore-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			loadMoreData();
		});
		$("#btnExtract").mouseover(function() {			
			var isPC = Shell.util.Event.isPC();
			if(isPC) {
				//$("#btnExtract").addClass("btn btn-primary  btn-sm");
				this.style.border = "solid 2px #204d74";
			}			
		}).mouseout(function() {
			var isPC = Shell.util.Event.isPC();
			if(isPC) {
				//$("#btnExtract").addClass("btn btn-warning  btn-sm");
				this.style.border = "none";
			}			
		});
	}
	browserEventClick();
	login();
});