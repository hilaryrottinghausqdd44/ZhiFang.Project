/**
 * 项目进度维护
 * @author liangyl
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.pproject.schedule.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '项目进度信息',
	width: 280,
	height: 230,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPProjectTask',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectTaskByField',
    /**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 90,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push( {
			name: 'PProjectTask_StartTime',
			itemId: 'PProjectTask_StartTime',
			xtype: 'datefield',
			format: 'Y-m-d',
		    fieldLabel: "实际开始时间"
		},{
			fieldLabel: '动态完成时间',
			name: 'PProjectTask_EndTime',
			itemId: 'PProjectTask_EndTime',
			xtype: 'datefield',value:0,
			format: 'Y-m-d'
		},  {
			fieldLabel: '动态剩余工作量',
			name: 'PProjectTask_RemainWorkDays',
			itemId: 'PProjectTask_RemainWorkDays',
			xtype:'numberfield',value:0,
			labelAlign: 'right'
		}, {
			fieldLabel: '实际人工作量',
			name: 'PProjectTask_AllDays',
			itemId: 'PProjectTask_AllDays',
			xtype:'numberfield',value:0,
			labelAlign: 'right'
		},{
			fieldLabel: '实际工作量',
			name: 'PProjectTask_Workload',
			itemId: 'PProjectTask_Workload',
			xtype:'numberfield',value:0,
			emptyText:'必填项',allowBlank:false,
			labelAlign: 'right'
		}, {
			fieldLabel: '主键ID',
			name: 'PProjectTask_Id',
			hidden: true
		});
		return items;
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
	
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {};
		if(values.PProjectTask_StartTime){
			entity.StartTime = JShell.Date.toServerDate(values.PProjectTask_StartTime);
		}
		if(values.PProjectTask_EndTime){
			entity.EndTime = JShell.Date.toServerDate(values.PProjectTask_EndTime);
		}
		if(values.PProjectTask_RemainWorkDays){
			entity.RemainWorkDays = values.PProjectTask_RemainWorkDays;
		}
		if(values.PProjectTask_AllDays){
			entity.AllDays = values.PProjectTask_AllDays;
		}
		if(values.PProjectTask_Workload){
			entity.Workload =values.PProjectTask_Workload;
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['StartTime', 'EndTime', 'RemainWorkDays', 'AllDays','Workload','Id'];
		entity.fields = fields.join(',');
		if(values.PProjectTask_Id != '') {
			entity.entity.Id = values.PProjectTask_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		if(data.PProjectTask_EndTime) data.PProjectTask_EndTime = JShell.Date.getDate(data.PProjectTask_EndTime);
		if(data.PProjectTask_StartTime) data.PProjectTask_StartTime = JShell.Date.getDate(data.PProjectTask_StartTime);
		return data;
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	}
});