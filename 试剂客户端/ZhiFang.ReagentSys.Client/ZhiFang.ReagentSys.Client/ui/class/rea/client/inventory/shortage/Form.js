/**
 * 盘库管理-出库
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.shortage.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '出库信息',
	requires: [
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding: '15px 0px 0px 0px',
	formtype: "edit",
	autoScroll: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsOutDocOfCheckDocID?isPlanish=true',

	/**内容周围距离*/
	bodyPadding: '10px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 5 //每行有几列
	},

	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 155,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	buttonDock: "top",
	/**盘库单Id*/
	PK: null,
	OutTypeList: [],
	/**客户端出库类型*/
	OutTypeKey: "ReaBmsOutDocOutType",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onConfirmClick');
		me.defaults.width = parseInt(me.width / me.layout.columns);
		if(me.defaults.width < 155) me.defaults.width = 155;
		
		JShell.REA.StatusList.getStatusList(me.OutTypeKey, false, true, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];

		items.push("-", {
			xtype: 'button',
			iconCls: 'button-check',
			itemId: "btnConfirm",
			text: '确认出库',
			tooltip: '确认出库',
			handler: function() {
				me.onSaveClick();
			}
		});
		if(me.hasReset) items.push('reset');

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: true
		});
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '使用部门',
			name: 'ReaBmsOutDoc_DeptName',
			itemId: 'ReaBmsOutDoc_DeptName',
			xtype: 'uxCheckTrigger',
			emptyText: '必填项',
			allowBlank: false,
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
			},
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '部门id',
			name: 'ReaBmsOutDoc_DeptID',
			itemId: 'ReaBmsOutDoc_DeptID',
			hidden: true
		});
		//出库类型
		items.push({
			fieldLabel: '出库类型',
			name: 'ReaBmsOutDoc_OutType',
			itemId: 'ReaBmsOutDoc_OutType',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.OutTypeKey].List,
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true,
			value: "6",//盘亏出库
			className: 'Shell.class.rea.client.storagetype.CheckGrid',
			listeners: {
				check: function(p, record) {
					p.close();
				}
			}
		});

		items.push({
			fieldLabel: '操作日期',
			name: 'ReaBmsOutDoc_DataAddTime',
			itemId: 'ReaBmsOutDoc_DataAddTime',
			//xtype: 'datefield',
			//format: 'Y-m-d',
			xtype: 'datetimefield',
			format: 'Y-m-d H:i:s',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '领用人id',
			name: 'ReaBmsOutDoc_TakerID',
			itemId: 'ReaBmsOutDoc_TakerID',
			hidden: true
		}, {
			fieldLabel: '领用人',
			name: 'ReaBmsOutDoc_TakerName',
			itemId: 'ReaBmsOutDoc_TakerName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.user.CheckApp',
			colspan: 1,
			width: me.defaults.width * 1,
			classConfig: {
				title: '领用人选择',
				checkOne: true
			},
			listeners: {
				check: function(p, record) {
					me.onUserAccept(record);
					p.close();
				}
			}
		});

		items.push({
			fieldLabel: '出库单号',
			name: 'ReaBmsOutDoc_OutDocNo',
			colspan: 2,
			width: me.defaults.width * 2
		}, {
			fieldLabel: '出库时间',
			name: 'ReaBmsOutDoc_OperDate',
			itemId: 'ReaBmsOutDoc_OperDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		}, {
			fieldLabel: '审核人',
			name: 'ReaBmsOutDoc_CheckName',
			itemId: 'ReaBmsOutDoc_CheckName',
			readOnly: true,
			locked: true,
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '货品总额',
			name: 'ReaBmsOutDoc_TotalPrice',
			itemId: 'ReaBmsOutDoc_TotalPrice',
			readOnly: true,
			locked: true,
			xtype: 'numberfield',
			colspan: 1,
			width: me.defaults.width * 1
		});

		items.push({
			height: 50,
			fieldLabel: '出库说明',
			emptyText: '出库说明',
			name: 'ReaBmsOutDoc_Memo',
			xtype: 'textarea',
			colspan: 5,
			width: me.defaults.width * 5
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsOutDoc_Id',
			hidden: true,
			type: 'key'
		}, {
			fieldLabel: '盘库单ID',
			name: 'ReaBmsOutDoc_CheckDocID',
			hidden: true
		}, {
			fieldLabel: '审核人ID',
			name: 'ReaBmsOutDoc_CheckID',
			itemId: 'ReaBmsOutDoc_CheckID',
			hidden: true
		}, {
			fieldLabel: '出库人ID',
			name: 'ReaBmsOutDoc_OutBoundID',
			itemId: 'ReaBmsOutDoc_OutBoundID',
			hidden: true
		}, {
			fieldLabel: '出库人',
			name: 'ReaBmsOutDoc_OutBoundName',
			itemId: 'ReaBmsOutDoc_OutBoundName',
			hidden: true
		});
		return items;
	},
	/**使用部门选择*/
	onDepAccept: function(record) {
		var me = this;
		var DeptID = me.getComponent('ReaBmsOutDoc_DeptID');
		var DeptName = me.getComponent('ReaBmsOutDoc_DeptName');
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		DeptID.setValue(id);
		DeptName.setValue(text);
	},
	/**领用人选择*/
	onUserAccept: function(record) {
		var me = this;
		var UseID = me.getComponent('ReaBmsOutDoc_TakerID');
		var UseName = me.getComponent('ReaBmsOutDoc_TakerName');
		UseName.setValue(record ? record.get('HREmployee_CName') : '');
		UseID.setValue(record ? record.get('HREmployee_Id') : '');
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			Visible: 1,
			DeptName: values.ReaBmsOutDoc_DeptName,
			OutBoundName: values.ReaBmsOutDoc_OutBoundName,
			TakerName: values.ReaBmsOutDoc_TakerName,
			CheckName: values.ReaBmsOutDoc_CheckName
		};
		if(values.ReaBmsOutDoc_DeptID) {
			entity.DeptID = values.ReaBmsOutDoc_DeptID;
		}
		if(values.ReaBmsOutDoc_OperDate)
			entity.OperDate = JShell.Date.toServerDate(values.ReaBmsOutDoc_OperDate);
			
		if(values.ReaBmsOutDoc_OutBoundID)
			entity.OutBoundID = values.ReaBmsOutDoc_OutBoundID;			
		if(values.ReaBmsOutDoc_TakerID)
			entity.TakerID = values.ReaBmsOutDoc_TakerID;
		if(values.ReaBmsOutDoc_CheckID)
			entity.CheckID = values.ReaBmsOutDoc_CheckID;

		if(values.ReaBmsOutDoc_CheckDocID)
			entity.CheckDocID = values.ReaBmsOutDoc_CheckDocID;
		if(values.ReaBmsOutDoc_OutType)
			entity.OutType = values.ReaBmsOutDoc_OutType;
		if(values.ReaBmsOutDoc_OutDocNo)
			entity.OutDocNo = values.ReaBmsOutDoc_OutDocNo;

		if(values.ReaBmsOutDoc_TotalPrice)
			entity.TotalPrice = parseFloat(values.ReaBmsOutDoc_TotalPrice);
		entity.Memo = values.ReaBmsOutDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');

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
			'Id', 'Status', 'OutType', 'OutDocNo', 'CheckName', 'TotalPrice', 'Memo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsOutDoc_Id;
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		if(data.ReaBmsOutDoc_OperDate)
			data.ReaBmsOutDoc_OperDate = JShell.Date.getDate(data.ReaBmsOutDoc_OperDate);
		var dataAddTime = data.ReaBmsOutDoc_DataAddTime;
		if (dataAddTime) {
			data.ReaBmsOutDoc_DataAddTime = JcallShell.Date.toString(dataAddTime.replace(/\//g, "-"));
		}
		var confirmTime = data.ReaBmsOutDoc_ConfirmTime;
		if (confirmTime) {
			data.ReaBmsOutDoc_ConfirmTime = JcallShell.Date.toString(confirmTime.replace(/\//g, "-"));
		}
		var checkTime = data.ReaBmsOutDoc_CheckTime;
		if (checkTime) {
			data.ReaBmsOutDoc_CheckTime = JcallShell.Date.toString(checkTime.replace(/\//g, "-"));
		}
		var approvalTime = data.ReaBmsOutDoc_ApprovalTime;
		if (approvalTime) {
			data.ReaBmsOutDoc_ApprovalTime = JcallShell.Date.toString(approvalTime.replace(/\//g, "-"));
		}
		var reg = new RegExp("<br />", "g");
		data.ReaBmsOutDoc_Memo = data.ReaBmsOutDoc_Memo.replace(reg, "\r\n");
		return data;
	},
	/**@overwrite根据主键ID加载数据*/
	load: function(id) {
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();

		if(!id) return;

		me.PK = id; //面板主键

		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}

		me.showMask(me.loadingText); //显示遮罩层
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&") + "id=" + id;
		url += '&fields=' + me.getStoreFields().join(',');

		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				if(data.value) {
					data.value = JShell.Server.Mapping(data.value);
					me.lastData = me.changeResult(data.value);
					me.getForm().setValues(data.value);
				}
			} else {
				me.getComponent('buttonsToolbar').hide();
				//JShell.Msg.error(data.msg);
			}
			me.fireEvent('load', me, data);
		});
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**更改标题*/
	ininValues: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
		var value = JcallShell.Date.toString(Sysdate, true);
		var OperDate = me.getComponent('ReaBmsOutDoc_OperDate');
		OperDate.setValue(value);

		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var usrCName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

		var TakerID = me.getComponent('ReaBmsOutDoc_TakerID');
		var TakerName = me.getComponent('ReaBmsOutDoc_TakerName');
		TakerID.setValue(userId);
		TakerName.setValue(usrCName);

		var CheckID = me.getComponent('ReaBmsOutDoc_CheckID');
		var CheckName = me.getComponent('ReaBmsOutDoc_CheckName');
		CheckName.setValue(usrCName);
	},
	setTotalPrice: function(totalPrice) {
		var me = this;
		var TotalPrice = me.getComponent('ReaBmsOutDoc_TotalPrice');
		TotalPrice.setValue(totalPrice);
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.formtype = 'add';
		me.ininValues();
	},
	isEdit: function(id) {
		var me = this;
		me.callParent(arguments);
		me.formtype = "add";
		me.ininValues();
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("封装提交信息错误!");
			return;
		}
		me.fireEvent('onConfirmClick', me, params);
	}
});