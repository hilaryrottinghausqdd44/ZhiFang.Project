/**
 * 项目字典对照
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.contrast.App',{
    extend:'Shell.ux.panel.AppPanel',
    requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
    padding:'0 0 1 0',
    title:'项目字典对照',
    hasBtntoolbar:true,
    width:960,
    height:430,
    /**项目编号*/
	ItemNo:null,
    /**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchGroupItemSubItemByPItemNo?isPlanish=true',
    RecDatas:[],
    hideTimes:2000,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.Btn.on({
			click:function(){//对照
				//中心项目
				var recItem = me.TestItemGrid.getSelection();//获取修改过的行记录
				//实验室项目
				var rec = me.Grid.getSelection();//获取修改过的行记录
				if(!recItem || !rec){
					JShell.Msg.error('请选择要进行对照的记录');
					return;
				}
				var bottomToolbar = me.getComponent('buttonsToolbar');
	            var ClienteleId = bottomToolbar.getComponent('ClienteleId').getValue();
				if(!ClienteleId){
					JShell.Msg.error('实验室不能为空！');
					return;
				}
				me.Btn.onSaveUpdate(recItem[0],rec[0],ClienteleId);
			},
			cancelClick:function(){//取消对照
				//实验室项目
				var rec = me.Grid.getSelection();//获取修改过的行记录
				if(!rec){
					JShell.Msg.error('请选择要取消的对照行记录');
					return;
				}
				if(rec[0] && rec[0]!='undefined'){
					var TestItemId= rec[0].get('BTestItemControlVO_TestItem_Id');
					var TestItemCName= rec[0].get('BTestItemControlVO_TestItem_CName');
			        //如果实验室项目id和项目名称是空的，id无效
			        if(!TestItemId && !TestItemCName ){
			        	JShell.Msg.error('选择行数据未对照,不能取消对照');
			        	return;
			        }
			        me.Btn.onClearClick(rec[0]);
				}else{
					JShell.Msg.error('请选择要取消的对照行记录');
					return;
				}
			},
			intelligenceClick:function(){//智能对照
				//实验室
				var dictList=me.Grid.getItemList();
				//中心项目
				var itemList=me.TestItemGrid.getItemList();
				me.openIntelligenceForm(dictList,itemList);
			},
			save:function(){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				me.Grid.loadDatas();
				
			}
		});
		me.Grid.on({
			itemclick:function(v, record) {
				me.onSelect(record);
			},
			select:function(RowModel, record){
				me.onSelect(record);
			}
		});
		
		me.TestItemGrid.on({
			changeData:function(grid){
				var record=me.Grid.myGridPanel.getSelectionModel().getSelection();
				if(record){
					me.onSelect(record[0]);
				}
			}
		});
	},
   
	initComponent:function(){
		var me = this;
		me.addEvents('save','onAcceptClick');
		 //创建挂靠功能栏
	 	var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0){
			me.dockedItems = dockedItems;
		}
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		me.Grid = Ext.create('Shell.class.weixin.dict.lab.dictitem.contrast.Grid', {
			region: 'center',
			header: false,
			itemId: 'Grid'
		});
		
		me.Btn = Ext.create('Shell.class.weixin.dict.lab.dictitem.contrast.BtnPanel', {
		    region: 'east',
			width:100,
			split: false,
			border:false,
			collapsible: false,
			itemId: 'Btn'
		});
		
		me.TestItemGrid = Ext.create('Shell.class.weixin.dict.lab.dictitem.contrast.TestItemGrid', {
			split: false,
			region: 'east',
			width:500,
			header:false,
			itemId: 'TestItemGrid'
		});
		
		return [me.Grid,me.Btn,me.TestItemGrid];
	},
	 /**
	 * 选中数据处理
	 * @private
	 * @param {} callback 数据处理
	 */
	checkedRecords:function(){
		var me=this;
		var recs = me.GroupPanel.getRecs();
		if(!recs) return;
        var len=recs.length;
        for(var i =0 ;i<len;i++){
        	var ItemNo=recs[i].get('ItemAllItem_Id');
        	me.onSetGroupItemData(ItemNo,recs);
        }
	},
	/**调用服务拆分到已选列表*/
	onSetGroupItemData:function(ItemNo,recs){
		var me=this;
		me.getLabTestItem(ItemNo,function(data){
			if(data.value){
				var list = data.value.list;
				for(var i=0;i<list.length;i++){
					var Id=list[i].GroupItemVO_TestItem_Id;
					var index = me.GroupItemGrid.store.findExact('GroupItemVO_TestItem_Id',Id);
			        if(index==-1){
						var obj={
				        	GroupItemVO_TestItem_Id:list[i].GroupItemVO_TestItem_Id,
				        	GroupItemVO_TestItem_CName:list[i].GroupItemVO_TestItem_CName,
				            GroupItemVO_TestItem_MarketPrice:list[i].GroupItemVO_TestItem_MarketPrice,
				            GroupItemVO_TestItem_GreatMasterPrice:list[i].GroupItemVO_TestItem_GreatMasterPrice,
				            GroupItemVO_TestItem_Price:list[i].GroupItemVO_TestItem_Price,
				            GroupItemVO_TestItem_Tag:'1'
				        };
			        	 me.GroupItemGrid.store.insert(me.GroupItemGrid.getStore().getCount(),obj);            
			        }
				}
			}else{
				 for(var i =0;i<recs.length;i++) {
				 	var Id=recs[i].data.ItemAllItem_Id;
					var index = me.GroupItemGrid.store.findExact('GroupItemVO_TestItem_Id',Id);
			        if(index==-1){
			        	var obj={
				        	GroupItemVO_TestItem_Id:recs[i].data.ItemAllItem_Id,
				        	GroupItemVO_TestItem_CName:recs[i].data.ItemAllItem_CName,
				            GroupItemVO_TestItem_MarketPrice:recs[i].data.ItemAllItem_MarketPrice,
				            GroupItemVO_TestItem_GreatMasterPrice:recs[i].data.ItemAllItem_GreatMasterPrice,
				            GroupItemVO_TestItem_Price:recs[i].data.ItemAllItem_Price,
				            GroupItemVO_TestItem_Tag:'1'
				        };
				        me.GroupItemGrid.store.insert(me.GroupItemGrid.getStore().getCount(),obj);            
			        }
		       }
			}
		});
	},
	/**获取已selectUrl*/
	getLabTestItem:function(itemNo,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl;
		var fields='GroupItemVO_TestItem_Id,GroupItemVO_TestItem_CName,GroupItemVO_TestItem_MarketPrice,GroupItemVO_TestItem_GreatMasterPrice,GroupItemVO_TestItem_Price';
		url += '&fields='+ fields +'&pitemNo='+itemNo;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
    /**显示遮罩*/
	showMask: function(text) {
		var me = this;
		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.body) {
			me.body.unmask();
		} //隐藏遮罩层
	},
    onSaveClick:function(){
    	var me = this;
    	//删除
    	me.GroupItemGrid.onDelSave();
	    var recs = me.GroupPanel.getRecs();
		if(!recs) return;
		
		//新增
		me.GroupItemGrid.onSaveClick();
    },
    onAcceptClick:function(){
    	var me =this;
    	var recs=me.GroupItemGrid.getAllRecDatas();
    	me.fireEvent('onAcceptClick',me,recs);
    },
    /**@description 创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];
		items.push({
	        xtype: 'label',
	        text: '实验室-中心对照表',
	        itemId:'labMarketPrice',
	        style: "font-weight:bold;color:#0000EE;"
		},{fieldLabel:'实验室ID',hidden:true,xtype:'textfield',
				name:'ClienteleId',itemId:'ClienteleId'
	    },'-',{
			fieldLabel: '',xtype: 'uxCheckTrigger',emptyText: '实验室',
			width:200,labelSeparator:'',
			labelWidth: 55,
			labelAlign: 'right',
			name: 'ClienteleName',itemId: 'ClienteleName',
			className: 'Shell.class.weixin.dict.lab.dictitem.CheckGrid',
			listeners: {
				check: function(p, record) {
				    me.onCheckClick(p, record);
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			height:25,
			border: false,
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this,
			items = me.dockedItems || [];
		var buttontoolbar = me.createButtonToolbarItems();
		if(buttontoolbar) items.push(buttontoolbar);
		return items;
	},
	/**选实验室联动项目对照关系表*/
	onCheckClick:function(p,record){
		var me =this;
		var bottomToolbar = me.getComponent('buttonsToolbar');
	    var ClienteleId = bottomToolbar.getComponent('ClienteleId');
		var ClienteleName = bottomToolbar.getComponent('ClienteleName');
        var id=record ? record.get('CLIENTELE_Id') : '';
		ClienteleId.setValue(id);
		ClienteleName.setValue(record ? record.get('CLIENTELE_CNAME') : '');
		if(id){
			me.Btn.enableControl(true);
			me.Grid.enableControl(true);
			me.Grid.ClienteleID=id;
			me.Grid.loadDatas();
		}else{
			me.Grid.clearData();
			me.Grid.hideMask();
			me.Grid.enableControl(false);
			me.Btn.enableControl(false);
		}
		p.close();
	},
	/**打开智能对照*/
	openIntelligenceForm:function(dictList,ItemList){
		var me = this;
		var bottomToolbar = me.getComponent('buttonsToolbar');
	    var ClienteleId = bottomToolbar.getComponent('ClienteleId');
		JShell.Win.open('Shell.class.weixin.dict.lab.dictitem.contrast.IntelligenceGrid', {
			SUB_WIN_NO:'41',//内部窗口编号
			resizable: false,
			formtype:'edit',
			//实验室项目
			dictList:dictList,
			//中心项目
			ItemList:ItemList,
			ClienteleId:ClienteleId.getValue(),
			listeners: {
				save: function(p) {
					me.Grid.loadDatas();
					p.close();
				}
			}
		}).show();
	},
	//选中处理
	onSelect:function(record){
		var me = this;
		JShell.Action.delay(function(){
			if(record && record!='undefined'){
				var ItemNo=record.get('BTestItemControlVO_TestItem_Id');
				me.TestItemGrid.myGridPanel.getSelectionModel().deselectAll();
                var	records = me.TestItemGrid.myGridPanel.store.data.items;
                var len=records.length;
                for(var i =0 ;i< len;i++){
                	if(records[i].get('addClss')=='1'){
                		records[i].set('addClss','0');
                		break;
                	}
                }
				var rec = me.TestItemGrid.myGridPanel.store.findRecord('ItemAllItem_Id',ItemNo);
				if(rec){
					rec.set('addClss','1');
				}
			}
		},null,200);
	}
});