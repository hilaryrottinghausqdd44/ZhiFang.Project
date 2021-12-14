/***
 * 设置数据过滤条件表单
 */
Ext.define('Shell.class.sysbase.rowfilter.preconditions.AddForm', {
	extend: 'Shell.class.sysbase.rowfilter.basic.AddForm',
	title: '',

	EntityCode: '',
	EntityCName: '',
	PreconditionsList: [],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.items || [];
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.items = me.items || [];

		me.createFristRowItems();
		//属性下拉选择
		me.createInteractionField();
		me.items.push(me.InteractionField);

		//关系
		me.items.push(me.createOperationType());
		//值类型
		me.items.push(me.createColumnType());
		//其他默认组件
		me.createOthersItems();

		//显示字段
		me.createContrastRelation();
		me.items.push(me.ContrastRelation);

		me.items.push(me.createButtonRelationCom());
		me.items.push({
			//关联结果录入的id值
			xtype: 'hiddenfield',
			name: 'txtResultHidden',
			itemId: 'txtResultHidden'
		});
		//对比属性交互字段下拉框树-属性二
		me.createContrastInteractionField();
		me.items.push(me.ContrastInteractionField);

		me.getPreconditionsLists(function(datalist) {
			me.InteractionField.store.loadData(datalist);
			me.ContrastRelation.store.loadData(datalist);
			me.ContrastInteractionField.store.loadData(datalist);
		});
	},
	//交互字段
	createInteractionField: function() {
		var me = this;
		me.InteractionField = {
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
				data: []
			}),
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {
					me.onInteractionFieldChange(com, newValue, oldValue, e, eOpts);
				}
			}
		};
	},
	//关联类型的选择器的显示字段的下拉框
	createContrastRelation: function() {
		var me = this;
		me.ContrastRelation = {
			xtype: 'combobox',
			name: 'ContrastRelation',
			itemId: 'ContrastRelation',
			hidden: true,
			x: 382,
			y: 76,
			fieldLabel: '显示字段',
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
				data: []
			}),
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {

				}
			}
		};
	},
	//获取某一模块服务下的预置条件项数据
	getPreconditionsLists: function(callback) {
		var me = this;
		var dataList = [];
		me.PreconditionsList = [];
		if(me.moduleOperId) {
			var myUrl = JShell.System.Path.ROOT + "/RBACService.svc/RBAC_UDTO_SearchRBACPreconditionsByHQL??isPlanish=true&fields=RBACPreconditions_EName,RBACPreconditions_ValueType,RBACPreconditions_CName";
			var hqlWhere = "rbacpreconditions.RBACModuleOper.Id=" + me.moduleOperId + " and rbacpreconditions.EntityCode='" + me.EntityCode + "'";
			myUrl += '&where=' + hqlWhere;
			JShell.Server.get(myUrl, function(data) {
				if(data.success) {
					var list = data.value.list;
					//callback(dataList);
					for(var i = 0; i < list.length; i++) {
						var item = {
							value: list[i]["EName"], //+ "|" + list[i]["ValueType"] //数据类型
							text: list[i]["CName"]
						};
						me.PreconditionsList.push({
							EName: list[i]["EName"],
							CName: list[i]["CName"],
							ValueType: list[i]["ValueType"]
						});
						dataList.push(item);
						callback(dataList, me);
					}
				} else {
					callback(dataList, me);
				}
			});
		}
		//return dataList;
	},
	onInteractionFieldChange: function(com, newValue, oldValue, e, eOpts) {
		var me = this;
		var fieldValue = newValue;
		var tempCName = "",
			type = "";
		for(var i = 0; i < me.PreconditionsList.length; i++) {
			var obj = me.PreconditionsList[i];
			if(obj.EName == newValue) {
				type = obj.ValueType;
				tempCName = obj.CName;
				break;
			}
		}
		var lastName = fieldValue.toLowerCase();
		var oTypeCom = me.getoperationType();
		var cTypeList = me.getColumnType();
		oTypeCom.setValue('');

		lastName = lastName.toLowerCase();
		if(type == "string") {
			cTypeList.store.loadData(me.stringTypeList);
			oTypeCom.store.loadData(me.stringOperationType);
			cTypeList.setValue('string');
			oTypeCom.setValue('=');
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

		me.getComponent('ContrastRelation').setVisible(false);
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
			me.getComponent('ContrastRelation').setVisible(true);
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
	//对比属性交互字段下拉框
	createContrastInteractionField: function() {
		var me = this;
		me.ContrastInteractionField = {
			xtype: 'combobox',
			name: 'ContrastInteractionField',
			itemId: 'ContrastInteractionField',
			hidden: true,
			x: 382,
			y: 76,
			fieldLabel: '属性二',
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
				data: []
			}),
			listeners: {
				change: function(com, newValue, oldValue, e, eOpts) {}
			}
		};
		return me.ContrastInteractionField;
	}
});