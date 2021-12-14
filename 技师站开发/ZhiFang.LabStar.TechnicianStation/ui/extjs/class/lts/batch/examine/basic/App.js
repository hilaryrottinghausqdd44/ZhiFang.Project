/**
 * 批量检验审定审核基础类
 * @author liangyl	
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.examine.basic.App',{
    extend:'Shell.ux.panel.AppPanel',
    title:'批量检验审定审核基础类',
    
    requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
    layout : 'fit',
    /**小组id*/
	SectionID: '',	
	
	width:850,
	height:500,
	
	//执行服务
	saveUrl:'',
	BUTTON_CAN_CLICK:true,
	hideTimes:3000,
	
	//检验单列表
	TestFormGrid:'Shell.class.lts.batch.examine.basic.Grid',
	//审核人
	UserLable:'检验人',
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent:function(){
		var me = this;
		me.initDefaultDate();
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	
	createItems:function(){
		var me = this;
		me.Tab = Ext.create('Shell.class.lts.batch.examine.basic.Tab', {
			itemId:'Grid',split:true,header:false,collapsible:false,
			SectionID:me.SectionID,TestFormGrid:me.TestFormGrid//检验单列表
		});
		return [me.Tab];
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items =  [];

		items.push(me.createTopToolBar());
		items.push(me.createBottomToolBar());

		return items;
	},
	/**顶部工具栏*/
	createTopToolBar: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
	},
   /**创建功能按钮栏*/
	createBottomToolBar: function() {
		var me = this,
			items = [{
				fieldLabel:me.UserLable,xtype:'uxCheckTrigger',itemId:'AuthorizeUserName',
				className:'Shell.class.basic.user.CheckGrid',
				classConfig:{TSysCode:'1001001'},emptyText:me.UserLable,
				width:240,labelWidth:100,labelAlign:'right',
				listeners:{
					check:function(p,record){
						p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
						p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
						p.close();
					}
				}
			},{xtype:'textfield',itemId:'AuthorizeUserID',name:'AuthorizeUserID',fieldLabel:'检验人员ID',hidden:true},
			{text:'执行',tooltip:'执行',iconCls:'button-accept',margin:"0px 0px 0px 10px",
			   	handler:function(but,e){
			   		if(!me.getParams().ExecutorID){
						JShell.Msg.warning(me.UserLable+'不能为空');
						return;
			   		}
			   		if(!me.Tab.getIdList()){
						JShell.Msg.warning('检验单不能为空');
						return;
			   		}
				   	//确认审批意见
					JShell.Msg.confirm({
						title:'<div style="text-align:center;">审批意见</div>',
						msg:'',
						closable:false,
						multiline:true//多行输入框
					},function(but,text){
						if(but != "ok") return;
						me.onSaveClick(text);
					});
			   	}
			}];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'buttonsBottomToolbar',
			items: items
		});
	},
	//初始化查询日期
	initDefaultDate:function(){
		var me = this;
			date=JShell.System.Date.getDate();
		me.DEFAULT_DATE_VALUE = {start:date,end:date};
	},
	
	/**
	 * 获取查询条件
	 * itemResultFlag 都不选的话 参数值：0,0,0,全选为：1,1,1
	 * */
	getParams : function(){
		var me = this;
	},
	//执行实体封装
	getEntity : function(memoInfo){
	    var me = this;
	    return entity ={
			testFormIDList:me.Tab.getIdList(),
			empID:me.getParams().ExecutorID,
			empName:me.getParams().ExecutorName,
			memoInfo:memoInfo
		};
	},
	/**
	 * 检验确认(执行功能)
	 * memoInfo 审批意见
	 * */
	onSaveClick : function(memoInfo){
		var me = this,
			url = JShell.System.Path.ROOT + me.saveUrl;
	    
	    if(!me.BUTTON_CAN_CLICK)return;
		
	    me.BUTTON_CAN_CLICK = false;
		var entity = me.getEntity(memoInfo);
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode(entity),function(data){
			me.BUTTON_CAN_CLICK = true;
			if(data.success){
				me.fireEvent('save',me);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				//刷新检验单列表
				me.Tab.onSearchTestForm(me.getParams());
				//返回成功与失败信息
				me.Tab.setValueMsg(data.ErrorInfo,'');
			}else{
				JShell.Msg.error(data.msg,null,3000);
				//返回成功与失败信息
				me.Tab.setValueMsg('',data.msg);
			}
		});
	}
});