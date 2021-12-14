/**
 * 任务信息基础页签页面
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.basic.EditTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'任务信息基础页签页面',
    
    width:790,
	height:430,
    
    autoScroll:false,
    FormClassName:null,
    FormClassConfig:null,
    
    /**任务ID*/
    TaskId:null,
    /**保存数据提示*/
	saveText:JShell.Server.SAVE_TEXT,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		if(me.FormClassName){
			me.Form = Ext.create(me.FormClassName,Ext.apply(me.FormClassConfig,{
				title:'任务内容',
				PK:me.TaskId
			}));
		}else{
			me.Form = Ext.create('Ext.panel.Panel',{
				title:'任务内容',
				html:'<div style="color:red;text-align:center;margin:5px;font-weight:bold;">没有配置FormClassName</div>'
			});
		}
		
		me.Attachment = Ext.create('Shell.class.wfm.task.attachment.Upload',{
			title:'任务附件',
			TaskId:me.TaskId
		});
		
		me.Operate = Ext.create('Shell.class.wfm.task.operate.Panel',{
			title:'操作记录',
			TaskId:me.TaskId
		});
		
		me.Interaction = Ext.create('Shell.class.wfm.task.interaction.App',{
			title:'交流信息',
			FormPosition:'e',
			TaskId:me.TaskId
		});
		
		return [me.Form,me.Attachment,me.Operate,me.Interaction];
	},
	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.body){me.body.unmask();}//隐藏遮罩层
	}
});