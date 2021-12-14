/**
 * @name：检验单样本单信息
 * @author：liangyl
 * @version 2021-09-07
 */
layui.extend({
}).define(['form', 'uxutil','uxbase','uxtable','uxbasic'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,	
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		form = layui.form,
		uxbasic = layui.uxbasic,
		MOD_NAME = 'TestFormList';
	
	//获取样本单列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormBySampleNo?isPlanish=true';
	//修改数据服务路径
	var DEIT_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_DeleteBatchLisTestForm';
    //查询是否能删除
    var GET_EDIT_INFO_URL = uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormIsDelete';
	//默认查询条件
	var DEFAULTWHERE = 'listestform.MainStatusID=0';

	//内部列表+表头dom
	var TABLE_DOM = [
		'<div class="{tableId}-table" style="overflow-y:hidden;">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>'
	];
	//医嘱单列表
	var TestFormList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			DIRECTION:'asc',
			//双击
			rowDouble:function(){},
			//选择复选框
			checkClick:function(){},
			done:function(){}
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			size:'sm',//小尺寸的表格
			where:{},
			height:null,
			page: false,
			limit: 5000,
			limits: [20,50,100, 200, 500, 1000, 1500],
			autoSort:false,
			defaultOrderBy:[
				{property:'LisTestForm_GTestDate',direction:'ASC'},
				{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}
			],
			cols:[[
			    {type: 'checkbox', fixed: 'left'},
				{field:'LisTestForm_Id', width:180, title: '检验单ID', sort: true,hide:true},
				{field:'LisTestForm_GTestDate', width:100, title: '检验日期', sort: true, templet:function(record){
					var value = record["LisTestForm_GTestDate"],
	                    v = uxutil.date.toString(value, true) || '';
	                return v;
				}},
				{field:'LisTestForm_GSampleNoForOrder', width:100, title: '样本号', sort: true, templet:function(record){
					var v = record["LisTestForm_GSampleNo"];
	                return v;
				}},
				{field:'LisTestForm_GSampleNo', width:80, title: '样本号排序',hide:true, templet:function(record){
					var v = record["LisTestForm_GSampleNoForOrder"];
	                return v;
				}},
				{field:'LisTestForm_CName', width:100, title: '姓名'},
				{field:'LisTestForm_LisPatient_GenderName',width:70,title:'性别',sort:true},
				{field:'LisTestForm_GSampleType', width: 100, title: '样本类型', sort: true },
				{field:'LisTestForm_LBSection_CName', width: 100, title: '检验小组', sort: false },
				{field:'LisTestForm_ISource', width: 100, title: '检验单来源', sort: true,hide:true },
				{field:'LisTestForm_LisOrderForm_Id', width: 100, title: '医嘱单ID', sort: false,hide:true }
		    ]],
			text: {none: '暂无相关数据' },
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d\u000a/g, "\\n")) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,TestFormList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,TestFormList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
	
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			$(me.tableConfig.elem+'+div .layui-table-header input[name="layTableCheckbox"]').prop('checked', true)
            $(me.tableConfig.elem+'+div .layui-table-header input[name="layTableCheckbox"]').next().addClass('layui-form-checked')
			for(var i = 0; i < res.data.length; i++) {
				res.data[i].LAY_CHECKED = true;
			    $(me.tableConfig.elem+'+div .layui-table-body table.layui-table tbody tr[data-index=' + i + '] input[type="checkbox"]').prop('checked', true);
			    $(me.tableConfig.elem+'+div .layui-table-body table.layui-table tbody tr[data-index=' + i + '] input[type="checkbox"]').next().addClass('layui-form-checked');
			}
            form.render('checkbox');
            me.config.done && me.config.done(res.data);
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);

	};
    //store 重新按样本号+时间排序
	Class.prototype.storeSort = function(direction){
		var me = this;
		if(!direction)return false;
		me.uxtable.instance.reload({data:uxbasic.getStoreList(me.getListData(),direction)});
	};
	
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
      	//监听样本单列表排序
        me.uxtable.table.on('sort(' + me.tableId + ')', function (obj) {
            var field = obj.field, //当前排序的字段名
                type = obj.type, //当前排序类型：desc（降序）、asc（升序）、null（空对象，默认排序）
                sort = [];
            me.config.DIRECTION  = type;
            me.storeSort(me.config.DIRECTION);
        });

	   //双击行
		me.uxtable.table.on('rowDouble(' + me.tableId + ')', function(obj){
			//标注选中样式
		    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		   
		    me.config.rowDouble && me.config.rowDouble([obj.data]);
		});
		me.uxtable.table.on('checkbox(' + me.tableId + ')', function(obj){
			var checkStatus =  me.uxtable.table.checkStatus(me.tableId);
            var data = [];
            if(obj.type != 'all'){  //非全选
            	me.del([obj.data]);
            	me.config.checkClick && me.config.checkClick();
            }else{ //全选
            	me.del(me.getListData());
            	 me.config.checkClick && me.config.checkClick();
            }
		});
	};
	//获取列表数据
	Class.prototype.getListData = function(){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    return list;
	};
	//向左移动数据
	Class.prototype.add = function(records){
		var me = this,
	    	add_list = me.uxtable.table.cache[me.tableId];
		if(records.length==0){
			uxbase.MSG.onWarn("请选择检验单列表行!");
			return false;
		}
		var arr=[],isAdd = true;
		for(var i=0;i<records.length;i++){
			isAdd =true;
			for(var j=0;j<add_list.length;j++){
	      		if(add_list[j].LisTestForm_Id == records[i].LisTestForm_Id){
	  				isAdd = false;
//				  	add_list.splice(j,1); //删除下标为i的元素
					break;
	  			}
	      	}
			if(isAdd)add_list.push(records[i]);
        }
		me.uxtable.instance.reload({data:uxbasic.getStoreList(add_list,me.config.DIRECTION)});
	};
	//删除数据
	Class.prototype.del = function(records){
		var me = this;
		var leftlist = me.uxtable.table.cache[me.tableId];
		if(records.length == 0) {
			uxbase.MSG.onWarn("请选择已选检验单列表行!");
			return;
		} 
		var arr = [];
		for(var i=0;i<leftlist.length;i++){
			var isdel=false;
			for(var j=0;j<records.length;j++){
				if(leftlist[i].LisTestForm_Id == records[j].LisTestForm_Id){
	  				isdel= true;
	  				records.splice(j,1); //删除下标为i的元素
					break;
	  			}
			}
			if(!isdel)arr.push(leftlist[i]);
		}
		me.uxtable.instance.reload({data:arr});
	};
    Class.prototype.onDelClick = function(list,callback){
		var me = this,
		    msg = "";
		if(list.length==0){
			uxbase.MSG.onWarn("请选择要删除的检验单!");
			return;
		} 
		me.IsDelete(list,function(data){
			var num = data.length;
			var isExec = true;
			if(data.length>0){
				for(var i=0;i<data.length;i++){
					var str1  = '检验单('+data[i].检验日期+'&nbsp;&nbsp;'+data[i].样本号 +')';
					if(data[i].核收标志=='是')msg+=str1+"为核收的检验单<br/>";
					if(data[i].存在检验结果=='是')msg+=str1+"已有结果<br/>";
			    }
				if(msg)msg+='<br/>建议取消本次删除';
				layer.confirm(msg, {
					icon: 3, title: '提示' ,
                    btn: ['是:取消删除(建议)','否:执行删除'], //按钮
                    yes: function (index) {
				       layer.close(index);
				    },
				    btn2:function(){
				    	var isDeleteFlag = 1;
				        me.DelClick(isDeleteFlag,list,function(){
				        	me.clearData();
	                    	callback();
	                    });
                    }
               });
			}else{
				layer.confirm("确定要删除检验单吗", {
					icon: 3, title: '提示' ,
                    btn: ['是','否'] //按钮
                }, function(){
                	var isDeleteFlag = 0;
                    me.DelClick(isDeleteFlag,list,function(){
				        me.clearData();
                    	callback();
                    });
                });
			}
		});
	};
	//查询样本单是否可以删除
	Class.prototype.IsDelete = function(list, callback) {
		var me = this;
		var arr = [];
		for(var i = 0; i < list.length; i++) {
			arr.push(list[i].LisTestForm_Id);
		}
		var strArr = arr.splice(',');
		var params = {
			delIDList: strArr.join(',')
		};
		var config = {
			type: 'post',
			url: GET_EDIT_INFO_URL,
			data: JSON.stringify(params)
		};
		uxutil.server.ajax(config, function(data) {
			if(data.success) {
				callback([]);
			} else {
				callback($.parseJSON(data.ErrorInfo));
			}
		});
	};
	Class.prototype.DelClick  = function(isDeleteFlag,list,callback){
		var ids=[];
   	    for(var i=0;i<list.length;i++){
   	    	ids.push(list[i].LisTestForm_Id);
   	    }
	    var config = {
			type:'post',
			url:DEIT_URL,
			data:JSON.stringify({delIDList:ids.join(','),isDeleteFlag:isDeleteFlag})
		};
	    var index = layer.load();
	    uxutil.server.ajax(config,function(data){
	    	layer.close(index);
			if(data.success){
				uxbase.MSG.onSuccess("批量删除检验单成功!");
				callback();
			}else{
				uxbase.MSG.onError(data.ErrorInfo);
			}
		});
	};
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	//核心入口
	TestFormList.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({
			url: '',
			data:[]
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME,TestFormList);
});