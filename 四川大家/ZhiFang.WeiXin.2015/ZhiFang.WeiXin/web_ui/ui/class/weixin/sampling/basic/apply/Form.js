/**
 * 微信消费采样
 * @author GHX
 * @version 2021-01-05
 */
Ext.define("Shell.class.weixin.sampling.basic.apply.Form",{
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger_NEW',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.zhifangux.datetimenew'
    ],
    title:'新增采样',
	account:'',
	bodyPadding:5,
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'add',	
	/**获取数据服务路径*/
	selectAreaUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaById?isPlanish=true',
	selectSickTypeUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSickTypeByHQL?isPlanish=true',
	selectclentUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEById?isPlanish=true',
	//取消消费采样服务
	UN_CONSUME_URL:JShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinService.svc/UnConsumerUserOrderForm",
	//获取消费单信息服务
	GET_CONSUME_URL:JShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinService.svc/GetWeChatConsumptionSamplingInfo",
	/**是否回车*/
	CHECK_ENTER:false,
	//是否存在送检单位项目
	HAS_CLIENT_ITEM:false,
	myMask:'',
	/**布局方式*/
	layout:{
        type: 'table',
        columns: 8,
        tableAttrs: {
            cellpadding: 1,
            cellspacing: 1,
            width: '100%',            
            align: 'left'            
        }
    },
	/**每个组件的默认属性*/
    defaults:{
    	//anchor:'100%',
        labelWidth:70,
        labelAlign:'left'
    },
    
    afterRender:function(){
    	var me =this;
    	me.callParent(arguments);
		me.onMaskShow('基础数据加载中，请稍候……');//弹出遮罩层
		setTimeout(function(){
			me.getComponent('ClientName').onTriggerClick();	
			me.getComponent('JztypeName').onTriggerClick();
			me.getComponent('Operator').setValue(JShell.System.Cookie.get("000201"));
		},100);
		me.getComponent("PayCode").focus(true, 1000);
		me.loadSickType();
		me.onMaskHide();//取消遮罩层  		
		//初始化最后一次消费码信息
		me.initLastBarcodeInfo(function(){
			//不做处理
		});
    },
	initComponent:function(){
		var me = this;		
		me.addEvents('changeResult','load','beforesave','save','saveerror');
		me.defaultTitle = me.title;
		me.items = me.items || me.createItems();
		me._thisfields = [];
		me.initPKField();//初始化主键字段
		//创建挂靠功能栏
		var dockedItems = me.createDockedItems();
		if(dockedItems.length > 0){
			me.dockedItems = dockedItems;
		}		
		me.callParent(arguments);
		
		
	},
    /**更改标题*/
	changeTitle:function(){
		var me = this;
		me.setTitle(me.title);
	},
	createItems:function(){
		var me =this;
		var items=[ {
			xtype: 'uxCheckTrigger', fieldLabel: '送检单位', name: 'ClientName', itemId: 'ClientName',
			style: {color:'blue'},
			className: 'Shell.class.weixin.sampling.basic.dic.ClientCheckGrid',colspan:1,
			classConfig: {	checkOne: true,	autoSelect: true,account:me.account},
			anchor: '100%', allowBlank: false, 
			emptyText: '必选项',
			listeners: {
				check: function (p, record) {
					me.getComponent('ClientName').setValue(record ? record.get('BusinessLogicClientControl_CLIENTELE_CNAME') : '');
					me.getComponent('ClientNo').setValue(record ? record.get('BusinessLogicClientControl_CLIENTELE_Id') : '');
					me.loadArea(record);
					p.close();
				}
			}
		},{
				xtype: 'textfield',
				fieldLabel: '送检单位编号',
				name: 'ClientNo',
				itemId: 'ClientNo',
				hidden: true,
				anchor: '90%'
		},{
			fieldLabel:'姓名',
			name:'CName',colspan:1,disabled:true,
			itemId:'CName',
			listeners:{
				specialkey:function(field,e){
					if(e.getKey()==Ext.EventObject.ENTER){
						me.onsearch();
					}
				}
			}
		},{
			fieldLabel:'年龄',
			name:'Age',colspan:1,
			itemId:'Age'
		},{
            xtype: 'combobox',
            fieldLabel: '',
            name: 'AgeUnitNo',
			width:40,
			style: {
				position: 'relative',
				right: '50px'
			},
			colspan:1,
            itemId: 'AgeUnitNo',
            editable:false,
            displayField: 'text', valueField: 'value',value: "1",
            store: Ext.create('Ext.data.Store', {
                fields: ['text', 'value'],
                data: [
                    { text: '岁', value: '1' },
                    { text: '月', value: '2' },
					{ text: '日', value: '3' },
					{ text: '小时', value: '4' }
                ]
            }),
			listeners: {                     
				change: function (m, v) {     
					me.getComponent('AgeUnitName').setValue(m.rawValue);               
				}                
			}
        },{
			xtype: 'textfield',
			fieldLabel: '年龄单位编号',
			name: 'AgeUnitName',
			itemId: 'AgeUnitName',
			hidden: true,
			anchor: '90%',
			value:'岁'
		},{
			fieldLabel:'消费码',
			name:'PayCode',colspan:4,
			itemId:'PayCode',
			style: {color:'red'},
			listeners:{
				specialkey:function(field,e){
					if(e.getKey()==Ext.EventObject.ENTER){
						me.onsearch();
					}
				}
			}
		},{fieldLabel:'实验室区域',xtype: 'uxCheckTrigger',name:'CLIENTELE_AreaName',disabled:true,
			itemId: 'CLIENTELE_AreaName',emptyText:'可双击选择',allowBlank:false,
			className: 'Shell.class.weixin.clientele.new.CheckGrid',colspan:1,
			listeners: {
				check: function(p, record) {
					me.getComponent('CLIENTELE_AreaID').setValue(record ? record.get('ClientEleArea_Id') : '');
					me.getComponent('CLIENTELE_AreaName').setValue(record ? record.get('ClientEleArea_AreaCName') : '');
					me.getComponent('CLIENTELE_ClientNo').setValue(record ? record.get('ClientEleArea_ClientNo') : '');					
					p.close();					
				}
			}
		},{fieldLabel:'区域Id',itemId:'CLIENTELE_AreaID',name:'CLIENTELE_AreaID',hidden:true}
		,{fieldLabel:'区域编号',itemId:'CLIENTELE_ClientNo',name:'CLIENTELE_ClientNo',hidden:true}
		,{
		    xtype: 'combobox',
		    fieldLabel: '性别',disabled:true,
		    name: 'GenderNo',
		    itemId: 'GenderNo',
			colspan:1,
		    editable:false,
		    displayField: 'text', valueField: 'value',
		    store: Ext.create('Ext.data.Store', {
		        fields: ['text', 'value'],
		        data: [
		            { text: '男', value: '男' },
		            { text: '女', value: '女' }
		        ]
		    })
		},{
			fieldLabel:'病历号',
			name:'PatNo',colspan:2,disabled:true,
			itemId:'PatNo'
		},{
			xtype: 'datetimenew', 
			fieldLabel:'采样时间',colspan:1,
			style: {color:'red'},
			name:'CollectDate',
			itemId:'CollectDate'
		}, {
			xtype: 'button',
			text: '解锁消费码',
			height:60,
			rowspan:2,
			colspan:3,
			iconCls: 'button-search',
			labelAlign:'right',
			handler: function () {
				me.unPayCode();
			}
		},{fieldLabel:'就诊类型',xtype: 'uxCheckTrigger',name:'JztypeName',
			itemId: 'JztypeName',emptyText:'可双击选择',allowBlank:false,colspan:1,disabled:true,
			className: 'Shell.class.weixin.sampling.basic.dic.sicktypeCheckGrid',
			classConfig: {	checkOne: true,	autoSelect: true,account:me.account},
			listeners: {
				check: function(p, record) {
					me.getComponent('Jztype').setValue(record ? record.get('SickType_Id') : '');
					me.getComponent('JztypeName').setValue(record ? record.get('SickType_CName') : '');
					p.close();
				}
			}
		},{fieldLabel:'就诊类型Id',itemId:'Jztype',name:'Jztype',hidden:true}
		,{
			fieldLabel:'科室',
			name:'DeptName',colspan:1,disabled:true,
			itemId:'DeptName'
		},{
			fieldLabel:'医生',
			name:'DoctorName',colspan:2,disabled:true,
			itemId:'DoctorName'
		},{
			xtype: 'datetimenew', 
			fieldLabel:'开单时间',colspan:1,
			style: {color:'red'},
			name:'OperDate',
			itemId:'OperDate'
		},{
			fieldLabel:'采样人',
			name:'Operator',colspan:1,disabled:true,
			itemId:'Operator'
		},{
			fieldLabel:'收费',disabled:true,
			name:'Charge',colspan:1,
			itemId:'Charge'
		},{
			fieldLabel:'临床诊断',width:700,disabled:true,
			name:'Diag',colspan:6,
			itemId:'Diag'
		}
		];
		return items;
	},
	//消费码回车
	onsearch:function(){
		var me = this,
		formval = me.getForm().getValues();		
		if(me.CHECK_ENTER) return;//已经回车的继续回车无效
		me.CHECK_ENTER = true;//已经按了回车键
		//一秒后光标复位，防止快速回车
		setTimeout(function(){
			me.CHECK_ENTER = false;//回车重新启用
		},1000);
		me.onPayCodeEnter();
	},
	loadArea:function(record){
		var me = this;
		var clientid = record.get('BusinessLogicClientControl_CLIENTELE_Id');
		if(clientid && clientid != "" && clientid+"" != "0"){			
			var clenturl=JShell.System.Path.ROOT+me.selectclentUrl+"&fields=CLIENTELE_AreaID,CLIENTELE_Id&id="+clientid;
			JShell.Server.get(clenturl,function(data){
				if(data.success){
					data.value = JShell.Server.Mapping(data.value);
					if(data.value && data.value.CLIENTELE_AreaID){
						//区域
						var areaurl=JShell.System.Path.ROOT+me.selectAreaUrl+"&fields=ClientEleArea_AreaCName,ClientEleArea_Id,ClientEleArea_ClientNo&id="+data.value.CLIENTELE_AreaID;
						JShell.Server.get(areaurl,function(dataa){
							if(dataa.success){
								dataa.value = JShell.Server.Mapping(dataa.value);
								if(dataa.value && dataa.value.ClientEleArea_AreaCName){
									me.getComponent('CLIENTELE_AreaName').setValue(dataa.value.ClientEleArea_AreaCName);
									me.getComponent('CLIENTELE_AreaID').setValue(dataa.value.ClientEleArea_Id);
									me.getComponent('CLIENTELE_ClientNo').setValue(dataa.value.ClientEleArea_ClientNo);	
								}
							}							
						},true);
					}
				}							
			},true);			
		}
	},
	loadSickType:function(){
		var me = this;
		var sickurl=JShell.System.Path.ROOT+me.selectSickTypeUrl+"&fields=SickType_CName,SickType_Id&page=1&limit=5";
		JShell.Server.get(sickurl,function(data){
			if(data.success){
				data.value = JShell.Server.Mapping(data.value);
				if(data.value.list.length > 0 && data.value.count > 0){
					me.getComponent('Jztype').setValue(data.value.list[0].SickType_Id);
					me.getComponent('JztypeName').setValue(data.value.list[0].SickType_CName);
				}
			}							
		},true);
	},
	//消费码回车处理
	onPayCodeEnter:function(){
		var me = this;
		var payCode = me.getComponent("PayCode").getValue();//消费码
		me.onMaskShow("获取消费单信息中请稍等!");   	
		//获取最后一次消费码信息
		var LastBarcodeInfo = me.getLastBarcodeInfo();
		//输入的消费码为空
		if(!payCode){
			//最后一次消费码为空
			if(!LastBarcodeInfo || !LastBarcodeInfo.PayCode){
				return;
			}else{
				me.initLastBarcodeInfo(function(isSame){
					if(isSame){
						return;
					}
				});
			}
		}
		
		//消费码不变，不做处理
		if(payCode == LastBarcodeInfo.PayCode){
			return;
		}
		//弹出切换提示框
		me.confirmUnConsume(function(){
			PAY_CODE = payCode;//用户消费码
			me.getComponent("PayCode").blur();
			
			//初始化Local里面存在锁定消费码
			me.initLastBarcodeInfo(function(){
				//获取消费单信息
				me.getConsumeInfo(payCode,function(data){
					if(data.success){
						var info = data.value;	
						me.clearPatientInfo();//清空与患者相关数据
						if(info.BLabTestItems && info.BLabTestItems.length > 0){
							me.HAS_CLIENT_ITEM = true;
						}
						me.initConsumeInfo(info.userOrderFormVO);//初始化消费单信息
						me.changePackageTable(info.userOrderFormVO.UserOrderItem,info.testItemDetails);//修改列表
						
						//最后一次用户消费码相关信息
						LastBarcodeInfo.PayCode = payCode;//消费码
						LastBarcodeInfo.WeblisSourceOrgID = me.getComponent("ClientNo").getValue();//送检单位ID
						LastBarcodeInfo.WeblisSourceOrgName = me.getComponent("ClientName").getValue();//送检单位名称
						LastBarcodeInfo.ConsumerAreaID = me.getComponent("CLIENTELE_AreaID").getValue();//区域ID
						me.setLastBarcodeInfo(LastBarcodeInfo); 
						me.onMaskHide();   
					}else{
						me.onMaskHide();   
						JShell.Msg.error("获取消费单信息失败！错误信息：" + data.msg);
					}
				});
			});
		});
	},
	//设置最后一次消费码信息
	setLastBarcodeInfo:function(info){
		var me = this;
		var str = JShell.JSON.encode(info) || '';
		localStorage.setItem('LastBarcodeInfo', str);
	},
	//获取最后一次消费码信息
	getLastBarcodeInfo:function(){
		var me = this;
		var str = localStorage.getItem('LastBarcodeInfo') || '';
		var info = JShell.JSON.decode(str) || {};
		return info;
	},
	//初始化Local里面存在锁定消费码
	initLastBarcodeInfo:function(callback){
		var me = this;
		//获取最后一次消费码信息
		var LastBarcodeInfo = me.getLastBarcodeInfo();
		
		//Local里面存在锁定消费码
		if(LastBarcodeInfo && LastBarcodeInfo.PayCode){
			//取消消费码锁定
			me.UnConsume(LastBarcodeInfo.PayCode,function(data){
				me.clearPatientInfo();//清空与患者相关数据
				callback(false);
			});
		}else{
			callback(true);
		}
	},
	//取消消费码锁定
	UnConsume:function(payCode,callback){
		var me = this;
		me.onMaskShow('消费码锁定取消中，请稍候……');//弹出遮罩层
		
		//获取最后一次消费码信息
		var LastBarcodeInfo = me.getLastBarcodeInfo();
		
		var entity= {
			PayCode:payCode,
			WeblisSourceOrgID:LastBarcodeInfo.WeblisSourceOrgID,
			WeblisSourceOrgName:LastBarcodeInfo.WeblisSourceOrgName,
			ConsumerAreaID:LastBarcodeInfo.ConsumerAreaID
		};
		var params = Ext.JSON.encode({jsonentity: entity});
		JShell.Server.post(me.UN_CONSUME_URL,params,function(data){
			if(data.success){
				me.onMaskHide();//取消遮罩层
				me.setLastBarcodeInfo({});//清空Local数据
				callback();
			}else{
				me.onMaskHide();//取消遮罩层
				callback();				
			}
		});		
	},
	//弹出遮罩层
	onMaskShow:function(msg) {
		var me = this;
		me.fireEvent('onMaskShow',msg);
		//me.body.mask(msg);	
	},
	//取消遮罩层  
	onMaskHide:function() {
		var me = this;
		me.fireEvent('onMaskHide',"");
		//me.body.unmask();
	},
	//弹出切换提示框
	confirmUnConsume:function(callback){
		var me = this;
		//获取最后一次消费码信息
		var LastBarcodeInfo = me.getLastBarcodeInfo();
		//Local里面存在锁定消费码
		if(LastBarcodeInfo && LastBarcodeInfo.PayCode){
			JShell.Msg.confirm({
				msg:'您是否要切换消费码？'
			},function(but){
				if(but == 'ok'){
					me.getComponent("PayCode").setValue(LastBarcodeInfo.PayCode);
					return;
				}else{
					callback();
				}
			});			
		}else{
			callback();
		}
	}
	//获取消费单信息
    ,getConsumeInfo:function (payCode,callback){
		var me = this;
    	me.onMaskShow('消费单信息加载中，请稍候……');//弹出遮罩层        
		var formval = me.getForm().getValues();	
		var url = me.GET_CONSUME_URL+"?PayCode='"+formval.PayCode+"'&WeblisSourceOrgID='"+formval.ClientNo+"'&WeblisSourceOrgName='"+formval.ClientName+"'&AreaID='"+formval.CLIENTELE_AreaID+"'";
        url += "&AreaNo='"+formval.CLIENTELE_ClientNo+"'";
		JShell.Server.get(url,function(data){			
        	if(data.success){
        		//me.onMaskHide();//取消遮罩层
				callback(data);
        	}else{
				me.onMaskHide();//取消遮罩层
				JShell.Msg.error("获取消费单信息失败！错误信息：" + data.msg);
			}							
        });
    }
    //清空与患者相关数据
    ,clearPatientInfo:function(){
		var me = this;
		me.clearConsumeInfo();//清空消费单信息
		//clearPackageTable();//清空订单套餐列表
		//clearPackageItemsTable();//清空订单套餐明细列表
		//clearBarcodeTable();//清空条码列表	
	}
	//清空消费单信息
	,clearConsumeInfo:function(){
		var me = this;
		me.getComponent("CName").setValue("");
		me.getComponent("Age").setValue("");
		me.getComponent("GenderNo").setValue("");
		me.getComponent("PatNo").setValue("");
		me.getComponent("DeptName").setValue("");
		me.getComponent("DoctorName").setValue("");
		me.getComponent("Charge").setValue("");
		me.getComponent("Diag").setValue("");
		me.getComponent("CollectDate").setValue("");
		me.getComponent("OperDate").setValue("");
	}
	 //初始化消费单信息
    ,initConsumeInfo:function(data){
    	var me = this;
    	//当前时间
    	var now = new Date();
    	now = JShell.Date.toString(now);
    	
		me.getComponent("CName").setValue(data.Name);
		me.getComponent("Age").setValue(data.Age);
		me.getComponent("GenderNo").setValue(data.SexName);
		me.getComponent("PatNo").setValue(data.PatNo);
		me.getComponent("DeptName").setValue(data.DeptName);
		me.getComponent("DoctorName").setValue(data.DoctorName);
		me.getComponent("Charge").setValue(data.Price);
		me.getComponent("Diag").setValue(data.DoctorMemo);
		
		me.getComponent("CollectDate").setValue(now);
		me.getComponent("OperDate").setValue(now);
    }
	,changePackageTable:function(UserOrderItem,testItemDetails){
		var me = this;
		me.fireEvent("changetable",UserOrderItem,testItemDetails);
	},
	getSaveData:function(){
		var me = this;
		var formval = me.getForm().getValues();	
		var GenderNo = 1;
		var GenderName = me.getComponent("GenderNo").getValue();
		if(GenderName == "男"){GenderNo = 1	}else if (GenderName == "女"){GenderNo=2}
		var form = {
			NRequestFormNo: "0", //申请号
			ClientNo: formval.ClientNo || "0",//送检单位编号
			ClientName: formval.ClientName,//送检单位名称
			Area: formval.CLIENTELE_ClientNo,//区域编号
			AreaID:formval.CLIENTELE_AreaID,//区域ID
			AreaName:me.getComponent("CLIENTELE_AreaName").getValue(),//区域名称
			
			CName: me.getComponent("CName").getValue(), //姓名
			Age: formval.Age, //年龄
			AgeUnitNo: formval.AgeUnitNo,//年龄单位编号
			AgeUnitName: formval.AgeUnitName,//年龄单位名称
			GenderNo: GenderNo, //性别编号
			GenderName: GenderName, //性别名称
			PatNo: me.getComponent("PatNo").getValue(), //病历号
			
			jztype: me.getComponent("Jztype").getValue(),//就诊类型
			jztypeName: me.getComponent("JztypeName").getValue(),//就诊类型名称
			DeptName: me.getComponent("DeptName").getValue(),//科室名称
			DoctorName: me.getComponent("DoctorName").getValue(),//医生名称
			
			Charge: me.getComponent("Charge").getValue() || "0", //收费
			Diag: me.getComponent("Diag").getValue(), //诊断结果
			Operator: me.getComponent("Operator").getValue(), //采样人
			
			OperDate: JShell.Date.toServerDate(me.getComponent("OperDate").getValue()), //开单日期
			OperTime: JShell.Date.toServerDate(me.getComponent("OperDate").getValue()), //开单时间
			CollectDate: JShell.Date.toServerDate(me.getComponent("CollectDate").getValue()), //采样日期
			CollectTime: JShell.Date.toServerDate(me.getComponent("CollectDate").getValue()), //采样时间
			
			SampleTypeNo:"0",//给样本类型默认一个值，签收的时候样本类型不能为空
			SampleType:"0"
		};
		
		//如果送检单位存在项目，则区域编码=送检单位编码
		if(me.HAS_CLIENT_ITEM){
			form.Area = form.ClientNo;
		}
		
		return form;
	},
	unPayCode:function(){
		var me = this,
			WeblisSourceOrgID = me.getComponent("ClientNo").getValue(),
			WeblisSourceOrgName = me.getComponent("ClientName").getValue(),
			ConsumerAreaID = me.getComponent("CLIENTELE_ClientNo").getValue();
		JShell.Win.open("Shell.class.weixin.sampling.basic.apply.unlock.App", {
			formType:"add",
			width:800,
			WeblisSourceOrgID:WeblisSourceOrgID,
			WeblisSourceOrgName:WeblisSourceOrgName,
			ConsumerAreaID:ConsumerAreaID,
			height:400,
			account:me.account,
			listeners: {
			}
		}).show();		
		
	}
});
