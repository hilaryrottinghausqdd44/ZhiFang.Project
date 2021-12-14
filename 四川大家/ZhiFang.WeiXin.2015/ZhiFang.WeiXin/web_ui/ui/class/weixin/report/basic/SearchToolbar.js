/**
 * 财务报表查询条件
 * @author liangyl	
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.report.basic.SearchToolbar', {
	extend: 'Shell.ux.toolbar.Button',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.MonthComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	height: 95,
	/**布局方式*/
	layout: 'absolute',
	defaultStatusValue: '',
	/**收缩高度*/
	toolbarHeight: 95,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},

	initComponent: function() {
		var me = this;
		me.addEvents('search');
		//初始化申请时间
		me.initDate();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var date = JShell.Date.getNextDate(new Date(),-1);
		
		var year = date.getFullYear();
		var month = date.getMonth() + 1;
		me.defaultBeginDate = JcallShell.Date.toString(JShell.Date.getMonthFirstDate(year, month),true);
		me.defaultEndDate = JcallShell.Date.toString(JShell.Date.getMonthLastDate(year, month),true);
		me.YearMonth = year;
		me.MonthMonth = month;
		
	},
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this,
			items = [];
		items.push(me.Year,me.Month,me.BeginDate, me.EndDate);
		//操作
		var buttons = me.createButtons();
		if(buttons) {
			items = items.concat(buttons);
		}
		return items;
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//创建可见组件
		me.createShowItems();
		//创建隐形组件
		items = items.concat(me.createHideItems());
		//获取列表布局组件内容
		items = items.concat(me.getTableLayoutItems());
		return items;
	},
	/**创建可见组件*/
	createShowItems: function() {
		var me = this;
		//创建时间组件
		me.createDateItems();
		//创建字典选择组件
		me.createDictItems();
		//创建其他组件
		me.createOrderItems();
	},
	//创建字典选择组件
	createDictItems: function() {
		var me = this;
		//部门
		me.ItemName = {
			fieldLabel: '项目名称',
			emptyText: '项目名称',
			name: 'ItemName',
			itemId: 'ItemName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.weixin.report.item.labtestitem.CheckApp',
			labelWidth: 60,
			width: 460,
			classConfig: {
				title: '项目选择',
				checkOne: true
			}
		};
		//区域
		me.AreaName = {
			fieldLabel: '区域',
			emptyText: '区域',
			name: 'AreaName',
			itemId: 'AreaName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.weixin.hospital.area.CheckGrid',
			labelWidth: 60,
			width: 160,
			classConfig: {
				title: '区域选择'
			}
		};
		//开单医生
		me.DoctorName = {
			fieldLabel: '开单医生',
			emptyText: '开单医生',
			name: 'DoctorName',
			itemId: 'DoctorName',
			xtype: 'uxCheckTrigger',
			labelAlign: 'right',
			className: 'Shell.class.weixin.doctor.CheckGrid',
			labelWidth: 60,
			width: 160,
			classConfig: {
				title: '开单医生选择',
				checkOne: true
			}
		};
	},
	createOrderItems: function() {
		var me = this;

        //订单编号
		me.UOFCode = {
			width: 200,
			fieldLabel: '订单编号',
			labelWidth: 60,
			labelAlign: 'right',
			itemId: 'UOFCode',
			name: 'UOFCode',
			xtype: 'textfield'
		};
		  //子订单编号
		me.UOIID = {
			width: 210,
			fieldLabel: '子订单编号',
			labelAlign: 'right',
			labelWidth: 70,
			itemId: 'UOIID',
			name: 'UOIID',
			xtype: 'textfield'
		};
		
		//开单日期
		me.BillingBeginDate = {
			width: 160,
			fieldLabel: '开单日期',
			labelWidth: 60,
			labelAlign: 'right',
			itemId: 'BillingBeginDate',
			name: 'BillingBeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
		//开单结束日期
		me.BillingEndDate = {
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'BillingEndDate',
			name: 'BillingEndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		};

		
		//采样日期
		me.SampleBeginDate = {
			width: 160,
			fieldLabel: '采样日期',
			labelWidth: 60,
				labelAlign: 'right',
			itemId: 'SampleBeginDate',
			name: 'SampleBeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
		//采样结束日期
		me.SampleEndDate = {
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'SampleEndDate',
			name: 'SampleEndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		};
		
		 //退款单号
		me.RefundNO = {
			width: 180,
			fieldLabel: '退款单号',
			labelAlign: 'right',
			labelWidth: 60,
			itemId: 'RefundNO',
			name: 'RefundNO',
			xtype: 'textfield'
		};
		
	    //转款单号
		me.TransferNO = {
			width: 180,
			fieldLabel: '转款单号',
				labelAlign: 'right',
			labelWidth: 60,
			itemId: 'TransferNO',
			name: 'TransferNO',
			xtype: 'textfield'
		};
		//是否转款
		me.ISTransfer={
			labelWidth: 60,
			width: 150,
			fieldLabel: '是否转款',
			labelAlign: 'right',
			xtype: 'uxBoolComboBox',
			itemId: 'ISTransfer',
			hasAll: true,
			value: null
		};
		//是否退款
		me.ISRefund={
			labelWidth: 60,
			width: 150,
			fieldLabel: '是否退款',
			labelAlign: 'right',
			xtype: 'uxBoolComboBox',
			itemId: 'ISRefund',
			hasAll: true,
			value: null
		};
		//客户
		me.AccountName = {
			width: 150,
			fieldLabel: '客户',
			labelWidth: 60,
			labelAlign: 'right',
			itemId: 'AccountName',
			name: 'AccountName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.weixin.ordersys.seach.weixinaccount.CheckGrid',
			classConfig: {
				title: '用户选择',
				checkOne: true
			}
		};		
	},
	/**创建时间组件*/
	createDateItems: function() {
		var me = this;
		me.Year={
			width: 105,
			x: 5,
			y: 5,
			fieldLabel: '年月',
			labelAlign: 'right',
			labelWidth: 35,
			xtype: 'uxYearComboBox',
			itemId: 'YearMonth',
			value: me.YearMonth
		};
		me.Month={
			width: 65,
			x: 113,
			y: 5,
			xtype: 'uxMonthComboBox',
			itemId: 'MonthMonth',
			value: me.MonthMonth
		}; 
		
		//开始时间
		me.BeginDate = {
			x: 190,
			y: 5,
			width: 130,
			fieldLabel: '时间',
			labelAlign: 'right',
			labelWidth: 35,
			itemId: 'BeginDate',
			name: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			emptyText: '必填项',
			allowBlank: false,
			value: me.defaultBeginDate
		};
		//结束时间
		me.EndDate = {
			x: 320,
			y: 5,
			width: 105,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			name: 'EndDate',
			xtype: 'datefield',
			value: me.defaultEndDate,
			emptyText: '必填项',
			allowBlank: false,
			format: 'Y-m-d'
		};
	},
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '项目Id',
			xtype: 'textfield',
			hidden: true,
			name: 'ItemID',
			itemId: 'ItemID'
		});
		items.push({
			fieldLabel: '区域Id',
			xtype: 'textfield',
			hidden: true,
			name: 'AreaID',
			itemId: 'AreaID'
		});
		items.push({
			fieldLabel: '开单医生Id',
			xtype: 'textfield',
			hidden: true,
			name: 'DoctorID',
			itemId: 'DoctorID'
		});
		items.push({
			fieldLabel: '用户主键ID',
			xtype: 'textfield',
			hidden: true,
			name: 'AccountID',
			itemId: 'AccountID'
		});
		
		return items;
	},
	/**创建功能按钮*/
	createButtons: function() {
		var me = this,
			items = [];
		items.push({
		    x: 450,
			y: 5,
			width: 60,
			iconCls: 'button-cancel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '清空',
			tooltip: '<b>清空查询条件</b>',
			handler: function() {
				me.onClearSearch();
			}
		}, {
			x: 515,
			y: 5,
			width: 60,
			iconCls: 'button-search',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '查询',
			tooltip: '<b>查询</b>',
			handler: function() {
				me.onFilterSearch();
			}
		},{
			x: 580,
			y: 5,
			width: 80,
			iconCls: 'file-excel',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '导出Excel',
			tooltip: '<b>导出Excel</b>',
			handler: function() {
				me.onExpExcelClick();
			}
		},{
			x: 665,
			y: 5,
			width: 55,
			iconCls: 'button-print',
			margin: '0 0 0 10px',
			xtype: 'button',
			text: '打印',
			tooltip: '<b>打印</b>',
			handler: function() {
				me.onDownloadPdf();
			}
		},   {
			x: 740,
			y: 5,
			width: 85,
			iconCls: 'button-up',
			margin: '0 0 0 4px',
			xtype: 'button',
			tooltip: '<b>简单查询</b>',
			text: '简单查询',
			itemId: 'up',
			handler: function() {
				this.ownerCt.getComponent('down').show();
				this.ownerCt.getComponent('up').hide();
				me.OrderHide(false);
				me.setHeight(30);
			}
		}, {
			x: 740,
			y: 5,
			width: 85,
			iconCls: 'button-down',
			margin: '0 0 0 4px',
			xtype: 'button',
			hidden: true,
			itemId: 'down',
			tooltip: '<b>高级查询</b>',
			text: '高级查询',
			handler: function() {
				this.ownerCt.getComponent('down').hide();
				this.ownerCt.getComponent('up').show();
				me.OrderHide(true);
				me.setHeight(me.toolbarHeight);
			}
		});
		return items;
	},
	/**隐藏其他组件*/
	OrderHide: function(bo) {
		var me = this;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
			//项目
		var ItemName = me.getComponent('ItemName'),
			ItemID = me.getComponent('ItemID');
		if(ItemName) {
			ItemName.on({
				check: function(p, record) {
					ItemName.setValue(record ? record.get('BLabTestItem_CName') : '');
					ItemID.setValue(record ? record.get('BLabTestItem_ItemNo') : '');
					p.close();
				}
			});
		}
			//医生
		var DoctorName = me.getComponent('DoctorName'),
			DoctorID = me.getComponent('DoctorID');
		if(DoctorName) {
			DoctorName.on({
				check: function(p, record) {
					DoctorName.setValue(record ? record.get('BDoctorAccount_Name') : '');
					DoctorID.setValue(record ? record.get('BDoctorAccount_Id') : '');
					p.close();
				}
			});
		}
				//用户
		var AccountName = me.getComponent('AccountName'),
			AccountID = me.getComponent('AccountID');
		if(AccountName) {
			AccountName.on({
				check: function(p, record) {
					AccountName.setValue(record ? record.get('BWeiXinAccount_UserName') : '');
					AccountID.setValue(record ? record.get('BWeiXinAccount_Id') : '');
					p.close();
				}
			});
		}
		//区域
		var AreaName = me.getComponent('AreaName'),
			AreaID = me.getComponent('AreaID');
		if(AreaName) {
			AreaName.on({
				check: function(p, record) {
					AreaName.setValue(record ? record.get('ClientEleArea_AreaCName') : '');
					AreaID.setValue(record ? record.get('ClientEleArea_Id') : '');
					p.close();
				}
			});
		}
		//年
		var YearMonth = me.getComponent('YearMonth');
		//月
		var MonthMonth = me.getComponent('MonthMonth');
		
		var EndDate=me.getComponent('EndDate');
		var BeginDate=me.getComponent('BeginDate');
		
		YearMonth.on({
			change:function(com,newValue,oldValue,eOpts ){
				if(newValue){
					var StartDateVal = JcallShell.Date.toString(JShell.Date.getMonthFirstDate(newValue, MonthMonth.getValue()),true);
	            	var EndDateVal = JcallShell.Date.toString(JShell.Date.getMonthLastDate(newValue, MonthMonth.getValue()),true);
					BeginDate.setValue(StartDateVal);
					EndDate.setValue(EndDateVal);
				}
			}
		});
		MonthMonth.on({
			change:function(com,newValue,oldValue,eOpts ){
				if(newValue){
					var StartDateVal = JcallShell.Date.toString(JShell.Date.getMonthFirstDate(YearMonth.getValue(), newValue),true);
	            	var EndDateVal = JcallShell.Date.toString(JShell.Date.getMonthLastDate(YearMonth.getValue(), newValue),true);
					BeginDate.setValue(StartDateVal);
					EndDate.setValue(EndDateVal);
				}
			}
		});
	},
	
	/**清空查询内容*/
	onClearSearch: function() {
		var me = this;
	
	},
	/**查询处理*/
	onFilterSearch: function() {
		var me = this;
		var params = me.getParams();
		me.fireEvent('search', me, params);
	},

	/**获取参数*/
	getParams: function() {
		var me = this,
		params={};
		return params;
     },
     /**
	 * @param {Boolean} params 最终的查询条件的数组
	 * @param {Boolean} comList 待查询的选择项
	 * @param {Boolean} comListType 待查询的选择项类型名称
	 * @param {Boolean} me 当前查询条件的表单对象
	 */
	getSearchParams: function(params, comList, comListType) {
		var me = this;
		if(params == null) {
			params = {};
		}
		switch(comListType) {
			case "textList":
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(v) {
							params[name] = v;
						}
					}
				}
				break;
			case "comboList":
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(v != null && v != '' && v !== 0) {
							params[name] = v;
						}
					}
				}
				break;
			case "booleanList":
				//booleanList这一类的选择项的值为部时值是null,不需要传参数值
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(typeof(v) == "boolean") {
							if(v == true) {
								params[name] = '1';
							} else {
								params[name] = '0';
							}
						}
					}
				}
				break;
			case "checkList":
				for(var i in comList) {
					var name = comList[i];
					var com = me.getComponent(name);
					if(com) {
						var v = com.getValue();
						if(v) {
							params[name] = v;
						}
					}
				}
				break;
			default:
				break;
		}
		return params;
	},
	/**导出EXCEL文件*/
	onExpExcelClick: function() {
		var me = this;
	},
	/**打印文件*/
	onDownloadPdf: function() {
		var me = this;
	},
	/**打开预览窗口*/
	openPreviewForm: function(hasColse, url) {
		var me = this;
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		var config = {
			width: maxWidth,
			height: height,
			hasColse: hasColse,
			URL: url,
			title:'预览PDF文件'
		};
		JShell.Win.open('Shell.class.weixin.report.basic.PreviewPdf', config).show();
	}
});