/***
 * 设置数据过滤条件应用
 */
Ext.define('Shell.class.sysbase.rowfilter.datafilters.AddPanel', {
	extend: 'Shell.class.sysbase.rowfilter.basic.AddPanel',

	title: '',
	cls: 'bg-white',

	//数据过滤条件行记录的Id
	PK: null,
	appType: 'add',

	//行过滤新增服务
	addUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_AddRBACRowFilterAndRBACRoleRightByModuleOperId',
	//行过滤更新服务
	editUrl: JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_UpdateRBACRowFilterAndRBACRoleRightByFieldAndModuleOperId',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		me.loadData();
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
	//创建表单
	createAddForm: function() {
		var me = this;
		me.AddForm = Ext.create('Shell.class.sysbase.rowfilter.datafilters.AddForm', {
			header: false,
			border: false,
			split: false,
			collapsible: true,
			region: "north",
			itemId: 'AddForm',
			moduleOperId: me.moduleOperId,
			moduleOperSelect: me.moduleOperSelect,
			objectName: me.objectName,
			objectCName: me.objectCName
		});
	},
	//数据条件列表
	createDatafiltersGrid: function() {
		var me = this;
		me.DatafiltersGrid = Ext.create('Shell.class.sysbase.rowfilter.basic.DatafiltersGrid', {
			header: false,
			itemId: 'DatafiltersGrid',
			title: '',
			autoScroll: true,
			border: true,
			region: "center",
			collapsible: true
		});
	},
	createRoleRightGrid: function() {
		var me = this;
		me.RoleRightGrid = Ext.create('Shell.class.sysbase.rowfilter.datafilters.roleright.Grid', {
			itemId: 'RoleRightGrid',
			header: false,
			title: '角色选择',
			autoScroll: true,
			region: "east",
			width: 220,
			split: false,
			collapsible: false,
			border: true,
			moduleId: me.moduleId,
			moduleOperId: me.moduleOperId,
			rowFilterId: me.PK
		});
	},
	loadData: function() {
		var me = this;
		if(me.isDefaultCondition) {
			me.AddForm.setdefaultCondition(true);
		}
		if(me.PK) {
			me.RoleRightGrid.rowFilterId = me.PK;
			me.RoleRightGrid.load();
			var hqlWhere = 'rbacrowFilter.Id=' + me.PK;
			var fields = "RBACRowFilter_RowFilterConstruction,RBACRowFilter_CName";
			var url = JShell.System.Path.ROOT + '/RBACService.svc/RBAC_UDTO_SearchRBACRowFilterById?isPlanish=true&fields=' + fields + "&id=" + me.PK;
			JShell.Server.get(url, function(data) {
				if(data.success) {
					if(data.value) {
						data.value = JShell.Server.Mapping(data.value);
						me.lastData = data.value;
						//数据过滤条件的过滤结构还原
						var rowFilterConstruction = data.value['RBACRowFilter_RowFilterConstruction'];
						if(rowFilterConstruction) rowFilterConstruction = rowFilterConstruction.replace(/&apos;/g, '"');
						rowFilterConstruction = JcallShell.JSON.decode(rowFilterConstruction);
						if(rowFilterConstruction) {
							var array = rowFilterConstruction['DesignCode'];
							//赋值
							me.DatafiltersGrid.store.loadData(array);
							if(array.length >= 1) {
								me.DatafiltersGrid.getSelectionModel().select(0);
							}
						}
						//数据过滤条件中文名称还原
						var cname = data.value['RBACRowFilter_CName'];
						var cnameCom = me.AddForm.getrowFilterCName();
						cnameCom.setValue(cname);
					}
				} else {
					var msg = data.msg;
					msgInfo = msgInfo + '失败';
					JShell.Msg.error(msgInfo);
				}
			});
		}
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

	getSaveParams: function(button) {
		var me = this;
		//过滤结构设计代码（还原代码）
		var appParams = me.getAppParams();
		var id = -1;
		if(me.PK) id = me.PK;
		//默认条件复选框值
		me.isDefaultCondition = me.AddForm.getdefaultConditionValue();
		var cname = '' + me.AddForm.getrowFilterCNameValue();
		if(!cname) {
			button.setDisabled(false);
			JShell.Msg.alert('名称不能为空!', null, 1000);
			return false;
		} else {
			//var hqlWhere = "" + me.transRecordsHqlStr();
			var hqlWhere = "" + me.transHqlSelectStr();
			var RBACRowFilter = {
				"Id": id,
				"LabID": 0,
				IsPreconditions: false,
				"IsUse": true, //是否使用
				"CName": "" + cname,
				"RowFilterCondition":hqlWhere, //过滤条件
				"RowFilterConstruction": "" + appParams, //过滤结构,设计代码
				"EntityCode":me.objectName
			};
			var paramStr = "";
			var params = {
				'entity': RBACRowFilter,
				isDefaultCondition: me.isDefaultCondition,
				moduleOperId: me.moduleOperId,
				addRoleIdStr: '',
				editRoleRightIdStr: ''
			};
			params = me.getRoleIdStrAndRoleRightIdStr(params);
			if(me.appType == "edit") params['fields'] = 'CName,RowFilterCondition,RowFilterConstruction,Id,IsPreconditions,EntityCode';
			paramStr = JcallShell.JSON.encode(params);
			return paramStr;
		}
	},

	save: function(bo, button) {
		var me = this;
		var params = me.getSaveParams(button);
		var url = me.addUrl;
		if(me.appType == "edit") url = me.editUrl;
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				//新增成功后的处理
				if(me.appType === 'add' && data.value.id) {
					me.PK = data.value.id;
				}
				button.setDisabled(false);
				me.fireEvent('saveClick', me, me.PK);
			} else {
				button.setDisabled(false);
				var errorInfo = '';
				if(me.PK) {
					errorInfo = '修改数据过滤条件失败！错误信息:' + data.msg + '</b>';
				} else {
					errorInfo = '新增数据过滤条件失败！错误信息:' + data.msg + '</b>';
				}
				JShell.Msg.error(errorInfo);
			}
		});
	}
});