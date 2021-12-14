/**
 * 报销信息基础页签页面
 * @author liangyl	
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.apply.EditTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'报销信息基础页签页面',
   	width: 750,
	height: 350,
    autoScroll:false,
    FormClassName:null,
     FormClassConfig:{
    	formtype:'edit'
    },
    formtype:'edit',
    /**报销单ID*/
    PK:null,
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
				title:'单据信息',
				PK:me.PK
			}));
		}else{
			me.Form = Ext.create('Ext.panel.Panel',{
				title:'单据内容',
				html:'<div style="color:red;text-align:center;margin:5px;font-weight:bold;">没有配置FormClassName</div>'
			});
		}
		
		me.Attachment = Ext.create('Shell.class.sysbase.scattachment.SCAttachment', {
			title: '附件',
			PK: me.PK,
			formtype: me.formtype,
			defaultLoad: true,
			SaveCategory: "PExpenseAccount"
		});
		me.Operate = Ext.create('Shell.class.wfm.business.expenseaccount.operate.Panel', {
			title: '操作记录',
			itemId: 'Operate',
			hasLoadMask: false,
			PK: me.PK
		});
		
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App',{
			title:'交流信息',
			FormPosition:'e',
			PK:me.PK
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