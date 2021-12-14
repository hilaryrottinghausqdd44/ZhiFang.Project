/**
 * 任务类型人员
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.type.user.App',{
    extend:'Shell.ux.panel.AppPanel',
    
    title:'任务类型人员',
    
    /**获取任务类型人员服务*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkByHQL',
    /**新增任务类型人员服务*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskTypeEmpLink',
    /**修改任务类型人员服务*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskTypeEmpLinkByField',
    /**删除任务类型人员服务*/
    delUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPTaskTypeEmpLink',
    
    width:600,
    height:400,
    /**字典树IDS*/
    IDS:JShell.WFM.GUID.DictTree.TaskType.GUID,
    
    /**选中的员工*/
	SelectUser:{},
    /**原始任务类型人员数据*/
    ResourceDatas:[],
    /**需要新增的数据*/
    AddDatas:[],
    /**需要修改的数据*/
    UpdateDatas:[],
    /**需要删除的数据*/
    RemoveDatas:[],
    /**错误信息*/
    ErrorInfo:[],
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.Grid.on({
			itemclick:function(v, record) {
				me.selectOneRow(record);
			},
			select:function(RowModel, record){
				me.selectOneRow(record);
			},
			nodata:function(){
				me.Tree.disableControl();
			}
		});
		
		me.Tree.on({
			save:function(p,nodes){
				me.onSaveTaskTypeUser(nodes);
			}
		});
	},
    
	initComponent:function(){
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		
		me.Grid = Ext.create('Shell.class.sysbase.user.role.SimpleGrid', {
			region: 'west',
			header: false,
			split: true,
			collapsible: true,
			itemId:'Grid',
			autoSelect:true,//默认选中数据
			ROLE_IDS:JShell.WFM.GUID.Role.TaskExamineTwo.GUID + ',' + JShell.WFM.GUID.Role.TaskAllot.GUID
		});
		me.Tree = Ext.create('Shell.class.wfm.task.type.TreeGrid', {
			region: 'center',
			header: false,
			itemId: 'Tree',
			IDS:me.IDS//字典树IDS
		});
		
		return [me.Grid,me.Tree];
	},
	
	/**选一行处理*/
	selectOneRow:function(record){
		var me = this;
		JShell.Action.delay(function(){
			me.SelectUser = {
				Id:record.get('RBACEmpRoles_HREmployee_Id'),
				CName:record.get('RBACEmpRoles_HREmployee_CName')
			};
			me.Tree.enableControl();
			me.changeTaskTypeUser(me.SelectUser.Id);
		},null,300);
	},
	/**获取任务类型人员数据*/
	changeTaskTypeUser:function(userId){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
			
		var fields = [
			'PTaskTypeEmpLink_Id',
			'PTaskTypeEmpLink_TaskTypeID',
			'PTaskTypeEmpLink_TwoAudit',
			'PTaskTypeEmpLink_Publish'
		];
		
		url += '?isPlanish=true&fields=' + fields.join(',');
		url += '&where=ptasktypeemplink.EmpID=' + userId;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.changeChecked(data.value);
			}else{
				me.ResourceDatas = [];
				me.Tree.onResetData();
				JShell.Msg.error(data.msg);
			}
		});
	},
	changeChecked:function(data){
		var me = this,
			list = (data || {}).list || [],
			len = list.length;
			
		me.ResourceDatas = [];
		for(var i=0;i<len;i++){
			var id = list[i].PTaskTypeEmpLink_Id;
			if(id){
				list[i] = JShell.Server.Mapping(list[i]);
				me.ResourceDatas.push({
					Id:list[i].PTaskTypeEmpLink_Id,
					TaskTypeId:list[i].PTaskTypeEmpLink_TaskTypeID,
					TwoAudit:list[i].PTaskTypeEmpLink_TwoAudit,
					Publish:list[i].PTaskTypeEmpLink_Publish
				});
			}
		}
		
		me.Tree.onChangeData(me.ResourceDatas);
	},
	
	/**保存任务类型人员*/
	onSaveTaskTypeUser:function(nodes){
		var me = this;
		
		//更改新增和删除数据
		me.onChangeAddAndUpdateAndRemoveDatas(nodes);
		
		JShell.Msg.log('人员：' + me.SelectUser.CName);
		for(var i=0;i<me.AddDatas.length;i++){
			JShell.Msg.log('新增任务类型人员：' + me.AddDatas[i].CName);
		}
		for(var i=0;i<me.UpdateDatas.length;i++){
			JShell.Msg.log('修改任务类型人员：' + me.UpdateDatas[i].CName);
		}
		for(var i=0;i<me.RemoveDatas.length;i++){
			JShell.Msg.log('删除任务类型人员：' + me.RemoveDatas[i].CName);
		}
		
		//新增&删除操作
		if(me.AddDatas.length > 0 || me.UpdateDatas.length > 0 || me.RemoveDatas.length > 0){
			me.doAddAndUpdateAndRemoveDatas();
		}
	},
	/**更改新增、修改、删除数据*/
	onChangeAddAndUpdateAndRemoveDatas:function(nodes){
		var me = this,
			nodesLen = nodes.length,
			ResourceDatas = Ext.clone(me.ResourceDatas),
			resourceLen = ResourceDatas.length;
			
		me.AddDatas = [];
		me.UpdateDatas = [];
		me.RemoveDatas = [];
		me.ErrorInfo = [];
		
		for(var i=0;i<nodesLen;i++){
			var id = nodes[i].tid;
			if(!id) continue;
			
			var PTaskTypeEmpLinkId = nodes[i].PTaskTypeEmpLinkId,
				TaskTypeName = nodes[i].text,
				TwoAudit = nodes[i].TwoAudit,
				Publish = nodes[i].Publish,
				inArr = false;//任务类型在原始数据中是否存在
				
			for(var j=0;j<resourceLen;j++){
				if(ResourceDatas[j] && ResourceDatas[j].TaskTypeId == id){
					if(TwoAudit || Publish){
						//任务类型存在，更新任务类型
						me.UpdateDatas.push({
							Id:ResourceDatas[j].Id,
							TaskTypeName:TaskTypeName,
							TwoAudit:TwoAudit,
							Publish:Publish
						});
					}else{
						//需要删除的任务类型
						me.RemoveDatas.push(ResourceDatas[j]);
					}
					inArr = true;
					break;
				}
			}
			//任务类型不存在，新增
			if(!inArr){
				me.AddDatas.push({
					TaskTypeID:id,
					TaskTypeName:TaskTypeName,
					TwoAudit:TwoAudit,
					Publish:Publish
				});
			}
		}
	},
	/**新增修改删除操作*/
	doAddAndUpdateAndRemoveDatas:function(){
		var me = this;
			
		me.ErrorInfo = [];
		me.showMask(me.saveText);//显示遮罩层
		me.doAddDatas(0);
	},
	/**新增数据*/
	doAddDatas:function(index){
		var me = this,
			AddDatas = me.AddDatas,
			len = AddDatas.length;
			
		if(index >= len){
			me.doUpdateDatas(0);
		}else{
			var url = JShell.System.Path.ROOT + me.addUrl;
			var entity = {
				EmpID:me.SelectUser.Id,
				EmpName:me.SelectUser.CName,
				TaskTypeID:AddDatas[index].TaskTypeID,
				TaskTypeName:AddDatas[index].TaskTypeName,
				TwoAudit:AddDatas[index].TwoAudit,
				Publish:AddDatas[index].Publish
			};
			var params = Ext.encode({entity:entity});
			
			JShell.Server.post(url,params,function(data){
				if(!data.success){
					me.ErrorInfo.push(AddDatas[index].CName + ' 新增错误');
				}
				me.doAddDatas(++index);
			});
		}
	},
	/**更新数据*/
	doUpdateDatas:function(index){
		var me = this,
			UpdateDatas = me.UpdateDatas,
			len = UpdateDatas.length;
			
		if(index >= len){
			me.doRomoveDatas(0);
		}else{
			var url = JShell.System.Path.ROOT + me.editUrl;
			var entity = {
				Id:UpdateDatas[index].Id,
				TwoAudit:UpdateDatas[index].TwoAudit,
				Publish:UpdateDatas[index].Publish
			};
			var fields = ['Id','TwoAudit','Publish'];
			var params = Ext.encode({entity:entity,fields:fields.join(',')});
			
			JShell.Server.post(url,params,function(data){
				if(!data.success){
					me.ErrorInfo.push(UpdateDatas[index].CName + ' 更新错误');
				}
				me.doUpdateDatas(++index);
			});
		}
	},
	/**删除数据*/
	doRomoveDatas:function(index){
		var me = this,
			RemoveDatas = me.RemoveDatas,
			len = RemoveDatas.length;
			
		if(index >= len){
			me.afterAddAndUpdateAndRemoveDatas();
		}else{
			var url = JShell.System.Path.ROOT + me.delUrl + '?id=' + RemoveDatas[index].Id;
				
			JShell.Server.get(url,function(data){
				if(!data.success){
					me.ErrorInfo.push(RemoveDatas[index].CName + ' 删除错误');
				}
				me.doRomoveDatas(++index);
			});
		}
	},
	afterAddAndUpdateAndRemoveDatas:function(){
		var me = this;
		me.hideMask();//隐藏遮罩层
		if(me.ErrorInfo.length > 0){
			JShell.Msg.error(me.ErrorInfo.join('</br>'));
		}else{
			JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
		}
		
		me.AddDatas = [];
		me.UpdateDatas = [];
		me.RemoveDatas = [];
		me.ErrorInfo = [];
		
		var record = me.Grid.store.findRecord('RBACEmpRoles_HREmployee_Id',me.SelectUser.Id);
		me.selectOneRow(record);
	}
});