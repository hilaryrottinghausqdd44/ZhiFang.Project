/**
 * 医生表单
 * @author Jcall
 * @version 2016-12-27
 */
Ext.define('Shell.class.weixin.doctor.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],

	title: '医生信息',
	width: 270,
	height: 480,
	bodyPadding: 10,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccountById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_AddBDoctorAccount',
	/**修改服务地址*/
	editUrl: '/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBDoctorAccountByField',
	/**图片服务地址*/
	UploadImgUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UploadBDoctorAccountImageByAccountID',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 90,
		labelAlign: 'right'
	},
	/**银行种类*/
	Bank: 'SYS1002',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**医生账号类型*/
	DoctorTypeList:[
	    ['1', '普通医生'],
	    ['2', '检验技师']
	],
	/**默认医生账号类型*/
	defaultDoctorType:'1',
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
			fieldLabel: '医院',
			xtype: 'uxCheckTrigger',
			emptyText: '必填项',
			allowBlank: false,
			name: 'BDoctorAccount_HospitalName',
			itemId: 'BDoctorAccount_HospitalName',
			className: 'Shell.class.weixin.hospital.CheckGrid'
		}, {
			fieldLabel: '医院编码',
//			hidden: true,
			name: 'BDoctorAccount_HospitalCode',
			itemId: 'BDoctorAccount_HospitalCode'
		},  {
			fieldLabel: '医院Id',
			hidden: true,
			name: 'BDoctorAccount_HospitalID',
			itemId: 'BDoctorAccount_HospitalID'
		},{
			fieldLabel: '区域id',
			hidden: true,
			name: 'BDoctorAccount_AreaID',
			itemId: 'BDoctorAccount_AreaID'
		},{
			fieldLabel: '科室',
			xtype: 'uxCheckTrigger',
			name: 'BDoctorAccount_HospitalDeptName',
			itemId: 'BDoctorAccount_HospitalDeptName',
			className: 'Shell.class.weixin.dept.CheckGrid'
		}, {
			fieldLabel: '科室主键ID',
			hidden: true,
			name: 'BDoctorAccount_HospitalDeptID',
			itemId: 'BDoctorAccount_HospitalDeptID'
		}, {
			fieldLabel: '医生类型',name: 'BLabTestItem_DoctorAccountType',
			itemId: 'BLabTestItem_DoctorAccountType', xtype: 'uxSimpleComboBox',
			hasStyle: true,data: me.DoctorTypeList,value:me.defaultDoctorType,
			colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel: '姓名',
			name: 'BDoctorAccount_Name',
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '工号',
			name: 'BDoctorAccount_HWorkNumberID'
		}, {
			fieldLabel: '专业级别',
			xtype: 'uxCheckTrigger',
			//			hidden:true,
			name: 'BDoctorAccount_ProfessionalAbilityName',
			itemId: 'BDoctorAccount_ProfessionalAbilityName',
			className: 'Shell.class.weixin.doctor.professionalability.CheckGrid'
		}, {
			fieldLabel: '专业级别ID',
			hidden: true,
			name: 'BDoctorAccount_ProfessionalAbilityID',
			itemId: 'BDoctorAccount_ProfessionalAbilityID'
		}, {
			fieldLabel: '职业证书',
			name: 'BDoctorAccount_ProfessionalAbilityImageUrl',
			itemId: 'BDoctorAccount_ProfessionalAbilityImageUrl',
			xtype: 'filefield',
			buttonConfig: {
				iconCls: 'button-search',
				text: ''
			},
			emptyText: '只能上传图片png文件'
		}, {
			xtype: 'filefield',
			fieldLabel: '个人照片',
			name: 'BDoctorAccount_PersonImageUrl',
			itemId: 'BDoctorAccount_PersonImageUrl',
			buttonConfig: {
				iconCls: 'button-search',
				text: ''
			},
			emptyText: '只能上传图片png文件'
		},{
			fieldLabel: '咨询费比率(%)',
			name: 'BDoctorAccount_BonusPercent',
			xtype: 'numberfield',
			minValue:0,
			maxValue:100,
			emptyText: '百分率在0-100之间'
		}, {
			fieldLabel: '银行种类',
			name: 'BDoctorAccount_Bank',
			itemId: 'BDoctorAccount_Bank',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '银行种类选择',
				defaultWhere: "bdict.BDictType.DictTypeCode='"+me.Bank+"'"
			}
		}, {
			fieldLabel: '银行种类ID',
			hidden: true,
			name: 'BDoctorAccount_BankID',
			itemId: 'BDoctorAccount_BankID'
		}, {
			fieldLabel: '银行帐号',
			name: 'BDoctorAccount_BankAccount',
			itemId: 'BDoctorAccount_BankAccount'
		}, {
			fieldLabel: '开户行地址',
			name: 'BDoctorAccount_BankAddress',
			itemId: 'BDoctorAccount_BankAddress'
		}, {
			xtype: 'textarea',
			fieldLabel: 'fields',
			hidden: true,
			name: 'fields'
		}, {
			fieldLabel: '次序',
			name: 'BDoctorAccount_DispOrder',
			xtype: 'numberfield',
			value: 0,
			emptyText: '必填项',
			allowBlank: false
		}, {
			fieldLabel: '主键ID',
			name: 'BDoctorAccount_Id',
			hidden: true
		}];

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Name: values.BDoctorAccount_Name,
			HWorkNumberID: values.BDoctorAccount_HWorkNumberID,
			DispOrder: values.BDoctorAccount_DispOrder,
			HospitalCode : values.BDoctorAccount_HospitalCode
		};

		if(values.BDoctorAccount_HospitalName) {
			entity.HospitalName = values.BDoctorAccount_HospitalName;
			entity.HospitalID = values.BDoctorAccount_HospitalID;
			entity.AreaID=values.BDoctorAccount_AreaID;
		}
	
		if(values.BDoctorAccount_HospitalDeptName) {
			entity.HospitalDeptName = values.BDoctorAccount_HospitalDeptName;
			entity.HospitalDeptID = values.BDoctorAccount_HospitalDeptID;
		}
		if(values.BDoctorAccount_ProfessionalAbilityID) {
			entity.ProfessionalAbilityID = values.BDoctorAccount_ProfessionalAbilityID;
			entity.ProfessionalAbilityName = values.BDoctorAccount_ProfessionalAbilityName;
		}
		if(values.BDoctorAccount_BankAccount) {
			entity.BankAccount = values.BDoctorAccount_BankAccount;
		}
		if(values.BDoctorAccount_BankAddress) {
			entity.BankAddress = values.BDoctorAccount_BankAddress;
		}
		if(values.BDoctorAccount_BankID) {
			entity.BankID = values.BDoctorAccount_BankID;
		}
		if(values.BLabTestItem_DoctorAccountType) {
			entity.DoctorAccountType = values.BLabTestItem_DoctorAccountType;
		}
		if(values.BDoctorAccount_BonusPercent || values.BDoctorAccount_BonusPercent === 0){
			entity.BonusPercent = values.BDoctorAccount_BonusPercent;
		}
		return {
			entity: entity
		};DoctorAccountType
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams(),
			fieldsArr = [];
		var fields = ['HWorkNumberID', 'HospitalName', 'HospitalCode','AreaID',
			'BankID', 'BankAccount', 'ProfessionalAbilityName', 'ProfessionalAbilityID',
			'Name', 'DispOrder', 'Id','BonusPercent','BankAddress','HospitalID',
			'DoctorAccountType','HospitalDeptName','HospitalDeptID'
		];
		entity.fields = fields.join(',');
		entity.entity.Id = values.BDoctorAccount_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		//银行种类
		var BDoctorAccount_Bank = me.getComponent('BDoctorAccount_Bank'),
			BDoctorAccount_BankID = me.getComponent('BDoctorAccount_BankID');
			
			if(data.BDoctorAccount_BankID){
		        me.getBankName(data.BDoctorAccount_BankID,function(data){
		        	if(data.value.list){
		        		BDoctorAccount_Bank.setValue(data.value.list[0].BDict_CName);
		        	}
		        });
			}else{
				BDoctorAccount_Bank.setValue('');
			}
		return data;
	},
	/**获取银行种类信息*/
	getBankName:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true';
		url += '&fields=BDict_CName,BDict_Id&where=bdict.Id='+id;
        me.ItemEnum = {};
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;
		//医院
		var BDoctorAccount_HospitalName = me.getComponent('BDoctorAccount_HospitalName'),
			BDoctorAccount_HospitalID = me.getComponent('BDoctorAccount_HospitalID'),
			BDoctorAccount_AreaID=me.getComponent('BDoctorAccount_AreaID'),
			BDoctorAccount_HospitalCode = me.getComponent('BDoctorAccount_HospitalCode');

		if(BDoctorAccount_HospitalName) {
			BDoctorAccount_HospitalName.on({
				check: function(p, record) {
					BDoctorAccount_HospitalName.setValue(record ? record.get('BHospital_Name') : '');
                    BDoctorAccount_HospitalCode.setValue(record ? record.get('BHospital_HospitalCode') : '');
					BDoctorAccount_HospitalID.setValue(record ? record.get('BHospital_Id') : '');					
					BDoctorAccount_AreaID.setValue(record ? record.get('BHospital_AreaID') : '');

					p.close();
				}
			});
		}

		//科室
		var BDoctorAccount_HospitalDeptName = me.getComponent('BDoctorAccount_HospitalDeptName'),
			BDoctorAccount_HospitalDeptID = me.getComponent('BDoctorAccount_HospitalDeptID');

		if(BDoctorAccount_HospitalDeptName) {
			BDoctorAccount_HospitalDeptName.on({
				check: function(p, record) {
					BDoctorAccount_HospitalDeptName.setValue(record ? record.get('BHospitalDept_Name') : '');
					BDoctorAccount_HospitalDeptID.setValue(record ? record.get('BHospitalDept_Id') : '');
					p.close();
				}
			});
		}
		//职称专业级别
		var BDoctorAccount_ProfessionalAbilityName = me.getComponent('BDoctorAccount_ProfessionalAbilityName'),
			BDoctorAccount_ProfessionalAbilityID = me.getComponent('BDoctorAccount_ProfessionalAbilityID');

		if(BDoctorAccount_ProfessionalAbilityName) {
			BDoctorAccount_ProfessionalAbilityName.on({
				check: function(p, record) {
					BDoctorAccount_ProfessionalAbilityName.setValue(record ? record.get('BProfessionalAbility_Name') : '');
					BDoctorAccount_ProfessionalAbilityID.setValue(record ? record.get('BProfessionalAbility_Id') : '');
					p.close();
				}
			});
		}

		//银行种类
		var BDoctorAccount_Bank = me.getComponent('BDoctorAccount_Bank'),
			BDoctorAccount_BankID = me.getComponent('BDoctorAccount_BankID');

		if(BDoctorAccount_Bank) {
			BDoctorAccount_Bank.on({
				check: function(p, record) {
					BDoctorAccount_Bank.setValue(record ? record.get('BDict_CName') : '');
					BDoctorAccount_BankID.setValue(record ? record.get('BDict_Id') : '');
					p.close();
				}
			});
		}

		var ProfessionalAbilityImageUrl = me.getComponent('BDoctorAccount_ProfessionalAbilityImageUrl');
		ProfessionalAbilityImageUrl.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue){
				    var val = newValue.substring(newValue.length, newValue.toString().lastIndexOf("."));
					if(val.toString().toLowerCase() != '.png') {
						JShell.Msg.alert('只能上传png图片文件');
						com.setRawValue(oldValue);
                   }
				}
			}
		});
		var PersonImageUrl = me.getComponent('BDoctorAccount_PersonImageUrl');
		PersonImageUrl.on({
			change: function(com, newValue, oldValue, eOpts) {
				if(newValue){
					var val = newValue.substring(newValue.length, newValue.toString().lastIndexOf("."));
                 	if(val.toString().toLowerCase() != '.png') {
                 		JShell.Msg.alert('只能上传png图片文件');
						com.setRawValue(oldValue);
                 	}
				}
			}
		});
	},
	/**个人照片/职业证书*/
	onSavePersonImage: function(file, Id, ImageType) {
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
	}
});