/**
 * @name: 样本送达
 * @author: liangyl
 * @version: 
 */
layui.define(['uxutil','form','table','uxtable'], function (exports) {
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil=layui.uxutil,
		uxtable=layui.uxtable,
		table = layui.table;
	
	//外部接口
	var basic = {
		//查询工具栏
		SearchBar : {
			   //扫码服务调用
		    onBarCode : function(url,where,barCode,callback){
		   	   	var selecturl =  url;
//		   	   	var indexs=layer.load(2);
		   	    uxutil.server.ajax({
					url:selecturl,
					async:false
				},function(data){
//					layer.close(indexs);
					if(data){
						callback(data.data[0]);
					}else{
						layer.msg(data.msg);
					}
				});
		    },
			//条码号扫码
		    onScanBarCode : function(url,where,barCode,Mode){
		    	var myTable = basic.ServiceTable.myTable;
	        	if(barCode.val()){
	        		layer.closeAll();
	        		this.onBarCode(url,where,barCode,function(obj){
						var myTable =basic.ServiceTable.myTable;
				        var config = myTable.config;
				        var dataTemp = table.cache["LAY-sample-service"];
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
		//样本送达列表
		ServiceTable:{
		    myTable:'',//样本送达列表
		    height:$(document).height() - $('#LAY-sample-service').offset().top - 114,
		    //创建列表
	        cretateTable : function (Mode){
	        	this.myTable = table.render({
			        elem: '#LAY-sample-service',
			        height:this.height,
			        toolbar: '#toolbarDemo',
			        defaultToolbar: ['filter', 'exports'],
                    page: false,
                    limit:10000,
			        cols: [[
//			            {type: 'checkbox', fixed: 'left'},
			            {type: 'numbers',title: '行号',fixed: 'left'},
			            {field: '急查',width: 65,title: '急查',sort: true,filter: true},
			            {field: '床号',title: '床号',width: 80,sort: true,filter: true}, 
			            {field: '姓名',title: '姓名',width: 100,sort: true,filter: true}, 
			            {field: '病历号',width: 100,title: '病历号',sort: true,filter: true},
			            {field: '性别',width: 65,title: '性别',sort: true,filter: true},
			            {field: '条码号',width: 100,title: '条码号',sort: true,filter: true},
			            {field: '样本类型',title: '样本类型',width: 130,sort: true,filter: true},
			            {field: '采样管',title: '采样管',width: 100,sort: true,filter: true},
			            {field: '医嘱项目',width: 200,title: '医嘱项目',sort: true,filter: true} ,
			            {field: '年龄描述',width: 155,title: '年龄',sort: true,filter: true},
			            {field: '科室',title: '科室',width: 150,sort: true,filter: true}, 
			            {field: '医生',width: 100,title: '医生',sort: true,filter: true} ,
			            {field: '采样时间',width: 155,title: '采样时间',sort: true,filter: true},
			            {field: '送检时间',width: 155,title: '送检时间',sort: true,filter: true},
			            {field: '备注',width: 155,title: '备注',sort: true,filter: true}, 
			        ]],
			        done: function (res, curr, count) {
			            if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
			            //默认选择行联动
			            var value="";
			            if(res.data.length>0){
						    value=res.data[0].条码号;
						}
			            basic.showBarcode(Mode,value);
			            $("#txtNum").val(res.data.length);
			        },
			        text: {none: '暂无相关数据'}
			    });
				//头部工具栏事件
			    table.on('toolbar(LAY-sample-service)', function(obj){
				    var checkStatus = table.checkStatus(obj.config.id);
				    switch(obj.event){
					    case 'clearData':
				        basic.ServiceTable.myTable.reload({
			                data: []
			            });
					    break;
				     
				    };
				});
				//行单击事件
				table.on('row(LAY-sample-service)', function(obj) {
					var value=obj.data.条码号;
					basic.showBarcode(Mode,value);
				});
		    },
		    //样本送达列表数据加载
		    onSearch : function(url,where){
	           	uxtable.onSearch(basic.ServiceTable.myTable,url,where);
		    }
		},
		//行单击显示条码内容,Mode当前模式
		showBarcode : function(Mode,value){
			if(Mode=='2')value='送达成功:'+value;
			$('#txtBarcode2').val(value);
		}
	};
	
//  $(document).on("click", "td div.laytable-cell-checkbox div.layui-form-checkbox", function (e) {
//	    e.stopPropagation();
//	});	
 //单击行勾选checkbox事件
//	$(document).on("click",".layui-table-body table.layui-table tbody tr", function () {
//	    var index = $(this).attr('data-index');
//	    var tableBox = $(this).parents('.layui-table-box');
//	    //存在固定列
//	    if (tableBox.find(".layui-table-fixed.layui-table-fixed-l").length>0) {
//	        tableDiv = tableBox.find(".layui-table-fixed.layui-table-fixed-l");
//	    } else {
//	        tableDiv = tableBox.find(".layui-table-body.layui-table-main");
//	    }
//	    var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
//	    if (checkCell.length>0) {
//	        checkCell.click();
//	    }
//	});
	//暴露接口
	exports('basic',basic);
});