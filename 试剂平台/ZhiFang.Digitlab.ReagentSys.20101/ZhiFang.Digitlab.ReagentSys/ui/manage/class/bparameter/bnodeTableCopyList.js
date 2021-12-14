/*******************************************************************************
 * 站点设置列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bparameter.bnodeTableCopyList', {
	extend : 'Ext.zhifangux.GridPanel',
	alias : 'widget.bnodeTableCopyList',
	title : '',
	width : 320,
	objectName : 'BNodeTable',
	openFormId : '',
	defaultLoad : true,
	defaultWhere : '',
	sortableColumns : true,
	initComponent : function() {
		var me = this;
		Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
		me.url = getRootPath()+ '/SingleTableService.svc/ST_UDTO_SearchBNodeTableByHQL?isPlanish=true&fields=BNodeTable_Name,BNodeTable_SName,BNodeTable_IP,BNodeTable_PinYinZiTou,BNodeTable_Shortcode,BNodeTable_IsUse,BNodeTable_HRDept_CName,BNodeTable_HRDept_Id,BNodeTable_HRDept_DataTimeStamp,BNodeTable_Id,BNodeTable_LabID,BNodeTable_DataTimeStamp';
		me.searchArray = ['bnodetable.Name', 'bnodetable.SName',
				'bnodetable.Shortcode'];
		me.store = me.createStore({
			fields : ["checkSelect",'BNodeTable_Name', 'BNodeTable_SName', 'BNodeTable_IP',
					'BNodeTable_PinYinZiTou', 'BNodeTable_Shortcode',
					'BNodeTable_IsUse', 'BNodeTable_HRDept_CName',
					'BNodeTable_HRDept_Id', 'BNodeTable_HRDept_DataTimeStamp',
					'BNodeTable_Id', 'BNodeTable_DataTimeStamp'],
			url : me.url,
            remoteSort : false,
			sorters : [],
			PageSize : 8000,
			hasCountToolbar : true,
			buffered : false,
			leadingBufferZone : null
		});
        me.getStore().on({
            load:function(s, records, successful, eOpts) {
                if (!successful) {
                    Ext.Msg.alert("提示", "获取数据错误！");
                }else{
                    if(s.getCount() > 0){
                        var nodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
                        var removeRecord=null;
                        s.each(function(record){
                            var value=record.get("BNodeTable_Id");
                            if(nodeId==value){
                                removeRecord=record;
                            }
                        });
                        s.remove(removeRecord);
                    }
                }
            }
        });
		me.defaultColumns = [{
					xtype : 'rownumberer',
					text : '序号',
					width : 35,
					align : 'center'
				}, {
		            text : '选择',
		            dataIndex : 'checkSelect',
		            width : 40,
		            editor:{
		                xtype:'checkbox',
		                cls:'x-grid-checkheader-editor'
		            },
		            align:me.columnAlign,
		            xtype:'checkcolumn'
		        },{
					text : '名称',
					dataIndex : 'BNodeTable_Name',
					width : 155,
					hideable : true,
                    sortable : true,
                    editor : {
                            allowBlank : true
                        },
					align : 'left'
				}, {
					text : '简称',
					dataIndex : 'BNodeTable_SName',
					width : 100,
					hideable : true,
                    sortable : true,
                    editor : {
                            allowBlank : true
                        },
					align : 'left'
				}, {
					text : 'IP地址',
					dataIndex : 'BNodeTable_IP',
					width : 100,
					sortable : true,
                    editor : {
                            allowBlank : true
                        },
					hideable : true,
					align : 'left'
				}, {
					text : '汉字拼音字头',
					dataIndex : 'BNodeTable_PinYinZiTou',
					width : 100,
					sortable : true,
					hideable : true,
                    editor : {
                            allowBlank : true
                        },
					align : 'left'
				}, {
					text : '快捷码',
					dataIndex : 'BNodeTable_Shortcode',
					width : 100,
                    sortable : true,
					hideable : true,
                    editor : {
                            allowBlank : true
                        },
					align : 'left'
				}, {
					text : '是否使用',
                    sortable : true,
					dataIndex : 'BNodeTable_IsUse',
					xtype : 'booleancolumn',
					trueText : '是',
					falseText : '否',
					defaultRenderer : function(value) {
						if (value === undefined) {
							return this.undefinedText;
						}
						if (!value || value === 'false' || value === '0'
								|| value === 0) {
							return this.falseText;
						}
						return this.trueText;
					},
					width : 67,
					hideable : true,
					align : 'left'
				}, {
					text : '所属部门',
					dataIndex : 'BNodeTable_HRDept_CName',
					width : 100,
					sortable : true,
					hideable : true,
					align : 'left'
				}, {
					text : '主键ID',
					dataIndex : 'BNodeTable_HRDept_Id',
					width : 100,
					sortable : false,
					hidden : true,
                    hideable : false,
					align : 'left'
				}, {
					text : '时间戳',
					dataIndex : 'BNodeTable_HRDept_DataTimeStamp',
					width : 100,
					sortable : false,
					hidden : true,
                    hideable : false,
					align : 'left'
				},{
					text : '实验室ID',
					dataIndex : 'BNodeTable_LabID',
					width : 100,
					sortable : false,
					hidden : true,
                    hideable : false,
					align : 'left'
				}, {
					text : '时间戳',
					dataIndex : 'BNodeTable_DataTimeStamp',
					width : 100,
					sortable : false,
					hidden : true,
                    hideable : false,
					align : 'left'
				}, {
                    text : '主键',
                    dataIndex : 'BNodeTable_Id',
                    width : 100,
                    sortable : false,
                    hideable : false,
                    hidden : true,
                    editor:{readOnly:true},
                    align : 'left'
                }];
		me.columns = me.createColumns();
		me.dockedItems = [{
					itemId : 'pagingtoolbar',
					xtype : 'toolbar',
					dock : 'bottom',
					items : [{
								xtype : 'label',
								itemId : 'count',
								text : '共0条'
							}]
				}, {
					xtype : 'toolbar',
					itemId : 'buttonstoolbar',
					dock : 'top',
					items : [{
								type : 'refresh',
								itemId : 'refresh',
								text : '更新',
								iconCls : 'build-button-refresh',
								handler : function(but, e) {
									var com = but.ownerCt.ownerCt;
									com.load(true);
								}
							}, '->', {
								xtype : 'textfield',
								itemId : 'searchText',
								width : 160,
								emptyText : '名称/简称/快捷码',
								listeners : {
									render : function(input) {
										new Ext.KeyMap(input.getEl(), [{
													key : Ext.EventObject.ENTER,
													fn : function() {
														me.search();
													}
												}]);
									}
								}
							}, {
								xtype : 'button',
								text : '查询',
								iconCls : 'search-img-16 ',
								tooltip : '按照名称/简称/快捷码进行查询',
								handler : function(button) {
									me.search();
								}
							}]
				}];
		this.callParent(arguments);
	}
});