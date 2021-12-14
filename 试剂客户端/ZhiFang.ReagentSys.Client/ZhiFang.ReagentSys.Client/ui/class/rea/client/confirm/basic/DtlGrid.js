/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.DtlGrid',
	requires: [
		'Ext.ux.RowExpander'
	],
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL?isPlanish=true',
	/**修改数据服务路径*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDtlConfirmByField',
	
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10000, 10000]
	],
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**明细按钮的启用状态*/
	buttonsDisabled: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDtlConfirm_ReaGoodsNo',
		direction: 'ASC'
	}],
	/**默认选中*/
	autoSelect: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**主单信息*/
	DocInfo: {},
	/**默认选中*/
	autoSelect: false,
	/**是否可编辑*/
	canEdit: false,
	/**是否多选行*/
	checkOne: true,
	StatusList: [],
	/**申请单状态枚举*/
	StatusEnum: {},
	/**申请单状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	/**是否默认开启全屏模式*/
	isLaunchFullscreen: false,

	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.on({
			beforeedit: function (editor, e) {
				return me.canEdit;
			}
		});
	},
	initComponent: function () {
		var me = this;
		me.getStatusListData();
		me.callParent(arguments);
	},
	setCheckboxModel: function () {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**创建数据列*/
	createGridColumns: function () {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsCenSaleDtlConfirm_BarCodeType',
			text: '条码类型',
			hidden: true,
			width: 60,
			renderer: function (value, meta) {
				var v = "";
				if (value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},
		{
			dataIndex: 'ReaBmsCenSaleDtlConfirm_ReaGoodsNo',
			text: '货品编码',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_CenOrgGoodsNo',
			sortable: false,
			text: '供应商货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_ReaGoodsName',
			text: '货品名称',
			width: 160,
			renderer: function (value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsCenSaleDtlConfirm_BarCodeType");
				if (!barCodeMgr) barCodeMgr = "";
				if (barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if (barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if (value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_ProdOrgName',
			text: '厂家名称',
			width: 55,
			defaultRenderer: true
		},{
			dataIndex: 'ReaBmsCenSaleDtlConfirm_GoodsUnit',
			text: '单位',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_Status',
			text: '状态',
			hidden: false,
			width: 75,
			renderer: function (value, meta) {
				var v = value;
				if (me.StatusEnum != null)
					v = me.StatusEnum[value];
				var bColor = "";
				if (me.StatusBGColorEnum != null)
					bColor = me.StatusBGColorEnum[value];
				var fColor = "";
				if (me.StatusFColorEnum != null)
					fColor = me.StatusFColorEnum[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_LotNo',
			text: '批号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_InvalidDate',
			text: '有效期至',
			width: 75,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_GoodsQty',
			text: '购进数',
			width: 50,
			type: 'float'
			//align: 'center'
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_InCount',
			text: '已入库',
			width: 50,
			type: 'float',
			//align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_AcceptCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '接收数',
			width: 50,
			type: 'float',
			//align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_RefuseCount',
			text: '拒收数',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 50,
			type: 'float',
			//align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_Price',
			text: '单价',
			width: 70,
			type: 'float',
			//align: 'right',
			renderer: function (value, meta) {
				var v = value || '';
				if (v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_SumTotal',
			sortable: false,
			text: '金额',
			//align: 'right',
			width: 70,
			renderer: function (value, meta) {
				var v = value || '';
				if (v && ("" + v).indexOf(".") >= 0) {
					v = parseFloat(v).toFixed(2);
					meta.tdAttr = 'data-qtip="<b>' + v + '元</b>"';
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_AcceptMemo',
			sortable: false,
			text: '<b style="color:red;">异常信息</b>',
			width: 60,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_ProdDate',
			text: '生产日期',
			align: 'center',
			width: 85,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_BiddingNo',
			text: '招标号',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_TaxRate',
			text: '税率',
			align: 'right',
			width: 40,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_RegisterNo',
			sortable: false,
			text: '注册证编号',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_RegisterInvalidDate',
			text: '注册证有效期',
			width: 85,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_GoodsSerial',
			sortable: false,
			text: '货品条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_PackSerial',
			sortable: false,
			text: '包装单位条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_MixSerial',
			sortable: false,
			text: '混合条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_CompGoodsLinkID',
			sortable: false,
			text: '供应商与货品关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_FactoryOutTemperature',
			text: '厂家出库温度',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_ArrivalTemperature',
			text: '到货温度',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDtlConfirm_AppearanceAcceptance',
			text: '外观验收',
			width: 80,
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function () {
		var me = this;
		var items = [];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**刷新数据*/
	onSearch: function () {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = true;
		this.load(null, true);
	},
	/**只看模式*/
	onSearchOnlyRead: function () {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = false;
		this.load(null, true);
	},
	deleteOne: function (record) { },
	/**删除一条数据*/
	delOneById: function (record, index, id) { },
	/**获取供货验收总单状态参数*/
	getParams: function () {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "ReaBmsCenSaleDtlConfirmStatus",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		};
		return params;
	},
	/**获取状态信息*/
	getStatusListData: function (callback) {
		var me = this;
		if (me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode(me.getParams());
		me.StatusList = [];
		me.StatusEnum = {};
		me.StatusFColorEnum = {};
		me.StatusBGColorEnum = {};
		var tempArr = [];
		JcallShell.Server.post(url, params, function (data) {
			if (data.success) {
				if (data.value) {
					if (data.value[0].ReaBmsCenSaleDtlConfirmStatus.length > 0) {
						me.StatusList.push(["", '请选择', 'font-weight:bold;color:black;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsCenSaleDtlConfirmStatus, function (obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if (obj.FontColor) {
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if (obj.BGColor) {
								style.push('color:' + obj.BGColor);
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
	/**加载数据前*/
	onBeforeLoad: function () {
		var me = this;
		me.getView().update();
		if (!me.PK) return false;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	onAfterLoad: function (records, successful) {
		var me = this;
		me.callParent(arguments);
	},
	setBtnDisabled: function (com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if (buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if (btn) btn.setDisabled(disabled);
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function () {
		var me = this,
			arr = [];
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function () {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var status = buttonsToolbar.getComponent('DtlConfirmStatus');
		var where = [];
		if (status) {
			var value = status.getValue();
			if (value) {
				where.push("reabmscensaledtlconfirm.Status=" + value);
			}
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				var searchHql = me.getSearchWhere(value);
				if (searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	}
});