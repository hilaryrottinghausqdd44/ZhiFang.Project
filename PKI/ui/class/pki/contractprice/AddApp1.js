/**
 * 新增送检单位合同
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.contractprice.AddApp1', {
	extend: 'Ext.panel.Panel',
	title: '新增送检单位合同',

	layout: 'border',
	bodyPadding: 1,

	width: 600,
	height: 300,

	/**新增服务地址*/
	saveUrl: '/StatService.svc/Stat_UDTO_AddDContractPrice',

	/**送检单位ID*/
	LaboratoryId: null,
	/**送检单位时间戳*/
	LaboratoryDataTimeStamp: null,

	/**送检单位默认开票方ID*/
	LaboratoryBillingUnitId: null,
	/**送检单位默认开票方名称*/
	LaboratoryBillingUnitName: null,
	/**送检单位默认开票方时间戳*/
	LaboratoryBillingUnitDataTimeStamp: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		UnitItemCheckGrid.load();

		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');
		ContractPriceAddForm.on({
			save: function() {
				me.onSaveInfo();
			}
		});
		if (ContractPriceAddForm) {
			var com = ContractPriceAddForm.getComponent('checkUpdate');
			ContractPriceAddForm.defaultSetValue(com.checked);
		}
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
		items.push(Ext.create('Shell.class.pki.contractprice.item.CheckGrid', {
			region: 'center',
			header: false,
			checkOne: false,
			defaultLoad: false,
			itemId: 'UnitItemCheckGrid',
			hasAcceptButton: false,
			isShow: true,
			defaultWhere: ''
				//			defaultWhere:'dunititem.UnitType=2 and dunititem.UnitID=' + me.LaboratoryId,
				//			searchInfo:{width:'100%',emptyText:'项目名称',isLike:true,
				//				fields:['btestitem.CName']}
		}));
		items.push(Ext.create('Shell.class.pki.contractprice.AddForm', {
			region: 'west',
			header: false,
			formtype: 'add',
			split: true,
			collapsible: true,
			itemId: 'ContractPriceAddForm',
			LaboratoryId: me.LaboratoryId, //送检单位ID
			LaboratoryDataTimeStamp: me.LaboratoryDataTimeStamp, //送检单位时间戳
			LaboratoryBillingUnitId: me.LaboratoryBillingUnitId, //送检单位默认开票方ID
			LaboratoryBillingUnitName: me.LaboratoryBillingUnitName, //送检单位默认开票方名称
			LaboratoryBillingUnitDataTimeStamp: me.LaboratoryBillingUnitDataTimeStamp //送检单位默认开票方时间戳
		}));

		return items;
	},
	/**保存数据*/
	onSaveInfo: function() {
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');
		var values = ContractPriceAddForm.getForm().getValues();
		//表单数据校验
		if (!ContractPriceAddForm.getForm().isValid()) return;

		//项目必须勾选
		var records = UnitItemCheckGrid.getSelectionModel().getSelection();
		var len = records.length;
		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var entity = me.getEntity();

		//勾选同时维护关系是否选择上
		var dealerRelation =null;
		var isCheck = "";
		if (values.checkUpdate) {
			isCheck = values.checkUpdate.toString();
		}
		if (isCheck == "on" || isCheck == "1" || isCheck == "true") {
			dealerRelation ={};
			dealerRelation = me.getEntity();
			//dealerRelation.BTestItem =null;
			//经销商
			if (values.DContractPrice_BDealer_Id.toString() != "") {
				var BDealer = {
					Id: values.DContractPrice_BDealer_Id.toString(),
					DataTimeStamp: values.DContractPrice_BDealer_DataTimeStamp.split(',')
				};

				dealerRelation.BDealer = BDealer;
				dealerRelation.BDealer.BBillingUnit = {
					Id: "",
					DataTimeStamp: null
				};
			}
			//开票类型
			if (values.DContractPrice_BillingUnitType.toString() != "") {
				dealerRelation.BillingUnitType = values.DContractPrice_BillingUnitType.toString();
			}
			//默认开票方
			if (values.DContractPrice_BBillingUnit_Id.toString() != "") {
				var BBillingUnit = {
					Id: values.DContractPrice_BBillingUnit_Id.toString(),
					DataTimeStamp: values.DContractPrice_BBillingUnit_DataTimeStamp.split(',')
				};
				dealerRelation.BBillingUnit = BBillingUnit;
				dealerRelation.BDealer.BBillingUnit = BBillingUnit;
				dealerRelation.BLaboratory.BBillingUnit = BBillingUnit;
			}
			//合作级别
			if (values.DContractPrice_CoopLevel) {
				dealerRelation.CoopLevel = values.DContractPrice_CoopLevel;
			}
		}
		me.saveCount = len;
		me.saveIndex = 0;
		me.saveError = [];
		me.showMask(me.saveText); //显示遮罩层
		var listDUnitItem = [];
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var DUnitItem = {
				BTestItem: {
					Id: rec.get('BTestItem_Id'),
					DataTimeStamp: rec.get('BTestItem_DataTimeStamp').split(','),
				}
			};
			listDUnitItem.push(DUnitItem);
		}
		me.saveAll(entity, listDUnitItem, dealerRelation);
	},
	/**保存信息*/
	saveAll: function(entity, listDUnitItem, dealerRelation) {
		var me = this;
		var url = (me.saveUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;

		var params = {
			entity: entity,
			listDUnitItem: listDUnitItem
		};
		if(dealerRelation!=null){
			params.dealerRelation=dealerRelation;
		}
		params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			//me.saveIndex++;
			if (!data.success) {
				me.saveError.push(data.msg);
			}
			me.hideMask(); //隐藏遮罩层
			if (me.saveError.length == 0) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
				me.fireEvent('save', me);
			} else {
				JShell.Msg.error(me.saveError.join('</br>'));
			}
		});
	},

	/**保存一条信息*/
	saveOne: function(entity) {
		var me = this;
		var url = (me.saveUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;

		var params = {
			entity: entity
		};
		params = Ext.JSON.encode(params);

		JShell.Server.post(url, params, function(data) {
			me.saveIndex++;
			if (!data.success) {
				me.saveError.push(data.msg);
			}
			if (me.saveIndex == me.saveCount) {
				me.hideMask(); //隐藏遮罩层
				if (me.saveError.length == 0) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
					me.fireEvent('save', me);
				} else {
					JShell.Msg.error(me.saveError.join('</br>'));
				}
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');

		UnitItemCheckGrid.disableControl(); //禁用所有的操作功能
		ContractPriceAddForm.disableControl(); //禁用所有的操作功能

		me.body.mask(text); //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		var UnitItemCheckGrid = me.getComponent('UnitItemCheckGrid');
		var ContractPriceAddForm = me.getComponent('ContractPriceAddForm');

		UnitItemCheckGrid.enableControl(); //启用所有的操作功能
		ContractPriceAddForm.enableControl(); //启用所有的操作功能

		me.body.unmask(); //隐藏遮罩层
	},
	/**获取对象数据*/
	getEntity: function() {
		var me = this,
			form = me.getComponent('ContractPriceAddForm'),
			values = form.getForm().getValues();
		var entity = {
			BeginDate: JShell.Date.toServerDate(values.DContractPrice_BeginDate),
			EndDate: JShell.Date.toServerDate(values.DContractPrice_EndDate),
			ContractNo: values.DContractPrice_ContractNo,
			BLaboratory: {
				Id: me.LaboratoryId,
				DataTimeStamp: me.LaboratoryDataTimeStamp.split(',')
			},
			ContractType: '1'
		};

		return entity;
	}
});