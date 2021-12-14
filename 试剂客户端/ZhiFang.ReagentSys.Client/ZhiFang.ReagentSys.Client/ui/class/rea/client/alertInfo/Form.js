/**
 * 预警设置
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.alertInfo.Form', {
	extend: 'Shell.ux.form.Panel',	
	requires: [
	    'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.BoolComboBox',
	    'Shell.class.rea.client.alertInfo.ColorField'
	],
	
	title: '预警设置信息',
	width: 250,
	height: 390,
	/**内容周围距离*/
	bodyPadding:'10px 20px 0px 10px',
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaAlertInfoSettingsById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaAlertInfoSettings',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaAlertInfoSettingsByField',
	
    formtype: "edit",
    autoScroll: false,
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 70,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
    /**预警分类类型Key*/
	AlertType: "AlertType",
	/**已存在的数据*/
	GridData:[],
	/**默认预警类型*/
	defaultAlertType:'1',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();		
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.AlertType, false, false, null);
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '预警分类',xtype: 'uxSimpleComboBox',
			name: 'ReaAlertInfoSettings_AlertTypeId',
			itemId: 'ReaAlertInfoSettings_AlertTypeId',
			hasStyle: true,	allowBlank: false,colspan: 1,
			data: JShell.REA.StatusList.Status[me.AlertType].List,
			width: me.defaults.width * 1,value:me.defaultAlertType,
			listeners:{
				change : function(com,newValue,oldValue,eOpts ){
					var  info = me.getComponent('info');
					var msg ='';
					var v = newValue+'';
					if(v=='1'||  v=='2'){
						msg='说明:库存预警上限值和下限值范围使用的是百分比(%)';
					}else{
						msg='说明:效期预警上限值和下限值范围使用的是数量(天数)';
					}
					if(!msg)info.setVisible(false);
					else info.setVisible(true);
					info.setValue(msg);
				}
			}
		},{
			fieldLabel: '预警颜色',name: 'ReaAlertInfoSettings_AlertColor',
			itemId:'ReaAlertInfoSettings_AlertColor',allowBlank: false,xtype: 'colorfield'
		},{
			fieldLabel: '下限值',name: 'ReaAlertInfoSettings_StoreLower',
			itemId: 'ReaAlertInfoSettings_StoreLower',
			xtype:'numberfield',value:0,
			emptyText: '必填项',allowBlank: false
		},{
			fieldLabel:'上限值',name:'ReaAlertInfoSettings_StoreUpper',
			itemId: 'ReaAlertInfoSettings_StoreUpper',
			xtype:'numberfield',value:0,
			emptyText: '必填项',allowBlank: false
		},{
			fieldLabel:'启用',name:'ReaAlertInfoSettings_Visible',
			xtype:'uxBoolComboBox',value:true,hasStyle:true
		},{
			fieldLabel:'显示次序',name:'ReaAlertInfoSettings_DispOrder',
			emptyText:'必填项',allowBlank:false,xtype:'numberfield',value:0
		},{
			height: 85,fieldLabel:'备注',emptyText: '备注',
			name: 'ReaAlertInfoSettings_Memo',xtype: 'textarea'
		},{
			fieldLabel: '主键ID',name: 'ReaAlertInfoSettings_Id',hidden: true
		},{
			xtype:'displayfield',fieldLabel:'  ',
		    value:'',itemId:'info',labelSeparator:'',
			fieldStyle: 'font-size:14px;color:red;background:none;border:0;border-bottom:0px',
			style: {
				marginLeft: '-60px'
			}
		});
		return items;
	},
	changeColor:function(value){
		var me = this;
		var AlertColor = me.getComponent('ReaAlertInfoSettings_AlertColor');
        AlertColor.inputEl.applyStyles({
            backgroundColor: value
        });
        AlertColor.setFieldStyle('background:' + value + ';color:#FFFFFF');
	},
    setAlertType:function(val){
    	var me = this;
        var AlertTypeId = me.getComponent('ReaAlertInfoSettings_AlertTypeId');
        AlertTypeId.setValue(val);

    },
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			AlertTypeId: values.ReaAlertInfoSettings_AlertTypeId,
			Memo: values.ReaAlertInfoSettings_Memo,
			Visible:values.ReaAlertInfoSettings_Visible ? 1 : 0,
			StoreUpper:values.ReaAlertInfoSettings_StoreUpper,
			StoreLower:values.ReaAlertInfoSettings_StoreLower
		};
		var AlertType = me.getComponent('ReaAlertInfoSettings_AlertTypeId');
		if(values.ReaAlertInfoSettings_AlertTypeId){
			entity.AlertTypeCName=AlertType.getRawValue();
		}
		if(values.ReaAlertInfoSettings_AlertColor){
			entity.AlertColor=values.ReaAlertInfoSettings_AlertColor;
		}
		if(values.ReaAlertInfoSettings_DispOrder){
			entity.DispOrder=values.ReaAlertInfoSettings_DispOrder;
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
			'AlertTypeId', 'Id', 'Memo', 'Visible','StoreUpper',
			'StoreLower','AlertTypeCName','DispOrder','AlertColor'
		];
		entity.fields = fields.join(',');
		if(values.ReaAlertInfoSettings_Id != '') {
			entity.entity.Id = values.ReaAlertInfoSettings_Id;
		}
		return entity;
	},
	/**返回数据处理方法*/
	changeResult: function(data) {
		var me =this;
		data.ReaAlertInfoSettings_Visible = data.ReaAlertInfoSettings_Visible == '1' ? true : false;
		
		return data;
	},
	  /**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
	},
		/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		var values = me.getForm().getValues();
		//类型
		var AlertTypeId = values.ReaAlertInfoSettings_AlertTypeId;
		if(!AlertTypeId){
			JShell.Msg.alert('预警分类不能为空!');
			return;
		}
		var AlertColor = values.ReaAlertInfoSettings_AlertColor;

		if(!AlertColor){
			JShell.Msg.alert('预警颜色不能为空!');
			return;
		}
		if(!me.getForm().isValid()) return;
	
		//范围值 上限
		var StoreUpper = values.ReaAlertInfoSettings_StoreUpper;
		//范围值 下限
		var StoreLower = values.ReaAlertInfoSettings_StoreLower;
		
		if(Number(StoreLower)>Number(StoreUpper)){
			JShell.Msg.alert('下限值不能大于上限值!');
			return;
		}

		var isExec = me.IsOnlyCheck();
		if(!isExec)return;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				id += '';
				me.fireEvent('save',me,id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**校验是否是唯一
	 * 类型+范围值+颜色值 判断是否重复
	 * */
	IsOnlyCheck:function(){
		var me = this;
		var values = me.getForm().getValues();
		//类型
		var AlertTypeId = values.ReaAlertInfoSettings_AlertTypeId;
		//范围值 下限
		var  StoreLower = values.ReaAlertInfoSettings_StoreLower;
		//范围值 上限
		var StoreUpper = values.ReaAlertInfoSettings_StoreUpper;
		//颜色值
		var AlertColor =  values.ReaAlertInfoSettings_AlertColor;
	    var Settings_Id = values.ReaAlertInfoSettings_Id;
	    var str=AlertTypeId+StoreUpper+StoreLower+AlertColor;
	    StoreLower=Number(StoreLower);
	    StoreUpper=Number(StoreUpper);
	    var isExec=true;
		var len = me.GridData.length;
		var recs =me.GridData;
		var msg ='';
		for(var i = 0;i<len;i++){
			var Type = recs[i].get('ReaAlertInfoSettings_AlertTypeId');
	       	var TypeName = recs[i].get('ReaAlertInfoSettings_AlertTypeCName');
	        var Lower =  recs[i].get('ReaAlertInfoSettings_StoreLower');
			var Upper = recs[i].get('ReaAlertInfoSettings_StoreUpper');
            var Color = recs[i].get('ReaAlertInfoSettings_AlertColor');
			var id = recs[i].get('ReaAlertInfoSettings_Id');
			
			Lower=Number(Lower);
	        Upper=Number(Upper);
			//判断是否是唯一
			var strdata=Type+Upper+Lower+Color;
			
			if(strdata==str && !Settings_Id){
				if(msg)msg+='</br>';
				msg='预警分类:【'+TypeName+'】颜色:【'+Color+ '】已存在相同的预警数据!';
				isExec=false;
			}
			if(strdata==str && Settings_Id && Settings_Id!=id){
				if(msg)msg+='</br>';
				msg='预警分类:【'+TypeName+'】颜色:【'+Color+ '】已存在相同的预警数据!';
				isExec=false;
			}
			// 同一类型不允许交叉
			if(Type==AlertTypeId){
				if(Settings_Id && Settings_Id ==id )continue;
				if(StoreLower>Lower && StoreLower<Upper){
					if( StoreUpper<Upper ){
						if(msg)msg+='</br>';
	                	msg='预警分类:【'+TypeName+'】存在上下限范围交叉数据,请检查!';
						isExec=false;
					}
				}
				
				if((StoreLower<=Lower) && (StoreUpper>=Lower  )){
					if(msg)msg+='</br>';
                	msg='预警分类:【'+TypeName+'】存在上下限范围交叉数据,请检查!';
					isExec=false;
				}
				
				if(StoreLower>Lower && (StoreLower<=Upper) ){
					if(msg)msg+='</br>';
                	msg='预警分类:【'+TypeName+'】存在上下限范围交叉数据,请检查!';
					isExec=false;
				}
			}
		}
		if(!isExec)JShell.Msg.error(msg);
		return isExec;
	}
});