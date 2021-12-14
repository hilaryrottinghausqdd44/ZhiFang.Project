/**
 * 项目跟踪记录浏览模块
 * @author liangyl
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.interaction.seach.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '项目跟踪记录浏览模块',
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
	
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
        me.Interaction = Ext.create('Shell.class.wfm.business.projecttrack.interaction.seach.Grid', {
			region: 'center',
			header: false,
			defaultLoad: true,
			selectUrl:me.selectUrl,
			objectName:me.objectName,
			fObejctName:me.fObejctName,
			fObjectIsID:me.fObjectIsID,
			border:false,
			itemId: 'Interaction'
		});
		
		return [me.Interaction];
	}
});