Ext.define("Shell.class.setting.PrintSetting.class.section", {
    extend: 'Shell.ux.grid.Panel',
    title: '小组选择',
    width: 350,
    height: 350,
    checkOne: true,
    /**获取数据服务路径*/
   /* selectUrl: '/ServiceWCF/DictionaryService.svc/GetPGroupPaging?fields=SectionNo,CName',*/
    selectUrl: '/ServiceWCF/DictionaryService.svc/GetPGroup?fields=SectionNo,CName',
    /**默认加载数据*/
    defaultLoad: true,
    /**带分页栏*/
    hasPagingtoolbar: false,
    /**默认选中第一行*/
    autoSelect: true,
    /**默认每页数量*/
    defaultPageSize: 50000,
    sRecd:'',
    initComponent: function () {
        var me = this;
        if (!me.checkOne) {
            //复选框
            me.multiSelect = false;
            me.selType = 'checkboxmodel';
            me.selModel= new Ext.selection.CheckboxModel({checkOnly:true});
        }
       // me.selModel = { checkOnly: me.CheckOnly };
        //数据列
        
        me.columns = me.createGridColumns();
        me.createtolb();
        
        me.callParent(arguments);
    },
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        //单选双击触发确认事件
        if (me.checkOne) {
            me.on({
                itemdblclick: function (view, record) {
                    me.fireEvent('accept', me, record);
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
                dataIndex: 'SectionNo', text: '主键ID', hidden: true, hideable: false, isKey: true, defaultRenderer: true
            }];
        return columns;
    },
    createtolb: function () {
        var me = this;
        var items = [];

        items.push({
            xtype: 'button',
            text: '清除', iconCls: 'button-cancel', tooltip: '<b>清除原先的选择</b>',
            handler: function () {
                me.fireEvent('accept', me, null);
            },
            listeners: {
                click: function () {
                    me.getSelectionModel().deselect(me.sRecd);
                    me.sRecd = '';
                    me.store.load();
                }
            }
        }, 'searchtext', { xtype: 'tbfill', height: 0 }, {
            xtype: 'button',
            text: "确定",
            iconCls:'button-accept',
            listeners: {
                click: function () {
                    me.onAcceptClick();
                }
            }
        });

        me.toolbars = me.toolbars || [{
            dock: 'top', itemId: 'toptoolbar', buttons: [],
            listeners: {
                search: function (m, v) {
                    me.selectUrl += "&Where=(CName like '%" + v.value + "%')";
                    me.onSearch();
                    me.selectUrl = "/ServiceWCF/DictionaryService.svc/GetPGroup?fields=SectionNo,CName";
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
    },
    onAfterLoad: function (records, successful) {
        var me = this;
        me.enableControl();//启用所有的操作功能
        if (!successful || records.length == 0) {
            me.store.removeAll();
            return;
        }
        var aa = [];
        if (me.sRecd != "") {
            for (var i = 0; i < me.sRecd.length; i++) {
                me.store.each(function (r) {
                    if (r.index == me.sRecd[i].index) {
                        aa.push(r);
                    }
                });
            }
            me.getSelectionModel().select(aa);
            
        }
      
    },
});