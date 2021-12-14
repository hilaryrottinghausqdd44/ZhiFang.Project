/**
 * @description 部门采购申请录入
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.ApplyForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '申请录入',
	width: 405,
	height: 185,
	header: false,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaBmsReqDocAndDt',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDt',

	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**申请单当前状态*/
	Status: '1',
	/**申请单当前中文名称状态*/
	StatusName: "",
	/**新增或编辑时需要提交保存的数据*/
	saveParams: null,
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**申请单状态Key*/
	StatusKey: "ReaBmsReqDocStatus",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	initComponent: function() {
		var me = this;
		me.addEvents('hrdptcheck', 'isEditAfter');
		me.width = me.width || 405;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 160) me.defaults.width = 160;
		
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//根据登录者的部门id 查询
		var depID = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);

		items.push({
			fieldLabel: '部门选择',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaBmsReqDoc_DeptName',
			itemId: 'ReaBmsReqDoc_DeptName',
			colspan: 2,
			width: me.defaults.width * 2,
			snotField: true,
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			value: me.ParentName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.CheckOrgTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**显示所有部门树:false;只显示用户自己的树:true*/
					ISOWN: true,
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onDeptAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '部门主键ID',
			hidden: true,
			name: 'ReaBmsReqDoc_DeptID',
			itemId: 'ReaBmsReqDoc_DeptID'
		});

		items.push({
			fieldLabel: '申请单号',
			name: 'ReaBmsReqDoc_ReqDocNo',
			readOnly: true,
			locked: true,
			colspan: 2,
			width: me.defaults.width * 2
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsReqDoc_Id',
			hidden: true,
			type: 'key'
		});

		//申请人
		items.push({
			fieldLabel: '申请人',
			name: 'ReaBmsReqDoc_ApplyName',
			itemId: 'ReaBmsReqDoc_ApplyName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '申请人ID',
			name: 'ReaBmsReqDoc_ApplyID',
			itemId: 'ReaBmsReqDoc_ApplyID',
			hidden: true
		});
		//申请日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '申请日期',
			format: 'Y-m-d', // H:m:s
			name: 'ReaBmsReqDoc_ApplyTime',
			itemId: 'ReaBmsReqDoc_ApplyTime',
			width: me.defaults.width * 1,
			colspan: 1
		});
		//紧急标志
		items.push({
			fieldLabel: '紧急标志',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsReqDoc_UrgentFlag',
			itemId: 'ReaBmsReqDoc_UrgentFlag',
			colspan: 1,
			width: me.defaults.width * 1,
			data: JShell.REA.Enum.getList('BmsCenSaleDoc_UrgentFlag'),
			allowBlank: false,
			hasStyle: true,
			value: "0"
		});
		//单据状态
		items.push({
			xtype: 'uxSimpleComboBox',
			fieldLabel: '单据状态',
			name: 'ReaBmsReqDoc_Status',
			itemId: 'ReaBmsReqDoc_Status',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			hasStyle: true,
			border: false,
			data: JShell.REA.StatusList.Status[me.StatusKey].List,
			value: me.Status
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsReqDoc_Memo',
			height: 40,
			colspan: 4,
			width: me.defaults.width * 4
		});
		//备注
		items.push({
			fieldLabel: '是否启用',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsReqDoc_Visible',
			itemId: 'ReaBmsReqDoc_Visible',
			data: [
				[1, "启用"],
				[0, "禁用"]
			],
			value: 1,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//审核人
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核人',
			name: 'ReaBmsReqDoc_ReviewManName',
			itemId: 'ReaBmsReqDoc_ReviewManName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '审核人ID',
			name: 'ReaBmsReqDoc_ReviewManID',
			itemId: 'ReaBmsReqDoc_ReviewManID',
			hidden: true
		});
		//审核日期
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核日期',
			format: 'Y-m-d',
			name: 'ReaBmsReqDoc_ReviewTime',
			itemId: 'ReaBmsReqDoc_ReviewTime',
			colspan: 1,
			border: false,
			readOnly: true,
			locked: true
		});
		//操作日期
		items.push({
			xtype: 'displayfield',
			fieldLabel: '操作日期',
			format: 'Y-m-d H:m:s',
			name: 'ReaBmsReqDoc_OperDate',
			itemId: 'ReaBmsReqDoc_OperDate',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核意见',
			itemId: 'ReaBmsReqDoc_ReviewMemo',
			name: 'ReaBmsReqDoc_ReviewMemo',
			colspan: 4,
			width: me.defaults.width * 4
		});
		return items;
	},
	/**部门选择*/
	onDeptAccept: function(record) {
		var me = this,
			deptID = me.getComponent('ReaBmsReqDoc_DeptID'),
			deptName = me.getComponent('ReaBmsReqDoc_DeptName');
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		deptID.setValue((record ? record.get('tid') : ''));
		deptName.setValue(text);
		me.fireEvent('hrdptcheck', me, record);
	},
	getAddFormDefault: function() {
		var me = this;
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) || "";
		var Sysdate = JcallShell.System.Date.getDate();
		var curSysDateTime = JcallShell.Date.toString(Sysdate, true);
		var addFormDefault = {
			'ReaBmsReqDoc_DeptID': deptId,
			'ReaBmsReqDoc_DeptName': deptName,
			'ReaBmsReqDoc_ReqDocNo': "",
			'ReaBmsReqDoc_ApplyName': userName,
			'ReaBmsReqDoc_ApplyID': userId,
			'ReaBmsReqDoc_ApplyTime': curSysDateTime,
			"ReaBmsReqDoc_Visible": 1
		};
		return addFormDefault;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var addFormDefault = me.getAddFormDefault();
		if(addFormDefault) me.getForm().setValues(addFormDefault);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		me.saveParams = null;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;

		if(!values.ReaBmsReqDoc_DeptID) {
			JShell.Msg.alert("申请部门信息为空!", null, 1000);
			return;
		}
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		me.fireEvent('beforesave', me);
		var params = me.getSaveParams();
		if(!params) {
			JShell.Msg.error("封装提交信息出错!");
			return;
		}
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				id += '';
				me.fireEvent('save', me, true, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var Visible = values.ReaBmsReqDoc_Visible;
		if(!Visible) Visible = 1;
		var entity = {
			ReqDocNo: values.ReaBmsReqDoc_ReqDocNo,
			UrgentFlag: values.ReaBmsReqDoc_UrgentFlag,
			Visible: Visible,
			Status: me.Status,
			ApplyName: values.ReaBmsReqDoc_ApplyName,
			DeptName: values.ReaBmsReqDoc_DeptName,
			Memo: values.ReaBmsReqDoc_Memo
		};
		if(values.ReaBmsReqDtl_ApplyID) entity.ApplyID = values.ReaBmsReqDtl_ApplyID;
		if(values.ReaBmsReqDoc_DeptID) entity.DeptID = values.ReaBmsReqDoc_DeptID;
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate);
		var applyTime = curDateTime;
		if(values.ReaBmsReqDoc_ApplyTime) {
			applyTime = values.ReaBmsReqDoc_ApplyTime;
			if(JShell.Date.toServerDate(applyTime)) {
				entity.ApplyTime = JShell.Date.toServerDate(applyTime);
			}
		}
		if(me.formtype == "edit") {
			if(JShell.Date.toServerDate(curDateTime)) {
				entity.DataUpdateTime = JShell.Date.toServerDate(curDateTime);
			}
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
		entity.fields = 'Id,DeptID,DeptName,Status,UrgentFlag,Memo,Visible,ReviewMemo';
		if(values.ReaBmsReqDoc_ApplyTime && me.OTYPE == "entry") entity.fields += ",ApplyTime";
		if(me.formtype == "edit") entity.fields += ",DataUpdateTime";
		entity.entity.Id = values.ReaBmsReqDoc_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var visible = data.ReaBmsReqDoc_Visible;
		if(visible == "true" || visible == true) visible = '1';
		visible = (visible == '1' ? 1 : 0);
		data.ReaBmsReqDoc_Visible = visible;
		if(data.ReaBmsReqDoc_OperDate) data.ReaBmsReqDoc_OperDate = JcallShell.Date.toString(data.ReaBmsReqDoc_OperDate, true);
		if(data.ReaBmsReqDoc_ReviewTime) data.ReaBmsReqDoc_ReviewTime = JcallShell.Date.toString(data.ReaBmsReqDoc_ReviewTime, true);
		if(data.ReaBmsReqDoc_ApplyTime) data.ReaBmsReqDoc_ApplyTime = JcallShell.Date.toString(data.ReaBmsReqDoc_ApplyTime, true);

		//以中文名称显示
		//if(me.StatusName) data.ReaBmsReqDoc_Status = me.StatusName;
		return data;
	},
	/**@description 获取保存提交数据*/
	getSaveParams: function() {
		var me = this;
		var entity = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!me.saveParams) {
			me.saveParams = {
				entity: entity.entity,
				"dtAddList": []
			}
		}
		me.saveParams.entity = entity.entity;
		if(me.formtype == 'edit') {
			me.saveParams.fields = entity.fields;
		}
		return me.saveParams;
	},
	/**@description 保存前的提交参数再次处理*/
	setSaveParams: function(params) {
		var me = this;
		if(!me.saveParams) {
			me.saveParams = {
				"entity": null,
				"dtAddList": []
			}
		}
		me.saveParams = params;
	}
});