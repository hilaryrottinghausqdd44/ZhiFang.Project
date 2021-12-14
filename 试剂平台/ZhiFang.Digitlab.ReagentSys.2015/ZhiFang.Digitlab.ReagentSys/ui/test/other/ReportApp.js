Ext.define('ReportApp',{
	extend:'Ext.grid.Panel',
	alias:'widget.reportapp',
	title:'临床检验中心服务报表',
	width:600,
	height:300,
	//===================可配参数====================
	/**
	 * 是否默认打开查询界面
	 * @type Boolean
	 */
	defaultOpenSearchWin:true,
	/**
	 * 获取报表数据的服务地址
	 * @type 
	 */
	getInfoUrl:getRootPath()+'/FinanceService.svc/Finance_UDTO_SearchReport',
	/**
	 * 获取文件路径服务地址
	 * @type String
	 */
	getFileInfoUrl:getRootPath()+'/FinanceService.svc/Finance_UDTO_GetReportFile',
	/**
	 * 下载文件服务地址
	 * @type 
	 */
	downloadUrl:getRootPath()+'/FinanceService.svc/Common_UDTO_DownLoadFile',
	/**
	 * 打印服务地址
	 * @type String
	 */
	printInfoUrl:'',
	//==============================================
	/**
	 * 是否打开了查询界面
	 * @type Boolean
	 */
	hasOpenSearchWin:false,
	/**
	 * 条件查询界面
	 * @type 
	 */
	searchWin:null,
	/**
	 * 导出方式列表
	 * @type 
	 */
	exportTypeList:[
		{type:'word',text:'WORD'},
		{type:'excel',text:'EXCEL(2007)'},
		{type:'xml',text:'XML'},
		{type:'html',text:'HTML'},
		{type:'jpg',text:'JPG'},
		{type:'pdf',text:'PDF'}
	],
	//==============================================
	/**
	 * 渲染后执行代码
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//查询界面
		me.createSearchWin();
	},
	/**
	 * 初始化组件
	 * @private
	 */
	initComponent:function(){
		var me = this;
		//初始化视图
		me.initView();
		me.callParent(arguments);
	},
	/**
	 * 初始化视图
	 * @private
	 */
	initView:function(){
		var me = this;
		//没有获取到数据的时候显示
		me.emptyText = "没有找到数据！";
		//数据集
		me.store = me.createStore();
		//数据列
		me.columns = me.createColumns();
		//挂靠功能
		me.dockedItems = me.createDockedItems();
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var url = me.getListUrl;
		var store = Ext.create('Ext.data.Store',{
			fields:me.getFields(),
			autoLoad:false
		});
		return store;
	},
	/**
	 * 创建数据列
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
		var columns = [{
			text:'病区',width:150,align:'left',
			dataIndex:'SearchResultObject_Dept'
		},{
			text:'项目个数',width:100,align:'right',
			dataIndex:'SearchResultObject_ItemCount'
		},{
			text:'折前金额',width:150,align:'right',
			dataIndex:'SearchResultObject_ItemPrice'
		},{
			text:'折后金额',width:150,align:'right',
			dataIndex:'SearchResultObject_ItemAgioPrice'
		}];
		return columns;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		
		//导出方式菜单内容
		var exportMenu = [];
		var exportTypeList = me.exportTypeList;
		for(var i in exportTypeList){
			var item = {
				text:exportTypeList[i].text,
				type:exportTypeList[i].type,
				iconCls:exportTypeList[i].type+'Icon',
				listeners:{click:function(but){me.downLoadFile(but.type);}}
			};
			exportMenu.push(item);
		}
		
		//顶部功能栏
		var top = {
			xtype:'toolbar',
			itemId:'toolbar-top',
			items:[{
				xtype:'button',disabled:true,itemId:'search',text:'显示查询条件',
				iconCls:'search-img-16',tooltip:'显示查询条件',
				handler:function(but){
					if(me.hasOpenSearchWin){
						me.hideSearchWin();
					}else{
						me.showSearchWin();
					}
				}
			},{
				xtype:'button',disabled:true,itemId:'export',text:'导出',
				iconCls:'exportFile',tooltip:'选择一种导出方式进行操作',
				menu:exportMenu
			},{
				xtype:'button',disabled:true,itemId:'print',text:'打印',
				iconCls:'print',tooltip:'打印符合条件的数据',
				handler:function(but){
					me.print();
				}
			}]
		};
		//底部功能栏
		var bottom = {
			xtype:'toolbar',
			itemId:'toolbar-bottom',
			dock:'bottom',
			items:[{
				xtype:'label',
				itemId:'info'
			},'->',{
				xtype:'label',
				itemId:'count'
			}]
		};
		var dockedItems= [top,bottom];
		return dockedItems;
	},
	/**
	 * 获取数据字段
	 * @private
	 * @return {}
	 */
	getFields:function(){
		var me  =this;
		var fields = ['SearchResultObject_Dept','SearchResultObject_ItemCount','SearchResultObject_ItemPrice','SearchResultObject_ItemAgioPrice'];
		return fields;
	},
	/**
	 * 显示程序界面
	 * @private
	 */
	showSearchWin:function(){
		var me = this;
		if(me.searchWin){
			me.hasOpenSearchWin = true;
			me.searchWin.show();
		}
	},
	/**
	 * 隐藏查询界面
	 * @private
	 */
	hideSearchWin:function(){
		var me = this;
		if(me.searchWin){
			me.hasOpenSearchWin = false;
			me.searchWin.hide();
		}
	},
	
	/**
	 * 创建查询界面
	 * @private
	 */
	createSearchWin:function(){
		var me = this;
		var callback = function(info){
			var par = {
				autoScroll:true,
	    		modal:false,//模态
	    		floating:true,//漂浮
				closable:true,//有关闭按钮
				closeAction:'hide',//关闭时隐藏
				resizable:true,//可变大小
				draggable:true,//可移动
				title:'查询条件',
				layout:'absolute'
			};
			me.searchWin = getPanelByInfoAndPar(info,par);
			
			me.searchWin.on({
				beforeclose:function(panel){
					me.hasOpenSearchWin = false;
				}
			});
			
			if(me.defaultOpenSearchWin){
				me.hasOpenSearchWin = true;
				me.searchWin.show();
			}
			//初始化查询页面的数据
			me.initValues();
			//初始化监听
			me.initListeners();
		};
		var id = "5162030365732315717";
		//util文件里面的公共方法
		getAppObjectById(id,callback);
	},
	/**
	 * 初始化查询页面数据
	 * @private
	 */
	initValues:function(){
		var me = this;
		//送检日期
		me.searchWin.setValueByItemId('SearchParameterObject_OperdateStart','');
		me.searchWin.setValueByItemId('SearchParameterObject_OperdateEnd','');
		//财务审核
		me.searchWin.setValueByItemId('SearchParameterObject_IsLockdateStart','');
		me.searchWin.setValueByItemId('SearchParameterObject_IsLockdateEnd','');
	},
	/**
	 * 初始化监听
	 * @private
	 */
	initListeners:function(){
		var me = this;
		var toolbar = me.getDockedComponent('toolbar-top');
		var search = toolbar.getComponent('search');
		search.enable();
		//初始化查询页面的监听
		me.initSearchListeners();
	},
	/**
	 * 初始化查询页面的监听
	 * @private
	 */
	initSearchListeners:function(){
		var me = this;
		var searchWin = me.searchWin;
		//查询按钮
		var searchBut = searchWin.getComponent('searchBut');
		searchBut.on({
			click:function(but){
				me.search();
			}
		});
		//关闭按钮
		var closeBut = searchWin.getComponent('closeBut');
		closeBut.on({
			click:function(but){
				me.hideSearchWin();
			}
		});
		//回车键=查询按钮
		new Ext.KeyMap(searchWin.getEl(),[{
	      	key:Ext.EventObject.ENTER,
	      	fn:function(){me.search();}
     	}]);
	},
	/**
	 * 检索处理
	 * @private
	 */
	search:function(){
		var me = this;
		var bo = me.validation();
		if(bo){
			var entity = me.getEntity();
			me.load({entity:entity});
		}
	},
	/**
	 * 数据校验
	 * @private
	 * @return {Boolean}
	 */
	validation:function(){
		var me = this;
		var searchWin = me.searchWin;
		//送检日期
		var startCom = searchWin.getComponent('SearchParameterObject_OperdateStart');
		var endCom = searchWin.getComponent('SearchParameterObject_OperdateEnd');
		
		var user = searchWin.getComponent('SearchParameterObject_CLIENTELE_Id').getValue();
		if(user == null || user == ""){
			Ext.Msg.alert('提示','必须选择客户！');
			return false;
		}
		
		var start = startCom.getValue();
		var end = endCom.getValue();
		
		if(start == "" || end == ""){
			Ext.Msg.alert('提示','送检开始和结束日期不能为空！');
			return false;
		}
		
		var startTime = new Date(start);
		var endTime = new Date(end);
		
		var num = endTime.getTime() - startTime.getTime();
		if(num < 0){
			Ext.Msg.alert('提示','送检结束日期不能小于开始日期！');
			return false;
		}
		return true;
	},
	/**
	 * 获取数据对象
	 * @private
	 */
	getEntity:function(){
		var me = this;
		var searchWin = me.searchWin;
        
        var values = searchWin.getForm().getValues();
        
        //送检日期
        var OperdateStart = values['SearchParameterObject_OperdateStart'];
		var OperdateEnd = values['SearchParameterObject_OperdateEnd'];
		//财务审核
		var IsLockdateStart = values['SearchParameterObject_IsLockdateStart'];
		var IsLockdateEnd = values['SearchParameterObject_IsLockdateEnd'];
        
        if(OperdateStart && OperdateStart != ''){
        	var d = new Date(OperdateStart);
        	values['SearchParameterObject_OperdateStart'] = "\/Date(" + d.getTime() + "+0800)\/";
        }
		if(OperdateEnd && OperdateEnd != ''){
        	var d = new Date(OperdateEnd);
        	values['SearchParameterObject_OperdateEnd'] = "\/Date(" + d.getTime() + "+0800)\/";
        }
        if(IsLockdateStart && IsLockdateStart != ''){
        	var d = new Date(IsLockdateStart);
        	values['SearchParameterObject_IsLockdateStart'] = "\/Date(" + d.getTime() + "+0800)\/";
        }
        if(IsLockdateEnd && IsLockdateEnd != ''){
        	var d = new Date(IsLockdateEnd);
        	values['SearchParameterObject_IsLockdateEnd'] = "\/Date(" + d.getTime() + "+0800)\/";
        }
		//util文件里面的公共方法
       	var obj = getObjByValue(values);
        
        return obj;
	},
	/**
	 * 加载数据
	 * @private
	 * @param {} obj
	 */
	load:function(obj){
		var me = this;
		var url = me.getInfoUrl;
		var callback = function(data){
			if(data.success){
				me.store.loadData(data.data.list);
				//总条数数值赋值
			    me.setCount(data.data.list.length);
			    //数据的日期信息
			    me.setInfo();
			    //统计数据
			    me.insertSum();
			    //改变列头
				me.changeCloumns();
				//隐藏查询界面
				me.hideSearchWin();
			}else{
				Ext.Msg.alert('提示','错误信息【<b style="color:red">' + data.ErrorInfo +'</b>】');
			}
		};
		//util文件里面的公共方法
		getDataFromServer(url,obj,callback);
	},
	/**
	 * 改变列头
	 * @private
	 */
	changeCloumns:function(){
		var me = this;
		var searchWin = me.searchWin;
        var values = searchWin.getForm().getValues();
        //送检日期
        var OperdateStart = values['SearchParameterObject_OperdateStart'];
		
        var arr = OperdateStart.split('-');
        
        var str = arr[0] + "年" + arr[1] + "月份";
        
		var columns = me.columns;
		columns[2].setText(str + "折前金额");
		columns[3].setText(str + "折后金额");
	},
	/**
	 * 总条数赋值
	 * @private
	 * @param {} count
	 */
	setCount:function(count){
		var me = this;
		var com = me.getComponent('toolbar-bottom').getComponent('count');
		var str = "总共 <b>" + count + "</b> 条数据";
		com.setText(str,false);
		//控制导出和打印按钮是否可以使用
		var toolbar = me.getDockedComponent('toolbar-top');
		var expor = toolbar.getComponent('export');
		var print = toolbar.getComponent('print');
		if(count > 0){
			expor.enable();
			print.enable();
		}else{
			expor.disable();
			print.disable();
		}
	},
	/**
	 * 数据的日期信息
	 * @private
	 */
	setInfo:function(){
		var me = this;
		var com = me.getComponent('toolbar-bottom').getComponent('info');
		
		var searchWin = me.searchWin;
        var values = searchWin.getForm().getValues();
        //送检日期
        var OperdateStart = values['SearchParameterObject_OperdateStart'];
		var OperdateEnd = values['SearchParameterObject_OperdateEnd'];
		
		var str = "<b style='color:black'>送检日期：&nbsp;" + OperdateStart + "&nbsp;至&nbsp;" + OperdateEnd + "</b>";
		com.setText(str,false);
	},
	/**
	 * 统计数据
	 * @private
	 */
	insertSum:function(){
		var me = this;
		var n = me.getStore().getCount();// 获得总行数
		if(n == 0){
			return;
		}
		
		var ItemCount=0,ItemPrice=0,ItemAgioPrice=0;
		var items = me.store.data.items;
		
		for(var i in items){
			var item = items[i];
			ItemCount += parseFloat(item.get('SearchResultObject_ItemCount'));
			ItemPrice += parseFloat(item.get('SearchResultObject_ItemPrice'));
			ItemAgioPrice += parseFloat(item.get('SearchResultObject_ItemAgioPrice'));
		}
		
	    var p = ('Ext.data.Model',{
	    	SearchResultObject_Dept:'<b>总计:</b>',
	        SearchResultObject_ItemCount:'<b>'+ItemCount+'</b>',
	        SearchResultObject_ItemPrice:'<b>'+ItemPrice.toFixed(2)+'</b>',
	        SearchResultObject_ItemAgioPrice:'<b>'+ItemAgioPrice.toFixed(2)+'</b>'
	    });
		me.store.insert(n,p);
	},
	/**
	 * 根据类型下载文件
	 * @private
	 * @param {} type
	 */
	downLoadFile:function(type){
		var me = this;
		me.handleFile(type,0);
	},
	/**
	 * 打印
	 * @private
	 */
	print:function(){
		var me = this;
		me.handleFile('pdf',1);
	},
	/**
	 * 文件处理（打印或者下载）
	 * @private
	 * @param {} fileType [文件类型]
	 * @param {} type [0:下载；1:打印]
	 */
	handleFile:function(fileType,type){
		var me = this;
		//下载文件
		var entity = me.getEntity();
		var obj = {entity:entity,fileType:fileType};
		var callback = function(data){
			if(data.success){
				var url = me.downloadUrl + "?type=" + type + "&id=" + data.data.url;
				if(type == 0){//下载
					url = url + "&name=" + me.getFileName();
					window.location.href = url;
					//window.open(url);
				}else if(type == 1){//打印
					//var winname = "";//me.getFileName() + "-内容打印";
					//var par = "toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no";
					//window.open(url,winname,par);
					window.open(url);
				}
			}else{
				Ext.Msg.alert('提示','错误信息【<b style="color:red">' + data.ErrorInfo +'</b>】');
			}
		};
		getDataFromServer(me.getFileInfoUrl,obj,callback);
	},
	/**
	 * 获取文件名称
	 * @private
	 * @return {}
	 */
	getFileName:function(){
		var me = this;
		var com = me.getComponent('toolbar-bottom').getComponent('info');
		
		var searchWin = me.searchWin;
        var values = searchWin.getForm().getValues();
        //送检日期
        var OperdateStart = values['SearchParameterObject_OperdateStart'];
		var OperdateEnd = values['SearchParameterObject_OperdateEnd'];
		
		var name = me.title + "[" + OperdateStart + "至" + OperdateEnd + "]";

		return name;
	}
});