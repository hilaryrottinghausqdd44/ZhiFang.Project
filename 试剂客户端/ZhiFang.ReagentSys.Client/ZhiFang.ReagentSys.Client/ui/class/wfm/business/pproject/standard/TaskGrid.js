/**
 * 标准项目任务列表
 * @author liangyl	
 * @version 2017-04-06
 */
Ext.define('Shell.class.wfm.business.pproject.standard.TaskGrid', {
    extend: 'Shell.class.wfm.business.pproject.basic.TaskGrid',
    title: '项目任务列表',
    IsStandard:1,
    ProjectID:null,
    /*项目类型**/
    ItemTypeEnum:null,
    /*标准总工作量天数**/
    EstiWorkload:0,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        //标准总工作量天数
        var EstiWork=me.getComponent('buttonsToolbar').getComponent('EstiWorkload');
       
        if(EstiWork && me.EstiWorkload){
        	EstiWork.setText("标准总工作量天数:"+me.EstiWorkload);
        }
    },
   
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = me.callParent(arguments);
        columns.splice(2,0,{
			text: '预计工作量(天)',dataIndex: 'PProjectTask_EstiWorkload',width: 100,sortable: false, 
			menuDisabled: false,
			editor: {
				xtype: 'numberfield',minValue: 0
			}, defaultRenderer: true
		},{
			text: '标准工作量(天)',dataIndex: 'PProjectTask_StandWorkload',width: 100,sortable: false, 
			menuDisabled: false,
			editor: {
				xtype: 'numberfield',minValue: 0
//				 listeners:{
//			    	change:function(file,value,eOpt){
//			    		if(value &&  value> Number(me.EstiWorkload)){
//			    			JShell.Msg.error('单项值标准工作量不能大于标准总工作量天数');
//		                	file.setValue('0');
//			    		}
//			    	}
//			    }
			}, defaultRenderer: true
		},{
			text: '标准开始时间',dataIndex: 'PProjectTask_PlanTheNextFewDays',width: 85,sortable: false, 
			menuDisabled: false,editor: {
				xtype: 'numberfield',minValue:1, allowDecimals:false,
				 listeners:{
//			    	change:function(file,value,eOpt){
//			    		
//			    		var records = me.getSelectionModel().getSelection();
//						if (records.length == 0) {
//							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
//							return;
//						}
//						var PlanTheEndFewDays=records[0].get('PProjectTask_PlanTheEndFewDays');
//						if()
//			    	}
			    }
	
			 }, defaultRenderer: true
		},{
			text: '标准结束时间',dataIndex: 'PProjectTask_PlanTheEndFewDays',width: 85,sortable: false, 
			menuDisabled: false,editor: {
				xtype: 'numberfield',minValue:1, allowDecimals:false,
			 }, defaultRenderer: true
		});
        return columns;
   },
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =  me.callParent(arguments);
		buttonToolbarItems.push('-',{
			xtype: 'button',iconCls: 'button-add',text: '复制标准项目任务',tooltip: '复制标准项目任务',
			handler: function(but) {
                if(me.ProjectID){
                	me.onCopyTaskClick(me.ProjectID);
                }
			}
		});	
		buttonToolbarItems.push({
			xtype: 'label',
			text: '标准总工作量:0',
			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;",
			itemId: 'EstiWorkload',
			name: 'EstiWorkload'
		});
		
		return buttonToolbarItems;
	},
	  onSaveClick:function(){
		var me = this;
		if(!me.IsValid()) {
			return;
		}
		var records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		if(len == 0) return;
		
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		var isExect=true;
		var count=0;
		
		var msg='';
        //单项值标准工作量不能大于标准总工作量天数。
        me.store.each(function(record) {
        	var StandWorkload=record.get('PProjectTask_StandWorkload');
        	var CName=record.get('PProjectTask_CName');
        	var count=Number(me.EstiWorkload);
        	var PlanTheNextFewDays=record.get('PProjectTask_PlanTheNextFewDays');
        	var PlanTheEndFewDays=record.get('PProjectTask_PlanTheEndFewDays');

        	if(StandWorkload ){
        		if(Number(StandWorkload)>count){
        			msg+=CName+' 的单项值标准工作量不能大于标准总工作量天数'+'</br>';
					isExect=false;
				}
        	}
        	if(PlanTheNextFewDays &&  PlanTheEndFewDays){
        		if(Number(PlanTheNextFewDays)>Number(PlanTheEndFewDays)){
	        		msg+=CName+' 的标准开始时间不能大于标准结束时间'+'</br>';
	        		isExect=false;
	        	}
        	}
        });
        if(!isExect){
        	JShell.Msg.error(msg);
			return;
        }
        me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			var rec = records[i];
			me.onSave(rec);
		}
	},
   onSave:function(rec){
		var me = this;
		var Id=rec.get('PProjectTask_Id');
		var url=me.addUrl;
		if(Id){
			url=me.editUrl;
		}
		url = JShell.System.Path.getRootUrl(url);
		if(!JShell.System.Cookie.map.USERID) {
			JShell.Msg.error('用户登录信息不存在，请重新登录后操作！');
			return;
		}
		var params = {};
		params.entity = {
			CName:rec.get('PProjectTask_CName'),
		    DispOrder:rec.get('PProjectTask_DispOrder'),
		    IsStandard:me.IsStandard,
		    IsUse:1
		};
		if(rec.get('PProjectTask_EstiWorkload')){
			params.entity.EstiWorkload=rec.get('PProjectTask_EstiWorkload');
		}
		if(rec.get('PProjectTask_StandWorkload')){
			params.entity.StandWorkload=rec.get('PProjectTask_StandWorkload');
		}
		if(rec.get('PProjectTask_PlanTheNextFewDays')){
			params.entity.PlanTheNextFewDays=rec.get('PProjectTask_PlanTheNextFewDays');
		}
		if(rec.get('PProjectTask_PlanTheEndFewDays')){
			params.entity.PlanTheEndFewDays=rec.get('PProjectTask_PlanTheEndFewDays');
		}
		if(me.ProjectID){
			params.entity.PProject = {
				Id:me.ProjectID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(rec.get('PProjectTask_PTaskID')){
			params.entity.PTaskID = rec.get('PProjectTask_PTaskID');
		}
		if(Id){
			params.entity.Id=Id; 
			params.fields = 'Id,CName,EstiWorkload,StandWorkload,DispOrder,EstiStartTime,EstiEndTime,PlanTheNextFewDays,PlanTheEndFewDays';
		}else{
			var userId= JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			if(userId){
			    params.entity.Creater = {
					Id:userId,
					DataTimeStamp:[0,0,0,0,0,0,0,0]
				};
			}
		}
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				if(data.success){
					if(rec){
						rec.set('PProjectTask_CName',rec.get('PProjectTask_CName'));
						rec.set('PProjectTask_EstiWorkload',rec.get('PProjectTask_EstiWorkload'));
						rec.set('PProjectTask_DispOrder',rec.get('PProjectTask_DispOrder'));
						rec.set('PProjectTask_PTaskID',rec.get('PProjectTask_PTaskID'));
						rec.set('PProjectTask_StandWorkload',rec.get('PProjectTask_StandWorkload'));
						rec.set('PProjectTask_PlanTheNextFewDays',rec.get('PProjectTask_PlanTheNextFewDays'));
						rec.set('PProjectTask_PlanTheEndFewDays',rec.get('PProjectTask_PlanTheEndFewDays'));
						rec.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(rec){
						rec.set('PProjectTask_CName',rec.get('PProjectTask_CName'));
						rec.set('PProjectTask_EstiWorkload',rec.get('PProjectTask_EstiWorkload'));
						rec.set('PProjectTask_DispOrder',rec.get('PProjectTask_DispOrder'));
						rec.set('PProjectTask_PTaskID',rec.get('PProjectTask_PTaskID'));
						rec.set('PProjectTask_StandWorkload',rec.get('PProjectTask_StandWorkload'));
						rec.set('PProjectTask_PlanTheNextFewDays',rec.get('PProjectTask_PlanTheNextFewDays'));
						rec.set('PProjectTask_PlanTheEndFewDays',rec.get('PProjectTask_PlanTheEndFewDays'));
						rec.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) 
					me.fireEvent('save',me);
				}
			});
		},200);
	},
	/**复制拷贝项目任务*/
	onSaveCopyProjectTask : function(fromProjectID,toProjectID,p) {
		var me = this,
			url = '/SingleTableService.svc/PM_UDTO_CopyProjectTask';
		url += '?fromProjectID=' + fromProjectID+'&toProjectID='+toProjectID+'&isStandard=true';
		url = JShell.System.Path.getRootUrl(url);
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.onSearch();
				p.close();
			} else {
				var msg = data.msg;
				if(!data.value){
					msg='选择的项目任务为空,不能复制!';
				}
				JShell.Msg.error(msg);
			}
		});
	}
});