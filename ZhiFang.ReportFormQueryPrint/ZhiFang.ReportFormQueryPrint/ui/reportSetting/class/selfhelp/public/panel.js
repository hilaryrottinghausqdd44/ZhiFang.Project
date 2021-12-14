Ext.define("Shell.reportSetting.class.selfhelp.public.panel", {
    extend: 'Shell.reportSetting.class.base.public.panel',
    createItems: function () {
        var me = this;
        var items = [];
       
        items.push(
        {
            xtype: 'combobox',
            style: me.itemStyle,
            name: 'printPageType',
            itemId: 'printPageType',
            displayField: 'text', valueField: 'value',
            store: Ext.create('Ext.data.Store', {
                fields: ['text', 'value'],
                data: [
                    { text: 'A4', value: 'A4' },
                    { text: 'A5', value: 'A5' },
                    { text: '双A5', value: '双A5' }
                ]
            }),
            fieldLabel: '默认打印类型'
        });

        items.push({
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'printtimes',
            itemId: 'printtimes',
            fieldLabel: '限制打印次数'
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'selectColumn',
            itemId: 'selectColumn',
            fieldLabel: '设置查询字段'
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'lastDay',
            itemId: 'lastDay',
            fieldLabel: '查询多少天之前的记录'
        }, {
            xtype: 'textfield',
            style: me.itemStyle,
            name: 'tackTime',
            itemId: 'tackTime',
            fieldLabel: '提示信息关闭倒计时'
        });

        return items;
    }
});