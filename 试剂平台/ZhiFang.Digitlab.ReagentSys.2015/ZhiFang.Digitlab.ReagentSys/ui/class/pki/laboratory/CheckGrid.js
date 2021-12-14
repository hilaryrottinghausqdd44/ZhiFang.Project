/**
 * 送检单位选择列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.laboratory.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'送检单位选择列表',
    width:270,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true',
    
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:150,emptyText:'送检单位名称',isLike:true,fields:['blaboratory.CName']};
		//数据列
		me.columns = [{
			dataIndex:'BLaboratory_CName',text:'送检单位名称',width:200,defaultRenderer:true
		},{
			dataIndex:'BLaboratory_Id',text:'ID',hidden:true,hideable:false,isKey:true
		},{
			dataIndex:'BLaboratory_DataTimeStamp',text:'时间戳',hidden:true,hideable:false
		}];
		
		me.callParent(arguments);
	}
});