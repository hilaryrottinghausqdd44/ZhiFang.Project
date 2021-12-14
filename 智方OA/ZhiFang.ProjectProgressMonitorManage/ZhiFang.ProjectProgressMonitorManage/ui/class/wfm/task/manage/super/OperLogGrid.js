/**
 * 任务操作记录列表
 * @author Jcall
 * @version 2018-08-02
 */
Ext.define('Shell.class.wfm.task.manage.super.OperLogGrid',{
    extend: 'Shell.ux.grid.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    title:'任务操作记录列表',
    width:500,
    height:800,
	
  	//获取数据服务路径
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskOperLogByHQL?isPlanish=true',
	//修改服务地址
	editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskOperLogByField',
	//删除数据服务路径
	delUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPTaskOperLog',
	//默认排序字段
	defaultOrderBy: [{ property: 'PTaskOperLog_DataAddTime', direction: 'ASC' }],
  	
  	//默认加载数据
	defaultLoad:false,
	//默认选中数据
	autoSelect: false,
	//是否启用序号列
	hasRownumberer: false,
	
	//是否启用刷新按钮
	hasRefresh: true,
	//是否启用保存按钮
	hasSave: true,
	
	plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
	
	//任务ID
	TaskId:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		if(me.TaskId){
			me.defaultWhere = 'ptaskoperlog.PTaskID=' + me.TaskId;
			me.onSearch();
		}
	},
	initComponent:function(){
		var me = this;
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	//创建数据列
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'数据新增时间',dataIndex:'PTaskOperLog_DataAddTime',width:135,
			style:'font-weight:bold;color:white;background:#169ada;',
			defaultRenderer:true,editor:{}
		},{
			text:'操作人ID',dataIndex:'PTaskOperLog_OperaterID',width:150,defaultRenderer:true,hidden:true
		},{
			text:'操作人',dataIndex:'PTaskOperLog_OperaterName',width:60,defaultRenderer:true,
			style:'font-weight:bold;color:white;background:#169ada;',
			editor:{
				xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp',
				listeners:{
					blur:function(field,e){
						return false;
					},
					check: function(p,record) {
						var Id = record.get('HREmployee_Id');
						var CName = record.get('HREmployee_CName');
						
						var records = me.getSelectionModel().getSelection();
						records[0].set({
							PTaskOperLog_OperaterID:Id,
							PTaskOperLog_OperaterName:CName
						});
						
						this.setValue(CName);
						me.getView().refresh();
					}
				}
			}
		},{
			text:'操作类型',dataIndex:'PTaskOperLog_PTaskOperTypeID',width:60,renderer:function(v,meta){
				var OperTypeInfo = JShell.WFM.GUID.getInfoByGUID('TaskStatus',v) || {};
				var OperTypeName = OperTypeInfo.text || '';
				
				if(OperTypeInfo.bgcolor){
					meta.style = 'color:' + OperTypeInfo.bgcolor;
				}
				
				return OperTypeName;
			}
		},{
			text:'操作说明',dataIndex:'PTaskOperLog_OperateMemo',width:300,sortable:false,menuDisabled:false,
			style:'font-weight:bold;color:white;background:#169ada;',
			defaultRenderer:true,editor:{}
		},{
			text:'操作时间',dataIndex:'PTaskOperLog_OperateTime',width:135,defaultRenderer:true,editor:{},
			style:'font-weight:bold;color:white;background:#169ada;',
			defaultRenderer:true,editor:{},hidden:true
		},{
			text:'主键ID',dataIndex:'PTaskOperLog_Id',width:150,isKey:true,hidden:true
		},{
			text:'任务ID',dataIndex:'PTaskOperLog_PTaskID',width:150,defaultRenderer:true,hidden:true
		}];
		
		return columns;
	},
	//保存信息
	onSaveClick:function(){
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;
		
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		me.saveErrorInfoList = [];

		for(var i = 0; i < len; i++) {
			var rec = records[i];
			me.onSaveOne(rec,i);
		}
	},
	//保存单个文件
	onSaveOne:function(record,index){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id:record.get('PTaskOperLog_Id'),
				OperaterID:record.get('PTaskOperLog_OperaterID'),
				OperaterName:record.get('PTaskOperLog_OperaterName'),
				OperateTime:JShell.Date.toServerDate(record.get('PTaskOperLog_OperateTime')) || null,
				OperateMemo:record.get('PTaskOperLog_OperateMemo'),
				DataAddTime:JShell.Date.toServerDate(record.get('PTaskOperLog_DataAddTime')) || null
			},
			fields: 'Id,OperaterID,OperaterName,OperateTime,OperateMemo,DataAddTime'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data){
				if(data.success) {
					//record.set(me.DelField, true);
					record.commit();
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					me.saveErrorInfoList.push(data.msg);
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0){
						JShell.Msg.alert('保存成功',null,1000);
						//me.onSearch();
					}else{
						JShell.Msg.error(me.saveErrorInfoList.join('</br>'));
					}
				}
			});
		}, 100 * index);
	}
});