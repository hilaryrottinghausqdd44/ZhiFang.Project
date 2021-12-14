/**
 * 申请单列表
 * @author Jcall
 * @version 2014-10-23
 */
Ext.define('Shell.OrderEntry.class.OrderEntryList',{
	extend:'Shell.ux.panel.Grid',
	
	requires:['Shell.ux.form.field.DateArea'],
	
	title:'申请单列表',
	width:400,
	height:300,
	multiSelect:true,
	defaultPageSize:50,
	remoteSort:false,
	pagingtoolbar:'basic',
	
	/**删除成功标志字段*/
	stateField:'state',
	
	/**默认数据参数条件*/
	params:{},
	/**科室信息列表*/
	DeptList:[],
	
	/**获取数据服务路径*/
	selectUrl:'/OrderService.svc/GetOrderList',
	/**删除服务*/
	delUrl:'/OrderService.svc/DeleteOrder',
	/**审核服务*/
	checkUrl:'/OrderService.svc/UpdateOrder',
	/**取消审核服务*/
	uncheckUrl:'/OrderService.svc/CancelOrder',
	
	/**开启单元格内容提示*/
	tooltip:true,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
	initComponent:function(){
		var me = this;
		
		me.columns = [
			{xtype:'rownumberer',text:'序号',width:35,align:'center'},
			{dataIndex:'DoInputflag',text:'审核',width:50,renderer:function(value,meta){
				var v = (value + '') == '1' ? "<b style='color:green'>已审</b>" : "<b style='color:red'>未审</b>";
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}},
			{dataIndex:'CName',text:'病人姓名',width:60,sortable:false},
			{dataIndex:'DeptNo',text:'科室',width:60,sortable:false,renderer:function(value,meta){
				var len = me.DeptList.length;
				for(var i=0;i<len;i++){
					if(me.DeptList[i].DeptNo == value){
						if(me.DeptList[i].CName) meta.tdAttr = 'data-qtip="<b>' + me.DeptList[i].CName + '</b>"';
						return me.DeptList[i].CName;
					}
				}
				return "";
			}},
			{dataIndex:'Bed',text:'床位号',width:60,sortable:false},
			{dataIndex:'ItemList',text:'项目信息',width:160,sortable:false},
			{dataIndex:'Flagdatedelete',text:'开单时间',width:130,sortable:false,type:'datetime'},
			{dataIndex:'SerialNo',text:'申请单号',width:140,sortable:false,type:'key'}
		];
		
		me.toolbars = [{
			dock:'top',itemId:'toptoolbar',buttons:['refresh','-',
				'add',{btype:'edit',tooltip:'只能修改未审核的记录!'},'del','-','print','check','uncheck',
				'->',
				{xtype:'uxbutton',itemId:'collapse',text:'',iconCls:'button-left',tooltip:'<b>收缩面板</b>'}
			]
		},{
			dock:'top',itemId:'toptoolbar2',buttons:[
				{xtype:'uxdatearea',itemId:'date',fieldLabel:'开单时间'},
				{xtype:'uxbutton',itemId:'search',iconCls:'button-search',tooltip:'<b>查询</b>'},'-',
				{xtype:'checkbox',itemId:'thisDept',boxLabel:'本科室',checked:true},
				{xtype:'checkbox',itemId:'notCheck',boxLabel:'未审核',checked:false}
			]
		}];
	
		//数据集属性
		me.storeConfig = {
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
							len = list.length;
							
						for(var i=0;i<len;i++){
							list[i].Birthday = Shell.util.Date.toString(list[i].Birthday);
							list[i].CollectDate = Shell.util.Date.toString(list[i].CollectDate,true);
							list[i].CollectTime = Shell.util.Date.toString(list[i].CollectTime);
							list[i].FlagDateDelete = Shell.util.Date.toString(list[i].FlagDateDelete,true);
							list[i].OperDate = Shell.util.Date.toString(list[i].OperDate,true);
							list[i].OperTime = Shell.util.Date.toString(list[i].OperTime);
						}
						
						result = {count:len,list:list};
					}
					
					response.responseText = Ext.JSON.encode(result);
					return response;
				}
			}
		};
		
		me.callParent(arguments);
	},
	
	/**收缩*/
	onCollapseClick:function(but){
		this.collapse();
	},
	/**点击刷新按钮*/
	onRefreshClick:function(){
		this.onSearchClick();
	},
	/**查询按钮处理*/
	onSearchClick:function(but){
		this.load(null,true);
	},
	/**加载数据*/
	load:function(params,isPrivate){
		var me = this,
			collapsed = me.getCollapsed();
			
		me.defaultLoad = true;
		me.params = isPrivate ? me.params : params;
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.store.currentPage = 1;
		me.store.load();
	},
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl,
			toptoolbar2 = me.getComponent('toptoolbar2'),
			thisDept = toptoolbar2.getComponent('thisDept').getValue(),
			notCheck = toptoolbar2.getComponent('notCheck').getValue(),
			date = toptoolbar2.getComponent('date').getValue(),
			flagdatedelete = "flagdatedelete",//开单时间字段
			where = [],
			pars = [];
			
		//本科室
		if(thisDept){
			pars.push("HISDEPTNO=" + me.params.HISDEPTNO);
		}
		if(me.params.PATNO){
			pars.push("PATNO=" + me.params.PATNO);
		}
		if(me.params.SICKTYPENO){
			pars.push("SICKTYPENO=" + me.params.SICKTYPENO);
		}
		
		//未审核
		if(notCheck){
			where.push("DoInputflag=0");
		}
		if(!date){
			//默认加载7天的数据
			where.push("datediff(day," + flagdatedelete + ",getDate())<=7");
		}else{
			var arr = [];
			if(date.start){
				arr.push(flagdatedelete + ">='" + Shell.util.Date.toString(date.start,true) + "'");
			}
			if(date.end){
				arr.push(flagdatedelete + "<'" + Shell.util.Date.toString(Shell.util.Date.getNextDate(date.end),true) + "'");
			}
			if(arr.length == 1) where.push(arr[0]);
			if(arr.length == 2) where.push("(" + arr.join(" and ") + ")");
		}
		pars.push("strWhere=" + where.join(" and "));
		
		if(pars.length > 0){
			url += "?" + pars.join("&");
		}
		
		return url;
	},
	
	/**本科室勾选处理*/
	onThisDeptClick:function(){
		this.onRefreshClick();
	},
	/**未审核勾选处理*/
	onNotCheckClick:function(){
		this.onRefreshClick();
	},
	
	/**科室信息列表赋值*/
	setDeptList:function(list){
		this.DeptList = list || [];
	},
	
	/**重写删除功能*/
	onDelClick:function(but){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			length = records.length;
		
		if(length == 0){
			me.showInfo('请勾选需要删除的记录!');
			return;
		}
		
		for(var i=0;i<length;i++){
			if(records[i].get('DoInputflag') + '' == '1'){
				me.showInfo('已审核的记录不能进行删除操作！请重新勾选需要删除的记录!');
				return;
			}
		}
		
		me.callParent(arguments);
	},
	
	/**审核按钮处理*/
	onCheckClick:function(but){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			length = records.length;
			
		if(length == 0){
			me.showInfo('请勾选需要审核的记录!');
			return;
		}
		
		for(var i=0;i<length;i++){
			if(records[i].get('DoInputflag') + '' == '1'){
				me.showInfo('已审核的记录不能进行审核操作！请重新勾选需要审核的记录!');
				return;
			}
		}
		
		var ids = [];
		me.confirmDel(function(button){
			if(button == "ok"){
				for(var i=0;i<length;i++){
					ids.push(records[i].get(me.PKColumn));
				}
				me.onCheckAll(ids,true);
			}
		},"审核确认","确定要审核吗");
	},
	/**取消审核按钮处理*/
	onUncheckClick:function(but){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			length = records.length;
			
		if(length == 0){
			me.showInfo('请勾选需要取消审核的记录!');
			return;
		}
		
		for(var i=0;i<length;i++){
			if(records[i].get('DoInputflag') + '' == '0'){
				me.showInfo('未审核的记录不能进行取消审核操作！请重新勾选需要取消审核的记录!');
				return;
			}
		}
		
		var ids = [];
		me.confirmDel(function(button){
			if(button == "ok"){
				for(var i=0;i<length;i++){
					ids.push(records[i].get(me.PKColumn));
				}
				me.onCheckAll(ids,false);
			}
		},"取消审核确认","确定要取消审核吗");
	},
	
	/**删除数据*/
	onDel:function(ids){
		var me = this,
			delUrl = Shell.util.Path.rootPath + me.delUrl,
			length = ids.length,
			count = 0;
			
		if(!me.multiSelect){//单选删除
			var url = delUrl + '?serialno=' + ids[0];
			me.getToServer(url,function(text){
				var result = Ext.JSON.decode(text);
				if(result.success){
					me.load(null,true);
				}else{
					Shell.util.Msg.showError('删除失败！');
				}
			},false);
		}else{
			for(var i=0;i<length;i++){
				var url = delUrl + '?serialno=' + ids[i];
				var id = ids[i];
				me.getToServer(url,function(text){
					var result = Ext.JSON.decode(text);
					var record = me.store.findRecord(me.PKColumn,id);
					if(result.success){
						if(record){record.set(me.stateField,true);}
						count++;
						if(count == length){me.load(null,true);}
					}else{
						if(record){record.set(me.stateField,false);}
					}
				},false);
			}
		}
	},
	/**审核数据*/
	onCheckAll:function(ids,isCheck){
		var me = this,
			checkUrl = Shell.util.Path.rootPath + (isCheck ? me.checkUrl : me.uncheckUrl),
			length = ids.length,
			count = 0;
			
		for(var i=0;i<length;i++){
			var url = checkUrl + '?serialno=' + ids[i];
			var id = ids[i];
			me.getToServer(url,function(text){
				var result = Ext.JSON.decode(text);
				var record = me.store.findRecord(me.PKColumn,id);
				if(result.success){
					if(record){record.set(me.stateField,true);}
					count++;
					if(count == length){me.load(null,true);}
				}else{
					if(record){record.set(me.stateField,false);}
				}
			},false);
		}
	}
});