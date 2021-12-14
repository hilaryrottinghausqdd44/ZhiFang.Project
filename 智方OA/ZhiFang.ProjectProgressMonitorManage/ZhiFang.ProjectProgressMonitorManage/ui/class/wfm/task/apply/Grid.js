/**
 * 任务申请列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.apply.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'任务申请列表',
    
    /**获取任务类型服务地址*/
    getTaskTypeUrl:'/ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField',
	
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
				//一审退回
				var isOneAuditBack = JShell.WFM.GUID.TaskStatus.OneAuditBack.GUID == record.get('PTask_Status_Id');
				if(isTemporary || isOneAuditBack){
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
		me.defaultWhere = "ptask.ApplyID=" + userId;
		
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
			
		columns.splice(5,0,{
			xtype: 'actioncolumn',
			text: '验收',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					//执行完成
					var isExecuteOver = JShell.WFM.GUID.TaskStatus.ExecuteOver.GUID == record.get('PTask_Status_Id');
					//验收中
					var isCheckIng = JShell.WFM.GUID.TaskStatus.CheckIng.GUID == record.get('PTask_Status_Id');
					
					//暂存
					var isTemporary = JShell.WFM.GUID.TaskStatus.Temporary.GUID == record.get('PTask_Status_Id');
					//申请
					var isApply = JShell.WFM.GUID.TaskStatus.Apply.GUID == record.get('PTask_Status_Id');
					//已终止
					var isStop = JShell.WFM.GUID.TaskStatus.IsStop.GUID == record.get('PTask_Status_Id');
					//已验收
					var isCheckOver = JShell.WFM.GUID.TaskStatus.CheckOver.GUID == record.get('PTask_Status_Id');
					
					//暂存、申请、已终止的任务不能验收
					if(!isTemporary && !isApply && !isStop && !isCheckOver){
						if(isExecuteOver || isCheckIng){
							meta.tdAttr = 'data-qtip="<b style=\'color:#7cba59\'>正常验收</b>"';
						}else{
							meta.tdAttr = 'data-qtip="<b style=\'color:#dd6572\'>提前验收</b>"';
							meta.style = 'background-color:#dd6572';
						}
						return 'button-save hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.onCheckForm(id);
				}
			}]
		});
		
		columns.splice(2,0,{
			xtype: 'actioncolumn',
			text: '撤回',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					//申请
					var isApply = JShell.WFM.GUID.TaskStatus.Apply.GUID == record.get('PTask_Status_Id');
					
					if(isApply){
						meta.tdAttr = 'data-qtip="<b>撤回申请</b>"';
						return 'button-back hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.onBackApply(id);
				}
			}]
		});
			
		return columns;
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
		JShell.Win.open('Shell.class.wfm.task.apply.AddPanel', {
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
	/**修改*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.apply.EditPanel', {
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
	},
	/**验收*/
	onCheckForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.check.EditPanel', {
			SUB_WIN_NO:'3',//内部窗口编号
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
	
	/**撤回*/
	onBackApply:function(id){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);
			
		JShell.Msg.confirm({
			msg:'确定要撤回任务吗？'
		},function(but){
			if (but != "ok") return;
			
			var params = {
				entity:{
					Id:id,
					Status:{
						Id:JShell.WFM.GUID.TaskStatus.Temporary.GUID,
						DataTimeStamp:[0,0,0,0,0,0,0,0]
					},
					StatusName:JShell.WFM.GUID.TaskStatus.Temporary.text
				},
				fields:'Id,Status_Id,StatusName'
			};
				
			me.showMask('任务撤回中');//显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					me.onSearch();
				}else{
					var msg = data.msg ? data.msg : '任务状态已经更改，请刷新列表后再操作。</br>如果想撤回任务，请联系一审人员，让其主动退回。';
					JShell.Msg.error(msg);
				}
			});
		});
	}
});