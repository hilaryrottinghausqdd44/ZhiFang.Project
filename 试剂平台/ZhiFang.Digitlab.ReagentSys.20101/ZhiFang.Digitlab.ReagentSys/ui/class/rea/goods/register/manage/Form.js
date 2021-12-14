/***
 *  注册证管理
 * @author longfc
 * @version 2017-05-19
 */
Ext.define('Shell.class.rea.goods.register.manage.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '注册证信息',
	width: 260,
	bodyPadding: 5,
	formtype: 'add',
	/**新增服务地址*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddGoodsRegisterAndUploadRegisterFile',
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsRegisterById?isPlanish=true',
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateGoodsRegisterAndUploadRegisterFileByField',

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 75,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initPreviewPDFListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push( {
			fieldLabel: '中文名称',
			name: 'GoodsRegister_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '英文名称',
			name: 'GoodsRegister_EName',
			emptyText: '必填项',
			allowBlank: false
		},{
			fieldLabel: '产品编号[厂]',
			emptyText: '必填项',
			allowBlank: false,
			name: 'GoodsRegister_GoodsNo'
		}, {
			fieldLabel: '产品批号[厂]',
//			emptyText: '必填项',
//			allowBlank: false,
			name: 'GoodsRegister_GoodsLotNo'
		}, {
			fieldLabel: '注册证编号',
			name: 'GoodsRegister_RegisterNo',
			emptyText: '必填项',
			allowBlank: false
		}, {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '注册日期',
			name: 'GoodsRegister_RegisterDate',
			itemId: 'GoodsRegister_RegisterDate',
			emptyText: '必填项',
			allowBlank: false
		}, {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '有效期至',
			name: 'GoodsRegister_RegisterInvalidDate',
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'GoodsRegister_Visible',
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
			fieldLabel: '产品代码',
			hidden: true,
			name: 'GoodsRegister_ShortCode'
		}, {
			fieldLabel: '主键ID',
			name: 'GoodsRegister_Id',
			hidden: true
		}, {
			fieldLabel: '机构ID',
			name: 'GoodsRegister_CenOrgID',
			hidden: true
		},{
			fieldLabel: '机构编号',
			name: 'GoodsRegister_CenOrgNo',
			hidden: true
		},{
			fieldLabel: '封装的实体信息',
			name: 'entity',
			xtype: 'textarea',
			hidden: true
		}, {
			fieldLabel: '封装的实体修改字段',
			name: 'fields',
			xtype: 'textarea',
			hidden: true
		},{
			fieldLabel: '原件路径',
			name: 'GoodsRegister_RegisterFilePath',
			hidden: true
		},{
			xtype: "button",
			width:105,
			hidden:true,
			anchor: '75%',
			text:"查看证书原件",
			style: {
				marginLeft: '80px',
				borderColor: "transparent",
				borderRadius: 0,
				color: "#337ab7"
			},
			itemId: 'PreviewPDF'
		});
		return items;
	},
	initPreviewPDFListeners: function() {
		var me = this;
		me.getComponent("PreviewPDF").on({
			click: function(buttton, e, eOpts) {
				var url = JShell.System.Path.getRootUrl("/ReagentSysService.svc/ST_UDTO_GoodsRegisterPreviewPdf");
				url += '?operateType=1&id=' + me.PK;
				window.open(url);
			}
		});
	},
	/**@overwrite */
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var date = JShell.System.Date.getDate();
		date = JcallShell.Date.toString(date, true);
		me.getForm().setValues({
			"GoodsRegister_RegisterDate": date,
			"GoodsRegister_CenOrgID": JShell.REA.System.CENORG_ID,
			"GoodsRegister_CenOrgNo": JShell.REA.System.CENORG_CODE,
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
			CName: values.GoodsRegister_CName,
			GoodsNo: values.GoodsRegister_GoodsNo,
			EName: values.GoodsRegister_EName,
			ShortCode: values.GoodsRegister_ShortCode,
			GoodsLotNo: values.GoodsRegister_GoodsLotNo,
			RegisterNo: values.GoodsRegister_RegisterNo
		};
		if(values.GoodsRegister_Visible) {
			var Visible = values.GoodsRegister_Visible;
			if(Visible == true) Visible = 1;
			if(Visible == false) Visible = 0;
			entity.Visible = Visible == 1 ? 1 : 0;
		}
		if(values.GoodsRegister_CenOrgID) {
			entity.CenOrgID = values.GoodsRegister_CenOrgID;
		} else {
			entity.CenOrgID = JShell.REA.System.CENORG_ID;
		}
		if(values.GoodsRegister_CenOrgNo) {
			entity.CenOrgNo = values.GoodsRegister_CenOrgNo;
		} else {
			entity.CenOrgNo = JShell.REA.System.CENORG_CODE;
		}
		if(values.GoodsRegister_RegisterDate) {
			entity.RegisterDate = JShell.Date.toServerDate(values.GoodsRegister_RegisterDate);
		}
		if(values.GoodsRegister_RegisterInvalidDate) {
			entity.RegisterInvalidDate = JShell.Date.toServerDate(values.GoodsRegister_RegisterInvalidDate);
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
		var noFields=["file","entity","fields","PreviewPDF","GoodsRegister_RegisterFilePath"];
		for(var i in fields) {			
			if(noFields.indexOf(fields[i])==-1) {
				var arr = fields[i].split('_');
				if(arr.length > 2) continue;
				fieldsArr.push(arr[1]);
			}
		}
		entity.fields = fieldsArr.join(',');
		if(entity.fields) entity.fields += ",EmpID,EmpName";
		entity.entity.Id = values.GoodsRegister_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.GoodsRegister_Visible = data.GoodsRegister_Visible == '1' ? 1 : 0;
		data.GoodsRegister_RegisterDate = JShell.Date.getDate(data.GoodsRegister_RegisterDate);
		data.GoodsRegister_RegisterInvalidDate = JShell.Date.getDate(data.GoodsRegister_RegisterInvalidDate);
		var visible=false;
		if(data.GoodsRegister_RegisterFilePath&&me.formtype=="show")visible=true;
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
		if(me.formtype == 'add' && !JcallShell.REA.System.CENORG_CODE) {
			JcallShell.Msg.alert("登录人的中心机构编码为空!");
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
	}
});