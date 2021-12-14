/**
 * 重复扫码提示信息
 * @author longfc
 * @version 2019-11-12
 */
Ext.define('Shell.class.rea.client.transfer.basic.DtlInfo', {
	extend: 'Ext.panel.Panel',
	title: '提示信息',
	
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
		//html = html.replace(/{EName}/g,data.EName || "");
		html = html.replace(/{BarCode}/g,data.BarCode || "");
		html = html.replace(/{Info}/g,data.Info || "");
		
		me.update(html);
	},
	/**清空数据*/
	clearData:function(){
		this.update("");
	},
	/**获取内容样式模板*/
	getHtmlTemplet:function(){
		//中文名称、英文名称、规格、批号、效期、数量、单位、单价、总价
		var templet = 
		'<div style="padding:10px;font-weight:bold;font-size:20px;">' +
			'<div style="padding:2px;"><span style="color:blue;">{CName}</span></div>' +
			//'<div style="padding:2px;">{EName}</div>' +
			'<div style="padding:2px;">条码号:{BarCode}</div>' +
			'<div style="padding:2px;color:red;">提示:{Info}</div>' +
		'</div>';
		
		return templet;
	}
});