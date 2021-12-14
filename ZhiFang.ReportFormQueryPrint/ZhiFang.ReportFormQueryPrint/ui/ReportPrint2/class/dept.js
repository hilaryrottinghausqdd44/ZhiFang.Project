Ext.define("Shell.ReportPrint2.class.dept", {
    extend: 'Shell.ux.grid.Panel',
    title: '科室选择',
    width: 350,
    height: 350,

    /**获取数据服务路径*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetDeptListPaging?fields=DeptNo,CName',
    /**默认加载数据*/
    defaultLoad: true,
    /**带分页栏*/
    hasPagingtoolbar: false,
    /**默认选中第一行*/
    autoSelect: false,
    checkOne:true,
    /**默认每页数量*/
    defaultPageSize: 50000,
    initComponent: function () {
        var me = this;
        if (!me.checkOne) {
            //复选框
            me.multiSelect = true;
            me.selType = 'checkboxmodel';
        }
        
        me.selModel = { checkOnly: me.CheckOnly };
        //数据列
        me.columns = me.createGridColumns();
        //me.toolbars = me.createtolb();
        me.createtolb();
        me.callParent(arguments);
    },

    afterRender:function () {
        var me =this;
        me.callParent(arguments);
        //单选双击触发确认事件
        if(me.checkOne){
            me.on({
                itemdblclick:function(view,record){
                    me.fireEvent('accept',me,record);
                }
            });
        }
    },

    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{ xtype: 'rownumberer', text: '序号', width: 40, align: 'center' },
            {
                dataIndex: 'CName', text: '名称', flex: 1, minWidth: 100, defaultRenderer: true
        }, {
            dataIndex: 'DeptNo', text: '主键ID', hidden: true, hideable: false, isKey: true, defaultRenderer: true
        }];
        return columns;
    },
    createtolb: function () {
        var me = this;
        var items = [];

        items.push({
                xtype: 'button',
                text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
                handler:function(){me.fireEvent('accept',me,null);}
           }, 'searchtext', { xtype: 'tbfill', height: 0 }, {
            xtype: 'button',
            iconCls:'button-accept',
                text: "确定",
                listeners: {
                    click: function () {
                        me.onAcceptClick();
                    }
                }
            });

        me.toolbars = me.toolbars || [{
            dock: 'top', itemId: 'toptoolbar', buttons: [],
            listeners: {
                search: function (m,v) {
                    me.selectUrl += "&Where=(CName like '%" + v.value + "%')";
                    me.onSearch();
                    me.selectUrl = "/ServiceWCF/DictionaryService.svc/GetDeptListPaging?fields=DeptNo,CName";
                }
            }
        }];
        me.toolbars[0].buttons = me.toolbars[0].buttons.concat(items);
        return items;
    },
    /**确定按钮处理*/
    onAcceptClick: function () {
        var me = this;
        var records = me.getSelectionModel().getSelection();

        if (me.checkOne) {
            if (records.length != 1) {
                me.close();
                return;
            }
            me.fireEvent('accept', me, records[0]);
        } else {
            if (records.length == 0) {
                me.close();
                return;
            }
            me.fireEvent('accept', me, records);
        }
    }
});