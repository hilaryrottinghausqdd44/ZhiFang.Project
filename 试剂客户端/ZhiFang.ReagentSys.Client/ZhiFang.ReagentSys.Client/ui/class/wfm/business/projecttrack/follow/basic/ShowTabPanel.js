/**
 * 项目信息查看面板
 * @author liangyl	
 * @version 2017-08-09
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.basic.ShowTabPanel',{
    extend: 'Ext.tab.Panel',
    title:'项目信息查看面板',
    
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
		
		me.Form = Ext.create('Shell.class.wfm.business.projecttrack.follow.basic.ContentPanel',{
			title:'项目内容',
			PK:me.PK
		});
		me.Interaction = Ext.create('Shell.class.wfm.business.projecttrack.interaction.App',{
			title:'跟踪信息',
			FormPosition:'s',
			PK:me.PK
		});
	
		return [me.Form,me.Interaction];
	}
});