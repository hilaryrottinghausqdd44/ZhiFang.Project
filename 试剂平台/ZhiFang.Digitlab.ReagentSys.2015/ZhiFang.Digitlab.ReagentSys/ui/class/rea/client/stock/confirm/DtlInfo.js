/**
 * 货品显示的信息
 * @author liangyl	
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.stock.confirm.DtlInfo', {
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
		
		var Price = parseFloat(data.Price || "0").toFixed(2);
		var SumTotal = parseFloat(data.SumTotal || "0").toFixed(2);
		
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
			InvalidDate = '<span style="color:' + InvalidDateColor + ';">' + JShell.Date.toString(InvalidDate, true) + '</span>';
		}
		
		var html = me.getHtmlTemplet();
		html = html.replace(/{CName}/g,data.CName || "");
		html = html.replace(/{EName}/g,data.EName || "");
		html = html.replace(/{Unit}/g,data.Unit || "");
		html = html.replace(/{UnitMemo}/g,data.UnitMemo || "");
		html = html.replace(/{LotNo}/g,data.LotNo || "");
		html = html.replace(/{InvalidDate}/g,InvalidDate || "");
		html = html.replace(/{InCount}/g,data.InCount || "");
		html = html.replace(/{AcceptCount}/g,data.AcceptCount || "");
		html = html.replace(/{Price}/g,data.Price || "");
		html = html.replace(/{SumTotal}/g,data.SumTotal || "");
		
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
			'<div style="padding:2px;">{EName}</div>' +
			'<div style="padding:2px;">规格:{UnitMemo}</div>' +
			'<div style="padding:2px;">批号:{LotNo}</div>' +
			'<div style="padding:2px;">效期:{InvalidDate}</div>' +
			'<div style="padding:2px;">验收数量:<span style="color:green;">{AcceptCount}</span> </div>' +
			'<div style="padding:2px;">已入库数量:<span style="color:green;">{InCount}</span> </div>' +
			'<div style="padding:2px;">单价:<span style="color:blue;">{Price}</span>元</div>' +
			'<div style="padding:2px;">共:<span style="color:red;">{SumTotal}</span>元</div>' +
		'</div>';
		
		return templet;
	}
});