/**
 * 待选择的检验项目
 * @author zhangda
 * @version 2020-04-17
 * @desc Jcall 2020-09-14修改代码部分内容
 */
Ext.define('Shell.class.lts.sample.result.sample.add.ItemGrid',{
	extend:'Shell.ux.grid.Panel',
	requires:['Shell.ux.toolbar.Button'],
	title:'待选择项目列表',
	width:285,
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true',
	//默认加载数据
	defaultLoad:false,
	//序号列宽度
	rowNumbererWidth:35,
	//排序字段
	defaultOrderBy:[
		{property:'LBSectionItem_DispOrder',direction:'ASC'},
		{property:'LBSectionItem_LBItem_DispOrder',direction:'ASC'}
	],
	
	//小组ID
	sectionId:null,
	//已有项目数据
	existingData:null,
	
    initComponent: function () {
        var me = this;
        //默认条件
        me.defaultWhere = "lbsectionitem.LBItem.GroupType in (0,1) and lbsectionitem.LBItem.IsUse=true and lbsectionitem.LBSection.Id=" + me.sectionId;
        //内部条件
		me.internalWhere = 'lbsectionitem.LBItem.IsOrderItem=true';
	
        //查询框信息
		me.searchInfo = {
			labelWidth:75,width:290,fieldLabel:'已选择项目',itemId:'SearchValue',
			emptyText:'名称、简称、快捷代码、拼音字头',isLike:true,
			fields:[
				'lbsectionitem.LBItem.CName','lbsectionitem.LBItem.SName',
				'lbsectionitem.LBItem.Shortcode','lbsectionitem.LBItem.PinYinZiTou'
			]
		};
        
        //创建列
        me.columns = me.createGridColumns();
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			type:'search',info:me.searchInfo
		},{
			xtype:'tbspacer',width:10
		},{
			xtype:'checkbox',boxLabel:'仅查看医嘱项目',itemId:'IsOrderItem',checked:true,
			listeners:{
				change:function(t,newVal,oldVal){
					var value = this.ownerCt.getComponent('SearchValue').getValue();
					me.onSearchClick(this.ownerCt,value);
				}
			}
		}];
        
        me.callParent(arguments);
    },
	//创建数据列
	createGridColumns:function(){
		var me = this;
		
		var items = [{
			text:'主键ID',dataIndex:'LBSectionItem_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'项目ID',dataIndex:'LBSectionItem_LBItem_Id',hidden:true,hideable:false
		},{
			text:'项目名称',dataIndex:'LBSectionItem_LBItem_CName',width:100,
			renderer:me.isPItemRenderer
		},{
			text:'项目简称',dataIndex:'LBSectionItem_LBItem_SName',width:80,
			renderer:me.isPItemRenderer
		},{
			text:'快捷码',dataIndex:'LBSectionItem_LBItem_Shortcode',width:80,
			renderer:me.isPItemRenderer
		},{
			text:'拼音字头',dataIndex:'LBSectionItem_LBItem_PinYinZiTou',width:80,hidden:true,
			renderer:me.isPItemRenderer
		},{
			text:'医嘱项目',dataIndex:'LBSectionItem_LBItem_IsOrderItem',width:80,
			renderer:function(v,metaData,record){
				if(String(v) == "true"){
					metaData.style = 'background-color:#7CE9BE;color:black';
					return '是';
				}else{
					metaData.style = 'color:red';
					return '否';
				}
			}
		},{
			text:'组合类型',dataIndex:'LBSectionItem_LBItem_GroupType',hidden:true,hideable:false
		},{
			text:'是否在用',dataIndex:'LBSectionItem_DataAddTime',width:60,
			renderer:function(v,metaData,record){
				if(v == "在用"){
					metaData.style = 'background-color:#ADE3F7';
				}else if(v == "新增"){
					metaData.style = 'background-color:#BDFFDE';
				}
				return v;
			}
		}];
		
		return items;
	},
	//组合项目的显示处理
	isPItemRenderer:function(v,metaData,record){
		var IsComItem = record.get("LBSectionItem_LBItem_GroupType");
		if(IsComItem == 1){
			metaData.style = 'background-color:#FFC3A5';
		}
		return v;
	},
	/**@overwrite 改变返回的数据*/
	changeResult:function(data){
		var me = this;
		if(data.value.list && data.value.list.length > 0){
			Ext.Array.each(data.value.list,function(str1,index1,array1){
				var str = "";
				Ext.Array.each(me.existingData,function(str2,index2,array3){
					if(str1.LBSectionItem_LBItem_Id == str2.LisTestItem_LBItem_Id || 
					str1.LBSectionItem_LBItem_Id == str2.LisTestItem_PLBItem_Id){//单项 或者 组合
						if(str2.LisTestItem_Id) {
							str = "在用";
						}else{
							str = "新增";
						}
					}
				});
				str1.LBSectionItem_DataAddTime = str;
			});
		}
		return data;
	},
	
	/**@overwrite 查询按钮点击处理方法*/
	onSearchClick:function(but,value){
		var me = this,
			where = [];
		
		//仅查看医嘱项目
		var IsOrderItem = me.getComponent("buttonsToolbar").getComponent("IsOrderItem").getValue();
		if(IsOrderItem){
			where.push('lbsectionitem.LBItem.IsOrderItem=true');
		}
		//查询框条件
		var searchWhere = value ? me.getSearchWhere(value) : '';
		if(searchWhere){
			where.push('(' + searchWhere + ')');
		}
		
		me.internalWhere = where.join(' and ');
		me.onSearch();
	}
});