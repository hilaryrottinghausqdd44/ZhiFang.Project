/**
 * 申请单病人信息
 * @author Jcall
 * @version 2014-10-23
 */
Ext.define('Shell.OrderEntry.class.OrderPatientInfo',{
	extend:'Shell.ux.panel.InfoForm',
	
	title:'病人信息',
	
	width:600,
	height:250,
	
	formtype:'add',
	
	layout:'absolute',
	
	defaults:{labelAlign:'right',labelWidth:45,width:180},
	searchUrl:'/OrderService.svc/GetOrderForm',
	/**申请单号*/
	SerialNo:null,
	
	toolbars:[],
	
	items:[
		{x:5,y:5,xtype:'textfield',itemId:'SerialNo',fieldLabel:'申请单',type:'key',locked:true,readOnly:true},//主键
		{x:5,y:30,xtype:'textfield',itemId:'CName',fieldLabel:'姓名',labelStyle:'color:blue',locked:true,readOnly:true,allowBlank:false},
		{x:5,y:55,xtype:'textfield',itemId:'PatNo',fieldLabel:'病历号',labelStyle:'color:blue',locked:true,readOnly:true,allowBlank:false},
		{x:5,y:80,width:120,xtype:'textfield',itemId:'Age',fieldLabel:'年龄',allowBlank:false},
		{x:125,y:80,width:60,xtype:'uxcombobox',itemId:'AgeUnitNo',
			displayField:'CName',valueField:'AgeUnitNo',allowBlank:false},//年龄单位代码
		
		{x:195,y:5,labelWidth:35,xtype:'uxcombobox',itemId:'DeptNo',fieldLabel:'科室',labelStyle:'color:blue',
			displayField:'CName',valueField:'DeptNo',locked:true,readOnly:true,allowBlank:false},
		{x:195,y:30,labelWidth:35,xtype:'uxcombobox',itemId:'DistrictNo',fieldLabel:'病区',
			displayField:'CName',valueField:'DistrictNo'},
		{x:195,y:55,labelWidth:35,xtype:'uxcombobox',itemId:'Doctor',fieldLabel:'医生',labelStyle:'color:blue',
			displayField:'CName',valueField:'DoctorNo',allowBlank:false},
		{x:195,y:80,labelWidth:35,xtype:'uxcombobox',itemId:'GenderNo',fieldLabel:'性别',
			displayField:'CName',valueField:'GenderNo'},
		
		{x:385,y:5,labelWidth:65,xtype:'uxcombobox',itemId:'SickTypeNo',fieldLabel:'就诊类型',labelStyle:'color:blue',
			displayField:'CName',valueField:'SickTypeNo',locked:true,readOnly:true,allowBlank:false},
		{x:385,y:30,labelWidth:65,xtype:'uxcombobox',itemId:'ExecDeptNo',fieldLabel:'执行科室',selectFirst:true,
			displayField:'SectorName',valueField:'SectorTypeNo'},
		{x:385,y:55,labelWidth:65,xtype:'textfield',itemId:'Bed',fieldLabel:'床位'},
		{x:385,y:80,labelWidth:65,xtype:'uxcombobox',itemId:'TestTypeNo',fieldLabel:'检验类型',
			displayField:'CName',valueField:'TestTypeNo'},
		
		{x:385,y:105,labelWidth:65,xtype:'uxcombobox',itemId:'ChargeNo',fieldLabel:'收费类型',
			displayField:'CName',valueField:'ChargeNo'},
			
		{x:5,y:130,width:560,xtype:'textarea',itemId:'zdy5',fieldLabel:'诊断'}
	],
	
	/**加载数据*/
	load:function(SerialNo,isPrivate){
		var me = this,
			url = Shell.util.Path.rootPath + me.searchUrl;
			//fields = me.getFields();
			
		if(!isPrivate && !SerialNo){
			Shell.util.Msg.showError("请传递申请单号!");
			return;
		}
			
		if(!isPrivate) me.SerialNo = SerialNo;
		
		url += "?serialno=" + me.SerialNo;// + "&fields=" + fields.join(",");
		
		if(me.hasLoadMask){me.body.mask(me.loadingText);}//显示遮罩层
		
		me.getToServer(url,function(text){
    		var result = Ext.JSON.decode(text),
				infoField = 'ResultDataValue';
			
    		if(result.success){
    			if(result[infoField]){
                    me.setValues(result[infoField]);
                }
    		}else{
    			Shell.util.Msg.showError(result.ErrorInfo);
    		}
    		if(me.hasLoadMask){me.body.unmask();}//隐藏遮罩层
    		me.fireEvent('load',me);
    	},false);
	},
	/**获取加载字段*/
	getFields:function(){
		var me = this,
			items = me.items.items || [],
			len = items.length,
			fields = [];
			
		for(var i=0;i<len;i++){
			if(items[i].itemId){fields.push(items[i].itemId);}
		}
			
		return fields;
	},
	
	/**赋值*/
	setValues:function(info){
		var me = this;
		
		me.clearData();
			
		for(var i in info){
			if(info[i] == 0){
				info[i] = null;
			}
		}
		
		me.callParent([info]);
	}
});