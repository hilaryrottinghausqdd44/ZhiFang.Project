/**
	@name：默认设置
	@author：liangyl
	@version 2020-02-10
 */
layui.extend({

}).define(['form','paraform','uxutil','colorpicker'],function(exports){
	"use strict";
	
	var $ = layui.$,
	    form = layui.form,
		uxutil = layui.uxutil,
		colorpicker = layui.colorpicker,
		paraform = layui.paraform;
		
	  //获取默认设置参数服务
    var SELECT_DEFEAULT_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?isPlanish=true"; 
	  //获取出厂设置参数服务
    var SELECT_FACTORY_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryFactorySettingPara?isPlanish=true"; 
	 //保存系统默认设置参数
    var SAVE_DEFALUT_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemDefaultPara"; 
	//类型
	var PARATYPECODE="";
	//最终数据
	var DEFALUTLIST=[];
	//数据合并（出厂设置和默认数据）后
	var DEFALUTLISTMERGE=[];
	 //是否保存后关闭
    var ISCOLOSE =false;
     //是否全部采用出厂设置
    var IS_RESET_FACTORY =false;
    //出厂设置查询字段
    var DEFALUT_FIELDS ='BPara_ParaNo,BPara_ParaValue,BPara_Id,BPara_CName,BPara_ParaEditInfo,BPara_ParaType,BPara_ShortCode,BPara_SystemCode,BPara_TypeCode';

	
	var defalutform={
		//全局项
		config:{
		},
		//设置全局项
		set:function(options){
			var me = this;
			me.config = $.extend({},me.config,options);
			return me;
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,defalutform.config,setings);
	};
	Class.pt = Class.prototype;
   
    //核心入口
	defalutform.render = function(options){
		var me = new Class(options);
		me.loadData = me.loadDatas;
		me.clearData = me.clearData;
		return me;
	};
	//保存
	form.on('submit(default_save)',function(data){
		Class.pt.onSaveClick(data);
	});
	//全部采用出厂设置 
	$('#reset').on('click',function(){
    	var me = this;
    	IS_RESET_FACTORY = true;
    	var obj = {},btnArr=[];
    	for(var i=0;i<DEFALUTLIST.length;i++){
    		var obj1 = Class.pt.getValues(DEFALUTLIST[i],DEFALUTLIST[i].Old_BPara_ParaValue);
    	    Object.assign(obj,obj1);
    	    btnArr.push('Default_btn_'+DEFALUTLIST[i].BPara_ParaNo);
    	}
    	form.val('Default',obj);
		form.render();		
	    //隐藏采用出厂设置按钮
	    for(var i=0;i<btnArr.length;i++){
	    	$('#'+btnArr[i]).hide();
	    }
	});
	//刷新
    $('#refresh').on('click',function(){
    	var me = this;
    	Class.pt.loadDatas(PARATYPECODE);
	});
	Class.pt.compare = function(property){
	    return function(a,b){
	        var value1 = a[property];
	        var value2 = b[property];
	        return value1 - value2;
	    }
	};

    //加载表单数据,isclose 保存设置是否关闭
	Class.pt.loadDatas = function(paraTypeCode,isclose){
		var me = this;
		IS_RESET_FACTORY = false;
		DEFALUTLIST=[];
		var URL = SELECT_DEFEAULT_URL;
		PARATYPECODE = paraTypeCode;
		if(isclose)ISCOLOSE=isclose;
		var para = paraform.render();
		var index = layer.load();
		para.load(URL,paraTypeCode,'',function(data){
			layer.close(index);
			var list = data.value ? data.value.list : [] || [];
			if(list.length==0){//数据不存在时
				me.clearData('#ContentDiv2');
				return;
			}
			//排序
			list.sort(me.compare('BPara_DispOrder'));
			var index2 = layer.load();
			//获取出厂设置,匹配
			para.load(SELECT_FACTORY_URL,paraTypeCode,DEFALUT_FIELDS,function(data1){
				layer.close(index2);
				var arr = data1.value.list || [];
				var arr2=[];
				for(var i=0;i<list.length;i++){
					list[i].IsShow=false;
					list[i].Old_BPara_ParaValue=list[i].BPara_ParaValue;
					for(var j=0;j<arr.length;j++){
						if( list[i].BPara_ParaNo  == arr[j].BPara_ParaNo){
							list[i].Old_BPara_CName = arr[j].BPara_CName;
	                        list[i].Old_BPara_ParaEditInfo = arr[j].BPara_ParaEditInfo;
							list[i].Old_BPara_ParaType = arr[j].BPara_ParaType;
							list[i].Old_BPara_ShortCode = arr[j].BPara_ShortCode;
							list[i].Old_BPara_SystemCode = arr[j].BPara_SystemCode;
							list[i].Old_BPara_TypeCode = arr[j].BPara_TypeCode;
							if(list[i].BPara_ParaValue  != arr[j].BPara_ParaValue){
								list[i].Old_BPara_ParaValue = arr[j].BPara_ParaValue;
							}
						}
					}
					arr2.push(list[i]);
				}
				me.defaultBtnIsShow(true);
				DEFALUTLIST = arr2;
				para.createControl(arr2,'#ContentDiv2','Default');
				var html ='<div class="layui-btn-container" style="padding-left: 15px;" >'+
				    '<button class="layui-btn  layui-btn-xs"   lay-submit="" id="default_save" lay-filter="default_save"><i class="iconfont">&#xe603;</i>&nbsp;保存设置</button>'
				    '</div>';
//              $('#Default_row').append(html);//追加新元素
                 document.getElementById("Default_toolbarbtn").innerHTML =html;
				me.isShowBtn();
				me.setValues();
				me.initListeners();
			});
		});
	};
	 //显示采用出厂设置
	Class.pt.isShowBtn = function(){
		var me = this;
		//判断是否显示
		for(var i=0;i<DEFALUTLIST.length;i++){
			var valType="C";
			var ParaEditInfo = DEFALUTLIST[i].BPara_ParaEditInfo;
		    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
    		var valTypeStr = ParaEditInfoStr.split('|');
    		if(valTypeStr[0])valType = valTypeStr[0];
    		var ParaValue = DEFALUTLIST[i].Old_BPara_ParaValue;
    		switch (valType){
    			case 'E':
    			    if(!DEFALUTLIST[i].Old_BPara_ParaValue)ParaValue = DEFALUTLIST[i].Old_BPara_ParaValue="0";
    				break;
    			default:
    			    ParaValue = DEFALUTLIST[i].Old_BPara_ParaValue;
    				break;
    		}
			if(DEFALUTLIST[i].BPara_ParaValue != ParaValue ){
				$('#Default_btn_'+DEFALUTLIST[i].BPara_ParaNo).parent().removeClass('layui-hide');
				$('#Default_btn_'+DEFALUTLIST[i].BPara_ParaNo).show();
			} 
		}
	};
    //清空表单数据	
	Class.pt.clearData = function(divid){
		var me = this;
		me.defaultBtnIsShow(false);
		var HTML ='<div class="layui-none">暂无数据</div>';
		$(divid).html(HTML);
	};
	  //默认设置按钮隐藏
    Class.pt.defaultBtnIsShow = function(bo){
    	if(bo){
    		$('#refresh').show();
    		$('#reset').show();
    	}else{
    		$('#refresh').hide();
    		$('#reset').hide();
    	}
    };
     /**@overwrite 获取需要保存的数据*/
	Class.pt.getParams = function(data) {
		var me = this;
		var entity = JSON.stringify(data.field).replace(/Default_/g, "");
	    if(entity) entity = JSON.parse(entity);
		var entityList=[];
		for(var i = 0 ;i<DEFALUTLIST.length;i++){
			var obj = {
				Id:DEFALUTLIST[i].BPara_Id,
				BVisible: 1,
				CName: IS_RESET_FACTORY ? DEFALUTLIST[i].Old_BPara_CName : DEFALUTLIST[i].BPara_CName,
				IsUse:1,
				ParaEditInfo:IS_RESET_FACTORY ? DEFALUTLIST[i].Old_BPara_ParaEditInfo : DEFALUTLIST[i].BPara_ParaEditInfo,
				ParaNo: IS_RESET_FACTORY ? DEFALUTLIST[i].Old_BPara_ParaNo : DEFALUTLIST[i].BPara_ParaNo,
				ParaType: DEFALUTLIST[i].BPara_ParaType,
				ParaValue: IS_RESET_FACTORY ? DEFALUTLIST[i].BPara_ParaValue : DEFALUTLIST[i].BPara_ParaValue,
				ShortCode: IS_RESET_FACTORY ? DEFALUTLIST[i].Old_BPara_ShortCode : DEFALUTLIST[i].BPara_ShortCode,
				SystemCode: IS_RESET_FACTORY ? DEFALUTLIST[i].Old_BPara_SystemCode : DEFALUTLIST[i].BPara_SystemCode,
				TypeCode: IS_RESET_FACTORY ? DEFALUTLIST[i].Old_BPara_TypeCode : DEFALUTLIST[i].BPara_TypeCode
			}
			var valType="C";
			var ParaEditInfo = DEFALUTLIST[i].BPara_ParaEditInfo;
		    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
    		var valTypeStr = ParaEditInfoStr.split('|');
    		valType = valTypeStr[0];
            if(valType == 'E')obj.ParaValue='0';
    	    for(var key  in entity){
				if(key == DEFALUTLIST[i].BPara_ParaNo){
					var btn = 'Default_btn_'+DEFALUTLIST[i].BPara_ParaNo;
					//默认出厂设置按钮
					if($('#'+btn).prop("disabled") && !$("#"+btn).is(":hidden")){//设置了采用出厂设置（行按钮)
	                    me.getfactoryentity(obj,DEFALUTLIST[i]);
					}else{
						switch (valType){
							case 'E':
			                	//存在下拉列表时
							    if(valTypeStr[2]){
							    	obj.ParaValue= entity[key];
							    }else {
							    	obj.ParaValue = entity[key]=="on" ? "1" : "0";
							    }
							    break;  
							case 'S':
								 obj.ParaValue = entity[key];
							    break; 
							case 'CL':
								obj.ParaValue = entity[key];
				    		    break;
							default:
								obj.ParaValue = entity[key];
								break;
						}
					}
				}
		    }
			if(uxutil.cookie.get(uxutil.cookie.map.USERID)){
				obj.Operator=uxutil.cookie.get(uxutil.cookie.map.USERNAME);
	        	obj.OperatorID=uxutil.cookie.get(uxutil.cookie.map.USERID);
	        }
			entityList.push(obj) ;
		}
		var entityList ={
			entityList :entityList
		}
		return entityList;
	};
	Class.pt.getfactoryentity = function(obj,list) {
		var me = this;
		obj.ParaValue  = list.Old_BPara_ParaValue;
		obj.CName  = list.Old_BPara_CName; 
		obj.ParaEditInfo  = list.Old_BPara_ParaEditInfo; 
		obj.ParaNo  = list.Old_BPara_ParaNo; 
		obj.ShortCode  = list.Old_BPara_ShortCode; 
        obj.TypeCode= list.Old_BPara_TypeCode; 
        obj.SystemCode = list.Old_BPara_SystemCode;
		return obj;
	};
	//表单保存处理
	Class.pt.onSaveClick = function(data) {
		var me = this;
		
		var params = me.getParams(data);
		if (!params) return;
		params = JSON.stringify(params);
		//显示遮罩层
		var config = {
			type: "POST",
			url: uxutil.path.ROOT +SAVE_DEFALUT_URL	,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				layer.msg("保存成功",{icon:6,time:2000});
				IS_RESET_FACTORY = false;
				me.loadDatas(PARATYPECODE);
				if(ISCOLOSE)parent.layer.closeAll('iframe');
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	};
	
	
     //还原
	Class.pt.setValues = function() {
		var me = this;
		var obj = {};
		for(var i=0;i<DEFALUTLIST.length;i++){
			var ParaNoID = 'Default_'+[DEFALUTLIST[i].BPara_ParaNo];
		    var valType="C";
			var ParaEditInfo = DEFALUTLIST[i].BPara_ParaEditInfo;
		    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
		    
		    
    		var valTypeStr = ParaEditInfoStr.split('|');
    		//如果存在多个值，截取第一个|前的内容，取出类型
    		if(ParaEditInfo.length>=1)valType = valTypeStr[0];
    		
    		switch (valType){
    			case 'E':
    			    var ParaValue  =  DEFALUTLIST[i].BPara_ParaValue=="1" ? "true" :"";
    			    if(valTypeStr[2]){//存在下拉列表
    			    	//默认值为空时,默认为否
    			    	if((!valTypeStr[1] || valTypeStr[1]=='0') && !DEFALUTLIST[i].BPara_ParaValue){
    			    		ParaValue = "0";
    			    	}
    			   	    else {
    			   	    	 ParaValue = DEFALUTLIST[i].BPara_ParaValue;
    			   	    }
    			    }
    			    obj[ParaNoID] = ParaValue;
    				break;
    		    case 'S':
    			     obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
			         if(obj[ParaNoID]=='0')$("input[name='Default_"+DEFALUTLIST[i].BPara_ParaNo+"']").removeAttr('checked');			    
    				break;
    			case 'RGB':
                    obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
                    //表单赋值(颜色)
					colorpicker.render({
					    elem: '#'+ParaNoID+'color-form',
					    color: obj[ParaNoID],
					    done: function(color){
					       $(this.elem.split('color-form')[0]).val(color);
					    },
					    change: function(color){	
					    	var ParaNo = this.elem.split('color-form')[0].split('#Default_')[1];
					    
					    	var btn =$('#Default_btn_'+ParaNo)[0];
					    	if(this.color!=color){
					    		$(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
					    	}else{
					    		$(btn).addClass("layui-btn-disabled").attr('disabled',true);
					    	}
					    }
					});
    				break;	
    			case 'CL':
    			    obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
	    		    //获取下拉内容的最后一个值
//	    	        if(valTypeStr[3]){
//	    	        	var list = valTypeStr[3].split('#') || [];
//	    	            if(list.length>0){
//	    	            	var rulearr = list[list.length-1].split("/");
//	    	            	//1为可手工输入结果
//	    	            	if(rulearr.length>0 && rulearr[1]=='1'){
//	    	            		obj['Default_Text_'+DEFALUTLIST[i].BPara_ParaNo] = DEFALUTLIST[i].BPara_ParaValue;
//	    	            	}
//	    	            	else 
//	    	            	    obj[ParaNoID] =DEFALUTLIST[i].BPara_ParaValue;
//	    	            }
//	    	        }else{
//	    	        	obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
//	    	        }
	    		    break;
    			default:
    			   obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
    				break;
    		}
		}
		form.val('Default',obj);
		form.render();
	};
	
	Class.pt.getValues = function(rowobj,value){
		var me = this;
		var obj={};
		var valType="C";
		var ParaEditInfo = rowobj.BPara_ParaEditInfo;
	    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
		var valTypeStr = ParaEditInfoStr.split('|');
		//如果存在多个值，截取第一个|前的内容，取出类型
		if(ParaEditInfo.length>=1)valType = valTypeStr[0];
		switch (valType){
			case 'E':
			    var ParaValue  = value =="1" ? true :false;
			    if(valTypeStr[2]) ParaValue = value=="1" ? "1" :"0";//存在下拉列表
			    obj['Default_'+rowobj.BPara_ParaNo] = ParaValue;
				break;
			case 'S':
			    obj['Default_'+rowobj.BPara_ParaNo] = value;
			    if(obj['Default_'+rowobj.BPara_ParaNo]=='0')$("input[name='Default_"+rowobj.BPara_ParaNo+"']").removeAttr('checked');			    
    		break;
    		case 'RGB':
    		    obj['Default_'+rowobj.BPara_ParaNo] = value;
    		     //渲染颜色
				colorpicker.render({
				    elem: '#Default_'+rowobj.BPara_ParaNo+'color-form',
					color: value,
					done: function(color){
				       $(this.elem.split('color-form')[0]).val(color);
				    },
				    change: function(color){	
				    	var ParaNo = this.elem.split('color-form')[0].split('#Default_')[1];
				    	var btn =$('#Default_btn_'+ParaNo)[0];
				    	if(this.color!=color){
				    		$(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
				    	}else{
				    		$(btn).addClass("layui-btn-disabled").attr('disabled',true);
				    	}
				    	
				    }
				});		    
    		    break;
    		case 'CL':
    		    //获取下拉内容的最后一个值
//  	        if(valTypeStr[3]){
//  	        	var list = valTypeStr[3].split('#') || [];
//  	            if(list.length>0){
//  	            	var rulearr = list[list.length-1].split("/");
//  	            	//1为可手工输入结果
//  	            	if(rulearr.length>0 && rulearr[1]=='1')obj['Default_Text_'+rowobj.BPara_ParaNo] = value;
//  	            	else 
//  	            	    obj['Default_'+rowobj.BPara_ParaNo] = value;
//  	            }
//  	        }else{
//  	        	obj['Default_'+rowobj.BPara_ParaNo] = value;
//  	        }
			    obj['Default_'+rowobj.BPara_ParaNo] = value;
    		    break;
			default:
			    obj['Default_'+rowobj.BPara_ParaNo] = value;
				break;
		}
		return obj;
	};
	
	//采用出厂设置监听
	Class.pt.initListeners = function(){
		var me = this;
		
		//高度
        $("#Default").css("height", ($(window).height() - 135) + "px");//设置表单容器高度
        $("#Default").css("width", $('.cardHeight').width()+ "px");

        // 窗体大小改变时，调整高度显示
    	$(window).resize(function() {
			 //表单高度
		    $("#Default").css("height", ($(window).height() - 135) + "px");//设置表单容器高度
		    $("#Default").css("width", $('.cardHeight').width()+ "px");
    	});
    	
		for(var i=0;i<DEFALUTLIST.length;i++){
			var ParaNo = DEFALUTLIST[i].BPara_ParaNo;
			var btnStr = 'Default_btn_'+DEFALUTLIST[i].BPara_ParaNo; 
	     	var btnParaNo = btnStr.split('Default_btn_')[1];
			if(btnParaNo  == DEFALUTLIST[i].BPara_ParaNo){ //按钮的联动
				$('#Default_btn_'+ParaNo).on('click',function(){ //采用出厂按钮
					IS_RESET_FACTORY = false;
					var id = $(this).attr('id').split('Default_btn_')[1];
					for(var j=0;j<DEFALUTLIST.length;j++){
						if(DEFALUTLIST[j].BPara_ParaNo ==id ){
							var obj = me.getValues(DEFALUTLIST[j],DEFALUTLIST[j].Old_BPara_ParaValue);
							form.val('Default',obj);
		                    form.render();
		                    $(this).addClass("layui-btn-disabled").attr('disabled',true);
						}
					}
				});
			}
			//checkbox的联动
            form.on('checkbox(Default_'+DEFALUTLIST[i].BPara_ParaNo+')', function(data){
            	var id = $(this).attr('name').split('Default_')[1];
            	me.showBtnByValue(id,data.elem.checked);
			});    
			//radio的联动
            form.on('radio(Default_'+DEFALUTLIST[i].BPara_ParaNo+')', function(data){
            	var id = $(this).attr('name').split('Default_')[1];
            	me.showBtnByValue(id,data.value);
			}); 
			//下拉的联动
		    form.on('select(Default_'+DEFALUTLIST[i].BPara_ParaNo+')', function(data){
            	var id = $(data.elem).attr('name').split('Default_')[1];
//          	//下拉可编辑同步输入框
//          	if($(data.elem).prev().attr('id')){
//          		for(var j=0;j<DEFALUTLIST.length;j++){
//						if(DEFALUTLIST[j].BPara_ParaNo == $(data.elem).prev().attr('id').split('Default_Text_')[1]){
//							
//							var textid = $(data.elem).prev().attr('id');
//							$('#'+textid).val(data.value);
//							var obj = me.getValues(DEFALUTLIST[j],DEFALUTLIST[j].Old_BPara_ParaValue);
//							var btn = $('#Default_btn_'+DEFALUTLIST[j].BPara_ParaNo)[0];
//							if(obj['Default_Text_'+DEFALUTLIST[j].BPara_ParaNo] == data.value)$(btn).addClass("layui-btn-disabled").attr('disabled',true);
//							else
//							    $(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
//						}
//					}
//          	}else
            	     me.showBtnByValue(id,data.value);
			}); 

			//文本域联动
			$('input[name="Default_'+DEFALUTLIST[i].BPara_ParaNo+'"]').on('input propertychange', function() {//监听文本框\
				var id = $(this).attr('name').split('Default_')[1];
				me.showBtnByValue(id,$(this).val());

			});
			//文本域联动(文本框没设置ID)
			$('#Default_'+DEFALUTLIST[i].BPara_ParaNo).on('input propertychange', function() {//监听文本框
                var id = $(this).attr('id').split('Default_')[1];
                me.showBtnByValue(id,$(this).val());
	        });
//	        //下拉可编辑输入框只输入不选择联动
//			$('#Default_Text_'+DEFALUTLIST[i].BPara_ParaNo).on('input propertychange', function() {//监听文本框
//              var id = $(this).attr('id').split('Default_Text_')[1];
//           
//              for(var j=0;j<DEFALUTLIST.length;j++){
//					if(DEFALUTLIST[j].BPara_ParaNo ==id ){
//						var obj = me.getValues(DEFALUTLIST[j],DEFALUTLIST[j].Old_BPara_ParaValue);
//						var btn =$('#Personality_btn_'+PERSONALITYDATA[j].BParaItem_BPara_ParaNo)[0];
//						if(obj['Default_Text_'+DEFALUTLIST[j].BPara_ParaNo] == $(this).val())$(btn).addClass("layui-btn-disabled").attr('disabled',true); 
//						else
//						    $(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
//					}
//				}
//	        });
		}
	};
    //根据输入值联动显示隐藏按钮
    Class.pt.showBtnByValue = function(id,value){
		var me =  this;
		for(var j=0;j<DEFALUTLIST.length;j++){
			if(DEFALUTLIST[j].BPara_ParaNo ==id ){
				var obj = me.getValues(DEFALUTLIST[j],DEFALUTLIST[j].Old_BPara_ParaValue);
				var btn = $('#Default_btn_'+DEFALUTLIST[j].BPara_ParaNo)[0];
				if(obj['Default_'+DEFALUTLIST[j].BPara_ParaNo] == value)$(btn).addClass("layui-btn-disabled").attr('disabled',true);
				else
				    $(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
			}
		}
    };
    //暴露接口
	exports('defalutform',defalutform);
});		