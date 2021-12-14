/**
 * 员工财务账户信息列表
 * @author longfc
 * @version 2016-11-18
 */
Ext.define('Shell.class.sysbase.user.pempfinanceaccount.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '员工财务账户信息列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccountByDeptId',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPEmpFinanceAccount',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePEmpFinanceAccountByField',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPEmpFinanceAccount',

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 1000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 20,

	/**复选框*/
	multiSelect: false,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	/*是否管理员应用**/
	isManage: false,
	checkOne: true,
	/**根据部门ID查询模式*/
	DeptTypeModel: false,
	/**部门ID*/
	DeptId: null,
	initComponent: function() {
		var me = this;
		me.addEvents('onAddClick');
		me.addEvents('onEditClick');

		//		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
		//			clicksToEdit: 1
		//		});

		me.buttonToolbarItems = ['refresh', {
			xtype: 'checkbox',
			boxLabel: '本部门',
			itemId: 'onlyShowDept',
			checked: !me.DeptTypeModel,
			inputValue: !me.DeptTypeModel,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeShowType(newValue);
				}
			}
		}, '-', 'edit', '-', '->'];
		// 'save',
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '主键ID',
			dataIndex: 'Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '员工Id',
			dataIndex: 'EmpID',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '员工姓名',
			dataIndex: 'Name',
			flex: 1,
			hidden: false,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				if(record) {
					var oldValue = record.get("IsExist").toString();
					if(oldValue == "0" || oldValue == "" || oldValue.toLowerCase() == 'false') {
						meta.style = 'font-weight:bold;background-color:#87CEEB;color:#ffffff;'; //orange
					}
				}
				return value;
			}
		}, {
			text: '拼音字头',
			dataIndex: 'PinYinZiTou',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'Shortcode',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'SName',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '单笔借款上限',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'OneceLoanUpperAmount',
			width: 90,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('OneceLoanUpperAmount', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var isExist = record.get("IsExist");
				if((isExist == false || isExist == 0) && (value == "0" || value == 0)) {
					value = "";
				}
				return value;
			}
		}, {
			text: '借款上限',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'LoanUpperAmount',
			width: 90,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('LoanUpperAmount', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var isExist = record.get("IsExist");
				if((isExist == false || isExist == 0) && (value == "0" || value == 0)) {
					value = "";
				}
				return value;
			}
		}, {
			text: '借款总额',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'LoanAmount',
			width: 90,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				readOnly: (me.isManage == false ? true : false),
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('LoanAmount', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var isExist = record.get("IsExist");
				if((isExist == false || isExist == 0) && (value == "0" || value == 0)) {
					value = "";
				}
				return value;
			}
		}, {
			text: '待还额度',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'UnRepaymentAmount',
			width: 90,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				readOnly: (me.isManage == false ? true : false),
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('UnRepaymentAmount', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var isExist = record.get("IsExist");
				if((isExist == false || isExist == 0) && (value == "0" || value == 0)) {
					value = "";
				}
				return value;
			}
		}, {
			text: '还款总额',
			style: 'font-weight:bold;color:white;background:orange;',
			dataIndex: 'RepaymentAmount',
			width: 90,
			sortable: true,
			menuDisabled: true,
			editor: {
				xtype: 'numberfield',
				allowBlank: true,
				readOnly: (me.isManage == false ? true : false),
				listeners: {
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('RepaymentAmount', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				var isExist = record.get("IsExist");
				if((isExist == false || isExist == 0) && (value == "0" || value == 0)) {
					value = "";
				}
				return value;
			}
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'IsUse',
			width: 40,
			align: 'center',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '是否存在员工财务账户',
			dataIndex: 'IsExist',
			width: 40,
			align: 'center',
			hidden: true,
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}];
		return columns;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var record = records[i];
			var IsExist = record.get("IsExist");
			switch(IsExist) {
				case true:
					me.updateOne(i, record);
					break;
				default:
					me.addOne(i, record);
					break;
			}

		}
	},
	updateOne: function(index, rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var IsUse = rec.get('IsUse');
		var OneceLoanUpperAmount = rec.get('OneceLoanUpperAmount');
		var LoanUpperAmount = rec.get('LoanUpperAmount');
		var LoanAmount = rec.get('LoanAmount');
		var url = JShell.System.Path.getUrl(me.editUrl);
		var entity = {
			entity: {
				Id: id,
				IsUse: IsUse,
				OneceLoanUpperAmount: OneceLoanUpperAmount,
				LoanUpperAmount: LoanUpperAmount,
				LoanAmount: LoanAmount
			},
			fields: 'Id,IsUse,OneceLoanUpperAmount,LoanUpperAmount,LoanAmount'
		};
		if(me.isManage == true) {
			var UnRepaymentAmount = rec.get('UnRepaymentAmount');
			var RepaymentAmount = rec.get('RepaymentAmount');
			entity.entity.UnRepaymentAmount = UnRepaymentAmount;
			entity.entity.RepaymentAmount = RepaymentAmount;
			entity.fields = entity.fields + ",UnRepaymentAmount,RepaymentAmount";
		}
		var params = Ext.JSON.encode(entity);

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	addOne: function(index, rec) {
		var me = this;
		var id = rec.get(me.PKField);
		var IsUse = rec.get('IsUse');
		var OneceLoanUpperAmount = rec.get('OneceLoanUpperAmount');
		var LoanUpperAmount = rec.get('LoanUpperAmount');
		var LoanAmount = rec.get('LoanAmount');
		var url = JShell.System.Path.getUrl(me.addUrl);
		var entity = {
			Name: rec.get('Name'),
			EmpID: rec.get('EmpID'),
			PinYinZiTou: rec.get('PinYinZiTou'),
			Shortcode: rec.get('Shortcode'),
			SName: rec.get('SName'),
			IsUse: true
		};

		if(rec.get('OneceLoanUpperAmount') != "") {
			entity.OneceLoanUpperAmount = rec.get('OneceLoanUpperAmount');
		}
		if(rec.get('LoanUpperAmount') != "") {
			entity.LoanUpperAmount = rec.get('LoanUpperAmount');
		}
		if(rec.get('LoanAmount') != "") {
			entity.LoanAmount = rec.get('LoanAmount');
		}

		if(me.isManage == true) {
			if(rec.get('UnRepaymentAmount') != "") {
				entity.UnRepaymentAmount = rec.get('UnRepaymentAmount');
			}
			if(rec.get('RepaymentAmount') != "") {
				entity.RepaymentAmount = rec.get('RepaymentAmount');
			}
		}
		var params = {
			entity: entity
		};
		params = Ext.JSON.encode(params);

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},

	onAddClick: function() {
		var me = this;
		me.fireEvent('onAddClick', me);
	},
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onEditClick', me, records[0]);
	},
	changeShowType: function(value) {
		var me = this;
		me.DeptTypeModel = value ? false : true;
		me.onSearch();
	},
	loadByDeptId: function(id) {
		var me = this;
		me.DeptId = id;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			url = '';
		//根据部门ID查询模式
		url += JShell.System.Path.getUrl(me.selectUrl);
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		if(me.DeptId == null || me.DeptId == undefined)
			me.DeptId = "0";
		url += '&deptid=' + me.DeptId + "&isincludesubdept=" + me.DeptTypeModel;

		return url;
	}
});