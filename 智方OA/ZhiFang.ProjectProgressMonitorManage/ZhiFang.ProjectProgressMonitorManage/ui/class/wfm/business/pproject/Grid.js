/**
 * 项目监控
 * @author longfc
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.pproject.Grid', {
	extend: 'Shell.class.wfm.business.pproject.basic.Grid',
	title: '项目监控',
	/**是否是标准项目列,0:否，1:是*/
	IsStandard:0,
    /**项目阶段列表*/
	dictList: [
		{'Id':'4979100207557443548', 'Name':'未开始', 'BGColor':'#bfbfbf', 'FontColor' :'#ffffff'},
		{'Id':'5312501669507423819', 'Name':'开始准备', 'BGColor':'#f4c600', 'FontColor' :'#ffffff'}, 
		{'Id':'4681988131726092027', 'Name':'已完成准备',  'BGColor':'#aad08f', 'FontColor' :'#ffffff'},
		{'Id':'4780943879366294437', 'Name':'已进场',  'BGColor':'#7cba59', 'FontColor' :'#ffffff'},
		{'Id':'5618820972093211125', 'Name':'开始试运行',  'BGColor':'#7dc5eb', 'FontColor' :'#ffffff'},
		{'Id':'5451113863253901111', 'Name':'已上线',  'BGColor':'#17abe3', 'FontColor' :'#ffffff'}, 
		{'Id':'5551657554650328324', 'Name':'已验收',  'BGColor':'#be8dbd', 'FontColor' :'#ffffff'}, 
		{'Id':'5572276754094614738', 'Name': '暂停',  'BGColor':'#d6204b', 'FontColor' :'#ffffff'}, 
		{'Id':'4746723532904316067', 'Name':'终止',  'BGColor':'#a4579d', 'FontColor' :'#ffffff'}
	],
    PhaseEnum : {},
	PhaseFColorEnum : {},
	PhaseBColorEnum: {},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.changePhaseNameColor();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(1,0,{
			text: '用户名称',dataIndex: 'PProject_PClientName',width: 200,sortable: true,menuDisabled: true
		},{
			text: '项目类型',dataIndex: 'PProject_TypeID',width: 150,
			sortable: true,menuDisabled: true,
            renderer: function(value, meta) {
				var v = value;
				if(me.ItemTypeEnum != null){
					v = me.ItemTypeEnum[value];
				}
				return v;
			}
		},{
			text: '进度',dataIndex: 'PProject_PaceEvalName',sortable: false,menuDisabled: true,width: 55
		},{
			text: '延期状态',dataIndex: 'PProject_DelayLevelName',width: 80,
			sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			xtype: 'actioncolumn',text: '项目任务',align: 'center',width: 60,
			hideable: false,sortable: false,menuDisabled: true,style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PProject_Id');
					//项目类型
					var TypeID= rec.get('PProject_TypeID');
					if(!TypeID){
						JShell.Msg.error("选择的行项目类型类型为空!");
						return;
					}
                    if(id && TypeID){
						me.onProjectTaskApp(id,TypeID,rec);
					}
				}
			}]
		},{ 
			xtype: 'actioncolumn',text: '项目进度',align: 'center',
			width: 60,hideable: false,sortable: false,hidden:me.hasprogress,
			menuDisabled: true,style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand', //
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onOpenScheduleApp(rec);
				}
			}]
		});
		
		columns.splice(7,0,{
			xtype: 'actioncolumn',text: '工作记录',align: 'center',
			width: 70,hideable: false,sortable: false,
			menuDisabled: true,style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					//选中行号为num的数据行
					if (rowIndex >= 0) {
						me.getSelectionModel().select(rowIndex);
					}
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PProject_Id');
					me.onOpenExecuteApp(id);
				}
			}]
		},{
			text: '项目阶段',dataIndex: 'PProject_PhaseID',sortable: false,menuDisabled: true,width: 80,
			renderer:function(value,meta,record){
				var v = value;		
				if(me.PhaseEnum != null)
					v = me.PhaseEnum[value];
				var bColor = "";
				if(me.PhaseBColorEnum != null)
					bColor = me.PhaseBColorEnum[value];
				var fColor = "";
				if(me.PhaseFColorEnum != null)
					fColor = me.PhaseFColorEnum[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		});
		
		columns.push({
			text: '初始风险等级',dataIndex: 'PProject_RiskLevelName',width: 85,
			sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '动态风险等级',dataIndex: 'PProject_DynamicRiskLevelName',
			width: 85,sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '签署时间',dataIndex: 'PProject_SignDate',
			width: 90,sortable: false,menuDisabled: true,isDate: true,hasTime: false
		}, {
			text: '进场时间',dataIndex: 'PProject_EntryTime',width: 90,sortable: false,
			menuDisabled: true,isDate: true,hasTime: false
		}, {
			text: '预计总开始时间',dataIndex: 'PProject_EstiStartTime',width: 100,
			sortable: false,menuDisabled: true,isDate: true,hasTime: false
		}, {
			text: '预计总完成时间',dataIndex: 'PProject_EstiEndTime',width: 100,
			sortable: false,menuDisabled: true,isDate: true,hasTime: false
		}, {
			text: '实际完成时间',dataIndex: 'PProject_EndTime',
			width: 95,sortable: false,menuDisabled: true,isDate: true,hasTime: false
		},  {
			text: '延期天数',dataIndex: 'PProject_ScheduleDelayDays',
			type: 'number',xtype: 'numbercolumn',width: 65,
			align: 'center',sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '延期百分比',dataIndex: 'PProject_ScheduleDelayPercent',
			width: 70,align: 'center',sortable: false,menuDisabled: true,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && v != 0) {
					v = Ext.util.Format.number(v, '0.00');
					v = parseFloat(v) * 100 + "%";
				} else {
					v = "";
				}
				return v;
			},
		}, {
			text: '预计总工作量',dataIndex: 'PProject_EstiAllDays',type: 'number',
			xtype: 'numbercolumn',width: 90,align: 'center',sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '实际总工作量',dataIndex: 'PProject_AllDays',type: 'number',
			xtype: 'numbercolumn',width: 90,align: 'center',sortable: false,defaultRenderer: true
		}, {
			text: '超工作量天数',dataIndex: 'PProject_MoreWorkDays',type: 'number',
			xtype: 'numbercolumn',width: 85,align: 'center',sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '超工作量百分比',dataIndex: 'PProject_MoreWorkPercent',width: 95,
			align: 'center',sortable: false,menuDisabled: true,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && v != 0) {
					v = Ext.util.Format.number(v, '0.00');
					v = parseFloat(v) * 100 + "%";
				} else {
					v = "";
				}
				return v;
			}
		}, {
			text: '实施负责人',dataIndex: 'PProject_ProjectLeader',width: 85,
			sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '实施人员',dataIndex: 'PProject_ProjectExec',
			width: 85,sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '销售人员',dataIndex: 'PProject_Principal',width: 85,
			sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '进度监控人',dataIndex: 'PProject_PhaseManager',width: 85,
			sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '项目类型',dataIndex: 'PProject_TypeID',
			width: 85,hidden:true,sortable: false,menuDisabled: true,defaultRenderer: true
		}, {
			text: '预计总工作量',dataIndex: 'PProject_EstiWorkload',
			width: 85,hidden:true,sortable: false,menuDisabled: true,defaultRenderer: true
		});
		return columns;
	},
	/**处理交流*/
	onOpenExecuteApp: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth - 220;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.wfm.business.pproject.interaction.AppExt', {
			fObjectValue: id,
			PK: id,
            ProjectID:id,
			height:height,
			width:maxWidth
		}).show();
	},
	/**新增项目任务*/
	onProjectTaskApp: function(id,TypeID,rec) {
		var me = this;
	
		var maxWidth = document.body.clientWidth - 200;
		var height = document.body.clientHeight - 60;
		
		var PClientName= rec.get('PProject_CName');
		//项目计划开始时间
		var EstiStartTime = rec.get("PProject_EstiStartTime");
		//项目计划完成时间
        var EstiEndTime = rec.get("PProject_EstiEndTime");
        //预计总工作量
        var EstiWorkload = rec.get("PProject_EstiWorkload");
        var TypeName = '',titletext='',str='';
		if(me.ItemTypeEnum != null){
			TypeName = me.ItemTypeEnum[TypeID];
		}
		titletext=PClientName;
       	if(!titletext){
       		titletext='项目任务';
		}
		JShell.Win.open('Shell.class.wfm.business.pproject.standard.ItemGrid', {
			ProjectID: id,
			TypeID:TypeID,
			SUB_WIN_NO: '101',
			title:titletext,
			IsStandard:me.IsStandard,
			hasTask:true,
			height:height,
			width:maxWidth,
			ItemEstiStartTime:EstiStartTime,
			ItemEstiEndTime:EstiEndTime,
			EstiWorkload:EstiWorkload,
			TypeName:TypeName,
			defaultWhere:'pprojecttask.IsStandard='+me.IsStandard,
			listeners: {
				save:function(grid){
					grid.onSearch();
				},
				beforeclose:function( grid,  eOpts ){
					var edit = grid.getPlugin('NewsGridEditing');  
                    edit.cancelEdit();  
				}
			}
		}).show();
	},
	/**改变查询条件*/
	changeWhere:function(){
		var me = this;
		var params = me.params || [];
		params.push('pproject.IsStandard=0');
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
	},
	/**项目阶段颜色*/
	changePhaseNameColor:function(){
		var me=this;
			me.PhaseEnum = {};
			me.PhaseFColorEnum = {};
			me.PhaseBColorEnum = {};

		Ext.Array.each(me.dictList, function(obj, index) {
			var style = ['font-weight:bold;text-align:center;'];
			if(obj.FontColor) {
				me.PhaseFColorEnum[obj.Id] = obj.FontColor;
			}
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor); //background-
				me.PhaseBColorEnum[obj.Id] = obj.BGColor;
			}
			me.PhaseEnum[obj.Id] = obj.Name;
			
		});
	}
});