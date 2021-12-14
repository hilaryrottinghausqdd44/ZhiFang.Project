/**
 * 检验样本信息列表
 * @author liangyl	
 * @version 2019-11-26
 */
Ext.define('Shell.class.lts.itemmerge.PatientGrid', {
    extend: 'Shell.ux.grid.PostPanel',
	requires: [
		'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger',
		'Ext.ux.CheckColumn'
	],
	title: '检验样本信息列表',
	width: 300,
	height: 400,
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryItemMergeFormInfo',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'LBMergeItemFormVO_PatNo',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**默认选中数据*/
	autoSelect: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	defaultAddDate:null,
    /**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	  /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	GroupItemObj:[],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		//查询展开
		setTimeout(function () {
			me.getComponent("buttonsToolbar").getComponent('LBItemName').onTriggerClick();
		}, 500);
	},
	initComponent: function() {
		var me = this;
		me.initDate();
		//创建数据列
		me.columns = me.createGridColumns();
        me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || me.createDockedItems();
		
		me.callParent(arguments);
	},

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'病历号',dataIndex:'LBMergeItemFormVO_PatNo',width:120,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'姓名',dataIndex:'LBMergeItemFormVO_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'合并',dataIndex:'LBMergeItemFormVO_IsMerge',width:40,
			sortable:false,menuDisabled:true,
			isBool: true,align: 'center',type: 'bool',defaultRenderer:true
		}];

		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = [];
		
		items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift({
			width:255,labelWidth:80,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'LBItemName',fieldLabel:'合并项目名称',
			className:'Shell.class.lts.item.CheckGrid',emptyText: '项目名称',
			classConfig: { checkOne: true,autoSelect:true ,defaultWhere:'lbitem.IsUnionItem=1 and lbitem.GroupType=1'},
			listeners : {
				check: function(p, record) {
					var CName = me.getComponent('buttonsToolbar').getComponent('LBItemName');
					var ID = me.getComponent('buttonsToolbar').getComponent('LBItemID');
					CName.setValue(record ? record.get('LBItem_CName') : '');
					ID.setValue(record ? record.get('LBItem_Id') : '');
					me.GroupItemObj=[];
					me.GroupItemObj.push({
						LBItem_Id:record.get('LBItem_Id'),
						LBItem_CName:record.get('LBItem_CName') ? record.get('LBItem_CName'): '',
						LBItem_SName:record.get('LBItem_SName') ? record.get('LBItem_SName') : ''
					});
					p.close();
				}
			}
		},{
			xtype:'textfield',itemId:'LBItemID',fieldLabel:'合并项目名称ID',hidden:true
		});
		return buttonToolbarItems;
	},
	//获取组合项目信息
	getGroupItemObj :function(){
		var me =this;
		return me.GroupItemObj[0];
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:[{
				xtype: 'uxdatearea',itemId: 'date',labelWidth: 60,labelAlign: 'right',
				fieldLabel: '日期范围',value:me.defaultAddDate,
				listeners: {
					enter: function() {
						me.onSearch();
					}
				}
			}, '-', {
				xtype: 'button',iconCls: 'button-search',text: '查询',
				tooltip: '查询操作',
				handler: function() {
					me.onSearch();
				}
			}]
		};
		return items;
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JShell.System.Date.getDate();
//		var Sysdate=new Date();
		var dateArea = {
			start:JShell.Date.getNextDate(Sysdate, -1),
			end: Sysdate
		};
		me.defaultAddDate = dateArea;
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
	    //合并项目
		var LBItemID = me.getComponent('buttonsToolbar').getComponent('LBItemID').getValue();
		if(!LBItemID){
			JShell.Msg.alert('请选择合并项目!');
			return;
		}
		var date = me.getComponent('buttonsToolbar2').getComponent('date');
		me.postParams = {
			itemID:LBItemID,
			beginDate: JShell.Date.toString(date.getValue().start,true),
			endDate:JShell.Date.toString(date.getValue().end,true),
			isPlanish: true,
			fields: me.getStoreFields(true).join(',')
		};
	},
	/**获取查询条件*/
	getParams : function(){
		var me = this;
		//合并项目
		var LBItemID = me.getComponent('buttonsToolbar').getComponent('LBItemID').getValue();
		if(!LBItemID){
			JShell.Msg.alert('请选择合并项目!');
			return;
		}
		var date = me.getComponent('buttonsToolbar2').getComponent('date');
		return  {
			itemID:LBItemID,
			beginDate: JShell.Date.toString(date.getValue().start,true),
			endDate:JShell.Date.toString(date.getValue().end,true)
		}
	},
	/**获取选中行*/
	getSelect : function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
    	return  records;
	},
	/**查询数据*/
	onSearch:function(){
		var me = this,
			collapsed = me.getCollapsed();
		
		me.defaultLoad = true;
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.disableControl();//禁用 所有的操作功能
		
		me.showMask(me.loadingText);//显示遮罩层
		
		var url = me.getLoadUrl();
		var params = Ext.JSON.encode(me.postParams);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				var obj = data.value || {};
				var list = obj.list || [];
				me.store.loadData(list);
				
				if(list.length == 0){
					var msg = me.msgFormat.replace(/{msg}/,JShell.Server.NO_DATA);
					JShell.Action.delay(function(){me.getView().update(msg);},200);
				}else{
					if(me.autoSelect){
						me.doAutoSelect(list,true);
					}
				}
			}else{
				var msg = me.errorFormat.replace(/{msg}/,data.msg);
				JShell.Action.delay(function(){me.getView().update(msg);},200);
			}
		});
	}
});