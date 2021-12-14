layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
	//获取采样组项目列表数据
	var GET_SAMPLINGITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/SearchLBSamplingItemBandItemNameList?isPlanish=true';
   //项目查询
   	var GET_ITEM_LIST_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true';
    //采样组项目更新
    var UPDATE_SAMPLINGITEM_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBSamplingItemByField';
    //采样组项目缺省更新服务
    var UPDATE_IS_DEFAULT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_UpdateSamplingItemIsDefault';
    //采样组ID
    var SamplingGroupID = null;
     //采样名称
    var SamplingGroupCName = "";
    //列表实例
    var DATA_LIST = [];
	var SamplingItemList = {
		//参数配置
		config:{
            page: true,
			limit: 50,
			loading : true,
			defaultLoad:false,
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LBSamplingItemVO_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBSamplingItemVO_LBItem_Id',title:'项目编号',width:150,hide:true},
				{field:'LBSamplingItemVO_LBItem_CName',title:'项目名称',minWidth:180,flex:1},
				{field:'LBSamplingItemVO_LBItem_UseCode',title:'用户编码',width:100},
				{field:'LBSamplingItemVO_IsDefault',title:'是否缺省',width:100,align:'center',templet: '#switchTpl'},
				{field:'LBSamplingItemVO_MustItemID',title:'必须项目',minWidth:150,flex:1,align:'center', templet: function (data) {
                     var str = 
                     '<select id="MustItem'+data.LAY_TABLE_INDEX+'" lay-filter="mustitem"></select> ';
                    return str;
                }},
                {field:'LBSamplingItemVO_ItemCName',title:'必须项目名称',width:120,hide:true},
				{field:'LBSamplingItemVO_VirtualItemNo',title:'虚拟采样量',width:100,align:'center',edit:true},
				{field:'LBSamplingItemVO_DispOrder',title:'显示次序',width:100,align:'center',edit:true},
				{field:'LBSamplingItemVO_Tab', width:150, title: '用于判断行是否有修改数据',hide:true}
			]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				//下拉列表渲染
				for(var i=0;i<res.data.length;i++){
					var id = 'MustItem'+res.data[i].LAY_TABLE_INDEX;
			        $("#"+id).append(Class.pt.ComDataList(res.data[i]));
					$('#'+id).val(res.data[i].LBSamplingItemVO_MustItemID);
					form.render('select');
				}
			}
		}
	};
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
                var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
				DATA_LIST = [];
				if(data && data.list){
					for(var i=0;i<data.list.length;i++){
						data.list[i].LBSamplingItemVO_Tab="";
						DATA_LIST.push(data.list[i]);
					}
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": DATA_LIST || []
				};
			},
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
		},me.config,SamplingItemList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(id,name) {
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SAMPLINGITEM_LIST_URL;
	    SamplingGroupCName = name; // 采样组名称
		SamplingGroupID = id;// 采样组ID
		
		var where = 'lbsamplingitem.LBSamplingGroup.Id='+id;
		var whereObj ={"where":where};
		me.uxtable.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};  
	//数据加载
	Class.pt.clearData = function() {
		var  me = this;
		me.uxtable.instance.reload({
			url: '',
			data:[]
		});
	};  
	//下拉框数据
	Class.pt.ComDataList = function(d) {
		var list = [],
			htmls = ['<option value="">请选择项目</option>'];
		for(var i=0;i<DATA_LIST.length;i++){
		 	if(DATA_LIST[i].LBSamplingItemVO_LBItem_Id != d.LBSamplingItemVO_LBItem_Id ){
		 	    htmls.push("<option value='" + DATA_LIST[i].LBSamplingItemVO_LBItem_Id +"'>" + DATA_LIST[i].LBSamplingItemVO_LBItem_CName + "</option>");
		 	}
		}
		return htmls.join("");
	};
	/**
	 * 为输入框校验合法数字
	 * @param event
	 * @param obj
	 */
	Class.pt.validateNum = function(event, obj) {
		//响应鼠标事件，允许左右方向键移动
		event = window.event || event;
		if (event.keyCode == 37 | event.keyCode == 39) {
			return;
		}
		var t = obj.value.charAt(0);
		//先把非数字的都替换掉，除了数字和.
		obj.value = obj.value.replace(/[^\d.]/g, "");
		//必须保证第一个为数字而不是.
		obj.value = obj.value.replace(/^\./g, "");
		//保证只有出现一个.而没有多个.
		obj.value = obj.value.replace(/\.{2,}/g, ".");
		//保证.只出现一次，而不能出现两次以上
		obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
        obj.value = Number(obj.value);
	};
    Class.pt.initListeners =  function(){
    	var  me = this;
    	var filter = $(me.config.elem).attr("lay-filter");
		//选择采样组项目
    	$('#addsamplingitem').on('click',function(){
			me.AddSamplingItem();
		});
		//新增采样组与项目的关系
		$('#additemsampling').on('click',function(){
			me.AddItemSampling();
		});
	    form.on('select(mustitem)', function(data){
	    	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		  	var tableCache = table.cache[filter]; 
	     	var rowObj = tableCache[dataindex];
		  	me.changeRowData(data.value,rowObj,'LBSamplingItemVO_MustItemID');
		  	
		});
        // 监听keyup事件
		$(document).on('keyup', 'td[data-field="LBSamplingItemVO_VirtualItemNo"]>input.layui-table-edit', function (event) {
			me.validateNum(event,this);
		});
		$(document).on('keyup', 'td[data-field="LBSamplingItemVO_DispOrder"]>input.layui-table-edit', function (event) {
			me.validateNum(event,this);
		});
	    //监听单元格编辑
		table.on('edit(samplingitem_table)', function(obj){
		    var value = obj.value, //得到修改后的值
		        data = obj.data,//得到所在行所有键值
		        field = obj.field; //得到字段
 	        me.changeRowData(value,data,field);
    	});
		$('#saveitemsampling').on('click',function(){
			me.onSaveRecords();
		});
		form.on('switch(isdefult)', function(data){
            var elem = data.othis.parents('tr');
	  	    var dataindex = elem.attr("data-index");
	  	    var tableCache = table.cache[filter]; 
     	    var rowObj = tableCache[dataindex];
            me.onUpateIsDefult(rowObj,data.elem.checked);
	    });  
    };
    //更新缺省
    Class.pt.onUpateIsDefult =  function(obj,IsDefault){
    	var me = this;
    	var index = layer.load();
    	var entity ={
        	Id:obj.LBSamplingItemVO_Id,
        	ItemId:obj.LBSamplingItemVO_LBItem_Id,
        	IsDefault:IsDefault	
        };
	    var params = JSON.stringify(entity);
       //显示遮罩层
		var config = {
			type: "POST",
			url: UPDATE_IS_DEFAULT_URL,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			layer.close(index);
			if (data.success) {
				me.loadData(SamplingGroupID,SamplingGroupCName);
			} else {
                layer.msg(data.ErrorInfo, { icon: 5 });
			}
		})
    };
    //保存列表数据
    Class.pt.onSaveRecords =  function(){
    	var me = this;
    	var filter = $(me.config.elem).attr("lay-filter");
		var records = me.getModifiedRecords();
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		//显示遮罩
		if(records.length==0)return;
		var indexs=layer.load(2);
		//获取列表数据
	    for(var i = 0;i<records.length;i++){
	    	me.updateOne(i,records[i]);
	    }
    };
    	//获取修改过的行记录
    Class.pt.getModifiedRecords =  function(){
    	var me = this,list=[];
    	//获取列表数据
    	var filter = $(me.config.elem).attr("lay-filter");
		var tableCache = table.cache.samplingitem_table; 
	    for(var i = 0;i<tableCache.length;i++){
	    	//找到修改过数据的行
	    	if(tableCache[i].LBSamplingItemVO_Tab){
	    		var data = {};
				var arr = Object.keys(tableCache[i].LBSamplingItemVO_Tab);
				if(arr.length != 0){
					list.push(tableCache[i]);
				}
	    	}
	    }
	    return list;
    };
    Class.pt.updateOne =  function(index,obj){
   		var me = this;
   		setTimeout(function() {
   	        var id = obj.LBSamplingItemVO_Id;
   	        var IsDefault = obj.LBSamplingItemVO_IsDefault;
   	        var MustItemID = obj.LBSamplingItemVO_MustItemID;
   	        var VirtualItemNo = obj.LBSamplingItemVO_VirtualItemNo;
   	        var DispOrder = obj.LBSamplingItemVO_DispOrder;
            var entity ={
            	Id:id,
            	IsDefault:IsDefault==true ? 1:0
            };
            if(MustItemID)entity.MustItemID=MustItemID;
            if(VirtualItemNo)entity.VirtualItemNo=VirtualItemNo;
            if(DispOrder)entity.DispOrder=DispOrder;
            var fields ="Id,IsDefault,MustItemID,VirtualItemNo,DispOrder";
            var params={entity:entity,fields:fields};
		    params = JSON.stringify(params);
           //显示遮罩层
			var config = {
				type: "POST",
				url: UPDATE_SAMPLINGITEM_URL,
				data: params
			};
   			uxutil.server.ajax(config, function(data) {
				//隐藏遮罩层
				layer.closeAll('loading');
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}				
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					if (me.saveErrorCount == 0){
						layer.msg('保存成功!',{icon:6,time:2000});
						me.loadData(SamplingGroupID,SamplingGroupCName);
					}else{
						layer.msg(data.ErrorInfo, { icon: 5});
					}
				}
			})
		}, 100 * index);
   	};
   	/**记录行操作记录，用于保存修改行
   	 *@param value:修改后的值 
   	 *@param obj 修改行obj对象
   	 *@param field 修改行field 字段
	 * */
	Class.pt.changeRowData = function(value,data,field) {
	    var tableCache = table.cache.samplingitem_table;  
	    var dataindex = 0;
        for(var i=0;i<tableCache.length;i++){
        	if(tableCache[i].LBSamplingItemVO_Id == data.LBSamplingItemVO_Id){
        		dataindex = i;
        		break;
        	}
        }
    	var rowObj = data.LBSamplingItemVO_Tab;
	    if(rowObj[field]) delete rowObj[field];
	    if(!rowObj)rowObj={};
	    $.each(tableCache,function(index,item){
	       	if(item.LAY_TABLE_INDEX==dataindex){
	       		if(value)rowObj[field]=value;
	       		item.LBSamplingItemVO_Tab=rowObj;
	       		item[field] = value;
	       	}
        });  
	};
	Class.pt.AddItemSampling = function(){
		var me = this,
		    flag = false;
		layer.open({
            type: 2,
            area: ['95%', '98%'],
            fixed: false,
            maxmin: false,
            title:'采样组与项目的关系',
            content: 'item/relation/index.html',
            cancel: function (index, layero) {
               flag = false;
            },
            success: function(layero, index){
	        },
	        end : function() {
	        	if(flag)return;
	        	me.loadData(SamplingGroupID,SamplingGroupCName);
	        }
        });
	};
	Class.pt.AddSamplingItem = function(){
		var me = this,
		    flag = false;
		var win = $(window),
		    maxWidth = win.width()-100,
			maxHeight = win.height()-80,
			width = maxWidth > 800 ? maxWidth : 800,
			height = maxHeight > 400 ? maxHeight : 400;
		layer.open({
            type: 2,
		    area:[width+'px',height+'px'],
            fixed: false,
            maxmin: false,
            title:'选择采样组项目',
            content: 'item/transfer/app.html?SamplingGroupID='+SamplingGroupID+'&SamplingGroupCName='+SamplingGroupCName,
            cancel: function (index, layero) {
                flag = true;
            },
	        end : function() {
	        	if(flag)return;
	        	me.loadData(SamplingGroupID,SamplingGroupCName);
	        }
        });
	};
	//核心入口
	SamplingItemList.render = function(options){
		var me = new Class(options);
	
		me.uxtable = uxtable.render(me.config);
		me.uxtable.instance.reload({
			url: '',
			data:[]
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	//暴露接口
	exports('SamplingItemList',SamplingItemList);
});