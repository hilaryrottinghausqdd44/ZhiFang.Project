/**
 * 附件及说明
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.attachment.PrimaryGrid', {
	extend: 'Shell.ux.grid.Panel',
	
	title: '附件及说明 ',
	width: 280,
	height: 500,
	
	/**附属主体名*/
	PrimaryName:null,
	/**附属主体数据ID*/
	PrimaryID:null,
	/**列表模板地址*/
	GridModelUrl:null,
	/**文件下载服务地址*/
	FileDownloadUrl:'/WorkManageService.svc/WM_UDTO_AttachmentDownLoad',
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchFAttachmentById?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BaseService.svc/ST_UDTO_UpdateFAttachmentByField',
  	/**列表模板内容，新增时使用*/
  	GridModel:null,
  	/**列表实际内容，修改时使用*/
  	GridContent:null,
  	
  	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:100,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: false,
	
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),
  	
  	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onLoadGridModel(function(){
			me.isAdd();
		});
	},
  	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.dockedItems = [Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottomToolbar',
			items: ['add','-','save']
		})];
		
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			xtype:'label',
			style:'margin:2px;color:#04408c;font-weight:bold',
			text:me.title
		},'->','COLLAPSE_LEFT'];
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
  	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'标题',dataIndex:'Title',width:100,editor:{},
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'ID',dataIndex:'Id',isKey:true,hidden:true,hideable:false
		},{
			text:'FileName',dataIndex:'FileName',hidden:true,hideable:false
		},{
			text:'FileSize',dataIndex:'FileSize',hidden:true,hideable:false
		},{
			text:'FileExt',dataIndex:'FileExt',hidden:true,hideable:false
		},{
			text:'HasExtraMsg',dataIndex:'HasExtraMsg',hidden:true,hideable:false
		}];
		
		columns.push({
			xtype: 'actioncolumn',
			sortable:false,
			text: '文件',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,sortable:false,menuDisabled:true,
			renderer:function(v, meta, record){
				var FileName = record.get('FileName');
				if(FileName){
					meta.style = 'background:green';
				}
			},
			items: [{
				iconCls:'button-edit hand',
				tooltip:'修改文件',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onOpenForm(rec);
				}
			}]
		});
		columns.push({
			xtype: 'actioncolumn',
			sortable:false,
			text: '下载',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,sortable:false,menuDisabled:true,
			items: [{
//				iconCls:'file-excel hand',
//				tooltip:'下载文件',
				getClass: function(v, meta, record) {
					var FileName = record.get('FileName');
					
					if(FileName){
						var Size = record.get('FileSize');
						Size = parseInt(Size);
						Size = JShell.Bytes.toSize(Size);
						var Tooltip = FileName + '(' + Size + ')';
						
						meta.tdAttr = 'data-qtip="<b>' + Tooltip + '</b>"';
						return 'file-excel hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('Id');
					me.onFileDownload(id);
				}
			}]
		});
		columns.push({
			xtype: 'actioncolumn',
			sortable:false,
			text: '说明',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,sortable:false,menuDisabled:true,
			items: [{
				iconCls:'button-edit hand',
				tooltip:'打开附加信息',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get('Id');
					me.onOpenExtraMsg(id);
				}
			}]
		});
		columns.push({
			xtype: 'actioncolumn',
			sortable:false,
			text: '删除',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:red;',
			hideable: false,sortable:false,menuDisabled:true,
			items: [{
				iconCls:'button-del hand',
				tooltip:'删除该条记录',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.store.remove(rec);
				}
			}]
		});
		
		return columns;
	},
  	
  	/**
  	 * 加载数据
  	 * @param {Object} data
  	 * @example
  	 * {
  	 * 	Id:123,
  	 * 	FileName:'测试文件',
  	 *  FileSize:123456,
  	 *  FileExt:'txt',
  	 * 	FileUrl:'/20150101/1001.txt',
  	 * 	HasExtraMsg:true,
  	 * 	Title:'仪器清单'
  	 * }
  	 */
  	load:function(id,data){
  		var me = this;
  		me.PrimaryID = id;
  		if(!data){
  			me.isAdd();
  		}else{
  			me.isEdit(data);
  		}
  	},
  	isAdd:function(){
  		var me = this;
  		
  		if(!me.GridModel){
  			JShell.Msg.error('模板【' + me.GridModelUrl + '】不存在，请配置模板');
  			return;
  		}
  		
  		var list = me.GridModel.list,
  			len = list.length,
  			data = [];
  		
  		for(var i=0;i<len;i++){
  			data.push({
  				Id:null,
				FileName:null,
				FileSize:0,
  	 			FileExt:null,
  	 			HasExtraMsg:false,
  				Title:list[i]
  			});
  		}
  		me.isEdit(data);
  	},
  	isEdit:function(data){
  		var me = this;
  		me.store.loadData(data);
  	},
  	/**打开面板*/
  	onOpenForm:function(record){
  		var me = this;
  		
  		JShell.Win.open('Shell.class.sysbase.attachment.PrimaryForm',{
			showSuccessInfo:false,
			resizable:false,
			modal: false, //模态化
			formtype:'add',
			listeners:{
				save:function(p,value){
					record.set('Id',value.Id);
					record.set('FileName',value.FileName);
					record.set('FileSize',value.FileSize);
					record.set('FileExt',value.FileExt);
					record.commit();
					p.close();
					me.onSaveClick();
				}
			},
			/**附属主体名*/
			PrimaryName:me.PrimaryName,
			/**附属主体数据ID*/
			PrimaryID:me.PrimaryID,
			FileInfo:{
		    	Id:record.get('Id'),
				FileName:record.get('FileName'),
				FileSize:record.get('FileSize'),
				FileExt:record.get('FileExt')
		    }
//			FileInfo:{
//		    	Id:0,
//				FileName:'测试文件名',
//				FileSize:10240,
//				FileExt:'txt'
//		    }
		}).showAt(-100,-100);
  	},
  	/**加载模板文件*/
  	onLoadGridModel:function(callback){
  		var me = this;
  		var url = JShell.System.Path.getUrl(me.GridModelUrl);
  		me.showMask();
  		JShell.Server.get(url,function(text){
  			me.hideMask();
  			var data = Ext.decode(text);
  			me.GridModel = data;
  			callback();
  		},null,null,true);
  	},
  	/**下载文件*/
  	onFileDownload:function(id){
  		var me = this;
  		
  		if(!id){
  			JShell.Msg.error('没有文件');
  			return;
  		}
  		
  		var url = JShell.System.Path.getUrl(me.FileDownloadUrl);
  		url += '?AttachmentID=' + id;
  		//JShell.Server.get(url,function(text){});
  		window.open(url);
  	},
  	/**打开附加信息*/
  	onOpenExtraMsg:function(id){
  		var me = this;
  		
  		me.getExtraMsg(id,function(Msg){
  			JShell.Win.open('Shell.class.sysbase.attachment.MsgForm',{
				resizable:false,
				editUrl:me.editUrl,
				MsgField:'ExtraMsg',
				DataId:id,
				Content:Msg,
				listeners:{
					save:function(p,id){
						p.close();
						me.onSaveClick();
					}
				}
			}).show();
  		});
  	},
  	getExtraMsg:function(id,callback){
  		var me = this;
  		var url = JShell.System.Path.getUrl(me.selectUrl);
  		url += '&fields=FAttachment_ExtraMsg&id=' + id;
  		JShell.Server.get(url,function(data){
  			if(data.success){
  				if(data.value){
  					callback(data.value.FAttachment_ExtraMsg);
  				}else{
  					JShell.Msg.error('未找到数据');
  				}
  			}else{
  				JShell.Msg.error('加载错误');
  			}
  		});
  	},
	onCollapseClick:function(){
		this.collapse();
	},
	onAddClick:function(){
		var me = this;
		me.store.add({});
	},
	onSaveClick:function(){
		var me = this;
		var msg = me.getOtherMsgParms();
		me.fireEvent('save',me,msg);
		
		var records = me.store.data.items,
			len = records.length;
			
		for(var i=0;i<len;i++){
			records[i].commit();
		}
	},
	getOtherMsgParms:function(){
		var me = this,
			records = me.store.data.items,
			len = records.length,
			Msg = [];
		
		for(var i=0;i<len;i++){
			Msg.push(records[i].data);
		}
		
		return Msg;
	}
});