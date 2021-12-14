/*******************************************************************************
 * 站点参数复制设置列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bparameter.bparameterCopyList', {
	extend : 'Ext.zhifangux.GridPanel',
    required:["Ext.zhifangux.GridPanel"],
	alias : 'widget.bparameterCopyList',
	title : '',
	objectName : 'BParameter',
	openFormId : '',
	defaultLoad : false,
	defaultWhere : '',
	sortableColumns : true,
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.enableControl();
        me.initLink();
        if (Ext.typeOf(me.callback) == "function") {
            me.callback(me);
        }
    },
    initLink:function() {
        var me = this;
        me.getStore().on({
            load:function(s, records, successful, eOpts) {
                if (!successful) {
                    Ext.Msg.alert("提示", "获取数据错误！");
                }else{
                    if(s.getCount() > 0){
                        s.each(function(r){
                            r.set('checkSelect',true);
                            r.commit(); 
                        });
                        if(me.autoSelect){
                            me.getSelectionModel().select(0);
                        }
                    }
                }
            }
        });
    },

    getBNodeTableId:function() {
        var me = this;
        var buttonstoolbar=me.getComponent("buttonstoolbar");
        var com=buttonstoolbar.getComponent("BParameter_BNodeTable_Id");
        return com;
    },
	initComponent : function() {
		var me = this;
        Ext.Loader.setConfig({enabled: true});//允许动态加载
		Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
        Ext.Loader.setPath('Ext.zhifangux.GridPanel', getRootPath() + '/ui/zhifangux/GridPanel.js');
        
		me.url = getRootPath()+ '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true&fields=BParameter_ParaValue,BParameter_BNodeTable_DataTimeStamp,BParameter_Id,BParameter_DataTimeStamp,BParameter_LabID,BParameter_GroupNo,BParameter_Name,BParameter_SName,BParameter_PinYinZiTou,BParameter_ParaType,BParameter_Shortcode,BParameter_IsUse,BParameter_BNodeTable_Id,BParameter_BNodeTable_Name,BParameter_BNodeTable_IP,BParameter_ParaNo,BParameter_DispOrder';
		me.searchArray = ['bparameter.Name', 'bparameter.SName',
				'bparameter.ParaType', 'bparameter.Shortcode',
				'bparameter.PinYinZiTou'];
		me.store = me.createStore({
			fields : ["checkSelect",'BParameter_Id', 'BParameter_LabID',
					'BParameter_GroupNo', 'BParameter_Name',
					'BParameter_SName', 'BParameter_PinYinZiTou',
					'BParameter_ParaType', 'BParameter_Shortcode',
					'BParameter_IsUse', 'BParameter_BNodeTable_Id',
					'BParameter_BNodeTable_Name', 'BParameter_BNodeTable_IP',
					'BParameter_ParaNo','BParameter_DataTimeStamp',"BParameter_ParaValue",
					'BParameter_DispOrder','BParameter_BNodeTable_DataTimeStamp'],
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
					text : '主键ID',
					dataIndex : 'BParameter_Id',
					width : 100,
					sortable : false,
                    hideable : false,
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
                    hidden : false,
                    align : 'left'
                },{
                    text : '站点名称',
                    dataIndex : 'BParameter_BNodeTable_Name',
                    width : 100,
                    sortable : true,
                    hidden : true,
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
                    text : '参数值',
                    dataIndex : 'BParameter_ParaValue',
                    width : 100,
                    sortable : true,
                    hideable : true,
                    hidden:true,
                    align : 'left'
                },{
					text : '站点主键ID',
					dataIndex : 'BParameter_BNodeTable_Id',
					width : 100,
					sortable : false,
                    hideable : false,
					hidden : true,
					align : 'left'
				}, {
                    text : '站点时间戳',
                    dataIndex : 'BParameter_BNodeTable_DataTimeStamp',
                    width : 100,
                    sortable : false,
                    hidden : true,
                    hideable : true,
                    align : 'left'
                },{
                    text : '时间戳',
                    dataIndex : 'BParameter_DataTimeStamp',
                    width : 100,
                    sortable : false,
                    hidden:true,
                    hideable : false,
                    align : 'left'
                } ];
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
					items : [
		                  {
		                    xtype:'combobox',
		                    itemId : 'BParameter_BNodeTable_Id',
		                    name : 'BParameter_BNodeTable_Id',
		                    fieldLabel : '站点选择',
		                    labelAlign : 'left',
		                    height:22,
                            labelWidth : 60,
                            model:'local',
                            editable:true,
                            queryMode:'local',
                            displayField:"BNodeTable_Name",
				            valueField:"BNodeTable_Id",
				            store:new Ext.data.Store({
				                fields:[ "BNodeTable_Name", "BNodeTable_Id" ],
				                pageSize:8000,
				                proxy:{
				                    type:"ajax",
				                    async:false,
				                    url:getRootPath() + "/SingleTableService.svc/ST_UDTO_SearchBNodeTableByHQL?isPlanish=true&fields=BNodeTable_Name,BNodeTable_Id",
				                    reader:{
				                        type:"json",
				                        root:"list"
				                    },
				                    extractResponseData:function(response){
					                    return me.changeFormStoreData(response);
					                }
				                },
				                listeners:{
				                    load:function(s,records,successful){
				                        if(successful==true&&s!=null){
                                              var nodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
                                              var rowIndex = s.find('BNodeTable_Id',nodeId);
                                              if(rowIndex>-1){//删除所有站点的选择项
                                                s.removeAt(rowIndex);
                                              }
					                          var com=me.getBNodeTableId();
	                                          var firstValue=records[0].data["BNodeTable_Id"];//这种方法也可以获得第一项的值
	                                          com.setValue(firstValue);//选中第一项
                                              me.defaultLoad=true;
                                              var hqlWhere="bparameter.BNodeTable.Id='"+firstValue+"'";
                                              me.load(hqlWhere);
				                         }
				                    }
				                },
				                autoLoad:true
				            })
		                }, {
						xtype : 'textfield',
						itemId : 'searchText',
						width : 160,
						emptyText : '名称/简称/参数类型/快捷码/汉字拼音字头',
						listeners : {
							render : function(input) {
								new Ext.KeyMap(input.getEl(), [{
									key : Ext.EventObject.ENTER,
									fn : function() {
                                        var combo=me.getBNodeTableId();
                                        var value=combo.getValue();
	                                    if(value!=""&&value!=null){
                                            me.externalWhere="bparameter.BNodeTable.Id='"+value+"'";
	                                        me.search();
	                                    }else{
			                                alert("请选择一个站点再操作");
			                            }
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
						    var combo=me.getBNodeTableId();
	                        var value=combo.getValue();
	                        if(value!=""&&value!=null){
	                            me.externalWhere="bparameter.BNodeTable.Id='"+value+"'";
	                            me.search();
	                        }else{
                                alert("请选择一个站点再操作");
                            }
					}
				},{
                    type : 'del',
                    itemId : 'del',
                    text : '删除',
                    tooltip:'删除和默认站点参数不相同的站点参数',
                    iconCls : 'build-button-delete',
                    handler : function(but, e) {
                        var counts = me.store.getCount();
                        if (counts > 0) {
                            Ext.Msg.confirm('提示', '确定要删除与默认站点参数不同的数据吗？',
                                function(button) {
                                    var createFunction = function(id) {
                                        var f = function() {
                                            var rowIndex = me.store.find('BParameter_Id',id);
                                            me.deleteIndex = rowIndex;
                                            me.load(true);
                                            me.fireEvent('delClick');
                                        };
                                        return f;
                                    };
                                    if (button == 'yes') {
                                        me.store.each(function(record){
				                            var id = record.get('BParameter_Id');
                                            var nodeId=record.get('BParameter_BNodeTable_Id');
                                            //取系统的默认站点的Id值
                                            var allNodeId=getBNodeTableAllNodeIdInfo("AllNodeId");
                                            var checkSelect=record.get('checkSelect');
                                            if(allNodeId==nodeId){
                                                alertInfo('系统默认站点信息不能删除！');
                                            }else if(checkSelect==true){
                                                var callback = createFunction(id);
                                                me.deleteInfo(id, callback,record);
                                            }
				                        });
                                    }
                                });
                        } else {
                            alertInfo('请选择数据进行操作！');
                        }
                    }
                }, '->']
				}];
		 
		this.callParent(arguments);
	},
    deleteInfo:function(id, callback,record) {
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
    },
    /**
     * 数据适配
     * @private
     * @param {} response
     * @return {}
     */
    changeFormStoreData: function(response){
        var data = Ext.JSON.decode(response.responseText);
        var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
        data.ResultDataValue = ResultDataValue;
        data.list = ResultDataValue.list;
        response.responseText = Ext.JSON.encode(data);
        return response;
    }
});