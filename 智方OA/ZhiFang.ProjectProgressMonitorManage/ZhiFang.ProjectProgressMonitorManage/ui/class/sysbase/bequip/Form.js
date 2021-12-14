/**
 * 仪器信息
 * @author longfc
 * @version 2015-09-29
 */
Ext.define('Shell.class.sysbase.bequip.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	bodyPadding: '5px 5px 5px 5px',
	layout: {
		type: 'table',
		columns: 4 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 75,
		width: 220,
		labelAlign: 'right'
	},
	title: '仪器信息',
	//width: 760,
	height: 550,
	/**获取数据服务路径*/
	//selectUrl: '/ui/class/sysbase/bequip/test/form.json',
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBEquipById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddBEquip',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateBEquipByField',
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否重置按钮*/
	hasReset: false,
	/**启用表单状态初始化*/
	openFormType: false,
	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**字典类型里的仪器类型ID*/
	EquipTypeId: '',
	EquipTypeCName: '',
	/**字典类型里的仪器厂商品牌ID*/
	EquipFactoryBrandId: "",
	EquipFactoryBrandCName: '',
	/*仪器厂商品牌ID**/
	EBRADID: '4777630349498328266',
	/*仪器分类**/
	ETYPEID: '5724611581318422977',
	/**内容自动显示*/
	autoScroll: false,
	isLoadingComplete: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.formtype == "add") {
			me.changeFullCNameValue();
		}
	},
	initComponent: function() {
		var me = this;
		me.width = me.width;
		var width = me.width / 4 - 15;
		me.defaults.width = (width < 220) ? 220 : width;
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		//me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.createAddShowItems();
		items = items.concat(me.getAddFFileTableLayoutItems());
		//创建隐形组件
		items = items.concat(me.createHideItems());
		return items;
	},
	/**创建可见组件*/
	createAddShowItems: function() {
		var me = this;
		me.createBEquip_CName("仪器名称");
		me.createBEquip_Shortcode('SQH号');
		me.createBEquip_PinYinZiTou("拼音字头");
		me.createBEquip_EName('英文名称');
		me.createBEquip_SName('简称');

		me.createBEquip_UseCode('代码');
		me.createBEquip_DispOrder('显示次序');
		me.createBEquip_EquipFactoryBrand_CName("仪器品牌");
		me.createBEquip_EquipType_CName("仪器分类");
		me.createBEquip_Equipversion("仪器型号");
		me.createBEquip_IsUse('是否使用');
		me.createBEquip_FullCName("仪器全称");
		me.createBEquip_Comment('描述');
		me.createBEquip_Memo('概要说明');
	},
	/**@overwrite 获取列表布局组件内容*/
	getAddFFileTableLayoutItems: function() {
		var me = this,
			items = [];
		//名称
		me.BEquip_CName.colspan = 1;
		me.BEquip_CName.width = me.defaults.width * me.BEquip_CName.colspan;
		items.push(me.BEquip_CName);

		//仪器分类
		me.BEquip_EquipType_CName.colspan = 1;
		me.BEquip_EquipType_CName.width = me.defaults.width * me.BEquip_EquipType_CName.colspan;
		items.push(me.BEquip_EquipType_CName);

		//仪器品牌
		me.BEquip_EquipFactoryBrand_CName.colspan = 1;
		me.BEquip_EquipFactoryBrand_CName.width = me.defaults.width * me.BEquip_EquipFactoryBrand_CName.colspan;
		items.push(me.BEquip_EquipFactoryBrand_CName);

		//仪器型号
		me.BEquip_Equipversion.colspan = 1;
		me.BEquip_Equipversion.width = me.defaults.width * me.BEquip_Equipversion.colspan;
		items.push(me.BEquip_Equipversion);

		//第二行
		me.BEquip_PinYinZiTou.colspan = 1;
		me.BEquip_PinYinZiTou.width = me.defaults.width * me.BEquip_PinYinZiTou.colspan;
		items.push(me.BEquip_PinYinZiTou);

		me.BEquip_EName.colspan = 1;
		me.BEquip_EName.width = me.defaults.width * me.BEquip_EName.colspan;
		items.push(me.BEquip_EName);

		me.BEquip_SName.colspan = 1;
		me.BEquip_SName.width = me.defaults.width * me.BEquip_SName.colspan;
		items.push(me.BEquip_SName);

		me.BEquip_Shortcode.colspan = 1;
		me.BEquip_Shortcode.width = me.defaults.width * me.BEquip_Shortcode.colspan;
		items.push(me.BEquip_Shortcode);

		//第三行
		me.BEquip_UseCode.colspan = 1;
		me.BEquip_UseCode.width = me.defaults.width * me.BEquip_UseCode.colspan;
		items.push(me.BEquip_UseCode);

		me.BEquip_DispOrder.colspan = 1;
		me.BEquip_DispOrder.width = me.defaults.width * me.BEquip_DispOrder.colspan;
		items.push(me.BEquip_DispOrder);

		me.BEquip_IsUse.colspan = 2;
		items.push(me.BEquip_IsUse);

		//仪器全称描述
		me.BEquip_FullCName.colspan = 4;
		me.BEquip_FullCName.width = me.defaults.width * me.BEquip_FullCName.colspan;
		items.push(me.BEquip_FullCName);

		//仪器描述
		me.BEquip_Comment.colspan = 4;
		me.BEquip_Comment.width = me.defaults.width * me.BEquip_Comment.colspan;
		items.push(me.BEquip_Comment);

		//备注
		me.BEquip_Memo.colspan = 4;
		me.BEquip_Memo.width = me.defaults.width * me.BEquip_Memo.colspan;
		items.push(me.BEquip_Memo);
		return items;
	},
	/*
	 * 仪器全称,自动生成
	 * 声称规则：分类+‘-’+品牌+‘-’+型号
	 * */
	createBEquip_FullCName: function(fieldLabel) {
		var me = this;
		me.BEquip_FullCName = {
			xtype: 'displayfield',
			fieldLabel: fieldLabel,
			name: 'BEquip_FullCName',
			itemId: 'BEquip_FullCName'
		};
	},
	/*
	 * 仪器描述
	 * */
	createBEquip_Comment: function(fieldLabel) {
		var me = this;
		me.BEquip_Comment = {
			fieldLabel: fieldLabel,
			name: 'BEquip_Comment',
			minHeight: 50,
			height: 80,
			maxLength: 500,
			maxLengthText: "摘要最多只能输入500字",
			style: {
				marginBottom: '2px'
			},
			xtype: 'textarea'
		};
	},
	/**摘要*/
	createBEquip_Memo: function(fieldLabel) {
		var me = this;
		var minHeight = me.height * 0.45,
			height = me.height * 0.47;
		me.BEquip_Memo = {
			fieldLabel: fieldLabel,
			name: 'BEquip_Memo',
			minHeight: minHeight,
			height: height,
			//maxLength: 500,
			maxLengthText: "仪器摘要信息",
			style: {
				marginBottom: '2px'
			},
			xtype: 'textarea'
		};
	},
	/*仪器厂商品牌ID选择***/
	createBEquip_EquipFactoryBrand_CName: function(fieldLabel) {
		var me = this;
		me.BEquip_EquipFactoryBrand_CName = {
			fieldLabel: fieldLabel,
			name: 'BEquip_EquipFactoryBrand_CName',
			itemId: 'BEquip_EquipFactoryBrand_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			value: me.EquipFactoryBrandCName,
			classConfig: {
				title: fieldLabel,
				height: 480,
				defaultWhere: "pdict.PDictType.Id=" + me.EBRADID + ""
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('BEquip_EquipFactoryBrand_CName');
					var Id = me.getComponent('BEquip_EquipFactoryBrand_Id');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');
					me.changeFullCNameValue();
					p.close();
				}
			}
		};
	},
	createBEquip_EquipType_CName: function(fieldLabel) {
		var me = this;
		me.BEquip_EquipType_CName = {
			fieldLabel: fieldLabel,
			name: 'BEquip_EquipType_CName',
			itemId: 'BEquip_EquipType_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.dict.CheckGrid',
			value: me.EquipTypeCName,
			classConfig: {
				title: fieldLabel,
				height: 480,
				defaultWhere: "pdict.PDictType.Id=" + me.ETYPEID + ""
			},
			listeners: {
				check: function(p, record) {
					var CName = me.getComponent('BEquip_EquipType_CName');
					var Id = me.getComponent('BEquip_EquipType_Id');
					CName.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');
					me.changeFullCNameValue();
					p.close();
				}
			}
		};
	},
	createBEquip_Equipversion: function(fieldLabel) {
		var me = this;
		me.BEquip_Equipversion = {
			fieldLabel: fieldLabel,
			name: 'BEquip_Equipversion',
			itemId: 'BEquip_Equipversion',
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeFullCNameValue();
				}
			}
		};
	},
	/*
	 * 仪器全称处理
	 * 声称规则：分类+‘-’+品牌+‘-’+型号
	 * 如果分类++品牌++型号都没有值,取仪器中文名称值
	 **/
	changeFullCNameValue: function() {
		var me = this;
		var EquipType = me.getComponent('BEquip_EquipType_CName');
		var EquipFactoryBrand = me.getComponent('BEquip_EquipFactoryBrand_CName');
		var Equipversion = me.getComponent('BEquip_Equipversion');
		var FullCName = me.getComponent('BEquip_FullCName');

		var newValue = "",
			oldValue = FullCName.getValue();
		if(EquipType) {
			newValue = EquipType.getValue();
		}
		if(EquipFactoryBrand && EquipFactoryBrand.getValue() != "") {
			newValue = (newValue != "" ? newValue + "-" + EquipFactoryBrand.getValue() : EquipFactoryBrand.getValue());
		}
		if(Equipversion && Equipversion.getValue() != "") {
			newValue = (newValue != "" ? newValue + "-" + Equipversion.getValue() : Equipversion.getValue());
		}
		if(newValue != "") {
			FullCName.setValue(newValue);
		} else {
			var CName = me.getComponent('BEquip_CName');
			FullCName.setValue(CName.getValue());
		}
	},
	createBEquip_CName: function(fieldLabel) {
		var me = this;
		me.BEquip_CName = {
			fieldLabel: fieldLabel,
			itemId: 'BEquip_CName',
			name: 'BEquip_CName',
			emptyText: '必填项',
			allowBlank: false,
			listeners: {
				change: function(field, newValue, oldValue) {
					me.changeFullCNameValue();
					//拼音字头的处理

					if(me.formtype == 'add') {
						me.isLoadingComplete = true;
					}
					if(me.isLoadingComplete == true) {
						var obj = {
							'BEquip_PinYinZiTou': ""
						};
						me.cnameChangeForPinYinZiTou(obj, newValue);
					} else {
						me.isLoadingComplete = true;
					}
				}
			}
		};
	},
	/**
	 * 更改中文名称时拼音头处理
	 * @private
	 */
	cnameChangeForPinYinZiTou: function(obj, newValue) {
		var me = this;
		var changePinYinZiTou = function(newValue) {
			obj["BEquip_PinYinZiTou"] = newValue;
			me.getForm().setValues(obj);
		}
		var BEquip_PinYinZiTou = me.getComponent('BEquip_PinYinZiTou');
		if(newValue != "") {
			var url = JShell.System.Path.ROOT + '/ConstructionService.svc/GetPinYin?chinese=' + newValue,
				isAscii = escape(newValue).indexOf("%u") == -1 ? true : false;
			if(isAscii) { //全英文直接联动
				BEquip_PinYinZiTou.setValue(newValue);
			} else {
				JShell.Action.delay(function() {
					JShell.Server.get(url, function(data) {
						BEquip_PinYinZiTou.setValue(data.value);
					});
				});
			}
		} else {
			obj["BEquip_PinYinZiTou"] = "";
			me.getForm().setValues(obj);
		}
	},
	createBEquip_Shortcode: function(fieldLabel) {
		var me = this;
		me.BEquip_Shortcode = {
			fieldLabel: fieldLabel,
			name: 'BEquip_Shortcode'
		};
	},
	createBEquip_PinYinZiTou: function(fieldLabel) {
		var me = this;
		me.BEquip_PinYinZiTou = {
			fieldLabel: fieldLabel,
			itemId: 'BEquip_PinYinZiTou',
			name: 'BEquip_PinYinZiTou'
		};
	},
	createBEquip_EName: function(fieldLabel) {
		var me = this;
		me.BEquip_EName = {
			fieldLabel: fieldLabel,
			name: 'BEquip_EName'
		};
	},
	createBEquip_SName: function(fieldLabel) {
		var me = this;
		me.BEquip_SName = {
			fieldLabel: fieldLabel,
			name: 'BEquip_SName'
		};
	},
	createBEquip_UseCode: function(fieldLabel) {
		var me = this;
		me.BEquip_UseCode = {
			fieldLabel: fieldLabel,
			name: 'BEquip_UseCode'
		};
	},
	createBEquip_DispOrder: function(fieldLabel) {
		var me = this;
		me.BEquip_DispOrder = {
			xtype: 'numberfield',
			fieldLabel: fieldLabel,
			name: 'BEquip_DispOrder'
		};
	},

	createBEquip_IsUse: function(fieldLabel) {
		var me = this;
		me.BEquip_IsUse = {
			boxLabel: fieldLabel,
			name: 'BEquip_IsUse',
			itemId: 'BEquip_IsUse',
			xtype: 'checkbox',
			checked: true,
			style: {
				marginLeft: '50px'
			}
		};
	},
	/**@overwrite 创建隐形组件*/
	createHideItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			name: 'BEquip_Id',
			hidden: true
		}, {
			fieldLabel: '仪器分类ID',
			name: 'BEquip_EquipType_Id',
			itemId: 'BEquip_EquipType_Id',
			value: me.EquipTypeId,
			hidden: true
		}, {
			fieldLabel: '仪器品牌ID',
			itemId: 'BEquip_EquipFactoryBrand_Id',
			name: 'BEquip_EquipFactoryBrand_Id',
			value: me.EquipFactoryBrandId,
			hidden: true
		});

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var strDataTimeStamp = "1,2,3,4,5,6,7,8";
		var entity = {
			CName: values.BEquip_CName,
			EName: values.BEquip_EName,
			SName: values.BEquip_SName,
			Shortcode: values.BEquip_Shortcode,
			PinYinZiTou: values.BEquip_PinYinZiTou,
			UseCode: values.BEquip_UseCode,
			Equipversion: values.BEquip_Equipversion,
			IsUse: values.BEquip_IsUse ? true : false
		};

		var FullCName = me.getComponent('BEquip_FullCName');
		if(FullCName && FullCName.getValue() != "") {
			entity.FullCName = FullCName.getValue();
		}

		if(values.BEquip_DispOrder) {
			entity.DispOrder = values.BEquip_DispOrder;
		}
		if(values.BEquip_EquipType_Id) {
			entity.EquipType = {
				Id: values.BEquip_EquipType_Id,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
		if(values.BEquip_EquipFactoryBrand_Id) {
			entity.EquipFactoryBrand = {
				Id: values.BEquip_EquipFactoryBrand_Id,
				DataTimeStamp: strDataTimeStamp.split(',')
			};
		}
		//描述
		if(values.BEquip_Comment) {
			entity.Comment = values.BEquip_Comment.replace(/\\/g, '&#92');
			entity.Comment = entity.Comment.replace(/[\r\n]/g, '<br />');
		}
		//概要
		if(values.BEquip_Memo) {
			entity.Memo = values.BEquip_Memo.replace(/\\/g, '&#92');
			entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');
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

		for(var i in fields) {
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			fieldsArr.push(arr[1]);
		}
		fieldsArr.push('EquipType_Id');
		fieldsArr.push('EquipFactoryBrand_Id');
		entity.fields = fieldsArr.join(',');

		entity.entity.Id = values.BEquip_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var reg = new RegExp("<br />", "g");
		data.BEquip_Memo = data.BEquip_Memo.replace(reg, "\r\n");
		data.BEquip_Comment = data.BEquip_Comment.replace(reg, "\r\n");
		return data;
	}
});