/**
 * 供货单选择基础类
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.choose.sale.SaleDocCheck', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '本地供货单选择',
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否多选行*/
	checkOne: true,
	/**客户端供货单及明细状态Key*/
	StatusKey: "ReaBmsCenSaleDocAndDtlStatus",
	/**客户端供货单及明细数据标志Key*/
	IOFlagKey: "ReaBmsCenSaleDocIOFlag",

	/**供应商ID*/
	ReaCompID: null,
	ReaCompCName: null,
	ReaServerCompCode: null,
	/**访问BS平台的URL*/
	BSPlatformURL: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);

		me.dockedItems = me.dockedItems || [];
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
				dataIndex: 'ReaBmsCenSaleDoc_CheckTime',
				text: '审核日期',
				align: 'center',
				width: 130,
				isDate: true,
				hasTime: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_CompID',
				text: '供货商Id',
				hidden: true,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_CompanyName',
				text: '供货商',
				width: 120,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_ReaServerCompCode',
				text: '供货商平台构码',
				width: 95,
				hideable: false
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_ReaCompCode',
				text: '供货商编码',
				width: 100,
				hidden: true,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_ReaLabcCode',
				text: '订货方编码',
				width: 100,
				hidden: true,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_LabcID',
				text: '订货方Id',
				hidden: true,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_LabcName',
				text: '订货方',
				width: 120,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_ReaServerLabcCode',
				text: '订货方平台构码',
				width: 95,
				hideable: false
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_SaleDocNo',
				text: '供货单号',
				width: 145,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_InvoiceNo',
				text: '发票号',
				width: 85,
				defaultRenderer: true
			},
			{
				dataIndex: 'ReaBmsCenSaleDoc_Status',
				text: '单据状态',
				align: 'center',
				width: 75,
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
				dataIndex: 'ReaBmsCenSaleDoc_OrderDocNo',
				text: '订货单号',
				hidden: true,
				width: 120,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_OrderDocID',
				text: '订货单号ID',
				hidden: true,
				width: 120,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_TotalPrice',
				text: '总价',
				width: 100,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_UrgentFlag',
				text: '紧急标志',
				align: 'center',
				width: 65,
				renderer: function(value, meta) {
					var v = JShell.REA.Enum.BmsCenOrderDoc_UrgentFlag['E' + value] || '';
					var result = value;
					if(v && v.value) {
						result = v.value;
						meta.tdAttr = 'data-qtip="<b>' + v.value + '</b>"';
					}
					meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
					return result;
				}
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_IOFlag',
				text: '数据标志',
				align: 'center',
				width: 65,
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
				dataIndex: 'ReaBmsCenSaleDoc_UserName',
				text: '操作人员',
				width: 95,
				defaultRenderer: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_Memo',
				text: '备注',
				hidden: true,
				width: 200,
				renderer: function(value, meta) {
					return "";
				}
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_Id',
				text: '主键ID',
				hidden: true,
				hideable: false,
				isKey: true
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_ReaCompID',
				text: '本地供货商ID',
				hidden: true,
				hideable: false
			}, {
				dataIndex: 'ReaBmsCenSaleDoc_ReaCompanyName',
				text: '本地供货商',
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
			}
		];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		me.buttonToolbarItems.unshift(2, 'refresh', {
			type: 'search',
			info: me.searchInfo
		}, '-');
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
		var id = record.get("ReaBmsCenSaleDoc_Id");
		var config = {
			title: '供货单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: me.StatusKey //类名
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