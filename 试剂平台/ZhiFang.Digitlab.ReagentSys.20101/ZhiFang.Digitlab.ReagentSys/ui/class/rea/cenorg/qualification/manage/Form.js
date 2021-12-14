/***
 *  资质证件管理
 * @author longfc
 * @version 2017-07-14
 */
Ext.define('Shell.class.rea.cenorg.qualification.manage.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '资质证件信息',
	width: 260,
	bodyPadding:10,
	formtype: 'add',
	/**新增服务地址*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddGoodsQualificationAndUploadRegisterFile',
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsQualificationById?isPlanish=true',
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateGoodsQualificationAndUploadRegisterFileByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 65,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**机构信息*/
	CenOrg: {
		Id: '',
		Name: '',
		readOnly: false
	},
	/**供应商信息*/
	Comp: {
		Id: '',
		Name: '',
		readOnly: false
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '供应商',
			emptyText: '必填项',
			allowBlank: false,
			name: 'GoodsQualification_CompCName',
			itemId: 'GoodsQualification_CompCName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			locked: true,
			readOnly: true,
			value: JShell.REA.System.CENORG_NAME,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.cenorgcondition.ChildrenCheckGrid', {
					resizable: false,
					CenOrgId: JShell.REA.System.CENORG_ID,
					listeners: {
						accept: function(p, record) {
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '供应商主键ID',
			hidden: true,
			value: JShell.REA.System.CENORG_ID,
			name: 'GoodsQualification_Comp_Id',
			itemId: 'GoodsQualification_Comp_Id'
		}, {
			fieldLabel: '资质类型',
			name: 'GoodsQualification_ClassType',
			xtype: 'uxSimpleComboBox',
			value: '1',
			hasStyle: true,
			data: [
				['1', '机构资质', 'color:orange;'],
				['2', '授权资质', 'color:green;'],
				['3', '产品资质', 'color:blue;']
			]
		}, {
			fieldLabel: '编号',
			name: 'GoodsQualification_RegisterNo',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '显示名称',
			name: 'GoodsQualification_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '起始日期',
			name: 'GoodsQualification_RegisterDate',
			itemId: 'GoodsQualification_RegisterDate'
		}, {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '有效截止',
			name: 'GoodsQualification_RegisterInvalidDate'
		}, {
			fieldLabel: '所用医院',
			name: 'GoodsQualification_CenOrgCName',
			itemId: 'GoodsQualification_CenOrgCName',
			xtype: 'uxCheckTrigger',
			//className: 'Shell.class.rea.cenorg.CheckGrid'
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.cenorgcondition.ChildrenCheckGrid', {
					resizable: false,
					CenOrgId: JShell.REA.System.CENORG_ID,
					listeners: {
						accept: function(p, record) {
							me.onCenOrgAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '所用医院主键ID',
			hidden: true,
			name: 'GoodsQualification_CenOrg_Id',
			itemId: 'GoodsQualification_CenOrg_Id'
		}, {
			fieldLabel: '授权人',
			name: 'GoodsQualification_AuthorizeCName'
		}, {
			fieldLabel: '联系电话',
			name: 'GoodsQualification_Telephone'
		}, {
			height: 55,
			xtype: 'textarea',
			fieldLabel: '描述',
			name: 'GoodsQualification_Memo'
		}, {
			boxLabel: '是否使用',
			name: 'GoodsQualification_Visible',
			xtype: 'checkbox',
			inputValue: 1,
			checked: true,
			style: {
				marginLeft: '80px'
			}
		}, {
			fieldLabel: "证书原件",
			name: 'file',
			itemId: 'file',
			xtype: 'filefield',
			buttonText: '选择'
		}, {
			xtype: "button",
			width: 85,
			hidden: true,
			itemId: 'PreviewPDF',
			anchor: '55%',
			text: "查看证书",
			style: {
				marginLeft: '70px',
				borderColor: "transparent",
				//borderRadius: 0,
				color: "#337ab7"
			}
		}, {
			fieldLabel: '主键ID',
			name: 'GoodsQualification_Id',
			hidden: true
		}, {
			fieldLabel: '封装的实体信息',
			name: 'entity',
			xtype: 'textarea',
			hidden: true
		}, {
			fieldLabel: '封装的实体修改字段',
			name: 'fields',
			xtype: 'textarea',
			hidden: true
		}, {
			fieldLabel: '原件路径',
			name: 'GoodsQualification_RegisterFilePath',
			hidden: true
		});
		return items;
	},
	/**@overwrite */
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var date = JShell.System.Date.getDate();
		date = JcallShell.Date.toString(date, true);
		me.getForm().setValues({
			"GoodsQualification_RegisterDate": date,
			"GoodsQualification_Comp_Id": JShell.REA.System.CENORG_ID,
			'GoodsQualification_CompCName': JShell.REA.System.CENORG_NAME,
			"entity": "",
			"entity": "fields"
		});
		me.getComponent("file").setVisible(true);
		me.getComponent("PreviewPDF").setVisible(false);
	},
	/**@overwrite */
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getForm().setValues({
			"entity": "",
			"fields": ""
		});
		me.getComponent("file").setVisible(true);
	},
	/**@overwrite */
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.getForm().setValues({
			"entity": "",
			"fields": ""
		});
		me.getComponent("file").setVisible(false);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			Comp: {
				Id: values.GoodsQualification_Comp_Id
			},
			RegisterNo: values.GoodsQualification_RegisterNo,
			CName: values.GoodsQualification_CName,
			CenOrgCName: values.GoodsQualification_CenOrgCName,
			CompCName: values.GoodsQualification_CompCName,
			ClassType: values.GoodsQualification_ClassType,
			AuthorizeCName: values.GoodsQualification_AuthorizeCName,
			Telephone: values.GoodsQualification_Telephone
		};
		if(values.GoodsQualification_Visible) {
			var Visible = values.GoodsQualification_Visible;
			if(Visible == true) Visible = 1;
			if(Visible == false) Visible = 0;
			entity.Visible = Visible == 1 ? 1 : 0;
		}
		if(!entity.Comp.Id) {
			entity.Comp.Id = JShell.REA.System.CENORG_ID;
		}
		if(values.GoodsQualification_CenOrg_Id) {
			entity.CenOrg = {
				Id: values.GoodsQualification_CenOrg_Id
			};
		}
		if(values.GoodsQualification_RegisterDate) {
			entity.RegisterDate = JShell.Date.toServerDate(values.GoodsQualification_RegisterDate);
		}
		if(values.GoodsQualification_RegisterInvalidDate) {
			entity.RegisterInvalidDate = JShell.Date.toServerDate(values.GoodsQualification_RegisterInvalidDate);
		}
		//概要
		if(values.GoodsQualification_Memo) {
			entity.Memo = values.GoodsQualification_Memo.replace(/\\/g, '&#92');
			entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		var noFields = ["file", "entity", "fields", "PreviewPDF", "GoodsQualification_RegisterFilePath"];
		for(var i in fields) {
			if(noFields.indexOf(fields[i]) == -1) {
				fieldsArr.push(fields[i].split('_').slice(1).join('_'));
			}
		}
		entity.fields = fieldsArr.join(',');
		entity.entity.Id = values.GoodsQualification_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var reg = new RegExp("<br />", "g");
		data.GoodsQualification_Memo = data.GoodsQualification_Memo.replace(reg, "\r\n");
		data.GoodsQualification_Visible = data.GoodsQualification_Visible == '1' ? 1 : 0;
		data.GoodsQualification_RegisterDate = JShell.Date.getDate(data.GoodsQualification_RegisterDate);
		data.GoodsQualification_RegisterInvalidDate = JShell.Date.getDate(data.GoodsQualification_RegisterInvalidDate);
		var visible = false;
		if(data.GoodsQualification_RegisterFilePath && me.formtype != "add") visible = true;
		me.getComponent("PreviewPDF").setVisible(visible);
		return data;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;
		if(me.formtype == 'add' && !JcallShell.REA.System.CENORG_ID) {
			JcallShell.Msg.alert("登录人的中心机构ID信息为空!");
			return;
		}
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) return;
		var id = params.entity.Id;

		me.showMask(me.saveText); //显示遮罩层
		me.fireEvent('beforesave', me);
		me.getForm().setValues({
			"entity": JcallShell.JSON.encode(params.entity),
			"fields": params.fields
		});

		me.getForm().submit({
			url: url,
			timeout: 600, //10分钟,默认30秒
			//waitMsg: "保存处理中,请稍候...",
			success: function(form, action) {
				var data = action.result;
				me.hideMask();
				if(data.success) {
					id = me.formtype == 'add' ? data.value : id;
					if(Ext.typeOf(id) == 'object') {
						id = id.id;
					}
					if(me.isReturnData) {
						me.fireEvent('save', me, me.returnData(id));
					} else {
						me.fireEvent('save', me, id);
					}
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);

				} else {
					var msg = data.ErrorInfo;
					var index = msg.indexOf('UNIQUE KEY');
					if(index != -1) {
						msg = '有重复';
					}
					me.fireEvent('saveerror', me);
					JShell.Msg.error(data.msg);
				}
				me.getForm().setValues({
					"entity": "",
					"fields": ""
				});
			},
			failure: function(form, action) {
				me.hideMask();
				me.getForm().setValues({
					"entity": "",
					"fields": ""
				});
				var data = action.result;
				var msg = data.ErrorInfo;
				JShell.Msg.error('服务错误:' + msg);
			}
		});
	},
	/**供应商选择*/
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('GoodsQualification_Comp_Id');
		var ComName = me.getComponent('GoodsQualification_CompCName');

		ComId.setValue(record ? record.get('CenOrgCondition_cenorg2_Id') : '');
		ComName.setValue(record ? record.get('CenOrgCondition_cenorg2_CName') : '');
	},
	/**所用医院选择*/
	onCenOrgAccept: function(record) {
		var me = this;
		var CenOrg_CName = me.getComponent('GoodsQualification_CenOrgCName');
		var CenOrg_Id = me.getComponent('GoodsQualification_CenOrg_Id');

		CenOrg_Id.setValue(record ? record.get('CenOrgCondition_cenorg2_Id') : '');
		CenOrg_CName.setValue(record ? record.get('CenOrgCondition_cenorg2_CName') : '');
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var CenOrg_CName = me.getComponent('GoodsQualification_CenOrgCName');
		var CenOrg_Id = me.getComponent('GoodsQualification_CenOrg_Id');

		//		CenOrg_CName.on({
		//			check: function(p, record) {
		//				CenOrg_CName.setValue(record ? record.get('CenOrg_CName') : '');
		//				CenOrg_Id.setValue(record ? record.get('CenOrg_Id') : '');
		//				p.close();
		//			}
		//		});
		me.getComponent("PreviewPDF").on({
			click: function(buttton, e, eOpts) {
				var url = JShell.System.Path.getRootUrl("/ReagentSysService.svc/ST_UDTO_GoodsQualificationPreviewPdf");
				url += '?operateType=1&id=' + me.PK;
				window.open(url);
			}
		});
	}
});