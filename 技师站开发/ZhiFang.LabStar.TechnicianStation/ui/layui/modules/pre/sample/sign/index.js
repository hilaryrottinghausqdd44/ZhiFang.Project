/**
 * @name：modules/pre/sample/sign/index 样本签收
 * @author：zhangda
 * @version 2020-08-27
 */
layui.extend({
	uxutil: 'ux/util',
	querybar: 'modules/pre/sample/sign/content/querybar',//查询栏
	packlist: 'modules/pre/sample/sign/content/packlist',//打包列表
	list: 'modules/pre/sample/sign/content/list',//条码号列表
	infolist: 'modules/pre/sample/sign/content/infolist',//信息
	signfaillist: 'modules/pre/sample/sign/content/signfaillist',//签收失败列表
	signsuccesslist: 'modules/pre/sample/sign/content/signsuccesslist',//签收成功列表
	CommonSelectSickType: 'modules/common/select/sicktype',//就诊类型
	CommonSelectUser: 'modules/common/select/preuser',//送达人
}).define(['uxutil', 'table', 'form', 'dropdown', 'querybar', 'packlist', 'list', 'infolist', 'signfaillist', 'signsuccesslist', 'CommonSelectSickType', 'CommonSelectUser', 'PreSampleSignParams'], function (exports) {
	"use strict";

	var $ = layui.$,
		form = layui.form,
		dropdown = layui.dropdown,
		uxutil = layui.uxutil,
		table = layui.table,
		querybar = layui.querybar,
		packlist = layui.packlist,
		list = layui.list,
		infolist = layui.infolist,
		signfaillist = layui.signfaillist,
		signsuccesslist = layui.signsuccesslist,
		CommonSelectSickType = layui.CommonSelectSickType,
		CommonSelectUser = layui.CommonSelectUser,
		//PreBasicHostType = layui.PreBasicHostType,
		PreSampleSignParams = layui.PreSampleSignParams,
		MOD_NAME = 'PreSampleSignIndex';

	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" lay-filter="{domId}-form">',
		'<div class="layui-form-item" style="margin-bottom:0;">',
		'<div class="layui-input-inline">',
		'<input type="text" name="{domId}-barcode" id="{domId}-barcode" placeholder="条码号" autocomplete="off" class="layui-input">',
		'</div>',
		'<div class="layui-input-inline" style="display:none;">',
		'<select name="{domId}-sicktypeno" id="{domId}-sicktypeno" lay-filter="{domId}-sicktypeno" lay-search="">',
		'<option value="">就诊类型</option>',
		'</select>',
		'</div>',
		'<div class="layui-input-inline" style="display:none;">',
		'<select name="{domId}-deliverer" id="{domId}-deliverer" lay-filter="{domId}-deliverer" lay-search="">',
		'<option value="">送达人</option>',
		'</select>',
		'</div>',
		'<div class="layui-input-inline" style="display:none;">',
		'<input type="checkbox" name="{domId}-query-show" id="{domId}-query-show" lay-filter="{domId}-query-show" title="查询" lay-skin="primary" />',
		'</div>',
		'</div>',
		'</div>',
		'<div class="layui-form {domId}-grid-div" id="{domId}-query-form" lay-filter="{domId}-query-form" style="display:none;">',
		'<div class="layui-form-item" style="margin-bottom:0;">',
		'<div class="layui-row">',
		'<div class="layui-col-xs9">',
		'<div id="QueryBar"></div>',
		'</div>',
		'<div class="layui-col-xs3">',
		'<div class="layui-input-inline">',
		'<button type="button" id="{domId}-query-btn" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询</button>',
		'</div>',
		'</div>',
		'</div>',
		'</div>',
		'</div>',
		'<div class="layui-row layui-col-space10 layui-form" lay-filter="{domId}-form-cards" style="background-color:#f2f2f2;padding:0;margin:0;">',
		'<div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12">',
		'<div id="{domId}-table"></div>',
		'</div>',
		'</div>',
		'<style>',
		'.{domId}-grid-div{padding:2px;margin-bottom:2px;border-bottom:1px solid #e6e6e6;background-color:#f2f2f2;}',
		//			'.layui-form-onswitch-red{border-color:#FF5722;background-color:#FF5722;}',
		'</style>'
	].join('');
	//验证身份
	var IDENYITY_DOM = [
		'<div class="layui-form" style="padding:15px;">',
		'<div class="layui-form-item">',
		'<input type="text" name="account" id="account" lay-filter="account" lay-verify="required" placeholder="用户名" autocomplete="off" class="layui-input"/>',
		'</div>',
		'<div class="layui-form-item">',
		'<input type="text" name="password" id="password" lay-filter="password" lay-verify="required" placeholder="密码" autocomplete="off" class="layui-input"/>',
		'</div>',
		'<div class="layui-form-item" style="text-align:right;margin-top:10px;">',
		'<button type="button" id="confirm" class="layui-btn layui-btn-xs layui-btn-normal" lay-filter="idenyity" lay-submit><i class="layui-icon layui-icon-ok"></i>确定</button>',
		'</div>',
		'</div>'
	].join('');

	//根据条码号签收并查询数据服务路径
	var AUTO_SIGN_BY_BARCODE_GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSignForSampleByBarCode';
	//根据条码号查询数据服务路径
	var GET_LIST_BY_BARCODE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode';
	//根据条码号查询该条码号所属打包号下条码数据服务路径
	var GET_PACK_LIST_BY_BARCODE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode';
	//根据where条件查询
	var GET_LIST_BY_WHERE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere';
	//模式2通过打包号自动签收或获取列表
	var GET_LIST_BY_PACKNO_OR_BARCODE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForOrGetBarCodeFormByPackNoOrBarCode';
	//登录服务
	var LOGIN_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//取消签收服务
	var CANCEL_SIGN_BY_BARCODE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreCancelSampleSignForOrByBarCode';
	//获得取单凭证服务
	var GET_VOUCHERRETRIEVAL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetNeedPrintVoucherBarCodeFormListByBarCodeAndPara';

	//打印模板_业务类型
	var BusinessType = 3;
	//打印模板_模板类型
	var ModelType = 8;
	//打印模板_模板类型
	var ModelTypeName = "样本签收_样本清单";

	//所有列表数据
	var LIST_DATA = [];

	//功能参数实例
	var PreSampleSignParamsInstance = null;

	//打包列表实例
	var PacklistInstance = null;
	//打包列表选中列
	var PacklistCheckRowData = [];
	//签收列表实例
	var SignlistInstance = null;
	//签收列表选中列
	var SignlistCheckRowData = [];
	//信息列表实例
	var InfolistInstance = null;
	//签收失败信息列表实例
	var SignfaillistInstance = null;
	//签收成功信息列表实例
	var SignsuccesslistInstance = null;
	//查询栏实例
	var QueryBarInstance = null;

	//身份验证失效时间 -毫秒数  -- 存储到local中的名称
	var Sign_Local_VerifiyInvalidDate_Name = 'PreSampleSignIndex_Local_VerifiyInvalidDate';

	//标签打印机名
	//var LabelPrinterName = PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0037');

	//就诊类型local存储名称
	var HistorySickTypeLocalName = "PreSampleSignSickTypeValue";

	/** --模式--
	 * 1.扫条码并自动签收
	 * 2.打包号核收或者查询获取数据，显示在列表，然后在条码号框扫条码，匹配时签收并且从列表中剔除该条码信息
	 * 3.单个扫描，最后一起签收（需要签收人身份验证）
	 * 4.查询出未签收已送检的条码信息，选择需要签收的条码签收
	 *
	 * **/
	//现有模式
	var MODELS = [1, 2, 3, 4];
	//门诊样本条码
	var PreSampleSignIndex = {
		//对外参数
		config: {
			domId: null,
			modelType: null,
			nodetype: null
		}
	};
	//构造器
	var Class = function (setings) {
		var me = this;
		me.config = $.extend({}, me.config, PreSampleSignIndex.config, setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function () {
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g, me.config.domId);

		$('#' + me.config.domId).append(html);
		//模式处理
		me.modelHandle();
		//是否显示查询功能
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0039') == 1) {
			//显示就诊类型
			$("#" + me.config.domId + "-query-show").parent().show();
			//实例化查询栏
			QueryBarInstance = querybar.render({ domId: "QueryBar", PreSampleSignParamsInstance: PreSampleSignParamsInstance });
		}
		//初始化主体内容
		me.loadContentDom();
		//是否显示核收就诊类型
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0040') == 1) {
			//显示就诊类型
			$("#" + me.config.domId + "-sicktypeno").parent().show();
			//初始化就诊类型下拉框
			CommonSelectSickType.render({ domId: me.config.domId + "-sicktypeno", defaultName: '就诊类型', done: function () { } });
			//就诊类型默认取历史记录值
			if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0041') == 1) {
				var sicktypeno = uxutil.localStorage.get(HistorySickTypeLocalName, true);
				if (sicktypeno) $("#" + me.config.domId + "-sicktypeno").val(sicktypeno);
			}
		}
		//是否显示送达人下拉框
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0019') == 1) {
			//显示就诊类型
			$("#" + me.config.domId + "-deliverer").parent().show();
			//初始化送达人下拉框
			CommonSelectUser.render({ domId: me.config.domId + "-deliverer", defaultName: '送达人', code: [1001002, 1001004], done: function () { } });
		}

		form.render();
	};
	//传入模式处理 --不存在这个模式则默认为模式1
	Class.prototype.modelHandle = function () {
		var me = this,
			isExist = false,
			modelType = me.config.modelType;
		//查询是否支持该模式
		$.each(MODELS, function (i, item) {
			if (item == modelType) {
				isExist = true;
				return false;
			}
		});
		if (!isExist) me.config.modelType = 1;//默认为模式1
	};
	//根据模式加载栅格dom元素
	Class.prototype.loadDomByModel = function (modelType, contentDomID) {
		var me = this,
			modelType = modelType || me.config.modelType,
			leftColNum = 3, middleColNum = 6, rightColNum = 3,
			leftContainerID = 'leftContainer', middleContainerID = 'middleContainer', rightContainerID = 'rightContainer',
			html = [];
		if (!contentDomID) return;
		if (modelType != 2) middleColNum += leftColNum;//模式2存在打包列表(最左侧列表)
		if (modelType == 2) html.push('<div id="' + leftContainerID + '" class="layui-col-xs6 layui-col-sm' + leftColNum + '"></div>');
		html.push('<div id="' + middleContainerID + '" class="layui-col-xs6 layui-col-sm' + middleColNum + '"></div>');
		html.push('<div id="' + rightContainerID + '" class="layui-col-xs6 layui-col-sm' + rightColNum + '"></div>');
		$("#" + contentDomID).html('<div class="layui-row">' + html.join('') + '</div>');
		return { leftContainerID: leftContainerID, middleContainerID: middleContainerID, rightContainerID: rightContainerID };
	};
	//加载主体内容Dom
	Class.prototype.loadContentDom = function () {
		var me = this,
			modelType = me.config.modelType,
			contentDomID = me.config.domId + '-table',
			ContainerIDList = me.loadDomByModel(modelType, contentDomID);//根据模式加载内容主体部分栅格dom元素 并返回每个容器ID
		//实例化打包列表
		if (modelType == 2)//模式2存在打包列表
			PacklistInstance = packlist.render({
				domId: ContainerIDList["leftContainerID"],
				height: 'full-140',
				modeltype: modelType
			});
		//实例化签收列表
		SignlistInstance = list.render({
			domId: ContainerIDList["middleContainerID"],
			height: 'full-140',
			modeltype: modelType,
			colsStr: PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0058'),
			sortStr: PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0035')
		});
		//实例化信息
		InfolistInstance = infolist.render({
			domId: ContainerIDList["rightContainerID"],
			height: 'full-368',
			modeltype: modelType,
		});
		//实例化签收失败列表
		SignfaillistInstance = signfaillist.render({
			domId: ContainerIDList["rightContainerID"],
			height: '120',
			modeltype: modelType,
		});
		//实例化签收成功列表
		SignsuccesslistInstance = signsuccesslist.render({
			domId: ContainerIDList["rightContainerID"],
			height: '120',
			modeltype: modelType,
		});
		//模式四直接调用服务查询数据
		if (modelType == 4) {
			//未签收已送检
			var where = 'BarCodeStatusID >= 5 and BarCodeStatusID < 7';
			//根据where查询出数据
			me.onSearchByWhere(where, function (list) {
				//记录数据
				$.each(list, function (i, item) {
					LIST_DATA.push(item);
				});
				//数据加入列表
				me.onSignlistChange(LIST_DATA);
			});
		}
	};

	//监听事件
	Class.prototype.initListeners = function () {
		var me = this,
			modelType = me.config.modelType;
		//监听回车按下事件
		$("#" + me.config.domId).keydown(function (event) {
			var inputID = me.config.domId + "-barcode";
			switch (event.keyCode) {
				case 13:
					//判断焦点是否在该输入框
					if (document.activeElement == document.getElementById(inputID)) {
						var value = $("#" + inputID).val();
						me.onSearch(value);
					}
			}
		});
		//触发行单击事件 -- 打包列表单击事件
		if (modelType == 2) //打包列表
			PacklistInstance.uxtable.table.on('row(' + PacklistInstance.tableId + ')', function (obj) {
				obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
				PacklistCheckRowData = [];
				PacklistCheckRowData.push(obj.data);
				
				me.onSignlistChange(me.getListByPackNo(obj.data["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"], obj.data["barcode"]));//更新条码号签收列表
			});
		//条码号签收列表单击
		SignlistInstance.uxtable.table.on('row(' + SignlistInstance.tableId + ')', function (obj) {
			obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			SignlistCheckRowData = [];
			SignlistCheckRowData.push(obj.data);
			$("#" + SignlistInstance.tableId + "-div").html(obj.data["ItemName"]);

			me.onInfolistChange(SignlistCheckRowData[0]);//更新信息列表
			me.onSignSuccesslistChange(SignlistCheckRowData);//更新签收失败列表
			//me.onSignfaillistChange(SignlistCheckRowData);//更新签收成功列表
		});
		//主列表触发排序事件
		SignlistInstance.uxtable.table.on('sort(' + SignlistInstance.tableId + ')', function (obj) {
			//console.log(obj.field); //当前排序的字段名
			//console.log(obj.type); //当前排序类型：desc（降序）、asc（升序）、null（空对象，默认排序）
			//前台排序--因为当前没分页 并且只是时间排序
		});
		//查询复选监听
		form.on('checkbox(' + me.config.domId + "-query-show" + ')', function (data) {
			var QueryBarID = me.config.domId + "-query-form";
			if (data.elem.checked)
				$("#" + QueryBarID).show();
			else
				$("#" + QueryBarID).hide();
		});
		//监听就诊类型选择
		form.on('select(' + me.config.domId + "-sicktypeno" +')', function (data) {
			var value = data.value;
			uxutil.localStorage.set(HistorySickTypeLocalName, JSON.stringify(value));
		});
		//查询按钮处理
		$("#" + me.config.domId + "-query-btn").on('click', function () {
			var entity = QueryBarInstance.getWhere();
			entity.nodetypeID = me.config.nodetype;
			entity.isPlanish = true;
			entity.fields = SignlistInstance.getStoreFields(true).join() + "," + SignsuccesslistInstance.getStoreFields(true).join() + "," + SignfaillistInstance.getStoreFields(true).join() + (modelType == 2 ? ("," + PacklistInstance.getStoreFields(true).join()) : "");
			entity.sortFields = null;
			me.onSearchBtnClickGetList(entity);
		});

		//身份验证确认按钮
		form.on('submit(idenyity)', function (data) {
			var account = data.field["account"],
				password = data.field["password"];
			layer.load();
			//请求登入接口
			uxutil.server.ajax({
				url: LOGIN_URL,
				cache: false,
				data: {
					strUserAccount: account,
					strPassWord: password
				}
			}, function (data) {
				layer.closeAll('loading');

				if (data === true) {
					if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0011')) {
						//分钟转为毫秒+当前毫秒数 -- 存储到local
						var VerifiyInvalidDate = uxutil.server.date.getTimes() + PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0011') * 60 * 1000;
						uxutil.localStorage.set(Sign_Local_VerifiyInvalidDate_Name, JSON.stringify(VerifiyInvalidDate));
						
					}
					me.onSignClick(true);
					layer.closeAll();
				} else {
					layer.msg('账号或密码错误！', { icon: 5, anim: 0 });
				}
			});
		});
	};
	//条码号获得焦点
	Class.prototype.setFocus = function () {
		var me = this;
		setTimeout(function () {
			$("#" + me.config.domId + "-barcode").focus();
		}, 10);
	};
	//清空条码号并获得焦点
	Class.prototype.onClearBarCode = function () {
		var me = this;
		$("#" + me.config.domId + "-barcode").val("");
		me.setFocus();
	};
	//是否是条码号
	Class.prototype.isBarcode = function (value) {
		var me = this,
			value = String(value).trim() || null,
			isPackNoSearch = PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0017');//是否使用打包号查询  格式 1(0)|P 0:不使用，1:使用；P:打包号标识符；中间用|隔开

		if (isPackNoSearch != "" && isPackNoSearch.indexOf("|") != -1 && isPackNoSearch.split("|")[0] == 1) {
			if (value.indexOf(isPackNoSearch.split("|")[1]) != -1)
				return false;
		}

		return true;
	};
	//查询处理
	Class.prototype.onSearch = function (value) {
		var me = this,
			value = value || null;

		//根据条码号或者打包号查询数据
		me.getListByValue(value);

	};
	//根据条码号或者打包号查询数据
	Class.prototype.getListByValue = function (value) {
		var me = this,
			value = value || null;

		if (!value) {
			layer.msg("请输入条码号!", { icon: 0, anim: 0 });
			return;
		}
		me.onSearchHandleByModel(value);
	};
	//根据模式分别做查询处理
	Class.prototype.onSearchHandleByModel = function (value) {
		var me = this,
			modelType = me.config.modelType,
			value = value || null,
			Msg = [],//提示信息
			isExist = me.onBarCodeIsExist(value),//该条码号是否已存在
			errorlistTableCache = SignfaillistInstance.uxtable.table.cache[SignfaillistInstance.tableId];

		switch (String(modelType)) {
			case "1":
				if (isExist) {
					layer.msg("该条码号已存在!", { icon: 0, anim: 0 });
					return;
				}
				//根据条码号自动签收
				me.onAutoSignByBarCode(value, true, false, function (list) {
					//提示信息处理
					me.onFailureInfoHandle(list, function (resultList) {
						var PrintList = [],//打印清单数据
							VoucherRetrievalList = [];//取单凭证
						//根据参数判断是否清空列表
						if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0036') != 1) LIST_DATA = [];
						
						$.each(resultList, function (i, item) {
							var data = item["data"],
								tipList = item["tipList"];
							if (item["isSuccess"]) {
								LIST_DATA.push(item["data"]);
								//自动签收是否自动打印签收单
								if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0013') == 1) PrintList.push(data);
								//签收后是否自动打印取单凭证
								if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0031') == 1) VoucherRetrievalList.push(data);
							} else {
								//签收失败是否显示样本信息
								if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0015') == 1) LIST_DATA.push(data);
								//失败信息加入失败列表
								$.each(tipList, function (j, itemJ) {
									errorlistTableCache.unshift({ LisBarCodeFormVo_LisBarCodeForm_BarCode: data["LisBarCodeFormVo_LisBarCodeForm_BarCode"], LisBarCodeFormVo_failureInfo: itemJ });
								});
							}
							//弹出信息处理
							if (tipList.length > 0) {
								$.each(tipList, function (k, itemK) {
									Msg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
								});
							}
						});
						if (Msg.length > 0)
							layer.alert(Msg.join('<br>'), { icon: 0, anim: 0 });
						//数据加入列表
						me.onSignlistChange(LIST_DATA);
						//失败数据更新
						me.onSignfaillistChange(errorlistTableCache);
						//清空条码号
						me.onClearBarCode();
						//打印签收单
						if (PrintList.length > 0) me.onPrintList(PrintList);
						//打印取单凭证
						if (VoucherRetrievalList.length > 0) me.onPrintVoucherRetrieval(VoucherRetrievalList);
					});
				});
				break;
			case "2":
				var isBarcode = me.isBarcode(value),
					isClearBarcode = true,
					isPackNoAutoSign = PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0018');//是否按打包号自动签收
				if (isBarcode && isExist) {//是条码号 并且存在 进行签收动作
					//条码号存在 执行更新 剔除列表中  --根据条码号自动签收
					me.onAutoSignByBarCode(value, true, false, function (list) {
						//提示信息处理
						me.onFailureInfoHandle(list, function (resultList) {
							var VoucherRetrievalList = [];//取单凭证

							$.each(resultList, function (i, item) {
								var data = item["data"],
									tipList = item["tipList"];
								//成功后剔除
								$.each(LIST_DATA, function (a, itemA) {
									if (data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
										if (item["isSuccess"]) {
											LIST_DATA.splice(a, 1);
											//签收后是否自动打印取单凭证
											if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0031') == 1) VoucherRetrievalList.push(data);
										} else {
											isClearBarcode = false;
											//签收失败是否显示样本信息
											if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0015') == 1) {
												LIST_DATA[a] = data;
												LIST_DATA[a]["LAY_CHECKED"] = true;
											} else {
												LIST_DATA.splice(a, 1);
											}
											//失败信息加入失败列表
											$.each(tipList, function (j, itemJ) {
												errorlistTableCache.unshift({ LisBarCodeFormVo_LisBarCodeForm_BarCode: data["LisBarCodeFormVo_LisBarCodeForm_BarCode"], LisBarCodeFormVo_failureInfo: itemJ });
											});
										}
										return false;
									}
								});
								//弹出信息处理
								if (tipList.length > 0) {
									$.each(tipList, function (k, itemK) {
										Msg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
									});
								}
							});
							if (Msg.length > 0)
								layer.alert(Msg.join('<br>'), { icon: 0, anim: 0 });
							//数据加入列表
							me.onSignlistChange(LIST_DATA);
							//无数据清空打包列表
							if (LIST_DATA.length == 0) me.onPacklistChange([]);
							//失败数据更新
							me.onSignfaillistChange(errorlistTableCache);
							//清空条码号
							if (isClearBarcode) me.onClearBarCode();
							//打印取单凭证
							if (VoucherRetrievalList.length > 0) me.onPrintVoucherRetrieval(VoucherRetrievalList);
						});
					});
				} else if (isBarcode && !isExist) {//是条码号 不存在 进行查询
					me.onSearchPackListByBarCode(value, function (list) {
						//先清空列表
						LIST_DATA = list;
						//数据加入列表
						me.onPacklistChange(me.getPackNoList(LIST_DATA));
						//清空条码号
						me.onClearBarCode();
					});
				} else {//打包号
					me.onSearchByPackNoOrBarcode(value, function (list) {
						//先清空列表
						LIST_DATA = [];
						//是打包号 并且自动签收
						if (!isBarcode && isPackNoAutoSign == 1) {
							//提示信息处理
							me.onFailureInfoHandle(list, function (resultList) {
								var PrintList = [],//打印清单数据
									VoucherRetrievalList = [];//取单凭证

								$.each(resultList, function (i, item) {
									var data = item["data"],
										tipList = item["tipList"];
									if (item["isSuccess"]) {
										LIST_DATA.push(item["data"]);
										//自动签收是否自动打印签收单
										if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0013') == 1) PrintList.push(data);
										//签收后是否自动打印取单凭证
										if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0031') == 1) VoucherRetrievalList.push(data);
									} else {
										isClearBarcode = false;
										//签收失败是否显示样本信息
										if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0015') == 1) LIST_DATA.push(data);
										//失败信息加入失败列表
										$.each(tipList, function (j, itemJ) {
											errorlistTableCache.unshift({ LisBarCodeFormVo_LisBarCodeForm_BarCode: data["LisBarCodeFormVo_LisBarCodeForm_BarCode"], LisBarCodeFormVo_failureInfo: itemJ });
										});
									}
									//弹出信息处理
									if (tipList.length > 0) {
										$.each(tipList, function (k, itemK) {
											Msg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
										});
									}
								});
								if (Msg.length > 0)
									layer.alert(Msg.join('<br>'), { icon: 0, anim: 0 });
								//数据加入列表
								me.onPacklistChange(me.getPackNoList(LIST_DATA));
								//失败数据更新
								me.onSignfaillistChange(errorlistTableCache);
								//清空条码号
								if (isClearBarcode) me.onClearBarCode();
								//打印签收单
								if (PrintList.length > 0) me.onPrintList(PrintList);
								//打印取单凭证
								if (VoucherRetrievalList.length > 0) me.onPrintVoucherRetrieval(VoucherRetrievalList);
							});
						} else {
							LIST_DATA = list;
							//数据加入列表
							me.onPacklistChange(me.getPackNoList(LIST_DATA));
							//失败数据更新
							me.onSignfaillistChange(errorlistTableCache);
							//清空条码号
							if (isClearBarcode) me.onClearBarCode();
						}
					});
				}
				break;
			case "3":
				if (isExist) {
					layer.msg("该条码号已存在!", { icon: 0, anim: 0 });
					return;
				}
				//根据条码号查询出数据
				me.onSearchByBarCode(value, function (list) {
					//记录数据
					$.each(list, function (i, item) {
						LIST_DATA.push(item);
					});
					//数据加入列表
					me.onSignlistChange(LIST_DATA);
					//清空条码号
					me.onClearBarCode();
				});
				break;
			case "4":
				if (isExist) {
					layer.msg("该条码号已存在!", { icon: 0, anim: 0 });
					return;
				}
				//根据条码号查询出数据
				me.onSearchByBarCode(value, function (list) {
					//记录数据
					$.each(list, function (i, item) {
						LIST_DATA.push(item);
					});
					//数据加入列表
					me.onSignlistChange(LIST_DATA);
					//清空条码号
					me.onClearBarCode();
				});
				break;
			default:
				break;
		}
	};

	//根据条码号自动签收  isAutoSignFor:自动签收；isForceSignFor：强制签收
	Class.prototype.onAutoSignByBarCode = function (values, isAutoSignFor, isForceSignFor, callback) {
		var me = this,
			modelType = me.config.modelType,
			value = value,
			nodetype = me.config.nodetype,//站点类型
			isAutoSignFor = isAutoSignFor || false,
			isForceSignFor = isForceSignFor || false,
			fields = SignlistInstance.getStoreFields(true).join() + "," + SignsuccesslistInstance.getStoreFields(true).join() + "," + SignfaillistInstance.getStoreFields(true).join() + (modelType == 2 ? ("," + PacklistInstance.getStoreFields(true).join()) : ""),
			load = layer.load(),
			entity = { nodetypeID: nodetype, barCodes: values, sickType: null, deliverierID: null, deliverier: null, fields: fields, isPlanish: true, isAutoSignFor: isAutoSignFor, isForceSignFor: isForceSignFor };

		//就诊类型
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-sicktypeno").val()) {
			entity.sickType = $("#" + me.config.domId + "-sicktypeno").val();
		}
		//送达人
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-deliverer").val()) {
			entity.deliverierID = $("#" + me.config.domId + "-deliverer").val();
			entity.deliverier = $("#" + me.config.domId + "-deliverer option:selected").text();
		}

		var config = { type: "POST", url: AUTO_SIGN_BY_BARCODE_GET_LIST_URL, data: JSON.stringify(entity) };

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})

	};
	//根据条码号查询出数据
	Class.prototype.onSearchByBarCode = function (values, callback) {
		var me = this,
			modelType = me.config.modelType,
			values = values,
			nodetype = me.config.nodetype,//站点类型
			fields = SignlistInstance.getStoreFields(true).join() + "," + SignsuccesslistInstance.getStoreFields(true).join() + "," + SignfaillistInstance.getStoreFields(true).join() + (modelType == 2 ? ("," + PacklistInstance.getStoreFields(true).join()) : ""),
			load = layer.load(),
			entity = { nodetypeID: nodetype, barCode: values, sickType: null, fields: fields, isPlanish: true };

		//就诊类型
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-sicktypeno").val()) {
			entity.sickType = $("#" + me.config.domId + "-sicktypeno").val();
		}

		var config = { type: "POST", url: GET_LIST_BY_BARCODE_URL, data: JSON.stringify(entity) };

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})

	};
	//根据条码号查询该条码号所属打包号下条码数据服务路径
	Class.prototype.onSearchPackListByBarCode = function (value, callback) {
		var me = this,
			modelType = me.config.modelType,
			value = value,
			nodetype = me.config.nodetype,//站点类型
			fields = SignlistInstance.getStoreFields(true).join() + "," + SignsuccesslistInstance.getStoreFields(true).join() + "," + SignfaillistInstance.getStoreFields(true).join() + (modelType == 2 ? ("," + PacklistInstance.getStoreFields(true).join()) : ""),
			load = layer.load(),
			entity = { nodetypeID: nodetype, barCode: value, sickType: null, fields: fields, isPlanish: true };

		//就诊类型
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-sicktypeno").val()) {
			entity.sickType = $("#" + me.config.domId + "-sicktypeno").val();
		}

		var config = { type: "POST", url: GET_PACK_LIST_BY_BARCODE_URL, data: JSON.stringify(entity) };

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})

	};
	//根据where条件查询
	Class.prototype.onSearchByWhere = function (where, callback) {
		var me = this,
			modelType = me.config.modelType,
			value = value,
			nodetype = me.config.nodetype,//站点类型
			where = where || null,
			fields = SignlistInstance.getStoreFields(true).join() + "," + SignsuccesslistInstance.getStoreFields(true).join() + "," + SignfaillistInstance.getStoreFields(true).join() + (modelType == 2 ? ("," + PacklistInstance.getStoreFields(true).join()) : ""),
			load = layer.load(),
			entity = { nodetypeID: nodetype, where: where, fields: fields, isPlanish: true };

		var config = { type: "POST", url: GET_LIST_BY_WHERE_URL, data: JSON.stringify(entity) };

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})

	};
	//模式2通过打包号自动签收或获取列表
	Class.prototype.onSearchByPackNoOrBarcode = function (values, callback) {
		var me = this,
			value = value,
			nodetype = me.config.nodetype,//站点类型
			fields = SignlistInstance.getStoreFields(true).join() + "," + SignsuccesslistInstance.getStoreFields(true).join() + "," + SignfaillistInstance.getStoreFields(true).join() + "," + PacklistInstance.getStoreFields(true).join(),
			load = layer.load(),
			entity = { nodetypeID: nodetype, barCodeOrPackNo: values, sickType: null, deliverierID: null, deliverier: null, fields: fields, isPlanish: true };

		//就诊类型
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-sicktypeno").val()) {
			entity.sickType = $("#" + me.config.domId + "-sicktypeno").val();
		}
		//送达人
		if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-deliverer").val()) {
			entity.deliverierID = $("#" + me.config.domId + "-deliverer").val();
			entity.deliverier = $("#" + me.config.domId + "-deliverer option:selected").text();
		}

		var config = { type: "POST", url: GET_LIST_BY_PACKNO_OR_BARCODE_URL, data: JSON.stringify(entity) };

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})

	};
	//取消签收
	Class.prototype.onCancelSignByBarCode = function (barcodes) {
		var me = this,
			modelType = me.config.modelType,
			barcodes = barcodes,
			entity = {
				nodetypeID: me.config.nodetype,
				BarCodeList: barcodes,
				isPlanish: true,
				fields: SignlistInstance.getStoreFields(true).join() + "," + SignsuccesslistInstance.getStoreFields(true).join() + "," + SignfaillistInstance.getStoreFields(true).join() + (modelType == 2 ? ("," + PacklistInstance.getStoreFields(true).join()) : "")
			},
			config = { type: "POST", url: CANCEL_SIGN_BY_BARCODE_URL, data: JSON.stringify(entity) },
			load = layer.load();

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					$.each(res.value.list, function (i, item) {
						$.each(LIST_DATA, function (a, itemA) {
							if (item["LisBarCodeFormVo_LisBarCodeForm_BarCode"] == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
								LIST_DATA[a] = data;
								LIST_DATA[a]["LAY_CHECKED"] = true;
								return false;
							}
						});
					});
					layer.msg("取消签收操作成功!", { icon: 6, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "取消签收操作失败!", { icon: 5, anim: 0 });
			}
		})
	};
	//查询按钮点击获取数据
	Class.prototype.onSearchBtnClickGetList = function (entity) {
		var me = this,
			modelType = me.config.modelType,
			entity = entity,
			config = { type: "POST", url: GET_LIST_BY_WHERE_URL, data: JSON.stringify(entity) },
			load = layer.load();

		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				LIST_DATA = [];//先清空
				if (res.value && res.value.list && res.value.list.length > 0) {
					LIST_DATA = res.value.list;
				}
				if (modelType == 2)
					me.onPacklistChange(me.getPackNoList(LIST_DATA));
				else
					me.onSignlistChange(LIST_DATA);
			} else {
				layer.msg(res.ErrorInfo || "查询失败!", { icon: 5, anim: 0 });
			}
		})
	};
	
	//签收成功信息处理
	Class.prototype.onSuccessInfoHandle = function (list, callback) {
		var me = this,
			successList = [];
		$.each(list, function (i, item) {
			if (item["LisBarCodeFormVo_SignForMan"] || item["LisBarCodeFormVo_LisBarCodeForm_InceptTime"])
				successList.push({ LisBarCodeFormVo_SignForMan: item["LisBarCodeFormVo_SignForMan"], LisBarCodeFormVo_LisBarCodeForm_InceptTime: item["LisBarCodeFormVo_LisBarCodeForm_InceptTime"] });
		});
		callback && callback(successList);
	};
	//更新打包列表
	Class.prototype.onPacklistChange = function (list) {
		var me = this,
			list = list || [];
		PacklistInstance.onSearch(list);
	};
	//更新条码号签收列表
	Class.prototype.onSignlistChange = function (list) {
		var me = this,
			list = list || [];
		SignlistInstance.onSearch(list);
	};
	//更新信息列表
	Class.prototype.onInfolistChange = function (obj) {
		var me = this,
			obj = obj || null,
			list = [],
			fields = {
				"LisBarCodeFormVo_LisBarCodeForm_LisPatient_Bed": "床位",
				"LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName": "姓名",
				"LisBarCodeFormVo_LisBarCodeForm_LisPatient_PatNo": "病历号",
				"LisBarCodeFormVo_LisBarCodeForm_BarCode": "条码号",
				"LisBarCodeFormVo_LisBarCodeForm_LisPatient_SickType": "就诊类型",
				"LisBarCodeFormVo_SampleTypeName": "样本类型",
				"LisBarCodeFormVo_SamplingGroupName": "采样组",
				"LisBarCodeFormVo_LisBarCodeForm_LisOrderForm_OrderFormNo": "原始单号",
				"LisBarCodeFormVo_LisBarCodeForm_ReceiveFlag": "核收标志",
			};
		if (obj) {
			for (var i in fields) {
				list.push({ "property": fields[i],"value":(obj[i] || "") });
			}
		}
		InfolistInstance.onSearch(list);
	};
	//更新签收失败列表
	Class.prototype.onSignfaillistChange = function (list) {
		var me = this,
			list = list || [];
		SignfaillistInstance.onSearch(list);
	};
	//更新签收成功列表
	Class.prototype.onSignSuccesslistChange = function (list) {
		var me = this,
			list = list || [];
		me.onSuccessInfoHandle(list, function (data) { SignsuccesslistInstance.onSearch(data); });
	};
	//是否该条码号已经存在列表
	Class.prototype.onBarCodeIsExist = function (value) {
		var me = this,
			isExist = false;
			value = value || null;

		$.each(LIST_DATA, function (i,item) {
			if (value == item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
				isExist = true;
				return false;
			}
		});
		return isExist;
	};
	//模式2获得打包号列表
	Class.prototype.getPackNoList = function (list) {
		var me = this,
			list = list || [],
			packlist = [];

		$.each(list, function (a, itemA) {
			var isExist = false;
			$.each(packlist, function (b, itemB) {
				if (itemA["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"] && itemA["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"] == itemB["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"]) {
					isExist = true;
					return false;
				}
			});
			if (!isExist)
				packlist.push({
					LisBarCodeFormVo_LisBarCodeForm_CollectPackNo: itemA["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"],
					barcode: itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"],
					LisBarCodeFormVo_count: itemA["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"] ? itemA["LisBarCodeFormVo_count"] : 1,
					LisBarCodeFormVo_hasSignForCount: itemA["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"] ? itemA["LisBarCodeFormVo_hasSignForCount"] : (itemA["LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID"] == 7 ? 1 : 0),
					LisBarCodeFormVo_notSignForCount: itemA["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"] ? itemA["LisBarCodeFormVo_notSignForCount"] : (itemA["LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID"] == 9 ? 1 : 0)
				});
		});

		if (packlist.length == 0) me.onSignlistChange(list);
		return packlist;
	};
	//模式2根据打包号获得签收列表数据
	Class.prototype.getListByPackNo = function (PackNo,BarCode) {
		var me = this,
			list = list || [],
			PackNo = PackNo || "",
			BarCode = BarCode || "";

		$.each(LIST_DATA, function (a, itemA) {
			if (PackNo) {
				if (PackNo == itemA["LisBarCodeFormVo_LisBarCodeForm_CollectPackNo"]) {
					list.push(itemA);
					return true;
				}
			} else {
				if (BarCode == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
					list.push(itemA);
					return false;
				}
			}
			
		});

		return list;
	};

	//清空按钮
	Class.prototype.onClearClick = function () {
		var me = this,
			modelType = me.config.modelType;
		LIST_DATA = [];
		if (modelType == 2) PacklistInstance.onSearch([]);
		me.onSignlistChange([]);
		InfolistInstance.onSearch([]);
		me.onSignfaillistChange([]);
		SignsuccesslistInstance.onSearch([]);
		me.setFocus();
	};
	//刷新按钮
	Class.prototype.onRefreshClick = function () {
		var me = this;
		layer.msg("刷新");
	};
	//样本签收按钮
	Class.prototype.onSignClick = function (isVerified) {
		var me = this,
			modelType = me.config.modelType,
			barcodes = [],
			isVerified = isVerified || false,//是否已验证过身份
			VerifiyInvalidDate = uxutil.localStorage.get(Sign_Local_VerifiyInvalidDate_Name, true) || null,
			errorlistTableCache = SignfaillistInstance.uxtable.table.cache[SignfaillistInstance.tableId],
			checkStatus = SignlistInstance.uxtable.table.checkStatus(SignlistInstance.tableId);

		//获得选中的条码
		if (checkStatus && checkStatus.data && checkStatus.data.length > 0) {
			//是否需要签收人身份验证
			if (modelType == 3 && !isVerified && PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0010') == 1 && (!VerifiyInvalidDate || uxutil.server.date.getTimes() > VerifiyInvalidDate)) {
				me.onVerifiySign();
				return;
			}
			$.each(checkStatus.data, function (i, item) {
				barcodes.push(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]);
			});
			//根据条码号签收
			me.onAutoSignByBarCode(barcodes.join(), true, false, function (list) {
				//提示信息处理
				me.onFailureInfoHandle(list, function (resultList) {
					var Msg = [];//弹出信息
					var VoucherRetrievalList = [];//取单凭证

					$.each(resultList, function (i, item) {
						var data = item["data"],
							tipList = item["tipList"];
						$.each(LIST_DATA, function (a, itemA) {
							if (data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
								if (item["isSuccess"]) {
									//手动签收后是否清空界面 -- 针对成功签收数据
									if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0012') == 1) {
										LIST_DATA.splice(a,1);
									} else {
										LIST_DATA[a] = data;
										LIST_DATA[a]["LAY_CHECKED"] = true;
									}
									//签收后是否自动打印取单凭证
									if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0031') == 1) VoucherRetrievalList.push(data);
								} else {
									//签收失败是否显示样本信息
									if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0015') == 1) {
										LIST_DATA[a] = data;
										LIST_DATA[a]["LAY_CHECKED"] = true;
									} else {
										LIST_DATA.splice(a, 1);
									}
									//失败信息加入失败列表
									$.each(tipList, function (j, itemJ) {
										errorlistTableCache.unshift({ LisBarCodeFormVo_LisBarCodeForm_BarCode: data["LisBarCodeFormVo_LisBarCodeForm_BarCode"], LisBarCodeFormVo_failureInfo: itemJ });
									});
								}
								return false;
							}
						});
						//弹出信息处理
						if (tipList.length > 0) {
							$.each(tipList, function (k, itemK) {
								Msg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
							});
						}
					});
					if (Msg.length > 0)
						layer.alert(Msg.join('<br>'), { icon: 0, anim: 0 });
					//无数据清空打包列表
					if (modelType == 2 && LIST_DATA.length == 0) me.onPacklistChange([]);
					//数据加入列表
					me.onSignlistChange(LIST_DATA);
					//失败数据更新
					me.onSignfaillistChange(errorlistTableCache);
					//打印取单凭证
					if (VoucherRetrievalList.length > 0) me.onPrintVoucherRetrieval(VoucherRetrievalList);
				});
			});
		} else {
			layer.msg("请先勾选签收数据!", { icon:0,anim:0});
		}
	};
	//取消签收按钮
	Class.prototype.onCancelSignClick = function () {
		var me = this,
			barcodes = [],
			checkStatus = SignlistInstance.uxtable.table.checkStatus(SignlistInstance.tableId);

		if (checkStatus && checkStatus.data && checkStatus.data.length > 0) {
			$.each(checkStatus.data, function (i, item) {
				barcodes.push(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]);
			});
			me.onCancelSignByBarCode(barcodes.join());
		} else {
			layer.msg("请先勾选取消签收数据!", { icon: 0, anim: 0 });
		}
		
	};
	//签收清单按钮
	Class.prototype.onSignListClick = function () {
		var me = this,
			checkStatus = SignlistInstance.uxtable.table.checkStatus(SignlistInstance.tableId);

		if (checkStatus && checkStatus.data && checkStatus.data.length > 0) 
			me.onPrintList(checkStatus.data);
		else 
			layer.msg("请先勾选数据!", { icon: 0, anim: 0 });
	};

	//打印清单
	Class.prototype.onPrintList = function (list) {
		var me = this,
			//签收样本清单打印机名
			PrinterName = PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0038'),
			isDownLoadPDF = PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0014') == 1 ? true : false,
			list = list || [];
		if (list.length == 0) return;
		//去除前缀
		list = JSON.stringify([list]).replace(RegExp("LisBarCodeFormVo_", "g"), "").replace(RegExp("LisBarCodeForm_", "g"), "");
		layer.open({
			type: 2,
			area: ['45%', '55%'],
			fixed: false,
			maxmin: false,
			title: '打印',
			content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=' + BusinessType + '&ModelType=' + ModelType + '&ModelTypeName' + ModelTypeName + '&isDownLoadPDF=' + isDownLoadPDF + (PrinterName ? ("&PrinterName=" + PrinterName) : ""),
			success: function (layero, index) {
				var iframe = $(layero).find("iframe")[0].contentWindow;
				iframe.PrintDataStr = list;
			},
			end: function () {
			}
		});
	};
	//验证身份
	Class.prototype.onVerifiySign = function () {
		var me = this;
		layer.open({
			type: 1,
			skin: 'layui-layer-rim', //加上边框
			area: '300px', //宽高
			content: IDENYITY_DOM,
			success: function (layero, index) {
				
			}
		});
	};
	//签收显示图片信息
	Class.prototype.onShowImageInfo = function () {
		var me = this;
	};
	//打印取单凭证
	Class.prototype.onPrintVoucherRetrieval = function (list) {
		var me = this,
			BusinessTypeCode = BusinessType,//前处理
			ModelTypeCode = 9,//样本签收_样本清单
			ModelTypeName = "样本签收_取单凭证",
			barcodes = [],
			list = list || [],
			config = {};
		if (list && list.length > 0) {
			//拼接条码号
			$.each(list, function (i, item) {
				barcodes.push(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]);
			});
			//调用通用打印界面
			config = {
				nodetypeID: me.config.nodetype,
				barCodes: barcodes.join()
			};
			layer.open({
				type: 2,
				area: ['45%', '55%'],
				fixed: false,
				maxmin: false,
				title: '打印',
				content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=' + BusinessTypeCode + '&ModelType=' + ModelTypeCode + '&ModelTypeName=' + ModelTypeName + '&isDownLoadPDF=false',
				success: function (layero, index) {
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.PrintDataStr = JSON.stringify(config);
					iframe.GetPDFUrl = GET_VOUCHERRETRIEVAL_URL;
				},
				end: function () { }
			});
		}
	};

	//参数配置的提示方式处理
	Class.prototype.onFailureInfoHandle = function (list, callback) {
		var me = this,
			list = JSON.parse(JSON.stringify(list)),
			isOut = false,//是否是等待确认框执行
			pendingData = [],//暂时存储数据集合 等待再次发送服务数据返回后一起处理
			needUpdateBarcode = [],//需要再次发送服务条码号集合
			resultList = [];//{ data: null, isSuccess: true, tipList: [] };//data:当前数据，isSuccess：是否成功,tipList:提示信息集合
		$.each(list, function (i, item) {
			//自主选择-- 不允许
			if (item["IsNotAllow"] && item["IsNotAllow"] == 1) {
				resultList.push({ data: item, isSuccess: false, tipList: [item["NotAllowInfo"]] });
				//不需要再次发送服务的数据存储
				if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1) pendingData.push(item);
				return true;
			}
			resultList.push({ data: item, isSuccess: true, tipList: [] });
			var failureInfoArr = item["LisBarCodeFormVo_failureInfo"] ? JSON.parse(item["LisBarCodeFormVo_failureInfo"]) : [];
			$.each(failureInfoArr, function (j, itemJ) {
				if (isOut) return false;
				var alterMode = itemJ["alterMode"],
					failureInfo = itemJ["failureInfo"];
				switch (String(alterMode)) {
					case "4"://用户自行选择
						isOut = true;
						layer.confirm("条码号为：" + item["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "，" + failureInfo + ",是否允许操作？", { icon: 3, title: '提示', closeBtn:0 }, function (index) {
							//允许
							isOut = false;
							failureInfoArr[j]["alterMode"] = -1;
							list[i]["LisBarCodeFormVo_failureInfo"] = JSON.stringify(failureInfoArr);
							me.onFailureInfoHandle(list, callback);
							layer.close(index);
						}, function (index) {
							//不允许
							isOut = false;
							list[i]["IsNotAllow"] = 1;
							list[i]["NotAllowInfo"] = failureInfo+",用户操作不允许签收!";
							me.onFailureInfoHandle(list, callback);
							layer.close(index);
						});
						break;
					case "3"://允许且提示
						resultList[resultList.length - 1].tipList.push(failureInfo);
						break;
					case "2"://不允许不提示
						resultList[resultList.length - 1].isSuccess = false;
						break;
					case "1"://不允许且提示
						resultList[resultList.length - 1].tipList.push(failureInfo);
						resultList[resultList.length - 1].isSuccess = false;
						break;
					case "-1"://需要再次发送服务
						if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1)
							needUpdateBarcode.push(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]);
						break;
					default:
						break;
				}

			});
			//不需要再次发送服务的数据存储
			if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1) pendingData.push(item);

			if (isOut) return false;
		});
		//存在需要再次更新数据
		if (!isOut && needUpdateBarcode.length > 0) {
			me.onAutoSignByBarCode(needUpdateBarcode.join(), true, true, function (data) {
				me.onFailureInfoHandle(data.concat(pendingData), callback);
			});
		}
		//执行完成
		if (!isOut && needUpdateBarcode.length == 0) callback && callback(resultList);

	};


	//核心入口
	PreSampleSignIndex.render = function (options) {
		var me = new Class(options);

		if (!me.config.domId) {
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		PreSampleSignParamsInstance = PreSampleSignParams.render({ nodetype: me.config.nodetype });
		//初始化功能参数
		PreSampleSignParamsInstance.init(function () {
			//初始化HTML
			me.initHtml();
			//监听事件
			me.initListeners();
		});
		

		return me;
	};

	//暴露接口
	exports(MOD_NAME, PreSampleSignIndex);
});