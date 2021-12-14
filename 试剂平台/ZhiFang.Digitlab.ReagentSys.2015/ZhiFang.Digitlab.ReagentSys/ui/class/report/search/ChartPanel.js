/**
 * 历史对比
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.report.search.ChartPanel', {
	extend: 'Shell.class.report.search.Chart',
	
	/**语言包内容*/
	Shell_class_report_search_ChartPanel:{
		title:{
			TEXT:'历史对比'
		},
		RECEIVEDATE:'核收日期'
	},
	
	/**获取数据服务路径*/
	selectUrl:'/ServiceWCF/ReportFormService.svc/ResultHistory',
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	serverParams:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initListeners();
		me.disableControl();
	},
	initComponent: function() {
		var me = this;
		
		//替换语言包
		me.changeLangage('Shell.class.report.search.ChartPanel');
		
		//标题
		me.title = me.Shell_class_report_search_ChartPanel.title.TEXT;
		
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createDockedItems:function(){
		var me = this;
		var items = [];
		
		//实验室
		items.push({
			xtype:'label',
			style:'margin:2px;color:#04408c;font-weight:bold',
			text:me.title
		},{
			width: 165,
			labelWidth: 70,
			fieldLabel: me.Shell_class_report_search_ChartPanel.RECEIVEDATE,
			labelAlign:'right',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		},{
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		});
		
		items.push('-','searchb','->','COLLAPSE_DOWN');
		
		var dockedItems = Ext.create('Shell.ux.toolbar.Button',{
			dock:'top',
			itemId:'buttonsToolbar',
			items:items
		});
		
		return dockedItems;
	},
	/**刷新按钮点击处理方法*/
	onRefreshClick:function(){
		this.onSearch(true);
	},
	/**查询*/
	onSearchBClick:function(){
		this.externalWhere = '';
		this.onSearch(true);
	},
	onSearch:function(isPrivate){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
			
		if(buttonsToolbar){
			//更改核收日期值
			if(!isPrivate) me.changeDate();
			
			var BeginDate = buttonsToolbar.getComponent('BeginDate').getValue(),
				EndDate = buttonsToolbar.getComponent('EndDate').getValue(),
				params = [];
				
			if(BeginDate){
				params.push("rf.RECEIVEDATE>='" + JShell.Date.toString(BeginDate,true) + "'");
			}
			if(EndDate){
				params.push("rf.RECEIVEDATE<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
			}
			
			me.internalWhere = params.join(' and ');
		}
		
		me.load(isPrivate);
	},
	load:function(isPrivate){
		var me = this,
			collapsed = me.getCollapsed(),
			params = me.serverParams,
			url = JShell.System.Path.ROOT + me.selectUrl;
			
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
			
		if(!params){
			me.changeData({list:[]});
			return;
		}
		
		var where = [];
		if(me.defaultWhere) where.push(me.defaultWhere);
		if(me.internalWhere) where.push(me.internalWhere);
		if(me.externalWhere) where.push(me.externalWhere);
		
		url += "?PatNo=" + params.PatNo + "&ItemNo=" + params.ItemNo + "&Table=" + params.Table;
		
		url += '&where=' + where.join(' and ') || '1=1';
		
		me.showMask(me.loadingText);//显示遮罩
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩
			if(data.success){
				me.changeData({list:data.value});
			}else{
				me.showErrror(data.msg);
			}
		});
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		}
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		}
		me.enableControl(); //启用所有的操作功能
	},
	/**更改日期内容*/
	changeDate:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BeginDate = buttonsToolbar.getComponent('BeginDate'),
			EndDate = buttonsToolbar.getComponent('EndDate');
			
		var ReceiveDate = me.serverParams.ReceiveDate,
			sDate = '',
			eDate = '';
		
		if(ReceiveDate){
			sDate = JShell.Date.getDate(ReceiveDate);
			eDate = JShell.Date.getNextDate(sDate,90);
		}
		
		BeginDate.setValue(sDate);
		EndDate.setValue(eDate);
	},
	/**收缩面板*/
	onCollapseClick:function(){
		this.collapse();
	},
	/**开启功能栏*/
    enableControl: function () {
        this.disableControl(true);
    },
    /**禁用功能栏*/
    disableControl: function (bo) {
        var me = this,
			toptoolbar = me.getComponent('buttonsToolbar'),
			items = toptoolbar.items.items,
			len = items.length;

        for (var i = 0; i < len; i++) {
            items[i][bo ? "enable" : "disable"]();
        }
    },
	/**清空数据*/
    clearData:function(){
    	var me = this;
		me.disableControl();
		me.showErrror("");
    },
    /**初始化监听*/
    initListeners:function(){
    	var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BeginDate = buttonsToolbar.getComponent('BeginDate'),
			EndDate = buttonsToolbar.getComponent('EndDate');
			
		//回车键监听
		new Ext.KeyMap(BeginDate.getEl(),[{
	      	key:Ext.EventObject.ENTER,
	      	fn:function(){
	       		me.onSearch(true);
	      	}
     	}]);
     	new Ext.KeyMap(EndDate.getEl(),[{
	      	key:Ext.EventObject.ENTER,
	      	fn:function(){
	       		me.onSearch(true);
	      	}
     	}]);
     	
     	me.on({
			expand:function(p,d){
				if(me.isCollapsed){
					me.onSearch(true);
				}
				me.isCollapsed = false;
			}
		});
    }
});