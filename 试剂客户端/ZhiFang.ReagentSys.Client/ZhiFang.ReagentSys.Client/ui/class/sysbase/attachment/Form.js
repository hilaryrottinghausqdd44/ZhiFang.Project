/**
 * 附件表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.attachment.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'附件信息',
    width:570,
	height:320,
    
    /**获取数据服务路径*/
    selectUrl:'/RBACService.svc/RBAC_UDTO_SearchAttachmentById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/RBACService.svc/RBAC_UDTO_AddAttachment',
    /**修改服务地址*/
    editUrl:'/RBACService.svc/RBAC_UDTO_UpdateAttachmentByField', 
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/** 每个组件的默认属性*/
    defaults:{
    	width:200,
        labelWidth:55,
        labelAlign:'right'
    },
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push(
			{x:10,y:10,fieldLabel:'附属主体名',name:'Attachment_PrimaryName',
				itemId:'Attachment_PrimaryName',emptyText:'必填项',allowBlank:false},
			{x:200,y:10,fieldLabel:'主体数据ID',name:'Attachment_PrimaryID',
				itemId:'Attachment_PrimaryID',emptyText:'必填项',allowBlank:false},
			{x:10,y:35,fieldLabel:'文件名',name:'Attachment_FileName',
				itemId:'Attachment_FileName',emptyText:'必填项',allowBlank:false},
			{x:200,y:35,boxLabel:'是否使用',name:'Attachment_IsUse',
				width:70,xtype:'checkbox',checked:true},
			{x:10,y:60,width:400,fieldLabel:'文件',name:'File',xtype:'filefield'},
			{x:10,y:85,width:400,fieldLabel:'备注',height:85,
				name:'Attachment_Memo',xtype:'textarea'},
			
			{fieldLabel:'附件ID',name:'Attachment_Id',hidden:true}
		);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			PrimaryName:values.Attachment_PrimaryName,
			PrimaryID:values.Attachment_PrimaryID,
			FileName:values.Attachment_FileName,
			IsUse:values.Attachment_IsUse ? true : false,
			Memo:values.Attachment_Memo
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.Attachment_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				id += '';
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});