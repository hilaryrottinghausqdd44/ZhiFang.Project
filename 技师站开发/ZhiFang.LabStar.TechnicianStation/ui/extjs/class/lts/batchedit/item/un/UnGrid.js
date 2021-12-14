/**
 * 未选项目
 * @author liangyl
 * @version 2019-12-06
 */
Ext.define('Shell.class.lts.batchedit.item.un.UnGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '未选全部项目',
	requires: ['Ext.ux.CheckColumn'],
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true',
	
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize:50,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
   //排序字段
	defaultOrderBy: [{property:'LBSectionItem_LBSection_DispOrder',direction:'ASC'},{property:'LBSectionItem_LBItem_DispOrder',direction:'ASC'}],
  	/**小组*/
	SectionID: null,
	/**项目类型,0-全部项目,1-医嘱项目，2-组合项目*/
	type:'0' ,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:'100%',emptyText:'项目名称/项目简称/用户代码/拼音字头',isLike:true,itemId:'search',
			fields:['lbsectionitem.LBItem.CName','lbsectionitem.LBItem.SName','lbsectionitem.LBItem.UseCode','lbsectionitem.LBItem.PinYinZiTou']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'项目id',dataIndex:'LBSectionItem_LBItem_Id',width:180,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LBSectionItem_LBItem_CName',width:150,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				if (v) {
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					var GroupType = record.get('LBSectionItem_LBItem_GroupType');
					if(GroupType=='1')meta.style = 'background-color:#F08080;';
				}
				return v;
			}
		},{
			text:'项目简称',dataIndex:'LBSectionItem_LBItem_SName',width:80,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				var IsOrderItem = record.get('LBSectionItem_LBItem_IsOrderItem');
				if(IsOrderItem=='true')meta.style = 'background-color:#3CB371;';
				
				return v;
			}
		},{
			text:'医嘱项目',dataIndex:'LBSectionItem_LBItem_IsOrderItem',width:60,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = value;
				if(v=='true')v='医嘱';
				   else  v='';
				if (v) {
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = 'background-color:#3CB371;';
				}
				return v;
			}
		},{
			text:'用户代码',dataIndex:'LBSectionItem_LBItem_UseCode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'拼音字头',dataIndex:'LBSectionItem_LBItem_PinYinZiTou',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'是否组合项目',dataIndex:'LBSectionItem_LBItem_GroupType',width:70,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
		
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
		search = me.getComponent('buttonsToolbar').getComponent('search').getValue();

		//小组Id
		if(me.SectionID) {
			params.push("lbsectionitem.LBSection.Id=" + me.SectionID + "");
		}
		
		if(me.type=='1'){//按医嘱项目查询
			params.push("lbsectionitem.LBItem.IsOrderItem=1");
		}
		if(me.type=='2'){//按组合项目查询
			params.push("lbsectionitem.LBItem.GroupType=1");
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
	}
});