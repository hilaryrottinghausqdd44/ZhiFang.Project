/*******************************************************************************
 * 小组类型列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.gmgroupType.gmgroupTypeList', {
	extend : 'Ext.zhifangux.GridPanel',
	alias : 'widget.gmgroupTypeList',
	title : '',
	width : 800,
	objectName : 'GMGroupType',
	openFormId : '',
	defaultLoad : true,
	defaultWhere : '',
	sortableColumns : false,
	initComponent : function() {
		var me = this;
		me.url = getRootPath()
				+ '/SingleTableService.svc/ST_UDTO_SearchGMGroupTypeByHQL?isPlanish=true&fields=GMGroupType_Id,GMGroupType_DataTimeStamp,GMGroupType_LabID,GMGroupType_Name,GMGroupType_SName,GMGroupType_Shortcode,GMGroupType_PinYinZiTou,GMGroupType_Comment,GMGroupType_DispOrder,GMGroupType_DataAddTime,GMGroupType_DataUpdateTime,GMGroupType_IsUse';
		me.searchArray = ['gmgrouptype.Name', 'gmgrouptype.SName',
				'gmgrouptype.Shortcode', 'gmgrouptype.PinYinZiTou'];
		me.store = me.createStore({
			fields : ['GMGroupType_Id', 'GMGroupType_DataTimeStamp',
					'GMGroupType_LabID', 'GMGroupType_Name',
					'GMGroupType_SName', 'GMGroupType_Shortcode',
					'GMGroupType_PinYinZiTou', 'GMGroupType_Comment',
					'GMGroupType_DispOrder', 'GMGroupType_DataAddTime',
					'GMGroupType_DataUpdateTime', 'GMGroupType_IsUse'],
			url : 'SingleTableService.svc/ST_UDTO_SearchGMGroupTypeByHQL?isPlanish=true&fields=GMGroupType_Id,GMGroupType_DataTimeStamp,GMGroupType_LabID,GMGroupType_Name,GMGroupType_SName,GMGroupType_Shortcode,GMGroupType_PinYinZiTou,GMGroupType_Comment,GMGroupType_DispOrder,GMGroupType_DataAddTime,GMGroupType_DataUpdateTime,GMGroupType_IsUse',
			remoteSort : false,
			sorters : [],
			PageSize : 25,
			hasCountToolbar : false,
			buffered : false,
			leadingBufferZone : null
		});
		me.defaultColumns = [{
					xtype : 'rownumberer',
					text : '序号',
					width : 35,
					align : 'center'
				}, {
					text : '主键ID',
					dataIndex : 'GMGroupType_Id',
					width : 100,
					sortable : false,
					hidden : true,
					hideable : true,
					align : 'left'
				}, {
					text : '时间戳',
					dataIndex : 'GMGroupType_DataTimeStamp',
					width : 100,
					sortable : false,
					hidden : true,
					hideable : false,
					align : 'left'
				}, {
					text : '实验室ID',
					dataIndex : 'GMGroupType_LabID',
					width : 100,
					sortable : false,
					hidden : true,
					hideable : true,
					align : 'left'
				}, {
					text : '名称',
					dataIndex : 'GMGroupType_Name',
					width : 174,
					sortable : true,
					hideable : true,
					editor : {
						allowBlank : true
					},
					align : 'left'
				}, {
					text : '简称',
					dataIndex : 'GMGroupType_SName',
					width : 100,
					sortable : true,
					hideable : true,
					editor : {
						allowBlank : true
					},
					align : 'left'
				}, {
					text : '快捷码',
					dataIndex : 'GMGroupType_Shortcode',
					width : 100,
					sortable : true,
					hideable : true,
					editor : {
						allowBlank : true
					},
					align : 'left'
				}, {
					text : '汉字拼音字头',
					dataIndex : 'GMGroupType_PinYinZiTou',
					width : 100,
					sortable : true,
					hideable : true,
					editor : {
						allowBlank : true
					},
					align : 'left'
				},  {
					text : '显示次序',
					dataIndex : 'GMGroupType_DispOrder',
					width : 100,
					sortable : true,
					hideable : true,
					align : 'left'
				}, {
                    text : '是否使用',
                    dataIndex : 'GMGroupType_IsUse',
                    width : 100,
                    sortable : true,
                    hideable : true,
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
                    align : 'left'
                }, {
					text : '加入时间',
					dataIndex : 'GMGroupType_DataAddTime',
					xtype : 'datecolumn',
					format : 'Y-m-d',
					width : 100,
					sortable : false,
					hideable : true,
					align : 'left'
				}, {
					text : '更新时间',
					dataIndex : 'GMGroupType_DataUpdateTime',
					xtype : 'datecolumn',
					format : 'Y-m-d',
					width : 100,
					sortable : false,
					hideable : true,
					align : 'left'
				}, {
                    text : '备注',
                    dataIndex : 'GMGroupType_Comment',
                    width : 200,
                    sortable : false,
                    hideable : true,
                    align : 'left'
                },{
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
											var id = record.get('GMGroupType_Id');
											var callback = function() {
												me.deleteIndex = rowIndex;
												me.load(true);
											};
											me.deleteInfo(id, callback);
										}
									});
						}
					}]
				}];
		me.columns = me.createColumns();
		me.dockedItems = [{
					itemId : 'pagingtoolbar',
					xtype : 'pagingtoolbar',
					store : me.store,
					dock : 'bottom',
					displayInfo : true
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
									var records = me.getSelectionModel().getSelection();
									if (records.length > 0) {
										Ext.Msg.confirm('提示', '确定要删除吗？',
												function(button) {
													var createFunction = function(
															id) {
														var f = function() {
															var rowIndex = me.store.find('GMGroupType_Id',id);
															me.deleteIndex = rowIndex;
															me.load(true);
															me.fireEvent('delClick');
														};
														return f;
													};
													if (button == 'yes') {
														for (var i in records) {
															var id = records[i].get('GMGroupType_Id');
															var callback = createFunction(id);
															me.deleteInfo(id,callback);
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
								emptyText : '名称/简称/快捷码/汉字拼音字头',
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
								tooltip : '按照名称/简称/快捷码/汉字拼音字头进行查询',
								handler : function(button) {
									me.search();
								}
							}]
				}];
		me.saveToTable = function(e, callback) {
			var url = 'SingleTableService.svc/ST_UDTO_UpdateGMGroupTypeByField';
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
	            var Id=record.get("GMGroupType_Id");
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
			var url = getRootPath()
					+ '/SingleTableService.svc/ST_UDTO_DelGMGroupType?id=' + id;
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