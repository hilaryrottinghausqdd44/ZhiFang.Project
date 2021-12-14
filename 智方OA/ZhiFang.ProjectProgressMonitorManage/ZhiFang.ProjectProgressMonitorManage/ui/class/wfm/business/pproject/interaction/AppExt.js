/**
 * 公共交流互动信息
 * @author liangyl	
 * @version 2017-03-20
 */
Ext.define('Shell.class.wfm.business.pproject.interaction.AppExt', {
	extend: 'Shell.class.wfm.business.pproject.interaction.App',
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskProgressByHQL?isPlanish=true',
//	/**依某一业务对象ID获取该业务对象ID下的所有交流内容信息*/
//	selectAllUrl: '/ProjectProgressMonitorManageService.svc/SC_UDTO_SearchAllSCInteractionByBobjectID?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPProjectTaskProgress',
	/**交流对象名*/
	objectName: 'PProjectTaskProgress',
	/**附件关联对象名*/
	fObejctName: 'PProject',
	/**附件关联对象主键*/
	fObjectValue: '',
	FormPosition: 's',
	/**ID*/
	PK: '',
	fObjectIsID: true,
	width: 1200,
	height: 600,
    ProjectID:null,
    ProjectTaskID:null,
	initComponent: function() {
		var me = this;
		me.ProjectTaskID=me.ProjectTaskID;
		me.fObjectValue = me.ProjectID;
		me.ProjectID = me.ProjectID;
		
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