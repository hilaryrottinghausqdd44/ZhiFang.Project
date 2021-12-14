/**
 * 服务受理
 * @author Jcall	
 * @version 2016-11-03
 */
Ext.define('Shell.class.wfm.service.accept.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '服务受理',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	width: 810,
	height: 880,
	bodyPadding: 20,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPCustomerService',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceByField',
	/**获取获取类字典列表服务路径*/
	classdicSelectUrl: '/SystemCommonService.svc/GetClassDicList',
	layout: 'anchor',
	defaults: {
		anchor: '100%'
	},
	/**完成情况*/
	CompletionStatus: 'CompletionStatus',
	/**用户满意度*/
	Satisfaction: 'Satisfaction',
	/**服务方式*/
	ServiceMode: 'ServiceMode',
	/**fieldset布局*/
	fieldsetLayout: {
		type: 'table',
		columns: 3
	},
	/**fieldset内部组件初始属性*/
	fieldsetDefaults: {
		labelWidth: 80,
		width: 240,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**未处理的处理人*/
	ServiceOperationCompleteMan:{
		ID:'',
		Name:''
	},
	/**服务状态*/
	ServiceStatus:'ServiceStatus',
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	hasCancel: true,
	defaultStatusValue: '1',
	/**是否启用取消按钮*/
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			defaults = me.fieldsetDefaults,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			name: 'PCustomerService_Id',
			hidden: true
		});
		//服务受理
		items.push(me.createAppectFieldSet());
		//服务处理
		items.push(me.createExecuteFieldSet());
		//服务分析
		items.push(me.createAnalysisFieldSet());
		//服务回访
		items.push(me.createVisitFieldSet());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**服务受理*/
	createAppectFieldSet: function() {
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;
		var fieldset = {
			xtype: 'fieldset',
			title: '服务受理',
			collapsible: true,
			defaultType: 'textfield',
			itemId: 'ServiceAcceptance',
			layout: layout,
			defaults: defaults,
			items: [{
				fieldLabel: '用户名称',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.client.CheckGrid',
				name: 'PCustomerService_ClientName',
				itemId: 'PCustomerService_ClientName',
				colspan: 1,
				width: defaults.width * 1
			}, {
				fieldLabel: '省份',
				name: 'PCustomerService_ProvinceName',
				itemId: 'PCustomerService_ProvinceName',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.country.province.CheckGrid',
				colspan: 1,
				width: defaults.width * 1
			}, {
				fieldLabel: '代理请求',
				name: 'PCustomerService_IsProxy',
				xtype: 'uxBoolComboBox',
				hasAll: false,
				hasStyle: false,
				/**默认否*/
				value: false,
				colspan: 1,
				width: defaults.width * 1
			}, {
				fieldLabel: '请求人',
				name: 'PCustomerService_RequestMan',
				itemId: 'PCustomerService_RequestMan',
				colspan: 1,
				width: defaults.width * 1
			}, {
				fieldLabel: '联系电话',
				name: 'PCustomerService_RequestManPhone',
				colspan: 2,
				width: defaults.width * 1
			}, {
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.user.CheckApp',
				itemId: 'PCustomerService_ServiceAcceptanceMan',
				fieldLabel: '受理人',
				name: 'PCustomerService_ServiceAcceptanceMan',
				colspan: 1,
				width: defaults.width * 1
			}, {
				xtype: 'datefield',
				format: 'Y-m-d',
				fieldLabel: '受理时间',
				name: 'PCustomerService_ServiceAcceptanceDate',
				itemId: 'PCustomerService_ServiceAcceptanceDate',
				colspan: 2,
				width: defaults.width * 1
			}, {
				xtype: 'textarea',
				grow: true,
				fieldLabel: '问题原始描述',
				name: 'PCustomerService_ProblemMemo',
				colspan: 3,
				width: defaults.width * 3
			}]
		};

		return fieldset;
	},
	/**服务处理*/
	createExecuteFieldSet: function() {
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;

		var fieldset = {
			xtype: 'fieldset',
			title: '服务处理',
			collapsible: true,
			defaultType: 'textfield',
			itemId: 'ServiceOperation',
			layout: layout,
			defaults: defaults,
			items: [{
				fieldLabel: '状态',
				name: 'PCustomerService_StatusName',
				itemId: 'PCustomerService_StatusName',
				colspan: 1,
				width: defaults.width * 1,
				hasStyle: true,
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '状态选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ServiceStatus + "'"
				}
			}, {
				fieldLabel: '服务方式',
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '服务方式选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ServiceMode + "'"
				},
				itemId: 'PCustomerService_ServiceModeName',
				name: 'PCustomerService_ServiceModeName',
				colspan: 2,
				width: defaults.width * 1
			}, {
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.user.CheckApp',
				fieldLabel: '处理人',
				itemId: 'PCustomerService_ServiceOperationCompleteMan',
				name: 'PCustomerService_ServiceOperationCompleteMan',
				colspan: 1,
				width: defaults.width * 1
			}, {
				xtype: 'datefield',
				format: 'Y-m-d',
				fieldLabel: '处理时间',
				name: 'PCustomerService_ServiceOperationDate',
				itemId: 'PCustomerService_ServiceOperationDate',
				colspan: 1,
				width: defaults.width * 1
			}, {
				xtype: 'datefield',
				format: 'Y-m-d',
				fieldLabel: '完成时间',
				name: 'PCustomerService_ServiceFinishDate',
				colspan: 1,
				width: defaults.width * 1
			}, {
				fieldLabel: '是否需要回访',
				name: 'L207',
				colspan: 1,
				width: defaults.width * 1,
				hidden: true
			}, {
				fieldLabel: '是否已回访',
				name: 'L208',
				colspan: 3,
				width: defaults.width * 1,
				hidden: true
			}, {
				xtype: 'textarea',
				grow: true,
				fieldLabel: '处理说明',
				name: 'PCustomerService_ServiceOperationCompleteMemo',
				colspan: 3,
				width: defaults.width * 3
			}]
		};
		return fieldset;
	},
	/**服务分析*/
	createAnalysisFieldSet: function() {
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;

		var fieldset = {
			xtype: 'fieldset',
			title: '服务分析',
			collapsible: true,
			collapsed:true,
			defaultType: 'textfield',
			layout: layout,
			defaults: defaults,
			items: [{
				xtype: 'combobox',
				fieldLabel: '现象分类',
				name: 'L301',
				colspan: 1,
				//				emptyText: '必填项',
				width: defaults.width * 1
			}, {
				xtype: 'combobox',
				fieldLabel: '原因分类',
				name: 'L302',
				colspan: 1,
				//				emptyText: '必填项',
				width: defaults.width * 1
			}, {
				xtype: 'combobox',
				fieldLabel: '措施分类',
				name: 'L303',
				colspan: 1,
				//				emptyText: '必填项',
				width: defaults.width * 1
			}, {
				fieldLabel: '远程处理时长',
				name: 'L305',
				colspan: 1,
				//				emptyText: '必填项',
				width: defaults.width * 1
			}, {
				fieldLabel: '上门服务时长',
				name: 'L306',
				colspan: 1,
				//				emptyText: '必填项',
				width: defaults.width * 1
			}, {
				fieldLabel: '服务成本',
				name: 'L307',
				colspan: 1,
				//				emptyText: '必填项',
				width: defaults.width * 1
			}, {
				xtype: 'textarea',
				grow: true,
				fieldLabel: '相关功能',
				name: 'L304',
				colspan: 3,
				//				emptyText: '必填项',
				width: defaults.width * 3
			}]
		};

		return fieldset;
	},
	/**服务回访*/
	createVisitFieldSet: function() {
		var me = this,
			layout = me.fieldsetLayout,
			defaults = me.fieldsetDefaults;

		var fieldset = {
			xtype: 'fieldset',
			title: '服务回访',
			itemId: 'ServiceReturn',
			collapsible: true,
			collapsed:true,
			defaultType: 'textfield',
			layout: layout,
			defaults: defaults,
			items: [{
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.sysbase.user.CheckApp',
				fieldLabel: '回访人',
				name: 'PCustomerService_ServiceReturnMan',
				itemId: 'PCustomerService_ServiceReturnMan',
				colspan: 1,
				width: defaults.width * 1
			}, {
				xtype: 'datefield',
				format: 'Y-m-d',
				fieldLabel: '回访时间',
				name: 'PCustomerService_ServiceReturnDate',
				colspan: 1,
				width: defaults.width * 1
			}, {
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '用户满意度选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.Satisfaction + "'"
				},
				fieldLabel: '用户满意度',
				name: 'PCustomerService_SatisfactionName',
				itemId: 'PCustomerService_SatisfactionName',
				colspan: 1,
				width: defaults.width * 1
			}, {
				fieldLabel: '被回访人',
				itemId: 'PCustomerService_ServiceReturnToMan',
				name: 'PCustomerService_ServiceReturnToMan',
				colspan: 1,
				width: defaults.width * 1
			}, {
				fieldLabel: '联系电话',
				name: 'PCustomerService_ServiceReturnToManPhone',
				colspan: 1,
				width: defaults.width * 1
			}, {
				xtype: 'uxCheckTrigger',
				className: 'Shell.class.wfm.dict.CheckGrid',
				classConfig: {
					title: '完成情况选择',
					defaultWhere: "pdict.PDictType.DictTypeCode='" + this.CompletionStatus + "'"
				},
				fieldLabel: '完成情况',
				name: 'PCustomerService_CompletionStatusName',
				itemId: 'PCustomerService_CompletionStatusName',
				colspan: 1,
				width: defaults.width * 1
			}, {
				xtype: 'textarea',
				grow: true,
				fieldLabel: '回访结果',
				name: 'PCustomerService_ServiceReturnMemo',
				colspan: 3,
				width: defaults.width * 3
			}]
		};

		return fieldset;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			IsUse: true, //是否使用
			IsProxy: values.PCustomerService_IsProxy == true ? 1 : 0
		};
		//用户
		if(values.PCustomerService_ClientName) {
			entity.ClientID = values.PCustomerService_ClientID;
			entity.ClientName = values.PCustomerService_ClientName;
		}
		//省份
		if(values.PCustomerService_ProvinceName) {
			entity.ProvinceID = values.PCustomerService_ProvinceID;
			entity.ProvinceName = values.PCustomerService_ProvinceName;
		}
		//请求人
		if(values.PCustomerService_RequestMan) {
			entity.RequestMan = values.PCustomerService_RequestMan;
		}
		//联系电话
		if(values.PCustomerService_RequestManPhone) {
			entity.RequestManPhone = values.PCustomerService_RequestManPhone;
		}
		//受理人
		if(values.PCustomerService_ServiceAcceptanceMan) {
			entity.ServiceAcceptanceManID = values.PCustomerService_ServiceAcceptanceManID;
			entity.ServiceAcceptanceMan = values.PCustomerService_ServiceAcceptanceMan;
		}
		//受理时间
		if(values.PCustomerService_ServiceAcceptanceDate) {
			entity.ServiceAcceptanceDate = JShell.Date.toServerDate(values.PCustomerService_ServiceAcceptanceDate);
		}
		//问题描述
		if(values.PCustomerService_ProblemMemo) {
			entity.ProblemMemo = values.PCustomerService_ProblemMemo.replace(/\\/g, '&#92');
		}
		//状态
		if(values.PCustomerService_StatusID) {
//			entity.StatusCName =values.PCustomerService_StatusName;
			entity.Status = values.PCustomerService_StatusID;
		}
		//服务方式
		if(values.PCustomerService_ServiceModeName) {
			entity.ServiceModeID = values.PCustomerService_ServiceModeID;
			entity.ServiceModeName = values.PCustomerService_ServiceModeName;
		}
		//处理人
		if(values.PCustomerService_ServiceOperationCompleteMan) {
			entity.ServiceOperationCompleteManID = values.PCustomerService_ServiceOperationCompleteManID;
			entity.ServiceOperationCompleteMan = values.PCustomerService_ServiceOperationCompleteMan;
		}
		//处理时间
		if(values.PCustomerService_ServiceOperationDate) {
			entity.ServiceOperationDate = JShell.Date.toServerDate(values.PCustomerService_ServiceOperationDate);
		}
		//完成时间
		if(values.PCustomerService_ServiceFinishDate) {
			entity.ServiceFinishDate = JShell.Date.toServerDate(values.PCustomerService_ServiceFinishDate);
		}
		//处理说明
		if(values.PCustomerService_ServiceOperationCompleteMemo) {
			entity.ServiceOperationCompleteMemo = values.PCustomerService_ServiceOperationCompleteMemo.replace(/\\/g, '&#92');
		}
		//回访人
		if(values.PCustomerService_ServiceReturnMan) {
			entity.ServiceReturnManID = values.PCustomerService_ServiceReturnManID;
			entity.ServiceReturnMan = values.PCustomerService_ServiceReturnMan;
		}
		//回访时间
		if(values.PCustomerService_ServiceReturnDate) {
			entity.ServiceReturnDate = JShell.Date.toServerDate(values.PCustomerService_ServiceReturnDate);
		}
		//用户满意度
		if(values.PCustomerService_SatisfactionName) {
			entity.SatisfactionID = values.PCustomerService_SatisfactionID;
			entity.SatisfactionName = values.PCustomerService_SatisfactionName;
		}
		//被回访人
		if(values.PCustomerService_ServiceReturnToMan) {
			entity.ServiceReturnToMan = values.PCustomerService_ServiceReturnToMan;
		}
		//联系电话
		if(values.PCustomerService_ServiceReturnToManPhone) {
			entity.ServiceReturnToManPhone = values.PCustomerService_ServiceReturnToManPhone;
		}
		//完成情况
		if(values.PCustomerService_CompletionStatusName) {
			entity.CompletionStatusID = values.PCustomerService_CompletionStatusID;
			entity.CompletionStatusName = values.PCustomerService_CompletionStatusName;
		}
		//回访结果
		if(values.PCustomerService_ServiceReturnMemo) {
			entity.ServiceReturnMemo = values.PCustomerService_ServiceReturnMemo.replace(/\\/g, '&#92');
		}
		//默认员工ID
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//默认员工名称
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		//登记人
		if(userId) {
			entity.ServiceRegisterManID = userId;
			entity.ServiceRegisterMan = userName;
		}
		//登记时间
		var Sysdate = JcallShell.System.Date.getDate();
		var ServiceRegisterDate = JcallShell.Date.toString(Sysdate);
		if(ServiceRegisterDate) {
			entity.ServiceRegisterDate = JShell.Date.toServerDate(ServiceRegisterDate);
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['Id', 'IsProxy', 'ClientID', 'ClientName',
			'ProvinceID', 'ProvinceName', 'RequestMan', 'ServiceOperationDate',
			'RequestManPhone', 'ServiceAcceptanceManID', 'ServiceAcceptanceMan', 'ServiceAcceptanceDate',
			'ProblemMemo', 'Status', 'ServiceModeID', 'ServiceModeName',
			'ServiceOperationCompleteManID', 'ServiceOperationCompleteMan',
			'ServiceFinishDate', 'ServiceOperationCompleteMemo', 'ServiceReturnManID', 'ServiceReturnMan',
			'ServiceReturnDate', 'SatisfactionID', 'SatisfactionName',
			'ServiceReturnToMan', 'ServiceReturnToManPhone', 'CompletionStatusName',
			'CompletionStatusID', 'ServiceReturnMemo'
		];
		entity.fields = fields.join(',');
		if(values.PCustomerService_Id != '') {
			entity.entity.Id = values.PCustomerService_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		data.PCustomerService_ServiceAcceptanceDate = JShell.Date.getDate(data.PCustomerService_ServiceAcceptanceDate);
		data.PCustomerService_ServiceReturnDate = JShell.Date.getDate(data.PCustomerService_ServiceReturnDate);
		data.PCustomerService_ServiceFinishDate = JShell.Date.getDate(data.PCustomerService_ServiceFinishDate);
		data.PCustomerService_ServiceOperationDate = JShell.Date.getDate(data.PCustomerService_ServiceOperationDate);
		this.ServiceOperationCompleteMan.Name=data.PCustomerService_ServiceOperationCompleteMan;
		this.ServiceOperationCompleteMan.ID=data.PCustomerService_ServiceOperationCompleteManID;
		
		//服务处理
		var ServiceOperation = me.getComponent('ServiceOperation');
		var StatusName = ServiceOperation.getComponent('PCustomerService_StatusName'),
			StatusID = me.getComponent('PCustomerService_StatusID');
		StatusID.setValue(data.PCustomerService_Status);	
		if(data.PCustomerService_Status){
		        me.getStatusName(data.PCustomerService_Status,function(data){
		        	if(data.value.list){
		        		StatusName.setValue(data.value.list[0].PDict_CName);
		        	}
		        });
			}else{
				StatusName.setValue('');
			}
		return data;
		
	},
	/**获取信息*/
	getStatusName:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true';
		url += '&fields=PDict_CName,PDict_Id&where=pdict.Id='+id;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**更改标题*/
	changeTitle: function() {},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
			var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
			var ServiceAcceptance = me.getComponent('ServiceAcceptance');
			var ServiceAcceptanceMan = ServiceAcceptance.getComponent('PCustomerService_ServiceAcceptanceMan');
			var ServiceAcceptanceManID = me.getComponent('PCustomerService_ServiceAcceptanceManID');
			if(ServiceAcceptanceMan) {
				ServiceAcceptanceMan.setValue(userName);
				ServiceAcceptanceManID.setValue(userId);
			}
			//受理时间
			var Sysdate = JcallShell.System.Date.getDate();
			var AcceptanceDate = JcallShell.Date.toString(Sysdate, true);
			var ServiceAcceptanceDate = ServiceAcceptance.getComponent('PCustomerService_ServiceAcceptanceDate');
			if(ServiceAcceptanceDate) {
				ServiceAcceptanceDate.setValue(AcceptanceDate);
			}
		} else {
			me.getForm().setValues(me.lastData);
		}
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//服务受理
		var ServiceAcceptance = me.getComponent('ServiceAcceptance');
		//服务处理
		var ServiceOperation = me.getComponent('ServiceOperation');
		//服务回访
		var ServiceReturn = me.getComponent('ServiceReturn');
		//用户
		var ClientName = ServiceAcceptance.getComponent('PCustomerService_ClientName'),
			ClientID = me.getComponent('PCustomerService_ClientID');
			//省份
		var ProvinceName = ServiceAcceptance.getComponent('PCustomerService_ProvinceName'),
			ProvinceID = me.getComponent('PCustomerService_ProvinceID');
		if(ClientName) {
			ClientName.on({
				check: function(p, record) {
					ClientName.setValue(record ? record.get('PClient_Name') : '');
					ClientID.setValue(record ? record.get('PClient_Id') : '');
					ProvinceID.setValue(record ? record.get('PClient_ProvinceID') : '');
					ProvinceName.setValue(record ? record.get('PClient_ProvinceName') : '');
					p.close();
				}
			});
		}

       if(ProvinceName) {
			ProvinceName.on({
				check: function(p, record) {
					ProvinceName.setValue(record ? record.get('BProvince_Name') : '');
					ProvinceID.setValue(record ? record.get('BProvince_Id') : '');
					p.close();
				}
			});
		}
		//受理人
		var ServiceAcceptanceMan = ServiceAcceptance.getComponent('PCustomerService_ServiceAcceptanceMan'),
			ServiceAcceptanceManID = me.getComponent('PCustomerService_ServiceAcceptanceManID');
		if(ServiceAcceptanceMan) {
			ServiceAcceptanceMan.on({
				check: function(p, record) {
					ServiceAcceptanceMan.setValue(record ? record.get('HREmployee_CName') : '');
					ServiceAcceptanceManID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			});
		}
		//服务方式
		var ServiceModeName = ServiceOperation.getComponent('PCustomerService_ServiceModeName'),
			ServiceModeID = me.getComponent('PCustomerService_ServiceModeID');
		if(ServiceModeName) {
			ServiceModeName.on({
				check: function(p, record) {
					ServiceModeName.setValue(record ? record.get('PDict_CName') : '');
					ServiceModeID.setValue(record ? record.get('PDict_Id') : '');
					p.close();
				}
			});
		}
		//处理人
		var ServiceOperationCompleteMan = ServiceOperation.getComponent('PCustomerService_ServiceOperationCompleteMan'),
			ServiceOperationCompleteManID = me.getComponent('PCustomerService_ServiceOperationCompleteManID');
		if(ServiceOperationCompleteMan) {
			ServiceOperationCompleteMan.on({
				check: function(p, record) {
					ServiceOperationCompleteMan.setValue(record ? record.get('HREmployee_CName') : '');
					ServiceOperationCompleteManID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			});
		}
		//回访人
		var ServiceReturnMan = ServiceReturn.getComponent('PCustomerService_ServiceReturnMan'),
			ServiceReturnManID = me.getComponent('PCustomerService_ServiceReturnManID');
		if(ServiceReturnMan) {
			ServiceReturnMan.on({
				check: function(p, record) {
					ServiceReturnMan.setValue(record ? record.get('HREmployee_CName') : '');
					ServiceReturnManID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				}
			});
		}
		//状态
		var StatusName = ServiceOperation.getComponent('PCustomerService_StatusName'),
			StatusID = me.getComponent('PCustomerService_StatusID');
		if(StatusName) {
			StatusName.on({
				check: function(p, record) {
					StatusName.setValue(record ? record.get('PDict_CName') : '');
					StatusID.setValue(record ? record.get('PDict_Id') : '');
					p.close();
				}
			});
		}
		
		//字典监听
		var dictList = [
			'Satisfaction', 'CompletionStatus'
		];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}
	    var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		//服务状态
		//根据状态改变，选择处理中和已完成选项，处理人变为当前登录者
		var Status = ServiceOperation.getComponent('PCustomerService_Status');
		if(Status) {
			Status.on({
				change: function(com,newValue,oldValue,eOpts ) {
					if(newValue=='2' || newValue=='3' ){
						ServiceOperationCompleteMan.setValue(userName);
					    ServiceOperationCompleteManID.setValue(userId);
					}else{
						ServiceOperationCompleteMan.setValue(me.ServiceOperationCompleteMan.Name);
					    ServiceOperationCompleteManID.setValue(me.ServiceOperationCompleteMan.ID);
					}
				}
			});
		}
	
	},
	/**字典监听*/
	doDictListeners: function(name) {
		var me = this;
		var ServiceReturn = me.getComponent('ServiceReturn');
		var CName = ServiceReturn.getComponent('PCustomerService_' + name + 'Name');
		var Id = me.getComponent('PCustomerService_' + name + 'ID');
		if(!CName) return;

		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				p.close();
			}
		});
	},
	/**创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '用户名称ID',
			hidden: true,
			name: 'PCustomerService_ClientID',
			itemId: 'PCustomerService_ClientID'
		});
		items.push({
			fieldLabel: '省份ID',
			hidden: true,
			name: 'PCustomerService_ProvinceID',
			itemId: 'PCustomerService_ProvinceID'
		});
		items.push({
			fieldLabel: '受理人ID',
			hidden: true,
			name: 'PCustomerService_ServiceAcceptanceManID',
			itemId: 'PCustomerService_ServiceAcceptanceManID'
		});
		items.push({
			fieldLabel: '处理人ID',
			hidden: true,
			name: 'PCustomerService_ServiceOperationCompleteManID',
			itemId: 'PCustomerService_ServiceOperationCompleteManID'
		});
		items.push({
			fieldLabel: '服务方式ID',
			hidden: true,
			name: 'PCustomerService_ServiceModeID',
			itemId: 'PCustomerService_ServiceModeID'
		});
		items.push({
			fieldLabel: '回访人ID',
			hidden: true,
			name: 'PCustomerService_ServiceReturnManID',
			itemId: 'PCustomerService_ServiceReturnManID'
		});
		items.push({
			fieldLabel: '用户满意度ID',
			hidden: true,
			name: 'PCustomerService_SatisfactionID',
			itemId: 'PCustomerService_SatisfactionID'
		});
		items.push({
			fieldLabel: '完成情况ID',
			hidden: true,
			name: 'PCustomerService_CompletionStatusID',
			itemId: 'PCustomerService_CompletionStatusID'
		});
		items.push({
			fieldLabel: '服务状态',
			hidden: true,
			name: 'PCustomerService_StatusID',
			itemId: 'PCustomerService_StatusID'
		},{
			fieldLabel: '服务状态',
			hidden: true,
			name: 'PCustomerService_Status',
			itemId: 'PCustomerService_Status'
		});
		return items;
	},
	/**获取状态列表*/
	getStatusData: function(StatusListData) {
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
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this;
		var fields = ['Id', 'IsProxy', 'ClientID', 'ClientName',
			'ProvinceID', 'ProvinceName', 'RequestMan',
			'RequestManPhone', 'ServiceAcceptanceManID', 'ServiceAcceptanceMan', 'ServiceAcceptanceDate',
			'ProblemMemo', 'Status', 'ServiceModeID', 'ServiceModeName',
			'ServiceOperationCompleteManID', 'ServiceOperationCompleteMan',
			'ServiceFinishDate', 'ServiceOperationCompleteMemo', 'ServiceReturnManID', 'ServiceReturnMan',
			'ServiceReturnDate', 'SatisfactionID', 'SatisfactionName',
			'ServiceReturnToMan', 'ServiceReturnToManPhone', 'CompletionStatusName',
			'CompletionStatusID', 'ServiceReturnMemo', 'ServiceOperationDate'
		];
		fields = "PCustomerService_" + fields.join(",PCustomerService_");
		fields = fields.split(',');
		return fields;
	}
});