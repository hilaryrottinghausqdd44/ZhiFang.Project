$(function() {
	//外部参数
	var params = Shell.util.getRequestParams(true);
	//账号
	var ACCOUNT = params.ACCOUNT || "";
	//密码
	var PASSWORD = params.PASSWORD || "";
	//是否需要重新登录:不需要:0;需要:1
	var ISLOGIN = params.ISLOGIN || 1;
	//验收时是否需要双确认:默认本实验室,不需要:1,供应商,不需要:2,供应商或实验室,需要:3
	var SECACCEPTERTYPE = params.SECACCEPTERTYPE || 1;
	//条件类型:待验收 2、已验收 1
	var TYPE = params.TYPE || 2;
	//机构ID
	var LABID = "";
	//每页加载多少条数据
	var LIMIT = 10;
	//加载第几页
	var PAGE = 1;
	//当前tab选中的类型
	var CHECKED_TYPE = null;

	//登录
	function login() {
		//如果不需要重新登录
		if(ISLOGIN == 0) {
			//判断本地缓存是否存在,不存在,需要重新登录
			LABID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
			if(!LABID) {
				ISLOGIN = 1;
			}
		}
		if(ACCOUNT && PASSWORD) {
			if(ISLOGIN == 1) {
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
							loginSuccess(ACCOUNT, PASSWORD);
						} else {
							showError("账号或密码错误！");
						}
					}, 100);
				});
			} else { //不需要再次登录,如从明细验收后返回
				LABID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
				if(LABID) {
					//默认选中类型--待验收,刷新数据
					setTimeout(function() {
						$("#type-ul li[data='" + (TYPE || 2) + "']").click();
					}, 500);
				} else {
					ISLOGIN = 1;
					loginSuccess(ACCOUNT, PASSWORD);
				}
			}
		} else {
			ISLOGIN = 1;
			showError("参数:ACCOUNT,PASSWORD的账号或密码错误！");
		}
	}
	//登录成功
	function loginSuccess(account, password) {
		Shell.Rea.onAfterLogin(function(data) {
			if(data.success) {
				//机构id
				LABID = Shell.util.LocalStorage.get(Shell.Rea.LocalStorage.map.CENORGID);
				//默认选中类型--待验收,刷新数据
				setTimeout(function() {
					$("#type-ul li[data='" + (TYPE || 2) + "']").click();
				}, 500);
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
				'BmsCenSaleDoc_Id', 'BmsCenSaleDoc_SaleDocNo', 'BmsCenSaleDoc_CompanyName',
				'BmsCenSaleDoc_UrgentFlag', 'BmsCenSaleDoc_Status', 'BmsCenSaleDoc_IOFlag',
				'BmsCenSaleDoc_UserName', 'BmsCenSaleDoc_OperDate'
			];
			var where = getWhere();
			where += "&page=" + PAGE + "&limit=" + LIMIT;
			var sort = "[{\"property\":\"BmsCenSaleDoc_OperDate\",\"direction\":\"DESC\"}]";

			if(TYPE == 1) {
				fields.push("BmsCenSaleDoc_AccepterName", 'BmsCenSaleDoc_AccepterTime');
				sort = "[{\"property\":\"BmsCenSaleDoc_AccepterTime\",\"direction\":\"DESC\"}]";
			} else {
				fields.push("BmsCenSaleDoc_Checker", 'BmsCenSaleDoc_CheckTime');
			}
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
	//获取条件
	function getWhere() {
		var where = [];
		where.push("(bmscensaledoc.DeleteFlag=0 or bmscensaledoc.DeleteFlag is null) ");
		where.push("bmscensaledoc.IsSplit=1 ");
		where.push("bmscensaledoc.Lab.Id=" + LABID);
		if(TYPE) TYPE = parseInt(TYPE);
		//默认为待验收
		if(!TYPE) TYPE = 2;
		//待验收:2,已验收:1
		switch(TYPE) {
			case 2:
				where.push("bmscensaledoc.Status=2 ");
				break;
			case 1: //已验收
				where.push("bmscensaledoc.Status=1 ");
				var textSearch = $("#textSearch").val();
				if(textSearch) textSearch = textSearch.replace(/"/g, "");
				if(textSearch) where.push("bmscensaledoc.SaleDocNo like '" + textSearch + "%' ");
				break;
			default:
				break;
		}
		return where.join(" and ");
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
		var title = '供应商:' + value.BmsCenSaleDoc_CompanyName + '';
		var info = '供货单号:' + value.BmsCenSaleDoc_SaleDocNo;
		var UserName = '操作人员:' + value.BmsCenSaleDoc_UserName;
		var OperDate = '操作时间:' + value.BmsCenSaleDoc_OperDate;

		if(TYPE == 1) {
			UserName = '验收人:' + value.BmsCenSaleDoc_AccepterName;
			OperDate = '验收时间:' + value.BmsCenSaleDoc_AccepterTime;
		}

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
			if(TYPE) TYPE = parseInt(TYPE);
			var id = $(this).attr("data");
			//待验收:2,已验收:1
			switch(TYPE) {
				case 2:
					showInfo(id);
					break;
				case 1:
					showDtInfo(id);
					break;
				default:
					break;
			}

		});
	}
	//供货单待验收处理
	function showInfo(id) {
		location.href = 'info.html?id=' + id + "&account=" + ACCOUNT + "&password=" + PASSWORD + "&secAccepterType=" + SECACCEPTERTYPE + "&t=" + new Date().getTime();
	}
	//供货单明细显示
	function showDtInfo(id) {
		location.href = 'dtinfo.html?id=' + id + "&account=" + ACCOUNT + "&password=" + PASSWORD + "&t=" + new Date().getTime();
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
			if(!data.value || !data.value.list || data.value.list.length == 0) {
				PAGE--;
			}
		});
	}
	//提取供货单成功后列表数据刷新处理
	function extractRefreshData() {
		refreshContent();
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
		//tab页签
		$("#type-ul li").on("click", function() {

			if(CHECKED_TYPE) {
				CHECKED_TYPE.removeClass("active");
			}
			CHECKED_TYPE = $(this);
			TYPE = CHECKED_TYPE.attr("data");
			if(TYPE) TYPE = parseInt(TYPE);
			switch(TYPE) {
				case 1: //验收					
					$("#ContentDiv").css("margin-top", "8px");
					//$("#divSearch").css("margin-top:100px;");
					$("#divSearch").show();
					break;
				default:
					$("#divSearch").hide();
					$("#textSearch").val("");
					$("#ContentDiv").css("margin-top", "100px");
					break;
			}
			CHECKED_TYPE.addClass("active");
			refreshContent();
		});
		if(Shell.util.Event.click != "click") {
			$("#type-ul li").on(Shell.util.Event.click, function() {
				$(this).click();
			});
		}
		//验收查询输入栏处理
		$('#textSearch').keydown(function(e) {
			if(!Shell.util.Event.isClick()) return;
			if(e.keyCode == 13) {
				var barcode = $('#textSearch').val();
				if(!barcode) return;
				refreshContent();
			}
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