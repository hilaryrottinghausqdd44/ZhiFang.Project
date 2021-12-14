/**
 * 基础任务列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.task.basic.Grid',{
    extend: 'Shell.ux.grid.Panel',
    title:'基础任务列表',
    requires:[
    	'Shell.ux.toolbar.Button',
    	'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.CheckTrigger'
    ],
    width:1200,
    height:800,
	
  	/**获取数据服务路径*/
	selectUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl:'/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPTask',
	/**默认排序字段*/
	defaultOrderBy: [{ property: 'PTask_ApplyDataTime', direction: 'DESC' }],
  	
  	/**默认加载数据*/
	defaultLoad:true,
	/**默认选中数据*/
	autoSelect: false,
	/**默认时间类型*/
	defaultDateType:'DataAddTime',
	/**默认员工类型*/
	defaultUserType:'ApplyID',
	
	/**员工类型列表*/
	UserTypeList:[
		['','不过滤'],['ApplyID','申请人'],['OneAuditID','一审人'],['TwoAuditID','二审人'],
		['PublisherID','分配人'],['ExecutorID','执行人'],['CheckerID','检查人']
	],
	/**时间类型列表*/
	DateTypeList:[
		['DataAddTime','创建时间'],['ApplyDataTime','申请时间'],
		['OneAuditDataTime','一审时间'],['TwoAuditDataTime','二审时间'],
		['PublisherDataTime','分配时间'],['CheckerDataTime','验收时间']
	],
	/**默认员工赋值*/
	hasDefaultUser:true,
	/**默认员工ID*/
	defaultUserID:null,
	/**默认员工名称*/
	defaultUserName:null,
	
	/**是否只返回开启的数据*/
	IsUse:true,
	/**是否按部门查询*/
	hasDept:false,
	/**任务分类-字典*/
    TaskClassification:'TaskClassification',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//初始化监听
		me.initFilterListeners();
	},
	initComponent:function(){
		var me = this;
		
		if(me.hasDefaultUser){
			//默认员工ID
			me.defaultUserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			//默认员工名称
			me.defaultUserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		}
		
		//使用中的数据才显示
		if(me.IsUse){
			if(me.defaultWhere){me.defaultWhere += ' and ';}
			me.defaultWhere += 'ptask.IsUse=1';
		}
		
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:[{
				width:140,labelWidth:30,labelAlign:'right',
				xtype:'uxCheckTrigger',itemId:'PClientName',fieldLabel:'客户',
				className:'Shell.class.wfm.client.CheckGrid'
			},{
				xtype:'textfield',itemId:'PClientID',fieldLabel:'客户主键ID',hidden:true
			},'-',{
				width:110,labelWidth:30,labelAlign:'right',
				xtype:'uxSimpleComboBox',itemId:'UserType',fieldLabel:'人员',
				data:me.UserTypeList,
				value:me.defaultUserType
			},{
				width:60,xtype:'uxCheckTrigger',itemId:'UserName',
				className:'Shell.class.sysbase.user.CheckApp',
				value:me.defaultUserName
			},{
				xtype:'textfield',itemId:'UserID',fieldLabel:'申请人主键ID',hidden:true,
				value:me.defaultUserID
			},'-',{
				width:140,labelWidth:60,labelAlign:'right',
				xtype:'uxSimpleComboBox',itemId:'DateType',fieldLabel:'时间范围',
				data:me.DateTypeList,
				value:me.defaultDateType
			},{
				width:95,itemId:'BeginDate',xtype:'datefield',format:'Y-m-d'
			},{
				width:100,labelWidth:5,fieldLabel:'-',labelSeparator:'',
				itemId:'EndDate',xtype:'datefield',format:'Y-m-d'
			}]
		};
		
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
			
		//查询框信息
		me.searchInfo = {
			width:120,emptyText:'任务名称',isLike:true,itemId:'search',
			fields:['ptask.CName']
		};
		
		buttonToolbarItems.unshift('refresh','-');
			
