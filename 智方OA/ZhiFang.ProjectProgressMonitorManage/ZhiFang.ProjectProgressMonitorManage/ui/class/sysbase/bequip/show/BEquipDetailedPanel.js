/**
 * 仪器详情信息
 * @author longfc
 * @version 2016-10-08
 */
Ext.define('Shell.class.sysbase.bequip.show.BEquipDetailedPanel', {
	extend: 'Shell.ux.form.Panel',
	title: '仪器详情信息',
	width: 500,
	height: 600,
	formtype: 'show',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBEquipById?isPlanish=true',
	/**服务附件服务路径*/
	selectAttachmentUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCAttachmentByHQL',
	/**文件下载服务路径*/
	downloadUrl: "/SystemCommonService.svc/SC_UDTO_DownLoadSCAttachment",
	selectFFileOperationUrl: '',
	FFileOperationHtml: '',
	ContentHtml: '',
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
		me.selectUrl = me.selectUrl;
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

		html = html.replace(/{CName}/g, data.BEquip_CName);
		html = html.replace(/{EquipType_CName}/g, data.BEquip_EquipType_CName);
		html = html.replace(/{EquipFactoryBrand_CName}/g, data.BEquip_EquipFactoryBrand_CName);
		html = html.replace(/{Equipversion}/g, data.BEquip_Equipversion);

		html = html.replace(/{PinYinZiTou}/g, data.BEquip_PinYinZiTou);
		html = html.replace(/{EName}/g, data.BEquip_EName);
		html = html.replace(/{SName}/g, data.BEquip_SName);
		html = html.replace(/{Shortcode}/g, data.BEquip_Shortcode);
		html = html.replace(/{UseCode}/g, data.BEquip_UseCode);

		html = html.replace(/{FullCName}/g, data.BEquip_FullCName);
		html = html.replace(/{Comment}/g, data.BEquip_Comment);
		html = html.replace(/{Memo}/g, data.BEquip_Memo);
		html = html.replace(/{Content}/g, data.BEquip_Content);

		html = html.replace(/{DataAddTime}/g, data.BEquip_DataAddTime);
		return html;
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var fields = [
			'CName', 'EName', 'SName', 'Shortcode', 'PinYinZiTou', 'UseCode', 'Comment', 'FullCName', 'EquipType_CName', 'Equipversion', 'EquipFactoryBrand_CName', 'Content', 'Memo', 'DataAddTime'
		];
		var len = fields.length;
		for(var i = 0; i < len; i++) {
			fields[i] = 'BEquip_' + fields[i];
		}

		return fields;
	},

	HtmlOperationType: function(operationtypeInfo) {
		var me = this,
			html = me.FFileOperationHtml;
		var operationTypeHtml = me.getOperationTypeHtml(operationtypeInfo);
		html += operationTypeHtml;
		return html;
	},
	HtmlAttachment: function(attachmentInfo) {
		var me = this,
			html = me.AttachmentHtml;
		var attachmentHtml = me.getAttachmentHtml(attachmentInfo);
		html += attachmentHtml;
		return html;
	},
	changeHtmlContent: function() {
		var me = this;
		var html = me.ContentHtml + me.AttachmentHtml + me.FFileOperationHtml;
		me.update(html);
		me.initDownloadListeners();
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},

	/**获取附件信息*/
	loadAttachmentData: function(callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);
		var fields = [
			'SCAttachment_Id', 'SCAttachment_FileName', 'SCAttachment_FileSize',
			'SCAttachment_CreatorName', 'SCAttachment_DataAddTime'
		];
		url += "?isPlanish=true&fields=" + fields.join(",");
		var where = 'scattachment.IsUse=1 and scattachment.BobjectID=' + me.PK;
		url += '&where=' + where;
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
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
				attHtml = attHtml.replace(/{Id}/g, info.SCAttachment_Id);
				attHtml = attHtml.replace(/{FileName}/g, info.SCAttachment_FileName);
				attHtml = attHtml.replace(/{FileSize}/g, JShell.Bytes.toSize(info.SCAttachment_FileSize));
				var Title = info.SCAttachment_CreatorName + '创建于' +
					JShell.Date.toString(info.SCAttachment_DataAddTime);
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
	/**获取附件HTML*/
	getOperationTypeHtml: function(data) {
		var me = this;
		var html = me.getOperationTypeTemplet();
		if(data.success) {
			var list = (data.value || {}).list || [],
				len = list.length,
				temp = me.getOneOperationTypeTemplet(),
				attArr = [];
			for(var i = 0; i < len; i++) {
				var info = list[i],
					attHtml = temp;
				var Type = JcallShell.QMS.Enum.FFileOperationType[info.FFileOperation_Type];
				var OperationTypeColor = JShell.QMS.Enum.FFileOperationTypeColor[info.FFileOperation_Type] || '#FFFFFF';
				attHtml = attHtml.replace(/{Id}/g, info.FFileOperation_Id);
				attHtml = attHtml.replace(/{Type}/g, Type || '');
				attHtml = attHtml.replace(/{DataAddTime}/g, info.FFileOperation_DataAddTime);
				attHtml = attHtml.replace(/{CreatorName}/g, info.FFileOperation_CreatorName);
				attHtml = attHtml.replace(/{OperationTypeColor}/g, OperationTypeColor);
				attArr.push(attHtml);
			}
			html = html.replace(/{FFileOperationType}/g, attArr.join(""));
		} else {
			var errorInfo = '<b style="color:red">' + data.msg + '</b>'
			html = html.replace(/{FFileOperationType}/g, errorInfo);
		}
		return html;
	},

	/**获取仪器模板*/
	getNewTemplet: function() {
		var templet =
			'<style type="text/css">' +
			'.dl-horizontal dt{width:90px}' +
			'.dl-horizontal dd{margin-left:100px}' +
			'</style>' +
			'<br>' +
			'<div class="col-sm-12">' +
			'<div class="wrapper wrapper-content animated fadeInUp">' +
			'<div class="ibox">' +
			'<div class="ibox-content">' +
			'<!--标题+状态-->' +
			'<div class="row">' +
			'<div class="col-sm-12">' +
			'<p style="word-break:break-all; word-wrap:break-word;">{Content}</p>' +
			'</div>' +
			'</div>' +
			'</div>' +
			'</div>' +
			'</div>' +
			'</div>';
		return templet;
	},

	/**获取文档模板*/
	getTemplet: function() {
		var templet =
			'<style type="text/css">' +
			'.dl-horizontal dt{width:85px}' +
			'.dl-horizontal dd{margin-left:50px}' +
			'</style>' +
			'<div class="col-sm-12">' +
			'<div class="wrapper wrapper-content animated fadeInUp">' +
			'<div class="ibox">' +
			'<div class="ibox-content">' +
			'<!--标题+状态-->' +
			'<div class="row">' +
			'<div class="col-sm-12">' +
			'<div class="m-b-md">' +
			'</div>' +
			'</div>' +
			'</div>' +
			'<br>' +

			'<div class="row">' +
			'<div class="col-sm-3">' +
			'<dl class="dl-horizontal">' +
			'<dt>仪器名称：</dt>' +
			'<dd>{CName}</dd>' +
			'<dt>SQH号：</dt>' +
			'<dd>{Shortcode}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-3">' +
			'<dl class="dl-horizontal">' +
			'<dt>仪器分类：</dt>' +
			'<dd> {EquipType_CName}</dd>' +
			'<dt>简称：</dt>' +
			'<dd>{SName}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-3" id="cluster_info">' +
			'<dl class="dl-horizontal">' +
			'<dt>仪器品牌：</dt>' +
			'<dd>{EquipFactoryBrand_CName}</dd>' +
			'<dt>代码：</dt>' +
			'<dd>{UseCode}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-3" id="cluster_info">' +
			'<dl class="dl-horizontal">' +
			'<dt>仪器型号：</dt>' +
			'<dd>{Equipversion}</dd>' +
			'<dt>加入时间：</dt>' +
			'<dd>{DataAddTime}</dd>' +
			'</dl>' +
			'</div>' +
			'</div>' +

			'<div class="row">' +
			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
			'<dt>仪器全称：</dt>' +
			'<dd>{FullCName}</dd>' +

			'</dl>' +
			'</div>' +
			'</div>' +
			'</div>' +

			'<div class="row">' +
			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
			'<dt>概要说明：</dt>' +
			'<dd>{Memo}</dd>' +
			'<dt>描述：</dt>' +
			'<dd>{Comment}</dd>' +

			'</dl>' +
			'</div>' +
			'</div>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>详细说明</h4>' +
			'</div>' +
			'<ul >' +
			'<p style="padding:0px 5px 0px 16px; word-break:break-all; word-wrap:break-word;">{Content}</p>' +
			'<p style="padding:5px 5px 0px 16px; word-break:break-all; word-wrap:break-word;">创建于&nbsp;{DataAddTime}&nbsp;</p>' +
			'</ul>' +
			'</div>' +

			'<br>' +
			'<div class="col-sm-12" >' +
			'<div style="border-top:1px solid #e0e0e0;margin:5px 0"></div>' +
			'</div>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**操作记录*/
	getOperationTypeTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>操作记录</h4>' +
			'<p class="small">{FFileOperationType}</p>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**获取一个附件模板*/
	getOneOperationTypeTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<ul >' +
			'<li><span style="font-weight:bold;">{DataAddTime} &nbsp; {CreatorName} &nbsp; <span style="color:{OperationTypeColor};">{Type}</span></span></li>' +
			'</ul>' +
			'</div>';
		return templet;
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
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" title="{FileName}">{FileName}</a> ' +
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
				var title = this.getAttribute("title");
				me.onDwonload(id, title);
			};
		}
	},
	onDwonload: function(id, title) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=0&id=' + id + "&" + title;
		window.open(url);
	}

});