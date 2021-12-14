/**
 * 任务执行列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.execute.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'任务执行列表',
    /**默认员工类型*/
	defaultUserType:'ExecutorID',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				//分配通过
				var isPublisherOver = JShell.WFM.GUID.TaskStatus.PublisherOver.GUID == record.get('PTask_Status_Id');
				//分配中
				var isExecuteIng = JShell.WFM.GUID.TaskStatus.ExecuteIng.GUID == record.get('PTask_Status_Id');
				//验收退回
				var isCheckBack = JShell.WFM.GUID.TaskStatus.CheckBack.GUID == record.get('PTask_Status_Id');
				
				if(isPublisherOver || isExecuteIng || isCheckBack){
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
		me.defaultWhere = "ptask.ExecutorID=" + userId;
		me.callParent(arguments);
	},
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.execute.EditPanel', {
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