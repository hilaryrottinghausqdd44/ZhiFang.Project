/***
 * 设置数据过滤条件表单
 */
Ext.Loader.setConfig({
	enabled: true
});
Ext.Loader.setPath('Ext.manage.datafilters.ComboBoxTree', getRootPath() + '/ui/manage/datafilters/ComboBoxTree.js');
Ext.ns('Ext.manage');
Ext.define('Ext.manage.datafilters.setForm', {
	extend: 'Ext.form.Panel',
	alias: 'widget.setForm',
	title: '',
	isLoadingComplete: false,
	bodyPadding: 2,
	header: false,
	labelAlign: 'right',
	/***
	 * 外部传入
	 * 模块操作id
	 * @type String
	 */
	moduleOperId: '',
	/***
	 * 外部传入
	 * 模块操作列表选中行
	 * @type String
	 */
	moduleOperSelect: '',
	/***
	 * 是否在节点展开后处理其他事情
	 * @type Boolean
	 */
	isbeforeload: true,
	/***
	 * 是否在节点展开后处理其他事情
	 * @type Boolean
	 */
	beforeitemexpand: true,
	/***
	 * 是否隐藏预定义属性按钮
	 * @type Boolean
	 */
	isShowPredefinedAttributes: false,
	/***
	 * 下拉列表框树的所需的数据对象名称
	 * @type String
	 */
	objectName: '',
	/***
	 * 下拉列表框树的所需的数据对象中文名
	 * @type String
	 */
	objectCName: '数据对象',
	isSuccessMsg: true,

	/***
	 * 复选组选择器查询数据服务
	 * @type String
	 */
	selectorServerUrl: '',
	/***
	 *  复选组选择器keyField数据项匹配字段
	 * @type String
	 */
	iKeyField: 'inputValue',
	/***
	 *  复选组选择器textField数据项匹配字段
	 * @type String
	 */
	iTextField: 'boxLabel',
	autoScroll: true,
	layout: 'absolute',
	itemid: 'setForm',
	beforeRender: function() {
		var me = this;
		me.callParent(arguments);
		if(!(me.header === false)) {
			me.updateHeader();
		}
	},

	childrenField: 'Tree',

	fields: ['id', 'text', 'expanded', 'leaf', 'FieldClass', 'tid', 'value', 'InteractionField', 'ParentCName', 'ParentEName'],

	/**
	 * 获取数据对象内容的服务地址
	 * @type 
	 */
	objectPropertyUrl: getRootPath() + '/ConstructionService.svc/CS_BA_GetEntityFrameTree',
	/**
	 * 获取数据对象内容时后台接收的参数名称
	 * @type String
	 */
	objectPropertyParam: 'EntityName',
	/***
	 * 获取数据集
	 */
	getServerLists: function(url, hqlWhere, async) {
		var arrLists = [];
		var myUrl = "";
		if(hqlWhere && hqlWhere != null) {
			myUrl = url + '&where=' + encodeString(hqlWhere);;
		} else {
			myUrl = url;
		}
		//查询数据过滤条件行记录
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			async: async, //非异步
			url: myUrl,
			method: 'GET',
			success: function(response, opts) {
				var data = Ext.JSON.decode(response.responseText);
				var success = (data.success + "" == "true" ? true : false);
				if(!success) {
					alert(data.ErrorInfo);
				}
				if(success) {
					if(data.ResultDataValue && data.ResultDataValue != '') {
						data.ResultDataValue = data.ResultDataValue.replace(/[\r\n]+/g, "");
						var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
						arrLists = ResultDataValue.list; //ResultDataValue.list
					} else {
						arrLists = [];
					}
				}
			},
			failure: function(response, options) {
				arrLists = [];
			}
		});
		return arrLists;
	},
	initComponent: function() {
		var me = this;
		Ext.Loader.setConfig({
			enabled: true
		});
		Ext.Loader.setPath('Ext.manage.datafilters.ComboBoxTree', getRootPath() + '/ui/manage/class/datafilters/ComboBoxTree.js');

		me.addEvents('addRecordClick');
		me.addEvents('addOrClick');
		me.addEvents('addAllClick');
		me.addEvents('assignRolesClick');
		me.addEvents('btnSelect');
		var url = "";
		me.isbeforeload = true;
		me.beforeitemexpand = true;
		if(me.moduleOperId && me.moduleOperId != "" && me.moduleOperId != null) {
			url = getRootPath() + "/ConstructionService.svc/CS_BA_GetEntityFrameTreeByModuleOperID?ModuleOperID=" + me.moduleOperId + "&EntityName=" + me.objectName;
		} else { //原来
			url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + me.objectName;
		}

		var treeStore = new Ext.data.TreeStore({
			fields: me.fields,
			proxy: {
				type: 'ajax',
				url: url,
				extractResponseData: function(response) {
					var data = Ext.JSON.decode(response.responseText);
					if(data.ResultDataValue && data.ResultDataValue != "") {
						var arr = Ext.JSON.decode(data.ResultDataValue);
						//过滤时间戳
						var children = [];
						var treeCom = me.getcontrastRelationTree();
						if(arr) {
							for(var i in arr) {
								var obj = arr[i];
								var arrTemp = obj['InteractionField'].split('_');
								if(arrTemp[arrTemp.length - 1] != 'DataTimeStamp') {
									children.push(arr[i]);
								}
							}
						} else {
							children = arr;
						}
						if(treeCom.nodeClassName != "") {
							data['Tree'] = children;
						} else { //根节点
							data['Tree'] = children;
						}
					}
					response.responseText = Ext.JSON.encode(data);
					return response;
				}
			},
			defaultRootProperty: 'Tree',
			autoLoad: false
		});

		var contrastRelationTree = Ext.create('Ext.manage.datafilters.ComboBoxTree', {
			//关联类型的选择器的显示字段的下拉框树
			name: 'contrastRelationTree',
			itemId: 'contrastRelationTree',
			hidden: true,
			x: 382,
			y: 98,
			rootVisible: false,
			nodeClassName: '',
			ParentCName: '',
			ParentEName: '',
			CName: '',
			objectName: me.objectName,
			/***
			 * 是否在节点展开后处理其他事情
			 * @type Boolean
			 */
			isbeforeload: me.isbeforeload,
			/***
			 * 是否在节点展开后处理其他事情
			 * @type Boolean
			 */
			beforeitemexpand: me.beforeitemexpand,
			ClassName: "",
			fieldLabel: '显示字段',
			labelWidth: 60,
			enableKeyEvents: true,
			forceSelection: true,
			width: 360,
			hasReadOnly: false,
			labelAlign: me.labelAlign,
			multiCascade: false,
			multiSelect: false, //是否多选
			columns: [{
					xtype: 'treecolumn',
					text: '名称',
					width: 260,
					sortable: false,
					dataIndex: 'text'
				},
				{
					text: 'tid',
					hidden: true,
					width: 10,
					sortable: true,
					dataIndex: 'tid'
				}, {
					text: '字段类型',
					hidden: true,
					width: 80,
					sortable: true,
					dataIndex: 'FieldClass'
				}
			],
			store: treeStore
		});

		var interactionField = Ext.create('Ext.manage.datafilters.ComboBoxTree', {
			//交互字段
			name: 'interactionField',
			itemId: 'interactionField',
			x: 10,
			y: 62,
			rootVisible: false,
			nodeClassName: '',
			ParentCName: '',
			ParentEName: '',
			CName: '',
			objectName: me.objectName,
			/***
			 * 是否在节点展开后处理其他事情
			 * @type Boolean
			 */
			isbeforeload: me.isbeforeload,
			/***
			 * 是否在节点展开后处理其他事情
			 * @type Boolean
			 */
			beforeitemexpand: me.beforeitemexpand,
			ClassName: "",
			fieldLabel: '属性',
			labelWidth: 60,
			editable: false,
			forceSelection: true,
			queryMode: 'local',
			width: 360,
			hasReadOnly: false,
			labelAlign: me.labelAlign,
			multiCascade: false,
			multiSelect: false, //是否多选
			columns: [{
					xtype: 'treecolumn',
					text: '名称',
					width: 260,
					sortable: false,
					dataIndex: 'text'
				},
				{
					text: 'tid',
					hidden: true,
					width: 10,
					sortable: true,
					dataIndex: 'tid'
				}, {
					text: '字段类型',
					hidden: true,
					width: 80,
					sortable: true,
					dataIndex: 'FieldClass'
				}
			],
			store: treeStore,
			listeners: {
				itemclick: function(self, record, e, object) {
					var leaf = record.get('leaf');
					var type = '' + record.get('FieldClass');
					if(leaf == false) {
						//如果是父节点,需要动态加载该父节点的数据对象的字段属性数据
						this.nodeClassName = '' + type;

					} else if(leaf == true) {
						var fieldValue = record.get('InteractionField');
						var fieldCname = record.get('text');
						var arrCName = fieldCname.split('.');
						var arr = [];
						var lastName = '';
						if(fieldValue != '') {
							arr = fieldValue.split('_');
							lastName = '' + arr[arr.length - 1];
						}
						var oTypeCom = me.getoperationType();
						var cTypeList = me.getcolumnTypeList();
						oTypeCom.setValue('');

						var tempCName = '' + arrCName[arrCName.length - 1];
						if((lastName == 'Name' && arr.length > 2) || (lastName == 'CName' && arr.length > 2) || (tempCName == '名称' && arrCName.length > 2)) {

							//关联(子对象的中文名称也关联)
							cTypeList.store.loadData(me.stringTypeList);
							oTypeCom.store.loadData(me.relationOperationType);
							cTypeList.setValue('relation');
							oTypeCom.setValue('in');
						}
						if((lastName == 'CNAME' && arr.length > 2) || (tempCName == '名称' && arrCName.length > 2)) {

							//关联(子对象的中文名称也关联)
							cTypeList.store.loadData(me.stringTypeList);
							oTypeCom.store.loadData(me.relationOperationType);
							cTypeList.setValue('relation');
							oTypeCom.setValue('in');
						} else if(type == "date" || type == 'Nullable`1') { //日期型
							cTypeList.store.loadData(me.dateTypeList);
							oTypeCom.store.loadData(me.dateOperationType);
							cTypeList.setValue('date');
							oTypeCom.setValue('=');
						} else if((type == "number" || type == 'Int64' || type == 'Int32') && (lastName != 'Id')) { //数值型
							cTypeList.store.loadData(me.numberTypeList);
							oTypeCom.store.loadData(me.numberOperationType);
							cTypeList.setValue('number');
							oTypeCom.setValue('=');
						} else if(lastName == 'Id') {
							//关联(子对象的中文名称也关联)
							cTypeList.store.loadData(me.stringTypeList);
							oTypeCom.store.loadData(me.relationOperationType);
							cTypeList.setValue('relation');
							oTypeCom.setValue('in');
						} else if(type == "boolean" || type == 'Boolean') { //布尔勾选
							cTypeList.store.loadData(me.booleanTypeList);
							oTypeCom.store.loadData(me.booleanOperationType);
							cTypeList.setValue('boolean');
							oTypeCom.setValue('=');
						} else { //宏
							cTypeList.store.loadData(me.stringTypeList);
							oTypeCom.store.loadData(me.stringOperationType);
							cTypeList.setValue('string');
							oTypeCom.setValue('=');
						}
					}
				}
			}

		});

		me.items = [{
				xtype: 'textfield',
				name: 'rowFilterCName',
				fieldLabel: '名称',
				labelWidth: 60,
				width: 360,
				height: 22,
				itemId: 'rowFilterCName',
				x: 10,
				y: 27,
				labelAlign: me.labelAlign
			}, {
				xtype: 'checkboxfield',
				itemId: 'defaultCondition',
				x: 382,
				y: 27,
				name: 'defaultCondition',
				fieldLabel: '默认条件',
				labelWidth: 60,
				width: 160,
				height: 22,
				boxLabel: '',
				inputValue: 'true',
				uncheckedValue: 'false',
				labelAlign: me.labelAlign
			},
			interactionField,
			{ //运算关系
				xtype: 'combobox',
				name: 'operationType',
				fieldLabel: '关系',
				labelWidth: 60,
				width: 360,
				height: 22,
				itemId: 'operationType',
				x: 10,
				y: 98,
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
					data: me.stringOperationType
				}),
				listeners: {
					select: function(com, records, eOpts) {

						var newValue = me.getcolumnTypeListValue();
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
						var contrastRelationTree = me.getcontrastRelationTree();

						var otype = me.getoperationTypeValue();
						var numberfieldTwo = me.gettxtNumberfieldTwo();
						var txtStringTwo = me.gettxtStringTwo();
						var datefieldComTwo = me.getdatefieldComTwo();
						datefieldComTwo.setVisible(false);
						numberfieldTwo.setVisible(false);
						txtStringTwo.setVisible(false);
						cbomacrosListTwo.setVisible(false);

						contrastRelationTree.setVisible(false);
						btnRelationCom.setVisible(false);

						if(newValue === "macros") { //宏命令
							cbomacrosList.setVisible(true);
							var data = me.createBTDMacroCommandStore('');
							cbomacrosList.store.loadData(data);
							if(otype == '>= and <=') { //区间(包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							}
						} else if(newValue === "date") { //日期型
							datefieldCom.setVisible(true);
							if(otype == '>= and <=') { //区间(包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							}

						} else if(newValue === "boolean") { //布尔勾选
							booleanCom.setVisible(true);
						} else if(newValue === "relation") { //关联
							btnRelationCom.setVisible(true);
							contrastRelationTree.setVisible(true);
						} else if(newValue === "string") { //字符串
							txtString.setVisible(true);
							if(otype == 'like A%B') { //字符之间(A*B) (输入2项)
								txtStringTwo.setVisible(true);
							}
						} else if(newValue === "number") { //数值型
							numberfield.setVisible(true);
							if(otype == '>= and <=') { //区间(包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							}
						}
					},
					change: function(com, newValue, oldValue, e, eOpts) {}
				}
			}, { //列属性类型
				xtype: 'combobox',
				name: 'columnTypeList',
				fieldLabel: '值类型',
				labelWidth: 60,
				width: 240,
				height: 22,
				hidden: false,
				itemId: 'columnTypeList',
				x: 382,
				y: 62,
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
					select: function(com, records, eOpts) {},
					change: function(com, newValue, oldValue, e, eOpts) {
						var newValue = com.getValue();
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
						var contrastRelationTree = me.getcontrastRelationTree();
						//对比属性树
						var contrastTree = me.getcontrastTree();

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
						contrastRelationTree.setVisible(false);
						contrastTree.setVisible(false);

						if(newValue === "macros") { //宏命令macrosOperationType
							cbomacrosList.setVisible(true);
							var data = me.createBTDMacroCommandStore('');
							cbomacrosList.store.loadData(data);
							oTypeCom.store.loadData(me.macrosOperationType);
							if(otype == '>= and <=') { //区间(包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
								cbomacrosListTwo.setVisible(true);
								cbomacrosListTwo.store.loadData(data);
							}

						} else if(newValue === "date") { //日期型
							oTypeCom.store.loadData(me.dateOperationType);
							datefieldCom.setVisible(true);
							if(otype == '>= and <=') { //区间(包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
								datefieldComTwo.setVisible(true);
							}

						} else if(newValue === "boolean") { //布尔勾选
							booleanCom.setVisible(true);
						} else if(newValue === "relation") { //关联
							oTypeCom.store.loadData(me.relationOperationType);
							btnRelationCom.setVisible(true);
							contrastRelationTree.setVisible(true);
						} else if(newValue === "string") { //字符串
							oTypeCom.store.loadData(me.stringOperationType);
							txtString.setVisible(true);
							if(otype == 'like A%B') { //字符之间(A*B) (输入2项)
								txtStringTwo.setVisible(true);
							}
						} else if(newValue === "number") { //数值型
							oTypeCom.store.loadData(me.numberOperationType);
							numberfield.setVisible(true);
							if(otype == '>= and <=') { //区间(包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
								numberfieldTwo.setVisible(true);
							}
						} else if(newValue === "contrast") { //对比属性型
							contrastTree.setVisible(true);
							oTypeCom.store.loadData(me.contrastOperationType);
						}

					}
				}

			}, { //宏下列框
				xtype: 'combobox',
				name: 'cbomacrosList',
				itemId: 'cbomacrosList',
				labelAlign: me.labelAlign,
				fieldLabel: '宏',
				labelWidth: 60,
				width: 240,
				height: 22,
				hidden: true,
				x: 382,
				y: 98,
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
			},
			{ //宏下列框二
				xtype: 'combobox',
				name: 'cbomacrosListTwo',
				itemId: 'cbomacrosListTwo',
				labelAlign: me.labelAlign,
				fieldLabel: '宏选择二',
				labelWidth: 60,
				width: 240,
				height: 22,
				hidden: true,
				x: 638,
				y: 98,
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
			},
			{ //日期值录入框
				xtype: 'datefield',
				name: 'datefieldCom',
				itemId: 'datefieldCom',
				fieldLabel: '日期值',
				labelWidth: 60,
				labelAlign: me.labelAlign,
				width: 180,
				height: 22,
				value: new Date(),
				format: 'Y-m-d',
				editable: true,
				hidden: true,
				x: 382,
				y: 98
			},
			{ //日期值第二录入框
				xtype: 'datefield',
				name: 'datefieldComTwo',
				itemId: 'datefieldComTwo',
				fieldLabel: '日期二',
				labelWidth: 60,
				labelAlign: me.labelAlign,
				width: 180,
				height: 22,
				value: new Date(),
				format: 'Y-m-d',
				editable: true,
				hidden: true,
				x: 572,
				y: 98
			},
			{ //布尔勾选值选择框
				xtype: 'checkboxfield',
				itemId: 'booleanCom',
				name: 'booleanCom',
				hidden: true,
				x: 382,
				y: 98,
				fieldLabel: '布尔值',
				labelWidth: 60,
				width: 160,
				height: 22,
				boxLabel: '',
				inputValue: 'true',
				uncheckedValue: 'false',
				labelAlign: 'right'
			},
			{ //数值型结果录入值
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
				y: 98,
				labelAlign: me.labelAlign
			},
			{ //数值型结果录入值二
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
				y: 98,
				labelAlign: me.labelAlign
			},
			{ //字符型结果录入值
				xtype: 'textfield',
				hidden: true,
				width: 240,
				emptyText: '输入多项时请用英文逗号分隔',
				name: 'txtString',
				itemId: 'txtString',
				fieldLabel: '结果值',
				labelWidth: 60,
				x: 382,
				y: 98,
				labelAlign: me.labelAlign
			},
			{ //字符型结果录入值二
				xtype: 'textfield',
				hidden: true,
				width: 240,
				name: 'txtStringTwo',
				itemId: 'txtStringTwo',
				fieldLabel: '结果二',
				labelWidth: 60,
				x: 608,
				y: 98,
				labelAlign: me.labelAlign
			},
			contrastRelationTree,
			{
				//关联类型的值选择
				xtype: 'button',
				text: '请选择',
				hidden: true,
				itemId: 'btnRelationCom',
				name: 'btnRelationCom',
				x: 762,
				y: 98,
				width: 100,
				listeners: {
					click: function(com, e, eOpts) {
						var fieldValue = me.getinteractionFieldValue();
						var arr = fieldValue.split('_');
						var fieldCname = me.getDisplayValue();
						var arrCName = fieldCname.split('.');
						//选择器的显示字段中文名称
						var cnameShow = me.getcontrastRelationTreeValue();
						var type = 'Id';
						if(fieldValue != '') {
							if(cnameShow == null || cnameShow == "") {
								Ext.Msg.alert('提示', '请先选择选择器的显示字段');
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
									var myUrl = getRootPath() + '/SingleTableService.svc/ST_UDTO_Search' + objectName + 'ByHQL';
									myUrl = myUrl + params + '&fields=' + fields;
									me.selectorServerUrl = myUrl;
									me.iKeyField = id;
									me.iTextField = cname;
									me.openAppShowWin(type);
								}
							}
						} else {
							Ext.Msg.alert('提示', '关联类型的属性选择只能选择主键或中文名称');
						}
					}
				}
			},
			{
				//关联结果录入的id值
				xtype: 'hiddenfield',
				name: 'txtResultHidden',
				itemId: 'txtResultHidden'
			},
			{
				//对比属性交互字段下拉框树
				xtype: 'comboboxtree',
				name: 'contrastTree',
				itemId: 'contrastTree',
				hidden: true,
				x: 382,
				y: 98,
				rootVisible: false,
				/***
				 * 是否在节点展开后处理其他事情
				 * @type Boolean
				 */
				isbeforeload: me.isbeforeload,
				/***
				 * 是否在节点展开后处理其他事情
				 * @type Boolean
				 */
				beforeitemexpand: me.beforeitemexpand,
				nodeClassName: '',
				ParentCName: '',
				ParentEName: '',
				CName: '',
				ClassName: '',
				fieldLabel: '属性二',
				labelWidth: 60,
				editable: false,
				width: 360,
				hasReadOnly: false,
				labelAlign: me.labelAlign,
				multiCascade: false,
				multiSelect: false, //是否多选
				columns: [{
						xtype: 'treecolumn',
						text: '名称',
						width: 260,
						sortable: false,
						dataIndex: 'text'
					},
					{
						text: 'tid',
						hidden: true,
						width: 10,
						sortable: true,
						dataIndex: 'tid'
					}, {
						text: '字段类型',
						hidden: true,
						width: 80,
						sortable: true,
						dataIndex: 'FieldClass'
					}
				],
				store: treeStore,
				listeners: {
					itemclick: function(self, record, e, object) {}
				}
			},

			{
				xtype: 'button',
				text: '增加',
				name: 'btnAddRecord',
				itemId: 'btnAddRecord',
				x: 78,
				y: 132,
				width: 100,
				height: 22,
				listeners: {
					click: function(com, e, eOpts) {
						me.fireEvent('addRecordClick');
					}
				}
			}, {
				xtype: 'button',
				text: '或关系',
				itemId: 'btnaddOr',
				name: 'btnaddOr',
				x: 200,
				y: 132,
				width: 100,
				height: 22,
				listeners: {
					click: function(com, e, eOpts) {
						me.fireEvent('addOrClick');
					}
				}
			}, {
				xtype: 'button',
				text: '查看全部数据',
				itemId: 'btnAddAll',
				x: 330,
				y: 132,
				width: 110,
				height: 22,
				listeners: {
					click: function(com, e, eOpts) {
						me.fireEvent('addAllClick');
					}
				}
			}, {
				xtype: 'button',
				text: '查看数据条件',
				itemId: 'btnSelect',
				x: 468,
				y: 132,
				width: 110,
				height: 22,
				listeners: {
					click: function(com, e, eOpts) {
						me.fireEvent('btnSelect');
					}
				}
			}, {
				xtype: 'button',
				text: '预定义可选属性',
				itemId: 'btnPredefinedAttributes',
				name: 'btnPredefinedAttributes',
				hidden: true, //me.isShowPredefinedAttributes,
				x: 800,
				y: 10,
				width: 160,
				height: 22,
				listeners: {
					click: function(com, e, eOpts) {
						if(me.objectName == "" && me.objectName == null) {
							alert("获取不到当前的模块操作的数据对象信息");
						}
						me.fireEvent('predefinedAttributesClick', com, e, me.objectName);
					}
				}
			}, {
				xtype: 'button',
				text: '分配角色',
				itemId: 'btnAssignRoles',
				x: 860,
				y: 132,
				width: 100,
				height: 22,
				listeners: {
					click: function(com, e, eOpts) {
						me.fireEvent('assignRolesClick');
					}
				}
			}
		];
		me.callParent(arguments);
	},
	/***
	 * 数值型结果录入值二
	 * @return {}
	 */
	gettxtNumberfieldTwoValue: function() {
		var me = this;
		var com = me.getComponent('txtNumberfieldTwo');
		var value = com.getValue();
		return value;
	},
	/***
	 * 数值型结果录入框二
	 * @return {}
	 */
	gettxtNumberfieldTwo: function() {
		var me = this;
		var com = me.getComponent('txtNumberfieldTwo');
		return com;
	},
	/***
	 * 数值型结果录入值
	 * @return {}
	 */
	gettxtNumberfieldValue: function() {
		var me = this;
		var com = me.getComponent('txtNumberfield');
		var value = com.getValue();
		return value;
	},
	/***
	 * 数值型结果录入框
	 * @return {}
	 */
	gettxtNumberfield: function() {
		var me = this;
		var com = me.getComponent('txtNumberfield');
		return com;
	},
	/***
	 * 字符结果录入值
	 * @return {}
	 */
	gettxtStringValue: function() {
		var me = this;
		var com = me.getComponent('txtString');
		var value = com.getValue();
		return value;
	},
	/***
	 * 字符结果录入框
	 * @return {}
	 */
	gettxtString: function() {
		var me = this;
		var com = me.getComponent('txtString');
		return com;
	},
	/***
	 * 字符结果录入值二
	 * @return {}
	 */
	gettxtStringTwoValue: function() {
		var me = this;
		var com = me.getComponent('txtStringTwo');
		var value = com.getValue();
		return value;
	},
	/***
	 * 字符结果录入框二
	 * @return {}
	 */
	gettxtStringTwo: function() {
		var me = this;
		var com = me.getComponent('txtStringTwo');
		return com;
	},
	/***
	 * 布尔勾选值
	 * @return {}
	 */
	getbooleanComValue: function() {
		var me = this;
		var com = me.getComponent('booleanCom');
		var value = com.getValue();
		return value;
	},
	/***
	 * 布尔勾选框
	 * @return {}
	 */
	getbooleanCom: function() {
		var me = this;
		var com = me.getComponent('booleanCom');
		return com;
	},
	/***
	 * 日期选择框值
	 * @return {}
	 */
	getdatefieldComValue: function() {
		var me = this;
		var com = me.getComponent('datefieldCom');
		var value = com.getValue();
		return value;
	},
	/***
	 * 日期选择框
	 * @return {}
	 */
	getdatefieldCom: function() {
		var me = this;
		var com = me.getComponent('datefieldCom');
		return com;
	},
	/***
	 * 日期选择框值(二)
	 * @return {}
	 */
	getdatefieldComTwoValue: function() {
		var me = this;
		var com = me.getComponent('datefieldComTwo');
		var value = com.getValue();
		return value;
	},
	/***
	 * 日期选择框(二)
	 * @return {}
	 */
	getdatefieldComTwo: function() {
		var me = this;
		var com = me.getComponent('datefieldComTwo');
		return com;
	},
	/***
	 * 宏结果选择框值
	 * @return {}
	 */
	getcbomacrosListValue: function() {
		var me = this;
		var com = me.getComponent('cbomacrosList');
		var value = com.getValue();
		return value;
	},
	/***
	 * 宏结果选择框
	 * @return {}
	 */
	getcbomacrosList: function() {
		var me = this;
		var com = me.getComponent('cbomacrosList');
		return com;
	},
	/***
	 * 宏结果二选择框值
	 * @return {}
	 */
	getcbomacrosListTwoValue: function() {
		var me = this;
		var com = me.getComponent('cbomacrosListTwo');
		var value = com.getValue();
		return value;
	},
	/***
	 * 宏结果二选择框
	 * @return {}
	 */
	getcbomacrosListTwo: function() {
		var me = this;
		var com = me.getComponent('cbomacrosListTwo');
		return com;
	},
	/***
	 * 数据过滤条件中文输入框值
	 * @return {}
	 */
	getrowFilterCNameValue: function() {
		var me = this;
		var com = me.getComponent('rowFilterCName');
		var value = com.getValue();
		return value;
	},
	/***
	 * 数据过滤条件输入框
	 * @return {}
	 */
	getrowFilterCName: function() {
		var me = this;
		var com = me.getComponent('rowFilterCName');
		return com;
	},

	/***
	 * 默认条件复选框值
	 * @return {}
	 */
	getdefaultConditionValue: function() {
		var me = this;
		var com = me.getComponent('defaultCondition');
		var value = com.getValue();
		return value;
	},
	/***
	 * 默认条件复选框
	 * @return {}
	 */
	getdefaultCondition: function() {
		var me = this;
		var com = me.getComponent('defaultCondition');
		return com;
	},
	/***
	 * 默认条件复选框
	 * @return {}
	 */
	setdefaultCondition: function(value) {
		var me = this;
		var com = me.getComponent('defaultCondition');
		if(com) {
			com.setValue(value);
		}
	},
	/***
	 * 关联类型选择器显示字段
	 * @return {}
	 */
	getcontrastRelationTree: function() {
		var me = this;
		var com = me.getComponent('contrastRelationTree');
		return com;
	},
	/***
	 * 关联类型选择器显示字段 获得当前选中的提交值
	 * @return {}
	 */
	getcontrastRelationTreeValue: function() {
		var me = this;
		var com = me.getComponent('contrastRelationTree');
		var value = com.getSubmitValue();
		return value;
	},
	/***
	 * 关联类型选择按钮
	 * @return {}
	 */
	getbtnRelationCom: function() {
		var me = this;
		var com = me.getComponent('btnRelationCom');
		return com;
	},
	/***
	 * 交互字段值 返回选中的所有节点
	 * @return {}
	 */
	getChecked: function() {
		var me = this;
		var com = me.getComponent('interactionField');
		var arr = com.getChecked();
		return arr;
	},
	/***
	 * 交互字段值 获得当前选中的提交值
	 * @return {}
	 */
	getinteractionFieldValue: function() {
		var me = this;
		var com = me.getComponent('interactionField');
		var value = com.getSubmitValue();
		return value;
	},
	/***
	 * 交互字段值 获得当前选中的显示值
	 * @return {}
	 */
	getDisplayValue: function() {
		var me = this;
		var com = me.getComponent('interactionField');
		var value = com.getDisplayValue();
		return value;
	},
	/***
	 * 交互字段
	 * @return {}
	 */
	getinteractionField: function() {
		var me = this;
		var com = me.getComponent('interactionField');
		return com;
	},
	/***
	 * 对比属性交互字段值 获得当前选中的提交值
	 * @return {}
	 */
	getcontrastTreeValue: function() {
		var me = this;
		var com = me.getComponent('contrastTree');
		var value = com.getSubmitValue();
		return value;
	},
	/***
	 * 对比属性交互字段值 获得当前选中的显示值
	 * @return {}
	 */
	getcTreeDisplayValue: function() {
		var me = this;
		var com = me.getComponent('contrastTree');
		var value = com.getDisplayValue();
		return value;
	},
	/***
	 * 对比属性交互字段
	 * @return {}
	 */
	getcontrastTree: function() {
		var me = this;
		var com = me.getComponent('contrastTree');
		return com;
	},

	/***
	 * 列属性值类型值
	 * @return {}
	 */
	getcolumnTypeListValue: function() {
		var me = this;
		var com = me.getComponent('columnTypeList');
		var value = com.getValue();
		return value;
	},
	/***
	 * 列属性值类型
	 * @return {}
	 */
	getcolumnTypeList: function() {
		var me = this;
		var com = me.getComponent('columnTypeList');
		return com;
	},

	/***
	 * 运算关系下拉列表值
	 * @return {}
	 */
	getoperationTypeValue: function() {
		var me = this;
		var com = me.getComponent('operationType');
		var value = com.getValue();
		return value;
	},
	/***
	 * 运算关系下拉列表显示值
	 * @return {}
	 */
	getoperationNameValue: function() {
		var me = this;
		var com = me.getComponent('operationType');
		var value = com.getRawValue();
		return value;
	},
	/***
	 * 运算关系下拉列表
	 * @return {}
	 */
	getoperationType: function() {
		var me = this;
		var com = me.getComponent('operationType');
		return com;
	},

	/***
	 * 结果录入值:id值
	 * @return {}
	 */
	gettxtResultHiddenValue: function() {
		var me = this;
		var com = me.getComponent('txtResultHidden');
		var value = com.getValue();
		return value;
	},
	/***
	 * 结果录入组件:id值
	 * @return {}
	 */
	gettxtResultHidden: function() {
		var me = this;
		var com = me.getComponent('txtResultHidden');
		return com;
	},

	/***
	 * 增加或关系按钮
	 * @return {}
	 */
	getbtnaddOr: function() {
		var me = this;
		var com = me.getComponent('btnaddOr');
		return com;
	},
	/***
	 * 增加查看全部数据按钮
	 * @return {}
	 */
	getbtnAddAll: function() {
		var me = this;
		var com = me.getComponent('btnAddAll');
		return com;
	},
	/***
	 * 增加行记录按钮
	 * @return {}
	 */
	getbtnAddRecord: function() {
		var me = this;
		var com = me.getComponent('btnAddRecord');
		return com;
	},
	/***
	 * 设置更新ComboBoxTree的相关组件的isbeforeload值和beforeitemexpand
	 * @param {} boolValue
	 */
	setboolValue: function(boolValue) {
		var me = this;
		me.isbeforeload = boolValue;
		me.beforeitemexpand = boolValue;
		//属性
		var interactionField = me.getinteractionField();
		var contrastTree = me.getcontrastTree();
		var contrastRelationTree = me.getcontrastRelationTree();
		interactionField.isbeforeload = boolValue;
		interactionField.beforeitemexpand = boolValue;

		contrastTree.isbeforeload = boolValue;
		contrastTree.beforeitemexpand = boolValue;

		contrastRelationTree.isbeforeload = boolValue;
		contrastRelationTree.beforeitemexpand = boolValue;
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
		var treeCom = me.getcontrastTree();
		treeCom.setValue('');
	},
	/**
	 * number关系运算关系
	 * @type 
	 */
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

	/**
	 * 字符串关系运算关系
	 * @type 
	 */
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
	/**
	 * 日期时间关系运算关系
	 * @type 
	 */
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
	/**
	 * 关联关系运算关系
	 * @type 
	 */
	relationOperationType: [
		//{'value':'=','text': '等于'},
		//{'value':'<>','text': '不等于'},
		{
			'value': 'in',
			'text': '包含'
		},
		{
			'value': 'not in',
			'text': '不包含'
		}
	],
	/**
	 * 布尔勾选关系运算关系
	 * 
	 * @type 
	 */
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
	/**
	 * 对比属性关系运算关系
	 * @type 
	 */
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
	/**
	 * 列属性类型
	 * @type 
	 */
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
	/**
	 * 日期列属性类型
	 * @type 
	 */
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
	/**
	 * 布尔勾选列属性类型
	 * @type 
	 */
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
	/**
	 * 数值型列属性类型
	 * @type 
	 */
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
	/**
	 * 字符串列属性类型
	 * @type 
	 */
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
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(Ext.typeOf(me.callback) == 'function') {
			me.callback(me);
		}
	},
	/***
	 * 打开复选组选择器
	 * @param {} title
	 * @param {} classCode
	 * @param {} com
	 * @param {} textCom
	 */
	openAppShowWin: function(type) {
		var me = this;
		var panel = 'Ext.manage.datafilters.checkGroupSelector';
		var com = me.gettxtResultHidden();
		com.setValue('');
		var maxHeight = document.body.clientHeight * 0.78;
		var maxWidth = document.body.clientWidth * 0.88;
		var win = Ext.create(panel, {
			id: -1,
			internalWhere: '',
			externalWhere: '',
			type: type,
			maxWidth: maxWidth,
			height: maxHeight * 0.88,
			width: maxWidth * 0.78,
			autoScroll: true,
			modal: true, //模态
			floating: true, //漂浮
			closable: true, //有关闭按钮
			resizable: true, //可变大小
			draggable: true, //可移动
			serverUrl: me.selectorServerUrl,
			checkboxgroupName: 'checkboxgroupName', //复选组组名称
			iKeyField: me.iKeyField, //keyField数据项匹配字段,
			iTextField: me.iTextField //textField数据项匹配字段
		});

		if(win.height > maxHeight) {
			win.height = maxHeight;
		}
		//解决chrome浏览器的滚动条问题
		var callback = function() {
			win.hide();
			win.show();
		}
		win.show(null, callback);
		win.on({
			saveClick: function() {
				var values = "";
				if(type === 'CName') {
					values = "" + win.getDisplayValue();
				} else {
					values = "" + win.getSubmitValue();
				}
				com.setValue(values);
				win.close();
			},
			comeBackClick: function() {
				win.close();
			}
		});
	},
	/***
	 * 获取宏命令数据集
	 * 过滤数字类型:IntDynamic
	 * @return {}
	 */
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
				var data = Ext.JSON.decode(response.responseText);
				if(data.success) {
					var count = 0;
					var list = [];
					if(data.ResultDataValue && data.ResultDataValue != '') {
						var ResultDataValue = Ext.JSON.decode(data.ResultDataValue);
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
					Ext.Msg.alert('提示', '获取信息失败！');
				}
			}
		});
		return localData;
	},
	//查询宏命令(HQL)
	getSearchBTDMacroCommandByHQLUrl: getRootPath() + '/ConstructionService.svc/CS_UDTO_SearchBTDMacroCommandByHQL',
	selectMacroCommandFields: 'BTDMacroCommand_Id,BTDMacroCommand_CName,BTDMacroCommand_EName,BTDMacroCommand_MacroCode,BTDMacroCommand_TypeCode,BTDMacroCommand_TypeName'

});