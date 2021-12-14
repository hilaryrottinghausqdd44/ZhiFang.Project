/**
 * 历史修订记录页签
 * @author liangyl
 * @version 2016-07-08
 */
Ext.define('Shell.class.qms.file.basic.HistoryForm', {
	extend: 'Shell.ux.form.Panel',
	title: '历史修订记录',
	width: 500,
	height: 600,
	formtype: 'add',
    selectUrl: '/QMSService.svc/QMS_UDTO_SearchReviseFFileListByFileID?isPlanish=true',
	AttachmentHtml: '',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		//初始化检索监听
		me.on({
			load: function(p, data) {
				me.AttachmentHtml = me.HtmlAttachment(data);
				me.changeHtmlContent();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = [];
		me.selectUrl += "&fileId=" + me.PK;
		me.initHtmlContent();
		me.callParent(arguments);
	},
	initHtmlContent: function() {
		var me = this;
		me.html = '';
	},
	HtmlAttachment: function( attachmentInfo) {
		var me = this,
			html = me.AttachmentHtml;
		var attachmentHtml = me.getAttachmentHtml(attachmentInfo);
		html += attachmentHtml;
		return html;
	},
	changeHtmlContent: function() {
		var me = this;
		var html =  me.AttachmentHtml;
		me.update(html);
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var fields = [
			'Title','Memo', 'ReviseTime','ReviseNo','VersionNo'
		];
		var len = fields.length;
		for(var i = 0; i < len; i++) {
			fields[i] = 'FFile_' + fields[i];
		}
		return fields;
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
					
				attHtml = attHtml.replace(/{Title}/g, info.FFile_Title);
				attHtml = attHtml.replace(/{Memo}/g, info.FFile_Memo);
				attHtml = attHtml.replace(/{ReviseTime}/g, info.FFile_ReviseTime);
				attHtml = attHtml.replace(/{ReviseNo}/g, info.FFile_ReviseNo);
				attHtml = attHtml.replace(/{VersionNo}/g, info.FFile_VersionNo);

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
			'<p style="word-break:break-all; word-wrap:break-word;">{AttachmentList}</p>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**获取一个附件模板*/
	getOneAttachmentTemplet: function() {
		  //行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';
		
		var templet =
			'<div class="col-sm-12">' +
//			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;"></h4>' +
//				'<div style="' + rDivStyle + '">' +
                '<div><blockquote style="margin-bottom: 10px;padding: 15px;line-height: 22px;border-left: 5px solid #009688;border-radius: 0 2px 2px 0;background-color: #f2f2f2;">' +
//					'<div style="' + sDivStyle + '"><b>版本号：{VersionNo}</b></div>' +
//					'<div style="' + sDivStyle + '">修订内容：{Memo}</div>' +
					'<div style="' + sDivStyle + '"><b>文档标题：{Title}</b></div>' +
					'<div style="' + sDivStyle + '"><b>版本号：{VersionNo}</b></div>' +
					'<div style="' + sDivStyle + '"><b>修订时间：{ReviseTime}</b></div>' +
					'<div style="' + sDivStyle + '"><b>修订号：{ReviseNo}</b></div>' +
					'<br/><p><div style="padding:5px; margin-right:10px;">修订内容：{Memo}</div>' +
					'<br/>'+
//				'</div>' +
                '</blockquote></div>' +
			'</div>';
		
		return templet;
	}
	
});