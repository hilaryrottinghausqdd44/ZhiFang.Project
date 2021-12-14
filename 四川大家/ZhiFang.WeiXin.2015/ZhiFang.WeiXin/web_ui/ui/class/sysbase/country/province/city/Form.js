/**
 * 城市信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.country.province.city.Form',{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'城市信息',
    width:570,
	height:320,
	bodyPadding:10,
    
    /**获取数据服务路径*/
    selectUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBCityById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBCity',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBCityByField', 
    
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
	
	/**省份*/
	BProvince:{},
	
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
			
		//省份
		items.push({
			fieldLabel:'省份',
			emptyText:'必填项',allowBlank:false,
			name:'BCity_BProvince_Name',
			itemId:'BCity_BProvince_Name',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.country.province.CheckApp'
		},{
			fieldLabel:'省份主键ID',hidden:true,
			name:'BCity_BProvince_Id',
			itemId:'BCity_BProvince_Id'
		},{
			fieldLabel:'省份时间戳',hidden:true,
			name:'BCity_BProvince_DataTimeStamp',
			itemId:'BCity_BProvince_DataTimeStamp'
		});
		
		items.push(
			{fieldLabel:'城市名称',name:'BCity_Name',itemId:'BCity_Name',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'城市简称',name:'BCity_SName',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'快捷码',name:'BCity_Shortcode',itemId:'BCity_Shortcode',
				emptyText:'必填项',allowBlank:false},	
			{fieldLabel:'拼音字头',name:'BCity_PinYinZiTou',itemId:'BCity_PinYinZiTou',
				emptyText:'必填项',allowBlank:false},
			{fieldLabel:'备注',height:85,
				name:'BCity_Comment',xtype:'textarea'},
			{boxLabel:'是否使用',name:'BCity_IsUse',
				xtype:'checkbox',checked:true},
			{fieldLabel:'主键ID',name:'BCity_Id',hidden:true}
		);
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			BProvince:{
				Id:values.BCity_BProvince_Id,
				DataTimeStamp:values.BCity_BProvince_DataTimeStamp.split(',')
			},
			Name:values.BCity_Name,
			SName:values.BCity_SName,
			Shortcode:values.BCity_Shortcode,
			PinYinZiTou:values.BCity_PinYinZiTou,
			IsUse:values.BCity_IsUse ? true : false,
			Memo:values.BCity_Memo
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
		entity.fields = fieldsArr.join(',') + ',BProvince_Id';
		
		entity.entity.Id = values.BCity_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//省份监听
		var dictList = ['BProvince'];
		
		for(var i=0;i<dictList.length;i++){
			me.doCountryListeners(dictList[i]);
		}
	},
	/**省份监听*/
	doCountryListeners:function(name){
		var me = this;
		var Name = me.getComponent('BCity_' + name + '_Name');
		var Id = me.getComponent('BCity_' + name + '_Id');
		var DataTimeStamp = me.getComponent('BCity_' + name + '_DataTimeStamp');
		
		Name.on({
			check: function(p, record) {
				Name.setValue(record ? record.get('BProvince_Name') : '');
				Id.setValue(record ? record.get('BProvince_Id') : '');
				DataTimeStamp.setValue(record ? record.get('BProvince_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	/**拼音字头监听*/
	initPinYinZiTouListeners:function(){
		var me = this,
			BCity_Name = me.getComponent('BCity_Name');
			
		BCity_Name.on({
			change:function(field,newValue,oldValue,eOpts){
				me.changePinYinZiTou(newValue);
			}
		});
	},
	changePinYinZiTou:function(data){
		var me = this,
			BCity_PinYinZiTou = me.getComponent('BCity_PinYinZiTou'),
			BCity_Shortcode = me.getComponent('BCity_Shortcode');
			
		if(data != ""){
			JShell.Action.delay(function(){
				JShell.System.getPinYinZiTou(data,function(value){
					me.getForm().setValues({
						BCity_PinYinZiTou:value,
						BCity_Shortcode:value
					});
				});
			},null,200);
		}else{
			me.getForm().setValues({
				BCity_PinYinZiTou:"",
				BCity_Shortcode:""
			});
		}
	},
	/**新增*/
	isAdd:function(){
		var me = this;
		me.callParent(arguments);
		
		var Name = me.getComponent('BCity_BProvince_Name'),
			Id = me.getComponent('BCity_BProvince_Id'),
			DataTimeStamp = me.getComponent('BCity_BProvince_DataTimeStamp');
		
		Name.setValue(me.BProvince.Name);
		Id.setValue(me.BProvince.Id);
		DataTimeStamp.setValue(me.BProvince.DataTimeStamp);
	}
});