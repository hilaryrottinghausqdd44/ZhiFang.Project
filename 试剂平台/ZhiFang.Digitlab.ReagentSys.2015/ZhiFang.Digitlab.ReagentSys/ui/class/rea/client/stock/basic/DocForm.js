/**
 * 客户端入库
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.stock.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		  'Shell.ux.form.field.CheckTrigger'
	],
	title: '入库信息',

	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchReaBmsInDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsInDocOfManualInput',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocOfManualInput',
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
		columns: 5 //每行有几列
	},
	StatusList:[['1','暂存'],['2','已完成入库']],
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 185,
		labelAlign: 'right'
	},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('reacompcheck', 'isEditAfter');
		me.width = me.width || 405;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 160) me.defaults.width = 160;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//供应商
		items.push( {
			fieldLabel: '供应商',name: 'ReaBmsInDoc_CompanyName',itemId: 'ReaBmsInDoc_CompanyName',
			emptyText: '必填项',allowBlank: false,xtype: 'uxCheckTrigger',
			colspan: 2, width: me.defaults.width * 2,
			className: 'Shell.class.rea.client.cenorg.basic.CheckGrid',
			classConfig: {
				title: '供应商选择',checkOne:true,
				defaultWhere:'reacenorg.OrgType=0',
				width:300
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(record);
					p.close();
				}
			}
		},{
			fieldLabel: '供货方主键ID',
			hidden: true,
			name: 'ReaBmsInDoc_CompanyID',
			itemId: 'ReaBmsInDoc_CompanyID'
		});
        //入库类型
		items.push({
			fieldLabel: '入库类型',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaBmsInDoc_InTypeName',
			itemId: 'ReaBmsInDoc_InTypeName',
			xtype: 'uxCheckTrigger',
			colspan: 1,
			width: me.defaults.width * 1,
			className: 'Shell.class.rea.client.storagetype.CheckGrid',
			listeners: {
				check: function(p, record) {
					me.onStorageTypeAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '入库类型ID',
			hidden: true,
			name: 'ReaBmsInDoc_InType',
			itemId: 'ReaBmsInDoc_InType'
		});
	//	总单金额
		items.push({
			fieldLabel: '合计金额',
			name: 'ReaBmsInDoc_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//单据状态
		items.push({
			fieldLabel: '单据状态',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsInDoc_Status',
			itemId: 'ReaBmsInDoc_Status',
			hasStyle: true,
			data: me.StatusList,
			value: me.Status,
			colspan: 1,
			width: me.defaults.width * 1,
			allowBlank: false,
			readOnly: true,
			locked: true
		});
		//供货单号
		items.push({
			fieldLabel: '入库总单号',
			name: 'ReaBmsInDoc_InDocNo',
			readOnly: true,
			locked: true,
			colspan: 2,
			width: me.defaults.width * 2
		});
//		//	打印次数
//		items.push({
//			fieldLabel: '打印次数',
//			name: 'ReaBmsInDoc_PrintTimes',
//			colspan: 1,
//			width: me.defaults.width * 1
//		});
		
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsInDoc_Id',
			hidden: true,
			type: 'key'
		});

		
	
        
		//发票号
		items.push({
			fieldLabel: '发票号',
			name: 'ReaBmsInDoc_InvoiceNo',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//送货人
		items.push({
			fieldLabel: '送货人',
			name: 'ReaBmsInDoc_Carrier',
			itemId: 'ReaBmsInDoc_Carrier',
			colspan: 1,
			width: me.defaults.width * 1
//			readOnly: true,
//			locked: true
		}, {
			fieldLabel: '操作人员ID',
			hidden: true,
			name: 'ReaBmsInDoc_UserID',
			itemId: 'ReaBmsInDoc_UserID'
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
//		//被操作入库总单号
//		items.push({
//			fieldLabel: '被入库总单',
//			name: 'ReaBmsInDoc_OperateInDocNo',
//			colspan: 2,
//			width: me.defaults.width * 2
//		});
		
	    //操作者
		items.push({
			fieldLabel: '操作者',
			name: 'ReaBmsInDoc_CreaterName',
			itemId: 'ReaBmsInDoc_CreaterName',
			colspan: 1,hidden:true,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '操作人员ID',
			hidden: true,
			name: 'ReaBmsInDoc_CreaterID',
			itemId: 'ReaBmsInDoc_CreaterID'
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsInDoc_Memo',
			itemId: 'ReaBmsInDoc_Memo',
			colspan: 5,
			width: me.defaults.width * 5,
			height: 50
		});

	
		return items;
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.fireEvent('isEditAfter', me);
	},
	isAdd:function(){
		var me = this;
		me.setReadOnly(false);
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle();//标题更改
		me.enableControl();//启用所有的操作功能
		me.onResetClick();
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);
		var DataAddTime = me.getComponent('ReaBmsInDoc_DataAddTime');
		DataAddTime.setValue(curDateTime);
		var AcceptTime = me.getComponent('ReaBmsInDoc_OperDate');
		AcceptTime.setValue(curDateTime);
	},
	
	/**订货方选择*/
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaBmsInDoc_CompanyID');
		var ComName = me.getComponent('ReaBmsInDoc_CompanyName');

		ComId.setValue(record ? record.get('ReaCenOrg_Id') : '');
		ComName.setValue(record ? record.get('ReaCenOrg_CName') : '');
		me.fireEvent('reacompcheck', me, record);
	},
    /**订货方选择*/
	onStorageTypeAccept: function(record) {
		var me = this;
	    var InTypeId = me.getComponent('ReaBmsInDoc_InType');
		var InTypeName = me.getComponent('ReaBmsInDoc_InTypeName');

		InTypeId.setValue(record ? record.get('BStorageType_Id') : '');
		InTypeName.setValue(record ? record.get('BStorageType_Name') : '');

		me.fireEvent('storagetypecheck', me, record);
	},
	
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(data.ReaBmsInDoc_DataAddTime) data.ReaBmsInDoc_DataAddTime = JcallShell.Date.toString(data.ReaBmsInDoc_DataAddTime, true);
		if(data.ReaBmsInDoc_OperDate) data.ReaBmsInDoc_OperDate = JcallShell.Date.toString(data.ReaBmsInDoc_OperDate, true);
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			InDocNo: values.ReaBmsInDoc_InDocNo,
			Memo: values.ReaBmsInDoc_Memo,
			Carrier: values.ReaBmsInDoc_Carrier
		};
		if(values.ReaBmsInDoc_CompanyID){
			entity.CompanyID = values.ReaBmsInDoc_CompanyID;
			entity.CompanyName = values.ReaBmsInDoc_CompanyName;
		} 
		var userid=JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var username=JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userid){
			entity.UserID = userid;
		}
		if(values.ReaBmsInDoc_DataAddTime) entity.DataAddTime = JShell.Date.toServerDate(values.ReaBmsInDoc_DataAddTime);
		if(values.ReaBmsInDoc_OperDate) entity.OperDate = JShell.Date.toServerDate(values.ReaBmsInDoc_OperDate);
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
			'Id', 'SaleDocConfirmNo', 'ReaCompID', 'ReaCompName', 'Memo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsInDoc_Id;
		return entity;
	}
});