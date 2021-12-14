Ext.define("Shell.deleteReport.class.delete", {
    extend: 'Ext.panel.Panel',
    deleteFileUrl: "/ServiceWCF/ReportFormService.svc/DeleteReportPDFFile",
    layout: {
        type: 'hbox',
        pack: 'center',
        align:'middle'
    },
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'datefield',
                fieldLabel: '删除时间间隔',
                itemId:'startDate',
                labelWidth: 80,
                width: 200
            }, {
                xtype: 'datefield',
                itemId: 'endDate',
                fieldLabel: '-',
                labelWidth: 1,
                width:120
            },
            {
                style: 'margin-left: 10px;',
                xtype:"button",
                text: '删除',
                handler: function () {
                    var start = me.getComponent("startDate").value;
                    var end = me.getComponent("endDate").value;
                    start = Shell.util.Date.toString(start);
                    end = Shell.util.Date.toString(end);
                    if (start == null ||end ==null) {
                        Shell.util.Msg.showError("请输入时间");
                        return;
                    }
                    var report = me.getDelete(start, end);
                    me.fireEvent("btnDelete", me, start, end, report);
                }
            }
        ];
        me.callParent(arguments);
    },
    getDelete: function (startDate, endDate) {
        var uri = Shell.util.Path.rootPath + this.deleteFileUrl;
        var report = [];
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: uri,
            async: false,
            method: 'POST',
            params: Ext.encode({
                startDate: startDate,
                endDate: endDate
            }),
            success: function (response, options) {
                var rs = Ext.JSON.decode(response.responseText);
                if (rs.success) {
                    report =  Ext.JSON.decode(rs.ResultDataValue);
                }
            }
        });
        return report;
    }
});