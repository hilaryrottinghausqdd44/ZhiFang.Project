/**
 * @name：项目信息
 * @author liangyl
 * @version 2021-05-21
 */
layui.extend({
}).define(['uxutil','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form;
	
    //获取项目服务路径
	var GET_ITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?isPlanish=true';
    //专业
	var GET_SPECIALTY_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?isPlanish=true';

    var info_ind =null;
	var infoform = {
		//参数配置
		config:{
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
		},me.config,infoform.config,setings);
	};
	
	Class.pt = Class.prototype;

	//获取项目
	Class.pt.loadData  = function(id,callback){
		var fields = ['CName','SName','EName','DiagMethod','Unit','SpecialtyID','ItemCharge','SamplingRequire','IsCalcItem','GroupType','ClinicalInfo','Comment'],
			url = GET_ITEM_LIST_URL + "&where=lbitem.Id="+id;
		url += '&fields=LBItem_' + fields.join(',LBItem_');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	};
	//创建表单
	Class.pt.createCom  = function(list,callback){
		var me = this;
		me.SpecialtyList(function(data){
			var html = '',
			    Specialty = '' ;
			for(var i=0;i<list.length;i++){
				for(var j =0;j<data.length;j++){
					if(list[i].LBItem_SpecialtyID == data[j].LBSpecialty_Id){
						Specialty = data[j].LBSpecialty_CName;
						break;
					}
				}
				html+='  <div class="layui-col-xs6"> '+
		         '<div class="layui-form myform" >'+
		          '<div class="layui-form-item"> '+
		           '<label class="layui-form-label" style="color: blue;font-size: large;">检验项目:</label> '+
		           '<div class="layui-input-block"> '+
		           '<label style="color: blue;font-size: large;">'+list[i].LBItem_CName+'</label>'+
		           '</div>'+ 
		          '</div>'+ 
		          '<div class="layui-form-item"> '+
		          '<div class="layui-col-xs6"> '+
		            '<label class="layui-form-label">项目简称:</label>'+ 
		            '<div class="layui-input-block"> '+
		              '<label>'+list[i].LBItem_SName+'</label>'+
		            '</div> '+
		           '</div> '+
		           '<div class="layui-col-xs6"> '+
		            '<label class="layui-form-label">项目英文名:</label> '+
		            '<div class="layui-input-block"> '+
		             '<label>'+list[i].LBItem_EName+'</label>'+
		            '</div> '+
		           '</div> '+
		          '</div> '+
		          '<div class="layui-form-item"> '+
		           '<div class="layui-col-xs6"> '+
		            '<label class="layui-form-label" style="width: 90px;">默认检验方法:</label> '+
		            '<div class="layui-input-block" style="padding-left: 10px;"> '+
		             '<label>'+list[i].LBItem_DiagMethod+'</label>'+
		            '</div> '+
		           '</div>'+ 
		           '<div class="layui-col-xs6">  '+
		            '<label class="layui-form-label" style="width: 90px;">默认结果单位:</label>  '+
		            '<div class="layui-input-block" style="padding-left: 10px;">  '+
		             '<label>'+list[i].LBItem_Unit+'</label>'+
		            '</div> '+
		           '</div> '+
		          '</div> '+
		          '<div class="layui-form-item">'+ 
		           '<div class="layui-col-xs6">'+ 
		            '<label class="layui-form-label">专业:</label> '+ 
		            '<div class="layui-input-block"> '+ 
		             '<label>'+Specialty+'</label>'+
		            '</div> '+
		           '</div> '+
		           '<div class="layui-col-xs6">'+ 
		           ' <label class="layui-form-label">项目价格:</label> '+
		            '<div class="layui-input-block"> '+
		            '<label>'+list[i].LBItem_ItemCharge+'</label>'+
		            '</div> '+
		           '</div> '+
		          '</div> '+
		         ' <div class="layui-form-item"> '+
		          '<div class="layui-col-xs12"> '+
		            '<label class="layui-form-label">采样要求:</label> '+
		           ' <div class="layui-input-block"> '+
		           		'<label>'+list[i].LBItem_SamplingRequire+'</label>'+
		            '</div> '+
		           '</div>'+ 
		          '</div> '+
		         '</div> '+
		        '</div> '+
		        '<div class="layui-col-xs6" style="padding-left: 10px;"> '+
		        ' <span> 临床意义</span> '+
		         '<textarea style="height: 50px;min-height: 40px; " value="'+list[i].LBItem_ClinicalInfo+'" class="layui-textarea" readonly="readonly" ></textarea>'+ 
		         '<span> 备注</span> '+
		        ' <textarea  value="'+list[i].LBItem_Comment+'" style="height: 50px;min-height: 40px; " class="layui-textarea" readonly="readonly" ></textarea> '+
		        '</div> ';
			}
			callback(html);
			
		});
	};
	//专业查询
	Class.pt.SpecialtyList = function(callback){
		var fields = ['CName','Id'],
			url = GET_SPECIALTY_LIST_URL+'&where=lbspecialty.IsUse=1';
		url += '&fields=LBSpecialty_' + fields.join(',LBSpecialty_');
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg);
			}
		});
	}
	
	
	//主入口
	infoform.render = function(options){
		info_ind = new Class(options);
		//初始化下拉框
	    info_ind.loadData = Class.pt.loadData;
	    info_ind.createCom = Class.pt.createCom;
		return info_ind;
	};
	//暴露接口
	exports('infoform',infoform);
});