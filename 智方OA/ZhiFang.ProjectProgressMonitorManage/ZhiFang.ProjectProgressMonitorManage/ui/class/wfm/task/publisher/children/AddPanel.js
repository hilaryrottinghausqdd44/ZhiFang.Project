/**
 * 子任务信息新增页面
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.task.publisher.children.AddPanel',{
    extend: 'Ext.tab.Panel',
    title:'子任务信息新增页面',
    
    /**是否是申请任务*/
    isApplyTask:false,
    
	width:790,
	height:400,
    
    autoScroll:false,
    FormConfig:null,
    
    /**任务ID*/
    TaskId:null,
    
    /**保存数据提示*/
	saveText:JShell.Server.SAVE_TEXT,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
			beforesave:function(p){
				me.showMask(me.saveText);
			},
			aftersave:function(p,id){
				me.hideMask();
				me.TaskId = id;
				me.onSaveAttachment(id);
			}
		});
		me.Attachment.on({
			save:function(p){
				me.hideMask();
				me.fireEvent('save',me,me.TaskId);
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			FormClassName = '';
		
		if(me.isApplyTask){
			FormClassName = 'Shell.class.wfm.task.publisher.children.ApplyForm';
		}else{
			FormClassName = 'Shell.class.wfm.task.publisher.children.PublisherForm';
		}
		
		me.Form = Ext.create(FormClassName,Ext.apply(me.FormConfig,{
			formtype:'add',
			hasLoadMask:false,//开启加载数据遮罩层
			title:'任务内容'
		}));
		
		me.Attachment = Ext.create('Shell.class.wfm.task.attachment.Upload',{
			title:'任务附件'
		});
		
		return [me.Form,me.Attachment];
	},
	onSaveAttachment:function(id){
		var me = this;
		
		me.showMask(me.saveText);
		
		me.Attachment.setValues({fkObjectId:id});
		me.Attachment.save();
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