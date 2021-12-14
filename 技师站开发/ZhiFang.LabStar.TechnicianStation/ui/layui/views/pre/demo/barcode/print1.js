/**
   @Name：模式1-条码打印
   @Author：liangyl
   @version 2019-04-10
 */
layui.extend({
	uxutil: 'ux/util',
	uxtable:'views/pre/demo/modules/uxtable',
	basic: 'views/pre/demo/barcode/basic'
}).use(['table','basic','uxutil','uxtable','element','layer'],function(){
	var $ = layui.$,
	    basic =layui.basic,
	    form = layui.form,
	    uxutil=layui.uxutil,
	    uxtable=layui.uxtable,
	    element=layui.element,
	    layer = layui.layer,
		table = layui.table;
	
	//获取病人列表 服务
	var GET_PATIENT_LIST_URL =  uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/Patient.js';
	//获取条码列表服务
	var GET_BARCODE_URL =  uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/mode1.js';
    //获取项目列表服务
	var GET_ITEM_URL =  uxutil.path.ROOT + '/ui/layui/views/pre/demo/data/json/item.js';
    
    ///**未确认页签-----------------------------*/
    var UnConfirm = {
	    //病人列表
    	PatientTable:{
		    myTable:'',//病人列表
			height:$(document).height() - $('#LAY-unconfirm-patient').offset().top-59,
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-unconfirm-patient',
					height: this.height,
					toolbar: '#unToolbar',
			        defaultToolbar: ['filter', 'exports'],
					title: '病人列表',
					limit: 1000,
					parseData:uxtable.parseData,
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
			            var that = this.elem.next();
			            for(var i=0;i<res.data.length;i++){
			            	if(res.data[i].RgbCode){
			                    that.find(".layui-table-box tbody tr[data-index='" + i + "']").find('td:eq(10)').css("background-color", res.data[i].RgbCode);
			            	}
			            }
					},
					cols: [[
					   {type: 'checkbox', fixed: 'left'},
					   {type: 'numbers',title: '行号',fixed: 'left'},
					   {field: '床号',title: '床号',width: 100,sort: true},
					   {field: '姓名',title: '姓名',width: 120,sort: true}, 
					   {field: '病历号',width: 100,title: '病历号',sort: true}, 
					   {field: '性别',width: 70,title: '性别',sort: true},
					   {field: '年龄',width: 70,title: '年龄',sort: true},
					   {field: '科室',width: 150,title: '科室',sort: true},
					   {fixed: 'right', title:'操作', toolbar: '#unBarTool', width:330}
				    ]],
					loading:true,
					text: {none: '暂无相关数据'}
				});
				//行具栏事件
				table.on('tool(LAY-unconfirm-patient)', function(obj){
					switch(obj.event){
						case 'editsampletype': //修改样本类型
						    basic.OpenWin.onEditSampleType(obj.data);
						break;
						case 'edit': //信息补录
						    basic.OpenWin.onEditSupplement(obj.data);
						break;
						case 'del' : //删除条码
							layer.confirm('真的删除条码吗？', function(index){
						        obj.del();
						        layer.close(index);
						    });
						break;
						case 'delete': //作废条码
						    layer.confirm('真的删除条码吗？', function(index){
						        obj.del();
						        layer.close(index);
						    });
						break;
					};
				});	
				//头工具栏事件
			    table.on('toolbar(LAY-unconfirm-patient)', function(obj){
				    switch(obj.event){
				      case 'acceptance'://核收
				      
				      break;
				      case 'refresh'://刷新
						    var where ={limit:10000,page:1};
							//病人列表加载	
					        UnConfirm.PatientTable.onSearch(GET_PATIENT_LIST_URL,where); 
						    //条码列表加载	
//						    UnConfirm.BarCodeTable.onSearch(GET_BARCODE_URL,where); 
//					        //项目列表加载	
//					        UnConfirm.ItemTable.onSearch(GET_ITEM_URL,where);
					  break;
				      case 'confirm'://确认
				      
				      break;
				      case 'printBarcode'://打印条码
				      
				      break;
				      case 'printList'://打印清单
				      
				      break;
				      case 'originalAdvice'://原始医嘱
				          basic.OpenWin.onOriginalAdvice($,obj.data);
				      break;
				    };
				});
		    },
    	    //数据加载
    	    onSearch : function(url,where){
    	    	uxtable.onSearch(UnConfirm.PatientTable.myTable,url,where);
		    },
		    // 清空信息
		    clearData : function(){
		     	uxtable.clearData(UnConfirm.PatientTable.myTable);
		    }
    	},
    	//条码列表
    	BarCodeTable:{
    		myTable:'',//条码列表
		    height:$(document).height() - $('#LAY-unconfirm-barcode').offset().top-59,
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-unconfirm-barcode',
					height: this.height,
					title: '条码列表',
					toolbar:true,
					defaultToolbar: ['filter', 'exports'],
					limit: 1000,
					parseData:uxtable.parseData,
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
					},
					cols: [[
						{type: 'numbers',title: '行号',fixed: 'left'},
					    {field: '急查',title: '急查',width: 65,sort: true},
					    {field: '条码号',title: '条码号',width: 100,sort: true},
					    {field: '样本类型',width: 120,title: '样本类型',sort: true},
						{field: '采样管',width: 100,title: '采样管',sort: true},
					    {field: '送检目的',width: 150,title: '送检目的地',sort: true},
					    {field: '医嘱项目',width: 200,title: '医嘱项目',sort: true},
					    {field: '医生',width: 100,title: '医生',sort: true},
					    {field: '备注',width: 150,title: '备注',sort: true}
					]],
					loading:true,
					text: {none: '暂无相关数据'}
				});
		    },
    	    //数据加载
    	    onSearch : function(url,where){
    	    	uxtable.onSearch(UnConfirm.BarCodeTable.myTable,url,where);
		    },
		    // 清空信息
		    clearData : function(){
		     	uxtable.clearData(UnConfirm.BarCodeTable.myTable);
		    }

        },
        //项目列表
        ItemTable : {
        	myTable:'',//项目列表
		    height:$(document).height() - $('#LAY-unconfirm-item').offset().top-59,
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-unconfirm-item',
					height: this.height,
					toolbar:true,
		            defaultToolbar: ['filter', 'exports'],
					title: '项目列表',
					limit: 1000,
					parseData:uxtable.parseData,
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
					},
					cols: [[{
						type: 'numbers',title: '行号',fixed: 'left'
					},{
						field: '名称',title: '名称',width: 150,sort: true
					}, {
						field: '编号',title: '项目编号',width: 150,sort: true
					}]],
					loading:true,
					text: {none: '暂无相关数据'}
				});
				
		    },
    	    
    	    //数据加载
    	    onSearch : function(url,where){
    	    	uxtable.onSearch(UnConfirm.ItemTable.myTable,url,where);
		   },
		   // 清空病人信息
		   clearData : function(){
		     	uxtable.clearData(UnConfirm.ItemTable.myTable);
		   }
        }
    };
    
    ///**已确认页签-------------------------------*/
	var Confirm = {
    	//病人列表
    	PatientTable:{
		    myTable:'',//病人列表
			height:$(document).height() - $('#LAY-confirm-patient').offset().top-217,
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-confirm-patient',
					height: this.height,
					toolbar:'#toolbar' ,
			        defaultToolbar: ['filter', 'exports'],
					title: '病人列表',
					limit: 1000,
					parseData:uxtable.parseData,
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
			            var that = this.elem.next();
			            for(var i=0;i<res.data.length;i++){
			            	if(res.data[i].RgbCode){
			                    that.find(".layui-table-box tbody tr[data-index='" + i + "']").find('td:eq(10)').css("background-color", res.data[i].RgbCode);
			            	}
			            }
					},
					cols: [[{type: 'checkbox', fixed: 'left'},
					   {type: 'numbers',title: '行号',fixed: 'left'},
					   {field: '床号',title: '床号',width: 100,sort: true},
					   {field: '姓名',title: '姓名',width: 120,sort: true}, 
					   {field: '病历号',width: 100,title: '病历号',sort: true}, 
					   {field: '性别',width: 70,title: '性别',sort: true},
					   {field: '年龄',width: 70,title: '年龄',sort: true},
					   {field: '科室',width: 150,title: '科室',sort: true},
					   {fixed: 'right', title:'操作', toolbar: '#barTool', width:235}
			    	]],
					loading:true,
					text: {none: '暂无相关数据'}
				});
				//监听行工具事件
			    table.on('tool(LAY-confirm-patient)', function(obj){
					switch(obj.event){
						case 'revoke': //撤销确认
						    basic.OpenWin.onRevoke(obj.data);
						break;
					};
				});	
				//头工具栏事件
			    table.on('toolbar(LAY-confirm-patient)', function(obj){
				    switch(obj.event){
				      case 'originalAdvice'://原始医嘱
				          basic.OpenWin.onOriginalAdvice($,obj.data);
				      break;
				    };
				});
		    },
    	    //数据加载
    	    onSearch : function(url,where){
    	    	uxtable.onSearch(Confirm.PatientTable.myTable,url,where);
		    },
		    // 清空信息
		    clearData : function(){
		     	uxtable.clearData(Confirm.PatientTable.myTable);
		    }
    	},
    	//条码列表
    	BarCodeTable:{
    		myTable:'',//条码列表
		    height:$(document).height() - $('#LAY-confirm-barcode').offset().top-217,
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-confirm-barcode',
					height: this.height,
					title: '条码列表',
					toolbar:true,
					defaultToolbar: ['filter', 'exports'],
					limit: 1000,
					parseData:uxtable.parseData,
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
					},
					cols: [[ {type: 'numbers',title: '行号',fixed: 'left'},
					    {field: '急查',title: '急查',width: 65,sort: true},
					    {field: '条码号',title: '条码号',width: 100,sort: true},
					    {field: '样本类型',width: 120,title: '样本类型',sort: true},
						{field: '采样管',width: 100,title: '采样管',sort: true},
					    {field: '送检目的',width: 150,title: '送检目的地',sort: true},
					    {field: '医嘱项目',width: 200,title: '医嘱项目',sort: true},
					    {field: '医生',width: 100,title: '医生',sort: true},
					    {field: '备注',width: 150,title: '备注',sort: true}
					]],
					loading:true,
					text: {none: '暂无相关数据'}
				});
		    },
    	    //数据加载
    	    onSearch : function(url,where){
				uxtable.onSearch(Confirm.BarCodeTable.myTable,url,where);
		    },
		   // 清空病人信息
		   clearData : function(){
		     	uxtable.clearData(Confirm.BarCodeTable.myTable);
		   }
        },
        //项目列表
        ItemTable : {
        	myTable:'',//项目列表
		    height:$(document).height() - $('#LAY-confirm-item').offset().top-217,
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-confirm-item',
					height: this.height,
					toolbar:true,
		            defaultToolbar: ['filter', 'exports'],
					title: '项目列表',
					limit: 1000,
					parseData:uxtable.parseData,
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
					},
					cols: [[{
						type: 'numbers',title: '行号',fixed: 'left'
					},{
						field: '名称',title: '名称',width: 150,sort: true
					}, {
						field: '编号',title: '项目编号',width: 150,sort: true
					}]],
					loading:true,
					text: {none: '暂无相关数据'}
				});
				
		    },
    	    //数据加载
    	    onSearch : function(url,where){
				uxtable.onSearch(Confirm.ItemTable.myTable,url,where);
		    },
		    // 清空信息
		    clearData : function(){
		     	uxtable.clearData(Confirm.ItemTable.myTable);
		    }
        }
    };
    
    //初始化
	iniInfo();
    //数据加载
    onSearch();
    
    function iniInfo (){
    	basic.SearchBar.iniDate();
    	
		basic.SearchBar.showSelect();
		
		basic.OpenWin.isShowBtn();
		
		/**未确认-----------------------------*/
	    //病人列表创建	
	    UnConfirm.PatientTable.cretateTable();
	    //条码列表创建
	    UnConfirm.BarCodeTable.cretateTable();
	     //项目列表创建
	    UnConfirm.ItemTable.cretateTable();
	   
	    /**已确认-----------------------------*/
	   
	    //病人列表创建	
	    Confirm.PatientTable.cretateTable();
	    //条码列表创建
	    Confirm.BarCodeTable.cretateTable();
	     //项目列表创建
	    Confirm.ItemTable.cretateTable();
	}
     
	function onSearch (){
		//未确认
		var where ={limit:10000,page:1};
		//病人列表加载	
        UnConfirm.PatientTable.onSearch(GET_PATIENT_LIST_URL,where); 
	    //条码列表加载	
	    UnConfirm.BarCodeTable.onSearch(GET_BARCODE_URL,where); 
        //项目列表加载	
        UnConfirm.ItemTable.onSearch(GET_ITEM_URL,where); 
    
	    var isConfirmLoad=false;//页签切换只加载一次
	    element.on('tab(print-tabs)', function(obj){
	    	//切换页签加载（已确认数据加载）
	    	if(!isConfirmLoad && obj.index!=0){
			    //病人列表加载	
			    Confirm.PatientTable.onSearch(GET_PATIENT_LIST_URL,where); 
			    //条码列表加载	
			    Confirm.BarCodeTable.onSearch(GET_BARCODE_URL,where); 
			    //项目列表加载	
			    Confirm.ItemTable.onSearch(GET_ITEM_URL,where); 
				isConfirmLoad=true;
	    	}
		});
	}
	//未确认----监听行工具事件
	$('#btnSet').on('click',function(){
		basic.OpenWin.onShowModePrint();
	});
});