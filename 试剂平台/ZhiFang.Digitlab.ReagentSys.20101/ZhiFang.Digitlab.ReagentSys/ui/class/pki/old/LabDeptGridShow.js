/**
 * 送检单位科室列表-只读
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.LabDeptGridShow',{
    extend:'Shell.ux.grid.Panel',
    title:'送检单位科室列表',
    
    width:600,
    height:400,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBLabDeptByHQL?isPlanish=true',
    /**默认加载*/
	defaultLoad:true,
	/**后台排序*/
	remoteSort:false,
	/**带分页栏*/
	hasPagingtoolbar:true,
	/**默认每页数量*/
	defaultPageSize:50,
	/**是否启用序号列*/
	hasRownumberer:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick:function(view,record){
				me.onEditClick();
			}
		});
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {width:'100%',emptyText:'科室名称',isLike:true,fields:['blabdept.BDept.CName']};
		//自定义按钮功能栏
		me.buttonToolbarItems = [{type:'search',info:me.searchInfo}];
		//数据列
		me.columns = [{
			dataIndex:'BLabDept_BLaboratory_CName',text:'送检单位名称',width:200,defaultRenderer:true
		},{
			dataIndex:'BLabDept_BDept_CName',text:'科室名称',width:100,defaultRenderer:true
		}];
		
		me.callParent(arguments);
	}
});