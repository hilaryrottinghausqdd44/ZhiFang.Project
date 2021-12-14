/**
 * 退库入库
 * @author liangyl
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.out.refund.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '退库入库',
	width: 680,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?isPlanish=true',

	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	formtype: 'add',
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 155,
		labelAlign: 'right'
	},

	StatusList: [],
	InDocInTypeList: [],
	/**入库类型,退库入库3*/
	defalutInType: '3',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		me.defaults.width = parseInt(me.width / me.layout.columns);
		if (me.defaults.width < 155) me.defaults.width = 155;

		me.getStatusListData();
		me.getInDocInTypeList();

		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//送货人
		items.push({
			fieldLabel: '送货人',
			name: 'ReaBmsInDoc_Carrier',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//入库类型
		items.push({
			fieldLabel: '入库类型',
			emptyText: '必填项',
			name: 'ReaBmsInDoc_InType',
			itemId: 'ReaBmsInDoc_InType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.InDocInTypeList,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: me.defalutInType,
			className: 'Shell.class.rea.client.storagetype.CheckGrid',
			listeners: {
				check: function(p, record) {
					p.close();
				}
			}
		});
		//操作者
		items.push({
			fieldLabel: '操作人',
			name: 'ReaBmsInDoc_CreaterName',
			itemId: 'ReaBmsInDoc_CreaterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '操作人员ID',
			hidden: true,
			name: 'ReaBmsInDoc_CreaterID',
			itemId: 'ReaBmsInDoc_CreaterID'
		});
		//单据状态
		items.push({
			fieldLabel: '单据状态',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsInDoc_Status',
			itemId: 'ReaBmsInDoc_Status',
			hasStyle: true,
			hidden: true,
			data: me.StatusList,
			colspan: 1,
			width: me.defaults.width * 1,
			//allowBlank: false,
			readOnly: true,
			locked: true
		});
		//操作日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '操作日期',
			format: 'Y-m-d',
			name: 'ReaBmsInDoc_OperDate',
			itemId: 'ReaBmsInDoc_OperDate',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});

		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsInDoc_Id',
			itemId: 'ReaBmsInDoc_Id',
			hidden: true,
			type: 'key'
		});

		//发票号
		items.push({
			xtype: 'textarea',
			fieldLabel: '发票号',
			name: 'ReaBmsInDoc_InvoiceNo',
			itemId: 'ReaBmsInDoc_InvoiceNo',
			colspan: 2,
			width: me.defaults.width * 2,
			height: 20
		});
		//入库总单号
		items.push({
			fieldLabel: '入库单号',
			name: 'ReaBmsInDoc_InDocNo',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//	总单金额
		items.push({
			fieldLabel: '合计金额',
			name: 'ReaBmsInDoc_TotalPrice',
			itemId: 'ReaBmsInDoc_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsInDoc_Memo',
			itemId: 'ReaBmsInDoc_Memo',
			colspan: 4,
			width: me.defaults.width * 4,
			height: 50
		});
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if (data.ReaBmsInDoc_OperDate) data.ReaBmsInDoc_OperDate = JcallShell.Date.toString(data.ReaBmsInDoc_OperDate,
			true);
		var reg = new RegExp("<br />", "g");
		data.ReaBmsInDoc_Memo = data.ReaBmsInDoc_Memo.replace(reg, "\r\n");
		return data;
	},
	/**获取状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if (me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsInDocStatus",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.StatusList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if (data.success) {
				if (data.value) {
					if (data.value[0].ReaBmsInDocStatus.length > 0) {
						me.StatusList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if (obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**客户端入库类型*/
	getInDocInTypeList: function(callback) {
		var me = this;
		if (me.InDocInTypeList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsInDocInType",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.InDocInTypeList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if (data.success) {
				if (data.value) {
					if (data.value[0].ReaBmsInDocInType.length > 0) {
						me.InDocInTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocInType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if (obj.BGColor) {
								style.push('color:' + obj.BGColor);
							}
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.InDocInTypeList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: values.ReaBmsInDoc_Id,
			Status: 2,
			InDocNo: values.ReaBmsInDoc_InDocNo,
			Carrier: values.ReaBmsInDoc_Carrier,
			InType: values.ReaBmsInDoc_InType,
			CreaterName: values.ReaBmsInDoc_CreaterName,
			InvoiceNo: values.ReaBmsInDoc_InvoiceNo
		};
		entity.Memo = values.ReaBmsInDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		if (values.ReaBmsInDoc_CreaterID) entity.CreaterID = values.ReaBmsInDoc_CreaterID;
		if (values.ReaBmsInDoc_OperDate) entity.OperDate = JShell.Date.toServerDate(values.ReaBmsInDoc_OperDate);
		if (values.ReaBmsInDoc_TotalPrice) entity.TotalPrice = parseFloat(values.ReaBmsInDoc_TotalPrice);
		return entity;
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		// 'OperDate', 'TotalPrice',后台处理
		var fields = [
			'Id', 'Status', 'InDocNo', 'Carrier', 'InType', 'CreaterName', 'InvoiceNo', 'Memo', 'CreaterID'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsInDoc_Id;
		return entity;
	},

	isAdd: function(docId) {
		var me = this;
		me.callParent(arguments);
		me.getComponent('buttonsToolbar').hide();
		me.getComponent('ReaBmsInDoc_Id').setValue(docId);
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if (!me.PK) {
			this.getForm().reset();
		} else {
			me.getForm().setValues(me.lastData);
		}
		var CreaterID = me.getComponent('ReaBmsInDoc_CreaterID');
		var CreaterName = me.getComponent('ReaBmsInDoc_CreaterName');
		CreaterID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		CreaterName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));

		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);

		var OperDate = me.getComponent('ReaBmsInDoc_OperDate');
		OperDate.setValue(curDateTime);
		var InType = me.getComponent('ReaBmsInDoc_InType');
		InType.setValue(me.defalutInType);
	},
	setTotalPrice: function(totalPrice) {
		var me = this;
		var TotalPrice = me.getComponent('ReaBmsInDoc_TotalPrice');
		TotalPrice.setValue(totalPrice);
	}
});
