/**
 * 项目信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.project.Form',{
	extend:'Shell.ux.model.ExtraForm',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'项目信息',
    
    /**其他信息模板地址*/
	OtherMsgUrl:null,
    
    /**获取数据服务路径*/
    selectUrl:'/BaseService.svc/ST_UDTO_SearchFProjectById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddFProject',
    /**修改服务地址*/
    editUrl:'/BaseService.svc/ST_UDTO_UpdateFProjectByField', 
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		
		me.on({
			load:function(p,data){
				if(data.success){
					me.OtherMsgContent = data.value.FProject_OtherMsg;
					me.changeOtherMsg();
				}else{
					me.getComponent('OtherMsg').hide();
				}
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			items = [];
		
		//项目名称
		items.push({
			fieldLabel:'项目名称',name:'FProject_CName',
			emptyText:'必填项',allowBlank:false
		});
		
		//项目类别
		items.push({
			fieldLabel:'项目类别',
			//emptyText:'必填项',allowBlank:false,
			name:'FProject_Type_CName',
			itemId:'FProject_Type_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'项目类别选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.ProjectType + "'"
			}
		},{
			fieldLabel:'项目类别主键ID',hidden:true,
			name:'FProject_Type_Id',
			itemId:'FProject_Type_Id'
		});
		
		//项目负责人
		items.push({
			fieldLabel:'项目负责人',
			//emptyText:'必填项',allowBlank:false,
			name:'FProject_ProjectLeader_CName',
			itemId:'FProject_ProjectLeader_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.user.CheckApp'
		},{
			fieldLabel:'项目负责人主键ID',hidden:true,
			name:'FProject_ProjectLeader_Id',
			itemId:'FProject_ProjectLeader_Id'
		},{
			fieldLabel:'项目负责人时间戳',hidden:true,
			name:'FProject_ProjectLeader_DataTimeStamp',
			itemId:'FProject_ProjectLeader_DataTimeStamp'
		});
		//现场负责人
		items.push({
			fieldLabel:'现场负责人',
			//emptyText:'必填项',allowBlank:false,
			name:'FProject_SpotLeader_CName',
			itemId:'FProject_SpotLeader_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.user.CheckApp'
		},{
			fieldLabel:'现场负责人主键ID',hidden:true,
			name:'FProject_SpotLeader_Id',
			itemId:'FProject_SpotLeader_Id'
		},{
			fieldLabel:'现场负责人时间戳',hidden:true,
			name:'FProject_SpotLeader_DataTimeStamp',
			itemId:'FProject_SpotLeader_DataTimeStamp'
		});
		//远程负责人
		items.push({
			fieldLabel:'远程负责人',
			//emptyText:'必填项',allowBlank:false,
			name:'FProject_RemoteLeader_CName',
			itemId:'FProject_RemoteLeader_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.user.CheckApp'
		},{
			fieldLabel:'远程负责人主键ID',hidden:true,
			name:'FProject_RemoteLeader_Id',
			itemId:'FProject_RemoteLeader_Id'
		},{
			fieldLabel:'远程负责人时间戳',hidden:true,
			name:'FProject_RemoteLeader_DataTimeStamp',
			itemId:'FProject_RemoteLeader_DataTimeStamp'
		});
		//紧急程度
		items.push({
			fieldLabel:'紧急程度',
			//emptyText:'必填项',allowBlank:false,
			name:'FProject_Urgency_CName',
			itemId:'FProject_Urgency_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'紧急程度选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.Urgency + "'"
			}
		},{
			fieldLabel:'紧急程度主键ID',hidden:true,
			name:'FProject_Urgency_Id',
			itemId:'FProject_Urgency_Id'
		});
		//项目状态
		items.push({
			fieldLabel:'项目状态',
			//emptyText:'必填项',allowBlank:false,
			name:'FProject_Status_CName',
			itemId:'FProject_Status_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'项目状态选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.ProjectStatus + "'"
			}
		},{
			fieldLabel:'项目状态主键ID',hidden:true,
			name:'FProject_Status_Id',
			itemId:'FProject_Status_Id'
		});
		//项目进度
		items.push({
			fieldLabel:'项目进度',
			//emptyText:'必填项',allowBlank:false,
			name:'FProject_Pace_CName',
			itemId:'FProject_Pace_CName',
			xtype:'uxCheckTrigger',
			className:'Shell.class.sysbase.dict.CheckGrid',
			classConfig:{
				title:'项目进度选择',
				defaultWhere:"fdict.FDictType.DictTypeCode='" + 
					JShell.WFM.DictTypeCode.ProjectPace + "'"
			}
		},{
			fieldLabel:'项目进度主键ID',hidden:true,
			name:'FProject_Pace_Id',
			itemId:'FProject_Pace_Id'
		});
		
		//进场时间
		items.push({
			fieldLabel:'进场时间',name:'FProject_EntryTime',
			xtype:'datefield',format:'Y-m-d'
		});
		//计划验收时间
		items.push({
			fieldLabel:'计划验收时间',name:'FProject_EstiAcceptTime',
			xtype:'datefield',format:'Y-m-d'
		});
		//实际验收时间
		items.push({
			fieldLabel:'实际验收时间',name:'FProject_AcceptTime',
			xtype:'datefield',format:'Y-m-d'
		});
		//计划人天数
		items.push({
			fieldLabel:'计划人天数',name:'FProject_EstiAllDays',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		});
		//实际人天数
		items.push({
			fieldLabel:'实际人天数',name:'FProject_AllDays',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',value:0
		});
		
		
		items.push({
			fieldLabel:'备注',height:85,
			name:'FProject_Memo',xtype:'textarea'
		},{
			boxLabel:'是否使用',name:'FProject_IsUse',
			xtype:'checkbox',checked:true
		},{
			fieldLabel:'主键ID',name:'FProject_Id',hidden:true
		},{
			fieldLabel:'附加信息',name:'FProject_ExtraMsg',hidden:true
		},{
			fieldLabel:'其他信息',name:'FProject_OtherMsg',hidden:true
		});
		
		//其他信息
		items.push({
			xtype:'button',
			itemId:'OtherMsg',
			text:'其他信息',
			hidden:true,
			handler:function(){
				me.openMsgForm('OtherMsg',me.OtherMsgContent);
			}
		});
		
		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			CName:values.FProject_CName,
			
			EntryTime:JShell.Date.toServerDate(values.FProject_EntryTime),
			EstiAcceptTime:JShell.Date.toServerDate(values.FProject_EstiAcceptTime),
			AcceptTime:JShell.Date.toServerDate(values.FProject_AcceptTime),
			EstiAllDays:values.FProject_EstiAllDays,
			AllDays:values.FProject_AllDays,
			
			IsUse:values.FProject_IsUse ? true : false,
			Memo:values.FProject_Memo
		};
		
		if(values.FProject_Type_Id){
			entity.Type = {Id:values.FProject_Type_Id};
		}
		if(values.FProject_Status_Id){
			entity.Status = {Id:values.FProject_Status_Id};
		}
		if(values.FProject_Pace_Id){
			entity.Pace = {Id:values.FProject_Pace_Id};
		}
		if(values.FProject_Urgency_Id){
			entity.Urgency = {Id:values.FProject_Urgency_Id};
		}
		
		if(values.FProject_ProjectLeader_Id){
			entity.ProjectLeader = {
				Id:values.FProject_ProjectLeader_Id,
				DataTimeStamp:values.FProject_ProjectLeader_DataTimeStamp.split(',')
			};
		}
		if(values.FProject_SpotLeader_Id){
			entity.SpotLeader = {
				Id:values.FProject_SpotLeader_Id,
				DataTimeStamp:values.FProject_SpotLeader_DataTimeStamp.split(',')
			};
		}
		if(values.FProject_RemoteLeader_Id){
			entity.RemoteLeader = {
				Id:values.FProject_RemoteLeader_Id,
				DataTimeStamp:values.FProject_RemoteLeader_DataTimeStamp.split(',')
			};
		}
		
		
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams();
		
		var fields = [
			'CName','Type_Id','Status_Id','Pace_Id','Urgency_Id',
			'ProjectLeader_Id','SpotLeader_Id','RemoteLeader_Id',
			'EntryTime','EstiAcceptTime','AcceptTime','EstiAllDays',
			'AllDays','IsUse','Memo,Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.FProject_Id;
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult:function(data){
		data.FProject_EntryTime = JShell.Date.getDate(data.FProject_EntryTime);
		data.FProject_EstiAcceptTime = JShell.Date.getDate(data.FProject_EstiAcceptTime);
		data.FProject_AcceptTime = JShell.Date.getDate(data.FProject_AcceptTime);
		return data;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		
		//字典监听
		var dictList = ['Type','Status','Pace','Urgency'];
		
		for(var i=0;i<dictList.length;i++){
			me.doDictListeners(dictList[i]);
		}
		
		//员工监听
		var dictList = ['ProjectLeader','SpotLeader','RemoteLeader'];
		
		for(var i=0;i<dictList.length;i++){
			me.doUserListeners(dictList[i]);
		}
	},
	/**字典监听*/
	doDictListeners:function(name){
		var me = this;
		var CName = me.getComponent('FProject_' + name + '_CName');
		var Id = me.getComponent('FProject_' + name + '_Id');
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('FDict_CName') : '');
				Id.setValue(record ? record.get('FDict_Id') : '');
				p.close();
			}
		});
	},
	/**员工监听*/
	doUserListeners:function(name){
		var me = this;
		var CName = me.getComponent('FProject_' + name + '_CName');
		var Id = me.getComponent('FProject_' + name + '_Id');
		var DataTimeStamp = me.getComponent('FProject_' + name + '_DataTimeStamp');
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('HREmployee_CName') : '');
				Id.setValue(record ? record.get('HREmployee_Id') : '');
				DataTimeStamp.setValue(record ? record.get('HREmployee_DataTimeStamp') : '');
				p.close();
			}
		});
	},
	afterSaveOtherMsg:function(){
		var me = this;
		me.changeOtherMsg();
	},
	changeOtherMsg:function(){
		var me = this;
		var OtherMsg = me.getComponent('OtherMsg');
		
		var msg = me.OtherMsgContent ? 
			'<b style="color:green">(存在)</b>' : '<b style="color:red">(无)</b>';
		OtherMsg.setText('其他信息' + msg);
		OtherMsg.show();
	},
	isAdd:function(){
		var me = this;
		me.getComponent('OtherMsg').hide();
		me.callParent(arguments);
	},
	isEdit:function(id){
		var me = this;
		me.getComponent('OtherMsg').show();
		me.callParent(arguments);
	}
});