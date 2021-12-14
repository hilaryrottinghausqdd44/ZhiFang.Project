/**
 * 输血过程记录:批量修改--病人体征信息
 * @description 分步批量登记(分为输血结束前登记及输血结束登记)
 * @author longfc
 * @version 2020-03-23
 */
Ext.define('Shell.class.blood.nursestation.transrecord.batchregister.TransDtlForm', {
	extend: 'Shell.class.blood.nursestation.transrecord.transdtl.DtlForm',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '病人体征信息',
	formtype: "edit",
	/**发血血袋明细记录Id字符串值:如123,234,4445*/
	outDtlIdStr: null,
	//fieldset的高度计算值
	fheight: 275,
	//fieldset的高度是否需要计算
	hasCalcFHeight: true,
	/**病人体征信息记录项集合信息*/
	transRecordTypeItemList: [],
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchPatientSignsByOutDtlIdStr?isPlanish=true',
	/**输血记录类型 1:输血结束前;2:输血结束;*/
	transTypeId:"",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.isAdd();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.callParent(arguments);
	},
	getStoreFields: function() {
		var me = this;
		var fields = me.callParent(arguments);
		//批量修改录入的数据标志
		fields.push("BloodTransItem_BatchSign");
		return fields;
	},
	/**根据输血过程记录主单ID加载数据*/
	load: function(outDtlIdStr) {
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
		if (!outDtlIdStr) return;
		me.outDtlIdStr = outDtlIdStr;
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if (collapsed) {
			me.isCollapsed = true;
			return;
		}
		me.showMask(me.loadingText); //显示遮罩层
		url = (url.slice(0, 4) == 'http' ? '' : me.getPathRoot()) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&") + "outDtlIdStr=" + me.outDtlIdStr;
		url += '&fields=' + me.getStoreFields().join(',');

		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				if (data.value) {
					//data.value = JShell.Server.Mapping(data.value);
					me.lastData = me.changeResult(data);
					me.getForm().setValues(me.lastData);
				}
			} else {
				JShell.Msg.error(data.msg);
			}
			me.fireEvent('load', me, data);
		});
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var list = [];
		if (data.value && data.value.list) {
			list = data.value.list;
		} else {
			return data;
		}
		//封装转换
		var values = {};

		//返回的输血记录项结果
		for (var i = 0; i < list.length; i++) {
			//输血记录项字典Id
			var typeItemId = "" + list[i]["BloodTransItem_BloodTransRecordTypeItem_Id"];

			//需要保存提交的表单项基本信息
			for (var j = 0; j < me.itemObjList.length; j++) {
				if (me.itemObjList[j].itemId == typeItemId) {
					//输血记录项字典信息
					if (!me.itemObjList[j]["BloodTransRecordTypeItem"]) {
						me.itemObjList[j]["BloodTransRecordTypeItem"] = {
							"Id": list[i]["BloodTransItem_BloodTransRecordTypeItem_Id"],
							"CName": list[i]["BloodTransItem_BloodTransRecordTypeItem_CName"],
							"DispOrde": list[i]["BloodTransItem_BloodTransRecordTypeItem_DispOrder"]
						};
					}
					//输血过程记录明细Id
					me.itemObjList[j]["Id"] = "";
					me.itemObjList[j]["BloodTransItem"] = {
						"Id": ""
					};
					//输血过程记录主单Id
					me.itemObjList[j]["BloodTransForm"] = {
						"Id": ""
					};
					//按组件类型赋值
					var xtype = me.itemObjList[j].xtype;
					var itemValue = "";
					if (xtype == "numberfield") {
						itemValue = list[i]["BloodTransItem_NumberItemResult"];
					} else if (xtype == "radiogroup") {
						var objValue = {};
						objValue[typeItemId] = list[i]["BloodTransItem_TransItemResult"];
						itemValue = objValue;
					} else {
						itemValue = list[i]["BloodTransItem_TransItemResult"];
					}
					values[typeItemId] = itemValue;
					//label处理
					var batchSign = "" + list[i]["BloodTransItem_BatchSign"];
					me.setItemLabel(me.itemObjList[j], typeItemId, batchSign);
					break;
				}
			}
		}
		return values;
	},
	//获取输血过程记录项信息
	loadTransRecordTypeItem: function(callback) {
		var me = this;
		var cachekey="BloodTransRecordTypeItemList"
		if (me.transTypeId && me.transTypeId.length > 0) {
			cachekey+=""+me.transTypeId;
		}
		//从缓存读取
		var list = JcallShell.BLTF.cachedata.getCache(cachekey);
		if (list && list.length > 0) {
			me.transRecordTypeItemList = list;
			return callback();
		}
		if (me.transRecordTypeItemList.length > 0) {
			if (callback) {
				return callback();
			} else {
				return;
			}
		}
		//输血记录项
		var where =
			"bloodtransrecordtypeitem.BloodTransRecordType.ContentTypeID=1 and bloodtransrecordtypeitem.BloodTransRecordType.IsVisible=1 and bloodtransrecordtypeitem.IsVisible=1";
		if (me.transTypeId && me.transTypeId.length > 0) {
			where += " and bloodtransrecordtypeitem.BloodTransRecordType.TransTypeId=" + me.transTypeId;
		}
		var sort = [{
			"property": "BloodTransRecordTypeItem_BloodTransRecordType_DispOrder",
			"direction": "ASC"
		}, {
			"property": "BloodTransRecordTypeItem_DispOrder",
			"direction": "ASC"
		}];
		var url = JShell.System.Path.ROOT +
			'/BloodTransfusionManageService.svc/BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL?isPlanish=true';
		url = url + "&where=" + JShell.String.encode(where);
		url = url + "&sort=" + JShell.JSON.encode(sort);
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.transRecordTypeItemList = data.value.list;
			} else {
				me.transRecordTypeItemList = [];
			}
			JcallShell.BLTF.cachedata.setCache(cachekey, me.transRecordTypeItemList)
			if (callback) callback();
		}, false);
	},
	/**
	 * @description label处理
	 * @param {Object} itemId 对应表单项的itemId
	 * @param {Object} batchSign 批量修改录入的数据标志
	 */
	setItemLabel: function(itemObj, itemId, batchSign) {
		var me = this;
		var typeItemId = "" + itemObj["BloodTransRecordType"]["Id"];
		var item1 = null;
		var typeItem = me.getComponent(typeItemId);
		if (typeItem) item1 = typeItem.getComponent(itemId);
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
				var fieldLabe = item1.getFieldLabel();
				item1.setFieldLabel(fieldLabe +'<span style="background-color:'+bColor+';">&nbsp;&nbsp;</span>');
			}
		}
	},
	getSaveInfo: function() {
		var me = this;
		var values = me.getForm().getValues();
		var entityList = [];
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];

		for (var i = 0; i < me.itemObjList.length; i++) {
			var itemObj = me.itemObjList[i];
			//没有输血记录项字典Id,直接忽略
			if (!itemObj.itemId) continue;

			//输血记录项字典信息
			var recordTypeItem = itemObj.BloodTransRecordTypeItem;
			var transRecordTypeItem = {
				"Id": itemObj.itemId,
				"CName": itemObj.CName,
				"DataTimeStamp": dataTimeStamp
			};
			var dispOrder = 0;
			if (recordTypeItem && recordTypeItem.DispOrder) {
				dispOrder = recordTypeItem.DispOrder;
			}
			if (!itemObj.ContentTypeID) itemObj.ContentTypeID = 1;
			var itemValue = values[itemObj.itemId];
			if (!itemValue) itemValue = "";
			var entity = {
				"Visible": 1,
				"DispOrder": dispOrder,
				"ContentTypeID": itemObj.ContentTypeID,
				"TransItemResult": itemValue,
				"BloodTransRecordTypeItem": transRecordTypeItem,
			};
			entity.Id = -1;
			//数字类型值处理
			if (itemValue && itemObj.xtype == 'numberfield') {
				entity.NumberItemResult = parseFloat(itemValue);
			}
			//输血过程记录分类字典信息
			if (itemObj.BloodTransRecordType) {
				entity.BloodTransRecordType = {
					"Id": itemObj.BloodTransRecordType.Id,
					"DataTimeStamp": dataTimeStamp
				};
			}
			entityList.push(entity);
		}
		return entityList;
	}
});
