/**
 * 报销单内容
 * @author liangyl
 * @version 2016-11-02
 */
Ext.define('Shell.class.wfm.business.expenseaccount.basic.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '报销单内容',
	width: 600,
	height: 600,

	autoScroll: true,

	/**获取数据服务路径*/
	selectInfoUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPExpenseAccountById',
	/**获取附件服务路径*/
	selectAttachmentUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCAttachmentByHQL',
	/**任务操作记录服务路径*/
	selectOperLogUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCOperationByHQL',
	/**文件下载服务路径*/
	downloadUrl: '/SystemCommonService.svc/SC_UDTO_DownLoadSCAttachment',

	/**加载数据提示*/
	lodingText: JShell.Server.LOADING_TEXT,

	/**信息数据*/
	InfoData: '',
	/**附件数据*/
	AttachmentData: '',
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
			'Id', 'PExpenseAccountNo', 'ClientName', 'ComponeName', 'OneLevelItemName', 'TwoLevelItemName',
			'DeptName', 'Status', 'DayCount', 'IsSpecially',
			'Transport', 'CityTransport', 'HotelRates', 'Meals', 'AccountingDeptName',
			'EntertainsCosts', 'CommunicationCosts', 'OtherCosts',
			'PExpenseAccounAmount', 'ApplyMan', 'ApplyDate',
			'ReviewMan', 'ReviewDate', 'ReviewInfo', 'AccountingDate',
			'TwoReviewMan', 'TwoReviewDate', 'TwoReviewInfo',
			'ThreeReviewMan', 'ThreeReviewDate', 'ThreeReviewInfo',
			'FourReviewMan', 'FourReviewDate', 'FourReviewInfo',
			'PayManName', 'PayDate', 'PayDateInfo', 'CashAmount', 'TransferAmount', 'LoadAmount',
			'ReceiveManName', 'ReceiveBankInfo', 'PExpenseAccounMemo'
		];
		fields = "PExpenseAccount_" + fields.join(",PExpenseAccount_");
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
				html = html.replace(/{PExpenseAccountNo}/g, data.value.PExpenseAccount_PExpenseAccountNo || '');
				html = html.replace(/{ClientName}/g, data.value.PExpenseAccount_ClientName || '');
				html = html.replace(/{ComponeName}/g, data.value.PExpenseAccount_ComponeName || '');
				html = html.replace(/{ContractName}/g, data.value.PExpenseAccount_ContractName || '');
				html = html.replace(/{PExpenseAccounAmount}/g, data.value.PExpenseAccount_PExpenseAccounAmount || '');
				html = html.replace(/{PExpenseAccounMemo}/g, data.value.PExpenseAccount_PExpenseAccounMemo || '');
				html = html.replace(/{DeptName}/g, data.value.PExpenseAccount_DeptName || '');
				html = html.replace(/{Status}/g, data.value.PExpenseAccount_Status || '');
				html = html.replace(/{ApplyMan}/g, data.value.PExpenseAccount_ApplyMan || '');
				html = html.replace(/{ApplyDate}/g, JShell.Date.toString(data.value.PExpenseAccount_ApplyDate) || '');
				html = html.replace(/{DayCount}/g, data.value.PExpenseAccount_DayCount || '');
				html = html.replace(/{Transport}/g, data.value.PExpenseAccount_Transport || '');
				html = html.replace(/{CityTransport}/g, data.value.PExpenseAccount_CityTransport || '');
				html = html.replace(/{HotelRates}/g, data.value.PExpenseAccount_HotelRates || '');
				html = html.replace(/{Meals}/g, data.value.PExpenseAccount_Meals || '');
				html = html.replace(/{EntertainsCosts}/g, data.value.PExpenseAccount_EntertainsCosts || '');
				html = html.replace(/{CommunicationCosts}/g, data.value.PExpenseAccount_CommunicationCosts || '');
				html = html.replace(/{OtherCosts}/g, data.value.PExpenseAccount_OtherCosts || '');
				html = html.replace(/{AccountingDate}/g, data.value.PExpenseAccount_AccountingDate);
				html = html.replace(/{ReviewMan}/g, data.value.PExpenseAccount_ReviewMan || '');
				html = html.replace(/{ReviewDate}/g, JShell.Date.toString(data.value.PExpenseAccount_ReviewDate) || '');
				html = html.replace(/{ReviewInfo}/g, data.value.PExpenseAccount_ReviewInfo || '');
				html = html.replace(/{AccountingDeptName}/g, data.value.PExpenseAccount_AccountingDeptName || '');
				html = html.replace(/{ProjectTypeName}/g, data.value.PExpenseAccount_ProjectTypeName || '');
				html = html.replace(/{TwoReviewMan}/g, data.value.PExpenseAccount_TwoReviewMan || '');
				html = html.replace(/{TwoReviewDate}/g, JShell.Date.toString(data.value.PExpenseAccount_TwoReviewDate) || '');
				html = html.replace(/{TwoReviewInfo}/g, data.value.PExpenseAccount_TwoReviewInfo || '');
				html = html.replace(/{ThreeReviewMan}/g, data.value.PExpenseAccount_ThreeReviewMan || '');
				html = html.replace(/{ThreeReviewDate}/g, JShell.Date.toString(data.value.PExpenseAccount_ThreeReviewDate) || '');
				html = html.replace(/{ThreeReviewInfo}/g, data.value.PExpenseAccount_ThreeReviewInfo || '');
				html = html.replace(/{FourReviewMan}/g, data.value.PExpenseAccount_FourReviewMan || '');
				html = html.replace(/{FourReviewDate}/g, JShell.Date.toString(data.value.PExpenseAccount_FourReviewDate) || '');
				html = html.replace(/{FourReviewInfo}/g, data.value.PExpenseAccount_FourReviewInfo || '');
				html = html.replace(/{OneLevelItemName}/g, data.value.PExpenseAccount_OneLevelItemName || '');
				html = html.replace(/{TwoLevelItemName}/g, data.value.PExpenseAccount_TwoLevelItemName || '');
				html = html.replace(/{PayManName}/g, data.value.PExpenseAccount_PayManName || '');
				html = html.replace(/{PayDate}/g, JShell.Date.toString(data.value.PExpenseAccount_PayDate) || '');
				html = html.replace(/{PayDateInfo}/g, data.value.PExpenseAccount_PayDateInfo || '');
				html = html.replace(/{CashAmount}/g, data.value.PExpenseAccount_CashAmount || '');
				html = html.replace(/{TransferAmount}/g, data.value.PExpenseAccount_TransferAmount || '');
				html = html.replace(/{LoadAmount}/g, data.value.PExpenseAccount_LoadAmount || '');
				html = html.replace(/{ReceiveManName}/g, data.value.PExpenseAccount_ReceiveManName || '');
				html = html.replace(/{ReceiveBankInfo}/g, data.value.PExpenseAccount_ReceiveBankInfo || '');
				var IsSpecially = data.value.PExpenseAccount_IsSpecially == 'true' ? '是' : '否';
				html = html.replace(/{IsSpecially}/g, IsSpecially || '否');

			}
		} else {
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g, '单据内容');
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
			'<div style="' + sDivStyle + '"><b>公司名称：{ComponeName}</b></div>' +
			'<div style="' + sDivStyle + '">部门：{DeptName}</div>' +
			'<div style="' + sDivStyle + '">一级科目：{OneLevelItemName}</div>' +
			'<div style="' + sDivStyle + '">二级科目：{TwoLevelItemName}</div>' +
			'<div style="' + sDivStyle + '">核算单位：{AccountingDeptName}</div>' +
			'<div style="' + sDivStyle + '">核算年份：{AccountingDate}</div>' +
			'<div style="' + sDivStyle + '">申请人：{ApplyMan}</div>' +
			'<div style="' + sDivStyle + '">申请时间：{ApplyDate}</div>' +
			'</div>' +
			'</div>' +
			//项目名称
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>项目名称：{ClientName}</b></div>' +
			'</div>' +
			'</div>' +
			//报销金额
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>出差天数：{DayCount}</b></div>' +
			'<div style="' + sDivStyle + '"><b>交通费（跨市）：{Transport}</b></div>' +
			'<div style="' + sDivStyle + '"><b>市内车费：{CityTransport}</b></div>' +
			'<div style="' + sDivStyle + '"><b>住宿费：{HotelRates}</b></div>' +
			'<div style="' + sDivStyle + '"><b>餐费补贴：{Meals}</b></div>' +
			'<div style="' + sDivStyle + '"><b>招待费：{EntertainsCosts}</b></div>' +
			'<div style="' + sDivStyle + '"><b>通讯费：{CommunicationCosts}</b></div>' +
			'<div style="' + sDivStyle + '"><b>其它费用：{OtherCosts}</b></div>' +
			'<div style="' + sDivStyle + '"><b>报销单总金额：{PExpenseAccounAmount}</b></div>' +
			'</div>' +

			'</div>' +
			//一审人、一审时间、一审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">上级领导：{ReviewMan}</div>' +
			'<div style="' + sDivStyle + '">审核时间：{ReviewDate}</div>' +
			'<div style="' + sDivStyle + '">审核意见：{ReviewInfo}</div>' +
			'</div>' +
			'</div>' +
			//二审人、二审时间、二审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">商务助理：{TwoReviewMan}</div>' +
			'<div style="' + sDivStyle + '">商务核对时间：{TwoReviewDate}</div>' +
			'<div style="' + sDivStyle + '">商务核对意见：{TwoReviewInfo}</div>' +
			'<div style="' + sDivStyle + '">特殊审批：{IsSpecially}</div>' +
			'</div>' +
			'</div>' +

			//三审人、三审时间、三审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">总经理：{ThreeReviewMan}</div>' +
			'<div style="' + sDivStyle + '">特殊审批时间：{ThreeReviewDate}</div>' +
			'<div style="' + sDivStyle + '">特殊审批意见：{ThreeReviewInfo}</div>' +
			'</div>' +
			'</div>' +

			//四审人、四审时间、四审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">财务人员：{FourReviewMan}</div>' +
			'<div style="' + sDivStyle + '">财务复核时间：{FourReviewDate}</div>' +
			'<div style="' + sDivStyle + '">财务复核意见：{FourReviewInfo}</div>' +
			'</div>' +
			'</div>' +
			//打款负责人、打款时间、打款意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">出纳：{PayManName}</div>' +
			'<div style="' + sDivStyle + '">打款时间：{PayDate}</div>' +
			'<div style="' + sDivStyle + '">打款意见：{PayDateInfo}</div>' +
			'<div style="' + sDivStyle + '">现金金额：{CashAmount}</div>' +
			'<div style="' + sDivStyle + '">转账金额：{TransferAmount}</div>' +
			'<div style="' + sDivStyle + '">借款相抵金额：{LoadAmount}</div>' +
			'<div style="' + sDivStyle + '">领款人：{ReceiveManName}</div>' +
			'<div style="' + sDivStyle + '">领款备注：{ReceiveBankInfo}</div>' +
			'</div>' +
			'</div>' +
			//报销单说明
			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">报销单说明</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{PExpenseAccounMemo}</p>' +
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