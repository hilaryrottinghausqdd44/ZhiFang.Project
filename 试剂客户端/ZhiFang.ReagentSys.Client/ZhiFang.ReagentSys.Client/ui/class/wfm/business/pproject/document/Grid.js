/**
 * 文档
 * @author longfc
 * @version 2017-04-01
 */
Ext.define('Shell.class.wfm.business.pproject.document.Grid', {
	extend: 'Shell.class.wfm.business.pproject.document.EditGrid',
	title: '文档',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	/**获取数据服务路径*/
    selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectDocumentByHQL?isPlanish=true',
	/**获取数据服务路径*/
//	selectUrl: '/ui/class/wfm/business/pproject/testData/document.json',
	/**删除数据服务路径*/
	delUrl: '',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PProjectDocument_DataAddTime',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: true,

	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**序号列宽度*/
	rowNumbererWidth: 35,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 250,

	/**工具栏项的显示值*/
	initItemsValue: null,
    ProjectID:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.initItemsValue) {
			me.initButtonToolbarItemsValue(me.initItemsValue);
		}
		me.on({
            itemdblclick: function (view, record) {
                var id = record.get(me.PKField);
                me.openEditProjectForm(id);
            }
        });
	},
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		if(me.hasButtontoolbar) me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = me.buttonToolbarItems || [];
		//var textfieldStyle = "border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: solid;";
		items.push({
			fieldLabel: '项目名称',
			labelWidth: 65,
			readOnly: true,
			width: 285,
			xtype: 'displayfield',
			name: 'CName',
			itemId: 'CName'
		});
		items.push({
			fieldLabel: '进场时间',
			labelWidth: 65,
			readOnly: true,
			width: 145,
			xtype: 'displayfield',
			name: 'EntryTime',
			itemId: 'EntryTime'
		});
		items.push({
			fieldLabel: '进度状态',
			labelWidth: 65,
			readOnly: true,
			width: 120,
			xtype: 'displayfield',
			name: 'PaceEvalName',
			itemId: 'PaceEvalName'
		});
		items.push({
			fieldLabel: '实施负责人',
			labelWidth: 75,
			readOnly: true,
			width: 155,
			xtype: 'displayfield',
			name: 'ProjectLeader',
			itemId: 'ProjectLeader'
		});
		items.push({
			fieldLabel: '销售负责人',
			labelWidth: 75,
			readOnly: true,
			width: 155,
			xtype: 'displayfield',
			name: 'Principal',
			itemId: 'Principal'
		});
		items.push({
			fieldLabel: '进度监督人',
			labelWidth: 75,
			readOnly: true,
			width: 155,
			xtype: 'displayfield',
			name: 'PhaseManager',
			itemId: 'PhaseManager'
		});
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '项目任务',
			dataIndex: 'PProjectDocument_PProject_CName',
			width: 215,
			sortable: false,
			menuDisabled: true
		}, {
			xtype: 'actioncolumn',
			text: '指',
			align: 'center',
			width: 35,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:#00F;',
			items: [{
				getClass: function(v, meta, record) {
					var id=record.get('PProjectDocument_DocumentLink');
			    	if(id){
			    		return 'button-show hand';
			    	}else{
			    		return '';
			    	}
				},
			    handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id=rec.get('PProjectDocument_DocumentLink');
					me.showFFileById(id);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '工作记录',
			align: 'center',
			width: 75,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PProjectDocument_Id');
					var ProjectTaskID = rec.get('PProjectDocument_ProjectTaskID');
					me.onOpenExecuteApp(id,ProjectTaskID);
				}
			}]
		}, {
			text: '标准',dataIndex: 'PProjectDocument_PProject_StandWorkload',width: 50,sortable: true,menuDisabled: false
		}, {
			text: '计划',
			dataIndex: 'PProjectDocument_PProject_EstiWorkload',
			width: 50,sortable: true,menuDisabled: false,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('EstiWorkload', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '实际',dataIndex: 'PProjectDocument_PProject_DynamicRemainingWorkDays',
			width: 50,sortable: true,menuDisabled: false,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('Workload', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '主键ID',dataIndex: 'PProjectDocument_Id',isKey: true,hidden: true,hideable: false
		}, {
			text: 'DocumentLink',dataIndex: 'PProjectDocument_DocumentLink',hidden: true,hideable: false
		}];
		columns.push({
			text: '文档模板',
			dataIndex: 'PProjectDocument_DocumentName',
			style: 'font-weight:bold;color:white;background:#00F;',
			width: 135,
			hideable: false,
			sortable: false,
			menuDisabled: true
		}, {
			xtype: 'actioncolumn',
			text: '项目文档',
			align: 'center',
			width: 90,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand', //
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var Id = rec.get(me.PKField);
		          	me.openEditForm(Id);
				}
			}]
		});
		return columns;
	},
	/**处理交流*/
	onOpenExecuteApp: function(id,ProjectTaskID) {
		var me = this;
		var maxWidth = document.body.clientWidth - 220;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.wfm.business.pproject.interaction.AppExt', {
			fObjectValue: id,
			PK: id,
            ProjectID:id,
            ProjectTaskID:ProjectTaskID,
			height:height,
			width:maxWidth
		}).show();
	},
	/**创建功能按钮栏Items*/
	initButtonToolbarItemsValue: function(data) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar && data) {
			var CName = buttonsToolbar.getComponent('CName');
			var EntryTime = buttonsToolbar.getComponent('EntryTime');
			var PaceEvalName = buttonsToolbar.getComponent('PaceEvalName');
			var ProjectLeader = buttonsToolbar.getComponent('ProjectLeader');
			var Principal = buttonsToolbar.getComponent('Principal');
			var PhaseManager = buttonsToolbar.getComponent('PhaseManager');

			if(CName && data.CName) CName.setValue(data.CName);
			if(EntryTime && data.EntryTime) {
				data.EntryTime = Ext.util.Format.date(data.EntryTime, 'Y-m-d');
				EntryTime.setValue(data.EntryTime)
			}

			if(PaceEvalName && data.PaceEvalName) PaceEvalName.setValue(data.PaceEvalName);
			if(ProjectLeader && data.ProjectLeader) ProjectLeader.setValue(data.ProjectLeader);
			if(Principal && data.Principal) Principal.setValue(data.Principal);
			if(PhaseManager && data.PhaseManager) Principal.setValue(data.PhaseManager);
		}
	},
    /**项目进度*/
	openEditProjectForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.schedule.Form', {
			SUB_WIN_NO:'101',//内部窗口编号
            //resizable:false,
			title:'进度信息',
			PK:id
		}).show();
	}
});