/**
 * 项目进度管理
 * @author longfc
 * @version 2017-04-01
 */
Ext.define('Shell.class.wfm.business.pproject.schedule.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '项目进度管理',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	/**获取数据服务路径*/
    selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskByHQL?isPlanish=true',
    /**获取数据服务路径*/
    //	selectUrl: '/ui/class/wfm/business/pproject/testData/schedule.json',
	/**删除数据服务路径*/
	delUrl: '',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PProjectTask_DispOrder',
		direction: 'ASC'
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
	/**计划开始时间*/
	EstiStartTime: null,
	/**计划结束时间*/
	EstiEndTime: null,
	/**工具栏项的显示值*/
	initItemsValue: null,
	/**月份列对应的颜色00CED1*/
	MonthColor: {
		1: { BGColor: "#008B8B" },
		2: { BGColor: "#00688B" },
		3: { BGColor: "#DAA520" },
		4: { BGColor: "#8B7355" },
		5: { BGColor: "#8B6508" },
		6: { BGColor: "#00CED1" },
		7: { BGColor: "#00C5CD" },
		8: { BGColor: "#20B2AA" },
		9: { BGColor: "#B8860B" },
		10: { BGColor: "#B22222" },
		11: { BGColor: "#CDAA7D" },
		12: { BGColor: "#CD950C" }
	},
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
                me.openEditForm(id,record);
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
		items.push({
			fieldLabel: '项目名称',labelWidth: 65,
			readOnly: true,width: 285,xtype: 'displayfield',name: 'CName',itemId: 'CName'
		});
		items.push({
			fieldLabel: '进场时间',labelWidth: 65,readOnly: true,
			width: 145,xtype: 'displayfield',name: 'EntryTime',itemId: 'EntryTime'
		});
		items.push({
			fieldLabel: '进度状态',labelWidth: 65,
			readOnly: true,width: 120,xtype: 'displayfield',name: 'PaceEvalName',itemId: 'PaceEvalName'
		});
		items.push({
			fieldLabel: '实施负责人',labelWidth: 75,readOnly: true,width: 155,
			xtype: 'displayfield',name: 'ProjectLeader',itemId: 'ProjectLeader'
		});
		items.push({
			fieldLabel: '销售负责人',labelWidth: 75,readOnly: true,width: 155,
			xtype: 'displayfield',name: 'Principal',itemId: 'Principal'
		});
		items.push({
			fieldLabel: '进度监督人',labelWidth: 75,readOnly: true,
			width: 155,xtype: 'displayfield',name: 'PhaseManager',itemId: 'PhaseManager'
		});
		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '实施任务',
			dataIndex: 'PProjectTask_CName',
			width: 195,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v=value;
				if(!record.get("PProjectTask_PTaskID") && record.get("PProjectTask_Id") ){
					meta.style =  'color:#0033FF;';
				}
				if(record.get("PProjectTask_PTaskID") && record.get("PProjectTask_Id") ){
					v = "<div align='right'>"+ value+'</div>';  
				}
				return v;
			}
		}, {
			xtype: 'actioncolumn',
			text: '指',
			align: 'center',
			width: 30,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:#00F;',
			items: [{
				getClass: function(v, meta, record) {
					var id=record.get('PProjectTask_TaskHelp');
			    	if(id){
			    		return 'button-show hand';
			    	}else{
			    		return '';
			    	}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id=rec.get('PProjectTask_TaskHelp');
					me.showFFileById(id);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '工作记录',
			align: 'center',
			width: 70,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-interact hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PProjectTask_Id');
					me.onOpenExecuteApp(id,me.ProjectID);
				}
			}]
		}, {
			text: '标准',
			dataIndex: 'PProjectTask_StandWorkload',
			width: 50,
			sortable: true,
			menuDisabled: true
		}, {
			text: '计划',
			dataIndex: 'PProjectTask_EstiWorkload',
			width: 50,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					focus: function(com, e, eOpts) {
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('PProjectTask_EstiWorkload', newValue);
						me.getView().refresh();
					}
				}
			}
		}, {
			text: '实际',
			//style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'PProjectTask_Workload',
			width: 50,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('PProjectTask_Workload', newValue);
						me.getView().refresh();
					}
				}
			}
		},{
			menuDisabled: false,text: '实际开始时间',hidden:true,dataIndex: 'PProjectTask_StartTime',width: 85,sortable: false,
			type: 'date',xtype: 'datecolumn',format: 'Y-m-d', isDate: true
		}, {
			text: '实际完成时间',dataIndex: 'PProjectTask_EndTime',hidden:true,width: 85,sortable: false,
			menuDisabled: false,type: 'date',xtype: 'datecolumn',format: 'Y-m-d', isDate: true
		},{
			text: '剩余量',dataIndex: 'PProjectTask_RemainWorkDays',width: 50,sortable: true,menuDisabled: true
		}, {
			text: '主键ID',dataIndex: 'PProjectTask_Id',isKey: true,hidden: true,hideable: false
		},{text: '项目任务指导书',menuDisabled: false,dataIndex: 'PProjectTask_TaskHelp',hidden: true,sortable: false, defaultRenderer: true},
		{text: '状态',menuDisabled: false,dataIndex: 'PProjectTask_Status',hidden: true,sortable: false, defaultRenderer: true}];
		//动态生成计划日期范围内的天数列
		columns = me.createDaysColumns(columns);
		columns.push({
			text: '实施任务',dataIndex: 'PProjectTask_CName',width: 195,
			sortable: false,menuDisabled: true,
			renderer: function(value, meta, record) {
				var v=value;
				if(!record.get("PProjectTask_PTaskID") && record.get("PProjectTask_Id") ){
					meta.style =  'color:#0033FF;';
				}
				if(record.get("PProjectTask_PTaskID") && record.get("PProjectTask_Id") ){
					v = "<div align='right'>"+ value+'</div>';  
				}
				return v;
			}
		},{
			text: 'DispOrder',hidden:true,dataIndex: 'PProjectTask_DispOrder',width: 50,sortable: true,menuDisabled: true
		},{
			text: 'PProjectTask_PTaskID',hidden:true,dataIndex: 'PProjectTask_PTaskID',width: 50,sortable: true,menuDisabled: true
		});
		return columns;
	},

	/**动态生成计划日期范围内的天数列*/
	createDaysColumns: function(columns) {
		var me = this;
		if(me.EstiStartTime && me.EstiEndTime) {
			var start = Ext.util.Format.date(me.EstiStartTime, 'Y-m-d');
			var end = Ext.util.Format.date(me.EstiEndTime, 'Y-m-d');
			var endDate = JcallShell.Date.getDate(end);
			var startDate = JcallShell.Date.getDate(start);
			var currentDate = startDate;
			//一个月为一个分组
			var monthTempArr = [];
			var monthColumn = null;
			var endDateStr = Ext.util.Format.date(endDate, 'Y-m-d');
			var status=0;
			while(currentDate <= endDate) {
				//一个月为一个分组
				var month = currentDate.getMonth() + 1;
				var endMonth = endDate.getMonth() + 1;
				var curDateStr = Ext.util.Format.date(currentDate, 'Y-m-d');
				var curDayColumn = {
					text: currentDate.getDate(),
					width: 35,
					dataIndex: curDateStr,
					sortable: false,
					menuDisabled: true,
					renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
						meta.tdAttr = 'data-qtip="' + value + '"';
						return value;
					}
				};
				if(monthTempArr.indexOf(month) == -1) {
					monthTempArr.push(month);
					if(monthColumn != null) {
						columns.push(monthColumn);
					}
					var text = currentDate.getFullYear() + "-" + month + "";
					monthColumn = {
						text: text,
						style: 'font-weight:bold;color:white;background:' + me.MonthColor[month].BGColor + ';',
						columns: []
					};
				}
				monthColumn.columns.push(curDayColumn);
				currentDate = JcallShell.Date.getNextDate(currentDate, 1);
				//日期范围内的最后一个月天数集合
				if(curDateStr == endDateStr) {
					if(monthColumn != null) {
						columns.push(monthColumn);
					}
				}
				
			}
		}
		return columns;
	},
		/**处理交流*/
	onOpenExecuteApp: function(id,ProjectID) {
		var me = this;
		var maxWidth = document.body.clientWidth - 220;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.wfm.business.pproject.interaction.AppExt', {
			fObjectValue: id,
			PK: id,
            ProjectID:ProjectID,
            ProjectTaskID:id,
			height:height,
			width:maxWidth
		}).show();
	},
     /**根据id显示指导文档*/
	showFFileById: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.qms.file.basic.FFileDetailedForm', {
			//resizable: false,
			PK: id ,
			width:800,
			title:'实施任务指导书',
			itemId: 'FFileOperationForm',
			FFileId : id
		}).show();
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
		if(me.ProjectID) {
			params.push("pprojecttask.PProject.Id=" + me.ProjectID + "");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
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
    /**查询合同信息*/
	openEditForm:function(id,record){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.schedule.Form', {
			SUB_WIN_NO:'101',//内部窗口编号
            //resizable:false,
			title:'进度信息',
			PK:id,
			listeners: {
				save:function(p){
                    //给实际赋值
                    var Workload=p.getComponent('PProjectTask_Workload');
                    record.set('PProjectTask_Workload',Workload.getValue());
                    var RemainWorkDays=p.getComponent('PProjectTask_RemainWorkDays');
                    record.set('PProjectTask_RemainWorkDays',RemainWorkDays.getValue());
                    var StartTime=p.getComponent('PProjectTask_StartTime');
                    record.set('PProjectTask_StartTime',StartTime.getValue());
                    var EndTime=p.getComponent('PProjectTask_EndTime');
                    record.set('PProjectTask_EndTime',EndTime.getValue());
                    record.commit();
					p.close();
				}
			}
		}).show();
	},
	
    /**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this,
			columns = me._resouce_columns || [],
			length = columns.length,
			fields = [];

		for (var i = 0; i < length; i++) {
			if (columns[i].dataIndex) {
				var obj = isString ? columns[i].dataIndex : {
					name: columns[i].dataIndex,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		return fields;
	},
	getDays:function (st, et) {  
		var me=this;
	    var retArr = [];  
	    // 获取开始日期的年，月，日  
	    var yyyy = st.getFullYear(),  
	        mm = st.getMonth(),  
	        dd = st.getDate();  
	    // 循环  
	    while (st.getTime() != et.getTime()) {  
	        // 使用dd++进行天数的自增  
	        st = new Date(yyyy, mm,  dd++);  
	        var stStr =JcallShell.Date.toString(st, true);
	        retArr.push(stStr);
	    }  
	     var etStr =JcallShell.Date.toString(et, true);
	    // 将结束日期的天放进数组  
	    retArr.push(etStr); 
	    
	    return retArr;
	} ,
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this,list=[],result={};
		var Parr=[],arr=[];
		for(var i=0;i<data.list.length;i++){
			if(!data.list[i].PProjectTask_PTaskID){
				Parr.push(data.list[i]);
			}
		}
		Parr.sort(me.compare('PProjectTask_DispOrder'));
		for(var i=0;i<data.list.length;i++){
			if(data.list[i].PProjectTask_PTaskID){
				arr.push(data.list[i]);
			}
		}
		arr.sort(me.compare('PProjectTask_DispOrder'));
		for(var i=0;i<Parr.length;i++){
			list.push(Parr[i]);
			for(var j=0;j<arr.length;j++){
				if(arr[j].PProjectTask_PTaskID==Parr[i].PProjectTask_Id){
					list.push(arr[j]);
				}
	        }
		}
		result.list = list;	
		return result;
	},
	compare : function(property){
	    return function(a,b){
	        var value1 = a[property];
	        var value2 = b[property];
	        return value1 - value2;
	    }
	}
});