/**
 * 条码号表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.DtlBarcodeForm', {
	extend: 'Shell.ux.form.Panel',
	title: '条码号表单',
	
	width:200,
	height:400,
	
	/**内容周围距离*/
	bodyPadding:'5px 10px 0 10px',
	/**布局方式*/
	layout:'anchor',
	/** 每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
    	xtype:'label'
    },
    
    GoodsNameFormat:'<div style="width:100%;text-align:center;margin-top:10px;' +
    	'font-size:24px;color:blue;">{msg}</div>',
	GoodsNumberFormat:'<div style="width:100%;text-align:center;margin-top:10px;">' +
		'<b>{label}：</b>' +
		'<span style="color:green;font-size:30px;">{number}</span>' +
		'<b style="margin:5px;">{unit}</b></div>',
		
	BarcodeNumberFormat:'<div style="width:100%;text-align:center;margin-top:10px;">' +
		'<b style="color:red;">{label}：</b>' +
		'<span style="color:red;font-size:30px;">{number}</span>' +
		'<b style="margin:5px;">{unit}</b></div>',
    
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		var items = [
			{itemId:'GoodsName'},
			{itemId:'GoodsNumber'},
			{itemId:'BarcodeNumber'}
		];
		
		return items;
	},
	/**初始化数据*/
	initData:function(data){
		var me = this,
			GoodsName = me.getComponent('GoodsName'),
			GoodsNumber = me.getComponent('GoodsNumber');
			
		me.defaultData = data;
			
		var name = me.GoodsNameFormat.replace(/{msg}/g,(data.Name || '无名称'));
		GoodsName.setText(name,false);
		
		var number = me.GoodsNumberFormat.replace(/{number}/g,(data.Qty || '0'));
		number = number.replace(/{unit}/g,(data.Unit || '无单位'));
		number = number.replace(/{label}/g,'货品总数');
		
		GoodsNumber.setText(number,false);
	},
	/**清空数据*/
	clearData:function(){
		var me = this,
			GoodsName = me.getComponent('GoodsName'),
			GoodsNumber = me.getComponent('GoodsNumber'),
			BarcodeNumber = me.getComponent('BarcodeNumber');
			
		me.defaultData = null;
		
		GoodsName.setText('');
		GoodsNumber.setText('');
		BarcodeNumber.setText('');
	},
	/**修改条码数量*/
	changeBarcodeNumber:function(number){
		var me = this,
			BarcodeNumber = me.getComponent('BarcodeNumber');
		
		var number = me.BarcodeNumberFormat.replace(/{number}/g,(number || '0'));
		number = number.replace(/{unit}/g,(me.defaultData.Unit || '无单位'));
		number = number.replace(/{label}/g,'已扫条码');
		
		BarcodeNumber.setText(number,false);
	}
});