/**
 * 客户端供货单验收
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.add.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '验货单信息',
	width: 420,
	height: 280,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?isPlanish=true',

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
		columns: 6 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 185,
		labelAlign: 'right'
	},
	/**封装保存信息的验收单状态*/
	Status: "0",
	/**申请单当前中文名称状态*/
	StatusName: "",
	/**验货单数据来源类型*/
	SourceTypeValue: null,
	/**客户端验收单状态Key*/
	StatusKey: "ReaBmsCenSaleDocConfirmStatus",
	/**供货单数据来源Key*/
	SourceTypeKey: "ReaBmsCenSaleDocSource",
	/**供货单数据标志Key*/
	IOFlagKeyKey: "ReaBmsCenSaleDocIOFlag",

	hasOnlyOneComp: false,
	defaultCompInfo: null,
	hasLoadComp: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.SourceTypeKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);

		me.addEvents('reacompcheck', 'isEditAfter');
		if(me.defaults.width < 185) me.defaults.width = 185;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		//供货商
		items.push({
			fieldLabel: '供货商',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaBmsCenSaleDocConfirm_CompanyName',
			itemId: 'ReaBmsCenSaleDocConfirm_CompanyName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: me.formtype == "edit" ? true : false,
			locked: me.formtype == "edit" ? true : false,
			onTriggerClick: function() {
				me.onCompValidator();

				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel: '供货单号',
			name: 'ReaBmsCenSaleDocConfirm_SaleDocNo',
			itemId: 'ReaBmsCenSaleDocConfirm_SaleDocNo',
			colspan: 2,
			width: me.defaults.width * 2,
			listeners: {
				specialkey: function(field, e) {
					if(me.OTYPE == "reasale" && e.getKey() == Ext.EventObject.ENTER) {
						if(field.getValue()) {
							me.onSaleDocNo(field, e);
						} else {
							JShell.Msg.alert("供货单号不能为空!");
						}
					}
				}
			}
		});
		//总单金额
		items.push({
			fieldLabel: '总单金额',
			name: 'ReaBmsCenSaleDocConfirm_TotalPrice',
			itemId: 'ReaBmsCenSaleDocConfirm_TotalPrice',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			xtype: 'datefield',
			fieldLabel: '加入日期',
			hidden: true,
			format: 'Y-m-d',
			name: 'ReaBmsCenSaleDocConfirm_DataAddTime',
			itemId: 'ReaBmsCenSaleDocConfirm_DataAddTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});

		//验收时间
		items.push({
			xtype: 'datefield',
			fieldLabel: '验收日期',
			format: 'Y-m-d',
			name: 'ReaBmsCenSaleDocConfirm_AcceptTime',
			itemId: 'ReaBmsCenSaleDocConfirm_AcceptTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//货运单号
		items.push({
			xtype: 'textarea',
			fieldLabel: '货运单号',
			name: 'ReaBmsCenSaleDocConfirm_TransportNo',
			itemId: 'ReaBmsCenSaleDocConfirm_TransportNo',
			colspan: 2,
			width: me.defaults.width*2,
			height: 20
		});
		//发票号
		items.push({
			xtype: 'textarea',
			fieldLabel: '发票号',
			name: 'ReaBmsCenSaleDocConfirm_InvoiceNo',
			itemId: 'ReaBmsCenSaleDocConfirm_InvoiceNo',
			colspan: 3,
			width: me.defaults.width*3,
			height: 20
		});
		//主验收人
		items.push({
			fieldLabel: '主验收人',
			name: 'ReaBmsCenSaleDocConfirm_AccepterName',
			itemId: 'ReaBmsCenSaleDocConfirm_AccepterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '主验收人ID',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_AccepterID',
			itemId: 'ReaBmsCenSaleDocConfirm_AccepterID'
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsCenSaleDocConfirm_Memo',
			itemId: 'ReaBmsCenSaleDocConfirm_Memo',
			colspan: 6,
			width: me.defaults.width * 6,
			height: 40
		});
		//数据来源(供单ID和订单ID)
		items.push({
			fieldLabel: '数据来源',
			//xtype: 'uxSimpleComboBox',
			itemId: 'ReaBmsCenSaleDocConfirm_SourceType',
			name: 'ReaBmsCenSaleDocConfirm_SourceType',
			data: JShell.REA.StatusList.Status[me.SourceTypeKey].List,
			value: me.SourceTypeValue,
			hidden: true,
			hasStyle: true,
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsCenSaleDocConfirm_Id',
			hidden: true,
			type: 'key'
		}, {
			fieldLabel: '供货商主键ID',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_CompID',
			itemId: 'ReaBmsCenSaleDocConfirm_CompID'
		}, {
			fieldLabel: '供应商平台机构码',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_ReaServerCompCode',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaServerCompCode'
		}, {
			fieldLabel: '供应商机构码',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_ReaCompCode',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaCompCode'
		});

		items.push({
			fieldLabel: '实验室ID',
			name: 'ReaBmsCenSaleDocConfirm_LabcID',
			itemId: 'ReaBmsCenSaleDocConfirm_LabcID',
			hidden: true
		}, {
			fieldLabel: '实验名称',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_LabcName',
			itemId: 'ReaBmsCenSaleDocConfirm_LabcName'
		}, {
			fieldLabel: '实验室所属平台编码',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_ReaServerLabcCode',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaServerLabcCode'
		}, {
			fieldLabel: '本地供应商ID',
			name: 'ReaBmsCenSaleDocConfirm_ReaCompID',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaCompID',
			hidden: true
		}, {
			fieldLabel: '本地供应商名称',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_ReaCompanyName',
			itemId: 'ReaBmsCenSaleDocConfirm_ReaCompanyName'
		});

		//次验收人
		items.push({
			fieldLabel: '次验收人',
			name: 'ReaBmsCenSaleDocConfirm_SecAccepterName',
			itemId: 'ReaBmsCenSaleDocConfirm_SecAccepterName',
			hidden: true,
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '次验收人ID',
			hidden: true,
			name: 'ReaBmsCenSaleDocConfirm_SecAccepterID',
			itemId: 'ReaBmsCenSaleDocConfirm_SecAccepterID'
		});
		items.push({
			fieldLabel: '供货验收单号',
			name: 'ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo',
			itemId: 'ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo',
			hidden: true,
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		});
		return items;
	},
	onCompValidator: function() {
		return true;
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);

		var AccepterID = me.getComponent('ReaBmsCenSaleDocConfirm_AccepterID');
		var AccepterName = me.getComponent('ReaBmsCenSaleDocConfirm_AccepterName');
		AccepterID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		AccepterName.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));

		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate, true);
		var DataAddTime = me.getComponent('ReaBmsCenSaleDocConfirm_DataAddTime');
		DataAddTime.setValue(curDateTime);
		var AcceptTime = me.getComponent('ReaBmsCenSaleDocConfirm_AcceptTime');
		AcceptTime.setValue(curDateTime);
		var SourceType = me.getComponent('ReaBmsCenSaleDocConfirm_SourceType');
		SourceType.setValue(me.SourceTypeValue);

		me.getComponent('buttonsToolbar').hide();

		var LabcName = me.getComponent('ReaBmsCenSaleDocConfirm_LabcName');
		LabcName.setValue(JcallShell.REA.System.CENORG_NAME);

		var ReaServerLabcCode = me.getComponent('ReaBmsCenSaleDocConfirm_ReaServerLabcCode');
		ReaServerLabcCode.setValue(JcallShell.REA.System.CENORG_CODE);
		//新增验收时，如果供货方只有一家，最好默认，不要再去选择		
		if(me.hasOnlyOneComp == true && me.defaultCompInfo) {
			me.setCompValue(me.defaultCompInfo);
		} else if(me.hasLoadComp == false) {
			me.loadDefaultComp();
		}
	},
	loadDefaultComp: function() {
		var me = this;
		var me = this;
		var url = '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true';
		var fields = "ReaCenOrg_Id,ReaCenOrg_OrgNo,ReaCenOrg_PlatformOrgNo,ReaCenOrg_CName";
		/**OrgType:机构类型 0供货方，1订货方*/
		var where = "reacenorg.OrgType=0 and reacenorg.Visible=1";
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		url = url + "&fields=" + fields;
		url = url + "&where=" + where;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.hasLoadLabc = true;
				if(data.value && data.value.list && data.value.list.length == 1) {
					me.hasOnlyOneComp = true;
					var record = data.value.list[0];
					var labcInfo = {
						"ReaCompID": record["ReaCenOrg_Id"],
						"ReaCompCName": record["ReaCenOrg_CName"],
						"ReaCompCode": record["ReaCenOrg_OrgNo"],
						"PlatformOrgNo": record["ReaCenOrg_PlatformOrgNo"],
					};
					me.defaultCompInfo = labcInfo;
					me.setCompValue(labcInfo);
				}
			} else {
				JShell.Msg.error('获取订货方信息失败！' + data.msg);
			}
		});
	},
	setCompValue: function(labcInfo) {
		var me = this;
		if(!labcInfo) {
			labcInfo = {
				"ReaCompID": "",
				"ReaCompCName": "",
				"ReaCompCode": "",
				"PlatformOrgNo": ""
			};
		}
		var CompID = me.getComponent('ReaBmsCenSaleDocConfirm_CompID');
		var ComName = me.getComponent('ReaBmsCenSaleDocConfirm_CompanyName');

		var ReaCompID = me.getComponent('ReaBmsCenSaleDocConfirm_ReaCompID');
		var ReaCompanyName = me.getComponent('ReaBmsCenSaleDocConfirm_ReaCompanyName');
		var ReaCompCode = me.getComponent('ReaBmsCenSaleDocConfirm_ReaCompCode');
		var ReaServerCompCode = me.getComponent('ReaBmsCenSaleDocConfirm_ReaServerCompCode');

		CompID.setValue(labcInfo.ReaCompID);
		ComName.setValue(labcInfo.ReaCompCName);

		ReaCompID.setValue(labcInfo.ReaCompID);
		ReaCompanyName.setValue(labcInfo.ReaCompCName);
		ReaCompCode.setValue(labcInfo.ReaCompCode);
		ReaServerCompCode.setValue(labcInfo.PlatformOrgNo);
		me.fireEvent('reacompcheck', me, labcInfo);
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		//me.getComponent('ReaBmsCenSaleDocConfirm_CompID')
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if(data.ReaBmsCenSaleDocConfirm_DataAddTime) data.ReaBmsCenSaleDocConfirm_DataAddTime = JcallShell.Date.toString(data.ReaBmsCenSaleDocConfirm_DataAddTime, true);
		if(data.ReaBmsCenSaleDocConfirm_AcceptTime) data.ReaBmsCenSaleDocConfirm_AcceptTime = JcallShell.Date.toString(data.ReaBmsCenSaleDocConfirm_AcceptTime, true);

		var reg = new RegExp("<br />", "g");
		data.ReaBmsCenSaleDocConfirm_Memo = data.ReaBmsCenSaleDocConfirm_Memo.replace(reg, "\r\n");
		return data;
	},
	/**供货商选择*/
	onCompAccept: function(record) {
		var me = this;
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		var orgNo = record ? record.data.value.OrgNo : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		var objValue = {
			"ReaCompID": id,
			"ReaCompCName": text,
			"ReaCompCode": orgNo,
			"PlatformOrgNo": platformOrgNo
		};
		me.setCompValue(objValue);
		//me.fireEvent('reacompcheck', me, objValue);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Id: -1,
			Status: me.Status,
			DeleteFlag: 0,
			SaleDocNo: values.ReaBmsCenSaleDocConfirm_SaleDocNo,
			SaleDocConfirmNo: values.ReaBmsCenSaleDtlConfirm_SaleDocConfirmNo,
			ReaServerCompCode: values.ReaBmsCenSaleDocConfirm_ReaServerCompCode,
			AccepterName: values.ReaBmsCenSaleDocConfirm_AccepterName,
			SourceType: me.SourceTypeValue,
			LabcName: values.ReaBmsCenSaleDocConfirm_LabcName,
			ReaServerLabcCode: values.ReaBmsCenSaleDocConfirm_ReaServerLabcCode,
			CompanyName: values.ReaBmsCenSaleDocConfirm_CompanyName,
			ReaCompanyName: values.ReaBmsCenSaleDocConfirm_ReaCompanyName,
			ReaCompCode: values.ReaBmsCenSaleDocConfirm_ReaCompCode,
			TransportNo:values.ReaBmsCenSaleDocConfirm_TransportNo,
			InvoiceNo:values.ReaBmsCenSaleDocConfirm_InvoiceNo
		};
		entity.Memo = values.ReaBmsCenSaleDocConfirm_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');

		if(values.ReaBmsCenSaleDocConfirm_LabcID) entity.LabcID = values.ReaBmsCenSaleDocConfirm_LabcID;
		if(values.ReaBmsCenSaleDocConfirm_CompID) entity.CompID = values.ReaBmsCenSaleDocConfirm_CompID;
		if(values.ReaBmsCenSaleDocConfirm_ReaCompID) entity.ReaCompID = values.ReaBmsCenSaleDocConfirm_ReaCompID;
		if(values.ReaBmsCenSaleDocConfirm_AccepterID) entity.AccepterID = values.ReaBmsCenSaleDocConfirm_AccepterID;

		if(values.ReaBmsCenSaleDocConfirm_DataAddTime) entity.DataAddTime = JShell.Date.toServerDate(values.ReaBmsCenSaleDocConfirm_DataAddTime);
		if(values.ReaBmsCenSaleDocConfirm_AcceptTime) entity.AcceptTime = JShell.Date.toServerDate(values.ReaBmsCenSaleDocConfirm_AcceptTime);
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
			'Id', 'Status', 'SaleDocNo', 'CompID', 'CompanyName', 'ReaCompID', 'ReaCompanyName', 'ReaServerCompCode', 'SourceType', 'Memo','TransportNo','InvoiceNo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsCenSaleDocConfirm_Id;
		return entity;
	},
	/**@description 通过选择供货商+供货单号查找本地供货单*/
	onSaleDocNo: function(field, e) {
		var me = this;
		//判断供货商是否已经选择
		var reaCompID = me.getComponent('ReaBmsCenSaleDocConfirm_ReaCompID');
		var reaCompanyName = me.getComponent('ReaBmsCenSaleDocConfirm_ReaCompanyName');
		var reaServerCompCode = me.getComponent('ReaBmsCenSaleDocConfirm_ReaServerCompCode');
		if(!reaCompID.getValue()) {
			JShell.Msg.alert("请选择供货商后再操作!");
			return;
		}
		var reaComp = {
			"ReaCompID": reaCompID.getValue(),
			"ReaCompanyName": reaCompanyName.getValue(),
			"ReaServerCompCode": reaServerCompCode.getValue()
		}
		me.fireEvent('onSaleDocNo', me, reaComp, field, e);
	},
	setCompReadOnlys: function(readOnly) {
		var me = this;
		var companyName = me.getComponent("ReaBmsCenSaleDocConfirm_CompanyName");
		if(readOnly == undefined || readOnly == null) readOnly = true;
		companyName.setReadOnly(readOnly);
	}
});