//		buttonToolbarItems.push({
//			width:140,labelWidth:60,labelAlign:'right',
//			xtype:'uxCheckTrigger',itemId:'StatusName',fieldLabel:'任务状态',
//			className:'Shell.class.wfm.dict.CheckGrid',
//			classConfig:{dictTypeCode:JShell.WFM.DictTypeCode.TaskStatus}
//		},{
//			xtype:'textfield',itemId:'StatusID',fieldLabel:'任务状态主键ID',hidden:true
//		});
		
		buttonToolbarItems.push({
			width:160,labelWidth:60,labelAlign:'right',hasStyle:true,//multiSelect:true,
			xtype:'uxSimpleComboBox',itemId:'StatusID',fieldLabel:'任务状态',
			data:me.getStatusData()
		});
		
		buttonToolbarItems.push('-',{
			type: 'search',
			info: me.searchInfo
		});
		if(me.hasDept){
		    buttonToolbarItems.push('-',{
				fieldLabel: '部门',
				emptyText: '部门',
				name: 'DeptName',
				itemId: 'DeptName',
				xtype: 'uxCheckTrigger',
				labelAlign: 'right',
		        className: 'Shell.class.wfm.service.accept.CheckGrid',labelWidth: 35,
				width: 160,
				classConfig: {
					title: '部门选择',
					checkOne:true
				}
			}, {
				fieldLabel: '部门',
				emptyText: '部门',
				name: 'DeptID',
				itemId: 'DeptID',
				hidden: true,
				xtype: 'textfield'
			});
		}
		buttonToolbarItems.push({
			width:180,labelWidth:60,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'PClassName',fieldLabel:'任务分类',emptyText:'任务分类',
			className:'Shell.class.wfm.dict.CheckGrid',
			classConfig:{
				title:'任务分类选择',
				defaultWhere:"pdict.PDictType.DictTypeCode='" + me.TaskClassification + "'"
			}
		},{
			xtype:'textfield',itemId:'PClassID',fieldLabel:'任务分类ID',hidden:true
		});
		return buttonToolbarItems;
	},
	/**获取状态列表*/
	getStatusData:function(){
		var me = this,
			TaskStatus = JShell.WFM.GUID.TaskStatus,
			data = [];
			
		data.push(['','=全部=','font-weight:bold;text-align:center']);
			
		for(var i in TaskStatus){
			var obj = TaskStatus[i];
			var style = ['font-weight:bold;text-align:center'];
			//if(obj.color){style.push('color:' + obj.color);}
			//if(obj.bgcolor){style.push('background-color:' + obj.bgcolor);}
			if(obj.bgcolor){style.push('color:' + obj.bgcolor);}
			data.push([obj.GUID,obj.text,style.join(';')]);
		}
		
		return data;
	},
	
	/**创建数据列 */
	/*显示分配时间和验收时间两列  @author liangyl  @version 2017-08-01*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'任务名称',dataIndex:'PTask_CName',width:180,
			sortable:false,menuDisabled:false,defaultRenderer:true
		},{
			text:'客户',dataIndex:'PTask_PClient_Name',width:120,
			sortable:false,menuDisabled:false,defaultRenderer:true
		},{
			text:'任务状态',dataIndex:'PTask_StatusName',width:70,align:'center',
			sortable:true,menuDisabled:false,renderer:function(value,meta,record){
				var v = record.get('PTask_Status_Id');
				var obj = JShell.WFM.GUID.getInfoByGUID('TaskStatus',v);
				
				var style = [];
				if(obj.color){
					style.push('color:' + obj.color);
				}
				if(obj.bgcolor){
					style.push('background-color:' + obj.bgcolor);
				}
				if(style.length > 0){
					meta.style = style.join(';');
				}

				return value;
			}
		},{
			text:'任务进度',dataIndex:'PTask_PaceName',width:120,resizable:false,
			sortable:false,menuDisabled:false,renderer:function(value,meta){
				value = value || '0%';
				var templet = 
	                '<div class="progress progress-mini" style="float:left;width:67%;height:6px;margin:0;margin-top:3px;">'+
	                    '<div style="width: {PaceName};" class="progress-bar"></div>'+
	                '</div><div style="float:left;width:33%;">&nbsp;{PaceName}</div>';
	                
	            var v = templet.replace(/{PaceName}/g,value);
				return v;
			}
		},{
			text:'任务分类',dataIndex:'PTask_PClassName',width:100,
			sortable:false,menuDisabled:false,defaultRenderer:true
		},
		{
			xtype: 'actioncolumn',text:'交流',align:'center',width:40,
			style:'font-weight:bold;color:white;background:orange;',
			sortable:false,hideable:false,
			items: [{
				iconCls:'button-interact hand',
				tooltip:'<b>交流</b>',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.showInteractionById(id);
				}
			}]
		},{
			text:'申请人',dataIndex:'PTask_ApplyName',width:70,
			sortable:false,menuDisabled:false,defaultRenderer:true
		},{
			text:'申请时间',dataIndex:'PTask_ApplyDataTime',width:130,
			isDate:true,hasTime:true
		},{
			text:'要求完成时间',dataIndex:'PTask_ReqEndTime',width:85,isDate:true
		},{
			text:'实际完成时间',dataIndex:'PTask_EndTime',width:85,isDate:true
		},{
			text:'任务主类',dataIndex:'PTask_MTypeName',width:60,
			sortable:false,menuDisabled:false,defaultRenderer:true
		},{
			text:'任务大类',dataIndex:'PTask_PTypeName',width:60,
			sortable:false,menuDisabled:false,defaultRenderer:true
		},{
			text:'任务类别',dataIndex:'PTask_TypeName',width:100,
			sortable:false,menuDisabled:false,defaultRenderer:true
		},{
			text:'一审人',dataIndex:'PTask_OneAuditName',width:70,
			sortable:false,menuDisabled:false,defaultRenderer:true,hidden:true
		},{
			text:'二审人',dataIndex:'PTask_TwoAuditName',width:70,
			sortable:false,menuDisabled:false,defaultRenderer:true,hidden:true
		},{
			text:'分配人',dataIndex:'PTask_PublisherName',width:70,
			sortable:false,menuDisabled:false,defaultRenderer:true,hidden:true
		},{
			text:'执行人',dataIndex:'PTask_ExecutorName',width:70,
			sortable:false,menuDisabled:false,defaultRenderer:true,hidden:true
		},{
			text:'检查人',dataIndex:'PTask_CheckerName',width:70,
			sortable:false,menuDisabled:false,defaultRenderer:true,hidden:true
		},{
			text:'一审时间',dataIndex:'PTask_OneAuditDataTime',width:130,
			isDate:true,hasTime:true,hidden:true
		},{
			text:'二审时间',dataIndex:'PTask_TwoAuditDataTime',width:130,
			isDate:true,hasTime:true,hidden:true
		},{
			text:'分配时间',dataIndex:'PTask_PublisherDataTime',width:130,
			isDate:true,hasTime:true,hidden:false
		},{
			text:'验收时间',dataIndex:'PTask_CheckerDataTime',width:130,
			isDate:true,hasTime:true,hidden:false
		},{
			text:'创建时间',dataIndex:'PTask_DataAddTime',width:130,
			isDate:true,hasTime:true,hidden:true
		},{
			text:'主键ID',dataIndex:'PTask_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'任务状态主键ID',dataIndex:'PTask_Status_Id',hidden:true,hideable:false
		}];
		
		return columns;
	},
	
	/**根据任务ID查看任务交流*/
	showInteractionById:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.task.interaction.App', {
			//resizable: false,
			TaskId:id//任务ID
		}).show();
	},
	/**显示任务信息*/
	openShowForm:function(id){
		JShell.Win.open('Shell.class.wfm.task.basic.ShowTabPanel', {
			SUB_WIN_NO:'101',//内部窗口编号
			//resizable: false,
			TaskId:id//任务ID
		}).show();
	},
	
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		
		//功能按钮栏1监听
		me.doButtonsToolbarListeners();
		//功能按钮栏2监听
		me.doButtonsToolbarListeners2();
	},
	/**功能按钮栏1监听*/
	doButtonsToolbarListeners:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
			
		if(!buttonsToolbar) return;
			
