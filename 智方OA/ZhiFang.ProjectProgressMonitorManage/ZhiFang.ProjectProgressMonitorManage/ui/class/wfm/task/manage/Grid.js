/**
 * 任务管理列表
 * @author Jcall
 * @version 2016-10-09
 */
Ext.define('Shell.class.wfm.task.manage.Grid',{
    extend: 'Shell.class.wfm.task.basic.Grid',
    
    title:'任务管理列表',
    
    /**是否只返回开启的数据*/
	IsUse:false,
	/**默认员工赋值*/
	hasDefaultUser:false,
	/**默认加载数据*/
	defaultLoad:false,
	
	/**复选框*/
	multiSelect:true,
	selType:'checkboxmodel',
	hasDel:true,
	
	/**是否使用字段的类型，bool/int，默认bool*/
    IsUseType:'bool',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskByField',
	/**修改状态服务地址*/
    editStatusUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField',
    /**新增任务操作记录服务地址*/
    addPTaskOperLogUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog',
	/**是否按部门查询*/
	hasDept:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				me.openShowForm(id);
			}
		});
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
			
		columns.splice(2,0,{
			text:'使用',dataIndex:'PTask_IsUse',width:40,
			align:'center',isBool:true,type:'bool'
		},{
			xtype: 'actioncolumn',text:'改类',align:'center',width:40,
			style:'font-weight:bold;color:white;background:orange;',
			sortable:false,hideable:false,
			items: [{
				iconCls:'button-edit hand',
				tooltip:'<b>修改任务类型(用于纠错)</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.onEditType(id);
				}
			}]
		},{
			xtype: 'actioncolumn',text:'编辑',align:'center',width:40,
			style:'font-weight:bold;color:white;background:orange;',
			sortable:false,hideable:false,
			items: [{
				iconCls:'button-edit hand',
				tooltip:'<b>编辑(用于纠错)</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.openEditForm(id);
				}
			}]
		},{
			xtype: 'actioncolumn',text:'验收',align:'center',width:40,
			style:'font-weight:bold;color:white;background:orange;',
			sortable:false,hideable:false,
			items: [{
				iconCls:'button-edit hand',
				tooltip:'<b>强制验收，用于验收人长期不验收的情况</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.onCheck(id);
				}
			}]
		});
			
		return columns;
	},
	/**重写方法*/
	createDefaultButtonToolbarItems:function(){
		var me = this,
			toolbar = me.callParent(arguments);
			
		toolbar.items.push('-',{
			text:'修改任务信息',
			tooltip:'修改任务信息及操作记录',
			iconCls:'button-edit',
			handler:function(){
				me.onChangeInfo();
			}
		});
		
		return toolbar;
	},
	/**重写方法*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.callParent(arguments);
			
		buttonToolbarItems.push('-',{
			text:'禁用',
			tooltip:'将选中的记录进行批量禁用',
			iconCls:'button-lock',
			handler:function(){
				me.onChangeUseField(false);
			}
		},{
			text:'撤销禁用',
			tooltip:'撤销禁用',
			iconCls:'button-back',
			handler:function(){
				me.onChangeUseField(true);
			}
		});
		
		return buttonToolbarItems;
	},
	/**批量修改使用字段值*/
	onChangeUseField:function(IsUse){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			me.updateOneByIsUse(i,id,IsUse);
		}
	},
	updateOneByIsUse:function(index,id,IsUse){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		
		//是否使用的类型不同处理
		if(me.IsUseType == 'int'){
			IsUse = IsUse ? "1" : "0";
		}
		
		var params = {
			entity:{
				Id:id,
				IsUse:IsUse
			},
			fields:'Id,IsUse'
		};
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	},
	/**根据字段和值列表刷新数据*/
	onSearchByFieldAndIds:function(fieldName,id){
		var me = this;
		
		if(fieldName){
			me.defaultWhere = "ptask." + fieldName + "=" + id;
		}
		
		me.onSearch();
	},
	/**编辑*/
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.manage.EditPanel', {
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
	/**修改任务类型*/
	onEditType:function(id){
		var me = this;
		var tree = JShell.Win.open('Shell.class.wfm.task.type.CheckTree', {
			SUB_WIN_NO:'2',//内部窗口编号
			//resizable: false,
			title:'任务类别选择',
			rootVisible:false,
			IDS:JShell.WFM.GUID.DictTree.TaskType.GUID,
			onAcceptClick: function() {
				var records = tree.getSelectionModel().getSelection();
				if (records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				
				var node = records[0];
				//任务类别+任务父类别+任务主类别
				if(node && node.parentNode && node.parentNode.parentNode && 
					node.parentNode.parentNode.parentNode &&
					!node.parentNode.parentNode.parentNode.parentNode){
					tree.fireEvent('accept', tree, node);
				}else{
					JShell.Msg.error('请选择一个任务详细类别，例如：开发->OA->考勤管理');
				}
			},
			listeners: {
				accept: function(p,node) {
					me.onChangeType(id,node,function(){
						p.close();
						JShell.Msg.alert('类型更改成功',null,1000);
					});
				}
			}
		}).show();
	},
	onChangeType:function(id,node,callback){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = {
			entity:{
				Id:id,
				//任务主类别
				MTypeID:node.parentNode.parentNode.data.tid,
				MTypeName:node.parentNode.parentNode.data.text,
				//任务父类别
				PTypeID:node.parentNode.data.tid,
				PTypeName:node.parentNode.data.text,
				//任务类别
				TypeID:node.data.tid,
				TypeName:node.data.text
			},
			fields:'Id,MTypeID,MTypeName,PTypeID,PTypeName,TypeID,TypeName'
		};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			var record = me.store.findRecord(me.PKField,id);
			if(data.success){
				if(!record) return;
				record.set('PTask_MTypeName',params.entity.MTypeName);
				record.set('PTask_PTypeName',params.entity.PTypeName);
				record.set('PTask_TypeName',params.entity.TypeName);
				record.commit();
				callback();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**强制验收*/
	onCheck:function(id){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editStatusUrl);
		
		JShell.Msg.confirm({
			msg:"确定要强制验收吗？"
		},function(but){
			if (but != "ok") return;
			
			var params = {
				entity:{
					Id:id,
					Status:{
						Id:JShell.WFM.GUID.TaskStatus.CheckOver.GUID,
						DataTimeStamp:[0,0,0,0,0,0,0,0]
					},
					StatusName:JShell.WFM.GUID.TaskStatus.CheckOver.text
				},
				fields:'Id,Status_Id,StatusName'
			};
				
			me.showMask('任务强制验收中');//显示遮罩层
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					//任务操作记录
					me.onSavePTaskOperLog(id,params.entity.Status.Id,"强制验收",function(){
						me.onSearch();
					});
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		});
	},
	/**任务操作记录*/
	onSavePTaskOperLog:function(PTaskID,StatusID,OperMsg,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addPTaskOperLogUrl);
			
		var USERNAME = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			USERID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		
		var entity = {
			PTaskID:PTaskID,
			PTaskOperTypeID:StatusID,
			OperaterID:USERID,
			OperaterName:USERNAME,
			OperateMemo:OperMsg
		};
		var params = Ext.JSON.encode({entity:entity});
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			callback(data);
		});
	},
	
	//修改任务信息及操作记录
	onChangeInfo:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		JShell.Win.open('Shell.class.wfm.task.manage.super.EditApp',{
			TaskId:records[0].get(me.PKField)
		}).show();
	}
});