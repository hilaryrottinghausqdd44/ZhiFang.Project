/***
 * 设置数据过滤条件应用
 */
Ext.define('Shell.class.sysbase.rowfilter.basic.AddPanel', {
	extend: 'Ext.panel.Panel',

	title: '',
	cls: 'bg-white',

	//数据过滤条件行记录的Id
	PK: null,
	appType: 'add',
	moduleId: null,
	//模块操作id
	moduleOperId: '',

	objectName: '',
	objectCName: '数据对象',
	//模块操作列表选中行
	moduleOperSelect: null,
	//行过滤条件是否模块操作的默认行过滤查询条件(模块操作的行过滤条件Id不为空时为默认)
	isDefaultCondition: false,

	//行过滤新增服务
	addUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_AddRBACRowFilterAndRBACRoleRightByModuleOperId',
	//行过滤更新服务
	editUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_UpdateRBACRowFilterAndRBACRoleRightByFieldAndModuleOperId',

	afterRender: function() {
		var me = this;
		me.addEvents('saveClick'); //
		me.addEvents('comeBackClick'); //
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建Items栏*/
	createItems: function() {
		var me = this;
		me.createAddForm();
		me.createDatafiltersGrid();
		me.createRoleRightGrid();
		var items = [];
		if(me.AddForm) items.push(me.AddForm);
		if(me.DatafiltersGrid) items.push(me.DatafiltersGrid);
		if(me.RoleRightGrid) items.push(me.RoleRightGrid);
		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createButtontoolbar());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtontoolbar: function() {
		var me = this,
			items = [];
		items.push({
			text: '　保存　',
			xtype: 'button',
			iconCls: 'build-button-save',
			cls: "btn btn-default btn-sm active",
			width: 80,
			itemId: 'save',
			style: {
				marginLeft: '40px'
			},
			handler: function(button) {
				button.setDisabled(true);
				me.save(true, button);
			}
		}, {
			text: '　返回　',
			xtype: 'button',
			iconCls: 'build-button-refresh',
			cls: "btn btn-default btn-sm active",
			width: 80,
			style: {
				marginLeft: '10px'
			},
			itemId: 'comeBack',
			handler: function(button) {
				me.fireEvent('comeBackClick');
			}
		});
		var toolbar = {
			xtype: 'toolbar',
			dock: 'bottom',
			itemId: 'dockedItemsbuttons',
			items: items
		};
		return toolbar;
	},

	loadData: function() {
		var me = this;
	},
	initListeners: function(panel) {
		var me = this;
		me.DatafiltersGrid.on({
			//删除行记录
			deleteClick: function(win, record, rowIndex) {
				if(record) {
					me.DatafiltersGrid.store.remove(record);
				}
			},
			//添加行记录
			addRecordClick: function() {
				me.gridAddRecord();
			},
			//添加左括号
			addLeftBracketClick: function() {
				me.addLeftBracket();
			},
			//添加右括号
			addRightBracketClick: function() {
				me.addRightBracket();
			},
			//添加并且关系
			addAndClick: function() {
				me.addAnd();
			},
			//添加或关系
			addOrClick: function() {
				me.addOr();
			},
			//数据过滤条件查看
			btnSelect: function() {
				var hqlWhere = '';
				hqlWhere = me.transHqlShowStr() + "<br /><br />";
				hqlWhere = hqlWhere + me.transHqlSelectStr();
				if(hqlWhere) {
					Ext.Msg.show({
						title: "查看数据过滤条件",
						msg: '' + hqlWhere,
						width: 600,
						height: 380,
						closable: true,
						modal: true,
						icon: Ext.Msg.INFO
					});
				}
			}
		});
	},
	gridAddRecord: function() {
		var me = this;
		//交互字段值 获得当前选中的提交值
		var submitValue = '' + me.AddForm.getInteractionFieldValue();
		//交互字段值 获得当前选中的显示值
		var displayValue = '' + me.AddForm.getDisplayValue();
		//运算关系下拉列表值
		var operationValue = me.AddForm.getoperationTypeValue();
		var operationNameValue = me.AddForm.getoperationNameValue();
		//值类型值
		var typeValue = me.AddForm.getColumnTypeValue();

		if(!submitValue) {
			JShell.Msg.alert('请选择属性字段', null, 1000);
			return;
		}
		if(!operationValue) {
			JShell.Msg.alert('请选择运算关系符', null, 1000);
			return;
		}
		if(!typeValue) {
			JShell.Msg.alert('提示', '请选择值类型', null, 1000);
			return;
		}

		//日期选择框值
		var datefieldValue = me.AddForm.getdatefieldComValue();
		//宏结果选择框值
		var cbomacrosValue = me.AddForm.getcbomacrosListValue();
		//宏结果选择框二值
		var cbomacrosTwoValue = me.AddForm.getcbomacrosListTwoValue();
		//字符型结果录入值
		var txtString = '' + me.AddForm.gettxtStringValue();
		//布尔勾选录入值
		var booleanComValue = me.AddForm.getbooleanComValue();
		//数值型结果录入值
		var txtNumberfieldValue = me.AddForm.gettxtNumberfieldValue();
		//关联类型的值选择
		var txtResultHidden = me.AddForm.gettxtResultHiddenValue();
		//数值型结果录入值二
		var txtNumberfieldTwoValue = me.AddForm.gettxtNumberfieldTwoValue();
		//字符型结果录入值二
		var txtStringTwoValue = me.AddForm.gettxtStringTwoValue();
		//日期选择框值
		var datefieldComTwoValue = me.AddForm.getdatefieldComTwoValue();

		//对比属性交互字段值 获得当前选中的显示值
		var cTreeDisplayValue = '' + me.AddForm.getContrastInteractionFieldValue();
		//对比属性交互字段值 获得当前选中的提交值
		var contrastTreeValue = '' + me.AddForm.getContrastInteractionFieldValue();

		var content = '';
		var contentTwo = '';
		if(typeValue === 'string') { //字符型结果录入值
			if(!txtString) {
				JShell.Msg.alert('结果值不能为空', null, 1000);
				return;
			}
			content = '' + txtString;
			contentTwo = '' + txtStringTwoValue;
		} else if(typeValue === 'date') { //值类型为日期
			if(!datefieldValue) {
				JShell.Msg.alert('日期选择值不能为空', null, 1000);
				return;
			}
			datefieldValue = '' + Ext.util.Format.date(datefieldValue, 'Y-m-d');
			content = '' + datefieldValue;
			if(datefieldComTwoValue != '') {
				datefieldComTwoValue = '' + Ext.util.Format.date(datefieldComTwoValue, 'Y-m-d');
				contentTwo = '' + datefieldComTwoValue;
			}
		} else if(typeValue === 'macros') { //值类型为宏
			if(!cbomacrosValue) {
				JShell.Msg.alert('宏命令不能为空', null, 1000);
				return;
			}
			if(operationValue == '>= and <=' || operationValue == '<= or >=' || operationValue == '> and <' || operationValue == '< or >') {
				if(!cbomacrosTwoValue) {
					JShell.Msg.alert('宏命令不能为空', null, 1000);
					return;
				}
			}
			content = '' + cbomacrosValue;
			contentTwo = '' + cbomacrosTwoValue;
		} else if(typeValue === 'boolean') { //值类型为布尔勾选
			content = '' + booleanComValue;
			contentTwo = '';
		} else if(typeValue === 'number') { //数值型结果
			if(!txtNumberfieldValue) {
				JShell.Msg.alert('数值不能为空', null, 1000);
				return;
			}
			content = '' + txtNumberfieldValue;
			contentTwo = '' + txtNumberfieldTwoValue;

		} else if(typeValue === 'relation') { //关联类型结果
			if(!txtResultHidden) {
				JShell.Msg.alert('关联对象的结果值不能为空', null, 1000);
				return;
			}
			content = '' + txtResultHidden;
			contentTwo = '';
		} else if(typeValue === 'contrast') { //对比属性
			if(!contrastTreeValue) {
				JShell.Msg.alert('对比属性的属性二名称不能为空', null, 1000);
				return;
			}
			content = '';
			contentTwo = '';
		}

		if((!content) && (typeValue != 'contrast')) {
			JShell.Msg.alert('结果值不能为空', null, 1000);
			return;
		}
		if(submitValue) {
			submitValue = me.transformHqlStr(submitValue);
		}
		//对比属性二
		if(contrastTreeValue) {
			contrastTreeValue = me.transformHqlStr(contrastTreeValue);
		}

		var model = {
			'InteractionField': submitValue,
			'CName': displayValue,
			'InteractionFieldTwo': contrastTreeValue,
			'CNameTwo': cTreeDisplayValue,
			'LogicalType': '',
			'ColumnTypeList': '' + typeValue,
			'OperationType': '' + operationValue,
			'OperationName': '' + operationNameValue,
			'Content': content,
			'ContentTwo': '' + contentTwo
		};
		me.insertRrcord(model);
		//清空表单的结果值
		me.AddForm.setFormValue();
	},
	addLeftBracket: function() {
		var me = this;
		var model = {
			'InteractionField': '',
			'CName': '左括号',
			'LogicalType': '(',
			'ColumnTypeList': '',
			'OperationType': '',
			'OperationName': '左括号',
			'Content': '(',
			'ContentTwo': ''
		};
		me.insertRrcord(model);
	},
	addRightBracket: function() {
		var me = this;
		var model = {
			'InteractionField': '',
			'CName': '右括号',
			'LogicalType': ')',
			'ColumnTypeList': '',
			'OperationType': '',
			'OperationName': '右括号',
			'Content': ')',
			'ContentTwo': ''
		};
		me.insertRrcord(model);
	},
	addAnd: function() {
		var me = this;
		var count = me.DatafiltersGrid.store.getCount();
		if(count == 0) {
			JShell.Msg.alert('并且关系不能添加到第一行', null, 1000);
			return false;
		} else {
			var model = {
				'InteractionField': '',
				'CName': '并且',
				'LogicalType': 'and',
				'ColumnTypeList': '',
				'OperationType': '',
				'OperationName': '并且',
				'Content': 'and',
				'ContentTwo': ''
			};
			me.insertRrcord(model);
		}
	},
	addOr: function() {
		var me = this;
		var count = me.DatafiltersGrid.store.getCount();
		if(count == 0) {
			JShell.Msg.alert('或关系不能添加到第一行', null, 1000);
			return false;
		} else {
			var model = {
				'InteractionField': '',
				'CName': '或',
				'LogicalType': 'or',
				'ColumnTypeList': '',
				'OperationType': '',
				'OperationName': '或',
				'Content': 'or',
				'ContentTwo': ''
			};
			me.insertRrcord(model);
		}
	},
	getSelectIndex: function() {
		var me = this;
		var count = me.DatafiltersGrid.store.getCount();
		var linenum = 0;
		if(count > 0) linenum = count;
		var sm = me.DatafiltersGrid.getSelectionModel().getSelection();
		if(sm) {
			var indexOf = me.DatafiltersGrid.store.indexOf(sm[0])
			linenum = indexOf > -1 ? indexOf + 1 : linenum;
		}
		switch(me.DatafiltersGrid.chooseCheck) {
			case 1: //在选中的之前插入
				linenum = linenum >= 1 ? linenum - 1 : linenum;
				break;
			default:
				break;
		}
		return linenum;
	},
	insertRrcord: function(model) {
		var me = this;
		var index = me.getSelectIndex();

		me.DatafiltersGrid.store.insert(index, model);
		me.DatafiltersGrid.getSelectionModel().select(index);
	},
	//数据过滤行列表的HQLwhere串处理
	transHqlSelectStr: function() {
		var me = this;
		var store = me.DatafiltersGrid.store;
		var hqlAllStr = ''; //最终的hql
		var hqlPartStr = ''; //某一属性行的hql
		var records = [];
		store.each(function(record) {
			records.push(record);
		});
		if(records.length == 0) {
			return '';
		}
		var tempRecord = records[records.length - 1];
		var logicalType = tempRecord.get('LogicalType');
		if(logicalType == 'or') {
			JShell.Msg.alert('最后的行记录不能为或关系,请先删除再操作!', null, 1000);
			return '';
		}
		if(logicalType == 'and') {
			JShell.Msg.alert('最后的行记录不能为并且关系,请先删除再操作!', null, 1000);
			return '';
		}
		store.each(function(record) {
			//逻辑运算符 (,),and,or(如果是or关系,需要将后面的用小括号括起来)
			logicalType = '' + record.get('LogicalType');

			//交互字段
			var fiedValue = '' + record.get('InteractionField');
			//交互字段二(对比属性)
			var fiedTwoValue = '' + record.get('InteractionFieldTwo');
			//值类型
			var ctype = '' + record.get('ColumnTypeList');
			//关系运算符
			var otype = '' + record.get('OperationType');
			//结果值
			var content = '' + record.get('Content');
			//结果值二
			var contentTwo = '' + record.get('ContentTwo');

			switch(logicalType) {
				case '(':
				case ')':
				case 'and':
				case 'or':
					hqlAllStr = hqlAllStr + ' ' + logicalType;
					break;
				default:
					break;
			}
			//值类型不为空时为属性的处理
			switch(ctype) {
				case 'relation': //关联 (in或者 not in)
					hqlPartStr = ' ' + fiedValue + ' ' + otype + ' (' + content + ')';
					break;
				case 'boolean': //布尔勾选(=)
					hqlPartStr = ' ' + fiedValue + ' ' + otype + content;
					break;
				case 'number': //数字
					if(otype == '=' || otype == '<>' || otype == '>' || otype == '<' || otype == '<=' || otype == '>=') {
						hqlPartStr = ' ' + fiedValue + otype + content;
					} else if(otype == '>= and <=') { //区间(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '>=' + content + ' and ' + fiedValue + '<=' + contentTwo + ')';
					} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '<=' + content + ' or ' + fiedValue + '>=' + contentTwo + ')';
					} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '>' + content + ' and ' + fiedValue + '<' + contentTwo + ')';
					} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '<' + content + ' or ' + fiedValue + '>' + contentTwo + ')';
					}
					break;
				case 'macros': //宏命令
					if(otype == '=' || otype == '<>' || otype == '>' || otype == '<' || otype == '<=' || otype == '>=') {
						hqlPartStr = ' ' + fiedValue + ' ' + otype + content;
					} else if(otype == '>= and <=') { //区间(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '>=' + content + ' and ' + fiedValue + '<=' + contentTwo + ')';
					} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '<=' + content + ' or ' + fiedValue + '>=' + contentTwo + ')';
					} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '>' + content + ' and ' + fiedValue + '<' + contentTwo + ')';
					} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedValue + '<' + content + ' or ' + fiedValue + '>' + contentTwo + ')';
					}
					break;
				case 'string':
					hqlPartStr = me.gethqlStrByString(otype, fiedValue, content, contentTwo);
					break;
				case 'date': //日期类型
					hqlPartStr = me.gethqlStrByDate(otype, fiedValue, content, contentTwo);
					break;
				case 'contrast': //对比属性
					if(otype == '=' || otype == '<>' || otype == '>' || otype == '>=' || otype == '<' || otype == '<=') {
						hqlPartStr = ' ' + fiedValue + otype + fiedTwoValue;
					}
					break;
				default:
					hqlPartStr = '';
					break;
			}
			if(hqlPartStr) hqlAllStr = hqlAllStr + '' + hqlPartStr;
		});
		if(hqlAllStr) hqlAllStr.trim();
		return hqlAllStr;
	},
	//数据过滤行列表的中文HQLwhere串处理
	transHqlShowStr: function() {
		var me = this;
		var store = me.DatafiltersGrid.store;
		var hqlAllStr = ''; //最终的hql
		var hqlPartStr = ''; //某一属性行的hql
		var records = [];
		store.each(function(record) {
			records.push(record);
		});
		if(records.length == 0) {
			return '';
		}
		var tempRecord = records[records.length - 1];
		var logicalType = tempRecord.get('LogicalType');
		if(logicalType == 'or') {
			JShell.Msg.alert('最后的行记录不能为或关系,请先删除再操作!', null, 1000);
			return '';
		}
		if(logicalType == 'and') {
			JShell.Msg.alert('最后的行记录不能为并且关系,请先删除再操作!', null, 1000);
			return '';
		}
		store.each(function(record) {
			//属性交互字段
			var fiedValue = '' + record.get('InteractionField');
			//属性名称
			var fiedCName = '' + record.get('CName');

			//关系运算符
			var otype = '' + record.get('OperationType');
			//关系运算符
			var otypeName = '' + record.get('OperationName');

			//属性一结果值
			var content = '' + record.get('Content');

			//逻辑运算符 (,),and,or(如果是or关系,需要将后面的用小括号括起来)
			logicalType = '' + record.get('LogicalType');
			var operationType = '' + record.get('OperationType');

			//交互字段二(对比属性)
			var fiedTwoValue = '' + record.get('InteractionFieldTwo');
			var fiedCNameTwo = '' + record.get('CNameTwo');
			//结果值二
			var contentTwo = '' + record.get('ContentTwo');

			switch(logicalType) {
				case 'and':
				case 'or':
					hqlAllStr = hqlAllStr + ' ' + otypeName;
					break;
				case '(':
				case ')':
					hqlAllStr = hqlAllStr + ' ' + logicalType;
					break;
				default:
					break;
			}
			//值类型不为空时为属性的处理
			switch(record.get('ColumnTypeList')) {
				case 'relation': //关联 (in或者 not in)
					hqlPartStr = ' ' + fiedCName + ' ' + otypeName + ' (' + content + ')';
					break;
				case 'boolean': //布尔勾选(=)
					hqlPartStr = ' ' + fiedCName + ' ' + otypeName + content;
					break;
				case 'number': //数字
					if(otype == '=' || otype == '<>' || otype == '>' || otype == '<' || otype == '<=' || otype == '>=') {
						hqlPartStr = ' ' + fiedCName + otypeName + content;
					} else if(otype == '>= and <=') { //区间(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '>=' + content + ' and ' + fiedCName + '<=' + contentTwo + ')';
					} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '<=' + content + ' or ' + fiedCName + '>=' + contentTwo + ')';
					} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '>' + content + ' and ' + fiedCName + '<' + contentTwo + ')';
					} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '<' + content + ' or ' + fiedCName + '>' + contentTwo + ')';
					}
					break;
				case 'macros': //宏命令
					if(otype == '=' || otype == '<>' || otype == '>' || otype == '<' || otype == '<=' || otype == '>=') {
						hqlPartStr = ' ' + fiedCName + ' ' + otypeName + content;
					} else if(otype == '>= and <=') { //区间(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '>=' + content + ' and ' + fiedCName + '<=' + contentTwo + ')';
					} else if(otype == '<= or >=') { //区间外(包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '<=' + content + ' or ' + fiedCName + '>=' + contentTwo + ')';
					} else if(otype == '> and <') { //区间(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '>' + content + ' and ' + fiedCName + '<' + contentTwo + ')';
					} else if(otype == '< or >') { //区间外(不包含边界)(输入2项)
						hqlPartStr = ' ' + '(' + fiedCName + '<' + content + ' or ' + fiedCName + '>' + contentTwo + ')';
					}
					break;
				case 'string':
					hqlPartStr = me.gethqlStrByString(otype, fiedValue, content, contentTwo);
					//hqlPartStr=hqlPartStr.replace(new RegExp(otype, "g"),otypeName);
					hqlPartStr = hqlPartStr.replace(new RegExp(fiedValue, "g"), fiedCName);
					break;
				case 'date': //日期类型
					hqlPartStr = me.gethqlStrByDate(otype, fiedValue, content, contentTwo);
					//hqlPartStr=hqlPartStr.replace(new RegExp(otype, "g"),otypeName);
					hqlPartStr = hqlPartStr.replace(new RegExp(fiedValue, "g"), fiedCName);
					break;
				case 'contrast': //对比属性
					if(otype == '=' || otype == '<>' || otype == '>' || otype == '>=' || otype == '<' || otype == '<=') {
						hqlPartStr = ' ' + fiedCName + otypeName + fiedCNameTwo;
					}
					break;
				default:
					hqlPartStr = '';
					break;
			}
			if(hqlPartStr) hqlAllStr = hqlAllStr + '' + hqlPartStr;
		});
		if(hqlAllStr) hqlAllStr.trim();
		return hqlAllStr;
	},

	//属性数据类型为日期类型的hqlWhere处理
	gethqlStrByDate: function(otype, fiedValue, content, contentTwo) {
		var me = this;
		var hqlPartStr = "";
		if(otype == '=' || otype == '<>' || otype == '>' || otype == '<' || otype == '<=' || otype == '>=') {
			hqlPartStr = ' ' + fiedValue + otype + "'" + content + "'";
		} else {
			switch(otype) {
				case '>= and <=': //区间(包含边界)(输入2项)
					hqlPartStr = ' ' + '(' + fiedValue + '>=' + "'" + content + "'" + ' and ' + fiedValue + '<=' + "'" + contentTwo + "'" + ')';
					break;
				case '<= or >=': //区间外(包含边界)(输入2项)
					hqlPartStr = ' ' + '(' + fiedValue + '<=' + "'" + content + "'" + ' or ' + fiedValue + '>=' + "'" + contentTwo + "'" + ')';
					break;
				case '> and <':
					hqlPartStr = ' ' + '(' + fiedValue + '>' + "'" + content + "'" + ' and ' + fiedValue + '<' + "'" + contentTwo + "'" + ')';
					break;
				case '< or >':
					hqlPartStr = ' ' + '(' + fiedValue + '<' + "'" + content + "'" + ' or ' + fiedValue + '>' + "'" + contentTwo + "'" + ')';
					break;
				default:
					break;
			}
		}
		return hqlPartStr;
	},
	//属性数据类型为字符串的hqlWhere处理
	gethqlStrByString: function(otype, fiedValue, content, contentTwo) {
		var me = this;
		var hqlPartStr = "";
		switch(otype) {
			case '=':
				hqlPartStr = ' ' + fiedValue + otype + "'" + content + "'";
				break;
			case '<>':
				hqlPartStr = ' ' + fiedValue + otype + "'" + content + "'";
				break;
			case 'like A%': //开始于(A*)
				hqlPartStr = ' ' + fiedValue + " like '" + content + "%'";
				break;
			case 'like %A': //结束于(*A)
				hqlPartStr = ' ' + fiedValue + " like %'" + content + "'";
				break;
			case 'like A%B': //字符之间(A*B) (输入2项)
				hqlPartStr = ' ' + fiedValue + " like " + "'" + content + '%' + contentTwo + "'";
				break;
			case '= or = or =': //等于其中一项(输入多项)
				var arr = content.split(',');
				if(arr && arr.length > 0) {
					var str = '';
					Ext.each(arr, function(item, index, itemAll) {
						if(index < (arr.length - 1)) {
							str = str + (fiedValue + "=" + "'" + item + "'" + " or ");
						} else if(index == (arr.length - 1)) {
							str = str + (fiedValue + "=" + "'" + item + "'");
						}
					});
					hqlPartStr = ' (' + str + ") ";
				}
				break;
			case 'not (= or = or =)': //不等于任何一项(输入多项)
				var arr = content.split(',');
				if(arr && arr.length > 0) {
					var str = '';
					Ext.each(arr, function(item, index, itemAll) {
						if(index < (arr.length - 1)) {
							str = str + (fiedValue + "!=" + "'" + item + "'" + " and ");
						} else if(index == (arr.length - 1)) {
							str = str + (fiedValue + "!=" + "'" + item + "'");
						}
					});
					hqlPartStr = ' (' + str + ") ";
				}
				break;
			case 'like %A% or like %B%': //包含(可输入多个)
				var arr = content.split(',');
				if(arr && arr.length > 0) {
					var str = '';
					Ext.each(arr, function(item, index, itemAll) {
						if(index < (arr.length - 1)) {
							str = str + (fiedValue + " like " + "'%" + item + "%'" + " or ");
						} else if(index == (arr.length - 1)) {
							str = str + (fiedValue + " like " + "'%" + item + "%'");
						}
					});
					hqlPartStr = ' (' + str + ") ";
				}
				break;
			case 'not (like %A% or like %B%)': //不包含(可输入多个)
				var arr = content.split(',');
				if(arr && arr.length > 0) {
					var str = '';
					Ext.each(arr, function(item, index, itemAll) {
						if(index < (arr.length - 1)) {
							//2014-07-23 bug修改
							str = str + (fiedValue + " not like " + "'%" + item + "%'" + " and ");
						} else if(index == (arr.length - 1)) {
							str = str + (fiedValue + " not like " + "'%" + item + "%'");
						}
					});
					hqlPartStr = ' (' + str + ") ";
				}
				break;
			default:
				break;
		}
		return hqlPartStr;
	},
	//过滤结构设计代码
	getAppParams: function(bo) {
		var me = this;
		var DesignCode = me.getRocordInfoArray();
		var appParams = {
			DesignCode: DesignCode,
			roleLists: ''
		};
		var appParams = JcallShell.JSON.encode(appParams);
		appParams = appParams.replace(/\"/g, "&quot;");
		return appParams;
	},
	//获取所有组件属性信息
	getRocordInfoArray: function() {
		var me = this;
		var store = me.DatafiltersGrid.store;
		var records = [];
		store.each(function(record) {
			records.push(record.data);
		});
		return records;
	},
	/***
	 * 将传入的字符串(如BTDModuleType_DataTimeStamp)
	 * 转换为hql查询格式btdmoduletype.DataTimeStamp
	 * 第一个数据对象的字母为小写,其他的数据对象不变,以实心点代替'_'
	 * @param {} objectName
	 * @return {}
	 */
	transformHqlStr: function(objectName) {
		var me = this;
		var defaultValueArr = objectName.split('_');
		var tempStr = '';
		for(var j = 0; j < defaultValueArr.length - 1; j++) {
			if(j == 0) {
				var tempVlue = defaultValueArr[j];
				tempStr = tempStr + tempVlue.toLowerCase() + '.';
			} else if(j < defaultValueArr.length - 1) {
				tempStr = tempStr + defaultValueArr[j] + '.';
			}
		}
		var hqlStr = tempStr + defaultValueArr[defaultValueArr.length - 1];
		return hqlStr;
	},

	getRoleIdStrAndRoleRightIdStr: function(params) {
		var me = this;
		var addRoleIdStr = "",
			roleRightIdStr = "";
		me.RoleRightGrid.store.each(function(record) {
			var IsAdd = "" + record.get('IsAdd');
			//在该模块操作下,角色已经存在,更新行过滤条件关系
			var id = record.get('RBACRoleRight_Id');
			if(!id && IsAdd.toLowerCase() == "true") {
				addRoleIdStr = addRoleIdStr + record.get('RBACRoleRight_RBACRole_Id') + ",";
			} else {
				roleRightIdStr = roleRightIdStr + record.get('RBACRoleRight_Id') + ",";
			}
		});
		if(addRoleIdStr && addRoleIdStr.length > 0) addRoleIdStr = addRoleIdStr.substring(0, addRoleIdStr.length - 1);
		if(roleRightIdStr && roleRightIdStr.length > 0) roleRightIdStr = roleRightIdStr.substring(0, roleRightIdStr.length - 1);
		params["addRoleIdStr"] = addRoleIdStr;
		params["editRoleRightIdStr"] = roleRightIdStr;
		return params;
	},
	getSaveParams: function(button) {},
	save: function(bo, button) {

	}
});