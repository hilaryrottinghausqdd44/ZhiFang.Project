/**
 * 项目列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.project.Grid',{
    extend: 'Shell.ux.model.ExtraGrid',
	requires: ['Ext.ux.CheckColumn'],
    
    title:'项目列表',
    
    /**是否使用字段*/
	IsUseField:'FProject_IsUse',
	/**其他信息模板路径*/
	OtherMsgModelUrl:'',
	/**信息字段*/
	MsgField:'OtherMsg',
	/**项目表主体名*/
	PrimaryName:'F_Project',
	
  	/**获取数据服务路径*/
	selectUrl:'/BaseService.svc/ST_UDTO_SearchFProjectByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/BaseService.svc/ST_UDTO_UpdateFProjectByField',
	/**删除数据服务路径*/
	delUrl:'/BaseService.svc/ST_UDTO_DelFProject',
	/**默认排序字段*/
	defaultOrderBy: [{ property: 'FProject _DataAddTime', direction: 'ASC' }],
  	
  	initComponent: function() {
		var me = this;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','add','del','save','->',{
			xtype:'button',
			iconCls:'button-show',
			text:'查看任务',
			tooltip:'<b>查看任务</b>',
			handler:function(){me.openTaskApp();}
		},{
			xtype:'button',
			iconCls:'button-show',
			text:'交流信息',
			tooltip:'<b>查看互动交流信息</b>',
			handler:function(){me.openInteractionList();}
		},'-',{
			xtype:'button',
			iconCls:'button-config',
			text:'设置模板',
			tooltip:'<b>设置其他信息模板</b>',
			handler:function(){me.openOtherMsgForm();}
		}];
		
		me.callParent(arguments);
	},
  	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		var columns = [{
			text:'项目类别',dataIndex:'FProject_Type_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'FProject_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目负责人',dataIndex:'FProject_ProjectLeader_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'进场时间',dataIndex:'FProject_EntryTime',width:100,
			isDate:true
		},{
			text:'项目状态',dataIndex:'FProject_Status_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'目前进度',dataIndex:'FProject_Pace_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'FProject_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'创建时间',dataIndex:'FProject_DataAddTime',width:130,
			isDate:true,hasTime:true
		},{
			text:'主键ID',dataIndex:'FProject_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	openTaskApp:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		JShell.Win.open('Shell.class.wfm.task.App',{
			resizable:false,
			title:records[0].get('FProject_CName') + '-任务',
			FileUrl:me.FileUrl,
			Content:me.OtherMsgModelContent,
			/**项目表主体名*/
		    PrimaryName:me.PrimaryName,
		    /**项目表数据ID*/
			PrimaryID:records[0].get(me.PKField),
			/**项目名称*/
			ProjectName:records[0].get('FProject_CName')
		}).show();
	},
	openInteractionList:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		JShell.Win.open('Shell.class.sysbase.interaction.App',{
			resizable:false,
			/**项目表主体名*/
		    PrimaryName:me.PrimaryName,
		    /**项目表数据ID*/
			PrimaryID:records[0].get(me.PKField)
		}).show();
	}
});