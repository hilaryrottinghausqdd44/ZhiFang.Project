/**
 * 客户端验收:实验室订单导入
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.choose.order.OrderCheck', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '订货单列表',
	width: 830,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDocByHQL?isPlanish=true',
	
	/**是否带清除按钮*/
	hasClearButton: false,
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
		property: 'ReaBmsCenOrderDoc_OperDate',
		direction: 'DESC'
	}, {
		property: 'ReaBmsCenOrderDoc_Status',
		direction: 'ASC'
	}],

	/**订单状态Key*/
	StatusKey: "ReaBmsOrderDocStatus",
	/**订单数据标志Key*/
	IOFlagKey: "ReaBmsOrderDocIOFlag",
	/**下拉状态默认值*/
	defaultStatusValue: "",
	/**供应商ID*/
	ReaCompID: null,
	ReaCompCName: null,
	
	/**用户UI配置Key*/
	userUIKey: 'confirm.choose.order.OrderCheck',
	/**用户UI配置Name*/
	userUIName: "订单验收订单选择列表",
	
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '订货单号',
			fields: ['reabmscenorderdoc.OrderDocNo']
		};
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);

		me.dockedItems = me.dockedItems || [];
		me.buttonToolbarItems = me.createButtonToolbarItems2();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsCenOrderDoc_OperDate',
			text: '申请日期',
			align: 'center',
			width: 90,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_CompanyName',
			text: '供货商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaCompCode',
			text: '供货商编码',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaCompanyName',
			text: '本地供货商',
			hidden: true,
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Status',
			text: '单据状态',
			align: 'center',
			width: 65,
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
			dataIndex: 'ReaBmsCenOrderDoc_OrderDocNo',
			text: '订货单号',
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_TotalPrice',
			text: '总价',
			width: 75,
			renderer: function(value, meta) {
				var v = value || '';
				if(v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_UrgentFlag',
			text: '紧急标志',
			align: 'center',
			width: 65,
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
			width: 60,
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
			dataIndex: 'ReaBmsCenOrderDoc_UserName',
			text: '订购人员',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_Checker',
			text: '审核人员',
			width: 90,
			hidden: true,
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
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerCompCode',
			text: '供应商机平台构码',
			width: 100,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenOrderDoc_ReaServerLabcCode',
			text: '订货方机平台构码',
			width: 100,
			hidden: true,
			defaultRenderer: true
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
		var deptIdv = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
		var deptNamev = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		var items = ['refresh', '-'];
		items.push({
			fieldLabel: '供货商主键ID',
			hidden: true,
			xtype: 'textfield',
			itemId: 'ReaCompID',
			value: me.ReaCompID
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
			width: 235,
			value: me.ReaCompCName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onCompAccept(record);
							p.close();
							me.onSearch();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '所属部门',
			name: 'DeptName',
			itemId: 'DeptName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.out.basic.CheckTree',
			classConfig: {
				title: '部门选择',
				checkOne: true
			},
			labelWidth: 65,
			labelAlign: 'right',
			value: deptNamev,
			listeners: {
				check: function(p, record) {
					if(record && record.get("tid") == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDepAccept(record);
					me.onSearch();
					p.close();
				}
			},
			width: 230
		}, {
			fieldLabel: '所属部门',
			name: 'DeptID',
			itemId: 'DeptID',
			xtype: 'textfield',
			hidden: true,
			width: 160,
			value: deptIdv
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
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		ComId.setValue(id);
		ComName.setValue(text);
	},
	/**使用部门选择*/
	onDepAccept: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var DeptID = buttonsToolbar.getComponent('DeptID');
		var DeptName = buttonsToolbar.getComponent('DeptName');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		DeptID.setValue(id);
		DeptName.setValue(text);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar'),
			ReaCompID = buttonsToolbar2.getComponent('ReaCompID');
		var search = buttonsToolbar2.getComponent('Search');
		var DeptID = buttonsToolbar2.getComponent('DeptID');

		var where = [];
		if(ReaCompID) {
			var value = ReaCompID.getValue();
			if(value) {
				where.push("reabmscenorderdoc.ReaCompID=" + value);
			}
		}
		if(DeptID) {
			var DeptIDVal = DeptID.getValue();
			if(DeptIDVal) {
				where.push("reabmscenorderdoc.DeptID=" + DeptIDVal);
			}
		}
		//审核通过,审批通过或上传,部分验收
		where.push("reabmscenorderdoc.ReaCompID is not null and (reabmscenorderdoc.Status=3 or reabmscenorderdoc.Status=4 or reabmscenorderdoc.Status=5 or reabmscenorderdoc.Status=12) and reabmscenorderdoc.DeleteFlag=0");
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
		var id = record.get("ReaBmsCenOrderDoc_Id");
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
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.StatusKey].List));
		return tempList;
	}
});