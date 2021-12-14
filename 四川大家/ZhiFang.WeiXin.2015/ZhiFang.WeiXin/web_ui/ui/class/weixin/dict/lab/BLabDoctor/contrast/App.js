Ext.define("Shell.class.weixin.dict.lab.BLabDoctor.contrast.App",{
	extend:"Shell.ux.panel.AppPanel",
	
	requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
	
	initComponent:function(){
		var me =this;
		me.items=me.createPitems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	
	afterRender:function(){
		var me =this;
		me.callParent(arguments);
		me.LGrid.on({
			itemclick:function(v, record){
				var sampleTypeList = me.RGrid.store.data.items;
				var sampleTypeId=record.data.BLabDoctor_doctorId;
				
				for(var i =0 ; i< sampleTypeList.length;i++){
					if(sampleTypeId==sampleTypeList[i].data.Doctor_Id){
						me.RGrid.getSelectionModel().select(i);
						break;
					}
				}
			}
		});
		
		me.CBtn.on({
			click:function(){
				var controlrow=me.RGrid.getSelectionModel().getSelection();
				var row=me.LGrid.getSelectionModel().getSelection();
				if(!controlrow || !row){
					JShell.Msg.error('请选择要进行对照的记录');
					return;
				}
				var bottonbar=me.getComponent('buttonsToolbar');
				var ClienteleId = bottonbar.getComponent('ClienteleId').getValue();
				if(!ClienteleId){
					JShell.Msg.error('实验室不能为空！');
					return;
				}
				me.CBtn.onSaveAdd(controlrow[0],row[0],ClienteleId);	
			},
			cancelClick:function(){
				var row=me.LGrid.getSelectionModel().getSelection();
				if(!row){
					JShell.Msg.error('请选择要取消对照的记录');
					return;
				}
				if(row[0] && row[0]!=='undefined'){
					if(row[0].get('BLabDoctor_isContrast')==''){
						JShell.Msg.error('该记录未对照，不能取消对照');
						return;
					}
					me.CBtn.onClearClick(row[0]);
				}else{
					JShell.Msg.error('请选择要取消对照的记录');
					return;
				}
			},
			
			save:function(){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				me.LGrid.store.reload();
			},
			intelligenceClick:function(){
				if(!me.LGrid.labCode){
					JShell.Msg.error('实验室为空');
					return;
				}
				var dictList=me.LGrid.getLabDoctor();
				var itemList=me.RGrid.getDoctor();
				if(!dictList){
					JShell.Msg.error('没有可对照项');
					return ;
				}
				me.openIntelligenceForm(dictList,itemList);
			}
		});
		
	},
	
	createPitems:function(){
		var me =this;
		me.LGrid=Ext.create("Shell.class.weixin.dict.lab.BLabDoctor.contrast.Grid",{
			region:'center',
		});
		me.CBtn=Ext.create("Shell.class.weixin.dict.lab.BLabDoctor.contrast.Btn",{
			region:'east',
			width:100,
			split: false,
			border:false,
			collapsible: false,
		});
		me.RGrid=Ext.create("Shell.class.weixin.dict.lab.BLabDoctor.contrast.Doctor",{
			
			split: false,
			region: 'east',
			width:500,
			header:false,
		});
		return [me.LGrid,me.CBtn,me.RGrid];
	},
	
	createDockedItems:function(){
		var me =this;
		var items=[];
		var buttontoolbar = me.createButtonToolbarItems();
		items.push(buttontoolbar);
		return items;
	},
	
	createButtonToolbarItems:function(){
		var me =this;
		var items=[];
		items.push(
			{
				xtype:'label',
				text:'医生实验室选择',
				width:100,
				style: "font-weight:bold;color:#0000EE;"
			},
			{
				fileLabel:'实验室id',
				hidden:true,
				xtype:"textfield",
				name:'ClienteleId',
				itemId:'ClienteleId'
			},
			{
				xtype:'uxCheckTrigger',
				emptyText:'实验室',
				width:280,
				labelWidth:55,
				labelAlign:'right',
				name:"ClienteleName",
				itemId:"ClienteleName",
				className:"Shell.class.weixin.dict.lab.BLabDoctor.contrast.CheckGrid",
				listeners:{
					check:function(p,record){
						me.onCheckClick(p,record);
					}
				},
			},
		);
		
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			height:25,
			itemId: 'buttonsToolbar',
			border: false,
			items: items
		});
	},
	
	onCheckClick:function(p,record){
		var me = this;
		var buttonsTollbar = me.getComponent('buttonsToolbar');
		var clienteleId = buttonsTollbar.getComponent('ClienteleId');
		var clienteleName = buttonsTollbar.getComponent('ClienteleName');
		
		clienteleId.setValue(record ? record.get('CLIENTELE_Id'):'');
		clienteleName.setValue(record ? record.get('CLIENTELE_CNAME'):'');
		me.LGrid.labCode =record.get('CLIENTELE_Id');
		me.LGrid.onSearch(); //加载数据
		p.close();
	},
	
	/**打开智能对照*/
	openIntelligenceForm:function(dictList,ItemList){
		var me = this;
		var bottomToolbar = me.getComponent('buttonsToolbar');
	    var ClienteleId = bottomToolbar.getComponent('ClienteleId');
		JShell.Win.open('Shell.class.weixin.dict.lab.BLabDoctor.contrast.IntelligenceGrid', {
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
					me.LGrid.store.reload();
					p.close();
				}
			}
		}).show();
	},
});
