/**
 * @name：检验单样本单信息
 * @author liangyl
 * @version 2021-05-07
 */
layui.extend({
	uxtable:'ux/table',
	basicStatus:'views/sample/basic/status'//状态公共方法
}).define(['uxutil','uxbase','uxtable','table','form','uxbasic','basicStatus'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbasic = layui.uxbasic,
		basicStatus = layui.basicStatus,
		uxbase = layui.uxbase,
		uxtable = layui.uxtable;
	
		
	//获取样本单列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true';
	//默认查询条件
	var DEFAULTWHERE = 'listestform.MainStatusID>=0';
	//检验单范围所产生的样本号
	var RANGESAMPLENO="";
	//目标样本批量生成的样本号
	var TSAMPLENOLIST=[];
    //查询条件内容
    var SERACHOBJ={};

	var formtable = {
		//参数配置
		config:{
            page: false,
			limit: 5000,
			loading : true,
			defaultOrderBy:[{property:'LisTestForm_GSampleNoForOrder',direction:'ASC'}],
			cols:[[
			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LisTestForm_Id', width:180, title: 'ID', sort: true,hide:true},
				{field:'LisTestForm_GTestDate', width:100, title: '原检验日期', templet:function(record){
					var value = record["LisTestForm_GTestDate"],
	                    v = uxutil.date.toString(value, true) || '';
	                return v;
				}},
				{field:'LisTestForm_GSampleNo', width:80, title: '原样本号' ,templet:function(record){
					var v = record["LisTestForm_GSampleNo"] ? '<span style="color: #FF0000;">'+record["LisTestForm_GSampleNo"]+'</span>' : '';
	                return v;
				}},
				{field:'LisTestForm_BarCode', width:70, title: '条码号'},
				{field:'LisTestForm_CName', width:70, title: '姓名'},
				{field:'LisTestForm_MainStatusID',width:70,title:'状态', templet: function (data) { return basicStatus.onStatusRenderer(data); }},
				{field:'LisTestForm_IsExec', width:80, title: '能否执行',align:'center', templet: '#switchTpl'},
				{ field: 'Direction', width: 45, title: '→', sort: false},
				{ field: 'TLisTestForm_GTestDate', width: 110, title: '目标检验日期',templet:function(record){
					var value = record["TLisTestForm_GTestDate"],
	                    v = uxutil.date.toString(value, true) || '';
	                return v;
				}},
				{ field: 'TLisTestForm_GSampleNo', width: 90, title: '目标样本号',templet:function(record){
					var v = record["TLisTestForm_GSampleNo"] ? '<span style="color: #FF0000;">'+record["TLisTestForm_GSampleNo"]+'</span>' : '';
	                return v;
				}}
		    ]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
		
				var type = typeof res.ResultDataValue,
					data = res.ResultDataValue || {},
					list = [];
					
				if(res.success){
					if(type == 'string'){
						data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
					}
					for(var i in data.list){
						list.push(me.changeData(data.list[i],i));
					}
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
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
		},me.config,formtable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(obj){
		var me = this;
		SERACHOBJ = obj;
		me.instance.reload({data:[]});//清空列表数据
		//查询样本号批量生成的新样本号
	    uxbasic.batchCreateSampleNo(obj.StartSampleNo,obj.GSampleNoForOrder,function(data){
	    	if(!data){
				uxbase.MSG.onWarn("请输入正确的样本号格式!");
	    		return;
	    	}
	    	var arr = data.split(',');
	    	RANGESAMPLENO ="";
            for(var i=0;i<arr.length;i++){
            	if(RANGESAMPLENO.length>1)RANGESAMPLENO+=",";
            	RANGESAMPLENO+="'"+arr[i]+"'";
            }
            TSAMPLENOLIST=[];
            //查询目标样本号批量生成的新样本号
            uxbasic.batchCreateSampleNo(obj.TStartSampleNo,obj.GSampleNoForOrder,function(data2){
	    	    if(!data2){
					uxbase.MSG.onWarn("请输入正确的样本号格式!");
                    return;
		    	}
	    	   TSAMPLENOLIST = data2.split(',');
	    	   Class.pt.onLoad(obj,me);
	    	});
	    });
	};
	//数据加载
	Class.pt.onLoad = function(obj,me){
		var cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
		me.instance.reload({
			url:url,
			where:$.extend({},Class.pt.getWhere(obj),{
				fields:fields.join(','),
				sort:JSON.stringify(me.config.defaultOrderBy)
			})
		});
	};
	Class.pt.getWhere =function(obj){
		var params = [],
		    where = DEFAULTWHERE;
			//小组Id
		if(obj.SECTIONID) {
			params.push("listestform.LBSection.Id=" + obj.SECTIONID + "");
		}
		if(obj.GTestDate){
			params.push("listestform.GTestDate='" + obj.GTestDate + "'");
		}
		if(RANGESAMPLENO){
			params.push("listestform.GSampleNo in(" + RANGESAMPLENO + ")");
		}
		if(params.length > 0) {
			where+= ' and '+ params.join(' and ');
		}
		return {"where":where};
	};
	//数据处理
	Class.pt.changeData =function(data,i){
		var obj = SERACHOBJ,
		    msg = "",SampleNoArr=[];
		    
		data.TLisTestForm_GTestDate = obj.TGTestDate;
		data.TLisTestForm_GSampleNo = TSAMPLENOLIST[i];
		data.Direction ='→';
		data.LisTestForm_IsExec = "true";
		if(data.LisTestForm_MainStatusID!=0){
			data.LisTestForm_IsExec = "false";
			SampleNoArr.push('【'+data.LisTestForm_GSampleNo+'】');
		}
    	if(SampleNoArr.length>0){
    		msg+='原样本号为'+SampleNoArr.join(',')+'的检验单状态不是检验中,不能执行批量样本号更改!';
			uxbase.MSG.onWarn(msg);
    	}
		return data;
	};
	//联动
	Class.pt.initListeners= function(result){
		var me =  this;
	};
	//主入口
	formtable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		result.instance.reload({data:[]});//清空列表数据
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('formtable',formtable);
});