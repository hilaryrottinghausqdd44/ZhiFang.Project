/**
 * 系统版本维护列表
 * @author Jcall
 * @version 2014-08-20
 */
Ext.define('Shell.sysbase.manage.VersionList',{
	extend:'Shell.ux.panel.Grid',
	alias:'widget.versionlist',
	
	title:'软件版本列表',
	tooltip:true,
	multiSelect:true,
	width:600,
	height:250,
	pagingtoolbar:'number',
	defaultLoad:false,
	
	delUrl:'/SingleTableService.svc/ST_UDTO_DelBSoftWareVersionManager',
	updateUrl:'/SingleTableService.svc/ST_UDTO_UpdateBSoftWareVersionManagerByField',
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBSoftWareVersionManagerByHQL?isPlanish=true&fields=' +
		'BSoftWareVersionManager_Id,BSoftWareVersionManager_SoftWareName,BSoftWareVersionManager_Code' +
		'BSoftWareVersionManager_SoftWareComment,BSoftWareVersionManager_SoftWareVersion,BSoftWareVersionManager_Name' +
		'BSoftWareVersionManager_Comment,BSoftWareVersionManager_IsUse,BSoftWareVersionManager_DataAddTime' +
		'BSoftWareVersionManager_PublishTime,BSoftWareVersionManager_PublishName',
	
	
	toolbars:[
		{dock:'top',buttons:[
			'refresh','-',
			//'add','edit','show',
				{btype:'add',className:'Shell.sysbase.manage.VersionForm',classConfig:{
					BSoftWare:this.BSoftWare,
					listeners:{cancel:function(panel){panel.close();}}
				}},
//				{btype:'edit',className:'Shell.test.class.BCountryForm'},
//				{btype:'show',className:'Shell.test.class.BCountryForm'},
			'del','-',
			{btype:'combo',width:70,emptyText:'是否使用',searchField:'bsoftwareversionmanager.IsUse',
				data:[['所有',null],['使用','true'],['不使用','false']]
			},
			'-',
			{btype:'searchtext',width:220,emptyText:'软件名称/软件简称/版本号/版本名称'},
			'->',
			{btype:'help',text:'',iframeUrl:'http://www.baidu.com'}],
			searchFields:[
				'bsoftwareversionmanager.SoftWareName','bsoftwareversionmanager.Code',
				'bsoftwareversionmanager.SoftWareVersion','bsoftwareversionmanager.Name'
			]
		}
	],
	
	initComponent:function(){
		var me = this;
		
		me.columns = [
			{xtype:'rownumberer',text:'序号',width:35,align:'center'},
			{dataIndex:'BSoftWareVersionManager_Id',text:'版本ID',type:'key',hidden:true},
			
			{dataIndex:'BSoftWareVersionManager_SoftWareName',text:'软件名称'},
			{dataIndex:'BSoftWareVersionManager_Code',text:'软件简称'},
			{dataIndex:'BSoftWareVersionManager_SoftWareComment',text:'软件描述'},
			
			{dataIndex:'BSoftWareVersionManager_SoftWareVersion',text:'版本号'},
			{dataIndex:'BSoftWareVersionManager_Name',text:'版本名称'},
			{dataIndex:'BSoftWareVersionManager_Comment',text:'版本描述'},
			
			{dataIndex:'BSoftWareVersionManager_IsUse',text:'使用',type:'isuse',width:40},
			{dataIndex:'BSoftWareVersionManager_DataAddTime',text:'创建时间',type:'datetime',width:140},
			{dataIndex:'BSoftWareVersionManager_PublishTime',text:'发布时间',type:'datetime',width:140},
			{dataIndex:'BSoftWareVersionManager_PublishName',text:'发布者',width:80},
			{xtype:'actioncolumn',text:'启用',width:30,align:'center',sortable:false,hideable:false,
				items:[{
					xtype:'button',iconCls:'button-accept hand',tooltip:'启用',
					itemId:'use',handler:function(grid, rowIndex, colIndex){
						me.onUseVersion(grid, rowIndex, colIndex);
					}
				}]
			},
			{xtype:'actioncolumn',text:'禁用',width:30,align:'center',sortable:false,hideable:false,
				items:[{
					xtype:'button',iconCls:'button-cancel hand',tooltip:'禁用',
					itemId:'unuse',handler:function(grid, rowIndex, colIndex){
						me.onUnuseVersion(grid, rowIndex, colIndex);
					}
				}]
			}
		];
		
		me.callParent(arguments);
	},
	
	/**软件信息赋值*/
	setBSoftWare:function(BSoftWare){
		this.BSoftWare = BSoftWare;
	},
	/**重写新增功能*/
	onAddClick:function(but){
		but.classConfig.BSoftWare = this.BSoftWare;
		this.callParent(arguments);
	},
	/**启用版本*/
	onUseVersion:function(grid, rowIndex, colIndex){
		var me = this;
		var rec = grid.getStore().getAt(rowIndex);
		var isUse = rec.get('BSoftWareVersionManager_IsUse') + '';
		if(isUse === 'true') return;
		
		var id = rec.get('BSoftWareVersionManager_Id');
		me.updateIsUse(id,true,function(){
			rec.set('BSoftWareVersionManager_IsUse','true');
			rec.commit();
		});
	},
	/**禁用版本*/
	onUnuseVersion:function(grid, rowIndex, colIndex){
		var me = this;
		var rec = grid.getStore().getAt(rowIndex);
		var isUse = rec.get('BSoftWareVersionManager_IsUse') + '';
		if(isUse === 'false') return;
		
		var id = rec.get('BSoftWareVersionManager_Id');
		me.updateIsUse(id,false,function(){
			rec.set('BSoftWareVersionManager_IsUse','false');
			rec.commit();
		});
	},
	/**更改使用状态*/
	updateIsUse:function(id,bo,callback){
		var me = this;
		var url = Shell.util.Path.rootPath + me.updateUrl;
		var params = {
			entity:{Id:id,IsUse:bo},
			fields:'Id,IsUse'
		};
		
		postToServer(url,Ext.JSON.encode(params),function(text){
			var result = Ext.JSON.decode(text);
			if(result.success){
				callback();
			}else{
				Shell.util.Msg.showError(result.ErrorInfo);
			}
		},null,false);
	}
});