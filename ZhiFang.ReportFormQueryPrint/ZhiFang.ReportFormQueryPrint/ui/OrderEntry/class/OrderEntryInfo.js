/**
 * 申请单内容应用
 * @author Jcall
 * @version 2014-10-23
 */
Ext.define('Shell.OrderEntry.class.OrderEntryInfo',{
	extend:'Shell.ux.panel.AppPanel',
	
	title:'申请详细信息',
	
	/**状态类型*/
	formtype:'show',
	/**字典数据*/
	dictionaryData:{},
	/**HIS科室ID*/
	HisDeptNo:null,
	/**就诊类型*/
	SickTypeNo:null,
	/**新增数据服务地址*/
	addUrl:'/OrderService.svc/AddOrder',
	/**修改数据服务地址*/
	editUrl:'/OrderService.svc/EditOrder',
	/**申请单号*/
	SerialNo:null,
	
	/**错误信息*/
	errorInfo:[],
	
	layout:{type:'border',regionWeights:{east:2,north:1}},
	
	/**渲染完后处理*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.disableControl();
		me.setReadOnly(true);
	},
	initListeners:function(){
		var me = this,
			OrderItemsTree = me.getComponent('OrderItemsTree'),
			OrderItems = me.getComponent('OrderItems');
			
		OrderItemsTree.on({
			itemdblclick:function(view,record){
				var list = OrderItemsTree.getChildrenItems(record),
					len = list.length;
					
				for(var i=0;i<len;i++){
					OrderItems.addData(list[i]);
				}
			}
		});
	},
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		
		me.addEvents('save');
		//功能按钮栏
		me.toolbars = [{
			dock:'top',itemId:'toptoolbar',buttons:[
				{xtype:'label',itemId:'title',text:'申请单详细信息',style:'color:blue;font-weight:bold;',margin:'0 10 0 10'},
				'-','save'
			]
		}];
		//内部应用
		me.apps = [{
			className:'Shell.OrderEntry.class.OrderItemsTree',
			itemId:'OrderItemsTree',header:false,defaultLoad:false,
			HisDeptNo:me.HisDeptNo,SickTypeNo:me.SickTypeNo,
			width:230,region:'east',
			split:true,collapsible:true
		},{
			className:'Shell.OrderEntry.class.OrderItems',
			itemId:'OrderItems',header:false,
			region:'center'
		},{
			className:'Shell.OrderEntry.class.OrderPatientInfo',
			itemId:'OrderPatientInfo',header:false,autoScroll:true,
			height:225,region:'north',
			split:true,collapsible:true
		}];
		
		me.callParent(arguments);
	},
	
	/**所有的字典赋值*/
	setAllDictionary:function(obj){
		var me = this,
			OrderPatientInfo = me.getComponent('OrderPatientInfo'),
			OrderItemsTree = me.getComponent('OrderItemsTree'),
			
			AgeUnitNo = OrderPatientInfo.getComponent('AgeUnitNo'),//年龄单位
			DeptNo = OrderPatientInfo.getComponent('DeptNo'),//科室
			DistrictNo = OrderPatientInfo.getComponent('DistrictNo'),//病区
			Doctor = OrderPatientInfo.getComponent('Doctor'),//医生
			GenderNo = OrderPatientInfo.getComponent('GenderNo'),//性别
			SickTypeNo = OrderPatientInfo.getComponent('SickTypeNo'),//就诊类型
			ExecDeptNo = OrderPatientInfo.getComponent('ExecDeptNo'),//执行科室
			TestTypeNo = OrderPatientInfo.getComponent('TestTypeNo'),//检验类型
			ChargeNo = OrderPatientInfo.getComponent('ChargeNo');//收费类型
		
		me.dictionaryData = obj || {};
		
		AgeUnitNo.onTriggerClick();
		DeptNo.onTriggerClick();
		DistrictNo.onTriggerClick();
		Doctor.onTriggerClick();
		GenderNo.onTriggerClick();
		SickTypeNo.onTriggerClick();
		ExecDeptNo.onTriggerClick();
		TestTypeNo.onTriggerClick();
		ChargeNo.onTriggerClick();
		
		AgeUnitNo.collapse();
		DeptNo.collapse();
		DistrictNo.collapse();
		Doctor.collapse();
		GenderNo.collapse();
		SickTypeNo.collapse();
		ExecDeptNo.collapse();
		TestTypeNo.collapse();
		ChargeNo.collapse();
		
		AgeUnitNo.store.loadData(me.dictionaryData.AgeUnitList || []);
		DeptNo.store.loadData(me.dictionaryData.DeptList || []);
		DistrictNo.store.loadData(me.dictionaryData.DistrictList || []);
		Doctor.store.loadData(me.dictionaryData.DoctorList || []);
		GenderNo.store.loadData(me.dictionaryData.GenderList || []);
		SickTypeNo.store.loadData(me.dictionaryData.SickTypeList || []);
		ExecDeptNo.store.loadData(me.dictionaryData.SectorTypeList || []);
		TestTypeNo.store.loadData(me.dictionaryData.TestTypeList || []);
		ChargeNo.store.loadData(me.dictionaryData.ChargeTypeList || []);
		
		//默认选中第一个执行科室
		var record = ExecDeptNo.store.getAt(0);
		if(record){
			ExecDeptNo.setValue(record);
		}
		
		//默认选中第一个检验类型
		var record2 = TestTypeNo.store.getAt(0);
		if(record2){
			TestTypeNo.setValue(record2);
		}
		
		OrderPatientInfo.setReadOnly(true);
	},
	
	/**加载数据 申请单号*///PatNo,DeptNo
	load:function(SerialNo){
		var me = this,
			OrderPatientInfo = me.getComponent('OrderPatientInfo'),
			OrderItems = me.getComponent('OrderItems');
		
		if(me.formtype == "add"){
			me.formtype = "edit";
			me.changeTitle();
		}
			
		me.SerialNo = SerialNo;
		OrderPatientInfo.load(SerialNo);//加载申请单信息
		OrderItems.load(SerialNo);//加载申请单项目信息
	},
	
	/**@public 新增信息*/
	infoAdd:function(info){
		var me = this,
			OrderPatientInfo = me.getComponent('OrderPatientInfo');
			
		me.formtype = 'add';
        me.changeTitle();
        
        me.enableControl();
        me.setReadOnly(false);
        me.reset();
        
        info.Doctor = info.DoctorNo;
        
        OrderPatientInfo.setValues(info);
    },
    /**@public 修改信息*/
    infoEdit:function(id){
    	var me = this;
    	me.formtype = 'edit';
    	
        me.enableControl();
        me.setReadOnly(false);
        
        me.changeTitle();
       	me.load(id);
    },
    /**@public 查看信息*/
    infoShow:function(id){
        var me = this;
        me.formtype = 'show';
        
        me.disableControl();
        me.setReadOnly(true);
        
        me.changeTitle();
        me.load(id);
    },
    /**@public 清空数据,禁用功能按钮*/
    clearData:function(){
    	var me = this;
    	me.disableControl();//禁用 所有的操作功能
    	me.reset();//还原数据
    },
	
    changeTitle:function(){
    	var me = this,
    		title = me.getComponent('toptoolbar').getComponent('title'),
    		type = me.formtype,
    		text = "申请单详细信息-";
    		
    	switch(type){
    		case "add" : text += "新增";break;
    		case "edit" : text += "修改";break;
    		case "show" : text += "查看";break;
    	}
    	
    	title.setText(text);
    },
    setReadOnly:function(bo){
    	var me = this,
    		OrderPatientInfo = me.getComponent('OrderPatientInfo'),
    		OrderItems = me.getComponent('OrderItems');
    		
    	OrderPatientInfo.setReadOnly(bo);
    	OrderItems.setReadOnly(bo);
    },
    reset:function(){
    	var me = this,
    		OrderPatientInfo = me.getComponent('OrderPatientInfo'),
    		OrderItems = me.getComponent('OrderItems');
    		
    	OrderPatientInfo.infoAdd();
    	OrderItems.clearData();
    },
    
    /**保存数据*/
    onSaveClick:function(){
    	var me = this,
    		isAdd = me.formtype == "add",
    		url = Shell.util.Path.rootPath + (isAdd ? me.addUrl : me.editUrl),
    		infoField = 'ResultDataValue';
    	
    	me.errorInfo = [];//清空错误信息
		var params = {
			orderForm:me.getOrderPatientInfoValues(),
			orderItem:me.getOrderItemsValues()
		};
		
		if(me.errorInfo.length > 0){
			Shell.util.Msg.showError(me.errorInfo.join("</br>"));
			return;
		}
		
		params = Ext.JSON.encode(params);
    	
    	me.postToServer(url,params,function(text){
    		var result = Ext.JSON.decode(text);
    		if(!result.success){
    			Shell.util.Msg.showError(result.ErrorInfo);
    		}
    		var SerialNo = isAdd ? result[infoField] : me.SerialNo;
    		me.fireEvent('save',me,SerialNo);
    	},false);
    },
    /**获取病人信息*/
    getOrderPatientInfoValues:function(){
    	var me = this,
    		OrderPatientInfo = me.getComponent('OrderPatientInfo'),
    		values = OrderPatientInfo.getValues(true);
    	
    	if(!values){
    		me.errorInfo.push("姓名/病历号/科室/医生/就诊类型/年龄/年龄单位,必须填写!");
    		return null;
    	}
    	
    	//下拉框数据转化
    	values.AgeUnitNo = values.AgeUnitNo || 0;
    	values.DeptNo = values.DeptNo || 0;
    	values.DistrictNo = values.DistrictNo || 0;
    	values.Doctor = values.Doctor || 0;
    	values.GenderNo = values.GenderNo || 0;
    	values.SickTypeNo = values.SickTypeNo || 0;
    	values.ExecDeptNo = values.ExecDeptNo || 0;
    	values.TestTypeNo = values.TestTypeNo || 0;
    	values.ChargeNo = values.ChargeNo || 0;
    	
    	//操作者=医生
    	values.Operator = values.Doctor;
    	//新增时去掉申请单号
    	if(me.formtype == "add"){
    		values.SerialNo = "";
    	}
    	
    	return values;
    },
    /**获取子项信息*/
    getOrderItemsValues:function(){
    	var me = this,
    		OrderItems = me.getComponent('OrderItems');
    		
    	var list = OrderItems.getData();
    	
    	if(list.length == 0){
    		me.errorInfo.push("必须选择项目!");
    		return null;
    	}
    	
    	//修改状态时加上申请单号
    	if(me.formtype == "edit"){
    		var len = list.length;
	    	for(var i=0;i<len;i++){
	    		list[i].SerialNo = me.SerialNo;
	    	}
    	}
    	
    	return list;
    }
});