/**
 * @name: 样本签收
 * @author: liangyl
 * @version: 
 */
layui.define(['uxutil','table','form','layer','element','laydate','uxtable'], function (exports) {
	"use strict";
		
	var $ = layui.$,
	    form = layui.form,
	    laytpl=layui.laytpl,
	    uxutil= layui.uxutil,
	    element = layui.element,
	    laydate = layui.laydate,
	    layer= layui.layer,
	    uxtable = layui.uxtable,
		table = layui.table;
    form.render();
//	element.render('collapse');
	//外部接口
	var basic = {
		//查询条件
		SearchBar : {
			iniDate:function(){
		    	//日期时间选择器
			    laydate.render({
			       elem: '#start_time',
			       type: 'date'
			    });
			    laydate.render({
			       elem: '#end_time',
			       type: 'date'
			    });
		    },
		     //扫码服务调用
		    onBarCode : function(url,where,barCode,callback){
		   	   	var selecturl =  url;
		   	   	var indexs=layer.load(2);
		   	    uxutil.server.ajax({
					url:selecturl,
					async:false
				},function(data){
					layer.close(indexs);
					if(data){
						callback(data.data[0]);
					}else{
						layer.msg(data.msg);
					}
				});
		    },
			//条码号扫码
		    onScanBarCode : function(url,where,barCode){
	        	if(barCode.val()){
	        		layer.closeAll();
	        		this.onBarCode(url,where,barCode,function(obj){
						var myTable =basic.DrAdviceTable.myTable;
				        var config = myTable.config;
				        var dataTemp = table.cache["LAY-sample-signfor"];
				        dataTemp.push(obj);
				        myTable = table.reload(config.id, $.extend(true, {
				            // 更新数据
				            data: dataTemp,
				        }, config.page ? {
				            // 一般新增都是加到最后，所以始终打开最后一页
				            page: {
				                curr: Math.ceil(dataTemp.length / config.page.limit)
				            }
				        } : {}));
				        barCode.val('');
				        layui.event("barCode", "click", obj);
					});
	        	}else{
	        		layer.msg('条码号不能为空,请扫码',{icon:2,time:2000});
	        	}
	            return false;
		    }
		},
		//弹出窗体实现功能
		OpenWin:{
			//签收人身份验证
		    onIdentityCheck :function (obj){
				layer.open({  
			       type: 2,  
			       title: '确认用户身份',  
			       maxmin: false,  
			       area: ['380px', '250px'],  
			       resize:false,//禁止拖拉框的大小
			       content:'identityform.html',
			       btn:["保存","取消"],
		           btnAlign:'c',
			       success: function(layero, index){
			            var revoke = layero.find('iframe').contents().find('#LAY-identity-check-form');    
			           
			        }
			    }); 
			}
		},
		//其他方法
		OtherMethod : {
			//选择列表行联动项目和样本信息面板
		    linkSampleAndItem:function(obj){
		    	var getTpl = $('#sampleinfo').html();
				var obj2 = {
		        	data:[obj]
		        };
				laytpl(getTpl).render(obj2, function(html){
				  $('#LAY-sampleinfo-view').html(html);
				});
				var ZDY1 = $('#txtZDY1');
                ZDY1.val(obj.医嘱项目);
           },
           //清空项目和样本信息面板
           clearSampleAndItem:function(){
           	    var getTpl = $('#sampleinfo').html();
		        var obj2 = {
		        	data:[]
		        };
				laytpl(getTpl).render(obj2, function(html){
				  $('#LAY-sampleinfo-view').html(html);
				});
	            var ZDY1 = $('#txtZDY1');
	            ZDY1.val('');  
           }
		},
		//打包机列表
        BalerTable:{
		    myTable:'',//打包机列表
		    //创建列表
	        cretateTable : function (){
	        	this.myTable  = table.render({
					elem: '#LAY-sample-signfor-info',
					toolbar:'#toolBarDate',
					defaultToolbar: ['filter'],
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
						if(res.data.length>0){
						   $(".layui-table-view[lay-id='LAY-sample-signfor-info'] .layui-table-body tr[data-index=0] .layui-form-checkbox").click();
						}
					},
					cols: [[
					   {type: 'checkbox', fixed: 'left'},
					   {type: 'numbers',title: '行号',fixed: 'left'},
					   {field: '打包号',title: '打包号',width: 100,sort: true},
					   {field: '总数',title: '总数',width: 80,sort: true},
					   {field: '已签收',width: 80,title: '已签收',sort: true},
					   {field: '已拒收',width: 80,title: '已拒收',sort: true}
					  ]
					],
					text: {none: '暂无相关数据'},
					page: false,
					limit: 1000,
					height: 'full-110'
				});
		    },
		    //打包机列表数据加载
		    onSearch : function(url,where){
		    	uxtable.onSearch(basic.BalerTable.myTable,url,where);
		    }
		},
		//医嘱信息表
		DrAdviceTable:{
		    myTable:'',//医嘱信息表
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-sample-signfor',
					toolbar:'#toolbarDemo',
					defaultToolbar: ['filter', 'exports'],
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
						if(res.data.length>0){
							basic.OtherMethod.linkSampleAndItem(res.data[0]);
						}
					},
					cols: [[
					   {type: 'checkbox', fixed: 'left'},
					   {type: 'numbers',title: '行号',fixed: 'left'},
					   {field: '急查',title: '急查',width: 65,sort: true},
					   {field: '病历号',title: '病历号',width: 100,sort: true},
					   {field: '姓名',title: '姓名',width: 100,sort: true},
					   {field: '性别',width: 100,	title: '性别',sort: true},
					   {field: '年龄描述',width: 120,title: '年龄',sort: true},
					   {field: '床位',width: 100,title: '床位',sort: true},
					   {field: '条码号',width: 150,title: '条码号',sort: true}, 
					   {field: '科室',width: 150,title: '科室',sort: true}, 
					   {field: '医嘱项目',width: 200,title: '医嘱项目',sort: true} ,
					   {field: '确认时间',width: 155,title: '确认时间',sort: true}, 
					   {field: '采样时间',width: 155,title: '采样时间',sort: true},
					   {field: '采样人',width: 100,title: '采样人',sort: true} ]
					],
					text: {none: '暂无相关数据'},
					page: false,
					limit: 1000,
					height: 'full-170'
				});
				
				//头部工具栏事件
			    table.on('toolbar(LAY-sample-signfor)', function(obj){
				    var checkStatus = table.checkStatus(obj.config.id);
				    switch(obj.event){
					    case 'clearData'://清空
				        basic.DrAdviceTable.clearData();
				        basic.OtherMethod.clearSampleAndItem();
			            basic.RecordTable.clearData();
			            basic.FailTable.clearData();
					    break;
				     
				    };
				});
		    },
		    //医嘱列表数据加载
		    onSearch : function(url,where){
		    	uxtable.onSearch(basic.DrAdviceTable.myTable,url,where);
		    },
		    //清空列表数据
			clearData : function(){
				basic.DrAdviceTable.myTable.reload({
	                data: []
	            });
			}
		},
        //签收失败列表
        FailTable :{
        	myTable:'',//打包机列表
		    //创建列表
	        cretateTable : function (){
	        	this.myTable  = table.render({
					elem: '#LAY-sample-signfor-fail-record',
					toolbar:'#toolbarFailDemo',
					defaultToolbar: ['filter', 'exports'],
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
					},
					cols: [[
					   {type: 'numbers',title: '行号',fixed: 'left'},
					   {field: '条码号',title: '条码号',width: 100,sort: true},
					   {field: '失败原因',title: '失败原因',minWidth: 100,flex:1,sort: true} ]
					],
					text: {none: '暂无相关数据'},
					page: false,
					limit: 1000,
				    height: $(document).height() - $('#LAY-sample-signfor-fail-record').offset().top-465
				});
				//头部工具栏事件
			    table.on('toolbar(LAY-sample-signfor-fail-record)', function(obj){
				    var checkStatus = table.checkStatus(obj.config.id);
				    switch(obj.event){
					    case 'clearData2'://清空
				        basic.FailTable.myTable.reload({
			                data: []
			            });
					    break;
				     
				    };
				});
			},
			//签收失败列表数据加载
		    onSearch : function(url,where){
		    	uxtable.onSearch(basic.FailTable.myTable,url,where);
		    },
		    //清空列表
		    clearData:function(){
		    	var dataTemp = table.cache["LAY-sample-signfor-fail-record"];
			    if(dataTemp.length>0){
			    	basic.FailTable.myTable.reload({
		                data: []
		            });
			    }
		    }
        },
        //操作记录列表
        RecordTable :{
        	myTable:'',//打包机列表
		    //创建列表
	        cretateTable : function (){
	        	this.myTable  = table.render({
					elem: '#LAY-sample-signfor-record',
					done: function(res, curr, count) {
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
					},
					cols: [[
					   {type: 'numbers',title: '行号',fixed: 'left'},
					   {field: '签收者',title: '签收者',width: 80,sort: true},
					   {field: '签收时间',title: '签收时间',minWidth: 100,flex:1,sort: true} ]
					],
					text: {none: '暂无相关数据'},
					page: false,
					limit: 1000,
				    height: $(document).height() - $('#LAY-sample-signfor-record').offset().top-245
				});
	        },
	        //操作记录列表数据加载
		    onSearch : function(url,where){
		    	uxtable.onSearch(basic.RecordTable.myTable,url,where);
		    },
		    //清空列表
		    clearData:function(){
			    var dataTemp = table.cache["LAY-sample-signfor-record"];
			    if(dataTemp.length>0){
			    	basic.RecordTable.myTable.reload({
		                data: []
		            });
			    }
		    }
		}
	};
	//创建医嘱信息列表
	basic.DrAdviceTable.cretateTable();
	//创建签收失败列表
	basic.FailTable.cretateTable();
	//创建操作记录列表
	basic.RecordTable.cretateTable();
	//暴露接口
	exports('basic',basic);
});