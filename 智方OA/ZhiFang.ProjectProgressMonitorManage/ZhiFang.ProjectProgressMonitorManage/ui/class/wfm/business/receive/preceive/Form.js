/**
 * 商务收款表单
 * @author liangyl	
 * @version 2016-12-23
 */
Ext.define('Shell.class.wfm.business.receive.preceive.Form', {
    extend:'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'商务收款表单',
    width:470,
    height:260,
    bodyPadding:10,

	/**获取数据服务路径*/
    selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceiveById?isPlanish=true',
	/**修改服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AddPReceive',
	/**删除数据服务路径*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePReceiveByField',
	
    /**布局方式*/
    layout:{type:'table',columns:2},
    /**每个组件的默认属性*/
    defaults:{labelWidth:70,width:215,labelAlign:'right'},
    
    /*本公司名称*/
	COMPNAME: 'OurCorName',
	/**付款单位ID*/
	PayOrgID:null,
	/**合同ID*/
	PContractID:null,
	/**合同*/
	PContractName:null,
	/**财务收款*/
	PFReceivenID:null,
	/**收款计划*/
	PReceivePlanId:null,
	/**收款负责人Id*/
	ReceiveManID:null,
	/**收款负责人*/
	ReceiveManName:null,
	/**未付*/
	UnReceiveAmount:0,
		//财务收款金额
	PFReceiveAmount:0,
	//财务收款已分配金额
	PFSplitAmount:0,
	//客户
	PClientName:null,
	//客户Id
	PClientID:null,
	UnReceiveAmount:0,
	/**执行公司ID*/
	CompnameID:null,
	/**执行公司*/
	ComponeName:null,
	ReceiveDate:null,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initListeners();
    },
    initComponent: function () {
        var me = this;
		me.buttonToolbarItems = ['->',{
			text:'提交',
			iconCls:'button-save',
			tooltip:'提交',
			handler:function(){
				me.onSave(true);
			}
		},'reset'];
        me.callParent(arguments);
    },
    /**@overwrite 创建内部组件*/
    createItems: function () {
        var me = this;
		var items = [{
			fieldLabel: '收款计划内未收金额',
			labelWidth:130,
			name: 'UnReceiveAmount',
			itemId: 'UnReceiveAmount',
			labelStyle: 'font-weight:bold;',
			value: 0,
			fieldStyle: 'font-weight:bold;color:red;background:none;border:0;border-bottom:0px',
			colspan: 1,
			readOnly:true,
//			locked:true,
//				style: {
//				marginLeft: '0px'
//			},
			width: me.defaults.width * 1
		}, {
			fieldLabel: '财务收款可分配金额',
			readOnly:true,
			locked:true,
			fieldStyle: 'background:none;border:0;border-bottom:0px',
			name: 'PFReceiveAmount',
			itemId: 'PFReceiveAmount',
			width:280,
			labelWidth:125,
			labelStyle: 'font-weight:bold;',
			fieldStyle: 'font-weight:bold;color:green;background:none;border:0;border-bottom:0px',
			value: 0,
			colspan: 1,
			width: me.defaults.width * 1
		},{
			fieldLabel:'合同',name:'PReceive_PContractName',itemId:'PReceive_PContractName',
			hidden:true,value:me.PContractName,
			xtype:'uxCheckTrigger',className:'Shell.class.wfm.business.receive.preceiveplan.basic.CheckGrid',
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'客户',name:'PReceive_PClientName',itemId:'PReceive_PClientName',value:me.PClientName,
			hidden:true,xtype:'uxCheckTrigger',className:'Shell.class.wfm.client.CheckGrid',
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'执行公司',name:'PReceive_Compname',itemId:'PReceive_Compname',
			emptyText:'必填项',allowBlank:false,
			xtype:'uxCheckTrigger',className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title:'本公司名称选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + me.COMPNAME + "'"
			},value:me.ComponeName,
			colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'收款金额',name:'PReceive_ReceiveAmount',itemId:'PReceive_ReceiveAmount',
			emptyText:'必填项',allowBlank:false,
			xtype:'numberfield',minValue:0,value:0,
			colspan:1,width:me.defaults.width * 1
		},{
			fieldLabel:'收款日期',name:'PReceive_ReceiveDate',
			emptyText:'必填项',allowBlank:false,
			xtype:'datefield',format: 'Y-m-d',value:me.ReceiveDate,
			colspan:1,width:me.defaults.width * 1	
		},{
			fieldLabel:'收款负责人',name:'PReceive_ReceiveManName',itemId:'PReceive_ReceiveManName',
			hidden:true,value:me.ReceiveManName,
			xtype:'uxCheckTrigger',className:'Shell.class.sysbase.user.CheckApp',
			colspan:2,width:me.defaults.width * 1
		},{
            fieldLabel:'备注',name:'PReceive_Comment',itemId:'PReceive_Comment',
            xtype:'textarea',height:80,
            colspan:2,width:me.defaults.width * 2
		},{
			fieldLabel:'主键ID',name:'PReceive_Id',hidden:true
		},{
			fieldLabel:'收款计划主键ID',name:'PReceive_PReceivePlanID',itemId:'PReceive_PReceivePlanID',hidden:true,value:me.PReceivePlanId
		},{
        	fieldLabel:'合同ID',name:'PReceive_PContractID',itemId:'PReceive_PContractID',hidden:true,value:me.PContractID
		},{
        	fieldLabel:'客户ID',name:'PReceive_PClientID',itemId:'PReceive_PClientID',hidden:true,value:me.PClientID
		},{
        	fieldLabel:'销售负责人主键ID',name:'PReceive_ReceiveManID',itemId:'PReceive_ReceiveManID',hidden:true,value:me.ReceiveManID
		},{
			fieldLabel:'本公司ID',name:'PReceive_CompnameID',value:me.CompnameID,itemId:'PReceive_CompnameID',hidden:true
        },{
			fieldLabel:'财务收款收款ID',name:'PReceive_PFReceivenID',itemId:'PReceive_PFReceivenID',hidden:true,value:me.PFReceivenID
        }];
        
		return items;
	},
	/**更改标题*/
	changeTitle: function() {},
	/**返回数据处理方法*/
	changeResult: function(data) {
		data.PReceive_ReceiveDate = JShell.Date.getDate(data.PReceive_ReceiveDate);
		return data;
	},
	/**初始化监听*/
	initListeners:function(){
		var me = this;
		//本公司
		var Compname = me.getComponent('PReceive_Compname'),
			CompnameID = me.getComponent('PReceive_CompnameID');
		Compname.on({
			check: function(p, record) {
				Compname.setValue(record ? record.get('PDict_CName') : '');
				CompnameID.setValue(record ? record.get('PDict_Id') : '');
				p.close();
			}
		});
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
			IsUse:1,
			InputerID:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			InputerName:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			ReceiveAmount:values.PReceive_ReceiveAmount,
			Comment : values.PReceive_Comment.replace(/\\/g, '&#92')
		};
