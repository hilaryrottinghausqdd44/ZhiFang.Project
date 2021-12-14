/**
 * 模板多次记录数据
 * 每天份数记录表，一个模板再一天种需要填写5份，此列表会存在5份记录
 * @author liangyl
 * @version 2018-12-06
 */
Ext.define('Shell.class.qms.equip.emaintenancedata.RecordGrid', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '模板多次记录数据',
	layout: 'fit',
	/**获取数据服务路径*/
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchTempletGroupData',
	delUrl:'/QMSReport.svc/QMS_UDTO_DelETempletData',
	/**模板id*/
	TempletID:null,
	beginDate:null,
	endDate:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddClick','selectclick','itemclick', 'nodata','loadsearch');
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
			me.onSearch();
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
	onSearch: function() {
		var me = this;
		me.removeAll();
		var where = '?templetID=' + me.TempletID + '&beginDate=' + me.beginDate + '&endDate=' + me.endDate;
		var url = JShell.System.Path.getRootUrl(me.selectUrl) + where;
		var arr = [],str = '',tempStr = '',fields = [];
		var gridcolumns = [{
				xtype: 'rownumberer',text: '序号',width:45,align: 'center'
			},{text: '编号',dataIndex: 'TempletID',width: 160,sortable: false,hidden: true,defaultRenderer: true},
			{text: '日期',dataIndex: '日期',width: 80,sortable: false,
			    renderer : function(value, meta, record, rowIndex, colIndex) {
					var v = JShell.Date.toString(value, true) || '';
					if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					return v;
				}
			}];
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
						if(tempStr){
							var filedobj = {
								name: tempStr,
								type: 'string'
							};
							if(tempStr != "TempletBatNo" && tempStr != "TempletID" &&
							tempStr != "日期") {
								var column = {
									text: tempStr,
									width: 80,
									style: "text-align:center;",
									align: 'center',
									dataIndex:tempStr,
									renderer: function(value, meta, record) {
						            	var v=me.showMemoText(value, meta, record);
										return v;
									}
								};
								gridcolumns.push(column);
							}
							fields.push(tempStr);
						}
					}
					gridcolumns.push({text: '批号',dataIndex: 'TempletBatNo',width: 150,sortable: false,defaultRenderer: true});
					//数据处理
					for(var n = 0; n < data.value.list.length; n++) {
						//替换.
						 var jsonstr=Ext.encode(data.value.list[n]);
						 var strobj = jsonstr.replace(".", "");
                         var obj= Ext.JSON.decode(strobj);
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
		var recs = me.Grid.store.data.items,
			len = recs.length;
		if(len==0)return;
		var records = me.Grid.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		return records[0];
	},
	/**清空tb列表*/
	clearData:function(){
		var me=this;
		me.removeAll();
	},
	/**数据查询*/
	searchData:function(templetID,stratDate){
		var me = this;
		/**模板ID*/
	    me.TempletID=templetID;
	    /**开始时间*/
	    me.beginDate=stratDate;
	    /**结束时间*/
	    me.endDate=stratDate;
        me.onSearch();
	},
	/**删除一条数据*/
	onDelClick: function( rec) {
		var me = this;
		var rec = me.getGridSelect();
		var ItemDate=rec.get('日期');
		var operatedate = JcallShell.Date.toString(ItemDate, true);
	    var templetBatNo = rec.get('TempletBatNo');
	    var templetID = rec.get('TempletID');
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'templetID=' + templetID+
		'&templetBatNo='+templetBatNo+'&beginDate='+operatedate+'&endDate='+operatedate;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if (data.success) {
					me.fireEvent('onDelClick', me);
                    me.load();
				} 
			});
		}, 100 * 2);
	}
});