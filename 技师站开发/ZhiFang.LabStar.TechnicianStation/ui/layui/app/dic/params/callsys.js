/**
	@name：叫号系统数据更新
	@author：liangyl
	@version 2021-10-19
	BH||LBSickType (下拉选择+数字输入框)
           数据格式 A231-3000,A236-6000
 */
layui.extend({
	uxutil: 'ux/util',
	xmSelect: '../src/xm-select'
}).use(['uxutil','form','table','xmSelect'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
		
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//列表数据(查字典数据)
    var SELECT_DATA = [];
    //获取子窗体值
    var BHDataStr = parent.getBHValue();
    //选择列表实例
    var table_ind = null;
	//初始化选择列表
	function initHtml(){
		//列表配置
		var config = {
			elem: '#callsys_table',
			height:'full-65',
			title:'',
			page: false,
			limit: 500,
			loading : true,
			size:'sm',
			cols:[[
			    {type:'checkbox'},
				{field:'value',title:'value',minWidth:150,flex:1,hide:true},
				{field:'name',title:'号段类型编码',minWidth:150,flex:1},
		        {field:'SerialNo',title:'排队号起始位',minWidth:150,flex:1,edit:'text'}
	
			]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
			}
		};
	    //列表实例
	    table_ind = table.render(config);
		table_ind.reload({data:SELECT_DATA});
	}
	//事件监听
	function initListeners(){
		//监听行单击事件
		table.on('row(callsys_table)', function(obj){
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		});
		 // 监听keyup事件
		$(document).on('keyup', 'td[data-field="SerialNo"]>input.layui-table-edit', function (event) {
			validateNum(event,this);
		});

		//按钮事件
		var active = {
			save: function() {//选择、
				var data = table.checkStatus('callsys_table').data; 
				//校验
				var isExec = isValid(data);
				if(!isExec){
					layer.msg('勾选数据行的排队号起始位不能为空,请检查！');
					return false;
				}
				var obj = getParams(data);
				parent.layer.closeAll('iframe');
				parent.afterUpateBH(obj,PARAMS.ID,PARAMS.NAME,PARAMS.NUM);
			},
			close: function() {//关闭close
				parent.layer.closeAll('iframe');
			}
		};
		$('.layui-btn-group .layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
	}
	//验证不能为空
	function isValid(data){
		var isExec = true;
		for(var i=0;i<data.length;i++){
			if(!data[i].SerialNo){
		    	isExec = false;
		    	break;
		    }
		}
		return isExec;
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
        //不能输入0开头的数据
        var patrn=/^([1-9]\d*|0)(\.\d*[1-9])?$/;
        if (!patrn.exec(obj.value)){obj.value=0;}
        obj.value=Number(obj.value);
	}
	//保存的格式为A231-3000,A236-6000
	function getParams(data){
		var ids = [],names = [];
		for(var i=0;i<data.length;i++){
			var value = data[i].value+'-'+data[i].SerialNo;
			ids.push(value);
			names.push(data[i].name+'-'+data[i].SerialNo);
		}
		return {id:ids.join(','),name:names};
	}
    //数据处理
    function initData(data){
    	var list = [];
    	var id_data = data.id;//还原值
    	var data = data.data;//列表数据 
    	if(id_data)id_data = id_data.split(','); //还原数据
    	//还原值处理
    	var sp_arr =[];
    	for(var i=0;i<id_data.length;i++){
			var str1 = id_data[i].split('-');
			var obj = {
				value :str1[0],
				SerialNo:str1[1]
			}
			sp_arr.push(obj);
		}
    	//还原匹配列表
    	for(var i =0;i<data.length;i++){
    		var isExec =true;
    		data[i].LAY_CHECKED = false;
    		for(var j=0;j<sp_arr.length;j++){
    			if(data[i].value == sp_arr[j].value){
    				data[i].LAY_CHECKED = true;
    				data[i].SerialNo = sp_arr[j].SerialNo;
    				sp_arr.splice(j, 1); 
    				break;
    			}
    		}
    		list.push(data[i]);
    	}
    	return list;
    }
	//初始化数据
	function init(){
        var data = BHDataStr ? BHDataStr : [];
		SELECT_DATA = initData(data);//数据处理
		initHtml();//创建列表实例
		initListeners();//事件监听
	}
	//初始化
	init();
});