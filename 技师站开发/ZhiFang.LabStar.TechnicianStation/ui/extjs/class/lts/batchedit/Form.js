/**
 * 检验单修改
 * @author liangyl
 * @version 2019-01-07
 */
Ext.define('Shell.class.lts.batchedit.Form', {
	extend:'Shell.ux.form.Panel',
	requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
    formtype:'edit',
	title:'检验单修改',
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
	//修改服务地址
    editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateBatchLisTestForm',
    //查询检验单服务地址
    selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormByHQL?isPlanish=true',
    //检验日期
    defaultDate:null,
    //检验小组
    SectionID:1,
   //按钮是否可点击
    BUTTON_CAN_CLICK:true, 
    /**功能按钮栏位置*/
	buttonDock:'top',
	
	/**带功能按钮栏*/
	hasButtontoolbar:true,

	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
	
	},
	initComponent:function(){
		var me = this;
		
		me.addEvents('onSaveClick');

        me.buttonToolbarItems=['save','reset'];

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
				classConfig: { TSysCode: '1001001' },
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

	//@overwrite 获取新增的数据
	getAddParams:function(list){
		var me = this,
			values = me.getForm().getValues();
			
		//要更新的样本单实体列表
		var entityList =[];
		for(var i=0;i<list.length;i++){
			
			var obj = {
				Id:list[i].data.LisTestForm_Id
			}

			if (values.LisTestForm_GSampleInfo) obj.GSampleInfo = values.LisTestForm_GSampleInfo;
			if (values.LisTestForm_FormMemo) obj.FormMemo = values.LisTestForm_FormMemo;
			if (values.LisTestForm_SampleSpecialDesc) obj.SampleSpecialDesc = values.LisTestForm_SampleSpecialDesc;
			if (values.LisTestForm_TestComment) obj.TestComment = values.LisTestForm_TestComment;
			if (values.LisTestForm_TestInfo) obj.TestInfo = values.LisTestForm_TestInfo;

			if(values.LisTestForm_MainTesterId){
				obj.MainTester = values.LisTestForm_MainTester;
				obj.MainTesterId = values.LisTestForm_MainTesterId;
			}	
			if (values.LisTestForm_GSampleTypeID) obj.GSampleTypeID = values.LisTestForm_GSampleTypeID; 
			if (values.LisTestForm_GSampleTypeCName) obj.GSampleType = values.LisTestForm_GSampleTypeCName; 
			entityList.push(obj);
		}
		//修改字段
		var fields = [];
		for (var o in obj) {
			fields.push(o);
		}
		var entity={
			entityList: entityList,
			fields: fields.join(",")
		};
		return entity;
	},
    /**保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this;
		if(!me.BUTTON_CAN_CLICK)return;
		me.fireEvent('onSaveClick', me);
	},
	/**批量修改保存
	 *list 已选的检验单 */
	onSave:function(list){
		var me = this;
		
		var url =  JShell.System.Path.ROOT + me.editUrl;
		var params = me.getAddParams(list);
		if(!params) return;	
		
        
		me.showMask(me.saveText);//显示遮罩层
		me.BUTTON_CAN_CLICK = false;
		
		JShell.Server.post(url,Ext.encode(params),function(data){
			me.hideMask();//隐藏遮罩层
			me.BUTTON_CAN_CLICK=true;
			if(data.success){
				me.fireEvent('save',me);
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
	}    
});