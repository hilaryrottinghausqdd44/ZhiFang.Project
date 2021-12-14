/**
 *预授权设置页面
 * @author liangyl
 * @version 2020-05-18
 */
Ext.define('Shell.class.lts.operate.config.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '预授权设置列表',
	width: 800,
	height: 500,
    /**获数据服务路径*/
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeByHQL?isPlanish=true',
		//获取操作授权对应小组
    selectUrl2:'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateASectionByHQL?isPlanish=true',
	
	editUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_UpdateLisOperateAuthorizeByField',
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize:1000,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,
	//默认显示周期
	defaultAuthorizeType:'2',
	
	multiSelect: true,
	selType :'checkboxmodel',
	
	defaultOrderBy:[{property:'LisOperateAuthorize_DataAddTime',direction:'ASC'}],
    
    features: [Ext.create("Ext.grid.feature.Grouping", {
		groupByText: "用本字段分组",
		showGroupsText: "显示分组",
		groupHeaderTpl: "被授权人:{name}",
		startCollapsed: true
	})],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);	
        var AuthorizeType = me.getComponent('buttonsToolbar').getComponent('AuthorizeType').getValue();
        if(AuthorizeType =='2'){ //周期
        	me.columns[me.columns.length-1].setVisible(true);
        }else{
        	me.columns[me.columns.length-1].setVisible(false);
        }
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
	    me.buttonToolbarItems=['refresh','-',{
			hasStyle: true,xtype: 'uxSimpleComboBox',itemId: 'AuthorizeType',
			fieldLabel: '',name:'AuthorizeType',width:80,labelWidth:0,
			listeners:{
				change:function(com,newValue,oldValue,eOpts ){
					if(newValue =='2'){ //周期
			        	me.columns[me.columns.length-1].setVisible(true);
			        }else{
			        	me.columns[me.columns.length-1].setVisible(false);
			        }
					me.onSearch();
				}
			}
		},'-',{text:'新增时段',tooltip:'新增',iconCls:'button-add',
	    handler:function(){
	    	var AuthorizeType = me.getComponent('buttonsToolbar').getComponent('AuthorizeType');
	    	JShell.Win.open('Shell.class.lts.operate.config.Form',{
				width:500,height:250,
				resizable: false,
				maximizable: false, //是否带最大化功能
				formtype:'add',
				AuthorizeType:AuthorizeType.getValue(),//新增默认操作类型
				listeners: {
					save: function(p,id) {
						p.close();
						me.onSearch();
					}
				}
			}).show();
	    }},{text:'修改时段',tooltip:'修改时段',iconCls:'button-edit',
		    handler:function(){
		    	var records = me.getSelectionModel().getSelection();
				if (records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id = records[0].get(me.PKField);//id
				var SectionCName = records[0].get('LisOperateAuthorize_LBSection_CName');//涉及小组name
				var SectionID = records[0].get('LisOperateAuthorize_LBSection_Id');//涉及小组name
				
		    	JShell.Win.open('Shell.class.lts.operate.config.Form',{
					width:500,height:250,
					resizable: false,
					formtype:'edit',
					maximizable: false, //是否带最大化功能
					PK:id,
					SectionID:SectionID,//被选小组
					SectionCName:SectionCName,//被选小组
					listeners: {
						save: function(p,id) {
							p.close();
							me.onSearch();
						}
					}
				}).show();
		    }
	    },{text:'将状态(在用状态)置为否',tooltip:'将状态(在用状态)置为否',iconCls:'button-cancel',
	    handler:function(){
	    	me.onSaveClick();
	    }},
	    {text:'关闭',tooltip:'关闭',iconCls:'button-cancel',
	    handler:function(){
	    	me.close();
	    }}];
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){		  
		var me = this;
		var columns = [{
			text:'被授权人',dataIndex:'LisOperateAuthorize_OperateUser',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'ID',dataIndex:'LisOperateAuthorize_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'起始日期',dataIndex:'LisOperateAuthorize_BeginTime',width:85,
			sortable:false,menuDisabled:true,	
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var bo = me.columns[colIndex].hasTime ? false : true;
				var v = JShell.Date.toString(value, bo) || '';
				if(record.get('LisOperateAuthorize_AuthorizeType')!='1')v="";
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'时间',dataIndex:'LisOperateAuthorize_BeginTime',width:70,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v="";
				if(value){
					var arr  = value.split(' ');
					v = arr[1] || "";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'截止日期',dataIndex:'LisOperateAuthorize_EndTime',width:85,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var bo = me.columns[colIndex].hasTime ? false : true;
				var v = JShell.Date.toString(value, bo) || '';
				if(record.get('LisOperateAuthorize_AuthorizeType')!='1')v="";
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'时间',dataIndex:'LisOperateAuthorize_EndTime',width:70,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v="";
				if(value){
					var arr  = value.split(' ');
					v = arr[1] || "";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'操作类型',dataIndex:'LisOperateAuthorize_OperateType',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'涉及小组',dataIndex:'LisOperateAuthorize_LBSection_CName',width:220,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'涉及小组ID',dataIndex:'LisOperateAuthorize_LBSection_Id',width:220,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'状态',dataIndex:'LisOperateAuthorize_IsUse',
			width:55,	align: 'center',
			isBool: true,
			type: 'bool',
			sortable:false,
			defaultRenderer:true
		},{
			text:'授权类型',dataIndex:'LisOperateAuthorize_AuthorizeType',width:100,
			sortable:false,menuDisabled:true,hidden:true,
			renderer: function(value, meta) {
				var v = value || '';
				if(v) {
					var info = JShell.System.ClassDict.getClassInfoById('AuthorizeType', v);
					if(info) {
						v = info.Name;
					}
				}
				return v;
			}
		},{
			text:'周一',dataIndex:'LisOperateAuthorize_Day1',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周二',dataIndex:'LisOperateAuthorize_Day2',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周三',dataIndex:'LisOperateAuthorize_Day3',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周四',dataIndex:'LisOperateAuthorize_Day4',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周五',dataIndex:'LisOperateAuthorize_Day5',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周六',dataIndex:'LisOperateAuthorize_Day6',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周日',dataIndex:'LisOperateAuthorize_Day0',width:85,hidden:true,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'周期',dataIndex:'LisOperateAuthorize_Cycle',width:200,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
	/**创建数据集*/
	createStore: function() {
		var me = this;
		return Ext.create('Ext.data.Store', {
			fields: me.getStoreFields(),
			pageSize: me.defaultPageSize,
			remoteSort: me.remoteSort,
			sorters: me.defaultOrderBy,
			groupField: "LisOperateAuthorize_OperateUser",
			proxy: {
				type: 'ajax',
				url: '',
				reader: {
					type: 'json',
					totalProperty: 'count',
					root: 'list'
				},
				extractResponseData: function(response) {
					var data = JShell.Server.toJson(response.responseText);
					if (data.success) {
						var info = data.value;
						if (info) {
							var type = Ext.typeOf(info);
							if (type == 'object') {
								info = info;
							} else if (type == 'array') {
								info.list = info;
								info.count = info.list.length;
							} else {
								info = {};
							}

							data.count = info.count || 0;
							data.list = info.list || [];
						} else {
							data.count = 0;
							data.list = [];
						}
						data = me.changeResult(data);
						me.fireEvent('changeResult', me, data);
					} else {
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);

					return response;
				}
			},
			listeners: {
				beforeload: function() {
					return me.onBeforeLoad();
				},
				load: function(store, records, successful) {
					me.onAfterLoad(records, successful);
				}
			}
		});
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.LabStar','AuthorizeType',function(){
			if(!JShell.System.ClassDict.AuthorizeType){
    			JShell.Msg.error('未获取到授权类型,请重新刷新');
    			return;
    		}
			var List=JShell.System.ClassDict.AuthorizeType;
			var AuthorizeType = me.getComponent('buttonsToolbar').getComponent('AuthorizeType');
    		if(AuthorizeType.store.data.items.length==0){
			     AuthorizeType.loadData(me.getListData(List));
			     AuthorizeType.setValue(me.defaultAuthorizeType);
			}
    		me.load(null, true, autoSelect);
		});
	},
	//获取所有 操作授权对应小组的数据
	getOperateASection:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectUrl2;
		url += '&fields=LisOperateASection_LBSection_CName,LisOperateASection_LBSection_Id,LisOperateASection_LisOperateAuthorize_Id';
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = data.value ? data.value.list : [];
				callback(list);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
		/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this;
	
		me.getOperateASection(function(list){
			for(var i=0;i<data.list.length;i++){
				var sectionID=[],sectionName =[];
				for(var j=0;j<list.length;j++){
					if(list[j].LisOperateASection_LisOperateAuthorize_Id == data.list[i].LisOperateAuthorize_Id){
						sectionID.push(list[j].LisOperateASection_LBSection_Id);
						sectionName.push(list[j].LisOperateASection_LBSection_CName);
					}
				}
				data.list[i].LisOperateAuthorize_LBSection_CName = sectionName.join(',') || ""; 
				data.list[i].LisOperateAuthorize_LBSection_Id = sectionID.join(',') || ""; 
				var cycleArr = [];
				if(data.list[i].LisOperateAuthorize_Day1=="true")cycleArr.push("周一");
				if(data.list[i].LisOperateAuthorize_Day2=="true")cycleArr.push("周二");
				if(data.list[i].LisOperateAuthorize_Day3=="true")cycleArr.push("周三");
				if(data.list[i].LisOperateAuthorize_Day4=="true")cycleArr.push("周四");
				if(data.list[i].LisOperateAuthorize_Day5=="true")cycleArr.push("周五");
				if(data.list[i].LisOperateAuthorize_Day6=="true")cycleArr.push("周六");
				if(data.list[i].LisOperateAuthorize_Day0=="true")cycleArr.push("周日");
				data.list[i].LisOperateAuthorize_Cycle= cycleArr.join(',') || "";
			}
		});
		
		return data;
	},
	onSaveClick:function(){
		var me = this,
		    records = me.getSelectionModel().getSelection(),
		    len = records.length;
		if (len == 0) {
			JShell.Msg.error('请选择行后再操作!');
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			me.updateOneByIsUse(i,id);
		}
	},
	updateOneByIsUse:function(index,id){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		var params = {};
		params.entity = {
			Id:id,
			IsUse:0
		};
		params.fields = 'Id,IsUse';
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
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
	/**获取状态列表*/
	getListData: function(list) {
		var me = this,
			data = [];
		for(var i in list) {
			var obj = list[i];
			data.push([obj.Id, obj.Name]);
		}
		return data;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			params = [];
			
		var AuthorizeType = me.getComponent('buttonsToolbar').getComponent('AuthorizeType').getValue();
		//按操作类型过滤
		if(AuthorizeType){
			var info = JShell.System.ClassDict.getClassInfoByName('AuthorizeType','临时');
			if(AuthorizeType==info.Id){//临时(有效时间范围内)
				var sysdate = JShell.System.Date.getDate();
			    var DataAddTime = JShell.Date.toString(sysdate);
                params.push("lisoperateauthorize.BeginTime<'" + DataAddTime + "' and lisoperateauthorize.EndTime>='"+DataAddTime +"'");
			}
			params.push("lisoperateauthorize.AuthorizeType=" + AuthorizeType + "");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	}
});