/**
 * 库存变化统计报表
 * @author longfc
 * @version 2019-06-06
 */
Ext.define('Shell.class.rea.client.qtyoperation.report.add.DocForm', {
	extend: 'Shell.class.rea.client.qtyoperation.report.basic.DocForm',

	title: '结转报表',
	formtype: 'show',
	width: 680,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyMonthBalanceDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddQtyBalanceReportOfQtyDtlOperList',
	/**修改服务地址*/
	editUrl: '',
	buttonDock: "top",
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	hasPrint: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**月结最小年份*/
	minYearValue: 2018,
	/**月结最小年份*/
	minYearValue: 2018,
	/**货架选择项是否只读*/
	PlaceReadOnly: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			if(me.hasSave) items.push("save");
			if(me.hasReset) items.push('reset');
		}
		if(me.hasPrint) {
			items.push("-", {
				xtype: 'button',
				iconCls: 'button-print',
				itemId: "btnPrint",
				text: '打印结转报表',
				tooltip: '打印结转报表',
				hidden: true,
				handler: function() {
					me.onPrintClick();
				}
			});
		}

		if(items.length == 0) return null;

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: true
		});
	},
	/**@description 结转类型选择后处理*/
	onTypeSelect: function(com, records) {
		var me = this;
		if(!records) return;
		var typeValue = "" + com.getValue();
		me.clearStorageAndPlace();

		var statisticalID = "1",
			storageReadOnly = true;
		me.PlaceReadOnly = true;
		switch(typeValue) {
			case "2": //按库房
				statisticalID = "2"; //按货品批号库房
				storageReadOnly = false;
				break;
			case "3": //按库房货架
				statisticalID = "3"; //按货品批号库房货架
				storageReadOnly = false;
				me.PlaceReadOnly = false;
				break;
			default:
				break;
		}
		var StatisticalTypeID = me.getComponent('ReaBmsQtyMonthBalanceDoc_StatisticalTypeID');
		StatisticalTypeID.setValue(statisticalID);

		var StorageName = me.getComponent('ReaBmsQtyMonthBalanceDoc_StorageName');
		StorageName.setReadOnly(storageReadOnly);
	},
	/****@description清空库房货架选择*/
	clearStorageAndPlace: function() {
		var me = this;
		var StorageName = me.getComponent('ReaBmsQtyMonthBalanceDoc_StorageName');
		var StorageID = me.getComponent('ReaBmsQtyMonthBalanceDoc_StorageID');
		StorageID.setValue("");
		StorageName.setValue("");
		me.clearPlace();
	},
	/****@description清空货架选择*/
	clearPlace: function() {
		var me = this;
		var PlaceID = me.getComponent('ReaBmsQtyMonthBalanceDoc_PlaceID');
		var PlaceName = me.getComponent('ReaBmsQtyMonthBalanceDoc_PlaceName');
		PlaceID.setValue("");
		PlaceName.setValue("");
		PlaceName.changeClassConfig({
			defaultWhere: ""
		});
	},
	/****@description库房选择*/
	onStorageAccept: function(record) {
		var me = this;
		me.clearPlace();

		var StorageName = me.getComponent('ReaBmsQtyMonthBalanceDoc_StorageName');
		var StorageID = me.getComponent('ReaBmsQtyMonthBalanceDoc_StorageID');
		var idValue = record ? record.get('ReaStorage_Id') : '';
		var textValeu = record ? record.get('ReaStorage_CName') : '';
		StorageID.setValue(idValue);
		StorageName.setValue(textValeu);

		var data = {
			defaultWhere: idValue ? "reaplace.ReaStorage.Id=" + idValue : ""
		};
		var readOnly = idValue ? false : true;
		//如果不是"按货品批号库房货架"
		if(me.PlaceReadOnly) readOnly = true;
		var PlaceName = me.getComponent('ReaBmsQtyMonthBalanceDoc_PlaceName');
		PlaceName.setReadOnly(me.PlaceReadOnly);
		PlaceName.changeClassConfig(data);
	},
	/**货架选择*/
	onPlaceAccept: function(record) {
		var me = this;
		var PlaceID = me.getComponent('ReaBmsQtyMonthBalanceDoc_PlaceID');
		var PlaceName = me.getComponent('ReaBmsQtyMonthBalanceDoc_PlaceName');

		var idValue = record ? record.get('ReaPlace_Id') : '';
		var textValeu = record ? record.get('ReaPlace_CName') : '';
		PlaceID.setValue(idValue);
		PlaceName.setValue(textValeu);
	},
	/**@description 打印月结单*/
	onPrintClick: function() {
		var me = this;
		if(!me.PK) {
			JShell.Msg.error("请先保存结转报表后,再打印结转报表!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf");
		url += '?operateType=1&id=' + me.PK;
		window.open(url);
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("结转报表封装提交信息错误!");
			return;
		}
		var labCName = JcallShell.REA.System.CENORG_NAME;
		if(!labCName) labCName = "";
		params.labCName = labCName;
		params = JcallShell.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		var url = me.addUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value.id : id;
				id += '';
				me.fireEvent('save', me, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			Round: values.ReaBmsQtyMonthBalanceDoc_Round,
			QtyMonthBalanceDocNo: values.ReaBmsQtyMonthBalanceDoc_QtyMonthBalanceDocNo,
			StorageName: values.ReaBmsQtyMonthBalanceDoc_StorageName,
			CreaterName: values.ReaBmsQtyMonthBalanceDoc_CreaterName,
			PlaceName: values.ReaBmsQtyMonthBalanceDoc_PlaceName,
			GoodsClass: values.ReaBmsQtyMonthBalanceDoc_GoodsClass,
			GoodsClassType: values.ReaBmsQtyMonthBalanceDoc_GoodsClassType
		};
		entity.Memo = values.ReaBmsQtyMonthBalanceDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');

		if(values.ReaBmsQtyMonthBalanceDoc_TypeID) entity.TypeID = values.ReaBmsQtyMonthBalanceDoc_TypeID;
		if(values.ReaBmsQtyMonthBalanceDoc_StatisticalTypeID) entity.StatisticalTypeID = values.ReaBmsQtyMonthBalanceDoc_StatisticalTypeID;
		if(values.ReaBmsQtyMonthBalanceDoc_CreaterID) entity.CreaterID = values.ReaBmsQtyMonthBalanceDoc_CreaterID;
		if(values.ReaBmsQtyMonthBalanceDoc_StorageID) entity.StorageID = values.ReaBmsQtyMonthBalanceDoc_StorageID;
		if(values.ReaBmsQtyMonthBalanceDoc_PlaceID) entity.PlaceID = values.ReaBmsQtyMonthBalanceDoc_PlaceID;
		if(values.ReaBmsQtyMonthBalanceDoc_StartDate) entity.StartDate = JShell.Date.toServerDate(values.ReaBmsQtyMonthBalanceDoc_StartDate);
		if(values.ReaBmsQtyMonthBalanceDoc_EndDate) entity.EndDate = JShell.Date.toServerDate(values.ReaBmsQtyMonthBalanceDoc_EndDate);

		return {
			entity: entity
		};
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var startDate = me.getComponent('ReaBmsQtyMonthBalanceDoc_StartDate');
		var endDate = me.getComponent('ReaBmsQtyMonthBalanceDoc_EndDate');
		var sysdate = JcallShell.System.Date.getDate();
		var startDateV = JcallShell.Date.getNextDate(sysdate,-30);
		//var curDateTime = JcallShell.Date.toString(sysdate, true);
		startDate.setValue(Ext.util.Format.date(startDateV, "Y-m-d"));// H:m:s
		endDate.setValue(Ext.util.Format.date(sysdate, "Y-m-d"));
	}
});