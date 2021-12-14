/**
 * 销售表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.seller.Form',{
    extend:'Shell.ux.form.Panel',
    
    requires:[
    	'Shell.ux.form.field.BoolComboBox',
    	'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'销售表单',
    width:430,
    height:340,
    UseCode:'',
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchBSellerById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddBSeller',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateBSellerByField',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.initData();
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		//maxLength:20,enforceMaxLength:true
		items.push({fieldLabel:'主键ID',name:'BSeller_Id',hidden:true});
		
		items.push({x:0,y:10,fieldLabel:'名称',name:'BSeller_Name',allowBlank:false});
		items.push({x:210,y:10,fieldLabel:'简称',name:'BSeller_SName'});
		
		items.push({x:0,y:40,fieldLabel:'用户代码',name:'BSeller_UseCode',allowBlank:false,value:me.UseCode});
		items.push({x:210,y:40,fieldLabel:'拼音字头',name:'BSeller_PinYinZiTou'});
		
		items.push({x:0,y:70,fieldLabel:'快捷码',name:'BSeller_Shortcode'});
		items.push({x:210,y:70,fieldLabel:'职位',name:'BSeller_Position'});
		
		items.push({x:0,y:100,fieldLabel:'联系电话',name:'BSeller_PhoneCode'});
		items.push({x:210,y:100,fieldLabel:'联系方式',name:'BSeller_ContactInfo'});
		
		items.push({x:0,y:130,xtype:'uxSimpleComboBox',fieldLabel:'区域',
			name:'BSeller_AreaIn',itemId:'BSeller_AreaIn',editable:true});
		items.push({x:210,y:130,fieldLabel:'邮箱',name:'BSeller_EMail'});
		
		items.push({x:0,y:160,xtype:'uxBoolComboBox',fieldLabel:'使用',name:'BSeller_IsUse',value:true});
		items.push({x:210,y:160,xtype:'numberfield',fieldLabel:'显示次序',name:'BSeller_DispOrder',value:0});
		
		items.push({x:0,y:190,width:410,height:80,xtype:'textarea',fieldLabel:'备注',name:'BSeller_Comment'});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.BSeller_Name,
			SName:values.BSeller_SName,
			Shortcode:values.BSeller_Shortcode,
			PinYinZiTou:values.BSeller_PinYinZiTou,
			PhoneCode:values.BSeller_PhoneCode,
			ContactInfo:values.BSeller_ContactInfo,
			Position:values.BSeller_Position,
			IsUse:values.BSeller_IsUse,
			DispOrder:values.BSeller_DispOrder,
			Comment:values.BSeller_Comment,
			
			UseCode:values.BSeller_UseCode,
			AreaIn:values.BSeller_AreaIn,
			EMail:values.BSeller_EMail
		};
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
		
		for(var i in fields){
			fields[i] = fields[i].split('_').slice(1);
		}
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.BSeller_Id;
		return entity;
	},
	/**初始化数据*/
	initData:function(){
		var me = this;
		var url = JShell.System.Path.ROOT + '/StatService.svc/Stat_UDTO_SearchSellerArea';
		var BSeller_AreaIn = me.getComponent('BSeller_AreaIn');
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value;
				var len = list.length;
				var arr = [];
				for(var i=0;i<len;i++){
					var v = list[i];
					arr.push([v,v]);
				}
				BSeller_AreaIn.store.loadData(arr);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				me.fireEvent('save',me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY){
					msg = '销售代码有重复';
				}
				JShell.Msg.error(msg);
			}
		});
	}
});