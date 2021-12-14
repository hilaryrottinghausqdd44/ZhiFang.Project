/**
 * 字典列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.dict.Grid',{
    extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
    
    title:'字典列表',
    width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true',
	/**新增服务地址*/
    addUrl:'/SingleTableService.svc/ST_UDTO_AddBDict',
	/**修改服务地址*/
	editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBDictByField',
	/**删除数据服务路径*/
	delUrl:'/SingleTableService.svc/ST_UDTO_DelBDict',
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:500,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
//	/**是否启用修改按钮*/
//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**字典类型ID*/
	DictTypeId:null,
	/**字典类型时间戳*/
	DictTypeDataTimeStamp:null,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	
	defaultOrderBy:[{property:'BDict_DispOrder',direction:'ASC'}],

	plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.initEditorListeners();
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:220,emptyText:'名称',isLike:true,
			fields:['pdict.CName']};
			
		me.buttonToolbarItems = ['refresh','-','add','del','save','-',{
			xtype: 'button',
			iconCls: 'file-database',
			text: '内容拷贝',
			tooltip: '拷贝勾选的字典内容到文本域',
			handler: function() {
				me.onInfoCopy();
			}
		},{
			xtype: 'button',
			iconCls: 'file-database',
			text: '快速导入',
			tooltip: '将文本域的内容快速导入到字典中',
			handler: function() {
				me.onInsertData();
			}
		},'-',{
			xtype: 'button',
			iconCls: 'button-config',
			text: '快速排序',
			tooltip: '快速排序',
			handler: function() {
				me.onShowDispOrderPanel();
			}
		}];
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		var columns = [{
			text:'<b style="color:blue;">字典名称</b>',dataIndex:'BDict_CName',width:100,editor:{},
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'<b style="color:blue;">字典编码</b>',dataIndex:'BDict_Shortcode',width:100,editor:{},
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'BDict_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean',
			renderer:function(value,meta,record){
				if(record.get('BDict_DeveCode')){
					return '';
				}else{
					return (new Ext.ux.CheckColumn).renderer(value);
				}
			},
			listeners:{
				beforecheckchange:function(column,rowIndex){
					var record = me.store.getAt(rowIndex);
					if(record.get('BDict_DeveCode')){
						return false;
					}
					return true;
				}
			}
		},{
			text:'<b style="color:blue;">字典描述</b>',dataIndex:'BDict_Memo',width:200,editor:{xtype:'textarea',height:80},
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'次序',dataIndex:'BDict_DispOrder',width:70,defaultRenderer:true,align:'center',type:'int'
		},{
			text:'创建时间',dataIndex:'BDict_DataAddTime',width:130,isDate:true,hasTime:true
		},{
			text:'开发商代码',dataIndex:'BDict_DeveCode',hidden:true,hideable:false
		},{
			text:'主键ID',dataIndex:'BDict_Id',isKey:true,hidden:true//,hideable:false
		}];
		
		return columns;
	},
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			me.updateOneInfo(i,records[i]);
		}
	},
	updateOneInfo:function(index,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var id = record.get(me.PKField);
		var params = Ext.JSON.encode({
			entity:{
				Id:id,
				IsUse:record.get('BDict_IsUse'),
				CName:record.get('BDict_CName'),
				Shortcode:record.get('BDict_Shortcode'),
				Memo:record.get('BDict_Memo'),
				DispOrder:record.get('BDict_DispOrder')
			},
			fields:'Id,IsUse,CName,Shortcode,Memo,DispOrder'
		});
		
		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick',this);
	},
	
	/**字典内容拷贝*/
	onInfoCopy:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		JShell.Win.open('Shell.class.wfm.dict.InfoCopyPanel',{
			InfoRecords:records
		}).show();
	},
	/**快速导入*/
	onInsertData:function(){
		var me = this;
		JShell.Win.open('Shell.class.wfm.dict.InfoCopyPanel',{
			title:'字典信息快速导入',
			hasSave:true,
			listeners:{
				save:function(p,data){
					p.close();
					me.inserDictData(data);
				}
			}
		}).show();
	},
	inserDictData:function(list){
		var me = this,
			len = list.length,
			max = me.getMaxDispOrder();
			
		me.addErrorCount = 0;
		me.addCount = 0;
		me.addLength = len;
			
		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			list[i].BDict_DispOrder = max + i + 1;
			me.saveOneDict(i,list[i]);
		}
	},
	saveOneDict:function(index,values){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.addUrl);
		var entity = {
			CName:values.BDict_CName,
			Shortcode:values.BDict_Shortcode,
			DispOrder:values.BDict_DispOrder,
			IsUse:true,
			Memo:values.BDict_Memo,
			BDictType:{
				Id:me.DictTypeId,
				DataTimeStamp:me.DictTypeDataTimeStamp.split(",")
			}
		};
		
		var params = Ext.JSON.encode({entity:entity});
		
		setTimeout(function() {
			JShell.Server.post(url,params,function(data){
				if (data.success) {
						me.addCount++;
					} else {
						me.addErrorCount++;
					}
					if (me.addCount + me.addErrorCount == me.addLength) {
						me.hideMask(); //隐藏遮罩层
						if (me.addErrorCount == 0){
							me.onSearch();
						}else{
							JShell.Msg.error('字典导入存在失败！');
						}
					}
			});
		}, 100 * index);
	},
	getMaxDispOrder:function(){
		var me = this,
			records = me.store.data.items,
			len = records.length,
			max = 0;
		
		for(var i=0;i<len;i++){
			var num = records[i].get('BDict_DispOrder');
			if(num > max){
				max = num;
			}
		}
		
		return max;
	},
	
	/**显示快速排序面板*/
	onShowDispOrderPanel:function(){
		var me = this,
			records = me.store.data.items,
			len = records.length,
			Data = [];
		
		for(var i=0;i<len;i++){
			Data.push({
				Id:records[i].get('BDict_Id'),
				Name:records[i].get('BDict_CName'),
				DispOrder:records[i].get('BDict_DispOrder')
			});
		}
		
		JShell.Win.open('Shell.ux.grid.DispOrderDrag',{
			resizable:false,
			title:'快速排序',
			Data:Data,
			listeners:{
				save:function(panel,records){
					panel.close();
					me.onSaveDispOrder(panel,records);
				}
			}
		}).show();
	},
	onSaveDispOrder:function(panel,records){
		var me = this,
			items = me.store.data.items;
			
		for(var i in records){
			var rec = me.store.findRecord('BDict_Id',records[i].get('Id'));
			rec.set('BDict_DispOrder',records[i].get('DispOrder'));
		}
		
		me.onSaveClick();
	},
	
	onSelectData:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		
		var hasDeveCode = false;
		for(var i=0;i<len;i++){
			if(records[i].get('BDict_DeveCode')){
				hasDeveCode = true;
				break;
			}
		}
		
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			DelButton = buttonsToolbar.items.items[2];
			
		if(hasDeveCode){
			DelButton.disable();
		}else{
			DelButton.enable();
		}
	},
	initEditorListeners:function(){
		this.on({
			beforeedit : function(editor, e) {
				var DeveCode = e.record.data.BDict_DeveCode;
				if(DeveCode) return false;
				return true; 
			}
		});
	}
});