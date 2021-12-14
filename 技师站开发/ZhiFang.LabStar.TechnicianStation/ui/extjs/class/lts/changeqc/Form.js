/**
 * 转换质控样本
 * @author liangyl
 * @version 2019-12-13
 */
Ext.define('Shell.class.lts.changeqc.Form', {
	extend:'Shell.ux.form.Panel',
	title:'转换质控样本',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	width:455,
	height:320,
	bodyPadding:'20px 0px 20px 10',
	isLoadRecord:false,
	layout: 'anchor',
	defaults: {
		anchor: '100%'
	},
	autoScroll:true,
	
	/***获取样本单数据服务路径*/
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormById?isPlanish=true',
	/***检查样本是否符合转换条件 */
    selectQcUrl:'/ServerWCF/LabStarService.svc/CheckSampleConvertStatus',
    /***常规转质控保存服务地址*/
    saveUrl:'/ServerWCF/LabStarQCService.svc/TestFormConvatQCItem',
    //修改数据服务路径
	editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisTestFormByField',
	//是否启用保存按钮
	hasSave:false,
	//是否重置按钮
	hasReset:false,
	hasCancel:true,
	/**表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'edit',
	/**检验单ID*/
	TestFormID:null,
	/**小组ID*/
	SectionID:null,
	/**原检验单打标记：删除作废*/
	MainStatusID:-2,	
	 //按钮是否可点击
    BUTTON_CAN_CLICK:true, 
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.PK = me.TestFormID;
		if(!me.PK || !me.SectionID){
			JShell.Msg.alert('样本单ID和检验小组ID都不能为空!');
		}
		me.buttonToolbarItems =[{text:'转换质控样本',tooltip:'转换质控样本',iconCls:'button-saveas',
            handler:function(but,e){
            	me.onSave();
		    }
        },'->','cancel'];
		me.items = me.createItems();
		me.callParent(arguments);
		me.initFromListeners();
	},
	createItems:function(){
		var me = this;
		var layout = {type:'table', columns:2};
		var defaults = { width:200,labelWidth:60,labelAlign: 'right'};
		var items = [
			{ xtype: 'fieldset',title: '请核对即将转换为质控的检验单',collapsible: true,
				defaultType: 'textfield',itemId: 'LisTestForm',
				layout: layout,defaults: defaults,
				items: [
					{xtype: 'displayfield',format: 'Y-m-d',fieldLabel: '检验日期',name: 'LisTestForm_GTestDate',readOnly: true,locked: true,colspan: 1,width: defaults.width * 1},
					{xtype: 'displayfield', fieldLabel: '姓名',name: 'LisTestForm_CName',readOnly: true,locked: true,colspan: 1,width: defaults.width * 1},
					{xtype: 'displayfield',fieldLabel:'样本类型id',name:'LisTestForm_GSampleTypeID', itemId:'LisTestForm_GSampleTypeID',colspan: 1,width: defaults.width * 1,hidden:true},
					{xtype: 'displayfield',fieldLabel:'样本类型',name:'LisTestForm_GSampleTypeCName', itemId:'LisTestForm_GSampleTypeCName',colspan: 1,width: defaults.width * 1},
					{xtype: 'displayfield',fieldLabel: '病历号',name: 'LisTestForm_PatNo',readOnly: true,locked: true,colspan: 1,width: defaults.width * 1},
					{xtype: 'displayfield',fieldLabel: '样本号',name: 'LisTestForm_GSampleNo',readOnly: true,locked: true,colspan: 1,width: defaults.width * 1},
					{xtype: 'displayfield',fieldLabel: '性别',name: 'LisTestForm_GenderID',itemId:'LisTestForm_GenderID',colspan: 1,width: defaults.width * 1,hidden:true},
					{xtype: 'displayfield',fieldLabel: '性别',name: 'LisTestForm_GenderCName',itemId:'LisTestForm_GenderCName',colspan: 1,width: defaults.width * 1}
                ]
			},
			{ xtype: "checkbox",boxLabel: "转入质控表成功后,删除原样本",inputValue: "true",checked: true,//labelWidth:200,
				itemId: "IsUse",name: "IsUse",fieldLabel: ""
			},
			{ xtype: 'fieldset',title: '请核对即将转换为质控的检验单',collapsible: true,
				defaultType: 'textfield',itemId: 'QC',
				layout: layout,defaults: defaults,labelWidth:0,
				items: [{fieldLabel: '',xtype: 'uxCheckTrigger',
					name: 'LisTestForm_QCMaterial',itemId: 'LisTestForm_QCMaterial',
					colspan:2,width:defaults.width * 2,className: 'Shell.class.lts.changeqc.CheckGrid',
					classConfig:{checkOne:true,width:400,SectionID:me.SectionID},
					listeners :{
						check: function(p, record) {
							var CName = me.getComponent('QC').getComponent('LisTestForm_QCMaterial');
							var ID = me.getComponent('QC').getComponent('LisTestForm_QCMaterialID');
							var strName = "";
							if(record){
								//质控物显示：仪器、模块、质控物名称、浓度水平
								strName+=record.get('LBQCMaterial_LBEquip_CName');
								strName+='/';
								strName+=record.get('LBQCMaterial_EquipModule');
								strName+='/';
								strName+=record.get('LBQCMaterial_CName');
								strName+='/';
								strName+=record.get('LBQCMaterial_ConcLevel');
								strName+='/';
							}
							CName.setValue(strName);
							ID.setValue(record ? record.get('LBQCMaterial_Id') : '');
							p.close();
						}
					}
				},{
					xtype:'textfield',itemId:'LisTestForm_QCMaterialID',fieldLabel:'ID',hidden:true
				}]
			},
			{fieldLabel:'主键ID',name:'LisTestForm_Id',hidden:true}
		];
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		data.LisTestForm_GTestDate = data.LisTestForm_GTestDate ? JShell.Date.toString(data.LisTestForm_GTestDate,true) : '';
		//样本类型
		var GSampleTypeCName = me.getComponent('LisTestForm').getComponent('LisTestForm_GSampleTypeCName');
		me.getSampleType(function(list){
			for(var i=0;i<list.length;i++){
				if(list[i].LBSampleType_Id == data.LisTestForm_GSampleTypeID ){
					GSampleTypeCName.setValue(list[i].LBSampleType_CName);
					break;
				}
			}
		});
        //性别
       	var GenderCName = me.getComponent('LisTestForm').getComponent('LisTestForm_GenderCName');
		me.getGender(function(list){
			for(var i=0;i<list.length;i++){
				if(list[i].BSex_Id == data.LisTestForm_GenderID ){
					GenderCName.setValue(list[i].BSex_Name);
					break;
				}
			}
		});
		return data;
	},
	/**初始化表单监听*/
	initFromListeners: function() {
		var me = this;
    },
   /**创建数据字段*/
	getStoreFields: function() {
		var me = this;
		var fields = ['Id', 'GTestDate', 'CName', 'GSampleTypeID',
			'PatNo', 'GSampleNo', 'GenderID'
		];
		fields = "LisTestForm_" + fields.join(",LisTestForm_");
		fields = fields.split(',');
		return fields;
	},
	/**获取样本类型信息*/
	getSampleType: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl('/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true');
			
		url += '&fields=LBSampleType_CName,LBSampleType_Id';
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value ? data.value.list : [];
				callback(list);
			} 
		});
	},
	/**获取性别信息*/
	getGender: function(callback) {
		var me = this,
			url = JShell.System.Path.LIIP_ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBSexByHQL?isPlanish=true';
			
		url += '&fields=BSex_Name,BSex_Id';
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value ? data.value.list : [];
				callback(list);
			} 
		});
	},
	/**判断常规转为质控数据校验*/
	getTurnQcCheck: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectQcUrl)+'?TestFormID='+me.TestFormID;
        var QCMatID = me.getComponent('QC').getComponent('LisTestForm_QCMaterialID').getValue();
        if(QCMatID) url += '&QCMatID='+QCMatID;
		JcallShell.Server.get(url, function(data) {
			if(data.success) {
//				var list = data.value ? data.value.list : [];
				callback(data.msg);
			}else{
				callback(data.msg);
//				JShell.Msg.alert(data.msg);
				return;
			}
		});
	},
	/**转换质控样本*/
	onSave : function(){
		var me = this;
		if(!me.BUTTON_CAN_CLICK)return;
		
		if(!me.PK || !me.SectionID){
			JShell.Msg.alert('样本单ID和检验小组ID都不能为空!');
			return;
		}
		var ID = me.getComponent('QC').getComponent('LisTestForm_QCMaterialID').getValue();
        if(!ID){
			JShell.Msg.alert('请选择质控物!');
			return;
		}
        me.BUTTON_CAN_CLICK = false;
		me.getTurnQcCheck(function(msg){
			me.BUTTON_CAN_CLICK = true;
			//正确的msg为空
			if(!msg){
				me.onUpdate();
			}else{
				JShell.Msg.confirm({
					msg:'是否放弃转换项目结果为空或者项目结果不能进行转化的项目?'
				},function(but){
					if (but != "ok")return;
					else me.onUpdate();
				});
			}
		});
	},
	/**转换质控样本保存*/
	onUpdate : function(){
		var me = this;
		var QCMatID = me.getComponent('QC').getComponent('LisTestForm_QCMaterialID').getValue();

		var	url = JShell.System.Path.getRootUrl(me.saveUrl)+'?QCMatId='+QCMatID+'&TestFormId='+me.TestFormID;
		me.BUTTON_CAN_CLICK = false;
		JShell.Server.get(url, function(data) {
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				//转入质控表成功后,删除原样本
			    var IsUse = me.getComponent('IsUse').getValue();
				//如果原检验单有结果的项目都转换成功，并且标记删除源检验单的情况下，原检验单打标记：删除作废MainStatusID=-2 
				if(IsUse){
				    if(data.BoolFlag){	//返回参数中的BoolFlag为true表示有结果的项目都转换成功了
				         me.updateOne();
					}else{
						JShell.Msg.confirm({
							msg:'原检验单只有部分项目转换成成功,是否删除作废?'
						},function(but){
							if (but != "ok"){
								me.isLoadRecord = true;
								JShell.Msg.alert('转换质控样本成功');
								return;
							}
							else me.updateOne();
						});
					}	
			    }else{
			    	JShell.Msg.alert('转换质控样本成功');
			    }
			}else{
				JShell.Msg.alert(data.msg);
			}
		});
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this,
			type = me.formtype;
	},
	/**检验单打删除标记*/
	updateOne:function(id){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = {};
		params.entity = {Id:me.TestFormID};
		params.entity.MainStatusID = me.MainStatusID;
		params.fields = 'Id,MainStatusID';
		me.BUTTON_CAN_CLICK = false;
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			me.BUTTON_CAN_CLICK = true;
			if (data.success) {
				me.isLoadRecord = true;
				JShell.Msg.alert('转换质控样本成功');
			}else{
				JShell.Msg.alert(msg);
			}
		});
	}
});
