/**
 * 项目跟踪记录浏览
 * @author liangyl	
 * @version 2017-08-07
 */
Ext.define('Shell.class.wfm.business.projecttrack.interaction.seach.Grid', {
	extend:'Shell.ux.grid.Panel',
	requires:[
	    'Ext.ux.RowExpander',
	    'Shell.ux.form.field.CheckTrigger'
    ],
    
	title:'互动列表 ',
	width:800,
	height:500,
	/**默认展开内容*/
	defaultShowContent:true,
	
  	/**获取数据服务路径*/
	selectUrl:'',
	/**附件对象名*/
	objectName:'',
	/**附件关联对象名*/
	fObejctName:'',
	/**附件关联对象主键*/
	fObjectValue:'',
	/**交流关联对象是否ID*/
	fObjectIsID:false,
  	
	/**默认加载*/
	defaultLoad:false,
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用序号列*/
	hasRownumberer:false,
	
	/**默认每页数量*/
	defaultPageSize:20,
	/**分页栏下拉框数据*/
	pageSizeList:[
		[10,10],[20,20],[50,50],[100,100],[200,200]
	],
	/**默认结束时间*/
	defaultEndDateDate:null,
	/**默认开始时间*/
    defaultBeginDateDate:null,
	constructor:function(config){
		var me = this;
		me.plugins = [{
			ptype:'rowexpander',
			rowBodyTpl :[
//				'<p><b>内容:</b></p>',
				'<p>{' + config.objectName + '_Contents}</p>'
			]
		}];
		
		me.callParent(arguments);
	},
	afterRender:function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			load:function(){
				me.changeShowType(me.showType);
			},
			resize:function(){
//				var gridWidth = me.getWidth();
//				var width = gridWidth - me.columns[0].getWidth() - me.columns[1].getWidth() - me.columns[3].getWidth();
//			    me.columns[2].setWidth(width - 20);
			}
		});
		me.initListeners();
	},
	initComponent:function() {
		var me = this;
		me.initDate();	
		me.columns = me.createGridColumns();
		
		me.buttonToolbarItems = ['refresh',{
			xtype:'checkbox',
			boxLabel:'展开内容',
			itemId:'showContent',
			checked:me.defaultShowContent,
			listeners:{
				change:function(field,newValue,oldValue){
					me.changeShowType(newValue);
				}
			}
		},'-',{
			text: '本周',tooltip: '本周',xtype: 'button',width: 45,name: 'Thisweek',itemId: 'Thisweek'
		}, {
			text: '本月',tooltip: '本月',xtype: 'button',width: 45,name: 'Thismonth',itemId: 'Thismonth'
		},{
			width:155,labelWidth:60,labelAlign:'right',fieldLabel:'发表时间',
			itemId:'BeginDate',xtype:'datefield',format:'Y-m-d',value:me.defaultBeginDateDate
		},{
			width:100,labelWidth:5,fieldLabel:'-',labelSeparator:'',
			itemId:'EndDate',xtype:'datefield',format:'Y-m-d',value:me.defaultEndDateDate
		},'-', {
			labelWidth: 60,labelAlign:'right',width:165,
			fieldLabel:'跟踪人员',xtype: 'uxCheckTrigger',itemId: 'UserName',
			className: 'Shell.class.sysbase.user.CheckApp'
		}, {
			xtype: 'textfield',itemId: 'UserID',fieldLabel: '申请人主键ID',hidden: true
		}];
		
		me.showType = me.defaultShowContent;
		me.defaultOrderBy = [{property:me.objectName + '_DataAddTime',direction:'DESC'}];
		me.callParent(arguments);
	},
	
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'发表时间',dataIndex:me.objectName + '_DataAddTime',isDate:true,hasTime:true,width:135
		},{
			text:'发表人',minWidth:100,width:120,dataIndex:me.objectName + '_SenderName',
			sortable:false,menuDisabled:true,
			renderer:function(value,meta,record){
				var isOwner = record.get(me.objectName + '_SenderID') == 
					JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
				var color = isOwner ? "color:green" :"color:#000";
				var v = '<b style=\'' + color + '\'>' + record.get(me.objectName + '_SenderName') + '</b>';
				if (v) {
					meta.tdAttr = 'data-qtip="' + v + '"';
				}
				return v;
			}
		},{
			text:'项目编号',dataIndex:me.objectName + '_PContractFollow_ContractNumber',width:140, defaultRenderer: true
		},{
			text:'公司名称',dataIndex:me.objectName + '_PContractFollow_Compname',width:140, defaultRenderer: true
		},{
			text:'客户名称',dataIndex:me.objectName + '_PContractFollow_PClientName',width:140, defaultRenderer: true
		},{
			text:'付款单位',dataIndex:me.objectName + '_PContractFollow_PayOrg',width:140, defaultRenderer: true
		},{
			text:'项目类别',dataIndex:me.objectName + '_PContractFollow_Content',width:100, defaultRenderer: true
		},{
			text:'项目名称',dataIndex:me.objectName + '_PContractFollow_Name',width:140, defaultRenderer: true
		},{
			text:'金额',dataIndex:me.objectName + '_PContractFollow_Amount',width:85, defaultRenderer: true
		},{
			text:'申请人',dataIndex:me.objectName + '_PContractFollow_ApplyMan',width:85, defaultRenderer: true
		},{
			text:'申请时间',dataIndex:me.objectName + '_PContractFollow_ApplyDate',isDate:true,hasTime:true,width:135
		},{
			text:'销售负责人',dataIndex:me.objectName + '_PContractFollow_Principal',width:85, defaultRenderer: true
		},{
			text:'计划签署时间',dataIndex:me.objectName + '_PContractFollow_PlanSignDateTime',width:135,isDate:true,hasTime:true, defaultRenderer: true
		},{
			text:'签署人',dataIndex:me.objectName + '_PContractFollow_SignMan',width:85, defaultRenderer: true
		},{
			text:'签署日期',dataIndex:me.objectName + '_PContractFollow_SignDate',width:85,isDate:true, defaultRenderer: true
		},{
			text:'服务开始时间',dataIndex:me.objectName + '_PContractFollow_ServerContractStartDateTime',width:85,isDate:true, defaultRenderer: true
		},{
			text:'到期时间',dataIndex:me.objectName + '_PContractFollow_ServerContractEndDateTime',width:85,isDate:true, defaultRenderer: true
		},{
			text:'原项目总额',dataIndex:me.objectName + '_PContractFollow_OriginalMoneyTotal',width:85, defaultRenderer: true
		},{
			text:'服务费比例',dataIndex:me.objectName + '_PContractFollow_ServerChargeRatio',hidden:true,width:85,
		    renderer:function(value,meta,record){
		    	var v=value+'%';
				if (v) {
					meta.tdAttr = 'data-qtip="' + v + '"';
				}
				return v;
			}	
		},{
			xtype:'rownumberer',
			text:'序号',
			width:40,
			align:'center'
		},{
			text:'主键ID',isKey:true,hidden:true,hideable:false,dataIndex:me.objectName + '_Id'
		},{
			text:'发表人ID',dataIndex:me.objectName + '_SenderID',notShow:true
		},{
			text:'内容',dataIndex:me.objectName + '_Contents',notShow:true
		}];
		
		return columns;
	},
	/**初始化送检时间*/
	initDate: function() {
		var me = this;
		var Sysdate = JcallShell.System.Date.getDate();
//		var defaultDate = JcallShell.Date.getNextDate(Sysdate, -7);
        var Sysdate = JcallShell.System.Date.getDate();
	    var nowDayOfWeek = Sysdate.getDay(); //今天本周的第几天
		var nowDay = Sysdate.getDate(); //当前日     
		var LastMonthValue = Sysdate.getMonth(); //上月 
		var nowYear = Sysdate.getYear(); //当前年   
		nowYear += (nowYear < 2000) ? 1900 : 0; // 
	     //获得本周的开始日期
		var getWeekStartDate = new Date(nowYear, LastMonthValue, nowDay - nowDayOfWeek);
		var getWeekStartDate = me.formatDate(getWeekStartDate);
		//获得本周的结束日期
		var getWeekEndDate = new Date(nowYear, LastMonthValue, nowDay + (6 - nowDayOfWeek));
		var getWeekEndDate = me.formatDate(getWeekEndDate);

		me.defaultBeginDateDate = JcallShell.Date.toString(getWeekStartDate, true);
		me.defaultEndDateDate = JcallShell.Date.toString(getWeekEndDate, true);
	},
	 //初始化监听
	initListeners:function(){
    	var me=this;
        var buttonsToolbar = me.getComponent('buttonsToolbar');
        
        	//本周
		var	Thisweek = buttonsToolbar.getComponent('Thisweek'),
			//本月
			Thismonth = buttonsToolbar.getComponent('Thismonth');
    	 //客户
		var showContent = buttonsToolbar.getComponent('showContent'),
			BeginDate = buttonsToolbar.getComponent('BeginDate'),
			EndDate = buttonsToolbar.getComponent('EndDate');
		if(BeginDate) {
			BeginDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && EndDate.getValue()){
						me.onSearch();
					}				
				}
			});
		}
		if(EndDate) {
			EndDate.on({
				change: function(com, newValue, oldValue, eOpts) {
					if(newValue && BeginDate.getValue()){
						me.onSearch();
					}
				}
			});
		}
		var Sysdate = JcallShell.System.Date.getDate();
	    var nowDayOfWeek = Sysdate.getDay(); //今天本周的第几天
		var nowDay = Sysdate.getDate(); //当前日     
		var LastMonthValue = Sysdate.getMonth(); //上月 
		var nowYear = Sysdate.getYear(); //当前年   
		nowYear += (nowYear < 2000) ? 1900 : 0; // 
		Thisweek.on({
			click: function() {
				//获得本周的开始日期
				var getWeekStartDate = new Date(nowYear, LastMonthValue, nowDay - nowDayOfWeek);
				var getWeekStartDate = me.formatDate(getWeekStartDate);
				//获得本周的结束日期
				var getWeekEndDate = new Date(nowYear, LastMonthValue, nowDay + (6 - nowDayOfWeek));
				var getWeekEndDate = me.formatDate(getWeekEndDate);
				BeginDate.setValue(getWeekStartDate);
				EndDate.setValue(getWeekEndDate);
			}
		});
		//当月
		Thismonth.on({
			click: function() {
				//获得本月的开始日期
				var getMonthStartDate = new Date(nowYear, LastMonthValue, 1);
				var getMonthStartDate = me.formatDate(getMonthStartDate);
				//获得本月的结束日期
				var myDate = JcallShell.Date.toString(Sysdate, true);
				var dayCount = JcallShell.Date.getCountDays(myDate); //该月天数
				var getMonthEndDate = new Date(nowYear, LastMonthValue, dayCount);
				var getMonthEndDate = me.formatDate(getMonthEndDate);
				BeginDate.setValue(getMonthStartDate);
				EndDate.setValue(getMonthEndDate);
			}
		});
		//人员
		var	UserName = buttonsToolbar.getComponent('UserName'),
			UserID = buttonsToolbar.getComponent('UserID');
		if(UserName) {
			UserName.on({
				check: function(p, record) {
					UserName.setValue(record ? record.get('HREmployee_CName') : '');
					UserID.setValue(record ? record.get('HREmployee_Id') : '');
					me.onSearch();
					p.close();
				}
			});
		}
	},
	changeShowType:function(value){
		var me = this;
		me.showType = value ? true :false;
		me.toggleRow(me.showType);
	},
	toggleRow:function(bo){
		var me = this,
			plugins = me.plugins[0],
        	view = plugins.view,
			records = me.store.data,
			len = records.length;
        	
        for(var i=0;i<len;i++){
			var rowNode = view.getNode(i),
	            row = Ext.get(rowNode),
	            nextBd = Ext.get(row).down(plugins.rowBodyTrSelector),
	            record = view.getRecord(rowNode);
	        if(bo){
	        	row.removeCls(plugins.rowCollapsedCls);
	            nextBd.removeCls(plugins.rowBodyHiddenCls);
	            plugins.recordsExpanded[record.internalId] = true;
	        }else{
	        	row.addCls(plugins.rowCollapsedCls);
	            nextBd.addCls(plugins.rowBodyHiddenCls);
	            plugins.recordsExpanded[record.internalId] = false;
	        }
		}
        view.refreshSize();
        if(bo){
            view.fireEvent('expandbody', rowNode, record, nextBd.dom);
        }else{
            view.fireEvent('collapsebody', rowNode, record, nextBd.dom);
        }
    },
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
			EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
			UserID=buttonsToolbar.getComponent('UserID').getValue(),
			params = [];
		me.defaultWhere ='';
		me.objectNameLower = me.objectName.toLocaleLowerCase();
		
		if(me.defaultWhere){
			me.defaultWhere = "(" + me.defaultWhere + ") and ";
		}else{
			me.defaultWhere = "";
		}

		if(BeginDate){
			params.push(me.objectNameLower + ".DataAddTime>='" + 
				JShell.Date.toString(BeginDate,true) + "'");
		}
		if(EndDate){
			params.push(me.objectNameLower + ".DataAddTime<'" + 
				JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
		}
		//员工
		if( UserID) {
			params.push(me.objectNameLower +".SenderID='"  + UserID + "'");
		}
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		return me.callParent(arguments);
	},
	//格式化日期：yyyy-MM-dd     
	formatDate: function formatDate(date) {
		var myyear = date.getFullYear();
		var mymonth = date.getMonth() + 1;
		var myweekday = date.getDate();

		if(mymonth < 10) {
			mymonth = "0" + mymonth;
		}
		if(myweekday < 10) {
			myweekday = "0" + myweekday;
		}
		return(myyear + "-" + mymonth + "-" + myweekday);
	}
});