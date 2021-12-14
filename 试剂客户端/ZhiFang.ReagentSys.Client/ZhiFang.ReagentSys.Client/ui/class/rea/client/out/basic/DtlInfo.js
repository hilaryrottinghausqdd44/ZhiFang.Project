/**
 * 货品弹框显示的信息
 * @description 主要显示：试剂名称，该试剂出库数，出库试剂的种类数，所有出库试剂的总数。
 * @author zhaoqi
 * @version 2020-12-17
 */
Ext.define('Shell.class.rea.client.out.basic.DtlInfo', {
	extend: 'Ext.panel.Panel',
	
	title: '货品信息',	
	width:280,
	height:380,
	
	autoScroll:true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**初始化数据*/
	initData:function(data){
		var me = this;
		var html = me.getHtmlTemplet();
		html = html.replace(/{CName}/g,data.CName || "");
		html = html.replace(/{GoodsQty}/g,data.GoodsQty || "");
		html = html.replace(/{TypesOfGoods}/g,data.TypesOfGoods || "");
		html = html.replace(/{OutSumTotal}/g,data.OutSumTotal || "");
		me.update(html);
	},
	/**清空数据*/
	clearData:function(){
		this.update("");
	},
	/**获取内容样式模板*/
	getHtmlTemplet:function(){
		//试剂名称、当前数据出库数，货品数量（就是货品种类数），所有试剂出库总数
		var templet = 
		'<div style="padding:10px;font-weight:bold;font-size:20px;">' +
			'<div style="padding:2px;">{CName}:{GoodsQty}</div>' +
			'<div style="padding:2px;">货品种类数量:{TypesOfGoods}</div>' +
			'<div style="padding:2px;">出库数合计:{OutSumTotal}</div>' +
		'</div>';
		
		return templet;
	}
});