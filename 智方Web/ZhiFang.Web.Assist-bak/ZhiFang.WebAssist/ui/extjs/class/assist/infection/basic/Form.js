/**
 * 环境监测送检样本登记
 * @author longfc
 * @version 2020-11-09
 */
Ext.define('Shell.class.assist.infection.basic.Form', {
	extend: 'Shell.class.assist.infection.basic.ItemsForm',

	title: '环境监测送检样本登记信息',
	width: 720,
	height: 190,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormAndDtlById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_AddGKSampleRequestFormAndDtl',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKSampleRequestFormAndDtlByField',

	formtype: 'show',
	/**带功能按钮栏*/
	hasButtontoolbar: false,

	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 195,
		labelAlign: 'right'
	},
	/**通过指定字段(如工号等)获取RBACUser(PUser转换)*/
	fieldKey: "ShortCode",
	//人员选择是否从集成平台获取
	userIsGetLimp: false,
	//人员选择输入工号后是否按下了回车键触发
	isEnterKeyPress: false,
	//获取所有人员基本信息，方便验证核对人录入信息是否正常
	puseList: [],

	/**当前选择的监测类型Id*/
	SCRecordTypeId: "",
	/**当前样品信息结果值*/
	SampleItemsVal: null,

	/**一维条码模板信息*/
	BarcodeModel: null,
	/**一维条码模板集合信息*/
	BarcodeModelList: [],
	/**默认条码模板值*/
	DefaultBarcodeModel: "128C5525",
	/**默认选择的打印机*/
	DefaultPrinter: "",
	/**注入作条码打印使用*/
	GridPanel: null,
	/**是否登录科室*/
	ISCURDEPT: false,

	itemStyle: {
		fontSize: "18px"
	},
	/**默认采样时间*/
	SampleTimeValue: "07:30",
	/**上一次选择的采样时间*/
	LastSampleTimeValue: "07:30",
	/**上一次选择的监测类型*/
	LastRecordTypeValue: "",

	/**1:感控监测;2:科室监测;*/
	MonitorType: "2",

	afterRender: function() {
		var me = this;

		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.addEvents('isEditAfter');
		//加载一维条码模板组件
		me.BarCodeModel = me.BarCodeModel || Ext.create('Shell.class.assist.printbarcode.gbbarcode.BarCodeModel');
		me.BarcodeModelList = me.BarCodeModel.getModelList();

		if (!me.width) me.width = 760;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if (me.defaults.width < 195) me.defaults.width = 195;

		me.initDefaultInfo();
		me.initDeptPhrase(function() {
			me.getSampleItemList(function() {
				me.items = me.createItems();
			});
		});

		me.callParent(arguments);
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		me.createPrinterList();
		var items = [];

		if (me.hasAdd) {
			items.push({
				xtype: 'button',
				iconCls: 'button-add',
				text: '新增',
				tooltip: '新增',
				handler: function() {
					me.onAddClick();
				}
			});
		}

		if (me.hasSave) {
			items.push({
				xtype: 'button',
				iconCls: 'button-save',
				text: '暂存',
				tooltip: '暂存',
				itemId:"btnTempSave",
				handler: function() {
					me.onTempSave();
				}
			});
			items.push({
				xtype: 'button',
				iconCls: 'button-accept',
				text: '确认提交',
				tooltip: '确认提交',
				itemId:"btnSubmitted",
				handler: function() {
					me.onSubmitted();
				}
			});
			items.push({
				xtype: 'button',
				iconCls: 'button-accept',
				text: '批量确认',
				tooltip: '批量确认',
				itemId:"btnBatchSubmit",
				handler: function() {
					//me.onbatchSubmit();
					me.fireEvent('onBatchSubmit', me);
				}
			});
		}

		items.push('-', {
			text: '作废',
			tooltip: '作废',
			//hidden:true,
			iconCls: 'button-del',
			xtype: 'button',
			handler: function() {
				me.fireEvent('onObsolete', me);
			}
		});

		items.push('-', {
			fieldLabel: '',
			emptyText: '打印机选择',
			xtype: 'uxSimpleComboBox',
			itemId: 'PrinterList',
			width: 170,
			labelWidth: 0,
			labelAlign: 'right',
			data: me.PrinterList,
			value: me.DefaultPrinter,
			listeners: {
				change: function(field, newValue) {
					me.setDefaultPrinter(newValue);
				}
			}
		}, {
			fieldLabel: '',
			emptyText: '条码模板',
			xtype: 'uxSimpleComboBox',
			itemId: 'ModelType',
			width: 150,
			labelWidth: 0,
			labelAlign: 'right',
			data: me.BarcodeModelList,
			value: me.DefaultBarcodeModel,
			listeners: {
				change: function(field, newValue) {
					me.setDefaultBarcodeModel(newValue);
				}
			}
		});

		items.push({
			xtype: 'button',
			iconCls: 'button-print',
			text: '条码打印',
			tooltip: '条码打印',
			handler: function() {
				me.onBarcodePrint(1);
			}
		});

		if (me.hasJudgment) {
			items.push('-', {
				xtype: 'button',
				iconCls: 'button-check',
				text: '合格',
				tooltip: '合格',
				handler: function() {
					me.onTempSave();
				}
			});
			items.push({
				xtype: 'button',
				iconCls: 'button-uncheck',
				text: '不合格',
				tooltip: '不合格',
				handler: function() {
					me.onSubmitted();
				}
			});
			items.push('-', {
				xtype: 'button',
				iconCls: 'button-check',
				text: '归档',
				tooltip: '归档',
				handler: function() {
					me.onSubmitted();
				}
			});
		}

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar1',
			border: 1,
			items: items
		});
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var items = me.callParent(arguments);
		//var items = [];
		items.push(me.createButtonToolbar1Items());
		return items;
	},
	/**新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.isAdd();
	},
	/**条码打印*/
	onBarcodePrint: function(printType) {
		var me = this;
		//模板类型
		var modelType = me.getComponent('buttonsToolbar1').getComponent('ModelType').getValue();
		if (!modelType) {
			JShell.Msg.error("请选择打印模板类型后再操作!");
			return;
		}

		me.fireEvent('onBarcodePrint', printType, modelType);
	},
	/**@desc 创建内部组件*/
	createSampleDocInfo: function() {
		var me = this;
		var items = [{
				fieldLabel: '申请单Id',
				xtype: 'textfield',
				name: 'GKSampleRequestForm_Id',
				itemId: 'GKSampleRequestForm_Id',
				hidden: true,
				locked: true,
				readOnly: true
			},
			{
				fieldLabel: '样本状态',
				xtype: 'textfield',
				hidden: true,
				name: 'GKSampleRequestForm_StatusID',
				itemId: 'GKSampleRequestForm_StatusID'
			},
			{
				fieldLabel: '监测类型的记录项集合信息',
				xtype: 'textfield',
				hidden: true,
				name: 'GKSampleRequestForm_DtlJArray',
				itemId: 'GKSampleRequestForm_DtlJArray'
			}
		];

		items = me.createDeptItem(items);
		items = me.createSamplerItem(items);

		items.push({
			fieldLabel: '登记日期',
			name: 'GKSampleRequestForm_DataAddTime',
			itemId: 'GKSampleRequestForm_DataAddTime',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '采样日期',
			name: 'GKSampleRequestForm_SampleDate',
			itemId: 'GKSampleRequestForm_SampleDate',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '采样时间',
			name: 'GKSampleRequestForm_SampleTime',
			itemId: 'GKSampleRequestForm_SampleTime',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'timefield',
			format: "H:i",
			value: me.SampleTimeValue, //"07:30",
			colspan: 1,
			width: me.defaults.width * 1,
			listeners: {
				change: function(field, newValue, oldValue, e) {
					me.LastSampleTimeValue = newValue;
				}
			}
		});

		return items;
	},
	/**
	 * @description 创建科室
	 * @param {Object} items
	 */
	createDeptItem: function(items) {
		var me = this;
		if (!items) items = [];
		items.push({
			fieldLabel: '科室',
			xtype: 'uxCheckTrigger',
			colspan: 1,
			name: 'GKSampleRequestForm_DeptCName',
			itemId: 'GKSampleRequestForm_DeptCName',
			emptyText: '必填项',
			allowBlank: false,
			width: me.defaults.width * 1,
			editable: me.ISCURDEPT == true ? false : true,
			locked: me.ISCURDEPT == true ? true : false,
			readOnly: me.ISCURDEPT == true ? true : false,
			style: {
				fontSize: me.itemStyle.fontSize
			},
			className: 'Shell.class.sysbase.department.CheckGrid',
			classConfig: {
				title: '科室选择',
				checkOne: true
			},
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入编号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						me.onGetDept(field);
					}
				},
				beforetriggerclick: function(p) {
					var cname = me.getComponent('GKSampleRequestForm_DeptCName');
					if (cname) {
						p.changeClassConfig({
							searchInfoVal: cname.getValue()
						});
					}
					return true;
				},
				check: function(p, record) {
					var data = "";
					if (record) data = record.data;
					me.onDepCheck(p, data);
				}
			}
		}, {
			fieldLabel: '科室Id',
			hidden: true,
			xtype: "textfield",
			name: 'GKSampleRequestForm_DeptId',
			itemId: 'GKSampleRequestForm_DeptId'
		});
		return items;
	},
	createSamplerItem: function(items) {
		var me = this;
		if (!items) items = [];
		items.push({
			fieldLabel: '采样者',
			name: 'GKSampleRequestForm_Sampler',
			itemId: 'GKSampleRequestForm_Sampler',
			emptyText: '输入账号后按回车',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1,
			editable: true,
			style: {
				fontSize: me.itemStyle.fontSize
			},
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.deptuser.CheckApp',
			classConfig: {
				title: '采样者选择',
				width: 520,
				checkOne: true
			},
			listeners: {
				//获取焦点时触发
				focus: function(field, e, eOpts) {
					//获取焦点时重置
					me.isEnterKeyPress = false;
				},
				//失去焦点时触发
				blur: function(field, e, eOpts) {
					if (!field.getValue()) return;

					if (me.isEnterKeyPress == false) {
						var value1 = me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_SamplerId').getValue();
						if (!value1) {
							//从PUser选择人员
							me.onGetRBACUser(field, "SamplerId", me.fieldKey);
						}
					}
				},
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入工号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						//从PUser选择人员
						me.onGetRBACUser(field, "SamplerId", me.fieldKey);
					}
				},
				check: function(p, record) {
					me.isEnterKeyPress = true;
					me.onEmployeeCheck(p, record, "SamplerId");
				}
			}
		}, {
			fieldLabel: '采样者Id',
			hidden: true,
			xtype: "textfield",
			name: 'GKSampleRequestForm_SamplerId',
			itemId: 'GKSampleRequestForm_SamplerId'
		});
		return items;
	},
	/**@desc 院感科评估*/
	createJudgmentInfo: function() {
		var me = this;
		var items = [];
		items.push({
			xtype: 'displayfield',
			fieldLabel: '',
			labelWidth: 0,
			name: 'GKSampleRequestForm_SCRecordType_Description',
			itemId: 'GKSampleRequestForm_SCRecordType_Description',
			fieldStyle: {
				padding: "2px",
				fontSize: "17px",
				backgroundColor: "#FF5722"
			},
			value: ''
		});
		return items;
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;

		var sampleItemList = me.getFormItemList(me.SCRecordTypeId);
		var backgroundColor = sampleItemList.length > 0 ? sampleItemList[0].style.backgroundColor : "#FF5722";
		var items = [{
			xtype: 'radiogroup',
			fieldLabel: "",
			labelWidth: 0,
			border: 1,
			style: {
				fontSize: me.itemStyle.fontSize,
				borderStyle: 'solid'
			},
			colspan: 4,
			width: me.width,
			columns: 8,
			vertical: true,
			locked: true,
			readOnly: true,
			allowBlank: false,
			name: "GKSampleRequestForm_SCRecordType_Id",
			itemId: "GKSampleRequestForm_SCRecordType_Id",
			items: me.createRecordTypeItem(),
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					if (newValue && newValue != oldValue) {
						me.changeFormItemList(newValue);
						//JShell.Action.delay(function() {}, null, 600);
					}
				}
			}
		}, {
			xtype: 'fieldset',
			title: "登记信息",
			itemId: "SampleDocInfo",
			height: 180,
			colspan: 1,
			width: me.defaults.width * 1,
			collapsible: false,
			defaultType: 'textfield',
			layout: 'anchor',
			margin: "2",
			locked: true,
			readOnly: true,
			defaults: {
				anchor: '100%',
				labelWidth: 60,
				labelAlign: 'right'
			},
			style: {
				fontSize: "14px",
				borderStyle: 'solid'
			},
			items: me.createSampleDocInfo()
		}, {
			xtype: 'fieldset',
			title: "样品信息",
			itemId: "SampleItemList",
			height: 180,
			colspan: 1,
			width: me.defaults.width * 1,
			collapsible: false,
			margin: "2",
			defaultType: 'textfield',
			layout: 'anchor',
			defaults: {
				anchor: '100%',
				labelWidth: 60,
				labelAlign: 'right'
			},
			style: {
				fontSize: "14px",
				borderStyle: 'solid'
			},
			items: sampleItemList //动态生成
		}, {
			xtype: 'fieldset',
			title: "院感科评估",
			itemId: "JudgmentInfo",
			height: 180,
			layout: 'anchor',
			margin: "2",
			padding: "2",
			defaults: {
				anchor: '100%',
				labelWidth: 60,
				labelAlign: 'right',
				fontSize: "20px"
			},
			colspan: 2,
			width: me.defaults.width * 2,
			collapsible: false,
			defaultType: 'textfield',
			/* style: {
				backgroundColor: "#FF5722"
			}, */
			items: me.createJudgmentInfo()
		}];

		return items;
	},

	/**@overwrite isAdd处理方法*/
	isAdd: function() {
		var me = this;
		//me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if (buttonsToolbar) {
			buttonsToolbar.show();
		}
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle(); //标题更改
		me.enableControl(); //启用所有的操作功能
		me.setButtonsVisible(true);

		me.setSampleItemInfo(false,true);
		var objV = {
			"GKSampleRequestForm_Id": "",
			"GKSampleRequestForm_StatusID": "",
			"GKSampleRequestForm_DtlJArray": ""
		};
		//样品信息组表单项处理
		var items = me.getComponent('SampleItemList').items.items;
		for (var i = 0; i < items.length; i++) {
			var itemId = items[i].itemId;
			objV[itemId] = items[i].getValue();
		}
		me.setDefaultVals(objV);
		if (me.ISCURDEPT == true) {
			var deptCName = me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_DeptCName');
			deptCName.setReadOnly(true);
		}
	},
	/**@overwrite isEdit处理方法*/
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		//除了监测类型之外都可编辑
		me.setSampleItemInfo(false,false);
		me.getComponent('GKSampleRequestForm_SCRecordType_Id').setReadOnly(true);
		if (me.ISCURDEPT == true) {
			var deptCName = me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_DeptCName');
			deptCName.setReadOnly(true);
		}
		me.setButtonsVisible(true);
	},
	/**@overwrite isShow处理方法*/
	isShow: function(id) {
		var me = this;
		me.callParent(arguments);
		me.setSampleItemInfo(true,false);
		me.setButtonsVisible(false);
	},
	clearData: function() {
		var me = this;
		var objV = {
			"GKSampleRequestForm_Id": "",
			"GKSampleRequestForm_StatusID": "",
			"GKSampleRequestForm_DtlJArray": ""
		};
		//样品信息组表单项处理
		var items = me.getComponent('SampleItemList').items.items;
		for (var i = 0; i < items.length; i++) {
			var itemId = items[i].itemId;
			objV[itemId] = "";
		}
		me.getForm().setValues(objV);
	},
	setButtonsVisible:function(visible){
		var me=this;
		var buttonsToolbar1=me.getComponent('buttonsToolbar1');
		if(!buttonsToolbar1) return;
		
		buttonsToolbar1.getComponent('btnTempSave').setVisible(visible);
		buttonsToolbar1.getComponent('btnSubmitted').setVisible(visible);
		buttonsToolbar1.getComponent('btnBatchSubmit').setVisible(visible);
	},
	/**
	 * 样品信息组表单项处理
	 * @param {Object} readOnly
	 * @param {Object} isSetValue
	 */
	setSampleItemInfo: function(readOnly,isSetValue) {
		var me = this;
		//监测类型表单项
		me.getComponent('GKSampleRequestForm_SCRecordType_Id').setReadOnly(readOnly);

		//登记信息组表单项处理
		var items = me.getComponent('SampleDocInfo').items.items;
		for (var i = 0; i < items.length; i++) {
			var item = items[i];
			item.setReadOnly(readOnly);
		}

		//样品信息组表单项处理
		items = me.getComponent('SampleItemList').items.items;
		for (var i = 0; i < items.length; i++) {
			var item = items[i];
			item.setReadOnly(readOnly);
			
			if(isSetValue==false) continue;
			//开单是否可见为否时时,清空默认结果值
			me.setIsBillVisible(item);
		}
	},
	/**
	 * 开单是否可见为否时时,清空默认结果值
	 * @param {Object} item
	 */
	setIsBillVisible:function(item){
		var me=this;
		var isBillVisible=true;

		if(item.itemEditInfo&&item.itemEditInfo.IsBillVisible){
			isBillVisible=""+item.itemEditInfo.IsBillVisible;
			
			if(isBillVisible=="1"||isBillVisible=="true"){
				isBillVisible=true;
			}else{
				isBillVisible=false;
			}
		}
		if(isBillVisible==false)item.setValue("");
	},
	/**
	 * @description 新增时的默认值处理
	 */
	setDefaultVals: function(objV) {
		var me = this;
		var deptId ="",deptCName="";
		var deptIdV=	me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_DeptId').getValue();
		if(deptIdV){
			deptId =deptIdV;
			deptCName=	me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_DeptCName').getValue();
		}	
		if(!deptId) deptId = JcallShell.System.Cookie.get(JcallShell.System.Cookie.map.DEPTID) || "";
		if(!deptCName) deptCName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";

		var sysdate = JcallShell.System.Date.getDate();
		var curDateTime = Ext.Date.format(sysdate, "Y-m-d");

		var recordTypeValue = ""; //列表当前选择的监测类型值
		if (me.GridPanel) recordTypeValue = me.GridPanel.CurRecordTypeValue;

		if (!objV) objV = {};

		//当监测类型与上一次的监测类型不一样时,重置采样时间
		var sampleTimeValue = me.LastSampleTimeValue;
		if (recordTypeValue != me.LastRecordTypeValue) {
			sampleTimeValue = me.SampleTimeValue;
		}

		objV["GKSampleRequestForm_SCRecordType_Id"] = recordTypeValue;
		objV["GKSampleRequestForm_DeptId"] = deptId;
		objV["GKSampleRequestForm_DeptCName"] = deptCName;
		objV["GKSampleRequestForm_SamplerId"] = empID;
		objV["GKSampleRequestForm_Sampler"] = empName;
		objV["GKSampleRequestForm_DataAddTime"] = curDateTime;
		objV["GKSampleRequestForm_SampleDate"] = curDateTime;
		objV["GKSampleRequestForm_SampleTime"] = sampleTimeValue;

		me.lastData = objV;
		me.getForm().setValues(objV);
	},
	/**
	 * @description 
	 * @param {Object} field
	 */
	onGetDept: function(field) {
		var me = this;
		var fieldVal = field.getValue();
		if (fieldVal) {
			var fields = "Department_Id,Department_CName,Department_Code1,Department_Code2,Department_Code3";
			var where = "department.Id='" + fieldVal + "'";
			var url = JShell.System.Path.ROOT +
				"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDepartmentByHQL?isPlanish=true";
			url += '&where=' + where + "&fields=" + fields;
			JShell.Server.get(url, function(data) {
				if (data.success && data.value) {
					var list = data.value.list;
					if (list.length == 1) {
						me.onDepCheck(null, list[0]);
					} else if (list.length > 1) {
						JShell.Msg.error('编号为:' + fieldVal + ',获取科室信息存在多条记录！');
					} else {
						JShell.Msg.error('编号为:' + fieldVal + ',获取科室信息失败！');
					}
				} else {
					JShell.Msg.error('编号为:' + fieldVal + ',获取科室信息失败！');
				}
			});
		}
	},
	/**
	 * @description 弹出人员选择器选择确认后处理
	 * @param {Object} p
	 * @param {Object} data
	 */
	onDepCheck: function(p, data) {
		var me = this;
		var DeptId = me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_DeptId');
		var DeptCName = me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_DeptCName');
		DeptCName.setValue(data ? data["Department_CName"] : '');
		DeptId.setValue(data ? data["Department_Id"] : '');
		if (p) p.close();
		
		me.CurDeptId=DeptId.getValue();
		//科室选择改后,记录项的结果短语需要更新
		me.loadSampleItemsData();
	},
	/**
	 * @description 弹出人员选择器选择确认后处理
	 * @param {Object} p
	 * @param {Object} record
	 * @param {Object} type1
	 */
	onEmployeeCheck: function(p, record, type1) {
		var me = this;
		var userInfo = {
			"RBACUser_Id": "",
			"RBACUser_CName": ""
		};
		if (me.userIsGetLimp == true) {
			userInfo["RBACUser_Id"] = record ? record.get('HREmployee_CName') : '';
			userInfo["RBACUser_CName"] = record ? record.get('HREmployee_Id') : '';
		} else {
			userInfo["RBACUser_Id"] = record ? record.get('DepartmentUser_PUser_Id') : '';
			userInfo["RBACUser_CName"] = record ? record.get('DepartmentUser_PUser_CName') : '';
		}
		me.isEnterKeyPress = true;
		me.setCheckVal(type1, userInfo);
		p.close();
	},
	/**@desc 选择人员后赋值*/
	setCheckVal: function(type1, userInfo) {
		var me = this;
		var ManagerID = null,
			ManagerName = null;
		ManagerID = me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_SamplerId');
		ManagerName = me.getComponent('SampleDocInfo').getComponent('GKSampleRequestForm_Sampler');
		if (ManagerName) ManagerName.setValue(userInfo ? userInfo['RBACUser_CName'] : '');
		if (ManagerID) ManagerID.setValue(userInfo ? userInfo['RBACUser_Id'] : '');
	},
	/**
	 * @description 获取所有人员基本信息
	 */
	getPUserList: function() {
		var me = this;
		var fields = "PUser_Id,PUser_CName";
		var url = JShell.System.Path.ROOT +
			"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUserByHQL?isPlanish=true";
		url += (url.indexOf('?') == -1 ? '?' : '&') + "&fields=" + fields;
		JShell.Server.get(url, function(data) {
			if (data.success && data.value) {
				me.puseList = data.value.list;
			} else {
				me.puseList = [];
			}
		});
	},
	/**
	 * 输入工号后回车处理(从PUser)
	 * @param {Object} field
	 * @param {Object} type1
	 */
	onGetRBACUser: function(field, type1, fieldKey) {
		var me = this;
		me.isEnterKeyPress = true;
		var userInfo = {
			"RBACUser_Id": "",
			"RBACUser_CName": ""
		};
		var fieldVal = field.getValue();
		if (fieldVal) {
			var fields = "RBACUser_Id,RBACUser_CName";
			var url = JShell.System.Path.ROOT +
				"/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchRBACUserByFieldKey?isPlanish=true";
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'fieldVal=' + fieldVal;
			url += '&fieldKey=' + fieldKey + "&fields=" + fields;
			JShell.Server.get(url, function(data) {
				me.isEnterKeyPress = true;
				if (data.success && data.value) {
					userInfo = data.value;
					me.setCheckVal(type1, userInfo);
				} else {
					JShell.Msg.error('工号为:' + fieldVal + ',获取人员信息失败！');
					//清空
					me.setCheckVal(type1, userInfo);
				}
			});
		}
	},
	/**
	 * @description 获取打印机选择
	 */
	getPrinter: function() {
		var me = this;
		return me.getComponent('buttonsToolbar1').getComponent('PrinterList');
	},
	/**
	 * @description 初始化默认信息
	 */
	initDefaultInfo: function() {
		var me = this;

		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//获取还原当前用户默认选择的打印机
		var key = "printbarcode.gbbarcode.Printer." + userId;
		var printer = JcallShell.LocalStorage.get(key);
		if (printer) {
			printer = JcallShell.JSON.decode(printer);
			me.DefaultPrinter = printer.Value;
		}

		//获取还原当前用户默认选择的条码模板
		var key2 = "printbarcode.gbbarcode.BarcodeModel." + userId;
		var barcodeModel = JcallShell.LocalStorage.get(key2);
		if (barcodeModel) {
			barcodeModel = JcallShell.JSON.decode(barcodeModel);
			me.DefaultBarcodeModel = barcodeModel.Value;
		}

	},
	/**
	 * @description 缓存当前用户选择的打印机
	 * @param {Object} newValue
	 */
	setDefaultPrinter: function(newValue) {
		var me = this;
		me.DefaultPrinter = newValue;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "printbarcode.gbbarcode.Printer." + userId;
		var params = {
			"Value": me.DefaultPrinter
		};
		params = JcallShell.JSON.encode(params);
		JcallShell.LocalStorage.set(key, params);
	},
	/**缓存当前用户选择的条码模板*/
	setDefaultBarcodeModel: function(newValue) {
		var me = this;
		me.DefaultBarcodeModel = newValue;
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var key = "printbarcode.gbbarcode.BarcodeModel." + userId;
		var params = {
			"Value": me.DefaultBarcodeModel
		};
		params = JcallShell.JSON.encode(params);
		JcallShell.LocalStorage.set(key, params);
	},
	/**
	 * @description CLodop
	 * @param {Object} type
	 */
	getCLodop: function(type) {
		var me = this;
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.ux.lodop.Lodop');
		var LODOP = me.Lodop.getLodop(true);
		if (!LODOP) {
			//JShell.Msg.error("LODOP打印控件获取出错!");
			return;
		}
		return LODOP;
	},
	/**
	 * @description 获取客户端电脑上的打印机集合信息
	 * @param {Object} element
	 */
	createPrinterList: function(element) {
		var me = this;
		me.PrinterList = [];
		var LODOP = me.getCLodop();
		if (!LODOP || !CLODOP) return;
		var iCount = 0;
		if (CLODOP)
			iCount = CLODOP.GET_PRINTER_COUNT();
		var iIndex = 0;
		for (var i = 0; i < iCount; i++) {
			me.PrinterList.push([iIndex, CLODOP.GET_PRINTER_NAME(i)]);
			iIndex++;
		}
	},
	/**
	 * @description 获取某一监测类型的院感科评估提示信息
	 * @param {Object} newValue
	 */
	getDescription: function(newValue) {
		var me = this;
		var description2 = "";

		if (!newValue) return description2;

		if (newValue["GKSampleRequestForm_SCRecordType_Id"]) newValue = newValue["GKSampleRequestForm_SCRecordType_Id"];

		for (var i = 0; i < me.RecordTypeItemList.length; i++) {
			var item = me.RecordTypeItemList[i];
			if (newValue == item["SCRecordType_Id"]) {
				description2 = item["SCRecordType_Description"];
				break;
			}
		}
		return description2;
	},
	/**
	 * @description 监测类型改变后，更新替换记录项信息
	 * @param {Object} newValue
	 */
	changeFormItemList: function(newValue) {
		var me = this;

		var formItemList = me.getFormItemList(newValue);

		//样品记录项信息处理
		var itemInfoList = me.getComponent('SampleItemList');
		itemInfoList.removeAll();
		if (!formItemList) formItemList = [];
		itemInfoList.add(formItemList);

		//院感科评估提示信息
		var description1 = me.getComponent('JudgmentInfo').getComponent('GKSampleRequestForm_SCRecordType_Description');
		var description2 = me.getDescription(newValue);
		description1.setValue(description2);
	},
	/**保存按钮点击处理方法*/
	onTempSave: function() {
		var me = this;

		if (!me.getForm().isValid()) return;

		me.CurStatus = 0;

		var values = me.getForm().getValues();
		var sampleteTime = values.GKSampleRequestForm_SampleTime;

		var isAlert = false;
		if (me.LastSampleTimeValue == me.SampleTimeValue) {
			isAlert = true;
		}

		if (sampleteTime == me.SampleTimeValue) { //&&isAlert==true
			JShell.Msg.confirm({
				msg: "采样时间为" + me.SampleTimeValue + "确定吗?",
				title: "提示信息?"
			}, function(but) {
				if (but != "ok") return;

				me.onSaveClick();
			});
		} else {
			me.onSaveClick();
		}
	},
	/**保存按钮点击处理方法*/
	onSubmitted: function() {
		var me = this;
		if (!me.getForm().isValid()) return;

		me.CurStatus = 1;

		var values = me.getForm().getValues();
		var sampleteTime = values.GKSampleRequestForm_SampleTime;

		if (sampleteTime == me.SampleTimeValue) {
			JShell.Msg.confirm({
				msg: "采样时间为" + me.SampleTimeValue + "确定吗?",
				title: "提示信息?"
			}, function(but) {
				if (but != "ok") return;

				me.onSaveClick();
			});
		} else {
			me.onSaveClick();
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var dataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 1];
		var entity = {
			StatusID: me.CurStatus,
			MonitorType: me.MonitorType,
			DeptCName: values.GKSampleRequestForm_DeptCName,
			Sampler: values.GKSampleRequestForm_Sampler,
			Visible: true
		};
		if (values.GKSampleRequestForm_Id) entity.Id = values.GKSampleRequestForm_Id;
		if (values.GKSampleRequestForm_DeptId)entity.DeptId=values.GKSampleRequestForm_DeptId;
		if (values.GKSampleRequestForm_SamplerId)entity.SamplerId=values.GKSampleRequestForm_SamplerId;
		
		if (values.GKSampleRequestForm_SCRecordType_Id) {
			entity.SCRecordType = {
				Id: values.GKSampleRequestForm_SCRecordType_Id,
				DataTimeStamp: dataTimeStamp
			};
		}
		me.LastRecordTypeValue = values.GKSampleRequestForm_SCRecordType_Id;

		var dataAddTime = "";
		if (values.GKSampleRequestForm_DataAddTime) {
			dataAddTime = values.GKSampleRequestForm_DataAddTime;
		}
		if (dataAddTime) {
			entity.DataAddTime = JcallShell.Date.toServerDate(dataAddTime);
		}

		//采样日期时间处理
		var sampleteTime = "";
		if (values.GKSampleRequestForm_SampleDate) {
			sampleteTime = values.GKSampleRequestForm_SampleDate;
		}
		if (sampleteTime && values.GKSampleRequestForm_SampleTime) {
			sampleteTime = sampleteTime + " " + values.GKSampleRequestForm_SampleTime;
		}
		if (sampleteTime) {
			entity.SampleDate = JcallShell.Date.toServerDate(sampleteTime);
			entity.SampleTime = JcallShell.Date.toServerDate(sampleteTime);
		}

		//对应记录项明细信息
		var dtlEntityList = [];
		var dtlItems = me.getComponent('SampleItemList');
		var items = [];
		if (dtlItems.items && dtlItems.items.items) items = dtlItems.items.items;
		
		for (var i = 0; i < items.length; i++) {
			var item = items[i]; //某一表单记录项
			var objectId = -1;
			if (item.BObjectID) objectId = "" + item.BObjectID;
			var itemResult=item.getValue();
			if(!itemResult)itemResult="0";
			
			//透析液及透析用水的检验项目结果值处理,取检验项目的中文名称
			var curTypeItemId = "",
				itemEditInfoId = "";
			if (item.curTypeItem) curTypeItemId = "" + item.curTypeItem.Id;
			if (item.itemEditInfo) itemEditInfoId = "" + item.itemEditInfo.Id;
			if (curTypeItemId == "15" && itemEditInfoId == "120010") {
				itemResult=item.getRawValue();
			}
			
			var model2 = {
				Id: objectId, //监测类型记录项明细的主键Id
				Visible: 1,
				TestItemCode:item.TestItemCode,//对应检验项目编码
				ContentTypeID: "10000", //院感登记
				SCRecordType: entity.SCRecordType, //所属监测类型,
				SCRecordTypeItem: { //监测类型记录项
					Id: item.itemId,
					DataTimeStamp: dataTimeStamp
				},
				ItemResult: itemResult //描述结果值
			};
			dtlEntityList.push(model2);
		}

		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";

		var result = {
			"entity": entity,
			"dtlEntityList": dtlEntityList,
			"empID": empID,
			"empName": empName,
		};
		return result;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

		for (var i in fields) {
			var arr = fields[i].split('_');
			if (arr.length > 2) continue;

			if (arr[1] == "DtlJArray") continue;

			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		entity.entity.Id = values.GKSampleRequestForm_Id;
		return entity;
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this,
			fields = [];
		var items = me.items.items || [],
			len = items.length;
		//最外层的items	
		for (var i = 0; i < len; i++) {
			if (items[i].name && !items[i].IsnotField) {
				fields.push(items[i].name);
			}
		}
		//登记信息items
		items = me.getComponent('SampleDocInfo').items.items || [];
		len = items.length;
		for (var i = 0; i < len; i++) {
			if (items[i].name && !items[i].IsnotField) {
				fields.push(items[i].name);
			}
		}
		//样品信息items,后台服务自动以GKSampleRequestForm_DtlJArray返回
		//院感科评估
		fields.push("GKSampleRequestForm_SCRecordType_Description");

		return fields;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data = me.callParent(arguments);
		
		//科室选择改后,记录项的结果短语需要更新
		if(me.CurDeptId!=data["GKSampleRequestForm_DeptId"]){
			me.CurDeptId=data["GKSampleRequestForm_DeptId"];
			me.loadSampleItemsData();
		}
		//监测类型单选组赋值
		if (data["GKSampleRequestForm_SCRecordType_Id"]) {
			var objValue = {
				"GKSampleRequestForm_SCRecordType_Id": data["GKSampleRequestForm_SCRecordType_Id"]
			};
			data["GKSampleRequestForm_SCRecordType_Id"] = objValue;
			//直接赋值
			var itemCom = me.getComponent("GKSampleRequestForm_SCRecordType_Id");
			if (itemCom) {
				itemCom.setValue(objValue);
			}
		}

		//日期时间处理
		if (data.GKSampleRequestForm_DataAddTime) {
			data.GKSampleRequestForm_DataAddTime = JcallShell.Date.toString(data.GKSampleRequestForm_DataAddTime, true);
		}
		if (data.GKSampleRequestForm_SampleDate) {
			data.GKSampleRequestForm_SampleDate = JcallShell.Date.toString(data.GKSampleRequestForm_SampleDate, true);
		}
		if (data.GKSampleRequestForm_SampleTime) {
			var time1 = Ext.util.Format.date(data.GKSampleRequestForm_SampleTime, 'H:i'); //width: 680,
			if (time1 == "NaN:NaN") time1 = data.GKSampleRequestForm_SampleTime;
			data.GKSampleRequestForm_SampleTime = time1;
		}

		//监测类型的表单记录项处理
		me.SampleItemsVal = null;
		var dtlJArray = data.GKSampleRequestForm_DtlJArray;
		if (dtlJArray) {
			dtlJArray = JShell.JSON.decode(dtlJArray);
			me.SampleItemsVal = {};
			if (dtlJArray && dtlJArray.length > 0) {
				var itemId = '';
				var sampleItem = me.getComponent('SampleItemList');
				for (var i = 0; i < dtlJArray.length; i++) {
					var item = dtlJArray[i];
					itemId = "" + item.Id;
					data[itemId] = item.ItemResult;
					me.SampleItemsVal[itemId] = item.ItemResult;
					//监测类型记录项明细的主键Id处理
					if (item.BObjectID) {
						var item2 = sampleItem.getComponent(itemId);
						if (item2) {
							item2.BObjectID = "" + item.BObjectID;
							item2.setValue(item.ItemResult);
						}
					}
				}
			}
		}

		return data;
	}

});
