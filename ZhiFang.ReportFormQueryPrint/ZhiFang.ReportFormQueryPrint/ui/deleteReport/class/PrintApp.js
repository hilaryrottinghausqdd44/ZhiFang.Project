Ext.define("Shell.deleteReport.class.PrintApp", {
    extend: 'Ext.panel.Panel',
    layout: 'border',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.callParent(arguments);
    },

    afterRender:function () {
        var me = this;
        me.callParent(arguments);
        me.panel.on({
            btnDelete: function (m, start, end, data) {
                var body = "";
                var html = "";
                for (var i = 0; i < data.length; i++) {
                    html = '<label>文件夹' + data[i].Folder + '</label>&nbsp; &nbsp; ';
                    for (var j = 0; j < data[i].file.length; j++) {
                        body += html + data[i].file[j] + '&nbsp; &nbsp; &nbsp; 已删除 <br>';
                    }
                    
                }
                me.list.update(body)
            }
        });
    },

    createItems: function () {
        var me = this;
        me.panel = Ext.create("Shell.deleteReport.class.delete", {
            region: 'north',
            height:'30%'
        });
        me.list = Ext.create("Shell.deleteReport.class.list", {
            region:'center'
        });
        return [me.panel, me.list];
    }
});