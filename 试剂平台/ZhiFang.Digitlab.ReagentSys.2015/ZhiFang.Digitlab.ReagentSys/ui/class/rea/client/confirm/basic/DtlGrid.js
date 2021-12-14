/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.basic.DtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '验货单明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirmByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtlConfirm',
	/**新增数据服务路径*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDtlConfirm',
	/**修改数据服务路径*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlConfirmByField',
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
			property: 'BmsCenSaleDtlConfirm_ProdGoodsNo',
			direction: 'ASC'
		},
		{
			property: 'BmsCenSaleDtlConfirm_GoodsUnit',
			direction: 'ASC'
		}
	],
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
	canEdit: true,
	/**是否多选行*/
	checkOne: true,
	StatusList: [],
	/**申请单状态枚举*/
	StatusEnum: {},
	/**申请单状态背景颜色枚举*/
	StatusBGColorEnum: {},
	StatusFColorEnum: {},
	StatusBGColorEnum: {},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			beforeedit: function(editor, e) {
				return me.canEdit;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.getStatusListData();
		if(me.canEdit == true) {
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}
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
			dataIndex: 'BmsCenSaleDtlConfirm_BarCodeMgr',
			text: '条码类型',
			hidden: true,
			width: 60,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '产品编号',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_ReaGoodsName',
			text: '产品名称',
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("BmsCenSaleDtlConfirm_BarCodeMgr");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_GoodsUnit',
			text: '单位',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_UnitMemo',
			text: '包装规格',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_Status',
			text: '状态',
			hidden: false,
			width: 75,
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
			dataIndex: 'BmsCenSaleDtlConfirm_LotNo',
			text: '产品批号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_InvalidDate',
			text: '有效期至',
			width: 75,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_Price',
			text: '单价',
			width: 50,
			type: 'float',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_GoodsQty',
			text: '购进数',
			width: 50,
			type: 'int',
			align: 'center'
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_SumTotal',
			sortable: false,
			text: '金额',
			align: 'center',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_InCount',
			text: '已入库',
			width: 50,
			type: 'int',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_AcceptCount',
			style: 'font-weight:bold;color:#fff;background:#5cb85c;',
			text: '接收数',
			width: 50,
			type: 'int',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_RefuseCount',
			text: '拒收数',
			style: 'font-weight:bold;color:#fff;background:#c9302c;',
			width: 50,
			type: 'int',
			align: 'center',
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_AcceptMemo',
			sortable: false,
			text: '<b style="color:red;">异常信息</b>',
			width: 60,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_ProdDate',
			text: '生产日期',
			align: 'center',
			width: 85,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_BiddingNo',
			text: '招标号',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_TaxRate',
			text: '税率',
			align: 'right',
			width: 40,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_RegisterNo',
			sortable: false,
			text: '注册证编号',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_RegisterInvalidDate',
			text: '注册证有效期',
			width: 85,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '厂商产品编号',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_GoodsSerial',
			sortable: false,
			text: '产品条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_PackSerial',
			sortable: false,
			text: '包装单位条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_MixSerial',
			sortable: false,
			text: '混合条码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_OrderGoodsID',
			sortable: false,
			text: '供应商与货品关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_GoodsNo',
			sortable: false,
			text: '货品平台编码',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirm_ApproveDocNo',
			sortable: false,
			text: '批准文号',
			hidden: true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = [];

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**总金额自动计算*/
	onPriceOrGoodsQtyChanged: function(record) {
		var me = this;
		var Price = record.get('BmsCenSaleDtlConfirm_Price');
		var GoodsQty = record.get('BmsCenSaleDtlConfirm_GoodsQty');
		var SumTotal = parseFloat(Price) * parseInt(GoodsQty);
		record.set('BmsCenSaleDtlConfirm_SumTotal', SumTotal);
	},
	/**刷新数据*/
	onSearch: function() {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = true;
		this.load(null, true);
	},
	/**只看模式*/
	onSearchOnlyRead: function() {
		var me = this;
		me.ErrorMsg = '';
		me.canEdit = false;
		this.load(null, true);
	},
	/**获取数据错误信息*/
	getDataErrorMsg: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length,
			errorMsg = null;

		for(var i = 0; i < len; i++) {
			var rec = records[i],
				GoodsQty = rec.get('BmsCenSaleDtlConfirm_GoodsQty'),
				AcceptCount = rec.get('BmsCenSaleDtlConfirm_AcceptCount'),
				AccepterErrorMsg = rec.get('BmsCenSaleDtlConfirm_AcceptMemo');

			if(GoodsQty == AcceptCount && AccepterErrorMsg) {
				errorMsg = '验收数量与供货数量一致时，不能填写异常信息，请删除该条异常信息后再操作！';
				break;
			} else if(AcceptCount > GoodsQty) {
				errorMsg = '验收数量不能大于供货数量，请修改后再操作！';
				break;
			} else if(AcceptCount < GoodsQty && !AccepterErrorMsg) {
				errorMsg = '验收数量小于供货数量时，必须填写异常信息，请修改后再操作！';
				break;
			}
		}
		return errorMsg;
	},
	deleteOne: function(record) {
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		var showMask = false;
		var id = record.get(me.PKField);
		if(!id || id == "-1") {
			me.delCount++;
			me.store.remove(record);
			if((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
				me.fireEvent('onDelAfter', me);
			}
		} else {
			if(showMask == false) {
				showMask = true;
				me.showMask(me.delText); //显示遮罩层
			}
			me.delOneById(record, 1, id);
		}
	},
	/**删除一条数据*/
	delOneById: function(record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount != 0) {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					} else {
						me.fireEvent('onDelAfter', me);
					}
				}
			});
		}, 100 * index);
	},
	/**获取供货验收总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "BmsCenSaleDtlConfirmStatus",
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
					if(data.value[0].BmsCenSaleDtlConfirmStatus.length > 0) {
						me.StatusList.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']);
						Ext.Array.each(data.value[0].BmsCenSaleDtlConfirmStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
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
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();
		if(!me.PK) return false;
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		me.setButtonsDisabled(me.buttonsDisabled);
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;
	},
	onFullScreenClick: function() {
		var me = this;
		me.fireEvent('onFullScreenClick', me);
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
			me.updateOneInfo(i, records[i]);
		}
	},
	/**获取单个的修改封装信息*/
	getOneUpdateInfo: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var entity = {
			Id: id,
			GoodsUnit: record.get("BmsCenSaleDtlConfirm_GoodsUnit"),
			UnitMemo: record.get("BmsCenSaleDtlConfirm_UnitMemo"),
			GoodsNo: record.get("BmsCenSaleDtlConfirm_GoodsNo"),
			UnitMemo: record.get("BmsCenSaleDtlConfirm_UnitMemo"),
			BiddingNo: record.get("BmsCenSaleDtlConfirm_BiddingNo"),
			LotNo: record.get("BmsCenSaleDtlConfirm_LotNo"),
			ProdGoodsNo: record.get("BmsCenSaleDtlConfirm_ProdGoodsNo"),
			ApproveDocNo: record.get("BmsCenSaleDtlConfirm_ApproveDocNo"),
			RegisterNo: record.get("BmsCenSaleDtlConfirm_RegisterNo"),
			AcceptMemo: record.get("BmsCenSaleDtlConfirm_AcceptMemo")
		};
		var ProdDate = record.get("BmsCenSaleDtlConfirm_ProdDate");
		var InvalidDate = record.get("BmsCenSaleDtlConfirm_InvalidDate");
		var RegisterInvalidDate = record.get("BmsCenSaleDtlConfirm_RegisterInvalidDate");

		if(ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if(InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if(RegisterInvalidDate) entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);

		var TaxRate = record.get("BmsCenSaleDtlConfirm_TaxRate");
		if(TaxRate) entity.TaxRate = TaxRate;
		return entity;
	},
	getUpdateFields: function(record) {
		var me = this;
		var fields = [
			'Id', 'GoodsUnit', 'UnitMemo', 'GoodsNo', 'BiddingNo', 'LotNo', 'ProdGoodsNo', 'ApproveDocNo', 'RegisterNo', 'AcceptMemo', 'TaxRate'
		];
		fields.push("ProdDate", "InvalidDate", "RegisterInvalidDate");
		//fields.push("GoodsQty", "Price", "AcceptCount", "RefuseCount", "SumTotal");
		return fields.join(',');
	},
	/**修改单个*/
	updateOneInfo: function(index, record) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);
		var entity = me.getOneUpdateInfo(record);
		var params = JShell.JSON.encode({
			entity: entity,
			fields: me.getUpdateFields()
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
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
					if(me.saveErrorCount == 0) {
						me.onSearch();
					} else {
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var status = buttonsToolbar.getComponent('DtlConfirmStatus');
		var where = [];
		if(status) {
			var value = status.getValue();
			if(value) {
				where.push("bmscensaledtlconfirm.Status=" + value);
			}
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				where.push(me.getSearchWhere(value));
			}
		}
		where.push("bmscensaledtlconfirm.ReaGoodsID is not null");
		return where.join(" and ");
	}
});