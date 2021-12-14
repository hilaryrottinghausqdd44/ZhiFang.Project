/**
 * 批量申请
 * @author liangyl
 * @version 2019-12-26
 */
Ext.define('Shell.class.lts.batchadd.Form', {
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    formtype:'add',
	title:'批量申请',
	width:665,
	height:400,
	bodyPadding:'20px 10px',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	defaults:{
		width:240,
		labelWidth:80,
		labelAlign:'right'
	},
	autoScroll:true,
	//新增服务地址
    addUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_AddBatchLisTestForm',
    //查询检验单服务地址
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormByHQL?isPlanish=true',
    //检验日期
    defaultDate:null,
    //检验小组
    SectionID:null,
   //按钮是否可点击
    BUTTON_CAN_CLICK:true, 
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		//样本起始号
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var GSample = buttonsToolbar.getComponent('GSampleNo');
				
        me.getGSampleNo(function(list){
        	if(list.length==0){
        		GSample.setValue(1);
        		return;
        	}
        	var GSampleNo=1;//LisTestForm_GSampleNo
        	var arr = [],arr2=[];
        	for(var i=0;i<list.length;i++){
        		var isNum= me.isRealNum(list[i].LisTestForm_GSampleNo);
        		if(isNum)arr.push({LisTestForm_GSampleNo:Number(list[i].LisTestForm_GSampleNo)});
        	}
        	if(arr.length>0){
        		arr2 = JShell.Array.reorder(arr, "LisTestForm_GSampleNo", true);
        	}
        	if(arr2.length>0)GSampleNo=arr2[0].LisTestForm_GSampleNo+1;
        	GSample.setValue(GSampleNo);
        });
	},
	initComponent:function(){
		var me = this;
		me.initDate();
		//自定义按钮功能栏
		me.buttonToolbarItems = ['save','reset','->',{text:'关闭',tooltip:'关闭',iconCls:'button-del',
			handler:function(){
				if(me.BUTTON_CAN_CLICK)me.close();
			}
		}];
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems:function(){
		var me = this;
		var items = [
			{xtype:'textfield',itemId:'LisTestForm_GSampleTypeID',name:'LisTestForm_GSampleTypeID',fieldLabel:'样本类型ID',hidden:true},
		    {fieldLabel: '样本类型',name: 'LisTestForm_GSampleTypeCName',itemId:'LisTestForm_GSampleTypeCName',
                xtype: 'uxCheckTrigger',className: 'Shell.class.lts.sampletype.CheckGrid',classConfig:{checkOne:true},
                listeners :{
					check: function(p, record) {
						var CName = me.getComponent('LisTestForm_GSampleTypeCName');
						var ID = me.getComponent('LisTestForm_GSampleTypeID');
						CName.setValue(record ? record.get('LBSampleType_CName') : '');
						ID.setValue(record ? record.get('LBSampleType_Id') : '');
						p.close();
					}
				}
            },
            {xtype:'fieldcontainer',fieldLabel:'小组样本描述',layout:{align:'stretch',type:'hbox'},
                items:[
                    {fieldLabel:'',xtype:'textarea',name:'LisTestForm_GSampleInfo',flex:1,height:22},
                    {text:'',tooltip:'选择小组样本描述',iconCls:'button-add',xtype:'button',margins: '0 1 0 1',
	                    handler:function(){
	                    	var values = me.getForm().getValues();
	                    	me.openWin('小组样本描述',values.LisTestForm_GSampleInfo,'LisTestForm_GSampleInfo');
	                    }
	                }
                ]
            },
            {xtype:'fieldcontainer',fieldLabel:'检验样本备注',layout:{align:'stretch',type:'hbox'},
                items:[
                    {fieldLabel:'',xtype:'textarea',name:'LisTestForm_FormMemo',flex:1,height:22},
                    {text:'',tooltip:'选择检验样本备注',iconCls:'button-add',xtype:'button',margins: '0 1 0 1',
	                    handler:function(){
	                    	var values = me.getForm().getValues();
	                    	me.openWin('检验样本备注',values.LisTestForm_FormMemo,'LisTestForm_FormMemo');
	                    }
	                }
                ]
            },
            {xtype:'fieldcontainer',fieldLabel:'样本特殊性状',layout:{align:'stretch',type:'hbox'},
                items:[
                    {fieldLabel:'',xtype:'textarea',name:'LisTestForm_SampleSpecialDesc',flex:1,height:22},
                    {text:'',tooltip:'选择样本特殊性状',iconCls:'button-add',xtype:'button',margins: '0 1 0 1',
	                    handler:function(){
	                    	var values = me.getForm().getValues();
	                    	me.openWin('特殊性状描述',values.LisTestForm_SampleSpecialDesc,'LisTestForm_SampleSpecialDesc');
	                    }
	                }
                ]
            },
            {xtype:'fieldcontainer',fieldLabel:'检验备注',layout:{align:'stretch',type:'hbox'},
                items:[
                    {fieldLabel:'',xtype:'textarea',name:'LisTestForm_TestComment',flex:1,height:22},
                    {text:'',tooltip:'选择检验备注',iconCls:'button-add',xtype:'button',margins: '0 1 0 1',
	                    handler:function(){
	                    	var values = me.getForm().getValues();
	                    	me.openWin('检验备注',values.LisTestForm_TestComment,'LisTestForm_TestComment');
	                    }
	                }
                ]
            },
            {xtype:'fieldcontainer',fieldLabel:'检验评语',layout:{align:'stretch',type:'hbox'},
                items:[
                    {fieldLabel:'',xtype:'textarea',name:'LisTestForm_TestInfo',flex:1,height:22},
                    {text:'',tooltip:'选择检验评语',iconCls:'button-add',xtype:'button',margins: '0 1 0 1',
	                    handler:function(){
	                    	var values = me.getForm().getValues();
	                    	me.openWin('检验评语',values.LisTestForm_TestInfo,'LisTestForm_TestInfo');
	                    }
	                }
                ]
            },{
				fieldLabel:'检验人',xtype:'uxCheckTrigger',
				name:'LisTestForm_MainTester',itemId:'LisTestForm_MainTester',
				className:'Shell.class.basic.user.CheckGrid',
				classConfig:{TSysCode:'1001003'},
				listeners:{
					check:function(p,record){
						p.setValue(record ? record.get('HREmpIdentity_HREmployee_CName') : '');
						p.nextNode().setValue(record ? record.get('HREmpIdentity_HREmployee_Id') : '');
						p.close();
					}
				}
			},
			{xtype:'textfield',itemId:'LisTestForm_MainTesterId',name:'LisTestForm_MainTesterId',fieldLabel:'样本类型ID',hidden:true}
		];
		return items;
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this,
			items = me.dockedItems || [];
			
		if(me.hasButtontoolbar){
			var buttontoolbar = me.createButtontoolbar();
			if(buttontoolbar) items.push(buttontoolbar);
		}
		items.push(me.createButtonToolbarItems());
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			items = [];
		items.push({
            xtype: 'datefield',format: 'Y-m-d',fieldLabel: '检验日期',
			name: 'GTestDate',itemId: 'GTestDate',value:me.defaultDate,
			width:155,labelWidth:60,labelAlign:'right',emptyText:'检验日期'
        },{
        	fieldLabel:'样本起始号',name:'GSampleNo',itemId:'GSampleNo',xtype:'textfield',
        	width:160,labelWidth:80,labelAlign:'right',emptyText:'样本起始号'//xtype:'numberfield',value:1,
        },{
        	fieldLabel:'样本数量',name:'GSampleNoForOrder',itemId:'GSampleNoForOrder',xtype:'numberfield',value:10,
        	width:130,labelWidth:60,labelAlign:'right',emptyText:'样本数量',
        },{text:'保存',tooltip:'保存',iconCls:'button-save',hidden:true,
	        handler:function(){
	        	
	        }
	    });
		return Ext.create('Shell.ux.toolbar.Button',{
			dock:'top',
			itemId:'buttonsToolbar2',
			items:items
		});
	},	
	/**初始化检验日期*/
	initDate: function() {
		var me = this;
		var Sysdate = JShell.System.Date.getDate();
//		var Sysdate=new Date();
		
		me.defaultDate =  JShell.Date.toString(Sysdate,true)
	},
	//@overwrite 获取新增的数据
	getAddParams:function(){
		var me = this,
			values = me.getForm().getValues();
			
		var entity = {
			GSampleInfo:values.LisTestForm_GSampleInfo,
			FormMemo:values.LisTestForm_FormMemo,
			SampleSpecialDesc:values.LisTestForm_SampleSpecialDesc,
			TestComment:values.LisTestForm_TestComment,
			TestInfo:values.LisTestForm_TestInfo
		
		};
		if (values.LisTestForm_GSampleTypeID) entity.GSampleTypeID = values.LisTestForm_GSampleTypeID; 
		if (values.LisTestForm_GSampleTypeCName) entity.GSampleType = values.LisTestForm_GSampleTypeCName;
		if (values.LisTestForm_MainTesterId) {
			entity.MainTester = values.LisTestForm_MainTester;
			entity.MainTesterId = values.LisTestForm_MainTesterId;
		}	
		return entity;
	},
    /**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		if(!me.BUTTON_CAN_CLICK)return;
		if(!me.getForm().isValid()) return;
		
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var GTestDate = buttonsToolbar.getComponent('GTestDate').getValue();
		var GSampleNo = buttonsToolbar.getComponent('GSampleNo').getValue();
		var GSampleNoForOrder = buttonsToolbar.getComponent('GSampleNoForOrder').getValue();
		
		if(!GTestDate){
			JShell.Msg.alert('请输入检验日期');
			return;
		}
		if(!GSampleNo){
			JShell.Msg.alert('请输入样本起始号');
			return;
		}
		if(!GSampleNoForOrder){
			JShell.Msg.alert('请输入样本数量');
			return;
		}
		
		var url =  JShell.System.Path.ROOT + me.addUrl;
		var params = me.getAddParams();
		if(!params) return;	
		
        var entity = {
        	sampleInfo:JSON.stringify(params),
        	testDate:JShell.Date.toString(GTestDate,true),
        	sectionID:me.SectionID,
        	startSampleNo:GSampleNo,
        	sampleCount:GSampleNoForOrder
        };
		
		me.showMask(me.saveText);//显示遮罩层
		me.BUTTON_CAN_CLICK = false;
		
		JShell.Server.post(url,Ext.encode(entity),function(data){
			me.hideMask();//隐藏遮罩层
			me.BUTTON_CAN_CLICK=true;
			if(data.success){
				me.fireEvent('save',me);
				me.isAdd();
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick:function(){
		var me = this;
		if(!me.PK){
			this.getForm().reset();
			var MainTester = me.getComponent('LisTestForm_MainTester');
			var MainTesterId = me.getComponent('LisTestForm_MainTesterId');
		
            MainTester.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME));
            MainTesterId.setValue(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID));

		}else{
			me.getForm().setValues(me.lastData);
		}
	},
	//打开小组评语选择窗口
	openWin:function(TypeName,TestInfo,name){
		var me = this;
		if(!me.SectionID){
			JShell.Msg.alert('小组ID不能为空!');
			return;
		}
		var values = me.getForm().getValues();
    	JShell.Win.open('Shell.class.lts.batchadd.testInfo.App',{
			width:620,
			height:420,
			SectionID:me.SectionID,
			//小组评语名称
			TypeName:TypeName,
			//评语内容
			TestInfo:TestInfo,
			listeners:{
				checked : function(obj){
					me.getForm().setValues({
						[name]:obj.LBPhrase_CName
					});
				}
			}
		}).show();
	},
	/**获取默认样本起始号*/
	getGSampleNo: function(callback) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectUrl);
			
		url += '&fields=LisTestForm_GSampleNo';
		url +="&where=listestform.GTestDate='"+me.defaultDate+"' and listestform.LBSection.Id="+me.SectionID;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value ? data.value.list : [];
				callback(list);
			} 
		});
	},
	 /**判断值是否是数字
	 * true:数值型的，false：非数值型
	 * */
    isRealNum : function (val){
	    // isNaN()函数 把空串 空格 以及NUll 按照0来处理 所以先去除
	    if(val === "" || val ==null){
	        return false;
	    }
	    if(!isNaN(val)){
	        return true;
	    }else{
	        return false;
	    }
	}         
});