layui.extend({
  uxutil: 'ux/util'
}).define(['form', 'layer', 'uxutil'], function(exports){
	"use strict";
	var $ = layui.jquery;
	var form = layui.form;
	var layer = layui.layer;
	var uxutil = layui.uxutil;
	var selInputValue = '';
	var selctWorknoElem = null;
	var userform = {
    	selPuserUrl: uxutil.path.ROOT + '/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByHQL'
    };
    
    //护工选择输入按键事件，输入工号按回车直接选择护工功能兼容layui控件的功能
	function oNselectInputEvent(e){
	  var input = e.target;
	  var keyCode = e.keyCode;
	  var value = input.value; //如果是按方向键再按回车键这个值会马上改变，如果不是这个值保持原值
	  //回车键
	  if (keyCode === 13){
	  	if (selctWorknoElem == null || !selctWorknoElem) return;
	  	var dlElem = selctWorknoElem.find('dl');
	  	if (dlElem.length <= 0) return;
	  	var dds = dlElem.find('dd[class=""]'); //查找没有被隐藏的元素
	  	if (dds.length <= 0) return;
	  	//如果两个值相等说明没有按方向键和回车键选择值，触发选择第一个元素
	  	if (selInputValue === value) dds[0].click();
	    //缓存选择后的值，用来判断使用方向按钮回车选择就不执行dds[0].click()；否则执行；  	
	  	selInputValue = input.value; 
	  };
	};
	
	//初始用户
	userform.initUser = function(){
		var me = this;	
		var workno_elem_Id = "#user_workno";
		var html = '';
    	var name = '';
    	var Id = '';
    	var ShortCode = '';
    	var fields = ['PUser_Id','PUser_CName', 'PUser_ShortCode'];
    	var idField = fields[0];
    	var nameField = fields[1];
    	var ShortField = fields[2];
    	me.GetUser(function(res){
    		html = '<option value=""></option>';
	     	for(var i=0; i < res.length; i++){
	    		Id = res[i][idField];
	    		ShortCode = res[i][ShortField];
	    		name = res[i][nameField] + '-' + ShortCode;
	    		html = html + '<option value="' + Id + '">' + name  +'</option>';
	    	};
	    	if (html){
	    		var selectele = $(workno_elem_Id).empty().append(html);
	    		form.render('select');
	    		//这里注册回车事件,查找替代元素的的兄弟元素标题元素找到input元素
	    		selctWorknoElem = $(workno_elem_Id).siblings("div.layui-form-select");
	    		var titleEle = selctWorknoElem.find('div.layui-select-title');
	    		var inputEle = titleEle.find('input');
	    		inputEle.on('keydown', oNselectInputEvent);
	    	};   		
    	});
	};
	
	//获取用户数据
	userform.GetUser = function(callback){
		var me = this;
		var url = me.selPuserUrl;
		var data = {page: 0, limit: 10000, isPlanish:true};;
		var where = "puser.Visible = 1"; //待定
		var fields = ['PUser_Id','PUser_CName', 'PUser_ShortCode'];
		data["fields"] = fields.join(',');
		data["where"] = where;
		var config = {
			type: 'get',
        	url: url,
        	data: data
		};
		//查询数据
		uxutil.server.ajax(config, function(data) {
			if (data.success){
				callback && typeof callback === 'function' && callback(data.value.list);
			} else{
				layer.msg(data.msg);
			};
		});
	};
	
	exports('userform', userform);
})
