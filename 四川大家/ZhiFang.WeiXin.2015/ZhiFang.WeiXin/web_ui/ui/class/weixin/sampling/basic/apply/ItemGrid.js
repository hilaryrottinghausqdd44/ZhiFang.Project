/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define('Shell.class.weixin.sampling.basic.apply.ItemGrid', {
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
                text: '编码',
                dataIndex: 'ItemNo',
                width: 135,align: 'center',
				renderer:function (v, meta, record) {
					var ColorValue =  record.get('ColorValue');
					var ColorName =  record.get('ColorName');
					var ItemNo =  record.get('ItemNo');
					if(ColorName.indexOf("白色") != -1){
						meta.style="background-color:"+ColorValue+";";
						return ItemNo;
					}else{
						meta.style="background-color:"+ColorValue+";color:#FFFFFF;";
						return ItemNo;
					}		
				}
            }, {
                text: '名称',
                dataIndex: 'CName',
                width: 220,align: 'center',
				renderer:function (v, meta, record) {
					var ColorValue =  record.get('ColorValue');
					var ColorName =  record.get('ColorName')+"";
					var CName =  record.get('CName');
					if(ColorName.indexOf("白色")!= -1){
						meta.style="background-color:"+ColorValue+";";
						return CName;
					}else{
						meta.style="background-color:"+ColorValue+";color:#FFFFFF;";
						return CName;
					}		
				}
            }, {
                text: '英文名',
                dataIndex: 'EName',
                width: 140,align: 'center',
				renderer:function (v, meta, record) {
					var ColorValue =  record.get('ColorValue');
					var ColorName =  record.get('ColorName')+"";
					var EName =  record.get('EName');
					if(ColorName.indexOf("白色")!= -1){
						meta.style="background-color:"+ColorValue+";";
						return EName;
					}else{
						meta.style="background-color:"+ColorValue+";color:#FFFFFF;";
						return EName;
					}		
				}
            }, {
                text: '价格',
                dataIndex: 'Prices',
                width: 140,align: 'center',
				renderer:function (v, meta, record) {
					var ColorValue =  record.get('ColorValue');
					var ColorName =  record.get('ColorName')+"";
					var Prices =  record.get('Prices');
					if(ColorName.indexOf("白色")!= -1){
						meta.style="background-color:"+ColorValue+";";
						return Prices;
					}else{
						meta.style="background-color:"+ColorValue+";color:#FFFFFF;";
						return Prices;
					}		
				}
            },{
                text: '颜色名称',
                dataIndex: 'ColorName',
                width: 135,align: 'center',hidden:true
            },{
                text: '颜色值',
                dataIndex: 'ColorValue',
                width: 135,align: 'center',hidden:true
            },{
                text: '父级No',
                dataIndex: 'SItemNo',
                width: 135,align: 'center',hidden:true
            }];
        me.callParent(arguments);
    }
});