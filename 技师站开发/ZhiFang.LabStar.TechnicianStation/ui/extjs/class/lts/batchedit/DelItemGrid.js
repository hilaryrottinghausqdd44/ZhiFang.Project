/**
 * 删除检验项目
 * @author zhangda
 * @version 2021-02-04
 */
Ext.define('Shell.class.lts.batchedit.DelItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '删除检验项目列表',
	width: 800,
	height: 500,
    /**获取样本单数据服务路径*/
	selectUrl:'',
	//批量删除样本单项目
	delUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestItemByTestFormIDList',
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
		
		me.addEvents('onDelClick');
		
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

        items.push({ xtype: "checkbox",boxLabel: '<span style="color:blue;font-weight:bold;">仅删除没有结果的项目</span>',
			inputValue: "true", checked: true, itemId: "isDelNoResultItem", name: "isDelNoResultItem",fieldLabel: "",margin:'0 5 0 5'
		},{
			xtype: "checkbox", boxLabel: '<span style="color:blue;font-weight:bold;">仅删除非医嘱项目</span>',
			inputValue: "true", checked: true, itemId: "isDelNoOrderItem", name: "isDelNoOrderItem", fieldLabel: "", margin: '0 5 0 5'
		}, {
			xtype: 'displayfield', fieldLabel: '', value: '注:检验单如果存在这些项目,会删除这些项目', margin: '0 5 0 15'
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
		},{
			xtype: 'button', iconCls: 'button-del', text: '执行删除项目',
			tooltip: '执行',
			handler: function () {
				me.onDelClick();
			}
		});
		return buttonToolbarItems;
	},
	//删除按钮
	onDelClick : function(){
		var me = this,
			records = me.store.data.items;

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onDelClick', records);
	},
	/**批量删除检验项目
	 *list 检验单 
	 */
	onDel: function (list, records){
		var me = this;
		me.LisTestFormcList = list;
		if(!me.BUTTON_CAN_CLICK)return;
		
		JShell.Msg.del(function(but) {
			if (but != "ok") return;
     
			me.showMask(me.delText); //显示遮罩层
			var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl,
				testFormIDList = [],//检验单ID
				itemIDList = [],//项目Id
				isDelNoResultItem = me.getComponent('buttonsToolbar2').getComponent('isDelNoResultItem').getValue(),//仅删除没有结果项目
				isDelNoOrderItem = me.getComponent('buttonsToolbar2').getComponent('isDelNoOrderItem').getValue();//仅删除非医嘱项目
			for (var i = 0; i < list.length;i++) {
				testFormIDList.push(list[i].get('LisTestForm_Id'));
			}
			for (var j = 0; j < records.length; j++) {
				itemIDList.push(records[j].get('LisTestItem_LBItem_Id'));
			}
			me.BUTTON_CAN_CLICK = false;
			JShell.Server.post(url, Ext.JSON.encode({ testFormIDList: testFormIDList.join(","), itemIDList: itemIDList.join(","), isDelNoResultItem: isDelNoResultItem, isDelNoOrderItem: isDelNoOrderItem }), function (data) {
				me.hideMask(); //隐藏遮罩层
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					JShell.Msg.alert("删除成功！");
					me.store.removeAll();
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		});
		
	}
});