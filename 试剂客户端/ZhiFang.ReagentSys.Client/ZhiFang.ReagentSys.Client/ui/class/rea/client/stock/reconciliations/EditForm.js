/**
 * 修改发票号
 * @author liuyuj,zhaoqi
 * @version 2020-9-21
 */
Ext.define('Shell.class.rea.client.stock.reconciliations.EditForm', {
	extend: 'Shell.ux.form.Panel',
	
	title: '发票号',
	formtype: 'edit',
	width: 440,
	height: 180,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocByField',
	
	
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasCancel: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '15px 20px 0px 0px',
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
	defaults:{
		anchor:'100%',
	    labelWidth:100,
	    labelAlign:'right'
	},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'textarea',
			fieldLabel: '主键ID',
			name: 'ReaBmsInDoc_Id',
			itemId: 'ReaBmsInDoc_Id',
			hidden: true,
			hideable: false,
			isKey: true
		});
		//发票号
		items.push({
			xtype: 'textarea',
			fieldLabel: '发票号',
			name: 'ReaBmsInDoc_InvoiceNo',
			itemId: 'ReaBmsInDoc_InvoiceNo',
			width: me.defaults.width*4,
			height: 20
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsInDoc_Memo',
			itemId: 'ReaBmsInDoc_Memo',
			width: me.defaults.width*4,
			height: 50
		});
		return items;
	},
	//@overwrite 获取新增的数据
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			InvoiceNo: values.ReaBmsInDoc_InvoiceNo,
			Memo:values.ReaBmsInDoc_Memo
		};
		return {
			entity: entity
		};
	},
	
	//@overwrite 获取修改的数据
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
		
		entity.entity.Id = values.ReaBmsInDoc_Id;
		return entity;
	}
});