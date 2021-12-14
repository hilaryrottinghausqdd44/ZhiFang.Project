/**
 * 追加检验项目
 * @author zhangda
 * @version 2020-04-17
 * @desc Jcall 2020-09-10修改代码部分内容
 */
Ext.define('Shell.class.lts.sample.result.sample.add.App', {
	extend:'Shell.ux.panel.AppPanel',
	requires:['Shell.ux.toolbar.Button'],
	title:'追加项目',
	width:1200,
	height:630,
	
	saveUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestItem',
	
	//小组ID
	sectionId:null,
	//检验单ID
	testFormId:null,

	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//监听已选择项目列表
		me.Grid.on({
			afterLoad:function(storeData){
				me.ItemGrid.existingData = storeData;
				me.ItemGrid.onSearch();
			},
			//双击-删除
			itemdblclick:function(){
				me.onDelOneClick();
			}
		});
		//监听待选择项目列表
		me.ItemGrid.on({
			select:function(model,record){
				var GroupType = record.get("LBSectionItem_LBItem_GroupType");
				if(GroupType == 0){//如果不是组合项目，子项列表由显示改为隐藏
					if(!me.SubitemGrid.hidden){
						me.SubitemGrid.hide();
					}
				}else{//如果是组合项目，子项列表由隐藏改为显示，查询该组合项目下小所有子项
					if(me.SubitemGrid.hidden){
						me.SubitemGrid.show();
					}
					me.SubitemGrid.onSearch(record.get("LBSectionItem_LBItem_Id"));
				}
			},
			//双击-新增
			itemdblclick: function (model, record) {
				var GroupType = record.get("LBSectionItem_LBItem_GroupType");
				if (GroupType == 0) {
					me.onAddOneClick();
				} else if (GroupType == 1) {
					JcallShell.Action.delay(function () {
						me.onAddOneClick();
					}, this, 200);
				}
			}
		});
		//监听按钮
		me.Button.on({
			addOne:function(p){me.onAddOneClick();},
			delOne:function(p){me.onDelOneClick();},
			delAll:function(p){me.onDelAllClick();},
			reset:function(p){me.onResetClick();},
			save:function(p){me.onSaveClick();}
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
	createDockedItems:function (){
		var me = this;
		
		var dockedItems = [{
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'bottomToolbar',
			items:[
				{xtype:'tbspacer',width:10},
				{xtype:'label',text:'提示:',style:'color:red;font-weight:bold;'},
				{xtype:'tbspacer',width:10},
				{xtype:'button',overCls:'',height:22,text:'<span style="color:#000;">在用</span>',tooltip:'在用',style:'background-color:#ADE3F7;'},
				{xtype:'button',overCls:'',height:22,text:'<span style="color:#000;">新增</span>',tooltip:'新增',style:'background-color:#BDFFDE;'},
				{xtype:'tbspacer',width:10},
				{xtype:'button',overCls:'',height:22,text:'<span style="color:#000;">组合项目</span>',tooltip:'组合项目',style:'background-color:#FFC3A5;'},
				{xtype:'button',overCls:'',height:22,text:'<span style="color:#000;">医嘱项目</span>',tooltip:'医嘱项目',style:'background-color:#7CE9BE;'}
			]
		}];
		
		return dockedItems;
	},
	//创建内部组件
	createItems: function () {
		var me = this;
		me.Grid = Ext.create('Shell.class.lts.sample.result.sample.add.Grid',{
			region:'west',itemId:'Grid',width:555,header:false,
			split:true,collapsible:false,animCollapse:false,
			TestFormId:me.testFormId
		});
		me.Button = Ext.create('Shell.class.lts.sample.result.sample.add.Button',{
			region:'west',itemId:'Button',header:false,
			split:true,collapsible:false,animCollapse:false
		});
		me.ItemGrid = Ext.create('Shell.class.lts.sample.result.sample.add.ItemGrid',{
			region:'center',itemId:'ItemGrid',header:false,
			split:true,collapsible:false,animCollapse:false,
			sectionId:me.sectionId
		});
		me.SubitemGrid = Ext.create('Shell.class.lts.sample.result.sample.add.SubitemGrid',{
			region:'east',itemId:'SubitemGrid',header:false,
			split:true,collapsible:false,animCollapse:false,
			hidden:true
		});
		me.Content = Ext.create('Ext.panel.Panel',{
			region:'center',itemId:'Content',width:540,header:false,
			split:true,collapsible:false,animCollapse:false,
			layout:'border',bodyPadding:1,
			items:[me.ItemGrid,me.SubitemGrid]
		});
		
		return [me.Grid,me.Button,me.Content];
	},
	
	//新增选中行数据 -- 选中的是组合项目 则将所有子项都加入 需要去重
	onAddOneClick:function(){
		var me = this;
		var record = me.Content.getComponent("ItemGrid").getSelectionModel().getSelection();//选中数据
		var LBItemIdField = "";//选过来的项目id字段
		var storeData = me.Grid.allData;//已加入的全部数据
		var data = [];//加入的数据
		var addStoreData = [];
		if (record.length <= 0) {
			JShell.Msg.alert("未选中数据!")
			return;
		}
		var GroupType = record[0].get("LBSectionItem_LBItem_GroupType");//组合类型
		if (GroupType == 0) {//单项
			data = record;
			LBItemIdField = "LBSectionItem_";
		} else if (GroupType == 1) {//组合
			data = me.Content.getComponent("SubitemGrid").getStore().data.items;
			LBItemIdField = "LBItemGroup_";
		}
		//去重 -- 如果是组合里有子项已经存在 那存在的子项是单项情况则需要替换为该组合下的这个子项 如果已经是组合下的项目 则不用加入
		Ext.Array.each(data, function (str1, index1, array1) { //遍历数组
			var flag = true;//是否加入到新增数据中
			Ext.Array.each(storeData, function (str2, index2, array2) {//遍历数组
				if (str1.get(LBItemIdField +"LBItem_Id") == str2.LisTestItem_LBItem_Id) {//存在
					flag = false;
					if (GroupType == 1 && str2.LisTestItem_LBItem_Id == str2.LisTestItem_LBItem_Id){//加入项目是属于组合 已有项目是单项 则加入更新数据中
						if (str2.LisTestItem_Id) {
							//需要后台更新 记录
							str2.LisTestItem_Tab = 'update';
						}
						//更新表中数据
						str2.LisTestItem_PLBItem_Id = record[0].get("LBSectionItem_LBItem_Id");
						str2.LisTestItem_PLBItem_CName = record[0].get("LBSectionItem_LBItem_CName");
						str2.LisTestItem_PLBItem_SName = record[0].get("LBSectionItem_LBItem_SName");
						str2.LisTestItem_PLBItem_Shortcode = record[0].get("LBSectionItem_LBItem_Shortcode");
						str2.LisTestItem_PLBItem_PinYinZiTou = record[0].get("LBSectionItem_LBItem_PinYinZiTou");
					} else {
						return false;
					}
				}
				return true;
			});
			//是否新增
			if (flag) {
				addStoreData.push({
					LisTestItem_Id:"",
					LisTestItem_PLBItem_Id: GroupType == 0 ? "" : record[0].get("LBSectionItem_LBItem_Id"),
					LisTestItem_PLBItem_CName: GroupType == 0 ? "" : record[0].get("LBSectionItem_LBItem_CName"),
					LisTestItem_PLBItem_SName: GroupType == 0 ? "" : record[0].get("LBSectionItem_LBItem_SName"),
					LisTestItem_PLBItem_Shortcode: GroupType == 0 ? "" : record[0].get("LBSectionItem_LBItem_Shortcode"),
					LisTestItem_PLBItem_PinYinZiTou: GroupType == 0 ? "" : record[0].get("LBSectionItem_LBItem_PinYinZiTou"),
					LisTestItem_LBItem_Id: str1.get(LBItemIdField + "LBItem_Id"),
					LisTestItem_LBItem_CName: str1.get(LBItemIdField + "LBItem_CName"),
					LisTestItem_LBItem_SName: str1.get(LBItemIdField + "LBItem_SName"),
					LisTestItem_LBItem_Shortcode: str1.get(LBItemIdField + "LBItem_Shortcode"),
					LisTestItem_LBItem_PinYinZiTou: str1.get(LBItemIdField + "LBItem_PinYinZiTou"),
					LisTestItem_LBItem_IsOrderItem: str1.get(LBItemIdField + "LBItem_IsOrderItem"),
					LisTestItem_DataAddTime: '',
					LisTestItem_Tab:'add'
				});
				//更新待加入列表在用状态
				if (me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", str1.get(LBItemIdField + "LBItem_Id"))) me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", str1.get(LBItemIdField + "LBItem_Id")).set("LBSectionItem_DataAddTime", "新增");
				
				if (GroupType == 1 && index1 == 0) {//更新组合项目
					if (me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LBSectionItem_LBItem_Id")).get("LBSectionItem_DataAddTime") != null && me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LBSectionItem_LBItem_Id")).get("LBSectionItem_DataAddTime") != "在用") {
						me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LBSectionItem_LBItem_Id")).set("LBSectionItem_DataAddTime", "新增");
					}
				}
			}
			return true;
		});
		//加入到列表中
		//me.Grid.store.add(addStoreData);
		me.Grid.allData = Ext.Array.merge(storeData, addStoreData);
		var val = me.Grid.getComponent("buttonsToolbar").getComponent("selectItem").getValue();
		me.Grid.onSearchStore(val);

	},
	//删除选中行数据
	onDelOneClick: function () {
		var me = this;
		var record = me.Grid.getSelectionModel().getSelection();//选中数据
		if (record.length <= 0) {
			JShell.Msg.alert("未选中数据!");
			return;
		}
		if (record[0].get("LisTestItem_Id")) {//在用数据
			JShell.Msg.warning("在用的检验项目,请在检验界面删除!");
			return;
		} else {
			var index;//要删除数据的下标
			//更新待加入列表在用状态
			if (me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_LBItem_Id"))) me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_LBItem_Id")).set("LBSectionItem_DataAddTime", "");
			if (record[0].get("LisTestItem_PLBItem_Id") != "" && record[0].get("LisTestItem_PLBItem_Id") != record[0].get("LisTestItem_LBItem_Id")) {//组合
				var flag = true;
				Ext.Array.each(me.Grid.allData, function (str, i, array) {
					//获得删除数据的下标
					if (record[0].get("LisTestItem_LBItem_Id") == str.LisTestItem_LBItem_Id) {
						index = i;
					}
					if (record[0].get("LisTestItem_LBItem_Id") == str.LisTestItem_LBItem_Id && record[0].get("LisTestItem_PLBItem_Id") == str.LisTestItem_PLBItem_Id) {
						flag = false;
						return false;
					}
					return true;
				});
				if (flag) {
					if (me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_PLBItem_Id"))) me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_PLBItem_Id")).set("LBSectionItem_DataAddTime", "");
				}
			} else {
				Ext.Array.each(me.Grid.allData, function (str, i, array) {
					//获得删除数据的下标
					if (record[0].get("LisTestItem_LBItem_Id") == str.LisTestItem_LBItem_Id) {
						index = i;
					}
					return true;
				});
			}
			Ext.Array.splice(me.Grid.allData, index, 1);
			var val = me.Grid.getComponent("buttonsToolbar").getComponent("selectItem").getValue();
			me.Grid.onSearchStore(val);

		}
	},
	//删除选中行该组合项目下所有子项数据
	onDelAllClick: function () {
		var me = this;
		var record = me.Grid.getSelectionModel().getSelection();//选中数据
		if (record.length <= 0) {
			JShell.Msg.alert("未选中数据!");
			return;
		}
		if (record[0].get("LisTestItem_Id")) {//在用数据
			JShell.Msg.warning("在用的检验项目,请在检验界面删除!");
			return;
		} else {
			var delIndexArr = [];
			if (record[0].get("LisTestItem_PLBItem_Id") == "" || record[0].get("LisTestItem_PLBItem_Id") == record[0].get("LisTestItem_LBItem_Id")) {//单项
				Ext.Array.each(me.Grid.allData, function (str, i, array) {
					//获得删除数据的下标
					if (record[0].get("LisTestItem_LBItem_Id") == str.LisTestItem_LBItem_Id) {
						delIndexArr.push(i);
						return false;
					}
					return true;
				});
				//更新待加入列表在用状态
				if (me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_LBItem_Id"))) me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_LBItem_Id")).set("LBSectionItem_DataAddTime", "");
			} else {//组合项目
				var arr = [];
				Ext.Array.each(me.Grid.allData, function (str,index,array) {
					if (str.LisTestItem_Id == "" && str.LisTestItem_PLBItem_Id == record[0].get("LisTestItem_PLBItem_Id")) {
						arr.push(str);
						delIndexArr.push(index);//获得删除数据的下标
						//更新待加入列表在用状态 -- 组合中的子项
						if (me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", str.LisTestItem_LBItem_Id)) me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", str.LisTestItem_LBItem_Id).set("LBSectionItem_DataAddTime", "");
					}
					return true;
				});
				//更新待加入列表在用状态 -- 组合项目
				if (me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_PLBItem_Id"))) me.Content.getComponent("ItemGrid").store.findRecord("LBSectionItem_LBItem_Id", record[0].get("LisTestItem_PLBItem_Id")).set("LBSectionItem_DataAddTime", "");
				me.Grid.allData = Ext.Array.remove(me.Grid.allData,arr);
			}
			//删除
			for (var j = delIndexArr.length - 1; j >= 0;j--) {
				Ext.Array.splice(me.Grid.allData, delIndexArr[j], 1);
			}
			var val = me.Grid.getComponent("buttonsToolbar").getComponent("selectItem").getValue();
			me.Grid.onSearchStore(val);
		}
	},
	//重置
	onResetClick: function () {
		var me = this;
		me.Grid.getComponent("buttonsToolbar").getComponent("selectItem").setValue("");
		me.Grid.onSearch();
	},
	//保存
	onSaveClick: function () {
		var me = this,
			testFormId = me.testFormId,
			addUpdateList = [];//新增和修改数据都放
		if (!testFormId) {
			JShell.Msg.alert("检验单ID不存在!");
			return;
		}
		Ext.Array.each(me.Grid.allData, function (str, i, arr) {
			if (str.LisTestItem_Tab && (str.LisTestItem_Tab == "add" || str.LisTestItem_Tab == "update")) {
				var obj = {};
				if (str.LisTestItem_PLBItem_Id) obj["PLBItem"] = { Id: str.LisTestItem_PLBItem_Id, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
				if (str.LisTestItem_LBItem_Id) obj["LBItem"] = { Id: str.LisTestItem_LBItem_Id, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
				addUpdateList.push(obj);
			}
			return true;
		});
		if (addUpdateList.length == 0) {
			JShell.Msg.alert("没有需要保存的数据!");
			return;
		} else {
			var url = JShell.System.Path.ROOT + me.saveUrl;
			JShell.Server.post(url, Ext.JSON.encode({ testFormID: testFormId, listAddTestItem: addUpdateList, isRepPItem: true }), function (res) {
				if (res.success) {
					me.fireEvent('save', me);
				} else {
					JShell.Msg.error("保存失败，失败信息：" + res.msg);
					return;
				}
			});
		}
	}
});