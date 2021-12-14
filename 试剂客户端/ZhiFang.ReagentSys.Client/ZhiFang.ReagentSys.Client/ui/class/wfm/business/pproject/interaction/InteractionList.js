/**
 * 互动信息
 * @author liangyl
 * @version 2017-03-21
 */
Ext.define('Shell.class.wfm.business.pproject.interaction.InteractionList', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.RowExpander',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '互动列表 ',
	width: 800,
	height: 500,
	/**默认展开内容*/
	defaultShowContent: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**依某一业务对象ID获取该业务对象ID下的所有交流内容信息*/
	selectAllUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectTaskProgressByHQL?isPlanish=true',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectTaskProgressByHQL?isPlanish=true',
	/**附件对象名*/
	objectName: '',
	/**附件关联对象名*/
	fObejctName: 'PProject',
	/**附件关联对象主键*/
	fObjectValue: '',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID: false,

	/**默认加载*/
	defaultLoad: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用序号列*/
	hasRownumberer: false,

	/**默认每页数量*/
	defaultPageSize: 10,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10, 10],
		[20, 20],
		[50, 50],
		[100, 100],
		[200, 200]
	],
	/**依某一业务对象ID获取该业务对象ID下的所有交流内容信息开关*/
	loadAllData: false,
	/**某一话题的交流总回复数*/
	replyCount:0,
	/**项目ID*/
	ProjectID:null,
	ProjectTaskID:null,

    /**任务记录类型*/
	TaskTypeDict: [
		['日志', '日志'],
		['交流', '交流']],
	constructor: function(config) {
		var me = this;
		me.plugins = [{
			ptype: 'rowexpander',
			rowBodyTpl: [
//			    '<p>工作时间:{' + config.objectName + '_ExecuteTime} &nbsp; &nbsp;工作量:{' + config.objectName + '_Workload} &nbsp; &nbsp;任务记录类型:{'+config.objectName + '_TaskTypeDict} &nbsp; &nbsp;任务风险:{'+config.objectName + '_TaskRisk}</p>',
				'<p><b>内容:</b></p>',
				'<p>{' + config.objectName + '_TaskWorkInfo}</p>'
			]
		}];

		me.callParent(arguments);
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			load: function() {
				me.changeShowType(me.showType);
			},
			resize: function() {
				var gridWidth = me.getWidth();
				var width = gridWidth - me.columns[0].getWidth() - me.columns[1].getWidth() - me.columns[6].getWidth();
				me.columns[6].setWidth(width - 20);
			}
		});
	},

	initComponent: function() {
		var me = this;
		//me.initDefaultWhere();
		me.columns = me.createGridColumns();
		me.buttonToolbarItems = ['refresh', {
			xtype: 'checkbox',
			boxLabel: '展开内容',
			itemId: 'showContent',
			checked: me.defaultShowContent,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeShowType(newValue);
				}
			}
		}, {
			fieldLabel: '任务记录类型',name: 'PProjectTaskProgress_TaskTypeDict',itemId: 'PProjectTaskProgress_TaskTypeDict',
			emptyText: "任务记录类型",xtype: 'uxSimpleComboBox',labelWidth: 85,width: 170,
			labelAlign: 'right',hasStyle: true,allowBlank: false,
			data:me.TaskTypeDict,
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		},{
			width: 165,
			labelWidth: 65,
			labelAlign: 'right',
			fieldLabel: '发表时间',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		}, {
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		}];

		me.showType = me.defaultShowContent;
		me.defaultOrderBy = [{ property: me.objectName + '_DataAddTime', direction: 'DESC' }];
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '发表时间',
			dataIndex: me.objectName + '_DataAddTime',
			isDate: true,
			hasTime: true,
			flex:1
		},   {
			text: '任务记录类型',
			hideable: false,
			width: 100,
			dataIndex: me.objectName + '_TaskTypeDict'
		}, {
			text: '工作时间',
			dataIndex: me.objectName + '_ExecuteTime',
			isDate: true,
			width: 100,
			hideable: false
		},{
			text: '工作量',
			width: 100,
			hideable: false,
			dataIndex: me.objectName + '_Workload'
		}, {
			text: '任务风险',
			width: 100,
			hideable: false,
			dataIndex: me.objectName + '_TaskRisk'
		},{
			text: '发表人',
			width: 120,
			dataIndex: me.objectName + '_Register_CName',
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var isOwner = record.get(me.objectName + '_Register_Id') ==
					JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				var color = isOwner ? "color:green" : "color:#000";
			    var v = '<b style=\'' + color + '\'>' + value + '</b>';
				if(v) {
					meta.tdAttr = 'data-qtip="' + v + '"';
				}
				return v;
			}
		},{
			xtype: 'rownumberer',
			text: '序号',
			width: 40,
			hidden: true,
			align: 'center'
		}, {
			text: '主键ID',
			isKey: true,
			hidden: true,
			hideable: false,
			dataIndex: me.objectName + '_Id'
		}, {
			text: '发表人ID',
			hidden: true,
			hideable: false,
			dataIndex: me.objectName + '_Register_Id'
		}, {
			text: '发表人ID',
			hidden: true,
			hideable: false,
			dataIndex: me.objectName + '_Register_CName'
		},
	   {
			text: '发表人ID',
			dataIndex: me.objectName + '_Register_Id',
			notShow: true
		},
		{
			text: '内容',
			dataIndex: me.objectName + '_TaskWorkInfo',
			notShow: true
		}];

		return columns;
	},
	changeShowType: function(value) {
		var me = this;
		me.showType = value ? true : false;
		me.toggleRow(me.showType);
	},
	toggleRow: function(bo) {
		var me = this,
			plugins = me.plugins[0],
			view = plugins.view,
			records = me.store.data,
			len = records.length;

		for(var i = 0; i < len; i++) {
			var rowNode = view.getNode(i),
				row = Ext.get(rowNode),
				nextBd = Ext.get(row).down(plugins.rowBodyTrSelector),
				record = view.getRecord(rowNode);
			if(bo) {
				row.removeCls(plugins.rowCollapsedCls);
				nextBd.removeCls(plugins.rowBodyHiddenCls);
				plugins.recordsExpanded[record.internalId] = true;
			} else {
				row.addCls(plugins.rowCollapsedCls);
				nextBd.addCls(plugins.rowBodyHiddenCls);
				plugins.recordsExpanded[record.internalId] = false;
			}
		}
		view.refreshSize();
		if(bo) {
			view.fireEvent('expandbody', rowNode, record, nextBd.dom);
		} else {
			view.fireEvent('collapsebody', rowNode, record, nextBd.dom);
		}
	},
	initDefaultWhere: function() {
		var me = this;
		me.objectNameLower = me.objectName.toLocaleLowerCase();
		if(me.defaultWhere) {
			me.defaultWhere = "(" + me.defaultWhere + ") and ";
		} else {
			me.defaultWhere = "";
		}
		/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
//		if(me.fObjectIsID) {
//			me.defaultWhere += me.objectNameLower + ".IsCommunication=0 and " + me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + me.fObejctName + "ID=" + me.fObjectValue;
//		} else {
//			me.defaultWhere += me.objectNameLower + ".IsCommunication=0 and " + me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + me.fObejctName + ".Id=" + me.fObjectValue;
//		}

        if(me.ProjectTaskID){
        	me.defaultWhere +=  me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "."+ "PProjectTask.Id=" + me.ProjectTaskID;
        }else{
        	if(me.ProjectID){
        		me.defaultWhere +=  me.objectNameLower + ".IsUse=1 and " + me.objectNameLower + "." + me.fObejctName + ".Id=" + me.ProjectID;
        	}
        }
    },
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		if(me.loadAllData == true) {
			var url = (me.selectAllUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.selectAllUrl;
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
			if(me.ProjectTaskID){
				url += '&where=pprojecttaskprogress.PProjectTask.Id=' + me.ProjectTaskID;
            }else{
            	if(me.ProjectID) url += '&where=pprojecttaskprogress.PProject.Id=' + me.ProjectID;
            }
			return url;
		} else {
			var buttonsToolbar = me.getComponent('buttonsToolbar'),
				BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
				EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
				TaskTypeDict= buttonsToolbar.getComponent('PProjectTaskProgress_TaskTypeDict').getValue(),
				params = [];
			me.defaultWhere = "";
			me.initDefaultWhere();
			var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
				JShell.System.Path.ROOT) + me.selectUrl;
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
			if(BeginDate) {
				params.push(me.objectNameLower + ".DataAddTime>='" +
					JShell.Date.toString(BeginDate, true) + "'");
			}
			if(EndDate) {
				params.push(me.objectNameLower + ".DataAddTime<'" +
					JShell.Date.toString(JShell.Date.getNextDate(EndDate), true) + "'");
			}
            if(TaskTypeDict){
            	params.push(me.objectNameLower + ".TaskTypeDict='"+TaskTypeDict+"'");
            }
			if(params.length > 0) {
				me.internalWhere = params.join(' and ');
			} else {
				me.internalWhere = '';
			}
			var arr = [];
			//默认条件
			if(me.defaultWhere && me.defaultWhere != '') {
				arr.push(me.defaultWhere);
			}
			//内部条件
			if(me.internalWhere && me.internalWhere != '') {
				arr.push(me.internalWhere);
			}
			//外部条件
			if(me.externalWhere && me.externalWhere != '') {
				arr.push(me.externalWhere);
			}
			var where = arr.join(") and (");
			if(where) where = "(" + where + ")";
			if(where) {
				url += '&where=' + JShell.String.encode(where);
			}
			return url;
		}
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		if(data.count)me.replyCount=data.count;
		return data;
	}
});