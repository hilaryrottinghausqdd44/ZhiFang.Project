/**
 * 供货明细信息-基础
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.DtlInfo', {
	extend: 'Ext.panel.Panel',
	title: '供货明细信息-基础',
	
	width:240,
	height:600,
	
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
		
		var Price = parseFloat(data.Price || "0").toFixed(2);
		var TotalPrice = parseFloat(data.Price || "0") * parseInt(data.Count || "0");
		TotalPrice = TotalPrice.toFixed(2);
		
		var InvalidDate = data.InvalidDate || "";
		if(InvalidDate){
			var DAYS = 30 * 24 * 60 * 60 * 1000;//时间差，30天
			var iTimes = JcallShell.Date.getDate(InvalidDate).getTime();
			var nTimes = JShell.System.Date.getDate().getTime();
			var times = iTimes - nTimes;
			var InvalidDateColor = "green";
			if(times < 0){
				InvalidDateColor = "red";
			}else if(times < DAYS){
				InvalidDateColor = "orange";
			}
			InvalidDate = '<span style="color:' + InvalidDateColor + ';">' + InvalidDate + '</span>';
		}
		
		
		var html = me.getHtmlTemplet();
		html = html.replace(/{CName}/g,data.CName || "");
		html = html.replace(/{EName}/g,data.EName || "");
		html = html.replace(/{Unit}/g,data.Unit || "");
		html = html.replace(/{UnitMemo}/g,data.UnitMemo || "");
		html = html.replace(/{LotNo}/g,data.LotNo || "");
		html = html.replace(/{InvalidDate}/g,InvalidDate || "");
		html = html.replace(/{Count}/g,data.Count || "");
		html = html.replace(/{Price}/g,Price);
		html = html.replace(/{TotalPrice}/g,TotalPrice);
		html = html.replace(/{MixSerial}/g,data.MixSerial || "");
		
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
		'<div style="padding:10px;font-weight:bold;font-size:18px;">' +
			'<div style="padding:2px;">中文名：<span style="color:blue;">{CName}</span></div>' +
			'<div style="padding:2px;">英文名：{EName}</div>' +
			'<div style="padding:2px;">规格：{UnitMemo}</div>' +
			'<div style="padding:2px;">批号：{LotNo}</div>' +
			'<div style="padding:2px;">效期：{InvalidDate}</div>' +
			'<div style="padding:2px;">数量：<span style="color:green;">{Count}</span> {Unit}</div>' +
			'<div style="padding:2px;">单价：<span style="color:blue;">{Price}</span></div>' +
			'<div style="padding:2px;">总价：<span style="color:blue;">{TotalPrice}</span></div>' +
			'<div style="padding:2px;font-size:11px;">条码：{MixSerial}</div>' +
		'</div>';
		
		return templet;
	}
});