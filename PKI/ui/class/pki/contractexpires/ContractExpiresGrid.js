/**
 *  合同到期查看
 */
Ext.define('Shell.class.pki.contractexpires.ContractExpiresGrid', {
	//  extend:'Shell.ux.grid.Panel',
	extend: 'Shell.ux.grid.PostPanel',
	title: '合同到期查看',
	/**获取数据服务路径*/
	// selectUrl:'',
	selectUrl: '/StatService.svc/Stat_UDTO_SearchDContractPriceWillExpire',
	/**修改服务地址*/
	editUrl: '/StatService.svc/ST_UDTO_UpdateDContractPriceByField',
	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_DContractPriceWillExpireToExcel',

	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**导出功能*/
	//	buttonToolbarItems:['exp_excel'],
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DContractPrice_EndDate',
		direction: 'ASC'
	}],
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 15000,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**正在获取数据,请稍候...*/
	loadingText: '',
	/**是否带修改价格功能*/
	canEditPrice: true,

	afterRender: function() {
		var me = this;
		me.store.on({
			load: function(store, records, successful) {
				me.onAfterLoad(records, successful);
				Ext.Msg.hide();
			},
			datachanged: function(store, eOpts) {
				Ext.Msg.hide();
			}
		});
		me.callParent(arguments);

		me.getComponent('filterToolbar').on({
			search: function(p, params) {
				me.params = params;
				Ext.Msg.wait("正在获取数据,请稍候...", '提示');
				me.onSearch();
			}
		});

		JShell.Action.delay(function() {
			Ext.Msg.wait("正在获取数据,请稍候...", '提示');
			me.onSearch()
		}, null, 600);
	},
	initComponent: function() {
		var me = this;

		if(me.canEditPrice) {
			me.buttonToolbarItems = [''];
			me.buttonToolbarItems.push('exp_excel');
			me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
				clicksToEdit: 1
			});
		}

		//数据列
		me.columns = me.createGridColumns();
		//创建数据集
		me.store = me.createStore();
		//创建挂靠功能栏
		me.dockedItems = me.dockedItems || [Ext.create('Shell.class.pki.contractexpires.SearchToolbar', {
			itemId: 'filterToolbar',
			dock: 'top',
			isLocked: true,
			height: 55
		})];

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];

		columns.push({
			dataIndex: 'DContractPrice_ContractType',
			text: '合同类型',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ContractType2['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DContractPrice_ContractNo',
			text: '合同编号',
			width: 80,
			sortable: false
		}, {
			dataIndex: 'DContractPrice_BDealer_Name',
			text: '经销商',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BLaboratory_CName',
			text: '送检单位',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BTestItem_CName',
			text: '项目',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_BeginDate',
			text: '合同开始日期',
			isDate: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'DContractPrice_EndDate',
			hidden: false,
			text: '合同截止日期',
			width: 100,
			renderer: function(value, meta) {
				if(value.toString().length > 0) {
					var v = value;
					//v = '2016-02-23';
					var bgcolor = '';

					v = Ext.util.Format.date(v, 'Y-m-d');
					var now = Date.parse(Ext.util.Format.date(new Date(), 'Y-m-d'));
					var date = Date.parse(v.replace(/-/g, "/"));
					var iDays = parseInt(Math.abs(date - now) / 1000 / 60 / 60 / 24);
					if(date < now) {
						//过期，红色提示
						bgcolor = 'red';
						meta.tdAttr = 'data-qtip="<b>' + "当前合同已过期" + iDays + "天" + '</b>"';
					} else if(iDays == 30) {
						//如果合同截止日期只有30天，黄色提示
						meta.tdAttr = 'data-qtip="<b>' + "合同距离截止日期还剩下30天" + '</b>"';
						bgcolor = 'yellow';
					}

					meta.style = 'color:balck;background-color:' + bgcolor || '#FFFFFF';
					return v;
				}
			}
		}, {
			dataIndex: 'DContractPrice_StepPrice',
			hidden: false,
			text: '价格',
			width: 100,
			defaultRenderer: true
		});

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
		if(params.ContractNo) {
			ParaClass.ContractNo = params.ContractNo;
		}
		if(params.CoopLevel) {
			ParaClass.ContractType = params.CoopLevel;
		}
		if(params.Dealer_Id) {
			ParaClass.DealerID = params.Dealer_Id;
		}
		if(params.Laboratory_Id) {
			ParaClass.SLabID = params.Laboratory_Id;
		}

		if(params.TestItem_Id) {
			ParaClass.ItemID = params.TestItem_Id;
		}
		if(params.BillingUnitType) {
			ParaClass.BillingUnitType = params.BillingUnitType;
		}
		if(params.BillingUnit_Id) {
			ParaClass.BillingUnitID = params.BillingUnit_Id;
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
		if(!parObj) return;
		var postParams = me.postParams;
		for(var i in parObj) {
			postParams.entity[i] = parObj[i];
		}
		return postParams;
	},
	getInfoPostParams: function() {
		var me = this;
		var parObj = me.getInfoParObj();
		if(!parObj) return;
		var postParams = me.postParams;
		for(var i in parObj) {
			postParams.entity[i] = parObj[i];
		}
		return postParams;
	},
	/**获取详细信息参数对象*/
	getInfoParObj: function() {
		var me = this,
			params = me.params || {},
			ParaClass = {};
		params = me.getComponent('filterToolbar').getParams();

		var DealerID = params.Dealer_Id; //经销商
		var ItemID = params.TestItem_Id; //项目
		var SLabID = params.SLabID; //送检单位
		var parObj = {};
		var error = [];
		if(DealerID != null) {
			parObj.DealerID = DealerID;
		}
		if(ItemID != null) {
			parObj.ItemID = ItemID;
		}
		if(SLabID != null) {
			parObj.SLabID = SLabID;
		}
		if(error.length > 0) {
			JShell.Msg.error(error.join('</br>'));
			return;
		}
		return parObj;
	},
	/**导出EXCEL文件*/
	onExpExcelClick: function() {
		var me = this;
		me.onDownLoadExcel();
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;

		var postParams = me.getReportParams();
		var reportTotalParams = me.getReportTotalParams();
		var entityJson = Ext.JSON.encode(postParams.entity);
		var detailJson = Ext.JSON.encode(reportTotalParams.entity);

		url += "?entityJson=" + entityJson + "&reportType=" + operateType;
		window.open(url);
	},
	getReportParams: function() {
		var me = this;
		var postParams = me.postParams;
		return postParams;
	},
	getReportTotalParams: function() {
		var me = this;
		var postParams = me.postParams;
		return postParams;
	}
});