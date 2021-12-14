/**
 * 客户端入库
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.stock.confirm.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '入库信息',

	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsCenSaleDocConfirm',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDocConfirm',
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
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 70,
		width: 190,
		labelAlign: 'right'
	},

	formtype: 'show',
	StatusIDList: [],
	PK: null,
	InDocInTypeList: [],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.width = me.width || 405;
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 160) me.defaults.width = 160;
		me.getInDocInTypeList();
		me.getStatusIDListData();
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//供应商
		items.push({
			fieldLabel: '供应商',
			name: 'ReaBmsCenSaleDocConfirm_ReaCompanyName',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaCompanyName',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'uxCheckTrigger',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true,
			className: 'Shell.class.rea.client.reacenorg.basic.CheckGrid',
			classConfig: {
				title: '供应商选择',
				checkOne: true,
				defaultWhere: 'reacenorg.OrgType=0',
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '供货方主键ID',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_ReaCompID',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaCompID'
		}, {
			fieldLabel: '入库类型',
			emptyText: '必填项',
			name: 'ReaBmsInDoc_InType',
			itemId: 'ReaBmsInDoc_InType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.InDocInTypeList,
			colspan: 1,
			hidden: true,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: "1"
		}, {
			fieldLabel: '入库总单号',
			name: 'ReaBmsCenSaleDocConfirm_InDocNo',
			hidden: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '单据状态',
			//			emptyText: '必填项',
			name: 'ReaBmsInDoc_StatusID',
			itemId: 'ReaBmsInDoc_StatusID',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			hidden: true,
			data: me.StatusIDList,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: "2"
		}, {
			fieldLabel: '入库类型ID',
			hidden: true,
			value: me.defaultInType,
			name: 'ReaBmsCenSaleDocConfirm_InType',
			itemId: 'ReaBmsCenSaleDocConfirm_InType'
		}, {
			fieldLabel: '打印次数',
			name: 'ReaBmsCenSaleDocConfirm_PrintTimes',
			colspan: 1,
			hidden: true,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '入库发票号',
			name: 'ReaBmsCenSaleDocConfirm_InvoiceNo',
			colspan: 1,
			hidden: true,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '供货单号',
			name: 'ReaBmsCenSaleDocConfirm_SaleDocNo',
			colspan: 1,
			hidden: true,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '主键ID',
			name: 'ReaBmsCenSaleDocConfirm_Id',
			hidden: true,
			type: 'key'
		});
		items.push({
			fieldLabel: '所属部门',
			emptyText: '所属部门',
			name: 'ReaBmsInDoc_DeptName',
			itemId: 'ReaBmsInDoc_DeptName',
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
			name: 'ReaBmsInDoc_DeptID',
			itemId: 'ReaBmsInDoc_DeptID'
		});
		items.push({
			fieldLabel: '总单金额',
			name: 'ReaBmsCenSaleDocConfirm_TotalPrice',
			itemId: 'ReaBmsCenSaleDocConfirm_TotalPrice',
			value: 0,
			colspan: 1,
			readOnly: true,
			locked: true,
			width: me.defaults.width * 1
		}, {
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsCenSaleDocConfirm_Memo',
			itemId: 'ReaBmsCenSaleDocConfirm_Memo',
			colspan: 5,
			width: me.defaults.width * 5,
			height: 50
		});
		return items;
	},
	isEdit: function(id) {
		var me = this;
		me.setReadOnly(false);
		me.formtype = 'edit';
		me.changeTitle(); //标题更改
		me.load(me.PK);
	},
	isAdd: function() {
		var me = this;
		var deptIdV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		var deptNameV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
		var deptId = me.getComponent('ReaBmsInDoc_DeptID');
		var deptName = me.getComponent('ReaBmsInDoc_DeptName');
		if(deptId) deptId.setValue(deptIdV);
		if(deptName) deptName.setValue(deptNameV);
		me.setReadOnly(false);
		me.formtype = 'add';
		me.PK = '';
		me.changeTitle(); //标题更改
		me.enableControl(); //启用所有的操作功能
		me.onResetClick();
	},
	/**更改标题*/
	changeTitle: function() {},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			InDocNo: values.ReaBmsCenSaleDocConfirm_InDocNo,
			Memo: values.ReaBmsCenSaleDocConfirm_Memo
		};
		if(values.ReaBmsCenSaleDocConfirm_TotalPrice) {
			//			entity.TotalPrice=values.ReaBmsCenSaleDocConfirm_TotalPrice;
		}
		if(values.ReaBmsCenSaleDocConfirm_SaleDocNo) {
			entity.SaleDocNo = values.ReaBmsCenSaleDocConfirm_SaleDocNo;
		}
		if(values.ReaBmsCenSaleDocConfirm_Id) {
			entity.SaleDocConfirmID = values.ReaBmsCenSaleDocConfirm_Id;
		}
		if(values.ReaBmsCenSaleDocConfirm_InvoiceNo) {
			entity.InvoiceNo = values.ReaBmsCenSaleDocConfirm_InvoiceNo;
		}
		if(values.ReaBmsCenSaleDocConfirm_ReaCompID) {
			entity.CompanyID = values.ReaBmsCenSaleDocConfirm_ReaCompID;
			entity.CompanyName = values.ReaBmsCenSaleDocConfirm_ReaCompanyName;
		}
		var InType = me.getComponent('ReaBmsInDoc_InType');
		if(values.ReaBmsInDoc_InType) {
			entity.InType = values.ReaBmsInDoc_InType;
			//entity.InTypeName = InType.getRawValue();
		}
		var Status = me.getComponent('ReaBmsInDoc_StatusID');
		if(values.ReaBmsInDoc_StatusID) {
			entity.Status = values.ReaBmsInDoc_StatusID;
			entity.StatusName = Status.getRawValue();
		}
		if(values.ReaBmsInDoc_DeptID) entity.DeptID = values.ReaBmsInDoc_DeptID;
		if(values.ReaBmsInDoc_DeptName) entity.DeptName = values.ReaBmsInDoc_DeptName;
		return entity;
	},
	/**获取状态信息*/
	getStatusIDListData: function(callback) {
		var me = this;
		if(me.StatusIDList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode({
			"jsonpara": [{
				"classname": "ReaBmsInDocStatus",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		});
		me.StatusIDList = [];
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsInDocStatus.length > 0) {
						me.StatusIDList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocStatus, function(obj, index) {
							//							var style = ['font-weight:bold;text-align:center;'];
							//							if(obj.BGColor) {
							//								style.push('color:' + obj.BGColor);
							//							}
							tempArr = [obj.Id, obj.Name];
							me.StatusIDList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},
	/**客户端入库类型*/
	getInDocInTypeList: function(callback) {
		var me = this;
		if(me.InDocInTypeList.length > 0) return;
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
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsInDocInType.length > 0) {
						me.InDocInTypeList.push(["", '请选择', 'font-weight:bold;text-align:center;']);
						Ext.Array.each(data.value[0].ReaBmsInDocInType, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.BGColor) {
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
	/**部门选择*/
	onDeptAccept: function(record) {
		var me = this,
			deptID = me.getComponent('ReaBmsInDoc_DeptID'),
			deptName = me.getComponent('ReaBmsInDoc_DeptName');
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		deptID.setValue((record ? record.get('tid') : ''));
		deptName.setValue(text);
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if(!data.ReaBmsInDoc_DeptID || data.ReaBmsInDoc_DeptID == "") {
			var deptIdV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
			var deptNameV = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME) || "";
			data.ReaBmsInDoc_DeptID=deptIdV;
			data.ReaBmsInDoc_DeptName=deptNameV;
		}
		return data;
	}
});