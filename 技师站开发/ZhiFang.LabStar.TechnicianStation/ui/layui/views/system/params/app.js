/**
	@name：参数
	@author：liangyl
	@version 2020-02-04
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table',
    paraform:'views/system/params/form',
    defalutform:'views/system/params/defalut',
    personalityform:'views/system/params/personality',
    commonzf:'modules/common/zf'
}).use(['uxutil','element','defalutform','personalityform','table','commonzf'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		element = layui.element,
		commonzf = layui.commonzf,
		paraform = layui.paraform,
		defalutform = layui.defalutform,
		personalityform = layui.personalityform;
   
    var params = uxutil.params.get(true);
    
    //类型编码(站点类型)
    var OBJECTID = params.OBJECTID;
     //类型编码名称(站点类型)
    var OBJECTNAME =params.OBJECTNAME;
    //参数分组
    var PARATYPECODE = params.PARATYPECODE;
    //参数相关性
    var SYSTEMTYPECODE = params.SYSTEMTYPECODE;
    //是否保存后关闭
    var ISCOLOSE = params.ISCOLOSE=='true' ? true : false;
    
	 //查询系统个性参数设置
    var SELECT_PERSONALITY_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemParaItem?isPlanish=true"; 
      //获取默认设置参数服务
    var SELECT_DEFEAULT_URL = "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?isPlanish=true"; 
     //删除个性参数
    var DEL_URL =uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DeleteSystemParaItem';
    //复制个性参数
    var COPY_PERSONALITY_URL = uxutil.path.ROOT + '/ServerWCF/LS_UDTO_SetParaItemDefaultValue.svc/LS_UDTO_AddCopySystemParaItem';
     //保存系统个性参数设置
    var SAVE_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemParaItem';
    //个性设置数组
    var PARAITEMDATA = [];
	//默认设置数组
	var DEFALUTDATA=[];
	
	//根据站点类型和参数分组查询个性设置
	function onLoadData(callback){
		var url  = uxutil.path.ROOT + SELECT_PERSONALITY_URL;
		url += '&systemTypeCode='+SYSTEMTYPECODE+'&paraTypeCode=' + PARATYPECODE;
		url += '&fields=BParaItem_ParaValue,BParaItem_ParaNo,BParaItem_Id,BParaItem_BPara_Id,BParaItem_BPara_ParaValue,BParaItem_BPara_ParaEditInfo,BParaItem_BPara_ParaNo,BParaItem_BPara_CName,BParaItem_ObjectName,BParaItem_ObjectID';
		url += '&where=bparaitem.ObjectID='+OBJECTID;

		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};
	//参数加载
	function loadDefalutData(callback){
		var url  = uxutil.path.ROOT + SELECT_DEFEAULT_URL;
		var fields='BPara_ParaNo,BPara_CName,BPara_TypeCode,BPara_TypeCode,BPara_TypeCode,BPara_TypeCode,BPara_ParaType,'+
            'BPara_ParaDesc,BPara_ParaEditInfo,BPara_SystemCode,BPara_ShortCode,BPara_BVisible,BPara_BVisible,BPara_IsUse,BPara_ParaValue,BPara_Id,BPara_DispOrder';
		url += '&paraTypeCode='+PARATYPECODE+'&fields='+fields;
        uxutil.server.ajax({
			url:url,
			async: false
		},function(data){
			callback(data);
		});
	};
	
	//默认设置
	var form2_Ind  = defalutform.render({});
	//个性设置
	var form3_Ind  = personalityform.render({});
	
	function onSearchTab(CURRTABINDEX){	
		//查询当前页签
		switch (CURRTABINDEX){
			case 0://默认设置
			    form2_Ind.loadData(PARATYPECODE,ISCOLOSE);
				break;
			default: //个性设置
			    if(PARAITEMDATA.length>0){
			    	isAdd(false);
			    	OBJECTNAME=PARAITEMDATA[0].BParaItem_ObjectName;
			        $("#ObjectName").text(PARAITEMDATA[0].BParaItem_ObjectName);
			    	form3_Ind.loadData(SYSTEMTYPECODE,PARATYPECODE,ISCOLOSE,function(list){
			    		DEFALUTDATA = list;
	                    form3_Ind.setParaItem(PARAITEMDATA);
				    });	
			    }else{
			    	isAdd(true);
			    	form3_Ind.clearData();	
			    }
				break;
		}
	}
	element.on('tab(paratabs)', function(obj){
		var CURRTABINDEX = obj.index;
        onSearchTab(CURRTABINDEX);
    });
    //新增设置
	$('#addparaitem').on('click',function(){
		//加载默参数设置
		var index = layer.load();
    	loadDefalutData(function(data){
    		layer.close(index);
    		if (data.success) {
    			var list = data.value.list || [];
    			DEFALUTDATA = list;
    			onSaveParaitem();
    		}else{
    			layer.msg(data.msg,{ icon: 5, anim: 6 });
    		}
    	});
//		layer.open({
//          type: 2,
//          area: ['400px', '380px'],
//          fixed: false,
//          maxmin: false,
//          title:'新增设置',
//          content: 'add.html?systemTypeCode='+SYSTEMTYPECODE+'&objectid='+OBJECTID,
//          cancel: function (index, layero) {
//	        	parent.layer.closeAll('iframe');
//          }
//      });
	});
	//删除当前设置
	$('#delparaitem').on('click',function(){
		var objectInfo = [{
			ObjectID:OBJECTID,
			ObjectName:OBJECTNAME,
		}];
		if (objectInfo.length==0) return;
	
    	layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
			var params = JSON.stringify({objectInfo:JSON.stringify(objectInfo)});
			var config = {
				type: "POST",
				url:DEL_URL,
				data: params
			};
			var index = layer.load();
			uxutil.server.ajax(config, function(data) {
				layer.close(index);
				if (data.success) {
					layer.msg("删除成功",{icon:6,time:2000});
	                //清空面板内容，
	                form3_Ind.clearData();
	                PARAITEMDATA=[];
			    	isAdd(true);
				} else {
					layer.msg(data.msg,{ icon: 5, anim: 6 });
				}
			});
		});
	});
	//复制参数
	$('#copyparaitem').on('click',function(){
		layer.open({
            type: 2,
            area: ['400px', '380px'],
            fixed: false,
            maxmin: false,
            title:'复制参数',
            content: 'copy.html?systemTypeCode='+SYSTEMTYPECODE+'&paraTypeCode='+PARATYPECODE+'&objectid='+OBJECTID+'&objectname='+OBJECTNAME,
            cancel: function (index, layero) {
	        	parent.layer.closeAll('iframe');
            }
        });
	});
	//设为默认值
	$('#defalutparaitem').on('click',function(){
		form3_Ind.setResetValues(DEFALUTDATA);
	});
	//初始化
    function init(){
        var msg="";
        if(!OBJECTID)msg +='类型编码不能为空!</br>';
    	if(!PARATYPECODE)msg +='参数字典类名不能为空!</br>';
    	if(!SYSTEMTYPECODE)msg +='系统相关性不能为空!</br>';
    
    	if(msg){
    		layer.msg(msg);
    		return;
    	}
    	
    	commonzf.classdict.init('Para_SystemType',function(){
	   	    if(!commonzf.classdict.Para_SystemType){
		   		layer.alert('未获取到系统相关性')
    			return;
			}
	   	    var info = getClassInfoByKeyAndValue('Id',SYSTEMTYPECODE);
	   	    var sitename = '当前'+info.Name;
	   	    sitename = sitename.replace('相关',''); 
            $("#siteName").text(sitename+':');
	   	    $("#ObjectName").text(OBJECTNAME);
	    	 //tab高度
	        $(".cardHeight").css("height", ($(window).height() - 50) + "px");//设置表单容器高度
	        // 窗体大小改变时，调整高度显示
	    	$(window).resize(function() {
				 //表单高度
			    $(".cardHeight").css("height", ($(window).height() - 50) + "px");//设置表单容器高度
	    	});
	    	var index = layer.load();
	    	//判断有无个性设置
	        onLoadData(function(data){
	        	layer.close(index);
	        	if (data.success) {
					var list = data.value.list || [];
					PARAITEMDATA = list;
					//已设置个性参数
					if(list.length>0){
						element.tabChange('paratabs', 'PersonalitySet');
					}
					else{
						 element.tabChange('paratabs', 'DefaultSet');
					}
				} else {
					layer.msg(data.msg,{ icon: 5, anim: 6 });
				}
	        });
		});
    }
    //新增设置保存
    function onSaveParaitem(){
    	if(!OBJECTID){
			layer.msg('参数OBJECTID不能为空');
			return;
		}
		if(DEFALUTDATA.length==0){
			layer.msg('没有参数数据不能保存');
			return;
		}
		var entityList=[];
		for(var i=0;i<DEFALUTDATA.length;i++){
			var obj ={
				ParaNo : DEFALUTDATA[i].BPara_ParaNo,
				IsUse:1,
				ParaValue:DEFALUTDATA[i].BPara_ParaValue
			};
			if(DEFALUTDATA[i].BPara_Id){
				obj.BPara={
	        		Id:DEFALUTDATA[i].BPara_Id,
	        		DataTimeStamp:[0,0,0,0,0,0,0,0]
	        	};
			}
			if(uxutil.cookie.get(uxutil.cookie.map.USERID)){
	        	obj.OperatorID = uxutil.cookie.get(uxutil.cookie.map.USERID);
	        	obj.Operater =uxutil.cookie.get(uxutil.cookie.map.USERNAME);
	        }
			entityList.push(obj);
		}
		var objectInfo =[];
    	objectInfo.push({
    		ObjectID:OBJECTID,
			ObjectName:OBJECTNAME,
    	});
		var params={
			objectInfo:JSON.stringify(objectInfo),
			entityList:entityList
		};
		if (!params) return;
		params = JSON.stringify(params);
	
		var config = {
			type: "POST",
			url:SAVE_URL,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				layer.msg("保存成功",{icon:6,time:2000});
                loadPersonality();
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
    }
    //根据类名+字典内容(key+value)获取字典内容
	function getClassInfoByKeyAndValue(key,value){
		var	classInfo = commonzf.classdict.Para_SystemType,
			data = null;
		for(var i in classInfo){
			if(classInfo[i][key] == value){
				data = classInfo[i];
				break;
			}
		}
		return data;
	}
    //传递参数给子窗体
    function afterUpdate1(list){
    	var index = layer.load();
    	//加载默参数设置
    	loadDefalutData(function(data){
    		layer.close(index);
    		if (data.success) {
    			var list = data.value.list || [];
    			DEFALUTDATA = list;
    		}else{
    			layer.msg(data.msg,{ icon: 5, anim: 6 });
    		}
    	});
		return DEFALUTDATA;
    }
    //从父窗体传递给子窗体个性设置列表值
    function afterUpdate2(){
		return PARAITEMDATA;
    }

	//复制参数成功后回调
	function afterCopyUpdate(data1){
		if(data1.success)loadPersonality();
	}
	//成功回调加载个性设置
	function loadPersonality(){
		var index = layer.load();
    	//加载个性设置
        onLoadData(function(data){
        	layer.close(index);
        	if (data.success) {
				var list = data.value.list || [];
				PARAITEMDATA = list;
				isAdd(false);
                $("#ObjectName").text(OBJECTNAME);			    	//创建默认设置页签内容
		    	form3_Ind.loadData(SYSTEMTYPECODE,PARATYPECODE,ISCOLOSE,function(list){
		    		DEFALUTDATA = list;
	                form3_Ind.setParaItem(PARAITEMDATA);
			    });
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
        });
	}
	function isAdd(bo){
		if(bo){
		    $('#addparaitem').removeClass("layui-btn-disabled").removeAttr('disabled',true);
		  	$('#copyparaitem').removeClass("layui-btn-disabled").removeAttr('disabled',true);
            $('#delparaitem').addClass("layui-btn-disabled").attr('disabled',true);
		  	$('#defalutparaitem').addClass("layui-btn-disabled").attr('disabled',true);
		}else{
			$('#addparaitem').addClass("layui-btn-disabled").attr('disabled',true);
		  	$('#copyparaitem').addClass("layui-btn-disabled").attr('disabled',true);
		  	$('#delparaitem').removeClass("layui-btn-disabled").removeAttr('disabled',true);
	    	$('#defalutparaitem').removeClass("layui-btn-disabled").removeAttr('disabled',true);
		}
	}
	window.afterUpdate1 = afterUpdate1;
	window.afterUpdate2 = afterUpdate2;
	window.afterCopyUpdate = afterCopyUpdate;
	
	//初始化
    init();
});