/**
 * 供货方条码规则维护
 * @author longfc
 * @version 2018-01-10
 */
Ext.define('Shell.class.rea.client.barcodeformat.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '条码规则信息',
	width: 535,
	height: 390,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaCenBarCodeFormat',
	/**修改服务*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaCenBarCodeFormatByField',
	
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 95,
		width: 170,
		labelAlign: 'right'
	},
	/**当前的供货方平台机构编码*/
	PlatformOrgNo: null,
	DispOrder: 0,
	/**条码规则分类为按公共部分和按供应商部*/
	Category: null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = me.width / me.layout.columns - 5;

		if(me.defaults.width < 170) me.defaults.width = 170;
		if(!me.Category && me.PlatformOrgNo)
			me.Category = 2;
		else
			me.Category = 1;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '规则名称',
			name: 'ReaCenBarCodeFormat_CName',
			itemId: 'ReaCenBarCodeFormat_CName',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2
		});
		//条码类型
		items.push({
			fieldLabel: '条码类型',
			name: 'ReaCenBarCodeFormat_BarCodeType',
			itemId: 'ReaCenBarCodeFormat_BarCodeType',
			xtype: 'uxSimpleComboBox',
			allowBlank: false,
			value: "2",
			hasStyle: true,
			data: [
				["1", '一维码', 'color:green;'],
				["2", '二维码', 'color:orange;']
			],
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '规则前缀',
			name: 'ReaCenBarCodeFormat_SName',
			itemId: 'ReaCenBarCodeFormat_SName',
			//allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2
		});
		items.push({
			fieldLabel: '分割符',
			name: 'ReaCenBarCodeFormat_ShortCode',
			itemId: 'ReaCenBarCodeFormat_ShortCode',
			//allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//启用
		items.push({
			fieldLabel: '启用',
			name: 'ReaCenBarCodeFormat_IsUse',
			itemId: 'ReaCenBarCodeFormat_IsUse',
			xtype: 'uxBoolComboBox',
			//value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '优先级别',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_DispOrder',
			itemId: 'ReaCenBarCodeFormat_DispOrder',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '分隔符数',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_SplitCount',
			itemId: 'ReaCenBarCodeFormat_SplitCount',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '供应商编码位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_ProdOrgNoIndex',
			itemId: 'ReaCenBarCodeFormat_ProdOrgNoIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '起始位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_ProdOrgNoStart',
			itemId: 'ReaCenBarCodeFormat_ProdOrgNoStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '供应商编码长度',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_ProdOrgNoLength',
			itemId: 'ReaCenBarCodeFormat_ProdOrgNoLength',
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '货品编码位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_ProdGoodsNoIndex',
			itemId: 'ReaCenBarCodeFormat_ProdGoodsNoIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '起始位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_ProdGoodsNoStart',
			itemId: 'ReaCenBarCodeFormat_ProdGoodsNoStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '货品编码长度',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_ProdGoodsNoLength',
			itemId: 'ReaCenBarCodeFormat_ProdGoodsNoLength',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '批号位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_LotNoIndex',
			itemId: 'ReaCenBarCodeFormat_LotNoIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '批号起始位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_LotNoStart',
			itemId: 'ReaCenBarCodeFormat_LotNoStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '批号长度',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_LotNoLength',
			itemId: 'ReaCenBarCodeFormat_LotNoLength',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '效期位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_InvalidDateIndex',
			itemId: 'ReaCenBarCodeFormat_InvalidDateIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '效期起始位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_InvalidDateStart',
			itemId: 'ReaCenBarCodeFormat_InvalidDateStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '效期长度',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_InvalidDateLength',
			itemId: 'ReaCenBarCodeFormat_InvalidDateLength',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			fieldLabel: '单位位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_UnitIndex',
			itemId: 'ReaCenBarCodeFormat_UnitIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '单位起始位置',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_UnitStart',
			itemId: 'ReaCenBarCodeFormat_UnitStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '单位长度',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_UnitLength',
			itemId: 'ReaCenBarCodeFormat_UnitLength',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//一维条码维护项
		items.push({
			fieldLabel: '<b style="color:blue;">流水号位置</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_DtlNoIndex',
			itemId: 'ReaCenBarCodeFormat_DtlNoIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '<b style="color:blue;">流水号起始位置</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_DtlNoStart',
			itemId: 'ReaCenBarCodeFormat_DtlNoStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '<b style="color:blue;">流水号长度</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_DtlNoLength',
			itemId: 'ReaCenBarCodeFormat_DtlNoLength',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//明细数
		items.push({
			fieldLabel: '<b style="color:blue;">明细数位置</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_GoodsQtyIndex',
			itemId: 'ReaCenBarCodeFormat_GoodsQtyIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '<b style="color:blue;">明细数起始位置</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_GoodsQtyStart',
			itemId: 'ReaCenBarCodeFormat_GoodsQtyStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '<b style="color:blue;">明细数长度</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_GoodsQtyLength',
			itemId: 'ReaCenBarCodeFormat_GoodsQtyLength',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//当前序号
		items.push({
			fieldLabel: '<b style="color:blue;">当前序号位置</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_CurDispOrderIndex',
			itemId: 'ReaCenBarCodeFormat_CurDispOrderIndex',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '<b style="color:blue;">当前序号起始位置</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_CurDispOrderStart',
			itemId: 'ReaCenBarCodeFormat_CurDispOrderStart',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '<b style="color:blue;">当前序号长度</b>',
			xtype: 'numberfield',
			name: 'ReaCenBarCodeFormat_CurDispOrderLength',
			itemId: 'ReaCenBarCodeFormat_CurDispOrderLength',
			IsnotField: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//样例
		items.push({
			xtype: 'textarea',
			fieldLabel: '样例',
			name: 'ReaCenBarCodeFormat_BarCodeFormatExample',
			itemId: 'ReaCenBarCodeFormat_BarCodeFormatExample',
			allowBlank: false,
			colspan: 3,
			width: me.defaults.width * 3,
			height: 50
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaCenBarCodeFormat_Memo',
			itemId: 'ReaCenBarCodeFormat_Memo',
			colspan: 3,
			width: me.defaults.width * 3,
			height: 50
		});

		items.push({
			fieldLabel: '主键ID',
			name: 'ReaCenBarCodeFormat_Id',
			hidden: true,
			type: 'key'
		}, {
			fieldLabel: '条码分类',
			name: 'ReaCenBarCodeFormat_Type',
			itemId: 'ReaCenBarCodeFormat_Type',
			hidden: true,
			value: me.Category
		}, {
			fieldLabel: '供货方平台编码',
			hidden: true,
			name: 'ReaCenBarCodeFormat_PlatformOrgNo',
			itemId: 'ReaCenBarCodeFormat_PlatformOrgNo'
		}, {
			fieldLabel: '条码信息',
			hidden: true,
			name: 'ReaCenBarCodeFormat_RegularExpression',
			itemId: 'ReaCenBarCodeFormat_RegularExpression'
		});
		return items;

	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		me.getComponent('ReaCenBarCodeFormat_IsUse').setValue(true);
		me.getComponent('ReaCenBarCodeFormat_DispOrder').setValue(me.DispOrder);
		if(me.PlatformOrgNo) {
			me.Category = 2;
			me.getComponent('ReaCenBarCodeFormat_PlatformOrgNo').setValue(me.PlatformOrgNo);
		} else
			me.Category = 1;
		me.getComponent('ReaCenBarCodeFormat_Type').setValue(me.Category);
		me.getComponent('ReaCenBarCodeFormat_BarCodeType').setValue("2");
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var isUse = data.ReaCenBarCodeFormat_IsUse;
		if(isUse == "1" || isUse == 1 || isUse == true) isUse = true;
		data.ReaCenBarCodeFormat_IsUse = isUse;
		
		if(!data.ReaCenBarCodeFormat_BarCodeType) {
			data.ReaCenBarCodeFormat_BarCodeType ="2";
		}
		var jsonInfo = me.getBarCodeInfoJSON();
		if(data.ReaCenBarCodeFormat_RegularExpression) {
			jsonInfo = JShell.JSON.decode(data.ReaCenBarCodeFormat_RegularExpression);
			if(!jsonInfo) jsonInfo = me.getBarCodeInfoJSON();
		}
		data.ReaCenBarCodeFormat_ProdOrgNoIndex = jsonInfo.ProdOrgNo.Index;
		data.ReaCenBarCodeFormat_ProdOrgNoStart = jsonInfo.ProdOrgNo.StartIndex;
		data.ReaCenBarCodeFormat_ProdOrgNoLength = jsonInfo.ProdOrgNo.Length;

		data.ReaCenBarCodeFormat_LotNoIndex = jsonInfo.LotNo.Index;
		data.ReaCenBarCodeFormat_LotNoStart = jsonInfo.LotNo.StartIndex;
		data.ReaCenBarCodeFormat_LotNoLength = jsonInfo.LotNo.Length;

		if(!jsonInfo.ProdGoodsNo) {
			jsonInfo.ProdGoodsNo.Index = -1;
			jsonInfo.ProdGoodsNo.StartIndex = -1;
			jsonInfo.ProdGoodsNo.Length = -1;
		}
		data.ReaCenBarCodeFormat_ProdGoodsNoIndex = jsonInfo.ProdGoodsNo.Index;
		data.ReaCenBarCodeFormat_ProdGoodsNoStart = jsonInfo.ProdGoodsNo.StartIndex;
		data.ReaCenBarCodeFormat_ProdGoodsNoLength = jsonInfo.ProdGoodsNo.Length;

		if(!jsonInfo.InvalidDate) {
			jsonInfo.InvalidDate.Index = -1;
			jsonInfo.InvalidDate.StartIndex = -1;
			jsonInfo.InvalidDate.Length = -1;
		}
		data.ReaCenBarCodeFormat_InvalidDateIndex = jsonInfo.InvalidDate.Index;
		data.ReaCenBarCodeFormat_InvalidDateStart = jsonInfo.InvalidDate.StartIndex;
		data.ReaCenBarCodeFormat_InvalidDateLength = jsonInfo.InvalidDate.Length;

		//流水号
		if(!jsonInfo.DtlNo) {
			jsonInfo.DtlNo.Index = -1;
			jsonInfo.DtlNo.StartIndex = -1;
			jsonInfo.DtlNo.Length = -1;
		}
		data.ReaCenBarCodeFormat_DtlNoIndex = jsonInfo.DtlNo.Index;
		data.ReaCenBarCodeFormat_DtlNoStart = jsonInfo.DtlNo.StartIndex;
		data.ReaCenBarCodeFormat_DtlNoLength = jsonInfo.DtlNo.Length;

		//明细数量
		if(!jsonInfo.GoodsQty) {
			jsonInfo.GoodsQty.Index = -1;
			jsonInfo.GoodsQty.StartIndex = -1;
			jsonInfo.GoodsQty.Length = -1;
		}
		data.ReaCenBarCodeFormat_GoodsQtyIndex = jsonInfo.GoodsQty.Index;
		data.ReaCenBarCodeFormat_GoodsQtyStart = jsonInfo.GoodsQty.StartIndex;
		data.ReaCenBarCodeFormat_GoodsQtyLength = jsonInfo.GoodsQty.Length;
		//当前序号
		if(!jsonInfo.CurDispOrder) {
			jsonInfo.CurDispOrder.Index = -1;
			jsonInfo.CurDispOrder.StartIndex = -1;
			jsonInfo.CurDispOrder.Length = -1;
		}
		data.ReaCenBarCodeFormat_CurDispOrderIndex = jsonInfo.CurDispOrder.Index;
		data.ReaCenBarCodeFormat_CurDispOrderStart = jsonInfo.CurDispOrder.StartIndex;
		data.ReaCenBarCodeFormat_CurDispOrderLength = jsonInfo.CurDispOrder.Length;

		var UnitIndex = -1,
			UnitStart = -1,
			UnitLength = -1;
		if(jsonInfo.Unit) {
			UnitIndex = jsonInfo.Unit.Index;
			UnitStart = jsonInfo.Unit.StartIndex;
			UnitLength = jsonInfo.Unit.Length;
		}
		data.ReaCenBarCodeFormat_UnitIndex = UnitIndex;
		data.ReaCenBarCodeFormat_UnitStart = UnitStart;
		data.ReaCenBarCodeFormat_UnitLength = UnitLength;
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var barCodeFormatExample = values.ReaCenBarCodeFormat_BarCodeFormatExample.replace(/\\/g, '&#92');
		barCodeFormatExample = barCodeFormatExample.replace(/[\r\n]/g, '');

		var memo = values.ReaCenBarCodeFormat_Memo.replace(/\\/g, '&#92');
		memo = memo.replace(/[\r\n]/g, '');

		var isUse = values.ReaCenBarCodeFormat_IsUse;
		if(isUse == "1" || isUse == 1 || isUse == true) isUse = 1;
		else isUse = 0;

		var PlatformOrgNo = values.ReaCenBarCodeFormat_PlatformOrgNo;
		if(!PlatformOrgNo)
			PlatformOrgNo = null;

		var DispOrder = values.ReaCenBarCodeFormat_DispOrder;
		if(!DispOrder) DispOrder = 0;

		var type = values.ReaCenBarCodeFormat_Type;
		if(!type) type = me.Category;

		var jsonInfo = JShell.JSON.encode(me.getSaveBarCodeInfo());
		jsonInfo = jsonInfo.replace(/"/g, "'");

		var entity = {
			"Id": -1,
			"Type": type,
			"BarCodeType": values.ReaCenBarCodeFormat_BarCodeType,
			"CName": values.ReaCenBarCodeFormat_CName,
			"SName": values.ReaCenBarCodeFormat_SName,
			"ShortCode": values.ReaCenBarCodeFormat_ShortCode,
			"SplitCount": values.ReaCenBarCodeFormat_SplitCount,
			"DispOrder": DispOrder,
			"PlatformOrgNo": PlatformOrgNo,
			"IsUse": isUse,
			"BarCodeFormatExample": barCodeFormatExample,
			"RegularExpression": jsonInfo,
			"Memo": memo
		};
		if(values.ReaCenBarCodeFormat_Id) entity.Id = values.ReaCenBarCodeFormat_Id;
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
			'Id', 'Type','BarCodeType', 'CName', 'SName', 'ShortCode', 'IsUse', 'BarCodeFormatExample', 'PlatformOrgNo', 'Memo', 'DispOrder', 'RegularExpression'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaCenBarCodeFormat_Id;
		return entity;
	},
	getSaveBarCodeInfo: function() {
		var me = this;
		var values = me.getForm().getValues();
		var ProdOrgNoIndex = values.ReaCenBarCodeFormat_ProdOrgNoIndex;
		var ProdOrgNoStart = values.ReaCenBarCodeFormat_ProdOrgNoStart;
		var ProdOrgNoLength = values.ReaCenBarCodeFormat_ProdOrgNoLength;

		var LotNoIndex = values.ReaCenBarCodeFormat_LotNoIndex;
		var LotNoStart = values.ReaCenBarCodeFormat_LotNoStart;
		var LotNoLength = values.ReaCenBarCodeFormat_LotNoLength;

		var ProdGoodsNoIndex = values.ReaCenBarCodeFormat_ProdGoodsNoIndex;
		var ProdGoodsNoStart = values.ReaCenBarCodeFormat_ProdGoodsNoStart;
		var ProdGoodsNoLength = values.ReaCenBarCodeFormat_ProdGoodsNoLength;

		var InvalidDateIndex = values.ReaCenBarCodeFormat_InvalidDateIndex;
		var InvalidDateStart = values.ReaCenBarCodeFormat_InvalidDateStart;
		var InvalidDateLength = values.ReaCenBarCodeFormat_InvalidDateLength;

		var UnitIndex = values.ReaCenBarCodeFormat_UnitIndex;
		var UnitStart = values.ReaCenBarCodeFormat_UnitStart;
		var UnitLength = values.ReaCenBarCodeFormat_UnitLength;

		if(!ProdOrgNoIndex) ProdOrgNoIndex = -1;
		if(!ProdOrgNoStart) ProdOrgNoStart = -1;
		if(!ProdOrgNoLength) ProdOrgNoLength = -1;

		if(!LotNoIndex) LotNoIndex = -1;
		if(!LotNoStart) LotNoStart = -1;
		if(!LotNoLength) LotNoLength = -1;

		if(!ProdGoodsNoIndex) ProdGoodsNoIndex = -1;
		if(!ProdGoodsNoStart) ProdGoodsNoStart = -1;
		if(!ProdGoodsNoLength) ProdGoodsNoLength = -1;

		if(!InvalidDateIndex) InvalidDateIndex = -1;
		if(!InvalidDateStart) InvalidDateStart = -1;
		if(!InvalidDateLength) InvalidDateLength = -1;

		if(!UnitIndex) UnitIndex = -1;
		if(!UnitStart) UnitStart = -1;
		if(!UnitLength) UnitLength = -1;

		//流水号
		var DtlNoIndex = values.ReaCenBarCodeFormat_DtlNoIndex;
		var DtlNoStart = values.ReaCenBarCodeFormat_DtlNoStart;
		var DtlNoLength = values.ReaCenBarCodeFormat_DtlNoLength;
		if(!DtlNoIndex) DtlNoIndex = -1;
		if(!DtlNoStart) DtlNoStart = -1;
		if(!DtlNoLength) DtlNoLength = -1;
		//明细总数
		var GoodsQtyIndex = values.ReaCenBarCodeFormat_GoodsQtyIndex;
		var GoodsQtyStart = values.ReaCenBarCodeFormat_GoodsQtyStart;
		var GoodsQtyLength = values.ReaCenBarCodeFormat_GoodsQtyLength;
		if(!GoodsQtyIndex) GoodsQtyIndex = -1;
		if(!GoodsQtyStart) GoodsQtyStart = -1;
		if(!GoodsQtyLength) GoodsQtyLength = -1;
		//当前序号
		var CurDispOrderIndex = values.ReaCenBarCodeFormat_CurDispOrderIndex;
		var CurDispOrderStart = values.ReaCenBarCodeFormat_CurDispOrderStart;
		var CurDispOrderLength = values.ReaCenBarCodeFormat_CurDispOrderLength;
		if(!CurDispOrderIndex) CurDispOrderIndex = -1;
		if(!CurDispOrderStart) CurDispOrderStart = -1;
		if(!CurDispOrderLength) CurDispOrderLength = -1;

		var jsonInfo = {
			"LotNo": { //批号
				"Index": LotNoIndex, //位置
				"StartIndex": LotNoStart, //起始位置
				"Length": LotNoLength //长度
			},
			"InvalidDate": { //效期
				"Index": InvalidDateIndex,
				"StartIndex": InvalidDateStart,
				"Length": InvalidDateLength
			},
			"ProdOrgNo": { //厂商机构码(供应商)
				"Index": ProdOrgNoIndex,
				"StartIndex": ProdOrgNoStart,
				"Length": ProdOrgNoLength
			},
			"ProdGoodsNo": { //厂商货品编码
				"Index": ProdGoodsNoIndex,
				"StartIndex": ProdGoodsNoStart,
				"Length": ProdGoodsNoLength
			},
			"Unit": { //货品单位
				"Index": UnitIndex,
				"StartIndex": UnitStart,
				"Length": UnitLength
			},
			"DtlNo": { //流水号
				"Index": DtlNoIndex,
				"StartIndex": DtlNoStart,
				"Length": DtlNoLength
			},
			"GoodsQty": { //明细总数
				"Index": GoodsQtyIndex,
				"StartIndex": GoodsQtyStart,
				"Length": GoodsQtyLength
			},
			"CurDispOrder": { //当前序号
				"Index": CurDispOrderIndex,
				"StartIndex": CurDispOrderStart,
				"Length": CurDispOrderLength
			}
		};
		return jsonInfo;
	},
	getBarCodeInfoJSON: function() {
		var me = this;
		var jsonInfo = {
			"LotNo": { //批号
				"Index": -1, //位置
				"StartIndex": 0, //起始位置
				"Length": -1 //长度
			},
			"InvalidDate": { //效期
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"ProdOrgNo": { //厂商机构码
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"ProdGoodsNo": { //厂商货品编码
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"Unit": { //货品单位
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"DtlNo": { //流水号
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"GoodsQty": { //明细总数
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			},
			"CurDispOrder": { //当前序号
				"Index": -1,
				"StartIndex": -1,
				"Length": -1
			}
		};
		return jsonInfo;
	}
});