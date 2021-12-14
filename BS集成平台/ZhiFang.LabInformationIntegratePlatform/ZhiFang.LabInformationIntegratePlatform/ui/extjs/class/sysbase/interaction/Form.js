/**
 * 互动信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.interaction.Form',{
	extend:'Shell.ux.form.Panel',
    
    title:'互动信息',
	width:600,
	height:400,
    
    /**附属主体名*/
    PrimaryName:null,
    /**附属主体数据ID*/
	PrimaryID:null,
    
    /**新增服务地址*/
    addUrl:'/WorkManageService.svc/ST_UDTO_AddFInteraction',
    
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
	hasReset:true,
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
			PrimaryName:me.PrimaryName,
			PrimaryID:me.PrimaryID,
			HasAttachment:false,
			Contents:values.Content,
			IsUse:true
		};
		
//		InteractionID	互动主键ID
//		PrimaryName	附属主体名
//		PrimaryID	附属主体数据ID
//		HasAttachment	是否带附件
//		Sender	发件人
//		Receiver	接收人
//		Contents	内容
//		DataAddTime	创建时间
//		IsUse	是否使用
//		Memo	备注

		
		return {entity:entity};
	}
});