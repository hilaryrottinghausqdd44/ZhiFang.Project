/**
 * @name: 条码打印
 * @author: liangyl
 * @version: 
 */
layui.define(['uxutil','table','form','laydate','element','uxtable'], function (exports) {
	"use strict";
	
	var $ = layui.$,
	    form = layui.form,
	    uxutil=layui.uxutil,
	    laydate = layui.laydate,
	    element=layui.element,
	    uxtable=layui.uxtable,
		table = layui.table;
	
	//外部接口
	var basic = {
		//查询条件
		SearchBar : {
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
		    //查询条件下拉框处理
		    showSelect:function (){ 
		    	form.render('select');
		    	$('#LAY-e-txtSelect').hide();
				 //核收条件，科室病区对应下拉列表 ，其他对应文本框
				form.on('select(LAY-barcode-comselect-unconfirm)',function(obj){
					//假设科室id=0
					if(obj.value=='0'){
						$('#cbSelect').show();
						$('#LAY-e-txtSelect').hide();
						$('#LAY-e-txtSelect').addClass('layui-hide');
					}else{
						$('#cbSelect').hide();
						$('#LAY-e-txtSelect').removeClass('layui-hide');
						$('#LAY-e-txtSelect').show();
					}
					form.render();
				});
		    }
		},
		//弹出窗体实现功能
		OpenWin:{
			//判断浏览器大小方法
	        screen :function($) {
		        //获取当前窗口的宽度
			    var width = $(window).width();
			    if (width > 1200) {
			        return 3;   //大屏幕
			    } else if (width > 992) {
			        return 2;   //中屏幕
			    } else if (width > 768) {
			        return 1;   //小屏幕
			    } else {
			        return 0;   //超小屏幕
			    }
		    },
			//修改样本类型
			onEditSampleType : function(obj){
				layer.open({
		           type: 2,  
		           title: '修改样本类型',  
		           maxmin: false,  
		           id: "LAY-sampletype-form-edit-1",
		           area: ['500px', '405px'],  
		           resize:false,//禁止拖拉框的大小
		           content:'sampletype.html',
		           btn:["确认修改","关闭"],
		           btnAlign:'c',
			       success: function(layero, index){
			            var sampletypeform = layero.find('iframe').contents().find('#LAY-sampletype-form');  // div.html() div里面的内容,不包含当前这个div	            
			            sampletypeform.find('#acceptance_type').val(obj.样本类型);
			            sampletypeform.find('#barCode').val(obj.条码号);
			            sampletypeform.find('#cname').val(obj.姓名);
			            sampletypeform.find('#comitem').val(obj.医嘱项目);
			        }
		        }); 
			},
			//信息补录
			onEditSupplement : function(obj){
				layer.open({  
		           type: 2,  
		           title: '信息补录',  
		           maxmin: false,  
		           id: 'LAY-supplement-edit-form',
//		           skin: 'layui-layer-molv',  
		           area: ['500px', '352px'],  
		           content:'supplement.html',
		           resize:false,//禁止拖拉框的大小
		           btn:["保存","取消"],
		           btnAlign:'c',
		           yes: function(index, layero){
		           	   var iframeWindow = window['layui-layer-iframe'+ index] ,
				          submitID = 'LAY-supplement-save-submit',
				          submit = layero.find('iframe').contents().find('#'+ submitID);
			           var setform = iframeWindow.layui.form;
			          //监听提交
			          setform.on('submit('+ submitID +')', function(data){
//			             var obj= data.field; //获取提交的字段
//			             //调用服务保存
//			             layer.msg('调用服务保存',{icon:1,time:2000});
//			             //提交 Ajax 成功后，静态更新表格中的数据
//			             layer.close(index); //关闭弹层
			          });  
			          submit.trigger('click');
				   },
				   btn2: function(index, layero){
//				   	   return false;//开启该代码可禁止点击该按钮关闭
				   },
		           success: function(layero, index){
		           	    var editInfoform = layero.find('iframe').contents().find('#LAY-edit-info');    
			            var str=obj.条码号+'/'+obj.姓名;
			            editInfoform.find('#supplement').val('0');
			            editInfoform.find('#txtBarCode').val(str);
			            editInfoform.find('#ZDY2').val(obj.医嘱项目);
			        }
		        }); 
			},
		    //原始医嘱
		    onOriginalAdvice : function($,obj){
		    	layer.open({
		           type: 2,  
		           title: '原始医嘱',  
		           maxmin: true,  
		           id: '1001',
//		           skin: 'layui-layer-molv',  
				   area: basic.OpenWin.screen($) < 2 ? ['90%', '70%'] : ['1180px', '600px'],
				   content:'originaladvice.html'//弹框显示的url,对应的页面
		        }); 
		    },
		    //撤销确认
		    onRevoke : function (obj){
		    	layer.open({  
		           type: 2,  
		           title: '撤销确认',  
		           maxmin: false,  
		           btnAlign:'c',
		           area: ['500px', '402px'],  
		           resize:false,
		           content:'revoke.html',
		           btn:["撤销确认","取消"],
		           yes: function(index, layero){
		           	   var iframeWindow = window['layui-layer-iframe'+ index] ,
				          submitID = 'LAY-revoke-form-submit',
				          submit = layero.find('iframe').contents().find('#'+ submitID);
			           var setform = iframeWindow.layui.form;
			          //监听提交
			          setform.on('submit('+ submitID +')', function(data){
			             var obj= data.field; //获取提交的字段
			             //调用服务保存
			             layer.msg('调用服务保存',{icon:1,time:2000});
			             //提交 Ajax 成功后，静态更新表格中的数据
			             layer.close(index); //关闭弹层
			          });  
			          submit.trigger('click');
				   },
				   btn2: function(index, layero){
//				   	   return false;//开启该代码可禁止点击该按钮关闭
				   },
		           success: function(layero, index){
		                var revoke = layero.find('iframe').contents().find('#LAY-revoke-form');    
			            var str=obj.条码号+'/'+obj.姓名;
			           	revoke.find('#txtBarCode2').val(obj.条码号);
			            revoke.find('#supplement').val('0');
			            revoke.find('#txtBarCode').val(str);
			            revoke.find('#ZDY2').val(obj.医嘱项目);
			        }
		        });
		    },
		     /**本地数据存储*/
		    localStorageset : function(name, value) {
				localStorage.setItem(name, value);
			},
			localStorageget :function(name) {
				return localStorage.getItem(name);
			},
			 localStorageremove : function(name) {
				localStorage.removeItem(name);
			},
			//底部按钮显示隐藏
			isShowBtn :function(){
				if( basic.OpenWin.localStorageget('printConfig')){
					var obj = JSON.parse(basic.OpenWin.localStorageget('printConfig'));
					if(obj){
						if(obj.autoPrint!='1') $('#autoPrint').hide();
					       else $('#autoPrint').show();
		                if(obj.directPrint!='1') $('#directPrint').hide();
					       else $('#directPrint').show();   
					    if(obj.sampleNo!='1') $('#sampleNo').hide();
					       else $('#sampleNo').show();     
					}
				}
			},
		    //设置打印模式，显示隐藏
			onShowModePrint : function(){
				layer.open({
		           type: 2,  
		           title: '设置',  
		           maxmin: false,  
		           id: "LAY-setform-form",
//		           skin: 'layui-layer-molv',  
		           area: ['350px', '290px'],  
		           resize:false,//禁止拖拉框的大小
		           content:'setform.html',
		           btnAlign:'c',
		           btn:["确定","取消"],
		           yes: function(index, layero){
		           	   var iframeWindow = window['layui-layer-iframe'+ index] ,
				          submitID = 'LAY-set-print-submit',
				          submit = layero.find('iframe').contents().find('#'+ submitID);
			           var setform = iframeWindow.layui.form;
			          //监听提交
			          setform.on('submit('+ submitID +')', function(data){
			             var obj= data.field; //获取提交的字段
			             basic.OpenWin.localStorageset('printConfig',JSON.stringify(obj));
			             basic.OpenWin.isShowBtn();
			             //提交 Ajax 成功后，静态更新表格中的数据
			             layer.close(index); //关闭弹层
			          });  
			          submit.trigger('click');
				   },
				   btn2: function(index, layero){
//				   	   return false;//开启该代码可禁止点击该按钮关闭
				   },
			       success: function(layero, index){

			       }
		        }); 
			}
		}
    };
	//暴露接口
	exports('basic',basic);
});