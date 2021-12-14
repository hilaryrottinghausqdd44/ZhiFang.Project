Ext.define('Shell.DoctorReportPrint.class.PrintApp', {
    extend: 'Shell.ReportPrint.class.PrintApp',
    /**查询*/
    onSearch: function (where, isPrivate) {
        var me = this,
			PrintList = me.getComponent('PrintList');

        //where条件是否符合规范
        var isWhereValid = true;//me.isWhereValid();
        if (isWhereValid) {
            PrintList.internalWhere = PrintList.getInternalWhere();
            PrintList.load(where, isPrivate);
        } else {
            Shell.util.Msg.showError('条件不完整（除日期外，需要起码一个其他条件才能查询）！');
        }
    },
});