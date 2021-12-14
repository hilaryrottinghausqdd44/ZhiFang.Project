/**
 * 已选检验单列表
 * @author liangyl
 * @version 2019-12-17
 */
Ext.define('Shell.class.lts.batchedit.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '已选检验单列表',
	requires: [
		'Ext.ux.CheckColumn'
	],
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true',
     //修改数据服务路径
	editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestForm',
    /**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	//按钮是否可点击
    BUTTON_CAN_CLICK:true, 
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.store.on({
			refresh :function(store){
				me.onSelect(true);
			}
		});
		me.on({
			sortchange : function(ct,column,  direction,  eOpts ){
				//排序
				if(column.dataIndex =='LisTestForm_GSampleNoForOrder' || column.dataIndex =='LisTestForm_GTestDate'){
		           me.store.addSorted(me.getStoreList(direction));
		           me.onSelect(true);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
	
		me.addEvents('save');
		//数据列
		me.columns = me.createGridColumns();
	
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		
		var columns = [{
			text:'检验单ID',dataIndex:'LisTestForm_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:true,menuDisabled:true,defaultRenderer:true
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
			text:'性别',dataIndex:'LisTestForm_LisPatient_GenderName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本类型',dataIndex:'LisTestForm_GSampleType',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验小组',dataIndex:'LisTestForm_LBSection_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验单来源',dataIndex:'LisTestForm_ISource',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'医嘱单ID',dataIndex:'LisTestForm_LisOrderForm_Id',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**批量删除检验单
	 * records 已选检验单列表的选中行
	 * */
	onEditClick:function(){
		var me = this;
        if(!me.BUTTON_CAN_CLICK)return;
        
        var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error('请从已选检验单列表选择要批量删除的数据行');
			return;
		}
		
		//核收的样本单不能删除，需要提示
		var list = [],msg="";
		//找到核收的样本单
		for(var i=0;i<records.length;i++){		
			var OrderFormID = records[i].data.LisTestForm_LisOrderForm_Id;
			var ISource = records[i].data.LisTestForm_ISource;
            if(OrderFormID && ISource == '0' ){ //OrderFormID为空并且检验单来源不是签收的
            	msg+='样本号为【'+records[i].data.LisTestForm_GSampleNo+'】的样本单,来源是分发/核收检验申请单 ,不能删除</BR>';
            }else{
            	list.push(records[i]);
            }
		}

        JShell.Msg.confirm({
			msg:'您确定批量删除已选检验单?</br>是(不建议批量删除检验单)</br>否(取消批量删除检验单)'
		},function(but){
			if (but != "ok") return;
			var len = list.length;
			if(len == 0) {
				JShell.Msg.alert(msg);
				return;
			}
			
			me.showMask(me.saveText);//显示遮罩层
			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = len;
			
	   	    me.onSave(list,msg);
	   });
	},
	//删除检验单,标记改为作废，并且记录操作记录
	onSave:function(list,msg){
		var me = this;
		
		var strTestFormID="";
		for(var i=0;i<list.length;i++){
			var rec = list[i];
			strTestFormID += rec.get('LisTestForm_Id')+",";
		}
   	    if(strTestFormID.length>0)strTestFormID = strTestFormID.substr(0, strTestFormID.length - 1);

		var url = JShell.System.Path.ROOT + me.editUrl;
		me.BUTTON_CAN_CLICK = false;
		
		JShell.Server.post(url,Ext.JSON.encode({delIDList:strTestFormID}),function(data){
			me.BUTTON_CAN_CLICK=true;
			me.hideMask();
			if(data.success){
				if(!msg)msg="批量删除检验单成功!";
				else msg+="批量删除检验单成功!";
				JShell.Msg.alert(msg);
				
				//已被删除行从列表中删除
				Ext.Array.each(list, function(record, index, allItems) {
					var isAdd = true;
					me.store.each(function(rec) {
						if(rec.get('LisTestForm_Id') == record.get('LisTestForm_Id')) {
							me.store.remove(rec);
							return false;
						}
					});
				});
				me.fireEvent('save', me);    
			}else{
				JShell.Msg.alert(data.msg);
			}
		});
	},
	//默认全选
	onSelect : function(bo){
		var me = this,
		     recs = me.store.data.items;
	    for(var i=0;i<recs.length;i++){
	    	if(bo)me.getSelectionModel().select(i,true,false);  
	    	else 
	    	    me.getSelectionModel().deselectAll(true);  
	    }
	},
	//检验日期+样本号排序
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