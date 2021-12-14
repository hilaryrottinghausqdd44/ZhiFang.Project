/**
 * 财务收款详情
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.receive.finance.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '财务收款详情信息',
	requires: [
	    'Shell.ux.form.field.CheckTrigger'
	],
	width: 240,
	height: 320,
	bodyPadding: 10,
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	//	selectUrl: '/ui/class/wfm/finance/form.json',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPFinanceReceiveById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPFinanceReceive',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePFinanceReceiveByField',
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	/**付款单位*/
	PayOrgID: null,
	/**付款单位*/
	PayOrgName: '',
	/*执行公司名称*/
	Compname: 'OurCorName',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},

	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		items.push(me.createbtnToolbar());
		return items;
	},
	/**创建功能按钮栏*/
	createbtnToolbar: function() {
		var me = this,
			items = [{
				xtype: 'label',
				text: '财务明细',
				margin: '0 0 0 10',
				style: "font-weight:bold;color:blue;",
				itemId: 'EMaintenanceData_Date',
				name: 'EMaintenanceData_Date'
			}];
		if(items.length == 0) return null;
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			height: 28,
			itemId: 'btnToolbar',
			items: items
		});
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			xtype: 'label',
			style: "font-weight:bold;color:blue;font-size:18px",
			name: 'PFinanceReceive_PClient_Name',
			itemId: 'PFinanceReceive_PClient_Name',
			text: ''
		},{
			fieldLabel: '执行公司',
			name: 'PFinanceReceive_ComponeName',
			itemId: 'PFinanceReceive_ComponeName',
			colspan: 2,
			width: me.defaults.width * 2,
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '本公司名称选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.Compname + "'"
			}
		},  {
			fieldLabel: '收款时间',
			name: 'PFinanceReceive_ReceiveDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '收款金额',
			name: 'PFinanceReceive_ReceiveAmount',
			minValue: 0,
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			height: 85,
			emptyText: '备注',
			name: 'PFinanceReceive_ReceiveMemo',
			xtype: 'textarea'
		}, {
			fieldLabel: '主键ID',
			name: 'PFinanceReceive_Id',
			hidden: true
		}, {
			fieldLabel: '已分配金额',
			name: 'PFinanceReceive_SplitAmount',
			hidden: true
		},{
			itemId:'PFinanceReceive_CompnameID',
			name:'PFinanceReceive_CompnameID',
			fieldLabel:'执行公司ID',
			hidden:true
		});
		return items;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		if(!me.getForm().isValid()) return;

		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if(!params) return;
		var id = params.entity.Id;
		params = Ext.JSON.encode(params);
		var values = me.getForm().getValues();
		var ReceiveAmount = values.PFinanceReceive_ReceiveAmount;
		var SplitAmount = values.PFinanceReceive_SplitAmount;
		if(parseFloat(ReceiveAmount) < parseFloat(SplitAmount)) {
			JShell.Msg.error('收款金额不能少于已分配金额!');
			return;
		}
		me.fireEvent('beforesave', me);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object') {
					id = id.id;
				}
				me.fireEvent('save', me, id);
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
		var entity = {
			ReceiveDate: JShell.Date.toServerDate(values.PFinanceReceive_ReceiveDate),
			ReceiveAmount: values.PFinanceReceive_ReceiveAmount,
			ReceiveMemo: values.PFinanceReceive_ReceiveMemo,
			IsUse:1
		};
		if(me.PayOrgID) {
			entity.PayOrgID = me.PayOrgID;
		}
		if(me.PayOrgName) {
			entity.PayOrgName = me.PayOrgName;
		}
		if(values.PFinanceReceive_CompnameID) {
			entity.CompnameID = values.PFinanceReceive_CompnameID;
		}
		if(values.PFinanceReceive_ComponeName) {
			entity.ComponeName = values.PFinanceReceive_ComponeName;
		}
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		entity.InputerID=userId;
		entity.InputerName = userName;
	   
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = [
			'ReceiveDate', 'Id', 'ReceiveAmount', 'ReceiveMemo',
			'CompnameID','ComponeName'
		];
		entity.fields = fields.join(',');
		if(values.PFinanceReceive_Id != '') {
			entity.entity.Id = values.PFinanceReceive_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.PFinanceReceive_ReceiveDate = JShell.Date.getDate(data.PFinanceReceive_ReceiveDate);
		return data;
	},
	  /**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		//执行公司
		var CName = me.getComponent('PFinanceReceive_ComponeName');
		var Id = me.getComponent('PFinanceReceive_CompnameID');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BDict_CName') : '');
				Id.setValue(record ? record.get('BDict_Id') : '');
				p.close();
			}
		});
	}
});