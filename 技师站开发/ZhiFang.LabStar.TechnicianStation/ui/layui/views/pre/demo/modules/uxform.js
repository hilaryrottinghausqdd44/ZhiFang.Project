/**
	@name：form
	@author：liangyl
	@version 
 */
layui.define(['form','uxutil','common'],function(exports) {
	"use strict";
     var $ = layui.$,
        uxutil =layui.uxutil,
        common = layui.common,
        form =layui.form;
   
	var uxform = {
		/**获取数据服务路径*/
	    selectUrl:'',
		/**新增服务地址*/
	    addUrl:'',
	    /**修改服务地址*/
	    editUrl:'',
	    /***表单的默认状态,add(新增)edit(修改)show(查看)*/
	    formtype:'show',
	    hideTimes:2000,
		/**显示成功信息*/
		showSuccessInfo:true,
		/**保存后返回表单内容数据,*/
	    isReturnData:false,
	    /**获取表单数据*/
		getValues: function(formId) {
		    var obj = {};
			var t = $('#'+ formId+'[name]').serializeArray();
			$.each(t, function() {
				obj[this.name] = this.value;
			});
			return  obj;
		},

		/**根据主键ID加载数据
		 * filter即lay-filter 
		 * */
		load:function(filter,id){
			var me = this,
			url = me.selectUrl;
			if(!id || !url ) return;
	    	url = (url.slice(0,4) == 'http' ? '' : uxutil.path.ROOT) + url;
	    	url += (url.indexOf('?') == -1 ? "?" : "&" ) + "id=" + id;
	    	url += '&fields=' + me.getStoreFields().join(',');
			uxutil.server.ajax({
				url:url
			},function(data){
				if(data){
					me.lastData=me.changeResult(data.ResultDataValue);
					form.val(filter,me.changeResult(data.ResultDataValue));
				}else{
					layer.msg(data.msg);
				}
			});
		},
		/**创建数据字段*/
		getStoreFields:function(){
			var me = this;
			var fields = [];
			$(":input").each(function(){ 
				if(this.name)fields.push( this.name)
		    });
			return fields;
		},
		/**@overwrite 返回数据处理方法*/
		changeResult : function (data){
			var list =  JSON.parse(data);
			return list;
		},
		/**@overwrite 获取新增的数据*/
		getAddParams:function(obj){
			return;
		},
		/**@overwrite 获取修改的数据*/
		getEditParams:function(obj){
			return;
		},
		/**@overwrite 重置按钮点击处理方法*/
		onResetClick:function(filter){
			var me = this;
			if(!me.PK){
				this.getForm().reset();
			}else{
                form.val(filter,me.changeResult(me.lastData));
			}
		},
		/**保存按钮点击处理方法*/
		onSaveClick:function(obj){
			var me = this;
			var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
			var params = me.formtype == 'add' ? me.getAddParams(obj) : me.getEditParams(obj);
			if(!params) return;
			var id = params.entity.Id;
			params =common.JSON.encode(params);
			var indexs=layer.load(2);//显示遮罩层
			uxutil.server.ajax({
				type:'post',
				url: url,
				data:params
			}, function(data){
				layer.close(indexs);//隐藏遮罩层
				if(data.success){
					id = me.formtype == 'add' ? data.value : id;
					if(common.typeOf(id) == 'object'){
						id = id.id;
					}
					if(me.isReturnData){
						layui.event("save", "click", me.returnData(id));
					}else{
						layui.event("save", "click",id);
					}
					if(me.showSuccessInfo) layer.msg('保存成功',{icon:1,time:me.hideTimes});
				}else{
				    layui.event("saveerror", "saveerror",id);
					layer.msg(data.msg, {icon: 2});
				}
			});
		},
		/**保存后返回数据*/
		returnData:function(id){
			var me = this,
				data = me.getAddParams();
				
			if(data.entity){
				data = data.entity;
			}
			data.Id = id;
			return data;
		}
	};

	//暴露接口
	exports('uxform', uxform);
});
