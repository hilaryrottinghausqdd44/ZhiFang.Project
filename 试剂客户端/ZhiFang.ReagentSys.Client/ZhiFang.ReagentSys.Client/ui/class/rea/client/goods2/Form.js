/**
 * 货品信息
 * @author liangyl
 * @version 2017-09-11
 */
Ext.define('Shell.class.rea.client.goods2.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '货品信息',

	width: 670,
	height: 580,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaGoods',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
	/**获取数据服务路径*/
	selectUrl2: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?isPlanish=true',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 7 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 210,
		labelAlign: 'right'
	},
	/**内容周围距离*/
	bodyPadding: '10px',
	/*厂商*/
	ProdOrg: 'ProdOrg',
	/**一级分类集合*/
	GoodsClass: [],
	/**二级分类集合*/
	GoodsClassType: [],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		var columns = parseInt(me.width / me.defaults.width);
		if (columns > me.layout.columns) me.layout.columns = columns;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//主键ID
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaGoods_Id',
			hidden: true,
			type: 'key'
		});
		//1,1
		items.push({
			fieldLabel: '货品名称',
			name: 'ReaGoods_CName',
			itemId: 'ReaGoods_CName',
			emptyText: '必填项',
			allowBlank: false,
			style: 'color:blue;',
			//			fieldStyle:'background-color: #DFE9F6;border-color: #DFE9F6; background-image: none;', 
			colspan: 2,
			width: me.defaults.width * 2
		});
		//简称
		items.push({
			fieldLabel: '简称',
			emptyText: '简称',
			name: 'ReaGoods_SName',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//英文名
		items.push({
			fieldLabel: '英文名称',
			name: 'ReaGoods_EName',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//拼音字头
		items.push({
			fieldLabel: '拼音字头',
			name: 'ReaGoods_PinYinZiTou',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//产地
		items.push({
			fieldLabel: '产地',
			name: 'ReaGoods_ProdEara',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//1,3
		items.push({
			fieldLabel: '货品编码',
			name: 'ReaGoods_ReaGoodsNo',
			emptyText: '必填项',
			allowBlank: false,
			style: 'color:blue;',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '平台编码',
			name: 'ReaGoods_GoodsNo',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//厂商货品编码
		items.push({
			fieldLabel: '厂商货品编码',
			name: 'ReaGoods_ProdGoodsNo',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//货品序号
		items.push({
			fieldLabel: '货品序号',
			name: 'ReaGoods_GoodsSort',
			locked: true,
			readOnly: true,
			hidden: true,
			itemId: 'ReaGoods_GoodsSort',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '品牌',
			name: 'ReaGoods_ProdOrgName',
			itemId: 'ReaGoods_ProdOrgName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '品牌选择',
				defaultWhere: "bdict.BDictType.DictTypeCode='" + this.ProdOrg + "'"
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(p, record);
				}
			},
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '厂商主键ID',
			itemId: 'ReaGoods_Prod_Id',
			name: 'ReaGoods_Prod_Id',
			xtype: 'textfield',
			hidden: true
		});
		//批准文号
		items.push({
			fieldLabel: '批准文号',
			name: 'ReaGoods_ApproveDocNo',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//国标
		items.push({
			fieldLabel: '国标',
			name: 'ReaGoods_Standard',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//单位
		items.push({
			fieldLabel: '单位',
			name: 'ReaGoods_UnitName',
			itemId: 'ReaGoods_UnitName',
			emptyText: '必填项',
			allowBlank: false,
			style: 'color:blue;',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//单位描述
		items.push({
			fieldLabel: '规格',
			name: 'ReaGoods_UnitMemo',
			itemId: 'ReaGoods_UnitMemo',
			emptyText: '必填项',
			allowBlank: false,
			style: 'color:blue;',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//单价
		items.push({
			xtype: 'numberfield',
			fieldLabel: '推荐价格',
			name: 'ReaGoods_Price',
			decimalPrecision: 4,
			value: 0,
			minValue: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//二级分类
		items.push(me.createSimpleComboBox('一级分类', 'ReaGoods_GoodsClass'));
		//二级分类
		items.push(me.createSimpleComboBox('二级分类', 'ReaGoods_GoodsClassType'));

		//三级分类
		items.push({
			fieldLabel: '部门',
			name: 'ReaGoods_DeptName',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//代码
		items.push({
			fieldLabel: '同系列码',
			emptyText: '标识货品为同系列货品',
			name: 'ReaGoods_ShortCode',
			colspan: 1,
			width: me.defaults.width * 1,
			hidden: false
		});
		//测试数
		items.push({
			fieldLabel: '测试数',
			name: 'ReaGoods_TestCount',
			xtype: 'numberfield',
			value: 0,
			minValue: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//是否使用
		items.push({
			labelWidth: 80,
			width: 220,
			fieldLabel: '启用',
			name: 'ReaGoods_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//有注册证
		items.push({
			fieldLabel: '有注册证',
			name: 'ReaGoods_IsRegister',
			xtype: 'uxBoolComboBox',
			value: false,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//条码类型
		items.push({
			fieldLabel: '条码类型',
			name: 'ReaGoods_BarCodeMgr',
			xtype: 'uxSimpleComboBox',
			value: '0',
			hasStyle: true,
			data: [
				['0', '批条码', 'color:green;'],
				['1', '盒条码', 'color:orange;'],
				['2', '无条码', 'color:black;']
			],
			colspan: 1,
			width: me.defaults.width * 1
		});
		//是否打印条码
		items.push({
			labelWidth: 80,
			width: 220,
			fieldLabel: '是否打印条码',
			name: 'ReaGoods_IsPrintBarCode',
			xtype: 'uxBoolComboBox',
			value: false,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//注册号
		items.push({
			fieldLabel: '注册号',
			name: 'ReaGoods_RegistNo',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//注册日期
		items.push({
			fieldLabel: '注册日期',
			name: 'ReaGoods_RegistDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//注册证有效期
		items.push({
			fieldLabel: '注册证有效期',
			emptyText: '注册证有效期',
			name: 'ReaGoods_RegistNoInvalidDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//显示次序
		items.push({
			fieldLabel: '显示次序',
			name: 'ReaGoods_DispOrder',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'numberfield',
			value: 0,
			minValue: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//理论月用量
		items.push({
			fieldLabel: '理论月用量',
			name: 'ReaGoods_MonthlyUsage',
			xtype: 'numberfield',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//是否性能验证
		items.push({
			fieldLabel: '是否性能验证',
			name: 'ReaGoods_IsNeedPerformanceTest',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//最小单位 
		items.push({
			labelWidth: 80,
			width: 220,
			fieldLabel: '最小单位',
			name: 'ReaGoods_IsMinUnit',
			itemId: 'ReaGoods_IsMinUnit',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//换算系数 
		items.push({
			fieldLabel: '换算系数',
			name: 'ReaGoods_GonvertQty',
			xtype: 'numberfield',
			itemId: 'ReaGoods_GonvertQty',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//适用机型
		items.push({
			fieldLabel: '适用机型',
			name: 'ReaGoods_SuitableType',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//供应商
		items.push({
			fieldLabel: '供应商',
			name: 'ReaGoods_ReaCompanyName',
			colspan: 1,
			width: me.defaults.width * 1
		});

		var columns1 = me.layout.columns;
		if (me.layout.columns == 4) {
			columns1 = 4;
		} else if (me.layout.columns == 5) {
			columns1 = 3;
		} else if (me.layout.columns == 6) {
			columns1 = 4;
		} else if (me.layout.columns == 7) {
			columns1 = 5;
		}
		//挂网流水号
		items.push({
			fieldLabel: '挂网流水号',
			name: 'ReaGoods_NetGoodsNo',
<<<<<<< .mine
			colspan: me.layout.columns,
			width: me.defaults.width * columns1
||||||| .r2653
			colspan:me.layout.columns,
			width: me.defaults.width * columns1
=======
//			colspan:me.layout.columns,
//			width: me.defaults.width * columns1,
			colspan:3,
			width: me.defaults.width * 3
>>>>>>> .r2673
		});
		//物资对照码
		items.push({
			fieldLabel: '物资对照码',
			name: 'ReaGoods_MatchCode',
			// colspan: me.layout.columns,
			// width: me.defaults.width * me.layout.columns
			colspan: 2,
			width: me.defaults.width * 2
		});
		// 新增是否医疗器械（不设置服务返回false，对应否）
		items.push({
			fieldLabel: '是否医疗器械',
			name: 'ReaGoods_IsMed',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 2,
			width: me.defaults.width * 2
			
		});
		// 新增型号
		items.push({
			fieldLabel: '型号',
			name: 'ReaGoods_InvType',
			colspan: 3,
			width: me.defaults.width * 3
		});
		//储藏条件
		items.push({
			height: 40,
			xtype: 'textarea',
			fieldLabel: '储藏条件',
			name: 'ReaGoods_StorageType',
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		});

		//结构组成
		items.push({
			height: 40,
			xtype: 'textarea',
			fieldLabel: '结构组成',
			name: 'ReaGoods_Constitute',
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		});
		//用途
		items.push({
			height: 40,
			xtype: 'textarea',
			fieldLabel: '用途',
			name: 'ReaGoods_Purpose',
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		});
		//货品描述
		items.push({
			height: 40,
			xtype: 'textarea',
			fieldLabel: '货品描述',
			name: 'ReaGoods_GoodsDesc',
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		});
		//货品描述
		items.push({
			height: 120,
			xtype: 'textarea',
			fieldLabel: '验证说明',
			name: 'ReaGoods_VerificationMemo',
			colspan: me.layout.columns,
			width: me.defaults.width * me.layout.columns
		});

		return items;
	},
	getGoodsClass: function(classType, callback) {
		var me = this;

		if (me[classType]) return callback();

		var fields = "ReaGoodsClassVO_CName,ReaGoodsClassVO_Id";
		var url = JShell.System.Path.ROOT +
			'/ReaManageService.svc/RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL?isPlanish=true';

		url = url + "&fields=" + fields; // JShell.JSON.encode(fields);
		url = url + "&classType=" + classType;
		JShell.Server.get(url, function(data) {
			if (data.success && data.value && data.value.list) {
				me[classType] = data.value.list;
			} else {
				me[classType] = [];
			}
			if (callback) callback();
		}, false);
	},
	/**
	 * @description 创建uxSimpleComboBox
	 */
	createSimpleComboBox: function(fieldLabel, itemId) {
		var me = this;
		var dataSet1 = [];
		if (itemId == "ReaGoods_GoodsClass") {
			dataSet1 = me.GoodsClass;
		} else {
			dataSet1 = me.GoodsClassType;
		}
		if (!dataSet1) {
			dataSet1 = [{
				'ReaGoodsClassVO_Id': '',
				'ReaGoodsClassVO_CName': '请选择'
			}];
		}
		var store1 = Ext.create('Ext.data.Store', {
			fields: ['ReaGoodsClassVO_Id', 'ReaGoodsClassVO_CName'],
			data: dataSet1
		});

		var textfield = {
			xtype: 'combobox',
			colspan: 1,
			width: me.defaults.width * 1,
			fieldLabel: "" + fieldLabel,
			name: "" + itemId,
			itemId: "" + itemId,
			editable: true,
			typeAhead: true,
			store: store1,
			queryMode: 'local',
			displayField: 'ReaGoodsClassVO_CName',
			valueField: 'ReaGoodsClassVO_Id',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if (newValue == oldValue) return;

					JShell.Action.delay(function() {
						me.onComLoadData(com.itemId, newValue);
					}, null, 1500);
				}
			}
		};
		return textfield;
	},
	/**
	 * @description 某一记录项输入或选择后处理
	 * @param {Object} itemId
	 * @param {Object} newValue
	 */
	onComLoadData: function(itemId, newValue) {
		var me = this;
		if (!newValue) return;

		var comboBox = me.getComponent(itemId);
		if (!comboBox) return;

		var index1 = comboBox.store.find("ReaGoodsClassVO_CName", newValue);
		if (index1 >= 0) return; //已存在

		//同步科室记录项结果短语集合
		var list3 = comboBox.store.data;
		if (!list3) list3 = [];
		//从集合里判断
		for (var i = 0; i < list3.length; i++) {
			var item = list3[i];
			if (item["ReaGoodsClassVO_CName"] == newValue) {
				index1 = i;
				break;
			}
		}
		if (index1 >= 0) return; //已存在

		//不存在时,自动追加该选择项
		var model = {
			'ReaGoodsClassVO_Id': newValue,
			'ReaGoodsClassVO_CName': newValue
		};
		list3.push(model);
		comboBox.store.loadData(list3);
	},
	onCompAccept: function(p, record) {
		var me = this;
		var Id = me.getComponent('ReaGoods_Prod_Id');
		var CName = me.getComponent('ReaGoods_ProdOrgName');
		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');
		p.close();
	},

	isAdd: function() {
		var me = this;
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle(); //标题更改
		me.enableControl(); //启用所有的操作功能
		me.onResetClick();
	},
	isEdit: function(id) {
		var me = this;
		me.formtype = 'edit';
		me.changeTitle(); //标题更改
		me.load(id);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName: values.ReaGoods_CName,
			EName: values.ReaGoods_EName,
			ShortCode: values.ReaGoods_ShortCode,
			SName: values.ReaGoods_SName,
			ReaGoodsNo: values.ReaGoods_ReaGoodsNo,
			ProdGoodsNo: values.ReaGoods_ProdGoodsNo,
			ProdEara: values.ReaGoods_ProdEara,
			GoodsClass: values.ReaGoods_GoodsClass,
			GoodsClassType: values.ReaGoods_GoodsClassType,
			UnitName: values.ReaGoods_UnitName,
			UnitMemo: values.ReaGoods_UnitMemo,
			ApproveDocNo: values.ReaGoods_ApproveDocNo,
			Standard: values.ReaGoods_Standard,
			StorageType: values.ReaGoods_StorageType,
			Price: values.ReaGoods_Price == "" ? null : values.ReaGoods_Price,
			DispOrder: values.ReaGoods_DispOrder,
			Visible: values.ReaGoods_Visible ? 1 : 0,
			TestCount: values.ReaGoods_TestCount,
			SuitableType: values.ReaGoods_SuitableType,
			Constitute: values.ReaGoods_Constitute,
			Purpose: values.ReaGoods_Purpose,
			GoodsDesc: values.ReaGoods_GoodsDesc,
			BarCodeMgr: values.ReaGoods_BarCodeMgr,
			IsRegister: values.ReaGoods_IsRegister ? 1 : 0,
			IsPrintBarCode: values.ReaGoods_IsPrintBarCode ? 1 : 0,
			IsNeedPerformanceTest: values.ReaGoods_IsNeedPerformanceTest ? 1 : 0,
			//			IsMinUnit:values.ReaGoods_IsMinUnit ? 1 : 0,
			RegistNo: values.ReaGoods_RegistNo,
			RegistDate: JShell.Date.toServerDate(values.ReaGoods_RegistDate),
			RegistNoInvalidDate: JShell.Date.toServerDate(values.ReaGoods_RegistNoInvalidDate),
			//		    GoodsSort:values.ReaGoods_GoodsSort,
			PinYinZiTou: values.ReaGoods_PinYinZiTou,
			GoodsNo: values.ReaGoods_GoodsNo,
			ReaCompanyName: values.ReaGoods_ReaCompanyName,
			DeptName: values.ReaGoods_DeptName,
			MatchCode: values.ReaGoods_MatchCode,
			NetGoodsNo: values.ReaGoods_NetGoodsNo,
<<<<<<< .mine
			VerificationMemo: values.ReaGoods_VerificationMemo
||||||| .r2653
			VerificationMemo:values.ReaGoods_VerificationMemo
=======
			VerificationMemo:values.ReaGoods_VerificationMemo,
			IsMed: values.ReaGoods_IsMed, // 新增的是否医疗器械字段
			InvType: values.ReaGoods_InvType // 新增的型号字段
>>>>>>> .r2673
		};
		//换算系数
		if (values.ReaGoods_MonthlyUsage) {
			entity.MonthlyUsage = values.ReaGoods_MonthlyUsage;
		}
		//换算系数
		if (values.ReaGoods_GonvertQty) {
			entity.GonvertQty = values.ReaGoods_GonvertQty;
		}
		if (values.ReaGoods_ProdOrgName) {
			entity.ProdOrgName = values.ReaGoods_ProdOrgName;
		}
		//厂商
		if (values.ReaGoods_Prod_Id) {
			entity.ProdID = values.ReaGoods_Prod_Id;
		}
		var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		var fields = [
			'Id', 'CName', 'EName', 'ShortCode',
			'ReaGoodsNo', 'ProdGoodsNo',
			'ProdEara', 'SName', 'GoodsNo',
			'GoodsClass', 'GoodsClassType',
			'UnitName', 'UnitMemo', 'DeptName',
			'ApproveDocNo', 'Standard', 'MonthlyUsage',
			'Price', 'DispOrder', 'Visible',
			'StorageType', 'Constitute', 'Purpose', 'GoodsDesc',
			'BarCodeMgr', 'IsRegister', 'IsPrintBarCode',
			'TestCount', 'SuitableType', 'ReaCompanyName',
			'RegistNo', 'RegistDate', 'RegistNoInvalidDate',
			'ProdID', 'ProdOrgName', 'PinYinZiTou',
<<<<<<< .mine
			'IsNeedPerformanceTest', 'NetGoodsNo', 'VerificationMemo,MatchCode'
||||||| .r2653
			'IsNeedPerformanceTest', 'NetGoodsNo','VerificationMemo,MatchCode'
=======
			'IsNeedPerformanceTest', 'NetGoodsNo','VerificationMemo,MatchCode','IsMed','InvType'
>>>>>>> .r2673
		];

		entity.fields = fields.join(',');

		entity.entity.Id = values.ReaGoods_Id;
		return entity;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var CName = me.getComponent('ReaGoods_CName');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				newValue = me.changeCharCode(newValue);
				if (newValue != "") {
					JShell.Action.delay(function() {
						JcallShell.System.getPinYinZiTou(newValue, function(value) {
							me.getForm().setValues({
								ReaGoods_PinYinZiTou: value
							});
						});
					}, null, 200);
				} else {
					me.getForm().setValues({
						ReaGoods_PinYinZiTou: ""
					});
				}
			}
		});

	},
	/**中文符号转换为英文符号*/
	changeCharCode: function(val) {
		var me = this;
		/*正则转换中文标点*/
		val = val.replace(/：/g, ':');
		val = val.replace(/。/g, '.');
		val = val.replace(/“/g, '"');
		val = val.replace(/”/g, '"');
		val = val.replace(/【/g, '[');
		val = val.replace(/】/g, ']');
		val = val.replace(/《/g, '<');
		val = val.replace(/》/g, '>');
		val = val.replace(/，/g, ',');
		val = val.replace(/？/g, '?');
		val = val.replace(/、/g, ',');
		val = val.replace(/；/g, ';');
		val = val.replace(/（/g, '(');
		val = val.replace(/）/g, ')');
		val = val.replace(/‘/g, "'");
		val = val.replace(/’/g, "'");
		val = val.replace(/『/g, "[");
		val = val.replace(/』/g, "]");
		val = val.replace(/「/g, "[");
		val = val.replace(/」/g, "]");
		val = val.replace(/﹃/g, "[");
		val = val.replace(/﹄/g, "]");
		val = val.replace(/〔/g, "{");
		val = val.replace(/〕/g, "}");
		val = val.replace(/—/g, "-");
		val = val.replace(/·/g, ".");
		/*正则转换全角为半角*/
		//字符串先转化成数组  
		val = val.split("");
		for (var i = 0; i < val.length; i++) {
			//全角空格处理  
			if (val[i].charCodeAt(0) === 12288) {
				val[i] = String.fromCharCode(32);
			}
			/*其他全角*/
			if (val[i].charCodeAt(0) > 0xFF00 && val[i].charCodeAt(0) < 0xFFEF) {
				val[i] = String.fromCharCode(val[i].charCodeAt(0) - 65248);
			}
		}
		//数组转换成字符串  
		val = val.join("");
		return val;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		data.ReaGoods_Visible = data.ReaGoods_Visible == '1' ? true : false;
		data.ReaGoods_IsRegister = data.ReaGoods_IsRegister == '1' ? true : false;
		data.ReaGoods_IsPrintBarCode = data.ReaGoods_IsPrintBarCode == '1' ? true : false;
		data.ReaGoods_RegistDate = JShell.Date.getDate(data.ReaGoods_RegistDate);
		data.ReaGoods_RegistNoInvalidDate = JShell.Date.getDate(data.ReaGoods_RegistNoInvalidDate);
		return data;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**重新保存按钮点击处理方法
	 * 如果是最小单位，换算系数=1
	 * */
	onSaveClick: function(bo) {
		var me = this;
		if (!me.getForm().isValid()) return;
		var values = me.getForm().getValues();
		//		var IsMinUnit=me.getComponent('ReaGoods_IsMinUnit');
		var GonvertQty = me.getComponent('ReaGoods_GonvertQty');
		me.checkIsMinUnit(values.ReaGoods_ReaGoodsNo, function(data) {
			if (data && data.value) {
				var list = data.value.list,
					len = list.length;
				if (len == 0) {
					GonvertQty.setValue(1);
				}
			} else {
				GonvertQty.setValue(1);
			}
		});
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if (!params) return;

		var id = params.entity.Id;

		params = Ext.JSON.encode(params);
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				if (me.formtype == 'add') id = data.value.id;
				me.fireEvent('save', me, id, bo);
				if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**
	 * 校验是否已经存在最小单位
	 * @根据货品编码
	 * 已经存在 最小单位的，最小单位值为false,
	 * 不存在最小单位的，最小单位值为true
	 * */
	checkIsMinUnit: function(ReaGoodsNo, callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl2;
		url += "&fields=ReaGoods_ReaGoodsNo&where=reagoods.ReaGoodsNo='" + ReaGoodsNo + "'";
		JShell.Server.get(url, function(data) {
			if (data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}

});
