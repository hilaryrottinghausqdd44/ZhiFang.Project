layui.extend({
	uxutil: 'ux/util'	
}).use(['form', 'uxutil', 'layer'], function(){
	"use strict";
	var $ = layui.jquery;
	var form = layui.form; 
	var layer = layui.layer;
    var uxutil = layui.uxutil;
    var configdialog = {
     	config:{
    		fields:['BDict_Id', 'BDict_CName'],
    		params:{page: 0, limit: 1000, fields: "", where:"", sort:""}, //table 690
		  	bloodIntegrityEleId: '#bloodintegrity-select',  //完整性
			bloodAppearanceEleId: '#bloodappearance-select', //外观
			url: uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?isPlanish=true'  		
    	}
    };
    
    //添加doc option node
    configdialog.addselectNode = function(data, eleId){
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
    configdialog.getBDict = function(DictTypeCode, callback){
    	var me = this;
    	var where = "bdict.BDictType.DictTypeCode='" + DictTypeCode +"'" ;
    	where = where +  ' and bdict.IsUse = 1';
    	var fields =  me.config.fields.join(',');
		var data = me.config.params || {};
		var sort = [];
		sort.push({"property":'BDict_DispOrder', "direction": 'ASC'});
		data["fields"] = fields;
		data["where"] = where;
		data["sort"] = JSON.stringify(sort);
    	var config= {
    		type: 'get',
        	url: me.config.url,
        	data: data
    	};
    	//查询数据
		uxutil.server.ajax(config, function(data) {
			if (data.success){
			    if (callback) callback(data.value.list);
			}else{
				layer.msg('查询字典错误：' + data.msg, {time:3000});
			};
		});
    };
    
    //血袋外观
    configdialog.loadAppearanceDict = function(callback){
    	var me = this;
    	me.getBDict('BloodAppearance', function(data){
     	    me.addselectNode(data, me.config.bloodAppearanceEleId);
    	    if (callback && typeof(callback) == 'function') callback();
    	});
    };
    
    //血袋的完整性
    configdialog.loadintegrityDict = function(callback){
    	var me = this;
    	me.getBDict('BloodIntegrity', function(data){
     	    me.addselectNode(data, me.config.bloodIntegrityEleId);
    	    if (callback && typeof(callback) == 'function') callback();
    	});    	
    };
    
    //定义回调函数,这样的好处把计数器封装起来，只要修改计数器的累计总数就可以到达目标
    var selectCallback = function(){
    	var count = 0;
    	return function(){
	     	count = count + 1;
	    	if (count >= 2) {
	    		form.render('select');
	    		count = 0;
	    	};   		
    	};
    }();
    
    configdialog.loadAppearanceDict(selectCallback);
    configdialog.loadintegrityDict(selectCallback);
 
});

