/**
	@name：通讯控制参数
	@author：liangyl
	@version 2020-04-07
 */
layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','element','form','upload'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		element = layui.element,
		upload = layui.upload,
		form = layui.form;
    //数据查询URL
    SELECT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?isPlanish=true";
    //数据新增URL
    ADD_URL = uxutil.path.ROOT +"/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBEquip";
    //数据修改URL
    EDIT_URL = uxutil.path.ROOT +"/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipByField";
    //导出URL下载仪器参数文件
    EXPORT_URL = uxutil.path.ROOT +"/ServerWCF/LabStarCommService.svc/LS_UDTO_DownLoadEquipCommParaFile";
     //导入URL
    IMPORT_URL = uxutil.path.ROOT +"/ServerWCF/LabStarCommService.svc/LS_UDTO_UpLoadEquipCommParaFile";
    //当前激活页签
    TABNAME="CommControlChar";
    
    var params = uxutil.params.get(true);
    //仪器ID
    EQUIPID=params.EQUIPID;
    //通讯参数控制符内容
    COMMPARA=[];
    
	element.on('tab(comparatabs)', function(obj){
	    switch (obj.index){
	    	case 1:
	    	    TABNAME="CommCheck";
	    		break;
	    	case 2:
	    	    TABNAME="CommTransferFrame";
	    		break;
	    	case 3:
	    	    TABNAME="CommTimeInterval";
	    		break;
	    	default:
	    	   TABNAME="CommControlChar";
	    		break;
	    }
	    onSearch(COMMPARA);
    });
    
    //保存
    $('#save').on('click',function(){
    	//获取通讯控制符页签内容
    	var ControlCharParams = getControlCharParams();
    	//获取通讯校验页签内容
    	var CheckParams = getCommCheckParams();
    	//获取传输帧参数页签内容
    	var TransferFrameParams = getCommTransferFrameParams();
    	//获取通讯时间间隔控制页签内容
    	var TimeIntervalParams = getTimeIntervalParams();
    	
    	var obj ={
    		Id:EQUIPID,
    		CommPara:JSON.stringify({
	    		Para_CommControlChar:ControlCharParams,
	    		Para_CommCheck:CheckParams,
	    		Para_CommTransferFrame:TransferFrameParams,
	    		Para_CommTimeInterval:TimeIntervalParams
	    	})
    	};
		var params = JSON.stringify({entity:obj,fields :'Id,CommPara'});
		//显示遮罩层
		var config = {
			type: "POST",
			url: EDIT_URL,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				layer.msg("保存成功",{icon:6,time:2000});
				//数据加载
                loadData();
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
    });
    function getEntity(obj,tabname){
    	var value = obj.attr('data');
    	if (value && typeof (value) === "string") {
            if (isNaN(value)) {
                value = value.replace(/\\/g, '&#92');
                value = value.replace(/[\r\n]/g, '<br />');
                value = eval("(" + value + ")");
            } else {
                value = value + "";
            }
        }
    	value.Value = obj.val(); //控制字符
    	//通讯控制符时,常用控制字符，默认和Value相同
    	if(tabname == 'ControlChar')value.CommonValue = obj.val();//常用控制字符，默认和Value相同
    	value.DispOrder = "0";
    	value.IsUse = 1;
		return value;
    }
    //获取通讯控制符页签内form的obj
    function getControlCharParams(){
    	var list = [];
    	$('#ControlChar').find('input').each(function(){
    		if($(this)[0].name.length>0){
    			var obj = getEntity($(this),'ControlChar');
    		    list.push(obj);
    		}
		});
		return list;
    }
     //获取通讯校验页签内form的obj
    function getCommCheckParams(){
    	var list = [];
    	$('#CommCheck').find('input').each(function(){
    		if($(this)[0].name.length>0){
	    		var obj = getEntity($(this),'CommCheck');
	    		list.push(obj);
    		}
		});
		return list;
    }
     //获取传输帧参数页签内form的obj
    function getCommTransferFrameParams(){
    	var list = [];
    	$('#CommTransferFrame').find('input').each(function(){
    		if($(this)[0].name.length>0){
	    		var obj = getEntity($(this),'CommTransferFrame');
	    		list.push(obj);
    		}
		});
		return list;
    }
    //获取通讯时间间隔控制（双向）form的obj
    function getTimeIntervalParams(){
    	var list = [];
    	$('#CommTimeInterval').find('input').each(function(){
    		if($(this)[0].name.length>0){
	    		var obj = getEntity($(this),'CommTimeInterval');
	    		list.push(obj);
    		}
		});
		return list;
    }
    
    //导入
    upload.render({
	    elem: '#import',
	    url: IMPORT_URL ,//改成您自己的上传接口
	    auto: true,
        accept: 'file' , //普通文件
        data: {equipID:EQUIPID},
	    done: function(res){
	        layer.msg("文件上传成功",{icon:6,time:2000});
			//数据加载
            loadData();
	    }
	});

    //导出
    $('#export').on('click',function(){
	    var url = EXPORT_URL+'?equipID='+EQUIPID+'&operateType=0';
	    window.open(url);
    });
    
    //按仪器id加载数据
	function loadDataByID(id,callback){
		var url  = SELECT_URL+'&fields=LBEquip_Id,LBEquip_CommPara';
		if(!id)return;
		var where = "";
		if(id)where = "lbequip.Id="+id;
		if(where)url+="&where="+where;
        var index = layer.load();
        uxutil.server.ajax({
			url:url
		},function(data){
			layer.close(index);
			if (data.success) {
			    var list = data.value.list || [];
			    callback(list);
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	}
	//数据加载
	function loadData(){
    	if(!EQUIPID){
        	layer.msg("仪器ID不能为空");
        	parent.layer.closeAll('iframe');
        	return;
        }
    	loadDataByID(EQUIPID,function(list){
        	if(list.length>0){
        		$('#IsLoad1').val('');
        		$('#IsLoad2').val('');
        		$('#IsLoad3').val('');
        		$('#IsLoad4').val('');
        		var Id = list[0].LBEquip_Id;
        		COMMPARA = list[0].LBEquip_CommPara;
        		onSearch(COMMPARA);
        	}
        });
    }
    function onSearch(CommPara){
    	if (CommPara && typeof (CommPara) === "string") {
            if (isNaN(CommPara)) {
                CommPara = CommPara.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                CommPara = CommPara.replace(/\\"/g, '&quot;');
                CommPara = CommPara.replace(/\\/g, '\\\\');
                CommPara = CommPara.replace(/&quot;/g, '\\"');
                CommPara = eval("(" + CommPara + ")");
            } else {
                CommPara = CommPara + "";
            }
        }
    	switch (TABNAME){
	    	case "CommCheck": //校验
	    	    if($('#IsLoad2').val())break;
	    	    $('#CommCheck').find('input').each(function(){
	    	    	setValues(CommPara.Para_CommCheck,$(this));
	    	    	$('#IsLoad2').val('1');
				});
	    		break;
	    	case "CommTransferFrame": //传输帧参数
	    	    if($('#IsLoad3').val())break;
	    	    $('#CommTransferFrame').find('input').each(function(){
	    	    	setValues(CommPara.Para_CommTransferFrame,$(this));
	    	    	$('#IsLoad3').val('1');
				});
	    		break;
	    	case "CommTimeInterval": //时间间隔
	    	    if($('#IsLoad4').val())break;
	    	    $('#CommTimeInterval').find('input').each(function(){
	    	    	setValues(CommPara.Para_CommTimeInterval,$(this));
	    	    	$('#IsLoad4').val('1');
				});
	    		break;
	    	default:  //通讯控制符
	    	    if($('#IsLoad1').val())break;
	    	    $('#ControlChar').find('input').each(function(){
	    	    	setValues(CommPara.Para_CommControlChar,$(this));
	    	    	$('#IsLoad1').val('1');
				});
	    		break;
	    }
    }
    //循环还原值
    function setValues(list,com){
    	for(var i=0;i<list.length;i++){
    		if(list[i].CName == com[0].name){
    			com.val(list[i].Value);
    			break;
    		}
    	}
    }
	//联动监听
    function initListeners(){
    	//下拉框监听 同步输入框
    	for(var i = 0;i< $('.inputSelect').length;i++){
	        form.on("select("+$('.inputSelect')[i].name+"_Filter)", function (data) {
	        	var inputText = $(data.elem).prev()[0];
	        	$(inputText).val(data.value);
		    });
    	}
    }
	//初始化
    function init(){
    	 //tab高度
        $(".cardHeight").css("height", ($(window).height() - 95) + "px");//设置表单容器高度
        // 窗体大小改变时，调整高度显示
    	$(window).resize(function() {
			 //表单高度
		    $(".cardHeight").css("height", ($(window).height() - 95) + "px");//设置表单容器高度
    	});
    	//数据加载
        loadData();
    	initListeners();
    }
    //初始化
    init();
   
});