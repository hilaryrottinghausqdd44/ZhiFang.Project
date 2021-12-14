
/**
 * 检验确认人、审核人设置公共类
 * @author liangyl
 * @version 2020-05-08
 */
Ext.define('Shell.class.lts.operate.basic.Form', {
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger',
		'Shell.class.lts.operate.basic.DateTime'
    ],
    formtype:'add',
	title:'检验确认人设置',
	width:520,
	height:270,
	bodyPadding:'20px 10px',
	//布局方式
	layout:'anchor',
	//每个组件的默认属性
	defaults:{
		anchor:'100%',
		labelWidth:70,
		labelAlign:'right'
	},
	//检验小组
    SectionID:1,
    //自定义,用于保存到内存中。检验确认人弹出Handler,审核人弹出Checker
	OperateType:'',
	//授权操作类型枚举的Name
	OperateTypeText:'',
	//授权操作类型枚举的ID
	OperateTypeID:'',
	//是否是从预授权选择界面选的
	isSelected:false,
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	},
	initComponent:function(){
		var me = this;
		me.addEvents('save');
		JShell.System.ClassDict.init('ZhiFang.Entity.LabStar','AuthorizeType',function(){
			if(!JShell.System.ClassDict.AuthorizeType){
    			JShell.Msg.error('未获取到授权类型,请重新刷新');
    			return;
    		}
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = ['accept',{text:'预授权选择',tooltip:'预授权选择',iconCls:'button-config',
			handler:function(){
				me.openWin();
			}
		}];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [{xtype:'fieldcontainer',fieldLabel:'当前审定人',layout:{align:'stretch',type:'hbox'},
	            items:[{
					fieldLabel:'',xtype:'uxCheckTrigger',
					name:'OperateUserName',
					className:'Shell.class.basic.user.CheckGrid',
					classConfig:{TSysCode:'1001001'},
					readOnly: true,locked: true,width:100,
			        value:JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
					listeners:{
						check:function(p,record){
							p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
							p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
							p.close();
						}
					}
				},
	            {xtype:'textfield',itemId:'OperateUserID',name:'OperateUserID',fieldLabel:'登录者本人Id',hidden:true,value:JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)},
	            {xtype: 'label', text: '登录者本人(采用预授权)',margins: '2 0 0 10'}
	            ]
	        },{
		        xtype: 'displayfield',fieldLabel: '检验确认人',value: ''
		    },{
                xtype: 'fieldcontainer',itemId:'Pre',
				layout: {type: 'table',columns: 4 },
				defaults:{width:110,labelWidth:80,labelAlign:'right'},
                items: [ {
				        xtype: 'radiogroup', fieldLabel: '', columns: 1, vertical: true,
						height:22,labelAlign: 'right',colspan: 1,itemId:'cbOperateUserID',
				        items: [
				            { boxLabel: '登录者本人',labelWidth: 80,labelAlign:'right', name: 'OperaterType', inputValue: '0',margin: '2 0 0 10',checked: true}
				        ],
				        listeners:{
				        	change : function(com,newValue,oldValue,eOpts ){
				        		//判断是否是空对象，如果是,返回true,没有选中
				        		if(Ext.encode(newValue)!= "{}"){
				        			me.isComShow(false);
				        		}
				        	}
				        }
			        }, {
						fieldLabel:'',xtype:'uxCheckTrigger',
						name:'AuthorizeUserName',itemId:'AuthorizeUserName',
						className:'Shell.class.basic.user.CheckGrid',
						classConfig:{TSysCode:'1001001'},
						width:100,colspan: 3,
						listeners:{
							check:function(p,record){
								p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
								p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
								p.close();
							}
						}
					},
	                {xtype:'textfield',itemId:'AuthorizeUserID',name:'AuthorizeUserID',fieldLabel:'授权人员ID',hidden:true},
	                {
				        xtype: 'radiogroup', fieldLabel: '', columns: 1, vertical: true,
						height:22,labelAlign: 'right',colspan: 1,itemId:'cbOperateUserID2',
				        items: [
				            { boxLabel: '预授权',labelWidth: 80,labelAlign:'right', name: 'OperaterType', inputValue: '1',margin: '2 0 0 10'}
				        ],
				        listeners:{
				        	change : function(com,newValue,oldValue,eOpts ){
				        		//判断是否是空对象，如果是,返回true,没有选中
				        		if(Ext.encode(newValue)!= "{}"){
				        			if(!me.isSelected){
				        				 //内存中已存在数据,
										if(!JShell.LocalStorage.get('LabStar_TS')){
											me.openWin();
										}else{
											var LabStar_TS = Ext.JSON.decode(JShell.LocalStorage.get('LabStar_TS'));
											if(LabStar_TS.operation && LabStar_TS.operation[me.SectionID]){
												var obj  = LabStar_TS.operation[me.SectionID][me.OperateType];
												if(!obj) me.openWin();
											}else{
												me.openWin();
											}
										}
				        			}
				                  
				        			me.isComShow(true);
				        		}
				        	}
				        }
			        },{
						fieldLabel:'',xtype:'uxCheckTrigger',
						name:'AuthorizeUserName1',itemId:'AuthorizeUserName1',
						className:'Shell.class.basic.user.CheckGrid',
						classConfig:{TSysCode:'1001001'},
						width:100,
						listeners:{
							check:function(p,record){
								p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
								p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
								p.close();
							}
						}
					},{
			        	xtype:'textfield',itemId:'AuthorizeUserID1',name:'AuthorizeUserID1',fieldLabel:'',colspan:1,hidden:true
			        },{
				        xtype: 'displayfield',fieldLabel: '有效时间范围',width:90,value: '',itemId:'DateTimeText',margin: '0 0 0 10'
		            },{
				        xtype: 'datatimefield',width:180,fieldLabel: '',labelWidth: 0,name: 'BeginTime',itemId:'BeginTime',colspan: 1,margin: '5 0 0 0'
				    },{
				        xtype: 'displayfield',fieldLabel: '',width:10,colspan:3,value: ''
				    }, {
				        xtype: 'datatimefield',width:180,fieldLabel: '',labelWidth: 0,name: 'EndTime',itemId:'EndTime'
				    }
				]
            },{
	            xtype:'checkboxfield',margin:'0 5 0 5', boxLabel: '检验确认时,是否提示检验确认人',
	            name: 'isCheckTip',itemId:'isCheckTip',
	            checked:false,//labelSeparator:'',
	            listeners : {
	            	change : function(com,newValue,oldValue,eOpts ){
	            	}
	            }
	        }
		];
		return items;
	},
	//实体对象
	getEntity : function(){
		var me = this,
			values = me.getForm().getValues();	
        var Pre = me.getComponent('Pre');
        //登录者本人
        var cbOperateUserID = Pre.getComponent('cbOperateUserID').getValue();
        //预授权
        var cbOperateUserID2 = Pre.getComponent('cbOperateUserID2').getValue();
        var isLogin = false;
        //确认人是登录者本人或者是预授权
        var OperaterType  ="";
        if(Ext.encode(cbOperateUserID)!='{}'){
        	OperaterType = cbOperateUserID.OperaterType;
        }
        if(Ext.encode(cbOperateUserID2)!='{}'){
        	OperaterType = cbOperateUserID2.OperaterType;
        }
        //确认人
        var AuthorizeUserID = "",AuthorizeUserName="";
        if(OperaterType=='1'){//预授权
        	AuthorizeUserID = values.AuthorizeUserID1;
        	AuthorizeUserName = values.AuthorizeUserName1;
        }else{
        	isLogin = true;
        	AuthorizeUserID = values.AuthorizeUserID;
        	AuthorizeUserName = values.AuthorizeUserName;
        }
        return {
			UserName:values.OperateUserName,//审定人
			UserId:values.OperateUserID,//审定人Id
			AuthorizeUserID:AuthorizeUserID,//检验确认人ID
			AuthorizeUserName:AuthorizeUserName,//检验确认人
			BeginTime:Pre.getComponent('BeginTime').getValue(),//预授权有效开始时间
			EndTime:Pre.getComponent('EndTime').getValue(),//预授权有效结束时间
			isCheckTip:values.isCheckTip ? true : false, //检验确认时,是否提示检验确认人
			isLogin :isLogin  //是否预授权,false-是，true-登录者
		};
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	/**是否第一次登录，预授权相关组件显示隐藏false隐藏*/
	isComShow:function(bo){
		var me = this;
		var Pre = me.getComponent('Pre');
		Pre.getComponent('AuthorizeUserName1').setVisible(bo);
		Pre.getComponent('BeginTime').setVisible(bo);
		Pre.getComponent('EndTime').setVisible(bo);
		Pre.getComponent('DateTimeText').setVisible(bo);
	},

	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		me.callParent(arguments);
		me.loadDatas();
	},
	//初始化加载
	loadDatas:function(){
		var me = this;
		//内存中已存在数据,
		if(JShell.LocalStorage.get('LabStar_TS')){
			me.getLabStar_TS();
		}else{ //默认选择登陆者本人并且隐藏预授权相关设置（第一次打开)
			me.isComShow(false);
			//第一次加载,给登陆者本人赋默认值
			var Pre = me.getComponent('Pre');
            Pre.getComponent('AuthorizeUserName').setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
            Pre.getComponent('AuthorizeUserID').setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));
		}
	},
	//赋值
	loadStorageDatas:function(obj){
		var me = this;
		//第一次加载,给登陆者本人赋默认值
		var Pre = me.getComponent('Pre');
        var cbOperateUserID = Pre.getComponent('cbOperateUserID');
        var cbOperateUserID2 = Pre.getComponent('cbOperateUserID2');
		if(obj.isLogin){//登录者本人(选择)
			Pre.getComponent('AuthorizeUserID').setValue(obj.AuthorizeUserID);
			Pre.getComponent('AuthorizeUserName').setValue(obj.AuthorizeUserName);
			cbOperateUserID.setValue({OperaterType:'0'});
			me.isComShow(false); //隐藏预授权的其他组件
		}else{ //预授权(选择)
			Pre.getComponent('AuthorizeUserID1').setValue(obj.AuthorizeUserID);
			Pre.getComponent('AuthorizeUserName1').setValue(obj.AuthorizeUserName);
			Pre.getComponent('BeginTime').setValue(obj.BeginTime);
			Pre.getComponent('EndTime').setValue(obj.EndTime);
			cbOperateUserID2.setValue({OperaterType:'1'});
		}
		me.getComponent('isCheckTip').setValue(obj.isCheckTip);
	},

    /**本地保存localstorage*/
	saveStore: function(obj) {
		var me = this;
		if(JShell.LocalStorage.get('LabStar_TS') ) {
			var LabStar_TS = Ext.JSON.decode(JShell.LocalStorage.get('LabStar_TS'));
            if(!LabStar_TS.operation[me.SectionID])LabStar_TS.operation[ids[i]]={};
            LabStar_TS.operation[me.SectionID][me.OperateType]= obj.operation[me.SectionID][me.OperateType];
			JShell.LocalStorage.set('LabStar_TS', JSON.stringify(LabStar_TS));
		}else{
			JShell.LocalStorage.set('LabStar_TS', JSON.stringify(obj));
		}
	},
	
	//确定按钮
	onAcceptClick:function(){
        var me = this;
        me.isSelected=false;
		var LabStar_TS = window.top.ZhiFangLabStarTechnicianStation;
		LabStar_TS = LabStar_TS || {};
		LabStar_TS.operation =  LabStar_TS.operation || {};
		LabStar_TS.operation[me.SectionID] = {};
        LabStar_TS.operation[me.SectionID][me.OperateType] = me.getEntity();
		me.saveStore(LabStar_TS);
		me.fireEvent('save',me);
	},
	//内存中存在数据时处理
    getLabStar_TS:function(){
    	var me = this;
    	var LabStar_TS = Ext.JSON.decode(JShell.LocalStorage.get('LabStar_TS'));
		if(LabStar_TS.operation && LabStar_TS.operation[me.SectionID]){
			var obj  = LabStar_TS.operation[me.SectionID][me.OperateType];
			if(obj){
				me.loadStorageDatas(obj);
			}else{//没有数据
				me.isComShow(false); //隐藏预授权的其他组件
			}
		}else{
			me.isComShow(false);
		}
    },
    //预授权选择弹出窗口
	openWin:function(){
		var me = this
		me.isComShow(true);
	    JShell.Win.open('Shell.class.lts.operate.basic.Tab',{
			width:800,
			height:400,
			title:"预授权选择",
			maximizable: false, //是否带最大化功能
			SectionID:me.SectionID,
			OperateType:me.OperateType,
			OperateTypeText:me.OperateTypeText,
			OperateTypeID:me.OperateTypeID,
			listeners:{
				save:function(p){
					me.getLabStar_TS();
					p.close();
				},
				accept:function(panel,obj){
					me.isSelected=true;
					var Pre = me.getComponent('Pre');
					var cbOperateUserID2 = Pre.getComponent('cbOperateUserID2');
				    cbOperateUserID2.setValue({OperaterType:'1'});
				    //临时授权如果选择的小组并不包含当前小组，不赋值
				    if(obj.CurrSection){
				    	//选择赋值
	                    Pre.getComponent('AuthorizeUserID1').setValue(obj.AuthorizeUserID);
						Pre.getComponent('AuthorizeUserName1').setValue(obj.AuthorizeUserName);
						Pre.getComponent('BeginTime').setValue(obj.BeginTime);
						Pre.getComponent('EndTime').setValue(obj.EndTime);
				    }
				    panel.close();
				}
			}
		}).show();
	}
});