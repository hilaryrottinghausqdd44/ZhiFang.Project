/**
 * 标准项目维护
 * @author liangyl
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.pproject.standard.Grid', {
	extend: 'Shell.class.wfm.business.pproject.basic.Grid',
	title: '标准项目',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PProject_DispOrder',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	hasSearch:true,
	hasAdd:true,
    hasEdit:true,
	/**项目类型*/
	ProjectType:'ProjectType',
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPProject',
	EditForm:'Shell.class.wfm.business.pproject.standard.Form',
 
	/**是否是标准项目列,0:否，1:是*/
	IsStandard:1,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			savecopy:function(grid,win){
        		me.onSearch();
        		win.close();
        	}
		});
	},
	initComponent: function() {
		var me = this;
		
        //创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
		columns.splice(1,0, {
			text: '项目类型',dataIndex: 'PProject_TypeID',width: 100,
			sortable: true,menuDisabled: true,
            renderer: function(value, meta) {
				var v = value;
				if(me.ItemTypeEnum != null){
					v = me.ItemTypeEnum[value];
				}
				return v;
			}
		},{
			text: '标准总工作量',dataIndex: 'PProject_EstiWorkload',
			type: 'number',xtype: 'numbercolumn',width: 80,
			align: 'center',sortable: false,menuDisabled: true,defaultRenderer: true
		},{
			text: '显示次序',dataIndex: 'PProject_DispOrder',width: 70,sortable: true,
			menuDisabled: true,defaultRenderer: true
		},{
			text: '备注',dataIndex: 'PProject_Memo',width: 150,
			align: 'center',sortable: false,menuDisabled: false,defaultRenderer: true
		},{
			xtype: 'actioncolumn',text: '项目任务',align: 'center',width: 60,
			hideable: false,sortable: false,
			menuDisabled: true,style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PProject_Id');
					var PClientName= rec.get('PProject_CName');
					//项目类型
					var TypeID= rec.get('PProject_TypeID');
					if(!TypeID){
						JShell.Msg.error("选择的行项目类型类型为空!");
						return;
					}
					var EstiWorkload=rec.get('PProject_EstiWorkload');
					if(id && TypeID){
						me.onProjectTaskApp(id,TypeID,PClientName,EstiWorkload);
					}
				}
			}]
		}, {
			text: '计划开始时间',dataIndex: 'PProject_EstiStartTime',width: 150,hidden:true,
			align: 'center',sortable: false,menuDisabled: false,defaultRenderer: true
		},{
			text: '计划结束时间',dataIndex: 'PProject_EstiEndTime',width: 150,
			align: 'center',sortable: false,hidden:true,menuDisabled: false,defaultRenderer: true
		},{
			text: '开始时间',dataIndex: 'PProject_StartTime',width: 150,hidden:true,
			align: 'center',sortable: false,menuDisabled: false,defaultRenderer: true
		},{
			text: '结束时间',dataIndex: 'PProject_EndTime',width: 150,align: 'center',
			sortable: false,hidden:true,menuDisabled: false,defaultRenderer: true
		});

		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		//查询框信息
        me.searchInfo = {
            width: 145, emptyText: '项目名称/用户名称', isLike: true,itemId:'search',
            fields: ['pproject.CName','pproject.PClientName']
        };
		buttonToolbarItems.unshift('refresh','add','edit');
		buttonToolbarItems.push({text:'复制拷贝',tooltip:'复制拷贝',iconCls:'button-edit',
			handler:function(but,e){
				me.onCopyProjectForm();
			}
		});
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**新增*/
	onAddClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.pproject.standard.Form', {
			formtype: 'add',
			SUB_WIN_NO: '1',
			listeners: {
				save: function(form) {
					form.close();
					me.onSearch();
					me.fireEvent('save', form, me);
				}
			}
		}).show();
	},
	/**编辑*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get('PProject_Id');
		me.onOpenEditForm(id);
	},
	/**新增项目任务*/
	onProjectTaskApp: function(id,TypeID,PClientName,EstiWorkload) {
		var me = this;
		if(!PClientName){
			PClientName='项目任务';
		}
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.wfm.business.pproject.standard.TaskGrid', {
			ProjectID: id,
			TypeID:TypeID,
			SUB_WIN_NO: '101',
			title:PClientName,
			IsStandard:me.IsStandard,
			width:940,
			height:height,
			ItemTypeEnum:me.ItemTypeEnum,
			EstiWorkload:EstiWorkload,
			listeners: {
				save:function(grid){
					grid.onSearch();
				},
				beforeclose:function( grid,  eOpts ){
					var edit = grid.getPlugin('NewsGridEditing');  
                    edit.cancelEdit();  
				}
			}
		}).show();
	},

	/**选择项目类型*/
	onCopyProjectForm: function() {
		var me = this;
		var	records = me.getSelectionModel().getSelection();
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		  //项目类型默认查询条件
		  var TypedefaultWhere='',TypeID='';
		  me.getTypeById(function(data){
        	if(data.value.list){
        		TypeID='';
        		for(var i=0;i<data.value.list.length;i++){
        			var id=data.value.list[i].TypeID;
        			if(i>0){
        				TypeID+=","+data.value.list[i].TypeID;
        			}else{
        				TypeID=data.value.list[i].TypeID;
        			}
        		}
        	}
        });
        var TypedefaultWhere="pdict.BDictType.DictTypeCode='" + this.ProjectType+"'";
        if(TypeID){
        	TypedefaultWhere=TypedefaultWhere+" and pdict.Id not in("+TypeID +")";
        }

		var id = records[0].get('PProject_Id');
		JShell.Win.open('Shell.class.wfm.dict.CheckGrid', {
			formtype: 'edit',
			PK: id,
			SUB_WIN_NO: '103',
			title:'选择项目类型',
			defaultWhere:TypedefaultWhere,
			listeners: {
				accept:function(p, record){
					var Id=record ? record.get('BDict_Id') : '';
                    me.onSaveCopyProject(Id,p);
				}

			}
		}).show();
	},
    /**获取还未使用的项目类型*/
	getTypeById:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchPProjectByHQL?isPlanish=false';
		url += '&fields=TypeID&where=pproject.IsStandard=1';
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false,200);
	},
	/**改变查询条件*/
	changeWhere:function(){
		var me = this;
		var params = me.params || [];
		params.push('pproject.IsStandard=1');
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
	}
});