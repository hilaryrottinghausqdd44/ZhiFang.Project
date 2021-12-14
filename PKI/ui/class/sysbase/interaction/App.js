/**
 * 互动信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.interaction.App',{
    extend:'Shell.ux.panel.AppPanel',
    
	/**标题*/
    title:'互动信息',
    width:1200,
    height:800,
    
    /**附属主体名*/
    PrimaryName:null,
    /**附属主体数据ID*/
	PrimaryID:null,
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Form.on({
			save:function(p,id){
				p.getComponent('Content').setValue('');
				me.Grid.onLoadData();
			}
		});
		
		me.Grid.onLoadData();
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.sysbase.interaction.List', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			/**附属主体名*/
			PrimaryName:me.PrimaryName,
			/**附属主体数据ID*/
			PrimaryID:me.PrimaryID
		});
		
		me.Form = Ext.create('Shell.class.sysbase.interaction.Form', {
			region: 'east',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			width: 300,
			/**附属主体名*/
			PrimaryName:me.PrimaryName,
			/**附属主体数据ID*/
			PrimaryID:me.PrimaryID
		});
		
		return [me.Grid,me.Form];
	}
});
	