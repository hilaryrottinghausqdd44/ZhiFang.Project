/**
 * 仪器项目结果列表
 * @author zhangda
 * @version 2020-04-13
 */
Ext.define('Shell.class.lts.sample.result.equip.extract.RightGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '仪器项目结果列表',
    width: 285,
    //获取数据服务路径
    selectUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?isPlanish=true',
    //默认加载数据
    defaultLoad: false,
    //带功能按钮栏
    hasButtontoolbar: false,
    //排序字段
    defaultOrderBy: [{ property: 'LisEquipItem_IExamine', direction: 'DESC' }],
    //带分页栏
    hasPagingtoolbar: true,
    //是否启用序号列
    hasRownumberer: true,
    //是否默认选中数据
    autoSelect: true,
    /**序号列宽度*/
    rowNumbererWidth: 35,
    //改变状态名称颜色
    initComponent: function () {
        var me = this;

        me.columns = [
            {
                text: '主键Id',
                dataIndex: 'LisEquipItem_Id',
                isKey: true, hidden: true, hideable: false
            }, {
                text: '项目名称',
                dataIndex: 'LisEquipItem_LBItem_CName',
                width: 130
            }, {
                text: '报告结果',
                dataIndex: 'LisEquipItem_EReportValue',
                width: 100
            }, {
                text: '原始结果',
                dataIndex: 'LisEquipItem_EOriginalValue',
                width: 100
            }, {
                text: '原始结果状态',
                dataIndex: 'LisEquipItem_EOriginalResultStatus',
                width: 100
            }, {
                text: '结果状态',
                dataIndex: 'LisEquipItem_EResultStatus',
                width: 100
            }, {
                text: '结果报警',
                dataIndex: 'LisEquipItem_EResultAlarm',
                width: 100
            }];
        me.callParent(arguments);
    },
    clearData: function () {
        var me = this;
        me.externalWhere = "EquipFormID=-1";
        me.onSearch();
    }
});