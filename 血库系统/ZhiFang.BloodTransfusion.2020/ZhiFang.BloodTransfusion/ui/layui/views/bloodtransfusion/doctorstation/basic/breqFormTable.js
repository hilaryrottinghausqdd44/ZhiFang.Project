/**
	@name：医嘱申请主单基础列表
	@author：longfc
	@version 2019-06-20
 */
layui.extend({
	//uxutil: 'ux/util'
	bloodsconfig: '/config/bloodsconfig',
	csserver: '/views/interface/csserver',
	formSelects: '/ux/other/formselects/dist/formSelects-v4',
	"soulTable": "ux/other/soultable/ext/soulTable"
}).define(['util', 'layer', 'table', 'uxutil', 'dataadapter', 'formSelects', "form", "bloodsconfig", "csserver",
	"soulTable"
], function(exports) {
	"use strict";

	var $ = layui.jquery;
	var form = layui.form;
	var table = layui.table;
	var uxutil = layui.uxutil;
	var util = layui.util;
	var dataadapter = layui.dataadapter;
	var formSelects = layui.formSelects;
	var bloodsconfig = layui.bloodsconfig;
	var csserver = layui.csserver;
	var soulTable = layui.soulTable;

	var breqFormTable = {
		/**版本号*/
		version: bloodsconfig.version || "1.0.0.1",
		searchInfo: {
			isLike: true,
			fields: ['bloodbreqform.Id', 'bloodbreqform.CName', 'bloodbreqform.AdmID', 'bloodbreqform.PatNo']
		},
		config: {
			/**版本号*/
			version: bloodsconfig.version || "1.0.0.1",
			elem: '',
			toolbar: "",
			//是否包含默认申请状态查询信息
			hasStausWhere:false,
			/**当前日期范围值*/
			rangeDateValue: "",
			/**是否自动选中行*/
			autoSelect: true,
			/**默认传入参数*/
			defaultParams: {

			},
			/**默认数据条件*/
			defaultWhere: '',
			/**内部数据条件*/
			internalWhere: '',
			/**外部数据条件*/
			externalWhere: '',
			/**列表当前排序*/
			sort: [{
				"property": 'BloodBReqForm_ReqTime',
				"direction": 'DESC'
			},{
				"property": 'BloodBReqForm_PrintTotal',
				"direction": 'ASC'
			},{
				"property": 'BloodBReqForm_PatNo',
				"direction": 'ASC'
			}],
			selectUrl: uxutil.path.ROOT +
				"/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormEntityListByHql?isPlanish=true",
			/**删除数据服务路径*/
			delUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqForm",
			selectUrlById: uxutil.path.ROOT +
				"/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormById?isPlanish=true",
			updPrtTotalUrl: uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormPrintTotalById",
			/**table查询服务URL*/
			url: "",
			where: "",
			defaultOrderBy: [{
				"property": 'BloodBReqForm_ReqTime',
				"direction": 'DESC'
			},{
				"property": 'BloodBReqForm_PrintTotal',
				"direction": 'ASC'
			},{
				"property": 'BloodBReqForm_PatNo',
				"direction": 'ASC'
			}],
			page: true,
			limits: [10, 15, 20, 30, 40, 50],
			cols: [
				[{
					type: 'numbers',
					width: 45,
					title: '序号'
				}, {
					title: '选择',
					hide: true,
					type: 'radio'
				}, {
					field: 'BloodBReqForm_Id',
					width: 175,
					sort: true,
					filter: true,
					title: '申请单号'
				},{
					field: 'BloodBReqForm_ReqTotal',
					sort: true,
					width: 135,
					hide: true,
					title: '大量用血申请量'
				}, {
					field: 'BloodBReqForm_BreqStatusID',
					sort: true,
					hide: true,
					width: 125,
					filter: true,
					title: '医嘱状态Id'
				}, {
					field: 'BloodBReqForm_BreqStatusName',
					sort: true,
					width: 135,
					filter: true,
					title: '医嘱状态'
				}, {
					field: 'BloodBReqForm_PrintTotal',
					hide: false,
					width: 100,
					sort: true,
					filter: true,
					title: '打印状态',
					templet: function(data) {
						var value = data["BloodBReqForm_PrintTotal"];
						if (value == '0') {
							value = '<span style = "color:#FF5722;"> ' + "未打印" + '</span>';
						} else {
							value = '<span style = "color:#009688;"> ' + "已打印" + '</span>';
						}
						return value;
					}
				}, {
					field: 'BloodBReqForm_ToHisFlag',
					sort: true,
					width: 125,
					filter: true,
					title: 'HIS数据标志',
					templet: function(data) {
						var value = "" + data["BloodBReqForm_ToHisFlag"];
						if (value == "1") {
							value = '<span style = "color:#009688">' + "上传成功" + '</span>';
						} else if (value == "2") {
							value = '<span style = "color:red">' + "上传失败" + '</span>';
						} else {
							value = '<span style = "color:#FF5722">' + "未处理" + '</span>';
						}
						return value;
					}
				}, {
					field: 'BloodBReqForm_BReqFormFlag',
					sort: true,
					width: 145,
					filter: true,
					title: '输血科审核标志',
					templet: function(data) {
						var value = "" + data["BloodBReqForm_BReqFormFlag"];
						if (value == "0") {
							value = "未受理";
						} else if (value == "1") {
							value = "受理通过";
						} else if (value == "2") {
							value = "受理不通过";
						} else {
							value = "未受理";
						}
						return value;
					}
				}, {
					field: 'BloodBReqForm_BReqTypeID',
					hide: true,
					width: 115,
					filter: true,
					title: '就诊类型编码'
				}, {
					field: 'BloodBReqForm_BReqTypeCName',
					sort: true,
					width: 115,
					filter: true,
					title: '就诊类型'
				}, {
					field: 'BloodBReqForm_UseTypeID',
					sort: true,
					hide: true,
					width: 115,
					filter: true,
					title: '申请类型编码'
				}, {
					field: 'BloodBReqForm_UseTypeCName',
					sort: true,
					width: 115,
					filter: true,
					title: '申请类型'
				}, {
					field: 'BloodBReqForm_ReqTime',
					sort: true,
					width: 165,
					filter: true,
					title: '申请时间'
				}, {
					field: 'BloodBReqForm_PatID',
					hide: true,
					filter: true,
					title: '患者ID'
				}, {
					field: 'BloodBReqForm_AdmID',
					hide: true,
					sort: true,
					width: 115,
					filter: true,
					title: '就诊号'
				}, {
					field: 'BloodBReqForm_PatNo',
					sort: true,
					width: 115,
					filter: true,
					title: '住院号'
				}, {
					field: 'BloodBReqForm_CName',
					sort: true,
					width: 115,
					filter: true,
					title: '姓名'
				},  {
					field: 'BloodBReqForm_Sex',
					sort: true,
					width: 85,
					filter: true,
					title: '性别'
				}, {
					field: 'BloodBReqForm_Birthday',
					width: 110,
					title: '出生日期',
					filter: true,
					templet: function(data) {
						var value = "" + data["BloodBReqForm_Birthday"];
						value = util.toDateString(value, 'yyyy-MM-dd');
						return value;
					}
				}, {
					field: 'BloodBReqForm_HisABOCode',
					width: 125,
					filter: true,
					title: '申请ABO血型'
				}, {
					field: 'BloodBReqForm_HisrhCode',
					width: 135,
					filter: true,
					title: '申请RhD血型'
				}, {
					field: 'BloodBReqForm_WristBandNo',
					width: 115,
					filter: true,
					title: '登记号'
				}, {
					field: 'BloodBReqForm_UseTime',
					sort: true,
					width: 165,
					filter: true,
					title: '预用血时间'
				}, {
					field: 'BloodBReqForm_HisDeptID',
					hide: true,
					filter: true,
					title: '申请科室His编码'
				}, {
					field: 'BloodBReqForm_DeptNo',
					hide: true,
					filter: true,
					title: '申请科室编码'
				}, {
					field: 'BloodBReqForm_DeptCName',
					sort: true,
					width: 115,
					filter: true,
					title: '申请科室'
				}, {
					field: 'BloodBReqForm_HisDoctorId',
					hide: true,
					filter: true,
					title: '申请医生His工号'
				}, {
					field: 'BloodBReqForm_DoctorNo',
					hide: true,
					filter: true,
					title: '申请医生Id'
				}, {
					field: 'BloodBReqForm_DoctorCName',
					sort: true,
					width: 115,
					filter: true,
					title: '申请医生'
				}, {
					field: 'BloodBReqForm_BUseTimeTypeID',
					sort: true,
					width: 85,
					filter: true,
					title: '抢救用血',
					templet: function(data) {
						var value = "" + data["BloodBReqForm_BUseTimeTypeID"];
						if (value == "1") {
							value = "是";
						} else {
							value = "否";
						}
						return value;
					}
				}, {
					field: 'BloodBReqForm_ApplyName',
					sort: true,
					width: 125,
					filter: true,
					title: '申请人'
				}, {
					field: 'BloodBReqForm_ApplyTime',
					sort: true,
					width: 165,
					filter: true,
					title: '申请时间'
				}, {
					field: 'BloodBReqForm_CheckCompleteFlag',
					sort: true,
					//type:"radio",
					width: 145,
					filter: true,
					title: '是否审批完成',
					templet: function(data) {
						var value = "" + data["BloodBReqForm_CheckCompleteFlag"];
						if (value == "1" || value == "true") {
							value = "是";
						} else {
							value = "否";
						}
						return value;
					}
				}, {
					field: 'BloodBReqForm_CheckCompleteTime',
					sort: true,
					width: 165,
					filter: true,
					title: '审批完成时间'
				}, {
					field: 'BloodBReqForm_SeniorName',
					hide: true,
					width: 125,
					filter: true,
					title: '上级审核'
				}, {
					field: 'BloodBReqForm_SeniorTime',
					sort: true,
					width: 165,
					filter: true,
					title: '上级审核时间'
				}, {
					field: 'BloodBReqForm_SeniorMemo',
					hide: true,
					width: 125,
					filter: true,
					title: '上级审核意见'
				}, {
					field: 'BloodBReqForm_DirectorName',
					hide: true,
					width: 125,
					filter: true,
					title: '科主任审核'
				}, {
					field: 'BloodBReqForm_DirectorTime',
					sort: true,
					width: 165,
					filter: true,
					title: '科主任审核时间'
				}, {
					field: 'BloodBReqForm_DirectorMemo',
					hide: true,
					width: 125,
					filter: true,
					title: '科主任审核意见'
				}, {
					field: 'BloodBReqForm_MedicalName',
					sort: true,
					hide: true,
					width: 125,
					title: '医务处审批'
				}, {
					field: 'BloodBReqForm_MedicalTime',
					sort: true,
					width: 165,
					filter: true,
					title: '医务处审批时间'
				}, {
					field: 'BloodBReqForm_MedicalMemo',
					hide: true,
					width: 125,
					filter: true,
					title: '医务处审批意见'
				}, {
					field: 'BloodBReqForm_ObsoleteName',
					sort: true,
					hide: false,
					width: 125,
					filter: true,
					title: '作废人'
				}, {
					field: 'BloodBReqForm_ObsoleteTime',
					sort: true,
					width: 165,
					filter: true,
					title: '医嘱作废时间'
				}, {
					field: 'BloodBReqForm_ObsoleteMemo',
					hide: false,
					width: 125,
					filter: true,
					title: '医嘱作废原因'
				}, {
					title: '操作列',
					fixed: 'right',
					hide: true,
					width: 80,
					align: 'center',
					toolbar: '#LAY-app-table-BloodBreqForm-ActionColumn'
				}]
			],
			drag: {
				toolbar: true
			}, //soulTable开启拖拽工具栏
			soulSort: false, //soulTable
			filter: {
				bottom: false, //soulTable隐藏底部筛选区域，默认为 true
				//soulTable用于控制表头下拉显示，可以控制顺序、显示, 依次是：表格列、筛选数据、筛选条件、编辑筛选条件、导出excel
				items: ['column'] //, 'excel''column', 'data', 'condition', 'editCondition', 'excel'
			},
			/*contextmenu: {
				// 表头右键菜单配置
				// header: [] 菜单列表
				// header: false 禁用浏览器默认菜单
				// header: [],
				// 表格内容右键菜单配置
				body: [{
					name: '保存列设置', // 显示的菜单名
					icon: 'layui-icon layui-icon-theme', // 显示图标
					click: function(obj) { //点击事件
					},
					children: [
						// 子菜单，配置同父菜单，无限层级
					]
				}]
			},*/
			response: dataadapter.toResponse(),
			parseData: function(res) {
				var result = dataadapter.toList(res);
				//默认选择第一行
				if (result.data && result.data.length > 0) result.data[0].LAY_CHECKED = true;
				return result;
			},
			done: function(res, curr, response) {
				soulTable.render(this); //soulTable初始化
				if (this.autoSelect == true) {
					var table = this;
					if (this.table != undefined) table = this.table;
					breqFormTable.doAutoSelect(table, 0);
				}
			}
		},
		set: function(options) {
			var me = this;
			if (options) me.config = $.extend({}, me.config, options);
		}
	};
	//构造器
	var Class = function(options) {
		var me = this;
		me.config = $.extend({}, breqFormTable.config, me.config, options);
		var inst = $.extend(true, {}, breqFormTable, me); //table,
		inst.config.url = inst.getLoadUrl();
		inst.config.where = inst.getWhere();
		return inst;
	};
	//调车列表排序
	breqFormTable.onSearch = function(sort) {
		var me = this;
		if (sort) me.config.sort = sort || [];
	};
	/**获取查询框内容*/
	breqFormTable.getLikeWhere = function(value) {
		var me = this;
		if (me.searchInfo == null) return "";
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	};
	//列表申请状态处理
	breqFormTable.getStausWhere = function() {
		var me = this;
		var stausWhere = [];
		if (stausWhere.length > 0) {
			return "(" + stausWhere.join(" and ") + ")";
		} else {
			return "";
		}
	};
	//列表查询项处理
	breqFormTable.getSearchWhere = function() {
		var me = this;
		var likeSearch = $("#LAY-app-table-BloodBreqForm-Search-LikeSearch");
		var txtDate = $("#LAY-app-table-BloodBreqForm-Search-Date");
		var printTotal = $("#bloodbreqform_search_printtotal"); //打印次数 2019-12-16 by xhz
		var reqformstatus = $("#bloodbreqform_search_breqformstatus"); //申请状态 2019-12-18 by xhz
		var arr = [];
		if (likeSearch) {
			var searchV = likeSearch.val();
			if (searchV) {
				searchV = me.getLikeWhere(searchV);
				if (searchV) arr.push("(" + searchV + ")");
			}
		}
		var dateV = me.config.rangeDateValue; //txtDate[0].value或txtDate.val()得到的是上一次选择的值;
		if (!dateV && txtDate) dateV = txtDate.val();
		if (dateV) {
			var arr2 = dateV.split(" - ");
			//申请生成时间ReqTime
			if (arr2 && arr2.length == 2) {
				arr.push("bloodbreqform.DataAddTime>='" + arr2[0] + " 00:00:00'");
				arr.push("bloodbreqform.DataAddTime<='" + arr2[1] + " 23:59:59'");
			}
		}
		//打印次数,0：全部; 1：未打印；2：已打印；2019-12-16 by xhz 
		if (printTotal) {
			var printStaus = printTotal.val();
			if (printStaus == '1') {
				arr.push("bloodbreqform.PrintTotal <= 0 ");
			} else if (printStaus == '2') {
				arr.push("bloodbreqform.PrintTotal > 0");
			}
		}

		//申请状态
		if (reqformstatus) {
			var reqStausVal = reqformstatus.val();
			if (reqStausVal) {
				arr.push("bloodbreqform.BreqStatusID = " + reqStausVal);
			} else if (me.config.hasStausWhere==true) {
				var stausWhere=me.getStausWhere();
				if(stausWhere&&stausWhere.length>0)arr.push(stausWhere);
			}
		}
		//就诊类型
		var bloodBReqType = formSelects.value('selectBloodBReqType', 'valStr'); // 2,4
		if (bloodBReqType) arr.push("bloodbreqform.BReqTypeID in (" + bloodBReqType + ")");
		//申请类型
		var bloodUseType = formSelects.value('selectBloodUseType', 'valStr'); // 2,4
		if (bloodUseType) arr.push("selectBloodUseType.UseTypeID in (" + bloodUseType + ")");
		//申请科室
		var dept = formSelects.value('selectDept', 'valStr'); // 2,4
		if (dept) arr.push("bloodbreqform.DeptNo in (" + dept + ")");
		//申请医生
		var doctor = formSelects.value('selectDoctor', 'valStr'); // 2,4
		if (doctor) arr.push("bloodbreqform.DoctorNo in (" + doctor + ")");

		var where = "";
		if (arr && arr.length > 0) where = "(" + arr.join(" and ") + ")";
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//获取查询Url
	breqFormTable.getLoadUrl = function() {
		var me = this;
		var url = me.config.selectUrl;
		return url;
	};
	//获取查询Fields
	breqFormTable.getFields = function(isString) {
		var me = this,
			columns = me.config.cols[0] || [],
			length = columns.length,
			fields = [];
		for (var i = 0; i < length; i++) {
			if (columns[i].field) {
				var obj = isString ? columns[i].field : {
					name: columns[i].field,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	};
	//设置默认的查询where
	breqFormTable.setDefaultWhere = function(where) {
		var me = this;
		me.config.defaultWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqFormTable.setExternalWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的查询where
	breqFormTable.setWhere = function(where) {
		var me = this;
		me.config.externalWhere = where;
		me.config.where = me.getWhere();
	};
	//设置外部传入的排序信息range
	breqFormTable.setSort = function(sort) {
		var me = this;
		me.config.sort = sort || me.config.defaultOrderBy;
	};
	//获取查询where
	breqFormTable.getWhere = function() {
		var me = this,
			arr = [];
		//默认条件
		if (me.config.defaultWhere && me.config.defaultWhere != '') {
			arr.push(me.config.defaultWhere);
		}
		//内部条件
		if (me.config.internalWhere && me.config.internalWhere != '') {
			arr.push(me.config.config.internalWhere);
		}
		//外部条件
		if (me.config.externalWhere && me.config.externalWhere != '') {
			arr.push(me.config.externalWhere);
		}
		var whereStr = "";
		if (arr.length > 0) whereStr = arr.join(") and (");
		if (whereStr) whereStr = "(" + whereStr + ")";
		var where = {
			"where": whereStr,
			'fields': me.getFields(true).join(',')
		};
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		where.t = new Date().getTime();

		var defaultOrderBy = me.config.sort || me.config.defaultOrderBy;
		if (defaultOrderBy && defaultOrderBy.length > 0) where.sort = JSON.stringify(defaultOrderBy);
		return where;
	};

	/**通过ID查询一条数据*/
	breqFormTable.LoadById = function(id, callback) {
		var me = this;
		if (!id) return;
		var url = me.config.selectUrlById;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getFields(true).join(',');
		url += '&id=' + id;
		url += '&t=' + new Date().getTime();
		uxutil.server.ajax({
			url: url
		}, function(data) {
			var result = "";
			if (data.success) {
				result = data.value || {};
			} else {
				//清空表单信息
				result = null;
			}
			if (callback) callback(result);
		});
	};
	/**删除一条数据*/
	breqFormTable.delOneById = function(index, obj, callback) {
		var me = this;
		var url = me.config.delUrl;
		var id = obj.data["BloodBReqForm_Id"];
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			uxutil.server.ajax({
				url: url
			}, function(result) {
				if (result.success) {
					obj.del();
					if (callback) callback();
				} else {
					layer.msg('删除失败:' + result.msg);
				}
			});
		}, 100 * index);
	};
	/**更新打印总数*/
	breqFormTable.updatePrintTotal = function(id, callback) {
		var me = this;
		var url = me.config.updPrtTotalUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		uxutil.server.ajax({
			url: url
		}, function(data) {
			var result = "";
			if (data.success) {
				if (callback) callback();
			} else {
				layer.msg('更新打印标志失败:' + result.msg);
			};
		});
	};
	//对外公开返回对象
	breqFormTable.result = function(that) {
		that = that || new Class();
		return {
			setDefaultWhere: that.setDefaultWhere,
			setExternalWhere: that.setExternalWhere,
			delOneById: that.delOneById
		}
	};
	/***
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	breqFormTable.doAutoSelect = function(curTable, rowIndex) {
		var key = "";
		if (curTable.id) key = curTable.id;
		else if (curTable.key) key = curTable.key;
		var data = table.cache[key] || [];
		if (!data || data.length <= 0) return;

		rowIndex = rowIndex || 0;
		var filter = curTable.config.elem.attr('lay-filter');
		var thatrow = $(curTable.layBody[0]).find('tr:eq(' + rowIndex + ')');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function() {
				table.cache[key][index] = [];
				tr.remove();
				curTable.scrollPatch();
			},
			updte: {}
		};
		setTimeout(function() {
			layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		}, 300);
	};
	//设置外部传入的日期范围值
	breqFormTable.setRangeDateValue = function(value) {
		var me = this;
		me.config.rangeDateValue = value;
	};
	/***
	 * @description 行单击后设置当前行为单选选中状态 
	 * @param obj:当前操作行
	 * */
	breqFormTable.setRadioCheck = function(obj) {
		var me = this;
		var index = $(obj.tr).data('index');
		obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click'); //选中行样式
		obj.tr.find('input[lay-type="layTableRadio"]').prop("checked", true);
		var thisData = table.cache[me.config.id];
		//重置数据单选属性
		layui.each(thisData, function(i, item) {
			if (index === i) {
				item.LAY_CHECKED = true;
			} else {
				delete item.LAY_CHECKED;
			}
		});
		form.render('radio');
	};
	//作废处理
	breqFormTable.onObsolete = function(curRow, obsolete, callback) {
		var me = this;
		var reqFormId = curRow["BloodBReqForm_Id"];
		var statusId = curRow["BloodBReqForm_BreqStatusID"];
		var breqFormFlag = "" + curRow["BloodBReqForm_BReqFormFlag"];
		var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"]; //审批完成标志
		//判断是否可以进行作废操作:已作废不能作废
		if (statusId == "10") {
			var satusName = curRow["BloodBReqForm_BreqStatusName"];
			layer.open({
				type: 1,
				offset: "auto",
				content: '<div style="padding: 20px 10px;">当前医嘱状态为:<span style="color:red;">' + satusName +
					'</span>,不能操作!</div>',
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
			return callback(false);
		}
		//判断输血科审核标志:输血科审核受理通过不能作废
		if (breqFormFlag == "2") {
			layer.open({
				type: 1,
				offset: "auto",
				content: '<div style="padding: 20px 10px;">当前医嘱的输血科审核标志为:<span style="color:red;">受理通过</span>,不能操作!</div>',
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
			return callback(false);
		}

		//先判断是否需要调用HIS的作废接口(开启了调用HIS作废标志并且返回HIS数据成功)	
		var toHisFlag = "" + curRow["BloodBReqForm_ToHisFlag"];
		//toHisFlag="1";
		if (bloodsconfig.HisInterface.ISTOHISOBSOLETE == true && toHisFlag == "1") {
			//如果已经上传给HIS,先作废HIS成功后再作废BS血库数据库用血申请单
			me.onObsoleteToHis(curRow, function(result) {
				if (result.success == true) {
					me.onBSObsolete(curRow, obsolete, callback);
				}
			});
		} else {
			me.onBSObsolete(curRow, obsolete, callback);
		}
	};
	//作废BS血库数据库用血申请单
	breqFormTable.onBSObsolete = function(curRow, obsolete, callback) {
		var me = this;
		var reqFormId = curRow["BloodBReqForm_Id"];
		var userInfo = bloodsconfig.getCurOper();
		var empID = userInfo.empID;
		var empName = userInfo.empName;
		var entity = {
			"Id": reqFormId,
			"BreqStatusID": 10
		};
		if (obsolete) {
			entity.ObsoleteMemoId = obsolete.ObsoleteMemoId;
			entity.ObsoleteMemo = obsolete.ObsoleteMemo;
		}
		var params = {
			"entity": entity,
			"fields": "Id,BreqStatusID",
			"empID": empID,
			"empName": empName
		};
		//配置类信息
		var bloodsConfigVO = {
			"Common": bloodsconfig.Common,
			"CSServer": bloodsconfig.CSServer,
			"HisInterface": bloodsconfig.HisInterface
		};
		params.bloodsConfigVO = bloodsConfigVO;
		params = JSON.stringify(params);
		var config = {
			type: "POST",
			url: me.config.confirmUrl,
			timeout: uxutil.BS_TIME_OUT,
			data: params
		};
		//显示遮罩层
		var layerIndex = layer.msg('作废处理中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		uxutil.server.ajax(config, function(result2) {
			layer.close(layerIndex);
			//隐藏遮罩层
			if (result2.success == false) {
				//layer.msg(result2.msg);
				layer.alert(result2.msg, {
					title: "作废HIS申请提示",
					btn: ['关闭'],
					icon: 5,
					end: function(index) {
						if (callback) callback(result2);
					}
				});
			}else{
				if (callback) callback(result2);
			}
		});
	};
	//用血申请作废(BS调用CS服务,CS服务调用HIS作废接口作废)
	breqFormTable.onObsoleteToHis = function(curRow, callback) {
		var me = this;		
		me.onObsoleteToHisOfBS(curRow,function(result){
			if (callback) callback(result);
		});
	};
	/**调用BS服务作废用血申请单*/
	breqFormTable.onObsoleteToHisOfBS = function(curRow, callback) {
		var me = this;
		var reqFormId = curRow["BloodBReqForm_Id"];
		var userInfo = bloodsconfig.getCurOper();
		var empID = userInfo.empID;
		var empName = userInfo.empName;
		//当前操作的医生信息
		var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);	
		var params = {
			"id": reqFormId,
			"curDoctor": sysCurUserInfo,
			"empID": empID,
			"empName": empName
		};
		//配置类信息
		var bloodsConfigVO = {
			"Common": bloodsconfig.Common,
			"CSServer": bloodsconfig.CSServer,
			"HisInterface": bloodsconfig.HisInterface
		};
		params.bloodsConfigVO = bloodsConfigVO;
		params = JSON.stringify(params);
		
		var url = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateReqDataObsoleteToHis";
		var config = {
			type: "POST",
			url: url,
			timeout: uxutil.BS_TIME_OUT,
			data: params
		};
		//显示遮罩层
		var layerIndex = layer.msg('用血申请作废处理中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		uxutil.server.ajax(config, function(result) {
			layer.close(layerIndex);
			//隐藏遮罩层
			if (result.success == false) {
				layer.alert(result.msg, {
					title: "用血申请作废提示",
					btn: ['关闭'],
					icon: 5,
					end: function(index) {
						if (callback) callback(result);
					}
				});
			}else{
				if (callback) callback(result);
			}
		});
	};
	//物理删除处理
	breqFormTable.onDelete = function(curRow, callback) {
		var me = this;
		var reqFormId = curRow["BloodBReqForm_Id"];
		var toHisFlag = "" + curRow["BloodBReqForm_ToHisFlag"];
		var checkCompleteFlag = "" + curRow["BloodBReqForm_CheckCompleteFlag"];
		//审批完成后,医嘱申请单只能进行作废处理,不能进行物理删除;
		if (checkCompleteFlag == "1" || checkCompleteFlag.toLowerCase() == "true") {
			layer.open({
				type: 1,
				offset: "auto",
				content: '<div style="padding: 20px 10px;">当前医嘱申请已审批完成,不允许删除!</div>',
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
			return;
		}
		//不能物理删除条件:HIS数据标志为"上传成功";
		if (toHisFlag == "1") {
			var satusName = curRow["BloodBReqForm_BreqStatusName"];
			layer.open({
				type: 1,
				offset: "auto",
				content: '<div style="padding: 20px 10px;">当前医嘱申请数据已返回HIS成功,不允许删除!</div>',
				btn: '关闭',
				btnAlign: 'c',
				shade: 0,
				yes: function() {
					layer.closeAll();
				}
			});
			return;
		}

		var url = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqForm?id=" + reqFormId;
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		url = url + "&t=" + new Date().getTime();
		var config = {
			type: "GET",
			timeout: uxutil.BS_TIME_OUT,
			url: url
		};
		//显示遮罩层
		var layerIndex = layer.msg('删除申请处理中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		uxutil.server.ajax(config, function(result) {
			layer.close(layerIndex);
			//隐藏遮罩层
			if (result.success == false) {
				//layer.msg(result.msg);
				layer.alert(result.msg, {
					title: "删除提示",
					btn: ['关闭'],
					icon: 5,
					end: function(index) {
						if (callback) callback(result);
					}
				});
			}else{
				if (callback) callback(result);
			}
		}, true);
	};
	//审核通过或退回保存处理
	breqFormTable.onReview = function(entity, callback) {
		var me = this;
		var userInfo = bloodsconfig.getCurOper();
		var empID = userInfo.empID;
		var empName = userInfo.empName;
		//当前操作的医生信息
		var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);
		var params = {
			"entity": entity,
			"curDoctor": sysCurUserInfo,
			"fields": "Id,BreqStatusID",
			"empID": empID,
			"empName": empName
		};
		//配置类信息
		var bloodsConfigVO = {
			"Common": bloodsconfig.Common,
			"CSServer": bloodsconfig.CSServer,
			"HisInterface": bloodsconfig.HisInterface
		};
		params.bloodsConfigVO = bloodsConfigVO;
		params = JSON.stringify(params);

		var url = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormOfReviewByReqForm";
		var config = {
			type: "POST",
			url: url,
			timeout: uxutil.BS_TIME_OUT,
			data: params
		};
		//显示遮罩层
		var layerIndex = layer.msg('审核处理中,请耐心等待...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		uxutil.server.ajax(config, function(result) {
			layer.close(layerIndex);
			//隐藏遮罩层
			if (result.success == false) {
				layer.alert(result.msg, {
					title: "审核提示",
					btn: ['关闭'],
					icon: 5,
					end: function(index) {
						if (callback) callback(result);
					}
				});
			}else{
				if (callback) callback(result);
			}
		});
	};
	/**手工将用血申请单上传HIS*/
	breqFormTable.onToHisData = function(curRow, callback) {
		var me = this;
		
		me.onToHisDataOfBS(curRow,function(result){
			if (callback) callback(result);
		});
	};
	/**手工调用BS服务返回医嘱信息给HIS*/
	breqFormTable.onToHisDataOfBS = function(curRow, callback) {
		var me = this;
		var reqFormId = curRow["BloodBReqForm_Id"];
		var userInfo = bloodsconfig.getCurOper();
		var empID = userInfo.empID;
		var empName = userInfo.empName;
		//当前操作的医生信息
		var sysCurUserInfo = bloodsconfig.getData(bloodsconfig.cachekeys.SYSDOCTORINFO_KEY);	
		var params = {
			"id": reqFormId,
			"curDoctor": sysCurUserInfo,
			"empID": empID,
			"empName": empName
		};
		//配置类信息
		var bloodsConfigVO = {
			"Common": bloodsconfig.Common,
			"CSServer": bloodsconfig.CSServer,
			"HisInterface": bloodsconfig.HisInterface
		};
		params.bloodsConfigVO = bloodsConfigVO;
		params = JSON.stringify(params);
		
		var url = uxutil.path.ROOT + "/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateReqDataUploadToHis";
		var config = {
			type: "POST",
			url: url,
			timeout: uxutil.BS_TIME_OUT,
			data: params
		};
		//显示遮罩层
		var layerIndex = layer.msg('上传HIS处理中...', {
			time: 0,
			icon: 16,
			shade: 0.5
		});
		uxutil.server.ajax(config, function(result) {
			layer.close(layerIndex);
			//隐藏遮罩层
			if (result.success == false) {
				layer.alert(result.msg, {
					title: "上传HIS提示",
					btn: ['关闭'],
					icon: 5,
					end: function(index) {
						if (callback) callback(result);
					}
				});
			}else{
				if (callback) callback(result);
			}
		});
	};
	/**手工调用CS服务返回医嘱信息给HIS*/
	breqFormTable.onToHisDataOfCS = function(curRow, callback) {
		var me = this;
		var reqFormId = curRow["BloodBReqForm_Id"];
		
		var url = bloodsconfig.CSServer.CS_TOHISDATA_URL + "?sBreqFormID=" + reqFormId;
		//IE浏览器查询时,需要带上个额外的时间戳参数,防止获取到的查询结果是缓存信息
		url = url + "&t=" + new Date().getTime();
		var config = {
			type: "GET",
			dataType: "json",
			contentType: "application/json; charset=UTF-8",
			async: true,
			url: url
		};
		csserver.csAjax(config, function(result) {			
			layer.closeAll();
			var success = false;
			if (result) success = result.success;
			//隐藏遮罩层
			if (success == false) {
				if (!result.msg) result.msg = "调用返回医嘱信息给HIS服务失败,请查看日志!";
				//layer.msg(result.msg);
				layer.alert(result.msg, {
					title: "上传HIS提示",
					btn: ['关闭'],
					icon: 5,
					end: function(index) {
						if (callback) callback(result);
					}
				});
			}else{
				if (callback) callback(result);
			}
			
		}, null, true);
	};
	//核心入口
	breqFormTable.render = function(options) {
		var me = this;
		var inst = new Class(options);
		//可以考虑添加inst.getSearchWhere();				
		inst.tableIns = table.render(inst.config);
		return inst;
	};
	//暴露接口
	exports('breqFormTable', breqFormTable);
});
