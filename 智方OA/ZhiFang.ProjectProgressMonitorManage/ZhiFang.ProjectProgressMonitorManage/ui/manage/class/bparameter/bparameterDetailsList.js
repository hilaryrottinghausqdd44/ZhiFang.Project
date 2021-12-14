/*******************************************************************************
 * 站点参数设置列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bparameter.bparameterDetailsList', {
	extend : 'Ext.zhifangux.GridPanel',
	alias : 'widget.bparameterDetailsList',
	title : '',
	width : 680,
	objectName : 'BParameter',
	openFormId : '',
	defaultLoad : false,
   /**默认选中的数据行,可以是true(选中)、false(不选)、数字(下标)[正数从上往下、负数从下往上,例如-1就是选中length-1行数据]、字符串(主键ID)*/
    autoSelect:true,
	defaultWhere : '',
	sortableColumns : true,
    /**
     * 渲染完后处理
     * @private
     */
    afterRender:function(){
        var me = this;
        me.callParent(arguments);
        
        //数据集监听
        me.store.on({
            beforeload:function(store,operation){me.beforeLoad(store,operation);},
            load:function(store,records,successful,eOpts){
                if(successful){
                    if(store&&store!=null){
                        if(me.autoSelect >= 0){
			                me.getSelectionModel().select(me.autoSelect);
			            }else{
                            me.getSelectionModel().select(0);
                        }
                    }
                    
                }
            }
        });
    },
	initComponent : function() {
		var me = this;
		Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
		me.url = getRootPath()
				+ '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true&fields=BParameter_Id,BParameter_LabID,BParameter_GroupNo,BParameter_Name,BParameter_SName,BParameter_PinYinZiTou,BParameter_ParaType,BParameter_Shortcode,BParameter_IsUse,BParameter_DataAddTime,BParameter_DataUpdateTime,BParameter_BNodeTable_Id,BParameter_BNodeTable_Name,BParameter_BNodeTable_IP,BParameter_ParaNo,BParameter_DispOrder';
		me.searchArray = ['bparameter.Name', 'bparameter.SName',
				'bparameter.ParaType', 'bparameter.Shortcode',
				'bparameter.PinYinZiTou'];
		me.store = me.createStore({
			fields : ['BParameter_Id', 'BParameter_LabID',
					'BParameter_GroupNo', 'BParameter_Name',
					'BParameter_SName', 'BParameter_PinYinZiTou',
					'BParameter_ParaType', 'BParameter_Shortcode',
					'BParameter_IsUse', 'BParameter_DataAddTime',
					'BParameter_DataUpdateTime', 'BParameter_BNodeTable_Id',
					'BParameter_BNodeTable_Name', 'BParameter_BNodeTable_IP',
					 'BParameter_ParaNo',
					'BParameter_DispOrder'],
			url : me.url,
            remoteSort : false,
			sorters : [{
						property : 'BParameter_ParaType',
						direction : 'ASC'
					}],
			PageSize : 8000,
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
					text : '主键ID',
					dataIndex : 'BParameter_Id',
					width : 100,
					sortable : false,
					hidden : true,
					align : 'left'
				}, {
					text : '实验室ID',
					dataIndex : 'BParameter_LabID',
					width : 100,
					sortable : false,
					hidden : true,
					align : 'left'
				}, {
					text : '检验小组编号',
					dataIndex : 'BParameter_GroupNo',
					width : 100,
					sortable : false,
					hidden : true,
					align : 'left'
				}, {
					text : '名称',
					dataIndex : 'BParameter_Name',
					width : 200,
					hideable : true,
                    hidden : false,
                    sortable : true,
					align : 'left',
                    editor : {
                            allowBlank : true
                        }
				}, {
                    text : '参数类型',
                    dataIndex : 'BParameter_ParaType',
                    width : 70,
                    hideable : true,
                    sortable : true,
                    hidden : true,
                    align : 'left'
                },{
                    text : '站点名称',
                    dataIndex : 'BParameter_BNodeTable_Name',
                    width : 100,
                    sortable : true,
                    hideable : true,
                    align : 'left'
                },{
                    text : '参数编号',
                    dataIndex : 'BParameter_ParaNo',
                    width : 210,
                    hideable : true,
                    sortable : true,
                    editor : {
                            allowBlank : true
                        },
                    align : 'left'
                }, {
                    text : '显示序号',
                    dataIndex : 'BParameter_DispOrder',
                    width : 70,
                    hideable : true,
                    hidden : true,
                    editor : {
                            allowBlank : true
                        },
                    align : 'left'
                },{
					text : '简称',
					dataIndex : 'BParameter_SName',
					width : 100,
					hideable : true,
                    sortable : true,
                    hidden : true,
					align : 'left'
				}, {
                    text : '快捷码',
                    dataIndex : 'BParameter_Shortcode',
                    width : 100,
                   sortable : true,
                    hideable : true,
                    editor : {
                            allowBlank : true
                        },
                    align : 'left'
                }, {
					text : '汉字拼音字头',
					dataIndex : 'BParameter_PinYinZiTou',
					width : 100,
					sortable : true,
					hideable : true,
                    editor : {
                            allowBlank : true
                        },
					align : 'left'
				}, {
					text : '是否使用',
					dataIndex : 'BParameter_IsUse',
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
					width : 70,
					sortable : false,
					hideable : true,
					align : 'left'
				}, {
					text : '加入时间',
					dataIndex : 'BParameter_DataAddTime',
					xtype : 'datecolumn',
					format : 'Y-m-d',
					width : 100,
					sortable : false,
					hideable : true,
                    hidden : true,
					align : 'left'
				},  {
					text : '站点主键ID',
					dataIndex : 'BParameter_BNodeTable_Id',
					width : 100,
					sortable : false,
					hidden : true,
					hideable : true,
					align : 'left'
				},  {
					text : 'IP地址',
					dataIndex : 'BParameter_BNodeTable_IP',
					width : 100,
					sortable : false,
                    hidden:true,
					hideable : true,
					align : 'left'
				}, {
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
						handler : function(grid, rowIndex, colIndex, item, e,record) {
                            me.fireEvent('editClick',grid,record);
						}
					}, {
						type : 'show',
						tooltip : '查看',
						iconCls : 'build-button-see hand',
						handler : function(grid, rowIndex, colIndex, item, e,record) {
							me.fireEvent('showClick',grid,record);
						}
					}, {
						tooltip : '删除',
						iconCls : 'build-button-delete hand',
						handler : function(grid, rowIndex, colIndex, item, e,record) {
							Ext.Msg.confirm('提示', '确定要删除吗？', function(button) {
										if (button == 'yes') {
											
                                            var nodeId=record.get('BParameter_BNodeTable_Id');
                                            //取系统的默认站点的Id值
                                            var allNodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
                                            if(allNodeId==nodeId){
                                                alertInfo('系统默认站点信息不能删除！');
                                            }else{
                                                var id = record.get('BParameter_Id');
	                                            var callback = function() {
	                                                me.deleteIndex = rowIndex;
	                                                me.load(true);
	                                            };
											    me.deleteInfo(id, callback,record);
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
                                tooltip:'重新加载数据',
								iconCls : 'build-button-refresh',
								handler : function(but, e) {
									var com = but.ownerCt.ownerCt;
									com.load(true);
								}
							}, {
								type : 'add',
								itemId : 'add',
								text : '新增',
                                tooltip:'点击可以新增或者另存为选中的行信息',
								iconCls : 'build-button-add',
								handler : function(but, e) {
									var record=null;
                                    me.fireEvent('addClick',me,record);
								}
							}, {
								type : 'edit',
								itemId : 'edit',
								text : '修改',
                                tooltip:'点击修改选中的行站点参数信息',
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
                                tooltip:'点击查看选中的行站点参数信息',
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
                                tooltip:'点击删除选中的行站点参数',
								iconCls : 'build-button-delete',
								handler : function(but, e) {
									var records = me.getSelectionModel().getSelection();
									if (records.length > 0) {
										Ext.Msg.confirm('提示', '确定要删除吗？',
												function(button) {
													var createFunction = function(
															id) {
														var f = function() {
															var rowIndex = me.store.find('BParameter_Id',id);
															me.deleteIndex = rowIndex;
															me.load(true);
															me.fireEvent('delClick');
														};
														return f;
													};
													if (button == 'yes') {
														for (var i in records) {
															var id = records[i].get('BParameter_Id');
                                                            var nodeId=records[i].get('BParameter_BNodeTable_Id');
				                                            //取系统的默认站点的Id值
				                                            var allNodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
				                                            if(allNodeId==nodeId){
				                                                alertInfo('系统默认站点信息不能删除！');
				                                            }else{
                                                                var callback = createFunction(id);
				                                                me.deleteInfo(id, callback,records[i]);
				                                            }
														}
													}
												});
									} else {
										alertInfo('请选择数据进行操作！');
									}
								}
							},{
                                type : 'copy',
                                itemId : 'copy',
                                tooltip:'点击打开复制站点参数信息',
                                text : '复制',
                                iconCls : 'list-button-copy',
                                handler : function(but, e) {
                                    var records = me.getSelectionModel().getSelection();
                                    var record=null;
                                    if (records.length == 1) {
                                        record = records[0];
                                    } else {
                                        record=null;
                                        //alertInfo('请选择一条数据进行操作！');
                                    }
                                    me.fireEvent('copyClick',me,record);
                                    Ext.Loader.setConfig({enabled: true});//允许动态加载
							        Ext.Loader.setPath('Ext.manage.bparameter.bparameterCopyApp', getRootPath() +'/ui/manage/class/bparameter/bparameterCopyApp.js');
							        var win=openWin('Ext.manage.bparameter.bparameterCopyApp');
                                    win.on({
							            copySaveAfterClick:function(panel,panelWin){
							                me.fireEvent('copySaveAfterClick',panel,panelWin);
							            },
                                        closeAfterClick:function(panel){
                                            me.fireEvent('closeAfterClick',panel);
                                        },
                                        close:function(panel){
                                            me.fireEvent('closeAppClick',panel);
                                        }
							        });
                                }
                            },  '->', {
								xtype : 'textfield',
								itemId : 'searchText',
								width : 145,
								emptyText : '名称/简称/参数类型/快捷码/汉字拼音字头',
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
								tooltip : '按照名称/简称/参数类型/快捷码/汉字拼音字头进行查询',
								handler : function(button) {
									me.search();
								}
							}]
				}];
		me.saveToTable = function(records, callback,record) {
			var url = 'SingleTableService.svc/ST_UDTO_UpdateBParameterByField';
			if (url != '') {
				url = getRootPath() + '/' + url;
			} else {
				alertError('没有配置获取数据服务地址!');
				return null;
			}
            //针对单个数据对象
            var changed=record.getChanges( );
            var Id=record.get("BParameter_Id");
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
					//alertInfo('保存成功!');
                    record.commit();
					if (Ext.typeOf(callback) === 'function') {
						callback();
					}
				} else {
					alertError(result.ErrorInfo);
				}
			};
			postToServer(url, params, c, false);
		};
		me.plugins = Ext.create('Ext.grid.plugin.RowEditing', {
					clicksToMoveEditor : 2,
					autoCancel : false,
					listeners : {
						canceledit : function() {
						},
						edit : function(editor, e) {
							var records = e.record.data;
							me.saveToTable(records,null,e.record);
						}
					}
				});;
		me.deleteInfo = function(id, callback,record) {
			var url = getRootPath()+ '/SingleTableService.svc/ST_UDTO_DelBParameter?id=' + id;
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
        me.addEvents('copyClick');
        me.addEvents('copySaveAfterClick');
        me.addEvents('closeAfterClick');
        me.addEvents('closeAppClick');
		this.callParent(arguments);
	}
});