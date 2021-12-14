/**
 * 文档详情信息
 * @author 
 * @version 2016-07-08
 */
Ext.define('Shell.class.qms.file.basic.FFileDetailedForm', {
	extend: 'Shell.ux.form.Panel',
	title: '详细内容信息',
	width: 500,
	height: 600,
	formtype: 'show',
	/**获取数据服务路径*/
	selectUrl: '/QMSService.svc/QMS_UDTO_SearchFFileById?isPlanish=true',
	/**服务附件服务路径*/
	selectAttachmentUrl: '/QMSService.svc/QMS_UDTO_SearchFFileAttachmentByHQL',
	/**文件下载服务路径*/
	downloadUrl: '/QMSService.svc/QMS_UDTO_FFileAttachmentDownLoadFiles',
	/**内容类型 1：文档 2：新闻 ,通知*/
	FTYPE: '1',
	selectFFileOperationUrl: '/QMSService.svc/QMS_UDTO_SearchFFileOperationByHQL',
	/**抄送范围和阅读范围*/
	FileCopyUserOrReadingUserUrl: '/QMSService.svc/QMS_UDTO_SearchFFileCopyUserOrReadingUserByFFileId',
	FFileOperationHtml: '',
	ContentHtml: '',
	FFileCopyUserHtml: '',
	UserInfoHtml: '',
	AttachmentHtml: '',
	IsCopyUserTypeValue: '',
	/**查看文档时是否需要添加文档阅读记录信息:1需要,0:不需要*/
	isAddFFileReadingLog: 1,
	/**查看文档时是否需要添加文档操作记录信息:1需要,0:不需要*/
	isAddFFileOperation: 0,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			load: function(p, data) {
				me.ContentHtml = me.HtmlContent(data.value);
				if(me.FTYPE == '1') {
					me.loadFFileOperationData(function(info) {
						me.FFileOperationHtml = me.HtmlOperationType(data.value, info);
					});
				}
				//抄送对象
				me.loadUserInfoData(1, function(info) {
					me.FFileCopyUserHtml = me.HtmlFFileCopyUser(data.value, info, me.FFileCopyUserHtml);
				});
				//阅读对象
				me.loadUserInfoData(2, function(info) {
					me.UserInfoHtml = me.HtmlFFileCopyUser(data.value, info, me.UserInfoHtml);
				});
				me.loadAttachmentData(function(info) {
					me.AttachmentHtml = me.HtmlAttachment(data.value, info);
				});
				me.changeHtmlContent();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.selectUrl = me.selectUrl + "&isAddFFileReadingLog=" + me.isAddFFileReadingLog + "&isAddFFileOperation=" + me.isAddFFileOperation;
		me.items = [];
		me.ContentHtml = me.getInfoTemplet();
		me.FFileCopyUserHtml = me.getUserTemplet('抄送范围', '抄送对象信息');
		me.UserInfoHtml = me.getUserTemplet('阅读范围', '阅读对象信息');
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
		html = html.replace(/{Title}/g, data.FFile_Title);
		html = html.replace(/{No}/g, data.FFile_No);
		var content = data.FFile_Content;
		/**
		 * 2017-04-06
		 * longfc
		 *  将内容详情下的所有的如(href="http://localhot/uploads/file/20170401/41.TXT")的信息替换为onclick="window.open('http://localhot/uploads/file/20170401/41.TXT')"
		 * 解决在原内容详情页面上直接打开附件链接时,原附件打开后的页面被跳转不能返回原内容详情页面
		 * */
		if(content) {
			var reg = new RegExp(/href="(.*?)"/g),
				match;
			while(match = reg.exec(content)) {
				if(match && match.length >= 2) {
					var tempStr = 'href="' + match[1] + '"';
					var replaceStr = 'onclick="window.open(' + "'" + match[1] + "')" + '"';
					content = content.replace(tempStr, replaceStr);
				}
			}
		}
		html = html.replace(/{Content}/g, content);
		var Type = JcallShell.QMS.Enum.FFileType[data.FFile_Type];
		html = html.replace(/{Type}/g, Type || '');
		var ContentType = JcallShell.QMS.Enum.FFileStatus[data.FFile_ContentType];
		html = html.replace(/{ContentType}/g, ContentType || '');
		var Status = JcallShell.QMS.Enum.FFileStatus[data.FFile_Status];
		html = html.replace(/{Status}/g, Status || '');
		html = html.replace(/{Keyword}/g, data.FFile_Keyword);
		html = html.replace(/{BeginTime}/g, JShell.Date.toString(data.FFile_BeginTime, true) || '');
		html = html.replace(/{EndTime}/g, JShell.Date.toString(data.FFile_EndTime, true) || '永久');
		html = html.replace(/{Summary}/g, data.FFile_Summary || '');
		html = html.replace(/{Source}/g, data.FFile_Source || '');
		html = html.replace(/{VersionNo}/g, data.FFile_VersionNo || '');
		html = html.replace(/{Pagination}/g, data.FFile_Pagination || '');
		html = html.replace(/{ReviseNo}/g, data.FFile_ReviseNo || '');
		html = html.replace(/{ReviseReason}/g, data.FFile_ReviseReason || '');
		html = html.replace(/{ReviseContent}/g, data.FFile_ReviseContent || '');
		html = html.replace(/{ReviseTime}/g, JShell.Date.toString(data.FFile_ReviseTime, true) || '');
		html = html.replace(/{NextExecutorID}/g, data.FFile_NextExecutorID || '');
		html = html.replace(/{Memo}/g, data.FFile_Memo || '');

		html = html.replace(/{CreatorName}/g, data.FFile_CreatorName || '');
		html = html.replace(/{DrafterCName}/g, data.FFile_DrafterCName || '');
		html = html.replace(/{CheckerName}/g, data.FFile_CheckerName || '');
		html = html.replace(/{ApprovalName}/g, data.FFile_ApprovalName || '');
		html = html.replace(/{PublisherName}/g, data.FFile_PublisherName || '');
		html = html.replace(/{Revisor_CName}/g, data.FFile_Revisor_CName || '');

		html = html.replace(/{DataAddTime}/g, JShell.Date.toString(data.FFile_DataAddTime) || '');
		html = html.replace(/{DrafterDateTime}/g, JShell.Date.toString(data.FFile_DrafterDateTime) || '');
		html = html.replace(/{CheckerDateTime}/g, JShell.Date.toString(data.FFile_CheckerDateTime) || '');
		html = html.replace(/{ApprovalDateTime}/g, JShell.Date.toString(data.FFile_ApprovalDateTime) || '');
		html = html.replace(/{PublisherDateTime}/g, JShell.Date.toString(data.FFile_PublisherDateTime) || '');
		html = html.replace(/{DataAddTime2}/g, JShell.Date.toString(data.FFile_DataAddTime) || ''); //有效期
		return html;
	},
	/*抄送人信息**/
	HtmlFFileCopyUser: function(data, userInfo, Html) {
		var me = this,
			html = Html;
		var Type = JcallShell.QMS.Enum.FFileCopyUserType[userInfo.value.type];
		if(userInfo.value.type != '') {
			me.IsCopyUserTypeValue = userInfo.value.type;
		}
		html = html.replace(/{Type}/g, Type || '');
		html = html.replace(/{CheckerName}/g, userInfo.value.list.cnameStr);
		return html;
	},
	HtmlOperationType: function(data, operationtypeInfo) {
		var me = this,
			html = me.FFileOperationHtml;
		var operationTypeHtml = me.getOperationTypeHtml(operationtypeInfo);
		html += operationTypeHtml;
		return html;
	},
	HtmlAttachment: function(data, attachmentInfo) {
		var me = this,
			html = me.AttachmentHtml;
		var attachmentHtml = me.getAttachmentHtml(attachmentInfo);
		html += attachmentHtml;
		return html;
	},
	changeHtmlContent: function() {
		var me = this;
		var html = me.ContentHtml + me.FFileCopyUserHtml + me.UserInfoHtml + me.AttachmentHtml + me.FFileOperationHtml;
		me.update(html);
		me.initDownloadListeners();
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var fields = [
			'Title', 'No', 'Content', 'ReviseNo', 'Revisor_CName', 'Type', 'ContentType', 'Status', 'ReviseReason',
			'Keyword', 'BeginTime', 'EndTime', 'ReviseNo', 'Summary', 'Source', 'VersionNo',
			'Pagination', 'ReviseContent', 'ReviseTime', 'NextExecutorID', 'Memo', 'Revisor',
			'CreatorName', 'DrafterCName', 'CheckerName', 'ApprovalName', 'PublisherName', 'DataAddTime',
			'DrafterDateTime', 'CheckerDateTime', 'ApprovalDateTime', 'PublisherDateTime'
		];
		var len = fields.length;
		for(var i = 0; i < len; i++) {
			fields[i] = 'FFile_' + fields[i];
		}

		return fields;
	},
	/**获取抄送人信息和阅读范围(就是阅读对象)信息*/
	loadUserInfoData: function(searchType, callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.FileCopyUserOrReadingUserUrl);
		url += "?ffileId=" + me.PK + "&searchType=" + searchType;

		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**获取附件信息*/
	loadAttachmentData: function(callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);
		var fields = [
			'FFileAttachment_Id', 'FFileAttachment_FileName', 'FFileAttachment_FileSize',
			'FFileAttachment_CreatorName', 'FFileAttachment_DataAddTime'
		];
		url += "?isPlanish=true&fields=" + fields.join(",");
		var where = 'ffileattachment.IsUse=1 and ffileattachment.FFile.Id=' + me.PK;
		url += '&where=' + where+'&sort=[{"property":"FFileAttachment_DispOrder","direction":"ASC"}]';
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	/**获取文档操作记录信息*/
	loadFFileOperationData: function(callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.selectFFileOperationUrl);

		var fields = [
			'FFileOperation_Id', 'FFileOperation_FFile_Id', 'FFileOperation_Type',
			'FFileOperation_Memo', 'FFileOperation_CreatorName', 'FFileOperation_DataAddTime'
		];
		url += "?isPlanish=true&fields=" + fields.join(",");

		var where = 'ffileoperation.IsUse=1 and ffileoperation.FFile.Id=' + me.PK;
		url += '&where=' + where + '&sort=[{"property":"FFileOperation_FFile_Status","direction":"ASC"},{"property":"FFileOperation_DataAddTime","direction":"ASC"}]';
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
				attHtml = attHtml.replace(/{Id}/g, info.FFileAttachment_Id);
				attHtml = attHtml.replace(/{FileName}/g, info.FFileAttachment_FileName);
				attHtml = attHtml.replace(/{FileSize}/g, JShell.Bytes.toSize(parseFloat(info.FFileAttachment_FileSize)));
				var Title = info.FFileAttachment_CreatorName + '创建于' +
					JShell.Date.toString(info.FFileAttachment_DataAddTime);
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

	/**获取新闻,通知模板*/
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
	/**获取信息HTML模板*/
	getInfoTemplet: function() {

		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';

		var templet =

			'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{Title}</h4>' +
			'</div>' +
			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>编号：{No}</b></div>' +
			'<div style="' + sDivStyle + '">版本号：{VersionNo}</div>' +
			'<div style="' + sDivStyle + '">文档来源：{Source}</div>' +
			'<div style="' + sDivStyle + '">页码：{Pagination}</div>' +
			'<div style="' + sDivStyle + '">状态：{Status}</div>' +
			'</div>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>提交人：{DrafterCName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>提交时间：{DrafterDateTime}</b></div>' +
			'<div style="' + sDivStyle + '"><b>审核人：{CheckerName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>审核时间：{CheckerDateTime}</b></div>' +
			'<div style="' + sDivStyle + '"><b>审批人：{ApprovalName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>审批时间：{ApprovalDateTime}</b></div>' +
			'<div style="' + sDivStyle + '"><b>发布人：{PublisherName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>发布时间：{PublisherDateTime}</b></div>' +

			'</div>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '"><b>修订人：{Revisor_CName}</b></div>' +
			'<div style="' + sDivStyle + '"><b>修订时间：{ReviseTime}</b></div>' +
			'<div style="' + sDivStyle + '"><b>修订号：{ReviseNo}</b></div>' +
			'<div style="' + sDivStyle + '"><b>修订日期：{ReviseTime}</b></div>' +
			'<div style="' + sDivStyle + '"><b>修订原因：{ReviseReason}</b></div>' +

			'</div>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			'<div style="' + sDivStyle + '">文摘：{Summary}</div>' +
			'<div style="' + sDivStyle + '">关键字：{Keyword}</div>' +
			'</div>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">概要信息</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{Memo}</p>' +
			'</div>' +

			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">详细信息</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{Content}</p>' +
			'<p style="padding:5px 5px 0px 16px; word-break:break-all; word-wrap:break-word;">创建于&nbsp;{DataAddTime2}&nbsp;有效期:&nbsp;{BeginTime}&nbsp;-&nbsp;{EndTime}</p>' +
			'</div>';

		return templet;
	},
	/**获取文档操作记录的操作类型模板*/
	getOperationTypeTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>文档审核记录</h4>' +
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
	/**获取抄送人信息和阅读范围(就是阅读对象)信息*/
	getUserTemplet: function(text, ptext) {
		var templet =
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>' + ptext + '</h4>' +
			'</div>' +
			'<ul >' +
			'<p class="small"><li><span>&nbsp;&nbsp;&nbsp;&nbsp;类型:&nbsp; {Type} &nbsp;   &nbsp;  &nbsp; ' + text + ':&nbsp; {CheckerName} &nbsp;</span></li></p>' +
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
		url += '?operateType=1&id=' + id+ "&" + title;
        var suffix='',beforeV='',openUrl='';
        if(title){
	        var index = title.lastIndexOf(".");  
	        suffix = title.substring(index, title.length);
	        beforeV = title.substring(0, index);
	        suffix=suffix.toLowerCase();
        }
		window.open(url);
	},
	 /**判断是否是火狐浏览器*/
    isBrowser:function(){
    	var me =this;
    	var browser=true;
    	if(isFirefox=navigator.userAgent.indexOf("Firefox")>0){ 
    		browser=false;
		}  
	    return browser;
    }
});