/**
 * lodop模板维护
 * @author longfc
 * @version 2019-09-18
 */
Ext.define('Shell.class.sysbase.lodoptemplet.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '模板信息',
	width: 375,
	height: 400,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBLodopTempletById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddBLodopTemplet',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateBLodopTempletByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**布局方式*/
	//layout: 'anchor',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		//anchor: '100%',
		labelWidth: 75,
		width: 175,
		labelAlign: 'right'
	},
	PagSizeList: [],
	//新增时的默认模板的打印模板
	CurModel: "",

	afterRender: function() {
		var me = this;
		me.createPagSizeList(-1);
		me.callParent(arguments);
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [{
				fieldLabel: '模板名称',
				name: 'BLodopTemplet_CName',
				itemId: 'BLodopTemplet_Name',
				emptyText: '必填项',
				colspan: 2,
				width: me.defaults.width * 2,
				allowBlank: false
			},
			{
				fieldLabel: '模板类型',
				xtype: 'uxSimpleComboBox',
				name: 'BLodopTemplet_TypeCode',
				itemId: 'BLodopTemplet_TypeCode',
				//data:JShell.System.Enum.getList('TypeCode'),
				data: [
					['1', '入库一维条码'],
					['2', '入库二维条码']
					// ,['3', '其他模板']  先把这个选项禁用掉
				],
				colspan: 2,
				width: me.defaults.width * 2,
				allowBlank: false
			},
			{
				fieldLabel: '打印机选择',
				emptyText: '纸张类型使用',
				xtype: 'uxSimpleComboBox',
				name: 'PrinterList',
				itemId: 'PrinterList',
				//hidden:true,
				data: me.createPrinterList(),
				colspan: 2,
				width: me.defaults.width * 2,
				listeners: {
					change: function(field, newValue) {
						me.createPagSizeList(newValue);
					}
				}
			},
			{
				fieldLabel: '纸张选择',
				xtype: 'uxSimpleComboBox',
				name: 'BLodopTemplet_PaperType',
				itemId: 'BLodopTemplet_PaperType',
				data: [],
				colspan: 2,
				width: me.defaults.width * 2
			},
			{
				fieldLabel: '打印方向',
				xtype: 'uxSimpleComboBox',
				name: 'BLodopTemplet_PrintingDirection',
				itemId: 'BLodopTemplet_PrintingDirection',
				data: [
					['1', '纵(正)向打印，固定纸张'],
					['2', '横向打印，固定纸张'],
					['3', '纵(正)向打印,高度自适应'],
					['0', '按打印机缺省设置']
				],
				colspan: 2,
				width: me.defaults.width * 2,
				allowBlank: false
			},
			{
				fieldLabel: '纸宽',
				name: 'BLodopTemplet_PaperWidth',
				xtype: 'numberfield',
				value: 0,
				allowBlank: false,
				colspan: 1,
				width: me.defaults.width * 1
			},
			{
				fieldLabel: '纸高',
				name: 'BLodopTemplet_PaperHigh',
				xtype: 'numberfield',
				value: 0,
				allowBlank: false,
				colspan: 1,
				width: me.defaults.width * 1
			},
			{
				fieldLabel: '宽高单位',
				xtype: 'uxSimpleComboBox',
				name: 'BLodopTemplet_PaperUnit',
				itemId: 'BLodopTemplet_PaperUnit',
				data: [
					['mm', '毫米'],
					['cm', '厘米']
				],
				allowBlank: false,
				colspan: 1,
				width: me.defaults.width * 1
			},
			{
				fieldLabel: '显示次序',
				name: 'BLodopTemplet_DispOrder',
				xtype: 'numberfield',
				value: 0,
				allowBlank: false,
				colspan: 1,
				width: me.defaults.width * 1
			},
			{
				boxLabel: '是否使用',
				name: 'BLodopTemplet_IsUse',
				xtype: 'checkbox',
				checked: true,
				colspan: 2,
				width: me.defaults.width * 2
			},
			{
				fieldLabel: '模板代码',
				height: 385,
				labelAlign: 'top',
				name: 'BLodopTemplet_TemplateCode',
				itemId: "BLodopTemplet_TemplateCode",
				xtype: 'textarea',
				colspan: 2,
				width: me.defaults.width * 2,
				// readOnly: true,
				// locked: true,
				allowBlank: false
			},
			{
				fieldLabel: '描述',
				height: 65,
				labelAlign: 'top',
				name: 'BLodopTemplet_Memo',
				xtype: 'textarea',
				colspan: 2,
				width: me.defaults.width * 2
			},
			{
				fieldLabel: '主键数据项及测试值',
				itemId: 'BLodopTemplet_DataTestValue',
				name: 'BLodopTemplet_DataTestValue',
				hidden: true
			},
			{
				fieldLabel: '主键ID',
				name: 'BLodopTemplet_Id',
				hidden: true
			}
		];

		return items;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.setDefaultVale();
	},
	/**
	 * 新增时按选择的模板给表单项赋值
	 */
	setDefaultVale: function(defauleV) {
		var me = this;
		if (me.CurModel && !defauleV) {
			defauleV = {
				"BLodopTemplet_CName": me.CurModel.Name,
				"BLodopTemplet_PaperType": me.CurModel.PaperType,
				"BLodopTemplet_PrintingDirection": me.CurModel.PrintingDirection,
				"BLodopTemplet_PaperWidth": me.CurModel.PaperWidth,
				"BLodopTemplet_PaperHigh": me.CurModel.PaperHigh,
				"BLodopTemplet_PaperUnit": me.CurModel.PaperUnit,
				"BLodopTemplet_TemplateCode": me.CurModel.Content
			};
		}
		if (defauleV) me.getForm().setValues(defauleV);
	},
	getCLodop: function(type) {
		var me = this;
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		var LODOP = me.Lodop.getLodop(true);
		if (!LODOP) {
			//JShell.Msg.error("LODOP打印控件获取出错!");
			return;
		}
		return LODOP;
	},
	/**
	 * 获取客户端电脑上的打印机集合信息
	 * */
	createPrinterList: function() {
		var me = this;
		var printerList = [];
		var LODOP = me.getCLodop();
		if (!LODOP || !CLODOP) return printerList;
		var iCount = 0;
		if (CLODOP)
			iCount = CLODOP.GET_PRINTER_COUNT();
		var iIndex = 0;
		for (var i = 0; i < iCount; i++) {
			printerList.push([i, CLODOP.GET_PRINTER_NAME(i)]);
			iIndex++;
		}

		return printerList;
	},
	/**
	 * 获取当前选择的打印机
	 */
	getSelectedPrintIndex: function() {
		var me = this;
		var selectedPrintIndex = -1;
		var printerList = me.getComponent('PrinterList');
		if (printerList) {
			selectedPrintIndex = printerList.getValue();
		} else {
			selectedPrintIndex = -1;
		}
		return selectedPrintIndex;
	},
	/**
	 * 获取纸张集合信息
	 */
	createPagSizeList: function(selectedPrintIndex1) {
		var me = this;
		var pagSizeList = [];
		var LODOP = me.getCLodop();
		if (!LODOP || !CLODOP) return pagSizeList;
		var selectedPrintIndex = selectedPrintIndex1;
		if (!selectedPrintIndex) selectedPrintIndex = me.getSelectedPrintIndex();
		var strPageSizeList = LODOP.GET_PAGESIZES_LIST(selectedPrintIndex, "\n");
		var lists = strPageSizeList.split("\n");
		for (i in lists) {
			pagSizeList.push([lists[i], lists[i]]);
		}
		if (selectedPrintIndex1 >= -1) {
			var paperType = me.getComponent('BLodopTemplet_PaperType');
			paperType.loadData(pagSizeList);
		}
		return pagSizeList;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.BLodopTemplet_CName,
			TypeCode: values.BLodopTemplet_TypeCode,
			PaperType: values.BLodopTemplet_PaperType,
			PrintingDirection: values.BLodopTemplet_PrintingDirection,
			PaperWidth: values.BLodopTemplet_PaperWidth,
			PaperHigh: values.BLodopTemplet_PaperHigh,
			PaperUnit: values.BLodopTemplet_PaperUnit,
			TemplateCode: values.BLodopTemplet_TemplateCode,
			DataTestValue: values.BLodopTemplet_DataTestValue,
			IsUse: values.BLodopTemplet_IsUse ? true : false,
			DispOrder: values.BLodopTemplet_DispOrder,
			Memo: values.BLodopTemplet_Memo
		};

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(), // 注意这里的fields里面的PrinterList字段没有'_'
			entity = me.getAddParams(),
			fieldsArr = [];
		for (var i in fields) {
			var arr = fields[i].split('_');
			// if (arr.length > 2) continue; // 数组的长度为1,2，这里的continue没有意义
			if (arr.length == 2) {
				fieldsArr.push(arr[1]);
			} else{ // 注意这里的fields里面的PrinterList字段没有'_' ，将这个字段去除
				continue;
			}
			
		}
		entity.fields = fieldsArr.join(',');

		entity.entity.Id = values.BLodopTemplet_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	}
});
