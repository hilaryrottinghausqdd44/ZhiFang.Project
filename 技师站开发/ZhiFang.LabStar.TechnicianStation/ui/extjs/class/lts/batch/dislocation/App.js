/**
 * 批量样本错位处理
 * @author liangyl	
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.dislocation.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'批量样本错位处理(批量重新提取仪器结果)',
    hasLoadMask:true,
    /**小组id*/
	SectionID: '',	
	//批量提取仪器样本单结果
    saveUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_BatchExtractEquipResult',
	layout: {
	    type: 'vbox',
	    align : 'stretch',
	    pack  : 'start',
	},
	bodyStyle:'background:#dfe8f6',
	//按钮是否可点击
	BUTTON_CAN_CLICK: true,
	hideTimes:3000,
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
		    itemdblclick:function(v, record) {
				me.onSearch(v, record);
			},
			select:function(v, record){
				me.onSearch(v, record);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	
	createItems:function(){
		var me = this;
		me.Grid = Ext.create('Shell.class.lts.batch.dislocation.Grid', {
			flex:1,
			itemId:'Grid',
			split:true,
			header:false,
			SectionID:me.SectionID,
			collapsible:false
		});
		me.Panel = Ext.create('Shell.class.lts.batch.dislocation.Panel', {
			flex:1,
			itemId:'Panel',
             bodyPadding:'1px 0px 0px 0px',
			header:false
		});
		return [me.Grid,me.Panel];
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
		    items = [],
			buttonToolbarItems = me.buttonToolbarItems || [];
	    buttonToolbarItems.push({
	    	xtype: 'checkbox', boxLabel: '检验单中仪器自增项目删除', itemId: 'isDelAuotAddItem', name: 'isDelAuotAddItem', checked: true,margin:'0 30 0 0'
	    },{ 
	    	xtype: 'checkbox', boxLabel: '检验单样本号变更', itemId: 'isChangeSampleNo', name: 'isChangeSampleNo', checked: true,margin:'0 30 0 0'
	    },{
	    	text:'执行',tooltip:'执行',iconCls:'button-save',
		    handler:function(but,e){
		    	me.onSaveClick();
		    }
		});
		
		items.push(Ext.create('Shell.ux.toolbar.Button',{
			dock: 'bottom',
			itemId:'bottomToolbar',
			items:buttonToolbarItems
		}));
		return items;
	},
	onSearch : function(v, record){
		var me = this;
		JShell.Action.delay(function(){
			var TestFormID = record.get('LisTestForm_Id');
			var EquipFormID = record.get('LisTestForm_EquipFormID');
			me.Panel.onSearch(TestFormID,EquipFormID);
		},null,500);
	},
	//批量提取仪器结果
	onSaveClick:function(){
		var me = this,
		    url = JShell.System.Path.ROOT + me.saveUrl;
	    if(!me.BUTTON_CAN_CLICK)return;
	    var bottomToolbar = me.getComponent('bottomToolbar'),
	        isChangeSampleNo = bottomToolbar.getComponent('isChangeSampleNo').getValue(),
	        isDelAuotAddItem = bottomToolbar.getComponent('isDelAuotAddItem').getValue();
	    var idsobj = me.Grid.getIdList();
	    //testFormIDList, equipFormIDList，两个参数的ID要对应，就是说顺序一致
		var entity ={
			testFormIDList:idsobj.testFormIDList,//检验单id字符串列表
			equipFormIDList:idsobj.equipFormIDList,//仪器检验单id字符串列表
			isChangeSampleNo:isChangeSampleNo,//是否改变样本单样本号
			isDelAuotAddItem:isDelAuotAddItem//是否删除检验单中仪器自增项目
		};
		me.BUTTON_CAN_CLICK = false;
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
			me.BUTTON_CAN_CLICK = true;
			if(data.success){
				me.fireEvent('save',me);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});