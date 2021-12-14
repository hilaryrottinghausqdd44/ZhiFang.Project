/**
 * 公共交流互动信息
 * @author longfc
 * @version 2017-03-20
 */
Ext.define('Shell.class.sysbase.scinteraction.AppExt', {
	extend: 'Shell.class.sysbase.scinteraction.basic.App',
	/**获取数据服务路径*/
	selectUrl: '/SystemCommonService.svc/SC_UDTO_SearchSCInteractionByHQL?isPlanish=true',
	/**依某一业务对象ID获取该业务对象ID下的所有交流内容信息*/
	selectAllUrl: '/SystemCommonService.svc/SC_UDTO_SearchAllSCInteractionByBobjectID?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SystemCommonService.svc/SC_UDTO_AddSCInteractionExtend',
	/**交流对象名*/
	objectName: 'SCInteraction',
	/**附件关联对象名*/
	fObejctName: 'Bobject',
	/**附件关联对象主键*/
	fObjectValue: '',
	FormPosition: 's',
	/**ID*/
	PK: '',
	fObjectIsID: true,
	width: 1200,
	height: 800,

	initComponent: function() {
		var me = this;
		me.fObjectValue = me.PK;
		if(me.objectName && me.fObejctName && me.fObjectValue) {
			me.items = me.createItems();
		} else {
			me.html =
				'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">' +
				'请传递objectName、fObejctName、fObjectValue参数！' +
				'</div>';
		}
		me.callParent(arguments);
	},
});