/***
 * 设置数据过滤条件表单
 */
Ext.define('Shell.class.sysbase.rowfilter.basic.AddForm', {
	extend: 'Ext.form.Panel',

	title: '',
	isLoadingComplete: false,
	bodyPadding: 5,
	labelAlign: 'right',
	autoScroll: true,
	layout: 'absolute',
	//获取数据对象内容时后台接收的参数名称
	objectPropertyParam: 'EntityName',

	moduleOperId: '',
	//模块操作列表选中行
	moduleOperSelect: null,

	//下拉列表框树的所需的数据对象名称
	objectName: '',
	//下拉列表框树的所需的数据对象中文名
	objectCName: '数据对象',

	isSuccessMsg: true,
	//复选组选择器查询数据服务
	selectorServerUrl: '',
	//复选组选择器keyField数据项匹配字段
	iKeyField: 'inputValue',
	//复选组选择器textField数据项匹配字段
	iTextField: 'boxLabel',

	//查询宏命令(HQL)
	getSearchBTDMacroCommandByHQLUrl: JShell.System.Path.ROOT + '/ConstructionService.svc/CS_UDTO_SearchBTDMacroCommandByHQL',

	selectMacroCommandFields: 'BTDMacroCommand_Id,BTDMacroCommand_CName,BTDMacroCommand_EName,BTDMacroCommand_MacroCode,BTDMacroCommand_TypeCode,BTDMacroCommand_TypeName',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.items = [];
	},
	createFristRowItems: function() {
		var me = this;
		me.items = [{
			xtype: 'textfield',
			name: 'rowFilterCName',
			itemId: 'rowFilterCName',
			fieldLabel: '名称',
			labelWidth: 60,
			width: 360,
			x: 5,
			y: 5,
			labelAlign: me.labelAlign
		}, {
			xtype: 'checkboxfield',
			itemId: 'defaultCondition',
			name: 'defaultCondition',
			x: 382,
			y: 5,
			fieldLabel: '默认条件',
			labelWidth: 60,
			width: 160,
			boxLabel: '',
			inputValue: 'true',
			uncheckedValue: 'false',
			labelAlign: me.labelAlign
		}];
	},
	//交互字段
	createInteractionField: function(treeStore) {
		var me = this;
		var InteractionField = {
			xtype: 'combobox',
			name: 'InteractionField',
			itemId: 'InteractionField',
			x: 5,
			y: 40,
			fieldLabel: '属性',
			labelWidth: 60,
			width: 360,
			mode: 'local',
			editable: false,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			labelAlign: me.labelAlign,
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: me.columnTypeList
			}),
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {}
			}
		};
		return InteractionField;
	},
	//值类型
	createColumnType: function() {
		var me = this;
		var ColumnType = {
			xtype: 'combobox',
			name: 'ColumnType',
			itemId: 'ColumnType',
			fieldLabel: '值类型',
			labelWidth: 60,
			width: 240,
			height: 22,
			hidden: false,
			x: 382,
			y: 40,
			mode: 'local',
			editable: false,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			labelAlign: me.labelAlign,
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: me.columnTypeList
			}),
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					me.onColumnTypeChange(com, newValue, oldValue, e, eOpts);
				}
			}
		};
		return ColumnType;
	},
	onColumnTypeChange: function(com, newValue, oldValue, e, eOpts) {
		var me = this;
		var newValue = com.getValue();
		//关系选择项
		var oTypeCom = me.getoperationType();
		//宏
		var cbomacrosList = me.getcbomacrosList();
		var cbomacrosListTwo = me.getcbomacrosListTwo();
		//日期
		var datefieldCom = me.getdatefieldCom();
		//布尔勾选
		var booleanCom = me.getbooleanCom();
		//数值型
		var numberfield = me.gettxtNumberfield();
		//字符型
		var txtString = me.gettxtString();
		//关联
		var btnRelationCom = me.getbtnRelationCom();

		var otype = me.getoperationTypeValue();
		var numberfieldTwo = me.gettxtNumberfieldTwo();
		var txtStringTwo = me.gettxtStringTwo();
		var datefieldComTwo = me.getdatefieldComTwo();
		datefieldComTwo.setVisible(false);
		numberfieldTwo.setVisible(false);
		txtStringTwo.setVisible(false);

		cbomacrosList.setVisible(false);
		cbomacrosListTwo.setVisible(false);
		datefieldCom.setVisible(false);
		booleanCom.setVisible(false);

		numberfield.setVisible(false);
		txtString.setVisible(false);
		btnRelationCom.setVisible(false);
		me.getContrastRelation().setVisible(false);
		me.getContrastInteractionField().setVisible(false);

		switch(newValue) {
			case "macros": //宏命令macrosOperationType
				cbomacrosList.setVisible(true);
				var data = me.createBTDMacroCommandStore('');
				cbomacrosList.store.loadData(data);
				oTypeCom.store.loadData(me.macrosOperationType);
				oTypeCom.setValue("=");
				break;
			case "date": //日期型
				oTypeCom.store.loadData(me.dateOperationType);
				oTypeCom.setValue("=");
				datefieldCom.setVisible(true);
				break;
			case "boolean": //布尔勾选
				booleanCom.setVisible(true);
				break;
			case "relation": //关联
				oTypeCom.store.loadData(me.relationOperationType);
				oTypeCom.setValue("in");
				btnRelationCom.setVisible(true);
				me.getContrastRelation().setVisible(true);
				break;
			case "string": //字符串
				oTypeCom.store.loadData(me.stringOperationType);
				txtString.setVisible(true);
				oTypeCom.setValue("=");
				break;
			case "number": //数值型
				oTypeCom.store.loadData(me.numberOperationType);
				oTypeCom.setValue("=");
				numberfield.setVisible(true);
				break;
			case "contrast": //对比属性型
				me.ContrastInteractionField.setVisible(true);
				oTypeCom.store.loadData(me.contrastOperationType);
				oTypeCom.setValue("=");
				break;
			default:
				break;
		}
	},
	//关系
	createOperationType: function() {
		var me = this;
		var operationType = { //运算关系
			xtype: 'combobox',
			name: 'operationType',
			itemId: 'operationType',
			fieldLabel: '关系',
			labelWidth: 60,
			width: 360,
			x: 5,
			y: 76,
			mode: 'local',
			editable: false,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			labelAlign: me.labelAlign,
			autoSelect: true,
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: me.stringOperationType
			}),
			listeners: {
				select: function(com, records, eOpts) {
					me.onOperationTypeSelect(com, records, eOpts);
				}
			}
		};
		return operationType;
	},
	onOperationTypeSelect: function(com, records, eOpts) {
		var me = this;
	},
	createOthersItems: function() {
		var me = this;
		me.items.push({ //宏下列框
			xtype: 'combobox',
			name: 'cbomacrosList',
			itemId: 'cbomacrosList',
			labelAlign: me.labelAlign,
			fieldLabel: '宏',
			labelWidth: 60,
			width: 240,
			hidden: true,
			x: 382,
			y: 76,
			mode: 'local',
			editable: false,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: []
			})
		}, { //宏下列框二
			xtype: 'combobox',
			name: 'cbomacrosListTwo',
			itemId: 'cbomacrosListTwo',
			labelAlign: me.labelAlign,
			fieldLabel: '宏选择二',
			labelWidth: 60,
			width: 240,
			hidden: true,
			x: 638,
			y: 76,
			mode: 'local',
			editable: false,
			typeAhead: true,
			forceSelection: true,
			queryMode: 'local',
			displayField: 'text',
			valueField: 'value',
			store: new Ext.data.Store({
				fields: ['value', 'text'],
				autoLoad: true,
				data: []
			})
		}, { //日期值录入框
			xtype: 'datefield',
			name: 'datefieldCom',
			itemId: 'datefieldCom',
			fieldLabel: '日期值',
			labelWidth: 60,
			labelAlign: me.labelAlign,
			width: 180,
			value: new Date(),
			format: 'Y-m-d',
			editable: true,
			hidden: true,
			x: 382,
			y: 76
		}, { //日期值第二录入框
			xtype: 'datefield',
			name: 'datefieldComTwo',
			itemId: 'datefieldComTwo',
			fieldLabel: '日期二',
			labelWidth: 60,
			labelAlign: me.labelAlign,
			width: 180,
			value: new Date(),
			format: 'Y-m-d',
			editable: true,
			hidden: true,
			x: 572,
			y: 76
		}, { //布尔勾选值选择框
			xtype: 'checkboxfield',
			itemId: 'booleanCom',
			name: 'booleanCom',
			hidden: true,
			x: 382,
			y: 76,
			fieldLabel: '布尔值',
			labelWidth: 60,
			width: 160,
			boxLabel: '',
			inputValue: 'true',
			uncheckedValue: 'false',
			labelAlign: 'right'
		}, { //数值型结果录入值
			xtype: 'numberfield',
			width: 200,
			hidden: true,
			name: 'txtNumberfield',
			//输入数字的精度，默认为保留小数点后2位
			decimalPrecision: 5,
			itemId: 'txtNumberfield',
			fieldLabel: '数值',
			labelWidth: 60,
			x: 382,
			y: 76,
			labelAlign: me.labelAlign
		}, { //数值型结果录入值二
			xtype: 'numberfield',
			width: 200,
			hidden: true,
			//输入数字的精度，默认为保留小数点后2位
			decimalPrecision: 5,
			name: 'txtNumberfieldTwo',
			itemId: 'txtNumberfieldTwo',
			fieldLabel: '数值二',
			labelWidth: 60,
			x: 582,
			y: 76,
			labelAlign: me.labelAlign
		}, { //字符型结果录入值
			xtype: 'textfield',
			hidden: true,
			width: 240,
			emptyText: '输入多项时请用英文逗号分隔',
			name: 'txtString',
			itemId: 'txtString',
			fieldLabel: '结果值',
			labelWidth: 60,
			x: 382,
			y: 76,
			labelAlign: me.labelAlign
		}, { //字符型结果录入值二
			xtype: 'textfield',
			hidden: true,
			width: 240,
			name: 'txtStringTwo',
			itemId: 'txtStringTwo',
			fieldLabel: '结果二',
			labelWidth: 60,
			x: 608,
			y: 76,
			labelAlign: me.labelAlign
		});
	},
	//关联类型的值选择
	createButtonRelationCom: function(treeStore) {
		var me = this;
		var btn = {
			xtype: 'button',
			text: '请选择',
			hidden: true,
			itemId: 'btnRelationCom',
			cls: "btn btn-default btn-sm active",
			iconCls: 'button-search',
			x: 635, //762
			y: 70,
			listeners: {
				click: function(com, e, eOpts) {
					var fieldValue = me.getInteractionFieldValue();
					var arr = fieldValue.split('_');
					var fieldCname = me.getDisplayValue();
					var arrCName = fieldCname.split('.');
					//选择器的显示字段中文名称
					var cnameShow = me.getContrastRelationValue();
					var type = 'Id';
					if(fieldValue) {
						if(cnameShow == null || cnameShow == "") {
							JcallShell.Msg.alert('请先选择选择器的显示字段', null, 1000);
							return;
						} else {
							var arr = fieldValue.split('_');
							var lastName = '' + arr[arr.length - 1];

							if(lastName == 'Id') {
								type = 'Id';
							} else if(lastName == 'CName' || arrCName[arrCName.length - 1] == '名称' || arrCName[arrCName.length - 1] == '中文名称') {
								type = 'CName';
							}
							//不一定所有的数据对象的中文名称都是CName,所以选择器需要显示字段选择
							if(lastName == 'Id' || lastName == 'CName' || type == 'CName') {
								var objectName = (arr.length > 2) ? (arr[arr.length - 2]) : (arr[0]);
								var tempArr = cnameShow.split('_');
								var id = objectName + '_Id'; //选择器隐藏值字段
								var cname = objectName + '_' + tempArr[arr.length - 1]; //选择器显示值字段
								var fields = id + ',' + cname;
								var params = '?isPlanish=true&page=1&start=0&limit=10000';
								//单表获取数据的服务--SingleTableService.svc,其他数据对象获取数据的服务RBACService.svc/RBAC_UDTO_Search
								var myUrl = JShell.System.Path.ROOT + me.getObjectSearchUrl(objectName);
								myUrl = myUrl + params + '&fields=' + fields;
								me.selectorServerUrl = myUrl;
								me.iKeyField = id;
								me.iTextField = cname;
								me.openAppShowWin(type);
							}
						}
					} else {
						JcallShell.Msg.alert('关联类型的属性选择只能选择主键或中文名称', null, 1000);
					}
				}
			}
		};
		return btn;
	},
	//获取对象的查询服务
	getObjectSearchUrl: function(objectName) {
		var me = this;
		var url = '';
		var enumList = {

			RBACModule: '/RBACService.svc/RBAC_UDTO_SearchRBACModuleByHQL',
			RBACRole: '/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL',
			HRDept: '/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL',
			RBACModule: '/RBACService.svc/RBAC_UDTO_SearchRBACModuleByHQL',
			HREmployee: '/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL',
			PContract: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractByHQL',
			PClient: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQL',
			PDict: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL',

			EEquip: '/QMSReport.svc/ST_UDTO_SearchEEquipByHQL'
		};
		url = enumList[objectName];
		if(!url) {
			url = '/SingleTableService.svc/ST_UDTO_Search' + objectName + 'ByHQL';
		}
		return url;
	},
	//数值型结果录入值二
	gettxtNumberfieldTwoValue: function() {
		var me = this;
		var com = me.getComponent('txtNumberfieldTwo');
		var value = com.getValue();
		return value;
	},
	//数值型结果录入框二
	gettxtNumberfieldTwo: function() {
		var me = this;
		var com = me.getComponent('txtNumberfieldTwo');
		return com;
	},
	//数值型结果录入值
	gettxtNumberfieldValue: function() {
		var me = this;
		var com = me.getComponent('txtNumberfield');
		var value = com.getValue();
		return value;
	},
	//数值型结果录入框
	gettxtNumberfield: function() {
		var me = this;
		var com = me.getComponent('txtNumberfield');
		return com;
	},
	//字符结果录入值
	gettxtStringValue: function() {
		var me = this;
		var com = me.getComponent('txtString');
		var value = com.getValue();
		return value;
	},
	//字符结果录入框
	gettxtString: function() {
		var me = this;
		var com = me.getComponent('txtString');
		return com;
	},
	//字符结果录入值二
	gettxtStringTwoValue: function() {
		var me = this;
		var com = me.getComponent('txtStringTwo');
		var value = com.getValue();
		return value;
	},
	//字符结果录入框二
	gettxtStringTwo: function() {
		var me = this;
		var com = me.getComponent('txtStringTwo');
		return com;
	},
	//布尔勾选值
	getbooleanComValue: function() {
		var me = this;
		var com = me.getComponent('booleanCom');
		var value = com.getValue();
		return value;
	},
	//布尔勾选框
	getbooleanCom: function() {
		var me = this;
		var com = me.getComponent('booleanCom');
		return com;
	},
	//日期选择框值
	getdatefieldComValue: function() {
		var me = this;
		var com = me.getComponent('datefieldCom');
		var value = com.getValue();
		return value;
	},
	//日期选择框
	getdatefieldCom: function() {
		var me = this;
		var com = me.getComponent('datefieldCom');
		return com;
	},
	//日期选择框值(二)
	getdatefieldComTwoValue: function() {
		var me = this;
		var com = me.getComponent('datefieldComTwo');
		var value = com.getValue();
		return value;
	},
	//日期选择框(二)
	getdatefieldComTwo: function() {
		var me = this;
		var com = me.getComponent('datefieldComTwo');
		return com;
	},
	//宏结果选择框值
	getcbomacrosListValue: function() {
		var me = this;
		var com = me.getComponent('cbomacrosList');
		var value = com.getValue();
		return value;
	},
	//宏结果选择框
	getcbomacrosList: function() {
		var me = this;
		var com = me.getComponent('cbomacrosList');
		return com;
	},
	//宏结果二选择框值
	getcbomacrosListTwoValue: function() {
		var me = this;
		var com = me.getComponent('cbomacrosListTwo');
		var value = com.getValue();
		return value;
	},
	//宏结果二选择框
	getcbomacrosListTwo: function() {
		var me = this;
		var com = me.getComponent('cbomacrosListTwo');
		return com;
	},
	//数据过滤条件中文输入框值
	getrowFilterCNameValue: function() {
		var me = this;
		var com = me.getComponent('rowFilterCName');
		var value = com.getValue();
		return value;
	},
	//数据过滤条件输入框
	getrowFilterCName: function() {
		var me = this;
		var com = me.getComponent('rowFilterCName');
		return com;
	},
	//默认条件复选框值
	getdefaultConditionValue: function() {
		var me = this;
		var com = me.getComponent('defaultCondition');
		var value = com.getValue();
		return value;
	},
	//默认条件复选框
	getdefaultCondition: function() {
		var me = this;
		var com = me.getComponent('defaultCondition');
		return com;
	},
	//默认条件复选框
	setdefaultCondition: function(value) {
		var me = this;
		var com = me.getComponent('defaultCondition');
		if(com) {
			com.setValue(value);
		}
	},
	//关联类型选择器显示字段
	getContrastRelation: function() {
		var me = this;
		var com = me.getComponent('ContrastRelation');
		return com;
	},
	//关联类型选择器显示字段
	getContrastRelationValue: function() {
		var me = this;
		var com = me.getComponent('ContrastRelation');
		var value = com.getSubmitValue();
		return value;
	},
	//关联类型选择按钮
	getbtnRelationCom: function() {
		var me = this;
		var com = me.getComponent('btnRelationCom');
		return com;
	},
	//返回选中的所有节点
	getChecked: function() {
		var me = this;
		var com = me.getComponent('InteractionField');
		var arr = com.getChecked();
		return arr;
	},
	//交互字段值 获得当前选中的提交值
	getInteractionFieldValue: function() {
		var me = this;
		var com = me.getComponent('InteractionField');
		var value = com.getSubmitValue();
		return value;
	},
	//交互字段值 获得当前选中的显示值
	getDisplayValue: function() {
		var me = this;
		var com = me.getComponent('InteractionField');
		var value = com.getDisplayValue();
		return value;
	},
	//交互字段
	getInteractionField: function() {
		var me = this;
		var com = me.getComponent('InteractionField');
		return com;
	},
	//对比属性交互字段值 获得当前选中的提交值
	getContrastInteractionFieldValue: function() {
		var me = this;
		var value = me.getComponent('ContrastInteractionField').getSubmitValue();
		return value;
	},
	//对比属性交互字段值 获得当前选中的显示值
	getcContrastInteractionFieldDisplayValue: function() {
		var me = this;
		var value = me.getComponent('ContrastInteractionField').getDisplayValue();
		return value;
	},
	//对比属性交互字段
	getContrastInteractionField: function() {
		var me = this;
		var com = me.getComponent('ContrastInteractionField');
		return com;
	},
	//列属性值类型值
	getColumnTypeValue: function() {
		var me = this;
		var com = me.getComponent('ColumnType');
		var value = com.getValue();
		return value;
	},
	//列属性值类型
	getColumnType: function() {
		var me = this;
		var com = me.getComponent('ColumnType');
		return com;
	},
	//运算关系下拉列表值
	getoperationTypeValue: function() {
		var me = this;
		var com = me.getComponent('operationType');
		var value = com.getValue();
		return value;
	},
	//运算关系下拉列表显示值
	getoperationNameValue: function() {
		var me = this;
		var com = me.getComponent('operationType');
		var value = com.getRawValue();
		return value;
	},
	//运算关系下拉列表
	getoperationType: function() {
		var me = this;
		var com = me.getComponent('operationType');
		return com;
	},
	//结果录入值:id值
	gettxtResultHiddenValue: function() {
		var me = this;
		var com = me.getComponent('txtResultHidden');
		var value = com.getValue();
		return value;
	},
	//结果录入组件:id值
	gettxtResultHidden: function() {
		var me = this;
		var com = me.getComponent('txtResultHidden');
		return com;
	},
	setFormValue: function() {
		var me = this;
		var txtResultHidden = me.gettxtResultHidden();
		txtResultHidden.setValue('');
		var cbomacrosList = me.getcbomacrosList();
		cbomacrosList.setValue('');
		var cbomacrosListTwo = me.getcbomacrosListTwo();
		cbomacrosListTwo.setValue('');
		var datefieldComTwo = me.getdatefieldComTwo();
		datefieldComTwo.setValue('');
		var datefieldCom = me.getdatefieldCom();
		datefieldCom.setValue('');
		var txtStringTwo = me.gettxtStringTwo();
		txtStringTwo.setValue('');
		var txtString = me.gettxtString();
		txtString.setValue('');
		var txtNumberfield = me.gettxtNumberfield();
		txtNumberfield.setValue('');
		var txtNumberfieldTwo = me.gettxtNumberfieldTwo();
		txtNumberfieldTwo.setValue('');
		me.getContrastInteractionField().setValue('');
	},
	//number关系运算关系
	numberOperationType: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': '>',
			'text': '大于'
		},
		{
			'value': '>=',
			'text': '大于等于'
		},
		{
			'value': '<',
			'text': '小于'
		},
		{
			'value': '<=',
			'text': '小于等于'
		},
		{
			'value': '>= and <=',
			'text': '区间(包含边界)(输入2项)'
		},
		{
			'value': '<= or >=',
			'text': '区间外(包含边界)(输入2项)'
		},
		{
			'value': '> and <',
			'text': '区间(不包含边界)(输入2项)'
		},
		{
			'value': '< or >',
			'text': '区间外(不包含边界)(输入2项)'
		}
	],
	//字符串关系运算关系
	stringOperationType: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': 'like A%',
			'text': '开始于(A*)'
		},
		{
			'value': 'like %A',
			'text': '结束于(*A)'
		},
		{
			'value': 'like A%B',
			'text': '字符之间(A*B) (输入2项)'
		},
		{
			'value': '= or = or =',
			'text': '等于其中一项(输入多项)'
		},
		{
			'value': 'not (= or = or =)',
			'text': '不等于任何一项(输入多项)'
		},
		{
			'value': 'like %A% or like %B%',
			'text': '包含(可输入多个)'
		},
		{
			'value': 'not (like %A% or like %B%)',
			'text': '不包含(可输入多个)'
		}
	],
	//日期时间关系运算关系
	dateOperationType: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': '>',
			'text': '大于'
		},
		{
			'value': '>=',
			'text': '大于等于'
		},
		{
			'value': '<',
			'text': '小于'
		},
		{
			'value': '<=',
			'text': '小于等于'
		},
		{
			'value': '>= and <=',
			'text': '区间(包含边界)(输入2项)'
		},
		{
			'value': '<= or >=',
			'text': '区间外(包含边界)(输入2项)'
		},
		{
			'value': '> and <',
			'text': '区间(不包含边界)(输入2项)'
		},
		{
			'value': '< or >',
			'text': '区间外(不包含边界)(输入2项)'
		}
	],
	//关联关系运算关系
	relationOperationType: [{
			'value': 'in',
			'text': '包含'
		},
		{
			'value': 'not in',
			'text': '不包含'
		}
	],
	//布尔勾选关系运算关系
	booleanOperationType: [{
		'value': '=',
		'text': '等于'
	}],
	/**
	 * macros关系运算关系
	 * 宏的区间关系通过新增两行的关系建立
	 * @type 
	 */
	macrosOperationType: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '>',
			'text': '大于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': '>=',
			'text': '大于等于'
		},
		{
			'value': '<',
			'text': '小于'
		},
		{
			'value': '<=',
			'text': '小于等于'
		},
		{
			'value': '>= and <=',
			'text': '区间(包含边界)(输入2项)'
		},
		{
			'value': '<= or >=',
			'text': '区间外(包含边界)(输入2项)'
		},
		{
			'value': '> and <',
			'text': '区间(不包含边界)(输入2项)'
		},
		{
			'value': '< or >',
			'text': '区间外(不包含边界)(输入2项)'
		}
	],
	//对比属性关系运算关系
	contrastOperationType: [{
			'value': '=',
			'text': '等于'
		},
		{
			'value': '<>',
			'text': '不等于'
		},
		{
			'value': '>',
			'text': '大于'
		},
		{
			'value': '>=',
			'text': '大于等于'
		},
		{
			'value': '<',
			'text': '小于'
		},
		{
			'value': '<=',
			'text': '小于等于'
		}
	],
	//列属性类型
	columnTypeList: [{
			'value': 'date',
			'text': '日期型'
		},
		{
			'value': 'boolean',
			'text': '布尔勾选'
		},
		{
			'value': 'number',
			'text': '数值型'
		},
		{
			'value': 'string',
			'text': '字符串'
		},
		{
			'value': 'macros',
			'text': '宏命令'
		},
		{
			'value': 'relation',
			'text': '关联'
		},
		{
			'value': 'contrast',
			'text': '对比属性'
		}
	],
	//日期列属性类型
	dateTypeList: [{
			'value': 'date',
			'text': '日期型'
		},
		{
			'value': 'macros',
			'text': '宏命令'
		},
		{
			'value': 'relation',
			'text': '关联'
		},
		{
			'value': 'contrast',
			'text': '对比属性'
		}
	],
	//布尔勾选列属性类型
	booleanTypeList: [{
			'value': 'boolean',
			'text': '布尔勾选'
		},
		{
			'value': 'macros',
			'text': '宏命令'
		},
		{
			'value': 'relation',
			'text': '关联'
		},
		{
			'value': 'contrast',
			'text': '对比属性'
		}
	],
	//数值型列属性类型
	numberTypeList: [{
			'value': 'number',
			'text': '数值型'
		},
		{
			'value': 'macros',
			'text': '宏命令'
		},
		{
			'value': 'relation',
			'text': '关联'
		},
		{
			'value': 'contrast',
			'text': '对比属性'
		}
	],
	//字符串列属性类型
	stringTypeList: [{
			'value': 'string',
			'text': '字符串'
		},
		{
			'value': 'macros',
			'text': '宏命令'
		},
		{
			'value': 'relation',
			'text': '关联'
		},
		{
			'value': 'contrast',
			'text': '对比属性'
		}
	],
	//打开复选组选择器
	openAppShowWin: function(type) {
		var me = this;
		var com = me.gettxtResultHidden();
		com.setValue('');
		var maxHeight = document.body.clientHeight * 0.98;
		var maxWidth = document.body.clientWidth * 0.98;

		var config = {
			id: -1,
			type: type,
			maxWidth: maxWidth,
			height: maxHeight * 0.98,
			width: maxWidth * 0.98,
			serverUrl: me.selectorServerUrl,
			checkboxgroupName: 'checkboxgroupName', //复选组组名称
			iKeyField: me.iKeyField, //keyField数据项匹配字段,
			iTextField: me.iTextField, //textField数据项匹配字段
			listeners: {
				saveClick: function(win) {
					var values = "";
					if(type === 'CName') {
						values = "" + win.getDisplayValue();
					} else {
						values = "" + win.getSubmitValue();
					}
					com.setValue(values);
					win.close();
				},
				comeBackClick: function(win) {
					win.close();
				}
			}
		};
		if(config.height > maxHeight) {
			config.height = maxHeight;
		}
		JShell.Win.open('Shell.class.sysbase.rowfilter.basic.CheckGroupSelector', config).show();
	},
	//获取宏命令数据集
	createBTDMacroCommandStore: function(typeCode) {
		var me = this;
		var w = '';
		var myUrl = me.getSearchBTDMacroCommandByHQLUrl + '?isPlanish=true&fields=' + me.selectMacroCommandFields;
		if(typeCode != '') {
			w = '&where=btdmacrocommand.TypeCode=' + typeCode;
			myUrl = myUrl + w;
		}
		var localData = [];
		Ext.Ajax.request({
			async: false,
			timeout: 6000,
			url: myUrl,
			method: 'GET',
			success: function(response, opts) {
				var data = JShell.JSON.decode(response.responseText);
				if(data.success) {
					var count = 0;
					var list = [];
					if(data.ResultDataValue && data.ResultDataValue != '') {
						var ResultDataValue = JShell.JSON.decode(data.ResultDataValue);
						count = ResultDataValue.count;
						list = ResultDataValue.list;
					} else {
						list = [];
						count = 0;
					}
					for(var i = 0; i < list.length; i++) {
						var macroCode = list[i]['BTDMacroCommand_MacroCode'];
						var cName = list[i]['BTDMacroCommand_CName'];
						var typeCode = list[i]['BTDMacroCommand_TypeCode'];
						if(typeCode != 'IntDynamic') {
							if(macroCode == "$AddDay:+0,P1$" || macroCode == "$AddMonth:+0,P1$") { //过滤为日期追加天数或月数

							} else {
								//过滤数字类型
								var obj = {
									text: cName,
									value: macroCode
								};
								localData.push(obj);
							}
						}
					}
				} else {
					JcallShell.Msg.alert('获取信息失败！', null, 1000);
				}
			}
		});
		return localData;
	}
});