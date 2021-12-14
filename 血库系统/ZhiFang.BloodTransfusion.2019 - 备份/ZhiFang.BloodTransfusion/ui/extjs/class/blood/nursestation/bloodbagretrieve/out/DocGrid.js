/**
 * 护士站--血袋回收--发血主单
 * @author longfc
 * @version 2020-03-13
 */
Ext.define('Shell.class.blood.nursestation.bloodbagretrieve.out.DocGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.DateArea'
	],
	title: '发血记录信息',

	/**默认加载*/
	defaultLoad: false,	
	/**是否启用刷新按钮*/
	hasRefresh:false,
	/**是否启用查询框*/
	hasSearch:false,
	hasPagingtoolbar: true,
	autoSelect: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL?isPlanish=true',
	/**只能获取到可配置的系统参数*/
	defaultWhere: "",
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBOutForm_OperTime',
		direction: 'DESC'
	}, {
		property: 'BloodBOutForm_Id',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initDateArea(-7);
		me.onSearch();
	},
	/**His调用传入的就诊号*/
	AdmId: "", 
	/**用户UI配置Key*/
	userUIKey: 'bloodprohandover.out.DocGrid',
	/**用户UI配置Name*/
	userUIName: "发血记录信息",
	
	initComponent: function() {
		var me = this;
		me.addEvents('onAddTrans');
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.callParent(arguments);
		items.push(me.createDataTypeToolbarItems());
		items.push(me.createDateAreaToolbarItems());
		items.push(me.createButtonToolbarItems2());
		return items;
	},
	/**日期范围按钮*/
	createDataTypeToolbarItems: function() {
		var me = this;
		var items = [];
	
		items.push({
			xtype: 'button',
			text: '今天',
			tooltip: '按当天查',
			handler: function() {
				me.onSetDateArea(0);
			}
		}, {
			xtype: 'button',
			text: '10天内',
			tooltip: '按近10天查',
			handler: function() {
				me.onSetDateArea(-10);
			}
		}, {
			xtype: 'button',
			text: '20天内',
			tooltip: '按近20天查',
			handler: function() {
				me.onSetDateArea(-20);
			}
		}, {
			xtype: 'button',
			text: '30天内',
			tooltip: '按近30天查	',
			handler: function() {
				me.onSetDateArea(-30);
			}
		}, {
			xtype: 'button',
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
	/**创建功能 按钮栏*/
	createDateAreaToolbarItems: function() {
		var me = this;
		var items = [];
	
		items.push({
			xtype: 'button',
			iconCls: 'button-add',
			text: '登记',
			tooltip: '对选中的第血袋进行新增登记',
			listeners: {
				click: function(but) {
					me.onAddTrans();
				}
			}
		},'-',{
			xtype: 'uxdatearea',
			itemId: 'date',
			width: 255,
			labelWidth: 65,
			labelAlign: 'right',
			fieldLabel: "发血日期",
			listeners: {
				enter: function() {
					me.onSearch();
				},
				change: function(com, newValue, oldValue, eOpts) {
					//me.onSearch();
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'dateareaToolbar',
			items: items
		});
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems2: function() {
		var me = this;
		var items = [];
		//查询框信息
		me.searchInfo = {
			width: 145,
			itemId:"search",
			emptyText: '发血单号/病历号/姓名',
			isLike: true,
			fields: ['bloodboutform.Id', 'bloodboutform.BloodBReqForm.PatNo', 'bloodboutform.BloodBReqForm.CName']
		};
		if (!items) items = [];
		items.push({
			type: 'search',
			info: me.searchInfo
		});
		items.push({
			fieldLabel: '',
			name: 'RecoverCompletion',
			itemId: 'RecoverCompletion',
			xtype: 'uxSimpleComboBox',
			width: 105,
			labelWidth: 0,
			value: '',
			emptyText: '',
			hasStyle: true,
			data: [
				['', '请选择', 'color:black;'],
				['1', '未回收', 'color:#FFB800;'],
				['2', '部分回收', 'color:#00BFFF;'],
				['3', '回收完成', 'color:#009688;']
			],
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			listeners: {
				click: function(but) {
					me.onSearch();
				}
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},	
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '发血单号',
			dataIndex: 'BloodBOutForm_Id',
			width: 160,
			isKey: true,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '回收完成度',
			dataIndex: 'BloodBOutForm_RecoverCompletion',
			width: 75,
			renderer: function(value, meta) {
				var v = "";
				if (value == "2") {
					v = "部分回收";
					meta.style = "background-color:#00BFFF;color:#ffffff;";
				} else if (value == "3") {
					v = "回收完成";
					meta.style = "background-color:#009688;color:#ffffff;";
				} else {
					v = "未回收";
					meta.style = "background-color:#FFB800;color:#ffffff;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text: '姓名',
			dataIndex: 'BloodBOutForm_BloodBReqForm_CName',
			width: 80,
			sortable: true,
			defaultRenderer: true
		}, {
			text: '病历号',
			dataIndex: 'BloodBOutForm_BloodBReqForm_PatNo',
			width: 80,
			defaultRenderer: true
		}, {
			text: '就诊号',
			dataIndex: 'BloodBOutForm_BloodBReqForm_AdmID',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '性别',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Sex',
			width: 60,
			defaultRenderer: true
		}, {
			text: '出生日期',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Birthday',
			width: 90,
			isDate: true,
			hasTime: false,
			defaultRenderer: true
		},{
			text: '年龄',
			dataIndex: 'BloodBOutForm_BloodBReqForm_AgeALL',
			width: 50,
			hidden:true,
			defaultRenderer: true
		}, {
			text: '床号',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Bed',
			width: 60,
			defaultRenderer: true
		}, {
			text: '输血史',
			dataIndex: 'BloodBOutForm_BloodBReqForm_BeforUse',
			width: 60,
			defaultRenderer: true
		}, {
			text: '发血时间',
			dataIndex: 'BloodBOutForm_OperTime',
			width: 145,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '诊断',
			dataIndex: 'BloodBOutForm_BloodBReqForm_Diag',
			width: 80,
			defaultRenderer: true
		}];
		return columns;
	},
	onSetDateArea: function(day) {
		var me = this;
		var dateAreaValue = me.calcDateArea(day);
		var dateareaToolbar = me.getComponent('dateareaToolbar'),
			date = dateareaToolbar.getComponent('date');
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		me.internalWhere = params.join(" and ");
		return me.callParent(arguments);
	},
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');

		var recoverCompletion = buttonsToolbar.getComponent('RecoverCompletion');
		var dateareaToolbar = me.getComponent('dateareaToolbar');
		var	date = buttonsToolbar.getComponent('date');
		var params = [];
		if(me.AdmId){
			params.push("bloodbreqform.AdmID='"+me.AdmId+"'");
		}else{
			//按科室过滤
			var deptNo = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
			if (deptNo) {
				params.push("bloodbreqform.DeptNo=" + deptNo);
			}
		}
		if (recoverCompletion) {
			var value = recoverCompletion.getValue();
			if (value) {
				if (value == "1") {//未回收
					params.push("(bloodboutform.RecoverCompletion=0 or bloodboutform.RecoverCompletion=1 or bloodboutform.RecoverCompletion is null)");
				} else {
					params.push("bloodboutform.RecoverCompletion=" + value);
				}
			}
		}
		if (date) {
			var dateValue = date.getValue();
			var dateTypeValue = "bloodboutform.OperTime";//发血时间
			if (dateValue && dateTypeValue) {
				if (dateValue.start) {
					params.push(dateTypeValue + ">='" + JShell.Date.toString(dateValue.start, true) + " 00:00:00'");
				}
				if (dateValue.end) {
					params.push(dateTypeValue + "<'" + JShell.Date.toString(JShell.Date.getNextDate(dateValue.end), true) + "'");
				}
			}
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				params.push("(" + me.getSearchWhere(value) + ")");
			}
		}
		return params;
	},
	onAddTrans: function() {
		var me = this;
		me.fireEvent('onAddTrans', me);
	}
});
