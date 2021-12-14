/***
 * 设置数据过滤条件表单
 */
Ext.define('Shell.class.sysbase.rowfilter.datafilters.AddForm', {
	extend: 'Shell.class.sysbase.rowfilter.basic.AddForm',
	requires: [
		'Shell.class.sysbase.rowfilter.datafilters.tree.AttributesTree'
	],
	title: '',

	//是否在节点展开后处理其他事情
	isbeforeload: true,
	//是否在节点展开后处理其他事情
	beforeitemexpand: true,
	//下拉列表框树的所需的数据对象名称
	objectName: '',
	//下拉列表框树的所需的数据对象中文名
	objectCName: '数据对象',

	autoScroll: true,

	childrenField: 'Tree',
	fields: ['id', 'text', 'expanded', 'leaf', 'FieldClass', 'tid', 'value', 'InteractionField', 'ParentCName', 'ParentEName'],
	//获取数据对象内容的服务地址
	objectPropertyUrl: JShell.System.Path.ROOT + '/ConstructionService.svc/CS_BA_GetEntityFrameTree',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.items || [];
		me.callParent(arguments);
	},
	createItems: function(treeStore) {
		var me = this;
		var treeStore = me.createTreeStore();
		me.items = me.items || [];
		me.createFristRowItems();
		me.items.push(me.createInteractionField(treeStore));
		//关系
		me.items.push(me.createOperationType());
		//值类型
		me.items.push(me.createColumnType());
		//其他默认组件
		me.createOthersItems();
		//显示字段
		me.items.push(me.createContrastRelation(treeStore));
		me.items.push(me.createButtonRelationCom());
		me.items.push({
			//关联结果录入的id值
			xtype: 'hiddenfield',
			name: 'txtResultHidden',
			itemId: 'txtResultHidden'
		});
		//对比属性交互字段下拉框树-属性二
		me.items.push(me.createContrastInteractionField(treeStore));
	},
	//交互字段
	createInteractionField: function(treeStore) {
		var me = this;
		var InteractionField = Ext.create('Shell.class.sysbase.rowfilter.datafilters.tree.AttributesTree', {
			name: 'InteractionField',
			itemId: 'InteractionField',
			x: 5,
			y: 40,
			fieldLabel: '属性',
			labelWidth: 60,
			width: 360,
			rootVisible: false,
			nodeClassName: '',
			ParentCName: '',
			ParentEName: '',
			CName: '',
			objectName: me.objectName,
			isbeforeload: me.isbeforeload,
			beforeitemexpand: me.beforeitemexpand,
			ClassName: "",

			editable: false,
			forceSelection: true,
			queryMode: 'local',
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
					me.onInteractionFieldClick(self, record, e, object);
				}
			}

		});
		return InteractionField;
	},
	onInteractionFieldClick: function(self, record, e, object) {
		var me = this;
		var leaf = record.get('leaf');
		var type = '' + record.get('FieldClass');
		if(leaf == false) {
			//如果是父节点,需要动态加载该父节点的数据对象的字段属性数据
			me.nodeClassName = '' + type;

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
			var tempCName = '' + arrCName[arrCName.length - 1];
			
			var oTypeCom = me.getoperationType();
			var cTypeList = me.getColumnType();
			oTypeCom.setValue('');

			lastName = lastName.toLowerCase();
			if(((lastName == "name" || lastName == "cname") && arr.length > 2) || (tempCName == '名称' && arrCName.length > 2)) {
				//关联(子对象的中文名称也关联)
				cTypeList.store.loadData(me.stringTypeList);
				oTypeCom.store.loadData(me.relationOperationType);
				cTypeList.setValue('relation');
				oTypeCom.setValue('in');
			} else if(type == "date" || (type == 'Nullable`1' && lastName.indexOf('id') == -1)) { //日期型
				cTypeList.store.loadData(me.dateTypeList);
				oTypeCom.store.loadData(me.dateOperationType);
				cTypeList.setValue('date');
				oTypeCom.setValue('=');
			} else if((type == "number" || type == 'Int64' || type == 'Int32') || (type == 'Nullable`1' && lastName.indexOf('id') == -1)) { //数值型
				cTypeList.store.loadData(me.numberTypeList);
				oTypeCom.store.loadData(me.numberOperationType);
				cTypeList.setValue('number');
				oTypeCom.setValue('=');
			} else if(lastName == 'id' || lastName.indexOf('id') >= 0) {
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
	},
	//关联类型的选择器的显示字段的下拉框树
	createContrastRelation: function(treeStore) {
		var me = this;
		me.ContrastRelation = Ext.create('Shell.class.sysbase.rowfilter.datafilters.tree.AttributesTree', {
			name: 'ContrastRelation',
			itemId: 'ContrastRelation',
			hidden: true,
			x: 382,
			y: 76,
			rootVisible: false,
			nodeClassName: '',
			ParentCName: '',
			ParentEName: '',
			CName: '',
			objectName: me.objectName,
			isbeforeload: me.isbeforeload,
			beforeitemexpand: me.beforeitemexpand,
			ClassName: "",
			fieldLabel: '显示字段',
			labelWidth: 60,
			enableKeyEvents: true,
			forceSelection: true,
			width: 240,
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
		return me.ContrastRelation;
	},
	//下拉框树数据
	createTreeStore: function() {
		var me = this;
		var url = "";
		if(me.moduleOperId && me.moduleOperId != "" && me.moduleOperId != null) {
			url = JShell.System.Path.ROOT + "/ConstructionService.svc/CS_BA_GetEntityFrameTreeByModuleOperID?ModuleOperID=" + me.moduleOperId + "&EntityName=" + me.objectName;
		} else { //原来
			url = me.objectPropertyUrl + "?" + me.objectPropertyParam + "=" + me.objectName;
		}
		var treeStore = new Ext.data.TreeStore({
			fields: me.fields,
			proxy: {
				type: 'ajax',
				url: url,
				extractResponseData: function(response) {
					var data = JShell.JSON.decode(response.responseText);
					if(data.ResultDataValue && data.ResultDataValue != "") {
						var arr = JShell.JSON.decode(data.ResultDataValue);
						//过滤时间戳
						var children = [];
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
						if(me.getContrastRelation().nodeClassName != "") {
							data['Tree'] = children;
						} else { //根节点
							data['Tree'] = children;
						}
					}
					response.responseText = JShell.JSON.encode(data);
					return response;
				}
			},
			defaultRootProperty: 'Tree',
			autoLoad: false
		});
		return treeStore;
	},
	onOperationTypeSelect: function(com, records, eOpts) {
		var me = this;

		var newValue = me.getColumnTypeValue();
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
		cbomacrosListTwo.setVisible(false);

		me.getContrastRelation().setVisible(false);
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
			me.getContrastRelation().setVisible(true);
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
	//对比属性交互字段下拉框树
	createContrastInteractionField: function(treeStore) {
		var me = this;
		me.ContrastInteractionField = Ext.create('Shell.class.sysbase.rowfilter.datafilters.tree.AttributesTree', {
			name: 'ContrastInteractionField',
			itemId: 'ContrastInteractionField',
			hidden: true,
			x: 382,
			y: 76,
			rootVisible: false,
			isbeforeload: me.isbeforeload,
			beforeitemexpand: me.beforeitemexpand,
			nodeClassName: '',
			ParentCName: '',
			ParentEName: '',
			CName: '',
			ClassName: '',
			fieldLabel: '属性二',
			labelWidth: 60,
			editable: false,
			width: 240,
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
		return me.ContrastInteractionField;
	}
});