/**
 * 项目选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.item.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'项目选择列表',
    width:365,
    height:500,

    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBTestItemByHQL?isPlanish=true',

	initComponent:function(){
		var me = this;
		//查询框信息me.searchInfo||
		me.searchInfo ={width:250,emptyText:'项目名称',isLike:true,fields:['btestitem.CName']};
//		me.searchInfo = me.searchInfo||{width:170,emptyText:'项目名称',isLike:true,fields:['btestitem.CName']};
		//数据列
		me.columns = [{
			dataIndex:'BTestItem_CName',text:'项目名称',width:200,defaultRenderer:true
		},{
			dataIndex: 'BTestItem_IsCombiItem',
			text: '是否组套',
			width: 70,
			isBool: true,
			align: 'center',
			type: 'bool',defaultRenderer: true
		},{
			dataIndex:'BTestItem_Id',text:'项目ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BTestItem_DataTimeStamp',text:'项目时间戳',hidden:true,hideable:false
		}];

		me.callParent(arguments);
	}
});