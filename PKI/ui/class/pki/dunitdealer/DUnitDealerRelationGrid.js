/**
 *  送检单位与经销商关系与合同列表
 */
Ext.define('Shell.class.pki.dunitdealer.DUnitDealerRelationGrid', {
	extend: 'Shell.ux.grid.PostPanel',
	title: '送检单位与经销商关系与合同列表',
	/**获取数据服务路径*/
	selectUrl: '/StatService.svc/Stat_UDTO_SearchLabContractPrice',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: true,

	LaboratoryId: '',
	/**导出功能*/
	//	buttonToolbarItems:['exp_excel'],
	/**下载EXCEL文件服务地址*/
	downLoadExcelUrl: '/StatService.svc/Stat_UDTO_LabContractPriceToExcel',

	/**导出EXCEL文件*/
	onExpExcelClick: function() {
		var me = this;
		me.doActionClick = true;
		me.onDownLoadExcel();
	},
	/**排序字段*/
	defaultOrderBy: [{
		property: 'LabContractPrice_StepPrice',
		direction: 'ASC'
	}],
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否带修改价格功能*/
	canEditPrice: true,
	SearchToolbarType: "SearchToolbar2",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		if(me.SearchToolbarType == 'SearchToolbar2') {
			me.onSearch();
			me.getComponent('filterToolbar2').on({
				search: function(p, params) {
					me.params = me.getComponent('filterToolbar2').getParams();
					me.onSearch();
				}
			});
		} else {
			me.getComponent('filterToolbar').on({
				search: function(p, params) {
					me.onSearch();
				}
			});
		}
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
		//创建挂靠功能栏
		if(me.dockedItems == undefined) {
			switch(me.SearchToolbarType) {
				case "SearchToolbar":
					me.dockedItems = [Ext.create('Shell.class.pki.dunitdealer.SearchToolbar', {
						itemId: 'filterToolbar',
						dock: 'top',
						isLocked: true,
						height: 30
					})];
					break;
				case "SearchToolbar2":
					me.dockedItems = [Ext.create('Shell.class.pki.dunitdealer.SearchToolbar2', {
						itemId: 'filterToolbar2',
						dock: 'top',
						isLocked: true
							//height:30
					})];
					break;
				default:
					break;
			}
		}
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [];
		columns.push({
			dataIndex: 'LabContractPrice_NFClientName',
			text: '送检单位',
			width: 100,
			sortable: true
		}, {
			dataIndex: 'LabContractPrice_NFDeptName',
			text: '科室',
			width: 80,
			sortable: false
		}, {
			dataIndex: 'LabContractPrice_ItemName',
			text: '项目',
			width: 140,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'LabContractPrice_CoopLevel',
			text: '合作级别',
			width: 0,
			//			defaultRenderer: true,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.ContractType2['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'LabContractPrice_DealerName',
			text: '经销商',
			width: 120,
			sortable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'LabContractPrice_DealerID',
			hidden: true,
			text: '经销商ID',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'LabContractPrice_IsStepPrice',
			text: '是否阶梯价',
			width: 80,
			align: 'center',
			isBool: true,
			type: 'bool'
		}, {
			dataIndex: 'LabContractPrice_StepPrice',
			text: '<b style="color:blue;">合同价格</b>',
			width: 100,
			renderer: function(value, meta) {
				var v = value == null ? '' : value;
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				if(!value || value == 0 || value == '0') {
					meta.style = 'background-color:red;';
				}
				return v;
			}
		}, {
			dataIndex: 'LabContractPrice_BillingUnitName',
			text: '开票方',
			width: 80,
			sortable: false
		}, {
			dataIndex: 'LabContractPrice_BillingUnitType',
			text: '开票方类型',
			width: 80,
			hidden: false,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'LabContractPrice_ContractNo',
			hidden: false,
			text: '合同编号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'LabContractPrice_BeginDate',
			hidden: false,
			text: '开始日期',
			width: 100,
			defaultRenderer: true,
			isDate: true
		}, {
			dataIndex: 'LabContractPrice_EndDate',
			hidden: false,
			text: '截至日期',
			width: 80,
			defaultRenderer: true,
			isDate: true
		}, {
			dataIndex: 'LabContractPrice_AddUser',
			hidden: false,
			text: '合同录入人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'LabContractPrice_ConfirmUser',
			hidden: false,
			text: '合同确认人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'LabContractPrice_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'LabContractPrice_ItemID',
			text: '项目ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'LabContractPrice_SLabID',
			text: '送检单位ID',
			hidden: true,
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

		if(me.SearchToolbarType == 'SearchToolbar2') {
			params = me.getComponent('filterToolbar2').getParams();
		} else {
			params = me.getComponent('filterToolbar').getParams();
			params.Laboratory_Id = me.LaboratoryId;
			//			params.BillingUnitType ='1';
		}
		if(params.DateType) {
			ParaClass.DateType = params.DateType;
		}
		if(params.StartDate) {
			ParaClass.BeginDate = params.StartDate;
		}
		if(params.EndDate) {
			ParaClass.EndDate = params.EndDate;
		}

		if(params.ContractNo) {
			ParaClass.ContractNo = params.ContractNo;
		}
		if(params.CoopLevel) {
			ParaClass.CoopLevel = params.CoopLevel;
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
		//alert(params.IsStepPrice);
		if(params.IsStepPrice && params.IsStepPrice != undefined && params.IsStepPrice != null) {
			ParaClass.IsStepPrice = params.IsStepPrice;
		}
		me.postParams = {
			entity: ParaClass,
			//			reportType:'1',
			isPlanish: true,
			//			fields:"LabContractPrice_DealerName,LabContractPrice_ItemName,LabContractPrice_BeginDate,LabContractPrice_DealerID,LabContractPrice_ItemID,LabContractPrice_CoopLevel,LabContractPrice_IsStepPrice,LabContractPrice_ContractNo,LabContractPrice_SLabID,LabContractPrice_BillingUnitType"
			fields: me.getStoreFields(true).join(',')
		};
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
		var postParams = me.getInfoPostParams();
		var entityJson = Ext.JSON.encode(postParams.entity);
		url += "?entityJson=" + entityJson + //"&detailJson=" + detailJson
			"&operateType=" + operateType;;
		window.open(url);
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

		if(me.SearchToolbarType == 'SearchToolbar2') {
			params = me.getComponent('filterToolbar2').getParams();
		} else {
			params = me.getComponent('filterToolbar').getParams();
			params.Laboratory_Id = me.LaboratoryId
		}
		var DealerID = params.Dealer_Id; //经销商
		var ItemID = params.TestItem_Id; //项目
		var BillingUnitType = params.BillingUnitType; //开票方

		var SLabID = params.SLabID; //开票方
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
	}
});