/*******************************************************************************
 * 站点设置表单
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bnodeTable.bnodeTableForm', {
	extend : 'Ext.zhifangux.FormPanel',
	alias : 'widget.bnodeTableForm',
	title : '',
	defaultTitle : '',
	width : 300,
    isSuccessMsg:false,
	objectName : 'BNodeTable',
	addUrl : 'SingleTableService.svc/ST_UDTO_AddBNodeTable',
	editUrl : 'SingleTableService.svc/ST_UDTO_UpdateBNodeTableByField',
	selectUrl : 'SingleTableService.svc/ST_UDTO_SearchBNodeTableById',
	type : 'edit',
	bgFielName : '',
    /**
     * 更改中文名称时拼音头处理
     * @private
     */
    cnameChangeForPinYinZiTou:function (obj,newValue){
        var me=this;
        var changePinYinZiTou = function(value){
            obj["BNodeTable_PinYinZiTou"]=value;
            me.getForm().setValues(obj);
        }
        if(newValue != ""){
            //util中获取拼音字头的公共方法
            getPinYinZiTouFromServer(newValue,changePinYinZiTou);
        }else{
            obj["BNodeTable_PinYinZiTou"]="";
            me.getForm().setValues(obj);
        }
    },
	initComponent : function() {
		var me = this;
		if (me.type == 'show') {
			me.height -= 25;
		}
		me.fields = 'BNodeTable_Name,BNodeTable_HRDept_Id,BNodeTable_SName,BNodeTable_IP,BNodeTable_Shortcode,BNodeTable_PinYinZiTou,BNodeTable_DataAddTime,BNodeTable_IsUse,BNodeTable_Comment,BNodeTable_HRDept_DataTimeStamp,BNodeTable_DataTimeStamp,BNodeTable_Id,BNodeTable_LabID';
		me.items = [{
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BNodeTable_Name',
					name : 'BNodeTable_Name',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 5,
					readOnly : false,
                    maxLength :40,
					fieldLabel : '名称',
					labelAlign : 'right',
					sortNum : 1,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners:{
                        change:function(field,newValue,oldValue,eOpts){
                            if(newValue!=''&&me.isLoadingComplete==true&&(me.type=='edit'||me.type=='add')){
                                 var obj = {'BNodeTable_PinYinZiTou':""};
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
					xtype : 'combobox',
					editable : true,
					typeAhead : true,
					queryMode : 'local',
					defaultValue : '',
					displayField : 'HRDept_CName',
					valueField : 'HRDept_Id',
					DataTimeStampField : 'BNodeTable_HRDept_DataTimeStamp',
					store : me.createComboStore({
						fields : 'HRDept_CName,HRDept_Id,HRDept_DataTimeStamp',
						url : 'RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true',
						InteractionField : 'BNodeTable_HRDept_Id',
						DataTimeStampField : 'BNodeTable_HRDept_DataTimeStamp',
						valueField : 'HRDept_Id'
					}),
					type : 'combobox',
					itemId : 'BNodeTable_HRDept_Id',
					name : 'BNodeTable_HRDept_Id',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 31,
					readOnly : false,
					fieldLabel : '所属部门',
					labelAlign : 'right',
					sortNum : 2,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					//下拉框特有,时间戳匹配处理
                    listeners : me.createComboListeners()
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BNodeTable_SName',
					name : 'BNodeTable_SName',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 57,
					readOnly : false,
                    maxLength :40,
					fieldLabel : '简称',
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
					itemId : 'BNodeTable_IP',
					name : 'BNodeTable_IP',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 83,
					readOnly : false,
					fieldLabel : 'IP地址',
                    maxLength :20,
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
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BNodeTable_Shortcode',
					name : 'BNodeTable_Shortcode',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 109,
					readOnly : false,
                    maxLength :20,
					fieldLabel : '快捷码',
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
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BNodeTable_PinYinZiTou',
					name : 'BNodeTable_PinYinZiTou',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 135,
					readOnly : false,
					fieldLabel : '拼音字头',
					labelAlign : 'right',
					sortNum : 6,
                    maxLength :50,
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
				}, Ext.create('Ext.zhifangux.DateField', {
							editable : false,
							format : 'Y-m-d',
							type : 'datefield',
							itemId : 'BNodeTable_DataAddTime',
							name : 'BNodeTable_DataAddTime',
							labelWidth : 60,
							height : 22,
							x : 5,
							y : 161,
							fieldLabel : '加入时间',
							labelAlign : 'right',
							sortNum : 7,
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
						}), {
					xtype : 'checkbox',
					boxLabel : '',
					inputValue : 'true',
					uncheckedValue : 'false',
					checked : false,
					type : 'checkboxfield',
					itemId : 'BNodeTable_IsUse',
					name : 'BNodeTable_IsUse',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 187,
					readOnly : false,
					fieldLabel : '是否使用',
					labelAlign : 'right',
					sortNum : 8,
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
					xtype : 'textarea',
					type : 'textareafield',
					itemId : 'BNodeTable_Comment',
					name : 'BNodeTable_Comment',
					labelWidth : 60,
					x : 5,
					y : 213,
					readOnly : false,
                    rows:8,
					fieldLabel : '备注',
					labelAlign : 'right',
					sortNum : 9,
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
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BNodeTable_HRDept_DataTimeStamp',
					name : 'BNodeTable_HRDept_DataTimeStamp',
					labelWidth : 60,
					height : 22,
					x : 0,
					y : 0,
					readOnly : false,
					fieldLabel : '所属部门_时间戳',
					labelAlign : 'right',
					sortNum : 10,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BNodeTable_DataTimeStamp',
					name : 'BNodeTable_DataTimeStamp',
					labelWidth : 60,
					height : 22,
					x : 0,
					y : 0,
					readOnly : false,
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
					itemId : 'BNodeTable_Id',
					name : 'BNodeTable_Id',
					labelWidth : 60,
					height : 22,
					x : 0,
					y : 0,
					readOnly : false,
					fieldLabel : '主键ID',
					labelAlign : 'right',
					sortNum : 12,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BNodeTable_LabID',
					name : 'BNodeTable_LabID',
					labelWidth : 60,
					height : 22,
					x : 0,
					y : 0,
					readOnly : false,
					fieldLabel : '实验室ID',
					labelAlign : 'right',
					sortNum : 13,
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
	},
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.initLink();
        
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
    },
    initLink:function() {
        var me = this;
        var HRDept=me.getComponent("BNodeTable_HRDept_Id");
        if(HRDept){
            HRDept.on({
                    specialkey:function(field,e){
                        var form = field.ownerCt;
                        var items = form.items.items;
                        navigationListeners(items,field,e);
                    }
            });
        }
    }
});