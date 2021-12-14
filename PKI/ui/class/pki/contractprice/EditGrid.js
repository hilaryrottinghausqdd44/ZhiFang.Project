/**
 * 合同价格设置列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.EditGrid', {
	//extend:'Shell.ux.grid.Panel',
	extend: 'Shell.ux.grid.PostPanel',
	title: '合同价格设置列表',

	/**获取数据服务路径*/
	//selectUrl:'/BaseService.svc/ST_UDTO_SearchDContractPriceByHQL?isPlanish=true',
	//	selectUrl: '/StatService.svc/Stat_UDTO_SearchLabContractPrice',

	selectUrl: '/StatService.svc/Stat_UDTO_SearchDContractPrice',
	/**修改服务地址*/
	editUrl: '/StatService.svc/Stat_UDTO_UpdateDContractPriceByField',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DContractPrice_StepPrice',
		direction: 'ASC'
	}],
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**是否带修改价格功能*/
	canEditPrice: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.getComponent('filterToolbar').on({
			search: function(p, params) {
				me.onSearch();
			}
		});
	},
	initComponent: function() {
		var me = this;

		if (me.canEditPrice) {
			me.buttonToolbarItems = ['save'];
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}

		//数据列
		me.columns = me.createGridColumns();

		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.contractprice.SearchToolbar', {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			height: 80
		})];

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];

		columns.push({
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
			dataIndex: 'DContractPrice_ContractNo',
			text: '合同编号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_ContractType',
			text: '合同类型',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ContractType2['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DContractPrice_BLaboratory_CName',
			text: '送检单位',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BDealer_Name',
			text: '经销商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BTestItem_CName',
			text: '项目',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_IsStepPrice',
			text: '是否阶梯价',
			width: 80,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'DContractPrice_SampleCount',
			text: '数量',
			width: 80,
			sortable: false
		});

		if (me.canEditPrice) {
			columns.push({
				dataIndex: 'DContractPrice_StepPrice',
				text: '<b style="color:blue;">价格</b>',
				width: 80,
				type: 'float',
				align: 'right',
				editor: {
					xtype: 'numberfield',
					minValue: 0,
					allowBlank: false
				},
				renderer: function(value, meta) {
					var v = value == null ? '' : value;

					if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';

					if (!value || value == 0 || value == '0') {
						meta.style = 'background-color:red;';
					}

					return v;
				}
			});
		} else {
			columns.push({
				dataIndex: 'DContractPrice_StepPrice',
				text: '价格',
				width: 80,
				type: 'float',
				align: 'right',
				renderer: function(value, meta) {
					var v = value == null ? '' : value;

					if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';

					if (!value || value == 0 || value == '0') {
						meta.style = 'background-color:red;';
					}

					return v;
				}
			});
		}

		columns.push({
			dataIndex: 'DContractPrice_BDealer_BBillingUnit_Name',
			hidden: true,
			text: '开票方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BillingUnitType',
			text: '开票方类型',
			width: 80,
			hidden: true,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DContractPrice_AddUser',
			hidden: true,
			text: '合同录入人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_ConfirmUser',
			hidden: true,
			text: '合同确认人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		});

		if (me.canEditPrice) {
			columns.push({
				dataIndex: me.DelField,
				text: '',
				width: 40,
				hideable: false,
				sortable: false,
				renderer: function(value) {
					var v = '';
					if (value === 'true') {
						v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
					}
					if (value === 'false') {
						v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
					}
					return v;
				}
			});
		}

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.doFilterParams();
		return me.callParent(arguments);
	},
	/**@overwrite 条件处理*/
	doFilterParams: function() {
		var me = this,
			params = me.params || {},
			ParaClass = {};
		params = me.getComponent('filterToolbar').getParams();
		if (params.DateType) {
			ParaClass.DateType = params.DateType;
		}
		if (params.StartDate) {
			ParaClass.BeginDate = params.StartDate;
		}
		if (params.EndDate) {
			ParaClass.EndDate = params.EndDate;
		}

		if (params.ContractNo) {
			ParaClass.ContractNo = params.ContractNo;
		}
		if (params.CoopLevel) {
			ParaClass.ContractType = params.CoopLevel;
		}
		if (params.Dealer_Id) {
			ParaClass.DealerID = params.Dealer_Id;
		}
		if (params.Laboratory_Id) {
			ParaClass.SLabID = params.Laboratory_Id;
		}

		if (params.TestItem_Id) {
			ParaClass.ItemID = params.TestItem_Id;
		}
		if (params.BillingUnitType) {
			ParaClass.BillingUnitType = params.BillingUnitType;
		}
		if (params.BillingUnit_Id) {
			ParaClass.BillingUnitID = params.BillingUnit_Id;
		}
		if (params.IsStepPrice && params.IsStepPrice != undefined && params.IsStepPrice != null) {
			ParaClass.IsStepPrice = params.IsStepPrice;
		}
		me.postParams = {
			entity: ParaClass,
			isPlanish: true,
			fields: me.getStoreFields(true).join(',')
		};
	},
	getInfoPostParams: function() {
		var me = this;
		var parObj = me.getInfoParObj();
		if (!parObj) return;
		var postParams = me.postParams;
		for (var i in parObj) {
			postParams.entity[i] = parObj[i];
		}
		return postParams;
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
			if (rec && rec.dirty == true) {
				var id = rec.get("DContractPrice_Id");
				var price = rec.get('DContractPrice_StepPrice');
				me.updateOneByPrice(id, price, rec);
			} else {
				me.hideMask(); //隐藏遮罩层
			}
		}
		me.onSearch();
	},
	/**修改价格*/
	updateOneByPrice: function(id, price, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				StepPrice: price
			},
			fields: 'Id,StepPrice'
		});

		JShell.Server.post(url, params, function(data) {
			var record = me.store.findRecord("DContractPrice_Id", id);
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

		}, false);
	}
});