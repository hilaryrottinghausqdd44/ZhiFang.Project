/**
 * 样本检验
 * @author Jcall
 * @version 2019-11-05
 * @author liangyl 分页后定位修改 onAddClick
 */
Ext.define('Shell.class.lts.sample.App',{
	extend:'Shell.ux.panel.AppPanel',
	requires:['Shell.ux.toolbar.Button'],
	title:'样本检验',
	//小组ID
	sectionId:null,
	//选中的行数据record
	checkedRecord:null,
	//是否只查看
	isReadOnly: false,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//检验单列表监听
		me.Grid.on({
			select: function (model, record) {
				JcallShell.Action.delay(function () {
					me.checkedRecord = record;
					var testFormId = record.get(me.Grid.PKField);
					if (testFormId) {
						//状态
						var StatusID = record.get("LisTestForm_MainStatusID");
						//更新检验单数据显示内容
						me.Info.curSampleNo = record.get('LisTestForm_GSampleNo');
						if (me.isReadOnly || StatusID != 0) {
							me.Info.isShow(testFormId);
						} else {
							me.Info.isEdit(testFormId);
						}
						//更新检验结果相关数据显示内容
						me.ResultTab.onSearch(record);
						//不是自动新增 则显示当前样本号
						if (!me.Grid.getComponent("BarcodeToolbar").getComponent("cbSelect").checked)
							me.Grid.getComponent("BarcodeToolbar").getComponent("SampleNum").setValue(record.get('LisTestForm_GSampleNo'));
					} else {
						JShell.Msg.error("该条检验单没有主键！");
					}
				},this,300);
			},
			nodata:function(p){
				//选中的行数据
				me.checkedRecord = null;
				//样本信息置空
				me.Info.clearData();
				//结果信息置空
				me.ResultTab.clearData();
			},
			AllGroupOnBeforeLoad: function (p) {
				me.fireEvent('AllGroupOnBeforeLoad', p);
			}
		});
		//检验单列表数据变化监听
		me.Grid.store.on({
			//加载检验单列表后更新样本总数显示
			load:function(store,records,successful){
				me.updateSampleTotal(store.totalCount);
			}
		});
		//检验信息监听
		me.Info.on({
			add:function(p,id){
				//新增后定位到该条数据
				me.Grid.autoSelect = id;
				me.Grid.onSearch();
			},
			edit:function(p,id){
				//修改后列表中该数据自动更新
				me.Grid.onChangeRecodeDataById(id,function(){
					//保存并新增状态
					if(me.SaveAndAddClick){
						me.onAddClick();
						me.SaveAndAddClick = false;
					}
				});
			},
			load: function (p, data) {
				//保存并新增状态
				if (me.SaveAndAddClick) {
					me.onAddClick();
					me.SaveAndAddClick = false;
				}
			}
		});
		//检验项目监听
		me.ResultTab.Result.on({
			//保存检验单及检验项目
			save: function (p) {
				me.onSaveClick();
			},
			//加载项目列表后更新项目总数显示
			afterLoad: function (p, list) {
				me.updateItemTotal(list.length);
				if(me.ShowAddItemPanel){
					me.ShowAddItemPanel = false;
					p.onAddClick();
				}
			},
			//新增项目时不存在检验单信息，先保存检验单信息，然后自动弹出新增项目页面
			noforminfo:function(p){
				me.ShowAddItemPanel = true;
				me.Info.onSaveClick();
			}
		});
		
		//初始化当前检验者+审核人信息
		me.onConfigChange();
	},
	initComponent:function(){
		var me = this;
		//操作数据
		me.OperateData = Ext.create('Shell.class.basic.data.Operate');
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	//创建挂靠功能栏
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];

		items.push(Ext.create('Shell.class.lts.sample.TopToolbar',{
			dock:'top',
			itemId:'topToolbar',
			sectionId:me.sectionId,
			items:items,
			listeners:{
				refresh:function(){me.Grid.onRefreshClick();},
				addclick:function(){me.onAddClick()},
				saveclick:function(){me.onSaveClick();},
				saveandaddclick:function(){me.onSaveAndAddClick();},
				afterconfirmconfig:function(){me.onConfigChange();},
				aftercheckconfig: function () { me.onConfigChange(); },
				GridSearch: function () { me.Grid.onSearch(); },//刷新样本单列表
				loadRecord: function () {//刷新选中样本单
					var testFormId = me.checkedRecord.get(me.Grid.PKField);
					me.Grid.onChangeRecodeDataById(testFormId, function () {});
				},
				loadResult: function () {//刷新结果等页签内容
					me.ResultTab.onSearch(me.checkedRecord);
				}
			}
		}),{
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'bottomToolbar',
			//默认组件
			defaultType:'displayfield',
			//每个组件的默认属性
			defaults:{ 
				fieldStyle:'color:rgb(4,64,140);font-weight:bold;',
				style:'color:rgb(4,64,140);fontWeight:bold;'
			},
			items:[{
				fieldLabel:'当前检验者',itemId:'UserName',labelWidth:70,width:190,value:''
		    },{
				fieldLabel:'当前审核者',itemId:'CheckName',labelWidth:70,width:190,value:''
		    },{
				fieldLabel:'样本总数',itemId:'SampleTotal',labelWidth:60,width:140,value:'0'
		    },{
				fieldLabel:'项目总数',itemId:'ItemTotal',labelWidth:60,width:140,value:'0'
		    },{
				fieldLabel:'历史例数',itemId:'HistoryTotal',labelWidth:60,width:140,value:'0'
//		    },{
//				fieldLabel:'',itemId:'Hospital'labelWidth:0,width:120,value:'总院'
		    }]
		});
		return items;
	},
	//创建内部组件
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.lts.sample.Grid2', {
			region:'west',
			//width:300,
			itemId:'Grid',
			header:false,
			split:true,
			collapsible:true,
			sectionId:me.sectionId,
			isReadOnly:me.isReadOnly
		});
		me.Info = Ext.create('Shell.class.lts.sample.Info', {
			region:'west',
			//width:200,
			itemId:'Info',
			header:true,
			split:true,
			collapsible:true,
			sectionId:me.sectionId,
			isReadOnly:me.isReadOnly
		});
		me.ResultTab = Ext.create('Shell.class.lts.sample.result.Tab', {
			region:'center',
			itemId:'ResultTab',
			header:false,
			sectionId:me.sectionId,
			isReadOnly:me.isReadOnly
		});
		
		return [me.Grid,me.Info,me.ResultTab];
	},
	//新增
	onAddClick:function(){
		var me = this,
		    MaxRecord = null;//当天最大样本号数据
		//检验单信息+结果信息置空
		me.Info.clearData();
		me.ResultTab.clearData();
		
		//存在检验单选中行数据时，如果选中行是当天的样本号，则将该样本号记下，用于样本新增自动计算参数，
		//如果不是当天的样本号，则定位到当天的最大样本数据行上，同时将该样本号记下，用于后续计算
		//如果不存在选中行，则样本号置空，后续样本新增自动计算
		if(me.checkedRecord){
			var NowDate = JShell.Date.toString(JShell.System.Date.getDate(),true),
				GTestDate = JShell.Date.toString(me.checkedRecord.get('LisTestForm_GTestDate'),true);
			if(GTestDate == NowDate){
				me.Info.curSampleNo = me.checkedRecord.get('LisTestForm_GSampleNo');
			}else{
				var records = me.Grid.store.getRange();
//					MaxRecord = null;//当天最大样本号数据
					
				for(var i in records){
					var LisTestForm_GTestDate = JShell.Date.toString(records[i].get('LisTestForm_GTestDate'),true);
					if(LisTestForm_GTestDate == NowDate){
						if(MaxRecord){
							var GSampleNo = records[i].get('LisTestForm_GSampleNoForOrder'),
								MaxSampleNo = MaxRecord.get('LisTestForm_GSampleNoForOrder');
								
							if(GSampleNo > MaxSampleNo){
								MaxRecord = records[i];
							}
						}else{
							MaxRecord = records[i];
						}
					}
				}
				if(MaxRecord){
					me.Grid.getSelectionModel().select(MaxRecord,false,true);
					me.Info.curSampleNo = MaxRecord.get('LisTestForm_GSampleNo');
				}
			}
		}else{
			me.Info.curSampleNo = null;
		}
		//存在分页，需要重新加载
		if(me.Grid.store.totalCount>me.Grid.defaultPageSize){
			if(MaxRecord)me.Grid.autoSelect = MaxRecord.data.LisTestForm_Id;
			me.Grid.onSearch();
		}
		me.Info.isAdd();
		me.ResultTab.Result.isAdd();
	},
	//保存按钮
	onSaveClick:function(){
		var me = this;
		//检验项目保存
		me.ResultTab.Result.onSaveClick();
		//检验结果保存
		me.Info.onSaveClick();
	},
	//保存并新增按钮
	onSaveAndAddClick:function(){
		var me = this;
		me.SaveAndAddClick = true;
		//检验项目保存
		me.ResultTab.Result.onSaveClick();
		//检验结果保存
		me.Info.onSaveClick();
	},
	//设置修改后处理
	onConfigChange:function(){
		var me = this;
		me.updateUserName(me.OperateData.getHandlerInfo().Name);
		me.updateCheckName(me.OperateData.getCheckerInfo().Name);
		
	},
	//更改显示-当前检验者
	updateUserName:function(value){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar'),
			UserName = bottomToolbar.getComponent('UserName');
			
		UserName.setValue(value);
	},
	//更改显示-当前审核者
	updateCheckName:function(value){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar'),
			CheckName = bottomToolbar.getComponent('CheckName');
			
		CheckName.setValue(value);
	},
	//更改显示-样本总数
	updateSampleTotal:function(value){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar'),
			SampleTotal = bottomToolbar.getComponent('SampleTotal');
			
		SampleTotal.setValue(value);
	},
	//更改显示-项目总数
	updateItemTotal:function(value){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar'),
			ItemTotal = bottomToolbar.getComponent('ItemTotal');
			
		ItemTotal.setValue(value);
	},
	//更改显示-历史例数
	updateHistoryTotal:function(value){
		var me = this,
			bottomToolbar = me.getComponent('bottomToolbar'),
			HistoryTotal = bottomToolbar.getComponent('HistoryTotal');
			
		HistoryTotal.setValue(value);
	},
	//键盘监听事件
	onKeyDown: function (tab) {
		var me = this,
			body = Ext.getBody(),
			iTime = null,//定时器
			topToolbar = tab.getComponent("topToolbar");
		body.on('keydown', function (evt, el) {
			var keyCode = evt.keyCode;
			switch (keyCode) {
				case 112://F1 新增
					Ext.EventObject.preventDefault();
					clearTimeout(iTime);
					iTime = setTimeout(function () {
						if (topToolbar.getComponent('add') && topToolbar.getComponent('add').getEl()) topToolbar.getComponent('add').getEl().dom.click();
					}, 500);
					break;
				case 113://F2 保存
					Ext.EventObject.preventDefault();
					clearTimeout(iTime);
					iTime = setTimeout(function () {
						if (topToolbar.getComponent('save') && topToolbar.getComponent('save').getEl()) topToolbar.getComponent('save').getEl().dom.click();
					}, 500);
					break;
				case 114://F3 保存并新增
					Ext.EventObject.preventDefault();
					clearTimeout(iTime);
					iTime = setTimeout(function () {
						if (topToolbar.getComponent('saveAndadd') && topToolbar.getComponent('saveAndadd').getEl()) topToolbar.getComponent('saveAndadd').getEl().dom.click();
					}, 500);
					break;
			}
		});
	},
	//取消键盘监听事件
	removeKeyDown: function () {
		var me = this,
			body = Ext.getBody();
		body.removeListener('keydown');
	}
});