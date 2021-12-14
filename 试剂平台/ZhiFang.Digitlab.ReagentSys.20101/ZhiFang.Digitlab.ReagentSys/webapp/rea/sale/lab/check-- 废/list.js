$(function() {
	//外部参数
	var params = Shell.util.getRequestParams(true);
	//账号
	var ACCOUNT = params.ACCOUNT || "";
	//密码
	var PASSWORD = params.PASSWORD || "";
	//机构ID
	var LABID = "";

	//供货单明细本地LocalStorage的Key
	var LocalStorage_BmsCenSaleDtl = "BmsCenSaleDtl";

	//每页加载多少条数据
	var LIMIT = 20;
	//加载第几页
	var PAGE = 1;

	//登录
	function login() {
		if(ACCOUNT && PASSWORD) {
			var url = Shell.util.Path.ROOT + '/RBACService.svc/RBAC_BA_Login' +
				'?strUserAccount=' + ACCOUNT + '&strPassWord=' + PASSWORD;
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
				//单站点登录用户及密码
				Shell.util.LocalStorage.set("S_ACCOUNT", account);
				Shell.util.LocalStorage.set("S_PASSWORD", password);
				LABID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
				refreshContent();
			} else {
				showError("参数:ACCOUNT,PASSWORD的账号或密码错误！");
			}
		});
	}
	//错误信息显示
	function showError(msg) {
		Shell.util.LocalStorage.set("S_ACCOUNT", "");
		Shell.util.LocalStorage.set("S_PASSWORD", "");
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
			var where = 'bmscensaledoc.Status=2 and bmscensaledoc.IsSplit=1 and bmscensaledoc.Lab.Id=' + LABID;
			where += "&page=" + PAGE + "&limit=" + LIMIT;

			var sort = "[{\"property\":\"BmsCenSaleDoc_DataAddTime\",\"direction\":\"ASC\"}]";
			url += '&fields=' + fields.join(',') + "&where=" + where + "&sort=" + sort;

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
		//供货单列表数据重新生成时,清空下原明细列表的本地缓存数据
		Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, "");
		if(!data.success) {
			$("#div-loadmore-data").hide();
			if(PAGE == 1) {
				html.push('<div class="alert alert-warning" style="float:left;width:90%;margin:20px 5%;text-align:center;">' + data.msg + '</div>'); //错误信息
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
					html.push('<div class="alert alert-success" style="float:left;width:90%;margin:20px 5%;text-align:center;">没有找到数据!</div>'); //没有数据
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

	//查看供货单内容
	function showInfo(id) {
		Shell.util.LocalStorage.set(LocalStorage_BmsCenSaleDtl, "");
		location.href = 'info.html?id=' + id;
	}
	//刷新列表数据
	function refreshContent() {
		//加载数据
		loadData(function(data) {
			initListeners();
		});
	}
	//判断客户端是移动设备还是PC,设置单击事件
	function browserEventClick() {
		var isPC = Shell.util.Event.isPC();
		if(isPC) {
			Shell.util.Event.click = "click";
		} else {
			Shell.util.Event.click = "touchend";
		}
		//刷新
		$("#div-refresh-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			PAGE = 1;
			$("#ContentDiv").html("");
			refreshContent();
		});
		//加载更多
		$("#div-loadmore-data").on(Shell.util.Event.click, function() {
			if(!Shell.util.Event.isClick()) return;
			PAGE++;
			refreshContent();
		});
	}
	browserEventClick();
	login();
});