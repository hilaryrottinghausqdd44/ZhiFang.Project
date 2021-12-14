/**
 * 输血过程记录:血袋不良反应tab
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.adversereaction.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '',
	header: false,
	border: false,
	bodyPadding: 1,
	/**不良反应分类集合*/
	transRecordTypeList: [],
	//输血过程记录主单ID
	PK: null,
	//是否包含删除列
	hasDelCol: false,
	//新增还是编辑
	formtype: "",
	
	afterRender: function () {
		var me = this;
		me.callParent(arguments);
		me.on({
			/**页签切换事件处理*/
			tabchange: function (tabPanel, newCard, oldCard, eOpts) {
				if(me.formtype!="add")me.loadOfTabChange(tabPanel, newCard, oldCard);
			}
		});
		me.activeTab = 0;
	},
	initComponent: function () {
		var me = this;
		me.addEvents('save');
		me.loadTransRecordType(function () {
			me.items = me.createItems();
		});
		me.callParent(arguments);
	},
	createItems: function () {
		var me = this;
		var appInfos = [];

		for (var i = 0; i < me.transRecordTypeList.length; i++) {
			var transItem = me.transRecordTypeList[i];
			var itemTab = Ext.create('Shell.class.blood.nursestation.transrecord.adversereaction.TransItemGrid', {
				header: true,
				border: false,
				hasDelCol: me.hasDelCol,
				title: ""+transItem["BloodTransRecordType_CName"],
				itemId: ""+transItem["BloodTransRecordType_Id"],
				transRecordType: transItem
			});
			appInfos.push(itemTab);
		}
		return appInfos;
	},
	//加载不良反应分类字典信息
	loadTransRecordType: function (callback) {
		var me = this;
		if (me.transRecordTypeList.length > 0) {
			if (callback) {
				return callback();
			} else {
				return;
			}
		}
		//不良反应分类
		var where = "bloodtransrecordtype.ContentTypeID=2 and bloodtransrecordtype.IsVisible=1";
		var sort = [{
			"property": "BloodTransRecordType_DispOrder",
			"direction": "ASC"
		}];
		var url = JShell.System.Path.ROOT +
			'/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeByHQL?isPlanish=true';
		url = url + "&fields=BloodTransRecordType_Id,BloodTransRecordType_CName,BloodTransRecordType_DispOrder";
		url = url + "&where=" + JShell.String.encode(where);
		url = url + "&sort=" + JShell.JSON.encode(sort);
		JShell.Server.get(url, function (data) {
			if (data.success) {
				me.transRecordTypeList = data.value.list;
			} else {
				me.transRecordTypeList = [];
			}
			if (callback) callback();
		}, false);
	},
	loadData: function (id) {
		var me = this;
		me.PK = id;
		var gridPanel = me.getActiveTab();
		gridPanel.PK = id;
		var externalWhere = "bloodtransitem.BloodTransForm.Id=" + me.PK;
		externalWhere = externalWhere + " and bloodtransitem.BloodTransRecordType.Id=" +
			gridPanel.itemId;//记录项字典的所属分类字典Id
		gridPanel.externalWhere = externalWhere;
		gridPanel.onSearch();
	},
	clearData: function () {
		var me = this;
		me.PK = null;
		var gridPanel = me.getActiveTab();
		gridPanel.PK = null;
		gridPanel.externalWhere = "";
		gridPanel.onSearch();
	},
	//页签改变后加载对应的列表内容
	loadOfTabChange: function (tabPanel, newCard, oldCard) {
		var me = this;
		var externalWhere = "";
		if (me.PK) {
			newCard.PK = me.PK;
			var recordTypeId = newCard.itemId;
			externalWhere = "bloodtransitem.BloodTransForm.Id=" + me.PK;
			externalWhere = externalWhere + " and bloodtransitem.BloodTransRecordType.Id=" +
				recordTypeId;
		} else {
			newCard.PK = "";
			newCard.externalWhere = "";
		}
		newCard.externalWhere = externalWhere;
		newCard.onSearch();
	}
});
