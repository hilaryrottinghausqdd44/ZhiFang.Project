/**
 * 客户端验收:实验室订单导入
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.choose.order.OrderCheck', {
	extend: 'Shell.ux.grid.CheckPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '订货单列表',
	width: 830,
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocByHQL?isPlanish=true',
	/**是否带清除按钮*/
	hasClearButton: false,
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**是否多选行*/
	checkOne: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BmsCenOrderDoc_OperDate',
		direction: 'DESC'
	}, {
		property: 'BmsCenOrderDoc_Status',
		direction: 'ASC'
	}],
	StatusList: [],
	/**申请单状态枚举*/
	StatusEnum: {},
	/**申请单状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**下拉状态默认值*/
	defaultStatusValue: "",
	/**录入:entry/审核:check*/
	OTYPE: "entry",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 380,
			isLike: true,
			itemId: 'Search',
			emptyText: '订货单号',
			fields: ['bmscenorderdoc.OrderDocNo']
		};
		me.getStatusListData();
		me.dockedItems = me.dockedItems || [];
		me.buttonToolbarItems = me.createButtonToolbarItems2();
		//if(me.hasAcceptButton) me.buttonToolbarItems.push('->', 'accept');
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'BmsCenOrderDoc_OperDate',
			text: '申请日期',
			align: 'center',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'BmsCenOrderDoc_OrderDocNo',
			text: '订货单号',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = value;
				if(me.StatusEnum != null)
					v = me.StatusEnum[value];
				var bColor = "";
				if(me.StatusBGColorEnum != null)
					bColor = me.StatusBGColorEnum[value];
				var fColor = "";
				if(me.StatusFColorEnum != null)
					fColor = me.StatusFColorEnum[value];
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
		}, {
			dataIndex: 'BmsCenOrderDoc_UrgentFlag',
			text: '紧急标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var info = JShell.REA.Enum.BmsCenOrderDoc_UrgentFlag['E' + value] || {};
				var v = info.value || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + (info.bcolor || '#FFFFFF') +
					';color:' + (info.color || '#000000');
				return v;
			}
		}, {
			dataIndex: 'BmsCenOrderDoc_ReaCompName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_UserName',
			text: '订购人员',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenOrderDoc_CheckTime',
			text: '审核时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'BmsCenOrderDoc_Memo',
			text: '备注',
			hidden: true,
			width: 100,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'BmsCenOrderDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenOrderDoc_ReaCompID',
			text: '供货方ID',
			hidden: true,
			hideable: false
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}];

		return columns;
	},

	/**创建功能 按钮栏*/
	createButtonToolbarItems2: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '供货方主键ID',
			hidden: true,
			xtype: 'textfield',
			itemId: 'ReaCompID'
		}, {
			fieldLabel: '供应商选择',
			emptyText: '必选项',
			allowBlank: false,
			itemId: 'ReaCompName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			labelWidth: 75,
			width: 285,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.order.CenOrgCheck', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onCompAccept(record);
							p.close();
							me.onSearch();
						}
					}
				}).show();
			}
		});
		items.push({
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('buttonsToolbar').getComponent('ReaCompID');
		var ComName = me.getComponent('buttonsToolbar').getComponent('ReaCompName');
		ComId.setValue(record ? record.get('ReaCenOrg_Id') : '');
		ComName.setValue(record ? record.get('ReaCenOrg_CName') : '');
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		var buttonsToolbar2 = me.getComponent('buttonsToolbar'),
			ReaCompID = buttonsToolbar2.getComponent('ReaCompID');
		if(!ReaCompID.getValue()) return false;

		me.store.proxy.url = me.getLoadUrl(); //查询条件		
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar'),
			ReaCompID = buttonsToolbar2.getComponent('ReaCompID');
		var search = buttonsToolbar2.getComponent('search');
		var where = [];
		if(ReaCompID) {
			var value = ReaCompID.getValue();
			if(value) {
				where.push("bmscenorderdoc.ReaCompID=" + value);
			}
		}
		//审核通过或上传
		where.push("bmscenorderdoc.ReaCompID is not null and (bmscenorderdoc.Status=3 or bmscenorderdoc.Status=4) and bmscenorderdoc.DeleteFlag=0");
		if(search) {
			var value = search.getValue();
			if(value) {
				where.push(me.getSearchWhere(value));
			}
		}
		me.internalWhere = where.join(" and ");

		return me.callParent(arguments);
	},
	onShowOperation: function(record) {
		var me = this;
		if(!record) {
			var records = me.getSelectionModel().getSelection();
			if(records.length != 1) {
				JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		var id = record.get("BmsCenOrderDoc_Id");
		var config = {
			title: '订单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: 'ReaBmsOrderDocStatus' //类名
		};
		var win = JShell.Win.open('Shell.class.rea.client.reareqoperation.Panel', config);
		win.show();
	},
	/**获取申请总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "ReaBmsOrderDocStatus",
				"classnamespace": "ZhiFang.Digitlab.Entity.ReagentSys"
			}]
		};
		return params;
	},
	/**获取状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if(me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode(me.getParams());
		me.StatusList = [];
		me.StatusEnum = {};
		me.StatusFColorEnum = {};
		me.StatusBGColorEnum = {};
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsOrderDocStatus.length > 0) {
						me.StatusList.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']); //, 'font-weight:bold;text-align:center;'
						Ext.Array.each(data.value[0].ReaBmsOrderDocStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								//style.push('color:' + obj.FontColor);
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor); //background-
								me.StatusBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.StatusEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempStatus = me.StatusList;
		return tempStatus;
	}
});