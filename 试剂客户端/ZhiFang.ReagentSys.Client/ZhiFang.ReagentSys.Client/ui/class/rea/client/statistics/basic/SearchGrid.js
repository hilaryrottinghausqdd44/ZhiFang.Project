/**
 * 统计查询列表父类
 * @author longfc
 * @version 2018-09-11
 */
Ext.define('Shell.class.rea.client.statistics.basic.SearchGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.RowExpander',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.TextSearchTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '',

	/**默认加载*/
	defaultLoad: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: null,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",
	/**是否启用模糊查询选择类型*/
	hasSearchType: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建部门*/
	createDeptNameItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		//根据登录者的部门id 查询
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		items.push({
			fieldLabel: '',
			emptyText: '部门选择',
			name: 'DeptName',
			itemId: 'DeptName',
			width: 135,
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
					if (record && record.data && record.data.tid == 0) {
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
	/**创建供应商*/
	createCompanyNameItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			name: 'CompanyName',
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供货商选择',
			width: 135,
			labelWidth: 0,
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
			xtype: 'textfield',
			name: 'CompanyID',
			itemId: 'CompanyID'
		}, {
			fieldLabel: '供货商机构编码',
			xtype: 'textfield',
			hidden: true,
			name: 'ReaCompCode',
			itemId: 'ReaCompCode'
		}, {
			fieldLabel: '供应商机构平台码',
			xtype: 'textfield',
			hidden: true,
			name: 'ReaServerCompCode',
			itemId: 'ReaServerCompCode'
		});
		return items;
	},
	/**创建订货方*/
	createLabcNameItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			name: 'LabcName',
			itemId: 'LabcName',
			xtype: 'uxCheckTrigger',
			emptyText: '订货方选择',
			width: 105,
			labelWidth: 0,
			className: 'Shell.class.rea.client.reacenorg.CheckTree',
			classConfig: {
				/**是否显示根节点*/
				rootVisible: false,
				/**机构类型*/
				OrgType: "1",
				resizable: false
			},
			listeners: {
				check: function(p, record) {
					me.onLabcAccept(p, record);
				}
			}
		}, {
			fieldLabel: '订货方主键ID',
			xtype: 'textfield',
			hidden: true,
			name: 'LabcID',
			itemId: 'LabcID'
		}, {
			fieldLabel: '订货方平台机构编码',
			xtype: 'textfield',
			hidden: true,
			name: 'ReaServerLabcCode',
			itemId: 'ReaServerLabcCode'
		});
		return items;
	},
	/**创建使用仪器*/
	createTestEquipLabNameItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			name: 'TestEquipLabName',
			itemId: 'TestEquipLabName',
			xtype: 'uxCheckTrigger',
			emptyText: '仪器选择',
			width: 105,
			labelWidth: 0,
			className: 'Shell.class.rea.client.equip.lab.CheckGrid',
			classConfig: {
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					me.onTestEquipLabAccept(p, record);
				}
			}
		}, {
			fieldLabel: '使用仪器ID',
			hidden: true,
			xtype: 'textfield',
			name: 'TestEquipLabId',
			itemId: 'TestEquipLabId'
		});
		return items;
	},
	/**创建库房*/
	createStorageNameItems: function(items, fieldLabel) {
		var me = this;
		if (!items) {
			items = [];
		}
		if (!fieldLabel) {
			fieldLabel = '库房';
		}
		items.push({
			fieldLabel: "",
			emptyText: fieldLabel + '选择',
			name: 'StorageName',
			itemId: 'StorageName',
			labelWidth: 0,
			width: 100,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
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
	/**创建一级分类选择*/
	createGoodsClassItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			emptyText: '一级分类',
			labelWidth: 0,
			width: 100,
			fieldLabel: '',
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
		});
		return items;
	},
	/**创建二级分类选择*/
	createGoodsClassTypeItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			emptyText: '二级分类',
			labelWidth: 0,
			width: 100,
			fieldLabel: '',
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
		return items;
	},
	/**创建品牌*/
	createProdOrgNameItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			name: 'ProdOrgName',
			itemId: 'ProdOrgName',
			xtype: 'uxCheckTrigger',
			emptyText: '品牌选择',
			width: 105,
			labelWidth: 0,
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				checkOne: true,
				defaultWhere: "bdict.BDictType.DictTypeCode='ProdOrg'"
			},
			listeners: {
				check: function(p, record) {
					me.onProdOrgNameAccept(p, record);
				}
			}
		}, {
			fieldLabel: '品牌ID',
			hidden: true,
			xtype: 'textfield',
			name: 'ProdId',
			itemId: 'ProdId'
		});
		return items;
	},
	/**创建试剂选择*/
	createGoodsNameItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			emptyText: '货品选择',
			name: 'GoodsName',
			itemId: 'GoodsName',
			labelWidth: 0,
			width: 105,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goods2.basic.CheckGrid',
			classConfig: {
				title: '货品选择',
				/**是否单选*/
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsCheck(p, record);
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaGoodsNo',
			name: 'ReaGoodsNo',
			fieldLabel: '货品ID',
			hidden: true
		});
		return items;
	},
	/**创建检索条件选择*/
	createSearchItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		if (me.hasSearchType) {
			items.push({
				xtype: 'uxSimpleComboBox', //
				itemId: 'cboSearch',
				margin: '0 0 0 5',
				emptyText: '检索条件选择',
				fieldLabel: '检索',
				labelWidth: 35,
				width: 130,
				value: "1",
				data: [
					["1", "按机构货品"],
					["2", "按其他条件"]
				],
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						if (newValue) {
							var buttonsToolbar = me.getComponent('buttonsToolbar1');
							var txtSearch = buttonsToolbar.getComponent('txtSearch');
							if (newValue == "2") {
								txtSearch.emptyText = '货品批号/供货商货品编码';
							} else {
								txtSearch.emptyText = '货品名称/货品编码/拼音字头';
							}
							txtSearch.applyEmptyText();
							if (txtSearch.getValue()) me.onSearch();
						}
					}
				}
			});
		}
		items.push({
			xtype: 'textSearchTrigger',
			name: 'txtSearch',
			itemId: 'txtSearch',
			emptyText: '货品名称/货品编码/拼音字头',
			width: 165,
			fieldLabel: '',
			labelWidth: 0,
			enableKeyEvents: true,
			listeners: {
				onSearchClick: function(field, value) {
					me.onSearch();
				},
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		});
		return items;
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**试剂选择*/
	onGoodsCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var ReaGoodsNo = buttonsToolbar.getComponent('ReaGoodsNo');
		if (!ReaGoodsNo) {
			p.close();
			JShell.Msg.overwrite('onGoodsAccept');
			return;
		}
		var GoodsName = buttonsToolbar.getComponent('GoodsName');
		ReaGoodsNo.setValue(record ? record.get('ReaGoods_ReaGoodsNo') : '');
		GoodsName.setValue(record ? record.get('ReaGoods_CName') : '');
		p.close();
	},
	/**库房选择*/
	onStorageCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var storageID = buttonsToolbar.getComponent('StorageID');
		if (!storageID) {
			p.close();
			JShell.Msg.overwrite('onStorageCheck');
			return;
		}
		storageID.setValue(record ? record.get('ReaStorage_Id') : '');
		var storageName = buttonsToolbar.getComponent('StorageName');
		storageName.setValue(record ? record.get('ReaStorage_CName') : '');
		p.close();
	},
	/**供应商选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = buttonsToolbar.getComponent('CompanyID');
		var CName = buttonsToolbar.getComponent('CompanyName');
		var ReaCompCode = buttonsToolbar.getComponent('ReaCompCode');
		var ReaServerCompCode = buttonsToolbar.getComponent('ReaServerCompCode');
		if (!Id) {
			p.close();
			JShell.Msg.overwrite('onCompAccept');
			return;
		}

		if (record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			return;
		}
		if (record.data) {
			var orgNo = record ? record.data.value.OrgNo : '';

			CName.setValue(record.data ? record.data.text : '');
			Id.setValue(record.data ? record.data.tid : '');
			ReaCompCode.setValue(orgNo);
			ReaServerCompCode.setValue(record.data ? record.data.value.PlatformOrgNo : '');

			p.close();
		}
	},
	/**订货方选择*/
	onLabcAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = buttonsToolbar.getComponent('LabcID');
		var CName = buttonsToolbar.getComponent('LabcName');
		var ReaServerLabcCode = buttonsToolbar.getComponent('ReaServerLabcCode');
		if (!Id) {
			p.close();
			JShell.Msg.overwrite('onLabcAccept');
			return;
		}

		if (record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			return;
		}
		if (record.data) {
			CName.setValue(record.data ? record.data.text : '');
			Id.setValue(record.data ? record.data.tid : '');
			ReaServerLabcCode.setValue(record.data ? record.value.PlatformOrgNo : '');

			p.close();
		}
	},
	/**部门选择*/
	onDeptAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = buttonsToolbar.getComponent('DeptID'),
			CName = buttonsToolbar.getComponent('DeptName');
		if (!Id) {
			p.close();
			JShell.Msg.overwrite('onDeptAccept');
			return;
		}
		if (record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			return;
		}
		var text = record.data ? record.data.text : '';
		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		Id.setValue(record.data ? record.data.tid : '');
		CName.setValue(text);
	},
	/**品牌选择*/
	onProdOrgNameAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = buttonsToolbar.getComponent('ProdId');
		if (!Id) {
			p.close();
			JShell.Msg.overwrite('onProdOrgNameAccept');
			return;
		}
		var CName = buttonsToolbar.getComponent('ProdOrgName');
		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');
		p.close();
	},
	/**获取仪器ID组件*/
	getTestEquipLabId: function(){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = buttonsToolbar.getComponent('TestEquipLabId');
		return Id;
	},
	/**获取仪器名称组件*/
	getTestEquipLabName: function(){
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var testEquipLabName= buttonsToolbar.getComponent('TestEquipLabName');
		return testEquipLabName;
	},
	/**仪器选择*/
	onTestEquipLabAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var Id = me.getTestEquipLabId();
		var CName = me.getTestEquipLabName();
		if (!Id) {
			p.close();
			JShell.Msg.overwrite('onTestEquipLabAccept');
			return;
		}
		if (record == null) {
			CName.setValue('');
			Id.setValue('');
			p.close();
			return;
		}
		CName.setValue(record.get("ReaTestEquipLab_CName"));
		Id.setValue(record.get("ReaTestEquipLab_Id"));
		p.close();
	},
	/**创建日期范围查询栏*/
	createDateAreaButtonToolbar: function() {
		var me = this;
		var items = [];

		items.push({
			xtype: 'button',
			itemId: "CurDay",
			text: '今天',
			tooltip: '按今天查',
			handler: function(button, e) {
				me.onQuickSearch(0, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day10",
			text: '10天内',
			tooltip: '按10天查',
			handler: function(button, e) {
				me.onQuickSearch(-10, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day20",
			text: '20天内 ',
			tooltip: '按20天查',
			handler: function(button, e) {
				me.onQuickSearch(-20, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day30",
			text: '30天内',
			tooltip: '按30天查',
			handler: function(button, e) {
				me.onQuickSearch(-30, button);
			}
		}, {
			xtype: 'button',
			itemId: "Day60",
			text: '60天内',
			tooltip: '按60天查',
			handler: function(button, e) {
				me.onQuickSearch(-60, button);
			}
		});
		items.push({
			xtype: 'uxdatearea',
			itemId: 'date',
			labelWidth: 55,
			labelAlign: 'right',
			fieldLabel: '日期范围',
			listeners: {
				enter: function() {
					me.onSearch();
				}
			}
		}, {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});

		items.push("-");
		items = me.createReportClassButtonItem(items);
		items = me.createTemplateButtonItem(items);
		items = me.createPrintButtonItems(items);

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'dateareaToolbar',
			items: items
		});
	},
	/**按日期按钮点击后样式设置*/
	setButtonDayToggle: function(button) {
		var me = this;
		var buttonsToolbar = me.getComponent('dateareaToolbar');

		var items = buttonsToolbar.items.items;
		Ext.Array.forEach(items, function(item, index) {
			if (item && item.xtype == "button") item.toggle(false);
		});

		button.toggle(true);
	},
	/**验证日期类型是否选择*/
	validDateType: function() {
		return true;
	},
	/**@description 设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateAreaValue) date.setValue(dateAreaValue);
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		//sdate=Ext.Date.format(sdate,"Y-m-d");
		//edate=Ext.Date.format(edate,"Y-m-d");
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if (!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
		if (date && dateArea) date.setValue(dateArea);
	},
	/**按传入的天数进行默认点击查询*/
	initSearchDate: function(days) {
		var me = this;
		var buttonsToolbar = me.getComponent('dateareaToolbar');
		var btn = null;
		if (buttonsToolbar) {
			var itemId = "CurDay";
			switch (days) {
				case 0:
					itemId = "CurDay";
					break;
				case -10:
					itemId = "Day10";
					break;
				case -20:
					itemId = "Day20";
					break;
				case -30:
					itemId = "Day30";
					break;
				case -60:
					itemId = "Day60";
					break;
				default:
					break;
			}
			btn = buttonsToolbar.getComponent(itemId);
		}
		if (btn)
			me.onQuickSearch(days, btn);
	},
	/**按日期快捷查询*/
	onQuickSearch: function(day, button) {
		var me = this;
		if (!me.validDateType()) return;
		me.onSetDateArea(day);
		me.setButtonDayToggle(button);
		me.onSearch();
	},
	/**模板分类选择项*/
	createReportClassButtonItem: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			xtype: 'uxSimpleComboBox',
			itemId: 'ReportClass',
			labelWidth: 0,
			width: 85,
			value: me.reaReportClass,
			data: [
				["", "请选择"],
				["Frx", "PDF预览"],
				["Excel", "Excel导出"]
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onReportClassCheck(newValue);
				}
			}
		});
		return items;
	},
	/**模板选择项*/
	createTemplateButtonItem: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 160,
			name: 'cboTemplate',
			itemId: 'cboTemplate',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.template.CheckGrid',
			classConfig: {
				width: 195,
				height: 460,
				checkOne: true,
				/**BReportType:7*/
				breportType: me.breportType,
				/**模板分类:Excel模板,Frx模板*/
				publicTemplateDir: me.publicTemplateDir
			},
			listeners: {
				check: function(p, record) {
					me.onTemplateCheck(p, record);
				}
			}
		});
		return items;
	},
	/**创建功能按钮栏Items*/
	createPrintButtonItems: function(items) {
		var me = this;
		if (!items) {
			items = [];
		}
		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnPrint",
			text: 'PDF预览',
			tooltip: '预览PDF清单',
			handler: function() {
				me.onPrintClick();
			}
		}, '-');
		items.push({
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			width: 60,
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});
		return items;
	},
	/**@description 报告模板分类选择后*/
	onReportClassCheck: function(newValue) {
		var me = this;
		me.reaReportClass = newValue;
		me.pdfFrx = "";
		if (me.reaReportClass == "Frx") {
			me.publicTemplateDir = "Frx模板";
		} else if (me.reaReportClass == "Excel") {
			me.publicTemplateDir = "Excel模板";
		}
		var buttonsToolbar = me.getComponent("dateareaToolbar");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		cbo.setValue("");
		cbo.classConfig["publicTemplateDir"] = me.publicTemplateDir;
		var picker = cbo.getPicker();
		if (picker) {
			picker["publicTemplateDir"] = me.publicTemplateDir;
			cbo.getPicker().load();
		}
	},
	onTemplateCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent("dateareaToolbar");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		var cname = "";
		if (record) {
			me.pdfFrx = record.get("FileName");
			cname = record.get("CName");
		} else {
			me.pdfFrx = "";
		}
		if (cbo) {
			cbo.setValue(cname);
		}
		p.close();
	},
	/**获取主单查询条件*/
	getParamsDocHql: function() {
		//JShell.Msg.overwrite('getParamsDocHql');
		return "";
	},
	/**获取明细查询条件*/
	getParamsDtlHql: function() {
		//JShell.Msg.overwrite('getParamsDtlHql');
		return "";
	},
	/**获取机构货品查询条件*/
	getReaGoodsHql: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var goodsClass = buttonsToolbar.getComponent('GoodsClass');
		var goodsClassType = buttonsToolbar.getComponent('GoodsClassType');
		var cboSearch = buttonsToolbar.getComponent('cboSearch');
		var txtSearch = buttonsToolbar.getComponent('txtSearch');

		var reaGoodsHql = [];
		if (goodsClass && goodsClass.getValue()) {
			reaGoodsHql.push("reagoods.GoodsClass='" + goodsClass.getValue() + "'");
		}
		if (goodsClassType && goodsClassType.getValue()) {
			reaGoodsHql.push("reagoods.GoodsClassType='" + goodsClassType.getValue() + "'");
		}
		if (cboSearch) {
			cboSearch = cboSearch.getValue();
		} else {
			cboSearch = "1";
		}
		if (txtSearch) {
			txtSearch = txtSearch.getValue();
			if (txtSearch && cboSearch == "1") {
				reaGoodsHql.push("(reagoods.PinYinZiTou like '%" + txtSearch.toUpperCase() + "%' or reagoods.CName like'%" +
					txtSearch + "%'" +
					" or reagoods.ReaGoodsNo like'%" + txtSearch + "%')");
			}
		}
		if (reaGoodsHql && reaGoodsHql.length > 0) {
			reaGoodsHql = reaGoodsHql.join(" and ");
		} else {
			reaGoodsHql = "1=1";
		}
		return reaGoodsHql;
	},
	/**获取日期范围值*/
	getDateAreaValue: function() {
		var me = this;
		var dateareaToolbar = me.getComponent('dateareaToolbar');
		var dateArea = dateareaToolbar.getComponent('date');
		if (dateArea && dateArea.getValue()) {
			var date = dateArea.getValue();
			if (date.start) date.start = Ext.Date.format(date.start, "Y-m-d");
			if (date.end) date.end = Ext.Date.format(date.end, "Y-m-d");
			return date;
		} else {
			return {
				"start": "",
				"end": ""
			};
		}
	},
	/**@description 获取公共查询参数*/
	getSearchParams: function() {
		var me = this;
		var docHql = me.getParamsDocHql();
		var dtlHql = me.getParamsDtlHql();
		var reaGoodsHql = me.getReaGoodsHql();
		if (!docHql) docHql = "";
		if (!dtlHql) dtlHql = "";
		if (!reaGoodsHql) reaGoodsHql = "";
		if (!docHql && !dtlHql) {
			JShell.Msg.error("请先选择统计条件后再操作!");
			return [];
		}
		var startDate = "",
			endDate = "";
		var dateAre = me.getDateAreaValue();
		startDate = dateAre.start || "";
		endDate = dateAre.end || "";

		var params = [];
		params.push("reaReportClass=" + me.reaReportClass);
		params.push("breportType=" + me.breportType);
		params.push("frx=" + JShell.String.encode(me.pdfFrx));
		params.push("groupType=1");
		params.push("startDate=" + startDate);
		params.push("endDate=" + endDate);
		if (docHql)
			params.push("docHql=" + docHql);
		if (dtlHql)
			params.push("dtlHql=" + JShell.String.encode(dtlHql));
		if (reaGoodsHql)
			params.push("reaGoodsHql=" + JShell.String.encode(reaGoodsHql));

		var curOrderBy = me.curOrderBy;
		if (curOrderBy.length <= 0 && me.defaultOrderBy && me.defaultOrderBy.length > 0)
			curOrderBy = me.defaultOrderBy;
		params.push("sort=" + JShell.JSON.encode(curOrderBy));
		return params;
	},
	/**@description 选择某一试剂耗材订单,预览PDF清单*/
	onPrintClick: function() {
		var me = this,
			operateType = '1';
		if (!me.reaReportClass || me.reaReportClass != "Frx") {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		if (!me.pdfFrx) {
			JShell.Msg.error("请先选择Frx模板后再操作!");
			return;
		}
		var params = me.getSearchParams();
		if (!params || params.length <= 0) return;
		params.push("operateType=" + operateType);
		var url = JShell.System.Path.getRootUrl(
			"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchBusinessSummaryReportOfPdfByHql");
		url += "?" + params.join("&");
		window.open(url);
	},
	/**选择某一试剂耗材订单,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';
		if (!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		if (!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}

		var params = me.getSearchParams();
		if (!params || params.length <= 0) return;
		params.push("operateType=" + operateType);
		var url = JShell.System.Path.getRootUrl(
			"/ReaStatisticalAnalysisService.svc/RS_UDTO_SearchBusinessSummaryReportOfExcelByHql");
		url += "?" + params.join("&");
		window.open(url);
	}
});
