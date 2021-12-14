/**
 * 货品批号性能验证
 * @author liangyl
 * @version 2017-10-12
 */
Ext.define('Shell.class.rea.client.goodslot.verification.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '货品批号性能验证',

	width: 240,
	height: 185,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaGoodsLot',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLotByVerificationField',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/**内容周围距离*/
	bodyPadding: '10px 15px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 175,
		labelAlign: 'right'
	},
	/**验证状态*/
	defaluteStatus: '2',
	/**验证状态Key*/
	ReaGoodsLotVerificationStatus: 'ReaGoodsLotVerificationStatus',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaGoodsLotVerificationStatus, false, true, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		items.push(me.createbtnToolbar());
		return items;
	},
	/**创建功能按钮栏*/
	createbtnToolbar: function() {
		var me = this,
			items = [{
				xtype: 'label',
				text: '批次性能验证',
				margin: '0 0 0 10',
				style: "font-weight:bold;color:blue;",
				itemId: 'EMaintenanceData_Date',
				name: 'EMaintenanceData_Date'
			}];
		if(items.length == 0) return null;
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			height: 28,
			itemId: 'btnToolbar',
			items: items
		});
	},
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaGoodsLot_Id',
			hidden: true,
			type: 'key'
		});
		var StatusList = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].List,
			StatusList = me.removeSomeStatusList();
		//验证人
		items.push({
			fieldLabel: '货品编码',
			name: 'ReaGoodsLot_ReaGoodsNo',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '货品名称',
			name: 'ReaGoodsLot_GoodsCName',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '批号',
			name: 'ReaGoodsLot_LotNo',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '有效期',
			name: 'ReaGoodsLot_InvalidDate',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'datefield',
			colspan: 1,
			width: me.defaults.width * 1,
			format: 'Y-m-d'
		}, {
			fieldLabel: '是否性能验证',
			name: 'ReaGoodsLot_IsNeedPerformanceTest',
			xtype: 'uxBoolComboBox',
			hidden: true,
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '验证状态',
			xtype: 'uxSimpleComboBox',
			name: 'ReaGoodsLot_VerificationStatus',
			itemId: 'ReaGoodsLot_VerificationStatus',
			data: StatusList,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			name: 'ReaGoodsLot_VerificationUserName',
			itemId: 'ReaGoodsLot_VerificationUserName',
			xtype: 'uxCheckTrigger',
			fieldLabel: '验证人',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '验证人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					var UseID = me.getComponent('ReaGoodsLot_VerificationUserId');
					var UseName = me.getComponent('ReaGoodsLot_VerificationUserName');
					UseName.setValue(record ? record.get('HREmployee_CName') : '');
					UseID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			}
		}, {
			fieldLabel: '审核人id',
			hidden: true,
			name: 'ReaGoodsLot_VerificationUserId',
			itemId: 'ReaGoodsLot_VerificationUserId'
		}, {
			fieldLabel: '验证时间',
			name: 'ReaGoodsLot_VerificationTime',
			itemId: 'ReaGoodsLot_VerificationTime',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'radiogroup',
			fieldLabel: '外观',
			name: 'ReaGoodsLot_IncreaseAppearance',
			itemId: 'ReaGoodsLot_IncreaseAppearance',
			colspan: 2,
			width: me.defaults.width *2,
			columns: 2,
			vertical: true,
			items: [{
					boxLabel: '符合',
					name: 'ReaGoodsLot_IncreaseAppearance',
					inputValue: '符合'
				},
				{
					boxLabel: '不符合',
					name: 'ReaGoodsLot_IncreaseAppearance',
					inputValue: '不符合'
				}
			]
		}, {
			xtype: 'radiogroup',
			fieldLabel: '无菌试验',
			name: 'ReaGoodsLot_SterilityTest',
			itemId: 'ReaGoodsLot_SterilityTest',
			columns: 2,
			vertical: true,
			colspan: 2,
			width: me.defaults.width *2,
			items: [{
					boxLabel: '符合',
					name: 'ReaGoodsLot_SterilityTest',
					inputValue: '符合'
				},
				{
					boxLabel: '不符合',
					name: 'ReaGoodsLot_SterilityTest',
					inputValue: '不符合'
				}
			]
		}, {
			xtype: 'radiogroup',
			fieldLabel: '平行试验',
			name: 'ReaGoodsLot_ParallelTest',
			itemId: 'ReaGoodsLot_ParallelTest',
			columns: 2,
			vertical: true,
			colspan: 2,
			width: me.defaults.width *2,
			items: [{
					boxLabel: '符合',
					name: 'ReaGoodsLot_ParallelTest',
					inputValue: '符合'
				},
				{
					boxLabel: '不符合',
					name: 'ReaGoodsLot_ParallelTest',
					inputValue: '不符合'
				}
			]
		}, {
			xtype: 'radiogroup',
			fieldLabel: '生长试验',
			name: 'ReaGoodsLot_GrowthTest',
			itemId: 'ReaGoodsLot_GrowthTest',
			columns: 2,
			vertical: true,
			colspan: 2,
			width: me.defaults.width *2,
			items: [{
					boxLabel: '符合',
					name: 'ReaGoodsLot_GrowthTest',
					inputValue: '符合'
				},
				{
					boxLabel: '不符合',
					name: 'ReaGoodsLot_GrowthTest',
					inputValue: '不符合'
				}
			]
		},{
			xtype: 'radiogroup',
			fieldLabel: '留样比对试验',
			name: 'ReaGoodsLot_ComparisonTest',
			itemId: 'ReaGoodsLot_ComparisonTest',
			columns: 2,
			vertical: true,
			colspan: 2,
			width: me.defaults.width *2,
			items: [{
					boxLabel: '通过',
					name: 'ReaGoodsLot_ComparisonTest',
					inputValue: '通过'
				},
				{
					boxLabel: '不通过',
					name: 'ReaGoodsLot_ComparisonTest',
					inputValue: '不通过'
				}
			]
		},{
			fieldLabel: '验证说明',
			emptyText: '验证说明',
			name: 'ReaGoodsLot_VerificationMemo',
			xtype: 'textarea',
			height: 100,
			colspan: 2,
			width: me.defaults.width *2,
			border: false,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '验证结果',
			emptyText: '验证结果',
			name: 'ReaGoodsLot_VerificationContent',
			xtype: 'textarea',
			height: 80,
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '货品ID',
			name: 'ReaGoodsLot_GoodsID',
			hidden: true
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			VerificationStatus: values.ReaGoodsLot_VerificationStatus,
			VerificationContent: values.ReaGoodsLot_VerificationContent,
			LotNo: values.ReaGoodsLot_LotNo,
			GoodsID: values.ReaGoodsLot_GoodsID,
			GoodsCName: values.ReaGoodsLot_GoodsCName,
			InvalidDate: JShell.Date.toServerDate(values.ReaGoodsLot_InvalidDate),
			IsNeedPerformanceTest: values.ReaGoodsLot_IsNeedPerformanceTest ? 1 : 0,
			ReaGoodsNo: values.ReaGoodsLot_ReaGoodsNo,

			IncreaseAppearance: values.ReaGoodsLot_IncreaseAppearance,
			SterilityTest: values.ReaGoodsLot_SterilityTest,
			ParallelTest: values.ReaGoodsLot_ParallelTest,
			GrowthTest: values.ReaGoodsLot_GrowthTest,
			ComparisonTest: values.ReaGoodsLot_ComparisonTest
		};
		if(values.ReaGoodsLot_VerificationUserId) {
			entity.VerificationUserId = values.ReaGoodsLot_VerificationUserId;
			entity.VerificationUserName = values.ReaGoodsLot_VerificationUserName;
		}
		if(values.ReaGoodsLot_VerificationTime) {
			entity.VerificationTime = JShell.Date.toServerDate(values.ReaGoodsLot_VerificationTime);
		}

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			fields = [
				'Id', 'IsNeedPerformanceTest', 'VerificationStatus', 'VerificationContent', 'VerificationUserId',
				'VerificationUserName', 'VerificationTime', 'LotNo', 'ReaGoodsNo', 'GoodsID', 'InvalidDate', 'IncreaseAppearance', 'SterilityTest', 'ParallelTest', 'GrowthTest','ComparisonTest'
			],
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		entity.fields = fields.join(',');

		entity.entity.Id = values.ReaGoodsLot_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		me.getForm().reset();

		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(!data.ReaGoodsLot_VerificationUserId) data.ReaGoodsLot_VerificationUserId = userId;
		if(!data.ReaGoodsLot_VerificationUserName) data.ReaGoodsLot_VerificationUserName = userName;
		if(data.ReaGoodsLot_InvalidDate) data.ReaGoodsLot_InvalidDate = JcallShell.Date.toString(data.ReaGoodsLot_InvalidDate,
			true);
		if(!data.ReaGoodsLot_VerificationTime) {
			var Sysdate = JShell.System.Date.getDate();
			data.ReaGoodsLot_VerificationTime = Sysdate;
		} else {
			data.ReaGoodsLot_VerificationTime = JShell.Date.getDate(data.ReaGoodsLot_VerificationTime);
		}
		//为空时默认为通过
		if(!data.ReaGoodsLot_VerificationStatus || data.ReaGoodsLot_VerificationStatus == 1) data.ReaGoodsLot_VerificationStatus =
			'2';
		if(!data.ReaGoodsLot_IncreaseAppearance)data.ReaGoodsLot_IncreaseAppearance="";
		if(!data.ReaGoodsLot_SterilityTest)data.ReaGoodsLot_SterilityTest="";
		if(!data.ReaGoodsLot_ParallelTest)data.ReaGoodsLot_ParallelTest="";
		if(!data.ReaGoodsLot_GrowthTest)data.ReaGoodsLot_GrowthTest="";
		if(!data.ReaGoodsLot_ComparisonTest)data.ReaGoodsLot_ComparisonTest="";
		//单选组件赋值
		data.ReaGoodsLot_IncreaseAppearance = {
			"ReaGoodsLot_IncreaseAppearance": data.ReaGoodsLot_IncreaseAppearance
		};
		data.ReaGoodsLot_SterilityTest = {
			"ReaGoodsLot_SterilityTest": data.ReaGoodsLot_SterilityTest
		};
		data.ReaGoodsLot_ParallelTest = {
			"ReaGoodsLot_ParallelTest": data.ReaGoodsLot_ParallelTest
		};
		data.ReaGoodsLot_GrowthTest = {
			"ReaGoodsLot_GrowthTest": data.ReaGoodsLot_GrowthTest
		};
		data.ReaGoodsLot_ComparisonTest = {
			"ReaGoodsLot_ComparisonTest": data.ReaGoodsLot_ComparisonTest
		};
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**更改标题*/
	changeTitle: function() {
		
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus]
			.List));
		me.searchStatusValue = tempList;
		return tempList;
	}
});