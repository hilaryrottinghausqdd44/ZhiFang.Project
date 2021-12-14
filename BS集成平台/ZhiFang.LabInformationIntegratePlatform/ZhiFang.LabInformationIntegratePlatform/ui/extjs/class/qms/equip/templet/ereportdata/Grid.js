/**
 * 运营质量记录审核模块
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '运营质量记录审核模块列表',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchWillCheckRecord?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/ST_UDTO_AddEReportData',
	/**编辑服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEReportDataByField',
	/**删除服务地址*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelETemplet',
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'EReportData_ReportName',
		direction: 'ASC'
	}],
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	/**默认每页数量*/
//	defaultPageSize: 200,
	/**主键列*/
	PKField: 'EReportData_Id',
	columnLines: true,
	/**后台排序*/
	remoteSort: false,
	/**状态*/
	StatusList: [
		['0', '未审'],
		['1', '已审']
	],
	/**是否存在状态下拉框选项*/
	hasStatus: true,
	defaultStatusValue: '',
		//日期范围默认时间
	defaultAddDate:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},

	initComponent: function() {
		var me = this;
		//初始化时间
		me.initDate();
		me.searchInfo = {
			width: 140,
			emptyText: '质量记录名称',
			isLike: true,
			itemId: 'search',
			fields: ['ereportdata.ReportName']
		};
		me.buttonToolbarItems = me.createbuttonToolbarItems();

		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	createbuttonToolbarItems: function() {
		var me = this;
		//		//查询框信息
		me.searchInfo = {width: 140,emptyText: '质量记录名称',isLike: true,itemId: 'search',fields: ['ereportdata.ReportName']};
		var buttonToolbarItems = ['refresh', '-',{
			xtype: 'uxdatearea',itemId:'datearea',name:'datearea',labelWidth: 60,labelAlign: 'right',
			fieldLabel: '日期范围',value:me.defaultAddDate,
			listeners: {
				enter: function() {
					me.onSearch();
				},
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) me.onSearch();
				}
			}
		},'-', {
			fieldLabel: '类型',colspan: 1,labelWidth: 35,width: 175,labelAlign: 'right',emptyText: '质量记录类型',
			name: 'TempletType_CName',itemId: 'TempletType_CName',xtype: 'uxCheckTrigger',className: 'Shell.class.qms.equip.templet.DictCheckGrid',
			classConfig: {
				width: 350,
				title: '质量记录类型选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + JcallShell.QMS.DictTypeCode.QRecordType + "'"
			}
		}, {fieldLabel: '送检单位主键ID',itemId: 'TempletType_Id',name: 'TempletType_Id',xtype: 'textfield',hidden: true},
		'-', {
			xtype: 'uxSimpleComboBox',hasStyle: true,data: me.StatusList,value: me.defaultStatusValue,
			width: 115,labelWidth: 35,hidden: !me.hasStatus,fieldLabel: '状态',tooltip: '审核状态选择',itemId: 'selectStatus',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, '-', {type: 'search',info: me.searchInfo},'-', 
		{
			width: 60,iconCls: 'button-search',margin: '0 0 0 10px',xtype: 'button',
			hidden: true,text: '查询',tooltip: '<b>查询</b>',
			handler: function() {
				me.onSearch();
			}
		}];
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: 'Id',
			dataIndex: 'EReportData_ReportDataID',
			width: 100,
			hidden: true,
			sortable: false,
			isKey: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '仪器模板Id',
			dataIndex: 'EReportData_TempletID',
			width: 100,
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '质量记录名称',
			dataIndex: 'EReportData_ReportName',
			minWidth: 270,flex:1,
			sortable: true,
			defaultRenderer: true
		},{
			text: '年月',
			dataIndex: 'EReportData_ReportDate',
			width: 85,
			sortable: true,
			//			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var b = value.split("/");
				var v = b[0] + '年' + b[1] + '月';
				return v;
			}
		}, {
			text: '仪器名称',
			dataIndex: 'EReportData_EquipName',
			minWidth: 100,flex:1,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '小组名称',
			dataIndex: 'EReportData_SectionName',
			width: 100,
			sortable: true,
			defaultRenderer: true
		},  {
			text: '附件标志',
			dataIndex: 'EReportData_IsAttachment',width: 100,hidden: true,sortable: false,menuDisabled: false,
			defaultRenderer: true
		},{
			text: '批号',hidden:true,
			dataIndex: 'EReportData_TempletBatNo',width: 100,sortable: false,menuDisabled: false,
			defaultRenderer: true
		} ];
		return columns;
	},
	/**打开预览窗口*/
	openForm: function(hasColse, hasSave, url) {
		var me = this;
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		var config = {
			width: maxWidth,
			height: height,
			hasColse: hasColse,
			hasSave: hasSave,
			URL: url,
//			closeAction: 'hide',
			listeners: {
				save: function(win) {
					me.Grid.onSearch();
					win.hide();
					//					win.close();
				},
				onSaveClick: function(win) {
					me.fireEvent('onSaveClick', win);
				}
			}
		};
		JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.PreviewApp', config).show();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var checkList = [
			'TempletType_CName'
		];
		for(var i in checkList) {
			me.initCheckTriggerListeners(checkList[i]);
		}
	},
	/**下拉框监听*/
	initCheckTriggerListeners: function(name) {
		var me = this,
			Id = null,
			Name = null,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar) {
			Id = me.getComponent('buttonsToolbar').getComponent('TempletType_Id');
			Name = me.getComponent('buttonsToolbar').getComponent('TempletType_CName');
		}
		if(!Name) return;
		var idName = name.split('_')[0] + '_Id';
		Name.on({
			check: function(p, record) {
				Id.setValue(record ? record.get('PDict_Id') : '');
				Name.setValue(record ? record.get('PDict_CName') : '');
				me.onSearch();
				p.close();
			}
		});
	},
	
		/**初始化送检时间*/
	initDate: function() {
		var me = this;
	    var Sysdate = JShell.System.Date.getDate(); 
		var year = Sysdate.getFullYear();
		var month = Sysdate.getMonth() + 1;
		var start = JShell.Date.getMonthFirstDate(year,month,true);
		var end = JShell.Date.getMonthLastDate(year,month,true);
		
		var dateArea = {
			start:start,
			end: end
		};
		me.defaultAddDate = dateArea;
	},
	
	/**
	 * 获取上一个月
	 *
	 */
	getPreMonth: function(date) {
		var arr = date.split('-');
		var year = arr[0]; //获取当前日期的年份
		var month = arr[1]; //获取当前日期的月份
		var day = arr[2]; //获取当前日期的日
		var days = new Date(year, month, 0);
		days = days.getDate(); //获取当前日期中月的天数
		var year2 = year;
		var month2 = parseInt(month) - 1;
		if(month2 == 0) {
			year2 = parseInt(year2) - 1;
			month2 = 12;
		}
		
		var day2 = day;
		var days2 = new Date(year2, month2, 0);
		days2 = days2.getDate();
		if(day2 > days2) {
			day2 = days2;
		}
		if(month2 < 10) {
			month2 = '0' + month2;
		}
		var t2 = year2 + '-' + month2 + '-' + day2;
		return t2;
	},
	/**
	 * 获取上一个月的1号
	 */
	getPreMonthDate: function(date) {
		var arr = date.split('-');
		var year = arr[0]; //获取当前日期的年份
		var month = arr[1]; //获取当前日期的月份
		var day = arr[2]; //获取当前日期的日
		var days = new Date(year, month, 0);
		days = days.getDate(); //获取当前日期中月的天数
		var year2 = year;
		var month2 = parseInt(month) - 1;
		if(month2 == 0) {
			year2 = parseInt(year2) - 1;
			month2 = 12;
		}
		var day2 = day;
		var days2 = new Date(year2, month2, 0);
		days2 = days2.getDate();
		if(day2 > days2) {
			day2 = days2;
		}
		if(month2 < 10) {
			month2 = '0' + month2;
		}
		var t2 = year2 + '-' + month2 + '-01';
		return t2;
	},
	/**显示附件信息*/
	showAttachmentById: function(id, TempletID, beginDate, endDate) {
		var maxWidth = document.body.clientWidth - 280;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.PdfPanel', {
			formtype: "show",
			height: height,
			width: maxWidth,
			TempletID: TempletID,
			beginDate: beginDate,
			endDate: endDate,
			PK: id
		}).show();
	}
});