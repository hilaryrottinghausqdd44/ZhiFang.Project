/**
 * 组套内项目明细
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.comitem.ItemGrid', {
	extend: 'Shell.ux.grid.Panel',	
	title: '组套内项目明细 ',
	width: 800,
	height: 500,
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode?isPlanish=true',
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
    /**主项目编号*/
	PItemNo:null,
	/**实验室id*/
    ClienteleID:null,
    autoSelect: false,
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
			text:'项目编号',dataIndex:'BLabGroupItemVO_BLabTestItemVO_ItemNo',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'BLabGroupItemVO_BLabTestItemVO_CName',width:120,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'BLabGroupItemVO_BLabTestItemVO_MarketPrice',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'内部价格',dataIndex:'BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行价格',dataIndex:'BLabGroupItemVO_BLabTestItemVO_Price',width:85,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
	  
        //根据BLabTestItem编号
	    if(!me.ItemNo)return;
	    if(!me.ClienteleID)return;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
	    
	    url +='&pitemNo='+me.ItemNo+'&LabCode='+me.ClienteleID;
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
//	/**@overwrite 
//	 * 改变返回的数据
//	 * 细项不包含主项
//	 * */
//	changeResult: function(data) {
//		var me =this;
//		var result = {},
//			list = [],
//			arr = [];
//		if(data.value){
//			var len =data.value.list.length;
//		    for(var i =0 ;i<len;i++){
//		        //细项不包含主项
//		    	if(me.PItemNo!=data.value.list[i].BLabGroupItemVO_BLabTestItemVO_ItemNo){
//		    		list.push(data.value.list[i]);
//		    	}
//		    }
//		}else{
//			list=[];
//		}
//	    result.list =  list;
//		return result;
//	},
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