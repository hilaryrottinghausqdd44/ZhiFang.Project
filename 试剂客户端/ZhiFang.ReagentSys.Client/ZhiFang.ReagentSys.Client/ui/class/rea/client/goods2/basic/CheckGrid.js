/**
 * 货品选择列表
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.goods2.basic.CheckGrid',{
    extend:'Shell.class.rea.client.basic.CheckPanel',
    title:'货品选择列表',
    width:850,
    height:400,
    
    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:false,
	defaultWhere:'reagoods.Visible=1',
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:350,isLike:true,itemId: 'Search',
			emptyText:'货品编码/名称/一级分类/二级分类/部门/拼音字头/仪器',
			fields:['reagoods.ReaGoodsNo','reagoods.CName','reagoods.GoodsClass','reagoods.GoodsClassType','reagoods.DeptName','reagoods.PinYinZiTou','reagoods.SuitableType']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			dataIndex: 'ReaGoods_CName',text: '货品名称',width:180,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_ReaGoodsNo',text: '货品编码',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',text: '单位',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',text: '规格',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_GoodsClass',text: '一级分类',hidden:false,width: 100,
			defaultRenderer: true
		}, {
			text: '二级分类',dataIndex: 'ReaGoods_GoodsClassType',
			width: 100,hidden:false,defaultRenderer: true
		},{
			text: '部门',dataIndex: 'ReaGoods_DeptName',
			width: 100,hidden:false,defaultRenderer: true
		},  {
			text: '仪器',dataIndex: 'ReaGoods_SuitableType',
			width: 100,hidden:false,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		
		return columns;
	}
	
});