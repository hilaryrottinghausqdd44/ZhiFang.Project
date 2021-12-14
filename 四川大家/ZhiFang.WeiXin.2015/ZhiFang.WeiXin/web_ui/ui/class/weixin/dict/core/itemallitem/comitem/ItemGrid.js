/**
 * 组套内项目明细
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.comitem.ItemGrid', {
	extend: 'Shell.ux.grid.Panel',	
	title: '组套内项目明细 ',
	width: 800,
	height: 500,
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchGroupItemSubItemByPItemNo?isPlanish=true',
		/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: true,
		/**带分页栏*/
//	hasPagingtoolbar: false,
	/**复选框*/
	multiSelect: true,
		/**默认每页数量*/
	defaultPageSize: 500,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	 /**项目编号*/
	ItemNo:null,
	autoSelect: false,
	/**实验室id*/
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'项目编号',isKey:true,dataIndex:'GroupItemVO_TestItem_Id',width:120,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'GroupItemVO_TestItem_CName',flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'GroupItemVO_TestItem_MarketPrice',width:85,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'内部价格',dataIndex:'GroupItemVO_BTestItem_GreatMasterPrice',width:85,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行价格',dataIndex:'GroupItemVO_TestItem_Price',width:85,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
	    //根据TestItem编号
	    if(!me.ItemNo)return;
	    url +='&pitemNo='+me.ItemNo;
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(true);
	}
});