/**
 * 经销商与送检单位列表
 * @author longfc
 * @version 2016-05-17
 */
Ext.define('Shell.class.pki.dunitdealer.DUnitDealerGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '经销商与送检单位列表',

	//	width: 560,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDUnitDealerRelationByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelDUnitDealerRelation',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	//	autoScroll :true,
	//layout:'fit',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DUnitDealerRelation_BeginDate',
		direction: 'ASC'
	},{
		property: 'DUnitDealerRelation_BLaboratory_CName',
		direction: 'ASC'
	},{
		property: 'DUnitDealerRelation_BTestItem_CName',
		direction: 'ASC'
	},{
		property: 'DUnitDealerRelation_BBillingUnit_Name',
		direction: 'ASC'
	}],
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**只读*/
	readOnly: false,
	hasDel: true,
	/**复选框*/
	multiSelect: true,
	PKField: 'DUnitDealerRelation_Id',
	selType: 'checkboxmodel',
	BDealerId: null,
	BDealerName: null,
	BDealerDataTimeStamp: null,

	BDealerBBillingUnitId: null,
	BDealerBBillingUnitName: null,
	BDealerBBillingUnitDataTimeStamp: null,
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
		//自定义按钮功能栏
		me.buttonToolbarItems = [];
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '送检单位',
			isLike: true,
			fields: ['dunitdealerrelation.BLaboratory.CName']
		};

		if (me.hasButtons) {
			me.buttonToolbarItems.push({
				text: '批量新增',
				iconCls: 'button-add',
				tooltip: '<b>批量新增</b>',
				handler: function() {
					if(me.BDealerId=='0' || !me.BDealerId){
						JShell.Msg.error('请选择具体的经销商');
						return;
					}
					me.openBatchAddWin();
				}
			}, 'add', 'del'); //'add','edit','edit',
		}
		if (me.canEditPrice) {
			me.buttonToolbarItems.push('save');
		}
		me.buttonToolbarItems.push('->', {
			type: 'search',
			info: me.searchInfo
		});

		//数据列
		me.columns = [{
			dataIndex: 'DUnitDealerRelation_BeginDate',
			text: '起始日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DUnitDealerRelation_EndDate',
			text: '截止日期',
			width: 80,
			isDate: true,
			sortable: false
		}, {
			dataIndex: 'DUnitDealerRelation_BLaboratory_CName',
			text: '送检单位名称',
			width: 140,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitDealerRelation_BTestItem_CName',
			text: '项目',
			width: 140,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitDealerRelation_BillingUnitType',
			text: '开票方类型',
			width: 70,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'DUnitDealerRelation_BBillingUnit_Name',
			text: '开票方',
			width: 140,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitDealerRelation_BDept_CName',
			text: '科室',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitDealerRelation_LinkMan',
			text: '联系人',
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitDealerRelation_PhoneNum1',
			text: '联系电话',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitDealerRelation_Emall',
			text: '电子邮件',
			width: 110,
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'DUnitDealerRelation_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'DUnitDealerRelation_BLaboratory_Id',
			text: '送检单位主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitDealerRelation_BLaboratory_DataTimeStamp',
			text: '送检单位时间戳',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitDealerRelation_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitDealerRelation_BBillingUnit_Id',
			text: '默认开票方ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'DUnitDealerRelation_BBillingUnit_DataTimeStamp',
			text: '默认开票方时间戳',
			hidden: true,
			hideable: false
		}];

		me.callParent(arguments);
	},
	/**根据经销商ID获取数据*/
	loadByBDealerId: function(id) {
		var me = this;
		me.defaultWhere = 'dunitdealerrelation.BDealer.Id=' + id;
		me.onSearch();
	},
	/**批量新增*/
	openBatchAddWin: function() {
		var me = this;
		//当前经销商已维护的送检单位信息
		var BLaboratoryArr = [],
			arrId = [];
		var store = me.store;
		store.each(function(record) {
			var id = record.get("DUnitDealerRelation_BLaboratory_Id");
			var obj = {
				"BLaboratory_Id": id,
				"BLaboratory_CName": record.get("DUnitDealerRelation_BLaboratory_CName"),
				"BLaboratory_DataTimeStamp": record.get("DUnitDealerRelation_BLaboratory_DataTimeStamp"),

				"BLaboratory_BBillingUnit_Id": record.get("DUnitDealerRelation_BBillingUnit_Id"),
				"BLaboratory_BBillingUnit_Name": record.get("DUnitDealerRelation_BBillingUnit_Name"),
				"BLaboratory_BBillingUnit_DataTimeStamp": record.get("DUnitDealerRelation_BBillingUnit_DataTimeStamp")
			};
			if (!Ext.Array.contains(arrId, id)) {
				arrId.push(id);
				BLaboratoryArr.push(obj);
			}

		});
		var width = document.body.clientWidth*0.76;
		var height = document.body.clientHeight*0.88;
		JShell.Win.open('Shell.class.pki.dunitdealer.BatchAddApp', {
			resizable: false,
			width: width,
			height: height,
			BDealerId: me.BDealerId,
			BDealerName: me.BDealerName,
			BDealerDataTimeStamp: me.BDealerDataTimeStamp,
			//当前经销商已维护的送检单位信息
			BLaboratoryArr: BLaboratoryArr,
			BDealerBBillingUnitId: me.BDealerBBillingUnitId,
			BDealerBBillingUnitName: me.BDealerBBillingUnitName,
			BDealerBBillingUnitDataTimeStamp: me.BDealerBBillingUnitDataTimeStamp,
			listeners: {
				save: function(p) {
					me.onSearch();
					p.close();
				}
			}
		}).show();
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		if(me.BDealerId=='0' || !me.BDealerId){
			JShell.Msg.error('请选择具体的经销商');
			return;
		}
		me.openAddForm();
	},
	/**打开经销商与送检单位的表单*/
	openAddForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			BDealerId: me.BDealerId,
			BDealerName: me.BDealerName,
			BDealerDataTimeStamp: me.BDealerDataTimeStamp,

			BDealerBBillingUnitId: me.BDealerBBillingUnitId,
			BDealerBBillingUnitName: me.BDealerBBillingUnitName,
			BDealerBBillingUnitDataTimeStamp: me.BDealerBBillingUnitDataTimeStamp,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.dunitdealer.AddForm', config).show();

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
		me.openAddForm(id);
	}

});