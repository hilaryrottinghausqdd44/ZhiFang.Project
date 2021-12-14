/**
 * 职责选择列表
 * @author liangyl	
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.manage.basic.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'职责选择列表',
    width:450,
    height:350,
    
    /**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEResponsibilityByHQL?isPlanish=true',
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
			width:200,isLike:true,itemId: 'search',
			emptyText:'职责名称',
			fields:['eresponsibility.CName']
		};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			dataIndex: 'EResponsibility_CName',text: '职责名称',flex: 1,minWidth:100,defaultRenderer: true
		}, {
			dataIndex: 'EResponsibility_UseCode',text: '代码',width: 100,defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_Id',text: '主键ID',hidden: true,hideable: false,isKey: true,defaultRenderer: true
		}];
		
		return columns;
	}
	
});