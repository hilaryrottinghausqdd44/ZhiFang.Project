/*******************************************************************************
 * 站点参数设置列表
 */
Ext.ns('Ext.manage');
Ext.define('Ext.manage.bparameter.bparameterList', {
	extend : 'Ext.zhifangux.GridPanel',
	alias : 'widget.bparameterList',
	title : '',
	width : 255,
	objectName : 'BParameter',
	openFormId : '',
	defaultLoad : true,
	defaultWhere : '',
   /**默认选中的数据行,可以是true(选中)、false(不选)、数字(下标)[正数从上往下、负数从下往上,例如-1就是选中length-1行数据]、字符串(主键ID)*/
    autoSelect:true,
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
                        me.store = me.changStore(store, records);
                    }
                    
                }
            }
        });
        if(Ext.typeOf(me.callback)==='function'){me.callback(me);}
    },
    /***
     * 过滤参数编码重复的数据
     * @param {} s
     * @param {} records
     * @return {}
     */
    changStore:function(s, records) {
        var a = {}, b = {};
        var len = records.length;
        for (var i = 0; i < len; i++) {
            if (typeof a[records[i].get('BParameter_ParaNo')] == 'undefined') {
                a[records[i].get('BParameter_ParaNo')] = 1;
                b[records[i]] = 1;
            } else {
                s.remove(records[i]);
            }
        }
        return s;
    },
    
	initComponent : function() {
		var me = this;
		Ext.Loader.setPath('Ext.ux', getRootPath() + '/ui/extjs/ux');
		me.url = getRootPath()+ '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true&fields=BParameter_BNodeTable_Id,BParameter_Id,BParameter_Name,BParameter_SName,BParameter_PinYinZiTou,BParameter_ParaType,BParameter_ParaDesc,BParameter_Shortcode,BParameter_IsUse,BParameter_ParaNo';
		me.searchArray = ['bparameter.Name', 'bparameter.SName',
				'bparameter.ParaType', 'bparameter.Shortcode',
				'bparameter.PinYinZiTou'];
		me.store = me.createStore({
			fields : ['BParameter_Id', 'BParameter_LabID', 'BParameter_Name',
					'BParameter_SName', 'BParameter_PinYinZiTou',
					'BParameter_ParaType', 'BParameter_BNodeTable_Id',
					'BParameter_ParaDesc', 'BParameter_Shortcode',
					'BParameter_IsUse',
					'BParameter_ParaNo'],
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
                    hideable : false,
					align : 'left'
				},  {
                    text : '名称',
                    dataIndex : 'BParameter_Name',
                    width : 200,
                    hideable : true,
                    sortable : true,
                    align : 'left',
                    editor : {
                            allowBlank : true
                        }
                }, {
                    text : '参数编号',
                    dataIndex : 'BParameter_ParaNo',
                    width : 210,
                    hideable : true,
                    sortable : true,
                    editor : {
                            allowBlank : true
                        },
                    align : 'left'
                },{
                    text : '参数类型',
                    dataIndex : 'BParameter_ParaType',
                    width : 70,
                    hideable : true,
                    hidden : true,
                    sortable : true,
                    align : 'left'
                }, {
					text : '简称',
					dataIndex : 'BParameter_SName',
					width : 100,
					hideable : true,
                    hidden : true,
					align : 'left'
				}, {
                    text : '快捷码',
                    dataIndex : 'BParameter_Shortcode',
                    width : 100,
                    sortable : true,
                    hideable : true,
                    hidden : true,
                    align : 'left'
                }, {
					text : '汉字拼音字头',
					dataIndex : 'BParameter_PinYinZiTou',
					width : 100,
					sortable : false,
					hidden : true,
					align : 'left'
				},{
                    text : '站点Id',
                    dataIndex : 'BParameter_BNodeTable_Id',
                    width : 100,
                    sortable : false,
                    hideable : false,
                    hidden : true,
                    hideable : true,
                    align : 'left'
                }, {
                    text : '实验室ID',
                    dataIndex : 'BParameter_LabID',
                    width : 100,
                    hideable : false,
                    sortable : true,
                    hidden : true,
                    align : 'left'
                }, {
					text : '参数说明',
					dataIndex : 'BParameter_ParaDesc',
					width : 200,
					sortable : false,
					hideable : true,
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
								width : 140,
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
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
					clicksToMoveEditor : 2,
					autoCancel : false,
					listeners : {
						canceledit : function() {
						},
						edit : function(editor, e) {
							//var records = e.record.data;
							//me.saveToTable(records,null,e.record);
						}
					}
				});
		me.deleteInfo = function(id, callback) {
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
		this.callParent(arguments);
	}
});