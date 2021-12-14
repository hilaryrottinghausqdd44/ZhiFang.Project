/**
 * 仪器表单
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.Form', {
	extend: 'Shell.ux.form.Panel',
	title: "仪器信息",
	formtype: "edit",
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	bodyPadding:10,
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchEEquipById?isPlanish=true',
	/**查询字典*/
	selectDictUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/ST_UDTO_AddEEquip',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEEquipByField',
	/**显示成功信息*/
	showSuccessInfo: false,
	/**是否启用保存按钮*/
	hasSave: true,
	height: 375,
	width: 480,
	hasReset: false,
    /**主键字段*/
	PKField:'EEquip_Id',
	/**数据主键*/
	PK:'',
    hasSetValue:true,
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
		return [ {
			fieldLabel: '编号',hidden:true,name: 'EEquip_Id',itemId: 'EEquip_Id'
		}, {
			fieldLabel: '仪器名称',
			name: 'EEquip_CName',emptyText: '必填项', allowBlank: false,
			itemId: 'EEquip_CName',colspan :2,width : me.defaults.width * 2
		}, {
			fieldLabel: '仪器编号',name: 'EEquip_EquipNo',itemId: 'EEquip_EquipNo'
		}, {
			fieldLabel: '英文名称',name: 'EEquip_EName',itemId: 'EEquip_EName'
		}, {
			fieldLabel: '简称',name: 'EEquip_SName',itemId: 'EEquip_SName'
		}, {
			fieldLabel: '快捷码',name: 'EEquip_Shortcode',itemId: 'EEquip_Shortcode'
		},{
			fieldLabel: '拼音字头',name: 'EEquip_PinYinZiTou',itemId: 'EEquip_PinYinZiTou'
		}, {
			fieldLabel: '代码',name: 'EEquip_UseCode',itemId: 'EEquip_UseCode'
		},{
			fieldLabel:'检验小组',name:'EEquip_SectionName',itemId:'EEquip_SectionName',
			hidden:true,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'检验小组选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JcallShell.QMS.DictTypeCode.SectionName + "'"
			}
		},{
			fieldLabel:'仪器类型',name:'EEquip_EquipType_CName',itemId:'EEquip_EquipType_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'仪器类型选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JcallShell.QMS.DictTypeCode.EquipType + "'"
			},
			listeners :{
				check: function(p, record) {
					var Name = me.getComponent('EEquip_EquipType_CName'),
					  Id = me.getComponent('EEquip_EquipType_Id');
					Name.setValue(record ? record.get('PDict_CName') : '');
					Id.setValue(record ? record.get('PDict_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel: '厂商',name: 'EEquip_FactoryName',itemId: 'EEquip_FactoryName'
		},{
			fieldLabel: '出厂编号',name: 'EEquip_FactoryOutNo',itemId: 'EEquip_FactoryOutNo'
		},{
			fieldLabel: '放置区域',name: 'EEquip_StoreArea',
			colspan :1,width : me.defaults.width * 1,
			itemId: 'EEquip_StoreArea'
		},{
			fieldLabel: '启用日期',name: 'EEquip_EnableDate',itemId: 'EEquip_EnableDate',
			xtype:'datefield',format:'Y-m-d'
		},{
			fieldLabel: '校准有效期',name: 'EEquip_CalibrateDate',
			itemId: 'EEquip_CalibrateDate',xtype:'datefield',format:'Y-m-d'
		},{
			fieldLabel: '显示次序',xtype:'numberfield',maxValue:100000,step:1,
			name: 'EEquip_DispOrder',itemId: 'EEquip_DispOrder'
		},{
			fieldLabel: '是否使用',xtype: 'checkbox',name: 'EEquip_IsUse',
			itemId: 'EEquip_IsUse',checked: true
		},{
			fieldLabel: '仪器类型id',hidden:true,
			name: 'EEquip_EquipType_Id',itemId: 'EEquip_EquipType_Id'
		},{
			fieldLabel: '小组id',hidden:true,
			name: 'EEquip_SectionName_Id',itemId: 'EEquip_SectionName_Id'
		},{
			fieldLabel: '描述',name: 'EEquip_Comment',
			itemId: 'EEquip_Comment',colspan :2,width : me.defaults.width * 2,
			xtype:'textarea',height:50
		}];
	},
   /**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.EEquip_EnableDate = JShell.Date.getDate(data.EEquip_EnableDate);
		data.EEquip_CalibrateDate = JShell.Date.getDate(data.EEquip_CalibrateDate);
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		var EEquip_CName = me.getComponent('EEquip_CName');
		EEquip_CName.on({
            change:function(field,newValue){
                me.changePinYinZiTou();
            }
        });
       
	},
	
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		if(values.EEquip_CName==''){
			JShell.Msg.error('仪器名称不能为空!');
		    return;
		}
		var entity = {
			CName:values.EEquip_CName,
			IsUse:values.EEquip_IsUse ? true : false,
			EquipNo:values.EEquip_EquipNo,
			FactoryName:values.EEquip_FactoryName,
			FactoryOutNo:values.EEquip_FactoryOutNo,
			StoreArea:values.EEquip_StoreArea
		};
		if(values.EEquip_CalibrateDate){
			entity.CalibrateDate=JShell.Date.toServerDate(values.EEquip_CalibrateDate);
		}
		if(values.EEquip_EnableDate){
			entity.EnableDate=JShell.Date.toServerDate(values.EEquip_EnableDate);
		}
		if(values.EEquip_PinYinZiTou!=''){
	    	entity.PinYinZiTou=values.EEquip_PinYinZiTou;
	    }
		if(values.EEquip_EName!=''){
	    	entity.EName=values.EEquip_EName;
	    }
		if(values.EEquip_SName!=''){
	    	entity.SName=values.EEquip_SName;
	    }
		if(values.EEquip_Shortcode!=''){
	    	entity.Shortcode=values.EEquip_Shortcode;
	    }
		if(values.EEquip_UseCode!=''){
	    	entity.UseCode=values.EEquip_UseCode;
	    }
		if(values.EEquip_Comment!=''){
	    	entity.Comment=values.EEquip_Comment;
	    }
		if(values.EEquip_SectionName_Id!=''){
	    	entity.SectionName=values.EEquip_SectionName_Id;
		}
	    if(values.EEquip_DispOrder!=''){
	    	entity.DispOrder=values.EEquip_DispOrder;
	    }
        //仪器类型
		if(values.EEquip_EquipType_Id){
			entity.EquipType = {
				Id:values.EEquip_EquipType_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			//fields = me.getStoreFields(),
			entity = me.getAddParams();
		 var fields = [
			'CName','IsUse','Id',
			'EquipNo','FactoryName','FactoryOutNo',
			'EnableDate','CalibrateDate','StoreArea'
		];
		if(values.EEquip_PinYinZiTou!=''){
	    	fields.push('PinYinZiTou');
	    }
		if(values.EEquip_EName!=''){
	    	fields.push('EName');
	    }
		if(values.EEquip_SName!=''){
	    	fields.push('SName');
	    }
		if(values.EEquip_Shortcode!=''){
	    	fields.push('Shortcode');
	    }
		if(values.EEquip_UseCode!=''){
	    	fields.push('UseCode');
	    }
		if(values.EEquip_Comment!=''){
	    	fields.push('Comment');
	    }
		if(values.EEquip_SectionName_Id!=''){
	        fields.push('SectionName');
		}
	    if(values.EEquip_DispOrder!=''){
	    	fields.push('DispOrder');
	    }
		if(values.EEquip_EquipType_Id!='' && values.EEquip_EquipType_Id!=null){
		    fields.push('EquipType_Id');
		}
		entity.fields = fields.join(',');
		entity.entity.Id = values.EEquip_Id;
		return entity;
	},
	changePinYinZiTou:function(data){
		var me = this,
		    name = me.getComponent('EEquip_CName').getValue();
		if(name != ""){
			JShell.Action.delay(function(){
				JShell.System.getPinYinZiTou(name,function(value){
					me.getForm().setValues({
						EEquip_PinYinZiTou:value,
						EEquip_Shortcode:value
					});
				});
			},null,200);
		}else{
			me.getForm().setValues({
				EEquip_PinYinZiTou:"",
				EEquip_Shortcode:""
			});
		}
	},
	/**更改标题*/
	changeTitle: function() {}
});