Ext.define("Shell.class.CheckReportRequest.basic.sampleStateList.sampleStateGrid", {
    extend: 'Shell.ux.panel.Grid',
     requires: [
		'Shell.ux.form.field.ComboBox'
    ],
	
	title: '打印列表',
	width:400,
	//height:300,
	multiSelect:false,
	defaultPageSize:50,
	layout:'fit',
	//remoteSort:false,
	pagingtoolbar:'simple',
	/**获取数据服务路径*/
	selectUrl: '/ServiceWCF/ReportFormService.svc/SelectRequestFormAndReportFormCount',
    /**当前打印ID*/
	printId:null,
	/**打印错误信息数组*/
	errorInfo:[],
	/**打印动作*/
	printAction:null,
	/**需要标识打印的数据ID*/
	formIds:null,
	/**开启单元格内容提示*/
	tooltip:false,//兼容移动端，避免点击两次才触发点击事件
	/**是否开启打印功能*/
	hasPrint: true,
    //批量打印数量配置
	printCountSetting: 9999,
    /**A4纸张类型，1(A4) 2(16开)*/
	A4Type: 1,
    /**默认勾选过滤框*/
	checkFilter: false,
    /**默认打印类型*/
	printType: 'A4',
    /**默认顺序*/
	defaultOrderBy: [],
    /**报告时间字段*/
	DateField: 'OPERDATE',
    /**强制分页字段*/
	ForcedPagingField: '',
    /**点击复选框才选中行*/
    CheckOnly:false,
    
    /**最大打印数量*/
    maxPrintTimes:2,
    /**默认勾选双面打印*/
	checkDoublePrint: false,
	/**是否需要选择打印机*/
	hasPdfPrinter:false,
	/**PDF文件打印机数组*/
	pdfPrinterList: [],
	/**默认打印机*/
	_defaultPrinter:'',
	fields:'', //获取的数据字段
	/**默认选中第一行*/
	autoSelect:false,
	//选中的行Index
	clickRowIndex:null,
    /**排序字段*/
	itemOrder: [],
	//选中的行数
	selectRowCount: 0,
	/**后台排序*/
	remoteSort: false,
    appType :'',//页面类型
    IsbTempReport:false,//是否开启部分审核报告
    IsQueryRequest:false,//是否查询request表
	afterRender: function () {
	    var me = this;
	    if (me.defaultLoad) {
	        me.internalWhere = me.getInternalWhere();
	    }
	    me.callParent(arguments);
	    
	    me.on({
	    	itemclick:function(view,record,item,index) {
	    		me.focus();//默认光标自动定位面板
	    		var nowSelectRowCount = me.getSelectionModel().getCount();
	    		if(nowSelectRowCount != me.selectRowCount){
	    			me.selectRowCount = nowSelectRowCount;
	    			return;
	    		}
	    		
	    		if(me.clickRowIndex != null){
	    			me.getView().removeRowCls(me.clickRowIndex,'x-grid-record-itemclick');
	    		}
				me.getView().addRowCls(index,'x-grid-record-itemclick');
				me.clickRowIndex = index;
				me.RowClickIndex = index;
			},
			afterload:function(){
					setTimeout(function(){
						me.RowClickIndex = 0;
						me.onRowClick();						
					},10);
					setTimeout(function(){
						me.RowClickIndex = 0;
						me.onRowClick();
					},3000);
			}
	    });
	    //键盘上箭头监听
	    me.KeyUpMap = new Ext.util.KeyMap({
			target: me.getEl(),
			key: Ext.EventObject.UP,
			fn: me.onKeyUp,
			scope: me
		});
		//键盘下箭头监听
        me.KeyDownMap = new Ext.util.KeyMap({
			target: me.getEl(),
			key: Ext.EventObject.DOWN,
			fn: me.onKeyDown,
			scope: me
		});
		//默认光标自动定位面板
		me.focus();
	},
	initComponent:function(){
	    var me = this;
		
		me.addEvents('beforeSearch');
	    
		
	    //getRowClass：更改行样式
		//me.viewConfig = { getRowClass: me.changeRowClass };
	    //点击复选框才选中行
	    me.selModel = {
	    	checkOnly: me.CheckOnly
	    };
	    //创建功能按钮栏
		//me.createButtonsToolbar();
        //创建数据列
		me.createGridColumns();
	    //创建数据集属性
		me.createGridStore();
		
		me.callParent(arguments);
	},
    /**创建功能按钮栏*/
	createButtonsToolbar: function () {
	    var me = this;
	    //功能按钮栏
	    var buttons = [];	    
	    buttons.push('->');
	    buttons.push({ xtype: 'uxbutton', itemId: 'collapse', text: '', iconCls: 'button-left', tooltip: '<b>收缩面板</b>' });

	    me.toolbars = me.toolbars || [{
	        dock: 'top', itemId: 'toptoolbar', buttons: ['refresh']
	    }];
	    me.toolbars[0].buttons = me.toolbars[0].buttons.concat(buttons);
	},
	
    //默认排序
	SetDefalutOrder:function () {
	    var me = this;
	    var items = me.itemOrder;
	    if (items.length > 1) {
	        for (var i = 0; i < items.length; i++) {
	            for (var j = 0; j < items.length; j++) {
	                if (items[i].OrderDesc < items[j].OrderDesc) {
	                    var t = items[i];
	                    items[i] = items[j];
	                    items[j] = t;
	                }
	            }
	        }
	    }
	    for (var i = 0; i < items.length; i++) {
	        var order = items[i].OrderMode ? items[i].OrderMode : 'ASC';
	        me.defaultOrderBy.push({ field: items[i].ColumnName, order: order });
	        if(items[i].ColumnName =="CHECKDATE"){
	    		me.defaultOrderBy.push({ field: 'CHECKTIME', order: order });	
	    	}
	    }
	},

    //获得列配置
	getColumnsSetting:function () {
	    var me = this;
	    var columns = [];	    
	    
		columns.push({ dataIndex: "CNAME",draggable: true,hidden: false,hideable: true,sortable: true,text: "姓名",triStateSort: false,width: 60});
	    columns.push({dataIndex: "PatNo",draggable: true,hidden: false,hideable: true,sortable: true,text: "病历号",triStateSort: false,width: 120});
	    columns.push({dataIndex: "Bed",draggable: true,hidden: false,hideable: true,sortable: true,text: "床号",triStateSort: false,width: 60});
	    columns.push({dataIndex: "ItemNum",draggable: true,hidden: false,hideable: true,sortable: true,text: "数量",triStateSort: false,width: 60});
	    //设置排序
	    //me.SetDefalutOrder();
	    return columns;
	},
    /**创建数据列*/
	createGridColumns: function () {
	    var me = this;
	    var col = me.getColumnsSetting();
	    me.fields = col;
        //服务路径
	    me.selectUrl += '?fields=' + me.getStoreFields().join(',');

	    var columns = [];
	    columns.push({xtype: 'rownumberer', text: '序号', width: 40, align: 'center'});

        //数据列信息
	    columns=Ext.Array.merge(columns, col);
	    me.columns = me.columns || columns;
	},
    /**创建数据集属性*/
	createGridStore: function () {
	    var me = this;
	    //数据集属性
	    me.storeConfig = me.storeConfig || {};
	    me.storeConfig.proxy = me.storeConfig.proxy || {
	        type: 'ajax',
	        url: '',
	        reader: { type: 'json', totalProperty: 'total', root: 'rows' },
	        extractResponseData: function (response) {
	            var result = Ext.JSON.decode(response.responseText);

	            if (result.success) {
	                var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
	                result.total = ResultDataValue.total;
	                result.rows = ResultDataValue.rows;
	            } else {
	                result.total = 0;
	                result.rows = [];
	                Shell.util.Msg.showError(result.ErrorInfo);
	            }
	            response.responseText = Ext.JSON.encode(result);
	            return response;
	        }
	    };
	},
    /**获取数据字段*/
	getStoreFields: function () {
	    var me = this;
	    var fields = [];
	    fields.push("PatNo");
		fields.push("CNAME");
	    fields.push("Bed");
	    fields.push("DeptNo");
	    fields.push("CollectDate");
	    fields.push("CollectTime");
	    fields.push("OperDate");
	    fields.push("OperTime");
	    fields.push("DeptName");
	    fields.push("SickTypeNo");
	    fields.push("SickTypeName");
	    fields.push("ItemNum");	    
	    return fields;
	},
   
	
	
	/**获取勾选的数据*/
	getReportPrintInfo:function(reco){
	    var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			strPageName = toptoolbar.getComponent('strPageName').getValue(),
			hasFilter = me.getComponent('toptoolbar').getComponent('hasFilter'),
			hasFilter = hasFilter ? hasFilter.getValue() : null,
			isDoublePrint = me.getComponent('toptoolbar').getComponent('isDoublePrint'),
			isDoublePrint = isDoublePrint ? isDoublePrint.getValue() : null,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			data = [];
			if(reco){
				records = reco;
				len = records.length;
			}
		/**
		 * 解决列表勾选不能按页面顺序打印的处理
		 * @JcallShell
		 * @version 2018-07-28
		 */
		var count = me.store.getCount(),
			tempArr = [];
		for(var i=0;i<count;i++){
			tempArr.push(null);
		}
		for(var i=0;i<len;i++){
			var rec = records[i];
			tempArr[rec.index % count] = rec;
		}
			
		for(var i=0;i<count;i++){
			if(tempArr[i] == null) continue;
			if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			
			var record = tempArr[i],
				ReportFormID = record.get('ReportFormID'),
				DATE = Shell.util.Date.toString(record.get('RECEIVEDATE'), true),
				fileName = DATE + "/" + ReportFormID + ".pdf";
				
			var obj = {
			    ReportFormID: ReportFormID,
			    DATE: DATE,
			    SectionNo:record.get('SECTIONNO'),
			    SectionType:record.get('SectionType'),
			    CNAME: record.get('CNAME'),
			    SAMPLENO: record.get('SAMPLENO'),
			    PageName: record.get('PageName'),//纸张类型,A4/A5
			    PageCount: record.get('PageCount'),//文件页量
			    url: Shell.util.Path.reportPath + "/" + fileName
			};
			if (me.ForcedPagingField) {
			    obj[me.ForcedPagingField.dataIndex] = record.get(me.ForcedPagingField.dataIndex);
			}
			data.push(obj);
		}
		
		return {
		    A4Type:me.A4Type,
			strPageName:strPageName,
			isDoublePrint:isDoublePrint,
			data:data
		};
    },
   
	/**收缩*/
	onCollapseClick:function(but){
		this.collapse();
	},
	
    /**重写查询功能*/
	onSearch: function () {
	    this.internalWhere = this.getInternalWhere();
	    this.load(null, true);
	},
    /**获取内部条件*/
	getInternalWhere: function () {
	    var me = this,
            toptoolbar = this.getComponent('toptoolbar'),
            //upprint = toptoolbar.getComponent('upprint'),
            //dept = toptoolbar.getComponent('dept'),
            where = [];       
	    return where.join(" and ");
	},
    /**获取带查询参数的URL*/
	getLoadUrl: function () {
	    var me = this,
	    url = me.callParent(arguments);
	    var arryurl = url.split("SerialNo");
	    if (arryurl.length > 1) {
	        url = arryurl[0];
	    }
	    
	    if (me.defaultOrderBy.length > 0) {
	        var len = me.defaultOrderBy.length;
                orderby = [];
	        var flag = false;
            for(var i = 0;i<len;i++){
	           	if(orderby.length == 0){
	           		orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order );
	           	}else{
	           		for(var j = 0;j<orderby.length;j++){
	           			if(orderby[j] == me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order){
	           				flag = true;
	           			}
	           		}
	           		if(!flag){
	           			orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order );
	           			flag = false;
           			}
           		}
           		
            }
            
	        var index = url.indexOf('where=');

	        if (index != -1) {
	            url += ' ORDER BY ' + orderby.join(",").replace('\'','%27');
	        }

	        if (arryurl.length > 1) {
	            url += "&SerialNo=" + arryurl[1];
	        }
	    }
	    return url;
	},
    /**更改行样式*/
	changeRowClass: function (record, rowIndex, rowParams, store) {
	    if (!record.get("PRINTTIMES")) {
	        return 'x-grid-record-unprint';
	    }
	    return 'x-grid-record-print';
	},
	onBeforeLoad: function () {
	    var me = this;
	    me.fireEvent('beforeSearch', this);
	    me.callParent(arguments);
	},
	//行点击处理
	onRowClick:function(){
		var me = this,
			index = me.RowClickIndex,
			el = me.getView().getNode(index);
		
		if(el){
			el.focus();
			el.click();
		}
	},
	//键盘上箭头触发
	onKeyUp: function(e) {
    	var me = this;
    	if(me.RowClickIndex > 0){
    		me.RowClickIndex--;
    		me.onRowClick();
    	}
    },
    //键盘上箭头触发
    onKeyDown:function(e){
    	var me = this;
    	if(me.RowClickIndex < me.store.getCount() - 1){
    		me.RowClickIndex++;
    		me.onRowClick();
    	}
    },/**加载数据后*/
	onAfterLoad:function(records,successful){
		var me = this;
		
		me.enableControl();//启用所有的操作功能
		
		if(!successful || records.length == 0){
			me.store.removeAll();
			return;
		}
		
		var autoSelect = me.autoSelect,
			type = Ext.typeOf(autoSelect),
			num = autoSelect === true ? 0 : -1;
  		
  		if(type === 'string'){//需要选中的行主键
			num = me.store.find(me.PKColumn,autoSelect);
		}
		//选中行号为num的数据行
		if(num >= 0){me.getSelectionModel().select(num);}
	},
	/**重写load 根据where条件加载数据*/
	load: function (where, isPrivate) {
		var me = this,
			collapsed = me.getCollapsed();

		me.defaultLoad = true;
		me.externalWhere = isPrivate ? me.externalWhere : where;

		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if (collapsed) {
			me.isCollapsed = true;
			return;
		}

		me.store.currentPage = 1;
		//禁止后台排序，remoteSort属性无效暂时使用此方法
		me.store.sorters.items = [];
		me.store.sorters.keys = [];
		me.store.sorters.map = {};
		me.store.sorters.length = 0;
		me.store.load();
	},
    
});