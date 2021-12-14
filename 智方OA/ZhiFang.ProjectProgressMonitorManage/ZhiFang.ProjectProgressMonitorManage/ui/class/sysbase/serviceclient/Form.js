/**
 * 平台客户表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.serviceclient.Form',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    
    title:'平台客户表单',
    width:240,
	height:400,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/WeiXinAppService.svc/ST_UDTO_SearchSServiceClientById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/WeiXinAppService.svc/ST_UDTO_AddSServiceClient',
    /**修改服务地址*/
    editUrl:'/WeiXinAppService.svc/ST_UDTO_UpdateSServiceClientByField', 
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//初始化组件监听
		me.initFilterListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		//客户主键ID
		items.push({fieldLabel:'客户主键ID',name:'SServiceClient_Id',hidden:true});
		//显示次序
		items.push({
			fieldLabel:'显示次序',name:'SServiceClient_DispOrder',
			xtype:'numberfield',value:0,allowBlank:false
		});
		//客户类型
		items.push({
			fieldLabel:'客户类型',name:'SServiceClient_ClientTypeName',
			itemId:'SServiceClient_ClientTypeName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.serviceclient.level.CheckGrid'
		},{
			fieldLabel:'客户类型主键ID',hidden:true,name:'SServiceClient_ClientTypeID',
			itemId:'SServiceClient_ClientTypeID'
		});
		//客户级别
		items.push({
			fieldLabel:'客户级别',name:'SServiceClient_SServiceClientlevel_Name',
			itemId:'SServiceClient_SServiceClientlevel_Name',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.serviceclient.level.CheckGrid'
		},{
			fieldLabel:'客户级别主键ID',hidden:true,name:'SServiceClient_SServiceClientlevel_Id',
			itemId:'SServiceClient_SServiceClientlevel_Id'
		});
		//客户名称
		items.push({
			fieldLabel:'客户名称',name:'SServiceClient_Name',
			itemId:'SServiceClient_Name',emptyText:'必填项',allowBlank:false
		});
		//客户简称
		items.push({fieldLabel:'客户简称',name:'SServiceClient_SName'});
		//拼音字头
		items.push({fieldLabel:'拼音字头',name:'SServiceClient_PinYinZiTou'});
		//快捷码
		items.push({fieldLabel:'快捷码',name:'SServiceClient_Shortcode'});
		//描述
		items.push({
			fieldLabel:'描述',height:85,labelAlign:'top',
			name:'SServiceClient_Comment',xtype:'textarea'
		});
		//是否使用
		items.push({
			boxLabel:'是否使用',name:'SServiceClient_IsUse',
			xtype:'checkbox',checked:true
		});
		//主键ID
		items.push({fieldLabel:'主键ID',name:'SServiceClient_Id',hidden:true});
		
		//国家
		items.push({
			fieldLabel:'国家',name:'SServiceClient_CountryName',
			itemId:'SServiceClient_CountryName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.CheckGrid'
		},{
			fieldLabel:'国家主键ID',hidden:true,name:'SServiceClient_CountryID',
			itemId:'SServiceClient_CountryID'
		});
		//省份
		items.push({
			fieldLabel:'省份',name:'SServiceClient_ProvinceName',
			itemId:'SServiceClient_ProvinceName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.province.CheckGrid'
		},{
			fieldLabel:'省份主键ID',hidden:true,name:'SServiceClient_ProvinceID',
			itemId:'SServiceClient_ProvinceID'
		});
		//城市
		items.push({
			fieldLabel:'城市',name:'SServiceClient_CityName',
			itemId:'SServiceClient_CityName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.province.city.CheckGrid'
		},{
			fieldLabel:'城市主键ID',hidden:true,name:'SServiceClient_CityID',
			itemId:'SServiceClient_CityID'
		});
		
		//负责人
		items.push({fieldLabel:'负责人',name:'SServiceClient_Principal'});
		//联系人
		items.push({fieldLabel:'联系人',name:'SServiceClient_LinkMan'});
		//联系电话1
		items.push({fieldLabel:'联系电话1',name:'SServiceClient_PhoneNum'});
		//联系电话2
		items.push({fieldLabel:'联系电话2',name:'SServiceClient_PhoneNum2'});
		//地址
		items.push({fieldLabel:'地址',name:'SServiceClient_Address'});
		//邮编
		items.push({fieldLabel:'邮编',name:'SServiceClient_MailNo'});
		//电子邮件
		items.push({fieldLabel:'电子邮件',name:'SServiceClient_Emall'});
		//业务员编码
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		items.push({fieldLabel:'业务员编码',name:'SServiceClient_Bman'});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.SServiceClient_Name,
			EName:values.SServiceClient_EName,
			PinYinZiTou:values.SServiceClient_PinYinZiTou,
			
			SName:values.SServiceClient_SName,
			DispOrder:values.SServiceClient_DispOrder,
			
			UseCode:values.SServiceClient_UseCode,
			StandCode:values.SServiceClient_StandCode,
			DeveCode:values.SServiceClient_DeveCode,
			
			Shortcode:values.SServiceClient_Shortcode,
			IsUse:values.SServiceClient_IsUse ? true : false,
			
			Comment:values.SServiceClient_Comment,
			
			LabID:values.SServiceClient_LabID
		};
		
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
			fieldsArr.push(arr[1]);
		}
		entity.fields = fieldsArr.join(',');
		
		entity.entity.Id = values.SServiceClient_Id;
		entity.entity.DataTimeStamp = values.SServiceClient_DataTimeStamp.split(',')
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化组件监听*/
	initFilterListeners:function(){
		var me = this;
		
		//客户名称->简称+拼音字头
		var SServiceClient_Name = me.getComponent('SServiceClient_Name');
		SServiceClient_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				if(newValue != ""){
					JShell.Action.delay(function(){
						JShell.System.getPinYinZiTou(newValue,function(value){
							me.getForm().setValues({
								SServiceClient_PinYinZiTou:value,
								SServiceClient_Shortcode:value
							});
						});
					},null,200);
				}else{
					me.getForm().setValues({
						SServiceClient_PinYinZiTou:"",
						SServiceClient_Shortcode:""
					});
				}
			}
		});
		
		//平台客户级别
		var SServiceClientlevelName = me.getComponent('SServiceClient_SServiceClientlevel_Name'),
			SServiceClientlevelId = me.getComponent('SServiceClient_SServiceClientlevel_Id');
		
		SServiceClientlevelName.on({
			check:function(p,record){
				SServiceClientlevelName.setValue(record ? record.get('SServiceClientlevel_Name') : '');
				SServiceClientlevelId.setValue(record ? record.get('SServiceClientlevel_Id') : '');
				p.close();
			}
		});
		
		//国家+省份+城市
		var CountryName = me.getComponent('SServiceClient_CountryName'),
			CountryID = me.getComponent('SServiceClient_CountryID'),
			ProvinceName = me.getComponent('SServiceClient_ProvinceName'),
			ProvinceID = me.getComponent('SServiceClient_ProvinceID'),
			CityName = me.getComponent('SServiceClient_CityName');
			CityID = me.getComponent('SServiceClient_CityID');
			
		CountryName.on({
			check:function(p,record){
				CountryName.setValue(record ? record.get('BCountry_Name') : '');
				CountryID.setValue(record ? record.get('BCountry_Id') : '');
				p.close();
			},
			change:function(){
				ProvinceName.setValue('');
				ProvinceID.setValue('');
				CityName.setValue('');
				CityID.setValue('');
			}
		});
		ProvinceName.on({
			check:function(p,record){
				ProvinceName.setValue(record ? record.get('BProvince_Name') : '');
				ProvinceID.setValue(record ? record.get('BProvince_Id') : '');
				p.close();
			},
			change:function(){
				CityName.setValue('');
				CityID.setValue('');
			},
			beforetriggerclick:function(p){
				if(!CountryName.getValue()){
					JShell.Msg.error('请先选择国家！');
					return false;
				}else{
					p.setClassConfig({
						defaultWhere:"bprovince.BCountry.Id=" + CountryID.getValue()
					});
				}
			}
		});
		CityName.on({
			check:function(p,record){
				CityName.setValue(record ? record.get('BCity_Name') : '');
				CityID.setValue(record ? record.get('BCity_Id') : '');
				p.close();
			},
			beforetriggerclick:function(p){
				if(!ProvinceName.getValue()){
					JShell.Msg.error('请先选择省份！');
					return false;
				}else{
					p.setClassConfig({
						defaultWhere:"bcity.BProvince.Id=" + ProvinceID.getValue()
					});
				}
			}
		});
	}
});