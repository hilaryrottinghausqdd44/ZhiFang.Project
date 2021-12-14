/**
 * 还款内容
 * @author liangyl
 * @version 2016-11-02
 */
Ext.define('Shell.class.wfm.business.prepayment.basic.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '还款内容',
	width: 600,
	height: 600,
	autoScroll: true,
	/**获取数据服务路径*/
	selectInfoUrl: '/SingleTableService.svc/ST_UDTO_SearchPRepaymentById',
	/**任务操作记录服务路径*/
	selectOperLogUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCOperationByHQL',
	/**加载数据提示*/
	lodingText: JShell.Server.LOADING_TEXT,
	/**信息数据*/
	InfoData: '',
	/**操作记录数据*/
	OperLogData: '',
	/**是否显示操作记录*/
	showOperLog: true,
	/**信息ID*/
	InfoId: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.InfoId) {
			//初始化Html页面
			me.initHtml();
		}
	},
	initComponent: function() {
		var me = this;
		if(me.InfoId) {
			me.html =
				'<div class="loading-div">' +
				'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
				'<div style="padding-top:10px;">页面加载中</div>' +
				'</div>';
		}
		me.callParent(arguments);
	},

	initHtml: function() {
		var me = this;
		//显示遮罩
		me.showMask(me.lodingText);
		me.loadInfoData(function(data) {
			me.InfoData = data; //信息数据
			me.initHtmlContent(); //初始化HTML页面
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
	loadInfoData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectInfoUrl);
		var fields = [
			'Id', 'PRepaymentlTypeName', 'PRepaymentAmount', 'PRepaymentContentTypeName',
			'PRepaymentMemo', 'DeptName', 'ApplyMan',
			'ApplyDate', 'ReviewMan', 'ReviewDate',
			'ReviewInfo'
		];
		fields = "PRepayment_" + fields.join(",PRepayment_");
		url += "?isPlanish=true&fields=" + fields + "&id=" + me.InfoId;
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},

	/**获取信息HTML*/
	getInfoHtml: function() {
		var me = this,
			data = me.InfoData,
			html = '';

		if(data.success) {
			html = me.getInfoTemplet();
			if(data.value) {
				html = html.replace(/{DeptName}/g, data.value.PRepayment_DeptName || '');
				html = html.replace(/{PRepaymentlTypeName}/g, data.value.PRepayment_PRepaymentlTypeName || '');
				html = html.replace(/{PRepaymentAmount}/g, data.value.PRepayment_PRepaymentAmount || '');
				html = html.replace(/{PRepaymentContentTypeName}/g, data.value.PRepayment_PRepaymentContentTypeName || '');
				html = html.replace(/{PRepaymentMemo}/g, data.value.PRepayment_PRepaymentMemo || '');
				html = html.replace(/{PExpenseAccounAmount}/g, data.value.PRepayment_PExpenseAccounAmount || '');
				html = html.replace(/{Status}/g, data.value.PRepayment_Status || '');
				html = html.replace(/{ApplyDate}/g, JShell.Date.toString(data.value.PRepayment_ApplyDate) || '');
				html = html.replace(/{ReviewMan}/g, data.value.PRepayment_ReviewMan || '');
				html = html.replace(/{ReviewInfo}/g, data.value.PRepayment_ReviewInfo || '');
				html = html.replace(/{ReviewDate}/g, JShell.Date.toString(data.value.PRepayment_ReviewDate) || '');
				html = html.replace(/{ApplyMan}/g, data.value.PRepayment_ApplyMan || '');
			}
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g, '还款内容');
			html = html.replace(/{Error}/g, data.msg);
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
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;"></h4>' +
			'</div>' +
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>还款内容：{PRepaymentContentTypeName}</b></div>' +
			'<div style="' + sDivStyle + '">还款金额：{PRepaymentAmount}</div>' +
			'<div style="' + sDivStyle + '">还款部门：{DeptName}</div>' +
			'<div style="' + sDivStyle + '">还款人：{ApplyMan}</div>' +
			'<div style="' + sDivStyle + '">还款时间：{ApplyDate}</div>' +
			'</div>' +
			'</div>' +
			//一审人、一审时间、一审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">审核人：{ReviewMan}</div>' +
			'<div style="' + sDivStyle + '">审核时间：{ReviewDate}</div>' +
			'<div style="' + sDivStyle + '">审核意见：{ReviewInfo}</div>' +
			'</div>' +
			'</div>' +
			//还款说明
			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">还款说明</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{PRepaymentMemo}</p>' +
			'</div>';
		return templet;
	},

	/**获取内容模板*/
	getContentTemplet: function(title, name) {
		var templet =
			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">' + title + '</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">' + name + '</p>' +
			'</div>';
		return templet;
	},
	/**获取错误信息HTML模板*/
	getErrorTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<h4>{Title}-错误信息</h4>' +
			'<p style="color:red;padding:5px;word-break:break-all;word-wrap:break-word;">{Error}</p>' +
			'</div>';
		return templet;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},

	/**@public 加载数据*/
	load: function(id) {
		var me = this;
		me.InfoId = id;
		me.initHtml();
	},
	/**清空数据*/
	clearData: function() {
		var me = this;
		me.InfoId = null;
		me.update('');
	}
});