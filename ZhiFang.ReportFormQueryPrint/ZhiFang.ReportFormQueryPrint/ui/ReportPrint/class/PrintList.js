/**
 * 打印列表
 * @author Jcall
 * @version 2014-10-15
 */
Ext.define('Shell.ReportPrint.class.PrintList',{
    extend: 'Shell.ux.panel.Grid',

    requires: [
		'Shell.ux.form.field.ComboBox'
    ],
	
	title: '打印列表',
	width:400,
	height:300,
	multiSelect:false,
	defaultPageSize:50,
	//remoteSort:false,
	pagingtoolbar:'simple',
	/**获取数据服务路径*/
	selectUrl: '/ServiceWCF/ReportFormService.svc/SelectReport',
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
	DateField: 'RECEIVEDATE',
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
	
	/**默认选中第一行*/
	autoSelect:false,
	//选中的行Index
	clickRowIndex:null,
	
	//选中的行数
	selectRowCount:0,

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
		//服务路径
		me.selectUrl += '?fields=' + me.getStoreFields().join(',');
	    //getRowClass：更改行样式
		//me.viewConfig = { getRowClass: me.changeRowClass };
	    //点击复选框才选中行
	    me.selModel = {
	    	checkOnly: me.CheckOnly
	    };
	    //创建功能按钮栏
		me.createButtonsToolbar();
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
	    if (me.hasPrint) {
	        buttons.push({
	            xtype: 'checkbox', itemId: 'upprint', boxLabel: '未打印&nbsp;', checked: me.checkFilter,
	            handler: function () {
	                me.onSearch();
	            }
	        });
	        buttons.push({
	            xtype: 'uxcombobox', itemId: 'dept', dataSize: 1000, width: 120, editable: true,hidden:true,
	            displayField: 'CName', valueField: 'DeptNo', selectFirst: true, enableKeyEvents: true,
	            firstData: [{ CName: '所有科室', DeptNo: 0, ShortCode: 'ALL' }],
	            url: '/ServiceWCF/DictionaryService.svc/GetDeptList?fields=DeptNo,CName&WHERE=',
	            tpl: '<tpl for="."><li role="option" class="x-boundlist-item">{CName}[<b style="color:blue;">{ShortCode}</b>]</li></tpl>',
	            storeConfig: {
	                fields: ['CName', 'DeptNo', 'ShortCode']
	            },
	            listeners: {
	                beforequery: function (e) {
	                    var combo = e.combo;
	                    if (!e.forceAll) {
	                        var value = e.query.toLocaleUpperCase();
	                        combo.store.filterBy(function (record, id) {
	                            var text = record.get(combo.displayField).toLocaleUpperCase();
	                            var shortCode = record.get('ShortCode').toLocaleUpperCase();
	                            return (text.indexOf(value) != -1 || shortCode.indexOf(value) != -1);
	                        });
	                        combo.expand();
	                        return false;
	                    }
	                },
	                keypress: function (f, e) {
	                    if (e.keyCode == e.ENTER) {
	                        me.onSearch();
	                    }
	                }
	            }
	        });
	        buttons.push('-');

	        //buttons.push({xtype:'checkbox',itemId:'preview',boxLabel:'预览',handler:function(){}});
	        buttons.push({
	            xtype: 'checkbox', itemId: 'hasFilter', boxLabel: '过滤', checked: me.checkFilter,
	            handler: function () { }
	        });

	        buttons.push('-');
	        buttons.push({
	            xtype: 'combobox', itemId: 'strPageName',
	            mode: 'local', editable: false,
	            displayField: 'text', valueField: 'value',
	            width: 60, value: me.printType,
	            store: new Ext.data.SimpleStore({
	                fields: ['text', 'value'],
	                data: [['A4', 'A4'], ['A5', 'A5'], ['双A5', '双A5']]
	            })
	        });
            //谷歌下不显示双面打印
	        if (!Ext.isChrome) {
	            buttons.push('-', {
	                xtype: 'checkbox', itemId: 'isDoublePrint', boxLabel: '双面', checked: me.checkDoublePrint,
	                handler: function () { }
	            });
	        }


			buttons.push('-','print');
			
	        me.multiSelect = true;//取消复选框
	    }
	    buttons.push('->');
	    buttons.push({ xtype: 'uxbutton', itemId: 'collapse', text: '', iconCls: 'button-left', tooltip: '<b>收缩面板</b>' });

	    me.toolbars = me.toolbars || [{
	        dock: 'top', itemId: 'toptoolbar', buttons: ['refresh']
	    }];
	    me.toolbars[0].buttons = me.toolbars[0].buttons.concat(buttons);
	},
    /**创建数据列*/
	createGridColumns: function () {
	    var me = this;
	    var columns = [];
	    columns.push({xtype: 'rownumberer', text: '序号', width: 40, align: 'center'});
	    columns.push({ dataIndex: 'Bed', text: '床号', width: 40, sortable: false });
	    //columns.push({ dataIndex: 'PatNo', text: '病历号', width: 80, sortable: false });
	    columns.push({
	        dataIndex: me.DateField, text: '报告日期', width: 150, sortable: false, renderer: function (v, meta, record, index) {
	            //显示审核时间
	            var value = record.get("CHECKDATE").split(' ')[0];//+" "+record.get("CHECKTIME").split(' ')[1];
	            //var value = v ? Shell.util.Date.toString(v, true) : "";
	            //meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
	            return value;
	        }
	    });
	    columns.push({ dataIndex: 'CNAME', text: '姓名', width: 60, sortable: false });
	    columns.push({ dataIndex: 'SAMPLENO', text: '样本号', width: 45, sortable: false });
	    columns.push({
	        dataIndex: 'ItemName', text: '检验项目', width: 140, sortable: false,
	        renderer: function (value, meta, record) {
	            if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
	            return value;
	        }
	    });

	    //数据列信息
	    me.columns = me.columns || columns;

	    if (me.hasPrint) {
	    	me.columns.push({ dataIndex: 'PatNo', text: '病历号', hidden: true });
		    me.columns.push({ dataIndex: 'PageName', text: '纸张类型', width: 80, hideable: true, hidden: true });
		    me.columns.push({ dataIndex: 'PageCount', text: '文件页量', width: 80, hideable: true, hidden: true });
	        me.columns.push({
	            dataIndex: 'PRINTTIMES', text: '打印次数', width: 60, hideable: false,
	            renderer: function (v, meta, record) {
	                var imgName = (v && v >= me.maxPrintTimes) ? "unprint" : "print",
		                tootip = "已经打印<b style='color:red;'> " + v + " </b>次",
	                    value = v ? "  <b>" + v + "</b>" : "";

	                meta.tdAttr = 'data-qtip="' + tootip + '"';
	                
	                var result = '';
	                if(v >= 0){
	                    result = "<img src='" + Shell.util.Path.uiPath + "/ReportPrint/images/" + imgName + ".png'/>" + v;
	                }
	                
	                return result;
	            }
	        });
	    }
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
	    var fields = [
			'ReportFormID','FormNo','SAMPLENO','SECTIONNO','CNAME','CLIENTNO','SectionType',
			'RECEIVEDATE', 'CHECKDATE', 'PRINTTIMES', 'ItemName', 'PatNo', 'Serialno', 'Bed', 'CHECKTIME'
	    ];
	    var bo = false,bo2 = false;
		for(var i in fields){
			if(me.DateField == fields[i]){bo = true;}
			if(me.ForcedPagingField && me.ForcedPagingField.dataIndex == fields[i]){bo2 = true;}
		}
	    if(!bo) fields.push(me.DateField);
	    if(!bo2 && me.ForcedPagingField) fields.push(me.ForcedPagingField.dataIndex);
	    if (me.hasPrint) { fields.push('PageName', 'PageCount'); }
		
	    return fields;
	},
    /**创建挂靠*/
	createDockedItems: function () {
	    var me = this;
	    var dockedItems = me.callParent(arguments);
	    
	    if(me.hasPdfPrinter){
	    	//新建一个WScript.Shell对象     
			me.Shell = new ActiveXObject("WScript.Shell");
			//默认打印机
			me._defaultPrinter = me.Shell.RegRead("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows\\Device");
			
			var defaultPrinter = me._defaultPrinter.split(',')[0],
				list = me.pdfPrinterList || [],
				len = list.length,
				data = [];
			
			data.push([defaultPrinter,'【默认打印机】' + defaultPrinter]);//默认打印机
			//报告打印机
			for(var i=0;i<len;i++){
				data.push([list[i],'【报告打印机】' + list[i]]);
			}
			
	    	//打印机选择
		    dockedItems.unshift({
		    	dock:'top',
		    	xtype:'toolbar',
		    	itemId:'toptoolbar2',
		    	items:[{
		    		xtype:'combobox',itemId:'printer',
		            mode:'local',editable:false,
		            displayField:'text',valueField:'value',
		            width:'100%',fieldLabel:'打印机',labelWidth:50,
		            labelAlign:'right',value:defaultPrinter,
		            store:new Ext.data.SimpleStore({
		                fields: ['value','text'],
		                data: data
		            })
		    	}]
		    });
	    }

	    dockedItems.push(Ext.create('Shell.ReportPrint.class.PrintPdfWinS', {
	        itemId: 'PrintPdfWin',
	        header: false,
	        height: 0,
	        width: 0,
	        mergePageCount: me.mergePageCount,
	        clreaTimes: me.clreaTimes,
	        openAddPrintTimes: me.openAddPrintTimes,
	        DateField: me.DateField,
	        ForcedPagingField: me.ForcedPagingField,
	        hasPdfPrinter:me.hasPdfPrinter,//是否需要选择打印机
	        defaultPrinter:me._defaultPrinter,//默认打印机
	        listeners:{
	        	printStart:function(p){
	        		me.disableControl();
	        	},//禁用功能按钮
	        	printEnd:function(p,ids){
	        		me.enableControl();
	        		me.onPrintEnd(ids);
	        	}//启用功能按钮
	        }
	    }));
	    
	    return dockedItems;
	},
	/**打印完毕处理*/
	onPrintEnd:function(ids){
		var me = this,
			list = ids || [],
			len = list.length;
			
		for(var i=0;i<len;i++){
			var record = me.store.findRecord("ReportFormID",ids[i]);
			if(record){
				var num = parseInt((record.get("PRINTTIMES") || "0")) + 1;
				record.set("PRINTTIMES",num);
				record.commit();
			}
		}
		//清理所有勾选
		me.getSelectionModel().deselectAll();
	},
	/**点击直接打印处理*/
	onPrintClick:function(but){
		var me = this;
	    /**
		 * 批量打印功能只支持IE8以上版本
		 * @JcallShell
		 * @version 2018-07-04
		 */
		var IEVersion = Shell.util.IE.getIEVersion();
		//非IE、IE6、IE7、Edge的浏览器需要下载IE8
	    if (IEVersion == -1 || IEVersion == 6 || IEVersion == 7 || IEVersion == 'edge') {
	        if (Ext.isChrome) {
	            me.getComponent("PrintPdfWin").IsChrome = true;
	        } else {
	            Shell.util.Msg.showMsg({
	                title: '提示信息',
	                msg: '批量打印功能只支持IE8以上版本!需要下载IE8浏览器吗？',
	                icon: Ext.Msg.QUESTION,
	                buttons: Ext.Msg.OKCANCEL,
	                callback: function (btn) {
	                    if (btn != "ok") return;
	                    window.open(Shell.util.Adobe.IE8Url);
	                }
	            }, me);
	            return;
	        }
		}
		
		/**
		 * 打印必须先安装adobe组件，如果没有安装，弹出安装文件下载
		 * @JcallShell
		 * @version 2018-07-04
		 */
		var isAcrobatPluginInstall = Shell.util.Adobe.isInstall();
		if(!isAcrobatPluginInstall){
			window.open(Shell.util.Adobe.Url);
			return;
		}
		
		var me = this,
			toptoolbar = me.getComponent('toptoolbar'),
			strPageName = toptoolbar.getComponent('strPageName').getValue(),
			hasFilter = me.getComponent('toptoolbar').getComponent('hasFilter'),
			hasFilter = hasFilter ? hasFilter.getValue() : null,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			filterLen = 0;
			
		if(len == 0){
			Shell.util.Msg.showError("必须选择数据才能进行打印!",me);
			return;
		}
		
		for(var i=0;i<len;i++){
			if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			filterLen++;
		}
		if(filterLen == 0){
			Shell.util.Msg.showError("必须选择数据才能进行打印!",me);
			return;
		}
        //超出设置的打印数量提示
		if (len > me.printCountSetting) {
			Shell.util.Msg.showError("超出最大打印数量:" + me.printCountSetting + ",请修改勾选的数据!",me);
		    return;
		}

		if(strPageName == "A5"){
		    var A4List = [];;
            
			//for(var i=0;i<len;i++){
			//	if(hasFilter && records[i].get('PRINTTIMES') > 0) continue;//过滤已打印的数据
			//	if(records[i].get('PageName') == "A4"){
			//		A4List.push(records[i]);
			//	}
			//}
			if(A4List.length > 0){
				var error = ["<center><b>A5方式打印时不能选择A4报告!</b></center>"];
				for(var i=0;i<A4List.length;i++){
					var a4 = A4List[i],
						text = 
							'<b style="color:red;margin:0 5px;">报告日期【</b><b>' + 
							Shell.util.Date.toString(a4.get('RECEIVEDATE'), true) + '</b>' +
							'<b style="color:red;">】</b>' +
							'<b style="color:red;margin:0 5px;">姓名【</b><b>' + 
							a4.get('CNAME') + 
							'<b style="color:red;">】</b>' +
							'<b style="color:red;margin:0 5px;">样本号【</b><b>' + 
							a4.get('SAMPLENO') + 
							'<b style="color:red;">】</b>' +
							'<b style="margin:0 5px;">A4</b>';
					error.push(text);
				}
				
				Shell.util.Msg.showError(error.join("</br>"),me);
				return;
			}
		}
		if(me.maxPrintTimes){
			var error = [];
			for(var i=0;i<len;i++){
				var rec = records[i];
				//最大打印数量
				if(rec.get('PRINTTIMES') >= me.maxPrintTimes){
					var text = 
						'<b style="color:red;margin:0 5px;">报告日期【</b><b>' + 
						Shell.util.Date.toString(rec.get('RECEIVEDATE'), true) + '</b>' +
						'<b style="color:red;">】</b>' +
						'<b style="color:red;margin:0 5px;">姓名【</b><b>' + 
						rec.get('CNAME') + 
						'<b style="color:red;">】</b>' +
						'<b style="color:red;margin:0 5px;">样本号【</b><b>' + 
						rec.get('SAMPLENO') + 
						'<b style="color:red;">】</b>' +
						'<b style="margin:0 5px;">已打印 </b><b style="color:red;">' + 
						rec.get('PRINTTIMES') + '</b><b> 次</b>';
						
					error.push(text);
				}
			}
			if(error.length > 0){
				error.unshift("<center><b>已打印" + me.maxPrintTimes + "次及以上次数的报告不能再打印!</b></center>");
				Shell.util.Msg.showError(error.join("</br>"),me);
				return;
			}
		}
		//打印勾选的报告
		me.printAll(records);
	},
	/**打印勾选的报告*/
	printAll:function(records){
		var me = this,
			PrintPdfWin = me.getComponent('PrintPdfWin');
			
		if (PrintPdfWin) {
			if(me.hasPdfPrinter){
				var printer = me.getComponent('toptoolbar2').getComponent('printer').getValue();
				PrintPdfWin.print(me.getReportPrintInfo(),null,printer);
			}else{
				PrintPdfWin.print(me.getReportPrintInfo());
			}
		} else {
		    Shell.util.Msg.showError('打印组件加载失败！',me);
		}
	},
	/**获取勾选的数据*/
	getReportPrintInfo:function(){
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
            upprint = toptoolbar.getComponent('upprint'),
            dept = toptoolbar.getComponent('dept'),
            where = [];

        //是否已打印
	    if (upprint) {
	        var checked = upprint.getValue();
	        if (checked) {
	            where.push("PRINTTIMES=0");
	        }
	    }

        //科室ID
	    if (dept) {
	        var v = dept.getValue();
	        if (v) {
	            if (Ext.typeOf(v) == 'string') {
	                v = 0;
	                dept.select('所有科室');
	            }
	        } else {
	            dept.select('所有科室');
	        }

	        if (v) {
	            where.push("DeptNo=" + v);
	        }
	    }

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

            for (var i = 0; i < len;i++){
                orderby.push(me.defaultOrderBy[i].field + " " + me.defaultOrderBy[i].order);
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
    },
     /**加载数据后*/
	onAfterLoad:function(records,successful){
		var me = this;
		me.callParent(arguments);	
		me.fireEvent('focus',me);
	}
});