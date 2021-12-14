/**
 * 合同基础页签页面
 * @author Jcall
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.contract.basic.ActionTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'合同基础页签页面',
    
    width:800,
	height:500,
	
    /**通过按钮文字*/
    OverButtonName:'',
    /**不通过按钮文字*/
    BackButtonName:'',
    
    /**通过状态文字*/
	OverName:'',
	/**不通过状态文字*/
	BackName:'',
	
	/**处理意见字段*/
	OperMsgField:'',
	
	/**合同ID*/
    PK:null,
    
    /**表单参数*/
    FormConfig:null,
    /**默认要创建的表单 @author liangyl @version 2017-07-28*/
	ClassFormPanel:'Shell.class.wfm.business.contract.basic.ActionForm',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
			save: function(p, id) {
				me.fireEvent('save', me, id);
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
		var me = this;
		
		me.Form = Ext.create(me.ClassFormPanel,Ext.apply({
			title:'合同内容',
			OverButtonName:me.OverButtonName,
		    BackButtonName:me.BackButtonName,
		    OverName:me.OverName,
		    BackName:me.BackName,
		    OperMsgField:me.OperMsgField,
			PK:me.PK
		},me.FormConfig));
		
		me.Operate = Ext.create('Shell.class.sysbase.scoperation.SCOperation',{
			title:'操作记录',
			classNameSpace:'ZhiFang.Entity.ProjectProgressMonitorManage',//类域
			className:'PContractStatus',//类名
			PK:me.PK
		});
		
		me.Interaction = Ext.create('Shell.class.sysbase.scinteraction.App',{
			title:'交流信息',
			FormPosition:'e',
			PK:me.PK
		});
		
		return [me.Form,me.Operate,me.Interaction];

	}
});