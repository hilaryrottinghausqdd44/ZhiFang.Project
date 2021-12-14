/**
 * 待选项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.comitem.IsGroupItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '待选项目',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSBLabTestItemByLabCode?isPlanish=true',
		/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
    /**实验室id*/
    ClienteleID:null,
    ItemNo:null,
    formtype:'edit',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		   //根据BLabTestItem编号
	    if(me.ClienteleID) {
	    	me.onSearch();
	    }
	   
	},
	initComponent: function() {
		var me = this;
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
		me.searchInfo = {width:145,emptyText:'编号/名称/简称',isLike:true,
			fields:['blabtestitem.CName','blabtestitem.ShortCode','blabtestitem.ItemNo']};
		buttonToolbarItems.push({
	        xtype: 'label',
	        text: '组套外项目',
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
			text:'项目编号',dataIndex:'ItemNo',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'名称',dataIndex:'CName',width:120,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'MarketPrice',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'内部价格',dataIndex:'GreatMasterPrice',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行价格',dataIndex:'Price',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'Id',isKey:true,hidden:true,hideable:false
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
	    url +='&LabCode='+me.ClienteleID;
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
			if(i == 'blabtstitem.ItemNo'){
				if(!isNaN(value)){
					where.push("blabtstitem.ItemNo=" + value);
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
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me =this;
		var result = {},
			list = [],
			arr = [];
		var count=0;
		if(data.value){
			count=data.value.count;
			var len =data.value.list.length;
		    for(var i =0 ;i<len;i++){
		    	if(me.formtype=='add'){
		    		list.push(data.value.list[i]);
		    	}else{
		    		if(me.ItemNo!=data.value.list[i].ItemNo){
			    		list.push(data.value.list[i]);
			    	}
		    	}
		    	
		    }
		}else{
			list=[];
		}
	    result.count=count;
	    result.list =  list;
		return result;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;

		me.enableControl(); //启用所有的操作功能
		if (me.errorInfo) {
			var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
			me.getView().update(msg);
			me.errorInfo = null;
		} else {
			if (!records || records.length <= 0) {
				var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
				me.getView().update(msg);
			}
		}

		if (!records || records.length <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
		me.fireEvent('load', me);
		//默认选中处理
		me.doAutoSelect(records, me.autoSelect);
	}
});