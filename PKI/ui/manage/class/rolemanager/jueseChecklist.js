
/***
 * 角色管理--模块角色选择列表
 * 右模块角色选择列表
 * 选中左角色列表、中间模块树；该角色模块已有操作的还原
 */
Ext.Loader.setConfig({enabled:true});
Ext.ns('Ext.manage');
Ext.Loader.setPath('Ext.zhifangux.CheckList', getRootPath() + '/ui/zhifangux/CheckList.js');
Ext.define('Ext.manage.rolemanager.jueseChecklist', {
    extend:'Ext.zhifangux.CheckList',
    alias:['widget.jueseChecklist'],

    autoLoad:false,
    /***
     * 左角色选择列表选中的角色Id,外部传入
     * @type 
     */
    roleId:'',
    /***
     * 中间模块树的选中Id,外部传入
     * @type 
     */
    moduleId:'',
    title:'模块操作',
    externalWhere:'',
    internalWhere:'',
    border:true,
    autoSelect:true,
    autoScroll:true,
    multiSelect:true,
    primaryKey:'RBACModuleOper_Id',
    isload:false,//  加载数据开关
    isbutton:false,
    serverUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL',
    sortableColumns:true,
    /**
     * 初始化表单构建组件
     */
    initComponent:function(){
        var me = this;
        me.valueField=[ {
            text:'ID',
            dataIndex:'RBACModuleOper_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
            text:'名称',
            dataIndex:'RBACModuleOper_CName',
            width:180,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'描述',
            dataIndex:'RBACModuleOper_Comment',
            width:200,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        }, {
            text:'过滤条件',
            dataIndex:'RBACModuleOper_FilterCondition',
            width:160,
            sortable:true,
            hidden:false,
            hideable:true,
            align:'left'
        },  {
            text:'显示次序',
            dataIndex:'RBACModuleOper_DispOrder',
            width:100,
            hidden:true,
            hideable:true,
            align:'left'
        }, {
	        text:'模块操作时间戳',
	        dataIndex:'RBACModuleOper_DataTimeStamp',
	        width:100,
	        sortable:false,
	        hidden:true,
	        hideable:true,
	        align:'left'
	    },{
            text:'模块Id',
            dataIndex:'RBACModuleOper_RBACModule_Id',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        },{
            text:'模块时间戳',
            dataIndex:'RBACModuleOper_RBACModule_DataTimeStamp',
            width:100,
            sortable:false,
            hidden:true,
            hideable:true,
            align:'left'
        }];
	    me.internalDockedItem =[ {
	        xtype:'toolbar',
	        itemId:'buttonstoolbar',
	        dock:'top',
	        items:[ {
	            itemId:'refresh',
                hidden:true,
	            text:'更新',
	            iconCls:'build-button-refresh'
	        }, '->', {
	            xtype:'textfield',
	            itemId:'searchText',
	            width:160,
	            emptyText:'名称/描述',
                listeners: {
	                specialkey: function(field,e){    
	                    if (e.getKey()==Ext.EventObject.ENTER){  
	                        var newValue=field.getValue();
	                         me.filterFn(newValue);
	                    }  
	                },
                    change: function(com, newValue, oldValue, eOpts ){    
                        me.filterFn(newValue); 
                    }
                }
	        } ]
	    } ];
       me.callParent(arguments);
       
    },
    /**
     * 模糊查询过滤函数
     * @param {} value
     */
     filterFn: function (value) {
         var me = this, valtemp = value;
         //获取右上模块操作选择列表
         //var getjueseChecklist = me;
         var store = me.getStore();
         if (!valtemp) {
             store.clearFilter();
             return;
         }
         valtemp = String(value).trim().split(' ');
         store.filterBy(function (record, id) {
             var data = record.data;
             for (var p in data) {
                 var porp = String(data[p]);
                 for (var i = 0; i < valtemp.length; i++) {
                     var macther = valtemp[i];
                     var macther2 = '^' + Ext.escapeRe(macther);
                     mathcer = new RegExp(macther2);
                     if (mathcer.test(porp)) {
                         return true;
                     } 
                 } 
             }
             return false;
         });
     },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    }
});