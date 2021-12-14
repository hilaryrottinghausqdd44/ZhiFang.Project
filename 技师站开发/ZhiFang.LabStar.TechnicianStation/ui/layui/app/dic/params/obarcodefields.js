/**
	@name：弹出选择
	@author：liangyl
	@version 2021-08-02
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','table','form'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
		
	//获取自定义数据集枚举信息（病人信息分组/核收条件)
	var GET_FIELDS_URL= uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_QueryOrderBarCodeSelectFields';
     //外部参数
	var PARAMS = uxutil.params.get(true);
	//MODEL模式1-核收条件选择,模式2-病人信息分组规则,模式3-列表配置
	var MODEL = PARAMS.MODEL;
	
    //核收条件选择  --- 类型字典编码
	var PARATYPECODE_MODEL = "Pre_OrderBarCode_Check_Fields";
	//组件name
	var ID =PARAMS.ID;
	//列表数据
    var DEFAULT_DATA = [];
    //AL1模式是否单选,默认多选'checkbox'
    var CHOICE = PARAMS.CHOICE;
	 //获取子窗体值
    var ALDataStr = parent.getAlValue();
	//列表配置
	var config = {
		elem: '#obarcodefields_table',
		height:'full-65',
		title:'',
		page: false,
		limit: 500,
		loading : true,
		size:'sm',
		cols:[[
		    {type:'checkbox'},
	        {field:'Field',title:'字段',minWidth:150,flex:1},				
			{field:'FieldName',title:'字段名',minWidth:150,flex:1,edit: 'text'},
			{field:'width',title:'列宽',width:100,edit: 'text'},
			{field:'IsShow',title:'是否显示',width:100,templet: '#switchTpl'},
			{field:'DefaultDisplay',title:'显示次序',width:100,edit: 'text'},
			{field:'TalbeName',title:'字段存在表',minWidth:150,flex:1,hide:true},
			{field:'Memo',title:'备注',width:100}
		]],
		text: {none: '暂无相关数据' },
		done: function(res, curr, count) {
			// 隐藏列 ----模式0和模式1 隐藏 列宽与是否显示
		    if(MODEL=='AL1' || MODEL=='AL2'){
		    	$(".layui-table-box").find("[data-field='width']").css("display","none");
		    	$(".layui-table-box").find("[data-field='IsShow']").css("display","none");
		    	$(".layui-table-box").find("[data-field='DefaultDisplay']").css("display","none");
		    }
		    //模式2 字段名不允许修改
	        if(MODEL=='AL2')$(".layui-table").find('td').data('edit', false);
		}
	};
	//模式1 单选
	if(MODEL=='AL1' && CHOICE=='radio')config.cols[0][0] = {type:'radio'};
	//列表实例
	var tableInd = table.render(config);
	
	//监听行单击事件
	table.on('row(obarcodefields_table)', function(obj){
		//标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');

	});
	form.on('switch(IsShow)', function(data){
        var elem = data.othis.parents('tr');
  	    var dataindex = elem.attr("data-index");
  	    var value =  data.elem.checked ? 'true' : 'false';
	  	layui.$.extend(table.cache['obarcodefields_table'][dataindex],{'IsShow' : value});
    });  
    // 监听keyup事件
	$(document).on('keyup', 'td[data-field="width"]>input.layui-table-edit', function (event) {
		validateNum(event,this);
	});
	//按钮事件
	var active = {
		save: function() {//选择、
			var data = onAccptClick();
			parent.layer.closeAll('iframe');
			parent.afterUpateAL(data,ID,PARAMS.NUM);
		},
		clear: function() {//选择、
			var data = '';
			parent.layer.closeAll('iframe');
			parent.afterUpateAL(data,ID,PARAMS.NUM);
		},
		close: function() {//关闭close
			parent.layer.closeAll('iframe');
		}
	};
	$('.layui-btn-group .layui-btn').on('click', function() {
		var type = $(this).data('type');
		active[type] ? active[type].call(this) : '';
	});
    //数据加载
	function onSearch(){
		var  me = this;
		var index =layer.load();
	    //获取类型列表
		onLoadDataList(function(data){
			layer.close(index);
			if(data.success){
				var list = data.value || [];
                list = resultData(list);
				tableInd.reload({data:list});
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		}); 
	}
	//数据还原 处理
	function resultData(list){
		//默认数据
		DEFAULT_DATA = initData();
		for(var i=0;i<list.length;i++){
			var TalbeName = list[i].TalbeName+'.'+list[i].Field;
			if(MODEL=='AL3'){
				TalbeName = list[i].TalbeName+'_'+list[i].Field;
			}
			list[i].LAY_CHECKED = false;
			for(var j=0;j<DEFAULT_DATA.length;j++){
                if(DEFAULT_DATA[j].TalbeName == TalbeName){
					list[i].LAY_CHECKED = true;
					list[i].FieldName = DEFAULT_DATA[j].FieldName;
					if(DEFAULT_DATA[j].width)list[i].width = DEFAULT_DATA[j].width;
					if(DEFAULT_DATA[j].IsShow)list[i].IsShow = DEFAULT_DATA[j].IsShow;
					if(DEFAULT_DATA[j].DefaultDisplay)list[i].DefaultDisplay = DEFAULT_DATA[j].DefaultDisplay;
					DEFAULT_DATA.splice(j, 1); 
					break;
				}
			}
		}
		return list;
	}
	//获取类型列表
	function onLoadDataList(callback){
		var url  =  GET_FIELDS_URL+'?paraTypeCode='+PARATYPECODE_MODEL;
		uxutil.server.ajax({
			url:url
		},function(data){
			callback(data);
		});
	}
	//确定保存
	function onAccptClick(){
		var checkStatus = table.checkStatus('obarcodefields_table');
        var data = checkStatus.data;
        var entity ='';
    	switch (MODEL){
    		case 'AL2':
    		    entity = getEntity2(data);
    			break;
    		case 'AL3':
    		    entity = getEntity3(data);
    			break;
    		default:
    		    entity = getEntity1(data);
    			break;
    	}
    	return entity;
    }
	//获取模式1选择数据
	function getEntity1(data){
		var str = "",ids = [];
		for(var i=0;i<data.length;i++){
			var str1  = data[i].TalbeName+'.'+data[i].Field + '&'+data[i].FieldName+'&'+data[i].Memo;
			ids.push(str1);
		}
		if(ids.length>0)str = ids.join(',');
		return str;
	}
	//获取模式2选择数据
	function getEntity2(data){
		var str = "",ids = [];
		for(var i=0;i<data.length;i++){
			var str1  = data[i].TalbeName+'.'+data[i].Field;
			ids.push(str1);
		}
		if(ids.length>0)str = ids.join(',');
		return str;
	}
	//获取模式3选择数据
	function getEntity3(data){
		var str = "",ids = [];
		var arr = data.sort(compare('DefaultDisplay'));
		for(var i=0;i<arr.length;i++){
			var IsShow =arr[i].IsShow+'';
			var str2= IsShow=='true' ? 'show' : 'hide';
			var str1  = arr[i].TalbeName+'_'+arr[i].Field+ '&'+arr[i].FieldName+'&'+data[i].width+'&'+str2;
			ids.push(str1);
		}
		if(ids.length>0)str = ids.join(',');
		return str;
	}
	//初始化模式
	function mode1(){
		//模式1  核收条件类型编码从外部传入
		if(MODEL=='AL1' || MODEL=='AL2' )PARATYPECODE_MODEL  = PARAMS.PARANAME;
//		模式2
//  	if(MODEL=='AL2')PARATYPECODE_MODEL  = 'Pre_OrderBarCode_OrderGrouping_Fields';
    	//模式3
    	if(MODEL=='AL3')PARATYPECODE_MODEL  = 'Pre_OrderBarCode_BarCodeTable_Fields';
	}
	/**
	 * 为输入框校验合法数字
	 * @param event
	 * @param obj
	 */
	function validateNum(event, obj) {
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
	}
	function compare(property){
	    return function(a,b){
	        var value1 = a[property];
	        var value2 = b[property];
	        return value1 - value2;
	    }
	}
    function initData(){
        var data = ALDataStr || "";
    	var ids = [];
    	if(data && data.length>0){
			var arr1 = data.split(',');
			switch (MODEL){
	    		case 'AL2': //模式2
					for(var i=0;i<arr1.length;i++){
	    		    	var m2 = arr1[i].split('.');
	    		    	var obj ={};
	    		    	if(m2.length>0){
	    		    		obj.Field = m2[1];
	    		    		obj.TalbeName = m2[0]+'.'+m2[1];
	    		    	}
	    		    	ids.push(obj);
	    		    }
	    			break;
	    		case 'AL3': //模式3
	    		    for(var i=0;i<arr1.length;i++){
	    		    	var m2 = arr1[i].split('&');
	    		    	var obj = {};
	    		    	if(m2.length>0){
	    		    		var field = m2[0].split('_');
	    		    		obj.TalbeName = m2[0];
	    		    		obj.Field = field[field.length-1];
	    		    		obj.FieldName = m2[1];
	    		    		obj.width = m2[2];
	    		    		obj.IsShow = m2[3]=='show' ? 'true' : 'false';
	    		    		obj.DefaultDisplay = i+	1;
	    		    	}
	    		    	ids.push(obj);
	    		    }
	    			break;
	    		default: //模式1
				    for(var i=0;i<arr1.length;i++){
				    	var obj={};
				    	var m3 = arr1[i].split('&');
				    	var m31 = m3[0].split('.');
				    	if(m31[1].length>0)obj.Field = m31[1];
				    	obj.FieldName = m3[1];
				    	obj.TalbeName = m31[0]+'.'+m31[1];
				    	ids.push(obj);
	    		    }
	    			break;
	        }
		}
    	return ids;
    }
    //初始化数据
	function init(){
		
		mode1();
        onSearch();
	}
	//初始化
	init();
});