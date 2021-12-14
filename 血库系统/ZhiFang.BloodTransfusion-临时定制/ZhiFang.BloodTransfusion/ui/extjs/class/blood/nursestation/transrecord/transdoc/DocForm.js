/**
 * 输血过程记录:主单信息
 * @author longfc
 * @version 2020-02-21
 */
Ext.define('Shell.class.blood.nursestation.transrecord.transdoc.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		//'Ext.ux.picker.DateTime',
		//'Ext.ux.form.field.DateTime',

		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '过程记录信息',
	height: 220,
	/**内容周围距离*/
	bodyPadding: '5px 2px 0px 0px',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormById?isPlanish=true',

	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 85,
		width: 210,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**启用表单状态初始化*/
	openFormType: true,
	//输血过程记录主单ID
	PK: null,
	/**通过指定字段(如工号等)获取RBACUser(PUser转换)*/
	fieldKey: "Code1",
	//人员选择是否从集成平台获取
	userIsGetLimp: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		if (me.width && me.width > 0) {
			me.layout.columns = parseInt(me.width / me.defaults.width);
		}
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '血制品名称',
			name: 'BloodTransForm_Bloodstyle_CName',
			xtype: 'textfield',
			IsnotField: true,
			hidden: true,
			allowBlank: true
		}, {
			fieldLabel: '血袋号',
			name: 'BloodTransForm_BBagCode',
			xtype: 'textfield',
			emptyText: '',
			hidden: true,
			allowBlank: true
		}, {
			fieldLabel: '产品号',
			name: 'BloodTransForm_PCode',
			xtype: 'textfield',
			hidden: true,
			emptyText: '',
			allowBlank: true
		}, {
			fieldLabel: '血容量',
			name: 'BloodTransForm_BloodBOutItem_BOutCount',
			xtype: 'numberfield',
			hidden: true,
			IsnotField: true,
			allowBlank: true
		}, {
			fieldLabel: '单位',
			name: 'BloodTransForm_BloodBOutItem_BloodBUnit_BUnitName',
			hidden: true,
			IsnotField: true,
			allowBlank: true
		});
		items.push({
			fieldLabel: '开始时间',
			labelWidth: 65,
			name: 'BloodTransForm_TransBeginTime',
			//itemId: 'BloodTransForm_TransBeginTime',
			emptyText: '',
			xtype: 'datetimefield', //datefield
			format: 'Y-m-d H:i:s',
			allowBlank: false,
			validator: function(val, valid) {
				return true;
			}
		}, {
			fieldLabel: '结束时间',
			labelWidth: 65,
			name: 'BloodTransForm_TransEndTime',
			//itemId: 'BloodTransForm_TransEndTime',
			emptyText: '',
			xtype: 'datetimefield',
			format: 'Y-m-d H:i:s',
			validator: function(val, valid) {
				return true;
			}
		});
		items.push({
			fieldLabel: '输血前核对人',
			name: 'BloodTransForm_BeforeCheck1',
			itemId: 'BloodTransForm_BeforeCheck1',
			emptyText: '输入工号后回车',
			editable: true,
			xtype: 'uxCheckTrigger',
			//从集成平台选择人员
			//className: 'Shell.class.limp.sysbase.user.CheckApp',
			//从PUser选择人员
			className: 'Shell.class.sysbase.deptuser.CheckApp',
			classConfig: {
				title: '输血前核对人选择'
			},
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入工号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						//从PUser选择人员
						me.onGetRBACUser(field, "BeforeCheckID1");
					}
				},
				check: function(p, record) {
					me.onEmployeeCheck(p, record, "BeforeCheckID1");
				}
			}
		}, {
			fieldLabel: '输血前核对人ID1',
			itemId: 'BloodTransForm_BeforeCheckID1',
			name: 'BloodTransForm_BeforeCheckID1',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '输血前核对人2',
			name: 'BloodTransForm_BeforeCheck2',
			itemId: 'BloodTransForm_BeforeCheck2',
			emptyText: '输入工号后回车',
			editable: true,
			xtype: 'uxCheckTrigger',
			//从集成平台选择人员
			//className: 'Shell.class.limp.sysbase.user.CheckApp',
			//从PUser选择人员
			className: 'Shell.class.sysbase.deptuser.CheckApp',
			classConfig: {
				title: '输血前核对人选择'
			},
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入工号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						me.onGetRBACUser(field, "BeforeCheckID2");
					}
				},
				check: function(p, record) {
					me.onEmployeeCheck(p, record, "BeforeCheckID2");
				}
			}
		}, {
			fieldLabel: '输血前核对人ID2',
			itemId: 'BloodTransForm_BeforeCheckID2',
			name: 'BloodTransForm_BeforeCheckID2',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '输血时核对人',
			name: 'BloodTransForm_TransCheck1',
			itemId: 'BloodTransForm_TransCheck1',
			emptyText: '输入工号后回车',
			editable: true,
			xtype: 'uxCheckTrigger',
			//从集成平台选择人员
			//className: 'Shell.class.limp.sysbase.user.CheckApp',
			//从PUser选择人员
			className: 'Shell.class.sysbase.deptuser.CheckApp',
			classConfig: {
				title: '输血时对人选择'
			},
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入工号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						me.onGetRBACUser(field, "TransCheckID1");
					}
				},
				check: function(p, record) {
					me.onEmployeeCheck(p, record, "TransCheckID1");
				}
			}
		}, {
			fieldLabel: '输血时核对人ID1',
			itemId: 'BloodTransForm_TransCheckID1',
			name: 'BloodTransForm_TransCheckID1',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '输血时核对人2',
			name: 'BloodTransForm_TransCheck2',
			itemId: 'BloodTransForm_TransCheck2',
			emptyText: '输入工号后回车',
			editable: true,
			xtype: 'uxCheckTrigger',
			//从集成平台选择人员
			//className: 'Shell.class.limp.sysbase.user.CheckApp',
			//从PUser选择人员
			className: 'Shell.class.sysbase.deptuser.CheckApp',
			classConfig: {
				title: '输血时核对人选择'
			},
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						if (!field.getValue()) {
							var info = "请输入工号!";
							JShell.Msg.alert(info, null, 2000);
							return;
						}
						me.onGetRBACUser(field, "TransCheckID2");
					}
				},
				check: function(p, record) {
					me.onEmployeeCheck(p, record, "TransCheckID2");
				}
			}
		}, {
			fieldLabel: '输血时核对人2',
			itemId: 'BloodTransForm_TransCheckID2',
			name: 'BloodTransForm_TransCheckID2',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '主键ID',
			name: 'BloodTransForm_Id',
			xtype: 'textfield',
			type: "key",
			hidden: true
		});
		items.push({
			fieldLabel: '输血不良反应',
			boxLabel: '',
			name: 'BloodTransForm_HasAdverseReactions',
			itemId: 'BloodTransForm_HasAdverseReactions',
			xtype: 'checkbox',
			inputValue: true,
			checked: false
		}, {
			fieldLabel: '不良反应时间',
			name: 'BloodTransForm_AdverseReactionsTime',
			itemId: 'BloodTransForm_AdverseReactionsTime',
			xtype: 'datetimefield', //datefield
			format: 'Y-m-d H:i:s',
			validator: function(val, valid) {
				return me.validityAdverseReactionsTime();
			},
			listeners: {
				change: function(field,newValue,oldValue, e) {
					if (newValue) {
						var hasAdverseReactions = me.getComponent('BloodTransForm_HasAdverseReactions');
						if(hasAdverseReactions)hasAdverseReactions.setValue(true);
					}
				}
			}
		}, {
			fieldLabel: '剩余血量(ml)',
			name: 'BloodTransForm_AdverseReactionsHP',
			xtype: "numberfield",
			emptyText: ''
		});
		return items;
	},
	/**
	 * 开始日期选择改变后验证处理
	 */
	validityTransBeginTime: function() {
		var me = this;
		if (me.formtype == "show") return true;

		var beginDate = me.getComponent('BloodTransForm_TransBeginTime');
		var endDate = me.getComponent('BloodTransForm_TransEndTime');
		if (!beginDate || !endDate) return true;

		var beginDate1 = beginDate.getValue();
		var endDate2 = endDate.getValue();
		if (!beginDate1 || !endDate2) return true;

		if (beginDate1 < endDate2) {
			return true;
		} else {
			return "开始时间不能大于或等于结束时间！";
		}
	},
	/**
	 * 结束日期选择改变后验证处理
	 */
	validityTransEndTime: function() {
		var me = this;
		if (me.formtype == "show") return true;
		var beginDate = me.getComponent('BloodTransForm_TransBeginTime');
		var endDate = me.getComponent('BloodTransForm_TransEndTime');
		if (!beginDate || !endDate) return true;

		var beginDate1 = beginDate.getValue();
		var endDate2 = endDate.getValue();
		if (!endDate2) return true;

		if (endDate2 < beginDate1) {
			return true;
		} else {
			return "结束时间不能小于或等于开始时间！";
		}
	},
	/**
	 * 不良反应时间选择改变后验证处理
	 */
	validityAdverseReactionsTime: function() {
		var me = this;
		if (me.formtype == "show") return true;

		var adverseReactionsTime = me.getComponent('BloodTransForm_AdverseReactionsTime');
		var beginDate = me.getComponent('BloodTransForm_TransBeginTime');
		var endDate = me.getComponent('BloodTransForm_TransEndTime');
		if (!beginDate || !endDate || !adverseReactionsTime) return true;

		var adverseReactionsTime1 = adverseReactionsTime.getValue();
		var beginDate1 = beginDate.getValue();
		if (!adverseReactionsTime1 || !beginDate1) return true;

		if (adverseReactionsTime1 < beginDate1) {
			return "不良反应时间不能小于或等于开始时间！";
		} else {
			return true;
		}
	},
	/**@desc 弹出人员选择器选择确认后处理*/
	onEmployeeCheck: function(p, record, type1) {
		var me = this;
		var userInfo = {
			"RBACUser_Id": "",
			"RBACUser_CName": ""
		};
		if (me.userIsGetLimp==true) {
			userInfo["RBACUser_Id"] = record ? record.get('HREmployee_CName') : '';
			userInfo["RBACUser_CName"] = record ? record.get('HREmployee_Id') : '';
		}else{
			userInfo["RBACUser_Id"] = record ? record.get('DepartmentUser_PUser_Id') : '';
			userInfo["RBACUser_CName"] = record ? record.get('DepartmentUser_PUser_CName') : '';
		}
		me.setCheckVal(type1, userInfo);
		p.close();
	},
	/**
	 * 输入工号后回车处理(从PUser)
	 * @param {Object} field
	 * @param {Object} type1
	 */
	onGetRBACUser: function(field, type1) {
		var me = this;
		var userInfo = {
			"RBACUser_Id": "",
			"RBACUser_CName": ""
		};
		var fieldVal = field.getValue();
		if (fieldVal) {
			var fields = "RBACUser_Id,RBACUser_CName";
			var url = JShell.System.Path.ROOT +
				"/BloodTransfusionManageService.svc/BT_UDTO_SearchRBACUserByFieldKey?isPlanish=true";
			url += (url.indexOf('?') == -1 ? '?' : '&') + 'fieldVal=' + fieldVal;
			url += '&fieldKey=' + me.fieldKey + "&fields=" + fields;
			JShell.Server.get(url, function(data) {
				if (data.success && data.value) {
					userInfo = data.value;
					me.setCheckVal(type1, userInfo);
				} else {
					JShell.Msg.error('工号为:' + fieldVal + ',获取人员信息失败！');
				}
			});
		}
	},
	/**@desc 选择人员后赋值*/
	setCheckVal: function(type1, userInfo) {
		var me = this;
		var ManagerID = null,
			ManagerName = null;
		if (type1 == "BeforeCheckID1") {
			ManagerID = me.getComponent('BloodTransForm_BeforeCheckID1');
			ManagerName = me.getComponent('BloodTransForm_BeforeCheck1');
		} else if (type1 == "BeforeCheckID2") {
			ManagerID = me.getComponent('BloodTransForm_BeforeCheckID2');
			ManagerName = me.getComponent('BloodTransForm_BeforeCheck2');
		} else if (type1 == "TransCheckID1") {
			ManagerID = me.getComponent('BloodTransForm_TransCheckID1');
			ManagerName = me.getComponent('BloodTransForm_TransCheck1');
		} else if (type1 == "TransCheckID2") {
			ManagerID = me.getComponent('BloodTransForm_TransCheckID2');
			ManagerName = me.getComponent('BloodTransForm_TransCheck2');
		}

		if (ManagerName) ManagerName.setValue(userInfo ? userInfo['RBACUser_CName'] : '');
		if (ManagerID) ManagerID.setValue(userInfo ? userInfo['RBACUser_Id'] : '');
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;

		var hasAdverseReactions = data.BloodTransForm_HasAdverseReactions;
		if (hasAdverseReactions == "true" || hasAdverseReactions == true) hasAdverseReactions = '1';
		hasAdverseReactions = (hasAdverseReactions == '1' ? 1 : 0);
		data.BloodTransForm_HasAdverseReactions = hasAdverseReactions;

		var transBeginTime = data.BloodTransForm_TransBeginTime;
		if (transBeginTime) {
			data.BloodTransForm_TransBeginTime = JcallShell.Date.toString(transBeginTime.replace(/\//g, "-"));
		}
		var transEndTime = data.BloodTransForm_TransEndTime;
		if (transEndTime) {
			data.BloodTransForm_TransEndTime = JcallShell.Date.toString(transEndTime.replace(/\//g, "-"));
		};
		var adverseReactionsTime = data.BloodTransForm_AdverseReactionsTime;
		if (adverseReactionsTime) {
			data.BloodTransForm_AdverseReactionsTime = JcallShell.Date.toString(adverseReactionsTime.replace(/\//g, "-"));
		}
		return data;
	}
});
