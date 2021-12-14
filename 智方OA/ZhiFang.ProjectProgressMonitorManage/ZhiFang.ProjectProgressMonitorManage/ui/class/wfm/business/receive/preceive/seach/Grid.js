/**
 * 商务收款查询
 * @author liangyl
 * @version 2017-08-01
 */
Ext.define('Shell.class.wfm.business.receive.preceive.seach.Grid',{
	extend: 'Shell.ux.grid.Panel',
    title: '商务收款查询',
    requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
    width: 800,
    height: 500,

   	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPReceiveByHQL?isPlanish=true',

    /**默认加载*/
    defaultLoad: true,

    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
    defaultOrderBy: [{ property: 'PReceive_ReceiveDate', direction: 'DESC' }],
    /*默认开始时间*/
	StratDate:null,
	/*默认结束时间*/
	EndDate:null,
	features: [{ftype: 'summary'}],
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.initFilterListeners();
    },

    initComponent: function () {
        var me = this;
        me.iniDate();
        //数据列
        me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
        me.callParent(arguments);
    },
    
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
        var columns = [{
            text: '用户', dataIndex: 'PReceive_PClientName', minWidth: 140,flex:1,
            sortable: false, defaultRenderer: true
        }, {
            text: '付款单位', dataIndex: 'PReceive_PayOrgName', minWidth: 140,flex:1,
            sortable: false, defaultRenderer: true
        }, {
            text: '合同名称', dataIndex: 'PReceive_PContractName', minWidth: 140,flex:1,
            sortable: false, defaultRenderer: true
        }, {
            text: '签署时间', dataIndex: 'PReceive_ContractSignDateTime', width: 85,
            sortable: false, defaultRenderer: true,	isDate: true
        }, {
            text: '收款分期', dataIndex: 'PReceive_PReceivePlan_ReceiveGradationName', width: 100,
            sortable: false, summaryRenderer: function(value) {
				return '<div  style="text-align:right" ><strong>本页合计:</strong></div>';
			}
        }, {
			text: '本次收款金额',dataIndex: 'PReceive_ReceiveAmount',width: 85,
			sortable: false,type: 'number',xtype: 'numbercolumn',summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		},  {
            text: '本次收款时间', dataIndex: 'PReceive_ReceiveDate', width: 85,
            sortable: false, defaultRenderer: true,isDate: true
        }, {
            text: '计划收款金额', dataIndex: 'PReceive_PReceivePlan_ReceivePlanAmount', width: 85,
            sortable: false, defaultRenderer: true
        }, {
            text: '计划收款时间', dataIndex: 'PReceive_PReceivePlan_ExpectReceiveDate', width: 85,
            sortable: false, defaultRenderer: true,isDate: true
        }, {
            text: '负责人', dataIndex: 'PReceive_ReceiveManName', width: 70,
            sortable: false, defaultRenderer: true
        }];

        return columns;
    },
    /**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll(); //清空数据
		if(!me.defaultLoad) return false;
		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
	},	
    /**初始化时间*/
	iniDate:function() {
		var me=this;
		//近两个月
		var Sysdate = JcallShell.System.Date.getDate();
        me.StratDate = JShell.Date.getNextDate(Sysdate, -60);
        me.EndDate=JcallShell.Date.toString(Sysdate, true);
	},
    /**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.unshift('refresh','-');
		buttonToolbarItems.push({
			fieldLabel:'收款日期',labelWidth:55,labelAlign:'right',width:150,itemId:'BeginDate',xtype:'datefield',format:'Y-m-d',
			value:me.StratDate
		},{
			width:100,labelWidth:5,fieldLabel:'-',labelSeparator:'',
			itemId:'EndDate',xtype:'datefield',format:'Y-m-d',value:me.EndDate
		},{
			width:160,labelWidth:50,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'ReceiveManName',fieldLabel:'收款人',
			className: 'Shell.class.sysbase.user.CheckApp',
		    classConfig: {
				title: '收款计划收款人选择'
		    }
		},{
			xtype:'textfield',itemId:'ReceiveManID',fieldLabel:'负责人Id',hidden:true
		});
		return buttonToolbarItems;
	},
	
	initFilterListeners:function(){
    	var me=this;
        var buttonsToolbar = me.getComponent('buttonsToolbar');
    	 //收款人
		var ReceiveManName = buttonsToolbar.getComponent('ReceiveManName'),
			ReceiveManID = buttonsToolbar.getComponent('ReceiveManID');
			
		if(ReceiveManName){
			ReceiveManName.on({
				check:function(p, record) {
					ReceiveManName.setValue(record ? record.get('HREmployee_CName') : '');
					ReceiveManID.setValue(record ? record.get('HREmployee_Id') : '');
					p.close();
				},
				change:function(){me.onGridSearch();}
			});
		}
		
        var  BeginDate = buttonsToolbar.getComponent('BeginDate');
        var  EndDate = buttonsToolbar.getComponent('EndDate');
        if(BeginDate){
			BeginDate.on({change:function(){me.onGridSearch();}});
		}
		if(EndDate){
			EndDate.on({change:function(){me.onGridSearch();}});
		}    
   },
   getParms:function(){
   	var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			ReceiveManID = null,BeginDate = null,EndDate = null,
			search = null,params = [];

		
			
		if(buttonsToolbar){
			ReceiveManID= buttonsToolbar.getComponent('ReceiveManID').getValue();
			BeginDate = buttonsToolbar.getComponent('BeginDate').getValue();
			EndDate = buttonsToolbar.getComponent('EndDate').getValue();
		}
		//负责人
		if(ReceiveManID) {
			params.push("preceive.ReceiveManID='" + ReceiveManID + "'");
		}
		if(BeginDate){
			params.push("preceive.ReceiveDate"  + ">='" + JShell.Date.toString(BeginDate,true) + "'");
		}
		if(EndDate){
			params.push("preceive.ReceiveDate"  + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.internalWhere;
   },
   
      /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere=me.getParms();
		return me.callParent(arguments);
	},
		/**创建分页栏*/
	createPagingtoolbar: function() {
		var me = this;
		var config = {
			dock: 'bottom',
			itemId:'pagingToolbar',
			store: me.store
		};
		if (me.defaultPageSize) config.defaultPageSize = me.defaultPageSize;
		if (me.pageSizeList) config.pageSizeList = me.pageSizeList;
		me.agingToolbarCustomItems=['->',{
			xtype: 'label',
			itemId:'labText',
			style: "font-weight:bold;color:black;",
	        text: '总计:',
	        margin: '0 0 0 10'
		}];
		//分页栏自定义功能组件
		if (me.agingToolbarCustomItems) config.customItems = me.agingToolbarCustomItems;

		return Ext.create('Shell.ux.toolbar.Paging', config);
	},
	  /**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		this.onPFinanceReceiveTotal();
		this.load(null, true, autoSelect);
	},
	/**商务收款*/
	onPFinanceReceiveTotal:function(){
		var me = this;
		var buttonsToolbar = me.getComponent('pagingToolbar');
	    var labText=buttonsToolbar.getComponent('labText');
	    var Total=0;
        me.getPFinanceReceiveTotal(function(data){
       	    if(data.value && data){
       	    	Total=data.value;
       	    }
        });
        labText.setText("总计:"+Total);
	},
	/**商务收款统计*/
	getPFinanceReceiveTotal:function(callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceiveTotalByHQL';
		url += '?fields=ReceiveAmount';
		var where = me.getParms();
//		if(where) where = "(" + where + ")";
		if(where) {
			url += '&where=' + where;
		}
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	}
});