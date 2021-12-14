/**
 * 程序内容详细信息
 * @author longfc
 * @version 2016-09-30
 */
Ext.define('Shell.class.oa.pgm.program.show.DetailedContent', {
	extend: 'Shell.ux.form.Panel',
	title: '程序内容详细信息',
	width: 680,
	height: 600,
	formtype: 'show',
	/**获取数据服务路径*/
	selectUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramById?isPlanish=true',

	/**文件下载服务路径*/
	downloadUrl: "/PDProgramManageService.svc/PGM_UDTO_DownLoadPGMProgramAttachment",
	selectFFileOperationUrl: '',
	FFileOperationHtml: '',
	ContentHtml: '',
	/*相关程序*/
	OriginalHtml: '',
	AttachmentHtml: '',
	OriginalAttachmentHtml: '',
	/*是否更新总阅读数*/
	isUpdateCounts: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			load: function(p, data) {
				me.ContentHtml = me.HtmlContent(data.value);
				me.AttachmentHtml = me.HtmlAttachment(data.value);
				//相关程序
				me.OriginalAttachmentHtml = me.HtmlOriginalAttachment(data.value);
				me.changeHtmlContent();
			}
		});

	},
	initComponent: function() {
		var me = this;
		me.selectUrl = me.selectUrl + "&isUpdateCounts=" + me.isUpdateCounts;
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

		html = html.replace(/{Title}/g, data.PGMProgram_Title);
		html = html.replace(/{No}/g, data.PGMProgram_No);
		html = html.replace(/{VersionNo}/g, data.PGMProgram_VersionNo);
		html = html.replace(/{Counts}/g, data.PGMProgram_Counts);

		html = html.replace(/{BEquip_CName}/g, data.PGMProgram_BEquip_CName);
		html = html.replace(/{EquipType_CName}/g, data.PGMProgram_BEquip_EquipType_CName);
		html = html.replace(/{EquipFactoryBrand_CName}/g, data.PGMProgram_BEquip_EquipFactoryBrand_CName);
		html = html.replace(/{BEquip_Equipversion}/g, data.PGMProgram_BEquip_Equipversion);

		html = html.replace(/{ClientName}/g, data.PGMProgram_ClientName);
		html = html.replace(/{OtherFactoryName}/g, data.PGMProgram_OtherFactoryName);
		html = html.replace(/{SQH}/g, data.PGMProgram_SQH);
		html = html.replace(/{Keyword}/g, data.PGMProgram_Keyword);
		html = html.replace(/{Memo}/g, data.PGMProgram_Memo);

		html = html.replace(/{Content}/g, data.PGMProgram_Content);
		html = html.replace(/{DataAddTime}/g, data.PGMProgram_DataAddTime);
		html = html.replace(/{PublisherDateTime}/g, data.PGMProgram_PublisherDateTime);
		html = html.replace(/{PublisherName}/g, data.PGMProgram_PublisherName);

		return html;
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var fields = ['SQH', 'Id', 'Title', 'No', 'VersionNo', 'Keyword', 'Memo', 'Type', 'Status', 'DispOrder', 'PBDictTree_Id', 'SubBDictTree_Id', 'OriginalPGMProgram_Id', 'Content', 'PublisherDateTime', 'DataAddTime', 'ClientName', 'OtherFactoryName', 'IsDiscuss', 'FileName', 'Size', 'FilePath', 'NewFileName', 'FileExt', 'FileType', 'Counts', 'BEquip_CName', 'BEquip_Equipversion', 'BEquip_EquipFactoryBrand_CName', 'BEquip_EquipType_CName', 'PublisherName'];
		var len = fields.length;
		for(var i = 0; i < len; i++) {
			fields[i] = 'PGMProgram_' + fields[i];
		}

		return fields;
	},

	HtmlAttachment: function(attachmentInfo) {
		var me = this,
			html = me.AttachmentHtml;
		var attachmentHtml = me.getAttachmentHtml(attachmentInfo);
		html += attachmentHtml;
		return html;
	},
	HtmlOriginalAttachment: function(attachmentInfo) {
		var me = this,
			html = me.OriginalAttachmentHtml;
		me.getOriginalAttachmentHtml(attachmentInfo);
		html += me.OriginalHtml;
		return html;
	},
	changeHtmlContent: function() {
		var me = this;
		var html = me.ContentHtml + me.AttachmentHtml + me.OriginalAttachmentHtml + me.FFileOperationHtml;
		me.update(html);
		me.initDownloadListeners();
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**获取附件HTML*/
	getOriginalAttachmentHtml: function(data) {
		var me = this;
		var html = me.getOriginalAttachmentTemplet();

		var priginalId = "",
			isShowNull = false;

		if(data) {
			priginalId = data.PGMProgram_OriginalPGMProgram_Id;
		} else {
			isShowNull = true;
		}
		if(priginalId != "" && isShowNull == false) {
			var url = JShell.System.Path.getRootUrl("/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramById?isPlanish=true&isUpdateCounts=false");
			var fields = me.getStoreFields();
			url += "&fields=" + fields.join(",");
			//var where = 'pgmprogram.Id=' + priginalId;
			url += '&id=' + priginalId + '';

			JShell.Server.get(url, function(result) {
				var data = result.value;
				if(data) {
					var temp = me.getOneOriginalAttachmentTemplet(),
						attArr = [];
					var attHtml = temp;
					attHtml = attHtml.replace(/{Id}/g, data.PGMProgram_Id);
					attHtml = attHtml.replace(/{FileName}/g, data.PGMProgram_FileName);
					attHtml = attHtml.replace(/{NewFileName}/g, data.PGMProgram_NewFileName);
					var size = "";
					if(data.PGMProgram_Size != "") {
						size = JShell.Bytes.toSize(parseFloat(data.PGMProgram_Size));
					}
					attHtml = attHtml.replace(/{FileSize}/g, size);
					var Title = data.PGMProgram_NewFileName + '创建于' +
						JShell.Date.toString(data.PGMProgram_DataAddTime);
					attHtml = attHtml.replace(/{Title}/g, Title);
					attArr.push(attHtml);
					html = html.replace(/{AttachmentList}/g, attArr.join(""));
				} else {
					isShowNull = true;
				}
				me.OriginalHtml = html;
				//return html;
			}, false);

		} else {
			isShowNull = true;
		}
		if(isShowNull) {
			var errorInfo = '<b style="color:red">无相关程序信息 !</b>'
			html = html.replace(/{AttachmentList}/g, errorInfo);
			me.OriginalHtml = html;
		}
	},
	/**获取附件HTML*/
	getAttachmentHtml: function(data) {
		var me = this;
		var html = me.getAttachmentTemplet();
		if(data) {
			temp = me.getOneAttachmentTemplet(),
				attArr = [];
			var attHtml = temp;
			attHtml = attHtml.replace(/{Id}/g, data.PGMProgram_Id);
			attHtml = attHtml.replace(/{FileName}/g, data.PGMProgram_FileName);
			attHtml = attHtml.replace(/{NewFileName}/g, data.PGMProgram_NewFileName);

			var size = "";
			if(data.PGMProgram_Size != "") {
				size = JShell.Bytes.toSize(parseFloat(data.PGMProgram_Size));
			}
			attHtml = attHtml.replace(/{FileSize}/g, size);
			var Title = data.PGMProgram_NewFileName + '创建于' +
				JShell.Date.toString(data.PGMProgram_DataAddTime);
			attHtml = attHtml.replace(/{Title}/g, Title);
			attArr.push(attHtml);

			html = html.replace(/{AttachmentList}/g, attArr.join(""));
		} else {
			var errorInfo = '<b style="color:red">' + data.msg + '</b>'
			html = html.replace(/{AttachmentList}/g, errorInfo);
		}
		return html;
	},

	/**获取模板*/
	getTemplet: function() {
		var templet =
			'<style type="text/css">' +
			'.dl-horizontal dt{width:65px}' +
			'.dl-horizontal dd{margin-left:10px}' +
			'</style>' +
			'<div class="col-sm-12">' +
			'<div class="wrapper wrapper-content animated fadeInUp">' +
			'<div class="ibox">' +
			'<div class="ibox-content">' +

			'<!--标题-->' +
			'<br>' +

			'<div class="row">' +
			'<div class="col-sm-3">' +
			'<dl class="dl-horizontal">' +
			'<dt>程序标题：</dt>' +
			'<dd>{Title}</dd>' +
			'<dt>用户名称：</dt>' +
			'<dd>{ClientName}</dd>' +
			'<dt>仪器：</dt>' +
			'<dd>{BEquip_CName}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-3">' +
			'<dl class="dl-horizontal">' +
			'<dt>程序编号：</dt>' +
			'<dd>{No}</dd>' +
			'<dt>厂家名称：</dt>' +
			'<dd>{OtherFactoryName}</dd>' +
			'<dt>仪器分类：</dt>' +
			'<dd>{EquipType_CName}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-3">' +
			'<dl class="dl-horizontal">' +
			'<dt>版本号：</dt>' +
			'<dd>{VersionNo}</dd>' +
			'<dt>发布人：</dt>' +
			'<dd>{PublisherName}</dd>' +
			'<dt>仪器品牌：</dt>' +
			'<dd>{EquipFactoryBrand_CName}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-3">' +
			'<dl class="dl-horizontal">' +
			'<dt>总阅读数：</dt>' +
			'<dd>{Counts}</dd>' +
			'<dt>发布时间：</dt>' +
			'<dd>{PublisherDateTime}</dd>' +
			'<dt>仪器型号：</dt>' +
			'<dd>{BEquip_Equipversion}</dd>' +
			'</dl>' +
			'</div>' +

			'</div>' +

			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
			'<dt>授权号：</dt>' +
			'<dd>{SQH}</dd>' +
			'</dl>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
			'<dt>关键字：</dt>' +
			'<dd>{Keyword}</dd>' +
			'</dl>' +
			'</div>' +

			'</div>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
			'<dt>概要说明：</dt>' +
			'<dd>{Memo}</dd>' +
			'</dl>' +
			'</div>' +
			'</div>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<dl class="dl-horizontal">' +
			'<dt>详细说明：</dt>' +
			'<dd>{Content}</dd>' +
			'<dt>创建于：</dt>' +
			'<dd>{DataAddTime}</dd>' +
			'</dl>' +
			'</div>' +
			'</div>' +
			'</div>' +

			'<br>' +
			'<div class="col-sm-12" >' +
			'<div style="border-top:1px solid #e0e0e0;margin:5px 0"></div>' +
			'</div>' +
			'</div>' +
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
	/**获取附件模板*/
	getOriginalAttachmentTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>相关程序</h4>' +
			'<p style="word-break:break-all; word-wrap:break-word;">{AttachmentList}</p>' +
			'</div>' +
			'</div>';
		return templet;
	},
	/**获取一个附件模板*/
	getOneOriginalAttachmentTemplet: function() {
		var templet =
			'<div style="padding:5px;">' +
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" title="{NewFileName}">{NewFileName}</a> ' +
			'<span style="color:green;">({FileSize})</span>' +
			'</div>';
		return templet;
	},
	/**获取一个附件模板*/
	getOneAttachmentTemplet: function() {
		var templet =
			'<div style="padding:5px;">' +
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" title="{NewFileName}">{NewFileName}</a> ' +
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