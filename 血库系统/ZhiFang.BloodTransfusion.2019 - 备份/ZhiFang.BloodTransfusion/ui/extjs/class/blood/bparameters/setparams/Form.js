/**
 * 系统运行参数
 * @author longfc
 * @version 2020-03-25
 */
Ext.define("Shell.class.blood.bparameters.setparams.Form", {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '运行参数设置',

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateBParameterListByBatch',
	/**内容周围距离*/
	bodyPadding: '15px 15px 15px 120px',
	width: 825,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '78%',
		labelWidth: 260,
		labelAlign: 'right'
	},
	//新增还是编辑
	formtype: "edit",
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	//fieldset的高度是否需要计算
	hasCalcFHeight: true,
	/**运行参数集合信息*/
	bparameterList: [],
	/**
	 * 需要保存提交的表单项基本信息
	 * 样例:{
	 * }
	 */
	itemObjList: [],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.isEdit("");
	},
	initComponent: function() {
		var me = this;
		me.width = document.body.clientWidth - 20;
		//me.defaults.width = parseInt(me.width) - 40;
		me.loadBParameterList(function() {
			me.items = me.createItems();
		});
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		me.itemObjList = [];
		var fwidth = fwidth = me.defaults.width;
		//运行参数分组信息
		for (var i = 0; i < me.bparameterList.length; i++) {
			var transItem = me.bparameterList[i];
			var item = me.createFieldset(transItem, fwidth);
			items.push(item);
		}
		return items;
	},
	//创建某一分组下的fieldset
	createFieldset: function(data, fwidth) {
		var me = this;

		var items = [];
		//某运行参数分组信息下的运行参数集合
		var itemList = data["ItemList"];
		if (!itemList) itemList = [];
		//记录项分类不大于4时的高度重新计算
		var fheight = 60;
		if (me.hasCalcFHeight == true) {
			fheight = parseInt((itemList.length * 28 + 40));
		}
		for (var i = 0; i < itemList.length; i++) {
			var typeItem = itemList[i];
			var itemEditInfo = typeItem["ItemEditInfo"];
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
					itemId: "" + item.itemId, //运行参数Id
					xtype: item.xtype,
					Id: "" + item.itemId, //运行参数Id,在changeResult反填充
					Name: typeItem["Name"],
					BParameter: typeItem, //运行参数信息
					BParameterTypeGroup: data //运行参数分类信息
				};
				me.itemObjList.push(itemObj);
				items.push(item);
			}
		}
		var fieldset = {
			xtype: 'fieldset',
			title: "" + data["Name"], //运行参数分类名称
			//itemId: "" + data["Id"], //运行参数分类Id
			dataSet: data, //运行参数分类信息
			layout: 'anchor',
			defaults: me.defaults,
			height: fheight,
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
		var fieldLabel = "" + data["Name"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var textfield = {
			xtype: 'textfield',
			fieldLabel: fieldLabel,
			name: "" + data["Id"], //运行参数Id
			itemId: "" + data["Id"], //运行参数Id
			dataSet: data, //运行参数信息
			value: defaultValue
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
		var fieldLabel = "" + data["Name"];
		if (itemUnit) fieldLabel = fieldLabel + "(" + itemUnit + ")";
		var numberfield = {
			xtype: 'numberfield',
			fieldLabel: fieldLabel,
			emptyText: "",
			name: "" + data["Id"], //运行参数Id
			itemId: "" + data["Id"], //运行参数Id
			dataSet: data, //运行参数信息
			value: defaultValue
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
			fieldLabel: "" + data["Name"],
			emptyText: "" + data["Name"],
			name: "" + data["Id"], //运行参数Id
			itemId: "" + data["Id"], //运行参数Id
			dataSet: data, //运行参数信息
			data: dataSet1
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
				boxLabel: data1[1],
				inputValue: data1[0]
			};
			items.push(item);
		}
		var radiogroup = {
			xtype: 'radiogroup',
			fieldLabel: "" + data["Name"],
			name: "" + data["Id"], //运行参数Id
			itemId: "" + data["Id"], //运行参数Id
			dataSet: data, //运行参数信息
			columns: 2,
			vertical: true,
			items: items
		};
		return radiogroup;
	},
	loadBParameterList: function(callback) {
		var me = this;
		//从缓存读取
		var list = JcallShell.BLTF.cachedata.getCache("bparameterList");
		if (list && list.length > 0) {
			me.bparameterList = list;
			return callback();
		}
		if (me.bparameterList.length > 0) {
			if (callback) {
				return callback();
			} else {
				return;
			}
		}
		//过滤输血申请单当前流水号
		var where = "bparameter.ParaType='CONFIG' and bparameter.IsUse=1 and bparameter.ParaNo!='BL-BRQF-CURN-0009'";
		var sort = [{
			"property": "BParameter_SName",
			"direction": "ASC"
		}, {
			"property": "BParameter_DispOrder",
			"direction": "ASC"
		}];
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchBParameterOfUserSetByHQL';
		url = url + "?where=" + JShell.String.encode(where);
		url = url + "&sort=" + JShell.JSON.encode(sort);
		JShell.Server.get(url, function(data) {
			//console.log(data);
			if (data.success) {
				var list = data.value.list;
				if (list && Ext.typeOf(list) == "string") list = JShell.JSON.decode(list);;
				me.bparameterList = list;
			} else {
				me.bparameterList = [];
			}
			JcallShell.BLTF.cachedata.setCache("bparameterList", me.bparameterList)
			if (callback) callback();
		}, false);
	},
	getStoreFields: function() {
		var me = this;
		var fields = ["BParameter_Name", "BParameter_SName", "BParameter_Shortcode", "BParameter_PinYinZiTou",
			"BParameter_DispOrder", "BParameter_Id", "BParameter_ItemEditInfo", "BParameter_ParaValue",
			"BParameter_ParaDesc"
		];
		return fields;
	},
	/**根据输血过程记录主单ID加载数据*/
	load: function() {
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();

		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if (collapsed) {
			me.isCollapsed = true;
			return;
		}
		me.showMask(me.loadingText); //显示遮罩层
		url = (url.slice(0, 4) == 'http' ? '' : me.getPathRoot()) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&") + 'fields=' + me.getStoreFields().join(',');
		//过滤输血申请单当前流水号
		var where="bparameter.ParaNo!='BL-BRQF-CURN-0009'";
		url +="&where="+where;
		
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				if (data.value) {
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
		//返回的运行参数结果
		for (var i = 0; i < list.length; i++) {
			//运行参数Id
			var typeItemId = "" + list[i]["BParameter_Id"];
			//需要保存提交的表单项基本信息
			for (var j = 0; j < me.itemObjList.length; j++) {
				if (me.itemObjList[j].itemId == typeItemId) {
					//运行参数信息
					if (!me.itemObjList[j]["BParameter"]) {
						me.itemObjList[j]["BParameter"] = {
							"SName": list[i]["BParameter_SName"],
							"Id": list[i]["BParameter_Id"],
							"Name": list[i]["BParameter_Name"],
							"DispOrde": list[i]["BParameter_DispOrder"]
						};
					}
					//按组件类型赋值
					var xtype = me.itemObjList[j].xtype;
					var itemValue = "";
					if (xtype == "numberfield") {
						itemValue = list[i]["BParameter_ParaValue"];
					} else if (xtype == "radiogroup") {
						var objValue = {};
						objValue[typeItemId] = list[i]["BParameter_ParaValue"];
						itemValue = objValue;
					} else {
						itemValue = list[i]["BParameter_ParaValue"];
					}
					values[typeItemId] = itemValue;
					break;
				}
			}
		}
		return values;
	},
	getSaveInfo: function() {
		var me = this;
		var values = me.getForm().getValues();
		var entityList = [];
		//var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		for (var i = 0; i < me.itemObjList.length; i++) {
			var itemObj = me.itemObjList[i];
			//没有系统参数Id,直接忽略
			if (!itemObj.itemId) continue;

			var itemValue = values[itemObj.itemId];
			if (!itemValue) itemValue = "";
			var entity = {
				"Id": itemObj.itemId,
				"ParaValue": itemValue
			};
			entityList.push(entity);
		}
		return entityList;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

		if (!me.getForm().isValid()) return;

		var url = me.getPathRoot() + me.editUrl;
		var params = {
			entityList: me.getSaveInfo()
		};
		if (!params) return;

		params = Ext.JSON.encode(params);
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				me.fireEvent('save', me);
				if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
});
