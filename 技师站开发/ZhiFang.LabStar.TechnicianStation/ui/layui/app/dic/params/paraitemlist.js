/**
	@name：参数个性化设置
	@author：liangyl
	@version 2021-07-0
 */
layui.extend({
	uxtable:'ux/table',
	ListForm:'app/dic/params/form'//列表嵌入的表单
}).define(['form','uxutil','uxtable','table','ListForm','commonzf'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
        ListForm  = layui.ListForm,
        commonzf = layui.commonzf,
		uxtable = layui.uxtable;
		
	//获取参数列表数据
	var GET_PARA_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?isPlanish=true';
	//获取参数列表数据
	var GET_PARAITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemParaItem?isPlanish=true';
	//删除个性设置
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelBParaItem';
	//删除个性设置(整个)
	var DEL_PARAITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_DeleteSystemParaItem';
	//保存个性设置
	var SAVE_URL= uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemParaItem';
    //默认参数保存
    var DEFAULT_SAVE_URL= uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemDefaultPara';
    var EDIT_URL= uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateBParaItemByField';   //编辑基础服务-用于修改显示次序
	 //实例化
	var table_ind =null;
	//下拉框数据
	var COM_DATA_LIST=[];
	
	//列表数据
	var DATA_LIST =[];

	var ParaItemList = { 
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'BPara_DispOrder',direction: 'ASC'}]",
			cols:[[
				{field:'Id',title:'ID',width:150,hide:true},
			   	{ field:'l1',title:'操作', toolbar: '#barDemo', width:200,hide:true},
				{field:'BPara_Id',title:'ID',width:150,hide:true},
				{field:'BPara_ParaNo',title:'参数编号',width:240,hide:true},
				{field:'BPara_CName',title:'参数名称',flex:1,minWidth:100},
				{field:'ParaNo',title:'编号',width:60},
				{field:'ParaValue',title:'参数值',minWidth:150,flex:1, templet: function (data) {
					var str = data.ParaValue;
					var ParaEditInfo  = data.BPara_ParaEditInfo;
					if(!ListForm.isDefault(table_ind.ObjectID))str = ListForm.createEditInfo(data,'',COM_DATA_LIST,ParaEditInfo);
                    return str;
                }},
                {field:'BPara_ParaValue',title:'默认值',minWidth:150,flex:1,hide:false,templet: function (data) {
                    var ParaValue = Class.pt.ItemParaValue(data);
                    return ParaValue;
                }},
				{field:'ParaValue2',title:'默认值',minWidth:150,flex:1,templet: function (data) {
                    var str = data.BPara_ParaValue;
                    var ParaEditInfo  = data.BPara_ParaEditInfo;
					if(ListForm.isDefault(table_ind.ObjectID))str = ListForm.createEditInfo(data,'Default',COM_DATA_LIST,ParaEditInfo);
                    return str;
                }},
				{field:'BPara_TypeCode',title:'TypeCode',width:100,hide:true},
				{field:'BPara_ParaType',title:'ParaType',width:100,hide:true},
				{field:'BPara_ParaDesc',title:'ParaDesc',width:100,hide:true},
				{field:'BPara_ParaEditInfo',title:'ParaEditInfo',width:100,hide:true},
				{field:'BPara_DispOrder',title:'DispOrder',width:100,hide:true},
				{field:'DispOrder',title:'DispOrder',width:100,hide:true}
			]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				if(res.data.length>0)ListForm.initComRender(res.data,table_ind);
			}
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({},me.config,ParaItemList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(DefaultParaTypeCode,ObjectID,paraTypeName,list){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	    table_ind.ObjectID = ObjectID;
	    table_ind.ParaTypeName = paraTypeName;
        table_ind.DefaultParaTypeCode = DefaultParaTypeCode;
        if(list && list.length>0)COM_DATA_LIST = list;
	    if(ListForm.isDefault(table_ind.ObjectID) || !ObjectID){ //默认参数
	    	Class.pt.onParaList(DefaultParaTypeCode,function(arr){
		      	me.config.cols[0][1].hide = true; //操作列
		      	me.config.cols[0][6].hide = true; //参数值
		      	me.config.cols[0][7].hide = true; //默认值默认值
		      	me.config.cols[0][8].hide = false; //ParaValue2
		      	DATA_LIST = arr;
		     	me.instance.reload({data:arr});
		    });
	    }else{
	    	//查询个性设置
			Class.pt.onParaItemList(ObjectID,DefaultParaTypeCode,function(arr){
				me.config.cols[0][1].hide = false;
				me.config.cols[0][6].hide = false;
				me.config.cols[0][7].hide = false;
		      	me.config.cols[0][8].hide = true;
		      	DATA_LIST = arr;
                me.instance.reload({data:arr});
			});
	    }
	};
   //获取个性列表
	Class.pt.onParaItemList = function(ObjectID,DefaultParaTypeCode,callback){
		var me = this;
		var	cols = table_ind.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url  = GET_PARAITEM_LIST_URL+'&systemTypeCode=1&paraTypeCode='+DefaultParaTypeCode;
		url+='&where=bparaitem.ObjectID='+ ObjectID+'&fields=BParaItem_' + fields.join(',BParaItem_');
		url+="&sort=[{property: 'BParaItem_DispOrder',direction: 'ASC'}]";	

		uxutil.server.ajax({
			url:url
		},function(data){
			var list = (data.value ||{}).list || [];
			var arr = [];
			if(list.length>0){
				arr = JSON.stringify(list).replace(/BParaItem_/g, "");
		        arr = JSON.parse(arr);
			} 			
			if(arr.length>0)arr.sort(Class.pt.compare('DispOrder'));
			for(var i in arr){
				var ParaNo = arr[i].BPara_ParaNo;
				if(ParaNo){
					var index = ParaNo.lastIndexOf("_");  
	                ParaNo  = ParaNo.substring(index + 1, ParaNo.length);
					arr[i].ParaNo = ParaNo;
				}
			}
			callback(arr);
		});
	};
	 //获取默认参数数据
	Class.pt.onParaList = function(DefaultParaTypeCode,callback){
		
		var	cols = table_ind.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_PARA_LIST_URL+'&paraTypeCode='+DefaultParaTypeCode;
	        url+='&fields='+fields.join(',')+'&sort='+table_ind.config.defaultOrderBy;
		uxutil.server.ajax({
			url:url
		},function(data){
			var list = (data.value ||{}).list || [];
			var arr = [];
			for(var i=0;i<list.length;i++){
				list[i].ParaValue2 = list[i].BPara_ParaValue;
				var ParaNo = list[i].BPara_ParaNo;
				if(ParaNo){
					var index = ParaNo.lastIndexOf("_");  
	                ParaNo  = ParaNo.substring(index + 1, ParaNo.length);
					list[i].ParaNo = ParaNo;
				}
				arr.push(list[i]);
			}
			if(arr.length>0)arr.sort(Class.pt.compare('BPara_DispOrder'));

			callback(arr);
		});
	};
	 //删除方法 
	Class.pt.onDelClick = function(id,callback){
		var me = this;
        if(!id)return;
    	var url = DEL_URL +'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				layer.closeAll('loading');
				if(data.success === true) {
					layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                    callback();
				}else{
					layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
				}
			});
        });
	};
	//获取个性化保存实体
    Class.pt.getItemParams = function(){
    	var me = this,
    	    entityList = me.getParams();
		if(entityList.length ==0)return;
		var objectInfo =[];
	    objectInfo.push({
    		ObjectID:table_ind.ObjectID ,
			ObjectName:table_ind.ParaTypeName
    	}); 
		return JSON.stringify({entityList:entityList,objectInfo:JSON.stringify(objectInfo)});
    };
	Class.pt.initListeners = function(){
		var me = this;
		//保存个性设置
		$('#search').on('click',function(){
			var list = me.searchText($('#search_Text').val());
			table_ind.instance.reload({data:list});
		});
		 //回车事件
	    $("#search_Text").on('keydown', function (event) {
	        if (event.keyCode == 13) {
	        	var list = me.searchText($('#search_Text').val());
			    table_ind.instance.reload({data:list});
	            return false;
	        }
	    });
		
		//保存个性设置
		$('#save').on('click',function(){
			//个性设置实体
			var params = me.getItemParams(),
			    url = SAVE_URL;
			if(ListForm.isDefault(table_ind.ObjectID)){ //默认设置
				var entityList =me.getDefaultParams();
				params = JSON.stringify({entityList:entityList});
				url = DEFAULT_SAVE_URL;
			}
			me.onSaveClick(params,url);
		});
		form.on('checkbox(checkbox2)', function (data) {
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		  	ListForm.updateItem(table_ind,dataindex,data.elem.checked);
	    });
	    form.on('checkbox(select)', function (data) {
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		  	ListForm.updateItem(table_ind,dataindex,data.elem.checked);
	    });
	    
    };
    	/*一维数组对象模糊搜索
  dataList 为一维数组数据结构
  value 为input框的输入值
  type 为指定想要搜索的字段名，array格式 ["name", "number"];
 */
   Class.pt.searchText =function( value){
		var list = DATA_LIST;
		let filters = ["BPara_CName","ParaNo"];
	    var datalist = list.filter(function(item, index, arr) {
	    for (let j = 0; j < filters.length; j++) {
	      if (item[filters[j]] != undefined || item[filters[j]] != null) {
	        if (item[filters[j]].indexOf(value) >= 0) {
	          return item;
	        }
	      }
	    }
	  });
	  return datalist;
    };
    /**@overwrite 获取需要保存的个性设置数据*/
	Class.pt.getParams = function(data) {
		var me = this,
		    entityList = [];
		var list = table_ind.table.cache['paraitem_table'];
		for(var i=0;i<list.length;i++){
			var ParaValue = list[i].ParaValue;
			var type = ListForm.getComType(list[i].BPara_ParaEditInfo);
            if(type == 'E')ParaValue = ParaValue==true ? 1 : 0;
			var obj ={
				ParaNo:list[i].BPara_ParaNo,
				IsUse:1,
				ParaValue:ParaValue,
				Id:list[i].Id
			};
			if(list[i].BPara_Id){
				obj.BPara={
	        		Id:list[i].BPara_Id,
	        		DataTimeStamp:[0,0,0,0,0,0,0,0]
	        	};
			}
			if(uxutil.cookie.get(uxutil.cookie.map.USERID)){
	        	obj.OperatorID = uxutil.cookie.get(uxutil.cookie.map.USERID);
	        	obj.Operater =uxutil.cookie.get(uxutil.cookie.map.USERNAME);
	        }
			if(list[i].DispOrder)obj.DispOrder=list[i].DispOrder;
			entityList.push(obj);
		}
		
		return entityList;
	};
    /**@overwrite 获取需要保存的默认数据实体*/
	Class.pt.getDefaultParams = function(data) {
		var me = this,
		    entityList = [];
		var list = table_ind.table.cache['paraitem_table'];
		for(var i=0;i<list.length;i++){
			var ParaValue = list[i].ParaValue2;
			var type =  ListForm.getComType(list[i].BPara_ParaEditInfo);
            if(type == 'E')ParaValue = ParaValue==true ? 1 : 0;
			var obj ={
				CName:list[i].BPara_CName,
				BVisible:1,
				IsUse:1,
				ParaValue:ParaValue,
				ParaEditInfo:list[i].BPara_ParaEditInfo,
				Id:list[i].BPara_Id,
				ParaNo:list[i].BPara_ParaNo,
				ParaType:list[i].BPara_ParaType,
				ShortCode:list[i].BPara_ShortCode,
				SystemCode :'1',
				TypeCode:list[i].BPara_TypeCode
			};
			if(uxutil.cookie.get(uxutil.cookie.map.USERID)){
	        	obj.OperatorID = uxutil.cookie.get(uxutil.cookie.map.USERID);
	        	obj.Operater =uxutil.cookie.get(uxutil.cookie.map.USERNAME);
	        }
			if(list[i].DispOrder)obj.DispOrder=list[i].BPara_DispOrder;
			entityList.push(obj);
		}
		
		return entityList;
	};
    //表单保存处理
	Class.pt.onSaveClick = function(params,url) {
		var me = this;
		//显示遮罩层
		var config = {
			type: "POST",
			url: url,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config, function(data) {
			layer.close(index);
			if (data.success) {
				layer.msg("保存成功",{icon:6,time:2000});
				table_ind.loadData(table_ind.DefaultParaTypeCode,table_ind.ObjectID,table_ind.ParaTypeName);
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};
	Class.pt.move = function (Id,type) {
        var me = this,
            Id = Id,
            type = type || 'up', // up:上移，down：下移
            LAY_TABLE_INDEX = null,//记录当前行下标
            data = [],//发送数据
            saveCount = 0,
            saveSuccessCount = 0,
            saveErrorCount = 0,
            tableCache = table_ind.table.cache['paraitem_table'];
        if (!Id) return;
        $.each(tableCache, function (i,item) {
            if (Id == item["Id"]) {
                LAY_TABLE_INDEX = item["LAY_TABLE_INDEX"];
                return false;
            }
        });
        if (LAY_TABLE_INDEX == null) return;
        if (type == 'up') {
            if (LAY_TABLE_INDEX == 0) {
                layer.msg("已经是第一条!");
                return;
            }
            data.push({ Id: Id, DispOrder: tableCache[LAY_TABLE_INDEX - 1]["DispOrder"] });
            data.push({ Id: tableCache[LAY_TABLE_INDEX - 1]["Id"], DispOrder: tableCache[LAY_TABLE_INDEX]["DispOrder"] });
        } else if (type == 'down') {
            if (LAY_TABLE_INDEX == tableCache.length - 1) {
                layer.msg("已经是最后一条!");
                return;
            }
            data.push({ Id: Id, DispOrder: tableCache[LAY_TABLE_INDEX + 1]["DispOrder"] });
            data.push({ Id: tableCache[LAY_TABLE_INDEX + 1]["Id"], DispOrder: tableCache[LAY_TABLE_INDEX]["DispOrder"] });
        }
        saveCount = data.length;
        var load = layer.load();
        $.each(data, function (i, item) {
            var entity = entity = {
                entity: {
                    Id: item["Id"],
                    DispOrder: item["DispOrder"]
                },
                fields: "Id,DispOrder"
            };
            var config = {
                type: "POST",
                url: EDIT_URL,
                data: JSON.stringify(entity)
            };
            uxutil.server.ajax(config, function (data) {
                if (data.success) {
                    saveSuccessCount++;
                } else {
                    saveErrorCount++;
                }
                if (saveSuccessCount + saveErrorCount == saveCount) {
                    layer.close(load);
                    table_ind.loadData(table_ind.DefaultParaTypeCode,table_ind.ObjectID,table_ind.ParaTypeName);
                }
            })
        });
    };
    Class.pt.compare = function(property){
	    return function(a,b){
	        var value1 = a[property];
	        var value2 = b[property];
	        return value1 - value2;
	    }
	};
	//查个性表时默认参数返回处理
	Class.pt.ItemParaValue = function(data){
		var me =  this;
		var ParaValue = data.BPara_ParaValue;
		var ParaEditInfo =  data.BPara_ParaEditInfo;
		if(!ListForm.isDefault(table_ind.ObjectID)){
			switch (ListForm.getComType(ParaEditInfo)){
				case 'E':
	    			var arr = [
						'<div style="color:#FF5722;">否</div>',
						'<div style="color:#009688;">是</div>'
					];
					ParaValue = ParaValue == '1' ? arr[1] : arr[0];
					break;  
				case 'CL':
				    if(ParaValue)ParaValue = ListForm.setDisplayName(ParaValue,ListForm.getClList(ParaEditInfo));
	                break;
				case 'DB':
	                if(ParaValue)ParaValue = ListForm.setDisplayName(ParaValue,ListForm.getSelectList(ParaEditInfo));
	                break;
	            case 'SH':
	                if(ParaValue){
	            	    var sh_data = ListForm.getSelectList(ParaEditInfo);
				        var cllist = ListForm.getSHType(ParaEditInfo); //sh下拉数据
				        ParaValue = ListForm.setSHValue(ParaValue,sh_data,cllist);
	                }
	                break;
	            case 'BH':
	                if(ParaValue){
	            	    var bh_data = ListForm.getSelectList(ParaEditInfo);
				        ParaValue = ListForm.setBHValue(ParaValue,bh_data);
	                }
	                break;
			} 
        }
		return ParaValue;
	};
	//主入口
	ParaItemList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind =result;
		result.loadData = me.loadData;
		result.onDelClick = me.onDelClick;
		result.move = me.move;
		me.initListeners();
		return result;
	};
	//AL保存后更新内容
	function afterUpateAL(value,IdElemID,index){
		$("input[name='"+IdElemID+"']").val(value);
		ListForm.updateItem(table_ind,index,value);
	}
	//SH保存后更新内容
	function afterUpateSH(data,IdElemID,ElemName,index){
		$("input[name='"+IdElemID+"']").val(data.id);
		$("input[name='"+ElemName+"']").val(data.name);
		ListForm.updateItem(table_ind,index,data.id);
	}
	//BC保存后更新内容
	function afterUpateBC(value,IdElemID,index){
		$("input[name='"+IdElemID+"']").val(value);
		ListForm.updateItem(table_ind,index,value);
	}
	//BH保存后更新内容
	function afterUpateBH(data,IdElemID,ElemName,index){
		$("input[name='"+IdElemID+"']").val(data.id);
		$("input[name='"+ElemName+"']").val(data.name);
		ListForm.updateItem(table_ind,index,data.id);
	}
	window.afterUpateAL = afterUpateAL;
	window.afterUpateSH = afterUpateSH;
	window.afterUpateBC = afterUpateBC;
	window.afterUpateBH= afterUpateBH;
	//暴露接口
	exports('ParaItemList',ParaItemList);
});
