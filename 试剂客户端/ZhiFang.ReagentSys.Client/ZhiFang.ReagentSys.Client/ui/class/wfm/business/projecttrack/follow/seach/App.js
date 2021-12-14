/**
 * 项目跟踪查询
 * @author liangyl
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.seach.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '项目跟踪查询',
	/**获取数据服务路径*/
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchPContractFollowInteractionByHQL?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddPContractFollowInteraction',
    /**附件对象名*/
	objectName:'PContractFollowInteraction',
	/**附件关联对象名*/
	fObejctName:'PContractFollow',
	/**附件关联对象主键*/
	fObjectValue:'',
	/**交流关联对象是否ID*/
	fObjectIsID:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			select: function(RowModel, record) {
				JShell.Action.delay(function() {
					var id = record.get(me.Grid.PKField);
					me.Interaction.Form.fObjectValue=id;
					me.Interaction.Grid.fObjectValue=id;
					me.Interaction.Grid.onSearch();
				}, null, 300);
			},
			saveInteraction:function(p){
				p.close();
				me.Interaction.Grid.onSearch();
			}
		});
		
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		
		me.Grid = Ext.create('Shell.class.wfm.business.projecttrack.follow.seach.Grid', {
			region: 'center',
			header: false,
			title: '项目跟踪',
			itemId: 'Grid'
		});
        me.Interaction = Ext.create('Shell.class.wfm.business.projecttrack.follow.seach.InteractionApp', {
			region: 'east',
			header: false,
			defaultLoad: false,
			width: 420,
			split: true,
			collapsible: true,
			selectUrl:me.selectUrl,
			objectName:me.objectName,
			fObejctName:me.fObejctName,
			fObjectIsID:me.fObjectIsID,
			itemId: 'Interaction'
		});
		
		return [me.Interaction, me.Grid];
	}
});