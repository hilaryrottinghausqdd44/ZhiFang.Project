/**
 * 出库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.out.search.DocGrid', {
	extend: 'Shell.class.rea.client.out.basic.DocGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	/**出库类型*/
	outTypeList: [],
	/**用户UI配置Key*/
	userUIKey: 'out.search.DocGrid',
	/**用户UI配置Name*/
	userUIName: "出库总单列表",
	/**出库单状态默认选择值*/
	defaultStatus: '6',
	/**默认加载数据*/
	defaultLoad: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsOutDocStatus, false, true, null);
		me.getTypeList();
		//初始化参数
		me.initOutParams();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	//初始化参数
	initOutParams: function() {
		var me = this;
		me.initRunParams();
		me.changeType();
		var isUseEmpOut = me.IsEmpOut ? 1 : 2;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		me.selectUrl += '&empId=' + userId + '&type=' + me.typeByHQL + '&isUseEmpOut=' + isUseEmpOut;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var items = []; // me.callParent(arguments);
		if(!items) items = [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDataTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonsToolbar4());
		items.push(me.createDefaultButtonToolbarItems());
		items.push(me.createPrintButtonToolbarItems());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			items = [];
		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this;
		var items = [];
		//查询框信息
		me.searchInfo = {
			emptyText: '出库总单号/使用部门',
			itemId: 'search',
			flex: 1,
			isLike: true,
			fields: ['reabmsoutdoc.OutDocNo', 'reabmsoutdoc.DeptName']
		};
		items.push({
			fieldLabel: '',
			name: 'OutType',
			emptyText: '出库类型',
			itemId: 'OutType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			labelWidth: 0,
			labelAlign: 'right',
			data: me.outTypeList,
			width: 110,
			value: '',
			listeners: {
				change: function() {
					me.onSearch();
				}
			}
		});
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/**默认按钮栏*/
	createButtonsToolbar4: function() {
		var me = this;
		var items = [];
		me.createDeptItems(items);
		me.createStorageNameItems(items);
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar4',
			items: items
		});
	},
	/**使用部门选择*/
	onDepAccept: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar4');
		var deptID = buttonsToolbar.getComponent('DeptID');
		var deptName = buttonsToolbar.getComponent('DeptName');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		deptID.setValue(id);
		deptName.setValue(text);
	},
	/**创建库房*/
	createDeptItems: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}

		items.push({
			fieldLabel: '',
			name: 'DeptName',
			itemId: 'DeptName',
			xtype: 'uxCheckTrigger',
			labelWidth: 0,
			labelAlign: 'right',
			width: "48%",
			emptyText: '使用部门',
			className: 'Shell.class.rea.client.out.basic.CheckTree',
			classConfig: {
				title: '部门选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					if(record && record.get("tid") == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDepAccept(record);
					me.onSearch();
					p.close();
				}
			}
		}, {
			fieldLabel: '部门id',
			xtype: 'uxCheckTrigger',
			name: 'DeptID',
			itemId: 'DeptID',
			hidden: true
		});
		return items;
	},
	/**创建库房*/
	createStorageNameItems: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			fieldLabel: "",
			emptyText: '',
			name: 'StorageName',
			itemId: 'StorageName',
			labelWidth: 0,
			width: "48%",
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			emptyText: '库房选择',
			className: 'Shell.class.rea.client.shelves.storage.CheckGrid',
			classConfig: {
				title: '库房选择',
				/**是否单选*/
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onStorageCheck(p, record);
					me.onSearch();
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'StorageID',
			name: 'StorageID',
			fieldLabel: '库房ID',
			hidden: true
		});
		return items;
	},
	/**库房选择*/
	onStorageCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar4');
		var storageID = buttonsToolbar.getComponent('StorageID');
		if(!storageID) {
			p.close();
			JShell.Msg.overwrite('onStorageCheck');
			return;
		}
		storageID.setValue(record ? record.get('ReaStorage_Id') : '');
		var storageName = buttonsToolbar.getComponent('StorageName');
		storageName.setValue(record ? record.get('ReaStorage_CName') : '');
		p.close();
	},
	/**客户端出库类型*/
	getTypeList: function(callback) {
		var me = this;
		if(me.outTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsOutDocOutType",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.outTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsOutDocOutType.length > 0) {
						me.outTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsOutDocOutType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.outTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			buttonsToolbar4 = me.getComponent('buttonsToolbar4'),
			dateareaToolbar = me.getComponent('dateareaToolbar');

		var date = dateareaToolbar.getComponent('date');
		var dateType = dateareaToolbar.getComponent('dateType');
		var search = buttonsToolbar2.getComponent('search');
		var outType = buttonsToolbar2.getComponent('OutType');

		var deptID = buttonsToolbar4.getComponent('DeptID');
		var storageID = buttonsToolbar4.getComponent('StorageID');

		var where = [];
		//		where.push("reabmsoutdoc.OutType="+me.defaluteOutType);
		if(outType.getValue()) {
			where.push("reabmsoutdoc.OutType=" + outType.getValue());
		}
		if(deptID.getValue()) {
			where.push("reabmsoutdoc.DeptID=" + deptID.getValue());
		}
		if(storageID.getValue()) {
			where.push("reabmsoutdoc.StorageID=" + storageID.getValue());
		}
		if(me.searchStatusValue != null && parseInt(me.searchStatusValue) > -1)
			where.push("reabmsoutdoc.Status=" + me.searchStatusValue);

		//		if(date) {
		//			var dateValue = date.getValue();
		//			if(dateValue) {
		//				if(dateValue.start) {
		//					where.push('reabmsoutdoc.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
		//				}
		//				if(dateValue.end) {
		//					where.push('reabmsoutdoc.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
		//				}
		//			}
		//		}
		if(date) {
			var dateValue = date.getValue();
			var dateTypeValue = dateType.getValue();
			if(dateValue && dateTypeValue) {
				if(dateValue.start) {
					where.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					where.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		return where.join(" and ");
	},
	//根据类型，赋值
	changeType: function() {
		var me = this;
		me.typeByHQL = '4';
	},
	removeSomeStatusList: function() {
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsOutDocStatus].List));
		var removeArr = [];
		//暂存
		if(tempList[1]) removeArr.push(tempList[1]);
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		me.searchStatusValue = tempList;
		return tempList;
	}
});