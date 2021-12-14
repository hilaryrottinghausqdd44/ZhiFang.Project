/**
 * 指定质控物选择列表(仅针对本小组的仪器的质控物)
 * @author liangyl
 * @version 2019-12-16
 */
Ext.define('Shell.class.lts.changeqc.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'指定质控物选择列表',
    width:270,
    height:300,
    
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/LabStarQCService.svc/SearchQCMaterialbySectionEquip',
	
    /**是否单选*/
	checkOne:true,
    /**小组ID*/
	SectionID:null,
	initComponent:function(){
		var me = this;
		me.selectUrl+='?SectionId='+me.SectionID;

//		//查询框信息
//		me.searchInfo = {width:155,emptyText:'质控物名称/英文名称',isLike:true,
//			fields:['lbqcmaterial.CName','lbqcmaterial.EName']};
//			
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'仪器',dataIndex:'LBQCMaterial_LBEquip_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'模块',dataIndex:'LBQCMaterial_EquipModule',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'质控物名称',dataIndex:'LBQCMaterial_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'浓度水平',dataIndex:'LBQCMaterial_ConcLevel',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'LBQCMaterial_Id',isKey:true,hidden:true,hideable:false
		}];
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		
		if(me.checkOne){
			if(!me.searchInfo.width) me.searchInfo.width = 145;
			//自定义按钮功能栏
			me.buttonToolbarItems = me.buttonToolbarItems || [];
//			me.buttonToolbarItems.push({type:'search',info:me.searchInfo});
			
			if(me.hasClearButton){
				me.buttonToolbarItems.unshift({
					text:'清除',iconCls:'button-cancel',tooltip:'<b>清除原先的选择</b>',
					handler:function(){me.fireEvent('accept',me,null);}
				});
			}
			if(me.hasAcceptButton){
				me.buttonToolbarItems.push('->','accept');
			}
		}else{
			if(!me.searchInfo.width) me.searchInfo.width = 205;
			//自定义按钮功能栏
			me.buttonToolbarItems = me.buttonToolbarItems || [];
			me.buttonToolbarItems.push({type:'search',info:me.searchInfo});
			if(me.hasAcceptButton) me.buttonToolbarItems.push('->','accept');
		}
	}
});