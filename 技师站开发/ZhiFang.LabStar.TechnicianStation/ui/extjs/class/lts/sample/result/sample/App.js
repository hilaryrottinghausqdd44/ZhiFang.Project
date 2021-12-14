/**
 * 检验结果
 * @author Jcall
 * @version 2020-01-06
 */
Ext.define('Shell.class.lts.sample.result.sample.App', {
	extend:'Shell.ux.panel.AppPanel',
	requires:[
		'Shell.ux.toolbar.Button'
	],
	title:'检验结果',
	
	//项目列表服务
	GET_ITEM_LIST_URL:'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?isPlanish=true',
	
	//查询字段
	//采样项目名称(医嘱项目)、
	//检测项目名称(检验项目)、英文名称、简称
	//报告值、仪器原始数值、结果单位、参考范围、结果状态(仪器结果状态)
	//上次结果(前报告值)、对比(前值对比状态)
	//主键ID
	//组合项目名称、组合项目排序、项目排序
	SEARCH_FIELDS:[
		'LisOrderItem_HisItemName',
		'LBItem_Id','LBItem_CName','LBItem_EName','LBItem_SName','LBItem_Prec',
		'ReportValue', 'OriglValue', 'QuanValue', 'Unit', 'RefRange', 'ResultStatus','ResultStatusCode',
		'PreValue', 'PreCompStatus', 'PreValue2', 'PreValue3', 'BAlarmColor','AlarmColor',
		'Id', 'LisTestForm_Id', 'ResultComment','LisOrderItem_Id',
		'PLBItem_CName','PLBItem_DispOrder','LBItem_DispOrder'
	],
	//排序字段
	defaultOrderBy:[
		{property:'LisTestItem_PLBItem_DispOrder',direction:'ASC'},
		{property:'LisTestItem_LBItem_DispOrder',direction:'ASC'}
	],
	//获取的项目集合
	ItemListData:[],
	//默认显示方式:1列表,2表格
	defaultType:1,
	//小组ID
    sectionId:null,
	//检验单数据
    testFormRecord:null,
    //是否只读
	isReadOnly:false,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			beforeedit: function (editor, e, eOpts) {
	            if (me.testFormRecord.get('LisTestForm_MainStatusID') == '0') return true;
	            return false;
	        }
		});
	},
	initComponent:function(){
		var me = this;
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		//创建内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建挂靠功能栏
	createDockedItems:function(){
		var me = this,
			items = [];
			
		items.push({
			xtype:'radiogroup',fieldLabel:'',
			columns:2,width:205,vertical:true,
			items:[
				{boxLabel:'所有检验项目',name:'rb',inputValue:'1',checked:true},
				{boxLabel:'按医嘱项目',name:'rb',inputValue:'2'}
			],
			listeners:{
				change:function(com,newValue,oldValue,eOpts){
					var type = newValue.rb;
					if(type == 1){
						me.Grid.showByItem();
					}else if(type == 2){
						me.Grid.showByPItem();
					}
				}
			}
		});
		
		//只读模式没有新增、删除、保存功能
		if(!me.isReadOnly){
			items.push('-',{
		    	iconCls:'button-add',text:'新增',itemId:'add',tooltip:'如果样本信息还没保存，则自动保存样本信息后弹出新增页面',
		    	handler:function(but){me.onAddClick();}
			}, {
				iconCls: 'button-del', text: '删除', itemId: 'del', tooltip: '删除',
				handler: function (but) { me.onDelClick(); }
			},{
		    	iconCls:'button-save',text:'保存',itemId:'save',tooltip:'保存检验项目',
				handler: function (but) {
					//me.onSaveClick();
					me.fireEvent("save", me);//保存检验单及检验项目
				}
		    });
		}
		
		items.push('-',{
			xtype:'radiogroup',fieldLabel:'',
			columns:2,width:100,vertical:true,
			itemId:'SelectModel',
			items:[
				{boxLabel:'列表',name:'rbSelectModel',inputValue:1,checked:(me.defaultType == 1 ? true : false)}
				//{boxLabel:'表格',name:'rbSelectModel',inputValue:2,checked:(me.defaultType == 2 ? true : false)}
			],
			listeners:{
				change:function(com,newValue,oldValue,eOpts){
					me.showContent();
					if(newValue.rbSelectModel == 1){//列表
						me.getComponent('bottomToolbar').getComponent('config').hide();
					}else{//表格
						me.getComponent('bottomToolbar').getComponent('config').show();
					}
				}
			}
		},{
			text:'设置',iconCls:'button-config',
			itemId:'config',hidden:me.defaultType != 2 ? true : false,
			handler:function(){
				JShell.Win.open('Shell.class.lts.sample.result.sample.TableConfig',{}).show();
			}
		});
		
		var dockedItems = [{
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'bottomToolbar',
			items:items
		}];
		
		return dockedItems;
	},
	//创建内部组件
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.lts.sample.result.sample.Grid',{
			itemId: 'Grid', title: false, sectionId: me.sectionId,
			hidden:(me.defaultType != 1 ? true : false),
			listeners:{
				afterrender:function(p){
					p.showByItem();
				},
				aftersave:function(p){
					me.onSearch(me.testFormRecord);
				},
				delBatchLisTestItem: function (p) {
					me.onSearch(me.testFormRecord);
				}
			}
		});
		me.Table = Ext.create('Shell.class.lts.sample.result.sample.Table',{
			itemId: 'Table', title: false, sectionId: me.sectionId,
			hidden:(me.defaultType != 2 ? true : false),
			listeners:{
				aftersave:function(p){
					me.onSearch(me.testFormRecord);
				}
			}
		});
		
		me.Content = Ext.create('Ext.panel.Panel',{
			region:'center',itemId:'Content',sectionId:me.sectionId,
			header:false,border:false,layout:'fit',
			//autoScroll:true,bodyPadding:1,
			items:[me.Grid,me.Table]
		});
		me.Images = Ext.create('Shell.class.lts.sample.result.sample.Images2',{
			region:'east',itemId:'Images',width:155,sectionId:me.sectionId,
			header:false,//border:false,
			autoScroll:true,split:true,
			collapsible:true//,collapseMode:'mini'
		});

		return [me.Content,me.Images];
	},
	
	//显示列表面板
	showGridPanel:function(){
		var me = this;
		
		me.Table.hide();
		me.Grid.show();
		me.Grid.onLoadByData(me.ItemListData);
	},
	//显示表格面板
	showTablePanel:function(){
		var me = this;
		
		me.Grid.hide();
		me.Table.show();
		me.Table.onLoadByData(me.ItemListData);
	},
	
	//返回显示模式
	getShowMode:function(){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar'),
		    SelectModel = bottomToolbar.getComponent('SelectModel');
		    
		return SelectModel.getValue().rbSelectModel;
	},
	
	//查询数据
	onSearch:function(testFormRecord){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar');
		
		me.testFormRecord = testFormRecord;
		
		if (!me.testFormRecord) return;
		//不是检验中则不允许新增删除保存项目
		var add = me.getComponent('bottomToolbar').getComponent('add');
		var del = me.getComponent('bottomToolbar').getComponent('del');
		var save = me.getComponent('bottomToolbar').getComponent('save');
		if (me.testFormRecord.get("LisTestForm_MainStatusID") == 0) {
			if (add) add.show();
			if (del) del.show();
			if (save) save.show();
		} else {
			if (add) add.hide();
			if (del) del.hide();
			if (save) save.hide();
		}

		var url = JShell.System.Path.ROOT + me.GET_ITEM_LIST_URL;
		url += '&fields=LisTestItem_' + me.SEARCH_FIELDS.join(',LisTestItem_');
		url += '&where=listestitem.MainStatusID in (0,-1) and listestitem.LisTestForm.Id=' + me.testFormRecord.get('LisTestForm_Id');
		url += '&sort=' + Ext.JSON.encode(me.defaultOrderBy);
		
		JShell.Server.get(url,function(data){
			bottomToolbar.show();
			if(data.success){
				me.ItemListData = data.value.list || [];
				me.fireEvent('afterLoad',me,me.ItemListData);
				me.showContent();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
		me.Images.onSearch(testFormRecord);
	},
	//显示内容
	showContent:function(){
		var me = this,
			showMode = me.getShowMode();
			
		if(showMode == 1){//表格
			me.showGridPanel();
		}else if(showMode == 2){//列表
			me.showTablePanel();
		}
	},
	//新增项目
	onAddClick:function(){
		var me = this,
			testFormId = me.testFormRecord ? me.testFormRecord.get('LisTestForm_Id') : null;
			
		if(testFormId){
			me.showAddItemPanel(testFormId);
		}else{
			//如果是新增检验单状态时，则先保存检验单，再弹出新增项目
			me.fireEvent('noforminfo',me);
		}
	},
	//显示项目新增界面
	showAddItemPanel:function(testFormId){
		var me = this;
		JShell.Win.open('Shell.class.lts.sample.result.sample.add.App',{
			sectionId:me.sectionId,
			testFormId:testFormId,
			listeners:{
				save:function(p){
					p.close();
					JShell.Msg.alert("保存成功！",null,1000);
					me.onSearch(me.testFormRecord);
				}
			}
		}).show();
	},
	//删除项目
	onDelClick:function(){
		var me = this;
			
		if(!me.Grid.hidden){
			me.Grid.onDelClick(function(id){
				for(var i in me.ItemListData){
					if(me.ItemListData[i].LisTestItem_Id == id){
						me.ItemListData.splice(i,1);
						break;
					}
				}
			});
		}else{
			me.Table.onDelClick();
		}
	},
	//保存项目修改
	onSaveClick:function(){
		var me = this;
			
		if (!me.Grid.hidden) {
			me.Grid.onSaveClick(me.testFormRecord,function(){
				
			});
		}else{
			me.Table.onSaveClick(me.testFormRecord);
		}
	},
	//清空数据,禁用功能按钮
	clearData:function(){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar');
		
		me.testFormRecord = null;
		bottomToolbar.hide();
		me.Grid.hide();
		me.Table.hide();
		me.Images.clearData();//结果图片数据清空
	},
	//新增检验单
	isAdd:function(){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar');
		
		me.testFormRecord = null;
		me.ItemListData = [];
		me.Grid.clearData();
		me.Table.clearData();
		me.showContent();
		bottomToolbar.show();
	}
});