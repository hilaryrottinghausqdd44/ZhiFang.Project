/**
 * 互动信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.interaction.App',{
    extend:'Shell.class.sysbase.interaction.App',
	/**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInteractionByHQL?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPInteraction',
    /**附件对象名*/
	objectName:'PInteraction',
	/**附件关联对象名*/
	fObejctName:'PTask',
	/**附件关联对象主键*/
	fObjectValue:'',
	
	/**任务ID*/
	TaskId:'',
	
	initComponent:function(){
		var me = this;
		
		me.fObjectValue = me.TaskId;
		
		me.callParent(arguments);
	}
});
	