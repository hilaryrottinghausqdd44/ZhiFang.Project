/**
 * 任务验收列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.check.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'任务验收列表',
    /**默认员工类型*/
	defaultUserType:'CheckerID',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				//执行完成
				var isExecuteOver = JShell.WFM.GUID.TaskStatus.ExecuteOver.GUID == record.get('PTask_Status_Id');
				//验收中
				var isCheckIng = JShell.WFM.GUID.TaskStatus.CheckIng.GUID == record.get('PTask_Status_Id');
				
				if(isExecuteOver || isCheckIng){
					me.openEditForm(id);
				}else{
					me.openShowForm(id);
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere = "ptask.CheckerID=" + userId;
		me.callParent(arguments);
	},
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.check.EditPanel', {
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
	}
});