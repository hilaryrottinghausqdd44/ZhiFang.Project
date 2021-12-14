/**
 * 项目信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.model.ExtraForm',{
	extend:'Shell.ux.form.Panel',
	
	/**其他信息模板地址*/
	OtherMsgUrl:null,
	/**其他信息*/
	OtherMsgContent:null,
	
    title:'',
    width:570,
	height:320,
	bodyPadding:10,
	
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:80,
        labelAlign:'right'
    },
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**启用表单状态初始化*/
	openFormType:true,
	/**显示成功信息*/
	showSuccessInfo:false,
	
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.dockedItems = [Ext.create('Shell.ux.toolbar.Button',{
			dock:'top',
			itemId:'topToolbar',
			items:[{
				xtype:'label',
				style:'margin:2px;color:#04408c;font-weight:bold',
				text:me.title
			},'->','COLLAPSE_LEFT']
		})];
		
		me.callParent(arguments);
	},
	onCollapseClick:function(){
		this.collapse();
	},
	openMsgForm:function(MsgField,Content){
		var me = this;
		var url = JShell.System.Path.getUrl(me.OtherMsgUrl);
		if(!Content){
			JShell.Server.get(url,function(text){
				me.OtherMsgContent = text;
				me.doOpenMsgForm(MsgField);
			},null,null,true);
		}else{
			me.doOpenMsgForm(MsgField);
		}
	},
	doOpenMsgForm:function(MsgField){
		var me = this;
		JShell.Win.open('Shell.class.sysbase.attachment.MsgForm',{
			resizable:false,
			editUrl:me.editUrl,
			MsgField:MsgField,
			DataId:me.PK,
			Content:me.OtherMsgContent,
			listeners:{
				save:function(p){
					me.OtherMsgContent = p.ContentMsg;
					p.close();
					me.afterSaveOtherMsg();
				}
			}
		}).show();
	},
	afterSaveOtherMsg:function(){
		
	}
});