/**
 * 小组项目选择列表
 * @author liangyl
 * @version 2020-01-10
 */
Ext.define('Shell.class.lts.batchedit.sectionitem.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'小组项目选择列表',
    width:340,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true',
	//排序字段
	defaultOrderBy: [{property:'LBSectionItem_DispOrder',direction:'ASC'},{property:'LBSectionItem_LBItem_DispOrder',direction:'ASC'}],
    //是否单选
	checkOne:false,
    //检验小组ID
	SectionID: 1,
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
    	me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		if (me.SectionID) me.defaultWhere += "LBSection.Id=" + me.SectionID;
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'项目名称/项目代码',isLike:true,
			fields:['lbitem.CName','lbitem.UseCode']};
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text: '项目id', dataIndex:'LBSectionItem_LBItem_Id',width:180,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LBSectionItem_LBItem_CName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'LBSectionItem_LBItem_SName',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'精度',dataIndex:'LBSectionItem_LBItem_Prec',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'组合项目',dataIndex:'LBSectionItem_LBItem_GroupType',width:100,hidden:false,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	}
});