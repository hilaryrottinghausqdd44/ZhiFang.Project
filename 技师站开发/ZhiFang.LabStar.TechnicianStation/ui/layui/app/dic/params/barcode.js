/**
	@name：弹出根据条码号获取对应就诊类型
	@author：liangyl
	@version 2021-08-02
	BC||LBSickType 就诊类型下拉选择
           数据格式 [{"条码长度": "16","截取起始位": "5","截取长度":"2","值": {"01": "总院体检","02": "埌东体检"}},{"条码长度": "10","截取起始位": "9","截取长度":"2","值": {"44": "总院门诊","55": "总院住院","22": "埌东门诊","33": "埌东住院"}}]
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
	//下拉框数据
    var SELECT_DATA = [];
    //列表数据
    var DATA_LIST = [];
    var ENUM ={};
    //选择列表实例
    var table_ind = null;
     //获取子窗体值
    var BCDataStr = parent.getBCValue();
	//初始化选择列表
	function initHtml(){
		//列表配置
		var config = {
			elem: '#barcode_table',
			height:'full-65',
			title:'',
			page: false,
			limit: 500,
			loading : true,
			size:'sm',
			cols:[[
				{field:'条码长度',title:'条码长度',width:100,edit:'text'},
				{field:'截取起始位',title:'截取起始位',width:100,edit:'text'},	
				{field:'截取长度',title:'截取长度',width:100,edit:'text'},
				{field:'值',title:'值',minWidth:150,flex:1, templet: function (data) {
		            var str = 
		              '<div id="select_'+data.LAY_TABLE_INDEX+'"class="layui-form layui-form-pane xm-select layui-table-edit2" style="position: absolute;left:0; top: 0; width:100%;height:28px;margin:-5px 0px 0px 0px;padding:0px;"></div>';
		            return str;
		        }},
		        {field:'valus',title:'值',minWidth:150,flex:1,hide:true},
		        {fixed: 'right', title:'操作', toolbar: '#barDemo', width:80}
			]],
			text: {none: '暂无相关数据' },
			done: function(res, curr, count) {
				for(var i=0;i<res.data.length;i++){
					initSelect(res.data[i]);
				}
			}
		};
	    //列表实例
	    table_ind= table.render(config);
		table_ind.reload({data:DATA_LIST});
	}
	//事件监听
	function initListeners(){
		//监听行单击事件
		table.on('row(barcode_table)', function(obj){
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
		});
		//监听行工具事件
		table.on('tool(barcode_table)', function(obj){
		    var data = obj.data;
		    if(obj.event === 'del'){
		        obj.del();
		        var list = table.cache['barcode_table'];
		        for(var i=0;i<list.length;i++){
		        	if(JSON.stringify(list[i])=='[]'){//已删除的行数据
		        		list.splice(i,1);
		        	}
		        }
				table_ind.reload({data:list});
		    }
		});
		 // 监听keyup事件
		$(document).on('keyup', 'td[data-field="条码长度"]>input.layui-table-edit', function (event) {
			validateNum(event,this);
		});
		$(document).on('keyup', 'td[data-field="截取起始位"]>input.layui-table-edit', function (event) {
			validateNum(event,this);
		});
		$(document).on('keyup', 'td[data-field="截取长度"]>input.layui-table-edit', function (event) {
			validateNum(event,this);
		});
		//按钮事件
		var active = {
			add:function(){ //新增空行
				var list = table.cache['barcode_table'];
	            list.push({条码长度:"",截取起始位:"",截取长度:"",值:""});
				table_ind.reload({data:list});
			},
			save: function() {//选择、
				var data = table.cache['barcode_table']; 
				//校验
				var isExec = isValid(data);
				if(!isExec){
					layer.msg('数据都是必填,请检查！');
					return false;
				}
				var list = getParams(data);
				parent.layer.closeAll('iframe');
				parent.afterUpateBC(list,PARAMS.ID,PARAMS.NUM);
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
			if(!data[i].条码长度 || !data[i].截取起始位 || !data[i].截取长度 || !data[i].值){
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
        obj.value = Number(obj.value);
	}
	function getId(value){
		var arr = value.split(',');
		var str = "";
		for(var i =0;i<arr.length;i++ ){
			for(var j=0;j<SELECT_DATA.length;j++){
				if(arr[i]==SELECT_DATA[j].value){
					if(i>0)str+=",";
					str+= '"'+SELECT_DATA[j].value+'":"'+SELECT_DATA[j].name+'"';
					break;
				}
			}
		}
		return '{'+str+'}';
	}
	function getParams(data){
		var list = [];
		for(var i=0;i<data.length;i++){
			var value = getId(data[i].值); //值解析
			list.push({条码长度:data[i].条码长度,截取起始位:data[i].截取起始位,截取长度:data[i].截取长度,值:value});
		}
		var str = JSON.stringify(list);
	  	if(list.length>0){
	  		str = str.replace(/\\/g, "");
            str = str.replace(/}"/g, "}");
            str = str.replace(/"{/g, "{");
	  	}
		return str;
	}
	//初始化下拉数据组件
    function initSelect(d){
    	var obj ={
			el: '#select_'+d.LAY_TABLE_INDEX,
			language: 'zn',
			data: SELECT_DATA,
		    size: 'mini'
		};
		obj.on = function(data2){ //联动处理
			//arr:  当前多选已选中的数据
			var arr =  data2.arr;//d.ids;
		  	var tableCache = table.cache['barcode_table'];
		  	var list = [],list_name =[],ids="";
		  	for(var i=0;i<arr.length;i++){
		  		list.push(arr[i].value);
		  	}
		  	if(list.length>0)ids = list.join(',');
	  		//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
		  	layui.$.extend(table.cache['barcode_table'][d.LAY_TABLE_INDEX],{'值' : ids});
		};
		var demo1 = xmSelect.render(obj);
	    demo1.setValue(setComValue(d))
     }
     function setComValue(d){
    	var arr = [],data=[];
	    if(d.值)data = d.值.split(',');
        for(var i=0;i<SELECT_DATA.length;i++){
			for(var j=0;j<data.length;j++){
				if(SELECT_DATA[i].value==data[j]){
				   arr.push({name:SELECT_DATA[i].name, value: SELECT_DATA[i].value});
				   break;
				}
			}
		}
        return arr;
    }
    //数据处理
    function initData(data){
    	var list = [];
    	for(var i=0;i<data.length;i++){
    		var ids = [];
    		var obj = data[i].值;
    		var temp = "";
    		for(var j in obj){//用javascript的for/in循环遍历对象的属性
				ids.push(j);
			}
    		data[i].值  ="";
    		if(ids.length>0)data[i].值 = ids.join(',');
    		list.push(data[i]);
    	}
    	return list;
    }
	//初始化数据
	function init(){
        var data = BCDataStr ? BCDataStr : [];
		SELECT_DATA = data.data;//下拉数据
		DATA_LIST = initData(data.id);
		initHtml();//创建列表实例
		initListeners();//事件监听
	}
	//初始化
	init();
});