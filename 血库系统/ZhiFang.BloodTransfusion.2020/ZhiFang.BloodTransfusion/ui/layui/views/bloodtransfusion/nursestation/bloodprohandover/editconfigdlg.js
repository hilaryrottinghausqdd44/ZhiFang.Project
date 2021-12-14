layui.extend({
	uxutil: 'ux/util'	
}).use(['form', 'uxutil', 'layer', 'util', 'laydate'], function(){
	"use strict";
	var $ = layui.jquery;
	var util = layui.util;
	var form = layui.form; 
	var layer = layui.layer;
	var laydate = layui.laydate;
    var uxutil = layui.uxutil;
    var editconfigdlg = {
     	config:{
    		fields:['BDict_Id', 'BDict_CName'],
		  	bloodIntegrityEleId: '#integrity_select',  //完整性
			bloodAppearanceEleId: '#appearance_select', //外观 
			params:{where:"", fields: "", page:0, limit:1000, isPlanish:true}, //table 690
			dictUrl: uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictByHQL'
    	}
    };
    
  	//获取传入血袋登记ID
    var urlParams = uxutil.params.get() || {};
    var bagopertime = urlParams["bagopertime"] ? urlParams["bagopertime"] : "";
    var appearance_bdict_id = urlParams["appearance_bdict_id"] ? urlParams["appearance_bdict_id"] : "";
    var integrity_bdict_id = urlParams["integrity_bdict_id"] ? urlParams["integrity_bdict_id"] : "";
    
    //添加doc option node
    editconfigdlg.addselectNode = function(data, eleId){
    	var me = this;
    	var html = '';
    	var name = '';
    	var DCId = '';
    	data = data || [];
    	for(var i=0; i < data.length; i++){
    		DCId = data[i][me.config.fields[0]];
    		name = data[i][me.config.fields[1]];
    		html = html + '<option value="' + DCId + '">' + name  +'</option>';
    	};
    	if (html){
    		var selectele = $(eleId).empty().append(html);
    	};
    	
    };
    
    //获取字典编码数据
    editconfigdlg.getBDict = function(DictTypeCode, callback){
    	var me = this;
    	var where = "bdict.BDictType.DictTypeCode='" + DictTypeCode +"'"; 
    	var fields =  me.config.fields.join(',');
		var data = me.config.params || {};
		data["fields"] = fields;
		data["where"] = where;
    	var config= {
    		type: 'get',
        	url: me.config.dictUrl,
        	data: data
    	};
    	//查询数据
		uxutil.server.ajax(config, function(data) {
			if (data.success){
			    if (callback) callback(data.value.list);
			}else{
				layer.msg('查询字典错误：' + data.msg, {time:5000});
			};
		});
    };
    
    //血袋外观
    editconfigdlg.loadAppearanceDict = function(callback){
    	var me = this;
    	me.getBDict('BloodAppearance', function(data){
     	    me.addselectNode(data, me.config.bloodAppearanceEleId);
    	    if (callback && typeof(callback) == 'function') callback();
    	});
    };
    
    //血袋的完整性
    editconfigdlg.loadintegrityDict = function(callback){
    	var me = this;
    	me.getBDict('BloodIntegrity', function(data){
     	    me.addselectNode(data, me.config.bloodIntegrityEleId);
    	    if (callback && typeof(callback) == 'function') callback();
    	});    	
    };
    
    //初始日期
	editconfigdlg.InitbagOperationDate = function(){
		var me = this;
		var dateId = "#bagopertime_date";
		var initdate = util.toDateString(bagopertime, 'yyyy-MM-dd HH:mm:ss')
		laydate.render({
		 	elem: dateId,
		 	type:"datetime",
		    format:'yyyy-MM-dd HH:mm:ss',
		    value: initdate
		});
	};
	
	//按外观id选中
	editconfigdlg.InitAppearance = function(){
		var me = this;
		var elemId = me.config.bloodAppearanceEleId;
		var elem = $(elemId + ' option[value=' + appearance_bdict_id + ']');
		if (elem.length <= 0) return;
		elem.attr("selected","selected");
	};
	
	//按完整性id选中
	editconfigdlg.InitIntegrity = function(){
		var me = this;
		var elemId = me.config.bloodIntegrityEleId;
		var elem = $(elemId + ' option[value=' + integrity_bdict_id + ']');
		if (elem.length <= 0) return;
		elem.attr("selected","selected");
	};	
	
    //定义回调函数,这样的好处把计数器封装起来，只要修改计数器的累计总数就可以到达目标
    var selectCallback = function(){
    	var count = 0;
    	return function(){
	     	count = count + 1;
	    	if (count >= 2) {
	    		editconfigdlg.InitAppearance();
                editconfigdlg.InitIntegrity();
	    		form.render('select');
	    		count = 0;                
	    	};   		
    	};
    }();
    
    editconfigdlg.InitbagOperationDate();
    editconfigdlg.loadAppearanceDict(selectCallback);
    editconfigdlg.loadintegrityDict(selectCallback);
});

