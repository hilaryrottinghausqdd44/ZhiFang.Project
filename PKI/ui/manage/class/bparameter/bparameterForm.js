/*******************************************************************************
 * 站点参数设置表单
 */
Ext.ns('Ext.manage');

Ext.define('Ext.manage.bparameter.bparameterForm', {
	extend : 'Ext.zhifangux.FormPanel',
	alias : 'widget.bparameterForm',
	title : '',
	defaultTitle : '',
	width : 360,
    comWidth:220,
    labelWidth:60,
    labelAlign:'right',
    isSuccessMsg:false,
	objectName : 'BParameter',
	addUrl : 'SingleTableService.svc/ST_UDTO_AddBParameter',
	editUrl : 'SingleTableService.svc/ST_UDTO_UpdateBParameterByField',
	selectUrl : 'SingleTableService.svc/ST_UDTO_SearchBParameterById',
	type : 'edit',
	bgFielName : '',
	initComponent : function() {
		var me = this;
        if(!me.tooltip){
            me.tooltip = Ext.create('Ext.ToolTip', {
                title:me.name,
                html:me.explain,
                target:me.getEl()
            });
        }
		if (me.type == 'show') {
			me.height -= 25;
		}
		me.fields = 'BParameter_Id,BParameter_DataTimeStamp,BParameter_LabID,BParameter_GroupNo,BParameter_Name,BParameter_SName,BParameter_ParaType,BParameter_BNodeTable_DataTimeStamp,BParameter_BNodeTable_Id,BParameter_Shortcode,BParameter_PinYinZiTou,BParameter_DispOrder,BParameter_IsUse,BParameter_DataAddTime,BParameter_DataUpdateTime,BParameter_ParaNo,BParameter_ParaValue,BParameter_ParaDesc';
		var btnBParameterChoose={
            xtype : 'button',
            type : 'button',
            itemId : 'btnBParameterChoose',
            name : 'btnBParameterChoose',
            text : '参数值字典选择',
            tooltip : '参数值为选择字典的Id值',
            iconCls : '',
            hidden:false,
            x : 130,
            y : 181,
            sortNum : 1000,
            listeners:{
                click:function(field,e){
                    
                    Ext.Loader.setConfig({enabled: true});//允许动态加载
                    Ext.Loader.setPath('Ext.selector.selectorApp', getRootPath() +'/ui/selector/class/selectorApp.js');
                    var ParaValue=me.getComponent("BParameter_ParaValue");
                    var defaultValue=ParaValue.getValue();
                    var config=
			            {
			            header:true,
                        title:'字典选择器',
                        
			            itemId:'selectorApp',
			            name:'selectorApp',
			            border:true,
                        defaultValue:defaultValue,
			            autoScroll :true,
			            collapsed:false,
                        modal:true,//模态
			            floating:true,//漂浮
			            closable:true,//有关闭按钮
			            resizable:true,//可变大小
			            draggable:true//可移动
			        };
                    var win=openWin('Ext.selector.selectorApp',config,null,null);
                    win.on({
                        okClick:function(panel,lastValue){
                            ParaValue.setValue(lastValue);
	                        panel.close();
		                },
                        cancelClick:function(panel,lastValue){
                            ParaValue.setValue(lastValue);
                            panel.close();
                        }
                    });
                }
            }
        };
        me.items = [{
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BParameter_Id',
					name : 'BParameter_Id',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 5,
					readOnly : false,
					fieldLabel : '主键ID',
					labelAlign : 'right',
					sortNum : 1,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BParameter_DataTimeStamp',
					name : 'BParameter_DataTimeStamp',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 31,
					readOnly : false,
					fieldLabel : '时间戳',
					labelAlign : 'right',
					sortNum : 2,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				}, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BParameter_LabID',
					name : 'BParameter_LabID',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 57,
					readOnly : false,
					fieldLabel : '实验室ID',
					labelAlign : 'right',
					sortNum : 3,
					hasReadOnly : false,
					hidden : true,
					width : 220,
					listeners : me.createBasicListeners()
				},
				
				 {
	                xtype:"combobox",
	                editable:true,
	                typeAhead:true,
	                queryMode:"local",
	                defaultValue:"",
	                displayField:"GMGroup_Name",
	                valueField:"GMGroup_Id",
	                store:me.createComboStore({
	                    fields:"GMGroup_Name,GMGroup_Id,GMGroup_DataTimeStamp",
	                    url:"SingleTableService.svc/ST_UDTO_SearchGMGroupByHQL?isPlanish=true",
	                    InteractionField:"BParameter_GroupNo",
	                    valueField:"GMGroup_Id"
	                }),
	                type:"combobox",
	                itemId:"BParameter_GroupNo",
	                name:"BParameter_GroupNo",
	                labelWidth:60,
	                height:22,
	                x : 5,
					y : 108,
	                readOnly:false,
	                labelStyle:"font-style:normal",
	                fieldLabel:"小组",
	                labelAlign:"right",
	                sortNum:4,
	                hasReadOnly:false,
	                hidden:false,
	                width:220,
	                listeners:me.createComboListeners()
	            } ,
					
//				{
//					xtype : 'textfield',
//					type : 'textfield',
//					itemId : 'BParameter_GroupNo',
//					name : 'BParameter_GroupNo',
//					labelWidth : 60,
//					height : 22,
//					x : 5,
//					y : 108,
//					readOnly : false,
//					fieldLabel : '检验小组',
//					labelAlign : 'right',
//					sortNum : 4,
//					hasReadOnly : false,
//					hidden : false,
//					width : 220,
//					listeners:{
//                        specialkey:function(field,e){
//                            var form = field.ownerCt;
//                            var items = form.items.items;
//                            navigationListeners(items,field,e);
//                        }
//                    }
//				}, 
				
				
				{
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BParameter_Name',
					name : 'BParameter_Name',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 5,
					readOnly : false,
					maxLength :100,
					fieldLabel : '参数名称',
					labelAlign : 'right',
					sortNum : 5,
					hasReadOnly : false,
					hidden : false,
					width : 220,
                    maxLength :100,
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
					itemId : 'BParameter_SName',
					name : 'BParameter_SName',
                    maxLength :100,
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 31,
					readOnly : false,
					fieldLabel : '参数简称',
					labelAlign : 'right',
					sortNum : 6,
					hasReadOnly : false,
					hidden : false,
					width : 220,
                    maxLength :100,
					listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, {
					xtype : 'combobox',
					hasButton : false,
					model : 'local',
					editable : false,
					displayField : 'text',
					valueField : 'value',
					store : new Ext.data.SimpleStore({
								fields : ['value', 'text'],
								data : [['SYS', '全系统'], 
                                        ['Construction', '构建'],
										['MEPT', '前处理'], 
                                        ['QC', '质控'],
										['RBAC', '权限']]
							}),
					type : 'datacombobox',
					itemId : 'BParameter_ParaType',
					name : 'BParameter_ParaType',
					labelWidth : 60,
                    value:'SYS',
					height : 22,
					x : 5,
					y : 57,
					readOnly : false,
					fieldLabel : '参数类型',
					labelAlign : 'right',
					sortNum : 7,
					hasReadOnly : false,
					hidden : false,
					width : 220,
                    maxLength :100,
					listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
				}, {
					xtype : 'combobox',
					editable : true,
                    value:'1',
					queryMode : 'local',
					defaultValue : '',
					displayField : 'BNodeTable_Name',
					valueField : 'BNodeTable_Id',
					DataTimeStampField : 'BParameter_BNodeTable_DataTimeStamp',
					store :me.createComboStore({
						fields : 'BNodeTable_Name,BNodeTable_Id,BNodeTable_DataTimeStamp',
						url : 'SingleTableService.svc/ST_UDTO_SearchBNodeTableByHQL?isPlanish=true',
						InteractionField : 'BParameter_BNodeTable_Id',
						DataTimeStampField : 'BParameter_BNodeTable_DataTimeStamp',
						valueField : 'BNodeTable_Id'
					}),
					type : 'combobox',
					itemId : 'BParameter_BNodeTable_Id',
					name : 'BParameter_BNodeTable_Id',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 83,
					readOnly : false,
					fieldLabel : '所属站点',
					labelAlign : 'right',
					sortNum : 8,
					hasReadOnly : false,
					hidden : false,
					width : 220,
					listeners : me.createComboListeners()
				},{
                    xtype : 'textfield',
                    type : 'textfield',
                    itemId : 'BParameter_BNodeTable_DataTimeStamp',
                    name : 'BParameter_BNodeTable_DataTimeStamp',
                    labelWidth : 60,
                    height : 22,
                    x : 5,
                    y : 31,
                    readOnly : false,
                    fieldLabel : '时间戳',
                    labelAlign : 'right',
                    sortNum : 2,
                    hasReadOnly : false,
                    hidden : true,
                    width : 220,
                    listeners : me.createBasicListeners()
                }, {
					xtype : 'textfield',
					type : 'textfield',
					itemId : 'BParameter_Shortcode',
					name : 'BParameter_Shortcode',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 109,
					readOnly : false,
					fieldLabel : '快捷码',
					labelAlign : 'right',
					sortNum : 9,
					hasReadOnly : false,
					hidden : true,
					width : 220,
                    maxLength :20,
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
					itemId : 'BParameter_PinYinZiTou',
					name : 'BParameter_PinYinZiTou',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 135,
					readOnly : false,
					fieldLabel : '拼音字头',
					labelAlign : 'right',
					sortNum : 10,
					hasReadOnly : false,
					hidden : true,
					width : 220,
                    maxLength :50,
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
					itemId : 'BParameter_DispOrder',
					name : 'BParameter_DispOrder',
					labelWidth : 60,
					height : 22,
					x : 5,
	                y : 132,
                    value:'1',
					readOnly : false,
					fieldLabel : '显示序号',
					labelAlign : 'right',
					sortNum : 11,
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
				},{
                    xtype : 'textfield',
                    type : 'textfield',
                    itemId : 'BParameter_ParaNo',
                    name : 'BParameter_ParaNo',
                    labelWidth : 60,
                    height : 22,
                    x : 5,
                    y : 157,
                    readOnly : false,
                    maxLength :48,
                    fieldLabel : '参数编码',
                    emptyText :'系统运行时请不要修改(由系统管理员维护)', 
                    labelAlign : 'right',
                    sortNum : 12,
                    hasReadOnly : false,
                    hidden : false,
                    width : 330,
                    maxLength :50,
                    listeners:{
                        specialkey:function(field,e){
                            var form = field.ownerCt;
                            var items = form.items.items;
                            navigationListeners(items,field,e);
                        }
                    }
                },{
					xtype : 'checkbox',
					boxLabel : '',
					inputValue : 'true',
					uncheckedValue : 'false',
					checked : false,
					type : 'checkboxfield',
					itemId : 'BParameter_IsUse',
					name : 'BParameter_IsUse',
					labelWidth : 60,
					height : 22,
					x : 5,
					y : 181,
					readOnly : false,
					fieldLabel : '是否使用',
					labelAlign : 'right',
					sortNum : 13,
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
				} ,btnBParameterChoose,{
					xtype : 'textarea',
					type : 'textareafield',
					itemId : 'BParameter_ParaValue',
					name : 'BParameter_ParaValue',
					labelWidth : 60,
					x : 5,
					y : 206,
                    maxLength :4000,
					fieldLabel : '参数值',
					labelAlign : 'right',
					sortNum : 16,
					hasReadOnly : false,
					hidden : false,
                    rows:8,
					width : 330,
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
					itemId : 'BParameter_ParaDesc',
					name : 'BParameter_ParaDesc',
					labelWidth : 60,
                    rows:7,
					x : 5,
					y : 358,
					readOnly : false,
                    maxLength :1000,
					fieldLabel : '参数说明',
					labelAlign : 'right',
					sortNum : 17,
					hasReadOnly : false,
					hidden : false,
					width : 330,
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
                            itemId : 'BParameter_DataAddTime',
                            name : 'BParameter_DataAddTime',
                            labelWidth : 60,
                            height : 22,
                            x : 5,
                            y : 213,
                            readOnly : false,
                            fieldLabel : '加入时间',
                            labelAlign : 'right',
                            sortNum : 13,
                            hasReadOnly : false,
                            hidden : true,
                            width : 220,
                            listeners:{
		                        specialkey:function(field,e){
		                            var form = field.ownerCt;
		                            var items = form.items.items;
		                            navigationListeners(items,field,e);
		                        }
		                    }
                        }), Ext.create('Ext.zhifangux.DateField', {
                            editable : false,
                            format : 'Y-m-d',
                            type : 'datefield',
                            itemId : 'BParameter_DataUpdateTime',
                            name : 'BParameter_DataUpdateTime',
                            labelWidth : 60,
                            height : 22,
                            x : 5,
                            y : 239,
                            readOnly : false,
                            fieldLabel : '更新时间',
                            labelAlign : 'right',
                            sortNum : 14,
                            hasReadOnly : false,
                            hidden : true,
                            width : 220,
                            listeners:{
		                        specialkey:function(field,e){
		                            var form = field.ownerCt;
		                            var items = form.items.items;
		                            navigationListeners(items,field,e);
		                        }
		                    }
                        })];
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
        var BNodeTable=me.getComponent("BParameter_BNodeTable_Id");
        if(BNodeTable){
            BNodeTable.on({
                    specialkey:function(field,e){
                        var form = field.ownerCt;
                        var items = form.items.items;
                        navigationListeners(items,field,e);
                    }
            });
        }
    }
});