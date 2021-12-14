/**
 * 运营质量记录审核模块 （前台处理分页）
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.ExamineGrid', {
	extend: 'Shell.ux.panel.AppPanel',
	title:"质量记录审核",
	requires:[
	    'Shell.class.qms.equip.templet.ereportdata.PagingMemoryProxy',
	    'Shell.ux.form.field.DateArea',
		'Shell.ux.form.field.CheckTrigger'
	],
	selectUrl: '/QMSReport.svc/QMS_UDTO_SearchWillCheckRecord?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/QMSReport.svc/ST_UDTO_AddEReportData',
	/**编辑服务地址*/
	editUrl: '/QMSReport.svc/ST_UDTO_UpdateEReportDataByField',
	/**删除服务地址*/
	delUrl: '/QMSReport.svc/ST_UDTO_DelETemplet',
    layout:'fit',
    /*静态数据，可以从后台获取*/
    gridData:[], //保存数据
    totalCount:0, //数据总条数
    pageSize:50,  //每页显示的条数
    myStore:null, // 创建的store数据对象
    myGridPanel: null , //GridPanel对象
    getStoreFields:[], //数据列模型
    /**获取列表数据*/
	ItemList: [],
	rowNumbererWidth:50,
	border:false,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	autoSelect: false,
    myTag:'',
	CheckReportUrl : '/QMSReport.svc/QMS_UDTO_CheckReport',
	defaultStartDate:null,
	defaultEndDate:null,
	/**选择行及下一预览项的数据*/
	CurrentPageData:[],
	defaultAddDate:null,
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		me.onGridSearch();
	},
    initComponent:function(){
		var me = this;
		//初始化时间
		me.initDate();
		me.items = me.createGrid();
		me.addEvents('changeData');
		me.callParent(arguments);
	},
	createGridColumns:function(){
		var me = this;
		var columns = [{
			xtype: 'rownumberer',
			text: '序号',
			width: me.rowNumbererWidth,
			align: 'center'
		},{
			text: 'ReportDataID',dataIndex: 'EReportData_ReportDataID',
			width: 100,hidden: true,hideable: false,sortable: false,
			menuDisabled: true,defaultRenderer: true
		},{
			text: 'Id',dataIndex: 'EReportData_Id',width: 200,
			hidden: true,sortable: false,hideable: false,
			menuDisabled: true,defaultRenderer: true
		}, {
			text: '仪器模板Id',dataIndex: 'EReportData_TempletID',
			width: 100,hidden: true,
			sortable: false,menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '质量记录名称',dataIndex: 'EReportData_ReportName',
			minWidth: 270,flex:1,sortable: true,
			defaultRenderer: true
		},{
			text: '类型',dataIndex: 'EReportData_TempletTypeName',
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		}, {
			text: '代码',dataIndex: 'EReportData_TempletCode',
			width: 100,sortable: true,menuDisabled: true,defaultRenderer: true
		},{
			text: '年月',
			dataIndex: 'EReportData_ReportDate',
			width: 85,
			sortable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, store, view) {
				var b = value.split("/");
				var v = b[0] + '年' + b[1] + '月';
				return v;
			}
		},{
			text: '审核状态',dataIndex: 'EReportData_IsCheck',
			width: 65,sortable: true,menuDisabled: true,//align: 'center',
			renderer: function(value, meta) {
				var v = value;
				if(value == '1') {
					v = '已审';
					meta.style = 'color:green';
				} else {
					v = '未审';
					meta.style = 'color:red';
				}
				return v;
			}
		},{
			text: '数据状态',dataIndex: 'EReportData_IsContainData',
			width: 65,sortable: true,menuDisabled: true,
			renderer: function(value, meta) {
				var v = value;
				if(value == '1') {
					v = '已存';
					meta.style = 'color:green';
				} else {
					v = '未存';
					meta.style = 'color:red';
				}
				return v;
			}
		},{
			text: '仪器名称',dataIndex: 'EReportData_EquipName',
			minWidth: 100,flex:1,sortable: true,defaultRenderer: true
		}, {
			text: '小组名称',dataIndex: 'EReportData_SectionName',
			width: 100,sortable: true,defaultRenderer: true
		},  {
			text: '附件标志',
			dataIndex: 'EReportData_IsAttachment',width: 100,hidden: true,sortable: false,menuDisabled: false,
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',text: '预览',
			align: 'center',tooltip: '预览',
			width: 60,style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-show hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.myGridPanel.getSelectionModel().select(rowIndex);
					var rec = grid.getStore().getAt(rowIndex);
					me.openForm(rowIndex);
				}
			}]
		}, {
			xtype: 'actioncolumn',text: '附件',
			align: 'center',tooltip: '附件',width: 60,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
//                	IsAttachment等于0 代表没有附件 大于0 有附件 
					var IsAttachment=record.get('EReportData_IsAttachment');
					if( IsAttachment && parseInt(IsAttachment)>0){
						return 'button-show hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var TempletID = rec.get('EReportData_TempletID');
					var ReportDate = rec.get('EReportData_ReportDate');
					var b = ReportDate.split("/");
					var beginDate = b[0] + '-' + b[1] + '-01';
					var endDate = JcallShell.Date.getMonthLastDate(b[0], b[1], true);
					var endDate2=JShell.Date.getNextDate(endDate,1);
					var endDate3=JShell.Date.toString(endDate2,true);
					var id = rec.get('EReportData_ReportDataID');
					me.showAttachmentById(id, TempletID, beginDate, endDate3);
				}
			}]
		}, {
			text: '审核意见',dataIndex: 'EReportData_CheckView',
			width: 100,hidden:true,sortable: true,
			menuDisabled: true,defaultRenderer: true
		}, {
			xtype: 'actioncolumn',text: '已审',
			align: 'center',width: 40,
			style: 'font-weight:bold;color:white;background:orange;',hideable: false,
			hidden: true,tooltip: '已审',
			items: [{
				getClass: function(v, meta, record) {
					if(record.get('EReportData_IsCheck') == '1') {
						return 'button-edit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {}
			}]
		}, {
			text: '模板路径',
			dataIndex: 'EReportData_TempletPath',
			width: 240,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '仪器类型',
			dataIndex: 'EReportData_EquipTypeName',
			width: 100,
			sortable: true,
			defaultRenderer: true
		},{
			text: '小组代码',
			dataIndex: 'EReportData_SectionUseCode',
			width: 100,
			sortable: true,
			defaultRenderer: true
		},{
			text: '批号',hidden:true,
			dataIndex: 'EReportData_TempletBatNo',width: 100,sortable: false,menuDisabled: false,
			defaultRenderer: true
		}];
		return columns;
	},
    /*创建gridPanel */
    createGrid:function(){
    	var me = this;
    	/*创建数据列 */
        var gridColumn = me.createGridColumns();
        for (var i = 0; i < gridColumn.length; i++) {
        	if(gridColumn[i].dataIndex && gridColumn[i].dataIndex !='undefined'){
        		me.getStoreFields.push(gridColumn[i].dataIndex);
        	}
        }
        me.totalCount = me.gridData.length;
        me.myGridPanel = Ext.create("Ext.grid.Panel", { 
            store: me.createStore(),
            itemId:'Grid',
            multiSelect : true,
	        selType : 'checkboxmodel',
            columns: gridColumn,
            layout:"fit", 
            height:350,
            sortableColumns: false, 
            autoScroll: true,
            dockedItems:me.createDockedItems()
        });
        return me.myGridPanel; 
    },
    /**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createPagingtoolbar());
		items.push(me.createbuttonToolbarItems());
		return items;
	},
    changeRowClass:function(){
    	var me =this;
    	
    },
    createStore:function(){
    	var me = this;
    	me.myStore = Ext.create("Ext.data.Store", {
            fields: me.getStoreFields,
            pageSize: me.pageSize, // 指定每页显示2条记录
            autoLoad: true,
            data: me.gridData,
            proxy: {
                type: 'pagingmemory',
                reader: {
                    type: 'json',
                    totalProperty:'total'
                }
            }
        });
        return me.myStore;
    },
    
    /**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;
		var config = {
			store: me.myStore,
            dock: 'bottom',
            cls: "smallPagingToolBar",
            inputItemWidth: 50, 
            displayInfo: true,
            dorefreshData: function () { 
                me.refreshData();
                me.fireEvent('changeData', me.myGridPanel);
            },
            listeners: {
                change:function(){
                	me.fireEvent('changeData', me.myGridPanel);
                }
            }
		};
		return Ext.create('Ext.toolbar.Paging', config);
	},
	/**获取带查询条件
	 * 全部 比如现在是10月 参数开始和结束日期为2017-09-01和2017-10-01 
	 * 本月 比如现在是10月 参数开始和结束日期为2017-10-01和2017-10-01 
	 * 上月 比如现在是10月 参数开始和结束日期为2017-09-01和2017-09-01 
	 * */
	getSearchWhereParams: function() {
		var me = this,
			whereParams = "",
			params = [];
	    var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
	    var datearea = buttonsToolbar.getComponent('datearea').getValue();
        //本月
        if(me.myTag=='2'){
            var IsCheck={IsCheck:'0'};
            params.push("&otherPara="+Ext.encode(IsCheck)); 
        }
         //上月
        if(me.myTag=='3'){
            var IsCheck={IsCheck:'0'};
            params.push("&otherPara="+Ext.encode(IsCheck)); 
        }
        if(!datearea.start ){
        	JShell.Msg.error('本月开始时间不能为空');
        	return;
        }
        if(!datearea.end){
        	JShell.Msg.error('本月结束时间不能为空');
        	return;
        }
        params.push("&endDate=" + JShell.Date.toString(datearea.end,true));
		params.push("&beginDate=" + JShell.Date.toString(datearea.start,true));
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
	/**创建功能按钮栏*/
	createbuttonToolbarItems: function() {
		var me = this,
			items =  [];
		items.push({text:'刷新',tooltip:'刷新',iconCls:'button-refresh',
		    handler:function(){
		    	me.onRefreshClick();
		    }
		},{
			xtype: 'uxdatearea',itemId:'datearea',name:'datearea',labelWidth: 60,labelAlign: 'right',
			fieldLabel: '日期范围',value:me.defaultAddDate,
			listeners: {
				enter: function() {
					me.onGridSearch();
				},
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue) me.onGridSearch();
				}
			}
		},'-',{
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
		    name: 'fastKey', margin: '0 0 0 5',
		    emptyText: '质量记录名称',
			listeners:{
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER){
						me.onSearch();
						me.fireEvent('changeData', me.myGridPanel);
					}
				}
            }
        },
		'-',{text:'本月未审核', xtype: 'button', enableToggle:false, itemId:'monthBtn'},
        '-',{text:'上月未审核', xtype: 'button', itemId:'premonthBtn' },
	    '-', {text:'批量审核', xtype: 'button',tooltip:'批量审核',iconCls:'button-save', itemId:'checksave',
	        handler:function(){
	        	me.onSaveCheckReport();
	        }},'->',{
	        xtype: 'label',forId: 'myFieldId',
	        itemId: 'myFieldId',style: "font-weight:bold;color:blue;",
	        text: '查询全部数据,包括已审核和未审核', margin: '0 0 0 10'
	    });

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**刷新数据*/
    refreshData:function(){
        //清空筛选输入框的数据,
        var fastKey = this.myGridPanel.query("[name='fastKey']")[0];
        fastKey.setValue("");
        /*重新加载数据*/
        this.onSearch(); 
    },
    /*
     * 查询数据
     * 本地过滤
     */
    onSearch: function () {
        var me = this;
         me.showMask();
        var fastKey = me.myGridPanel.query("[name='fastKey']")[0];
        var searchValue = fastKey.getValue().toString().toLowerCase(),
            newData = [];  //newData保存筛选出来的数据
            me.ItemList=[];
        if (searchValue == "") { 
            newData = me.gridData;
        } else {
            for (var i = 0, len = me.gridData.length; i < len; i++) {
                for (var j = 1, jlen = me.getStoreFields.length; j < jlen; j++) {
                	//按质量记录名称查询，全过滤(me.gridData[i][me.getStoreFields[j]] && me.gridData[i][me.getStoreFields[j]].toString().toLowerCase().indexOf(searchValue) >= 0)
                	if (me.gridData[i].EReportData_ReportName && me.gridData[i].EReportData_ReportName.toString().toLowerCase().indexOf(searchValue) >= 0) {
                        newData.push(me.gridData[i]);
                        break;
                    }
                }
            }
        } 
         me.ItemList= newData;
        /*重新加载数据*/
        me.myGridPanel.store.loadData(newData);
        me.myGridPanel.store.getProxy().data = newData; //更新在缓存的数据
        me.myGridPanel.store.loadPage(1); //重新刷新
        me.hideMask();
    },
	onGridSearch:function(){
	    var me = this;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
		var monthBtn = buttonsToolbar.getComponent('monthBtn');
        var premonthBtn = buttonsToolbar.getComponent('premonthBtn');
	    if(monthBtn.pressed)me.myTag='2';
	    if(premonthBtn.pressed)me.myTag='3';
        if(!monthBtn.pressed && !premonthBtn.pressed)me.myTag='';
		me.getDataAll(function(data){
			if(data.value){
				me.gridData=data.value.list;
				me.ItemList=me.gridData;
			}
		});
		me.onSearch();
	},
	getFields:function(){
		var me = this;
		var fields = 'EReportData_ReportDataID,EReportData_Id,'+
			'EReportData_TempletID,EReportData_ReportName,'+
			'EReportData_TempletTypeName,EReportData_TempletCode,'+
			'EReportData_ReportDate,EReportData_IsCheck,'+
			'EReportData_IsContainData,EReportData_EquipName,'+
			'EReportData_SectionName,EReportData_IsAttachment,'+
			'EReportData_CheckView,EReportData_TempletPath,'+
			'EReportData_EquipTypeName,EReportData_SectionUseCode,'+
			'EReportData_TempletBatNo';
		return fields;
	},
	//获取数据
	getDataAll:function(callback){
		var me=this;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
		var datearea = buttonsToolbar.getComponent('datearea').getValue();

		strval=	datearea.start;	 
		endval=	datearea.end;
        var year = strval.getFullYear();
		var month = strval.getMonth() + 1;
		//开始时间  X年X月01日
		var startdate = JShell.Date.getMonthFirstDate(year, month);
		startdate = JcallShell.Date.toString(startdate, true);
		var year2 = endval.getFullYear();
		var month2 = endval.getMonth() + 1;
		//结束时间  X年X月01日
		var endDate = JShell.Date.getMonthFirstDate(year2, month2);
		endDate = JcallShell.Date.toString(endDate, true);
		//最大结束时间
		var Start =JcallShell.Date.getNextDate(startdate,182.5);
	    if(endval>Start){
	    	JShell.Msg.alert('只能查半年内的数据!', null, 2000);
	    	return;
	    }
	    if(endDate<startdate){
	    	JShell.Msg.alert('结束时间不能小于开始时间!', null, 2000);
	    	return;
	    }
	    
		var url = JShell.System.Path.ROOT + me.selectUrl;
		var whereParams = me.getSearchWhereParams();
		url += (url.indexOf('?') == -1 ? '?' : '&') ;
		if(whereParams) {
			url += whereParams;
		}
		url += "&fields="+me.getFields();
	
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	
	/**获取数据*/
	getItemList:function(){
		var me =this;
		return me.ItemList;
	},
	/**获取选中行*/
	getSelection:function(){
		var me =this;
		var Selection=me.myGridPanel.getSelectionModel().getSelection();
		return Selection;
	},
	/**显示遮罩*/
	showMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(me.loadingText);
		} //显示遮罩层
		me.enableControl(false); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(true); //启用所有的操作功能
	},
		/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.myGridPanel.store.removeAll(); //清空数据
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
	    var Sysdate = JcallShell.System.Date.getDate();
	    if(Sysdate == null) Sysdate = new Date();
	    var TDate = JcallShell.Date.toString(Sysdate, true);
		var year = Sysdate.getFullYear();
		var month = Sysdate.getMonth() + 1;
		//本月1号
		var EndDate = JShell.Date.getMonthFirstDate(year, month);
        //获取上月1号
        var BeginDate = me.getPreMonthDate(TDate);
        me.defaultAddDate = {
			start:JShell.Date.toString(BeginDate,true),
			end: JShell.Date.toString(EndDate,true)
		};
	},
	initFilterListeners:function(){
		var me=this;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
		if(!buttonsToolbar)return;
        var monthBtn = buttonsToolbar.getComponent('monthBtn');
        var premonthBtn = buttonsToolbar.getComponent('premonthBtn');
	    var myFieldId=buttonsToolbar.getComponent('myFieldId');
	    var datearea = buttonsToolbar.getComponent('datearea');
	    var Sysdate = JcallShell.System.Date.getDate();
	    if(Sysdate == null) Sysdate = new Date();
	    var TDate = JcallShell.Date.toString(Sysdate, true);
		var year = Sysdate.getFullYear();
		var month = Sysdate.getMonth() + 1;
		//本月1号
		var EndDate = JShell.Date.getMonthFirstDate(year, month);
        //获取上月1号
        var BeginDate = me.getPreMonthDate(TDate);
        var tab=true;
		monthBtn.on({
			click:function(){
				tab=false;
				//获取上月1号
	            datearea.setValue({start:EndDate,end:EndDate});
				me.myTag='2';
				monthBtn.toggle(true);
				premonthBtn.toggle(false);
				myFieldId.setText('查询本月未审数据');
				me.onGridSearch();
				tab=true;
			}
		});
		premonthBtn.on({
			click:function(){
				tab=false;
				me.myTag='3';
	            datearea.setValue({start:JShell.Date.getDate(BeginDate),end:EndDate})
				monthBtn.toggle(false);
				premonthBtn.toggle(true);
				myFieldId.setText('查询上月未审数据');
				me.onGridSearch();
				tab=true;
			}
		});
	},
	/**
	 * 获取上一个月的1号
	 */
	getPreMonthDate: function(date) {
		var arr = date.split('-');
		var year = arr[0]; //获取当前日期的年份
		var month = arr[1]; //获取当前日期的月份
		var day = arr[2]; //获取当前日期的日
		var days = new Date(year, month, 0);
		days = days.getDate(); //获取当前日期中月的天数
		var year2 = year;
		var month2 = parseInt(month) - 1;
		if(month2 == 0) {
			year2 = parseInt(year2) - 1;
			month2 = 12;
		}
		var day2 = day;
		var days2 = new Date(year2, month2, 0);
		days2 = days2.getDate();
		if(day2 > days2) {
			day2 = days2;
		}
		if(month2 < 10) {
			month2 = '0' + month2;
		}
		var t2 = year2 + '-' + month2 + '-01';
		return t2;
	},
	onRefreshClick:function(){
		var me = this;
		me.onGridSearch();
	},
	/**时间比较，超过半年提示*/
	checkEndDate :function(newValue){
		var me =this;
        var isexect=true;
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
	    var startdatecom = buttonsToolbar.getComponent('startdate');
	    var enddatecom=buttonsToolbar.getComponent('enddate');
		startdate=	startdatecom.getValue();	 
		endDate=enddatecom.getValue();
		if(!startdate)return;
		if(!endDate)return;
		//最大结束时间
		var nowenddata =JcallShell.Date.getNextDate(startdate,182.5);
	    if((startdate && endDate) &&(startdate>endDate)){
	    	JShell.Msg.alert('结束时间不能小于开始时间', null, 2000);
			isexect=false;
			return false;
	    }
		if((startdate && endDate) && (nowenddata<newValue)){
			JShell.Msg.alert("只能查半年内的数据", null, 2000);
			isexect=false;return false;
		}
		return isexect;
	},
	/**批量审核按钮点击处理方法*/
	onSaveCheckReport: function() {
		var me = this,
			records = me.getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var list=[];
		//找到未审核的行数据
		for (var i in records) {
			if(records[i].get('EReportData_IsCheck') == '0') {
				var EReportData_Id = records[i].get('EReportData_Id');
				var TempletID = records[i].get('EReportData_TempletID');
				var ReportDate = records[i].get('EReportData_ReportDate');
				var b = ReportDate.split("/");
				//当前行EReportData_ReportDate的1号
				var stratDate = b[0] + '-' + b[1] + '-01';
				//当前行EReportData_ReportDate月的最后一天
				var endDate = JcallShell.Date.getMonthLastDate(b[0], b[1], true);
				var obj={
					Id:EReportData_Id,
					TempletID:TempletID,
					stratDate:stratDate,
					endDate:endDate
				};
				list.push(obj);
			}
		}
		if(list.length==0){
			JShell.Msg.alert('选中行已审核');
			return;
		} 
        Ext.MessageBox.show({
			title: '操作确认消息',
			msg: '批量审核选中行？',
			icon: Ext.Msg.QUESTION,
			buttons: Ext.MessageBox.OKCANCEL,
			fn: function(btn) {
				if(btn == 'ok') {
					me.delErrorCount = 0;
					me.delCount = 0;
					me.delLength = list.length;
		            
					me.showMask("正在审核..."); //显示遮罩层
				    for(var i=0;i<list.length;i++){
						var TempletID = list[i].TempletID;
						var stratDate = list[i].stratDate;
						var endDate = list[i].endDate;
						me.SaveOne(i, TempletID,stratDate,endDate);
					}
				}
			}
		});
	},
	/**审核一条数据*/
	SaveOne: function(index, TempletID,stratDate,endDate) {
		var me = this;
		var url = (me.CheckReportUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.CheckReportUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'templetID=' + TempletID;
        if(stratDate != '') {
			url += "&beginDate=" + stratDate;
		}
		if(endDate != '') {
			url += "&endDate=" + endDate;
		}
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if (data.success) {
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount == 0){
						me.onGridSearch();
					}else{
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	},
	/**打开预览窗口*/
	openForm: function(rowIndex) {
		var me = this;
		var maxWidth = document.body.clientWidth - 380;
		var height = document.body.clientHeight - 60;
		var config = {
			width: maxWidth,
			height: height,
            rowIndex:rowIndex,
			CurrentPageData:me.getCurrentPageData(),
			SUB_WIN_NO : '1',
			listeners: {
				close:function(){
					me.onGridSearch();
				},
				onSaveClick: function(win) {
					me.fireEvent('onSaveClick', win);
				}
			}
		};
		var win =JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.CheckApp', config);
		win.show();
	},
	/**获取当前页数据
	 * 根据选择行的id 找到行位置，截取行之后的数据并返回
	 * */
	getCurrentPageData:function(){
		var me = this;
		var records = me.myGridPanel.store.data.items,
			len = records.length;
		var list=[];
		for(var i=0;i<len;i++){
			var obj ={
				EReportData_ReportDataID:records[i].data.EReportData_ReportDataID,
				EReportData_ReportDate:records[i].data.EReportData_ReportDate,
				EReportData_TempletID:records[i].data.EReportData_TempletID,
				EReportData_ReportName:records[i].data.EReportData_ReportName,
				EReportData_IsCheck:records[i].data.EReportData_IsCheck,
				EReportData_TempletBatNo:records[i].data.EReportData_TempletBatNo
			}
	     	list.push(obj);
		}
		return list;
	},
	/**时间比较，超过半年提示*/
	checkStartDate :function(newValue){
		var me =this;
		var isexect=true;
		
		var buttonsToolbar = me.myGridPanel.getComponent('buttonsToolbar');
	    var startdatecom = buttonsToolbar.getComponent('startdate');
	    var enddatecom=buttonsToolbar.getComponent('enddate');
		strval=	startdatecom.getValue();	 
		endval=	enddatecom.getValue();
		if(!strval)return;
		if(!endval)return;
	 	var year = strval.getFullYear();
		var month = strval.getMonth() + 1;
		//开始时间  X年X月01日
		var startdate = JShell.Date.getMonthFirstDate(year, month);
		var year2 = endval.getFullYear();
		var month2 = endval.getMonth() + 1;
		//结束时间  X年X月01日
		var endDate = JShell.Date.getMonthFirstDate(year2, month2);
		endDate = JcallShell.Date.toString(endDate, true);
		//最小开始时间
		var Start =JcallShell.Date.getNextDate(endDate,-182.5);
		var year3 = newValue.getFullYear();
		var month3 = newValue.getMonth() + 1;
		//正在改变的值  X年X月01日
	    newValue = JShell.Date.getMonthFirstDate(year3, month3);
		if(JShell.Date.getMonthFirstDate(year, month)> JShell.Date.getMonthFirstDate(year2, month2)){
			JShell.Msg.alert('开始时间不能大于结束时间', null, 2000);
			isexect=false;
			return  false;
		}
		if(newValue<Start){
			JShell.Msg.alert("只能查半年内的数据", null, 2000);
			isexect=false;
			return false;
		}
		return isexect;
	},
	/**显示附件信息*/
	showAttachmentById: function(id, TempletID, beginDate, endDate) {
		var maxWidth = document.body.clientWidth - 280;
		var height = document.body.clientHeight - 60;
		JShell.Win.open('Shell.class.qms.equip.templet.ereportdata.PdfPanel', {
			formtype: "show",
			height: height,
			width: maxWidth,
			TempletID: TempletID,
			beginDate: beginDate,
			endDate: endDate,
			PK: id
		}).show();
	}
});