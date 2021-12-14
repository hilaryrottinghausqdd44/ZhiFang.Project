/**
 * 合同申请表单
 * @author Jcall
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.contract.apply.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'合同申请表单',
    width:640,
    height:360,
    bodyPadding:10,

	/**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchPContractById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddPContract',
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdatePContractStatus',
	
    /**布局方式*/
    layout:{type:'table',columns:3},
    /**每个组件的默认属性*/
    defaults:{labelWidth:85,width:220,labelAlign:'right'},
    
    /*本公司名称*/
    COMPNAME:'OurCorName',
    /**项目类别*/
    ProjectType: 'ContracType',
    /**项目性质*/
    ProjectAttr: 'ContracAttr',
    /*是否有暂存按钮*/
    hastempSave:true,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initListeners();
    },
    initComponent: function () {
        var me = this;
        me.buttonToolbarItems = ['->'];
        if(me.hastempSave){
        	me.buttonToolbarItems.push({
				text:'暂存',
				iconCls:'button-save',
				tooltip:'暂存',
				handler:function(){
					me.onSave(false);
				}
			})
        }
		me.buttonToolbarItems.push({
			text:'提交',
			iconCls:'button-save',
			tooltip:'提交',
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
			fieldLabel:'合同名称',name:'PContract_Name',
			emptyText:'必填项',allowBlank:false,
			colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'合同编号',name:'PContract_ContractNumber',itemId:'PContract_ContractNumber',
			//emptyText:'必填项',allowBlank:false,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'客户选择',name:'PContract_PClientName',itemId:'PContract_PClientName',
			//emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'销售负责人',name:'PContract_Principal',itemId:'PContract_Principal',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'付款单位',name:'PContract_PayOrg',itemId:'PContract_PayOrg',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			colspan:2,width:me.defaults.width * 2	
		},{
			fieldLabel:'签署人',name:'PContract_SignMan',itemId:'PContract_SignMan',
//			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',
//			className:'Shell.class.sysbase.user.CheckApp',
	        className: 'Shell.class.sysbase.user.role.CheckGrid',
			classConfig: {
				checkOne: true,
				searchInfoWidth: '70%',
				RoleHREmployeeCName: "'总经理','副总经理'"
			},
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'软件价格',name:'PContract_Software',itemId:'PContract_Software',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'硬件价格',name:'PContract_Hardware',itemId:'PContract_Hardware',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'签署日期',name:'PContract_SignDate',
//			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'服务费',name:'PContract_ContractServiceCharge',itemId:'PContract_ContractServiceCharge',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'合同总额',name:'PContract_Amount',itemId:'PContract_Amount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'有偿服务',name:'PContract_PaidServiceStartTime',
			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format:'Y-m-d',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'其它费用',name:'PContract_MiddleFee',itemId:'PContract_MiddleFee',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'合同份数',name:'PContract_Emphases',itemId:'PContract_Emphases',
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
		    boxLabel:'是否开具发票',name:'PContract_IsInvoices',itemId:'PContract_IsInvoices',
		    xtype:'checkbox',checked:true,fieldLabel:'&nbsp;',labelSeparator:'',
		    colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'单向仪器数',name:'PContract_EquipOneWayCount',itemId:'PContract_EquipOneWayCount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'双向仪器数',name:'PContract_EquipTwoWayCount',itemId:'PContract_EquipTwoWayCount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		}, {
		    fieldLabel: '合同性质',
		    name: 'PContract_PContractAttrName',
		    xtype: 'uxCheckTrigger',
		    itemId: 'PContract_PContractAttrName',
		    className: 'Shell.class.wfm.dict.CheckGrid',
		    classConfig: {
		        title: '合同性质选择',
		        defaultWhere: "pdict.BDictType.DictTypeCode='" + this.ProjectAttr + "'"
		    },
		    colspan: 1,
		    width: me.defaults.width * 1
		}, {
			fieldLabel:'本公司名称',name:'PContract_Compname',itemId:'PContract_Compname',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title:'本公司名称选择',
				defaultWhere:"pdict.BDictType.DictTypeCode='" + me.COMPNAME + "'"
			},
			colspan:2,width:me.defaults.width * 2
		}
        , {
			fieldLabel: '合同类型',
			name: 'PContract_Content',
			xtype: 'uxCheckTrigger',
			itemId: 'PContract_Content',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '合同类型选择',
				defaultWhere: "pdict.BDictType.DictTypeCode='" + this.ProjectType + "'"
			},
			colspan: 1,
			width: me.defaults.width * 1
		},{
        	fieldLabel:'合同类型ID',name:'PContract_ContentID',itemId:'PContract_ContentID',hidden:true
		}, {
		    fieldLabel: '合同性质ID', name: 'PContract_PContractAttrID', itemId: 'PContract_PContractAttrID', hidden: true
		},{
			fieldLabel:'服务开始时间',name:'PContract_ServerContractStartDateTime',
//			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'服务结束时间',name:'PContract_ServerContractEndDateTime',
//			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		}, {
			fieldLabel:'计划签署时间',name:'PContract_PlanSignDateTime',
//			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'原合同总额',name:'PContract_OriginalMoneyTotal',itemId:'PContract_OriginalMoneyTotal',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'服务费比列',name:'PContract_ServerChargeRatio',itemId:'PContract_ServerChargeRatio',
			readOnly: true,locked: true,colspan:1,width:150}
		,{
			xtype: 'label',
			itemId:'labText',
			style: {
				marginLeft: '-70px'
			},
	        text: '%'
//	        margin: '0 0 0 0'
		},{
            fieldLabel:'备注',name:'PContract_Comment',itemId:'PContract_Comment',
            xtype:'textarea',height:80,
            colspan:3,width:me.defaults.width * 3
		},{
			fieldLabel:'主键ID',name:'PContract_Id',hidden:true
		},{
			fieldLabel:'客户主键ID',name:'PContract_PClientID',itemId:'PContract_PClientID',hidden:true
		},{
        	fieldLabel:'付款单位ID',name:'PContract_PayOrgID',itemId:'PContract_PayOrgID',hidden:true
		},{
        	fieldLabel:'销售负责人主键ID',name:'PContract_PrincipalID',itemId:'PContract_PrincipalID',hidden:true
		},{
        	fieldLabel:'签署人ID',name:'PContract_SignManID',itemId:'PContract_SignManID',hidden:true
		},{
			fieldLabel:'本公司ID',name:'PContract_CompnameID',itemId:'PContract_CompnameID',hidden:true
        },{
        	fieldLabel:'状态',name:'PContract_ContractStatus',itemId:'PContract_ContractStatus',hidden:true
        },{
        	fieldLabel:'仪器清单',name:'PContract_LinkEquipInfoListHTML',itemId:'PContract_LinkEquipInfoListHTML',hidden:true
        },{
        	fieldLabel:'采购说明Html',name:'PContract_PurchaseDescHTML',itemId:'PContract_PurchaseDescHTML',hidden:true
        }];
        
		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.PContract_PaidServiceStartTime = JShell.Date.getDate(data.PContract_PaidServiceStartTime);
		data.PContract_SignDate = JShell.Date.getDate(data.PContract_SignDate);
		data.PContract_IsInvoices = data.PContract_IsInvoices == '是' ? true : false;
		data.PContract_ServerContractStartDateTime = JShell.Date.getDate(data.PContract_ServerContractStartDateTime);
		data.PContract_ServerContractEndDateTime = JShell.Date.getDate(data.PContract_ServerContractEndDateTime);
		data.PContract_PlanSignDateTime = JShell.Date.getDate(data.PContract_PlanSignDateTime);
		return data;
	},
	/**初始化监听*/
	initListeners:function(){
		var me = this;
		
		//客户
		var PClientName = me.getComponent('PContract_PClientName'),
			PClientID = me.getComponent('PContract_PClientID');
			
		PClientName.on({
			check: function(p, record) {
				PClientName.setValue(record ? record.get('PClient_Name') : '');
				PClientID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			}
		});
		
		//付款单位
		var PayOrg = me.getComponent('PContract_PayOrg'),
			PayOrgID = me.getComponent('PContract_PayOrgID');
			
		PayOrg.on({
			check: function(p, record) {
				PayOrg.setValue(record ? record.get('PClient_Name') : '');
				PayOrgID.setValue(record ? record.get('PClient_Id') : '');
				p.close();
			}
		});
		
		//销售负责人
		var Principal = me.getComponent('PContract_Principal'),
			PrincipalID = me.getComponent('PContract_PrincipalID');
		
		Principal.on({
			check: function(p, record) {
				Principal.setValue(record ? record.get('HREmployee_CName') : '');
				PrincipalID.setValue(record ? record.get('HREmployee_Id') : '');
				p.close();
			}
		});
		
		//签署人
		var SignMan = me.getComponent('PContract_SignMan'),
			SignManID = me.getComponent('PContract_SignManID');
		
		SignMan.on({
			check: function(p, record) {
				SignMan.setValue(record ? record.get('RBACEmpRoles_HREmployee_CName') : '');
				SignManID.setValue(record ? record.get('RBACEmpRoles_HREmployee_Id') : '');
				p.close();
			}
		});
		
		//本公司
		var Compname = me.getComponent('PContract_Compname'),
			CompnameID = me.getComponent('PContract_CompnameID');
		
		Compname.on({
			check: function(p, record) {
				Compname.setValue(record ? record.get('BDict_CName') : '');
				CompnameID.setValue(record ? record.get('BDict_Id') : '');
				p.close();
			}
		});
	    var PContractAttrName = me.getComponent('PContract_PContractAttrName'),
			PContractAttrID = me.getComponent('PContract_PContractAttrID');

	    PContractAttrName.on({
		    check: function (p, record) {
		        PContractAttrName.setValue(record ? record.get('BDict_CName') : '');
		        PContractAttrID.setValue(record ? record.get('BDict_Id') : '');
		        p.close();
		    }
		});
		
		//项目类别
		var Content = me.getComponent('PContract_Content'),
		ContentID = me.getComponent('PContract_ContentID');
		Content.on({
			check: function(p, record) {
				Content.setValue(record ? record.get('BDict_CName') : '');
				ContentID.setValue(record ? record.get('BDict_Id') : '');
				p.close();
			}
		});
		
		//价格联动
		var Software = me.getComponent('PContract_Software'),
			ContractServiceCharge = me.getComponent('PContract_ContractServiceCharge'),
			Hardware = me.getComponent('PContract_Hardware');
//			MiddleFee = me.getComponent('PContract_MiddleFee');
			
		Software.on({change:function(){me.onPriceChange();}});
		Hardware.on({change:function(){me.onPriceChange();}});
		ContractServiceCharge.on({change:function(){me.onPriceChange();}});
//		MiddleFee.on({change:function(){me.onPriceChange();}});

        /*服务费比列计算 @author liangyl  @version 2017-08-01 */
        var OriginalMoneyTotal = me.getComponent('PContract_OriginalMoneyTotal'),
			Amount = me.getComponent('PContract_Amount');
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
			Software = me.getComponent('PContract_Software'),
			Hardware = me.getComponent('PContract_Hardware'),
//			MiddleFee = me.getComponent('PContract_MiddleFee'),
            ContractServiceCharge = me.getComponent('PContract_ContractServiceCharge'),
			Amount = me.getComponent('PContract_Amount');
			
		var sPrice = Software.getValue() || 0,
			hPrice = Hardware.getValue() || 0;
			mPrice = ContractServiceCharge.getValue() || 0;
			
		Amount.setValue(sPrice + hPrice+mPrice );
	},
	/**服务费比列计算变化*/
	onServerChargeRatioChange:function(){
		var me = this,
			OriginalMoneyTotal = me.getComponent('PContract_OriginalMoneyTotal'),
			ServerChargeRatio = me.getComponent('PContract_ServerChargeRatio');
			Amount = me.getComponent('PContract_Amount');
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
			Name:values.PContract_Name,//合同名称
			ContractNumber:values.PContract_ContractNumber,//合同编号
//			PClientName:values.PContract_PClientName,//客户
			Principal:values.PContract_Principal,//销售负责人
			PayOrg:values.PContract_PayOrg,//付款单位
			PaidServiceStartTime:JShell.Date.toServerDate(values.PContract_PaidServiceStartTime),//有偿服务时间
			Software:values.PContract_Software,//软件价格
			Hardware:values.PContract_Hardware,//硬件价格
//			SignDate:JShell.Date.toServerDate(values.PContract_SignDate),//签署日期
			MiddleFee:values.PContract_MiddleFee,//其它费用
			Amount:values.PContract_Amount,//合同总额
//			SignMan:values.PContract_SignMan,//签署人
			Compname: values.PContract_Compname,//本公司名称
			IsInvoices:values.PContract_IsInvoices ? '是' : '否',//是否开具发票
			Comment:values.PContract_Comment,//备注
//			PClientID:values.PContract_PClientID,//客户主键ID
			PayOrgID:values.PContract_PayOrgID,//付款单位ID
			PrincipalID:values.PContract_PrincipalID,//销售负责人主键ID
//			SignManID:values.PContract_SignManID,//签署人ID
			CompnameID: values.PContract_CompnameID,//本公司ID
			ContractStatus:values.PContract_ContractStatus,//状态
			EquipOneWayCount:values.PContract_EquipOneWayCount, //单项仪器个数
			EquipTwoWayCount:values.PContract_EquipTwoWayCount, //双向仪器个数
			LinkEquipInfoListHTML:values.PContract_LinkEquipInfoListHTML,
			PurchaseDescHTML:values.PContract_PurchaseDescHTML,
			ContractServiceCharge:values.PContract_ContractServiceCharge,
			Emphases:values.PContract_Emphases
		};
		if(values.PContract_PClientID){
			entity.PClientID=values.PContract_PClientID; //客户
			entity.PClientName=values.PContract_PClientName;
		}
		if(values.PContract_ContentID){
			entity.Content=values.PContract_Content;//项目类别
			entity.ContentID=values.PContract_ContentID;//项目类别
		}
		if (values.PContract_PContractAttrID) {
		    entity.PContractAttrName = values.PContract_PContractAttrName;//合同性质名称
		    entity.PContractAttrID = values.PContract_PContractAttrID;//合同性质ID
		}
		if(values.PContract_SignDate){
			entity.SignDate=JShell.Date.toServerDate(values.PContract_SignDate);//签署日期
		}
		if(values.PContract_SignMan){
			entity.SignMan=values.PContract_SignMan;//签署人
			entity.SignManID=values.PContract_SignManID;//签署人
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
		
		if(values.PContract_ServerContractStartDateTime){
			entity.ServerContractStartDateTime=JShell.Date.toServerDate(values.PContract_ServerContractStartDateTime);//
		}
		if(values.PContract_ServerContractEndDateTime){
			entity.ServerContractEndDateTime=JShell.Date.toServerDate(values.PContract_ServerContractEndDateTime);//到期时间
		}
		if(values.PContract_PlanSignDateTime){
			entity.PlanSignDateTime=JShell.Date.toServerDate(values.PContract_PlanSignDateTime);//签署日期
		}
		if(values.PContract_OriginalMoneyTotal){
			entity.OriginalMoneyTotal=values.PContract_OriginalMoneyTotal;
		}	
		if(values.PContract_ServerChargeRatio){
			entity.ServerChargeRatio=values.PContract_ServerChargeRatio;
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
			'SignDate','MiddleFee','Amount','SignMan',
			'Compname','IsInvoices','Comment','PClientID',
			'PayOrgID','PrincipalID','SignManID','CompnameID',
			'Id','ContractStatus','EquipOneWayCount','EquipTwoWayCount',
			'Content', 'ContractServiceCharge', 'Emphases', 'ContentID', 'PContractAttrName', 'PContractAttrID',
			'ServerContractStartDateTime','ServerContractEndDateTime',
			'OriginalMoneyTotal','ServerChargeRatio','PlanSignDateTime'
		];
		
		entity.fields = fields.join(',');
		if(values.PContract_LinkEquipInfoListHTML){
			entity.fields+=',LinkEquipInfoListHTML';
		}
		if(values.PContract_PurchaseDescHTML){
			entity.fields+=',PurchaseDescHTML';
		}
		entity.entity.Id = values.PContract_Id;
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
		
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请重新保存');
    			return;
    		}
			if(isSubmit){//提交
    			var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus','申请');
				me.getForm().setValues({
					PContract_ContractStatus:info.Id
				});
			}else{//暂存
				var info = JShell.System.ClassDict.getClassInfoByName('PContractStatus','暂存');
				me.getForm().setValues({
					PContract_ContractStatus:info.Id
				});
			}
			//新增时需要校验合同编号是否不重复
			if(values.PContract_Id){
				me.onSaveClick();
			}else{
				me.isValidContractNumber(function(){
					me.onSaveClick();
				});
			}
    	});
	},
	/**校验合同编号是否不重复*/
	isValidContractNumber:function(callback,ContractNumber){
		var me = this,
			values = me.getForm().getValues(),
			url = '/SingleTableService.svc/ST_UDTO_SearchPContractByHQL';
		
		url = JShell.System.Path.getRootUrl(url);
		var where = "pcontract.ContractNumber='" + values.PContract_ContractNumber + "'";
		//修改时判断合同编号是否重复
		if(ContractNumber){
			where +=' and pcontract.Id!='+ContractNumber;
		}
		url += '?fields=Id&where=' + where;
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value && data.value.count > 0){
					JShell.Msg.error("合同编号为" + (values.PContract_ContractNumber || ' ') + "的合同已经存在，请换一个合同编号！");
				}else{
					callback();
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});