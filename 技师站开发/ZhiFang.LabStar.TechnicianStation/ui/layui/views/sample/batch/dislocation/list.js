/**
 * @name：检验单样本单,仪器信息
 * @author liangyl
 * @version 2021-05-07
 */
layui.extend({
	basicStatus:'views/sample/basic/status'//状态公共方法
}).define(['uxutil','uxbase','uxtable','table','form','uxbasic','basicStatus'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxbasic = layui.uxbasic,
		uxbase = layui.uxbase,
		basicStatus = layui.basicStatus,
		uxtable = layui.uxtable;
	
		
	//获取样本单列表服务
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?isPlanish=true';
	//获取仪器样本单服务
	var GET_EQIP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisEquipFormByHQL?isPlanish=true';
	//默认查询条件
	var DEFAULTWHERE = 'listestform.MainStatusID>=0';
	//检验单范围所产生的样本号
	var RANGESAMPLENO="";
	//目标样本批量生成的样本号
	var TSAMPLENOLIST=[];
	//检验单范围所产生的样本号,方便数据处理
	var RANGESAMPLENOLIST=[];
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
//			    {type: 'numbers',title: '行号',fixed: 'left'},
				{field:'LisTestForm_Id', width:180, title: '样本单ID', sort: true,hide:true},
				{field:'LisTestForm_GTestDate', width:100, title: '检验日期', templet:function(record){
					var value = record["LisTestForm_GTestDate"],
	                    v = uxutil.date.toString(value, true) || '';
	                return v;
				}},
				{field:'LisTestForm_GSampleNo', width:80, title: '样本号' ,templet:function(record){
					var v = record["LisTestForm_GSampleNo"] ? '<span style="color: #FF0000;">'+record["LisTestForm_GSampleNo"]+'</span>' : '';
	                return v;
				}},
				{field:'LisTestForm_BarCode', width:100, title: '条码号'},
				{field:'LisTestForm_CName', width:100, title: '姓名'},
				{field:'LisTestForm_MainStatusID',width:70,title:'状态',templet: function (data) { return basicStatus.onStatusRenderer(data); }},
				{field:'LisTestForm_IsExec', width:80, title: '能否执行',align:'center', templet: '#switchTpl'},
				{field:'LisTestForm_IsExist', width:70, title: '存在',templet:function(record){
					var v = '<span style="color: red;">否</span>';
					if(record["LisTestForm_IsExist"])v = '<span style="color:green;">是</span>';
	                return v;
				}},
				{ field: 'Direction', width: 45, title: '←', sort: false},
				{ field: 'LisTestForm_EquipFormID', width: 45, title: '仪器样本单ID', sort: false,hide:true},
				{ field: 'LisTestForm_EquipName', width: 100, title: '检验仪器', sort: false},
				{ field: 'LisTestForm_EquipGTestDate', width: 120, title: '仪器检验日期', sort: false},
				{ field: 'LisTestForm_EquipSampleNo', width: 80, title: '样本号',templet:function(record){
					var v = record["LisTestForm_EquipSampleNo"] ? '<span style="color: #FF0000;">'+record["LisTestForm_EquipSampleNo"]+'</span>' : '';
	                return v;
				}},
				{ field: 'LisTestForm_IsEquipExist', width: 70, title: '存在', sort: false,templet:function(record){
					var v = '<span style="color: red;">否</span>';
					if(record["LisTestForm_IsEquipExist"])v = '<span style="color:green;">是</span>';
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
					data.count = Number(SERACHOBJ.GSampleNoForOrder);
				
				    //数据处理返回list
					data.list = me.resultList(data);
					
					    //加载仪器样本单
				    me.EquipForm(function(list){
				    	for(var i=0;i<data.list.length;i++){
				    		data.list[i].LisTestForm_IsEquipExist = '';
				    		for(var j=0;j<list.length;j++){
				    			if(list[j].LisEquipForm_ESampleNo == data.list[i].LisTestForm_EquipSampleNo){
				    				data.list[i].LisTestForm_IsEquipExist = '1';
				    				list.splice(j,1); //删除下标为i的元素
                                    break;
				    			}
				    		}
				    	}
				    });
				    
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			afterRender:function(that){
			}
		},me.config,formtable.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(obj){
		var me = this;
		SERACHOBJ = obj;
//		me.instance.reload({data:[]});//清空列表数据
		//查询样本号批量生成的新样本号
	    uxbasic.batchCreateSampleNo(obj.StartSampleNo,obj.GSampleNoForOrder,function(data){
	    	if(!data){
				uxbase.MSG.onWarn("请输入正确的样本号格式!");
	    		return;
	    	}
	    	var arr = data.split(',');
	    	RANGESAMPLENO ="";
	    	RANGESAMPLENOLIST = arr;
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
	
	//返回list 处理
	Class.pt.resultList = function(data){
		var me = this,
		    list = [], obj = SERACHOBJ;
	    var SampleNoArr=[],IsExistArr=[],IsEquipExistArr=[];
		
		for(var i=0;i<data.count;i++ ){
			list.push({
				LisTestForm_GTestDate:obj.GTestDate,
				LisTestForm_EquipGTestDate:obj.TGTestDate,
				LisTestForm_GSampleNo:RANGESAMPLENOLIST[i],
				LisTestForm_EquipSampleNo:TSAMPLENOLIST[i],
				LisTestForm_EquipName:obj.EquipID,
				Direction:'←',
				LisTestForm_IsEquipExist :'',
				LisTestForm_IsExist :'',
				LisTestForm_IsExec : "false",
				LisTestForm_Id:'',
				LisTestForm_EquipFormID:''
			});
			if(data.list){
				for( var j=0;j<data.list.length;j++){
					if(data.list[j].LisTestForm_GSampleNo == RANGESAMPLENOLIST[i]){
						list[i].LisTestForm_Id= data.list[j].LisTestForm_Id;
						list[i].LisTestForm_GSampleNo= data.list[j].LisTestForm_GSampleNo;
						list[i].LisTestForm_BarCode = data.list[j].LisTestForm_BarCode;
						list[i].LisTestForm_CName = data.list[j].LisTestForm_CName;
						list[i].LisTestForm_MainStatusID = data.list[j].LisTestForm_MainStatusID;
						list[i].LisTestForm_EquipFormID = data.list[j].LisTestForm_EquipFormID;
						list[i].LisTestForm_IsExist= '1';
						if(data.list[j].LisTestForm_EquipFormID)list[i].LisTestForm_IsEquipExist = '1';
						if(data.list[j].LisTestForm_MainStatusID==0){
							if(data.list[j].LisTestForm_EquipFormID)list[i].LisTestForm_IsExec = "true";
						}else{
							SampleNoArr.push('【'+RANGESAMPLENOLIST[i]+'】');
						}
		                break;
					}
				}
			}
			
			if(!list[i].LisTestForm_IsExist)IsExistArr.push('【'+RANGESAMPLENOLIST[i]+'】');
			if(!list[i].LisTestForm_IsEquipExist)IsEquipExistArr.push('【'+RANGESAMPLENOLIST[i]+'】');
		}
		me.tipMsg(SampleNoArr,IsExistArr,IsEquipExistArr);
		return list;
	};
	//不能执行原有提示（如果非状态中,样本单不存在，或者仪器样本单不存在，背景灰色显示，并提醒用户，不做错位处理。 ）
	Class.pt.tipMsg = function(SampleNoArr,IsExistArr,IsEquipExistArr){
		var me = this;
		var msg ="";
		if(SampleNoArr.length>0){
    		msg+='检验单样本号为'+SampleNoArr.join(',')+'的检验单状态不是检验中,不能执行错位处理!';
    	}
    	if(IsExistArr.length>0){
    		if(msg)msg+='<br/>';
    		msg+='检验单样本号为'+IsExistArr.join(',')+'的检验单不存在,不能执行错位处理!';
    	}
    	if(IsEquipExistArr.length>0){
    		if(msg)msg+='<br/>';
    		msg+='检验单样本号为'+IsEquipExistArr.join(',')+'的仪器样本单不存在,不能执行错位处理!';
    	}
		if (msg) uxbase.MSG.onWarn(msg);
	};
    //获取仪器样本单
	Class.pt.EquipForm =  function(callback){
		var start=SERACHOBJ.GTestDate+" 00:00:00",end =SERACHOBJ.TGTestDate+" 23:59:59";
		var where = "(lisequipform.LBEquip.Id="+$('#EquipID').val()+" and lisequipform.ETestDate>='"+start+"' and lisequipform.ETestDate<='"+end+"')";
		var	url = GET_EQIP_LIST_URL + "&where="+where;
		url += '&fields=LisEquipForm_ESampleNo';
		uxutil.server.ajax({
			url:url,
			async: false 
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				uxbase.MSG.onError(data.msg);
			}
		});
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
//		result.instance.reload({data:[]});//清空列表数据
        me.initListeners(result);
		return result;
	};
	//暴露接口
	exports('formtable',formtable);
});