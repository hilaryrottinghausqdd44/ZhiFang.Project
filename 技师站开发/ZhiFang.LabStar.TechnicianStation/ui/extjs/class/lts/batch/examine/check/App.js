/**
 * 批量检验审核-批量检验单审核
 * @author liangyl	
 * @version 2021-02-23
 */
Ext.define('Shell.class.lts.batch.examine.check.App',{
    extend:'Shell.class.lts.batch.examine.basic.App',
    title:'检验单批量审核',
   
	//样本单批量审定服务
	saveUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormBatchCheck',
    //检验单列表
	TestFormGrid:'Shell.class.lts.batch.examine.check.Grid',
  
    UserLable:'检验确认(初审)人',
	
	/**顶部工具栏*/
	createTopToolBar: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
       
        var layout = {type:'table',columns:5},
            defaults ={labelWidth:70,width:180,labelAlign:'right'};

	    buttonToolbarItems.push({
	        xtype: 'displayfield',colspan:2,margin: '5 0 5 15',
	        value: '<span style="font-weight:bold;color:blue;">仅针对完成初审的检验单</span>'
	    },{
            xtype:'fieldcontainer',fieldLabel:'',layout:layout,defaults:defaults,
            colspan:2,width:600,itemId:'Row2',
            items :[{
				fieldLabel:'就诊类型',xtype:'uxCheckTrigger',
				itemId:'LisTestForm_LisPatient_SickType',
				colspan:2,width:200,emptyText:'就诊类型',
				className:'Shell.class.lts.sicktype.CheckGrid',
				listeners:{
					check:function(p,record){
						p.setValue(record ? record.get('LBSickType_CName') : '');
						p.nextNode().setValue(record ? record.get('LBSickType_Id') : '');
						p.close();
					}
				}
			},{
				xtype: 'textfield',fieldLabel:'就诊类型ID',itemId:'LisTestForm_LisPatient_SickTypeID',hidden:true
			},{
				xtype:'uxdatearea',itemId:'DateValue',
				fieldLabel:'检验日期',
				value:me.DEFAULT_DATE_VALUE,
				colspan:3,width:280,margin:"0px 0px 0px 100px",
				listeners:{
					enter:function(){me.onSearch();}
				}
			}]
        },{
            xtype:'fieldcontainer',fieldLabel:'',layout:layout,defaults:defaults,
            colspan:1,width:600,itemId:'Row3',
            items :[{
				fieldLabel:'科室',xtype:'uxCheckTrigger',
				itemId:'LisTestForm_LisPatient_DeptName',
				className:'Shell.class.basic.dept.CheckGrid',
				classConfig:{TSysCode:'1001101'},//送检科室
				colspan:2,width:200,emptyText:'科室',
				listeners:{
					check:function(p,record){
						p.setValue(record ? record.get('HRDeptIdentity_HRDept_CName') : '');
						p.nextNode().setValue(record ? record.get('HRDeptIdentity_HRDept_Id') : '');
						p.close();
					}
				}
			},{
				xtype: 'textfield',fieldLabel:'科室ID',itemId:'LisTestForm_LisPatient_DeptID',hidden:true
			},{
	            xtype: 'textfield',itemId: 'StartSampleNo', fieldLabel: '样本号范围',emptyText: '开始样本号',labelAlign:'right',width:170,labelWidth:70,colspan:1,margin:"0px 0px 0px 100px"
	        },{
	        	xtype: 'textfield',itemId:'EndSampleNo',emptyText: '结束样本号',fieldLabel:'-',labelSeparator:'',labelWidth:10,width:110,colspan:1
	        }]
        },'accept');
		
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			//布局方式
			layout:{type:'table',columns:2},
			items: buttonToolbarItems
		});
	},
	
	/**
	 * 获取查询条件
	 * itemResultFlag 都不选的话 参数值：0,0,0,全选为：1,1,1
	 * */
	getParams : function(){
		var me = this,
		    itemResultFlag= "",
		    buttonsBottomToolbar = me.getComponent('buttonsBottomToolbar'),
		    buttonsToolbar = me.getComponent('buttonsToolbar'),
		    Row2 = buttonsToolbar.getComponent('Row2'),
		    Row3 = buttonsToolbar.getComponent('Row3');
		
        var StartDate = Row2.getComponent('DateValue').getValue().start ? JShell.Date.toString(Row2.getComponent('DateValue').getValue().start,true) : "";
        var EndDate = Row2.getComponent('DateValue').getValue().end ? JShell.Date.toString(Row2.getComponent('DateValue').getValue().end,true) : "";

		return{
	        SickTypeID:Row2.getComponent('LisTestForm_LisPatient_SickTypeID').getValue(),//就诊类型
	        StartDate:StartDate,//检验日期开始日期
	        EndDate:EndDate,//检验日期结束日期
	        DeptID:Row3.getComponent('LisTestForm_LisPatient_DeptID').getValue(),//送检科室
	        beginSampleNo:Row3.getComponent('StartSampleNo').getValue(),//开始样本号        
	        endSampleNo:Row3.getComponent('EndSampleNo').getValue(),//结束样本号
	        ExecutorID:buttonsBottomToolbar.getComponent('AuthorizeUserID').getValue(),//执行人
	        ExecutorName:buttonsBottomToolbar.getComponent('AuthorizeUserName').getValue()
		}
	},
	//确定(查询)
	onAcceptClick : function(){
		var me = this,
		    buttonsToolbar = me.getComponent('buttonsToolbar'),
		    Row2 = buttonsToolbar.getComponent('Row2');
		
        var DateValue = Row2.getComponent('DateValue');
        //时间校验
        if(!DateValue.isValid())return;
		me.Tab.onSearchTestForm(me.getParams());
	}
});