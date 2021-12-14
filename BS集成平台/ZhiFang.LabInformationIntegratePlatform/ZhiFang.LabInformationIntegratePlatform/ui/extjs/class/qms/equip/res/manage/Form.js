/**
 * 职责表单
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.manage.Form', {
	extend: 'Shell.ux.form.Panel',
	requires:[
	    'Shell.ux.form.field.BoolComboBox'
    ],
	title: "职责信息",
	formtype: "edit",
	bodyPadding: '10px 10px 5px 10px',
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 60,
		width: 200,
		labelAlign: 'right'
	},
	hasSetValue:true,
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEResponsibilityById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/ST_UDTO_AddEResponsibility',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEResponsibilityByField',
	/**显示成功信息*/
	showSuccessInfo: true,
	/**是否启用保存按钮*/
	hasSave: true,
	height: 260,
	width: 440,
	hasReset: true,
	/**数据主键*/
	PK:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [{
			fieldLabel: '职责名称',colspan: 2,
			emptyText: '必填项',allowBlank: false,
			name: 'EResponsibility_CName',itemId: 'EResponsibility_CName',
			width: me.defaults.width * 2
		}, {
			fieldLabel: '英文名称',name: 'EResponsibility_EName',
			itemId: 'EResponsibility_EName',colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '简称',name: 'EResponsibility_SName',
			itemId: 'EResponsibility_SName',colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '快捷码',name: 'EResponsibility_Shortcode',
			itemId: 'EResponsibility_Shortcode',colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel: '拼音字头',name: 'EResponsibility_PinYinZiTou',
			itemId: 'EResponsibility_PinYinZiTou',colspan: 1,
			width: me.defaults.width * 1
		}, {
			fieldLabel:'启用',name:'EResponsibility_IsUse',itemId: 'EResponsibility_IsUse',
			xtype:'uxBoolComboBox',value:true,hasStyle:true,
			colspan: 1,width: me.defaults.width * 1
		},{
			fieldLabel: '次序',
			name: 'EResponsibility_DispOrder',
			itemId: 'EResponsibility_DispOrder',
			xtype:'numberfield',value:0,
			emptyText:'必填项',allowBlank:false,
		    colspan: 1,width: me.defaults.width * 1
		}, {
			xtype: 'textarea',fieldLabel: '描述',height: 60,
            itemId: 'EResponsibility_Comment',name: 'EResponsibility_Comment',
			colspan: 2,width: me.defaults.width * 2
		}, {
			fieldLabel: '主键ID',name: 'EResponsibility_Id',hidden: true
		}];
		return items;
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var CName = me.getComponent('EResponsibility_CName');
		CName.on({
            change:function(field,newValue){
                me.doCNameChange();
            }
      });
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName:values.EResponsibility_CName,
			IsUse:values.EResponsibility_IsUse ? 1 : 0,
			DispOrder:values.EResponsibility_DispOrder,
			Shortcode:values.EResponsibility_Shortcode,
			PinYinZiTou:values.EResponsibility_PinYinZiTou,
			EName:values.EResponsibility_EName,
			SName:values.EResponsibility_SName,
			Comment:values.EResponsibility_Comment
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
		
		entity.entity.Id = values.EResponsibility_Id;
		return entity;
	},
	setValues:function(values){
		var me = this;
		me.hasSetValue = false;
		me.getForm().setValues(values);
      
		me.hasSetValue = true;
	},
	doCNameChange:function(){
		var me = this;
		if(!me.hasSetValue) return;
		var CName = me.getComponent('EResponsibility_CName'),
			EResponsibility_PinYinZiTou = me.getComponent('EResponsibility_PinYinZiTou'),
			EResponsibility_Shortcode = me.getComponent('EResponsibility_Shortcode'),
			value = '';
		value = CName.getValue();
		var url = JShell.System.Path.ROOT + '/ConstructionService.svc/GetPinYin?chinese=' + value,
			isAscii = escape(value).indexOf("%u") == -1 ? true : false;
		if(isAscii){//全英文直接联动
			EResponsibility_PinYinZiTou.setValue(value);
			EResponsibility_Shortcode.setValue(value);
		}else{
			JShell.Action.delay(function(){
				JShell.Server.get(url,function(data){
					EResponsibility_PinYinZiTou.setValue(data.value);
					EResponsibility_Shortcode.setValue(data.value);
				});
			});
		}
	},

	/**更改标题*/
	changeTitle: function() {}
});