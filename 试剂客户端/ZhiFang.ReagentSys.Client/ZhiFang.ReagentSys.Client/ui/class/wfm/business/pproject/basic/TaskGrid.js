/**
 * 项目任务列表
 * @author liangyl	
 * @version 2017-04-06
 */
Ext.define('Shell.class.wfm.business.pproject.basic.TaskGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '项目任务列表',
    width: 780,
    height: 440,
    /**获取数据服务路径*/
    selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectTaskByHQL?isPlanish=true',
    /**修改服务地址*/
    editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePProjectTaskByField',
    /**删除数据服务路径*/
    delUrl: '/SingleTableService.svc/ST_UDTO_DelPProjectTask',
    
	addUrl:'/SingleTableService.svc/ST_UDTO_AddPProjectTask',
    /**默认加载*/
    defaultLoad: true,
    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: false,
    hasSave:true,
    /**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否有删除列*/
	hasDelBtn:true,
	/**项目ID*/
	ProjectID:null,
	/**项目类型*/
	TypeID:null,
	/**是否标准任务,默认不是*/
    IsStandard:1,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认每页数量*/
	defaultPageSize: 200,
	/**带分页栏*/
	hasPagingtoolbar: false,
    defaultOrderBy: [{ property: 'PProjectTask_DispOrder', direction: 'DESC' }],
   
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
		me.on({
			nodata:function(){
				me.getView().update('');
			}
		});
    },
    
    initComponent: function () {
        var me = this;
        //数据列
        me.columns = me.createGridColumns();
        me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{text: '主键ID',dataIndex: 'PProjectTask_Id',isKey: true,hidden: true,sortable: false, defaultRenderer: true},{
            menuDisabled: false, text: '任务名称', dataIndex: 'PProjectTask_CName',	sortable: false,
			flex:1,minWidth:250,maxWidth:350,
			editor: {xtype: 'textfield',allowBlank: false,
			    listeners:{
			    	change:function(file,value,eOpt){
			    		var records = me.getSelectionModel().getSelection();
						if (records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						records[0].set('PProjectTask_CName',value);
                        me.getView().refresh();
			    	}
			    }
			},
			renderer: function(value, meta, record) {
				var v=value;
				if(!record.get("PProjectTask_PTaskID") && record.get("PProjectTask_Id") ){
					meta.style =  'color:#0033FF;';
				}
				if(record.get("PProjectTask_PTaskID") && record.get("PProjectTask_Id") ){
					v = "<div align='right'>"+ value+'</div>';  
				}
				return v;
			}
        },
        {
			text: '内部排序列',dataIndex: 'PProjectTask_DispOrder',width: 70,sortable: false, 
			menuDisabled: false,editor: {
				xtype: 'numberfield',minValue: 0,allowBlank: false
			}, defaultRenderer: true
		},{
			xtype: 'actioncolumn',text: '附件',align: 'center',width: 40,hideable: false,sortable: false,menuDisabled: true,
			style: 'font-weight:bold;color:white;background:#00F;',
			items: [{
				getClass: function(v, meta, record) {
					var id=record.get('PProjectTask_TaskHelp');
			    	if(id){
			    		return 'button-show hand';
			    	}else{
			    		return '';
			    	}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id=rec.get('PProjectTask_TaskHelp');
					me.showFFileById(id);
				}
			}]
		}, {
			xtype: 'actioncolumn',text: '指',align: 'center',
			width: 30,sortable: false,style: 'font-weight:bold;color:white;background:#00F;',
			menuDisabled: false,items: [{
				getClass: function(v, meta, record) {
					if(record.get('PProjectTask_Id')){
						return 'button-edit hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var Id = rec.get('PProjectTask_Id');
					var TaskHelp=rec.get('PProjectTask_TaskHelp');
					me.onOpenHelpForm(Id,TaskHelp,rec);
				}
			}]
		},{
			xtype: 'actioncolumn',text: '一级任务',	align: 'center',width: 60,
			style:'font-weight:bold;color:white;background:orange;',
			menuDisabled: false,sortable: false,
			items: [{
				getClass: function(v, meta, record) {
				    var Id = record.get('PProjectTask_Id');
				    var PTaskID = record.get('PProjectTask_PTaskID');
					if(Id && PTaskID || record.get('PProjectTask_tab')=='1' ){
						return '';
					}else{
						return 'button-add hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					me.oneAddTask(grid, rowIndex, colIndex);
				}
			}]
		}, {
			xtype: 'actioncolumn',text: '二级任务',align: 'center',
			menuDisabled: false,sortable: false,width: 60,style:'font-weight:bold;color:white;background:orange;',
			items: [{
				getClass: function(v, meta, record) {
				    var Id = record.get('PProjectTask_Id');
				    var PTaskID = record.get('PProjectTask_PTaskID');
					if(Id){
						return 'button-add hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					me.twoAddTask(grid, rowIndex, colIndex);
				}
			}]
		},{
			menuDisabled: false,sortable: false,text: 'PProjectTask_PTaskID',dataIndex: 'PProjectTask_PTaskID',hidden: true, defaultRenderer: true
		},{text: '项目任务指导书',menuDisabled: false,dataIndex: 'PProjectTask_TaskHelp',hidden: true,sortable: false, defaultRenderer: true},
        {text: '标志',menuDisabled: false,dataIndex: 'PProjectTask_tab',hidden: true,sortable: false, defaultRenderer: true}];
		if(me.hasDelBtn){
			columns.push({
				xtype: 'actioncolumn',text: '删除',align: 'center',width: 40,
				menuDisabled: false,style:'font-weight:bold;color:white;background:orange;',
				items: [{
					getClass: function(v, meta, record) {
						return 'button-del hand';
					},
					handler: function(grid, rowIndex, colIndex) {
						me.onDelSave(grid, rowIndex, colIndex);
					}
				}] 
			});
		}
        return columns;
   },
   /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift('refresh','-','save','-',{
			xtype: 'button',iconCls: 'button-del',text: '清空项目任务',tooltip: '清空项目任务',
			handler: function(but) {
				me.onDelSaveClick();
			}
		});
		return buttonToolbarItems;
	},
     /**删除*/
    onDelSave:function(grid, rowIndex, colIndex){
    	var me=this;
		var rec = grid.getStore().getAt(rowIndex);
		var id = rec.get('PProjectTask_Id');
		var PTaskID = rec.get('PProjectTask_PTaskID');
		//数据库不存在的行
		if(!id){
			me.store.remove(rec);
			if(me.getStore().getCount()==0){
			   me.onSearch();
			}
		}else{
			JShell.Msg.del(function(but) {
				if (but != "ok") return;
				me.delErrorCount = 0;
				me.delCount = 0;
				me.delLength =1;
				//子任务，直接删除选择行
				if(PTaskID){
					me.delOneById(2,id);
				}						
				//任务下有子节点的，删除该任务和该任务的所有子节点
				if(!PTaskID){
				    me.getProjectTask(id,function(data){
			        	if(data.value.list && data.value){
			        		for(var i=0;i<data.value.list.length;i++){
			        			me.delLength = data.value.list.length+1;
			        			if(data.value.list[i].PProjectTask_Id){
			        				me.delOneById(2,data.value.list[i].PProjectTask_Id);
			        			}
			        		}
			        	}
			        	me.delOneById(2,id);
			        });
				}
			});
		}
    },
  
   onSaveClick:function(){
		var me = this;
		if(!me.IsValid()) {
			return;
		}
		var records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		if(len == 0) return;
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i=0;i<len;i++){
			var rec = records[i];
			me.onSave(rec);
		}
	},

    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
		//Id 
		if(me.ProjectID) {
			params.push("pprojecttask.PProject.Id=" + me.ProjectID + "");
		}
		if(me.TypeID){
			params.push("pprojecttask.PProject.TypeID='" + me.TypeID + "'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},	
	
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this,list=[],result={};
		var Parr=[],arr=[];
		if(data.list.length==0 ){
			var obj={PProjectTask_Id:'',PProjectTask_CName:'',PProjectTask_EstiWorkload:'',PProjectTask_DispOrder:'1',PProjectTask_PTaskID:'',PProjectTask_TaskHelp:'',PProjectTask_EstiStartTime:'',PProjectTask_EstiEndTime:'',PProjectTask_Workload:'',PProjectTask_tab:'0'};
			list.push(obj);
		}
		for(var i=0;i<data.list.length;i++){
			if(!data.list[i].PProjectTask_PTaskID){
				var obj1={PProjectTask_tab:'0'};
            var obj2 = Ext.Object.merge(data.list[i], obj1);
				Parr.push(obj2);
			}
		}
		Parr.sort(me.compare('PProjectTask_DispOrder'));
		for(var i=0;i<data.list.length;i++){
			if(data.list[i].PProjectTask_PTaskID){
				var obj3={PProjectTask_tab:'1'};
                var obj4 = Ext.Object.merge(data.list[i], obj3);
				arr.push(obj4);
			}
		}
		arr.sort(me.compare('PProjectTask_DispOrder'));
		for(var i=0;i<Parr.length;i++){
			list.push(Parr[i]);
			for(var j=0;j<arr.length;j++){
				if(arr[j].PProjectTask_PTaskID==Parr[i].PProjectTask_Id){
					list.push(arr[j]);
				}
	        }
		}
		result.list = list;	
		return result;
	},
	onSave:function(rec){
		var me = this;
		
	},
	   /**获取选择行的父任务得到选择行的所有子任务*/
	getProjectTask:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchPProjectTaskByHQL?isPlanish=true';
		url += '&fields=PProjectTask_Id&where=pprojecttask.PTaskID='+id;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
    
    /**验证*/
	IsValid: function(str) {
		var me = this,
		    IsSave=true,Count=0;
		if(me.store.getCount()==0){
			JShell.Msg.error('没有数据,不需要保存!');
			IsSave=false;
			return;
		}
		me.store.each(function(rec) {
		    var CName = rec.get('PProjectTask_CName');    
			CName.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
			if(!CName) {
				JShell.Msg.error('项目任务名称不能为空!');
				IsSave=false;
				return;
			}
		});
		return IsSave;
	},
    /**增加一行*/
	createAddRec: function(num,count,PTaskID,tab) {
		var me = this;
		var obj = {
			PProjectTask_CName:'' ,
			PProjectTask_EstiWorkload: '',
			PProjectTask_StandWorkload:'',
			PProjectTask_DispOrder:num,
			PProjectTask_tab:tab
		};
		if(PTaskID){
			obj.PProjectTask_PTaskID=PTaskID;
		}
		me.store.insert(count, obj);
	},
	compare : function(property){
	    return function(a,b){
	        var value1 = a[property];
	        var value2 = b[property];
	        return value1 - value2;
	    }
	},
	 /**添加一级任务*/
    oneAddTask:function(grid, rowIndex, colIndex){
    	var me=this;
		//当前选中行
    	var rec = grid.getStore().getAt(rowIndex);
		var innerIndex1 = me.getStore().indexOf(rec);			
		var Arr=[],DispOrder=0,num=0;
		var CName = rec.get('PProjectTask_CName');    

		//空行
        if(!rec.get('PProjectTask_Id') && me.getStore().getCount()==1 && !CName){
        	innerIndex1=0;
        	DispOrder=0;
        }else{  
        	//有数据时
        	DispOrder=rec.get('PProjectTask_DispOrder');
        	//找二级任务
        	var tasknum=0,taskArr=[];
        	me.store.each(function(record) {
				if(record.get('PProjectTask_PTaskID')==rec.get('PProjectTask_Id') && record.get('PProjectTask_tab')=='1'){
					taskArr.push(record);
				}
		    });
		    //tasknum 二级任务个数
	        if(taskArr.length>0){
	        	tasknum=taskArr.length;
	        	innerIndex1=innerIndex1+tasknum;
	        }
	        innerIndex1= innerIndex1+1;
//          innerIndex1= parseInt(innerIndex1)+1+ parseInt(tasknum);
        }
        num = parseInt(DispOrder)+1;
		me.createAddRec(num,innerIndex1,null,'0');
        var arr=[];
		me.store.each(function(rec) {
			if(rec.get('PProjectTask_tab')=='0'){
				arr.push(rec);
			}
	    });
		me.reorder(arr);
    },
     /**添加二级任务*/
    twoAddTask:function(grid, rowIndex, colIndex){
    	var me=this;
    	var rownum=0, Arr=[],num=0,rec=null;
    	var PTaskID =null,Id=null,DispOrder=0;
    	var arr2=[];
    	//当前选中行
    	rec = grid.getStore().getAt(rowIndex);
    	PTaskID = rec.get('PProjectTask_PTaskID');
    	DispOrder=rec.get('PProjectTask_DispOrder');
    	//如果选择行是一级任务
    	if(!PTaskID && rec.get('PProjectTask_tab')=='0'){
    		Id = rec.get('PProjectTask_Id');
    		//如果选择的任务没有二级任务
    		var record = me.store.findRecord('PProjectTask_PTaskID',Id);
			if (!record){
		        var index=DispOrder+0;
		        num= (num+1)+ parseInt(index);
			}else{

				//找到二级任务最小排序
				me.store.each(function(rec) {
					//二级任务的DispOrder
		            if(rec.get('PProjectTask_PTaskID')== Id && Id){
		              var TaskDispOrder=rec.get('PProjectTask_DispOrder');
		          	  Arr.push(TaskDispOrder);
		            }
				});
				if(Arr && Arr.length>0){
					num = Math.min.apply(null, Arr);
				}
			}
            // 当前选中行+1
		    rownum=rowIndex+1;
    	}
    	//如果选择行是二级任务
    	if(PTaskID && rec.get('PProjectTask_tab')=='1'){
    		//找到选择行的行号
    		rownum=rowIndex+1;
    		//找到选择行的排序号
    		num= parseInt(DispOrder)+1;
    	    //找到选择行一级任务ID
    		var indexOfNum = grid.store.find('PProjectTask_Id', rec.get('PProjectTask_PTaskID'));
			rec = grid.getStore().getAt(indexOfNum);
		    Id = rec.get('PProjectTask_Id');
    	}
		me.createAddRec(num,rownum,Id,'1');
		//排序
	    var arr2=[];
		me.store.each(function(rec) {
			 if(rec.get('PProjectTask_PTaskID')== Id && Id){
				if(rec.get('PProjectTask_tab')=='1' && (rec.get('PProjectTask_DispOrder')>num || rec.get('PProjectTask_DispOrder')==num)) {
					arr2.push(rec);
				}
			}
	    });
		me.reorder(arr2,num);
    },
    /**选择帮助文档*/
	onOpenHelpForm: function(id,TaskHelp,rec) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.knowledgebase.CheckGrid', {
			PK: id,
			SUB_WIN_NO: '102',
			TaskHelp:TaskHelp,
			  /**是否单选*/
	        checkOne:true,
	        /**项目任务作业指导类型Id*/
	        BDictTreeId: "5275917086900702957",
	        title:'实施任务作业指导',
			listeners: {
				accept: function(p, record) {
					if(record){
						var TaskHelp=record.get('FFile_Id');
					    me.updateOneByTaskHelp(id,TaskHelp,p,rec);
					}else{
						//清空
						me.updateOneByTaskHelp(id,'',p,rec);
					}
				},
				load:function(grid){
					var indexOfNum = grid.store.find('FFile_Id',TaskHelp);
	                if(indexOfNum >= 0) {
	                	grid.getSelectionModel().select(indexOfNum);
	                }
				}
			}
		}).show();
	},
	/**设置指导文件*/
	updateOneByTaskHelp:function(id,TaskHelp,p,rec){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = {};
		params.entity = {Id:id};
		params.entity.TaskHelp = TaskHelp;
		params.fields = 'Id,TaskHelp';
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				if(data.success){
					rec.set('PProjectTask_TaskHelp',TaskHelp);
					rec.commit();
					me.hideMask();//隐藏遮罩层
					p.close();
				}else{
					JShell.Msg.alert("保存失败");
				}
			});
		});
	},
	/**根据项目类型获取标准项目任务*/
	getStandardTask:function(TypeID,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchPProjectTaskByHQL?isPlanish=true';
		url += '&fields=PProjectTask_Id,PProjectTask_CName,PProjectTask_DispOrder,PProjectTask_EstiWorkload,PProjectTask_TaskHelp,PProjectTask_EstiStartTime,PProjectTask_EstiEndTime&where=pprojecttask.PProject.TypeID='+TypeID;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
    /**复制拷贝*/
	onAddStandardTask: function(projectID) {
		var me = this,
			url = '/SingleTableService.svc/PM_UDTO_CopyStandardTask';
        if(!projectID){
        	JShell.Msg.error('项目ID不能为空!');
        	return;
        }
		url += '?projectID=' + projectID;
		url = JShell.System.Path.getRootUrl(url);
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.onSearch();
  				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				JShell.Msg.error(msg);
			}
		});
	},
	/**
	 * 清空
	 * @private
	 */
	onDelSaveClick:function(){
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = me.getStore().getCount();

		me.showMask(me.delText); //显示遮罩层
		me.store.each(function(record) {
			var id = record.get('PProjectTask_Id');
			me.delOneById(2,id);
		});
	},
	/**根据id显示指导文档*/
	showFFileById: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.qms.file.basic.FFileDetailedForm', {
			//resizable: false,
			PK: id ,
			width:800,
			title:'实施任务指导书',
			itemId: 'FFileOperationForm',
			FFileId : id
		}).show();
	},
	/**
	 * 重新排序
	 * @private
	 */
	reorder:function(records,pnum){
		var me = this,
			count = me.store.getCount(),
			length = records.length,
			list = [],
			index1,index2,temp,num;
		for(var m=0;m<length;m++){	
			list.push(m);
		}
		for(var i=0;i<length-1;i++){
			for(var j=i+1;j<length;j++){
				index1 = parseInt(records[j].DispOrder);
				index2 = parseInt(records[i].DispOrder);
				if(index1 < index2){
					temp = list[i];
					list[i] = list[j];
					list[j] = temp;
				}
			}
		}
		
		for(var i=0;i<length;i++){
			num = (i + 1) + '';
			//二级任务排序
			if(pnum){
				num = (i + pnum) + '';
			}
			records[list[i]].set('PProjectTask_DispOrder',num);
		}
	},
	/**复制标准项目任务*/
	onCopyTaskClick: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth - 220;
		var height = document.body.clientHeight - 120;
		JShell.Win.open('Shell.class.wfm.business.pproject.basic.CheckGrid', {
			/**项目ID*/
			ProjectID:id,
			SUB_WIN_NO: '106',
			title:'复制标准项目任务',
			width:350,
			height:300,
		    ItemTypeEnum:me.ItemTypeEnum,
		    IsStandard:'1',
		    defaultWhere:'pproject.Id!='+id,
			listeners: {
				accept: function(p, record) {
					if(record){
						var fromProjectID=record.get('PProject_Id');
						var toProjectID=id;
					    me.onSaveCopyProjectTask(fromProjectID,toProjectID,p);
					}
				}
			}
		}).show();
	},
	/**复制标准项目任务*/
	onSaveCopyProjectTask:function(fromProjectID,toProjectID,p){
	   var me=this;
	}
    
});