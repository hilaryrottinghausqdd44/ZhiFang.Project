/**
 * 单项目结果录入
 * @author liangyl
 * @version 2020-01-10
 */
Ext.define('Shell.class.lts.batchedit.SingleResult', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '单项目结果录入',
	width: 800,
	height: 500,
    /**获取样本单项目数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
	/**样本单项目结果保存*/
    addUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItemResult',

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
	/**小组*/
	SectionID: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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
		
		me.addEvents('check','onSaveClick');
		//创建功能按钮栏Items
		me.buttonToolbarItems = ['save', {
			text: '项目结果导出', tooltip: '项目结果导出', iconCls: 'button-exp',
			handler: function () {

			}
		},{
			width:220,labelWidth:60,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'LBItemName',fieldLabel:'检验项目',
			className:'Shell.class.lts.batchedit.sectionitem.CheckGrid',emptyText: '检验项目',
			classConfig:{checkOne:true,SectionID:me.SectionID,defaultWhere:'LBItem.GroupType=0'},
			listeners : {
				check: function(p, record) {
					me.fireEvent('check', p, record);
				}
			}
		},{
			xtype:'textfield',itemId:'LBItemID',fieldLabel:'项目ID',hidden:true
		},{
			xtype:'textfield',itemId:'LBItemPrec',fieldLabel:'项目精度',hidden:true
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
			text:'检验日期',dataIndex:'LisTestForm_GTestDate',width:85,
			sortable:true,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = JShell.Date.toString(value, true) || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				if(!record.get('LisTestItem_LBItem_Id'))meta.style = 'background-color:#BEBEBE;';
				return v;
			}
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
			text:'姓名',dataIndex:'LisTestForm_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'性别',dataIndex:'LisTestForm_LisPatient_GenderName',width:70,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'原报告值',dataIndex:'LisTestItem_ReportValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'录入报告值',dataIndex:'LisTestItem_NewReportValue',width:100,
			sortable:false,menuDisabled:true,style: 'font-weight:bold;color:white;background:orange;',
			editor: {xtype: 'textfield'},defaultRenderer:true
		},{
			text:'仪器结果',dataIndex:'LisTestItem_OriglValue',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'参考范围',dataIndex:'LisTestItem_RefRange',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目ID',dataIndex:'LisTestItem_LBItem_Id',width:190,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'LisTestItem_LBItem_CName',width:190,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目精度',dataIndex:'LisTestItem_LBItem_Prec',width:190,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本单项目ID',dataIndex:'LisTestItem_Id',width:70,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'样本单ID',dataIndex:'LisTestForm_Id',width:70,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text: '是否存在', dataIndex: 'Exist', width: 70,
			renderer: function (value, meta, record) {
				if (value == 1) {
					meta.style = 'color:green;';
					return "是";
				} else {
					meta.style = 'color:red;';
					return "否";
				}
			}
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

        items.push({ xtype: "checkbox",boxLabel: '<span style="color:blue;font-weight:bold;">如果检验单不存在该项目,录入报告值有值,添加</span>',
            inputValue: "true",checked: true,itemId: "isAddItem",name: "isAddItem",fieldLabel: "",margin:'0 5 0 5'
		},{
	        xtype: 'displayfield',fieldLabel: '',value: '报告值=删除(汉字删除),表示该项目删除',margin:'0 5 0 5'
	    });
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	//选择项目
	onCheckItem : function(TestFormList,p,record){
		var me = this;
		var CName = me.getComponent('buttonsToolbar').getComponent('LBItemName');
		var ID = me.getComponent('buttonsToolbar').getComponent('LBItemID');
		var Prec = me.getComponent('buttonsToolbar').getComponent('LBItemPrec');
		CName.setValue(record ? record.get('LBSectionItem_LBItem_CName') : '');
		ID.setValue(record ? record.get('LBSectionItem_LBItem_Id') : '');
		Prec.setValue(record ? record.get('LBSectionItem_LBItem_Prec') : '');
        if(!record){
        	me.store.removeAll();
        	p.close();
        	return;
        }
		me.onAddItem(TestFormList);
		p.close();
	},
	//添加单项目
	onAddItem : function(TestFormList){
		var me = this;
		
		var ID = me.getComponent('buttonsToolbar').getComponent('LBItemID'),
			CName = me.getComponent('buttonsToolbar').getComponent('LBItemName'),
			Prec = me.getComponent('buttonsToolbar').getComponent('LBItemPrec');
        if(!ID.getValue())return;
        
        me.store.removeAll();
        
	    for(var i=0;i<TestFormList.length;i++){
			var id = TestFormList[i].data.LisTestForm_Id;
			var obj =TestFormList[i].data;
			obj.LisTestItem_LBItem_Id = ID.getValue();
			obj.LisTestItem_LBItem_CName = CName.getValue();
			obj.LisTestItem_LBItem_Prec = Prec.getValue();
			obj.LisTestItem_NewReportValue = "";
			obj.LisTestItem_ReportValue = "";
			obj.LisTestItem_OriglValue = "";
			obj.LisTestItem_RefRange = "";
			obj.LisTestItem_Id = "";
			obj.Exist = 0;

			me.getTestItemByID(id,ID.getValue(),function(list){
				for(var j=0;j<list.length;j++){
					obj.LisTestItem_NewReportValue = '';
					obj.LisTestItem_ReportValue = list[j].LisTestItem_ReportValue;
					obj.LisTestItem_LBItem_Id = list[j].LisTestItem_LBItem_Id;
					obj.LisTestItem_LBItem_CName = list[j].LisTestItem_LBItem_CName;
					obj.LisTestItem_LBItem_Prec = list[j].LisTestItem_LBItem_Prec;
					obj.LisTestItem_OriglValue = list[j].LisTestItem_OriglValue;
					obj.LisTestItem_RefRange = list[j].LisTestItem_RefRange;
					obj.LisTestItem_Id = list[j].LisTestItem_Id;
					obj.Exist = 1;
				}
			});
            me.store.add(obj);
		}
	},
	//根据样本单ID调用样本单项目服务，组合拼成数据行
	getTestItemByID:function(TestFormID,ItemID,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl;
		url += '&fields=LisTestItem_LBItem_Id,LisTestItem_LBItem_CName,LisTestItem_LBItem_Prec,' +
			'LisTestItem_ReportValue,LisTestItem_OriglValue,LisTestItem_RefRange,LisTestItem_Id';
		url+='&where=(listestitem.LisTestForm.Id='+TestFormID +' and listestitem.LBItem.Id='+ItemID+')';
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}else{
				JShell.Msg.error(data.msg);
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
		    isSingleItem: true
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
			if(items[i].data.LisTestItem_NewReportValue){
				var obj ={
					LBItem: {
						Id: items[i].data.LisTestItem_LBItem_Id,
						CName: items[i].data.LisTestItem_LBItem_CName,
						Prec: items[i].data.LisTestItem_LBItem_Prec,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					},
					LisTestForm: {
						Id: items[i].data.LisTestForm_Id,
						DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
					}
				};
				if(items[i].data.LisTestItem_PItemID){
					obj.PItemID = items[i].data.LisTestItem_PItemID;
				}
                if(items[i].data.LisTestItem_Id)obj.Id = items[i].data.LisTestItem_Id;
				obj.ReportValue = items[i].data.LisTestItem_NewReportValue;
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