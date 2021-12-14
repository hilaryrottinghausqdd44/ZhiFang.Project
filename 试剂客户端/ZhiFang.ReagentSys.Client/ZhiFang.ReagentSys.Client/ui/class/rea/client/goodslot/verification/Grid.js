/**
 * 货品批号性能验证
 * @author liangyl
 * @version 2018-12-26
 */
Ext.define('Shell.class.rea.client.goodslot.verification.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.DateArea'
	],
	title: '货品批号性能验证',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchReaGoodsLotByAllJoinHql?isPlanish=true',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**是否启用新增按钮*/
	hasAdd: false,
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{
			property: 'ReaGoodsLot_ReaGoodsNo',
			direction: 'ASC'
		},
		{
			property: 'ReaGoodsLot_InvalidDate',
			direction: 'ASC'
		}
	],
	/**验证状态Key*/
	ReaGoodsLotVerificationStatus: 'ReaGoodsLotVerificationStatus',
	/**用户UI配置Key*/
	userUIKey: 'goodslot.verification.Grid',
	/**用户UI配置Name*/
	userUIName: "货品批号性能验证列表",

	/**PDF报表模板*/
	pdfFrx: null,
	/**业务报表类型:对应BTemplateType枚举的key*/
	breportType: 21,
	/**模板/报表类型:Frx;Excel*/
	reaReportClass: "Excel",
	/**模板分类:Excel模板,Frx模板*/
	publicTemplateDir: "Excel模板",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initDateArea(-7);
		me.onSearch();
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			value: me.CurLotNo,
			itemId: 'Search',
			emptyText: '货品编码/货品名称/批号',
			fields: ['reagoodslot.ReaGoodsNo', 'reagoodslot.GoodsCName', 'reagoodslot.LotNo']
		};
		JShell.REA.StatusList.getStatusList(me.ReaGoodsLotVerificationStatus, false, true, null);
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsLot_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_GoodsCName',
			text: '货品名称',
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_LotNo',
			text: '批号',
			width: 120,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_DataAddTime',
			text: '登记时间',
			width: 145,
			type: 'date',
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_ProdDate',
			text: '生产日期',
			width: 90,
			type: 'date',
			isDate: true
		}, {
			dataIndex: 'ReaGoodsLot_InvalidDate',
			text: '有效期',
			width: 90,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_Id',
			text: '主键ID',
			hidden: true,
			isKey: true
		}, {
			dataIndex: 'ReaGoodsLot_IsNeedPerformanceTest',
			text: '是否性能验证',
			width: 80,
			align: 'center',
			hidden: true,
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_VerificationStatus',
			width: 80,
			text: '验证状态',
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].Enum != null)
					v = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].FColor[value];
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
			dataIndex: 'ReaGoodsLot_VerificationUserName',
			text: '验证人',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_VerificationUserId',
			text: '验证人ID',
			width: 100,
			hidden: true
		}, {
			dataIndex: 'ReaGoodsLot_VerificationTime',
			text: '验证时间',
			width: 100,
			type: 'date',
			isDate: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_IncreaseAppearance',
			text: '外观',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_SterilityTest',
			text: '无菌试验',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_ParallelTest',
			text: '平行试验',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_GrowthTest',
			text: '生长试验',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_ComparisonTest',
			text: '留样比对试验',
			width: 85,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_VerificationContent',
			text: '验证结果',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsLot_Visible',
			text: '启用',
			width: 50,
			hidden: true,
			align: 'center',
			type: 'bool',
			isBool: true,
			defaultRenderer: true
		}];
		return columns;
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			buttonsToolbar = me.callParent(arguments);
		buttonsToolbar.border = false;
		return buttonsToolbar;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push(me.createDataTypeToolbarItems());
		items.push(me.createButtonToolbar1Items());
		return items;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			emptyText: '一级分类',
			labelWidth: 0,
			width: 105,
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
			width: 105,
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
		var list = JShell.REA.StatusList.Status[me.ReaGoodsLotVerificationStatus].List;
		items.push({
			fieldLabel: '',
			emptyText: '验证状态',
			labelWidth: 0,
			width: 105,
			hasStyle: true,
			hasAll: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'scbVerificationStatus',
			name: 'scbVerificationStatus',
			value: "1",
			data: list,
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			style: {
				marginLeft: "5px"
			},
			handler: function() {
				me.onSearch();
			}
		});
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var items = [];
		items.push({
			fieldLabel: '',
			labelWidth: 0,
			width: 80,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'dateType',
			value: "reagoodslot.DataAddTime",
			data: [
				["", "请选择"],
				["reagoodslot.DataAddTime", "登记日期"],
				["reagoodslot.InvalidDate", "有效日期"]
			],
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
					var dateareaToolbar = me.getComponent('buttonsToolbar1');
					var date = dateareaToolbar.getComponent('date');
					if(!records[0].data.value)
						date.disable();
					else
						date.enable();
				}
			}
		},{
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 255,
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
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		}, '-');
		items = me.createTemplate(items);
		items.push({
			text: '导出',
			tooltip: 'EXCEL导出',
			iconCls: 'file-excel',
			xtype: 'button',
			name: 'EXCEL',
			itemId: 'EXCEL',
			handler: function() {
				me.onDownLoadExcel();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar1',
			items: items
		});
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		var whereHql = me.getWhereHql();
		if(whereHql) url += ('&where=' + JShell.String.encode(whereHql));
		var reaGoodsHql = me.getReaGoodsHql();
		if(reaGoodsHql) url += ('&reaGoodsHql=' + JShell.String.encode(reaGoodsHql));
		return url;
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
	/**库存查询条件*/
	getWhereHql: function() {
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

		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			verificationStatus = buttonsToolbar.getComponent('scbVerificationStatus').getValue();
		//验证状态	
		if(verificationStatus) {
			arr.push('reagoodslot.VerificationStatus=' + verificationStatus);
		}
		var buttonsToolbar1 = me.getComponent('buttonsToolbar1');
		var date = buttonsToolbar1.getComponent('date');
		var dateType = buttonsToolbar1.getComponent('dateType');
		if(date) {
			var dateValue = date.getValue();
			var dateTypeValue = dateType.getValue();
			if(dateValue && dateTypeValue) {
				if(dateValue.start) {
					arr.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if(dateValue.end) {
					arr.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) +
						"'");
				}
			}
		}
		//默认只显示"是否性能验证"等于是的库存货品
		arr.push('reagoodslot.IsNeedPerformanceTest=1');
		var where = "";
		if(arr && arr.length > 0) where = arr.join(") and (");
		if(where) where = "(" + where + ")";
		return where;
	},
	/**模板选择项*/
	createTemplate: function(items) {
		var me = this;
		if(!items) {
			items = [];
		}
		items.push({
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 135,
			name: 'cboTemplate',
			itemId: 'cboTemplate',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 195,
				height: 460,
				checkOne: true,
				breportType: me.breportType,
				/**模板分类:Excel模板,Frx模板*/
				publicTemplateDir: me.publicTemplateDir
			},
			className: 'Shell.class.rea.client.template.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onTemplateCheck(p, record);
				}
			}
		});
		return items;
	},
	onTemplateCheck: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar1");
		var cbo = buttonsToolbar.getComponent("cboTemplate");
		var cname = "";
		if(record) {
			me.pdfFrx = record.get("FileName");
			cname = record.get("CName");
		}
		if(cbo) {
			cbo.setValue(cname);
		}
		p.close();
	},
	/**选择试剂耗材信息,导出EXCEL*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0';

		if(!me.reaReportClass || me.reaReportClass != "Excel") {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}
		if(!me.pdfFrx) {
			JShell.Msg.error("请先选择Excel模板后再操作!");
			return;
		}

		//查询条件
		var whereHql = me.getWhereHql();
		//机构货品查询条件
		var reaGoodsHql = me.getReaGoodsHql();
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_SearchReaGoodsLotOfExcelByHql");
		var params = [];

		params.push("operateType=" + operateType);
		params.push("breportType=" + me.breportType);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		params.push("where=" + whereHql);
		params.push("reaGoodsHql=" + reaGoodsHql);
		if(me.pdfFrx) {
			params.push("frx=" + JShell.String.encode(me.pdfFrx));
		}
		var curOrderBy = me.curOrderBy;
		if(curOrderBy.length <= 0 && me.defaultOrderBy && me.defaultOrderBy.length > 0)
			curOrderBy = me.defaultOrderBy;
		params.push("sort=" + JShell.JSON.encode(curOrderBy));
		url += "?" + params.join("&");
		//console.log(url);
		window.open(url);
	},
	/**验证日期类型是否选择*/
	validDateType: function() {
		var me = this;
		var dateareaToolbar = me.getComponent('buttonsToolbar1'),
			dateType = dateareaToolbar.getComponent('dateType');
		if(!dateType.getValue()) {
			JShell.Msg.alert("请选择日期类型后再查询!", null, 1000);
			dateType.focus();
			return false;
		}
		return true;
	},
	/**日期范围按钮*/
	createDataTypeToolbarItems: function() {
		var me = this;
		var items = [];
	
		items.push({
			xtype: 'button',
			itemId: "CurDay",
			text: '今天',
			tooltip: '按当天查',
			handler: function() {
				me.onSetDateArea(0);
			}
		}, {
			xtype: 'button',
			itemId: "Day3",
			text: '3天内',
			tooltip: '按近3天查',
			handler: function() {
				me.onSetDateArea(-3);
			}
		}, {
			xtype: 'button',
			itemId: "Day7",
			text: '7天内',
			tooltip: '按近7天查',
			handler: function() {
				me.onSetDateArea(-7);
			}
		}, {
			xtype: 'button',
			itemId: "Day15",
			text: '15天内',
			tooltip: '按近15天查',
			handler: function() {
				me.onSetDateArea(-15);
			}
		}, {
			xtype: 'button',
			itemId: "Day30",
			text: '30天内',
			tooltip: '按近30天查	',
			handler: function() {
				me.onSetDateArea(-30);
			}
		}, {
			xtype: 'button',
			itemId: "Day60",
			text: '60天内',
			tooltip: '按近60天查',
			handler: function() {
				me.onSetDateArea(-60);
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar3',
			items: items
		});
	
	},
	/**@description 设置日期范围值*/
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('buttonsToolbar1'),
			date = dateareaToolbar.getComponent('date');
		if(!me.validDateType()) return;
		if(dateareaToolbar) {
			var itemId = "CurDay";
			switch(day) {
				case 0:
					itemId = "CurDay";
					break;
				case -3:
					itemId = "Day3";
					break;
				case -7:
					itemId = "Day7";
					break;
				case -15:
					itemId = "Day15";
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
			var buttonsToolbar3 = me.getComponent('buttonsToolbar3'),
				btn = buttonsToolbar3.getComponent(itemId);
			if(btn){
				/**按日期按钮点击后样式设置*/
				var items = buttonsToolbar3.items.items;
				Ext.Array.forEach(items, function(item, index) {
					if(item && item.xtype == "button") item.toggle(false);
				});
			
				btn.toggle(true);
			}
		}
		if(date && dateAreaValue) date.setValue(dateAreaValue);
		me.onSearch();
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	/**初始化日期范围*/
	initDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		me.onSetDateArea(day);
	}
});