//		if(me.PReceivePlanId){
//			entity.PReceivePlanID=me.PReceivePlanId;
//		}
		if(me.PReceivePlanId){
			entity.PReceivePlan = {
				Id:me.PReceivePlanId,
				DataTimeStamp:[0,0,0,0,0,0,0,0],
				UnReceiveAmount:me.UnReceiveAmount
			};
		}
		if(me.PContractID){
			entity.PContractID=me.PContractID;
			entity.PContractName=me.PContractName;
		}
		if(me.PClientID){
			entity.PClientID=me.PClientID;
			entity.PClientName=me.PClientName;
		}
		if(values.PReceive_CompnameID){
			entity.CompnameID=values.PReceive_CompnameID;
			entity.Compname=values.PReceive_Compname;
		}
		if(values.PReceive_ReceiveDate){
			entity.ReceiveDate=JShell.Date.toServerDate(values.PReceive_ReceiveDate);
		}
//		if(me.PFReceivenID){
//			entity.PFReceivenID=me.PFReceivenID;
//		}
		if(me.PFReceivenID){
			entity.PFinanceReceive = {
				Id:me.PFReceivenID,
				DataTimeStamp:[0,0,0,0,0,0,0,0],
				ReceiveAmount:me.PFReceiveAmount,
				SplitAmount:me.PFSplitAmount
			};
		}
		if(me.PayOrgID){
			entity.PayOrgID=me.PayOrgID;
		}
		if(me.PayOrgName){
			entity.PayOrgName=me.PayOrgName;
		}
		if(me.ReceiveManID){
			entity.ReceiveManID=me.ReceiveManID;
		}
		if(me.ReceiveManName){
			entity.ReceiveManName=me.ReceiveManName;
		}
		return {entity:entity};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		
		var fields = [
			'ReceiveAmount','Comment',
			'ReceiveDate','Id'
		];
		entity.fields = fields.join(',');
		
		entity.entity.Id = values.PReceive_Id;
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
		var msg='';
		//实际金额
		var AmountValue=parseFloat(values.PReceive_ReceiveAmount);
	    Amount = Number(AmountValue.toFixed(2));
        //未付
		var UnReceiveAmountValue=parseFloat(me.UnReceiveAmount);
	    var UnReceiveAmount = Number(UnReceiveAmountValue.toFixed(2));
	    
	    //财务收款金额
		var PFReceiveAmountValue=parseFloat(me.PFReceiveAmount);
	    var PFReceiveAmount = Number(PFReceiveAmountValue.toFixed(2));
	    //财务收款已分配金额
		var PFSplitAmountValue=parseFloat(me.PFSplitAmount);
	    var PFSplitAmount = Number(PFSplitAmountValue.toFixed(2));

        //未分配的财务收款
        var UNPFSplitAmount=PFReceiveAmount-PFSplitAmount;
          //a)如果实际收款小于未付，修改，录入后保存。
          //注意录入的数据不能大于本计划未付款，
          //也不能大于未分配的财务收款。
		if(Amount>UnReceiveAmount){
			JShell.Msg.error('收款金额不能大于本收款计划内未收金额!');
			 return ;
		}
		if(Amount>UNPFSplitAmount){
			JShell.Msg.error('收款金额不能大于财务收款可分配金额!');
			 return ;
		}
//		if(Amount==0 ){
//			JShell.Msg.error('收款金额不能等于0!');
//			 return ;
//		}
		me.onSaveClick();
    	
     },
	/**更改标题*/
	changeTitle: function() {},
		/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			var UnReceive = me.getComponent('UnReceiveAmount');
			UnReceive.setValue(me.UnReceiveAmount);
		    //财务收款金额
			var PFReceiveAmountValue=parseFloat(me.PFReceiveAmount);
		    var PFReceiveAmount = Number(PFReceiveAmountValue).toFixed(2);
		    //财务收款已分配金额
			var PFSplitAmountValue=parseFloat(me.PFSplitAmount);
		    var PFSplitAmount = Number(PFSplitAmountValue).toFixed(2);
	
	        //未分配的财务收款
	        var UNPFSplitAmount=PFReceiveAmount-PFSplitAmount;
			var PFReceive = me.getComponent('PFReceiveAmount');
			PFReceive.setValue(UNPFSplitAmount);
			
		} else {
			me.getForm().setValues(me.lastData);
		}
	}
});