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
    width:670,
	height:400,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_SearchSServiceClientById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_AddSServiceClient',
    /**修改服务地址*/
    editUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateSServiceClientByField', 
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	bodyPadding:'20px 20px 10px 20px',
	
    layout:{
        type:'table',
        columns:3//每行有几列
    },
    /**每个组件的默认属性*/
    defaults:{
        labelWidth:60,
        width:200,
        labelAlign:'right'
    },
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//初始化组件监听
		me.initFilterListeners();
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		//创建可见组件
		me.createShowItems();
		//创建隐形组件
		items = items.concat(me.createHideItems());
		
		//获取列表布局组件内容
		items = items.concat(me.getTableLayoutItems());
		
		return items;
	},
	/**创建可见组件*/
	createShowItems:function(){
		var me = this;
		
		//客户级别
		me.SServiceClient_SServiceClientlevel_Name = {
			fieldLabel:'客户级别',name:'SServiceClient_SServiceClientlevel_Name',
			itemId:'SServiceClient_SServiceClientlevel_Name',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.serviceclient.level.CheckGrid'
		};
		//国家
		me.SServiceClient_CountryName = {
			fieldLabel:'国家',name:'SServiceClient_CountryName',
			itemId:'SServiceClient_CountryName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.CheckGrid'
		};
		//省份
		me.SServiceClient_ProvinceName = {
			fieldLabel:'省份',name:'SServiceClient_ProvinceName',
			itemId:'SServiceClient_ProvinceName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.province.CheckGrid'
		};
		//城市
		me.SServiceClient_CityName = {
			fieldLabel:'城市',name:'SServiceClient_CityName',
			itemId:'SServiceClient_CityName',
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.country.province.city.CheckGrid'
		};
		
		//客户名称
		me.SServiceClient_Name = {
			fieldLabel:'客户名称',name:'SServiceClient_Name',
			itemId:'SServiceClient_Name',emptyText:'必填项',allowBlank:false
		};
		//客户简称
		me.SServiceClient_SName = {fieldLabel:'客户简称',name:'SServiceClient_SName'};
		//拼音字头
		me.SServiceClient_PinYinZiTou = {fieldLabel:'拼音字头',name:'SServiceClient_PinYinZiTou'};
		//快捷码
		me.SServiceClient_Shortcode = {fieldLabel:'快捷码',name:'SServiceClient_Shortcode'};
		//描述
		me.SServiceClient_Comment = {fieldLabel:'描述',height:85,name:'SServiceClient_Comment',xtype:'textarea'};
		//是否使用
		me.SServiceClient_IsUse = {
			boxLabel:'是否使用',name:'SServiceClient_IsUse',
			xtype:'checkbox',checked:true
		};
		//显示次序
		me.SServiceClient_DispOrder = {
			fieldLabel:'显示次序',name:'SServiceClient_DispOrder',
			xtype:'numberfield',value:0,allowBlank:false
		};
		//负责人
		me.SServiceClient_Principal = {fieldLabel:'负责人',name:'SServiceClient_Principal'};
		//联系人
		me.SServiceClient_LinkMan = {fieldLabel:'联系人',name:'SServiceClient_LinkMan'};
		//联系电话1
		me.SServiceClient_PhoneNum = {fieldLabel:'联系电话1',name:'SServiceClient_PhoneNum'};
		//联系电话2
		me.SServiceClient_PhoneNum2 = {fieldLabel:'联系电话2',name:'SServiceClient_PhoneNum2'};
		//地址
		me.SServiceClient_Address = {fieldLabel:'地址',name:'SServiceClient_Address'};
		//邮编
		me.SServiceClient_MailNo = {fieldLabel:'邮编',name:'SServiceClient_MailNo'};
		//电子邮件
		me.SServiceClient_Emall = {fieldLabel:'电子邮件',name:'SServiceClient_Emall'};
		//业务员编码
		me.SServiceClient_Bman = {fieldLabel:'业务员编码',name:'SServiceClient_Bman'};
	},
	/**创建隐形组件*/
	createHideItems:function(){
		var me = this,
			items = [];
			
		items.push({fieldLabel:'客户主键ID',name:'SServiceClient_Id',hidden:true});
		items.push({
			fieldLabel:'客户级别主键ID',hidden:true,name:'SServiceClient_SServiceClientlevel_Id',
			itemId:'SServiceClient_SServiceClientlevel_Id'
		});
		items.push({
			fieldLabel:'国家主键ID',hidden:true,name:'SServiceClient_CountryID',
			itemId:'SServiceClient_CountryID'
		});
		items.push({
			fieldLabel:'省份主键ID',hidden:true,name:'SServiceClient_ProvinceID',
			itemId:'SServiceClient_ProvinceID'
		});
		items.push({
			fieldLabel:'城市主键ID',hidden:true,name:'SServiceClient_CityID',
			itemId:'SServiceClient_CityID'
		});
		
		return items;
	},
	/**获取列表布局组件内容*/
	getTableLayoutItems:function(){
		var me = this,
			items = [];
			
		//客户名称
		me.SServiceClient_Name.colspan = 2;
		items.push(me.SServiceClient_Name);
		//客户级别
		me.SServiceClient_SServiceClientlevel_Name.colspan = 1;
		items.push(me.SServiceClient_SServiceClientlevel_Name);
		
		//客户简称
		me.SServiceClient_SName.colspan = 1;
		items.push(me.SServiceClient_SName);
		//国家
		me.SServiceClient_CountryName.colspan = 1;
		items.push(me.SServiceClient_CountryName);
		//显示次序
		me.SServiceClient_DispOrder.colspan = 1;
		items.push(me.SServiceClient_DispOrder);
		
		//拼音字头
		me.SServiceClient_PinYinZiTou.colspan = 1;
		items.push(me.SServiceClient_PinYinZiTou);
		//省份
		me.SServiceClient_ProvinceName.colspan = 1;
		items.push(me.SServiceClient_ProvinceName);
		//负责人
		me.SServiceClient_Principal.colspan = 1;
		items.push(me.SServiceClient_Principal);
		
		//快捷码
		me.SServiceClient_Shortcode.colspan = 1;
		items.push(me.SServiceClient_Shortcode);
		//城市
		me.SServiceClient_CityName.colspan = 1;
		items.push(me.SServiceClient_CityName);
		//是否使用
		me.SServiceClient_IsUse.style = {marginLeft:'30px'};
		me.SServiceClient_IsUse.colspan = 1;
		items.push(me.SServiceClient_IsUse);
		
		//联系人
		me.SServiceClient_LinkMan.colspan = 1;
		items.push(me.SServiceClient_LinkMan);
		//联系电话1
		me.SServiceClient_PhoneNum.colspan = 1;
		items.push(me.SServiceClient_PhoneNum);
		//联系电话2
		me.SServiceClient_PhoneNum2.colspan = 1;
		items.push(me.SServiceClient_PhoneNum2);
		
		//邮编
		me.SServiceClient_MailNo.colspan = 1;
		items.push(me.SServiceClient_MailNo);
		//电子邮件
		me.SServiceClient_Emall.colspan = 2;
		items.push(me.SServiceClient_Emall);
		
		//地址
		me.SServiceClient_Address.colspan = 3;
		items.push(me.SServiceClient_Address);
		//描述
		me.SServiceClient_Comment.colspan = 3;
		items.push(me.SServiceClient_Comment);
		
		for(var i in items){
			items[i].width = me.defaults.width * items[i].colspan;
		}
			
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.SServiceClient_Name,
			PinYinZiTou:values.SServiceClient_PinYinZiTou,
			Shortcode:values.SServiceClient_Shortcode,
			SName:values.SServiceClient_SName,
			DispOrder:values.SServiceClient_DispOrder,
			IsUse:values.SServiceClient_IsUse ? true : false,
			Comment:values.SServiceClient_Comment,
			
			Principal:values.SServiceClient_Principal,
			LinkMan:values.SServiceClient_LinkMan,
			PhoneNum:values.SServiceClient_PhoneNum,
			PhoneNum2:values.SServiceClient_PhoneNum2,
			MailNo:values.SServiceClient_MailNo,
			Emall:values.SServiceClient_Emall,
			Address:values.SServiceClient_Address
		};
		//客户级别
		if(values.SServiceClient_SServiceClientlevel_Id){
			entity.SServiceClientlevel = {
				Id:values.SServiceClient_SServiceClientlevel_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		//国家
		if(values.SServiceClient_CountryID){
			entity.CountryID = values.SServiceClient_CountryID;
			entity.CountryName = values.SServiceClient_CountryName;
		}
		//省份
		if(values.SServiceClient_ProvinceID){
			entity.ProvinceID = values.SServiceClient_ProvinceID;
			entity.ProvinceName = values.SServiceClient_ProvinceName;
		}
		//城市
		if(values.SServiceClient_CityID){
			entity.CityID = values.SServiceClient_CityID;
			entity.CityName = values.SServiceClient_CityName;
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			'Name','PinYinZiTou','Shortcode','SName','DispOrder','IsUse','Comment',
			'CountryID','CountryName','ProvinceID','ProvinceName','CityID','CityName',
			'Principal','LinkMan','PhoneNum','PhoneNum2','MailNo','Emall','Address',
			'SServiceClientlevel_Id','Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.SServiceClient_Id;
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