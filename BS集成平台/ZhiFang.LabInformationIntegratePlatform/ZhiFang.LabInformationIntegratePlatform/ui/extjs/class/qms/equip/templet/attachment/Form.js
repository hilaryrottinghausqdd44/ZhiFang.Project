/**
 * 附件上传
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.attachment.Form', {
	extend: 'Shell.ux.form.Panel',
	title: "附件上传",
	formtype: "add",
	bodyPadding: 10,
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 300,
		labelAlign: 'right'
	},
	layout: {
		type: 'anchor',
		columns: 1 //每行有几列
	},
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEAttachmentById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/QMS_UDTO_UploadTempletAttachment',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEAttachmentByField',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: true,
	height: 240,
	width: 300,
	hasReset: false,
	/**主键字段*/
	PKField: 'EAttachment_Id',
	/**数据主键*/
	PK: '',
	TempletID: '',
	/**查询对象*/
	objectEName: 'EAttachment',
	hasSetValue: true,
	/**月保养*/
	TempletType: '月保养',
	/**月保养编码*/
	TempletTypeCode: 'MD',
	FileUploadDate:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		return [{
			fieldLabel: '编号',
			hidden: true,
			name: 'EAttachment_Id',
			itemId: 'EAttachment_Id'
		}, {
			fieldLabel: '月保养',
			hidden: true,
			name: 'EAttachment_TempletType',
			itemId: 'EAttachment_TempletType'
		}, {
			fieldLabel: '月保养编码',
			hidden: true,
			name: 'EAttachment_TempletTypeCode',
			itemId: 'EAttachment_TempletTypeCode'
		}, {
			fieldLabel: '文件类型',
			hidden: true,
			name: 'EAttachment_FileExt',
			itemId: 'EAttachment_FileExt'
		}, {
			fieldLabel: '文件大小',
			hidden: true,
			name: 'EAttachment_FileSize',
			itemId: 'EAttachment_FileSize'
		}, {
			fieldLabel: '路径',
			hidden: true,
			name: 'EAttachment_FilePath',
			itemId: 'EAttachment_FilePath'
		}, {
			fieldLabel: '新名称',
			hidden: true,
			name: 'EAttachment_FileNewName',
			itemId: 'EAttachment_FileNewName'
		}, {
			fieldLabel: '文件格式',
			hidden: true,
			name: 'EAttachment_FileType',
			itemId: 'EAttachment_FileType'
		}, {
			xtype: 'filefield',
			fieldLabel: '附件',
			colspan: 1,
			width: me.defaults.width * 1,
			name: 'EAttachment_FileName',
			itemId: 'EAttachment_FileName',
			allowBlank: false,
			emptyText: '请选择',
			buttonConfig: {
				iconCls: 'button-search',
				text: ''
			},
			emptyText: '只能上传pdf、图片文件',
			allowBlank: false,
			validator: function(value) {
			    var val = value.substring(value.length, value.toString().lastIndexOf("."));
				if(val.toString().toLowerCase() != '.png' &&
					val.toString().toLowerCase() != '.gif' &&
					val.toString().toLowerCase() != '.jpg' &&
					val.toString().toLowerCase() != '.bmp' &&
					val.toString().toLowerCase() != '.tiff' &&
					val.toString().toLowerCase() != '.pdf'
				) {
					return '文件格式不正确!只能上传txt、图片文件';
				} else {
					return true;
				}
			},
			listeners: {
				change: {
					element: 'el',
					fn: function(com, value, eOpts) {}
				}
			}
		}, {
			xtype: 'textarea',
			fieldLabel: 'entityJson',
			hidden: true,
			colspan: 3,
			name: 'entityJson'
		}, {
			xtype: 'textarea',
			fieldLabel: 'fields',
			hidden: true,
			colspan: 3,
			name: 'fields'
		}];
	},
	initFilterListeners: function() {
		var me = this;
		var FileName = me.getComponent('EAttachment_FileName');
		var FileExt = me.getComponent('EAttachment_FileExt');
		var FileSize = me.getComponent('EAttachment_FileSize');
		var FilePath = me.getComponent('EAttachment_FilePath');
		var FileType = me.getComponent('EAttachment_FileType');
		var FileNewName = me.getComponent('EAttachment_FileNewName');

		FileName.on({
			change: function(field, newValue, oldValue, eOpts) {
				if(newValue){
					var fileList = field.fileInputEl.dom.files;
					Ext.each(fileList, function(f) {
						var fileExtVal = f.name.substring(f.name.toString().lastIndexOf("."), f.name.length).toLowerCase();
						var newFileNameVal = f.name.substring(0, f.name.toString().lastIndexOf("."));
						FileNewName.setValue(newFileNameVal);
						FileExt.setValue(fileExtVal);
						FileName.setValue(f.name);
						FileType.setValue(f.type);
						FileSize.setValue(f.size);
					});
				}
			}
		});

	},
	/**更改标题*/
	changeTitle: function() {
		//不做处理
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			IsUse: 1
		};
		if(me.TempletID) {
			entity.ETemplet = {
				Id: me.TempletID,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0].join(',')
			};
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		//默认员工名称
		var UserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId) {
			entity.HREmployee = {
				Id: userId,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0].join(',')
			};
			entity.CreatorName = UserName;
		}
		if(values.EAttachment_FileName) {
			entity.FileName = values.EAttachment_FileName;
		}
		if(values.EAttachment_FileExt) {
			entity.FileExt = values.EAttachment_FileExt;
		}
		if(values.EAttachment_FileSize) {
			entity.FileSize = values.EAttachment_FileSize;
		}
		if(values.EAttachment_FilePath) {
			entity.FilePath = values.EAttachment_FilePath;
		}
		if(values.EAttachment_FileType) {
			entity.FileType = values.EAttachment_FileType;
		}
		if(values.EAttachment_FileNewName) {
			entity.FileNewName = values.EAttachment_FileNewName;
		}
		if(me.TempletType) {
			entity.TempletType = values.EAttachment_TempletType;
		}
		if(me.TempletTypeCode) {
			entity.TempletTypeCode = values.EAttachment_TempletTypeCode;
		}
		if(me.FileUploadDate){
			entity.FileUploadDate=JShell.Date.toServerDate(me.FileUploadDate);
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			//			fields = me.getStoreFields(),
			entity = me.getAddParams();
		var fields = [
			'FileName', 'IsUse', 'Id', 'TempletTypeCode', 'TempletType',
			'FileNewName', 'FileType', 'FilePath', 'FileSize', 'FileExt',
			'CreatorName', 'HREmployee_Id', 'ETemplet_Id'
		];

		entity.fields = fields.join(',');
		entity.entity.Id = values.EAttachment_Id;
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) return;
		var id = params.entity.Id;
		me.fireEvent('beforesave', me);

		if(me.formtype == 'add') {
			var File = me.getComponent('EAttachment_FileName').getValue();
			if(File == '') {
				JShell.Msg.error('附件不能为空!');
				return;
			}
		}
		me.getForm().setValues({
			entityJson: Ext.JSON.encode(params.entity),
			fields: params.fields || ''
		});
		me.showMask(me.saveText); //显示遮罩层
		me.getForm().submit({
			url: url,
			success: function(form, action) {
				me.hideMask(); //隐藏遮罩层
				var data = action.result;
				if(data.success) {
					me.fireEvent('save', me);
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, Form.hideTimes);
				} else {
					JShell.Msg.error(data.ErrorInfo);
				}
			},
			failure: function(form, action) {
				me.hideMask(); //隐藏遮罩层
				var data = action.result;
				JShell.Msg.error(data.ErrorInfo);
			}
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			var TempletType = me.getComponent('EAttachment_TempletType');
			var TempletTypeCode = me.getComponent('EAttachment_TempletTypeCode');
			if(TempletType) {
				TempletType.setValue(me.TempletType);
				TempletTypeCode.setValue(me.TempletTypeCode);
			}
		} else {
			me.getForm().setValues(me.lastData);
		}
	}
});