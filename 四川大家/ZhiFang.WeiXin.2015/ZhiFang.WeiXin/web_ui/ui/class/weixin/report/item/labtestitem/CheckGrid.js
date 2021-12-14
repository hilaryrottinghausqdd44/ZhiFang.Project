/**
 * 套餐项目选择列表
 * @author liangyl
 * @version 2017-03-09
 */
Ext.define('Shell.class.weixin.report.item.labtestitem.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'套餐项目选择列表',
    width:270,
    height:300,
    
    /**获取数据服务路径*/
    selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemByHQL?isPlanish=true',
    /**是否单选*/
	checkOne:true,
    /**区域ID*/
    AreaID:null,
    defaultWhere:'',
	initComponent:function(){
		var me = this;
		me.defaultWhere += 'blabtestitem.Visible=1 and isCombiItem=1';
		//查询框信息
		me.searchInfo = {width:'70%',emptyText:'名称/代码',isLike:true,
			itemId: 'search',fields:['blabtestitem.CName','blabtestitem.ShortCode']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'项目编号',dataIndex:'BLabTestItem_ItemNo',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'名称',dataIndex:'BLabTestItem_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'代码',dataIndex:'BLabTestItem_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BLabTestItem_ShortName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'英文名称',dataIndex:'BLabTestItem_EName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'BLabTestItem_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	},
		  /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,
			params = [];
			
		me.internalWhere = '';
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		//根据区域Id
		if(me.AreaID) {
			params.push("blabtestitem.LabCode=" + me.AreaID);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});