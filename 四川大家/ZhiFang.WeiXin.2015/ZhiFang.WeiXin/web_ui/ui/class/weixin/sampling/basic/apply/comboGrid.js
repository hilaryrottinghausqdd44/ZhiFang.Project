/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define('Shell.class.weixin.sampling.basic.apply.comboGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '申请列表',
    //获取数据服务路径
    selectUrl: '',
    //默认加载数据
    defaultLoad: false,
    //带功能按钮栏
    hasButtontoolbar: false,
    //排序字段
    defaultOrderBy: [],
    //带分页栏
    hasPagingtoolbar: true,
    //是否启用序号列
    hasRownumberer: false,
    //是否默认选中数据
    autoSelect: true,
    /**序号列宽度*/
    //rowNumbererWidth: 35,
    //改变状态名称颜色
    initComponent: function () {
        var me = this;

        me.columns = [
            {
                text: '套餐名称',
                dataIndex: 'Name',sort:false,
                width: 120,align: 'center',
				renderer:function (v, meta, record) {
					var value =  record.get('Name');
					meta.style="background-color:#0066FF;color:#FFFFFF;"; 						
					return value;					
				}
            }, {
                text: '套餐编号',
                dataIndex: 'ItemNo',sort:false,
                width: 80,align: 'center',
				renderer:function (v, meta, record) {
					var value =  record.get('ItemNo');
					meta.style="background-color:#0066FF;color:#FFFFFF;"; 						
					return value;					
				}
            }, {
                text: '套餐ID',
                dataIndex: 'ItemID',sort:false,
                width: 80,align: 'center',hidden:true
            }];
        me.callParent(arguments);
    }
});