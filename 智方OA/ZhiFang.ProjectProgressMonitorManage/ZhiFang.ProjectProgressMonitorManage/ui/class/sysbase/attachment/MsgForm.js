/**
 * 附加信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.attachment.MsgForm',{
    extend:'Shell.ux.form.Panel',
    
    title:'附加信息',
    width:800,
	height:600,
	
	/**修改服务地址*/
    editUrl:'', 
	/**信息字段*/
    MsgField:'',
    /**原始内容*/
    Content:'',
    /**数据ID*/
    DataId:'',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/** 每个组件的默认属性*/
    layout:'fit',
	bodyPadding:'1px 1px 5px 1px',
    /**内容自动显示*/
	autoScroll:false,
	/**显示成功信息*/
	showSuccessInfo:false,
	/**当前内容*/
    ContentMsg:'',
    /**表单的默认状态*/
    formtype:'edit',
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push({
			xtype:'htmleditor',
			itemId:'html',
			value:me.Content
		},{
			fieldLabel:'主键ID',
			name:'Id',
			hidden:true
		});
		
		return items;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			MsgCom = me.getComponent('html'),
			Msg = MsgCom.getValue(),
			entity = {};
		
		entity.fields = ['Id',me.MsgField].join(',');
		entity.entity = {Id:me.DataId};
		entity.entity[me.MsgField] = Msg;
		
		me.ContentMsg = Msg;
		
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	}
});