/**
	@name：参数类型
	@author：liangyl
	@version 2021-07-0
 */
layui.extend({
	uxtable:'ux/table'
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		uxtable = layui.uxtable;
		
	//获取所有枚举
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QueryParaSystemTypeInfoAndAddDefultPara';
	//删除个性设置(整个)
    var DEL_PARAITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_DeleteSystemParaItem';
	//删除站点类型
	var DEL_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelBHostType';

	var paraTypeCode ="";
	var paraTypeName ="";
	var table_ind = null;
	//下拉框数据
	var COM_DATA_LIST=[];
	var ParameterList = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			title:'系统列表',
			initSort:{field:'Id',type:'asc'},
			size: 'sm', //小尺寸的表格
			cols:[[
		        {field:'Id',title:'ID',width:150,sort:true,hide:true},				
				{field:'Code',title:'Code',width:150,sort:true,hide:true},
				{field:'Name',title:'站点类型',minWidth:150,flex:1}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
				}
			}
		},me.config,ParameterList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(paraTypeCode,paraTypeName,com_data){
		var  me = this;
		table_ind.paraTypeName = paraTypeName;
		table_ind.paraTypeCode = paraTypeCode;
		if(com_data && com_data.length>0)COM_DATA_LIST =com_data;
		var index =layer.load();
	    //获取类型列表
		Class.pt.onLoadTypeList(paraTypeCode,function(data){
			layer.close(index);
			if(data.success){
				var list = data.value || [];
				var arr = [];
				for(var i=0;i<list.length;i++){
					var obj={
						Code:list[i][0],
						Name:list[i][1]
					};
					arr.push(obj);
				}
				me.instance.reload({data:arr});
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		}); 
	};
	//获取类型列表
	Class.pt.onLoadTypeList = function(paraTypeCode,callback){
		var url  =  GET_LIST_URL+'?systemTypeCode=1&paraTypeCode='+paraTypeCode;
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	};

    Class.pt.initListeners= function(){
    	var me = this;
    	
    };
    Class.pt.openWin= function(title,type,Code,name){
    	var me = this,
    	    flag = false;
		var win = $(window),
		    maxWidth = win.width()-100,
			maxHeight = win.height()-80,
			width = maxWidth > 800 ? maxWidth : 800,
			height = maxHeight > 600 ? maxHeight : 600;
		//默认参数
    	layer.open({
            type: 2,
		    area:[width+'px',height+'px'],
            fixed: false,
            maxmin: false,
            title:title,
            content: 'add/index.html?paraTypeName='+table_ind.paraTypeName+'&paraTypeCode='+Code+'&defaultParaTypeCode='+table_ind.paraTypeCode+'&type='+type+'&name='+name,
            cancel: function (index, layero) {
                flag = true;
            },
	        end : function() {
	        	if(flag)return;
	        	table_ind.loadData(table_ind.paraTypeCode,table_ind.paraTypeName);
	        }
        });
    };
    Class.pt.onDelParaItemClick = function(objectInfo,callback){
    	var me = this;
		if (objectInfo.length==0) return;
    	var params = JSON.stringify({objectInfo:JSON.stringify(objectInfo)});
		var config = {
			type: "POST",
			url:DEL_PARAITEM_URL,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				Class.pt.delHostType(objectInfo[0].ObjectID,function(){
					callback(data);
				});
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};
	//删除站点类型 
	 Class.pt.delHostType = function(id,callback){
        if(!id)return;
    	var url = DEL_INFO_URL+'?id='+ id;
	    uxutil.server.ajax({
			url: url
		}, function(data) {
			layer.closeAll('loading');
			if(data.success === true) {
               callback();
			}else{
				layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
			}
		});
	};
	//主入口
	ParameterList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		result.onDelParaItemClick= me.onDelParaItemClick;
		result.openWin= me.openWin;
		me.initListeners();
		return result;
	};
	//下拉数据传给子窗体
	function childComDataUpate(){
		return COM_DATA_LIST;
	}
	window.childComDataUpate = childComDataUpate;
	//暴露接口
	exports('ParameterList',ParameterList);
});