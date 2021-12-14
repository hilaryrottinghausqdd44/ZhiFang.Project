/**
 * 单站点授权表单
 * @author longfc	
 * @version 2016-12-13
 */
Ext.define('Shell.class.wfm.authorization.ahsingle.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '单站点授权申请',
	width: 580,
	bodyPadding: '5px',
	
	formtype: 'edit',
	PK: null,
	
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHSingleLicenceById?isPlanish=false',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHSingleLicence',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHSingleLicenceByField',
	/**获取程序数据服务路径*/
	selectProgramUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByBDictTreeId?isPlanish=true',
	/**获取仪器数据服务路径*/
	selectEquipUrl: '/SingleTableService.svc/ST_UDTO_SearchBEquipByHQL?isPlanish=true',	
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	/**授权类型数据类域*/
	classNameSpace: 'ZhiFang.Entity.ProjectProgressMonitorManage',
	
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 280,
		labelAlign: 'right'
	},
	/*结束日期改变时是否需要作节假日延后处理*/
	IsGetLicenceEndDate: false,
	/**授权类型默认值*/
	defaultStatusValue: '2',
	/**授权类型是否选择程序*/
	IsCheckProgram: true,
	/**授权类型数据*/
	StatusListData: [],
	
	/**授权类型数据类名*/
	className: 'LicenceType',
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	pclientNameAllowBlank: true,
	/**选择合同默认条件,状态为评审通过*/
	contractExternalWhere: "",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage', 'LicenceType', function() {
			if(!JShell.System.ClassDict.LicenceType) {
				return;
			}
			var LicenceTypeId = me.getComponent('LicenceTypeId');
			var LicenceTypeList = JShell.System.ClassDict.LicenceType;
			if(LicenceTypeId.store.data.items.length == 0) {
				LicenceTypeId.store.loadData(me.getLicenceTypeData(LicenceTypeList));
			}
		});
		//初始化监听
		me.initListeners();
		if(me.formtype == "add") me.initDate();
	},
	initComponent: function() {
		var me = this;
		me.defaults.width = me.width / 2 - 20;
		if(me.hasButtontoolbar) me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;
		var items = [{
			fieldLabel: '用户名称',
			name: 'PClientName',
			itemId: 'PClientName',
			emptyText: '一审时为必填项',
			allowBlank: me.pclientNameAllowBlank,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.client.CheckGrid',
			classConfig: {
				height: 450,
				title: '用户选择'
			},
			colspan: 2,
			width: me.width - 40
		}, {
			fieldLabel: '合同',
			name: 'PContractName',
			itemId: 'PContractName',
			className: 'Shell.class.wfm.business.contract.CheckGrid',
			classConfig: {
				title: '合同选择',
				defaultLoad: true,
				externalWhere: me.contractExternalWhere
			},
			xtype: 'uxCheckTrigger',
			colspan: 2,
			width: me.width - 40
		}, {
			fieldLabel: 'MAC地址',
			name: 'MacAddress',
			itemId: 'MacAddress',
			emptyText: 'MAC格式：XX-XX-XX-XX-XX-XX 英文字母要大写',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1 - 10,
			validator: function(value) {
				if(value == "" || value == undefined || value == null) {
					return 'MAC地址不能为空！<br />MAC地址格式:XX-XX-XX-XX-XX-XX<br />英文字母要大写';
				} else {
					var macs = value.split("-");
					var msg = 'MAC地址格式不正确！<br />MAC地址格式:XX-XX-XX-XX-XX-XX<br />英文字母要大写';
					if(macs.length != 6) {
						return msg;
					} else if(macs.length == 6) {
						//mac地址正则表达式 
						var reg = new RegExp(/[A-F\d]{2}-[A-F\d]{2}-[A-F\d]{2}-[A-F\d]{2}-[A-F\d]{2}-[A-F\d]{2}/);
						if(!reg.test(value)) {
							return msg;
						} else {
							return true;
						}
					} else {
						return true;
					}
				}
			}
		}];

		items.push({
			name: 'SelectProgramOrEquip',
			itemId: 'SelectProgramOrEquip',
			xtype: 'radiogroup',
			fieldLabel: '授权选择',
			columns: 2,
			columnWidth: 0.5,
			vertical: true,
			colspan: 1,
			width: me.defaults.width * 1 - 10,
			items: [{
				boxLabel: '程序',
				name: 'ProgramOrEquip',
				inputValue: 1,
				checked: me.IsCheckProgram
			}, {
				boxLabel: '仪器 ',
				name: 'ProgramOrEquip',
				inputValue: 2,
				checked: !me.IsCheckProgram
			}]
		}, {
			fieldLabel: '程序选择',
			name: 'ProgramName',
			itemId: 'ProgramName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.authorization.program.CheckApp',
			colspan: 2,
			width: me.width - 40,
			classConfig: { width: 860 }
		}, {
			fieldLabel: '仪器选择',
			name: 'EquipName',
			itemId: 'EquipName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.authorization.equip.CheckApp',
			colspan: 2,
			width: me.width - 40,
			classConfig: { width: 860 }
		});

		items.push({
			fieldLabel: '授权类型',
			name: 'LicenceTypeId',
			itemId: 'LicenceTypeId',
			emptyText: '必填项',
			allowBlank: false,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			value: me.defaultStatusValue,
			colspan: 1,
			width: me.defaults.width * 1 - 10
		}, {
			fieldLabel: '授权号(SQH)',
			name: 'SQH',
			itemId: 'SQH',
			emptyText: '程序或仪器的SQH(必填项)',
			allowBlank: false,
			colspan: 1,
			saveDelay: 2000,
			width: me.defaults.width * 1 - 20
		}, {
			fieldLabel: '开始日期',
			name: 'StartDate',
			itemId: 'StartDate',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1 - 10
		}, {
			fieldLabel: '截止日期',
			name: 'EndDate',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1 - 20
		}, {
			xtype: 'fieldset',
			labelAlign: 'right',
			title: '按天数选择截止日期',
			layout: {
				type: 'table',
				columns: 1
			},
			name: 'AddDaysFieldSet',
			itemId: 'AddDaysFieldSet',
			defaultType: 'radiogroup',
			colspan: 2,
			width: me.width - me.defaults.labelWidth - 40,
			defaults: me.defaults,
			style: {
				marginLeft: '80px'
			},
			items: [{
				name: 'SelectAddDays',
				itemId: 'SelectAddDays',
				xtype: 'radiogroup',
				fieldLabel: '',
				columns: 4,
				columnWidth: 0.25,
				width: me.width - me.defaults.labelWidth - 40,
				vertical: true,
				items: [{
					boxLabel: '30天',
					name: 'addDays',
					inputValue: 30,
					checked: true
				}, {
					boxLabel: '60天 ',
					name: 'addDays',
					inputValue: 60
				}, {
					boxLabel: '90天',
					name: 'addDays',
					inputValue: 90
				}, {
					boxLabel: '180天',
					name: 'addDays',
					inputValue: 180
				}]
			}]
		}, {
			boxLabel: '如遇节假日,日期顺延(包括春节)',
			name: 'checkHoliday',
			itemId: 'checkHoliday',
			xtype: 'checkbox',
			checked: true,
			fieldLabel: '&nbsp;',
			labelSeparator: '',
			colspan: 1,
			width: me.defaults.width * 1 + 20
		}, {
			fieldLabel: '计划收款时间',
			emptyText: '临时授权时必填',
			name: 'PlannReceiptDate',
			itemId: 'PlannReceiptDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1 - 20
		}, {
			fieldLabel: '主键ID',
			name: 'Id',
			hidden: true
		}, {
			fieldLabel: '客户主键ID',
			name: 'PClientID',
			itemId: 'PClientID',
			hidden: true
		}, {
			fieldLabel: '合同',
			name: 'PContractID',
			itemId: 'PContractID',
			hidden: true
		}, {
			fieldLabel: '程序ID',
			name: 'ProgramID',
			itemId: 'ProgramID',
			hidden: true
		}, {
			fieldLabel: '仪器ID',
			name: 'EquipID',
			itemId: 'EquipID',
			hidden: true
		}, {
			fieldLabel: '状态',
			name: 'Status',
			itemId: 'Status',
			hidden: true
		}, {
			fieldLabel: '未收款原因',
			name: 'Comment',
			itemId: 'Comment',
			emptyText: '临时授权时必填',
			allowBlank: true,
			xtype: 'textareafield',
			colspan: 2,
			height: 50,
			width: me.width - 40
		});

		return items;
	},
	verificationSubmit: function() {
		var me = this
		if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return false;
		}
		var values = me.getForm().getValues();
		if(!JShell.System.Cookie.map.USERID) {
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return false;
		}
		if(me.pclientNameAllowBlank == false && !values.PClientID) {
			JShell.Msg.error('用户名称信息不能为空！');
			return false;
		}
		if(!values.EquipID && !values.ProgramID) {
			JShell.Msg.error('程序和仪器不能同时为空!');
			return false;
		}
		//授权类型不是商业
		var info2 = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
		if(values.LicenceTypeId != info2.Id) {
			var StartDateValue = JcallShell.Date.toString(values.StartDate, true);
			var EndDateValue = JcallShell.Date.toString(values.EndDate, true);
			if(StartDateValue > EndDateValue) {
				JShell.Msg.error('结束日期不能小于开始日期!');
				return false;
			}
		}

		var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '临时');
		if(values.LicenceTypeId && values.LicenceTypeId == info.Id) {
			if(values.Comment == "" || values.Comment == null || values.Comment == undefined) {
				JShell.Msg.error('授权类型为临时,未收款原因不能为空!');
				return false;
			}
			if(values.PlannReceiptDate == "" || values.PlannReceiptDate == null || values.PlannReceiptDate == undefined) {
				JShell.Msg.error('授权类型为临时,计划收款时间不能为空!');
				return false;
			}
		}
		return true;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if(data.ProgramID != null && data.ProgramID != "" && data.ProgramID != undefined) {
			me.IsCheckProgram = true;
		} else {
			me.IsCheckProgram = false;
		}
		if(!me.IsCheckProgram){ //仪器选择
			var SelectProgramOrEquip = me.getComponent('SelectProgramOrEquip');
            SelectProgramOrEquip.items.get(1).setValue(true);
		}
		if(data.EndDate && data.EndDate != null && data.EndDate != undefined)
			data.EndDate = JShell.Date.getDate(data.EndDate);
		if(data.StartDate && data.StartDate != null && data.StartDate != undefined)
			data.StartDate = JShell.Date.getDate(data.StartDate);
		if(data.PlannReceiptDate && data.PlannReceiptDate != null && data.PlannReceiptDate != undefined)
			data.PlannReceiptDate = JShell.Date.getDate(data.PlannReceiptDate);
		if(me.formtype == "edit" && data.LoanBillAmount != null) {
			if(data.PContractID != null && data.PContractID != "")
				me.contractExternalWhere = me.contractExternalWhere + " pcontract.PClientID=" + data.PContractID;
		}
		if(data.Comment != null && data.Comment != undefined) {
			var reg = new RegExp("<br />", "g");
			data.Comment = data.Comment.replace(reg, "\r\n");
		}
		return data;
	},
	isEdit: function() {
		var me = this;
		me.callParent(arguments);
		if(me.contractExternalWhere != "") {
			var ContractName = me.getComponent('PContractName');
			if(ContractName) {
				ContractName.classConfig.externalWhere = me.contractExternalWhere;
				ContractName.changeClassConfig(ContractName.classConfig);
			}
		}
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
		} else {
			me.getForm().setValues(me.lastData);
		}
		if(me.formtype == "add") me.initDate();
	},
	/**初始化监听*/
	initListeners: function() {
		var me = this;
		//客户
		var PClientName = me.getComponent('PClientName'),
			PClientID = me.getComponent('PClientID');
		//合同
		var PContractID = me.getComponent('PContractID');
		var PContractName = me.getComponent('PContractName');
		PClientName.on({
			check: function(p, record) {
				PClientName.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				//客户选择改变后,合同信息先清空
				me.contractExternalWhere = (record ? " pcontract.PClientID=" + record.get('PClient_Id') : '');
				PContractID.setValue("");
				PContractName.setValue("");
				PContractName.classConfig.externalWhere = me.contractExternalWhere;
				PContractName.changeClassConfig(PContractName.classConfig);
				p.close();
			}
		});
		PContractName.on({
			check: function(p, record) {
				PContractName.setValue(record ? record.get('PContract_Name') : '');
				PContractID.setValue(record ? record.get('PContract_Id') : '');
				p.close();
			}
		});

		var SelectProgramOrEquip = me.getComponent('SelectProgramOrEquip');
		var SQH = me.getComponent('SQH');
		var ProgramName = me.getComponent('ProgramName');
		var ProgramID = me.getComponent('ProgramID');
		var EquipName = me.getComponent('EquipName');
		var EquipID = me.getComponent('EquipID');
		var LicenceTypeId = me.getComponent('LicenceTypeId');
		var StartDate = me.getComponent('StartDate');
		var EndDate = me.getComponent('EndDate');
		var AddDaysFieldSet = me.getComponent('AddDaysFieldSet');
		var SelectAddDays = AddDaysFieldSet.getComponent('SelectAddDays');
		var checkHoliday = me.getComponent('checkHoliday');

		switch(SelectProgramOrEquip.getValue().ProgramOrEquip) {
			case 1:
				EquipName.setVisible(false);
				ProgramName.setVisible(true);
				break;
			default:
				ProgramName.setVisible(false);
				EquipName.setVisible(true);
				break;
		}
		//授权选择
		SelectProgramOrEquip.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue != null) {
					var ProgramName = me.getComponent('ProgramName');
					var EquipName = me.getComponent('EquipName');
					switch(newValue.ProgramOrEquip) {
						case 1:
							if(EquipName) {
								EquipName.setValue("");
								EquipName.allowBlank = true;
								EquipName.emptyText = '';
								EquipName.setVisible(false);
							}
							EquipID.setValue("");
							ProgramName.allowBlank = false;
							ProgramName.emptyText = '必填项';
							ProgramName.setVisible(true);
							break;
						default:
							if(ProgramName) {
								ProgramName.setValue("");
								ProgramName.emptyText = '';
								ProgramName.allowBlank = true;
								ProgramName.setVisible(false);
							}
							ProgramID.setValue("");
							EquipName.emptyText = '必填项';
							EquipName.allowBlank = false;
							EquipName.setVisible(true);
							EquipName.show();
							break;
					}
					me.getComponent('SQH').setValue('');
				}
			}
		});

		ProgramName.on({
			check: function(p, record) {
				ProgramName.setValue(record ? record.get('PGMProgram_Title') : '');
				ProgramID.setValue(record ? record.get('PGMProgram_Id') : '');
				SQH.setValue(record ? record.get('PGMProgram_SQH') : '');
				p.close();
			}
		});

		EquipName.on({
			check: function(p, record) {
				EquipName.setValue(record ? record.get('BEquip_CName') : '');
				EquipID.setValue(record ? record.get('BEquip_Id') : '');
				SQH.setValue(record ? record.get('BEquip_Shortcode') : '');
				p.close();
			}
		});

		//授权类型
		LicenceTypeId.on({
			change: function(com, newValue, oldValue, eOpts) {
				var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
				var isReadOnly = false;
				var height = 375;
				if(newValue == info.Id) {
					StartDate.allowBlank = true;
					EndDate.allowBlank = true;
					StartDate.emptyText = '';
					EndDate.emptyText = '';
					StartDate.setValue("");
					EndDate.setValue("");
					isReadOnly = true;
					height = 295;
				} else {
					StartDate.allowBlank = false;
					EndDate.allowBlank = false;
					StartDate.emptyText = '必填项';
					EndDate.emptyText = '必填项';
					me.initDate();
				}
				StartDate.setReadOnly(isReadOnly);
				EndDate.setReadOnly(isReadOnly);
				AddDaysFieldSet.setVisible(!isReadOnly);
				checkHoliday.setVisible(!isReadOnly);

				var Comment = me.getComponent('Comment');
				var PlannReceiptDate = me.getComponent('PlannReceiptDate');

				var tempType = JShell.System.ClassDict.getClassInfoByName('LicenceType', '临时');
				if(newValue == tempType.Id) {
					height = height + 55;
					Comment.allowBlank = false;
					Comment.setVisible(true);
					PlannReceiptDate.allowBlank = false;
					PlannReceiptDate.setVisible(true);
				} else {
					Comment.allowBlank = true;
					Comment.setVisible(false);
					PlannReceiptDate.allowBlank = true;
					PlannReceiptDate.setVisible(false);
				}
				me.setHeight(height);
			}
		});

		EndDate.on({
			focus: function(com, e, eOpts) {
				//节假日顺延开关
				if(checkHoliday.getValue() == true) {
					me.IsGetLicenceEndDate = true;
				}
			},
			collapse: function(com, eOpts) {
				//节假日顺延开关
				if(checkHoliday.getValue() == true) {
					me.IsGetLicenceEndDate = true;
				}
			},
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue && newValue != null && newValue != "") {
					if(checkHoliday.getValue() == true) {
						me.IsGetLicenceEndDate=true;
						var EndDateValue = JShell.Date.toString(newValue, true);
						if(me.IsGetLicenceEndDate == true) {
							var dateFormat =/^(\d{4})-(\d{2})-(\d{2})$/;
							if(!dateFormat.test(EndDateValue)){
								return;
							}
							if(!com.isValid())return;
							newValue = me.getLicenceEndDate(EndDateValue);
							com.Value = newValue;
							com.setValue(newValue);
						}
					}
					var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '商业');
					if(LicenceTypeId.getValue() != info.Id) {
						var StartDateValue = JcallShell.Date.toString(StartDate.getValue(), true);
						var EndDateValue = JcallShell.Date.toString(newValue, true);
						if(StartDateValue > EndDateValue) {
							JShell.Msg.error('结束日期不能小于开始日期!');
						}
					}
				}
			}
		});
		//按日子选择
		SelectAddDays.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue != null) {
					var EndDateValue = me.dateAddDays(SelectAddDays.getValue().addDays);
					EndDate.setValue(EndDateValue);
				}
			}
		});

		var task = new Ext.util.DelayedTask(function() {
			me.getStartDateValueOfApply();
		});
		SQH.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue != null) {
					task.delay(1500);
				}
			}
		});
		var MacAddress = me.getComponent('MacAddress');
		MacAddress.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue != null) {
					if(newValue.length>17){
			    	    com.setValue(oldValue);
			    	    return;
			        }
					var val = me.strInsert(newValue,2);
					com.setValue(val);
					task.delay(1500);
				}
			}
		});
		var SelectProgramOrEquip = me.getComponent('SelectProgramOrEquip');
		  	//SQH授权号
		var SQH = me.getComponent('SQH');
		SQH.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue != null) {
					//是程序还仪器
	            	var isProgram = SelectProgramOrEquip.getValue().ProgramOrEquip;
					me.getSQH(newValue,isProgram,function(list){
						var Id = null,Title="";
						if(isProgram==1){//程序
						    if(list.length>0){
						    	Id = list[0].PGMProgram_Id;
							    Title = list[0].PGMProgram_Title;
						    }
						    me.getForm().setValues({
								ProgramID:Id,
								ProgramName:Title
							});
						}else{ //仪器
							if(list.length>0){
						    	Id = list[0].BEquip_Id;
							    Title = list[0].BEquip_CName;
						    }
							me.getForm().setValues({
								EquipID:Id,
								EquipName:Title
							});
						}					
					});
				}
			}
		});
	},
	/**初始化开始日期及结束日期*/
	initDate: function() {
		var me = this;
		var SelectAddDays = me.getComponent('AddDaysFieldSet').getComponent('SelectAddDays');
		var checkHoliday = me.getComponent('checkHoliday');
		var Sysdate = JcallShell.System.Date.getDate();
		var StartDateValue = Ext.util.Format.date(Sysdate, 'Y-m-d');
		var addDays = 0;
		addDays = SelectAddDays.getValue().addDays;
		var EndDateValue = Ext.util.Format.date(JcallShell.Date.getNextDate(StartDateValue, addDays), 'Y-m-d');
		if(EndDateValue != "") {
			me.IsGetLicenceEndDate = true;
			EndDateValue = me.getLicenceEndDate(EndDateValue);
		}
		var StartDate = me.getComponent('StartDate');
		var EndDate = me.getComponent('EndDate');
		StartDate.setValue(StartDateValue);
		EndDate.setValue(EndDateValue);
	},
	/**日期加上天数得到新的日期
	 * 日期dayCount 要增加的天数
	 */
	dateAddDays: function(addDays) {
		var me = this;
		var startDate = me.getComponent('StartDate');
		var endDate = me.getComponent('EndDate');
		var StartDateValue = startDate.getValue(),
			EndDateValue = "";
		if(StartDateValue != "") {
			StartDateValue = Ext.util.Format.date(StartDateValue, 'Y-m-d');
			if(StartDateValue != "")
				EndDateValue = Ext.util.Format.date(JcallShell.Date.getNextDate(StartDateValue, addDays), 'Y-m-d');
		}
		if(EndDateValue != "") {
			me.IsGetLicenceEndDate = true;
			EndDateValue = me.getLicenceEndDate(EndDateValue);
		}
		return EndDateValue;
	},
	/**节假日顺延*/
	getLicenceEndDate: function(endDate) {
		var me = this,
			Date = endDate;
		if(me.IsGetLicenceEndDate == true) {
			var url = '/ProjectProgressMonitorManageService.svc/ST_UDTO_GetLicenceEndDate';
			url = JShell.System.Path.getRootUrl(url);
			if(endDate) {
				url = url + '?endDate=' + endDate;
			}
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.IsGetLicenceEndDate = false;
					Date = data.value.EndDate;
				} else {
					me.IsGetLicenceEndDate = true;
					JShell.Msg.error(data.msg);
				}
			}, false, 10, false);
		} else {
			me.IsGetLicenceEndDate = true;
		}
		return Date;
	},

	/**授权类型为临时时获取开始日期值处理*/
	getStartDateValueOfApply: function() {
		var me = this;
		var LicenceTypeId = me.getComponent('LicenceTypeId');
		var info = JShell.System.ClassDict.getClassInfoByName('LicenceType', '临时');
		if(me.formtype == "add" && LicenceTypeId.getValue() == info.Id) {
			var mac = "",
				sqh = "";
			var StartDate = me.getComponent('StartDate');
			var EndDate = me.getComponent('EndDate');
			var macValue = me.getComponent('MacAddress').getValue();
			var sqhValue = me.getComponent('SQH').getValue();
			if(macValue != "" && sqhValue != "") {
				var url = '/ProjectProgressMonitorManageService.svc/ST_UDTO_GetStartDateValueOfApply';
				url = JShell.System.Path.getRootUrl(url);
				url = url + '?mac=' + macValue + "&sqh=" + sqhValue;
				JShell.Server.get(url, function(data) {
					if(data.success) {
						//me.IsGetLicenceEndDate = false;
						var StartDateValue = data.value.StartDate;
						if(StartDateValue) {
							StartDate.setValue(StartDateValue);
							var SelectAddDays = me.getComponent('AddDaysFieldSet').getComponent('SelectAddDays');
							var addDays = 0;
							addDays = SelectAddDays.getValue().addDays;
							var EndDateValue = Ext.util.Format.date(JcallShell.Date.getNextDate(StartDateValue, addDays), 'Y-m-d');
							if(EndDateValue != "") {
								me.IsGetLicenceEndDate = true;
								EndDateValue = me.getLicenceEndDate(EndDateValue);
								EndDate.setValue(EndDateValue);
							}
						}
					}
				}, false, 10, false);
			}
		}
	},
	/**获取状态列表*/
	getLicenceTypeData: function(StatusListData) {
		var me = this,
			data = [];
		for(var i in StatusListData) {
			var obj = StatusListData[i];
			var style = ['font-weight:bold;']; //text-align:center
			if(obj.BGColor) {
				style.push('color:' + obj.BGColor);
			}
			data.push([obj.Id, obj.Name, style.join(';')]);
		}
		return data;
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit) {
		var me = this;
	},
	createButtonToolbarItems:function(){
		var me = this,
		    buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.push('->', {
			text: '暂存',
			iconCls: 'button-save',
			tooltip: '暂存',
			handler: function() {
				me.onSave(false);
			}
		}, {
			text: '提交',
			iconCls: 'button-save',
			tooltip: '提交',
			handler: function() {
				me.onSave(true);
			}
		}, 'reset');    
		return buttonToolbarItems;
	},
	/**
	* @param {string} str   需要插入的字符（串）
	* @param {int} length   间隔几个字符
	*/
	strInsert : function (str, length) {
	   var val = "";
	   var strval = str.toLocaleUpperCase();
	   let reg = new RegExp("\[0-9a-fA-F]{1," + length + "}", "g");	   
	   let ma = strval.match(reg);
	   if(ma)val = ma.join("-"); 
	   return val; //
	},
	/**
	 * 根据授权号返回程序
	* @param  SQH 授权号
	*/
	getSQH : function(SQH,isProgram,callback){
		var me = this;
		//仪器
		if(isProgram==2){
			var url = JShell.System.Path.ROOT + me.selectEquipUrl;
			url += '&fields=BEquip_CName,BEquip_Id,BEquip_Shortcode';
			url += "&where=(bequip.IsUse=1 and bequip.Shortcode='"+SQH+"')";
		}else{
			var url = JShell.System.Path.ROOT + me.selectProgramUrl;
			url +='&fields=PGMProgram_Title,PGMProgram_SQH,PGMProgram_No,PGMProgram_VersionNo,PGMProgram_Id';
			url += "&isSearchChildNode=false&where=id=5323720114866215336^(pgmprogram.SubBDictTree.Id!=5684872576807158459 and pgmprogram.PBDictTree.Id!=5684872576807158459 and pgmprogram.IsUse=1 and Status=3 and pgmprogram.SQH='"+SQH+"')";
		}
		JShell.Action.delay(function(){
	        JShell.Server.get(url,function(data){
				if(data.success){
					var list = data.value.list || [];
				    callback(list);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		},null,200);
	}
});