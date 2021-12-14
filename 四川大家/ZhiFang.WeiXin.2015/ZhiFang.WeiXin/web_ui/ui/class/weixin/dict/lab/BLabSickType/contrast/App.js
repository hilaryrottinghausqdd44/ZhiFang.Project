Ext.define("Shell.class.weixin.dict.lab.BLabSickType.contrast.App",{
	extend:"Shell.ux.panel.AppPanel",
	requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
	

	initComponent:function(){
		var me =this;
		me.items=me.createPItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	
	afterRender:function(){
		var me =this;
		me.callParent(arguments);
		me.CBtn.on({
			click:function(){
				var contorlRow = me.RGird.getSelectionModel().getSelection();
				var row = me.LGrid.getSelectionModel().getSelection();
				
				if(!contorlRow || !row){
					JShell.Msg.error('请选择要进行对照的记录');
					return;
				}
				var bottomToolbar = me.getComponent('buttonsToolbar');
	            var ClienteleId = bottomToolbar.getComponent('ClienteleId').getValue();
				if(!ClienteleId){
					JShell.Msg.error('实验室不能为空！');
					return;
				}
				me.CBtn.onSaveAdd(contorlRow[0],row[0],ClienteleId);
			},
			
			save:function(){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				me.LGrid.store.reload();
			},
			
			cancelClick:function(){
				var row = me.LGrid.getSelectionModel().getSelection();;
				
				if(!row){
					JShell.Msg.error('请选择要取消的对照行记录');
					return;
				}
				if(row[0] && row[0]!='undefined'){
					var flagId= row[0].get('BLabSickType_isContrast');
					
			        if(flagId==''){
			        	JShell.Msg.error('选择行数据未对照,不能取消对照');
			        	return;
			        }
			        me.CBtn.onClearClick(row[0]);
				}else{
					JShell.Msg.error('请选择要取消的对照行记录');
					return;
				}
			},
			
			intelligenceClick:function(){
				if(!me.LGrid.labCode){
					JShell.Msg.error('实验室为空');
					return;
				}
				var dictList=me.LGrid.getLabSickType();
				var itemList=me.RGird.getSickType();
				if(!dictList){
					JShell.Msg.error('没有可对照项');
					return ;
				}
				me.openIntelligenceForm(dictList,itemList);
			}
		});
		me.LGrid.on({
			itemclick:function(v, record){
				var sickTypeList = me.RGird.store.data.items;
				var sickTypeId=record.data.BLabSickType_sickTypeId;
				
				for(var i =0 ; i< sickTypeList.length;i++){
					if(sickTypeId==sickTypeList[i].data.SickType_Id){
						me.RGird.getSelectionModel().select(i);
						break;
					}
				}
			}
		});
	},
	
	createPItems:function(){
		var me =this;
		me.LGrid = Ext.create("Shell.class.weixin.dict.lab.BLabSickType.contrast.Grid",{
			region:"center",
			header: false,
			itemId: 'Grid'
		});
		me.CBtn = Ext.create("Shell.class.weixin.dict.lab.BlabSickType.contrast.BtnPanel",{
			region:"east",
			width:100,
			split: false,
			border:false,
			collapsible: false,
			itemId: 'Btn'
		});
		me.RGird = Ext.create("Shell.class.weixin.dict.lab.BlabSickType.contrast.SickType",{
			split: false,
			region: 'east',
			width:500,
			header:false,
			itemId: 'TestItemGrid'
		});
		return [me.LGrid,me.CBtn,me.RGird];
	},
	
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this,
		items = me.dockedItems || [];
		var buttontoolbar = me.createButtonToolbarItems();
		if(buttontoolbar) items.push(buttontoolbar);
		return items;
	},
	
	/**重写   创建功能按钮栏*/
	createButtonToolbarItems: function() {
		var me =this;
		var items=[];
		items.push(
			{
				xtype:'label',
				text:'就诊类型实验室选择',
				width:120,
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
				className:"Shell.class.weixin.dict.lab.BLabSickType.contrast.CheckGrid",
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
		//me.LGrid.internalWhere=' labcode = '+record.get('CLIENTELE_Id');
		me.LGrid.onSearch(); //加载数据
		p.close();
	},
	
	/**打开智能对照*/
	openIntelligenceForm:function(dictList,ItemList){
		var me = this;
		var bottomToolbar = me.getComponent('buttonsToolbar');
	    var ClienteleId = bottomToolbar.getComponent('ClienteleId');
		JShell.Win.open('Shell.class.weixin.dict.lab.BlabSickType.contrast.IntelligenceGrid', {
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
