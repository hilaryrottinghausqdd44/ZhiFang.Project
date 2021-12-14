/**
 * 发票信息
 * @author liangyl
 * @version 2016-07-08
 */
Ext.define('Shell.class.wfm.business.invoice.basic.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '发票内容',
	width: 600,
	height: 600,
	autoScroll: true,
	/**获取数据服务路径*/
	selectInfoUrl: '/SingleTableService.svc/ST_UDTO_SearchPInvoiceById',
	/**加载数据提示*/
	lodingText: JShell.Server.LOADING_TEXT,
	/**信息数据*/
	InfoData: '',
	/**信息ID*/
	PK: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.PK) {
			//初始化Html页面
			me.initHtml();
		}
	},
	initComponent: function() {
		var me = this;
		if(me.PK) {
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
//		//初始化下载监听
//		me.initDownloadListeners();
	},
	/**加载信息*/
	loadInfoData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectInfoUrl);
		var fields = [
		    'ExpectReceiveDate','IncomeTypeName','IsReceiveMoney','InvoiceNo',
			'PayOrgName', 'ProjectTypeName', 'ProjectPaceName', 'InvoiceAmount', 'ApplyMan',
			'InvoiceTypeName', 'ApplyDate', 'ComponeName', 'InvoiceContentTypeName',
			'InvoiceInfo', 'Comment', 'StatusName', 'ClientName', 'FreightOddNumbers',
			'FreightName', 'ReceiveInvoicePhoneNumbers', 'ReceiveInvoiceAddress',
			'ReceiveInvoiceName', 'InvoiceDate', 'InvoiceManName', 'TwoReviewInfo',
			'ReviewInfo', 'ReviewDate', 'ReviewMan', 'DataAddTime', 'TwoReviewMan', 'TwoReviewDate',
			'InvoiceMemo','ClientAddress','ClientPhone','VATNumber','VATBank','VATAccount',
			'HardwareCount','SoftwareCount','ServerAmount','SoftwareAmount','HardwareAmount','PercentAmount'
		];
		fields = "PInvoice_" + fields.join(",PInvoice_");
		url += "?isPlanish=true&fields=" + fields + "&id=" + me.PK;
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**获取附件信息*/
	loadAttachmentData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);

		var fields = ['Id', 'FileName', 'FileSize', 'CreatorName', 'DataAddTime', 'NewFileName', 'FileExt'];
		url += "?isPlanish=true&fields=SCAttachment_" + fields.join(",SCAttachment_");
		url += '&where=scattachment.IsUse=1 and scattachment.BobjectID=' + me.PK;

		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**获取操作记录信息*/
	loadOperLogData: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectOperLogUrl);
		var fields = ['Id', 'PTaskOperTypeID', 'OperaterID', 'OperaterName', 'DataAddTime', 'OperateMemo'];
		url += '?isPlanish=true&fields=SCOperation_' + fields.join(',SCOperation_');
		url += '&where=scoperation.BobjectID=' + me.PK;
		url += '&sort=[{"property":"SCOperation_DataAddTime","direction":"ASC"}]';
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
				html = html.replace(/{PayOrgName}/g, data.value.PInvoice_PayOrgName);
				html = html.replace(/{ProjectTypeName}/g, data.value.PInvoice_ProjectTypeName);
				html = html.replace(/{ClientName}/g, data.value.PInvoice_ClientName);
				html = html.replace(/{ProjectPaceName}/g, data.value.PInvoice_ProjectPaceName);
				var InvoiceAmount = Ext.util.Format.number(data.value.PInvoice_InvoiceAmount, data.value.PInvoice_InvoiceAmount > 0 ? '0.00' : "0");
				html = html.replace(/{InvoiceAmount}/g, InvoiceAmount);
				html = html.replace(/{ApplyMan}/g, data.value.PInvoice_ApplyMan);
				html = html.replace(/{InvoiceTypeName}/g, data.value.PInvoice_InvoiceTypeName);
				html = html.replace(/{ApplyDate}/g, JShell.Date.toString(data.value.PInvoice_ApplyDate) || '');
				html = html.replace(/{ComponeName}/g, data.value.PInvoice_ComponeName);
				html = html.replace(/{InvoiceContentTypeName}/g, data.value.PInvoice_InvoiceContentTypeName);
				html = html.replace(/{Status}/g, data.value.PInvoice_StatusName);
				html = html.replace(/{Comment}/g, data.value.PInvoice_Comment || '无');
				html = html.replace(/{ReviewDate}/g, JShell.Date.toString(data.value.PInvoice_ReviewDate) || '');
				html = html.replace(/{DataAddTime}/g, JShell.Date.toString(data.value.PInvoice_DataAddTime) || '');
				html = html.replace(/{TwoReviewDate}/g, JShell.Date.toString(data.value.PInvoice_TwoReviewDate) || '');
				html = html.replace(/{InvoiceDate}/g, JShell.Date.toString(data.value.PInvoice_InvoiceDate) || '');
				html = html.replace(/{ReviewMan}/g, data.value.PInvoice_ReviewMan);
				html = html.replace(/{TwoReviewMan}/g, data.value.PInvoice_TwoReviewMan);
				html = html.replace(/{InvoiceManName}/g, data.value.PInvoice_InvoiceManName);
				html = html.replace(/{ReceiveInvoiceName}/g, data.value.PInvoice_ReceiveInvoiceName);
				html = html.replace(/{ReceiveInvoiceAddress}/g, data.value.PInvoice_ReceiveInvoiceAddress);
				html = html.replace(/{ReceiveInvoicePhoneNumbers}/g, data.value.PInvoice_ReceiveInvoicePhoneNumbers);
				html = html.replace(/{FreightName}/g, data.value.PInvoice_FreightName);
				html = html.replace(/{FreightOddNumbers}/g, data.value.PInvoice_FreightOddNumbers);
				html = html.replace(/{ReviewInfo}/g, data.value.PInvoice_ReviewInfo);
				html = html.replace(/{TwoReviewInfo}/g, data.value.PInvoice_TwoReviewInfo);
				html = html.replace(/{InvoiceInfo}/g, data.value.PInvoice_InvoiceInfo);
				html = html.replace(/{ExpectReceiveDate}/g, JShell.Date.toString(data.value.PInvoice_ExpectReceiveDate,true) || '');
				html = html.replace(/{IncomeTypeName}/g, data.value.PInvoice_IncomeTypeName);
				html = html.replace(/{InvoiceNo}/g, data.value.PInvoice_InvoiceNo);
				html = html.replace(/{VATNumber}/g, data.value.PInvoice_VATNumber);
				html = html.replace(/{VATBank}/g, data.value.PInvoice_VATBank);
				html = html.replace(/{VATAccount}/g, data.value.PInvoice_VATAccount);
				var IsReceiveMoney= data.value.PInvoice_IsReceiveMoney=='true' ? '是' : '否';
				html = html.replace(/{IsReceiveMoney}/g, IsReceiveMoney  || '否');
				html = html.replace(/{InvoiceMemo}/g, data.value.PInvoice_InvoiceMemo);
				html = html.replace(/{ClientAddress}/g, data.value.PInvoice_ClientAddress);
				html = html.replace(/{ClientPhone}/g, data.value.PInvoice_ClientPhone);	
				html = html.replace(/{HardwareCount}/g, data.value.PInvoice_HardwareCount);
				html = html.replace(/{SoftwareCount}/g, data.value.PInvoice_SoftwareCount);
				
				var ServerAmount = Ext.util.Format.number(data.value.PInvoice_ServerAmount, data.value.PInvoice_ServerAmount > 0 ? '0.00' : "0");
				html = html.replace(/{ServerAmount}/g, ServerAmount);
				
                var SoftwareAmount = Ext.util.Format.number(data.value.PInvoice_SoftwareAmount, data.value.PInvoice_SoftwareAmount > 0 ? '0.00' : "0");
				html = html.replace(/{SoftwareAmount}/g, SoftwareAmount);

                var HardwareAmount = Ext.util.Format.number(data.value.PInvoice_HardwareAmount, data.value.PInvoice_HardwareAmount > 0 ? '0.00' : "0");
				html = html.replace(/{HardwareAmount}/g, HardwareAmount);
				
				//本次开盘金额占合同金额百分比
				var PercentAmount = data.value.PInvoice_PercentAmount || '';
				html = html.replace(/{PercentAmount}/g, PercentAmount);
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
			'<div style="' + sDivStyle + '"><b>付款单位：{PayOrgName}</b></div>' +
			'<div style="' + sDivStyle + '">用户名称：{ClientName}</div>' +
			'<div style="' + sDivStyle + '">执行公司：{ComponeName}</div>' +
			'<div style="' + sDivStyle + '">项目类别：{ProjectTypeName}</div>' +
			'<div style="' + sDivStyle + '">项目进度：{ProjectPaceName}</div>' +
			'<div style="' + sDivStyle + '">开票内容：{InvoiceContentTypeName}</div>' +
			'<div style="' + sDivStyle + '">开票金额：{InvoiceAmount}</div>' +
			'<div style="' + sDivStyle + '">软件套数：{SoftwareCount}</div>' +
			'<div style="' + sDivStyle + '">硬件数量：{HardwareCount}</div>' +
			'<div style="' + sDivStyle + '">硬件金额：{HardwareAmount}</div>' +
			'<div style="' + sDivStyle + '">软件金额：{SoftwareAmount}</div>' +
			'<div style="' + sDivStyle + '">服务金额：{ServerAmount}</div>' +
			'<div style="' + sDivStyle + '">本次开票金额占合同额百分比：{PercentAmount}</div>' +

			'<div style="' + sDivStyle + '">预计回款时间：{ExpectReceiveDate}</div>' +
			'<div style="' + sDivStyle + '">申请人：{ApplyMan}</div>' +
			'<div style="' + sDivStyle + '">申请时间：{ApplyDate}</div>' +
			'</div>' +
			'</div>' +
				//发票类型
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">发票类型：{InvoiceTypeName}</div>' +
			'<div style="' + sDivStyle + '">客户注册地址：{ClientAddress}</div>' +
			'<div style="' + sDivStyle + '">客户注册电话：{ClientPhone}</div>' +
			'<div style="' + sDivStyle + '">增值税税号：{VATNumber}</div>' +
			'<div style="' + sDivStyle + '">增值税账号：{VATAccount}</div>' +
			'<div style="' + sDivStyle + '">增值税开户行：{VATBank}</div>' +
			'</div>' +
			'</div>' +
			//一审人、一审时间、一审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">商务助理：{ReviewMan}</div>' +
			'<div style="' + sDivStyle + '">审核时间：{ReviewDate}</div>' +
			'<div style="' + sDivStyle + '">审核意见：{ReviewInfo}</div>' +
			'<div style="' + sDivStyle + '">收入分类：{IncomeTypeName}</div>' +
			'<div style="' + sDivStyle + '">申请表编号：{InvoiceNo}</div>' +
			'</div>' +
			'</div>' +
			//二审人、二审时间、二审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">商务经理：{TwoReviewMan}</div>' +
			'<div style="' + sDivStyle + '">审核时间：{TwoReviewDate}</div>' +
			'<div style="' + sDivStyle + '">审核意见：{TwoReviewInfo}</div>' +
			'</div>' +
			'</div>' +
			//三审人、三审时间、三审意见
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">出纳：{InvoiceManName}</div>' +
			'<div style="' + sDivStyle + '">是否收款：{IsReceiveMoney}</div>' +
			'<div style="' + sDivStyle + '">开票时间：{InvoiceDate}</div>' +
			'<div style="' + sDivStyle + '">开票信息：{InvoiceInfo}</div>' +
			'</div>' +
			'</div>' +
			//收票人
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">收票人姓名：{ReceiveInvoiceName}</div>' +
			'<div style="' + sDivStyle + '">收票人地址：{ReceiveInvoiceAddress}</div>' +
			'<div style="' + sDivStyle + '">收票人电话：{ReceiveInvoicePhoneNumbers}</div>' +
			'<div style="' + sDivStyle + '">货运公司名称：{FreightName}</div>' +
			'<div style="' + sDivStyle + '">货运单号：{FreightOddNumbers}</div>' +
			'</div>' +
			'</div>' +
			//发票单备注
			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">备注</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{Comment}</p>' +
			'</div>' +
			//发票单说明
			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">说明</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{InvoiceMemo}</p>' +
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