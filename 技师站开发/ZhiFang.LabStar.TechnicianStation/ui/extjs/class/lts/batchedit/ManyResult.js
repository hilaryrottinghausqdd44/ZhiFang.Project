/**
 * 多项目结果录入
 * @author liangyl
 * @version 2020-01-10
 */
Ext.define('Shell.class.lts.batchedit.ManyResult', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '多项目结果录入',
	width: 800,
	height: 500,
    /**获取该小组下所有项目数据服务路径*/
	selectUrl:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?isPlanish=true',
	 /**获取组合项目子项服务路径*/
	select_ItemGroup_Url:'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?isPlanish=true',
	/**样本单项目结果保存*/
    addUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItemResult',
	/**默认加载*/
	defaultLoad: true,
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
	defaultWhere:'LBItem.GroupType=0',//默认显示单项
	defaultOrderBy: [{ property: 'LBSectionItem_DispOrder', direction: 'ASC' }, { property: 'LBSectionItem_LBItem_DispOrder', direction: 'ASC' }],

	//按钮是否可点击
    BUTTON_CAN_CLICK:true,
     /**小组*/
	SectionID: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';

		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		if (me.SectionID) me.defaultWhere += "LBSection.Id=" + me.SectionID;
		me.addEvents('onSaveClick');
		
		//创建功能按钮栏Items
		me.buttonToolbarItems = ['save','-',{
			width:220,labelWidth:60,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'LBItemName',fieldLabel:'检验项目',
			className:'Shell.class.lts.batchedit.sectionitem.CheckGrid',emptyText: '检验项目',
			classConfig: { checkOne: true,SectionID: me.SectionID },
			listeners : {
				check: function (p, records) {
					if (records)//选中
						me.onCheck(p, records);
					else//清除
						me.onSearch();
				}
			}
		},{
			xtype:'textfield',itemId:'LBItemID',fieldLabel:'项目ID',hidden:true
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
			text:'组合项目',dataIndex:'PItemName',width:80,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'组合项目ID',dataIndex:'PItemID',width:70,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目id',dataIndex:'LBSectionItem_LBItem_Id',width:150,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LBSectionItem_LBItem_CName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目简称',dataIndex:'LBSectionItem_LBItem_SName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目精度',dataIndex:'LBSectionItem_LBItem_Prec',width:100,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'报告值',dataIndex:'ReportValue',width:100,
			editor: {xtype: 'textfield'},style: 'font-weight:bold;color:white;background:orange;',
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

        items.push({ xtype: "checkbox",boxLabel: '<span style="color:blue;font-weight:bold;">如果检验单不存在该项目,添加</span>',
            inputValue: "true",checked: false,itemId: "isAddItem",name: "isAddItem",fieldLabel: "",margin:'0 5 0 5'
		},{
	        xtype: 'displayfield',fieldLabel: '',value: '批量设置检验结果=报告值(报告值不设置,则不处理该项目)',margin:'0 5 0 5'
	    },{
	        xtype: 'displayfield',fieldLabel: '',value: '报告值=空(汉字空),表示批量将该结果设置为空(Null)',margin:'0 5 0 5'
	    });
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	//添加项目
	onCheck :function(p,record){
		var me = this;
        //先清空
        me.store.removeAll();
        
        var CName = me.getComponent('buttonsToolbar').getComponent('LBItemName');
		var ID = me.getComponent('buttonsToolbar').getComponent('LBItemID');
		CName.setValue(record ? record.get('LBSectionItem_LBItem_CName') : '');
		ID.setValue(record ? record.get('LBSectionItem_LBItem_Id') : '');

        if(record){
        	 //如果选择的是组合项目需要找出子项,添加到列表
	       	if(record.data.LBSectionItem_LBItem_GroupType==1){
	       		me.getItemGroup(record.data.LBSectionItem_LBItem_Id,function(list){
					for(var j=0;j<list.length;j++){
						me.store.add({
							LBSectionItem_LBItem_Id: list[j].LBItemGroup_LBItem_Id,
							LBSectionItem_LBItem_CName:list[j].LBItemGroup_LBItem_CName,
							LBSectionItem_LBItem_SName: list[j].LBItemGroup_LBItem_SName,
							LBSectionItem_LBItem_Prec: list[j].LBItemGroup_LBItem_Prec,
							PItemID:record.data.LBSectionItem_LBItem_Id,
	                        PItemName:record.data.LBSectionItem_LBItem_CName
						});
					}
				});
	       	}else{
	       		me.store.add({
					LBSectionItem_LBItem_Id:record.data.LBSectionItem_LBItem_Id,
					LBSectionItem_LBItem_CName:record.data.LBSectionItem_LBItem_CName,
					LBSectionItem_LBItem_SName:record.data.LBSectionItem_LBItem_SName,
					LBSectionItem_LBItem_Prec:record.data.LBSectionItem_LBItem_Prec
				});
	       	}
        }
        
		p.close();
	},
	/**根据组合项目id获取组合项目子项*/
	getItemGroup: function(GroupItemID,callback) {
		var me = this,
			url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.select_ItemGroup_Url;
			
		url += '&fields=LBItemGroup_LBItem_Id,LBItemGroup_LBItem_CName,LBItemGroup_LBItem_SName,LBItemGroup_LBItem_Prec';
		url += '&where=GroupItemID='+GroupItemID;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value ? data.value.list : [];
				callback(list);
			} 
		},false);
	},
	onSaveClick : function(){
		var me = this;
		me.fireEvent('onSaveClick', me);
	},
	  /**
     * 保存(批量新增样本单项目结果)
     * list检验单
     * */
	onSave : function(list){
	   	var me = this;
	   	if(!me.BUTTON_CAN_CLICK)return;
	   	//校验
	   	if(me.store.data.items.length==0){
	   		JShell.Msg.alert('请选择项目后再保存');
	   		return;
	   	}
        var listAddTestItem = me.getAddTestItem();
		if(listAddTestItem.length==0){
			JShell.Msg.alert('录入报告值不能为空');
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		
		//是否新增项目
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var isAddItem = buttonsToolbar.getComponent('isAddItem').getValue(); 
		
		
		var arr=[];
	   	for(var i =0;i<list.length;i++){
	   		arr.push(list[i].data.LisTestForm_Id);
		}
	   	var strArr=arr.splice(',');
	   	var entity ={
			testFormID:strArr.join(','),
			listAddTestItem:listAddTestItem,
			isAddItem: isAddItem,
		    isSingleItem: false
		};
		me.addOne(entity);
	},
	//获取listAddTestItem
	getAddTestItem : function(){
		var me = this;
		var items = me.store.data.items,
		    len = items.length,
		    list=[];
		for(var i=0;i<len;i++){
			if(items[i].data.ReportValue){
				var obj ={
					LBItem: {
						Id: items[i].data.LBSectionItem_LBItem_Id,
						CName: items[i].data.LBSectionItem_LBItem_CName,
						Prec: items[i].data.LBSectionItem_LBItem_Prec,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					}
				};
				if(items[i].data.PItemID){
					obj.PLBItem = { Id: items[i].data.PItemID, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
				}
				obj.ReportValue = items[i].data.ReportValue;
				list.push(obj);
			}
		}
		return list; 
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
				//去掉脏数据
				me.store.each(function(record) {
				     record.commit();
		        });
			}else{
				JShell.Msg.alert(data.msg);
			}
		});
	}
});