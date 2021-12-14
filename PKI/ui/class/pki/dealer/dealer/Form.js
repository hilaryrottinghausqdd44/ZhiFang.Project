/**
 * 经销商表单
 * @author liangyl	
 * @version 2017-07-18
 */
Ext.define('Shell.class.pki.dealer.dealer.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger', 
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '经销商表单',
	width: 650,
	height: 470,
	UseCode: '',
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBDealerById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/BaseService.svc/ST_UDTO_AddBDealer',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateBDealerByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
    /**经销商ID*/
	BDealerId:'',
	/**经销商称*/
	BDealerName:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			unitTypeObj = JShell.PKI.Enum.DealerType,
			DealerTypeList = [],
			items = [];
		for (var i in unitTypeObj) {
			DealerTypeList.push([i.slice(1), unitTypeObj[i]]);
		}
		//maxLength:20,enforceMaxLength:true
		items.push({
			fieldLabel: '主键ID',
			name: 'BDealer_Id',
			hidden: true
		});

		items.push({
			x: 10,
			y: 10,
			fieldLabel: '名称',
			name: 'BDealer_Name',
			allowBlank: false
		});
		items.push({
			x: 220,
			y: 10,
			fieldLabel: '简称',
			name: 'BDealer_SName'
		});
		items.push({
			x: 430,
			y: 10,
			fieldLabel: '快捷码',
			name: 'BDealer_Shortcode'
		});

		items.push({
			x: 10,
			y: 40,
			fieldLabel: '用户代码',
			name: 'BDealer_UseCode',
			allowBlank: false,
			value: me.UseCode
		});
		items.push({
			x: 220,
			y: 40,
			fieldLabel: '拼音字头',
			name: 'BDealer_PinYinZiTou'
		});
		items.push({
			x: 430,
			y: 40,
			fieldLabel: '联系方式',
			name: 'BDealer_ContactInfo'
		});

		//开票方
		items.push({
			x: 10,
			y: 70,
			fieldLabel: '开票方',
			name: 'BDealer_BBillingUnit_Name',
			itemId: 'BDealer_BBillingUnit_Name',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.pki.billingunit.CheckGrid', {
					resizable: false,
					listeners: {
						accept: function(p, record) {
							me.onBillingUnitAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel: '开票方主键ID',
			hidden: true,
			name: 'BDealer_BBillingUnit_Id',
			itemId: 'BDealer_BBillingUnit_Id'
		});
		items.push({
			fieldLabel: '开票方时间戳',
			hidden: true,
			name: 'BDealer_BBillingUnit_DataTimeStamp',
			itemId: 'BDealer_BBillingUnit_DataTimeStamp'
		});

		items.push({
			x: 220,
			y: 70,
			xtype: 'uxBoolComboBox',
			fieldLabel: '使用',
			name: 'BDealer_IsUse',
			value: true
		});
		items.push({
			x: 430,
			y: 70,
			xtype: 'numberfield',
			fieldLabel: '显示次序',
			name: 'BDealer_DispOrder',
			value: 0
		});
		items.push({
			x: 0,
			y: 100,
			xtype: 'uxSimpleComboBox',
			fieldLabel: '经销商类型',
			name: 'BDealer_DealerType',
			labelWidth: 70,
			data: DealerTypeList,
			width: 210,
			value: '0'
		});
		//上级经销商
		items.push({
			x: 210,
			y: 100,labelWidth: 70,width: 210,
		    fieldLabel:'上级经销商',name:'BDealer_ParentName',itemId:'BDealer_ParentName',
			xtype:'uxCheckTrigger',className:'Shell.class.pki.dealer.dealer.CheckTree',
			IsnotField:true,value:me.BDealerName,
			classConfig:{
				title:'上级经销商选择',
				selectId:me.BDealerId,//默认选中节点ID
				hideNodeId:me.PK//默认隐藏节点ID
			}
		});
		items.push({fieldLabel: '上级经销商ID',hidden: true,value:me.BDealerId,name: 'BDealer_ParentID',itemId: 'BDealer_ParentID'});
		items.push({
			x: 10,
			y: 130,
			width: 620,
			height: 80,
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'BDealer_Comment'
		});

		//联系人1
		items.push({
			xtype: 'fieldset',
			title: '联系人1',
			collapsible: true,
			layout: 'anchor',
			defaultType: 'textfield',
			defaults: {
				anchor: '100%',
				labelWidth: 40,
				labelAlign: 'right'
			},
			x: 10,
			y: 215,
			width: 200,
			items: [{
				fieldLabel: '联系人',
				name: 'BDealer_ContactPerson'
			}, {
				fieldLabel: '职务',
				name: 'BDealer_PersonPost'
			}, {
				fieldLabel: '手机',
				name: 'BDealer_CellPhone'
			}, {
				fieldLabel: '电话',
				name: 'BDealer_PhoneCode'
			}, {
				fieldLabel: 'Email',
				name: 'BDealer_EMail'
			}]
		});
		//联系人2
		items.push({
			xtype: 'fieldset',
			title: '联系人2',
			collapsible: true,
			layout: 'anchor',
			defaultType: 'textfield',
			defaults: {
				anchor: '100%',
				labelWidth: 40,
				labelAlign: 'right'
			},
			x: 220,
			y: 215,
			width: 200,
			items: [{
				fieldLabel: '联系人',
				name: 'BDealer_ContactPerson2'
			}, {
				fieldLabel: '职务',
				name: 'BDealer_PersonPost2'
			}, {
				fieldLabel: '手机',
				name: 'BDealer_CellPhone2'
			}, {
				fieldLabel: '电话',
				name: 'BDealer_PhoneCode2'
			}, {
				fieldLabel: 'Email',
				name: 'BDealer_EMail2'
			}]
		});
		//联系人3
		items.push({
			xtype: 'fieldset',
			title: '联系人3',
			collapsible: true,
			layout: 'anchor',
			defaultType: 'textfield',
			defaults: {
				anchor: '100%',
				labelWidth: 40,
				labelAlign: 'right'
			},
			x: 430,
			y: 215,
			width: 200,
			items: [{
				fieldLabel: '联系人',
				name: 'BDealer_ContactPerson3'
			}, {
				fieldLabel: '职务',
				name: 'BDealer_PersonPost3'
			}, {
				fieldLabel: '手机',
				name: 'BDealer_CellPhone3'
			}, {
				fieldLabel: '电话',
				name: 'BDealer_PhoneCode3'
			}, {
				fieldLabel: 'Email',
				name: 'BDealer_EMail3'
			}]
		});
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Name: values.BDealer_Name,
			SName: values.BDealer_SName,
			Shortcode: values.BDealer_Shortcode,
			PinYinZiTou: values.BDealer_PinYinZiTou,
			PhoneCode: values.BDealer_PhoneCode,
			ContactInfo: values.BDealer_ContactInfo,
			IsUse: values.BDealer_IsUse,
			DispOrder: values.BDealer_DispOrder,
			Comment: values.BDealer_Comment,
			DealerType: values.BDealer_DealerType || '0',
			//代码
			UseCode: values.BDealer_UseCode,
			//联系人1
			ContactPerson: values.BDealer_ContactPerson,
			PersonPost: values.BDealer_PersonPost,
			CellPhone: values.BDealer_CellPhone,
			PhoneCode: values.BDealer_PhoneCode,
			EMail: values.BDealer_EMail,
			//联系人2
			ContactPerson2: values.BDealer_ContactPerson2,
			PersonPost2: values.BDealer_PersonPost2,
			CellPhone2: values.BDealer_CellPhone2,
			PhoneCode2: values.BDealer_PhoneCode2,
			EMail2: values.BDealer_EMail2,
			//联系人3
			ContactPerson3: values.BDealer_ContactPerson3,
			PersonPost3: values.BDealer_PersonPost3,
			CellPhone3: values.BDealer_CellPhone3,
			PhoneCode3: values.BDealer_PhoneCode3,
			EMail3: values.BDealer_EMail3

		};

		if (values.BDealer_BBillingUnit_Id) {
			entity.BBillingUnit = {
				Id: values.BDealer_BBillingUnit_Id,
				DataTimeStamp: values.BDealer_BBillingUnit_DataTimeStamp.split(','),
			}
		}
		if(values.BDealer_ParentID){
			entity.ParentID =values.BDealer_ParentID;
		}

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

		for (var i in fields) {
			var arr = fields[i].split('_');
			if(fields[i]!='BDealer_ParentName') {
				if (arr.length > 2) continue;
			    fieldsArr.push(arr[1]);
			}
		}
		entity.fields = fieldsArr.join(',');
		entity.fields += ',BBillingUnit_Id';
		entity.entity.Id = values.BDealer_Id;
		return entity;
	},
	/**开票方选择确认处理*/
	onBillingUnitAccept: function(record) {
		var me = this;
		var Id = me.getComponent('BDealer_BBillingUnit_Id');
		var Name = me.getComponent('BDealer_BBillingUnit_Name');
		var DataTimeStamp = me.getComponent('BDealer_BBillingUnit_DataTimeStamp');

		Id.setValue(record ? record.get('BBillingUnit_Id') : '');
		Name.setValue(record ? record.get('BBillingUnit_Name') : '');
		DataTimeStamp.setValue(record ? record.get('BBillingUnit_DataTimeStamp') : '');
	},
	/**创建数据字段*/
	getStoreFields: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = [];

		for (var i in values) {
			fields.push(i);
		}

		return fields;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;

		if (!me.getForm().isValid()) return;

		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();

		if (!params) return;

		params = Ext.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				me.fireEvent('save', me);
				if (me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if (msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '经销商代码有重复';
				}
				JShell.Msg.error(msg);
			}
		});
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//上级经销商选择监听
		var ParentName = me.getComponent('BDealer_ParentName'),
			ParentID = me.getComponent('BDealer_ParentID');
		if(ParentName){
			ParentName.on({
				check: function(p, record) {
					ParentName.setValue(record ? record.get('text') : '');
					ParentID.setValue(record ? record.get('tid') : '');
					p.close();
				}
			});
		}
	}
});