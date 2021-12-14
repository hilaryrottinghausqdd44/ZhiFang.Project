/**
 * 检验确认人、审核人设置公共类
 * @author Jcall
 * @version 2020-07-13
 */
Ext.define('Shell.class.lts.sample.operate.basic.Form', {
	extend:'Shell.ux.form.Panel',
	mixins:['Shell.class.basic.data.Operate'],
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.class.lts.operate.basic.DateTime'
	],
	
	title:'',
	width:560,
	height:250,
	bodyPadding:'20px 10px',
	formtype:'add',
	//布局方式
	layout:'anchor',
	//每个组件的默认属性
	defaults:{
		anchor:'100%',
		labelWidth:70,
		labelAlign:'right'
	},
	//检验确认人弹出Handler,审核人弹出Checker
	OperateType:null,
	//授权操作类型枚举的Name
	OperateTypeText:null,
	//授权操作类型枚举的ID
	OperateTypeID:null,
	//是否是从预授权选择界面选的
	isSelected:false,
	//检验小组
    SectionID:null,
    //授权类型MAP
    FORM_TYPE_MAP:{
    	"Handler":{"OperateUserName":"当前检验人"},
    	"Checker":{"OperateUserName":"当前审核人"}
    },
    
    //授权方式
    OPERATER_TYPE_LIST:[
    	{"value":"0","text":"登录者本人"},
    	{"value":"1","text":"预授权"}
    ],
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.on({
			afterIsAdd:function(){
				//初始化检验人/审核人信息
				me.initOperateUserInfo();
			}
		});
	},
	initComponent:function(){
		var me = this;
		//操作数据
		me.OperateData = Ext.create('Shell.class.basic.data.Operate');
		//内部组件
		me.items = me.createItems();
		//自定义按钮功能栏
		me.buttonToolbarItems = ['->',{
			text:'预授权选择',tooltip:'预授权选择',iconCls:'button-config',handler:function(){
				me.getForm().setValues({
					"OperaterType":me.OPERATER_TYPE_LIST[1].value
				});
				me.onOpenWin();
			}
		},'accept'];
		
		me.callParent(arguments);
	},
	
	//创建内部组件
	createItems:function(){
		var me = this;
		
		var items = [{
			xtype:'fieldcontainer',itemId:'OperateUserInfo',
			layout:{align:'stretch',type:'hbox'},
			fieldLabel:me.FORM_TYPE_MAP[me.OperateType].OperateUserName,
			items:[{
				xtype:'displayfield',itemId:'OperateUserName',name:'OperateUserName',value:''
			},{
				xtype:'displayfield',itemId:'OperaterTypeName',name:'OperaterTypeName',margins: '2 0 0 10'
			}]
		},{
			xtype:'displayfield',fieldLabel:'授&nbsp;权&nbsp;方&nbsp;式',value: ''
		},{
			xtype:'fieldcontainer',itemId:'Pre',layout:{type:'table',columns:4},
			defaults:{width:110,labelWidth:80,labelAlign:'right'},
			items:[{
				xtype:'radio',name:'OperaterType',itemId:'OperaterType0',colspan:1,checked:false,
				boxLabel:me.OPERATER_TYPE_LIST[0].text,
				inputValue:me.OPERATER_TYPE_LIST[0].value,
				labelWidth:80,labelAlign:'right',margin:'2 0 0 10',height:22,
				listeners:{change:function(){me.onOperaterTypeChange();}}
	        },{
				xtype:'label',colspan:3,text:'【' + JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME) + '】'
			},{
				xtype:'radio',name:'OperaterType',itemId:'OperaterType1',colspan:1,checked:false,
				boxLabel:me.OPERATER_TYPE_LIST[1].text,
				inputValue:me.OPERATER_TYPE_LIST[1].value,
				labelWidth:80,labelAlign:'right',margin:'2 0 0 10',height:22,
				listeners:{change:function(){me.onOperaterTypeChange();}}
			},{
				xtype:'displayfield',colspan:1,itemId:'AuthorizeUserName',name:'AuthorizeUserName',value:'【无】'
			},{
				xtype:'displayfield',itemId:'AuthorizeUserID',name:'AuthorizeUserID',fieldLabel:'授权人员ID',hidden:true
			},{
				xtype:'displayfield',colspan:1,itemId:'DateTimeText',width:90,fieldLabel:'有效时间范围',
				value:'',margin:'0 0 0 10'
			},{
				xtype:'fieldcontainer',itemId:'BeginTime',layout:{type:'hbox'},width:170,
				items:[{
					xtype:'datefield',width:95,format:'Y-m-d',labelSeparator:'',labelWidth:5,
					itemId:'BeginTime_Date',name:'BeginTime_Date'
				},{
					xtype:'timefield',width:75,format:'H:i:s',
					itemId:'BeginTime_Time',name:'BeginTime_Time'
				}]
			},{
				xtype:'displayfield',fieldLabel:'',width:10,colspan:3,value:''
			},{
				xtype:'fieldcontainer',itemId:'EndTime',layout:{type:'hbox'},width:170,
				items:[{
					xtype:'datefield',width:95,format:'Y-m-d',labelSeparator:'',labelWidth:5,
					itemId:'EndTime_Date',name:'EndTime_Date'
				},{
					xtype:'timefield',width:75,format:'H:i:s',
					itemId:'EndTime_Time',name:'EndTime_Time'
				}]
			}]
		},{
			xtype:'checkboxfield',margin:'0 5 0 5',boxLabel:'检验确认时,是否提示检验确认人',
			name:'isCheckTip',itemId:'isCheckTip',checked:false,
			listeners:{
				change:function(com,newValue,oldValue,eOpts){
					
				}
			}
		}];
		return items;
	},
	
	//初始化检验人/审核人信息
	initOperateUserInfo:function(){
		var me = this,
			UserId = '',
			UserName = '',
			OperaterType = '',
			OperaterTypeName = '',
			BeginTime = '',
			EndTime = '',
			isCheckTip = false;
			
		if(me.OperateType == 'Handler'){//检验人
			UserId = me.OperateData.getHandlerInfo().Name || '';
			UserName = me.OperateData.getHandlerInfo().Name || '无';
			OperaterType = me.OperateData.getHandlerInfo().OperaterType || me.OPERATER_TYPE_LIST[0].value;
			BeginTime = me.OperateData.getHandlerInfo().BeginTime || '';
			EndTime = me.OperateData.getHandlerInfo().EndTime || '';
			isCheckTip = me.OperateData.getHandlerInfo().isCheckTip || false;
		}else if(me.OperateType == 'Checker'){//审核人
			UserId = me.OperateData.getCheckerInfo().Name || '';
			UserName = me.OperateData.getCheckerInfo().Name || '无';
			OperaterType = me.OperateData.getCheckerInfo().OperaterType || me.OPERATER_TYPE_LIST[0].value;
			BeginTime = me.OperateData.getCheckerInfo().BeginTime || '';
			EndTime = me.OperateData.getCheckerInfo().EndTime || '';
			isCheckTip = me.OperateData.getCheckerInfo().isCheckTip || false;
		}
		
		if(OperaterType == me.OPERATER_TYPE_LIST[0].value){
			OperaterTypeName = me.OPERATER_TYPE_LIST[0].text;
		}else if(OperaterType == me.OPERATER_TYPE_LIST[1].value){
			OperaterTypeName = me.OPERATER_TYPE_LIST[1].text;
		}
		
		me.getForm().setValues({
			"OperateUserName":UserName,
			"OperaterTypeName":'(' + OperaterTypeName + ')',
			"OperaterType":OperaterType,
			"BeginTime_Date":BeginTime.slice(0,10),
			"BeginTime_Time":BeginTime.slice(-8),
			"EndTime_Date":EndTime.slice(0,10),
			"EndTime_Time":EndTime.slice(-8),
			"isCheckTip":isCheckTip
		});
		
		//预授权模式
		if(OperaterType == me.OPERATER_TYPE_LIST[1].value){
			me.getForm().setValues({
				"AuthorizeUserID":UserId,
				"AuthorizeUserName":UserName
			});
		}
	},
	
	//授权方式变化处理
	onOperaterTypeChange:function(){
		var me = this,
			Pre = me.getComponent('Pre'),
			values = me.getForm().getValues(),
			OperaterType = values.OperaterType,
			isShow = null;
			
		if(OperaterType == me.OPERATER_TYPE_LIST[0].value){//登录者本人
			isShow = false;
		}else if(OperaterType == me.OPERATER_TYPE_LIST[1].value){//预授权
			isShow = true;
		}
		if(isShow != null){
			Pre.getComponent('AuthorizeUserName').setVisible(isShow);
			Pre.getComponent('DateTimeText').setVisible(isShow);
			Pre.getComponent('BeginTime').setVisible(isShow);
			Pre.getComponent('EndTime').setVisible(isShow);
		}
		
		//预授权选择弹出窗口
		if(isShow && me.isFristOpened){
			me.onOpenWin();
		}
		me.isFristOpened = true;
	},
	//确定按钮
	onAcceptClick:function(){
        var me = this,
        	values = me.getForm().getValues(),
        	Pre = me.getComponent('Pre'),
        	BeginTime = Pre.getComponent('BeginTime'),
        	EndTime = Pre.getComponent('EndTime'),
        	AuthorizeUserID = Pre.getComponent('AuthorizeUserID').getValue() || '',
        	AuthorizeUserName = Pre.getComponent('AuthorizeUserName').getValue() || '',
        	BeginTime_Date = BeginTime.getComponent('BeginTime_Date').getValue() || '',
        	BeginTime_Time = BeginTime.getComponent('BeginTime_Time').getValue() || '',
        	EndTime_Date = EndTime.getComponent('EndTime_Date').getValue() || '',
        	EndTime_Time = EndTime.getComponent('EndTime_Time').getValue() || '',
        	isCheckTip = me.getComponent('isCheckTip').getValue();
        
        BeginTime_Date = JShell.Date.toString(BeginTime_Date,true) || '';
        BeginTime_Time = JShell.Date.toString(BeginTime_Time,false) || '';
        EndTime_Date = JShell.Date.toString(EndTime_Date,true) || '';
        EndTime_Time = JShell.Date.toString(EndTime_Time,false) || '';
        
        var info = {
        	"OperaterType":values.OperaterType,
        	"isCheckTip":isCheckTip
        };
        
        if(values.OperaterType == '0'){//登陆者本人
        	info.Id = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
        	info.Name = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
        }else if(values.OperaterType == '1'){//预授权模式
        	info.Id = AuthorizeUserID;
        	info.Name = AuthorizeUserName;
        	info.BeginTime = BeginTime_Date + ' ' + BeginTime_Time.slice(-8);
        	info.EndTime = EndTime_Date + ' ' + EndTime_Time.slice(-8);
        	
        	if(info.BeginTime.length > 0 && info.BeginTime.length < 19){
	        	JShell.Msg.error("开始时间格式错误！");
	        	return;
	        }
	        if(info.BeginTime.length > 0 && info.BeginTime.length < 19){
	        	JShell.Msg.error("结束时间格式错误！");
	        	return;
	        }
	        
	        if(!info.BeginTime || !info.EndTime){
	        	JShell.Msg.error("预授权模式下，开始时间和结束时间不能为空！");
	        	return;
	        }
	        if(info.BeginTime > info.EndTime){
	        	JShell.Msg.error("预授权模式下，开始时间不能大于结束时间！");
	        	return;
	        }
        }
        
        if(me.OperateType == 'Handler'){//检验人
			me.OperateData.setHandlerInfo(info);
		}else if(me.OperateType == 'Checker'){//审核人
			me.OperateData.setCheckerInfo(info);
		}
        
		me.fireEvent('save',me);
	},
	
	//更改标题
	changeTitle:function(){
		var me = this;
	},
	
    //预授权选择弹出窗口
	onOpenWin:function(){
		var me = this
	    JShell.Win.open('Shell.class.lts.sample.operate.basic.authorize.Tab',{
			title:"预授权选择",
			maximizable: false, //是否带最大化功能
			SectionID:me.SectionID,
			OperateType:me.OperateType,
			OperateTypeText:me.OperateTypeText,
			OperateTypeID:me.OperateTypeID,
			listeners:{
				save:function(p,data){
					p.close();
					me.onAuthorizeUpdate(data);
				},
				accept:function(p,data){
				    p.close();
				    me.onAuthorizeUpdate(data);
				}
			}
		}).show();
	},
	//临时授权赋值
	onAuthorizeUpdate:function(data){
		var me = this;
		me.getForm().setValues({
			"AuthorizeUserID":data.AuthorizeUserID,
			"AuthorizeUserName":data.AuthorizeUserName,
			"BeginTime_Date":data.BeginTime.slice(0,10),
			"BeginTime_Time":data.BeginTime.slice(-8),
			"EndTime_Date":data.EndTime.slice(0,10),
			"EndTime_Time":data.EndTime.slice(-8)
		});
	}
});