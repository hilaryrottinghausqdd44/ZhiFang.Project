/**
 * 合同信息
 * @author longfc
 * @version 2016-12-28
 */
Ext.define('Shell.class.wfm.authorization.contract.Grid', {
	extend: 'Shell.ux.grid.Panel',

	title: '合同信息',
	width: 280,
	height: 100,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
	/**分页栏下拉框数据*/
	pageSizeList: null,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**后台排序*/
	remoteSort: true,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**默认加载*/
	defaultLoad: false,
	/**序号列宽度*/
	rowNumbererWidth: 32,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,
	PClientID: null,
	hiddenPContractName: true,
	defaultOrderBy: [{
		property: 'PContract_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '主键ID',
			dataIndex: 'PContract_Id',
			isKey: true,
			hidden: true,
			hideable: false
		},{
			text: '单项',
			dataIndex: 'PContract_EquipOneWayCount',
			width: 35,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '双向',
			dataIndex: 'PContract_EquipTwoWayCount',
			width: 35,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '仪器清单',
			dataIndex: 'PContract_LinkEquipInfoListHTML',
			width: 62,
			hidden: true,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				return "";
			}
		}, {
			xtype: 'actioncolumn',
			text: '仪器清单',
			align: 'center',
			width: 60,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					//var id = rec.get(me.PKField);
					me.showLinkEquipInfoListHTML(rec);
				}
			}]
		}, {
			text: '合同总额',
			dataIndex: 'PContract_Amount',
			width: 60,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '已收金额',
			dataIndex: 'PContract_PayedMoney',
			width: 60,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '剩余款项',
			dataIndex: 'PContract_LeftMoney',
			width: 60,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '合同编号',
			dataIndex: 'PContract_ContractNumber',
			width: 80,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '合同名称',
			dataIndex: 'PContract_Name',
			//width: 140,
			flex: 1,
			hidden: me.hiddenPContractName,
			sortable: false,
			defaultRenderer: true
		}];
		return columns;
	},
	/**仪器清单*/
	showLinkEquipInfoListHTML: function(rec) {
		var me = this;
		var listHTML = "";
		if(rec != null && rec != undefined)
			listHTML = rec.get("PContract_LinkEquipInfoListHTML");
		if(listHTML == "" || listHTML == null)
			listHTML = "无";
		JShell.Win.open('Ext.panel.Panel', {
			title: '仪器清单',
			html: listHTML,
			width: 800,
			height: 420,
			autoScroll: true,
			bodyPadding: 10
		}).show();
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		if(autoSelect == null || autoSelect == undefined)
			autoSelect = true;
		me.defaultWhere = 'pcontract.PClientID=' + me.PClientID;
		me.load(null, true, autoSelect);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		return me.callParent(arguments);
	}
});