/**
 * 样本信息
 * @author Jcall
 * @version 2020-06-21
 */
Ext.define('Shell.class.lts.sample.Info',{
    extend:'Shell.ux.form.Panel',
    requires:[
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'样本信息',
    width:260,
    bodyPadding:5,
    
    //获取数据服务路径
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormById?isPlanish=true',
    //新增就诊信息
    addPatientUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisPatient',
    //修改就诊信息
    editPatientUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisPatientByField',
	//新增检验单信息
    addTestFormUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddSingleLisTestForm',
    //修改检验单信息
    editTestFormUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_EditLisTestFormByField',
    //根据指定的样本号生成新样本号
    getNewSampleNoUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_GetNewSampleNoByOldSampleNo',
    //根据出生日期获得年龄、年龄单位、年龄描述
	getAgeInfoByBirthdayUrl:'/ServerWCF/LabStarCommonService.svc/GetPatientAge',
	//布局方式
	layout:'anchor',
	//每个组件的默认属性
	defaults:{ 
		anchor:'100%'
	},
	formtype:'show',
	
	//小组ID
    sectionId:null,
    //列表中选中的样本号
    curSampleNo:null,
    //是否只读
	isReadOnly:false,
    
    //主状态属性列表
    statusList:[{id:'1',name:'检验中'},{id:'2',name:'检验确认'},{id:'3',name:'审核'},{id:'-2',name:'作废'}],
    
    afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.on({
			afterIsAdd:function(){
				var GTestDate = JShell.System.Date.getDate(),
					GSampleNo = null;
				me.getNewSampleNoByOldSampleNo(me.sectionId,JShell.Date.toString(GTestDate,true),me.curSampleNo,function(data){
					if(data.success){
						GSampleNo = data.value;
					}else{
						JShell.Msg.error(data.msg);
					}
					me.getForm().setValues({
						LisTestForm_GTestDate:GTestDate,
						LisTestForm_MainTesterId:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
						LisTestForm_MainTester:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
						LisTestForm_GSampleNo:GSampleNo
					});
				});
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
	},
	//@overwrite 创建内部组件
	createItems:function(){
		var me = this;
		
		var items = [{
			xtype:'fieldset',
			title:'关键信息',
			itemId:'VitalInfo',
			collapsible:true,
			defaultType:'textfield',
			layout:'anchor',
			defaults:{
				anchor:'100%',
				labelWidth:60,
				labelAlign:'right'
			},
			items:me.createVitalInfoItems()
		},{
			xtype:'fieldset',
			title:'检验信息',
			itemId:'TestForm',
			collapsible:true,
			defaultType:'textfield',
			layout:'anchor',
			defaults:{
				anchor:'100%',
				labelWidth:60,
				labelAlign:'right'
			},
			items:me.createTestFormItems()
		},{
			xtype:'fieldset',
			title:'就诊信息',
			itemId:'Patient',
			collapsible:true,
			defaultType:'textfield',
			layout:'anchor',
			defaults:{
				anchor:'100%',
				labelWidth:60,
				labelAlign:'right'
			},
			items:me.createPatientItems()
		}];
		
		return items;
	},
	//创建关键信息组件
	createVitalInfoItems:function(){
		var me = this;
		var items = [{
			fieldLabel:'检测日期',name:'LisTestForm_GTestDate',
			xtype:'datefield',format:'Y-m-d'
		},{
			fieldLabel:'样&nbsp;&nbsp;本&nbsp;号',name:'LisTestForm_GSampleNo'
		},{
			fieldLabel:'条&nbsp;&nbsp;码&nbsp;号',name:'LisTestForm_BarCode'
		},{
			fieldLabel:'就诊类型',xtype:'uxCheckTrigger',
			name:'LisTestForm_LisPatient_SickType',
			className:'Shell.class.lts.sicktype.CheckGrid',
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('LBSickType_CName') : '');
					p.nextNode().setValue(record ? record.get('LBSickType_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'就诊类型ID',name:'LisTestForm_LisPatient_SickTypeID',hidden:true
		},{
			fieldLabel:'样本类型',xtype:'uxCheckTrigger',
			name:'LisTestForm_GSampleType',
			className:'Shell.class.lts.sampletype.CheckGrid',
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('LBSampleType_CName') : '');
					p.nextNode().setValue(record ? record.get('LBSampleType_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'样本类型ID',name:'LisTestForm_GSampleTypeID',hidden:true
		},{
			fieldLabel:'姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名',name:'LisTestForm_LisPatient_CName'
		},{
			fieldLabel:'病&nbsp;&nbsp;历&nbsp;号',name:'LisTestForm_LisPatient_PatNo'
		},{
			fieldLabel:'加急标识',name:'LisTestForm_UrgentState'
		},{
			fieldLabel:'送检目的',name:'LisTestForm_Testaim'
		},{
			fieldLabel:'样本备注',name:'LisTestForm_FormMemo'
		},{
			fieldLabel:'特殊标注',name:'LisTestForm_SampleSpecialDesc'
		}];
		
		return items;
	},
	//创建检验信息组件
	createTestFormItems:function(){
		var me = this;
		var items = [{
			fieldLabel: '采样时间', name: 'LisTestForm_CollectTime', itemId: 'LisTestForm_CollectTime',
			xtype:'datefield',format:'Y-m-d H:i:s'
		},{
			fieldLabel:'签收时间',name:'LisTestForm_InceptTime',
			xtype:'datefield',format:'Y-m-d H:i:s'
		},{
//			fieldLabel:'孕周',name:'',style:'color:red;',IsnotField:true
//		},{
//			fieldLabel:'胎儿样本类型',name:'',style:'color:red;',IsnotField:true
//		},{
			fieldLabel:'核收时间',name:'LisTestForm_ReceiveTime',itemId: 'LisTestForm_ReceiveTime',
			xtype:'datefield',format:'Y-m-d H:i:s',isSetReadOnly:true
		},{
			fieldLabel:'审核者',xtype:'uxCheckTrigger',
			name:'LisTestForm_Checker',readOnly:true,isSetReadOnly:true,
			className:'Shell.class.basic.user.CheckGrid',
			classConfig:{TSysCode:'1001001'},
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
					p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'审核者ID',name:'LisTestForm_CheckerID',hidden:true,readOnly:true,isSetReadOnly:true
		},{
			fieldLabel:'审核时间',name:'LisTestForm_CheckTime',readOnly:true,isSetReadOnly:true,
			xtype:'datefield',format:'Y-m-d H:i:s'
		},{
			fieldLabel:'检验者',xtype:'uxCheckTrigger',
			name:'LisTestForm_MainTester',
			className:'Shell.class.basic.user.CheckGrid',
			classConfig:{TSysCode:'1001001'},
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
					p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'检验者ID',name:'LisTestForm_MainTesterId',hidden:true
		},{
			fieldLabel:'检验备注',name:'LisTestForm_TestComment'
		},{
			fieldLabel:'检验评语',name:'LisTestForm_TestInfo'
		},{
			fieldLabel:'检验类型',name:'LisTestForm_TestType',//style:'color:red;',
				xtype: 'uxSimpleComboBox', data: [['1', '常规'], ['2', '急诊'], ['3', '质控']], value:'1'
		},{
			fieldLabel:'主键ID',name:'LisTestForm_Id',hidden:true
		},{
			fieldLabel:'检验单主状态',name:'LisTestForm_MainStatusID',hidden:true
		}];
		
		return items;
	},
	//创建就诊信息组件
	createPatientItems:function(){
		var me = this;
		var items = [{
			fieldLabel:'性别',xtype:'uxCheckTrigger',
			name:'LisTestForm_LisPatient_GenderName',
			className:'Shell.class.basic.enum.CheckGrid',
			classConfig:{EnumName:'GenderType'},
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('Name') : '');
					p.nextNode().setValue(record ? record.get('Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'性别ID',name:'LisTestForm_LisPatient_GenderID',hidden:true
		},{
			fieldLabel:'出生日期',xtype:'datefield',format:'Y-m-d H:i:s',
			name:'LisTestForm_LisPatient_Birthday',
			itemId:'LisTestForm_LisPatient_Birthday',
			listeners: {
				change: function (t, newValue, oldValue, o) {
					var newValueStr = JShell.Date.toString(newValue);
					var oldValueStr = JShell.Date.toString(oldValue);
					var today = JShell.Date.toString(new Date());
					if (new Date(newValueStr).getTime() > new Date(today).getTime()) {
						JShell.Msg.alert("请选择当前时间之前的时间！");
						this.setValue(oldValueStr);
						return;
					}
					var collectTime = me.getComponent('TestForm').getComponent('LisTestForm_CollectTime').getValue() || null,
						testTime = me.getComponent('TestForm').getComponent('LisTestForm_ReceiveTime').getValue() || new Date(),
						DataAddTime = JShell.Date.toString(new Date());
					collectTime = collectTime == null ? null : JShell.Date.toString(collectTime);
					testTime = JShell.Date.toString(testTime);
					me.getAgeInfoByBirthday(collectTime, testTime, DataAddTime,newValueStr, function (value) {
						me.getComponent('Patient').getComponent('LisTestForm_LisPatient_Age').setValue(value["Age"]);
						me.getComponent('Patient').getComponent('LisTestForm_LisPatient_AgeUnitID').setValue(value["AgeUnitID"]);
						me.getComponent('Patient').getComponent('LisTestForm_LisPatient_AgeUnitName').setValue(value["AgeUnitName"]);
						me.getComponent('Patient').getComponent('LisTestForm_LisPatient_AgeDesc').setValue(value["AgeDesc"]);
					});
				}
			}
		},{
			fieldLabel:'年龄',name:'LisTestForm_LisPatient_Age',itemId:'LisTestForm_LisPatient_Age',hidden:true
		},{
			fieldLabel:'年龄单位',xtype:'uxCheckTrigger',
			name:'LisTestForm_LisPatient_AgeUnitName',itemId:'LisTestForm_LisPatient_AgeUnitName',
			className:'Shell.class.basic.enum.CheckGrid',
			classConfig:{EnumName:'AgeUnitType'},hidden:true,
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('Name') : '');
					p.nextNode().setValue(record ? record.get('Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'年龄单位ID',name:'LisTestForm_LisPatient_AgeUnitID',itemId:'LisTestForm_LisPatient_AgeUnitID',hidden:true
		},{
			fieldLabel:'年龄描述',name:'LisTestForm_LisPatient_AgeDesc',itemId:'LisTestForm_LisPatient_AgeDesc',
		},{
			fieldLabel:'体重',name:'LisTestForm_LisPatient_PatWeight',hidden:true
		},{
			fieldLabel:'身高',name:'LisTestForm_LisPatient_PatHeight',hidden:true
		},{
			fieldLabel:'送检单位',name:'LisTestForm_Hospital',style:'color:red;',
			xtype:'uxSimpleComboBox',data:[],hidden:true
		},{
			fieldLabel:'床号',name:'LisTestForm_LisPatient_Bed'
		},{
			fieldLabel:'病区',xtype:'uxCheckTrigger',
			name:'LisTestForm_LisPatient_DistrictName',
			className:'Shell.class.basic.dept.CheckGrid',
			classConfig:{TSysCode:'1001102'},
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HRDeptIdentity_HRDept_CName') : '');
					p.nextNode().setValue(record ? record.get('HRDeptIdentity_HRDept_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'病区ID',name:'LisTestForm_LisPatient_DistrictID',hidden:true
		},{
			fieldLabel:'科室',xtype:'uxCheckTrigger',
			name:'LisTestForm_LisPatient_DeptName',
			className:'Shell.class.basic.dept.CheckGrid',
			classConfig:{TSysCode:'1001101'},//送检科室
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HRDeptIdentity_HRDept_CName') : '');
					p.nextNode().setValue(record ? record.get('HRDeptIdentity_HRDept_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'科室ID',name:'LisTestForm_LisPatient_DeptID',hidden:true
		},{
			fieldLabel:'医生',xtype:'uxCheckTrigger',
			name:'LisTestForm_LisPatient_DoctorName',
			className:'Shell.class.basic.user.CheckGrid',
			classConfig:{TSysCode:'1001003'},
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
					p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'医生ID',name:'LisTestForm_LisPatient_DoctorID',hidden:true
		},{
			fieldLabel:'诊断',xtype:'uxCheckTrigger',
			name:'LisTestForm_LisPatient_DiagName',
			className:'Shell.class.basic.diag.CheckGrid',
			listeners:{
				check:function(p,record){
					p.setValue(record ? record.get('LBDiag_CName') : '');
					p.nextNode().setValue(record ? record.get('LBDiag_Id') : '');
					p.close();
				}
			}
		},{
			fieldLabel:'诊断ID',name:'LisTestForm_LisPatient_DiagID',hidden:true
		},{
			fieldLabel:'临床HIS备注',name:'LisTestForm_LisPatient_HISComment'
		},{
			fieldLabel:'患者就诊信息ID',name:'LisTestForm_LisPatient_Id',hidden:true
		}];
		
		return items;
	},
	//@overwrite 创建数据字段
	getStoreFields:function(){
		var me = this,
			VitalInfo = me.getComponent('VitalInfo'),
			Patient = me.getComponent('Patient'),
			TestForm = me.getComponent('TestForm'),
			VitalInfoItems = VitalInfo.items.items || [],
			PatientItems = Patient.items.items || [],
			TestFormItems = TestForm.items.items || [],
			fields = [];
		
		for(var i in VitalInfoItems){
			if(VitalInfoItems[i].name && !VitalInfoItems[i].IsnotField){
				fields.push(VitalInfoItems[i].name);
			}
		}	
		for(var i in PatientItems){
			if(PatientItems[i].name && !PatientItems[i].IsnotField){
				fields.push(PatientItems[i].name);
			}
		}
		for(var i in TestFormItems){
			if(TestFormItems[i].name && !TestFormItems[i].IsnotField){
				fields.push(TestFormItems[i].name);
			}
		}
		
		return fields;
	},
	
	//@overwrite 保存按钮点击处理方法
	onSaveClick:function(){
		var me = this;
		
		if(me.isReadOnly){
			JShell.Msg.error("当前为只读模式，无法保存");
			return;
		}
		
		if(!me.sectionId){
			JShell.Msg.error("当前小组ID为空，无法保存");
			return;
		}

		if (me.formtype == 'show') {
			var status = "";
			//保存时需提示什么状态无法保存
			var values = me.getForm().getValues();
			for(var i =0;i<me.statusList.length;i++){
				if(values.LisTestForm_MainStatusID ==me.statusList[i].id ){
					status = me.statusList[i].name;
					break;
				}
			}
			JShell.Msg.error("当前检验单为"+status+"状态，无法保存");
			return;
		}

		if(!me.getForm().isValid()) return;
		
		if(me.formtype == 'add'){
			me.showMask(me.saveText);//显示遮罩层
			//新增检验单信息
			me.onAddTestFormInfo(false,function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					me.fireEvent('add',me,data.value.id);
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				}else{
					me.hideMask();//隐藏遮罩层
					if(data.code == 1){//样本号已经存在
						JShell.Msg.confirm({
							msg:data.msg + '</BR></BR>选择“确定”：自动生成样本号并保存</BR>选择“取消”：放弃此次保存！'
						},function(but){
							if (but != "ok") return;
							me.showMask(me.saveText);//显示遮罩层
							me.onAddTestFormInfo(true,function(data){
								me.hideMask();//隐藏遮罩层
								if(data.success){
									me.fireEvent('add',me,data.value.id);
									if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
								}else{
									JShell.Msg.error(data.msg);
								}
							});
					  });
					}else{
						JShell.Msg.error(data.msg);
					}
				}
			});
		}else{
			//患者就诊信息更新-可能存在问题（影响已发布报告内容的准确性）；
			me.showMask(me.saveText);//显示遮罩层
			//修改检验单信息
			me.onEditTestFormInfo(function(data){
				me.hideMask();//隐藏遮罩层
				if(data.success){
					var values = me.getForm().getValues();
					me.fireEvent('edit',me,values.LisTestForm_Id);
					if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				}else{
					JShell.Msg.error(data.msg);
				}
			});
		}
	},
	//新增检验单信息
	onAddTestFormInfo:function(isCreateSampleNo,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.addTestFormUrl,
			values = me.getForm().getValues(),
			entity = me.getTestFormInfo(),
			PatientInfo = me.getPatientInfo();
			
		//小组样本描述：GSampleInfo????
		//主状态-检验中
		entity.MainStatusID = 0;
		//样本来源-lis录入
		entity.iSource = 1;
		//检验过程状态-检验单生成
		entity.StatusID = 1;
		//小组属性
		entity.LBSection = {Id:me.sectionId,DataTimeStamp:[0,0,0,0,0,0,0,0]};
		//就诊信息
		//entity.LisPatient = {Id:PatID,DataTimeStamp:[0,0,0,0,0,0,0,0]};
		if(PatientInfo){
			entity.LisPatient = PatientInfo;
		}
		
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode({
			testForm:entity,
			isCreateSampleNo:isCreateSampleNo
		}),function(data){
			callback(data);
		});
	},
	//修改检验单信息
	onEditTestFormInfo:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.editTestFormUrl,
			values = me.getForm().getValues();
			
		//获取页面检验单信息	
		var entity = me.getTestFormInfo();
		entity.Id = values.LisTestForm_Id;
		//检验单-需要更新的字段
		//检测日期、小组检测编号（样本号）、样本号排序、条码号、样本类型、
		//姓名、病历号、加急标识、送检目的、检验样本备注、特殊样本标注
		//采样时间、签收时间、核收时间、审核者、审核时间
		//检验者、检验备注、检验评语
		//性别、年龄、年龄单位、年龄描述、病区、科室、主键ID
		var testFormFields = [
			//'GTestDate','GSampleNo','GSampleNoForOrder','BarCode','SickTypeID',
			//'GSampleTypeID','GSampleType','CName','PatNo','UrgentState','Testaim',
			//'FormMemo','SampleSpecialDesc','CollectTime','InceptTime','ReceiveTime',
			//'CheckerID','Checker','CheckTime','MainTesterId','MainTester','TestComment',
			//'TestInfo', 'GenderID', 'Age', 'AgeUnitID', 'AgeDesc', 'PatHeight', 'PatHeight',
			//'DistrictID', 'DeptID', 'Id'
		];
		if (entity.Id) {
			for (var e in entity) {
				testFormFields.push(e);
			}
		}
		
		var PatientInfo = me.getPatientInfo();
		var patientFields = [];
		if (PatientInfo) {
			entity.LisPatient = PatientInfo;
			testFormFields.push("LisPatient_Id");
		}
		//患者-需要更新的字段
		if (PatientInfo) {
			for (var o in PatientInfo) {
				patientFields.push(o);
			}
		}
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode({
			testForm:entity,
			testFormFields:testFormFields.join(','),
			patientFields:patientFields.join(',')
		}),function(data){
			callback(data);
		});
	},
	//获取页面患者就诊信息
	getPatientInfo:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {};
		
		//患者主键ID
		if(values.LisTestForm_LisPatient_Id){
			entity.Id = values.LisTestForm_LisPatient_Id;
		}
		//姓名
		if(values.LisTestForm_LisPatient_CName){
			entity.CName = values.LisTestForm_LisPatient_CName;
		} else {
			entity.CName = "";
		}
		//性别
		if(values.LisTestForm_LisPatient_GenderID){
			entity.GenderID = values.LisTestForm_LisPatient_GenderID;
			entity.GenderName = values.LisTestForm_LisPatient_GenderName;
		} else {
			entity.GenderID = null;
			entity.GenderName = "";
		}
		//出生日期
		if(values.LisTestForm_LisPatient_Birthday){
			entity.Birthday = JShell.Date.toServerDate(values.LisTestForm_LisPatient_Birthday);
		} else {
			entity.Birthday = null;
		}
		//年龄
		if (values.LisTestForm_LisPatient_Age) {
			entity.Age = values.LisTestForm_LisPatient_Age;
		} else {
			entity.Age = null;
		}
		//年龄单位
		if(values.LisTestForm_LisPatient_AgeUnitID){
			entity.AgeUnitID = values.LisTestForm_LisPatient_AgeUnitID;
			entity.AgeUnitName = values.LisTestForm_LisPatient_AgeUnitName;
		} else {
			entity.GenderID = null;
			entity.GenderName = "";
		}
		//年龄描述
		if(values.LisTestForm_LisPatient_AgeDesc){
			entity.AgeDesc = values.LisTestForm_LisPatient_AgeDesc;
		} else {
			entity.AgeDesc = "";
		}
		//体重
		if (values.LisTestForm_LisPatient_PatWeight) {
			entity.PatWeight = values.LisTestForm_LisPatient_PatWeight;
		} else {
			entity.PatWeight = null;
		}
		//身高
		if (values.LisTestForm_LisPatient_PatHeight) {
			entity.PatHeight = values.LisTestForm_LisPatient_PatHeight;
		} else {
			entity.PatHeight = null;
		}
		//病历号
		if(values.LisTestForm_LisPatient_PatNo){
			entity.PatNo = values.LisTestForm_LisPatient_PatNo;
		} else {
			entity.PatNo = "";
		}
		//床号
		if(values.LisTestForm_LisPatient_Bed){
			entity.Bed = values.LisTestForm_LisPatient_Bed;
		} else {
			entity.Bed = "";
		}
		//病区
		if(values.LisTestForm_LisPatient_DistrictID){
			entity.DistrictID = values.LisTestForm_LisPatient_DistrictID;
			entity.DistrictName = values.LisTestForm_LisPatient_DistrictName;
		} else {
			entity.DistrictID = null;
			entity.DistrictName = "";
		}
		//科室
		if(values.LisTestForm_LisPatient_DeptID){
			entity.DeptID = values.LisTestForm_LisPatient_DeptID;
			entity.DeptName = values.LisTestForm_LisPatient_DeptName;
		} else {
			entity.DeptID = null;
			entity.DeptName = "";
		}
		//医生
		if(values.LisTestForm_LisPatient_DoctorID){
			entity.DoctorID = values.LisTestForm_LisPatient_DoctorID;
			entity.DoctorName = values.LisTestForm_LisPatient_DoctorName;
		} else {
			entity.DoctorID = null;
			entity.DoctorName = "";
		}
		//就诊类型
		if(values.LisTestForm_LisPatient_SickTypeID){
			entity.SickTypeID = values.LisTestForm_LisPatient_SickTypeID;
			entity.SickType = values.LisTestForm_LisPatient_SickType;
		} else {
			entity.SickTypeID = null;
			entity.SickType = "";
		}
		//诊断
		if(values.LisTestForm_LisPatient_DiagID){
			entity.DiagID = values.LisTestForm_LisPatient_DiagID;
			entity.DiagName = values.LisTestForm_LisPatient_DiagName;
		} else {
			entity.DiagID = null;
			entity.DiagName = "";
		}
		//临床HIS备注
		if(values.LisTestForm_LisPatient_HISComment){
			entity.HISComment = values.LisTestForm_LisPatient_HISComment;
		} else {
			entity.HISComment = "";
		}
		
		//病人信息是否为空
		var isEmpty = true;
		for(var i in entity){
			if(entity[i]){
				isEmpty = false;
			}
		}
		if(isEmpty){
			entity = null;
		}
		
		return entity;
	},
	//获取页面检验单信息
	getTestFormInfo:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {};
		
		//=============关键信息=============
		//检测日期
		if (values.LisTestForm_GTestDate) {
			entity.GTestDate = JShell.Date.toServerDate(values.LisTestForm_GTestDate);
		}
		//小组检测编号（样本号）
		if(values.LisTestForm_GSampleNo){
			entity.GSampleNo = values.LisTestForm_GSampleNo;
		} else {
			entity.GSampleNo = "";
		}
		//样本号排序
		if(values.LisTestForm_GSampleNoForOrder){
			entity.GSampleNoForOrder = values.LisTestForm_GSampleNoForOrder;
		}
		//条码号
		if(values.LisTestForm_BarCode){
			entity.BarCode = values.LisTestForm_BarCode;
		} else {
			entity.BarCode = "";
		}
		//就诊类型
		if(values.LisTestForm_LisPatient_SickTypeID){
			entity.SickTypeID = values.LisTestForm_LisPatient_SickTypeID;
		} else {
			entity.SickTypeID = null;
		}
		//样本类型
		if(values.LisTestForm_GSampleType){
			entity.GSampleTypeID = values.LisTestForm_GSampleTypeID;
			entity.GSampleType = values.LisTestForm_GSampleType;
		} else {
			entity.GSampleTypeID = null;
			entity.GSampleType = "";
		}
		//姓名
		if(values.LisTestForm_LisPatient_CName){
			entity.CName = values.LisTestForm_LisPatient_CName;
		} else {
			entity.CName = "";
		}
		//病历号
		if(values.LisTestForm_LisPatient_PatNo){
			entity.PatNo = values.LisTestForm_LisPatient_PatNo;
		} else {
			entity.PatNo = "";
		}
		//加急标识
		if(values.LisTestForm_UrgentState){
			entity.UrgentState = values.LisTestForm_UrgentState;
		} else {
			entity.UrgentState = "";
		}
		//送检目的
		if(values.LisTestForm_Testaim){
			entity.Testaim = values.LisTestForm_Testaim;
		} else {
			entity.Testaim = "";
		}
		//检验样本备注
		if(values.LisTestForm_FormMemo){
			entity.FormMemo = values.LisTestForm_FormMemo;
		} else {
			entity.FormMemo = "";
		}
		//特殊样本标注
		if(values.LisTestForm_SampleSpecialDesc){
			entity.SampleSpecialDesc = values.LisTestForm_SampleSpecialDesc;
		} else {
			entity.SampleSpecialDesc = "";
		}
		
		//=============检验信息=============
		//采样时间
		if(values.LisTestForm_CollectTime){
			entity.CollectTime = JShell.Date.toServerDate(values.LisTestForm_CollectTime);
		} else {
			entity.CollectTime = null;
		}
		//签收时间
		if(values.LisTestForm_InceptTime){
			entity.InceptTime = JShell.Date.toServerDate(values.LisTestForm_InceptTime);
		} else {
			entity.InceptTime = null;
		}
		//核收时间
		if(values.LisTestForm_ReceiveTime){
			entity.ReceiveTime = JShell.Date.toServerDate(values.LisTestForm_ReceiveTime);
		} else {
			entity.ReceiveTime = null;
		}
		//审核者
		if (values.LisTestForm_CheckerID) {
			entity.CheckerID = values.LisTestForm_CheckerID;
			entity.Checker = values.LisTestForm_Checker;
		} else {
			entity.CheckerID = null;
			entity.Checker = "";
		}
		//审核时间
		if(values.LisTestForm_CheckTime){
			entity.CheckTime = JShell.Date.toServerDate(values.LisTestForm_CheckTime);
		} else {
			entity.CheckTime = null;
		}
		//检验者
		if (values.LisTestForm_MainTesterId) {
			entity.MainTesterId = values.LisTestForm_MainTesterId;
			entity.MainTester = values.LisTestForm_MainTester;
		} else {
			entity.MainTesterId = null;
			entity.MainTester = "";
		}
		//检验备注
		if(values.LisTestForm_TestComment){
			entity.TestComment = values.LisTestForm_TestComment;
		} else {
			entity.TestComment = "";
		}
		//检验评语
		if(values.LisTestForm_TestInfo){
			entity.TestInfo = values.LisTestForm_TestInfo;
		} else {
			entity.TestInfo = "";
		}
		//检验类型
		if (values.LisTestForm_TestType) {
			entity.TestType = values.LisTestForm_TestType;
		} else {
			entity.TestType = null;
		}
		//=============就诊信息=============
		//性别
		if (values.LisTestForm_LisPatient_GenderID) {
			entity.GenderID = values.LisTestForm_LisPatient_GenderID;
		} else {
			entity.GenderID = null;
		}
		//年龄
		if(values.LisTestForm_LisPatient_Age){
			entity.Age = values.LisTestForm_LisPatient_Age;
		} else {
			entity.Age = null;
		}
		//体重
		if (values.LisTestForm_LisPatient_PatWeight) {
			entity.PatWeight = values.LisTestForm_LisPatient_PatWeight;
		} else {
			entity.PatWeight = null;
		}
		//身高
		//if (values.LisTestForm_LisPatient_PatHeight) {
		//	entity.PatHeight = values.LisTestForm_LisPatient_PatHeight;
		//}
		//年龄单位
		if(values.LisTestForm_LisPatient_AgeUnitID){
			entity.AgeUnitID = values.LisTestForm_LisPatient_AgeUnitID;
		} else {
			entity.AgeUnitID = null;
		}
		//年龄描述
		if(values.LisTestForm_LisPatient_AgeDesc){
			entity.AgeDesc = values.LisTestForm_LisPatient_AgeDesc;
		} else {
			entity.AgeDesc = "";
		}
		//病区
		if(values.LisTestForm_LisPatient_DistrictID){
			entity.DistrictID = values.LisTestForm_LisPatient_DistrictID;
		} else {
			entity.DistrictID = null;
		}
		//科室
		if(values.LisTestForm_LisPatient_DeptID){
			entity.DeptID = values.LisTestForm_LisPatient_DeptID;
		} else {
			entity.DeptID = null;
		}
		
		return entity;
	},
	//@overwrite 返回数据处理方法
	changeResult:function(data){
		//检测日期
		data.LisTestForm_GTestDate = JShell.Date.getDate(data.LisTestForm_GTestDate);
		//采样时间
		data.LisTestForm_CollectTime = JShell.Date.getDate(data.LisTestForm_CollectTime);
		//签收时间
		data.LisTestForm_InceptTime = JShell.Date.getDate(data.LisTestForm_InceptTime);
		//核收时间
		data.LisTestForm_ReceiveTime = JShell.Date.getDate(data.LisTestForm_ReceiveTime);
		//审核时间
		data.LisTestForm_CheckTime = JShell.Date.getDate(data.LisTestForm_CheckTime);
		//出生日期
		data.LisTestForm_LisPatient_Birthday = JShell.Date.getDate(data.LisTestForm_LisPatient_Birthday);
		return data;
	},
	//根据指定的样本号生成新样本号
	getNewSampleNoByOldSampleNo:function(sectionID,testDate,curSampleNo,callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getNewSampleNoUrl;
		
		//保存到后台
		JShell.Server.post(url,Ext.JSON.encode({
			sectionID:sectionID,
			testDate:testDate,
			curSampleNo:curSampleNo
		}),function(data){
			callback(data);
		});
	},
	//内容置空
	clearData:function(){
		var me = this;
		me.callParent(arguments);
		//列表中选中的样本号置空
		me.curSampleNo = null;
	},
	/**数据项是否只读处理 -- 重写*/
	setReadOnly: function (bo) {
		var me = this,
			fields = me._thisfields,
			length = fields.length,
			field;

		for (var i = 0; i < length; i++) {
			field = fields[i];
			if (!field.locked) {
				if (Ext.typeOf(field.setReadOnly) == 'function') {
					if (!field.isSetReadOnly)
						field.setReadOnly(bo);
				}
			}
		}
	},
	//根据出生日期获得年龄、年龄单位、年龄描述
	getAgeInfoByBirthday: function (collectTime, testTime, DataAddTime, Birthday, CallBack) {
		var me = this,
			url = JShell.System.Path.ROOT + me.getAgeInfoByBirthdayUrl,
			collectTime = collectTime || null,
			testTime = testTime || null,
			DataAddTime = DataAddTime || null,
			Birthday = Birthday || null;
		if (!Birthday) return;
		url += "?birthday=" + Birthday;
		if (collectTime) url += "&collectTime=" + collectTime;
		if (testTime) url += "&testTime=" + testTime;
		if (DataAddTime) url += "&DataAddTime=" + DataAddTime;
		JShell.Server.get(url, function (res) {
			if (res.success) {
				CallBack(res.value);
			}
		});
	}
});