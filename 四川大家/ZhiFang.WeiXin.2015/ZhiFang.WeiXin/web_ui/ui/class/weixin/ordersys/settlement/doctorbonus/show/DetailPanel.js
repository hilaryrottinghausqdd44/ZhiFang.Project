/**
 *医生奖金结算单详细信息
 * @author longfc
 * @version 2017-03-02
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.show.DetailPanel', {
	extend: 'Shell.ux.form.Panel',
	title: '医生奖金结算单信息',
	width: 680,
	height: 600,
	formtype: 'show',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusFormById?isPlanish=false',
	/**服务附件服务路径*/
	selectAttachmentUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusAttachmentByHQL',
	/**文件下载服务路径*/
	downloadUrl: "/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DownLoadOSDoctorBonusAttachment",
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
		var fields = ['DoctorCount', 'Id', 'BonusApplytTime', 'OrderFormCount', 'BonusFormRound', 'Memo', 'BonusApplyManName', 'Discount', 'Amount', 'RefundPrice', 'Status', 'RefundApplyManName', 'RefundApplyTime', 'BonusOneReviewManName', 'BonusOneReviewStartTime', 'BonusOneReviewFinishTime', 'BonusTwoReviewManName', 'BonusTwoReviewStartTime', 'BonusTwoReviewFinishTime', 'BonusThreeReviewManName', 'BonusThreeReviewStartTime', 'BonusThreeReviewFinishTime','StatusName'];
		return fields;
	},
	HtmlContent: function(data) {
		var me = this,
			html = me.ContentHtml;
		//data = data.replace(/null/g, "");
		html = html.replace(/{DoctorCount}/g, data.DoctorCount);
		html = html.replace(/{BonusApplytTime}/g, data.BonusApplytTime);
		html = html.replace(/{OrderFormCount}/g, data.OrderFormCount);
		html = html.replace(/{Memo}/g, data.Memo);

		html = html.replace(/{BonusFormRound}/g, data.BonusFormRound);
		html = html.replace(/{OrderFormAmount}/g, data.OrderFormAmount);
		html = html.replace(/{BonusFormCode}/g, data.BonusFormCode);
		html = html.replace(/{StatusName}/g, data.StatusName);
		html = html.replace(/{BonusApplyManName}/g, data.BonusApplyManName);
		html = html.replace(/{Discount}/g, data.Discount);
		html = html.replace(/{Amount}/g, data.Amount);
		html = html.replace(/{RefundPrice}/g, data.RefundPrice);

		html = html.replace(/{RefundApplyManName}/g, data.RefundApplyManName);

		html = html.replace(/{RefundApplyTime}/g, data.RefundApplyTime);

		html = html.replace(/{BonusOneReviewManName}/g, data.BonusOneReviewManName);
		html = html.replace(/{BonusOneReviewStartTime}/g, data.BonusOneReviewStartTime);
		html = html.replace(/{BonusOneReviewFinishTime}/g, data.BonusOneReviewFinishTime);

		html = html.replace(/{BonusTwoReviewManName}/g, data.BonusTwoReviewManName);
		html = html.replace(/{BonusTwoReviewStartTime}/g, data.BonusTwoReviewStartTime);
		html = html.replace(/{BonusTwoReviewFinishTime}/g, data.BonusTwoReviewFinishTime);

		html = html.replace(/{BonusThreeReviewManName}/g, data.BonusThreeReviewManName);
		html = html.replace(/{BonusThreeReviewStartTime}/g, data.BonusThreeReviewStartTime);
		html = html.replace(/{BonusThreeReviewFinishTime}/g, data.BonusThreeReviewFinishTime);
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
			'<dt>结算周期：</dt>' +
			'<dd>{BonusFormRound}</dd>' +
			'<dt>开单数量：</dt>' +
			'<dd>{OrderFormCount}</dd>' +
			'<dt>医生数量：</dt>' +
			'<dd>{DoctorCount}</dd>' +
			'<dt>处理人：</dt>' +
			'<dd>{BonusOneReviewManName}</dd>' +
			'<dt>审批人：</dt>' +
			'<dd>{BonusTwoReviewManName}</dd>' +
			'<dt>发放人：</dt>' +
			'<dd>{BonusThreeReviewManName}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-4">' +
			'<dl class="dl-horizontal">' +

			'<dt>申请人：</dt>' +
			'<dd>{BonusApplyManName}</dd>' +
			'<dt>结算金额：</dt>' +
			'<dd>{Amount}</dd>' +
			'<dt>开单金额：</dt>' +
			'<dd>{OrderFormAmount}</dd>' +
			'<dt>处理开始时间：</dt>' +
			'<dd>{BonusOneReviewStartTime}</dd>' +
			'<dt>审批开始时间：</dt>' +
			'<dd>{BonusTwoReviewStartTime}</dd>' +
			'<dt>发放开始时间：</dt>' +
			'<dd>{BonusThreeReviewStartTime}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-4">' +
			'<dl class="dl-horizontal">' +
			'<dt>申请时间：</dt>' +
			'<dd>{BonusApplytTime}</dd>' +
			'<dt>结算单号：</dt>' +
			'<dd>{BonusFormCode}</dd>' +
			'<dt>状态：</dt>' +
			'<dd>{StatusName}</dd>' +
			'<dt>处理完成时间：</dt>' +
			'<dd>{BonusOneReviewFinishTime}</dd>' +
			'<dt>审批完成时间：</dt>' +
			'<dd>{BonusTwoReviewFinishTime}</dd>' +
			'<dt>发放完成时间：</dt>' +
			'<dd>{BonusThreeReviewFinishTime}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
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
			'Id', 'FileName', 'FileSize', 'CreatorName', 'DataAddTime','NewFileName'
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
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" Title="{NewFileName}">{NewFileName}</a> ' +
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