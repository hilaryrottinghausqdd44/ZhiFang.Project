/**
 * 经销商阶梯价格列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.stepprice.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '经销商阶梯价格列表',

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDContractPriceByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateDContractPriceByField',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelDContractPrice',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DContractPrice_BeginDate',
		direction: 'DESC'
	}, {
		property: 'DContractPrice_SampleCount',
		direction: 'DESC'
	}],
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	/**经销商ID*/
	DealerId: null,
	/**经销商时间戳*/
	DealerDataTimeStamp: null,
	/**项目ID*/
	ItemId: null,
	/**项目时间戳*/
	ItemDataTimeStamp: null,

	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		//me.searchInfo = {width:'100%',emptyText:'经销商名称',isLike:true,fields:[]};

		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh', 'add', 'copy', 'edit', 'del', 'save'];
		//数据列
		me.columns = [{
			dataIndex: 'DContractPrice_BeginDate',
			text: '开始日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_EndDate',
			text: '截止日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_SampleCount',
			text: '<b style="color:blue;">数量要求</b>',
			width: 100,
			editor: {
				xtype: 'numberfield'
			},
			sortable: false
		}, {
			dataIndex: 'DContractPrice_StepPrice',
			text: '<b style="color:blue;">价格</b>',
			width: 100,
			editor: {
				xtype: 'numberfield'
			},
			sortable: false
		}, {
			dataIndex: 'DContractPrice_ContractNo',
			text: '合同号',
			width: 100,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_StepPriceMemo',
			text: '阶梯价格备注',
			width: 200,
			defaultRenderer: true,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.openStepPriceForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		var id = records[0].get(me.PKField);
		me.openStepPriceForm(id);
	},
	/**打开表单*/
	openStepPriceForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			listeners: {
				save: function(win) {
					me.onSearch();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.DealerId = me.DealerId; //经销商ID
			config.DealerDataTimeStamp = me.DealerDataTimeStamp; //经销商时间戳
			config.ItemId = me.ItemId; //项目ID
			config.ItemDataTimeStamp = me.ItemDataTimeStamp; //项目时间戳
		}
		JShell.Win.open('Shell.class.pki.stepprice.Form', config).show();
	},
	/**点击复制按钮处理*/
	onCopyClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		JShell.Win.open('Shell.class.pki.stepprice.FormCopy', {
			formtype:'add',
			ContractNo: records[0].get('DContractPrice_ContractNo'),
			listeners: {
				save: function(win, data) {
					me.saveCopyIfo(win, data)
				}
			}
		}).show();
	},
	/**保存拷贝信息*/
	saveCopyIfo: function(win, data) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		win.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var count = rec.get('DContractPrice_SampleCount')
			var price = rec.get('DContractPrice_StepPrice');
			me.saveOneByEntity(win, rec.get(me.PKField), {
				BeginDate: data.BeginDate,
				EndDate: data.EndDate,
				ContractNo: data.ContractNo,

				StepPrice: price,
				SampleCount: count,

				StepPriceMemo: '数量＞' + count + '时，价格=' + price + '元',

				BDealer: {
					Id: me.DealerId,
					DataTimeStamp: me.DealerDataTimeStamp.split(',')
				},
				BTestItem: {
					Id: me.ItemId,
					DataTimeStamp: me.ItemDataTimeStamp.split(',')
				}
			});
		}
	},
	/**保存一条复制的数据*/
	saveOneByEntity: function(win, id, entity) {
		var me = this;
		var url = (win.addUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + win.addUrl;

		var params = Ext.JSON.encode({
			entity: entity
		});
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
				win.hideMask(); //隐藏遮罩层
				win.close();
				if (me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	},
	/**@overwrite 保存按钮点击处理方法*/
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
			var count = rec.get('DContractPrice_SampleCount');
			var price = rec.get('DContractPrice_StepPrice');
			me.updateOneBySampleCountAndPrice(id, count, price);
		}
	},
	/**修改数量和价格*/
	updateOneBySampleCountAndPrice: function(id, count, price) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				SampleCount: count,
				StepPrice: price,
				StepPriceMemo: '数量＞' + count + '时，价格=' + price + '元'
			},
			fields: 'Id,SampleCount,StepPrice,StepPriceMemo,ConfirmUser,ConfirmTime'
		});
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
		}, false);
	},
	/**根据经销商ID和项目ID获取数据*/
	loadByDealerIdAndItemId: function(dealerId, itemId) {
		var me = this;
		me.DealerId = dealerId;
		me.ItemId = itemId;
		me.defaultWhere = 'dcontractprice.BDealer.Id=' + dealerId +
			' and dcontractprice.BTestItem.Id=' + itemId;
		me.onSearch();
	}
});