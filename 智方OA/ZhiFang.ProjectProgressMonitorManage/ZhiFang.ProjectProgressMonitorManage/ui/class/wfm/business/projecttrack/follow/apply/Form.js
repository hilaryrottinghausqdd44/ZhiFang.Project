/**
 * 项目跟踪申请表单
 * @author liangyl
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.follow.apply.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'项目跟踪申请表单',
    width:640,
    height:360,
    bodyPadding:10,

	/**获取数据服务路径*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContractFollow',
    /**修改服务地址*/
    editUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractFollowByField',
	
    /**布局方式*/
    layout:{type:'table',columns:3},
    /**每个组件的默认属性*/
    defaults:{labelWidth:85,width:220,labelAlign:'right'},
    
    /*本公司名称*/
    COMPNAME:'OurCorName',
    /**项目类别*/
    ProjectType: 'ContracType',
    /**项目性质*/
    ProjectAttr: '4801123276579782631',
//  ProjectAttr: '5147931795319426326',
   
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initListeners();
    },
    initComponent: function () {
        var me = this;
        me.buttonToolbarItems = ['->'];
		me.buttonToolbarItems.push({
			text:'保存',
			iconCls:'button-save',
			tooltip:'保存',
			handler:function(){
				me.onSave(true);
			}
		},'reset');
        me.callParent(arguments);
    },
    /**@overwrite 创建内部组件*/
    createItems: function () {
        var me = this;
		
		var items = [{
			fieldLabel:'项目名称',name:'PContractFollow_Name',
			emptyText:'必填项',allowBlank:false,
			colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'项目编号',name:'PContractFollow_ContractNumber',itemId:'PContractFollow_ContractNumber',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'客户选择',name:'PContractFollow_PClientName',itemId:'PContractFollow_PClientName',
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'销售负责人',name:'PContractFollow_Principal',itemId:'PContractFollow_Principal',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'付款单位',name:'PContractFollow_PayOrg',itemId:'PContractFollow_PayOrg',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			colspan:2,width:me.defaults.width * 2	
		},{
			fieldLabel:'签署人',name:'PContractFollow_SignMan',itemId:'PContractFollow_SignMan',
			xtype:'uxCheckTrigger',
	        className: 'Shell.class.sysbase.user.role.CheckGrid',
			classConfig: {
				checkOne: true,
				searchInfoWidth: '70%',
				RoleHREmployeeCName: "'总经理','副总经理'"
			},
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'软件价格',name:'PContractFollow_Software',itemId:'PContractFollow_Software',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'硬件价格',name:'PContractFollow_Hardware',itemId:'PContractFollow_Hardware',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'签署日期',name:'PContractFollow_SignDate',
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'服务费',name:'PContractFollow_ContractServiceCharge',itemId:'PContractFollow_ContractServiceCharge',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'项目总额',name:'PContractFollow_Amount',itemId:'PContractFollow_Amount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'有偿服务',name:'PContractFollow_PaidServiceStartTime',
			xtype:'datefield',format:'Y-m-d',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'其它费用',name:'PContractFollow_MiddleFee',itemId:'PContractFollow_MiddleFee',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'项目份数',name:'PContractFollow_Emphases',itemId:'PContractFollow_Emphases',
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
		    boxLabel:'是否开具发票',name:'PContractFollow_IsInvoices',itemId:'PContractFollow_IsInvoices',
		    xtype:'checkbox',checked:true,fieldLabel:'&nbsp;',labelSeparator:'',
		    colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'单向仪器数',name:'PContractFollow_EquipOneWayCount',itemId:'PContractFollow_EquipOneWayCount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'双向仪器数',name:'PContractFollow_EquipTwoWayCount',itemId:'PContractFollow_EquipTwoWayCount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		}, {
		    fieldLabel: '合同性质',
		    name: 'PContractFollow_PContractAttrName',
		    xtype: 'uxCheckTrigger',
		    itemId: 'PContractFollow_PContractAttrName',
		    className: 'Shell.class.wfm.dict.CheckGrid',
		    classConfig: {
		        title: '合同性质选择',
		        defaultWhere: "pdict.Id='" + this.ProjectAttr + "'"
		    },
		    colspan: 1,
		    	readOnly: true,
				locked: true,
		    width: me.defaults.width * 1
		}, {
			fieldLabel:'本公司名称',name:'PContractFollow_Compname',itemId:'PContractFollow_Compname',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title:'本公司名称选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + me.COMPNAME + "'"
			},
			colspan:2,width:me.defaults.width * 2
		}
        , {
			fieldLabel: '项目类型',
			name: 'PContractFollow_Content',
			xtype: 'uxCheckTrigger',
			itemId: 'PContractFollow_Content',emptyText:'必填项',
			className: 'Shell.class.wfm.dict.CheckGrid',allowBlank:false,
			classConfig: {
				title: '项目类型选择',
				defaultWhere: "pdict.PDictType.DictTypeCode='" + this.ProjectType + "'"
			},
			colspan: 1,
			width: me.defaults.width * 1
		},{
        	fieldLabel:'项目类型ID',name:'PContractFollow_ContentID',itemId:'PContractFollow_ContentID',hidden:true
		}, {
		    fieldLabel: '项目性质ID', name: 'PContractFollow_PContractAttrID', itemId: 'PContractFollow_PContractAttrID', hidden: true
		},{
			fieldLabel:'服务开始时间',name:'PContractFollow_ServerContractStartDateTime',
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'服务结束时间',name:'PContractFollow_ServerContractEndDateTime',
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		}, {
			fieldLabel:'计划签署时间',name:'PContractFollow_PlanSignDateTime',
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'原项目总额',name:'PContractFollow_OriginalMoneyTotal',itemId:'PContractFollow_OriginalMoneyTotal',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'服务费比列',name:'PContractFollow_ServerChargeRatio',itemId:'PContractFollow_ServerChargeRatio',
			readOnly: true,locked: true,colspan:1,width:150}
		,{
			xtype: 'label',
			itemId:'labText',
			style: {
				marginLeft: '-70px'
			},
	        text: '%'
		},{
            fieldLabel:'备注',name:'PContractFollow_Comment',itemId:'PContractFollow_Comment',
            xtype:'textarea',height:80,
            colspan:3,width:me.defaults.width * 3
		},{
			fieldLabel:'主键ID',name:'PContractFollow_Id',hidden:true
		},{
			fieldLabel:'客户主键ID',name:'PContractFollow_PClientID',itemId:'PContractFollow_PClientID',hidden:true
		},{
        	fieldLabel:'付款单位ID',name:'PContractFollow_PayOrgID',itemId:'PContractFollow_PayOrgID',hidden:true
		},{
        	fieldLabel:'销售负责人主键ID',name:'PContractFollow_PrincipalID',itemId:'PContractFollow_PrincipalID',hidden:true
		},{
        	fieldLabel:'签署人ID',name:'PContractFollow_SignManID',itemId:'PContractFollow_SignManID',hidden:true
		},{
			fieldLabel:'本公司ID',name:'PContractFollow_CompnameID',itemId:'PContractFollow_CompnameID',hidden:true
        },{
        	fieldLabel:'状态',name:'PContractFollow_ContractStatus',itemId:'PContractFollow_ContractStatus',hidden:true
        },{
        	fieldLabel:'仪器清单',name:'PContractFollow_LinkEquipInfoListHTML',itemId:'PContractFollow_LinkEquipInfoListHTML',hidden:true
        },{
        	fieldLabel:'采购说明Html',name:'PContractFollow_PurchaseDescHTML',itemId:'PContractFollow_PurchaseDescHTML',hidden:true
        },{
		    fieldLabel: '合同性质ID', name: 'PContract_PContractAttrID', itemId: 'PContract_PContractAttrID', hidden: true
		}];
        
		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.PContractFollow_PaidServiceStartTime = JShell.Date.getDate(data.PContractFollow_PaidServiceStartTime);
		data.PContractFollow_SignDate = JShell.Date.getDate(data.PContractFollow_SignDate);
		data.PContractFollow_IsInvoices = data.PContractFollow_IsInvoices == '是' ? true : false;
		data.PContractFollow_ServerContractStartDateTime = JShell.Date.getDate(data.PContractFollow_ServerContractStartDateTime);
		data.PContractFollow_ServerContractEndDateTime = JShell.Date.getDate(data.PContractFollow_ServerContractEndDateTime);
		data.PContractFollow_PlanSignDateTime = JShell.Date.getDate(data.PContractFollow_PlanSignDateTime);
		return data;
	},
	/**初始化监听*/
	initListeners:function(){
		var me = this;
		
		//客户
		var PClientName = me.getComponent('PContractFollow_PClientName'),
			PClientID = me.getComponent('PContractFollow_PClientID');
			
		PClientName.on({
			check: function(p, record) {
				PClientName.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			}
		});
		
		//付款单位
		var PayOrg = me.getComponent('PContractFollow_PayOrg'),
			PayOrgID = me.getComponent('PContractFollow_PayOrgID');
			
		PayOrg.on({
			check: function(p, record) {
				PayOrg.setValue(record ? record.get('PClient_Name') : '');
				PayOrgID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			}
		});
		
		//销售负责人
		var Principal = me.getComponent('PContractFollow_Principal'),
			PrincipalID = me.getComponent('PContractFollow_PrincipalID');
		
		Principal.on({
			check: function(p, record) {
				Principal.setValue(record ? record.get('HREmployee_CName') : '');
				PrincipalID.setValue(record ? record.get('HREmployee_Id') : '');
				p.close();
			}
		});
		
		//签署人
		var SignMan = me.getComponent('PContractFollow_SignMan'),
			SignManID = me.getComponent('PContractFollow_SignManID');
		
		SignMan.on({
			check: function(p, record) {
				SignMan.setValue(record ? record.get('RBACEmpRoles_HREmployee_CName') : '');
				SignManID.setValue(record ? record.get('RBACEmpRoles_HREmployee_Id') : '');
				p.close();
			}
		});
		
		//本公司
		var Compname = me.getComponent('PContractFollow_Compname'),
			CompnameID = me.getComponent('PContractFollow_CompnameID');
		
		Compname.on({
			check: function(p, record) {
				Compname.setValue(record ? record.get('PDict_CName') : '');
				CompnameID.setValue(record ? record.get('PDict_Id') : '');
				p.close();
			}
		});
	    var PContractAttrName = me.getComponent('PContractFollow_PContractAttrName'),
			PContractAttrID = me.getComponent('PContractFollow_PContractAttrID');

	    PContractAttrName.on({
		    check: function (p, record) {
		        PContractAttrName.setValue(record ? record.get('PDict_CName') : '');
		        PContractAttrID.setValue(record ? record.get('PDict_Id') : '');
		        p.close();
		    }
		});
		
		//项目类别
		var Content = me.getComponent('PContractFollow_Content'),
		ContentID = me.getComponent('PContractFollow_ContentID');
		Content.on({
			check: function(p, record) {
				Content.setValue(record ? record.get('PDict_CName') : '');
				ContentID.setValue(record ? record.get('PDict_Id') : '');
				p.close();
			}
		});
		
		//价格联动
		var Software = me.getComponent('PContractFollow_Software'),
			ContractServiceCharge = me.getComponent('PContractFollow_ContractServiceCharge'),
			Hardware = me.getComponent('PContractFollow_Hardware');
			
		Software.on({change:function(){me.onPriceChange();}});
		Hardware.on({change:function(){me.onPriceChange();}});
		ContractServiceCharge.on({change:function(){me.onPriceChange();}});

        var OriginalMoneyTotal = me.getComponent('PContractFollow_OriginalMoneyTotal'),
			Amount = me.getComponent('PContractFollow_Amount');
        OriginalMoneyTotal.on({
        	change:function(){me.onServerChargeRatioChange();}
        });
        Amount.on({
        	change:function(){me.onServerChargeRatioChange();}
        });
	},
	/**价格变化处理*/
	onPriceChange:function(){
		var me = this,
			Software = me.getComponent('PContractFollow_Software'),
			Hardware = me.getComponent('PContractFollow_Hardware'),
            ContractServiceCharge = me.getComponent('PContractFollow_ContractServiceCharge'),
			Amount = me.getComponent('PContractFollow_Amount');
			
		var sPrice = Software.getValue() || 0,
			hPrice = Hardware.getValue() || 0;
			mPrice = ContractServiceCharge.getValue() || 0;
			
		Amount.setValue(sPrice + hPrice+mPrice );
	},
	/**服务费比列计算变化*/
	onServerChargeRatioChange:function(){
		var me = this,
			OriginalMoneyTotal = me.getComponent('PContractFollow_OriginalMoneyTotal'),
			ServerChargeRatio = me.getComponent('PContractFollow_ServerChargeRatio');
			Amount = me.getComponent('PContractFollow_Amount');
		var contractoriginalmoneytotal = OriginalMoneyTotal.getValue() || 0,
			contractamount = Amount.getValue() || 0;
			var count=0;
		if(contractamount!=0 && contractoriginalmoneytotal!=0 ){
		    count=(contractamount / contractoriginalmoneytotal)*100;
		}
		//.toFixed(2)
		ServerChargeRatio.setValue(count.toFixed(0));
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		if(!JShell.System.Cookie.map.USERID){
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
			
		var entity = {
			IsUse:true,
			ApplyManID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			ApplyMan:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			Name:values.PContractFollow_Name,//项目跟踪名称
			ContractNumber:values.PContractFollow_ContractNumber,//项目跟踪编号
			Principal:values.PContractFollow_Principal,//销售负责人
			PayOrg:values.PContractFollow_PayOrg,//付款单位
			Software:values.PContractFollow_Software,//软件价格
			Hardware:values.PContractFollow_Hardware,//硬件价格
			MiddleFee:values.PContractFollow_MiddleFee,//其它费用
			Amount:values.PContractFollow_Amount,//项目跟踪总额
			Compname: values.PContractFollow_Compname,//本公司名称
			IsInvoices:values.PContractFollow_IsInvoices ? '是' : '否',//是否开具发票
			Comment:values.PContractFollow_Comment,//备注
			PayOrgID:values.PContractFollow_PayOrgID,//付款单位ID
			PrincipalID:values.PContractFollow_PrincipalID,//销售负责人主键ID
			CompnameID: values.PContractFollow_CompnameID,//本公司ID
			ContractStatus:1,//状态
			EquipOneWayCount:values.PContractFollow_EquipOneWayCount, //单项仪器个数
			EquipTwoWayCount:values.PContractFollow_EquipTwoWayCount, //双向仪器个数
			LinkEquipInfoListHTML:values.PContractFollow_LinkEquipInfoListHTML,
			PurchaseDescHTML:values.PContractFollow_PurchaseDescHTML,
			ContractServiceCharge:values.PContractFollow_ContractServiceCharge,
			Emphases:values.PContractFollow_Emphases
		};
		if(values.PContractFollow_PaidServiceStartTime)entity.PaidServiceStartTime=JShell.Date.toServerDate(values.PContractFollow_PaidServiceStartTime);//有偿服务时间

		if(values.PContractFollow_PClientID){
			entity.PClientID=values.PContractFollow_PClientID; //客户
			entity.PClientName=values.PContractFollow_PClientName;
		}
		if (values.PContractFollow_PContractAttrID) {
		    entity.PContractAttrName = values.PContractFollow_PContractAttrName;//合同性质名称
		    entity.PContractAttrID = values.PContractFollow_PContractAttrID;//合同性质ID
		}
		if(values.PContractFollow_ContentID){
			entity.Content=values.PContractFollow_Content;//项目类别
			entity.ContentID=values.PContractFollow_ContentID;//项目类别
		}

		if(values.PContractFollow_SignDate){
			entity.SignDate=JShell.Date.toServerDate(values.PContractFollow_SignDate);//签署日期
		}
		if(values.PContractFollow_SignMan){
			entity.SignMan=values.PContractFollow_SignMan;//签署人
			entity.SignManID=values.PContractFollow_SignManID;//签署人
		}
		//所属部门
		var  DeptID=JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
	    var DeptName=JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
		if(DeptID){
			entity.DeptID=DeptID;
		}
		if(DeptName){
			entity.DeptName=DeptName;
		}
		var Sysdate = JcallShell.System.Date.getDate();
		var DataAddTime = JcallShell.Date.toString(Sysdate);
		if(JShell.Date.toServerDate(DataAddTime)){
			entity.DataAddTime=JShell.Date.toServerDate(DataAddTime);//申请时间
		}
		
		if(values.PContractFollow_ServerContractStartDateTime){
			entity.ServerContractStartDateTime=JShell.Date.toServerDate(values.PContractFollow_ServerContractStartDateTime);//
		}
		if(values.PContractFollow_ServerContractEndDateTime){
			entity.ServerContractEndDateTime=JShell.Date.toServerDate(values.PContractFollow_ServerContractEndDateTime);//到期时间
		}
		if(values.PContractFollow_PlanSignDateTime){
			entity.PlanSignDateTime=JShell.Date.toServerDate(values.PContractFollow_PlanSignDateTime);//签署日期
		}
		if(values.PContractFollow_OriginalMoneyTotal){
			entity.OriginalMoneyTotal=values.PContractFollow_OriginalMoneyTotal;
		}	
		if(values.PContractFollow_ServerChargeRatio){
			entity.ServerChargeRatio=values.PContractFollow_ServerChargeRatio;
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			'Name','ContractNumber','PClientName','Principal',
			'PayOrg','PaidServiceStartTime','Software','Hardware',
			'SignDate','MiddleFee','Amount','SignMan','PContractAttrName', 'PContractAttrID',
			'Compname','IsInvoices','Comment','PClientID',
			'PayOrgID','PrincipalID','SignManID','CompnameID',
			'Id','ContractStatus','EquipOneWayCount','EquipTwoWayCount',
			'Content', 'ContractServiceCharge', 'Emphases', 'ContentID', 
			'ServerContractStartDateTime','ServerContractEndDateTime',
			'OriginalMoneyTotal','ServerChargeRatio','PlanSignDateTime'
		];
		
		entity.fields = fields.join(',');
		if(values.PContractFollow_LinkEquipInfoListHTML){
			entity.fields+=',LinkEquipInfoListHTML';
		}
		if(values.PContractFollow_PurchaseDescHTML){
			entity.fields+=',PurchaseDescHTML';
		}
		entity.entity.Id = values.PContractFollow_Id;
		return entity;
	},
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.getForm().getValues();
		
		if(!me.getForm().isValid()){
			me.fireEvent('isValid',me);
			return;
		}
		me.onSaveClick();
	},
	/**校验项目跟踪编号是否不重复*/
	isValidContractNumber:function(callback,ContractNumber){
		var me = this,
			values = me.getForm().getValues(),
			url = '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowByHQL';
		
		url = JShell.System.Path.getRootUrl(url);
		var where = "pcontractfollow.ContractNumber='" + values.PContractFollow_ContractNumber + "'";
		//修改时判断项目跟踪编号是否重复
		if(ContractNumber){
			where +=' and pcontractfollow.Id!='+ContractNumber;
		}
		url += '?fields=Id&where=' + where;
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value && data.value.count > 0){
					JShell.Msg.error("项目跟踪编号为" + (values.PContractFollow_ContractNumber || ' ') + "的项目跟踪已经存在，请换一个项目跟踪编号！");
				}else{
					callback();
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
		    var PContractAttrName = me.getComponent('PContractFollow_PContractAttrName'),
				PContractAttrID = me.getComponent('PContractFollow_PContractAttrID');
			me.getContractAttr(function(data){
				if(data && data.value){
					var idval=data.value.list[0].PDict_Id;
					var nameval=data.value.list[0].PDict_CName;
					PContractAttrID.setValue(idval);
			        PContractAttrName.setValue(nameval);
				}
			});
			
			var Compname = me.getComponent('PContractFollow_Compname');
			var CompnameID = me.getComponent('PContractFollow_CompnameID');
			
			//默认新公司,写死(字典内容)
			CompnameID.setValue('5446797576966742329');
			Compname.setValue('智方（北京）科技发展有限公司');
			
			//销售负责人 默认登陆者
			var Principal = me.getComponent('PContractFollow_Principal');
			var PrincipalID = me.getComponent('PContractFollow_PrincipalID');
			PrincipalID.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
			Principal.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
			
		} else {
			me.getForm().setValues(me.lastData);
		}
	},
	/**获取合同性质*/
	getContractAttr:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?isPlanish=true';
		url += '&fields=PDict_CName,PDict_Id&where=pdict.Id='+me.ProjectAttr;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});