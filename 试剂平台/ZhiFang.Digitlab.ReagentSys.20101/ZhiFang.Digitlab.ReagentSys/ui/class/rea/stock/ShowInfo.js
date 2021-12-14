/**
 * 供货明细信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.stock.ShowInfo', {
	extend: 'Shell.ux.form.Panel',
	title: '供货明细信息',

	width: 240,
	height: 400,

	/**内容周围距离*/
	bodyPadding: '5px 10px 0 10px',
	/**布局方式*/
	layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		xtype: 'label'
	},

	NameFormat: '<div style="width:100%;text-align:center;margin-top:10px;' +
		'font-size:24px;color:blue;">{msg}</div>',
	UnitMemoFormat: '<div style="width:100%;text-align:center;margin-top:10px;' +
		'font-size:18px;color:black;">{msg}</div>',
	NoFormat: '<div style="width:100%;text-align:left;margin-top:10px;">' +
		'<b>{name}：</b>' +
		'<span style="color:black;font-size:18px;">{msg}</span>' +
		'</div>',
	InvalidDateFormat:
    	'<div style="width:100%;text-align:left;margin-top:10px;">' +
			'<b>有效期至：</b>' +
			'<span style="color:red;font-size:24px;">{msg}</span>' +
		'</div>',
	GoodsQtyFormat: 
		'<div style="width:100%;text-align:left;margin-top:10px;">' +
			'<b>产品总数：</b>' +
			'<span style="color:red;font-size:24px;">{number}</span>' +
			'<b style="margin-left:5px;">{unit}</b>' +
		'</div>',
	CenOrgFormat:'<div style="width:100%;text-align:left;margin-top:10px;">' +
		'<b>{name}：</b>' +
		'<span style="color:black;font-size:18px;">{msg}</span>' +
		'</div>',

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
		var me = this;

		var items = [
			{itemId: 'GoodsName'}, //产品名称
			{itemId: 'UnitMemo'}, //规格
			{itemId: 'ProdGoodsNo'}, //产品编号
			{itemId: 'LotNo'}, //产品批号
			{itemId: 'InvalidDate'}, //有效期至
			{itemId: 'GoodsQty'}, //数量
			{itemId: 'ProdOrgName'}, //品牌
			{itemId: 'CompanyName'}, //供应商
			{itemId: 'LabName'}, //实验室
			
		];

		return items;
	},
	/**初始化数据*/
	initData: function(data) {
		var me = this,
			GoodsName = me.getComponent('GoodsName'),
			ProdGoodsNo = me.getComponent('ProdGoodsNo'),
			UnitMemo = me.getComponent('UnitMemo'),
			LotNo = me.getComponent('LotNo'),
			InvalidDate = me.getComponent('InvalidDate'),
			GoodsQty = me.getComponent('GoodsQty'),
			ProdOrgName = me.getComponent('ProdOrgName'),
			CompanyName = me.getComponent('CompanyName'),
			LabName = me.getComponent('LabName');

		me.defaultData = data;

		var GoodsNameValue = me.NameFormat.replace(/{msg}/g, (data.GoodsName || ''));
		GoodsName.setText(GoodsNameValue, false);

		var ProdGoodsNoValue = me.NoFormat.replace(/{name}/g,'产品编号').replace(/{msg}/g, (data.ProdGoodsNo || ''));
		ProdGoodsNo.setText(ProdGoodsNoValue, false);

		var UnitMemoVlaue = me.UnitMemoFormat.replace(/{msg}/g, (data.UnitMemo || ''));
		UnitMemo.setText(UnitMemoVlaue, false);
		
		var LotNoVlaue = me.NoFormat.replace(/{name}/g,'产品批号').replace(/{msg}/g, (data.LotNo || ''));
		LotNo.setText(LotNoVlaue, false);
		
		var InvalidDateVlaue = me.InvalidDateFormat.replace(/{msg}/g, (data.InvalidDate || ''));
		InvalidDate.setText(InvalidDateVlaue, false);

		var GoodsQtyValue = me.GoodsQtyFormat.replace(/{number}/g, (data.GoodsQty || '0'));
		GoodsQtyValue = GoodsQtyValue.replace(/{unit}/g, (data.GoodsUnit || ''));
		GoodsQty.setText(GoodsQtyValue, false);
		
		var ProdOrgNameVlaue = me.CenOrgFormat.replace(/{name}/g,'品牌').replace(/{msg}/g, (data.ProdOrgName || ''));
		ProdOrgName.setText(ProdOrgNameVlaue, false);
		
		var CompanyNameVlaue = me.CenOrgFormat.replace(/{name}/g,'供应商').replace(/{msg}/g, (data.CompanyName || ''));
		CompanyName.setText(CompanyNameVlaue, false);
		
		var LabNameVlaue = me.CenOrgFormat.replace(/{name}/g,'实验室').replace(/{msg}/g, (data.LabName || ''));
		LabName.setText(LabNameVlaue, false);
	},
	/**清空数据*/
	clearData: function() {
		var me = this,
			GoodsName = me.getComponent('GoodsName'),
			ProdGoodsNo = me.getComponent('ProdGoodsNo'),
			UnitMemo = me.getComponent('UnitMemo'),
			LotNo = me.getComponent('LotNo'),
			InvalidDate = me.getComponent('InvalidDate'),
			GoodsQty = me.getComponent('GoodsQty'),
			ProdOrgName = me.getComponent('ProdOrgName'),
			CompanyName = me.getComponent('CompanyName'),
			LabName = me.getComponent('LabName');

		me.defaultData = null;

		GoodsName.setText('');
		ProdGoodsNo.setText('');
		UnitMemo.setText('');
		LotNo.setText('');
		InvalidDate.setText('');
		GoodsQty.setText('');
		ProdOrgName.setText('');
		CompanyName.setText('');
		LabName.setText('');
	}
});