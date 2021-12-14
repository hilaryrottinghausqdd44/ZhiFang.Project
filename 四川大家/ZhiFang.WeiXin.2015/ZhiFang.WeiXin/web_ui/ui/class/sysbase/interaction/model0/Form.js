/**
 * 互动信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.interaction.model0.Form',{
	extend:'Shell.ux.form.Panel',
    
    title:'互动信息',
	width:600,
	height:400,
    
    /**新增服务地址*/
    addUrl:'',
	/**附件对象名*/
	objectName:'',
	/**附件关联对象名*/
	fObejctName:'',
	/**附件关联对象主键*/
	fObjectValue:'',
    
    /** 每个组件的默认属性*/
    layout:'fit',
	bodyPadding:1,
    /**内容自动显示*/
	autoScroll:false,
    /**表单的默认状态*/
    formtype:'add',
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:false,
	/**启用表单状态初始化*/
	openFormType:true,
	/**显示成功信息*/
	showSuccessInfo:false,
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		//项目名称
		items.push({
			xtype:'textarea',name:'Content',itemId:'Content',
			emptyText:'等待输入...'//,allowBlank:false
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!values.Content || !values.Content.trim()){
			return null;
		}
			
		var entity = {
			Contents:values.Content,
			IsUse:true
		};
		//发送人
		entity.SenderID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		entity.SenderName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		//附件关联对象主键
		entity[me.fObejctName] = {
			Id:me.fObjectValue,
			DataTimeStamp:[0,0,0,0,0,0,0,0]
		};
		
//		Sender	发件人
//		Receiver	接收人
//		Contents	内容
//		DataAddTime	创建时间
//		IsUse	是否使用
//		Memo	备注

		
		return {entity:entity};
	}
});