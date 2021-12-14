/**
 * 文档列表
 * @author Jcall
 * @version 2016-09-18
 */
Ext.define('Shell.class.small.qms.file.basic.ShowGrid',{
    extend: 'Shell.class.small.basic.Grid',
    
    title:'文档列表',
    /**获取数据服务路径*/
	selectUrl: '/QMSService.svc/QMS_UDTO_SearchFFileByBDictTreeId?isPlanish=true&isSearchChildNode=true',
	/**默认排序字段*/
	defaultOrderBy:[
		{property:'FFile_IsTop',direction:'DESC'},
		{property:'FFile_PublisherDateTime',direction:'DESC'},
		{property:'FFile_BDictTree_Id',direction:'ASC'},
		{property:'FFile_Title',direction:'ASC'}
	],
	
	/**默认树节点ID*/
	IDS:'',
	/**文档类型*/
	FTYPE:'2',
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				me.openForm(id);
			}
		});
	},
  	initComponent:function(){
		var me = this;
		
		var dt = new Date();
		dt = Ext.Date.format(dt, 'Y-m-d');
		me.defaultWhere = me.defaultWhere || 
			"id=" + me.IDS + "^" + "ffile.Type in(" + me.FTYPE + ") and " +
			"ffile.Status=5 and ffile.IsUse=1 and (" +
				"(ffile.BeginTime is null and ffile.EndTime is null) or " +
				"(ffile.BeginTime<='" + dt + "') or " +
				"(ffile.EndTime>='" + dt + "')" +
			")";
			
		//创建数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'标题',dataIndex:'FFile_Title',
			flex:1,sortable:false,menuDisabled:true,
			renderer: function(value,meta,record){
				var IsTop = record.get("FFile_IsTop");
				if(IsTop == "true") {
					value = "<b style='color:red;'>【置顶】</b>" + value;
				}
				return value;
			}
			
		},{
			text:'发布人',dataIndex:'FFile_PublisherName',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'发布时间',dataIndex:'FFile_PublisherDateTime',
			width:130,isDate:true,hasTime:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'FFile_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'是否置顶',dataIndex:'FFile_IsTop',hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		url += '&where=' + me.defaultWhere;

		return url;
	},
	/**查看内容*/
	openForm:function(id){
		var me = this;
		JShell.Win.open("Shell.class.qms.file.show.ShowTabPanel",{
			//resizable:false,
			showSuccessInfo:false,
			height:'80%',
			width:'80%',
			
			hasReset:false,
			title:'内容详情',
			formtype:'show',
			
			FTYPE:'2',
			PK:id,
			FFileId:id,
			isAddFFileReadingLog:1,
			isAddFFileOperation:0,
			/**是否显示操作记录页签*/
			hasOperation: false,
			/**是否显示阅读记录页签*/
			hasReadingLog: false
		}).show();
	}
});