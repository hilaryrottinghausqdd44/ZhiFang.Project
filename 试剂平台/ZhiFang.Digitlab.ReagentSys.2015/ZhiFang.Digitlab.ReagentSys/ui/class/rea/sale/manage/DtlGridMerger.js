/**
 * 管理-供货明细列表-合并
 * @author Jcall
 * @version 2017-02-19
 */
Ext.define('Shell.class.rea.sale.manage.DtlGridMerger', {
	extend: 'Shell.class.rea.sale.basic.DtlGridMerger',
	title: '管理-供货明细列表-合并',
	
	initComponent: function() {
		var me = this;
		
		//加载条码模板组件
		me.BarcodeModel = me.BarcodeModel || Ext.create('Shell.class.rea.sale.basic.BarcodeModel');
		
		var ModelType = me.BarcodeModel.getLastModdelType();
		ModelType = ModelType || me.defaultModelType;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','-',{
			xtype:'checkbox',boxLabel:'合并',itemId:'merger',checked:true,
			listeners:{
				change:function(field,newValue,oldValue){
					me.mergerData(newValue);
				}
			}
		},'-',{
			fieldLabel:'模板类型',xtype:'uxSimpleComboBox',
			itemId:'ModelType',allowBlank:false,value:me.ModelType,
			width:200,labelWidth:55,labelAlign:'right',
			data:me.BarcodeModel.getModelList(),
			listeners:{change:function(field,newValue){
				me.BarcodeModel.setLastModdelType(newValue);
			}}
		},{
			xtype:'splitbutton',
            textAlign: 'left',
			iconCls:'button-print',
			text:'条码打印',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'直接打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(1);}}
			},{
				text:'浏览打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(2);}}
			},{
				text:'维护打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(3);}}
			},{
				text:'设计打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(4);}}
			}]
		}];
		
		me.callParent(arguments);
	}
});