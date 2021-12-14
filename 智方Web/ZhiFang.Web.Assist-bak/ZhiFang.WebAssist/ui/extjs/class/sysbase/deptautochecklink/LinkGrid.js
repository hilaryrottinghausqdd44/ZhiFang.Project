/**
 * 科室自动核收关系
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.deptautochecklink.LinkGrid', {
	extend: 'Shell.class.assist.basic.GridPanel',

	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '科室自动核收关系',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKDeptAutoCheckLinkByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKDeptAutoCheckLinkByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_DelGKDeptAutoCheckLink',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddGKDeptAutoCheckLink',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'GKDeptAutoCheckLink_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'deptautochecklink.LinkGrid',
	/**用户UI配置Name*/
	userUIName: "科室自动核收关系",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '编码/名称',
			isLike: true,
			fields: ['gkdeptautochecklink.Department.Id', 'gkdeptautochecklink.Department.CName']
		};
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '编码',
			dataIndex: 'GKDeptAutoCheckLink_Id',
			isKey: true,
			width: 150,
			hidden:true,
			hideable: false
		}, {
			text: '科室编码',
			dataIndex: 'GKDeptAutoCheckLink_Department_Id',
			width: 130,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '科室名称',
			dataIndex: 'GKDeptAutoCheckLink_Department_CName',
			width: 150,
			menuDisabled: true,
			defaultRenderer: true,
			editor: {}
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'GKDeptAutoCheckLink_IsUse',
			width: 65,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '次序',
			dataIndex: 'GKDeptAutoCheckLink_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-',
			{
				text: '科室选择',
				tooltip: '科室选择',
				iconCls: 'button-add',
				handler: function() {
					me.onAddClick();
				}
			}, 'edit', 'del', '-'
		];
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;

		var defaultWhere = " department.Visible=1 ";
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;

		var linkWhere = ""; //关系表查询条件
		JShell.Win.open('Shell.class.sysbase.deptautochecklink.choose.App', {
			resizable: true,
			width: maxWidth,
			height: height,
			linkWhere: linkWhere,
			leftDefaultWhere: defaultWhere,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onSave(p, records);
				}
			}
		}).show();
	},
	/**保存关系数据*/
	onSave: function(p, records) {
		var me = this;

		if (records.length == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;

		for (var i in records) {
			me.onAddOneLink(p, records[i], i);
		}
	},
	onAddOneLink: function(p, record, index) {
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		//创建者信息
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		var params = {
			empID: empID,
			empName: empName,
			entity: {
				Id: -1,
				IsUse: 1,
				DispOrder: record.get('Department_DispOrder'),
				Department: {
					Id: record.get('Department_Id'),
					DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
				}
			}
		};

		params = Ext.JSON.encode(params);
		var url = JShell.System.Path.ROOT + me.addUrl;
		setTimeout(function() {
			//提交数据到后台
			JShell.Server.post(url, params, function(data) {
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					p.close();
					//if (me.saveErrorCount == 0) me.onSearch();
					me.onSearch();
				}
			});
		}, 100 * index);
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
			me.updateOne(i, rec);
		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var id = rec.get(me.PKField);
		var isUse = rec.get('GKDeptAutoCheckLink_IsUse');
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		var params = Ext.JSON.encode({
			empID: empID,
			empName: empName,
			entity: {
				Id: id,
				IsUse: isUse,
				DispOrder: rec.get('GKDeptAutoCheckLink_DispOrder')
			},
			fields: 'Id,IsUse,DispOrder'
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

	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var len = data.list.length;
		for (var i = 0; i < len; i++) {
			var IsUse = data.list[i].SCRecordType_IsUse;
			if (IsUse == 'True' || IsUse ==
				'true' || IsUse == '1' || IsUse == 1) {
				IsUse = true;
			} else {
				IsUse = false;
			}
			data.list[i].SCRecordType_IsUse = IsUse;
		}
		return data;
	},
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.ApplyGrid.getSelectionModel().getSelection();
			if (records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			PK: id,
			classNameSpace: 'ZhiFang.Entity.WebAssist', //类域
			className: 'UpdateOperationType', //类名
			title: '操作记录',
			defaultWhere: "scoperation.BusinessModuleCode='BDictType'"
		};
		var win = JShell.Win.open('Shell.class.assist.scoperation.SCOperation', config);
		win.show();
	}
});
