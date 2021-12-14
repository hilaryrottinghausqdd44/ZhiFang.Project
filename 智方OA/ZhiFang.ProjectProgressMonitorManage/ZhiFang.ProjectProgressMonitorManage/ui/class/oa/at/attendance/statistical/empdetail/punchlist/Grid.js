/**
 * 打卡清单
 * @author liangyl
 * @version 2017-1-24
 */
Ext.define('Shell.class.oa.at.attendance.statistical.empdetail.punchlist.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '打卡清单列表',
	width: 800,
	height: 500,
	columnLines: true,
	/**获取数据服务路径*/
	selectUrl: '/WeiXinAppService.svc/SC_UDTO_GetATEmpSignInfoDetailList',
	/**默认加载*/
	defaultLoad: true,
	timeout:90000,
	defaultOrderBy: [{
		property: 'SignInfoExport_StartDateTime',
		direction: 'ASC'
	}],
	/**类型*/
	searchType: 10401,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		buttonsToolbar.on({
			search: function(params) {
				me.onSearch();
			}
		});

	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		return Ext.create('Shell.class.oa.at.attendance.statistical.empdetail.punchlist.SearchToolbar', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			searchType: me.searchType,
			items: items
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '日期',
			dataIndex: 'SignInfoExport_ATEventDateCode',
			width: 85,
			sortable: false,
			defaultRenderer: true,
			isDate: true
		}, {
			text: '星期',
			dataIndex: 'SignInfoExport_WeekInfo',
			width: 55,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '姓名',
			dataIndex: 'SignInfoExport_EmpName',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '工号',
			dataIndex: 'SignInfoExport_Account',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '部门',
			dataIndex: 'SignInfoExport_HRDeptCName',
			width: 90,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '职务',
			dataIndex: 'SignInfoExport_HRPositionCName',
			width: 45,
			sortable: false,
			defaultRenderer: true
		}, {
			text: "签到",
			columns: [{
				text: "时间",
				width: 80,
				dataIndex: 'SignInfoExport_SignInTime',
				sortable: false,
				renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
					var SignInType = record.get('SignInfoExport_SignInType');
					meta.style = me.changeMetaColor(value, meta, record, SignInType);
					return value;
				}
			}, {
				text: "签到类型",
				width: 65,
				dataIndex: 'SignInfoExport_SignInType',
				sortable: false,
				hidden: false,
				renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
					meta.style = me.changeMetaColor(value, meta, record, value);
					return value;
				}
			}, {
				text: "地点",
				width: 140,
				dataIndex: 'SignInfoExport_SigninATEventLogPostionName',
				sortable: false,
				renderer: function(value, meta, record) {
					meta.style = 'overflow:auto;padding: 3px 6px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;';
					return value;
				}
			}, {
				text: "说明",
				width: 80,
				dataIndex: 'SignInfoExport_SignInMemo',
				sortable: false,
				renderer: function(value, meta, record) {
					meta.style = 'overflow:auto;padding: 3px 6px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;';
					return value;
				}
			}, {
				text: "是否脱岗",
				width: 60,
				align: 'center',
				isBool: true,
				hidden: true,
				type: 'bool',
				dataIndex: 'SignInfoExport_SignInIsOffsite',
				sortable: false,
				renderer: function(value, meta) {
					var v = value + '';
					if(v == 'true') {
						meta.style = 'color:red';
						v = JShell.All.TRUE;
					} else if(v == 'false') {
						meta.style = 'color:green';
						v = '';
					} else {
						v == '';
					}
					return v;
				}
			}]
		}, {
			text: "签退",
			columns: [{
				text: "时间",
				width: 80,
				dataIndex: 'SignInfoExport_SignOutTime',
				sortable: false,
				renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
					var SignOutType = record.get('SignInfoExport_SignOutType');
					meta.style = me.changeMetaColor(value, meta, record, SignOutType);
					return value;
				}
			}, {
				text: "签退类型",
				width: 65,
				dataIndex: 'SignInfoExport_SignOutType',
				sortable: false,
				hidden: false,
				renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
					meta.style = me.changeMetaColor(value, meta, record, value);
					return value;
				}
			}, {
				text: "地点",
				width: 140,
				dataIndex: 'SignInfoExport_SignoutATEventLogPostionName',
				sortable: false,
				renderer: function(value, meta, record) {
					meta.style = 'overflow:auto;padding: 3px 6px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;';
					return value;
				}
			}, {
				text: "说明",
				width: 80,
				dataIndex: 'SignInfoExport_SignOutMemo',
				sortable: false,
				renderer: function(value, meta, record) {
					meta.style = 'overflow:auto;padding: 3px 6px;text-overflow: ellipsis;white-space: nowrap;white-space:normal;line-height:20px;';
					return value;
				}
			}, {
				text: "是否脱岗",
				align: 'center',
				isBool: true,
				hidden: true,
				type: 'bool',
				width: 60,
				dataIndex: 'SignInfoExport_SignOutIsOffsite',
				sortable: false,
				renderer: function(value, meta) {
					var v = value + '';
					if(v == 'true') {
						meta.style = 'color:red';
						v = JShell.All.TRUE;
					} else if(v == 'false') {
						meta.style = 'color:green';
						v = '';
					} else {
						v == '';
					}
					return v;
				}
			}, ]
		}, {
			text: '是否工作日',
			dataIndex: 'SignInfoExport_IsWorkDay',
			hidden: true,
			hideable: false
		}, {
			text: '主键ID',
			dataIndex: 'SignInfoExport_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**改变签到签退列颜色*/
	changeMetaColor: function(value, meta, record, Type) {
		var me = this,
			metastyle = '';
		var IsWorkDay = record.get('SignInfoExport_IsWorkDay');
		switch(Type) {
			case '签到迟到':
				metastyle = 'color:red';
				break;
			case '签退早退':
				metastyle = 'color:red';
				break;
			case '未打卡':
				metastyle = 'color:red';
				break;
			case '':
				metastyle = 'color:red';
				break;
			case '旷工':
				metastyle = 'color:red';
				break;
			case '补签卡':
				metastyle = 'color:green';
				break;
			default:
				metastyle = 'color:black';
				break;
		}
		return metastyle;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			par = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&');
		var params = me.getComponent('buttonsToolbar').getParams();
		if(params) {
			par.push("filterType=" + params.filterType);
			par.push("deptId=" + params.DeptID);
			par.push("isGetSubDept=" + params.isCheckTree);
			par.push("empId=" + params.UserID);
			par.push("startDate=" + params.applySDate);
			par.push("endDate=" + params.applyEDate);
			url += par.join("&");
		}
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
	},
	/**创建数据字段*/
	getStoreFields: function(isString) {
		var me = this,
			columns = me._resouce_columns || [],
			length = columns.length,
			fields = [];

		for(var i = 0; i < length; i++) {
			if(columns[i].dataIndex) {
				var obj = isString ? columns[i].dataIndex : {
					name: columns[i].dataIndex,
					type: columns[i].type ? columns[i].type : 'string'
				};
				fields.push(obj);
			}
		}
		fields.push({
			name: 'SignInfoExport_SignInTime',
			type: 'string'
		}, {
			name: 'SignInfoExport_SigninATEventLogPostionName',
			type: 'string'
		}, {
			name: 'SignInfoExport_SignInType',
			type: 'string'
		}, {
			name: 'SignInfoExport_SignInMemo',
			type: 'string'
		}, {
			name: 'SignInfoExport_SignInIsOffsite',
			type: 'bool'
		}, {
			name: 'SignInfoExport_SignOutTime',
			type: 'string'
		}, {
			name: 'SignInfoExport_SignoutATEventLogPostionName',
			type: 'string'
		}, {
			name: 'SignInfoExport_SignOutType',
			type: 'string'
		}, {
			name: 'SignInfoExport_SignOutMemo',
			type: 'string'
		}, {
			name: 'SignInfoExport_SignOutIsOffsite',
			type: 'bool'
		});
		return fields;
	}
});