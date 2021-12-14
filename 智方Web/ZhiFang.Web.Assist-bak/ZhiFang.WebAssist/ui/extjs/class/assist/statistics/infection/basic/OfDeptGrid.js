/**
 * 院感统计--按科室统计表
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.statistics.infection.basic.OfDeptGrid', {
	extend: 'Shell.class.assist.statistics.infection.basic.InfectionDtlGrid',
	
	title: '按科室',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKListByHQLInfectionOfDept?isPlanish=false',

	defaultOrderBy: [{
		property: 'GKSampleRequestForm_DataAddTime',
		direction: 'ASC'
	}],
	
	/**用户UI配置Key*/
	userUIKey: 'statistics.infection.OfDeptGrid',
	/**用户UI配置Name*/
	userUIName: "按科室",
	
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 3,
	/**院感登记报表类型*/
	groupType:4,
	
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
				text: '科室',
				dataIndex: 'DeptCName',
				width: 90,
				defaultRenderer: true
			}, {
				text: '手卫生',
				//dataIndex: 'HandHSamplesCount',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'HandHSamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'HandHQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'HandHQualifiedRate'
				}]
			}, {
				text: '空气培养',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'AirCSamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'AirCQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'AirCQualifiedRate'
				}]
			}, {
				text: '物体表面',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'SurfaceSamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'SurfaceQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'SurfaceQualifiedRate'
				}]
			}, {
				text: '消毒剂',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'DisinfectantSamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'DisinfectantQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'DisinfectantQualifiedRate'
				}]
			}, {
				text: '透析液及透析用水',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'DialysateSamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'DialysateQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'DialysateQualifiedRate',
				}]
			},
			{
				text: '医疗器材',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'MedicalESamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'MedicalEQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'MedicalEQualifiedRate'
				}]
			},
			{
				text: '污水',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'SewageSamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'SewageQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'SewageQualifiedRate'
				}]
			}, {
				text: '其它',
				columns: [{
					text: '采样数',
					width: 75,
					sortable: true,
					dataIndex: 'OtherSamplesCount'
				}, {
					text: '合格数',
					width: 75,
					sortable: true,
					dataIndex: 'OtherQualifiedCount'
				}, {
					text: '合格率%',
					width: 75,
					sortable: true,
					dataIndex: 'OtherQualifiedRate'
				}]
			}, {
				text: '采样数',
				width: 75,
				sortable: true,
				dataIndex: 'SumSamplesCount'
			}, {
				text: '合格数',
				width: 75,
				sortable: true,
				//renderer: change,
				dataIndex: 'SumQualifiedCount'
			}, {
				text: '合格率%',
				width: 75,
				sortable: true,
				dataIndex: 'SumQualifiedRate'
			}
		];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		if (me.hasRefresh) items.push('refresh');
		
		items=me.createRecordTypeItem(items);
		items.push('-');
		//感控评估查询项
		if (me.IsHaveDept == true) {
			items = me.createDeptItem(items);
			items.push('-');
		}
		
		items=me.createDateareaItem(items);
		items.push('-');
		
		items=me.createMonitorTypeItem(items);
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
	},
	/**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this;
		var fields =
			"DeptCName,HandHSamplesCount,HandHQualifiedCount,HandHQualifiedRate,AirCSamplesCount,AirCQualifiedCount,AirCQualifiedRate,SurfaceSamplesCount,SurfaceQualifiedCount,SurfaceQualifiedRate,DisinfectantSamplesCount,DisinfectantQualifiedCount,DisinfectantQualifiedRate,DialysateSamplesCount,DialysateQualifiedCount,DialysateQualifiedRate,MedicalESamplesCount,MedicalEQualifiedCount,MedicalEQualifiedRate,SewageSamplesCount,SewageQualifiedCount,SewageQualifiedRate,OtherSamplesCount,OtherQualifiedCount,OtherQualifiedRate,SumSamplesCount,SumQualifiedCount,SumQualifiedRate";
		fields = fields.split(",");
		return fields;
	},
	
});
