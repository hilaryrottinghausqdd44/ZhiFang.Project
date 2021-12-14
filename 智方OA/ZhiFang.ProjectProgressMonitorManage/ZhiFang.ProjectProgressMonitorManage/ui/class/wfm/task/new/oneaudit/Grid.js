/**
 * 任务一审列表
 * @author liangyl	
 * @version 2017-06-09
 */
Ext.define('Shell.class.wfm.task.new.oneaudit.Grid',{
    extend: 'Shell.class.wfm.task.new.basic.Grid',
    title:'任务一审列表',
    /**默认员工类型*/
	defaultUserType:'OneAuditID',
    /**默认加载数据*/
	defaultLoad:false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				//申请
				var isApply = JShell.WFM.GUID.TaskStatus.Apply.GUID == record.get('PTask_Status_Id');
				//一审中
				var isOneAuditIng = JShell.WFM.GUID.TaskStatus.OneAuditIng.GUID == record.get('PTask_Status_Id');
				//二审退回
				var isTwoAuditBack = JShell.WFM.GUID.TaskStatus.TwoAuditBack.GUID == record.get('PTask_Status_Id');
				if(isApply || isOneAuditIng || isTwoAuditBack){
					me.openEditForm(id);
				}else{
					me.openShowForm(id);
				}
			}
		});
	},
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.oneaudit.EditPanel', {
			SUB_WIN_NO:'1',//内部窗口编号
			//resizable: false,
			TaskId:id,
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**加载符合的任务类型数据*/
	loadTaskTypeData:function(ids){
	    var me=this;
    	var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere = "ptask.OneAuditID=" + userId + ' and ptask.IsUse=1'+
			" and ptask.TypeID in(" + ids + ")";
		me.onSearch();
	}
});