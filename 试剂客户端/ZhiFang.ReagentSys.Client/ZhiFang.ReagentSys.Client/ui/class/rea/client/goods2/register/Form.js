/**
 * 注册证
 * @author liangyl
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goods2.register.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '注册证信息',
	width: 260,
	/**内容周围距离*/
	bodyPadding:'10px',
	formtype: 'edit',
    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterById?isPlanish=true',
	 /**新增服务地址*/
    addUrl:'/ReaSysManageService.svc/ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 75,
		labelAlign: 'right'
	},
	/**内容周围距离*/
	bodyPadding:'10px 20px 10px 10px',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	 /**货品ID*/
	GoodsID:null,
    /**货品名称*/
	GoodsCName:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initPreviewPDFListeners();
		//初始化检索监听
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '厂家',name: 'ReaGoodsRegister_FactoryName',itemId: 'ReaGoodsRegister_FactoryName',
			emptyText: '必填项',allowBlank: false,xtype: 'uxCheckTrigger',
			colspan: 1, width: me.defaults.width * 1,
			className: 'Shell.class.rea.client.reacenorg.basic.CheckGrid',
			classConfig: {
				title: '厂家选择',checkOne:true,
				width:550
			}
		},{
			fieldLabel: '供应商',name: 'ReaGoodsRegister_CompanyName',itemId: 'ReaGoodsRegister_CompanyName',
			emptyText: '必填项',allowBlank: false,xtype: 'uxCheckTrigger',
			colspan: 1, width: me.defaults.width * 1,
			className: 'Shell.class.rea.client.reacenorg.basic.CheckGrid',
			classConfig: {
				title: '供应商选择',checkOne:true,
				width:550
			}
		},{
			fieldLabel: '中文名称',
			name: 'ReaGoodsRegister_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '英文名称',
			name: 'ReaGoodsRegister_EName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '货品编码[厂]',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaGoodsRegister_GoodsNo'
		}, {
			fieldLabel: '货品批号[厂]',
			//			emptyText: '必填项',
			//			allowBlank: false,
			name: 'ReaGoodsRegister_GoodsLotNo'
		}, {
			fieldLabel: '注册证编号',
			name: 'ReaGoodsRegister_RegisterNo',
			emptyText: '必填项',
			allowBlank: false
		}, {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '注册日期',
			name: 'ReaGoodsRegister_RegisterDate',
			itemId: 'ReaGoodsRegister_RegisterDate',
			emptyText: '必填项',
			allowBlank: false
		}, {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '有效期至',
			name: 'ReaGoodsRegister_RegisterInvalidDate',
			emptyText: '必填项',
			allowBlank: false
		}, {
			boxLabel: '是否使用',
			name: 'ReaGoodsRegister_Visible',
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
			buttonText: '选择',
			emptyText: 'pdf格式',
			validator: function(value) {
				if(!value) {
					return true;
				} else {
					var arr = value.split('.');
					var fileValue = arr[arr.length - 1].toString().toLowerCase();
					if(fileValue && fileValue != 'pdf') {
						return '只能上传pdf格式';
					} else {
						return true;
					}
				}
			}
		}, {
			fieldLabel: '货品代码',
			hidden: true,
			name: 'ReaGoodsRegister_ShortCode'
		}, {
			fieldLabel: '主键ID',
			name: 'ReaGoodsRegister_Id',
			hidden: true
		},
		{
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
			fieldLabel: '厂家',
			name: 'ReaGoodsRegister_FactoryID',itemId: 'ReaGoodsRegister_FactoryID',
			hidden: true
		},{
			fieldLabel: '供应商',
			name: 'ReaGoodsRegister_CompID',itemId: 'ReaGoodsRegister_CompID',
			hidden: true
		},{
			fieldLabel: '原件路径',
			name: 'ReaGoodsRegister_RegisterFilePath',
			hidden: true
		}, {
			xtype: "button",
			width: 105,
			hidden: true,
			anchor: '75%',
			text: "查看证书原件",
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
				var url = JShell.System.Path.getRootUrl("/ReaSysManageService.svc/ST_UDTO_ReaGoodsRegisterPreviewPdf");
				url += '?operateType=1&id=' + me.PK;
				window.open(url);
			}
		});
	},
	  /**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//厂家
		var FactoryName = me.getComponent('ReaGoodsRegister_FactoryName');
		var FactoryID= me.getComponent('ReaGoodsRegister_FactoryID');
		if(!FactoryName) return;
		FactoryName.on({
			check: function(p, record) {
				FactoryName.setValue(record ? record.get('ReaCenOrg_CName') : '');
				FactoryID.setValue(record ? record.get('ReaCenOrg_Id') : '');
				p.close();
			}
		});
       //供货方
		var CompanyName = me.getComponent('ReaGoodsRegister_CompanyName');
		var CompID= me.getComponent('ReaGoodsRegister_CompID');
		if(!CompanyName) return;
		CompanyName.on({
			check: function(p, record) {
				CompanyName.setValue(record ? record.get('ReaCenOrg_CName') : '');
				CompID.setValue(record ? record.get('ReaCenOrg_Id') : '');
				p.close();
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
			"ReaGoodsRegister_RegisterDate": date,
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
//			Id: -1,
			CName: values.ReaGoodsRegister_CName,
			GoodsNo: values.ReaGoodsRegister_GoodsNo,
			EName:   values.ReaGoodsRegister_EName,
			ShortCode: values.ReaGoodsRegister_ShortCode,
			GoodsLotNo: values.ReaGoodsRegister_GoodsLotNo,
			RegisterNo: values.ReaGoodsRegister_RegisterNo
		};
		if(values.ReaGoodsRegister_Visible) {
			var Visible = values.ReaGoodsRegister_Visible;
			if(Visible == true) Visible = 1;
			if(Visible == false) Visible = 0;
			entity.Visible = Visible == 1 ? 1 : 0;
		}
        if(values.ReaGoodsRegister_CompID) {
        	entity.CompID = values.ReaGoodsRegister_CompID;
			entity.CompanyName = values.ReaGoodsRegister_CompanyName;
		}
        if(values.ReaGoodsRegister_FactoryID) {
        	entity.FactoryID = values.ReaGoodsRegister_FactoryID;
			entity.FactoryName = values.ReaGoodsRegister_FactoryName;
		}
		if(values.ReaGoodsRegister_RegisterDate) {
			entity.RegisterDate = JShell.Date.toServerDate(values.ReaGoodsRegister_RegisterDate);
		}
		if(values.ReaGoodsRegister_RegisterInvalidDate) {
			entity.RegisterInvalidDate = JShell.Date.toServerDate(values.ReaGoodsRegister_RegisterInvalidDate);
		}
		if(me.GoodsID){
			entity.ReaGoods = {
				Id:me.GoodsID,
				DataTimeStamp:"['0','0','0','0','0','0','0','0']"
			};
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
		var noFields = ["file", "entity", "fields", "PreviewPDF", "ReaGoodsRegister_RegisterFilePath"];
		for(var i in fields) {
			if(noFields.indexOf(fields[i]) == -1) {
				var arr = fields[i].split('_');
				if(arr.length > 2) continue;
				fieldsArr.push(arr[1]);
			}
		}
		entity.fields = fieldsArr.join(',');
//		if(entity.fields) entity.fields += ",EmpID,EmpName";
		entity.entity.Id = values.ReaGoodsRegister_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ReaGoodsRegister_Visible = data.ReaGoodsRegister_Visible == '1' ? 1 : 0;
		data.ReaGoodsRegister_RegisterDate = JShell.Date.getDate(data.ReaGoodsRegister_RegisterDate);
		data.ReaGoodsRegister_RegisterInvalidDate = JShell.Date.getDate(data.ReaGoodsRegister_RegisterInvalidDate);
		var visible = false;
		if(data.ReaGoodsRegister_RegisterFilePath && me.formtype == "show") visible = true;
		me.getComponent("PreviewPDF").setVisible(visible);
		return data;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;
        var values = me.getForm().getValues();
        if(values.ReaGoodsLot_InvalidDate<values.ReaGoodsLot_ProdDate){
            JShell.Msg.error("开始时间不能大于结束时间!");
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
	/**更改标题*/
	changeTitle:function(){
	}
});