/**
	@name：短语表单
	@author：liangyl
	@version 2019-10-30
 */
layui.extend({
	uxutil: 'ux/util',
}).use(['form','uxutil'],function(){

	var $=layui.$,
		uxutil = layui.uxutil,
		form = layui.form;

	//变量	
    var config ={
    	formtype:'add',
		PK:null
    };
     //外部参数
	var PARAMS = uxutil.params.get(true);
	//短语新增服务地址
	var ADD_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase";
	//短语修改服务地址
	var EDIT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhraseByField";
	//短语查询服务地址
	var SELECT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseById?isPlanish=true";
    //短语查询服务地址
	var DEL_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase";
	//样本类型查询服务地址
	var SELECT_SAMPLETYPE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true";
    //提取中文字符串拼音字头
    var SELECT_PINYIN_URL = uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou';

	/**创建数据字段*/
	function getStoreFields() {
		var fields = [];
		$(":input").each(function(){ 
			if(this.name)fields.push(this.name)
	    });
		return fields;
	}
	//加载表单数据	
	function loadDatas(id,callback){
		var url = SELECT_URL + '&id=' + id+
		'&fields='+getStoreFields().join(',');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				callback(data);
			}else{
				layer.msg(data.msg);
			}
		});
	}
	
	 /**@overwrite 返回数据处理方法*/
	function changeResult(data){
		var list =  JSON.parse(data);
	    if(list.LBPhrase_IsUse=="false")list.LBPhrase_IsUse="";
	    config.currData=list;
		return list;
	}

    //样本类型-下拉框加载
	function initSampleType(){
		var url = SELECT_SAMPLETYPE_URL+ '&lbsampletype.IsUse=1'+
		'&fields=LBSampleType_CName,LBSampleType_Id';
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
				var tempAjax = "<option value=''>请选择</option>";
                if(!value)return;
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSampleType_Id + "'>" + value.list[i].LBSampleType_CName + "</option>";
                    $("#LBPhrase_SampleTypeID").empty();
                    $("#LBPhrase_SampleTypeID").append(tempAjax);
                    
                }
                form.render('select');//需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	}
    /**@overwrite 获取新增的数据*/
	function getAddParams(data) {
		var entity = JSON.stringify(data).replace(/LBPhrase_/g, "");
		if(entity) entity = JSON.parse(entity);
		if(entity.IsUse) entity.IsUse = entity.IsUse ? 1 :0;
		if(!entity.DispOrder)delete entity.DispOrder;
		if(!entity.SampleTypeID)delete entity.SampleTypeID;
		if(!entity.Id)delete entity.Id;
		entity.ObjectID=PARAMS.OBJECTID;
		entity.ObjectType=PARAMS.OBJECTTYPE;
		entity.TypeName=PARAMS.CODE;
        entity.TypeCode=PARAMS.TYPECODE; 
		entity.PhraseType='ItemPhrase';
		return {
			entity: entity
		};
		return entity;
	}
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var entity = getAddParams(data);
		entity.fields = 'Id,IsUse,SampleTypeID,CName,Shortcode,PinYinZiTou,DispOrder,Comment';
		if (data["LBPhrase_Id"])
			entity.entity.Id = data["LBPhrase_Id"];
		return entity;
	}
	
	//表单保存处理
	function onSaveClick (data) {
	  	var url = PARAMS.FORMTYPE == 'add' ? ADD_URL : EDIT_URL;
		var params = PARAMS.FORMTYPE == 'add' ? getAddParams(data.field) : getEditParams(data.field);
		if (!params) return;
		var id = params.entity.Id;
		params = JSON.stringify(params);
		//显示遮罩层
		var obj = {
			type: "POST",
			url: url,
			data: params
		};
		uxutil.server.ajax(obj, function(data) {
			//隐藏遮罩层
			if (data.success) {
				id = PARAMS.FORMTYPE == 'add' ? data.value.id : "";
				parent.layer.closeAll('iframe');
				parent.afterPhraseUpdate(id,PARAMS.FORMTYPE);
			} else {
				layer.msg(data.msg,{ icon: 5, anim: 6 });
			}
		});
	}
	  //获得拼音字头
    function getPinYinZiTou(chinese, callBack) {
        if (chinese == "") {
            if (typeof (callBack) == "function") {
                callBack(chinese);
            }
            return;
        }
        $.ajax({
            type: "get",
            url:SELECT_PINYIN_URL +'?chinese=' + encodeURI(chinese),
            dataType: 'json',
            success: function (res) {
                if (res.success) {
                    if (typeof (callBack) == "function") {
                        callBack(res.ResultDataValue);
                    }
                } else {
                    layer.msg("拼音字头获得失败！", { icon: 5, anim: 6 });
                }
            }
        });
    }
    function initFilterListeners(){
    	//监听名称输入同步拼音字头
	    $('#LBPhrase_CName').bind('input propertychange', function () {
	        getPinYinZiTou($(this).val(), function (str) {
	            if ($("#LBPhrase_Shortcode").val() == $("#LBPhrase_PinYinZiTou").val()) {
	                $("#LBPhrase_Shortcode").val(str);
	            }
	            $("#LBPhrase_PinYinZiTou").val(str);
	        });
	    });
	    //保存
		form.on('submit(save)',function(data){
			onSaveClick(data);
		});
    }
	//初始化
	function init(){
		initSampleType();
		if(PARAMS.FORMTYPE=='add'){
			$("#LBPhrase_DispOrder").val(PARAMS.DISPORDER);
		}else{
			//加载数据
			loadDatas(PARAMS.PK,function(data){
				form.val('LBPhrase',changeResult(data.ResultDataValue));
			});
		}
	}
	init();
	initFilterListeners();
	
});