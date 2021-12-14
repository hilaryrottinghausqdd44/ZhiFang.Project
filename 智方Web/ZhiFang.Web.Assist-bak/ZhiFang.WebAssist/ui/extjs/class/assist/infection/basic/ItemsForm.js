/**
 * 环境监测送检样本登记
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.basic.ItemsForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '环境监测送检样本登记信息',
	width: 720,
	height: 190,
	bodyPadding: 10,

	formtype: 'show',
	/**带功能按钮栏*/
	hasButtontoolbar: false,

	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 195,
		labelAlign: 'right'
	},
	/**通过指定字段(如工号等)获取RBACUser(PUser转换)*/
	fieldKey: "ShortCode",
	//人员选择是否从集成平台获取
	userIsGetLimp: false,
	//人员选择输入工号后是否按下了回车键触发
	isEnterKeyPress: false,
	//获取所有人员基本信息，方便验证核对人录入信息是否正常
	puseList: [],

	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用暂存/确认提交按钮*/
	hasSave: true,
	/**是否启用合格/不合格按钮*/
	hasJudgment: false,

	/**监测类型集合信息*/
	RecordTypeItemList: [],
	/**监测类型记录项集合信息*/
	SampleItemList: [],
	/**科室记录项结果短语集合信息*/
	DeptPhraseList: [],
	/**当前选择的监测类型Id*/
	SCRecordTypeId: "",
	/**当前样品信息结果值*/
	SampleItemsVal: null,

	/**一维条码模板信息*/
	BarcodeModel: null,
	/**一维条码模板集合信息*/
	BarcodeModelList: [],
	/**默认条码模板值*/
	DefaultBarcodeModel: "128C5525",
	/**默认选择的打印机*/
	DefaultPrinter: "",
	/**注入作条码打印使用*/
	GridPanel: null,
	/**是否登录科室*/
	ISCURDEPT: false,

	itemStyle: {
		fontSize: "18px"
	},
	/**默认采样时间*/
	SampleTimeValue: "07:30",
	/**当前选择的科室*/
	CurDeptId:"",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.CurDeptId= JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		me.callParent(arguments);
	},
	/**新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.isAdd();
	},
	/**@desc 监测类型*/
	createRecordTypeItem: function() {
		var me = this;
		var items = [{
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "手卫生",
				inputValue: "11",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#C0FFC0"
				}
			},
			{
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "空气培养",
				inputValue: "12",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#FFE0C0"
				}
			}, {
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "物体表面",
				inputValue: "13",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#FFC0FF"
				}
			},
			{
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "消毒剂",
				inputValue: "14",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#C0FFFF"
				}
			}, {
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "透析液及透析用水",
				width: 175,
				inputValue: "15",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#C0C0FF"
				}
			},
			{
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "医疗器材",
				inputValue: "16",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#FFFFC0"
				}
			}, {
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "污水",
				inputValue: "17",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#00C0C0"
				}
			},
			{
				name: "GKSampleRequestForm_SCRecordType_Id",
				boxLabel: "其它",
				inputValue: "18",
				style: {
					fontSize: me.itemStyle.fontSize,
					backgroundColor: "#C0C000"
				}
			}
		];
		return items;
	},
	/**
	 * @description 初始化科室记录项结果短语信息
	 * @param {Object} callback
	 */
	initDeptPhrase: function(callback) {
		var me = this;
		if (me.DeptPhraseList.length > 0) {
			if (callback) {
				return callback();
			} else {
				return;
			}
		}
		//短语类型为按科室
		var where = "screcordphrase.IsUse=1 ";
		/**1:感控监测;2:科室监测;*/
		if (me.MonitorType == "2") {
			var deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
			if (deptId) {
				where += " and screcordphrase.TypeObjectId=" + deptId;
			}
		}

		var sort = [{
			"property": "SCRecordPhrase_TypeObjectId",
			"direction": "ASC"
		}, {
			"property": "SCRecordPhrase_BObjectId",
			"direction": "ASC"
		}, {
			"property": "SCRecordPhrase_DispOrder",
			"direction": "ASC"
		}];
		var url = JShell.System.Path.ROOT +
			'/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordPhraseOfGKByHQL?isPlanish=true';;
		url = url + "&where=" + JShell.String.encode(where);
		url = url + "&sort=" + JShell.JSON.encode(sort);
		JShell.Server.get(url, function(data) {
			if (data.success && data.value && data.value.list) {
				me.DeptPhraseList = data.value.list;
			}
			if (callback) callback();
		}, false);
	},
	/**
	 * @description 获取监测类型的记录项集合信息
	 * @param {Object} callback
	 */
	getSampleItemList: function(callback) {
		var me = this;
		//从缓存读取
		var list = JcallShell.BLTF.cachedata.getCache("SampleItemList");
		if (list && list.length > 0) {
			me.SampleItemList = list;
			if (callback) return callback();
		}
		if (me.SampleItemList.length > 0) {
			if (callback) return callback();
		}

		var where =
			"screcorditemlink.SCRecordType.ContentTypeID=10000 and screcorditemlink.SCRecordTypeItem.IsUse=1";
		var sort = [{
			"property": "SCRecordItemLink_SCRecordType_DispOrder",
			"direction": "ASC"
		}, {
			"property": "SCRecordItemLink_DispOrder", //SCRecordItemLink_SCRecordTypeItem_DispOrder
			"direction": "ASC"
		}];
		var fields =
			"SCRecordItemLink_SCRecordType_ContentTypeID,SCRecordItemLink_SCRecordType_Id,SCRecordItemLink_SCRecordType_CName,SCRecordItemLink_SCRecordType_TestItemCode,SCRecordItemLink_SCRecordType_BGColor,SCRecordItemLink_SCRecordType_DispOrder,SCRecordItemLink_SCRecordTypeItem_Id,SCRecordItemLink_SCRecordTypeItem_ItemCode,SCRecordItemLink_SCRecordTypeItem_CName,SCRecordItemLink_TestItemCode,SCRecordItemLink_SCRecordTypeItem_DispOrder,SCRecordItemLink_SCRecordTypeItem_ItemXType,SCRecordItemLink_SCRecordTypeItem_DefaultValue,SCRecordItemLink_SCRecordTypeItem_ItemUnit,SCRecordItemLink_IsBillVisible";
		var url = JShell.System.Path.ROOT +
			'/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordItemLinkByHQL?isPlanish=true';
		url = url + "&where=" + JShell.String.encode(where);
		url = url + "&fields=" + fields; // JShell.JSON.encode(fields);
		url = url + "&sort=" + JShell.JSON.encode(sort);
		JShell.Server.get(url, function(data) {
			if (data.success && data.value && data.value.list) {
				me.SampleItemList = me.changeSampleItemList(data.value.list);
			} else {
				me.SampleItemList = [];
			}
			JcallShell.BLTF.cachedata.setCache("SampleItemList", me.SampleItemList)
			if (callback) callback();
		}, false);
	},
	/**
	 * @description 封装记录项集合信息
	 * @param {Object} list
	 */
	changeSampleItemList: function(list) {
		var me = this;
		var newList = [];
		var curTypeItem = "";

		for (var i = 0; i < list.length; i++) {
			var item = list[i];
			var typeItemId = item["SCRecordItemLink_SCRecordType_Id"];
			//监测类型的某一记录项
			var itemEditInfo = {
				Id: item["SCRecordItemLink_SCRecordTypeItem_Id"],
				CName: item["SCRecordItemLink_SCRecordTypeItem_CName"],
				TestItemCode: item["SCRecordItemLink_TestItemCode"], //记录项对应的检验项目编码
				DispOrder: item["SCRecordItemLink_SCRecordTypeItem_DispOrder"],
				ItemXType: item["SCRecordItemLink_SCRecordTypeItem_ItemXType"],
				DefaultValue: item["SCRecordItemLink_SCRecordTypeItem_DefaultValue"],
				ItemUnit: item["SCRecordItemLink_SCRecordTypeItem_ItemUnit"],
				BGColor: item["SCRecordItemLink_SCRecordType_BGColor"],
				IsBillVisible: item["SCRecordItemLink_IsBillVisible"] //开单是否可见
			};

			if (!curTypeItem || curTypeItem.Id != typeItemId) {
				//新的监测类型
				curTypeItem = {
					Id: typeItemId,
					CName: item["SCRecordItemLink_SCRecordType_CName"],
					TestItemCode: item["SCRecordItemLink_SCRecordType_TestItemCode"], //监测类型对应的检验项目编码
					BGColor: item["SCRecordItemLink_SCRecordType_BGColor"],
					DispOrder: item["SCRecordItemLink_SCRecordType_DispOrder"],
					SampleInfoList: [], //监测类型记录项对象的集合信息
					FormItemList: [] //监测类型记录项表单项集合信息
				};
				if (curTypeItem && curTypeItem.Id) newList.push(curTypeItem);
			}
			var formItem = me.createFormItem(curTypeItem, itemEditInfo);
			curTypeItem.SampleInfoList.push(itemEditInfo);
			curTypeItem.FormItemList.push(formItem);

		}
		return newList;
	},
	/**
	 * @description创建某一监测类型的记录项表单项信息
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createFormItem: function(curTypeItem, itemEditInfo) {
		var me = this;
		var item = null;
		switch (itemEditInfo.ItemXType) {
			case "radiogroup":
				item = me.createRadiogroup(curTypeItem, itemEditInfo);
				break;
			case "uxSimpleComboBox":
				item = me.createSimpleComboBox(curTypeItem, itemEditInfo);
				break;
			case "editComboBox":
				item = me.createSimpleComboBox(curTypeItem, itemEditInfo);
				break;
			case "textfield":
				item = me.createTextField(curTypeItem, itemEditInfo);
				break;
			case "numberfield":
				item = me.createNumberField(curTypeItem, itemEditInfo);
				break;
			case "datetimefield":
				item = me.createDateTimeField(curTypeItem, itemEditInfo);
				break;
			case "datefield":
				item = me.createDateField(curTypeItem, itemEditInfo);
				break;
			case "timefield":
				item = me.createTimeField(curTypeItem, itemEditInfo);
				break;
			default:
				item = me.createTextField(curTypeItem, itemEditInfo);
				break;
		}
		return item;
	},
	/**
	 * @description 创建textfield
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createTextField: function(curTypeItem, itemEditInfo) {
		var me = this;

		var itemUnit = "",
			defaultValue = "";
		if (itemEditInfo) {
			if (itemEditInfo["ItemUnit"]) itemUnit = itemEditInfo["ItemUnit"];
			if (itemEditInfo["DefaultValue"]) defaultValue = itemEditInfo["DefaultValue"];
		}
		var fieldLabel = "" + itemEditInfo["CName"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";

		var isBillVisible = itemEditInfo["IsBillVisible"];
		var hidden1 = false;
		if (isBillVisible == "0" || isBillVisible == false || isBillVisible == "false" || isBillVisible == 0) {
			hidden1 = true;
		} else {
			hidden1 = false;
		}

		var textfield = {
			xtype: 'textfield',
			curTypeItem: curTypeItem, //监测类型信息
			itemEditInfo: itemEditInfo, //记录项信息
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + itemEditInfo["Id"], //记录项字典Id
			itemId: "" + itemEditInfo["Id"], //记录项字典Id
			colspan: 1,
			value: defaultValue,
			width: me.defaults.width * 1 - 20,
			hidden: hidden1,
			style: {
				backgroundColor: curTypeItem["BGColor"]
			}
		};
		return textfield;
	},
	/**
	 * @description 创建datetimefield
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createDateTimeField: function(curTypeItem, itemEditInfo) {
		var me = this;

		var itemUnit = "",
			defaultValue = "";
		if (itemEditInfo) {
			if (itemEditInfo["ItemUnit"]) itemUnit = itemEditInfo["ItemUnit"];
			if (itemEditInfo["DefaultValue"]) defaultValue = itemEditInfo["DefaultValue"];
		}
		var fieldLabel = "" + itemEditInfo["CName"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var isBillVisible = itemEditInfo["IsBillVisible"];
		var hidden1 = false;
		if (isBillVisible == "0" || isBillVisible == false || isBillVisible == "false" || isBillVisible == 0) {
			hidden1 = true;
		} else {
			hidden1 = false;
		}
		var textfield = {
			xtype: 'datetimefield',
			curTypeItem: curTypeItem, //监测类型信息
			itemEditInfo: itemEditInfo, //记录项信息
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + itemEditInfo["Id"], //记录项字典Id
			itemId: "" + itemEditInfo["Id"], //记录项字典Id
			colspan: 1,
			value: defaultValue,
			format: 'Y-m-d H:i:s',
			hidden: hidden1,
			width: me.defaults.width * 1 - 20,
			style: {
				backgroundColor: curTypeItem["BGColor"]
			}
		};
		return textfield;
	},
	/**
	 * @description 创建datefield
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createDateField: function(curTypeItem, itemEditInfo) {
		var me = this;

		var itemUnit = "",
			defaultValue = "";
		if (itemEditInfo) {
			if (itemEditInfo["ItemUnit"]) itemUnit = itemEditInfo["ItemUnit"];
			if (itemEditInfo["DefaultValue"]) defaultValue = itemEditInfo["DefaultValue"];
		}
		var fieldLabel = "" + itemEditInfo["CName"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var isBillVisible = itemEditInfo["IsBillVisible"];
		var hidden1 = false;
		if (isBillVisible == "0" || isBillVisible == false || isBillVisible == "false" || isBillVisible == 0) {
			hidden1 = true;
		} else {
			hidden1 = false;
		}
		var textfield = {
			xtype: 'datefield',
			curTypeItem: curTypeItem, //监测类型信息
			itemEditInfo: itemEditInfo, //记录项信息
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + itemEditInfo["Id"], //记录项字典Id
			itemId: "" + itemEditInfo["Id"], //记录项字典Id
			colspan: 1,
			value: defaultValue,
			format: "H:i",
			hidden: hidden1,
			width: me.defaults.width * 1 - 20,
			style: {
				backgroundColor: curTypeItem["BGColor"]
			}
		};
		return textfield;
	},
	/**
	 * @description 创建timefield
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createTimeField: function(curTypeItem, itemEditInfo) {
		var me = this;

		var itemUnit = "",
			defaultValue = "";
		if (itemEditInfo) {
			if (itemEditInfo["ItemUnit"]) itemUnit = itemEditInfo["ItemUnit"];
			if (itemEditInfo["DefaultValue"]) defaultValue = itemEditInfo["DefaultValue"];
		}
		var fieldLabel = "" + itemEditInfo["CName"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var isBillVisible = itemEditInfo["IsBillVisible"];
		var hidden1 = false;
		if (isBillVisible == "0" || isBillVisible == false || isBillVisible == "false" || isBillVisible == 0) {
			hidden1 = true;
		} else {
			hidden1 = false;
		}
		var textfield = {
			xtype: 'timefield',
			curTypeItem: curTypeItem, //监测类型信息
			itemEditInfo: itemEditInfo, //记录项信息
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + itemEditInfo["Id"], //记录项字典Id
			itemId: "" + itemEditInfo["Id"], //记录项字典Id
			colspan: 1,
			value: defaultValue,
			format: 'Y-m-d',
			hidden: hidden1,
			width: me.defaults.width * 1 - 20,
			style: {
				backgroundColor: curTypeItem["BGColor"]
			}
		};
		return textfield;
	},

	/**
	 * @description 创建numberfield
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createNumberField: function(curTypeItem, itemEditInfo) {
		var me = this;

		var itemUnit = "",
			defaultValue = "";
		if (itemEditInfo) {
			if (itemEditInfo["ItemUnit"]) itemUnit = itemEditInfo["ItemUnit"];
			if (itemEditInfo["DefaultValue"]) defaultValue = itemEditInfo["DefaultValue"];
		}
		var fieldLabel = "" + itemEditInfo["CName"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var isBillVisible = itemEditInfo["IsBillVisible"];
		var hidden1 = false;
		if (isBillVisible == "0" || isBillVisible == false || isBillVisible == "false" || isBillVisible == 0) {
			hidden1 = true;
		} else {
			hidden1 = false;
		}
		var numberfield = {
			xtype: 'numberfield',
			curTypeItem: curTypeItem, //监测类型信息
			itemEditInfo: itemEditInfo, //记录项信息
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + itemEditInfo["Id"], //记录项字典Id
			itemId: "" + itemEditInfo["Id"], //记录项字典Id
			colspan: 1,
			value: defaultValue,
			width: me.defaults.width * 1 - 20,
			hidden: hidden1,
			style: {
				backgroundColor: curTypeItem["BGColor"]
			}
		};
		return numberfield;
	},
	/**
	 * @description 创建uxSimpleComboBox
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createSimpleComboBox: function(curTypeItem, itemEditInfo) {
		var me = this;
		var dataSet1 = [];

		//透析液及透析用水的选择项为指定的检验项目
		var id2 = "" + curTypeItem.Id;
		if (id2 == "15" && itemEditInfo.Id == "120010") {
			dataSet1 = [{
					'value': '',
					'text': '请选择'
				},
				{
					'value': '120010',
					'text': '细菌内毒素'
				},
				{
					'value': '601040',
					'text': '细菌总数'
				}
			];
		} else {
			//按科室记录项结果短语查询
			if (!dataSet1 || dataSet1.length <= 0) {
				dataSet1 = me.getDeptPhraseList(itemEditInfo["Id"]);
			}
		}
		if (!dataSet1) {
			dataSet1 = [{
				'value': '',
				'text': '请选择'
			}];
		}
		var store1 = Ext.create('Ext.data.Store', {
			fields: ['value', 'text'],
			data: dataSet1
		});

		var defaultValue = "";
		if (itemEditInfo && itemEditInfo["DefaultValue"]) defaultValue = itemEditInfo["DefaultValue"];
		var isBillVisible = itemEditInfo["IsBillVisible"];
		var hidden1 = false;
		if (isBillVisible == "0" || isBillVisible == false || isBillVisible == "false" || isBillVisible == 0) {
			hidden1 = true;
		} else {
			hidden1 = false;
		}

		var textfield = {
			xtype: 'combobox', //combobox, combo uxSimpleComboBox
			curTypeItem: curTypeItem, //监测类型信息
			itemEditInfo: itemEditInfo, //记录项信息
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			colspan: 1,
			width: me.defaults.width * 1,
			style: {
				backgroundColor: curTypeItem["BGColor"]
			},
			labelWidth: 85,
			fieldLabel: "" + itemEditInfo["CName"],
			emptyText: "" + itemEditInfo["CName"],
			name: "" + itemEditInfo["Id"], //记录项字典Id
			itemId: "" + itemEditInfo["Id"], //记录项字典Id
			value: defaultValue,

			editable: true,
			typeAhead: true,
			//multiSelect: true,
			store: store1,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			hidden: hidden1,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if (newValue == oldValue) return;

					//透析液及透析用水
					var curTypeItemId = "",
						itemEditInfoId = "";
					if (com.curTypeItem) curTypeItemId = "" + com.curTypeItem.Id;
					if (com.itemEditInfo) itemEditInfoId = "" + com.itemEditInfo.Id;
					if (curTypeItemId == "15" && itemEditInfoId == "120010") {
						com.TestItemCode = newValue;
						//return;
					} else {
						JShell.Action.delay(function() {
							me.onComLoadData(com.itemId, newValue);
						}, null, 1500);
					}
				}
			}
		};
		//透析液及透析用水的选择项为指定的检验项目
		if (curTypeItem.Id == "15" && itemEditInfo.Id == "120010") {
			textfield.typeAhead = false;
			textfield.editable = false;
		}
		return textfield;
	},
	/**
	 * @description 创建radiogroup
	 * @param {Object} curTypeItem
	 * @param {Object} itemEditInfo
	 */
	createRadiogroup: function(curTypeItem, itemEditInfo) {
		var me = this;
		var items = [];
		var dataSet1 = [];
		if (itemEditInfo["ItemDataSet"]) {
			dataSet1 = itemEditInfo["ItemDataSet"];
			if (dataSet1) {
				dataSet1 = dataSet1.replace(/{/g, "[").replace(/}/g, "]").replace(/:/g, ",");
				dataSet1 = JShell.JSON.decode(dataSet1);
			}
		}
		if (!dataSet1) dataSet1 = [];
		var defaultValue = "";
		if (itemEditInfo["DefaultValue"]) defaultValue = itemEditInfo["DefaultValue"];
		var isBillVisible = itemEditInfo["IsBillVisible"];
		var hidden1 = false;
		if (isBillVisible == "0" || isBillVisible == false || isBillVisible == "false" || isBillVisible == 0) {
			hidden1 = true;
		} else {
			hidden1 = false;
		}
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
			curTypeItem: curTypeItem, //监测类型信息
			itemEditInfo: itemEditInfo, //记录项信息
			TestItemCode: itemEditInfo.TestItemCode, //检验项目编码
			fieldLabel: "" + itemEditInfo["CName"],
			name: "" + itemEditInfo["Id"], //记录项字典Id
			itemId: "" + itemEditInfo["Id"], //记录项字典Id
			columns: 2,
			vertical: true,
			labelWidth: 60,
			hidden: hidden1,
			style: {
				backgroundColor: curTypeItem["BGColor"]
			},
			items: items
		};
		return radiogroup;
	},
	/**
	 * @description 某一记录项输入或选择后处理
	 * @param {Object} itemId
	 * @param {Object} newValue
	 */
	onComLoadData: function(itemId, newValue) {
		var me = this;
		if (!newValue) return;
		
		var comboBox = me.getComponent('SampleItemList').getComponent(itemId);
		if (!comboBox) return;

		var index1 = comboBox.store.find("text", newValue);
		if (index1 >= 0) return; //已存在

		//同步科室记录项结果短语集合
		var list3 = me.getDeptPhraseList(itemId);
		if (!list3) list3 = [];
		//从集合里判断
		for (var i = 0; i < list3.length; i++) {
			var item = list3[i];
			if (item["text"] == newValue) {
				index1 = i;
				break;
			}
		}
		if (index1 >= 0) return; //已存在

		//不存在时,自动追加该选择项
		var model = {
			'value': newValue,
			'text': newValue
		};
		list3.push(model);
		me.setDeptPhraseList(itemId,list3);
		comboBox.store.loadData(list3);
	},
	/**
	 * @description 科室选择改后,记录项的结果短语需要更新
	 */
	loadSampleItemsData: function() {
		var me = this;
		
		var sampleItemList = me.getComponent('SampleItemList');
		if (!sampleItemList) return;
		
		//样品信息组表单项处理
		var items = sampleItemList.items.items;
		for (var i = 0; i < items.length; i++) {
			//透析液及透析用水的选择项为指定的检验项目
			if(items[i].Id == "120010")continue;
			
			var xtype=items[i].xtype;
			if(xtype!="combobox"&&xtype!="editComboBox"&&xtype!="uxSimpleComboBox")continue;
			
			var itemId = items[i].itemId;
			var list3 = me.getDeptPhraseList(itemId)||[];
			//if(!list3||list3.length<=0)continue;
			
			items[i].store.loadData(list3);
		}
	},
	/**
	 * @description 获取某一记录项的结果短语集合
	 * @param {Object} itemId
	 */
	getDeptPhraseList: function(itemId) {
		var me = this;
		var list2 = []; //某一记录项的结果短语

		var deptId =me.CurDeptId;
		if (!deptId) deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";

		var deptPhraseList1 = me.DeptPhraseList || [];
		var deptPhraseList2 = []; //某一科室的记录项结果短语
		for (var i = 0; i < deptPhraseList1.length; i++) {
			var item1 = deptPhraseList1[i];
			if (item1 && item1["DeptId"] == deptId) {
				deptPhraseList2 = item1["Data"] || [];
				break;
			}
		}
		
		//某一记录项的结果短语集合
		for (var i = 0; i < deptPhraseList2.length; i++) {
			var deptPhrase = deptPhraseList2[i];
			if (deptPhrase && deptPhrase["BObjectId"] == itemId) {
				list2 = deptPhrase["Data"];
				break;
			}
		}
		if (!list2) list2 = [];
		return list2;
	},
	/**
	 * @description 
	 * @param {Object} itemId
	 * @param {Object} data
	 */
	setDeptPhraseList: function(itemId, data) {
		var me = this;
		var deptId = me.CurDeptId;
		if (!deptId) deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";

		for (var i = 0; i < me.DeptPhraseList.length; i++) {
			var item1 = me.DeptPhraseList[i];
			if (item1 && item1["DeptId"] == deptId) {
				//某一科室的记录项结果短语集合
				var deptPhraseList2 = me.DeptPhraseList[i]["Data"] || [];
				for (var j = 0; j < deptPhraseList2.length; j++) {
					//某一记录项的结果短语集合
					var deptPhrase = deptPhraseList2[j];
					if (deptPhrase && deptPhrase["BObjectId"] == itemId) {
						//me.DeptPhraseList[i]["Data"][j]["Data"]=data;
						deptPhrase["Data"] = data;
						break;
					}
				}
				break;
			}
		}
	},
	/**
	 * @description 获取某一监测类型的记录项表单项信息
	 * @param {Object} newValue
	 */
	getFormItemList: function(newValue) {
		var me = this;
		var formItemList = []; //某一监测类型的记录项
		if (!newValue) return formItemList;

		if (newValue["GKSampleRequestForm_SCRecordType_Id"]) newValue = newValue["GKSampleRequestForm_SCRecordType_Id"];

		for (var i = 0; i < me.SampleItemList.length; i++) {
			var item = me.SampleItemList[i];
			if (newValue == item.Id) {
				formItemList = item.FormItemList;
				break;
			}
		}
		return formItemList;
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this,
			fields = [];
		var items = me.items.items || [],
			len = items.length;
		//最外层的items	
		for (var i = 0; i < len; i++) {
			if (items[i].name && !items[i].IsnotField) {
				fields.push(items[i].name);
			}
		}
		//登记信息items
		items = me.getComponent('SampleDocInfo').items.items || [];
		len = items.length;
		for (var i = 0; i < len; i++) {
			if (items[i].name && !items[i].IsnotField) {
				fields.push(items[i].name);
			}
		}
		//样品信息items,后台服务自动以GKSampleRequestForm_DtlJArray返回
		//院感科评估
		fields.push("GKSampleRequestForm_SCRecordType_Description");

		return fields;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data = me.callParent(arguments);

		//监测类型单选组赋值
		if (data["GKSampleRequestForm_SCRecordType_Id"]) {
			var objValue = {
				"GKSampleRequestForm_SCRecordType_Id": data["GKSampleRequestForm_SCRecordType_Id"]
			};
			data["GKSampleRequestForm_SCRecordType_Id"] = objValue;
			//直接赋值
			var itemCom = me.getComponent("GKSampleRequestForm_SCRecordType_Id");
			if (itemCom) {
				itemCom.setValue(objValue);
			}
		}

		//日期时间处理
		if (data.GKSampleRequestForm_DataAddTime) {
			data.GKSampleRequestForm_DataAddTime = JcallShell.Date.toString(data.GKSampleRequestForm_DataAddTime, true);
		}
		if (data.GKSampleRequestForm_SampleDate) {
			data.GKSampleRequestForm_SampleDate = JcallShell.Date.toString(data.GKSampleRequestForm_SampleDate, true);
		}
		if (data.GKSampleRequestForm_SampleTime) {
			data.GKSampleRequestForm_SampleTime = Ext.util.Format.date(data.GKSampleRequestForm_SampleTime, 'H:i');
		}

		//监测类型的表单记录项处理
		me.SampleItemsVal = null;
		var dtlJArray = data.GKSampleRequestForm_DtlJArray;
		if (dtlJArray) {
			dtlJArray = JShell.JSON.decode(dtlJArray);
			me.SampleItemsVal = {};
			if (dtlJArray && dtlJArray.length > 0) {
				var itemId = '';
				var sampleItem = me.getComponent('SampleItemList');
				for (var i = 0; i < dtlJArray.length; i++) {
					var item = dtlJArray[i];
					itemId = "" + item.Id;
					data[itemId] = item.ItemResult;
					me.SampleItemsVal[itemId] = item.ItemResult;
					//监测类型记录项明细的主键Id处理
					if (item.BObjectID) {
						var item2 = sampleItem.getComponent(itemId);
						if (item2) {
							item2.BObjectID = "" + item.BObjectID;
							item2.setValue(item.ItemResult);
						}
					}
				}
			}
		}

		return data;
	}

});
