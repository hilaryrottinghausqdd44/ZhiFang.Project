/**
 * 国家信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.province.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'国家信息',
    width:570,
	height:320,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBProvinceById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBProvince',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBProvinceByField', 
    
	/**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
        labelWidth:60,
        labelAlign:'right'
    },
    /**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	/**启用表单状态初始化*/
	openFormType:true,
	
	/**国家*/
	BCountry:{},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		//拼音字头监听
		me.initPinYinZiTouListeners();
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
			
		//国家
		items.push({
			fieldLabel:'国家',
			emptyText:'必填项',allowBlank:false,
			name:'BProvince_BCountry_Name',
			itemId:'BProvince_BCountry_Name',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.country.CheckGrid'
		},{
			fieldLabel:'国家主键ID',hidden:true,
			name:'BProvince_BCountry_Id',
			itemId:'BProvince_BCountry_Id'
		},{
			fieldLabel:'国家时间戳',hidden:true,
			name:'BProvince_BCountry_DataTimeStamp',
			itemId:'BProvince_BCountry_DataTimeStamp'
		});
			
		items.push(
			{fieldLabel:'省份名称',name:'BProvince_Name',itemId:'BProvince_Name',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'省份简称',name:'BProvince_SName',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'快捷码',name:'BProvince_Shortcode',itemId:'BProvince_Shortcode',
				emptyText:'必填项',allowBlank:false},	
			{fieldLabel:'拼音字头',name:'BProvince_PinYinZiTou',itemId:'BProvince_PinYinZiTou',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'备注',height:85,
				name:'BProvince_Comment',xtype:'textarea'},
			{boxLabel:'是否使用',name:'BProvince_IsUse',
				xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BProvince_Id',hidden:true}
		);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			BCountry:{
				Id:values.BProvince_BCountry_Id,
				DataTimeStamp:values.BProvince_BCountry_DataTimeStamp.split(',')
			},
			Name:values.BProvince_Name,
			SName:values.BProvince_SName,
			Shortcode:values.BProvince_Shortcode,
			PinYinZiTou:values.BProvince_PinYinZiTou,
			IsUse:values.BProvince_IsUse ? true : false,
			Memo:values.BProvince_Memo
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
		entity.fields = fieldsArr.join(',') + ',BCountry_Id';
		
		entity.entity.Id = values.BProvince_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//国家监听
		var dictList = ['BCountry'];
		
		for(var i=0;i<dictList.length;i++){
			me.doCountryListeners(dictList[i]);
		}
	},
	/**国家监听*/
	doCountryListeners:function(name){
		var me = this;
		var Name = me.getComponent('BProvince_' + name + '_Name');
		var Id = me.getComponent('BProvince_' + name + '_Id');
		var DataTimeStamp = me.getComponent('BProvince_' + name + '_DataTimeStamp');
		
		Name.on({
			check: function(p, record) {
				Name.setValue(record ? record.get('BCountry_Name') : '');
				Id.setValue(record ? record.get('BCountry_Id') : '');
				DataTimeStamp.setValue(record ? record.get('BCountry_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	/**拼音字头监听*/
	initPinYinZiTouListeners:function(){
		var me = this,
			BProvince_Name = me.getComponent('BProvince_Name');
			
		BProvince_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				me.changePinYinZiTou(newValue);
			}
		});
	},
	changePinYinZiTou:function(data){
		var me = this,
			BProvince_PinYinZiTou = me.getComponent('BProvince_PinYinZiTou'),
			BProvince_Shortcode = me.getComponent('BProvince_Shortcode');
			
		if(data != ""){
			JShell.Action.delay(function(){
				JShell.System.getPinYinZiTou(data,function(value){
					me.getForm().setValues({
						BProvince_PinYinZiTou:value,
						BProvince_Shortcode:value
					});
				});
			},null,200);
		}else{
			me.getForm().setValues({
				BProvince_PinYinZiTou:"",
				BProvince_Shortcode:""
			});
		}
	},
	/**新增*/
	isAdd:function(){
		var me = this;
		me.callParent(arguments);
		
		var Name = me.getComponent('BProvince_BCountry_Name'),
			Id = me.getComponent('BProvince_BCountry_Id'),
			DataTimeStamp = me.getComponent('BProvince_BCountry_DataTimeStamp');
		
		Name.setValue(me.BCountry.Name);
		Id.setValue(me.BCountry.Id);
		DataTimeStamp.setValue(me.BCountry.DataTimeStamp);
	}
});