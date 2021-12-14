/*******************************************************************************
 * 站点设置列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bnodeTable.bnodeTableList', {
	extend : 'Ext.zhifangux.GridPanel',
	alias : 'widget.bnodeTableList',
	title : '',
	width : 800,
	objectName : 'BNodeTable',
	openFormId : '',
	defaultLoad : true,
	defaultWhere : '',
    plugins: getCellEditing(),
	sortableColumns : true,
	initComponent : function() {
		var me = this;
		Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
		me.url = getRootPath()+ '/SingleTableService.svc/ST_UDTO_SearchBNodeTableByHQL?isPlanish=true&fields=BNodeTable_Name,BNodeTable_SName,BNodeTable_IP,BNodeTable_PinYinZiTou,BNodeTable_Shortcode,BNodeTable_IsUse,BNodeTable_HRDept_CName,BNodeTable_HRDept_Id,BNodeTable_HRDept_DataTimeStamp,BNodeTable_Id,BNodeTable_LabID,BNodeTable_DataAddTime,BNodeTable_DataTimeStamp,BNodeTable_Comment';
		me.searchArray = ['bnodetable.Name', 'bnodetable.SName',
				'bnodetable.Shortcode'];
		me.store = me.createStore({
			fields : ['BNodeTable_Name', 'BNodeTable_SName', 'BNodeTable_IP',
					'BNodeTable_PinYinZiTou', 'BNodeTable_Shortcode',
					'BNodeTable_IsUse', 'BNodeTable_HRDept_CName',
					'BNodeTable_HRDept_Id', 'BNodeTable_HRDept_DataTimeStamp',
					'BNodeTable_Id', 'BNodeTable_LabID',
					'BNodeTable_DataAddTime', 'BNodeTable_DataTimeStamp',
					'BNodeTable_Comment'],
			url : me.url,
            remoteSort : false,
			sorters : [],
			PageSize : 25,
			hasCountToolbar : true,
			buffered : false,
			leadingBufferZone : null
		});
		me.defaultColumns = [{
					xtype : 'rownumberer',
					text : '序号',
					width : 35,
					align : 'center'
				}, {
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
					hideable : true,
                    sortable : true,
					align : 'left'
				}, {
					text : '部门主键ID',
					dataIndex : 'BNodeTable_HRDept_Id',
					width : 100,
					hideable : true,
                    hidden:true,
                    sortable : true,
                    editor:{readOnly:true},
					align : 'left'
				}, {
					text : '部门时间戳',
					dataIndex : 'BNodeTable_HRDept_DataTimeStamp',
					width : 100,
					hideable : false,
                    hidden:true,
                    sortable : true,
					align : 'left'
				},{
					text : '实验室ID',
					dataIndex : 'BNodeTable_LabID',
					width : 100,
					hideable : false,
                    hidden:true,
                    sortable : true,
					align : 'left'
				}, {
					text : '加入时间',
					dataIndex : 'BNodeTable_DataAddTime',
					xtype : 'datecolumn',
					format : 'Y-m-d',
					width : 100,
					sortable : true,
					hideable : true,
					align : 'left'
				}, {
					text : '时间戳',
					dataIndex : 'BNodeTable_DataTimeStamp',
					width : 100,
					sortable : false,
                    hideable : false,
					hidden : true,
					align : 'left'
				}, {
					text : '备注',
					dataIndex : 'BNodeTable_Comment',
					width : 100,
                    hidden:true,
					sortable : true,
					hideable : true,
					align : 'left'
				}, {
                    text : '主键',
                    dataIndex : 'BNodeTable_Id',
                    width : 100,
                    hideable : true,
                    hidden:true,
                    sortable : true,
                    editor:{readOnly:true},
                    align : 'left'
                },  {
					xtype : 'actioncolumn',
					text : '操作',
					width : 60,
					align : 'center',
					sortable : false,
					hidden : false,
					hideable : false,
					items : [{
						type : 'edit',
						tooltip : '修改',
						iconCls : 'build-button-edit hand',
						handler : function(grid, rowIndex, colIndex, item, e,
								record) {
                                     me.fireEvent('editClick',grid,record); 
						}
					}, {
						type : 'show',
						tooltip : '查看',
						iconCls : 'build-button-see hand',
						handler : function(grid, rowIndex, colIndex, item, e,
								record) {
                                    me.fireEvent('showClick',grid,record);
						}
					}, {
						tooltip : '删除',
						iconCls : 'build-button-delete hand',
						handler : function(grid, rowIndex, colIndex, item, e,
								record) {
							Ext.Msg.confirm('提示', '确定要删除吗？', function(button) {
										if (button == 'yes') {
                                             var nodeId=record.get('BNodeTable_Id');
                                            //取系统的默认站点的Id值
                                            var allNodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
                                            if(allNodeId==nodeId){
                                                alertInfo('系统默认站点信息不能删除！');
                                            }else{
                                                
	                                            var callback = function() {
	                                                me.deleteIndex = rowIndex;
	                                                me.load(true);
	                                            };
	                                            me.deleteInfo(nodeId, callback);
                                            }
										}
									});
						}
					}]
				}];
		me.columns = me.createColumns();
		me.dockedItems = [{
					itemId : 'pagingtoolbar',
					xtype : 'toolbar',
					dock : 'bottom',
					items : [{
                    itemId : 'pagingtoolbar',
                    xtype : 'pagingtoolbar',
                    store : me.store,
                    dock : 'bottom',
                    displayInfo : true
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
							}, {
								type : 'add',
								itemId : 'add',
								text : '新增',
								iconCls : 'build-button-add',
								handler : function(but, e) {
									var record=null;
                                    me.fireEvent('addClick',me,record);
								}
							}, {
								type : 'edit',
								itemId : 'edit',
								text : '修改',
								iconCls : 'build-button-edit',
								handler : function(but, e) {
									var records = me.getSelectionModel().getSelection();
                                    var record=null;
                                    if (records.length == 1) {
                                        record = records[0];
                                    } else {
                                        record=null;
                                        alertInfo('请选择一条数据进行操作！');
                                    }
                                    me.fireEvent('editClick',me,record);
								}
							}, {
								type : 'show',
								itemId : 'show',
								text : '查看',
								iconCls : 'build-button-see',
								handler : function(but, e) {
									var records = me.getSelectionModel().getSelection();
                                    var record=null;
                                    if (records.length == 1) {
                                        record = records[0];
                                    } else {
                                        record=null;
                                        alertInfo('请选择一条数据进行操作！');
                                    }
                                    me.fireEvent('showClick',me,record);
								}
							}, {
								type : 'del',
								itemId : 'del',
								text : '删除',
								iconCls : 'build-button-delete',
								handler : function(but, e) {
									var records = me.getSelectionModel()
											.getSelection();
									if (records.length > 0) {
										Ext.Msg.confirm('提示', '确定要删除吗？',
												function(button) {
													var createFunction = function(
															id) {
														var f = function() {
															var rowIndex = me.store.find('BNodeTable_Id',id);
															me.deleteIndex = rowIndex;
															me.load(true);
															me.fireEvent('delClick');
														};
														return f;
													};
													if (button == 'yes') {
														for (var i in records) {
															var rowIndex=records[i].get('BNodeTable_Id');
				                                            //取系统的默认站点的Id值
				                                            var allNodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
				                                            if(allNodeId==rowIndex){
				                                                alertInfo('系统默认站点信息不能删除！');
				                                            }else{
				                                                var callback = function() {
				                                                    me.deleteIndex = rowIndex;
				                                                    me.load(true);
				                                                };
				                                                me.deleteInfo(rowIndex, callback);
				                                            }
														}
													}
												});
									} else {
										alertInfo('请选择数据进行操作！');
									}
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
		me.saveToTable = function(e, callback) {
			var url = 'SingleTableService.svc/ST_UDTO_UpdateBNodeTableByField';
			if (url != '') {
				url = getRootPath() + '/' + url;
			} else {
				alertError('没有配置获取数据服务地址!');
				return null;
			}
			//针对单个数据对象
            var record=rowIdx=e.record;
            var rowIdx=e.rowIdx;
            var changed=record.getChanges( );
            if(changed&&changed!=null&&changed!=undefined){
	            var Id=record.get("BNodeTable_Id");
	            var fields ="Id";//
	            var modified = record.modified,
	            changes  = {Id:Id};
	            for (field in modified) {
	                if (modified.hasOwnProperty(field)){
	                    //针对单个数据对象
	                    var arr=field.split("_");
	                    var newField=arr[arr.length-1];
	                    fields=fields+","+newField;
	                    changes[newField] = record.get(field);
	                }
	            }
	            var params = Ext.JSON.encode({
	                        entity : changes,
	                        fields : fields
	                    });
				var c = function(text) {
					var result = Ext.JSON.decode(text);
					if (result.success) {
                        record.commit();
                        me.getSelectionModel().select(rowIdx);
                        if (Ext.typeOf(callback) === 'function') {
                            callback();
                        }
					} else {
						alertError(result.ErrorInfo);
					}
				};
				postToServer(url, params, c, false);
            }
		};
		me.plugins = Ext.create('Ext.grid.plugin.RowEditing', {
					clicksToMoveEditor : 2,
					autoCancel : false,
					listeners : {
						canceledit : function() {
						},
						edit : function(editor, e) {
                            me.saveToTable(e,null);
						}
					}
				});;
		me.deleteInfo = function(id, callback) {
			var url = getRootPath()+ '/SingleTableService.svc/ST_UDTO_DelBNodeTable?id=' + id;
			var c = function(text) {
				var result = Ext.JSON.decode(text);
				if (result.success) {
					if (Ext.typeOf(callback) == 'function') {
						callback();
					}
				} else {
					alertError(result.ErrorInfo);
				}
			};
			getToServer(url, c);
		};
		me.addEvents('addClick');
		me.addEvents('afterOpenAddWin');
		me.addEvents('editClick');
		me.addEvents('afterOpenEditWin');
		me.addEvents('showClick');
		me.addEvents('afterOpenShowWin');
		me.addEvents('delClick');
		this.callParent(arguments);
	}
});