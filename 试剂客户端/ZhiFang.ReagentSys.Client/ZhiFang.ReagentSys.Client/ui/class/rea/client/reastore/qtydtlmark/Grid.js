/**
 * 库存标志维护
 * @author longfc
 * @version 2019-07-09
 */
Ext.define('Shell.class.rea.client.reastore.qtydtlmark.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	
	title: '库存列表',
	width: 800,
	height: 500,

	/**查询数据*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType?isPlanish=true',

	features: [{
		ftype: 'summary'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**库存查询的合并选择项Key*/
	ReaBmsStatisticalTypeKey: "ReaBmsStatisticalType",
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 6,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**库房是否按库房员工权限获取*/
	isEmpPermission: false,
	/**库存标志*/
	ReaBmsQtyDtlMark: "ReaBmsQtyDtlMark",
	/**用户UI配置Key*/
	userUIKey: 'reastore.qtydtlmark.Grid',
	/**用户UI配置Name*/
	userUIName: "库存标志维护",
	defaultOrderBy: [{
		property: 'ReaBmsQtyDtl_DispOrder',
		direction: 'ASC'
	}],
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	/**当前选择的库房ID*/
	StorageID: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		me.initDateArea(-30);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.ReaBmsStatisticalTypeKey, false, false, null);
		//JShell.REA.StatusList.getStatusList(me.ReaGoodsLotVerificationStatus, false, false, null);
		JShell.REA.StatusList.getStatusList(me.ReaBmsQtyDtlMark, false, true, null);
		me.initIsUseEmp();
		me.selectUrl = me.selectUrl + "&isEmpPermission=" + me.isEmpPermission;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	initIsUseEmp: function() {
		var me = this;
		//系统运行参数"是否启用库存库房权限":1:是;2:否;
		var isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
		if(!isUseEmp) {
			JShell.REA.RunParams.getRunParamsValue("ReaBmsQtyDtlIsUseEmp", false, function(data) {
				isUseEmp = JcallShell.REA.RunParams.Lists.ReaBmsQtyDtlIsUseEmp.Value;
				if(isUseEmp && (isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true")) {
					me.isEmpPermission = true;
				}
			});
		} else {
			isUseEmp = "" + isUseEmp;
			if(isUseEmp == 1 || isUseEmp == "1" || isUseEmp == "true") {
				me.isEmpPermission = true;
			}
		}
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsQtyDtl_BarCodeType',
			text: '条码类型',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_StorageName',
			text: '库房',
			width: 100,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_PlaceName',
			text: '货架',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_CompanyName',
			text: '供应商',
			hidden: true,
			width: 120,
			defaultRenderer: true,
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsID',
			text: '货品ID',
			width: 155,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsSort',
			text: '货品序号',
			hidden:true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoodsNo',
			text: '货品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsName',
			text: '货品名称',
			width: 240,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaBmsQtyDtl_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"') >= 0) value = value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_QtyDtlMark',
			width: 250,
			text: '库存标志',
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_LotNo',
			text: '货品批号',
			width: 90,
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsQty',
			text: '库存数',
			width: 95,
			renderer: function(value, meta) {
				var v = value;
				if(v == 0 || v == "0") {
					var style = 'font-weight:bold;';
					style = style + "background-color:red;";
					style = style + "color:#ffffff;";
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					meta.style = style;
				}
				return v;
			}
		}, {
			dataIndex: 'ReaBmsQtyDtl_GoodsUnit',
			text: '单位',
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_ReaGoodsNo',
			hidden: true,
			text: '货品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsQtyDtl_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push(me.createButtonToolbar1Items());
		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var qtyDtlMarkList = JShell.REA.StatusList.Status[me.ReaBmsQtyDtlMark].List || [];
		var items = [];
		items.push({
			boxLabel: '按日期',
			name: 'cboIsDatearea',
			itemId: 'cboIsDatearea',
			xtype: 'checkboxfield',
			inputValue: false,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		}, {
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 185,
			labelWidth: 0,
			labelAlign: 'right',
			fieldLabel: '',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		});
		items.push({
			fieldLabel: '',
			emptyText: '库存标志选择',
			labelWidth: 0,
			width: 245,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'sReaBmsQtyDtlMark',
			name: 'sReaBmsQtyDtlMark',
			value: "",
			data: qtyDtlMarkList,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		
		items.push('-', {
			fieldLabel: '将库存标志更新为',
			emptyText: '库存标志更新',
			labelWidth: 110,
			width: 365,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmReaBmsQtyDtlMark',
			name: 'cmReaBmsQtyDtlMark',
			value: "1",
			data: qtyDtlMarkList,
			listeners: {
				select: function(com, records, eOpts) {

				}
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-edit',
			text: '确认更新',
			tooltip: '确认更新',
			handler: function() {
				me.onUpdate();
			}
		});

		//查询框信息
		me.searchInfo = {
			width: 165,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名称/货品编码/批号',
			fields: ['reabmsqtydtl.GoodsName', 'reabmsqtydtl.ReaGoodsNo', 'reabmsqtydtl.LotNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar1',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		var list = JShell.REA.StatusList.Status[me.ReaBmsStatisticalTypeKey].List;
		items.push('-', {
			boxLabel: '合并',
			name: 'cbMerge',
			itemId: 'cbMerge',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: true,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {

				}
			}
		}, {
			fieldLabel: '',
			emptyText: '库存货品合并方式',
			labelWidth: 0,
			width: 175,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'cmReaBmsStatisticalType',
			name: 'cmReaBmsStatisticalType',
			value: "7",
			data: list,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		}, '-');

		items = me.createDeptNameItems(items);
		items.push({
			emptyText: '一级分类',
			labelWidth: 0,
			width: 100,
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 100,
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		});
		items.push({
			fieldLabel: '',
			name: 'CompanyName',
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供应商选择',
			width: 100,
			labelWidth: 0,
			labelAlign: 'right',
			className: 'Shell.class.rea.client.reacenorg.CheckTree',
			classConfig: {
				title: '供应商选择',
				resizable: false,
				/**是否显示根节点*/
				rootVisible: false,
				/**机构类型*/
				OrgType: "0"
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(p, record);
				}
			}
		}, {
			fieldLabel: '供货商主键ID',
			hidden: true,
			xtype: 'uxCheckTrigger',
			name: 'CompanyID',
			itemId: 'CompanyID'
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 100,
			labelSeparator: '',
			labelAlign: 'right',
			emptyText: '货品选择',
			name: 'GoodsName',
			itemId: 'GoodsName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goods2.basic.CheckGrid',
			classConfig: {
				width: 350,
				checkOne: true,
				title: '货品选择'
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsAccept(p, record);
				}
			}
		}, {
			fieldLabel: '货品主键ID',
			itemId: 'GoodsID',
			name: 'GoodsID',
			xtype: 'textfield',
			hidden: true
		});
		return items;
	},
	/**创建部门*/
	createDeptNameItems: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		//根据登录者的部门id 查询
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		items.push({
			boxLabel: '按部门货品',
			name: 'cboDeptGoods',
			itemId: 'cboDeptGoods',
			xtype: 'checkboxfield',
			inputValue: true,
			checked: false,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue == true) {
						me.getComponent('buttonsToolbar1').getComponent('DeptName').setDisabled(false);
					} else {
						me.getComponent('buttonsToolbar1').getComponent('DeptName').setDisabled(true);
					}
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '',
			emptyText: '按选择部门全部货品过滤',
			name: 'DeptName',
			itemId: 'DeptName',
			width: 100,
			labelWidth: 0,
			snotField: true,
			xtype: 'uxCheckTrigger',
			enableKeyEvents: false,
			editable: false,
			value: deptName,
			className: 'Shell.class.rea.client.CheckOrgTree',
			classConfig: {
				resizable: false,
				/**是否显示根节点*/
				rootVisible: false,
				/**显示所有部门树:false;只显示用户自己的树:true*/
				ISOWN: true
			},
			listeners: {
				check: function(p, record) {
					if(record && record.data && record.data.tid == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDeptAccept(p, record);
					p.close();
				}
			}
		}, {
			fieldLabel: '部门主键ID',
			xtype: 'textfield',
			hidden: true,
			name: 'DeptID',
			itemId: 'DeptID',
			value: deptId
		});
		return items;
	},
	/**部门选择*/
	onDeptAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var Id = buttonsToolbar.getComponent('DeptID'),
			CName = buttonsToolbar.getComponent('DeptName');
		if(!Id) {
			p.close();
			JShell.Msg.overwrite('onDeptAccept');
			return;
		}
		if(record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			return;
		}
		var text = record.data ? record.data.text : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		Id.setValue(record.data ? record.data.tid : '');
		CName.setValue(text);
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**供货方选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var Id = buttonsToolbar.getComponent('CompanyID');
		var CName = buttonsToolbar.getComponent('CompanyName');
		if(record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			me.onSearch();
			return;
		}
		if(record.data) {
			CName.setValue(record.data ? record.data.text : '');
			Id.setValue(record.data ? record.data.tid : '');
			p.close();
			me.onSearch();
		}
	},
	onGoodsAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsID = buttonsToolbar.getComponent('GoodsID');
		var GoodsName = buttonsToolbar.getComponent('GoodsName');
		GoodsID.setValue(record ? record.get('ReaGoods_Id') : '');
		GoodsName.setValue(record ? record.get('ReaGoods_CName') : '');
		p.close();
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//库存查询条件
		var qtyHql = me.getQtyHql();
		if(qtyHql) {
			url += '&where=' + JShell.String.encode(qtyHql);
		}
		//部门货品查询条件
		var deptGoodsHql = me.getDeptGoodsHql();
		if(deptGoodsHql) {
			url += '&deptGoodsHql=' + JShell.String.encode(deptGoodsHql);
		}
		//机构货品查询条件
		var reaGoodsHql = me.getReaGoodsHql();
		if(reaGoodsHql) {
			url += '&reaGoodsHql=' + JShell.String.encode(reaGoodsHql);
		}
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar) {
			var groupType = buttonsToolbar.getComponent('cmReaBmsStatisticalType').getValue();
			if(!groupType) {
				//没有选择内容时
				url += '&groupType=0'
			} else {
				url += '&groupType=' + groupType;
			}
		}
		url += ('&storageId=' + me.StorageID);
		return url;
	},
	setExternalWhere: function(hql) {
		var me = this;
		me.externalWhere = hql;
	},
	/**库存查询条件*/
	getQtyHql: function() {
		var me = this,
			arr = [];

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var GoodsID = buttonsToolbar.getComponent('GoodsID').getValue();
		var CompanyID = buttonsToolbar.getComponent('CompanyID').getValue();

		var buttonsToolbar1 = me.getComponent('buttonsToolbar1');
		var date = buttonsToolbar1.getComponent('date');
		var isDatearea = buttonsToolbar1.getComponent('cboIsDatearea').getValue();
		var qtyDtlMark = ""+buttonsToolbar1.getComponent('sReaBmsQtyDtlMark').getValue();
		
		//货品	
		if(GoodsID) {
			arr.push('reabmsqtydtl.GoodsID=' + GoodsID);
		}
		//供应商	
		if(CompanyID) {
			arr.push('reabmsqtydtl.ReaCompanyID=' + CompanyID);
		}
		if(qtyDtlMark) {
			arr.push('reabmsqtydtl.QtyDtlMark=' + qtyDtlMark);
		}
		//库存数量等于0
		arr.push('(reabmsqtydtl.GoodsQty=0 or reabmsqtydtl.GoodsQty is null)');

		if(isDatearea == true && date) {
			var dateValue = date.getValue();
			if(dateValue) {
				if(dateValue.start) {
					arr.push('reabmsqtydtl.DataAddTime' + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					arr.push('reabmsqtydtl.DataAddTime' + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		var where = "";
		if(arr && arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		return where;
	},
	/**部门货品查询条件*/
	getDeptGoodsHql: function() {
		var me = this,
			arr = [];
		var buttonsToolbar1 = me.getComponent('buttonsToolbar'),
			deptID = buttonsToolbar1.getComponent('DeptID').getValue(),
			cboDeptGoods = buttonsToolbar1.getComponent('cboDeptGoods').getValue();
		//部门ID	
		if(deptID && cboDeptGoods == true) {
			arr.push('readeptgoods.DeptID=' + deptID);
		}
		var where = "";
		if(arr && arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		return where;
	},
	/**机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this,
			arr = [];
		var buttonsToolbar1 = me.getComponent('buttonsToolbar'),
			goodsClass = buttonsToolbar1.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar1.getComponent('GoodsClassType').getValue();
		//一级分类	
		if(goodsClass) {
			arr.push("reagoods.GoodsClass='" + goodsClass + "'");
		}
		//二级分类	
		if(goodsClassType) {
			arr.push("reagoods.GoodsClassType='" + goodsClassType + "'");
		}
		var where = "";
		if(arr && arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		return where;
	},
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cmReaBmsStatisticalType = buttonsToolbar.getComponent('cmReaBmsStatisticalType');
		var cbMerge = buttonsToolbar.getComponent('cbMerge');
		cmReaBmsStatisticalType.disable();

		cbMerge.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue) {
					cmReaBmsStatisticalType.enable();
				} else {
					cmReaBmsStatisticalType.setValue('');
					cmReaBmsStatisticalType.disable();
					me.onSearch();
				}

			}
		});
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;

		if(!me.StorageID) {
			var error = me.errorFormat.replace(/{msg}/, "获取库房信息为空!");
			me.getView().update(error);
			return;
		}

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var cmReaBmsStatisticalType = buttonsToolbar.getComponent('cmReaBmsStatisticalType');
		var cbMerge = buttonsToolbar.getComponent('cbMerge');
		if(cbMerge.getValue()) return;
		cmReaBmsStatisticalType.disable();
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('buttonsToolbar1'),
			date = dateareaToolbar.getComponent('date');
		if(date && dateArea) date.setValue(dateArea);
	},
	onUpdate: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		if(!me.StorageID) {
			JShell.Msg.error("获取库房信息为空!");
			return;
		}
		var buttonsToolbar1 = me.getComponent('buttonsToolbar1');
		var qtyDtlMark = ""+buttonsToolbar1.getComponent('cmReaBmsQtyDtlMark').getValue();
		if(qtyDtlMark=="") {
			JShell.Msg.error("库存标志不能为空!");
			return;
		}
		
		var reaGoodNoListList = [];
		for(var i in records) {
			var reaGoodsNo = records[i].get("ReaBmsQtyDtl_ReaGoodsNo");
			if(reaGoodsNo) reaGoodNoListList.push(reaGoodsNo);
		}
		var entity={
			"StorageID":me.StorageID,
			"QtyDtlMark":qtyDtlMark,
			"ReaGoodNoList":reaGoodNoListList
		};
		var params = JcallShell.JSON.encode({"entity":entity});
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/RS_UDTO_UpdateReaBmsQtyDtlByQtyDtlMark";
		
		me.showMask("库存标志更新中"); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				JShell.Msg.alert("库存标志更新成功!", null, 1000);
				me.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});