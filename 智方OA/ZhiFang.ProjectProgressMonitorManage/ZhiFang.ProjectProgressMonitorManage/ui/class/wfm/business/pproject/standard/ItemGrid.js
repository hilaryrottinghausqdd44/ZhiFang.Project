/**
 * 项目管理项目任务列表
 * @author liangyl	
 * @version 2017-04-06
 */
Ext.define('Shell.class.wfm.business.pproject.standard.ItemGrid', {
    extend: 'Shell.class.wfm.business.pproject.basic.TaskGrid',
    title: '项目任务列表',
    IsStandard:0,
    ProjectID:null,
     /**项目类型数组*/
    ItemTypeList:[],
	ItemTypeEnum:{},
	 /**项目计划开始时间*/
	ItemEstiStartTime:null,
	 /**项目计划完成时间*/
	ItemEstiEndTime:null,
	/**预计总工作量(天)*/
	EstiWorkload:0,
	/**项目类型*/
	TypeName:null,
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.showText();
      
    },
    
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = me.callParent(arguments);
        columns.splice(2,0,{
			text: '标准工作量(天)',dataIndex: 'PProjectTask_StandWorkload',width: 100,sortable: false, 
			menuDisabled: false,defaultRenderer: true
		},{
			text: '预计工作量(天)',dataIndex: 'PProjectTask_EstiWorkload',width: 100,sortable: false, 
			menuDisabled: false,
			editor: {
				xtype: 'numberfield',minValue: 0
			}, defaultRenderer: true
		},{
			menuDisabled: false,text: '预计开始时间',dataIndex: 'PProjectTask_EstiStartTime',width: 100,sortable: false,
			type: 'date',xtype: 'datecolumn',format: 'Y-m-d', isDate: true,
			editor: {
				xtype: 'datefield',format: 'Y-m-d', value :'',maxValue:JShell.Date.getDate(me.ItemEstiEndTime),
				minValue:JShell.Date.getDate(me.ItemEstiStartTime),//editable:false
			},
			defaultRenderer: true
		}, 
		{
			text: '预计完成时间',dataIndex: 'PProjectTask_EstiEndTime',width: 100,sortable: false,
			menuDisabled: false,type: 'date',xtype: 'datecolumn',format: 'Y-m-d', isDate: true,
			editor: {
				xtype: 'datefield',format: 'Y-m-d', value :'',maxValue:JShell.Date.getDate(me.ItemEstiEndTime),
				minValue:JShell.Date.getDate(me.ItemEstiStartTime),//editable:false
			},	defaultRenderer: true
		},{
			text: '标准开始时间',dataIndex: 'PProjectTask_PlanTheNextFewDays',width: 85,sortable: false, 
			menuDisabled: false,hidden:false
		},{
			text: '标准结束时间',dataIndex: 'PProjectTask_PlanTheEndFewDays',width: 85,sortable: false, 
			menuDisabled: false,hidden:false
		});
        return columns;
   },
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =  me.callParent(arguments);
		buttonToolbarItems.splice(3, 0,'-',{
			xtype: 'button',iconCls: 'button-add',text: '导入标准项目任务',tooltip: '导入项目任务',
			handler: function(but) {
			if(me.getStore().getCount()>0){
				Ext.MessageBox.show({
					title: '操作确认消息',
					msg: '该项目已有计划任务 是否还导入标准任务',
					width: 300,
					zIndex: 999990,
					buttons: Ext.MessageBox.OKCANCEL,
					fn: function(btn) {
						if(btn == 'ok') {
							me.onAddStandardTask(me.ProjectID);
						}
					},
					icon: Ext.MessageBox.QUESTION
				});
			}else{
					me.onAddStandardTask(me.ProjectID);
				}
			}
		},'-',{
			xtype: 'button',iconCls: 'button-add',text: '导入其他选项',tooltip: '导入其他选项',
			handler: function(but) {
                if(me.ProjectID){
                	me.onCopyTaskClick(me.ProjectID);
                }
			}
		});	
		buttonToolbarItems.push('-',{
			xtype: 'label',
			text: '标准总工作量:0',
//			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;",
			itemId: 'StandardWorkload',
			name: 'StandardWorkload'
		},{
			xtype: 'label',
			text: '预计总工作量:0',
			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;",
			itemId: 'EstiWorkload',
			name: 'EstiWorkload'
		},{
			xtype: 'label',
			text: '预计开始时间:',
			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;",
			itemId: 'StartTime',
			name: 'StartTime'
		},{
			xtype: 'label',
			text: '项目类型:',
			margin: '0 0 0 10',
			style: "font-weight:bold;color:blue;",
			itemId: 'TypeName',
			name: 'TypeName'
		});
		
		return buttonToolbarItems;
	},
	 /**显示标准总工作量,预计总工作量,预计开始时间,项目类型*/
	showText:function(){
    	var me=this;
    	var StandardWorkload=me.getComponent('buttonsToolbar').getComponent('StandardWorkload');
        var EstiWorkload=me.getComponent('buttonsToolbar').getComponent('EstiWorkload');
        var StartTime=me.getComponent('buttonsToolbar').getComponent('StartTime');
        var TypeName=me.getComponent('buttonsToolbar').getComponent('TypeName');
        var Workload=me.getEstiWorkload(me.TypeID);
        StandardWorkload.setText('标准总工作量:'+Workload);
        EstiWorkload.setText('预计总工作量:'+me.EstiWorkload);
        var StartDate=JcallShell.Date.toString(me.ItemEstiStartTime,true);
        StartTime.setText('预计开始时间:'+StartDate);
        TypeName.setText('项目类型:'+ me.TypeName);
    },
    onSaveClick:function(){
		var me = this;
//		me.focus(false);
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
        //预计工作量，当导入或者录入超过预计总工作量的时候，提交的保存的时候，进行提示，以便进行修改。
        me.store.each(function(record) {
        	var Workload=record.get('PProjectTask_EstiWorkload');
        	var CName=record.get('PProjectTask_CName');
        	if(Workload){
        		if(Number(Workload)>me.EstiWorkload){
        			msg+=CName+' 的预计工作量不能大于预计总工作量'+'</br>';
					isExect=false;
				}
        	}
        	
        	var EstiStartTime=record.get('PProjectTask_EstiStartTime');
        	var EstiEndTime=record.get('PProjectTask_EstiEndTime');
        	var ItemEstiStartTime = JcallShell.Date.getDate(me.ItemEstiStartTime);
        	var ItemEstiEndTime = JcallShell.Date.getDate(me.ItemEstiEndTime);
        	if(EstiStartTime && EstiEndTime){
        		if(EstiStartTime>EstiEndTime){
					msg+=CName+' 的预计开始时间不能大于预计完成时间'+'</br>';
					isExect=false;
				}
        	}
		
        	if(EstiStartTime && EstiStartTime<ItemEstiStartTime){
				msg+=CName+' 的预计开始时间不能小于项目预计总开始时间'+'</br>';
				isExect=false;
			}
        
        	if(EstiEndTime && ItemEstiEndTime<EstiEndTime){
				msg+=CName+' 的预计完成时间不能大于项目预计总完成时间'+'</br>';
				isExect=false;
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
    /**保存*/
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
		if(me.ProjectID){
			params.entity.PProject = {
				Id:me.ProjectID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		if(rec.get('PProjectTask_PTaskID')){
			params.entity.PTaskID = rec.get('PProjectTask_PTaskID');
		}

	    if(rec.get('PProjectTask_EstiStartTime')){
			params.entity.EstiStartTime = JShell.Date.toServerDate(rec.get('PProjectTask_EstiStartTime'));
	    }
	    if(rec.get('PProjectTask_EstiEndTime')){
			params.entity.EstiEndTime = JShell.Date.toServerDate(rec.get('PProjectTask_EstiEndTime'));
		}
		if(Id){
			params.entity.Id=Id; 
			params.fields = 'Id,CName,EstiWorkload,DispOrder,EstiStartTime,EstiEndTime';
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
						rec.set('PProjectTask_EstiStartTime',rec.get('PProjectTask_EstiStartTime'));
						rec.set('PProjectTask_EstiEndTime',rec.get('PProjectTask_EstiEndTime'));
						rec.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(rec){
						rec.set('PProjectTask_CName',rec.get('PProjectTask_CName'));
						rec.set('PProjectTask_EstiWorkload',rec.get('PProjectTask_EstiWorkload'));
						rec.set('PProjectTask_DispOrder',rec.get('PProjectTask_DispOrder'));
						rec.set('PProjectTask_PTaskID',rec.get('PProjectTask_PTaskID'));
						rec.set('PProjectTask_EstiStartTime',rec.get('PProjectTask_EstiStartTime'));
						rec.set('PProjectTask_EstiEndTime',rec.get('PProjectTask_EstiEndTime'));
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
			url = '/ProjectProgressMonitorManageService.svc/PM_UDTO_CopyProjectTask';
		url += '?fromProjectID=' + fromProjectID+'&toProjectID='+toProjectID+'&isStandard=false';
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
	},
    /**根据类型得到标准项目的标准总工作量天数*/
	getEstiWorkload:function(typeId){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectByHQL?isPlanish=true';
		url += "&fields=PProject_EstiWorkload&where= pproject.IsStandard=1 and pproject.TypeID="+typeId;
		var  EstiWorkload='';
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value && data) {
					if( data.value.list.length>0){
						 EstiWorkload = data.value.list[0].PProject_EstiWorkload;
					}
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
		return EstiWorkload;
	}
});