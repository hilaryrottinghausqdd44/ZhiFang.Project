/*
 * 新闻发布的微信内容页签
 * @author longfc
 * @version 2017-01-03
 */
Ext.define('Shell.class.qms.file.news..basic.WeiXinContentForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UEditor',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 220,
		labelAlign: 'right'
	},
	formtype: "add",
	title: '微信内容',
	width: 790,
	height: 550,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**获取数据服务路径*/
	selectUrl: '/CommonService.svc/QMS_UDTO_SearchFFileById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/CommonService.svc/QMS_UDTO_AddFFileByFormData',
	/**修改服务地址*/
	editUrl: '/CommonService.svc/QMS_UDTO_UpdateFFileByFieldAndFormData',
	/**新闻缩略图获取服务路径*/
	downloadUrl: "/CommonService.svc/QMS_UDTO_DownLoadNewsThumbnailsById",
	/**新闻缩略图上传保存服务路径*/
	uploadUrl: "/CommonService.svc/QMS_UDTO_UploadNewsThumbnails",
	/**显示成功信息*/
	showSuccessInfo: false,
	/**带附件按钮*/
	hasAttachButton: true,
	/**文档操作记录类型值*/
	fFileOperationType: 1,
	/**文档状态值*/
	fFileStatus: 1,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,

	autoScroll: true,
	PK: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	initComponent: function() {
		var me = this;
		me.FTYPE = '2';
		me.colspanWidth = me.width / 2 - 10;
		me.defaults.width = (me.colspanWidth < 220) ? 220 : me.colspanWidth;
		me.selectUrl = me.selectUrl + "&isAddFFileReadingLog=0&isAddFFileOperation=0";
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},

	/*新闻缩略图**/
	createNewsThumbnails: function(fieldLabel) {
		var me = this;
		var src = "";
		me.FFile_ThumbnailsPath = {
			xtype: 'image',
			fieldLabel: fieldLabel,
			labelWidth: 0,
			src: Ext.BLANK_IMAGE_URL,
			height: 160,
			style: 'background:none;border:0;border-bottom:0px;',
			name: 'FFile_ThumbnailsPath',
			itemId: 'FFile_ThumbnailsPath'
		};
		me.FFile_ThumbnailsPath.colspan = 1;
		me.FFile_ThumbnailsPath.width = me.colspanWidth - 10;
	},
	/**创建新闻可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.createFFile_IsSyncWeiXin('是否同步到微信');
		me.createFile('浏览');
		me.createFFile_ThumbnailsMemo('描述');
		me.createLeftFieldSet();

		me.createNewsThumbnails('');
		me.createThumbnailsPathFieldSet();
		me.createFFile_WeiXinContent('');
	},

	createFFile_IsSyncWeiXin: function(fieldLabel) {
		var me = this;
		me.FFile_IsSyncWeiXin = {
			boxLabel: fieldLabel,
			name: 'FFile_IsSyncWeiXin',
			itemId: 'FFile_IsSyncWeiXin',
			xtype: 'checkbox',
			inputValue: false,
			style: {
				marginLeft: '45px'
			},
		};
		me.FFile_IsSyncWeiXin.colspan = 1;
		me.FFile_IsSyncWeiXin.width = me.colspanWidth - 30;
		if(me.formtype == "add")
			me.FFile_IsSyncWeiXin.checked = false;
	},
	/*上传**/
	createFile: function(fieldLabel) {
		var me = this;
		me.File = {
			fieldLabel: fieldLabel,
			name: 'file',
			emptyText: '只能上传png格式',
			allowBlank: true,
			itemId: 'File',
			xtype: 'filefield',
			buttonText: '选择',
			validator: function(value) {
				var arr = value.split('.');
				if(arr[arr.length - 1].toString().toLowerCase() != 'png') {
					return '文件格式不正确！只能上传.png';
				} else {
					return true;
				}
			},
			listeners: {
				change: function(field, newValue, oldValue, eOpts) {
					var file = me.getComponent('LeftFieldSet').getComponent('File');
					if(me.formtype == 'edit' && newValue != "") {
						me.uploadSave(field, newValue, eOpts);
					} else if(newValue != "") {
						var url = 'file://' + file.getValue();
						var image = me.getComponent('ThumbnailsPathFieldSet').getComponent('FFile_ThumbnailsPath');
						image.setSrc(newValue);
					}
				}
			}
		};
		me.File.colspan = 1;
		me.File.width = me.colspanWidth - 30;
	},
	uploadSave: function(field, value, eOpts) {
		var me = this;
		me.showMask(me.saveText); //显示遮罩层
		me.fireEvent('beforesave', me);
		var url = (me.uploadUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.uploadUrl;
		me.getForm().submit({
			url: url,
			//waitMsg: "程序信息保存处理中,请稍候...",
			success: function(form, action) {
				var data = action.result;
				var resultDataValue = data.ResultDataValue;
				if(data.success) {
					me.hideMask();
					//JShell.Msg.alert(resultDataValue, null, 3000);
					//JShell.Msg.alert("上传成功!", null, 1000);
				}
				var image = me.getComponent('ThumbnailsPathFieldSet').getComponent('FFile_ThumbnailsPath');
				if(image != undefined)
					image.setSrc(Ext.BLANK_IMAGE_URL);
				if(resultDataValue != "") {
					var timestamp = (new Date()).valueOf();
					resultDataValue = JShell.System.Path.ROOT + resultDataValue + "?timestamp=" + timestamp;
					if(image != undefined) {
						image.setSrc(resultDataValue);
						//image.callParent(arguments);
						image.update(resultDataValue);
					}
				}
			},
			failure: function(form, action) {
				var data = action.result;
				me.hideMask();
				var image = me.getComponent('ThumbnailsPathFieldSet').getComponent('FFile_ThumbnailsPath');
				if(image != undefined)
					image.setSrc("");
				//Ext.BLANK_IMAGE_URL
				JShell.Msg.error('上传失败！<br />' + data.ErrorInfo);
			}
		});
	},
	createLeftFieldSet: function() {
		var me = this;
		me.LeftFieldSet = {
			xtype: 'fieldset',
			labelAlign: 'right',
			layout: {
				type: 'table',
				columns: 1
			},
			defaults: {
				labelWidth: 40,
				labelAlign: 'right'
			},
			style: 'background:none;border:1px solid #5cb85c;',
			margin: '2px 2px 2px 2px',
			//style: 'background:none;border:0;border-bottom:0px;',
			name: 'LeftFieldSet',
			itemId: 'LeftFieldSet',
			defaultType: 'textfield',
			items: [me.FFile_IsSyncWeiXin, me.File, me.FFile_ThumbnailsMemo]
		};
	},
	createThumbnailsPathFieldSet: function() {
		var me = this;
		me.ThumbnailsPathFieldSet = {
			xtype: 'fieldset',
			labelAlign: 'right',
			layout: {
				type: 'table',
				columns: 1
			},
			margin: '2px 2px 2px 2px',
			style: 'background:none;border:1px solid #5cb85c;',
			name: 'LeftFieldSet',
			defaultType: 'textfield',
			height: 160,
			itemId: 'ThumbnailsPathFieldSet',
			items: [me.FFile_ThumbnailsPath]
		};
	},
	/**缩略图描述*/
	createFFile_ThumbnailsMemo: function(fieldLabel) {
		var me = this;
		var minHeight = 50,
			height = 80;
		me.FFile_ThumbnailsMemo = {
			fieldLabel: fieldLabel,
			name: 'FFile_ThumbnailsMemo',
			minHeight: minHeight,
			height: height,
			maxLength: 500,
			maxLengthText: "摘要最多只能输入500字",
			style: {
				marginBottom: '2px'
			},
			xtype: 'textarea'
		};
		me.FFile_ThumbnailsMemo.colspan = 1;
		me.FFile_ThumbnailsMemo.width = me.colspanWidth - 30;
	},
	/**微信内容*/
	createFFile_WeiXinContent: function(fieldLabel) {
		var me = this;
		var height = 185; // document.body.clientHeight * 0.415;
		height = (height > 185 ? height : 185);
		me.FFile_WeiXinContent = {
			name: 'FFile_WeiXinContent',
			itemId: 'FFile_WeiXinContent',
			height: height,
			margin: '2px 2px 2px 2px',
			xtype: 'ueditor',
			autoScroll: true,
			border: false
		};
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		me.colspanWidth = parseInt(me.width / 2) - 10;

		me.LeftFieldSet.colspan = 1;
		me.LeftFieldSet.width = me.colspanWidth;
		items.push(me.LeftFieldSet);

		//新闻缩略图
		me.ThumbnailsPathFieldSet.colspan = 1;
		me.ThumbnailsPathFieldSet.width = me.colspanWidth - 20;
		items.push(me.ThumbnailsPathFieldSet);
		//微信内容
		me.FFile_WeiXinContent.colspan = 2;
		me.FFile_WeiXinContent.width = me.colspanWidth * 2 - 10;
		items.push(me.FFile_WeiXinContent);

		return items;
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this,
			items = me.items.items || [],
			len = items.length,
			fields = ["FFile_IsSyncWeiXin", "FFile_ThumbnailsMemo", "FFile_WeiXinContent", 'FFile_ThumbnailsPath'];
		return fields;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var reg = new RegExp("<br />", "g");
		if(data.FFile_ThumbnailsPath && data.FFile_ThumbnailsPath != null) {
			var timestamp = (new Date()).valueOf();
			data.FFile_ThumbnailsPath = data.FFile_ThumbnailsPath.replace(/\\\\/g,"\/"); ;
			data.FFile_ThumbnailsPath = JShell.System.Path.ROOT + data.FFile_ThumbnailsPath + "?timestamp=" + timestamp;
			var image = me.getComponent('ThumbnailsPathFieldSet').getComponent('FFile_ThumbnailsPath');
			image.setSrc(data.FFile_ThumbnailsPath);
		}
		if(data.FFile_ThumbnailsMemo && data.FFile_ThumbnailsMemo != null)
			data.FFile_ThumbnailsMemo = data.FFile_ThumbnailsMemo.replace(reg, "\r\n");
		if(data.FFile_WeiXinContent && data.FFile_WeiXinContent != null)
			data.FFile_WeiXinContent = data.FFile_WeiXinContent.replace(reg, "\r\n");
		//		reg = new RegExp("&#92", "g");
		//		data.FFile_Memo = data.FFile_Memo.replace(reg, '\\');
		return data;
	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			hidden: true,
			name: 'FFile_Id',
			value: (me.formtype == "edit" ? me.PK : "")
		});
		return items;
	}
});