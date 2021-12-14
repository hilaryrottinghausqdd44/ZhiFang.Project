/**
 * 操作记录日志查看
 * @author longfc
 * @version 2016-07-26
 */
Ext.define('Shell.class.pki.search.SearchBSysOperateGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '操作记录日志查看',
	showSuccessInfo: true,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BSysOperate_OperateTime',
		direction: 'ASC'
	}, {
		property: 'BSysOperate_OperateClass',
		direction: 'ASC'
	}, {
		property: 'BSysOperate_Operater',
		direction: 'ASC'
	}],
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBSysOperateByHQL?isPlanish=true',
	afterRender: function() {
		var me = this;
		me.store.on({
			load: function(store, records, successful) {
				me.onAfterLoad(records, successful);
				Ext.Msg.hide();
			},
			datachanged: function(store, eOpts) {
				Ext.Msg.hide();
			}
		});
		me.callParent(arguments);

		me.getComponent('filterToolbar').on({
			search: function(p, params) {
				me.params = params;
				var isExec = true,
					msg = "";
				if(!me.params.BeginDate || me.params.BeginDate == "") {
					isExec = false;
					msg = msg + "开始日期不能为空!<br />";
				}
				if(!me.params.EndDate || me.params.EndDate == "") {
					isExec = false;
					msg = msg + "结束日期不能为空!";
				}
				if(isExec) {
					Ext.Msg.wait("正在获取数据,请稍候...", '提示');
					me.onSearch();
				} else if(msg != "") {
					JShell.Msg.alert(msg);
				}
			}
		});
		JShell.Action.delay(function() {
			Ext.Msg.wait("正在获取数据,请稍候...", '提示');
			me.onSearch()
		}, null, 600);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.search.SearchBSysOperateToolbar', {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			height: 30
		})];

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			dataIndex: 'BSysOperate_OperateClass',
			text: '操作类名称',
			width: 150,
			sortable: true,
			//align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'BSysOperate_OperateTime',
			text: '操作时间',
			width: 140,
			isDate: true,
			hasTime: true,
			sortable: true
		}, {
			dataIndex: 'BSysOperate_Operater',
			text: '操作人',
			width: 130,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BSysOperate_OperateHost',
			text: '操作计算机',
			width: 130,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BSysOperate_OperateMemo',
			text: '操作备注',
			flex: 1,
			//width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'BSysOperate_ZDY1',
			text: '自定义一',
			width: 80
		}, {
			dataIndex: 'BSysOperate_ZDY2',
			text: '自定义二',
			width: 80,
			sortable: false
		});
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		me.params = me.params || me.getComponent('filterToolbar').getParams();

		var strWhere = "(1=1)";
		//做处理
		if(me.params.BeginDate && me.params.EndDate) { //BeginDate
			strWhere = strWhere + " and (bsysoperate.OperateTime>='" + me.params.BeginDate.trim() + "'" +
				" and bsysoperate.OperateTime<='" + me.params.EndDate.trim() + "')"; // 00:00:00.000
		};
		me.externalWhere = strWhere;

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
});