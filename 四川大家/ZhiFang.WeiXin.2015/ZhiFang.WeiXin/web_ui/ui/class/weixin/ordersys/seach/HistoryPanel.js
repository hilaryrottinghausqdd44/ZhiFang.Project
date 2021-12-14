/**
 * 操作记录
 * @author liangyl
 * @version 2017-02-025
 */
Ext.define('Shell.class.weixin.ordersys.seach.HistoryPanel', {
	extend: 'Ext.panel.Panel',
	title: '操作记录',
	width: 800,
	height: 380,
	
	autoScroll:true,
	
	/**获取数据服务路径*/
    selectInfoUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormById',

    /**加载数据提示*/
	lodingText:JShell.Server.LOADING_TEXT,
	
	/**信息数据*/
	InfoData: '',
	/**是否显示操作记录*/
	showOperLog:true,
	
	/**信息ID*/
	PK:1,
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
		
		JShell.System.ClassDict.init('ZhiFang.WeiXin.Entity','UserOrderFormStatus',function(){
			if(!JShell.System.ClassDict.UserOrderFormStatus){
    			JShell.Msg.error('未获取到订单状态，请刷新列表');
    			return;
    		}
				//显示遮罩
			me.showMask(me.lodingText);
			
			me.loadInfoData(function(data){
				me.InfoData = data;//信息数据
				me.initHtmlContent();//初始化HTML页面
			});
			
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
			url = JShell.System.Path.getRootUrl(me.selectInfoUrl);
			
		var fields = [
			'Id','UOFCode','UserName','Status','Price','RefundPrice','PayTime','CancelApplyTime','CancelFinishedTime','ConsumerStartTime','ConsumerFinishedTime',
			'RefundApplyTime','RefundOneReviewManName','RefundOneReviewStartTime','RefundOneReviewFinishTime','RefundTwoReviewManName',
			'RefundTwoReviewStartTime','RefundTwoReviewFinishTime','RefundThreeReviewManName','RefundThreeReviewStartTime',
			'RefundThreeReviewFinishTime','RefundReason','RefundOneReviewReason','RefundTwoReviewReason','RefundThreeReviewReason'
		];
		
		fields = "OSUserOrderForm_" + fields.join(",OSUserOrderForm_");
		
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
				html = html.replace(/{UOFCode}/g,data.value.OSUserOrderForm_UOFCode || '');
				html = html.replace(/{UserName}/g,data.value.OSUserOrderForm_UserName || '');
				html = html.replace(/{Status}/g,data.value.OSUserOrderForm_Status || '');
				html = html.replace(/{Price}/g,data.value.OSUserOrderForm_Price || '');
				html = html.replace(/{RefundPrice}/g,data.value.OSUserOrderForm_RefundPrice || '');
				
				html = html.replace(/{PayTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_PayTime) || '');
				html = html.replace(/{CancelApplyTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_CancelApplyTime) || '');
				html = html.replace(/{CancelFinishedTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_CancelFinishedTime) || '');
				html = html.replace(/{RefundApplyTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_RefundApplyTime) || '');
				html = html.replace(/{RefundOneReviewManName}/g,data.value.OSUserOrderForm_RefundOneReviewManName || '');
				
				html = html.replace(/{RefundOneReviewStartTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_RefundOneReviewStartTime) || '');
				html = html.replace(/{RefundOneReviewFinishTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_RefundOneReviewFinishTime) || '');
				html = html.replace(/{RefundTwoReviewManName}/g,data.value.OSUserOrderForm_RefundTwoReviewManName || '');
			
				html = html.replace(/{RefundThreeReviewStartTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_RefundThreeReviewStartTime) || '');
				html = html.replace(/{RefundThreeReviewFinishTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_RefundThreeReviewFinishTime) || '');
				html = html.replace(/{RefundReason}/g,data.value.OSUserOrderForm_RefundReason || '');
				html = html.replace(/{ConsumerStartTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_ConsumerStartTime) || '');
				html = html.replace(/{ConsumerFinishedTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_ConsumerFinishedTime) || '');
				html = html.replace(/{RefundTwoReviewStartTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_RefundTwoReviewStartTime) || '');
                html = html.replace(/{RefundTwoReviewFinishTime}/g,JShell.Date.toString(data.value.OSUserOrderForm_RefundTwoReviewFinishTime) || '');
				html = html.replace(/{RefundThreeReviewManName}/g,data.value.OSUserOrderForm_RefundThreeReviewManName || '');
				html = html.replace(/{RefundOneReviewReason}/g,data.value.OSUserOrderForm_RefundOneReviewReason || '');
				html = html.replace(/{RefundTwoReviewReason}/g,data.value.OSUserOrderForm_RefundTwoReviewReason || '');
				html = html.replace(/{RefundThreeReviewReason}/g,data.value.OSUserOrderForm_RefundThreeReviewReason || '');
			}
		}else{
			html = me.getErrorTemplet();
			html = html.replace(/{UserName}/g,'操作记录');
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
		//合同标题
		'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{UOFCode}</h4>' +
		'</div>' +
		
	    '<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">用户姓名：{UserName}</b></div>' +
			    '<div style="' + sDivStyle + '">缴费时间：{PayTime}</b></div>' +
				'<div style="' + sDivStyle + '">实际金额：{Price}</b></div>' +
//				'<div style="' + sDivStyle + '">状态：{Status}</b></div>' +
			   	'<div style="' + sDivStyle + '">消费开始时间：{ConsumerStartTime}</b></div>' +
			    '<div style="' + sDivStyle + '">消费完成时间：{ConsumerFinishedTime}</b></div>' +
			    '<div style="' + sDivStyle + '">取消申请时间：{CancelApplyTime}</b></div>' +
			    '<div style="' + sDivStyle + '">取消完成时间：{CancelFinishedTime}</b></div>' +
				'<div style="' + sDivStyle + '">退费金额：{RefundPrice}</b></div>' +
				'<div style="' + sDivStyle + '">退费原因：{RefundReason}</b></div>' +
			'</div>' +
		'</div>' +
		//退款
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">退款处理人：{RefundOneReviewManName}</b></div>' +
				'<div style="' + sDivStyle + '">退款处理开始时间：{RefundOneReviewStartTime}</b></div>' +
				'<div style="' + sDivStyle + '">退款处理完成时间：{RefundOneReviewFinishTime}</b></div>' +
				'<div style="' + sDivStyle + '">退款处理说明：{RefundOneReviewReason}</b></div>' +
			'</div>' +
		'</div>' +
		//退款审批人
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">退款审批人：{RefundTwoReviewManName}</b></div>' +
				'<div style="' + sDivStyle + '">退款审批开始时间：{RefundTwoReviewStartTime}</b></div>' +
				'<div style="' + sDivStyle + '">退款审批完成时间：{RefundTwoReviewFinishTime}</b></div>' +
				'<div style="' + sDivStyle + '">退款审批说明：{RefundTwoReviewReason}</b></div>' +
			'</div>' +
		'</div>' +
		//退款发放人
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">退款发放人：{RefundThreeReviewManName}</b></div>' +
				'<div style="' + sDivStyle + '">退款发放开始时间：{RefundThreeReviewStartTime}</b></div>' +
				'<div style="' + sDivStyle + '">退款发放完成时间：{RefundThreeReviewFinishTime}</b></div>' +
				'<div style="' + sDivStyle + '">退款发放说明：{RefundThreeReviewReason}</b></div>' +
			'</div>' +
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