//		var StatusName = buttonsToolbar.getComponent('StatusName'),
//			StatusID = buttonsToolbar.getComponent('StatusID');
//		
//		//任务状态
//		if(StatusName){
//			StatusName.on({
//				check: function(p, record) {
//					StatusName.setValue(record ? record.get('PDict_CName') : '');
//					StatusID.setValue(record ? record.get('PDict_Id') : '');
//					p.close();
//					me.onSearch();
//				},
//				change:function(){me.onSearch();}
//			});
//		}

		var StatusID = buttonsToolbar.getComponent('StatusID');
		
		//任务状态
		if(StatusID){
			StatusID.on({
				change:function(){me.onSearch();}
			});
		}
		if(me.hasDept){
			var DeptName=buttonsToolbar.getComponent('DeptName'),
		    DeptID=buttonsToolbar.getComponent('DeptID');
			if(DeptName) {
				DeptName.on({
					check:function(p, record) {
						var Id='',Name='';
						if(record){
							var HRDeptId= record ? record.get('HRDept_Id') : '';
							Name=record ? record.get('HRDept_CName') : '';
							if(HRDeptId){
								me.getEmpInfoById(HRDeptId,function(data){
									if(data.value && data.value.count > 0){
										for(var i=0;i<data.value.count;i++){
											if(i>0){
											   Id += ",";	
											}
											Id +=data.value.list[i].HREmployee_Id;
										}
									}
								});
							}
						
						}		
						DeptName.setValue(Name);
						DeptID.setValue(Id);
						p.close();
					},
					change:function(){me.onGridSearch();}
				});
			}  
		}
	    //任务分类   @author liangyl @version 2017-07-13
		var PClassName = buttonsToolbar.getComponent('PClassName'),
			PClassID = buttonsToolbar.getComponent('PClassID');
		if(PClassName){
			PClassName.on({
				check:function(p, record) {
					PClassName.setValue(record ? record.get('PDict_CName') : '');
					PClassID.setValue(record ? record.get('PDict_Id') : '');
					p.close();
				},
				change:function(){me.onGridSearch();}
			});
		}
	},
	/**功能按钮栏2监听*/
	doButtonsToolbarListeners2:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar2');
			
		if(!buttonsToolbar) return;
		
		//客户
		var PClientName = buttonsToolbar.getComponent('PClientName'),
			PClientID = buttonsToolbar.getComponent('PClientID');
		if(PClientName){
			PClientName.on({
				check:function(p, record) {
					PClientName.setValue(record ? record.get('PClient_Name') : '');
					PClientID.setValue(record ? record.get('PClient_Id') : '');
					p.close();
				},
				change:function(){me.onGridSearch();}
			});
		}
		
		//人员类型+人员
		var UserType = buttonsToolbar.getComponent('UserType'),
			UserName = buttonsToolbar.getComponent('UserName'),
			UserID = buttonsToolbar.getComponent('UserID');
		if(UserType){
			UserType.on({change:function(){me.onGridSearch();}});
		}
		if(UserName){
			UserName.on({
				check:function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change:function(){me.onGridSearch();}
			});
		}
		
		//时间类型+时间
		var DateType = buttonsToolbar.getComponent('DateType'),
			BeginDate = buttonsToolbar.getComponent('BeginDate'),
			EndDate = buttonsToolbar.getComponent('EndDate');
		if(DateType){
			DateType.on({change:function(){
				me.onGridSearch();
			}});
		}
		if(BeginDate){
			BeginDate.on({change:function(field,newValue){
				var isValid = field.isValid();
				if(isValid){
					me.onGridSearch();
				}
			}});
		}
		if(EndDate){
			EndDate.on({change:function(field,newValue){
				var isValid = field.isValid();
				if(isValid){
					me.onGridSearch();
				}
			}});
		}
	},
	/**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			PClientID = null,UserType = null,UserID = null,
			DateType = null,BeginDate = null,EndDate = null,
			StatusID = null,search = null,DeptID=null,PClassID=null,
			params = [];
			
		if(buttonsToolbar){
			StatusID = buttonsToolbar.getComponent('StatusID').getValue();
			search = buttonsToolbar.getComponent('search').getValue();
			if(me.hasDept){
				DeptID = buttonsToolbar.getComponent('DeptID').getValue();
			}
			PClassID = buttonsToolbar.getComponent('PClassID').getValue();
		}
		if(buttonsToolbar2){
			PClientID = buttonsToolbar2.getComponent('PClientID').getValue();
			UserType = buttonsToolbar2.getComponent('UserType').getValue();
			UserID = buttonsToolbar2.getComponent('UserID').getValue();
			DateType = buttonsToolbar2.getComponent('DateType').getValue();
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
		}
		
		//任务分类   @author liangyl @version 2017-07-13
		if(PClassID){
			params.push("ptask.PClassID='" + PClassID + "'");
		}
		//客户
		if(PClientID){
			params.push("ptask.PClient.Id='" + PClientID + "'");
		}
		//状态
		if(StatusID){
			params.push("ptask.Status.Id='" + StatusID + "'");
		}
		//员工
		if(UserType && UserID){
			params.push("ptask." + UserType + "='" + UserID + "'");
		}
		//时间
		if(DateType){
			if(BeginDate){
				params.push("ptask." + DateType + ">='" + JShell.Date.toString(BeginDate,true) + "'");
			}
			if(EndDate){
				params.push("ptask." + DateType + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
			}
		}
		if(me.hasDept){
			if(DeptID){
				params.push("ptask.ApplyID in (" + DeptID + ")");
			}
		}
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		
		return me.callParent(arguments);
	},
   /**根据部门得到员工id*/
	getEmpInfoById:function(id,callback){
		var me = this,
			url = JShell.System.Path.getRootUrl('/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true');
		var fields = ['Id'];
		url += '&fields=HREmployee_' + fields.join(',HREmployee_');
		url += '&where=id=' + id;

		JShell.Server.get(url, function(data) {
			callback(data);
		},false);
	}
});