/**
 * 组合项目下的子项
 * @author zhangda
 * @version 2020-04-17
 * @desc Jcall 2020-09-14修改代码部分内容
 */
Ext.define('Shell.class.lts.sample.result.sample.add.SubitemGrid',{
	extend:'Shell.ux.grid.Panel',
	title:'子项列表',
	width:200,
	//获取数据服务路径
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemGroupByHQL?isPlanish=true',
	//默认加载数据
	defaultLoad:false,
	//带功能按钮栏
	hasButtontoolbar:false,
	//排序字段
	defaultOrderBy:[
		{property:'LBItemGroup_DispOrder',direction:'ASC'},
		{property:'LBItemGroup_LBItem_DispOrder',direction:'ASC'}
	],
	
	//是否默认选中数据
	autoSelect:false,
	//序号列宽度
	rowNumbererWidth:35,
    
    initComponent: function () {
        var me = this;
        me.columns = me.createGridColumns();
        me.callParent(arguments);
    },
    //创建数据列
	createGridColumns:function(){
		var me = this;
		
		var items = [{
			text:'主键ID',dataIndex:'LBItemGroup_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'项目ID',dataIndex:'LBItemGroup_LBItem_Id',hidden:true,hideable:false
		},{
			text:'项目名称',dataIndex:'LBItemGroup_LBItem_CName',width:100
		},{
			text:'项目简称',dataIndex:'LBItemGroup_LBItem_SName',width:80
		},{
			text:'快捷码',dataIndex:'LBItemGroup_LBItem_Shortcode',width:80
		},{
			text:'拼音字头',dataIndex:'LBItemGroup_LBItem_PinYinZiTou',width:80,hidden:true
		},{
			text:'医嘱项目',dataIndex:'LBItemGroup_LBItem_IsOrderItem',width:80,
			renderer:function(v,metaData,record){
				if(String(v) == "true"){
					metaData.style = 'background-color:#7CE9BE;color:black';
					return '是';
				}else{
					metaData.style = 'color:red';
					return '否';
				}
			}
		}];	
		
		return items;
	},
	//查询
	onSearch:function(GroupItemID){
		var me = this;
		me.defaultWhere = "LBItem.IsUse=true and GroupItemID=" + GroupItemID;
		me.load(null,true,me.autoSelect);
	}
});