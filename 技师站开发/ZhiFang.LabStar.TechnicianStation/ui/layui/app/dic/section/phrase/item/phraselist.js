/**
	@name：短语列表
	@author：liangyl	
	@version 2019-10-30
 */
layui.extend({
}).define(['uxutil','uxtable','table'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable;
	
		
	//获取短语列表数据
	var GET_BPHRASE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?isPlanish=true';
	//删除短语列表数据
	var DEL_BPHRASE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase';
    //样本类型
    var GET_SAMPLETYPE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true';
    var config ={
    	ObjectID : "",
    	EMP_ENUM:{},
    	checkRowData:[{
    		table:'',
    		data:[]
    	}],
    	tableIns:null
    };
	var phrasetable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultOrderBy:"[{property: 'LBPhrase_DispOrder',direction: 'ASC'}]",
			defaultLoad:false,
			data:[],
            toolbar: '#itemPhrasetool',
            size: 'sm', //小尺寸的表格
			cols:[[
				{field: 'LBPhrase_Id', width: 60,title: '主键ID', hide: true},
				{field: 'LBPhrase_TypeName',title: '短语类型',minWidth: 100}, 
				{field: 'LBPhrase_ObjectType', title: '针对类型',minWidth: 80,
	                templet: function (data) {
	                    var str = "";
	                    if (data.LBPhrase_ObjectType == 1) {
	                        str = "<span  style='color:red'>小组样本</span>";
	                    } else if (data.LBPhrase_ObjectType == 2) {
	                        str = "<span  style='color:red'>检验项目</span>";
	                    }
	                    return str;
	                }
	            }, 
	            {field: 'LBPhrase_ObjectID',title: '对象Id', minWidth: 120,hide:true}, 
	            {field: 'LBPhrase_SampleTypeID',title: '样本类型Id',minWidth: 100,hide:true},
	            {field: 'LBPhrase_CName', title: '短语名称',minWidth: 80},
	            {field: 'LBPhrase_Shortcode',title: '快捷码',minWidth: 70},
	            {field: 'LBPhrase_PinYinZiTou',title: '拼音字头',minWidth: 70},
	            {field: 'LBPhrase_SampleTypeName',title: '样本类型',minWidth: 70,templet: function (data) {
					var v = data.LBPhrase_SampleTypeID;
					if(config.EMP_ENUM != null)v = config.EMP_ENUM[v];
					if(v =='undefined' || !v)v='';
                    return v;
                }},
	            {field: 'LBPhrase_DispOrder',title: '显示次序',minWidth: 70,hide:true}

	        ]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				if(count>0){
					var filter = this.elem.attr("lay-filter");
					
					//默认选择第一行
					var rowIndex = 0;
		            //默认选择行
				    Class.pt.doAutoSelect(rowIndex,this);
			    }

			}
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						var rowobj={
							table:filter,
							data:obj.data
						};
						config.checkRowData.push(rowobj);
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
					 //头工具栏事件
				    that.table.on('toolbar(' + filter + ')', function(obj){
				    	config.tableIns = that;
					    switch(obj.event){
					      case 'add':
					        var list = table.cache[filter];
                            var DispOrder =1;
					        if(list.length>0)DispOrder = Number(list[list.length-1].LBPhrase_DispOrder)+1;
					         me.openWinForm('add',null,DispOrder);
					      break;
					      case 'edit':
					        var checkRowObj =null;
					        for(var i=0;i<config.checkRowData.length;i++){
					        	if(config.checkRowData[i].table == filter){
					        		checkRowObj = config.checkRowData[i].data;
					        		break;
					        	}
					        }
					        var checkRowObj = me.getCheckRow(filter);
					        if(!checkRowObj){
					        	layer.msg("请选择编辑行!",{ icon: 5, anim: 6 });
					        	return;
					        }
					        me.openWinForm('edit',checkRowObj.LBPhrase_Id);
					      break;
					      case 'del':
					         var checkRowObj = me.getCheckRow(filter);
					         if(!checkRowObj){
					        	layer.msg("请选择删除行!",{ icon: 5, anim: 6 });
					        	return;
					         }
					         me.onDelClick(checkRowObj.LBPhrase_Id);
					      break;
					    };
				    });
				}
			}
		},me.config,phrasetable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//获取选择行
	Class.pt.getCheckRow = function(filter){
		var checkRowObj =null;
        for(var i=0;i<config.checkRowData.length;i++){
        	if(config.checkRowData[i].table == filter){
        		checkRowObj = config.checkRowData[i].data;
        		break;
        	}
        }
        return checkRowObj;
	};
	//数据加载
	Class.pt.loadData = function(whereObj,ObjectID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
		config.checkRowData = [];
		config.ObjectID = ObjectID;
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_BPHRASE_LIST_URL+'&fields='+fields.join(',')+'&sort='+me.config.defaultOrderBy;
		var obj ={};
		var where = "lbphrase.ObjectID=" + ObjectID + " and lbphrase.TypeCode='" + me.config.TypeCode +"' and lbphrase.ObjectType=2";
		obj.where=where;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,obj)
		});
	};
	Class.pt.openWinForm = function(type,id,DispOrder){
		var me =  this;
		var title = "新增"+me.config.title;
		if(type =='edit')title = "修改"+me.config.title;
		layer.open({
            type: 2,
            area: ['600px', '340px'],
            fixed: false,
            maxmin: false,
//          resize/
            title:title,
            content: 'phrase/item/form.html?ObjectType='+me.config.ObjectType+'&ObjectID='+config.ObjectID+'&formtype='+type+'&pk='+id+'&TypeCode='+me.config.TypeCode+'&Code='+me.config.CODE+'&DispOrder='+DispOrder
        });
	};
	   //删除方法 
	Class.pt.onDelClick = function(id){
		var me = this;
        if(!id)return;
//      var index = layer.load();
    	var url = DEL_BPHRASE_URL+'?id='+ id;
	    layer.confirm('确定删除选中项?',{ icon: 3, title: '提示' }, function(index) {
	        uxutil.server.ajax({
				url: url
			}, function(data) {
				if(data.success === true) {
//					layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
                    config.tableIns.loadData({},config.ObjectID);
				}else{
					layer.msg(data.msg, { icon: 5, anim: 6 });
				}
			});
        });
	};
	  //获取所有样本类型
	Class.pt.SampleTypeList = function(callback){
		var fields = ['LBSampleType_Id','LBSampleType_CName'],
			url = GET_SAMPLETYPE_LIST_URL;
		url += '&fields='+fields.join(',');
		
		uxutil.server.ajax({
			url:url,
			async:false
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	};
		/***默认选择行
	 * @description 默认选中并触发行单击处理 
	 * @param curTable:当前操作table
	 * @param rowIndex: 指定选中的行
	 * */
	Class.pt.doAutoSelect = function (rowIndex,that) {
		var  me = this;
		var data = table.cache[that.id] || [];
		if (!data || data.length <= 0) return;
		rowIndex = rowIndex || 0;
		var tableDiv = $("#"+that.id+"+div .layui-table-body.layui-table-body.layui-table-main");
        var thatrow = tableDiv.find('tr:eq(' + rowIndex + ')');
		var filter = $(that.elem).find('lay-filter');
		var obj = {
			tr: thatrow,
			data: data[rowIndex] || {},
			del: function () {
				table.cache[that.id][index] = [];
				tr.remove();
				that.scrollPatch();
			},
			updte:{}
		};
		layui.event.call(thatrow, 'table', 'row' + '(' + filter + ')', obj);
		thatrow.click();
	};
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
		
	};
	//主入口
	phrasetable.render = function(options){
		var me = new Class(options);
		
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		//加载数据
		if(me.config.defaultLoad)result.loadData(me.config.where,1);
       	me.SampleTypeList(function(list){
			for(var i=0;i<list.length;i++){
				config.EMP_ENUM[list[i].LBSampleType_Id] = list[i].LBSampleType_CName;
			}
		});
        me.initListeners(result);
		return result;
	};
	Class.pt.afterPhraseUpdate = function(data,FORMTYPE){
        var me = this;
        if(FORMTYPE=='add')layer.msg('新增成功!',{ icon: 6, anim: 0 ,time:2000 });
           else layer.msg('编辑成功!',{ icon: 6, anim: 0 ,time:2000 });
        config.tableIns.loadData({},config.ObjectID);
    };
    window.afterPhraseUpdate = Class.pt.afterPhraseUpdate;
	//暴露接口
	exports('phrasetable',phrasetable);
});