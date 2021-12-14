/**
	@name：选择小组
	@author：liangyl
	@version 2021-06-02
 */
layui.extend({
	uxutil: 'ux/util'
}).use(['uxutil','form','table'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		table = layui.table,
		form = layui.form;
		
	 //外部参数
	var PARAMS = uxutil.params.get(true);
    //站点类型ID
    var HOSTTYPEID = PARAMS.HOSTTYPEID;
	//获取小组列表数据
	var GET_SECTION_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true';
	//获取站点类型对应小组列表数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleHostSectionByHQL?isPlanish=true';
	//保存服务
	var SAVE_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarPreService.svc/LS_UDTO_AddDelLBTranRuleHostSection';
     //获取指定实体字段的最大号
    var GET_MAXNO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBTranRuleHostSection&entityField=DispOrder';

    //当前左列表选择行
    var LEFT_SELECT_DATA =[];
    //当前右列表选择行
    var RIGHT_SELECT_DATA =[];
    //是否第一次加载
    var IS_LOAD = true;
    //左列表 原始数据（刷新后最初的数据）
    var OLD_LEFT_DATA = [];
     //是否第一次加载
    var IS_RIGHT_LOAD = true;
    //右列表 原始数据（刷新后最初的数据）
    var OLD_RIGHT_DATA = [];
    var EMP_ENUM ={};
    
	//左列表实例
	var table_Ind0 = table.render({
		elem: '#lefttable',
		height:'full-75',
		title: '左列表',
		size:'sm',
		initSort:false,
		limit: 5000,
		cols: [[
			{type: 'checkbox',fixed: 'left'},
			{type: 'numbers',title: '行号',fixed: 'left'},
		    {field:'LBTranRuleHostSection_Id',title:'ID',width:150,hide:true},
			{field:'LBTranRuleHostSection_SectionID',title:'项目编号',width:100,hide:true},
			{field:'LBTranRuleHostSection_SectionCName',title:'小组名称',flex:1,templet: function (data) {
				var v = data.LBTranRuleHostSection_SectionID;
				if(EMP_ENUM != null)v = EMP_ENUM[v];
                return v;
            }},
			{field:'LBTranRuleHostSection_DispOrder',title:'次序',width:100,hide:true}
		]],
		defaultOrderBy: JSON.stringify([{property: 'LBTranRuleHostSection_DispOrder',direction: 'ASC'}]),
		text: {none: '暂无相关数据' },
		loading:true,
		page: false
	});

    table_Ind0.reload({data:[]});
	//右列表实例
	var table_Ind1 = table.render({
		elem: '#righttable',
		height:'full-75',
		title: '右列表实例',
		size:'sm',
		initSort:false,
		cols: [[
		    {type: 'checkbox',fixed: 'left'},
		    {type: 'numbers',title: '行号',fixed: 'left'},
			{field:'LBSection_Id',width: 150,title: '小组编号',hide:true},
            {field:'LBSection_CName', minWidth:150,flex:1, title: '小组名称'},
            {field:'LBTranRuleHostSection_Id0', width:150, title: '关系ID', hide: true}
            
		]],
		loading:true,
	    page: false,
		limit: 500,
		defaultOrderBy: JSON.stringify([{property: 'LBSection_DispOrder',direction: 'ASC'}]),
		text: {none: '暂无相关数据' }
	});
	
	table_Ind1.reload({data:[]});
	//下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
    $("input.layui-input+.layui-icon").on('click', function () {
        if (!$(this).hasClass("myDate")) {
            $(this).prev('input.layui-input')[0].click();
            return false;//不加的话 不能弹出
        }
    });
    //左列表监听
	//监听行双击事件
    table.on('rowDouble(lefttable)', function (obj) {
    	var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0)checkCell.click();
        changeData('right');
    });
    //选中状态
    table.on('checkbox(lefttable)', function(obj){
        var leftcheckStatus = table.checkStatus('lefttable');
        LEFT_SELECT_DATA = leftcheckStatus.data;
    });
    //右列表监听
    //监听行双击事件
    table.on('rowDouble(righttable)', function (obj) {
        var tableBox = $(this).parents('.layui-table-box');
    	var index = $(this).attr('data-index');
        var tableDiv = tableBox.find(".layui-table-body.layui-table-main");
    	var checkCell = tableDiv.find("tr[data-index=" + index + "]").find("td div.laytable-cell-checkbox div.layui-form-checkbox I");
	    if (checkCell.length>0)checkCell.click();
        changeData('left');
    });
    //选中状态
    table.on('checkbox(righttable)', function(obj){
        var rightcheckStatus = table.checkStatus('righttable');
        RIGHT_SELECT_DATA = rightcheckStatus.data;
    });
    // 按钮事件
	//向左移动
	$('#add').on('click',function(){
		changeData('left');
	});
	//向右移动
	$('#remove').on('click',function(){
		changeData('right');
	});
	//重置
	$('#reset').on('click',function(){
        load();
	});
	//保存
	$('#save').on('click',function(){
		onSaveClick();
	});
	//查询
	 //监听查询，小组列表
	$('#search').on('click',function(){ 
		rightLoad();
	});
	 //回车事件
    $("#searchText").on('keydown', function (event) {
        if (event.keyCode == 13) {
        	rightLoad();
            return false;
        }
    });
	//右列表 剔除已选左列表数据
    function resultData(data){
		//左列表现有数据
	    var leftData = table.cache['lefttable'];
		var list = [],isExec=true;
		for(var i=0;i<data.length;i++){
			isExec = true;
    		for(var j=0;j<leftData.length;j++){
    		    if(data[i].LBSection_Id == leftData[j].LBTranRuleHostSection_SectionID){
    		    	isExec = false;
    		    	break;
    		    }
    	    }
    		if(isExec)list.push(data[i]);
        }
    	return list;
	}
		
    //type向那边添加数据
    function changeData(type){
    	//左列表现有数据
    	var leftData = table.cache['lefttable'];
        var rightData =  table.cache['righttable'];
    	if(type=='left'){//向左移动数据
		   var obj = addLeft(leftData,rightData);
    	}
    	if(type=='right'){//向右移动数据
		   var obj = addRight(leftData,rightData);
    	}
    	
    	if(!obj)return;
    	IS_LOAD=false;
        leftLoad(obj.leftData);
    	rightLoad();
    }
   
   //向左列表添加数据
    function addLeft(leftData,rightData){
   	    //获取右列表选择数据
	    if(RIGHT_SELECT_DATA.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var r_d=[]; 
        leftData.reverse()
        var d1 = [];
        for (var i = 0; i < rightData.length; i++) {
            if(rightData[i].LAY_CHECKED && rightData[i].LAY_CHECKED===true){
                delete rightData[i].LAY_CHECKED
                rightData[i].LAY_TABLE_INDEX && delete rightData[i].LAY_TABLE_INDEX
                 var obj = {
                    'LBTranRuleHostSection_SectionID':  rightData[i].LBSection_Id,
                    'LBTranRuleHostSection_SectionCName':  rightData[i].LBSection_CName,
                    'LBTranRuleHostSection_Id':  rightData[i].LBTranRuleHostSection_Id0,
                    'isAdd':'1'
                };
                leftData.push(obj)
            }else{
                delete rightData[i].LAY_TABLE_INDEX
                r_d.push(rightData[i])
            }
        }
        rightData = r_d;
        return {leftData:leftData,rightData:rightData};
    }
    
    //向右边列表添加数据
    function addRight(leftData,rightData){
    	//获取右列表选择数据
	    if(LEFT_SELECT_DATA.length==0){
            layer.msg("未选择项目！", { icon: 5, anim: 6 });
            return;
        }
        var l_d=[]; 
        rightData.reverse();
        var d1 = [];
        for (var i = 0; i < leftData.length; i++) {
            if(leftData[i].LAY_CHECKED && leftData[i].LAY_CHECKED===true){
                delete leftData[i].LAY_CHECKED
                leftData[i].LAY_TABLE_INDEX && delete leftData[i].LAY_TABLE_INDEX
                 var obj = {
                    'LBSection_Id': leftData[i].LBTranRuleHostSection_SectionID,
                    'LBSection_CName': leftData[i].LBTranRuleHostSection_SectionCName,
                    'LBTranRuleHostSection_Id0' : leftData[i].LBTranRuleHostSection_Id
                };
                rightData.push(obj)
            }else{
                delete leftData[i].LAY_TABLE_INDEX
                l_d.push(leftData[i])
            }
        }
        leftData = l_d;
        return {leftData:leftData,rightData:rightData};
    }
	
	
	//左列表数据加载
	function leftLoad(list){
		table_Ind0.reload({
            data: list
        });
	}
	
	//右列表数据加载
	function rightLoad(){
		var list = OLD_RIGHT_DATA;
		if(list.length>0)list=resultData(list);

		if($('#searchText').val()){
			list = searchList($('#searchText').val(),list);
		}
		table_Ind1.reload({
            data: list
        });
	}
	//获取指定实体字段的最大号
    function getMaxNo(callback) {
        var me = this;
        var result = "";
        uxutil.server.ajax({
            url: GET_MAXNO_URL,
            async:false
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
               layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    }
	 //保存
	function onSaveClick() {
		//新增实体
		var entity = getEntity();
		var params = JSON.stringify(entity);
		//显示遮罩层
		var config = {
			type: "POST",
			url: SAVE_URL,
			data: params
		};
		uxutil.server.ajax(config, function(data) {
			//隐藏遮罩层
			if (data.success) {
				var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
	            parent.layer.close(index); //再执行关闭
	            parent.afterUpdateAddLink();
			} else {
				if(!data.msg)data.msg='保存失败';
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	function getEntity(){
    	var entity={},addList=[],delIDList=[];
    	var leftData = table.cache['lefttable'];
    	var DispOrder = 0;
     	getMaxNo(function(val){
     		DispOrder =Number(val);
     	});
             	
    	//新增
    	for (var i = 0; i < leftData.length; i++) {
             if (leftData[i].isAdd =='1') {
                var DataTimeStamp = [0,0,0,0,0,0,0,0];
                addList.push({
                	SectionID:leftData[i].LBTranRuleHostSection_SectionID,
                	HostTypeID:HOSTTYPEID,
                	DispOrder:DispOrder+i
                });
            }
        }
    	//删除 
    	for (var i = 0; i < OLD_LEFT_DATA.length; i++) {
            var delFlag = true;//是删除的
            for (var j = 0; j < leftData.length; j++) {
                if (OLD_LEFT_DATA[i].LBTranRuleHostSection_SectionID == leftData[j].LBTranRuleHostSection_SectionID) {
                    delFlag = false;//存在 不是删除的
                    break;
                }
            }
            if (delFlag)delIDList.push(OLD_LEFT_DATA[i].LBTranRuleHostSection_Id);
        }
    	delIDList  = delIDList.join();
    	//第二个如果为true新增时 则判断实体是否已经存在 否则不判断
        var isCheckEntityExist = true;
    	entity={addEntityList:addList,isCheckEntityExist:isCheckEntityExist,delIDList:delIDList};
    	return entity;
    }
	//获取小组数据
	function LBSection(callback){
		var cols = table_Ind1.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_SECTION_LIST_URL+'&fields='+fields.join(',')+'&sort='+table_Ind1.config.defaultOrderBy;
	    loadData(url,function(data){
	    	OLD_RIGHT_DATA = data;
        	for(var i=0;i<OLD_RIGHT_DATA.length;i++){
        		EMP_ENUM[OLD_RIGHT_DATA[i].LBSection_Id] = OLD_RIGHT_DATA[i].LBSection_CName;
        	}
        	callback(OLD_RIGHT_DATA);
	    });
	}
	//获取站点类型对应小组
	function TranRuleHostSection(callback){
		var cols = table_Ind0.config.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL+'&fields='+fields.join(',')+'&sort='+table_Ind0.config.defaultOrderBy;
	    url +='&where=lbtranrulehostsection.HostTypeID='+HOSTTYPEID;
	    loadData(url,function(data){
	    	OLD_LEFT_DATA = data;
	    	callback(data);
	    });
	}
	function loadData(url,callback){
		uxutil.server.ajax({
			url:url
		},function(data){
			var list = data.value.list|| [];
			callback(list);
		});
	}
	/*
	 * 模糊查询一个数组
	 */
    function searchList(str, container) {
	    var newList = [];
	    //新的列表
	    var startChar = str.charAt(0);
	    //开始字符
	    var strLen = str.length;
	    //查找符串的长度
	    for (var i = 0; i < container.length; i++) {
	        var obj = container[i];
	        var isMatch = false;
	        for (var p in obj) {
	            if ( typeof (obj[p]) == "function") {
	                obj[p]();
	            } else {
	                var curItem = "";
	                if(obj[p]!=null){
	                    curItem = obj[p];
	                }
	                for (var j = 0; j < curItem.length; j++) {
	                    if (curItem.charAt(j) == startChar)//如果匹配起始字符,开始查找
	                    {
	                        if (curItem.substring(j).substring(0, strLen) == str)//如果从j开始的字符与str匹配，那ok
	                        {
	                            isMatch = true;
	                            break;
	                        }
	                    }
	                }
	            }
	        }
	        if (isMatch) {
	            newList.push(obj);
	        }
	    }
	    return newList;
	}
    function load(){
    	LBSection(function(data){
        	TranRuleHostSection(function(data1){
        		leftLoad(data1);
        		rightLoad();
        	});
        });
    }
	//初始化
	function init(){
		//默认高度加载
		$(".fiexdHeight").css("height", ($(window).height() - 50) + "px");//设置中间容器高度
        load();
	}
	
	//初始化
	init();
});