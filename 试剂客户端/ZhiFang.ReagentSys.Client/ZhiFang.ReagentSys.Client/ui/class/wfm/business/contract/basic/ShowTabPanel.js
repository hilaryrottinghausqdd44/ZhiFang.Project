/**
 * 合同信息查看面板
 * @author Jcall
 * @version 2016-11-20
 */
Ext.define('Shell.class.wfm.business.contract.basic.ShowTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'合同信息查看面板',
    
    width:800,
	height:500,
	
	/**合同ID*/
    PK:null,
	
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
		
		me.Form = Ext.create('Shell.class.wfm.business.contract.basic.ContentPanel',{
			title:'合同内容',
			PK:me.PK
		});
		
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
		/**liangyl 
		 * 预览pdf
		 * 2017-01-06
		 * */
		me.Pdf = Ext.create('Shell.class.wfm.business.contract.basic.PdfApp', {
			title: '预览PDF',
			itemId: 'Pdf',
			border: false,
			height: me.height,
			width: me.width,
			hasBtntoolbar:false,
			defaultLoad:true,
			PK: me.PK
		});
		
		return [me.Form,me.Operate,me.Interaction,me.Pdf];
	}
});