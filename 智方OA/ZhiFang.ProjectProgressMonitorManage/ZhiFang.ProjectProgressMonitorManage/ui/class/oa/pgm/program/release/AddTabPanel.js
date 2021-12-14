/**
 * 程序基本信息新增
 * @author longfc
 * @version 2016-09-28
 */
Ext.define('Shell.class.oa.pgm.program.release.AddTabPanel', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '程序发布',
	border: false,
	closable: true,
	/**是否重置按钮*/
	hasReset: false,
	/**是否启用取消按钮*/
	hasCancel: false,
	/**自定义按钮功能栏*/
	buttonToolbarItems: null,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**功能按钮栏位置*/
	buttonDock: 'bottom',
	PBDictTreeId: '',
	/**所属字典树Id*/
	SubBDictTreeId: "",
	SubBDictTreeCName: '',
	hasLoadMask: true,
	/**文档操作记录类型*/
	fFileOperationType: 3,
	/**文档状态值*/
	fFileStatus: 1,
	PK: '',
	formtype: "",
	/**显示操作记录页签	,false不显示*/
	hasOperation: false,
	formLoaded: false,
	basicFormIsLoad: false,
	contentIsLoad: false,
	basicFormApp: '',
	isLeaf: null,
	initComponent: function() {
		var me = this;
		me.PBDictTreeId = me.PBDictTreeId || "";
		me.SubBDictTreeId = me.SubBDictTreeId || "";
		me.bodyPadding = 1;
		me.title = me.title || "";
		me.setTitles();
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.formtype == 'edit') {
			JShell.Action.delay(function() {
				me.loadContentForm();
			}, null, 200);
		}
	},
	/**设置各页签的显示标题*/
	setTitles: function() {
		var me = this;
		me.basicFormTitle = "程序基本信息";
		me.contentFormTitle = "程序详细说明内容";
	},

	loadbasicForm: function() {
		var me = this;
		if(me.basicFormIsLoad == false && me.formtype == 'edit') {
			me.basicForm.load(me.PK);
		}
		me.basicFormIsLoad = true;
	},
	/**加载文档内容信息*/
	loadContentForm: function() {
		var me = this;
		if(me.contentIsLoad == false && me.formtype == 'edit') {
			me.contentIsLoad = true;
			me.ContentForm.load(me.PK);
		}
	},

	loadDafultData: function() {
		var me = this;
		var id = me.PK;
		me.contentIsLoad = false;
		if(me.formtype == 'edit') {
			me.basicForm.isEdit(me.PK);
			JShell.Action.delay(function() {
				me.loadContentForm();
			}, null, 200);
		}
	},
	createItems: function() {
		var me = this;
		me.basicForm = Ext.create(me.basicFormApp, {
			itemId: 'basicForm',
			formtype: me.formtype,
			hasSave: false,
			hasReset: me.hasReset,
			border: false,
			title: me.basicFormTitle || '基本信息',
			PBDictTreeId: me.PBDictTreeId,
			PBDictTreeCName: me.PBDictTreeCName,
			SubBDictTreeId: me.SubBDictTreeId,
			SubBDictTreeCName: me.SubBDictTreeCName,
			height: me.height,
			width: me.width,
			isLeaf: me.isLeaf,
			PK: me.PK
		});
		me.ContentForm = Ext.create('Shell.class.oa.pgm.basic.ContentForm', {
			title: me.contentFormTitle || '程序详细说明内容',
			header: false,
			height: me.height,
			width: me.width,
			itemId: 'ContentForm',
			border: false,
			formtype: me.formtype,
			PK: me.PK
		});
		if(me.formtype == 'edit') {
			me.hasOperation = true;
		}
		me.OperationForm = Ext.create('Shell.class.oa.sc.operation.Grid', {
			title: '操作记录',
			header: false,
			hasButtontoolbar: false,
			hasPagingtoolbar: false,
			/**默认每页数量*/
			defaultPageSize: 500,
			hidden: !me.hasOperation,
			itemId: 'OperationForm',
			PK: me.PK,
			border: false,
			isShowForm: false
		});
		return [me.basicForm, me.ContentForm, me.OperationForm];
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) {
			items = me.createButtontoolbar();
		}
		return Ext.create('Ext.toolbar.Toolbar', {
			dock: 'bottom',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push("->");
		items.push({
			xtype: 'button',
			itemId: 'btnTempSave',
			iconCls: 'button-save',
			text: "暂存",
			tooltip: "程序临时暂存",
			handler: function() {
				me.onTempSaveClick();
			}
		});
		items.push({
			xtype: 'button',
			itemId: 'btnRelease',
			iconCls: 'button-save',
			text: "发布",
			tooltip: "程序发布",
			handler: function() {
				me.onReleaseClick();
			}
		}, {
			xtype: 'button',
			itemId: 'btnReset',
			iconCls: 'button-reset',
			text: "重置",
			tooltip: '重置',
			handler: function() {
				me.onResetClick();
			}
		});
		items.push({
			xtype: 'button',
			itemId: 'btnColse',
			iconCls: 'button-del',
			text: "关闭",
			tooltip: '关闭',
			handler: function() {
				me.onCloseClick();
			}
		});
		return items;
	},

	/**页签切换事件处理*/
	ontabchange: function() {
		var me = this;
		me.on({
			tabchange: function(tabPanel, newCard, oldCard, eOpts) {
				var oldItemId = null;
				if(oldCard != null) {
					oldItemId = oldCard.itemId
				}
				switch(newCard.itemId) {
					case 'ContentForm':
						me.loadContentForm();
						break;
					case 'basicForm':
						me.loadbasicForm();
						break;
					default:
						break
				}
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(false); //启用所有的操作功能
	},

	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		var btnTempSave = buttonsToolbar.getComponent('btnTempSave');
		var btnRelease = buttonsToolbar.getComponent('btnRelease');
		var btnReset = buttonsToolbar.getComponent('btnReset');
		var btnColse = buttonsToolbar.getComponent('btnColse');

		btnTempSave.setDisabled(enable);
		btnRelease.setDisabled(enable);
		btnReset.setDisabled(enable);
		btnColse.setDisabled(enable);
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	},
	/**提交点击处理方法*/
	onSaveClick: function() {
		var me = this;
		me.saveffile();
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');

		var values = Form.getForm().getValues();
		var IsUse = 1; //隐藏,全是在用 values.FFile_IsUse ? 1 : 0;

		var entity = {
			Title: values.PGMProgram_Title, //标题
			No: values.PGMProgram_No,
			VersionNo: values.PGMProgram_VersionNo,
			Status: me.fFileStatus,
			Keyword: values.PGMProgram_Keyword,
			ClientName: values.PGMProgram_ClientName,
			OtherFactoryName: values.PGMProgram_OtherFactoryName,
			IsDiscuss: values.PGMProgram_IsDiscuss ? 1 : 0,
			IsUse: IsUse,
			OperationMemo: me.ffileOperationMemo
		};

		if(values.PGMProgram_SQH) {
			entity.SQH = values.PGMProgram_SQH;
		}

		if(values.PGMProgram_ClientID) {
			entity.ClientID = values.PGMProgram_ClientID;
		}
		if(values.PGMProgram_OtherFactoryID) {
			entity.OtherFactoryID = values.PGMProgram_OtherFactoryID;
		}
		//程序名称
		var NewFileName = Form.getComponent('PGMProgram_NewFileName');
		if(NewFileName && NewFileName.getValue() != "") {
			entity.NewFileName = NewFileName.getValue();
		} else {
			entity.NewFileName = Title + "-" + VersionNo;
		}

		if(values.PGMProgram_DispOrder) {
			entity.DispOrder = values.PGMProgram_DispOrder;
		}

		if(values.PGMProgram_PublisherDateTime) {
			entity.PublisherDateTime = JShell.Date.toServerDate(values.PGMProgram_PublisherDateTime);
		}
		//仪器
		if(values.PGMProgram_BEquip_Id) {
			entity.BEquip = {
				Id: values.PGMProgram_BEquip_Id
			};
		}
		//程序
		if(values.PGMProgram_OriginalPGMProgram_Id) {
			entity.OriginalPGMProgram = {
				Id: values.PGMProgram_OriginalPGMProgram_Id
			};
		}
		//所属字典树上级节点
		if(values.PGMProgram_PBDictTree_Id) {
			entity.PBDictTree = {
				Id: values.PGMProgram_PBDictTree_Id
			};
		}
		//所属字典树
		if(values.PGMProgram_SubBDictTree_Id) {
			entity.SubBDictTree = {
				Id: values.PGMProgram_SubBDictTree_Id
			};
			if(values.PGMProgram_SubBDictTree_CName) {
				entity.SubBDictTree.CName = values.PGMProgram_SubBDictTree_CName;
			} else {
				entity.SubBDictTree.CName = me.SubBDictTreeCName;
			}
		}
		//文档内容
		var contentvalues = me.ContentForm.getForm().getValues();
		var content = contentvalues.PGMProgram_Content;
		if(content && content != 'undefined') {
			entity.Content = content.replace(/\\/g, '&#92');
		} else {
			entity.Content = "";
		}

		//文档概要
		if(values.PGMProgram_Memo) {
			entity.Memo = values.PGMProgram_Memo.replace(/\\/g, '&#92');
			entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		}
		return {
			entity: entity,
			ffileOperationMemo: me.ffileOperationMemo
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();
		entity = me.getAddParams();

		var fields = [];
		var fields = ['Id', 'Title', 'No', 'VersionNo', 'Keyword', 'Memo', 'Status', 'DispOrder', 'NewFileName', 'PBDictTree_Id', 'SubBDictTree_Id', 'OriginalPGMProgram_Id', 'BEquip_Id', 'DataUpdateTime', 'ClientID', 'ClientName', 'OtherFactoryID', 'OtherFactoryName'];

		//如果内容已加载,需要编辑保存
		if(me.contentIsLoad) {
			fields.push('Content');
		}
		//发布信息处理
		fields.push('PublisherDateTime', 'IsDiscuss');

		//附件信息处理
		var file = Form.getComponent("PGMProgram_File");
		if(file.fileInputEl.dom.files) {
			if(file.fileInputEl.dom.files.length > 0) {
				fields.push('FileName', 'Size', 'FilePath', 'FileExt', 'FileType');
			}
		} else{
			if(file.value!=""&&file.value!=null) {
				fields.push('FileName', 'Size', 'FilePath', 'FileExt', 'FileType');
			}
		}
		entity.fields = fields.join(',');
		entity.entity.Id = values.PGMProgram_Id;
		return entity;
	},
	/*文档新增及编辑保存*/
	saveffile: function() {
		var me = this;
		var Form = me.getComponent('basicForm');
		var values = Form.getForm().getValues();
		var isValid = Form.getForm().isValid();
		if(!values.PGMProgram_Title || values.PGMProgram_Title == "") {
			JShell.Msg.alert("标题不能为空", null, 1000);
			return;
		}
		if(!isValid) return;
		var url = Form.formtype == 'add' ? Form.addUrl : Form.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		var params = Form.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if(!params) return;
		if(Form.formtype == "edit" && Form.PK == "") {
			return;
		}

		params.fFileOperationType = me.fFileOperationType;
		params.entity.Status = me.fFileStatus;

		var Content = params.entity.Content;
		params.entity.Content = "";

		/*程序类型:1为通用;2为定制通讯;3为通讯模板*/
		var programType = Form.PROGRAMTYPE;

		var items = [{
			xtype: 'textfield',
			fieldLabel: 'programType',
			name: 'programType',
			value: programType
		}, {
			xtype: 'textfield',
			fieldLabel: 'fFileContent',
			name: 'fFileContent',
			value: Content
		}, {
			xtype: 'textarea',
			fieldLabel: 'fFileEntity',
			name: 'fFileEntity',
			value: Ext.JSON.encode(params.entity)
		}, {
			xtype: 'textfield',
			fieldLabel: 'fields',
			name: 'fields',
			value: params.fields
		}, {
			xtype: 'textfield',
			fieldLabel: 'fFileOperationType',
			name: 'fFileOperationType',
			value: params.fFileOperationType
		}, {
			xtype: 'textfield',
			fieldLabel: 'ffileOperationMemo',
			name: 'ffileOperationMemo',
			value: params.ffileOperationMemo
		}];

		//表单的附件组件
		var file = Form.getComponent("PGMProgram_File");
		var filenew = {
			xtype: 'filefield',
			fieldLabel: 'file',
			name: 'file'
		};
		if(file) {
			filenew = Ext.Object.merge(file, filenew);
		}
		filenew.itemId = "filenew";
		filenew.id = "filenewId";
		items.push(filenew);

		me.showMask("数据保存中...");
		var panel = Ext.create('Ext.form.Panel', {
			title: '需要保存的数据',
			bodyPadding: 10,
			layout: 'anchor',
			hidden: true,
			/**每个组件的默认属性*/
			defaults: {
				anchor: '100%',
				labelWidth: 80,
				labelAlign: 'right',
				hidden: true
			},
			html: '<div style="text-align:center;margin:20px;">数据保存中...</div>',
			items: items,
			listeners: {
				afterrender: function() {
					panel.getForm().submit({
						url: url,
						timeout: 600,//10分钟,默认30秒
						//waitMsg: "程序信息保存处理中,请稍候...",
						//async: false,
						success: function(form, action) {
							var data = action.result;
							me.hideMask();
							if(data.success) {
								if(Form.formtype == "add") {
									data.value = Ext.JSON.decode(data.ResultDataValue);
									if(data.value == null) {
										JShell.Msg.error("保存失败!");
										return false;
									} else {
										me.PK = data.value.id;
									}
								}
								if(Form.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, Form.hideTimes);
								me.fireEvent('save', me);
							} else {
								var msg = data.ErrorInfo;
								var index = msg.indexOf('UNIQUE KEY');
								if(index != -1) {
									msg = '有重复';
								}
								JShell.Msg.error(msg);
							}
						},
						failure: function(form, action) {
							var data = action.result;
							me.hideMask();
							JShell.Msg.error('服务错误！'+data.ErrorInfo);
						}
					});
				}
			}
		});

		me.getComponent("buttonsToolbar").add(panel);
	},

	/**程序临时保存*/
	onTempSaveClick: function() {
		var me = this;
		me.fireEvent('onTempSaveClick', me);
		var Form = me.getComponent('basicForm');
		if(Form.PK != "") {
			me.fFileOperationType = 5; //修改暂存
			me.ffileOperationMemo = "程序修改暂存";
		} else {
			me.fFileOperationType = 3; //新增暂存
			me.ffileOperationMemo = "程序暂存";
		}
		me.fFileStatus = 1;
		me.onSaveClick();
	},
	/**程序发布*/
	onReleaseClick: function() {
		var me = this;
		me.fireEvent('onReleaseClick', me);
		var Form = me.getComponent('basicForm');
		if(Form.PK != "") {
			me.fFileOperationType = 6; //修改发布
			me.ffileOperationMemo = "修改发布";
		} else {
			me.fFileOperationType = 4; //新增发布
			me.ffileOperationMemo = "直接发布";
		}
		me.fFileStatus = 3;
		me.onSaveClick();
	},
	/**关闭*/
	onCloseClick: function() {
		var me = this;
		me.fireEvent('onCloseClick', me);
		me.close();
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		//基本表单重置
		if(!me.basicForm.PK) {
			me.basicForm.getForm().reset();
		} else {
			me.basicForm.getForm().setValues(me.basicForm.lastData);
		}
		//详细内容表单重置
		if(!me.ContentForm.PK) {
			me.ContentForm.getForm().reset();
		} else {
			me.ContentForm.getForm().setValues(me.ContentForm.lastData);
		}
	}
});