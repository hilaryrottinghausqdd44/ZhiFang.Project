/**
 * 合同编辑表单
 * @author Jcall
 * @version 2016-10-31
 */
Ext.define('Shell.class.wfm.business.contract.manage.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'合同编辑表单',
    width:640,
    height:360,
    bodyPadding:10,

	/**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchPContractById?isPlanish=true',
    /**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddPContract',
    /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdatePContractByField',
	
    /**布局方式*/
    layout:{type:'table',columns:3},
    /**每个组件的默认属性*/
    defaults:{labelWidth:70,width:200,labelAlign:'right'},
    
    /*本公司名称*/
    COMPNAME:'OurCorName',
    
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initListeners();
    },
    initComponent: function () {
        var me = this;
		me.buttonToolbarItems = ['->','save','reset'];
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
			fieldLabel:'合同编号',name:'PContract_ContractNumber',
			emptyText:'必填项',allowBlank:false,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'客户选择',name:'PContract_PClientName',itemId:'PContract_PClientName',
			emptyText:'必填项',allowBlank:false,
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
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp',
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
			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'其它费用',name:'PContract_MiddleFee',itemId:'PContract_MiddleFee',
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
			fieldLabel:'本公司名称',name:'PContract_Compname',itemId:'PContract_Compname',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title:'本公司名称选择',
				defaultWhere:"pdict.BDictType.DictTypeCode='" + me.COMPNAME + "'"
			},
			colspan:2,width:me.defaults.width * 2
		},{
		    boxLabel:'是否开具发票',name:'PContract_IsInvoices',itemId:'PContract_IsInvoices',
		    xtype:'checkbox',checked:true,fieldLabel:'&nbsp;',labelSeparator:'',
		    colspan:1,width:me.defaults.width * 1
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
				SignMan.setValue(record ? record.get('HREmployee_CName') : '');
				SignManID.setValue(record ? record.get('HREmployee_Id') : '');
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
		
		//价格联动
		var Software = me.getComponent('PContract_Software'),
			Hardware = me.getComponent('PContract_Hardware'),
			MiddleFee = me.getComponent('PContract_MiddleFee');
			
		Software.on({change:function(){me.onPriceChange();}});
		Hardware.on({change:function(){me.onPriceChange();}});
		MiddleFee.on({change:function(){me.onPriceChange();}});
	},
	/**价格变化处理*/
	onPriceChange:function(){
		var me = this,
			Software = me.getComponent('PContract_Software'),
			Hardware = me.getComponent('PContract_Hardware'),
			MiddleFee = me.getComponent('PContract_MiddleFee'),
			Amount = me.getComponent('PContract_Amount');
			
		var sPrice = Software.getValue() || 0,
			hPrice = Hardware.getValue() || 0,
			mPrice = MiddleFee.getValue() || 0;
			
		Amount.setValue(sPrice + hPrice + mPrice);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			Name:values.PContract_Name,//合同名称
			ContractNumber:values.PContract_ContractNumber,//合同编号
			PClientName:values.PContract_PClientName,//客户
			Principal:values.PContract_Principal,//销售负责人
			PayOrg:values.PContract_PayOrg,//付款单位
			PaidServiceStartTime:JShell.Date.toServerDate(values.PContract_PaidServiceStartTime),//有偿服务时间
			Software:values.PContract_Software,//软件价格
			Hardware:values.PContract_Hardware,//硬件价格
			SignDate:JShell.Date.toServerDate(values.PContract_SignDate),//签署日期
			MiddleFee:values.PContract_MiddleFee,//其它费用
			Amount:values.PContract_Amount,//合同总额
			SignMan:values.PContract_SignMan,//签署人
			Compname:values.PContract_Compname,//本公司名称
			IsInvoices:values.PContract_IsInvoices ? '是' : '否',//是否开具发票
			Comment:values.PContract_Comment,//备注
			PClientID:values.PContract_PClientID,//客户主键ID
			PayOrgID:values.PContract_PayOrgID,//付款单位ID
			PrincipalID:values.PContract_PrincipalID,//销售负责人主键ID
			SignManID:values.PContract_SignManID,//签署人ID
			CompnameID:values.PContract_CompnameID//本公司ID
		};
		
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
			'Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.PContract_Id;
		return entity;
	}
});