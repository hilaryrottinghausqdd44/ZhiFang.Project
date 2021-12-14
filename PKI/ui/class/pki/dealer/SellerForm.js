/**
 * 经销商销售表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.dealer.SellerForm', {
	extend: 'Shell.ux.form.Panel',

	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '经销商销售表单',
	width: 330,
	height: 250,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchDDealSellerById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BaseService.svc/ST_UDTO_AddDDealSeller',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateDDealSellerByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/**经销商ID*/
	DealerId: null,
	/**经销商时间戳*/
//	DealerDataTimeStamp: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '主键ID',
			name: 'DDealSeller_Id',
			hidden: true
		});

		//销售【勾选列表】
		items.push({
			x: 0,
			y: 40,
			width: 310,
			fieldLabel: '销售',
			name: 'DDealSeller_BSeller_Name',
			itemId: 'DDealSeller_BSeller_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.pki.seller.CheckGrid',
			allowBlank: false
		}, {
			fieldLabel: '销售主键ID',
			name: 'DDealSeller_BSeller_Id',
			itemId: 'DDealSeller_BSeller_Id',
			hidden: true
		}, {
			fieldLabel: '销售时间戳',
			name: 'DDealSeller_BSeller_DataTimeStamp',
			itemId: 'DDealSeller_BSeller_DataTimeStamp',
			hidden: true
		});

		items.push({
			x: 0,
			y: 10,
			width: 155,
			fieldLabel: '开始日期',
			name: 'DDealSeller_BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});
		items.push({
			x: 155,
			y: 10,
			width: 155,
			fieldLabel: '截止日期',
			name: 'DDealSeller_EndDate',
			xtype: 'datefield',
			format: 'Y-m-d',
			allowBlank: false
		});

		items.push({
			x: 0,
			y: 70,
			width: 310,
			fieldLabel: '说明',
			name: 'DDealSeller_Explain'
		});
		items.push({
			x: 0,
			y: 100,
			width: 310,
			height: 80,
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'DDealSeller_Comment'
		});

		return items;
	},
	initListeners: function() {
		var me = this,
			Name = me.getComponent('DDealSeller_BSeller_Name'),
			Id = me.getComponent('DDealSeller_BSeller_Id'),
			DataTimeStamp = me.getComponent('DDealSeller_BSeller_DataTimeStamp');

		Name.on({
			check: function(p, record) {
				Id.setValue(record ? record.get('BSeller_Id') : '');
				Name.setValue(record ? record.get('BSeller_Name') : '');
				DataTimeStamp.setValue(record ? record.get('BSeller_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.DDealSeller_BeginDate = JShell.Date.getDate(data.DDealSeller_BeginDate);
		data.DDealSeller_EndDate = JShell.Date.getDate(data.DDealSeller_EndDate);
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			BeginDate: JShell.Date.toServerDate(values.DDealSeller_BeginDate),
			EndDate: JShell.Date.toServerDate(values.DDealSeller_EndDate),
			Explain: values.DDealSeller_Explain,
			Comment: values.DDealSeller_Comment,
			BDealer: {
				Id: me.DealerId,
				DataTimeStamp :[0,0,0,0,0,0,0,0]

			},
			BSeller: {
				Id: values.DDealSeller_BSeller_Id,
				DataTimeStamp: values.DDealSeller_BSeller_DataTimeStamp.split(',')
			}
		};
//
//		if(me.DealerDataTimeStamp) {
//			entity.BDealer.DataTimeStamp = me.DealerDataTimeStamp;
//		}

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			//fields = me.getStoreFields(),
			entity = me.getAddParams();

		//		for(var i in fields) {
		//			if(fields[i] != "DDealSeller_BSeller_Name") {
		//				fields[i] = fields[i].split('_').slice(1);
		//			}
		//		}
		entity.fields = "Id,BSeller_Id,BeginDate,EndDate,Explain,Comment"; // fields.join(',');

		entity.entity.Id = values.DDealSeller_Id;
		return entity;
	}
});