/**
 *合并项目
 * @author liangyl
 * @version 2019-11-20
 */
Ext.define('Shell.class.lts.itemmerge.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'项目合并(糖耐量)',
    addUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemMergeByVOEntity',
    //获取要合并的检验样本项目信息
	selectUrl: '/ServerWCF/LabStarService.svc/LS_UDTO_QueryItemMergeInfo',
	isloadResult:false,
    hasLoadMask:true,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.PatientGrid.on({
			//itemclick: function (v, record) {
			//	me.body.mask("加载中");
			//	me.onSearch(record,me.PatientGrid.getGroupItemObj());
			//},
			select: function (RowModel, record) {
				JcallShell.Action.delay(function () {
					me.onSearch(record, me.PatientGrid.getGroupItemObj());
				}, this, 300);
			},
			nodata:function(){
			}
		});
		me.Panel.on({
			save : function(){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				me.PatientGrid.onSearch();
//				me.close();
			},
			ItemGridAfterLoad: function (p) {
				//me.body.unmask();
			}
		});
	},
	initComponent:function(){
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		var toolitems=[];
	    toolitems.push({text:'合并',tooltip:'合并',iconCls:'button-accept',margin:'0 0 0 10',
		     handler:function(but,e){
		    	me.onSaveClick();
		    }
		});
        toolitems.push('->',{text:'关闭',tooltip:'关闭',iconCls:'button-del', 
            handler:function(but,e){
		    	me.close();
		    }
        });
		items.push(Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			itemId:'bottomToolbar',
			items:toolitems
		}));
		return items;
	},
	createItems:function(){
		var me = this;
		me.PatientGrid = Ext.create('Shell.class.lts.itemmerge.PatientGrid', {
			region:'west',
			width:320,
			itemId:'PatientGrid',
			header:false,
			split:true,
			collapsible:false
		});
		me.Panel = Ext.create('Shell.class.lts.itemmerge.Panel', {
			region:'center',
			itemId:'Panel',
			header:false
		});
		return [me.PatientGrid,me.Panel];
	},
	onSearch : function(record,GroupItemObj){
		var me = this;
		JShell.Action.delay(function(){
			var params = me.PatientGrid.getParams();
			var obj = {
				itemID:params.itemID,
				beginDate: params.beginDate,
				endDate:params.endDate,
				PatNo:record.get('LBMergeItemFormVO_PatNo'),
			    /**病人姓名*/
				CName:record.get('LBMergeItemFormVO_CName'),
				isMerge:record.get('LBMergeItemFormVO_IsMerge') ? '1' : ''
			}
			me.Panel.onSearch(obj,GroupItemObj);
		},null,200);
	},
	isCheck : function(){
		var me = this;
	    var items = me.Panel.ItemGrid.store.data.items,
		    itemslen = items.length;
		var isExec = false;
		for(var i = 0 ;i<itemslen;i++){
			var IsMerge = items[i].data.LBMergeItemVO_IsMerge ? 1 :0;
			if(IsMerge==1){
				isExec= true;
				break;
			}
		}
		return isExec;
	},
	//执行合并
	onSaveClick : function(){
		var me = this;
	    var url = JShell.System.Path.ROOT + me.selectUrl;
	    var recs = me.PatientGrid.getSelect(),
	        len = recs.length;
	        
	    if(len == 0) return;
	    
	    var items = me.Panel.ItemGrid.store.data.items,
		    itemslen = items.length;
	     //校验
	    var isExec = me.isCheck();
		if(!isExec){
			JShell.Msg.alert('合并项必须有一个是"是"!');
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		var strPatNo = "";
		
		for(var i=0;i<len;i++){
			var PatNo = recs[i].data.LBMergeItemFormVO_PatNo;
			if(strPatNo) return;
		 	for(var j=0;j<itemslen;j++){
	    		if(PatNo == items[j].data.LBMergeItemVO_LisTestItem_LisTestForm_PatNo){
	    			strPatNo=PatNo;
	    			break;
	    		}
	    	}
		}
		
	    for(var i=0;i<len;i++){
	    	var PatNo = recs[i].data.LBMergeItemFormVO_PatNo;
	    	//当前选择行的项目信息
	    	if(strPatNo == PatNo){
		        var listLBMergeItemVO = [];
				for(var j = 0; j<itemslen; j++){
					var obj = {
						ParItemID:items[j].data.LBMergeItemVO_ParItemID,
						ParItemName:items[j].data.LBMergeItemVO_ParItemName,
						ChangeItemID:items[j].data.LBMergeItemVO_ChangeItemID,
						ChangeItemName:items[j].data.LBMergeItemVO_ChangeItemName,
						IsMerge:items[j].data.LBMergeItemVO_IsMerge ? 1 :0,	
						LBChangeItem: { Id: items[j].data.LBMergeItemVO_ChangeItemID,DataTimeStamp:[0,0,0,0,0,0,0,0]},
						LisTestItem:{Id:items[j].data.LBMergeItemVO_LisTestItem_Id,LisTestForm:{Id:items[j].data.LBMergeItemVO_LisTestItem_LisTestForm_Id,MainStatusID:items[j].data.LBMergeItemVO_LisTestItem_LisTestForm_MainStatusID,DataTimeStamp:[0,0,0,0,0,0,0,0]},LBItem:{Id:items[j].data.LBMergeItemVO_LisTestItem_LBItem_Id,DataTimeStamp:[0,0,0,0,0,0,0,0]}}
					};
					listLBMergeItemVO.push(obj);
				}
				me.OneUpdate(listLBMergeItemVO);
	    	}else{
	    		var params = me.PatientGrid.getParams();
	    		var obj ={
		        	itemID:params.itemID,
					patNo:PatNo,
					cName:recs[i].data.LBMergeItemFormVO_CName,
					beginDate: params.beginDate,
					endDate: params.endDate,
					isPlanish: true,
					fields: me.Panel.ItemGrid.getStoreFields(true).join(',')
		        }
		        JShell.Server.post(url,Ext.JSON.encode(obj),function(data){
					if(data.success){
					    if(data.value && data){
					    	var list = data.value.list,
					    	    listLBMergeItemVO =[];
				    		for(var i = 0; i<list.length; i++){
								var obj = {
									ParItemID:list[i].LBMergeItemVO_ParItemID,
									ParItemName:list[i].LBMergeItemVO_ParItemName,
									ChangeItemID:list[i].LBMergeItemVO_ChangeItemID,
									ChangeItemName:list[i].LBMergeItemVO_ChangeItemName,
									IsMerge:list[i].LBMergeItemVO_IsMerge ? 1 :0,	
									LBChangeItem:{Id:list[i].LBMergeItemVO_LisTestItem_LBItem_Id,DataTimeStamp:[0,0,0,0,0,0,0,0]},
									LisTestItem:{Id:list[i].data.LBMergeItemVO_LisTestItem_Id,LisTestForm:{Id:list[i].LBMergeItemVO_LisTestItem_LisTestForm_Id,MainStatusID:list[i].LBMergeItemVO_LisTestItem_LisTestForm_MainStatusID,DataTimeStamp:[0,0,0,0,0,0,0,0]},LBItem:{Id:list[i].LBMergeItemVO_LisTestItem_LBItem_Id,DataTimeStamp:[0,0,0,0,0,0,0,0]}}
								};
								listLBMergeItemVO.push(obj);
							}
					    	me.OneUpdate(listLBMergeItemVO);
					    }
					}else{
						JShell.Msg.error(data.msg);
					}
				});
	    	}
	    }
	},
	OneUpdate : function(listLBMergeItemVO){
		var me = this;
		var url =  JShell.System.Path.ROOT + me.addUrl;
		JShell.Server.post(url,Ext.JSON.encode({listLBMergeItemVO:listLBMergeItemVO}),function(data){
			if (data.success) {
				me.isloadResult = true;
				me.saveCount++;
			}else{
				me.saveErrorCount++;
				JShell.Msg.error(data.msg);
			}
			me.onSaveEnd(data);
		});
	},
	onSaveEnd:function(data){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			me.hideMask(); //隐藏遮罩层
			if (me.saveErrorCount == 0){
				me.Panel.onSearchChart();
				me.Panel.onUploadImg();
			}else{
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
		}
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} 
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} 
	}
});