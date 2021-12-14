/**
 * 模板信息维护
 * @author longfc
 * @version 2020-03-27
 */
Ext.define('Shell.class.blood.template.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '模板信息',
	requires: [
	    'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.BoolComboBox'
	],
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding:'15px 20px 0px 0px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBTemplateById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddTemplateUploadFile',
	/**修改服务地址*/
	editUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateTemplateAndUploadFileByField',
    /**布局方式*/
	layout: {
		type: 'table',
		columns:2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK:null,
	/**模板信息类型Key*/
	BTemplateType: "BTemplateType",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
    initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.BTemplateType, true, false, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '模板名称',name: 'BTemplate_CName',emptyText: '必填项',allowBlank: false,
			itemId: 'BTemplate_CName',colspan: 2,width: me.defaults.width * 2
		},{
			fieldLabel: '模板类型',xtype: 'uxSimpleComboBox',
			name: 'BTemplate_TypeID',itemId: 'BTemplate_TypeID',
			hasStyle: true,	allowBlank: false,colspan: 1,emptyText: '必填项',
			data: JShell.REA.StatusList.Status[me.BTemplateType].List,
			width: me.defaults.width * 1
		},{
			fieldLabel: '简称',name: 'BTemplate_SName',
			colspan: 1,width: me.defaults.width * 1
		}, {
			fieldLabel: '快捷码',name: 'BTemplate_Shortcode',
			colspan: 1,width: me.defaults.width * 1,
			itemId: 'BTemplate_Shortcode'
		},{
			fieldLabel: '拼音字头',name: 'BTemplate_PinYinZiTou',
			itemId: 'BTemplate_PinYinZiTou',colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel:'启用',name:'BTemplate_IsUse',
			xtype:'uxBoolComboBox',value:true,hasStyle:true,
			colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel:'显示次序',name:'BTemplate_DispOrder',emptyText:'必填项',
			allowBlank:false,xtype:'numberfield',value:0,
			colspan: 1,width: me.defaults.width * 1
		},  {
			fieldLabel: "文件",name: 'file',itemId: 'file',
			xtype: 'filefield',buttonText: '选择',emptyText: 'frx/xls/xlsx格式',
			colspan: 2,width: me.defaults.width * 2,
			validator: function(value) {
				if(!value) {
					return true;
				} else {
					var arr = value.split('.');
					var fileValue = arr[arr.length - 1].toString().toLowerCase();
					if(fileValue && (fileValue != 'frx'&&fileValue != 'xlsx'&&fileValue != 'xls')) {
						return '只能上传frx/xls/xlsx格式';
					} else {
						return true;
					}
				}
			}
		}, {
			fieldLabel:'备注',name: 'BTemplate_Comment',xtype:'textarea',
			colspan: 2,width: me.defaults.width *2,height:60
		},  {
			fieldLabel: '主键ID',name: 'BTemplate_Id',hidden: true,
			colspan: 1,width: me.defaults.width * 1
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
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			CName: values.BTemplate_CName,
			TypeID: values.BTemplate_TypeID,
//			TypeName: values.BTemplate_TypeName,
			SName: values.BTemplate_SName,
			Shortcode: values.BTemplate_Shortcode,
			PinYinZiTou: values.BTemplate_PinYinZiTou,
			Comment: values.BTemplate_Comment
		};
		var InType = me.getComponent('BTemplate_TypeID');
       
		var val = InType.getRawValue();
		entity.TypeName =val;
		if(values.BTemplate_IsUse) {
			var IsUse = values.BTemplate_IsUse;
			if(IsUse == true) IsUse = 1;
			if(IsUse == false) IsUse = 0;
			entity.IsUse = IsUse == 1 ? 1 : 0;
		}
		if(values.BTemplate_DispOrder) {
			entity.DispOrder = values.BTemplate_DispOrder;
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
		var noFields = ["file", "entity", "fields"];
		for(var i in fields) {
			if(noFields.indexOf(fields[i]) == -1) {
				var arr = fields[i].split('_');
				if(arr.length > 2) continue;
				fieldsArr.push(arr[1]);
			}
		}
		entity.fields = fieldsArr.join(',');
		entity.entity.Id = values.BTemplate_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me =this;
		data.BTemplate_IsUse = data.BTemplate_IsUse == '1' ? true : false;
		return data;
	},
		/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
		/**@overwrite */
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getForm().setValues({
			"entity": "",
			"entity": "fields"
		});
		
		me.getComponent("file").allowBlank=false;
		me.getComponent("file").setVisible(true);
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
  	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;
        var values = me.getForm().getValues();
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) return;
		var id = params.entity.Id;
		me.showMask(me.saveText); //显示遮罩层
		me.fireEvent('beforesave', me);
		
		var strparams = JcallShell.JSON.encode(params.entity);
		me.getForm().setValues({
			"entity": strparams,
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
		/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var CName = me.getComponent('BTemplate_CName');
		CName.on({
			change:function(field,newValue,oldValue,eOpts){
				newValue=me.changeCharCode(newValue);
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							//去掉特殊字符 &和？
		                    var valueStr=JcallShell.String.encode(value);    
							
							me.getForm().setValues({
								BTemplate_PinYinZiTou:valueStr,
								BTemplate_Shortcode:valueStr
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						BTemplate_PinYinZiTou:"",
						BTemplate_Shortcode:''
					});
				}
			}
		});
		
	},
	 /**中文符号转换为英文符号*/
	changeCharCode:function(val){
		var me =this;
        /*正则转换中文标点*/  
        val=val.replace(/：/g,':');  
        val=val.replace(/。/g,'.');  
        val=val.replace(/“/g,'"');  
        val=val.replace(/”/g,'"');  
        val=val.replace(/【/g,'[');  
        val=val.replace(/】/g,']');  
        val=val.replace(/《/g,'<');  
        val=val.replace(/》/g,'>');  
        val=val.replace(/，/g,',');  
        val=val.replace(/？/g,'?');  
        val=val.replace(/、/g,',');  
        val=val.replace(/；/g,';');  
        val=val.replace(/（/g,'(');  
        val=val.replace(/）/g,')');  
        val=val.replace(/‘/g,"'");  
        val=val.replace(/’/g,"'");  
        val=val.replace(/『/g,"[");  
        val=val.replace(/』/g,"]");  
        val=val.replace(/「/g,"[");  
        val=val.replace(/」/g,"]");  
        val=val.replace(/﹃/g,"[");  
        val=val.replace(/﹄/g,"]");  
        val=val.replace(/〔/g,"{");  
        val=val.replace(/〕/g,"}");  
        val=val.replace(/—/g,"-");  
        val=val.replace(/·/g,".");  
        /*正则转换全角为半角*/  
        //字符串先转化成数组  
        val=val.split("");  
        for(var i=0;i<val.length;i++){  
            //全角空格处理  
            if(val[i].charCodeAt(0)===12288){  
               val[i]=String.fromCharCode(32);    
            }  
            /*其他全角*/  
            if(val[i].charCodeAt(0)>0xFF00 && val[i].charCodeAt(0)<0xFFEF){  
               val[i]=String.fromCharCode(val[i].charCodeAt(0)-65248);  
            }  
        }  
        //数组转换成字符串  
        val=val.join("");  
        return val;
   }
});