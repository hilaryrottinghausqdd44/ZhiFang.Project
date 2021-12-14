/**
 * 仪器维护
 * @author sheldon	
 * @version 2018-10-31
 */
Ext.define('Shell.class.rea.client.equip.lab.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '仪器信息',
	width: 295,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '10px 20px 0px 10px',
	formtype: "edit",
	autoScroll: false,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaTestEquipLab',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipLabByField',
	/**获取获取供应商数据服务路径*/
	selectCenOrgUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
	/**获取获取仪器厂商数据服务路径*/
	selectDictUrl: '/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true',
	
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	/*layout: {
		type: 'table',
		columns:2 //每行有几列
	},*/
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 265,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	CenOrgEnum: {},
	/*厂商*/
	ProdOrg: 'ProdOrg',
	DictEnum: {},
	/**部门*/
	DeptId: 'DeptId',
	DeptEnum: {},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '所属部门',
			name: 'ReaTestEquipLab_DeptName',
			itemId: 'ReaTestEquipLab_DeptName',
			xtype: 'uxCheckTrigger',
			emptyText: '所属部门',
			className: 'Shell.class.rea.client.out.basic.CheckTree',
			classConfig: {
				title: '部门选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					if(record && record.get("tid") == 0) {
						JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
						return;
					}
					me.onDepAccept(record);
					p.close();
				}
			}
		}, {
			fieldLabel: '部门ID',
			itemId: 'ReaTestEquipLab_DeptID',
			name: 'ReaTestEquipLab_DeptID',
			xtype: 'textfield',
			hidden: true

		},{
			fieldLabel: '中文名称',
			name: 'ReaTestEquipLab_CName',
			emptyText: '必填项',
			allowBlank: false

		}, {
			fieldLabel: '英文名称',
			name: 'ReaTestEquipLab_EName'

		}, {
			fieldLabel: '代码',
			name: 'ReaTestEquipLab_ShortCode'

		}, {
			fieldLabel: 'LIS编码',
			name: 'ReaTestEquipLab_LisCode'

		},  {
			fieldLabel: '启用',
			name: 'ReaTestEquipLab_Visible',
			xtype: 'uxBoolComboBox',
			value: true,
			hasStyle: true,

		}, {
			fieldLabel: '显示次序',
			name: 'ReaTestEquipLab_DispOrder',
			emptyText: '必填项',
			allowBlank: false,
			xtype: 'numberfield',
			value: 0,

		}, {
			fieldLabel: '仪器厂商',
			name: 'ReaTestEquipLab_ProdOrgName',
			emptyText: '仪器厂商',
			itemId: 'ReaTestEquipLab_ProdOrgName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '仪器厂商选择',
				defaultWhere: "bdict.BDictType.DictTypeCode='" + this.ProdOrg + "'"
			},
			listeners: {
				check: function(p, record) {
					me.onProdOrgAccept(p, record);
				}
			}
		}, {
			fieldLabel: '仪器厂商主键ID',
			itemId: 'ReaTestEquipLab_ProdOrgID',
			name: 'ReaTestEquipLab_ProdOrgID',
			xtype: 'textfield',
			hidden: true

		}, {
			fieldLabel: '供应商',
			name: 'ReaTestEquipLab_CompOrgName',
			itemId: 'ReaTestEquipLab_CompOrgName',
			//			emptyText: '必填项',allowBlank: false,
			xtype: 'uxCheckTrigger',
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
			fieldLabel: '供应商主键ID',
			hidden: true,
			name: 'ReaTestEquipLab_CompOrgID',
			itemId: 'ReaTestEquipLab_CompOrgID'
		}, {
			height: 85,
			fieldLabel: '备注',
			emptyText: '备注',
			name: 'ReaTestEquipLab_Memo',
			xtype: 'textarea',
			height: 200
		}, {
			fieldLabel: '主键ID',
			name: 'ReaTestEquipLab_Id',
			hidden: true
		});
		return items;
	},

	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			DeptName: values.ReaTestEquipLab_DeptName,
			CName: values.ReaTestEquipLab_CName,
			EName: values.ReaTestEquipLab_EName,
			ShortCode: values.ReaTestEquipLab_ShortCode,
			Memo: values.ReaTestEquipLab_Memo,
			Visible: values.ReaTestEquipLab_Visible ? 1 : 0
		};
		if(values.ReaTestEquipLab_DispOrder) {
			entity.DispOrder = values.ReaTestEquipLab_DispOrder;
		}
		if(values.ReaTestEquipLab_LisCode) {
			entity.LisCode = values.ReaTestEquipLab_LisCode;
		}
		if(values.ReaTestEquipLab_CompOrgID) {
			entity.CompOrgID = values.ReaTestEquipLab_CompOrgID;
		}
		if(values.ReaTestEquipLab_ProdOrgID) {
			entity.ProdOrgID = values.ReaTestEquipLab_ProdOrgID;
		}
		if(values.ReaTestEquipLab_DeptID) {
			entity.DeptID = values.ReaTestEquipLab_DeptID;
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
			'CName', 'Id', 'ShortCode', 'EName', 'ProdOrgID', 'DeptID', 'DeptName',
			'LisCode', 'CompOrgID', 'DispOrder', 'Memo', 'Visible'
		];
		entity.fields = fields.join(',');
		if(values.ReaTestEquipLab_Id != '') {
			entity.entity.Id = values.ReaTestEquipLab_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var ProdOrgName = me.getComponent('ReaTestEquipLab_ProdOrgName'),
			CompOrgName = me.getComponent('ReaTestEquipLab_CompOrgName');
		DeptName = me.getComponent('ReaTestEquipLab_DeptName');

		var ProdValue = me.DictEnum[data.ReaTestEquipLab_ProdOrgID];
		ProdOrgName.setValue(ProdValue);

		var CompValue = me.CenOrgEnum[data.ReaTestEquipLab_CompOrgID];
		CompOrgName.setValue(CompValue);

		var DeptValue = me.DeptEnum[data.ReaTestEquipLab_DeptID];
		DeptName.setValue(DeptValue);

		data.ReaTestEquipLab_Visible = data.ReaTestEquipLab_Visible == '1' ? true : false;
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	//获取供应商
	getCompOrgName: function(id, callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectCenOrgUrl;
		url += "&fields=ReaCenOrg_CName&where=reacenorg.Id=" + id;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**供应商选择*/
	onCompAccept: function(record) {
		var me = this;
		var ComId = me.getComponent('ReaTestEquipLab_CompOrgID');
		var ComName = me.getComponent('ReaTestEquipLab_CompOrgName');

		ComId.setValue(record ? record.get('ReaCenOrg_Id') : '');
		ComName.setValue(record ? record.get('ReaCenOrg_CName') : '');
	},
	/**厂商选择*/
	onProdOrgAccept: function(p, record) {
		var me = this;
		var Id = me.getComponent('ReaTestEquipLab_ProdOrgID');
		var CName = me.getComponent('ReaTestEquipLab_ProdOrgName');

		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');

		p.close();
	},
	/**部门选择*/
	onDepAccept: function(record) {
		var me = this;
		var DeptID = me.getComponent('ReaTestEquipLab_DeptID');
		var DeptName = me.getComponent('ReaTestEquipLab_DeptName');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		DeptID.setValue(id);
		DeptName.setValue(text);
	}
});