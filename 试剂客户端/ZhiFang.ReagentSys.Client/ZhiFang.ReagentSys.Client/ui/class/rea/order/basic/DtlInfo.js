/**
 * 订货明细信息
 * @author Jcall
 * @version 2017-03-06
 */
Ext.define('Shell.class.rea.order.basic.DtlInfo', {
	extend: 'Shell.ux.form.Panel',
	title: '订货明细信息',
	
	width:240,
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
    
   	NameFormat:
   		'<div style="width:100%;text-align:center;margin-top:10px;' +
    		'font-size:24px;color:blue;">{msg}</div>',
    UnitMemoFormat:
    	'<div style="width:100%;text-align:center;margin-top:10px;' +
    		'font-size:18px;color:#5bc0de;">{msg}</div>',
    NumberFormat:
    	'<div style="width:100%;text-align:left;margin-top:10px;">' +
			//'<b>货品总数：</b>' +
			'<span style="color:green;font-size:24px;">{number}</span>' +
			'<b style="margin-left:5px;">{unit}</b>' +
		'</div>',
	PriceFormat:
    	'<div style="width:100%;text-align:left;margin-top:10px;">' +
			'<b>单价：</b>' +
			'<span style="color:red;font-size:24px;">{msg}</span>元' +
		'</div>',
	TotalPriceFormat:
    	'<div style="width:100%;text-align:left;margin-top:10px;">' +
			'<b>总价：</b>' +
			'<span style="color:red;font-size:24px;">{msg}</span>元' +
		'</div>',
		
	Format:
		'<div style="text-align:center;margin-top:5px;">' +
			'<div style="font-size:24px;color:blue;">{Name}</div>' +
			'<div style="font-size:12px;color:blue;">{UnitMemo}</div>' +
			'<div style="font-size:12px;color:blue;">{Price}/{Unit}</div>' +
			'<div style="font-size:12px;color:blue;">{Number}{Unit}</div>' +
			'<div style="font-size:12px;color:blue;">{TotalPrice}</div>' +
		'</div>',
		
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = [{itemId:'Format'}];
		//me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		var items = [
			{itemId:'GoodsName'},//产品名称
			{itemId:'UnitMemo'},//包装规格
			{itemId:'GoodsNumber'},//数量
			{itemId:'GoodsPrice'},//单价
			{itemId:'TotalPrice'}//总价
		];
		
		return items;
	},
	/**初始化数据*/
	initData2:function(data){
		var me = this,
			GoodsName = me.getComponent('GoodsName'),
			UnitMemo = me.getComponent('UnitMemo'),
			GoodsNumber = me.getComponent('GoodsNumber'),
			GoodsPrice = me.getComponent('GoodsPrice'),
			TotalPrice = me.getComponent('TotalPrice');
			
		me.defaultData = data;
			
		var name = me.NameFormat.replace(/{msg}/g,(data.Name || '没有名称'));
		GoodsName.setText(name,false);
		
		var unitmemo = me.UnitMemoFormat.replace(/{msg}/g,(data.UnitMemo || ''));
		UnitMemo.setText(unitmemo,false);
		
		var number = me.NumberFormat.replace(/{number}/g,(data.Qty || '0'));
		number = number.replace(/{unit}/g,(data.Unit || '无单位'));
		GoodsNumber.setText(number,false);
		
		var price = me.PriceFormat.replace(/{msg}/g,(data.Price || '没有名称'));
		GoodsPrice.setText(price,false);
		
		var totalprice = me.TotalPriceFormat.replace(/{msg}/g,(data.Price*data.Qty));
		TotalPrice.setText(totalprice,false);
	},
	/**清空数据*/
	clearData2:function(){
		var me = this,
			GoodsName = me.getComponent('GoodsName'),
			UnitMemo = me.getComponent('UnitMemo'),
			GoodsNumber = me.getComponent('GoodsNumber'),
			GoodsPrice = me.getComponent('GoodsPrice'),
			TotalPrice = me.getComponent('TotalPrice');
			
		me.defaultData = null;
		
		GoodsName.setText('');
		UnitMemo.setText('');
		GoodsNumber.setText('');
		GoodsPrice.setText('');
		TotalPrice.setText('');
	},
	/**初始化数据*/
	initData:function(data){
		var me = this,
			html = [];
			
		html.push('<div style="text-align:center;margin-top:5px;">');
		html.push('<div style="padding:5px;font-size:24px;color:blue;">' + data.Name + '</div>');
		html.push('<div style="padding:5px;font-weight:bold;color:#5bc0de">' + data.UnitMemo + '</div>');
		html.push('<div style="padding:5px;"><span style="font-size:21px;color:orange;">' + data.Price + '</span>&nbsp;<b>元/' + data.Unit + '</b></div>');
		html.push('<div style="padding:5px;"><span style="font-size:21px;color:green;">' + data.Qty + '</span>&nbsp;<b>' + data.Unit + '</b></div>');
		html.push('<div style="padding:5px;"><b>共</b><span style="font-size:21px;color:red;">' + (data.Qty*data.Price).toFixed(2) + '</span>&nbsp;<b>元</b></div>');
		html.push('</div>');
			
		me.update(html.join(""));
	},
	/**清空数据*/
	clearData:function(){
		var me = this;
		me.update('');
	}
});