/**
 * 发票说明
 * @author 
 * @version 2016-07-08
 */
Ext.define('Shell.class.wfm.business.invoice.basic.MemoPanel', {
	extend: 'Shell.ux.form.Panel',
	title: '发票说明',
	width: 500,
	height: 600,
	formtype: 'show',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPInvoiceById?isPlanish=true',
	ContentHtml: '',
	PK: '',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			load: function(p, data) {
				me.ContentHtml = me.HtmlContent(data.value);
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
	HtmlContent: function(data) {
		var me = this,
			html = me.ContentHtml;
		html = html.replace(/{Comment}/g, data.PInvoice_Comment || '<div style="color:freen;text-align:center;margin:20px 10px;font-weight:bold;">无</div>');
		return html;
	},
	changeHtmlContent: function() {
		var me = this;
		var html = me.ContentHtml ;
		me.update(html);
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var fields = [
			'Comment'
		];
		var len = fields.length;
		for(var i = 0; i < len; i++) {
			fields[i] = 'PInvoice_' + fields[i];
		}
		return fields;
	},
	/**获取文档模板*/
	getTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<div>' +
//			'<h4>操作记录</h4>' +
			'<p style="word-break:break-all; word-wrap:break-word;">{Comment}</p>' +
			'</div>' +
			'</div>';
		return templet;
	}
});