/**
 * 检查并打款
 * @author longfc
 * @version 2017-03-01
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.basic.EditBonusGrid', {
	extend: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.BonusGrid',

	/**默认加载数据*/
	defaultLoad: false,
	/**是否隐藏工具栏查询条件*/
	hiddenbuttonsToolbar: false,
	hasButtontoolbar: true,
	checkOne: false,
	/**后台排序*/
	remoteSort: true,
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**状态查询选择项过滤*/
	removeSomeStatusList: function() {
		var me = this;
		var tempList = me.StatusList;
		var itemArr = [];
		//临时
		if(tempList[1]) itemArr.push(tempList[1]);
		Ext.Array.each(itemArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, itemArr[index]);
		});
		return tempList;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 150,
			emptyText: '医生/手机号/身份证号',
			isLike: true,
			itemId: 'search',
			fields: ['DoctorName', 'IDNumber', 'MobileCode']
		};
		var items = me.buttonToolbarItems || [];
		if(!me.StatusList || me.StatusList.length == 0) me.getStatusListData();
		var tempStatus = me.StatusList;
		tempStatus = me.removeSomeStatusList();
		items.push({
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: tempStatus,
			value: "",
			width: 140,
			labelWidth: 40,
			fieldLabel: '状态',
			tooltip: '状态选择',
			emptyText: '状态选择',
			itemId: 'selectStatus',
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '银行种类',
			xtype: 'textfield',
			hidden: true,
			name: 'BankID',
			itemId: 'BankID'
		}, {
			fieldLabel: "银行种类",
			labelWidth: 65,
			width: 205,
			name: 'BankName',
			itemId: 'BankName',
			emptyText: "银行种类",
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: "银行种类",
				height: 380,
				defaultLoad: true,
				defaultWhere: "bdict.BDictType.DictTypeCode='SYS1002'"
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('buttonsToolbar').getComponent('BankName');
					var Id = me.getComponent('buttonsToolbar').getComponent('BankID');
					CName.setValue(record ? record.get('BDict_CName') : '');
					Id.setValue(record ? record.get('BDict_Id') : '');
					p.close();
				}
			}
		});
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});
		if(me.hasExportExcel) {
			items.push({
				xtype: 'button',
				itemId: 'btnExportExcel',
				iconCls: 'file-excel hand',
				text: "导出",
				tooltip: '导出清单为excel',
				handler: function() {
					me.onDownLoadExcel();
				}
			});
		}
		return items;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = [];
		params = me.getSearchWhereHQL();
		return me.callParent(arguments);
	},
	getSearchWhereHQL: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var bankID = "",
			selectStatus = "",
			search = null;
		var params = [];

		if(buttonsToolbar) {
			search = buttonsToolbar.getComponent('search').getValue();
			bankID = buttonsToolbar.getComponent('BankID').getValue();
			selectStatus = buttonsToolbar.getComponent('selectStatus').getValue();
		}

		if(bankID && bankID != "") {
			params.push("BankID=" + bankID + "");
		}
		if(selectStatus) {
			params.push("Status=" + selectStatus + "");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			params.push(me.defaultWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			params.push(me.externalWhere);
		}
		return params;
	},
	/**初始化默认条件*/
	initDefaultWhere: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';
		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') ';
		}
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this;
		var params = [];
		params = me.getSearchWhereHQL();
		var showInfo = "";
		var isExec = true;
		var where = "";
		if(params.length > 0) {
			where = params.join(") and (");
			if(where) where = "(" + where + ")";
		} else {
			showInfo = "查询条件不能为空!";
			isExec = false;
		}
		if(isExec && where != "") {
			var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
			url += "?operateType=1" + "&where=" + where;
			window.open(url);
		} else {
			JShell.Msg.alert(showInfo, null, 2000);
		}
	}
});