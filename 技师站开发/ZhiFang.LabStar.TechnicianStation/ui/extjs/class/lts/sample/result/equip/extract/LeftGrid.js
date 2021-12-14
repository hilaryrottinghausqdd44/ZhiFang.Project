/**
 * 仪器样本列表
 * @author zhangda
 * @version 2020-04-13
 */
Ext.define('Shell.class.lts.sample.result.equip.extract.LeftGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '仪器样本列表',
    width: 285,
    //获取数据服务路径
    selectUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipFormByHQL?isPlanish=true',
    //默认加载数据
    defaultLoad: false,
    //带功能按钮栏
    hasButtontoolbar: false,
    //排序字段
    defaultOrderBy: [{ property: 'LisEquipForm_ETestDate', direction: 'DESC' }],
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
                text: '仪器样本单Id',
                dataIndex: 'LisEquipForm_Id',
                isKey: true, hidden: true, hideable: false
            }, {
                text: '仪器Id',
                dataIndex: 'LisEquipForm_EquipID',
                hidden: true
            }, {
                text: '仪器检验日期',
                dataIndex: 'LisEquipForm_ETestDate',
                width: 130
            }, {
                text: '仪器样本编号',
                dataIndex: 'LisEquipForm_ESampleNo', 
                width: 110
            }, {
                text: '条码号',
                dataIndex: 'LisEquipForm_EBarCode', 
                width: 110
            }];
        me.callParent(arguments);
    },
    clearData: function () {
        var me = this;
        me.externalWhere = "EquipID=-1";
        me.onSearch();
    }
});