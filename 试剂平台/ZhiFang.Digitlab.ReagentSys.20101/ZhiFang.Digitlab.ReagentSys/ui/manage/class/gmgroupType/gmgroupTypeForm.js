/*******************************************************************************
 * 站点设置表单
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.gmgroupType.gmgroupTypeForm', {
	extend : 'Ext.zhifangux.FormPanel',
	alias : 'widget.gmgroupTypeForm',
	title : '',
	defaultTitle : '',
	width : 300,
    isSuccessMsg:false,
	objectName : 'GMGroupType',
	addUrl : 'SingleTableService.svc/ST_UDTO_AddGMGroupType',
	editUrl : 'SingleTableService.svc/ST_UDTO_UpdateGMGroupTypeByField',
	selectUrl : 'SingleTableService.svc/ST_UDTO_SearchGMGroupTypeById',
	type : 'edit',
	bgFielName : '',
    /**
     * 更改中文名称时拼音头处理
     * @private
     */
    cnameChangeForPinYinZiTou:function (obj,newValue){
        var me=this;
        var changePinYinZiTou = function(value){
            obj["GMGroupType_PinYinZiTou"]=value;
            me.getForm().setValues(obj);
        }
        if(newValue != ""){
            //util中获取拼音字头的公共方法
            getPinYinZiTouFromServer(newValue,changePinYinZiTou);
        }else{
            obj["GMGroupType_PinYinZiTou"]="";
            me.getForm().setValues(obj);
        }
    },
	initComponent : function() {
		var me = this;
		if (me.type == 'show') {
			me.height -= 25;
		}
		me.fields = 'GMGroupType_Name,GMGroupType_SName,GMGroupType_Shortcode,GMGroupType_PinYinZiTou,GMGroupType_IsUse,GMGroupType_DispOrder,GMGroupType_Comment,GMGroupType_DataAddTime,GMGroupType_DataUpdateTime,GMGroupType_Id,GMGroupType_DataTimeStamp,GMGroupType_LabID';
		me.items = [{
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'GMGroupType_Name',
					name : 'GMGroupType_Name',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 5,
					fieldLabel : '名称',
					labelAlign : 'right',
					sortNum : 1,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners:{
                        change:function(field,newValue,oldValue,eOpts){
                            if(newValue!=''&&me.isLoadingComplete==true&&(me.type=='edit'||me.type=='add')){
                                 var obj = {'GMGroupType_PinYinZiTou':""};
                                 me.cnameChangeForPinYinZiTou(obj,newValue)
                            }else{
                                me.isLoadingComplete=true;
                            }
                        },
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'GMGroupType_SName',
					name : 'GMGroupType_SName',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 31,
					readOnly : false,
					fieldLabel : '简称',
					labelAlign : 'right',
					sortNum : 2,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'GMGroupType_Shortcode',
					name : 'GMGroupType_Shortcode',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 57,
					readOnly : false,
					fieldLabel : '快捷码',
					labelAlign : 'right',
					sortNum : 3,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'GMGroupType_PinYinZiTou',
					name : 'GMGroupType_PinYinZiTou',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 83,
					fieldLabel : '拼音字头',
					labelAlign : 'right',
					sortNum : 4,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, {
					xtype : 'checkbox',
					boxLabel : '',
					inputValue : 'true',
					uncheckedValue : 'false',
					checked : false,
					type : 'checkboxfield',
					itemId : 'GMGroupType_IsUse',
					name : 'GMGroupType_IsUse',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 109,
					fieldLabel : '是否使用',
					labelAlign : 'right',
					sortNum : 5,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, {
					xtype : 'numberfield',
					type : 'numberfield',
					itemId : 'GMGroupType_DispOrder',
					name : 'GMGroupType_DispOrder',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 135,
					fieldLabel : '显示次序',
					labelAlign : 'right',
					sortNum : 6,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners : me.createBasicListeners()
				}, {
					xtype : 'textarea',
					type : 'textareafield',
					itemId : 'GMGroupType_Comment',
					name : 'GMGroupType_Comment',
					labelWidth : 60,
					rows:8,
					x : 5,
					y : 161,
					fieldLabel : '备注',
					labelAlign : 'right',
					sortNum : 7,
					hasReadOnly : false,
					hidden : false,
					width : 260,
					listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, Ext.create('Ext.zhifangux.DateField', {
							editable : false,
							format : 'Y-m-d',
							type : 'datefield',
							itemId : 'GMGroupType_DataAddTime',
							name : 'GMGroupType_DataAddTime',
							labelWidth : 60,
							height : 22,
							x : 5,
							y : 187,
							fieldLabel : '加入时间',
							labelAlign : 'right',
							sortNum : 8,
							hasReadOnly : false,
							hidden : true,
							width : 220,
							listeners : me.createBasicListeners()
						}), Ext.create('Ext.zhifangux.DateField', {
							editable : false,
							format : 'Y-m-d',
							type : 'datefield',
							itemId : 'GMGroupType_DataUpdateTime',
							name : 'GMGroupType_DataUpdateTime',
							labelWidth : 60,
							height : 22,
							x : 5,
							y : 213,
							fieldLabel : '更新时间',
							labelAlign : 'right',
							sortNum : 9,
							hasReadOnly : false,
							hidden : true,
							width : 220,
							listeners : me.createBasicListeners()
						}), {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'GMGroupType_Id',
					name : 'GMGroupType_Id',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 213,
					fieldLabel : '主键ID',
					labelAlign : 'right',
					sortNum : 10,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'GMGroupType_DataTimeStamp',
					name : 'GMGroupType_DataTimeStamp',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 291,
					fieldLabel : '时间戳',
					labelAlign : 'right',
					sortNum : 11,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'GMGroupType_LabID',
					name : 'GMGroupType_LabID',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 239,
					fieldLabel : '实验室ID',
					labelAlign : 'right',
					sortNum : 12,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				}];
		me.dockedItems = [{
					xtype : 'toolbar',
					dock : 'bottom',
					itemId : 'bottomtoolbar',
					items : ['->', {
								xtype : 'button',
								text : '保存',
								iconCls : 'build-button-save',
								handler : function(but) {
									me.submit(but);
								}
							}, {
								xtype : 'button',
								text : '重置',
								iconCls : 'build-button-refresh',
								handler : function(but) {
									me.getForm().reset();
								}
							}]
				}];
		me.changeConfig();
		me.callParent(arguments);
	}
});