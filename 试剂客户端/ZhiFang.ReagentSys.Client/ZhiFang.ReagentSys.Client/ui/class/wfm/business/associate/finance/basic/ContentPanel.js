/**
 *财务收款信息
 * @author liangyl
 * @version 2017-03-10
 */
Ext.define('Shell.class.wfm.business.associate.finance.basic.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '财务收款信息',
	width: 800,
	height: 600,
	
	autoScroll:true,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPFinanceReceiveById',
    /**加载数据提示*/
	lodingText:JShell.Server.LOADING_TEXT,
	
	/**信息数据*/
	InfoData: '',
	
	/**信息ID*/
	PK:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.PK){
			//初始化Html页面
			me.initHtml();
		}
	},
	
	initComponent: function() {
		var me = this;
		
		if(me.PK){
			me.html = 
			'<div class="loading-div">' +
				'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
				'<div style="padding-top:10px;">页面加载中</div>' +
			'</div>';
		}
		me.callParent(arguments);
	},
	
	initHtml:function(){
		var me = this;
		
		//显示遮罩
		me.showMask(me.lodingText);
		
		me.loadInfoData(function(data){
			me.InfoData = data;//信息数据
			me.initHtmlContent();//初始化HTML页面
		});
	},
	/**初始化HTML页面*/
	initHtmlContent: function() {
		var me = this,
			html = [];
			
		//获取任务HTML
		html.push(me.getInfoHtml());
		
		//更新HTML内容
		me.update(html.join(''));
		
		//隐藏遮罩
		me.hideMask();
		
	},
	/**加载信息*/
	loadInfoData:function(callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);
		
		var fields = [
			'Id','PClientName','PClientID','PayOrgID','PayOrgName','AccountsYear','YearMonth',
			'ReceiveAmount','SplitAmount','ReceiveDate','ReceiveMemo','IncomeTypeName',
			'IncomeClassName','InputerName','ComponeName',
			'ReviewMan','ReviewDate','ReviewInfo','DataAddTime',
			'ContrastCName','CheckCName'
		];
		
		fields = "PFinanceReceive_" + fields.join(",PFinanceReceive_");
		
		url += "?isPlanish=true&fields=" + fields + "&id=" + me.PK;
		
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	
	/**获取信息HTML*/
	getInfoHtml:function(){
		var me = this,
			data = me.InfoData,
			html = '';
			
		if(data.success){
			html = me.getInfoTemplet();
			if(data.value){
				html = html.replace(/{PClientName}/g,data.value.PFinanceReceive_PClientName || '');
				html = html.replace(/{PayOrgName}/g,data.value.PFinanceReceive_PayOrgName || '');
				html = html.replace(/{ComponeName}/g,data.value.PFinanceReceive_ComponeName || '');
				html = html.replace(/{AccountsYear}/g,data.value.PFinanceReceive_AccountsYear || '');
				
				html = html.replace(/{YearMonth}/g,data.value.PFinanceReceive_YearMonth || '');
				html = html.replace(/{InputerName}/g,data.value.PFinanceReceive_InputerName || '');
				html = html.replace(/{IncomeTypeName}/g,data.value.PFinanceReceive_IncomeTypeName || '');
				html = html.replace(/{IncomeClassName}/g,data.value.PFinanceReceive_IncomeClassName || '');
				html = html.replace(/{ReceiveAmount}/g,data.value.PFinanceReceive_ReceiveAmount || '');
				
				html = html.replace(/{SplitAmount}/g,data.value.PFinanceReceive_SplitAmount || '');
				html = html.replace(/{ReceiveMemo}/g,data.value.PFinanceReceive_ReceiveMemo || '');
				html = html.replace(/{DataAddTime}/g,JShell.Date.toString(data.value.PFinanceReceive_DataAddTime) || '');

				html = html.replace(/{ReceiveDate}/g,JShell.Date.toString(data.value.PFinanceReceive_ReceiveDate,true) || '');
				html = html.replace(/{ReviewDate}/g,JShell.Date.toString(data.value.PFinanceReceive_ReviewDate) || '');

				html = html.replace(/{ReviewMan}/g,data.value.PFinanceReceive_ReviewMan || '');
//				html = html.replace(/{ReviewDate}/g,data.value.PFinanceReceive_ReviewDate || '');
				html = html.replace(/{ReviewInfo}/g,data.value.PFinanceReceive_ReviewInfo || '');
				html = html.replace(/{ContrastCName}/g,data.value.PFinanceReceive_ContrastCName || '');
				html = html.replace(/{CheckCName}/g,data.value.PFinanceReceive_CheckCName || '');

			}
		}else{
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'财务收款内容');
			html = html.replace(/{Error}/g,data.msg);
		}
		
		return html;
	},
	/**获取信息HTML模板*/
	getInfoTemplet: function() {
		
		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';
		
		var templet =
		'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;"></h4>' +
		'</div>' +
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '"><b>收款单位：{PayOrgName}</b></div>' +
				'<div style="' + sDivStyle + '">客户名称：{PClientName}</div>' +
				'<div style="' + sDivStyle + '">执行公司：{ComponeName}</div>' +
				'<div style="' + sDivStyle + '">核算年月：{AccountsYear}{YearMonth}</div>' +
				'<div style="' + sDivStyle + '">收款日期：{ReceiveDate}</div>' +
				'<div style="' + sDivStyle + '">收款金额：{ReceiveAmount}</div>' +
				'<div style="' + sDivStyle + '">已分配金额：{SplitAmount}</div>' +
				'<div style="' + sDivStyle + '">收款说明：{ReceiveMemo}</div>' +
				'<div style="' + sDivStyle + '">录入人：{InputerName}</div>' +
				'<div style="' + sDivStyle + '">录入时间：{DataAddTime}</div>' +
				'<div style="' + sDivStyle + '">收入分类：{IncomeTypeName}</div>' +
				'<div style="' + sDivStyle + '">收入类别：{IncomeClassName}</div>' +
			'</div>' +
		'</div>' +
	
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">审核人：{ReviewMan}</div>' +
				'<div style="' + sDivStyle + '">审核时间：{ReviewDate}</div>' +
				'<div style="' + sDivStyle + '">审核人意见：{ReviewInfo}</div>' +
			'</div>' +
		'</div>' +
			
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
                '<div style="' + sDivStyle + '">对比人：{ContrastCName}</div>' +
				'<div style="' + sDivStyle + '">对比审核人：{CheckCName}</div>' +
			'</div>' +
		'</div>' +
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">收款说明</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{ReceiveMemo}</p>' +
		'</div>' ;
	
		return templet;
	},
	
	
	/**获取内容模板*/
	getContentTemplet:function(title,name){
		var templet =
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">' + title + '</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">' + name + '</p>' +
		'</div>';
		return templet;
	},
	/**获取错误信息HTML模板*/
	getErrorTemplet:function(){
		var templet = 
		'<div class="col-sm-12">' +
			'<h4>{Title}-错误信息</h4>' +
			'<p style="color:red;padding:5px;word-break:break-all;word-wrap:break-word;">{Error}</p>' +
		'</div>';
		return templet;
	},
	
	
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.body){me.body.unmask();}//隐藏遮罩层
	},
	
	/**@public 加载数据*/
	load:function(id){
		var me = this;
		me.PK = id;
    	me.initHtml();
	},
	/**清空数据*/
	clearData: function() {
		var me = this;
		me.PK = null;
		me.update('');
	}
});