/**
 * 小组选择列表
 * @author Jcall
 * @version 2019-11-18
 */
Ext.define('Shell.class.lts.section.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'小组选择列表',
    width:340,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',
	//排序字段
	defaultOrderBy: [{property:'LBSection_DispOrder',direction:'ASC'}],
    //是否单选
	checkOne:false,
	//已选择过的小组
	checkedIds:null,
	SectionID:null,
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
		if(me.checkedIds){
			me.defaultWhere += 'lbsection.Id not in(' + me.checkedIds + ') and ';
		}
		me.defaultWhere += 'lbsection.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'小组名称/编码',isLike:true,
			fields:['lbsection.CName','lbsection.UseCode']};
			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'小组名称',dataIndex:'LBSection_CName',width:100,defaultRenderer:true
		},{
			text:'小组编码',dataIndex:'LBSection_UseCode',width:100,defaultRenderer:true
		},{
			text:'排序',dataIndex:'LBSection_DispOrder',width:60,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LBSection_Id',isKey:true,hidden:true,hideable:false
		}]
		
		return columns;
	},
	changeResult: function (data) {
		var me = this,
			list = data.list;
		for(var i in list){
			
		}
		return data;
	},
	onAfterLoad: function (records) {
		var me = this;
		//选中某值
		if (me.SectionID) {
			var record = me.store.findRecord('LBSection_Id', me.SectionID);
			me.getSelectionModel().select(record);
		}
	}
});