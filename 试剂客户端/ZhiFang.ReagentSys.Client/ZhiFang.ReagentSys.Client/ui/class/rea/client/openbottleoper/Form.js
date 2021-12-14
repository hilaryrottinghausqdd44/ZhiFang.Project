/**
 * 开瓶管理
 * @author longfc	
 * @version 2020-01-18
 */
Ext.define('Shell.class.rea.client.openbottleoper.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '开瓶管理',
	width: 270,
	height: 380,

	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
	defaults:{
		anchor:'92%',
	    labelWidth:100,
	    labelAlign:'right'
	},
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_GetOBottleOperDocByOutDtlId?isPlanish=true',
	/**获取数据服务路径*/
	//selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaOpenBottleOperDocById?isPlanish=true',
	/**更新数据服务路径*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaOpenBottleOperDocByField',
	
	PK: null,
	OutDtlID: null,
	/**主键字段*/
	PKField: 'ReaOpenBottleOperDoc_Id',
	
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [{
				fieldLabel: '是否使用完',
				boxLabel: '',
				name: 'ReaOpenBottleOperDoc_IsUseCompleteFlag',
				xtype: 'checkbox',
				inputValue: true
			},
			{
				fieldLabel: '开瓶时间',
				name: 'ReaOpenBottleOperDoc_BOpenDate',
				xtype: 'datetimefield',
				format: 'Y-m-d H:i:s'
			}, 
			{
				fieldLabel: '开瓶后有效期',
				name: 'ReaOpenBottleOperDoc_InvalidBOpenDate',
				xtype: 'datetimefield',
				format: 'Y-m-d H:i:s'
			}, {
				fieldLabel: '使用完时间',
				name: 'ReaOpenBottleOperDoc_UseCompleteDate',
				xtype: 'datetimefield',
				format: 'Y-m-d H:i:s'
			}, 
			{
				fieldLabel: '是否作废',
				boxLabel: '',
				name: 'ReaOpenBottleOperDoc_IsObsolete',
				xtype: 'checkbox',
				inputValue: true
			},{
				fieldLabel: '作废时间',
				name: 'ReaOpenBottleOperDoc_ObsoleteTime',
				xtype: 'datetimefield',
				format: 'Y-m-d H:i:s'
			},
			{
				fieldLabel: '作废备注',
				height: 85,
				name: 'ReaOpenBottleOperDoc_ObsoleteMemo',
				xtype: 'textarea'
			},
			{
				fieldLabel: '出库总单ID',
				name: 'ReaOpenBottleOperDoc_OutDocID',
				hidden: true
			},
			{
				fieldLabel: '出库明细单ID',
				name: 'ReaOpenBottleOperDoc_OutDtlID',
				hidden: true
			},
			{
				fieldLabel: '库存ID',
				name: 'ReaOpenBottleOperDoc_QtyDtlID',
				hidden: true
			},
			{
				fieldLabel: '货品ID',
				name: 'ReaOpenBottleOperDoc_GoodsID',
				hidden: true
			},
			{
				fieldLabel: '主键ID',
				name: 'ReaOpenBottleOperDoc_Id',
				type: "key",
				hidden: true
			}
		];
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			IsUseCompleteFlag:values.ReaOpenBottleOperDoc_IsUseCompleteFlag ? 1 : 0,
			IsObsolete:values.ReaOpenBottleOperDoc_IsObsolete ? 1 : 0
		};
		if (values.ReaOpenBottleOperDoc_OutDocID) entity.OutDocID = values.ReaOpenBottleOperDoc_OutDocID;
		if (values.ReaOpenBottleOperDoc_OutDtlID) entity.OutDtlID = values.ReaOpenBottleOperDoc_OutDtlID;
		if (values.ReaOpenBottleOperDoc_QtyDtlID) entity.QtyDtlID = values.ReaOpenBottleOperDoc_QtyDtlID;		
		if (values.ReaOpenBottleOperDoc_GoodsID) entity.GoodsID = values.ReaOpenBottleOperDoc_GoodsID;
		if (values.ReaOpenBottleOperDoc_Id) entity.Id = values.ReaOpenBottleOperDoc_Id;
		
		if (values.ReaOpenBottleOperDoc_Memo) {
			entity.Memo = values.ReaOpenBottleOperDoc_Memo;
		}
		if (values.ReaOpenBottleOperDoc_BOpenDate) {
			entity.BOpenDate = JcallShell.Date.toServerDate(values.ReaOpenBottleOperDoc_BOpenDate);
		}
		if (values.ReaOpenBottleOperDoc_InvalidBOpenDate) {
			entity.InvalidBOpenDate = JcallShell.Date.toServerDate(values.ReaOpenBottleOperDoc_InvalidBOpenDate);
		}
		if (values.ReaOpenBottleOperDoc_UseCompleteDate) {
			entity.UseCompleteDate = JcallShell.Date.toServerDate(values.ReaOpenBottleOperDoc_UseCompleteDate);
		}
		if (values.ReaOpenBottleOperDoc_ObsoleteTime) {
			entity.ObsoleteTime = JcallShell.Date.toServerDate(values.ReaOpenBottleOperDoc_ObsoleteTime);
		}
		return {"entity":entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.ReaOpenBottleOperDoc_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		me.PK = data["ReaOpenBottleOperDoc_Id"];
		var bOpenDate = data.ReaOpenBottleOperDoc_BOpenDate;
		if (bOpenDate) {
			data.ReaOpenBottleOperDoc_BOpenDate = JcallShell.Date.toString(bOpenDate.replace(/\//g, "-"));
		}
		var invalidBOpenDate = data.ReaOpenBottleOperDoc_InvalidBOpenDate;
		if (invalidBOpenDate) {
			data.ReaOpenBottleOperDoc_InvalidBOpenDate = JcallShell.Date.toString(invalidBOpenDate.replace(/\//g, "-"));
		}
		var useCompleteDate = data.ReaOpenBottleOperDoc_UseCompleteDate;
		if (useCompleteDate) {
			data.ReaOpenBottleOperDoc_UseCompleteDate = JcallShell.Date.toString(useCompleteDate.replace(/\//g, "-"));
		}
		var obsoleteTime = data.ReaOpenBottleOperDoc_ObsoleteTime;
		if (obsoleteTime) {
			data.ReaOpenBottleOperDoc_ObsoleteTime = JcallShell.Date.toString(obsoleteTime.replace(/\//g, "-"));
		}
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	}
});
