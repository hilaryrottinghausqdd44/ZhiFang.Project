/**
 * 财务收款列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.wfm.business.receive.finance.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '财务收款列表',
	requires: [
		'Shell.ux.toolbar.Button',
    	'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
	//	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPFinanceReceiveByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePFinanceReceiveByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPFinanceReceive',
	/**默认排序字段*/
	defaultOrderBy: [{
		property: 'PFinanceReceive_ReceiveDate',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
	/**后台排序*/
	remoteSort: true,
	/**默认每页数量*/
	defaultPageSize: 50,
	hasRefresh: true,
	hasAdd: true,
	PKField: 'PFinanceReceive_Id',
	/**收入分类字典*/
	IncomeTypeName: 'IncomeTypeName',
	/**收入类别字典*/
	IncomeClass:'IncomeClass',
	/*执行公司名称*/
	Compname: 'OurCorName',
	/*默认开始时间*/
	StratDate:null,
	/*默认结束时间*/
	EndDate:null,
	features: [{
		ftype: 'summary'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.enableControl(); //启用所有的操作功能
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.iniDate();
		//创建数据列
		me.columns = me.createGridColumns();
		me.initButtonToolbarItems();
		me.addEvents('addclick');
		me.addEvents('checkclick');
		me.callParent(arguments);
	},
	
	
	/**初始化功能按钮栏内容*/
	initButtonToolbarItems: function() {
		var me = this;
		me.buttonToolbarItems = ['refresh'];
		me.buttonToolbarItems.push('-', {
			fieldLabel: '付款单位',
			width: 220,
			labelAlign: 'right',
			colspan: 1,
			labelWidth: 63,
			name: 'PFinanceReceive_PClient_Name',
			itemId: 'PFinanceReceive_PClient_Name',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.wfm.client.CheckGrid'
		}, {
			fieldLabel: '主键ID',
			xtype: 'textfield',
			name: 'PFinanceReceive_PClient_Id',
			itemId: 'PFinanceReceive_PClient_Id',
			hidden: true
		});
		if(me.hasAdd) {
			me.buttonToolbarItems.push('-', {
				text: '新增',
				tooltip: '新增',
				iconCls: 'button-add',
				itemId: 'Add',
				name: 'Add',
				xtype: 'button',
				disabled: true,
				handler: function() {
					me.fireEvent('addclick', me);
				}
			})
		}
		me.buttonToolbarItems.push('-',{
			width:75,labelAlign:'right',boxLabel:'分配完成',itemId:'IsSplit',
		    xtype:'checkbox',checked:false
	    },{
			width:85,labelAlign:'right',boxLabel:'分配未完成',itemId:'IsNotSplit',
		    xtype:'checkbox',checked:true
	    });
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '编号',
			dataIndex: 'PFinanceReceive_Id',
			width: 170,
			hidden: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '收款时间',
			dataIndex: 'PFinanceReceive_ReceiveDate',
			width: 90,
			sortable: true,
			defaultRenderer: true,
			isDate: true
		}, {
			text: '付款单位',
			dataIndex: 'PFinanceReceive_PayOrgName',
			width: 180,
			sortable: false,
			summaryRenderer: function(value) {
				return '<div  style="text-align:right" ><strong>当前合计:</strong></div>';
			}
		}, {
			text: '收款金额',
			dataIndex: 'PFinanceReceive_ReceiveAmount',
			width: 85,
			sortable: false,type: 'number',xtype: 'numbercolumn',summaryType: 'sum',
			renderer: function(value, meta, record, rowIndex, colIndex, store, veiw) {
				value = Ext.util.Format.number(value, value > 0 ? '0.00' : "0");
				return value;
			},
			summaryRenderer: function(value) {
				return '<strong>' + Ext.util.Format.number(value, value > 0 ? '0.00' : "0") + '</strong>';
			}
		}, {
			text: '已分配',
			dataIndex: 'PFinanceReceive_SplitAmount',
			width: 85,
			sortable: false,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				var ReceiveAmount = record.get("PFinanceReceive_ReceiveAmount");
				var v = value;

				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';

				if(ReceiveAmount === value) {
					meta.style = 'font-weight:bold;color:#2C2C2C';
				}
				if(ReceiveAmount > value) {
					meta.style = 'font-weight:bold;color:#008B00';
				}
				if(ReceiveAmount < value) {
					meta.style = 'font-weight:bold;color:#EE0000';
				}
				return v;
			}
		}, {
			text: '备注',
			dataIndex: 'PFinanceReceive_ReceiveMemo',
			width: 200,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '付款单位',
			dataIndex: 'PFinanceReceive_PayOrgID',
			width: 150,
			sortable: false,
			defaultRenderer: true,
			hidden: true
		}, {
			text: '客户名称',
			dataIndex: 'PFinanceReceive_PClientName',
			width: 180,
			sortable: false,
			defaultRenderer: true
		},{
			text: '收入分类',
			dataIndex: 'PFinanceReceive_IncomeTypeName',
			width: 100,
			sortable: false,
			defaultRenderer: true
		},{
			text: '收入类别',
			dataIndex: 'PFinanceReceive_IncomeClassName',
			width: 100,
			sortable: false,
			defaultRenderer: true
		},{
			text: '资金帐户',
			dataIndex: 'PFinanceReceive_FundAccount',
			width: 100,
			sortable: false,
			defaultRenderer: true
		},{
			text: '核算年份',
			dataIndex: 'PFinanceReceive_AccountsYear',
			width: 55,
			sortable: false,
			defaultRenderer: true
		},{
			text: '核算月份',
			dataIndex: 'PFinanceReceive_YearMonth',
			width: 55,
			sortable: false,
			defaultRenderer: true
		},{
			text: '执行公司',
			dataIndex: 'PFinanceReceive_ComponeName',
			width: 180,
			sortable: false,
			defaultRenderer: true
		},{
			text: '录入人',
			dataIndex: 'PFinanceReceive_InputerName',
			width: 70,
			sortable: false,
			defaultRenderer: true
		},{
			text: '录入时间',
			dataIndex: 'PFinanceReceive_DataAddTime',
			width: 135,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '审核人',
			dataIndex: 'PFinanceReceive_ReviewMan',
			width: 70,
			sortable: false,
			defaultRenderer: true
		},{
			text: '审核时间',
			dataIndex: 'PFinanceReceive_ReviewDate',
			width: 135,
			menuDisabled: false,
			isDate: true,
			hasTime: true,
			sortable: false,
			defaultRenderer: true
		},{
			text: '审核意见',
			dataIndex: 'PFinanceReceive_ReviewInfo',
			width: 180,
			sortable: false,
			renderer: function(value, meta, record) {
				var v=me.showMemoText(value, meta, record);
				return v;
			}
		}];
		return columns;
	},
	/**初始化时间*/
	iniDate:function() {
		var me=this;
		//近两个月
		var Sysdate = JcallShell.System.Date.getDate();
        me.StratDate = JShell.Date.getNextDate(Sysdate, -60);
        me.EndDate=JcallShell.Date.toString(Sysdate, true);
	},
    /**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		me.doDictListeners();
		//字典监听
		var dictList = ['IncomeType', 'IncomeClass', 'Compone'];
		for(var i = 0; i < dictList.length; i++) {
			me.DictListeners(dictList[i]);
		}
		var buttonsToolbar2=me.getComponent('buttonsToolbar2');
		var  BeginDate = buttonsToolbar2.getComponent('BeginDate');
        var  EndDate = buttonsToolbar2.getComponent('EndDate');
        if(BeginDate){
			BeginDate.on({change:function(){me.onSearch();}});
		}
		if(EndDate){
			EndDate.on({change:function(){me.onSearch();}});
		}    
		
	},
	/**字典监听*/
	DictListeners: function(name) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var CName = buttonsToolbar.getComponent('PFinanceReceive_' + name + 'Name');
		var Id = buttonsToolbar.getComponent('PFinanceReceive_' + name + 'ID');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BDict_CName') : '');
				Id.setValue(record ? record.get('BDict_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
	/**付款单位监听*/
	doDictListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var CName = buttonsToolbar.getComponent('PFinanceReceive_PClient_Name');
		var Id = buttonsToolbar.getComponent('PFinanceReceive_PClient_Id');
		var Add = buttonsToolbar.getComponent('Add');

		if(!CName) return;
		var val = '',Name='';
		CName.on({
			check: function(p, record) {
			    val = record ? record.get('PClient_Id') : '';
				Name=record ? record.get('PClient_Name') : '';
				CName.setValue(Name);
				Id.setValue(val);
				me.onSearch();
				me.fireEvent('checkclick',val,Name);
				p.close();
			}
		});
		var IsSplit= buttonsToolbar.getComponent('IsSplit');
        var IsNotSplit= buttonsToolbar.getComponent('IsNotSplit');
        IsSplit.on({
        	change:function(com,  newValue,  oldValue,  eOpts ){
        	   me.onSearch();
        	}
        });
        IsNotSplit.on({
        	change:function(com,  newValue,  oldValue,  eOpts ){
        		me.onSearch();
        	}
        });
	},
   	showMemoText:function(value, meta, record){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v ="" + value;
		var length=me.getStrLeng(value);
		if(length > 0)v = (length > 16 ? v.substring(0, 16) : v);
		if(length>16){
			v= v+"...";
		}
		v = v.replace(/<.*?>/ig,"");
		value = value.replace(me.regStr, "'");
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px;'>" + "<b>审核意见:</b>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},// UTF8字符集实际长度计算 
    getStrLeng:function (str){ 
	    var realLength = 0; 
	    var len = str.length; 
	    var charCode = -1; 
	    for(var i = 0; i < len; i++){ 
	        charCode = str.charCodeAt(i); 
	        if (charCode >= 0 && charCode <= 128) {  
	            realLength += 1; 
	        }else{  
	            // 如果是中文则长度加3 
	            realLength += 3; 
	        } 
	    }  
	    return realLength; 
	} ,
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var Add = buttonsToolbar.getComponent('Add');
		var PClientId = buttonsToolbar.getComponent('PFinanceReceive_PClient_Id').getValue();
		if(PClientId) {
			Add.enable();
		} else {
			Add.disable();
		}
	},
    getParms:function(){
    	var me = this,
    		PClientId = null,
			search = null,FundAccount= null,BeginDate= null,EndDate= null,
			IncomeTypeID= null,IncomeClassID= null,ComponeID= null,
			IsSplit=false,IsNotSplit=false,
			params = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar');
        if(buttonsToolbar){
        	 PClientId = buttonsToolbar.getComponent('PFinanceReceive_PClient_Id').getValue();
        	 IsSplit= buttonsToolbar.getComponent('IsSplit').getValue();
            IsNotSplit= buttonsToolbar.getComponent('IsNotSplit').getValue();
        }
        var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
        
        if(buttonsToolbar2){
            IncomeTypeID = buttonsToolbar2.getComponent('PFinanceReceive_IncomeTypeID').getValue();
            IncomeClassID = buttonsToolbar2.getComponent('PFinanceReceive_IncomeClassID').getValue();
            ComponeID = buttonsToolbar2.getComponent('PFinanceReceive_ComponeID').getValue();
            FundAccount = buttonsToolbar2.getComponent('FundAccount').getValue();
            BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue();
            EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
            
        }
        
		//付款单位
		if(PClientId) {
			params.push("pfinancereceive.PayOrgID='" + PClientId + "'");
		}
		//收入分类
		if(IncomeTypeID) {
			params.push("pfinancereceive.IncomeTypeID='" + IncomeTypeID + "'");
		}
		//收入类别
		if(IncomeClassID) {
			params.push("pfinancereceive.IncomeClassID='" + IncomeClassID + "'");
		}
		//执行公司
		if(ComponeID) {
			params.push("pfinancereceive.CompnameID='" + ComponeID + "'");
		}
		//资金账号
		if(FundAccount) {
			params.push("pfinancereceive.FundAccount='" + FundAccount + "'");
		}
		if(BeginDate){
			params.push("pfinancereceive.ReceiveDate"  + ">='" + JShell.Date.toString(BeginDate,true) + "'");
		}
		if(EndDate){
			params.push("pfinancereceive.ReceiveDate"  + "<'" + JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
		}
		
		//未分配完成
		if(IsNotSplit){
			params.push("(pfinancereceive.ReceiveAmount>pfinancereceive.SplitAmount)");
		}
		//已分配
		if(IsSplit){
			params.push("pfinancereceive.ReceiveAmount=pfinancereceive.SplitAmount");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.internalWhere;
    },
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere=me.getParms();
		return me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:[{
				width:160,labelWidth:55,labelAlign:'right',
				xtype:'uxCheckTrigger',itemId:'PFinanceReceive_IncomeTypeName',fieldLabel:'收入分类',
				className: 'Shell.class.wfm.dict.CheckGrid',
			    classConfig: {
					title: '收入分类选择',
					defaultWhere: "pdict.BDictType.DictTypeCode='" + this.IncomeTypeName + "'"
			    }
			},{
				xtype:'textfield',itemId:'PFinanceReceive_IncomeTypeID',fieldLabel:'收入分类ID',hidden:true
			},{
				width:160,labelWidth:55,labelAlign:'right',
				xtype:'uxCheckTrigger',itemId:'PFinanceReceive_IncomeClassName',fieldLabel:'收入类别',
				className: 'Shell.class.wfm.dict.CheckGrid',
			    classConfig: {
					title: '收入类别选择',
					defaultWhere: "pdict.BDictType.DictTypeCode='" + this.IncomeClass + "'"
				}
			},{
				xtype:'textfield',itemId:'PFinanceReceive_IncomeClassID',fieldLabel:'收入类别ID',hidden:true
			},{
				width:160,labelWidth:55,labelAlign:'right',
				xtype:'uxCheckTrigger',itemId:'PFinanceReceive_ComponeName',fieldLabel:'执行公司',
				className: 'Shell.class.wfm.dict.CheckGrid',
			    classConfig: {
					title: '执行公司选择',
					defaultWhere: "pdict.BDictType.DictTypeCode='" + this.Compname + "'"
				}
			},{
				xtype:'textfield',itemId:'PFinanceReceive_ComponeID',fieldLabel:'执行公司ID',hidden:true
			},{
				xtype:'textfield',itemId:'FundAccount',labelWidth:55,labelAlign:'right',width:160,fieldLabel:'资金帐户'
			},'-',{
				fieldLabel:'收款时间',labelWidth:55,labelAlign:'right',width:150,itemId:'BeginDate',xtype:'datefield',format:'Y-m-d',
				value:me.StratDate
			},{
				width:100,labelWidth:5,fieldLabel:'-',labelSeparator:'',
				itemId:'EndDate',xtype:'datefield',format:'Y-m-d',value:me.EndDate
			}]
		};
		return items;
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
		var url = JShell.System.Path.ROOT + '/SingleTableService.svc/ST_UDTO_SearchPFinanceReceiveTotalByHQL';
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