/**
 *预览退款申请单
 * @author longfc
 * @version 2017-02-20
 */
Ext.define('Shell.class.weixin.ordersys.refund.show.DetailPanel', {
	extend: 'Shell.ux.form.Panel',
	title: '退款申请单详细信息',
	width: 680,
	height: 600,
	formtype: 'show',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormById?isPlanish=false',
	/**服务附件服务路径*/
	selectAttachmentUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormAttachmentByHQL',
	/**文件下载服务路径*/
	downloadUrl: "/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DownLoadOSManagerRefundFormAttachment",
	selectOperationUrl: '/ServerWCF/ZhiFangWeiXinService.svc/SC_UDTO_SearchSCOperationByHQL?isPlanish=false',
	OperationHtml: '',
	ContentHtml: '',
	/*相关程序*/
	OriginalHtml: '',
	AttachmentHtml: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			load: function(p, data) {
				me.ContentHtml = me.HtmlContent(data.value);
				me.loadAttachmentData(function(info) {
					me.AttachmentHtml = me.HtmlAttachment(info);
				});
				me.changeHtmlContent();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = [];
		me.ContentHtml = me.getTemplet();
		me.initHtmlContent();
		me.callParent(arguments);
	},
	initHtmlContent: function() {
		var me = this;
		me.html = '';
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var fields = ['MRefundFormCode', 'Id', 'UOFCode', 'DoctorName', 'UserName', 'PayCode', 'Memo', 'DiscountPrice', 'Discount', 'Price', 'RefundPrice', 'PayTime', 'Status', 'RefundApplyManName', 'RefundApplyTime', 'RefundOneReviewManName', 'RefundOneReviewStartTime', 'RefundOneReviewFinishTime', 'RefundOneReviewReason', 'RefundTwoReviewManName', 'RefundTwoReviewStartTime', 'RefundTwoReviewFinishTime', 'RefundTwoReviewReason', 'RefundThreeReviewManName', 'RefundThreeReviewStartTime', 'RefundThreeReviewFinishTime', 'RefundThreeReviewReason', 'RefundReason', 'BankAccount', 'BankTransFormCode'];
		return fields;
	},
	HtmlContent: function(data) {
		var me = this,
			html = me.ContentHtml;
		//data = data.replace(/null/g, "");
		html = html.replace(/{MRefundFormCode}/g, data.MRefundFormCode);
		html = html.replace(/{UOFCode}/g, data.UOFCode);
		html = html.replace(/{DoctorName}/g, data.DoctorName);
		html = html.replace(/{PayCode}/g, data.PayCode); //
		html = html.replace(/{Memo}/g, data.Memo);

		html = html.replace(/{UserName}/g, data.UserName);
		html = html.replace(/{OSUserConsumerFormCode}/g, data.OSUserConsumerFormCode);
		html = html.replace(/{RefundReason}/g, data.RefundReason);
		html = html.replace(/{TransactionId}/g, data.TransactionId);
		html = html.replace(/{RefundId}/g, data.RefundId);

		html = html.replace(/{DiscountPrice}/g, data.DiscountPrice);
		html = html.replace(/{Discount}/g, data.Discount);
		html = html.replace(/{Price}/g, data.Price);
		html = html.replace(/{RefundPrice}/g, data.RefundPrice);
		html = html.replace(/{PayTime}/g, data.PayTime);

		html = html.replace(/{RefundApplyManName}/g, data.RefundApplyManName);

		html = html.replace(/{RefundApplyTime}/g, data.RefundApplyTime);

		html = html.replace(/{RefundOneReviewManName}/g, data.RefundOneReviewManName);
		html = html.replace(/{RefundOneReviewStartTime}/g, data.RefundOneReviewStartTime);
		html = html.replace(/{RefundOneReviewFinishTime}/g, data.RefundOneReviewFinishTime);
		html = html.replace(/{RefundOneReviewReason}/g, data.RefundOneReviewReason);

		html = html.replace(/{RefundTwoReviewManName}/g, data.RefundTwoReviewManName);
		html = html.replace(/{RefundTwoReviewStartTime}/g, data.RefundTwoReviewStartTime);
		html = html.replace(/{RefundTwoReviewFinishTime}/g, data.RefundTwoReviewFinishTime);
		html = html.replace(/{RefundTwoReviewReason}/g, data.RefundTwoReviewReason);

		html = html.replace(/{RefundThreeReviewManName}/g, data.RefundThreeReviewManName);
		html = html.replace(/{RefundThreeReviewStartTime}/g, data.RefundThreeReviewStartTime);
		html = html.replace(/{RefundThreeReviewFinishTime}/g, data.RefundThreeReviewFinishTime);
		html = html.replace(/{RefundThreeReviewReason}/g, data.RefundThreeReviewReason);
		html = html.replace(/null/g, "");
		html = html.replace(/undefined/g, "");
		return html;
	},
	/**获取模板*/
	getTemplet: function() {
		var templet =
			'<style type="text/css">' +
			'.dl-horizontal dt{width:95px}' +
			'.dl-horizontal dd{margin-left:5px}' +
			'</style>' +
			'<div class="col-sm-12">' +
			'<div class="wrapper wrapper-content animated fadeInUp">' +
			'<div class="ibox">' +
			'<div class="ibox-content">' +

			'<!--标题-->' +
			'<br>' +

			'<div class="col-sm-4">' +
			'<dl class="dl-horizontal">' +
			'<dt>用户姓名：</dt>' +
			'<dd>{UserName}</dd>' +
			'<dt>退费金额：</dt>' +
			'<dd>{DiscountPrice}</dd>' +

			'<dt>订单编号：</dt>' +
			'<dd>{UOFCode}</dd>' +
			'<dt>退费单号：</dt>' +
			'<dd>{MRefundFormCode}</dd>' +
			'<dt>退款处理人：</dt>' +
			'<dd>{RefundOneReviewManName}</dd>' +
			'<dt>退款审批人：</dt>' +
			'<dd>{RefundTwoReviewManName}</dd>' +
			'<dt>退款发放人：</dt>' +
			'<dd>{RefundThreeReviewManName}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-4">' +
			'<dl class="dl-horizontal">' +
			'<dt>医生姓名：</dt>' +
			'<dd>{DoctorName}</dd>' +
			'<dt>实际金额：</dt>' +
			'<dd>{Price}</dd>' +
			'<dt>消费单号：</dt>' +
			'<dd>{OSUserConsumerFormCode}</dd>' +

			'<dt>微信退款单号：</dt>' +
			'<dd>{RefundId}</dd>' +
			'<dt>处理开始时间：</dt>' +
			'<dd>{RefundOneReviewStartTime}</dd>' +
			'<dt>审批开始时间：</dt>' +
			'<dd>{RefundTwoReviewStartTime}</dd>' +
			'<dt>发放开始时间：</dt>' +
			'<dd>{RefundThreeReviewStartTime}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-4">' +
			'<dl class="dl-horizontal">' +
			'<dt>缴费时间：</dt>' +
			'<dd>{PayTime}</dd>' +
			'<dt>折扣价格：</dt>' +
			'<dd>{RefundPrice}</dd>' +

			'<dt>消费码：</dt>' +
			'<dd>{PayCode}</dd>' +
			'<dt>微信订单号：</dt>' +
			'<dd>{TransactionId}</dd>' +
			'<dt>处理完成时间：</dt>' +
			'<dd>{RefundOneReviewFinishTime}</dd>' +
			'<dt>审批完成时间：</dt>' +
			'<dd>{RefundTwoReviewFinishTime}</dd>' +
			'<dt>发放完成时间：</dt>' +
			'<dd>{RefundThreeReviewFinishTime}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
			'<dt>退费原因：</dt>' +
			'<dd>{RefundReason}</dd>' +
			'<dt>退款处理说明：</dt>' +
			'<dd>{RefundOneReviewReason}</dd>' +
			'<dt>退款审批说明：</dt>' +
			'<dd>{RefundTwoReviewReason}</dd>' +
			'<dt>退款发放说明：</dt>' +
			'<dd>{RefundThreeReviewReason}</dd>' +
			'<dt>备注：</dt>' +
			'<dd>{Memo}</dd>' +
			'</dl>' +
			'</div>' +

			'</div>' +
			'</div>' +
			'</div>' +
			'</div>' +

			'<div class="col-sm-12" >' +
			'<div style="border-top:1px solid #e0e0e0;margin:5px 0"></div>' +
			'</div>' +
			'</div>' +
			'</div>';
		return templet;
	},

	HtmlAttachment: function(attachmentInfo) {
		var me = this,
			html = me.AttachmentHtml;
		var attachmentHtml = me.getAttachmentHtml(attachmentInfo);
		html += attachmentHtml;
		return html;
	},
	/**获取附件信息*/
	loadAttachmentData: function(callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);
		var fields = [
			'Id', 'FileName', 'FileSize', 'CreatorName', 'DataAddTime', 'NewFileName'
		];
		url += "?isPlanish=false&fields=" + fields.join(",");
		var where = 'IsUse=1 and BobjectID=' + me.PK;
		url += '&where=' + where;
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	changeHtmlContent: function() {
		var me = this;
		var html = me.ContentHtml + me.AttachmentHtml + me.OperationHtml;
		me.update(html);
		me.initDownloadListeners();
	},
	/**获取附件HTML*/
	getAttachmentHtml: function(data) {
		var me = this;
		var html = me.getAttachmentTemplet();
		if(data.success) {
			var list = (data.value || {}).list || [],
				len = list.length,
				temp = me.getOneAttachmentTemplet(),
				attArr = [];
			for(var i = 0; i < len; i++) {
				var info = list[i],
					attHtml = temp;
				attHtml = attHtml.replace(/{Id}/g, info.Id);
				attHtml = attHtml.replace(/{FileName}/g, info.FileName);
				attHtml = attHtml.replace(/{NewFileName}/g, info.NewFileName);
				attHtml = attHtml.replace(/{FileSize}/g, JShell.Bytes.toSize(parseFloat(info.FileSize)));
				var Title = info.CreatorName + '创建于' +
					JShell.Date.toString(info.DataAddTime);
				attHtml = attHtml.replace(/{Title}/g, Title);
				attArr.push(attHtml);
			}
			html = html.replace(/{AttachmentList}/g, attArr.join(""));
		} else {
			var errorInfo = '<b style="color:red">' + data.msg + '</b>'
			html = html.replace(/{AttachmentList}/g, errorInfo);
		}
		return html;
	},

	/**获取附件模板*/
	getAttachmentTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>附件信息</h4>' +
			'<p style="word-break:break-all; word-wrap:break-word;">{AttachmentList}</p>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**获取一个附件模板*/
	getOneAttachmentTemplet: function() {
		var templet =
			'<div style="padding:5px;">' +
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" Title="{Title}">{NewFileName}</a> ' +
			'<span style="color:green;">({FileSize})</span>' +
			'</div>';
		return templet;
	},
	initDownloadListeners: function() {
		var me = this,
			DomArray = Ext.query("[filedownload]"),
			len = DomArray.length;
		for(var i = 0; i < len; i++) {
			DomArray[i].onclick = function() {
				var id = this.getAttribute("data");
				var Title = this.getAttribute("Title");
				me.onDwonload(id, Title);
			};
		}
	},
	onDwonload: function(id, Title) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=0&id=' + id + "&" + Title;
		window.open(url);
	}
});