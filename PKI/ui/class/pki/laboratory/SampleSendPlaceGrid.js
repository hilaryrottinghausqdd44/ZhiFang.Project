/**
 * 委托单位选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.laboratory.SampleSendPlaceGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'委托单位选择列表',
    width:270,
    height:500,

    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchSampleSendPlaceByHQL?isPlanish=true',

	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:150,emptyText:'委托单位名称',isLike:true,fields:['samplesendplace.CName']};
		//数据列
		me.columns = [{
			dataIndex:'SampleSendPlace_CName',text:'委托单位名称',width:200,defaultRenderer:true
		},{
			dataIndex:'SampleSendPlace_Id',text:'ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'SampleSendPlace_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		}];

		me.callParent(arguments);
	}
});