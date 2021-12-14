/**
 * 员工列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.user.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '员工列表',
	width: 1200,
	height: 600,

	/**根据部门ID查询模式*/
	DeptTypeModel: true,
	/**部门ID*/
	DeptId: null,
	/**部门名称*/
	DeptName: null,
	/**部门时间戳*/
	DeptDataTimeStamp: null,

	/**获取数据服务路径*/
	selectUrl: '/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
	/**根据部门ID获取数据服务路径*/
	selectUrl2: '/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true',

	/**修改服务地址*/
	editUrl: '/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField',
	/**删除数据服务路径*/
	delUrl: '/RBACService.svc/RBAC_UDTO_DelHREmployee',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: false,

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 1000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**是否存在上级*/
	hasManager: false,

	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.addEvents('addclick');
		//查询框信息
		me.searchInfo = {
			width: 145,
			emptyText: '名称',
			isLike: true,
			fields: ['hremployee.CName']
		};

		me.buttonToolbarItems = ['refresh', 'add', 'del', 'save', {
			xtype: 'checkbox',
			boxLabel: '本部门',
			itemId: 'onlyShowDept',
			checked: !me.DeptTypeModel,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeShowType(newValue);
				}
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'file-excel',
			text: '员工导入',
			tooltip: 'EXCEL导入导出员工',
			menu: [{
				iconCls: 'button-exp',
				text: '员工模板下载',
				tooltip: '员工模板下载',
				handler: function() {
					me.onUserDownload();
				}
			}, '-', {
				iconCls: 'button-exp',
				text: '导入当前部门人员',
				tooltip: '导入当前部门人员',
				handler: function() {
					me.onUserUpload(true);
				}
			}, '-', {
				iconCls: 'file-excel',
				text: '导入所有部门人员',
				tooltip: '导入所有部门人员',
				hidden: true,
				handler: function() {
					me.onUserUpload(false);
				}
			}]
		}, '->', {
			type: 'search',
			info: me.searchInfo
		}];

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '员工姓名',
			dataIndex: 'HREmployee_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '员工代码',
			dataIndex: 'HREmployee_UseCode',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '隶属部门',
			dataIndex: 'HREmployee_HRDept_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		},  {
			text: '标准代码',
			dataIndex: 'HREmployee_StandCode',
			width: 100,
			editor: {},
			menuDisabled: true,
			defaultRenderer: true
		},{
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'HREmployee_IsUse',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '主键ID',
			dataIndex: 'HREmployee_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '时间戳',
			dataIndex: 'HREmployee_DataTimeStamp',
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '微信',
			align: 'center',
			width: 40,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				iconCls: 'button-config hand',
				tooltip: '微信绑定',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openWinXinLinkGrid(rec);
				}
			}]
		}];

		if (me.hasManager) {
			columns.push({
				text: '直	接上级',
				dataIndex: 'HREmployee_ManagerName',
				width: 100,
				sortable: false,
				menuDisabled: true,
				defaultRenderer: true
			});
		}
		return columns;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			//var IsUse = rec.get('HREmployee_IsUse');
			me.updateOneByIsUse(i, id, rec);
		}
	},
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	updateOneByIsUse: function(index, id, rec) {
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var IsUse = rec.get('HREmployee_IsUse');
		var StandCode = rec.get('HREmployee_StandCode');
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsUse: IsUse,
				StandCode: StandCode
			},
			fields: 'Id,IsUse,StandCode'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	loadByDeptId: function(id) {
		var me = this;
		me.DeptId = id;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			url = '';

		//根据部门ID查询模式
		if (me.DeptTypeModel) {
			url += JShell.System.Path.getUrl(me.selectUrl2);
		} else {
			url += JShell.System.Path.getUrl(me.selectUrl);
		}

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		//根据部门ID查询模式
		if (me.DeptTypeModel) {
			url += '&where=id=' + me.DeptId;
			if (where) {
				url += '^' + JShell.String.encode(where);
			}
		} else {
			url += "&where=hremployee.HRDept.Id='" + me.DeptId + "'";
			if (where) {
				url += ' and ' + JShell.String.encode(where);
			}
		}

		return url;
	},
	changeShowType: function(value) {
		var me = this;
		me.DeptTypeModel = value ? false : true;
		me.onSearch();
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var len = data.list.length;
		for (var i = 0; i < len; i++) {
			if (data.list[i].HREmployee_IsUse == 'True' || data.list[i].HREmployee_IsUse == 'true') {
				data.list[i].HREmployee_IsUse = true;
			} else {
				data.list[i].HREmployee_IsUse = false;
			}
		}
		return data;
	},
	/**打开微信选择列表*/
	openWinXinLinkGrid: function(record) {
		var me = this,
			Id = record.get(me.PKField),
			Name = record.get('HREmployee_CName');

		JShell.Win.open('Shell.class.sysbase.user.weixin.LinkGrid', {
			SUB_WIN_NO: '3', //内部窗口编号
			resizable: false,
			EmpId: Id,
			EmpName: Name
		}).show();
	},
	//用户模板下载
	onUserDownload: function() {
		var me = this;
		var url = JShell.System.Path.UI + '/models/rea/user/' + JShell.REA.User.EXCEL + '?v=' + new Date().getTime();
		window.open(url);
	},
	//用户导入
	onUserUpload: function(hasOrg) {
		var me = this;
		var tid = me.DeptId + '';
		if (tid == '0') {
			JShell.Msg.alert('请选择一个机构节点', null, 2000);
			return;
		}
		JShell.Win.open('Shell.class.sysbase.user.UploadUser', {
			DeptID: hasOrg ? me.DeptId : '',
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	}
});
