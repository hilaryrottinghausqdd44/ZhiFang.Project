/**
	@name：个性设置
	@author：liangyl
	@version 2020-02-12
 */
layui.extend({
	uxdata:'ux/data',
}).define(['form','paraform','uxutil','uxtable','table','colorpicker','multiSelect'],function(exports){
	"use strict";
	
	var $ = layui.$,
	    form = layui.form,
	    uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		table = layui.table,
		colorpicker = layui.colorpicker,
		multiSelect = layui.multiSelect,
		paraform = layui.paraform;
		
	  //获取默认设置参数服务
    var SELECT_DEFEAULT_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?isPlanish=true"; 
	 //获取出厂设置参数服务
    var SELECT_FACTORY_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryFactorySettingPara?isPlanish=true"; 
	 //保存个性设置参数
    var SAVE_PERSONALITY_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemParaItem"; 
     //询系统个性参数设置
    var SELECT_PERSONALITY_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemParaItem?isPlanish=true"; 
	//删除个性参数
    var DEL_URL = '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DeleteSystemParaItem';

	//默认设置数据
	var DEFALUTLIST=[];
    //相关性
	var SYSTEMTYPECODE="";
	//分组类型Code
	var PARATYPECODE = "";
	//类型列表选择行
	var CHECKROWDARA = [];
	//个性设置已保存的数据,
	var PERSONALITYDATA = [];
	
	//个性设置ID
	var OBJECTID ="";
	//个性设置Name
	var OBJECTIDNAME = "";
	//是否保存后关闭
    var ISCOLOSE =false;
    
	var personalityform={
		//全局项
		config:{
			IsLink:false  //是否关联业务模块
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
		me.config = $.extend({},me.config,personalityform.config,setings);
	};
	Class.pt = Class.prototype;
    //核心入口
	personalityform.render = function(options){
		var me = new Class(options);
		me.loadDefault = me.loadDefault;
		me.loadData = me.loadData;
		me.clearData = me.clearData;
		me.onSearch = me.onSearch;
		me.clearListData = me.clearListData;
		me.setParaItem = me.setParaItem;
		//设为默认值
		me.setResetValues = me.setResetValues;
		return me;
	};

	//加载默认设置
	Class.pt.loadDefault = function(systemTypeCode,paraTypeCode,callback){
		var me = this;
		OBJECTID="";
		OBJECTIDNAME="";
		//相关性
		SYSTEMTYPECODE=systemTypeCode;
		//分组类型Code
		PARATYPECODE=paraTypeCode;
		var para = paraform.render();
		var index = layer.load();
		para.load(SELECT_DEFEAULT_URL,paraTypeCode,'',function(data){
			layer.close(index);
			var list = data.value.list || [];
			
			if(list.length==0){//数据不存在时
				me.clearData();
				return;
			}
			DEFALUTLIST = list;
			//排序
			list.sort(me.compare('BPara_DispOrder'));
			para.createControl(list,'#ContentDiv3','Personality');
			
			me.createHtml1();
			callback(list);
		});
	};
	//创建统一管理组件
	Class.pt.createHtml1 = function(){
		var me = this;
		var html ='<div class="layui-btn-container" style="padding-left: 15px;" >'+
		    '<button class="layui-btn  layui-btn-xs layui-hide" lay-submit="" id="Personality_save" lay-filter="Personality_save"><i class="iconfont">&#xe713;</i>&nbsp;保存设置</button>'+
		    '<button class="layui-btn  layui-btn-xs layui-hide" id="Personality_reset2"><i class="iconfont">&#xe615;</i>&nbsp; 设为默认值</button>'+
		    '<button class="layui-btn  layui-btn-xs layui-hide" lay-submit="" id="Personality_batchsave" lay-filter="Personality_batchsave"><i class="iconfont">&#xe713;</i>&nbsp;批量保存</button>'+
		    '</div>';
        document.getElementById("Personality_toolbarbtn").innerHTML =html;
		var html2='<p style="text-align: center;color:blue;font-weight: bold;" id="ObjectName"> </p>';
		$('#Personality').prepend(html2);
	};
	
	//加载表单数据	(业务模块使用，公开方法)
	Class.pt.loadData = function(systemTypeCode,paraTypeCode,iscolose,callback){
		var me = this;
		OBJECTID="";
		OBJECTIDNAME="";
		//相关性
		SYSTEMTYPECODE=systemTypeCode;
		//分组类型Code
		PARATYPECODE=paraTypeCode;
		//保存是否关闭窗体
		if(iscolose)ISCOLOSE = iscolose;
		var para = paraform.render();
		var index = layer.load();
		para.load(SELECT_DEFEAULT_URL,paraTypeCode,'',function(data){
			layer.close(index);
			var list = data.value.list || [];
			
			if(list.length==0){//数据不存在时
				me.clearData();
				return;
			}
			DEFALUTLIST = list;
			//排序
			list.sort(me.compare('BPara_DispOrder'));
			para.createControl(list,'#ContentDiv3','Personality');
			var html =
			    '<div class="layui-btn-container" style="padding-left: 15px;" >'+
			    '<button class="layui-btn  layui-btn-xs layui-hide" lay-submit="" id="Personality_save" lay-filter="Personality_save">保存设置</button>'+
			   	'</div>';
            document.getElementById("Personality_toolbarbtn").innerHTML =html;
			callback(list);
		});
	};

	//加载个性设置,用于显示采用默认值
	Class.pt.onSearch = function(ObjectID,ObjectName){
		var me = this;
		OBJECTID = ObjectID;
		OBJECTIDNAME = ObjectName;
		$('#ObjectName').text(ObjectName);
		var index = layer.load();
		
		//获取类型列表
		me.onLoadSystemParaItem(ObjectID,function(data){
			layer.close(index);
			if(data.success){
				var list = data.value.list || [];
				if(list.length>0)me.btnIsShow(true);
				me.setParaItem(list);
			}else{
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	};
	
	//
	Class.pt.setParaItem = function(list){
		var me = this;
		PERSONALITYDATA = list;
		
		if(PERSONALITYDATA.length>0){
			OBJECTID = PERSONALITYDATA[0].BParaItem_ObjectID;
		    OBJECTIDNAME = PERSONALITYDATA[0].BParaItem_ObjectName;
		}
		
		if(list.length>0)me.btnIsShow(true);
		for(var i=0;i<PERSONALITYDATA.length;i++){
			//隐藏所有采用默认设置按钮
		    $('#Personality_btn_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo).hide();
	    }
		me.setValues(PERSONALITYDATA);
		//显示采用默认按钮
		for(var i=0;i<PERSONALITYDATA.length;i++){
			if(PERSONALITYDATA[i].BParaItem_BPara_ParaValue !=PERSONALITYDATA[i].BParaItem_ParaValue ){
				$('#Personality_btn_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo).parent().removeClass('layui-hide');
				$('#Personality_btn_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo).show();
			}
		}
		me.initListeners();
	};

	
	//获取个性设置数据
	Class.pt.onLoadSystemParaItem = function(ObjectID,callback){
		
		var url  = uxutil.path.ROOT + SELECT_PERSONALITY_URL;
		url += '&systemTypeCode='+SYSTEMTYPECODE+'&paraTypeCode=' + PARATYPECODE;
		url += '&fields=BParaItem_ParaValue,BParaItem_ParaNo,BParaItem_Id,BParaItem_BPara_Id,BParaItem_BPara_ParaValue,BParaItem_BPara_ParaEditInfo,BParaItem_BPara_ParaNo,BParaItem_BPara_CName,BParaItem_ObjectName,BParaItem_ObjectID';
		url += '&where=bparaitem.ObjectID='+ObjectID;

		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	//保存
	form.on('submit(Personality_save)',function(data){
		var objectInfo = [{
			ObjectID:OBJECTID,
			ObjectName:OBJECTIDNAME
		}];
		Class.pt.onSaveClick(data,objectInfo);
	});
	//批量保存
	form.on('submit(Personality_batchsave)',function(data){
		var checkStatus = table.checkStatus('personality_table'),
	        data1 = checkStatus.data;
	    var objectInfo =[];
	    for(var i=0;i<data1.length;i++){
	    	objectInfo.push({
	    		ObjectID:data1[i].BParaItem_ObjectID,
				ObjectName:data1[i].BParaItem_ObjectName,
	    	});
	    }
	    if (objectInfo.length==0){
	    	layer.msg("请勾选需要批量保存的数据行");
	    	return;
	    } 
		Class.pt.onSaveClick(data,objectInfo);
	});	
    //清空表单数据	
	Class.pt.clearData = function(){
		var me = this;
		
		var HTML ='<div class="layui-none" style="padding-left:10px;">暂无数据</div>';
		$('#ContentDiv3').html(HTML);
	};
	
      //按钮显示隐藏
    Class.pt.btnIsShow = function(bo){
    	if(bo){
    		$('#Personality_save').removeClass('layui-hide');
    		$('#Personality_save').show();
    		$('#Personality_reset2').removeClass('layui-hide');
    		$('#Personality_reset2').show();
    		$('#Personality_batchsave').removeClass('layui-hide');
    		$('#Personality_batchsave').show();
    	}else{
    		$('#Personality_save').hide();
    		$('#Personality_reset2').hide();
    		$('#Personality_batchsave').hide();
    	}
    };
     /**@overwrite 获取需要保存的数据*/
	Class.pt.getParams = function(data) {
		var me = this;
		
		var entity = JSON.stringify(data.field).replace(/Personality_/g, "");
	    if(entity) entity = JSON.parse(entity);
		var entityList=[];
		for(var i = 0 ;i<PERSONALITYDATA.length;i++){
			var valType="C";
			var ParaEditInfo = PERSONALITYDATA[i].BParaItem_BPara_ParaEditInfo;
		    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
    		var valTypeStr = ParaEditInfoStr.split('|');
    		//如果存在多个值，截取第一个|前的内容，取出类型
    		if(ParaEditInfo.length>=1)valType = valTypeStr[0];
    		if(valType == 'E')PERSONALITYDATA[i].BParaItem_ParaValue='0';
    	    for(var key  in entity){
    	    	//E类并且存在不存在下拉列表时,BPara_ParaValue=0
				if(key == PERSONALITYDATA[i].BParaItem_BPara_ParaNo){
			
					switch (valType){
						case 'E':
							if(valTypeStr[2])PERSONALITYDATA[i].BParaItem_ParaValue = entity[key];
							else 
							    PERSONALITYDATA[i].BParaItem_ParaValue = entity[key]=="on" ? "1" : "0";
							break;
						case 'S':
						    PERSONALITYDATA[i].BParaItem_ParaValue = entity[key];
						    break; 
						case 'CL':
			    		    //获取下拉内容的最后一个值
//			    	        if(valTypeStr[3]){
//			    	        	var list = valTypeStr[3].split('#') || [];
//			    	            if(list.length>0){
//			    	            	var rulearr = list[list.length-1].split("/");
//			    	            	//1为可手工输入结果
//			    	            	if(rulearr.length>0 && rulearr[1]=='1')PERSONALITYDATA[i].BParaItem_ParaValue = entity['Text_'+key];
//			    	            	else 
//			    	            	   PERSONALITYDATA[i].BParaItem_ParaValue = entity[key];
//			    	            }
//			    	        }else{
//			    	        	PERSONALITYDATA[i].BParaItem_ParaValue = entity[key];
//			    	        }
                            PERSONALITYDATA[i].BParaItem_ParaValue = entity[key];
			    		    break;
						default:
						    PERSONALITYDATA[i].BParaItem_ParaValue = entity[key];
				            break;
					}
				}
    	    }
    	   
    	    var obj ={
				ParaNo : PERSONALITYDATA[i].BParaItem_BPara_ParaNo,
				IsUse:1,
				ParaValue:PERSONALITYDATA[i].BParaItem_ParaValue
			};
			if(PERSONALITYDATA[i].BParaItem_Id)obj.Id=PERSONALITYDATA[i].BParaItem_Id;
			if(PERSONALITYDATA[i].BParaItem_BPara_Id){
				obj.BPara={
	        		Id:PERSONALITYDATA[i].BParaItem_BPara_Id,
	        		DataTimeStamp:[0,0,0,0,0,0,0,0]
	        	};
			}
			if(uxutil.cookie.get(uxutil.cookie.map.USERID)){
	        	obj.OperatorID = uxutil.cookie.get(uxutil.cookie.map.USERID);
	        	obj.Operater =uxutil.cookie.get(uxutil.cookie.map.USERNAME);
	        }
			entityList.push(obj);
		}
		return entityList;
	};
     
	
	//表单保存处理
	Class.pt.onSaveClick = function(data,objectInfo) {
		var me = this;
		var entityList = me.getParams(data);
		if(entityList.length ==0)return;
	
		var entity ={
			objectInfo:JSON.stringify(objectInfo),
			entityList:entityList
		};
		var params = JSON.stringify(entity);
		//显示遮罩层
		var config = {
			type: "POST",
			url: uxutil.path.ROOT +SAVE_PERSONALITY_URL,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				layer.msg("保存成功",{icon:6,time:2000});
				me.onSearch(OBJECTID,OBJECTIDNAME);
                if(ISCOLOSE)parent.layer.closeAll('iframe');
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	};
	
	  //还原
	Class.pt.setValueType = function(valType,row,valTypeStr,obj) {
		var ParaNoID = 'Personality_'+[row.BParaItem_BPara_ParaNo];
		switch (valType){
			case 'E':
			    var ParaValue  =  row.BParaItem_ParaValue=="1" ? "true" :"";
			    if(valTypeStr[2]){//存在下拉列表
			    	//默认值为空时,默认为否
			    	if((!valTypeStr[1] || valTypeStr[1]=='0') && !row.BParaItem_ParaValue){
			    		ParaValue = "0";
			    	}
			   	    else {
			   	    	 ParaValue = row.BParaItem_ParaValue;
			   	    }
			    }
			    obj[ParaNoID] = ParaValue;
				break;
		    case 'S':
			     obj[ParaNoID] = row.BParaItem_ParaValue;
		         if(obj[ParaNoID]=='0')$("input[name='Personality_"+row.BParaItem_BPara_ParaNo+"']").removeAttr('checked');			    
				break;
			case 'RGB':
                obj[ParaNoID] = row.BParaItem_ParaValue;
                //表单赋值(颜色)
				colorpicker.render({
				    elem: '#'+ParaNoID+'color-form',
				    color: obj[ParaNoID],
				    done: function(color){
				       $(this.elem.split('color-form')[0]).val(color);
				    },
				    change: function(color){	
				    	var ParaNo = this.elem.split('color-form')[0].split('#Personality_')[1];
				    	var btn =$('#Personality_btn_'+ParaNo)[0];
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
//  	            	if(rulearr.length>0 && rulearr[1]=='1')obj['Personality_Text_'+row.BParaItem_BPara_ParaNo] = row.BParaItem_ParaValue;
//  	            	else 
//  	            	    obj[ParaNoID] = row.BParaItem_ParaValue;
//  	            }
//  	        }else{
//  	        	obj[ParaNoID] = row.BParaItem_ParaValue;
//  	        }
			    obj[ParaNoID] = row.BParaItem_ParaValue;
    		    break;
			default:
			   obj[ParaNoID] = row.BParaItem_ParaValue;
				break;
		}
		return obj;
	};
		
	  //还原
	Class.pt.setValues = function(list) {
		var me = this;
		var obj = {};
		for(var i=0;i<list.length;i++){
		    var valType="C";
			var ParaEditInfo = list[i].BParaItem_BPara_ParaEditInfo;
		    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
		    
    		var valTypeStr = ParaEditInfoStr.split('|');
    		//如果存在多个值，截取第一个|前的内容，取出类型
    		if(ParaEditInfo.length>=1)valType = valTypeStr[0];
    		obj = me.setValueType(valType,list[i],valTypeStr,obj);

		}
		form.val('Personality',obj);
		form.render();
	};
	
	//重置为默认值
	Class.pt.setResetValues = function(DEFALUTLIST) {
		var me = this;
		var obj = {};
		for(var i=0;i<DEFALUTLIST.length;i++){
			var ParaNoID = 'Personality_'+[DEFALUTLIST[i].BPara_ParaNo];
		    var valType="C";
			var ParaEditInfo = DEFALUTLIST[i].BPara_ParaEditInfo;
		    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
    		var valTypeStr = ParaEditInfoStr.split('|');
    		//如果存在多个值，截取第一个|前的内容，取出类型
    		if(ParaEditInfo.length>=1)valType = valTypeStr[0];
    		//隐藏所有采用默认设置按钮
    		$('#Personality_btn_'+DEFALUTLIST[i].BPara_ParaNo).hide();
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
			         if(obj[ParaNoID]=='0')$("input[name='Personality_"+DEFALUTLIST[i].BPara_ParaNo+"']").removeAttr('checked');			    
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
					    	var ParaNo = this.elem.split('color-form')[0].split('#Personality_')[1];
					    	var btn =$('#Personality_btn_'+ParaNo)[0];
					    	if(this.color!=color){
					    		$(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
					    	}else{
					    		$(btn).addClass("layui-btn-disabled").attr('disabled',true);
					    	}
					    }
					});
					break;
				case 'CL':
//	    		    //获取下拉内容的最后一个值
//	    	        if(valTypeStr[3]){
//	    	        	var list = valTypeStr[3].split('#') || [];
//	    	            if(list.length>0){
//	    	            	var rulearr = list[list.length-1].split("/");
//	    	            	//1为可手工输入结果
//	    	            	if(rulearr.length>0 && rulearr[1]=='1')obj['Personality_Text_'+DEFALUTLIST[i].BPara_ParaNo] = DEFALUTLIST[i].BPara_ParaValue;
//	    	            	else 
//	    	            	    obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
//	    	            }
//	    	        }else{
//	    	        	obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
//	    	        }
				    obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
	    		    break;
				default:
				   obj[ParaNoID] = DEFALUTLIST[i].BPara_ParaValue;
					break;
			}
		}
	
		form.val('Personality',obj);
		form.render();
		
	};
	Class.pt.getValues = function(rowobj,value){
		var me = this;
		var obj={};
		var valType="C";
		var ParaEditInfo = rowobj.BParaItem_BPara_ParaEditInfo;
	    var ParaEditInfoStr = ParaEditInfo.toUpperCase();
		var valTypeStr = ParaEditInfoStr.split('|');
		//如果存在多个值，截取第一个|前的内容，取出类型
		if(ParaEditInfo.length>=1)valType = valTypeStr[0];
		switch (valType){
			case 'E':
			    var ParaValue  = value =="1" ? true :false;
			    if(valTypeStr[2]) ParaValue = value=="1" ? "1" :"0";//存在下拉列表
			    obj['Personality_'+rowobj.BParaItem_BPara_ParaNo] = ParaValue;
				break;
			case 'S':
			    obj['Personality_'+rowobj.BParaItem_BPara_ParaNo] = value;
			    if(obj['Personality_'+rowobj.BParaItem_BPara_ParaNo]=='0')$("input[name='Personality_"+rowobj.BParaItem_BPara_ParaNo+"']").removeAttr('checked');			    
    		break;
    		case 'RGB':
    		    obj['Personality_'+rowobj.BParaItem_BPara_ParaNo] = value;
    		     //渲染颜色
				colorpicker.render({
				    elem: '#Personality_'+rowobj.BParaItem_BPara_ParaNo+'color-form',
					color: value,
					done: function(color){
				       $(this.elem.split('color-form')[0]).val(color);
				    },
				    change: function(color){	
				    	var ParaNo = this.elem.split('color-form')[0].split('#Personality_')[1];
				    	var btn =$('#Personality_btn_'+ParaNo)[0];
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
//  	            	if(rulearr.length>0 && rulearr[1]=='1')obj['Personality_Text_'+rowobj.BParaItem_BPara_ParaNo] = value;
//  	            	else 
//  	            	    obj['Personality_'+rowobj.BParaItem_BPara_ParaNo] = value;
//  	            }
//  	        }else{
//  	        	obj['Personality_'+rowobj.BParaItem_BPara_ParaNo] = value;
//  	        }
			    obj['Personality_'+rowobj.BParaItem_BPara_ParaNo] = value;
    		    break;
			default:
			    obj['Personality_'+rowobj.BParaItem_BPara_ParaNo] = value;
				break;
		}
		return obj;
	};
	//采用出厂设置监听
	Class.pt.initListeners = function(){
		var me = this;
		  
        //高度
        $("#Personality").css("height", ($(".cardHeight").height() - 100) + "px");//设置表单容器高度
        // 窗体大小改变时，调整高度显示
    	$(window).resize(function() {
		    $("#Personality").css("height", $('.cardHeight').height()-100+ "px");
    	});
				    	
		//设为默认值 
		$('#Personality_reset2').on('click',function(){
	    	var me = this;
	    	Class.pt.setResetValues(DEFALUTLIST);
		});
		for(var i=0;i<PERSONALITYDATA.length;i++){
			var ParaNo = PERSONALITYDATA[i].BParaItem_BPara_ParaNo;
			var btnStr = 'Personality_btn_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo; 
	     	var btnParaNo = btnStr.split('Personality_btn_')[1];
			if(btnParaNo  == PERSONALITYDATA[i].BParaItem_BPara_ParaNo){ //按钮的联动
				$('#Personality_btn_'+ParaNo).on('click',function(){
					var id = $(this).attr('id').split('Personality_btn_')[1];
					for(var j=0;j<PERSONALITYDATA.length;j++){
						if(PERSONALITYDATA[j].BParaItem_BPara_ParaNo ==id ){
							var obj = me.getValues(PERSONALITYDATA[j],PERSONALITYDATA[j].BParaItem_BPara_ParaValue);
							form.val('Personality',obj);
		                    form.render();
                            $(this).addClass("layui-btn-disabled").attr('disabled',true);
						}
					}
				});
			}
			//checkbox的联动
            form.on('checkbox(Personality_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo+')', function(data){
            	var id = $(this).attr('name').split('Personality_')[1];
            	me.showBtnByValue(id,data.elem.checked);
			});    
			//radio的联动
            form.on('radio(Personality_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo+')', function(data){
            	var id = $(this).attr('name').split('Personality_')[1];
            	me.showBtnByValue(id,data.value);
			}); 
			//下拉的联动
		    form.on('select(Personality_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo+')', function(data){
            	var id = $(data.elem).attr('name').split('Personality_')[1];
            	//下拉可编辑同步输入框
            	if($(data.elem).prev().attr('id')){
            		for(var j=0;j<PERSONALITYDATA.length;j++){
						if(PERSONALITYDATA[j].BParaItem_BPara_ParaNo == $(data.elem).prev().attr('id').split('Personality_Text_')[1]){
							var textid = $(data.elem).prev().attr('id');
							$('#'+textid).val(data.value);
							var obj = me.getValues(PERSONALITYDATA[j],PERSONALITYDATA[j].BParaItem_BPara_ParaValue);
							var btn =$('#Personality_btn_'+PERSONALITYDATA[j].BParaItem_BPara_ParaNo)[0];
							if(obj['Personality_Text_'+PERSONALITYDATA[j].BParaItem_BPara_ParaNo] == data.value)$(btn).addClass("layui-btn-disabled").attr('disabled',true);
							else
							    $(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
						}
					}
            	}
            	else
            	     me.showBtnByValue(id,data.value);
			}); 

			//文本域联动
			$('input[name="Personality_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo+'"]').on('input propertychange', function() {//监听文本框\
				var id = $(this).attr('name').split('Personality_')[1];
				me.showBtnByValue(id,$(this).val());
			});
			//文本域联动(文本框没设置ID)
			$('#Personality_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo).on('input propertychange', function() {//监听文本框
                var id = $(this).attr('id').split('Personality_')[1];
                me.showBtnByValue(id,$(this).val());
	        });
            //下拉可编辑输入框只输入不选择联动
			$('#Personality_Text_'+PERSONALITYDATA[i].BParaItem_BPara_ParaNo).on('input propertychange', function() {//监听文本框
                var id = $(this).attr('id').split('Personality_Text_')[1];
     
                for(var j=0;j<PERSONALITYDATA.length;j++){
					if(PERSONALITYDATA[j].BParaItem_BPara_ParaNo ==id ){
						var obj = me.getValues(PERSONALITYDATA[j],PERSONALITYDATA[j].BParaItem_BPara_ParaValue);
						var btn =$('#Personality_btn_'+PERSONALITYDATA[j].BParaItem_BPara_ParaNo)[0];
						if(obj['Personality_Text_'+PERSONALITYDATA[j].BParaItem_BPara_ParaNo] == $(this).val())$(btn).addClass("layui-btn-disabled").attr('disabled',true);
						else
						    $(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
					}
				}
	        });
		}
	};
	//根据输入值联动显示隐藏按钮
    Class.pt.showBtnByValue = function(id,value){
		var me =  this;
		if(OBJECTID==''){
			layer.msg('请先添加个性设置');
			return;
		}
		for(var j=0;j<PERSONALITYDATA.length;j++){
			if(PERSONALITYDATA[j].BParaItem_BPara_ParaNo ==id ){
				var obj = me.getValues(PERSONALITYDATA[j],PERSONALITYDATA[j].BParaItem_BPara_ParaValue);
				var btn =$('#Personality_btn_'+PERSONALITYDATA[j].BParaItem_BPara_ParaNo)[0];
				if(obj['Personality_'+PERSONALITYDATA[j].BParaItem_BPara_ParaNo] == value)$(btn).addClass("layui-btn-disabled").attr('disabled',true);
				else
				   $(btn).removeClass("layui-btn-disabled").removeAttr('disabled',true);
			}
		}
    };
    //排序比较
	Class.pt.compare = function(property){
	    return function(a,b){
	        var value1 = a[property];
	        var value2 = b[property];
	        return value1 - value2;
	    }
	};
	 //清空列表数据	
	Class.pt.clearListData = function(){
		var me = this;
		OBJECTID="";
		OBJECTIDNAME="";
		me.btnIsShow(false);
		me.setResetValues(DEFALUTLIST);
		
	};
    //暴露接口
	exports('personalityform',personalityform);
});	