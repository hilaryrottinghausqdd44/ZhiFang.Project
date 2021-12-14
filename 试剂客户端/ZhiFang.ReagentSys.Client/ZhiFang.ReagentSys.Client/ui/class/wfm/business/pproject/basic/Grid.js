/**
 * 项目监控
 * @author liangyl
 * @version 2017-03-23
 */
Ext.define('Shell.class.wfm.business.pproject.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '项目监控',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPProjectByHQL?isPlanish=true',
	addUrl: '/SingleTableService.svc/ST_UDTO_AddPProject',
   	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePProjectByField',

	/**删除数据服务路径*/
	delUrl: '',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PProject_DataAddTime',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: false,

	/**带功能按钮栏*/
	hasButtontoolbar: false,
	/**序号列宽度*/
	rowNumbererWidth: 35,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**项目类型字典编码*/
	ProjectType:'ProjectType',
	EditForm:'Shell.class.wfm.business.pproject.Form',
	/**是否是标准项目列,0:否，1:是*/
	IsStandard:0,
	   /**项目类型数组*/
    ItemTypeList:[],
	ItemTypeEnum:{},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.getProjectType();
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '项目名称',dataIndex: 'PProject_CName',width: 195,sortable: true,menuDisabled: true
		}, {
			xtype: 'actioncolumn',
			text: '项目文档',	align: 'center',width: 60,	hideable: true,	sortable: false,
			menuDisabled: false,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PProject_Id');
					var PClientName= rec.get('PProject_CName');
					if(id){
						me.onProjectDocumentApp(id,PClientName);
					}
				}
			}]
		}, {
			xtype: 'actioncolumn',text: '文档',	align: 'center',width: 40,hideable: false,
			sortable: false,menuDisabled: true,style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onOpenDocumentApp(rec);
				}
			}]
		},  {
			xtype: 'actioncolumn',	text: '编辑',align: 'center',
			width: 40,hideable: false,sortable: false,menuDisabled: true,
			style: 'font-weight:bold;color:white;background:orange;',
			items: [{
				iconCls: 'button-edit hand',
				handler: function(grid, rowIndex, colIndex) {
					//选中行号为num的数据行
					if (rowIndex >= 0) {
						me.getSelectionModel().select(rowIndex);
					}
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('PProject_Id');
					me.onOpenEditForm(id);
				}
			}]
		},  {
			text: '启用',dataIndex: 'PProject_IsUse',width: 40,align: 'center',isBool: true,type: 'bool'
		}, {
			xtype: 'actioncolumn',text: '启/禁',align: 'center',width: 45,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('PProject_IsUse') == "true") {
						meta.tdAttr = 'data-qtip="<b>禁用</b>"';
						return 'button-edit hand';
					} else {
						meta.tdAttr = 'data-qtip="<b>启用</b>"';
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					var isUse = rec.get('PProject_IsUse')+'';
					var msg = isUse == "true" ? "是否禁用该项目?" : "是否启用该项目?";
					var newIsUse = isUse.toString() == "true" ? 0 : 1;
					Ext.MessageBox.show({
						title: '操作确认消息',
						msg: msg,
						width: 300,
						icon: Ext.MessageBox.QUESTION,
						buttons: Ext.MessageBox.OKCANCEL,
						fn: function(btn) {
							if(btn == 'ok') {
								me.UpdateIsUseByStrIds(rec, newIsUse);
							}
						}
					});
				}
			}]
		}, {
			text: '主键ID',dataIndex: 'PProject_Id',isKey: true,hidden: true,hideable: false
		}];
		return columns;
	},

	/**编辑*/
	onOpenEditForm: function(id) {
		var me = this;
		JShell.Win.open(me.EditForm, {
			formtype: 'edit',
			PK: id,
			SUB_WIN_NO: '1',
			listeners: {
				save: function(p) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},

	/**处理文档*/
	onOpenDocumentApp: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var maxWidth = document.body.clientWidth - 20;
		var height = document.body.clientHeight - 35;
		var initItemsValue = {
			CName: record.get("PProject_CName"),
			EntryTime: record.get("PProject_EntryTime"),
			PaceEvalName: record.get("PProject_PaceEvalName"),
			ProjectLeader: record.get("PProject_ProjectLeader"),
			Principal: record.get("PProject_Principal"),
			PhaseManager: record.get("PProject_PhaseManager")
		};
		JShell.Win.open('Shell.class.wfm.business.pproject.TabPanel', {
			ProjectID:id,
			PK:id,
			width: maxWidth,
			height: height,
			activeTab:1,
			initItemsValue: initItemsValue
		}).show();
	},
	
	/**处理进度管理*/
	onOpenScheduleApp: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var EstiStartTime = record.get("PProject_EstiStartTime");
		var EstiEndTime = record.get("PProject_EstiEndTime");
		if(EstiStartTime && EstiEndTime) {
			var maxWidth = document.body.clientWidth - 20;
			var height = document.body.clientHeight - 35;
			var initItemsValue = {
				CName: record.get("PProject_CName"),
				EntryTime: record.get("PProject_EntryTime"),
				PaceEvalName: record.get("PProject_PaceEvalName"),
				ProjectLeader: record.get("PProject_ProjectLeader"),
				Principal: record.get("PProject_Principal"),
				PhaseManager: record.get("PProject_PhaseManager")
			};
			JShell.Win.open('Shell.class.wfm.business.pproject.TabPanel', {
				PK: id,
				ProjectID: id,
				width: maxWidth,
				height: height,
				activeTab:0,
				/**计划开始时间*/
				EstiStartTime: EstiStartTime,
				/**计划结束时间*/
				EstiEndTime: EstiEndTime,
				initItemsValue: initItemsValue,
				listeners: {
					save:function(p){
						me.onSearch();
						p.close();
					}
				}
			}).show();
		} else {
			JcallShell.Msg.alert("计划开始时间或计划结束时间为空!", null, 2000);
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		//改变条件
		me.changeWhere();
		return me.callParent(arguments);
	},
		/**改变查询条件*/
	changeWhere:function(){
		var me = this;
		var params = me.params || [];
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
	},
	/**复制拷贝*/
	onSaveCopyProject: function(typeID,win) {
		var me = this,
			url = '/SingleTableService.svc/PM_UDTO_CopyProject';
		var	records = me.getSelectionModel().getSelection();
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
	    var IsStandard=true;
		if(me.IsStandard==0){
			IsStandard=false;
		}
		var id = records[0].get('PProject_Id');
		if(!typeID){
			typeID=records[0].get('PProject_TypeID');
		}
		url += '?projectID=' + id+'&typeID='+typeID+'&isStandard='+IsStandard;
		url = JShell.System.Path.getRootUrl(url);
		JShell.Server.get(url, function(data) {
			if(data.success) {
				me.fireEvent('savecopy', me,win);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				JShell.Msg.error(msg);
			}
		});
	},
	/**打开项目文档维护*/
	onProjectDocumentApp: function(id,PClientName) {
		var me = this;
		if(!PClientName){
			PClientName='项目文档';
		}
		var maxWidth = document.body.clientWidth - 220;
		var height = document.body.clientHeight - 120;
		JShell.Win.open('Shell.class.wfm.business.pproject.document.EditGrid', {
			/**项目ID*/
			ProjectID:id,
			SUB_WIN_NO: '106',
			title:PClientName,
			width:770,
			height:height,
			listeners: {
				save:function(p){
					me.onSearch();
					p.close();
				},
				beforeclose:function( grid,  eOpts ){
					var edit = grid.getPlugin('FFileGridEditing');  
                    edit.cancelEdit();  
				}
			}
		}).show();
	},
	/*启用或禁用操作**/
	UpdateIsUseByStrIds: function(rec,newIsUse) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var isUse = rec.get('PProject_IsUse')+'';
		var msgInfo = "";
		if(isUse.toString() == "true" || isUse.toString() == "1") {
			msgInfo = "项目禁用";
		} else {
			msgInfo = "项目启用";
		}
		var entity = {
			Id: id,
			IsUse: newIsUse
		};
		var params = {
			entity: entity,
			fields: "Id,IsUse"
		};
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				rec.commit();
				JShell.Msg.alert(msgInfo + "成功", null, 1000);
				me.onSearch();
			} else {
				var msg = data.msg;
				msgInfo = msgInfo + '失败';
				JShell.Msg.error(msgInfo + "<br />" + data.msg);
			}
		});
	},
	/**项目类型信息*/
	getProjectType:function(){
		var me = this;
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true';
		url += "&fields=BDict_CName,BDict_Id&where=pdict.BDictType.DictTypeCode='ProjectType'";
		me.ItemTypeEnum = {};
		JShell.Server.get(url,function(data){
			if(data.success){
				if(data.value) {
					Ext.Array.each(data.value.list, function(obj, index) {
						tempArr = [obj.BDict_Id, obj.BDict_CName];
						me.ItemTypeEnum[obj.BDict_Id] = obj.BDict_CName;
						me.ItemTypeList.push(tempArr);
					});
				}
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});