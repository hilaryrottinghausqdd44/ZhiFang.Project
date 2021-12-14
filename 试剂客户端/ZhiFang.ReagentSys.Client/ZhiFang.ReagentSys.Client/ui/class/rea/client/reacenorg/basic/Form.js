/**
 * 订货方/供货方表单
 * @author liangyl	
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.reacenorg.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '订货方/供货方信息',
	width: 470,
	height: 595,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaCenOrg',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaCenOrgByField',
	/**检查机构编码是否存在*/
	selectUrl2: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**内容周围距离*/
	bodyPadding: '10px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 200,
		labelAlign: 'right'
	},
	/**机构类型 供货方0，订货方1*/
	OrgType: '0',
	POrgName: '',
	POrgID: null,
	POrgNo: '',
	
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
			name: 'ReaCenOrg_Id',
			hidden: true,
			type: 'key'
		});
		//机构编号
		items.push({
			fieldLabel: '供应商编码',
			name: 'ReaCenOrg_OrgNo',
			itemId: 'ReaCenOrg_OrgNo',
			//xtype:'numberfield',
			colspan: 1,
			locked: true,
			readOnly: true,
			hidden: true,
			width: me.defaults.width * 1
		});
		//中文名
		items.push({
			fieldLabel: '供应商名称',
			name: 'ReaCenOrg_CName',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 2,
			width: me.defaults.width * 2
		});
		//英文名
		items.push({
			fieldLabel: '英文名称',
			name: 'ReaCenOrg_EName',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//代码
		items.push({
			fieldLabel: '代码',
			name: 'ReaCenOrg_ShortCode',
			colspan: 1,
			width: me.defaults.width * 1
		});

		//上级机构
		items.push({
			fieldLabel: '上级机构',
			emptyText: '必填项',
			allowBlank: false,
			name: 'ReaCenOrg_POrgName',
			itemId: 'ReaCenOrg_POrgName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			colspan: 1,
			width: me.defaults.width * 1,
			//			value:me.POrgName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.basic.Tree', {
					resizable: false,
					/**机构类型*/
					OrgType: me.OrgType,
					listeners: {
						accept: function(p, record) {
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '父机构ID',
			name: 'ReaCenOrg_POrgID',
			colspan: 1,
			//			value:me.POrgID,
			itemId: 'ReaCenOrg_POrgID',
			width: me.defaults.width * 1,
			hidden: true
		}, {
			fieldLabel: '上级机构编号',
			name: 'ReaCenOrg_POrgNo',
			colspan: 1,
			//			value:me.POrgNo,
			itemId: 'ReaCenOrg_POrgNo',
			width: me.defaults.width * 1,
			hidden: true
		});
		//平台机构编号
		items.push({
			fieldLabel: '平台机构编码',
			name: 'ReaCenOrg_PlatformOrgNo',
			itemId: 'ReaCenOrg_PlatformOrgNo',
			xtype: 'numberfield',
			colspan: 1,
			style: 'color:red;',
			width: me.defaults.width * 1
		});
		//物资对照码
		items.push({
			fieldLabel: '物资对照码',
			name: 'ReaCenOrg_MatchCode',
			itemId: 'ReaCenOrg_MatchCode',
			//xtype: 'numberfield',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//启用
		items.push({
			fieldLabel: '启用',
			name: 'ReaCenOrg_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,
			colspan: 1,
			width: me.defaults.width * 1
		});
		//显示次序
		items.push({
			fieldLabel: '显示次序',
			name: 'ReaCenOrg_DispOrder',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'numberfield',
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		});

		//联系人
		items.push({
			fieldLabel: '联系人',
			name: 'ReaCenOrg_Contact',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//传真
		items.push({
			fieldLabel: '传真',
			name: 'ReaCenOrg_Fox',
			colspan: 1,
			width: me.defaults.width * 1
		});

		//联系手机
		items.push({
			fieldLabel: '联系手机',
			name: 'ReaCenOrg_Tel',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//联系手机2
		items.push({
			fieldLabel: '联系手机2',
			name: 'ReaCenOrg_Tel1',
			colspan: 1,
			width: me.defaults.width * 1
		});

		//热线电话
		items.push({
			fieldLabel: '联系电话',
			name: 'ReaCenOrg_HotTel',
			colspan: 1,
			width: me.defaults.width * 1
		});
		//联系电话2
		items.push({
			fieldLabel: '联系电话2',
			name: 'ReaCenOrg_HotTel1',
			colspan: 1,
			width: me.defaults.width * 1
		});

		//邮箱
		items.push({
			fieldLabel: '邮箱',
			name: 'ReaCenOrg_Email',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//网址
		items.push({
			fieldLabel: '网址',
			name: 'ReaCenOrg_WebAddress',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//机构地址
		items.push({
			fieldLabel: '联系地址',
			name: 'ReaCenOrg_Address',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//开户名称
		items.push({
			fieldLabel: '开户名称',
			name: 'ReaCenOrg_OpeningBank',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//税号
		items.push({
			fieldLabel: '税号',
			name: 'ReaCenOrg_TaxNumber',
			colspan: 2,
			width: me.defaults.width * 2
		});
		// 订货方的form要增加一个单据类型NextBillType，下拉框，值范围：1销售/2共建/3调拨
		if(me.OrgType == '1') { // 订货方独有的
			items.push({
				fieldLabel: '单据类型',
				xtype: 'uxSimpleComboBox',
				name: 'ReaCenOrg_NextBillType',
				colspan: 2,
				width: me.defaults.width * 2,
				data: [
					['0','请选择'],
					['1','销售'],
					['2','共建'],
					['3','调拨']
				],
				value: '1' // 默认选中第一条
			})
			
		}
		//开户银行
		items.push({
			fieldLabel: '开户银行',
			name: 'ReaCenOrg_BankName',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//银行账号
		items.push({
			fieldLabel: '银行账号',
			name: 'ReaCenOrg_BankAccount',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//开户许可证
		items.push({
			fieldLabel: '开户许可证',
			name: 'ReaCenOrg_IdentificationCode',
			colspan: 2,
			width: me.defaults.width * 2
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaCenOrg_Memo',
			height: 60,
			colspan: 2,
			width: me.defaults.width * 2
		});
		//说明
		items.push({
			xtype: 'displayfield',
			fieldLabel: ' ',
			colspan: 2,
			value: '说明:当机构为本地供应商时,其平台机构编号等于机构编号',
			width: me.defaults.width * 2 + 60,
			labelSeparator: '',
			fieldStyle: 'font-size:14px;color:blue;background:none;border:0;border-bottom:0px',
			style: {
				marginLeft: '-60px'
			}
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.ReaCenOrg_CName,
			EName: values.ReaCenOrg_EName,
			ShortCode: values.ReaCenOrg_ShortCode,
			DispOrder: values.ReaCenOrg_DispOrder,
			Visible: values.ReaCenOrg_Visible ? 1 : 0,
			Address: values.ReaCenOrg_Address,
			Contact: values.ReaCenOrg_Contact,
			Tel: values.ReaCenOrg_Tel,
			Tel1: values.ReaCenOrg_Tel2,
			HotTel: values.ReaCenOrg_HotTel,
			HotTel1: values.ReaCenOrg_HotTel1,
			Fox: values.ReaCenOrg_Fox,
			Email: values.ReaCenOrg_Email,
			WebAddress: values.ReaCenOrg_WebAddress,
			Memo: values.ReaCenOrg_Memo,
			IdentificationCode: values.ReaCenOrg_IdentificationCode,
			BankAccount: values.ReaCenOrg_BankAccount,
			BankName: values.ReaCenOrg_BankName,
			TaxNumber: values.ReaCenOrg_TaxNumber,
			OpeningBank: values.ReaCenOrg_OpeningBank,
			MatchCode: values.ReaCenOrg_MatchCode
		};
		if(me.OrgType == '1') { // 订货方增加单据类型
			entity.NextBillType = values.ReaCenOrg_NextBillType;
		}
		if (values.ReaCenOrg_POrgID) {
			entity.POrgID = values.ReaCenOrg_POrgID;
		}
		if (values.ReaCenOrg_POrgNo) {
			entity.POrgNo = values.ReaCenOrg_POrgNo;
		}
		if (values.ReaCenOrg_OrgNo) {
			entity.OrgNo = values.ReaCenOrg_OrgNo;
		}
		if (values.ReaCenOrg_PlatformOrgNo) {
			entity.PlatformOrgNo = values.ReaCenOrg_PlatformOrgNo;
		}
		if (me.OrgType) {
			entity.OrgType = me.OrgType;
		}
		var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
		if (JShell.Date.toServerDate(DataAddTime)) {
			entity.DataUpdateTime = JShell.Date.toServerDate(DataAddTime);
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
		var fields = [
				'Id', 'CName', 'EName', 'ShortCode', 'OrgNo', 'OpeningBank', 'TaxNumber', 'BankName', 'BankAccount',
				'IdentificationCode',
				'DispOrder', 'Visible', 'Address', 'Contact', 'Tel', 'Tel1', 'HotTel', 'HotTel1', 'Fox', 'Email',
				'WebAddress', 'Memo', 'PlatformOrgNo', 'POrgID', 'POrgNo', 'MatchCode'
			];
		if(me.OrgType == '1') { // 订货方多了一个单据字段
			fields.push('NextBillType');
		}
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaCenOrg_Id;
		return entity;
	},
	/**订货方选择*/
	onCompAccept: function(record) {
		var me = this;
		var POrgName = me.getComponent('ReaCenOrg_POrgName');
		var POrgID = me.getComponent('ReaCenOrg_POrgID');
		var POrgNo = me.getComponent('ReaCenOrg_POrgNo');
		var text = record ? record.get('text') : '';
		var OrgNo = record ? record.get('OrgNo') : '';
		var id = record ? record.get('tid') : 0;
		POrgID.setValue(id);
		POrgName.setValue(text);
		POrgNo.setValue(OrgNo);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.ReaCenOrg_Visible = data.ReaCenOrg_Visible == '1' ? true : false;
		var POrgName = me.getComponent('ReaCenOrg_POrgName');
		var POrgID = me.getComponent('ReaCenOrg_POrgID');
		var OrgId = data.ReaCenOrg_POrgID + '';
		if (OrgId && OrgId != '0') {
			me.getOrgName(OrgId, function(data) {
				if (data.value.list) {
					POrgName.setValue(data.value.list[0].ReaCenOrg_CName);
				}
			});
		} else {
			if (OrgId == '0') {
				POrgName.setValue('所有机构');
			} else {
				POrgName.setValue('');
			}
		}
		return data;
	},
	/**获取机构名称信息*/
	getOrgName: function(id, callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl2;
		url += '&fields=ReaCenOrg_CName&where=reacenorg.Id=' + id;
		me.ItemEnum = {};
		JShell.Server.get(url, function(data) {
			if (data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		});

	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},

	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var POrgName = me.getComponent('ReaCenOrg_POrgName');
		var POrgID = me.getComponent('ReaCenOrg_POrgID');
		var POrgNo = me.getComponent('ReaCenOrg_POrgNo');
		if (!me.POrgID) me.POrgID = 0;
		POrgName.setValue(me.POrgName);
		POrgID.setValue(me.POrgID);
		POrgNo.setValue(me.POrgNo);

	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
	},
	/**更改标题*/
	changeTitle: function() {},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

		if (!me.getForm().isValid()) return;
		//编辑时当前节点不能作为上级几点
		if (me.formtype == 'edit') {
			var values = me.getForm().getValues();
			if (values.ReaCenOrg_POrgID == values.ReaCenOrg_Id) {
				JShell.Msg.alert('上级节点不能是' + values.ReaCenOrg_CName, 5000);
				return;
			}
		}
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if (!params) return;

		var id = params.entity.Id;

		params = Ext.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				id = me.formtype == 'add' ? data.value : id;
				id += '';
				me.fireEvent('save', me, id);
				if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});
