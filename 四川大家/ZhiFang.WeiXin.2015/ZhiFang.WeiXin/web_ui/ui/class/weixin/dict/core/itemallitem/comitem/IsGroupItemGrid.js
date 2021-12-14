/**
 * 待选项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.comitem.IsGroupItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '待选项目',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchItemAllItemByHQL?isPlanish=true',
		/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
    ItemNo:null,
    formtype:'edit',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//根据BLabTestItem编号
	    me.onSearch();
	},
	initComponent: function() {
		var me = this;
		if(me.formtype!='add'){
			me.defaultWhere='testitem.Id!='+me.ItemNo;
		}
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = [];
		        //查询框信息
		me.searchInfo = {width:125,emptyText:'编号/名称/简称',isLike:true,
			fields:['testitem.Id','testitem.CName','testitem.ShortName']};
		buttonToolbarItems.push({
	        xtype: 'label',
	        text: '所有项目',
	        style: "font-weight:bold;color:blue;",
	        margin: '0 0 10 10'
		},'-',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'项目编号',dataIndex:'ItemAllItem_Id',isKey:true,width:120,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'名称',dataIndex:'ItemAllItem_CName',flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'ItemAllItem_MarketPrice',width:85,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'内部价格',dataIndex:'ItemAllItem_GreatMasterPrice',width:85,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行价格',dataIndex:'ItemAllItem_Price',width:85,
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
//	    url +='&LabCode='+me.ClienteleID;
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
	},
		/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if(i == 'testitem.Id'){
				if(!isNaN(value)){
					where.push("testitem.Id=" + value);
				}
				continue;
			}
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	}

//	/**@overwrite 改变返回的数据*/
//	changeResult: function(data) {
//		var me =this;
//		var result = {},
//			list = [],
//			arr = [];
//		var count=0;
//		if(data.value){
//			count=data.value.count;
//			var len =data.value.list.length;
//		    for(var i =0 ;i<len;i++){
//		    	if(me.ItemNo!=data.value.list[i].ItemAllItem_Id){
//		    		list.push(data.value.list[i]);
//		    	}
////		    	else{
////		    		count=count-1;
////		    	}
//		    }
//		}else{
//			list=[];
//		}
//		result.count=count;
//	    result.list =  list;
//		return result;
//	}
});