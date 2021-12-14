/**
 * 输血过程记录:病人体征信息
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.transdtl.DtlForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '病人体征信息',

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemListByTransFormId?isPlanish=true',

	/**内容周围距离*/
	bodyPadding: '5px 2px 0px 0px',
	height: 385,
	width: 735,
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 195,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**启用表单状态初始化*/
	openFormType: true,
	//输血过程记录主单ID
	PK: null,
	//fieldset的计算高度值
	fheight: 275,
	//fieldset的高度是否需要计算
	hasCalcFHeight: true,
	/**病人体征信息记录项集合信息*/
	transRecordTypeItemList: [],
	/**
	 * 需要保存提交的表单项基本信息
	 * 样例:{
	 * itemId: '',////对应记录项字典的主键值
	 * xtype: 'numberfield',
	 * ContentTypeID:"1",//1:输血记项;2: 输血记录不良反应;3: 输血记录临床处理措施;
	 * BloodTransItem:{},//输血过程记录明细信息
	 * BloodTransRecordTypeItem:{}//对应记录项字典信息
	 * }
	 */
	itemObjList: [],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.isAdd();
	},
	initComponent: function() {
		var me = this;
		if (me.width && me.width > 0) {
			me.layout.columns = parseInt(me.width / me.defaults.width);
		}
		me.loadTransRecordTypeItem(function() {
			me.items = me.createItems();
		});
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.itemObjList = [];
		var fwidth = me.defaults.width * 1 + 5;
		var counts = me.transRecordTypeItemList.length;
		if (!counts) counts = 4;
		if (me.width && me.width > 0) {
			fwidth = parseInt(me.width / counts) - (counts * 5);
		}
		if(fwidth<me.defaults.width)fwidth=me.defaults.width+5;
		//输血记录项分类
		for (var i = 0; i < me.transRecordTypeItemList.length; i++) {
			var transItem = me.transRecordTypeItemList[i];
			var item = me.createFieldset(transItem, fwidth);
			items.push(item);
		}
		return items;
	},
	//创建fieldset
	createFieldset: function(data, fwidth) {
		var me = this;

		var items = [];
		//某一输血记录项分类下的记录项字典集合
		var itemList = data["ItemList"];
		if (!itemList) itemList = [];
		//记录项分类不大于4时的高度重新计算
		if (me.hasCalcFHeight == true) {// && itemList.length <= 4
			//标题-工具栏-fieldset边距
			me.fheight = parseInt((me.height - 20 - 20 - 60));
			if (me.fheight < 275) me.fheight = 275;
			if (me.fheight > 375) me.fheight = 375;
		}
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		for (var i = 0; i < itemList.length; i++) {
			var typeItem = itemList[i];
			var itemEditInfo = typeItem["TransItemEditInfo"];
			var itemXType = "textfield";
			if (itemEditInfo) itemEditInfo = JShell.JSON.decode(itemEditInfo);
			if (itemEditInfo && itemEditInfo["ItemXType"]) {
				itemXType = itemEditInfo["ItemXType"];
			}
			if (!itemEditInfo) itemEditInfo = {};
			var item = null;
			switch (itemXType) {
				case "radiogroup":
					item = me.createRadiogroup(typeItem, itemEditInfo);
					break;
				case "uxSimpleComboBox":
					item = me.createSimpleComboBox(typeItem, itemEditInfo);
					break;
				case "textfield":
					item = me.createTextField(typeItem, itemEditInfo);
					break;
				case "numberfield":
					item = me.createNumberField(typeItem, itemEditInfo);
					break;
				default:
					item = me.createTextField(typeItem, itemEditInfo);
					break;
			}
			if (item != null) {
				var itemObj = {
					itemId: item.itemId, //输血记录项字典Id
					xtype: item.xtype,
					Id: "", //输血过程记录明细Id,在changeResult反填充
					CName: typeItem["CName"],
					ContentTypeID: 1, //1:输血记项;
					BloodTransRecordTypeItem: typeItem, //输血记项字典信息
					BloodTransRecordType: data //输血记项分类字典信息
				};
				if (me.PK) {
					itemObj.BloodTransForm = {
						"Id": me.PK,
						"DataTimeStamp": dataTimeStamp
					};
				}
				me.itemObjList.push(itemObj);
				items.push(item);
			}
		}
		if (!fwidth) fwidth = me.defaults.width * 1 + 20;
		var fieldset = {
			xtype: 'fieldset',
			title: "" + data["CName"], //输血记录项分类字典名称
			itemId: "" + data["Id"], //输血记录项分类字典Id
			dataSet: data, //输血记录项分类字典信息
			layout: {
				type: 'table',
				columns: 1
			},
			colspan: 1,
			height: me.fheight,
			defaults: me.defaults,
			width: fwidth,
			margin: '2px',
			collapsible: false,
			defaultType: 'textfield',
			items: items
		};
		return fieldset;
	},
	//创建textfield
	createTextField: function(data, itemEditInfo) {
		var me = this;

		var itemUnit = "",
			defaultValue = "";
		if (itemEditInfo) {
			if (itemEditInfo["ItemUnit"]) itemUnit = itemEditInfo["ItemUnit"];
			if (itemEditInfo["ItemDefaultValue"]) defaultValue = itemEditInfo["ItemDefaultValue"];
		}
		var fieldLabel = "" + data["CName"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var textfield = {
			xtype: 'textfield',
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + data["Id"], //输血记录项字典Id
			itemId: "" + data["Id"], //输血记录项字典Id
			dataSet: data, //输血记录项字典信息
			colspan: 1,
			value: defaultValue,
			width: me.defaults.width * 1 - 20
		};
		return textfield;
	},
	//创建numberfield
	createNumberField: function(data, itemEditInfo) {
		var me = this;

		var itemUnit = "",
			defaultValue = "";
		if (itemEditInfo) {
			if (itemEditInfo["ItemUnit"]) itemUnit = itemEditInfo["ItemUnit"];
			if (itemEditInfo["ItemDefaultValue"]) defaultValue = itemEditInfo["ItemDefaultValue"];
		}
		var fieldLabel = "" + data["CName"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var numberfield = {
			xtype: 'numberfield',
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + data["Id"], //输血记录项字典Id
			itemId: "" + data["Id"], //输血记录项字典Id
			dataSet: data, //输血记录项字典信息
			colspan: 1,
			value: defaultValue,
			width: me.defaults.width * 1 - 20
		};
		return numberfield;
	},
	//创建uxSimpleComboBox
	createSimpleComboBox: function(data, itemEditInfo) {
		var me = this;
		var dataSet1 = [];
		if (itemEditInfo && itemEditInfo["ItemDataSet"]) {
			dataSet1 = itemEditInfo["ItemDataSet"];
			if (dataSet1) {
				dataSet1 = dataSet1.replace(/{/g, "[").replace(/}/g, "]").replace(/:/g, ",");
				dataSet1 = JShell.JSON.decode(dataSet1);
			}
		}
		var defaultValue = "";
		if (itemEditInfo && itemEditInfo["ItemDefaultValue"]) defaultValue = itemEditInfo["ItemDefaultValue"];

		if (!dataSet1) dataSet1 = [];
		var textfield = {
			xtype: 'uxSimpleComboBox',
			labelWidth: 85,
			fieldLabel: "" + data["CName"],
			emptyText: "" + data["CName"],
			name: "" + data["Id"], //输血记录项字典Id
			itemId: "" + data["Id"], //输血记录项字典Id
			dataSet: data, //输血记录项字典信息
			data: dataSet1,
			colspan: 1,
			width: me.defaults.width * 1,
		};
		return textfield;
	},
	//创建radiogroup
	createRadiogroup: function(data, itemEditInfo) {
		var me = this;
		var items = [];
		var dataSet1 = [];
		if (itemEditInfo && itemEditInfo["ItemDataSet"]) {
			dataSet1 = itemEditInfo["ItemDataSet"];
			if (dataSet1) {
				dataSet1 = dataSet1.replace(/{/g, "[").replace(/}/g, "]").replace(/:/g, ",");
				dataSet1 = JShell.JSON.decode(dataSet1);
			}
		}
		if (!dataSet1) dataSet1 = [];
		var defaultValue = "";
		if (itemEditInfo && itemEditInfo["ItemDefaultValue"]) defaultValue = itemEditInfo["ItemDefaultValue"];

		for (var i = 0; i < dataSet1.length; i++) {
			var data1 = dataSet1[i];
			var item = {
				name: "" + data["Id"],
				boxLabel: data1[0],
				inputValue: data1[1]
			};
			items.push(item);
		}
		var radiogroup = {
			xtype: 'radiogroup',
			fieldLabel: "" + data["CName"],
			name: "" + data["Id"], //输血记录项字典Id
			itemId: "" + data["Id"], //输血记录项字典Id
			dataSet: data, //输血记录项字典信息
			columns: 2,
			vertical: true,
			//layout: 'hbox',
			labelWidth: 60,
			items: items
		};
		return radiogroup;
	},
	loadTransRecordTypeItem: function(callback) {
		var me = this;
		//从缓存读取
		var list = JcallShell.BLTF.cachedata.getCache("BloodTransRecordTypeItemList");
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
		var sort = [{
			"property": "BloodTransRecordTypeItem_BloodTransRecordType_DispOrder",
			"direction": "ASC"
		}, {
			"property": "BloodTransRecordTypeItem_DispOrder",
			"direction": "ASC"
		}];
		var url = JShell.System.Path.ROOT +
			'/ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL?isPlanish=true';
		url = url + "&where=" + JShell.String.encode(where);
		url = url + "&sort=" + JShell.JSON.encode(sort);
		JShell.Server.get(url, function(data) {
			if (data.success) {
				me.transRecordTypeItemList = data.value.list;
			} else {
				me.transRecordTypeItemList = [];
			}
			JcallShell.BLTF.cachedata.setCache("BloodTransRecordTypeItemList", me.transRecordTypeItemList)
			if (callback) callback();
		}, false);
	},
	getStoreFields: function() {
		var me = this;
		var fields = ["BloodTransItem_Id", "BloodTransItem_ContentTypeID", "BloodTransItem_BloodTransForm_Id"];
		fields.push("BloodTransItem_TransItemResult", "BloodTransItem_NumberItemResult");
		fields.push("BloodTransItem_DispOrder", "BloodTransItem_BloodTransRecordTypeItem_DispOrder");
		fields.push("BloodTransItem_BloodTransRecordTypeItem_Id");
		fields.push("BloodTransItem_BloodTransRecordTypeItem_BloodTransRecordType_DispOrder");
		fields.push("BloodTransItem_BloodTransRecordTypeItem_BloodTransRecordType_Id");		
		return fields;
	},
	/**根据输血过程记录主单ID加载数据*/
	load: function(id) {
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
		if (!id) return;
		me.PK = id; //面板主键
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if (collapsed) {
			me.isCollapsed = true;
			return;
		}
		me.showMask(me.loadingText); //显示遮罩层
		url = (url.slice(0, 4) == 'http' ? '' : me.getPathRoot()) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&") + "id=" + id;
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
					me.itemObjList[j]["Id"] = list[i]["BloodTransItem_Id"];
					me.itemObjList[j]["BloodTransItem"] = {
						"Id": list[i]["BloodTransItem_Id"]
					};
					//输血过程记录主单Id
					me.itemObjList[j]["BloodTransForm"] = {
						"Id": list[i]["BloodTransItem_BloodTransForm_Id"]
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
					break;
				}
			}
		}
		//console.log(values);
		return values;
	}
});
