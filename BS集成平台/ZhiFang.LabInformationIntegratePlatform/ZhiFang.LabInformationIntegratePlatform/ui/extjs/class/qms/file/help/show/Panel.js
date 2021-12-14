/**
 * 帮助系统查看应用
 * @author longfc
 * @version 2016-11-22
 */
Ext.define('Shell.class.qms.file.help.show.Panel', {
	extend: 'Ext.panel.Panel',
	title: '帮助文档',
	layout: 'border',
	width: 500,
	height: 400,
	hasBtn: true,
	bodyStyle: {
		background: '#ffffff',
		padding: '5px'
	},
	hasBtntoolbar: false,
	hasLoadMask: true,
	/**默认加载数据*/
	defaultLoad: true,
	autoScroll: true,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_GetHelpHtmlAndJson', //'/help/jsons',
	/**服务附件服务路径*/
	selectAttachmentUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_GetHelpHtmlAndJson',
	/**文件下载服务路径*/
	downloadUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_FFileAttachmentDownLoadFiles',
	AttachmentHtml: '',
	ContentHtml: '',
	/*帮助文档Id,内部调用传入**/
	PK: null,
	/*帮助系统所属的模块Id,外部调用传入**/
	ModuleId: null,
	/*帮助文档所属模块的子序号,外部调用传入**/
	SubWinNo: null,
	/*帮助内容信息是否按主键Id查询,false为按No查询**/
	isSearchForId: false,
	/*帮助系统的类型**/
	FTYPE: '5',
	layout: {
		type: 'border',
		regionWeights: {
			//south: 1,
			center: 1,
		}
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.defaultLoad) {
			me.showHtmls();
		}
	},
	initComponent: function() {
		var me = this;
		me.PK = me.PK || null;
		me.ModuleId = me.ModuleId || null;
		if(me.ModuleId != null && me.ModuleId != undefined) {
			me.ModuleId = me.ModuleId.toString();
			//me.ModuleId.indexOf("H") == -1||
			if(me.ModuleId.length > 0 && me.ModuleId.indexOf("H") != 0) {
				me.ModuleId = "H-" + me.ModuleId;
			}
		}
		me.SubWinNo = me.SubWinNo || null;
		me.title = me.title || "帮助文档";
		me.items = [];
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasBtntoolbar) items.push(me.createButtonbottomtoolbar());
		return items;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	/*查看帮助信息*/
	showHtmls: function(isClear) {
		var me = this;
		me.AttachmentHtml = '';
		me.ContentHtml = '';
		var html = '';
		if(isClear == true) html = '';
		var idIsNull = false;
		if(me.isSearchForId == true) {
			if(me.PK == null || me.PK == "" || me.PK == undefined)
				idIsNull = true;
		} else {
			if(me.ModuleId == null || me.ModuleId == "" || me.ModuleId == undefined)
				idIsNull = true;
		}
		if(idIsNull == true) {
			html = "<div class='alert alert-warning' style='margin:40px 20px;text-align:center;padding-top:40px;padding-bottom:40px;'>传入的文档编码为空,暂无帮助信息!<br>请联系管理人员添加!</div>";
			me.update(html)
			me.hideMask();
		} else {
			me.loadData(function(data) {
				me.changeHtmlContent();
				me.hideMask();
			});
		}
	},
	/**创建功能按钮栏*/
	createButtonbottomtoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			items.push('->');
			items.push({
				xtype: 'button',
				itemId: 'btnRefresh',
				iconCls: 'button-refresh',
				text: "刷新",
				tooltip: '刷新',
				handler: function() {
					me.showHtmls()
				}
			}, {
				xtype: 'button',
				itemId: 'btnColse',
				iconCls: 'button-del',
				text: "关闭",
				tooltip: '关闭',
				handler: function() {
					me.fireEvent('onCloseClick', me);
					me.hide();
				}
			});
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottombuttonsToolbar',
			items: items
		});
	},
	/**获取帮助内容模板*/
	getTemplet: function() {
		var templet =
			//'<br>' +
			'</div>' +
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>文档信息</h4>' +
			'<p style="padding:5px; word-break:break-all; word-wrap:break-word;">{Content}</p>' +
			'</div>' + '</div>';
		return templet;
	},
	/**获取附件模板*/
	getAttachmentTemplet: function() {
		var templet =
			'<div class="col-sm-12">' +
			'<div>' +
			'<h4>附件信息</h4>' +
			'<p style="word-break:break-all; word-wrap:break-word;">{AttachmentList}</p>' +
			'</div>' + '</div>';
		return templet;
	},
	/**获取一个附件模板*/
	getOneAttachmentTemplet: function() {
		var templet =
			'<div style="padding:5px;">' +
			'<a style="font-weight:bold;" filedownload="filedownload" data="{Id}" title="{Title}">{FileName}</a> ' +
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
				me.onDwonload(id);
			};
		}
	},
	onDwonload: function(id) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=1&id=' + id;
		window.open(url);
	},

	/**获取附件HTML*/
	getAttachmentHtml: function(data) {
		var me = this;
		var html = me.getAttachmentTemplet();
		var list = (data.value || {}).list || [],
			len = list.length,
			temp = me.getOneAttachmentTemplet(),
			attArr = [];
		for(var i = 0; i < len; i++) {
			var info = list[i],
				attHtml = temp;
			attHtml = attHtml.replace(/{Id}/g, info.Id);
			attHtml = attHtml.replace(/{FileName}/g, info.FileName);
			attHtml = attHtml.replace(/{FileSize}/g, JShell.Bytes.toSize(parseFloat(info.FileSize)));
			var Title = info.CreatorName + '创建于' +
				JShell.Date.toString(info.DataAddTime);
			attHtml = attHtml.replace(/{Title}/g, Title);
			attArr.push(attHtml);
		}
		html = html.replace(/{AttachmentList}/g, attArr.join(""));
		return html;
	},
	/**按帮助系统所属的模块ID(No)获取帮助内容信息*/
	loadData: function(callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.selectUrl);
		var no = me.ModuleId;
		//帮助文档的模块子序号
		if(me.SubWinNo != "" && me.SubWinNo != null && me.SubWinNo != undefined) {
			no = no + "-" + me.SubWinNo;
		}
		url = url + "?no=" + no + "&fileName=FFile.json";
		me.ContentHtml = "<div class='alert alert-warning' style='margin:40px 20px;text-align:center;padding-top:40px;padding-bottom:40px;'><h4>文档编码: " + me.getModuleNo() + "</h4>该功能没有维护帮助文档，请联系管理人员添加!</div>";

		JShell.Server.get(url, function(data) {
			var success = data.success;
			var callbackData = {
				success: data.success,
				msg: data.msg
			};
			if(data.success) {
				var list = (data.value || {}).list || [];
				var info = null;
				if(list.length > 0) {
					info = list[0];
					var ffileId = null;
					if(info != null) {
						me.ContentHtml = me.HtmlContent(info);
						ffileId = info.FFile_Id;
						me.loadAttachmentData(ffileId, function(attachmentInfo) {
							callbackData = {
								success: attachmentInfo.success,
								msg: attachmentInfo.msg
							};
						});
						callback(callbackData);
					}
				}
			} else {
				me.ContentHtml = "<div class='alert alert-warning' style='margin:40px 20px;text-align:center;padding-top:40px;padding-bottom:40px;'><h4>文档编码: " + me.getModuleNo() + "</h4>" + data.msg + '<br>请联系管理人员</div>';
				callback(callbackData);
			}
		}, false);
	},
	/**获取附件信息*/
	loadAttachmentData: function(ffileId, callback) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);
		var no = me.ModuleId;
		//帮助文档的模块子序号
		if(me.SubWinNo != "" && me.SubWinNo != null && me.SubWinNo != undefined) {
			no = no + "-" + me.SubWinNo;
		}
		url = url + "?no=" + no + "&fileName=FFileAttachment.json";
		me.AttachmentHtml = me.getAttachmentTemplet();
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.AttachmentHtml = me.getAttachmentHtml(data);
			} else {
				var errorInfo = '<b style="color:red">' + data.msg + '</b>'
				me.AttachmentHtml = me.AttachmentHtml.replace(/{AttachmentList}/g, errorInfo);
			}
			callback(data);
		}, false);
	},
	getModuleNo: function() {
		var me = this;
		var no = "";
		if(me.isSearchForId == true) {
			no = me.PK;
		} else {
			no = me.ModuleId;
		}
		if(me.SubWinNo != "" && me.SubWinNo != null && me.SubWinNo != undefined)
			no = no + "-" + me.SubWinNo;
		return no;
	},

	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},

	HtmlContent: function(data) {
		var me = this;
		me.ContentHtml = me.getTemplet();
		var html = me.ContentHtml;
		html = html.replace(/{Title}/g, data.FFile_Title);
		html = html.replace(/{Memo}/g, data.FFile_Memo);
		html = html.replace(/{Content}/g, data.FFile_Content);
		//所属模块名称
		html = html.replace(/{Keyword}/g, data.FFile_Keyword || '');
		html = html.replace(/{No}/g, data.FFile_No);
		html = html.replace(/{DataAddTime}/g, JShell.Date.toString(data.FFile_DataAddTime, true) || '');
		return html;
	},

	changeHtmlContent: function() {
		var me = this;
		var html = me.ContentHtml + me.AttachmentHtml;
		me.update(html);
		me.initDownloadListeners();
	}
});