/**
 * 文档内容详细信息
 * @author 
 * @version 2016-07-08
 */
Ext.define('Shell.class.qms.file.basic.DetailedContent', {
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
	/**是否显示文档审核记录，1：显示，其他不显示*/
	//  RoleType:'1',
	selectFFileOperationUrl: '/QMSService.svc/QMS_UDTO_SearchFFileOperationByHQL',
	AttachmentHtml: '',
	ContentHtml: '',
	/**查看文档时是否需要添加文档阅读记录信息:1需要,0:不需要*/
	isAddFFileReadingLog: 1,
	/**查看文档时是否需要添加文档操作记录信息:1需要,0:不需要*/
	isAddFFileOperation: 0,
	/**PDF预览0下载按钮显示，1不显示*/
	DOWNLOAD:'',
	/**PDF预览0打印按钮显示，1不显示*/
	PRINT:'',
	/**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
    BUILTIN:'1',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			load: function(p, data) {
				me.ContentHtml = me.HtmlContent(data.value);
				me.loadAttachmentData(function(info) {
					me.AttachmentHtml = me.HtmlAttachment(data.value, info);
				});
				me.changeHtmlContent();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = [];
		me.selectUrl = me.selectUrl + "&isAddFFileReadingLog=" + me.isAddFFileReadingLog + "&isAddFFileOperation=" + me.isAddFFileOperation;

		//HTML模板
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
		html = html.replace(/{Title}/g, data.FFile_Title);

	    var content = data.FFile_Content;
		/**
		 * 2017-04-06
		 * longfc
		 *  将内容详情下的所有的如(href="http://localhot/uploads/file/20170401/41.TXT")的信息替换为onclick="window.open('http://localhot/uploads/file/20170401/41.TXT')"
		 * 解决在原内容详情页面上直接打开附件链接时,原附件打开后的页面被跳转不能返回原内容详情页面
		 * */
		if(content) {
			var reg = new RegExp(/href="(.*?)"/g),match;
			while(match = reg.exec(content)) {
				if(match&&match.length>=2) {
					var tempStr = 'href="' + match[1] + '"';
					var replaceStr = 'onclick="window.open(' + "'" + match[1] + "')" + '"';
					content = content.replace(tempStr, replaceStr);
				}
			}
		}
		html = html.replace(/{Content}/g, content);
		html = html.replace(/{Memo}/g, data.FFile_Memo || '');
		html = html.replace(/{BeginTime}/g, JShell.Date.toString(data.FFile_BeginTime, true) || '');
		html = html.replace(/{EndTime}/g, JShell.Date.toString(data.FFile_EndTime, true) || '永久');
		html = html.replace(/{DataAddTime}/g, JShell.Date.toString(data.FFile_DataAddTime, true) || '');
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
		var html = me.ContentHtml + me.AttachmentHtml;
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
			'Title','Memo','Content', 'BeginTime', 'EndTime', 'DataAddTime'
		];
		var len = fields.length;
		for(var i = 0; i < len; i++) {
			fields[i] = 'FFile_' + fields[i];
		}
		return fields;
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
	/**获取文档模板*/
	getTemplet: function() {
		var templet =
			//'<br>' +
			'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{Title}</h4>' +
			'</div>' +
			
			'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">概要信息</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{Memo}</p>' +
			'</div>'+
			
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">详细信息</h4>' +
			'<p style="padding:5px; word-break:break-all; word-wrap:break-word;">{Content}</p>' +
			'<p style="padding:5px; word-break:break-all; word-wrap:break-word;">创建于&nbsp;{DataAddTime}&nbsp;有效期:&nbsp;{BeginTime}&nbsp;-&nbsp;{EndTime}</p>' +
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
				me.onDwonload(id,title);
			};
		}
	},
	onDwonload: function(id, title) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=1&id=' + id + "&" + title;
        var suffix='',beforeV='';
        var openUrl ='';
        if(title){
        	var index = title.lastIndexOf(".");  
	        suffix = title.substring(index, title.length);
	        beforeV = title.substring(0, index);
	        var fileUrl = JShell.System.Path.ROOT+"/ui/pdfjs/web/viewer.html?file=";
	        openUrl = fileUrl+ encodeURIComponent(url)+ '&print='+me.PRINT+'&download='+me.DOWNLOAD;
	        suffix=suffix.toLowerCase();
        }
    	var firefox=me.isBrowser();
    	if(suffix=='.pdf'){
            if((!me.DOWNLOAD && !me.PRINT) && me.BUILTIN=='1'){//使用内置pdf预览
				window.open(url);
			}else{//使用第三方PDF.js 解析pdf ，可控制pdf 打印，下载按钮
				var IeVersion = me.getIEVersion();
			    if(!IeVersion){
			   	   JShell.Msg.alert('请使用IE8以上或者其他浏览器');
			    }else{
			   	   window.open(openUrl);
			    }
			}
    	}else{
    		window.open(url);
    	}
	},
	 /**判断是否是火狐浏览器*/
    isBrowser:function(){
    	var me =this;
    	var browser=true;
    	if(isFirefox=navigator.userAgent.indexOf("Firefox")>0){ 
    		browser=false;
		}  
	    return browser;
    },
    /**判断是否是IE浏览器*/
    getIEVersion:function (){
     	var num=true;
        var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
        var isIE =  navigator.userAgent.indexOf("MSIE")>0//判断是否IE浏览器
        if(isIE){
            var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
            reIE.test(userAgent);
            var fIEVersion = parseFloat(RegExp["$1"]);
            if(fIEVersion<9){
             	num=false;
            }
        }
        return num;
    }
});