/**
 * 任务申请/一审列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.apply.oneaudit.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'任务申请/一审列表',
    
    getTaskTypeUrl:'/ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeByHQL?isPlanish=true',
	
	/**默认员工类型*/
	defaultUserType:'ApplyID',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				//暂存
				var isTemporary = JShell.WFM.GUID.TaskStatus.Temporary.GUID == record.get('PTask_Status_Id');
				//二审退回
				var isTwoAuditBack = JShell.WFM.GUID.TaskStatus.TwoAuditBack.GUID == record.get('PTask_Status_Id');
				if(isTemporary || isTwoAuditBack){
					me.openEditForm(id);
				}else{
					me.openShowForm(id);
				}
			}
		});
		me.initAddButtons();
	},
	initComponent:function(){
		var me = this;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.defaultWhere = "ptask.ApplyID=" + userId + " and ptask.OneAuditID=" + userId;
		me.callParent(arguments);
	},
	initAddButtons:function(){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.getTaskTypeUrl);
		
		url += '&fields=BDictTree_Id,BDictTree_CName';
		url += '&where=bdicttree.IsUse=1 and bdicttree.ParentID=' + JShell.WFM.GUID.DictTree.TaskType.GUID;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.onAddButtons((data.value || {}).list);
			}
		});
	},
	onAddButtons:function(list){
		var me = this,
			arr = list || [],
			len = arr.length,
			items = [];
			
		for(var i=0;i<len;i++){
			var obj = list[i];
			items.push({
				iconCls:'button-add',
				tooltip:'新增"' + obj.BDictTree_CName + '"类型的任务',
				text:obj.BDictTree_CName,
				TaskMTypeId:obj.BDictTree_Id,
				TaskMTypeName:obj.BDictTree_CName,
				handler:function(but){
					me.onAddClick(but.TaskMTypeId,but.TaskMTypeName);
				}
			});
		}
		
		if(items.length > 0){
			var buttonsToolbar = me.getComponent('buttonsToolbar');
			buttonsToolbar.insert(1,['-',{
				xtype:'button',
				iconCls:'button-add',
				text:'任务申请',
				tooltip:'任务申请',
				menu:items
			}]);
		}
	},
	/**新增任务*/
	onAddClick:function(TaskMTypeId,TaskMTypeName){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.apply.oneaudit.AddPanel', {
			SUB_WIN_NO:'1',//内部窗口编号
			title:'【' + TaskMTypeName + '】任务信息新增页面',
			resizable: false,
			FormConfig:{
				TaskMTypeId:TaskMTypeId,//任务主类别ID
				TaskMTypeName:TaskMTypeName//任务主类别名称
			},
			listeners: {
				save: function(p,id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.apply.oneaudit.EditPanel', {
			SUB_WIN_NO:'2',//内部窗口编号
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