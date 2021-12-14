/**
 * 打印样本清单检验日期+样本号排序（前台排序)
 * @author liangyl
 * @version 2019-12-06
 */
Ext.define('Shell.class.lts.print.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	width: 800,
	height: 500,
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:10000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: false,
		//默认排序字段
	defaultOrderBy:[
		{property:'LisTestForm_GTestDate',direction:'ASC'},
		{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			sortchange : function(ct,column,  direction,  eOpts ){
				if(column.dataIndex =='LisTestForm_GSampleNoForOrder' || column.dataIndex =='LisTestForm_GTestDate'){
		           me.store.addSorted(me.getStoreList(direction));
				}
			}
		});
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'检验单ID',dataIndex:'LisTestForm_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			isDate:true,sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本号',dataIndex:'LisTestForm_GSampleNoForOrder',width:80,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNo'),
					tipText = v;
				meta.tdAttr = 'data-qtip="' + tipText + '"';
				return v;
			}
		},{
			text:'样本号排序',dataIndex:'LisTestForm_GSampleNo',width:150,hidden:true,renderer:function(value,meta,record){
				var v = record.get('LisTestForm_GSampleNoForOrder');
				meta.tdAttr = 'data-qtip="' + v + '"';
				return v;
			}
		},{
			text:'姓名',dataIndex:'LisTestForm_CName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'条码号',dataIndex:'LisTestForm_BarCode',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'病历号',dataIndex:'LisTestForm_PatNo',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本类型',dataIndex:'LisTestForm_GSampleType',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'科室',dataIndex:'LisTestForm_LisPatient_DeptName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本单项目',dataIndex:'LisTestForm_ItemNameList',minWidth:180,flex:1,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	//检验日期+样本号排序（升序）
	mysort:function(a,b){
	    if (a["LisTestForm_GTestDate"] === b["LisTestForm_GTestDate"]) {
	        if (a["LisTestForm_GSampleNoForOrder"] > b["LisTestForm_GSampleNoForOrder"]) {
	            return 1;
	        } else if (a["LisTestForm_GSampleNoForOrder"] < b["LisTestForm_GSampleNoForOrder"]) {
	            return - 1;
	        } else {
	            return 0;
	        }
	    } else {
	        if (a["LisTestForm_GTestDate"] > b["LisTestForm_GTestDate"]) {
	            return 1;
	        } else {
	            return - 1;
	        }
	    }
	},
	//检验日期+样本号排序(倒序)
	mydescsort:function(a,b){
	    if (a["LisTestForm_GTestDate"] === b["LisTestForm_GTestDate"]) {
	        if (a["LisTestForm_GSampleNoForOrder"] < b["LisTestForm_GSampleNoForOrder"]) {
	            return 1;
	        } else if (a["LisTestForm_GSampleNoForOrder"] > b["LisTestForm_GSampleNoForOrder"]) {
	            return - 1;
	        } else {
	            return 0;
	        }
	    } else {
	        if (a["LisTestForm_GTestDate"] < b["LisTestForm_GTestDate"]) {
	            return 1;
	        } else {
	            return - 1;
	        }
	    }
	},
	getStoreList : function(direction){
		var me = this,
		    recs = me.store.data.items,
		    arr = [];
		
		for(var i=0;i<recs.length;i++){
			arr.push(recs[i].data);
		}
		if(!direction)direction='ASC';
		var sortList = arr.sort(me.mysort);
		if(direction=='DESC')sortList = arr.sort(me.mydescsort);
		me.store.removeAll();
		return sortList;
	},
	//store 重新按样本号+时间排序
	storeSort : function(direction){
		var me = this;
		me.store.addSorted(me.getStoreList(direction));
	}
   
});