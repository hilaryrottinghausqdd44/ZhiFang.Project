/**
 * 工作日志--任务列表
 * @author liangyl
 * @version 2016-08-02
 */
Ext.define('Shell.class.oa.worklog.daylog.PTaskGrid', {
	extend: 'Shell.ux.grid.PostPanel',
	title: '任务选择列表',
	width: 300,
	height: 400,
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPTask',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PTask_DataAddTime',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	hasRownumberer: true,
		/**语言包内容*/
	Shell_ux_grid_Panel:{
		NumberText: ''
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},

	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '',
			dataIndex: 'CName',
			width: 150,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '',
			dataIndex: 'IdString',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '',
			align: 'center',
			width: 65,
			hidden: false,
			hideable: false,
			items: [{
                icon: '../ui/images/user/worklog.gif',
				handler: function(grid, rowIndex, colIndex) {
					me.hasChecked = true;
					var rec = grid.getStore().getAt(rowIndex);
					me.fireEvent('onClick', grid, rec, me);
				}

			}]
		}];

		return columns;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.doFilterParams();
		return me.callParent(arguments);
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.params || {},
			ParaClass = {};
		var ParaClass = {
			ExportType:4,
			Status: "4621621720762238176",
			Page: 1,
			Limit: 5,
			CopyForEmpNameList: []
		};
		me.postParams = {
			entity: ParaClass,
			isPlanish: true,
			fields: me.getStoreFields(true).join(',')
		};
	},
	/**查询数据*/
	onSearch: function() {
		var me = this,
			collapsed = me.getCollapsed();
		me.defaultLoad = true;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}
		me.disableControl(); //禁用 所有的操作功能
		me.showMask(me.loadingText); //显示遮罩层
		var url = me.getLoadUrl();
		var params = Ext.JSON.encode(me.postParams);
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				var listArr=[];
				var objs = {
					CName: '普通日志',
					IdString: '',
				};
				listArr.push(objs);
				var obj = data.value || {};
				var valueData=[];
				if(obj.list!=null){
				   valueData=listArr.concat(obj.list);
				}else{
					valueData=listArr;
				}
			
				var list = valueData || [];
				
				me.store.loadData(list);

				if(list.length == 0) {
					var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
					JShell.Action.delay(function() {
						me.getView().update(msg);
					}, 200);
				} else {
					if(me.autoSelect) {
						me.doAutoSelect(list.length - 1);
					}
				}
			} else {
				var msg = me.errorFormat.replace(/{msg}/, data.msg);
				JShell.Action.delay(function() {
					me.getView().update(msg);
				}, 200);
			}
		});
	}
});