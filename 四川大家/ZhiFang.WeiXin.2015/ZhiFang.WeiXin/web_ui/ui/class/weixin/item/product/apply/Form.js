/**
 * 特推项目产品
 * @author liangyl
 * @version 2017-03-21
 */
Ext.define('Shell.class.weixin.item.product.apply.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '特推项目产品',
	width: 450,
	height: 320,
	bodyPadding: 10,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProductById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_AddOSRecommendationItemProduct',
	/**修改服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSRecommendationItemProductByField',
	/**图片服务地址*/
	UploadImgUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UploadRecommendationItemProductImageByID',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		//		anchor: '100%',
		width: 210,
		labelWidth: 75,
		labelAlign: 'right'
	},
	/**显示成功信息*/
	showSuccessInfo: false,
	PK: null,
	/**区域ID*/
	AreaID: null,
	/**名称*/
	AreaName: '',
	/**区域*/
	ClientNo:null,
	//B_Lab_TestItem ItemID
	ItemID: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//表单监听
		me.initFromListeners();
		
	},

	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this;		
		var items = [{
			fieldLabel: '区域',
			xtype: 'uxCheckTrigger',
			emptyText: '必填项',
			allowBlank: false,
			name: 'OSRecommendationItemProduct_Area',
			itemId: 'OSRecommendationItemProduct_Area',
			colspan: 1,
			className: 'Shell.class.weixin.hospital.area.CheckGrid'
		}, {
			fieldLabel: '区域主键ID',
			hidden: true,
			name: 'OSRecommendationItemProduct_AreaID',
			itemId: 'OSRecommendationItemProduct_AreaID'
		},{
			fieldLabel: '项目',
			name: 'OSRecommendationItemProduct_ItemName',
			itemId: 'OSRecommendationItemProduct_ItemName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.weixin.item.check.App',
			emptyText: '必填项',
			allowBlank: false
			
		},{
			fieldLabel: '名称',
			name: 'OSRecommendationItemProduct_CName',
			itemId: 'OSRecommendationItemProduct_CName',
			colspan: 2,
			width: me.defaults.width * 2,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '简称',
			colspan: 1,
			width: me.defaults.width * 1,
			name: 'OSRecommendationItemProduct_SName'
		}, {
			fieldLabel: '快捷码',
			colspan: 1,
			width: me.defaults.width * 1,
			name: 'OSRecommendationItemProduct_Shortcode'
		},  {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '开始时间',
			name: 'OSRecommendationItemProduct_StartDateTime',
			itemId: 'OSRecommendationItemProduct_StartDateTime',
			emptyText: '必填项',
			allowBlank: false,
			colspan: 1,
			width: me.defaults.width * 1
		},  {
			xtype: 'datefield',
			format: 'Y-m-d',
			fieldLabel: '结束时间',
			name: 'OSRecommendationItemProduct_EndDateTime',
			itemId: 'OSRecommendationItemProduct_EndDateTime',
			colspan: 1,
			emptyText: '必填项',
			allowBlank: false,
			width: me.defaults.width * 1
		}, {
			fieldLabel: '市场价格',
			name: 'OSRecommendationItemProduct_MarketPrice',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '大家价格',
			name: 'OSRecommendationItemProduct_GreatMasterPrice',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '折扣价格',
			name: 'OSRecommendationItemProduct_DiscountPrice',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '折扣率',
			name: 'OSRecommendationItemProduct_Discount',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '图片',
			name: 'OSRecommendationItemProduct_Image',
			itemId: 'OSRecommendationItemProduct_Image',
			colspan: 1,
			width: me.defaults.width * 1,
			xtype: 'filefield',
			buttonConfig: {
				iconCls: 'button-search',
				text: ''
			},
			emptyText: '只能上传图片png文件'
		}, {
			fieldLabel: '咨询费',
			name: 'OSRecommendationItemProduct_BonusPercent',
			itemId: 'OSRecommendationItemProduct_BonusPercent',
			xtype: 'numberfield',
			colspan: 1,
			width: me.defaults.width * 1,
			minValue:0
//			emptyText: '咨询费'
		}, {
			boxLabel: '是否使用',
			name: 'OSRecommendationItemProduct_IsUse',
			xtype: 'checkbox',
			colspan: 1,
			width: me.defaults.width * 1,
			checked: true,
			fieldLabel: ' ',
			labelSeparator: ''
		}, {
			boxLabel: '是否置顶',
			name: 'OSRecommendationItemProduct_IsTop',
			xtype: 'checkbox',
			colspan: 1,
			width: me.defaults.width * 1,
			//			checked: true,
			fieldLabel: ' ',
			labelSeparator: ''
		}, {
			boxLabel: '发布后是否可评论',
			name: 'OSRecommendationItemProduct_IsDiscuss',
			xtype: 'checkbox',
			colspan: 1,
			width: me.defaults.width * 1,
			//			checked: true,
			fieldLabel: ' ',
			labelSeparator: ''
		}, {
			fieldLabel: '描述',
			xtype: 'htmleditor',
			height: 100,
			hidden: true,

			colspan: 2,
			width: me.defaults.width * 2,
			name: 'OSRecommendationItemProduct_Memo',
			itemId: 'OSRecommendationItemProduct_Memo'
		}, {
			fieldLabel: '主键ID',
			name: 'OSRecommendationItemProduct_Id',
			hidden: true
		}, {
			fieldLabel: '项目Id',
			name: 'OSRecommendationItemProduct_ItemNo',
			itemId: 'OSRecommendationItemProduct_ItemNo',
			hidden: true
		}, {
			fieldLabel: '状态',
			name: 'OSRecommendationItemProduct_Status',
			itemId: 'OSRecommendationItemProduct_Status',
			hidden: true
		}];

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		if(!JShell.System.Cookie.map.USERID||!JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)) {
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		var entity = {
			DrafterId: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			DrafterCName: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			CName: values.OSRecommendationItemProduct_CName, //名称
			SName: values.OSRecommendationItemProduct_SName, //简称
			Shortcode: values.OSRecommendationItemProduct_Shortcode,
			MarketPrice: values.OSRecommendationItemProduct_MarketPrice,
			GreatMasterPrice: values.OSRecommendationItemProduct_GreatMasterPrice,
			DiscountPrice: values.OSRecommendationItemProduct_DiscountPrice,
			Discount: values.OSRecommendationItemProduct_Discount,
			IsTop: values.OSRecommendationItemProduct_IsTop ? 1 : 0,
			IsDiscuss: values.OSRecommendationItemProduct_IsDiscuss ? 1 : 0,
			IsUse: values.OSRecommendationItemProduct_IsUse ? 1 : 0,
			Memo: values.OSRecommendationItemProduct_Memo, //备注
			Status: values.OSRecommendationItemProduct_Status
		};
		if(values.OSRecommendationItemProduct_StartDateTime) {
			entity.StartDateTime = JShell.Date.toServerDate(values.OSRecommendationItemProduct_StartDateTime); //
		}
		if(values.OSRecommendationItemProduct_EndDateTime) {
			entity.EndDateTime = JShell.Date.toServerDate(values.OSRecommendationItemProduct_EndDateTime); //
		}
		if(values.OSRecommendationItemProduct_ItemNo) {
			entity.ItemNo = values.OSRecommendationItemProduct_ItemNo;
		}
		if(values.OSRecommendationItemProduct_AreaID) {
			entity.AreaID = values.OSRecommendationItemProduct_AreaID;
		}
		//B_Lab_TestItem ItemID
		if(me.ItemID) {
			entity.ItemID = me.ItemID;
		}
		if(values.OSRecommendationItemProduct_BonusPercent || values.OSRecommendationItemProduct_BonusPercent === 0){
			entity.BonusPercent = values.OSRecommendationItemProduct_BonusPercent;
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
			'CName', 'SName', 'Shortcode', 'MarketPrice',
			'GreatMasterPrice', 'DiscountPrice', 'Discount', 'IsTop',
			'IsDiscuss', 'IsUse', 'StartDateTime', 'EndDateTime','BonusPercent',
			'ItemID', 'Id', 'Status', 'Memo', 'ItemNo', 'AreaID'
		];
		entity.fields = fields.join(',');

		entity.entity.Id = values.OSRecommendationItemProduct_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.OSRecommendationItemProduct_StartDateTime = JShell.Date.getDate(data.OSRecommendationItemProduct_StartDateTime);
		data.OSRecommendationItemProduct_EndDateTime = JShell.Date.getDate(data.OSRecommendationItemProduct_EndDateTime);
		var ItemNo = me.getComponent('OSRecommendationItemProduct_ItemNo'),
			ItemName = me.getComponent('OSRecommendationItemProduct_ItemName');

		var AreaID = me.getComponent('OSRecommendationItemProduct_AreaID'),
			AreaName = me.getComponent('OSRecommendationItemProduct_Area');
		AreaName.setValue(me.AreaName);
		AreaID.setValue(me.AreaID);

		if(data.OSRecommendationItemProduct_ItemNo) {
			me.getItemName(data.OSRecommendationItemProduct_ItemNo, function(data) {
				if(data.value) {
					ItemName.setValue(data.value.list[0].BLabTestItem_CName);
					me.ItemID = data.value.list[0].BLabTestItem_Id;
					ItemName.classConfig = {
						ItemID: ItemNo.getValue() ? me.ItemID :'',
						AreaID:me.AreaID,
						ItemName:ItemName.getValue(),
						ClientNo:me.ClientNo
					};
				}
			});
		} else {
			ItemName.setValue('');
		}
		return data;
	},
	showItemName:function(AreaID){
	    var me=this;
	    var ItemName = me.getComponent('OSRecommendationItemProduct_ItemName');
        if(!AreaID){
        	ItemName.disable();
        }else{
        	ItemName.enable();
        }
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;
		
		var CName = me.getComponent('OSRecommendationItemProduct_CName');
		CName.on({
			change: function(field, newValue, oldValue, eOpts) {
				setTimeout(function() {
					me.changePinYinZiTou();
				}, 100);
			}
		});
		var ItemName = me.getComponent('OSRecommendationItemProduct_ItemName'),
			ItemNo = me.getComponent('OSRecommendationItemProduct_ItemNo');
		ItemName.on({
			check: function(p, record) {
				var Id = record ? record.get('BLabTestItem_ItemNo') : '';
				var Name = record ? record.get('BLabTestItem_CName') : '';
				ItemName.setValue(Name);
				ItemNo.setValue(Id);
				//ItemID赋值
				me.ItemID = record ? record.get('BLabTestItem_Id') : '';
				p.close();
			},
			beforetriggerclick:function(btn){
				if(!ItemNo.getValue()){
					btn.classConfig = {
						ItemID: '',
						AreaID:me.AreaID,
						ItemName:'',
						ClientNo:me.ClientNo
					};
				}
				
			}
		});
		var Image = me.getComponent('OSRecommendationItemProduct_Image');
		Image.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue) {
					var val = newValue.substring(newValue.length, newValue.toString().lastIndexOf("."));
					if(val.toString().toLowerCase() != '.png') {
						JShell.Msg.alert('只能上传png图片文件');
						com.setRawValue(oldValue);
					}
				}
			}
		});

		var Area = me.getComponent('OSRecommendationItemProduct_Area'),
			AreaID = me.getComponent('OSRecommendationItemProduct_AreaID');
		Area.on({
			check: function(p, record) {
				var Id = record ? record.get('ClientEleArea_Id') : '';
				var Name = record ? record.get('ClientEleArea_AreaCName') : '';
				var ClientNo = record ? record.get('ClientEleArea_ClientNo') : '';
				
				Area.setValue(Name);
				AreaID.setValue(Id);
				me.AreaID=Id;
				me.ClientNo=ClientNo;
				me.showItemName(Id);
				ItemName.classConfig = {
					title: '套餐项目选择',
					AreaID:me.AreaID,
					ClientNo:me.ClientNo
				};
				//选区域时清空套餐项目
				ItemNo.setValue('');
				ItemName.setValue('');
				p.close();
			}
		});
	},
	changePinYinZiTou: function(data) {
		var me = this,
			OSRecommendationItemProduct_CName = me.getComponent('OSRecommendationItemProduct_CName'),
			OSRecommendationItemProduct_Shortcode = me.getComponent('OSRecommendationItemProduct_Shortcode');

		var name = OSRecommendationItemProduct_CName.getValue();

		if(name != "") {
			JShell.Action.delay(function() {
				JShell.System.getPinYinZiTou(name, function(value) {
					me.getForm().setValues({
						OSRecommendationItemProduct_Shortcode: value
					});
				});
			}, null, 200);
		} else {
			me.getForm().setValues({
				OSRecommendationItemProduct_Shortcode: ""
			});
		}
	},
	/**更改标题*/
	changeTitle: function() {
		var me = this;
	},
	/**图片*/
	onSaveImage: function(file, Id, ImageType) {
		var me = this;
		var fileupload = {
			xtype: 'filefield',
			name: 'file',
			colspan: 1,
			itemId: 'file'
		};
		var isNull = false;
		if(file && file.fileInputEl.dom.files) {
			if(file.fileInputEl.dom.files.length > 0)
				fileupload = file.fileInputEl.dom.files[0];
			else
				isNull = true;
		} else {
			if(file.value != "" && file.value != undefined) {
				fileupload = file;
			} else {
				isNull = true;
			}
		}
		if(isNull == null) {
			JShell.Msg.alert("没有选择到文件!<br>", null, 1000);
			return;
		} else {
			var items = [];
			items.push(file);
			items.push({
				xtype: 'textfield',
				name: 'Id',
				colspan: 1,
				value: Id
			}, {
				xtype: 'textfield',
				name: 'ImageType',
				colspan: 1,
				value: ImageType
			});

			var uploadForm = Ext.create('Ext.form.Panel', {
				hidden: true,
				layout: {
					type: 'table',
					columns: 1
				},
				width: 100,
				height: 10,
				itemId: "uploadForm",
				items: items
			});
			var url = JShell.System.Path.getRootUrl(me.UploadImgUrl);
			uploadForm.getForm().submit({
				url: url,
				method: "POST",
				success: function(form, action) {
					var data = action.result;
					if(data.success) {
						me.fireEvent('saveclick', me);
						//						JShell.Msg.alert("上传成功!");
					}
				},
				failure: function(form, action) {
					var data = action.result;
					Ext.destroy(uploadForm);
					JShell.Msg.alert(data.ErrorInfo);
				}
			});
		}
	},
	/**获取套餐信息*/
	getItemName: function(Id, callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl('/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemByHQL?isPlanish=true');

		url += '&fields=BLabTestItem_ItemNo,BLabTestItem_CName,BLabTestItem_Id';
		url += "&where=blabtestitem.ItemNo='" + Id+"'";

		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			}
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			var AreaID = me.getComponent('OSRecommendationItemProduct_AreaID'),
				AreaName = me.getComponent('OSRecommendationItemProduct_Area');
			if(me.AreaID != '-1') {
				AreaName.setValue(me.AreaName);
				AreaID.setValue(me.AreaID);
			}
           me.showItemName(AreaName.getValue());
		} else {
			me.getForm().setValues(me.lastData);
		}
	}
});