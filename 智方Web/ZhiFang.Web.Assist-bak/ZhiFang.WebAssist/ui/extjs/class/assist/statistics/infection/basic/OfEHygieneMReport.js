/**
 * 院感统计--院感登记清单
 * @author longfc
 * @version 2020-12-15
 */
Ext.define('Shell.class.assist.statistics.infection.basic.OfEHygieneMReport', {
	extend: 'Shell.class.assist.statistics.infection.basic.InfectionDtlGrid',

	title: '环境卫生学监测报告',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKListByHQLInfectionOfEvaluation?isPlanish=false',

	defaultOrderBy: [{
		property: 'GKSampleRequestForm_DataAddTime',
		direction: 'ASC'
	}],

	/**用户UI配置Key*/
	userUIKey: 'statistics.infection.OfEHygieneMReport',
	/**用户UI配置Name*/
	userUIName: "环境卫生学监测报告",

	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 3,
	/**院感登记报表类型*/
	groupType:6,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initDateArea();
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '监测日期',
			width: 125,
			sortable: false,
			dataIndex: 'MonitoringDate',
			doSort: function(state) {
				var field = "GKSampleRequestForm_DataAddTime";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '科室',
			dataIndex: 'DeptCName',
			width: 135,
			doSort: function(state) {
				var field = "GKSampleRequestForm_DeptId";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '场所',
			width: 125,
			sortable: false,
			dataIndex: 'Place'
		}, {
			text: '医疗器材名称',
			width: 145,
			sortable: false,
			dataIndex: 'MedicalEquipment'
		}, {
			text: '微生物学结果',
			width: 155,
			sortable: false,
			dataIndex: 'TestResult',
			doSort: function(state) {
				var field = "GKSampleRequestForm_TestResult";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '细菌总数',
			width: 125,
			sortable: false,
			dataIndex: 'MicroCount'
		}, {
			text: '检测评价',
			width: 95,
			sortable: false,
			dataIndex: 'TestEvaluation',
			doSort: function(state) {
				var field = "GKSampleRequestForm_Judgment";
				me.store.sort({
					property: field,
					direction: state
				});
			},
			renderer: function(value, meta) {
				var v = value;
				var style = 'font-weight:bold;';
				if (value == "合格") {
					style = style + "color:#ffffff;background-color:#009688;";
				} else if (value == "不合格") {
					style = style + "background-color:#FF5722;";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			text: '监测者',
			width: 125,
			sortable: false,
			dataIndex: 'TestCName',
			doSort: function(state) {
				var field = "GKSampleRequestForm_TesterName";
				me.store.sort({
					property: field,
					direction: state
				});
			}
		}, {
			text: '备注',
			width: 125,
			sortable: false,
			dataIndex: 'Memo'
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		if (me.hasRefresh) items.push('refresh');

		items = me.createRecordTypeItem(items);
		items.push('-');
		
		//按科室查询项
		if (me.IsHaveDept == true) {
			items = me.createDeptItem(items);
			items.push('-');
		}
		
		items = me.createDateareaItem(items);
		items.push('-');

		items = me.createMonitorTypeItem(items);
		items.push('-');
		
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		items.push('-');
		
		items = me.createPDFEXCELItems(items);
		
		return items;
	}
});
