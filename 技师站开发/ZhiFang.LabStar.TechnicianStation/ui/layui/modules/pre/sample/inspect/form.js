/**
 * @name：modules/pre/sample/inspect/form 撤销送检
 * @author：liangyl
 * @version 2021-09-09
 */
layui.extend({
	uxutil:'ux/util'
}).define(['uxutil','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		MOD_NAME = 'RevocationForm';
	
	//扫码获取信息
	var BARCODE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleRevocationExchangeInspectDataVerifyByBarCode";
	//撤销送检确认服务
	var SAVE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleRevocationExchangeInspectByBarCode";
	//样本单信息字段
	var FIELDS = [
		'LisBarCodeForm_BarCode','LisBarCodeFormVo_LisBarCodeForm_BarCode','LisBarCodeForm_LisOrderForm_ParItemCName'
	];
	//内部表单dom
	var FORM_DOM = [
	'<div class="layui-form" id="{formId}" lay-filter="{formId}">',
	   '<div class="layui-form-item">', 
        '<label class="layui-form-label">输入条码号</label>', 
        '<div class="layui-input-block">', 
         '<input type="text" name="{formId}-barCode" id="{formId}-barCode" placeholder="扫描条码" autocomplete="off" class="layui-input" />', 
        '</div>', 
       '</div>', 
        '<div class="layui-form-item">', 
        '<label class="layui-form-label">条码号/姓名</label>', 
        '<div class="layui-input-block">', 
         '<input type="text" name="{formId}-barCodeAndName"  placeholder="" autocomplete="off" readonly="true" class="layui-input" />', 
        '</div>', 
       '</div>', 
       '<div class="layui-form-item layui-form-text">', 
        '<label class="layui-form-label">医嘱项目</label>', 
        '<div class="layui-input-block">', 
         '<textarea name="{formId}-ItemName" placeholder="" readonly="true" class="layui-textarea"></textarea>', 
        '</div>', 
       '</div>', 
	'</div>'
	];
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//是否能采集撤销
	var isTrue = false;
	//样本单
	var RevocationForm = {
		formId:null,//表单ID
		//对外参数
		config:{
			domId:null,
			height:null,
			nodetype:PARAMS.NODETYPE
		},
		//内部表单参数
		formConfig:{
			
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,RevocationForm.config,setings);
		me.formConfig = $.extend({},me.formConfig,RevocationForm.formConfig);
		me.formId = me.config.domId + "-form";
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = FORM_DOM.join("").replace(/{formId}/g,me.formId);
		$('#' + me.config.domId).append(html);
		form.render();
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		$("#"+me.formId+"-barCode").focus();
		//扫码,回车事件
	    $("#"+me.formId+"-barCode").on('keydown', function (event) {
	        if (event.keyCode == 13) {
	        	isTrue = false;
	        	var barcode = $("#"+me.formId+"-barCode").val();
	        	if(barcode){
	        		me.onBarCode(barcode,function(list){
	        			if(list.length>0){
	        				var failureInfo  = list[0].failureInfo || '[]';
	        				failureInfo = JSON.parse(failureInfo);
	        				if(failureInfo.length>0){   //有验证的的提示
	        					layer.msg(failureInfo[0].failureInfo,{ icon: 5, anim: 6 });
	        					me.clearData();
//	        					$("#"+me.formId+"-barCode").val('')
	        					$("#"+me.formId+"-barCode").focus();
	        				}else{
	        					var defaultValues ={ };
		        				defaultValues[me.formId + '-barCodeAndName'] = list[0].LisBarCodeForm.BarCode+'/'+list[0].LisBarCodeForm.LisPatient.CName;
		        				defaultValues[me.formId + '-ItemName'] = list[0].LisBarCodeForm.LisOrderForm.ParItemCName;
		        				form.val(me.formId ,defaultValues);
		        				$("#"+me.formId+"-barCode").focus();
		        				isTrue =true;
	        				}
	        			}else{
	        				layer.msg("未找到该条码信息！",{icon:5});
	        				me.clearData();
	        			}
	        		});
	        	}else{
	        		layer.msg('条码号不能为空,请扫码!',{icon:5});
	        		me.clearData();
	        	}
	            return false;
	        }
	    });
	};
	
	//清空数据
	Class.prototype.clearData = function(){
		var me = this;
		var defaultValues ={ };
		defaultValues[me.formId + '-barCodeAndName'] = '';
		defaultValues[me.formId + '-ItemName'] = '';
		defaultValues[me.formId + '-barCode'] = '';
		form.val(me.formId ,defaultValues);
		$("#"+me.formId+"-barCode").focus();
	};
	//扫码
	Class.prototype.onBarCode = function(barcode,callback){
		var me = this;
		if(!barcode){
			layer.msg("请先扫码！",{icon:5});
			return false;
		}
		var config = {
			type:'post',
			url:BARCODE_LIST_URL,
			data:JSON.stringify({
				nodetype:me.config.nodetype,//站点类型
				barcodes:barcode
			})
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback(data.value || {});
			}else{
				me.clearData();
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	//保存
	Class.prototype.onSave = function(){
		var me = this,
			values = form.val(me.formId);
	    var barCode = values[me.formId + '-barCode'];	    
		if(!isTrue){
			layer.msg("请先扫码,再撤销确认！",{icon:5});
		}else{
			var config = {
				type:'post',
				url:SAVE_URL,
				data:JSON.stringify({
					nodetype:me.config.nodetype,//站点类型
					barcodes:barCode
				})
			};
			var index = layer.load();
			uxutil.server.ajax(config,function(data){
				layer.close(index);
				isTrue = false;
				if(data.success){
				    layer.msg("保存成功",{icon:6,time:2000});
				    $("#"+me.formId+"-barCode").val('');
					parent.layer.closeAll('iframe');
					//被撤销确认的条码号
					var id = "",isTrue = true;
					if(data.value && data.value[0]){
						var obj = JSON.parse(data.value[0]);
						if(obj[barCode]=='true')id = barCode;
					}
			        //返回撤销成功的条码号
			        parent.afterUpateInspectRevocation(data.value, barCode);
				}else{
					layer.msg(data.value,{ icon: 5, anim: 6 });
				}
			});
		}
	};
	//核心入口
	RevocationForm.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		//实例化表单
		me.form = form.render();
		//监听事件
		me.initListeners();
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,RevocationForm);
});
