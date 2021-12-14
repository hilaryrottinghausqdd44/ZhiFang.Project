/**
 * 货架选择列表
 * @author liangyl
 * @version 2017-12-06
 */
Ext.define('Shell.class.rea.client.shelves.place.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'货架选择列表',
    width:270,
    height:300,
    
	/**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?isPlanish=true',
	
    /**是否单选*/
	checkOne:true,
    /**默认加载数据*/
//	defaultLoad:false,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:135,emptyText:'货位名称',isLike:true,itemId:'Search',
			fields:['reaplace.CName']
		};		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			text:'货位名称',dataIndex:'ReaPlace_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'代码',dataIndex:'ReaPlace_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'ReaPlace_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
		
	},
	/**根据库房id加载*/
	loadDataId:function(id){
		var me=this;
		me.defaultWhere='reaplace.ReaStorage.Id='+id+' and reaplace.Visible=1';
		me.onSearch();
	},
	
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('Search').getValue();
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