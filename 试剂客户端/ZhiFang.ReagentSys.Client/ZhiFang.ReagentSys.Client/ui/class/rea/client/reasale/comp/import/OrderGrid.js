/**
 * @description 提取订单转供货单
 * @author longfc
 * @version 2018-05-17
 */
Ext.define('Shell.class.rea.client.reasale.comp.import.OrderGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',

	requires: [
		'Shell.ux.form.field.DateArea',
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '提取订单转供货单',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**是否多选行*/
	checkOne: true,
	/**选择订货方信息*/
	LabcID: null,
	LabcName: null,
	ReaServerLabcCode: null,

	/**订单状态Key*/
	StatusKey: "ReaBmsOrderDocStatus",
	/**订单数据标志Key*/
	IOFlagKey: "ReaBmsOrderDocIOFlag",

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenOrderDoc_OperDate',
		direction: 'DESC'
	}, {
		property: 'ReaBmsCenOrderDoc_Status',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {
			width: 280,
			isLike: true,
			itemId: 'search',
			emptyText: '订货单号',
			fields: ['reabmscenorderdoc.OrderDocNo']
		};
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);

		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsCenOrderDoc_DataAddTime',
			text: '申请日期',
			align: 'center',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_LabcName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 80,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.StatusKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.StatusKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.StatusKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.StatusKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.StatusKey].FColor[value];
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
			dataIndex: 'ReaBmsCenOrderDoc_UrgentFlag',
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
			dataIndex: 'ReaBmsCenOrderDoc_IOFlag',
			text: '数据标志',
			align: 'center',
			width: 75,
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.IOFlagKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.IOFlagKey].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.IOFlagKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.IOFlagKey].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.IOFlagKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.IOFlagKey].FColor[value];
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
			dataIndex: 'ReaBmsCenOrderDoc_DeptName',
			text: '所属部门',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_OrderDocNo',
			text: '订货单号',
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 65,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
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
		}, {
			xtype: 'actioncolumn',
			text: '提取',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-import hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onOrderToSupply(rec);
				}
			}]
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_UserName',
			text: '操作人',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_CheckTime',
			text: '审核时间',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Memo',
			text: '备注',
			hidden: true,
			width: 100,
			renderer: function(value, meta) {
				return "";
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_CompID',
			text: '供货商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaCompID',
			text: '本地供货商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaCompanyName',
			text: '本地供货商',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerCompCode',
			text: '供货商所属机构编码',
			width: 105,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerLabcCode',
			text: '订货方所属机构编码',
			width: 105,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Confirm',
			text: '确认人',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ConfirmTime',
			text: '确认时间',
			width: 130,
			isDate: true,
			hasTime: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	initButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-'];
		//订货方
		items.push({
			fieldLabel: '订货方选择',
			emptyText: '必填项',
			allowBlank: false,
			name: 'LabcName',
			itemId: 'LabcName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			width: 280,
			labelWidth: 80,
			onTriggerClick: function() {
				JcallShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "1",
					resizable: false,
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JcallShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onLabcAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			type: 'search',
			info: me.searchInfo
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		items.push('->', 'accept');
		me.buttonToolbarItems = items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!JcallShell.REA.System.CENORG_CODE) {
			var error = me.errorFormat.replace(/{msg}/, "获取机构平台编码信息为空!");
			me.getView().update(error);
			return false;
		};
		//机构所属平台编码
		var labOrgNo = JShell.REA.System.CENORG_CODE;
		if(!labOrgNo) labOrgNo = '';
		//订单的供货商为当前机构,订单状态及数据标志为供应商确认
		me.defaultWhere = "(reabmscenorderdoc.IOFlag=3 and reabmscenorderdoc.Status=8)";
		if(labOrgNo) {
			me.defaultWhere = me.defaultWhere + " and reabmscenorderdoc.ReaServerCompCode='" + labOrgNo + "'"
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var where = [];
		if(me.ReaServerLabcCode) {
			where.push("reabmscenorderdoc.ReaServerLabcCode='" + me.ReaServerLabcCode + "'");
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		me.internalWhere = where.join(" and ");
		return me.callParent(arguments);
	},
	/**@description 订货方选择*/
	onLabcAccept: function(record) {
		var me = this;

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';

		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		me.LabcID = id;
		me.LabcName = text;
		me.ReaServerLabcCode = platformOrgNo;
		me.getComponent('buttonsToolbar').getComponent('LabcName').setValue(text);
	},
	onShowOperation: function(record) {
		var me = this;
		if(!record) {
			var records = me.getSelectionModel().getSelection();
			if(records.length != 1) {
				JcallShell.Msg.error(JcallShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		//临时,已撤销申请,已撤消审核,可以修改
		var id = record.get("ReaBmsCenOrderDoc_Id");
		var config = {
			title: '订单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: 'ReaBmsOrderDocStatus' //类名
		};
		var win = JcallShell.Win.open('Shell.class.rea.client.reareqoperation.Panel', config);
		win.show();
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	},
	/**提取订单转供货单*/
	onOrderToSupply: function(rec) {
		var me = this;
		var id = rec.get("ReaBmsCenOrderDoc_Id");
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply";
		var params = {
			"orderId": id
		};
		JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
			if(data.success) {
				me.onSearch();
			} else {
				JShell.Msg.error('提取订单转供货单失败！' + data.msg);
			}
		});
	}
});