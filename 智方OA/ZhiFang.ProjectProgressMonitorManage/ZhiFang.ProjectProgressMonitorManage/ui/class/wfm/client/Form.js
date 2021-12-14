/**
 * 客户信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.client.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'客户信息',
    width:680,
	height:420,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPClient',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePClientByField', 
    
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**启用表单状态初始化*/
	//openFormType:true,
	/**每个组件的默认属性*/
//	defaults: {
//		labelWidth: 65,
////		width: 250,
//		labelAlign: 'right'
//	},
    PClientSourceList:[],
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//初始化监听
		me.initFilterListeners();
		
		//国家省份城市监听
		if(me.formtype == 'add'){
			me.doCountryProvinceCityListeners();
		}else{
			me.on({
				load:function(){
					if(!me.hasLoaded){
						me.doCountryProvinceCityListeners();
					}
					me.hasLoaded = true;
				}
			});
		}
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		items.push({
			x:10,y:10,itemId:'PClient_Name',
			fieldLabel:'名称',name:'PClient_Name',
			emptyText:'必填项',allowBlank:false
		},{
			x:10,y:35,emptyText:'必填项',allowBlank:false,
			fieldLabel:'授权名称',name:'PClient_LicenceClientName',itemId:'PClient_LicenceClientName'
		},{
			x:10,y:60,
			fieldLabel:'快捷码',name:'PClient_Shortcode'
		},{
			x:10,y:85,
			fieldLabel:'拼音字头',name:'PClient_PinYinZiTou'
		});
		
		items.push({
			x:220,y:10,
			fieldLabel:'负责人',name:'PClient_Principal'
		},{
			x:220,y:35,
			fieldLabel:'联系人',name:'PClient_LinkMan'
		},{
			x:220,y:60,
			fieldLabel:'联系电话',name:'PClient_PhoneNum'
		},{
			x:220,y:85,
			fieldLabel:'联系电话2',name:'PClient_PhoneNum2'
		});
		
		items.push({
			x:430,y:10,
			fieldLabel:'业务员',name:'PClient_Bman'
		},{
			x:430,y:35,
			fieldLabel:'地址',name:'PClient_Address'
		},{
			x:430,y:60,
			fieldLabel:'邮编',name:'PClient_MailNo'
		},{
			x:430,y:85,
			fieldLabel:'电子邮件',name:'PClient_Emall'
		});
		
		//国家
		items.push({
			x:10,y:110,
			fieldLabel:'国家',
			name:'PClient_CountryName',itemId:'PClient_CountryName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.CheckGrid'
		},{
			fieldLabel:'国家主键ID',hidden:true,
			name:'PClient_CountryID',itemId:'PClient_CountryID'
		});
		//省份
		items.push({
			x:10,y:135,
			fieldLabel:'省份',
			name:'PClient_ProvinceName',itemId:'PClient_ProvinceName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.province.CheckGrid'
		},{
			fieldLabel:'省份主键ID',hidden:true,
			name:'PClient_ProvinceID',itemId:'PClient_ProvinceID'
		});
		//城市
		items.push({
			x:10,y:160,
			fieldLabel:'城市',
			name:'PClient_CityName',itemId:'PClient_CityName'
		},{
			fieldLabel:'城市主键ID',hidden:true,
			name:'PClient_CityID',itemId:'PClient_CityID'
		});
		
		//地理区域
		items.push({
			x:220,y:110,
			fieldLabel:'地理区域',
			name:'PClient_ClientAreaName',itemId:'PClient_ClientAreaName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'地理区域选择',
				defaultWhere:"pdict.PDictType.Id='" + JShell.WFM.GUID.DictType.ClientArea.value + "'"
			}
		},{
			fieldLabel:'区域主键ID',hidden:true,
			name:'PClient_ClientAreaID',itemId:'PClient_ClientAreaID'
		});
		//客户类型
		items.push({
			x:220,y:135,
			fieldLabel:'客户类型',
			name:'PClient_ClientTypeName',itemId:'PClient_ClientTypeName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'客户类型选择',
				defaultWhere:"pdict.PDictType.Id='" + JShell.WFM.GUID.DictType.ClientType.value + "'"
			}
		},{
			fieldLabel:'客户类型主键ID',hidden:true,
			name:'PClient_ClientTypeID',itemId:'PClient_ClientTypeID'
		});
		
		//医院类别
		items.push({
			x:430,y:110,
			fieldLabel:'医院类别',
			name:'PClient_HospitalTypeName',itemId:'PClient_HospitalTypeName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'医院类别选择',
				defaultWhere:"pdict.PDictType.Id='" + JShell.WFM.GUID.DictType.HospitalType.value + "'"
			}
		},{
			fieldLabel:'医院类别主键ID',hidden:true,
			name:'PClient_HospitalTypeID',itemId:'PClient_HospitalTypeID'
		});
		//医院等级
		items.push({
			x:430,y:135,
			fieldLabel:'医院等级',
			name:'PClient_HospitalLevelName',itemId:'PClient_HospitalLevelName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'医院等级选择',
				defaultWhere:"pdict.PDictType.Id='" + JShell.WFM.GUID.DictType.HospitalLevel.value + "'"
			}
		},{
			fieldLabel:'医院等级主键ID',hidden:true,
			name:'PClient_HospitalLevelID',itemId:'PClient_HospitalLevelID'
		});

		items.push({
			x:220,y:160,
			fieldLabel:'简称',name:'PClient_SName'
		},{
			x:495,y:160,width:100,hidden:false,
			boxLabel:'合约用户',name:'PClient_IsContract',
			xtype:'checkbox',checked:true,itemId:'PClient_IsContract'
		});
		
		
		//授权编码,主服务器授权申请号,备份服务器授权申请号
		items.push({
			x:10,y:185,labelWidth: 85,
			fieldLabel:'客户服务编号',//locked : true,readOnly:true,
			regex:/^[0-9]+$/,//匹配正则表达式
            regexText:"只能输入数字",emptyText:'新增自增,无须填写',
			name:'PClient_LicenceCode',itemId:'PClient_LicenceCode'
		},{
			x:220,y:185,
			fieldLabel:'主服务器授权号',labelWidth: 100,
			name:'PClient_LRNo1',itemId:'PClient_LRNo1',width:220,
			style: {
				marginLeft: '-20px'
			}
		},{
			x:430,y:185,
			fieldLabel:'备份服务器授权号',labelWidth: 110,width:220,
			name:'PClient_LRNo2',itemId:'PClient_LRNo2',
			style: {
				marginLeft: '-20px'
			}
		});
	

		items.push({
			x:10,y:237,locked : true,readOnly:true,hidden:false,
			fieldLabel:'客户编码',name:'PClient_ClientNo',itemId:'PClient_ClientNo'
		},{
			x:225,y:237,width:80,hidden:false,
			boxLabel:'重复标记',name:'PClient_IsRepeat',
			xtype:'checkbox',checked:false,itemId:'PClient_IsRepeat'
		},{
			x:320,y:237,width:80,hidden:false,
			boxLabel:'使用',name:'PClient_IsUse',
			xtype:'checkbox',checked:true,itemId:'PClient_IsUse'
		},{
			fieldLabel:'主键ID',name:'PClient_Id',
			hidden:true
		});
		items.push({
			fieldLabel: '客户来源',
			emptyText: '必填项',
			name: 'PClient_ProjectSourceName',
			itemId: 'PClient_ProjectSourceName',
			xtype: 'uxSimpleComboBox',
			hasStyle: true,
			data: me.PClientSourceList,
			x:10,y:211
		},{
			labelWidth: 75,x:225,y:211,width:415,fieldLabel:'代理商名称',name:'PClient_AgentName',
			style: {
				marginLeft: '-10px'
			}
		});
		if(!me.PK){
			items.push({
				x:10,y:237,width:620,
			   fieldLabel:'网址',name:'PClient_Url'
			});
		}else{
			items.push({x:365,y:237,width:265,
			fieldLabel:'网址',name:'PClient_Url'
			});
		}
	
		items.push({
			x:10,y:263,width:620,itemId:'PClient_Comment',
			fieldLabel:'备注',name:'PClient_Comment',
			xtype:'textarea',height:85
		});
		return items;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			me.ShowInfo(false);
		    //客户服务编号
			var LicenceCode = me.getComponent('PClient_LicenceCode');
            LicenceCode.setReadOnly(true);
		} else {
			me.getForm().setValues(me.lastData);
			me.ShowInfo(true);
			
		}
	},
	/**显示*/
	ShowInfo:function(bo){
		var me=this;
		//用户编号
		var ClientNo = me.getComponent('PClient_ClientNo');
		//是否重复
		var IsRepeat = me.getComponent('PClient_IsRepeat');
		//使用
		var IsUse = me.getComponent('PClient_IsUse');
		if(bo === false) {
			ClientNo.hide();
			IsRepeat.hide();
			IsUse.hide();
		} else {
			ClientNo.show();
			IsRepeat.show();
			IsUse.show();
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.PClient_Name,
			SName:values.PClient_SName,
			Shortcode:values.PClient_Shortcode,
			PinYinZiTou:values.PClient_PinYinZiTou,
			Principal:values.PClient_Principal,
			LinkMan:values.PClient_LinkMan,
			PhoneNum:values.PClient_PhoneNum,
			PhoneNum2:values.PClient_PhoneNum2,
			Bman:values.PClient_Bman,
			Address:values.PClient_Address,
			MailNo:values.PClient_MailNo,
			Emall:values.PClient_Emall,
			Url:values.PClient_Url,
			IsUse:values.PClient_IsUse ? true : false,
			IsContract:values.PClient_IsContract ? true : false,
			Comment:values.PClient_Comment,
			LicenceCode:values.PClient_LicenceCode,
			LRNo1:values.PClient_LRNo1,
			LRNo2:values.PClient_LRNo2,
			IsRepeat:values.PClient_IsRepeat ? true : false,
			LicenceClientName:values.PClient_LicenceClientName,
			AgentName:values.PClient_AgentName
//			ClientNo:values.PClient_ClientNo
		};
		var ProjectSourceName = me.getComponent('PClient_ProjectSourceName');
		//客户来源
		if(values.PClient_ProjectSourceName){
			entity.ProjectSourceName = ProjectSourceName.getRawValue();
		}
		//国家
		if(values.PClient_CountryName){
			entity.CountryID = values.PClient_CountryID;
			entity.CountryName = values.PClient_CountryName;
		}
		//省份
		if(values.PClient_ProvinceName){
			entity.ProvinceID = values.PClient_ProvinceID;
			entity.ProvinceName = values.PClient_ProvinceName;
		}
		//城市
		if(values.PClient_CityName){
//			entity.CityID = values.PClient_CityID;
			entity.CityName = values.PClient_CityName;
		}
		//地理区域
		if(values.PClient_ClientAreaID){
			entity.ClientAreaID = values.PClient_ClientAreaID;
		}
		if(values.PClient_ClientAreaName){
			entity.ClientAreaName = values.PClient_ClientAreaName;
		}
		//客户类型
		if(values.PClient_ClientTypeName){
			entity.ClientTypeID = values.PClient_ClientTypeID;
			entity.ClientTypeName = values.PClient_ClientTypeName;
		}
		
		//医院类别
		if(values.PClient_HospitalTypeName){
			entity.HospitalTypeID = values.PClient_HospitalTypeID;
			entity.HospitalTypeName = values.PClient_HospitalTypeName;
		}
		//医院等级
		if(values.PClient_HospitalLevelName){
			entity.HospitalLevelID = values.PClient_HospitalLevelID;
			entity.HospitalLevelName = values.PClient_HospitalLevelName;
		}
		
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];
		
		for(var i in fields){
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			if(fields[i]!='PClient_ClientNo'){
				fieldsArr.push(arr[1]);
			}
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.PClient_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		var me = this;
		//客户服务编号
		var LicenceCode = me.getComponent('PClient_LicenceCode');
        LicenceCode.emptyText='必填项';
        LicenceCode.allowBlank=false;
        LicenceCode.setReadOnly(false);
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//字典监听
		var dictList = [
			'ClientArea','ClientType','HospitalType','HospitalLevel'
		];
		for(var i=0;i<dictList.length;i++){
			me.doDictListeners(dictList[i]);
		}
		//国家监听
		me.doCountryListeners();
		//省份监听
		me.doProvinceListeners();
		//城市监听
//		me.doCityListeners();
		//名称变化监听
		me.doNameListeners();
	},

	/**字典监听*/
	doDictListeners:function(name){
		var me = this;
		var CName = me.getComponent('PClient_' + name + 'Name');
		var Id = me.getComponent('PClient_' + name + 'ID');
		
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				p.close();
			}
		});
	},
	/**国家监听*/
	doCountryListeners:function(){
		var me = this,
			CName = me.getComponent('PClient_CountryName'),
			Id = me.getComponent('PClient_CountryID');
			
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BCountry_Name') : '');
				Id.setValue(record ? record.get('BCountry_Id') : '');
				p.close();
			},
			beforetriggerclick:function(field){
				//bprovince.BCountry.Id去掉查询条件  @author liangyl	 @version 2020-03-11  
				CName.changeClassConfig({
					defaultWhere:'bcountry.IsUse=1'
				});
			}
		});
	},
	/**省份监听*/
	doProvinceListeners:function(){
		var me = this,
			CName = me.getComponent('PClient_ProvinceName'),
			Id = me.getComponent('PClient_ProvinceID');
			
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BProvince_Name') : '');
				Id.setValue(record ? record.get('BProvince_Id') : '');
				p.close();
			},
			beforetriggerclick:function(field){
				var CountryID = me.getComponent('PClient_CountryID').getValue();
				var defaultWhere = CountryID ? 'bprovince.BCountry.Id=' + CountryID : '';
				CName.changeClassConfig({
					defaultWhere:defaultWhere
				});
			}
		});
	},
	/**城市监听*/
	doCityListeners:function(){
		var me = this,
			CName = me.getComponent('PClient_CityName'),
			Id = me.getComponent('PClient_CityID');
			
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BCity_Name') : '');
				Id.setValue(record ? record.get('BCity_Id') : '');
				p.close();
			},
			beforetriggerclick:function(field){
				var ProvinceID = me.getComponent('PClient_ProvinceID').getValue();
				var CountryID = me.getComponent('PClient_CountryID').getValue();
				
				if(ProvinceID){
					CName.classConfig.defaultWhere = 'bcity.BProvince.Id=' + ProvinceID;
				}else if(CountryID){
					CName.classConfig.defaultWhere = 'bcity.BProvince.BCountry.Id=' + CountryID;
				}else{
					CName.classConfig.defaultWhere = '';
				}
			},
			beforetriggerclick:function(field){
				var ProvinceID = me.getComponent('PClient_ProvinceID').getValue(),
					CountryID = me.getComponent('PClient_CountryID').getValue(),
					defaultWhere = '';
					
				if(ProvinceID){
					defaultWhere = 'bcity.BProvince.Id=' + ProvinceID;
				}else if(CountryID){
					defaultWhere = 'bcity.BProvince.BCountry.Id=' + CountryID;
				}else{
					defaultWhere = '';
				}
				CName.changeClassConfig({
					defaultWhere:defaultWhere
				});
				
				
			}
		});
	},
	//名称变化监听
	doNameListeners:function(){
		var me = this,
			Name = me.getComponent('PClient_Name');
			
		if(!Name) return;

		Name.on({
			change:function(com,  newValue,  oldValue,  eOpts){
				setTimeout(function(){
					me.onNameChange();
				},100);
				if(me.formtype=='add'){
	                var LicenceClientName=me.getComponent('PClient_LicenceClientName');
                    LicenceClientName.setValue(newValue);
				}
			}
		});
	},
	onNameChange:function(){
		var me = this,
			Name = me.getComponent('PClient_Name');
			
		if(!Name) return;
		
		var value = Name.getValue();
		if(value != ""){
			JShell.Action.delay(function(){
				JShell.System.getPinYinZiTou(value,function(value){
					me.getForm().setValues({
						PClient_Shortcode:value,
						PClient_PinYinZiTou:value
					});
				});
			},null,200);
		}else{
			me.getForm().setValues({
				PClient_Shortcode:"",
				PClient_PinYinZiTou:""
			});
		}
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	doCountryProvinceCityListeners:function(){
		var me = this,
			PClient_CountryID = me.getComponent('PClient_CountryID'),
			PClient_CountryName = me.getComponent('PClient_CountryName'),
			PClient_ProvinceID = me.getComponent('PClient_ProvinceID'),
			PClient_ProvinceName = me.getComponent('PClient_ProvinceName');
		
		if(PClient_CountryID){
			PClient_CountryID.on({
				change:function(){
					setTimeout(function(){
						me.getForm().setValues({
							PClient_ProvinceID:'',
							PClient_ProvinceName:'',
							PClient_CityID:'',
							PClient_CityName:''
						});
					},100);
				}
			});
		}
		if(PClient_ProvinceID){
			PClient_ProvinceID.on({
				change:function(){
					setTimeout(function(){
						me.getForm().setValues({
							PClient_CityID:'',
							PClient_CityName:''
						});
					},100);
				}
			});
		}
	}
});