/**
 * 结果偏移
 * @author liangyl
 * @version 2020-01-15
 */
Ext.define('Shell.class.lts.batchedit.ResultDeviation', {
	extend: 'Shell.ux.grid.PostPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '结果偏移',
	width: 800,
	height: 500,
	/**获取多样本单共有项目列表*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryCommonItemByTestFormID',
	//批量样本单项目结果偏移
    saveUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemResultOffset',
    //查询样本单项目
	select_TestItem_Url:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
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
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,

	//按钮是否可点击
    BUTTON_CAN_CLICK:true,
    /**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,

	//已选检验单
    TestFormList:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.addEvents('onSaveClick');

		//创建功能按钮栏Items
		me.buttonToolbarItems = [{
			xtype: 'button',iconCls: 'button-accept',text: '执行',
			tooltip: '执行',
			handler: function() {
				me.fireEvent('onSaveClick', me);
			}
		}];
		//数据列
		me.columns = me.createGridColumns();
	
	    me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;

		var columns = [{
			text:'检验单ID',dataIndex:'LisTestItem_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'检验项目编号',dataIndex:'LBItem_Id',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目名称',dataIndex:'LBItem_CName',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目简称',dataIndex:'LBItem_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'偏离系数',dataIndex:'Coefficient',width:80,
			editor: {xtype: 'numberfield',value: 1},sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'附加值',dataIndex:'AddValue',width:80,
			editor: {xtype: 'numberfield',value: 0},sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'默认参考范围',dataIndex:'',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
		/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',
			dock:'bottom',
			itemId:'bottombuttonsToolbar',
			items:[{
				xtype: 'label',text: '定量结果=原定量结果*偏移系数+附加值;',
				margin: '0 0 0 5'//,style: "font-weight:bold;color:blue;"
			},{
				xtype: 'label',text: '不设置偏移系数和附加值,该项目不处理;',
				margin: '0 0 0 15'
			},{
				xtype: 'label',text: '不设置偏移系数,默认=1;不设置附加值,默认=0;',
				margin: '0 0 0 15'
			}]
		};
		return items;
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.doFilterParams();
		return me.callParent(arguments);
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.params || {},
			ParaClass = {};
        var testFormIDList = "",arr=[];
        for(var i=0;i<me.TestFormList.length;i++){
        	arr.push(me.TestFormList[i].data.LisTestForm_Id);
        }
        var str  = arr.splice(',');
        if(str.length>0)testFormIDList = str.join(',');
        
		me.postParams = {
			testFormIDList:testFormIDList,
			fields: me.getStoreFields(true).join(',')
		};
	},
	
	/**根据样本单ID查询*/
	loadDataByTestFormId : function(list){
		var me = this;
		if(list.length==0){
			me.store.removeAll();
			return;
		}
		me.TestFormList = list;
	    if(me.TestFormList.length==0){
        	JShell.Msg.alert('请先选择样本单');
        	return;
        }
		me.onSearch();
	},
	/**
     * 保存
     * list检验单
     * */
	onSave : function(list){
	   	var me = this;
	   	if(!me.BUTTON_CAN_CLICK)return;
	    me.TestFormList = list;
	    
	    var items = me.store.data.items,
		    len = items.length; 
		    
		//满足偏移条件的数组，用于存储临时数据，减少循环匹配次数
		var CoefficientArr = [];
        //校验，获取列表项目id
		var itemids = "",str="",itemarr = [];
		for(var i=0;i<len;i++){
			var Coefficient = items[i].data.Coefficient;//系数
			var AddValue = items[i].data.AddValue;//附加值
			//不设置偏移系数和附加值时  不保存
            if((!Coefficient && AddValue) || (Coefficient && !AddValue) || (Coefficient && AddValue)){
            	itemarr.push(items[i].data.LBItem_Id);
            	CoefficientArr.push(items[i]);
            }
		}
		if(itemarr.length==0){
			JShell.Msg.alert("请设置偏移系数和附加值!");
			return;
		}
	    str = itemarr.splice(',');
		if(str.length>0)itemids = str.join(',');
		if(!itemids)return;
		
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = list.length;
		
	   	for(var i =0;i<list.length;i++){
	   		var testFormID = list[i].data.LisTestForm_Id;
			if(!testFormID)return;
			
			var testItemInfolist =[];
			//找出检验单勾选的检验单项目ID
			me.getTestItemByID(testFormID,itemids,function(addlist){
				if(addlist.length==0)return;
				for(var i = 0;i<addlist.length;i++){
					

					var LBItemId = addlist[i].LisTestItem_LBItem_Id;
					var LisTestItemID = addlist[i].LisTestItem_Id;
					
				    for(var j=0;j<CoefficientArr.length;j++){
				    	//根据项目id 匹配
				    	 if(LBItemId == CoefficientArr[j].data.LBItem_Id){
				    	 	var Coefficient = CoefficientArr[j].data.Coefficient;
				    	 	var AddValue = CoefficientArr[j].data.AddValue;
                            //不设置偏移系数，偏移系数默认=1
                            if(!Coefficient && AddValue)Coefficient=1;
                            //不设置附加值，附加值默认=0
                            if(Coefficient && !AddValue)AddValue=0;
                            
				    	 	testItemInfolist.push({
				    	 		LisTestItemID:LisTestItemID,
				    	 		LBItemID:LBItemId,
				    	 		Coefficient:Coefficient,
				    	 		AddValue:AddValue
				    	 	});
				    	 	break;
				    	 }
				    }
				}
			});
		   if(testItemInfolist.length>0) me.addOne(testItemInfolist);

		}
	},
	//循环保存
	addOne:function(entity){
		var me = this;
		var params = JSON.stringify(entity);
        var url = JShell.System.Path.ROOT + me.saveUrl;
        me.BUTTON_CAN_CLICK=false;
        JShell.Server.post(url,Ext.JSON.encode({testItemInfo:params}),function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if (me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				me.BUTTON_CAN_CLICK=true;
				if (me.saveErrorCount == 0){
				   JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				    //去掉脏数据,清空系数和附加值
				   	me.store.each(function(record) {
				   		record.set('Coefficient','');
				   		record.set('AddValue','');
					    record.commit();
			        });
				}else{
					JShell.Msg.error(data.msg);
				}
			}
		});
	},
	//根据样本单ID调用样本单项目服务，组合拼成数据行
	getTestItemByID:function(TestFormID,ItemID,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.select_TestItem_Url;
		url += '&fields=LisTestItem_Id,LisTestItem_LBItem_Id';
		url+='&where=listestitem.LisTestForm.Id='+TestFormID +' and listestitem.LBItem.Id in ('+ItemID+')';
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});