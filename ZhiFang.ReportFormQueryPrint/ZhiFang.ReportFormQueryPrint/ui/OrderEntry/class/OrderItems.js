/**
 * 申请单项目列表
 * @author Jcall
 * @version 2014-10-23
 */
Ext.define('Shell.OrderEntry.class.OrderItems',{
	extend:'Shell.ux.panel.Grid',
	
	title:'申请单项目',
	
	/**获取列表数据服务*/
	selectUrl:'/OrderService.svc/GetOrderItemList',
	/**申请单号*/
	SerialNo:null,
	/**开启单元格内容提示*/
	tooltip:true,
	/**是否默认收缩分组*/
	groupCollapsed:true,
	
	afterRender:function(){
		var me = this;
		
		me.store.on({
			datachanged:function(){
				me.onDataChanged();
			}
		});
			
		me.callParent(arguments);
		
		//视图准备完毕
		me.on({boxready:function(){me.enableControl();}});
	},
	
	initComponent:function(){
		var me = this;
		
		me.toolbars = [{
			dock:'top',itemId:'toptoolbar',buttons:[
				{xtype:'label',itemId:'allprice',style:'color:blue;font-weight:bold;',margin:'0 10 0 10'},'->',
				{xtype:'checkbox',itemId:'showSum',boxLabel:'组合统计可见',checked:false},'-',
				{xtype:'uxbutton',iconCls:'button-arrow-in',tooltip:'<b>全部收缩</b>',itemId:'collapseAll'},
				{xtype:'uxbutton',iconCls:'button-arrow-out',tooltip:'<b>全部展开</b>',itemId:'expandAll'}
			]
		}];
		
		me.columns = [
			{dataIndex:'itemno',text:'编号',width:100,sortable:false},
			{dataIndex:'cname',text:'项目名称',width:100,sortable:false,
				summaryType:'count',
	            summaryRenderer:function(value,summaryData,dataIndex) {
	                return ((value === 0 || value > 1) ? '(' + value + ' 个项目)' : '(1 个项目)');
	            }
			},
			{dataIndex:'ename',text:'英文名称',width:100,sortable:false},
			{dataIndex:'price',text:'价格',width:100,sortable:false,
				summaryType:'sum',
				renderer:function(value,meta){
					var v = Ext.util.Format.currency(value,'￥',2);
					meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
					return v;
				},
            	summaryRenderer:function(v){return Ext.util.Format.currency(v,'￥',2);}
			},
			{dataIndex:'itemdesc',text:'项目说明',width:150,sortable:false,
				renderer:function(value,meta){
					if(value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
					return value;
				}
			},
			{dataIndex:'pno',text:'组合编号',width:100,sortable:false,hidden:true,hideable:false},
			{dataIndex:'pname',text:'组合名称',width:100,sortable:false,hidden:true,hideable:false}
		];
		
		//数据集属性
		me.storeConfig = {
			groupField:'pno',
			proxy:{
				type:'ajax',
				url:'',
				reader:{type:'json',totalProperty:'count',root:'list'},
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText),
						infoField = 'ResultDataValue';
						
					if(!result.success){
						Shell.util.Msg.showError(result.ErrorInfo);
						result = {count:0,list:[]};
					}else{
						var info = result[infoField],
							list = info.list || [],
							len = list.length,
							addList = [];
							
						for(var i=0;i<len;i++){
							var itemList = Ext.JSON.decode(list[i].ItemList) || [],
								length = itemList.length;
								
							for(var j=0;j<length;j++){
								addList.push({
									pno:list[i].ParItemNo,
									pname:list[i].ParItemName,
									itemno:itemList[j].itemno,
									cname:itemList[j].cname,
									ename:itemList[j].ename,
									price:itemList[j].price,
									itemdesc:itemList[j].itemdesc
								});
							}
						}
						
						result = {count:addList.length,list:addList};
					}
					
					response.responseText = Ext.JSON.encode(result);
					return response;
				}
			}
		};
		
		Shell.util.Function.OrderItemsDel = function(id){me.delData(id);};
		me.features = me.groupingFeature = Ext.create('Ext.grid.feature.GroupingSummary',{
            id:'group',
            ftype:'groupingsummary',
            groupHeaderTpl:'{[values.children[0].get("pname")]}&nbsp;&nbsp;&nbsp;&nbsp;' +
            	'<img src="' + Shell.util.Path.uiPath + '/css/images/buttons/del.png" ' +
            		'onclick="Shell.util.Function.OrderItemsDel({[values.children[0].get("pno")]})" ' + 
            	'/>',
			
            hideGroupedHeader:true,
            startCollapsed:me.groupCollapsed,
            enableGroupingMenu:false,
            showSummaryRow:false
        });
		
		me.callParent(arguments);
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			fields = me.getStoreFields();
			
		url += "?serialno=" + me.SerialNo + "&fields=" + fields.join(",");
			
		return url;
	},
	/**加载数据*/
	load:function(SerialNo,isPrivate){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			fields = me.getStoreFields();
			
		if(!isPrivate && !SerialNo){
			Shell.util.Msg.showError("请传递申请单号!");
			return;
		}
		
		if(!isPrivate) me.SerialNo = SerialNo;
		
		me.defaultLoad = true;
		me.store.load();
	},
	/**清空数据*/
	clearData:function(){
		var me = this;
		me.store.removeAll();//清空数据
		me.onDataChanged();
	},
	
	setReadOnly:function(bo){
		
	},
	
	/**新增数据*/
	addData:function(obj){
		var me = this,
			store = me.store,
			list = Ext.JSON.decode(obj.itemList) || [],
			len = list.length,
			addList = [];
			
		for(var i=0;i<len;i++){
			addList.push({
				pno:obj.tid,
				pname:obj.text,
				itemno:list[i].itemno,
				cname:list[i].cname,
				ename:list[i].ename,
				price:list[i].price,
				itemdesc:list[i].itemdesc
			});
		}
	
		if(obj.tid){
			var record = store.findRecord('pno',obj.tid);
			if(!record){
				store.add(addList);
			}
		}
	},
	/**获取数据*/
	getData:function(){
		var me = this,
			items = me.store.data.items,
			len = items.length,
			temp = {},
			list = [];
		
		for(var i=0;i<len;i++){
			var id = items[i].get('pno');
			if(!temp[id]){
				temp[id] = items[i];
			}
		}
		
		for(var i in temp){
			list.push({
				ParItemNo:temp[i].get('pno'),
				ParItemName:temp[i].get('pname')
			});
		}
		
		return list;
	},
	/**删除数据*/
	delData:function(id){
		var me = this,
			items = me.store.data.items,
			len = items.length,
			records = [];
			
		for(var i=0;i<len;i++){
			if(items[i].get(['pno']) == id){
				records.push(items[i]);
			}
		}
		
		me.store.remove(records);
	},
	
	/**全部收缩*/
	onCollapseAllClick:function(){
		this.groupingFeature.collapseAll();
		this.groupCollapsed = true;
	},
	/**全部展开*/
	onExpandAllClick:function(){
		this.groupingFeature.expandAll();
		this.groupCollapsed = false;
	},
	/**分组统计可见按钮处理*/
	onShowSumClick:function(but){
		var me = this,
			value = but.getValue();
			
		me[value ? "showSum" : "hideSum"]();
	},
	/**分组统计可见*/
	showSum:function(){
		var view = this.getView();
        view.getFeature('group').toggleSummaryRow(true);
        view.refresh();
	},
	/**分组统计不可见*/
	hideSum:function(){
		var view = this.getView();
        view.getFeature('group').toggleSummaryRow(false);
        view.refresh();
	},
	
	onDataChanged:function(){
		var me = this,
			allprice = me.getComponent('toptoolbar').getComponent('allprice'),
			items = me.store.data.items,
			len = items.length,
			obj = {},
			info = "  总价 : ",
			count = 0;
			
		for(var i=0;i<len;i++){
			var id = items[i].get('itemno');
			if(obj[id] == null){
				obj[id] = items[i];
			}
		}
		
		for(var i in obj){
			count += obj[i].get('price');
		}
		
		info += Ext.util.Format.currency(count,'￥',2);
			
		allprice.setText(info);
		
		if(me.groupCollapsed){
			me.onCollapseAllClick();
		}else{
			me.onExpandAllClick();
		}
	}
});