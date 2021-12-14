/**
 * 批量新增经销商与送检单位关系
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dunitdealer.BatchAddApp', {
	extend: 'Ext.panel.Panel',
	title: '批量新增',
	layout: 'border',
	bodyPadding: 1,
	width: 880,
	height: 420,
	editBLaboratoryUrl: '/BaseService.svc/ST_UDTO_UpdateBLaboratoryByField',
	/**批量新增服务地址*/
	saveUrl: '/StatService.svc/Stat_UDTO_AddDUnitDealerRelation',
	BDealerId: null,
	BDealerName: null,
	BDealerDataTimeStamp: null,
	BDealerBBillingUnitId: null,
	BDealerBBillingUnitName: null,
	BDealerBBillingUnitDataTimeStamp: null,
	BLaboratoryArr: [], //当前经销商已维护的送检单位信息
	checkedValue: false, //当前经销商已维护的送检单位信息选择状态
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		var BLaboratoryCheckGrid = me.getComponent('BLaboratoryCheckGrid');
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var BatchAddForm = me.getComponent('BatchAddForm');
		UnitItemCheckGrid.getStore().on({
			load: function(store, records, successful) {
				UnitItemCheckGrid.getSelectionModel().selectAll();
			}
		});

		BatchAddForm.on({
			save: function() {
				me.onSaveInfo();
			}
		});
		//经销商选择改变后
		BatchAddForm.on({
			onDealerAccept: function(record) {
				var Id = record.get('BDealer_Id');
				var Name = record.get('BDealer_Name');
				var DataTimeStamp = record.get('BDealer_DataTimeStamp');
				me.BDealerId = Id;
				me.BDealerName = Name;
				me.BDealerDataTimeStamp = DataTimeStamp;
				if(me.BDealerId && me.BDealerId.toString().length > 0) {
					var url = '/BaseService.svc/ST_UDTO_SearchDUnitDealerRelationByHQL?isPlanish=true';
					url = (url.slice(0, 4) == 'http' ? '' : JcallShell.System.Path.ROOT) + url;
					url += "&fields=DUnitDealerRelation_BLaboratory_CName,DUnitDealerRelation_BLaboratory_Id,DUnitDealerRelation_BLaboratory_DataTimeStamp";
					url += "&where=dunitdealerrelation.BDealer.Id='" + me.BDealerId + "'";
					me.BLaboratoryArr = [];
					JcallShell.Server.get(url, function(data) {
						if(data.success) {
							var list = data.value.list;
							var arrId = [];

							Ext.Array.each(list, function(objRecord) {
								var id = objRecord["DUnitDealerRelation_BLaboratory_Id"];
								var obj = {
									"BLaboratory_Id": id,
									"BLaboratory_CName": objRecord["DUnitDealerRelation_BLaboratory_CName"],
									"BLaboratory_DataTimeStamp": objRecord["DUnitDealerRelation_BLaboratory_DataTimeStamp"],

									"BLaboratory_BBillingUnit_Id": objRecord["DUnitDealerRelation_BBillingUnit_Id"],
									"BLaboratory_BBillingUnit_Name": objRecord["DUnitDealerRelation_BBillingUnit_Name"],
									"BLaboratory_BBillingUnit_DataTimeStamp": objRecord["DUnitDealerRelation_BBillingUnit_DataTimeStamp"]
								};
								if(!Ext.Array.contains(arrId, id)) {
									arrId.push(id);
									me.BLaboratoryArr.push(obj);
								}
							});
							//直接联动更新送检单位列表
							if(me.checkedValue == true) {
								BLaboratoryCheckGrid.store.removeAll();
								BLaboratoryCheckGrid.store.loadData(me.BLaboratoryArr, false);
							}
						}
					});
				}
			}
		});

		BLaboratoryCheckGrid.on({
			checkchange: function(com, newValue, oldValue, eOpts) {
				me.checkedValue = newValue;
				if(newValue == true && me.BLaboratoryArr.length > 0) {
					BLaboratoryCheckGrid.store.removeAll();
					BLaboratoryCheckGrid.store.insert(0, me.BLaboratoryArr);
				} else if(newValue == true && me.BLaboratoryArr.length == 0) {
					BLaboratoryCheckGrid.store.removeAll();
					//BLaboratoryCheckGrid.store.insert(0, me.BLaboratoryArr);
				} else {
					BLaboratoryCheckGrid.store.loadData(BLaboratoryCheckGrid.oldRecords, false);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push(Ext.create('Shell.class.pki.dunitdealer.BatchAddForm', {
			region: 'west',
			header: false,
			formtype: 'add',
			split: true,
			collapsible: true,
			itemId: 'BatchAddForm',
			BDealerId: me.BDealerId,
			BDealerName: me.BDealerName,
			BDealerDataTimeStamp: me.BDealerDataTimeStamp,

			BDealerBBillingUnitId: me.BDealerBBillingUnitId,
			BDealerBBillingUnitName: me.BDealerBBillingUnitName,
			BDealerBBillingUnitDataTimeStamp: me.BDealerBBillingUnitDataTimeStamp
		}));

		items.push(Ext.create('Shell.class.pki.dealercontract.CheckGrid', {
			region: 'west',
			width: 385,
			header: false,
			checkOne: false,
			defaultLoad: true,
			itemId: 'UnitItemCheckGrid',
			hasAcceptButton: false,
			/**后台排序*/
			//remoteSort: false,
			/**带分页栏*/
			hasPagingtoolbar: false,
			/**默认每页数量*/
			defaultPageSize: 5000,
			/**获取数据服务路径*/
	        selectUrl: '/StatService.svc/Stat_UDTO_SearchDealerItemByHQL?isPlanish=true&dealerID='+me.BDealerId,
//			defaultWhere: 'dunititem.UnitType=2 and dunititem.UnitID=' + me.BDealerId,
			defaultWhere: 'dunititem.UnitType=2',
			searchInfo: {
				width: '100%',
				emptyText: '项目名称',
				isLike: true,
				fields: ['dunititem.BTestItem.CName']
			}
		}));
		items.push(Ext.create('Shell.class.pki.dunitdealer.CheckGrid', {
			region: 'center',
			header: false,
			checkOne: false,
			defaultLoad: true,
			hasButtons: true,
			itemId: 'BLaboratoryCheckGrid',
			/**后台排序*/
			remoteSort: false,
			/**带分页栏*/
			hasPagingtoolbar: false,
			/**默认每页数量*/
			defaultPageSize: 5000,
			searchInfo: {
				width: '53%',
				emptyText: '送检单位名称',
				isLike: true,
				fields: ['blaboratory.CName']
			},
			hasAcceptButton: false
		}));

		return items;
	},
	/**保存数据*/
	onSaveInfo: function() {
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var BLaboratoryCheckGrid = me.getComponent('BLaboratoryCheckGrid');
		var BatchAddForm = me.getComponent('BatchAddForm');

		//表单数据校验
		if(!BatchAddForm.getForm().isValid()) return;
		var values = BatchAddForm.getForm().getValues();
		//项目必须勾选
		var recordsItems = UnitItemCheckGrid.getSelectionModel().getSelection();
		var lenItems = recordsItems.length;
		if(lenItems == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		//送检单位必须勾选
		var recordsBLaboratory = BLaboratoryCheckGrid.getSelectionModel().getSelection();
		var lenBLaboratory = recordsBLaboratory.length;
		if(lenBLaboratory == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var entity = me.getEntity();
		entity.ContractType = JcallShell.PKI.Enum.ContractType.E2;
		me.saveCount = lenItems;
		me.saveIndex = 0;
		me.saveError = [];

		me.showMask(me.saveText); //显示遮罩层
		//项目处理
		var listDUnitItem = [];
		for(var i = 0; i < lenItems; i++) {
			var rec = recordsItems[i];
			var DUnitItem = {
				Id: rec.get('DUnitItem_Id'),
				DataTimeStamp: rec.get('DUnitItem_DataTimeStamp').split(','),
				IsStepPrice: rec.get('DUnitItem_IsStepPrice') == true ? '1' : '0'
			};
			var BTestItem = {
				Id: rec.get('DUnitItem_BTestItem_Id'),
				DataTimeStamp: rec.get('DUnitItem_BTestItem_DataTimeStamp').split(','),
			};
			DUnitItem.BTestItem = BTestItem;
			listDUnitItem.push(DUnitItem);
		}
		var isExec = true;
		//开票类型为送检单位才处理默认开票方
		if(values.DUnitDealerRelation_BillingUnitType == '1') {
			//选择的送检单位的默认开票方是否为空
			for(var i = 0; i < lenBLaboratory; i++) {
				var BBillingUnitId = recordsBLaboratory[i].get('BLaboratory_BBillingUnit_Id');
				if(BBillingUnitId == "") {
					isExec = false;
					break;
				}
			}

		}
		if(isExec == false) {
			JShell.Msg.error("选择的送检单位的默认开票方为空,不能保存");
			me.hideMask(); //隐藏遮罩层
			return;
		} else {
			//先保存送检单位的默认开票方信息
			me.updateDefaultBBillingUnit();
			//送检单位处理
			var listBLaboratory = [];
			for(var i = 0; i < lenBLaboratory; i++) {
				var rec = recordsBLaboratory[i];
				var BLaboratory = {
					Id: rec.get('BLaboratory_Id'),
					DataTimeStamp: rec.get('BLaboratory_DataTimeStamp').split(','),
					//必须传开票方信息
					BBillingUnit: null
				};
				listBLaboratory.push(BLaboratory);
			}
			//再保存批量新增信息
			me.saveAll(entity, listDUnitItem, listBLaboratory);
			me.hideMask(); //隐藏遮罩层
		}
	},
	/**更新送检单位的默认开票方信息*/
	updateDefaultBBillingUnit: function() {
		var me = this;
		var BLaboratoryCheckGrid = me.getComponent('BLaboratoryCheckGrid');
		var records = BLaboratoryCheckGrid.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		var rec = null,
			id = '',
			BBillingUnitId = "";
		for(var i = 0; i < len; i++) {
			rec = records[i];
			id = rec.get("BLaboratory_Id");
			BBillingUnitId = rec.get('BLaboratory_BBillingUnit_Id');
			me.updateBBillingUnit(id, {
				entity: {
					Id: id,
					BBillingUnit: {
						Id: BBillingUnitId
					}
				},
				fields: 'Id,BBillingUnit_Id'
			});
		}
	},

	/**修改价格*/
	updateBBillingUnit: function(id, params) {
		var me = this;
		var BLaboratoryCheckGrid = me.getComponent('BLaboratoryCheckGrid');
		var url = (me.editBLaboratoryUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editBLaboratoryUrl;

		var params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			var record = BLaboratoryCheckGrid.store.findRecord("BLaboratory_Id", id);
			if(data.success) {
				if(record) {
					//record.set(me.DelField, true);
					record.commit();
				}
				me.saveCount++;
			} else {
				me.saveErrorCount++;
				if(record) {
					//record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				//if (me.saveErrorCount == 0) me.onSearch();
			}
		}, false);
	},
	/**保存一条信息*/
	saveAll: function(entity, listDUnitItem, listBLaboratory) {
		var me = this;
		var url = (me.saveUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;

		var params = {
			'entity': entity,
			'listBLaboratory': listBLaboratory,
			'listDUnitItem': listDUnitItem
		};
		params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			if(!data.success) {
				me.saveError.push(data.msg);
			}
			me.hideMask(); //隐藏遮罩层
			if(me.saveError.length == 0) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(me.saveError.join('</br>'));
			}
		});
	},

	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var BatchAddForm = me.getComponent('BatchAddForm');

		UnitItemCheckGrid.disableControl(); //禁用所有的操作功能
		BatchAddForm.disableControl(); //禁用所有的操作功能

		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var BatchAddForm = me.getComponent('BatchAddForm');

		UnitItemCheckGrid.enableControl(); //启用所有的操作功能
		BatchAddForm.enableControl(); //启用所有的操作功能

		me.body.unmask(); //隐藏遮罩层
	},
	/**获取对象数据*/
	getEntity: function() {
		var me = this,
			form = me.getComponent('BatchAddForm'),
			values = form.getForm().getValues();

		var entity = {
			BeginDate: JShell.Date.toServerDate(values.DUnitDealerRelation_BeginDate),
			BDealer: {
				Id: me.BDealerId,
				DataTimeStamp: me.BDealerDataTimeStamp.split(',')
			}
		};

		if(values.DUnitDealerRelation_EndDate && values.DUnitDealerRelation_EndDate != null) {
			entity.EndDate = JShell.Date.toServerDate(values.DUnitDealerRelation_EndDate);
		}

		//开票类型
		if(values.DUnitDealerRelation_BillingUnitType) {
			entity.BillingUnitType = values.DUnitDealerRelation_BillingUnitType;
		}
		//开票方,如果是批量新增,前台不用传,后台处理
		if(values.DUnitDealerRelation_BBillingUnit_Id) {
			entity.BBillingUnit = {
				Id: values.DUnitDealerRelation_BBillingUnit_Id,
				DataTimeStamp: values.DUnitDealerRelation_BBillingUnit_DataTimeStamp.split(',')
			};
		}
		//经销商的开票方信息,必须传开票方信息
		if(values.DUnitDealerRelation_BillingUnitType != '2') {
			entity.BDealer.BBillingUnit = null;
		} else {
			entity.BDealer.BBillingUnit = entity.BBillingUnit;
		}
		//合作级别
		if(values.DUnitDealerRelation_CoopLevel) {
			entity.CoopLevel = values.DUnitDealerRelation_CoopLevel;
		}
		return entity;
	}
});