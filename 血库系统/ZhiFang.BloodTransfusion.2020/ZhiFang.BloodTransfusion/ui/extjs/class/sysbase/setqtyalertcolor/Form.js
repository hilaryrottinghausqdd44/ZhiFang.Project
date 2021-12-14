/**
 * 预警颜色表单
 * @author xiehz
 * @version 2020-08-03
 */

Ext.define('Shell.class.sysbase.setqtyalertcolor.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: ['Shell.class.sysbase.setqtyalertcolor.ColorPicker', 
	  'Shell.ux.form.field.SimpleComboBox'],
	title: '预警颜色报警信息',
	width: 240,
	height: 400,
	bodyPadding: 10,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertColorById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodSetQtyAlertColor',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSetQtyAlertColorByField',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 75,
		labelAlign: 'right'
	},
	/**机构ID*/
	LabId: 0,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent: function() {
		var me = this;
		me.AlertType ='AlertType';
		JcallShell.BLTF.StatusList.getStatusList(me.AlertType, false, false, null);
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
        var AlertTypedata = JcallShell.BLTF.StatusList.Status[me.AlertType].List;
		var items = [{
				fieldLabel: '预警编号',
				name: 'BloodSetQtyAlertColor_Id',
				itemId: 'BloodSetQtyAlertColor_Id',
				emptyText: '数字编码',
				locked: true,
				readOnly: true
			},
			{
				fieldLabel: '预警名称',
				name: 'BloodSetQtyAlertColor_AlertName',
				itemId: 'BloodSetQtyAlertColor_AlertName',
				emptyText: '必填项',
				allowBlank: false
			},
			{
				fieldLabel: '预警分类',
				xtype:'uxSimpleComboBox',
				data: AlertTypedata,
				name: 'BloodSetQtyAlertColor_AlertTypeId',
				itemId: 'BloodSetQtyAlertColor_AlertTypeId',
				emptyText: '必填项',	
				allowBlank: false
			},{
				fieldLabel: '预警分类名',
				name: 'BloodSetQtyAlertColor_AlertTypeName',
				itemId: 'BloodSetQtyAlertColor_AlertTypeName',
			},
			{
				fieldLabel: '预警颜色',
				name: 'BloodSetQtyAlertColor_AlertColor',
				itemId: 'BloodSetQtyAlertColor_AlertColor',
				xtype:'smmcolorpicker',
				emptyText: '必填项',
				readOnly: true,
				allowBlank: false				
			},
			{
				fieldLabel: '下限值',
				name: 'BloodSetQtyAlertColor_StoreLower',
				itemId: 'BloodSetQtyAlertColor_StoreLower',
				xtype: 'numberfield',
				emptyText: '必填项',	
				allowBlank: false					
			},
			{
				fieldLabel: '上限值',
				name: 'BloodSetQtyAlertColor_StoreUpper',
				itemId: 'BloodSetQtyAlertColor_StoreUpper',
				xtype: 'numberfield',
				emptyText: '必填项',	
				allowBlank: false					
			},
			{
				fieldLabel: '显示次序',
				name: 'BloodSetQtyAlertColor_DispOrder',
				xtype: 'numberfield'
			},
		    {
				boxLabel: '是否启用',
				name: 'BloodSetQtyAlertColor_Visible',
				xtype: 'checkbox',
				checked: true
			}			
		];
		return items;
	},
	
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodSetQtyAlertColor_Id').setReadOnly(false);
		me.getComponent('BloodSetQtyAlertColor_AlertColor').setReadOnly(false);
	},
	
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodSetQtyAlertColor_Id').setReadOnly(false);
		me.getComponent('BloodSetQtyAlertColor_AlertColor').setReadOnly(false);
	},
	
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('BloodSetQtyAlertColor_Id').setReadOnly(false);
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id:-2,
			AlertName: values.BloodSetQtyAlertColor_AlertName,
			AlertTypeId: values.BloodSetQtyAlertColor_AlertTypeId,
			AlertColor: values.BloodSetQtyAlertColor_AlertColor,
			StoreUpper: values.BloodSetQtyAlertColor_StoreUpper,
			StoreLower: values.BloodSetQtyAlertColor_StoreLower,
			DispOrder: values.BloodSetQtyAlertColor_DispOrder,
			Visible: values.BloodSetQtyAlertColor_Visible ? true : false
		};
		if (values.BloodSetQtyAlertColor_Id) entity.Id = values.BloodSetQtyAlertColor_Id;
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

		for (var i in fields) {
			var arr = fields[i].split('_');
			if (arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		entity.empID = empID;
		entity.empName = empName;
		entity.entity.Id = values.BloodSetQtyAlertColor_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	}

})