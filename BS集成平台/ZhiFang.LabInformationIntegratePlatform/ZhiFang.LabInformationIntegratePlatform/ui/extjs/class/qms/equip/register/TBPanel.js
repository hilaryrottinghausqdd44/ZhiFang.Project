/**
 * TB模板
 * @author liangyl
 * @version 2017-02-09
 */
Ext.define('Shell.class.qms.equip.register.TBPanel', {
	extend: 'Shell.ux.panel.AppPanel',
	title: 'TB模板',
	layout: 'fit',
	/**是否创建列表*/
	IsCreateGrid: false,
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchMaintenanceDataTB',
	/**删除服务地址*/
	delUrl: '/QMSReport.svc/QMS_UDTO_DelMaintenanceDataTB',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**模板id*/
	TempletID:null,
	Operatedate:null,
	/**列表类型*/
	typeCode:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddClick','selectclick','itemclick', 'nodata','loadsearch','onDelClick');
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createButtontoolbar();
	
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
			items.push({text:'刷新',tooltip:'刷新',iconCls:'button-refresh',
				handler:function(){
					me.load();
				}
			});
			items.push({text:'新增',tooltip:'新增',iconCls:'button-add',
				handler:function(){
					me.fireEvent('onAddClick',me.Grid);
				}
			});
			items.push({text:'删除',tooltip:'删除',iconCls:'button-del',
				handler:function(){
					me.onDelClick();
				}
			});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	createItems: function() {
		var me = this;
		var items = [];
		return items;
	},
	/**数据加载*/
	load:function(){
		var me=this;
		if(me.TempletID){
			me.onSearch(me.TempletID,me.Operatedate,me.typeCode);
		}
	},
	showMemoText:function(value, meta, record){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1=v.indexOf("</br>");
		if(index1>0)v=v.substring(0,index1);
		if(v.length > 0)v = (v.length > 22 ? v.substring(0, 22) : v);
		if(value.length>22){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},
	/**获取数据*/
	onSearch: function(TempletID,operatedate,typeCode) {
		var me = this;
		me.TempletID=TempletID;
		me.Operatedate=operatedate;
		me.typeCode=typeCode;
		me.removeAll();
		var where = '?templetID=' + TempletID + '&beginDate=' + operatedate + '&endDate=' + operatedate+'&typeCode='+typeCode;
		var url = JShell.System.Path.getRootUrl(me.selectUrl) + where+'&isLoadBeforeData=0';
		var arr = [],str = '',tempStr = '',fields = [];
		var gridcolumns = [{
				xtype: 'rownumberer',text: '序号',width:45,align: 'center'
			},{text: '编号',dataIndex: 'TempletID',width: 160,sortable: false,hidden: true,defaultRenderer: true},
			{text: 'BatchNumber',dataIndex: 'BatchNumber',width: 150,sortable: false,defaultRenderer: true,hidden: true}, 
			{text: '操作日期',dataIndex: '操作日期',width: 150,sortable: false,defaultRenderer: true,hidden: true}];
		var list=[],result={};
		var fieldsArr=[];
		JShell.Server.get(url, function(data) {
			if(!data.value)me.fireEvent('nodata', me.Form);
			if(data.success && data.value) {
				if(data.value.list.length > 0) {
					var keys1 = [];
				    for (var p1 in data.value.list[0]) {
				        if (data.value.list[0].hasOwnProperty(p1)){
				          	   keys1.push(p1);
				        }
				    }
					for(var i =0;i<keys1.length;i++){
						var tempStr = keys1[i]+"";
						var fieldStr=tempStr;
						if(fieldStr.indexOf(".") != -1 ){
					      	for(var j=0;j<fieldStr.length;j++){
					      		 fieldStr = fieldStr.replace(/\./, '' );
					      	}
				        }
//						fieldStr=Ext.decode(fieldStr);
						if(tempStr){
							var filedobj = {
								name: fieldStr,
								type: 'string'
							};
							if(tempStr != "BatchNumber" && tempStr != "TempletID" &&
							tempStr != "操作日期" && tempStr != "TempletTypeCode" ) {
								var column = {
									text: tempStr,
									width: 80,
									style: "text-align:center;",
									align: 'center',
									dataIndex:fieldStr,
									renderer: function(value, meta, record) {
						            	var v=me.showMemoText(value, meta, record);
										return v;
									}
								};
								gridcolumns.push(column);
							}
							fields.push(fieldStr);
						}
					}
					//数据处理
					for(var n = 0; n < data.value.list.length; n++) {
						//替换.去掉返回的特殊字符
						var jsonstr=Ext.encode(data.value.list[n]);
						if(jsonstr.indexOf(".") != -1 ){
					      	for(var j=0;j<jsonstr.length;j++){
					      		 jsonstr = jsonstr.replace(/\./, '' );
					      	}
				        }
                        var obj= Ext.JSON.decode(jsonstr);
						list.push(obj);
					}
					
					result.count=data.value.list.length;
					result.list = list;	
					var store = Ext.create("Ext.data.Store", {
						fields: fields,
						data: result,
						proxy: {
							type: "memory",
							reader: {
								type: "json",
								root: "list"
							}
						}
					});
					me.Grid = Ext.create('Ext.grid.Panel', {
						fields: fields,
						columns: gridcolumns,
						itemId: 'Grid',
						layout: 'fit',
						store: store,
						margin: '0 0 1 0',
						listeners:{
							itemclick: function(v, record) {
								me.fireEvent('itemclick', record, me.Grid,fields);
							},
							select:function(v, record){
								me.fireEvent('selectclick', record, me.Grid,fields);
							},
							afterrender: function(com,  eOpts ){
								com.getSelectionModel().select(0);
							}
						}
					});
					me.items.add(me.Grid);
					me.fireEvent('loadsearch',result.count);
				}else{
					me.fireEvent('nodata', me);
				}
			}
		}, false);
	},
	//获取选中行
	getGridSelect:function(){
		var me =this;
		var records = me.Grid.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		return records[0];
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.Grid.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		JShell.Msg.del(function(but) {
			if (but != "ok") return;
			for (var i in records) {
				me.delOneById(i, records);
			}
		});
	},
	/**删除一条数据*/
	delOneById: function(index, records) {
		var me = this;
		var ItemDate=records[0].get('操作日期');
		var operatedate = JcallShell.Date.toString(ItemDate, true);
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'TempletID=' + me.TempletID+'&itemDate='+operatedate+'&batchNumber='+records[0].get('BatchNumber')+'&typeCode='+me.typeCode;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if (data.success) {
					me.fireEvent('onDelClick', me);
                    me.load();
				} 
			});
		}, 100 * index);
	},
	/**清空tb列表*/
	clearData:function(){
		var me=this;
		me.removeAll();
	}
	
});