/**
 * 机构与货品信息
 * @author longfc	
 * @version 2017-09-12
 */
Ext.define('Shell.class.rea.client.goodsorglink.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '机构与货品信息',

	width: 435,
	height: 340,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaGoodsOrgLink',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsOrgLinkByField',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**内容周围距离*/
	bodyPadding: '10px 15px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 90,
		width: 190,
		labelAlign: 'right'
	},
	/**应用类型:货品:goods;订货/供货:cenorg*/
	appType: "",
	/**单个新增时的表单默认值*/
	addFormDefault: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'ReaGoodsOrgLink_Id',
			hidden: true,
			type: 'key'
		});
		items.push({
			fieldLabel: '机构选择',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaGoodsOrgLink_CenOrg_CName',
			itemId: 'ReaGoodsOrgLink_CenOrg_CName',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 565,
				height: 460,
				checkOne: true
			},
			className: 'Shell.class.rea.client.ordergoods.basic.CenOrgCheck',
			readOnly: me.appType == "goods" ? false : true,
			locked: me.appType == "goods" ? false : true,
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '机构主键ID',
			hidden: true,
			name: 'ReaGoodsOrgLink_CenOrg_Id',
			itemId: 'ReaGoodsOrgLink_CenOrg_Id'
		}, {
			fieldLabel: '机构类型',
			hidden: true,
			name: 'ReaGoodsOrgLink_CenOrg_OrgType',
			itemId: 'ReaGoodsOrgLink_CenOrg_OrgType'
		});

		items.push({
			fieldLabel: '货品选择',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaGoodsOrgLink_ReaGoods_CName',
			itemId: 'ReaGoodsOrgLink_ReaGoods_CName',
			xtype: 'uxCheckTrigger',
			classConfig: {
				width: 565,
				height: 460,
				ReaCenOrgId:me.ReaCenOrgId
			},
			className: 'Shell.class.rea.client.ordergoods.basic.GoodsCheck',
			readOnly: me.formtype == "add" ? false : true,
			locked: me.formtype == "add" ? false : true,
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '货品主键ID',
			hidden: true,
			name: 'ReaGoodsOrgLink_ReaGoods_Id',
			itemId: 'ReaGoodsOrgLink_ReaGoods_Id'
		});

		items.push({
			fieldLabel: '供货商货品编码',
			name: 'ReaGoodsOrgLink_CenOrgGoodsNo',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2
		});
		items.push({
			fieldLabel: '招标号',
			name: 'ReaGoodsOrgLink_BiddingNo',
			itemId: 'ReaGoodsOrgLink_BiddingNo',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//优先级
		items.push({
			fieldLabel: '优先级',
			name: 'ReaGoodsOrgLink_DispOrder',
			xtype: 'numberfield',
			minValue: 0,
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//价格
		items.push({
			fieldLabel: '价格',
			name: 'ReaGoodsOrgLink_Price',
			xtype: 'numberfield',
			minValue: 0,
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//启用
		items.push({
			fieldLabel: '启用',
			name: 'ReaGoodsOrgLink_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//有效开始
		items.push({
			xtype: 'datefield',
			fieldLabel: '有效开始',
			format: 'Y-m-d', // H:m:s
			name: 'ReaGoodsOrgLink_BeginTime',
			itemId: 'ReaGoodsOrgLink_BeginTime',
			allowBlank: false,
			width: me.defaults.width * 1,
			colspan: 1
		});
		//有效截止
		items.push({
			xtype: 'datefield',
			fieldLabel: '有效截止',
			format: 'Y-m-d', // H:m:s
			name: 'ReaGoodsOrgLink_EndTime',
			itemId: 'ReaGoodsOrgLink_EndTime',
			width: me.defaults.width * 1,
			colspan: 1
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaGoodsOrgLink_Memo',
			height: 55,
			colspan: 2,
			width: me.defaults.width * 2
		});
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		data.ReaGoodsOrgLink_Visible = data.ReaGoodsOrgLink_Visible == '1' ? true : false;
		if(data.ReaGoodsOrgLink_BeginTime) data.ReaGoodsOrgLink_BeginTime = JcallShell.Date.toString(data.ReaGoodsOrgLink_BeginTime, true);
		if(data.ReaGoodsOrgLink_EndTime) data.ReaGoodsOrgLink_EndTime = JcallShell.Date.toString(data.ReaGoodsOrgLink_EndTime, true);
		
		var reg = new RegExp("<br />", "g");
		data.ReaGoodsOrgLink_Memo = data.ReaGoodsOrgLink_Memo.replace(reg, "\r\n");
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var CenOrg_CName = me.getComponent('ReaGoodsOrgLink_CenOrg_CName'),
			CenOrg_Id = me.getComponent('ReaGoodsOrgLink_CenOrg_Id'),
			CenOrg_OrgType = me.getComponent('ReaGoodsOrgLink_CenOrg_OrgType');
		CenOrg_CName.on({
			check: function(p, record) {
				CenOrg_CName.setValue(record ? record.get('ReaCenOrg_CName') : '');
				CenOrg_Id.setValue(record ? record.get('ReaCenOrg_Id') : '');
				CenOrg_OrgType.setValue(record ? record.get('ReaCenOrg_OrgType') : '');
				me.setCenOrgCNameFieldLabel();
				p.close();
			}
		});

		var ReaGoods_CName = me.getComponent('ReaGoodsOrgLink_ReaGoods_CName'),
			ReaGoods_Id = me.getComponent('ReaGoodsOrgLink_ReaGoods_Id');
		ReaGoods_CName.on({
			check: function(p, record) {
				ReaGoods_CName.setValue(record ? record.get('ReaGoods_CName') : '');
				ReaGoods_Id.setValue(record ? record.get('ReaGoods_Id') : '');
				p.close();
			}
		});
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		if(!me.addFormDefault["ReaGoodsOrgLink_BeginTime"]) {
			var Sysdate = JcallShell.System.Date.getDate();
			var curSysDateTime = JcallShell.Date.toString(Sysdate, true);
			me.addFormDefault["ReaGoodsOrgLink_BeginTime"] = curSysDateTime;
		}
		me.ReaCenOrgId=me.addFormDefault.ReaGoodsOrgLink_CenOrg_Id;
		if(me.addFormDefault) me.getForm().setValues(me.addFormDefault);
		me.setCenOrgCNameFieldLabel();
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.setCenOrgCNameFieldLabel();
	},
	setCenOrgCNameFieldLabel: function(id) {
		var me = this;
		var CenOrgCName = me.getComponent('ReaGoodsOrgLink_CenOrg_CName');
		var CenOrgOrgType = me.getComponent('ReaGoodsOrgLink_CenOrg_OrgType').getValue();
		var fieldLabel = "机构选择";
		//供货商
		if(CenOrgOrgType == "0") {
			fieldLabel += "(供货方)";
		} else if(CenOrgOrgType == "1") {
			fieldLabel += "(订货方)";
		}
		CenOrgCName.setFieldLabel(fieldLabel);
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			me.getForm().reset();
			if(me.addFormDefault) me.getForm().setValues(me.addFormDefault);
		} else {
			me.getForm().setValues(me.lastData);
		}
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;

		if(!values.ReaGoodsOrgLink_CenOrg_Id) {
			JShell.Msg.alert("机构信息为空!", null, 1000);
			return;
		}
		if(!values.ReaGoodsOrgLink_ReaGoods_Id) {
			JShell.Msg.alert("货品信息为空!", null, 1000);
			return;
		}
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("封装提交信息错!");
			return;
		}

		var id = params.entity.Id;
		params = JcallShell.JSON.encode(params);
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value : id;
				id += '';
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
		var DispOrder = values.ReaGoodsOrgLink_DispOrder;
		var Price = values.ReaGoodsOrgLink_Price;
		if(!DispOrder) DispOrder = 0;
		if(!Price) Price = 0;
		var entity = {
			CenOrgGoodsNo: values.ReaGoodsOrgLink_CenOrgGoodsNo,
			BiddingNo: values.ReaGoodsOrgLink_BiddingNo,
			Price: Price,
			DispOrder: DispOrder,
			Visible: values.ReaGoodsOrgLink_Visible ? 1 : 0
		};
		entity.Memo = values.ReaGoodsOrgLink_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
		var Sysdate = JcallShell.System.Date.getDate();
		var curDateTime = JcallShell.Date.toString(Sysdate);
		if(!values.ReaGoodsOrgLink_BeginTime) {
			values.ReaGoodsOrgLink_BeginTime = curDateTime;
		}
		if(JShell.Date.toServerDate(values.ReaGoodsOrgLink_BeginTime)) {
			entity.BeginTime = JShell.Date.toServerDate(values.ReaGoodsOrgLink_BeginTime);
		}
		if(values.ReaGoodsOrgLink_EndTime) {
			if(JShell.Date.toServerDate(values.ReaGoodsOrgLink_EndTime)) {
				entity.EndTime = JShell.Date.toServerDate(values.ReaGoodsOrgLink_EndTime);
			}
		}

		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		if(values.ReaGoodsOrgLink_CenOrg_Id) {
			entity.CenOrg = {
				'Id': values.ReaGoodsOrgLink_CenOrg_Id
			}
			if(me.formtype == "add") entity.CenOrg.DataTimeStamp = strDataTimeStamp.split(',');
		}
		if(values.ReaGoodsOrgLink_ReaGoods_Id) {
			entity.ReaGoods = {
				'Id': values.ReaGoodsOrgLink_ReaGoods_Id
			}
			if(me.formtype == "add") entity.ReaGoods.DataTimeStamp = strDataTimeStamp.split(',');
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

		entity.fields = 'Id,CenOrg_Id,ReaGoods_Id,Visible,Price,DispOrder,CenOrgGoodsNo,BiddingNo,Memo,BeginTime,EndTime';
		//if(me.formtype == "edit") entity.fields += ",DataUpdateTime";
		entity.entity.Id = values.ReaGoodsOrgLink_Id;
		return entity;
	}
});