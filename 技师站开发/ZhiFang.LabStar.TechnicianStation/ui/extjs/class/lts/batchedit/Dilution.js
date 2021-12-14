/**
 * 稀释处理
 * @author liangyl
 * @version 2020-01-14
 */
Ext.define('Shell.class.lts.batchedit.Dilution', {
	extend: 'Shell.ux.grid.PostPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '检验样本信息列表',
	width: 300,
	height: 400,
	//获取多样本单共有项目列表
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryCommonItemByTestFormID',
	//查询样本单项目
	select_TestItem_Url:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
	/**稀释处理*/
    saveUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemResultDilution',

	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
    /**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	  /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	//已选检验单
    TestFormList:[],
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
	},
	initComponent: function() {
		var me = this;
		//创建数据列
		me.columns = me.createGridColumns();
        me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'检验项目编号',dataIndex:'LBItem_Id',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目名称',dataIndex:'LBItem_CName',width:180,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目简称',dataIndex:'LBItem_SName',width:120,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'默认参考范围',dataIndex:'',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];

		return columns;
	},
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift({
			fieldLabel:'稀释倍数',name:'Multiple',editable:true, 
			xtype:'uxSimpleComboBox',value:'10',hasStyle:true,
			width:150,labelWidth:60,labelAlign:'right',itemId:'Multiple',
			data:[
				['10','10'],
				['100','100'],
				['1000','1000']
			]
		},'-',{
			xtype: 'button',iconCls: 'button-accept',text: '执行',
			tooltip: '执行',
			handler: function() {
				me.fireEvent('onSaveClick', me);
			}
		});
		return buttonToolbarItems;
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
	onSelect : function(bo){
		var me = this;
	    var recs = me.store.data.items,
	        len = recs.length;
	    for(var i=0;i<len;i++){
	    	if(bo)me.getSelectionModel().select(i,true,false);
	    	 else 
	    	   me.getSelectionModel().deselectAll(true);
	    }
	},
	/**根据样本单ID查询*/
	loadDataByTestFormId : function(list){
		var me = this;
		me.TestFormList = list;
	    if(me.TestFormList.length==0){
        	JShell.Msg.alert('请先选择样本单');
        	return;
        }
		me.onSearch();
	},
	//根据样本单ID调用样本单项目服务，组合拼成数据行
	getTestItemByID:function(TestFormID,ItemID,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.select_TestItem_Url;
		url += '&fields=LisTestItem_Id';
		url+='&where=listestitem.LisTestForm.Id='+TestFormID +' and listestitem.LBItem.Id in ('+ItemID+')';
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
     /**
     * 保存
     * list检验单
     * */
	onSave : function(list){
	   	var me = this;
	   	if(!me.BUTTON_CAN_CLICK)return;
	    me.TestFormList = list;
	   	//校验,有勾选项
		var	records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var itemids = "",str="",itemarr = [];
		for(var i=0;i<records.length;i++){
			itemarr.push(records[i].data.LBItem_Id);
		}
		
	    if(itemarr.length>0)str = itemarr.splice(',');
		if(str.length>0)itemids = str.join(',');
		if(!itemids)return;
		
	    var buttonsToolbar = me.getComponent('buttonsToolbar');
		var dilutionTimes = buttonsToolbar.getComponent('Multiple').getValue(); 
		
		//校验 校验稀释倍数。稀释倍数不能小于等于0；不能等于1，等于1没有意义。当稀释系数＜1，提示稀释倍数＜1，确定要执行稀释样本结果调整吗？
		if(dilutionTimes==0 || dilutionTimes==1 ){
			JShell.Msg.alert("稀释倍数不能小于等于0；不能等于1");
			return;
		}
		
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = list.length;
		
	   	for(var i =0;i<list.length;i++){
	   		var testFormID = list[i].data.LisTestForm_Id;
			if(!testFormID)return;
			//找出检验单勾选的检验单项目ID
			me.getTestItemByID(testFormID,itemids,function(addlist){
				if(addlist.length==0)return;
				var testItemIDList="",testitemarr=[],strids="";
				for(var i = 0;i<addlist.length;i++){
					testitemarr.push(addlist[i].LisTestItem_Id);
				}
			    if(testitemarr.length>0)strids = testitemarr.splice(',');
			    if(strids.length>0)testItemIDList = strids.join(',');
			    var  entity = {testItemIDList:testItemIDList,dilutionTimes:dilutionTimes}
				me.addOne(entity);
			});
		}
	},
   //循环保存
	addOne:function(entity){
		var me = this;
        var url = JShell.System.Path.ROOT + me.saveUrl;
        me.BUTTON_CAN_CLICK=false;
        JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
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
				}else{
					JShell.Msg.error(data.msg);
				}
			}
		});
	}
});