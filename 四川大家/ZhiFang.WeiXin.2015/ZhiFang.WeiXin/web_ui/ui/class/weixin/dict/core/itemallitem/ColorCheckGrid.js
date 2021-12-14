/**
 * 采样管颜色颜色选择列表
 * @author liangyl	
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.ColorCheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'采样管颜色颜色选择列表',
    width:450,
    height:350,
    
    /**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDictByHQL?isPlanish=true',
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
			width:200,isLike:true,itemId: 'Search',
			emptyText:'采样管颜色名称',
			fields:['itemcolordict.ColorName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			dataIndex: 'ItemColorDict_ColorName',text: '颜色名称',flex: 1,minWidth:100,defaultRenderer: true
		}, {
			dataIndex: 'ItemColorDict_ColorValue',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		return columns;
	}
	
});