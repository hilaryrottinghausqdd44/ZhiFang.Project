/**
 * 互动信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.interaction.model0.App',{
    extend:'Shell.ux.panel.AppPanel',
    
	/**标题*/
    title:'互动信息',
    width:1200,
    height:800,
    
    /**获取数据服务路径*/
	selectUrl:'',//BaseService.svc/ST_UDTO_SearchFInteractionByHQL?isPlanish=true',
	/**新增服务地址*/
    addUrl:'',//'/WorkManageService.svc/ST_UDTO_AddFInteraction',
    /**附件对象名*/
	objectName:'',
	/**附件关联对象名*/
	fObejctName:'',
	/**附件关联对象主键*/
	fObjectValue:'',
    
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
		
		if(!me.selectUrl || !me.addUrl || !me.objectName || !me.fObejctName || !me.fObjectValue){
			me.html = 
				'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">'+
					'请传递selectUrl、addUrl、objectName、fObejctName、fObjectValue参数！'+
				'</div>';
		}else{
			me.items = me.createItems();
		}
		
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.sysbase.interaction.model0.List', {
			region: 'center',
			header: false,
			itemId: 'Grid',
			selectUrl:me.selectUrl,
			objectName:me.objectName,
			fObejctName:me.fObejctName,
			fObjectValue:me.fObjectValue
		});
		
		me.Form = Ext.create('Shell.class.sysbase.interaction.model0.Form', {
			region: 'south',
			header: false,
			itemId: 'Form',
			split: true,
			collapsible: true,
			height: 150,
			addUrl:me.addUrl,
			objectName:me.objectName,
			fObejctName:me.fObejctName,
			fObjectValue:me.fObjectValue
		});
		
		return [me.Grid,me.Form];
	}
});
	