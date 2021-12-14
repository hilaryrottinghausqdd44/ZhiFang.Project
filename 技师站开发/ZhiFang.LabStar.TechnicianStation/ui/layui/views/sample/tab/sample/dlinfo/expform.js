/**
 * @name：参考范围后处理
 * @author liangyl
 * @version 2021-05-21
 */
layui.extend({
}).define(['uxutil', 'form','uxbase'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		form = layui.form;
	
	//获取项目结果后处理
    var GET_ITEM_RANGE_EXP_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeExpByHQL?isPlanish=true';
    //获取字典数据
    var GET_DICT_LIST_URL = uxutil.path.ROOT +'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDictByHQL?isPlanish=true';
    var expform_ind =null;
	var expform = {
		//参数配置
		config:{
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
		},me.config,expform.config,setings);
	};
	
	Class.pt = Class.prototype;

	//获取参考范围后处理
	Class.pt.loadData  = function(id,callback){
		var fields = ['JudgeValue','ResultStatus','ResultReport','ResultComment'],
			url = GET_ITEM_RANGE_EXP_LIST_URL + '&where=lbitemrangeexp.LBItem.Id='+id+ ' and lbitemrangeexp.JudgeType=0';
		url += '&fields=LBItemRangeExp_' + fields.join(',LBItemRangeExp_');
		
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	};
	//创建表单
	Class.pt.createCom  = function(list){
		var me = this;
		me.DictList(function(data){
			var html = '',
			    ResultStatus = '' ;
			for(var i=0;i<list.length;i++){
				for(var j =0;j<data.length;j++){
					if(list[i].LBItemRangeExp_ResultStatus == data[j].LBDict_DictCode){
						ResultStatus = data[j].LBDict_CName;
						break;
					}
				}
				html+='<hr class="layui-bg-green" /> '+
	           '<div class="layui-form expform"> '+
	            '<div class="layui-form-item" style="margin-bottom: 0px;">'+ 
	              '<div class="layui-col-xs3"> '+
		            '<label class="layui-form-label">正常时状态为</label> '+
		            '<div class="layui-input-block"> '+
		             '<label>'+ResultStatus+'</label>'+
		            '</div> '+
	              '</div>'+
	              '<div class="layui-col-xs3">'+ 
	                '<label class="layui-form-label">报告值</label> '+
	                '<div class="layui-input-block"> '+
	                '<label>'+list[i].LBItemRangeExp_ResultReport+'</label>'+
	                '</div> '+
	              '</div>'+
	               '<div class="layui-col-xs3">'+ 
	                '<label class="layui-form-label">结果说明</label> '+
	                '<div class="layui-input-block"> '+
	                  '<label>'+list[i].LBItemRangeExp_ResultComment+'</label>'+
	                '</div> '+
	              '</div>'+
	           ' </div> '+
	          '</div> ';
			}
			$('#expform').html(html);
		});
	};
	//字典查询
	Class.pt.DictList = function(callback){
		var fields = ['CName','DictCode'],
			url = GET_DICT_LIST_URL+"&where=IsUse=1 and DictType='ResultStatus'";
		url += '&fields=LBDict_' + fields.join(',LBDict_');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	}
	
	
	//主入口
	expform.render = function(options){
		expform_ind = new Class(options);
		//初始化下拉框
	    expform_ind.loadData = Class.pt.loadData;
	    expform_ind.createCom = Class.pt.createCom;
		return expform_ind;
	};
	//暴露接口
	exports('expform',expform);
});