/**
 * @name: 样本采集
 * @author: liangyl
 * @version: 
 */
layui.define(['uxutil','table','form','laydate','uxtable','uxcombobox','uxform'], function (exports) {
	"use strict";
	
	var $ = layui.$,
	    form = layui.form,
	    laydate = layui.laydate,
	    uxutil = layui.uxutil,
	    uxform = layui.uxform,
	    uxtable=layui.uxtable,
	    uxcombobox = layui.uxcombobox,
		table = layui.table;
	//外部接口
	var basic = {
		//下拉组件数据加载
		dropdownbox:{
			//采样人
			samplePerson : function (selecturl){
				var url = selecturl;
				//请求登入接口
				uxutil.server.ajax({
					url:url
				},function(data){
					if(data){
						var value = uxcombobox.changeResult(data);
						var tempAjax = '';
		                for (var i = 0; i < value.list.length; i++) {
		                    tempAjax += "<option value='" + value.list[i].采样人ID + "'>" + value.list[i].采样人 + "</option>";
		                    $("#lay-sample-person").empty();
		                    $("#lay-sample-person").append(tempAjax);
		                }
		                form.render('select');//需要渲染一下;
					}else{
						layer.msg(data.msg);
					}
				});
			}
		},
		//查询栏条件
		searchtoolbar : {
			//查询工具栏收缩-展开
			isShow :function (){
				 //监听锁定操作
		        form.on('checkbox(test-table-lockDemo)', function(obj){
			    	if(obj.elem.checked){
			    		$("#lay-date-serach").show();
			            $("#lay-area-serach").show();
//			            basic.CollectionTable.myTable.height=$(document).height() - $('#LAY-collection-mode').offset().top-63;
//			            basic.UnCollectionTable.myTable.height=$(document).height() - $('#LAY-notcollection-mode').offset().top-63;
//			            table.render();
			    	}else{
			    		$("#lay-date-serach").hide();
			            $("#lay-area-serach").hide();
//			            basic.CollectionTable.myTable.height=$(document).height() - $('#LAY-collection-mode').offset().top-13;
//			            basic.UnCollectionTable.myTable.height=$(document).height() - $('#LAY-notcollection-mode').offset().top-13;
//			            table.render();
			    	}
			     });
			},
			iniDate:function(){
		    	//日期时间选择器
			    laydate.render({
			       elem: '#test-laydate-type-datetime-mode1',
			       type: 'datetime'
			    });
			    laydate.render({
			       elem: '#test-laydate-type-datetime-mode2',
			       type: 'datetime'
			    });
		   },
		   //扫码服务调用
		    onBarCode : function(url,where,barCode,callback){
//		   	   	var indexs=layer.load(2);
		   	    uxutil.server.ajax({
					url:url,
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
		    onScanBarCode : function(url,where,barCode){
	        	if(barCode.val()){
	        		layer.closeAll();
					this.onBarCode(url,where,barCode,function(obj){
						var myTable =basic.CollectionTable.myTable;
				        var config = myTable.config;
				        var dataTemp = table.cache["LAY-collection-mode"];
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
			//撤销采集
		    onRevoke : function (data){
				layer.open({  
			       type: 2,  
			       title: '撤销采集',  
			       maxmin: false,  
			       area: ['500px', '355px'],  
			       resize:false,//禁止拖拉框的大小
			       content:'revoke.html',
			       btn:["撤销确认","取消"],
		           btnAlign:'c',
		           yes: function(index, layero){
		           	   var iframeWindow = window['layui-layer-iframe'+ index] ,
				          submitID = 'LAY-revoke-form-submit',
				          submit = layero.find('iframe').contents().find('#'+ submitID);
			           var setform = iframeWindow.layui.form;
			          //监听提交
			          setform.on('submit('+ submitID +')', function(data){
			             var obj= data.field; //获取提交的字段
			             //调用服务保存
			             layer.msg('调用服务保存，未完成',{icon:2,time:2000});

			             //OpenWin.onRevokeUpdate(obj);
			             //提交 Ajax 成功后，静态更新表格中的数据
			             layer.close(index); //关闭弹层
			          });  
			          submit.trigger('click');
				   },
				   btn2: function(index, layero){
//				   	   return false;//开启该代码可禁止点击该按钮关闭
				   },
			       success: function(layero, index){
			       	   var iframeWindow = window['layui-layer-iframe'+ index];
			           var setform = iframeWindow.layui.form;
//			            setform.set(obj) =data;
//			           setform.config.obj = data;
//			           setform.render();
//			           setform.val("LAY-revoke-form", {
//						  BarCode: "贤心"
//						  ,BarCodeOrName: "女"
//						  ,ZDY2: "3"
//					   });
			 
			           var revoke = layero.find('iframe').contents().find('#LAY-revoke-form');    
			           revoke.find('#LAY-check-isShow').hide();
			            var str=data.条码号+'/'+data.姓名;
			           	revoke.find('#txtBarCode2').val(data.条码号);
			            revoke.find('#supplement').val('0');
			            revoke.find('#txtBarCode').val(str);
			            revoke.find('#ZDY2').val(data.医嘱项目);
//			            var id='1';
//			            revoke.find('#ID').val(id);
//


//			           var body = layer.getChildFrame('body',index);//建立父子联系
//			            var iframeWin = window[layero.find('iframe')[0]['name']];
//			            // console.log(arr); //得到iframe页的body内容
//			            // console.log(body.find('input'));
//			            var inputList = body.find('input');
//			            for(var j = 0; j< inputList.length; j++){
//			                $(inputList[j]).val(arr[j]);
//			            }
            
			        },
			        end:function () {
			            //结束之后请求表格数据的方法，新编辑的数据直接存库并展示在页面
			        }
			    }); 
			},
			//撤销采集保存服务
			onRevokeUpdate : function(obj,EDIT_URL){
		    	var indexs=layer.load(2);
		    	var params={
					Id: obj.field.HREmployee_Id,
//					Birthday: obj.field.HREmployee_Birthday ? JcallShell.Date.toServerDate(obj.field.HREmployee_Birthday) : null,
					IsUse: true
				};
		    	var entity = {
					entity: params,
					fields:'Id,IsUse'
				};
		    	uxutil.server.ajax({
					type:'post',
					url: EDIT_URL
//					data:JcallShell.JSON.encode(entity)
				}, function(data){
					layer.close(indexs);
					if(data.success == true){
						layer.msg('修改成功',{icon:1,time:2000});
					}else{
		                layer.msg(data.msg, {icon: 2});
					}
				});
		    }
		},
		//样本采集列表
		CollectionTable:{
		    myTable:'',//样本采集列表
		    height:$(document).height() - $('#LAY-collection-mode').offset().top-13,
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-collection-mode',
					height: this.height,
					toolbar: '#toolbarMode',
			        defaultToolbar: ['filter', 'exports'],
					title: '样本采集',
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
						$("#txtNum").val('数量:'+res.data.length);
						var params ={res:res, curr:curr, count:count};
						layui.event("collection", "done", params);
					},
					cols: [this.getCollectionCols()],
					loading:true,
					text: {none: '暂无相关数据'}
				});
				//工具栏事件
			    table.on('tool(LAY-collection-mode)', function(obj){
					switch(obj.event){
						case 'edit':basic.OpenWin.onRevoke(obj.data);break;
					};
				});
				//头部工具栏事件
			    table.on('toolbar(LAY-collection-mode)', function(obj){
				    var checkStatus = table.checkStatus(obj.config.id);
				    switch(obj.event){
					    case 'clearData':
					        uxtable.clearData(basic.CollectionTable.myTable);
					    break;
				    }
				});
		    },
		    //创建样本采集列表cols
		    getCollectionCols :function (colums){
		    	var colums=[{type: 'checkbox', fixed: 'left'},
				   {type: 'numbers',title: '行号',fixed: 'left'},
				   {field: '姓名',title: '姓名',width: 120,sort: true},
				   {field: '年龄描述',title: '年龄',width: 120,sort: true}, 
				   {field: '床号',width: 100,title: '床号',sort: true}, 
				   {field: '性别',width: 70,title: '性别',sort: true},
				   {field: '科室',width: 150,title: '科室',sort: true},
				   {field: '条码号',width: 100,title: '条码号',sort: true},
				   {field: '医嘱项目',minWidth: 250,flex:1,title: '医嘱项目',sort: true} ,
				   {field: '送检目的',width: 150,title: '送检目的地',sort: true},
				   {field: '采样管',width: 100,title: '采样管',sort: true},
				   {fixed: 'right', title:'操作', toolbar: '#barMode', width:100}];
				return colums;
		    },
		    //样本采集列表数据加载
		    onSearch : function(url,where){
				uxtable.onSearch(basic.CollectionTable.myTable,url,where);
		    }
		},
		//未采集列表
		UnCollectionTable:{
		    myTable:'',//未采集列表
		    //创建列表
	        cretateTable : function (){
	        	this.myTable = table.render({
					elem: '#LAY-notcollection-mode',
					height: $(document).height() - $('#LAY-notcollection-mode').offset().top-13,
					title: '未采集列表',
					toolbar:true,
					limit: 1000,
					done: function(res, curr, count) {
						var that =this.elem.next();
						var params ={res:res, curr:curr, count:count,that:that};
						layui.event("uncollection", "done", params);
						if(res.rowCount && (res.rowCount == 0 ||res.rowCount == '0')) {
							res.code = 0;
							res.data = [];
						}
					},
					cols: [[
					    {type: 'numbers',title: '序号',fixed: 'left'},
					    {field: '条码号',title: '条码号',width: 150,sort: true},
					    {field: '姓名',title: '姓名',width: 150,sort: true},
					    {field: '急查',width: 100,title: '急查',sort: true}]
					],
					loading:true,
					text: {none: '暂无相关数据'}
				});
		    },
		    //未采集列表数据加载
		    onSearch : function(url,where){
				uxtable.onSearch(basic.UnCollectionTable.myTable,url,where);
		    }
		}
    };
	basic.CollectionTable.cretateTable();
	//暴露接口
	exports('basic',basic);
});