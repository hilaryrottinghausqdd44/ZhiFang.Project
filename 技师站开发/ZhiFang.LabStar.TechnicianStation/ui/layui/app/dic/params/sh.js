/**
	@name：弹出sh选择
	@author：liangyl
	@version 2021-08-02
	SH||LBSamplingGroup   列表不包含下拉框操作列  存的值为  显示选定|选择行号id
	SH||LBSickType|0&HIS获取#1&LIS获取    包含下拉框操作列   0&HIS获取#1&LIS获取 为下拉框内容    存的值为  具体行id|行下拉内容
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','form','table','layer'], function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		layer =  layui.layer,
		form = layui.form;
		
	//外部参数
	var PARAMS = uxutil.params.get(true);
	//默认设置
    var DEFAULT_DATA = [];
    //是否允许配置下拉数据
    var SELECT_DATA = "";
    var ENUM ={};
    
	//列表配置
	var config = {
		elem: '#sh_table',
		height:'full-50',
		title:'',
		page: false,
		limit: 500,
		loading : true,
		size:'sm',
		cols:[[
		    {type:'checkbox'},
	        {field:'value',title:'主键',minWidth:150,flex:1,hide:true},				
			{field:'name',title:'名称',minWidth:150,flex:1},
			{field:'cllist',title:'下拉选择',minWidth:100,flex:1,align:'center', templet: function (data) {
	             var str = 
	             '<select id="cllist'+data.LAY_TABLE_INDEX+'" lay-filter="cllist"></select> ';
	            return str;
	        }}
		]],
		text: {none: '暂无相关数据' },
		done: function(res, curr, count) {
			$(".layui-table-box").find("[data-field='cllist']").css("display","none");
			initComList(res.data);  //里边内下拉框初始化
		}
	};
	//列表实例
	var tableInd = table.render(config);
	tableInd.reload({data:[]});
	
	$('#save_SH').on('click', function() {
		var checkStatus = table.checkStatus('sh_table'),
		    data = checkStatus.data;
        if(SELECT_DATA){  //列表带下拉返回数据
        	var isExec = true;
	    	//校验,下拉框数据不能为空
	    	for(var i=0;i<data.length;i++){
	        	var  cl = data[i].cllist;
	        	var v1 = "";
	        	if(cl){
	        		if(ENUM != null)v1 = ENUM[cl];
	        		if(v1=='undefined' || v1==undefined){
	        			isExec = false;
	        			break;
	        		}
	        	}
	        }
	    	if(!isExec){
	    	    layer.msg("下拉选择不能为空", { icon: 5, anim: 0 });
	    		return false;
	    	}
        	var obj = getComSH(data);
        	parent.layer.closeAll('iframe');
            parent.afterUpateSH(obj,PARAMS.ID,PARAMS.NAME,PARAMS.NUM);
        }else{ //不带下 拉返回数据
        	var obj = getSH(data);
            parent.layer.closeAll('iframe');
            parent.afterUpateSH(obj,PARAMS.ID,PARAMS.NAME,PARAMS.NUM);
        }
	});
	//清空
	$('#clear_SH').on('click', function() {
		parent.layer.closeAll('iframe');
        parent.afterUpateSH({id:'',name:''},PARAMS.ID,PARAMS.NAME,PARAMS.NUM);
	});
	
	form.on('select(cllist)', function(data){
    	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
		var elem = data.othis.parents('tr');
	  	var dataindex = elem.attr("data-index");
    	layui.$.extend(table.cache['sh_table'][dataindex],{'cllist' : data.value});
	});
	
	//具体下拉数据赋值
	function initComList(data){
		if(SELECT_DATA){
			$("#check_row").addClass('layui-hide');    //隐藏显示选定
			$(".layui-table-box").find("[data-field='cllist']").css("display","block");
			//下拉列表渲染
			for(var i=0;i<data.length;i++){
				var id = 'cllist'+data[i].LAY_TABLE_INDEX;
		        $("#"+id).append(comList());
		        $("#"+id).val(data[i].cllist)
				form.render('select');
			}
		}
	}

    //不含下拉框选择返回数据
    function getSH(data){
    	 var isCheck =$("input[name='isCheck']").prop("checked");
        var check = isCheck ? true : false;
        var str = check ? 1 : 2 ;
        var ids=[],names=[];
        for(var i=0;i<data.length;i++){
        	ids.push(data[i].value);
        	names.push(data[i].name);
        }
        var obj={id: str+'|'+ids.join(','),name: str+'|'+names.join(',')};
        return obj;
    }
    
     //含下拉框选择返回数据   格式1001&1,1002&1,1003&0
    function getComSH(data){
        var ids=[],names=[];
        for(var i=0;i<data.length;i++){
        	ids.push(data[i].value+'&'+data[i].cllist);
        	var v =data[i].cllist;
        	if(ENUM != null)v = ENUM[v];
        	names.push(data[i].name+'&'+v);
        }
        var obj={id: ids.join(','),name: names.join(',')};
        return obj;
    }
    
    //返回还原id
    function getId(name){
    	var str = [];
    	if(!SELECT_DATA){ //不带下拉框，返回的id数组
    		var arr = name.split('|');
    	    if(arr.length>=1)str = arr[1];//第1个数组
    	    
    	}else{ //带下拉框返回id 数组    		
    		var arr = name.split('&');
    		for(var i = 0;i<arr[0].length;i++){
    			str.push(arr[0]);
    		}
    	}
    	return str;
    }
    //下拉数据项
    function comList(){
    	 var arr = SELECT_DATA.split('#');
    	//下拉框数据
		var list = [],
			htmls = [];
		for(var i=0;i<arr.length;i++){
			var arr2 = arr[i].split('&');
		 	htmls.push("<option value='" + arr2[0] +"'>" + arr2[1] + "</option>");
		}
		return htmls.join("");
    }
    //显示选定,还原复选框
    function getCheck(name){
    	var str = "";
    	var arr = name.split('|');
    	if(arr.length>=0)str = arr[0];//第0个数组
    	return str;
    }
    //还原——列表勾选
    function load(data){
    	for(var i=0;i<DEFAULT_DATA.length;i++){
			DEFAULT_DATA[i].LAY_CHECKED = false;
			DEFAULT_DATA[i].cllist = '0';
			if(SELECT_DATA && data.id){ //列表带下拉
				var arr = data.id.split(',');
    			for(var n=0;n<arr.length;n++){
    				var arr2 = arr[n].split('&');
    				if(arr2[0] == DEFAULT_DATA[i].value){
						DEFAULT_DATA[i].LAY_CHECKED = true;
						DEFAULT_DATA[i].cllist = arr2[1];  //下拉框值赋值
						break;
					}
    			}
			}else if(!SELECT_DATA && data.id){  //不带下拉数据
				var str ="", ids = [];
				var arr = data.id.split('|');
    	        if(arr.length>=1)str = arr[1];//第1个数组
    	        if(!str)break;
    	        var ids = str.split(',');
    	        if(ids.length==0)break;
				for(var j=0;j<ids.length;j++){
					if(ids[j] == DEFAULT_DATA[i].value){
						DEFAULT_DATA[i].LAY_CHECKED = true;
						break;
					}
				}
			}
		}
    	
    }
     //是否存在下拉框处理
    function load2(data){
    	
    	if(!SELECT_DATA){ //列表项是否显示下拉框列
			//复选框（显示绑定）还原
		    if(getCheck(data.id)=='2'){
		    	$("input[name='isCheck']").attr("checked",false);
		    	form.render('checkbox');
		    }	
		}else{
			var arr = SELECT_DATA.split('#');
			for(var i=0;i<arr.length;i++){
				var arr2 = arr[i].split('&');
				ENUM[arr2[0]] = arr2[1];
			}
		}
    }
    function initData(data){
    	//列表数据集处理
    	DEFAULT_DATA = data.data.length>0 ? data.data : [];
    	load(data);
	}
    function resultData(data){
    	
    	//列表数据集处理
    	DEFAULT_DATA = data.data.length>0 ? data.data : [];
    	for(var i=0;i<DEFAULT_DATA.length;i++){
			DEFAULT_DATA[i].LAY_CHECKED = false;
			DEFAULT_DATA[i].cllist = '0';
			if(SELECT_DATA && data.id){ //列表带下拉
				var arr = data.id.split(',');
    			for(var n=0;n<arr.length;n++){
    				var arr2 = arr[n].split('&');
    				if(arr2[0] == DEFAULT_DATA[i].value){
						DEFAULT_DATA[i].LAY_CHECKED = true;
						DEFAULT_DATA[i].cllist = arr2[1];  //下拉框值赋值
						break;
					}
    			}
			}else if(!SELECT_DATA && data.id){  //不带下拉数据
				var str ="", ids = [];
				var arr = data.id.split('|');
    	        if(arr.length>=1)str = arr[1];//第1个数组
    	        if(!str)break;
    	        var ids = str.split(',');
    	        if(ids.length==0)break;
				for(var j=0;j<ids.length;j++){
					if(ids[j] == DEFAULT_DATA[i].value){
						DEFAULT_DATA[i].LAY_CHECKED = true;
						break;
					}
				}
			}
		}

    	var list  = [];
    	for(var i in DEFAULT_DATA){
    		list.push(DEFAULT_DATA[i]);
    	}
    	//列表数据加载
	    tableInd.reload({data:list});
    }
	//初始化数据
	function init(){
		//获取子窗体值
        var SHDataStr = parent.getSHValue();
        var data = SHDataStr ? SHDataStr : [];
        SELECT_DATA = data.cllist;//列表带下拉框的下拉数据集
        load2(data); //两种模式数据处理
		resultData(data);
	}
	//初始化
	init();
});