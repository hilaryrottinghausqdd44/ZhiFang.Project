/**
 * 系统信息维护列表
 * @author Jcall
 * @version 2014-08-20
 */
Ext.define('Shell.sysbase.manage.SoftwareList',{
	extend:'Shell.ux.panel.Grid',
	alias:'widget.softwarelist',
	
	title:'软件信息列表',
	tooltip:false,
	multiSelect:true,
	width:600,
	height:250,
	pagingtoolbar:'number',
	defaultLoad:false,
	
	delUrl:'/SingleTableService.svc/ST_UDTO_DelBSoftWare',
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBSoftWareByHQL?isPlanish=true&fields=' +
		'BSoftWare_Id,BSoftWare_Name,BSoftWare_SName,BSoftWare_Code,BSoftWare_PinYinZiTou,' +
		'BSoftWare_Comment,BSoftWare_IsUse,BSoftWare_DataAddTime,BSoftWare_DataTimeStamp,BSoftWare_ICO',
	
	columns:[
		//{xtype:'rownumberer',text:'序号',width:35,align:'center'},
		{dataIndex:'BSoftWare_ICO',text:'',width:30,sortable:false,draggable:false,hideable:false,renderer:function(v){
			return "<img width=16 height= 16 src='data:image/png;base64," + v + "' alt=''>";
		}},
		{dataIndex:'BSoftWare_Id',text:'版本ID',type:'key',hidden:true},
		{dataIndex:'BSoftWare_DataTimeStamp',text:'时间戳',hidden:true},
		
		{dataIndex:'BSoftWare_Name',text:'软件名称'},
		{dataIndex:'BSoftWare_SName',text:'软件简称'},
		{dataIndex:'BSoftWare_Code',text:'软件代码'},
		{dataIndex:'BSoftWare_PinYinZiTou',text:'拼音字头'},
		{dataIndex:'BSoftWare_Comment',text:'软件描述'},
		{dataIndex:'BSoftWare_IsUse',text:'使用',type:'isuse',width:40},
		{dataIndex:'BSoftWare_DataAddTime',text:'创建时间',type:'datetime',width:140}
//		{xtype:'actioncolumn',text:'操作',width:60,align:'center',
//			items:[{
//				iconCls:'button-image-add hand',
//                tooltip:'选择软件图标',
//                handler:function(grid,rowIndex,colIndex,item,e,record){
//                	grid.ownerCt.changeSoftwareIcon(record);
//                }
//			}]
//		}
	],
	toolbars:[
		{dock:'top',buttons:[
			'refresh','-','add','edit','show','del','-',
			{btype:'combo',width:75,emptyText:'是否使用',searchField:'bsoftware.IsUse',
				data:[['所有',null],['使用','true'],['不使用','false']]
			},
			'-',
			{btype:'searchtext',width:220,emptyText:'软件名称/软件简称/软件代码/拼音字头'},
			'->',
			{btype:'help',text:'',iframeUrl:'http://www.baidu.com'}],
			searchFields:[
				'bsoftware.Name','bsoftware.SName',
				'bsoftware.Code','bsoftware.PinYinZiTou'
			]
		}
	]
//	/**修改软件图标*/
//	changeSoftwareIcon:function(record){
//		var me = this,
//			id = record.get(me.PKColumn);
//			
//		Shell.util.Win.open('Shell.sysbase.manage.AddSoftwareIconForm',{
//			formtype:'add',
//			PK:id,
//			listeners:{cancel:function(panel){panel.close();}}
//		});
//	}
});