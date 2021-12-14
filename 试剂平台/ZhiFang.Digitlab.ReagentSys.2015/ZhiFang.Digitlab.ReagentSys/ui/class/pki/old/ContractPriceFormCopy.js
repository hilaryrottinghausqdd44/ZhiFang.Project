/**
 * 合同价格表单-复制
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.ContractPriceFormCopy',{
    extend:'Shell.ux.form.Panel',
    
    requires:[
	    'Shell.ux.form.field.BoolComboBox',
	    'Shell.ux.form.field.SimpleComboBox'
    ],
    
    title:'合同价格表单-复制',
    width:320,
    height:260,
    
    /**新增服务地址*/
    addUrl:'/BaseService.svc/ST_UDTO_AddDContractPrice',
    
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否重置按钮*/
	hasReset:true,
	
	/**送检单位ID*/
	LaboratoryId:null,
	/**送检单位时间戳*/
	LaboratoryDataTimeStamp:null,
	
	/**送检单位默认开票方ID*/
	LaboratoryBillingUnitId:null,
	/**送检单位默认开票方名称*/
	LaboratoryBillingUnitName:null,
	/**送检单位默认开票方时间戳*/
	LaboratoryBillingUnitDataTimeStamp:null,
	
	defaults:{
    	width:270,
        labelWidth:60,
        labelAlign:'left'
    },
	
	check1:true,
	check2:false,
	check3:false,
	check4:false,
	check5:false,
	check6:false,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var comCheck1 = me.getComponent('check1');
		var comCheck2 = me.getComponent('check2');
		var comCheck3 = me.getComponent('check3');
		var comCheck4 = me.getComponent('check4');
		var comCheck5 = me.getComponent('check5');
		var comCheck6 = me.getComponent('check6');
		
		var BeginDate = me.getComponent('DContractPrice_BeginDate');
		var EndDate = me.getComponent('DContractPrice_EndDate');
		var ContractNo = me.getComponent('DContractPrice_ContractNo');
		var BDealer = me.getComponent('DContractPrice_BDealer_Name');
		var BDept = me.getComponent('DContractPrice_BDept_CName');
		var BillingUnitType = me.getComponent('DContractPrice_BillingUnitType');
		var BBillingUnit = me.getComponent('DContractPrice_BBillingUnit_Name');
		
		comCheck1.on({
			change:function(field,newValue){
				if(newValue){
					BeginDate.enable();
					EndDate.enable();
				}else{
					BeginDate.disable();
					EndDate.disable();
				}
			}
		});
		comCheck2.on({
			change:function(field,newValue){
				newValue ? ContractNo.enable() : ContractNo.disable();
			}
		});
		comCheck3.on({
			change:function(field,newValue){
				newValue ? BDealer.enable() : BDealer.disable();
			}
		});
		comCheck4.on({
			change:function(field,newValue){
				newValue ? BDept.enable() : BDept.disable();
			}
		});
		comCheck5.on({
			change:function(field,newValue){
				newValue ? BillingUnitType.enable() : BillingUnitType.disable();
			}
		});
		comCheck6.on({
			change:function(field,newValue){
				newValue ? BBillingUnit.enable() : BBillingUnit.disable();
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems:function(){
		var me = this,
			unitTypeObj = JShell.PKI.Enum.UnitType,
			unitTypeList = [],
			items = [];
			
		for(var i in unitTypeObj){
			unitTypeList.push([i.slice(1),unitTypeObj[i]]);
		}
		
		items.push({fieldLabel:'主键ID',name:'DContractPrice_Id',hidden:true});
		
		//复选框
		items.push({x:10,y:12,xtype:'checkbox',width:30,itemId:'check1',checked:me.check1});
		items.push({x:10,y:42,xtype:'checkbox',width:30,itemId:'check2',checked:me.check2});
		items.push({x:10,y:72,xtype:'checkbox',width:30,itemId:'check3',checked:me.check3});
		items.push({x:10,y:102,xtype:'checkbox',width:30,itemId:'check4',checked:me.check4});
		items.push({x:10,y:132,xtype:'checkbox',width:30,itemId:'check5',checked:me.check5});
		items.push({x:10,y:162,xtype:'checkbox',width:30,itemId:'check6',checked:me.check6});
		
		//开始日期
		items.push({
			x:30,y:10,width:160,fieldLabel:'日期范围',disabled:!me.check1,
			name:'DContractPrice_BeginDate',
			itemId:'DContractPrice_BeginDate',
			xtype:'datefield',format:'Y-m-d',
			allowBlank:false
		});
		//截止日期
		items.push({
			x:190,y:10,width:110,fieldLabel:'-',disabled:!me.check1,
			labelWidth:10,labelAlign:'right',
			name:'DContractPrice_EndDate',
			itemId:'DContractPrice_EndDate',
			xtype:'datefield',format:'Y-m-d',
			labelSeparator:'',allowBlank:false
		});
		//合同编号
		items.push({
			x:30,y:40,fieldLabel:'合同编号',disabled:!me.check2,
			name:'DContractPrice_ContractNo',
			itemId:'DContractPrice_ContractNo'
		});
		
		//经销商
		items.push({
			x:30,y:70,fieldLabel:'经销商',allowBlank:false,disabled:!me.check3,
			name:'DContractPrice_BDealer_Name',
			itemId:'DContractPrice_BDealer_Name',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.pki.DealerCheckGrid',{
					resizable:false,
					listeners:{
						accept:function(p,record){me.onDealerAccept(record);p.close();}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel:'经销商主键ID',hidden:true,
			name:'DContractPrice_BDealer_Id',
			itemId:'DContractPrice_BDealer_Id'
		});
		items.push({
			fieldLabel:'经销商时间戳',hidden:true,
			name:'DContractPrice_BDealer_DataTimeStamp',
			itemId:'DContractPrice_BDealer_DataTimeStamp'
		});
		
		//科室-可以为空
		items.push({
			x:30,y:100,fieldLabel:'科室',disabled:!me.check4,
			name:'DContractPrice_BDept_CName',
			itemId:'DContractPrice_BDept_CName',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.pki.LabDeptCheckGrid',{
					resizable:false,
					defaultWhere:'blabdept.BLaboratory.Id=' + me.LaboratoryId,
					listeners:{
						accept:function(p,record){me.onDeptAccept(record);p.close();}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel:'科室主键ID',hidden:true,
			name:'DContractPrice_BDept_Id',
			itemId:'DContractPrice_BDept_Id'
		});
		items.push({
			fieldLabel:'科室时间戳',hidden:true,
			name:'DContractPrice_BDept_DataTimeStamp',
			itemId:'DContractPrice_BDept_DataTimeStamp'
		});
		
		//开票类型-可以为空
		items.push({
			x:30,y:130,fieldLabel:'开票类型',xtype:'uxSimpleComboBox',disabled:!me.check5,
			name:'DContractPrice_BillingUnitType',
			itemId:'DContractPrice_BillingUnitType',
			data:unitTypeList,
			listeners:{
				change:function(field,newValue){me.onBillingUnitTypeChange(newValue);}
			}
		});
		//开票方
		items.push({
			x:30,y:160,fieldLabel:'开票方',allowBlank:false,disabled:!me.check6,
			name:'DContractPrice_BBillingUnit_Name',
			itemId:'DContractPrice_BBillingUnit_Name',
			xtype:'trigger',triggerCls:'x-form-search-trigger',
			enableKeyEvents:false,editable:false,
			onTriggerClick:function(){
				JShell.Win.open('Shell.class.pki.BillingUnitCheckGrid',{
					resizable:false,
					listeners:{
						accept:function(p,record){me.onBillingUnitAccept(record);p.close();}
					}
				}).show();
			}
		});
		items.push({
			fieldLabel:'开票方主键ID',hidden:true,
			name:'DContractPrice_BBillingUnit_Id',
			itemId:'DContractPrice_BBillingUnit_Id'
		});
		items.push({
			fieldLabel:'开票方时间戳',hidden:true,
			name:'DContractPrice_BBillingUnit_DataTimeStamp',
			itemId:'DContractPrice_BBillingUnit_DataTimeStamp'
		});
		
		//说明
//		items.push({
//			x:10,y:190,width:290,
//          xtype:'fieldset',
//          title:'说明',
//          items:[{
//          	xtype:'label',
//          	text:'选中的部分为需要更新的部分，没选中的，保持原值'
//          }]
//      });
		return items;
	},
	/**开票类型发生变化*/
	onBillingUnitTypeChange:function(value){
		var me = this;
		
		switch(value){
			case '0' ://没有开票方
				me.changeBillingUnit();break;
			case '1' ://经销商
				me.changeBillingUnit(
					me.BDealerBBillingUnitId,
					me.BDealerBBillingUnitName,
					me.BDealerBBillingUnitDataTimeStamp
				);break;
			case '2' ://送检单位
				me.changeBillingUnit(
					me.LaboratoryBillingUnitId,
					me.LaboratoryBillingUnitName,
					me.LaboratoryBillingUnitDataTimeStamp
				);break;
		}
	},
	/**经销商选择确认处理*/
	onDealerAccept:function(record){
		var me = this;
		var Id = me.getComponent('DContractPrice_BDealer_Id');
		var Name = me.getComponent('DContractPrice_BDealer_Name');
		var DataTimeStamp = me.getComponent('DContractPrice_BDealer_DataTimeStamp');
		
		Id.setValue(record ? record.get('BDealer_Id') : '');
		Name.setValue(record ? record.get('BDealer_Name') : '');
		DataTimeStamp.setValue(record ? record.get('BDealer_DataTimeStamp') : '');
		
		
		me.BDealerBBillingUnitId = record ? record.get('BDealer_BBillingUnit_Id') : '';
		me.BDealerBBillingUnitName = record ? record.get('BDealer_BBillingUnit_Name') : '';
		me.BDealerBBillingUnitDataTimeStamp = record ? record.get('BDealer_BBillingUnit_DataTimeStamp') : '';
		
		var BillingUnitType = me.getComponent('DContractPrice_BillingUnitType');
		var v = BillingUnitType.getValue();
		if(v == '1'){//经销商
			me.changeBillingUnit(
				me.BDealerBBillingUnitId,
				me.BDealerBBillingUnitName,
				me.BDealerBBillingUnitDataTimeStamp
			);
		}
	},
	
	/**科室选择确认处理*/
	onDeptAccept:function(record){
		var me = this;
		var Id = me.getComponent('DContractPrice_BDept_Id');
		var Name = me.getComponent('DContractPrice_BDept_CName');
		var DataTimeStamp = me.getComponent('DContractPrice_BDept_DataTimeStamp');
		
		Id.setValue(record ? record.get('BLabDept_BDept_Id') : '');
		Name.setValue(record ? record.get('BLabDept_BDept_CName') : '');
		DataTimeStamp.setValue(record ? record.get('BLabDept_BDept_DataTimeStamp') : '');
	},
	/**开票方选择确认处理*/
	onBillingUnitAccept:function(record){
		var me = this;
		if(record){
			me.changeBillingUnit(
				record.get('BBillingUnit_Id'),
				record.get('BBillingUnit_Name'),
				record.get('BBillingUnit_DataTimeStamp')
			);
		}else{
			me.changeBillingUnit();
		}
	},
	/**更改开票方信息*/
	changeBillingUnit:function(Id,Name,DataTimeStamp){
		var me = this;
		var ComId = me.getComponent('DContractPrice_BBillingUnit_Id');
		var ComName = me.getComponent('DContractPrice_BBillingUnit_Name');
		var ComDataTimeStamp = me.getComponent('DContractPrice_BBillingUnit_DataTimeStamp');
		
		ComId.setValue(Id || '');
		ComName.setValue(Name || '');
		ComDataTimeStamp.setValue(DataTimeStamp || '');
	},
	/**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		
		if(!me.getForm().isValid()) return;
		
		var entity = me.getEntity();
		
		me.fireEvent('save',me,entity);
	},
	/**获取数据对象*/
	getEntity:function(){
		var me = this,
			values = me.getForm().getValues(),
			entity = {};
		
		var check1 = me.getComponent('check1').getValue();
		var check2 = me.getComponent('check2').getValue();
		var check3 = me.getComponent('check3').getValue();
		var check4 = me.getComponent('check4').getValue();
		var check5 = me.getComponent('check5').getValue();
		var check6 = me.getComponent('check6').getValue();
		
		if(check1){
			entity.BeginDate = JShell.Date.toServerDate(values.DContractPrice_BeginDate);
			entity.EndDate = JShell.Date.toServerDate(values.DContractPrice_EndDate);
		}
		if(check2){
			entity.ContractNo = values.DContractPrice_ContractNo;
		}
		if(check3){
			entity.BDealer = {
				Id:values.DContractPrice_BDealer_Id,
				DataTimeStamp:values.DContractPrice_BDealer_DataTimeStamp.split(',')
			};
		}
		if(check4){
			//开票类型
			entity.BillingUnitType = values.DContractPrice_BillingUnitType || '0';
		}
		if(check5){
			//科室
			if(values.DContractPrice_BDept_Id){
				entity.BDept = {
					Id:values.DContractPrice_BDept_Id,
					DataTimeStamp:values.DContractPrice_BDept_DataTimeStamp.split(',')
				};
			}else{
				entity.BDept = null;
			}
		}
		if(check6){
			entity.BBillingUnit = {
				Id:values.DContractPrice_BBillingUnit_Id,
				DataTimeStamp:values.DContractPrice_BBillingUnit_DataTimeStamp.split(',')
			};
		}
		
		return entity;
	}
});