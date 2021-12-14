/**
	@name：人员信息
	@author：liangyl
	@version 2021-08-05
 */
layui.extend({
	uxtable:'ux/table'
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
		
	//获取所有人員列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_GetLIIPHREmpIdentity';
    var table_ind = null;
	var EmpList = {
		//参数配置
		config:{
            page: false,
			limit: 5000,
			loading : true,
			cols:[[
			    {type:'checkbox'},	
				{field:'Id',title:'ID',width:150,hide:true},
				{field:'CName',title:'姓名',minWidth:100,flex:1},
				{field:'StandCode',title:'工号',minWidth:100,flex:1},
				{field:'TSysName',title:'员工身份',minWidth:150,flex:1},
				{field:'SystemName',title:'系统名称',width:100,hide:true}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({},me.config,EmpList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(whereObj,callback){
		var  me = this;
	    Class.pt.onLoadData(function(data){
	    	callback(data);
	    });
	};
	 //获取站点类型数据
    Class.pt.onLoadData =  function(callback){
		var params = JSON.stringify({});
		//显示遮罩层
		var config = {
			type: "POST",
			url: GET_LIST_URL,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				var list = data.value || [];
				callback(list);
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
    };
    /*
	 * 模糊查询一个数组
	 */
    Class.pt.searchList =  function(str, container) {
	    var newList = [];
	    //新的列表
	    var startChar = str.charAt(0);
	    //开始字符
	    var strLen = str.length;
	    //查找符串的长度
	    for (var i = 0; i < container.length; i++) {
	        var obj = container[i];
	        var isMatch = false;
	        for (var p in obj) {
	            if ( typeof (obj[p]) == "function") {
	                obj[p]();
	            } else {
	                var curItem = "";
	                if(obj[p]!=null){
	                    curItem = obj[p];
	                }
	                for (var j = 0; j < curItem.length; j++) {
	                    if (curItem.charAt(j) == startChar)//如果匹配起始字符,开始查找
	                    {
	                        if (curItem.substring(j).substring(0, strLen) == str)//如果从j开始的字符与str匹配，那ok
	                        {
	                            isMatch = true;
	                            break;
	                        }
	                    }
	                }
	            }
	        }
	        if (isMatch) {
	            newList.push(obj);
	        }
	    }
	    return newList;
	};
	//主入口
	EmpList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		result.searchList = me.searchList;
		return result;
	};

	//暴露接口
	exports('EmpList',EmpList);
});