/*
 * 新闻发布的微信内容
 * @author Jcall
 * @version 2017-01-10
 */
Ext.define('Shell.class.qms.file.news.basic.WeiXin', {
	extend: 'Ext.panel.Panel',
	requires: [
		'Shell.ux.form.field.UEditor'
	],

	layout: 'fit',

	/**获取数据服务路径*/
	selectUrl: '/CommonService.svc/QMS_UDTO_SearchFFileById',
	/**新闻缩略图上传保存服务路径*/
	imgUploadUrl: "/CommonService.svc/QMS_UDTO_UploadNewsThumbnails",
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**选中的微信新闻数据*/
	CheckedWeiXinData: null,
	/**是否已加载数据*/
	hasLoaded: false,

	/**新增或修改标记*/
	formtype: 'add',
	/**数据ID*/
	PK: null,
	/**默认的微信数据*/
	DEFAULT_WEIXIN_DATA: {},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();

		me.callParent(arguments);
	},
	/**创建挂靠*/
	createDockedItems: function() {
		var me = this;
		var dockedItems = [{
			xtype: 'toolbar',
			dock: 'top',
			itemId: 'topToolbar',
			items: ['->', {
				xtype: 'radiogroup',
				itemId: 'type',
				columns: 2,
				width: 160,
				vertical: true,
				items: [{
					boxLabel: '内部新闻',
					name: 'Type',
					inputValue: '1',
					checked: true
				}, {
					boxLabel: '微信新闻',
					name: 'Type',
					inputValue: '2'
				}],
				listeners: {
					change: function(field, newValue, oldValue) {
						me.onTypeChange(newValue.Type);
					}
				}
			}]
		}];

		return dockedItems;
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		//创建内部新闻面板
		items.push(me.createInsidePanel());
		//创建微信新闻面板
		items.push(me.createWinXinPanel());

		return items;
	},

	/**创建内部新闻面板*/
	createInsidePanel: function() {
		var me = this;
		/**
		 * 微信页签在线编辑器的高度处理(先暂时这样设置)
		 * @author longfc
		 * @version 2017-01-13
		 */
		var height = me.height - 225;
		height = (height > 370 ? height : 370);
		var panel = {
			itemId: 'InsidePanel',
			bodyPadding: 1,
			border: false,
			header: false,
			layout: 'border',
			items: [{
				region: 'west',
				xtype: 'form',
				itemId: 'Summary',
				title: '新闻概要信息',
				header: false,
				width: 240,
				split: true,
				collapsible: true,
				bodyPadding: 10,
				layout: 'anchor',
				defaults: {
					anchor: '100%',
					labelWidth: 60,
					labelAlign: 'top'
				},
				items: [{
					xtype: 'textarea',
					itemId: 'ThumbnailsMemo',
					name: 'ThumbnailsMemo',
					fieldLabel: '概要说明',
					height: 100
				}, {
					xtype: 'filefield',
					itemId: 'ImageFile',
					fieldLabel: '缩略图',
					buttonText: '选择',
					listeners: {
						change: function(file, newValue, oldValue, eOpts) {
							me.onImageChange(file, newValue, oldValue, eOpts);
						}
					}
				}, {
					xtype: 'image',
					itemId: 'Image'
				}, {
					xtype: 'textfield',
					itemId: 'ThumbnailsPath',
					name: 'ThumbnailsPath',
					hidden: true
				}]
			}, {
				region: 'center',
				itemId: 'content',
				layout: 'fit',
				items: [{
					xtype: 'ueditor',
					itemId: 'content',
					margin: '2px 2px 2px 2px',
					height: height,
					width: '100%'
				}]
			}]
		};

		return panel;
	},
	/**创建微信新闻面板*/
	createWinXinPanel: function() {
		var me = this;
		var panel = {
			itemId: 'WinXinPanel',
			bodyPadding: 1,
			border: false,
			header: false,
			layout: 'border',
			hidden: true,
			dockedItems: [{
				xtype: 'toolbar',
				dock: 'top',
				itemId: 'topToolbar',
				items: ['->', {
					iconCls: 'button-search',
					text: '选择微信新闻',
					tooltip: '选择微信新闻',
					handler: function() {
						me.onOpenWeiXinCheckGrid();
					}
				}]
			}],
			items: [{
				region: 'west',
				xtype: 'form',
				itemId: 'Summary',
				title: '新闻概要信息',
				header: false,
				width: 240,
				split: true,
				collapsible: true,
				bodyPadding: 10,
				layout: 'anchor',
				defaults: {
					anchor: '100%',
					labelWidth: 60,
					labelAlign: 'top'
				},
				items: [{
					xtype: 'textarea',
					itemId: 'ThumbnailsMemo',
					name: 'ThumbnailsMemo',
					fieldLabel: '概要说明',
					height: 100
				}, {
					xtype: 'label',
					text: '缩略图:'
				}, {
					xtype: 'image',
					itemId: 'Image'
				}]
			}, {
				region: 'center',
				itemId: 'info',
				bodyPadding: 5,
				autoScroll: true
			}]
		};

		var hiddenItems = [{
			xtype: 'textfield',
			fieldLabel: '新闻缩略图上传保存路径',
			itemId: 'ThumbnailsPath',
			name: 'ThumbnailsPath',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信内容',
			itemId: 'WeiXinContent',
			name: 'WeiXinContent',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信Title',
			itemId: 'WeiXinTitle',
			name: 'WeiXinTitle',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信MEDIA_ID',
			itemId: 'Mediaid',
			name: 'Mediaid',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信Url',
			itemId: 'WeiXinUrl',
			name: 'WeiXinUrl',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信缩略图Thumbmediaid',
			itemId: 'Thumbmediaid',
			name: 'Thumbmediaid',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信Author',
			itemId: 'WeiXinAuthor',
			name: 'WeiXinAuthor',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信Digest',
			itemId: 'WeiXinDigest',
			name: 'WeiXinDigest',
			hidden: true
		}, {
			xtype: 'textfield',
			fieldLabel: '微信Content_source_url',
			itemId: 'WeiXinContentsourceurl',
			name: 'WeiXinContentsourceurl',
			hidden: true
		}];

		panel.items[0].items = panel.items[0].items.concat(hiddenItems);

		return panel;
	},

	/**类型变化处理*/
	onTypeChange: function(value) {
		var me = this,
			InsidePanel = me.getComponent('InsidePanel'),
			WinXinPanel = me.getComponent('WinXinPanel');

		if(value == '1') {
			WinXinPanel.hide();
			InsidePanel.show();
		} else {
			InsidePanel.hide();
			WinXinPanel.show();
		}
	},
	/**打开微信新闻选择页面*/
	onOpenWeiXinCheckGrid: function() {
		var me = this;
		JShell.Win.open('Shell.class.qms.file.news.basic.WeiXinCheckGrid', {
			resizable: false,
			title: '微信新闻选择',
			listeners: {
				accept: function(p, data) {
					//选中的微信新闻数据
					me.CheckedWeiXinData = data;
					//显示微信信息概要
					me.onShowWeiXinSummary();
					//显示微信新闻内容
					me.onShowWeiXinInfo();
					p.close();
				}
			}
		}).show();

	},
	/**显示微信信息概要*/
	onShowWeiXinSummary: function() {
		var me = this,
			WeiXinInfo = me.CheckedWeiXinData,
			Item = WeiXinInfo.content.news_item[0],
			WinXinPanel = me.getComponent('WinXinPanel'),
			Summary = WinXinPanel.getComponent('Summary'),
			Image = Summary.getComponent('Image');

		var src = JShell.System.Path.ROOT + '/' + Item.thumb_media_Url;
		Image.setSrc(src);

		Summary.getForm().setValues({
			ThumbnailsPath: Item.thumb_media_Url,
			ThumbnailsMemo: Item.digest,
			WeiXinContent: Item.content,
			WeiXinTitle: Item.tilte,
			Mediaid: WeiXinInfo.media_id,
			WeiXinUrl: Item.url,
			Thumbmediaid: Item.thumb_media_id,
			WeiXinAuthor: Item.author,
			WeiXinDigest: Item.digest,
			WeiXinContentsourceurl: Item.content_source_url
		});
	},
	/**显示微信新闻内容*/
	onShowWeiXinInfo: function() {
		var me = this,
			data = me.CheckedWeiXinData,
			content = data.content || {},
			list = content.news_item || [],
			len = list.length,
			html = [];

		for(var i = 0; i < len; i++) {
			html.push(list[i].content);
		}

		var WinXinPanel = me.getComponent('WinXinPanel'),
			info = WinXinPanel.getComponent('info');
		info.update(html.join(""));
	},

	/**
	 * @public
	 * 获取数据
	 */
	getData: function() {
		var me = this,
			topToolbar = me.getComponent('topToolbar'),
			type = topToolbar.getComponent('type').getValue().Type,
			data = {};

		if(type == '1') {
			data = me.getInsideData();
		} else if(type == '2') {
			data = me.getWeiXinData();
		}

		return data;
	},
	/**获取内部新闻数据*/
	getInsideData: function() {
		var me = this,
			InsidePanel = me.getComponent('InsidePanel'),
			content = InsidePanel.getComponent('content').getComponent('content'),
			Summary = InsidePanel.getComponent('Summary'),
			values = Summary.getForm().getValues(),
			data = {};

		//是否同步到微信服务器
		data.IsSyncWeiXin = false;
		//新闻缩略图上传保存路径
		data.ThumbnailsPath = values.ThumbnailsPath;
		//新闻缩略图描述
		data.ThumbnailsMemo = values.ThumbnailsMemo;
		//微信内容
		data.WeiXinContent = content.getValue();
		//微信Title
		data.WeiXinTitle = '';

		return data;
	},
	/**获取微信新闻数据*/
	getWeiXinData: function() {
		var me = this,
			WinXinPanel = me.getComponent('WinXinPanel'),
			Summary = WinXinPanel.getComponent('Summary'),
			values = Summary.getForm().getValues(),
			data = {};

		//是否同步到微信服务器
		data.IsSyncWeiXin = true;
		//新闻缩略图上传保存路径
		data.ThumbnailsPath = values.ThumbnailsPath;
		//新闻缩略图描述
		data.ThumbnailsMemo = values.ThumbnailsMemo;
		//微信内容
		data.WeiXinContent = values.WeiXinContent;
		//微信Title
		data.WeiXinTitle = values.WeiXinTitle;
		//微信MEDIA_ID
		data.Mediaid = values.Mediaid;
		//微信Url
		data.WeiXinUrl = values.WeiXinUrl;
		//微信缩略图Thumbmediaid
		data.Thumbmediaid = values.Thumbmediaid;
		//微信Author
		data.WeiXinAuthor = values.WeiXinAuthor;
		//微信Digest
		data.WeiXinDigest = values.WeiXinDigest;
		//微信Content_source_url
		data.WeiXinContentsourceurl = values.WeiXinContentsourceurl;

		return data;
	},

	/**加载数据*/
	load: function(id, reset) {
		var me = this;

		//id不存在、不强制重置数据 并且已经加载过数据的就就不再加载
		//if(!id || (!reset && me.hasLoaded)) return;
		if(me.hasLoaded) return;

		me.hasLoaded = true;

		if(id) { me.PK = id; }

		if(!me.PK) return;

		//加载数据
		me.getDataById(me.PK, function() {
			//赋值渲染-初始化面板内容
			me.onInitPanel();
		});
	},
	/**根据ID获取数据 */
	getDataById: function(id, callback) {
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl + '?isPlanish=false&id=' + id;

		var fields = [
			'IsSyncWeiXin', 'WeiXinContent', 'ThumbnailsMemo', 'ThumbnailsPath',
			'WeiXinTitle', 'Mediaid', 'WeiXinUrl', 'Thumbmediaid', 'WeiXinAuthor',
			'WeiXinDigest', 'WeiXinContentsourceurl', 'Id'
		];

		url += '&fields=FFile_' + fields.join(",FFile_");

		me.showMask(me.saveText); //显示遮罩
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩
			if(data.success) {
				me.onChangeData(data.value);
				callback();
			} else {
				JShell.Msg.error(data.msg || "微信新闻-获取数据请求错误！");
			}
		});
	},
	/**数据处理*/
	onChangeData: function(data) {
		var me = this;

		data.IsSyncWeiXin = (data.IsSyncWeiXin + "" == "true") ? true : false;
		data.WeiXinContent = data.WeiXinContent.replace(/<br\/>/g, '\r\n');
		data.ThumbnailsMemo = data.ThumbnailsMemo.replace(/<br\/>/g, '\r\n');

		me.DEFAULT_WEIXIN_DATA = data;
	},
	/**初始化面板内容*/
	onInitPanel: function() {
		var me = this,
			data = me.DEFAULT_WEIXIN_DATA;

		if(!data) return;

		//赋值渲染
		var topToolbar = me.getComponent('topToolbar'),
			type = topToolbar.getComponent('type');
		if(!data.IsSyncWeiXin) {
			type.setValue({ Type: '1' });
			var InsidePanel = me.getComponent('InsidePanel'),
				Summary = InsidePanel.getComponent('Summary'),
				Image = Summary.getComponent('Image'),
				content = InsidePanel.getComponent('content').getComponent('content');

			Summary.getForm().setValues(data);

			Image.setSrc(JShell.System.Path.ROOT + '/' + data.ThumbnailsPath + '?t=' + new Date().getTime());
			content.setValue(data.WeiXinContent);
		} else {
			type.setValue({ Type: '2' });
			var WinXinPanel = me.getComponent('WinXinPanel'),
				Summary = WinXinPanel.getComponent('Summary'),
				Image = Summary.getComponent('Image'),
				info = WinXinPanel.getComponent('info');

			Summary.getForm().setValues(data);

			Image.setSrc(JShell.System.Path.ROOT + '/' + data.ThumbnailsPath);
			info.update(data.WeiXinContent);
		}
	},

	/**缩略图更改处理*/
	onImageChange: function(file, newValue, oldValue, eOpts) {
		if(!newValue) return;
		var me = this;
		if(me.formtype != 'edit') return;
		//保存图片
		var url = JShell.System.Path.ROOT + me.imgUploadUrl;
		var items = [];
		items.push({ xtype: 'textfield', name: 'FFile_Id', value: me.PK });
		var ImageFile = me.getComponent('InsidePanel').getComponent("Summary").getComponent("ImageFile");
		var filenew = { xtype: 'filefield', fieldLabel: '缩略图文件', name: 'file', itemId: 'file' };
		if(ImageFile.fileInputEl.dom.files) {
			if(ImageFile.fileInputEl.dom.files.length > 0) {
				filenew = Ext.Object.merge(ImageFile, filenew);
			}
		} else {
			if(ImageFile.value != "" && ImageFile.value != undefined) {
				filenew = Ext.Object.merge(ImageFile, filenew);
			}
		}
		items.push(filenew);

		var panel = Ext.create('Ext.form.Panel', {
			title: '需要保存的数据',
			hidden: true,
			items: items,
			listeners: {
				afterrender: function() {
					panel.getForm().submit({
						url: url,
						success: function(form, action) {
							var data = action.result;
							if(data.success) {
								var src = data.ResultDataValue;
								if(src) {
									var Summary = me.getComponent('InsidePanel').getComponent('Summary');
									Summary.getComponent('ThumbnailsPath').setValue(src);
									var Image = Summary.getComponent('Image');
									Image.setSrc(JShell.System.Path.ROOT + '/' + src + '?t=' + new Date().getTime());
								} else {
									JShell.Msg.error('服务错误：返回路径为空！');
								}
							} else {
								var msg = data.ErrorInfo;
								JShell.Msg.error(msg);
							}
							//me.remove(panel);
						},
						failure: function(form, action) {
							JShell.Msg.error('服务错误！');
							//me.remove(panel);
						}
					});
				}
			}
		});
		me.add(panel);

	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	}
});