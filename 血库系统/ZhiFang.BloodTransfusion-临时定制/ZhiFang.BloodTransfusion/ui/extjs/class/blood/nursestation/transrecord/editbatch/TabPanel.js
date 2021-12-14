/**
 * 输血过程记录:批量修改--血袋不良反应tab
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.editbatch.TabPanel', {
	extend: 'Ext.tab.Panel',

	title: '',
	header: false,
	border: false,
	bodyPadding: 1,
	/**不良反应分类集合*/
	transRecordTypeList: [],
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr:null,
	//当前选中发血血袋记录集合
	outDtlRrecords: [],
	//是否包含删除列
	hasDelCol: false,
	//新增还是编辑
	formtype: "edit",
	
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
		me.addEvents('save','onDelBatchClick');
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
			var itemTab = Ext.create('Shell.class.blood.nursestation.transrecord.adversereaction.BatchTransItemGrid', {
				header: true,
				border: false,
				hasDelCol: me.hasDelCol,
				title: ""+transItem["BloodTransRecordType_CName"],
				itemId: ""+transItem["BloodTransRecordType_Id"],
				//当前选择的不良反应分类ID
				recordTypeId:""+transItem["BloodTransRecordType_Id"],
				outDtlIdStr:me.outDtlIdStr,
				transRecordType: transItem,
				listeners: {
					changeResult: function(p, data) {
						me.setItemTitle(p.itemId,data);
					},
					onDelBatchClick: function(p) {
						me.fireEvent('onDelBatchClick',me);//me.getActiveTab()
					}
				}
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
	clearData: function () {
		var me = this;
		me.outDtlIdStr = null;
		var gridPanel = me.getActiveTab();
		gridPanel.outDtlIdStr = null;
		gridPanel.externalWhere = "";
		gridPanel.onSearch();
	},
	loadData: function (outDtlIdStr) {
		var me = this;
		me.outDtlIdStr = outDtlIdStr;
		var gridPanel = me.getActiveTab();
		gridPanel.outDtlIdStr = outDtlIdStr;
		////记录项字典的所属分类字典Id
		var externalWhere = " bloodtransitem.BloodTransRecordType.Id=" +gridPanel.itemId;
		gridPanel.externalWhere = externalWhere;
		gridPanel.onSearch();
	},
	//页签改变后加载对应的列表内容
	loadOfTabChange: function (tabPanel, newCard, oldCard) {
		var me = this;
		if(newCard.defaultLoad==false){
			var externalWhere = "";
			var recordTypeId = newCard.itemId;
			if (me.outDtlIdStr) {
				newCard.outDtlIdStr = me.outDtlIdStr;
				externalWhere =" bloodtransitem.BloodTransRecordType.Id=" +recordTypeId;
			} else {
				newCard.outDtlIdStr = "";
				newCard.externalWhere = "";
			}
			newCard.externalWhere = externalWhere;
			newCard.recordTypeId = recordTypeId;
			newCard.onSearch();
		}else{
			//console.log(newCard.defaultLoad);
		}
	},
	/**
	 * @description label处理
	 * @param {Object} batchSign 批量修改录入的数据标志
	 */
	setItemTitle: function(itemId,data) {
		var me = this;
		var item1=me.getComponent(itemId);//me.getActiveTab();
		var batchSign = "" + data["batchSign"];
		var bColor = "";
		if (item1 && batchSign) {
			switch (batchSign) {
				case "1":
					//1:表示当前选择的发血血袋对应的记录项完全未作过登记,结果值全部为空;
					bColor = "";//橙色:#FFB800;灰石色(石板灰):#708090;金色:#FFD700
					break;
				case "2":
					//2:表示当前选择的发血血袋对应的记录项的结果值部分相同,部分不相同;
					bColor = "#FF4500";// 橙红色:#FF4500; 赤色:#FF5722
					break;
				case "3":
					//3:表示当前选择的发血血袋对应的记录项的结果值完全一致;
					bColor = "#66CDAA";//墨绿:#009688;中宝石碧绿:#66CDAA
					break;
				default:
					break;
			}
			if (bColor) {
				var fieldLabel = ""+item1.transRecordType["BloodTransRecordType_CName"];
				item1.setTitle(fieldLabel +'<span style="background-color:'+bColor+';">&nbsp;&nbsp;</span>');
			}
		}
	}
});
