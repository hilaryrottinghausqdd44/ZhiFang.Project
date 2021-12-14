/**
 * 院感统计--统计表父类
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.statistics.infection.basic.InfectionDtlGrid', {
	extend: 'Shell.class.assist.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.YearComboBox'
	],

	title: '',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKListByHQLInfectionOfQuarterly?isPlanish=false',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	/**默认加载*/
	defaultLoad: false,
	
	/**默认每页数量*/
	defaultPageSize: 100,
	/**后台排序*/
	remoteSort: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'MonitoringDate',
		direction: 'ASC'
	}],
	/**报表类型*/
	breportType: 3,
	/**院感登记报表类型*/
	groupType:"",
	/**当前选择的监测类型值*/
	CurRecordTypeValue: "",
	/**感控评估查询项*/
	IsHaveDept:false,
	/**超时设置*/
	timeout:120000,
	/**是否隐藏打印按钮*/
	isHiddenPrint:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initDateArea();
	},
	initComponent: function() {
		var me = this;
		//评估标志为已评估的才统计
		if(!me.defaultWhere){
			me.defaultWhere=" gksamplerequestform.EvaluatorFlag=1 ";
		}else{
			me.defaultWhere=me.defaultWhere+" and gksamplerequestform.EvaluatorFlag=1 ";
		}
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**
	 * @description 创建监测类型项
	 * @param {Object} items
	 */
	createRecordTypeItem:function(items){
		var me=this;
		if(!items)items=[];
		
		items.push({
			xtype: 'uxSimpleComboBox',
			itemId: 'chooseRecordType',
			labelWidth: 0,
			width: 120,
			hasStyle: true,
			value: "",
			data: [
				['', '所有监测类型', 'background-color:#FFC0C0;font-weight:bold;'],
				['11', '手卫生', 'background-color:#C0FFC0;'],
				['12', '空气培养', 'background-color:#FFE0C0;'],
				['13', '物体表面', 'background-color:#FFC0FF;'],
				['14', '消毒剂', 'background-color:#C0FFFF;'],
				['15', '透析液及透析用水', 'background-color:#C0C0FF;'],
				['16', '医疗器材', 'background-color:#FFFFC0;'],
				['17', '污水', 'background-color:#00C0C0;'],
				['18', '其它', 'background-color:#C0C000;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.CurRecordTypeValue = newValue;
					me.onSearch();
				}
			}
		});
		return items;
	},
	/**
	 * @description 创建日期范围项
	 * @param {Object} items
	 */
	createDateareaItem:function(items){
		var me=this;
		if(!items)items=[];
		
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 95,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'dateType',
			value: "gksamplerequestform.DataAddTime",
			data: [
				["", "请选择"],
				["gksamplerequestform.DataAddTime", "登记日期"],
				["gksamplerequestform.SampleDate", "采样日期"],
				["gksamplerequestform.TestTime", "检验日期"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		}, {
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 215,
			labelWidth: 0,
			labelAlign: 'right',
			fieldLabel: '',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		});
		return items;
	},
	/**
	 * @description 创建监测级别项
	 * @param {Object} items
	 */
	createMonitorTypeItem:function(items){
		var me=this;
		if(!items)items=[];
		
		items.push( {
			boxLabel: '院感科监测',
			name: 'cboHospitalSense',
			itemId: 'cboHospitalSense',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			boxLabel: '科室监测',
			name: 'cboDept',
			itemId: 'cboDept',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		return items;
	},
	/**
	 * @param {Object} items
	 */
	createDeptItem: function(items) {
		var me = this;
		if (!items) items = [];
	
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 120,
			name: 'DeptCName',
			itemId: 'DeptCName',
			emptyText: '科室选择',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.department.CheckGrid',
			classConfig: {
				title: '科室选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					var data="";
					if(record)data=record.data;
					me.onDepCheck(p, data);
				}
			}
		}, {
			fieldLabel: '科室Id',
			hidden: true,
			xtype: "textfield",
			name: 'DeptId',
			itemId: 'DeptId'
		});
	
		return items;
	},	
	/**
	 * @description 弹出人员选择器选择确认后处理
	 * @param {Object} p
	 * @param {Object} data
	 */
	onDepCheck: function(p, data) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var DeptId = buttonsToolbar.getComponent('DeptId');
		var DeptCName = buttonsToolbar.getComponent('DeptCName');
		DeptCName.setValue(data ? data["Department_CName"] : '');
		DeptId.setValue(data ? data["Department_Id"] : '');
		if (p) p.close();
	
		me.onSearch();
	},
	/**
	 * @description 创建预览及导出按钮
	 * @param {Object} items
	 */
	createPDFEXCELItems:function(items){
		var me=this;
		if(!items)items=[];
		
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: '预览打印',
			hidden:me.isHiddenPrint,
			tooltip: '预览PDF清单',
			handler: function() {
				me.onPreviewPDF();
			}
		});
		items.push('-', {
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});
		
		return items;
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		
		var edate = JcallShell.System.Date.getDate();
		if(!day) day = -Ext.Date.getDayOfYear(edate);
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			date = buttonsToolbar.getComponent('date');
		if(date && dateArea) date.setValue(dateArea);
		if(me.defaultLoad==true)me.onSearch();
	},
	getWhere: function() {
		var me = this;
		var arr = [];
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		where = JShell.String.encode(where);
		return where;
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var chooseRecordType = buttonsToolbar.getComponent('chooseRecordType');
		var deptId = buttonsToolbar.getComponent('DeptId');
		var dateType = buttonsToolbar.getComponent('dateType');
		var date = buttonsToolbar.getComponent('date');
	
		var cboHospitalSense = buttonsToolbar.getComponent('cboHospitalSense'); //感控监测
		var cboDept = buttonsToolbar.getComponent('cboDept'); //科室监测
		
		var search = buttonsToolbar.getComponent('search');
		var params = [];
	
		//监测类型
		if (chooseRecordType && chooseRecordType.getValue()) {
			params.push("gksamplerequestform.SCRecordType.Id=" + chooseRecordType.getValue());
		}
		//感控评估:按选择科室过滤
		if (deptId && deptId.getValue()) {
			params.push("gksamplerequestform.DeptId=" + deptId.getValue());
		}
		//感控监测类型:1:感控监测;0:科室监测;
		if (cboHospitalSense && cboDept) {
			var check1 = cboHospitalSense.getValue(); //感控监测
			var check2 = cboDept.getValue(); //科室监测
	
			if (check1 == true && check2 == false) { //感控监测
				params.push("gksamplerequestform.MonitorType=1");
			} else if (check1 == false && check2 == true) { //科室监测
				params.push("gksamplerequestform.MonitorType=0");
			}
		}
	
		//日期范围
		if (dateType && date) {
			var dateTypeValue = dateType.getValue();
			var dateValue = date.getValue();
			if (dateValue && dateTypeValue) {
				if (dateValue.start) {
					params.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if (dateValue.end) {
					params.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
	
		if (params.length > 0) params = params.join(" and ");
	
		return params;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		var url=me.callParent(arguments);
		var docVO=me.getDocVO();
		if (docVO) {
			url += '&docVO=' + JShell.JSON.encode(docVO);
		}
		
		return url;
	},
	/**获取封装的VO查询条件*/
	getDocVO: function() {
		var me = this;
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var chooseRecordType = buttonsToolbar.getComponent('chooseRecordType');
		var deptId = buttonsToolbar.getComponent('DeptId');
		var cboYear= buttonsToolbar.getComponent('cboYear');
		var dateType = buttonsToolbar.getComponent('dateType');
		var date = buttonsToolbar.getComponent('date');	
				
		var cboHospitalSense = buttonsToolbar.getComponent('cboHospitalSense'); //感控监测
		var cboDept = buttonsToolbar.getComponent('cboDept'); //科室监测
		
		var monitorType="",recordTypeNo="",year="",startDate="",endDate="",deptIdStr="";
		//监测类型
		if (chooseRecordType && chooseRecordType.getValue()) {
			recordTypeNo= chooseRecordType.getValue();
		}
		//感控评估:按选择科室过滤
		if (deptId && deptId.getValue()) {
			deptIdStr=deptId.getValue();
		}
		//按年份
		if (cboYear && cboYear.getValue()) {
			year= cboYear.getValue();
		}
		//感控监测类型:1:感控监测;0:科室监测;
		if (cboHospitalSense && cboDept) {
			var check1 = cboHospitalSense.getValue(); //感控监测
			var check2 = cboDept.getValue(); //科室监测
			if (check1 == true && check2 == false) { //感控监测
				monitorType="1";
			} else if (check1 == false && check2 == true) { //科室监测
				monitorType="0";
			}
		}
		//日期范围
		if (dateType && date) {
			var dateTypeValue = dateType.getValue();
			var dateValue = date.getValue();
			if (dateValue && dateTypeValue) {
				if (dateValue.start) {
					startDate=JShell.Date.toString(dateValue.start, true);
				}
				if (dateValue.end) {
					endDate=JShell.Date.toString(dateValue.end, true);
					year=endDate.split('-')[0];
				}
			}
		}
		
		var vo={
			"MonitorType":monitorType,
			"RecordTypeNo":recordTypeNo,
			"DeptId":deptIdStr,
			"Year":year,
			"StartDate":startDate,
			"EndDate":endDate,
			"Quarterly":""
		};
		return vo;
	},
	/**@description预览PDF清单*/
	onPreviewPDF: function() {
		var me = this;
		
		var where=me.getWhere();
		var docVO=me.getDocVO();
		if(!where&&!docVO) {
			JShell.Msg.error("请先选择统计条件后再操作!");
			return;
		}
		
		var url = JShell.System.Path.getRootUrl("/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormOfPdfByHQL");		
		var params = [];
		var sort = "";
		if (me.curOrderBy && me.curOrderBy.length > 0) JShell.JSON.encode(me.curOrderBy); //me.store.sorters;
		
		var operateType = '1';
		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		params.push("groupType=" + me.groupType);
		params.push("where=" + where);
		if (sort) params.push("sort=" + sort);	
		if (docVO) {
			params.push("docVO=" + JShell.JSON.encode(docVO));
		}
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	},
	/**导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this;
		
		var where=me.getWhere();
		var docVO=me.getDocVO();
		if(!where&&!docVO) {
			JShell.Msg.error("请先选择统计条件后再操作!");
			return;
		}
		
		var url = JShell.System.Path.getRootUrl("/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormOfExcelByHql");
		
		var params = [];
		var sort = "";
		if (me.curOrderBy && me.curOrderBy.length > 0) JShell.JSON.encode(me.curOrderBy); //me.store.sorters;
		
		var operateType = '0';
		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		params.push("groupType=" + me.groupType);
		params.push("where=" + where);
		if (sort) params.push("sort=" + sort);
		
		if (docVO) {
			params.push("docVO=" + JShell.JSON.encode(docVO));
		}
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		url += "?" + params.join("&");
		window.open(url);
	},
	/**
	 * 操作记录查看
	 * @param {Object} record
	 */
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.WebAssist', //类域
			className: 'GKSampleFormStatus', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='GKSampleRequestForm'"
		};
		var win = JShell.Win.open('Shell.class.assist.scoperation.SCOperation', config);
		win.show();
	}
});
