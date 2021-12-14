/**
 * 包装单位选择列表
 * @author liangyl	
 * @version 2017-10-25
 */
Ext.define('Shell.class.rea.client.goods2.unit.CheckGrid',{
    extend:'Shell.class.rea.client.basic.CheckPanel',
    title:'包装单位选择列表',
    width:300,
    height:350,
    
    /**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnitByHQL?isPlanish=true',
	/**是否单选*/
	checkOne:false,
	
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}

		//查询框信息
		me.searchInfo = {
			width:160,isLike:true,itemId: 'Search',
			emptyText:'主单位/次单位',
			fields:['reagoodsunit.GoodsUnit','reagoodsunit.ChangeUnit']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			dataIndex: 'ReaGoodsUnit_GoodsUnit',text: '主单位',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsUnit_ChangeUnit',text: '次单位',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsUnit_ChangeUnitID',text: '次单位id',width: 100,hidden:true,defaultRenderer: true
		},{
			dataIndex: 'ReaGoodsUnit_Id',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		
		return columns;
	}
	
});