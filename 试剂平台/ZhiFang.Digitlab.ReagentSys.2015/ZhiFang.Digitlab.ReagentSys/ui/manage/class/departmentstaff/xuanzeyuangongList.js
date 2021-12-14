//选择员工列表
Ext.Loader.setConfig({enabled:true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath() + '/ui/zhifangux');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.departmentstaff.xuanzeyuangongList', {
	extend:'Ext.zhifangux.CheckList',
    alias:['widget.xuanzeyuangongList' ],
    layout:'fit',
    requires: ['Ext.zhifangux.CheckList'],
    title:'',
    externalWhere:'',
    internalWhere:'',
    border:false,
    header:false,
    autoSelect:true,
    autoScroll:true,
    multiSelect:true,
    isbutton:false,
    defaultLoad:false,
    itemId:'yuangongListCheck',
    primaryKey:'HREmployee_Id',
    serverUrl:getRootPath() + '/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL',
    sortableColumns:true,
    valueField:[ {
        text:'时间戳',
        dataIndex:'HREmployee_DataTimeStamp',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }, {
        text:'时间戳',
        dataIndex:'HREmployee_HRDept_DataTimeStamp',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }, {
        text:'名称',
        dataIndex:'HREmployee_CName',
        width:84,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'主键ID',
        dataIndex:'HREmployee_HRDept_Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }, {
        text:'直属部门',
        dataIndex:'HREmployee_HRDept_CName',
        width:91,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'职位',
        dataIndex:'HREmployee_HRPosition_CName',
        width:64,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'在职',
        dataIndex:'HREmployee_IsEnabled',
        width:37,
        xtype:'booleancolumn',
        trueText:'是',
        falseText:'否',
        defaultRenderer:function(value) {
            if (value === undefined) {
                return this.undefinedText;
            }
            if (!value || value === 'false' || value === '0' || value === 0) {
                return this.falseText;
            }
            return this.trueText;
        },
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'主键ID',
        dataIndex:'HREmployee_Id',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }, {
        text:'英文名称',
        dataIndex:'HREmployee_EName',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'代码',
        dataIndex:'HREmployee_UseCode',
        width:78,
        sortable:true,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'描述',
        dataIndex:'HREmployee_Comment',
        width:100,
        sortable:false,
        hidden:false,
        hideable:true,
        align:'left'
    }, {
        text:'性别',
        dataIndex:'HREmployee_BSex_Name',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }, {
        text:'身份证号',
        dataIndex:'HREmployee_IdNumber',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    } , {
        text:'拼音字头',
        dataIndex:'HREmployee_PinYinZiTou',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    } , {
        text:'快捷码',
        dataIndex:'HREmployee_Shortcode',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }, {
        text:'手机号码',
        dataIndex:'HREmployee_MobileTel',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    } , {
        text:'工作职责',
        dataIndex:'HREmployee_JobDuty',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    } , {
        text:'Email',
        dataIndex:'HREmployee_Email',
        width:100,
        sortable:false,
        hidden:true,
        hideable:true,
        align:'left'
    }],
    internalDockedItem:[ {
        xtype:'toolbar',
        itemId:'buttonstoolbar',
        dock:'top',
        items:[ {
            itemId:'refresh',
            text:'更新',
            iconCls:'build-button-refresh'
        }, {
            itemId:'show',
            text:'查看',
            iconCls:'build-button-see'
        },  {
            xtype:'textfield',
            itemId:'searchText',
            width:160,
            emptyText:'员工姓名/直属部门/职位'
        }, {
            xtype:'button',
            text:'查询',
            itemId:'searchbtn',
            iconCls:'search-img-16 ',
            tooltip:'员工姓名/直属部门/职位'
        } ]
    }],
    /**
       * 初始化组件
    */
    initComponent:function() {
        var me = this;
        this.callParent(arguments);
    },
    afterRender:function() {
        var me = this;
        me.callParent(arguments);
        me.store.on({
            load:function(store, records, successful) {
                var autoSelect = me.autoSelect;
                if (successful && records.length > 0) {
                    if (me.deleteIndex && me.deleteIndex != "" && me.deleteIndex != -1) {
                        if (records.length - 1 > me.deleteIndex) {
                            me.getSelectionModel().select(me.deleteIndex);
                        } else {
                            me.getSelectionModel().select(records.length - 1);
                        }
                        me.deleteIndex = -1;
                    } else {
                        if (autoSelect) {
                            if (autoSelect === true) {
                                me.getSelectionModel().select(0);
                            } else {
                                var num = 0;
                                if (autoSelect >= 0) {
                                    num = autoSelect % records.length;
                                } else {
                                    num = length - Math.abs(num) % length;
                                }
                                me.getSelectionModel().select(num);
                            }
                        }
                    }
                }
            }
        });
        if (Ext.typeOf(me.callback) == 'function') {
            me.callback(me);
        }
    }

});