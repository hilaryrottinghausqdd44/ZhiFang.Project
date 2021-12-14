/**
 * 实验室数据库链接配置
 * @author liangyl
 * @version 2017-10-13
 */
Ext.define('Shell.class.rea.client.set.linklab.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '实验室数据库链接配置',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReagentService.svc/RS_UDTO_GetJsonConfig',
	/**保存*/
    saveUrl:'/ReagentService.svc/RS_UDTO_SaveJsonConfig',
    
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: false,
	/**是否启用查询框*/
	hasSearch: false,

	/**默认加载数据*/
	defaultLoad: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	XMLCONFIGTYPE:'101',
	/**默认每页数量*/
	defaultPageSize: 500,
	
	afterRender: function () {
        var me = this;
        me.callParent(arguments);
		me.on({
			nodata:function(){
				me.getView().update('');
			},
			itemdblclick: function(view, record) {
				me.showForm(record);
			}
		});
    },
    
	initComponent: function() {
		var me = this;
		if(me.XMLCONFIGTYPE){
			me.selectUrl+='?jsonConfigType=LabADODBLinkInfo';
		}
		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'OrgNo',text: '机构编号',hidden:true,
			flex:1,maxWidth: 120,defaultRenderer: true
		}, {
			dataIndex: 'OrgName',text: '机构名称',
			flex:1,maxWidth: 200,defaultRenderer: true
		}, {
			dataIndex: 'ServerName',text: '服务器名称',flex:1,maxWidth: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'DatabaseName',text: '数据库名称',flex:1,maxWidth: 200,
			defaultRenderer: true
		},{
			dataIndex: 'UserName',text: '用户名',
			width:150,defaultRenderer: true
		},{
			dataIndex: 'Password',text: '密码',hidden:true,
			hideable: false,sortable: false,menuDisabled: true,
			width:100,defaultRenderer: true
		},{
			dataIndex: 'DriverName',text: '数据库驱动',hidden:true,
			width:100,defaultRenderer: true
		},{
			dataIndex: 'Visible',text: '是否使用',hidden:true,
			align:'center',
			type:'bool',
			isBool:true,
			width:55,defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			width: 55,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-del hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
                    JShell.Msg.del(function(but) {
					if (but != "ok") return;
						me.store.remove(rec);
						me.onSaveClick();
					});
				}
			}]
		}];
		return columns;
	},
	onAddClick:function(){
		var me = this,
			config = {
				resizable: false,
				listeners: {
					save: function(p, values) {
//						var obj={
//							OrgNo:values.OrgNo,
//							OrgName:values.OrgName,
//				//			DriverName:values.DriverName,
//							ServerName:values.ServerName,
//							DatabaseName:values.DatabaseName,
//							UserName:values.UserName,
//							Password:values.Password
//						};
						p.close();
						me.store.add(values);
						me.onSaveClick();
					}
				}
			};
		config.FieldNameStore=me.store;
		config.formtype = 'add';
		JShell.Win.open('Shell.class.rea.client.set.linklab.Form', config).show();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//me.fireEvent('editclick',me,records[0].get(me.PKField));

		me.showForm(records[0]);
	},
	/**显示表单*/
	showForm: function(record) {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					save: function(p, values) {
						p.close();
						//修改列表行
						if(record){
//							record.set('OrgNo',values.OrgNo);
//							record.set('OrgName',values.OrgName);
//                          record.set('DriverName',values.DriverName);
							record.set('ServerName',values.ServerName);
							record.set('DatabaseName',values.DatabaseName);
							record.set('UserName',values.UserName);
							record.set('Password',values.Password);
//							record.set('Visible',values.Visible);
							record.commit();
							me.onSaveClick();
						}
					}
				}
			};
		//判断不允许有重复值
		config.FieldNameStore=me.store;
		config.formtype = 'edit';
		config.recvalues={
			OrgNo:record.get('OrgNo'),
			OrgName:record.get('OrgName'),
			ServerName:record.get('ServerName'),
			DatabaseName:record.get('DatabaseName'),
			UserName:record.get('UserName'),
			Password:record.get('Password')
		};

		JShell.Win.open('Shell.class.rea.client.set.linklab.Form', config).show();
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
        JShell.Msg.del(function(but) {
			if (but != "ok") return;
					
			for (var i in records) {
				var id = records[i].get(me.PKField);
				me.store.remove(records[i]);
				
			}
			me.onSaveClick();
		});
	},
	/**@overwrite 保存按钮点击处理方法*/
	onSaveClick: function() {
		var me=this;
	    var url = JShell.System.Path.getRootUrl(me.saveUrl);
//	    url += '?jsonConfigType=LabADODBLinkInfo';
	    var entity = me.getAddParams();
        var params=Ext.JSON.encode({jsonConfigType:'LabADODBLinkInfo', jsonConfigInfo:entity});
	    JShell.Server.post(url,params,function(data) {
			if (data.success) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,500);
//				me.onSearch();
			} else {
				JShell.Msg.error('保存数据错误');
			}
			
		});
	},
	getAddParams:function(){
		var me=this;
		var records = me.store.data.items,
			len = records.length;
	    var entity = {count:len}, list=[];
		for(var i=0;i<len;i++){
			var obj={
			 	OrgNo:records[i].get('OrgNo'),
			 	OrgName:records[i].get('OrgName'),
//			 	DriverName:records[i].get('DriverName'),
			 	ServerName:records[i].get('ServerName'),
			 	DatabaseName:records[i].get('DatabaseName'),
			 	UserName:records[i].get('UserName'),
			 	Password:records[i].get('Password')
			};
			list.push(obj);
		}
	    entity.list=list;
	    var str=Ext.JSON.encode(entity);
	    return str;
	}
});