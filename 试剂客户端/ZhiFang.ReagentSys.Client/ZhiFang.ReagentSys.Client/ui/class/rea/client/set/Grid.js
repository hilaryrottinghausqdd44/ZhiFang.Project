/**
 * 配置信息
 * @author liangyl
 * @version 2017-10-13
 * 货品导入配置
 * 无类型 = 0,
 * 货品导入配置 ,
 * 订货单导入配置 ,
 * 订货单明细导入配置 ,
 * 供货单导入配置 ,
 * 供货单明细导入配置,
 * 新货品导入配置
 * 
 */
Ext.define('Shell.class.rea.client.set.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '配置信息',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReagentService.svc/RS_UDTO_GetInputXmlConfig',
	/**保存*/
    saveUrl:'/ReagentService.svc/RS_UDTO_SaveInputXmlConfig',
    
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: false,

	/**默认加载数据*/
	defaultLoad: true,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**外部参数传入,默认货品导入配置
	 * 无类型 = 0,
	 * 货品导入配置 = 101,
	 * 订货单导入配置 = 102,
	 * 订货单明细导入配置 = 103,
	 * 供货单导入配置 = 104,
	 * 供货单明细导入配置 = 105,
	 * 新货品导入配置 = 106
	 * */
	XMLCONFIGTYPE:'101',
	/**默认每页数量*/
	defaultPageSize: 500,
	afterRender: function () {
        var me = this;
        me.callParent(arguments);
		me.on({
			nodata:function(){
				me.getView().update('');
			}
		});
    },
    
	initComponent: function() {
		var me = this;
		if(me.XMLCONFIGTYPE){
			me.selectUrl+='?xmlConfigType='+me.XMLCONFIGTYPE;
		}
		//数据列
		me.columns = me.createGridColumns();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'FieldName',
			text: '平台字段名',
			flex:1,
			maxWidth: 180,
			editor:{}
		}, {
			dataIndex: 'ExcelFieldName',
			text: 'Excel列名',
			flex:1,
			maxWidth: 180,
			editor:{}
		}, {
			dataIndex: 'IsPrimaryKey',
			text: '主键字段',
			flex:1,
			maxWidth: 120,
			editor:{}
		}, {
			dataIndex: 'IsRequiredField',
			text: '必填字段',
			flex:1,
			maxWidth: 180,
			editor:{}
		},{
			dataIndex: 'DefaultValue',
			text: '默认值',
			flex:1,
			maxWidth: 180,
			editor:{}
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
						me.onSaveClick('del');
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
					save: function(p, entity) {
						p.close();
						me.store.add(entity);
						me.onSaveClick('add');
					},
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
		                edit.cancelEdit();
					}
				}
			};
		config.FieldNameStore=me.store;
		config.formtype = 'add';
		JShell.Win.open('Shell.class.rea.client.set.Form', config).show();
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
			me.onSaveClick('del');
		});
	},
	/**@overwrite 保存按钮点击处理方法*/
	onSaveClick: function(tag) {
		var me=this;
		
	    var url = JShell.System.Path.getRootUrl(me.saveUrl);
	    var entity = me.getAddParams();
        var params=Ext.JSON.encode({xmlConfigType:me.XMLCONFIGTYPE, xmlConfigInfo:entity});
	    JShell.Server.post(url,params,function(data) {
			if (data.success) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,500);
				//如果是新增和删除是保存不需要刷新列表
				if(tag!='add' && tag!='del'){			
					me.onSearch();
				}
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
			var IsPrimaryKey=records[i].get('IsPrimaryKey');
			if(!IsPrimaryKey)IsPrimaryKey='0';
			var IsRequiredField=records[i].get('IsRequiredField');
			if(!IsRequiredField)IsRequiredField='0';
			var obj={
			 	FieldName:records[i].get('FieldName'),
			 	ExcelFieldName:records[i].get('ExcelFieldName'),
			 	IsPrimaryKey:IsPrimaryKey,
			 	IsRequiredField:IsRequiredField,
			 	DefaultValue:records[i].get('DefaultValue')
			};
			list.push(obj);
		}
	    entity.list=list;
	    var str=Ext.JSON.encode(entity);
	    return str;
	}
});