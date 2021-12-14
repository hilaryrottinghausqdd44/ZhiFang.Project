/**
 * 仪器模板表单(修改信息不含文件上传)
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.EditForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.YearComboBox',
		'Shell.ux.form.field.CheckTrigger'
    ],
	bodyPadding:10,
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/ST_UDTO_SearchETempletById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/QMS_UDTO_AddExcelTemplet',
	/**修改服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateETempletByField',
	/**获取数据服务路径*/
	selectUrl2: '/QMSReport.svc/ST_UDTO_SearchETempletByHQL?isPlanish=true',
    /**小组Id*/
    SectionID:'',
	/**显示成功信息*/
	showSuccessInfo: false,
	title: "质量记录模板信息",
	formtype: 'edit',
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**数据主键*/
	PK:'',
	width:720,
	height:315,
	CheckTypeList: [
		['0', '按月审核'],
		['1', '按天审核']
	],
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
			fieldLabel: '编号',name: 'ETemplet_Id',	hidden:true,itemId: 'ETemplet_Id'
		},{
			fieldLabel: '模板名称',colspan :3,emptyText: '必填项', allowBlank: false,
			width : me.defaults.width * 3+1,name: 'ETemplet_CName',itemId: 'ETemplet_CName'
		},{
			fieldLabel: '简称',colspan :1,
			width : me.defaults.width * 1,name: 'ETemplet_SName',itemId: 'ETemplet_SName'
		},{
			colspan :1,fieldLabel: '代码',width :me.defaults.width * 1,
			name: 'ETemplet_UseCode',itemId: 'ETemplet_UseCode'
		},{
			name: 'ETemplet_CheckType',itemId: 'ETemplet_CheckType',
			fieldLabel: '审核类型',xtype: 'uxSimpleComboBox',
			hasStyle: false,editable: false,
			colspan :1,width : me.defaults.width * 1,
			emptyText: "审核类型",data:me.CheckTypeList
	   },{
			fieldLabel: '模板类型ID',hidden:true,name: 'ETemplet_TempletType_Id',
			itemId: 'ETemplet_TempletType_Id'
		},{
			fieldLabel: '模板类型时间戳',hidden:true,name: 'ETemplet_TempletType_DataTimeStamp',
			itemId: 'ETemplet_TempletType_DataTimeStamp'
		},{
			fieldLabel: '质量记录类型',colspan :1,width : me.defaults.width * 1,
		    name: 'ETemplet_TempletType_CName',itemId: 'ETemplet_TempletType_CName',
			xtype:'uxCheckTrigger',className:'Shell.class.qms.equip.templet.DictCheckGrid',
			classConfig:{
				width:350,
				title:'质量记录类型选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + JcallShell.QMS.DictTypeCode.QRecordType + "'"
			}
		},{
			fieldLabel: '仪器ID',hidden:true,
			name: 'ETemplet_EEquip_Id',itemId: 'ETemplet_EEquip_Id'
		},{
			fieldLabel: '仪器',	colspan :1,width : me.defaults.width * 1,
			xtype:'uxCheckTrigger',className:'Shell.class.qms.equip.CheckGrid',
			name: 'ETemplet_EEquip_CName',itemId: 'ETemplet_EEquip_CName',
			classConfig:{
				width:550,
				title:'仪器选择'
			}
		},{
			fieldLabel: '是否使用',name: 'ETemplet_IsUse',
			itemId: 'ETemplet_IsUse',xtype: 'checkbox',colspan :1,
			width : me.defaults.width * 1,checked: true
//			style: {
//				marginLeft: '20px'
//			}
		},{
			fieldLabel: '开始时间',colspan :1,
			width : me.defaults.width * 1,	xtype: 'datefield',format:'Y-m-d',
			name: 'ETemplet_BeginDate',itemId: 'ETemplet_BeginDate'
		}, {
			fieldLabel: '结束时间',format:'Y-m-d',
			colspan :2,width : me.defaults.width * 1,
			xtype: 'datefield',name: 'ETemplet_EndDate',itemId: 'ETemplet_EndDate'
		}, {
			fieldLabel: '填写说明',name: 'ETemplet_Comment',
			itemId: 'ETemplet_Comment',minHeight:100,colspan :3,
			width : me.defaults.width * 3,xtype:'textarea'
		}];
	},
	
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.ETemplet_BeginDate = JShell.Date.getDate(data.ETemplet_BeginDate);
		data.ETemplet_EndDate = JShell.Date.getDate(data.ETemplet_EndDate);
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			UseCode:values.ETemplet_UseCode,
			CName:values.ETemplet_CName,
			Comment:values.ETemplet_Comment,
			SName:values.ETemplet_SName,
			IsUse:values.ETemplet_IsUse ? 1 : 0
		};
		if(values.ETemplet_CheckType){
			entity.CheckType=values.ETemplet_CheckType;
		}
		if(values.ETemplet_BeginDate!=''){
			entity.BeginDate=JShell.Date.toServerDate(values.ETemplet_BeginDate);
		}
		if(values.ETemplet_EndDate!=''){
			entity.EndDate=JShell.Date.toServerDate(values.ETemplet_EndDate);
		}
		
		//小组
		if(me.SectionID!=''  && me.formtype=='add') {
			entity.Section = {
				Id:me.SectionID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		//仪器
		if(values.ETemplet_EEquip_Id!=''){
			entity.EEquip = {
				Id:values.ETemplet_EEquip_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
	    //模板类型
		if(values.ETemplet_TempletType_Id!=''){
			entity.TempletType = {
				Id:values.ETemplet_TempletType_Id,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = [
			'UseCode','CName','Comment','IsUse',
			'BeginDate','EndDate','Id','SName',
			'TempletType_Id','EEquip_Id','CheckType'
		];
		entity.fields = fields.join(',');
		if(values.ETemplet_Id!=''){
			entity.entity.Id = values.ETemplet_Id;
		}
		return entity;
	},
    /**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		var isExec=true;
	    me.checkisValid(function(data){
	    	if(data && data.count > 0){
				JShell.Msg.error("该小组已存在相同的模板名称");
				isExec=false;
				return;
			}
	    });
	    if(!isExec) return;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = JShell.System.Path.getRootUrl(url);
		
		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		
		if(!params) return;
		
		var id = params.entity.Id;
		
		params = Ext.JSON.encode(params);
		
		me.showMask(me.saveText);//显示遮罩层
		me.fireEvent('beforesave',me);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				id = me.formtype == 'add' ? data.value : id;
				if(Ext.typeOf(id) == 'object'){
					id = id.id;
				}
				if(me.isReturnData){
					me.fireEvent('save',me,me.returnData(id));
				}else{
					me.fireEvent('save',me,id);
				}
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				me.fireEvent('saveerror',me);
				JShell.Msg.error(data.msg);
			}
		});
	},
	
	/**验证小组模板名称是否已存在*/
	checkisValid:function(callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl2);
		var fields = [
			'ETemplet_Id'
		];
		var CName=me.getComponent('ETemplet_CName').getValue();
		url += '&fields=' + fields.join(',');
		url += "&where=etemplet.CName='" + CName+"'";
		if(me.SectionID){
			url += " and etemplet.Section.Id=" +me.SectionID ;
		}
		url+=' and etemplet.Id!='+me.PK;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data.value);
			}else{
				JShell.Msg.error(data.msg);
			}
		}, false, 500, false);
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		me.doTempletTypeListeners('TempletType');
		me.doEEquipListeners('EEquip');
	},
	/**模板类型监听*/
	doTempletTypeListeners:function(name){
		var me = this;
		var CName = me.getComponent('ETemplet_' + name+'_CName' );
		var Id = me.getComponent('ETemplet_' + name + '_Id');
		var DataTimeStamp = me.getComponent('ETemplet_' + name + '_DataTimeStamp');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
			    CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				DataTimeStamp.setValue(record ? record.get('PDict_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	/**仪器监听*/
	doEEquipListeners:function(name){
		var me = this;
		var CName = me.getComponent('ETemplet_' + name +'_CName' );
		var Id = me.getComponent('ETemplet_' + name + '_Id');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('EEquip_CName') : '');
				Id.setValue(record ? record.get('EEquip_Id') : '');
				p.close();
			}
		});
	},
	/**更改标题*/
	changeTitle: function() {}
});