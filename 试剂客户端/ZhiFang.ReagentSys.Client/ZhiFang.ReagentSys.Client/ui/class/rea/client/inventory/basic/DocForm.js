/**
 * 盘库管理
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.basic.DocForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '盘库信息',
	formtype: 'show',
	width: 420,
	height: 225,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDocById?isPlanish=true',

	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**内容周围距离*/
	bodyPadding: '8px 5px 0px 0px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 5 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 65,
		width: 165,
		labelAlign: 'right'
	},
	/**客户端盘库单状态*/
	StatusKey: "ReaBmsCheckDocStatus",
	/**客户端盘库单盘库结果*/
	CheckResultKey: "ReaBmsCheckResult",
	buttonDock: "top",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.CheckResultKey, false, true, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '',
			style: {
				marginLeft: "10px"
			},
			xtype: 'checkboxfield',
			boxLabel: '是否区分供应商(默认按"货品+批号+库房+货架"进行合并)',
			checked: false,
			inputValue: true,
			name: 'ReaBmsCheckDoc_IsCompFlag',
			itemId: 'ReaBmsCheckDoc_IsCompFlag',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			listeners: {
				change: function(field, newValue, oldValue, e) {
					me.IsDistinguishComp = newValue;
				}
			}
		});

		items.push({
			fieldLabel: '',
			style: {
				marginLeft: "5px"
			},
			xtype: 'checkboxfield',
			boxLabel: '包括库存数为零的试剂',
			checked: false,
			inputValue: true,
			name: 'ReaBmsCheckDoc_IsHasZeroQty',
			itemId: 'ReaBmsCheckDoc_IsHasZeroQty',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			listeners: {
				change: function(field, newValue, oldValue, e) {}
			}
		});

		items.push({
			fieldLabel: '',
			style: {
				marginLeft: "5px"
			},
			xtype: 'checkboxfield',
			boxLabel: '过滤库存数为0且不是这个库房的试剂',
			checked: false,
			inputValue: true,
			name: 'ReaBmsCheckDoc_IsStorageGoodsLink',
			itemId: 'ReaBmsCheckDoc_IsStorageGoodsLink',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			listeners: {
				change: function(field, newValue, oldValue, e) {}
			}
		});

		//供货方
		items.push({
			fieldLabel: '供应商',
			emptyText: '供应商选择',
			name: 'ReaBmsCheckDoc_CompanyName',
			itemId: 'ReaBmsCheckDoc_CompanyName',
			xtype: 'uxCheckTrigger',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					resizable: false,
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
		}, {
			fieldLabel: '供货方主键ID',
			hidden: true,
			name: 'ReaBmsCheckDoc_ReaCompanyID',
			itemId: 'ReaBmsCheckDoc_ReaCompanyID'
		}, {
			fieldLabel: '供应商机平台构码',
			hidden: true,
			name: 'ReaBmsCheckDoc_ReaServerCompCode',
			itemId: 'ReaBmsCheckDoc_ReaServerCompCode'
		});

		items.push({
			fieldLabel: '库房选择',
			emptyText: '盘点库房选择',
			name: 'ReaBmsCheckDoc_StorageName',
			itemId: 'ReaBmsCheckDoc_StorageName',
			colspan: 2,
			width: me.defaults.width * 2,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			className: 'Shell.class.rea.client.shelves.storage.CheckGrid',
			classConfig: {
				title: '库房选择',
				/**是否单选*/
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onStorageAccept(record);
					p.close();
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaBmsCheckDoc_StorageID',
			name: 'ReaBmsCheckDoc_StorageID',
			fieldLabel: '库房ID',
			hidden: true
		});
		items.push({
			fieldLabel: '货架选择',
			emptyText: '盘点货架选择',
			name: 'ReaBmsCheckDoc_PlaceName',
			itemId: 'ReaBmsCheckDoc_PlaceName',
			colspan: 1,
			width: me.defaults.width * 1,
			labelAlign: 'right',
			xtype: 'uxCheckTrigger',
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			className: 'Shell.class.rea.client.shelves.place.CheckGrid',
			classConfig: {
				title: '货架选择',
				/**是否单选*/
				checkOne: true,
				width: 300
			},
			listeners: {
				check: function(p, record) {
					me.onPlaceAccept(record);
					p.close();
				}
			}
		}, {
			xtype: 'textfield',
			itemId: 'ReaBmsCheckDoc_PlaceID',
			name: 'ReaBmsCheckDoc_PlaceID',
			fieldLabel: '货架ID',
			hidden: true
		});
		items.push({
			fieldLabel: '一级分类',
			emptyText: '一级分类',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaBmsCheckDoc_GoodsClass',
			name: 'ReaBmsCheckDoc_GoodsClass',
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'ReaBmsCheckDoc_GoodsClass');
				}
			}
		}, {
			fieldLabel: '二级分类',
			emptyText: '二级分类',
			colspan: 1,
			width: me.defaults.width * 1,
			itemId: 'ReaBmsCheckDoc_GoodsClassType',
			name: 'ReaBmsCheckDoc_GoodsClassType',
			readOnly: me.formtype != "add" ? true : false,
			locked: me.formtype != "add" ? true : false,
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'ReaBmsCheckDoc_GoodsClassType');
				}
			}
		});
		//盘库总单号
		items.push({
			fieldLabel: '盘库单号',
			name: 'ReaBmsCheckDoc_CheckDocNo',
			colspan: 2,
			width: me.defaults.width * 2,
			readOnly: true,
			locked: true
		});
		//单据状态
		items.push({
			fieldLabel: '单据状态',
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCheckDoc_Status',
			itemId: 'ReaBmsCheckDoc_Status',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.StatusKey].List,
			colspan: 1,
			width: me.defaults.width * 1,
			//allowBlank: false,
			readOnly: true,
			locked: true
		});

		items.push({
			fieldLabel: '是否异常',
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCheckDoc_IsException',
			itemId: 'ReaBmsCheckDoc_IsException',
			data: [
				[1, "是"],
				[0, "否"]
			],
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		items.push({
			fieldLabel: '异常是否已处理',
			labelWidth: 95,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			name: 'ReaBmsCheckDoc_IsHandleException',
			itemId: 'ReaBmsCheckDoc_IsHandleException',
			data: [
				[1, "是"],
				[0, "否"]
			],
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});

		//创建人
		items.push({
			fieldLabel: '创建人',
			name: 'ReaBmsCheckDoc_CreaterName',
			itemId: 'ReaBmsCheckDoc_CreaterName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//盘点人
		items.push({
			fieldLabel: '盘点人',
			name: 'ReaBmsCheckDoc_CheckerName',
			itemId: 'ReaBmsCheckDoc_CheckerName',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});
		//盘点日期
		items.push({
			xtype: 'datefield',
			fieldLabel: '盘点日期',
			format: 'Y-m-d',
			name: 'ReaBmsCheckDoc_CheckDateTime',
			itemId: 'ReaBmsCheckDoc_CheckDateTime',
			colspan: 1,
			width: me.defaults.width * 1,
			readOnly: true,
			locked: true
		});

		items.push({
			xtype: 'displayfield',
			fieldLabel: '审核人',
			name: 'ReaBmsCheckDoc_ExaminerName',
			colspan: 1,
			width: me.defaults.width * 1
		}, {
			xtype: 'displayfield',
			fieldLabel: '审核时间',
			name: 'ReaBmsCheckDoc_ExaminerDateTime',
			colspan: 1,
			width: me.defaults.width * 1
		});
		items.push({
			xtype: 'textarea',
			fieldLabel: '审核意见',
			name: 'ReaBmsCheckDoc_ExaminerMemo',
			itemId: 'ReaBmsCheckDoc_ExaminerMemo',
			colspan: 3,
			width: me.defaults.width * 3,
			height: 20
		});
		items.push({
			fieldLabel: '主键ID',
			name: 'ReaBmsCheckDoc_Id',
			hidden: true,
			type: 'key'
		});
		//备注
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'ReaBmsCheckDoc_Memo',
			itemId: 'ReaBmsCheckDoc_Memo',
			colspan: 5,
			width: me.defaults.width * 5,
			height: 40
		});
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		var reg = new RegExp("<br />", "g");
		data.ReaBmsCheckDoc_Memo = data.ReaBmsCheckDoc_Memo.replace(reg, "\r\n");

		if(data.ReaBmsCheckDoc_ExaminerDateTime) data.ReaBmsCheckDoc_ExaminerDateTime = JcallShell.Date.toString(data.ReaBmsCheckDoc_ExaminerDateTime, true);
		if(data.ReaBmsCheckDoc_CheckDateTime) data.ReaBmsCheckDoc_CheckDateTime = JcallShell.Date.toString(data.ReaBmsCheckDoc_CheckDateTime, true);

		var IsCompFlag = data.ReaBmsCheckDoc_IsCompFlag;
		if(IsCompFlag == "1" || IsCompFlag == 1 || IsCompFlag == true)
			IsCompFlag = true;
		else
			IsCompFlag = false;
		data.ReaBmsCheckDoc_IsCompFlag = IsCompFlag;

		var isStorageGoodsLink = data.ReaBmsCheckDoc_IsStorageGoodsLink;
		if(isStorageGoodsLink == "1" || isStorageGoodsLink == 1 || isStorageGoodsLink == true)
			isStorageGoodsLink = true;
		else
			isStorageGoodsLink = false;
		data.ReaBmsCheckDoc_IsStorageGoodsLink = isStorageGoodsLink;

		var IsHasZeroQty = data.ReaBmsCheckDoc_IsHasZeroQty;
		if(IsHasZeroQty == "1" || IsHasZeroQty == 1 || IsHasZeroQty == true)
			IsHasZeroQty = true;
		else
			IsHasZeroQty = false;
		data.ReaBmsCheckDoc_IsHasZeroQty = IsHasZeroQty;

		var IsException = data.ReaBmsCheckDoc_IsException;
		if(IsException == "1" || IsException == 1)
			IsException = 1;
		else
			IsException = 0;
		data.ReaBmsCheckDoc_IsException = IsException;

		var IsHandleException = data.ReaBmsCheckDoc_IsHandleException;
		if(IsHandleException == "1" || IsHandleException == 1)
			IsHandleException = 1;
		else
			IsHandleException = 0;
		data.ReaBmsCheckDoc_IsHandleException = IsHandleException;
		me.onSetReadOnlyOfLocked(true);
		return data;
	},
	onSetReadOnlyOfLocked: function(isLocked) {
		var me = this;
		var readOnly = isLocked ? true : false;
		me.getComponent("ReaBmsCheckDoc_IsCompFlag").setReadOnly(readOnly);
		me.getComponent("ReaBmsCheckDoc_CompanyName").setReadOnly(readOnly);
		me.getComponent("ReaBmsCheckDoc_PlaceName").setReadOnly(readOnly);
		me.getComponent("ReaBmsCheckDoc_StorageName").setReadOnly(readOnly);
		me.getComponent("ReaBmsCheckDoc_GoodsClass").setReadOnly(readOnly);
		me.getComponent("ReaBmsCheckDoc_GoodsClassType").setReadOnly(readOnly);
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		me.getComponent('ReaBmsCheckDoc_Status').setValue("1");
		me.onSetReadOnlyOfLocked(false);
	},
	/**供货方选择*/
	onCompAccept: function(record) {
		var me = this;
		var ReaCompanyID = me.getComponent('ReaBmsCheckDoc_ReaCompanyID');
		var CompanyName = me.getComponent('ReaBmsCheckDoc_CompanyName');
		var ReaServerCompCode = me.getComponent('ReaBmsCheckDoc_ReaServerCompCode');

		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		ReaCompanyID.setValue(id);
		CompanyName.setValue(text);
		ReaServerCompCode.setValue(platformOrgNo);
	},
	/**库房选择*/
	onStorageAccept: function(record) {
		var me = this;
		var StorageName = me.getComponent('ReaBmsCheckDoc_StorageName');
		var StorageID = me.getComponent('ReaBmsCheckDoc_StorageID');

		var idValue = record ? record.get('ReaStorage_Id') : '';
		var textValeu = record ? record.get('ReaStorage_CName') : '';
		StorageID.setValue(idValue);
		StorageName.setValue(textValeu);

		var PlaceName = me.getComponent('ReaBmsCheckDoc_PlaceName');
		var data = {
			defaultWhere: idValue ? "reaplace.ReaStorage.Id=" + idValue : ""
		};
		var readOnly = idValue ? false : true;
		PlaceName.setReadOnly(readOnly);
		PlaceName.changeClassConfig(data);
	},
	/**货架选择*/
	onPlaceAccept: function(record) {
		var me = this;
		var PlaceID = me.getComponent('ReaBmsCheckDoc_PlaceID');
		var PlaceName = me.getComponent('ReaBmsCheckDoc_PlaceName');

		var idValue = record ? record.get('ReaPlace_Id') : '';
		var textValeu = record ? record.get('ReaPlace_CName') : '';
		PlaceID.setValue(idValue);
		PlaceName.setValue(textValeu);
	},
	/**@desc 一级分类/二级分类选择*/
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var classTypeCom = me.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.fireEvent('onSearch', me);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -1,
			CheckDocNo: values.ReaBmsCheckDoc_CheckDocNo,
			CompanyName: values.ReaBmsCheckDoc_CompanyName,
			ReaServerCompCode: values.ReaBmsCheckDoc_ReaServerCompCode,
			StorageName: values.ReaBmsCheckDoc_StorageName,
			CreaterName: values.ReaBmsCheckDoc_CreaterName,
			PlaceName: values.ReaBmsCheckDoc_PlaceName
		};

		if(values.ReaBmsCheckDoc_GoodsClass) entity.GoodsClass = values.ReaBmsCheckDoc_GoodsClass;
		if(values.ReaBmsCheckDoc_GoodsClassType) entity.GoodsClassType = values.ReaBmsCheckDoc_GoodsClassType;
		if(values.ReaBmsCheckDoc_ExaminerMemo) entity.ExaminerMemo = values.ReaBmsCheckDoc_ExaminerMemo;
		entity.Memo = values.ReaBmsCheckDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');

		var IsCompFlag = values.ReaBmsCheckDoc_IsCompFlag;
		if(IsCompFlag == "1" || IsCompFlag == 1 || IsCompFlag == true)
			IsCompFlag = 1;
		else
			IsCompFlag = 0;
		entity.IsCompFlag = IsCompFlag;

		var IsStorageGoodsLink = values.ReaBmsCheckDoc_IsStorageGoodsLink;
		if(IsStorageGoodsLink == "1" || IsStorageGoodsLink == 1 || IsStorageGoodsLink == true)
			IsStorageGoodsLink = 1;
		else
			IsStorageGoodsLink = 0;
		entity.IsStorageGoodsLink = IsStorageGoodsLink;

		var IsHasZeroQty = values.ReaBmsCheckDoc_IsHasZeroQty;
		if(IsHasZeroQty == "1" || IsHasZeroQty == 1 || IsHasZeroQty == true)
			IsHasZeroQty = 1;
		else
			IsHasZeroQty = 0;
		entity.IsHasZeroQty = IsHasZeroQty;

		if(values.ReaBmsCheckDoc_Status) entity.Status = values.ReaBmsCheckDoc_Status;
		if(values.ReaBmsCheckDoc_CreaterID) entity.CreaterID = values.ReaBmsCheckDoc_CreaterID;
		if(values.ReaBmsCheckDoc_ReaCompanyID) entity.ReaCompanyID = values.ReaBmsCheckDoc_ReaCompanyID;
		if(values.ReaBmsCheckDoc_StorageID) entity.StorageID = values.ReaBmsCheckDoc_StorageID;
		if(values.ReaBmsCheckDoc_PlaceID) entity.PlaceID = values.ReaBmsCheckDoc_PlaceID;
		if(values.ReaBmsCheckDoc_ExaminerDateTime) entity.ExaminerDateTime = JShell.Date.toServerDate(values.ReaBmsCheckDoc_ExaminerDateTime);
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
			'Id', 'Status', 'ExaminerMemo', 'Memo'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.ReaBmsCheckDoc_Id;
		return entity;
	}
});