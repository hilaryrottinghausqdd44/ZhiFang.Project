/**
 * 添加检验项目
 * @author liangyl
 * @version 2019-12-17
 */
Ext.define('Shell.class.lts.batchedit.ItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '添加检验项目列表',
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
    //修改数据服务路径
	//editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestItemByField',
	//批量新增样本单项目
	addUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItem',
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
	hasRownumberer: true,
	//小组ID
    SectionID:null,
     //按钮是否可点击
    BUTTON_CAN_CLICK:true, 
     /**已选择的检验单*/
    LisTestFormList:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.addEvents('onSaveClick','onDelClick');
		
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
	
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'组合项目',dataIndex:'LisTestItem_PLBItem_CName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目id',dataIndex:'LisTestItem_LBItem_Id',width:180,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LisTestItem_LBItem_CName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目简称',dataIndex:'LisTestItem_LBItem_SName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'组合项目ID',dataIndex:'LisTestItem_PItemID',width:70,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'检验项目ID',dataIndex:'LisTestItem_Id',width:70,hidden:true,
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
        items.push(me.createToolbar());
		return items;
	},
	/**创建功能按钮栏*/
	createToolbar: function() {
		var me = this,
			items = [];

        items.push({ xtype: "checkbox",boxLabel: '<span style="color:blue;font-weight:bold;">如果检验单已有项目,采用该组合项目</span>',
            inputValue: "true",checked: false,itemId: "isRepPItem",name: "isRepPItem",fieldLabel: "",margin:'0 5 0 5'
		},{
	        xtype: 'displayfield',fieldLabel: '',value: '注:检验单如果没有这些项目,会添加这些项目',margin:'0 5 0 15'
	    });
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift({text:'选项目',tooltip:'选项目',iconCls:'button-add',
			handler: function(){
				JShell.Win.open('Shell.class.lts.batchedit.item.App',{
					width:'95%',height:'95%',
					SectionID:me.SectionID,
					listeners:{
						save : function(p,items){
							for(var i=0;i<items.length;i++){
								var isAdd = true;
								me.store.each(function(rec) {
									if(rec.get('LisTestItem_LBItem_Id') == items[i].data.LBSectionItem_LBItem_Id) {
										//如果列表存在非组合项目，需要替换为组合项目
										if(!rec.get('LisTestItem_PItemID'))rec.set('LisTestItem_PItemID',items[i].data.LBSectionItem_LBItem_GroupItemID);
										isAdd = false;
										return false;
									}
								});
								if(isAdd == true){
								    var result = Ext.encode(items[i].data);
								    var obj = result.replace(/LBSectionItem/g,"LisTestItem");
						            obj = Ext.JSON.decode(obj);
						            obj.LisTestItem_PLBItem_CName = items[i].data.LBSectionItem_LBItem_GroupItemCName;
						            obj.LisTestItem_PItemID = items[i].data.LBSectionItem_LBItem_GroupItemID;
									me.store.add(obj);
								}
							}
							p.close();
						}
					}
				}).show();
			}
		}, 'del', {
			xtype: 'button', iconCls: 'button-accept', text: '执行添加项目',
			tooltip: '执行',
			handler: function () {
				me.fireEvent('onSaveClick', me);
			}
		});
		return buttonToolbarItems;
	},
	//获取listAddTestItem
	getAddTestItem : function(testFormID){
		var me = this;
		var items = me.store.data.items,
		    len = items.length,
		    list=[];
		for(var i=0;i<len;i++){
			var obj ={
				LBItem:{Id:items[i].data.LisTestItem_LBItem_Id,DataTimeStamp:[0,0,0,0,0,0,0,0]}
			};
			
			if(items[i].data.LisTestItem_PItemID){
				obj.PLBItem = {Id:items[i].data.LisTestItem_PItemID,DataTimeStamp:[0,0,0,0,0,0,0,0]};
			}
			list.push(obj);
		}
		return list; 
	},
	//保存(批量新增样本单项目)
	onSaveClick : function(){
		var me = this;
		me.fireEvent('onSaveClick', me);
    },
    /**
     * 保存(批量新增样本单项目)
     * list检验单
     * */
	onSave : function(list){
	   	var me = this;
	   	if(!me.BUTTON_CAN_CLICK)return;
	    me.LisTestFormList = list;
	   	//校验
	   	if(me.store.data.items.length==0){
	   		JShell.Msg.alert('请选择添加项目后再保存');
	   		return;
	   	}
	   	
	    var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var isRepPItem = buttonsToolbar.getComponent('isRepPItem').getValue(); 
		
		me.showMask(me.saveText);//显示遮罩层
		
		var testFormID="",arr=[];
	   	for(var i =0;i<list.length;i++){
	   		arr.push(list[i].data.LisTestForm_Id);
		}
	   	testFormID = arr.splice(',');
	   	var entity ={
			testFormID:testFormID.join(','),
			listAddTestItem:me.getAddTestItem(testFormID),
			isRepPItem:isRepPItem
		};
		me.addOne(entity);
	},
    addOne:function(entity){
		var me = this;
		var url = JShell.System.Path.ROOT + me.addUrl;
		me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
			me.BUTTON_CAN_CLICK=true;
			me.hideMask();
			if(data.success){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				me.loadDataByItem();
			}else{
				JShell.Msg.alert(data.msg);
			}
		});
	},
	//加载批量检验单项目的数据
	loadDataByItem : function(){
		var me = this;
		//只加载存在此次批量新增检验单项目的的数据
		var id = me.LisTestFormList[0].data.LisTestForm_Id;
		if(!id)return;
	    me.defaultWhere="";
	    var items = me.store.data.items;
	    var StrID = me.getStrID(items);
	    if(StrID.length==0)return;
		me.defaultWhere="listestitem.LisTestForm.Id="+id+' and listestitem.LBItem.Id in('+StrID+') and listestitem.MainStatusID=0';
	    me.onSearch();
	},
	//获取批量新增的检验项目
	getStrID : function(items){
		var me = this;
	    var arr=[],strIds="",str="";
	    for(var i=0;i<items.length;i++){
	    	arr.push(items[i].data.LisTestItem_LBItem_Id);
	    }
	    if(arr.length>0)str = arr.splice(',');
	    if(str.length>0)strIds = str.join(',');
		return strIds;
	},
	//删除按钮
	onDelClick : function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onDelClick', records[0]);
	},
	/**删除选中检验项目*/
	onDel : function(list,record){
		var me = this;
		if(!me.BUTTON_CAN_CLICK)return;
		
		JShell.Msg.del(function(but) {
			if (but != "ok") return;
			me.BUTTON_CAN_CLICK = true;
			me.store.remove(record);
		});
		
	}
});